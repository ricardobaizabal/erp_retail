Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Threading
Imports System.IO

Public Class activos
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

            lblActivosListLegend.Text = "Listado de activos fijos"
            lblEditLegend.Text = "Agregar/Editar activo fijo"

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnAgregaActivo.Text = "Agregar activo fijo"
            btnGuardar.Text = Resources.Resource.btnSave
            btnCancelar.Text = Resources.Resource.btnCancel

            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(cmbSucursalFiltro, "select id, nombre from tblSucursal order by nombre", 0, True)
            ObjData.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0)
            ObjData = Nothing

        End If

    End Sub

    Private Sub btnAgregaActivo_Click(sender As Object, e As EventArgs) Handles btnAgregaActivo.Click
        InsertOrUpdate.Value = 0
        lblCodigoValue.Text = ""
        cmbSucursal.SelectedValue = 0
        txtDescripcion.Text = ""
        txtMarca.Text = ""
        txtModelo.Text = ""
        txtUso.Text = ""
        txtCostoReposicion.Text = ""
        hdnFoto.Value = ""
        hdnAnexo.Value = ""
        panelAgregarActivo.Visible = True
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        InsertOrUpdate.Value = 0
        lblCodigoValue.Text = ""
        cmbSucursal.SelectedValue = 0
        txtDescripcion.Text = ""
        txtMarca.Text = ""
        txtModelo.Text = ""
        hdnFoto.Value = ""
        panelAgregarActivo.Visible = False
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Dim ObjData As New DataControl(1)
        Dim mensaje As String = ""
        Dim codigo As String = ""

        Try

            If InsertOrUpdate.Value = 0 Then
                codigo = ObjData.RunSQLScalarQueryString("EXEC pActivoFijo @cmd=3, @sucursalid='" & cmbSucursal.SelectedValue & "', @userid='" & Session("userid").ToString & "', @descripcion='" & txtDescripcion.Text & "', @marca='" & txtMarca.Text & "', @modelo='" & txtModelo.Text & "', @imgFoto='" & ActualizaFoto() & "', @uso='" & txtUso.Text & "', @anexo='" & ActualizaAnexo() & "', @costo_reposicion='" & txtCostoReposicion.Text & "'")

                panelAgregarActivo.Visible = False

                grdActivosFijos.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                grdActivosFijos.DataSource = ActivosFijos()
                grdActivosFijos.DataBind()

                mensaje = "<br>Activo fijo guardado exitosamente:<br><br><span style=" & Chr(34) & "font-size: 18px; text-align: center;" & Chr(34) & ">Código: " & codigo.ToString & "</span>"
                rwAlerta.RadAlert(mensaje, 330, 200, "Alerta", "", "")
                ''rwAlerta.RadConfirm(mensaje, "confirmAlert", 420, 200, Nothing, "Alerta")
            Else

                ObjData.RunSQLQuery("EXEC pActivoFijo @cmd=4, @id='" & ActivoID.Value & "', @sucursalid='" & cmbSucursal.SelectedValue & "', @userid='" & Session("userid").ToString & "', @descripcion='" & txtDescripcion.Text & "', @marca='" & txtMarca.Text & "', @modelo='" & txtModelo.Text & "', @imgFoto='" & ActualizaFoto() & "', @uso='" & txtUso.Text & "', @anexo='" & ActualizaAnexo() & "', @costo_reposicion='" & txtCostoReposicion.Text & "'")

                panelAgregarActivo.Visible = False

                grdActivosFijos.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                grdActivosFijos.DataSource = ActivosFijos()
                grdActivosFijos.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        End Try
    End Sub

    Private Function ActualizaFoto() As String
        '
        '   Actualiza o Agregar Foto
        '
        Dim archivo As String = ""
        Dim i As Integer = 0

        If hdnFoto.Value.ToString = "" Then
            If foto.PostedFile.ContentLength > 0 Then
                archivo = foto.PostedFile.FileName.Substring(foto.PostedFile.FileName.LastIndexOf("\") + 1)
                If File.Exists(Server.MapPath("~/portalcfd/almacen/images/") + archivo) Then
                    For i = 1 To 99
                        archivo = i.ToString + "_" + foto.PostedFile.FileName.Substring(foto.PostedFile.FileName.LastIndexOf("\") + 1)
                        If Not File.Exists(Server.MapPath("~/portalcfd/almacen/images/") + archivo) Then
                            foto.PostedFile.SaveAs(Server.MapPath("~/portalcfd/almacen/images/") + archivo)
                            Exit For
                        End If
                    Next
                Else
                    foto.PostedFile.SaveAs(Server.MapPath("~/portalcfd/almacen/images/") + archivo)
                End If
            Else
                archivo = hdnFoto.Value.ToString
            End If
        Else
            If foto.PostedFile.ContentLength > 0 Then
                archivo = foto.PostedFile.FileName.Substring(foto.PostedFile.FileName.LastIndexOf("\") + 1)
                If File.Exists(Server.MapPath("~/portalcfd/almacen/images/") + archivo) Then
                    For i = 1 To 99
                        archivo = i.ToString + "_" + foto.PostedFile.FileName.Substring(foto.PostedFile.FileName.LastIndexOf("\") + 1)
                        If Not File.Exists(Server.MapPath("~/portalcfd/almacen/images/") + archivo) Then
                            foto.PostedFile.SaveAs(Server.MapPath("~/portalcfd/almacen/images/") + archivo)
                            Exit For
                        End If
                    Next
                Else
                    foto.PostedFile.SaveAs(Server.MapPath("~/portalcfd/almacen/images/") + archivo)
                End If
            Else
                archivo = hdnFoto.Value.ToString
            End If
        End If

        Return archivo

    End Function

    Private Function ActualizaAnexo() As String
        '
        '   Actualiza o Agregar Foto
        '
        Dim archivo As String = ""
        Dim i As Integer = 0

        If hdnAnexo.Value.ToString = "" Then
            If anexo.PostedFile.ContentLength > 0 Then
                archivo = anexo.PostedFile.FileName.Substring(anexo.PostedFile.FileName.LastIndexOf("\") + 1)
                If File.Exists(Server.MapPath("~/portalcfd/almacen/anexos/") + archivo) Then
                    For i = 1 To 99
                        archivo = i.ToString + "_" + anexo.PostedFile.FileName.Substring(anexo.PostedFile.FileName.LastIndexOf("\") + 1)
                        If Not File.Exists(Server.MapPath("~/portalcfd/almacen/anexos/") + archivo) Then
                            anexo.PostedFile.SaveAs(Server.MapPath("~/portalcfd/almacen/anexos/") + archivo)
                            Exit For
                        End If
                    Next
                Else
                    anexo.PostedFile.SaveAs(Server.MapPath("~/portalcfd/almacen/anexos/") + archivo)
                End If
            Else
                archivo = hdnAnexo.Value.ToString
            End If
        Else
            If anexo.PostedFile.ContentLength > 0 Then
                archivo = anexo.PostedFile.FileName.Substring(anexo.PostedFile.FileName.LastIndexOf("\") + 1)
                If File.Exists(Server.MapPath("~/portalcfd/almacen/anexos/") + archivo) Then
                    For i = 1 To 99
                        archivo = i.ToString + "_" + anexo.PostedFile.FileName.Substring(anexo.PostedFile.FileName.LastIndexOf("\") + 1)
                        If Not File.Exists(Server.MapPath("~/portalcfd/almacen/anexos/") + archivo) Then
                            anexo.PostedFile.SaveAs(Server.MapPath("~/portalcfd/almacen/anexos/") + archivo)
                            Exit For
                        End If
                    Next
                Else
                    anexo.PostedFile.SaveAs(Server.MapPath("~/portalcfd/almacen/anexos/") + archivo)
                End If
            Else
                archivo = hdnAnexo.Value.ToString
            End If
        End If

        Return archivo

    End Function

    Private Sub grdActivosFijos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdActivosFijos.NeedDataSource
        If Not e.IsFromDetailTable Then

            grdActivosFijos.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            grdActivosFijos.DataSource = ActivosFijos()

        End If
    End Sub

    Function ActivosFijos() As DataSet

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pActivoFijo @cmd=1, @sucursalid='" & cmbSucursalFiltro.SelectedValue.ToString & "', @txtSearch='" & txtSearch.Text.ToString & "'", conn)

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

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

    End Sub

    Private Sub btnAll_Click(sender As Object, e As EventArgs) Handles btnAll.Click

    End Sub

    Private Sub grdActivosFijos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdActivosFijos.ItemCommand
        Select Case e.CommandName

            Case "cmdEdit"
                Edita(e.CommandArgument)

            Case "cmdDelete"
                Elimina(e.CommandArgument)

            Case "cmdDownload"
                DescargarAnexo(e.CommandArgument)

        End Select
    End Sub

    Private Sub Elimina(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pActivoFijo @cmd=5, @id='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            InsertOrUpdate.Value = 0
            lblCodigoValue.Text = ""
            cmbSucursal.SelectedValue = 0
            txtDescripcion.Text = ""
            txtMarca.Text = ""
            txtModelo.Text = ""
            hdnFoto.Value = ""
            panelAgregarActivo.Visible = False

            grdActivosFijos.DataSource = ActivosFijos()
            grdActivosFijos.DataBind()

        Catch ex As Exception
        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Private Sub Edita(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try
            Dim cmd As New SqlCommand("EXEC pActivoFijo @cmd=2, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                lblCodigoValue.Text = rs("codigo")
                Dim ObjData As New DataControl(1)
                ObjData.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", rs("sucursalid"))
                ObjData = Nothing

                txtDescripcion.Text = rs("descripcion")
                txtMarca.Text = rs("marca")
                txtModelo.Text = rs("modelo")
                txtUso.Text = rs("uso")
                txtCostoReposicion.Text = rs("costo_reposicion")

                If Not DBNull.Value.Equals(rs("imgFoto")) Then
                    imgFoto.ImageUrl = "~/portalcfd/almacen/images/" & rs("imgFoto")
                End If

                If rs("imgFoto") = "" Then
                    imgFoto.Visible = False
                    hdnFoto.Value = ""
                    'imgBtnEliminarFoto.Visible = False
                Else
                    imgFoto.Visible = True
                    'imgBtnEliminarFoto.Visible = True
                    imgFoto.ImageUrl = "~/portalcfd/almacen/images/" & rs("imgFoto")
                    hdnFoto.Value = rs("imgFoto").ToString
                End If

                If rs("anexo") = "" Then
                    hdnAnexo.Value = ""
                Else
                    hdnAnexo.Value = rs("anexo").ToString
                End If

                panelAgregarActivo.Visible = True

                InsertOrUpdate.Value = 1
                ActivoID.Value = id

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Private Sub DescargarAnexo(ByVal anexo As String)
        Dim FilePath = Server.MapPath("~/portalcfd/almacen/anexos/") & anexo
        Dim FileName As String = Path.GetFileName(FilePath)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
        Response.Flush()
        Response.WriteFile(FilePath)
        Response.End()
    End Sub

    Private Sub grdActivosFijos_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdActivosFijos.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim btnDownload As ImageButton = CType(e.Item.FindControl("btnDownload"), ImageButton)
                Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('Va a borrar un registro de activo fijo. ¿Desea continuar?');")
                If e.Item.DataItem("anexo").ToString.Length = 0 Then
                    btnDownload.Visible = False
                End If
        End Select
    End Sub

End Class