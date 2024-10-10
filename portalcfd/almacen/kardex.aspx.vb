Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Threading
Imports System.Globalization

Partial Class portalcfd_almacen_kardex
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim ObjCat As New DataControl(1)
            ObjCat.Catalogo(cmbSucursal, "select id, nombre from tblSucursal order by nombre", 0)
            ObjCat = Nothing

            fechaini.SelectedDate = Now
            fechafin.SelectedDate = Now

            Me.gridResults.DataSource = New String() {}
            Me.gridResults.Rebind()

            'If Session("clienteid") = 3 Then
            '    productlistcodsa.Visible = True
            '    productslist.Visible = False
            '    MuestraKardexCodsa(ProductoID.Value)
            'End If
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If cmbSucursal.SelectedValue = 0 Then
            rwAlerta.RadAlert("Selecciona una sucursal.", 350, 200, "Alerta", "", "")
        ElseIf txtSearch.Text.Length = 0 Then
            rwAlerta.RadAlert("Proporciona un código de producto", 350, 200, "Alerta", "", "")
        Else
            panelKardex.Visible = False
            gridResults.Visible = True
            gridResults.DataSource = Existencias()
            gridResults.DataBind()
        End If
    End Sub

    Function Existencias() As DataSet
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pInventario @cmd=2, @txtSearch='" & txtSearch.Text & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "'", conn)
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

    Protected Sub gridResults_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gridResults.ItemCommand
        Select Case e.CommandName
            Case "cmdView"
                panelKardex.Visible = True
                ProductoID.Value = e.CommandArgument

                If Session("clienteid") = 3 Then
                    Call MuestraKardexCodsa(ProductoID.Value)
                Else
                    Call MuestraKardex(ProductoID.Value)
                End If

        End Select
    End Sub

    Private Sub MuestraKardex(ByVal productoid As Long)
        '
        productlistcodsa.Visible = False
        productslist.Visible = True
        productslist.DataSource = Nothing
        productslist.DataBind()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        productslist.DataSource = ObjData.FillDataSet("exec pInventario @cmd=8, @productoid='" & productoid.ToString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "'")
        productslist.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub btnBuscarKardex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarKardex.Click
        If Session("clienteid") = 3 Then
            Call MuestraKardexCodsa(ProductoID.Value)
        Else
            Call MuestraKardex(ProductoID.Value)
        End If
    End Sub

    Private Sub MuestraKardexCodsa(ByVal productoid As Long)
        '
        productslist.Visible = False
        productlistcodsa.Visible = True
        productlistcodsa.DataSource = Nothing
        productlistcodsa.DataBind()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl(1)
        productlistcodsa.DataSource = ObjData.FillDataSet("exec pInventario @cmd=8, @productoid='" & productoid.ToString & "', @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @sucursalid='" & cmbSucursal.SelectedValue.ToString & "'")
        productlistcodsa.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

End Class