<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_reportes_ingresosDiv" CodeBehind="ingresosDiv.aspx.vb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("reporteGrid") > -1) || (arguments.get_eventTarget().indexOf("lnkFolio") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">--%>
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblReportsLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td style="height: 5px"></td>
            </tr>
            <tr>
                <td class="item">Seleccione el rango de fechas que desee consultar:<br />
                    <br />
                    <table>
                        <tr>
                            <td>Desde: </td>
                            <td>
                                <telerik:RadDatePicker ID="fechaini" runat="server">
                                    <Calendar UseColumnHeadersAsSelectors="False"
                                        UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>Hasta: </td>
                            <td>
                                <telerik:RadDatePicker ID="fechafin" runat="server">
                                    <Calendar UseColumnHeadersAsSelectors="False"
                                        UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>

                            <td>Tipo:
                                <asp:DropDownList ID="tipoid" runat="server" CssClass="box" AutoPostBack="True"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="4">Cliente:
                                <asp:DropDownList ID="clienteid" runat="server" CssClass="box"></asp:DropDownList>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnGenerate" runat="server" Text="Generar" OnClick="btnGenerate_Click" />
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
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="Small" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black"></legend>
        <table width="100%">
            <tr>
                <td style="height: 5px" colspan="5"></td>
            </tr>
            <tr>
                <td class="item" colspan="5">&nbsp;&nbsp;<asp:CheckBox ID="chkAll" runat="server" Text="Seleccionar todo" /><br />
                    <br />
                    <telerik:RadGrid ID="reporteGrid" runat="server" AllowPaging="True" ShowHeader="true"
                        AutoGenerateColumns="False" GridLines="None" ShowFooter="true"
                        PageSize="50" ShowStatusBar="True" ExportSettings-ExportOnlyData="false"
                        Width="100%">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings IgnorePaging="True" FileName="ReporteFacturacion">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Ingresos" Width="100%" NoMasterRecordsText="No existen registros en ese rango de fechas." CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowExportToPdfButton="false" ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportar a Excel"></CommandItemSettings>
                            <Columns>
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkcfdid" runat="server" CssClass="item" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="serie" HeaderText="Serie" UniqueName="serie">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn>
                                    <HeaderTemplate>Folio</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFolio" runat="server" Text='<%# Eval("folio") %>' CommandArgument='<%# Eval("id") %>' CommandName="cmdFolio"></asp:LinkButton>
                                        <%--<asp:Label ID="lblFolio" runat="server" Visible =false ></asp:Label>--%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha" DataFormatString="{0:d}">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente" UniqueName="cliente">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="importe" HeaderText="Importe" UniqueName="importe" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="iva" HeaderText="IVA" UniqueName="iva" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ieps" HeaderText="IEPS" UniqueName="ieps" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="total" HeaderText="Total" UniqueName="total" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="estatus_cobranza" HeaderText="Estatus" UniqueName="estatus_cobranza" ItemStyle-HorizontalAlign="Left">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="instrucciones" HeaderText="Observaciones" UniqueName="instrucciones" ItemStyle-HorizontalAlign="Left">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>

        </table>
    </fieldset>
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black"></legend>
        <table width="80%">
            <tr>
                <td class="item">Estatus:&nbsp;<asp:RequiredFieldValidator ID="valEstatus" ControlToValidate="estatus_cobranzaid" ErrorMessage="* requerido" ForeColor="Red" SetFocusOnError="true" InitialValue="0" ValidationGroup="valUpdateAll" runat="server"></asp:RequiredFieldValidator><br />
                    <br />
                    <asp:DropDownList ID="estatus_cobranzaid" runat="server" CssClass="box"></asp:DropDownList></td>
                <td class="item">Tipo de pago:&nbsp;<asp:RequiredFieldValidator ID="valTipoPago" ControlToValidate="tipo_pagoid" ErrorMessage="* requerido" ForeColor="Red" SetFocusOnError="true" InitialValue="0" ValidationGroup="valUpdateAll" runat="server"></asp:RequiredFieldValidator><br />
                    <br />
                    <asp:DropDownList ID="tipo_pagoid" runat="server" CssClass="box"></asp:DropDownList></td>
                <td class="item">Referencia:<br />
                    <br />
                    <asp:TextBox ID="referencia" runat="server" CssClass="box"></asp:TextBox></td>
                <td class="item">Fecha de pago:&nbsp;<asp:RequiredFieldValidator ID="valFecha" ControlToValidate="fechapago" ErrorMessage="* requerido" ForeColor="Red" SetFocusOnError="true" ValidationGroup="valUpdateAll" runat="server"></asp:RequiredFieldValidator><br />
                    <br />
                    <telerik:RadDatePicker ID="fechapago" runat="server"></telerik:RadDatePicker>
                </td>
                <td class="item">
                    <asp:Button ID="btnPayAll" ValidationGroup="valUpdateAll" runat="server" Text="Aplicar" /></td>
            </tr>
            <tr>
                <td colspan="5">
                    <br />
                    <br />
                    <asp:Label ID="lblMensajeActualiza" runat="server" CssClass="item" ForeColor="Green"></asp:Label></td>
            </tr>
        </table>
    </fieldset>
    <br />
    <br />
    <br />
    <%--</telerik:RadAjaxPanel>--%>
</asp:Content>


