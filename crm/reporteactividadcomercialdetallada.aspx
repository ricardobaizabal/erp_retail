<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="reporteactividadcomercialdetallada.aspx.vb" Inherits="LinkiumCFDI.reporteactividadcomercialdetallada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
        <br />
        <fieldset>
            <legend>
                <asp:Label ID="lblVacanteBuscadorLegend" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
            </legend>
            <table width="990">
                <tr>
                    <td>
                        <asp:Label ID="lblComercial" runat="server" CssClass="item" Font-Bold="True" Text="Ejecutivo comercial :"></asp:Label>
                    </td>
                      <td>
                        <asp:Label ID="lblCategoria" runat="server" CssClass="item" Font-Bold="True" Text="Categoría:"></asp:Label>
                    </td>                 
                    <td>
                        <asp:Label ID="lblEstatus" runat="server" CssClass="item" Font-Bold="True" Text="Etapa:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDesde" runat="server" CssClass="item" Font-Bold="True" Text="Desde:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblHasta" runat="server" CssClass="item" Font-Bold="True" Text="Hasta:"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:DropDownList ID="cmbUsuario" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                     <td valign="top">
                        <asp:DropDownList ID="cmbCategoria" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>                   
                    <td valign="top">
                        <asp:DropDownList ID="cmbEtapa" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="fha_ini" Runat="server">
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="fha_fin" Runat="server">
                        </telerik:RadDatePicker>
                    </td>
                    <td><asp:Button ID="btnBuscar" runat="server" Text="buscar" /></td>
                </tr>
                
            </table>
            <br />
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblTituloReporte" runat="server" Font-Bold="True" CssClass="item" Text="Reporte de Actividad Comercial (Oportunidades)"></asp:Label>
            </legend>
            <table width="1000">
                <tr>
                    <td style="height: 5px"></td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="oportunidadeslist" runat="server" Width="970px" ShowStatusBar="true"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="100" GridLines="None" ShowFooter="true" 
                            AllowSorting="true" AllowMultiRowSelection="False" Skin="Office2010Silver">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Vacantes" AllowMultiColumnSorting="false" AllowSorting="true" HeaderStyle-Font-Underline="true">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="nombre_comercial" HeaderText="Nombre comercial" UniqueName="nombre_comercial" SortExpression="nombre_comercial">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="etapa" HeaderText="Etapa" UniqueName="etapa">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="vendedor" HeaderText="Ejecutivo comercial " UniqueName="vendedor">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cobertura" HeaderText="Cobertura" UniqueName="cobertura">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="estatus" HeaderText="Tamaño de Emp." UniqueName="estatus">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha" DataFormatString="{0:dd/MM/yyyy}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="tiposervicio" HeaderText="Tipo Serv." UniqueName="tiposervicio">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="contacto" HeaderText="Contacto" UniqueName="contacto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Image ID="imgEmail" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/arrow.gif" Visible="false"  />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Teléfono">
                                        <ItemTemplate>
                                            <asp:Image ID="imgTelefono" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/arrow.gif" Visible="false"  />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="monto" HeaderText="Monto" UniqueName="monto" DataFormatString="{0:c0}" ItemStyle-HorizontalAlign="Right" Aggregate="Sum" FooterAggregateFormatString="{0:c0}" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Comentarios" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ultima_actividad" HeaderText="Ult. Actividad" UniqueName="ultima_actividad">
                                    </telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"
        Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
