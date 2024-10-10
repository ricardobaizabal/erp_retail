<%@ Page Title="" Language="vb" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="subfamilias.aspx.vb" Inherits="LinkiumCFDI.subfamilias" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript">
     function OnRequestStart(target, arguments) {
         if (arguments.get_eventTarget().indexOf("grdCategorias") > -1) {
             arguments.set_enableAjax(false);
         } 
     }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
    <fieldset>
        <legend style="padding-right:6px; color:Black">
            <asp:Label ID="lblSubFamiliasList" runat="server" Text="Lista de SubFamilias" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%" border="0">
            <tr>
                <td style="height:2px">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="grdSubFamilias" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" GridLines="None" AllowSorting="true"
                        PageSize="15" ShowStatusBar="True" 
                        Skin="Office2007" Width="100%">
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <MasterTableView AllowMultiColumnSorting="true" AllowSorting="true" DataKeyNames="id" Name="SubFamilias" Width="100%">
                            <Columns>
                                <telerik:GridBoundColumn DataField="id" HeaderText="Id" HeaderStyle-Width="50px" UniqueName="id" Visible="false"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="SubFamilia" DataField="nombre" SortExpression="nombre" UniqueName="nombre">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="familia" HeaderText="Familia" UniqueName="familia"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="porcentaje_utilidad" HeaderText="Porcentaje Utilidad" UniqueName="porcentaje_utilidad"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" 
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>'  DataFormatString="{0:P}" CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
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
                <td style="height:2px">&nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnAgregaSubFamilia" runat="server" Text="Agrega SubFamilia" CausesValidation="False" CssClass="item" />
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <asp:Panel ID="panelRegistroSubFamilia" runat="server" Visible="False">
        <fieldset>
            <legend style="padding-right:6px; color:Black">
                <asp:Label ID="lblRegistroTitle" runat="server" Text="Agregar/Editar SubFamilia" Font-Bold="True" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table width="100%" border="0">
                <tr valign="top">
                    <td width="25%">
                        <asp:Label ID="lblFamilia" runat="server" CssClass="item" Font-Bold="True" Text="Familia:"></asp:Label>
                    </td>
                    <td width="25%">
                        <asp:Label ID="lblNombre" runat="server" CssClass="item" Font-Bold="True" Text="SubFamilia:"></asp:Label>
                    </td>
                    <td width="25%">
                        <asp:Label ID="lblPorcentaje" runat="server" CssClass="item" Font-Bold="True" Text="Porcentaje Utilidad:"></asp:Label>
                    </td>
                    <td width="25%">&nbsp;</td>
                </tr>
                <tr valign="top">
                    <td width="25%">
                        <asp:DropDownList ID="familiaid" runat="server" Width="90%" CssClass="box"></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="familiaid" InitialValue="0" CssClass="item" ForeColor="Red" ErrorMessage=" *" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="txtNombre" CssClass="item" Width="200px" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombre" CssClass="item" ForeColor="Red" ErrorMessage=" *" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td width="25%">
                        <telerik:RadNumericTextBox ID="txtPorcentajeUtilidad" runat="server" MinValue="0" Type="Percent" Skin="Default" Value="0" Width="85%">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td width="25%">
                        <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="item" />&nbsp;<asp:Button ID="btnCancelar" CausesValidation="false" Text="Cancelar" runat="server" CssClass="item" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                        <asp:HiddenField ID="SubFamiliaID" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>