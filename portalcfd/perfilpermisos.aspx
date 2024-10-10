<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="perfilpermisos.aspx.vb" Inherits="LinkiumCFDI.perfilpermisos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .ulwrapper {
            background: #B8CEDC;
            margin: auto;
            padding: 0;
            width: 108px;
            height: 20px;
            position: relative;
        }

        .Label {
            /*display: block;*/
            text-align: left;
            line-height: 150%;
            font-size: 10pt;
            font-family: Verdana;
            text-decoration: none;
            /*color: white;*/
        }

        .Level-0 {
            font-weight: bold;
        }

        .Level-1 {
            padding-left: 5% !important;
        }

        .Level-2 {
            padding-left: 10% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <%--<asp:HiddenField ID="hIdPerfil" runat="server" Value="0" />--%>
        <table>
            <tr>
                <td style="width: 10%;">
                    <asp:Label ID="Label1" runat="server" CssClass="Label" Font-Bold="True">Perfil:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbPerfil" runat="server" CausesValidation="false" Width="20%" CssClass="item" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <telerik:RadGrid ID="grdAccesos" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" GridLines="None" AllowSorting="true"
                        PageSize="50" ShowStatusBar="True" HeaderStyle-Font-Bold="true"
                        Skin="Office2007" Width="65%">
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <MasterTableView AllowMultiColumnSorting="true" AllowSorting="true" DataKeyNames="IdPerfilPermisos, Level" Name="Perfiles" Width="100%">
                            <Columns>
                                <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Menu" UniqueName="Descripcion"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkLectura" runat="server" Checked='<%# IIf(Eval("Lectura") Is DBNull.Value, "False", Eval("Lectura"))%>' AutoPostBack="True" OnCheckedChanged="ToggleRowSelection" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkLectura_header" runat="server" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True" />&nbsp;<span>Lectura</span>
                                    </HeaderTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEscritura" runat="server" Checked='<%# IIf(Eval("Escritura") Is DBNull.Value, "False", Eval("Escritura"))%>' AutoPostBack="True" OnCheckedChanged="ToggleRowSelection" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkEscritura_header" runat="server" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True" />&nbsp;<span>Escritura</span>
                                    </HeaderTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEliminar" runat="server" Checked='<%# IIf(Eval("Eliminacion") Is DBNull.Value, "False", Eval("Eliminacion"))%>' AutoPostBack="True" OnCheckedChanged="ToggleRowSelection" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkEliminar_header" runat="server" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True" />&nbsp;<span>Eliminar</span>
                                    </HeaderTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
</asp:Content>