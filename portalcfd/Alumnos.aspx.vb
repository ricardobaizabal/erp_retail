Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class Alumnos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal where isnull(borradobit,0)=0  and id <>4 order by nombre ", 0)
            objCat.Catalogo(cmbGradoEscolar, "select id, nombre from tblGradoEscolar where isnull(borradobit,0)=0 order by nombre", 0)
            objCat.Catalogo(cmbRegistroPortalWeb, "select id, ISNULL(nombre_alumno,'') + ' | ' + convert(varchar, id) + ' - ' + nombre_padre_madre + ' (' + email + ')'  as nombre  from tblRegistroPortalWeb order by nombre_alumno", 0)
            objCat = Nothing
        End If
    End Sub

    Private Sub btnAgregarAlumno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarAlumno.Click
        panelRegistration.Visible = True
        AlumnoID.Value = 0
        txtNombreAlumno.Text = ""
        cmbSucursal.SelectedValue = 0
        cmbGradoEscolar.SelectedValue = 0
        txtFamilia.Text = ""
        txtContactoPadre.Text = ""
        txtTelofonoContactoPadre.Text = ""
        txtEmailContactoPadre.Text = ""

        txtContactoMadre.Text = ""
        txtTelofonoContactoMadre.Text = ""
        txtEmailContactoMadre.Text = ""
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim conn As New SqlConnection(Session("conexion"))

        Try

            If AlumnoID.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pAlumnos @cmd=1, @matricula='" & txtMatricula.Text & "', @nombre='" & txtNombreAlumno.Text & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @gradoescolarid='" & cmbGradoEscolar.SelectedValue.ToString & "', @familia='" & txtFamilia.Text & "', @contacto_padre='" & txtContactoPadre.Text & "', @telefono_contacto_padre='" & txtTelofonoContactoPadre.Text & "', @email_contacto_padre='" & txtEmailContactoPadre.Text & "', @contacto_madre='" & txtContactoMadre.Text & "', @telefono_contacto_madre='" & txtTelofonoContactoMadre.Text & "', @email_contacto_madre='" & txtEmailContactoMadre.Text & "', @usuario='" & txtusuario.Text & "', @contrasena='" & txtcontraseña.Text & "', @registrowebid='" & cmbRegistroPortalWeb.SelectedValue.ToString & "', @usuarioid='" & Session("userid").ToString & "'", conn)
                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

                AlumnosList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                AlumnosList.DataSource = ObtenerAlumnos()
                AlumnosList.DataBind()

                conn.Close()
                conn.Dispose()

            Else
                Dim cmd As New SqlCommand("EXEC pAlumnos @cmd=4, @matricula='" & txtMatricula.Text & "', @nombre='" & txtNombreAlumno.Text & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @gradoescolarid='" & cmbGradoEscolar.SelectedValue.ToString & "', @familia='" & txtFamilia.Text & "', @contacto_padre='" & txtContactoPadre.Text & "', @telefono_contacto_padre='" & txtTelofonoContactoPadre.Text & "', @email_contacto_padre='" & txtEmailContactoPadre.Text & "', @id='" & AlumnoID.Value.ToString & "', @contacto_madre='" & txtContactoMadre.Text & "', @telefono_contacto_madre='" & txtTelofonoContactoMadre.Text & "', @email_contacto_madre='" & txtEmailContactoMadre.Text & "', @usuario='" & txtusuario.Text & "', @contrasena='" & txtcontraseña.Text & "', @registrowebid='" & cmbRegistroPortalWeb.SelectedValue.ToString & "', @usuarioid='" & Session("userid").ToString & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

                AlumnosList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                AlumnosList.DataSource = ObtenerAlumnos()
                AlumnosList.DataBind()

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
        panelRegistration.Visible = False
        AlumnoID.Value = 0
        txtNombreAlumno.Text = ""
        cmbSucursal.SelectedValue = 0
        cmbGradoEscolar.SelectedValue = 0
        txtFamilia.Text = ""
        txtContactoPadre.Text = ""
        txtTelofonoContactoPadre.Text = ""
        txtEmailContactoPadre.Text = ""
        txtContactoMadre.Text = ""
        txtTelofonoContactoMadre.Text = ""
        txtEmailContactoMadre.Text = ""
        cmbRegistroPortalWeb.SelectedValue = 0
    End Sub

    Private Sub AlumnosList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles AlumnosList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaAlumno(e.CommandArgument)

            Case "cmdDelete"
                EliminarAlumno(e.CommandArgument)

        End Select
    End Sub

    Private Sub AlumnosList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles AlumnosList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Alumnos" Then
                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('Va a borrar este registro. ¿Desea continuar?');")
            End If
        End If
    End Sub

    Private Sub AlumnosList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles AlumnosList.NeedDataSource
        If Not e.IsFromDetailTable Then
            AlumnosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            AlumnosList.DataSource = ObtenerAlumnos()
        End If
    End Sub

    Function ObtenerAlumnos() As DataSet

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 5))
        p.Add(New SqlParameter("@txtSearch", txtSearch.Text))
        ds = ObjData.FillDataSet("pAlumnos", p)
        ObjData = Nothing

        Return ds

    End Function

    Private Sub EditaAlumno(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pAlumnos @cmd=2, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtMatricula.Text = rs("matricula")
                txtNombreAlumno.Text = rs("nombre")
                cmbSucursal.SelectedValue = rs("sucursalid")
                cmbGradoEscolar.SelectedValue = rs("gradoescolarid")
                txtFamilia.Text = rs("familia")
                txtContactoPadre.Text = rs("contacto_padre")
                txtTelofonoContactoPadre.Text = rs("telefono_contacto_padre")
                txtEmailContactoPadre.Text = rs("email_contacto_padre")
                txtContactoMadre.Text = rs("contacto_madre")
                txtTelofonoContactoMadre.Text = rs("telefono_contacto_madre")
                txtEmailContactoMadre.Text = rs("email_contacto_madre")
                panelRegistration.Visible = True
                btnGuardar.Text = "Actualizar"
                AlumnoID.Value = id
                txtusuario.Text = rs("usuario")
                txtcontraseña.Text = rs("contrasena")
                cmbRegistroPortalWeb.SelectedValue = rs("registrowebid")
            End If

        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub EliminarAlumno(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pAlumnos @cmd=3, @id=" & id.ToString, conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistration.Visible = False

            AlumnosList.DataSource = ObtenerAlumnos()
            AlumnosList.DataBind()

        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        AlumnosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        AlumnosList.DataSource = ObtenerAlumnos()
        AlumnosList.DataBind()
    End Sub

    Private Sub btnTodo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTodo.Click
        txtSearch.Text = ""
        AlumnosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        AlumnosList.DataSource = ObtenerAlumnos()
        AlumnosList.DataBind()
    End Sub

End Class