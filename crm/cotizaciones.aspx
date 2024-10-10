<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="cotizaciones.aspx.vb" Inherits="LinkiumCFDI.CRMcotizaciones" MaintainScrollPositionOnPostback="true" %>

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
    </script>
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if (arguments.get_eventTarget().indexOf("btnDownload") > -1) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">--%>
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
        <asp:Button ID="btnSearch" runat="server" CssClass="boton" Text="Buscar" CausesValidation="false" />&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" CssClass="boton" CausesValidation="false" Text="Ver todo" />
        <br />
        <br />
    </fieldset>
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProvidersListLegend" runat="server" Text="Listado de Cotizaciónes" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%" border="0">
            <tr>
                <td style="height: 5px" colspan="2">
                    <telerik:RadGrid ID="cotizacionesList" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" GridLines="None"
                        PageSize="20" ShowStatusBar="True"
                        Width="100%">
                        <PagerStyle Mode="NumericPages" />
                        <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Cotizaciones" NoMasterRecordsText="No se encontraron cotizaciones para mostrar" Width="100%">
                            <Columns>

                                <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Folio" UniqueName="id">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente/Prospecto" UniqueName="cliente">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="fecha_alta" HeaderText="Fecha Alta" UniqueName="fecha_alta" DataFormatString="{0:dd/MM/yyyy}">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="moneda" HeaderText="Moneda" UniqueName="moneda">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="condiciones" HeaderText="Condiciones" UniqueName="condiciones">
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Descargar anexo" UniqueName="Download">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDownload" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDownload" ImageUrl="~/images/download.png" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Enviar" UniqueName="Send">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgSend" runat="server" ImageUrl="~/images/envelope.jpg" CommandArgument='<%# Eval("id") %>' CommandName="cmdSend" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
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
                    <asp:Button ID="btnAgregarCotizacion" runat="server" Text="Agregar Cotizacion" CausesValidation="False" CssClass="item" TabIndex="6" />
                </td>
            </tr>
            <tr>
                <td style="height: 2px" colspan="2">&nbsp;</td>
            </tr>
        </table>
    </fieldset>
    <br />
    <asp:Panel ID="panelRegistroCotizacion" runat="server" Visible="False">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image3" runat="server" ImageUrl="~/images/AgregarEditarProveedor_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblEditarLeyendaOportunidad" Text="Agregar/Editar Cotización" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table border="0" width="100%">
                <tr style="height: 25px;">
                    <td>
                        <asp:Label ID="lblClienteProspecto" runat="server" Text="Seleccione:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="20%">
                        <asp:Label ID="lblPorcentajeDescuento" runat="server" Text="Porcentaje Descuento:" CssClass="item" Font-Bold="True" Visible="false"></asp:Label>
                    </td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                </tr>
                <tr style="height: 25px;">
                    <td width="20%">
                        <asp:RadioButtonList ID="rblClienteProspecto" CssClass="item" RepeatDirection="Horizontal" AutoPostBack="true" runat="server">
                            <asp:ListItem Text="Cliente" Selected="True" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Prospecto" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td width="20%">
                        <telerik:RadNumericTextBox ID="txtPorcentajeDescuento" runat="server" Width="100px" MinValue="0" MaxValue="100" Type="Percent" Visible="false"></telerik:RadNumericTextBox>
                    </td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                </tr>
                <tr style="height: 25px;">
                    <td width="20%">
                        <asp:DropDownList ID="cmbClienteProspecto" runat="server" Width="200px" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr style="height: 25px;">
                    <td width="20%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbClienteProspecto" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr style="height: 25px;">
                    <td width="20%">
                        <asp:Label ID="lblOportunidad" runat="server" Text="Oportunidad:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr style="height: 25px;">
                    <td width="20%">
                        <asp:DropDownList ID="cmbOportunidades" runat="server" Width="200px" AutoPostBack="false" CssClass="box"></asp:DropDownList>
                    </td>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr style="height: 25px;">
                    <td colspan="5">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbOportunidades" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="height: 25px;">
                    <td width="20%">
                        <asp:Label ID="lblCondiciones" runat="server" Text="Condiciones de pago:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="20%">
                        <asp:Label ID="lblMonto" runat="server" Text="Monto:" CssClass="item" Font-Bold="True"></asp:Label></td>

                    <td width="20%">
                        <asp:Label ID="lblMoneda" runat="server" Text="Moneda:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="20%">
                        <asp:Label ID="lblTipoCambio" runat="server" Text="Tipo de cambio:" CssClass="item" Font-Bold="True" Visible="false"></asp:Label>
                    </td>
                    <td width="20%">&nbsp;</td>
                </tr>
                <tr style="height: 25px;">
                    <td width="20%">
                        <asp:DropDownList ID="cmbCondiciones" runat="server" Width="200px" CssClass="box"></asp:DropDownList>
                    </td>
                    <td width="20%">
                        <telerik:RadNumericTextBox ID="txtMonto" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox></td>


                    <td width="20%">
                        <asp:DropDownList ID="cmbMoneda" runat="server" AutoPostBack="true" Width="200px" CssClass="box"></asp:DropDownList>
                    </td>
                    <td width="20%">
                        <telerik:RadNumericTextBox ID="txtTipoCambio" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="4" MaxLength="9" Visible="false"></telerik:RadNumericTextBox>
                    </td>
                    <td width="20%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="20%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbCondiciones" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td width="20%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbMoneda" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                </tr>
                <tr style="height: 25px;">
                    <td width="20%">
                        <asp:Label ID="lblComentarios" runat="server" Text="Comentarios:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <telerik:RadTextBox ID="txtComentarios" TextMode="MultiLine" runat="server" Width="60%" Height="50px"></telerik:RadTextBox>
                    </td>
                </tr>


                <tr style="height: 25px;">
                    <td width="20%">
                        <asp:Label ID="lblAnexo" runat="server" Text="Archivo anexo:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:FileUpload ID="cotizacionAnexo" runat="server" />&nbsp;<asp:LinkButton ID="lnkDownload" runat="server"></asp:LinkButton>&nbsp;<asp:LinkButton ID="lnkDeleteFile" runat="server" Text="[ borrar anexo ]" Visible="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td valign="bottom" colspan="5">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="item" />&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="item" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                        <asp:HiddenField ID="CotizacionID" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <br />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" BorderStyle="None" BorderWidth="0px" VisibleStatusbar="True" VisibleTitlebar="False">
        <Windows> 
            <telerik:RadWindow ID="RadWindow1" runat="server" ShowContentDuringLoad="False" Modal="True" ReloadOnShow="True" VisibleStatusbar="False" VisibleTitlebar="True" BorderStyle="None" BorderWidth="0px" Behaviors="Close" Width="600px" Height="500px" Skin="Simple">
                <ContentTemplate>
                    <table style="width:95%; height:100%;" align="center" cellpadding="0" cellspacing="3" border="0">
                        <tr>
                            <td colspan="2" style="height:10px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:10%"> 
                                <asp:Label ID="lblFrom" runat="server" Font-Bold="true" CssClass="item" Text="De:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFrom" runat="server" Width="100%" Enabled="false" CssClass="box"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%"> 
                                <asp:Label ID="lblTo" runat="server" Font-Bold="true" CssClass="item" Text="Para:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" runat="server" Width="100%" CssClass="box"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                <asp:Label ID="lblCC" runat="server" Font-Bold="true" CssClass="item" Text="CC:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCC" runat="server" Width="100%" CssClass="box"></asp:TextBox>
                                <br /><span style="color:#FF0000">* Los emails deben ser separados por coma (,) o punto y coma(;).</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%"> 
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
                            <td colspan="2" style="height:15px">
                                <asp:Label ID="lblMensajeEmail" runat="server" style="color:#FF0000" Font-Bold="true" CssClass="item"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Button ID="btnSendEmail" runat="server" CssClass="boton" Width="100px" Height="30px" Text="Enviar" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:10px">
                                <asp:HiddenField id="tempcfdid" runat="server" Value="0"/>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
        </telerik:RadWindowManager>
    <%--  </telerik:RadAjaxPanel>--%>
</asp:Content>
