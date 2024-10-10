<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="registro_porta_web.aspx.vb" Inherits="LinkiumCFDI.registro_porta_web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .RadWindow .rwTitle {
            font-family: Tahoma !important;
        }
        .rwDialogMessage {
            font-size: 12px !important;
            font-family: Tahoma !important;
        }
        .RadWindow_Office2007 .rwDialogButtons button {
            font-family: Tahoma !important;
        }
    </style>
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("btnDownload") > -1) || (arguments.get_eventTarget().indexOf("pedidosList") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <asp:Panel ID="panelListado" runat="server" DefaultButton="btnSearch">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table style="width: 100%;" border="0">
                    <tr>
                        <td class="item" style="width: 12%; font-weight: bold;">Críterio de búsqueda:</td>
                        <td class="item" style="width: 40%;">
                            <asp:TextBox ID="txtBusqueda" runat="server" CssClass="box" Width="95%" placeholder="Matrícula, nombre del alumno, nombre del padre/madre tutor, correo electrónico"></asp:TextBox>
                        </td>
                        <td class="item" style="width: 6%; font-weight: bold;">Sucursal:</td>
                        <td class="item" style="width: 15%;">
                            <asp:DropDownList ID="cmbSucursalb" Skin="Silk" runat="server" AutoPostBack="false" Width="95%"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Buscar" CausesValidation="false" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" CausesValidation="false" Text="Ver todo" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProvidersListLegend" runat="server" Text="Listado de acceso portal web" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>
                <table width="100%" border="0">
                    <tr>
                        <td style="height: 5px" colspan="2">
                            <telerik:RadGrid ID="registroWebList" runat="server" Width="100%" ShowStatusBar="True" AllowSorting="true"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None" ExportSettings-ExportOnlyData="false"
                                Skin="Office2007" ShowFooter="true">
                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <ExportSettings IgnorePaging="True" FileName="CatalogoPresentaciones">
                                    <Excel Format="Biff" />
                                </ExportSettings>
                                <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Pedidos" NoMasterRecordsText="No se encontraron registros para mostrar" Width="100%" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="exportar a pdf" ExportToExcelText="exportat a excel"></CommandItemSettings>
                                    <Columns>

                                        <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Folio" UniqueName="id" SortExpression="id">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEditar" Text='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn DataField="matricula" HeaderText="Matrícula" UniqueName="matricula" SortExpression="matricula">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="nombre_alumno" HeaderText="Alumno" UniqueName="nombre_alumno" SortExpression="nombre_alumno">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="nombre_padre_madre" HeaderText="Padre/madre tutor" UniqueName="nombre_padre_madre" SortExpression="nombre_padre_madre">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="email" HeaderText="Email" UniqueName="email">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="contrasena" HeaderText="Contraseña" UniqueName="contrasena">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha Alta" UniqueName="fecha">
                                        </telerik:GridBoundColumn>

                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr style="height: 50px">
                        <td>
                            <div id="dmensaje" style="display: none" class="div">
                                <asp:Label ID="lblMensajeGuardar" runat="server" CssClass="item" ForeColor="#ffffff" Font-Bold="true" Font-Size="Small"></asp:Label>
                            </div>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 2px" colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
            <br />
        </asp:Panel>
        <asp:Panel ID="panelRegistroPortalWeb" runat="server" Visible="False">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/icons/AgregarEditarProveedor_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblEditar" Text="Editar Registro Web" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table border="0" width="100%">
                    <tr style="height: 25px;">
                        <td width="20%">
                            <asp:Label ID="lblMatricula" runat="server" Text="Matrícula:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="40%" colspan="2">
                            <asp:Label ID="lblNombreAlumno" runat="server" Text="Nombre alumno:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="40%" colspan="2">
                            <asp:Label ID="lblNombrePadreMadre" runat="server" Text="Nombre del padre/madre:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <telerik:RadTextBox ID="txtMatricula" runat="server" Width="70%"></telerik:RadTextBox>
                        </td>
                        <td width="40%" colspan="2">
                            <telerik:RadTextBox ID="txtNombreAlumno" runat="server" Width="80%"></telerik:RadTextBox>
                        </td>
                        <td width="40%" colspan="2">
                            <telerik:RadTextBox ID="txtNombrePadreMadre" runat="server" Width="80%"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lblContrasena" runat="server" Text="Contraseña:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="40%" colspan="2">
                            <asp:Label ID="lblEmail" runat="server" Text="Email padre/madre tutor:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="40%" colspan="2">
                            <asp:Label ID="lblSucursalPedido" runat="server" Text="Sucursal:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <telerik:RadTextBox ID="txtContrasena" runat="server" Width="70%"></telerik:RadTextBox>
                        </td>
                        <td width="40%" colspan="2">
                            <telerik:RadTextBox ID="txtEmail" runat="server" InputType="Email" Width="80%"></telerik:RadTextBox>
                        </td>
                        <td width="40%" colspan="2">
                            <asp:DropDownList ID="cmbSucursal" Skin="Silk" runat="server" Width="80%" AutoPostBack="true"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="2">&nbsp;
                            <asp:RegularExpressionValidator ID="reEmail" runat="server" ControlToValidate="txtEmail" SetFocusOnError="true" ErrorMessage="Formato email no válido" CssClass="item" ValidationExpression=".*@.*\..*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="bottom" colspan="5">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" />&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                            <asp:HiddenField ID="registroID" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <telerik:RadWindowManager ID="rwAlerta" runat="server" CenterIfModal="true" Modal="true" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight" Behaviors="Close"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
</asp:Content>
