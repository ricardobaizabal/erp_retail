﻿<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="ordenes_compra.aspx.vb" Inherits="LinkiumCFDI.ordenes_compra" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("ordersList") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/filtros_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Filtros" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" border="0">
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item">
                        <table width="100%" border="0">
                            <tr valign="top">
                                <td>Proveedor:</td>
                                <td style="width: 27%">
                                    <asp:DropDownList ID="proveedorid" runat="server" Width="95%" CssClass="box"></asp:DropDownList>&nbsp;
                                </td>
                                <td>Desde:</td>
                                <td style="width: 10%">
                                    <telerik:RadDatePicker ID="fechaini" runat="server" Skin="Office2007" Width="110px">
                                        <Calendar ID="Calendar1" runat="server" Skin="Office2007" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>Hasta:</td>
                                <td>
                                    <telerik:RadDatePicker ID="fechafin" runat="server" Skin="Office2007" Width="110px">
                                        <Calendar ID="Calendar2" runat="server" Skin="Office2007" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>No. Orden:</td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtNoOrden" Width="60px" NumberFormat-DecimalDigits="0" MinValue="0" runat="server">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnGenerate" runat="server" Text="Consultar" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" CssClass="item" ControlToValidate="fechaini" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" CssClass="item" ControlToValidate="fechafin" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                                <td>&nbsp;</td>
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
                <asp:Label ID="lblOrdenesCompra" Text="Ordenes de Compra" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                        <telerik:RadGrid ID="ordersList" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" ShowStatusBar="True"
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" NoMasterRecordsText="No se encontraron registros para mostrar" Name="Orders" Width="100%">
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="No. Orden" UniqueName="id">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="proveedor" HeaderText="Proveedor" UniqueName="proveedor">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="productos" HeaderText="Cat. Prod." ItemStyle-HorizontalAlign="Right" UniqueName="productos">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="costo_estandar" DataFormatString="{0:C}" HeaderText="Costo Estandar" ItemStyle-HorizontalAlign="Right" UniqueName="costo_estandar">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" UniqueName="estatus">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRecibir" runat="server" CommandArgument='<%# eval("id") %>' CommandName="cmdReceive" Text="recibir"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Send" Visible="true">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDonwload" runat="server" ImageUrl="~/portalcfd/images/pdf.png" ToolTip="Descargar orden de compra" CommandArgument='<%# Eval("id") %>' CommandName="cmdDonwload" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAddOrder" Text="Agregar orden" runat="server" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMensajeEnvioOrden" runat="server" CssClass="item"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
