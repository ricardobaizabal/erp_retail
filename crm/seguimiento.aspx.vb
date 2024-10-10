Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization

Public Class seguimiento1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call LoadSeguimiento()
        End If
        '
        'AddWindow.Style.Add("z-index", "3500")
    End Sub
    Private Sub LoadSeguimiento()
        '
        seguimientoid.Value = 0
        '
        '
        Dim ObjData As New DataControl(1)
        ObjData.Catalogo(cmbTipoSeguimiento, "select id, nombre from tblCRMTipoSeguimiento order by id", 0)
        ObjData.Catalogo(cmbOportunidad, "exec pCRMOportunidades @cmd=10, @userid='" & Session("userid").ToString & "'", 0)
        'objData.Catalogo(cmbUsuario, "select id, nombre from tblusuario where perfilid in (1,6) and isnull(borradoBit,0)=0 order by nombre", 0, True)
        'objData = Nothing
        '
        calFechaInicio.DateInput.DateFormat = "dd/MM/yyyy HH:mm"
        'calFechaFin.DateInput.DateFormat = "dd/MM/yyyy HH:mm"
        calFechaInicio.TimeView.TimeFormat = "hh:mm tt"
        'calFechaFin.TimeView.TimeFormat = "hh:mm tt"
        calFechaInicio.SelectedDate = Now
        'calFechaFin.SelectedDate = Now
        '
        If Session("perfilid") > 1 Then
            cmbUsuario.Enabled = False
            ObjData.Catalogo(cmbUsuario, "select id, nombre from tblusuario where isnull(borradoBit,0)=0 and id='" & Session("userid").ToString & "' ", 1)
            RadScheduler1.DataSource = CargarSeguimiento()
            RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView
            RadScheduler1.DayStartTime = TimeSpan.Parse("14:00:00")
            RadScheduler1.DayEndTime = TimeSpan.Parse("00:00:00")
        Else
            RadScheduler1.DataSource = muestraSeguimiento()
            RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView
            RadScheduler1.DayStartTime = TimeSpan.Parse("14:00:00")
            RadScheduler1.DayEndTime = TimeSpan.Parse("00:00:00")
            'RadScheduler1.DataBind()

            ObjData.Catalogo(cmbUsuario, "select id, nombre from tblusuario where perfilid in (1,6) and isnull(borradoBit,0)=0 order by nombre", 0, True)
        End If
        '
        ObjData = Nothing
    End Sub
    Private Sub CargaDetalleSeguimiento()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCRMOportunidades @cmd=12, @seguimientoid='" & seguimientoid.Value.ToString & "'", conn)
        Dim ObjData As New DataControl(1)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtTitulo.Text = rs("titulo")
                ObjData.Catalogo(cmbTipoSeguimiento, "select id, nombre from tblCRMTipoSeguimiento order by id", rs("tipo_seguimiento"))
                ObjData.Catalogo(cmbOportunidad, "exec pCRMOportunidades @cmd=13, @userid='" & rs("usuarioid") & "'", rs("oportunidadid"))
                'cmbTipoSeguimiento.SelectedValue = rs("tipo_seguimiento")
                'cmbOportunidad.SelectedValue = rs("oportunidadid")
                txtDescripcionSeguimiento.Text = rs("descripcion")
                calFechaInicio.SelectedDate = rs("fecha_inicio")
                'calFechaFin.SelectedDate = rs("fecha_fin")
                'seguimientoid.Value = 0
            End If
        Catch ex As Exception
            '
            Response.Write(ex.Message.ToString())
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub
    Function muestraSeguimiento() As DataSet
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")

        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        ''Dim cmd As New SqlDataAdapter("EXEC pCRMOportunidades @cmd=9, @userid='" & Session("userid").ToString & "'", conn)
        Dim cmd As New SqlDataAdapter("EXEC pCRMOportunidades @cmd=9, @userid='" & cmbUsuario.SelectedValue.ToString & "'", conn)

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
    Function CargarSeguimiento() As DataSet
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pCRMOportunidades @cmd=9, @userid='" & Session("userid").ToString & "'", conn)
        'Dim cmd As New SqlDataAdapter("EXEC pCRMOportunidades @cmd=9, @userid='" & cmbUsuario.SelectedValue.ToString & "'", conn)

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
    Private Sub RadScheduler1_AppointmentCommand(sender As Object, e As Telerik.Web.UI.AppointmentCommandEventArgs) Handles RadScheduler1.AppointmentCommand
        Select Case e.CommandName
            Case "cmdEdit"
                seguimientoid.Value = e.CommandArgument
                Call CargaDetalleSeguimiento()
                AddWindow.VisibleOnPageLoad = True
        End Select
    End Sub

    Private Sub RadScheduler1_AppointmentCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.AppointmentCreatedEventArgs) Handles RadScheduler1.AppointmentCreated
        Dim lnkDetail As LinkButton = CType(e.Container.FindControl("lnkDetail"), LinkButton)
        Dim id As Long = e.Appointment.Attributes("id")
        Dim tipo As Integer = e.Appointment.Attributes("tipo_seguimientoid")
        If tipo = 1 Then
            lnkDetail.ForeColor = Drawing.ColorTranslator.FromHtml("#53A93F")
        ElseIf tipo = 2 Then
            lnkDetail.ForeColor = Drawing.ColorTranslator.FromHtml("#0000FF")
        ElseIf tipo = 3 Then
            lnkDetail.ForeColor = Drawing.ColorTranslator.FromHtml("#000000")
        End If
        If (e.Appointment.Attributes("userid") <> Session("userid")) Then
            e.Appointment.AllowDelete = False
        Else
            e.Appointment.AllowDelete = True
        End If
    End Sub

    Private Sub BtnAddClick(sender As Object, e As EventArgs) Handles btnAdd.Click
        AddWindow.VisibleOnPageLoad = True
    End Sub
    Private Sub BtnCancelSeguimientoClick(sender As Object, e As EventArgs) Handles btnCancelSeguimiento.Click
        AddWindow.VisibleOnPageLoad = False
    End Sub
    Private Sub BtnGuardarSeguimientoClick(sender As Object, e As EventArgs) Handles btnGuardarSeguimiento.Click
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)


        Dim fechaInicio As DateTime
        fechaInicio = calFechaInicio.SelectedDate

        'Dim fechaFin As DateTime
        'fechaFin = calFechaFin.SelectedDate


        'If fechaInicio > fechaFin Then
        '    'RadWindowManager2.RadAlert("La fecha de inicio no puede ser mayor a la fecha fin.", 330, 180, "Alert", "", "")
        '    lblMensajeFechas.Text = "La fecha de inicio no puede ser mayor a la fecha fin."
        '    lblMensajeFechas.ForeColor = Drawing.Color.Red
        '    Dim script As String = "<script type='text/javascript'>mensajefechas();</script>"
        '    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", script, False)
        '    Return
        'Else
        Try
            Dim sql As String = ""

            If seguimientoid.Value = 0 Then
                sql = "EXEC pCRMOportunidades @cmd=6, @titulo='" & txtTitulo.Text & "', @oportunidadid='" & cmbOportunidad.SelectedValue.ToString & "', @descripcion='" & txtDescripcionSeguimiento.Text & "', @fecha_inicio='" & fechaInicio.ToString("yyyyMMdd HH:mm") & "', @tipo_seguimientoid='" & cmbTipoSeguimiento.SelectedValue.ToString & "', @usuarioid='" & Session("userid").ToString() & "'"
            Else
                sql = "EXEC pCRMOportunidades @cmd=6, @seguimientoid='" & seguimientoid.Value.ToString & "', @titulo='" & txtTitulo.Text & "', @oportunidadid='" & cmbOportunidad.SelectedValue.ToString & "', @descripcion='" & txtDescripcionSeguimiento.Text & "', @fecha_inicio='" & fechaInicio.ToString("yyyyMMdd HH:mm") & "',  @tipo_seguimientoid='" & cmbTipoSeguimiento.SelectedValue.ToString & "', @usuarioid='" & Session("userid").ToString() & "'"
            End If

            Dim cmd As New SqlCommand(sql, conn)
            conn.Open()
            cmd.ExecuteReader()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        End Try
        txtTitulo.Text = ""
        cmbOportunidad.SelectedIndex = 0
        cmbTipoSeguimiento.SelectedIndex = 0
        calFechaInicio.SelectedDate = Now
        'calFechaFin.SelectedDate = Now
        txtDescripcionSeguimiento.Text = ""
        AddWindow.VisibleOnPageLoad = False
        If Session("perfilid") > 1 Then
            RadScheduler1.DataSource = CargarSeguimiento()
        Else
            RadScheduler1.DataSource = muestraSeguimiento()
        End If
        'RadScheduler1.DataSource = muestraSeguimiento()
        RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView
        RadScheduler1.DayStartTime = TimeSpan.Parse("14:00:00")
        RadScheduler1.DayEndTime = TimeSpan.Parse("00:00:00")
        RadScheduler1.DataBind()
        seguimientoid.Value = 0

        'End If


    End Sub
    Private Sub RadScheduler1_AppointmentDelete(sender As Object, e As Telerik.Web.UI.AppointmentDeleteEventArgs) Handles RadScheduler1.AppointmentDelete
        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("exec pCRMOportunidades @cmd=11, @appointmentID='" & e.Appointment.ID.ToString & "'")
        ObjData = Nothing
        If Session("perfilid") > 1 Then
            RadScheduler1.DataSource = CargarSeguimiento()
        Else
            RadScheduler1.DataSource = muestraSeguimiento()
        End If
        RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView
        RadScheduler1.DayStartTime = TimeSpan.Parse("14:00:00")
        RadScheduler1.DayEndTime = TimeSpan.Parse("00:00:00")
        RadScheduler1.DataBind()
    End Sub
    Private Sub cmbUsuario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUsuario.SelectedIndexChanged
        'Call LoadSeguimiento()
        seguimientoid.Value = 0
        '
        RadScheduler1.DataSource = muestraSeguimiento()
        RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView
        RadScheduler1.DayStartTime = TimeSpan.Parse("14:00:00")
        RadScheduler1.DayEndTime = TimeSpan.Parse("00:00:00")
        RadScheduler1.DataBind()
        'RadScheduler1.DataBind()
        '
        'Dim ObjData As New DataControl(1)
        'ObjData.Catalogo(cmbTipoSeguimiento, "select id, nombre from tblCRMTipoSeguimiento order by id", 0)
        'ObjData.Catalogo(cmbOportunidad, "exec pCRMOportunidades @cmd=10, @userid='" & Session("userid").ToString & "'", 0)

        'calFechaInicio.DateInput.DateFormat = "dd/MM/yyyy HH:mm"
        'calFechaFin.DateInput.DateFormat = "dd/MM/yyyy HH:mm"
        'calFechaInicio.TimeView.TimeFormat = "hh:mm tt"
        'calFechaFin.TimeView.TimeFormat = "hh:mm tt"
        'calFechaInicio.SelectedDate = Now
        'calFechaFin.SelectedDate = Now

    End Sub
End Class