<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="CFD_Detalle.aspx.vb" Inherits="LinkiumCFDI.CFD_Detalle" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblEstatusCobranza" runat="server" Font-Bold="true" CssClass="item" Text="CFDI Estatus de cobranza"></asp:Label>
        </legend>
        <br />
        <table width="90%">
            <tr>
                <td colspan="5" class="item"><strong>Documento: </strong>
                    <asp:Label ID="lblDocumento" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td class="item">Estatus:<br />
                    <br />
                    <asp:DropDownList ID="estatus_cobranzaid" runat="server" CssClass="box"></asp:DropDownList></td>
                <td class="item">Tipo de pago:<br />
                    <br />
                    <asp:DropDownList ID="tipo_pagoid" runat="server" CssClass="box"></asp:DropDownList></td>
                <td class="item">Referencia:<br />
                    <br />
                    <asp:TextBox ID="referencia" runat="server" CssClass="box"></asp:TextBox></td>
                <td class="item">Fecha de pago:<br />
                    <br />
                    <telerik:RadDatePicker ID="fechapago" runat="server"></telerik:RadDatePicker>
                </td>
                <td style="vertical-align: bottom">
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="boton" /></td>
            </tr>
        </table>
        <br />
        <br />
    </fieldset>
    <br />
    <br />
</asp:Content>
