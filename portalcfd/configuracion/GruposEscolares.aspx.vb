Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class GruposEscolares
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbGradoEscolar, "select id, nombre from tblGradoEscolar where isnull(borradobit,0)=0 order by nombre", 0)
            objCat = Nothing
        End If
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        GrupoID.Value = 0
        txtNombre.Text = ""
        cmbGradoEscolar.SelectedValue = 0
        panelRegistration.Visible = True
    End Sub

    Private Sub GruposEscolaresList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles GruposEscolaresList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaGrupoEscolar(e.CommandArgument)

            Case "cmdDelete"
                DeleteGrupoEscolar(e.CommandArgument)

        End Select
    End Sub

    Private Sub GruposEscolaresList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles GruposEscolaresList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "GrupoEscolar" Then
                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('Va a borrar este registro. ¿Desea continuar?');")
            End If
        End If
    End Sub

    Private Sub GruposEscolaresList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles GruposEscolaresList.NeedDataSource
        If Not e.IsFromDetailTable Then
            GruposEscolaresList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            GruposEscolaresList.DataSource = ObtenerCiclos()
        End If
    End Sub

    Function ObtenerCiclos() As DataSet

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 5))
        ds = ObjData.FillDataSet("pCatalogoGrupoEscolar", p)
        ObjData = Nothing

        Return ds

    End Function

    Private Sub EditaGrupoEscolar(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoGrupoEscolar @cmd=2, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                cmbGradoEscolar.SelectedValue = rs("gradoescolarid")
                txtNombre.Text = rs("nombre")
                panelRegistration.Visible = True
                btnGuardar.Text = "Actualizar"
                GrupoID.Value = id
            End If

        Catch ex As Exception

        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub DeleteGrupoEscolar(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoGrupoEscolar @cmd=3, @id=" & id.ToString, conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistration.Visible = False

            GruposEscolaresList.DataSource = ObtenerCiclos()
            GruposEscolaresList.DataBind()

        Catch ex As Exception


        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim conn As New SqlConnection(Session("conexion"))

        Try

            If GrupoID.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pCatalogoGrupoEscolar @cmd=1, @nombre='" & txtNombre.Text & "', @gradoescolarid='" & cmbGradoEscolar.SelectedValue.ToString & "'", conn)
                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

                GruposEscolaresList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                GruposEscolaresList.DataSource = ObtenerCiclos()
                GruposEscolaresList.DataBind()

                conn.Close()
                conn.Dispose()

            Else
                Dim cmd As New SqlCommand("EXEC pCatalogoGrupoEscolar @cmd=4, @nombre='" & txtNombre.Text & "', @id='" & GrupoID.Value.ToString & "', @gradoescolarid='" & cmbGradoEscolar.SelectedValue.ToString & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

                GruposEscolaresList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                GruposEscolaresList.DataSource = ObtenerCiclos()
                GruposEscolaresList.DataBind()

                conn.Close()
                conn.Dispose()

            End If

        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        GrupoID.Value = 0
        btnGuardar.Text = "Guardar"
        txtNombre.Text = ""
        cmbGradoEscolar.SelectedValue = 0
        panelRegistration.Visible = False
    End Sub

End Class