<%@ Page Title="" Language="vb" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="Alumnos.aspx.vb" Inherits="LinkiumCFDI.Alumnos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <asp:Panel ID="pBusqueda" runat="server" DefaultButton="btnBuscar">
                <span class="item">Nombre alumno:
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="box"></asp:TextBox>&nbsp;
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CausesValidation="false" />&nbsp;&nbsp;<asp:Button ID="btnTodo" runat="server" Text="Ver todo" CausesValidation="false" />
                </span>
                <br />
                <br />
            </asp:Panel>
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblClientsListLegend" runat="server" Font-Bold="true" Text="Listado de Alumnos" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                        <telerik:RadGrid ID="AlumnosList" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None" AllowSorting="true"
                            PageSize="15" ShowStatusBar="True"
                            Skin="Office2007" Width="100%">
                            <PagerStyle Mode="NextPrevAndNumeric" />
                            <MasterTableView AllowMultiColumnSorting="true" NoMasterRecordsText="No se encontraron registros para mostrar" AllowSorting="true" DataKeyNames="id" Name="Alumnos" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="matricula" HeaderText="Matrícula" UniqueName="matricula">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Alumno" DataField="nombre" SortExpression="nombre" UniqueName="nombre">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="familia" HeaderText="Familia" UniqueName="familia">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="contacto_padre" HeaderText="Contacto Padre" UniqueName="contacto_padre">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="telefono_contacto_padre" HeaderText="Teléfono" UniqueName="telefono_contacto_padre">
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
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAgregarAlumno" runat="server" Text="Agregar Alumno" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
        <asp:Panel ID="panelRegistration" runat="server" Visible="false">
            <table width="100%">
                <tr>
                    <td style="width: 33%;">
                        <asp:Label ID="lblMatricula" runat="server" CssClass="item" Text="Matrícula:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblNombre" runat="server" CssClass="item" Text="Nómbre:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblGradoEscolar" runat="server" CssClass="item" Text="Grado Escolar:" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <telerik:RadNumericTextBox NumberFormat-GroupSeparator="" ID="txtMatricula" runat="server" Width="92%" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        <%--<telerik:RadTextBox ID="txtMatricula" Runat="server" Width="92%">
                    </telerik:RadTextBox>--%>
                    </td>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtNombreAlumno" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">
                        <asp:DropDownList ID="cmbGradoEscolar" runat="server" CssClass="box" Width="90%"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valMatricula" runat="server" ControlToValidate="txtMatricula" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valNombreAlumno" runat="server" ControlToValidate="txtNombreAlumno" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:Label ID="lblFamilia" runat="server" CssClass="item" Text="Familia:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblSucursal" runat="server" CssClass="item" Text="Sucursal:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="Label1" runat="server" CssClass="item" Text="Registro portal web (nombre padre/madre tutor):" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtFamilia" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">
                        <asp:DropDownList ID="cmbSucursal" runat="server" CssClass="box" Width="90%"></asp:DropDownList>
                    </td>
                    <td style="width: 33%;">
                        <asp:DropDownList ID="cmbRegistroPortalWeb" runat="server" CssClass="box" Width="90%"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valSucursal" runat="server" ControlToValidate="cmbSucursal" InitialValue="0" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:Label ID="lblContactoPadre" runat="server" CssClass="item" Text="Contacto Padre:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblTelefonoContactoPadre" runat="server" CssClass="item" Text="Teléfono/Celular Padre:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblCorreoContactoPadre" runat="server" CssClass="item" Text="Email Padre:" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtContactoPadre" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtTelofonoContactoPadre" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtEmailContactoPadre" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailContactoPadre" SetFocusOnError="true" ErrorMessage="Formato no válido" CssClass="item" ValidationExpression=".*@.*\..*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:Label ID="lblContactoMadre" runat="server" CssClass="item" Text="Contacto Madre:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblTelefonoContactoMadre" runat="server" CssClass="item" Text="Teléfono/Celular Madre:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblCorreoContactoMadre" runat="server" CssClass="item" Text="Email Madre:" Font-Bold="True"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtContactoMadre" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtTelofonoContactoMadre" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtEmailContactoMadre" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>

                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:Label ID="lblusuario" runat="server" CssClass="item" Text="Usuario:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblcontrasena" runat="server" CssClass="item" Text="Contraseña:" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtusuario" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td width="33%">
                        <telerik:RadTextBox ID="txtcontraseña" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">&nbsp;</td>
                    <td width="33%">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmailContactoMadre" SetFocusOnError="true" ErrorMessage="Formato no válido" CssClass="item" ValidationExpression=".*@.*\..*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:HiddenField ID="AlumnoID" runat="server" Value="0" />
                    </td>
                </tr>
                <tr>
                    <td valign="bottom" colspan="3">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
