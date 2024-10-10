Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class GradosEscolares
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbNivelEscolar, "select id, nombre from tblNivelEscolar where isnull(borradobit,0)=0", 0)
            objCat = Nothing
        End If
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        GradoID.Value = 0
        cmbNivelEscolar.SelectedValue = 0
        txtNombre.Text = ""
        panelRegistration.Visible = True
    End Sub

    Private Sub GradosEscolaresList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles GradosEscolaresList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaGradoEscolar(e.CommandArgument)

            Case "cmdDelete"
                DeleteGradoEscolar(e.CommandArgument)

        End Select
    End Sub

    Private Sub GradosEscolaresList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles GradosEscolaresList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "GradoEscolar" Then
                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('Va a borrar este registro. ¿Desea continuar?');")
            End If
        End If
    End Sub

    Private Sub GradosEscolaresList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles GradosEscolaresList.NeedDataSource
        If Not e.IsFromDetailTable Then
            GradosEscolaresList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            GradosEscolaresList.DataSource = ObtenerCiclos()
        End If
    End Sub

    Function ObtenerCiclos() As DataSet

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 5))
        ds = ObjData.FillDataSet("pCatalogoGradoEscolar", p)
        ObjData = Nothing

        Return ds

    End Function

    Private Sub EditaGradoEscolar(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoGradoEscolar @cmd=2, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtNombre.Text = rs("nombre")
                cmbNivelEscolar.SelectedValue = rs("nivelescolarid")
                panelRegistration.Visible = True
                btnGuardar.Text = "Actualizar"
                GradoID.Value = id
            End If

        Catch ex As Exception

        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub DeleteGradoEscolar(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoGradoEscolar @cmd=3, @id=" & id.ToString, conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistration.Visible = False

            GradosEscolaresList.DataSource = ObtenerCiclos()
            GradosEscolaresList.DataBind()

        Catch ex As Exception


        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim conn As New SqlConnection(Session("conexion"))

        Try

            If GradoID.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pCatalogoGradoEscolar @cmd=1, @nombre='" & txtNombre.Text & "', @nivelescolarid='" & cmbNivelEscolar.SelectedValue.ToString & "'", conn)
                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

                GradosEscolaresList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                GradosEscolaresList.DataSource = ObtenerCiclos()
                GradosEscolaresList.DataBind()

                conn.Close()
                conn.Dispose()

            Else
                Dim cmd As New SqlCommand("EXEC pCatalogoGradoEscolar @cmd=4, @nombre='" & txtNombre.Text & "', @nivelescolarid='" & cmbNivelEscolar.SelectedValue.ToString & "', @id='" & GradoID.Value.ToString & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

                GradosEscolaresList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                GradosEscolaresList.DataSource = ObtenerCiclos()
                GradosEscolaresList.DataBind()

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
        GradoID.Value = 0
        btnGuardar.Text = "Guardar"
        txtNombre.Text = ""
        cmbNivelEscolar.SelectedValue = 0
        panelRegistration.Visible = False
    End Sub

End Class