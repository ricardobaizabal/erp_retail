Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class inventarios
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            objCat.Catalogo(filtrofamiliaid, "select id, nombre from tblFamilia where isnull(borradoBit,0)=0 order by nombre", 0, True)
            objCat.Catalogo(filtrosubfamiliaid, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 order by nombre", 0, True)
            objCat.Catalogo(filtrosucursalid, "select id, nombre from tblSucursal order by nombre", 0, True)
            objCat = Nothing
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteExistencias @familiaid='" & filtrofamiliaid.SelectedValue.ToString & "', @subfamiliaid='" & filtrosubfamiliaid.SelectedValue.ToString & "', @sucursalid='" & filtrosucursalid.SelectedValue.ToString & "'")
        reporteGrid.DataSource = ds
        reporteGrid.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub reporteGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles reporteGrid.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    e.Item.Cells(9).Text = ds.Tables(0).Compute("sum(existencia)", "")
                    e.Item.Cells(9).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(9).Font.Bold = True
                    e.Item.Cells(10).Text = FormatCurrency(ds.Tables(0).Compute("sum(importe)", ""), 2).ToString
                    e.Item.Cells(10).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(10).Font.Bold = True
                    e.Item.Cells(11).Text = FormatCurrency(ds.Tables(0).Compute("sum(importe_ventas)", ""), 2).ToString
                    e.Item.Cells(11).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(11).Font.Bold = True
                End If
        End Select
    End Sub

    Private Sub reporteGrid_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles reporteGrid.NeedDataSource
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("exec pReporteExistencias @familiaid='" & filtrofamiliaid.SelectedValue.ToString & "', @subfamiliaid='" & filtrosubfamiliaid.SelectedValue.ToString & "', @sucursalid='" & filtrosucursalid.SelectedValue.ToString & "'")
        reporteGrid.DataSource = ds
        ObjData = Nothing
    End Sub

    Private Sub filtrofamiliaid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles filtrofamiliaid.SelectedIndexChanged
        If filtrofamiliaid.SelectedValue = 0 Then
            filtrosubfamiliaid.Enabled = False
            filtrosubfamiliaid.SelectedValue = 0
        Else
            filtrosubfamiliaid.Enabled = True
            Dim objCat As New DataControl(1)
            objCat.Catalogo(filtrosubfamiliaid, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 and familiaid='" & filtrofamiliaid.SelectedValue & "' order by nombre", 0)
            objCat = Nothing
        End If
    End Sub

End Class