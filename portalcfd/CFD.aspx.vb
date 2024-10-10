Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Threading
Imports System.Xml
Imports System.Net.Mail
Imports ThoughtWorks.QRCode
Imports ThoughtWorks.QRCode.Codec
Imports Telerik.Reporting.Processing
Imports System.Web.Services.Protocols

Partial Class portalcfd_CFD
    Inherits System.Web.UI.Page

    Private totaldescuento As Decimal = 0
    Private tipocontribuyenteid As Integer = 0
    Private RFCEmisor As String = ""
    Private uuids As New List(Of String)()
    Private qrBackColor As Integer = System.Drawing.Color.FromArgb(255, 255, 255, 255).ToArgb
    Private qrForeColor As Integer = System.Drawing.Color.FromArgb(255, 0, 0, 0).ToArgb

#Region "Load Initial Values"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.Server.ScriptTimeout = 3600
        Me.Title = Resources.Resource.WindowsTitle
        If Not IsPostBack Then
            fha_ini.SelectedDate = Now()
            fha_fin.SelectedDate = Now()
            Dim objCat As New DataControl(1)
            ObjCat.Catalogo(tipoid, "exec pCatalogos @cmd=3", 0)
            objCat.Catalogo(clienteid, "exec pMisClientes @cmd=1", 0)
            objCat.Catalogo(cmbMotivoCancela, "select isnull(clave,'') as clave, isnull(motivo,'') as motivo from tblCFD_MotivoCancelacion order by clave", "02")
            objCat = Nothing
            Call MuestraLista()
            Call SetGridFilters()
        End If
    End Sub

    Private Sub MuestraLista()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        cfdlist.DataSource = ObjData.FillDataSet("exec pCFD @cmd=8, @tipodocumentoid='" & tipoid.SelectedValue.ToString & "', @clienteid='" & clienteid.SelectedValue.ToString & "', @fhaini='" & fha_ini.SelectedDate.Value.ToShortDateString & "', @fhafin='" & fha_fin.SelectedDate.Value.ToShortDateString & "'")
        cfdlist.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub
#End Region

#Region "Grid Handle"
    Protected Sub cfdlist_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles cfdlist.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/portalcfd/Facturar40.aspx?id=" & e.CommandArgument.ToString)
            Case "cmdXML"
                WinCancel.VisibleOnPageLoad = False
                Call DownloadXML(e.CommandArgument)
            Case "cmdPDF"
                WinCancel.VisibleOnPageLoad = False
                Call DownloadPDF(e.CommandArgument)
            Case "cmdSend"
                Call SendEmail(e.CommandArgument)
            Case "cmdPDFsv"
                Response.Redirect("~/portalcfd/FacturaPDFsinValor.aspx?id=" & e.CommandArgument.ToString)
            Case "cmdDelete"
                WinCancel.VisibleOnPageLoad = False
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pCFD @cmd=9, @cfdid='" & e.CommandArgument.ToString & "'")
                ObjData = Nothing
                Call MuestraLista()
            Case "cmdCancel"
                Call CancelaCFDI(e.CommandArgument)
            Case "cmdAcuse"
                WinCancel.VisibleOnPageLoad = False
                Call VerAcuse(e.CommandArgument)
        End Select
    End Sub

    Protected Sub cfdlist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles cfdlist.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim lnkEdit As LinkButton = CType(e.Item.FindControl("lnkEdit"), LinkButton)
                Dim lnkCancelar As LinkButton = CType(e.Item.FindControl("lnkCancelar"), LinkButton)
                Dim lnkAcuse As LinkButton = CType(e.Item.FindControl("lnkAcuse"), LinkButton)
                Dim lnkBorrar As LinkButton = CType(e.Item.FindControl("lnkBorrar"), LinkButton)
                Dim lnkXML As LinkButton = CType(e.Item.FindControl("lnkXML"), LinkButton)
                Dim lnkPDF As LinkButton = CType(e.Item.FindControl("lnkPDF"), LinkButton)
                Dim lblTimbrado As Label = CType(e.Item.FindControl("lblTimbrado"), Label)
                Dim imgTimbrado As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgTimbrado"), System.Web.UI.WebControls.Image)
                Dim imgSend As ImageButton = CType(e.Item.FindControl("imgSend"), ImageButton)

                imgTimbrado.Visible = e.Item.DataItem("timbrado")

                If Not e.Item.DataItem("timbrado") Then
                    lblTimbrado.Text = " "
                    If e.Item.DataItem("folio") = 0 Then
                        lnkEdit.Enabled = True
                    Else
                        lnkEdit.Enabled = False
                    End If
                End If

                If e.Item.DataItem("formatoBit") Then
                    lnkEdit.Enabled = Not e.Item.DataItem("formatoBit")
                    lnkXML.Enabled = Not e.Item.DataItem("formatoBit")
                    lnkPDF.Enabled = e.Item.DataItem("formatoBit")
                    imgSend.Enabled = e.Item.DataItem("formatoBit")
                    lnkCancelar.Enabled = Not e.Item.DataItem("formatoBit")
                    lnkBorrar.Visible = e.Item.DataItem("formatoBit")
                Else
                    lnkEdit.Enabled = Not e.Item.DataItem("timbrado")
                    lnkXML.Enabled = e.Item.DataItem("timbrado")
                    lnkPDF.Enabled = e.Item.DataItem("timbrado")
                    imgSend.Enabled = e.Item.DataItem("timbrado")
                    lnkCancelar.Enabled = e.Item.DataItem("timbrado")
                    lnkBorrar.Visible = Not e.Item.DataItem("timbrado")
                End If

                If e.Item.DataItem("enviadoBit") = True Then
                    imgSend.ImageUrl = "~/portalcfd/images/envelopeok.jpg"
                    imgSend.ToolTip = "Enviado el " & e.Item.DataItem("fechaenvio").ToString
                End If

                lnkBorrar.Attributes.Add("onclick", "javascript:return confirm('Va a borrar un folio no timbrado. ¿Desea continuar?');")
                imgSend.Attributes.Add("onclick", "javascript:return confirm('Va a enviar por correo los archivos del cfdi. ¿Desea continuar?');")

                If e.Item.DataItem("estatus") = "Aplicado" Or e.Item.DataItem("estatus") = "Cancelado" Then
                    lnkCancelar.Visible = True
                Else
                    lnkCancelar.Visible = False
                End If
                If (e.Item.DataItem("estatus") = "Cancelado") Then
                    lnkCancelar.Visible = False
                    lnkAcuse.Visible = True
                    imgSend.Visible = False
                Else
                    lnkAcuse.Visible = False
                End If
        End Select
    End Sub

    Protected Sub cfdlist_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles cfdlist.PageIndexChanged
        cfdlist.CurrentPageIndex = e.NewPageIndex
        Call MuestraLista()
    End Sub

    Protected Sub cfdlist_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles cfdlist.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        cfdlist.DataSource = ObjData.FillDataSet("exec pCFD @cmd=8, @tipodocumentoid='" & tipoid.SelectedValue.ToString & "', @clienteid='" & clienteid.SelectedValue.ToString & "', @fhaini='" & fha_ini.SelectedDate.Value.ToShortDateString & "', @fhafin='" & fha_fin.SelectedDate.Value.ToShortDateString & "'")
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        ''
    End Sub
