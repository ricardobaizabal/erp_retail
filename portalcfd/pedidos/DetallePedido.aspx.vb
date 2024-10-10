Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Web.UI.RadNumericTextBox
Imports System.IO
Imports System.Net.Mail
Imports Telerik.Reporting.Processing
Imports System.Threading
Imports System.Globalization

Partial Public Class DetallePedido
    Inherits System.Web.UI.Page
    Private importe As Decimal = 0
    Private descuento As Decimal = 0
    Private subtotal As Decimal = 0
    Private iva As Decimal = 0
    Private total As Decimal = 0
    Private clienteid As Long = 0
    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            fechaini.SelectedDate = Now
            fechafin.SelectedDate = Now
            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(cmbfamilia, "select id,nombre from tblfamilia", 0, 1)
            ObjData.Catalogo(cmbsubfamilia, "select id,nombre from tblsubfamilia where familiaid =" & cmbfamilia.SelectedValue, 0, 1)
            ObjData.Catalogo(cmbsucursal, "select id,nombre from tblSucursal where id <>4 order by nombre ", 0, 1)
            pedidosList.DataSource = ObtenerPedidos()
            pedidosList.DataBind()
            ObjData = Nothing
            '
            lblTotalValue.Text = FormatCurrency(total, 4).ToString
            '
        End If
    End Sub

    Function ObtenerPedidos() As DataSet

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

        Dim conn As New SqlConnection(Session("conexion"))
        Dim sql As String = "EXEC pReportePedidoDetalle @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @familia='" & cmbfamilia.SelectedValue & "', @subfamilia='" & cmbsubfamilia.SelectedValue & "', @sucursalid='" & cmbsucursal.SelectedValue & "'"

        If fechaini.SelectedDate Is Nothing Then
            sql = "EXEC pReportePedidoDetalle @familia='" & cmbfamilia.SelectedValue & "', @subfamilia='" & cmbsubfamilia.SelectedValue & "', @sucursalid='" & cmbsucursal.SelectedValue & "'"
        End If

        Dim cmd As New SqlDataAdapter(sql, conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")

        Return ds

    End Function

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        panelRegistroPedido.Visible = False
        panelItemsRegistration.Visible = False
        panelProductosCotizados.Visible = False
        panelResumen.Visible = False
        gridResults.Visible = False
        itemsList.Visible = False
        btnCancelSearch.Visible = False
        ' txtSearchItem.Text = ""
        pedidosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        pedidosList.DataSource = ObtenerPedidos()
        pedidosList.DataBind()
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        panelRegistroPedido.Visible = False
        panelItemsRegistration.Visible = False
        panelProductosCotizados.Visible = False
        panelResumen.Visible = False
        gridResults.Visible = False
        itemsList.Visible = False
        btnCancelSearch.Visible = False
        txtSearchItem.Text = ""
        fechaini.Clear()
        fechafin.Clear()
        pedidosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        pedidosList.DataSource = ObtenerPedidos()
        pedidosList.DataBind()
    End Sub

    Private Sub cmbfamilia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbfamilia.SelectedIndexChanged
        Dim ObjData As New DataControl(1)
        ObjData.Catalogo(cmbsubfamilia, "select id,nombre from tblsubfamilia where familiaid =" & cmbfamilia.SelectedValue, 0)
        ObjData = Nothing
    End Sub

End Class