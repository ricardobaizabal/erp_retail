<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="complementopago.aspx.vb" Inherits="LinkiumCFDI.complementopago" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openRadWindow(url) {
            var radwindow = $find('<%=RadWindow2.ClientID %>');
            radwindow.setUrl(url);
            radwindow.show();
        }
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("lnkEdit") > -1) || (arguments.get_eventTarget().indexOf("lnkXML") > -1) || (arguments.get_eventTarget().indexOf("lnkPDF") > -1) || (arguments.get_eventTarget().indexOf("btnDelete") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">

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
                        <table style="width: 100%;" border="0">
                            <tr>
                                <td>Desde: </td>
                                <td>
                                    <telerik:RadDatePicker ID="fechaini" runat="server" Skin="Web20" Width="120px">
                                        <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>Hasta: </td>
                                <td>
                                    <telerik:RadDatePicker ID="fechafin" runat="server" Skin="Web20" Width="120px">
                                        <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>Cliente: </td>
                                <td style="width: 50%;">
                                    <asp:DropDownList ID="clienteid" runat="server" Width="95%" CssClass="box"></asp:DropDownList>
                                </td>
                                <td style="width: 10%;">
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
                    <td style="height: 5px"></td>
                </tr>
                <tr>
                    <td class="item">
                        <telerik:RadGrid ID="cfdlist" runat="server" Width="100%" ShowStatusBar="True" ShowFooter="true"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None"
                            AllowFilteringByColumn="false">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <ExportSettings IgnorePaging="True" FileName="ReporteComplementosPago">
                                <Excel Format="Biff" />
                            </ExportSettings>
                            <MasterTableView Width="100%" DataKeyNames="id" NoMasterRecordsText="No se encontraron registros" Name="Clients" AllowMultiColumnSorting="False" CommandItemDisplay="Top">
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowRefreshButton="false" ShowExportToPdfButton="false" ExportToPdfText="Exportar a PDF" ExportToExcelText="Exportar a Excel"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente" UniqueName="cliente" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="serie" HeaderText="Serie" UniqueName="serie" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="folio" HeaderText="Folio" UniqueName="folio" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="saldoAnterior" HeaderText="Saldo Anterior" UniqueName="saldoAnterior" AllowFiltering="false" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="importePagado" HeaderText="Importe Pagado" UniqueName="importePagado" AllowFiltering="false" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="saldoInsoluto" HeaderText="Saldo Insoluto" UniqueName="saldoInsoluto" AllowFiltering="false" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </fieldset>
        <telerik:RadWindowManager ID="RadAlert" runat="server" Skin="Default" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" BorderStyle="None" BorderWidth="0px" VisibleStatusbar="True" VisibleTitlebar="False" Behaviors="Close">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" runat="server" ShowContentDuringLoad="False" Modal="True" ReloadOnShow="True" VisibleStatusbar="False" VisibleTitlebar="True" BorderStyle="None" BorderWidth="0px" Behaviors="Close" Width="600px" Height="500px" Skin="Bootstrap">
                    <ContentTemplate>
                        <table style="width: 95%; height: 100%;" align="center" cellpadding="0" cellspacing="3" border="0">
                            <tr>
                                <td colspan="3" style="height: 10px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <asp:Label ID="lblFrom" runat="server" Font-Bold="true" CssClass="item" Text="De:"></asp:Label>
                                </td>
                                <td style="width: 70%">
                                    <asp:TextBox ID="txtFrom" runat="server" Width="100%" Enabled="false" CssClass="box"></asp:TextBox>
                                </td>
                                <td style="width: 20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <asp:Label ID="lblTo" runat="server" Font-Bold="true" CssClass="item" Text="Para:"></asp:Label>
                                </td>
                                <td style="width: 70%">
                                    <asp:DropDownList ID="cboTo" runat="server" Width="100%" CssClass="item"></asp:DropDownList>
                                </td>
                                <td style="width: 20%">
                                    <asp:RequiredFieldValidator ID="valPara" runat="server" ErrorMessage="Requerido" ControlToValidate="cboTo" ForeColor="Red" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <asp:Label ID="lblCC" runat="server" Font-Bold="true" CssClass="item" Text="CC:"></asp:Label>
                                </td>
                                <td style="width: 70%">
                                    <telerik:RadComboBox RenderMode="Lightweight" ID="cmbConCopia" runat="server" EmptyMessage="--Seleccione--" Localization-CheckAllString="Seleccionar todos" Localization-AllItemsCheckedString="Todos seleccionados" Localization-ItemsCheckedString="correos seleccionados" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" Value="-1">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <asp:Label ID="lblSubject" runat="server" Font-Bold="true" CssClass="item" Text="Asunto:"></asp:Label>
                                </td>
                                <td style="width: 70%">
                                    <asp:TextBox ID="txtSubject" runat="server" Width="100%" CssClass="box"></asp:TextBox>
                                </td>
                                <td style="width: 20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMenssage" TextMode="MultiLine" runat="server" Width="100%" Height="200px" CssClass="box"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="height: 15px">
                                    <asp:Label ID="lblMensajeEmail" runat="server" Style="color: #FF0000" Font-Bold="true" CssClass="item"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="right">
                                    <asp:Button ID="btnSendEmail" runat="server" CssClass="boton" Width="100px" Height="30px" Text="Enviar" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="height: 10px">
                                    <asp:HiddenField ID="tempcfdid" runat="server" Value="0" />
                                    <asp:HiddenField ID="clienteidcorreo" runat="server" Value="0" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <telerik:RadWindow ID="RadWindow2" runat="server" Modal="true" BorderStyle="None" BorderWidth="0px" CenterIfModal="true" AutoSize="False" Behaviors="Close" VisibleOnPageLoad="False" Width="1200" Height="900">
        </telerik:RadWindow>
    </telerik:RadAjaxPanel>
</asp:Content>
