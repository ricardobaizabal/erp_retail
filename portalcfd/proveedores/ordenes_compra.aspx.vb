Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI
Imports Telerik.Reporting.Processing
Imports System.IO
Partial Public Class ordenes_compra
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fechaini.SelectedDate = Date.Now
            fechafin.SelectedDate = Date.Now

            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(proveedorid, "select id, razonsocial as nombre from tblMisProveedores order by razonsocial", 0, True)
            ObjData = Nothing

            If Not Session("proveedorid") Is Nothing Then
                proveedorid.SelectedValue = Session("proveedorid")
            End If

            If Not Session("fecini") Is Nothing Then
                fechaini.SelectedDate = Session("fecini")
            End If

            If Not Session("fecfin") Is Nothing Then
                fechafin.SelectedDate = Session("fecfin")
            End If

            If Not Session("no_orden") Is Nothing Then
                txtNoOrden.Text = Session("no_orden").ToString
            End If

            Call MuestraOrdenes()

        End If
    End Sub

    Private Sub btnAddOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddOrder.Click
        Response.Redirect("~/portalcfd/proveedores/agregarorden.aspx")
    End Sub

    Private Sub MuestraOrdenes()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Session("proveedorid") = proveedorid.SelectedValue
        Session("fecini") = CDate(fechaini.SelectedDate.Value.ToShortDateString)
        Session("fecfin") = CDate(fechafin.SelectedDate.Value.ToShortDateString)
        Session("no_orden") = txtNoOrden.Text
        '
        Dim ObjData As New DataControl(1)
        Dim sql As String = ""
        If txtNoOrden.Text.ToString.Length > 0 Then
            sql = "exec pOrdenCompra @cmd=1, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & Session("proveedorid").ToString & "', @no_orden='" & Session("no_orden").ToString & "'"
        Else
            sql = "exec pOrdenCompra @cmd=1, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & Session("proveedorid").ToString & "'"
        End If
        '
        ds = ObjData.FillDataSet(sql)
        ordersList.DataSource = ds
        ordersList.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub ordersList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles ordersList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/portalcfd/proveedores/editarorden.aspx?id=" & e.CommandArgument.ToString)
            Case "cmdDelete"
                Call EliminaOrden(e.CommandArgument)
                Call MuestraOrdenes()
            Case "cmdReceive"
                Response.Redirect("~/portalcfd/proveedores/recibirorden.aspx?id=" & e.CommandArgument.ToString)
            Case "cmdDonwload"

                'Call EnviaOrdenProveedor(e.CommandArgument)

                If Not Directory.Exists(Server.MapPath("~/clientes/" & Session("appkey").ToString & "/oc")) Then
                    Directory.CreateDirectory(Server.MapPath("~/clientes/" & Session("appkey").ToString & "/oc"))
                End If

                Dim FilePath = Server.MapPath("~/clientes/" & Session("appkey").ToString & "/oc/") & "OrdenCompra_" & e.CommandArgument.ToString & ".pdf"

                If File.Exists(FilePath) Then
                    Dim FileName As String = Path.GetFileName(FilePath)
                    Response.Clear()
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
                    Response.Flush()
                    Response.WriteFile(FilePath)
                    Response.End()
                Else
                    GuardaPDF(GeneraOC(e.CommandArgument), FilePath)
                    Dim FileName As String = Path.GetFileName(FilePath)
                    Response.Clear()
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
                    Response.Flush()
                    Response.WriteFile(FilePath)
                    Response.End()
                End If
        End Select
    End Sub

    Private Sub ordersList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles ordersList.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
                Dim btnDonwload As ImageButton = CType(e.Item.FindControl("btnDonwload"), ImageButton)
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('Va a borrar una orden de compra. ¿Desea continuar?');")

                If e.Item.DataItem("estatusid") = 4 Or e.Item.DataItem("estatusid") = 5 Then
                    btnDelete.Visible = False
                End If

                If e.Item.DataItem("estatusid") <> 4 Then
                    e.Item.Cells(8).Text = ""
                    btnDonwload.Visible = False
                Else
                    btnDonwload.Visible = True
                End If

        End Select
    End Sub

    Private Sub ordersList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles ordersList.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Session("proveedorid") = proveedorid.SelectedValue
        Session("fecini") = CDate(fechaini.SelectedDate.Value.ToShortDateString)
        Session("fecfin") = CDate(fechafin.SelectedDate.Value.ToShortDateString)
        Session("no_orden") = txtNoOrden.Text
        '
        Dim ObjData As New DataControl(1)
        Dim sql As String = ""
        If txtNoOrden.Text.ToString.Length > 0 Then
            sql = "exec pOrdenCompra @cmd=1, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & Session("proveedorid").ToString & "', @no_orden='" & Session("no_orden").ToString & "'"
        Else
            sql = "exec pOrdenCompra @cmd=1, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & Session("proveedorid").ToString & "'"
        End If
        '
        ds = ObjData.FillDataSet(sql)
        ordersList.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub EliminaOrden(ByVal ordenid As Long)
        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("exec pOrdenCompra @cmd=4, @ordenid='" & ordenid.ToString & "'")
        ObjData = Nothing
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Call MuestraOrdenes()
    End Sub

    Private Sub EnviaOrdenProveedor(ByVal ordenId As Integer)
        '
        Dim ObjData As New DataControl(1)
        Dim dsPartidas As DataSet
        Dim comentarios As String = ""
        '
        Dim emailFrom As String = "rbaizabal@linkium.mx"
        Dim emailTo As String = "gesquivel@linkium.mx"
        Dim total As Decimal = 0

        Dim BodyTxt As String = "<html><head></head><body style='font-family:arial; font-size:12px;'>"
        BodyTxt += "<br /><br />Estimado proveedor, se anexa la presente órden de compra.<br /><br />"
        BodyTxt += "<fieldset style='padding:10px;'>"

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("exec pOrdenCompra @cmd=3, @ordenId='" & ordenId.ToString & "'", conn)
        Try
            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            If rs.Read Then
                BodyTxt += "<strong>Orden No.:</strong> " & ordenId.ToString & "<br />"
                BodyTxt += "<strong>Proveedor:</strong> " & rs("proveedor") & "<br />"
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        BodyTxt += "</fieldset><br />"
        BodyTxt += "<table border='1' cellpadding='3' cellspacing='0' style='width:1100px; border-color:#DBDFE4; font-family:arial; font-size:12px;'>"
        BodyTxt += "<tr><td style='background-color:#DBDFE4;'>Código</td><td style='background-color:#DBDFE4;'>Código Barras</td><td style='background-color:#DBDFE4;'>Descripción</td><td style='background-color:#DBDFE4;'>U. Medida</td><td style='background-color:#DBDFE4;'>Cantidad</td><td style='background-color:#DBDFE4;'>Costo</td><td style='background-color:#DBDFE4;'>Total</td></tr>"

        Try

            dsPartidas = objData.FillDataSet("exec pOrdenCompra @cmd=7, @ordenId='" & ordenId.ToString & "'")
            If Not dsPartidas Is Nothing Then
                For Each row As DataRow In dsPartidas.Tables(0).Rows
                    BodyTxt += "<tr><td>" &
                                        row("codigo").ToString & "</td><td>" &
                                        row("codigo_barras").ToString & "</td><td>" &
                                        row("descripcion").ToString & "</td><td>" &
                                        row("unidad").ToString & "</td><td align='right'>" &
                                        row("cantidad").ToString & "</td><td align='right'>" &
                                        FormatCurrency(row("costo_estandar"), 2).ToString & "</td><td align='right'>" &
                                        FormatCurrency(row("subtotal"), 2).ToString & "</td></tr>"
                    total = total + Convert.ToDecimal(row("subtotal"))
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

        BodyTxt += "<tr><td colspan='6' align='right'><strong>Total:</strong></td>"
        BodyTxt += "<td align='right'><strong>" & FormatCurrency(total, 2).ToString & "</strong>"
        BodyTxt += "</td></tr>"

        BodyTxt += "</table><br /><br /><br /><br />"

        BodyTxt += "<br /><br />COMERCIALIZADORA CODSA SA DE CV</body></html>"
        Dim ObjEmail As New ObjComms
        ObjEmail.EmailSubject = "CODSA - Órden de Compra No. " & ordenId.ToString
        ObjEmail.EmailBody = BodyTxt
        ObjEmail.EmailTo = emailTo
        ObjEmail.EmailCc = emailFrom
        ObjEmail.EmailFrom = emailFrom
        ObjEmail.EmailSend()
        ObjEmail = Nothing
        ''
    End Sub

    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub

    Private Function GeneraOC(ByVal ordenid As Long) As Telerik.Reporting.Report
        Dim plantillaid As Integer = 0
        Dim logo_formato As String = ""
        Dim connL As New SqlConnection(Session("conexion"))
        Dim cmdL As New SqlCommand("select top 1 isnull(logo_formato,'') as logo_formato, isnull(plantillaid,1) as plantillaid from tblCliente", connL)
        Try
            connL.Open()
            Dim rsL As SqlDataReader
            rsL = cmdL.ExecuteReader()

            If rsL.Read Then
                plantillaid = rsL("plantillaid")
                logo_formato = rsL("logo_formato").ToString
            End If
            connL.Close()
            connL.Dispose()
        Catch ex As Exception
            '
        Finally
            connL.Close()
            connL.Dispose()
            connL = Nothing
        End Try

        Dim fecha As String = ""
        Dim proveedor As String = ""
        Dim rfc As String = ""
        Dim direccion As String = ""
        Dim colonia As String = ""
        Dim estado As String = ""
        Dim comentarios As String = ""
        Dim usuario As String = ""

        Dim ObjData As New DataControl(1)
        Dim dsOC As DataSet
        dsOC = ObjData.FillDataSet("exec pOrdenCompra @cmd=3, @ordenId='" & ordenid.ToString & "'")

        If dsOC.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In dsOC.Tables(0).Rows
                fecha = row("fecha")
                proveedor = row("proveedor")
                rfc = row("rfc")
                direccion = row("direccion")
                colonia = row("colonia")
                estado = row("estado")
                comentarios = row("comentarios")
                usuario = row("usuario")
            Next
        End If

        Dim reporte As New OrdenCompra
        reporte.ReportParameters("cnn").Value = Session("conexion").ToString
        reporte.ReportParameters("plantillaId").Value = plantillaid
        reporte.ReportParameters("ordenId").Value = ordenid
        reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/logos/" + Session("logo").ToString + "")
        reporte.ReportParameters("txtFechaEmision").Value = fecha
        reporte.ReportParameters("txtNumeroOrdenCompra").Value = ordenid.ToString
        reporte.ReportParameters("txtProveedorRazonSocial").Value = proveedor.ToString
        reporte.ReportParameters("txtProveedorRFC").Value = rfc.ToString
        reporte.ReportParameters("txtProveedorCalleNum").Value = direccion.ToString
        reporte.ReportParameters("txtProveedorColonia").Value = colonia.ToString
        reporte.ReportParameters("txtProveedorCiudadEstado").Value = estado.ToString
        reporte.ReportParameters("txtComentarios").Value = comentarios.ToString

        reporte.ReportParameters("txtUsuario").Value = usuario
        reporte.ReportParameters("txtRazonSocialCliente").Value = Session("razonsocial").ToString

        Return reporte

    End Function

End Class