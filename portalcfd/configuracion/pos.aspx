<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="pos.aspx.vb" Inherits="LinkiumCFDI.pos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/CONFIGURACION.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblTitle" runat="server" Font-Bold="true" CssClass="item">Configuraciones generales POS</asp:Label>
            </legend>
            <table width="50%" border="0">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblHabilitarVentaPresentacionesTitle" runat="server" Text="Seleccione las sucursales para venta de presentaciones en POS:" Font-Bold="true" CssClass="item"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadComboBox RenderMode="Lightweight" ID="cmbSucursal" runat="server" CheckBoxes="true" Width="100%" Localization-CheckAllString="Seleccionar todo" Localization-ItemsCheckedString="sucursales seleccionadas" Localization-AllItemsCheckedString="Todas seleccionadas" EnableCheckAllItemsCheckBox="true" EmptyMessage="--Seleccione--">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Button ID="btnCancelar" CausesValidation="false" Text="Cancelar" runat="server" CssClass="item" />&nbsp;&nbsp;
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CausesValidation="False" CssClass="item" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
