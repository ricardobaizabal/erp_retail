Imports System.Data
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI
Imports System.Data.SqlClient

Partial Public Class tarjetas
    Inherits System.Web.UI.Page
    Private ds As DataSet


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle
        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Reporte de ventas con tarjetas"

        If Not IsPostBack Then
            ' Establecer las fechas iniciales en el control de fechas
            fechaini.SelectedDate = Now
            fechafin.SelectedDate = Now

            ' Crear instancia de DataControl
            Dim ObjCat As New DataControl(1)

            ' Cargar todas las sucursales en el RadComboBox, ordenadas alfabéticamente por nombre
            ObjCat.FillRadComboBox(cmbSucursal, "SELECT id, nombre FROM tblSucursal ORDER BY nombre")

            ObjCat = Nothing
        Else
            ' Si es un postback, obtener los IDs seleccionados después de que el usuario ha hecho alguna selección
            Dim selectedIds As New List(Of String)

            ' Recorre los items seleccionados en el RadComboBox
            For Each item As RadComboBoxItem In cmbSucursal.CheckedItems
                selectedIds.Add(item.Value)
            Next

            ' Unir los IDs en una cadena separada por comas
            Dim sucursalIdString As String = String.Join(",", selectedIds)

            ' Llamar a MuestraReporte con los IDs seleccionados
            Call MuestraReporte(sucursalIdString)
        End If
    End Sub


    'Private Sub reporteGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles reporteGrid.NeedDataSource
    '    '
    '    reporteGrid.CurrentPageIndex = 0
    '    '
    '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
    '    '

    '    Dim ObjData As New DataControl(1)
    '    ds = ObjData.FillDataSet("exec pReporteTarjetas @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @sucursalid='" & sucursalIdString & "'")
    '    reporteGrid.DataSource = ds.Tables(0).DefaultView
    '    ObjData = Nothing
    '    '
    '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
    '    '
    'End Sub

    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        ' Si es un postback, obtener los IDs seleccionados después de que el usuario ha hecho alguna selección
        Dim selectedIds As New List(Of String)

        ' Recorre los items seleccionados en el RadComboBox
        For Each item As RadComboBoxItem In cmbSucursal.CheckedItems
            selectedIds.Add(item.Value)
        Next

        ' Unir los IDs en una cadena separada por comas
        Dim sucursalIdString As String = String.Join(",", selectedIds)

        ' Llamar a MuestraReporte con los IDs seleccionados
        Call MuestraReporte(sucursalIdString)
    End Sub

    Private Sub MuestraReporte(sucursalIdString As String)
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '

        Dim ObjData As New DataControl(1)

        ds = ObjData.FillDataSet("exec pReporteTarjetas @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @sucursalid='" & sucursalIdString & "'")
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
    'Private Sub CargarSucursales()
    '    ' Aquí debes obtener los datos de tu tabla de sucursales
    '    Dim query As String = "select id, nombre from tblSucursal order by nombre"
    '    Dim connString As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
    '    Using conn As New SqlConnection(connString)
    '        Dim cmd As New SqlCommand(query, conn)
    '        conn.Open()
    '        Dim reader As SqlDataReader = cmd.ExecuteReader()
    '        cmbSucursal.DataSource = reader
    '        cmbSucursal.DataTextField = "SucursalNombre"  ' Lo que se muestra
    '        cmbSucursal.DataValueField = "SucursalID"     ' El valor del campo (ID)
    '        cmbSucursal.DataBind()
    '    End Using
    'End Sub



End Class