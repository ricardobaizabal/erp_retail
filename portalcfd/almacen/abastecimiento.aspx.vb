Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class portalcfd_almacen_abastecimiento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            objCat.Catalogo(filtrosucursalid, "select id, nombre from tblSucursal where isnull(borradoBit,0)=0 order by nombre", 0)
            objCat.Catalogo(filtrofamiliaid, "select id, nombre from tblFamilia where isnull(borradoBit,0)=0 order by nombre", 0, True)
            objCat.Catalogo(filtrosubfamiliaid, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 order by nombre", 0, True)
            objCat = Nothing

            If Session("moduloescolar") Then
                productslist.MasterTableView.GetColumn("existencia_bodega").Visible = False
                productslist.MasterTableView.GetColumn("disponibles").Visible = False
                productslist.MasterTableView.GetColumn("cantidad").Visible = False
            End If
        End If
    End Sub

    Private Sub productslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles productslist.NeedDataSource
        productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        productslist.DataSource = GetProducts()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        productslist.DataSource = GetProducts()
        productslist.DataBind()
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        txtSearch.Text = ""
        filtrofamiliaid.SelectedValue = 0
        filtrosubfamiliaid.SelectedValue = 0
        filtrosubfamiliaid.Enabled = False
        productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        productslist.DataSource = GetProducts()
        productslist.DataBind()
    End Sub

    Function GetProducts() As DataSet
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pInventario @cmd=1, @txtSearch='" & txtSearch.Text & "', @familiaid='" & filtrofamiliaid.SelectedValue.ToString & "', @subfamiliaid='" & filtrosubfamiliaid.SelectedValue.ToString & "', @sucursalid='" & filtrosucursalid.SelectedValue.ToString & "'", conn)
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

        If ds.Tables(0).Rows.Count > 0 Then
            If Session("moduloescolar") Then
                btnAgregarTransferencia.Visible = False
            Else
                btnAgregarTransferencia.Visible = True
            End If
        End If

        Return ds

    End Function

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
        'productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        'productslist.DataSource = GetProducts()
        'productslist.DataBind()
    End Sub

    Private Sub productslist_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles productslist.ItemCommand

    End Sub

    Private Sub productslist_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles productslist.ItemDataBound
        For Each dataItem As Telerik.Web.UI.GridDataItem In productslist.MasterTableView.Items

            Dim id As String = dataItem.GetDataKeyValue("id").ToString()
            Dim existencia As Decimal = dataItem.GetDataKeyValue("existencia")
            Dim existencia_bodega As Decimal = dataItem.GetDataKeyValue("existencia_bodega")
            Dim punto_reorden As Decimal = dataItem.GetDataKeyValue("punto_reorden")

            Dim txtCantidad As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCantidad"), RadNumericTextBox)

            If existencia_bodega = 0 Then
                txtCantidad.Enabled = False
            Else
                If existencia_bodega >= punto_reorden Then
                    txtCantidad.Text = punto_reorden
                Else
                    txtCantidad.Text = existencia_bodega
                End If
            End If

        Next
    End Sub

    Private Sub btnAgregarTransferencia_Click(sender As Object, e As EventArgs) Handles btnAgregarTransferencia.Click
        Dim cantidad_total As Decimal = 0
        For Each dataItem As Telerik.Web.UI.GridDataItem In productslist.MasterTableView.Items

            ID = dataItem.GetDataKeyValue("id")
            Dim cantidad As Decimal = 0

            Dim txtCantidad As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCantidad"), RadNumericTextBox)

            Try
                cantidad = Convert.ToDecimal(txtCantidad.Text)
            Catch ex As Exception
                cantidad = 0
            End Try

            cantidad_total = cantidad_total + cantidad

        Next
        If cantidad_total > 0 Then
            rwConfirm.RadConfirm("Va a generar un nuevo lote de transferencia con el origen: Bodega. ¿Desea continuar?", "confirmCallbackFinalizarTransferencia", 330, 180, Nothing, "Confirmar")
        Else
            rwAlerta.RadAlert("Debes proporcionar las cantidades para transferir.", 330, 180, "Alerta", "", "")
        End If
    End Sub

    Private Sub btnFinalizarTransferencia_Click(sender As Object, e As EventArgs) Handles btnFinalizarTransferencia.Click
        Dim transferenciaid As Long = 0
        Dim id As Long = 0
        Dim presentacionid As Integer = 0
        Dim disponibles As Decimal = 0
        Dim factor As Decimal = 0
        Dim cantidad_total As Decimal = 0
        Dim ObjData As New DataControl(1)

        For Each dataItem As Telerik.Web.UI.GridDataItem In productslist.MasterTableView.Items

            id = dataItem.GetDataKeyValue("id")
            Dim cantidad As Decimal = 0

            Dim txtCantidad As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCantidad"), RadNumericTextBox)

            Try
                cantidad = Convert.ToDecimal(txtCantidad.Text)
            Catch ex As Exception
                cantidad = 0
            End Try

            cantidad_total = cantidad_total + cantidad

        Next

        If cantidad_total > 0 Then
            Dim dtToday As String
            dtToday = Format(Date.Today, "dd/MM/yyyy")
            transferenciaid = ObjData.RunSQLScalarQuery("exec pTransferencia @cmd=1, @userid='" & Session("userid").ToString & "', @origenid='44', @destinoid='" & filtrosucursalid.SelectedValue.ToString & "', @comentario='Transferencia por abastecimiento " & dtToday & "', @autorizadaBit=1")

            For Each row As GridDataItem In productslist.MasterTableView.Items

                id = row.GetDataKeyValue("id")
                presentacionid = row.GetDataKeyValue("presentacionid")
                disponibles = row.GetDataKeyValue("disponibles")
                factor = row.GetDataKeyValue("factor")

                Dim txtCantidad As RadNumericTextBox = DirectCast(row.FindControl("txtCantidad"), RadNumericTextBox)
                Dim cantidad As Decimal = 0

                Try
                    cantidad = Convert.ToDecimal(txtCantidad.Text)
                Catch ex As Exception
                    cantidad = 0
                End Try

                If cantidad > 0 Then
                    If presentacionid > 0 Then
                        If cantidad <= disponibles Then
                            ObjData.RunSQLQuery("exec pTransferencia @cmd=3, @transferenciaid='" & transferenciaid.ToString & "', @cantidad='" & cantidad.ToString & "', @productoid=0, @presentacionid='" & presentacionid.ToString & "', @factor='" & factor.ToString & "'")
                        End If
                    Else
                        ObjData.RunSQLQuery("exec pTransferencia @cmd=3, @transferenciaid='" & transferenciaid.ToString & "', @cantidad='" & cantidad.ToString & "', @productoid='" & id.ToString & "', @presentacionid=0, @factor='" & factor.ToString & "'")
                    End If
                End If
            Next
            ObjData.RunSQLQuery("exec pTransferencia @cmd=8, @transferenciaid='" & transferenciaid.ToString & "'")
            ObjData = Nothing

            Response.Redirect("~/portalcfd/almacen/transferencias.aspx")

        Else
            rwAlerta.RadAlert("Debes proporcionar las cantidades para transferir.", 330, 180, "Alerta", "", "")
        End If
    End Sub

End Class