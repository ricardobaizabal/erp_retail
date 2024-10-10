<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="ventas_familia.aspx.vb" Inherits="LinkiumCFDI.ventas_familia" %>

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
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblReportsLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" border="0">
                <tr>
                    <td style="height: 5px">&nbsp;</td>
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
                                <td style="width: 5%; font-weight: bold;">Familia: </td>
                                <td style="width: 10%;">
                                    <asp:DropDownList ID="cmbFamilia" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td style="width: 5%; font-weight: bold;">SubFamilia: </td>
                                <td style="width: 10%;">
                                    <asp:DropDownList ID="cmbSubFamilia" Enabled="false" runat="server"></asp:DropDownList>
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
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 5%; font-weight: bold;">Producto: </td>
                                <td style="width: 10%;">
                                    <asp:TextBox ID="txtProducto" runat="server" CssClass="box"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td style="width: 10%;" align="left">
                                    <asp:Button ID="btnGenerate" runat="server" Text="Consultar" />
                                </td>
                                <td>&nbsp;</td>
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
                <asp:Label ID="Label2" runat="server" Text="Venta por presentación" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item">
                        <telerik:RadGrid ID="reporteGridFamilia" runat="server" AllowPaging="true" ShowHeader="true"
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true"
                            PageSize="50" ShowStatusBar="true" ExportSettings-ExportOnlyData="false"
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <ExportSettings IgnorePaging="true" FileName="ReporteVentasFamilia">
                                <Excel Format="Biff" />
                            </ExportSettings>
                            <MasterTableView AllowMultiColumnSorting="False" Name="Ventas" Width="100%" NoMasterRecordsText="No existen registros en el rango de fechas seleccionado." CommandItemDisplay="Top">
                                <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportat a EXCEL"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Cantidad" HeaderText="Cantidad" UniqueName="Cantidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Producto" HeaderText="Producto" UniqueName="Producto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Familia" HeaderText="Familia" UniqueName="Familia">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SubFamilia" HeaderText="SubFamilia" UniqueName="SubFamilia">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Total" HeaderText="Total" UniqueName="Total" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </fieldset>
        <br />
        <br />
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
