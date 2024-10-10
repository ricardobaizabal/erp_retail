Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Threading
Partial Public Class conteodiario
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle
        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Conteo diario"

        If Not IsPostBack Then
            calFechaDesde.SelectedDate = Now()
            calFechaHasta.SelectedDate = Now()
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0, True)
            objCat = Nothing

            Session("filtroconteoid") = 0
            Session("filtrosucursalid") = 0

        End If

    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Call MuestraLista()
    End Sub

    Private Sub MuestraLista()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        panelDetalleConteo.Visible = False
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pConteoDiario @cmd=1, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaDesde.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaHasta.SelectedDate.Value.ToShortDateString & "'")
        reporteGrid.DataSource = ds
        reporteGrid.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub VerDetalle(ByVal conteoid As String, ByVal sucursalid As String)

        Session("filtroconteoid") = conteoid
        Session("filtrosucursalid") = sucursalid

        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pConteoDiario @cmd=2, @id='" & conteoid.ToString & "', @sucursalid='" & sucursalid.ToString & "'")
        detalleGrid.DataSource = ds.Tables(0)
        detalleGrid.DataBind()

        If ds.Tables(1).Rows.Count > 0 Then
            lblUltimoInventarioValue.Text = FormatCurrency(ds.Tables(1).Rows(0)("importe"), 2)
            lblVentasValue.Text = FormatCurrency(ds.Tables(1).Rows(0)("ventas"), 2)
            lblComprasValue.Text = FormatCurrency(ds.Tables(1).Rows(0)("compras"), 2)
            lblInventarioValue.Text = FormatCurrency(ds.Tables(1).Rows(0)("inventario"), 2)
            lblInventarioFinalRealValue.Text = FormatCurrency(ds.Tables(1).Rows(0)("inventario_real"), 2)
        End If

        ds = ObjData.FillDataSet("exec pConteoDiario @cmd=3, @id='" & conteoid.ToString & "', @sucursalid='" & sucursalid.ToString & "'")

        If ds.Tables(0).Rows.Count > 0 Then
            lblCantidadFaltanteValue.Text = ds.Tables(0).Rows(0)("cantidad_faltante")
            lblImporteFaltanteValue.Text = FormatCurrency(ds.Tables(0).Rows(0)("importe_faltante"), 2)
        End If

        If ds.Tables(1).Rows.Count > 0 Then
            lblCantidadSobranteValue.Text = ds.Tables(1).Rows(0)("cantidad_sobrante")
            lblImporteSobranteValue.Text = FormatCurrency(ds.Tables(1).Rows(0)("importe_sobrante"), 2)
            lblDiferenciaValue.Text = FormatCurrency(ds.Tables(1).Rows(0)("importe_sobrante") - ds.Tables(0).Rows(0)("importe_faltante"), 2)
        End If

        ObjData = Nothing

    End Sub

    Private Sub reporteGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles reporteGrid.ItemCommand
        Select Case e.CommandName
            Case "cmdDetalle"
                panelDetalleConteo.Visible = True
                Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","c})
                Dim conteoid As String = commandArgs(0)
                Dim sucursalid As String = commandArgs(1)
                Call VerDetalle(conteoid, sucursalid)
        End Select
    End Sub

    Private Sub reporteGrid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles reporteGrid.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pConteoDiario @cmd=1, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaDesde.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaHasta.SelectedDate.Value.ToShortDateString & "'")
        reporteGrid.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub detalleGrid_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles detalleGrid.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    e.Item.Cells(7).Text = ds.Tables(0).Compute("sum(cantidad)", "").ToString
                    e.Item.Cells(7).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(7).Font.Bold = True
                    '
                    e.Item.Cells(8).Text = ds.Tables(0).Compute("sum(existencia)", "").ToString
                    e.Item.Cells(8).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(8).Font.Bold = True
                    '
                    e.Item.Cells(9).Text = ds.Tables(0).Compute("sum(faltante)", "").ToString
                    e.Item.Cells(9).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(9).Font.Bold = True
                    '
                    e.Item.Cells(10).Text = ds.Tables(0).Compute("sum(sobrante)", "").ToString
                    e.Item.Cells(10).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(10).Font.Bold = True
                End If
        End Select
    End Sub

    Private Sub detalleGrid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles detalleGrid.NeedDataSource
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pConteoDiario @cmd=2, @id='" & Session("filtroconteoid").ToString & "', @sucursalid='" & Session("filtrosucursalid").ToString & "'")
        detalleGrid.DataSource = ds.Tables(0)
        ObjData = Nothing
    End Sub

End Class