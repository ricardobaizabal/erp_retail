Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Web.UI.RadNumericTextBox
Imports System.IO
Imports System.Net.Mail
Imports Telerik.Reporting.Processing

Partial Public Class cotizaciones
    Inherits System.Web.UI.Page
    Private subtotal As Decimal = 0
    Private iva As Decimal = 0
    Private total As Decimal = 0
    Private clienteid As Long = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbCliente, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by razonsocial", 0)
            objCat = Nothing
            '
            'lblSubtotalValue.Text = FormatCurrency(total, 2).ToString
            'lblIVAValue.Text = FormatCurrency(total, 2).ToString
            lblTotalValue.Text = FormatCurrency(total, 2).ToString
            '
        End If

    End Sub

    Function ObtenerCotizaciones() As DataSet

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pCotizacion @cmd=1, @txtSearch='" & txtSearch.Text & "'", conn)

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

    Function ObtenerProductosCotizados() As DataSet

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pCotizacion @cmd=15, @clienteid='" & clienteid.ToString & "'", conn)

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

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/portalcfd/cotizaciones/cotizaciones.aspx")
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If InsertOrUpdate.Value = 0 Then

                Dim sql As String = ""
                sql = "EXEC pCotizacion @cmd=4, @userid='" & Session("userid").ToString & "', @clienteid='" & cmbCliente.SelectedValue.ToString & "', @comentarios='" & txtComentarios.Text & "'"

                Dim ObjData As New DataControl(1)
                CotizacionID.Value = ObjData.RunSQLScalarQuery(sql)
                ObjData = Nothing

                panelRegistroCotizacion.Visible = True
                panelItemsRegistration.Visible = True
                panelProductosCotizados.Visible = True
                panelResumen.Visible = True

                cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
                cotizacionesList.DataSource = ObtenerCotizaciones()
                cotizacionesList.DataBind()

            Else

                Dim sql As String = ""
                sql = "EXEC pCotizacion @cmd=5, @cotizacionid='" & CotizacionID.Value & "', @userid='" & Session("userid").ToString & "', @clienteid='" & cmbCliente.SelectedValue.ToString & "', @comentarios='" & txtComentarios.Text & "'"

                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery(sql)
                ObjData = Nothing

                panelRegistroCotizacion.Visible = True
                panelItemsRegistration.Visible = True
                panelProductosCotizados.Visible = False
                panelResumen.Visible = True

                txtSearchItem.Focus()

            End If

            clienteid = cmbCliente.SelectedValue

            gridProductosCotizados.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
            gridProductosCotizados.DataSource = ObtenerProductosCotizados()
            gridProductosCotizados.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally
        End Try
        panelItemsRegistration.Visible = True
        panelProductosCotizados.Visible = True
    End Sub

    Protected Sub btnAgregarCotizacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarCotizacion.Click
        InsertOrUpdate.Value = 0
        panelRegistroCotizacion.Visible = True
        panelProductosCotizados.Visible = False
        panelItemsRegistration.Visible = False
        panelResumen.Visible = False
        Dim ObjData As New DataControl(1)
        ObjData.Catalogo(cmbCliente, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by razonsocial", 0)
        ObjData = Nothing
    End Sub

    Protected Sub cotizacionesList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles cotizacionesList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaCotizacion(e.CommandArgument)
                Call DisplayItems()
                Call CargaTotales()
                panelResumen.Visible = True
                gridResults.Visible = False
                itemsList.Visible = True
                txtSearchItem.Text = ""
                txtSearchItem.Focus()
                btnCancelSearch.Visible = False
            Case "cmdDelete"
                EliminaCotizacion(e.CommandArgument)
            Case "cmdDownload"
                Call DownloadPDF(e.CommandArgument)
            Case "cmdSend"
                Try
                    SendEmail(e.CommandArgument)
                    lblMensajeGuardar.Text = "Cotización enviada exitosamente"
                    lblMensajeGuardar.ForeColor = Drawing.Color.Green
                    Dim script As String = "<script type='text/javascript'>mensaje();</script>"
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", script, False)
                Catch ex As Exception
                    lblMensajeGuardar.Text = "Error: " & ex.Message.ToString
                    lblMensajeGuardar.ForeColor = Drawing.Color.Red
                    Dim script As String = "<script type='text/javascript'>mensaje();</script>"
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", script, False)
                End Try
        End Select
    End Sub

    Private Sub EditaCotizacion(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCotizacion @cmd=2, @cotizacionid='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                clienteid = rs("clienteid")

                txtComentarios.Text = rs("comentarios")

                Dim ObjData As New DataControl(1)
                ObjData.Catalogo(cmbCliente, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by razonsocial", rs("clienteid"))
                ObjData = Nothing

                gridProductosCotizados.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
                gridProductosCotizados.DataSource = ObtenerProductosCotizados()
                gridProductosCotizados.DataBind()

                panelRegistroCotizacion.Visible = True
                panelItemsRegistration.Visible = True
                panelProductosCotizados.Visible = True
                panelResumen.Visible = True

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

            Dim cmd As New SqlCommand("EXEC pCotizacion @cmd=3, @cotizacionid='" & id.ToString & "'", conn)

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

        Response.Redirect("~/portalcfd/cotizaciones/cotizaciones.aspx")

    End Sub

    Private Sub DownloadPDF(ByVal cotizacionid As Long)
        Dim FilePath = Server.MapPath("~/portalcfd/cotizaciones/cotizaciones/Cotizacion_") & cotizacionid.ToString & ".pdf"

        'Call CargaTotalesPDF(cotizacionid)
        'GuardaPDF(GeneraPDF(cotizacionid), FilePath)
        'Dim FileName As String = Path.GetFileName(FilePath)
        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
        'Response.Flush()
        'Response.WriteFile(FilePath)
        'Response.End()
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        Else
            Call CargaTotalesPDF(cotizacionid)
            GuardaPDF(GeneraPDF(cotizacionid), FilePath)
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If
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
        '
        Dim conn As New SqlConnection(Session("conexion"))
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pCotizacion @cmd=13, @cotizacionid='" & cotizacionid.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            razonsocial = rs("razon_social")
            email_from = rs("email_from")
            email_to = rs("email_to")
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
        '   Agrega anexos
        '
        Dim FilePath = Server.MapPath("~/portalcfd/cotizaciones/cotizaciones/Cotizacion_") & cotizacionid.ToString & ".pdf"

        If Not File.Exists(FilePath) Then
            Call CargaTotalesPDF(cotizacionid)
            GuardaPDF(GeneraPDF(cotizacionid), FilePath)
        End If

        Dim AttachPDF As Net.Mail.Attachment = New Net.Mail.Attachment(FilePath)
        '
        objMM.Attachments.Add(AttachPDF)
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
            '
            '
        Finally
            SmtpMail = Nothing
        End Try
        objMM = Nothing
        ''
    End Sub

    Private Function GeneraPDF(ByVal cotizacionid As Long) As Telerik.Reporting.Report
        Dim conn As New SqlConnection(Session("conexion"))
        '
        Dim no_cotizacion As Integer = 0
        Dim tipo_cliente As Integer = 0
        Dim cliente As String = ""
        Dim prospecto As String = ""
        Dim fecha_cotizacion As String = ""
        Dim condiciones As String = ""
        '
        Dim ds As DataSet = New DataSet
        '
        Try
            '
            Dim cmd As New SqlCommand("EXEC pCotizacion @cmd=11, @cotizacionid='" & cotizacionid.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            '
            If rs.Read Then
                no_cotizacion = rs("id")
                cliente = rs("cliente")
                fecha_cotizacion = rs("fecha_alta")
                condiciones = rs("condiciones")
                subtotal = rs("importe")
                iva = rs("iva")
                total = rs("total")
            End If
            rs.Close()
            '
        Catch ex As Exception
            Response.Write(ex.ToString)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
        '
        Dim reporte As New FormatosPDF.formato_cotizacion_trevisa
        '
        reporte.ReportParameters("plantillaId").Value = 5
        reporte.ReportParameters("cotizacionid").Value = cotizacionid
        reporte.ReportParameters("paramImgBanner").Value = "banerComercial.jpg"
        reporte.ReportParameters("txtNoCotizacion").Value = cotizacionid
        reporte.ReportParameters("txtClienteProspecto").Value = cliente
        reporte.ReportParameters("txtFechaCotizacion").Value = fecha_cotizacion
        reporte.ReportParameters("txtCondicionesPago").Value = condiciones
        '
        'reporte.ReportParameters("txtSubTotal").Value = FormatCurrency(subtotal, 2).ToString
        'reporte.ReportParameters("txtIva").Value = FormatCurrency(iva, 2).ToString
        reporte.ReportParameters("txtTotal").Value = FormatCurrency(subtotal, 2).ToString
        '
        Return reporte
        ''
    End Function

    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub

    Protected Sub cotizacionesList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles cotizacionesList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Cotizaciones" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea eliminar de la base de datos esta cotización?');")

                Dim btnDownload As ImageButton = CType(e.Item.FindControl("btnDownload"), ImageButton)
                Dim imgSend As ImageButton = CType(e.Item.FindControl("imgSend"), ImageButton)

                Dim lblPartidas As Label = CType(e.Item.FindControl("lblPartidas"), Label)

                If Convert.ToInt32(lblPartidas.Text) > 0 Then
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
        cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        cotizacionesList.DataSource = ObtenerCotizaciones()
    End Sub

    Protected Sub txtSearchItem_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchItem.TextChanged
        gridResults.Visible = True
        itemsList.Visible = False
        Dim objdata As New DataControl
        gridResults.DataSource = objdata.FillDataSet("exec pCotizacion @cmd=6, @txtSearch='" & txtSearchItem.Text & "'")
        gridResults.DataBind()
        objdata = Nothing
        txtSearchItem.Text = ""
        txtSearchItem.Focus()
        btnCancelSearch.Visible = True
    End Sub

    Protected Sub btnCancelSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelSearch.Click
        gridResults.Visible = False
        itemsList.Visible = True
        txtSearchItem.Text = ""
        txtSearchItem.Focus()
        btnCancelSearch.Visible = False
    End Sub

    Protected Sub gridResults_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gridResults.ItemCommand
        Select Case e.CommandName
            Case "cmdAdd"
                InsertItem(e.CommandArgument, e.Item)
                DisplayItems()
                Call CargaTotales()
                panelResumen.Visible = True
                gridResults.Visible = False
                itemsList.Visible = True
                txtSearchItem.Text = ""
                txtSearchItem.Focus()
                btnCancelSearch.Visible = False
        End Select
    End Sub

    Protected Sub InsertItem(ByVal id As Integer, ByVal item As GridItem)
        '
        ' Instancía objetos del grid
        '
        Dim lblCodigo As Label = DirectCast(item.FindControl("lblCodigo"), Label)
        Dim lblPresentacionId As Label = DirectCast(item.FindControl("lblPresentacionId"), Label)
        Dim lblDescripcion As Label = DirectCast(item.FindControl("lblDescripcion"), Label)
        Dim lblUnidad As Label = DirectCast(item.FindControl("lblUnidad"), Label)
        Dim txtQuantity As RadNumericTextBox = DirectCast(item.FindControl("txtQuantity"), RadNumericTextBox)
        '
        '   Agrega la partida
        '
        Dim objdata As New DataControl
        objdata.RunSQLQuery("EXEC pCotizacion @cmd=7, @cotizacionid='" & CotizacionID.Value.ToString & "', @codigo='" & lblCodigo.Text & "', @descripcion='" & lblDescripcion.Text & "', @cantidad='" & txtQuantity.Text & "', @unidad='" & lblUnidad.Text & "', @productoid='" & id.ToString & "', @presentacionid='" & lblPresentacionId.Text.ToString & "'")
        objdata = Nothing
        '
        cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        cotizacionesList.DataSource = ObtenerCotizaciones()
        cotizacionesList.DataBind()
        '
        ''
    End Sub

    Private Sub DisplayItems()
        Dim ds As DataSet
        Dim ObjData As New DataControl(1)
        itemsList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        ds = ObjData.FillDataSet("EXEC pCotizacion @cmd=8, @cotizacionid='" & CotizacionID.Value.ToString & "'")
        itemsList.DataSource = ds
        itemsList.DataBind()
        ObjData = Nothing
    End Sub

    Protected Sub itemsList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles itemsList.ItemCommand
        Select Case e.CommandName

            Case "cmdDelete"
                DeleteItem(e.CommandArgument)
                CargaTotales()

        End Select
    End Sub

    Private Sub DeleteItem(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("EXEC pCotizacion @cmd=10, @partidaId ='" & id.ToString & "'", conn)

        conn.Open()

        cmd.ExecuteReader()

        conn.Close()

        Call DisplayItems()

    End Sub

    Protected Sub itemsList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles itemsList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Items" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('" & Resources.Resource.ItemsDeleteConfirmationMessage & "');")

            End If

        End If
    End Sub

    Protected Sub itemsList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles itemsList.NeedDataSource
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pCotizacion @cmd=8, @cotizacionid='" & CotizacionID.Value.ToString & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            itemsList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
            itemsList.DataSource = ds
            itemsList.DataBind()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            '
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub

    Private Sub CargaTotales()
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("exec pCotizacion @cmd=9, @cotizacionid='" & CotizacionID.Value.ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                subtotal = rs("subtotal")
                iva = rs("iva")
                total = rs("total")
                'lblSubTotalValue.Text = FormatCurrency(subtotal, 2).ToString
                'lblIVAValue.Text = FormatCurrency(iva, 2).ToString
                lblTotalValue.Text = FormatCurrency(subtotal, 2).ToString
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Private Sub CargaTotalesPDF(ByVal cotizacionid As Integer)
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("exec pCotizacion @cmd=9, @cotizacionid='" & cotizacionid.ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                subtotal = rs("subtotal")
                iva = rs("iva")
                total = rs("total")
            End If
        Catch ex As Exception
            '
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        panelRegistroCotizacion.Visible = False
        panelItemsRegistration.Visible = False
        panelProductosCotizados.Visible = False
        panelResumen.Visible = False
        gridResults.Visible = False
        itemsList.Visible = False
        btnCancelSearch.Visible = False
        txtSearchItem.Text = ""
        cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        cotizacionesList.DataSource = ObtenerCotizaciones()
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        panelRegistroCotizacion.Visible = False
        panelItemsRegistration.Visible = False
        panelProductosCotizados.Visible = False
        panelResumen.Visible = False
        gridResults.Visible = False
        itemsList.Visible = False
        btnCancelSearch.Visible = False
        txtSearchItem.Text = ""
        txtSearch.Text = ""
        cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        cotizacionesList.DataSource = ObtenerCotizaciones()
    End Sub

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

    Private Sub gridProductosCotizados_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gridProductosCotizados.NeedDataSource
        gridProductosCotizados.MasterTableView.NoMasterRecordsText = "No se encontraron cotizaciones para mostrar"
        gridProductosCotizados.DataSource = ObtenerCotizaciones()
    End Sub

    Private Sub btnFinalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        Response.Redirect("cotizaciones.aspx", False)
    End Sub

End Class