Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Web.UI.RadNumericTextBox
Imports System.IO
Imports System.Net.Mail
Imports Telerik.Reporting.Processing
Imports Formatos


Public Class CRMcotizaciones
    Inherits System.Web.UI.Page
    Private importe As Decimal = 0
    Private descuento As Decimal = 0
    Private subtotal As Decimal = 0
    Private iva As Decimal = 0
    Private total As Decimal = 0
    Dim Valido As Boolean = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(cmbClienteProspecto, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by UPPER(isnull(razonsocial,'')) asc", 0)
            ObjData.Catalogo(cmbOportunidades, "select id, isnull(descripcion,'') as descripcion from tblCRMOportunidad where etapaid in (1,2,3) and clienteid='" & cmbClienteProspecto.SelectedValue & "'", 0)
            ObjData.Catalogo(cmbMoneda, "select id, nombre from tblMoneda order by id", 0)
            ObjData.Catalogo(cmbCondiciones, "select id, nombre from tblCondiciones", 0)
            ObjData.Catalogo(cmbResponsable, "exec pCatalogos @cmd=5", 0, True)
            ObjData = Nothing
            If Session("perfilid") <> 1 Then
                Valido = FnValido()
                If Valido <> 0 Then
                    cmbResponsable.SelectedValue = Session("userid")
                    cmbResponsable.Enabled = True
                Else
                    cmbResponsable.SelectedValue = Session("userid")
                    cmbResponsable.Enabled = False
                End If
            Else
                cmbResponsable.Enabled = True
            End If
            '
        End If
    End Sub

    Function ObtenerCotizaciones() As DataSet
        Dim sql As String = ""
        If Session("perfilid") <> 1 Then
            Valido = FnValido()
            If Valido <> 0 Then
                sql = "EXEC pCRMCotizacion @cmd=1, @txtSearch='" & txtSearch.Text & "', @userid='" & cmbResponsable.SelectedValue & "', @userseccionid='" & Session("userid").ToString() & "'"
            Else
                sql = "EXEC pCRMCotizacion @cmd=1, @txtSearch='" & txtSearch.Text & "', @userid='" & Session("userid").ToString() & "', @userseccionid='" & Session("userid").ToString() & "'"
            End If
        Else
            sql = "EXEC pCRMCotizacion @cmd=1, @txtSearch='" & txtSearch.Text & "', @userid='" & cmbResponsable.SelectedValue & "', @userseccionid='" & Session("userid").ToString() & "'"
        End If
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
    Private Sub cmbResponsable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbResponsable.SelectedIndexChanged
        RadWindow1.VisibleOnPageLoad = False
        cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron oportunidades para mostrar"
        cotizacionesList.DataSource = ObtenerCotizaciones()
        cotizacionesList.DataBind()
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/crm/cotizaciones.aspx")
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        RadWindow1.VisibleOnPageLoad = False
        Try

            Dim FileName As String = ""
            Dim currentTime As System.DateTime = System.DateTime.Now

            If cotizacionAnexo.FileContent.Length > 0 Then
                FileName = currentTime.Year.ToString & currentTime.Month.ToString & currentTime.Day.ToString & currentTime.Hour.ToString & currentTime.Minute.ToString & currentTime.Second.ToString & currentTime.Millisecond.ToString & "_" & cotizacionAnexo.FileName
                cotizacionAnexo.SaveAs(Server.MapPath("~/crm/cotizaciones/") & FileName)
            Else
                FileName = lnkDownload.Text
            End If

            If InsertOrUpdate.Value = 0 Then

                Dim sql As String = ""
                If rblClienteProspecto.SelectedValue = 0 Then
                    sql = "EXEC pCRMCotizacion @cmd=4, @userid='" & Session("userid").ToString & "', @clienteid='" & cmbClienteProspecto.SelectedValue.ToString & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @tipocambio='" & txtTipoCambio.Text & "', @condicionesid='" & cmbCondiciones.SelectedValue.ToString & "', @comentarios='" & txtComentarios.Text & "', @oportunidadid='" & cmbOportunidades.SelectedValue.ToString & "', @anexo='" & FileName.ToString & "'"
                Else
                    sql = "EXEC pCRMCotizacion @cmd=4, @userid='" & Session("userid").ToString & "', @prospectoid='" & cmbClienteProspecto.SelectedValue.ToString & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @tipocambio='" & txtTipoCambio.Text & "', @condicionesid='" & cmbCondiciones.SelectedValue.ToString & "', @comentarios='" & txtComentarios.Text & "', @porcentaje_descuento='" & txtPorcentajeDescuento.Text & "', @oportunidadid='" & cmbOportunidades.SelectedValue.ToString & "', @anexo='" & FileName.ToString & "'"
                End If

                Dim ObjData As New DataControl(1)
                CotizacionID.Value = ObjData.RunSQLScalarQuery(sql)
                ObjData = Nothing

                panelRegistroCotizacion.Visible = True

                cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron cotizaciones para mostrar"
                cotizacionesList.DataSource = ObtenerCotizaciones()
                cotizacionesList.DataBind()

            Else

                Dim sql As String = ""
                If rblClienteProspecto.SelectedValue = 0 Then
                    sql = "EXEC pCRMCotizacion @cmd=5, @cotizacionid='" & CotizacionID.Value & "', @userid='" & Session("userid").ToString & "', @clienteid='" & cmbClienteProspecto.SelectedValue.ToString & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @tipocambio='" & txtTipoCambio.Text & "', @condicionesid='" & cmbCondiciones.SelectedValue.ToString & "', @comentarios='" & txtComentarios.Text & "', @oportunidadid='" & cmbOportunidades.SelectedValue.ToString & "', @anexo='" & FileName.ToString & "'"
                Else
                    sql = "EXEC pCRMCotizacion @cmd=5, @cotizacionid='" & CotizacionID.Value & "', @userid='" & Session("userid").ToString & "', @prospectoid='" & cmbClienteProspecto.SelectedValue.ToString & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @tipocambio='" & txtTipoCambio.Text & "', @condicionesid='" & cmbCondiciones.SelectedValue.ToString & "', @comentarios='" & txtComentarios.Text & "', @porcentaje_descuento='" & txtPorcentajeDescuento.Text & "', @oportunidadid='" & cmbOportunidades.SelectedValue.ToString & "', @anexo='" & FileName.ToString & "'"
                End If

                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery(sql)
                ObjData = Nothing

                panelRegistroCotizacion.Visible = True

            End If
            Response.Redirect("~/crm/cotizaciones.aspx")
        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally
        End Try
    End Sub

    Protected Sub btnAgregarCotizacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarCotizacion.Click
        InsertOrUpdate.Value = 0
        panelRegistroCotizacion.Visible = True
        RadWindow1.VisibleOnPageLoad = False
        Dim ObjData As New DataControl(1)
        ObjData.Catalogo(cmbClienteProspecto, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by razonsocial", 0)
        ObjData.Catalogo(cmbMoneda, "select id, nombre from tblMoneda order by id", 0)
        ObjData.Catalogo(cmbCondiciones, "select id, nombre from tblCondiciones", 0)
        ObjData = Nothing
        rblClienteProspecto.SelectedValue = 0
    End Sub

    Protected Sub rblClienteProspecto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblClienteProspecto.SelectedIndexChanged
        RadWindow1.VisibleOnPageLoad = False
        Dim ObjData As New DataControl(1)
        If rblClienteProspecto.SelectedValue = 0 Then
            lblPorcentajeDescuento.Visible = False
            txtPorcentajeDescuento.Visible = False
            ObjData.Catalogo(cmbClienteProspecto, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by razonsocial", 0)
            ObjData.Catalogo(cmbOportunidades, "select id, isnull(descripcion,'') as descripcion from tblCRMOportunidad where etapaid in (1,2,3) and clienteid='" & cmbClienteProspecto.SelectedValue & "'", 0)
        Else
            lblPorcentajeDescuento.Visible = True
            txtPorcentajeDescuento.Visible = True
            ObjData.Catalogo(cmbClienteProspecto, "select id, isnull(razonsocial,'') as razonsocial from tblCRMProspecto order by isnull(razonsocial,'')", 0)
            ObjData.Catalogo(cmbOportunidades, "select id, isnull(descripcion,'') as descripcion from tblCRMOportunidad where etapaid in (1,2,3) and prospectoid='" & cmbClienteProspecto.SelectedValue & "'", 0)
        End If
        ObjData = Nothing
    End Sub

    Protected Sub cotizacionesList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles cotizacionesList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaCotizacion(e.CommandArgument)
            Case "cmdDelete"
                EliminaCotizacion(e.CommandArgument)
            Case "cmdSend"
                Call DatosEmail(e.CommandArgument)
                'Try
                '    SendEmail(e.CommandArgument)
                '    lblMensajeGuardar.Text = "Cotización enviada exitosamente"
                '    lblMensajeGuardar.ForeColor = Drawing.Color.Green
                '    Dim script As String = "<script type='text/javascript'>mensaje();</script>"
                '    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", script, False)
                'Catch ex As Exception
                '    lblMensajeGuardar.Text = "Error: " & ex.Message.ToString
                '    lblMensajeGuardar.ForeColor = Drawing.Color.Red
                '    Dim script As String = "<script type='text/javascript'>mensaje();</script>"
                '    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", script, False)
                'End Try
            Case "cmdDownload"
                Call DescargaAnexo(e.CommandArgument)

        End Select
    End Sub
    Private Sub DatosEmail(ByVal cotizacionid As Long)
        '
        '
        tempcfdid.Value = cotizacionid
        '
        '
        '   Obtiene datos de la persona
        '
        Dim mensaje As String = ""
        Dim razonsocial As String = ""
        Dim correo As String = ""
        Dim email_from As String = ""
        Dim email_smtp_server As String = ""
        Dim email_smtp_username As String = ""
        Dim email_smtp_password As String = ""
        Dim email_smtp_port As String = ""
        '
        Dim conn As New SqlConnection(Session("conexion"))
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pCRMCotizacion @cmd=13, @cotizacionid='" & cotizacionid.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            '
            razonsocial = rs("razon_social")
            correo = rs("email_to")
            email_from = rs("email_from")
            '
            'razonsocial = rs("razon_social")
            'email_from = rs("email_from")
            'email_to = rs("email_to")
            'email_anexo = rs("email_anexo")
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
        '
        '
        'mensaje = "<html><head></head><body style='font-size: 10pt; color: #000000; font-family: Verdana;'><br />"
        mensaje += "Estimado(a) Cliente, por este medio se le anexa la cotización solicitada." & vbCrLf & vbCrLf & "Gracias por su preferencia." & vbCrLf & vbCrLf

        'mensaje += "<br /><br />"
        mensaje += "Atentamente." & vbCrLf & vbCrLf
        mensaje += razonsocial.ToString.ToUpper

        'mensaje = "Estimado(a) Cliente, por este medio se le anexa la factura solicitada, la cual sirve como comprobante fiscal ante Secretaría de Hacienda y Crédito Público." & vbCrLf & vbCrLf & "Gracias por su preferencia." & vbCrLf & vbCrLf
        'mensaje += "Atentamente." & vbCrLf & vbCrLf
        'mensaje += razonsocial.ToString.ToUpper

        lblMensajeEmail.Text = ""
        txtFrom.Text = email_from.ToString
        txtTo.Text = correo.ToString
        txtSubject.Text = razonsocial & " - Cotización No. " & cotizacionid.ToString
        txtMenssage.Text = mensaje.ToString

        RadWindow1.VisibleOnPageLoad = True

    End Sub
    Private Sub EditaCotizacion(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCRMCotizacion @cmd=2, @cotizacionid='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtPorcentajeDescuento.Enabled = False
                txtPorcentajeDescuento.Text = rs("porcentaje_descuento")
                txtComentarios.Text = rs("comentarios")
                rblClienteProspecto.SelectedValue = rs("tipo_cliente")

                Dim ObjData As New DataControl(1)
                If rblClienteProspecto.SelectedValue = 0 Then
                    lblPorcentajeDescuento.Visible = False
                    txtPorcentajeDescuento.Visible = False
                    ObjData.Catalogo(cmbClienteProspecto, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by UPPER(isnull(razonsocial,'')) asc", rs("clienteid"))
                    ObjData.Catalogo(cmbOportunidades, "select id, isnull(descripcion,'') as descripcion from tblCRMOportunidad where etapaid in (1,2,3) and clienteid='" & rs("clienteid") & "'", rs("oportunidadid"))
                Else
                    lblPorcentajeDescuento.Visible = True
                    txtPorcentajeDescuento.Visible = True
                    ObjData.Catalogo(cmbClienteProspecto, "select id, isnull(razonsocial,'') as razonsocial from tblCRMProspecto order by UPPER(isnull(razonsocial,'')) asc", rs("prospectoid"))
                    ObjData.Catalogo(cmbOportunidades, "select id, isnull(descripcion,'') as descripcion from tblCRMOportunidad where etapaid in (1,2,3) and prospectoid='" & rs("prospectoid") & "'", rs("oportunidadid"))
                End If

                ObjData.Catalogo(cmbCondiciones, "select id, nombre from tblCondiciones", rs("condicionesid"))
                ObjData.Catalogo(cmbMoneda, "select id, nombre from tblMoneda order by id", rs("monedaid"))

                If cmbMoneda.SelectedValue = 1 Or cmbMoneda.SelectedValue = 0 Then
                    lblTipoCambio.Visible = False
                    txtTipoCambio.Visible = False
                Else
                    lblTipoCambio.Visible = True
                    txtTipoCambio.Visible = True
                End If
                txtTipoCambio.Text = rs("tipocambio")
                If Not String.IsNullOrEmpty(rs("anexo")) Then
                    lnkDownload.Text = rs("anexo")
                    lnkDownload.Visible = True
                    lnkDeleteFile.Visible = True
                    lnkDeleteFile.Attributes.Add("onclick", "return confirm('Va a eliminar el archivo anexo. ¿Desea continuar?');")
                Else
                    lnkDownload.Text = ""
                    lnkDownload.Visible = False
                    lnkDeleteFile.Visible = False
                End If
                ObjData = Nothing

                panelRegistroCotizacion.Visible = True

                InsertOrUpdate.Value = 1
                CotizacionID.Value = id

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub

    Private Sub EliminaCotizacion(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCRMCotizacion @cmd=3, @cotizacionid='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Response.Redirect("~/crm/cotizaciones.aspx")

    End Sub

    Private Sub SendEmail(ByVal cotizacionid As Long)
        '
        '
        '   Obtiene datos de la persona
        '
        Dim mensaje As String = ""
        Dim razonsocial As String = ""
        Dim email_to As String = ""
        Dim email_from As String = ""
        Dim email_anexo As String = ""
        '
        Dim conn As New SqlConnection(Session("conexion"))
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pCRMCotizacion @cmd=13, @cotizacionid='" & cotizacionid.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            '       
            razonsocial = rs("razon_social")
            email_from = rs("email_from")
            email_to = rs("email_to")
            email_anexo = rs("email_anexo")
            '
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
        '
        '
        mensaje = "<html><head></head><body style='font-size: 10pt; color: #000000; font-family: Verdana;'><br />"
        mensaje += "Estimado(a) Cliente, por este medio se le anexa la cotización solicitada.<br /><br />Gracias por su preferencia."

        mensaje += "<br /><br />"
        mensaje += "Atentamente.<br /><br />"
        mensaje += "<strong>" & razonsocial.ToString & "</strong><br /><br /></body></html>"

        Dim objMM As New MailMessage
        objMM.To.Add(email_to)
        objMM.From = New MailAddress(email_from, razonsocial)
        objMM.IsBodyHtml = True
        objMM.Priority = MailPriority.Normal
        objMM.Subject = razonsocial & " - Cotización No. " & cotizacionid.ToString
        objMM.Body = mensaje
        '
        '   Agrega anexos en caso de existir
        '
        If email_anexo.Length > 0 Then
            Dim FilePath = Server.MapPath("~/crm/cotizaciones/") & email_anexo
            Dim AttachPDF As Net.Mail.Attachment = New Net.Mail.Attachment(FilePath)
            objMM.Attachments.Add(AttachPDF)
        End If
        '
        Dim SmtpMail As New SmtpClient
        Try
            Dim SmtpUser As New Net.NetworkCredential
            SmtpUser.UserName = "enviosweb@linkium.mx"
            SmtpUser.Password = "Link2020"
            SmtpMail.UseDefaultCredentials = False
            SmtpMail.Credentials = SmtpUser
            SmtpMail.Host = "smtp.linkium.mx"
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
            SmtpMail.Send(objMM)
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            SmtpMail = Nothing
        End Try
        objMM = Nothing
    End Sub

    Private Sub DescargaAnexo(ByVal cotizacionid As Long)
        '
        '
        Dim nombreanexo As String = ""
        Dim conn As New SqlConnection(Session("conexion"))
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pCRMCotizacion @cmd=16, @cotizacionid='" & cotizacionid.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            nombreanexo = rs("anexo")
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
        Dim path As String = Server.MapPath("~/crm/cotizaciones/" & nombreanexo) 'get file object as FileInfo
        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
        '
        '
        If file.Exists Then 'set appropriate headers
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(file.FullName)
            Response.End() 'if file does not exist
        End If
    End Sub

    Protected Sub cotizacionesList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles cotizacionesList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Cotizaciones" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea eliminar de la base de datos esta cotización?');")

                Dim btnDownload As ImageButton = CType(e.Item.FindControl("btnDownload"), ImageButton)
                Dim imgSend As ImageButton = CType(e.Item.FindControl("imgSend"), ImageButton)
                'imgSend.Attributes.Add("onclick", "javascript: return confirm('Va a enviar la cotización al prospecto por correo. ¿Desea continuar?');")
                lnkdel.Enabled = e.Item.DataItem("Activo")

                If e.Item.DataItem("enviadoBit") = True Then
                    imgSend.ImageUrl = "~/images/envelopeok.jpg"
                    'imgSend.ToolTip = "Enviado el " & e.Item.DataItem("fechaenvio").ToString
                End If

                If e.Item.DataItem("anexo").ToString.Length > 0 Then
                    btnDownload.Visible = True
                    imgSend.Visible = True
                Else
                    btnDownload.Visible = False
                    imgSend.Visible = False
                End If

            End If

        End If
    End Sub

    Protected Sub cotizacionesList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles cotizacionesList.NeedDataSource
        cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron cotizaciones para mostrar"
        cotizacionesList.DataSource = ObtenerCotizaciones()
    End Sub

    Protected Sub cmbMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMoneda.SelectedIndexChanged
        RadWindow1.VisibleOnPageLoad = False
        If cmbMoneda.SelectedValue = 1 Or cmbMoneda.SelectedValue = 0 Then
            lblTipoCambio.Visible = False
            txtTipoCambio.Visible = False
        Else
            lblTipoCambio.Visible = True
            txtTipoCambio.Visible = True
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        panelRegistroCotizacion.Visible = False
        RadWindow1.VisibleOnPageLoad = False
        cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron cotizaciones para mostrar"
        cotizacionesList.DataSource = ObtenerCotizaciones()
        cotizacionesList.DataBind()
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        'panelRegistroCotizacion.Visible = False
        'txtSearch.Text = ""
        'cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron cotizaciones para mostrar"
        'cotizacionesList.DataSource = ObtenerCotizaciones()
        txtSearch.Text = ""
        panelRegistroCotizacion.Visible = False
        RadWindow1.VisibleOnPageLoad = False
        cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron oportunidades para mostrar"
        Dim ObjData As New DataControl(1)
        If Session("perfilid") <> 1 Then
            Valido = FnValido()
            If Valido <> 0 Then
                ObjData.Catalogo(cmbResponsable, "exec pCatalogos @cmd=5", 0, True)
            End If
        Else
            ObjData.Catalogo(cmbResponsable, "exec pCatalogos @cmd=5", 0, True)
        End If
        'ObjData.Catalogo(cmbResponsable, "select id, nombre from tblusuario where isnull(borradoBit,0)=0 and id='" & Session("userid").ToString & "' ", 1)
        ObjData = Nothing
        'ObjData.Catalogo(cmbClienteProspecto, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by razonsocial", rs("clienteid"))
        cotizacionesList.DataSource = ObtenerCotizaciones()
        cotizacionesList.DataBind()
    End Sub

    Protected Sub cmbClienteProspecto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClienteProspecto.SelectedIndexChanged
        RadWindow1.VisibleOnPageLoad = False
        Dim ObjData As New DataControl(1)
        If rblClienteProspecto.SelectedValue = 0 Then
            ObjData.Catalogo(cmbOportunidades, "select id, isnull(descripcion,'') as descripcion from tblCRMOportunidad where etapaid in (1,2,3) and clienteid='" & cmbClienteProspecto.SelectedValue & "'", 0)
        Else
            ObjData.Catalogo(cmbOportunidades, "select id, isnull(descripcion,'') as descripcion from tblCRMOportunidad where etapaid in (1,2,3) and prospectoid='" & cmbClienteProspecto.SelectedValue & "'", 0)
        End If
        ObjData = Nothing
    End Sub

    Private Sub lnkDeleteFile_Click(sender As Object, e As EventArgs) Handles lnkDeleteFile.Click
        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("exec pCRMCotizacion @cmd=15, @cotizacionid='" & CotizacionID.Value.ToString & "'")
        ObjData = Nothing
        lnkDownload.Text = ""
        lnkDownload.Visible = False
        lnkDeleteFile.Visible = False
    End Sub

    Private Sub lnkDownload_Click(sender As Object, e As EventArgs) Handles lnkDownload.Click
        '
        '
        Dim nombreanexo As String = ""


        Dim conn As New SqlConnection(Session("conexion"))
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pCRMCotizacion @cmd=16, @cotizacionid='" & CotizacionID.Value.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            nombreanexo = rs("anexo")
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
        Dim path As String = Server.MapPath("~/crm/cotizaciones/" & nombreanexo) 'get file object as FileInfo
        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
        '
        '
        If file.Exists Then 'set appropriate headers
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(file.FullName)
            Response.End() 'if file does not exist
        End If
    End Sub
    Private Function FnValido() As Boolean
        Dim Valido As Boolean = 0
        Dim conn As New SqlConnection(Session("conexion"))
        Try

            Dim cmd As New SqlCommand("EXEC pPermisosEspeciales @cmd=1, @IdPerfil='" & Session("perfilid").ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Valido = rs("bitCRM_ShowAllagents")
            Else
                Valido = 0
            End If

        Catch ex As Exception
            'Response.Write(ex.Message.ToString())
            Valido = 0
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return Valido
    End Function
    Private Sub btnSendEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click

        If txtTo.Text = "" Then
            lblMensajeEmail.Text = "Proporciona un correo válido para el destino. "
            Return
        End If
        '
        '
        '   Obtiene datos de la persona
        '
        Dim mensaje As String = ""
        Dim razonsocial As String = ""
        Dim email_to As String = ""
        Dim email_from As String = ""
        Dim email_anexo As String = ""
        '
        Dim conn As New SqlConnection(Session("conexion"))
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pCRMCotizacion @cmd=13, @cotizacionid='" & tempcfdid.Value.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            '       
            razonsocial = rs("razon_social")
            email_from = rs("email_from")
            email_to = rs("email_to")
            email_anexo = rs("email_anexo")
            '
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing

        Dim delimit As Char() = New Char() {";"c, ","c}
        Dim novalidos As String = ""

        Dim objMM As New MailMessage

        For Each splitTo As String In txtCC.Text.Trim().Split(delimit)
            If validarEmail(splitTo.Trim()) Then
                objMM.CC.Add(splitTo.Trim())
            Else

                If novalidos = "" Then
                    novalidos += splitTo.Trim()
                Else
                    novalidos += "," & splitTo.Trim()
                End If

            End If
        Next

        If novalidos = "" Then
            'objMM.To.Add(email_from)
            objMM.To.Add(txtTo.Text.Trim())
            objMM.Bcc.Add("cotizaciones@humantop.mx")
            objMM.From = New MailAddress(txtFrom.Text, razonsocial)
            objMM.IsBodyHtml = False
            objMM.Priority = MailPriority.Normal
            objMM.Subject = txtSubject.Text
            objMM.Body = txtMenssage.Text

            '
            '   Agrega anexos en caso de existir
            '
            If email_anexo.Length > 0 Then
                Dim FilePath = Server.MapPath("~/crm/cotizaciones/") & email_anexo
                Dim AttachPDF As Net.Mail.Attachment = New Net.Mail.Attachment(FilePath)
                objMM.Attachments.Add(AttachPDF)
            End If

            '
            Dim SmtpMail As New SmtpClient
            Try
                Dim SmtpUser As New Net.NetworkCredential
                SmtpUser.UserName = "enviosweb@linkium.mx"
                SmtpUser.Password = "Link2020"
                SmtpMail.UseDefaultCredentials = False
                SmtpMail.Credentials = SmtpUser
                SmtpMail.Host = "smtp.linkium.mx"
                SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
                SmtpMail.Send(objMM)
                '
                '   Lo marca como enviado
                Dim ObjData As New DataControl
                ObjData.RunSQLQuery("exec pCRMCotizacion @cmd=17, @cotizacionid='" & tempcfdid.Value.ToString & "'")
                ObjData = Nothing

            Catch ex As Exception
                '
                Response.Write(ex.ToString)
                'Response.End()
                '
            Finally
                SmtpMail = Nothing
            End Try
            objMM = Nothing
            '
            '
            'Call MuestraLista()
            RadWindow1.VisibleOnPageLoad = False
            '
            ''
        Else
            lblMensajeEmail.Text = "Formato de correo no válido: " & novalidos
        End If
    End Sub
    Public Shared Function validarEmail(ByVal email As String) As Boolean
        Dim expresion As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        If Regex.IsMatch(email, expresion) Then
            If Regex.Replace(email, expresion, String.Empty).Length = 0 Then
                Return True
            Else
                Return False
            End If

        Else
            Return False
        End If

    End Function
    Public Sub b()
        '
        '
        '   Obtiene datos de la persona
        '
        Dim mensaje As String = ""
        Dim razonsocial As String = ""
        Dim email_to As String = ""
        Dim email_from As String = ""
        Dim email_anexo As String = ""
        '
        Dim conn As New SqlConnection(Session("conexion"))
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pCRMCotizacion @cmd=13, @cotizacionid='" & CotizacionID.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            '       
            razonsocial = rs("razon_social")
            email_from = rs("email_from")
            email_to = rs("email_to")
            email_anexo = rs("email_anexo")
            '
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
        '
        '
        mensaje = "<html><head></head><body style='font-size: 10pt; color: #000000; font-family: Verdana;'><br />"
        mensaje += "Estimado(a) Cliente, por este medio se le anexa la cotización solicitada.<br /><br />Gracias por su preferencia."

        mensaje += "<br /><br />"
        mensaje += "Atentamente.<br /><br />"
        mensaje += "<strong>" & razonsocial.ToString & "</strong><br /><br /></body></html>"

        Dim objMM As New MailMessage
        objMM.To.Add(email_to)
        objMM.From = New MailAddress(email_from, razonsocial)
        objMM.IsBodyHtml = True
        objMM.Priority = MailPriority.Normal
        objMM.Subject = razonsocial & " - Cotización No. " & CotizacionID.ToString
        objMM.Body = mensaje
        '
        '   Agrega anexos en caso de existir
        '
        If email_anexo.Length > 0 Then
            Dim FilePath = Server.MapPath("~/crm/cotizaciones/") & email_anexo
            Dim AttachPDF As Net.Mail.Attachment = New Net.Mail.Attachment(FilePath)
            objMM.Attachments.Add(AttachPDF)
        End If
        '
        Dim SmtpMail As New SmtpClient
        Try
            Dim SmtpUser As New Net.NetworkCredential
            SmtpUser.UserName = "enviosweb@linkium.mx"
            SmtpUser.Password = "Link2020"
            SmtpMail.UseDefaultCredentials = False
            SmtpMail.Credentials = SmtpUser
            SmtpMail.Host = "smtp.linkium.mx"
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
            SmtpMail.Send(objMM)
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            SmtpMail = Nothing
        End Try
        objMM = Nothing
    End Sub


End Class