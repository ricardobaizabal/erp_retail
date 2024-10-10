Partial Class OrdenCompra

    'NOTE: The following procedure is required by the telerik Reporting Designer
    'It can be modified using the telerik Reporting Designer.  
    'Do not modify it using the code editor.
    Private Sub InitializeComponent()
        Dim ReportParameter1 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter2 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter3 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter4 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter5 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter6 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter7 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter8 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter9 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter10 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter11 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter12 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter13 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter14 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter15 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter16 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter17 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter18 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Me.pageHeaderSection1 = New Telerik.Reporting.PageHeaderSection()
        Me.imgBanner = New Telerik.Reporting.PictureBox()
        Me.panelTipo = New Telerik.Reporting.Panel()
        Me.TextBox37 = New Telerik.Reporting.TextBox()
        Me.TextBox38 = New Telerik.Reporting.TextBox()
        Me.lblFechaOrdenCompra = New Telerik.Reporting.TextBox()
        Me.TextBox7 = New Telerik.Reporting.TextBox()
        Me.lblTitle = New Telerik.Reporting.TextBox()
        Me.TextBox2 = New Telerik.Reporting.TextBox()
        Me.boxTelefono = New Telerik.Reporting.TextBox()
        Me.boxContacto = New Telerik.Reporting.TextBox()
        Me.lblDatosProveedor = New Telerik.Reporting.TextBox()
        Me.boxRFC = New Telerik.Reporting.TextBox()
        Me.boxCiudadEstado = New Telerik.Reporting.TextBox()
        Me.TextBox8 = New Telerik.Reporting.TextBox()
        Me.boxCalleNum = New Telerik.Reporting.TextBox()
        Me.boxRazonSocial = New Telerik.Reporting.TextBox()
        Me.detail = New Telerik.Reporting.DetailSection()
        Me.TextBox16 = New Telerik.Reporting.TextBox()
        Me.unidadDataTextBox = New Telerik.Reporting.TextBox()
        Me.cantidadDataTextBox = New Telerik.Reporting.TextBox()
        Me.costoDataTextBox = New Telerik.Reporting.TextBox()
        Me.descripcionDataTextBox = New Telerik.Reporting.TextBox()
        Me.Shape1 = New Telerik.Reporting.Shape()
        Me.TextBox3 = New Telerik.Reporting.TextBox()
        Me.pageFooterSection1 = New Telerik.Reporting.PageFooterSection()
        Me.panelTotales = New Telerik.Reporting.Panel()
        Me.lblTotales = New Telerik.Reporting.TextBox()
        Me.boxTotal = New Telerik.Reporting.TextBox()
        Me.boxTotalLbl = New Telerik.Reporting.TextBox()
        Me.panelEspeciales = New Telerik.Reporting.Panel()
        Me.lblComentarios = New Telerik.Reporting.TextBox()
        Me.txtComentarios = New Telerik.Reporting.TextBox()
        Me.boxAtentamente = New Telerik.Reporting.TextBox()
        Me.boxUsuario = New Telerik.Reporting.TextBox()
        Me.boxCliente = New Telerik.Reporting.TextBox()
        Me.Shape2 = New Telerik.Reporting.Shape()
        Me.ReportHeaderSection1 = New Telerik.Reporting.ReportHeaderSection()
        Me.headerCant = New Telerik.Reporting.TextBox()
        Me.headerUnidad = New Telerik.Reporting.TextBox()
        Me.headerDesc = New Telerik.Reporting.TextBox()
        Me.headerPU = New Telerik.Reporting.TextBox()
        Me.headerImporte = New Telerik.Reporting.TextBox()
        Me.TextBox1 = New Telerik.Reporting.TextBox()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'pageHeaderSection1
        '
        Me.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(3.221R)
        Me.pageHeaderSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.imgBanner, Me.panelTipo, Me.lblTitle, Me.TextBox2, Me.boxTelefono, Me.boxContacto, Me.lblDatosProveedor, Me.boxRFC, Me.boxCiudadEstado, Me.TextBox8, Me.boxCalleNum, Me.boxRazonSocial})
        Me.pageHeaderSection1.Name = "pageHeaderSection1"
        '
        'imgBanner
        '
        Me.imgBanner.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.imgBanner.MimeType = ""
        Me.imgBanner.Name = "imgBanner"
        Me.imgBanner.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.043R), Telerik.Reporting.Drawing.Unit.Inch(1.43R))
        Me.imgBanner.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch
        Me.imgBanner.Value = "=Parameters.paramImgBanner.Value"
        '
        'panelTipo
        '
        Me.panelTipo.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.TextBox37, Me.TextBox38, Me.lblFechaOrdenCompra, Me.TextBox7})
        Me.panelTipo.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.043R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.panelTipo.Name = "panelTipo"
        Me.panelTipo.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.67R), Telerik.Reporting.Drawing.Unit.Inch(1.43R))
        Me.panelTipo.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.panelTipo.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5R)
        '
        'TextBox37
        '
        Me.TextBox37.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.132R), Telerik.Reporting.Drawing.Unit.Inch(0.315R))
        Me.TextBox37.Name = "TextBox37"
        Me.TextBox37.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.898R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox37.Style.Font.Bold = True
        Me.TextBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.TextBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox37.Value = "Número de orden"
        '
        'TextBox38
        '
        Me.TextBox38.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.03R), Telerik.Reporting.Drawing.Unit.Inch(0.315R))
        Me.TextBox38.Name = "TextBox2"
        Me.TextBox38.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.638R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox38.Style.Font.Bold = False
        Me.TextBox38.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.TextBox38.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.TextBox38.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox38.Value = "= Parameters.txtNumeroOrdenCompra.Value"
        '
        'lblFechaOrdenCompra
        '
        Me.lblFechaOrdenCompra.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.132R), Telerik.Reporting.Drawing.Unit.Inch(0.118R))
        Me.lblFechaOrdenCompra.Name = "lblFechaOrdenCompra"
        Me.lblFechaOrdenCompra.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.898R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.lblFechaOrdenCompra.Style.Font.Bold = True
        Me.lblFechaOrdenCompra.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.lblFechaOrdenCompra.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.lblFechaOrdenCompra.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblFechaOrdenCompra.Value = "Fecha de emisión"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.03R), Telerik.Reporting.Drawing.Unit.Inch(0.118R))
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.64R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox7.Style.Font.Bold = False
        Me.TextBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.TextBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.TextBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox7.Value = "= Parameters.txtFechaEmision.Value"
        '
        'lblTitle
        '
        Me.lblTitle.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(1.654R))
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.713R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.lblTitle.Style.BackgroundColor = System.Drawing.Color.Empty
        Me.lblTitle.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None
        Me.lblTitle.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5R)
        Me.lblTitle.Style.Color = System.Drawing.Color.Black
        Me.lblTitle.Style.Font.Bold = True
        Me.lblTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10.0R)
        Me.lblTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.lblTitle.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblTitle.Value = "ORDEN DE COMPRA"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(3.042R))
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.819R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox2.Style.Font.Bold = False
        Me.TextBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.TextBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox2.Value = "= Parameters.txtEmailContacto.Value"
        '
        'boxTelefono
        '
        Me.boxTelefono.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(2.911R))
        Me.boxTelefono.Name = "boxTelefono"
        Me.boxTelefono.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.819R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxTelefono.Style.Font.Bold = False
        Me.boxTelefono.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.boxTelefono.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxTelefono.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxTelefono.Value = "= Parameters.txtTelefonoContacto.Value"
        '
        'boxContacto
        '
        Me.boxContacto.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(2.781R))
        Me.boxContacto.Name = "boxContacto"
        Me.boxContacto.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.819R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxContacto.Style.Font.Bold = False
        Me.boxContacto.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.boxContacto.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxContacto.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxContacto.Value = "= Parameters.txtContacto.Value"
        '
        'lblDatosProveedor
        '
        Me.lblDatosProveedor.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(1.951R))
        Me.lblDatosProveedor.Name = "lblDatosProveedor"
        Me.lblDatosProveedor.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.96R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.lblDatosProveedor.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblDatosProveedor.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None
        Me.lblDatosProveedor.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5R)
        Me.lblDatosProveedor.Style.Color = System.Drawing.Color.White
        Me.lblDatosProveedor.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.0R)
        Me.lblDatosProveedor.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.lblDatosProveedor.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblDatosProveedor.Value = "PROVEEDOR"
        '
        'boxRFC
        '
        Me.boxRFC.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(2.261R))
        Me.boxRFC.Name = "boxRFC"
        Me.boxRFC.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.819R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxRFC.Style.Font.Bold = True
        Me.boxRFC.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.boxRFC.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxRFC.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxRFC.Value = "= Parameters.txtProveedorRFC.Value"
        '
        'boxCiudadEstado
        '
        Me.boxCiudadEstado.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(2.651R))
        Me.boxCiudadEstado.Name = "boxCiudadEstado"
        Me.boxCiudadEstado.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.819R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxCiudadEstado.Style.Font.Bold = False
        Me.boxCiudadEstado.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.boxCiudadEstado.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxCiudadEstado.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxCiudadEstado.Value = "= Parameters.txtProveedorCiudadEstado.Value"
        '
        'TextBox8
        '
        Me.TextBox8.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(2.521R))
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.819R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox8.Style.Font.Bold = False
        Me.TextBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.TextBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox8.Value = "= Parameters.txtProveedorColonia.Value"
        '
        'boxCalleNum
        '
        Me.boxCalleNum.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(2.391R))
        Me.boxCalleNum.Name = "boxCalleNum"
        Me.boxCalleNum.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.819R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxCalleNum.Style.Font.Bold = False
        Me.boxCalleNum.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.boxCalleNum.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxCalleNum.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxCalleNum.Value = "= Parameters.txtProveedorCalleNum.Value"
        '
        'boxRazonSocial
        '
        Me.boxRazonSocial.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(2.131R))
        Me.boxRazonSocial.Name = "boxRazonSocial"
        Me.boxRazonSocial.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.819R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxRazonSocial.Style.Font.Bold = True
        Me.boxRazonSocial.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.boxRazonSocial.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxRazonSocial.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxRazonSocial.Value = "= Parameters.txtProveedorRazonSocial.Value"
        '
        'detail
        '
        Me.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.725R)
        Me.detail.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.TextBox16, Me.unidadDataTextBox, Me.cantidadDataTextBox, Me.costoDataTextBox, Me.descripcionDataTextBox, Me.Shape1, Me.TextBox3})
        Me.detail.Name = "detail"
        Me.detail.Style.BackgroundColor = System.Drawing.Color.Empty
        '
        'TextBox16
        '
        Me.TextBox16.Culture = New System.Globalization.CultureInfo("es-MX")
        Me.TextBox16.Format = "{0:C2}"
        Me.TextBox16.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.83R), Telerik.Reporting.Drawing.Unit.Inch(0.064R))
        Me.TextBox16.Name = "TextBox16"
        Me.TextBox16.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.87R), Telerik.Reporting.Drawing.Unit.Inch(0.157R))
        Me.TextBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.TextBox16.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Cm(0R)
        Me.TextBox16.Style.Font.Name = "Arial"
        Me.TextBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.TextBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.TextBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.TextBox16.Value = "=fields.total"
        '
        'unidadDataTextBox
        '
        Me.unidadDataTextBox.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.22R), Telerik.Reporting.Drawing.Unit.Inch(0.064R))
        Me.unidadDataTextBox.Name = "unidadDataTextBox"
        Me.unidadDataTextBox.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.689R), Telerik.Reporting.Drawing.Unit.Inch(0.157R))
        Me.unidadDataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.unidadDataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Cm(0R)
        Me.unidadDataTextBox.Style.Font.Name = "Arial"
        Me.unidadDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.unidadDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.unidadDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.unidadDataTextBox.StyleName = "Data"
        Me.unidadDataTextBox.Value = "=unidad"
        '
        'cantidadDataTextBox
        '
        Me.cantidadDataTextBox.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.61R), Telerik.Reporting.Drawing.Unit.Inch(0.064R))
        Me.cantidadDataTextBox.Name = "cantidadDataTextBox"
        Me.cantidadDataTextBox.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.61R), Telerik.Reporting.Drawing.Unit.Inch(0.157R))
        Me.cantidadDataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.cantidadDataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Cm(0R)
        Me.cantidadDataTextBox.Style.Font.Name = "Arial"
        Me.cantidadDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.cantidadDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.cantidadDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.cantidadDataTextBox.StyleName = "Data"
        Me.cantidadDataTextBox.Value = "=cantidad"
        '
        'costoDataTextBox
        '
        Me.costoDataTextBox.Culture = New System.Globalization.CultureInfo("es-MX")
        Me.costoDataTextBox.Format = "{0:C2}"
        Me.costoDataTextBox.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.89R), Telerik.Reporting.Drawing.Unit.Inch(0.064R))
        Me.costoDataTextBox.Name = "costoDataTextBox"
        Me.costoDataTextBox.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.94R), Telerik.Reporting.Drawing.Unit.Inch(0.157R))
        Me.costoDataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.costoDataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Cm(0R)
        Me.costoDataTextBox.Style.Font.Name = "Arial"
        Me.costoDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.costoDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.costoDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.costoDataTextBox.StyleName = "Data"
        Me.costoDataTextBox.Value = "=fields.costo_estandar"
        '
        'descripcionDataTextBox
        '
        Me.descripcionDataTextBox.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.909R), Telerik.Reporting.Drawing.Unit.Inch(0.064R))
        Me.descripcionDataTextBox.Name = "descripcionDataTextBox"
        Me.descripcionDataTextBox.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.981R), Telerik.Reporting.Drawing.Unit.Inch(0.157R))
        Me.descripcionDataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.descripcionDataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Cm(0R)
        Me.descripcionDataTextBox.Style.Font.Name = "Arial"
        Me.descripcionDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.descripcionDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.descripcionDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.descripcionDataTextBox.StyleName = "Data"
        Me.descripcionDataTextBox.Value = "=descripcion"
        '
        'Shape1
        '
        Me.Shape1.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0R), Telerik.Reporting.Drawing.Unit.Cm(0.562R))
        Me.Shape1.Name = "Shape1"
        Me.Shape1.ShapeType = New Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW)
        Me.Shape1.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(19.584R), Telerik.Reporting.Drawing.Unit.Cm(0.163R))
        Me.Shape1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1.0R)
        '
        'TextBox3
        '
        Me.TextBox3.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(0.064R))
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.61R), Telerik.Reporting.Drawing.Unit.Inch(0.157R))
        Me.TextBox3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.TextBox3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Cm(0R)
        Me.TextBox3.Style.Font.Name = "Arial"
        Me.TextBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.TextBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.TextBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.TextBox3.StyleName = "Data"
        Me.TextBox3.Value = "=codigo"
        '
        'pageFooterSection1
        '
        Me.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(3.509R)
        Me.pageFooterSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.panelTotales, Me.panelEspeciales, Me.boxAtentamente, Me.boxUsuario, Me.boxCliente, Me.Shape2})
        Me.pageFooterSection1.Name = "pageFooterSection1"
        '
        'panelTotales
        '
        Me.panelTotales.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.lblTotales, Me.boxTotal, Me.boxTotalLbl})
        Me.panelTotales.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7R), Telerik.Reporting.Drawing.Unit.Inch(0.537R))
        Me.panelTotales.Name = "panelTotales"
        Me.panelTotales.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.013R), Telerik.Reporting.Drawing.Unit.Inch(0.881R))
        Me.panelTotales.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.panelTotales.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5R)
        '
        'lblTotales
        '
        Me.lblTotales.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.lblTotales.Name = "lblTotales"
        Me.lblTotales.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0R), Telerik.Reporting.Drawing.Unit.Inch(0.12R))
        Me.lblTotales.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblTotales.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None
        Me.lblTotales.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5R)
        Me.lblTotales.Style.Color = System.Drawing.Color.White
        Me.lblTotales.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.0R)
        Me.lblTotales.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.lblTotales.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblTotales.Value = "IMPORTE"
        '
        'boxTotal
        '
        Me.boxTotal.Format = "{0:C2}"
        Me.boxTotal.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.912R), Telerik.Reporting.Drawing.Unit.Inch(0.369R))
        Me.boxTotal.Name = "boxTotal"
        Me.boxTotal.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.869R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxTotal.Style.Font.Bold = False
        Me.boxTotal.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.5R)
        Me.boxTotal.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.boxTotal.Value = "= Parameters.txtTotal.Value"
        '
        'boxTotalLbl
        '
        Me.boxTotalLbl.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.19R), Telerik.Reporting.Drawing.Unit.Inch(0.369R))
        Me.boxTotalLbl.Name = "boxTotalLbl"
        Me.boxTotalLbl.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.721R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxTotalLbl.Style.Font.Bold = True
        Me.boxTotalLbl.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.5R)
        Me.boxTotalLbl.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxTotalLbl.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxTotalLbl.Value = "TOTAL"
        '
        'panelEspeciales
        '
        Me.panelEspeciales.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.lblComentarios, Me.txtComentarios})
        Me.panelEspeciales.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.panelEspeciales.Name = "panelEspeciales"
        Me.panelEspeciales.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7R), Telerik.Reporting.Drawing.Unit.Inch(0.458R))
        Me.panelEspeciales.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid
        Me.panelEspeciales.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5R)
        '
        'lblComentarios
        '
        Me.lblComentarios.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.lblComentarios.Name = "lblComentarios"
        Me.lblComentarios.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.96R), Telerik.Reporting.Drawing.Unit.Inch(0.12R))
        Me.lblComentarios.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblComentarios.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None
        Me.lblComentarios.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5R)
        Me.lblComentarios.Style.Color = System.Drawing.Color.White
        Me.lblComentarios.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.0R)
        Me.lblComentarios.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.lblComentarios.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblComentarios.Value = "COMENTARIOS"
        '
        'txtComentarios
        '
        Me.txtComentarios.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.06R), Telerik.Reporting.Drawing.Unit.Inch(0.16R))
        Me.txtComentarios.Name = "txtComentarios"
        Me.txtComentarios.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.575R), Telerik.Reporting.Drawing.Unit.Inch(0.298R))
        Me.txtComentarios.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.txtComentarios.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtComentarios.Value = "= Parameters.txtComentarios.Value"
        '
        'boxAtentamente
        '
        Me.boxAtentamente.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(1.688R))
        Me.boxAtentamente.Name = "boxAtentamente"
        Me.boxAtentamente.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.713R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxAtentamente.Style.Font.Bold = True
        Me.boxAtentamente.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.boxAtentamente.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.boxAtentamente.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxAtentamente.Value = "A T E N T A M E N T E"
        '
        'boxUsuario
        '
        Me.boxUsuario.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(2.283R))
        Me.boxUsuario.Name = "boxUsuario"
        Me.boxUsuario.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.713R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxUsuario.Style.Font.Bold = True
        Me.boxUsuario.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.boxUsuario.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.boxUsuario.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxUsuario.Value = "= Parameters.txtUsuario.Value"
        '
        'boxCliente
        '
        Me.boxCliente.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(2.414R))
        Me.boxCliente.Name = "boxCliente"
        Me.boxCliente.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.713R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxCliente.Style.Font.Bold = True
        Me.boxCliente.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.boxCliente.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.boxCliente.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxCliente.Value = "= Parameters.txtRazonSocialCliente.Value"
        '
        'Shape2
        '
        Me.Shape2.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.6R), Telerik.Reporting.Drawing.Unit.Cm(5.637R))
        Me.Shape2.Name = "Shape2"
        Me.Shape2.ShapeType = New Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW)
        Me.Shape2.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.484R), Telerik.Reporting.Drawing.Unit.Cm(0.163R))
        Me.Shape2.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1.0R)
        '
        'ReportHeaderSection1
        '
        Me.ReportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.18R)
        Me.ReportHeaderSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.headerCant, Me.headerUnidad, Me.headerDesc, Me.headerPU, Me.headerImporte, Me.TextBox1})
        Me.ReportHeaderSection1.Name = "ReportHeaderSection1"
        '
        'headerCant
        '
        Me.headerCant.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.61R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.headerCant.Name = "headerCant"
        Me.headerCant.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.61R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.headerCant.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerCant.Style.Color = System.Drawing.Color.White
        Me.headerCant.Style.Font.Bold = True
        Me.headerCant.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(6.0R)
        Me.headerCant.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.headerCant.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerCant.Value = "   CANTIDAD"
        '
        'headerUnidad
        '
        Me.headerUnidad.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.22R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.headerUnidad.Name = "headerUnidad"
        Me.headerUnidad.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.689R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.headerUnidad.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerUnidad.Style.Color = System.Drawing.Color.White
        Me.headerUnidad.Style.Font.Bold = True
        Me.headerUnidad.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(6.0R)
        Me.headerUnidad.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.headerUnidad.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerUnidad.Value = "   UNIDAD"
        '
        'headerDesc
        '
        Me.headerDesc.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.909R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.headerDesc.Name = "headerDesc"
        Me.headerDesc.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.981R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.headerDesc.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerDesc.Style.Color = System.Drawing.Color.White
        Me.headerDesc.Style.Font.Bold = True
        Me.headerDesc.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(6.0R)
        Me.headerDesc.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.headerDesc.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerDesc.Value = "DESCRIPCIÓN"
        '
        'headerPU
        '
        Me.headerPU.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.89R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.headerPU.Name = "headerPU"
        Me.headerPU.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.94R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.headerPU.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerPU.Style.Color = System.Drawing.Color.White
        Me.headerPU.Style.Font.Bold = True
        Me.headerPU.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(6.0R)
        Me.headerPU.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.headerPU.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerPU.Value = "   PRECIO UNITARIO"
        '
        'headerImporte
        '
        Me.headerImporte.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.83R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.headerImporte.Name = "headerImporte"
        Me.headerImporte.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.87R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.headerImporte.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerImporte.Style.Color = System.Drawing.Color.White
        Me.headerImporte.Style.Font.Bold = True
        Me.headerImporte.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(6.0R)
        Me.headerImporte.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.headerImporte.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerImporte.Value = "   IMPORTE"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.61R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.TextBox1.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.TextBox1.Style.Color = System.Drawing.Color.White
        Me.TextBox1.Style.Font.Bold = True
        Me.TextBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(6.0R)
        Me.TextBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox1.Value = "   CÓDIGO"
        '
        'OrdenCompra
        '
        Me.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.pageHeaderSection1, Me.detail, Me.pageFooterSection1, Me.ReportHeaderSection1})
        Me.Name = "formato_cfdi"
        Me.PageSettings.Landscape = False
        Me.PageSettings.Margins = New Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(1.0R), Telerik.Reporting.Drawing.Unit.Cm(1.0R), Telerik.Reporting.Drawing.Unit.Cm(1.0R), Telerik.Reporting.Drawing.Unit.Cm(0R))
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter
        ReportParameter1.Name = "cnn"
        ReportParameter2.Name = "plantillaId"
        ReportParameter2.Type = Telerik.Reporting.ReportParameterType.[Integer]
        ReportParameter3.Name = "ordenId"
        ReportParameter4.AllowNull = True
        ReportParameter4.Name = "paramImgBanner"
        ReportParameter5.AllowNull = True
        ReportParameter5.Name = "txtFechaEmision"
        ReportParameter6.AllowNull = True
        ReportParameter6.Name = "txtNumeroOrdenCompra"
        ReportParameter7.AllowNull = True
        ReportParameter7.Name = "txtProveedorRazonSocial"
        ReportParameter8.AllowNull = True
        ReportParameter8.Name = "txtProveedorRFC"
        ReportParameter9.AllowNull = True
        ReportParameter9.Name = "txtProveedorCalleNum"
        ReportParameter10.AllowNull = True
        ReportParameter10.Name = "txtProveedorColonia"
        ReportParameter11.AllowNull = True
        ReportParameter11.Name = "txtProveedorCiudadEstado"
        ReportParameter12.AllowNull = True
        ReportParameter12.Name = "txtProveedorContacto"
        ReportParameter13.AllowNull = True
        ReportParameter13.Name = "txtProveedorTelefonoContacto"
        ReportParameter14.AllowNull = True
        ReportParameter14.Name = "txtProveedorEmailContacto"
        ReportParameter15.AllowNull = True
        ReportParameter15.Name = "txtComentarios"
        ReportParameter16.AllowNull = True
        ReportParameter16.Name = "txtTotal"
        ReportParameter17.AllowNull = True
        ReportParameter17.Name = "txtUsuario"
        ReportParameter18.AllowNull = True
        ReportParameter18.Name = "txtRazonSocialCliente"
        Me.ReportParameters.Add(ReportParameter1)
        Me.ReportParameters.Add(ReportParameter2)
        Me.ReportParameters.Add(ReportParameter3)
        Me.ReportParameters.Add(ReportParameter4)
        Me.ReportParameters.Add(ReportParameter5)
        Me.ReportParameters.Add(ReportParameter6)
        Me.ReportParameters.Add(ReportParameter7)
        Me.ReportParameters.Add(ReportParameter8)
        Me.ReportParameters.Add(ReportParameter9)
        Me.ReportParameters.Add(ReportParameter10)
        Me.ReportParameters.Add(ReportParameter11)
        Me.ReportParameters.Add(ReportParameter12)
        Me.ReportParameters.Add(ReportParameter13)
        Me.ReportParameters.Add(ReportParameter14)
        Me.ReportParameters.Add(ReportParameter15)
        Me.ReportParameters.Add(ReportParameter16)
        Me.ReportParameters.Add(ReportParameter17)
        Me.ReportParameters.Add(ReportParameter18)
        Me.Style.BackgroundColor = System.Drawing.Color.White
        Me.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.0R)
        Me.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.Width = Telerik.Reporting.Drawing.Unit.Inch(7.713R)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents pageHeaderSection1 As Telerik.Reporting.PageHeaderSection
    Friend WithEvents detail As Telerik.Reporting.DetailSection
    Friend WithEvents pageFooterSection1 As Telerik.Reporting.PageFooterSection
    Friend WithEvents imgBanner As Telerik.Reporting.PictureBox
    Friend WithEvents panelTipo As Telerik.Reporting.Panel
    Friend WithEvents ReportHeaderSection1 As Telerik.Reporting.ReportHeaderSection
    Friend WithEvents headerCant As Telerik.Reporting.TextBox
    Friend WithEvents headerUnidad As Telerik.Reporting.TextBox
    Friend WithEvents headerDesc As Telerik.Reporting.TextBox
    Friend WithEvents headerPU As Telerik.Reporting.TextBox
    Friend WithEvents headerImporte As Telerik.Reporting.TextBox
    Friend WithEvents TextBox16 As Telerik.Reporting.TextBox
    Friend WithEvents unidadDataTextBox As Telerik.Reporting.TextBox
    Friend WithEvents cantidadDataTextBox As Telerik.Reporting.TextBox
    Friend WithEvents costoDataTextBox As Telerik.Reporting.TextBox
    Friend WithEvents descripcionDataTextBox As Telerik.Reporting.TextBox
    Friend WithEvents panelTotales As Telerik.Reporting.Panel
    Friend WithEvents lblTitle As Telerik.Reporting.TextBox
    Friend WithEvents lblTotales As Telerik.Reporting.TextBox
    Friend WithEvents lblFechaOrdenCompra As Telerik.Reporting.TextBox
    Friend WithEvents TextBox7 As Telerik.Reporting.TextBox
    Friend WithEvents boxTotal As Telerik.Reporting.TextBox
    Friend WithEvents boxTotalLbl As Telerik.Reporting.TextBox
    Friend WithEvents TextBox37 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox38 As Telerik.Reporting.TextBox
    Friend WithEvents panelEspeciales As Telerik.Reporting.Panel
    Friend WithEvents lblComentarios As Telerik.Reporting.TextBox
    Friend WithEvents txtComentarios As Telerik.Reporting.TextBox
    Friend WithEvents Shape1 As Telerik.Reporting.Shape
    Friend WithEvents boxAtentamente As Telerik.Reporting.TextBox
    Friend WithEvents boxUsuario As Telerik.Reporting.TextBox
    Friend WithEvents boxCliente As Telerik.Reporting.TextBox
    Friend WithEvents TextBox3 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox1 As Telerik.Reporting.TextBox
    Friend WithEvents Shape2 As Telerik.Reporting.Shape
    Friend WithEvents TextBox2 As Telerik.Reporting.TextBox
    Friend WithEvents boxTelefono As Telerik.Reporting.TextBox
    Friend WithEvents boxContacto As Telerik.Reporting.TextBox
    Friend WithEvents lblDatosProveedor As Telerik.Reporting.TextBox
    Friend WithEvents boxRFC As Telerik.Reporting.TextBox
    Friend WithEvents boxCiudadEstado As Telerik.Reporting.TextBox
    Friend WithEvents TextBox8 As Telerik.Reporting.TextBox
    Friend WithEvents boxCalleNum As Telerik.Reporting.TextBox
    Friend WithEvents boxRazonSocial As Telerik.Reporting.TextBox
End Class