<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="cotizaciones.aspx.vb" Inherits="LinkiumCFDI.cotizaciones" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!-- jQuery library (served from Google) -->
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
               <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <span class="item">
                Razón Social: <asp:TextBox ID="txtSearch" runat="server" CssClass="box"></asp:TextBox>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Buscar" CausesValidation="false" />&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" CausesValidation="false" Text="Ver todo" />
            </span>
            <br /><br />
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProvidersListLegend" runat="server" Text="Listado de Cotizaciones" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" border="0">
                <tr>
                    <td style="height: 5px" colspan="2">
                        <telerik:RadGrid ID="cotizacionesList" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None" 
                            PageSize="20" ShowStatusBar="True" 
                            Skin="Simple" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Cotizaciones" NoMasterRecordsText="No se encontraron registros para mostrar" Width="100%">
                                <Columns>
                                
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Folio" UniqueName="id">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente" UniqueName="cliente">
                                    </telerik:GridBoundColumn>
                                    
                                    <telerik:GridBoundColumn DataField="fecha_alta" HeaderText="Fecha Alta" UniqueName="fecha_alta" DataFormatString="{0:dd/MM/yyyy}">
                                    </telerik:GridBoundColumn>
                                    
                                    <telerik:GridBoundColumn DataField="condiciones" HeaderText="Condiciones" UniqueName="condiciones">
                                    </telerik:GridBoundColumn>
                                    
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Descargar" UniqueName="Download">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDownload" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDownload" CausesValidation="false" ImageUrl="~/portalcfd/images/download.png" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Enviar" UniqueName="Send">
                                        <ItemTemplate>
                                            <asp:ImageButton id="imgSend" runat="server" ImageUrl="~/portalcfd/images/envelope.jpg" CommandArgument='<%# Eval("id") %>' CommandName="cmdSend" CausesValidation="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" CausesValidation="false" ImageUrl="~/images/action_delete.gif" />
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
                    <td>
                        <div id="dmensaje" style="display:none" class="div">
                            <asp:Label ID="lblMensajeGuardar" runat="server" CssClass="item" ForeColor="#ffffff" Font-Bold="true" Font-Size="Small"></asp:Label>
                        </div>
                    </td>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAgregarCotizacion" runat="server" Text="Agregar Cotización" CausesValidation="False" />
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
                <asp:Image ID="Image3" runat="server" ImageUrl="~/images/icons/AgregarEditarProveedor_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblEditarLeyendaOportunidad" Text="Agregar/Editar Pedido - Cotización" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table border="0" width="100%">
                <tr style="height:25px;">
                    <td width="40%" colspan="2">
                        <asp:Label ID="lblCliente" runat="server" Text="Seleccione un cliente:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                </tr>
                <tr style="height:25px;">
                    <td width="40%" colspan="2">
                        <asp:DropDownList ID="cmbCliente" runat="server" Width="90%" AutoPostBack="false"></asp:DropDownList>
                    </td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                    <td width="20%">&nbsp;</td>
                </tr>
                <tr style="height:25px;">
                    <td width="40%" colspan="2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" InitialValue="0" ControlToValidate="cmbCliente" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr style="height:25px;">
                    <td colspan="5">
                        <asp:Label ID="lblComentarios" runat="server" Text="Comentarios:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <telerik:RadTextBox ID="txtComentarios" TextMode="MultiLine" Runat="server" Width="60%" Height="50px"></telerik:RadTextBox>
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
                        <asp:HiddenField ID="CotizacionID" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="panelProductosCotizados" runat="server" Visible="False">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image6" runat="server" ImageUrl="~/images/icons/ConfiguraCertificados_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProductosCotizados" Text="Productos Cotizados" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
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
            <fieldset style="padding:10px;">
                <asp:HiddenField ID="productoid" runat="server" />
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/portalcfd/images/concept.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientItems" Text="Conceptos" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table width="900" cellspacing="0" cellpadding="1" border="0" align="center">
                    <tr>
                        <td  valign="bottom" class="item">
                            <strong>Buscar:</strong> <asp:TextBox ID="txtSearchItem" runat="server" CssClass="box" AutoPostBack="true"></asp:TextBox>&nbsp;presione enter después de escribir el código
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <telerik:RadGrid ID="gridResults" runat="server" Width="100%" ShowStatusBar="True"
                                AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                                Skin="Simple" Visible="False">
                                <MasterTableView Width="100%" DataKeyNames="productoid" Name="Items" AllowMultiColumnSorting="False" NoMasterRecordsText="No se encontraron registros para mostrar">
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Código</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodigo" runat="server" Text='<%# eval("codigo") %>'></asp:Label>
                                                <asp:Label ID="lblPresentacionId" Visible="false" runat="server" Text='<%# eval("presentacionid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Descripción</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# eval("descripcion") %>' Width="300px"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>U. Medida</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnidad" runat="server" Text='<%# eval("unidad") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Cantidad</HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Skin="Default" Width="50px" MinValue="0" Text='<%# eval("cantidad") %>'>
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia" ItemStyle-HorizontalAlign="Right" >
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center"
                                            UniqueName="Add">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("productoid") %>' CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_add.gif" CausesValidation="False" ToolTip="Agregar producto comoo partida" />
                                            </ItemTemplate>
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
                    <td colspan="2">
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
                                    <telerik:GridBoundColumn DataField="precio" HeaderText="Precio" UniqueName="precio" DataFormatString="{0:C}" >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="importe" HeaderText="Importe" UniqueName="importe" DataFormatString="{0:C}" >
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
                    <td colspan="2" style="height:30px">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblInfoIva" Text="**Precios mas IVA" runat="server" CssClass="item"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height:20px">&nbsp;</td>
                </tr>
                </table>

            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="panelResumen" runat="server" Visible="False">
            <fieldset style="padding:10px;">
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/portalcfd/images/resumen.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblResumen" Text="Resumen" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>

                <table width="100%" align="left">
                    <%--<tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblSubtotal" runat="server" Text="Sub Total =" CssClass="item" Font-Bold="True"></asp:Label>&nbsp;
                            <asp:Label ID="lblSubtotalValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="Label1" runat="server" Text="IVA =" CssClass="item" Font-Bold="True"></asp:Label>&nbsp;
                            <asp:Label ID="Label2" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblIVA" runat="server" Text="IVA =" CssClass="item" Font-Bold="True"></asp:Label>&nbsp;
                            <asp:Label ID="lblIVAValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblTotal" runat="server" Text="Total =" CssClass="item" Font-Bold="True"></asp:Label>&nbsp;
                            <asp:Label ID="lblTotalValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>                    
                    <tr>
                        <td align="left" width="5%">&nbsp;</td>
                    </tr>
                </table>

            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>
</asp:Content>
