Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Class portalcfd_usuarios_usuarios
    Inherits System.Web.UI.Page
    

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblUsersListLegend.Text = "Listado de Usuarios"
            lblUserEditLegend.Text = "Agregar/Editar Usuario"

            '''''''''''''''''''''''''''''''''
            'Combobox Values & Empty Message'
            '''''''''''''''''''''''''''''''''

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblNombre.Text = "Nombre:"
            lblEmail.Text = "Usuario:"
            lblContrasena.Text = "Contraseña:"
            lblPerfil.Text = "Perfil:"

            '''''''''''''''''''
            'Validators Titles'
            '''''''''''''''''''

            valNombre.Text = Resources.Resource.validatorMessage
            valPerfil.Text = Resources.Resource.validatorMessage
            valContrasena.Text = Resources.Resource.validatorMessage
            'revEmail.Text = Resources.Resource.validatorMessageEmail

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnAddUser.Text = "Agregar usuario"
            btnSaveUser.Text = Resources.Resource.btnSave
            btnCancel.Text = Resources.Resource.btnCancel

            Dim ObjData As New DataControl(0)
            'ObjData.Catalogo(cmbPerfil, "select id, descripcion from tblTipoPerfil order by descripcion", 0)
            ObjData.Catalogo(cmbFiltroEstatus, "select id, nombre from tblEstatus order by nombre", 0, True)
            ObjData.Catalogo(cmbEstatus, "select id, nombre from tblEstatus order by nombre", 0)
            ObjData = New DataControl(1)
            ObjData.Catalogo(cmbPerfil, "select id, nombre from tblUsuarioPerfil order by nombre", 0)
            ObjData.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0)
            ObjData = Nothing

        End If
    End Sub

#End Region

#Region "Load List Of Users"

    Function GetUsers() As DataSet

        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pUsuarios @cmd=6, @estatusid='" & cmbFiltroEstatus.SelectedValue.ToString & "', @txtSearch='" & txtSearch.Text.ToString & "', @clienteid='" & Session("clienteid").ToString & "'", conn)

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

#End Region

#Region "Telerik Grid Users Loading Events"

    Protected Sub userslist_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles userslist.NeedDataSource

        If Not e.IsFromDetailTable Then

            userslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            userslist.DataSource = GetUsers()

        End If

    End Sub

#End Region

#Region "Telerik Grid Language Modification(Spanish)"

    Protected Sub userslist_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles userslist.Init

        userslist.PagerStyle.NextPagesToolTip = "Ver mas"
        userslist.PagerStyle.NextPageToolTip = "Siguiente"
        userslist.PagerStyle.PrevPagesToolTip = "Ver más"
        userslist.PagerStyle.PrevPageToolTip = "Atrás"
        userslist.PagerStyle.LastPageToolTip = "Última Página"
        userslist.PagerStyle.FirstPageToolTip = "Primera Página"
        userslist.PagerStyle.PagerTextFormat = "{4}    Página {0} de {1}, Registros {2} al {3} de {5}"
        userslist.SortingSettings.SortToolTip = "Ordernar"
        userslist.SortingSettings.SortedAscToolTip = "Ordenar Asc"
        userslist.SortingSettings.SortedDescToolTip = "Ordenar Desc"


        Dim menu As Telerik.Web.UI.GridFilterMenu = userslist.FilterMenu
        Dim i As Integer = 0

        While i < menu.Items.Count

            If menu.Items(i).Text = "NoFilter" Or menu.Items(i).Text = "Contains" Then
                i = i + 1
            Else
                menu.Items.RemoveAt(i)
            End If

        End While

        Call ModificaIdiomaGrid()

    End Sub

    Private Sub ModificaIdiomaGrid()

        userslist.GroupingSettings.CaseSensitive = False

        Dim Menu As Telerik.Web.UI.GridFilterMenu = userslist.FilterMenu
        Dim item As Telerik.Web.UI.RadMenuItem

        For Each item In Menu.Items

            ''''''''''''''''''''''''''''''''''''''''''''''
            'Change The Text For The StartsWith Menu Item'
            ''''''''''''''''''''''''''''''''''''''''''''''

            If item.Text = "StartsWith" Then
                item.Text = "Empieza con"
            End If

            If item.Text = "NoFilter" Then
                item.Text = "Sin Filtro"
            End If

            If item.Text = "Contains" Then
                item.Text = "Contiene"
            End If

            If item.Text = "EndsWith" Then
                item.Text = "Termina con"
            End If

        Next

    End Sub

#End Region

