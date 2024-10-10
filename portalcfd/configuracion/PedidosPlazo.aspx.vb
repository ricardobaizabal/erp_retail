Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class PedidosPlazo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim conn As New SqlConnection(Session("conexion"))

            Try

                Dim cmd As New SqlCommand("EXEC pConfiguracionSistema @cmd=2, @TIPOCONFIGURACION='" & 1 & "'", conn)

                conn.Open()

                Dim rs As SqlDataReader
                rs = cmd.ExecuteReader()

                If rs.Read Then

                    txtdias.Text = rs("descripcion")

                End If

            Catch ex As Exception
                Response.Write(ex.Message.ToString())
                Response.End()
            Finally
                conn.Close()
                conn.Dispose()
            End Try
        End If
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim sql As String = ""
        sql = "EXEC pConfiguracionSistema @tipoconfiguracion=1, @descripcion='" & txtdias.Text & "'"

        Dim ObjData As New DataControl(1)
        ObjData.RunSQLScalarQuery(sql)
        ObjData = Nothing
        txtdias.Text = String.Empty
    End Sub

    

   

   

    Function ObtenerSucursales() As DataSet

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 5))
        ds = ObjData.FillDataSet("pCatalogoSucursal", p)
        ObjData = Nothing

        Return ds

    End Function

    Private Sub EditaSucursal(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoSucursal @cmd=2, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtNombre.Text = rs("nombre")
                panelRegistration.Visible = True
                btnGuardar.Text = "Actualizar"
                SucursalID.Value = id
            End If

        Catch ex As Exception

        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub DeleteSucursal(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoSucursal @cmd=3, @id=" & id.ToString, conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistration.Visible = False

        Catch ex As Exception

        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim conn As New SqlConnection(Session("conexion"))

        Try

            If SucursalID.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pCatalogoSucursal @cmd=1, @nombre='" & txtNombre.Text & "'", conn)
                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

               
                conn.Close()
                conn.Dispose()

            Else
                Dim cmd As New SqlCommand("EXEC pCatalogoSucursal @cmd=4, @nombre='" & txtNombre.Text & "', @id=" & SucursalID.Value.ToString, conn)

                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

              
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
        SucursalID.Value = 0
        btnGuardar.Text = "Guardar"
        txtNombre.Text = ""
        panelRegistration.Visible = False
    End Sub

End Class