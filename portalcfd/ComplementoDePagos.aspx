﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="ComplementoDePagos.aspx.vb" Inherits="LinkiumCFDI.ComplementoDePagos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .titulos {
            font-family: verdana;
            font-size: medium;
            color: Purple;
        }
    </style>
    <script type="text/javascript">
        checked = false;
        function checkedAll(frm1) {
            var aa = frm1;
            if (checked == false) {
                checked = true
            }
            else {
                checked = false
            }
            for (var i = 0; i < aa.elements.length; i++) {
                aa.elements[i].checked = checked;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panelClients" runat="server">
        <fieldset style="padding: 10px;">
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="imgPanel1" runat="server" ImageUrl="~/portalcfd/images/comprobant.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientsSelectionLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table border="0" style="width: 100%;">
                <tr>
                    <td class="item" style="width: 50%;" colspan="2">
                        <asp:Label ID="lblCliente" runat="server" CssClass="item" Font-Bold="true" Text="Seleccione el cliente:"></asp:Label>
                    </td>
                    <td class="item" style="width: 50%;" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="width: 50%;" colspan="2">
                        <asp:DropDownList ID="cmbCliente" runat="server" CausesValidation="false" CssClass="box" AutoPostBack="true" Width="95%"></asp:DropDownList>
                    </td>
                    <td class="item" style="width: 50%;" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="width: 50%;" colspan="2">
                        <asp:RequiredFieldValidator ID="valCliente" runat="server" InitialValue="0" ErrorMessage="Seleccione el cliente" ControlToValidate="cmbCliente" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="item" style="width: 50%;" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="width: 25%;">
                        <asp:Label ID="lblFormaPago" runat="server" CssClass="item" Font-Bold="true" Text="Forma de pago:"></asp:Label>
                    </td>
                    <td class="item" style="width: 25%;">
                        <asp:Label ID="lblFechaPago" runat="server" CssClass="item" Font-Bold="true" Text="Fecha de Pago:"></asp:Label>
                    </td>
                    <td class="item" style="width: 25%;">
                        <asp:Label ID="lblMoneda" runat="server" CssClass="item" Font-Bold="true" Text="Moneda:"></asp:Label>
                    </td>
                    <td class="item" style="width: 25%;">
                        <asp:Label ID="lblTipoCambio" runat="server" CssClass="item" Font-Bold="true" Text="Tipo Cambio:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="item" style="width: 25%;">
                        <asp:DropDownList ID="cmbFormaPago" runat="server" CssClass="box" AutoPostBack="true" Width="95%"></asp:DropDownList>
                    </td>
                    <td class="item" style="width: 25%;">
                        <telerik:RadDatePicker ID="fecha" runat="server"></telerik:RadDatePicker>
                        <telerik:RadDateTimePicker ID="calFecha" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy hh:mm:ss" runat="server" Visible="false">
                            <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                            </Calendar>
                            <TimeView ID="TimeView1" Interval="00:15:00" StartTime="07:00" EndTime="23:00" runat="server">
                            </TimeView>
                        </telerik:RadDateTimePicker>
                    </td>
                    <td class="item" style="width: 25%;">
                        <asp:DropDownList ID="cmbMoneda" runat="server" Width="50%" AutoPostBack="true" CssClass="box"></asp:DropDownList>
                    </td>
                    <td class="item" style="width: 25%;">
                        <telerik:RadNumericTextBox ID="txtTipoCambio" runat="server" Width="50%" Type="Currency" NumberFormat-DecimalDigits="4" Value="0"></telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="item">
                        <asp:RequiredFieldValidator ID="valFormaPago" runat="server" ErrorMessage="Requerido" ControlToValidate="cmbFormaPago" CssClass="item" ForeColor="Red" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td class="item" style="width: 25%;">&nbsp;</td>
                    <td class="item" style="width: 25%;">
                        <asp:RequiredFieldValidator ID="valMoneda" runat="server" InitialValue="0" ErrorMessage="Requerido" CssClass="item" ForeColor="Red" ControlToValidate="cmbMoneda" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td class="item" style="width: 25%;">
                        <asp:RequiredFieldValidator ID="valTipoCambio" runat="server" Enabled="false" ControlToValidate="txtTipoCambio" InitialValue="0" CssClass="item" ForeColor="Red" ErrorMessage="Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblObservaciones" runat="server" CssClass="item" Font-Bold="true" Text="Comentarios:"></asp:Label>
                    </td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadTextBox ID="txtObservaciones" runat="server" Width="100%" CssClass="item" TextMode="MultiLine" Height="40px"></telerik:RadTextBox>
                    </td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <br />
    <asp:Panel ID="panelRecepcionPago" runat="server" Visible="false">
        <fieldset style="padding: 10px;">
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image4" runat="server" ImageUrl="~/portalcfd/images/datClient.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="Label5" runat="server" Font-Bold="true" CssClass="item">Recepcion de Pagos - Información Cliente-Proveedor</asp:Label>
            </legend>
            <br />
            <table width="100%" border="0">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="LblEmisorCtabeneficiario" runat="server" CssClass="item" Font-Bold="true">RFC Banco Beneficiario:</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" CssClass="item" Font-Bold="True">Cuenta Beneficiario:</asp:Label>
                    </td>
                    <td style="width: 43%;">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadTextBox ID="txtRfcBeneficiario" runat="server" Width="95%" Enabled="false">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbCtaBeneficiario" runat="server" CssClass="box" Width="95%" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td style="width: 43%;">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width: 43%;"></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label2" runat="server" CssClass="item" Font-Bold="True">RFC Banco Ordenante:</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" CssClass="item" Font-Bold="True">Cuenta Ordenante:</asp:Label>
                    </td>
                    <td style="width: 43%;">
                        <asp:Label ID="Label8" runat="server" CssClass="item" Font-Bold="True" Text="Banco Ordenante Extranjero:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadTextBox ID="txtRFCCtaOrdenante" runat="server" Width="95%" Enabled="false">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbCtaOrdenante" runat="server" CssClass="box" Width="95%" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td style="width: 43%;">
                        <telerik:RadTextBox ID="txtBancoExtr" runat="server" Width="80%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width: 43%;">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label9" runat="server" CssClass="item" Font-Bold="True" Visible="false">Número de Operación:</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" CssClass="item" Font-Bold="True" Visible="false">Tipo de Pago:</asp:Label>
                    </td>
                    <td style="width: 43%;">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadTextBox ID="txtNumOperacion" runat="server" Width="95%" Visible="false">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbTipoPago" runat="server" CssClass="box" AutoPostBack="true" Width="95%" Visible="false"></asp:DropDownList>
                    </td>
                    <td style="width: 43%;">
                        <asp:HiddenField ID="serie" runat="server" Value=""></asp:HiddenField>
                        <asp:HiddenField ID="folio" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <br />
    <asp:Panel ID="panelSPEI" runat="server" Visible="false">
        <fieldset style="padding: 10px;">
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/portalcfd/images/datClient.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="item">SPEI-Digital</asp:Label>
            </legend>

            <br />

            <table width="100%" border="0">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label11" runat="server" CssClass="item" Font-Bold="True">Certificado de pago: </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtCertPago" runat="server" Width="602px" CssClass="box" TextMode="MultiLine" MaxLength="400" Height="47px"></asp:TextBox>
                    </td>
                    <td style="width: 43%;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Requerido" CssClass="item" ForeColor="Red" ControlToValidate="txtCertPago" SetFocusOnError="true" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" CssClass="item" Font-Bold="True">Cadena Original del comprobante de pago:</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtCadPago" runat="server" Width="602px" CssClass="box" TextMode="MultiLine" MaxLength="400" Height="47px"></asp:TextBox>
                    </td>
                    <td style="width: 43%;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Requerido" CssClass="item" ForeColor="Red" ControlToValidate="txtCadPago" SetFocusOnError="true" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 43%;">
                        <asp:Label ID="Label13" runat="server" CssClass="item" Font-Bold="True">Sello del pago:</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtSelloPago" runat="server" Width="602px" CssClass="box" TextMode="MultiLine" MaxLength="400" Height="47px"></asp:TextBox>
                    </td>
                    <td style="width: 43%;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ErrorMessage="Requerido" CssClass="item" ForeColor="Red" ControlToValidate="txtSelloPago" SetFocusOnError="true" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="panelItemsRegistration" runat="server" Visible="False">
        <fieldset style="padding: 10px;">
            <asp:HiddenField ID="productoid" runat="server" />
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/portalcfd/images/concept.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientItems" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table width="95%" cellspacing="0" cellpadding="1" border="0" align="center">
                <tr>
                    <td class="item" colspan="5">&nbsp;&nbsp;<asp:CheckBox ID="chkAll" runat="server" Visible="false" AutoPostBack="true" Text="Seleccionar todo" />
                        <br />
                        <br />
                        <telerik:RadGrid ID="itemsList" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                            Skin="Simple" Visible="False">
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Items" AllowMultiColumnSorting="False">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" ItemStyle-Width="20">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkcfdid" runat="server" CssClass="item" Checked='<%# IIf(Eval("chkcfdid") Is DBNull.Value, "False", Eval("chkcfdid"))%>' AutoPostBack="True" OnCheckedChanged="ToggleRowSelection" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Fecha CFDI</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblfechaCFDI" runat="server" Text='<%# eval("fecha") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>serie</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerie" runat="server" Text='<%# eval("serie") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>folio</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFolio" runat="server" Text='<%# eval("folio") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>UUID</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUUID" runat="server" Text='<%# eval("uuid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Factura Emitida</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonedaDR" runat="server" Text='<%#Eval("monedaDR") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Total CFDI</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalCFDI" runat="server" Text='<%# eval("total") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbltotalCFDIValue" runat="server" Text='<%# String.Format("{0:C}", eval("total"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Saldo Anterior</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaldoAnterior" runat="server" Text='<%# eval("saldoanterior") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblSaldoAnteriorValue" runat="server" Text='<%# String.Format("{0:C}", eval("saldoanterior"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Saldo Pendiente</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaldo" runat="server" Text='<%# eval("saldo") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblSaldoValue" runat="server" Text='<%# String.Format("{0:C}", eval("saldo"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Monto</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox Text='<%# eval("monto") %>' ID="txtMonto" OnTextChanged="txtMonto_TextChanged" AutoPostBack="true" runat="server" Skin="Default" Width="80px" MinValue="0" Value='0'>
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Importe Divisa</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox Text='<%# eval("montomxn") %>' ID="txtImporteDivisa" OnTextChanged="txtMonto_TextChanged" AutoPostBack="true" runat="server" Skin="Default" Width="80px" MinValue="0" Value='0'>
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>

                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
            </table>

        </fieldset>
    </asp:Panel>
    <br />
    <asp:Panel ID="panelResume" runat="server" Visible="False">
        <fieldset style="padding: 10px;">
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image3" runat="server" ImageUrl="~/portalcfd/images/resumen.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblResume" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>

            <br />

            <table width="100%" align="left">
                <tr>
                    <td width="16%" align="left" style="width: 32%">
                        <asp:Label ID="lblSubTotal" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        &nbsp;<asp:Label ID="lblSubTotalValue" runat="server" CssClass="item"
                            Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="16%" align="left" style="width: 32%">
                        <asp:Label ID="lblTotal" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        &nbsp;<asp:Label ID="lblTotalValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td align="left" width="16%">
                        <br />
                        <br />
                        <asp:Button ID="btnCreateInvoice" runat="server" CausesValidation="true" CssClass="item" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancelInvoice" runat="server" CausesValidation="False" CssClass="item" />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" CenterIfModal="true" AutoSize="False" Behaviors="None" VisibleOnPageLoad="False" Width="740" Height="600">
        <ContentTemplate>
            <br />
            <table align="center" width="95%">
                <tr>
                    <td>
                        <asp:TextBox ID="txtErrores" TextMode="MultiLine" Width="100%" Rows="32" ReadOnly="true" CssClass="item" runat="server"></asp:TextBox>
                    </td>
                    <tr>
                        <td align="left" width="16%">
                            <br />
                            <br />
                            <asp:Button ID="btnAceptar" runat="server" CausesValidation="true" CssClass="item" Text="Aceptar" />&nbsp;&nbsp;
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