#Region "Telerik Grid Users Editing & Deleting Events"

    Protected Sub userslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles userslist.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Users" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('Va a borrar un usuario. ¿Desea continuar?');")

                'Dim imgAutorizado As Image = CType(e.Item.FindControl("imgAutorizado"), Image)
                'imgAutorizado.Visible = e.Item.DataItem("autorizaOrdenBit")

            End If

        End If

    End Sub

    Protected Sub userslist_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles userslist.ItemCommand

        Select Case e.CommandName

            Case "cmdEdit"
                EditUser(e.CommandArgument)

            Case "cmdDelete"
                DeleteUser(e.CommandArgument)

        End Select

    End Sub

    Private Sub DeleteUser(ByVal id As Integer)

        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pUsuarios @cmd='6', @userid ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelUserRegistration.Visible = False

            userslist.DataSource = GetUsers()
            userslist.DataBind()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Private Sub EditUser(ByVal id As Integer)

        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try
            Dim cmd As New SqlCommand("EXEC pUsuarios @cmd=2, @userid='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                txtNombre.Text = rs("nombre")
                txtEmail.Text = rs("email")
                txtContrasena.Text = rs("contrasena")

                Dim ObjData As New DataControl(0)
                ObjData.Catalogo(cmbEstatus, "select id, nombre from tblEstatus order by nombre", rs("estatusid"))
                ObjData = New DataControl(1)
                ObjData.Catalogo(cmbPerfil, "select id, nombre from tblUsuarioPerfil order by nombre", rs("perfilid"))
                ObjData.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", rs("sucursalid"))
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

                chkConteoDiario.Checked = CBool(rs("conteo_diarioBit"))

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

#End Region

#Region "Telerik Grid Users Column Names (From Resource File)"

    Protected Sub clientslist_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles userslist.ItemCreated

        If TypeOf e.Item Is GridHeaderItem Then

            Dim header As GridHeaderItem = CType(e.Item, GridHeaderItem)

            If e.Item.OwnerTableView.Name = "Users" Then

                header("nombre").Text = "Usuario"
                header("email").Text = "Email"

            End If

        End If

    End Sub

#End Region

#Region "Display User Data Panel"

    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUser.Click

        InsertOrUpdate.Value = 0

        txtNombre.Text = ""
        txtEmail.Text = ""
        txtContrasena.Text = ""
        chkConteoDiario.Checked = False
        panelUserRegistration.Visible = True

    End Sub

#End Region

#Region "Save User"

    Protected Sub btnSaveUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveUser.Click

        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Dim conteo_diarioBit As Integer = 0

        If chkConteoDiario.Checked Then
            conteo_diarioBit = 1
        End If

        Try

            If InsertOrUpdate.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pUsuarios @cmd=3, @nombre='" & txtNombre.Text & "', @email='" & txtEmail.Text & "', @contrasena='" & txtContrasena.Text & "', @perfilid='" & cmbPerfil.SelectedValue.ToString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @estatusid='" & cmbEstatus.SelectedValue.ToString & "', @imgFoto='" & ActualizaFoto() & "', @clienteid='" & Session("clienteid").ToString & "', @conteo_diarioBit='" & conteo_diarioBit.ToString & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelUserRegistration.Visible = False

                userslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                userslist.DataSource = GetUsers()
                userslist.DataBind()

                conn.Close()
                conn.Dispose()

            Else

                Dim cmd As New SqlCommand("EXEC pUsuarios @cmd=4, @nombre='" & txtNombre.Text & "', @email='" & txtEmail.Text & "', @contrasena='" & txtContrasena.Text & "', @perfilid='" & cmbPerfil.SelectedValue.ToString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @estatusid='" & cmbEstatus.SelectedValue.ToString & "', @userid='" & UsersID.Value.ToString & "', @imgFoto='" & ActualizaFoto() & "', @clienteid='" & Session("clienteid").ToString & "', @conteo_diarioBit='" & conteo_diarioBit.ToString & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelUserRegistration.Visible = False

                userslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                userslist.DataSource = GetUsers()
                userslist.DataBind()

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

#End Region

#Region "Cancel User (Save/Edit)"

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        InsertOrUpdate.Value = 0

        txtNombre.Text = ""
        txtEmail.Text = ""
        txtContrasena.Text = ""
        chkConteoDiario.Checked = False
        panelUserRegistration.Visible = False

    End Sub

#End Region

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        userslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        userslist.DataSource = GetUsers()
        userslist.DataBind()
    End Sub

    Private Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        cmbFiltroEstatus.SelectedValue = 0
        txtSearch.Text = ""
        userslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        userslist.DataSource = GetUsers()
        userslist.DataBind()
    End Sub

End Class