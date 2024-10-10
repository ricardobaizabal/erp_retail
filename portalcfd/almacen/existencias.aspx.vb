Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class existencias
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            objCat.Catalogo(filtrofamiliaid, "select id, nombre from tblFamilia where isnull(borradoBit,0)=0 order by nombre", 0)
            objCat.Catalogo(filtrosubfamiliaid, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 order by nombre", 0)
            objCat.Catalogo(cmbsucursal, "select id, nombre from tblsucursal where isnull(borradoBit,0)=0 and id <>4 order by nombre", 0)
            objCat = Nothing
        End If
    End Sub

    Private Sub presentacionesList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles presentacionesList.NeedDataSource
        presentacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        presentacionesList.DataSource = GetProducts()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        presentacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        presentacionesList.DataSource = GetProducts()
        presentacionesList.DataBind()
    End Sub

    Private Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        txtSearch.Text = ""
        filtrofamiliaid.SelectedValue = 0
        filtrosubfamiliaid.SelectedValue = 0
        filtrosubfamiliaid.Enabled = False
        presentacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        presentacionesList.DataSource = GetProducts()
        presentacionesList.DataBind()
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

    Function GetProducts() As DataSet
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pReportePresentaciones @txtSearch='" & txtSearch.Text & "', @familiaid='" & filtrofamiliaid.SelectedValue.ToString & "', @subfamiliaid='" & filtrosubfamiliaid.SelectedValue.ToString & "', @sucursalid='" & cmbsucursal.SelectedValue.ToString & "'", conn)
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

End Class