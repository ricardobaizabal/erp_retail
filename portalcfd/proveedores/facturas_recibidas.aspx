<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="facturas_recibidas.aspx.vb" Inherits="LinkiumCFDI.facturas_recibidas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("facturaslist") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="Label1" runat="server" Text="Filtros" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" border="0" cellpadding="3" cellspacing="0">
                <tr style="height: 10px;">
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="font-weight: bold; width: 8%;">Sucursal:</td>
                    <td colspan="3">
                        <asp:DropDownList ID="sucursalid" runat="server" Width="90%" CssClass="box"></asp:DropDownList>
                    </td>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="font-weight: bold; width: 8%;">Proveedor:</td>
                    <td colspan="3">
                        <asp:DropDownList ID="proveedorid" runat="server" Width="90%" CssClass="box"></asp:DropDownList>
                    </td>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="font-weight: bold; width: 5%;">Desde:</td>
                    <td class="item" style="font-weight: bold; width: 15%;">
                        <telerik:RadDatePicker ID="calFechaInicio" Skin="Office2007" Width="100%" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                    </td>
                    <td class="item" style="font-weight: bold; width: 5%;">Hasta:</td>
                    <td class="item" style="font-weight: bold; width: 15%;">
                        <telerik:RadDatePicker ID="calFechaFin" Skin="Office2007" Width="100%" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                    </td>
                    <td style="text-align: left;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
                    </td>
                </tr>
                <tr>
                    <td colspan="7" style="height: 30px;">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="Label2" runat="server" Text="Listado de Facturas" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" cellpadding="3" cellspacing="3" border="0">
                <tr style="height: 10px;">
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="facturaslist" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None"
                            Skin="Office2007" AllowFilteringByColumn="false">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" NoMasterRecordsText="No se encontraron registros para mostrar" Name="Facturas" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fecha_emision" HeaderText="Emisión" UniqueName="fecha_emision" DataFormatString="{0:d}" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="emisor" HeaderText="Proveedor" UniqueName="emisor" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="lugar_expedicion" HeaderText="Lugar Expedición" UniqueName="lugar_expedicion" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="serie" HeaderText="Serie" UniqueName="serie" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="folio" HeaderText="Folio" UniqueName="folio" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="uuid" HeaderText="UUID" UniqueName="uuid" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="total" HeaderText="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" UniqueName="total" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="moneda" HeaderText="Moneda" UniqueName="moneda" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="XML">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkXML" runat="server" Text="xml" CausesValidation="false" CommandArgument='<%# Eval("id") %>' CommandName="cmdXML"></asp:LinkButton>
                                            <asp:Label ID="lblXML" runat="server" Text='<%# Eval("xml") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn AllowFiltering="false" Visible="false" HeaderText="" UniqueName="PDF">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPDF" runat="server" Text="pdf" CausesValidation="false" CommandArgument='<%# Eval("id") %>' CommandName="cmdPDF"></asp:LinkButton>
                                            <asp:Label ID="lblPDF" runat="server" Text='<%# Eval("pdf") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr style="height: 10px;">
                    <td>&nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </telerik:RadAjaxPanel>
</asp:Content>
