<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="editapedido.aspx.vb" Inherits="LinkiumCFDI.editapedido" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <fieldset style="border-color: #cccccc; width: 98%; border-width: 1px; border-style: solid; padding: 10px;">
        <legend title="Pedidos." class="item"><strong>Agregar / Editar Pedido</strong></legend>
        <table width="100%" border="0">
            <tr>
                <td align="left" style="vertical-align: top;">
                    <table id="tblProductos" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <table width="100%" border="0" cellpadding="5">
                                    <tr>
                                        <td class="item">
                                            <strong>Cliente: </strong>
                                            <asp:Label ID="lblRazonsocial" runat="server" class="item" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="item">
                                            <strong>Sucursal: </strong>
                                            <asp:Label ID="lblSucursal" runat="server" class="item" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="item">
                                            <strong>Orden de compra: </strong>
                                            <asp:Label ID="lblOrdenCompra" runat="server" class="item" Text=""></asp:Label>
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
                                <asp:Panel ID="panelBusqueda" DefaultButton="btnSearch" runat="server">
                                    <table runat="server">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblBuscar" runat="server" class="item" Text="Buscar productos:"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtSearch" runat="server" Width="200px" ValidationGroup="ValSearch"></telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSearch" runat="server" Text="Buscar" ValidationGroup="ValSearch" />&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnCancelarBusqueda" runat="server" Text="Cancelar Búsqueda" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="valSearch" runat="server" ControlToValidate="txtSearch" ErrorMessage="Debe ingresar un texto sobre productos a buscar." ValidationGroup="ValSearch" class="item" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="panel1" Visible="false" runat="server">
                                    <asp:Label ID="lblProdsTitulo" runat="server" Font-Bold="true" Font-Size="Small" class="item" Text="Lista de Productos"></asp:Label><br />
                                    <br />
                                    <telerik:RadGrid Width="100%" ID="productosList" runat="server" AllowPaging="True"
                                        PageSize="50" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0"
                                        GridLines="None" Skin="Office2007" HeaderStyle-Font-Size="Small" ShowHeader="true">
                                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"></ClientSettings>
                                        <MasterTableView DataKeyNames="id" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="No hay registros para mostrar.">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="codigo" ItemStyle-Width="10%" FilterControlAltText="Filter column column" HeaderText="Código" UniqueName="codigo" HeaderStyle-Font-Size="Small">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="disponibles" ItemStyle-Width="5%" HeaderText="Disponibles" UniqueName="disponibles">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Cantidad" UniqueName="ColCantidad" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtCantidad" Width="60px" Type="Number" NumberFormat-DecimalDigits="2" runat="server"></telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="descripcion" ItemStyle-Width="50%" FilterControlAltText="Filter column2 column" HeaderText="Descripción" UniqueName="descripcion">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings>
                                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="item">
                                <asp:Label ID="lblMensaje" runat="server" class="item" ForeColor="Red"></asp:Label>
                                <asp:HiddenField runat="server" ID="ClienteId" Value="0" />
                                <asp:HiddenField runat="server" ID="SucursalId" Value="0" />
                                <asp:HiddenField runat="server" ID="EstatusId" Value="0" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <div align="right">
                        <asp:Button ID="btnAgregaConceptos" runat="server" CssClass="item" Visible="False" Text="Agregar Conceptos" />&nbsp;&nbsp;
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left" style="vertical-align: top;">
                    <br />
                    <asp:Label ID="lblPedidotitulo" runat="server" Font-Bold="true" Font-Size="Small" class="item" Text="Detalle del pedido"></asp:Label>
                    <br />
                    <br />
                    <telerik:RadGrid Width="99.8%" ID="pedidodetallelist" runat="server" AllowPaging="True"
                        PageSize="50" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0"
                        Skin="Office2007" HeaderStyle-Font-Size="Small" ShowHeader="true" ShowFooter="true">
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                        </ClientSettings>
                        <MasterTableView DataKeyNames="id" ShowHeadersWhenNoRecords="true" NoMasterRecordsText="No hay registros para mostrar.">
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="codigo" FilterControlAltText="Filter column column" HeaderText="Código" UniqueName="codigo" HeaderStyle-Font-Size="Small">
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="unidad" ItemStyle-HorizontalAlign="Right" FilterControlAltText="Filter column2 column" HeaderText="Unidad" UniqueName="unidad">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="descripcion" FilterControlAltText="Filter column2 column" HeaderText="Descripción" UniqueName="descripcion">
                                </telerik:GridBoundColumn>                                

                                <telerik:GridNumericColumn DataField="precio" ItemStyle-HorizontalAlign="Right" FilterControlAltText="Filter column column" HeaderText="Precio" UniqueName="precio" DataType="System.Decimal" DataFormatString="{0:$###,##0.00}" NumericType="Currency">
                                </telerik:GridNumericColumn>                                

                                <telerik:GridNumericColumn DataField="importe" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FilterControlAltText="Filter column column" HeaderText="Importe" UniqueName="importe">
                                </telerik:GridNumericColumn>

                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="ColEliminarProducto" HeaderText="Remover">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEliminar" ImageUrl="~/images/action_delete.gif" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td align="center" style="vertical-align: middle; height: 30px;">
                    <asp:Label ID="lblPedidoError" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 70px;" class="item">
                    <asp:Button ID="btnColocarPedido" runat="server" Text="Colocar" />&nbsp;&nbsp;&nbsp; 
                    <asp:Button ID="btnAutorizar" runat="server" Text="Autorizar" />&nbsp;&nbsp;&nbsp; 
                    <%--<asp:Button ID="btnRechazar" runat="server" Text="Rechazar" />&nbsp;&nbsp;&nbsp; --%>
                    <%--<asp:Button ID="btnReactivar" runat="server" Text="Reactivar" />&nbsp;&nbsp;&nbsp; --%>
                    <%--<asp:Button ID="btnPack" runat="server" Text="Empaquetado" />&nbsp;&nbsp;&nbsp; --%>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar Pedido" CausesValidation="false" />&nbsp;&nbsp;&nbsp; 
                    <%--<asp:Button ID="btnSent" runat="server" Text="Enviado" />&nbsp;&nbsp;&nbsp; --%>
                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CausesValidation="false" />&nbsp;&nbsp;&nbsp; 
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>