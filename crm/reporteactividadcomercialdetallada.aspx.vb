Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Public Class reporteactividadcomercialdetallada
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub OportunidadeslistItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles oportunidadeslist.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim imgEmail As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgEmail"), System.Web.UI.WebControls.Image)
                Dim imgTelefono As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgTelefono"), System.Web.UI.WebControls.Image)

                If e.Item.DataItem("email").ToString.Length > 0 Then
                    imgEmail.Visible = True
                    imgEmail.ToolTip = e.Item.DataItem("email").ToString
                End If

                If e.Item.DataItem("telefono").ToString.Length > 0 Then
                    imgTelefono.Visible = True
                    imgTelefono.ToolTip = e.Item.DataItem("telefono").ToString
                End If
        End Select
    End Sub
    Private Sub OportunidadeslistNeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles oportunidadeslist.NeedDataSource
        If Not e.IsFromDetailTable Then
            oportunidadeslist.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
            oportunidadeslist.DataSource = LeerReporte()
        End If
    End Sub

    Private Function LeerReporte() As DataSet
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim sql As String = ""
        sql = "exec pCRMReporte @cmd=1, @userid='" & cmbUsuario.SelectedValue.ToString() & "', @etapaid='" & cmbEtapa.SelectedValue.ToString & "', @fechaini='" & fha_ini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fha_fin.SelectedDate.Value.ToShortDateString & "',@categoriaid='" & cmbCategoria.SelectedValue.ToString & "'"

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter(sql, conn)
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
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
        Return ds
    End Function
End Class