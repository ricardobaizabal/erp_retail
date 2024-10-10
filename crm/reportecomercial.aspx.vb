Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization

Public Class reportecomercial
    Inherits System.Web.UI.Page
    Private ds As DataSet
    Dim Valido As Boolean = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle
        If Not IsPostBack Then
            fha_ini.SelectedDate = DateAdd(DateInterval.Day, -7, Now())
            fha_fin.SelectedDate = Now()
            Call CargaCatalogos()
            If Session("perfilid") <> 1 Then
                Valido = FnValido()
                If Valido <> 0 Then
                    cmbUsuario.SelectedValue = Session("userid")
                    cmbUsuario.Enabled = True
                Else
                    cmbUsuario.SelectedValue = Session("userid")
                    cmbUsuario.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Sub CargaCatalogos()
        Dim ObjData As New DataControl(1)
        ObjData.Catalogo(cmbEtapa, "exec pCRMReporte @cmd=2", 0, True)
        ObjData.Catalogo(cmbUsuario, "exec pCRMReporte @cmd=3", 0, True)
        ObjData.Catalogo(cmbCategoria, "exec pCRMReporte @cmd=4", 0, True)
        ObjData = Nothing
    End Sub

    Private Sub OportunidadeslistItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles oportunidadeslist.ItemDataBound
        Select e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim imgEmail As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgEmail"), System.Web.UI.WebControls.Image)
                Dim imgTelefono As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgTelefono"), System.Web.UI.WebControls.Image)

                If e.Item.DataItem("email").ToString.Length > 0 Then
                    imgEmail.Visible = True
                    imgEmail.ToolTip = e.Item.DataItem("email").ToString
                End If

                If e.Item.DataItem("telefono").ToString.Length > 0 Then
                    imgTelefono.Visible = True
                    imgTelefono.ToolTip = e.Item.DataItem("telefono").ToString
                End If
        End Select
    End Sub

    Private Sub OportunidadeslistNeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles oportunidadeslist.NeedDataSource
        If Not e.IsFromDetailTable Then
            oportunidadeslist.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
            oportunidadeslist.DataSource = LeerReporte()
        End If
    End Sub

    Private Function LeerReporte() As DataSet
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim sql As String = ""
        sql = "exec pCRMReporte @cmd=1, @userid='" & cmbUsuario.SelectedValue.ToString() & "', @etapaid='" & cmbEtapa.SelectedValue.ToString & "', @fechaini='" & fha_ini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fha_fin.SelectedDate.Value.ToShortDateString & "',@categoriaid='" & cmbCategoria.SelectedValue.ToString & "'"

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter(sql, conn)
        Dim ds As DataSet = New DataSet
        Try
            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
        Return ds
    End Function

    Private Sub CmbEtapaSelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEtapa.SelectedIndexChanged
        oportunidadeslist.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
        oportunidadeslist.DataSource = LeerReporte()
        oportunidadeslist.DataBind()
    End Sub

    Private Sub CmbUsuarioSelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUsuario.SelectedIndexChanged
        oportunidadeslist.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
        oportunidadeslist.DataSource = LeerReporte()
        oportunidadeslist.DataBind()
    End Sub

    Private Sub BtnBuscarClick(sender As Object, e As EventArgs) Handles btnBuscar.Click
        oportunidadeslist.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
        oportunidadeslist.DataSource = LeerReporte()
        oportunidadeslist.DataBind()
    End Sub
    Private Function FnValido() As Boolean
        Dim Valido As Boolean = 0
        Dim conn As New SqlConnection(Session("conexion"))
        Try

            Dim cmd As New SqlCommand("EXEC pPermisosEspeciales @cmd=1, @IdPerfil='" & Session("perfilid").ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Valido = rs("bitCRM_ShowAllagents")
            Else
                Valido = 0
            End If

        Catch ex As Exception
            'Response.Write(ex.Message.ToString())
            Valido = 0
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return Valido
    End Function
End Class