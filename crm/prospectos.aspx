<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="prospectos.aspx.vb" Inherits="LinkiumCFDI.prospectos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
               <asp:Image ID="Image1" runat="server" ImageUrl="~/images/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <span class="item">
                Palabra clave: <asp:TextBox ID="txtSearch" runat="server" CssClass="box"></asp:TextBox>&nbsp;
            <asp:Button ID="btnSearch" runat="server" CssClass="boton" Text="Buscar" />&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" CssClass="boton" Text="Ver todo" />
            </span>
            <br /><br />
        </fieldset>
        <br />
        
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProvidersListLegend" runat="server" Text="Listado de Prospectos" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                        <telerik:RadGrid ID="prospectosList" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" GridLines="None" 
                            PageSize="20" ShowStatusBar="True" 
                             Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Prospectos" NoMasterRecordsText="No se encontraron prospectos para mostrar" Width="100%"  AllowSorting="true" HeaderStyle-Font-Underline="false">
                                <Columns>
                                    <telerik:GridTemplateColumn DataField="id" HeaderText="Folio" UniqueName="id">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Nombre Comercial" UniqueName="razonsocial" SortExpression="nombre_comercial" DataField="razonsocial" >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="contacto" HeaderText="Contacto" UniqueName="contacto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="telefono_contacto" HeaderText="Télefono" UniqueName="telefono_contacto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="tipoempresa" HeaderText="Tamaño de Empresa" UniqueName="tipoempresa">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cobertura" HeaderText="Cobertura" UniqueName="cobertura">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha de Alta" UniqueName="fecha" DataFormatString="{0:dd/MM/yyyy}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Enviar" UniqueName="" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton id="imgSend" runat="server" ImageUrl="~/images/envelope.jpg" CommandArgument='<%# Eval("id") %>' CommandName="cmdSend"></asp:ImageButton>
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
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAgregarProspecto" runat="server" Text="Agregar Prospecto" CausesValidation="False" CssClass="item" TabIndex="6" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        
        <br />
        
        <asp:Panel ID="panelRegistroProspecto" runat="server" Visible="False">
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Simple" CausesValidation="false" MultiPageID="RadMultiPage1">
                <Tabs>
                    <telerik:RadTab Text="Datos generales" Selected="true" Visible="true" PageViewID="DatosGeneralesView"></telerik:RadTab>
                    <telerik:RadTab Text="Contacto" PageViewID="DatosContacto"></telerik:RadTab>

                </Tabs>
            </telerik:RadTabStrip>

<telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server">
    <telerik:RadPageView ID="DatosGeneralesView" runat="server" Selected="true">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image3" runat="server" ImageUrl="~/images/AgregarEditarProveedor_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblEditarLeyendaProspecto" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table border="0" width="100%">
                <tr>
                    <td width="33%">
                        <asp:Label ID="lblRazonSocial" runat="server" Text=" Nombre Comercial:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="33%" valign="top" colspan="2" style="width: 66%">
                        <telerik:RadTextBox ID="txtRazonSocial" Runat="server" Width="50%">
                        </telerik:RadTextBox>
                    </td>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="33%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="txtRazonSocial" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="33%">
                        <asp:Label ID="lblContacto" runat="server" Text="Contacto:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="33%">
                        <asp:Label ID="lblEmailContacto" runat="server" Text="Email:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="33%">
                        <asp:Label ID="lblTelefonoCOntacto" runat="server" Text="Teléfono:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtContacto" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtEmailContacto" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="txtEmailContacto" CssClass="item" Text="Formato no válido" ValidationExpression=".*@.*\..*"></asp:RegularExpressionValidator>
                    </td>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtTelefonoContacto" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="33%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="txtContacto" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td width="33%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="txtEmailContacto" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="33%">
                        <asp:Label ID="lblCalle" runat="server" Text="Calle" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="33%" align="left">
                        <asp:Label ID="lblNumExterior" runat="server" Text="Num. Ext.:" CssClass="item" Font-Bold="True"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblNumInterior" runat="server" Text="Num. Int.:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td align="left" width="33%">
                        <asp:Label ID="lblColonia" runat="server" Text="Colonia:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtCalle" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtNumExterior" Runat="server" Width="35%">
                        </telerik:RadTextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <telerik:RadTextBox ID="txtNumInterior" Runat="server" Width="35%">
                        </telerik:RadTextBox>
                    </td>
                    <td align="left" width="33%">
                        <telerik:RadTextBox ID="txtColonia" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="33%">
                       
                    </td>
                    <td width="33%">
                       
                    </td>
                    <td align="left" width="33%">
                        
                    </td>
                </tr>
                <tr>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="33%">
                        <asp:Label ID="lblPais" runat="server" Text="País:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="33%">
                        <asp:Label ID="lblEstado" runat="server" Text="Estado:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="33%">
                        <asp:Label ID="lblMunicipio" runat="server" Text="Ciudad:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtPais" Runat="server" Width="85%" Text="México">
                        </telerik:RadTextBox>
                    </td>
                    <td width="33%">
                        <telerik:RadComboBox ID="cmbEstados" Runat="server" AllowCustomText="True" CausesValidation="true" Width="87%">
                        </telerik:RadComboBox>
                    </td>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtMunicipio" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="33%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="txtPais" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td width="33%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="cmbEstados" CssClass="item" InitialValue="--Seleccione--"></asp:RequiredFieldValidator>
                    </td>
                    <td width="33%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtMunicipio" ErrorMessage="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="33%">
                        <asp:Label ID="lblCP" runat="server" Text="Código Postal:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="33%">
                        <asp:Label ID="lblRFC" runat="server" Text="RFC:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtCP" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtRFC" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>                    
                    <td width="33%">&nbsp;</td>
                </tr>


                <tr>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="33%">
                        <asp:Label ID="lblTipoEmpresa" runat="server" Text="Tamaño de Empresa:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="33%">
                        <asp:Label ID="lblCobertura" runat="server" Text="Cobertura:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="33%">
                        <asp:DropDownList ID="cmdTipoEmpresa" runat="server" CssClass="item"></asp:DropDownList>
                    </td>
                    <td width="33%">
                        <asp:DropDownList ID="cmbCobertura" runat="server" CssClass="item"></asp:DropDownList>
                    </td>                    
                    <td width="33%">&nbsp;</td>
                </tr>



                <tr>
                    <td width="33%">
                        
                    </td>
                    <td width="33%">
                       
                    </td>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td width="33%">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td valign="bottom" colspan="3">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="item" />&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="item" CausesValidation="False" />                        
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="width: 66%; height: 5px;">
                        <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                        <asp:HiddenField ID="InsertOrUpdate2" runat="server" Value="0" />
                        <asp:HiddenField ID="ProspectoID" runat="server" Value="0" />
                       <asp:HiddenField ID="ProspectoID2" runat="server" Value="0" />
                    </td>
                </tr>
            </table>

        </fieldset>
        </telerik:RadPageView>

    <telerik:RadPageView ID="DatosContacto" runat="server" Selected="false">
        <fieldset>
                        <br />
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/images/AgregarEditarProveedor_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="Label1" Text="Agregar Entrada Seguimiento" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table border="0" width="100%">
                 <tr>
                    <td width="33%">
                        <asp:Label ID="Label2" runat="server" Text="Contacto:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="33%">
                        <asp:Label ID="Label3" runat="server" Text="Email: " CssClass="item" Font-Bold="True"></asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Requerido" ControlToValidate="txtEmailContacto" CssClass="item" Text="Formato no válido" ValidationExpression=".*@.*\..*"></asp:RegularExpressionValidator>
                    </td>
                    <td width="33%">
                        <asp:Label ID="Label4" runat="server" Text="Teléfono:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtNombreContacto1" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtEmailContacto1" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                        
                    </td>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtTelefonoContacto1" Runat="server" Width="85%">
                        </telerik:RadTextBox>
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
                                    <asp:Label ID="lblDescripcionSeguimiento" runat="server" Text="Puesto:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                        <telerik:RadTextBox ID="txtPuestoContacto" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td colspan="2">&nbsp;</td>
                            </tr>

                            <tr style="height: 40px">
                                <td colspan="2" style="text-align: right;">
                                    <asp:Button ID="btnGuardarContacto" runat="server" CausesValidation="true" ValidationGroup="gpoSeguimiento" Text="Guardar" CssClass="item" />&nbsp;
                                <asp:Button ID="btnCancelContacto" runat="server" CausesValidation="false" Text="Cancelar" CssClass="item" />
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <telerik:RadGrid ID="ContactoList" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" GridLines="None"
                                        PageSize="20" ShowStatusBar="True"
                                        Skin="Simple" Width="100%">
                                        <PagerStyle Mode="NumericPages" />
                                        <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Contactos" NoMasterRecordsText="No se encontraron movimientos para mostrar" Width="100%">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Contacto" UniqueName="contacto" SortExpression="contacto">
                                                     <ItemTemplate>
                                                           <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("contacto") %>' CausesValidation="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="telefono" HeaderText="Teléfono" UniqueName="telefono">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="email" HeaderText="Email" UniqueName="email">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="puesto" HeaderText="Puesto" UniqueName="puesto" >
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
                            <tr style="height: 20px">
                                <td colspan="2">&nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

    </asp:Panel>


    <telerik:RadWindow ID="RadWindow1" runat="server" ShowContentDuringLoad="False" Modal="True" ReloadOnShow="True" VisibleStatusbar="False" VisibleTitlebar="True" BorderStyle="None" BorderWidth="0px" Behaviors="Close" Width="600px" Height="500px" Skin="Default">
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
                                <asp:TextBox ID="txtFrom" runat="server" Width="100%" Enabled="false" CssClass="box"></asp:TextBox><%--&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Requerido" ControlToValidate="txtFrom" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:10%"> 
                                <asp:Label ID="lblTo" runat="server" Font-Bold="true" CssClass="item" Text="Para:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" runat="server" Width="100%" Enabled="true" CssClass="box"></asp:TextBox><%--&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Requerido" ControlToValidate="txtTo" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
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
                                <asp:TextBox ID="txtMenssage" TextMode="MultiLine" runat="server" Width="100%" Height="150px" CssClass="box"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadComboBox ID="anexosList" runat="server" CheckBoxes="true" DataValueField="archivo" DataTextField="nombre" EnableCheckAllItemsCheckBox="true"
                                Width="250" Label="Seleccione los anexos por enviar:"  Localization-CheckAllString="Seleccionar todo" Localization-ItemsCheckedString="items seleccionados" Localization-AllItemsCheckedString="Todos los items seleccionados"></telerik:RadComboBox>
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
                    <%--<asp:sqldatasource id="SqlDataSource1" runat="server" ConnectionString="<%$ session("conexion") %>" SelectCommand="select archivo, nombre from tblDocumentosApoyo where tipoid=2"></asp:sqldatasource>--%>
                </ContentTemplate>
            </telerik:RadWindow>
    
    </telerik:RadAjaxPanel>
            <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Default" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>