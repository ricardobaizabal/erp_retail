Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI

Partial Public Class agregarlote
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle
        If Not IsPostBack Then
            Dim ObjCat As New DataControl(1)
            ObjCat.Catalogo(origenid, "select id, nombre from tblSucursal order by nombre", 0)
            ObjCat.Catalogo(destinoid, "select id, nombre from tblSucursal order by nombre", 0)
            If Session("clienteid") = 3 And Session("perfilid") = 7 Then
                origenid.SelectedValue = 44
                origenid.Enabled = False
                ObjCat.Catalogo(destinoid, "select id, nombre from tblSucursal where id <> " & origenid.SelectedValue.ToString & " order by nombre", 0)
            End If
            ObjCat = Nothing
        End If
    End Sub

    Private Sub origenid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles origenid.SelectedIndexChanged
        If origenid.SelectedValue > 0 Then
            Dim ObjCat As New DataControl(1)
            ObjCat.Catalogo(destinoid, "select id, nombre from tblSucursal where id <> " & origenid.SelectedValue.ToString & " order by nombre", 0)
            ObjCat = Nothing
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim transferenciaid As Long = 0
        Dim origen As Integer = 0
        Dim destino As Integer = 0

        If origenid.SelectedValue = destinoid.SelectedValue Then
            rwAlerta.RadAlert("Selecciona una sucursal destino diferente.", 350, 200, "Alerta", "", "")
        Else
            Dim ObjData As New DataControl(1)
            transferenciaid = ObjData.RunSQLScalarQuery("exec pTransferencia @cmd=1, @userid='" & Session("userid").ToString & "', @origenid='" & origenid.SelectedValue.ToString & "', @destinoid='" & destinoid.SelectedValue.ToString & "', @comentario='" & txtComentario.Text & "', @autorizadaBit=1")
            ObjData = Nothing

            Response.Redirect("~/portalcfd/almacen/editalote.aspx?id=" & transferenciaid.ToString)

        End If
    End Sub

End Class