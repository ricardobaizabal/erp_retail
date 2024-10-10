Imports System.Data
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI

Partial Public Class utilidad
    Inherits System.Web.UI.Page
    Private ds As DataSet
    Private venta, utilidad, egresos, inventario As Decimal

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '
        Me.Title = Resources.Resource.WindowsTitle
        '
        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Reporte de utilidad"
        '
        If Not IsPostBack Then
            Dim ObjCat As New DataControl(1)
            ObjCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal where id<>52 order by nombre", 0, True)
            ObjCat = Nothing
        End If
    End Sub

    Private Sub reporteGrid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles reporteGrid.NeedDataSource
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteUtilidad @anio='" & cmbAnio.SelectedValue.ToString & "', @mes='" & cmbMes.SelectedValue.ToString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "'")
        reporteGrid.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        reporteGrid.DataSource = ds.Tables(0)
        ObjData = Nothing
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteUtilidad @anio='" & cmbAnio.SelectedValue.ToString & "', @mes='" & cmbMes.SelectedValue.ToString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "'")
        reporteGrid.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        reporteGrid.DataSource = ds.Tables(0)
        reporteGrid.DataBind()
        ObjData = Nothing
    End Sub

    'Private Sub reporteGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles reporteGrid.ItemDataBound
    '    Select e.Item.ItemType
    '        Case Telerik.Web.UI.GridItemType.Footer
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                If Not IsDBNull(ds.Tables(0).Compute("sum(total)", "")) Then
    '                    e.Item.Cells(5).Text = FormatCurrency(ds.Tables(0).Compute("sum(total)", ""), 2).ToString
    '                    e.Item.Cells(5).HorizontalAlign = HorizontalAlign.Right
    '                    e.Item.Cells(5).Font.Bold = True
    '                    venta = ds.Tables(0).Compute("sum(total)", "")
    '                    lblTotalVenta.Text = FormatCurrency(venta, 2).ToString
    '                End If
    '                If Not IsDBNull(ds.Tables(0).Compute("sum(costo)", "")) Then
    '                    e.Item.Cells(6).Text = FormatCurrency(ds.Tables(0).Compute("sum(costo)", ""), 2).ToString
    '                    e.Item.Cells(6).HorizontalAlign = HorizontalAlign.Right
    '                    e.Item.Cells(6).Font.Bold = True
    '                End If
    '                If Not IsDBNull(ds.Tables(0).Compute("sum(utilidad)", "")) Then
    '                    e.Item.Cells(7).Text = FormatCurrency(ds.Tables(0).Compute("sum(utilidad)", ""), 2).ToString
    '                    e.Item.Cells(7).HorizontalAlign = HorizontalAlign.Right
    '                    e.Item.Cells(7).Font.Bold = True
    '                    utilidad = ds.Tables(0).Compute("sum(utilidad)", "")
    '                    lblTotalUtilidad.Text = FormatCurrency(utilidad, 2).ToString
    '                End If
    '                If Not IsDBNull(ds.Tables(0).Compute("avg(porcentaje_utilidad)", "")) Then
    '                    e.Item.Cells(8).Text = FormatPercent(ds.Tables(0).Compute("avg(porcentaje_utilidad)", ""), 2).ToString
    '                    e.Item.Cells(8).HorizontalAlign = HorizontalAlign.Right
    '                    e.Item.Cells(8).Font.Bold = True
    '                End If
    '                If Not IsDBNull(ds.Tables(0).Compute("avg(factor_ponderado)", "")) Then
    '                    e.Item.Cells(9).Text = FormatPercent(ds.Tables(0).Compute("sum(factor_ponderado)", ""), 2).ToString
    '                    e.Item.Cells(9).HorizontalAlign = HorizontalAlign.Right
    '                    e.Item.Cells(9).Font.Bold = True
    '                End If
    '                If Not IsDBNull(ds.Tables(0).Compute("avg(utilidad_ponderada)", "")) Then
    '                    e.Item.Cells(10).Text = FormatPercent(ds.Tables(0).Compute("sum(utilidad_ponderada)", ""), 2).ToString
    '                    e.Item.Cells(10).HorizontalAlign = HorizontalAlign.Right
    '                    e.Item.Cells(10).Font.Bold = True
    '                End If
    '                Call MuestraTotales()
    '                lblTotal.Text = FormatCurrency(utilidad - egresos + inventario).ToString
    '            End If
    '    End Select

    'End Sub

    'Private Sub reporteGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles reporteGrid.NeedDataSource
    '    '
    '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
    '    '
    '    Dim ObjData As New DataControl(1)
    '    ds = ObjData.FillDataSet("exec pMisInformes @cmd=26, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @clienteid='" & clienteid.SelectedValue.ToString & "', @tipoid='" & tipoid.SelectedValue.ToString & "'")
    '    reporteGrid.DataSource = ds.Tables(0)
    '    ObjData = Nothing
    '    '
    '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
    '    '
    'End Sub

    'Private Sub MuestraReporte()
    '    '
    '    reporteGrid.Rebind()
    '    reporteGrid.CurrentPageIndex = 0
    '    '
    '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
    '    '
    '    Dim ObjData As New DataControl(1)
    '    ds = ObjData.FillDataSet("exec pMisInformes @cmd=26, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @clienteid='" & clienteid.SelectedValue.ToString & "', @tipoid='" & tipoid.SelectedValue.ToString & "'")
    '    reporteGrid.DataSource = ds.Tables(0).DefaultView
    '    reporteGrid.DataBind()
    '    ObjData = Nothing
    '    '
    '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
    '    '
    'End Sub

    'Private Sub MuestraTotales()
    '    '
    '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
    '    '
    '    Dim ObjData As New DataControl(1)
    '    Dim datos As New DataSet
    '    datos = ObjData.FillDataSet("exec pMisInformes @cmd=27, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "'")
    '    ObjData = Nothing
    '    '
    '    If datos.Tables(0).Rows.Count > 0 Then
    '        For Each row As DataRow In datos.Tables(0).Rows
    '            egresos = row("total_egresos")
    '            inventario = row("total_inventario")
    '        Next
    '        lblTotalEgresos.Text = FormatCurrency(egresos, 2).ToString
    '        lblTotalInventario.Text = FormatCurrency(inventario, 2).ToString
    '    End If
    '    '
    '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
    '    '
    'End Sub

    'Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
    '    Call MuestraReporte()
    '    Call MuestraTotales()
    'End Sub

End Class