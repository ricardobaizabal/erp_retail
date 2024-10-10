<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="canceladas.aspx.vb" Inherits="LinkiumCFDI.canceladas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblReportsLegend" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
        </legend>
        <table style="width: 100%;">
            <tr>
                <td style="height: 5px"></td>
            </tr>
            <tr>
                <td class="item">Seleccione el rango de fechas que desee consultar:<br />
                    <br />
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 4%;">Desde: </td>
                            <td style="width: 13%;">
                                <telerik:RadDatePicker ID="fechaini" runat="server" Width="98%">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>
                            <td style="width: 4%;">Hasta: </td>
                            <td style="width: 13%;">
                                <telerik:RadDatePicker ID="fechafin" runat="server" Width="98%">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>
                            <td style="width: 4%;">Cliente:</td>
                            <td style="width: 35%;">
                                <asp:DropDownList ID="cmbCliente" runat="server" Width="95%" CssClass="box"></asp:DropDownList>
                            </td>
                            <td style="width: 4%;">Tipo:</td>
                            <td style="width: 15%;">
                                <asp:DropDownList ID="cmbTipoDocumento" runat="server" CssClass="box" Width="95%" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnGenerate" runat="server" Text="Generar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 5px"></td>
            </tr>
            <tr>
                <td style="height: 5px">
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black"></legend>
        <table style="width: 100%;">
            <tr>
                <td style="height: 5px" colspan="5">&nbsp;</td>
            </tr>
            <tr>
                <td class="item" colspan="5">
                    <telerik:RadGrid ID="reporteGrid" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" GridLines="None" ShowFooter="True"
                        PageSize="50" ShowStatusBar="True"
                        Width="100%">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings IgnorePaging="True" FileName="ReporteCanceladas" ExportOnlyData="True">
                            <Excel Format="ExcelML" />
                        </ExportSettings>
                        <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Canceladas" Width="100%" NoMasterRecordsText="No existen registros en ese rango de fechas." CommandItemDisplay="Top">
                            <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="True" ShowExportToPdfButton="false" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="serie" HeaderText="Serie" UniqueName="serie">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="folio" HeaderText="Folio" UniqueName="folio">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha Factura" UniqueName="fecha" DataFormatString="{0:d}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="fecha_cancelacion" HeaderText="Fecha Cancelación" UniqueName="fecha_cancelacion" DataFormatString="{0:d}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente" UniqueName="cliente">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="importe" HeaderText="Importe" UniqueName="importe" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="iva" HeaderText="IVA" UniqueName="iva" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ieps" HeaderText="IEPS" UniqueName="ieps" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="total" HeaderText="Total" UniqueName="total" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="estatus_cobranza" HeaderText="Estatus" UniqueName="estatus_cobranza" ItemStyle-HorizontalAlign="Left">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="instrucciones" HeaderText="Instrucciones Esp." UniqueName="instrucciones">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <br />
    <br />
</asp:Content>