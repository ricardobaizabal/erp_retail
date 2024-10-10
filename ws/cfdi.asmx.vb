Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Reporting.Processing
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Xml.Schema
Imports System.Collections
Imports Org.BouncyCastle.Crypto
Imports System.Xml.Xsl
Imports Org.BouncyCastle.OpenSsl
Imports Org.BouncyCastle.Security
Imports FirmaSAT.Sat
Imports ThoughtWorks.QRCode.Codec
Imports ThoughtWorks.QRCode.Codec.Util
'Imports LinkiumCFDI.Cfdi32
Imports System.Security.Cryptography.X509Certificates
Imports System.Threading
Imports System.Globalization
Imports System.Web.Services
Imports uCFDsLib
Imports Ionic.Zip
Imports System.Web.Services.Protocols

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://retail.linkium.mx")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class cfdi
    Inherits System.Web.Services.WebService

    Private subtotal As Decimal = 0
    Private iva As Decimal = 0
    Private total As Decimal = 0
    Private p_serie As String = ""
    Private p_folio As Long = 0
    Private p_cadena As String = ""
    Private p_sello As String = ""
    Private p_noaprobacion As String = ""
    Private p_anioaprobacion As String = ""
    Private p_certificado As String = ""
    Private p_fechayhora As String = ""
    Private p_archivoxml As String = ""
    Private p_archivopdf As String = ""
    Private p_enviara As String = ""
    Private p_instrucciones As String = ""
    Private tieneIvaTasaCero As Boolean = False
    Private tieneIva16 As Boolean = False
    Private archivoLlavePrivada As String = ""
    Private contrasenaLlavePrivada As String = ""
    Private archivoCertificado As String = ""
    Private _selloCFD As String = ""
    Private _cadenaOriginal As String = ""
    Private serie As String = ""
    Private folio As Long = 0
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

