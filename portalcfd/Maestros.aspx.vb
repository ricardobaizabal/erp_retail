Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Public Class Maestros
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

            Dim ObjData As New DataControl(1)
            ObjData.FillRadComboBox(cmbGradoEscolar, "select a.id, b.nombre + ' - ' + a.nombre as grado from tblGradoEscolar a inner join tblNivelEscolar b on b.id=a.nivelescolarid where isnull(a.borradobit,0)=0 and isnull(b.borradobit,0)=0 order by b.nombre, a.nombre")
            ObjData.FillRadComboBox(cmbSucursal, "exec pCatalogos @cmd=11")
            ObjData.FillRadComboBox(cmbSucursalFiltro, "exec pCatalogos @cmd=11")
            ObjData = Nothing

            MaestrosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            MaestrosList.DataSource = ObtenerMaestros()
            MaestrosList.DataBind()
        End If
    End Sub

    Function ObtenerMaestros() As DataSet

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 5))
        p.Add(New SqlParameter("@txtSearch", txtSearch.Text))
        p.Add(New SqlParameter("@sucursalid", cmbSucursalFiltro.SelectedValue))
        ds = ObjData.FillDataSet("pMaestros", p)
        ObjData = Nothing

        Return ds

    End Function

    Private Sub MaestrosList_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles MaestrosList.NeedDataSource
        If Not e.IsFromDetailTable Then
            MaestrosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            MaestrosList.DataSource = ObtenerMaestros()
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        MaestrosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        MaestrosList.DataSource = ObtenerMaestros()
        MaestrosList.DataBind()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        txtNombreMaestro.Text = ""
        panelRegistration.Visible = False
        panelBusqueda.Visible = True
        Dim collectionGradoEscolar As IList(Of RadComboBoxItem) = cmbGradoEscolar.Items
        If (collectionGradoEscolar.Count <> 0) Then
            For Each item As RadComboBoxItem In collectionGradoEscolar
                item.Checked = False
            Next
        End If
        Dim collectionSucursal As IList(Of RadComboBoxItem) = cmbSucursal.Items
        If (collectionSucursal.Count <> 0) Then
            For Each item As RadComboBoxItem In collectionSucursal
                item.Checked = False
            Next
        End If
    End Sub

    Private Sub btnTodo_Click(sender As Object, e As EventArgs) Handles btnTodo.Click
        txtSearch.Text = ""
        cmbSucursalFiltro.SelectedValue = 0
        MaestrosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        MaestrosList.DataSource = ObtenerMaestros()
        MaestrosList.DataBind()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Dim conn As New SqlConnection(Session("conexion"))

        Dim sucursalid As Integer = 0

        If cmbSucursal.SelectedValue = "" Then
            sucursalid = 0
        Else
            sucursalid = cmbSucursal.SelectedValue
        End If

        Try

            If MaestroID.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pMaestros @cmd=1, @nombre='" & txtNombreMaestro.Text & "', @email='" & txtCorreoElectronico.Text & "', @sucursalid='" & sucursalid.ToString & "', @usuarioid='" & Session("userid").ToString & "'", conn)
                conn.Open()

                MaestroID.Value = cmd.ExecuteScalar()
                conn.Close()
                conn.Dispose()

            Else
                Dim cmd As New SqlCommand("EXEC pMaestros @cmd=4, @id='" & MaestroID.Value.ToString & "', @nombre='" & txtNombreMaestro.Text & "', @email='" & txtCorreoElectronico.Text & "', @sucursalid='" & sucursalid.ToString & "', @usuarioid='" & Session("userid").ToString & "'", conn)

                conn.Open()

                cmd.ExecuteReader()
                conn.Close()
                conn.Dispose()

            End If

            Dim ObjData As New DataControl(1)
            ObjData.RunSQLQuery("EXEC pMaestros @cmd=6, @id='" & MaestroID.Value.ToString & "'")

            For Each item As RadComboBoxItem In cmbGradoEscolar.CheckedItems
                ObjData.RunSQLQuery("EXEC pMaestros @cmd=7, @id='" & MaestroID.Value.ToString & "', @gradoescolarid='" & item.Value.ToString & "'")
            Next

            'ObjData.RunSQLQuery("EXEC pMaestros @cmd=9, @id='" & MaestroID.Value.ToString & "'")

            'For Each item As RadComboBoxItem In cmbSucursal.CheckedItems
            '    ObjData.RunSQLQuery("EXEC pMaestros @cmd=10, @id='" & MaestroID.Value.ToString & "', @sucursalid='" & item.Value.ToString & "'")
            'Next

            ObjData = Nothing

            MaestrosList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
            MaestrosList.DataSource = ObtenerMaestros()
            MaestrosList.DataBind()

            panelRegistration.Visible = False
            panelBusqueda.Visible = True

        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub btnAgregarMaestro_Click(sender As Object, e As EventArgs) Handles btnAgregarMaestro.Click
        txtNombreMaestro.Text = ""
        panelRegistration.Visible = True
        panelBusqueda.Visible = False
    End Sub

    Private Sub MaestrosList_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles MaestrosList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaMaestro(e.CommandArgument)

            Case "cmdDelete"
                EliminarMaestro(e.CommandArgument)

        End Select
    End Sub

    Private Sub EditaMaestro(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pMaestros @cmd=2, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtNombreMaestro.Text = rs("nombre")
                txtCorreoElectronico.Text = rs("email")
                txtContrasena.Text = rs("contrasena")
                cmbSucursal.SelectedValue = rs("sucursalid")
                MaestroID.Value = id
                panelBusqueda.Visible = False
                panelRegistration.Visible = True
            End If

            Dim dt As New DataTable
            Dim ObjData As New DataControl(1)
            dt = ObjData.FillDataTable("EXEC pMaestros @cmd=8, @id='" & id.ToString & "'")

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    Dim collection As IList(Of RadComboBoxItem) = cmbGradoEscolar.Items
                    If (collection.Count <> 0) Then
                        For Each item As RadComboBoxItem In collection
                            If item.Value = row("gradoescolarid") Then
                                item.Checked = True
                            End If
                        Next
                    End If
                Next
            End If

            'dt = ObjData.FillDataTable("EXEC pMaestros @cmd=11, @id='" & id.ToString & "'")

            'If dt.Rows.Count > 0 Then
            '    For Each row As DataRow In dt.Rows
            '        Dim collection As IList(Of RadComboBoxItem) = cmbSucursal.Items
            '        If (collection.Count <> 0) Then
            '            For Each item As RadComboBoxItem In collection
            '                If item.Value = row("sucursalid") Then
            '                    item.Checked = True
            '                End If
            '            Next
            '        End If
            '    Next
            'End If

            ObjData = Nothing

        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub EliminarMaestro(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pMaestros @cmd=3, @id=" & id.ToString, conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            'panelBusqueda.Visible = True
            panelRegistration.Visible = False

            MaestrosList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            MaestrosList.DataSource = ObtenerMaestros()
            MaestrosList.DataBind()

        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub MaestrosList_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles MaestrosList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Maestros" Then
                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('Va a borrar este registro. ¿Desea continuar?');")
            End If
        End If
    End Sub

End Class