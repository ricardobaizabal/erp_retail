<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="pedidos.aspx.vb" Inherits="LinkiumCFDI.pedidos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- jQuery library (served from Google) -->
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <style type="text/css">
        .div {
            border: 1px solid #a1a1a1;
            padding: 10px 40px;
            background: #e4e4e4;
            width: 300px;
            min-width: 300px;
            border-radius: 15px;
        }

        .RadWindow .rwTitle {
            font-size: 14px !important;
            font-family: Verdana !important;
        }

        .rwDialogMessage {
            font-size: 14px !important;
            font-family: Verdana !important;
        }

        .RadWindow_Office2007 .rwDialogButtons button {
            font-size: 14px !important;
            font-family: Verdana !important;
        }
    </style>
    <script type="text/javascript">
        function mensaje() {
            $('#dmensaje').slideToggle(2500);
        }
    </script>
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("btnDownload") > -1) || (arguments.get_eventTarget().indexOf("pedidosList") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table style="width: 100%;" border="0" cellpadding="5">
                <tr>
                    <td class="item" style="width: 6%; font-weight: bold;">Desde:</td>
                    <td class="item" style="width: 10%;">
                        <telerik:RadDatePicker ID="fha_ini" Skin="Office2007" runat="server">
                        </telerik:RadDatePicker>
                    </td>
                    <td class="item" style="width: 6%; font-weight: bold;">Hasta:</td>
                    <td class="item" style="width: 10%;">
                        <telerik:RadDatePicker ID="fha_fin" Skin="Office2007" runat="server">
                        </telerik:RadDatePicker>
                    </td>
                    <td class="item"></td>
                    <td class="item">
                        <asp:CheckBox ID="chkPedidoWeb" runat="server" Text="Pedido WEB" Font-Bold="true" />
                    </td>
                    <td class="item">
                        <asp:CheckBox ID="chkRequieroFacturaBit" runat="server" Text="Requiere factura" Font-Bold="true" />
                    </td>
                </tr>
                <tr>
                    <td class="item" style="width: 6%; font-weight: bold;">Matrícula:</td>
                    <td class="item" style="width: 10%;">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="box"></asp:TextBox>
                    </td>
                    <td class="item" style="width: 6%; font-weight: bold;">Estatus:</td>
                    <td class="item" style="width: 10%;">
                        <asp:DropDownList ID="cmbestatusb" Skin="Silk" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                    </td>
                    <td class="item" style="width: 6%; font-weight: bold;">Sucursal:</td>
                    <td class="item" style="width: 10%;">
                        <asp:DropDownList ID="cmbsucursalb" Skin="Silk" runat="server" AutoPostBack="false"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar" CausesValidation="false" AutoPostBack="true" />&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" CausesValidation="false" Text="Ver todo" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProvidersListLegend" runat="server" Text="Listado de Pedidos" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" border="0">
                <tr style="height: 50px">
                    <td colspan="2">
                        <asp:Label ID="lblMensaje" ForeColor="Red" runat="server" class="item" Visible="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px" colspan="2">
                        <telerik:RadGrid ID="pedidosList" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None" ExportSettings-ExportOnlyData="false"
                            Skin="Office2007" ShowFooter="true">
                            <PagerStyle Mode="NextPrevAndNumeric" />
                            <ExportSettings IgnorePaging="True" FileName="CatalogoPresentaciones">
                                <Excel Format="Biff" />
                            </ExportSettings>
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Pedidos" NoMasterRecordsText="No se encontraron registros para mostrar" Width="100%" CommandItemDisplay="Top">
                                <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="exportar a pdf" ExportToExcelText="exportat a excel"></CommandItemSettings>
                                <Columns>

                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Folio" UniqueName="id">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn DataField="Matricula" HeaderText="Matrícula" UniqueName="matricula">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Alumno" HeaderText="Alumno" UniqueName="alumno">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="PiezasenTotal" HeaderText="Piezas Total" UniqueName="condiciones">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Monto" HeaderText="Monto" UniqueName="precio" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterStyle-Font-Bold="true">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="F_Alta" HeaderText="Fecha Alta" UniqueName="fecha_alta" DataFormatString="{0:dd/MM/yyyy}">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="F_Pago" HeaderText="Fecha de Pago" UniqueName="fecha_alta" DataFormatString="{0:dd/MM/yyyy}">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="F_Entrega" HeaderText="Fecha de Entrega" UniqueName="fecha_alta" DataFormatString="{0:dd/MM/yyyy}">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Pedido WEB" HeaderStyle-HorizontalAlign="Center" UniqueName="pedidoweb">
                                        <ItemTemplate>
                                            <asp:Image ID="imgPedidoWeb" runat="server" ImageAlign="AbsMiddle" Visible="false" ImageUrl="~/images/icons/arrow.gif" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Requiere Factura" HeaderStyle-HorizontalAlign="Center" UniqueName="requieroFacturaBit">
                                        <ItemTemplate>
                                            <asp:Image ID="imgRequiereFactura" runat="server" ImageAlign="AbsMiddle" Visible="false" ImageUrl="~/images/icons/arrow.gif" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn DataField="factura" HeaderText="Factura" UniqueName="factura">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn UniqueName="ColFacturar40" AllowFiltering="true" HeaderText="Facturar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Exportable="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkFacturar40" runat="server" Text="Facturar" Visible="false" CommandArgument='<%# Eval("id") %>' CommandName="cmdFacturar40"></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" CausesValidation="false" ImageUrl="~/images/action_delete.gif" />
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
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr style="height: 50px">
                    <td>
                        <div id="dmensaje" style="display: none" class="div">
                            <asp:Label ID="lblMensajeGuardar" runat="server" CssClass="item" ForeColor="#ffffff" Font-Bold="true" Font-Size="Small"></asp:Label>
                        </div>
                    </td>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAgregarPedido" runat="server" Text="Agregar Pedido" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px" colspan="2">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
        <asp:Panel ID="panelRegistroPedido" runat="server" Visible="False">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/icons/AgregarEditarProveedor_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblEditarLeyendaOportunidad" Text="Agregar/Editar Pedido" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table border="0" width="100%">
                    <tr style="height: 25px;">
                        <td width="40%" colspan="2">
                            <asp:Label ID="Label1" runat="server" Text="Seleccione una sucursal:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="40%" colspan="2">
                            <asp:Label ID="Label2" runat="server" Text="Seleccione un estatus:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="20%">&nbsp;</td>
                    </tr>
                    <tr style="height: 25px;">
                        <td width="40%" colspan="2">
                            <asp:DropDownList ID="cmbsucursal" Skin="Silk" runat="server" Width="95%" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td width="40%" colspan="2">
                            <asp:DropDownList ID="cmbestatus" Skin="Silk" runat="server" Width="95%" AutoPostBack="false"></asp:DropDownList>
                        </td>
                        <td width="20%">&nbsp;</td>
                    </tr>
                    <tr style="height: 25px;">
                        <td width="40%" colspan="2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbsucursal" CssClass="item"></asp:RequiredFieldValidator>
                        </td>
                        <td width="40%" colspan="2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbestatus" CssClass="item"></asp:RequiredFieldValidator>
                        </td>
                        <td width="20%">&nbsp;</td>
                    </tr>
                    <tr style="height: 25px;">
                        <td width="40%" colspan="2">
                            <asp:Label ID="lblBusqueda" runat="server" Text="Seleccione un alumno:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="20%">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
                    </tr>
                    <tr style="height: 25px;">
                        <td width="40%" colspan="2">
                            <telerik:RadAutoCompleteBox RenderMode="Lightweight" ID="cmbAlumnos" Skin="Silk" CssClass="item" runat="server" Width="95%" Style="text-transform: uppercase;"
                                DataTextField="Alumno" InputType="Text" TextSettings-SelectionMode="Single"
                                DataValueField="id">
                                <DropDownItemTemplate>
                                    <table style="width: 100%;" border="0">
                                        <tr>
                                            <td style="width: 100%" class="item">
                                                <%# DataBinder.Eval(Container.DataItem, "Alumno")%>
                                            </td>
                                        </tr>
                                    </table>
                                </DropDownItemTemplate>
                            </telerik:RadAutoCompleteBox>
                        </td>
                        <td width="20%">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
                    </tr>
                    <tr style="height: 25px;">
                        <td width="40%" colspan="2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbAlumnos" CssClass="item"></asp:RequiredFieldValidator>
                        </td>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr style="height: 25px;">
                        <td colspan="5">
                            <asp:Label ID="lblComentarios" runat="server" Text="Comentarios:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <telerik:RadTextBox ID="txtComentarios" TextMode="MultiLine" runat="server" Width="60%" Height="50px"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td valign="bottom" colspan="5">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" />&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                            <asp:HiddenField ID="pedidoID" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="panelDatosPortalWeb" runat="server" Visible="False">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image7" runat="server" ImageUrl="~/images/icons/ConfiguraDatos_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="Label3" Text="Datos registro portal web" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table style="width: 100%" border="0">
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lblNombrePadreMadre" runat="server" Text="Nombre del padre/madre:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblEmailPadreMadre" runat="server" Text="Email:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblNombreAlumno" runat="server" Text="Nombre alumno:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblMatricula" runat="server" Text="Matrícula:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblSucursalPedido" runat="server" Text="Sucursal:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lblNombrePadreMadreValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblEmailPadreMadreValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblNombreAlumnoValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblMatriculaValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblSucursalPedidoValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lbltransactionTokenId" runat="server" Text="Identificación del token de transacción:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblpaymentMethod" runat="server" Text="Método de pago:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblbankName" runat="server" Text="Nombre del banco:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblmerchantReferenceCode" runat="server" Text="Código de referencia del comerciante:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblorderId" runat="server" Text="ID solicitud:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lbltransactionTokenIdValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblpaymentMethodValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblbankNameValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblmerchantReferenceCodeValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblorderIdValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lblaceptoTerminosCondiciones" runat="server" Text="Acepto términos y condiciones:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblrequieroFacturaBit" runat="server" Text="Requiere factura:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblSerieFactura" runat="server" Text="Serie factura:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFoliofactura" runat="server" Text="Folio factura:" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lblaceptoTerminosCondicionesValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblrequieroFacturaValue" runat="server" CssClass="item"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <telerik:RadTextBox ID="txtSerieFactura" RenderMode="Lightweight" runat="server">
                            </telerik:RadTextBox>
                        </td>
                        <td style="width: 20%">
                            <telerik:RadNumericTextBox ID="txtFolioFactura" RenderMode="Lightweight" runat="server">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20%">
                            <asp:Button ID="btnGuardarFactura" runat="server" Text="Guardar factura" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="panelProductosCotizados" runat="server" Visible="False">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image6" runat="server" ImageUrl="~/images/icons/ConfiguraCertificados_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProductosCotizados" Text="Productos en Pedidos" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table style="width: 100%" border="0">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gridProductosCotizados" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" GridLines="None"
                                PageSize="20" ShowStatusBar="True"
                                Skin="Simple" Width="100%">
                                <PagerStyle Mode="NumericPages" />
                                <MasterTableView AllowMultiColumnSorting="False" Name="ProductoCotizados" NoMasterRecordsText="No se encontraron registros para mostrar" Width="100%">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="panelItemsRegistration" runat="server" Visible="False">
            <fieldset style="padding: 10px;">
                <asp:HiddenField ID="productoid" runat="server" />
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/portalcfd/images/concept.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientItems" Text="Conceptos" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table width="900" cellspacing="0" cellpadding="1" border="0" align="center">
                    <tr>
                        <td valign="bottom" class="item">
                            <strong>Buscar:</strong>
                            <asp:TextBox ID="txtSearchItem" runat="server" CssClass="box" AutoPostBack="true"></asp:TextBox>&nbsp;presione enter después de escribir el código
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <telerik:RadGrid ID="gridResults" runat="server" Width="100%" ShowStatusBar="True"
                                AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                                Skin="Office2007" Visible="false">
                                <MasterTableView Width="100%" DataKeyNames="productoid" NoMasterRecordsText="No hay registros que mostrar." Name="Items" AllowMultiColumnSorting="False">
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Código</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodigo" runat="server" Text='<%# eval("codigo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" />
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Descripción</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# eval("descripcion") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" />
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Cant.</HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtCantidad" runat="server" AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged" Skin="Default" Width="50px" MinValue="0" Value='0'>
                                                    <NumberFormat DecimalDigits="4" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Unidad</HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtCostoUnitario" runat="server" Text='<%# eval("preciounitario") %>' Enabled="false" MinValue="0" Value="0" Skin="Default" Width="80px">
                                                    <NumberFormat DecimalDigits="4" GroupSeparator="," />


                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Importe</HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtImporte" runat="server" Text='<%# eval("precio") %>' Enabled="false" MinValue="0" Value="0" Skin="Default" Width="80px">
                                                    <NumberFormat DecimalDigits="4" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>



                                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Add">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("productoid") %>' CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_add.gif" CausesValidation="False" ToolTip="Agregar entrada de este producto" />
                                                <asp:LinkButton ID="lnkCombinaciones" runat="server" Text="Combinaciones" CommandArgument='<%# Eval("productoid") %>' CommandName="cmdCombinaciones"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <br />
                            <asp:Button ID="btnCancelSearch" Visible="false" runat="server" CausesValidation="False" Text="Cancelar" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="900" cellspacing="0" cellpadding="1" border="0" align="center">
                    <tr>
                        <td>
                            <br />
                            <telerik:RadGrid ID="itemsList" runat="server" Width="100%" ShowStatusBar="True"
                                AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                                Skin="Simple" Visible="False">
                                <MasterTableView Width="100%" DataKeyNames="id" Name="Items" AllowMultiColumnSorting="False">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="unidad" HeaderText="Unidad de medida" UniqueName="unidad">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="precio" HeaderText="Precio" UniqueName="precio" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="importe" HeaderText="Importe" UniqueName="importe" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center"
                                            UniqueName="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>'
                                                    CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" CausesValidation="False" />
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
                        <td style="height: 30px">&nbsp;</td>
                    </tr>

                    <tr>
                        <td style="height: 20px">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="panelCombinaciones" runat="server" Visible="false">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblCombinacionesList" runat="server" Text="Combinaciones de producto" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td style="height: 5px">
                            <asp:HiddenField ID="ProductID" runat="server" Value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                            <telerik:RadGrid ID="ProductoCombinacionesList" runat="server" Width="100%" ShowStatusBar="True"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None" ShowFooter="true"
                                Skin="Office2007">
                                <PagerStyle Mode="NumericPages"></PagerStyle>
                                <MasterTableView Width="100%" DataKeyNames="productoid" Name="Combinaciones" NoMasterRecordsText="No hay registros que mostrar." AllowMultiColumnSorting="False">
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Código</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodigo" runat="server" Text='<%# eval("codigo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" />
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Producto</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProducto" runat="server" Text='<%# eval("producto") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" />
                                        </telerik:GridTemplateColumn>

                                        <%--<telerik:GridBoundColumn DataField="producto" HeaderText="Producto" UniqueName="producto">
                                    </telerik:GridBoundColumn>--%>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Combinacion</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# eval("combinacion") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" />
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Cant.</HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtCantidad" runat="server" AutoPostBack="true" OnTextChanged="txtCantidadCombinacion_TextChanged" Skin="Default" Width="50px" MinValue="0" Value='0'>
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Costo Unit.</HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtCostoUnitario" runat="server" Text='<%# eval("unitario") %>' Enabled="false" MinValue="0" Value="0" Skin="Default" Width="80px">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />

                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Importe</HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtImporte" runat="server" Text='<%# eval("costo_estandar") %>' Enabled="false" MinValue="0" Value="0" Skin="Default" Width="80px">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Sucursal</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="cmbSucursal" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>



                                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Add">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("combinacionid") & "|" & Eval("productoid") %>' CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_add.gif" CausesValidation="False" ToolTip="Agregar entrada de este producto" />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">&nbsp;</td>
                    </tr>

                    <tr>
                        <td style="height: 5px">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="panelResumen" runat="server" Visible="False">
            <fieldset style="padding: 10px;">
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/portalcfd/images/resumen.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblResumen" Text="Resumen" runat="server" Font-Bold="true" CssClass="item"></asp:Label>

                </legend>

                <br />

                <table width="100%" align="left">
                    <%--<tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblSubtotal" runat="server" Text="Sub Total =" CssClass="item" Font-Bold="True"></asp:Label>&nbsp;
                            <asp:Label ID="lblSubtotalValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblIVA" runat="server" Text="IVA =" CssClass="item" Font-Bold="True"></asp:Label>&nbsp;
                            <asp:Label ID="lblIVAValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td width="16%" align="right" style="width: 32%">
                            <asp:Label ID="lblTotal" runat="server" Text="Total =" CssClass="item" Font-Bold="True" Font-Size="20px"></asp:Label>&nbsp;
                            <asp:Label ID="lblTotalValue" runat="server" CssClass="item" Font-Bold="False" Font-Size="20px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnFinalizar" runat="server" Text="Cerrar" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="5%">&nbsp;</td>
                    </tr>
                </table>

            </fieldset>
        </asp:Panel>
        <telerik:RadWindowManager ID="rwAlerta" runat="server" CenterIfModal="true" Modal="true" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight" Behaviors="Close"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
