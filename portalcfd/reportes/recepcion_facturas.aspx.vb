Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Threading
Imports System.Globalization
Imports System.Xml
Imports Telerik.Web.UI

Partial Class portalcfd_proveedores_recepcion_facturas
    Inherits System.Web.UI.Page
    Private ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If System.Configuration.ConfigurationManager.AppSettings("proveedores") = 0 Then
            Response.Redirect("~/portalcfd/proveedores/informacion.aspx")
        End If
        If Not IsPostBack Then
            Dim ObjData As New DataControl
            ObjData.Catalogo(proveedorid, "select id, razonsocial as nombre from tblMisProveedores order by razonsocial", 0)
            ObjData.Catalogo(monedaid, "select id, nombre as nombre from tblMoneda order by id", 1)
            ObjData.Catalogo(ordencompraid, "select id, 'O.C.-' + convert(varchar,id) + ' - ' + convert(varchar(10), fecha,103) as nombre from tblOrdenCompra where isnull(facturaid,0)=0 and isnull(gastoid,0)=0 order by id", 0)
            ObjData = Nothing
        End If
    End Sub

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
            cantidad = lista(i).Attributes("cantidad").LastChild.Value
            Try
                unidad = lista(i).Attributes("unidad").LastChild.Value
            Catch ex As Exception
                unidad = ""
            End Try
            Try
                noIdentificacion = lista(i).Attributes("noIdentificacion").LastChild.Value
            Catch ex As Exception
                noIdentificacion = ""
            End Try
            descripcion = lista(i).Attributes("descripcion").LastChild.Value
            Try
                valorUnitario = lista(i).Attributes("valorUnitario").LastChild.Value
            Catch ex As Exception
                valorUnitario = "0"
            End Try
            Try
                importe = lista(i).Attributes("importe").LastChild.Value
            Catch ex As Exception
                importe = "0"
            End Try

            i += 1

            Dim DataControl As New DataControl
            Dim p As New ArrayList
            p.Add(New SqlParameter("@cmd", 2))
            p.Add(New SqlParameter("@id", facturaProveedorId))
            p.Add(New SqlParameter("@noIdentificacion", noIdentificacion))
            p.Add(New SqlParameter("@descripcion", descripcion))
            p.Add(New SqlParameter("@cantidad", cantidad))
            p.Add(New SqlParameter("@unidad", unidad))
            p.Add(New SqlParameter("@precio", valorUnitario))
            p.Add(New SqlParameter("@importe", importe))
            DataControl.Sentence("pFacturaProveedor", 1, p)
            DataControl = Nothing
        Next
    End Sub

    Private Sub MuestraLista()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
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
            Case "cmdXML"
                DownloadXML(e.CommandArgument)
            Case "cmdPDF"
                DownloadPDF(e.CommandArgument)
        End Select
    End Sub

    Private Sub VerFactura(ByVal id As Long)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pFacturaProveedor @cmd=4, @id='" & id.ToString & "'", conn)

            conn.Open()
            Dim pdf As String = ""
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                IdFacturaProveedor.Value = id
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
                lblImporte.Text = FormatCurrency(rs("importe"), 4)
                lblImpuestos.Text = FormatCurrency(rs("impuestos"), 4)
                lblTotal.Text = FormatCurrency(rs("total"), 4)
                lblSelloCFDI.Text = rs("sello_cfdi")
                lblSelloSAT.Text = rs("sello_sat")
                pdf = rs("pdf")

                pDetalleProveedor.Visible = True

                If pdf.Length <= 0 Then
                    panelPDF.Visible = True
                Else
                    panelPDF.Visible = False
                End If

            End If

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Private Sub Eliminafactura(ByVal id As Integer)
        Dim DataControl As New DataControl
        DataControl.RunSQLQuery("EXEC pFacturaProveedor @cmd=5, @id='" & id & "'")
        DataControl = Nothing
        Call MuestraLista()
    End Sub

    Protected Sub facturaslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles facturaslist.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Facturas" Then

                Dim lnkdel As LinkButton = CType(dataItem("Eliminar").FindControl("lnkEliminar"), LinkButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('¿Esta seguro que desea eliminar esta factura?');")

                Dim lblXML As Label = CType(dataItem("XML").FindControl("lblXML"), Label)
                Dim lblPDF As Label = CType(dataItem("XML").FindControl("lblPDF"), Label)
                Dim lnkXML As LinkButton = CType(dataItem("XML").FindControl("lnkXML"), LinkButton)
                Dim lnkPDF As LinkButton = CType(dataItem("XML").FindControl("lnkPDF"), LinkButton)

                If lblXML.Text = "" Then
                    lnkXML.Text = ""
                End If

                If lblPDF.Text = "" Then
                    lnkPDF.Text = ""
                End If

            End If

        End If
    End Sub

    Protected Sub facturaslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles facturaslist.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pFacturaProveedor @cmd=3")
        facturaslist.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        ''
    End Sub

    Protected Sub btnCargarXML_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCargarXML.Click

        Dim idFacturaProveedor As Long = 0
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
        Dim regimen_emisor As String = ""
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
        Dim mensaje As String = ""
        Dim facturas As String = ""

        Dim moneda As String = ""
        Dim tipo_cambio As Decimal = 0

        If archivosXML.UploadedFiles.Count > 0 Then

            Dim nombreXML As String = ""
            Dim nombrePDF As String = ""
            Dim i As Integer = 0

            For Each f As Telerik.Web.UI.UploadedFile In archivosXML.UploadedFiles
                If f.GetExtension().ToString <> ".xml" Then
                    mensaje = mensaje & "El documento " & f.GetName() & " no cuenta con extención de .xml válida" & "<br/>"
                Else
                    For i = 1 To 99
                        nombreXML = i.ToString + "_" + f.GetName.ToString
                        If Not File.Exists(Server.MapPath("../proveedores/xml/") & nombreXML) Then
                            f.SaveAs(Server.MapPath("../proveedores/xml/") & nombreXML)

                            Dim FilePath = Server.MapPath("~/portalcfd/proveedores/xml/") & nombreXML

                            If File.Exists(FilePath) Then

                                fecha_emision = XML_Lee(FilePath, "fecha", "cfdi:Comprobante", "")
                                lugar_expedicion = XML_Lee(FilePath, "LugarExpedicion", "cfdi:Comprobante", "")
                                emisor = XML_Lee(FilePath, "nombre", "cfdi:Comprobante", "cfdi:Emisor")
                                rfc = XML_Lee(FilePath, "rfc", "cfdi:Comprobante", "cfdi:Emisor")

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
                                regimen_emisor = XML_Lee(FilePath, "Regimen", "cfdi:Comprobante", "cfdi:Emisor/cfdi:RegimenFiscal ")
                                forma_pago = XML_Lee(FilePath, "formaDePago", "cfdi:Comprobante", "")
                                metodo_pago = XML_Lee(FilePath, "metodoDePago", "cfdi:Comprobante", "")

                                certificado_emisor = XML_Lee(FilePath, "certificado", "cfdi:Comprobante", "")
                                serie_certificado_emisor = XML_Lee(FilePath, "noCertificado", "cfdi:Comprobante", "")

                                sello_cfdi = XML_Lee(FilePath, "selloCFD", "cfdi:Comprobante", "cfdi:Complemento/tfd:TimbreFiscalDigital")
                                sello_sat = XML_Lee(FilePath, "selloSAT", "cfdi:Comprobante", "cfdi:Complemento/tfd:TimbreFiscalDigital")

                                importe = Convert.ToDecimal(XML_Lee(FilePath, "subTotal", "cfdi:Comprobante", ""))
                                total = Convert.ToDecimal(XML_Lee(FilePath, "total", "cfdi:Comprobante", ""))
                                moneda = XML_Lee(FilePath, "Moneda", "cfdi:Comprobante", "")

                                Try
                                    tipo_cambio = Convert.ToDecimal(XML_Lee(FilePath, "TipoCambio", "cfdi:Comprobante", ""))
                                Catch ex As Exception
                                    tipo_cambio = 0
                                End Try

                                impuestos = (Convert.ToDecimal(total) - Convert.ToDecimal(importe))

                                Dim DataControl As New DataControl
                                Dim validauuid As Long
                                validauuid = DataControl.RunSQLScalarQuery("select count(id) from tblFacturaProveedor where uuid='" & uuid & "'")

                                If validauuid <= 0 Then
                                    idFacturaProveedor = DataControl.RunSQLScalarQuery("EXEC pFacturaProveedor @cmd=1," & _
                                    "@proveedorid='" & proveedorid.SelectedValue.ToString & "', " & _
                                    "@ordencompraid='" & ordencompraid.SelectedValue.ToString & "', " & _
                                    "@fecha_emision='" & fecha_emision & "', " & _
                                    "@lugar_expedicion='" & lugar_expedicion & "', " & _
                                    "@emisor='" & emisor & "', " & _
                                    "@rfc='" & rfc & "', " & _
                                    "@calle='" & calle & "', " & _
                                    "@num_int='" & num_int & "', " & _
                                    "@num_ext='" & num_ext & "', " & _
                                    "@colonia='" & colonia & "', " & _
                                    "@cp='" & cp & "', " & _
                                    "@municipio='" & municipio & "', " & _
                                    "@estado='" & estado & "', " & _
                                    "@pais='" & pais & "', " & _
                                    "@serie='" & serie & "', " & _
                                    "@folio='" & folio & "', " & _
                                    "@uuid='" & uuid & "', " & _
                                    "@regimen_emisor='" & regimen_emisor & "', " & _
                                    "@forma_pago='" & forma_pago & "', " & _
                                    "@metodo_pago='" & metodo_pago & "', " & _
                                    "@certificado_emisor='" & certificado_emisor & "', " & _
                                    "@serie_certificado_emisor='" & serie_certificado_emisor & "', " & _
                                    "@sello_cfdi='" & sello_cfdi & "', " & _
                                    "@sello_sat='" & sello_sat & "', " & _
                                    "@importe='" & importe.ToString & "', " & _
                                    "@impuestos='" & impuestos.ToString & "', " & _
                                    "@total='" & total.ToString & "', " & _
                                    "@monedaid='" & monedaid.SelectedValue.ToString & "', " & _
                                    "@moneda='" & moneda & "', " & _
                                    "@tipo_cambio='" & tipo_cambio.ToString & "', " & _
                                    "@xml='" & nombreXML & "', " & _
                                    "@pdf='" & nombrePDF & "', " & _
                                    "@userid='" & Session("userid").ToString & "'")

                                    If facturas = "" Then
                                        facturas = f.GetName().ToString
                                    Else
                                        facturas = facturas & ",<br/>" & f.GetName().ToString
                                    End If

                                    Try
                                        Call CargaPartidas(FilePath, idFacturaProveedor)
                                    Catch ex As Exception
                                        mensaje = mensaje & "Error al cargar partidas de factura " & f.GetName().ToString & " -->" & ex.Message.ToString
                                    End Try
                                Else
                                    mensaje = mensaje & "El comprobante " & f.GetName().ToString & " ya se encuentra registrado." & "<br/>"
                                End If
                                DataControl = Nothing
                            End If
                            Exit For
                        End If
                    Next
                End If
            Next
        End If

        Call MuestraLista()

        If facturas <> "" Then
            lblMensaje.ForeColor = Drawing.Color.Green
            lblMensaje.Text = "Se cargó con éxito las siguientes facturas :" & "<br/>" & facturas
        End If

        If mensaje <> "" Then
            lblError.ForeColor = Drawing.Color.Red
            lblError.Text = mensaje
        End If

    End Sub

    Protected Sub btnCargaPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCargaPDF.Click
        Dim mensaje As String = ""
        Dim errorpdf As String = ""
        If archivoPDF.UploadedFiles.Count > 0 Then

            Dim nombrePDF As String = ""
            Dim i As Integer = 0

            For Each f As Telerik.Web.UI.UploadedFile In archivoPDF.UploadedFiles
                If f.GetExtension().ToString <> ".pdf" Then
                    errorpdf = mensaje & "El documento " & f.GetName() & " no cuenta con extención de .pdf válida" & "<br/>"
                Else
                    Dim DataControl As New DataControl
                    For i = 1 To 99
                        nombrePDF = i.ToString + "_" + f.GetName.ToString
                        If Not File.Exists(Server.MapPath("~/portalcfd/proveedores/pdf/") & nombrePDF) Then
                            f.SaveAs(Server.MapPath("~/portalcfd/proveedores/pdf/") & nombrePDF)
                            Dim FilePath = Server.MapPath("~/portalcfd/proveedores/pdf/") & nombrePDF
                            If File.Exists(FilePath) Then
                                DataControl.RunSQLQuery("EXEC pFacturaProveedor @cmd=7, @pdf='" & nombrePDF & "', @id='" & IdFacturaProveedor.Value.ToString & "'")
                                mensaje = "El documento " & f.GetName() & " se ha guardado correctamente."
                            End If
                            Exit For
                        End If
                    Next
                    DataControl = Nothing
                End If
            Next
        End If

        Call MuestraLista()

        If mensaje <> "" Then
            panelPDF.Visible = False
            lblMensajePDF.ForeColor = Drawing.Color.Green
            lblMensajePDF.Text = mensaje
        End If

        If errorpdf <> "" Then
            lblMensajePDF.ForeColor = Drawing.Color.Red
            lblMensajePDF.Text = errorpdf
        End If

    End Sub

    Private Sub DownloadXML(ByVal id As Long)
        Dim xml As String = ""
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pFacturaProveedor @cmd=4, @id='" & id.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                xml = rs("xml").ToString
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Dim FilePath = Server.MapPath("~/portalcfd/proveedores/xml/") & xml.ToString
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

    Private Sub DownloadPDF(ByVal id As Long)
        Dim pdf As String = ""
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pFacturaProveedor @cmd=4, @id='" & id.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                pdf = rs("pdf").ToString
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Dim FilePath = Server.MapPath("~/portalcfd/proveedores/pdf/") & pdf.ToString
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

End Class
