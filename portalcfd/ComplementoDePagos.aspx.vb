Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Reporting.Processing
Imports System.IO
Imports System.Xml
Imports System.Xml.Xsl
Imports ThoughtWorks.QRCode.Codec
Imports System.Security.Cryptography.X509Certificates
Imports System.Threading
Imports System.Globalization
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices
Imports Ionic.Zip
Imports System.Web.Services.Protocols
Imports System.Net.Security
Imports ThoughtWorks.QRCode
Public Class ComplementoDePagos
    Inherits System.Web.UI.Page

    Private total As Decimal = 0
    Private importe As Decimal = 0
    Private archivoLlavePrivada As String = ""
    Private contrasenaLlavePrivada As String = ""
    Private archivoCertificado As String = ""
    Private m_xmlDOM As New XmlDocument
    Const URI_SAT = "http://www.sat.gob.mx/cfd/3"
    Private listErrores As New List(Of String)
    Private Comprobante As XmlNode
    Private Url As Integer = 0
    Private qrBackColor As Integer = System.Drawing.Color.FromArgb(255, 255, 255, 255).ToArgb
    Private qrForeColor As Integer = System.Drawing.Color.FromArgb(255, 0, 0, 0).ToArgb
    Private data As Byte()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        chkAll.Attributes.Add("onclick", "checkedAll(" & Me.Form.ClientID.ToString & ");")
        '
        If Not IsPostBack Then

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblClientsSelectionLegend.Text = Resources.Resource.lblClientsSelectionLegend
            lblClientItems.Text = "Facturas Pendientes de Pago"
            lblResume.Text = Resources.Resource.lblResume

            '''''''''''''''''''''''''''''''''
            'Combobox Values & Empty Message'
            '''''''''''''''''''''''''''''''''

            Dim ObjCat As New DataControl(1)
            ObjCat.Catalogo(cmbCliente, "select id, UPPER(isnull(razonsocial,'')) as razonsocial from tblMisClientes order by razonsocial", 0)
            ObjCat.Catalogo(cmbTipoPago, "select top 1 id, nombre from tblTipoPagos", 0)
            ObjCat.Catalogo(cmbFormaPago, "select id, id + ' - ' + nombre as nombre from tblFormaPago where id not in (99) order by nombre", 0)
            ObjCat.Catalogo(cmbMoneda, "select id, nombre from tblMoneda where id not in (3) order by nombre", 1)
            ObjCat = Nothing

            If cmbMoneda.SelectedValue <> 1 Then
                txtTipoCambio.Text = 0
                txtTipoCambio.Enabled = True
                valTipoCambio.Enabled = True
            Else
                txtTipoCambio.Text = 0
                'txtTipoCambio.Enabled = False
                'valTipoCambio.Enabled = False
            End If

            cmbCliente.Text = Resources.Resource.cmbEmptyMessage
            lblSubTotal.Text = Resources.Resource.lblSubTotal
            lblTotal.Text = Resources.Resource.lblTotal

            btnCreateInvoice.Text = Resources.Resource.btnCreateInvoice
            btnCancelInvoice.Text = Resources.Resource.btnCancelInvoice
            '
            '   Protege contra doble clic la creación de la factura
            '
            btnCreateInvoice.Attributes.Add("onclick", "javascript:" + btnCreateInvoice.ClientID + ".disabled=true;" + ClientScript.GetPostBackEventReference(btnCreateInvoice, ""))
            '
            '
            panelItemsRegistration.Visible = False
            itemsList.Visible = False
            panelResume.Visible = False

            CrearTablaTemp()

            If Not String.IsNullOrEmpty(Request("id")) Then
                Session("CFD") = Request("id")
                Session("PAGOCFD") = 0

                Call CargaCFD()
                Call CargarSaldoPendiente()
            Else
                Session("CFD") = 0
                Session("PAGOCFD") = 0
            End If

        End If
    End Sub

    Private Sub CrearTablaTemp()
        Dim dt As New DataTable()
        dt.Columns.Add("id")
        dt.Columns.Add("fecha")
        dt.Columns.Add("serie")
        dt.Columns.Add("folio")
        dt.Columns.Add("uuid")
        dt.Columns.Add("monedaDR")
        dt.Columns.Add("total")
        dt.Columns.Add("saldo")
        dt.Columns.Add("saldoanterior")
        dt.Columns.Add("monto")
        dt.Columns.Add("montomxn")
        dt.Columns.Add("chkcfdid")
        Session("TmpDetalleComplemento") = dt
    End Sub

    Private Sub CargaCFD()
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("EXEC pCFD @cmd=10, @cfdid='" & Session("CFD").ToString & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            panelItemsRegistration.Visible = True
            panelResume.Visible = True

            If rs.Read() Then

                cmbCliente.SelectedValue = rs("clienteid")

                If cmbCliente.SelectedValue > 0 Then
                    Call DisplayItems()
                    panelItemsRegistration.Visible = True
                    itemsList.Visible = True
                End If

            End If

            rs.Close()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        '
        'Call CargaRazonSocial(cmbCliente.SelectedValue)
        'Call MuestraDetalle(cmbRazonSocail.SelectedValue)
        '
    End Sub

    Private Sub DisplayItems()
        itemsList.MasterTableView.NoMasterRecordsText = Resources.Resource.ItemsEmptyGridMessage
        Session("TmpDetalleComplemento") = ObtenerItems().Tables(0)
        itemsList.DataSource = Session("TmpDetalleComplemento")
        itemsList.DataBind()
    End Sub

    Private Sub CargaCliente(ByVal ClienteId As Long)
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("EXEC pMisClientes @cmd=7, @clienteid='" & ClienteId.ToString & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            panelItemsRegistration.Visible = True

            If rs.Read() Then
                Dim ObjData As New DataControl(1)
                ObjData.Catalogo(cmbFormaPago, "select id, id + ' - ' + nombre as nombre FROM tblFormaPago where id not in (99) order by nombre", rs("formapagoid"))
                ObjData.Catalogo(cmbCtaOrdenante, "select id, isnull(numctapago,'') as numctapago FROM tblCuentasBancarias where clienteid='" & cmbCliente.SelectedValue & "' order by numctapago", rs("idctaOrdenante"))
                ObjData.Catalogo(cmbCtaBeneficiario, "select id, isnull(numctapago,'') as numctapago FROM tblCuentasBeneficiario order by banco", rs("idctaBeneficiario"))
                ObjData = Nothing

                txtRfcBeneficiario.Text = rs("rfcbeneficiario")
                txtRFCCtaOrdenante.Text = rs("rfcctaordenante")

                If cmbFormaPago.SelectedValue.Length > 0 Then
                    If cmbFormaPago.SelectedValue = "02" Or cmbFormaPago.SelectedValue = "03" Or cmbFormaPago.SelectedValue = "04" Or cmbFormaPago.SelectedValue = "05" Or cmbFormaPago.SelectedValue = "06" Or cmbFormaPago.SelectedValue = "08" Or cmbFormaPago.SelectedValue = "17" Or cmbFormaPago.SelectedValue = "28" Or cmbFormaPago.SelectedValue = "29" Then
                        panelRecepcionPago.Visible = True
                    Else
                        panelRecepcionPago.Visible = False
                    End If
                End If
            End If

            rs.Close()
            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Function ObtenerItems() As DataSet

        Dim conn As New SqlConnection(Session("conexion"))

        Dim cmd As New SqlDataAdapter("EXEC pComplementoDePago @cmd=1, @clienteid='" & cmbCliente.SelectedValue & "', @tipoid=1", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function

    Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)
        CargarSaldoPendiente()
        RadWindow1.VisibleOnPageLoad = False
    End Sub

    Public Sub CargarSaldoPendiente()
        Dim i As Integer = 0

        Call ValidarExistComplemento()

        Session("TmpDetalleComplemento").Rows.Clear()

        For Each dataItem As Telerik.Web.UI.GridDataItem In itemsList.MasterTableView.Items
            Dim detalleid As String = dataItem.GetDataKeyValue("id").ToString
            Dim lblfechaCFDI As Label = DirectCast(dataItem.FindControl("lblfechaCFDI"), Label)
            Dim lbltotalCFDI As Label = DirectCast(dataItem.FindControl("lbltotalCFDI"), Label)
            Dim lblSerie As Label = DirectCast(dataItem.FindControl("lblSerie"), Label)
            Dim lblFolio As Label = DirectCast(dataItem.FindControl("lblFolio"), Label)
            Dim lblUUID As Label = DirectCast(dataItem.FindControl("lblUUID"), Label)
            Dim lblMonedaDR As Label = DirectCast(dataItem.FindControl("lblMonedaDR"), Label)
            Dim lblSaldo As Label = DirectCast(dataItem.FindControl("lblSaldo"), Label)
            Dim lblSaldoAnterior As Label = DirectCast(dataItem.FindControl("lblSaldoAnterior"), Label)
            Dim txtMonto As RadNumericTextBox = DirectCast(dataItem.FindControl("txtMonto"), RadNumericTextBox)
            Dim txtImporteDivisa As RadNumericTextBox = DirectCast(dataItem.FindControl("txtImporteDivisa"), RadNumericTextBox)
            Dim chkcfdid As System.Web.UI.WebControls.CheckBox = DirectCast(dataItem.FindControl("chkcfdid"), System.Web.UI.WebControls.CheckBox)

            Dim MonedaP As String = ""
            Dim Monto As Decimal = 0
            Dim TipoCambio As Decimal = 0
            Dim ImporteDivisa As Decimal = 0
            Dim SaldoAnterior As Decimal = 0
            Dim Saldo As Decimal = 0

            If cmbMoneda.SelectedValue = 1 Then
                MonedaP = "MXN"
            ElseIf cmbMoneda.SelectedValue = 2 Then
                MonedaP = "USD"
            End If
            If chkcfdid.Checked = True Then
                If MonedaP = "MXN" And lblMonedaDR.Text = "USD" Then
                    valTipoCambio.Enabled = True
                    txtMonto.Enabled = False
                    Try
                        ImporteDivisa = Convert.ToDecimal(txtImporteDivisa.Text)
                    Catch ex As Exception
                        ImporteDivisa = 0
                    End Try

                    Try
                        TipoCambio = 1 / Convert.ToDecimal(txtTipoCambio.Text)
                    Catch ex As Exception
                        TipoCambio = 0
                    End Try

                    Try
                        txtMonto.Text = ImporteDivisa * TipoCambio
                    Catch ex As Exception
                        txtMonto.Text = 0
                    End Try

                    Try
                        Monto = Convert.ToDecimal(txtMonto.Text)
                    Catch ex As Exception
                        Monto = 0
                    End Try

                    txtMonto.Enabled = False

                Else
                    valTipoCambio.Enabled = False
                    txtMonto.Enabled = True
                    Try
                        Monto = Convert.ToDecimal(txtMonto.Text)
                    Catch ex As Exception
                        Monto = 0
                    End Try
                End If
            Else
                Try
                    Monto = Convert.ToDecimal(txtMonto.Text)
                Catch ex As Exception
                    Monto = 0
                End Try
                Try
                    ImporteDivisa = Convert.ToDecimal(txtImporteDivisa.Text)
                Catch ex As Exception
                    ImporteDivisa = 0
                End Try
            End If

            Try
                SaldoAnterior = Convert.ToDecimal(lblSaldoAnterior.Text)
            Catch ex As Exception
                SaldoAnterior = 0
            End Try

            If chkcfdid.Checked = True Then
                Saldo = SaldoAnterior - Monto
            Else
                Saldo = SaldoAnterior
            End If

            Dim dr As DataRow = Session("TmpDetalleComplemento").NewRow()
            dr.Item("id") = detalleid.ToString
            dr.Item("fecha") = lblfechaCFDI.Text
            dr.Item("serie") = lblSerie.Text
            dr.Item("folio") = lblFolio.Text
            dr.Item("monedaDR") = lblMonedaDR.Text
            dr.Item("uuid") = lblUUID.Text
            dr.Item("total") = lbltotalCFDI.Text
            dr.Item("saldo") = Saldo.ToString
            dr.Item("saldoanterior") = SaldoAnterior.ToString
            dr.Item("monto") = Monto.ToString
            dr.Item("montomxn") = ImporteDivisa
            dr.Item("chkcfdid") = chkcfdid.Checked
            Session("TmpDetalleComplemento").Rows.Add(dr)
        Next

        itemsList.DataSource = Session("TmpDetalleComplemento")
        itemsList.DataBind()

        CargaTotalCFDI()
        panelResume.Visible = True

    End Sub

    Private Sub ValidarExistComplemento()
        If Session("CFD") = 0 Then
            GetCFD()
        End If

        If Session("PAGOCFD") = 0 Then
            GetPAGOCFD()
        End If
    End Sub

    Protected Sub GetCFD()

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("EXEC pCFD @cmd=1, @clienteid='" & cmbCliente.SelectedValue & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                Session("CFD") = rs("cfdid")

            End If

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Protected Sub GetPAGOCFD()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        If fecha.SelectedDate Is Nothing Then
            fecha.SelectedDate = Now()
        End If

        'If cmbFormaPago.SelectedValue = "01" Then
        '    txtNumOperacion.Text = "01"
        'End If

        Dim fechaP As String = fecha.SelectedDate.Value.ToShortDateString & " " & "12:00:00"

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("EXEC pComplementoDePago @cmd=2, @clienteid='" & cmbCliente.SelectedValue & "', @formapagoid='" & cmbFormaPago.SelectedValue & "', @numoperacion='" & txtNumOperacion.Text & "', @ctaordenante='" & cmbCtaOrdenante.SelectedItem.Text & "', @rfcemisorctaord='" & txtRFCCtaOrdenante.Text & "', @ctabeneficiaria='" & cmbCtaBeneficiario.SelectedItem.Text & "', @rfcemisorctabeneficiaria='" & txtRfcBeneficiario.Text & "', @nomBancoOrdext='" & txtBancoExtr.Text & "', @fecha_pago='" & fechaP.ToString & "'", conn)

        Dim moneda As String = ""

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Session("PAGOCFD") = rs("pagoid")
            End If

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub CargaTotalCFDI()
        Dim monedaP As String = ""
        Dim saldo As Decimal = 0
        importe = 0
        total = 0

        If cmbMoneda.SelectedValue = 1 Then
            monedaP = "MXN"
        ElseIf cmbMoneda.SelectedValue = 2 Then
            monedaP = "USD"
        End If

        For Each dataItem As Telerik.Web.UI.GridDataItem In itemsList.MasterTableView.Items
            Dim txtMonto As RadNumericTextBox = DirectCast(dataItem.FindControl("txtMonto"), RadNumericTextBox)
            Dim txtImporteDivisa As RadNumericTextBox = DirectCast(dataItem.FindControl("txtImporteDivisa"), RadNumericTextBox)
            Dim lblMonedaDR As Label = DirectCast(dataItem.FindControl("lblMonedaDR"), Label)
            Dim chkcfdid As System.Web.UI.WebControls.CheckBox = DirectCast(dataItem.FindControl("chkcfdid"), System.Web.UI.WebControls.CheckBox)

            If chkcfdid.Checked = True Then
                If monedaP <> lblMonedaDR.Text Then
                    saldo = txtImporteDivisa.Text
                Else
                    saldo = txtMonto.Text
                End If
            Else
                saldo = 0
            End If

            If total > 0 Then
                total = total + saldo
            Else
                total = saldo
            End If
        Next

        importe = total
        total = total

        lblSubTotalValue.Text = FormatCurrency(importe, 2).ToString
        lblTotalValue.Text = FormatCurrency(total, 2).ToString

    End Sub

    Public Sub txtMonto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim valido As Boolean
        valido = ValidarCFDI()

        If valido = True Then
            CargarSaldoPendiente()
            RadWindow1.VisibleOnPageLoad = False
        End If
    End Sub

    Function ValidarCFDI()
        Dim Validar As Boolean = False
        For Each dataItem As Telerik.Web.UI.GridDataItem In itemsList.MasterTableView.Items
            Dim chkcfdid As System.Web.UI.WebControls.CheckBox = DirectCast(dataItem.FindControl("chkcfdid"), System.Web.UI.WebControls.CheckBox)

            If chkcfdid.Checked = True Then
                Validar = True
            End If
        Next
        Return Validar
    End Function

    Private Sub cmbFormaPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFormaPago.SelectedIndexChanged
        If cmbFormaPago.SelectedValue.Length > 0 Then
            If cmbFormaPago.SelectedValue = "02" Or cmbFormaPago.SelectedValue = "03" Or cmbFormaPago.SelectedValue = "04" Or cmbFormaPago.SelectedValue = "05" Or cmbFormaPago.SelectedValue = "06" Or cmbFormaPago.SelectedValue = "08" Or cmbFormaPago.SelectedValue = "17" Or cmbFormaPago.SelectedValue = "28" Or cmbFormaPago.SelectedValue = "29" Then
                panelRecepcionPago.Visible = True
                If cmbCtaOrdenante.SelectedValue > 0 Then
                    Call CtaExtranjero()
                End If
            Else
                ClearClienteProveedor()
                panelRecepcionPago.Visible = False
            End If
            'Validadores()
        End If
    End Sub

    Private Sub CtaExtranjero()
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("EXEC pCatalogoCuentas @cmd=7, @id='" & cmbCtaOrdenante.SelectedValue & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read() Then
                txtBancoExtr.Text = rs("bancoextranjero")
            End If
            '
            rs.Close()
            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub

    Private Sub ClearClienteProveedor()
        txtRfcBeneficiario.Text = ""
        cmbCtaBeneficiario.SelectedValue = 0
        txtRFCCtaOrdenante.Text = ""
        cmbCtaOrdenante.SelectedValue = 0
        txtBancoExtr.Text = ""
        txtNumOperacion.Text = ""
        cmbTipoPago.SelectedValue = 0
    End Sub

    Private Sub cmbCliente_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCliente.SelectedIndexChanged
        Call CargaCliente(cmbCliente.SelectedValue)
        Call DisplayItems()
        panelItemsRegistration.Visible = True
        itemsList.Visible = True
    End Sub

    Private Sub itemsList_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles itemsList.NeedDataSource
        itemsList.DataSource = Session("TmpDetalleComplemento")
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Response.Redirect("~/portalcfd/ComplementosEmitidos.aspx")
    End Sub

    Private Sub btnCancelInvoice_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelInvoice.Click
        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("exec pCFD @cmd=31, @cfdid='" & Session("CFD").ToString & "'")
        ObjData.RunSQLQuery("exec pComplementoDePago @cmd=9, @pagoid='" & Session("PAGOCFD").ToString & "'")
        ObjData = Nothing
        '
        Session("CFD") = 0
        Session("PAGOCFD") = 0
        '
        Response.Redirect("~/portalcfd/ComplementosEmitidos.aspx")
        '
    End Sub

    Private Sub btnCreateInvoice_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreateInvoice.Click
        If Page.IsValid Then
            Dim Timbrado As Boolean = False
            Dim MensageError As String = ""
            RadWindow1.VisibleOnPageLoad = False

            Call AgregaCFDI()
            '
            Call CargaTotalCFDI()
            '
            '   Guadar Metodo de Pago
            '
            Call GuadarMetodoPago()
            '
            '   Rutina de generación de XML CFDI Versión 3.3
            '
            m_xmlDOM = CrearDOM()
            '
            '   Verifica que tipo de comprobante se va a emitir
            '
            Call AsignaSerieFolio()

            Comprobante = CrearNodoComprobante()

            m_xmlDOM.AppendChild(Comprobante)
            IndentarNodo(Comprobante, 1)
            '
            '   Agrega los datos del emisor
            '
            Call ConfiguraEmisor()
            '
            '   Asigna los datos del receptor
            '
            Call ConfiguraReceptor()
            '
            '   Agrega los conceptos de la factura
            '
            CrearNodoConceptos(Comprobante)
            IndentarNodo(Comprobante, 1)
            '
            CrearNodoPagos(Comprobante)
            IndentarNodo(Comprobante, 1)
            '
            SellarCFD(Comprobante)
            m_xmlDOM.InnerXml = (Replace(m_xmlDOM.InnerXml, "schemaLocation", "xsi:schemaLocation", , , CompareMethod.Text))
            m_xmlDOM.Save(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "\" & "link_" & serie.Value & folio.Value & ".xml")
            '
            '   Realiza Timbrado
            '
            If folio.Value > 0 Then
                Try
                    '
                    '   Timbrado SIFEI
                    '
                    Dim SIFEIUsuario As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIUsuario")
                    Dim SIFEIContrasena As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIContrasena")
                    Dim SIFEIIdEquipo As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIIdEquipo")
                    '
                    System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType) Or DirectCast(768, System.Net.SecurityProtocolType) Or DirectCast(192, System.Net.SecurityProtocolType) Or DirectCast(48, System.Net.SecurityProtocolType)
                    '
                    '   Pruebas
                    '
                    'Dim TimbreSifeiVersion33 As New SIFEIPruebasV33.SIFEIService()
                    '
                    '   Produccion
                    '
                    Dim TimbreSifeiVersion33 As New SIFEI33.SIFEIService()

                    Call Comprimir()

                    Dim bytes() As Byte
                    bytes = TimbreSifeiVersion33.getCFDI(SIFEIUsuario, SIFEIContrasena, data, "", SIFEIIdEquipo)
                    Descomprimir(bytes)
                    Timbrado = True
                    MensageError = ""

                Catch ex As SoapException
                    Call cfdnotimbrado()
                    Timbrado = False
                    MensageError = ex.Detail.InnerText
                End Try

                If Timbrado = True Then
                    '
                    '   Obtiene el UUID
                    '
                    Dim filePath As String = Server.MapPath("~/clientes/" & Session("appkey").ToString & "/xml") & "/" & "link_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml"
                    Dim UUID As String = ""
                    UUID = GetXmlAttribute(filePath, "UUID", "tfd:TimbreFiscalDigital")
                    '
                    '   Marca el cfd como timbrado
                    '
                    Call cfdtimbrado(UUID)
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
                        GuardaPDF(GeneraPDF(Session("CFD")), Server.MapPath("~/clientes/" & Session("appkey").ToString & "/pdf") & "/" & UUID.ToString & ".pdf")
                    End If

                    Session("CFD") = 0
                    Session("PAGOCFD") = 0

                End If
            Else
                MensageError = "No se encontraron folios disponibles."
            End If

            If Timbrado = True Then
                Response.Redirect("~/portalcfd/ComplementosEmitidos.aspx")
            Else
                txtErrores.Text = MensageError.ToString
                RadWindow1.VisibleOnPageLoad = True
            End If

        End If
    End Sub

    Private Function Comprimir()
        Dim zip As ZipFile = New ZipFile(serie.Value.ToString & folio.Value.ToString & ".zip")
        zip.AddFile(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "\" & "link_" & serie.Value & folio.Value & ".xml", "")
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
            archivo = zip1.Entries(0).FileName.ToString
            e.Extract(DirectorioExtraccion, ExtractExistingFileAction.OverwriteSilently)
        Next

        Dim Path = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml/")
        If File.Exists(Path & archivo) Then
            System.IO.File.Copy(Path & archivo, Path & "link_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml")
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

    Protected Sub AgregaCFDI()

        Dim MonedaP As String
        If cmbMoneda.SelectedValue = 2 Then
            MonedaP = "USD"
        Else
            MonedaP = "MXN"
        End If

        Dim ObjData As New DataControl(1)
        For Each rows As DataRow In Session("TmpDetalleComplemento").Rows
            If CBool(rows("chkcfdid")) = True Then
                ObjData.RunSQLQuery("EXEC pComplementoDePago @cmd=3, @complementoId='" & Session("PAGOCFD").ToString & "', @cfdid='" & rows("id") & "', @metodopagoid='PPD', @saldoInsoluto='" & rows("saldo") & "', @monto='" & rows("monto") & "', @montomxn='" & rows("montomxn") & "', @uuid='" & rows("uuid") & "', @monedap='" & MonedaP.ToString & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "'")
            End If
        Next
        ObjData = Nothing

    End Sub

    Private Sub GuadarMetodoPago()
        Dim usoCFDIID = "P01"
        Dim Objdata As New DataControl(1)
        Objdata.RunSQLQuery("exec pCFD @cmd=25, @metodopagoid='PPD', @usocfdi='" & usoCFDIID.ToString & "', @serieid='24', @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Function CargaLugarExpedicionAtributos() As String
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
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        '
        Return LugarExpedicion
        ''
    End Function

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
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return Certificado

    End Function

    Private Function Leerllave() As String
        Dim llave As String = ""

        Dim conn As New SqlConnection(Session("conexion"))
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                llave = rs("archivo_llave_privada")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return llave
    End Function

    Private Function LeerClave() As String
        Dim contrasena As String = ""

        Dim conn As New SqlConnection(Session("conexion"))
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                contrasena = rs("contrasena_llave_privada")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return contrasena
    End Function

    Private Sub AsignaSerieFolio()
        '
        '   Obtiene serie y folio
        '
        Dim aprobacion As String = ""
        Dim annioaprobacion As String = ""
        Dim condicionesid As Integer = 0
        Dim NumCtaPago As String = ""
        Dim nombreaduana As String = ""

        Dim SQLUpdate As String = ""

        SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='24', @instrucciones='" & txtObservaciones.Text.ToString & "', @aduana='" & nombreaduana.ToString & "', @fecha_pedimento='', @formapagoid='" & cmbFormaPago.SelectedValue.ToString & "', @metodopagoid='PPD', @numctapago='" & NumCtaPago.ToString & "', @condicionesid='" & condicionesid.ToString & "'"

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmdF As New SqlCommand(SQLUpdate, conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                serie.Value = rs("serie").ToString
                folio.Value = rs("folio").ToString
                aprobacion = rs("aprobacion").ToString
                annioaprobacion = rs("annio_solicitud").ToString
                'tipoidF.Value = rs("tipoid")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Private Sub ConfiguraEmisor()
        '
        '   Datos del Emisor
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
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
        End Try
        '
    End Sub

    Private Sub ConfiguraReceptor()
        '
        '   Obtiene datos del receptor
        '
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("exec pCFD @cmd=12, @clienteId='" & cmbCliente.SelectedValue.ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                CrearNodoReceptor(Comprobante, rs("razonsocial"), rs("fac_rfc"), "P01")
                IndentarNodo(Comprobante, 1)
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Private Sub CrearNodoConceptos(ByVal Nodo As XmlNode)
        Dim Conceptos As XmlElement
        Dim Concepto As XmlElement

        Conceptos = CrearNodo("cfdi:Conceptos")
        IndentarNodo(Conceptos, 2)

        Concepto = CrearNodo("cfdi:Concepto")
        Concepto.SetAttribute("ClaveProdServ", "84111506")
        Concepto.SetAttribute("Cantidad", "1")
        Concepto.SetAttribute("ClaveUnidad", "ACT")
        Concepto.SetAttribute("Descripcion", "Pago")
        Concepto.SetAttribute("ValorUnitario", "0")
        Concepto.SetAttribute("Importe", "0")
        Conceptos.AppendChild(Concepto)
        IndentarNodo(Conceptos, 1)
        Concepto = Nothing
        Nodo.AppendChild(Conceptos)
    End Sub

    Private Sub CrearNodoPagos(ByVal Nodo As XmlNode)

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

        Dim Complemento As XmlElement
        Complemento = CrearNodo("cfdi:Complemento")
        IndentarNodo(Complemento, 1)

        Url = 1
        Dim Pagos As XmlElement
        Dim Pago As XmlElement
        Dim DocumentoRelacionado As XmlElement

        Pagos = CrearNodo("pago10:Pagos")
        Pagos.SetAttribute("Version", "1.0")
        IndentarNodo(Pagos, 2)

        Pago = CrearNodo("pago10:Pago")

        Dim fechaP As String = fecha.SelectedDate.Value.ToShortDateString & " " & "12:00:00"

        Dim ObjComp As New DataControl(1)
        ObjComp.RunSQLQuery("exec pComplementoDePago @cmd=11, @formapagoid='" & cmbFormaPago.SelectedValue & "', @monedaid='" & cmbMoneda.SelectedValue & "', @tipocambio='" & txtTipoCambio.Text & "', @numoperacion='" & txtNumOperacion.Text & "', @fecha_pago='" & fechaP.ToString & "', @ctaordenante='" & cmbCtaOrdenante.SelectedItem.Text & "', @rfcemisorctaord='" & txtRFCCtaOrdenante.Text & "', @ctabeneficiaria='" & cmbCtaBeneficiario.SelectedItem.Text & "', @rfcemisorctabeneficiaria='" & txtRfcBeneficiario.Text & "', @nomBancoOrdext='" & txtBancoExtr.Text & "', @clienteid='" & cmbCliente.SelectedValue.ToString & "', @pagoid='" & Session("PAGOCFD").ToString & "'")
        ObjComp = Nothing

        calFecha.SelectedDate = fechaP

        Pago.SetAttribute("FechaPago", Format(calFecha.SelectedDate, "yyyy-MM-ddTHH:mm:ss"))

        Dim MonedaP As String = ""

        If cmbMoneda.SelectedValue = 1 Then
            MonedaP = "MXN"
        ElseIf cmbMoneda.SelectedValue = 2 Then
            MonedaP = "USD"
        End If

        Pago.SetAttribute("FormaDePagoP", cmbFormaPago.SelectedValue)
        Pago.SetAttribute("MonedaP", MonedaP.ToString)
        Pago.SetAttribute("Monto", Format(total, "#0.00"))

        If cmbMoneda.SelectedValue > 1 Then
            Dim tipocambio As Decimal = 0
            Try
                tipocambio = Convert.ToDecimal(txtTipoCambio.Text)
            Catch ex As Exception
                tipocambio = 0
            End Try
            Pago.SetAttribute("TipoCambioP", Format(tipocambio, "#0.000000"))
        End If

        If panelRecepcionPago.Visible = True Then
            If txtNumOperacion.Text.ToString.Length > 0 Then
                Pago.SetAttribute("NumOperacion", txtNumOperacion.Text)
            End If

            If cmbCtaOrdenante.SelectedValue > 0 Then
                Pago.SetAttribute("CtaOrdenante", cmbCtaOrdenante.SelectedItem.Text)
                Pago.SetAttribute("RfcEmisorCtaOrd", txtRFCCtaOrdenante.Text)
            End If

            If cmbCtaBeneficiario.SelectedValue > 0 Then
                Pago.SetAttribute("CtaBeneficiario", cmbCtaBeneficiario.SelectedItem.Text)
                Pago.SetAttribute("RfcEmisorCtaBen", txtRfcBeneficiario.Text)
            End If

            If txtBancoExtr.Text.Length > 1 Then
                Pago.SetAttribute("NomBancoOrdExt", txtBancoExtr.Text)
            End If
        End If

        Dim CertPago As String
        Dim CadPago As String
        Dim SelloPago As String
        Dim tipocadpago As String

        If cmbTipoPago.SelectedValue > 0 Then
            Pago.SetAttribute("TipoCadPago", "01")
            tipocadpago = "01"
            Pago.SetAttribute("CertPago", txtCertPago.Text)
            CertPago = txtCertPago.Text
            Pago.SetAttribute("CadPago", txtCadPago.Text)
            CadPago = txtCadPago.Text
            Pago.SetAttribute("SelloPago", txtSelloPago.Text)
            SelloPago = txtSelloPago.Text

            Dim Objdata As New DataControl(1)
            Objdata.RunSQLQuery("exec pComplementoDePago @cmd=6, @tipocadpago='" & tipocadpago.ToString & "', @certpago='" & CertPago.ToString & "', @cadpago='" & CadPago.ToString & "', @sellopago='" & SelloPago.ToString & "', @pagoid='" & Session("PAGOCFD").ToString & "'")
            Objdata = Nothing

        End If

        IndentarNodo(Pago, 2)

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("exec pComplementoDePago @cmd=4, @pagoid='" & Session("PAGOCFD").ToString & "'", conn)
        Try
            conn.Open()
            '
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            '
            Dim Monedacfdid As String = ""
            Dim saldoAnterior As Decimal = 0
            Dim importePagado As Decimal = 0
            Dim Saldoinsoluto As Decimal = 0
            While rs.Read
                DocumentoRelacionado = CrearNodo("pago10:DoctoRelacionado")
                DocumentoRelacionado.SetAttribute("IdDocumento", rs("uuid"))
                DocumentoRelacionado.SetAttribute("MonedaDR", rs("monedacfdi"))

                saldoAnterior = rs("saldoAnterior")
                importePagado = rs("importePagado")
                Saldoinsoluto = rs("Saldoinsoluto")

                If rs("monedacfdi").ToString <> rs("moneda").ToString Then
                    If rs("monedacfdi") = "MXN" And MonedaP <> "MXN" Then

                        DocumentoRelacionado.SetAttribute("TipoCambioDR", "1")

                        If rs("monedacfdi") = "MXN" And MonedaP = "USD" Then
                            Dim tipocambio As Decimal = 0
                            Try
                                tipocambio = 1 / Convert.ToDecimal(txtTipoCambio.Text)
                            Catch ex As Exception
                                tipocambio = 0
                            End Try

                            importePagado = rs("importeMxn") * tipocambio
                            Saldoinsoluto = rs("saldoAnterior") - importePagado

                            Dim DataControl As New DataControl(1)
                            DataControl.RunSQLQuery("exec pComplementoDePago @cmd=12, @importePagado='" & importePagado.ToString & "', @id='" & rs("id").ToString & "'")
                            DataControl.RunSQLQuery("exec pComplementoDePago @cmd=13, @tipocambio='" & tipocambio.ToString & "', @id='" & rs("id").ToString & "'")
                            DataControl = Nothing
                        End If
                    ElseIf rs("monedacfdi") = "USD" And MonedaP = "MXN" Then

                        Dim tipocambio As Decimal = 0
                        Try
                            'tipocambio = 1 / Convert.ToDecimal(txtTipoCambio.Text)
                            tipocambio = Convert.ToDecimal(txtTipoCambio.Text)
                        Catch ex As Exception
                            tipocambio = 0
                        End Try

                        DocumentoRelacionado.SetAttribute("TipoCambioDR", Format(tipocambio, "#0.000000"))

                        Dim DataControl As New DataControl(1)
                        DataControl.RunSQLQuery("exec pComplementoDePago @cmd=13, @tipocambio='" & tipocambio.ToString & "', @id='" & rs("id").ToString & "'")
                        'DataControl.RunSQLQuery("exec pComplementoDePago @cmd=14, @tipocambio='0.000000', @id='" & Session("PAGOCFD").ToString & "'")
                        DataControl = Nothing

                    Else
                        If rs("tipocambio") > 0 Then

                            DocumentoRelacionado.SetAttribute("TipoCambioDR", Format(rs("tipocambio"), "#0.000000"))

                            Dim DataControl As New DataControl(1)
                            DataControl.RunSQLQuery("exec pComplementoDePago @cmd=13, @tipocambio='" & rs("tipocambio").ToString & "', @id='" & rs("id").ToString & "'")
                            DataControl = Nothing
                        End If
                    End If
                End If

                DocumentoRelacionado.SetAttribute("MetodoDePagoDR", rs("metodopagoid"))
                DocumentoRelacionado.SetAttribute("NumParcialidad", rs("parcialidad"))
                DocumentoRelacionado.SetAttribute("ImpSaldoAnt", Format(saldoAnterior, "#0.00"))
                DocumentoRelacionado.SetAttribute("ImpPagado", Format(importePagado, "#0.00"))
                DocumentoRelacionado.SetAttribute("ImpSaldoInsoluto", Format(Saldoinsoluto, "#0.00"))
                Pago.AppendChild(DocumentoRelacionado)
                IndentarNodo(Pago, 3)
                DocumentoRelacionado = Nothing
            End While
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        Pagos.AppendChild(Pago)
        IndentarNodo(Pagos, 1)
        Complemento.AppendChild(Pagos)
        IndentarNodo(Complemento, 1)
        Nodo.AppendChild(Complemento)
        Url = 0
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub SellarCFD(ByVal NodoComprobante As XmlElement)
        Dim Certificado As String = ""
        Certificado = LeerCertificado()

        Dim Clave As String = ""
        Clave = LeerClave()

        Dim bRawData As Byte() = ReadFile(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/certificados/") & Certificado)
        Dim objCert As New X509Certificate2()
        objCert.Import(bRawData)

        NodoComprobante.SetAttribute("NoCertificado", FormatearSerieCert(objCert.SerialNumber))
        NodoComprobante.SetAttribute("Total", "0")
        NodoComprobante.SetAttribute("Certificado", Convert.ToBase64String(bRawData))
        NodoComprobante.SetAttribute("Sello", GenerarSello(Clave))
    End Sub

    Private Function GenerarSello(ByVal Clave As String) As String
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
            ObjData.RunSQLQuery("update tblCFD set sello='" & GenerarSello.ToString & "', cadenaoriginal='" & GetCadenaOriginal(m_xmlDOM.InnerXml).ToString & "' where id='" & Session("CFD").ToString & "'")
            ObjData = Nothing

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetCadenaOriginal(ByVal xmlCFD As String) As String
        Dim cadena As String = ""
        Try
            Dim xslt As New XslCompiledTransform
            Dim xmldoc As New XmlDocument
            Dim navigator As XPath.XPathNavigator
            Dim output As New StringWriter
            xmldoc.LoadXml(xmlCFD)
            navigator = xmldoc.CreateNavigator()
            xslt.Load(Server.MapPath("~/portalcfd/SAT/cadenaoriginal_3_3.xslt"))
            'xslt.Load("http://www.sat.gob.mx/sitio_internet/cfd/3/cadenaoriginal_3_3/cadenaoriginal_3_3.xslt")
            xslt.Transform(navigator, Nothing, output)
            cadena = output.ToString
        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.End()
        End Try

        Return cadena
        'CadenaF.Value = cadena

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
        Dim Objdata As New DataControl(1)
        Objdata.RunSQLQuery("exec pCFD @cmd=23, @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
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

    Private Sub cfdtimbrado(ByVal uuid As String)
        Dim Objdata As New DataControl(1)
        Objdata.RunSQLQuery("exec pCFD @cmd=24, @uuid='" & uuid.ToString & "', @cfdid='" & Session("CFD").ToString & "', @pagoid='" & Session("PAGOCFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        Dim valido As Boolean = False
        valido = ValidarCFDI()

        If valido = True Then
            CargarSaldoPendiente()
            RadWindow1.VisibleOnPageLoad = False
        End If
    End Sub

    Private Sub cmbMoneda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMoneda.SelectedIndexChanged
        If cmbMoneda.SelectedValue >= 2 Then
            txtTipoCambio.Text = 0
            txtTipoCambio.Enabled = True
            txtTipoCambio.Focus()
        Else
            'txtTipoCambio.Enabled = False
        End If
    End Sub

    Private Sub cmbCtaOrdenante_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCtaOrdenante.SelectedIndexChanged
        If cmbCtaOrdenante.SelectedValue > 0 Then
            Dim ds As New DataSet
            Dim ObjData As New DataControl(1)
            ds = ObjData.FillDataSet("EXEC pCatalogoCuentas @cmd=2, @id='" & cmbCtaOrdenante.SelectedValue & "'")
            ObjData = Nothing

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    txtRFCCtaOrdenante.Text = CStr(row("rfc"))
                Next
            End If

        Else
            txtRFCCtaOrdenante.Text = ""
        End If
    End Sub

    Private Sub cmbCtaBeneficiario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCtaBeneficiario.SelectedIndexChanged
        If cmbCtaBeneficiario.SelectedValue > 0 Then
            Dim ds As New DataSet
            Dim ObjData As New DataControl(1)
            ds = ObjData.FillDataSet("EXEC pCatalogoCuentasBeneficiario @cmd=2, @id='" & cmbCtaBeneficiario.SelectedValue & "'")
            ObjData = Nothing

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    txtRfcBeneficiario.Text = CStr(row("rfc"))
                Next
            End If

        Else
            txtRfcBeneficiario.Text = ""
        End If
    End Sub

#Region "Nodos para Crear XML"

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

    Private Function CrearDOM() As XmlDocument
        Dim oDOM As New XmlDocument
        Dim Nodo As XmlNode
        Nodo = oDOM.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
        oDOM.AppendChild(Nodo)
        Nodo = Nothing
        CrearDOM = oDOM
    End Function

    Private Function CrearNodo(ByVal Nombre As String) As XmlNode
        If Url = 0 Then
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, Nombre, URI_SAT)
        ElseIf Url = 1 Then
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, Nombre, "http://www.sat.gob.mx/Pagos")
        End If
    End Function

    Private Sub IndentarNodo(ByVal Nodo As XmlNode, ByVal Nivel As Long)
        Nodo.AppendChild(m_xmlDOM.CreateTextNode(vbNewLine & New String(ControlChars.Tab, Nivel)))
    End Sub

    Private Sub CrearNodoEmisor(ByVal Nodo As XmlNode, ByVal nombre As String, ByVal rfc As String, ByVal Regimen As String)
        Dim Emisor As XmlElement
        Emisor = CrearNodo("cfdi:Emisor")
        Emisor.SetAttribute("Nombre", nombre)
        Emisor.SetAttribute("Rfc", rfc)
        Emisor.SetAttribute("RegimenFiscal", Regimen)
        Nodo.AppendChild(Emisor)
    End Sub

    Private Sub CrearNodoReceptor(ByVal Nodo As XmlNode, ByVal nombre As String, ByVal rfc As String, ByVal UsoCFDI As String)
        Dim Receptor As XmlElement
        Receptor = CrearNodo("cfdi:Receptor")
        Receptor.SetAttribute("Rfc", rfc)
        Receptor.SetAttribute("Nombre", nombre)
        Receptor.SetAttribute("UsoCFDI", UsoCFDI)
        Nodo.AppendChild(Receptor)
    End Sub

    Private Function CrearNodoComprobante() As XmlNode
        Dim Comprobante As XmlNode
        Comprobante = m_xmlDOM.CreateElement("cfdi:Comprobante", URI_SAT)
        CrearAtributosComprobante(Comprobante)
        CrearNodoComprobante = Comprobante
    End Function

    Private Sub CrearAtributosComprobante(ByVal Nodo As XmlElement)
        Nodo.SetAttribute("xmlns:cfdi", URI_SAT)
        Nodo.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        Nodo.SetAttribute("xmlns:pago10", "http://www.sat.gob.mx/Pagos")
        Nodo.SetAttribute("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/Pagos http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd")
        Nodo.SetAttribute("Version", "3.3")
        If serie.Value <> "" Then
            Nodo.SetAttribute("Serie", serie.Value)
        End If
        Nodo.SetAttribute("Folio", folio.Value)
        Nodo.SetAttribute("Fecha", Format(Now(), "yyyy-MM-ddThh:mm:ss"))
        Nodo.SetAttribute("Sello", "")
        Nodo.SetAttribute("NoCertificado", "")
        Nodo.SetAttribute("Certificado", "")
        Nodo.SetAttribute("SubTotal", "0")
        Nodo.SetAttribute("Moneda", "XXX")
        Nodo.SetAttribute("Total", "0")
        Nodo.SetAttribute("TipoDeComprobante", "P")
        Nodo.SetAttribute("LugarExpedicion", CargaLugarExpedicionAtributos())
        'Nodo.SetAttribute("Confirmacion", "12345")
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

#End Region

#Region "Manejo de PDF"

    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub

    Private Function GeneraPDF(ByVal cfdid As Long) As Telerik.Reporting.Report
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
        Dim serie As String = ""
        Dim folio As Integer = 0
        Dim uuid As String = ""

        'Información Cliente-Proveedor ********
        Dim nombrebancoctaord As String = ""
        Dim nombrebancobeneficiario As String = ""
        Dim rfcemisorctaord As String = ""
        Dim ctaordenante As String = ""
        Dim rfcemisorctabeneficiario As String = ""
        Dim ctabeneficiario As String = ""
        Dim nomBancoOrdExt As String = ""

        'Información del Depósito ********
        Dim fechaPago As String = ""
        Dim moneda As String = ""
        Dim tipocambio As String = ""
        Dim monto As Decimal = 0
        Dim numoperacion As String = ""

        'SPEI-Digital ********
        Dim tipoCadPago As String = ""
        Dim certPago As String = ""
        Dim cadPago As String = ""
        Dim selloPago As String = ""

        Dim usoCFDI As String = ""
        Dim LugarExpedicion As String = ""
        Dim TipoComprobante As String = ""
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
                importe = rs("importe")
                importetasacero = rs("importetasacero")
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
                monto = rs("monto")
                fechaPago = rs("fechapago")
                tipocambio = rs("tipocambio")
                moneda = rs("moneda")
                tipopago = rs("tipopago")
                formapago = rs("formapago")
                numctapago = rs("numctapago")
                '
                nombrebancoctaord = rs("nomBancoctaord")
                nombrebancobeneficiario = rs("nomBancobeneficiario")
                rfcemisorctaord = rs("rfcemisorctaord")
                ctaordenante = rs("ctaordenante")
                rfcemisorctabeneficiario = rs("rfcemisorctabeneficiaria")
                ctabeneficiario = rs("ctabeneficiaria")
                nomBancoOrdExt = rs("nomBancoOrdext")
                numoperacion = rs("numoperacion")
                '
                tipoCadPago = rs("tipocadpago")
                certPago = rs("certpago")
                cadPago = rs("cadpago")
                selloPago = rs("sellopago")
                usoCFDI = rs("usocfdi")
                logo_formato = rs("logo_formato")
                uuid = rs("uuid")
            End If
            rs.Close()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        Dim largo = Len(CStr(Format(CDbl(total), "#,###.00")))
        Dim decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)

        LugarExpedicion = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "LugarExpedicion", "cfdi:Comprobante")
        TipoComprobante = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "TipoDeComprobante", "cfdi:Comprobante")

        If TipoComprobante.ToString <> "" Then
            Dim ObjData As New DataControl(1)
            TipoComprobante = ObjData.RunSQLScalarQueryString("select top 1 codigo + ' ' + isnull(descripcion,'') from tblTipoDeComprobante where codigo='" & TipoComprobante.ToString & "'")
            ObjData = Nothing
        End If

        Dim reporte As New FormatosPDF.formato_complemento33iu()
        reporte.ReportParameters("txtDocumento").Value = "Pago No. " & serie.ToString & folio.ToString
        reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "Fecha", "cfdi:Comprobante")
        reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
        reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "Nombre", "cfdi:Receptor")
        reporte.ReportParameters("txtClienteCalleNum").Value = callenum
        reporte.ReportParameters("txtClienteColonia").Value = colonia
        reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
        reporte.ReportParameters("txtClienteRFC").Value = "R.F.C. " & GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "Rfc", "cfdi:Receptor")        '
        reporte.ReportParameters("txtInstrucciones").Value = instrucciones
        reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "UUID", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "NoCertificado", "cfdi:Comprobante")
        reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "Sello", "cfdi:Comprobante")
        reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "SelloSAT", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe, 2).ToString
        reporte.ReportParameters("txtTotal").Value = FormatCurrency(total, 2).ToString
        reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento(uuid)
        reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/logos/" & logo_formato.ToString & "")
        If Not File.Exists(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/cbb/") & uuid & ".png") Then
            generacbb(uuid)
        End If
        reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/cbb/" & uuid.ToString & ".png")
        reporte.ReportParameters("cfdiId").Value = cfdid
        reporte.ReportParameters("cnn").Value = Session("conexion").ToString
        reporte.ReportParameters("plantillaId").Value = plantillaid
        reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
        reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
        reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
        reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString
        If numctapago.Length > 0 Then
            reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
        End If
        reporte.ReportParameters("txtUsoCFDI").Value = usoCFDI.ToString
        '
        '   Complemento Pago
        '
        reporte.ReportParameters("txtCtaordenante").Value = ctaordenante
        reporte.ReportParameters("txtRfcemisorctabeneficiario").Value = rfcemisorctabeneficiario
        reporte.ReportParameters("txtCtabeneficiario").Value = ctabeneficiario
        reporte.ReportParameters("txtRfcemisorctaord").Value = rfcemisorctaord
        reporte.ReportParameters("txtNomBancoOrdExt").Value = nomBancoOrdExt
        reporte.ReportParameters("txtFechaPago").Value = fechaPago
        reporte.ReportParameters("txtMonto").Value = FormatCurrency(monto, 2).ToString
        reporte.ReportParameters("txtTipoCambio").Value = FormatCurrency(tipocambio, 6).ToString
        reporte.ReportParameters("txtNumoperacion").Value = numoperacion
        reporte.ReportParameters("txtTipoCadPago").Value = tipoCadPago
        reporte.ReportParameters("txtCertPago").Value = certPago
        reporte.ReportParameters("txtCadPago").Value = cadPago
        reporte.ReportParameters("txtSelloPago").Value = selloPago
        reporte.ReportParameters("txtTipoComprobante").Value = TipoComprobante.ToString
        reporte.ReportParameters("txtPACCertifico").Value = "PAC Certificó: " & GetXmlAttribute(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/xml") & "/" & uuid.ToString & ".xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtMoneda").Value = moneda
        reporte.ReportParameters("txtNomBancoOrd").Value = nombrebancoctaord
        reporte.ReportParameters("txtNomBancoBen").Value = nombrebancobeneficiario

        Return reporte

    End Function

#End Region

End Class