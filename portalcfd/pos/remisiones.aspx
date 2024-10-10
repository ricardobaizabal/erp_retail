<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="remisiones.aspx.vb" Inherits="LinkiumCFDI.remisiones" %>

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
                            <td style="width: 10%">Cliente:</td>
                            <td colspan="3">
                                <asp:DropDownList ID="cmbCliente" runat="server" Width="95%"></asp:DropDownList>&nbsp;
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 10%">Sucursal:</td>
                            <td colspan="3">
                                <asp:DropDownList ID="cmbSucursal" runat="server" Width="95%"></asp:DropDownList>&nbsp;
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
                        <tr valign="top">
                            <td style="width: 5%">Ticket:</td>
                            <td style="width: 10%">
                                <asp:TextBox ID="txtTicket" runat="server"></asp:TextBox>
                            </td>
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
            <asp:Image ID="imgPanel1" runat="server" ImageUrl="~/images/icons/reportes_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblReportsLegend" Text="Ventas" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
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
                        <ExportSettings IgnorePaging="True" FileName="Reporte_Ventas">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <MasterTableView AllowMultiColumnSorting="False" Name="Ingresos" DataKeyNames="id" Width="100%" NoMasterRecordsText="No existen registros en ese rango de fechas." CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="exportar a pdf" ExportToExcelText="exportat a excel"></CommandItemSettings>
                            <Columns>

                                <telerik:GridBoundColumn DataField="estatusid" HeaderText="estatusid" UniqueName="estatusid" Visible="false">
                                </telerik:GridBoundColumn>

                                <%--<telerik:GridTemplateColumn UniqueName="TemplateColumn1">
                                    <HeaderTemplate>Ticket</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lnkVer" runat="server" Text='<%# Eval("folio") %>'></asp:Label>
                                        <telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lnkVer" RelativeTo="Element" Position="BottomCenter" RenderInPageRoot="true" Text='<%#Eval("detalle")%>' ManualClose="true"></telerik:RadToolTip>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>

                                <telerik:GridBoundColumn DataField="folio" HeaderText="Folio" UniqueName="folio">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="hora" HeaderText="Hora" UniqueName="hora">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="cajero" HeaderText="Cajero" UniqueName="cajero">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente" UniqueName="cliente">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="formapago" HeaderText="Forma de pago" UniqueName="formapago">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="total" HeaderText="Total" UniqueName="total" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="monto" HeaderText="Monto pagado" UniqueName="monto" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus" ItemStyle-HorizontalAlign="Center" Visible="false" AllowFiltering="false">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="estatusremision" HeaderText="Estatus" UniqueName="estatusremision" ItemStyle-HorizontalAlign="Center" Visible="false" AllowFiltering="false">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="factura" HeaderText="Factura" UniqueName="factura" ItemStyle-HorizontalAlign="Center" AllowFiltering="false" Visible="true">
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="facturar" Visible="true">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkfacturar" runat="server" Text="Facturar" CommandArgument='<%# Eval("id") %>' CommandName="cmdFacturar"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkXML" runat="server" Text="xml" CommandArgument='<%# Eval("cfdid") %>' CommandName="cmdXML"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPDF" runat="server" Text="pdf" CommandArgument='<%# Eval("cfdid") %>' CommandName="cmdPDF"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Cancelar" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCancelar" runat="server" Text="Cancelar" CommandArgument='<%# Eval("id") & ";" & Eval("cfdid") %>' CommandName="cmdCancel"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn DataField="timbrado" UniqueName="timbrado" Visible="false">
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
