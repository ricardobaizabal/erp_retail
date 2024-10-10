Imports System.Data
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI
Public Class ventas_familia
    Inherits System.Web.UI.Page
    Private ds As DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle
        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Reporte de ventas por presentaciones"

        If Not IsPostBack Then

            fechaini.SelectedDate = Now
            fechafin.SelectedDate = Now

            Dim ObjCat As New DataControl(1)
            ObjCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0, True)
            ObjCat.Catalogo(cmbFamilia, "select id, nombre from tblFamilia where isnull(borradoBit,0)=0 order by nombre", 0, True)
            ObjCat.Catalogo(cmbSubFamilia, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 order by nombre", 0, True)
            ObjCat = Nothing

            Call MuestraReporte()

        End If
    End Sub

    Private Sub reporteGridFamilia_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles reporteGridFamilia.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteVentasFamilia2 @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @familiaid='" & cmbFamilia.SelectedValue.ToString & "', @subfamiliaid='" & cmbSubFamilia.SelectedValue.ToString & "', @descripcion='" & txtProducto.Text.ToString & "'")
        reporteGridFamilia.DataSource = ds.Tables(0).DefaultView
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Call MuestraReporte()
    End Sub

    Private Sub MuestraReporte()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteVentasFamilia2 @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @familiaid='" & cmbFamilia.SelectedValue.ToString & "', @subfamiliaid='" & cmbSubFamilia.SelectedValue.ToString & "', @descripcion='" & txtProducto.Text.ToString & "'")
        reporteGridFamilia.DataSource = ds.Tables(0).DefaultView
        reporteGridFamilia.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub cmbFamilia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFamilia.SelectedIndexChanged
        If cmbFamilia.SelectedValue = 0 Then
            cmbSubFamilia.Enabled = False
            cmbSubFamilia.SelectedValue = 0
        Else
            cmbSubFamilia.Enabled = True
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbSubFamilia, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 and familiaid='" & cmbFamilia.SelectedValue & "' order by nombre", 0)
            objCat = Nothing
        End If
    End Sub

End Class