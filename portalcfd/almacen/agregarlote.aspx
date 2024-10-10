<%@ Page Language="vb" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="agregarlote.aspx.vb" Inherits="LinkiumCFDI.agregarlote" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
           <asp:Label ID="lblEntradas" runat="server" Font-Bold="true" CssClass="item" Text="Agregando nuevo lote de transferencia"></asp:Label>
        </legend>
        <br />
        <table border="0" cellpadding="2" cellspacing="0" align="center" width="100%">
            <tr>
                <td class="item">
                    Origen: <asp:DropDownList id="origenid" runat="server" AutoPostBack="true"></asp:DropDownList>&nbsp;<asp:RequiredFieldValidator ID="valOrigen" runat="server" ErrorMessage="Requerido" ForeColor="Red" ControlToValidate="origenid" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator><br /><br />
                    Destino: <asp:DropDownList ID="destinoid" runat="server" AutoPostBack="false"></asp:DropDownList>&nbsp;<asp:RequiredFieldValidator ID="valDestino" runat="server" ErrorMessage="Requerido" ForeColor="Red" ControlToValidate="destinoid" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator><br /><br />
                    Comentario:<br />
                    <asp:TextBox ID="txtComentario" runat="server" Width="600px" Height="80px" TextMode="MultiLine"></asp:TextBox><br /><br />
                    <asp:Button ID="btnAdd" runat="server" Text="Guardar" /><br />
                </td>
            </tr>
            <tr><td><br /></td></tr>
        </table>
    </fieldset>
    <br />
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Office2007" EnableShadow="false">
        <Localization OK="Aceptar" Cancel="Cancelar" />
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>