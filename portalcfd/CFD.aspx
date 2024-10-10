<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_CFD" CodeBehind="CFD_old.aspx.vb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openRadWindow(url) {
            var radwindow = $find('<%=RadWindow1.ClientID %>');
            radwindow.setUrl(url);
            radwindow.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <fieldset style="padding: 10px;">
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="item" Text="Encontrar CFDI"></asp:Label>
        </legend>
        <br />
        <span class="item">Serie:
            <asp:TextBox ID="txtSerie" runat="server" CssClass="box"></asp:TextBox>&nbsp;Folio:
            <asp:TextBox ID="txtFolio" runat="server" CssClass="box"></asp:TextBox>&nbsp;&nbsp;<asp:Button
                ID="btnView" runat="server" Text="Ver" Width="80px" />&nbsp;&nbsp;<asp:Label ID="lblMensaje" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
        </span>
        <br />
        <br />
    </fieldset>
    <br />
    <fieldset style="padding: 10px;">
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="item" Text="Buscador"></asp:Label>
        </legend>
        <br />
        <span class="item">Desde:
            <telerik:RadDatePicker ID="fha_ini" Skin="Office2007" runat="server">
            </telerik:RadDatePicker>
            Hasta:
            <telerik:RadDatePicker ID="fha_fin" Skin="Office2007" runat="server">
            </telerik:RadDatePicker>
            &nbsp;
            Tipo de documento:&nbsp;
            <asp:DropDownList ID="tipoid" runat="server" CssClass="box"></asp:DropDownList>&nbsp;
            <br />
            <br />
            Cliente:&nbsp;
            <asp:DropDownList ID="clienteid" runat="server" CssClass="box"></asp:DropDownList>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Width="80px" Text="Buscar" />
        </span>
        <br />
        <br />
    </fieldset>
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblCFDList" runat="server" Font-Bold="true" CssClass="item" Text="Lista de CFDIs"></asp:Label>
        </legend>
        <table width="100%">
            <tr>
                <td style="height: 5px"></td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="cfdlist" runat="server" Width="100%" ShowStatusBar="True"
                        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None"
                        Skin="Office2007" OnNeedDataSource="cfdlist_NeedDataSource" AllowFilteringByColumn="false">
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                        <MasterTableView Width="100%" DataKeyNames="id" NoMasterRecordsText="No se encontraron registros" Name="Clients" AllowMultiColumnSorting="False">
                            <Columns>
                                <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha" DataFormatString="{0:d}" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente" UniqueName="cliente" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="serie" HeaderText="Serie" UniqueName="serie" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="folio" HeaderText="Folio" UniqueName="folio" AllowFiltering="false">
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="editar" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Timbrado" UniqueName="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTimbrado" runat="server"></asp:Label>
                                        <asp:Image ID="imgTimbrado" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/arrow.gif" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkXML" runat="server" Text="xml" CommandArgument='<%# Eval("id") %>' CommandName="cmdXML"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPDF" runat="server" Text="pdf" CommandArgument='<%# Eval("id") %>' CommandName="cmdPDF"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Enviar" UniqueName="">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgSend" runat="server" ImageUrl="~/portalcfd/images/envelope.jpg" CommandArgument='<%# Eval("id") %>' CommandName="cmdSend" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Cancelar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCancelar" runat="server" Text="Cancelar" CommandArgument='<%# Eval("id") %>' CommandName="cmdCancel"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkAcuse" runat="server" Text="Ver Acuse" CommandArgument='<%# Eval("id") %>' CommandName="cmdAcuse"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Borrar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBorrar" runat="server" Text="Eliminar" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete"></asp:LinkButton>
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
                <td style="height: 5px">&nbsp;</td>
            </tr>
            <tr>
                <td style="height: 5px">&nbsp;</td>
            </tr>
        </table>
    </fieldset>
    <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" RenderMode="Lightweight" CenterIfModal="true" AutoSize="False" Behaviors="Close" VisibleOnPageLoad="False" Width="1024" Height="768">
    </telerik:RadWindow>
    <!-- Start Modal Cancelar -->
    <telerik:RadWindow ID="WinCancel" runat="server" RenderMode="Lightweight" Skin="Default" BorderStyle="None" Modal="true" Title="Cancelación CFDI" VisibleTitlebar="True" VisibleStatusbar="false" CenterIfModal="true" AutoSize="false" Behaviors="Close" VisibleOnPageLoad="False" Width="400" Height="260">
        <ContentTemplate>
            <div style="width: 95%; padding: 12px;">
                <table style="width: 100%;" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="lblMotivoCancela" runat="server" Font-Bold="true" CssClass="item">Motivo de cancelación:</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="cmbMotivoCancela" runat="server" Width="100%" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel runat="server" ID="panelFolioSustituye" Visible="false">
                                <asp:Label ID="Label3" runat="server" Font-Bold="true" CssClass="item">Folio que sustituye:</asp:Label><br />
                                <asp:TextBox ID="txtFolioSustituye" runat="server" Width="100%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFolioSustituye" runat="server" Enabled="false" ValidationGroup="vgFolioSustituye" InitialValue="" ErrorMessage="Requerido" CssClass="item" ForeColor="Red" ControlToValidate="txtFolioSustituye" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:HiddenField ID="CancelarId" runat="server" />
                            <asp:Button ID="btnCancelaFactura" runat="server" ValidationGroup="vgFolioSustituye" CssClass="cssBoton" Text="Cancelar CFDI" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>
    <!-- End Modal Cancelar -->
    <telerik:RadWindowManager ID="RadAlert" runat="server" Skin="Office2007" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
</asp:Content>
