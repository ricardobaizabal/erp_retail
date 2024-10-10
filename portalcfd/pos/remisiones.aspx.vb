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

Partial Public Class remisiones
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Private RFCEmisor As String = ""
    Private uuids As New List(Of String)()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbCliente, "exec pMisClientes @cmd=1", 0)
            objCat.Catalogo(cmbSucursal, "exec pCatalogos @cmd=11", 0)
            ObjCat = Nothing
            '
            fechaini.SelectedDate = Now
            fechafin.SelectedDate = Now
            '
            Call MuestraVentas()
            '
            If Session("clienteid") = 2 Then
                grdVentas.MasterTableView.GetColumn("factura").Visible = True
                grdVentas.MasterTableView.GetColumn("facturar").Visible = True
            Else
                grdVentas.MasterTableView.GetColumn("factura").Visible = False
                grdVentas.MasterTableView.GetColumn("facturar").Visible = False
            End If
            '
        End If
    End Sub

    Private Sub MuestraVentas()
        '
        grdVentas.DataSource = Nothing
        grdVentas.DataBind()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        Dim sql As String = ""
        sql = "exec pVentas @cmd=1, @clienteid='" & cmbCliente.SelectedValue.ToString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @folio='" & txtTicket.Text & "'"
        ds = ObjData.FillDataSet(sql)
        grdVentas.DataSource = ds
        grdVentas.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Call MuestraVentas()
    End Sub

    Protected Sub grdVentas_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdVentas.ItemCommand
        Select Case e.CommandName
            Case "cmdXML"
                Call DownloadXML(e.CommandArgument)
            Case "cmdPDF"
                Call DownloadPDF(e.CommandArgument)
            Case "cmdTicket"
                Call DownloadTicket(e.CommandArgument)
            Case "cmdCancel"
                Dim arg As String() = New String(1) {}
                arg = e.CommandArgument.ToString().Split(";"c)
                Dim remisionid As String = arg(0)
                Dim cfdid As String = arg(1)

                If Convert.ToInt64(cfdid) > 0 Then
                    Call CancelaSIFEI(cfdid)
                End If

                If Convert.ToInt64(cfdid) > 0 And Convert.ToInt64(remisionid) > 0 Then
                    Call CancelaRemision(remisionid)
                Else
                    Call CancelaRemisionRegresaInventario(remisionid)
                End If

                Call MuestraVentas()
            Case "cmdFacturar"
                Response.Redirect("~/portalcfd/pos/FacturarTicket.aspx?id=" & e.CommandArgument.ToString)
        End Select
    End Sub

    Protected Sub grdVentas_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdVentas.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim lblTimbrado As Label = CType(e.Item.FindControl("lblTimbrado"), Label)
                Dim lnkCancelar As LinkButton = CType(e.Item.FindControl("lnkCancelar"), LinkButton)
                Dim lnkXML As LinkButton = CType(e.Item.FindControl("lnkXML"), LinkButton)
                Dim lnkPDF As LinkButton = CType(e.Item.FindControl("lnkPDF"), LinkButton)
                Dim lnkfacturar As LinkButton = CType(e.Item.FindControl("lnkfacturar"), LinkButton)

                lnkCancelar.Attributes.Add("onclick", "javascript:return confirm('Va a cancelar un venta. Si la remisión fué facturada, la factura será cancelada. ¿Desea continuar?');")


                If e.Item.DataItem("factura") = "0" Then
                    lnkfacturar.Visible = True
                Else
                    lnkfacturar.Visible = False
                End If

                If e.Item.DataItem("estatusid") = 2 Then
                    lnkCancelar.Visible = False
                    lnkXML.Visible = False
                    lnkPDF.Visible = False
                    e.Item.Cells(12).Text = "CANCELADA"
                    e.Item.Cells(12).Font.Bold = True
                    e.Item.Cells(12).ForeColor = Drawing.Color.Red
                Else
                    If e.Item.DataItem("timbrado") = True Then
                        lnkCancelar.Visible = True
                        lnkXML.Visible = True
                        lnkPDF.Visible = True
                    Else
                        e.Item.Cells(12).Text = ""
                        lnkXML.Visible = False
                        lnkPDF.Visible = False
                    End If
                End If

                If e.Item.DataItem("estatus") = "Aplicado" Then
                    lnkCancelar.Visible = True
                ElseIf e.Item.DataItem("estatus") = "Cancelado" Then
                    lnkCancelar.Visible = False
                    lnkXML.Visible = False
                    e.Item.Cells(11).Text = "CANCELADA"
                    e.Item.Cells(11).Font.Bold = True
                    e.Item.Cells(11).ForeColor = Drawing.Color.Red
                End If

                If Convert.ToDecimal(e.Item.DataItem("monto")) <= 0 Then
                    lnkCancelar.Visible = False
                End If

                If Session("perfilid") <> 1 Or e.Item.DataItem("cfdid") > 0 Then
                    lnkCancelar.Visible = False
                End If

            Case Telerik.Web.UI.GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Compute("sum(total)", "estatusid<>2")) Then
                        e.Item.Cells(10).Text = FormatCurrency(ds.Tables(0).Compute("sum(total)", "estatusid<>2"), 2).ToString
                        e.Item.Cells(10).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(10).Font.Bold = True
                    End If
                    If Not IsDBNull(ds.Tables(0).Compute("sum(monto)", "estatusid<>2")) Then
                        e.Item.Cells(11).Text = FormatCurrency(ds.Tables(0).Compute("sum(monto)", "estatusid<>2"), 2).ToString
                        e.Item.Cells(11).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(11).Font.Bold = True
                    End If
                End If
        End Select
    End Sub

    Private Sub DownloadXML(ByVal cfdid As Long)
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim connF As New SqlConnection(Session("conexion"))
        Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                serie = rs("serie").ToString
                folio = rs("folio").ToString
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Dim FilePath = Server.MapPath("~/portalcfd/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml"
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If
        ''
    End Sub

    Private Sub DownloadPDF(ByVal cfdid As Long)
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim connF As New SqlConnection(Session("conexion"))
        Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                serie = rs("serie").ToString
                folio = rs("folio").ToString
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Dim FilePath = Server.MapPath("~/portalcfd/pdf/") & "link_" & serie.ToString & folio.ToString & ".pdf"
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
            'Else
            '    GuardaPDF(GeneraPDF(cfdid, serie, folio), Server.MapPath("~/portalcfd/pdf") & "\link_" & serie.ToString & folio.ToString & ".pdf")
            '    Dim FileName As String = Path.GetFileName(FilePath)
            '    Response.Clear()
            '    Response.ContentType = "application/octet-stream"
            '    Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            '    Response.Flush()
            '    Response.WriteFile(FilePath)
            '    Response.End()
        End If
        ''
    End Sub

    Private Sub DownloadTicket(ByVal remisionid As Long)
        Dim FilePath = Server.MapPath("~/portalcfd/pos/Tickets/") & "T" & remisionid.ToString & ".pdf"
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        Else
            GuardaPDF(GeneraPDFRemision(remisionid), FilePath)
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If
    End Sub

    Private Function GeneraPDFRemision(ByVal remisionid As Long) As Telerik.Reporting.Report
        Dim conn As New SqlConnection(Session("conexion"))
        '
        Dim folio As String = ""
        Dim cajero As String = ""
        Dim cliente As String = ""
        Dim fecha As String = ""
        Dim total As Decimal = 0
        '
        Dim ds As DataSet = New DataSet
        '
        Try
            '
            Dim cmd As New SqlCommand("EXEC pConsultaRemision @remisionid='" & remisionid.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            '
            If rs.Read Then
                folio = rs("id").ToString
                cajero = rs("cajero")
                cliente = rs("cliente")
                fecha = rs("fecha")
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
        Dim reporte As New FormatosPDF.formato_remision_trevisa
        '
        reporte.ReportParameters("plantillaId").Value = 5
        reporte.ReportParameters("remisionId").Value = folio
        reporte.ReportParameters("txtFolio").Value = folio
        reporte.ReportParameters("txtCajero").Value = cajero
        reporte.ReportParameters("txtCliente").Value = cliente
        reporte.ReportParameters("txtFecha").Value = fecha
        reporte.ReportParameters("txtTotal").Value = FormatCurrency(total, 2).ToString
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

#Region "Manejo de PDF"

    'Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
    '    Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
    '    Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
    '    Using fs As New FileStream(fileName, FileMode.Create)
    '        fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
    '    End Using
    'End Sub

    'Private Function GeneraPDF(ByVal cfdid As Long) As Telerik.Reporting.Report
    '    Dim conn As New SqlConnection(Session("conexion"))

    '    Dim tipocontribuyenteid As Integer = 0
    '    Dim numeroaprobacion As String = ""
    '    Dim anoAprobacion As String = ""
    '    Dim fechaHora As String = ""
    '    Dim noCertificado As String = ""
    '    Dim razonsocial As String = ""
    '    Dim callenum As String = ""
    '    Dim colonia As String = ""
    '    Dim ciudad As String = ""
    '    Dim rfc As String = ""
    '    Dim em_razonsocial As String = ""
    '    Dim em_callenum As String = ""
    '    Dim em_colonia As String = ""
    '    Dim em_ciudad As String = ""
    '    Dim em_rfc As String = ""
    '    Dim em_regimen As String = ""
    '    Dim importe As Decimal = 0
    '    Dim importetasacero As Decimal = 0
    '    Dim totaldescuento As Decimal = 0
    '    Dim iva As Decimal = 0
    '    Dim ieps As Decimal = 0
    '    Dim total As Decimal = 0
    '    Dim CantidadTexto As String = ""
    '    Dim condiciones As String = ""
    '    Dim enviara As String = ""
    '    Dim instrucciones As String = ""
    '    Dim pedimento As String = ""
    '    Dim retencion As Decimal = 0
    '    Dim tipoid As Integer = 0
    '    Dim divisaid As Integer = 1
    '    Dim expedicionLinea1 As String = ""
    '    Dim expedicionLinea2 As String = ""
    '    Dim expedicionLinea3 As String = ""
    '    Dim porcentaje As Decimal = 0
    '    Dim plantillaid As Integer = 1
    '    Dim tipopago As String = ""
    '    Dim formapago As String = ""
    '    Dim numctapago As String = ""
    '    Dim serie As String = ""
    '    Dim folio As Long = 0
    '    Dim uuid As String = ""

    '    Dim ds As DataSet = New DataSet

    '    Try

    '        Dim cmd As New SqlCommand("EXEC pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", conn)
    '        conn.Open()
    '        Dim rs As SqlDataReader
    '        rs = cmd.ExecuteReader()

    '        If rs.Read Then
    '            serie = rs("serie")
    '            folio = rs("folio")
    '            tipoid = rs("tipoid")
    '            em_razonsocial = rs("em_razonsocial")
    '            em_callenum = rs("em_callenum")
    '            em_colonia = rs("em_colonia")
    '            em_ciudad = rs("em_ciudad")
    '            em_rfc = rs("em_rfc")
    '            em_regimen = rs("regimen")
    '            razonsocial = rs("razonsocial")
    '            callenum = rs("callenum")
    '            colonia = rs("colonia")
    '            ciudad = rs("ciudad")
    '            rfc = rs("rfc")
    '            importe = rs("importe")
    '            importetasacero = rs("importetasacero")
    '            totaldescuento = rs("descuento")
    '            iva = rs("iva")
    '            ieps = rs("ieps")
    '            total = rs("total")
    '            divisaid = rs("divisaid")
    '            fechaHora = rs("fecha_factura").ToString
    '            condiciones = "Condiciones: " & rs("condiciones").ToString
    '            enviara = rs("enviara").ToString
    '            instrucciones = rs("instrucciones")
    '            If rs("aduana") = "" Or rs("numero_pedimento") = "" Then
    '                pedimento = ""
    '            Else
    '                pedimento = "Aduana: " & rs("aduana") & vbCrLf & "Fecha: " & rs("fecha_pedimento").ToString & vbCrLf & "Número: " & rs("numero_pedimento").ToString
    '            End If
    '            expedicionLinea1 = rs("expedicionLinea1")
    '            expedicionLinea2 = rs("expedicionLinea2")
    '            expedicionLinea3 = rs("expedicionLinea3")
    '            porcentaje = rs("porcentaje")
    '            plantillaid = rs("plantillaid")
    '            tipocontribuyenteid = rs("tipocontribuyenteid")
    '            tipopago = rs("tipopago")
    '            formapago = rs("formapago")
    '            numctapago = rs("numctapago")
    '            uuid = rs("uuid")
    '        End If
    '        rs.Close()
    '        '
    '    Catch ex As Exception
    '        '
    '        Response.Write(ex.ToString)
    '    Finally

    '        conn.Close()
    '        conn.Dispose()
    '        conn = Nothing
    '    End Try

    '    Dim largo = Len(CStr(Format(CDbl(total), "#,###.00")))
    '    Dim decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)

    '    If System.Configuration.ConfigurationManager.AppSettings("divisas") = 1 Then
    '        If divisaid = 1 Then
    '            CantidadTexto = "(" + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
    '        Else
    '            CantidadTexto = "(" + Num2Text(total - decimales) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD. )"
    '        End If
    '    Else
    '        CantidadTexto = "(" + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
    '    End If

    '    Select Case tipoid
    '        Case 3, 6, 7      ' Recibo de honorarios y arrendamiento
    '            Dim reporte As New Formatos.formato_cfdi_honorarios
    '            reporte.ReportParameters("plantillaId").Value = plantillaid
    '            reporte.ReportParameters("cfdiId").Value = cfdid
    '            Select Case tipoid
    '                Case 3
    '                    reporte.ReportParameters("txtDocumento").Value = "Recibo de Arrendamiento No.    " & serie.ToString & folio.ToString
    '                Case 6
    '                    reporte.ReportParameters("txtDocumento").Value = "Recibo de Honorarios No.    " & serie.ToString & folio.ToString
    '                Case 7
    '                    reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
    '            End Select
    '            reporte.ReportParameters("txtCondicionesPago").Value = condiciones
    '            reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & uuid.ToString & ".png")
    '            reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
    '            reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "fecha", "cfdi:Comprobante")
    '            reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
    '            reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "UUID", "tfd:TimbreFiscalDigital")
    '            reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "noCertificado", "cfdi:Comprobante")
    '            reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "noCertificadoSAT", "tfd:TimbreFiscalDigital")
    '            reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "nombre", "cfdi:Receptor")
    '            reporte.ReportParameters("txtClienteCalleNum").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "calle", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "noExterior", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "noInterior", "cfdi:Domicilio")
    '            reporte.ReportParameters("txtClienteColonia").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "colonia", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "codigoPostal", "cfdi:Domicilio")
    '            reporte.ReportParameters("txtClienteCiudadEstado").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "municipio", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "estado", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "pais", "cfdi:Domicilio")
    '            reporte.ReportParameters("txtClienteRFC").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "rfc", "cfdi:Receptor")
    '            '
    '            reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "sello", "cfdi:Comprobante")
    '            reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "selloSAT", "tfd:TimbreFiscalDigital")
    '            '
    '            reporte.ReportParameters("txtPedimento").Value = pedimento
    '            reporte.ReportParameters("txtEnviarA").Value = enviara

    '            '
    '            If tipocontribuyenteid = 1 Then
    '                reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
    '                reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
    '                reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
    '                reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
    '                reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
    '                reporte.ReportParameters("txtRetIva").Value = FormatCurrency(0, 2).ToString
    '                reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva), 2).ToString
    '                '
    '                '   Ajusta cantidad con texto
    '                '
    '                total = FormatNumber((importe + iva), 2)
    '                largo = Len(CStr(Format(CDbl(total), "#,###.00")))
    '                decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
    '                CantidadTexto = "(" + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
    '                '
    '            Else
    '                If tipoid = 7 Then
    '                    reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
    '                    reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
    '                    reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
    '                    reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
    '                    reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
    '                    reporte.ReportParameters("txtRetIva").Value = FormatCurrency((iva * 0.1), 2).ToString
    '                    reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva) - ((iva * 0.1)), 2).ToString
    '                    '
    '                    '   Ajusta cantidad con texto
    '                    '
    '                    total = FormatNumber((importe + iva) - ((iva * 0.1)), 2)
    '                    largo = Len(CStr(Format(CDbl(total), "#,###.00")))
    '                    decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
    '                    CantidadTexto = "(" + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
    '                    '
    '                Else
    '                    reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
    '                    reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
    '                    reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
    '                    reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
    '                    reporte.ReportParameters("txtRetISR").Value = FormatCurrency(importe * 0.1, 2).ToString
    '                    reporte.ReportParameters("txtRetIva").Value = FormatCurrency((iva / 3) * 2, 2).ToString
    '                    reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva) - (importe * 0.1) - ((iva / 3) * 2), 2).ToString
    '                    '
    '                    '   Ajusta cantidad con texto
    '                    '
    '                    total = FormatNumber((importe + iva) - (importe * 0.1) - ((iva / 3) * 2), 2)
    '                    largo = Len(CStr(Format(CDbl(total), "#,###.00")))
    '                    decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
    '                    CantidadTexto = "(" + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
    '                    '
    '                End If

    '            End If
    '            '
    '            '
    '            reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
    '            '
    '            '
    '            reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento(serie, folio)
    '            reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
    '            reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
    '            If porcentaje > 0 Then
    '                reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
    '            End If
    '            '
    '            reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
    '            reporte.ReportParameters("txtFormaPago").Value = tipopago.ToString
    '            reporte.ReportParameters("txtMetodoPago").Value = formapago.ToString
    '            reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
    '            reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
    '            Return reporte
    '        Case Else
    '            Dim reporte As New Formatos.formato_cfdi_bonbon
    '            reporte.ReportParameters("plantillaId").Value = plantillaid
    '            reporte.ReportParameters("cfdiId").Value = cfdid
    '            Select Case tipoid
    '                Case 1, 4, 7
    '                    reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
    '                Case 2, 8
    '                    reporte.ReportParameters("txtDocumento").Value = "Nota de Crédito No.    " & serie.ToString & folio.ToString
    '                Case 5
    '                    reporte.ReportParameters("txtDocumento").Value = "Carta Porte No.    " & serie.ToString & folio.ToString
    '                    reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO     EFECTOS FISCALES AL PAGO"
    '                Case 6
    '                    reporte.ReportParameters("txtDocumento").Value = "Recibo de Honorarios No.    " & serie.ToString & folio.ToString
    '                Case Else
    '                    reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
    '            End Select
    '            reporte.ReportParameters("txtCondicionesPago").Value = condiciones
    '            reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & uuid.ToString & ".png")
    '            reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
    '            reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "fecha", "cfdi:Comprobante")
    '            reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
    '            reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "UUID", "tfd:TimbreFiscalDigital")
    '            reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "noCertificado", "cfdi:Comprobante")
    '            reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "noCertificadoSAT", "tfd:TimbreFiscalDigital")
    '            reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "nombre", "cfdi:Receptor")
    '            reporte.ReportParameters("txtClienteCalleNum").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "calle", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "noExterior", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "noInterior", "cfdi:Domicilio")
    '            reporte.ReportParameters("txtClienteColonia").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "colonia", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "codigoPostal", "cfdi:Domicilio")
    '            reporte.ReportParameters("txtClienteCiudadEstado").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "municipio", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "estado", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "pais", "cfdi:Domicilio")
    '            reporte.ReportParameters("txtClienteRFC").Value = "R.F.C. " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "rfc", "cfdi:Receptor")
    '            '
    '            reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "sello", "cfdi:Comprobante")
    '            reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\" & uuid.ToString & ".xml", "selloSAT", "tfd:TimbreFiscalDigital")
    '            '
    '            reporte.ReportParameters("txtInstrucciones").Value = instrucciones
    '            reporte.ReportParameters("txtPedimento").Value = pedimento
    '            reporte.ReportParameters("txtEnviarA").Value = enviara
    '            reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
    '            '
    '            reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe, 2).ToString
    '            reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
    '            reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
    '            reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
    '            reporte.ReportParameters("txtIEPS").Value = FormatCurrency(ieps, 2).ToString
    '            reporte.ReportParameters("txtTotal").Value = FormatCurrency(total, 2).ToString
    '            reporte.ReportParameters("txtDescuento").Value = FormatCurrency(totaldescuento, 2).ToString
    '            '
    '            reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento(serie, folio)
    '            reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
    '            reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
    '            If porcentaje > 0 Then
    '                reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
    '            End If
    '            '
    '            If tipoid = 5 Then
    '                retencion = FormatNumber((importe * 0.04), 2)
    '                reporte.ReportParameters("txtRetencion").Value = FormatCurrency(retencion, 2).ToString
    '                reporte.ReportParameters("txtTotal").Value = FormatCurrency(total - retencion, 2).ToString
    '                largo = Len(CStr(Format(CDbl(total - retencion), "#,###.00")))
    '                decimales = Mid(CStr(Format(CDbl(total - retencion), "#,###.00")), largo - 2)
    '                If divisaid = 1 Then
    '                    CantidadTexto = "(" + Num2Text((total - retencion - decimales)) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
    '                Else
    '                    CantidadTexto = "(" + Num2Text((total - retencion - decimales)) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD )"
    '                End If
    '                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
    '            End If
    '            '
    '            '
    '            reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
    '            reporte.ReportParameters("txtFormaPago").Value = tipopago.ToString
    '            reporte.ReportParameters("txtMetodoPago").Value = formapago.ToString
    '            reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
    '            reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
    '            '
    '            Return reporte
    '    End Select

    'End Function

#End Region

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

    Private Function CadenaOriginalComplemento(ByVal serie As String, ByVal folio As String) As String
        '
        '   Obtiene los valores del timbre de respuesta
        '
        Dim selloSAT As String = ""
        Dim noCertificadoSAT As String = ""
        Dim selloCFD As String = ""
        Dim fechaTimbrado As String = ""
        Dim UUID As String = ""
        Dim Version As String = ""
        '
        Dim FlujoReader As XmlTextReader = Nothing
        Dim i As Integer
        ' leer del fichero e ignorar los nodos vacios
        FlujoReader = New XmlTextReader(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "iu_" & serie.ToString & folio.ToString & "_timbrado.xml")
        FlujoReader.WhitespaceHandling = WhitespaceHandling.None
        ' analizar el fichero y presentar cada nodo
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
                            ElseIf FlujoReader.Name = "noCertificadoSAT" Then
                                noCertificadoSAT = FlujoReader.Value
                            ElseIf FlujoReader.Name = "selloCFD" Then
                                selloCFD = FlujoReader.Value
                            ElseIf FlujoReader.Name = "version" Then
                                Version = FlujoReader.Value
                            End If
                        Next
                    End If
            End Select
        End While
        ''
        Dim cadena As String = ""
        cadena = "||" & Version & "|" & UUID & "|" & fechaTimbrado & "|" & selloCFD & "|" & noCertificadoSAT & "||"
        Return cadena
        ''
    End Function

    Private Sub CancelaSIFEI(ByVal cfdi As Long)
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
            Dim byteCertData As Byte() = ReadFile(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/certificados/") & CertificadoCliente() & ".pfx")
            Dim sifei As New SIFEI33.SIFEIService()
            Dim result = sifei.cancelaCFDI("LST101206985", "d77ab121", RFCEmisor, byteCertData, ContrasenaPfx(), uuids.ToArray())

            Dim xml As New XmlDocument()
            xml.LoadXml(result)
            xml.Save(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml/") & "acuse_" & uuids(0).ToString & ".xml")

            Dim EstatusUUID As String = ""
            Dim DescricionCodigo As String = ""
            EstatusUUID = GetXMLValue(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml/") & "acuse_" & uuids(0).ToString & ".xml", "EstatusUUID")

            If EstatusUUID = "201" Then

                'DescricionCodigo = "UUID Cancelado exitosamente"
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pCFD @cmd=21, @cfdid='" & cfdi.ToString & "'")
                ObjData = Nothing

            ElseIf EstatusUUID = "202" Then
                DescricionCodigo = "UUID Previamente cancelado"
            ElseIf EstatusUUID = "203" Then
                DescricionCodigo = "UUID No corresponde el RFC del Emisor y de quien solicita la cancelación"
            ElseIf EstatusUUID = "205" Then
                DescricionCodigo = "UUID No existe"
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

    Private Sub CancelaRemision(ByVal remisionid As String)
        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("exec pPosServices @cmd=23, @remisionid='" & remisionid.ToString & "'")
        ObjData = Nothing
    End Sub

    Private Sub CancelaRemisionRegresaInventario(ByVal remisionid As String)
        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("exec pPosServices @cmd=26, @remisionid='" & remisionid.ToString & "'")
        ObjData = Nothing
    End Sub

    Private Sub grdVentas_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdVentas.NeedDataSource        
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

        Dim ObjData As New DataControl(1)
        Dim sql As String = ""
        sql = "exec pVentas @cmd=1, @clienteid='" & cmbCliente.SelectedValue.ToString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @folio='" & txtTicket.Text & "'"
        ds = ObjData.FillDataSet(sql)
        grdVentas.DataSource = ds
        ObjData = Nothing

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
    End Sub

End Class