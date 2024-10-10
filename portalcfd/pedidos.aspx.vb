Public Partial Class pedidos1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbCliente, "EXEC pMisClientes @cmd=1", 0)
            objCat.Catalogo(cmbPedidoEstatus, "select id, nombre from tblPedidoEstatus order by nombre", 0)
            objCat = Nothing
        End If

    End Sub

    Private Sub pedidosList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles pedidosList.ItemCommand
        Dim ObjData As New DataControl(1)
        Select Case e.CommandName
            Case "cmdEditar"
                Response.Redirect("~/portalcfd/editapedido.aspx?id=" & e.CommandArgument)
            Case "cmdEliminar"
                objdata.RunSQLQuery("exec pPedidos @cmd=4, @pedidoid=" & e.CommandArgument)
                objdata = Nothing
                Call MuestraPedidos()
            Case "cmdFacturar"
                'Call Facturar(e.CommandArgument)
        End Select
    End Sub

    Private Sub MuestraPedidos()
        Dim ObjData As New DataControl(1)
        Dim dsData As New DataSet()
        dsData = ObjData.FillDataSet("exec pPedidos @cmd=2, @userid='" & Session("userid").ToString & "'")
        If dsData.Tables(0).Rows.Count > 0 Then
            pedidosList.DataSource = dsData
            pedidosList.DataBind()
        End If
        ObjData = Nothing
    End Sub

    Private Sub pedidosList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles pedidosList.NeedDataSource
        Dim ObjData As New DataControl(1)
        Dim dsData As New DataSet()
        dsData = ObjData.FillDataSet("exec pPedido @cmd=2, @userid='" & Session("userid").ToString & "', @clienteid='" & cmbCliente.SelectedValue.ToString & "', @estatusid='" & cmbPedidoEstatus.SelectedValue.ToString & "', @txtSearch='" & txtSearch.Text & "'")
        pedidosList.DataSource = dsData
        ObjData = Nothing
    End Sub

    Private Sub btnAgregarPedido_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarPedido.Click
        Response.Redirect("~/portalcfd/agregapedido.aspx")
    End Sub

End Class