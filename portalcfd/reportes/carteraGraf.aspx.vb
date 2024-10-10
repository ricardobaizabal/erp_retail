﻿Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Telerik.Web.UI
Partial Public Class carteraGraf
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call CargaCatalogos()
            Call CargaAcumulado()
            Call MuestraGrafica()
        End If
    End Sub

    Private Sub CargaCatalogos()
        Dim objCat As New DataControl(1)
        objCat.Catalogo(clienteid, "exec pCatalogos @cmd=2", 0)
        objCat = Nothing
    End Sub

    Private Sub CargaAcumulado()
        panelDetalle.Visible = False
        Dim ObjData As New DataControl(1)
        reporteGrid.DataSource = ObjData.FillDataSet("exec pReporteCobranza @cmd=1, @clienteid='" & clienteid.SelectedValue.ToString & "'")
        reporteGrid.DataBind()
        ObjData = Nothing
    End Sub

    Protected Sub reporteGrid_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles reporteGrid.ItemCommand
        Select Case e.CommandName
            Case "cmdR1"
                Call MuestraDetalle(1)
            Case "cmdR2"
                Call MuestraDetalle(2)
            Case "cmdR3"
                Call MuestraDetalle(3)
            Case "cmdR4"
                Call MuestraDetalle(4)
            Case "cmdR5"
                Call MuestraDetalle(5)
            Case "cmdR6"
                Call MuestraDetalle(6)
            Case "cmdR7"
                Call MuestraDetalle(7)
        End Select
    End Sub

    Private Sub MuestraDetalle(ByVal rangoId As Integer)
        Session("rangoId") = rangoId
        panelDetalle.Visible = True
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteCobranza @cmd=2, @rangoid='" & rangoId.ToString & "', @clienteid='" & clienteid.SelectedValue.ToString & "'")
        detalleGrid.DataSource = ds
        detalleGrid.DataBind()
        ObjData = Nothing
    End Sub

    Protected Sub detalleGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles detalleGrid.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim lnkFolio As LinkButton = CType(e.Item.FindControl("lnkFolio"), LinkButton)
                Dim lnkPDF As LinkButton = CType(e.Item.FindControl("lnkPDF"), LinkButton)
                If ds.Tables(0).Rows.Count > 0 Then
                    If e.Item.DataItem("tipo").ToString = "Remision" Then
                        lnkFolio.Enabled = False
                        lnkPDF.Visible = False
                    End If
                End If
            Case Telerik.Web.UI.GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    e.Item.Cells(8).Text = FormatCurrency(ds.Tables(0).Compute("sum(importe)", ""), 2).ToString
                    e.Item.Cells(8).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(8).Font.Bold = True
                    '
                    e.Item.Cells(9).Text = FormatCurrency(ds.Tables(0).Compute("sum(iva)", ""), 2).ToString
                    e.Item.Cells(9).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(9).Font.Bold = True
                    '
                    e.Item.Cells(10).Text = FormatCurrency(ds.Tables(0).Compute("sum(ieps)", ""), 2).ToString
                    e.Item.Cells(10).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(10).Font.Bold = True
                    '
                    e.Item.Cells(11).Text = FormatCurrency(ds.Tables(0).Compute("sum(total)", ""), 2).ToString
                    e.Item.Cells(11).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(11).Font.Bold = True
                End If
        End Select
    End Sub

    Private Sub MuestraGrafica()
        '
        RadChart1.Visible = True
        Dim ds As New DataSet
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporte306090 @clienteid='" & clienteid.SelectedValue.ToString & "'")
        ObjData = Nothing
        '
        RadChart1.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Vertical
        Dim dv As New DataView
        dv = ds.Tables(0).DefaultView
        RadChart1.DataSource = dv

        RadChart1.DataBind()
        dv = Nothing
        ''
    End Sub

    Private Sub clienteid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles clienteid.SelectedIndexChanged
        Call CargaAcumulado()
        Call MuestraGrafica()
    End Sub

    Private Sub DownloadPDF(ByVal cfdid As Long)
        Dim uuid As String = ""
        Dim connF As New SqlConnection(Session("conexion"))
        Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                uuid = rs("uuid").ToString
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Dim FilePath = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/pdf/") & uuid.ToString & ".pdf"
        '
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If

    End Sub

    Protected Sub detalleGrid_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles detalleGrid.NeedDataSource
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteCobranza @cmd=2, @rangoid='" & Session("rangoId").ToString & "', @clienteid='" & clienteid.SelectedValue.ToString & "'")
        detalleGrid.DataSource = ds
        ObjData = Nothing
    End Sub

    Protected Sub detalleGrid_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles detalleGrid.ItemCommand
        Select Case e.CommandName
            Case "cmdPDF"
                Call DownloadPDF(e.CommandArgument)
            Case "cmdFolio"
                Response.Redirect("~/portalcfd/CFD_Detalle.aspx?id=" & e.CommandArgument.ToString)
        End Select
    End Sub

End Class