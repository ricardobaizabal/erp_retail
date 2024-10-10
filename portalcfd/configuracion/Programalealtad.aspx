<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="Programalealtad.aspx.vb" Inherits="LinkiumCFDI.Programalealtad" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                 <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblUsersListLegend" runat="server" Font-Bold="true" CssClass="item">Programa de Lealtad</asp:Label>
            </legend>
            <table width="50%" border="0">
            <tr>
                <td>
                     <asp:Label ID="lblmodulo" runat="server" text="Modulo Activado" visible=true> </asp:Label>   
                     <asp:CheckBox runat=server ID="chbmoduloactivo" />
                </td>
            
            </tr>
            <tr>
                <td>
                     <asp:Label ID="lblaplicavigencia" runat="server" text="Aplica Vigencia" visible=true> </asp:Label>   
                     <asp:CheckBox runat=server ID="chbaplicavigencia" />
                </td>
            
            </tr>
            <tr>
              <td width="33%">
                         <asp:DropDownList ID="ddlfamilia" runat="server" CssClass="item" AutoPostBack=true></asp:DropDownList>
              </td>
            </tr>
               <tr>
                
                    <td style="height: 5px">
                        <telerik:RadGrid ID="topelist" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None" PageSize="15" ShowStatusBar="True"
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id"
                                Name="Unidad" Width="100%">
                                <Columns>
                                    
                                                                                                          
                                      <telerik:GridBoundColumn DataField="subfamilia" HeaderText="SubFamilia" UniqueName="subfamilia">
                                      </telerik:GridBoundColumn>
                                   
                                    
                                    <telerik:GridTemplateColumn HeaderStyle-Font-Bold =true>
                                        <HeaderTemplate>Porcentaje</HeaderTemplate>
                                        <ItemTemplate>
                                           <telerik:RadNumericTextBox ID="txtporcentaje" runat="server" Text='<%# eval("porcentaje_venta") %>' Enabled="true" MinValue="0"  Value="0" Skin="Default" Width="80px">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    
                                    
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAdd1" runat="server" Text="Guardar" CausesValidation="False" CssClass="item" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px"></td>
                </tr>
            </table>
        </fieldset>
        <br />
        <asp:Panel ID="panelRegistration" runat="server" Visible="False">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/AgreEditUsuario_03.jpg" ImageAlign="AbsMiddle" />&nbsp;
                    <asp:Label ID="lblUserEditLegend" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                </legend>

                <br />

                <table width="50%" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="lblNombre" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="Label1" runat="server" CssClass="item" Font-Bold="True" Text="Clave Unidad de Medida SAT"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 35%">
                            <telerik:RadTextBox ID="txtNombre" runat="server" Width="65%">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ForeColor="Red" ControlToValidate="txtNombre" CssClass="item" Text="Requerido"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="claveUnidad" runat="server" CssClass="box" Width="100%"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ControlToValidate="claveUnidad" CssClass="item" Text="Requerido" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="top">
                      <td colspan="3" align="right">
                          <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="item" />&nbsp;<asp:Button ID="btnCancelar" CausesValidation="false" Text="Cancelar" runat="server" CssClass="item" />
                      </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 5px;">
                            <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                            <asp:HiddenField ID="UnidadID" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>