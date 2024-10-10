Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class subfamilias
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim objCat As New DataControl(1)
            objCat.Catalogo(familiaid, "select id, nombre from tblFamilia where isnull(borradoBit,0)=0 order by id", 0)
            objCat = Nothing

            Call muestraSubFamilias()
        End If
    End Sub

    Private Sub muestraSubFamilias()
        Dim ds As New DataSet()
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("EXEC pSubFamilias @cmd=1")
        grdSubFamilias.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
        grdSubFamilias.DataSource = ds
        grdSubFamilias.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub grdSubFamilias_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdSubFamilias.ItemCommand
        Select Case e.CommandName

            Case "cmdEdit"
                EditaSubFamilia(e.CommandArgument)

            Case "cmdDelete"
                EliminaSubFamilia(e.CommandArgument)

        End Select
    End Sub

    Private Sub grdSubFamilias_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdSubFamilias.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "SubFamilias" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea eliminar esta subfamilia de la base de datos?');")

            End If

        End If
    End Sub

    Private Sub grdSubFamilias_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdSubFamilias.NeedDataSource
        Dim ds As New DataSet()
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("EXEC pSubFamilias @cmd=1")
        grdSubFamilias.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
        grdSubFamilias.DataSource = ds
        ObjData = Nothing
    End Sub

    Private Sub btnAgregaSubFamilia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregaSubFamilia.Click
        InsertOrUpdate.Value = 0
        familiaid.SelectedValue = 0
        txtNombre.Text = ""
        txtPorcentajeUtilidad.Text = 0
        panelRegistroSubFamilia.Visible = True
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        InsertOrUpdate.Value = 0
        familiaid.SelectedValue = 0
        txtNombre.Text = ""
        txtPorcentajeUtilidad.Text = 0
        panelRegistroSubFamilia.Visible = False
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim ObjData As New DataControl(1)
        If InsertOrUpdate.Value = 0 Then
            ObjData.RunSQLQuery("EXEC pSubFamilias @cmd=4, @familiaid='" & familiaid.SelectedValue & "', @nombre='" & txtNombre.Text.ToString.ToUpper & "', @porcentaje_utilidad='" & txtPorcentajeUtilidad.Text.ToString & "'")
        Else
            ObjData.RunSQLQuery("EXEC pSubFamilias @cmd=5, @subfamiliaid='" & SubFamiliaID.Value & "', @familiaid='" & familiaid.SelectedValue & "', @nombre='" & txtNombre.Text.ToString.ToUpper & "', @porcentaje_utilidad='" & txtPorcentajeUtilidad.Text.ToString & "'")
        End If
        ObjData = Nothing
        Response.Redirect("subfamilias.aspx")
    End Sub

    Private Sub EditaSubFamilia(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pSubFamilias @cmd=2, @subfamiliaid='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Dim objCat As New DataControl(1)
                objCat.Catalogo(familiaid, "select id, nombre from tblFamilia where isnull(borradoBit,0)=0 order by id", rs("familiaid"))
                objCat = Nothing
                txtNombre.Text = rs("nombre")
                txtPorcentajeUtilidad.Text = rs("porcentaje_utilidad")
                panelRegistroSubFamilia.Visible = True
                '
                InsertOrUpdate.Value = 1
                SubFamiliaID.Value = rs("id")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub EliminaSubFamilia(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pSubFamilias @cmd=3, @subfamiliaid='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistroSubFamilia.Visible = False

            Response.Redirect("subfamilias.aspx")

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

End Class