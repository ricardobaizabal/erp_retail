<%@ Page Language="VB" AutoEventWireup="false" Inherits="LinkiumCFDI._Default" Codebehind="Default.aspx.vb" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%; padding-top:10px;" align="center">
    <div align="center" id="login">
	    <table border="0" cellpadding="10" cellspacing="0" align="center" style="width:300px; height:300px; padding-top:210px;">
            <tr valign="top">
                <td align="right">Usuario: </td>
                <td>
                    <asp:TextBox ID="email" runat="server" Width="150px"></asp:TextBox>&nbsp;&nbsp;<asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="email" ErrorMessage="* Requerido" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr valign="top">
                <td align="right">Contraseña: </td>
                <td>
                    <asp:textbox ID="contrasena" runat="server" Width="150px" TextMode="Password"></asp:textbox>&nbsp;&nbsp;<asp:RequiredFieldValidator ID="valContrasena" runat="server" ControlToValidate="contrasena" ErrorMessage="* Requerido" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr valign="top">
                <td>&nbsp;</td>
                <td align="left">
                    <asp:Button ID="btnLogin" runat="server" Width="80px" Text="Entrar" /><br />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td align="left"><asp:CheckBox ID="chkRemember" runat="server" Text="Recordar mis datos" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Label ID="lblMensaje" ForeColor="Red" CssClass="item" runat="server"></asp:Label></td>
            </tr>            
        </table>
	    <br />
        <br />         
        <br />
        <br />
    </div>  
    <fieldset class="footer">
        <a href="http://www.linkium.mx" class="footer" target="_blank"><asp:Image ID="imgLogoLinkium" Width="60" runat="server" ImageUrl="~/images/icons/logolinkium.jpg" BorderStyle="None" /> Software Development & IT Consulting</a>
    </fieldset>
    </div>
    </form>
</body>
</html>