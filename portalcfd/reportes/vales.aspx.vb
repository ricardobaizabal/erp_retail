Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Threading
Public Class vales
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle
        If Not IsPostBack Then
            calFechaDesde.SelectedDate = Now()
            calFechaHasta.SelectedDate = Now()
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0, True)
            objCat = Nothing
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
        ds = ObjData.FillDataSet("exec pValesCaja @cmd=1, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaDesde.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaHasta.SelectedDate.Value.ToShortDateString & "'")
        valesList.DataSource = ds
        valesList.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub
    Private Sub valesList_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles valesList.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pValesCaja @cmd=1, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaDesde.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaHasta.SelectedDate.Value.ToShortDateString & "'")
        valesList.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

End Class