Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class General
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim ObjData As New DataControl(1)
            Dim ds As New DataSet
            Dim p As New ArrayList
            p.Add(New SqlParameter("@cmd", 0))

            ds = ObjData.FillDataSet("pConfiguracionPOS", p)
            ObjData = Nothing

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows

                    chbmoduloactivo.Checked = row("activobit")

                Next

                topelist.DataSource = ObtenerDias()
                topelist.DataBind()
            End If
        End If
    End Sub

   
    Function ObtenerDias() As DataSet
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pConfiguracionPOS @cmd=1", conn)
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
        Return ds
    End Function


   

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
     
    End Sub


   

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        
    End Sub

    Private Sub btnAdd1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd1.Click
        For Each dataItem As Telerik.Web.UI.GridDataItem In topelist.MasterTableView.Items


            Dim dia As Integer = dataItem.GetDataKeyValue("dia")
            Dim txtefectivo As Telerik.Web.UI.RadNumericTextBox = DirectCast(dataItem.FindControl("txtefectivo"), Telerik.Web.UI.RadNumericTextBox)
            ' Dim hora As Telerik.Web.UI.RadTimePicker = DirectCast(dataItem.FindControl("rtphora"), Telerik.Web.UI.RadTimePicker)


            Dim ObjData As New DataControl(1)
            'ObjData.RunSQLQuery("EXEC pConfiguracionPOS @cmd=2, @efectivo='" & txtefectivo.Text & "', @hora='" & hora.SelectedTime.ToString & "', @dia='" & dia & "', @activo='" & chbmoduloactivo.Checked & "'")
            ObjData.RunSQLQuery("EXEC pConfiguracionPOS @cmd=2, @efectivo='" & txtefectivo.Text & "', @dia='" & dia & "', @activo='" & chbmoduloactivo.Checked & "'")
            ObjData = Nothing

            

        Next

        topelist.DataSource = ObtenerDias()
        topelist.DataBind()
    End Sub

    Private Sub topelist_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles topelist.ItemCreated
        'Select Case e.Item.ItemType
        '    Case GridItemType.Item, GridItemType.AlternatingItem
        '        Dim hora As Telerik.Web.UI.RadTimePicker = DirectCast(e.Item.FindControl("rtphora"), Telerik.Web.UI.RadTimePicker)
        '        hora.TimeView.TimeFormat = "HH:mm:ss"
        '        hora.DateInput.DateFormat = "dd/MM/yyyy HH:mm:ss"
        ' End Select
    End Sub

    Private Sub topelist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles topelist.ItemDataBound
        'Select Case e.Item.ItemType
        '    Case GridItemType.Item, GridItemType.AlternatingItem
        '        Dim hora As Telerik.Web.UI.RadTimePicker = DirectCast(e.Item.FindControl("rtphora"), Telerik.Web.UI.RadTimePicker)
        '        hora.TimeView.TimeFormat = "HH:mm:ss"
        '        hora.DateInput.DateFormat = "dd/MM/yyyy HH:mm:ss"
        'End Select
    End Sub
End Class