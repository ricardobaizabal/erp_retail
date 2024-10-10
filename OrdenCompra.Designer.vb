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
        Me.pageHeaderSection1.Height = New Telerik.Reporting.Drawing.Unit(3.2208335399627686R, Telerik.Reporting.Drawing.UnitType.Inch)
        Me.pageHeaderSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.imgBanner, Me.panelTipo, Me.lblTitle, Me.TextBox2, Me.boxTelefono, Me.boxContacto, Me.lblDatosProveedor, Me.boxRFC, Me.boxCiudadEstado, Me.TextBox8, Me.boxCalleNum, Me.boxRazonSocial})
        Me.pageHeaderSection1.Name = "pageHeaderSection1"
        '
        'imgBanner
        '
        Me.imgBanner.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.imgBanner.MimeType = ""
        Me.imgBanner.Name = "imgBanner"
        Me.imgBanner.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(6.0426387786865234R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.4299999475479126R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.imgBanner.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch
        Me.imgBanner.Value = "=Parameters.paramImgBanner.Value"
        '
        'panelTipo
        '
        Me.panelTipo.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.TextBox37, Me.TextBox38, Me.lblFechaOrdenCompra, Me.TextBox7})
        Me.panelTipo.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(6.042717456817627R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.panelTipo.Name = "panelTipo"
        Me.panelTipo.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.6698808670043945R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.4299998283386231R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.panelTipo.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.panelTipo.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0.5R, Telerik.Reporting.Drawing.UnitType.Point)
        '
        'TextBox37
        '
        Me.TextBox37.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.13169288635253906R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.31496062874794006R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox37.Name = "TextBox37"
        Me.TextBox37.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.89795178174972534R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox37.Style.Font.Bold = True
        Me.TextBox37.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox37.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox37.Value = "Número de orden"
        '
        'TextBox38
        '
        Me.TextBox38.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(1.0297237634658814R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.31496062874794006R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox38.Name = "TextBox2"
        Me.TextBox38.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.63763809204101562R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox38.Style.Font.Bold = False
        Me.TextBox38.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox38.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.TextBox38.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox38.Value = "= Parameters.txtNumeroOrdenCompra.Value"
        '
        'lblFechaOrdenCompra
        '
        Me.lblFechaOrdenCompra.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.13169288635253906R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.11811023205518723R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblFechaOrdenCompra.Name = "lblFechaOrdenCompra"
        Me.lblFechaOrdenCompra.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.89795207977294922R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblFechaOrdenCompra.Style.Font.Bold = True
        Me.lblFechaOrdenCompra.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblFechaOrdenCompra.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.lblFechaOrdenCompra.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblFechaOrdenCompra.Value = "Fecha de emisión"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(1.0297235250473023R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.11811026185750961R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.64015674591064453R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox7.Style.Font.Bold = False
        Me.TextBox7.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.TextBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox7.Value = "= Parameters.txtFechaEmision.Value"
        '
        'lblTitle
        '
        Me.lblTitle.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039418537198798731R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.6535433530807495R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.712559700012207R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.17992125451564789R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblTitle.Style.BackgroundColor = System.Drawing.Color.Empty
        Me.lblTitle.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None
        Me.lblTitle.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0.5R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblTitle.Style.Color = System.Drawing.Color.Black
        Me.lblTitle.Style.Font.Bold = True
        Me.lblTitle.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.lblTitle.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblTitle.Value = "ORDEN DE COMPRA"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039577484130859375R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(3.041553258895874R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(3.8188586235046387R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox2.Style.Font.Bold = False
        Me.TextBox2.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox2.Value = "= Parameters.txtEmailContacto.Value"
        '
        'boxTelefono
        '
        Me.boxTelefono.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039577484130859375R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(2.9114742279052734R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxTelefono.Name = "boxTelefono"
        Me.boxTelefono.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(3.8188586235046387R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxTelefono.Style.Font.Bold = False
        Me.boxTelefono.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.boxTelefono.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxTelefono.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxTelefono.Value = "= Parameters.txtTelefonoContacto.Value"
        '
        'boxContacto
        '
        Me.boxContacto.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039577484130859375R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(2.7813951969146729R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxContacto.Name = "boxContacto"
        Me.boxContacto.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(3.8188586235046387R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxContacto.Style.Font.Bold = False
        Me.boxContacto.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.boxContacto.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxContacto.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxContacto.Value = "= Parameters.txtContacto.Value"
        '
        'lblDatosProveedor
        '
        Me.lblDatosProveedor.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039577484130859375R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.9507023096084595R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblDatosProveedor.Name = "lblDatosProveedor"
        Me.lblDatosProveedor.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.9599604606628418R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.17996063828468323R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblDatosProveedor.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblDatosProveedor.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None
        Me.lblDatosProveedor.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0.5R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblDatosProveedor.Style.Color = System.Drawing.Color.White
        Me.lblDatosProveedor.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(8.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblDatosProveedor.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.lblDatosProveedor.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblDatosProveedor.Value = "PROVEEDOR"
        '
        'boxRFC
        '
        Me.boxRFC.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039577484130859375R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(2.2610795497894287R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxRFC.Name = "boxRFC"
        Me.boxRFC.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(3.8188586235046387R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxRFC.Style.Font.Bold = True
        Me.boxRFC.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.boxRFC.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxRFC.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxRFC.Value = "= Parameters.txtProveedorRFC.Value"
        '
        'boxCiudadEstado
        '
        Me.boxCiudadEstado.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039577484130859375R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(2.6513164043426514R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxCiudadEstado.Name = "boxCiudadEstado"
        Me.boxCiudadEstado.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(3.8188586235046387R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxCiudadEstado.Style.Font.Bold = False
        Me.boxCiudadEstado.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.boxCiudadEstado.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxCiudadEstado.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxCiudadEstado.Value = "= Parameters.txtProveedorCiudadEstado.Value"
        '
        'TextBox8
        '
        Me.TextBox8.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039577484130859375R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(2.5212373733520508R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(3.8188586235046387R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox8.Style.Font.Bold = False
        Me.TextBox8.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox8.Value = "= Parameters.txtProveedorColonia.Value"
        '
        'boxCalleNum
        '
        Me.boxCalleNum.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039577484130859375R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(2.3911585807800293R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxCalleNum.Name = "boxCalleNum"
        Me.boxCalleNum.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(3.8188586235046387R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxCalleNum.Style.Font.Bold = False
        Me.boxCalleNum.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.boxCalleNum.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxCalleNum.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxCalleNum.Value = "= Parameters.txtProveedorCalleNum.Value"
        '
        'boxRazonSocial
        '
        Me.boxRazonSocial.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039577484130859375R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(2.1307418346405029R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxRazonSocial.Name = "boxRazonSocial"
        Me.boxRazonSocial.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(3.8188583850860596R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxRazonSocial.Style.Font.Bold = True
        Me.boxRazonSocial.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.boxRazonSocial.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxRazonSocial.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxRazonSocial.Value = "= Parameters.txtProveedorRazonSocial.Value"
        '
        'detail
        '
        Me.detail.Height = New Telerik.Reporting.Drawing.Unit(0.7250816822052002R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.detail.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.TextBox16, Me.unidadDataTextBox, Me.cantidadDataTextBox, Me.costoDataTextBox, Me.descripcionDataTextBox, Me.Shape1, Me.TextBox3})
        Me.detail.Name = "detail"
        Me.detail.Style.BackgroundColor = System.Drawing.Color.Empty
        '
        'TextBox16
        '
        Me.TextBox16.Culture = New System.Globalization.CultureInfo("es-MX")
        Me.TextBox16.Format = "{0:C2}"
        Me.TextBox16.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(6.8300800323486328R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.063733421266078949R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox16.Name = "TextBox16"
        Me.TextBox16.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.86984258890151978R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.15748055279254913R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.TextBox16.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.TextBox16.Style.Font.Name = "Arial"
        Me.TextBox16.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.TextBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.TextBox16.Value = "=fields.total"
        '
        'unidadDataTextBox
        '
        Me.unidadDataTextBox.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(1.2200788259506226R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.063733421266078949R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.unidadDataTextBox.Name = "unidadDataTextBox"
        Me.unidadDataTextBox.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.68909424543380737R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.15748055279254913R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.unidadDataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.unidadDataTextBox.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.unidadDataTextBox.Style.Font.Name = "Arial"
        Me.unidadDataTextBox.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.unidadDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.unidadDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.unidadDataTextBox.StyleName = "Data"
        Me.unidadDataTextBox.Value = "=unidad"
        '
        'cantidadDataTextBox
        '
        Me.cantidadDataTextBox.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.61003941297531128R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.063733421266078949R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.cantidadDataTextBox.Name = "cantidadDataTextBox"
        Me.cantidadDataTextBox.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.6099211573600769R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.15748052299022675R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.cantidadDataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.cantidadDataTextBox.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.cantidadDataTextBox.Style.Font.Name = "Arial"
        Me.cantidadDataTextBox.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.cantidadDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.cantidadDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.cantidadDataTextBox.StyleName = "Data"
        Me.cantidadDataTextBox.Value = "=cantidad"
        '
        'costoDataTextBox
        '
        Me.costoDataTextBox.Culture = New System.Globalization.CultureInfo("es-MX")
        Me.costoDataTextBox.Format = "{0:C2}"
        Me.costoDataTextBox.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(5.8899993896484375R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.063733421266078949R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.costoDataTextBox.Name = "costoDataTextBox"
        Me.costoDataTextBox.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.94000256061553955R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.15748055279254913R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.costoDataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.costoDataTextBox.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.costoDataTextBox.Style.Font.Name = "Arial"
        Me.costoDataTextBox.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.costoDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.costoDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.costoDataTextBox.StyleName = "Data"
        Me.costoDataTextBox.Value = "=fields.costo_estandar"
        '
        'descripcionDataTextBox
        '
        Me.descripcionDataTextBox.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(1.9092518091201782R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.063733421266078949R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.descripcionDataTextBox.Name = "descripcionDataTextBox"
        Me.descripcionDataTextBox.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(3.980668306350708R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.15748055279254913R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.descripcionDataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.descripcionDataTextBox.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.descripcionDataTextBox.Style.Font.Name = "Arial"
        Me.descripcionDataTextBox.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.descripcionDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.descripcionDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.descripcionDataTextBox.StyleName = "Data"
        Me.descripcionDataTextBox.Value = "=descripcion"
        '
        'Shape1
        '
        Me.Shape1.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.561883270740509R, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.Shape1.Name = "Shape1"
        Me.Shape1.ShapeType = New Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW)
        Me.Shape1.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(19.583600997924805R, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.16319847106933594R, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.Shape1.Style.LineWidth = New Telerik.Reporting.Drawing.Unit(1.0R, Telerik.Reporting.Drawing.UnitType.Pixel)
        '
        'TextBox3
        '
        Me.TextBox3.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039577484130859375R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.063733421266078949R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.6099211573600769R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.15748052299022675R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.TextBox3.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.TextBox3.Style.Font.Name = "Arial"
        Me.TextBox3.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.TextBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.TextBox3.StyleName = "Data"
        Me.TextBox3.Value = "=codigo"
        '
        'pageFooterSection1
        '
        Me.pageFooterSection1.Height = New Telerik.Reporting.Drawing.Unit(3.5091278553009033R, Telerik.Reporting.Drawing.UnitType.Inch)
        Me.pageFooterSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.panelTotales, Me.panelEspeciales, Me.boxAtentamente, Me.boxUsuario, Me.boxCliente, Me.Shape2})
        Me.pageFooterSection1.Name = "pageFooterSection1"
        '
        'panelTotales
        '
        Me.panelTotales.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.lblTotales, Me.boxTotal, Me.boxTotalLbl})
        Me.panelTotales.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(5.6999998092651367R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.53677117824554443R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.panelTotales.Name = "panelTotales"
        Me.panelTotales.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.0125987529754639R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.88055199384689331R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.panelTotales.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.panelTotales.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0.5R, Telerik.Reporting.Drawing.UnitType.Point)
        '
        'lblTotales
        '
        Me.lblTotales.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039170186937553808R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblTotales.Name = "lblTotales"
        Me.lblTotales.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.999921441078186R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.11999999731779099R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblTotales.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblTotales.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None
        Me.lblTotales.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0.5R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblTotales.Style.Color = System.Drawing.Color.White
        Me.lblTotales.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(8.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblTotales.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.lblTotales.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblTotales.Value = "IMPORTE"
        '
        'boxTotal
        '
        Me.boxTotal.Format = "{0:C2}"
        Me.boxTotal.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.91206276416778564R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.36874118447303772R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxTotal.Name = "boxTotal"
        Me.boxTotal.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.86908817291259766R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxTotal.Style.Font.Bold = False
        Me.boxTotal.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.5R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.boxTotal.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.boxTotal.Value = "= Parameters.txtTotal.Value"
        '
        'boxTotalLbl
        '
        Me.boxTotalLbl.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.18999999761581421R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.36874118447303772R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxTotalLbl.Name = "boxTotalLbl"
        Me.boxTotalLbl.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.72083252668380737R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxTotalLbl.Style.Font.Bold = True
        Me.boxTotalLbl.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.5R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.boxTotalLbl.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxTotalLbl.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxTotalLbl.Value = "TOTAL"
        '
        'panelEspeciales
        '
        Me.panelEspeciales.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.lblComentarios, Me.txtComentarios})
        Me.panelEspeciales.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039418537198798731R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.000000034769374934739972R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.panelEspeciales.Name = "panelEspeciales"
        Me.panelEspeciales.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.69992208480835R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.45803096890449524R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.panelEspeciales.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid
        Me.panelEspeciales.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0.5R, Telerik.Reporting.Drawing.UnitType.Point)
        '
        'lblComentarios
        '
        Me.lblComentarios.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblComentarios.Name = "lblComentarios"
        Me.lblComentarios.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.9599604606628418R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.11999999731779099R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblComentarios.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblComentarios.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None
        Me.lblComentarios.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0.5R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblComentarios.Style.Color = System.Drawing.Color.White
        Me.lblComentarios.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(8.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblComentarios.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.lblComentarios.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblComentarios.Value = "COMENTARIOS"
        '
        'txtComentarios
        '
        Me.txtComentarios.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.059999678283929825R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.16000044345855713R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtComentarios.Name = "txtComentarios"
        Me.txtComentarios.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.5750408172607422R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.29803049564361572R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtComentarios.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtComentarios.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtComentarios.Value = "= Parameters.txtComentarios.Value"
        '
        'boxAtentamente
        '
        Me.boxAtentamente.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039418537198798731R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.6875R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxAtentamente.Name = "boxAtentamente"
        Me.boxAtentamente.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.7125601768493652R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxAtentamente.Style.Font.Bold = True
        Me.boxAtentamente.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.boxAtentamente.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.boxAtentamente.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxAtentamente.Value = "A T E N T A M E N T E"
        '
        'boxUsuario
        '
        Me.boxUsuario.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039418537198798731R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(2.2834651470184326R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxUsuario.Name = "boxUsuario"
        Me.boxUsuario.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.7125601768493652R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxUsuario.Style.Font.Bold = True
        Me.boxUsuario.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.boxUsuario.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.boxUsuario.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxUsuario.Value = "= Parameters.txtUsuario.Value"
        '
        'boxCliente
        '
        Me.boxCliente.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(2.4135434627532959R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxCliente.Name = "boxCliente"
        Me.boxCliente.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.7125601768493652R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.12999999523162842R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.boxCliente.Style.Font.Bold = True
        Me.boxCliente.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(7.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.boxCliente.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.boxCliente.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxCliente.Value = "= Parameters.txtRazonSocialCliente.Value"
        '
        'Shape2
        '
        Me.Shape2.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(7.6000003814697266R, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(5.6366009712219238R, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.Shape2.Name = "Shape2"
        Me.Shape2.ShapeType = New Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW)
        Me.Shape2.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(4.4836006164550781R, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.16319847106933594R, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.Shape2.Style.LineWidth = New Telerik.Reporting.Drawing.Unit(1.0R, Telerik.Reporting.Drawing.UnitType.Pixel)
        '
        'ReportHeaderSection1
        '
        Me.ReportHeaderSection1.Height = New Telerik.Reporting.Drawing.Unit(0.18000000715255737R, Telerik.Reporting.Drawing.UnitType.Inch)
        Me.ReportHeaderSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.headerCant, Me.headerUnidad, Me.headerDesc, Me.headerPU, Me.headerImporte, Me.TextBox1})
        Me.ReportHeaderSection1.Name = "ReportHeaderSection1"
        '
        'headerCant
        '
        Me.headerCant.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.61003941297531128R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.headerCant.Name = "headerCant"
        Me.headerCant.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.60992121696472168R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.17999997735023499R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.headerCant.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerCant.Style.Color = System.Drawing.Color.White
        Me.headerCant.Style.Font.Bold = True
        Me.headerCant.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(6.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.headerCant.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.headerCant.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerCant.Value = "   CANTIDAD"
        '
        'headerUnidad
        '
        Me.headerUnidad.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(1.2200393676757813R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.headerUnidad.Name = "headerUnidad"
        Me.headerUnidad.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.68913370370864868R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.17999997735023499R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.headerUnidad.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerUnidad.Style.Color = System.Drawing.Color.White
        Me.headerUnidad.Style.Font.Bold = True
        Me.headerUnidad.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(6.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.headerUnidad.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.headerUnidad.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerUnidad.Value = "   UNIDAD"
        '
        'headerDesc
        '
        Me.headerDesc.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(1.9092518091201782R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.headerDesc.Name = "headerDesc"
        Me.headerDesc.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(3.9806690216064453R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.17992103099822998R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.headerDesc.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerDesc.Style.Color = System.Drawing.Color.White
        Me.headerDesc.Style.Font.Bold = True
        Me.headerDesc.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(6.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.headerDesc.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.headerDesc.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerDesc.Value = "DESCRIPCIÓN"
        '
        'headerPU
        '
        Me.headerPU.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(5.8899993896484375R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.headerPU.Name = "headerPU"
        Me.headerPU.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.94000226259231567R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.17992103099822998R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.headerPU.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerPU.Style.Color = System.Drawing.Color.White
        Me.headerPU.Style.Font.Bold = True
        Me.headerPU.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(6.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.headerPU.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.headerPU.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerPU.Value = "   PRECIO UNITARIO"
        '
        'headerImporte
        '
        Me.headerImporte.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(6.8300795555114746R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.headerImporte.Name = "headerImporte"
        Me.headerImporte.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.86991995573043823R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.17992103099822998R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.headerImporte.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerImporte.Style.Color = System.Drawing.Color.White
        Me.headerImporte.Style.Font.Bold = True
        Me.headerImporte.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(6.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.headerImporte.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.headerImporte.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerImporte.Value = "   IMPORTE"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039458274841308594R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.60992121696472168R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.17999997735023499R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox1.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.TextBox1.Style.Color = System.Drawing.Color.White
        Me.TextBox1.Style.Font.Bold = True
        Me.TextBox1.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(6.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox1.Value = "   CÓDIGO"
        '
        'OrdenCompra
        '
        Me.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.pageHeaderSection1, Me.detail, Me.pageFooterSection1, Me.ReportHeaderSection1})
        Me.Name = "formato_cfdi"
        Me.PageSettings.Landscape = False
        Me.PageSettings.Margins.Bottom = New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.Margins.Left = New Telerik.Reporting.Drawing.Unit(1.0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.Margins.Right = New Telerik.Reporting.Drawing.Unit(1.0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.Margins.Top = New Telerik.Reporting.Drawing.Unit(1.0R, Telerik.Reporting.Drawing.UnitType.Cm)
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
        Me.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(8.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.Width = New Telerik.Reporting.Drawing.Unit(7.7125587463378906R, Telerik.Reporting.Drawing.UnitType.Inch)
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