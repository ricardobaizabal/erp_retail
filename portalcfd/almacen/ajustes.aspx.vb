Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI
Partial Class portalcfd_almacen_ajustes
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fechaini.SelectedDate = Date.Now
            fechafin.SelectedDate = Date.Now

            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbSucursalFiltro, "select id, nombre from tblSucursal order by nombre", 0, True)
            objCat = Nothing

        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        gridResults.Visible = True
        gridResults.DataSource = GetProducts()
        gridResults.DataBind()
    End Sub

    Function GetProducts() As DataSet
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pProductoAlmacen @cmd=6, @txtSearch='" & txtSearch.Text & "'", conn)
        Dim ds As DataSet = New DataSet
        Try
            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        Return ds
    End Function

    Protected Sub gridResults_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gridResults.ItemCommand
        Select Case e.CommandName
            Case "cmdAdd"
                Call InsertItem(e.CommandArgument, e.Item)
            Case "cmdCombinaciones"
                ProductID.Value = e.CommandArgument
                panelCombinaciones.Visible = True
                Dim ObjData As New DataControl(1)
                ds = ObjData.FillDataSet("exec pProductoAlmacen @cmd=1, @productoid='" & e.CommandArgument.ToString & "'")
                ProductoCombinacionesList.DataSource = ds
                ProductoCombinacionesList.DataBind()
                ObjData = Nothing
        End Select
    End Sub

    Protected Sub gridResults_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gridResults.ItemDataBound
        Select Case e.Item.ItemType
            Case GridItemType.Item, GridItemType.AlternatingItem
                Dim txtCantidad As RadNumericTextBox = DirectCast(e.Item.FindControl("txtCantidad"), RadNumericTextBox)
                Dim txtCostoUnitario As RadNumericTextBox = DirectCast(e.Item.FindControl("txtCostoUnitario"), RadNumericTextBox)
                Dim txtImporte As RadNumericTextBox = DirectCast(e.Item.FindControl("txtImporte"), RadNumericTextBox)

                'Dim txtDocumento As System.Web.UI.WebControls.TextBox = DirectCast(e.Item.FindControl("txtDocumento"), System.Web.UI.WebControls.TextBox)
                Dim txtComentario As System.Web.UI.WebControls.TextBox = DirectCast(e.Item.FindControl("txtComentario"), System.Web.UI.WebControls.TextBox)
                Dim btnAdd As System.Web.UI.WebControls.ImageButton = DirectCast(e.Item.FindControl("btnAdd"), System.Web.UI.WebControls.ImageButton)
                Dim lnkCombinaciones As System.Web.UI.WebControls.LinkButton = DirectCast(e.Item.FindControl("lnkCombinaciones"), System.Web.UI.WebControls.LinkButton)
                Dim cmbSucursal As System.Web.UI.WebControls.DropDownList = DirectCast(e.Item.FindControl("cmbSucursal"), System.Web.UI.WebControls.DropDownList)

                txtCantidad.Text = "1"
                txtCostoUnitario.Text = e.Item.DataItem("costo_estandar")

                Dim objCat As New DataControl(1)
                objCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0)
                objCat = Nothing

                If e.Item.DataItem("tipo") = 2 Then
                    txtCantidad.Visible = False
                    txtCostoUnitario.Visible = False
                    txtImporte.Visible = False
                    cmbSucursal.Visible = False
                    'txtDocumento.Visible = False
                    txtComentario.Visible = False
                    btnAdd.Visible = False
                Else
                    lnkCombinaciones.Visible = False
                End If
        End Select
    End Sub

    Protected Sub gridResults_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gridResults.NeedDataSource
        gridResults.Visible = True
        gridResults.DataSource = GetProducts()
    End Sub

    Private Sub InsertItem(ByVal id As Long, ByVal item As GridItem)
        '
        '   Instancia elementos
        '
        Dim lblCodigo As Label = DirectCast(item.FindControl("lblCodigo"), Label)
        Dim lblDescripcion As Label = DirectCast(item.FindControl("lblDescripcion"), Label)
        Dim lblExistencia As Label = DirectCast(item.FindControl("lblExistencia"), Label)
        Dim txtCantidad As RadNumericTextBox = DirectCast(item.FindControl("txtCantidad"), RadNumericTextBox)
        Dim txtCostoUnitario As RadNumericTextBox = DirectCast(item.FindControl("txtCostoUnitario"), RadNumericTextBox)
        Dim txtComentario As TextBox = DirectCast(item.FindControl("txtComentario"), TextBox)
        Dim cmbSucursal As System.Web.UI.WebControls.DropDownList = DirectCast(item.FindControl("cmbSucursal"), System.Web.UI.WebControls.DropDownList)
        '
        '   Agrega entrada
        '
        If cmbSucursal.SelectedValue > 0 Then
            If lblExistencia.Text > 0 Then
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pProductoAlmacen @cmd=11, @productoid='" & id.ToString & "', @almacenid='" & cmbSucursal.SelectedValue & "', @cantidad='" & txtCantidad.Text & "', @precio='" & txtCostoUnitario.Text & "'")
                ObjData.RunSQLQuery("exec pProductoAlmacen @cmd=12, @productoid='" & id.ToString & "', @codigo='" & lblCodigo.Text & "', @descripcion='" & lblDescripcion.Text & "', @cantidad='" & txtCantidad.Text & "', @costo_unitario='" & txtCostoUnitario.Text & "', @userid='" & Session("userid").ToString & "', @comentario='" & txtComentario.Text & "', @almacenid='" & cmbSucursal.SelectedValue & "'")
                ObjData = Nothing
            Else
                RadAlert.RadAlert("No tines existencia para este producto en la sucursal seleccionada", 330, 180, "Alerta", "", "")
            End If
        Else
            RadAlert.RadAlert("Favor de seleccionar una sucursal", 330, 180, "Alerta", "", "")
        End If
        '
        panelCombinaciones.Visible = False
        txtSearch.Text = ""
        txtSearch.Focus()
        '
        gridResults.Visible = False
        Call MuestraUltimosMovimientos()
        '
    End Sub

    Private Sub InsertItemCombinacion(ByVal combinacionid As Long, ByVal productoid As Long, ByVal item As GridItem)
        '
        '   Instancia elementos
        '
        Dim lblCodigo As Label = DirectCast(item.FindControl("lblCodigo"), Label)
        Dim lblDescripcion As Label = DirectCast(item.FindControl("lblDescripcion"), Label)
        Dim lblProducto As Label = DirectCast(item.FindControl("lblProducto"), Label)
        Dim txtCantidad As RadNumericTextBox = DirectCast(item.FindControl("txtCantidad"), RadNumericTextBox)
        Dim txtCostoUnitario As RadNumericTextBox = DirectCast(item.FindControl("txtCostoUnitario"), RadNumericTextBox)
        Dim lblExistencia As Label = DirectCast(item.FindControl("lblExistencia"), Label)
        Dim txtComentario As TextBox = DirectCast(item.FindControl("txtComentario"), TextBox)
        Dim cmbSucursal As System.Web.UI.WebControls.DropDownList = DirectCast(item.FindControl("cmbSucursal"), System.Web.UI.WebControls.DropDownList)
        '
        '   Agrega entrada
        '
        If cmbSucursal.SelectedValue > 0 Then
            If lblExistencia.Text > 0 Then
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pProductoAlmacen @cmd=11, @productoid='" & productoid.ToString & "', @combinacionid='" & combinacionid.ToString & "', @almacenid='" & cmbSucursal.SelectedValue & "', @cantidad='" & txtCantidad.Text & "', @precio='" & txtCostoUnitario.Text & "'")
                ObjData.RunSQLQuery("exec pProductoAlmacen @cmd=12, @productoid='" & productoid.ToString & "', @combinacionid='" & combinacionid.ToString & "', @codigo='" & lblCodigo.Text & "', @descripcion='" & lblProducto.Text & " " & lblDescripcion.Text & "', @cantidad='" & txtCantidad.Text & "', @costo_unitario='" & txtCostoUnitario.Text & "', @userid='" & Session("userid").ToString & "', @comentario='" & txtComentario.Text & "', @almacenid='" & cmbSucursal.SelectedValue & "'")
                ObjData = Nothing
            Else
                RadAlert.RadAlert("No tines existencia para este producto en la sucursal seleccionada", 330, 180, "Alerta", "", "")
            End If
        Else
            RadAlert.RadAlert("Favor de seleccionar una sucursal", 330, 180, "Alerta", "", "")
        End If
        '
        panelCombinaciones.Visible = False
        txtSearch.Text = ""
        txtSearch.Focus()
        '
        gridResults.Visible = False
        Call MuestraUltimosMovimientos()
        ''
    End Sub

    Private Sub MuestraUltimosMovimientos()
        '
        productslist.Rebind()
        productslist.CurrentPageIndex = 0
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pProductoAlmacen @cmd=10, @almacenid='" & cmbSucursalFiltro.SelectedValue.ToString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "'")
        productslist.DataSource = ds
        productslist.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub productslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles productslist.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Compute("sum(importe)", "")) Then
                        e.Item.Cells(6).Text = "TOTAL:"
                        e.Item.Cells(6).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(6).Font.Bold = True

                        e.Item.Cells(7).Text = FormatCurrency(ds.Tables(0).Compute("sum(importe)", ""), 2).ToString
                        e.Item.Cells(7).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(7).Font.Bold = True
                    End If
                End If
        End Select
    End Sub

    Protected Sub productslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles productslist.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pProductoAlmacen @cmd=10, @almacenid='" & cmbSucursalFiltro.SelectedValue.ToString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "'")
        productslist.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Sub txtCantidad_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each dataItem As Telerik.Web.UI.GridDataItem In gridResults.MasterTableView.Items

            Dim txtCantidad As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCantidad"), RadNumericTextBox)
            Dim txtCostoUnitario As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCostoUnitario"), RadNumericTextBox)
            Dim txtImporte As RadNumericTextBox = DirectCast(dataItem.FindControl("txtImporte"), RadNumericTextBox)

            txtImporte.Text = txtCantidad.Text * txtCostoUnitario.Text

        Next
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Call MuestraUltimosMovimientos()
    End Sub

    Private Sub ProductoCombinacionesList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles ProductoCombinacionesList.ItemCommand
        Select Case e.CommandName
            Case "cmdAdd"
                Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {"|"c})

                Call InsertItemCombinacion(commandArgs(0), commandArgs(1), e.Item)
        End Select
    End Sub

    Private Sub ProductoCombinacionesList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles ProductoCombinacionesList.ItemDataBound
        Select Case e.Item.ItemType
            Case GridItemType.Item, GridItemType.AlternatingItem
                Dim txtCantidad As RadNumericTextBox = DirectCast(e.Item.FindControl("txtCantidad"), RadNumericTextBox)
                Dim txtCostoUnitario As RadNumericTextBox = DirectCast(e.Item.FindControl("txtCostoUnitario"), RadNumericTextBox)
                Dim txtImporte As RadNumericTextBox = DirectCast(e.Item.FindControl("txtImporte"), RadNumericTextBox)
                Dim cmbSucursal As System.Web.UI.WebControls.DropDownList = DirectCast(e.Item.FindControl("cmbSucursal"), System.Web.UI.WebControls.DropDownList)

                txtCantidad.Text = "1"
                txtCostoUnitario.Text = e.Item.DataItem("costo_estandar")

                Dim objCat As New DataControl(1)
                objCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0)
                objCat = Nothing

        End Select
    End Sub

    Private Sub ProductoCombinacionesList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles ProductoCombinacionesList.NeedDataSource
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pProductoAlmacen @cmd=1, @productoid='" & ProductID.Value.ToString & "'")
        ProductoCombinacionesList.DataSource = ds
        ObjData = Nothing
    End Sub

    Sub txtCantidadCombinacion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each dataItem As Telerik.Web.UI.GridDataItem In ProductoCombinacionesList.MasterTableView.Items

            Dim txtCantidad As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCantidad"), RadNumericTextBox)
            Dim txtCostoUnitario As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCostoUnitario"), RadNumericTextBox)
            Dim txtImporte As RadNumericTextBox = DirectCast(dataItem.FindControl("txtImporte"), RadNumericTextBox)

            txtImporte.Text = txtCantidad.Text * txtCostoUnitario.Text

        Next
    End Sub
    
    Private Sub btnGuardaAjustes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardaAjustes.Click
        For Each dataItem As Telerik.Web.UI.GridDataItem In ProductoCombinacionesList.MasterTableView.Items
            Dim productoid As Integer = dataItem.GetDataKeyValue("productoid")
            Dim combinacionid As Integer = dataItem.GetDataKeyValue("combinacionid")
            Dim lblCodigo As Label = DirectCast(dataItem.FindControl("lblCodigo"), Label)
            Dim lblDescripcion As Label = DirectCast(dataItem.FindControl("lblDescripcion"), Label)
            Dim lblProducto As Label = DirectCast(dataItem.FindControl("lblProducto"), Label)
            Dim lblExistencia As Label = DirectCast(dataItem.FindControl("lblExistencia"), Label)
            Dim txtCantidad As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCantidad"), RadNumericTextBox)
            Dim txtCostoUnitario As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCostoUnitario"), RadNumericTextBox)

            Dim txtComentario As TextBox = DirectCast(dataItem.FindControl("txtComentario"), TextBox)
            Dim cmbSucursal As System.Web.UI.WebControls.DropDownList = DirectCast(dataItem.FindControl("cmbSucursal"), System.Web.UI.WebControls.DropDownList)
            '
            '   Agrega entrada
            '
            If cmbSucursal.SelectedValue > 0 Then
                If lblExistencia.Text > 0 Then
                    Dim ObjData As New DataControl(1)
                    ObjData.RunSQLQuery("exec pProductoAlmacen @cmd=11, @productoid='" & productoid.ToString & "', @combinacionid='" & combinacionid.ToString & "', @almacenid='" & cmbSucursal.SelectedValue & "', @cantidad='" & txtCantidad.Text & "', @precio='" & txtCostoUnitario.Text & "'")
                    ObjData.RunSQLQuery("exec pProductoAlmacen @cmd=12, @productoid='" & productoid.ToString & "', @combinacionid='" & combinacionid.ToString & "', @codigo='" & lblCodigo.Text & "', @descripcion='" & lblProducto.Text & " " & lblDescripcion.Text & "', @cantidad='" & txtCantidad.Text & "', @costo_unitario='" & txtCostoUnitario.Text & "', @userid='" & Session("userid").ToString & "', @comentario='" & txtComentario.Text & "', @almacenid='" & cmbSucursal.SelectedValue & "'")
                    ObjData = Nothing
                Else
                    RadAlert.RadAlert("No tines existencia para este producto en la sucursal seleccionada", 330, 180, "Alerta", "", "")
                End If
            End If
            '
            gridResults.Visible = False
            Call MuestraUltimosMovimientos()
            '
        Next

        panelCombinaciones.Visible = False
        txtSearch.Text = ""
        txtSearch.Focus()

        RadAlert.RadAlert("Ajuste de almacén exitoso", 330, 180, "Alerta", "", "")
    End Sub

    Sub cmbSucursal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each dataItem As Telerik.Web.UI.GridDataItem In ProductoCombinacionesList.MasterTableView.Items

            Dim cmbSucursal As DropDownList = DirectCast(dataItem.FindControl("cmbSucursal"), DropDownList)
            Dim productoid As Integer = dataItem.GetDataKeyValue("productoid")
            Dim combinacionid As Integer = dataItem.GetDataKeyValue("combinacionid")

            Dim existencia As Decimal = 0
            Dim ObjData As New DataControl(1)
            existencia = ObjData.RunSQLScalarQueryDecimal("exec pProductoAlmacen @cmd=13, @combinacionid='" & combinacionid.ToString & "', @productoid='" & productoid.ToString & "', @almacenid='" & cmbSucursal.SelectedValue.ToString & "'")
            ObjData = Nothing

            Dim lblExistencia As Label = DirectCast(dataItem.FindControl("lblExistencia"), Label)
            lblExistencia.Text = existencia

        Next
    End Sub

    Sub cmbSucursalAlmacen_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each dataItem As Telerik.Web.UI.GridDataItem In gridResults.MasterTableView.Items

            Dim cmbSucursal As DropDownList = DirectCast(dataItem.FindControl("cmbSucursal"), DropDownList)
            Dim productoid As Integer = dataItem.GetDataKeyValue("id")

            Dim existencia As Decimal = 0
            Dim ObjData As New DataControl(1)
            existencia = ObjData.RunSQLScalarQueryDecimal("exec pProductoAlmacen @cmd=13, @productoid='" & productoid.ToString & "', @almacenid='" & cmbSucursal.SelectedValue.ToString & "'")
            ObjData = Nothing

            Dim lblExistencia As Label = DirectCast(dataItem.FindControl("lblExistencia"), Label)
            lblExistencia.Text = existencia

        Next
    End Sub

End Class