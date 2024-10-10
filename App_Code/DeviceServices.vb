Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

<WebService(Namespace:="http://interjoya.com.mx/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class DeviceServices
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function LeeEtiquetaTriple(ByVal codigo As String) As String()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("select top 1 isnull(left(codigo,10),'') as codigo, isnull(left(descripcion,21),'') as nombre, isnull(unitario,0) as precio from tblMisProductos where codigo='" & codigo.ToString & "'", conn)
        Dim valores(3) As String

        Try


            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                valores(0) = rs("codigo")
                valores(1) = rs("nombre")
                valores(2) = FormatCurrency(rs("precio"), 0).ToString
            End If

        Catch ex As Exception
            '    
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        Return valores
    End Function


End Class
