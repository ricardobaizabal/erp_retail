<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="PedidosPlazo.aspx.vb" Inherits="LinkiumCFDI.PedidosPlazo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                 <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblUsersListLegend" runat="server" Font-Bold="true" CssClass="item">Ingresar Plazo de Pago</asp:Label>
            </legend>
            <table width="50%" border="0">
                <tr>
                    <td style="height: 5px" class="item">
                    
                        Dias: <asp:TextBox ID="txtdias" runat="server" CssClass="box" Width="80px" ></asp:TextBox>&nbsp
                        <asp:Button ID="btnAgregar" runat="server" Text="Guardar" CausesValidation="False" CssClass="item" />
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
                        <td style="width:50%;">
                            <asp:Label ID="lblNombre" runat="server" CssClass="item" Text="Sucursal:" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width:50%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:50%;">
                            <telerik:RadTextBox ID="txtNombre" runat="server" Width="100%">
                            </telerik:RadTextBox>
                        </td>
                        <td style="width:50%;">&nbsp;
                            <asp:RequiredFieldValidator ID="valNombre" runat="server" ForeColor="Red" ControlToValidate="txtNombre" CssClass="item" Text="Requerido"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan=2>&nbsp;</td>
                    </tr>
                    <tr valign="top">
                      <td align="right">
                          <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="item" />&nbsp;<asp:Button ID="btnCancelar" CausesValidation="false" Text="Cancelar" runat="server" CssClass="item" />
                      </td>
                      <td style="width:50%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:HiddenField ID="SucursalID" runat="server" Value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan=2>&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>