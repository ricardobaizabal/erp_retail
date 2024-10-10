<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" EnableEventValidation="false" Inherits="LinkiumCFDI.portalcfd_almacen_abastecimiento" CodeBehind="abastecimiento.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("productslist") > -1) || (arguments.get_eventTarget().indexOf("productslist") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
        function confirmCallbackFinalizarTransferencia(arg) {
            if (arg) //the user clicked OK
            {
                __doPostBack("<%=btnFinalizarTransferencia.UniqueID %>", "");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table width="100%">
                <tr>
                    <td class="item" style="width: 10%;">Sucursal:</td>
                    <td style="width: 20%;">
                        <asp:DropDownList ID="filtrosucursalid" AutoPostBack="false" runat="server" Width="95%"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="valSearch" runat="server" ControlToValidate="filtrosucursalid" InitialValue="0" ErrorMessage="Debes seleccionar una sucursal." ValidationGroup="ValSearch" class="item" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="item" style="width: 10%;">Familia:</td>
                    <td style="width: 20%;">
                        <asp:DropDownList ID="filtrofamiliaid" AutoPostBack="true" runat="server" Width="95%"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="item" style="width: 10%;">Sub-Familia:</td>
                    <td style="width: 20%;">
                        <asp:DropDownList ID="filtrosubfamiliaid" AutoPostBack="false" Enabled="false" runat="server" Width="95%"></asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="width: 10%;">Palabra clave:</td>
                    <td style="width: 20%;">
                        <asp:TextBox ID="txtSearch" runat="server" Width="93%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar" ValidationGroup="ValSearch" CausesValidation="true" />&nbsp;&nbsp;
                        <asp:Button ID="btnAll" runat="server" Text="Ver todo" ValidationGroup="ValSearch" CausesValidation="true" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblProductsListLegend" Text="Productos en punto de reorden" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px"></td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="productslist" runat="server" ShowHeader="true"
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true"
                            PageSize="50" ShowStatusBar="True" ExportSettings-ExportOnlyData="false"
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <ExportSettings IgnorePaging="True" FileName="ReporteAbastecimiento">
                                <Excel Format="Biff" />
                            </ExportSettings>
                            <MasterTableView Width="100%" DataKeyNames="id,punto_reorden,existencia,existencia_bodega,presentacionid,disponibles,factor" Name="Products" AllowMultiColumnSorting="False" NoMasterRecordsText="No se encontraron registros" CommandItemDisplay="Top">
                                <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="exportar a pdf" ExportToExcelText="exportat a excel"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unidad" HeaderText="Unidad" UniqueName="unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" FooterText=" ">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="punto_reorden" HeaderText="Punto de reorden" UniqueName="reorden" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="existencia_bodega" HeaderText="Existencia Bodega" UniqueName="existencia_bodega" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" FooterText=" ">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="disponibles" HeaderText="Disponibles" UniqueName="disponibles" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" FooterText=" ">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Cantidad Transferencia" HeaderStyle-HorizontalAlign="Center" UniqueName="cantidad" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="180px" FooterText=" ">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCantidad" Value="0" MaxValue='<%# Eval("disponibles") %>' runat="server" Width="90px">
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
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
                    <td style="text-align: right;">
                        <asp:Button ID="btnAgregarTransferencia" runat="server" Text="Agregar lote de transferencia" ValidationGroup="ValSearch" CausesValidation="true" Visible="false" />
                        <asp:Button ID="btnFinalizarTransferencia" runat="server" Text="Finalizar lote de transferencia" ValidationGroup="ValSearch" CausesValidation="false" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Office2007" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rwConfirm" runat="server" Skin="Office2007" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
