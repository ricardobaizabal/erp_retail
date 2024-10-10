<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="retiros.aspx.vb" Inherits="LinkiumCFDI.retiros" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("cortesList") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" SkinID="Office2007" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset style="min-height: 400px;">
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblListTitle" runat="server" Font-Bold="true" CssClass="item" Text="Retiros de Caja"></asp:Label>
            </legend>
            <br />
            <table>
                <tr>
                    <td colspan="7" class="item">Seleccione el rango de fechas que desee consultar:<br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%;">
                        <asp:Label ID="lblDesde" Text="Desde:" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                    </td>
                    <td style="width: 20%;">
                        <telerik:RadDatePicker ID="calFechaDesde" Skin="Office2007" runat="server">
                        </telerik:RadDatePicker>
                    </td>
                    <td style="width: 5%;">
                        <asp:Label ID="lblHasta" Text="Hasta:" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                    </td>
                    <td style="width: 20%;">
                        <telerik:RadDatePicker ID="calFechaHasta" Skin="Office2007" runat="server">
                        </telerik:RadDatePicker>
                    </td>
                    <td style="width: 5%;">
                        <asp:Label ID="lblSucursal" Text="Sucursal:" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                    </td>
                    <td style="width: 25%;">
                        <asp:DropDownList ID="cmbSucursal" runat="server" Width="90%"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" Width="90px" CausesValidation="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7">
                        <telerik:RadGrid ID="retirosList" runat="server" Width="100%" ShowStatusBar="True" ShowFooter="true"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None" Skin="Office2007">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Retiros" NoMasterRecordsText="No se encontraron registros." AllowMultiColumnSorting="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="No. Retiro" UniqueName="id">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="usuario" HeaderText="Cajero" UniqueName="usuario">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="solicita" HeaderText="Solicita" UniqueName="solicita">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="concepto" HeaderText="Concepto" UniqueName="concepto">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="importe" HeaderText="Importe" UniqueName="importe" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
    </telerik:RadAjaxPanel>
</asp:Content>