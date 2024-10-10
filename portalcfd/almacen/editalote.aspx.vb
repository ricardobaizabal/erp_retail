Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI
Imports System.IO
Imports Telerik.Reporting.Processing

Partial Public Class editalote
    Inherits System.Web.UI.Page

    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle
        If Not IsPostBack Then
            Call MuestraEtiqueta()
            Call MuestraDetalle()
        End If
    End Sub

    Private Sub MuestraEtiqueta()
        Dim conn As New SqlConnection(Session("conexion"))
        Try

            Dim cmd As New SqlCommand("EXEC pTransferencia @cmd=2, @transferenciaid='" & Request("id").ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                lblFolio.Text = rs("id").ToString
                lblFecha.Text = rs("fecha").ToString
                lblOrigen.Text = rs("origen")
                lblDestino.Text = rs("destino")
                lblUsuario.Text = rs("usuario")
                lblComentario.Text = Replace(rs("comentario"), vbCrLf, "<br />")
                SucOrigenID.Value = rs("origenid")
                SucDestinoID.Value = rs("destinoid")
                '
                If rs("estatusid") = 2 Then
                    btnFinalizar.Enabled = False
                    btnFinalizar.ToolTip = "Esta transferencia se encuentra pendiente de recibir."
                    'productslist.Columns(5).Visible = False
                    txtSearch.Enabled = False
                    btnSearch.Enabled = False
                    btnCancel.Enabled = False
                ElseIf rs("estatusid") = 3 Then
                    btnFinalizar.Enabled = False
                    btnFinalizar.ToolTip = "Esta transferencia ya ha sido procesada"
                    'productslist.Columns(5).Visible = False
                    txtSearch.Enabled = False
                    btnSearch.Enabled = False
                    btnCancel.Enabled = False
                End If
                '
            End If
        Catch ex As Exception
            '
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Private Sub MuestraDetalle()
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pTransferencia @cmd=6, @transferenciaid='" & Request("id").ToString & "'")
        productslist.DataSource = ds
        productslist.DataBind()
        ObjData = Nothing
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("~/portalcfd/almacen/transferencias.aspx")
    End Sub

    Protected Sub productslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles productslist.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
                btnDelete.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea eliminar este producto?');")

                Dim txtCantidad As RadNumericTextBox = CType(e.Item.FindControl("txtCantidad"), RadNumericTextBox)

                Dim conn As New SqlConnection(Session("conexion"))
                Try
                    Dim cmd As New SqlCommand("EXEC pTransferencia @cmd=2, @transferenciaid='" & Request("id").ToString & "'", conn)
                    conn.Open()
                    Dim rs As SqlDataReader
                    rs = cmd.ExecuteReader()

                    If rs.Read Then
                        If rs("estatusid") = 2 Or rs("estatusid") = 3 Then
                            txtCantidad.Enabled = False
                            btnDelete.Visible = False
                        End If
                    End If
                Catch ex As Exception
                Finally
                    conn.Close()
                    conn.Dispose()
                    conn = Nothing
                End Try
            Case Telerik.Web.UI.GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    e.Item.Cells(6).Text = FormatCurrency(ds.Tables(0).Compute("sum(costo)", ""), 2).ToString
                    e.Item.Cells(6).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(6).Font.Bold = True
                End If
        End Select
    End Sub

    Protected Sub productslist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles productslist.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pTransferencia @cmd=4, @id='" & e.CommandArgument & "'")
                ObjData = Nothing
        End Select

        Call MuestraDetalle()

    End Sub

    Sub txtCantidad_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each dataItem As Telerik.Web.UI.GridDataItem In productslist.MasterTableView.Items
            Dim txtCantidad As RadNumericTextBox = DirectCast(dataItem.FindControl("txtCantidad"), RadNumericTextBox)
            Dim cantidad As Decimal = 0
            Try
                cantidad = Convert.ToDecimal(txtCantidad.Text)
            Catch ex As Exception
                cantidad = 0
            End Try

            Dim id As Integer = Convert.ToInt32(dataItem.GetDataKeyValue("id").ToString)

            Dim ObjData As New DataControl(1)
            ObjData.RunSQLQuery("exec pTransferencia @cmd=10, @id='" & id.ToString & "', @cantidad='" & cantidad.ToString & "'")
            ObjData = Nothing

        Next

        Call MuestraDetalle()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        panelSearch.Visible = True
        btnAgregaConceptos.Visible = True
        Dim ObjData As New DataControl(1)
        resultslist.DataSource = ObjData.FillDataSet("exec pTransferencia @cmd=9, @txtSearch='" & txtSearch.Text & "', @sucursalid='" & SucOrigenID.Value.ToString & "'")
        resultslist.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub resultslist_EditCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles resultslist.EditCommand
        Select Case e.CommandName
            Case "cmdAdd"
                Call AgregaItem(e.CommandArgument, e.Item)
        End Select
    End Sub

    Private Sub resultslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles resultslist.NeedDataSource
        Dim ObjData As New DataControl(1)
        resultslist.DataSource = ObjData.FillDataSet("exec pMisProductos @cmd=7, @txtSearch='" & txtSearch.Text & "'")
        ObjData = Nothing
    End Sub

    Private Sub AgregaItem(ByVal productoId As Long, ByVal item As GridItem)
        Dim txtCantidad As RadNumericTextBox = DirectCast(item.FindControl("txtCantidad"), RadNumericTextBox)

        Dim cantidad As Decimal = 0
        Try
            cantidad = Convert.ToDecimal(txtCantidad.Text)
        Catch ex As Exception
            cantidad = 0
        End Try

        Dim id As Long = 0
        Dim presentacion As Integer = 0
        Dim factor As Decimal = 0
        Dim ObjData As New DataControl(1)

        id = item.DataItem.GetDataKeyValue("id")
        presentacion = item.DataItem.GetDataKeyValue("presentacion")
        factor = item.DataItem.GetDataKeyValue("factor")

        If cantidad > 0 Then
            If presentacion > 0 Then
                ObjData.RunSQLQuery("exec pTransferencia @cmd=3, @transferenciaid='" & Request("id").ToString & "', @cantidad='" & cantidad.ToString & "', @productoid=0, @presentacionid='" & id.ToString & "', @factor='" & factor.ToString & "'")
            Else
                ObjData.RunSQLQuery("exec pTransferencia @cmd=3, @transferenciaid='" & Request("id").ToString & "', @cantidad='" & cantidad.ToString & "', @productoid='" & id.ToString & "', @presentacionid=0, @factor='" & factor.ToString & "'")
            End If
        End If

        txtSearch.Text = ""
        panelSearch.Visible = False
        Call MuestraDetalle()

    End Sub

    Private Sub btnAgregaConceptos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregaConceptos.Click

        Dim id As Long = 0
        Dim presentacion As Integer = 0
        Dim disponibles As Decimal = 0
        Dim factor As Decimal = 0
        Dim ObjData As New DataControl(1)

        For Each row As GridDataItem In resultslist.MasterTableView.Items

            id = row.GetDataKeyValue("id")
            presentacion = row.GetDataKeyValue("presentacion")
            disponibles = row.GetDataKeyValue("disponibles")
            factor = row.GetDataKeyValue("factor")

            Dim txtCantidad As RadNumericTextBox = DirectCast(row.FindControl("txtCantidad"), RadNumericTextBox)
            Dim cantidad As Decimal = 0
            Try
                cantidad = Convert.ToDecimal(txtCantidad.Text)
            Catch ex As Exception
                cantidad = 0
            End Try

            If cantidad > 0 Then
                If presentacion > 0 Then
                    If cantidad <= disponibles Then
                        ObjData.RunSQLQuery("exec pTransferencia @cmd=3, @transferenciaid='" & Request("id").ToString & "', @cantidad='" & cantidad.ToString & "', @productoid=0, @presentacionid='" & id.ToString & "', @factor='" & factor.ToString & "'")
                    End If
                Else
                    ObjData.RunSQLQuery("exec pTransferencia @cmd=3, @transferenciaid='" & Request("id").ToString & "', @cantidad='" & cantidad.ToString & "', @productoid='" & id.ToString & "', @presentacionid=0, @factor='" & factor.ToString & "'")
                End If
            End If
        Next
        ObjData = Nothing

        txtSearch.Text = ""
        panelSearch.Visible = False
        btnAgregaConceptos.Visible = False
        Call MuestraDetalle()

    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtSearch.Text = ""
        panelSearch.Visible = False
    End Sub

    Private Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        '
        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("exec pTransferencia @cmd=8, @transferenciaid='" & Request("id").ToString & "'")
        ObjData = Nothing
        '
        Response.Redirect("~/portalcfd/almacen/transferencias.aspx")
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        DownloadPDF(Request("id"))
    End Sub
    Private Sub DownloadPDF(ByVal ordenid As Long)
        Dim FilePath = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/pdf/") & "Orden_" + ordenid.ToString & ".pdf"
        GuardaPDF(GeneraPDF(ordenid), Server.MapPath("~/clientes/" + Session("appkey").ToString + "/pdf/") & "Orden_" + ordenid.ToString & ".pdf")
        Dim FileName As String = Path.GetFileName(FilePath)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
        Response.Flush()
        Response.WriteFile(FilePath)
        Response.End()
    End Sub
    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub
    Private Function GeneraPDF(ByVal ordenid As Long) As Telerik.Reporting.Report
        Dim conn As New SqlConnection(Session("conexion"))
        Dim ds As DataSet = New DataSet
        Dim plantillaid As String = ""
        Dim logo_formato As String = ""
        Dim txtFechaEmision As DateTime
        Dim txtFolio As String = ""
        Dim txtOrigen As String = ""
        Dim txtDestino As String = ""
        Dim txtUsuario As String = ""
        Dim loteid As String = ""
        Dim txtComentarios As String = ""

        Try

            Dim cmd As New SqlCommand("EXEC pTransferencia @cmd=2, @transferenciaid='" & Request("id").ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                plantillaid = rs("plantillaid")
                logo_formato = rs("logo_formato")

                txtFolio = rs("id").ToString
                txtFechaEmision = rs("fecha")
                txtOrigen = rs("origen")
                txtDestino = rs("destino")
                txtUsuario = rs("usuario")
                txtComentarios = rs("comentario")

                '
            End If
        Catch ex As Exception
            '
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        Dim reporte As New OrdenSalida

        reporte.ReportParameters("plantillaId").Value = plantillaid
        reporte.ReportParameters("loteid").Value = ordenid
        reporte.ReportParameters("cnn").Value = Session("conexion").ToString

        reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/logos/" & logo_formato.ToString & "")

        reporte.ReportParameters("txtFechaEmision").Value = Format(txtFechaEmision, "dd MMM yyyy")
        reporte.ReportParameters("txtFolio").Value = txtFolio
        reporte.ReportParameters("txtOrigen").Value = txtOrigen
        reporte.ReportParameters("txtDestino").Value = txtDestino
        reporte.ReportParameters("txtUsuario").Value = txtUsuario
        reporte.ReportParameters("loteid").Value = Request("id")
        reporte.ReportParameters("txtComentarios").Value = txtComentarios

        Return reporte

    End Function
End Class