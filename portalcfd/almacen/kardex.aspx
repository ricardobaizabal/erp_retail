<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_almacen_kardex" Codebehind="kardex.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">        
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
               <asp:Label ID="lblEntradas" runat="server" Font-Bold="true" CssClass="item" Text="Kardex de Producto"></asp:Label>
            </legend>
            <br />
            <table border="0" cellpadding="2" cellspacing="0" align="center" width="100%">
                <tr>
                    <td class="item">
                        <asp:Panel ID="panelBusqueda" runat="server" DefaultButton="btnSearch">
                            <table border="0" cellpadding="2" cellspacing="0" align="center" width="100%">
                                <tr>
                                    <td class="item" style="width: 17%">
                                        <asp:Label ID="lblSucursal" runat="server" Font-Bold="true" CssClass="item" Text="Sucursal:"></asp:Label>
                                    </td>
                                    <td class="item" style="width: 25%">
                                        <asp:DropDownList ID="cmbSucursal" runat="server" Width="90%"></asp:DropDownList>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="item" style="width: 17%">
                                        <asp:Label ID="lblCodigo" runat="server" Font-Bold="true" CssClass="item" Text="Código del producto:"></asp:Label>
                                    </td>
                                    <td class="item" style="width: 25%">
                                        <asp:TextBox ID="txtSearch" width="90%" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:button ID="btnSearch" runat="server" Text="Ver Kardex" Width="90px" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <telerik:RadGrid ID="gridResults" runat="server" Width="60%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                            Skin="Office2007" Visible="false">
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Items" AllowMultiColumnSorting="False">
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
                                    <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Add">
                                        <ItemTemplate>
                                            <asp:LinkButton id="lnkview" runat="server" CommandArgument ='<%# Eval("id") %>' CommandName="cmdView" Text="Ver Kardex"></asp:LinkButton>
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
            </table>
            <br />
        </fieldset>
        <br />
        <asp:Panel runat="server" ID="panelKardex" Visible="false">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblProductsListLegend" Text="Kardex del producto seleccionado" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <asp:HiddenField ID="ProductoID" Value="0" runat="server" />
            <table width="100%" border="0">
                <tr>
                    <td colspan="5" style="height: 5px">
                    </td>
                </tr>
                <tr class="item">
                    <td style="width:5%">Desde:</td>
                    <td style="width:10%">
                        <telerik:RadDatePicker ID="fechaini" runat="server" Skin="Office2007">
                            <Calendar ID="Calendar1" runat="server" Skin="Simple" UseColumnHeadersAsSelectors="False" 
                                UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </td>
                    <td style="width:5%">Hasta:</td>
                    <td style="width:10%">
                        <telerik:RadDatePicker ID="fechafin" runat="server" Skin="Simple">
                            <Calendar ID="Calendar2" runat="server" Skin="Office2007" UseColumnHeadersAsSelectors="False" 
                                UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </td>
                    <td style="width:70%">
                        <asp:button ID="btnBuscarKardex" runat="server" Text="Buscar" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <telerik:RadGrid ID="productslist" runat="server" Width="100%" ShowStatusBar="True" 
                            AutoGenerateColumns="False" AllowPaging="False" GridLines="None" Skin="Office2007">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Products" AllowMultiColumnSorting="False" NoMasterRecordsText="No hay registros que mostrar.">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="documento" HeaderText="Factura" UniqueName="documento">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="movimiento" HeaderText="Movimiento" UniqueName="movimiento">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unidad" HeaderText="Unidad" UniqueName="unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="factor" HeaderText="Factor" UniqueName="factor">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="codigo_presentacion" HeaderText="Código" UniqueName="codigo_presentacion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="presentacion" HeaderText="Presentación" UniqueName="presentacion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unidad_presentacion" HeaderText="Unidad" UniqueName="unidad_presentacion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="comentario" HeaderText="Comentarios" UniqueName="comentarios">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>                        
                        <telerik:RadGrid ID="productlistcodsa" runat="server" Width="100%" ShowStatusBar="True" Visible="false"
                            AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                            Skin="Office2007">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Products" AllowMultiColumnSorting="False" NoMasterRecordsText="No hay registros que mostrar.">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="folio">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="documento" HeaderText="Factura" UniqueName="documento">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn DataField="movimiento" HeaderText="Movimiento" UniqueName="movimiento">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="remisionid" HeaderText="Ticket" UniqueName="remisionid">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unidad" HeaderText="Unidad" UniqueName="unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="factor" HeaderText="Factor" UniqueName="factor">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="codigo_presentacion" HeaderText="Código" UniqueName="codigo_presentacion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="presentacion" HeaderText="Presentación" UniqueName="presentacion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unidad_presentacion" HeaderText="Unidad" UniqueName="unidad_presentacion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="comentario" HeaderText="Comentarios" UniqueName="comentarios">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="height: 5px"></td>
                </tr>
            </table>
        </fieldset>
        </asp:Panel>
        <br />    
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Office2007" EnableShadow="false">
        <Localization OK="Aceptar" Cancel="Cancelar" />
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>