#End Region

#Region "Functions"
    Private Sub DownloadXML(ByVal cfdid As Long)
        Dim uuid As String = ""
        Dim connF As New SqlConnection(Session("conexion"))
        Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                uuid = rs("uuid").ToString
            End If
        Catch ex As Exception
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Dim FilePath = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml"
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If
        '
    End Sub

    Private Sub DownloadPDF(ByVal cfdid As Long)
        Dim uuid As String = ""
        Dim connF As New SqlConnection(Session("conexion"))
        Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                uuid = rs("uuid").ToString
            End If
        Catch ex As Exception
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try

        Dim FilePath = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/pdf/") & uuid.ToString & ".pdf"
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        Else
            GuardaPDF(GeneraPDF(cfdid), Server.MapPath("~/clientes/" + Session("appkey").ToString + "/pdf/") & uuid.ToString & ".pdf")
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If

    End Sub

    Private Sub SendEmail(ByVal cfdid As Long)
        Dim uuid As String = ""
        Dim connF As New SqlConnection(Session("conexion"))
        Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", connF)
        Try

            connF.Open()

            Dim rsF = cmdF.ExecuteReader

            If rsF.Read Then
                uuid = rsF("uuid").ToString
            End If
        Catch ex As Exception
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
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
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pEnviaEmail @cfdid='" & cfdid.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            '       
            razonsocial = rs("razonsocial")
            correo = rs("email_to")
            email_from = rs("email_from")
            email_smtp_server = rs("email_smtp_server")
            email_smtp_username = rs("email_smtp_username")
            email_smtp_password = rs("email_smtp_password")
            email_smtp_port = rs("email_smtp_port")
            '
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
        '
        '
        mensaje = "<html><head></head><body><br />"
        mensaje += "Estimado(a) Cliente, por este medio se le anexa la factura solicitada, la cual sirve como comprobante fiscal ante Hacienda.<br /><br />Gracias por su preferencia."

        mensaje += "<br /><br />"
        mensaje += "Atentamente.<br /><br />"
        mensaje += "<strong>" & razonsocial.ToString & "</strong><br /><br /></body></html>"


        Dim objMM As New MailMessage
        objMM.To.Add(correo)
        objMM.To.Add(email_from)
        objMM.From = New MailAddress(email_from, razonsocial)
        objMM.IsBodyHtml = True
        objMM.Priority = MailPriority.Normal
        objMM.Subject = razonsocial & " - Comprobante Fiscal Digital"
        objMM.Body = mensaje
        '
        '   Agrega anexos
        '
        Dim AttachXML As Net.Mail.Attachment = New Net.Mail.Attachment(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml")
        Dim AttachPDF As Net.Mail.Attachment = New Net.Mail.Attachment(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/pdf") & "/" & uuid.ToString & ".pdf")
        '
        objMM.Attachments.Add(AttachXML)
        objMM.Attachments.Add(AttachPDF)
        '
        Dim SmtpMail As New SmtpClient
        Try
            Dim SmtpUser As New Net.NetworkCredential
            SmtpUser.UserName = email_smtp_username
            SmtpUser.Password = email_smtp_password
            SmtpUser.Domain = email_smtp_server
            SmtpMail.UseDefaultCredentials = False
            SmtpMail.Credentials = SmtpUser
            SmtpMail.Host = email_smtp_server
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
            SmtpMail.Send(objMM)
            '
            '   Lo marca como enviado
            Dim ObjData As New DataControl(1)
            ObjData.RunSQLQuery("exec pCFD @cmd=40, @cfdid='" & cfdid.ToString & "'")
            ObjData = Nothing
            '
        Catch ex As Exception
        Finally
            SmtpMail = Nothing
        End Try
        objMM = Nothing
        '
        Call MuestraLista()
        '
    End Sub

    Private Sub CancelaCFDI(ByVal cfdi As Long)
        CancelarId.Value = cfdi
        rfvFolioSustituye.Enabled = False
        panelFolioSustituye.Visible = False
        txtFolioSustituye.Text = ""
        cmbMotivoCancela.SelectedValue = "02"
        WinCancel.VisibleOnPageLoad = True
    End Sub

    Private Sub CancelaCFDI_Aplica(ByVal cfdi As Long, ByVal motivoCancela As String, ByVal folioSustituye As String)
        '
        '   Obtiene serie y folio y construye nombre del XML
        '
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim archivo_llave_privada As String = ""
        Dim contrasena_llave_privada As String = ""
        Dim archivoCertificado As String = ""
        Dim connX As New SqlConnection(Session("conexion"))
        Dim cmdX As New SqlCommand("select isnull(b.serie,'') as serie, isnull(b.folio,0) as folio from tblCFD b where b.id='" & cfdi.ToString & "'", connX)
        Try

            connX.Open()

            Dim rs As SqlDataReader
            rs = cmdX.ExecuteReader()

            If rs.Read Then
                serie = rs("serie")
                folio = rs("folio")
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.End()
        Finally
            connX.Close()
            connX.Dispose()
            connX = Nothing
        End Try

        Dim FlujoReader As XmlTextReader = Nothing
        Dim i As Integer
        FlujoReader = New XmlTextReader(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "\" & "link_" & serie.ToString & folio.ToString & "_timbrado.xml")
        FlujoReader.WhitespaceHandling = WhitespaceHandling.None
        While FlujoReader.Read()
            Select Case FlujoReader.NodeType
                Case XmlNodeType.Element
                    If FlujoReader.Name = "tfd:TimbreFiscalDigital" Then
                        For i = 0 To FlujoReader.AttributeCount - 1
                            FlujoReader.MoveToAttribute(i)
                            If FlujoReader.Name = "UUID" Then
                                uuids.Add(FlujoReader.Value)
                            End If
                        Next
                    ElseIf FlujoReader.Name = "cfdi:Emisor" Then
                        For i = 0 To FlujoReader.AttributeCount - 1
                            FlujoReader.MoveToAttribute(i)
                            If FlujoReader.Name = "Rfc" Then
                                RFCEmisor = FlujoReader.Value
                            End If
                        Next
                    End If
            End Select
        End While

        Try

            Dim SIFEIUsuario As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIUsuario")
            Dim SIFEIContrasena As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIContrasena")

            Dim byteCertData As Byte() = ReadFile(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/certificados/") & CertificadoCliente() & ".pfx")

            Dim sifei As New CancelacionSIFEI.Cancelacion()

            Dim uuidscancelar As New List(Of String)
            For Each row In uuids
                uuidscancelar.Add("|" & row & "|" & motivoCancela & "|" & folioSustituye & "|")
            Next

            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType) Or DirectCast(768, System.Net.SecurityProtocolType) Or DirectCast(192, System.Net.SecurityProtocolType) Or DirectCast(48, System.Net.SecurityProtocolType)

            Dim result = sifei.cancelaCFDI(SIFEIUsuario, SIFEIContrasena, RFCEmisor, byteCertData, ContrasenaPfx(), uuidscancelar.ToArray())

            Dim xml As New XmlDocument()
            xml.LoadXml(result)
            xml.Save(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml/") & "acuse_" & uuids(0).ToString & ".xml")

            Dim EstatusUUID As String = ""
            Dim DescricionCodigo As String = ""
            EstatusUUID = GetXMLValue(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml/") & "acuse_" & uuids(0).ToString & ".xml", "EstatusUUID")

            If EstatusUUID = "201" Then
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pCFD @cmd=21, @cfdid='" & cfdi.ToString & "', @motivoid='" & motivoCancela & "', @uuid_sustituye='" & folioSustituye & "'")
                ObjData = Nothing
            ElseIf EstatusUUID = "202" Then
                DescricionCodigo = "UUID Previamente cancelado"
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pCFD @cmd=21, @cfdid='" & cfdi.ToString & "', @motivoid='" & motivoCancela & "', @uuid_sustituye='" & folioSustituye & "'")
                ObjData = Nothing
            ElseIf EstatusUUID = "203" Then
                DescricionCodigo = "UUID No corresponde el RFC del Emisor y de quien solicita la Cancelación"
            ElseIf EstatusUUID = "204" Then
                DescricionCodigo = "Folio Fiscal No Aplicable a Cancelación"
            ElseIf EstatusUUID = "205" Then
                DescricionCodigo = "UUID No existe"
            ElseIf EstatusUUID = "206" Then
                DescricionCodigo = "UUID no corresponde a un CFDI del Sector Primario"
            ElseIf EstatusUUID = "207" Then
                DescricionCodigo = "Folio sustitución inválido"
            ElseIf EstatusUUID = "208" Then
                DescricionCodigo = "La Fecha de Solicitud de Cancelación es mayor a la fecha de declaración"
            ElseIf EstatusUUID = "209" Then
                DescricionCodigo = "La Fecha de Solicitud de Cancelación límite para factura global"
            ElseIf EstatusUUID = "300" Then
                DescricionCodigo = "Usuario y contraseña inválidos"
            ElseIf EstatusUUID = "301" Then
                DescricionCodigo = "XML mas formado"
            ElseIf EstatusUUID = "302" Then
                DescricionCodigo = "Sello mal formado o inválido"
            ElseIf EstatusUUID = "303" Then
                DescricionCodigo = "Sello no corresponde a emisor"
            ElseIf EstatusUUID = "304" Then
                DescricionCodigo = "Certificado Revocado o caduco"
            ElseIf EstatusUUID = "305" Then
                DescricionCodigo = "La fecha de emisión no esta dentro de la vigencia del CSD del Emisor"
            ElseIf EstatusUUID = "306" Then
                DescricionCodigo = "El certificado no es de tipo CSD"
            ElseIf EstatusUUID = "307" Then
                DescricionCodigo = "El CFDI contiene un timbre previo"
            ElseIf EstatusUUID = "308" Then
                DescricionCodigo = "Certificado no expedido por el SAT"
            ElseIf EstatusUUID = "401" Then
                DescricionCodigo = "Fecha y hora de generación fuera de rango"
            ElseIf EstatusUUID = "402" Then
                DescricionCodigo = "RFC del emisor no se encuentra en el régimen de contribuyentes"
            ElseIf EstatusUUID = "403" Then
                DescricionCodigo = "La fecha de emisión no es posterior al 01 de enero de 2012"
            ElseIf EstatusUUID = "501" Then
                DescricionCodigo = "Autenticación no válida"
            ElseIf EstatusUUID = "203" Then
                DescricionCodigo = "UUID No corresponde el RFC del Emisor y de quien solicita la cancelación"
            ElseIf EstatusUUID = "703" Then
                DescricionCodigo = "Cuenta suspendida"
            ElseIf EstatusUUID = "704" Then
                DescricionCodigo = "Error con la contraseña de la llave Privada"
            ElseIf EstatusUUID = "705" Then
                DescricionCodigo = "XML estructura inválida"
            ElseIf EstatusUUID = "706" Then
                DescricionCodigo = "Socio Inválido"
            ElseIf EstatusUUID = "707" Then
                DescricionCodigo = "XML ya contiene un nodo TimbreFiscalDigital"
            ElseIf EstatusUUID = "708" Then
                DescricionCodigo = "No se pudo conectar al SAT"
            End If

            If EstatusUUID <> "201" Then
                RadAlert.RadAlert(DescricionCodigo, 330, 180, "Alerta", "", "")
            Else
                Call MuestraLista()
            End If

        Catch ex As SoapException
            Response.Write(ex.Detail.InnerText.ToString)
            Response.End()
        End Try
    End Sub

    Public Function GetXMLValue(ByVal url As String, ByVal nodo As String) As String
        Dim valor As String = ""
        Try
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(url)

            Dim parentNode As XmlNodeList = xmlDoc.GetElementsByTagName(nodo)
            For Each childrenNode As XmlNode In parentNode
                valor = childrenNode.InnerText
            Next
        Catch ex As Exception
            valor = ""
        End Try
        Return valor
    End Function

    Function ReadFile(ByVal strArchivo As String) As Byte()
        Dim f As New FileStream(strArchivo, FileMode.Open, FileAccess.Read)
        Dim size As Integer = CInt(f.Length)
        Dim data As Byte() = New Byte(size - 1) {}
        size = f.Read(data, 0, size)
        f.Close()
        Return data
    End Function

    Private Function CertificadoCliente() As String
        Dim certificado As String = ""
        Dim ObjData As New DataControl(1)
        certificado = ObjData.RunSQLScalarQueryString("select top 1 isnull(archivoCertificado,'') as archivoCertificado from tblMisCertificados where isnull(activo,0)=1")
        Dim elements() As String = certificado.Split(New Char() {"."c}, StringSplitOptions.RemoveEmptyEntries)
        ObjData = Nothing
        Return elements(0)
    End Function

    Private Function ContrasenaPfx() As String
        Dim contrasena_llave_privada As String = ""
        Dim ObjData As New DataControl(1)
        contrasena_llave_privada = ObjData.RunSQLScalarQueryString("select top 1 isnull(contrasena_llave_privada,'') as contrasena_llave_privada from tblCliente")
        ObjData = Nothing
        Return contrasena_llave_privada
    End Function

    Private Function FileToMemory(ByVal Filename As String) As MemoryStream
        Dim FS As New System.IO.FileStream(Filename, FileMode.Open)
        Dim MS As New System.IO.MemoryStream
        Dim BA(FS.Length - 1) As Byte
        FS.Read(BA, 0, BA.Length)
        FS.Close()
        MS.Write(BA, 0, BA.Length)
        Return MS
    End Function

    Private Sub cfdnotimbrado()
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=23, @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub cfdtimbrado()
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=24, @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub

    Private Function GeneraPDF(ByVal cfdid As Long) As Telerik.Reporting.Report
        Dim conn As New SqlConnection(Session("conexion"))

        Dim TotalImpuestosTrasladados As Decimal = 0

        Dim numeroaprobacion As String = ""
        Dim anoAprobacion As String = ""
        Dim fechaHora As String = ""
        Dim noCertificado As String = ""
        Dim razonsocial As String = ""
        Dim callenum As String = ""
        Dim colonia As String = ""
        Dim ciudad As String = ""
        Dim rfc As String = ""
        Dim regimen_fiscal_receptor As String = ""
        Dim em_razonsocial As String = ""
        Dim em_callenum As String = ""
        Dim em_colonia As String = ""
        Dim em_ciudad As String = ""
        Dim em_rfc As String = ""
        Dim em_regimen As String = ""
        Dim subtotal As Decimal = 0
        Dim iva As Decimal = 0
        Dim ieps As Decimal = 0
        Dim descuento As Decimal = 0
        Dim total As Decimal = 0
        Dim CantidadTexto As String = ""
        Dim instrucciones As String = ""
        Dim retencion As Decimal = 0
        Dim tipoid As Integer = 0
        Dim divisaid As Integer = 1
        Dim expedicionLinea1 As String = ""
        Dim expedicionLinea2 As String = ""
        Dim expedicionLinea3 As String = ""
        Dim porcentaje As Decimal = 0
        Dim plantillaid As Integer = 1
        Dim metodopago As String = ""
        Dim formapago As String = ""
        Dim numctapago As String = ""
        Dim serie As String = ""
        Dim folio As Integer = 0
        Dim uuid As String = ""
        Dim usocfdi As String = ""
        Dim tipo_comprobante As String = ""
        Dim tiporelacion As String = ""
        Dim uuid_relacionado As String = ""
        Dim logo_formato As String = ""

        Dim ds As DataSet = New DataSet

        Try

            Dim cmd As New SqlCommand("EXEC pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                serie = rs("serie")
                folio = rs("folio")
                tipoid = rs("tipoid")
                em_razonsocial = rs("em_razonsocial")
                em_callenum = rs("em_callenum")
                em_colonia = rs("em_colonia")
                em_ciudad = rs("em_ciudad")
                em_rfc = rs("em_rfc")
                em_regimen = rs("em_regimen")
                razonsocial = rs("razonsocial")
                callenum = rs("callenum")
                colonia = rs("colonia")
                ciudad = rs("ciudad")
                rfc = rs("rfc")
                regimen_fiscal_receptor = rs("regimen_fiscal_receptor")
                descuento = rs("descuento")
                'iva = rs("iva")
                ieps = rs("ieps_total")
                divisaid = rs("divisaid")
                fechaHora = rs("fecha_factura").ToString
                instrucciones = rs("instrucciones")
                expedicionLinea1 = rs("expedicionLinea1")
                expedicionLinea2 = rs("expedicionLinea2")
                expedicionLinea3 = rs("expedicionLinea3")
                porcentaje = rs("porcentaje")
                plantillaid = rs("plantillaid")
                tipocontribuyenteid = rs("tipocontribuyenteid")
                metodopago = rs("metodopago")
                formapago = rs("formapago")
                numctapago = rs("numctapago")
                usocfdi = rs("usocfdi")
                uuid = rs("uuid")
                tiporelacion = rs("tiporelacion")
                uuid_relacionado = rs("uuid_relacionado")
                logo_formato = rs("logo_formato")
            End If
            rs.Close()
        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        subtotal = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "SubTotal", "cfdi:Comprobante")
        total = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "Total", "cfdi:Comprobante")
        TotalImpuestosTrasladados = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "TotalImpuestosTrasladados", "cfdi:Impuestos")
        iva = TotalImpuestosTrasladados - ieps

        Dim largo = Len(CStr(Format(CDbl(total), "#,###.00")))
        Dim decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)

        If System.Configuration.ConfigurationManager.AppSettings("divisas") = 1 Then
            If divisaid = 1 Then
                CantidadTexto = Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 MXN"
            Else
                CantidadTexto = Num2Text(total - decimales) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD"
            End If
        Else
            CantidadTexto = Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 MXN"
        End If

        Dim reporte As New Factura40()
        reporte.ReportParameters("plantillaId").Value = plantillaid
        reporte.ReportParameters("cfdiId").Value = cfdid
        reporte.ReportParameters("cnn").Value = Session("conexion").ToString

        Select Case tipoid
            Case 1, 4
                reporte.ReportParameters("txtDocumento").Value = "Factura No. " & serie.ToString & folio.ToString
            Case 2, 8
                reporte.ReportParameters("txtDocumento").Value = "Nota de Crédito No. " & serie.ToString & folio.ToString
                reporte.ReportParameters("txtUUIDRelacionadoTitle").Value = "UUID Relacionado"
            Case 5
                reporte.ReportParameters("txtDocumento").Value = "Carta Porte No. " & serie.ToString & folio.ToString
            Case 6
                reporte.ReportParameters("txtDocumento").Value = "Recibo de Honorarios No. " & serie.ToString & folio.ToString
            Case Else
                reporte.ReportParameters("txtDocumento").Value = "Factura No. " & serie.ToString & folio.ToString
        End Select
        If Not File.Exists(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/cbb/" & uuid.ToString & ".png")) Then
            generacbb(uuid)
        End If
        reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/cbb/" & uuid.ToString & ".png")
        reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/logos/" & logo_formato.ToString & "")
        reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "Fecha", "cfdi:Comprobante")
        reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "UUID", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtPACCertifico").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "NoCertificado", "cfdi:Comprobante")
        reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "Nombre", "cfdi:Receptor")
        reporte.ReportParameters("txtClienteCalleNum").Value = callenum
        reporte.ReportParameters("txtClienteColonia").Value = colonia
        reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
        reporte.ReportParameters("txtRegimenFiscalReceptor").Value = regimen_fiscal_receptor
        reporte.ReportParameters("txtClienteRFC").Value = "R.F.C. " & GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "Rfc", "cfdi:Receptor")        '
        reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "Sello", "cfdi:Comprobante")
        reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "SelloSAT", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtInstrucciones").Value = instrucciones
        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(subtotal, 2).ToString
        reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
        tipo_comprobante = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "TipoDeComprobante", "cfdi:Comprobante")
        If tipo_comprobante = "I" Then
            tipo_comprobante = "I - Ingreso"
        ElseIf tipo_comprobante = "E" Then
            tipo_comprobante = "E - Egreso"
            reporte.ReportParameters("txtTipoRelacion").Value = tiporelacion.ToString
            reporte.ReportParameters("txtUUIDRelacionado").Value = uuid_relacionado.ToString
        ElseIf tipo_comprobante = "N" Then
            tipo_comprobante = "N - Nómina"
        ElseIf tipo_comprobante = "P" Then
            tipo_comprobante = "P - Pago"
        ElseIf tipo_comprobante = "T" Then
            tipo_comprobante = "T - Traslado"
        End If
        reporte.ReportParameters("txtTipoComprobante").Value = tipo_comprobante
        reporte.ReportParameters("txtIEPS").Value = FormatCurrency(Math.Round(ieps, 2), 2).ToString
        reporte.ReportParameters("txtIVA").Value = FormatCurrency(Math.Round(iva, 2), 2).ToString
        reporte.ReportParameters("txtDescuento").Value = FormatCurrency(Math.Round(descuento, 2), 2).ToString
        reporte.ReportParameters("txtRetIVA").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtTotal").Value = FormatCurrency(Math.Round(total, 2), 2).ToString        '
        reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento(uuid)
        reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
        reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea2 & " - " & expedicionLinea3
        reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
        reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
        reporte.ReportParameters("txtMetodoPago").Value = metodopago.ToString
        reporte.ReportParameters("txtUsoCFDI").Value = usocfdi.ToString
        reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString

        Return reporte

    End Function

    Private Function Num2Text(ByVal value As Decimal) As String
        Select Case value
            Case 0 : Num2Text = "CERO"
            Case 1 : Num2Text = "UN"
            Case 2 : Num2Text = "DOS"
            Case 3 : Num2Text = "TRES"
            Case 4 : Num2Text = "CUATRO"
            Case 5 : Num2Text = "CINCO"
            Case 6 : Num2Text = "SEIS"
            Case 7 : Num2Text = "SIETE"
            Case 8 : Num2Text = "OCHO"
            Case 9 : Num2Text = "NUEVE"
            Case 10 : Num2Text = "DIEZ"
            Case 11 : Num2Text = "ONCE"
            Case 12 : Num2Text = "DOCE"
            Case 13 : Num2Text = "TRECE"
            Case 14 : Num2Text = "CATORCE"
            Case 15 : Num2Text = "QUINCE"
            Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
            Case 20 : Num2Text = "VEINTE"
            Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
            Case 30 : Num2Text = "TREINTA"
            Case 40 : Num2Text = "CUARENTA"
            Case 50 : Num2Text = "CINCUENTA"
            Case 60 : Num2Text = "SESENTA"
            Case 70 : Num2Text = "SETENTA"
            Case 80 : Num2Text = "OCHENTA"
            Case 90 : Num2Text = "NOVENTA"
            Case Is < 100 : Num2Text = Num2Text(Int(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
            Case 100 : Num2Text = "CIEN"
            Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
            Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Int(value \ 100)) & "CIENTOS"
            Case 500 : Num2Text = "QUINIENTOS"
            Case 700 : Num2Text = "SETECIENTOS"
            Case 900 : Num2Text = "NOVECIENTOS"
            Case Is < 1000 : Num2Text = Num2Text(Int(value \ 100) * 100) & " " & Num2Text(value Mod 100)
            Case 1000 : Num2Text = "MIL"
            Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
            Case Is < 1000000 : Num2Text = Num2Text(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
            Case 1000000 : Num2Text = "UN MILLON"
            Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
            Case Is < 1000000000000.0# : Num2Text = Num2Text(Int(value / 1000000)) & " MILLONES "
                If (value - Int(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000) * 1000000)
            Case 1000000000000.0# : Num2Text = "UN BILLON"
            Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : Num2Text = Num2Text(Int(value / 1000000000000.0#)) & " BILLONES"
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
        End Select
    End Function

    Private Function CadenaOriginalComplemento(ByVal UUID As String) As String
        '
        '   Obtiene los valores del timbre de respuesta
        '
        Dim Version As String = ""
        Dim selloSAT As String = ""
        Dim noCertificadoSAT As String = ""
        Dim selloCFD As String = ""
        Dim fechaTimbrado As String = ""
        Dim RfcProvCertif As String = ""

        Dim FlujoReader As XmlTextReader = Nothing
        Dim i As Integer
        FlujoReader = New XmlTextReader(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & UUID.ToString & ".xml")
        FlujoReader.WhitespaceHandling = WhitespaceHandling.None
        While FlujoReader.Read()
            Select Case FlujoReader.NodeType
                Case XmlNodeType.Element
                    If FlujoReader.Name = "tfd:TimbreFiscalDigital" Then
                        For i = 0 To FlujoReader.AttributeCount - 1
                            FlujoReader.MoveToAttribute(i)
                            If FlujoReader.Name = "fechaTimbrado" Or FlujoReader.Name = "FechaTimbrado" Then
                                fechaTimbrado = FlujoReader.Value
                            ElseIf FlujoReader.Name = "UUID" Then
                                UUID = FlujoReader.Value
                            ElseIf FlujoReader.Name = "NoCertificadoSAT" Then
                                noCertificadoSAT = FlujoReader.Value
                            ElseIf FlujoReader.Name = "SelloCFD" Then
                                selloCFD = FlujoReader.Value
                            ElseIf FlujoReader.Name = "SelloSAT" Then
                                selloSAT = FlujoReader.Value
                            ElseIf FlujoReader.Name = "Version" Then
                                Version = FlujoReader.Value
                            ElseIf FlujoReader.Name = "RfcProvCertif" Then
                                RfcProvCertif = FlujoReader.Value
                            End If
                        Next
                    End If
            End Select
        End While
        '
        Dim cadena As String = ""
        cadena = "||" & Version & "|" & UUID & "|" & fechaTimbrado & "|" & RfcProvCertif & "|" & selloCFD & "|" & noCertificadoSAT & "||"
        Return cadena
        '
    End Function

    Private Sub generacbb(ByVal UUID As String)
        Dim CadenaCodigoBidimensional As String = ""
        Dim FinalSelloDigitalEmisor As String = ""

        Dim rfcE As String = ""
        Dim rfcR As String = ""
        Dim total As String = ""
        Dim sello As String = ""
        '
        '   Obtiene datos del cfdi para construir string del CBB
        '
        rfcE = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & UUID.ToString & ".xml", "Rfc", "cfdi:Emisor")
        rfcR = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & UUID.ToString & ".xml", "Rfc", "cfdi:Receptor")
        total = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & UUID.ToString & ".xml", "Total", "cfdi:Comprobante")
        UUID = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & UUID.ToString & ".xml", "UUID", "tfd:TimbreFiscalDigital")
        sello = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & UUID.ToString & ".xml", "SelloCFD", "tfd:TimbreFiscalDigital")
        FinalSelloDigitalEmisor = Mid(sello, (Len(sello) - 7))
        '
        Dim totalDec As Decimal = CType(total, Decimal)
        '
        CadenaCodigoBidimensional = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx" & "?id=" & UUID & "&re=" & rfcE & "&rr=" & rfcR & "&tt=" & totalDec.ToString & "&fe=" & FinalSelloDigitalEmisor
        '
        '   Genera gráfico
        '
        Dim qrCodeEncoder As QRCodeEncoder = New QRCodeEncoder
        qrCodeEncoder.QRCodeEncodeMode = Codec.QRCodeEncoder.ENCODE_MODE.BYTE
        qrCodeEncoder.QRCodeScale = 6
        qrCodeEncoder.QRCodeErrorCorrect = Codec.QRCodeEncoder.ERROR_CORRECTION.L
        'La versión "0" calcula automáticamente el tamaño
        qrCodeEncoder.QRCodeVersion = 0

        qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor)
        qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor)

        Dim CBidimensional As Drawing.Image
        CBidimensional = qrCodeEncoder.Encode(CadenaCodigoBidimensional, System.Text.Encoding.UTF8)
        CBidimensional.Save(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/cbb/") & UUID & ".png", System.Drawing.Imaging.ImageFormat.Png)
    End Sub

    Private Sub VerAcuse(ByVal cfdi As Long)
        '
        '   Obtiene serie y folio y construye nombre del XML
        '
        Dim UUID As String = ""
        Dim archivo_llave_privada As String = ""
        Dim contrasena_llave_privada As String = ""
        Dim archivoCertificado As String = ""
        Dim connX As New SqlConnection(Session("conexion"))
        Dim cmdX As New SqlCommand("select isnull(uuid,'') as UUID from tblCFD where id='" & cfdi.ToString & "'", connX)
        Try

            connX.Open()

            Dim rs As SqlDataReader
            rs = cmdX.ExecuteReader()

            If rs.Read Then
                UUID = rs("UUID")
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.End()
        Finally
            connX.Close()
            connX.Dispose()
            connX = Nothing
        End Try
        '
        Dim UrlSAT As String = ""
        Dim FinalSelloDigitalEmisor As String = ""

        Dim rfcE As String = ""
        Dim rfcR As String = ""
        Dim total As String = ""
        Dim sello As String = ""
        '
        '   Obtiene datos del cfdi para construir string del CBB
        '
        rfcE = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "\" & UUID.ToString & ".xml", "Rfc", "cfdi:Emisor")
        rfcR = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "\" & UUID.ToString & ".xml", "Rfc", "cfdi:Receptor")
        total = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "\" & UUID.ToString & ".xml", "Total", "cfdi:Comprobante")
        UUID = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "\" & UUID.ToString & ".xml", "UUID", "tfd:TimbreFiscalDigital")
        sello = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "\" & UUID.ToString & ".xml", "SelloCFD", "tfd:TimbreFiscalDigital")
        FinalSelloDigitalEmisor = Mid(sello, (Len(sello) - 7))
        '
        Dim totalDec As Decimal = CType(total, Decimal)
        '
        UrlSAT = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx" & "?id=" & UUID & "&re=" & rfcE & "&rr=" & rfcR & "&tt=" & totalDec.ToString & "&fe=" & FinalSelloDigitalEmisor

        Dim script As String = "function f(){openRadWindow('" & UrlSAT & "'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)

    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Public Function GetXmlAttribute(ByVal url As String, ByVal campo As String, ByVal nodo As String) As String
        Dim valor As String = ""
        Dim FlujoReader As XmlTextReader = Nothing
        Dim i As Integer

        FlujoReader = New XmlTextReader(url)
        FlujoReader.WhitespaceHandling = WhitespaceHandling.None
        Try
            While FlujoReader.Read()
                Select Case FlujoReader.NodeType
                    Case XmlNodeType.Element
                        If FlujoReader.Name.ToString.ToUpper = nodo.ToUpper Then
                            For i = 0 To FlujoReader.AttributeCount - 1
                                FlujoReader.MoveToAttribute(i)
                                If FlujoReader.Name = campo Then
                                    valor = FlujoReader.Value.ToString
                                End If
                            Next
                        End If
                End Select
            End While
        Catch ex As Exception
            valor = ""
        End Try

        Return valor

    End Function
#End Region

#Region "Events"
    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        Dim ObjData As New DataControl(1)
        cfdlist.DataSource = ObjData.FillDataSet("exec pCFD @cmd=22, @serie='" & txtSerie.Text & "', @folio='" & txtFolio.Text & "'")
        cfdlist.DataBind()
        ObjData = Nothing
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Call MuestraLista()

    End Sub
#End Region

#Region "Grid Filters"
    Private Sub SetGridFilters()
        cfdlist.GroupingSettings.CaseSensitive = False
        Dim Menu As GridFilterMenu = cfdlist.FilterMenu
        Dim item As RadMenuItem
        For Each item In Menu.Items
            'change the text for the StartsWith menu item
            If item.Text = "StartsWith" Then
                item.Text = "Empieza con"
            End If

            If item.Text = "NoFilter" Then
                item.Text = "Sin Filtro"
            End If

            If item.Text = "EqualTo" Then
                item.Text = "Igual a"
            End If

            If item.Text = "EndsWith" Then
                item.Text = "Termina con"
            End If


        Next

    End Sub

    Private Sub cfdlist_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles cfdlist.Init
        cfdlist.PagerStyle.NextPagesToolTip = "Ver mas"
        cfdlist.PagerStyle.NextPageToolTip = "Siguiente"
        cfdlist.PagerStyle.PrevPagesToolTip = "Ver mas"
        cfdlist.PagerStyle.PrevPageToolTip = "Atras"
        cfdlist.PagerStyle.LastPageToolTip = "Ultima Página"
        cfdlist.PagerStyle.FirstPageToolTip = "Primera Pagina"
        cfdlist.PagerStyle.PagerTextFormat = "{4}    Pagina {0} de {1}, Registros {2} al {3} de {5}"
        cfdlist.SortingSettings.SortToolTip = "Ordernar"
        cfdlist.SortingSettings.SortedAscToolTip = "Ordenar Asc"
        cfdlist.SortingSettings.SortedDescToolTip = "Ordenar Desc"


        Dim menu As GridFilterMenu = cfdlist.FilterMenu
        Dim i As Integer = 0
        While i < menu.Items.Count
            If menu.Items(i).Text = "NoFilter" Or
               menu.Items(i).Text = "EqualTo" Then
                i = i + 1
            Else
                menu.Items.RemoveAt(i)
            End If
        End While
    End Sub

    Private Sub cmbMotivoCancela_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMotivoCancela.SelectedIndexChanged
        txtFolioSustituye.Text = ""
        If cmbMotivoCancela.SelectedValue = "01" Then
            rfvFolioSustituye.Enabled = True
            panelFolioSustituye.Visible = True
            WinCancel.Height = 270
            txtFolioSustituye.Focus()
        Else
            rfvFolioSustituye.Enabled = False
            panelFolioSustituye.Visible = False
            WinCancel.Height = 220
        End If
    End Sub

    Private Sub btnCancelaFactura_Click(sender As Object, e As EventArgs) Handles btnCancelaFactura.Click
        Dim motivoCancela As String = ""
        Dim folioSustituye As String = ""

        folioSustituye = txtFolioSustituye.Text
        motivoCancela = cmbMotivoCancela.SelectedValue

        CancelaCFDI_Aplica(CancelarId.Value, motivoCancela, folioSustituye)

        WinCancel.VisibleOnPageLoad = False
        CancelarId.Value = 0
        txtFolioSustituye.Text = ""
    End Sub

#End Region

End Class