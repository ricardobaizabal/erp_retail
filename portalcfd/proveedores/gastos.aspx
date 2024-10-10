<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="gastos.aspx.vb" Inherits="LinkiumCFDI.gastos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("lnkEditar") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblTitle" runat="server" Text="Módulo de Gastos" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" border="0" cellpadding="0" cellspacing="3">
                <tr style="height: 10px;">
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="font-weight: bold; width: 50%;" colspan="4">Sucursal:</td>
                </tr>
                <tr>
                    <td colspan="4" style="width:50%;">
                        <asp:DropDownList ID="sucursalid" runat="server" CssClass="box"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RequiredFieldValidator ID="valSucursal" runat="server" ControlToValidate="sucursalid" ErrorMessage="Seleccione una sucursal" InitialValue="0" ForeColor="Red" SetFocusOnError="true" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr valign="top">
                    <td class="item" style="font-weight: bold; width: 50%;" colspan="2">Proveedor:</td>
                    <td class="item" style="font-weight: bold; width: 50%;" colspan="2">Descripcion del gasto:</td>
                </tr>
                <tr valign="top">
                    <td class="item" style="font-weight: bold; width: 50%;" colspan="2">
                        <asp:DropDownList ID="proveedorid" runat="server" Width="90%" AutoPostBack="false" CssClass="box"></asp:DropDownList>
                    </td>
                    <td class="item" style="font-weight: bold; width: 50%;" colspan="2">
                        <asp:TextBox ID="txtDescGasto" TextMode="MultiLine" Width="95%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr valign="top">
                    <td class="item" style="width: 50%;" colspan="2">
                        <asp:RequiredFieldValidator ID="valProveedor" runat="server" ControlToValidate="proveedorid" ErrorMessage="Seleccione un proveedor" InitialValue="0" ForeColor="Red" SetFocusOnError="true" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td class="item" style="width: 50%;" colspan="2">
                        <asp:RequiredFieldValidator ID="valDescripcionGasto" runat="server" ControlToValidate="txtDescGasto" ErrorMessage="Debes proporcionar una descripción del gasto" ForeColor="Red" SetFocusOnError="true" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr valign="top">
                    <td class="item" style="font-weight: bold; width: 25%;">Monto:</td>
                    <td class="item" style="font-weight: bold; width: 25%;">Fecha Promesa de Pago:</td>
                    <td class="item" style="font-weight: bold; width: 25%;">No. de Documento:</td>
                </tr>
                <tr valign="top">
                    <td class="item" style="font-weight: bold; width: 25%;">
                        <telerik:RadNumericTextBox ID="txtMonto" runat="server" Width="120px" NumberFormat-DecimalDigits="2" Type="Currency"></telerik:RadNumericTextBox>
                        MXN
                    </td>
                    <td class="item" style="font-weight: bold; width: 25%;">
                        <telerik:RadDatePicker ID="calFechaPago" Width="140px" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                    </td>
                    <td class="item" style="font-weight: bold; width: 25%;">
                        <asp:TextBox ID="txtNoDocumento" Width="120px" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr valign="top">
                    <td class="item" style="width: 25%;">
                        <asp:RequiredFieldValidator ID="valMonto" runat="server" ControlToValidate="txtMonto" ErrorMessage="Debes proporcionar un monto" ForeColor="Red" SetFocusOnError="true" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td class="item" colspan="3">
                        <asp:RequiredFieldValidator ID="valFechaPago" runat="server" ControlToValidate="calFechaPago" ErrorMessage="Debes proporcionar una fecha promesa de pago" ForeColor="Red" SetFocusOnError="true" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 15px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="font-weight: bold; width: 75%;" colspan="3">&nbsp;</td>
                    <td class="item" style="font-weight: bold; width: 25%;" align="right">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" CausesValidation="false" Text="Cancelar" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 15px">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <telerik:RadGrid ID="facturaslist" runat="server" Width="100%" ShowStatusBar="True" ShowFooter="true"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None"
                            Skin="Office2007" AllowFilteringByColumn="false">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" NoMasterRecordsText="No se encontraron registros para mostrar" Name="Gastos" Width="100%">
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Folio" HeaderStyle-HorizontalAlign="Center" UniqueName="Ver">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVer" runat="server" Text='<%# Eval("id") %>' CausesValidation="false" CommandArgument='<%# Eval("id") %>' CommandName="cmdVer"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="proveeedor" HeaderText="Proveedor" UniqueName="proveedor" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion_gasto" HeaderText="Descripción Gasto Pago" UniqueName="descripcion_gasto" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="monto" HeaderText="Monto" ItemStyle-HorizontalAlign="Right" UniqueName="monto" DataFormatString="{0:c}" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="no_documento" HeaderText="No. Dcomuento" UniqueName="no_documento" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fecha_promesa_pago" HeaderText="Fecha Promesa de Pago" DataFormatString="{0:d}" UniqueName="fecha_promesa_pago" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus" AllowFiltering="false">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Eliminar">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CausesValidation="false" CommandArgument='<%# Eval("id") %>' CommandName="cmdEliminar"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                    <td colspan="4" style="height: 15px">
                        <asp:HiddenField ID="GastoID" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </telerik:RadAjaxPanel>
</asp:Content>
