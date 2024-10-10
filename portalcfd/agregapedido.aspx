<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="agregapedido.aspx.vb" Inherits="LinkiumCFDI.agregapedido" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblAgregarPedido" Text="Agregar Pedido" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td style="height: 5px">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 50%;" colspan="2" class="item">
                    <strong>Cliente:</strong>
                </td>
                <td style="width: 25%;" class="item">
                    <strong>Sucursal:</strong>
                    </td>
                <td style="width: 25%;" class="item">
                    <strong>Orden de Compra:</strong>
                </td>
            </tr>
            <tr>
                <td style="width: 50%;" colspan="2">
                    <asp:DropDownList ID="cmbCliente" runat="server" Width="90%" ValidationGroup="vgPedido"></asp:DropDownList>
                </td>
                <td align="left" style="width: 25%;">
                    <asp:DropDownList ID="cmbSucursal" runat="server" Width="90%" ValidationGroup="vgPedido" Enabled="false"></asp:DropDownList>
                </td>
                <td align="left" style="width: 25%;">
                    <telerik:RadTextBox ID="txtOrdenCompra" Width="90%" runat="server" CssClass="item">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 50%;" colspan="2">
                    <asp:RequiredFieldValidator ID="valCliente" runat="server" ControlToValidate="cmbCliente" ErrorMessage="Selecciona un cliente" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ValidationGroup="vgPedido"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 25%;">
                    <asp:RequiredFieldValidator ID="valSucursal" runat="server" ControlToValidate="cmbSucursal" ErrorMessage="Selecciona una sucursal" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ValidationGroup="vgPedido"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 25%;">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:Button ID="btnCancelar" CausesValidation="false" runat="server" Text="Cancelar" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCrearPedido" ValidationGroup="vgPedido" runat="server" Text="Crear Pedido" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 50%;" colspan="2">&nbsp;</td>
                <td style="width: 25%;">&nbsp;</td>
                <td style="width: 25%;">&nbsp;</td>
            </tr>
            <tr valign="top" style="height: 20px;">
                <td colspan="4">
                    <asp:Label ID="lblMensaje" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 5px">&nbsp;</td>
            </tr>
        </table>
    </fieldset>
</asp:Content>