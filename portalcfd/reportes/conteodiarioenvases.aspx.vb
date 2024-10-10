Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Threading
Public Class conteodiarioenvases
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle
        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Conteo diario envases"

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
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pConteoDiario @cmd=4, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaDesde.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaHasta.SelectedDate.Value.ToShortDateString & "'")
        reporteGrid.DataSource = ds
        reporteGrid.DataBind()

        ds = ObjData.FillDataSet("exec pConteoDiario @cmd=5, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaDesde.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaHasta.SelectedDate.Value.ToShortDateString & "'")
        reporteGridVentas.DataSource = ds
        reporteGridVentas.DataBind()

        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub
    'Private Sub reporteGrid_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles reporteGrid.ItemDataBound
    'Select Case e.Item.ItemType
    '    Case Telerik.Web.UI.GridItemType.Footer
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            e.Item.Cells(11).Text = ds.Tables(0).Compute("sum(cantidad)", "").ToString
    '            e.Item.Cells(11).HorizontalAlign = HorizontalAlign.Right
    '            e.Item.Cells(11).Font.Bold = True
    '            '
    '            e.Item.Cells(12).Text = ds.Tables(0).Compute("sum(existencia)", "").ToString
    '            e.Item.Cells(12).HorizontalAlign = HorizontalAlign.Right
    '            e.Item.Cells(12).Font.Bold = True
    '            '
    '            e.Item.Cells(13).Text = ds.Tables(0).Compute("sum(faltante)", "").ToString
    '            e.Item.Cells(13).HorizontalAlign = HorizontalAlign.Right
    '            e.Item.Cells(13).Font.Bold = True
    '            '
    '            e.Item.Cells(14).Text = ds.Tables(0).Compute("sum(sobrante)", "").ToString
    '            e.Item.Cells(14).HorizontalAlign = HorizontalAlign.Right
    '            e.Item.Cells(14).Font.Bold = True

    '        End If
    'End Select
    'End Sub
    Private Sub reporteGrid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles reporteGrid.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pConteoDiario @cmd=4, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaDesde.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaHasta.SelectedDate.Value.ToShortDateString & "'")
        reporteGrid.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub reporteGridVentas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles reporteGridVentas.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pConteoDiario @cmd=5, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaDesde.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaHasta.SelectedDate.Value.ToShortDateString & "'")
        reporteGridVentas.DataSource = ds
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub
End Class