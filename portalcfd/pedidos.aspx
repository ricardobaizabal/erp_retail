<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="pedidos.aspx.vb" Inherits="LinkiumCFDI.pedidos1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <br />
        <span class="item">Cliente:
        <asp:DropDownList ID="cmbCliente" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;Estatus:
        <asp:DropDownList ID="cmbPedidoEstatus" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;Palabra clave:
        <asp:TextBox ID="txtSearch" runat="server" Width="120px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Buscar" />&nbsp;&nbsp;&nbsp;
        </span>
        <br />
        <br />
    </fieldset>
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblListaPedidos" Text="Pedidos" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td style="height: 5px">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="pedidosList" runat="server" AllowPaging="True" PageSize="50" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0" Skin="Office2007" Width="100%">
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                        </ClientSettings>
                        <MasterTableView DataKeyNames="id" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="No hay registros para mostrar.">
                            <Columns>
                                <telerik:GridBoundColumn DataField="id" HeaderText="No. Pedido" UniqueName="id" HeaderStyle-Font-Size="Small">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente" UniqueName="cliente" HeaderStyle-Font-Size="Small">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn DataField="ejecutivo" HeaderText="Ejecutivo" UniqueName="ejecutivo" HeaderStyle-Font-Size="Small">
                                    </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="fecha_alta" HeaderText="Fecha alta" UniqueName="fecha_alta">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="factura" HeaderText="Factura" UniqueName="factura">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="guia" HeaderText="No. Guía" UniqueName="guia">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="orden_compra" HeaderText="Orden Compra" UniqueName="orden_compra">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="ColEditar" AllowFiltering="true" HeaderText="Ver/Editar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEditar" ImageUrl="~/images/action_edit.png" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="ColDelete" AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEliminar" ImageUrl="~/images/action_delete.gif" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="ColFacturar" AllowFiltering="true" HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFacturar" runat="server" Text="Facturar" CommandArgument='<%# Eval("id") %>' CommandName="cmdFacturar"></asp:LinkButton>
                                    </ItemTemplate>
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
                <td align="right" style="height: 5px">
                    <asp:Button ID="btnAgregarPedido" runat="server" Text="Agregar Pedido" CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td style="height: 5px">&nbsp;</td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
