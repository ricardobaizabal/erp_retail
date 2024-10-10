<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="recepcion_facturas.aspx.vb" Inherits="LinkiumCFDI.recepcion_facturas" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../styles/Styles.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="styles1.css" />
    <script type="text/javascript">
        function openRadWindowPDF(id) {
            var oWnd = radopen("AgregarPDF.aspx?id=" + id, "PdfWindow");
            oWnd.SetWidth(600);
            oWnd.SetHeight(300);
            oWnd.center();
        }
        function openRadWindowCorreo(id) {
            var oWnd = radopen("EnviarOrdenC.aspx?id=" + id, "PdfWindow");
            oWnd.SetWidth(800);
            oWnd.SetHeight(500);
            oWnd.center();
        }
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("btnCargarXML") > -1) || (arguments.get_eventTarget().indexOf("archivoPDF") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
    <style type="text/css">
        .textarea {
            resize: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="Label1" runat="server" Text="Recepción de Facturas" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" cellpadding="3" border="0">
                <tr style="height: 10px;">
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="width: 13%"><strong>Sucursal:</strong></td>
                    <td class="item" style="width: 30%">
                        <asp:DropDownList ID="sucursalid" runat="server" Width="95%" CssClass="box"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="valSucursal" runat="server" ControlToValidate="sucursalid" ErrorMessage="Seleccione una sucursal" InitialValue="0" ForeColor="Red" SetFocusOnError="true" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="item" style="width: 13%"><strong>Proveedor:</strong></td>
                    <td class="item" style="width: 30%">
                        <asp:HiddenField ID="ProviderID" runat="server" Value="0" />
                        <asp:DropDownList ID="proveedorid" runat="server" Width="95%" CssClass="box"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="valProveedor" runat="server" ControlToValidate="proveedorid" ErrorMessage="Seleccione un proveedor" InitialValue="0" ForeColor="Red" SetFocusOnError="true" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="item" style="width: 13%"><strong>Archivo(s) XML:</strong></td>
                    <td class="demo-container" style="width: 30%">
                        <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="archivosXML" Localization-Select="Seleccione" Localization-Cancel="Cancelar" Localization-Remove="Elimnar" Width="100%" AllowedFileExtensions="xml" TargetFolder="" MultipleFileSelection="Automatic" />
                    </td>
                    <td align="left">
                        <asp:HiddenField ID="IdFactura" runat="server" Value="0" />
                        <telerik:RadButton ID="btnCargarXML" RenderMode="Lightweight" runat="server" Skin="Silk" Text="Cargar XML"></telerik:RadButton>
                    </td>
                </tr>
                <tr style="height: 10px;">
                    <td colspan="3">
                        <asp:Label ID="lblMensaje" CssClass="item" Font-Bold="true" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <asp:Panel ID="pListadoFacturas" runat="server" Visible="true">
            <br />
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="Label2" runat="server" Text="Últimas Facturas Recibidas" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>
                <table width="100%" cellpadding="3" cellspacing="3" border="0">
                    <tr style="height: 10px;">
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="facturaslist" runat="server" Width="100%" ShowStatusBar="True" ShowFooter="true"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None"
                                Skin="Office2007" AllowFilteringByColumn="false">
                                <PagerStyle Mode="NumericPages"></PagerStyle>
                                <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" NoMasterRecordsText="No se encontraron registros para mostrar" Name="Facturas" Width="100%">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal" AllowFiltering="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="emisor" HeaderText="Proveedor" UniqueName="emisor" AllowFiltering="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="fecha_emision" HeaderText="Emisión" UniqueName="fecha_emision" DataFormatString="{0:d}" AllowFiltering="false">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc" AllowFiltering="false">
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridBoundColumn DataField="lugar_expedicion" HeaderText="Lugar Expedición" UniqueName="lugar_expedicion" AllowFiltering="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="serie" HeaderText="Serie" UniqueName="serie" AllowFiltering="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="folio" HeaderText="Folio" UniqueName="folio" AllowFiltering="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="uuid" HeaderText="UUID" UniqueName="uuid" AllowFiltering="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="total" HeaderText="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" UniqueName="total" AllowFiltering="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="moneda" HeaderText="Moneda" UniqueName="moneda" AllowFiltering="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" Visible="false" HeaderText="Enviar" UniqueName="">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgSend" runat="server" CausesValidation="false" ImageUrl="~/portalcfd/images/envelope.jpg" CommandArgument='<%# Eval("id") %>' CommandName="cmdSend" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>

                                        <%--<telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" HeaderText="Agregar PDF"
                                            UniqueName="Add">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_add.gif" CausesValidation="False" ToolTip="Agregar pdf" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>--%>

                                        <%--<telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkVer" runat="server" Text="Ver" CausesValidation="false" CommandArgument='<%# Eval("id") %>' CommandName="cmdVer"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>--%>

                                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Eliminar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CausesValidation="false" CommandArgument='<%# Eval("id") %>' CommandName="cmdEliminar"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>

                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr style="height: 10px;">
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="pDetalleProveedor" Width="100%" runat="server" Visible="False">
                    <table border="0" cellpadding="3" cellspacing="5" width="100%">
                        <tr>
                            <td class="item" style="font-weight: bold; width: 50%;" colspan="2">Proveedor</td>
                            <td class="item" style="font-weight: bold; width: 25%;">RFC</td>
                            <td class="item" style="font-weight: bold; width: 25%;">Regimen</td>
                        </tr>
                        <tr>
                            <td class="item" colspan="2">
                                <asp:Label ID="lblProveedor" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblRFC" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblRegimen" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="item" style="font-weight: bold; width: 25%;">Calle</td>
                            <td class="item" style="font-weight: bold; width: 25%;">No. Ext.</td>
                            <td class="item" style="font-weight: bold; width: 25%;">No. Int.</td>
                            <td class="item" style="font-weight: bold; width: 25%;">Colonia</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <asp:Label ID="lblCalle" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblNoExt" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblNoInt" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblColonia" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="item" style="font-weight: bold; width: 25%;">País</td>
                            <td class="item" style="font-weight: bold; width: 25%;">Estado</td>
                            <td class="item" style="font-weight: bold; width: 25%;">Municipio</td>
                            <td class="item" style="font-weight: bold; width: 25%;">CP</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <asp:Label ID="lblPais" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblEstado" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblMunicipio" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblCP" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="item" style="font-weight: bold; width: 25%;">Fecha Emisión</td>
                            <td class="item" style="font-weight: bold; width: 25%;">Lugar Expedición</td>
                            <td class="item" style="font-weight: bold; width: 25%;">Método de Pago</td>
                            <td class="item" style="font-weight: bold; width: 25%;">Forma de Pago</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <asp:Label ID="lblFechaEmision" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblLugarExpedicion" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblFormaPago" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblMetodoPago" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="item" style="font-weight: bold; width: 25%;">Serie</td>
                            <td class="item" style="font-weight: bold; width: 25%;">Folio</td>
                            <td class="item" style="font-weight: bold; width: 25%;">UUID</td>
                            <td class="item" style="font-weight: bold; width: 25%;">Folio de Referencia</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <asp:Label ID="lblSerie" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblFolio" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblUUID" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblFolReferencia" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="item" style="font-weight: bold; width: 25%;">Importe</td>
                            <td class="item" style="font-weight: bold; width: 25%;">Impuestos</td>
                            <td class="item" style="font-weight: bold; width: 25%;">Total</td>
                            <td class="item" style="font-weight: bold; width: 25%;">Orden de Compra</td>
                        </tr>
                        <tr>
                            <td class="item">
                                <asp:Label ID="lblImporte" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblImpuestos" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label></td>
                            <td class="item">
                                <asp:Label ID="lblOrdenCompra" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px;" colspan="4">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="item" colspan="4" style="font-weight: bold; width: 25%;">Sello Digital CFDI</td>
                        </tr>
                        <tr>
                            <td class="item" colspan="4">
                                <asp:TextBox ID="lblSelloCFDI" Width="100%" BorderStyle="None" CssClass="textarea" TextMode="MultiLine" ReadOnly="true" Rows="4" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="item" colspan="4" style="font-weight: bold; width: 25%;">Sello Digital SAT</td>
                        </tr>
                        <tr>
                            <td class="item" colspan="4">
                                <asp:TextBox ID="lblSelloSAT" Width="100%" BorderStyle="None" CssClass="textarea" TextMode="MultiLine" ReadOnly="true" Rows="4" runat="server" Text=""></asp:TextBox></td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="3" cellspacing="3" border="0">
                        <tr style="height: 10px;">
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="RadGrid1" runat="server" Width="100%" ShowStatusBar="True"
                                    AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None"
                                    Skin="Simple" AllowFilteringByColumn="false">
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                    <MasterTableView Width="100%" DataKeyNames="id" Name="Facturas2" AllowMultiColumnSorting="False">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id" AllowFiltering="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="concepto" HeaderText="Concepto" UniqueName="concepto" AllowFiltering="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="serialnumber" HeaderText="Número De Serie" UniqueName="serialnumber" AllowFiltering="false">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Style="z-index: 7001">
        <Windows>
            <telerik:RadWindow ID="PdfWindow" runat="server" ShowContentDuringLoad="False" Modal="True" ReloadOnShow="True" Skin="Default" VisibleStatusbar="False" Width="768px" Height="600px" Behavior="Close" BackColor="Gray" Behaviors="Close" InitialBehavior="None">
            </telerik:RadWindow>
            <%-- <telerik:RadWindow ID="RadWindow1" runat="server" ShowContentDuringLoad="False" Modal="True" ReloadOnShow="True" VisibleStatusbar="False" VisibleTitlebar="True" BorderStyle="None" BorderWidth="0px" Behaviors="Close" Width="700px" Height="500px" Skin="Simple">
                    <ContentTemplate>
                        <table style="width: 95%; height: 100%;" align="center" cellpadding="0" cellspacing="3" border="0">
                            <tr>
                                <td colspan="2" style="height: 10px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <asp:Label ID="lblFrom" runat="server" Font-Bold="true" CssClass="item" Text="De:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFrom" runat="server" Width="100%" Enabled="false" CssClass="box"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <asp:Label ID="lblTo" runat="server" Font-Bold="true" CssClass="item" Text="Para:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboTo" runat="server" CssClass="item"></asp:DropDownList>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Requerido" ControlToValidate="cboTo" ForeColor="Red" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <asp:Label ID="lblCC" runat="server" Font-Bold="true" CssClass="item" Text="CC:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBox1" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" 
                                        Width="500" Value="-1">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <asp:Label ID="lblSubject" runat="server" Font-Bold="true" CssClass="item" Text="Asunto:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubject" runat="server" Width="100%" CssClass="box"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtMenssage" TextMode="MultiLine" runat="server" Width="100%" Height="200px" CssClass="box"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 15px">
                                    <asp:Label ID="lblMensajeEmail" runat="server" Style="color: #FF0000" Font-Bold="true" CssClass="item"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnSendEmail" runat="server" CssClass="boton" Width="100px" Height="30px" Text="Enviar" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px">
                                    <asp:HiddenField ID="tempcfdid" runat="server" Value="0" />
                                    <asp:HiddenField ID="clienteidcorreo" runat="server" Value="0" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>--%>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
