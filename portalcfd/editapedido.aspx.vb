Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports Telerik.Web.UI
Imports Telerik.Reporting.Processing
Imports System.IO

Partial Public Class editapedido
    Inherits System.Web.UI.Page

    Dim datos As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblPedidoError.Visible = False

        If Not IsPostBack Then
            Dim dTable As New DataTable()
            productosList.DataSource = dTable
            pedidodetallelist.DataSource = dTable

            Call DetallePedido()

            If estatusId.Value = 4 Then 'No mostrar nada si el estatus es cancelado
                Response.Redirect("~/portalcfd/pedidos.aspx?id=" & Request("id").ToString())
            End If

            If estatusId.Value > 1 Then 'Deshabilitar edición de pedido si su estatus no es abierto
                lblBuscar.Visible = False
                txtSearch.Visible = False
                btnSearch.Visible = False
                btnCancelarBusqueda.Visible = False
                btnColocarPedido.Visible = False
            Else
                lblBuscar.Visible = True
                txtSearch.Visible = True
                btnSearch.Visible = True
                btnCancelarBusqueda.Visible = True
            End If

            Call MuestraPedido()
            '
            ' 'btnAutorizar.Attributes.Add("onclick", "javascript:return confirm('Va a autorizar un pedido y enviarlo a almacén. ¿Desea continuar?');")
            ' 'btnPack.Attributes.Add("onclick", "javascript:return confirm('Va a marcar como empaquetado el producto. ¿Desea continuar?');")
            '
            Select Case EstatusId.Value
                Case 1  '   Abierto
                    'btnAutorizar.Enabled = False
                    'btnRechazar.Enabled = False
                    'btnPack.Enabled = False
                    'btnSent.Enabled = False
                    'btnReactivar.Enabled = False
                Case 2, 3  '   En proceso
                    btnColocarPedido.Enabled = False
                    'btnPack.Enabled = False
                    btnCancel.Enabled = False
                    'btnSent.Enabled = False
                    'btnReactivar.Enabled = False
                Case 5  ' Autorizado
                    btnColocarPedido.Enabled = False
                    'btnAutorizar.Enabled = False
                    'btnRechazar.Enabled = False
                    btnCancel.Enabled = False
                    'btnSent.Enabled = False
                    'btnReactivar.Enabled = False
                Case 6 'Empaquetado
                    btnColocarPedido.Enabled = False
                    'btnAutorizar.Enabled = False
                    'btnRechazar.Enabled = False
                    'btnPack.Enabled = False
                    btnCancel.Enabled = False
                    btnSearch.Enabled = False
                    'btnSent.Enabled = False
                    'btnReactivar.Enabled = False
                Case 7 'Facturado
                    btnColocarPedido.Enabled = False
                    'btnAutorizar.Enabled = False
                    'btnRechazar.Enabled = False
                    'btnPack.Enabled = False
                    btnCancel.Enabled = False
                    btnSearch.Enabled = False
                    'btnSent.Enabled = True
                    'btnReactivar.Enabled = False
                Case 10 'Rechazado
                    'btnAutorizar.Enabled = False
                    'btnRechazar.Enabled = False
                    btnCancel.Enabled = False
                    btnColocarPedido.Enabled = False
                    'btnPack.Enabled = False
                    btnSearch.Enabled = False
                    'btnSent.Enabled = False
                    'btnReactivar.Enabled = True
                Case Else
                    'btnAutorizar.Enabled = False
                    'btnRechazar.Enabled = False
                    btnCancel.Enabled = False
                    btnColocarPedido.Enabled = False
                    'btnPack.Enabled = False
                    btnSearch.Enabled = False
                    'btnSent.Enabled = False
                    'btnReactivar.Enabled = False
            End Select
            '
            'Select Case Session("perfilid")
            '    Case 3, 5
            '        'btnAutorizar.Enabled = False
            '        'btnPack.Enabled = False
            '        'btnSent.Enabled = False
            '        'btnRechazar.Enabled = False
            '        'btnReactivar.Enabled = False
            'End Select
            '
        End If
    End Sub

    Private Sub DetallePedido()
        Dim ObjData As New DataControl(1)
        Dim dsData As New DataSet()
        Try
            dsData = ObjData.FillDataSet("exec pPedido @cmd=5, @pedidoid=" & Request("id").ToString)
            If Not IsNothing(dsData) Then
                For Each row As DataRow In dsData.Tables(0).Rows
                    ClienteId.Value = row("clienteid")
                    SucursalId = row("clienteid")
                    EstatusId.Value = row("estatusid")
                    lblRazonsocial.Text = row("cliente")
                    lblSucursal.Text = row("sucursal")
                    lblOrdenCompra.Text = row("orden_compra")
                Next
            End If
        Catch ex As Exception
            lblMensaje.Text = "Error: " + ex.ToString()
        End Try
        ObjData = Nothing
    End Sub

    Private Sub MuestraPedido()
        Dim ObjData As New DataControl(1)
        datos = ObjData.FillDataSet("exec pPedido @cmd=8, @pedidoid=" & Request("id"))
        If Not IsNothing(datos) Then
            pedidodetallelist.DataSource = datos
            pedidodetallelist.DataBind()
        End If
        ObjData = Nothing
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Call MuestraProductos()
        panel1.Visible = True
        btnAgregaConceptos.Visible = True
        lblMensaje.Text = ""
    End Sub

    Private Sub MuestraProductos()
        Dim ObjData As New DataControl(1)
        Dim dsData As New DataSet()
        dsData = ObjData.FillDataSet("exec pPedido @cmd=22, @txtSearch='" & txtSearch.Text & "', @clienteid='" & ClienteId.Value.ToString & "', @sucursalid='" & SucursalId.Value.ToString & "'")
        If Not IsNothing(dsData) Then
            productosList.DataSource = dsData
            productosList.DataBind()
        End If
        ObjData = Nothing
    End Sub

    Private Sub btnCancelarBusqueda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarBusqueda.Click

    End Sub

End Class