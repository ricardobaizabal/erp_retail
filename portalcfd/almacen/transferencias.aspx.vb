Imports System.Globalization
Imports System.Threading
Partial Public Class transferencias
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fha_ini.SelectedDate = Now()
            fha_fin.SelectedDate = Now()
            Dim ObjCat As New DataControl(1)
            ObjCat.Catalogo(origenid, "select id, nombre from tblSucursal order by nombre", 0)
            ObjCat.Catalogo(destinoid, "select id, nombre from tblSucursal order by nombre", 0)
            ObjCat.Catalogo(estatusid, "select id, nombre from tblTransferenciaEstatus order by nombre", 0)
            If Session("clienteid") = 3 And Session("perfilid") = 7 Then
                origenid.SelectedValue = 44
                origenid.Enabled = False
                ObjCat.Catalogo(destinoid, "select id, nombre from tblSucursal where id <> " & origenid.SelectedValue.ToString & " order by nombre", 0)
            End If
            ObjCat = Nothing
            Call CargaListado()
        End If
    End Sub

    Private Sub origenid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles origenid.SelectedIndexChanged
        If origenid.SelectedValue > 0 Then
            Dim ObjCat As New DataControl(1)
            ObjCat.Catalogo(destinoid, "select id, nombre from tblSucursal where id <> " & origenid.SelectedValue.ToString & " order by nombre", 0)
            ObjCat = Nothing
        End If
    End Sub

    Private Sub loteslist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles loteslist.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/portalcfd/almacen/editalote.aspx?id=" & e.CommandArgument.ToString)
            Case "cmdDelete"
                Call BorraTransferencia(e.CommandArgument)
                Call CargaListado()
            Case "cmdAutorizar"
                Call AutorizarTransferencia(e.CommandArgument)
                Call CargaListado()
            Case "cmdCancelar"
                Call CancelarTransferencia(e.CommandArgument)
                Call CargaListado()
        End Select
    End Sub

    Private Sub loteslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles loteslist.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('Va a eliminar una transferencia y su detalle de prodcutos. ¿Desea continuar?')")

                Dim lnkAutorizar As LinkButton = CType(e.Item.FindControl("lnkAutorizar"), LinkButton)
                lnkAutorizar.Attributes.Add("onclick", "javascript:return confirm('Va a autorizar una transferencia. ¿Desea continuar?')")

                Dim lnkCancelar As LinkButton = CType(e.Item.FindControl("lnkCancelar"), LinkButton)
                lnkCancelar.Attributes.Add("onclick", "javascript:return confirm('Va a cancelar una transferencia. ¿Desea continuar?')")

                If CBool(e.Item.DataItem("autorizadaBit")) = True Then
                    lnkAutorizar.Visible = False
                    lnkCancelar.Visible = False
                End If
                e.Item.Cells(10).Font.Bold = True
                If e.Item.DataItem("estatusid") = 2 Then
                    e.Item.Cells(10).ForeColor = Drawing.Color.Orange
                    btnDelete.Visible = True
                ElseIf e.Item.DataItem("estatusid") = 3 Then
                    e.Item.Cells(10).ForeColor = Drawing.Color.Green
                    lnkCancelar.Visible = False
                    btnDelete.Visible = False
                    btnDelete.ToolTip = "No se puede borrar una transferencia procesada."
                ElseIf e.Item.DataItem("estatusid") = 4 Then
                    e.Item.Cells(10).ForeColor = Drawing.Color.Red
                    lnkAutorizar.Visible = False
                    lnkCancelar.Visible = False
                    btnDelete.Visible = False
                    btnDelete.ToolTip = "No se puede borrar una transferencia cancelada."
                Else
                    e.Item.Cells(10).ForeColor = Drawing.Color.Blue
                    lnkAutorizar.Visible = False
                    lnkCancelar.Visible = False
                End If
        End Select
    End Sub

    Private Sub CargaListado()

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

        Dim sql As String = ""
        If Session("clienteid") = 3 And Session("perfilid") = 7 Then
            sql = "exec pTransferencia @cmd=5, @sucursalid=44, @origenid='" & origenid.SelectedValue.ToString & "', @destinoid='" & destinoid.SelectedValue.ToString & "', @fhaini='" & fha_ini.SelectedDate.Value.ToShortDateString & "', @fhafin='" & fha_fin.SelectedDate.Value.ToShortDateString & "', @estatusid='" & estatusid.SelectedValue.ToString & "'"
        Else
            sql = "exec pTransferencia @cmd=5, @origenid='" & origenid.SelectedValue.ToString & "', @destinoid='" & destinoid.SelectedValue.ToString & "', @fhaini='" & fha_ini.SelectedDate.Value.ToShortDateString & "', @fhafin='" & fha_fin.SelectedDate.Value.ToShortDateString & "', @estatusid='" & estatusid.SelectedValue.ToString & "'"
        End If
        Dim ObjData As New DataControl(1)
        loteslist.DataSource = ObjData.FillDataSet(sql)
        loteslist.DataBind()
        ObjData = Nothing

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")

    End Sub

    Private Sub AutorizarTransferencia(ByVal transferenciaid As Long)
        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("exec pTransferencia @cmd=17, @transferenciaid='" & transferenciaid.ToString & "'")
        ObjData = Nothing
    End Sub

    Private Sub CancelarTransferencia(ByVal transferenciaid As Long)
        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("exec pTransferencia @cmd=16, @transferenciaid='" & transferenciaid.ToString & "', @estatusid=4")
        ObjData = Nothing
    End Sub

    Private Sub BorraTransferencia(ByVal transferenciaid As Long)
        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("exec pTransferencia @cmd=7, @transferenciaid='" & transferenciaid.ToString & "'")
        ObjData = Nothing
    End Sub

    Private Sub loteslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles loteslist.NeedDataSource

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

        Dim sql As String = ""
        If Session("clienteid") = 3 And Session("perfilid") = 7 Then
            sql = "exec pTransferencia @cmd=5, @sucursalid=44, @origenid='" & origenid.SelectedValue.ToString & "', @destinoid='" & destinoid.SelectedValue.ToString & "', @fhaini='" & fha_ini.SelectedDate.Value.ToShortDateString & "', @fhafin='" & fha_fin.SelectedDate.Value.ToShortDateString & "', @estatusid='" & estatusid.SelectedValue.ToString & "'"
        Else
            sql = "exec pTransferencia @cmd=5, @origenid='" & origenid.SelectedValue.ToString & "', @destinoid='" & destinoid.SelectedValue.ToString & "', @fhaini='" & fha_ini.SelectedDate.Value.ToShortDateString & "', @fhafin='" & fha_fin.SelectedDate.Value.ToShortDateString & "', @estatusid='" & estatusid.SelectedValue.ToString & "'"
        End If
        Dim ObjData As New DataControl(1)
        loteslist.DataSource = ObjData.FillDataSet(sql)
        ObjData = Nothing

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")

    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Call CargaListado()
    End Sub

End Class