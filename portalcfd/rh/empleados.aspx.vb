Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Threading
Imports System.IO

Partial Public Class empleados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblUsersListLegend.Text = "Listado de Empleados"
            lblUserEditLegend.Text = "Agregar/Editar Empleado"

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnAgregaEmpleado.Text = "Agregar empleado"
            btnGuardar.Text = Resources.Resource.btnSave
            btnCancelar.Text = Resources.Resource.btnCancel

            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(cmbSucursalFiltro, "select id, nombre from tblSucursal order by nombre", 0, True)
            ObjData.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0)
            ObjData = New DataControl(0)
            ObjData.Catalogo(cmbEstatusFiltro, "select id, nombre from tblEstatus order by nombre", 0)
            ObjData.Catalogo(cmbEstatus, "select id, nombre from tblEstatus order by nombre", 0)
            ObjData = Nothing

        End If

    End Sub

    Private Sub btnAgregaEmpleado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregaEmpleado.Click
        InsertOrUpdate.Value = 0
        txtNombre.Text = ""
        txtEmpresa.Text = ""
        cmbSucursal.SelectedValue = 0
        cmbFormaPago.SelectedValue = 0
        cmbEstatus.SelectedValue = 0
        calFechaIngreso.Clear()
        panelUserRegistration.Visible = True
    End Sub

    Private Sub grdEmpleados_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdEmpleados.ItemCommand
        Select Case e.CommandName

            Case "cmdEdit"
                EditaEmpleado(e.CommandArgument)

            Case "cmdDelete"
                EliminaEmpleado(e.CommandArgument)

        End Select
    End Sub

    Private Sub grdEmpleados_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdEmpleados.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Users" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('Va a borrar un empleado. ¿Desea continuar?');")

            End If

        End If
    End Sub

    Private Sub grdEmpleados_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdEmpleados.NeedDataSource
        If Not e.IsFromDetailTable Then

            grdEmpleados.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            grdEmpleados.DataSource = Empleados()

        End If
    End Sub

    Function Empleados() As DataSet

        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pEmpleados @cmd=1, @sucursalid='" & cmbSucursalFiltro.SelectedValue.ToString & "', @estatusid='" & cmbEstatusFiltro.SelectedValue.ToString & "', @txtSearch='" & txtSearch.Text.ToString & "', @clienteid='" & Session("clienteid").ToString & "', @userid='" & Session("userid").ToString & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function

    Private Sub EliminaEmpleado(ByVal id As Integer)

        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pEmpleados @cmd=5, @userid ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelUserRegistration.Visible = False

            grdEmpleados.DataSource = Empleados()
            grdEmpleados.DataBind()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Private Sub EditaEmpleado(ByVal id As Integer)

        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try
            Dim cmd As New SqlCommand("EXEC pEmpleados @cmd=2, @userid='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                txtNombre.Text = rs("nombre")
                txtEmpresa.Text = rs("empresa")
                cmbFormaPago.Text = rs("formapago")

                If rs("fecha_ingreso").ToString.Length > 0 Then
                    calFechaIngreso.SelectedDate = CDate(rs("fecha_ingreso"))
                End If

                Dim ObjData As New DataControl(1)
                ObjData.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", rs("sucursalid"))
                ObjData = New DataControl(0)
                ObjData.Catalogo(cmbEstatus, "select id, nombre from tblEstatus order by nombre", rs("estatusid"))
                ObjData = Nothing

                If Not DBNull.Value.Equals(rs("ImgHuella")) Then
                    imgHuella.ImageUrl = "data:image/jpeg;base64," & Convert.ToBase64String(DirectCast(rs("ImgHuella"), Byte()))
                Else
                    imgHuella.ImageUrl = "~/images/img_NotFingerPrint.jpg"
                End If

                If rs("imgFoto") = "" Then
                    imgFoto.Visible = False
                    hdnFoto.Value = ""
                    imgBtnEliminarFoto.Visible = False
                Else
                    imgFoto.Visible = True
                    imgBtnEliminarFoto.Visible = True
                    foto.Visible = False
                    imgFoto.ImageUrl = "~/images/fotos/" & rs("imgFoto").ToString
                    hdnFoto.Value = rs("imgFoto").ToString
                End If

                panelUserRegistration.Visible = True

                InsertOrUpdate.Value = 1
                UsersID.Value = id

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            If InsertOrUpdate.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pEmpleados @cmd=3, @nombre='" & txtNombre.Text & "', @empresa='" & txtEmpresa.Text & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @imgFoto='" & ActualizaFoto() & "', @formapago='" & cmbFormaPago.SelectedValue.ToString & "', @fecha_ingreso='" & calFechaIngreso.SelectedDate.Value.ToShortDateString & "', @estatusid='" & cmbEstatus.SelectedValue.ToString & "', @clienteid='" & Session("clienteid").ToString & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelUserRegistration.Visible = False

                grdEmpleados.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                grdEmpleados.DataSource = Empleados()
                grdEmpleados.DataBind()

                conn.Close()
                conn.Dispose()

            Else

                Dim cmd As New SqlCommand("EXEC pEmpleados @cmd=4, @nombre='" & txtNombre.Text & "', @empresa='" & txtEmpresa.Text & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @imgFoto='" & ActualizaFoto() & "', @formapago='" & cmbFormaPago.SelectedValue.ToString & "', @fecha_ingreso='" & calFechaIngreso.SelectedDate.Value.ToShortDateString & "', @clienteid='" & Session("clienteid").ToString & "', @estatusid='" & cmbEstatus.SelectedValue.ToString & "', @userid='" & UsersID.Value.ToString & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelUserRegistration.Visible = False

                grdEmpleados.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                grdEmpleados.DataSource = Empleados()
                grdEmpleados.DataBind()

                conn.Close()
                conn.Dispose()

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")

    End Sub

    Private Function ActualizaFoto() As String
        '
        '   Actualiza o Agregar Foto
        '
        Dim archivo As String = ""
        Dim i As Integer = 0
        Dim objImage, objThumbnail As System.Drawing.Image
        Dim strServerPath, strFilename As String
        Dim shtWidth, shtHeight As Short

        If hdnFoto.Value.ToString = "" Then
            If foto.PostedFile.ContentLength > 0 Then
                archivo = foto.PostedFile.FileName.Substring(foto.PostedFile.FileName.LastIndexOf("\") + 1)
                If File.Exists(Server.MapPath("~/images/fotos/") + archivo) Then
                    For i = 1 To 99
                        archivo = i.ToString + "_" + foto.PostedFile.FileName.Substring(foto.PostedFile.FileName.LastIndexOf("\") + 1)
                        If Not File.Exists(Server.MapPath("~/images/fotos/") + archivo) Then
                            foto.PostedFile.SaveAs(Server.MapPath("~/images/fotos/") + archivo)
                            Exit For
                        End If
                    Next
                Else
                    foto.PostedFile.SaveAs(Server.MapPath("~/images/fotos/") + archivo)
                End If
            Else
                archivo = hdnFoto.Value.ToString
            End If
        Else
            archivo = hdnFoto.Value.ToString
        End If

        'If hdnFoto.Value.ToString = "" Then
        '    If foto.Visible = True And foto.PostedFile.ContentLength > 0 Then
        '        strServerPath = Server.MapPath("~/images/fotos/")
        '        strFilename = strServerPath & archivo
        '        '
        '        ' Guarda Thumbnail en 124 pixeles de ancho
        '        '
        '        Try
        '            objImage = Drawing.Image.FromFile(strFilename)
        '            shtWidth = 124
        '            shtHeight = objImage.Height / (objImage.Width / shtWidth)
        '            ' Crea el thumbnail
        '            objThumbnail = objImage.GetThumbnailImage(shtWidth, shtHeight, Nothing, System.IntPtr.Zero)
        '            Response.ContentType = "image/jpeg"
        '            objThumbnail.Save(Server.MapPath("~/images/fotos/small/") + archivo, Drawing.Imaging.ImageFormat.Jpeg)
        '            objImage.Dispose()
        '            objThumbnail.Dispose()
        '        Catch ex As Exception
        '            Response.Write(ex.Message.ToString)
        '            Response.End()
        '        End Try
        '    End If
        'End If

        Return archivo

    End Function

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        InsertOrUpdate.Value = 0
        txtNombre.Text = ""
        txtEmpresa.Text = ""
        cmbSucursal.SelectedValue = 0
        cmbFormaPago.SelectedValue = 0
        cmbEstatus.SelectedValue = 0
        calFechaIngreso.Clear()
        panelUserRegistration.Visible = False
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        grdEmpleados.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        grdEmpleados.DataSource = Empleados()
        grdEmpleados.DataBind()
    End Sub

    Private Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        cmbSucursalFiltro.SelectedValue = 0
        cmbEstatusFiltro.SelectedValue = 0
        txtSearch.Text = ""
        grdEmpleados.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        grdEmpleados.DataSource = Empleados()
        grdEmpleados.DataBind()
    End Sub

End Class