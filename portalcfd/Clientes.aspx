<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_Clientes" CodeBehind="Clientes.aspx.vb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style4 {
            height: 17px;
        }

        .style5 {
            height: 14px;
        }

        .style6 {
            height: 21px;
        }
    </style>
    <script language="javascript" type="text/javascript">
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <asp:Panel ID="pBusqueda" runat="server" DefaultButton="btnSearch">
                <span class="item">Palabra clave:
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="box"></asp:TextBox>&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Buscar" CausesValidation="false" />&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" Text="Ver todo" CausesValidation="false" />
                </span>
                <br />
                <br />
            </asp:Panel>
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblClientsListLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                        <telerik:RadGrid ID="clientslist" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None" AllowSorting="true"
                            PageSize="15" ShowStatusBar="True"
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NextPrevAndNumeric" />
                            <MasterTableView AllowMultiColumnSorting="true" NoMasterRecordsText="No se encontraron registros para mostrar" AllowSorting="true" DataKeyNames="id" Name="Clients" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Razón Social" DataField="razonsocial" SortExpression="razonsocial" UniqueName="razonsocial">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("razonsocial") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="contacto" HeaderText="Contacto" UniqueName="contacto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="telefono_contacto" HeaderText="Teléfono" UniqueName="telefono_contacto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False"
                                        HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
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
                        <asp:Button ID="btnAddClient" runat="server" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
        <asp:Panel ID="panelClientRegistration" runat="server" Visible="False">
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2007" MultiPageID="RadMultiPage1" SelectedIndex="0" CausesValidation="False">
                <Tabs>
                    <telerik:RadTab Text="Datos Generales">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Descuentos por familia">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Descuentos especiales">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Programa de Lealtad">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="100%" Width="100%">
                <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%">
                    <br />
                    <table width="100%">
                        <tr>
                            <td valign="top" colspan="2" style="width: 66%">
                                <asp:Label ID="lblSocialReason" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2" style="width: 66%">
                                <telerik:RadTextBox ID="txtSocialReason" runat="server" Width="92%">
                                </telerik:RadTextBox>
                            </td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSocialReason" CssClass="item" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                            <td width="33%">&nbsp;</td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2" style="width: 66%">
                                <asp:Label ID="lblDenominacionRaznScial" runat="server" CssClass="item" Font-Bold="True" Text="Denominación/Razón Social:" />
                            </td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadTextBox ID="txtDenominacionRaznScial" runat="server" Width="92%"></telerik:RadTextBox>
                            </td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="txtDenominacionRaznScial" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <asp:Label ID="lblContact" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="33%">
                                <asp:Label ID="lblContactEmail" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="33%">
                                <asp:Label ID="lblContactPhone" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <telerik:RadTextBox ID="txtContact" runat="server" Width="85%">
                                </telerik:RadTextBox>
                            </td>
                            <td width="33%">
                                <telerik:RadTextBox ID="txtContactEmail" runat="server" Width="85%">
                                </telerik:RadTextBox>
                            </td>
                            <td width="33%">
                                <telerik:RadTextBox ID="txtContactPhone" runat="server" Width="85%">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">&nbsp;</td>
                            <td width="33%">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ControlToValidate="txtContactEmail" CssClass="item"
                                    ValidationExpression=".*@.*\..*"></asp:RegularExpressionValidator>
                            </td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <asp:Label ID="lblStreet" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="33%" align="left">
                                <asp:Label ID="lblExtNumber" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblIntNumber" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="left" width="33%">
                                <asp:Label ID="lblColony" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <telerik:RadTextBox ID="txtStreet" runat="server" Width="85%">
                                </telerik:RadTextBox>
                            </td>
                            <td width="33%">
                                <telerik:RadTextBox ID="txtExtNumber" runat="server" Width="35%">
                                </telerik:RadTextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <telerik:RadTextBox ID="txtIntNumber" runat="server" Width="35%">
                        </telerik:RadTextBox>
                            </td>
                            <td align="left" width="33%">
                                <telerik:RadTextBox ID="txtColony" runat="server" Width="85%">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="txtStreet" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td width="33%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                    ControlToValidate="txtExtNumber" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" width="33%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="txtColony" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <asp:Label ID="lblCountry" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="33%">
                                <asp:Label ID="lblState" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="33%">
                                <asp:Label ID="lblTownship" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <telerik:RadTextBox ID="txtCountry" runat="server" Width="85%">
                                </telerik:RadTextBox>
                            </td>
                            <td width="33%">
                                <asp:DropDownList ID="estadoid" runat="server" CssClass="item"></asp:DropDownList>
                            </td>
                            <td width="33%">
                                <telerik:RadTextBox ID="txtTownship" runat="server" Width="85%">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                    ControlToValidate="txtCountry" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td width="33%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                    ControlToValidate="estadoid" InitialValue="0" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td width="33%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="txtTownship" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <asp:Label ID="lblZipCode" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="33%">
                                <asp:Label ID="lblRFC" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="33%">
                                <asp:Label ID="lblCondiciones" runat="server" CssClass="item" Font-Bold="true" Text="Condiciones:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <telerik:RadTextBox ID="txtZipCode" runat="server" Width="85%">
                                </telerik:RadTextBox>
                            </td>
                            <td width="33%">
                                <telerik:RadTextBox ID="txtRFC" runat="server" Width="85%">
                                </telerik:RadTextBox>
                            </td>
                            <td width="33%">
                                <asp:DropDownList ID="condicionesid" runat="server" CssClass="box"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="txtZipCode" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td width="33%">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                    ControlToValidate="txtRFC" CssClass="item"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="valRFC" CssClass="item" runat="server" ControlToValidate="txtRFC"
                                    SetFocusOnError="True" ValidationExpression="^([a-zA-Z]{3,4})\d{6}([a-zA-Z\w]{3})$"></asp:RegularExpressionValidator>
                            </td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblContribuyente" runat="server" Text="Tipo de contribuyente:" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFormaPago" runat="server" Text="Forma de Pago:" CssClass="item" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNumCtaPago" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="tipoContribuyenteid" runat="server" AutoPostBack="true" CssClass="box"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="formapagoid" runat="server" CssClass="box"></asp:DropDownList>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtNumCtaPago" runat="server" Width="55%">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <asp:RequiredFieldValidator ID="valTipoContribuyente" runat="server" InitialValue="0" SetFocusOnError="true" ControlToValidate="tipoContribuyenteid" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td width="33%">
                                <asp:RequiredFieldValidator ID="valFormaPago" runat="server" InitialValue="0" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="formapagoid" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2" style="width: 66%">
                                <asp:Label ID="lblRegimen" runat="server" CssClass="item" Font-Bold="true" Text="Régimen fiscal"></asp:Label>
                            </td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2" style="width: 66%">
                                <asp:DropDownList ID="regimenid" CssClass="box" Width="85%" runat="server"></asp:DropDownList>
                            </td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2" style="width: 66%">
                                <asp:RequiredFieldValidator ID="valRegimen" runat="server" InitialValue="0" ControlToValidate="regimenid" CssClass="item" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
                            </td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblUsoCfd" runat="server" Text="Uso de comprobante" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="33%">&nbsp;</td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="cmbUsoCfd" runat="server" CssClass="box" Width="85%"></asp:DropDownList>
                            </td>
                            <td width="33%">&nbsp;</td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="33%">&nbsp;</td>
                            <td width="33%">&nbsp;</td>
                            <td width="33%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="bottom" colspan="3">
                                <asp:Button ID="btnSaveClient" runat="server" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="width: 66%; height: 5px;">
                                <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                                <asp:HiddenField ID="ClientsID" runat="server" Value="0" />
                                <asp:HiddenField ID="DescuentoFamiliaID" runat="server" Value="0" />
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblDescClienteDescuetoFamilia" runat="server" Font-Bold="true" Text="Cliente" CssClass="item"></asp:Label>
                        </legend>
                        <div>
                            <asp:Label ID="lblDescClienteDescuetoFamiliaValue" runat="server" CssClass="item"></asp:Label>
                        </div>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblDescuentosFamilia" runat="server" Font-Bold="true" Text="Descuentos por familia" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="descuentosfamilialist" runat="server" Width="100%" ShowStatusBar="True"
                                        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None" Skin="Office2007">
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <MasterTableView Width="100%" DataKeyNames="id" Name="DescuentosFamilia" AllowMultiColumnSorting="False" NoMasterRecordsText="No se encontraron registros para mostrar">
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Folio" UniqueName="folio">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text='<%# Eval("id") %>' CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" CausesValidation="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="familia" HeaderText="Familia" UniqueName="familia">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="subfamilia" HeaderText="SubFamilia" UniqueName="subfamilia">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="descuento" HeaderText="Descuento" UniqueName="descuento" DataFormatString="{0:p}">
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
                                <td align="right">
                                    <asp:Button ID="btnAgregarDesuentoFamilia" runat="server" Text="Agregar descuento" CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <asp:Panel ID="panelRegistroDescuentoFamilia" runat="server" Visible="False">
                            <table width="100%" cellpadding="3">
                                <tr>
                                    <td width="20%">
                                        <asp:Label ID="lblFamiliaDescuento" runat="server" CssClass="item" Text="Familia:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="lblSubFamiliaDescuento" runat="server" CssClass="item" Text="SubFamilia:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="lblDescuentoFamilia" runat="server" CssClass="item" Text="Descuento:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">&nbsp;</td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        <asp:DropDownList ID="familiaid" AutoPostBack="true" runat="server" CssClass="box"></asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        <asp:DropDownList ID="subfamiliaid" Enabled="false" runat="server" CssClass="box"></asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        <telerik:RadNumericTextBox ID="txtDescuentoFamilia" runat="server" MinValue="0" Type="Percent" Skin="Default" Value="0">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="familiaid" InitialValue="0" Text="Requerido" ForeColor="Red" ValidationGroup="DescuentoFamilia" CssClass="item"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="20%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="subfamiliaid" InitialValue="0" Text="Requerido" ForeColor="Red" ValidationGroup="DescuentoFamilia" CssClass="item"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="20%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDescuentoFamilia" Text="Requerido" ForeColor="Red" ValidationGroup="DescuentoFamilia" CssClass="item"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="20%">&nbsp;</td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="right">
                                        <asp:Button ID="btnCancelarDescuentoFamilia" runat="server" Text="Cancelar" CausesValidation="false" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnGuardarDescuentoFamilia" runat="server" Text="Guardar" ValidationGroup="DescuentoFamilia" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView3" runat="server" Width="100%">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblDescClienteDescuetoEspecial" runat="server" Font-Bold="true" Text="Cliente" CssClass="item"></asp:Label>
                        </legend>
                        <div>
                            <asp:Label ID="lblDescClienteDescuetoEspecialValue" runat="server" CssClass="item"></asp:Label>
                        </div>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="Label1" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <span class="item">Palabra clave:
                            <asp:TextBox ID="txtProducto" runat="server" CssClass="box"></asp:TextBox>&nbsp;
                        <asp:Button ID="btnBuscar" runat="server" CssClass="boton" Text="Buscar" CausesValidation="false" />
                        </span>
                        <br />
                        <br />
                        <table border="0" cellpadding="2" cellspacing="0" align="center" width="100%">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="gridResults" runat="server" Width="100%" ShowStatusBar="True" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None" Skin="Office2007" Visible="false">
                                        <MasterTableView Width="100%" DataKeyNames="productoid, presentacionid" Name="Items" AllowMultiColumnSorting="False">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn>
                                                    <HeaderTemplate>Descripción</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# eval("descripcion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Middle" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn>
                                                    <HeaderTemplate>Descuento</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtDescuentoEspecial" runat="server" MinValue="0" Type="Percent" Width="100px" Skin="Default" Value="0">
                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Middle" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Add">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("productoid") & "," & Eval("presentacionid") %>' CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_add.gif" CausesValidation="False" ToolTip="Agregar descuento" />
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="lblListaDescuentosEspeciales" runat="server" Font-Bold="true" Text="Descuentos especiales" CssClass="item"></asp:Label>
                            </legend>
                            <br />
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="descuentosespecialeslist" runat="server" Width="100%" ShowStatusBar="True"
                                            AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None" Skin="Office2007">
                                            <PagerStyle Mode="NextPrevAndNumeric" />
                                            <MasterTableView Width="100%" DataKeyNames="id" Name="DescuentosEspeciales" AllowMultiColumnSorting="False" NoMasterRecordsText="No se encontraron registros para mostrar">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="producto" HeaderText="Producto" UniqueName="producto">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="presentacion" HeaderText="Presentación" UniqueName="presentacion" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="descuento" HeaderText="Descuento" UniqueName="descuento" DataFormatString="{0:p}">
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
                            </table>
                        </fieldset>
                    </fieldset>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView4" runat="server" Width="100%">
                    <br />
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblaplicavigencia" runat="server" CssClass="item" Text="Aplica Programa de Lealtad: " Visible="true"></asp:Label>
                                <asp:CheckBox runat="server" ID="chbaplicapogramalealtad" />
                            </td>

                            <td width="33%">
                                <asp:Label ID="lblnumeroafiliacion" runat="server" Text="Numero de Afiliacion: " CssClass="item" Font-Bold="True"></asp:Label>
                                <telerik:RadTextBox ID="txtnumeroafiliacion" runat="server" Width="92%">
                                </telerik:RadTextBox>
                            </td>
                            <td width="33%">&nbsp;</td>
                            <td>

                                <asp:Button ID="btnguardarprogramalealtad" runat="server" />&nbsp;
                        
                    
                            </td>


                        </tr>

                    </table>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </asp:Panel>
        <br />
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
