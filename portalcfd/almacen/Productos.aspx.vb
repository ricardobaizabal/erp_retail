Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Class almacen_portalcfd_Productos
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblProductsListLegend.Text = Resources.Resource.lblProductsListLegend
            'lblProductEditLegend.Text = Resources.Resource.lblProductEditLegend

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblCode.Text = Resources.Resource.lblCode
            lblUnit.Text = Resources.Resource.lblUnit
            lblUnitaryPrice.Text = Resources.Resource.lblUnitaryPrice
            lblDescription.Text = Resources.Resource.lblDescription

            '''''''''''''''''''
            'Validators Titles'
            '''''''''''''''''''

            valCode.Text = Resources.Resource.validatorMessage
            valUnidad.Text = Resources.Resource.validatorMessage
            valCostoStd.Text = Resources.Resource.validatorMessage
            valDescripcion.Text = Resources.Resource.validatorMessage
            valClaveProductoServicio.Text = Resources.Resource.validatorMessage
            valTasa.Text = Resources.Resource.validatorMessage
            valMoneda.Text = Resources.Resource.validatorMessage

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnAddProduct.Text = Resources.Resource.btnAddProduct
            btnSaveProduct.Text = Resources.Resource.btnSave
            btnCancel.Text = Resources.Resource.btnCancel

            Dim objCat As New DataControl(1)
            objCat.Catalogo(unidadmedidaid, "select id, descripcion from tblUnidadMedida order by id", 0)
            objCat.Catalogo(tasaid, "select id, nombre from tblTasa order by id", 0)
            objCat.Catalogo(monedaid, "select id, nombre from tblMoneda order by id", 0)
            objCat.FillRadComboBox(cmbProveedor, "select id, razonsocial from tblMisProveedores order by razonsocial")
            objCat.Catalogo(filtrofamiliaid, "select id, nombre from tblFamilia where isnull(borradoBit,0)=0 order by nombre", 0)
            objCat.Catalogo(filtrosubfamiliaid, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 order by nombre", 0)
            objCat.Catalogo(filtroproveedorid, "select id, razonsocial from tblMisProveedores order by razonsocial", 0)
            objCat.Catalogo(familiaid, "select id, nombre from tblFamilia where isnull(borradoBit,0)=0 order by nombre", 0)
            objCat.Catalogo(subfamiliaid, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 order by nombre", 0)
            objCat.CatalogoString(claveprodservid, "select clave, isnull(clave,'') + ' - ' + nombre as descripcion from tblClaveProducto order by nombre", 0)
            objCat.Catalogo(unidadid, "select id, isnull(clave,'') + ' - ' + descripcion from tblUnidadMedida order by descripcion", 0)
            objCat.Catalogo(cmbDia, "select diaid, isnull(descripcion,'') as descripcion from tblDias order by diaid", 0)
            objCat.Catalogo(cbmObjetoImpuesto, "select id, descripcion from tblObjetoImp order by id", 0)
            'objCat.Catalogo(cmbNivelEscolar, "select id, nombre from tblNivelEscolar where isnull(borradobit,0)=0", 0)
            objCat.FillRadComboBox(cmbNivelEscolar, "select id, nombre from tblNivelEscolar where isnull(borradobit,0)=0")
            objCat = Nothing

            chkInventariableBit.Checked = True

            lblNivelEscolar.Visible = CBool(Session("moduloescolar"))
            cmbNivelEscolar.Visible = CBool(Session("moduloescolar"))
            valNivelEscolar.Enabled = CBool(Session("moduloescolar"))

            If Session("moduloescolar") Then
                RadTabStrip1.Tabs(3).Visible = False
                RadTabStrip1.Tabs(4).Visible = False
                RadTabStrip1.Tabs(5).Visible = False
            Else
                RadTabStrip1.Tabs(3).Visible = True
                RadTabStrip1.Tabs(4).Visible = True
            End If

            If Session("perfilid") <> 1 Then
                btnAddProduct.Visible = False
            End If

            If CBool(Session("restriccionProductoBit")) = True Then
                btnAddProduct.Visible = False
                txtCode.Enabled = False
                txtCodigoBarras.Enabled = False
                unidadmedidaid.Enabled = False
                txtDescription.Enabled = False
                txtUnitaryPrice.Enabled = False

                RadTabStrip1.Tabs(1).Visible = False
                RadTabStrip1.Tabs(2).Visible = False
                RadTabStrip1.Tabs(3).Visible = False
                RadTabStrip1.Tabs(4).Visible = False
                RadTabStrip1.Tabs(5).Visible = False
            End If

        End If
    End Sub

#End Region

#Region "Load List Of Products"

    Function GetProducts() As DataSet
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pMisProductos @cmd=1, @txtSearch='" & txtSearch.Text & "', @familiaid='" & filtrofamiliaid.SelectedValue.ToString & "', @subfamiliaid='" & filtrosubfamiliaid.SelectedValue.ToString & "', @proveedorid='" & filtroproveedorid.SelectedValue.ToString & "'", conn)
        Dim ds As DataSet = New DataSet
        Try
            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        Return ds
    End Function

#End Region

#Region "Telerik Grid Products Loading Events"

    Protected Sub productslist_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles productslist.NeedDataSource

        If Not e.IsFromDetailTable Then

            productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
            productslist.DataSource = GetProducts()

        End If

    End Sub

#End Region

#Region "Telerik Grid Language Modification(Spanish)"

    Protected Sub productslist_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles productslist.Init

        productslist.PagerStyle.NextPagesToolTip = "Ver mas"
        productslist.PagerStyle.NextPageToolTip = "Siguiente"
        productslist.PagerStyle.PrevPagesToolTip = "Ver más"
        productslist.PagerStyle.PrevPageToolTip = "Atrás"
        productslist.PagerStyle.LastPageToolTip = "Última Página"
        productslist.PagerStyle.FirstPageToolTip = "Primera Página"
        productslist.PagerStyle.PagerTextFormat = "{4}    Página {0} de {1}, Registros {2} al {3} de {5}"
        productslist.SortingSettings.SortToolTip = "Ordernar"
        productslist.SortingSettings.SortedAscToolTip = "Ordenar Asc"
        productslist.SortingSettings.SortedDescToolTip = "Ordenar Desc"


        Dim menu As Telerik.Web.UI.GridFilterMenu = productslist.FilterMenu
        Dim i As Integer = 0

        While i < menu.Items.Count

            If menu.Items(i).Text = "NoFilter" Or menu.Items(i).Text = "Contains" Then
                i = i + 1
            Else
                menu.Items.RemoveAt(i)
            End If

        End While

        Call ModificaIdiomaGrid()

    End Sub

    Private Sub ModificaIdiomaGrid()

        productslist.GroupingSettings.CaseSensitive = False

        Dim Menu As Telerik.Web.UI.GridFilterMenu = productslist.FilterMenu
        Dim item As Telerik.Web.UI.RadMenuItem

        For Each item In Menu.Items

            ''''''''''''''''''''''''''''''''''''''''''''''
            'Change The Text For The StartsWith Menu Item'
            ''''''''''''''''''''''''''''''''''''''''''''''

            If item.Text = "StartsWith" Then
                item.Text = "Empieza con"
            End If

            If item.Text = "NoFilter" Then
                item.Text = "Sin Filtro"
            End If

            If item.Text = "Contains" Then
                item.Text = "Contiene"
            End If

            If item.Text = "EndsWith" Then
                item.Text = "Termina con"
            End If

        Next

    End Sub

#End Region

#Region "Telerik Grid Products Editing & Deleting Events"

    Protected Sub productslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles productslist.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Products" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('" & Resources.Resource.ProductsDeleteConfirmationMessage & "');")
                'Dim lblcodigobarras As Label = CType(dataItem("codigobarras").FindControl("lblcodigobarras"), Label)

                If e.Item.DataItem("existencia") > 0 Then
                    lnkdel.Visible = False
                End If

                'If Session("moduloescolar") Then


                '    lblCodigoBarras.Visible = False

                'Else
                '    lblCodigoBarras.Visible = True

                'End If

            End If

        End If

    End Sub

    Protected Sub productslist_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles productslist.ItemCommand

        Select Case e.CommandName

            Case "cmdEdit"
                EditProduct(e.CommandArgument)
                ObtenerPresentaciones()
            Case "cmdDelete"
                DeleteProduct(e.CommandArgument)

        End Select

        If e.CommandName = RadGrid.ExportToExcelCommandName Then
            productslist.MasterTableView.GetColumn("Delete").Visible = False
        End If

    End Sub

    Private Sub DeleteProduct(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pMisProductos  @cmd='2', @productoId ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelProductRegistration.Visible = False

            productslist.DataSource = GetProducts()
            productslist.DataBind()

        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub EditProduct(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pMisProductos @cmd=4, @productoid='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                'If Session("perfilid") <> 1 Then
                '    txtCostoStd.Enabled = True
                '    txtUnitaryPrice.Enabled = True
                'End If

                txtCode.Text = rs("codigo")
                txtUnitaryPrice.Text = rs("unitario")
                txtprecioalterno.Text = rs("precioalterno")
                txtDescription.Text = rs("descripcion")
                txtReorden.Text = rs("punto_reorden")
                txtCostoStd.Text = rs("costo_estandar")
                txtTipoCambio.Text = rs("tipo_cambio_std")
                txtCodigoBarras.Text = rs("codigo_barras")
                txtieps.Text = rs("ieps")
                lblDescProductoPresentacionesValue.Text = rs("codigo") & " - " & rs("descripcion")
                lblDescProductoMargenesValue.Text = rs("codigo") & " - " & rs("descripcion")
                lblDescHorarioValue.Text = rs("codigo") & " - " & rs("descripcion")
                lblDescProductoFotoValue.Text = rs("codigo") & " - " & rs("descripcion")
                lblDescProductoPuntoReordenValue.Text = rs("codigo") & " - " & rs("descripcion")

                RadMultiPage1.SelectedIndex = 0
                RadTabStrip1.Tabs(0).Selected = True
                panelProductRegistration.Visible = True
                InsertOrUpdate.Value = 1
                ProductID.Value = id

                Dim objCat As New DataControl(1)
                objCat.Catalogo(unidadmedidaid, "select id, descripcion from tblUnidadMedida order by id", rs("unidadmedidaid"))
                objCat.Catalogo(tasaid, "select id, nombre from tblTasa order by id", rs("tasaid"))
                objCat.Catalogo(monedaid, "select id, nombre from tblMoneda order by id", rs("monedaid"))
                'objCat.Catalogo(proveedorid, "select id, razonsocial from tblMisProveedores order by razonsocial", rs("proveedorid"))
                objCat.FillRadComboBox(cmbProveedor, "select id, razonsocial from tblMisProveedores order by razonsocial")
                objCat.Catalogo(familiaid, "select id, nombre from tblFamilia where isnull(borradoBit,0)=0 order by nombre", rs("familiaid"))
                objCat.Catalogo(subfamiliaid, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 order by nombre", rs("subfamiliaid"))
                objCat.CatalogoString(claveprodservid, "select clave, isnull(clave,'') + ' - ' + nombre as descripcion from tblClaveProducto order by nombre", rs("claveprodserv"))
                objCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal where isnull(borradobit,0)=0 order by nombre", 0)
                objCat.FillRadComboBox(cmbNivelEscolar, "select id, nombre from tblNivelEscolar where isnull(borradobit,0)=0")
                objCat = Nothing

                cbmObjetoImpuesto.SelectedValue = rs("objeto_impuestoid")
                cmbNivelEscolar.SelectedValue = rs("nivelescolarid")

                chkPredeterminadoProducto.Checked = CBool(rs("predeterminadoAlmacenBit"))
                chkInventariableBit.Checked = CBool(rs("inventariableBit"))
                chkBasculaBit.Checked = CBool(rs("basculaBit"))

                Call CrearTablaTemp()
                Call ObtenerPresentaciones()
                Call ObtenerMargenesUtilidad()
                Call ObtenerHorarios()
                Call LimpiarPresentacion()
                Call CargaFotos()
                Call AsignarPuntoReorden()

                Dim ObjData As New DataControl(1)
                combinacionesList.DataSource = ObjData.FillDataSet("EXEC pProductoCombinacion @cmd=1, @productoid='" & ProductID.Value.ToString & "'")
                combinacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
                combinacionesList.DataBind()

                ExistenciasCombinacionesList.DataSource = ObjData.FillDataSet("EXEC pProductoAlmacen @cmd=8, @productoid='" & ProductID.Value.ToString & "', @almacenid='" & cmbSucursal.SelectedValue.ToString & "'")
                ExistenciasCombinacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
                ExistenciasCombinacionesList.DataBind()

                grdProductosPuntoReorden.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
                grdProductosPuntoReorden.DataSource = ProductosPuntoReorden()
                grdProductosPuntoReorden.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
                grdProductosPuntoReorden.DataBind()

                Dim ds As DataSet = New DataSet
                ds = ObjData.FillDataSet("EXEC pMisProductos @cmd=14, @productoid='" & ProductID.Value.ToString & "'")

                If ds.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In ds.Tables(0).Rows
                        Dim collection As IList(Of RadComboBoxItem) = cmbProveedor.Items
                        If (collection.Count <> 0) Then
                            For Each item As RadComboBoxItem In collection
                                If item.Value = row("proveedorid") Then
                                    item.Checked = True
                                End If
                            Next
                        End If
                    Next
                End If

                If Session("moduloescolar") Then
                    ds = ObjData.FillDataSet("EXEC pMisProductos @cmd=20, @productoid='" & ProductID.Value.ToString & "'")

                    If ds.Tables(0).Rows.Count > 0 Then
                        For Each row As DataRow In ds.Tables(0).Rows
                            Dim collection As IList(Of RadComboBoxItem) = cmbNivelEscolar.Items
                            If (collection.Count <> 0) Then
                                For Each item As RadComboBoxItem In collection
                                    If item.Value = row("nivelescolarid") Then
                                        item.Checked = True
                                    End If
                                Next
                            End If
                        Next
                    End If
                End If
                ObjData = Nothing

                panelProductList.Visible = False

            End If

        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

#End Region

#Region "Telerik Grid Products Column Names (From Resource File)"

    Protected Sub productslist_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles productslist.ItemCreated

        If TypeOf e.Item Is GridHeaderItem Then

            Dim header As GridHeaderItem = CType(e.Item, GridHeaderItem)
            'Dim fila As GridItemCollection = e.Item
            If e.Item.OwnerTableView.Name = "Products" Then
                'If Session("moduloescolar") Then
                '    header("codigobarras").Text = ""
                'End If
                header("codigo").Text = Resources.Resource.gridColumnNameCode
                header("claveunidad").Text = Resources.Resource.gridColumnNameMeasureUnit
                header("descripcion").Text = Resources.Resource.gridColumnNameDescription
                'header("unitario").Text = Resources.Resource.gridColumnNameUnitaryPrice
                'header("unitario2").Text = Resources.Resource.gridColumnNameUnitaryPrice2
                'header("unitario3").Text = Resources.Resource.gridColumnNameUnitaryPrice3

            End If

        End If

    End Sub

#End Region

#Region "Display Product Data Panel"

    Protected Sub btnAddProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddProduct.Click

        InsertOrUpdate.Value = 0

        txtCode.Text = ""
        txtCodigoBarras.Text = ""
        unidadmedidaid.SelectedValue = 0
        txtUnitaryPrice.Text = ""
        txtDescription.Text = ""
        tasaid.SelectedValue = 0
        txtReorden.Text = ""
        txtCostoStd.Text = ""
        monedaid.SelectedValue = 0
        txtTipoCambio.Text = ""
        cmbNivelEscolar.SelectedValue = 0
        cmbProveedor.ClearCheckedItems()
        chkInventariableBit.Checked = True
        chkBasculaBit.Checked = False
        familiaid.SelectedValue = 0
        subfamiliaid.SelectedValue = 0
        cbmObjetoImpuesto.SelectedValue = 0
        panelProductRegistration.Visible = True
        RadTabStrip1.Tabs.Item(0).Selected = True
        RadMultiPage1.SelectedIndex = 0

    End Sub

#End Region

#Region "Save Product"

    Protected Sub btnSaveClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveProduct.Click

        Dim mensaje As String = ""
        Dim inventariableBit As Integer = 0
        Dim predeterminadoAlmacenBit As Integer = 0
        Dim basculaBit As Integer = 0

        If chkInventariableBit.Checked = True Then
            inventariableBit = 1
        Else
            inventariableBit = 0
        End If

        If chkPredeterminadoProducto.Checked = True Then
            predeterminadoAlmacenBit = 1
        Else
            predeterminadoAlmacenBit = 0
        End If

        If chkBasculaBit.Checked = True Then
            basculaBit = 1
        Else
            basculaBit = 0
        End If

        Try
            If InsertOrUpdate.Value = 0 Then
                Dim ObjData As New DataControl(1)
                Dim p As New ArrayList
                p.Clear()
                p.Add(New SqlParameter("@cmd", 3))
                p.Add(New SqlParameter("@codigo", txtCode.Text))
                p.Add(New SqlParameter("@codigo_barras", txtCodigoBarras.Text))
                p.Add(New SqlParameter("@unidadmedidaid", unidadmedidaid.SelectedValue))
                p.Add(New SqlParameter("@unitario", txtUnitaryPrice.Text))
                p.Add(New SqlParameter("@descripcion", txtDescription.Text))
                p.Add(New SqlParameter("@tasaid", tasaid.SelectedValue))
                p.Add(New SqlParameter("@punto_reorden", txtReorden.Text))
                p.Add(New SqlParameter("@costo_estandar", txtCostoStd.Text))
                p.Add(New SqlParameter("@monedaid", monedaid.SelectedValue))
                p.Add(New SqlParameter("@tipo_cambio_std", txtTipoCambio.Text))
                p.Add(New SqlParameter("@inventariableBit", inventariableBit))
                p.Add(New SqlParameter("@basculaBit", basculaBit))
                p.Add(New SqlParameter("@familiaid", familiaid.SelectedValue))
                p.Add(New SqlParameter("@subfamiliaid", subfamiliaid.SelectedValue))
                p.Add(New SqlParameter("@predeterminadoAlmacenBit", predeterminadoAlmacenBit))
                p.Add(New SqlParameter("@claveprodserv", claveprodservid.SelectedValue))
                p.Add(New SqlParameter("@ieps", txtIeps.Text))
                p.Add(New SqlParameter("@objeto_impuestoid", cbmObjetoImpuesto.SelectedValue))
                'p.Add(New SqlParameter("@nivelescolarid", cmbNivelEscolar.SelectedValue))
                Dim ds As DataSet = New DataSet
                ds = ObjData.FillDataSet("pMisProductos", p)
                'mensaje = ObjData.RunSQLScalarQueryString("pMisProductos", 1, p)

                If ds.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In ds.Tables(0).Rows
                        mensaje = row("mensaje")
                        ProductID.Value = row("id")
                    Next

                    For Each item As RadComboBoxItem In cmbProveedor.CheckedItems
                        ObjData.RunSQLQuery("EXEC pMisProductos @cmd=13, @productoid='" & ProductID.Value.ToString & "', @proveedorid='" & item.Value.ToString & "'")
                    Next

                    For Each item As RadComboBoxItem In cmbNivelEscolar.CheckedItems
                        ObjData.RunSQLQuery("EXEC pMisProductos @cmd=19, @productoid='" & ProductID.Value.ToString & "', @nivelescolarid='" & item.Value.ToString & "'")
                    Next

                End If

                If mensaje.Length > 0 Then
                    RadAlert.RadAlert(mensaje, 330, 180, "Alerta", "", "")
                Else
                    RadAlert.RadAlert("Producto agregado exitosamente.", 330, 180, "Alerta", "", "")
                    panelProductRegistration.Visible = False
                End If

                ObjData = Nothing

            Else
                Dim ObjData As New DataControl(1)
                Dim p As New ArrayList
                p.Clear()
                p.Add(New SqlParameter("@cmd", 5))
                p.Add(New SqlParameter("@productoid", ProductID.Value))
                p.Add(New SqlParameter("@codigo", txtCode.Text))
                p.Add(New SqlParameter("@codigo_barras", txtCodigoBarras.Text))
                p.Add(New SqlParameter("@unidadmedidaid", unidadmedidaid.SelectedValue))
                p.Add(New SqlParameter("@unitario", txtUnitaryPrice.Text))
                p.Add(New SqlParameter("@descripcion", txtDescription.Text))
                p.Add(New SqlParameter("@tasaid", tasaid.SelectedValue))
                p.Add(New SqlParameter("@punto_reorden", txtReorden.Text))
                p.Add(New SqlParameter("@costo_estandar", txtCostoStd.Text))
                p.Add(New SqlParameter("@monedaid", monedaid.SelectedValue))
                p.Add(New SqlParameter("@tipo_cambio_std", txtTipoCambio.Text))
                p.Add(New SqlParameter("@inventariableBit", inventariableBit))
                p.Add(New SqlParameter("@basculaBit", basculaBit))
                p.Add(New SqlParameter("@familiaid", familiaid.SelectedValue))
                p.Add(New SqlParameter("@subfamiliaid", subfamiliaid.SelectedValue))
                p.Add(New SqlParameter("@predeterminadoAlmacenBit", predeterminadoAlmacenBit))
                p.Add(New SqlParameter("@claveprodserv", claveprodservid.SelectedValue))
                p.Add(New SqlParameter("@ieps", txtIeps.Text))
                p.Add(New SqlParameter("@objeto_impuestoid", cbmObjetoImpuesto.SelectedValue))
                'p.Add(New SqlParameter("@nivelescolarid", cmbNivelEscolar.SelectedValue))
                mensaje = ObjData.RunSQLScalarQueryString("pMisProductos", 1, p)
                panelProductRegistration.Visible = False

                ObjData.RunSQLQuery("EXEC pMisProductos @cmd=12, @productoid='" & ProductID.Value.ToString & "'")

                For Each item As RadComboBoxItem In cmbProveedor.CheckedItems
                    ObjData.RunSQLQuery("EXEC pMisProductos @cmd=13, @productoid='" & ProductID.Value.ToString & "', @proveedorid='" & item.Value.ToString & "'")
                Next

                ObjData.RunSQLQuery("EXEC pMisProductos @cmd=18, @productoid='" & ProductID.Value.ToString & "'")

                For Each item As RadComboBoxItem In cmbNivelEscolar.CheckedItems
                    ObjData.RunSQLQuery("EXEC pMisProductos @cmd=19, @productoid='" & ProductID.Value.ToString & "', @nivelescolarid='" & item.Value.ToString & "'")
                Next

                ObjData = Nothing

            End If
        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        End Try

        Call Cancelar()
        panelProductList.Visible = True
        productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        productslist.DataSource = GetProducts()
        productslist.DataBind()

    End Sub

#End Region

#Region "Cancel Product (Save/Edit)"

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Call Cancelar()
    End Sub

    Private Sub Cancelar()
        InsertOrUpdate.Value = 0
        txtCode.Text = ""
        txtCodigoBarras.Text = ""
        unidadmedidaid.SelectedValue = 0
        txtUnitaryPrice.Text = ""
        txtDescription.Text = ""
        tasaid.SelectedValue = 0
        txtReorden.Text = ""
        txtCostoStd.Text = ""
        monedaid.SelectedValue = 0
        txtTipoCambio.Text = ""
        cmbNivelEscolar.SelectedValue = 0
        cmbProveedor.ClearCheckedItems()
        chkInventariableBit.Checked = False
        chkBasculaBit.Checked = False
        familiaid.SelectedValue = 0
        subfamiliaid.SelectedValue = 0
        claveprodservid.SelectedValue = 0
        panelProductRegistration.Visible = False
    End Sub

#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Call Cancelar()
        panelProductList.Visible = True
        productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        productslist.DataSource = GetProducts()
        productslist.DataBind()
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        txtSearch.Text = ""
        panelProductList.Visible = True
        filtrofamiliaid.SelectedValue = 0
        filtrosubfamiliaid.SelectedValue = 0
        filtrosubfamiliaid.Enabled = False
        productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        productslist.DataSource = GetProducts()
        productslist.DataBind()
    End Sub

    Private Sub familiaid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles familiaid.SelectedIndexChanged
        If familiaid.SelectedValue = 0 Then
            subfamiliaid.Enabled = False
            subfamiliaid.SelectedValue = 0
        Else
            subfamiliaid.Enabled = True
            Dim objCat As New DataControl(1)
            objCat.Catalogo(subfamiliaid, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 and familiaid='" & familiaid.SelectedValue & "' order by nombre", 0)
            objCat = Nothing
        End If
    End Sub

    Private Sub EliminaCombinacion(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pProductoCombinacion @cmd=4, @id ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            Dim ObjData As New DataControl(1)
            combinacionesList.DataSource = ObjData.FillDataSet("EXEC pProductoCombinacion @cmd=1, @productoid='" & ProductID.Value.ToString & "'")
            combinacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
            combinacionesList.DataBind()
            ObjData = Nothing

        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub filtrofamiliaid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles filtrofamiliaid.SelectedIndexChanged
        If filtrofamiliaid.SelectedValue = 0 Then
            filtrosubfamiliaid.Enabled = False
            filtrosubfamiliaid.SelectedValue = 0
        Else
            filtrosubfamiliaid.Enabled = True
            Dim objCat As New DataControl(1)
            objCat.Catalogo(filtrosubfamiliaid, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 and familiaid='" & filtrofamiliaid.SelectedValue & "' order by nombre", 0)
            objCat = Nothing
        End If
    End Sub

    Private Sub combinacionesList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles combinacionesList.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                EliminaCombinacion(e.CommandArgument)
        End Select
    End Sub

    Private Sub combinacionesList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles combinacionesList.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
                btnDelete.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea eliminar el registro?');")
            Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim txtUnitario As RadNumericTextBox = DirectCast(e.Item.FindControl("txtUnitario"), RadNumericTextBox)
                Dim txtCUnitario As RadNumericTextBox = DirectCast(e.Item.FindControl("txtCUnitario"), RadNumericTextBox)
                If e.Item.DataItem("sucursalid") = 0 Then
                    txtUnitario.Visible = False
                    txtCUnitario.Visible = False
                End If


        End Select
    End Sub

    Private Sub combinacionesList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles combinacionesList.NeedDataSource
        Dim ObjData As New DataControl(1)
        combinacionesList.DataSource = ObjData.FillDataSet("EXEC pProductoCombinacion @cmd=1, @productoid='" & ProductID.Value.ToString & "'")
        combinacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        ObjData = Nothing
    End Sub

    Private Sub btnAgregaCombinacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregaCombinacion.Click
        panelRegistroCombinacion.Visible = True
        Session("dtAtributoValor").Rows.Clear()
        txtCodigoBarrasCombinacion.Text = ""
        txtCombinacion.Text = ""
        btnGuardarCombinacion.Enabled = False

        Dim objCat As New DataControl(1)
        objCat.Catalogo(atributoid, "select id, nombre from tblAtributos where isnull(borradobit,0)=0 order by nombre", 0)
        objCat.Catalogo(valorid, "select id, nombre from tblValorAtributo where atributoid='" & atributoid.SelectedValue.ToString & "' and isnull(borradobit,0)=0 order by nombre", 0)
        objCat = Nothing
    End Sub

    Private Sub btnGuardarCombinacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarCombinacion.Click
        Try
            Dim mensaje As String = ""
            Dim combinacion As Integer = 0
            Dim ObjData As New DataControl(1)
            Dim p As New ArrayList
            Dim registros As Integer = 0
            Dim coincidencias As Integer = 0
            Dim i As Integer = 0
            registros = Session("dtAtributoValor").Rows.Count()

            For Each row As DataRow In Session("dtAtributoValor").Rows
                Dim atributoid As Integer = CInt(row("atributoid"))
                Dim valorid As Integer = CInt(row("valorid"))
                Dim existe As Integer = 0
                p.Clear()
                p.Add(New SqlParameter("@cmd", 7))
                p.Add(New SqlParameter("@productoid", ProductID.Value))
                p.Add(New SqlParameter("@atributoid", atributoid))
                p.Add(New SqlParameter("@valorid", valorid))
                existe = ObjData.SentenceScalarLong("pProductoCombinacion", 1, p)

                If existe > 0 Then
                    i = i + 1
                End If

            Next

            If i = registros Then
                RadAlert.RadAlert("Esta combinacion ya existe, favor de verificar.", 330, 180, "Alerta", "", "")
            Else
                p.Clear()
                p.Add(New SqlParameter("@cmd", 2))
                p.Add(New SqlParameter("@productoid", ProductID.Value))
                p.Add(New SqlParameter("@codigo_barras", txtCodigoBarrasCombinacion.Text))
                p.Add(New SqlParameter("@descripcion", txtCombinacion.Text))
                combinacion = ObjData.SentenceScalarLong("pProductoCombinacion", 1, p)

                For Each row As DataRow In Session("dtAtributoValor").Rows
                    Dim atributoid As Integer = CInt(row("atributoid"))
                    Dim valorid As Integer = CInt(row("valorid"))

                    p.Clear()
                    p.Add(New SqlParameter("@cmd", 6))
                    p.Add(New SqlParameter("@combinacionid", combinacion))
                    p.Add(New SqlParameter("@productoid", ProductID.Value))
                    p.Add(New SqlParameter("@atributoid", atributoid))
                    p.Add(New SqlParameter("@valorid", valorid))
                    mensaje = ObjData.RunSQLScalarQueryString("pProductoCombinacion", 1, p)
                Next

                If mensaje.Length > 0 Then
                    RadAlert.RadAlert(mensaje, 330, 180, "Alerta", "", "")
                End If

                panelRegistroCombinacion.Visible = False

                combinacionesList.DataSource = ObjData.FillDataSet("EXEC pProductoCombinacion @cmd=1, @productoid='" & ProductID.Value.ToString & "'")
                combinacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
                combinacionesList.DataBind()
                ObjData = Nothing
            End If

        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub

    Private Sub atributoid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles atributoid.SelectedIndexChanged

        If atributoid.SelectedValue > 0 Then
            valorid.Enabled = True
            Dim objCat As New DataControl(1)
            objCat.Catalogo(valorid, "select id, nombre from tblValorAtributo where atributoid='" & atributoid.SelectedValue.ToString & "' and isnull(borradobit,0)=0 order by nombre", 0)
            objCat = Nothing
        Else
            valorid.SelectedValue = 0
            valorid.Enabled = False
        End If

    End Sub

    Private Sub btnAgregarValorAtributo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarValorAtributo.Click

        If Session("dtAtributoValor").Rows.Count = 0 Then
            btnGuardarCombinacion.Enabled = True
            Dim dr As DataRow = Session("dtAtributoValor").NewRow()
            dr.Item("atributoid") = atributoid.SelectedValue
            dr.Item("valorid") = valorid.SelectedValue
            Session("dtAtributoValor").Rows.Add(dr)
            txtCombinacion.Text = atributoid.SelectedItem.Text.ToString & " : " & valorid.SelectedItem.Text.ToString
        Else
            Dim foundRows() As Data.DataRow
            foundRows = Session("dtAtributoValor").Select("atributoid=" & atributoid.SelectedValue.ToString)

            If foundRows IsNot Nothing Then
                If foundRows.Length = 0 Then
                    Try
                        Dim combinacion1 As String = ""
                        Dim combinacion2 As String = ""
                        Dim dr As DataRow = Session("dtAtributoValor").NewRow()
                        dr.Item("atributoid") = atributoid.SelectedValue
                        dr.Item("valorid") = valorid.SelectedValue
                        Session("dtAtributoValor").Rows.Add(dr)
                        combinacion1 = txtCombinacion.Text.ToString
                        combinacion2 = atributoid.SelectedItem.Text.ToString & " : " & valorid.SelectedItem.Text.ToString
                        txtCombinacion.Text = combinacion1 & vbCrLf & combinacion2
                    Catch ex As Exception
                    End Try
                Else
                    RadAlert.RadAlert("El valor para este atributo ya fue definido.", 330, 180, "Alerta", "", "")
                End If
            End If
        End If

        atributoid.SelectedValue = 0
        valorid.SelectedValue = 0

    End Sub

    Private Sub CrearTablaTemp()
        Dim dtValorAtributo As New DataTable
        dtValorAtributo.Columns.Add("id")
        dtValorAtributo.Columns.Add("atributoid")
        dtValorAtributo.Columns.Add("valorid")
        dtValorAtributo.Columns(0).AutoIncrement = True
        Session("dtAtributoValor") = dtValorAtributo
    End Sub

    Private Sub btnCancelarCombinacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarCombinacion.Click
        Session("dtAtributoValor").Rows.Clear()
        txtCombinacion.Text = ""
        atributoid.SelectedValue = 0
        valorid.SelectedValue = 0
        btnGuardarCombinacion.Enabled = False
        panelRegistroCombinacion.Visible = False
        panelProductRegistration.Visible = False
    End Sub

    Private Sub ExistenciasCombinacionesList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles ExistenciasCombinacionesList.NeedDataSource
        Dim ObjData As New DataControl(1)
        ExistenciasCombinacionesList.DataSource = ObjData.FillDataSet("EXEC pProductoAlmacen @cmd=8, @productoid='" & ProductID.Value.ToString & "', @almacenid='" & cmbSucursal.SelectedValue.ToString & "'")
        ExistenciasCombinacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        ObjData = Nothing
    End Sub

    Private Sub btnActualizarPrecios_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualizarPrecios.Click
        For Each dataItem As Telerik.Web.UI.GridDataItem In combinacionesList.MasterTableView.Items
            Dim txtCUnitario As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCUnitario"), RadNumericTextBox)
            Dim txtUnitario As RadNumericTextBox = DirectCast(dataItem.FindControl("txtUnitario"), RadNumericTextBox)
            Dim txtOrden As RadNumericTextBox = DirectCast(dataItem.FindControl("txtOrden"), RadNumericTextBox)
            Dim folio As Integer = dataItem.GetDataKeyValue("id")

            Dim costo As Decimal = 0
            Dim precio As Decimal = 0
            Dim orden As Decimal = 0

            Try
                costo = Convert.ToDecimal(txtCUnitario.Text)
            Catch ex As Exception
                costo = 0
            End Try

            Try
                precio = Convert.ToDecimal(txtUnitario.Text)
            Catch ex As Exception
                precio = 0
            End Try

            Try
                orden = Convert.ToDecimal(txtOrden.Text)
            Catch ex As Exception
                orden = 0
            End Try

            Dim ObjData As New DataControl(1)
            Dim p As New ArrayList
            p.Clear()
            p.Add(New SqlParameter("@cmd", 8))
            p.Add(New SqlParameter("@id", folio))
            p.Add(New SqlParameter("@costo", costo))
            p.Add(New SqlParameter("@precio", precio))
            p.Add(New SqlParameter("@orden", orden))
            ObjData.ExecuteSP("pProductoCombinacion", 1, p)
        Next

        RadAlert.RadAlert("Precios y orden actualizados", 330, 180, "Alerta", "", "")

    End Sub

    Private Sub btnConsultarExistencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultarExistencia.Click
        Dim ObjData As New DataControl(1)
        ExistenciasCombinacionesList.DataSource = ObjData.FillDataSet("EXEC pProductoAlmacen @cmd=8, @productoid='" & ProductID.Value.ToString & "', @almacenid='" & cmbSucursal.SelectedValue.ToString & "'")
        ExistenciasCombinacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        ExistenciasCombinacionesList.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub btnAgregarPresentacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarPresentacion.Click
        txtCodigoPresentacion.Text = ""
        txtCodigoBarrasPresentacion.Text = ""
        txtDescripcionPresentacion.Text = ""
        txtCostoPresentacion.Text = 0
        txtPrecioPresentacion.Text = 0
        txtPrecioPresentacionFueraHorario.Text = 0
        txtFactorPresentacion.Text = 0
        unidadid.SelectedValue = 0
        chkPredeterminadoPresentacion.Checked = False
        chkPredeterminadoPresentacionAlmacen.Checked = False
        panelRegistroPresentacion.Visible = True
        PresentacionID.Value = 0
    End Sub

    Private Sub btnGuardarPresentacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarPresentacion.Click
        Dim ObjData As New DataControl(1)
        Dim mensaje As String = ""
        Dim predeterminadoBit As Integer = 0
        Dim predeterminadoAlmacenBit As Integer = 0

        If chkPredeterminadoPresentacion.Checked = True Then
            predeterminadoBit = 1
        Else
            predeterminadoBit = 0
        End If

        If chkPredeterminadoPresentacionAlmacen.Checked = True Then
            predeterminadoAlmacenBit = 1
        Else
            predeterminadoAlmacenBit = 0
        End If

        If PresentacionID.Value = 0 Then
            mensaje = ObjData.RunSQLScalarQueryString("EXEC pPresentacionesProducto @cmd=3, @productoid='" & ProductID.Value.ToString & "', @codigo='" & txtCodigoPresentacion.Text & "', @codigo_barras='" & txtCodigoBarrasPresentacion.Text & "', @unidadid='" & unidadid.SelectedValue.ToString & "', @descripcion='" & txtDescripcionPresentacion.Text & "', @factor='" & txtFactorPresentacion.Text & "', @predeterminado_inventario='" & predeterminadoBit.ToString & "', @costo_estandar='" & txtCostoPresentacion.Text & "', @precio='" & txtPrecioPresentacion.Text & "', @precio_fuera_horario='" & txtPrecioPresentacionFueraHorario.Text & "', @predeterminadoAlmacenBit='" & predeterminadoAlmacenBit.ToString & "'")
        Else
            mensaje = ObjData.RunSQLScalarQueryString("EXEC pPresentacionesProducto @cmd=5, @id='" & PresentacionID.Value.ToString & "', @productoid='" & ProductID.Value.ToString & "', @codigo='" & txtCodigoPresentacion.Text & "', @codigo_barras='" & txtCodigoBarrasPresentacion.Text & "', @unidadid='" & unidadid.SelectedValue.ToString & "', @descripcion='" & txtDescripcionPresentacion.Text & "', @factor='" & txtFactorPresentacion.Text & "', @predeterminado_inventario='" & predeterminadoBit.ToString & "', @costo_estandar='" & txtCostoPresentacion.Text & "', @precio='" & txtPrecioPresentacion.Text & "', @precio_fuera_horario='" & txtPrecioPresentacionFueraHorario.Text & "', @predeterminadoAlmacenBit='" & predeterminadoAlmacenBit.ToString & "'")
        End If
        ObjData = Nothing

        If mensaje = "" Then
            RadTabStrip1.SelectedIndex = 0
            Call EditProduct(ProductID.Value)
            Call ObtenerPresentaciones()
            LimpiarPresentacion()
        Else
            lblMensajePresentacion.Text = mensaje
        End If
    End Sub

    Private Sub btnCancelarPresentacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarPresentacion.Click
        Call LimpiarPresentacion()
    End Sub

    Private Sub ObtenerPresentaciones()
        Dim ObjData As New DataControl(1)
        presentacioneslist.DataSource = ObjData.FillDataSet("EXEC pPresentacionesProducto @cmd=1, @productoid='" & ProductID.Value.ToString & "'")
        presentacioneslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        presentacioneslist.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub LimpiarPresentacion()
        txtCodigoPresentacion.Text = ""
        txtCodigoBarrasPresentacion.Text = ""
        txtDescripcionPresentacion.Text = ""
        txtCostoPresentacion.Text = 0
        txtPrecioPresentacion.Text = 0
        txtPrecioPresentacionFueraHorario.Text = 0
        txtFactorPresentacion.Text = 0
        unidadid.SelectedValue = 0
        chkPredeterminadoPresentacion.Checked = False
        panelRegistroPresentacion.Visible = False
        PresentacionID.Value = 0
    End Sub

    Private Sub presentacioneslist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles presentacioneslist.ItemCommand
        Select Case e.CommandName

            Case "cmdEdit"
                EditaPresentacion(e.CommandArgument)
            Case "cmdDelete"
                EliminaPresentacion(e.CommandArgument)

        End Select
    End Sub

    Private Sub presentacioneslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles presentacioneslist.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
                btnDelete.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea eliminar esta presentación de la base de datos?');")
        End Select
    End Sub

    Private Sub presentacioneslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles presentacioneslist.NeedDataSource
        Dim ObjData As New DataControl(1)
        presentacioneslist.DataSource = ObjData.FillDataSet("EXEC pPresentacionesProducto @cmd=1, @productoid='" & ProductID.Value.ToString & "'")
        presentacioneslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        ObjData = Nothing
    End Sub

    Private Sub EditaPresentacion(ByVal id As Integer)

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)

        Try

            Dim cmd As New SqlCommand("EXEC pPresentacionesProducto @cmd=4, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                txtCodigoPresentacion.Text = rs("codigo")
                txtCodigoBarrasPresentacion.Text = rs("codigo_barras")
                txtDescripcionPresentacion.Text = rs("descripcion")
                txtFactorPresentacion.Text = rs("factor")
                chkPredeterminadoPresentacion.Checked = rs("predeterminado_inventario")
                txtCostoPresentacion.Text = rs("costo_estandar")
                txtPrecioPresentacion.Text = rs("precio")
                txtPrecioPresentacionFueraHorario.Text = rs("precio_fuera_horario")
                chkPredeterminadoPresentacionAlmacen.Checked = rs("predeterminadoAlmacenBit")

                Dim objCat As New DataControl(1)
                objCat.Catalogo(unidadid, "select id, descripcion from tblUnidadMedida order by descripcion", rs("unidadid"))
                objCat = Nothing

                panelRegistroPresentacion.Visible = True
                PresentacionID.Value = id

            End If

        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub EliminaPresentacion(ByVal id As Integer)

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)

        Try

            Dim cmd As New SqlCommand("EXEC pPresentacionesProducto @cmd=2, @id ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistroPresentacion.Visible = False

            Call ObtenerPresentaciones()

        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub btnAgregaMargenUtilidad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregaMargenUtilidad.Click
        txtMinimoMargenUtilidad.Text = ""
        txtMaximoMargenUtilidad.Text = ""
        txtPorcentajeMargenUtilidad.Text = 0
        MargenID.Value = 0
        panelMargenUtilidad.Visible = True
    End Sub

    Private Sub btnGuardarMargenUtilidad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarMargenUtilidad.Click
        Dim ObjData As New DataControl(1)
        If MargenID.Value = 0 Then
            ObjData.RunSQLScalarQueryString("EXEC pMargenUtilidad @cmd=3, @productoid='" & ProductID.Value.ToString & "', @minimo='" & txtMinimoMargenUtilidad.Text.ToString & "', @maximo='" & txtMaximoMargenUtilidad.Text.ToString & "', @porcentaje_utilidad='" & txtPorcentajeMargenUtilidad.Text.ToString & "'")
        Else
            ObjData.RunSQLScalarQueryString("EXEC pMargenUtilidad @cmd=5, @id='" & MargenID.Value.ToString & "', @productoid='" & ProductID.Value.ToString & "', @minimo='" & txtMinimoMargenUtilidad.Text.ToString & "', @maximo='" & txtMaximoMargenUtilidad.Text.ToString & "', @porcentaje_utilidad='" & txtPorcentajeMargenUtilidad.Text.ToString & "'")
        End If
        Call ObtenerMargenesUtilidad()
        txtMinimoMargenUtilidad.Text = ""
        txtMaximoMargenUtilidad.Text = ""
        txtPorcentajeMargenUtilidad.Text = 0
        MargenID.Value = 0
        panelMargenUtilidad.Visible = False
        ObjData = Nothing
    End Sub

    Private Sub btnCancelarMargenUtilidad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarMargenUtilidad.Click
        Call LimpiarMargenUtilidad()
    End Sub

    Private Sub ObtenerMargenesUtilidad()
        Dim ObjData As New DataControl(1)
        grdMargenUtilidad.DataSource = ObjData.FillDataSet("EXEC pMargenUtilidad @cmd=1, @productoid='" & ProductID.Value.ToString & "'")
        grdMargenUtilidad.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        grdMargenUtilidad.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub LimpiarMargenUtilidad()
        txtMinimoMargenUtilidad.Text = ""
        txtMaximoMargenUtilidad.Text = ""
        txtPorcentajeMargenUtilidad.Text = 0
        MargenID.Value = 0
        panelMargenUtilidad.Visible = False
        panelProductRegistration.Visible = False
    End Sub

    Private Sub grdMargenUtilidad_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdMargenUtilidad.ItemCommand
        Select Case e.CommandName

            Case "cmdEdit"
                EditaMargenUtilidad(e.CommandArgument)
            Case "cmdDelete"
                EliminaMargenUtilidad(e.CommandArgument)

        End Select
    End Sub

    Private Sub EditaMargenUtilidad(ByVal id As Integer)

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)

        Try

            Dim cmd As New SqlCommand("EXEC pMargenUtilidad @cmd=4, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                txtMinimoMargenUtilidad.Text = rs("minimo")
                txtMaximoMargenUtilidad.Text = rs("maximo")
                txtPorcentajeMargenUtilidad.Text = rs("porcentaje_utilidad")

                panelMargenUtilidad.Visible = True
                MargenID.Value = id

            End If

        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub EliminaMargenUtilidad(ByVal id As Integer)

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)

        Try

            Dim cmd As New SqlCommand("EXEC pMargenUtilidad @cmd=2, @id ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistroPresentacion.Visible = False

            Call ObtenerMargenesUtilidad()

        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub ObtenerHorarios()
        Dim ObjData As New DataControl(1)
        rghorario.DataSource = ObjData.FillDataSet("EXEC pMisProductos @cmd=8, @productoid='" & ProductID.Value.ToString & "'")
        rghorario.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        rghorario.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub btnGuardarHorario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarHorario.Click

        Dim PrecioAlterno As Decimal = 0

        Try
            PrecioAlterno = Convert.ToDecimal(txtPrecioAlterno.Text)
        Catch ex As Exception
            PrecioAlterno = 0
        End Try

        Dim ObjData As New DataControl(1)

        If PrecioAlterno > 0 Then

            Dim p As New ArrayList
            p.Clear()
            p.Add(New SqlParameter("@cmd", 11))
            p.Add(New SqlParameter("@productoid", ProductID.Value))
            p.Add(New SqlParameter("@precioalterno", PrecioAlterno))
            ObjData.ExecuteSP("pMisProductos", 1, p)

            ObjData.RunSQLQuery("EXEC pMisProductos @cmd=10, @diaid='" & cmbDia.SelectedValue & "', @horarioinicial='" & RadHoraInicio.SelectedTime.ToString & "', @horariofinal='" & RadHoraFin.SelectedTime.ToString & "', @productoid='" & ProductID.Value & "'")

            cmbDia.SelectedValue = 0
            RadHoraInicio.Clear()
            RadHoraFin.Clear()

            RadAlert.RadAlert("Horario agregado.", 330, 180, "Alerta", "", "")

        Else
            RadAlert.RadAlert("Proporcione un precio alterno para el producto.", 330, 180, "Alerta", "", "")
        End If
        ObjData = Nothing

        Call ObtenerHorarios()

    End Sub

    Private Sub rghorario_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rghorario.ItemCreated
        Select Case e.Item.ItemType
            Case GridItemType.Item, GridItemType.AlternatingItem
                Dim horainicial As Telerik.Web.UI.RadTimePicker = DirectCast(e.Item.FindControl("rtphorainicial"), Telerik.Web.UI.RadTimePicker)
                horainicial.TimeView.TimeFormat = "HH:mm:ss"

                Dim horafinal As Telerik.Web.UI.RadTimePicker = DirectCast(e.Item.FindControl("rtphorafinal"), Telerik.Web.UI.RadTimePicker)
                horafinal.TimeView.TimeFormat = "HH:mm:ss"
        End Select
    End Sub

    Private Sub rghorario_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rghorario.NeedDataSource
        Dim ObjData As New DataControl(1)
        rghorario.DataSource = ObjData.FillDataSet("EXEC pMisProductos @cmd=8, @productoid='" & ProductID.Value.ToString & "'")
        rghorario.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        ObjData = Nothing
    End Sub

    Private Sub rghorario_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rghorario.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                DeleteHorario(e.CommandArgument)
        End Select
    End Sub

    Private Sub DeleteHorario(ByVal id As Integer)

        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("EXEC pMisProductos @cmd=15, @horarioid='" & id.ToString & "'")
        ObjData = Nothing

        rghorario.DataSource = ObjData.FillDataSet("EXEC pMisProductos @cmd=8, @productoid='" & ProductID.Value.ToString & "'")
        rghorario.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        rghorario.DataBind()

    End Sub

    Private Sub rghorario_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rghorario.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Horario" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('Va a borrar una configuración de horario. ¿Desea continuar?');")

            End If

        End If

    End Sub

    Private Sub CargaFotos()
        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)

        Try
            Dim cmd As New SqlCommand("EXEC pMisProductos @cmd=16, @productoid='" & ProductID.Value.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                If String.IsNullOrEmpty(rs("foto")) = True Then
                    imgProducto.Visible = False
                    hdnFoto.Value = ""
                    imgBtnEliminarAnexo.Visible = False
                Else
                    imgProducto.Visible = True
                    imgBtnEliminarAnexo.Visible = True
                    foto.Visible = False
                    imgProducto.ImageUrl = "~/images/productos/" & rs("foto").ToString
                    hdnFoto.Value = rs("foto")
                End If

                If String.IsNullOrEmpty(rs("foto2")) = True Then
                    imgProducto2.Visible = False
                    hdnFoto2.Value = ""
                    imgBtnEliminarAnexo2.Visible = False
                Else
                    imgProducto2.Visible = True
                    imgBtnEliminarAnexo2.Visible = True
                    foto2.Visible = False
                    imgProducto2.ImageUrl = "~/images/productos/" & rs("foto2").ToString
                    hdnFoto2.Value = rs("foto2")
                End If

            End If
        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Private Sub AsignarPuntoReorden()
        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("EXEC pMisProductos @cmd=21, @productoid='" & ProductID.Value.ToString & "'")
        ObjData = Nothing
    End Sub

    Private Sub btnGuardarFoto_Click(sender As Object, e As EventArgs) Handles btnGuardarFoto.Click
        Try
            Dim ObjData As New DataControl(1)
            ObjData.RunSQLQuery("exec pMisProductos @cmd=17, @foto='" & actualizaFoto.ToString & "', @foto2='" & actualizaFoto2.ToString & "', @productoid='" & ProductID.Value.ToString & "'")
            ObjData = Nothing

            Call CargaFotos()

        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub

    Private Function actualizaFoto() As String
        '
        '   Actualiza o Agregar Foto
        '
        Dim archivo As String = ""
        Dim i As Integer = 0

        If hdnFoto.Value.ToString = "" Then
            If foto.PostedFile.ContentLength > 0 Then
                archivo = foto.PostedFile.FileName.Substring(foto.PostedFile.FileName.LastIndexOf("\") + 1)
                If File.Exists(Server.MapPath("~/images/productos/") + archivo) Then
                    For i = 1 To 99
                        archivo = i.ToString + "_" + foto.PostedFile.FileName.Substring(foto.PostedFile.FileName.LastIndexOf("\") + 1)
                        If Not File.Exists(Server.MapPath("~/images/productos/") + archivo) Then
                            foto.PostedFile.SaveAs(Server.MapPath("~/images/productos/") + archivo)
                            Exit For
                        End If
                    Next
                Else
                    foto.PostedFile.SaveAs(Server.MapPath("~/images/productos/") + archivo)
                End If

            Else
                archivo = hdnFoto.Value.ToString
            End If
        Else
            archivo = hdnFoto.Value.ToString
        End If

        Return archivo

    End Function

    Private Function actualizaFoto2() As String
        '
        '   Actualiza o Agregar Foto
        '
        Dim archivo As String = ""
        If hdnFoto2.Value.ToString = "" Then
            If foto2.PostedFile.ContentLength > 0 Then
                archivo = foto2.PostedFile.FileName.Substring(foto2.PostedFile.FileName.LastIndexOf("\") + 1)
                If File.Exists(Server.MapPath("~/images/productos/") + archivo) Then
                    For i = 1 To 99
                        archivo = i.ToString + "_" + foto2.PostedFile.FileName.Substring(foto2.PostedFile.FileName.LastIndexOf("\") + 1)
                        If Not File.Exists(Server.MapPath("~/images/productos/") + archivo) Then
                            foto2.PostedFile.SaveAs(Server.MapPath("~/images/productos/") + archivo)
                            Exit For
                        End If
                    Next
                Else
                    foto2.PostedFile.SaveAs(Server.MapPath("~/images/productos/") + archivo)
                End If
            Else
                archivo = hdnFoto2.Value.ToString
            End If
        Else
            archivo = hdnFoto2.Value.ToString
        End If

        Return archivo

    End Function

    Protected Sub imgBtnEliminarAnexo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnEliminarAnexo.Click
        foto.Visible = True
        imgProducto.Visible = False
        imgBtnEliminarAnexo.Visible = False
        hdnFoto.Value = ""
    End Sub

    Protected Sub imgBtnEliminarAnexo2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnEliminarAnexo2.Click
        foto2.Visible = True
        imgProducto2.Visible = False
        imgBtnEliminarAnexo2.Visible = False
        hdnFoto2.Value = ""
    End Sub

    Private Sub btnRegresarListado_Click(sender As Object, e As EventArgs) Handles btnRegresarListado.Click
        Call Cancelar()
        panelProductList.Visible = True
        productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        productslist.DataSource = GetProducts()
        productslist.DataBind()
    End Sub

    Private Sub grdProductosPuntoReorden_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdProductosPuntoReorden.NeedDataSource
        If Not e.IsFromDetailTable Then

            grdProductosPuntoReorden.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
            grdProductosPuntoReorden.DataSource = ProductosPuntoReorden()

        End If
    End Sub

    Function ProductosPuntoReorden() As DataSet
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pMisProductos @cmd=22, @productoid='" & ProductID.Value.ToString & "'", conn)
        Dim ds As DataSet = New DataSet
        Try
            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            RadAlert.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        Return ds
    End Function

    Public Sub txtPuntoReorden_TextChanged(sender As Object, e As EventArgs)
        For Each dataItem As Telerik.Web.UI.GridDataItem In grdProductosPuntoReorden.MasterTableView.Items
            Dim id As String = dataItem.GetDataKeyValue("id").ToString()

            Dim txtPuntoReorden As RadNumericTextBox = DirectCast(dataItem.FindControl("txtPuntoReorden"), RadNumericTextBox)
            Dim punto_reorden As Decimal = 0

            Try
                punto_reorden = Convert.ToDecimal(txtPuntoReorden.Text)
            Catch ex As Exception
                punto_reorden = 0
            End Try

            Dim objData As New DataControl(1)
            objData.FillDataSet("EXEC pMisProductos @cmd=23, @punto_reorden='" & punto_reorden.ToString & "', @puntoreordenid='" & id.ToString & "'")
            objData = Nothing

        Next

        grdProductosPuntoReorden.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        grdProductosPuntoReorden.DataSource = ProductosPuntoReorden()
        grdProductosPuntoReorden.DataBind()

    End Sub

End Class