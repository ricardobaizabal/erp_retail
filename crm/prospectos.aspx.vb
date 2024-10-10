Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Net.Mail

Public Class prospectos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Carga_Pestañas()
            FillRadComboBox(cmbEstados, "EXEC pCatalogos @cmd=1")
            FillRadComboBox(anexosList, "select archivo, nombre from tblDocumentosApoyo where tipoid=2")
            cmbEstados.Text = "--Seleccione--"
            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(cmdTipoEmpresa, "exec pCRMProspectos @cmd=6", 0)
            ObjData.Catalogo(cmbCobertura, "exec pCRMProspectos @cmd=7", 0)
            ObjData = Nothing
            '
        End If


    End Sub

    Private Sub CargaDatosEmail(ByVal prospectoId As Long)
        Dim conn As New SqlConnection(Session("conexion"))

        'Try

        Dim cmd As New SqlCommand("EXEC pCRMProspectos @cmd=2, @prospectoid='" & prospectoId.ToString & "'", conn)

        conn.Open()

        Dim rs As SqlDataReader
        rs = cmd.ExecuteReader()

        If rs.Read Then
            txtFrom.Text = Session("user_email")
            txtTo.Text = rs("email_contacto")
            txtRazonSocial.Text = rs("razonsocial")
        End If

        'Catch ex As Exception
        'Response.Write(ex.Message.ToString())
        'Finally

        conn.Close()
        conn.Dispose()

        'End 'Try
    End Sub

    Public Sub FillRadComboBox(ByVal RadComboBox As RadComboBox, ByVal SQLCommand As String)

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As SqlDataAdapter = New SqlDataAdapter(SQLCommand, conn)

        conn.Open()

        Dim ds As New DataSet
        cmd.Fill(ds)

        RadComboBox.DataSource = ds.Tables(0)
        RadComboBox.DataTextField = ds.Tables(0).Columns(1).ColumnName.ToString()
        RadComboBox.DataValueField = ds.Tables(0).Columns(0).ColumnName.ToString()
        RadComboBox.DataBind()

        conn.Close()
        conn.Dispose()
        conn = Nothing

    End Sub

    Protected Sub prospectosList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles prospectosList.ItemCommand
        Select Case e.CommandName

            Case "cmdEdit"
                EditaProspecto(e.CommandArgument)

                ContactoList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
                ContactoList.DataSource = ObtenerContacto()
                ContactoList.DataBind()
            Case "cmdDelete"
                EliminaProspecto(e.CommandArgument)

            Case "cmdSend"
                Call EnviaMensaje(e.CommandArgument)

        End Select
    End Sub

    Private Sub EnviaMensaje(ByVal id As Long)
        '
        Call CargaDatosEmail(id)
        '
        RadWindow1.VisibleOnPageLoad = True
    End Sub

    Private Sub EliminaProspecto(ByVal id As Long)

        Dim conn As New SqlConnection(Session("conexion"))

        'Try

        Dim cmd As New SqlCommand("EXEC pCRMProspectos @cmd=3, @prospectoid='" & id.ToString & "'", conn)

        conn.Open()

        Dim rs As SqlDataReader

        rs = cmd.ExecuteReader()
        rs.Close()

        conn.Close()

        panelRegistroProspecto.Visible = False

        prospectosList.DataSource = ObtenerProspectos()
        prospectosList.DataBind()

        'Catch ex As Exception
        'Response.Write(ex.Message.ToString())
        'Finally

        conn.Close()
        conn.Dispose()

        'End 'Try
    End Sub
    Private Sub EliminaContacto(ByVal id As Long)

        Dim conn As New SqlConnection(Session("conexion"))

        'Try

        Dim cmd As New SqlCommand("EXEC pCRMProspectos @cmd=13, @prospectoid='" & id.ToString & "'", conn)

        conn.Open()

        Dim rs As SqlDataReader

        rs = cmd.ExecuteReader()
        rs.Close()

        conn.Close()

        'panelRegistroProspecto.Visible = False

        ContactoList.DataSource = ObtenerContacto()
        ContactoList.DataBind()

        'Catch ex As Exception
        'Response.Write(ex.Message.ToString())
        'Finally

        conn.Close()
        conn.Dispose()

        'End 'Try
    End Sub

    Private Sub EditaProspecto(ByVal id As Long)
        'InsertOrUpdate.Value = 0
        Dim conn As New SqlConnection(Session("conexion"))

        'Try

        Dim cmd As New SqlCommand("EXEC pCRMProspectos @cmd=2, @prospectoid='" & id & "'", conn)

        conn.Open()

        Dim rs As SqlDataReader
        rs = cmd.ExecuteReader()

        If rs.Read Then

            txtRazonSocial.Text = rs("razonsocial")
            txtContacto.Text = rs("contacto")
            txtEmailContacto.Text = rs("email_contacto")
            txtTelefonoContacto.Text = rs("telefono_contacto")
            txtCalle.Text = rs("calle")
            txtNumExterior.Text = rs("num_ext")
            txtNumInterior.Text = rs("num_int")
            txtColonia.Text = rs("colonia")
            txtPais.Text = rs("pais")
            cmbEstados.SelectedValue = rs("estadoid")
            txtMunicipio.Text = rs("municipio")
            txtCP.Text = rs("cp")
            txtRFC.Text = rs("rfc")

            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(cmdTipoEmpresa, "exec pCRMProspectos @cmd=6", rs("tipoempresaid"))
            ObjData.Catalogo(cmbCobertura, "exec pCRMProspectos @cmd=7", rs("coberturaid"))
            ObjData = Nothing

            panelRegistroProspecto.Visible = True

            InsertOrUpdate.Value = 1
            InsertOrUpdate2.Value = 0
            ProspectoID.Value = id
            'ObtenerContacto()
            Carga_Pestañas()

        End If

        'Catch ex As Exception
        'Response.Write(ex.Message.ToString())
        'Finally

        conn.Close()
        conn.Dispose()

        'End 'Try

    End Sub

    Private Sub EditaContacto(ByVal id As Long)

        Dim conn As New SqlConnection(Session("conexion"))

        'Try

        Dim cmd As New SqlCommand("EXEC pCRMProspectos @cmd=12, @prospectoid='" & id & "'", conn)

        conn.Open()

        Dim rs As SqlDataReader
        rs = cmd.ExecuteReader()

        If rs.Read Then

            txtNombreContacto1.Text = rs("contacto")
            txtTelefonoContacto1.Text = rs("telefono")
            txtEmailContacto1.Text = rs("email")
            txtPuestoContacto.Text = rs("puesto")


            'Dim ObjData As New DataControl(1)
            'ObjData.Catalogo(cmdTipoEmpresa, "exec pCRMProspectos @cmd=6", rs("tipoempresaid"))
            'ObjData.Catalogo(cmbCobertura, "exec pCRMProspectos @cmd=7", rs("coberturaid"))
            'ObjData = Nothing

            panelRegistroProspecto.Visible = True

            'InsertOrUpdate.Value = 1
            InsertOrUpdate2.Value = 1
            ProspectoID2.Value = id
            'ObtenerContacto()
            Carga_Pestañas()

        End If

        'Catch ex As Exception
        'Response.Write(ex.Message.ToString())
        'Finally

        conn.Close()
        conn.Dispose()

        'End 'Try

    End Sub

    Protected Sub prospectosList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles prospectosList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Prospectos" Then



                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea borrar de la base de datos a este prospecto?');")
                If Session("perfilid") > 1 Then
                    lnkdel.Enabled = False
                    lnkdel.ToolTip = "Función no autorizada."
                End If
            End If

        End If
    End Sub

    Protected Sub prospectosList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles prospectosList.NeedDataSource
        If Not e.IsFromDetailTable Then

            prospectosList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
            prospectosList.DataSource = ObtenerProspectos()


        End If
    End Sub
    Function ObtenerContacto() As DataSet

        Dim sql As String = ""
        'If Session("perfilid") <> 1 Then
        '    sql = "EXEC pCRMOportunidades @cmd=7, @oportunidadid='" & OportunidadID.Value & "', @userid='" & Session("userid").ToString() & "'"
        'Else
        '    sql = "EXEC pCRMOportunidades @cmd=7, @oportunidadid='" & OportunidadID.Value & "'"
        'End If
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pCRMProspectos @cmd=11, @prospectoid='" & ProspectoID.Value & "'", conn)

        Dim ds As DataSet = New DataSet

        'Try

        conn.Open()

        cmd.Fill(ds)

        'ContactoList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
        'ContactoList.DataSource = ObtenerContacto()
        'ContactoList.DataBind()

        conn.Close()
        conn.Dispose()

        'Catch ex As Exception
        'Response.Write(ex.Message.ToString())
        'Finally

        conn.Close()
        conn.Dispose()

        'End 'Try

        Return ds

    End Function

    Function ObtenerProspectos() As DataSet
        Dim sql As String = ""
        If Session("perfilid") > 1 Then
            sql = "EXEC pCRMProspectos @cmd=1, @txtSearch='" & txtSearch.Text & "', @userid='" & Session("userid").ToString() & "'"
        Else
            sql = "EXEC pCRMProspectos @cmd=1, @txtSearch='" & txtSearch.Text & "'"
        End If

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter(sql, conn)

        Dim ds As DataSet = New DataSet

        'Try

        conn.Open()

        cmd.Fill(ds)

        conn.Close()
        conn.Dispose()

        'Catch ex As Exception
        'Response.Write(ex.Message.ToString())
        'Finally

        conn.Close()
        conn.Dispose()

        'End 'Try

        Return ds

    End Function

    Protected Sub btnAgregarProspecto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarProspecto.Click
        InsertOrUpdate.Value = 0

        txtRazonSocial.Text = ""
        txtContacto.Text = ""
        txtEmailContacto.Text = ""
        txtTelefonoContacto.Text = ""
        txtCalle.Text = ""
        txtNumExterior.Text = ""
        txtNumInterior.Text = ""

        txtPais.Text = "México"
        txtMunicipio.Text = ""
        txtColonia.Text = ""
        txtCalle.Text = ""
        txtRFC.Text = ""

        FillRadComboBox(cmbEstados, "EXEC pCatalogos @cmd=1")

        cmbEstados.Text = "--Seleccione--"

        panelRegistroProspecto.Visible = True

    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim conn2 As New SqlConnection(Session("conexion"))


        Dim cmd2 As New SqlCommand("EXEC pCRMProspectos @cmd=8, @email_contacto='" & txtEmailContacto.Text & "',@razonsocial='" & txtRazonSocial.Text & "',@rfc='" & txtRFC.Text & "'", conn2)

        conn2.Open()

        Dim rs As SqlDataReader
        rs = cmd2.ExecuteReader()

        If rs.Read Then
            If rs("email_contacto") <> "" Then
                Session("email") = rs("email_contacto")
                Session("razonsocial") = rs("razonsocial")
                Session("rfc") = rs("rfc")
            End If
        End If
        conn2.Close()
        conn2.Dispose()


        Dim conn As New SqlConnection(Session("conexion"))

        ''Try


        If InsertOrUpdate.Value = 0 Then
            If Session("email") <> "" Then
                Session("email") = ""
                rwAlerta.RadAlert("El prospecto ya existe con este nombre:" & Session("razonsocial"), 330, 180, "Alert", "", "")

            Else
                Dim cmd As New SqlCommand("EXEC pCRMProspectos @cmd=4, @razonsocial='" & txtRazonSocial.Text & "', @contacto='" & txtContacto.Text & "', @email_contacto='" & txtEmailContacto.Text & "', @telefono_contacto='" & txtTelefonoContacto.Text & "', @calle='" & txtCalle.Text & "', @num_int='" & txtNumInterior.Text & "', @num_ext='" & txtNumExterior.Text & "', @colonia='" & txtColonia.Text & "', @pais='" & txtPais.Text & "', @municipio='" & txtMunicipio.Text & "', @estadoid='" & cmbEstados.SelectedValue & "', @cp='" & txtCP.Text & "', @rfc='" & txtRFC.Text & "', @userid='" & Session("userid").ToString() & "', @tipoempresaid='" & cmdTipoEmpresa.SelectedValue.ToString & "', @coberturaid='" & cmbCobertura.SelectedValue.ToString & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelRegistroProspecto.Visible = False

                prospectosList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
                prospectosList.DataSource = ObtenerProspectos()
                prospectosList.DataBind()

                conn.Close()
                conn.Dispose()



            End If
        Else

            Dim cmd As New SqlCommand("EXEC pCRMProspectos @cmd=5, @prospectoid='" & ProspectoID.Value & "', @razonsocial='" & txtRazonSocial.Text & "', @contacto='" & txtContacto.Text & "', @email_contacto='" & txtEmailContacto.Text & "', @telefono_contacto='" & txtTelefonoContacto.Text & "', @calle='" & txtCalle.Text & "', @num_int='" & txtNumInterior.Text & "', @num_ext='" & txtNumExterior.Text & "', @colonia='" & txtColonia.Text & "',  @pais='" & txtPais.Text & "', @municipio='" & txtMunicipio.Text & "', @estadoid='" & cmbEstados.SelectedValue & "', @cp='" & txtCP.Text & "', @rfc='" & txtRFC.Text & "', @userid='" & Session("userid").ToString() & "', @tipoempresaid='" & cmdTipoEmpresa.SelectedValue.ToString & "', @coberturaid='" & cmbCobertura.SelectedValue.ToString & "'", conn)

            conn.Open()

            cmd.ExecuteReader()

            panelRegistroProspecto.Visible = False

            prospectosList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
            prospectosList.DataSource = ObtenerProspectos()
            prospectosList.DataBind()

            conn.Close()
            conn.Dispose()
        End If

        ''Catch ex As Exception
        '    'Response.Write(ex.Message.ToString())
        ''Finally

        '    conn.Close()
        '    conn.Dispose()

        ''End 'Try
    End Sub
    Private Sub ClearItems()
        txtNombreContacto1.Text = ""
        txtEmailContacto1.Text = ""
        txtTelefonoContacto1.Text = ""
        txtPuestoContacto.Text = ""
        InsertOrUpdate2.Value = 0
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/crm/prospectos.aspx")
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        txtSearch.Text = ""
        prospectosList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
        prospectosList.DataSource = ObtenerProspectos()
        prospectosList.DataBind()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        prospectosList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
        prospectosList.DataSource = ObtenerProspectos()
        prospectosList.DataBind()
    End Sub

    Private Sub BtnSendEmailClick(sender As Object, e As EventArgs) Handles btnSendEmail.Click

        '
        '   Obtiene datos de la persona
        '
        Dim mensaje As String = ""
        '
        '
        mensaje = "<html><head></head><body style='font-size: 10pt; color: #000000; font-family: Verdana;'><br />"
        mensaje += txtMenssage.Text

        mensaje += "<br /><br />"
        mensaje += "Atentamente.<br /><br />"
        mensaje += "<strong>" & Session("nombre").ToString & "</strong><br /><br /></body></html>"

        Dim objMM As New MailMessage
        objMM.To.Add(txtTo.Text)
        objMM.From = New MailAddress(txtFrom.Text, Session("nombre"))
        If txtCC.Text.Length > 0 Then
            objMM.CC.Add(txtCC.Text)
        End If
        objMM.IsBodyHtml = True
        objMM.Priority = MailPriority.Normal
        objMM.Subject = txtSubject.Text
        objMM.Body = mensaje
        '
        '   Agrega anexos en caso de existir
        '
        Dim filePath = Server.MapPath("~/anexos/")
        Dim collection As IList(Of RadComboBoxItem) = anexosList.CheckedItems
        If (collection.Count <> 0) Then
            For Each item As RadComboBoxItem In collection
                Dim attachPdf As Net.Mail.Attachment = New Net.Mail.Attachment(filePath & item.Value.ToString)
                objMM.Attachments.Add(attachPdf)
            Next
        End If
        '
        Dim smtpMail As New SmtpClient
        'Try
        Dim smtpUser As New Net.NetworkCredential
        smtpUser.UserName = "enviosweb@linkium.mx"
        smtpUser.Password = "Link2020"
        smtpMail.UseDefaultCredentials = True
        smtpMail.Credentials = smtpUser
        smtpMail.Host = "smtp.linkium.mx"
        smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
        smtpMail.Send(objMM)
        '
        lblMensajeEmail.Text = "Mensaje enviado"
        '
        'Catch ex As Exception
        '
        '
        'Finally
        smtpMail = Nothing
        'End 'Try
        objMM = Nothing
        '

    End Sub
    Protected Sub Carga_Pestañas()
        If ProspectoID.Value = 0 Then
            RadTabStrip1.Tabs(0).Selected = True
            RadTabStrip1.Tabs(1).Enabled = False

        Else
            RadTabStrip1.Tabs(0).Selected = True
            RadTabStrip1.Tabs(1).Enabled = True

        End If
    End Sub

    Private Sub btnGuardarContacto_Click(sender As Object, e As EventArgs) Handles btnGuardarContacto.Click

        Dim conn As New SqlConnection(Session("conexion"))

        'Try
        If InsertOrUpdate2.Value = 0 Then
            Dim cmd As New SqlCommand("EXEC pCRMProspectos @cmd=9, @nombre='" & txtNombreContacto1.Text & "', @email_contacto1='" & txtEmailContacto1.Text & "',  @telefono_contacto1='" & txtTelefonoContacto1.Text & "', @puesto='" & txtPuestoContacto.Text & "', @prospectoid='" & ProspectoID.Value & "'", conn)

            conn.Open()

            cmd.ExecuteReader()

            'panelRegistroProspecto.Visible = False
            ClearItems()
            ContactoList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
            ContactoList.DataSource = ObtenerContacto()
            ContactoList.DataBind()

            conn.Close()
            conn.Dispose()
        Else

            Dim cmd As New SqlCommand("EXEC pCRMProspectos @cmd=10, @nombre='" & txtNombreContacto1.Text & "', @email_contacto1='" & txtEmailContacto1.Text & "',  @telefono_contacto1='" & txtTelefonoContacto1.Text & "', @puesto='" & txtPuestoContacto.Text & "', @prospectoid='" & ProspectoID2.Value & "'", conn)

            conn.Open()

            cmd.ExecuteReader()

            'panelRegistroProspecto.Visible = False
            ClearItems()
            ContactoList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
            ContactoList.DataSource = ObtenerContacto()
            ContactoList.DataBind()

            conn.Close()
            conn.Dispose()

        End If

        'Catch ex As Exception
        'Response.Write(ex.Message.ToString())
        'Finally

        conn.Close()
        conn.Dispose()

        'End 'Try
    End Sub

    Private Sub ContactoList_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles ContactoList.NeedDataSource
        ContactoList.MasterTableView.NoMasterRecordsText = "No se encontraron elementos para mostrar"
        ContactoList.DataSource = ObtenerContacto()
    End Sub

    Private Sub ContactoList_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles ContactoList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaContacto(e.CommandArgument)

            Case "cmdDelete"
                EliminaContacto(e.CommandArgument)
        End Select
    End Sub

    Private Sub btnCancelContacto_Click(sender As Object, e As EventArgs) Handles btnCancelContacto.Click
        Response.Redirect("~/crm/prospectos.aspx")
    End Sub

    Private Sub ContactoList_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles ContactoList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Contactos" Then



                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea borrar de la base de datos a este Contacto?');")
                If Session("perfilid") > 1 Then
                    lnkdel.Enabled = False
                    lnkdel.ToolTip = "Función no autorizada."
                End If
            End If

        End If
    End Sub
End Class