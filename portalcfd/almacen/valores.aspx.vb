Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class valores
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            objCat.Catalogo(cmbAtributoFiltro, "select id, nombre from tblAtributos where isnull(borradobit,0)=0 order by nombre", 0)
            objCat.Catalogo(cmbAtributo, "select id, nombre from tblAtributos where isnull(borradobit,0)=0 order by nombre", 0)
            objCat = Nothing
        End If
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        ValorID.Value = 0
        txtNombre.Text = ""
        cmbAtributo.SelectedValue = 0
        panelRegistration.Visible = True
    End Sub

    Private Sub ValoresList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles ValoresList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaValor(e.CommandArgument)

            Case "cmdDelete"
                EliminaValor(e.CommandArgument)

        End Select
    End Sub

    Private Sub ValoresList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles ValoresList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Valores" Then
                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('Va a borrar este registro. ¿Desea continuar?');")
            End If
        End If
    End Sub

    Private Sub ValoresList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles ValoresList.NeedDataSource
        If Not e.IsFromDetailTable Then
            ValoresList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            ValoresList.DataSource = ObtenerValores()
        End If
    End Sub

    Function ObtenerValores() As DataSet

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 5))
        If cmbAtributoFiltro.SelectedValue > 0 Then
            p.Add(New SqlParameter("@atributoid", cmbAtributoFiltro.SelectedValue))
        End If

        ds = ObjData.FillDataSet("pCatalogoValorAtributo", p)
        ObjData = Nothing

        Return ds

    End Function

    Private Sub EditaValor(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoValorAtributo @cmd=2, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                cmbAtributo.SelectedValue = rs("atributoid")
                txtNombre.Text = rs("nombre")
                panelRegistration.Visible = True
                btnGuardar.Text = "Actualizar"
                ValorID.Value = id
            End If

        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub EliminaValor(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoValorAtributo @cmd=3, @id=" & id.ToString, conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistration.Visible = False

            ValoresList.DataSource = ObtenerValores()
            ValoresList.DataBind()

        Catch ex As Exception


        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim conn As New SqlConnection(Session("conexion"))

        Try

            If ValorID.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pCatalogoValorAtributo @cmd=1, @nombre='" & txtNombre.Text & "', @atributoid='" & cmbAtributo.SelectedValue.ToString & "'", conn)
                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

                ValoresList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                ValoresList.DataSource = ObtenerValores()
                ValoresList.DataBind()

                conn.Close()
                conn.Dispose()

            Else
                Dim cmd As New SqlCommand("EXEC pCatalogoValorAtributo @cmd=4, @nombre='" & txtNombre.Text & "', @id='" & ValorID.Value.ToString & "', @atributoid='" & cmbAtributo.SelectedValue.ToString & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

                ValoresList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                ValoresList.DataSource = ObtenerValores()
                ValoresList.DataBind()

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
        ValorID.Value = 0
        btnGuardar.Text = "Guardar"
        txtNombre.Text = ""
        cmbAtributo.SelectedValue = 0
        panelRegistration.Visible = False
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        ValoresList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
        ValoresList.DataSource = ObtenerValores()
        ValoresList.DataBind()
    End Sub

End Class