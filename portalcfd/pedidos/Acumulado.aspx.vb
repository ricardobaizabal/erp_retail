Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Web.UI.RadNumericTextBox
Imports System.IO
Imports System.Net.Mail
Imports Telerik.Reporting.Processing

Partial Public Class Acumulado
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
            ObjData.Catalogo(cmbsucrusal, "select id,nombre from tblsucursal where id <>4 order by nombre ", 0, 1)
            ObjData.Catalogo(cmbsucursal, "SELECT id,nombre FROM tblSucursal where id <>4 order by nombre ", 0, 1)
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
        Dim cmd As New SqlDataAdapter("EXEC pAcumuladoPedidos @sucursalid='" & cmbsucursal.SelectedValue & "'", conn)

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

    Function ObtenerProductosCotizados() As DataSet

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pPedidos @cmd=15, @clienteid='" & clienteid.ToString & "'", conn)

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

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/portalcfd/pedidos/pedidos.aspx")
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If InsertOrUpdate.Value = 0 Then

                Dim sql As String = ""
                sql = "EXEC pPedidos @cmd=4, @userid='" & Session("userid").ToString.ToString & "', @alumnoid='" & cmbAlumnos.Entries(0).Value & "', @comentarios='" & txtComentarios.Text & "', @estatusid='" & 1 & "'"

                Dim ObjData As New DataControl(1)
                pedidoID.Value = ObjData.RunSQLScalarQuery(sql)
                ObjData = Nothing

                panelRegistroPedido.Visible = True
                panelItemsRegistration.Visible = True
                panelProductosCotizados.Visible = False
                panelResumen.Visible = True

                pedidosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
                pedidosList.DataSource = ObtenerPedidos()
                pedidosList.DataBind()

            Else

                Dim sql As String = ""
                sql = "EXEC pPedidos @cmd=5, @pedidoid='" & pedidoID.Value & "', @userid='" & Session("userid").ToString.ToString & "', @comentarios='" & txtComentarios.Text & "', @estatusid='" & cmbestatus.SelectedValue & "'"

                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery(sql)
                ObjData = Nothing

                panelRegistroPedido.Visible = True
                panelItemsRegistration.Visible = True
                panelProductosCotizados.Visible = False
                panelResumen.Visible = True

                txtSearchItem.Focus()

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally
        End Try
        panelItemsRegistration.Visible = True
        panelProductosCotizados.Visible = False
    End Sub

    'Private Sub btnAgregarPedido_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarPedido.Click
    '    InsertOrUpdate.Value = 0
    '    panelRegistroPedido.Visible = True
    '    panelProductosCotizados.Visible = False
    '    panelItemsRegistration.Visible = False
    '    panelResumen.Visible = False
    'End Sub

    Private Sub pedidosList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles pedidosList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaPedido(e.CommandArgument)
                Call DisplayItems()
                Call CargaTotales()
                panelResumen.Visible = True
                gridResults.Visible = False
                itemsList.Visible = True
                txtSearchItem.Text = ""
                txtSearchItem.Focus()
                btnCancelSearch.Visible = False
            Case "cmdDelete"
                EliminaPedido(e.CommandArgument)
            Case "cmdDownload"
                Call DownloadPDF(e.CommandArgument)
            Case "cmdSend"
                Try
                    SendEmail(e.CommandArgument)
                    lblMensajeGuardar.Text = "Pedido enviado exitosamente"
                    lblMensajeGuardar.ForeColor = Drawing.Color.Green
                    Dim script As String = "<script type='text/javascript'>mensaje();</script>"
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", script, False)
                Catch ex As Exception
                    lblMensajeGuardar.Text = "Error: " & ex.Message.ToString
                    lblMensajeGuardar.ForeColor = Drawing.Color.Red
                    Dim script As String = "<script type='text/javascript'>mensaje();</script>"
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", script, False)
                End Try
        End Select
    End Sub

    Private Sub EditaPedido(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pPedidos @cmd=2, @pedidoid='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                clienteid = rs("clienteid")
                cmbsucrusal.SelectedValue = rs("sucursal")
                cmbestatus.SelectedValue = rs("estatusid")
                txtComentarios.Text = rs("comentarios")


                cmbAlumnos.Entries.Add(New AutoCompleteBoxEntry(rs("Alumno"), rs("Alumno")))

                'cmbAlumnos.Entries(0).Value = rs("Alumno")
                'Dim ObjData As New DataControl(1)
                'ObjData.Catalogo(cmbCliente, "select id, UPPER(isnull(nombre,'')) as nombre from tblAlumnos where isnull(borradobit,0)=0 order by nombre", rs("clienteid"))
                'ObjData = Nothing

                gridProductosCotizados.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
                gridProductosCotizados.DataSource = ObtenerProductosCotizados()
                gridProductosCotizados.DataBind()

                panelRegistroPedido.Visible = True
                panelItemsRegistration.Visible = True
                panelProductosCotizados.Visible = False
                panelResumen.Visible = True

                InsertOrUpdate.Value = 1
                pedidoID.Value = id

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub EliminaPedido(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pPedidos @cmd=3, @pedidoid='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Response.Redirect("~/portalcfd/pedidos/pedidos.aspx")

    End Sub

    Private Sub DownloadPDF(ByVal pedidoid As Long)
        Dim FilePath = Server.MapPath("~/portalcfd/pedidos/pedidos/Pedido_") & pedidoid.ToString & ".pdf"

        Call CargaTotalesPDF(pedidoid)
        GuardaPDF(GeneraPDF(pedidoid), FilePath)
        Dim FileName As String = Path.GetFileName(FilePath)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
        Response.Flush()
        Response.WriteFile(FilePath)
        Response.End()

    End Sub

    Private Sub SendEmail(ByVal pedidoid As Long)
        '
        '
        '   Obtiene datos de la persona
        '
        Dim mensaje As String = ""
        Dim razonsocial As String = ""
        Dim email_to As String = ""
        Dim email_from As String = ""
        '
        Dim conn As New SqlConnection(Session("conexion"))
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pPedidos @cmd=13, @pedidoid='" & pedidoid.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            razonsocial = rs("razon_social")
            email_from = rs("email_from")
            email_to = rs("email_to")
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
        '
        '
        mensaje = "<html><head></head><body style='font-size: 10pt; color: #000000; font-family: Verdana;'><br />"
        mensaje += "Estimado(a) Cliente, por este medio se le anexa la cotización solicitada.<br /><br />Gracias por su preferencia."

        mensaje += "<br /><br />"
        mensaje += "Atentamente.<br /><br />"
        mensaje += "<strong>" & razonsocial.ToString & "</strong><br /><br /></body></html>"

        Dim objMM As New MailMessage
        objMM.To.Add(email_to)
        objMM.From = New MailAddress(email_from, razonsocial)
        objMM.IsBodyHtml = True
        objMM.Priority = MailPriority.Normal
        objMM.Subject = razonsocial & " - Cotización No. " & pedidoid.ToString
        objMM.Body = mensaje
        '
        '   Agrega anexos
        '
        Dim FilePath = Server.MapPath("~/portalcfd/cotizaciones/cotizaciones/Cotizacion_") & pedidoid.ToString & ".pdf"

        If Not File.Exists(FilePath) Then
            Call CargaTotalesPDF(pedidoid)
            GuardaPDF(GeneraPDF(pedidoid), FilePath)
        End If

        Dim AttachPDF As Net.Mail.Attachment = New Net.Mail.Attachment(FilePath)
        '
        objMM.Attachments.Add(AttachPDF)
        '
        Dim SmtpMail As New SmtpClient
        Try
            Dim SmtpUser As New Net.NetworkCredential
            SmtpUser.UserName = "enviosweb@linkium.mx"
            SmtpUser.Password = "Link2020"
            SmtpMail.UseDefaultCredentials = False
            SmtpMail.Credentials = SmtpUser
            SmtpMail.Host = "smtp.linkium.mx"
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
            SmtpMail.Send(objMM)
        Catch ex As Exception
            '
            '
        Finally
            SmtpMail = Nothing
        End Try
        objMM = Nothing
        ''
    End Sub

    Private Function GeneraPDF(ByVal pedidoid As Long) As Telerik.Reporting.Report
        Dim conn As New SqlConnection(Session("conexion"))
        '
        Dim no_pedido As Integer = 0
        Dim tipo_cliente As Integer = 0
        Dim cliente As String = ""
        Dim prospecto As String = ""
        Dim fecha_pedido As String = ""
        Dim condiciones As String = ""
        '
        Dim ds As DataSet = New DataSet
        '
        Try
            '
            Dim cmd As New SqlCommand("EXEC pPedidos @cmd=11, @pedidoid='" & pedidoid.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            '
            If rs.Read Then
                no_pedido = rs("id")
                cliente = rs("cliente")
                fecha_pedido = rs("fecha_alta")
                condiciones = rs("condiciones")
                subtotal = rs("importe")
                iva = rs("iva")
                total = rs("total")
            End If
            rs.Close()
            '
        Catch ex As Exception
            Response.Write(ex.ToString)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
        '
        Dim reporte As New FormatosPDF.formato_pedido_trevisa
        '
        reporte.ReportParameters("plantillaId").Value = 5
        reporte.ReportParameters("pedidoId").Value = pedidoid
        reporte.ReportParameters("txtNoPedido").Value = pedidoid
        reporte.ReportParameters("txtClienteProspecto").Value = cliente
        reporte.ReportParameters("txtFechaPedido").Value = fecha_pedido
        reporte.ReportParameters("txtCondicionesPago").Value = condiciones
        '
        'reporte.ReportParameters("txtSubTotal").Value = FormatCurrency(subtotal, 2).ToString
        'reporte.ReportParameters("txtIva").Value = FormatCurrency(iva, 2).ToString
        reporte.ReportParameters("txtTotal").Value = FormatCurrency(total, 2).ToString
        '
        Return reporte
        ''
    End Function

    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub

    'Private Sub pedidosList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles pedidosList.ItemDataBound
    '    If TypeOf e.Item Is GridDataItem Then

    '        Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

    '        If e.Item.OwnerTableView.Name = "Pedidos" Then

    '            Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
    '            lnkdel.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea eliminar de la base de datos este pedido?');")

    '            Dim btnDownload As ImageButton = CType(e.Item.FindControl("btnDownload"), ImageButton)
    '            Dim imgSend As ImageButton = CType(e.Item.FindControl("imgSend"), ImageButton)

    '            Dim lblPartidas As Label = CType(e.Item.FindControl("lblPartidas"), Label)

    '            'If Convert.ToInt32(lblPartidas.Text) > 0 Then
    '            '    btnDownload.Visible = True
    '            '    imgSend.Visible = True
    '            'ElseC:\Users\desarollo2\Documents\Visual Studio 2008\Projects\erp_retail\portalcfd\configuracion\Sucursales.aspx
    '            '    btnDownload.Visible = False
    '            '    imgSend.Visible = False
    '            'End If
    '            '<!-- <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Descargar" UniqueName="Download">
    '            '                       <ItemTemplate>
    '            '                           <asp:ImageButton ID="btnDownload" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDownload" CausesValidation="false" ImageUrl="~/portalcfd/images/download.png" />
    '            '                       </ItemTemplate>
    '            '                       <HeaderStyle HorizontalAlign="Center" />
    '            '                       <ItemStyle HorizontalAlign="Center" />
    '            '                   </telerik:GridTemplateColumn>

    '            '                   <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Enviar" UniqueName="Send">
    '            '                       <ItemTemplate>
    '            '                           <asp:ImageButton id="imgSend" runat="server" ImageUrl="~/portalcfd/images/envelope.jpg" CommandArgument='<%# Eval("id") %>' CommandName="cmdSend" CausesValidation="false" />
    '            '                       </ItemTemplate>
    '            '                       <HeaderStyle HorizontalAlign="Center" />
    '            '                       <ItemStyle HorizontalAlign="Center" />
    '            '                   </telerik:GridTemplateColumn>
    '            '                   -->

    '        End If

    '    End If
    'End Sub

    Private Sub pedidosList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles pedidosList.NeedDataSource
        pedidosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        pedidosList.DataSource = ObtenerPedidos()
    End Sub

    Protected Sub txtSearchItem_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchItem.TextChanged
        gridResults.Visible = True
        itemsList.Visible = False
        Dim ObjData As New DataControl(1)
        gridResults.DataSource = ObjData.FillDataSet("exec [pPedidos] @cmd = 16, @txtSearch = '" & txtSearchItem.Text & "'")
        gridResults.DataBind()
        ObjData = Nothing
        txtSearchItem.Text = ""
        panelCombinaciones.Visible = False
        txtSearchItem.Focus()
        btnCancelSearch.Visible = True
    End Sub

    Protected Sub btnCancelSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelSearch.Click
        gridResults.Visible = False
        itemsList.Visible = True
        txtSearchItem.Text = ""
        txtSearchItem.Focus()
        btnCancelSearch.Visible = False
        panelCombinaciones.Visible = False
    End Sub

    Protected Sub gridResults_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gridResults.ItemCommand
        Select Case e.CommandName
            Case "cmdAdd"
                InsertItem(e.CommandArgument, e.Item)
                DisplayItems()
                Call CargaTotales()
                panelResumen.Visible = True
                gridResults.Visible = False
                itemsList.Visible = True
                txtSearchItem.Text = ""
                txtSearchItem.Focus()
                btnCancelSearch.Visible = False
            Case "cmdCombinaciones"
                ProductID.Value = e.CommandArgument
                panelCombinaciones.Visible = True
                Dim ObjData As New DataControl(1)
                ds = ObjData.FillDataSet("exec pProductoAlmacen @cmd=1, @productoid='" & e.CommandArgument.ToString & "'")
                ProductoCombinacionesList.DataSource = ds
                ProductoCombinacionesList.DataBind()
                ObjData = Nothing
        End Select
    End Sub

    Protected Sub InsertItem(ByVal id As Integer, ByVal item As GridItem)
        '
        ' Instancía objetos del grid
        '
        Dim lblCodigo As Label = DirectCast(item.FindControl("lblCodigo"), Label)

        Dim lblDescripcion As Label = DirectCast(item.FindControl("lblDescripcion"), Label)

        Dim txtQuantity As RadNumericTextBox = DirectCast(item.FindControl("txtCantidad"), RadNumericTextBox)
        Dim preciounitario As RadNumericTextBox = DirectCast(item.FindControl("txtCostoUnitario"), RadNumericTextBox)
        Dim importe As RadNumericTextBox = DirectCast(item.FindControl("txtImporte"), RadNumericTextBox)
        If Convert.ToDecimal(txtQuantity.Text) <= 0 Then
            MessageBox("Debes proporcionar una cantidad")
        Else
            '
            '   Agrega la partida
            '
            Dim ObjData As New DataControl(1)
            ObjData.RunSQLQuery("EXEC pPedidos @cmd=7, @pedidoid='" & pedidoID.Value.ToString & "', @codigo='" & lblCodigo.Text & "', @descripcion='" & lblDescripcion.Text & "', @cantidad='" & txtQuantity.Text & "', @productoid='" & id.ToString & "', @preciounit='" & preciounitario.Text & "'")
            ObjData = Nothing
            '
            pedidosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
            pedidosList.DataSource = ObtenerPedidos()
            pedidosList.DataBind()
            '
            DisplayItems()
        End If
        ''
    End Sub

    Private Sub DisplayItems()
        Dim ds As DataSet
        Dim ObjData As New DataControl(1)
        itemsList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        ds = ObjData.FillDataSet("EXEC pPedidos @cmd=8, @pedidoid='" & pedidoID.Value.ToString & "'")
        itemsList.DataSource = ds
        itemsList.DataBind()
        ObjData = Nothing
    End Sub

    Protected Sub itemsList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles itemsList.ItemCommand
        Select Case e.CommandName

            Case "cmdDelete"
                DeleteItem(e.CommandArgument)
                CargaTotales()

        End Select
    End Sub

    Private Sub DeleteItem(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("EXEC pPedidos @cmd=10, @partidaId ='" & id.ToString & "'", conn)

        conn.Open()

        cmd.ExecuteReader()

        conn.Close()

        Call DisplayItems()

    End Sub

    Protected Sub itemsList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles itemsList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Items" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('" & Resources.Resource.ItemsDeleteConfirmationMessage & "');")

            End If

        End If
    End Sub

    Protected Sub itemsList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles itemsList.NeedDataSource
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pPedidos @cmd=8, @pedidoid='" & pedidoID.Value.ToString & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            itemsList.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
            itemsList.DataSource = ds
            itemsList.DataBind()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            '
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub

    Private Sub CargaTotales()
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("exec pPedidos @cmd=9, @pedidoid='" & pedidoID.Value.ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                subtotal = rs("subtotal")
                iva = rs("iva")
                total = rs("total")
                'lblSubTotalValue.Text = FormatCurrency(subtotal, 2).ToString
                'lblIVAValue.Text = FormatCurrency(iva, 2).ToString
                lblTotalValue.Text = FormatCurrency(total, 2).ToString
            End If
        Catch ex As Exception
            '
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Private Sub CargaTotalesPDF(ByVal pedidoid As Integer)
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("exec pPedidos @cmd=9, @pedidoid='" & pedidoid.ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                'importe = rs("importe")
                'descuento = rs("descuento")
                subtotal = rs("subtotal")
                iva = rs("iva")
                total = rs("total")
            End If
        Catch ex As Exception
            '
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        panelRegistroPedido.Visible = False
        panelItemsRegistration.Visible = False
        panelProductosCotizados.Visible = False
        panelResumen.Visible = False
        gridResults.Visible = False
        itemsList.Visible = False
        btnCancelSearch.Visible = False
        txtSearchItem.Text = ""
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

    Private Function Num2Text(ByVal value As Decimal) As String
        Select Case value
            Case 0 : Num2Text = "CERO"
            Case 1 : Num2Text = "UN"
            Case 2 : Num2Text = "DOS"
            Case 3 : Num2Text = "TRES"
            Case 4 : Num2Text = "CUATRO"
            Case 5 : Num2Text = "CINCO"
            Case 6 : Num2Text = "SEIS"
            Case 7 : Num2Text = "SIETE"
            Case 8 : Num2Text = "OCHO"
            Case 9 : Num2Text = "NUEVE"
            Case 10 : Num2Text = "DIEZ"
            Case 11 : Num2Text = "ONCE"
            Case 12 : Num2Text = "DOCE"
            Case 13 : Num2Text = "TRECE"
            Case 14 : Num2Text = "CATORCE"
            Case 15 : Num2Text = "QUINCE"
            Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
            Case 20 : Num2Text = "VEINTE"
            Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
            Case 30 : Num2Text = "TREINTA"
            Case 40 : Num2Text = "CUARENTA"
            Case 50 : Num2Text = "CINCUENTA"
            Case 60 : Num2Text = "SESENTA"
            Case 70 : Num2Text = "SETENTA"
            Case 80 : Num2Text = "OCHENTA"
            Case 90 : Num2Text = "NOVENTA"
            Case Is < 100 : Num2Text = Num2Text(Int(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
            Case 100 : Num2Text = "CIEN"
            Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
            Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Int(value \ 100)) & "CIENTOS"
            Case 500 : Num2Text = "QUINIENTOS"
            Case 700 : Num2Text = "SETECIENTOS"
            Case 900 : Num2Text = "NOVECIENTOS"
            Case Is < 1000 : Num2Text = Num2Text(Int(value \ 100) * 100) & " " & Num2Text(value Mod 100)
            Case 1000 : Num2Text = "MIL"
            Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
            Case Is < 1000000 : Num2Text = Num2Text(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
            Case 1000000 : Num2Text = "UN MILLON"
            Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
            Case Is < 1000000000000.0# : Num2Text = Num2Text(Int(value / 1000000)) & " MILLONES "
                If (value - Int(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000) * 1000000)
            Case 1000000000000.0# : Num2Text = "UN BILLON"
            Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : Num2Text = Num2Text(Int(value / 1000000000000.0#)) & " BILLONES"
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
        End Select
    End Function

    Private Sub gridProductosCotizados_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gridProductosCotizados.NeedDataSource
        gridProductosCotizados.MasterTableView.NoMasterRecordsText = "No se encontraron cotizaciones para mostrar"
        gridProductosCotizados.DataSource = ObtenerProductosCotizados()
    End Sub

    Private Sub btnFinalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        Response.Redirect("pedidos.aspx", False)
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Private Sub cmbAlumnos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAlumnos.Load
        VariablesUniversales.idcmbsucursal = cmbsucrusal.SelectedValue
        Dim dt As New DataTable
        Dim ObjData As New DataControl(1)
        dt = ObjData.FillDataTable("exec pAlumnos @cmd=6,@sucursalid=" & VariablesUniversales.idcmbsucursal)
        cmbAlumnos.DataSource = dt
        cmbAlumnos.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub cmbsucrusal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbsucrusal.SelectedIndexChanged
        VariablesUniversales.idcmbsucursal = cmbsucrusal.SelectedValue
    End Sub

    Sub txtCantidad_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each dataItem As Telerik.Web.UI.GridDataItem In gridResults.MasterTableView.Items

            Dim txtCantidad As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCantidad"), RadNumericTextBox)
            Dim txtCostoUnitario As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCostoUnitario"), RadNumericTextBox)
            Dim txtImporte As RadNumericTextBox = DirectCast(dataItem.FindControl("txtImporte"), RadNumericTextBox)

            txtImporte.Text = txtCantidad.Text * txtCostoUnitario.Text

        Next
    End Sub

    Private Sub gridResults_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gridResults.ItemDataBound
        Select Case e.Item.ItemType
            Case GridItemType.Item, GridItemType.AlternatingItem
                Dim txtCantidad As RadNumericTextBox = DirectCast(e.Item.FindControl("txtCantidad"), RadNumericTextBox)
                Dim txtCostoUnitario As RadNumericTextBox = DirectCast(e.Item.FindControl("txtCostoUnitario"), RadNumericTextBox)
                Dim txtImporte As RadNumericTextBox = DirectCast(e.Item.FindControl("txtImporte"), RadNumericTextBox)

                Dim txtDocumento As System.Web.UI.WebControls.TextBox = DirectCast(e.Item.FindControl("txtDocumento"), System.Web.UI.WebControls.TextBox)
                Dim txtComentario As System.Web.UI.WebControls.TextBox = DirectCast(e.Item.FindControl("txtComentario"), System.Web.UI.WebControls.TextBox)
                Dim btnAdd As System.Web.UI.WebControls.ImageButton = DirectCast(e.Item.FindControl("btnAdd"), System.Web.UI.WebControls.ImageButton)
                Dim lnkCombinaciones As System.Web.UI.WebControls.LinkButton = DirectCast(e.Item.FindControl("lnkCombinaciones"), System.Web.UI.WebControls.LinkButton)


                txtCantidad.Text = "1"
                txtCostoUnitario.Text = e.Item.DataItem("preciounitario")



                If e.Item.DataItem("presentacionid") = 0 Then

                    btnAdd.Visible = True
                    lnkCombinaciones.Visible = False
                Else
                    lnkCombinaciones.Visible = True
                    btnAdd.Visible = False
                End If
        End Select
    End Sub



    Private Sub ProductoCombinacionesList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles ProductoCombinacionesList.ItemCommand
        Select Case e.CommandName
            Case "cmdAdd"
                Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {"|"c})

                Call InsertItemCombinacion(commandArgs(0), commandArgs(1), e.Item, commandArgs(1))
        End Select
    End Sub

    Private Sub ProductoCombinacionesList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles ProductoCombinacionesList.ItemDataBound
        Select Case e.Item.ItemType
            Case GridItemType.Item, GridItemType.AlternatingItem
                Dim txtCantidad As RadNumericTextBox = DirectCast(e.Item.FindControl("txtCantidad"), RadNumericTextBox)
                Dim txtCostoUnitario As RadNumericTextBox = DirectCast(e.Item.FindControl("txtCostoUnitario"), RadNumericTextBox)
                Dim txtImporte As RadNumericTextBox = DirectCast(e.Item.FindControl("txtImporte"), RadNumericTextBox)
                Dim cmbSucursal As System.Web.UI.WebControls.DropDownList = DirectCast(e.Item.FindControl("cmbSucursal"), System.Web.UI.WebControls.DropDownList)

                txtCantidad.Text = "1"
                txtCostoUnitario.Text = e.Item.DataItem("costo_estandar")
                cmbSucursal.SelectedValue = cmbsucrusal.SelectedValue


                cmbSucursal.Enabled = False
                Dim objCat As New DataControl(1)
                objCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0)
                objCat = Nothing

        End Select
    End Sub

    Private Sub ProductoCombinacionesList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles ProductoCombinacionesList.NeedDataSource
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pProductoAlmacen @cmd=1, @productoid='" & ProductID.Value.ToString & "'")
        ProductoCombinacionesList.DataSource = ds
        ObjData = Nothing
    End Sub

    Sub txtCantidadCombinacion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each dataItem As Telerik.Web.UI.GridDataItem In ProductoCombinacionesList.MasterTableView.Items

            Dim txtCantidad As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCantidad"), RadNumericTextBox)
            Dim txtCostoUnitario As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCostoUnitario"), RadNumericTextBox)
            Dim txtImporte As RadNumericTextBox = DirectCast(dataItem.FindControl("txtImporte"), RadNumericTextBox)

            txtImporte.Text = txtCantidad.Text * txtCostoUnitario.Text

        Next
    End Sub

    Private Sub InsertItemCombinacion(ByVal combinacionid As Long, ByVal productoid As Long, ByVal item As GridItem, ByVal codigo As Integer)
        '
        '   Instancia elementos
        '
        Dim lblCodigo As Label = DirectCast(item.FindControl("lblCodigo"), Label)
        Dim lblDescripcion As Label = DirectCast(item.FindControl("lblDescripcion"), Label)
        Dim lblProducto As Label = DirectCast(item.FindControl("lblProducto"), Label)
        Dim txtCantidad As RadNumericTextBox = DirectCast(item.FindControl("txtCantidad"), RadNumericTextBox)
        Dim txtCostoUnitario As RadNumericTextBox = DirectCast(item.FindControl("txtCostoUnitario"), RadNumericTextBox)


        '
        '   Agrega entrada
        '
        If cmbsucrusal.SelectedValue > 0 Then
            Dim ObjData As New DataControl(1)
            ObjData.RunSQLQuery("EXEC pPedidos @cmd=7, @pedidoid='" & pedidoID.Value.ToString & "', @codigo='" & lblCodigo.Text & "',@descripcion='" & lblProducto.Text & " " & lblDescripcion.Text & "', @productoid='" & codigo & "', @cantidad='" & txtCantidad.Text & "', @preciounit='" & txtCostoUnitario.Text & "', @combinacion='" & lblCodigo.Text & "', @validarcombinacion='" & 1 & "'")
            ObjData = Nothing
        End If
        '
        panelCombinaciones.Visible = False
        'txtSearch.Text = ""
        ' txtSearch.Focus()
        '
        gridResults.Visible = False
        itemsList.Visible = True
        ''
        DisplayItems()
    End Sub



End Class