<%@ Page Language="vb" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="editalote.aspx.vb" Inherits="LinkiumCFDI.editalote" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblTransferencia" runat="server" Font-Bold="true" CssClass="item" Text="Editando lote de transferencia"></asp:Label>
        </legend>
        <br />
        <table border="0" cellpadding="2" cellspacing="0" align="center" width="100%">
            <tr>
                <td class="item" style="line-height: 20px; width: 12%;">
                    <strong>Folio:</strong>
                </td>
                <td class="item" style="width: 200px">
                    <asp:Label ID="lblFolio" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="item" style="line-height: 20px; width: 12%;">
                    <strong>Fecha:</strong>
                </td>
                <td class="item" style="width: 200px">
                    <asp:Label ID="lblFecha" runat="server"></asp:Label>
                </td>
                <td class="item">&nbsp;</td>
            </tr>
            <tr>
                <td class="item" style="line-height: 20px; width: 12%;">
                    <strong>Origen:</strong>
                </td>
                <td class="item" style="width: 200px">
                    <asp:Label ID="lblOrigen" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="item" style="line-height: 20px; width: 12%;">
                    <strong>Destino:</strong>
                </td>
                <td class="item" style="width: 200px">
                    <asp:Label ID="lblDestino" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="item" style="line-height: 20px; width: 12%;">
                    <strong>Usuario:</strong>
                </td>
                <td class="item" style="width: 200px">
                    <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr valign="top">
                <td class="item" style="line-height: 20px; width: 12%;">
                    <strong>Comentario:</strong>
                </td>
                <td class="item">
                    <asp:Label ID="lblComentario" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblAgregaProducto" runat="server" Font-Bold="true" CssClass="item" Text="Agregar productos"></asp:Label>
        </legend>
        <br />
        <table border="0" width="100%">
            <tr>
                <td class="item">
                    <asp:Panel runat="server" ID="pSeacrh" DefaultButton="btnSearch">
                        Producto: &nbsp;<asp:TextBox ID="txtSearch" runat="server" CssClass="item"></asp:TextBox>&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar" CausesValidation="false" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancelar Búsqueda" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="panelSearch" runat="server" Visible="false">
                        <telerik:RadGrid ID="resultslist" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None"
                            Skin="Office2007">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView Width="100%" DataKeyNames="id,presentacion,existencia,disponibles,factor" NoMasterRecordsText="No se encontraron registros." Name="Products" AllowMultiColumnSorting="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="codigo_barras" HeaderText="Código Barras" UniqueName="codigo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unidad" HeaderText="Unidad" UniqueName="unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="disponibles" HeaderText="Disponibles" UniqueName="disponibles" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="factor" HeaderText="Factor" UniqueName="factor" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>--%>
                                    <%--<telerik:GridBoundColumn DataField="existencia_unidad_minima" HeaderText="Existencia Unidad" UniqueName="existencia_unidad_minima" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Right" HeaderText="Cantidad" UniqueName="cantidad">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCantidad" Value="0" MaxValue='<%# Eval("disponibles") %>' runat="server" Width="80px">
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="costo_estandar" HeaderText="Costo Estandar" UniqueName="costo_estandar" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" Visible="false" UniqueName="Add">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_add.gif" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Button ID="btnAgregaConceptos" runat="server" Visible="false" Text="Agregar Conceptos" CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="productslist" runat="server" Width="100%" ShowStatusBar="True" ShowFooter="true"
                        AutoGenerateColumns="False" AllowPaging="False" PageSize="50" GridLines="None"
                        Skin="Office2007">
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                        <MasterTableView Width="100%" DataKeyNames="id" NoMasterRecordsText="No se encontraron registros." Name="Products" AllowMultiColumnSorting="False">
                            <Columns>
                                <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="unidad" HeaderText="Unidad" UniqueName="unidad">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn>
                                    <HeaderTemplate>Cantidad</HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" AutoPostBack="true" MinValue="0" Value='<%# Eval("cantidad") %>' OnTextChanged="txtCantidad_TextChanged" Skin="Default" Width="80px">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                            <EnabledStyle HorizontalAlign="Right" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="costo" HeaderText="Costo Total" UniqueName="costo" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="12%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="precio" Visible="true" HeaderText="Precio Total" UniqueName="precio" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="12%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" HeaderText="Elimar" UniqueName="Delete" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" CausesValidation="false" ImageUrl="~/images/action_delete.gif" />
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
                <td>
                    <asp:HiddenField ID="SucOrigenID" runat="server" Value="0" />
                    <asp:HiddenField ID="SucDestinoID" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    </fieldset>
    <br />
    <br />
    <asp:Button ID="btnFinalizar" runat="server" CausesValidation="false" Text="Finalizar Transferencia" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnImprimir" runat="server" CausesValidation="false" Text="Imprimir" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CausesValidation="false" />
    <br />
    <br />
</asp:Content>
