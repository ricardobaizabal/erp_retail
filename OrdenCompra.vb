Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Telerik.Reporting
Imports Telerik.Reporting.Drawing
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class OrdenCompra
    Inherits Telerik.Reporting.Report

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OrdenCompra_NeedDataSource(sender As Object, e As EventArgs) Handles Me.NeedDataSource

        Dim cnn As String = Me.ReportParameters("cnn").Value
        Dim ordenId As Long = Me.ReportParameters("ordenId").Value

        Dim conn As New SqlConnection(cnn)
        Dim cmd As New SqlDataAdapter("EXEC pOrdenCompra @cmd=7, @ordenId='" & ordenId.ToString & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        If Not IsDBNull(ds.Tables(0).Compute("sum(total)", "")) Then
            Me.boxTotal.Value = FormatCurrency(ds.Tables(0).Compute("sum(total)", ""), 2)
        End If

        Dim processingReport = CType(sender, Telerik.Reporting.Processing.Report)
        processingReport.DataSource = ds

    End Sub


End Class