#Region "Functions"

    Private Sub CalculaTotales(ByVal cfdid As Long)
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("exec pCFD_Webservice @cmd=3, @cfdid='" & cfdid.ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                '
                tieneIva16 = rs("tieneIva16")
                tieneIvaTasaCero = rs("tieneIvaTasaCero")
                subtotal = rs("importe")
                iva = rs("iva")
                total = rs("total")
                '
            End If

        Catch ex As Exception
            '
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Private Function TotalPartidas(ByVal cfdId As Long) As Long
        Dim Total As Long = 0
        Dim connP As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdP As New SqlCommand("exec pCFD_WebService @cmd=4, @cfdid='" & cfdId.ToString & "'", connP)
        Try

            connP.Open()

            Dim rs As SqlDataReader
            rs = cmdP.ExecuteReader()

            If rs.Read Then
                Total = rs("total")
            End If

        Catch ex As Exception
            '
        Finally
            connP.Close()
            connP.Dispose()
            connP = Nothing
        End Try
        Return Total
    End Function

    Private Function ValidaToken(ByVal token As String) As Boolean
        Dim TokenOK As Boolean = False
        Dim connP As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdP As New SqlCommand("exec pCFD_WebService @cmd=11, @token='" & token.ToString & "'", connP)
        Try

            connP.Open()

            Dim rs As SqlDataReader
            rs = cmdP.ExecuteReader()

            If rs.Read Then
                TokenOK = True
            Else
                TokenOK = False
            End If

        Catch ex As Exception
            '
        Finally
            connP.Close()
            connP.Dispose()
            connP = Nothing
        End Try
        '
        Return TokenOK
        ''
    End Function

    Private Function CargaLugarExpedicion() As String
        Dim fac_cp As String = ""
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("EXEC pCliente @cmd=3, @clienteid=1", conn)
        Dim clienteid As Long = 0
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read() Then
                fac_cp = rs("fac_cp")
            End If

            rs.Close()

        Catch ex As Exception
            '
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        Return fac_cp
    End Function

    <WebMethod()> _
    Public Function catalogoformaspago(ByVal token As String) As DataSet
        Dim ds As New DataSet
        If ValidaToken(token) Then
            Dim conn As New SqlConnection(Session("conexion"))
            conn.Open()
            Dim cmd As New SqlDataAdapter("SELECT id, nombre from tblFormaPago order by nombre", conn)
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End If
        Return ds
    End Function

#End Region

#Region "Methods"

    <WebMethod()> _
        Public Function nuevocfdi(ByVal remisionid As Long, ByVal clienteid As Long, ByVal razonsocial As String, ByVal calle As String, ByVal num_int As String, ByVal num_ext As String, ByVal colonia As String, ByVal municipio As String, ByVal estadoid As Integer, ByVal cp As String, ByVal rfc As String, ByVal sucursalid As Integer, ByVal token As String) As Long
        Dim cfdid As Long = 0
        '
        If ValidaToken(token) Then
            Dim conn As New SqlConnection(Session("conexion"))
            Dim cmd As New SqlCommand("EXEC pCFD_WebService @cmd=1, @clienteid='" & clienteid.ToString & "', @remisionid='" & remisionid.ToString & "', @razonsocial='" & razonsocial.ToString & "', @calle='" & calle.ToString & "', @num_int='" & num_int.ToString & "', @num_ext='" & num_ext.ToString & "', @colonia='" & colonia.ToString & "', @municipio='" & municipio.ToString & "', @estadoid='" & estadoid.ToString & "', @cp='" & cp.ToString & "', @rfc='" & rfc.ToString & "', @sucursalid='" & sucursalid.ToString & "'", conn)

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


            Finally

                conn.Close()
                conn.Dispose()
                conn = Nothing

            End Try
            '
        End If
        Return cfdid

    End Function

    <WebMethod()> _
        Public Function nuevocfdiclienteid(ByVal clienteid As Long, ByVal token As String) As Long
        Dim cfdid As Long = 0
        '
        If ValidaToken(token) Then
            Dim conn As New SqlConnection(Session("conexion"))
            Dim cmd As New SqlCommand("EXEC pCFD_WebService @cmd=12, @clienteid='" & clienteid.ToString & "'", conn)

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


            Finally

                conn.Close()
                conn.Dispose()
                conn = Nothing

            End Try
            '
        End If
        Return cfdid

    End Function

    <WebMethod()> _
        Public Function catalogoestados(ByVal token As String) As DataSet
        Dim ds As New DataSet
        If ValidaToken(token) Then
            Dim conn As New SqlConnection(Session("conexion"))
            conn.Open()
            Dim cmd As New SqlDataAdapter("select id, nombre from tblEstado", conn)
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End If
        Return ds
    End Function

    <WebMethod()> _
        Public Function catalogotipoimpuesto(ByVal token As String) As DataSet
        Dim ds As New DataSet
        If ValidaToken(token) Then
            Dim conn As New SqlConnection(Session("conexion"))
            conn.Open()
            Dim cmd As New SqlDataAdapter("select id, nombre from tblTasa", conn)
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End If
        Return ds
    End Function

    <WebMethod()> _
        Public Function catalogotiposdocumento(ByVal token As String) As DataSet
        Dim ds As New DataSet
        If ValidaToken(token) Then
            Dim conn As New SqlConnection(Session("conexion"))
            conn.Open()
            Dim cmd As New SqlDataAdapter("select id, nombre from tblTipoDocumento", conn)
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End If
        Return ds
    End Function

    <WebMethod()> _
    Public Sub agregapartida(ByVal cfdid As Long, ByVal productoid As Long, ByVal presentacionid As Long, ByVal factor As Decimal, ByVal codigo As String, ByVal descripcion As String, ByVal cantidad As Decimal, ByVal unidad As String, ByVal precio As Decimal, ByVal tipoimpuestoid As Integer, ByVal token As String)
        '
        If ValidaToken(token) Then
            '
            Dim conn As New SqlConnection(Session("conexion"))
            Try
                conn.Open()
                Dim cmd As New SqlCommand("exec pCFD_WebService @cmd=2, @cfdid='" & cfdid.ToString & "', @productoid='" & productoid.ToString & "', @presentacionid='" & presentacionid.ToString & "', @factor='" & factor.ToString & "', @codigo='" & codigo.ToString & "', @descripcion='" & descripcion.ToString & "', @cantidad='" & cantidad.ToString & "', @unidad='" & unidad.ToString & "', @precio='" & precio.ToString & "', @tipoimpuestoid='" & tipoimpuestoid.ToString & "'", conn)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                '
            Finally
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End Try
        End If
    End Sub

    <WebMethod()> _
    Public Function generacfdi(ByVal cfdid As Long, ByVal tipoid As Integer, ByVal token As String, ByVal enviara As String, ByVal instrucciones As String, ByVal usocfdi As String, ByVal formapagoid As String, ByVal numctapago As String, ByVal condicionesid As Integer, ByVal tasaid As Integer) As String()

        Dim valores(4) As String

        Try
            If ValidaToken(token) Then
                '
                '   Rutina de generación de XML CFDI Versión 3.3
                '
                Call CalculaTotales(cfdid)
                '
                '   Guadar Metodo de Pago
                '
                Call GuadarMetodoPago(cfdid, usocfdi)
                '
                m_xmlDOM = CrearDOM()
                '
                '   Asigna Serie y Folio
                '
                Dim SQLUpdate As String = ""

                SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & cfdid.ToString & "', @tipodocumentoid='" & tipoid.ToString & "', @enviara='" & enviara & "', @instrucciones='" & instrucciones & "', @aduana='', @fecha_pedimento='', @numero_pedimento='', @tipocambio='', @formapagoid='" & formapagoid.ToString & "', @metodopagoid='PUE', @tipopagoid='1', @numctapago='" & numctapago.ToString & "', @lugarexpedicion='" & CargaLugarExpedicion() & "', @condicionesid='" & condicionesid.ToString & "', @tasaid='" & tasaid.ToString & "'"

                Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
                Dim cmdF As New SqlCommand(SQLUpdate, connF)
                Try

                    connF.Open()

                    Dim rs As SqlDataReader
                    rs = cmdF.ExecuteReader()

                    If rs.Read Then
                        serie = rs("serie").ToString
                        folio = rs("folio").ToString

                        valores(0) = rs("serie").ToString
                        valores(1) = rs("folio").ToString
                        'valores.Add(serie.ToString)
                        'valores.Add(folio.ToString)
                    End If
                Catch ex As Exception
                    '
                Finally
                    connF.Close()
                    connF.Dispose()
                    connF = Nothing
                End Try
                '
                Comprobante = CrearNodoComprobante(formapagoid, condicionesid)
                '
                m_xmlDOM.AppendChild(Comprobante)
                IndentarNodo(Comprobante, 1)
                '
                '   Obtiene datos del emisor
                '
                Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)
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
                    valores(2) = "ERROR"
                    valores(3) = ex.Message.ToString
                Finally
                    conn.Close()
                    conn.Dispose()
                    conn = Nothing
                End Try
                '
                '   Obtiene datos del receptor
                '
                Dim connR As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)
                Dim cmdR As New SqlCommand("exec pCFD_WebService @cmd=6, @cfdid='" & cfdid.ToString & "'", connR)
                Try

                    connR.Open()

                    Dim rs As SqlDataReader
                    rs = cmdR.ExecuteReader()

                    If rs.Read Then
                        CrearNodoReceptor(Comprobante, rs("razonsocial"), rs("fac_rfc"))
                        IndentarNodo(Comprobante, 1)
                    End If
                Catch ex As Exception
                    valores(2) = "ERROR"
                    valores(3) = ex.Message.ToString
                Finally
                    connR.Close()
                    connR.Dispose()
                    connR = Nothing
                End Try
                '
                '   Agrega Partidas
                '
                Dim Conceptos As XmlElement
                Dim Concepto As XmlElement
                Dim ImpuestosConcepto As XmlElement
                Dim TrasladosConcepto As XmlElement
                Dim TrasladoConcepto As XmlElement

                Conceptos = CrearNodo("cfdi:Conceptos")
                IndentarNodo(Conceptos, 2)

                Dim connP As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
                Dim cmdP As New SqlCommand("exec pCFD @cmd=13, @cfdId='" & cfdid.ToString & "'", connP)
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
                        TrasladoConcepto = CrearNodo("cfdi:Traslado")

                        If rs("descuento") > 0 Then
                            TrasladoConcepto.SetAttribute("Base", Math.Round(rs("importe") - rs("descuento"), 6))
                        Else
                            TrasladoConcepto.SetAttribute("Base", Math.Round(rs("importe"), 6))
                        End If

                        TrasladoConcepto.SetAttribute("Impuesto", "002")
                        TrasladoConcepto.SetAttribute("TipoFactor", "Tasa")
                        TrasladoConcepto.SetAttribute("TasaOCuota", rs("tasaocuota"))
                        TrasladoConcepto.SetAttribute("Importe", Math.Round(rs("iva"), 6))
                        TrasladosConcepto.AppendChild(TrasladoConcepto)

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
                '   Agrega impuestos
                '
                Dim AgregarTraslado As Boolean = False
                Dim AgregarIeps As Boolean = False
                Dim TasaOCuotas As String = ""
                Dim TipoFactor As String = ""
                Dim TipoImpuesto As String = ""
                Dim Impuestos As XmlElement
                Dim Traslados As XmlElement
                Dim Traslado As XmlElement

                Impuestos = CrearNodo("cfdi:Impuestos")

                If iva > 0 Then
                    Impuestos.SetAttribute("TotalImpuestosTrasladados", Math.Round(iva, 2))
                Else
                    Impuestos.SetAttribute("TotalImpuestosTrasladados", "0.00")
                End If

                'Impuestos.SetAttribute("TotalImpuestosRetenidos", "0.00")

                If iva > 0 Then
                    TasaOCuotas = "0.160000"
                    AgregarTraslado = True
                    TipoFactor = "Tasa"
                    TipoImpuesto = "002"
                Else
                    TasaOCuotas = "0.000000"
                    TipoFactor = "Tasa"
                    AgregarTraslado = True
                    TipoFactor = "Tasa"
                    TipoImpuesto = "002"
                End If

                Traslados = CrearNodo("cfdi:Traslados")
                IndentarNodo(Traslados, 3)
                Impuestos.AppendChild(Traslados)

                If AgregarTraslado = True Then

                    Traslado = CrearNodo("cfdi:Traslado")
                    Traslado.SetAttribute("Impuesto", TipoImpuesto)
                    Traslado.SetAttribute("TipoFactor", TipoFactor)
                    Traslado.SetAttribute("TasaOCuota", TasaOCuotas)
                    If iva > 0 Then
                        Traslado.SetAttribute("Importe", Math.Round(CDbl(iva), 2))
                    Else
                        Traslado.SetAttribute("Importe", "0.00")
                    End If

                    Traslados.AppendChild(Traslado)

                End If

                IndentarNodo(Traslados, 2)
                Impuestos.AppendChild(Traslados)
                IndentarNodo(Impuestos, 1)
                Comprobante.AppendChild(Impuestos)
                '
                '   Sellar Comprobante
                '
                SellarCFD(Comprobante, cfdid)
                m_xmlDOM.InnerXml = (Replace(m_xmlDOM.InnerXml, "schemaLocation", "xsi:schemaLocation", , , CompareMethod.Text))
                m_xmlDOM.Save(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & ".xml")
                '
                '   Realiza Timbrado
                '
                If folio > 0 Then
                    Try
                        'Pruebas
                        'Dim TimbreSifeiVersion33 As New SIFEIPruebasV33.SIFEIService()
                        'Producción
                        Dim TimbreSifeiVersion33 As New SIFEI33.SIFEIService()
                        Call Comprimir()

                        Dim bytes() As Byte
                        bytes = TimbreSifeiVersion33.getCFDI("LST101206985", "d77ab121", data, "", "NDE0OTFlOTEtOGQxYi1mODM3LWRlOWMtNWE3YTJlNWE0ZTgy")
                        Descomprimir(bytes)
                        '
                        cadOrigComp = CadenaOriginalComplemento(serie, folio)
                        '
                        '   Obtiene UUID
                        '
                        Dim UUID As String = ""
                        Dim FlujoReader As XmlTextReader = Nothing
                        Dim t As Integer
                        ' leer del fichero e ignorar los nodos vacios
                        FlujoReader = New XmlTextReader(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml")
                        FlujoReader.WhitespaceHandling = WhitespaceHandling.None
                        ' analizar el fichero y presentar cada nodo
                        While FlujoReader.Read()
                            Select Case FlujoReader.NodeType
                                Case XmlNodeType.Element
                                    If FlujoReader.Name = "tfd:TimbreFiscalDigital" Then
                                        For t = 0 To FlujoReader.AttributeCount - 1
                                            FlujoReader.MoveToAttribute(t)
                                            If FlujoReader.Name = "UUID" Then
                                                UUID = FlujoReader.Value
                                            End If
                                        Next
                                    End If
                            End Select
                        End While
                        '
                        '   Marca el cfd como timbrado
                        '
                        Call cfdtimbrado(cfdid, UUID)
                        '
                        '   Renombra XML por UUID
                        '
                        'If File.Exists(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml") Then
                        '    Dim Path = Server.MapPath("~/portalcfd/cfd_storage/")
                        '    System.IO.File.Copy(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", Path & UUID.ToString & ".xml")
                        'End If
                        '
                        '   Genera Código Bidimensional
                        '
                        Call generacbb()
                        '
                        '   Genera PDF
                        '
                        If Not File.Exists(Server.MapPath("~/portalcfd/pdf/") & serie.ToString & folio.ToString & ".pdf") Then
                            GuardaPDF(GeneraPDF(cfdid), Server.MapPath("~/portalcfd/pdf/") & serie.ToString & folio.ToString & ".pdf")
                        End If
                        '
                        valores(2) = "OK"
                        valores(3) = "Ticket facturado correctamente"
                        valores(4) = UUID.ToString

                    Catch ex As SoapException
                        valores(0) = ""
                        valores(1) = 0
                        valores(2) = "ERROR"
                        valores(3) = "No se realizo el timbrado " & ex.Detail.InnerText
                    End Try

                    'Dim queusuariocertus As String = System.Configuration.ConfigurationManager.AppSettings("FactureHoyUsuario")
                    'Dim quepasscertus As String = System.Configuration.ConfigurationManager.AppSettings("FactureHoyContrasena")
                    'Dim queproceso As Integer = System.Configuration.ConfigurationManager.AppSettings("FactureHoyProceso")
                    'Dim MemStream As System.IO.MemoryStream = FileToMemory(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & ".xml")

                    'Dim archivo As Byte() = MemStream.ToArray()
                    'Dim service As New FactureHoyPruebasV33.WsEmisionTimbrado33()
                    ''Dim service As New FactureHoyNT33.WsEmisionTimbrado33()
                    'Dim puerto = service.EmitirTimbrar(queusuariocertus, quepasscertus, queproceso, archivo)

                    'If Not puerto.XML Is Nothing Then
                    '    If puerto.isError Then
                    '        '
                    '        '   Marca el cfd como no timbrado
                    '        '
                    '        'valores.Add("ERROR")
                    '        'valores.Add(puerto.message.ToString)
                    '        valores(2) = "ERROR"
                    '        valores(3) = puerto.message.ToString
                    '        Call cfdnotimbrado(cfdid)
                    '        '
                    '    Else
                    '        File.WriteAllBytes(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", puerto.XML)
                    '        'cadOrigComp = puerto.cadenaOriginalTimbre
                    '        '
                    '        '   Obtiene UUID
                    '        '
                    '        Dim UUID As String = ""
                    '        Dim FlujoReader As XmlTextReader = Nothing
                    '        Dim t As Integer
                    '        ' leer del fichero e ignorar los nodos vacios
                    '        FlujoReader = New XmlTextReader(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml")
                    '        FlujoReader.WhitespaceHandling = WhitespaceHandling.None
                    '        ' analizar el fichero y presentar cada nodo
                    '        While FlujoReader.Read()
                    '            Select Case FlujoReader.NodeType
                    '                Case XmlNodeType.Element
                    '                    If FlujoReader.Name = "tfd:TimbreFiscalDigital" Then
                    '                        For t = 0 To FlujoReader.AttributeCount - 1
                    '                            FlujoReader.MoveToAttribute(t)
                    '                            If FlujoReader.Name = "UUID" Then
                    '                                UUID = FlujoReader.Value
                    '                            End If
                    '                        Next
                    '                    End If
                    '            End Select
                    '        End While
                    '        '
                    '        '   Marca el cfd como timbrado
                    '        '
                    '        Call cfdtimbrado(cfdid, UUID)
                    '        '
                    '        '   Renombra XML por UUID
                    '        '
                    '        'If File.Exists(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml") Then
                    '        '    Dim Path = Server.MapPath("~/portalcfd/cfd_storage/")
                    '        '    System.IO.File.Copy(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", Path & UUID.ToString & ".xml")
                    '        'End If
                    '        '
                    '        '   Genera Código Bidimensional
                    '        '
                    '        Call generacbb()
                    '        '
                    '        '   Genera PDF
                    '        '
                    '        If Not File.Exists(Server.MapPath("~/portalcfd/pdf/") & serie.ToString & folio.ToString & ".pdf") Then
                    '            GuardaPDF(GeneraPDF(cfdid), Server.MapPath("~/portalcfd/pdf/") & serie.ToString & folio.ToString & ".pdf")
                    '        End If
                    '        '
                    '        valores(2) = "OK"
                    '        valores(3) = puerto.message.ToString
                    '        valores(4) = UUID.ToString
                    '        '
                    '    End If
                    'Else
                    '    Call cfdnotimbrado(cfdid)
                    '    valores(2) = "ERROR"
                    '    valores(3) = puerto.message.ToString
                    'End If
                End If
            Else
                valores(0) = ""
                valores(1) = 0
                valores(2) = "ERROR"
                valores(3) = "Token de caja no válido"
            End If
        Catch ex As Exception
            valores(2) = "ERROR"
            valores(3) = ex.Message.ToString
            'valores.Add("ERROR")
            'valores.Add(ex.Message.ToString)
        End Try

        Return valores

    End Function

    Private Function Comprimir()
        Dim zip As ZipFile = New ZipFile(serie & folio.ToString & ".zip")
        zip.AddFile(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & ".xml", "")
        Dim ms As New MemoryStream()
        zip.Save(ms)
        data = ms.ToArray
    End Function

    Private Function Descomprimir(ByVal data5 As Byte())
        Dim ms1 As New MemoryStream(data5)
        Dim zip1 As ZipFile = New ZipFile()
        zip1 = ZipFile.Read(ms1)

        Dim archivo As String = ""
        Dim DirectorioExtraccion As String = Server.MapPath("~/portalcfd/cfd_storage/").ToString
        Dim e As ZipEntry
        For Each e In zip1
            archivo = zip1.Entries(0).FileName.ToString
            e.Extract(DirectorioExtraccion, ExtractExistingFileAction.OverwriteSilently)
        Next

        Dim Path = Server.MapPath("~/portalcfd/cfd_storage/")
        If File.Exists(Path & archivo) Then
            System.IO.File.Copy(Path & archivo, Path & "link_" & serie.ToString & folio.ToString & "_timbrado.xml")
        End If

    End Function

    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub

    Private Function GeneraPDF(ByVal cfdid As Long) As Telerik.Reporting.Report
        Dim logo_formato As String = ""
        Dim connL As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdL As New SqlCommand("select top 1 isnull(logo_formato,'') as logo_formato from tblCliente", connL)
        Try
            connL.Open()
            Dim rsL As SqlDataReader
            rsL = cmdL.ExecuteReader()

            If rsL.Read Then
                logo_formato = rsL("logo_formato").ToString
            End If
            connL.Close()
            connL.Dispose()
        Catch ex As Exception
            '
        Finally
            connL.Close()
            connL.Dispose()
            connL = Nothing
        End Try

        Dim conn As New SqlConnection(Session("conexion"))

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
        Dim importe As Decimal = 0
        Dim importetasacero As Decimal = 0
        Dim descuento As Decimal = 0
        Dim iva As Decimal = 0
        Dim total As Decimal = 0
        Dim CantidadTexto As String = ""
        Dim condiciones As String = ""
        Dim enviara As String = ""
        Dim instrucciones As String = ""
        Dim pedimento As String = ""
        Dim retencion As Decimal = 0
        Dim tipoid As Integer = 0
        Dim divisaid As Integer = 1
        Dim expedicionLinea1 As String = ""
        Dim expedicionLinea2 As String = ""
        Dim expedicionLinea3 As String = ""
        Dim porcentaje As Decimal = 0
        Dim plantillaid As Integer = 1
        Dim tipopago As String = ""
        Dim formapago As String = ""
        Dim numctapago As String = ""

        Dim uuid As String = ""
        Dim usocfdi As String = ""
        Dim tipo_comprobante As String = ""

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
                importe = rs("importe")
                importetasacero = rs("importetasacero")
                descuento = rs("descuento")
                iva = rs("iva")
                total = rs("total")
                divisaid = rs("divisaid")
                fechaHora = rs("fecha_factura").ToString
                condiciones = "Condiciones: " & rs("condiciones").ToString
                enviara = rs("enviara").ToString
                instrucciones = rs("instrucciones")
                If rs("aduana") = "" Or rs("numero_pedimento") = "" Then
                    pedimento = ""
                Else
                    pedimento = "Aduana: " & rs("aduana") & vbCrLf & "Fecha: " & rs("fecha_pedimento").ToString & vbCrLf & "Número: " & rs("numero_pedimento").ToString
                End If
                expedicionLinea1 = rs("expedicionLinea1")
                expedicionLinea2 = rs("expedicionLinea2")
                expedicionLinea3 = rs("expedicionLinea3")
                porcentaje = rs("porcentaje")
                plantillaid = rs("plantillaid")
                tipocontribuyenteid = rs("tipocontribuyenteid")
                tipopago = rs("tipopago")
                formapago = rs("formapago")
                numctapago = rs("numctapago")
                uuid = rs("uuid")
                usocfdi = rs("usocfdi")
            End If
            rs.Close()
            '
        Catch ex As Exception
            '
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        Dim largo = Len(CStr(Format(CDbl(total), "#,###.00")))
        Dim decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)

        If System.Configuration.ConfigurationManager.AppSettings("divisas") = 1 Then
            If divisaid = 1 Then
                CantidadTexto = Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N."
            Else
                CantidadTexto = Num2Text(total - decimales) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD."
            End If
        Else
            CantidadTexto = Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N."
        End If

        Select Case tipoid
            Case 3, 6      ' Recibo de honorarios y arrendamiento
                Dim reporte As New FormatosPDF.formato_cfdi_honorarios
                reporte.ReportParameters("plantillaId").Value = plantillaid
                reporte.ReportParameters("cfdiId").Value = cfdid
                Select Case tipoid
                    Case 3
                        reporte.ReportParameters("txtDocumento").Value = "Recibo de Arrendamiento No.    " & serie.ToString & folio.ToString
                    Case 6
                        reporte.ReportParameters("txtDocumento").Value = "Recibo de Honorarios No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & logo_formato)
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "fecha", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "calle", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "noExterior", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "noInterior", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteColonia").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "colonia", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "codigoPostal", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteCiudadEstado").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "municipio", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "estado", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "pais", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteRFC").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "rfc", "cfdi:Receptor")
                '
                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "selloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones
                reporte.ReportParameters("txtPedimento").Value = pedimento
                reporte.ReportParameters("txtEnviarA").Value = enviara
                '
                If tipocontribuyenteid = 1 Then
                    reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
                    reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                    reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
                    reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
                    reporte.ReportParameters("txtRetIva").Value = FormatCurrency(0, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva), 2).ToString
                    '
                    '   Ajusta cantidad con texto
                    '
                    total = FormatNumber((importe + iva), 2)
                    largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                    decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                    CantidadTexto = Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N."
                    '
                Else
                    reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
                    reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                    reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
                    reporte.ReportParameters("txtRetISR").Value = FormatCurrency(importe * 0.1, 2).ToString
                    reporte.ReportParameters("txtRetIva").Value = FormatCurrency((iva / 3) * 2, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva) - (importe * 0.1) - ((iva / 3) * 2), 2).ToString
                    '
                    '   Ajusta cantidad con texto
                    '
                    total = FormatNumber((importe + iva) - (importe * 0.1) - ((iva / 3) * 2), 2)
                    largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                    decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                    CantidadTexto = Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N."
                    '
                End If

                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                '
                '
                reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento(serie, folio)
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                If porcentaje > 0 Then
                    reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
                End If
                '
                Return reporte
                '
            Case Else
                '
                CantidadTexto = Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N."
                '
                Dim reporte As New FormatosPDF.formato_cfdi33_link
                reporte.ReportParameters("plantillaId").Value = plantillaid
                reporte.ReportParameters("cfdiId").Value = cfdid
                Select Case tipoid
                    Case 1, 4
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                    Case 2
                        reporte.ReportParameters("txtDocumento").Value = "Nota de Crédito No.    " & serie.ToString & folio.ToString
                    Case 5
                        reporte.ReportParameters("txtDocumento").Value = "Carta Porte No.    " & serie.ToString & folio.ToString
                        reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO     EFECTOS FISCALES AL PAGO"
                    Case 6
                        reporte.ReportParameters("txtDocumento").Value = "Recibo de Honorarios No.    " & serie.ToString & folio.ToString
                    Case Else
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & logo_formato)
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Fecha", "cfdi:Comprobante")
                'reporte.ReportParameters("txtTipoDocumento").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "tipoDeComprobante", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtPACCertifico").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = callenum
                reporte.ReportParameters("txtClienteColonia").Value = colonia
                reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
                reporte.ReportParameters("txtClienteRFC").Value = "R.F.C. " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
                '
                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones
                reporte.ReportParameters("txtPedimento").Value = pedimento
                reporte.ReportParameters("txtEnviarA").Value = enviara
                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                '
                reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe, 2).ToString
                reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
                reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                reporte.ReportParameters("txtIEPS").Value = FormatCurrency(0, 2).ToString
                reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                reporte.ReportParameters("txtDescuento").Value = FormatCurrency(descuento, 2).ToString
                reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
                reporte.ReportParameters("txtRetIva").Value = FormatCurrency(0, 2).ToString
                reporte.ReportParameters("txtTotal").Value = FormatCurrency(total, 2).ToString
                reporte.ReportParameters("txtEtiquetaRetIVA").Value = "- RET IVA"
                '
                tipo_comprobante = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoDeComprobante", "cfdi:Comprobante")

                If tipo_comprobante = "I" Then
                    tipo_comprobante = "Ingreso"
                ElseIf tipo_comprobante = "E" Then
                    tipo_comprobante = "Egreso"
                ElseIf tipo_comprobante = "N" Then
                    tipo_comprobante = "Nómina"
                ElseIf tipo_comprobante = "P" Then
                    tipo_comprobante = "Pago"
                ElseIf tipo_comprobante = "T" Then
                    tipo_comprobante = "Traslado"
                End If
                '
                reporte.ReportParameters("txtTipoComprobante").Value = tipo_comprobante
                reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento(serie, folio)
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
                reporte.ReportParameters("txtUsoCFDI").Value = "Uso de CFDI: " & usocfdi

                Return reporte
        End Select
    End Function

    Private Function CadenaOriginalComplemento(ByVal serie As String, ByVal folio As Long) As String
        '
        '   Obtiene los valores del timbre de respuesta
        '
        Dim Version As String = ""
        Dim selloSAT As String = ""
        Dim UUID As String = ""
        Dim noCertificadoSAT As String = ""
        Dim selloCFD As String = ""
        Dim fechaTimbrado As String = ""
        Dim RfcProvCertif As String = ""
        'Dim Version As String = ""
        '
        Dim FlujoReader As XmlTextReader = Nothing
        Dim i As Integer
        ' leer del fichero e ignorar los nodos vacios
        FlujoReader = New XmlTextReader(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "link_" & serie.ToString & folio.ToString & "_timbrado.xml")
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
        '
        Dim cadena As String = ""
        cadena = "||" & Version & "|" & UUID & "|" & fechaTimbrado & "|" & RfcProvCertif & "|" & selloCFD & "|" & noCertificadoSAT & "||"
        Return cadena
        '
        ''
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

    Private Sub cfdnotimbrado(ByVal cfdid As Long)
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=23, @cfdid='" & cfdid.ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub cfdtimbrado(ByVal cfdid As Long, ByVal uuid As String)
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=24, @cfdid='" & cfdid.ToString & "'")
        Objdata = Nothing
    End Sub

    Private Function metodoPago(ByVal formapagoId As Integer) As String
        Dim metodo As String = ""
        Dim connX As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdX As New SqlCommand("select id, nombre from tblFormaPago where id='" & formapagoId.ToString & "'", connX)
        Try

            connX.Open()

            Dim rs As SqlDataReader
            rs = cmdX.ExecuteReader()

            If rs.Read Then
                metodo = rs("nombre")
            End If

        Catch ex As Exception
            '
        Finally

            connX.Close()
            connX.Dispose()
            connX = Nothing

        End Try
        Return metodo
    End Function

    Private Function condicionesTxt(ByVal condicionesId As Integer) As String
        Dim condiciones As String = ""
        Dim connX As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdX As New SqlCommand("select id, nombre from tblCondiciones where id='" & condicionesId.ToString & "'", connX)
        Try

            connX.Open()

            Dim rs As SqlDataReader
            rs = cmdX.ExecuteReader()

            If rs.Read Then
                condiciones = rs("nombre")
            End If

        Catch ex As Exception
            '
        Finally

            connX.Close()
            connX.Dispose()
            connX = Nothing

        End Try
        Return condiciones
    End Function

    Private Sub obtienellave()
        Dim connX As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdX As New SqlCommand("exec pCFD_WebService @cmd=9", connX)
        Try

            connX.Open()

            Dim rs As SqlDataReader
            rs = cmdX.ExecuteReader()

            If rs.Read Then
                archivoLlavePrivada = Server.MapPath("~/portalcfd/llave") & "\" & rs("archivo_llave_privada")
                contrasenaLlavePrivada = rs("contrasena_llave_privada")
                archivoCertificado = Server.MapPath("~/portalcfd/certificados") & "\" & rs("archivo_certificado")
            End If

        Catch ex As Exception
            '
        Finally

            connX.Close()
            connX.Dispose()
            connX = Nothing

        End Try
    End Sub

    Private Sub generacbb()
        Dim CadenaCodigoBidimensional As String = ""
        Dim FinalSelloDigitalEmisor As String = ""
        Dim rfcE As String = ""
        Dim rfcR As String = ""
        Dim total As String = ""
        Dim sello As String = ""
        '
        '   Obtiene datos del cfdi para construir string del CBB
        '
        rfcE = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Emisor")
        rfcR = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
        total = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Total", "cfdi:Comprobante")
        UUID = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
        sello = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloCFD", "tfd:TimbreFiscalDigital")
        FinalSelloDigitalEmisor = Mid(sello, (Len(sello) - 7))
        '
        Dim totalDec As Decimal = CType(total, Decimal)
        '
        CadenaCodigoBidimensional = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx" & "?id=" & UUID & "&re=" & rfcE & "&rr=" & rfcR & "&tt=" & totalDec.ToString & "&fe=" & FinalSelloDigitalEmisor
        '
        '   Genera gráfico
        '
        Dim qrCodeEncoder As QRCodeEncoder = New QRCodeEncoder
        qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE
        qrCodeEncoder.QRCodeScale = 6
        qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.L
        'La versión "0" calcula automáticamente el tamaño
        qrCodeEncoder.QRCodeVersion = 0

        qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor)
        qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor)

        Dim CBidimensional As Drawing.Image
        CBidimensional = qrCodeEncoder.Encode(CadenaCodigoBidimensional, System.Text.Encoding.UTF8)
        CBidimensional.Save(Server.MapPath("~/portalCFD/cbb") & "\" & serie.ToString & folio.ToString & ".png", System.Drawing.Imaging.ImageFormat.Png)
    End Sub

    Private Function FileToMemory(ByVal Filename As String) As MemoryStream
        Dim FS As New System.IO.FileStream(Filename, FileMode.Open)
        Dim MS As New System.IO.MemoryStream
        Dim BA(FS.Length - 1) As Byte
        FS.Read(BA, 0, BA.Length)
        FS.Close()
        MS.Write(BA, 0, BA.Length)
        Return MS
    End Function

    Function ReadFile(ByVal strArchivo As String) As Byte()
        Dim f As New FileStream(strArchivo, FileMode.Open, FileAccess.Read)
        Dim size As Integer = CInt(f.Length)
        Dim data As Byte() = New Byte(size - 1) {}
        size = f.Read(data, 0, size)
        f.Close()
        Return data
    End Function

    Private Sub GuadarMetodoPago(ByVal cfdid As Long, ByVal usocfdi As String)
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD_WebService @cmd=12, @metodopagoid='PUE', @usocfdi='" & usocfdi & "', @serieid='1', @cfdid='" & cfdid.ToString & "'")
        Objdata = Nothing
    End Sub

    Private Function CrearDOM() As XmlDocument
        Dim oDOM As New XmlDocument
        Dim Nodo As XmlNode
        Nodo = oDOM.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
        oDOM.AppendChild(Nodo)
        Nodo = Nothing
        CrearDOM = oDOM
    End Function

    Private Function CrearNodo(ByVal Nombre As String) As XmlNode
        CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, Nombre, URI_SAT)
    End Function

    Private Sub IndentarNodo(ByVal Nodo As XmlNode, ByVal Nivel As Long)
        Nodo.AppendChild(m_xmlDOM.CreateTextNode(vbNewLine & New String(ControlChars.Tab, Nivel)))
    End Sub

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

        'If descuento > 0 Then
        '    Nodo.SetAttribute("Descuento", Format(descuento, "#0.00"))
        'End If

        Nodo.SetAttribute("Moneda", "MXN")
        Nodo.SetAttribute("Total", Math.Round(subtotal, 2) + Math.Round(iva, 2))
        Nodo.SetAttribute("TipoDeComprobante", "I")
        Nodo.SetAttribute("MetodoPago", "PUE")
        Nodo.SetAttribute("LugarExpedicion", CargaLugarExpedicion())
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
            Receptor.SetAttribute("UsoCFDI", "G03")
            Nodo.AppendChild(Receptor)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub SellarCFD(ByVal NodoComprobante As XmlElement, ByVal cfdid As Long)
        Try
            Dim Certificado As String = ""
            Certificado = LeerCertificado()

            Dim Clave As String = ""
            Clave = LeerClave()

            Dim objCert As New X509Certificate2()
            Dim bRawData As Byte() = ReadFile(Server.MapPath("~/portalcfd/certificados/") & Certificado)
            objCert.Import(bRawData)
            Dim cadena As String = Convert.ToBase64String(bRawData)
            NodoComprobante.SetAttribute("NoCertificado", FormatearSerieCert(objCert.SerialNumber))
            NodoComprobante.SetAttribute("Total", Math.Round(subtotal, 2) + Math.Round(iva, 2))
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

    Private Function LeerLlave() As String
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

    Public Function FormatearSerieCert(ByVal Serie As String) As String
        Dim Resultado As String = ""
        Dim I As Integer
        For I = 2 To Len(Serie) Step 2
            Resultado = Resultado & Mid(Serie, I, 1)
        Next
        FormatearSerieCert = Resultado
    End Function

    Private Function GenerarSello(ByVal Clave As String, ByVal cfdid As Long) As String
        Try
            Dim pkey As New Chilkat.PrivateKey
            Dim pkeyXml As String
            Dim rsa As New Chilkat.Rsa
            pkey.LoadPkcs8EncryptedFile(Server.MapPath("~/portalcfd/llave/") & LeerLlave(), Clave)
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

#End Region

End Class