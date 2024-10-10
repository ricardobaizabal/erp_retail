<%@ Page Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="existencias.aspx.vb" Inherits="LinkiumCFDI.existencias" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("presentacionesList") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" SkinID="Office2007" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table width="100%">
             <tr>
                    <td class="item" style="width: 10%;">
                        Sucursal:
                    </td>
                    <td class="item" style="width: 90%;" colspan="2">
                        <asp:DropDownList  ID="cmbsucursal"  runat="server"  AutoPostBack="false" CssClass="box"></asp:DropDownList>    
                    </td>
                </tr>
            
                <tr>
                    <td class="item" style="width: 10%;">
                        Familia:
                    </td>
                    <td class="item" style="width: 90%;" colspan="2">
                        <asp:DropDownList ID="filtrofamiliaid" AutoPostBack="true" runat="server" CssClass="box"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="item" style="width: 10%;">
                        Sub-Familia:
                    </td>
                    <td class="item" style="width: 90%;" colspan="2">
                        <asp:DropDownList ID="filtrosubfamiliaid" AutoPostBack="true" Enabled="false" runat="server" CssClass="box"></asp:DropDownList>&nbsp;    
                    </td>
                </tr>
                <tr>
                    <td class="item" style="width: 10%;">
                        Palabra clave:
                    </td>
                    <td class="item" style="width: 20%;">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="box"></asp:TextBox>
                    </td>
                    <td class="item" style="width: 70%;">
                        <asp:Button ID="btnSearch" runat="server" CssClass="boton" Text="Buscar" CausesValidation="false" />&nbsp;&nbsp;
                        <asp:Button ID="btnAll" runat="server" CssClass="boton" Text="Ver todo" CausesValidation="false" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListadoProductos_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProductsListLegend" runat="server" Text="Listado de Presentaciones" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px"></td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="presentacionesList" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None" ExportSettings-ExportOnlyData="false"
                            Skin="Office2007" ShowFooter="true">
                            <PagerStyle Mode="NextPrevAndNumeric" />
                            <ExportSettings IgnorePaging="True" FileName="CatalogoPresentaciones">
                                <Excel Format="Biff" />
                            </ExportSettings>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Presentaciones" AllowMultiColumnSorting="False" CommandItemDisplay="Top">
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="exportar a pdf" ExportToExcelText="exportat a excel"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unidad" HeaderText="Unidad de Medida" UniqueName="unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="costo_estandar" HeaderText="Costo" UniqueName="costo_estandar" DataFormatString="{0:c}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign = "Right" Aggregate="Sum">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Total" HeaderText="Total" UniqueName="total" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign = "Right" Aggregate="Sum">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">&nbsp;</td>
                </tr>
                <tr>
                    <td style="height: 2px">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
    </telerik:RadAjaxPanel>
</asp:Content>