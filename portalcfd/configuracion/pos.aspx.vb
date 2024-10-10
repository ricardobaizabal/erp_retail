Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Public Class pos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim ObjData As New DataControl(1)
            ObjData.FillRadComboBox(cmbSucursal, "select id, nombre from tblSucursal order by nombre")

            Dim dt As New DataTable
            dt = ObjData.FillDataTable("EXEC pPresentacionesProducto @cmd=6")

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    Dim collection As IList(Of RadComboBoxItem) = cmbSucursal.Items
                    If (collection.Count <> 0) Then
                        For Each item As RadComboBoxItem In collection
                            If item.Value = row("sucursalid") Then
                                item.Checked = True
                            End If
                        Next
                    End If
                Next
            End If

            ObjData = Nothing

        End If

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        cmbSucursal.ClearCheckedItems()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim ObjData As New DataControl(1)
        For Each item As RadComboBoxItem In cmbSucursal.Items
            If item.Checked Then
                ObjData.RunSQLQuery("EXEC pPresentacionesProducto @cmd=7, @activobit=1, @sucursalid='" & item.Value.ToString & "'")
            Else
                ObjData.RunSQLQuery("EXEC pPresentacionesProducto @cmd=7, @activobit=0, @sucursalid='" & item.Value.ToString & "'")
            End If
        Next
        ObjData = Nothing
    End Sub

End Class