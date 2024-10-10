<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_almacen_entradas" CodeBehind="entradas.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .required {
            color: red;
            font-size: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblEntradas" runat="server" Font-Bold="true" CssClass="item" Text="Entradas de Almacén"></asp:Label>
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
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCamposRequeridos" runat="server" Visible="false" Font-Size="Small" Font-Bold="true" ForeColor="Red" CssClass="item" Text="* Campos requeridos."></asp:Label><br />
                        <telerik:RadGrid ID="gridResults" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                            Skin="Office2007" Visible="false">
                            <MasterTableView Width="100%" DataKeyNames="id" NoMasterRecordsText="No se encontraron registros." Name="Items" AllowMultiColumnSorting="False">
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
                                        <HeaderTemplate>Cant.<span class="required">*</span></HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCantidad" runat="server" AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged" Skin="Default" Width="50px" MinValue="0" Value='0'>
                                                <NumberFormat DecimalDigits="4" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Costo Unit.<span class="required">*</span></HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCostoUnitario" runat="server" MinValue="0" Enabled="true" Value="0" Skin="Default" Width="80px">
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
                                        <HeaderTemplate>Sucursal<span class="required">*</span></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbSucursal" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Mov. Ajuste</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMovAjuste" runat="server" Checked="false" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Lote</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtLote" runat="server" Width="80px" CssClass="box"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Documento</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDocumento" runat="server" Width="80px" CssClass="box"></asp:TextBox>
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
                                            <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_add.gif" CausesValidation="False" ToolTip="Agregar entrada de este producto" />
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
                                <MasterTableView Width="100%" DataKeyNames="productoid,combinacionid" Name="Combinaciones" NoMasterRecordsText="No se encontraron registros." AllowMultiColumnSorting="False">
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
                                                <asp:DropDownList ID="cmbSucursal" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Documento</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDocumento" runat="server" Width="80px" CssClass="box"></asp:TextBox>
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
                        <td align="right">
                            <asp:Button ID="btnGuardaEntradas" runat="server" Text="Guardar entradas de almacén" CausesValidation="False" />
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
                                    <telerik:RadDatePicker ID="fechaini" runat="server" Skin="Office2007">
                                        <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False"
                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>Hasta: </td>
                                <td>
                                    <telerik:RadDatePicker ID="fechafin" runat="server" Skin="Office2007">
                                        <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False"
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
                        <telerik:RadGrid ID="productslist" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None" ShowFooter="true"
                            Skin="Office2007">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Products" NoMasterRecordsText="No se encontraron registros." AllowMultiColumnSorting="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="sucursal" HeaderText="Sucursal" UniqueName="sucursal">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="lote_entrada" HeaderText="Lote" UniqueName="lote_entrada">
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
