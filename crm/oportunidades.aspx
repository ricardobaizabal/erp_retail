<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="oportunidades.aspx.vb" Inherits="LinkiumCFDI.oportunidades" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .div {
            border: 1px solid #a1a1a1;
            padding: 10px 40px;
            background: #e4e4e4;
            width: 300px;
            min-width: 300px;
            border-radius: 15px;
        }
    </style>
    <script type="text/javascript">
        function mensaje() {
            $('#dmensaje').slideToggle(2500);
        }
        function mensajefechas() {
            $('#dmensajefechas').slideToggle(2500);
        }
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("lnkEdit") > -1) || (arguments.get_eventTarget().indexOf("btnDownload") > -1)) {
                arguments.set_enableAjax(false);
            }
        }


        $(document).ready(function () {
            // Date Object
            $('#calFechaCierre').datepicker({
                dateFormat: "yy-mm-dd",
                minDate: 2
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
  <%--  <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">--%>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <span class="item">Palabra clave:
                <asp:TextBox ID="txtSearch" runat="server" CssClass="box"></asp:TextBox>&nbsp;
            </span>
            <span class="item">Ejecutivo:
                <asp:DropDownList ID="cmbResponsable" runat="server" Width="190px" AutoPostBack="true"></asp:DropDownList>
            </span>
            <%--  --%>
            <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="boton" Text="Buscar" />&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" CausesValidation="false" CssClass="boton" Text="Ver todo" />
            <br />
            <br />
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProvidersListLegend" runat="server" Text="Listado de Oportunidades" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                        <telerik:RadGrid ID="oportunidadesList" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None"
                            PageSize="20" ShowStatusBar="True"
                            Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Oportunidades" NoMasterRecordsText="No se encontraron oportunidades para mostrar" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="" UniqueName="descripcion">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" ImageUrl="~/images/action_edit.png"  CausesValidation="false"></asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="vendedor" HeaderText="Ejecutivo" UniqueName="cliente">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente/Prospecto" UniqueName="cliente">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Categoria" HeaderText="Categoría" UniqueName="categoria">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fecha_alta" HeaderText="Fecha Alta" UniqueName="fecha_alta" DataFormatString="{0:dd/MM/yyyy}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="etapa" HeaderText="Etapa" UniqueName="etapa">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="probabilidad" HeaderText="Probabilidad" UniqueName="probabilidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="monto" HeaderText="Monto" UniqueName="monto" DataFormatString="{0:C}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="moneda" HeaderText="Moneda" UniqueName="moneda">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fecha_cierre" HeaderText="Fecha Cierre" UniqueName="fecha_cierre" DataFormatString="{0:dd/MM/yyyy}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
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
                    <td style="height: 2px">&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAgregarOportunidad" runat="server" Text="Agregar Oportunidad" CausesValidation="False" CssClass="item" TabIndex="6" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
        <asp:Panel ID="panelRegistroOportunidad" runat="server" Visible="false">
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Simple" CausesValidation="false" MultiPageID="RadMultiPage1">
                <Tabs>
                    <telerik:RadTab Text="Datos generales" Selected="true" Visible="true" PageViewID="DatosGeneralesView"></telerik:RadTab>
                    <telerik:RadTab Text="Seguimiento" PageViewID="DatosSeguimiento"></telerik:RadTab>
                    <telerik:RadTab Text="Cotizaciones" PageViewID="DatosCotizaciones"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server">
                <telerik:RadPageView ID="DatosGeneralesView" runat="server" Selected="true">
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/AgregarEditarProveedor_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblEditarLeyendaOportunidad" Text="Agregar/Editar Oportunidad" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table border="0" width="100%">
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblCategoria" runat="server" Text="Categoría:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="width: 60%">
                                    <%--<asp:DropDownList ID="cmdCategoria" runat="server" CssClass="item"></asp:DropDownList>--%>
                                    <telerik:RadComboBox ID="cmdCategorias" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Localization-CheckAllString="Seleccionar todo"  Width="200px"></telerik:RadComboBox>
                                </td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="width: 60%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="cmdCategorias" InitialValue="0" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                            </tr>




                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblDescripcion" runat="server" Text="Comentarios:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="width: 60%">
                                    <telerik:RadTextBox ID="txtDescripcion" TextMode="MultiLine" runat="server" Width="100%" Height="50px">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="width: 60%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="txtDescripcion" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblClienteProspecto" runat="server" Text="Seleccione un cliente o prospecto:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="20%">
                                    <asp:Label ID="lblMonto" runat="server" Text="Monto aproximado de venta:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="20%">
                                    <asp:Label ID="lblMoneda" runat="server" Text="Moneda:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="left" width="20%">
                                    <asp:Label ID="lblProbabilidad" runat="server" Text="Probabilidad de venta:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">
                                    <asp:RadioButtonList ID="rblClienteProspecto" CssClass="item" RepeatDirection="Horizontal" AutoPostBack="true" runat="server">
                                        <asp:ListItem Text="Cliente" Selected="True" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Prospecto" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20%">
                                    <asp:DropDownList ID="cmbClienteProspecto" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>
                                </td>
                                <td width="20%">
                                    <telerik:RadNumericTextBox ID="txtMonto" runat="server" Type="Currency" Value="0" MinValue="0" NumberFormat-DecimalDigits="0" MaxLength="10"></telerik:RadNumericTextBox>
                                </td>
                                <td width="20%">
                                    <asp:DropDownList ID="cmbMoneda" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>
                                </td>
                                <td width="20%">
                                    <asp:DropDownList ID="cmbProbabilidad" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbClienteProspecto" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="20%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Proporcione un monto aproximado" ControlToValidate="txtMonto" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="20%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbMoneda" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="20%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbProbabilidad" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblVendedor" runat="server" Text="Vendedor:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="20%">
                                    <asp:Label ID="lblFormaContacto" runat="server" Text="Forma de contacto:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="left" width="20%">
                                    <asp:Label ID="lblFechaCierre" runat="server" Text="Fecha estimada de cierre:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="20%">
                                    <asp:Label ID="lblEtapa" runat="server" Text="Etapa:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="20%">
                                    <%--<asp:Label ID="lblComision" runat="server" Text="Comisión:" CssClass="item" Font-Bold="True"></asp:Label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">
                                    <asp:DropDownList ID="cmbVendedor" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>
                                </td>
                                <td width="20%">
                                    <asp:DropDownList ID="cmbFormaContacto" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>
                                </td>
                                <td width="20%">
                                    <telerik:RadDatePicker ID="calFechaCierre" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"    >
                                        <%--<DateInput runat="server" EnableSmartParsing="true"/>--%>
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="20%">
                                    <asp:DropDownList ID="cmbEtapas" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>
                                </td>
                                <td width="20%">
<%--                                    <telerik:RadNumericTextBox ID="txtComision" runat="server" Value="0" MinValue="0" Type="Percent" NumberFormat-DecimalDigits="0" MaxLength="2"></telerik:RadNumericTextBox>--%>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbVendedor" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="20%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbFormaContacto" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="20%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="calFechaCierre" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="20%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbEtapas" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="20%">
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="txtComision" CssClass="item"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" colspan="5">
                                    <asp:Button ID="btnGuardar" runat="server" CausesValidation="true" Text="Guardar" CssClass="item" />&nbsp;                                
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancelar" CssClass="item" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                                    <asp:HiddenField ID="OportunidadID" runat="server" Value="0" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </telerik:RadPageView>
                <telerik:RadPageView ID="DatosSeguimiento" runat="server" Selected="false">
                    <fieldset>
                        <br />
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/images/AgregarEditarProveedor_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="Label1" Text="Agregar Entrada Seguimiento" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table border="0" width="100%">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblTituloOportunidadSeguimiento" runat="server" Text="Tipo Seguimiento:" CssClass="item" Font-Bold="True"></asp:Label><br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblTipoSeguimiento" runat="server" Text="Tipo Seguimiento:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                               
                            </tr>
                           <tr>
                                <td colspan="3" style="width: 60%">
                                    <asp:DropDownList ID="cmbTipoSeguimiento" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="gpoSeguimiento" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbTipoSeguimiento" CssClass="item"></asp:RequiredFieldValidator>
                                    <asp:Label ID="Label4" runat="server" Text="Se concidera actividad de negociación:" CssClass="item" Font-Bold="True"></asp:Label>
                                 <asp:CheckBox ID="ChkNegociacion" runat="server" CssClass="item" Font-Bold="True"></asp:CheckBox>
                                </td>
                                <td width="20%">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                           </tr>

                           <tr style="height:10px;">
                             <td colspan="7">&nbsp;</td>
                           </tr>
                           <tr style="height:10px;">
                             <td colspan="7">&nbsp;</td>
                           </tr>
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblInicio" runat="server" Text="Inicio:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="10%">&nbsp;</td>
                            </tr>
                            <tr>
                                 <td width="90%">
                                    <telerik:RadDateTimePicker ID="calFechaInicio" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy hh:mm:ss" runat="server">
                                        <Calendar ID="Calendar1" runat="server" EnableKeyboardNavigation="true">
                                        </Calendar>
                                        <TimeView Interval="00:15:00" StartTime="07:00" EndTime="23:00" runat="server">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>&nbsp;
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="gpoSeguimiento" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="calFechaInicio" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="height:10px;">
                             <td colspan="7">&nbsp;</td>
                           </tr>
<%--                            <tr>
                                <td width="10%">
                                    <asp:Label ID="lblFin" runat="server" Text="Fin:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <telerik:RadDateTimePicker ID="calFechaFin" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy hh:mm:ss" runat="server">
                                        <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                                        </Calendar>
                                        <TimeView Interval="00:15:00" StartTime="07:00" EndTime="23:00" runat="server">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="gpoSeguimiento" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="calFechaFin" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                            <tr style="height:10px;">
                             <td colspan="7">&nbsp;</td>
                           </tr>
                            <tr valign="top">
                                <td width="10%">
                                    <asp:Label ID="lblDescripcionSeguimiento" runat="server" Text="Descripcion:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtDescripcionSeguimiento" TextMode="MultiLine" runat="server" Width="100%" Height="100px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr style="height: 40px">
                                <td colspan="2" >
                                    <div id="dmensajefechas" style="display: none; width:60%;" class="div">
                                        <asp:Label ID="lblMensajeFechas" runat="server" CssClass="item" ForeColor="#ffffff" Font-Bold="true" Font-Size="Small"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr style="height: 40px">
                                <td colspan="2" style="text-align: right;">
                                    <asp:Button ID="btnGuardarSeguimiento" runat="server" CausesValidation="true" ValidationGroup="gpoSeguimiento" Text="Guardar" CssClass="item" />&nbsp;
                                <asp:Button ID="btnCancelSeguimiento" runat="server" CausesValidation="false" Text="Cancelar" CssClass="item" />
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <telerik:RadGrid ID="seguimientoList" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" GridLines="None"
                                        PageSize="20" ShowStatusBar="True"
                                        Skin="Simple" Width="100%">
                                        <PagerStyle Mode="NumericPages" />
                                        <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Seguimiento" NoMasterRecordsText="No se encontraron movimientos para mostrar" Width="100%">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="tipo_seguimiento" HeaderText="Tipo Seguimiento" UniqueName="tipo_seguimiento">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="cliente_prospecto" HeaderText="Cliente/Prospecto" UniqueName="cliente_prospecto">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="fecha_inicio" HeaderText="Fecha Inicio" UniqueName="fecha_inicio" DataFormatString="{0:g}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="nombre" HeaderText="Ejecutivo Comercial" UniqueName="nombre" DataFormatString="{0:g}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Negociación" UniqueName="">
                                                    <ItemTemplate>
                                                         <asp:Label ID="lblTimbrado" runat="server"></asp:Label>
                                                         <asp:Image ID="imgNegociacion" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/arrow.gif" Visible="false" />
                                                    </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td colspan="2">&nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                </telerik:RadPageView>
                <telerik:RadPageView ID="DatosCotizaciones" runat="server" Selected="false">
                    <fieldset>
                        <br />
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Image ID="Image6" runat="server" ImageUrl="~/images/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="Label3" runat="server" Text="Listado de Cotizaciones de Oportunidad" Font-Bold="true" CssClass="item"></asp:Label>
                        </legend>
                        <table width="100%" border="0">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblTituloOportunidadCotizaciones" runat="server" Text="Tipo Seguimiento:" CssClass="item" Font-Bold="True"></asp:Label><br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="2">
                                    <telerik:RadGrid ID="cotizacionesList" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" GridLines="None"
                                        PageSize="20" ShowStatusBar="True"
                                        Skin="Simple" Width="100%">
                                        <PagerStyle Mode="NumericPages" />
                                        <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Cotizaciones" NoMasterRecordsText="No se encontraron cotizaciones para mostrar" Width="100%">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente/Prospecto" UniqueName="cliente">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="fecha_alta" HeaderText="Fecha Alta" UniqueName="fecha_alta" DataFormatString="{0:dd/MM/yyyy}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="moneda" HeaderText="Moneda" UniqueName="moneda">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="condiciones" HeaderText="Condiciones" UniqueName="condiciones">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Descargar" UniqueName="Download">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDownload" runat="server" CommandArgument='<%# Eval("id") %>' CausesValidation="false" CommandName="cmdDownload" ImageUrl="~/images/download.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Enviar" UniqueName="Send">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgSend" runat="server" ImageUrl="~/images/envelope.jpg" CommandArgument='<%# Eval("id") %>' CausesValidation="false" CommandName="cmdSend" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartidas" Text='<%# Eval("total_partidas") %>' runat="server" Font-Bold="true" CssClass="item"></asp:Label>
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
                                <td colspan="2">
                                    <div id="dmensaje" style="display: none" class="div">
                                        <asp:Label ID="lblMensajeGuardar" runat="server" CssClass="item" ForeColor="#ffffff" Font-Bold="true" Font-Size="Small"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 2px" colspan="2">&nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
