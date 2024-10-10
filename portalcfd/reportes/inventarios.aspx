<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="inventarios.aspx.vb" Inherits="LinkiumCFDI.inventarios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("reporteGrid") > -1)) {
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
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" SkinID="Office2007" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblFiltros" Text="Reportes - Reporte de inventarios" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" border="0">
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="width: 12%;">Sucursal:</td>
                    <td class="item" colspan="2">
                        <asp:DropDownList ID="filtrosucursalid" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Buscar" ValidationGroup="busqueda" CausesValidation="True" />
                        <%--<asp:RequiredFieldValidator ID="valSucursal" runat="server" ControlToValidate="filtrosucursalid" ValidationGroup="busqueda" InitialValue="0" ForeColor="Red" SetFocusOnError="True" Text="Requerido" CssClass="item"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="item" style="width: 12%;">Familia:</td>
                    <td class="item" style="width: 40%;">
                        <asp:DropDownList ID="filtrofamiliaid" AutoPostBack="True" runat="server"></asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="width: 12%;">Subfamilia:</td>
                    <td class="item" style="width: 40%;">
                        <asp:DropDownList ID="filtrosubfamiliaid" AutoPostBack="false" Enabled="false" runat="server"></asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="Label1" Text="Existencias" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item">
                        <telerik:RadGrid ID="reporteGrid" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="True" ExportSettings-ExportOnlyData="true"
                            PageSize="50" ShowStatusBar="True"
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <ExportSettings IgnorePaging="True" ExportOnlyData="true" FileName="ReporteExistencias">
                                <Excel Format="ExcelML" />
                            </ExportSettings>
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Existencias" Width="100%" NoMasterRecordsText="No existen registros." CommandItemDisplay="Top">
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="True" ShowExportToPdfButton="false" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="producto" HeaderText="Producto" UniqueName="producto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unidad" HeaderText="Unidad de Medida" UniqueName="unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="familia" HeaderText="Familia" UniqueName="familia">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="subfamilia" HeaderText="Subfamilia" UniqueName="subfamilia">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="costo_estandar" HeaderText="Costo" UniqueName="costo_estandar" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unitario" HeaderText="Precio" UniqueName="unitario" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="importe" HeaderText="Importe costo" UniqueName="importe" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="importe_ventas" HeaderText="Importe venta" UniqueName="importe_ventas" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </fieldset>
    </telerik:RadAjaxPanel>
</asp:Content>
