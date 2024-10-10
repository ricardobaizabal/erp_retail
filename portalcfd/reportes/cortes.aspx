<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="cortes.aspx.vb" Inherits="LinkiumCFDI.cortes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("cortesList") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" SkinID="Office2007" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">--%>
        <fieldset style="min-height: 400px;">
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblListTitle" runat="server" Font-Bold="true" CssClass="item" Text="Cortes de Caja"></asp:Label>
            </legend>
            <br />
            <table>
                <tr>
                    <td colspan="7" class="item">Seleccione el rango de fechas que desee consultar:<br /><br /></td>
                </tr>
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
                <tr>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <telerik:RadGrid ID="cortesList" runat="server" Width="100%" ShowStatusBar="True" ShowFooter="true"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None" Skin="Office2007">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Cortes" NoMasterRecordsText="No se encontraron registros." AllowMultiColumnSorting="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="No. Corte" UniqueName="id">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="usuario" HeaderText="Cajero" UniqueName="usuario">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="fechaini" HeaderText="Inicio" UniqueName="fechaini" DataFormatString="{0:dd/MM/yyyy H:mm:ss}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="fechafin" HeaderText="Fin" UniqueName="fechafin" DataFormatString="{0:dd/MM/yyyy H:mm:ss}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="faltante" HeaderText="Faltante" UniqueName="faltante" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="sobrante" HeaderText="Sobrante" UniqueName="sobrante" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn UniqueName="detalle">
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
            </table>
        </fieldset>
        <br />
        <asp:Panel ID="panelDetalleCorte" runat="server" Visible="false">
            <fieldset style="min-height: 500px;">
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblDetalleTitle" runat="server" Font-Bold="true" CssClass="item" Text="Detalle Corte de Caja"></asp:Label>
                </legend>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFolioCorte" runat="server" Font-Bold="true" Text="No. Corte:" CssClass="item"></asp:Label>&nbsp;
                        <asp:Label ID="lblFolioCorteValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSucursalCorte" runat="server" Font-Bold="true" Text="Sucursal:" CssClass="item"></asp:Label>&nbsp;
                        <asp:Label ID="lblSucursalCorteValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCajeroCorte" runat="server" Font-Bold="true" Text="Cajero:" CssClass="item"></asp:Label>&nbsp;
                        <asp:Label ID="lblCajeroCorteValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr>
                    <td>
                        <asp:Label ID="lblTicketsCorte" runat="server" Font-Bold="true" Text="Tickets en corte:" CssClass="item"></asp:Label>&nbsp;
                        <asp:Label ID="lblTicketsCorteValue" runat="server" CssClass="item"></asp:Label>
                    </td>
                </tr>--%>
                    <tr>
                        <td>
                            <asp:Label ID="lblFechasCorte" runat="server" Font-Bold="true" Text="Fechas corte:" CssClass="item"></asp:Label>&nbsp;
                        <asp:Label ID="lblFechasCorteValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSistema" runat="server" Font-Bold="true" Text="REPORTE DE EFECTIVO" CssClass="item"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 32%;" border="0">
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblEfectivoReportado" runat="server" Font-Bold="true" Text="Efectivo Reportado:" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblEfectivoReportadoValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblInicioCaja" runat="server" Font-Bold="true" Text="* Inicio de caja:" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblInicioCajaValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblTotalVentaEfectivo" runat="server" Font-Bold="true" Text="Total venta en efectivo:" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalVentaEfectivoValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblEntradas" runat="server" Font-Bold="true" Text="(+) Entradas" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblEntradasValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblVales" runat="server" Font-Bold="true" Text="(-) Vales" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblValesValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblRetiros" runat="server" Font-Bold="true" Text="(-) Retiros" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblRetirosValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblTotalEfectivo" runat="server" Font-Bold="true" Text="Total de efectivo:" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalEfectivoValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblFaltanteSobrante" runat="server" Font-Bold="true" Text="Diferencia:" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblFaltanteSobranteValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="tblDesgloceEfectivo" runat="server" Font-Bold="true" Text="DESGLOCE DE EFECTIVO" CssClass="item"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 10%;" border="0">
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblBilletes" runat="server" Font-Bold="true" Text="BILLETES" CssClass="item"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblBilletes1000" runat="server" Font-Bold="true" Text="$1,000" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblBilletes1000Value" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblBilletes500" runat="server" Font-Bold="true" Text="$500" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblBilletes500Value" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblBilletes200" runat="server" Font-Bold="true" Text="$200" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblBilletes200Value" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblBilletes100" runat="server" Font-Bold="true" Text="$100" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblBilletes100Value" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblBilletes50" runat="server" Font-Bold="true" Text="$50" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblBilletes50Value" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblBilletes20" runat="server" Font-Bold="true" Text="$20" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblBilletes20Value" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 10%;" border="0">
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="MONEDAS" CssClass="item"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblMonedas20" runat="server" Font-Bold="true" Text="$20" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblMonedas20Value" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblMonedas10" runat="server" Font-Bold="true" Text="$10" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblMonedas10Value" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblMonedas5" runat="server" Font-Bold="true" Text="$5" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblMonedas5Value" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblMonedas2" runat="server" Font-Bold="true" Text="$2" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblMonedas2Value" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblMonedas1" runat="server" Font-Bold="true" Text="$1" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblMonedas1Value" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblMonedas50c" runat="server" Font-Bold="true" Text="$0.50" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblMonedas50cValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblresumenVenta" runat="server" Font-Bold="true" Text="RESUMEN DE VENTA" CssClass="item"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 32%;" border="0">
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblEfectivo" runat="server" Font-Bold="true" Text="Efectivo:" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblEfectivoValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblTarjetas" runat="server" Font-Bold="true" Text="Tarjetas:" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTarjetasValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblCheques" runat="server" Font-Bold="true" Text="Cheques:" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblChequesValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblDepositos" runat="server" Font-Bold="true" Text="Depósitos:" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblDepositosValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblTotalVenta" runat="server" Font-Bold="true" Text="Total de venta:" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalVentaValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblTotalGastos" runat="server" Font-Bold="true" Text="Total de gastos:" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblTotalGastosValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60%;">
                                        <asp:Label ID="lblPromedioVenta" runat="server" Font-Bold="true" Text="Promedio de venta:" CssClass="item"></asp:Label>
                                    </td>
                                    <td style="width: 40%; text-align: right;">
                                        <asp:Label ID="lblPromedioVentaValue" runat="server" CssClass="item"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    <%--</telerik:RadAjaxPanel>--%>
</asp:Content>