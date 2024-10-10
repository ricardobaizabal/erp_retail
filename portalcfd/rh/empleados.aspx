<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="empleados.aspx.vb" Inherits="LinkiumCFDI.empleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">        
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("grdEmpleados") > -1) || (arguments.get_eventTarget().indexOf("btnGuardar") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" ClientEvents-OnRequestStart="OnRequestStart" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
               <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <asp:Panel ID="pBusqueda" runat="server" DefaultButton="btnSearch">
            <span class="item">
                Sucursal:&nbsp;<asp:DropDownList ID="cmbSucursalFiltro" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                Estatus:&nbsp;<asp:DropDownList ID="cmbEstatusFiltro" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                Palabra clave: <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Buscar" CausesValidation="false" />&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" Text="Ver todo" CausesValidation="false" />
            </span>
            <br />
            <br />
            </asp:Panel>
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblUsersListLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                        <telerik:RadGrid ID="grdEmpleados" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" ShowStatusBar="True"
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Users" Width="100%">
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Empleado" UniqueName="nombre">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="formapago" HeaderText="Forma Pago" UniqueName="formapago">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus">
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
                    <td style="height: 2px">&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAgregaEmpleado" runat="server" CausesValidation="False" CssClass="item" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
        <asp:Panel ID="panelUserRegistration" runat="server" Visible="false">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblUserEditLegend" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table width="100%" border="0">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblNombre" runat="server" CssClass="item" Text="Nómbre:" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="25%">
                            <asp:Label ID="lblFoto" runat="server" CssClass="item" Font-Bold="true" Text="Foto:"></asp:Label>&nbsp;
                            <asp:ImageButton ID="imgBtnEliminarFoto" runat="server" CausesValidation="false" ToolTip="Eliminar foto" ImageUrl="~/images/action_delete.gif" Visible="false" />
                        </td>
                        <td width="25%">
                            <asp:Label ID="lblHuella" runat="server" CssClass="item" Font-Bold="true" Text="Huella:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtNombre" runat="server" Width="92%">
                            </telerik:RadTextBox>
                        </td>
                        <td rowspan="10" valign="top" style="text-align:right;">
                            <asp:FileUpload ID="foto" runat="server" /><br />
                            <asp:Image ID="imgFoto" runat="server" Visible="false" Width="200px" BorderColor="gray" BorderStyle="Solid" BorderWidth="1px" />
                            <asp:HiddenField id="hdnFoto" runat="server" Value="" />
                        </td>
                        <td rowspan="10" valign="top" style="text-align:right;"><br />
                            <asp:Image ID="imgHuella" runat="server" Visible="true" Width="200px" BorderColor="gray" BorderStyle="Solid" BorderWidth="1px"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="valNombre" runat="server" ControlToValidate="txtNombre" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblEmpresa" runat="server" CssClass="item" Text="Empresa:" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtEmpresa" runat="server" Width="92%">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="valEmpresa" runat="server" ControlToValidate="txtEmpresa" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="lblSucursal" runat="server" CssClass="item" Font-Bold="true" Text="Sucursal:"></asp:Label>
                        </td>
                        <td width="25%">
                            <asp:Label ID="lblFormaPago" runat="server" CssClass="item" Text="Forma Pago:" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:DropDownList ID="cmbSucursal" runat="server"></asp:DropDownList>
                        </td>
                        <td width="25%">
                            <asp:DropDownList ID="cmbFormaPago" runat="server">
                                <asp:ListItem Text="--Seleccione--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Efectivo" Value="E"></asp:ListItem>
                                <asp:ListItem Text="Tarjeta de débito" Value="T"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:RequiredFieldValidator ID="valSucursal" runat="server" ControlToValidate="cmbSucursal" InitialValue="0" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                        </td>
                        <td width="25%">
                            <asp:RequiredFieldValidator ID="valFormaPago" runat="server" ControlToValidate="cmbFormaPago" InitialValue="0" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:Label ID="lblFechaIngreso" runat="server" CssClass="item" Font-Bold="true" Text="Fecha Ingreso:"></asp:Label>
                        </td>
                        <td width="25%">
                            <asp:Label ID="lblEstatus" runat="server" CssClass="item" Font-Bold="true" Text="Estatus:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <telerik:RadDatePicker ID="calFechaIngreso" Runat="server" Skin="Office2007">
                                <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </td>
                        <td width="25%">
                            <asp:DropDownList ID="cmbEstatus" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:RequiredFieldValidator ID="valFechaIngreso" runat="server" ControlToValidate="calFechaIngreso" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                        </td>
                        <td width="25%">
                            <asp:RequiredFieldValidator ID="valEstatus" runat="server" ControlToValidate="cmbEstatus" InitialValue="0" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                        </td>
                        <td width="25%">&nbsp;</td>
                        <td width="25%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnGuardar" runat="server" CssClass="item" />&nbsp;
                            <asp:Button ID="btnCancelar" runat="server" CssClass="item" CausesValidation="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                            <asp:HiddenField ID="UsersID" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    <%--</telerik:RadAjaxPanel>--%>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>
