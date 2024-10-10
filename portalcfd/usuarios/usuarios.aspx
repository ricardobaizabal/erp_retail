<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_usuarios_usuarios" CodeBehind="usuarios.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style4 {
            height: 17px;
        }

        .style5 {
            height: 14px;
        }

        .style6 {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" ClientEvents-OnRequestStart="OnRequestStart" LoadingPanelID="RadAjaxLoadingPanel1">--%>
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <br />
        <table width="100%" cellpadding="5">
            <tr>
                <td class="item" style="width: 12%;">Estatus:</td>
                <td class="item" style="width: 40%;">
                    <asp:DropDownList ID="cmbFiltroEstatus" AutoPostBack="false" runat="server"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="item" style="width: 12%;">Nómbre:</td>
                <td class="item" style="width: 40%;">
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar" CausesValidation="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnAll" runat="server" Text="Ver todo" CausesValidation="false" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
    </fieldset>
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblUsersListLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td style="height: 5px">
                    <telerik:RadGrid ID="userslist" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="userslist_NeedDataSource" PageSize="15" ShowStatusBar="True"
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
                                <telerik:GridBoundColumn DataField="email" HeaderText="Usuario" UniqueName="email">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="perfil" HeaderText="Perfil" UniqueName="perfil">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus">
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
                    <asp:Button ID="btnAddUser" runat="server" CausesValidation="False" CssClass="item" />
                </td>
            </tr>
            <tr>
                <td style="height: 2px">&nbsp;</td>
            </tr>
        </table>
    </fieldset>
    <br />
    <asp:Panel ID="panelUserRegistration" runat="server" Visible="False">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblUserEditLegend" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table width="100%" border="0">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblNombre" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="25%" class="style6">
                        <asp:Label ID="lblFoto" runat="server" CssClass="item" Font-Bold="true" Text="Foto:"></asp:Label>&nbsp;
                        <asp:ImageButton ID="imgBtnEliminarFoto" runat="server" CausesValidation="false" ToolTip="Eliminar foto" ImageUrl="~/images/action_delete.gif" Visible="false" />
                    </td>
                    <td width="25%" class="style6">
                        <asp:Label ID="lblHuella" runat="server" CssClass="item" Font-Bold="true" Text="Huella:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadTextBox ID="txtNombre" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td rowspan="10" valign="top" style="text-align: right;">
                        <asp:FileUpload ID="foto" runat="server" /><br />
                        <asp:Image ID="imgFoto" runat="server" Visible="false" Width="200px" BorderColor="gray" BorderStyle="Solid" BorderWidth="1px" />
                        <asp:HiddenField ID="hdnFoto" runat="server" Value="" />
                    </td>
                    <td rowspan="10" valign="top" style="text-align: right;">
                        <br />
                        <asp:Image ID="imgHuella" runat="server" Visible="true" Width="200px" BorderColor="gray" BorderStyle="Solid" BorderWidth="1px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:RequiredFieldValidator ID="valNombre" runat="server" ControlToValidate="txtNombre" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="style6">
                        <asp:Label ID="lblEmail" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="25%" class="style6">
                        <asp:Label ID="lblContrasena" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="style6">
                        <telerik:RadTextBox ID="txtEmail" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td width="25%" class="style6">
                        <telerik:RadTextBox ID="txtContrasena" runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="style6">&nbsp;</td>
                    <td width="25%" class="style6">
                        <asp:RequiredFieldValidator ID="valContrasena" runat="server" ControlToValidate="txtContrasena" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="style6">
                        <asp:Label ID="lblPerfil" runat="server" CssClass="item" Font-Bold="true" Text="Perfil:"></asp:Label>
                    </td>
                    <td width="25%" class="style6">
                        <asp:Label ID="lblSucursal" runat="server" CssClass="item" Font-Bold="true" Text="Sucursal:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="cmbPerfil" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbSucursal" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="style6">
                        <asp:RequiredFieldValidator ID="valPerfil" runat="server" ControlToValidate="cmbPerfil" InitialValue="0" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td width="25%" class="style6">
                        <asp:RequiredFieldValidator ID="valSUcursal" runat="server" ControlToValidate="cmbSucursal" InitialValue="0" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="style6">
                        <asp:Label ID="lblEstatus" runat="server" CssClass="item" Font-Bold="true" Text="Estatus:"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td width="25%" class="style6">
                        <asp:DropDownList ID="cmbEstatus" runat="server"></asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblConfiguracion" runat="server" CssClass="item" Font-Bold="true" Text="Configuración de permisos en POS:"></asp:Label>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:CheckBox ID="chkConteoDiario" Font-Bold="true" runat="server" Text="Crear conteos diarios" CssClass="item" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="2" style="text-align:right">
                        <asp:Button ID="btnSaveUser" runat="server" CssClass="item" />&nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="item" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="style6">
                        <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                        <asp:HiddenField ID="UsersID" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <%--</telerik:RadAjaxPanel>--%>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>
