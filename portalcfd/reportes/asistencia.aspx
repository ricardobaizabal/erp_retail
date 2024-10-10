<%@ Page Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="asistencia.aspx.vb" Inherits="LinkiumCFDI.asistencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("reporteAsistenciaGrid") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblReportsLegend" runat="server" Font-Bold="true" Text="Reporte de Asistencia" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item">
                        Seleccione el día que desee consultar:<br /><br />
                        <table border="0" style="width: 100%;">
                            <tr>
                                <td style="width: 8%;"><strong>Sucursal:</strong></td>
                                <td style="width: 20%;">
                                    <asp:DropDownList ID="cmbSucursal" runat="server"></asp:DropDownList>
                                </td>
                                <td style="width: 8%;"><strong>Empleado:</strong></td>
                                <td colspan="2">
                                    <asp:DropDownList ID="cmbEmpleado" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnGenerar" runat="server" CausesValidation="true" ValidationGroup="vgFecha" Text="Consultar" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 8%;"><strong>Desde:</strong></td>
                                <td style="width: 20%;">
                                    <telerik:RadDatePicker ID="calFechaIni" Width="120px" Runat="server" Skin="Office2007">
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width: 8%;"><strong>Hasta:</strong></td>
                                <td style="width: 20%;">
                                    <telerik:RadDatePicker ID="calFechaFin" Width="120px" Runat="server" Skin="Office2007">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RequiredFieldValidator ID="valFechaIni" runat="server" ControlToValidate="calFechaIni" CssClass="item" ForeColor="Red" ValidationGroup="vgFecha" ErrorMessage="Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:RequiredFieldValidator ID="valFechaFin" runat="server" ControlToValidate="calFechaFin" CssClass="item" ForeColor="Red" ValidationGroup="vgFecha" ErrorMessage="Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>                            
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
                <tr>
                    <td style="height: 5px">
                        <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="Small" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">                
                <asp:Label ID="lblRegistroLegend" runat="server" Font-Bold="true" Text="Registro de Asistencias" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item">
                        <telerik:RadGrid ID="reporteAsistenciaGrid" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true" ExportSettings-ExportOnlyData="True"
                            PageSize="50" ShowStatusBar="True" 
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <ExportSettings IgnorePaging="true" FileName="ReporteAsistencia">
                                <Excel Format="ExcelML" />
                            </ExportSettings>
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="empleadoid" Name="Asistencia" Width="100%" NoMasterRecordsText="No existen registros en ese rango de fechas." CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToExcelText="Exportar a Excel"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="sucursal_asistencia" HeaderText="Sucursal Asistencia" UniqueName="sucursal_asistencia">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="sucursal_empleado" HeaderText="Sucursal Empleado" UniqueName="sucursal_empleado">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="empleado" HeaderText="Empleado" UniqueName="empleado">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="hora1" HeaderText="Hora 1" UniqueName="hora1">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="hora2" HeaderText="Hora 2" UniqueName="hora2">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="hora3" HeaderText="Hora 3" UniqueName="hora3">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="hora4" HeaderText="Hora 4" UniqueName="hora4">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="tiempo2" HeaderText="Horas" DataFormatString="{0:N}" UniqueName="tiempo2">
                                        <ItemStyle HorizontalAlign=Right />
                                    </telerik:GridBoundColumn>--%>
                                    <%--<telerik:GridBoundColumn DataField="tiempo" HeaderText="Total horas, minutos" UniqueName="tiempo">
                                    </telerik:GridBoundColumn>--%>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
   </telerik:RadAjaxPanel>
   <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>