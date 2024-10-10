Imports System.Threading
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class asistencia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Reporte de asistencia"

        If Not IsPostBack Then
            calFechaIni.SelectedDate = Date.Now
            calFechaFin.SelectedDate = Date.Now
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0, True)
            objCat = New DataControl(0)
            objCat.Catalogo(cmbEmpleado, "exec pEmpleados @cmd=6, @clienteid='" & Session("clienteid") & "'", 0, True)
            objCat = Nothing
        End If
    End Sub

    Private Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Call MuestraAsistencias()
    End Sub

    Private Sub reporteAsistenciaGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles reporteAsistenciaGrid.NeedDataSource
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet()
        ds = ObjData.FillDataSet("exec pReporteAsistencia @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaIni.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaFin.SelectedDate.Value.ToShortDateString & "', @empleadoid='" & cmbEmpleado.SelectedValue.ToString & "'")
        reporteAsistenciaGrid.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
    End Sub

    Private Sub MuestraAsistencias()

        reporteAsistenciaGrid.DataSource = Nothing
        reporteAsistenciaGrid.DataBind()

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet()
        ds = ObjData.FillDataSet("exec pReporteAsistencia @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaIni.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaFin.SelectedDate.Value.ToShortDateString & "', @empleadoid='" & cmbEmpleado.SelectedValue.ToString & "'")
        reporteAsistenciaGrid.DataSource = ds
        reporteAsistenciaGrid.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
    End Sub

End Class