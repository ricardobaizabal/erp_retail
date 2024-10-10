<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="FacturaGlobal40.aspx.vb" Inherits="LinkiumCFDI.FacturaGlobal40" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        td {
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panelFiltros" runat="server">
        <fieldset style="padding: 10px;">
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="imgPanel1" runat="server" ImageUrl="~/portalcfd/images/comprobant.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientsSelectionLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table style="width: 100%;" border="0">
                <tr>
                    <td style="width: 18%;" class="item" colspan="6">
                        <strong>Seleccione el rango de fechas a facturar:</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%;" class="item">
                        <strong>Desde:</strong>
                    </td>
                    <td style="width: 20%;" class="item">
                        <telerik:RadDatePicker ID="calFechaInicio" runat="server">
                        </telerik:RadDatePicker>
                    </td>
                    <td style="width: 5%;" class="item">
                        <strong>Hasta:</strong>
                    </td>
                    <td style="width: 20%;" class="item">
                        <telerik:RadDatePicker ID="calFechaFin" runat="server">
                        </telerik:RadDatePicker>
                    </td>
                    <td style="width: 10%;" class="item">
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CausesValidation="true" CssClass="cssBoton" />
                    </td>
                    <td class="item">
                        <%--<asp:RequiredFieldValidator ID="valFechaFactura" runat="server" ErrorMessage="Proporcione la fecha para la factura" ForeColor="Red" ControlToValidate="calFechaInicio" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <asp:Panel ID="panelResume" runat="server" Visible="False">
            <fieldset style="padding: 10px;">
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/portalcfd/images/resumen.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblResume" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table style="width: 100%;">
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblSubTotal" runat="server" CssClass="item" Text="Sub Total = " Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblSubTotalValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblDescuento" runat="server" CssClass="item" Font-Bold="True" Text="Descuento = "></asp:Label>&nbsp;
                        <asp:Label ID="lblDescuentoValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblIVA" runat="server" CssClass="item" Text="IVA = " Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblIVAValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblIEPS" runat="server" CssClass="item" Text="IEPS = " Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblIEPSValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblTotal" runat="server" CssClass="item" Text="Total = " Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblTotalValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="16%">
                            <br />
                            <br />
                            <asp:Button ID="btnCreateInvoice" runat="server" Text="Generar CFDI" CausesValidation="true" CssClass="cssBoton" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancelInvoice" runat="server" Text="Cancelar Operación" CausesValidation="False" CssClass="cssBoton" />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </asp:Panel>
    <telerik:RadWindow ID="RadWindow1" runat="server" RenderMode="Lightweight" Modal="true" CenterIfModal="true" AutoSize="False" Behaviors="Close" VisibleOnPageLoad="False" Width="740" Height="600">
        <ContentTemplate>
            <br />
            <table align="center" width="95%">
                <tr>
                    <td>
                        <asp:TextBox ID="txtErrores" TextMode="MultiLine" Width="100%" Rows="30" ReadOnly="true" CssClass="item" runat="server"></asp:TextBox>
                    </td>
                    <tr>
                        <td align="left" width="16%">
                            <br />
                            <br />
                            <asp:Button ID="btnAceptar" runat="server" CausesValidation="true" CssClass="item" Text="Aceptar" />
                            <br />
                            <br />
                        </td>
                    </tr>
                </tr>
            </table>
            <br />
        </ContentTemplate>
    </telerik:RadWindow>
</asp:Content>