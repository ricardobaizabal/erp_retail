Imports System.Data
Imports System.Data.SqlClient
Partial Class portalcfd_Reportes
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        lblReportsLegend.Text = "Reportes"

        Dim ObjData As New DataControl(0)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 1))
        p.Add(New SqlParameter("@clienteid", Session("clienteid")))
        ds = ObjData.FillDataSet("pCliente", p)
        ObjData = Nothing

        If ds.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                If CBool(row("ModuloEscolarBit")) = False Then
                    panelPedidos.Visible = False
                    panelReportes.Visible = True
                Else
                    panelPedidos.Visible = True
                    panelReportes.Visible = False
                End If
            Next
        End If
    End Sub

#End Region

End Class
