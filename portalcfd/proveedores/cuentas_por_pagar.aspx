<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="cuentas_por_pagar.aspx.vb" Inherits="LinkiumCFDI.cuentas_por_pagar" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        //        function OnRequestStart(target, arguments) {
        //            if ((arguments.get_eventTarget().indexOf("lnkEditar") > -1)) {
        //                arguments.set_enableAjax(false);
        //            }
        //        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlMetodoPago">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlMetodoPago" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ddlBanco" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ddlCuenta" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtNoChequex" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Panel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Panel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlBanco">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlCuenta" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Panel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Panel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="proveedorid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="facturaslist" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--<telerik:AjaxSetting AjaxControlID="anioid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="facturaslist" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <%--<telerik:AjaxSetting AjaxControlID="mesid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="facturaslist" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="btnConsulta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="facturaslist" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="facturaslist">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnPayAll">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="facturaslist" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="facturaslist" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="Label1" runat="server" Text="Consultar Cuentas por Pagar" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%" border="0" cellpadding="3" cellspacing="0">
            <tr style="height: 10px;">
                <td colspan="7">&nbsp;</td>
            </tr>
            <tr>
                <td class="item" style="font-weight: bold; width: 8%;">Sucursal:</td>
                <td colspan="3">
                    <asp:DropDownList ID="sucursalid" runat="server" Width="90%" CssClass="box"></asp:DropDownList>
                </td>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td class="item" style="font-weight: bold; width: 8%;">Proveedor:</td>
                <td colspan="3">
                    <asp:DropDownList ID="proveedorid" runat="server" Width="90%" CssClass="box"></asp:DropDownList>
                </td>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td class="item" style="font-weight: bold; width: 5%;">Desde:</td>
                <td class="item" style="font-weight: bold; width: 15%;">
                    <telerik:RadDatePicker ID="calFechaInicio" Skin="Office2007" Width="100%" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                </td>
                <td class="item" style="font-weight: bold; width: 5%;">Hasta:</td>
                <td class="item" style="font-weight: bold; width: 15%;">
                    <telerik:RadDatePicker ID="calFechaFin" Skin="Office2007" Width="100%" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                </td>
                <td style="text-align: left;">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
                </td>
            </tr>
            <tr>
                <td colspan="7" style="height: 30px;">&nbsp;</td>
            </tr>
        </table>
    </fieldset>
    <br />
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="Label2" runat="server" Text="Cuentas por Pagar" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%" border="0" cellpadding="0" cellspacing="10" border="0">
            <tr style="height: 10px;">
                <td>
                    <telerik:RadGrid ID="facturaslist" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" GridLines="None" ShowFooter="True"
                        PageSize="50" ShowStatusBar="True" ExportSettings-ExportOnlyData="False"
                        Skin="Office2007" Width="100%">
                        <PagerStyle Mode="NumericPages" />
                        <MasterTableView Width="100%" DataKeyNames="id, tipo, total_pesos" Name="CuentasPorPagar" AllowMultiColumnSorting="False">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="chkid">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkid" runat="server" CssClass="item" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="no_documento" HeaderText="No. Dcomuento" UniqueName="no_documento" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="proveedor" HeaderText="Proveedor" UniqueName="proveedor" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="tipo" HeaderText="Documento" UniqueName="tipo" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="fecha_promesa_pago" HeaderText="Fecha Promesa de Pago" DataFormatString="{0:d}" UniqueName="fecha_promesa_pago" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="moneda" HeaderText="Moneda" UniqueName="moneda" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="total_dolares" HeaderText="Total en Divisa" DataFormatString="{0:c}" UniqueName="total_dolares" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="total_pesos" HeaderText="Total Pesos" DataFormatString="{0:c}" UniqueName="total_pesos" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Editar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server" Text="Editar" CausesValidation="false" CommandArgument='<%# Eval("id") & ";" & Eval("tipo") %>' CommandName="cmdEditar"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <fieldset>
            <legend class="item" style="padding-right: 6px; color: Black">
                <asp:Label ID="Label3" runat="server" Text="Establecer pago grupal" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" border="0">
                <tr>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr valign="top">
                    <td class="item">Estatus:&nbsp;
                    <asp:RequiredFieldValidator ID="valEstatus" ControlToValidate="estatusid" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" InitialValue="0" ValidationGroup="valUpdateAll" runat="server"></asp:RequiredFieldValidator>
                    </td>
                    <td class="item">Tipo de pago:&nbsp;
                    <asp:RequiredFieldValidator ID="valTipoPago" ControlToValidate="metodopagoid" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" InitialValue="0" ValidationGroup="valUpdateAll" runat="server"></asp:RequiredFieldValidator>
                    </td>
                    <td class="item">Fecha de pago:&nbsp;
                    <asp:RequiredFieldValidator ID="valFecha" ControlToValidate="fechapago" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ValidationGroup="valUpdateAll" runat="server"></asp:RequiredFieldValidator>
                    </td>
                    <td class="item">Banco:&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="bancoid" InitialValue="0" CssClass="item" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="valUpdateAll"></asp:RequiredFieldValidator>
                    </td>
                    <td class="item">Cuenta:
                    </td>
                    <td class="item">No. Cheque:&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtNoCheque" CssClass="item" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="valUpdateAll"></asp:RequiredFieldValidator>
                    </td>
                    <td class="item">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item">
                        <asp:DropDownList ID="estatusid" runat="server" CssClass="box"></asp:DropDownList>
                    </td>
                    <td class="item">
                        <asp:DropDownList ID="metodopagoid" runat="server" Width="90%" AutoPostBack="true" CssClass="box"></asp:DropDownList>
                    </td>
                    <td class="item" style="width: 12%">
                        <telerik:RadDatePicker ID="fechapago" Width="120px" Skin="Office2007" runat="server"></telerik:RadDatePicker>
                    </td>
                    <td class="item">
                        <asp:DropDownList ID="bancoid" runat="server" AutoPostBack="true" Width="90%"></asp:DropDownList>
                    </td>
                    <td class="item">
                        <asp:DropDownList ID="cuentaid" runat="server" Width="90%"></asp:DropDownList>
                    </td>
                    <td class="item" style="width: 10%">
                        <asp:TextBox ID="txtNoCheque" Width="90%" runat="server"></asp:TextBox>
                    </td>
                    <td class="item">
                        <asp:Button ID="btnPayAll" ValidationGroup="valUpdateAll" runat="server" Text="Aplicar" /></td>
                </tr>
                <tr>
                    <td colspan="7">
                        <br />
                        <br />
                        <asp:Label ID="lblMensajeActualiza" Font-Bold="true" CssClass="item" runat="server" ForeColor="Green"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="7">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </telerik:RadAjaxPanel>
    <br />
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1" EnableShadow="true" ShowOnTopWhenMaximized="false">
        <Windows>
            <telerik:RadWindow ID="EditarDocumentoWindow" runat="server" VisibleOnPageLoad="false"
                ShowContentDuringLoad="True" Modal="true" CenterIfModal="true" AutoSize="false" ReloadOnShow="True"
                VisibleStatusbar="True" VisibleTitlebar="True" BorderStyle="None" BorderWidth="0px" Behaviors="Close"
                Width="900px" Height="600px" Skin="Office2007">
                <ContentTemplate>
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="Label4" runat="server" Text="Editar Documento" Font-Bold="true" CssClass="item"></asp:Label>
                        </legend>
                        <table width="100%" border="0" cellpadding="0" cellspacing="10">
                            <tr style="height: 10px;">
                                <td colspan="5">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="item" style="font-weight: bold; width: 20%;">Fecha de Pago:&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="calFechaPago" CssClass="item" ForeColor="Red" ErrorMessage="*" ValidationGroup="gpEdita" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td class="item" style="font-weight: bold; width: 20%;">Estatus:</td>
                                <td class="item" style="font-weight: bold; width: 20%;">Monto:</td>
                                <td class="item" style="font-weight: bold; width: 20%;">Moneda:</td>
                                <td class="item" style="font-weight: bold; width: 20%;">Tipo de Cambio:</td>
                            </tr>
                            <tr>
                                <td class="item" style="font-weight: bold; width: 20%;">
                                    <telerik:RadDatePicker ID="calFechaPago" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                                </td>
                                <td class="item" style="font-weight: bold; width: 20%;">
                                    <asp:DropDownList ID="ddlEstatus" runat="server" Width="80%" CssClass="box" AutoPostBack="false"></asp:DropDownList>
                                </td>
                                <td class="item" style="width: 20%;">
                                    <telerik:RadNumericTextBox ID="txtMonto" Width="80%" runat="server" NumberFormat-GroupSeparator="," NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                </td>
                                <td class="item" style="width: 20%;">
                                    <asp:Label ID="lblMoneda" CssClass="item" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="item" style="font-weight: bold; width: 20%;">
                                    <telerik:RadNumericTextBox ID="txtTipoCambio" Width="80%" runat="server" NumberFormat-GroupSeparator="," NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="item" style="font-weight: bold; width: 20%;">Método de Pago:</td>
                                <td class="item" style="font-weight: bold; width: 20%;">Banco:&nbsp;
                                    <asp:Panel runat="server" ID="Panel1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBanco" InitialValue="0" CssClass="item" ForeColor="Red" ErrorMessage="*" ValidationGroup="gpEdita" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </asp:Panel>
                                </td>
                                <td class="item" style="font-weight: bold; width: 20%;">Cuenta:</td>
                                <td class="item" style="font-weight: bold; width: 20%;">Cheque:&nbsp;
                                <asp:Panel runat="server" ID="Panel2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNoChequex" CssClass="item" ForeColor="Red" ErrorMessage="*" ValidationGroup="gpEdita" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </asp:Panel>
                                </td>
                                <td class="item" style="font-weight: bold; width: 20%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlMetodoPago" runat="server" AutoPostBack="true" Width="80%"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBanco" runat="server" AutoPostBack="true" Width="80%"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCuenta" runat="server" Width="80%"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoChequex" Width="80%" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="5">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="item" style="font-weight: bold; width: 100%;" colspan="5">Comentario:</td>
                            </tr>
                            <tr>
                                <td class="item" style="font-weight: bold; width: 100%;" colspan="5">
                                    <asp:TextBox ID="txtComentario" TextMode="MultiLine" Height="100px" Width="95%" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 10px;">
                                <td colspan="5">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblMensaje" CssClass="item" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                <td align="right">
                                    <asp:Button ID="btnGuardar" runat="server" ValidationGroup="gpEdita" Text="Guardar" />
                                </td>
                            </tr>
                            <tr style="height: 10px;">
                                <td colspan="5">
                                    <asp:HiddenField ID="DocumentoID" Value="0" runat="server" />
                                    <asp:HiddenField ID="TipoDocumento" Value="" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
