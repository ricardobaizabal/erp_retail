Public Partial Class agregapedido
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbCliente, "EXEC pMisClientes @cmd=1", 0)
            objCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 6)
            objCat = Nothing
        End If

    End Sub

    Private Sub btnCrearPedido_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCrearPedido.Click
        Try
            Dim pedidoid As Long
            Dim ObjData As New DataControl(1)
            pedidoid = ObjData.RunSQLScalarQuery("EXEC pPedido @cmd=1, @userid='" & Session("userid").ToString & "', @clienteid='" & cmbCliente.SelectedValue.ToString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @orden_compra='" & txtOrdenCompra.Text.ToString & "'")
            ObjData = Nothing

            Response.Redirect("~/portalcfd/editapedido.aspx?id=" & pedidoid.ToString)

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("~/portalcfd/pedidos.aspx")
    End Sub

End Class