Imports System.Data
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI
Public Class abastecimiento_utilidad
    Inherits System.Web.UI.Page
    Private ds As DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '
        Me.Title = Resources.Resource.WindowsTitle
        '
        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Reporte de Abastecimiento y Utilidad"
        '
        If Not IsPostBack Then
            fechaini.SelectedDate = Date.Now
            fechafin.SelectedDate = Date.Now
            Dim ObjCat As New DataControl(1)
            ObjCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal where id<>52 order by nombre", 0, True)
            ObjCat = Nothing
            Call Totales()
        End If
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteAbastecimientoUtilidad @cmd=1, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @codigo='" & txtCodigo.Text & "'")
        reporteGrid.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        reporteGrid.DataSource = ds.Tables(0)
        reporteGrid.DataBind()
        ObjData = Nothing

        Totales()

        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub reporteGrid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles reporteGrid.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteAbastecimientoUtilidad @cmd=1, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @codigo='" & txtCodigo.Text & "'")
        reporteGrid.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        reporteGrid.DataSource = ds.Tables(0)
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub Totales()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteAbastecimientoUtilidad @cmd=2, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @codigo='" & txtCodigo.Text & "'")
        ObjData = Nothing

        If ds.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                lblCostoPromedioValue.Text = CStr(FormatCurrency(row("costo_promedio"), 2))
                'lblVentasPromedioValue.Text = CStr(FormatCurrency(row("venta_promedio"), 2))
                'lblUtilidadPromedioValue.Text = CStr(FormatPercent(row("utilidad_promedio"), 2))
            Next
        End If
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

End Class