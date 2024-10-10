Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Threading
Public Class entradas
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle
        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Entradas de Inventario"

        If Not IsPostBack Then
            calFechaDesde.SelectedDate = Now()
            calFechaHasta.SelectedDate = Now()
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0, True)
            objCat = Nothing

            Session("filtroentradaid") = 0
            Session("filtrosucursalid") = 0

        End If

    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Call MuestraLista()
    End Sub

    Private Sub reporteGrid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles reporteGrid.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pEntradaInventario @cmd=1, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaDesde.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaHasta.SelectedDate.Value.ToShortDateString & "'")
        reporteGrid.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub MuestraLista()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        panelDetalleEntradas.Visible = False
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pEntradaInventario @cmd=1, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaDesde.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaHasta.SelectedDate.Value.ToShortDateString & "'")
        reporteGrid.DataSource = ds
        reporteGrid.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub VerDetalle(ByVal entradaid As String, ByVal sucursalid As String)

        Session("filtroentradaid") = entradaid
        Session("filtrosucursalid") = sucursalid

        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pEntradaInventario @cmd=2, @id='" & entradaid.ToString & "', @sucursalid='" & sucursalid.ToString & "'")
        detalleGrid.DataSource = ds
        detalleGrid.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub reporteGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles reporteGrid.ItemCommand
        Select Case e.CommandName
            Case "cmdDetalle"
                panelDetalleEntradas.Visible = True
                Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","c})
                Dim entradaid As String = commandArgs(0)
                Dim sucursalid As String = commandArgs(1)
                Call VerDetalle(entradaid, sucursalid)
        End Select
    End Sub

    Private Sub detalleGrid_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles detalleGrid.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    e.Item.Cells(8).Text = ds.Tables(0).Compute("sum(cantidad)", "").ToString
                    e.Item.Cells(8).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(8).Font.Bold = True
                    '
                    e.Item.Cells(9).Text = FormatCurrency(ds.Tables(0).Compute("sum(importe)", ""), 2).ToString
                    e.Item.Cells(9).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(9).Font.Bold = True
                End If
        End Select
    End Sub

    Private Sub detalleGrid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles detalleGrid.NeedDataSource
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pEntradaInventario @cmd=2, @id='" & Session("filtroentradaid").ToString & "', @sucursalid='" & Session("filtrosucursalid").ToString & "'")
        detalleGrid.DataSource = ds
        ObjData = Nothing
    End Sub

End Class