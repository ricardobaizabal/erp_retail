Imports System.Threading
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.Serialization
Imports Org.BouncyCastle.OpenSsl
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.Security
Imports ThoughtWorks.QRCode.Codec
Imports ThoughtWorks.QRCode.Codec.Util
Imports Telerik.Reporting.Processing
Imports Ionic.Zip
Imports System.Web.Services.Protocols
Imports System.Net.Security
Public Class FacturaGlobal40
    Inherits System.Web.UI.Page
    Private subtotal As Decimal = 0
    Private descuento As Decimal = 0
    Private iva As Decimal = 0
    Private ieps As Decimal = 0
    Private ieps_8 As Decimal = 0
    Private total As Decimal = 0
    Private selloCFD As String = ""
    Private cadenaOriginal As String = ""
    Private serie As String = ""
    Private folio As Long = 0
    Private archivoLlavePrivada As String = ""
    Private contrasenaLlavePrivada As String = ""
    Private archivoCertificado As String = ""
    Private tipocontribuyenteid As Integer = 0
    Private cadOrigComp As String

    Private m_xmlDOM As New XmlDocument
    Const URI_SAT = "http://www.sat.gob.mx/cfd/3"
    Private listErrores As New List(Of String)
    Private Comprobante As XmlNode
    Public Const NOMBRE_XSLT = "cadenaoriginal_3_3.xslt"
    Public Const DIR_SAT = "\SAT\"
    Dim UUID As String = ""

    Private qrBackColor As Integer = System.Drawing.Color.FromArgb(255, 255, 255, 255).ToArgb
    Private qrForeColor As Integer = System.Drawing.Color.FromArgb(255, 0, 0, 0).ToArgb
    Private data As Byte()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '
        '   Protege contra doble clic la creación de la factura
        '
        btnCreateInvoice.Attributes.Add("onclick", "javascript:" + btnCreateInvoice.ClientID + ".disabled=true;" + ClientScript.GetPostBackEventReference(btnCreateInvoice, ""))
        '
        If Not IsPostBack Then
            calFechaInicio.SelectedDate = Date.Now()
            calFechaFin.SelectedDate = Date.Now()
        End If
        '
    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        panelResume.Visible = True
        Call CargaTotalesInicio()
    End Sub

    Private Sub btnCreateInvoice_Click(sender As Object, e As EventArgs) Handles btnCreateInvoice.Click
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim cfdid As Long = 0
        Dim Timbrado As Boolean = False
        Dim MensageError As String = ""
        ieps_8 = 0
        '
        '   Rutina de generación de XML CFDI Versión 3.3
        '
        cfdid = NuevoCFDI("01", 1) '01 - Efectivo, 1 - Pesos (MXN)
        '
        '   Totales Factura
        '
        Call CargaTotalesInicio()
        '
        '   Guadar Metodo de Pago
        '
        Call GuadarMetodoPago(cfdid)
        '
        m_xmlDOM = CrearDOM()
        '
        '   Asigna Serie y Folio
        '
        Dim SQLUpdate As String = ""

        SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & cfdid.ToString & "', @metodopagoid='PUE', @tipodocumentoid=1, @formapagoid='01', @tipopagoid='1', @lugarexpedicion='" & CargaLugarExpedicion() & "'"

        Dim connF As New SqlConnection(Session("conexion"))
        Dim cmdF As New SqlCommand(SQLUpdate, connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                serie = rs("serie").ToString
                folio = rs("folio")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Comprobante = CrearNodoComprobante("01", 1)
        '
        m_xmlDOM.AppendChild(Comprobante)
        IndentarNodo(Comprobante, 1)
        '
        '   Obtiene datos del emisor
        '
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("exec pCFD @cmd=11", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                CrearNodoEmisor(Comprobante, rs("razonsocial"), rs("fac_rfc"), rs("regimenid"))
                IndentarNodo(Comprobante, 1)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
        '
        '   Obtiene datos del receptor
        '
        Dim connR As New SqlConnection(Session("conexion"))
        Dim cmdR As New SqlCommand("exec pCFD @cmd=42", connR)
        Try

            connR.Open()

            Dim rs As SqlDataReader
            rs = cmdR.ExecuteReader()

            If rs.Read Then
                CrearNodoReceptor(Comprobante, rs("razonsocial"), rs("fac_rfc"))
                IndentarNodo(Comprobante, 1)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            connR.Close()
            connR.Dispose()
            connR = Nothing
        End Try
        '
        '   Agrega Partidas
        '
        Dim AplicaImpuestoTasa0 As Boolean = False
        Dim Conceptos As XmlElement
        Dim Concepto As XmlElement
        Dim ImpuestosConcepto As XmlElement
        Dim TrasladosConcepto As XmlElement
        Dim TrasladoConcepto As XmlElement

        Conceptos = CrearNodo("cfdi:Conceptos")
        IndentarNodo(Conceptos, 2)

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

        Dim connP As New SqlConnection(Session("conexion"))
        Dim cmdP As New SqlCommand("exec pCFD @cmd=43, @cfdid='" & cfdid.ToString & "', @fhaini='" & calFechaInicio.SelectedDate.Value.ToShortDateString & "', @fhafin='" & calFechaFin.SelectedDate.Value.ToShortDateString & "'", connP)
        cmdP.CommandTimeout = 0
        Try
            connP.Open()
            '
            Dim rs As SqlDataReader
            rs = cmdP.ExecuteReader()
            '
            While rs.Read
                Concepto = CrearNodo("cfdi:Concepto")
                Concepto.SetAttribute("ClaveProdServ", rs("claveprodserv"))
                Concepto.SetAttribute("NoIdentificacion", rs("codigo"))
                Concepto.SetAttribute("Cantidad", rs("cantidad"))
                Concepto.SetAttribute("ClaveUnidad", rs("claveunidad"))
                Concepto.SetAttribute("Unidad", rs("unidad"))
                Concepto.SetAttribute("Descripcion", rs("descripcion").ToString.Trim())

                If rs("descuento") > 0 Then
                    Concepto.SetAttribute("Descuento", Math.Round(rs("descuento"), 6))
                End If

                Concepto.SetAttribute("ValorUnitario", Math.Round(rs("precio"), 6))
                Concepto.SetAttribute("Importe", Math.Round(rs("importe"), 6))

                ImpuestosConcepto = CrearNodo("cfdi:Impuestos")

                TrasladosConcepto = CrearNodo("cfdi:Traslados")

                If CDbl(rs("importe_tasa16") > 0) Then
                    TrasladoConcepto = CrearNodo("cfdi:Traslado")
                    TrasladoConcepto.SetAttribute("Impuesto", "002")
                    TrasladoConcepto.SetAttribute("TipoFactor", "Tasa")
                    TrasladoConcepto.SetAttribute("TasaOCuota", "0.160000")
                    TrasladoConcepto.SetAttribute("Importe", Math.Round(rs("iva"), 6))
                    TrasladoConcepto.SetAttribute("Base", Math.Round(rs("importe_tasa16"), 6))
                    TrasladosConcepto.AppendChild(TrasladoConcepto)
                End If

                If CDbl(rs("importe_tasa0") > 0) Then
                    AplicaImpuestoTasa0 = True
                    TrasladoConcepto = CrearNodo("cfdi:Traslado")
                    TrasladoConcepto.SetAttribute("Impuesto", "002")
                    TrasladoConcepto.SetAttribute("TipoFactor", "Tasa")
                    TrasladoConcepto.SetAttribute("TasaOCuota", "0.000000")
                    TrasladoConcepto.SetAttribute("Importe", "0.000000")
                    TrasladoConcepto.SetAttribute("Base", Math.Round(rs("importe_tasa0"), 6))
                    TrasladosConcepto.AppendChild(TrasladoConcepto)
                End If

                If CDbl(rs("ieps") > 0) Then
                    Dim TasaOCuota As String = ""
                    If CDbl(rs("ieps") = 8) Then
                        ieps_8 = ieps_8 + rs("ieps_total")
                        TasaOCuota = "0.080000"
                        TrasladoConcepto = CrearNodo("cfdi:Traslado")
                        TrasladoConcepto.SetAttribute("Impuesto", "003")
                        TrasladoConcepto.SetAttribute("TipoFactor", "Tasa")
                        TrasladoConcepto.SetAttribute("TasaOCuota", TasaOCuota)
                        TrasladoConcepto.SetAttribute("Importe", Math.Round(rs("ieps_total"), 6))
                        TrasladoConcepto.SetAttribute("Base", Math.Round(rs("base_8"), 6))
                        TrasladosConcepto.AppendChild(TrasladoConcepto)
                    End If
                End If

                ImpuestosConcepto.AppendChild(TrasladosConcepto)
                Concepto.AppendChild(ImpuestosConcepto)
                Conceptos.AppendChild(Concepto)
                IndentarNodo(Conceptos, 2)
                Concepto = Nothing
            End While
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            connP.Close()
            connP.Dispose()
            connP = Nothing
        End Try

        Comprobante.AppendChild(Conceptos)
        '
        '   Totales Factura
        '
        Call CargaTotales(cfdid)
        '
        '   Agrega impuestos
        '
        Dim AgregarIeps As Boolean = False
        Dim TasaOCuotas As String = ""
        Dim TipoFactor As String = ""
        Dim TipoImpuesto As String = ""
        Dim Impuestos As XmlElement
        Dim Traslados As XmlElement
        Dim Traslado As XmlElement

        Impuestos = CrearNodo("cfdi:Impuestos")

        If iva > 0 Then
            Impuestos.SetAttribute("TotalImpuestosTrasladados", Math.Round(iva + ieps, 2))
        Else
            Impuestos.SetAttribute("TotalImpuestosTrasladados", "0.00")
        End If

        If iva > 0 Then
            TasaOCuotas = "0.160000"
            TipoFactor = "Tasa"
            TipoImpuesto = "002"
        End If

        Traslados = CrearNodo("cfdi:Traslados")
        IndentarNodo(Traslados, 3)
        Impuestos.AppendChild(Traslados)

        If iva > 0 Then
            Traslado = CrearNodo("cfdi:Traslado")
            Traslado.SetAttribute("Impuesto", TipoImpuesto)
            Traslado.SetAttribute("TipoFactor", TipoFactor)
            Traslado.SetAttribute("TasaOCuota", TasaOCuotas)
            Traslado.SetAttribute("Importe", Math.Round(iva, 2))
            Traslados.AppendChild(Traslado)
        End If

        If AplicaImpuestoTasa0 = True Then
            TipoImpuesto = "002"
            TipoFactor = "Tasa"
            TasaOCuotas = "0.000000"

            Traslado = CrearNodo("cfdi:Traslado")
            Traslado.SetAttribute("Impuesto", TipoImpuesto)
            Traslado.SetAttribute("TipoFactor", TipoFactor)
            Traslado.SetAttribute("TasaOCuota", TasaOCuotas)
            Traslado.SetAttribute("Importe", "0.00")
            Traslados.AppendChild(Traslado)
        End If

        If ieps_8 > 0 Then
            TasaOCuotas = "0.080000"
            TipoImpuesto = "003"
            Traslado = CrearNodo("cfdi:Traslado")
            Traslado.SetAttribute("Impuesto", TipoImpuesto)
            Traslado.SetAttribute("TipoFactor", TipoFactor)
            Traslado.SetAttribute("TasaOCuota", TasaOCuotas)
            Traslado.SetAttribute("Importe", Math.Round(ieps_8, 2))
            Traslados.AppendChild(Traslado)
        End If
        '
        IndentarNodo(Traslados, 2)
        Impuestos.AppendChild(Traslados)
        IndentarNodo(Impuestos, 1)
        Comprobante.AppendChild(Impuestos)
        '
        '   Sellar Comprobante
        '
        SellarCFD(Comprobante, cfdid)
        m_xmlDOM.InnerXml = (Replace(m_xmlDOM.InnerXml, "schemaLocation", "xsi:schemaLocation", , , CompareMethod.Text))
        m_xmlDOM.Save(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "\" & "link_" & serie.ToString & folio.ToString & ".xml")
        '
        '   Timbrado SIFEI
        '
        If folio > 0 Then
            '
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType) Or DirectCast(768, System.Net.SecurityProtocolType) Or DirectCast(192, System.Net.SecurityProtocolType) Or DirectCast(48, System.Net.SecurityProtocolType)
            '
            Dim Usuario As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIUsuario")
            Dim Password As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIContrasena")
            Dim IdEquipo As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIIdEquipo")
            '
            '   Produccion
            '
            Dim TimbreSifei As New SIFEI33.SIFEIService()
            'Dim TimbreSifei As New SIFEIPruebasV33.SIFEIService()
            '
            Call Comprimir()
            '
            Try
                Dim bytes() As Byte

                bytes = TimbreSifei.getCFDI(Usuario, Password, data, "", IdEquipo)
                Descomprimir(bytes)
                Timbrado = True
            Catch ex As SoapException
                MensageError = ex.Detail.InnerText
                Timbrado = False
                Call cfdnotimbrado(cfdid)
            End Try

            If Timbrado = True Then
                '
                '   Obtiene UUID
                '
                Dim UUID As String = ""
                Dim filePath As String = Server.MapPath("~/clientes/" & Session("appkey").ToString & "/xml") & "/" & "link_" & serie.ToString & folio.ToString & "_timbrado.xml"
                UUID = GetXmlAttribute(filePath, "UUID", "tfd:TimbreFiscalDigital")
                '
                '   Marca el cfd como timbrado
                '
                Call cfdtimbrado(cfdid, UUID)
                '
                '   Renombra XML por UUID
                '
                If File.Exists(filePath) Then
                    Dim Path = Server.MapPath("~/clientes/" & Session("appkey").ToString & "/xml/")
                    System.IO.File.Copy(filePath, Path & UUID.ToString & ".xml")
                End If
                '
                '   Genera Código Bidimensional
                '
                Call generacbb(UUID)
                '
                '   Genera PDF
                '
                If Not File.Exists(Server.MapPath("~/clientes/" & Session("appkey").ToString & "/pdf") & "/" & UUID.ToString & ".pdf") Then
                    GuardaPDF(GeneraPDF(cfdid), Server.MapPath("~/clientes/" & Session("appkey").ToString & "/pdf") & "/" & UUID.ToString & ".pdf")
                End If
            Else
                Timbrado = False
                Call cfdnotimbrado(cfdid)
            End If
        Else
            MensageError = "No se encontraron folios disponibles."
            Timbrado = False
        End If

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")

        cfdid = 0

        If Timbrado = True Then
            Response.Redirect("~/portalcfd/cfd.aspx")
        Else
            txtErrores.Text = MensageError.ToString
            RadWindow1.VisibleOnPageLoad = True
        End If

    End Sub

    Private Sub btnCancelInvoice_Click(sender As Object, e As EventArgs) Handles btnCancelInvoice.Click
        panelResume.Visible = False
        calFechaInicio.Clear()
        calFechaFin.Clear()
        lblSubTotalValue.Text = FormatCurrency(0, 2).ToString
        lblDescuentoValue.Text = FormatCurrency(0, 2).ToString
        lblIVAValue.Text = FormatCurrency(0, 2).ToString
        lblIEPSValue.Text = FormatCurrency(0, 2).ToString
        lblTotalValue.Text = FormatCurrency(0, 2).ToString
    End Sub

    Private Sub CargaTotalesInicio()

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

        Try
            Dim ds As DataSet = New DataSet
            Dim ObjData As New DataControl(1)
            ds = ObjData.FillDataSet("exec pTotalesFacturaGlobal @fecha_inicio='" & calFechaInicio.SelectedDate.Value.ToShortDateString & "', @fecha_fin='" & calFechaFin.SelectedDate.Value.ToShortDateString & "'")
            ObjData = Nothing

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    subtotal = row("Subtotal")
                    descuento = row("Descuento")
                    iva = row("Iva")
                    ieps = row("Ieps")
                    total = row("Total")

                    lblSubTotalValue.Text = FormatCurrency(row("Subtotal"), 2).ToString
                    lblDescuentoValue.Text = FormatCurrency(row("Descuento"), 2).ToString
                    lblIVAValue.Text = FormatCurrency(row("Iva"), 2).ToString
                    lblIEPSValue.Text = FormatCurrency(row("Ieps"), 2).ToString
                    lblTotalValue.Text = FormatCurrency(row("Total"), 2).ToString
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")

    End Sub

    Private Sub CargaTotales(ByVal cfdid As Long)

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

        Try
            Dim ds As DataSet = New DataSet
            Dim ObjData As New DataControl(1)
            ds = ObjData.FillDataSet("exec pTotalesFacturaGlobal @cfdid='" & cfdid.ToString & "'")
            ObjData = Nothing

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    subtotal = row("Subtotal")
                    descuento = row("Descuento")
                    iva = row("Iva")
                    total = row("Total")

                    lblSubTotalValue.Text = FormatCurrency(row("Subtotal"), 2).ToString
                    lblDescuentoValue.Text = FormatCurrency(row("Descuento"), 2).ToString
                    lblIVAValue.Text = FormatCurrency(row("Iva"), 2).ToString
                    lblIEPSValue.Text = FormatCurrency(row("Ieps"), 2).ToString
                    lblTotalValue.Text = FormatCurrency(row("Total"), 2).ToString
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")

    End Sub

    Private Function NuevoCFDI(ByVal formapagoId As String, ByVal monedaid As Integer) As Long

        Dim cfdid As Long = 0

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("EXEC pCFD @cmd=41, @formapagoId='" & formapagoId.ToString & "', @monedaid='" & monedaid.ToString & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                cfdid = rs("cfdid")

            End If

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        Return cfdid

    End Function

    Private Sub GuadarMetodoPago(ByVal cfdid As Long)
        Dim Objdata As New DataControl(1)
        Objdata.RunSQLQuery("exec pCFD @cmd=25, @metodopagoid='PUE', @usocfdi='P01', @serieid='1', @cfdid='" & cfdid.ToString & "'")
        Objdata = Nothing
    End Sub

    Private Function CargaLugarExpedicion() As String
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("EXEC pCliente @cmd=3", conn)
        Dim LugarExpedicion As String = ""
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read() Then
                LugarExpedicion = rs("fac_cp")
            End If

            rs.Close()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return LugarExpedicion

    End Function

    Private Function CrearDOM() As XmlDocument
        Dim oDOM As New XmlDocument
        Dim Nodo As XmlNode
        Nodo = oDOM.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
        oDOM.AppendChild(Nodo)
        Nodo = Nothing
        CrearDOM = oDOM
    End Function

    Private Function CrearNodoComprobante(ByVal formapagoid As String, ByVal condicionesid As Integer) As XmlNode
        Dim Comprobante As XmlNode
        Comprobante = m_xmlDOM.CreateElement("cfdi:Comprobante", URI_SAT)
        CrearAtributosComprobante(Comprobante, formapagoid, condicionesid)
        CrearNodoComprobante = Comprobante
    End Function

    Private Sub CrearAtributosComprobante(ByVal Nodo As XmlElement, ByVal formapagoid As String, ByVal condicionesid As Integer)
        Nodo.SetAttribute("xmlns:cfdi", URI_SAT)
        Nodo.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        Nodo.SetAttribute("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd")
        Nodo.SetAttribute("Version", "3.3")

        If serie.ToString.Length > 0 Then
            Nodo.SetAttribute("Serie", serie)
        End If

        Nodo.SetAttribute("Folio", folio)
        Nodo.SetAttribute("Fecha", Format(Now(), "yyyy-MM-ddThh:mm:ss"))
        Nodo.SetAttribute("Sello", "")
        Nodo.SetAttribute("FormaPago", formapagoid)
        Nodo.SetAttribute("NoCertificado", "")
        Nodo.SetAttribute("Certificado", "")

        If condicionesid > 0 Then
            Nodo.SetAttribute("CondicionesDePago", condicionesTxt(condicionesid))
        End If

        Nodo.SetAttribute("SubTotal", Math.Round(subtotal, 2))

        If descuento > 0 Then
            Nodo.SetAttribute("Descuento", Math.Round(descuento, 2))
        End If

        Nodo.SetAttribute("Moneda", "MXN")
        Nodo.SetAttribute("Total", Math.Round(subtotal, 2) - Math.Round(descuento, 2) + Math.Round(iva, 2) + Math.Round(ieps, 2))
        Nodo.SetAttribute("TipoDeComprobante", "I")
        Nodo.SetAttribute("MetodoPago", "PUE")
        Nodo.SetAttribute("LugarExpedicion", CargaLugarExpedicion())
    End Sub

    Private Function condicionesTxt(ByVal condicionesId As Integer) As String
        Dim condiciones As String = ""
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("select id, nombre from tblCondiciones where id='" & condicionesId.ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                condiciones = rs("nombre")
            End If

        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
        Return condiciones
    End Function

    Private Function CrearNodo(ByVal Nombre As String) As XmlNode
        CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, Nombre, URI_SAT)
    End Function

    Private Sub IndentarNodo(ByVal Nodo As XmlNode, ByVal Nivel As Long)
        Nodo.AppendChild(m_xmlDOM.CreateTextNode(vbNewLine & New String(ControlChars.Tab, Nivel)))
    End Sub

    Private Sub CrearNodoEmisor(ByVal Nodo As XmlNode, ByVal nombre As String, ByVal rfc As String, ByVal Regimen As String)
        Try
            Dim Emisor As XmlElement
            Emisor = CrearNodo("cfdi:Emisor")
            Emisor.SetAttribute("Nombre", nombre)
            Emisor.SetAttribute("Rfc", rfc)
            Emisor.SetAttribute("RegimenFiscal", Regimen)
            Nodo.AppendChild(Emisor)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CrearNodoReceptor(ByVal Nodo As XmlNode, ByVal nombre As String, ByVal rfc As String)
        Try
            Dim Receptor As XmlElement
            Receptor = CrearNodo("cfdi:Receptor")
            Receptor.SetAttribute("Rfc", rfc)
            Receptor.SetAttribute("Nombre", nombre)
            Receptor.SetAttribute("UsoCFDI", "G01")
            Nodo.AppendChild(Receptor)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Function ReadFile(ByVal strArchivo As String) As Byte()
        Dim f As New FileStream(strArchivo, FileMode.Open, FileAccess.Read)
        Dim size As Integer = CInt(f.Length)
        Dim data As Byte() = New Byte(size - 1) {}
        size = f.Read(data, 0, size)
        f.Close()
        Return data
    End Function

    Public Function FormatearSerieCert(ByVal Serie As String) As String
        Dim Resultado As String = ""
        Dim I As Integer
        For I = 2 To Len(Serie) Step 2
            Resultado = Resultado & Mid(Serie, I, 1)
        Next
        FormatearSerieCert = Resultado
    End Function

    Private Sub SellarCFD(ByVal NodoComprobante As XmlElement, ByVal cfdid As Long)
        Try
            Dim Certificado As String = ""
            Certificado = LeerCertificado()

            Dim Clave As String = ""
            Clave = LeerClave()

            Dim objCert As New X509Certificate2()
            Dim bRawData As Byte() = ReadFile(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/certificados/") & Certificado)
            objCert.Import(bRawData)
            Dim cadena As String = Convert.ToBase64String(bRawData)
            NodoComprobante.SetAttribute("NoCertificado", FormatearSerieCert(objCert.SerialNumber))
            NodoComprobante.SetAttribute("Total", Math.Round(subtotal, 2) - Math.Round(descuento, 2) + Math.Round(iva, 2) + Math.Round(ieps, 2))
            NodoComprobante.SetAttribute("Certificado", Convert.ToBase64String(bRawData))
            NodoComprobante.SetAttribute("Sello", GenerarSello(Clave, cfdid))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function LeerCertificado() As String
        Dim Certificado As String = ""

        Dim conn As New SqlConnection(Session("conexion"))
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Certificado = rs("archivo_certificado")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return Certificado

    End Function

    Private Function LeerClave() As String
        Dim Contrasena As String = ""

        Dim conn As New SqlConnection(Session("conexion"))
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Contrasena = rs("contrasena_llave_privada")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return Contrasena

    End Function

    Private Function Leerllave() As String
        Dim Llave As String = ""

        Dim conn As New SqlConnection(Session("conexion"))
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Llave = rs("archivo_llave_privada")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return Llave

    End Function

    Private Function GenerarSello(ByVal Clave As String, ByVal cfdid As Long) As String
        Try
            Dim pkey As New Chilkat.PrivateKey
            Dim pkeyXml As String
            Dim rsa As New Chilkat.Rsa
            pkey.LoadPkcs8EncryptedFile(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/llaves/") & Leerllave(), Clave)
            pkeyXml = pkey.GetXml()
            rsa.UnlockComponent("RSAT34MB34N_7F1CD986683M")
            rsa.ImportPrivateKey(pkeyXml)
            rsa.Charset = "utf-8"
            rsa.EncodingMode = "base64"
            rsa.LittleEndian = 0
            Dim base64Sig As String
            base64Sig = rsa.SignStringENC(GetCadenaOriginal(m_xmlDOM.InnerXml), "sha256")
            GenerarSello = base64Sig
            '
            '   Guarda sello y cadena en la BD
            '
            Dim ObjData As New DataControl(1)
            ObjData.RunSQLQuery("update tblCFD set sello='" & GenerarSello.ToString & "', cadenaoriginal='" & GetCadenaOriginal(m_xmlDOM.InnerXml).ToString & "' where id='" & cfdid.ToString & "'")
            ObjData = Nothing

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetCadenaOriginal(ByVal xmlCFD As String) As String
        Dim Cadena As String = ""
        Try
            Dim xslt As New XslCompiledTransform
            Dim xmldoc As New XmlDocument
            Dim navigator As XPath.XPathNavigator
            Dim output As New StringWriter
            xmldoc.LoadXml(xmlCFD)
            navigator = xmldoc.CreateNavigator()
            'xslt.Load(Server.MapPath("~/portalcfd/SAT/cadenaoriginal_3_3.xslt"))
            xslt.Load("http://www.sat.gob.mx/sitio_internet/cfd/3/cadenaoriginal_3_3/cadenaoriginal_3_3.xslt")
            xslt.Transform(navigator, Nothing, output)
            Cadena = output.ToString
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return Cadena

    End Function

    Private Function Comprimir()
        Dim zip As ZipFile = New ZipFile(serie.ToString & folio.ToString & ".zip")
        zip.AddFile(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "\" & "link_" & serie.ToString & folio.ToString & ".xml", "")
        Dim ms As New MemoryStream()
        zip.Save(ms)
        data = ms.ToArray
    End Function

    Private Function Descomprimir(ByVal data5 As Byte())
        Dim ms1 As New MemoryStream(data5)
        Dim zip1 As ZipFile = New ZipFile()
        zip1 = ZipFile.Read(ms1)

        Dim archivo As String = ""
        Dim DirectorioExtraccion As String = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml/").ToString
        Dim e As ZipEntry
        For Each e In zip1
            archivo = e.FileName.ToString
            e.Extract(DirectorioExtraccion, ExtractExistingFileAction.OverwriteSilently)
        Next

        Dim Path = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml/")
        If File.Exists(Path & archivo) Then
            System.IO.File.Copy(Path & archivo, Path & "link_" & serie.ToString & folio.ToString & "_timbrado.xml")
        End If
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

    Private Sub cfdnotimbrado(ByVal cfdid As Integer)
        Dim Objdata As New DataControl(1)
        Objdata.RunSQLQuery("exec pCFD @cmd=23, @cfdid='" & cfdid.ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub cfdtimbrado(ByVal cfdid As Long, ByVal uuid As String)

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

        Dim Objdata As New DataControl(1)
        Objdata.RunSQLQuery("exec pCFD @cmd=44, @cfdid='" & cfdid.ToString & "', @uuid='" & uuid & "', @fhaini='" & calFechaInicio.SelectedDate.Value.ToShortDateString & "', @fhafin='" & calFechaFin.SelectedDate.Value.ToShortDateString & "'")
        Objdata = Nothing

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")

    End Sub

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
        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE
        qrCodeEncoder.QRCodeScale = 6
        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L
        'La versión "0" calcula automáticamente el tamaño
        qrCodeEncoder.QRCodeVersion = 0

        qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor)
        qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor)

        Dim CBidimensional As Drawing.Image
        CBidimensional = qrCodeEncoder.Encode(CadenaCodigoBidimensional, System.Text.Encoding.UTF8)
        CBidimensional.Save(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/cbb/") & UUID & ".png", System.Drawing.Imaging.ImageFormat.Png)
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
        Dim em_razonsocial As String = ""
        Dim em_callenum As String = ""
        Dim em_colonia As String = ""
        Dim em_ciudad As String = ""
        Dim em_rfc As String = ""
        Dim em_regimen As String = ""
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

        Dim reporte As New Factura33
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
        reporte.ReportParameters("txtCadenaOriginal").Value = cadOrigComp
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

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Response.Redirect("~/portalcfd/cfd.aspx")
    End Sub

End Class