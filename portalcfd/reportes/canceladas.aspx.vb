Imports System.Data
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI
Public Class canceladas
    Inherits System.Web.UI.Page
    Private ds As DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Reporte de facturas canceladas"

        If Not IsPostBack Then
            fechaini.SelectedDate = DateAdd(DateInterval.Day, -7, Now)
            fechafin.SelectedDate = Now
            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(cmbCliente, "exec pCatalogos @cmd=2", 0, True)
            ObjData.Catalogo(cmbTipoDocumento, "select id, nombre from tblTipoDocumento where id in (1,2) order by nombre", 1, True)
            ObjData = Nothing
            Call MuestraReporte()
        End If

    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Call MuestraReporte()
    End Sub

    Private Sub cmbTipoDocumento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoDocumento.SelectedIndexChanged
        Call MuestraReporte()
    End Sub

    Private Sub MuestraReporte()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pMisInformes @cmd=25, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @clienteid='" & cmbCliente.SelectedValue.ToString & "', @tipoid='" & cmbTipoDocumento.SelectedValue.ToString & "'")
        reporteGrid.DataSource = ds.Tables(0).DefaultView
        reporteGrid.DataBind()
        ObjData = Nothing
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
        ds = ObjData.FillDataSet("exec pMisInformes @cmd=25, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @clienteid='" & cmbCliente.SelectedValue.ToString & "', @tipoid='" & cmbTipoDocumento.SelectedValue.ToString & "'")
        reporteGrid.DataSource = ds.Tables(0)
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

End Class