Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Threading
Partial Public Class cortes
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

    Private Sub cortesList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles cortesList.ItemCommand
        Select Case e.CommandName
            Case "cmdDetalle"
                Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","c})
                Dim cajaid As Integer = 0
                Dim sucursalid As Integer = 0
                Try
                    cajaid = CInt(commandArgs(0))
                Catch ex As Exception
                End Try
                Try
                    sucursalid = CInt(commandArgs(1))
                Catch ex As Exception
                End Try
                Call VerDetalle(cajaid, sucursalid)
        End Select
    End Sub

    Private Sub cortesList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles cortesList.ItemDataBound

    End Sub

    Private Sub cortesList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles cortesList.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pCortesCaja @cmd=2, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaDesde.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaHasta.SelectedDate.Value.ToShortDateString & "', @userid='" & Session("userid").ToString & "'")
        cortesList.DataSource = ds
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
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pCortesCaja @cmd=2, @sucursalid='" & cmbSucursal.SelectedValue.ToString & "', @fechaini='" & calFechaDesde.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaHasta.SelectedDate.Value.ToShortDateString & "', @userid='" & Session("userid").ToString & "'")
        cortesList.DataSource = ds
        cortesList.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Call MuestraLista()
    End Sub

    Private Sub VerDetalle(ByVal corteid As Integer, ByVal sucursalid As Integer)
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pCortesCaja @cmd=3, @corteid='" & corteid.ToString & "', @sucursalid='" & sucursalid.ToString & "'")
        ObjData = Nothing

        If ds.Tables(0).Rows.Count > 0 Then
            panelDetalleCorte.Visible = True
            For Each row As DataRow In ds.Tables(0).Rows
                lblFolioCorteValue.Text = CStr(row("id"))
                lblSucursalCorteValue.Text = CStr(row("sucursal"))
                lblCajeroCorteValue.Text = CStr(row("usuario"))
                'lblTicketsCorteValue.Text = CStr(row("inicio")) & " - " & CStr(row("fin"))
                lblFechasCorteValue.Text = CStr(row("fechaini")) & " - " & CStr(row("fechafin"))

                Dim totalefectivoreportado As Decimal = 0
                Dim encaja As Decimal = 0
                Dim faltante As Decimal = 0
                Dim grantotal As Decimal = 0

                totalefectivoreportado = row("total20") + row("total50") + row("total100") + row("total200") + row("total500") + row("total1000") + row("total50c") + row("total1") + row("total2") + row("total5") + row("total10") + row("total20m")
                'encaja = row("efectivo") + row("monto_inicial") + row("entradas") - row("vales") - row("retiros")
                encaja = row("efectivo") + row("entradas") - row("vales") - row("retiros")
                faltante = (encaja - totalefectivoreportado)
                grantotal = row("efectivo") + row("tarjetas") + row("cheques") + row("depositos") + row("transferencias") + row("credito")

                lblEfectivoReportadoValue.Text = FormatCurrency(totalefectivoreportado, 2)
                lblTotalVentaEfectivoValue.Text = FormatCurrency(row("efectivo"), 2)
                lblInicioCajaValue.Text = FormatCurrency(row("monto_inicial"), 2)
                lblEntradasValue.Text = FormatCurrency(row("entradas"), 2)
                lblValesValue.Text = FormatCurrency(row("vales"), 2)
                lblRetirosValue.Text = FormatCurrency(row("retiros"), 2)
                lblTotalEfectivoValue.Text = FormatCurrency(encaja, 2)
                'lblFaltanteSobranteValue.Text = FormatCurrency(faltante, 2)
                If faltante <= 0 Then
                    lblFaltanteSobrante.Text = "Sobrante"
                    lblFaltanteSobranteValue.Text = FormatCurrency(faltante * -1, 2)
                    lblFaltanteSobranteValue.ForeColor = Drawing.Color.Green
                    lblFaltanteSobranteValue.Font.Bold = True
                Else
                    lblFaltanteSobrante.Text = "Faltante"
                    lblFaltanteSobranteValue.Text = FormatCurrency(faltante, 2)
                    lblFaltanteSobranteValue.ForeColor = Drawing.Color.Red
                    lblFaltanteSobranteValue.Font.Bold = True
                End If

                lblBilletes1000Value.Text = FormatNumber(row("billetes1000"), 0)
                lblBilletes500Value.Text = FormatNumber(row("billetes500"), 0)
                lblBilletes200Value.Text = FormatNumber(row("billetes200"), 0)
                lblBilletes100Value.Text = FormatNumber(row("billetes100"), 0)
                lblBilletes50Value.Text = FormatNumber(row("billetes50"), 0)
                lblBilletes20Value.Text = FormatNumber(row("billetes20"), 0)

                lblMonedas20Value.Text = FormatNumber(row("monedas20"), 0)
                lblMonedas10Value.Text = FormatNumber(row("monedas10"), 0)
                lblMonedas5Value.Text = FormatNumber(row("monedas5"), 0)
                lblMonedas2Value.Text = FormatNumber(row("monedas2"), 0)
                lblMonedas1Value.Text = FormatNumber(row("monedas1"), 0)
                lblMonedas50cValue.Text = FormatNumber(row("monedas50c"), 0)

                lblEfectivoValue.Text = FormatCurrency(row("efectivo"), 2)
                lblTarjetasValue.Text = FormatCurrency(row("tarjetas"), 2)
                lblChequesValue.Text = FormatCurrency(row("cheques"), 2)
                lblDepositosValue.Text = FormatCurrency(row("depositos"), 2)

                lblTotalVentaValue.Text = FormatCurrency(grantotal, 2)
                lblTotalGastosValue.Text = FormatCurrency(row("totalgastos"), 2)
                lblPromedioVentaValue.Text = FormatCurrency(row("promedio_venta"), 2)

            Next
        End If

    End Sub

End Class