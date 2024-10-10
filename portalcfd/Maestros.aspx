<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="Maestros.aspx.vb" Inherits="LinkiumCFDI.Maestros" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .RadComboBox {
            font-family: Tahoma !important;
        }

        .RadComboBox_Office2007 {
            font-size: 12px !important;
            font-family: Tahoma !important;
        }

            .RadComboBox_Office2007 .rwDialogButtons button {
                font-family: Tahoma !important;
            }

        .RadComboBoxDropDown .rcbItem > label, .RadComboBoxDropDown .rcbHovered > label, .RadComboBoxDropDown .rcbDisabled > label, .RadComboBoxDropDown .rcbLoading > label, .RadComboBoxDropDown .rcbCheckAllItems > label, .RadComboBoxDropDown .rcbCheckAllItemsHovered > label {
            font-size: 12px !important;
            font-family: Tahoma !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("MaestrosList") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <asp:Panel ID="panelBusqueda" runat="server" DefaultButton="btnBuscar">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>
                <br />
                <asp:Panel ID="pBusqueda" runat="server" DefaultButton="btnBuscar">
                    <table>
                        <tr>
                            <td>
                                <span class="item">Nombre maestro(a):</span>&nbsp;&nbsp;
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtSearch" runat="server" Width="300px"></telerik:RadTextBox>
                            </td>
                            <td>
                                <span class="item">Sucursal:</span>&nbsp;&nbsp;
                            </td>
                            <td style="width: 210px;">
                                <telerik:RadComboBox RenderMode="Lightweight" ID="cmbSucursalFiltro" Skin="Office2007" runat="server" Width="200px" EmptyMessage="--Todos--">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CausesValidation="false" />&nbsp;&nbsp;<asp:Button ID="btnTodo" runat="server" Text="Ver todos" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </asp:Panel>
            </fieldset>
            <br />
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblClientsListLegend" runat="server" Font-Bold="true" Text="Listado de Maestros" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table width="100%">
                    <tr>
                        <td style="height: 5px">
                            <telerik:RadGrid ID="MaestrosList" runat="server" AllowPaging="true" ShowHeader="true"
                                AutoGenerateColumns="false" GridLines="None" AllowSorting="true"
                                PageSize="50" ShowStatusBar="true" ExportSettings-ExportOnlyData="true" ExportSettings-HideNonDataBoundColumns="true"
                                Skin="Office2007" Width="100%">
                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <ExportSettings IgnorePaging="True" FileName="ListadoMaestros">
                                    <Excel Format="Biff" />
                                </ExportSettings>
                                <MasterTableView AllowMultiColumnSorting="true" NoMasterRecordsText="No se encontraron registros para mostrar" AllowSorting="true" DataKeyNames="id" Name="Maestros" Width="100%" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToPdfButton="false" ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportar a Excel"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="id" DataType="System.Int32" HeaderText="Folio" UniqueName="id">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Maestro(a)" DataField="nombre" SortExpression="nombre" UniqueName="nombre">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal" ItemStyle-Width="180px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="gradoescolar" HeaderText="Grados escolar(es)" UniqueName="gradoescolar">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="email" HeaderText="Correo electrónico" UniqueName="email">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="contrasena" HeaderText="Contraseña" UniqueName="contrasena">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 5px">
                            <asp:Button ID="btnAgregarMaestro" runat="server" Text="Agregar Maestro" CausesValidation="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 2px">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="panelRegistration" runat="server" Visible="false">
            <table width="100%" border="0">
                <tr>
                    <td style="width: 20%;">
                        <asp:Label ID="lblNombreMaestro" runat="server" CssClass="item" Text="Nombre:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 20%;">
                        <asp:Label ID="lblSucursal" runat="server" CssClass="item" Text="Sucursal:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 20%;">
                        <asp:Label ID="lblGradoEscolar" runat="server" CssClass="item" Text="Grado Escolar:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 20%;">
                        <asp:Label ID="lblCorreoElectronico" runat="server" CssClass="item" Text="Correo electrónico:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 20%;">
                        <asp:Label ID="lblContrasena" runat="server" CssClass="item" Text="Contraseña:" Font-Bold="True"></asp:Label>
                        <asp:Image ID="imgContrasena" runat="server" ImageUrl="~/images/info.png" ToolTip="La contraseña es generada en automático por sistema." ImageAlign="AbsMiddle" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;">
                        <telerik:RadTextBox ID="txtNombreMaestro" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 20%;">
                        <%--<telerik:RadComboBox RenderMode="Lightweight" ID="cmbSucursal" Skin="Office2007" runat="server" CheckBoxes="true" Width="90%" Localization-CheckAllString="Seleccionar todo" Localization-ItemsCheckedString="grados seleccionados" Localization-AllItemsCheckedString="Todos seleccionados" EnableCheckAllItemsCheckBox="true" EmptyMessage="--Seleccione--">
                        </telerik:RadComboBox>--%>
                        <telerik:RadComboBox RenderMode="Lightweight" ID="cmbSucursal" Skin="Office2007" runat="server" Width="90%" EmptyMessage="--Seleccione--">
                        </telerik:RadComboBox>
                    </td>
                    <td style="width: 20%;">
                        <telerik:RadComboBox RenderMode="Lightweight" ID="cmbGradoEscolar" Skin="Office2007" runat="server" CheckBoxes="true" Width="90%" Localization-CheckAllString="Seleccionar todo" Localization-ItemsCheckedString="grados seleccionados" Localization-AllItemsCheckedString="Todos seleccionados" EnableCheckAllItemsCheckBox="true" EmptyMessage="--Seleccione--">
                        </telerik:RadComboBox>
                    </td>
                    <td style="width: 20%;">
                        <telerik:RadTextBox ID="txtCorreoElectronico" runat="server" Skin="Office2007" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 20%;">
                        <telerik:RadTextBox ID="txtContrasena" runat="server" Skin="Office2007" ReadOnly="true" Enabled="false">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;">
                        <asp:RequiredFieldValidator ID="valNombreMaestro" runat="server" ControlToValidate="txtNombreMaestro" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 20%;">
                        <asp:RequiredFieldValidator ID="valSucursal" runat="server" ControlToValidate="cmbSucursal" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 20%;">&nbsp;</td>
                    <td style="width: 20%;">
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" CssClass="item"
                            ControlToValidate="txtCorreoElectronico"
                            ErrorMessage="Dirección de correo electrónico no válida"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ForeColor="Red" />
                    </td>
                    <td style="width: 20%;">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:HiddenField ID="MaestroID" runat="server" Value="0" />
                    </td>
                </tr>
                <tr>
                    <td valign="bottom" colspan="5">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
