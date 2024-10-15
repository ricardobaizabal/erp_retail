<%@ Page Language="vb" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="ventas_hielo.aspx.vb" Inherits="LinkiumCFDI.ventas_hielo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("grdVentas") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" RestoreOriginalRenderDelegate="true" ClientEvents-OnRequestStart="OnRequestStart">--%>
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/filtros_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Filtros" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%" border="0">
            <tr>
                <td style="height: 5px"></td>
            </tr>
            <tr>
                <td class="item">
                    <%--Seleccione el rango de fechas que desee consultar:<br /><br />--%>
                    <table width="100%" border="0" cellpadding="5">

                        <tr>
                            <td style="width: 10%">Sucursal:</td>
                            <td colspan="3">
                                <telerik:RadComboBox RenderMode="Lightweight" CssClass="item" ID="cmbSucursal" runat="server" CheckBoxes="true" Width="95%" Localization-CheckAllString="Seleccionar todo" Localization-ItemsCheckedString="proveedores seleccionados" Localization-AllItemsCheckedString="Todos seleccionados" EnableCheckAllItemsCheckBox="true" EmptyMessage="--Seleccione--">
                                </telerik:RadComboBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 10%">Desde:</td>
                            <td style="width: 10%">
                                <telerik:RadDatePicker ID="fechaini" runat="server" Skin="Office2007">
                                    <Calendar ID="Calendar1" runat="server" Skin="Office2007" UseColumnHeadersAsSelectors="False"
                                        UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>
                            <td style="width: 10%">Hasta:</td>
                            <td style="width: 10%">
                                <telerik:RadDatePicker ID="fechafin" runat="server" Skin="Office2007">
                                    <Calendar ID="Calendar2" runat="server" Skin="Office2007" UseColumnHeadersAsSelectors="False"
                                        UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        
                            <td>
                                <asp:Button ID="btnGenerate" runat="server" Text="Consultar" />
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
            <asp:Image ID="imgPanel1" runat="server" ImageUrl="~/images/icons/reportes_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblReportsLegend" Text="Ventas de Hielo" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td style="height: 5px"></td>
            </tr>
            <tr>
                <td class="item">
                    <telerik:RadGrid ID="grdVentas" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" GridLines="None" ShowFooter="true"
                        PageSize="50" ShowStatusBar="True" ExportSettings-ExportOnlyData="false"
                        Skin="Office2007" Width="100%">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings IgnorePaging="True" FileName="ReporteVentasDetalle">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <MasterTableView AllowMultiColumnSorting="False" Name="VentasDetalle" DataKeyNames="id" Width="100%" NoMasterRecordsText="No existen registros en ese rango de fechas." CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="exportar a pdf" ExportToExcelText="exportat a excel"></CommandItemSettings>
                            <Columns>

                                <telerik:GridBoundColumn DataField="remisionid" HeaderText="No. Ticket" UniqueName="remisionid">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="hora" HeaderText="Hora" UniqueName="hora">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad" FooterStyle-Font-Bold="true" FooterText=" " ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="descripcion" HeaderText="Producto" UniqueName="descripcion">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="unitario" HeaderText="Unitario" UniqueName="unitario" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="importe" HeaderText="Importe" UniqueName="importe" DataFormatString="{0:c}" FooterStyle-Font-Bold="true" FooterText=" " ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="cajero" HeaderText="Cajero" UniqueName="cajero">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente" UniqueName="cliente">
                                </telerik:GridBoundColumn>

                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </fieldset>
    <%--</telerik:RadAjaxPanel>--%>
    <telerik:RadWindowManager ID="RadAlert" runat="server" Skin="Office2007" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
