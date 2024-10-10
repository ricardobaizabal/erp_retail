<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_almacen_ajustes" CodeBehind="ajustes.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblEntradas" runat="server" Font-Bold="true" CssClass="item" Text="Ajustes de Almacen"></asp:Label>
            </legend>
            <br />
            <table border="0" cellpadding="2" cellspacing="0" align="center" width="100%">
                <tr>
                    <td class="item">
                        <asp:Panel ID="panelBusqueda" runat="server" DefaultButton="btnSearch">
                            Escriba el código o alguna palabra clave para encontrar el producto:<br />
                            <br />
                            <br />
                            <asp:TextBox ID="txtSearch" Width="250px" runat="server" CssClass="box"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Buscar" CausesValidation="false" /><br />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="gridResults" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                            Skin="Office2007" Visible="false">
                            <MasterTableView Width="100%" DataKeyNames="id" NoMasterRecordsText="No hay registros que mostrar." Name="Items" AllowMultiColumnSorting="False">
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
                                        <HeaderTemplate>Costo Unit.</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCostoUnitario" runat="server" MinValue="0" Enabled="false" Value="0" Skin="Default" Width="80px">
                                                <NumberFormat DecimalDigits="4" GroupSeparator="," />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Importe</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtImporte" runat="server" Text='<%# eval("costo_estandar") %>' Enabled="false" MinValue="0" Value="0" Skin="Default" Width="80px">
                                                <NumberFormat DecimalDigits="4" GroupSeparator="," />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Sucursal</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbSucursal" AutoPostBack="true" OnSelectedIndexChanged="cmbSucursalAlmacen_SelectedIndexChanged" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Existencia</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblExistencia" runat="server" Text="0"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Comentario</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtComentario" TextMode="MultiLine" runat="server" CssClass="box" Width="200px" Height="50px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Add">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_minus.png" CausesValidation="False" ToolTip="Agregar entrada de este producto" />
                                            <asp:LinkButton ID="lnkCombinaciones" runat="server" Text="Combinaciones" CommandArgument='<%# Eval("id") %>' CommandName="cmdCombinaciones"></asp:LinkButton>
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
                                <MasterTableView Width="100%" DataKeyNames="productoid, combinacionid" Name="Combinaciones" NoMasterRecordsText="No hay registros que mostrar." AllowMultiColumnSorting="False">
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
                                                <telerik:RadNumericTextBox ID="txtCostoUnitario" runat="server" MinValue="0" Enabled="false" Value="0" Skin="Default" Width="80px">
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
                                                <asp:DropDownList ID="cmbSucursal" AutoPostBack="true" OnSelectedIndexChanged="cmbSucursal_SelectedIndexChanged" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Existencia</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblExistencia" runat="server" Text="0"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Comentario</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtComentario" TextMode="MultiLine" runat="server" CssClass="box" Width="200px" Height="50px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Add">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("combinacionid") & "|" & Eval("productoid") %>' CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_minus.png" CausesValidation="False" ToolTip="Agregar salida de este producto" />
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
                        <td align="right">
                            <asp:Button ID="btnGuardaAjustes" runat="server" Text="Guardar ajustes de almacén" CausesValidation="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblFiltrosTitulo" runat="server" Text="Seleccione el rango de fechas que desee consultar:" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item">
                        <table>
                            <tr>
                                <td>Sucursal: </td>
                                <td>
                                    <asp:DropDownList ID="cmbSucursalFiltro" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>Desde: </td>
                                <td>
                                    <telerik:RadDatePicker ID="fechaini" runat="server" Skin="Web20">
                                        <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>hasta: </td>
                                <td>
                                    <telerik:RadDatePicker ID="fechafin" runat="server" Skin="Web20">
                                        <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:Button ID="btnGenerate" runat="server" Text="Consultar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%">
                <tr>
                    <td style="height: 10px">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="productslist" runat="server" Width="100%" ShowStatusBar="true" ShowFooter="true"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None"
                            Skin="Office2007">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Products" NoMasterRecordsText="No hay registros que mostrar." AllowMultiColumnSorting="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="costo_unitario" HeaderText="Costo unitario" UniqueName="costo_unitario" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="importe" HeaderText="Importe" UniqueName="importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
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
                    <td>&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
        <telerik:RadWindowManager ID="RadAlert" runat="server" Skin="Office2007" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" ReloadOnShow="true" RenderMode="Lightweight"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
