<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_Reportes" CodeBehind="Reportes.aspx.vb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:Panel ID="panelReportes" runat="server">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblReportsLegend" runat="server" Font-Bold="true" CssClass="item" Text="Facturacion"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td style="height: 5px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="item">
                            <asp:Image ID="rptAsistencia" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="lnkAsistencia" runat="server" Text="Reporte Asistencia" NavigateUrl="~/portalcfd/reportes/asistencia.aspx" CssClass="item"></asp:HyperLink><br /><br />
                            <asp:Image ID="rptInventarios" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="lnkInventarios" runat="server" Text="Reporte Inventarios" NavigateUrl="~/portalcfd/reportes/inventarios.aspx" CssClass="item"></asp:HyperLink><br /><br />
                            <asp:Image ID="rptCortesCaja" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="lnkCortesCaja" runat="server" Text="Reporte Cortes de Caja" NavigateUrl="~/portalcfd/reportes/cortes.aspx" CssClass="item"></asp:HyperLink><br /><br />
                            <asp:Image ID="rptConteoDiario" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="lnkConteoDiario" runat="server" Text="Reporte Conteo Diario" NavigateUrl="~/portalcfd/reportes/conteodiario.aspx" CssClass="item"></asp:HyperLink><br /><br />
                            <asp:Image ID="rptEntradaInventario" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="lnkEntradaInventario" runat="server" Text="Reporte Entradas de Inventario" NavigateUrl="~/portalcfd/reportes/entradas.aspx" CssClass="item"></asp:HyperLink><br /><br />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="panelPedidos" runat="server">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="item" Text="Pedidos"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td style="height: 5px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="item">
                            <asp:Image ID="imgPdetallado" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" Text="Reporte Detallado de Pedidos" NavigateUrl="~/portalcfd/pedidos/DetallePedido.aspx" CssClass="item"></asp:HyperLink><br /><br />
                            <asp:Image ID="imgPVenta" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="HyperLink2" runat="server" Text="Reporte de Ventas" NavigateUrl="~/portalcfd/pedidos/DetalleVenta.aspx" CssClass="item"></asp:HyperLink><br /><br />
                            <asp:Image ID="imgPAcumulado" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="HyperLink3" runat="server" Text="Reporte Acumulado" NavigateUrl="~/portalcfd/pedidos/Acumulado.aspx" CssClass="item"></asp:HyperLink><br /><br />
                            <asp:Image ID="imgpVentaPago" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="HyperLink5" runat="server" Text="Reporte Acumulado por Tipo de Pago" NavigateUrl="~/portalcfd/pedidos/DetalleVentatipopago.aspx" CssClass="item"></asp:HyperLink><br /><br />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>
</asp:Content>