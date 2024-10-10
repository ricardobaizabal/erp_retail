Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Public Class perfilpermisos
    Inherits System.Web.UI.Page

    Private IdPerfil As Int32 = 0
    Private IdPerfilPermisos As Int32 = 0
    Private Lectura As Boolean = False
    Private Escritura As Boolean = False
    Private Eliminacion As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbPerfil, "select id, nombre from tblUsuarioPerfil order by nombre", 1)
            objCat = Nothing
        End If

    End Sub

    Private Sub grdAccesos_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdAccesos.ItemDataBound
        If (TypeOf (e.Item) Is GridDataItem) Then

            'Get the instance of the right type
            Dim dataBoundItem As GridDataItem = e.Item

            'Check the formatting condition
            If e.Item.OwnerTableView.Name = "Perfiles" Then
                Select Case dataBoundItem.GetDataKeyValue("Level")
                    Case 0
                        dataBoundItem("Descripcion").CssClass = "Level-0"
                    Case 1
                        dataBoundItem("Descripcion").CssClass = "Level-1"
                    Case 2
                        dataBoundItem("Descripcion").CssClass = "Level-2"
                End Select

            End If

        End If
    End Sub

    Private Sub grdAccesos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdAccesos.NeedDataSource
        grdAccesos.MasterTableView.NoMasterRecordsText = "No se encontraron perfiles para mostrar"
        grdAccesos.DataSource = CargaAccesos()
    End Sub

    Function CargaAccesos() As DataSet

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet()
        Dim p As New ArrayList
        p.Add(New SqlParameter("@IdPerfil", cmbPerfil.SelectedValue))
        ds = ObjData.FillDataSet("pPermisos_Read", p)
        ObjData = Nothing

        Return ds

    End Function

    Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)
        TryCast(TryCast(sender, CheckBox).NamingContainer, GridItem).Selected = TryCast(sender, CheckBox).Checked

        Dim chk As CheckBox = TryCast(sender, CheckBox)
        Dim row As GridDataItem = TryCast(chk.NamingContainer, GridDataItem)
        Dim keyValue As String = row.GetDataKeyValue("IdPerfilPermisos").ToString()

        IdPerfil = cmbPerfil.SelectedValue
        IdPerfilPermisos = keyValue

        Dim Tipo As String = ""
        Select Case chk.ID
            Case "chkLectura"
                Tipo = "R"
                Lectura = chk.Checked
            Case "chkEscritura"
                Tipo = "W"
                Escritura = chk.Checked
            Case "chkEliminar"
                Tipo = "D"
                Eliminacion = chk.Checked
        End Select

        ActualizarPermisos(Tipo)

        Dim checkHeader As Boolean = True
        For Each dataItem As GridDataItem In grdAccesos.MasterTableView.Items
            If Not TryCast(dataItem.FindControl(chk.ID), CheckBox).Checked Then
                checkHeader = False
                Exit For
            End If
        Next

        Dim headerItem As GridHeaderItem = TryCast(grdAccesos.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
        TryCast(headerItem.FindControl(chk.ID & "_header"), CheckBox).Checked = checkHeader
    End Sub

    Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
        Dim headerCheckBox As CheckBox = TryCast(sender, CheckBox)
        For Each dataItem As GridDataItem In grdAccesos.MasterTableView.Items
            TryCast(dataItem.FindControl(headerCheckBox.ID.Replace("_header", "")), CheckBox).Checked = headerCheckBox.Checked
            dataItem.Selected = headerCheckBox.Checked
            Dim keyValue As String = dataItem.GetDataKeyValue("IdPerfilPermisos").ToString()
        Next

        IdPerfil = cmbPerfil.SelectedValue

        Dim Tipo As String = ""
        Select Case headerCheckBox.ID
            Case "chkLectura_header"
                Tipo = "R"
                Lectura = headerCheckBox.Checked
            Case "chkEscritura_header"
                Tipo = "W"
                Escritura = headerCheckBox.Checked
            Case "chkEliminar_header"
                Tipo = "D"
                Eliminacion = headerCheckBox.Checked
        End Select

        ActualizarPermiso(Tipo)

    End Sub

    Private Sub ActualizarPermisos(ByVal Tipo As String)

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet()

        Dim p As New ArrayList
        p.Add(New SqlParameter("@IdPerfil", cmbPerfil.SelectedValue))
        Select Case Tipo
            Case "R"
                p.Add(New SqlParameter("@Lectura", Lectura))
            Case "W"
                p.Add(New SqlParameter("@Escritura", Escritura))
            Case "D"
                p.Add(New SqlParameter("@Eliminacion", Eliminacion))
        End Select

        If IdPerfilPermisos > 0 Then
            p.Add(New SqlParameter("@IdPerfilPermisos", IdPerfilPermisos))
        End If

        ObjData.ExecuteNonQueryWithParams("pPerfilPermiso_Actualizar", p)
        ObjData = Nothing

        grdAccesos.MasterTableView.NoMasterRecordsText = "No se encontraron perfiles para mostrar"
        grdAccesos.DataSource = CargaAccesos()
        grdAccesos.DataBind()

    End Sub

    Private Sub ActualizarPermiso(ByVal Tipo As String)

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet()

        Dim p As New ArrayList
        p.Add(New SqlParameter("@IdPerfil", cmbPerfil.SelectedValue))
        Select Case Tipo
            Case "R"
                p.Add(New SqlParameter("@Lectura", Lectura))
            Case "W"
                p.Add(New SqlParameter("@Escritura", Escritura))
            Case "D"
                p.Add(New SqlParameter("@Eliminacion", Eliminacion))
        End Select

        If IdPerfilPermisos > 0 Then
            p.Add(New SqlParameter("@IdPerfilPermisos", IdPerfilPermisos))
        End If

        ObjData.ExecuteNonQueryWithParams("pPerfilPermiso_Actualizar", p)
        ObjData = Nothing

    End Sub

    Private Sub cmbPerfil_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPerfil.SelectedIndexChanged
        grdAccesos.MasterTableView.NoMasterRecordsText = "No se encontraron perfiles para mostrar"
        grdAccesos.DataSource = CargaAccesos()
        grdAccesos.DataBind()
    End Sub

End Class