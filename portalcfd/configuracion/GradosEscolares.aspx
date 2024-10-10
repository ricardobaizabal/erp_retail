<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="GradosEscolares.aspx.vb" Inherits="LinkiumCFDI.GradosEscolares" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblUsersListLegend" runat="server" Font-Bold="true" CssClass="item">Listado de grados escolares</asp:Label>
            </legend>
            <table width="50%" border="0">
                <tr>
                    <td style="height: 5px">
                        <telerik:RadGrid ID="GradosEscolaresList" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None" PageSize="15" ShowStatusBar="True"
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <SortingSettings SortedBackColor="#FFF6D6" EnableSkinSortStyles="false"></SortingSettings>
                            <MasterTableView AllowSorting="true" AllowMultiColumnSorting="False" DataKeyNames="id" Name="GradoEscolar" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="Folio" HeaderStyle-Font-Bold="true" UniqueName="id">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Grado" HeaderStyle-Font-Bold="true" UniqueName="nombre" AllowSorting="true">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="nivel" HeaderText="Nivel" HeaderStyle-Font-Bold="true" UniqueName="nivel" AllowSorting="true">
                                    </telerik:GridBoundColumn>
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
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar grado escolar" CausesValidation="False" CssClass="item" />
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
                        <td style="width: 50%;">
                            <asp:Label ID="lblNivelEscolar" runat="server" CssClass="item" Text="Nivel escolar:" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 50%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">
                            <asp:DropDownList ID="cmbNivelEscolar" runat="server" CssClass="box" Width="100%"></asp:DropDownList>
                        </td>
                        <td style="width: 50%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">
                            <asp:RequiredFieldValidator ID="valNivelEscolar" runat="server" ForeColor="Red" ControlToValidate="cmbNivelEscolar" InitialValue="0" CssClass="item" Text="Requerido"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 50%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">
                            <asp:Label ID="lblNombre" runat="server" CssClass="item" Text="Grado:" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 50%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">
                            <telerik:RadTextBox ID="txtNombre" runat="server" Width="100%">
                            </telerik:RadTextBox>
                        </td>
                        <td style="width: 50%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 50%;">
                            <asp:RequiredFieldValidator ID="valNombre" runat="server" ForeColor="Red" ControlToValidate="txtNombre" CssClass="item" Text="Requerido"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 50%;">&nbsp;</td>
                    </tr>
                    <tr valign="top">
                        <td align="right">
                            <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="item" />&nbsp;<asp:Button ID="btnCancelar" CausesValidation="false" Text="Cancelar" runat="server" CssClass="item" />
                        </td>
                        <td style="width: 50%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:HiddenField ID="GradoID" runat="server" Value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
