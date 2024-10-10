<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_reportes_productos" Codebehind="productos.aspx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("reporteGrid") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblReportsLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item">
                        Seleccione el rango de fechas :<br /><br />
                        <table style="width:100%" border="0">
                            <tr>
                                <td style="width:8%;">Desde: </td>
                                <td style="width:15%;">
                                    <telerik:RadDatePicker ID="fechaini" Runat="server" Skin="Web20" Width="110px">
                                        <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False" 
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width:8%;">hasta: </td>
                                <td>
                                    <telerik:RadDatePicker ID="fechafin" Runat="server" Skin="Web20" Width="110px">
                                        <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False" 
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width:8%;">Cliente: </td>
                                <td>
                                    <asp:DropDownList ID="clienteid" runat="server" CssClass="box" Width="90%"></asp:DropDownList>
                                </td>
                                <td style="width:10%;">
                                    <asp:Button ID="btnGenerate" runat="server" Text="Generar" CssClass="boton" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">
                        <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="Small" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">                
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td class="item">
                        <telerik:RadGrid ID="reporteGrid" runat="server" AllowPaging="True" AllowSorting="true" 
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true" 
                            PageSize="50" ShowStatusBar="True"  ExportSettings-ExportOnlyData="false" 
                            Skin="Simple" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <ExportSettings IgnorePaging="True" FileName="Reporte_Productos">
                                <Excel Format="Biff" />
                            </ExportSettings>
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="productoid" Name="Productos" Width="100%" NoMasterRecordsText="No existen registros en ese rango de fechas." CommandItemDisplay="Top">
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="exportar a pdf" ExportToExcelText="exportat a excel"></CommandItemSettings>
                                <Columns>
                                    
                                    <%--<telerik:GridBoundColumn DataField="documento" HeaderText="Documento" UniqueName="documento" SortExpression="documento" ItemStyle-Width="80px">
                                    </telerik:GridBoundColumn>--%>
                                    
                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo" SortExpression="codigo">
                                    </telerik:GridBoundColumn>
                                
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion" SortExpression="descripcion">
                                    </telerik:GridBoundColumn>
                                    
                                    <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad" ItemStyle-HorizontalAlign="Right" SortExpression="cantidad">
                                    </telerik:GridBoundColumn>
                                    
                                    <telerik:GridBoundColumn DataField="total" HeaderText="Total" UniqueName="total" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" SortExpression="total">
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