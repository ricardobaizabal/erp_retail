<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="Perfiles.aspx.vb" Inherits="LinkiumCFDI.Perfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblTitulo" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td style="height: 5px">
                    <telerik:RadGrid ID="perfilesList" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" GridLines="None"
                        PageSize="15" ShowStatusBar="True"
                        Skin="Office2007" Width="100%">
                        <PagerStyle Mode="NumericPages" />
                        <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Users" Width="100%">
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="" UniqueName="nombre">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="nombre" HeaderText="Nómbre" UniqueName="nombre">
                                </telerik:GridBoundColumn>
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
                <td style="height: 2px">&nbsp;</td>
            </tr>
            <tr>
                <td align="right" style="height: 5px">
                    <asp:Button ID="btnAgregar" runat="server" CausesValidation="False" CssClass="item" />
                </td>
            </tr>
            <tr>
                <td style="height: 2px">&nbsp;</td>
            </tr>
        </table>
    </fieldset>
    <br />
    <asp:Panel ID="panelAgregarPerfil" runat="server" Visible="False">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblEditarTitle" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table width="100%" border="0">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblNombre" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadTextBox ID="txtNombre" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:RequiredFieldValidator ID="valNombre" runat="server" ControlToValidate="txtNombre" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnGuardar" runat="server" CssClass="item" />&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" CssClass="item" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="style6">
                        <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                        <asp:HiddenField ID="perfilID" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
</asp:Content>
