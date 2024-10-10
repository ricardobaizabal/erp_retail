<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="activos.aspx.vb" Inherits="LinkiumCFDI.activos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("grdActivosFijos") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
        function confirmAlert(arg) {
            if (arg) //the user clicked OK
            {
                window.top.location = "activos.aspx";
            }
            else {
                window.top.location = "activos.aspx";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">--%>
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <br />
        <asp:Panel ID="pBusqueda" runat="server" DefaultButton="btnSearch">
            <table>
                <tr>
                    <td>
                        <span class="item">Sucursal:</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbSucursalFiltro" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <span class="item">Palabra clave:</span>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtSearch" runat="server" Width="96%">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar" CausesValidation="false" />&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" Text="Ver todo" CausesValidation="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblActivosListLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td style="height: 5px">
                    <telerik:RadGrid ID="grdActivosFijos" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" GridLines="None"
                        PageSize="15" ShowStatusBar="True"
                        Skin="Office2007" Width="100%">
                        <PagerStyle Mode="NumericPages" />
                        <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Users" Width="100%">
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Código" UniqueName="nombre">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("codigo") %>' CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="marca" HeaderText="Marca" UniqueName="marca">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="modelo" HeaderText="Modelo" UniqueName="modelo">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Anexo" HeaderStyle-HorizontalAlign="Center" UniqueName="Download">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDownload" runat="server" CommandArgument='<%# Eval("anexo") %>' CommandName="cmdDownload" ImageUrl="~/images/download.png" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
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
                    <asp:Button ID="btnAgregaActivo" runat="server" CausesValidation="False" CssClass="item" />
                </td>
            </tr>
            <tr>
                <td style="height: 2px">&nbsp;</td>
            </tr>
        </table>
    </fieldset>
    <br />
    <asp:Panel ID="panelAgregarActivo" runat="server" Visible="false">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblEditLegend" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table width="100%" border="0">
                <tr>
                    <td width="25%">
                        <asp:Label ID="lblCodigo" runat="server" CssClass="item" Font-Bold="true" Text="Código:"></asp:Label>
                    </td>
                    <td width="25%">
                        <asp:Label ID="lblSucursal" runat="server" CssClass="item" Font-Bold="true" Text="Sucursal:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblDescripcion" runat="server" CssClass="item" Text="Descripción:" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="25%">
                        <asp:Label ID="lblCodigoValue" runat="server" CssClass="item"></asp:Label>
                    </td>
                    <td width="25%">
                        <asp:DropDownList ID="cmbSucursal" runat="server"></asp:DropDownList>
                    </td>
                    <td colspan="2">
                        <telerik:RadTextBox ID="txtDescripcion" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="25%">&nbsp;</td>
                    <td width="25%">
                        <asp:RequiredFieldValidator ID="valSucursal" runat="server" ControlToValidate="cmbSucursal" InitialValue="0" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td colspan="2">
                        <asp:RequiredFieldValidator ID="valDescripcion" runat="server" ControlToValidate="txtDescripcion" ForeColor="Red" SetFocusOnError="true" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="25%">
                        <asp:Label ID="lblMarca" runat="server" CssClass="item" Text="Marca:" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="25%">
                        <asp:Label ID="lblModelo" runat="server" CssClass="item" Text="Modelo:" Font-Bold="True"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblFoto" runat="server" CssClass="item" Font-Bold="true" Text="Foto:"></asp:Label>&nbsp;
                            <%--<asp:ImageButton ID="imgBtnEliminarFoto" runat="server" CausesValidation="false" ToolTip="Eliminar foto" ImageUrl="~/images/action_delete.gif" Visible="false" />--%>
                    </td>
                </tr>
                <tr>
                    <td width="25%">
                        <telerik:RadTextBox ID="txtMarca" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td width="25%">
                        <telerik:RadTextBox ID="txtModelo" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td colspan="2">
                        <asp:FileUpload ID="foto" runat="server" /><br />
                    </td>
                </tr>
                <tr>
                    <td width="25%">&nbsp;</td>
                    <td width="25%">&nbsp;</td>
                    <td colspan="2" rowspan="12" valign="top">
                        <asp:Image ID="imgFoto" runat="server" Visible="false" Width="200px" />
                        <asp:HiddenField ID="hdnFoto" runat="server" Value="" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblUso" runat="server" CssClass="item" Text="Uso dentro de la empresa:" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadTextBox ID="txtUso" TextMode="MultiLine" Rows="4" runat="server" Width="95%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="25%">&nbsp;</td>
                    <td width="25%">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblAnexo" runat="server" CssClass="item" Text="Anexo:" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:FileUpload ID="anexo" Width="90%" runat="server" />
                        <asp:HiddenField ID="hdnAnexo" runat="server" Value="" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblCosto" runat="server" CssClass="item" Text="Costo de reposición:" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadNumericTextBox ID="txtCostoReposicion" runat="server" Type="Currency" MinValue="0" Skin="Default" Value="0">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="item" />&nbsp;
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="item" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
                            <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                        <asp:HiddenField ID="ActivoID" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <%--</telerik:RadAjaxPanel>--%>
    <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Office2007" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
</asp:Content>
