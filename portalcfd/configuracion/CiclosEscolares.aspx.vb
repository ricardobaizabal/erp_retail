Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class CiclosEscolares
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

        End If
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        CicloID.Value = 0
        txtNombre.Text = ""
        panelRegistration.Visible = True
    End Sub

    Private Sub CiclosEscolaresList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles CiclosEscolaresList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaCicloEscolar(e.CommandArgument)

            Case "cmdDelete"
                DeleteCicloEscolar(e.CommandArgument)

        End Select
    End Sub

    Private Sub CiclosEscolaresList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles CiclosEscolaresList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "CicloEscolar" Then
                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('Va a borrar este registro. ¿Desea continuar?');")
            End If
        End If
    End Sub

    Private Sub CiclosEscolaresList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles CiclosEscolaresList.NeedDataSource
        If Not e.IsFromDetailTable Then
            CiclosEscolaresList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            CiclosEscolaresList.DataSource = ObtenerCiclos()
        End If
    End Sub

    Function ObtenerCiclos() As DataSet

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 5))
        ds = ObjData.FillDataSet("pCatalogoCicloEscolar", p)
        ObjData = Nothing

        Return ds

    End Function

    Private Sub EditaCicloEscolar(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoCicloEscolar @cmd=2, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtNombre.Text = rs("nombre")
                panelRegistration.Visible = True
                btnGuardar.Text = "Actualizar"
                CicloID.Value = id
            End If

        Catch ex As Exception

        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub DeleteCicloEscolar(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoCicloEscolar @cmd=3, @id=" & id.ToString, conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistration.Visible = False

            CiclosEscolaresList.DataSource = ObtenerCiclos()
            CiclosEscolaresList.DataBind()

        Catch ex As Exception


        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim conn As New SqlConnection(Session("conexion"))

        Try

            If CicloID.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pCatalogoCicloEscolar @cmd=1, @nombre='" & txtNombre.Text & "'", conn)
                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

                CiclosEscolaresList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                CiclosEscolaresList.DataSource = ObtenerCiclos()
                CiclosEscolaresList.DataBind()

                conn.Close()
                conn.Dispose()

            Else
                Dim cmd As New SqlCommand("EXEC pCatalogoCicloEscolar @cmd=4, @nombre='" & txtNombre.Text & "', @id=" & CicloID.Value.ToString, conn)

                conn.Open()

                cmd.ExecuteReader()

                panelRegistration.Visible = False

                CiclosEscolaresList.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                CiclosEscolaresList.DataSource = ObtenerCiclos()
                CiclosEscolaresList.DataBind()

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
        CicloID.Value = 0
        btnGuardar.Text = "Guardar"
        txtNombre.Text = ""
        panelRegistration.Visible = False
    End Sub

End Class