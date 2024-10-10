Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Web.UI.RadNumericTextBox
Imports System.IO
Imports System.Net.Mail
Imports Telerik.Reporting.Processing

Partial Public Class DetalleVenta
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
            Dim ObjData As New DataControl(1)
            'ObjData.Catalogo(cmbfamilia, "select id,nombre from tblfamilia", 0)
            'ObjData.Catalogo(cmbsubfamilia, "select id,nombre from tblsubfamilia where familiaid =" & cmbfamilia.SelectedValue, 0)
            ObjData.Catalogo(cmbsucursal, "SELECT id,nombre FROM tblSucursal where id <> 4 order by nombre", 0, 1)
            pedidosList.DataSource = ObtenerPedidos()
            pedidosList.DataBind()
            ObjData = Nothing
            '
            lblTotalValue.Text = FormatCurrency(total, 4).ToString
            '
        End If
    End Sub

    Function ObtenerPedidos() As DataSet

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pPedidos @cmd=29" & ",@sucursalid=" & cmbsucursal.SelectedValue, conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return ds

    End Function

    Private Sub pedidosList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles pedidosList.NeedDataSource
        pedidosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        pedidosList.DataSource = ObtenerPedidos()
    End Sub

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
        'txtSearch.Text = ""
        pedidosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        pedidosList.DataSource = ObtenerPedidos()
        pedidosList.DataBind()
    End Sub

End Class