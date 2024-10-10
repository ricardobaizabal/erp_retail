Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

#Region "Class Specifications"

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''This Class Fill A RadComboBox From Telerik With A Specific Data Source''
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''The Method Receives: '''''''''''''''''''''''''''''''''''''''''''''''''''
''The Name Of The Telerik RadComboBox & The SQL Command To Execute''''''''
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

#End Region

Public Class FillRadComboBox

    Private p_conexion As String = ""

    Sub New(Optional ByVal conexion As Integer = 0)
        If conexion = 0 Then
            p_conexion = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Else
            p_conexion = HttpContext.Current.Session("conexion").ToString
        End If
    End Sub

    Public Sub FillRadComboBox(ByVal RadComboBox As RadComboBox, ByVal SQLCommand As String)

        Dim conn As New SqlConnection(p_conexion)
        Dim cmd As SqlDataAdapter = New SqlDataAdapter(SQLCommand, p_conexion)

        conn.Open()

        Dim ds As New DataSet
        cmd.Fill(ds)

        RadComboBox.DataSource = ds.Tables(0)
        RadComboBox.DataTextField = ds.Tables(0).Columns(1).ColumnName.ToString()
        RadComboBox.DataValueField = ds.Tables(0).Columns(0).ColumnName.ToString()
        RadComboBox.DataBind()

        conn.Close()
        conn.Dispose()
        conn = Nothing

    End Sub

End Class