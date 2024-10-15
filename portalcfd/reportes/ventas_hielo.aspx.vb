Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Threading
Imports System.Xml
Imports System.Net.Mail
Imports ThoughtWorks.QRCode
Imports ThoughtWorks.QRCode.Codec
Imports Telerik.Reporting.Processing
Imports System.Web.Services.Protocols
Public Class ventas_hielo
    Inherits System.Web.UI.Page

    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbCliente, "exec pMisClientes @cmd=1", 0)
            objCat.Catalogo(cmbSucursal, "exec pCatalogos @cmd=11", 0)
            objCat = Nothing
            '
            fechaini.SelectedDate = Now
            fechafin.SelectedDate = Now
            '
            Call MuestraVentas()
            '
        End If
    End Sub

    Private Sub MuestraVentas()
        '
        grdVentas.DataSource = Nothing
        grdVentas.DataBind()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        Dim sql As String = ""
        sql = "exec pVentas @cmd=2, @clienteid='" & cmbCliente.SelectedValue.ToString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @folio='" & txtTicket.Text & "', @producto='" & txtProducto.Text & "'"
        ds = ObjData.FillDataSet(sql)
        grdVentas.DataSource = ds
        grdVentas.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Call MuestraVentas()
    End Sub

    Private Sub grdVentas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdVentas.NeedDataSource
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

        Dim ObjData As New DataControl(1)
        Dim sql As String = ""
        sql = "exec pVentas @cmd=2, @clienteid='" & cmbCliente.SelectedValue.ToString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @folio='" & txtTicket.Text & "', @producto='" & txtProducto.Text & "'"
        ds = ObjData.FillDataSet(sql)
        grdVentas.DataSource = ds
        ObjData = Nothing

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
    End Sub

End Class