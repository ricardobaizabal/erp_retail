<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="GruposEscolares.aspx.vb" Inherits="LinkiumCFDI.GruposEscolares" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                 <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblUsersListLegend" runat="server" Font-Bold="true" CssClass="item">Listado de grupo escolares</asp:Label>
            </legend>
            <table width="50%" border="0">
                <tr>
                    <td style="height: 5px">
                        <telerik:RadGrid ID="GruposEscolaresList" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None" PageSize="15" ShowStatusBar="True"
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="GrupoEscolar" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="Folio" HeaderStyle-Font-Bold="true" UniqueName="id">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Grado" HeaderStyle-Font-Bold="true" UniqueName="nombre">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete" HeaderText="Eliminar">
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
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar grupo escolar" CausesValidation="False" CssClass="item" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
        <asp:Panel ID="panelRegistration" runat="server" Visible="False">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/AgreEditUsuario_03.jpg" ImageAlign="AbsMiddle" />&nbsp;
                    <asp:Label ID="lblUserEditLegend" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table width="100%" border="0">
                    <tr>
                        <td style="width:25%;">
                            <asp:Label ID="lblGradoEscolar" runat="server" CssClass="item" Text="Grado escolar:" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width:25%;">
                            <asp:Label ID="lblNombre" runat="server" CssClass="item" Text="Grupo:" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width:50%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:25%;">
                            <asp:DropDownList ID="cmbGradoEscolar" runat="server" CssClass="box" Width="90%"></asp:DropDownList>
                        </td>
                        <td style="width:25%;">
                            <telerik:RadTextBox ID="txtNombre" runat="server" Width="100%">
                            </telerik:RadTextBox>
                        </td>
                        <td style="width:50%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:25%;">&nbsp;
                            <asp:RequiredFieldValidator ID="valGradoEscolar" runat="server" ForeColor="Red" ControlToValidate="txtNombre" CssClass="item" Text="Requerido"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width:25%;">&nbsp;
                            <asp:RequiredFieldValidator ID="valNombre" runat="server" ForeColor="Red" ControlToValidate="txtNombre" CssClass="item" Text="Requerido"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width:50%;">&nbsp;</td>
                    </tr>
                    <tr valign="top">
                      <td align="right" colspan="2">
                          <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="item" />&nbsp;<asp:Button ID="btnCancelar" CausesValidation="false" Text="Cancelar" runat="server" CssClass="item" />
                      </td>
                      <td style="width:50%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:HiddenField ID="GrupoID" runat="server" Value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>