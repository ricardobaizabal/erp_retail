<%@ Page Title="" Language="vb" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="abastecimiento_utilidad.aspx.vb" Inherits="LinkiumCFDI.abastecimiento_utilidad" %>

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
                                <td style="width: 15%;">
                                    <asp:DropDownList ID="cmbSucursal" Width="100%" runat="server"></asp:DropDownList>
                                </td>
                                <td style="width: 5%; font-weight: bold;">Desde: </td>
                                <td style="width: 15%;">
                                    <telerik:RadDatePicker ID="fechaini" runat="server" Width="98%" Skin="Office2007">
                                        <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width: 5%; font-weight: bold;">Hasta: </td>
                                <td style="width: 15%;">
                                    <telerik:RadDatePicker ID="fechafin" runat="server" Width="98%" Skin="Office2007">
                                        <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width: 5%; font-weight: bold;">Código: </td>
                                <td style="width: 15%;">
                                    <asp:TextBox ID="txtCodigo" runat="server" Width="98%" CssClass="box"></asp:TextBox>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Button ID="btnGenerate" runat="server" CausesValidation="true" Text="Generar" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">&nbsp;</td>
                                <td colspan="2">
                                    <asp:RequiredFieldValidator ID="valCodigo" runat="server" ErrorMessage="Proporcione un código." ForeColor="Red" ControlToValidate="txtCodigo" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td class="item">
                        <telerik:RadGrid ID="reporteGrid" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None" ShowFooter="true"
                            Skin="Office2007">
                            <PagerStyle Mode="NumericPages" />
                            <ExportSettings IgnorePaging="true" FileName="AbastecimientoyUtilidad">
                                <Excel Format="Biff" />
                            </ExportSettings>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Products" NoMasterRecordsText="No se encontraron registros." AllowMultiColumnSorting="False" CommandItemDisplay="Top">
                                <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportat a EXCEL"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="tipo" HeaderText="Tipo" UniqueName="tipo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="costo_unitario" HeaderText="Costo unitario" UniqueName="costo_unitario" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="importe" HeaderText="Importe" UniqueName="importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td class="item" style="width: 90%; text-align: right">
                                    <asp:Label ID="lblCostoPromedio" runat="server" Font-Bold="true" Text="Costo promedio:" CssClass="item"></asp:Label>
                                </td>
                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblCostoPromedioValue" runat="server" CssClass="item"></asp:Label>
                                </td>
                            </tr>
                            <%--<tr>
                                <td style="width: 90%; text-align: right">
                                    <asp:Label ID="lblVentasPromedio" runat="server" Font-Bold="true" Text="Ventas promedio:" CssClass="item"></asp:Label>
                                </td>
                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblVentasPromedioValue" runat="server" CssClass="item"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 90%; text-align: right">
                                    <asp:Label ID="lblUtilidadPromedio" runat="server" Font-Bold="true" Text="Utilidad promedio:" CssClass="item"></asp:Label>
                                </td>
                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblUtilidadPromedioValue" runat="server" CssClass="item"></asp:Label>
                                </td>
                            </tr>--%>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </telerik:RadAjaxPanel>
</asp:Content>
