Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Telerik.Reporting
Imports Telerik.Reporting.Drawing
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class OrdenSalida
    Inherits Telerik.Reporting.Report

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OrdenCompra_NeedDataSource(sender As Object, e As EventArgs) Handles Me.NeedDataSource

        Dim cnn As String = Me.ReportParameters("cnn").Value
        Dim transferenciaid As Long = Me.ReportParameters("loteid").Value

        Dim conn As New SqlConnection(cnn)
        Dim cmd As New SqlDataAdapter("EXEC ptransferencia @cmd=6, @transferenciaid='" & transferenciaid.ToString & "'", conn)

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


        Dim processingReport = CType(sender, Telerik.Reporting.Processing.Report)
        processingReport.DataSource = ds

    End Sub


End Class