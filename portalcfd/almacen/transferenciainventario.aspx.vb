Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI

Partial Class transferenciainventario
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'fechaini.SelectedDate = Date.Now
            'fechafin.SelectedDate = Date.Now
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

                Dim txtDocumento As System.Web.UI.WebControls.TextBox = DirectCast(e.Item.FindControl("txtDocumento"), System.Web.UI.WebControls.TextBox)
                Dim txtComentario As System.Web.UI.WebControls.TextBox = DirectCast(e.Item.FindControl("txtComentario"), System.Web.UI.WebControls.TextBox)
                Dim btnAdd As System.Web.UI.WebControls.ImageButton = DirectCast(e.Item.FindControl("btnAdd"), System.Web.UI.WebControls.ImageButton)
                Dim lnkCombinaciones As System.Web.UI.WebControls.LinkButton = DirectCast(e.Item.FindControl("lnkCombinaciones"), System.Web.UI.WebControls.LinkButton)
                Dim cmbSucursal As System.Web.UI.WebControls.DropDownList = DirectCast(e.Item.FindControl("cmbSucursal"), System.Web.UI.WebControls.DropDownList)

                txtCantidad.Text = "1"
                txtCostoUnitario.Text = e.Item.DataItem("costo_estandar")

                Dim objCat As New DataControl(1)
                objCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal where id <> 4 order by nombre", 0)
                objCat = Nothing


                If e.Item.DataItem("tipo") = 2 Then
                    txtCantidad.Visible = False
                    txtCostoUnitario.Visible = False
                    txtImporte.Visible = False
                    cmbSucursal.Visible = False
                    txtDocumento.Visible = False
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
        Dim txtCantidad As RadNumericTextBox = DirectCast(item.FindControl("txtCantidad"), RadNumericTextBox)
        Dim txtCostoUnitario As RadNumericTextBox = DirectCast(item.FindControl("txtCostoUnitario"), RadNumericTextBox)
        Dim txtDocumento As TextBox = DirectCast(item.FindControl("txtDocumento"), TextBox)
        Dim txtComentario As TextBox = DirectCast(item.FindControl("txtComentario"), TextBox)
        Dim cmbSucursal As System.Web.UI.WebControls.DropDownList = DirectCast(item.FindControl("cmbSucursal"), System.Web.UI.WebControls.DropDownList)
        '
        '   Agrega entrada
        '

        If txtCantidad.Text > 0 Then
            If cmbSucursal.SelectedValue > 0 Then
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pProductoAlmacen @cmd=2, @productoid='" + id.ToString + "', @almacenid='" + cmbSucursal.SelectedValue + "', @cantidad='" + txtCantidad.Text + "', @precio='" + txtCostoUnitario.Text + "'")
                ObjData.RunSQLQuery("exec pProductoAlmacen @cmd=5, @productoid='" + id.ToString + "', @codigo='" + lblCodigo.Text + "', @descripcion='" + lblDescripcion.Text + "', @cantidad='" + txtCantidad.Text + "', @costo_unitario='" + txtCostoUnitario.Text + "', @documento='" + txtDocumento.Text + "', @userid='" + Session("userid").ToString + "', @comentario='" + txtComentario.Text + "', @almacenid='" + cmbSucursal.SelectedValue + "'")
                ObjData = Nothing
            Else
                RadAlert.RadAlert("Eliga una Sucursal", 330, 180, "Alerta", "", "")
            End If

        Else
            RadAlert.RadAlert("Ingrese una Cantidad", 330, 180, "Alerta", "", "")
        End If

        '
        panelCombinaciones.Visible = False
        txtSearch.Text = ""
        txtSearch.Focus()
        '
        gridResults.Visible = False
        ''Call MuestraUltimosMovimientos()
        ''
    End Sub

    Private Sub InsertItemCombinacion(ByVal combinacionid As Long, ByVal productoid As Long, ByVal item As GridItem)
        '
        '   Instancia elementos
        '
        Dim lblCodigo As Label = DirectCast(item.FindControl("lblCodigo"), Label)
        Dim lblDescripcion As Label = DirectCast(item.FindControl("lblDescripcion"), Label)
        Dim lblProducto As Label = DirectCast(item.FindControl("lblProducto"), Label)
        Dim txtCantidad As RadNumericTextBox = DirectCast(item.FindControl("txtCantidad"), RadNumericTextBox)
        Dim txtComentario As TextBox = DirectCast(item.FindControl("txtComentario"), TextBox)
        Dim cmbSucursal As System.Web.UI.WebControls.DropDownList = DirectCast(item.FindControl("cmbSucursal"), System.Web.UI.WebControls.DropDownList)
        Dim cmbSucursalentrega As System.Web.UI.WebControls.DropDownList = DirectCast(item.FindControl("cmbSucursalentrega"), System.Web.UI.WebControls.DropDownList)
        '
        '   Transferir inventario
        '
        If txtCantidad.Text > 0 Then
            If cmbSucursalentrega.SelectedValue > 0 Then
                If cmbSucursalentrega.SelectedValue = cmbSucursal.SelectedValue Then
                    RadAlert.RadAlert("No se puede Tansferir a la misma sucursal", 330, 180, "Alerta", "", "")
                Else

                    Dim ObjData As New DataControl(1)
                    ObjData.RunSQLQuery("exec pInventario @cmd=9, @codigo='" + lblCodigo.Text + "', @sucursal='" + cmbSucursal.SelectedValue + "', @sucursalentrega='" + cmbSucursalentrega.SelectedValue + "', @cantidad='" + txtCantidad.Text + "', @comentario='" + txtComentario.Text + "'")
                    ObjData = Nothing

                End If
            Else
                RadAlert.RadAlert("Eliga una Sucursal", 330, 180, "Alerta", "", "")
            End If
        Else
            RadAlert.RadAlert("Ingrese una Cantidad", 330, 180, "Alerta", "", "")
        End If


        '
        panelCombinaciones.Visible = False
        txtSearch.Text = ""
        txtSearch.Focus()
        '
        gridResults.Visible = True
        'Call MuestraUltimosMovimientos()
        ''
    End Sub

    Sub txtCantidad_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each dataItem As Telerik.Web.UI.GridDataItem In gridResults.MasterTableView.Items

            Dim txtCantidad As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCantidad"), RadNumericTextBox)
            Dim txtCostoUnitario As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCostoUnitario"), RadNumericTextBox)
            Dim txtImporte As RadNumericTextBox = DirectCast(dataItem.FindControl("txtImporte"), RadNumericTextBox)

            txtImporte.Text = txtCantidad.Text * txtCostoUnitario.Text

        Next
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
                'Dim txtCostoUnitario As RadNumericTextBox = DirectCast(e.Item.FindControl("txtCostoUnitario"), RadNumericTextBox)
                Dim txtImporte As RadNumericTextBox = DirectCast(e.Item.FindControl("txtImporte"), RadNumericTextBox)
                Dim cmbSucursal As System.Web.UI.WebControls.DropDownList = DirectCast(e.Item.FindControl("cmbSucursal"), System.Web.UI.WebControls.DropDownList)
                Dim cmbSucursalentrega As System.Web.UI.WebControls.DropDownList = DirectCast(e.Item.FindControl("cmbSucursalentrega"), System.Web.UI.WebControls.DropDownList)


                txtCantidad.Text = "1"
                'txtCostoUnitario.Text = e.Item.DataItem("costo_estandar")

                Dim objCat As New DataControl(1)
                objCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal where id <> 4 order by nombre", 0)
                objCat.Catalogo(cmbSucursalentrega, "select id, nombre from tblSucursal where id <> 4 order by nombre", 0)
                objCat = Nothing
                cmbSucursal.SelectedValue = e.Item.DataItem("sucursalid")
                cmbSucursal.Enabled = False
        End Select
    End Sub

    Private Sub ProductoCombinacionesList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles ProductoCombinacionesList.NeedDataSource
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pProductoAlmacen @cmd=1, @productoid='" & ProductID.Value.ToString & "'")
        ProductoCombinacionesList.DataSource = ds
        ObjData = Nothing
    End Sub

    Sub txtCantidadCombinacion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'For Each dataItem As Telerik.Web.UI.GridDataItem In ProductoCombinacionesList.MasterTableView.Items

        '    Dim txtCantidad As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCantidad"), RadNumericTextBox)
        '    Dim txtCostoUnitario As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCostoUnitario"), RadNumericTextBox)
        '    Dim txtImporte As RadNumericTextBox = DirectCast(dataItem.FindControl("txtImporte"), RadNumericTextBox)

        '    txtImporte.Text = txtCantidad.Text * txtCostoUnitario.Text

        'Next
    End Sub

    Public Sub cargarexistencias()
        For Each dataItem As Telerik.Web.UI.GridDataItem In gridResults.MasterTableView.Items

            Dim codigo As Label = DirectCast(dataItem.FindControl("lblCodigo"), Label)
            Dim cmbSucursal As System.Web.UI.WebControls.DropDownList = DirectCast(dataItem.FindControl("cmbSucursal"), System.Web.UI.WebControls.DropDownList)
            Dim txtexistencia As RadNumericTextBox = DirectCast(dataItem.FindControl("txtexistencia"), RadNumericTextBox)


            Dim conn As New SqlConnection(Session("conexion"))

            Try

                Dim cmd As New SqlCommand("EXEC pInventario @cmd=9, @codigo='" & codigo.Text & "',@sucursal=" & cmbSucursal.SelectedValue, conn)

                conn.Open()

                Dim rs As SqlDataReader
                rs = cmd.ExecuteReader()

                If rs.Read Then
                    txtexistencia.text = rs("existencia")
                End If

            Catch ex As Exception
                Response.Write(ex.Message.ToString())
                Response.End()
            Finally
                conn.Close()
                conn.Dispose()
            End Try


        Next
    End Sub

End Class