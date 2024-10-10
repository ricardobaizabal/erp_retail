Partial Class OrdenSalida

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
        Dim ReportParameter19 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter20 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter21 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter22 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Me.pageHeaderSection1 = New Telerik.Reporting.PageHeaderSection()
        Me.imgBanner = New Telerik.Reporting.PictureBox()
        Me.lblTitle = New Telerik.Reporting.TextBox()
        Me.boxRFC = New Telerik.Reporting.TextBox()
        Me.TextBox8 = New Telerik.Reporting.TextBox()
        Me.boxCalleNum = New Telerik.Reporting.TextBox()
        Me.boxRazonSocial = New Telerik.Reporting.TextBox()
        Me.TextBox4 = New Telerik.Reporting.TextBox()
        Me.TextBox5 = New Telerik.Reporting.TextBox()
        Me.TextBox6 = New Telerik.Reporting.TextBox()
        Me.TextBox2 = New Telerik.Reporting.TextBox()
        Me.TextBox7 = New Telerik.Reporting.TextBox()
        Me.TextBox9 = New Telerik.Reporting.TextBox()
        Me.detail = New Telerik.Reporting.DetailSection()
        Me.unidadDataTextBox = New Telerik.Reporting.TextBox()
        Me.cantidadDataTextBox = New Telerik.Reporting.TextBox()
        Me.descripcionDataTextBox = New Telerik.Reporting.TextBox()
        Me.Shape1 = New Telerik.Reporting.Shape()
        Me.TextBox3 = New Telerik.Reporting.TextBox()
        Me.pageFooterSection1 = New Telerik.Reporting.PageFooterSection()
        Me.panelEspeciales = New Telerik.Reporting.Panel()
        Me.lblComentarios = New Telerik.Reporting.TextBox()
        Me.txtComentarios = New Telerik.Reporting.TextBox()
        Me.ReportHeaderSection1 = New Telerik.Reporting.ReportHeaderSection()
        Me.headerCant = New Telerik.Reporting.TextBox()
        Me.headerUnidad = New Telerik.Reporting.TextBox()
        Me.headerDesc = New Telerik.Reporting.TextBox()
        Me.TextBox1 = New Telerik.Reporting.TextBox()
        Me.TextBox10 = New Telerik.Reporting.TextBox()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'pageHeaderSection1
        '
        Me.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(2.047R)
        Me.pageHeaderSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.imgBanner, Me.lblTitle, Me.boxRFC, Me.TextBox8, Me.boxCalleNum, Me.boxRazonSocial, Me.TextBox4, Me.TextBox5, Me.TextBox6, Me.TextBox2, Me.TextBox7, Me.TextBox9})
        Me.pageHeaderSection1.Name = "pageHeaderSection1"
        '
        'imgBanner
        '
        Me.imgBanner.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.imgBanner.MimeType = ""
        Me.imgBanner.Name = "imgBanner"
        Me.imgBanner.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.402R), Telerik.Reporting.Drawing.Unit.Inch(1.43R))
        Me.imgBanner.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional
        Me.imgBanner.Value = "=Parameters.paramImgBanner.Value"
        '
        'lblTitle
        '
        Me.lblTitle.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.795R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.685R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.lblTitle.Style.BackgroundColor = System.Drawing.Color.Empty
        Me.lblTitle.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None
        Me.lblTitle.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5R)
        Me.lblTitle.Style.Color = System.Drawing.Color.Black
        Me.lblTitle.Style.Font.Bold = True
        Me.lblTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10.0R)
        Me.lblTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.lblTitle.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblTitle.Value = "ORDEN DE SALIDA"
        '
        'boxRFC
        '
        Me.boxRFC.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.913R), Telerik.Reporting.Drawing.Unit.Inch(0.394R))
        Me.boxRFC.Name = "boxRFC"
        Me.boxRFC.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.669R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxRFC.Style.Font.Bold = True
        Me.boxRFC.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10.0R)
        Me.boxRFC.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxRFC.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxRFC.Value = "FOLIO:"
        '
        'TextBox8
        '
        Me.TextBox8.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.913R), Telerik.Reporting.Drawing.Unit.Inch(0.788R))
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.669R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox8.Style.Font.Bold = False
        Me.TextBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9.0R)
        Me.TextBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox8.Value = "ORIGEN: "
        '
        'boxCalleNum
        '
        Me.boxCalleNum.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.913R), Telerik.Reporting.Drawing.Unit.Inch(0.591R))
        Me.boxCalleNum.Name = "boxCalleNum"
        Me.boxCalleNum.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.669R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxCalleNum.Style.Font.Bold = False
        Me.boxCalleNum.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10.0R)
        Me.boxCalleNum.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxCalleNum.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxCalleNum.Value = "FECHA:"
        '
        'boxRazonSocial
        '
        Me.boxRazonSocial.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.582R), Telerik.Reporting.Drawing.Unit.Inch(0.394R))
        Me.boxRazonSocial.Name = "boxRazonSocial"
        Me.boxRazonSocial.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.339R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.boxRazonSocial.Style.Font.Bold = True
        Me.boxRazonSocial.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10.0R)
        Me.boxRazonSocial.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.boxRazonSocial.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.boxRazonSocial.Value = "= Parameters.txtFolio.Value"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.582R), Telerik.Reporting.Drawing.Unit.Inch(0.591R))
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.339R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox4.Style.Font.Bold = False
        Me.TextBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10.0R)
        Me.TextBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox4.Value = "= Parameters.txtFechaEmision.Value"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.582R), Telerik.Reporting.Drawing.Unit.Inch(0.788R))
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.819R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox5.Style.Font.Bold = False
        Me.TextBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9.0R)
        Me.TextBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox5.Value = "= Parameters.txtOrigen.Value"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.913R), Telerik.Reporting.Drawing.Unit.Inch(0.984R))
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.669R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox6.Style.Font.Bold = False
        Me.TextBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9.0R)
        Me.TextBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox6.Value = "DESTINO:"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.582R), Telerik.Reporting.Drawing.Unit.Inch(0.984R))
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.819R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox2.Style.Font.Bold = False
        Me.TextBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9.0R)
        Me.TextBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox2.Value = "= Parameters.txtDestino.Value"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.582R), Telerik.Reporting.Drawing.Unit.Inch(1.18R))
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.819R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox7.Style.Font.Bold = False
        Me.TextBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9.0R)
        Me.TextBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox7.Value = "= Parameters.txtUsuario.Value"
        '
        'TextBox9
        '
        Me.TextBox9.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.913R), Telerik.Reporting.Drawing.Unit.Inch(1.18R))
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.669R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox9.Style.Font.Bold = False
        Me.TextBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9.0R)
        Me.TextBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox9.Value = "USUARIO:"
        '
        'detail
        '
        Me.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.725R)
        Me.detail.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.unidadDataTextBox, Me.cantidadDataTextBox, Me.descripcionDataTextBox, Me.Shape1, Me.TextBox3})
        Me.detail.Name = "detail"
        Me.detail.Style.BackgroundColor = System.Drawing.Color.Empty
        '
        'unidadDataTextBox
        '
        Me.unidadDataTextBox.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.772R), Telerik.Reporting.Drawing.Unit.Inch(0.064R))
        Me.unidadDataTextBox.Name = "unidadDataTextBox"
        Me.unidadDataTextBox.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.023R), Telerik.Reporting.Drawing.Unit.Inch(0.157R))
        Me.unidadDataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.unidadDataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Cm(0R)
        Me.unidadDataTextBox.Style.Font.Name = "Arial"
        Me.unidadDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9.0R)
        Me.unidadDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.unidadDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.unidadDataTextBox.StyleName = "Data"
        Me.unidadDataTextBox.Value = "=unidad"
        '
        'cantidadDataTextBox
        '
        Me.cantidadDataTextBox.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.945R), Telerik.Reporting.Drawing.Unit.Inch(0.064R))
        Me.cantidadDataTextBox.Name = "cantidadDataTextBox"
        Me.cantidadDataTextBox.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.827R), Telerik.Reporting.Drawing.Unit.Inch(0.157R))
        Me.cantidadDataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.cantidadDataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Cm(0R)
        Me.cantidadDataTextBox.Style.Font.Name = "Arial"
        Me.cantidadDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9.0R)
        Me.cantidadDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.cantidadDataTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.cantidadDataTextBox.StyleName = "Data"
        Me.cantidadDataTextBox.Value = "=cantidad"
        '
        'descripcionDataTextBox
        '
        Me.descripcionDataTextBox.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.795R), Telerik.Reporting.Drawing.Unit.Inch(0.064R))
        Me.descripcionDataTextBox.Name = "descripcionDataTextBox"
        Me.descripcionDataTextBox.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.905R), Telerik.Reporting.Drawing.Unit.Inch(0.157R))
        Me.descripcionDataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.descripcionDataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Cm(0R)
        Me.descripcionDataTextBox.Style.Font.Name = "Arial"
        Me.descripcionDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9.0R)
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
        Me.TextBox3.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.945R), Telerik.Reporting.Drawing.Unit.Inch(0.157R))
        Me.TextBox3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None
        Me.TextBox3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Cm(0R)
        Me.TextBox3.Style.Font.Name = "Arial"
        Me.TextBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9.0R)
        Me.TextBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.TextBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top
        Me.TextBox3.StyleName = "Data"
        Me.TextBox3.Value = "=codigo"
        '
        'pageFooterSection1
        '
        Me.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1.629R)
        Me.pageFooterSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.panelEspeciales, Me.TextBox10})
        Me.pageFooterSection1.Name = "pageFooterSection1"
        '
        'panelEspeciales
        '
        Me.panelEspeciales.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.lblComentarios, Me.txtComentarios})
        Me.panelEspeciales.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.panelEspeciales.Name = "panelEspeciales"
        Me.panelEspeciales.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.7R), Telerik.Reporting.Drawing.Unit.Inch(0.991R))
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
        Me.lblComentarios.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9.0R)
        Me.lblComentarios.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.lblComentarios.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblComentarios.Value = "COMENTARIOS"
        '
        'txtComentarios
        '
        Me.txtComentarios.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.06R), Telerik.Reporting.Drawing.Unit.Inch(0.16R))
        Me.txtComentarios.Name = "txtComentarios"
        Me.txtComentarios.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.575R), Telerik.Reporting.Drawing.Unit.Inch(0.831R))
        Me.txtComentarios.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10.0R)
        Me.txtComentarios.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtComentarios.Value = "= Parameters.txtComentarios.Value"
        '
        'ReportHeaderSection1
        '
        Me.ReportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.18R)
        Me.ReportHeaderSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.headerCant, Me.headerUnidad, Me.headerDesc, Me.TextBox1})
        Me.ReportHeaderSection1.Name = "ReportHeaderSection1"
        '
        'headerCant
        '
        Me.headerCant.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.945R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.headerCant.Name = "headerCant"
        Me.headerCant.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.827R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.headerCant.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerCant.Style.Color = System.Drawing.Color.White
        Me.headerCant.Style.Font.Bold = True
        Me.headerCant.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.0R)
        Me.headerCant.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.headerCant.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerCant.Value = "   CANTIDAD"
        '
        'headerUnidad
        '
        Me.headerUnidad.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.772R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.headerUnidad.Name = "headerUnidad"
        Me.headerUnidad.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.023R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.headerUnidad.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerUnidad.Style.Color = System.Drawing.Color.White
        Me.headerUnidad.Style.Font.Bold = True
        Me.headerUnidad.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.0R)
        Me.headerUnidad.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.headerUnidad.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerUnidad.Value = "   UNIDAD"
        '
        'headerDesc
        '
        Me.headerDesc.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.795R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.headerDesc.Name = "headerDesc"
        Me.headerDesc.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.905R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.headerDesc.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.headerDesc.Style.Color = System.Drawing.Color.White
        Me.headerDesc.Style.Font.Bold = True
        Me.headerDesc.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.0R)
        Me.headerDesc.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.headerDesc.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.headerDesc.Value = "DESCRIPCIÓN"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0R), Telerik.Reporting.Drawing.Unit.Inch(0R))
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.945R), Telerik.Reporting.Drawing.Unit.Inch(0.18R))
        Me.TextBox1.Style.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.TextBox1.Style.Color = System.Drawing.Color.White
        Me.TextBox1.Style.Font.Bold = True
        Me.TextBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.0R)
        Me.TextBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.TextBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox1.Value = "   CÓDIGO"
        '
        'TextBox10
        '
        Me.TextBox10.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.561R), Telerik.Reporting.Drawing.Unit.Inch(1.385R))
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.139R), Telerik.Reporting.Drawing.Unit.Inch(0.13R))
        Me.TextBox10.Style.Font.Bold = False
        Me.TextBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7.0R)
        Me.TextBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.TextBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox10.Value = "= 'página ' + PageNumber + ' de ' + PageCount"
        '
        'OrdenSalida
        '
        Me.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.pageHeaderSection1, Me.detail, Me.pageFooterSection1, Me.ReportHeaderSection1})
        Me.Name = "formato_cfdi"
        Me.PageSettings.Landscape = False
        Me.PageSettings.Margins = New Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(1.0R), Telerik.Reporting.Drawing.Unit.Cm(1.0R), Telerik.Reporting.Drawing.Unit.Cm(1.0R), Telerik.Reporting.Drawing.Unit.Cm(0R))
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter
        ReportParameter1.AllowNull = True
        ReportParameter1.Name = "cnn"
        ReportParameter2.AllowNull = True
        ReportParameter2.Name = "plantillaId"
        ReportParameter2.Type = Telerik.Reporting.ReportParameterType.[Integer]
        ReportParameter3.AllowNull = True
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
        ReportParameter19.AllowNull = True
        ReportParameter19.Name = "txtFolio"
        ReportParameter20.AllowNull = True
        ReportParameter20.Name = "txtOrigen"
        ReportParameter21.AllowNull = True
        ReportParameter21.Name = "txtDestino"
        ReportParameter22.AllowNull = True
        ReportParameter22.Name = "loteid"
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
        Me.ReportParameters.Add(ReportParameter19)
        Me.ReportParameters.Add(ReportParameter20)
        Me.ReportParameters.Add(ReportParameter21)
        Me.ReportParameters.Add(ReportParameter22)
        Me.Style.BackgroundColor = System.Drawing.Color.White
        Me.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10.0R)
        Me.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.Width = Telerik.Reporting.Drawing.Unit.Inch(7.713R)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents pageHeaderSection1 As Telerik.Reporting.PageHeaderSection
    Friend WithEvents detail As Telerik.Reporting.DetailSection
    Friend WithEvents pageFooterSection1 As Telerik.Reporting.PageFooterSection
    Friend WithEvents imgBanner As Telerik.Reporting.PictureBox
    Friend WithEvents ReportHeaderSection1 As Telerik.Reporting.ReportHeaderSection
    Friend WithEvents headerCant As Telerik.Reporting.TextBox
    Friend WithEvents headerUnidad As Telerik.Reporting.TextBox
    Friend WithEvents headerDesc As Telerik.Reporting.TextBox
    Friend WithEvents unidadDataTextBox As Telerik.Reporting.TextBox
    Friend WithEvents cantidadDataTextBox As Telerik.Reporting.TextBox
    Friend WithEvents descripcionDataTextBox As Telerik.Reporting.TextBox
    Friend WithEvents lblTitle As Telerik.Reporting.TextBox
    Friend WithEvents panelEspeciales As Telerik.Reporting.Panel
    Friend WithEvents lblComentarios As Telerik.Reporting.TextBox
    Friend WithEvents txtComentarios As Telerik.Reporting.TextBox
    Friend WithEvents Shape1 As Telerik.Reporting.Shape
    Friend WithEvents TextBox3 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox1 As Telerik.Reporting.TextBox
    Friend WithEvents boxRFC As Telerik.Reporting.TextBox
    Friend WithEvents TextBox8 As Telerik.Reporting.TextBox
    Friend WithEvents boxCalleNum As Telerik.Reporting.TextBox
    Friend WithEvents boxRazonSocial As Telerik.Reporting.TextBox
    Friend WithEvents TextBox4 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox5 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox6 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox2 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox7 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox9 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox10 As Telerik.Reporting.TextBox
End Class