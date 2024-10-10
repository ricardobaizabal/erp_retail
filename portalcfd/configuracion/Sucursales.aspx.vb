Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class Sucursales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

        End If
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        SucursalID.Value = 0
        txtNombre.Text = ""
        panelRegistration.Visible = True
    End Sub

    Private Sub SucursalesList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles SucursalesList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaSucursal(e.CommandArgument)

            Case "cmdDelete"
                DeleteSucursal(e.CommandArgument)

        End Select
    End Sub

    Private Sub SucursalesList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles SucursalesList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Sucursales" Then
                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('Va a borrar este registro. ¿Desea continuar?');")
            End If
        End If
    End Sub

    Private Sub SucursalesList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles SucursalesList.NeedDataSource
        If Not e.IsFromDetailTable Then
            SucursalesList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            SucursalesList.DataSource = ObtenerSucursales()
        End If
    End Sub

    Function ObtenerSucursales() As DataSet

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 5))
        ds = ObjData.FillDataSet("pCatalogoSucursal", p)
        ObjData = Nothing

        Return ds

    End Function

    Private Sub EditaSucursal(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoSucursal @cmd=2, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtNombre.Text = rs("nombre")
                panelRegistration.Visible = True
                btnGuardar.Text = "Actualizar"
                SucursalID.Value = id
            End If

        Catch ex As Exception

        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub DeleteSucursal(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoSucursal @cmd=3, @id=" & id.ToString, conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistration.Visible = False

            SucursalesList.DataSource = ObtenerSucursales()
            SucursalesList.DataBind()

        Catch ex As Exception

        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim conn As New SqlConnection(Session("conexion"))

        Try

            If SucursalID.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pCatalogoSucursal @cmd=1, @nombre='" & txtNombre.Text & "'", conn)
                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

                SucursalesList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                SucursalesList.DataSource = ObtenerSucursales()
                SucursalesList.DataBind()

                conn.Close()
                conn.Dispose()

            Else
                Dim cmd As New SqlCommand("EXEC pCatalogoSucursal @cmd=4, @nombre='" & txtNombre.Text & "', @id=" & SucursalID.Value.ToString, conn)

                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

                SucursalesList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                SucursalesList.DataSource = ObtenerSucursales()
                SucursalesList.DataBind()

                conn.Close()
                conn.Dispose()

            End If

        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        SucursalID.Value = 0
        btnGuardar.Text = "Guardar"
        txtNombre.Text = ""
        panelRegistration.Visible = False
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim ObjData As New DataControl(1)
        For Each dataItem As Telerik.Web.UI.GridDataItem In SucursalesList.MasterTableView.Items
            Dim sucursalid As Integer = dataItem.GetDataKeyValue("id")
            Dim txtEfectivo As RadNumericTextBox = DirectCast(dataItem.FindControl("txtEfectivo"), RadNumericTextBox)

            Dim p As New ArrayList
            p.Add(New SqlParameter("@cmd", 6))
            p.Add(New SqlParameter("@id", sucursalid))
            p.Add(New SqlParameter("@efectivo", txtEfectivo.Text))
            ObjData.ExecuteNonQueryWithParams("pCatalogoSucursal", p)
        Next
        ObjData = Nothing

        SucursalesList.DataSource = ObtenerSucursales()
        SucursalesList.DataBind()

        rwAlerta.RadAlert("Topes de efectivo actualizados.", 330, 180, "Alerta", "", "")

    End Sub

End Class