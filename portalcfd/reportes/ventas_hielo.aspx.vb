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
            Call MuestraVentas(sucursalIdString)
        End If
    End Sub

    Private Sub MuestraVentas(sucursalIdString As String)
        '
        grdVentas.DataSource = Nothing
        grdVentas.DataBind()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        Dim sql As String = ""
        sql = "exec pVentasHielo @cmd=1, @sucursalid='" & sucursalIdString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "'"
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
        ' Si es un postback, obtener los IDs seleccionados después de que el usuario ha hecho alguna selección
        Dim selectedIds As New List(Of String)

        ' Recorre los items seleccionados en el RadComboBox
        For Each item As RadComboBoxItem In cmbSucursal.CheckedItems
            selectedIds.Add(item.Value)
        Next

        ' Unir los IDs en una cadena separada por comas
        Dim sucursalIdString As String = String.Join(",", selectedIds)

        ' Llamar a MuestraReporte con los IDs seleccionados
        Call MuestraVentas(sucursalIdString)
    End Sub

    'Private Sub grdVentas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdVentas.NeedDataSource
    '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

    '    Dim ObjData As New DataControl(1)
    '    Dim sql As String = ""
    '    sql = "exec pVentasHielo @cmd=1, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & sucursalIdString & "'"
    '    grdVentas.DataSource = ds
    '    ObjData = Nothing

    '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
    'End Sub

End Class