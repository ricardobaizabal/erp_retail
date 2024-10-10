Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Partial Public Class categorias
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call muestraCategorias()
        End If
    End Sub

    Private Sub muestraCategorias()
        Dim ds As New DataSet()
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("EXEC pFamilias @cmd=1")
        grdCategorias.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
        grdCategorias.DataSource = ds
        ObjData = Nothing
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        InsertOrUpdate.Value = 0
        txtNombre.Text = ""
        panelRegistroCategoria.Visible = False
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim ObjData As New DataControl(1)
        If InsertOrUpdate.Value = 0 Then
            ObjData.RunSQLQuery("EXEC pFamilias @cmd=4, @nombre='" & txtNombre.Text.ToString.ToUpper & "'")
        Else
            ObjData.RunSQLQuery("EXEC pFamilias @cmd=5, @nombre='" & txtNombre.Text.ToString.ToUpper & "', @familiaid='" & categoriaID.Value.ToString & "'")
        End If
        ObjData = Nothing
        Response.Redirect("categorias.aspx")
    End Sub

    Private Sub EditaCategoria(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pFamilias @cmd=2, @familiaid='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtNombre.Text = rs("nombre")
                panelRegistroCategoria.Visible = True
                '
                InsertOrUpdate.Value = 1
                categoriaID.Value = rs("id")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub EliminaCategoria(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pFamilias @cmd=3, @familiaid='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistroCategoria.Visible = False

            Response.Redirect("categorias.aspx")

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub grdCategorias_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdCategorias.ItemCommand
        Select Case e.CommandName

            Case "cmdEdit"
                EditaCategoria(e.CommandArgument)

            Case "cmdDelete"
                EliminaCategoria(e.CommandArgument)

        End Select
    End Sub

    Private Sub grdCategorias_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdCategorias.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Categorias" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea familia de productos de la base de datos?');")

            End If

        End If
    End Sub

    Private Sub btnAgregaCategoria_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregaCategoria.Click
        InsertOrUpdate.Value = 0
        txtNombre.Text = ""
        panelRegistroCategoria.Visible = True
    End Sub

    Private Sub grdCategorias_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdCategorias.NeedDataSource
        Dim ds As New DataSet()
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("EXEC pFamilias @cmd=1")
        grdCategorias.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
        grdCategorias.DataSource = ds
        ObjData = Nothing
    End Sub

End Class