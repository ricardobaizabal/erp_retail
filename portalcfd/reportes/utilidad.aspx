<%@ Page Title="" Language="vb" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="utilidad.aspx.vb" Inherits="LinkiumCFDI.utilidad" %>

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
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblReportsLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item">
                        <table style="width: 100%;" border="0" cellpadding="5">
                            <tr>
                                <td style="width: 5%; font-weight: bold;">Sucursal: </td>
                                <td style="width: 20%;">
                                    <asp:DropDownList ID="cmbSucursal" Width="100%" runat="server"></asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 5%; font-weight: bold;">Año: </td>
                                <td style="width: 20%;">
                                    <asp:DropDownList ID="cmbAnio" runat="server" Width="100%">
                                        <asp:ListItem Value="0" Text="--Seleccione--" />
                                        <asp:ListItem Value="2021" Text="2021" />
                                        <asp:ListItem Value="2020" Text="2020" />
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 5%; font-weight: bold;">Mes: </td>
                                <td style="width: 20%;">
                                    <asp:DropDownList ID="cmbMes" runat="server" Width="100%">
                                        <asp:ListItem Value="0" Text="--Seleccione--" />
                                        <asp:ListItem Value="1" Text="Enero" />
                                        <asp:ListItem Value="2" Text="Febrero" />
                                        <asp:ListItem Value="3" Text="Marzo" />
                                        <asp:ListItem Value="4" Text="Abril" />
                                        <asp:ListItem Value="5" Text="Mayo" />
                                        <asp:ListItem Value="6" Text="Junio" />
                                        <asp:ListItem Value="7" Text="Julio" />
                                        <asp:ListItem Value="8" Text="Agosto" />
                                        <asp:ListItem Value="9" Text="Septiembre" />
                                        <asp:ListItem Value="10" Text="Octubre" />
                                        <asp:ListItem Value="11" Text="Noviembre" />
                                        <asp:ListItem Value="12" Text="Diciembre" />
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Button ID="btnGenerate" runat="server" Text="Generar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%">
                <tr>
                    <td class="item">
                        <telerik:RadGrid ID="reporteGrid" runat="server" AllowPaging="True" ShowHeader="true"
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true"
                            PageSize="100" ShowStatusBar="True" ExportSettings-ExportOnlyData="false"
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <ExportSettings IgnorePaging="True" FileName="ReporteUtilidad">
                                <Excel Format="Biff" />
                            </ExportSettings>
                            <MasterTableView AllowMultiColumnSorting="False" Name="Utulidad" Width="100%" NoMasterRecordsText="No existen registros en ese rango de fechas." CommandItemDisplay="Top">
                                <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="exportar a pdf" ExportToExcelText="exportat a excel"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="venta" HeaderText="Venta" UniqueName="venta" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="costo" HeaderText="Costo" UniqueName="costo" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="utilidad" HeaderText="Utilidad" UniqueName="utilidad" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="porcentaje_utilidad" HeaderText="% Utilidad" UniqueName="porcentaje_utilidad" DataFormatString="{0:p}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
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
        <br />
    </telerik:RadAjaxPanel>
    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblReportsLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td class="item">
                        Seleccione el rango de fechas que desee consultar:<br /><br />
                        <table style="width:100%;" border="0" cellpadding="5">
                            <tr>
                                <td style="width:5%;">Desde: </td>
                                <td style="width:10%;">
                                    <telerik:RadDatePicker ID="fechaini" Runat="server" Skin="Web20">
                                        <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False" 
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width:5%;">hasta: </td>
                                <td style="width:10%;">
                                    <telerik:RadDatePicker ID="fechafin" Runat="server" Skin="Web20">
                                        <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False" 
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width:5%;">Tipo: </td>
                                <td style="width:10%;">
                                    <asp:DropDownList ID="tipoid" runat="server" CssClass="box" AutoPostBack="false">
                                        <asp:ListItem Text="--Seleccione--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Factura" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Remisión" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:5%;">Cliente: </td>
                                <td colspan="4">
                                    <asp:DropDownList ID="clienteid" runat="server" Width="100%" CssClass="box"></asp:DropDownList>
                                </td>
                                <td style="width:10%;" align="right">
                                    <asp:Button ID="btnGenerate" runat="server" Text="Generar" CssClass="boton" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">
                    </td>
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
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px" colspan="5">
                    </td>
                </tr>
                <tr>
                    <td class="item" colspan="5">
                        <telerik:RadGrid ID="reporteGrid" runat="server" AllowPaging="True" ShowHeader="true" 
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true" 
                            PageSize="50" ShowStatusBar="True"  ExportSettings-ExportOnlyData="false" 
                            Skin="Simple" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <ExportSettings IgnorePaging="True" FileName="ReporteUtilidad">
                                <Excel Format="Biff" />
                            </ExportSettings>
                            <MasterTableView AllowMultiColumnSorting="False" Name="Utulidad" Width="100%" NoMasterRecordsText="No existen registros en ese rango de fechas." CommandItemDisplay="Top">
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="exportar a pdf" ExportToExcelText="exportat a excel"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="folio" HeaderText="No. Factura / Remisión" HeaderStyle-Width="150px" UniqueName="folio">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha" DataFormatString="{0:d}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente" UniqueName="cliente">
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn DataField="total" HeaderText="Total" UniqueName="total" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="costo" HeaderText="Costo" UniqueName="costo" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="utilidad" HeaderText="Utilidad" UniqueName="utilidad" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="porcentaje_utilidad" HeaderText="% Utilidad" UniqueName="porcentaje_utilidad" DataFormatString="{0:p}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="factor_ponderado" HeaderText="factor_ponderado" UniqueName="factor_ponderado" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="utilidad_ponderada" HeaderText="utilidad_ponderada" UniqueName="utilidad_ponderada" DataFormatString="{0:p}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <table border="1" cellpadding="4" style="width:30%;">
                <tr>
                    <td colspan="2" class="item" align="center"><span style="font-family:Verdana;font-size:Small;font-weight:bold;">R E S U M E N</span></td>
                </tr>
                <tr>
                    <td class="item" style="width:40%;"><span style="font-family:Verdana;font-size:12px;font-weight:bold;">Venta</span></td>
                    <td align="right">
                        <asp:Label ID="lblTotalVenta" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="item" style="width:40%;"><span style="font-family:Verdana;font-size:12px;font-weight:bold;">Utilidad</span></td>
                    <td align="right">
                        <asp:Label ID="lblTotalUtilidad" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="item"><span style="font-family:Verdana;font-size:12px;font-weight:bold;">(-)Egresos</span></td>
                    <td align="right">
                        <asp:Label ID="lblTotalEgresos" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="item"><span style="font-family:Verdana;font-size:12px;font-weight:bold;">(+)Inventario</span></td>
                    <td align="right">
                        <asp:Label ID="lblTotalInventario" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="item"><span style="font-family:Verdana;font-size:12px;font-weight:bold;">TOTAL</span></td>
                    <td align="right">
                        <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
            </table>
            <br /><br /><br />    
        </fieldset>
        <br /><br />
    </telerik:RadAjaxPanel>--%>
</asp:Content>
