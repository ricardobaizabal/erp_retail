<%@ Page Language="vb" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="transferencias.aspx.vb" Inherits="LinkiumCFDI.transferencias" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListadoProductos_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProductsListLegend" runat="server" Font-Bold="true" CssClass="item" Text="Lotes de Transferencia"></asp:Label>
        </legend>
        <br />
        <table width="100%">
            <tr>
                <td style="height: 5px">[
                    <asp:HyperLink CssClass="item" ID="lnkAddTransfer" runat="server" Text="Crear nuevo lote de transferencia" NavigateUrl="~/portalcfd/almacen/agregarlote.aspx"></asp:HyperLink>
                    ]
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="panelBusqueda" runat="server" DefaultButton="btnConsultar">
                        <table width="100%">
                            <tr>
                                <td class="item" style="font-weight:bold;">Desde:
                                </td>
                                <td class="item">
                                    <telerik:RadDatePicker ID="fha_ini" Skin="Office2007" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                                <td class="item" style="font-weight:bold;">Hasta:
                                </td>
                                <td class="item">
                                    <telerik:RadDatePicker ID="fha_fin" Skin="Office2007" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="item" style="font-weight:bold;">Origen
                                </td>
                                <td class="item">
                                    <asp:DropDownList ID="origenid" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td class="item" style="font-weight:bold;">Destino
                                </td>
                                <td class="item">
                                    <asp:DropDownList ID="destinoid" runat="server" AutoPostBack="false"></asp:DropDownList>
                                </td>
                                <td class="item" style="font-weight:bold;">Estatus
                                </td>
                                <td>
                                    <asp:DropDownList ID="estatusid" runat="server" AutoPostBack="false"></asp:DropDownList>
                                </td>
                                <td class="item">
                                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <telerik:RadGrid ID="loteslist" runat="server" Width="100%" ShowStatusBar="True" ShowFooter="true"
                        AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None"
                        Skin="Office2007">
                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                        <MasterTableView Width="100%" NoMasterRecordsText="No se encontraron registros." DataKeyNames="id" Name="Lotes" AllowMultiColumnSorting="False">
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Lote No." UniqueName="id">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text='<%# Eval("id") %>' CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="costo" HeaderText="Costo Total" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" UniqueName="costo" DataFormatString="{0:c}" Aggregate="Sum" FooterStyle-Font-Bold="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="precio" HeaderText="Precio Total" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" UniqueName="precio" DataFormatString="{0:c}" Aggregate="Sum" FooterStyle-Font-Bold="true">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="recibido" HeaderText="Recibido" UniqueName="recibido">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="origen" HeaderText="Origen" UniqueName="origen">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="destino" HeaderText="Destino" UniqueName="destino">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="comentario" HeaderText="Comentario" UniqueName="comentario">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="" UniqueName="id">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAutorizar" runat="server" Text='Autorizar' CommandArgument='<%# Eval("id") %>' CommandName="cmdAutorizar" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="" UniqueName="id">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCancelar" runat="server" Text='Cancelar' CommandArgument='<%# Eval("id") %>' CommandName="cmdCancelar" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
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
                <td style="height: 5px">&nbsp;</td>
            </tr>
            <tr>
                <td style="height: 5px">&nbsp;</td>
            </tr>
        </table>
    </fieldset>
    <br />
</asp:Content>
