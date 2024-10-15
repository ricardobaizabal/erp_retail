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
            fechaini.SelectedDate = Now
            fechafin.SelectedDate = Now
            lblSucursalesSeleccionadas.Text = ""
            hfSucursalesIds.Value = ""

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
        ds = ObjData.FillDataSet("exec pReporteTarjetas @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @sucursalid='" & hfSucursalesIds.Value & "'")
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

        ds = ObjData.FillDataSet("exec pReporteTarjetas @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @sucursalid='" & hfSucursalesIds.Value & "'")
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

    Protected Sub btnAgregarSucursal_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Verificar si se ha seleccionado una sucursal
        If cmbSucursal.SelectedValue IsNot Nothing Then
            ' Obtener el nombre de la sucursal seleccionada
            Dim sucursalSeleccionada As String = cmbSucursal.SelectedItem.Text
            Dim sucursalId As String = cmbSucursal.SelectedValue

            ' Verificar si ya hay sucursales seleccionadas para evitar duplicados
            If Not lblSucursalesSeleccionadas.Text.Contains(sucursalSeleccionada) Then
                ' Si ya hay sucursales seleccionadas, las concatenamos con la nueva
                If lblSucursalesSeleccionadas.Text <> "" Then
                    lblSucursalesSeleccionadas.Text &= ", " & sucursalSeleccionada
                    hfSucursalesIds.Value &= "," & sucursalId ' Concatenar los IDs
                Else
                    lblSucursalesSeleccionadas.Text = sucursalSeleccionada
                    hfSucursalesIds.Value = sucursalId ' Iniciar con el primer ID
                End If
            End If
        End If
        'Call MuestraReporte()
    End Sub

End Class