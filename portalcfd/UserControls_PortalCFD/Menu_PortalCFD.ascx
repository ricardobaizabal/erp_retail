<%@ Control Language="VB" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_usercontrols_portalcfd_Menu_PortalCFD" CodeBehind="Menu_PortalCFD.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="../Styles/Styles.css" rel="stylesheet" type="text/css" />
<telerik:RadMenu ID="RadMenu1" runat="server" Width="100%" Skin="Office2007" Style="z-index: 3000">
</telerik:RadMenu>
<div align="right" class="item">
    <table style="width: 100%;">
        <tr>
            <td style="width: 50%; text-align: left;">
                <asp:Label ID="lblSocialReason" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
            </td>
            <td style="width: 50%; text-align: right;">
                <asp:Label ID="lblContact" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
            </td>
        </tr>
    </table>
</div>