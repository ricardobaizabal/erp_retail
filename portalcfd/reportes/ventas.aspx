<%@ Page Title="" Language="vb" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="ventas.aspx.vb" Inherits="LinkiumCFDI.ventas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("reporteGrid") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">--%>
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblReportsLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%" border="0">
            <tr>
                <td style="height: 5px"></td>
            </tr>
            <tr>
                <td class="item">Seleccione el rango de fechas que desee consultar:<br />
                    <br />
                    <table style="width: 100%;" border="0" cellpadding="5">
                        <tr>
                            <td style="width: 5%; font-weight: bold;">Sucursal: </td>
                            <td colspan="3">
                                <asp:DropDownList ID="cmbSucursal" runat="server"></asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 5%; font-weight: bold;">Desde: </td>
                            <td style="width: 10%;">
                                <telerik:RadDatePicker ID="fechaini" runat="server" Skin="Web20" Width="120px">
                                    <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False"
                                        UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>
                            <td style="width: 5%; font-weight: bold;">Hasta: </td>
                            <td style="width: 10%;">
                                <telerik:RadDatePicker ID="fechafin" runat="server" Skin="Web20" Width="120px">
                                    <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False"
                                        UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>
                            <td style="width: 10%;" align="right">
                                <asp:Button ID="btnGenerate" runat="server" Text="Consultar" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 5px"></td>
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
            <asp:Label ID="Label1" runat="server" Text="Venta total por sucursal" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td style="height: 5px" colspan="5"></td>
            </tr>
            <tr>
                <td class="item" colspan="5">
                    <telerik:RadGrid ID="reporteGrid" runat="server" AllowPaging="false" ShowHeader="true"
                        AutoGenerateColumns="True" GridLines="None" ShowFooter="true"
                        ShowStatusBar="true" ExportSettings-ExportOnlyData="false"
                        Skin="Office2007" Width="100%">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings IgnorePaging="true" FileName="ReporteVentas">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <MasterTableView AllowMultiColumnSorting="False" Name="Ventas" Width="100%" NoMasterRecordsText="No existen registros en el rango de fechas seleccionado." CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportat a EXCEL"></CommandItemSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
        <br />
        <br />
    </fieldset>
    <br />
    <fieldset style="display: none;">
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="Label2" runat="server" Text="Venta por familia" Font-Bold="true" Visible="false" CssClass="item"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td style="height: 5px">&nbsp;</td>
            </tr>
            <tr>
                <td class="item">
                    <telerik:RadGrid ID="reporteGridFamilia" runat="server" AllowPaging="true" ShowHeader="true" Visible="false"
                        AutoGenerateColumns="True" GridLines="None" ShowFooter="true"
                        PageSize="50" ShowStatusBar="true" ExportSettings-ExportOnlyData="false"
                        Skin="Office2007" Width="100%">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings IgnorePaging="true" FileName="ReporteVentasFamilia">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <MasterTableView AllowMultiColumnSorting="False" Name="Ventas" Width="100%" NoMasterRecordsText="No existen registros en el rango de fechas seleccionado." CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportat a EXCEL"></CommandItemSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td class="item">
                    <telerik:RadHtmlChart runat="server" ID="RadVentasPorFamilia" Width="700px" Height="500px">
                        <PlotArea>
                            <Series>
                                <telerik:PieSeries DataFieldY="Total" NameField="Familia" ExplodeField="IsExploded">
                                    <LabelsAppearance DataFormatString="{0:C}">
                                    </LabelsAppearance>
                                    <TooltipsAppearance>
                                        <ClientTemplate>
                                                #=dataItem.Familia#
                                        </ClientTemplate>
                                    </TooltipsAppearance>
                                    <TooltipsAppearance Color="White"></TooltipsAppearance>
                                </telerik:PieSeries>
                            </Series>
                            <YAxis>
                            </YAxis>
                        </PlotArea>
                        <ChartTitle>
                        </ChartTitle>
                    </telerik:RadHtmlChart>
                </td>
            </tr>
        </table>
        <br />
        <br />
    </fieldset>
    <%--</telerik:RadAjaxPanel>--%>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
