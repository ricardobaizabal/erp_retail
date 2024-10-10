<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="seguimiento.aspx.vb" Inherits="erp_humantop.seguimiento1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .Hide {
            visibility: hidden !important;
            height: 0px !important;
        }

        html .RadScheduler .rsAptResize {
            cursor: default;
        }
    </style>
    <script type="text/javascript">
        function mensajefechas() {
            $('#dmensajefechas').slideToggle(2500);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadWindowManager ID="RadWindowManager2" runat="server">
        </telerik:RadWindowManager>
        <br />
        <fieldset style="padding: 5px;">
            <legend>
                <asp:Label ID="lblTituloVisitas" Font-Bold="true" Text="Tipo Seguimiento" CssClass="item" runat="server"></asp:Label>
            </legend>
            <table style="width: 100%;" border="0">
                <tr>
                    <td style="vertical-align: central; width: 30px;">
                        <div style="background-color: #53A93F; width: 10px; height: 10px"></div>
                    </td>
                    <td style="vertical-align: central; width: 100px;">
                        <strong class="item">Llamada</strong>
                    </td>
                    <td style="vertical-align: central; width: 30px;">
                        <div style="background-color: #0000FF; width: 10px; height: 10px"></div>
                    </td>
                    <td style="vertical-align: central; width: 100px;">
                        <strong class="item">Visita</strong>
                    </td>
                    <td style="vertical-align: central; width: 30px;">
                        <div style="background-color: #000000; width: 10px; height: 10px"></div>
                    </td>
                    <td style="vertical-align: central; width: 200px;">
                        <strong class="item">Enviar información</strong>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" CssClass="boton" Text="+ Agregar " />
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblUsuario" runat="server" Font-Bold="true" Text="Seleccionar Usuario:" class="textlogin"></asp:Label><br />
                </td>
                <td>
                    <asp:DropDownList ID="cmbUsuario" runat="server" Width="190px" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <telerik:RadScheduler ID="RadScheduler1" runat="server" Skin="Simple"
            DataKeyField="id"
            DataStartField="fecha_inicio"
            DataEndField="fecha_fin"
            DataSubjectField="titulo"
            Culture="Spanish (Mexico)"
            Height="600px" Width="100%" HoursPanelTimeFormat="htt"
            SelectedView="MonthView"
            ValidationGroup="RadScheduler1" AllowInsert="False" AllowDelete="True" AllowEdit="false" WorkDayEndTime="23:00:00" WorkDayStartTime="14:00:00"
            DisplayRecurrenceActionDialogOnMove="True" EditFormDateFormat="d/M/yyyy" Localization-HeaderToday="Hoy" Localization-HeaderDay="Día"
            Localization-HeaderWeek="Semana" Localization-HeaderMonth="Mes" Localization-HeaderTimeline="Linea Tiempo" CustomAttributeNames="id,tipo_seguimientoid,userid" DataDescriptionField="descripcion">
            <Localization AdvancedAllDayEvent="All day" ConfirmDeleteText="Va a borrar una entrada de la agenda. ¿Desea continuar?" Delete="" />
            <AdvancedForm DateFormat="d/M/yyyy" TimeFormat="h:mm tt" Modal="True" />
            <AppointmentTemplate>
                <div class="calendarItem">
                    <asp:LinkButton ID="lnkDetail" runat="server" Text='<%# Eval("Subject") %>' ToolTip='<%# Eval("Description") %>' CommandName="cmdEdit" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                </div>
            </AppointmentTemplate>
        </telerik:RadScheduler>

        <telerik:RadWindow ID="AddWindow" Behaviors="Close" VisibleOnPageLoad="false" Title="Agregar Appointment en la Agenda" Width="650px" Height="550px" runat="server">
            <ContentTemplate>
                <fieldset>
                    <br />
                    <legend style="padding-right: 6px; color: Black">
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/images/AgregarEditarProveedor_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="Label1" Text="Agregar Tarea / Cita" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                    </legend>
                    <br />
                    <asp:HiddenField ID="seguimientoid" runat="server" Value="0" />
                    <table border="0" style="width: 100%; line-height: 20px;">
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblTitulo" runat="server" Font-Bold="true" Text="Título:"></asp:Label><br />
                                <asp:TextBox ID="txtTitulo" runat="server" Width="600px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="valTitulo" runat="server" ValidationGroup="gpoSeguimiento" ForeColor="Red" SetFocusOnError="true" ErrorMessage="*" ControlToValidate="txtTitulo" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblOportunidad" runat="server" Font-Bold="true" Text="Oportunidad (CRM opcional):"></asp:Label><br />
                                <asp:DropDownList ID="cmbOportunidad" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoSeguimiento" runat="server" Text="Tipo Seguimiento:" CssClass="item" Font-Bold="True"></asp:Label><br />
                                <asp:DropDownList ID="cmbTipoSeguimiento" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="gpoSeguimiento" ForeColor="Red" SetFocusOnError="true" ErrorMessage="*" InitialValue="0" ControlToValidate="cmbTipoSeguimiento" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblInicio" runat="server" Text="Inicio:" CssClass="item" Font-Bold="True"></asp:Label>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="gpoSeguimiento" ForeColor="Red" SetFocusOnError="true" ErrorMessage="*" ControlToValidate="calFechaInicio" CssClass="item"></asp:RequiredFieldValidator><br />
                                <telerik:RadDateTimePicker ID="calFechaInicio" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy hh:mm:ss" runat="server">
                                    <Calendar ID="Calendar1" runat="server" EnableKeyboardNavigation="true">
                                    </Calendar>
                                    <TimeView Interval="00:15:00" StartTime="07:00" EndTime="23:00" runat="server">
                                    </TimeView>
                                </telerik:RadDateTimePicker>
                            </td>
<%--                            <td>
                                <asp:Label ID="lblFin" runat="server" Text="Fin:" CssClass="item" Font-Bold="True"></asp:Label>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="gpoSeguimiento" ForeColor="Red" SetFocusOnError="true" ErrorMessage="*" ControlToValidate="calFechaFin" CssClass="item"></asp:RequiredFieldValidator><br />
                                <telerik:RadDateTimePicker ID="calFechaFin" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy hh:mm:ss" runat="server">
                                    <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                                    </Calendar>
                                    <TimeView Interval="00:15:00" StartTime="07:00" EndTime="23:00" runat="server">
                                    </TimeView>
                                </telerik:RadDateTimePicker>
                            </td>--%>
                        </tr>

                        <tr valign="top">
                            <td colspan="3">
                                <asp:Label ID="lblDescripcionSeguimiento" runat="server" Text="Descripción:" CssClass="item" Font-Bold="True"></asp:Label><br />
                                <telerik:RadTextBox ID="txtDescripcionSeguimiento" TextMode="MultiLine" runat="server" Width="100%" Height="100px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td colspan="3">
                                <div id="dmensajefechas" style="display: none; width: 80%;" class="div">
                                    <asp:Label ID="lblMensajeFechas" runat="server" CssClass="item" ForeColor="#ffffff" Font-Bold="true" Font-Size="Small"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: right;">
                                <asp:Button ID="btnGuardarSeguimiento" runat="server" CausesValidation="true" ValidationGroup="gpoSeguimiento" Text="Guardar" CssClass="item" />&nbsp;
                    <asp:Button ID="btnCancelSeguimiento" runat="server" CausesValidation="false" Text="Cancelar" CssClass="item" />
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td colspan="3">&nbsp;</td>
                        </tr>
                    </table>
                </fieldset>
            </ContentTemplate>
        </telerik:RadWindow>

    </telerik:RadAjaxPanel>
</asp:Content>
