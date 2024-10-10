Imports System.Data
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI

Partial Public Class ventas
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle
        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Reporte de ventas"

        If Not IsPostBack Then
            fechaini.SelectedDate = Now
            fechafin.SelectedDate = Now

            Dim ObjCat As New DataControl(1)
            ObjCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0, True)
            ObjCat = Nothing

            Call MuestraReporte()

        End If

    End Sub

    Private Sub reporteGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles reporteGrid.NeedDataSource
        '
        reporteGrid.CurrentPageIndex = 0
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteVentasBAK @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "'")
        reporteGrid.DataSource = ds.Tables(0).DefaultView
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Call MuestraReporte()
    End Sub

    Private Sub MuestraReporte()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteVentasBAK @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "'")
        reporteGrid.DataSource = ds.Tables(0).DefaultView
        reporteGrid.DataBind()

        'ds = ObjData.FillDataSet("exec pReporteVentasFamilia @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "'")
        'reporteGridFamilia.DataSource = ds.Tables(0).DefaultView
        'reporteGridFamilia.DataBind()

        'RadVentasPorFamilia.ChartTitle.Appearance.TextStyle.Bold = True
        'RadVentasPorFamilia.DataSource = ds
        'RadVentasPorFamilia.DataBind()

        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub reporteGrid_ColumnCreated(sender As Object, e As GridColumnCreatedEventArgs) Handles reporteGrid.ColumnCreated
        If e.Column.DataType.ToString = "System.Decimal" Then
            e.Column.ItemStyle.HorizontalAlign = HorizontalAlign.Right
            Dim col As GridNumericColumn = TryCast(e.Column, GridNumericColumn)
            If e.Column.DataType.ToString = "System.Decimal" Then
                col.DataFormatString = "{0:C2}"
                col.Aggregate = GridAggregateFunction.Sum
                col.FooterStyle.HorizontalAlign = HorizontalAlign.Right
                col.FooterStyle.Font.Bold = True
            End If
        End If
    End Sub

    Private Sub reporteGridFamilia_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles reporteGridFamilia.NeedDataSource
        '
        'reporteGridFamilia.CurrentPageIndex = 0
        ''
        'Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        'Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        ''
        'Dim ObjData As New DataControl(1)
        'ds = ObjData.FillDataSet("exec pReporteVentasFamilia @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "'")
        'reporteGridFamilia.DataSource = ds.Tables(0).DefaultView
        'ObjData = Nothing
        ''
        'Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        'Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        ''
    End Sub

    'Private Sub reporteGridFamilia_ColumnCreated(sender As Object, e As GridColumnCreatedEventArgs) Handles reporteGridFamilia.ColumnCreated
    '    If e.Column.DataType.ToString = "System.Decimal" Then
    '        e.Column.ItemStyle.HorizontalAlign = HorizontalAlign.Right
    '        Dim col As GridNumericColumn = TryCast(e.Column, GridNumericColumn)
    '        If e.Column.DataType.ToString = "System.Decimal" Then
    '            col.DataFormatString = "{0:C2}"
    '            col.Aggregate = GridAggregateFunction.Sum
    '            col.FooterStyle.HorizontalAlign = HorizontalAlign.Right
    '            col.FooterStyle.Font.Bold = True
    '        End If
    '    End If
    'End Sub

End Class