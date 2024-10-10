Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Web.UI.RadNumericTextBox
Imports System.IO
Imports System.Net.Mail
Imports Telerik.Reporting.Processing
Imports System.Globalization
Imports System.Threading
Public Class registro_porta_web
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(cmbSucursalb, "select id, nombre from tblSucursal where id <>4 order by nombre ", 0, 1)
            ObjData.Catalogo(cmbSucursal, "select id,nombre from tblsucursal where id <>4 order by nombre ", 0)
            registroWebList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            registroWebList.DataSource = ObtenerRegistrosWeb()
            registroWebList.DataBind()
            ObjData = Nothing
        End If

    End Sub

    Private Sub registroWebList_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles registroWebList.NeedDataSource
        registroWebList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        registroWebList.DataSource = ObtenerRegistrosWeb()
    End Sub

    Private Sub registroWebList_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles registroWebList.ItemCommand
        Select Case e.CommandName
            Case "cmdEditar"
                RegistroWeb(e.CommandArgument)
        End Select
    End Sub

    Private Sub RegistroWeb(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pRegistroPortalWeb @cmd=3, @id='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                txtMatricula.Text = rs("matricula")
                txtContrasena.Text = rs("contrasena")
                txtNombreAlumno.Text = rs("nombre_alumno")
                cmbSucursal.SelectedValue = rs("sucursalid")
                txtNombrePadreMadre.Text = rs("nombre_padre_madre")
                txtEmail.Text = rs("email")

                InsertOrUpdate.Value = 1
                registroID.Value = id
                panelListado.Visible = False
                panelRegistroPortalWeb.Visible = True

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        registroWebList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        registroWebList.DataSource = ObtenerRegistrosWeb()
        registroWebList.DataBind()
    End Sub

    Private Sub btnAll_Click(sender As Object, e As EventArgs) Handles btnAll.Click
        Me.txtBusqueda.Text = ""
        Me.cmbsucursalb.SelectedValue = 0
        registroWebList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        registroWebList.DataSource = ObtenerRegistrosWeb()
        registroWebList.DataBind()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtMatricula.Text = ""
        txtNombreAlumno.Text = ""
        txtNombrePadreMadre.Text = ""
        txtEmail.Text = ""
        txtContrasena.Text = ""
        cmbSucursal.SelectedValue = 0
        panelListado.Visible = True
        panelRegistroPortalWeb.Visible = False
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            If registroID.Value > 0 Then

                Dim sql As String = ""
                sql = "EXEC pRegistroPortalWeb @cmd=4, @id='" & registroID.Value & "', @userid='" & Session("userid") & "', @nombre_padre_madre='" & txtNombrePadreMadre.Text.ToString & "', @email='" & txtEmail.Text.ToString & "', @contrasena='" & txtContrasena.Text.ToString & "', @matricula='" & txtMatricula.Text.ToString & "', @nombre_alumno='" & txtNombreAlumno.Text.ToString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "'"

                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery(sql)
                ObjData = Nothing

                txtMatricula.Text = ""
                txtNombreAlumno.Text = ""
                txtNombrePadreMadre.Text = ""
                txtEmail.Text = ""
                txtContrasena.Text = ""
                cmbSucursal.SelectedValue = 0
                panelListado.Visible = True
                panelRegistroPortalWeb.Visible = False

                rwAlerta.RadAlert("Datos actualizados exitosamente.", 330, 180, "Alerta", "", "")

            End If

            registroWebList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
            registroWebList.DataSource = ObtenerRegistrosWeb()
            registroWebList.DataBind()

        Catch ex As Exception
            rwAlerta.RadAlert(ex.Message.ToString(), 330, 180, "Alerta", "", "")
        Finally
        End Try
    End Sub

    Function ObtenerRegistrosWeb() As DataSet

        Dim sql As String = ""
        sql = "EXEC pRegistroPortalWeb @cmd=2, @busqueda='" & txtBusqueda.Text & "', @sucursalid='" & cmbSucursalb.SelectedValue & "'"

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter(sql, conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return ds

    End Function

End Class