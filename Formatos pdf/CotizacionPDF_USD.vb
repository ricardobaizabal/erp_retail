Imports System.ComponentModel
Imports System.Drawing
Imports Telerik.Reporting
Imports Telerik.Reporting.Drawing
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Public Class ReportePDF_USD
    Inherits Telerik.Reporting.Report

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Factura_NeedDataSource(sender As Object, e As EventArgs) Handles Me.NeedDataSource
        Dim cotizacionID As Long = Me.ReportParameters("cotizacionID").Value
        Dim ds As DataSet = New DataSet
        Try
            ds = FillDataSet("EXEC pCotizacionDetalle @cmd=2, @cotizacionid='" & cotizacionID.ToString & "'")

        Catch ex As Exception
        End Try

        Dim processingReport = CType(sender, Telerik.Reporting.Processing.Report)
        processingReport.DataSource = ds
        '

    End Sub
    Public Function FillDataSet(ByVal SQL As String) As DataSet
        Dim p_conexion As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim conn As New SqlConnection(p_conexion)
        conn.Open()
        Dim cmd As New SqlDataAdapter(SQL, conn)
        cmd.SelectCommand.CommandTimeout = 3600
        Dim ds As DataSet = New DataSet
        cmd.Fill(ds)
        conn.Close()
        conn.Dispose()
        conn = Nothing
        Return ds
    End Function

End Class