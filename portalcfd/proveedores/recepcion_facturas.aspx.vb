Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Threading
Imports System.Globalization
Imports System.Xml
Imports Telerik.Web.UI
Imports System.Net.Mail

Partial Class recepcion_facturas
    Inherits System.Web.UI.Page

    Private ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(proveedorid, "select id, razonsocial as nombre from tblMisProveedores order by razonsocial", 0)
            ObjData.Catalogo(sucursalid, "select id, nombre from tblSucursal order by nombre", 0)
            ObjData = Nothing
        End If
    End Sub
    Function ObtenerItems() As DataSet

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Dim cmd As New SqlDataAdapter("EXEC pCatalogos @cmd=10", conn)

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
    Protected Sub btnCargarXML_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCargarXML.Click

        Dim mensaje As String = ""
        Dim facturas As String = ""

        If archivosXML.UploadedFiles.Count > 0 Then

            Dim nombreXML As String = ""
            Dim nombrePDF As String = ""
            Dim i As Integer = 0

            For Each f As Telerik.Web.UI.UploadedFile In archivosXML.UploadedFiles
                If f.GetExtension().ToString.ToLower <> ".xml" Then
                    mensaje = mensaje & "El documento " & f.GetName() & " no cuenta con extención de .xml válida" & "<br/>"
                Else

                    nombreXML = f.GetName.ToString

                    If File.Exists(Server.MapPath("~/portalcfd/proveedores/xml/") & nombreXML) Then
                        For i = 1 To 999
                            nombreXML = i.ToString & "_" & f.GetName.ToString
                            If Not File.Exists(Server.MapPath("~/portalcfd/proveedores/xml/") & nombreXML) Then
                                f.SaveAs(Server.MapPath("~/portalcfd/proveedores/xml/") & nombreXML)
                                Exit For
                            End If
                        Next
                        Dim FilePath = Server.MapPath("~/portalcfd/proveedores/xml/") & nombreXML
                        mensaje = mensaje & GuardarDatosXML(FilePath, nombreXML, f.GetName())
                    Else
                        f.SaveAs(Server.MapPath("~/portalcfd/proveedores/xml/") & nombreXML)
                        Dim FilePath = Server.MapPath("~/portalcfd/proveedores/xml/") & nombreXML
                        mensaje = mensaje & GuardarDatosXML(FilePath, nombreXML, f.GetName())
                    End If

                End If
            Next
        Else
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "Selecciona un archivo en formato XML."
        End If

        If facturas <> "" Then
            lblMensaje.ForeColor = Drawing.Color.Green
            lblMensaje.Text = "Se cargó con éxito las siguientes facturas :" & "<br/>" & facturas
        End If

        If mensaje <> "" Then
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = mensaje
        End If

        sucursalid.SelectedValue = 0
        proveedorid.SelectedValue = 0

        Call MuestraLista()

    End Sub
    Private Function GuardarDatosXML(ByVal FilePath As String, ByVal nombreXML As String, ByVal documento As String) As String
        Dim mensaje As String = ""
        If File.Exists(FilePath) Then
            Dim IdUsuario As Integer = 0
            Dim IdFacturaProveedor As Long = 0
            Dim IdsFacturaProveedor As String = ""
            Dim fecha_emision As String = ""
            Dim lugar_expedicion As String = ""
            Dim emisor As String = ""
            Dim rfc As String = ""
            Dim calle As String = ""
            Dim num_int As String = ""
            Dim num_ext As String = ""
            Dim colonia As String = ""
            Dim cp As String = ""
            Dim municipio As String = ""
            Dim estado As String = ""
            Dim pais As String = ""
            Dim serie As String = ""
            Dim folio As String = ""
            Dim uuid As String = ""
            Dim regimenid As String = ""
            Dim forma_pago As String = ""
            Dim metodo_pago As String = ""
            Dim certificado_emisor As String = ""
            Dim serie_certificado_emisor As String = ""
            Dim sello_cfdi As String = ""
            Dim sello_sat As String = ""
            Dim importe As Decimal = 0
            Dim total As Decimal = 0
            Dim descuento As Decimal = 0
            Dim impuestos As Decimal = 0
            Dim impuestos_trasladados As Decimal = 0
            Dim impuestos_retenidos As Decimal = 0
            Dim facturas As String = ""
            Dim IdsFacturas As String = ""
            Dim moneda As String = ""
            Dim tipo_cambio As Decimal = 0
            Dim tipo_documento As String = ""
            Dim notacreditobit As Boolean = 0
            Dim usocfdi As String = ""

            Dim version As String = XML_Lee(FilePath, "Version", "cfdi:Comprobante", "")

            If version = "3.3" Then
                fecha_emision = XML_Lee(FilePath, "Fecha", "cfdi:Comprobante", "")
                lugar_expedicion = XML_Lee(FilePath, "LugarExpedicion", "cfdi:Comprobante", "")
                tipo_documento = XML_Lee(FilePath, "TipoDeComprobante", "cfdi:Comprobante", "")
                emisor = XML_Lee(FilePath, "Nombre", "cfdi:Comprobante", "cfdi:Emisor")
                rfc = XML_Lee(FilePath, "Rfc", "cfdi:Comprobante", "cfdi:Emisor")

                regimenid = XML_Lee(FilePath, "RegimenFiscal", "cfdi:Comprobante", "cfdi:Emisor")
                usocfdi = XML_Lee(FilePath, "UsoCFDI", "cfdi:Comprobante", "cfdi:Receptor")

                calle = ""
                num_int = ""
                num_ext = ""
                colonia = ""
                cp = ""
                municipio = ""
                estado = ""
                pais = ""

                serie = XML_Lee(FilePath, "Serie", "cfdi:Comprobante", "")
                folio = XML_Lee(FilePath, "Folio", "cfdi:Comprobante", "")
                uuid = XML_Lee(FilePath, "UUID", "cfdi:Comprobante", "cfdi:Complemento/tfd:TimbreFiscalDigital")
                metodo_pago = XML_Lee(FilePath, "FormaPago", "cfdi:Comprobante", "")
                forma_pago = XML_Lee(FilePath, "MetodoPago", "cfdi:Comprobante", "")

                certificado_emisor = XML_Lee(FilePath, "Certificado", "cfdi:Comprobante", "")
                serie_certificado_emisor = XML_Lee(FilePath, "NoCertificado", "cfdi:Comprobante", "")

                sello_cfdi = XML_Lee(FilePath, "SelloCFD", "cfdi:Comprobante", "cfdi:Complemento/tfd:TimbreFiscalDigital")
                sello_sat = XML_Lee(FilePath, "SelloSAT", "cfdi:Comprobante", "cfdi:Complemento/tfd:TimbreFiscalDigital")
                Try
                    importe = Convert.ToDecimal(XML_Lee(FilePath, "SubTotal", "cfdi:Comprobante", ""))
                Catch ex As Exception
                    importe = 0
                End Try
                Try
                    total = Convert.ToDecimal(XML_Lee(FilePath, "Total", "cfdi:Comprobante", ""))
                Catch ex As Exception
                    total = 0
                End Try
                Try
                    descuento = Convert.ToDecimal(XML_Lee(FilePath, "Descuento", "cfdi:Comprobante", ""))
                Catch ex As Exception
                    descuento = 0
                End Try
                Try
                    impuestos = (Convert.ToDecimal(total) - Convert.ToDecimal(descuento) - Convert.ToDecimal(importe))
                Catch ex As Exception
                    impuestos = 0
                End Try

                moneda = XML_Lee(FilePath, "Moneda", "cfdi:Comprobante", "")
                Try
                    tipo_cambio = Convert.ToDecimal(XML_Lee(FilePath, "TipoCambio", "cfdi:Comprobante", ""))
                Catch ex As Exception
                    tipo_cambio = 0
                End Try

                If tipo_documento = "E" Then
                    notacreditobit = 1
                End If

            Else
                fecha_emision = XML_Lee(FilePath, "fecha", "cfdi:Comprobante", "")
                lugar_expedicion = XML_Lee(FilePath, "LugarExpedicion", "cfdi:Comprobante", "")
                tipo_documento = XML_Lee(FilePath, "tipoDeComprobante", "cfdi:Comprobante", "")
                emisor = XML_Lee(FilePath, "nombre", "cfdi:Comprobante", "cfdi:Emisor")
                rfc = XML_Lee(FilePath, "rfc", "cfdi:Comprobante", "cfdi:Emisor")

                usocfdi = ""

                calle = XML_Lee(FilePath, "calle", "cfdi:Comprobante", "cfdi:Emisor/cfdi:DomicilioFiscal")
                num_int = XML_Lee(FilePath, "noInterior", "cfdi:Comprobante", "cfdi:Emisor/cfdi:DomicilioFiscal")
                num_ext = XML_Lee(FilePath, "noExterior", "cfdi:Comprobante", "cfdi:Emisor/cfdi:DomicilioFiscal")
                colonia = XML_Lee(FilePath, "colonia", "cfdi:Comprobante", "cfdi:Emisor/cfdi:DomicilioFiscal")
                cp = XML_Lee(FilePath, "codigoPostal", "cfdi:Comprobante", "cfdi:Emisor/cfdi:DomicilioFiscal")
                municipio = XML_Lee(FilePath, "municipio", "cfdi:Comprobante", "cfdi:Emisor/cfdi:DomicilioFiscal")
                estado = XML_Lee(FilePath, "estado", "cfdi:Comprobante", "cfdi:Emisor/cfdi:DomicilioFiscal")
                pais = XML_Lee(FilePath, "pais", "cfdi:Comprobante", "cfdi:Emisor/cfdi:DomicilioFiscal")

                serie = XML_Lee(FilePath, "serie", "cfdi:Comprobante", "")
                folio = XML_Lee(FilePath, "folio", "cfdi:Comprobante", "")
                uuid = XML_Lee(FilePath, "UUID", "cfdi:Comprobante", "cfdi:Complemento/tfd:TimbreFiscalDigital")
                regimenid = XML_Lee(FilePath, "Regimen", "cfdi:Comprobante", "cfdi:Emisor/cfdi:RegimenFiscal ")
                forma_pago = XML_Lee(FilePath, "formaDePago", "cfdi:Comprobante", "")
                metodo_pago = XML_Lee(FilePath, "metodoDePago", "cfdi:Comprobante", "")

                certificado_emisor = XML_Lee(FilePath, "certificado", "cfdi:Comprobante", "")
                serie_certificado_emisor = XML_Lee(FilePath, "noCertificado", "cfdi:Comprobante", "")

                sello_cfdi = XML_Lee(FilePath, "selloCFD", "cfdi:Comprobante", "cfdi:Complemento/tfd:TimbreFiscalDigital")
                sello_sat = XML_Lee(FilePath, "selloSAT", "cfdi:Comprobante", "cfdi:Complemento/tfd:TimbreFiscalDigital")

                importe = Convert.ToDecimal(XML_Lee(FilePath, "SubTotal", "cfdi:Comprobante"))
                total = Convert.ToDecimal(XML_Lee(FilePath, "Total", "cfdi:Comprobante", ""))
                moneda = XML_Lee(FilePath, "Moneda", "cfdi:Comprobante", "")

                If tipo_documento = "egreso" Then
                    notacreditobit = 1
                End If

                Try
                    tipo_cambio = Convert.ToDecimal(XML_Lee(FilePath, "TipoCambio", "cfdi:Comprobante", ""))
                Catch ex As Exception
                    tipo_cambio = 0
                End Try

                Try
                    impuestos_trasladados = Convert.ToDecimal(XML_Lee(FilePath, "totalImpuestosTrasladados", "cfdi:Comprobante", "cfdi:Impuestos"))
                Catch ex As Exception
                    impuestos_trasladados = 0
                End Try

                Try
                    impuestos_retenidos = Convert.ToDecimal(XML_Lee(FilePath, "totalImpuestosRetenidos", "cfdi:Comprobante", "cfdi:Impuestos"))
                Catch ex As Exception
                    impuestos_retenidos = 0
                End Try

                impuestos = (Convert.ToDecimal(total) - Convert.ToDecimal(importe))

            End If

            Dim DataControl As New DataControl(1)
            Dim validauuid As Long
            validauuid = DataControl.RunSQLScalarQuery("select count(id) from tblFacturaProveedor where uuid='" & uuid & "' and isnull(borradoBit,0)=0")

            If validauuid <= 0 Then
                Dim p As New ArrayList
                p.Add(New SqlParameter("@cmd", 1))
                p.Add(New SqlParameter("@version", version.ToString))
                p.Add(New SqlParameter("@sucursalid", sucursalid.SelectedValue))
                p.Add(New SqlParameter("@proveedorid", proveedorid.SelectedValue))
                p.Add(New SqlParameter("@fecha_emision", CDate(fecha_emision)))
                p.Add(New SqlParameter("@lugar_expedicion", lugar_expedicion.ToString))
                p.Add(New SqlParameter("@emisor", emisor.ToString))
                p.Add(New SqlParameter("@rfc", rfc.ToString))
                p.Add(New SqlParameter("@calle", calle.ToString))
                p.Add(New SqlParameter("@num_int", num_int.ToString))
                p.Add(New SqlParameter("@num_ext", num_ext.ToString))
                p.Add(New SqlParameter("@colonia", colonia.ToString))
                p.Add(New SqlParameter("@cp", cp.ToString))
                p.Add(New SqlParameter("@municipio", municipio.ToString))
                p.Add(New SqlParameter("@estado", estado.ToString))
                p.Add(New SqlParameter("@pais", pais.ToString))
                p.Add(New SqlParameter("@serie", serie.ToString))
                p.Add(New SqlParameter("@folio", folio.ToString))
                p.Add(New SqlParameter("@uuid", uuid.ToString))
                p.Add(New SqlParameter("@regimenid", regimenid.ToString))
                p.Add(New SqlParameter("@forma_pago", forma_pago.ToString))
                p.Add(New SqlParameter("@metodo_pago", metodo_pago.ToString))
                p.Add(New SqlParameter("@certificado_emisor", certificado_emisor.ToString))
                p.Add(New SqlParameter("@serie_certificado_emisor", serie_certificado_emisor.ToString))
                p.Add(New SqlParameter("@sello_cfdi", sello_cfdi.ToString))
                p.Add(New SqlParameter("@sello_sat", sello_sat.ToString))
                p.Add(New SqlParameter("@importe", importe))
                p.Add(New SqlParameter("@impuestos", impuestos))
                p.Add(New SqlParameter("@total", total))
                p.Add(New SqlParameter("@xml", nombreXML.ToString))
                p.Add(New SqlParameter("@moneda", moneda))
                p.Add(New SqlParameter("@tipo_cambio", tipo_cambio))
                p.Add(New SqlParameter("@userid", Session("userid")))
                p.Add(New SqlParameter("@notacreditobit", notacreditobit))
                p.Add(New SqlParameter("@usocfdi", usocfdi.ToString))
                IdFacturaProveedor = DataControl.SentenceScalarLong("pFacturaProveedor", 1, p)

                If facturas = "" Then
                    facturas = documento.ToString
                Else
                    facturas = facturas & ",<br/>" & documento.ToString
                End If

                Try
                    If version = "3.3" Then
                        Call CargaPartidas33(FilePath, IdFacturaProveedor)
                    Else
                        Call CargaPartidas(FilePath, IdFacturaProveedor)
                    End If
                Catch ex As Exception
                    mensaje = mensaje & "Error al cargar partidas de factura " & documento.ToString & " -->" & ex.Message.ToString
                End Try
            Else
                mensaje = mensaje & "El comprobante " & documento.ToString & " ya se encuentra registrado." & "<br/>"
            End If
        End If

        Return mensaje

    End Function
    Public Function GetXmlAttribute(ByVal url As String, ByVal campo As String, ByVal nodo As String) As String
        '
        Dim valor As String = ""
        Dim FlujoReader As XmlTextReader = Nothing
        Dim i As Integer
        '
        '   Leer del fichero e ignorar los nodos vacios
        '
        FlujoReader = New XmlTextReader(url)
        FlujoReader.WhitespaceHandling = WhitespaceHandling.None
        Try
            While FlujoReader.Read()
                Select Case FlujoReader.NodeType
                    Case XmlNodeType.Element
                        If FlujoReader.Name = nodo Then
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
    Private Function LeerAddendaHP() As Boolean
        Dim bitAddendaHp As Boolean = 0

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try
            Dim cmd As New SqlCommand("EXEC pFacturaProveedor @cmd=8, @proveedorid='" & proveedorid.SelectedValue & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                bitAddendaHp = rs("incluir_addenda")
            End If

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try


        Return bitAddendaHp
    End Function
    Public Function XML_Lee(ByVal URL As String, ByVal Clave As String, Optional ByVal Grupo As String = "", Optional ByVal SubGrupo As String = "") As String
        Try
            Dim ficheroXML As String = URL 'Path.Combine(Server.MapPath("filesXML"), "iu_T78_timbrado.xml")
            Dim doc As Xml.XmlDocument = New XmlDocument()
            If File.Exists(ficheroXML) Then
                doc.Load(ficheroXML)
            End If

            Dim oManager As New XmlNamespaceManager(New NameTable())
            oManager.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
            oManager.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")
            'Lectura del nodo principal
            Dim nodoPrincipal As Xml.XmlNode = doc.SelectSingleNode(Grupo, oManager)

            If SubGrupo = "" Then
                Return Convert.ToString(GetValor(nodoPrincipal, Clave))
            Else
                Dim nodoActual As Xml.XmlNode = nodoPrincipal.SelectSingleNode(SubGrupo, oManager)
                Return Convert.ToString(GetValor(nodoActual, Clave))
            End If

            'Return json
        Catch ex As Exception

            Return ""

        End Try
    End Function
    Protected Sub RefreshButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Page.Response.Redirect(Request.RawUrl)
    End Sub
    Private Function GetValor(ByVal nodoPadre As Xml.XmlNode, ByVal clave As String) As Object
        Dim valor As Xml.XmlNode = nodoPadre.Attributes(clave)
        Return valor.LastChild.Value
    End Function
    Private Sub CargaPartidas(ByVal URL As String, ByVal facturaProveedorId As Long)
        Dim xDoc As New XmlDocument()

        Dim noIdentificacion As String = ""
        Dim unidad As String = ""
        Dim cantidad As String = ""
        Dim importe As String = ""
        Dim valorUnitario As String = ""
        Dim descripcion As String = ""

        xDoc.Load(URL)

        Dim Conceptos As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Conceptos")

        Dim lista As XmlNodeList = DirectCast(Conceptos(0), XmlElement).GetElementsByTagName("cfdi:Concepto")
        Dim i As Integer = 0
        For Each nodo As XmlElement In lista
            'noIdentificacion = lista(i).Attributes("noIdentificacion").LastChild.Value
            unidad = lista(i).Attributes("unidad").LastChild.Value
            cantidad = lista(i).Attributes("cantidad").LastChild.Value
            'importe = lista(i).Attributes("importe").LastChild.Value
            valorUnitario = lista(i).Attributes("valorUnitario").LastChild.Value
            descripcion = lista(i).Attributes("descripcion").LastChild.Value
            i += 1

            '"@precio='" & valorUnitario.ToString & "', " &
            '"@noIdentificacion='" & noIdentificacion.ToString & "', " &

            Dim DataControl As New DataControl(1)
            DataControl.RunSQLQuery("EXEC pFacturaProveedor @cmd=2," & "@id='" & facturaProveedorId.ToString & "', " & "@descripcion='" & descripcion.ToString & "', " & "@cantidad='" & cantidad.ToString & "', " & "@unidad='" & unidad.ToString & "', " & "@precio='" & valorUnitario.ToString & "'")
            DataControl = Nothing
        Next
    End Sub
    Private Sub CargaPartidas33(ByVal URL As String, ByVal facturaProveedorId As Long)
        Dim xDoc As New XmlDocument()

        Dim noIdentificacion As String = ""
        Dim unidad As String = ""
        Dim cantidad As String = ""
        Dim importe As String = ""
        Dim descuento As Integer = 0
        Dim valorUnitario As String = ""
        Dim descripcion As String = ""
        Dim claveunidad As String = ""
        Dim claveprodserv As String = ""

        xDoc.Load(URL)

        Dim Conceptos As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Conceptos")

        Dim lista As XmlNodeList = DirectCast(Conceptos(0), XmlElement).GetElementsByTagName("cfdi:Concepto")
        Dim i As Integer = 0
        For Each nodo As XmlElement In lista
            Try
                noIdentificacion = lista(i).Attributes("NoIdentificacion").LastChild.Value
            Catch ex As Exception
                noIdentificacion = ""
            End Try
            Try
                unidad = lista(i).Attributes("Unidad").LastChild.Value
            Catch ex As Exception
                unidad = ""
            End Try
            Try
                cantidad = lista(i).Attributes("Cantidad").LastChild.Value
            Catch ex As Exception
                cantidad = 0
            End Try
            Try
                descuento = lista(i).Attributes("Descuento").LastChild.Value
            Catch ex As Exception
                descuento = 0
            End Try
            importe = lista(i).Attributes("Importe").LastChild.Value
            valorUnitario = lista(i).Attributes("ValorUnitario").LastChild.Value
            descripcion = lista(i).Attributes("Descripcion").LastChild.Value
            claveunidad = lista(i).Attributes("ClaveUnidad").LastChild.Value
            claveprodserv = lista(i).Attributes("ClaveProdServ").LastChild.Value
            i += 1

            Dim DataControl As New DataControl(1)
            DataControl.RunSQLQuery("EXEC pFacturaProveedor @cmd=2," & "@id='" & facturaProveedorId.ToString & "', " & "@noIdentificacion='" & noIdentificacion.ToString & "', " & "@descripcion='" & descripcion.ToString & "', " & "@cantidad='" & cantidad.ToString & "', " & "@unidad='" & unidad.ToString & "', " & "@claveunidad='" & claveunidad.ToString & "', " & "@claveprodserv='" & claveprodserv.ToString & "', " & "@precio='" & valorUnitario.ToString & "', " & "@descuento='" & descuento.ToString & "', " & "@importe='" & importe.ToString & "'")
            DataControl = Nothing
        Next
    End Sub
    Private Sub CargaPartidasAddenda(ByVal URL As String, ByVal facturaProveedorId As Long)
        Dim xDoc As New XmlDocument()

        Dim material As String = ""
        Dim clase As String = ""
        Dim serialnumber As String = ""
        Dim deliverynumber As String = ""
        Dim nolinea As String = ""
        Dim sku As String = ""
        Dim concepto As String = ""
        Dim orden As String = ""

        'Dim noIdentificacion As String = ""
        'Dim unidad As String = ""
        'Dim cantidad As String = ""
        'Dim importe As String = ""
        ''Dim valorUnitario As String = ""
        'Dim descripcion As String = ""

        xDoc.Load(URL)


        'folioreferencia = GetXmlAttribute(FilePath, "FolioReferencia", "if:Encabezado").ToString

        Dim Addendas As XmlNodeList = xDoc.GetElementsByTagName("if:Encabezado")

        Dim listas As XmlNodeList = DirectCast(Addendas(0), XmlElement).GetElementsByTagName("if:Cuerpo")
        Dim i As Integer = 0
        For Each nodo As XmlElement In listas
            material = listas(i).Attributes("Material").LastChild.Value
            serialnumber = listas(i).Attributes("SerialNumber").LastChild.Value
            concepto = listas(i).Attributes("Concepto").LastChild.Value
            'importe = lista(i).Attributes("importe").LastChild.Value
            ''valorUnitario = lista(i).Attributes("valorUnitario").LastChild.Value
            'descripcion = lista(i).Attributes("descripcion").LastChild.Value
            i += 1

            '"@serialnumber='" & serialnumber.ToString & "', " &

            Dim DataControl As New DataControl(1)
            DataControl.RunSQLQuery("EXEC pFacturaProveedor @cmd=9," & "@id='" & facturaProveedorId.ToString & "', " & "@concepto='" & concepto.ToString & "', " & "@serialnumber='" & serialnumber.ToString & "'")
            DataControl = Nothing
        Next
    End Sub
    Private Sub MuestraLista()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pFacturaProveedor @cmd=3")
        facturaslist.DataSource = ds
        facturaslist.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub
    Protected Sub facturaslist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles facturaslist.ItemCommand
        Select Case e.CommandName
            Case "cmdVer"
                VerFactura(e.CommandArgument)
            Case "cmdEliminar"
                Eliminafactura(e.CommandArgument)
            Case "cmdSend"
                'Call DatosEmail(e.CommandArgument)
            Case "cmdAdd"
                'IdFactura.Value = e.CommandArgument
                'OportunidadWindow.VisibleOnPageLoad = True
                'Eliminafactura(e.CommandArgument)
        End Select
    End Sub
    Private Sub VerFactura(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try
            ProviderID.Value = id
            Dim cmd As New SqlCommand("EXEC pFacturaProveedor @cmd=4, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                lblProveedor.Text = rs("emisor")
                lblRFC.Text = rs("rfc")
                lblRegimen.Text = rs("regimen_emisor")
                lblCalle.Text = rs("calle")
                lblNoExt.Text = rs("num_ext")
                lblNoInt.Text = rs("num_int")
                lblColonia.Text = rs("colonia")
                lblMunicipio.Text = rs("municipio")
                lblPais.Text = rs("pais")
                lblEstado.Text = rs("estado")
                lblCP.Text = rs("cp")
                lblFechaEmision.Text = rs("fecha_emision")
                lblLugarExpedicion.Text = rs("lugar_expedicion")
                lblFormaPago.Text = rs("metodo_pago")
                lblSerie.Text = rs("serie")
                lblFolio.Text = rs("folio")
                lblUUID.Text = rs("uuid")
                lblFolReferencia.Text = rs("folio_referencia")
                lblOrdenCompra.Text = rs("OrdenCompra")
                lblImporte.Text = FormatCurrency(rs("importe"), 4)
                lblImpuestos.Text = FormatCurrency(rs("impuestos"), 4)
                lblTotal.Text = FormatCurrency(rs("total"), 4)
                lblSelloCFDI.Text = rs("sello_cfdi")
                lblSelloSAT.Text = rs("sello_sat")

                pDetalleProveedor.Visible = True

                mostrarGrid()
            End If

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub
    Private Sub Eliminafactura(ByVal id As Integer)
        Dim DataControl As New DataControl(1)
        DataControl.RunSQLQuery("EXEC pFacturaProveedor @cmd=5, @id='" & id & "'")
        DataControl = Nothing
        Call MuestraLista()
    End Sub
    Protected Sub facturaslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles facturaslist.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Facturas" Then

                Dim lnkdel As LinkButton = CType(dataItem("Eliminar").FindControl("lnkEliminar"), LinkButton)
                Dim imgSend As ImageButton = CType(e.Item.FindControl("imgSend"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('¿Esta seguro que desea eliminar esta factura?');")

                'lnkdel.Enabled = Valido

                'If Valido = False Then
                '    lnkdel.ToolTip = "Acceso restringido."
                'End If

                'Dim lnkVer As ImageButton = CType(dataItem("Add").FindControl("btnAdd"), ImageButton)

                'lnkVer.Attributes.Add("onClick", "javascript:openRadWindowPDF(" & e.Item.DataItem("id").ToString() & "); return false;")
                'lnkVer.CausesValidation = False

                imgSend.Attributes.Add("onClick", "javascript:openRadWindowCorreo(" & e.Item.DataItem("id").ToString() & "); return false;")
                imgSend.CausesValidation = False

            End If

        End If
    End Sub
    Private Sub facturaslist_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles facturaslist.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pFacturaProveedor @cmd=3")
        facturaslist.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub
    Private Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pFacturaProveedor @cmd=10, @proveedorid='" & ProviderID.Value & "'")
        RadGrid1.DataSource = ds
        ObjData = Nothing
    End Sub
    Private Sub MostrarGrid()
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pFacturaProveedor @cmd=10, @proveedorid='" & ProviderID.Value & "'")
        RadGrid1.DataSource = ds
        RadGrid1.DataBind()
        ObjData = Nothing
    End Sub
    Private Sub SendEmail(ByVal userid As Long, ByVal IdFacturaProveedor As Integer)
        Dim Factura As String = ""
        Dim ordenCompra As String = ""
        Dim serie As String = ""
        Dim folio As String = "0"
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pCatalogos @cmd=11, @idFacturaProveedor='" & IdFacturaProveedor.ToString & "'", connF)
        Try

            connF.Open()

            Dim rsF = cmdF.ExecuteReader

            If rsF.Read Then
                'no = rsF("id").ToString
                serie = rsF("serie").ToString
                folio = rsF("folio").ToString
                ordenCompra = rsF("ordenCompra").ToString
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        Factura = serie + folio
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
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pEnviaEmailProveedor @userid='" & userid.ToString & "'", conn)
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
        mensaje = "<html><head></head><body><br/>"
        mensaje += "Estimado(a) Proveedor, Se ha recibido la Factura <strong>" & Factura.ToString & "</strong> para la Orden de Compra <strong>" & ordenCompra.ToString & "</strong><br /><br />"

        mensaje += "<br /><br />"
        mensaje += "Atentamente.<br /><br />"
        mensaje += "<strong>" & razonsocial.ToString & "</strong><br /><br /></body></html>"


        Dim objMM As New MailMessage
        objMM.To.Add(correo)
        objMM.From = New MailAddress(email_from, razonsocial)
        objMM.IsBodyHtml = True
        objMM.Priority = MailPriority.Normal
        objMM.Subject = razonsocial & " - Orden Compra"
        objMM.Body = mensaje
        '
        '   Agrega anexos
        '
        'Dim AttachXML As Net.Mail.Attachment = New Net.Mail.Attachment(Server.MapPath("~/portalcfd/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml")
        'Dim AttachPDF As Net.Mail.Attachment = New Net.Mail.Attachment(Server.MapPath("~/portalcfd/pdf/") & "iu_" & serie.ToString & folio.ToString & ".pdf")
        '
        'objMM.Attachments.Add(AttachXML)
        'objMM.Attachments.Add(AttachPDF)
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
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            SmtpMail = Nothing
        End Try
        objMM = Nothing
    End Sub

End Class