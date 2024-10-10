<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_Home" Codebehind="Home.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #panel 
        {
            width:800px;
        }

        #panel td
        {
            text-align: center;
            line-height:28px;
            font-family: Verdana;
            font-size: 14px;
            color: #333333;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br />
    <fieldset>
        <legend class="item"><strong>Inicio</strong></legend>
        <table border="0" cellpadding="2" cellspacing="2" align="center" style="width:980px;" id="panel">
            <tr>
                <td colspan="5"><br /></td>
            </tr>
            <tr>
                <td align="center"><asp:ImageButton ID="lnk1" runat="server" PostBackUrl="~/portalcfd/Clientes.aspx" ImageUrl="~/images/inicio/clientes.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk2" runat="server" PostBackUrl="~/portalcfd/almacen/Productos.aspx" ImageUrl="~/images/inicio/productos.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk3" runat="server" PostBackUrl="~/portalcfd/CFD.aspx" ImageUrl="~/images/inicio/Facturacion-02.jpg" /></td>                
                <td align="center"><asp:ImageButton ID="lnk10" runat="server" PostBackUrl="~/portalcfd/Datos.aspx" ImageUrl="~/images/inicio/mis-datos-01.jpg" /></td>
            </tr>
            <tr>
                <td colspan="5"><br /></td>
            </tr>
            <tr>
                <td align="center"><asp:ImageButton ID="lnk9" runat="server" PostBackUrl="~/portalcfd/proveedores/proveedores.aspx" ImageUrl="~/images/inicio/proveedores.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk8" runat="server" PostBackUrl="~/portalcfd/almacen/abastecimiento.aspx" ImageUrl="~/images/inicio/inventario.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk5" runat="server" PostBackUrl="~/portalcfd/Reportes.aspx" ImageUrl="~/images/inicio/reportes.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk11" runat="server" PostBackUrl="~/portalcfd/Salir.aspx" ImageUrl="~/images/inicio/salir.jpg" /></td>
            </tr>
            <tr>
                <td colspan="5"><br /></td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

