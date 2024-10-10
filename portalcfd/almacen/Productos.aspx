<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.almacen_portalcfd_Productos" MaintainScrollPositionOnPostback="true" CodeBehind="Productos.aspx.vb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        textarea {
            resize: none;
        }

        .foto {
            max-height: 130px;
        }

        .RadComboBox_Office2007 .rcbInputCell .rcbInput, .RadComboBoxDropDown_Office2007 {
            font: normal 12px "Segoe UI",Arial,Helvetica,sans-serif;
        }

        .rwTitle {
            font: normal 14px "Segoe UI",Arial,Helvetica,sans-serif;
            font-family: Tahoma !important;
        }

        .rwDialogMessage {
            font: normal 14px "Segoe UI",Arial,Helvetica,sans-serif;
            font-family: Tahoma !important;
        }

        .rwOkBtn {
            font: normal 14px "Segoe UI",Arial,Helvetica,sans-serif;
            font-family: Tahoma !important;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("productslist") > -1) || (arguments.get_eventTarget().indexOf("btnGuardarFoto") > -1)) {
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" SkinID="Office2007" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <asp:Panel runat="server" DefaultButton="btnSearch">
                <table width="100%">
                    <tr>
                        <td class="item" style="width: 12%;">Familia:</td>
                        <td class="item" style="width: 40%;">
                            <asp:DropDownList ID="filtrofamiliaid" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="item" style="width: 12%;">Sub-Familia:</td>
                        <td class="item" style="width: 40%;">
                            <asp:DropDownList ID="filtrosubfamiliaid" AutoPostBack="true" Enabled="false" runat="server"></asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="item" style="width: 12%;">Proveedor:</td>
                        <td class="item" style="width: 40%;">
                            <asp:DropDownList ID="filtroproveedorid" runat="server" Width="100%"></asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="item" style="width: 12%;">Palabra clave:</td>
                        <td class="item" style="width: 40%;">
                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar" CausesValidation="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnAll" runat="server" Text="Ver todo" CausesValidation="false" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <br />
        </fieldset>
        <br />
        <asp:Panel ID="panelProductList" runat="server">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListadoProductos_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProductsListLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td style="height: 5px"></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="productslist" runat="server" Width="100%" ShowStatusBar="True"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None" ExportSettings-ExportOnlyData="false"
                                Skin="Office2007" OnNeedDataSource="productslist_NeedDataSource">
                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <ExportSettings IgnorePaging="True" FileName="CatalogoProductos">
                                    <Excel Format="Biff" />
                                </ExportSettings>
                                <MasterTableView Width="100%" DataKeyNames="id" Name="Products" AllowMultiColumnSorting="False" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Foto" ItemStyle-HorizontalAlign="Center" UniqueName="Foto">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkViewPhoto" runat="server" Text='Ver' CommandArgument='<%# Eval("foto") %>' CommandName="cmdViewPhoto" CausesValidation="false"></asp:LinkButton>--%>
                                                <asp:Image ID="imgProductPhoto" runat="server" CssClass="foto" ImageUrl='<%# String.Format("~/images/productos/{0}", Eval("foto")) %>' Width="120px" ImageAlign="AbsMiddle" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="" UniqueName="codigo">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" Text='<%# Eval("codigo") %>' CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" CausesValidation="false"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="codigo_barras" HeaderText="Codigo de Barras" UniqueName="codigo_barras">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="claveunidad" HeaderText="" UniqueName="claveunidad">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="claveprodserv" HeaderText="Clave SAT" UniqueName="claveprodserv">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="descripcion" HeaderText="" UniqueName="descripcion">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="costo_estandar" HeaderText="Costo" UniqueName="costo_estandar" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="unitario" HeaderText="Precio" UniqueName="unitario" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="tasa" HeaderText="Tasa" UniqueName="tasa">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ieps" HeaderText="IEPS" UniqueName="ieps">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia">
                                            <ItemStyle HorizontalAlign="Right" />
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
                            <asp:Button ID="btnAddProduct" runat="server" CausesValidation="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 2px">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="panelProductRegistration" runat="server" Visible="False">
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2007" MultiPageID="RadMultiPage1" SelectedIndex="0" CausesValidation="False">
                <Tabs>
                    <telerik:RadTab Text="Datos Generales">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Combinaciones">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Existencias">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Presentaciones">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Precios">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Horario">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Fotos">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Punto de reorden">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="100%" Width="100%">
                <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%">
                    <br />
                    <table border="0" cellpadding="3" width="100%">
                        <tr>
                            <td width="20%">
                                <asp:Label ID="lblCode" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:Label ID="lblCodigoBarras" runat="server" CssClass="item" Font-Bold="True" Text="Código de Barras:"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:Label ID="lblUnit" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:Label ID="lblCostoStd" runat="server" CssClass="item" Font-Bold="true" Text="Costo estándar:"></asp:Label><br />
                            </td>
                            <td width="20%">
                                <asp:Label ID="lblUnitaryPrice" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <telerik:RadTextBox ID="txtCode" runat="server" Width="85%">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20%">
                                <telerik:RadTextBox ID="txtCodigoBarras" runat="server" Width="85%">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="unidadmedidaid" runat="server" CssClass="box"></asp:DropDownList>
                            </td>
                            <td width="20%">
                                <telerik:RadNumericTextBox ID="txtCostoStd" runat="server" Type="Currency" MinValue="0" Skin="Default" Value="0">
                                    <NumberFormat DecimalDigits="4" GroupSeparator="," />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="20%">
                                <telerik:RadNumericTextBox ID="txtUnitaryPrice" runat="server" Type="Currency" MinValue="0" Skin="Default" Value="0" Width="85%">
                                    <NumberFormat DecimalDigits="4" GroupSeparator="," />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:RequiredFieldValidator ID="valCode" SetFocusOnError="true" runat="server" ControlToValidate="txtCode" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">
                                <asp:RequiredFieldValidator ID="valUnidad" SetFocusOnError="true" runat="server" ControlToValidate="unidadmedidaid" InitialValue="0" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td width="20%">
                                <asp:RequiredFieldValidator ID="valCostoStd" SetFocusOnError="true" runat="server" ControlToValidate="txtCostoStd" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td width="20%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:Label ID="lblDescription" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <telerik:RadTextBox ID="txtDescription" runat="server" Width="95%" Height="80px" TextMode="MultiLine" MaxLength="400">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:RequiredFieldValidator ID="valDescripcion" SetFocusOnError="true" runat="server" ControlToValidate="txtDescription" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblClaveProdServ" runat="server" CssClass="item" Font-Bold="true" Text="Clave producto / servicio:"></asp:Label><br />
                            </td>
                            <td width="20%">
                                <asp:Label ID="lblFamilia" runat="server" CssClass="item" Font-Bold="true" Text="Familia:"></asp:Label><br />
                            </td>
                            <td width="20%">
                                <asp:Label ID="lblSubFamilia" runat="server" CssClass="item" Font-Bold="true" Text="Sub-Familia:"></asp:Label><br />
                            </td>
                            <td width="20%">
                                <asp:Label ID="lblNivelEscolar" runat="server" CssClass="item" Font-Bold="true" Text="Nivel escolar:"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:DropDownList ID="claveprodservid" runat="server" Enabled="true" Width="90%" CssClass="box"></asp:DropDownList>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="familiaid" runat="server" AutoPostBack="true" Width="90%" CssClass="box"></asp:DropDownList>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="subfamiliaid" runat="server" Enabled="false" Width="50%" CssClass="box"></asp:DropDownList>
                            </td>
                            <td width="20%">
                                <%--<asp:DropDownList ID="cmbNivelEscolar" runat="server" CssClass="box" Width="100%"></asp:DropDownList>--%>
                                <telerik:RadComboBox RenderMode="Lightweight" CssClass="item" ID="cmbNivelEscolar" runat="server" CheckBoxes="true" Width="95%" Localization-CheckAllString="Seleccionar todo" Localization-ItemsCheckedString="niveles escolares seleccionados" Localization-AllItemsCheckedString="Todos seleccionados" EnableCheckAllItemsCheckBox="true" EmptyMessage="--Seleccione--">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RequiredFieldValidator ID="valClaveProductoServicio" SetFocusOnError="true" runat="server" InitialValue="0" ControlToValidate="claveprodservid" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">
                                <asp:RequiredFieldValidator ID="valNivelEscolar" runat="server" ForeColor="Red" ControlToValidate="cmbNivelEscolar" InitialValue="0" CssClass="item" Text="Requerido"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="lblTasa" runat="server" CssClass="item" Font-Bold="true" Text="Tasa:"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:Label ID="lblIeps" runat="server" CssClass="item" Font-Bold="true" Text="IEPS:"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:Label ID="lblReorden" runat="server" CssClass="item" Font-Bold="true" Text="Punto reorden:" Visible="false"></asp:Label>
                            </td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:DropDownList ID="tasaid" runat="server" CssClass="box"></asp:DropDownList>
                            </td>
                            <td width="20%">
                                <telerik:RadNumericTextBox ID="txtIeps" runat="server" CssClass="box" Culture="es-MX" LabelWidth="64px" Type="Percent" Width="60px" MaxValue="100" MinValue="0" ReadOnly="False" NumberFormat-GroupSeparator='""'>
                                    <NumberFormat ZeroPattern="n %"></NumberFormat>
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="20%">
                                <telerik:RadNumericTextBox ID="txtReorden" runat="server" MinValue="0" Skin="Default" Value="0" Visible="false">
                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="2">
                                <asp:CheckBox ID="chkPredeterminadoProducto" CssClass="item" Text="Producto predeterminado para inventario" Font-Bold="true" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:RequiredFieldValidator ID="valTasa" runat="server" ControlToValidate="tasaid" CssClass="item" SetFocusOnError="True" ErrorMessage="Requerido" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="lblMoneda" runat="server" CssClass="item" Font-Bold="true" Text="Moneda:"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:Label ID="lblTipoCambioStd" runat="server" CssClass="item" Font-Bold="true" Text="Tipo de cambio:"></asp:Label>
                            </td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">
                                <asp:Label ID="lblObjetoImpuesto" runat="server" CssClass="item" Font-Bold="True" Text="Objeto de impuesto:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:DropDownList ID="monedaid" runat="server" CssClass="box"></asp:DropDownList>
                            </td>
                            <td width="20%">
                                <telerik:RadNumericTextBox ID="txtTipoCambio" runat="server" Type="Currency" MinValue="0" Skin="Default" Value="0">
                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="20%">
                                <asp:CheckBox ID="chkInventariableBit" Font-Bold="true" runat="server" Text="Producto inventariable" CssClass="item" />
                            </td>
                            <td width="20%">
                                <asp:CheckBox ID="chkBasculaBit" Font-Bold="true" runat="server" Text="Báscula" CssClass="item" />
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="cbmObjetoImpuesto" runat="server" CssClass="box" Width="85%"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <asp:RequiredFieldValidator ID="valMoneda" runat="server" ControlToValidate="monedaid" CssClass="item" SetFocusOnError="True" ErrorMessage="Requerido" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">
                                <asp:RequiredFieldValidator ID="valObjetoImpuesto" runat="server" ForeColor="Red" SetFocusOnError="true" Text="Requerido" InitialValue="0" ControlToValidate="cbmObjetoImpuesto" CssClass="item"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblProveedor" runat="server" CssClass="item" Font-Bold="true" Text="Proveedor:"></asp:Label>
                            </td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <telerik:RadComboBox RenderMode="Lightweight" CssClass="item" ID="cmbProveedor" runat="server" CheckBoxes="true" Width="95%" Localization-CheckAllString="Seleccionar todo" Localization-ItemsCheckedString="proveedores seleccionados" Localization-AllItemsCheckedString="Todos seleccionados" EnableCheckAllItemsCheckBox="true" EmptyMessage="--Seleccione--">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:RequiredFieldValidator ID="valProveedor" runat="server" ControlToValidate="cmbProveedor" CssClass="item" SetFocusOnError="True" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
                            </td>
                            <td width="20%">&nbsp;</td>
                            <td width="20%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <br />
                                <asp:Button ID="btnSaveProduct" runat="server" CssClass="item" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                                <asp:HiddenField ID="ProductID" runat="server" Value="0" />
                                <asp:HiddenField ID="PresentacionID" runat="server" Value="0" />
                                <asp:HiddenField ID="MargenID" runat="server" Value="0" />
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblConbinacionesTitle" runat="server" Font-Bold="true" Text="Administrar las combinaciones de sus productos" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="combinacionesList" runat="server" Width="100%" ShowStatusBar="True"
                                        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None" Skin="Office2007">
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <MasterTableView Width="100%" DataKeyNames="id" Name="Combinaciones" AllowMultiColumnSorting="False" NoMasterRecordsText="No se encontraron registros para mostrar">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="codigoproducto" HeaderText="Código Producto" UniqueName="codigoproducto">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="codigocombinacion" HeaderText="Código Combinacion" UniqueName="codigocombinacion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="descripcion" HeaderText="Combinación" UniqueName="descripcion">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>Costo Unit.</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtCUnitario" runat="server" Type="Currency" EnabledStyle-HorizontalAlign="Right" Text='<%# eval("costo_estandar") %>' MinValue="0" Skin="Default" Width="80px">
                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>Precio Unit.</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtUnitario" runat="server" Type="Currency" EnabledStyle-HorizontalAlign="Right" Text='<%# eval("unitario") %>' MinValue="0" Skin="Default" Width="80px">
                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>Orden</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtOrden" runat="server" Type="Number" EnabledStyle-HorizontalAlign="Right" Text='<%#Eval("orden") %>' MinValue="0" Skin="Default" Width="80px">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="," />
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
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
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td align="right">
                                    <asp:Button ID="btnActualizarPrecios" runat="server" Text="Actualizar precios y orden" CausesValidation="False" />
                                    <asp:Button ID="btnAgregaCombinacion" runat="server" Text="Agregar Combinacion" CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <br />
                        <asp:Panel ID="panelRegistroCombinacion" runat="server" Visible="False">
                            <table border="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 25%;">
                                        <asp:Label ID="lblCodigoBarrasCombinacion" runat="server" CssClass="item" Font-Bold="true" Text="Código de Barras:"></asp:Label>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:Label ID="lblAtributo" runat="server" CssClass="item" Font-Bold="true" Text="Atributo:"></asp:Label>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:Label ID="lblValor" runat="server" CssClass="item" Font-Bold="true" Text="Valor:"></asp:Label>
                                    </td>
                                    <td style="width: 25%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        <telerik:RadTextBox ID="txtCodigoBarrasCombinacion" runat="server">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:DropDownList ID="atributoid" runat="server" AutoPostBack="true" Width="90%" CssClass="box"></asp:DropDownList>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:DropDownList ID="valorid" runat="server" Enabled="false" Width="90%" CssClass="box"></asp:DropDownList>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:Button ID="btnAgregarValorAtributo" runat="server" ValidationGroup="vgCombinaciones" Text="Agregar valor de atributo" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">&nbsp;</td>
                                    <td style="width: 25%;">
                                        <asp:RequiredFieldValidator ID="valAtributo" runat="server" ValidationGroup="vgCombinaciones" ControlToValidate="atributoid" CssClass="item" SetFocusOnError="True" ErrorMessage="Requerido" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:RequiredFieldValidator ID="valValorAtributo" runat="server" ValidationGroup="vgCombinaciones" ControlToValidate="valorid" CssClass="item" SetFocusOnError="True" ErrorMessage="Requerido" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 25%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtCombinacion" TextMode="MultiLine" CssClass="box" Enabled="false" Height="100px" Width="191px" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnGuardarCombinacion" runat="server" Text="Guardar valor" CausesValidation="false" Enabled="false" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCancelarCombinacion" runat="server" Text="Cancelar" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <br />
                        </asp:Panel>
                    </fieldset>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView3" runat="server" Width="100%">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Existencias combinaciones de producto" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table style="width: 100%;">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSucursal" runat="server" Font-Bold="true" Text="Sucursal:" CssClass="item"></asp:Label>&nbsp;&nbsp;
                                    <asp:DropDownList ID="cmbSucursal" runat="server" Width="150px"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnConsultarExistencia" runat="server" Text="Consultar" CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="ExistenciasCombinacionesList" runat="server" Width="100%" ShowStatusBar="True"
                                        AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None" ShowFooter="true"
                                        Skin="Office2007">
                                        <PagerStyle Mode="NumericPages"></PagerStyle>
                                        <MasterTableView Width="100%" DataKeyNames="combinacionid,productoid,sucursalid" Name="Combinaciones" NoMasterRecordsText="No hay registros que mostrar." AllowMultiColumnSorting="False">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="producto" HeaderText="Producto" UniqueName="producto">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="combinacion" HeaderText="Combinacion" UniqueName="combinacion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia" ItemStyle-HorizontalAlign="Right">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView4" runat="server" Width="100%">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblDescProductoPresentaciones" runat="server" Font-Bold="true" Text="Producto" CssClass="item"></asp:Label>
                        </legend>
                        <div>
                            <asp:Label ID="lblDescProductoPresentacionesValue" runat="server" CssClass="item"></asp:Label>
                        </div>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblPresentaciones" runat="server" Font-Bold="true" Text="Presentaciones" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="presentacioneslist" runat="server" Width="100%" ShowStatusBar="True"
                                        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None" Skin="Office2007">
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <MasterTableView Width="100%" DataKeyNames="id" Name="Products" AllowMultiColumnSorting="False" NoMasterRecordsText="No se encontraron registros para mostrar">
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Código" UniqueName="codigo">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text='<%# Eval("codigo") %>' CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" CausesValidation="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="codigo_barras" HeaderText="Código de barras" UniqueName="codigo_barras">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="unidad" HeaderText="Unidad de Medida" UniqueName="unidad">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="factor" HeaderText="Factor" UniqueName="factor" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="costo_estandar" HeaderText="Costo" UniqueName="costo_estandar" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="precio" HeaderText="Precio" UniqueName="precio" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="precio_fuera_horario" HeaderText="Precio Fuera Horario" ItemStyle-HorizontalAlign="Right" UniqueName="precio_fuera_horario" DataFormatString="{0:C}">
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
                                    <asp:Button ID="btnAgregarPresentacion" runat="server" Text="Agregar presentación" CssClass="item" CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <asp:Panel ID="panelRegistroPresentacion" runat="server" Visible="False">
                            <table border="0" width="100%" cellpadding="3">
                                <tr>
                                    <td width="20%">
                                        <asp:Label ID="lblCodigoPresentacion" runat="server" CssClass="item" Text="Código:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="lblCodigoBarrasPresentacion" runat="server" CssClass="item" Text="Código de Barras:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="lblUnidadPresentacion" runat="server" CssClass="item" Text="Unidad:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="lblFactorPresentacion" runat="server" CssClass="item" Text="Factor:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        <telerik:RadTextBox ID="txtCodigoPresentacion" runat="server" Width="90%"></telerik:RadTextBox>
                                    </td>
                                    <td width="20%">
                                        <telerik:RadTextBox ID="txtCodigoBarrasPresentacion" runat="server" Width="90%"></telerik:RadTextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:DropDownList ID="unidadid" runat="server" Width="80%" CssClass="box"></asp:DropDownList>
                                    </td>
                                    <td width="20%">
                                        <telerik:RadNumericTextBox ID="txtFactorPresentacion" runat="server" MinValue="0" Width="80%" Skin="Default" Value="0">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCodigoPresentacion" Text="Requerido" ForeColor="Red" ValidationGroup="Presentaciones" CssClass="item"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="20%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtCodigoBarrasPresentacion" Text="Requerido" ForeColor="Red" ValidationGroup="Presentaciones" CssClass="item"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="20%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="unidadid" InitialValue="0" Text="Requerido" ForeColor="Red" ValidationGroup="Presentaciones" CssClass="item"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="20%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFactorPresentacion" Text="Requerido" ForeColor="Red" ValidationGroup="Presentaciones" CssClass="item"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        <asp:Label ID="lblPrecioPresentacion" runat="server" CssClass="item" Text="Precio:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="lblPrecioPresentacionFueraHorario" runat="server" CssClass="item" Text="Precio Fuera Horario:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="lblCostoPresentacion" runat="server" CssClass="item" Visible="false" Text="Costo estándar:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">&nbsp;</td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        <telerik:RadNumericTextBox ID="txtPrecioPresentacion" runat="server" Type="Currency" MinValue="0" Width="80%" Skin="Default" Value="0">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td width="20%">
                                        <telerik:RadNumericTextBox ID="txtPrecioPresentacionFueraHorario" runat="server" Type="Currency" MinValue="0" Width="80%" Skin="Default" Value="0">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td width="20%">
                                        <telerik:RadNumericTextBox ID="txtCostoPresentacion" runat="server" Type="Currency" Visible="false" MinValue="0" Width="80%" Skin="Default" Value="0">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        <asp:Label ID="lblDescripcionPresentacion" runat="server" Text="Descripción:" CssClass="item" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkPredeterminadoPresentacionAlmacen" CssClass="item" Text="Presentación predeterminada para inventario" Font-Bold="true" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkPredeterminadoPresentacion" CssClass="item" Visible="false" Text="Presentación predeterminada para inventario" Font-Bold="true" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <telerik:RadTextBox ID="txtDescripcionPresentacion" runat="server" Width="100%" Height="80px" TextMode="MultiLine" MaxLength="400">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lblMensajePresentacion" runat="server" Font-Bold="true" CssClass="item" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnCancelarPresentacion" runat="server" Text="Cancelar" CausesValidation="false" CssClass="item" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnGuardarPresentacion" runat="server" Text="Guardar" ValidationGroup="Presentaciones" CssClass="item" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView5" runat="server" Width="100%">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblDescProductoMargenes" runat="server" Font-Bold="true" Text="Producto" CssClass="item"></asp:Label>
                        </legend>
                        <div>
                            <asp:Label ID="lblDescProductoMargenesValue" runat="server" CssClass="item"></asp:Label>
                        </div>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblMargenUtilidad" runat="server" Font-Bold="true" Text="Configuracion de Precios" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="grdMargenUtilidad" runat="server" Width="100%" ShowStatusBar="True"
                                        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None" Skin="Office2007">
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <MasterTableView Width="100%" DataKeyNames="id" Name="Products" AllowMultiColumnSorting="False" NoMasterRecordsText="No se encontraron registros para mostrar">
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Folio" UniqueName="id">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text='<%# Eval("id") %>' CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" CausesValidation="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="minimo" HeaderText="Mínimo" UniqueName="minimo" DataFormatString="{0:N}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="maximo" HeaderText="Máximo" UniqueName="maximo" DataFormatString="{0:N}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="porcentaje_utilidad" HeaderText="Precio" UniqueName="porcentaje_utilidad" DataFormatString="{0:C}">
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
                                    <asp:Button ID="btnAgregaMargenUtilidad" runat="server" Text="Agregar Rango" CssClass="item" CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <asp:Panel ID="panelMargenUtilidad" runat="server" Visible="False">
                            <table width="100%" cellpadding="3">
                                <tr>
                                    <td width="20%">
                                        <asp:Label ID="lblMinimoMargen" runat="server" CssClass="item" Text="Mínimo:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="lblMaximoMargen" runat="server" CssClass="item" Text="Máximo:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="lblPrecio" runat="server" CssClass="item" Text="Precio:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">&nbsp;</td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        <telerik:RadNumericTextBox ID="txtMinimoMargenUtilidad" runat="server" MinValue="0" Skin="Default" Value="0">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td width="20%">
                                        <telerik:RadNumericTextBox ID="txtMaximoMargenUtilidad" runat="server" MinValue="0" Skin="Default" Value="0">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td width="20%">
                                        <telerik:RadNumericTextBox ID="txtPorcentajeMargenUtilidad" runat="server" Type="Currency" MinValue="0" Skin="Default" Value="0">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td width="20%">&nbsp;</td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true" runat="server" ControlToValidate="txtMinimoMargenUtilidad" Text="Requerido" ForeColor="Red" ValidationGroup="MargenUtilidad" CssClass="item"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="20%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" SetFocusOnError="true" runat="server" ControlToValidate="txtMaximoMargenUtilidad" Text="Requerido" ForeColor="Red" ValidationGroup="MargenUtilidad" CssClass="item"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="20%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true" runat="server" ControlToValidate="txtPorcentajeMargenUtilidad" Text="Requerido" ForeColor="Red" ValidationGroup="MargenUtilidad" CssClass="item"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="20%">&nbsp;</td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lblMensajeMargenUtilidad" runat="server" Font-Bold="true" CssClass="item" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnGuardarMargenUtilidad" runat="server" Text="Guardar" ValidationGroup="MargenUtilidad" CssClass="item" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCancelarMargenUtilidad" runat="server" Text="Cancelar" CausesValidation="false" CssClass="item" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView6" runat="server" Width="100%">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblDescProductoHorario" runat="server" Font-Bold="true" Text="Producto :" CssClass="item"></asp:Label>&nbsp;
                            <asp:Label ID="lblDescHorarioValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                        </legend>
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 15%;">
                                    <asp:Label ID="lblPrecioAlterno" runat="server" CssClass="item" Font-Bold="true" Text="Precio Alterno:"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <telerik:RadNumericTextBox ID="txtPrecioAlterno" runat="server" Type="Currency" MinValue="0" Skin="Default" Value="0" Width="100px">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Configuración de Precios" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 12%;">
                                    <asp:Label ID="lblDiaHorario" runat="server" Font-Bold="true" Text="Día:" CssClass="item"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:DropDownList ID="cmbDia" runat="server" CssClass="box">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" InitialValue="0" ControlToValidate="cmbDia" Text="Requerido" ForeColor="Red" ValidationGroup="vgHorario" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblHoraInicio" runat="server" Font-Bold="true" Text="Hora Inicio:" CssClass="item"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTimePicker RenderMode="Lightweight" ID="RadHoraInicio" runat="server" SelectedTime='<%# eval("horarioinicial") %>' Calendar-CultureInfo="es-MX">
                                        <DateInput DateFormat="HH:mm:tt" Label="" ToolTip="Hora inicio"></DateInput>
                                        <TimeView ID="TimeView1" runat="server" DateFormat="HH:mm:tt" Interval="01:00:00" Columns="4"></TimeView>
                                    </telerik:RadTimePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server" ControlToValidate="RadHoraInicio" Text="Requerido" ForeColor="Red" ValidationGroup="vgHorario" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblHoraFin" runat="server" Font-Bold="true" Text="Hora fin" CssClass="item"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTimePicker RenderMode="Lightweight" ID="RadHoraFin" runat="server" SelectedTime='<%# eval("horarioinicial") %>' Calendar-CultureInfo="es-MX">
                                        <DateInput DateFormat="HH:mm:tt" Label="" ToolTip="Hora inicio"></DateInput>
                                        <TimeView ID="TimeView3" runat="server" DateFormat="HH:mm:tt" Interval="01:00:00" Columns="4"></TimeView>
                                    </telerik:RadTimePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server" ControlToValidate="RadHoraFin" Text="Requerido" ForeColor="Red" ValidationGroup="vgHorario" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Button ID="btnGuardarHorario" runat="server" Text="Guardar" ValidationGroup="vgHorario" CssClass="item" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <telerik:RadGrid ID="rghorario" runat="server" Width="100%" ShowStatusBar="True" Visible="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None" Skin="Office2007">
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <MasterTableView Width="100%" DataKeyNames="id" Name="Horario" AllowMultiColumnSorting="False" NoMasterRecordsText="No se encontraron registros para mostrar">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="dia" HeaderText="Día" UniqueName="dia">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn>
                                                    <HeaderTemplate>Hora Inicial</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <telerik:RadTimePicker RenderMode="Lightweight" ID="rtphorainicial" runat="server" Enabled="false" SelectedTime='<%# eval("horarioinicial") %>' Calendar-CultureInfo="es-MX">
                                                            <DateInput DateFormat="HH:mm:tt" Label="" ToolTip="Hora inicio"></DateInput>
                                                            <TimeView ID="TimeView1" runat="server" DateFormat="HH:mm:tt" Interval="01:00:00" Columns="4"></TimeView>
                                                        </telerik:RadTimePicker>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn>
                                                    <HeaderTemplate>Hora Final</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <telerik:RadTimePicker RenderMode="Lightweight" ID="rtphorafinal" runat="server" Enabled="false" SelectedTime='<%# eval("horariofin") %>' Calendar-CultureInfo="es-MX">
                                                            <DateInput DateFormat="HH:mm:tt" Label="" ToolTip="Hora fin"></DateInput>
                                                            <TimeView ID="TimeView2" runat="server" DateFormat="HH:mm:tt" Interval="01:00:00" Columns="4"></TimeView>
                                                        </telerik:RadTimePicker>
                                                    </ItemTemplate>
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
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView7" runat="server" Width="100%">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblDescProductoFoto" runat="server" Font-Bold="true" Text="Producto :" CssClass="item"></asp:Label>&nbsp;
                            <asp:Label ID="lblDescProductoFotoValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                        </legend>
                        <asp:HiddenField ID="hdnProductoId" runat="server" Value="0" />
                        <table border="0" align="center" style="width: 95%" class="item">
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" class="item" style="width: 50%; line-height: 20px;" valign="top">
                                    <strong>Foto 1 del producto:</strong><asp:ImageButton ID="imgBtnEliminarAnexo" runat="server" CausesValidation="false" ImageUrl="~/images/action_delete.gif" Visible="false" /><br />
                                    <asp:FileUpload ID="foto" runat="server" /><br />
                                    <asp:Image ID="imgProducto" runat="server" Width="400px" Visible="false" />&nbsp;&nbsp;
                                    <asp:HiddenField ID="hdnFoto" runat="server" Value="" />
                                </td>
                                <td colspan="2" class="item" style="width: 50%; line-height: 20px;" valign="top">
                                    <strong>Foto 2 del producto:</strong><asp:ImageButton ID="imgBtnEliminarAnexo2" runat="server" CausesValidation="false" ImageUrl="~/images/action_delete.gif" Visible="false" /><br />
                                    <asp:FileUpload ID="foto2" runat="server" /><br />
                                    <asp:Image ID="imgProducto2" runat="server" Width="400px" Visible="false" />&nbsp;&nbsp;
                                    <asp:HiddenField ID="hdnFoto2" runat="server" Value="" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Button ID="btnGuardarFoto" Text="Guardar" CausesValidation="false" runat="server" CssClass="item" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRegresarListado" runat="server" Text="Regresar al listado" CssClass="item" CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblMensajeFoto" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView8" runat="server" Width="100%">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblDescProductoPuntoReorden" runat="server" Font-Bold="true" Text="Producto :" CssClass="item"></asp:Label>&nbsp;
                            <asp:Label ID="lblDescProductoPuntoReordenValue" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                        </legend>
                        <table border="0" align="center" style="width: 100%" class="item">
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="grdProductosPuntoReorden" runat="server" Width="40%" ShowStatusBar="True" RenderMode="Lightweight"
                                        AutoGenerateColumns="False" AllowPaging="True" PageSize="100" GridLines="None" Skin="Office2007">
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <MasterTableView Width="100%" DataKeyNames="id" Name="Products" AllowMultiColumnSorting="False" NoMasterRecordsText="No se encontraron registros para mostrar">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>Punto reorden</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtPuntoReorden" runat="server" AutoPostBack="true" OnTextChanged="txtPuntoReorden_TextChanged" EnabledStyle-HorizontalAlign="Right" Text='<%#Eval("punto_reorden") %>' MinValue="0" Skin="Default" Width="90px">
                                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </asp:Panel>

        <telerik:RadWindowManager ID="RadAlert" runat="server" CssClass="item" Skin="Office2007" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" ReloadOnShow="true" RenderMode="Lightweight"></telerik:RadWindowManager>

    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>