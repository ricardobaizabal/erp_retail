Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO
Imports System.Net.Mail
Imports Telerik.Reporting.Processing
Imports Formatos
Imports System.Linq

Public Class oportunidades
    Inherits System.Web.UI.Page
    Private importe As Decimal = 0
    Private descuento As Decimal = 0
    Private subtotal As Decimal = 0
    Private iva As Decimal = 0
    Private total As Decimal = 0
    Dim Valido As Boolean = 0



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle
        calFechaCierre.SelectedDate = Now()
        ' calFechaCierre.MinDate = Now()
        'fha_fin.SelectedDate = Now()
        If Not IsPostBack Then
            Carga_Pestañas()
            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(cmbClienteProspecto, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by razonsocial", 0)
            ObjData.Catalogo(cmbMoneda, "select id, nombre from tblMoneda order by id", 0)
            ObjData.Catalogo(cmbProbabilidad, "select id, nombre from tblCRMProbabilidadVenta order by id", 0)

            ObjData.Catalogo(cmbFormaContacto, "select id, nombre from tblCRMFormaContacto order by id", 0)
            ObjData.Catalogo(cmbEtapas, "select id, nombre from tblCRMOportunidadEtapas order by id", 0)
            ObjData.Catalogo(cmbTipoSeguimiento, "select id, nombre from tblCRMTipoSeguimiento order by id", 0)
            'ObjData.Catalogo(cmdCategoria, "select id, descripcion from tblCategoriaFacturacion where isnull(borradobit,0)=0 order by descripcion", 0)
            ObjData.FillRadComboBoxMultiple(cmdCategorias, "select id, descripcion from tblCategoriaFacturacion where isnull(borradobit,0)=0 order by descripcion", 0)
            ObjData.Catalogo(cmbResponsable, "exec pCatalogos @cmd=12", 0, True)

            If Session("perfilid") <> 1 Then
                Valido = FnValido()
                If Valido <> 0 Then
                    cmbResponsable.SelectedValue = Session("userid")
                    cmbResponsable.Enabled = True
                Else
                    cmbResponsable.SelectedValue = Session("userid")
                    cmbResponsable.Enabled = False
                End If
            Else
                cmbResponsable.Enabled = True
                'ObjData.Catalogo(cmbResponsable, "select id, nombre from tblusuario where perfilid in (1,6) and isnull(borradoBit,0)=0 order by nombre", 0, True)
            End If
            ObjData = Nothing
            '
            calFechaInicio.DateInput.DateFormat = "dd/MM/yyyy HH:mm"
            'calFechaFin.DateInput.DateFormat = "dd/MM/yyyy HH:mm"
            calFechaInicio.TimeView.TimeFormat = "hh:mm tt"
            'calFechaFin.TimeView.TimeFormat = "hh:mm tt"
            '
            Dim data As New DataControl
            data.Catalogo(cmbVendedor, "select id,nombre from tblUsuario where clienteid = 9 and perfilid not in (1,4) and isnull(borradoBit,0)=0 order by id", 0)
            data = Nothing
        End If
    End Sub
    Protected Sub Carga_Pestañas()
        If OportunidadID.Value = 0 Then
            RadTabStrip1.Tabs(0).Selected = True
            RadTabStrip1.Tabs(1).Enabled = False
            RadTabStrip1.Tabs(2).Enabled = False
        Else
            RadTabStrip1.Tabs(0).Selected = True
            RadTabStrip1.Tabs(1).Enabled = True
            RadTabStrip1.Tabs(2).Enabled = True
        End If
    End Sub
    Protected Sub rblClienteProspecto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblClienteProspecto.SelectedIndexChanged
        Dim ObjData As New DataControl(1)
        If rblClienteProspecto.SelectedValue = 0 Then
            ObjData.Catalogo(cmbClienteProspecto, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by razonsocial", 0)
        Else
            ObjData.Catalogo(cmbClienteProspecto, "select id, isnull(razonsocial,'') as razonsocial from tblCRMProspecto where baja is null order by isnull(razonsocial,'')", 0)
        End If
        ObjData = Nothing
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/crm/oportunidades.aspx")
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim conn As New SqlConnection(Session("conexion"))

        Try
            Dim categoriasStr As String = ""
            If cmdCategorias.CheckedItems.Count > 0 Then
                For Each item In cmdCategorias.CheckedItems
                    categoriasStr += item.Value.ToString + ","
                Next
                categoriasStr = categoriasStr.Substring(0, categoriasStr.Length - 1)
            End If

            Dim FechaCierre As DateTime
            FechaCierre = calFechaCierre.SelectedDate

            If InsertOrUpdate.Value = 0 Then

                Dim sql As String = ""
                If calFechaCierre.SelectedDate.ToString.Length > 0 Then
                    If rblClienteProspecto.SelectedValue = 0 Then
                        sql = "EXEC pCRMOportunidades @cmd=4, @descripcion='" & txtDescripcion.Text & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @fecha_cierre='" & FechaCierre.ToString("yyyyMMdd") & "', @clienteid='" & cmbClienteProspecto.SelectedValue.ToString & "', @vendedorid='" & cmbVendedor.SelectedValue.ToString & "', @monto='" & txtMonto.Text & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @probabilidad_ventaid='" & cmbProbabilidad.SelectedValue.ToString & "', @forma_contactoid='" & cmbFormaContacto.SelectedValue.ToString & "', @etapaid='" & cmbEtapas.SelectedValue.ToString & "', @userid='" & Session("userid").ToString() & "', @categoriaid='" & categoriasStr & "'"
                    Else
                        sql = "EXEC pCRMOportunidades @cmd=4, @descripcion='" & txtDescripcion.Text & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @fecha_cierre='" & FechaCierre.ToString("yyyyMMdd") & "', @prospectoid='" & cmbClienteProspecto.SelectedValue.ToString & "', @vendedorid='" & cmbVendedor.SelectedValue.ToString & "', @monto='" & txtMonto.Text & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @probabilidad_ventaid='" & cmbProbabilidad.SelectedValue.ToString & "', @forma_contactoid='" & cmbFormaContacto.SelectedValue.ToString & "', @etapaid='" & cmbEtapas.SelectedValue.ToString & "', @userid='" & Session("userid").ToString() & "', @categoriaid='" & categoriasStr & "'"
                    End If
                Else
                    If rblClienteProspecto.SelectedValue = 0 Then
                        sql = "EXEC pCRMOportunidades @cmd=4, @descripcion='" & txtDescripcion.Text & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @clienteid='" & cmbClienteProspecto.SelectedValue.ToString & "', @vendedorid='" & cmbVendedor.SelectedValue.ToString & "', @monto='" & txtMonto.Text & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @probabilidad_ventaid='" & cmbProbabilidad.SelectedValue.ToString & "', @forma_contactoid='" & cmbFormaContacto.SelectedValue.ToString & "', @etapaid='" & cmbEtapas.SelectedValue.ToString & "', @userid='" & Session("userid").ToString() & "', @categoriaid='" & categoriasStr & "'"
                    Else
                        sql = "EXEC pCRMOportunidades @cmd=4, @descripcion='" & txtDescripcion.Text & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @prospectoid='" & cmbClienteProspecto.SelectedValue.ToString & "', @vendedorid='" & cmbVendedor.SelectedValue.ToString & "', @monto='" & txtMonto.Text & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @probabilidad_ventaid='" & cmbProbabilidad.SelectedValue.ToString & "', @forma_contactoid='" & cmbFormaContacto.SelectedValue.ToString & "', @etapaid='" & cmbEtapas.SelectedValue.ToString & "', @userid='" & Session("userid").ToString() & "', @categoriaid='" & categoriasStr & "'"
                    End If
                End If

                Dim cmd As New SqlCommand(sql, conn)

                conn.Open()

                cmd.ExecuteReader()

                panelRegistroOportunidad.Visible = False

                oportunidadesList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
                oportunidadesList.DataSource = ObtenerOportunidades()
                oportunidadesList.DataBind()

                conn.Close()
                conn.Dispose()

            Else

                Dim sql As String = ""
                If calFechaCierre.SelectedDate.ToString.Length > 0 Then
                    If rblClienteProspecto.SelectedValue = 0 Then
                        sql = "EXEC pCRMOportunidades @cmd=5, @oportunidadid='" & OportunidadID.Value & "', @descripcion='" & txtDescripcion.Text & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @fecha_cierre='" & FechaCierre.ToString("yyyyMMdd") & "', @clienteid='" & cmbClienteProspecto.SelectedValue.ToString & "', @vendedorid='" & cmbVendedor.SelectedValue.ToString & "', @monto='" & txtMonto.Text & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @probabilidad_ventaid='" & cmbProbabilidad.SelectedValue.ToString & "', @forma_contactoid='" & cmbFormaContacto.SelectedValue.ToString & "', @etapaid='" & cmbEtapas.SelectedValue.ToString & "',  @userid='" & Session("userid").ToString() & "', @categoriaid='" & categoriasStr & "'"
                    Else
                        sql = "EXEC pCRMOportunidades @cmd=5, @oportunidadid='" & OportunidadID.Value & "', @descripcion='" & txtDescripcion.Text & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @fecha_cierre='" & FechaCierre.ToString("yyyyMMdd") & "', @prospectoid='" & cmbClienteProspecto.SelectedValue.ToString & "', @vendedorid='" & cmbVendedor.SelectedValue.ToString & "', @monto='" & txtMonto.Text & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @probabilidad_ventaid='" & cmbProbabilidad.SelectedValue.ToString & "', @forma_contactoid='" & cmbFormaContacto.SelectedValue.ToString & "', @etapaid='" & cmbEtapas.SelectedValue.ToString & "', @userid='" & Session("userid").ToString() & "', @categoriaid='" & categoriasStr & "'"
                    End If

                Else
                    If rblClienteProspecto.SelectedValue = 0 Then
                        sql = "EXEC pCRMOportunidades @cmd=5, @oportunidadid='" & OportunidadID.Value & "', @descripcion='" & txtDescripcion.Text & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @clienteid='" & cmbClienteProspecto.SelectedValue.ToString & "', @vendedorid='" & cmbVendedor.SelectedValue.ToString & "', @monto='" & txtMonto.Text & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @probabilidad_ventaid='" & cmbProbabilidad.SelectedValue.ToString & "', @forma_contactoid='" & cmbFormaContacto.SelectedValue.ToString & "', @etapaid='" & cmbEtapas.SelectedValue.ToString & "', @userid='" & Session("userid").ToString() & "', @categoriaid='" & categoriasStr & "'"
                    Else
                        sql = "EXEC pCRMOportunidades @cmd=5, @oportunidadid='" & OportunidadID.Value & "', @descripcion='" & txtDescripcion.Text & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @prospectoid='" & cmbClienteProspecto.SelectedValue.ToString & "', @vendedorid='" & cmbVendedor.SelectedValue.ToString & "', @monto='" & txtMonto.Text & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @probabilidad_ventaid='" & cmbProbabilidad.SelectedValue.ToString & "', @forma_contactoid='" & cmbFormaContacto.SelectedValue.ToString & "', @etapaid='" & cmbEtapas.SelectedValue.ToString & "', @userid='" & Session("userid").ToString() & "', @categoriaid='" & categoriasStr & "'"
                    End If
                End If

                Dim cmd As New SqlCommand(sql, conn)

                conn.Open()

                cmd.ExecuteReader()

                panelRegistroOportunidad.Visible = False

                oportunidadesList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
                oportunidadesList.DataSource = ObtenerOportunidades()
                oportunidadesList.DataBind()

                conn.Close()
                conn.Dispose()

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        Response.Redirect("~/crm/oportunidades.aspx")
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        panelRegistroOportunidad.Visible = False
        oportunidadesList.MasterTableView.NoMasterRecordsText = "No se encontraron oportunidades para mostrar"
        oportunidadesList.DataSource = ObtenerOportunidades()
        oportunidadesList.DataBind()
    End Sub
    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        txtSearch.Text = ""
        panelRegistroOportunidad.Visible = False
        oportunidadesList.MasterTableView.NoMasterRecordsText = "No se encontraron oportunidades para mostrar"
        Dim ObjData As New DataControl(1)
        If Session("perfilid") <> 1 Then
            Valido = FnValido()
            If Valido <> 0 Then
                ObjData.Catalogo(cmbResponsable, "exec pCatalogos @cmd=5", 0, True)
            End If
        Else
            ObjData.Catalogo(cmbResponsable, "exec pCatalogos @cmd=5", 0, True)
        End If
        'ObjData.Catalogo(cmbResponsable, "select id, nombre from tblusuario where isnull(borradoBit,0)=0 and id='" & Session("userid").ToString & "' ", 1)
        ObjData = Nothing
        'ObjData.Catalogo(cmbClienteProspecto, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by razonsocial", rs("clienteid"))
        oportunidadesList.DataSource = ObtenerOportunidades()
        oportunidadesList.DataBind()
    End Sub
    Protected Sub btnAgregarOportunidad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarOportunidad.Click
        OportunidadID.Value = 0
        InsertOrUpdate.Value = 0
        panelRegistroOportunidad.Visible = True
        txtDescripcion.Text = ""
        rblClienteProspecto.SelectedValue = 0
        Dim ObjData As New DataControl(1)
        ObjData.Catalogo(cmbClienteProspecto, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by razonsocial", 0)
        ObjData.Catalogo(cmbMoneda, "select id, nombre from tblMoneda order by id", 0)
        ObjData.Catalogo(cmbProbabilidad, "select id, nombre from tblCRMProbabilidadVenta order by id", 0)
        Try
            cmbVendedor.SelectedValue = Session("userid")
        Catch ex As Exception
            cmbVendedor.SelectedValue = 0
        End Try
        'ObjData.Catalogo(cmbVendedor, "select id, nombre from tblUsuario where perfilid IN(6,27) and isnull(borradoBit,0)=0 order by nombre", Session("userid"))
        'ObjData.Catalogo(cmdCategoria, "select id, descripcion from tblCategoriaFacturacion where isnull(borradobit,0)=0 order by descripcion", 0)
        ObjData.FillRadComboBoxMultiple(cmdCategorias, "select id, descripcion from tblCategoriaFacturacion where isnull(borradobit,0)=0 order by descripcion", 0)
        'If Session("perfilid") > 1 Then
        '    cmbVendedor.Enabled = False
        'End If
        If Session("perfilid") <> 1 Then
            Valido = FnValido()
            If Valido <> 0 Then
                cmbResponsable.SelectedValue = Session("userid")
                cmbResponsable.Enabled = True
            Else
                cmbResponsable.SelectedValue = Session("userid")
                cmbResponsable.Enabled = False
            End If
        Else
            cmbResponsable.Enabled = True
        End If

        ObjData.Catalogo(cmbFormaContacto, "select id, nombre from tblCRMFormaContacto order by id", 0)
        ObjData.Catalogo(cmbEtapas, "select id, nombre from tblCRMOportunidadEtapas order by id", 0)
        ObjData = Nothing

        txtMonto.Text = "0"
        ' calFechaCierre.Clear()
        'txtComision.Text = "0"
        Call Carga_Pestañas()

    End Sub
    Function ObtenerOportunidades() As DataSet
        Dim sql As String = ""
        If Session("perfilid") <> 1 Then
            Valido = FnValido()
            If Valido <> 0 Then
                sql = "EXEC pCRMOportunidades @cmd=1, @txtSearch='" & txtSearch.Text & "', @userid='" & cmbResponsable.SelectedValue.ToString & "' "
            Else
                sql = "EXEC pCRMOportunidades @cmd=1, @txtSearch='" & txtSearch.Text & "', @userid='" & Session("userid").ToString() & "'"
            End If
        Else
            sql = "EXEC pCRMOportunidades @cmd=1, @txtSearch='" & txtSearch.Text & "', @userid='" & cmbResponsable.SelectedValue.ToString & "' "
        End If
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

        Return ds

    End Function
    Function ObtenerSeguimiento() As DataSet
        Dim sql As String = ""
        If Session("perfilid") <> 1 Then
            sql = "EXEC pCRMOportunidades @cmd=7, @oportunidadid='" & OportunidadID.Value & "', @userid='" & Session("userid").ToString() & "'"
        Else
            sql = "EXEC pCRMOportunidades @cmd=7, @oportunidadid='" & OportunidadID.Value & "'"
        End If

        'Dim conn2 As New SqlConnection(Session("conexion"))
        'Dim cmd2 As New SqlCommand("EXEC pCRMOportunidades @cmd=7, @oportunidadid='" & OportunidadID.Value & "'", conn2)
        'conn2.Open()

        'Dim rs As SqlDataReader
        'rs = cmd2.ExecuteReader()

        'If rs.Read Then
        '    Session("negociacionBit") = rs("negociacionBit")

        'End If
        'conn2.Close()
        'conn2.Dispose()
        'conn2 = Nothing
        If Session("negociacionBit") = True Then

        End If
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pCRMOportunidades @cmd=7, @oportunidadid='" & OportunidadID.Value & "'", conn)

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

        Return ds

    End Function
    Function ObtenerCotizaciones() As DataSet
        Dim sql As String = ""
        If Session("perfilid") <> 1 Then
            sql = "EXEC pCRMCotizacion @cmd=14, @oportunidadid='" & OportunidadID.Value & "', @userid='" & Session("userid").ToString() & "'"
        Else
            sql = "EXEC pCRMCotizacion @cmd=14, @oportunidadid='" & OportunidadID.Value & "'"
        End If
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

        Return ds

    End Function
    Protected Sub oportunidadesList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles oportunidadesList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Oportunidades" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea borrar de la base de datos esta oportunidad?');")

            End If

        End If
    End Sub
    Protected Sub oportunidadesList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles oportunidadesList.ItemCommand
        Select Case e.CommandName

            Case "cmdEdit"

                EditaOportunidad(e.CommandArgument)

                seguimientoList.MasterTableView.NoMasterRecordsText = "No se encontraron elementos para mostrar"
                seguimientoList.DataSource = ObtenerSeguimiento()
                seguimientoList.DataBind()

                cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron cotizaciones para mostrar"
                cotizacionesList.DataSource = ObtenerCotizaciones()
                cotizacionesList.DataBind()

            Case "cmdDelete"
                EliminaOportunidad(e.CommandArgument)

        End Select
    End Sub
    Private Sub EliminaOportunidad(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCRMOportunidades @cmd=3, @oportunidadid='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistroOportunidad.Visible = False

            oportunidadesList.MasterTableView.NoMasterRecordsText = "No se encontraron oportunidades para mostrar"
            oportunidadesList.DataSource = ObtenerOportunidades()
            oportunidadesList.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub
    Private Sub EditaOportunidad(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCRMOportunidades @cmd=2, @oportunidadid='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then


                txtDescripcion.Text = rs("descripcion")
                lblTituloOportunidadSeguimiento.Text = rs("descripcion")
                lblTituloOportunidadCotizaciones.Text = rs("descripcion")
                rblClienteProspecto.SelectedValue = rs("tipo_cliente")

                Dim ObjData As New DataControl(1)
                If rblClienteProspecto.SelectedValue = 0 Then
                    ObjData.Catalogo(cmbClienteProspecto, "select id, UPPER(isnull(razonsocial,'')) from tblMisClientes where len(isnull(razonsocial,'')) >0 order by razonsocial", rs("clienteid"))
                Else
                    ObjData.Catalogo(cmbClienteProspecto, "select id, isnull(razonsocial,'') as razonsocial from tblCRMProspecto where baja is null order by id", rs("prospectoid"))
                End If

                ObjData.Catalogo(cmbMoneda, "select id, nombre from tblMoneda order by id", rs("monedaid"))
                ObjData.Catalogo(cmbProbabilidad, "select id, nombre from tblCRMProbabilidadVenta order by id", rs("probabilidad_ventaid"))
                cmbVendedor.SelectedValue = rs("vendedorid")
                'ObjData.Catalogo(cmbVendedor, "select id, nombre from tblUsuario where perfilid IN(6,27) and isnull(borradoBit,0)=0 order by nombre", rs("vendedorid"))
                'ObjData.Catalogo(cmdCategoria, "select id, descripcion from tblCategoriaFacturacion where isnull(borradobit,0)=0 order by descripcion", rs("categoriaid"))
                ObjData.FillRadComboBoxMultiple(cmdCategorias, "select id, descripcion from tblCategoriaFacturacion where isnull(borradobit,0)=0 order by descripcion", 0)
                'If Session("perfilid") > 1 Then
                '    'cmbVendedor.Enabled = False
                'End If
                If Session("perfilid") <> 1 Then
                    Valido = FnValido()
                    If Valido <> 0 Then
                        cmbResponsable.SelectedValue = Session("userid")
                        cmbResponsable.Enabled = True
                    Else
                        cmbResponsable.SelectedValue = Session("userid")
                        cmbResponsable.Enabled = False
                    End If
                Else
                    cmbResponsable.Enabled = True
                End If

                ObjData.Catalogo(cmbFormaContacto, "select id, nombre from tblCRMFormaContacto order by id", rs("forma_contactoid"))
                ObjData.Catalogo(cmbEtapas, "select id, nombre from tblCRMOportunidadEtapas order by id", rs("etapaid"))
                ObjData = Nothing

                txtMonto.Text = rs("monto")
                'txtComision.Text = rs("comision")

                If rs("fecha_cierre").ToString.Length > 0 Then
                    calFechaCierre.SelectedDate = CDate(rs("fecha_cierre"))
                End If

                Dim value As String = rs("categoriaid")
                ' Split string on comma characters.
                ' ... Remove empty elements from result.

                Dim elements() As String = value.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)


                Dim elementos As New List(Of Int32)

                For i As Integer = 0 To elements.Count - 1
                    elementos.Add(CInt(elements(i)))
                Next

                Dim collection As IList(Of RadComboBoxItem) = cmdCategorias.Items
                For Each item As RadComboBoxItem In collection
                    For Each itm As Int32 In elementos
                        If item.Value = itm Then
                            item.Checked = True
                        End If
                    Next
                Next


                panelRegistroOportunidad.Visible = True

                InsertOrUpdate.Value = 1
                OportunidadID.Value = id
                Carga_Pestañas()

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub
    Protected Sub oportunidadesList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles oportunidadesList.NeedDataSource
        oportunidadesList.MasterTableView.NoMasterRecordsText = "No se encontraron oportunidades para mostrar"
        oportunidadesList.DataSource = ObtenerOportunidades()
    End Sub
    Protected Sub btnGuardarSeguimiento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarSeguimiento.Click
        Dim conn As New SqlConnection(Session("conexion"))
        Try
            Dim sql As String = ""
            Dim sql2 As String = ""
            Dim sql3 As String = ""
            Dim FechaInicio As DateTime
            FechaInicio = calFechaInicio.SelectedDate

            'Dim FechaFin As DateTime
            'FechaFin = calFechaFin.SelectedDate

            'If FechaInicio > FechaFin Then
            '    lblMensajeFechas.Text = "La fecha de inicio no puede ser mayor a la fecha fin."
            '    lblMensajeFechas.ForeColor = Drawing.Color.Red
            '    Dim script As String = "<script type='text/javascript'>mensajefechas();</script>"
            '    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", script, False)
            '    Return
            'End If
            If cmbEtapas.SelectedValue < 2 And cmbTipoSeguimiento.SelectedValue = 2 And ChkNegociacion.Checked = False Then
                Dim conn2 As New SqlConnection(Session("conexion"))

                sql2 = "EXEC pCRMOportunidades @cmd=15,@oportunidadid='" & OportunidadID.Value & "'"

                Dim cmd2 As New SqlCommand(sql2, conn2)
                conn2.Open()
                cmd2.ExecuteReader()

                conn2.Close()
                conn2.Dispose()
                oportunidadesList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
                oportunidadesList.DataSource = ObtenerOportunidades()
                oportunidadesList.DataBind()
            End If

            If ChkNegociacion.Checked = True And cmbEtapas.SelectedValue < 4 Then
                Dim conn3 As New SqlConnection(Session("conexion"))

                sql3 = "EXEC pCRMOportunidades @cmd=16,@oportunidadid='" & OportunidadID.Value & "'"

                Dim cmd3 As New SqlCommand(sql3, conn3)
                conn3.Open()
                cmd3.ExecuteReader()

                conn3.Close()
                conn3.Dispose()
                oportunidadesList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
                oportunidadesList.DataSource = ObtenerOportunidades()
                oportunidadesList.DataBind()
            End If

            If rblClienteProspecto.SelectedValue = 0 Then
                sql = "EXEC pCRMOportunidades @cmd=14, @oportunidadid='" & OportunidadID.Value & "', @descripcion='" & txtDescripcionSeguimiento.Text & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @fecha_inicio='" & FechaInicio.ToString("yyyyMMdd HH:mm") & "', @clienteid='" & cmbClienteProspecto.SelectedValue.ToString & "', @tipo_seguimientoid='" & cmbTipoSeguimiento.SelectedValue.ToString & "', @usuarioid='" & Session("userid").ToString() & "', @negociacionBit='" & ChkNegociacion.Checked & "'"
            Else
                sql = "EXEC pCRMOportunidades @cmd=14, @oportunidadid='" & OportunidadID.Value & "', @descripcion='" & txtDescripcionSeguimiento.Text & "', @tipo_cliente='" & rblClienteProspecto.SelectedValue.ToString & "', @fecha_inicio='" & FechaInicio.ToString("yyyyMMdd HH:mm") & "', @prospectoid='" & cmbClienteProspecto.SelectedValue.ToString & "', @tipo_seguimientoid='" & cmbTipoSeguimiento.SelectedValue.ToString & "', @usuarioid='" & Session("userid").ToString() & "', @negociacionBit='" & ChkNegociacion.Checked & "'"
            End If

            Dim cmd As New SqlCommand(sql, conn)
            'Dim cmd2 As New SqlCommand(sql2, conn)
            conn.Open()
            cmd.ExecuteReader()

            conn.Close()
            conn.Dispose()

            ClearItems()
            panelRegistroOportunidad.Visible = False
            seguimientoList.MasterTableView.NoMasterRecordsText = "No se encontraron elementos para mostrar"
            seguimientoList.DataSource = ObtenerSeguimiento()
            seguimientoList.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        End Try

    End Sub
    Protected Sub btnCancelSeguimiento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelSeguimiento.Click
        ClearItems()
    End Sub
    Private Sub ClearItems()
        cmbTipoSeguimiento.SelectedValue = 0
        calFechaInicio.Clear()
        'calFechaFin.Clear()
        txtDescripcionSeguimiento.Text = ""
    End Sub
    Protected Sub seguimientoList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles seguimientoList.ItemCommand
        Select Case e.CommandName

            Case "cmdDelete"
                EliminaSeguimiento(e.CommandArgument)

        End Select
    End Sub
    Protected Sub seguimientoList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles seguimientoList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim imgNegociacion As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgNegociacion"), System.Web.UI.WebControls.Image)
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.DataItem("negociacionBit") = True Then
                imgNegociacion.Visible = True

            Else
                imgNegociacion.Visible = False
            End If

            If e.Item.OwnerTableView.Name = "Seguimiento" Then

                    Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                    lnkdel.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea borrar de la base de datos este seguimiento?');")

                End If

            End If
    End Sub
    Private Sub EliminaSeguimiento(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCRMOportunidades @cmd=8, @seguimientoid='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            seguimientoList.MasterTableView.NoMasterRecordsText = "No se encontraron elementos para mostrar"
            seguimientoList.DataSource = ObtenerSeguimiento()
            seguimientoList.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub
    Protected Sub cotizacionesList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles cotizacionesList.NeedDataSource
        cotizacionesList.MasterTableView.NoMasterRecordsText = "No se encontraron cotizaciones para mostrar"
        cotizacionesList.DataSource = ObtenerCotizaciones()
    End Sub
    Protected Sub seguimientoList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles seguimientoList.NeedDataSource
        seguimientoList.MasterTableView.NoMasterRecordsText = "No se encontraron elementos para mostrar"
        seguimientoList.DataSource = ObtenerSeguimiento()
    End Sub
    Protected Sub cotizacionesList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles cotizacionesList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Cotizaciones" Then

                Dim btnDownload As ImageButton = CType(e.Item.FindControl("btnDownload"), ImageButton)
                Dim imgSend As ImageButton = CType(e.Item.FindControl("imgSend"), ImageButton)

                Dim lblPartidas As Label = CType(e.Item.FindControl("lblPartidas"), Label)

                If Convert.ToInt32(lblPartidas.Text) > 0 Then
                    btnDownload.Visible = True
                    imgSend.Visible = True
                Else
                    'btnDownload.Visible = False
                    imgSend.Visible = False
                End If

            End If

        End If
    End Sub
    Protected Sub cotizacionesList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles cotizacionesList.ItemCommand
        Select Case e.CommandName
            Case "cmdDownload"
                Call DownloadPDF(e.CommandArgument)
            Case "cmdSend"
                Try
                    SendEmail(e.CommandArgument)
                    lblMensajeGuardar.Text = "Cotización enviada exitosamente"
                    lblMensajeGuardar.ForeColor = Drawing.Color.Green
                    Dim script As String = "<script type='text/javascript'>mensaje();</script>"
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", script, False)
                Catch ex As Exception
                    lblMensajeGuardar.Text = "Error: " & ex.Message.ToString
                    lblMensajeGuardar.ForeColor = Drawing.Color.Red
                    Dim script As String = "<script type='text/javascript'>mensaje();</script>"
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", script, False)
                End Try
        End Select
    End Sub
    Private Sub DownloadPDF(ByVal cotizacionid As Long)
        Dim FilePath = Server.MapPath("~/crm/cotizaciones/Cotizacion_") & cotizacionid.ToString & ".pdf"

        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        Else

        End If
    End Sub
    Private Sub CargaTotalesPDF(ByVal cotizacionid As Integer)
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlCommand("exec pCRMCotizacion @cmd=9, @cotizacionid='" & cotizacionid.ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                importe = rs("importe")
                descuento = rs("descuento")
                subtotal = rs("subtotal")
                iva = rs("iva")
                total = rs("total")
            End If
        Catch ex As Exception
            '
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub
    Private Sub SendEmail(ByVal cotizacionid As Long)
        '
        '
        '   Obtiene datos de la persona
        '
        Dim mensaje As String = ""
        Dim razonsocial As String = ""
        Dim email_to As String = ""
        Dim email_from As String = "rbaizabal@linkium.mx"
        'Dim email_smtp_server As String = ""
        'Dim email_smtp_username As String = ""
        'Dim email_smtp_password As String = ""
        'Dim email_smtp_port As String = ""
        '
        Dim conn As New SqlConnection(Session("conexion"))
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pCRMCotizacion @cmd=13, @cotizacionid='" & cotizacionid.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            '       
            razonsocial = rs("razon_social")
            email_to = rs("email_to")
            'email_from = rs("email_from")
            'email_smtp_server = rs("email_smtp_server")
            'email_smtp_username = rs("email_smtp_username")
            'email_smtp_password = rs("email_smtp_password")
            'email_smtp_port = rs("email_smtp_port")
            '
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
        '
        '
        mensaje = "<html><head></head><body style='font-size: 10pt; color: #000000; font-family: Verdana;'><br />"
        mensaje += "Estimado(a) Cliente, por este medio se le anexa la cotización solicitada.<br /><br />Gracias por su preferencia."

        mensaje += "<br /><br />"
        mensaje += "Atentamente.<br /><br />"
        mensaje += "<strong>" & razonsocial.ToString & "</strong><br /><br /></body></html>"

        Dim objMM As New MailMessage
        objMM.To.Add(email_to)
        objMM.From = New MailAddress(email_from, razonsocial)
        objMM.IsBodyHtml = True
        objMM.Priority = MailPriority.Normal
        objMM.Subject = razonsocial & " - Cotización No. " & cotizacionid.ToString
        objMM.Body = mensaje
        '
        '   Agrega anexos
        '
        Dim FilePath = Server.MapPath("~/crm/cotizaciones/Cotizacion_") & cotizacionid.ToString & ".pdf"

        If Not File.Exists(FilePath) Then
            Call CargaTotalesPDF(cotizacionid)
            GuardaPDF(GeneraPDF(cotizacionid), FilePath)
        End If

        Dim AttachPDF As Net.Mail.Attachment = New Net.Mail.Attachment(FilePath)
        '
        objMM.Attachments.Add(AttachPDF)
        '
        Dim SmtpMail As New SmtpClient
        Try
            Dim SmtpUser As New Net.NetworkCredential
            SmtpUser.UserName = "enviosweb@linkium.mx"
            SmtpUser.Password = "Link2020"
            SmtpMail.UseDefaultCredentials = False
            SmtpMail.Credentials = SmtpUser
            SmtpMail.Host = "smtp.linkium.mx"
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
            SmtpMail.Send(objMM)
        Catch ex As Exception
            '
            '
        Finally
            SmtpMail = Nothing
        End Try
        objMM = Nothing
        ''
    End Sub
    Private Function GeneraPDF(ByVal cotizacionid As Long) As Telerik.Reporting.Report
        Dim conn As New SqlConnection(Session("conexion"))

        Dim no_cotizacion As Integer = 0
        Dim tipo_cliente As Integer = 0
        Dim cliente As String = ""
        Dim prospecto As String = ""
        Dim fecha_cotizacion As String = ""
        Dim condiciones As String = ""
        Dim monedaid = 1
        Dim moneda As String = ""
        Dim tipo_cambio As String = ""

        Dim ds As DataSet = New DataSet

        Try

            Dim cmd As New SqlCommand("EXEC pCRMCotizacion @cmd=11, @cotizacionid='" & cotizacionid.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                no_cotizacion = rs("id")
                cliente = rs("cliente")
                fecha_cotizacion = rs("fecha_alta")
                condiciones = rs("condiciones")
                monedaid = rs("monedaid")
                moneda = rs("moneda")
                tipo_cambio = rs("tipocambio")
            End If
            rs.Close()
            '
        Catch ex As Exception
            '
            Response.Write(ex.ToString)
        Finally

            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        Dim reporte As New FormatosPDF.formato_crm_cotizacion
        reporte.ReportParameters("plantillaId").Value = 3
        reporte.ReportParameters("cotizacionid").Value = cotizacionid
        reporte.ReportParameters("paramImgBanner").Value = "logo.jpg"
        'InterjoyaBaner
        '
        reporte.ReportParameters("txtNoCotizacion").Value = cotizacionid
        reporte.ReportParameters("txtClienteProspecto").Value = cliente
        reporte.ReportParameters("txtFechaCotizacion").Value = fecha_cotizacion
        reporte.ReportParameters("txtCondicionesPago").Value = condiciones
        reporte.ReportParameters("txtMoneda").Value = moneda
        If monedaid <> 1 Then
            reporte.ReportParameters("txtEtiquetaTipoCambio").Value = "Tipo cambio:"
            reporte.ReportParameters("txtTipoCambio").Value = FormatCurrency(tipo_cambio, 2).ToString
        Else
            reporte.ReportParameters("txtEtiquetaTipoCambio").Value = ""
            reporte.ReportParameters("txtTipoCambio").Value = ""
        End If
        '
        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
        reporte.ReportParameters("txtDescuento").Value = FormatCurrency(descuento, 2).ToString
        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(subtotal, 2).ToString
        reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA"
        reporte.ReportParameters("txtIva").Value = FormatCurrency(iva, 2).ToString
        reporte.ReportParameters("txtTotal").Value = FormatCurrency(total, 2).ToString

        Return reporte

    End Function
    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub
    Private Sub cmbResponsable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbResponsable.SelectedIndexChanged
        oportunidadesList.MasterTableView.NoMasterRecordsText = "No se encontraron oportunidades para mostrar"
        oportunidadesList.DataSource = ObtenerOportunidades()
        oportunidadesList.DataBind()
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