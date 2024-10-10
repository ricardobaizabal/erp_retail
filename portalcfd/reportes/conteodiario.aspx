<%@ Page Title="" Language="vb" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="conteodiario.aspx.vb" Inherits="LinkiumCFDI.conteodiario" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("reporteGrid") > -1) || (arguments.get_eventTarget().indexOf("detalleGrid") > -1)) {
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
                    </td>
                </tr>
                <tr>
                    <td class="item">
                        <table style="width: 100%;" border="0" cellpadding="5">
                            <tr>
                                <td style="width: 5%;">
                                    <asp:Label ID="lblDesde" Text="Desde:" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                </td>
                                <td style="width: 20%;">
                                    <telerik:RadDatePicker ID="calFechaDesde" Skin="Office2007" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width: 5%;">
                                    <asp:Label ID="lblHasta" Text="Hasta:" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                </td>
                                <td style="width: 20%;">
                                    <telerik:RadDatePicker ID="calFechaHasta" Skin="Office2007" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width: 5%;">
                                    <asp:Label ID="lblSucursal" Text="Sucursal:" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:DropDownList ID="cmbSucursal" runat="server" Width="90%"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" Width="90px" CausesValidation="false" />
                                </td>
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
                <tr>
                    <td class="item">
                        <telerik:RadGrid ID="reporteGrid" runat="server" AllowPaging="true" ShowHeader="true"
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true"
                            PageSize="50" ShowStatusBar="true" ExportSettings-ExportOnlyData="false"
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <ExportSettings IgnorePaging="true" FileName="ConteoDiario">
                                <Excel Format="Biff" />
                            </ExportSettings>
                            <MasterTableView AllowMultiColumnSorting="False" Name="ConteoDiario" Width="100%" NoMasterRecordsText="No existen registros en el rango de fechas seleccionado." CommandItemDisplay="Top">
                                <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportat a EXCEL"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="usuario" HeaderText="Usuario" UniqueName="usuario">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="inventario_real" HeaderText="Inventario" UniqueName="inventario_real" DataFormatString="{0:c}" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDetalle" runat="server" Text="Ver Detalle" CommandArgument='<%# Eval("id") & "," & Eval("sucursalid") %>' CommandName="cmdDetalle" CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item">
                        <asp:Panel ID="panelDetalleConteo" runat="server" Visible="false">
                            <table width="100%" border="0">
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadGrid ID="detalleGrid" runat="server" AllowPaging="true" ShowHeader="true"
                                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true"
                                            PageSize="50" ShowStatusBar="true" ExportSettings-ExportOnlyData="false"
                                            Skin="Office2007" Width="100%">
                                            <PagerStyle Mode="NumericPages" />
                                            <ExportSettings IgnorePaging="true" FileName="DetalleConteoDiario">
                                                <Excel Format="Biff" />
                                            </ExportSettings>
                                            <MasterTableView AllowMultiColumnSorting="False" Name="DetalleConteoDiario" Width="100%" DataKeyNames="id" NoMasterRecordsText="No existen registros en el rango de fechas seleccionado." CommandItemDisplay="Top">
                                                <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportat a EXCEL"></CommandItemSettings>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Codigo" UniqueName="codigo">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Producto" UniqueName="descripcion">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="familia" HeaderText="Familia" UniqueName="familia">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="subfamilia" HeaderText="Subfamilia" UniqueName="subfamilia">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad" ItemStyle-HorizontalAlign="Right">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia" ItemStyle-HorizontalAlign="Right">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="faltante" HeaderText="Faltante" UniqueName="faltante" ItemStyle-HorizontalAlign="Right">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="sobrante" HeaderText="Sobrante" UniqueName="sobrante" ItemStyle-HorizontalAlign="Right">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="costo" HeaderText="Costo" UniqueName="costo" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="importe" HeaderText="Importe" UniqueName="importe" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px">&nbsp;</td>
                                </tr>
                                <tr style="vertical-align: top">
                                    <td style="width: 50%;">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td colspan="2" style="text-align: center;">
                                                    <asp:Label ID="lblResumenInventario" runat="server" Text="RESUMEN DE INVENTARIO" Font-Bold="true" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 75%; text-align: right;">
                                                    <asp:Label ID="lblCantidadFaltante" Text="Cantidad Faltante:" runat="server" CssClass="item"></asp:Label>
                                                </td>
                                                <td style="width: 25%; text-align: right;">
                                                    <asp:Label ID="lblCantidadFaltanteValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 75%; text-align: right;">
                                                    <asp:Label ID="lblImporteFaltante" Text="Importe Faltante:" runat="server" CssClass="item"></asp:Label>
                                                </td>
                                                <td style="width: 25%; text-align: right;">
                                                    <asp:Label ID="lblImporteFaltanteValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 75%; text-align: right;">
                                                    <asp:Label ID="lblCantidadSobrante" Text="Cantidad Sobrante:" runat="server" CssClass="item"></asp:Label>
                                                </td>
                                                <td style="width: 25%; text-align: right;">
                                                    <asp:Label ID="lblCantidadSobranteValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 75%; text-align: right;">
                                                    <asp:Label ID="lblImporteSobrante" Text="Cantidad Sobrante:" runat="server" CssClass="item"></asp:Label>
                                                </td>
                                                <td style="width: 25%; text-align: right;">
                                                    <asp:Label ID="lblImporteSobranteValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 75%; text-align: right;">
                                                    <asp:Label ID="lblDiferencia" Text="Diferencia:" runat="server" CssClass="item"></asp:Label>
                                                </td>
                                                <td style="width: 25%; text-align: right;">
                                                    <asp:Label ID="lblDiferenciaValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%;">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td colspan="2" style="text-align: center;">
                                                    <asp:Label ID="lblResumenCostos" runat="server" Text="RESUMEN DE COSTOS" Font-Bold="true" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 75%; text-align: right;">
                                                    <asp:Label ID="lblUltimoInventario" Text="Último inventario afectado:" runat="server" CssClass="item"></asp:Label>
                                                </td>
                                                <td style="width: 25%; text-align: right;">
                                                    <asp:Label ID="lblUltimoInventarioValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 75%; text-align: right;">
                                                    <asp:Label ID="lblVentas" Text="Ventas:" runat="server" CssClass="item"></asp:Label>
                                                </td>
                                                <td style="width: 25%; text-align: right;">
                                                    <asp:Label ID="lblVentasValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 75%; text-align: right;">
                                                    <asp:Label ID="lblCompras" Text="Compras:" runat="server" CssClass="item"></asp:Label>
                                                </td>
                                                <td style="width: 25%; text-align: right;">
                                                    <asp:Label ID="lblComprasValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 75%; text-align: right;">
                                                    <asp:Label ID="lblInventario" Text="Inventario final teórico:" runat="server" CssClass="item"></asp:Label>
                                                </td>
                                                <td style="width: 25%; text-align: right;">
                                                    <asp:Label ID="lblInventarioValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 75%; text-align: right;">
                                                    <asp:Label ID="lblInventarioFinalReal" Text="Inventario final real:" runat="server" CssClass="item"></asp:Label>
                                                </td>
                                                <td style="width: 25%; text-align: right;">
                                                    <asp:Label ID="lblInventarioFinalRealValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </fieldset>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
