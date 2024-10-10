Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class portalcfd_Clientes
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblClientsListLegend.Text = Resources.Resource.lblClientsListLegend
            'lblClientEditLegend.Text = Resources.Resource.lblClientEditLegend

            '''''''''''''''''''''''''''''''''
            'Combobox Values & Empty Message'
            '''''''''''''''''''''''''''''''''

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblSocialReason.Text = Resources.Resource.lblSocialReason
            lblContact.Text = Resources.Resource.lblContact
            lblContactEmail.Text = Resources.Resource.lblContactEmail
            lblContactPhone.Text = Resources.Resource.lblContactPhone
            lblStreet.Text = Resources.Resource.lblStreet
            lblExtNumber.Text = Resources.Resource.lblExtNumber
            lblIntNumber.Text = Resources.Resource.lblIntNumber
            lblColony.Text = Resources.Resource.lblColony
            lblCountry.Text = Resources.Resource.lblCountry
            lblState.Text = Resources.Resource.lblState
            lblTownship.Text = Resources.Resource.lblTownship
            lblZipCode.Text = Resources.Resource.lblZipCode
            lblRFC.Text = Resources.Resource.lblRFC
            lblNumCtaPago.Text = Resources.Resource.lblNumCtaPago

            '''''''''''''''''''
            'Validators Titles'
            '''''''''''''''''''

            RequiredFieldValidator1.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator2.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator3.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator4.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator5.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator6.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator7.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator8.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator9.Text = Resources.Resource.validatorMessage
            valTipoContribuyente.Text = Resources.Resource.validatorMessage

            RegularExpressionValidator1.Text = Resources.Resource.validatorMessageEmail
            valRFC.Text = Resources.Resource.validatorMessageRfc

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnAddClient.Text = Resources.Resource.btnAddClient
            btnSaveClient.Text = Resources.Resource.btnSave
            btnCancel.Text = Resources.Resource.btnCancel
            '
            Dim objCat As New DataControl(1)
            objCat.Catalogo(condicionesid, "select id, nombre from tblCondiciones", 0)
            objCat.Catalogo(tipoContribuyenteid, "select id, nombre from tblTipoContribuyente", 0)
            objCat.CatalogoString(formapagoid, "select id, nombre from tblFormaPago order by nombre", 0)
            objCat.Catalogo(estadoid, "select id, nombre from tblEstado order by nombre", 0)
            objCat.Catalogo(familiaid, "select id, nombre from tblFamilia where isnull(borradoBit,0)=0 order by nombre", 0)
            objCat.Catalogo(subfamiliaid, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 order by nombre", 0)
            objCat.Catalogo(cmbUsoCfd, "EXEC pCatalogos @cmd=8", 0)
            objCat.CatalogoString(regimenid, "select id, nombre from tblRegimenFiscal order by nombre", 0)
            objCat = Nothing

            If Session("moduloescolar") Then
                RadTabStrip1.Tabs(1).Visible = True
                RadTabStrip1.Tabs(2).Visible = True
                RadTabStrip1.Tabs(3).Visible = False
            Else
                RadTabStrip1.Tabs(1).Visible = False
                RadTabStrip1.Tabs(2).Visible = False
                RadTabStrip1.Tabs(3).Visible = False
            End If

        End If

    End Sub

#End Region

#Region "Load List Of Clients"

    Function GetClients() As DataSet
        
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pMisClientes  @cmd=1, @txtSearch='" & txtSearch.Text & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return ds

    End Function

#End Region

#Region "Telerik Grid Clients Loading Events"

    Protected Sub clientslist_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles clientslist.NeedDataSource

        clientslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        clientslist.DataSource = GetClients()
        
    End Sub

#End Region

#Region "Telerik Grid Language Modification(Spanish)"

    Protected Sub clientslist_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles clientslist.Init

        clientslist.PagerStyle.NextPagesToolTip = "Ver mas"
        clientslist.PagerStyle.NextPageToolTip = "Siguiente"
        clientslist.PagerStyle.PrevPagesToolTip = "Ver más"
        clientslist.PagerStyle.PrevPageToolTip = "Atrás"
        clientslist.PagerStyle.LastPageToolTip = "Última Página"
        clientslist.PagerStyle.FirstPageToolTip = "Primera Página"
        clientslist.PagerStyle.PagerTextFormat = "{4}    Página {0} de {1}, Registros {2} al {3} de {5}"
        clientslist.SortingSettings.SortToolTip = "Ordernar"
        clientslist.SortingSettings.SortedAscToolTip = "Ordenar Asc"
        clientslist.SortingSettings.SortedDescToolTip = "Ordenar Desc"


        Dim menu As Telerik.Web.UI.GridFilterMenu = clientslist.FilterMenu
        Dim i As Integer = 0

        While i < menu.Items.Count

            If menu.Items(i).Text = "NoFilter" Or menu.Items(i).Text = "Contains" Then
                i = i + 1
            Else
                menu.Items.RemoveAt(i)
            End If

        End While

        Call ModificaIdiomaGrid()

    End Sub

    Private Sub ModificaIdiomaGrid()

        clientslist.GroupingSettings.CaseSensitive = False

        Dim Menu As Telerik.Web.UI.GridFilterMenu = clientslist.FilterMenu
        Dim item As Telerik.Web.UI.RadMenuItem

        For Each item In Menu.Items

            ''''''''''''''''''''''''''''''''''''''''''''''
            'Change The Text For The StartsWith Menu Item'
            ''''''''''''''''''''''''''''''''''''''''''''''

            If item.Text = "StartsWith" Then
                item.Text = "Empieza con"
            End If

            If item.Text = "NoFilter" Then
                item.Text = "Sin Filtro"
            End If

            If item.Text = "Contains" Then
                item.Text = "Contiene"
            End If

            If item.Text = "EndsWith" Then
                item.Text = "Termina con"
            End If

        Next

    End Sub

#End Region

#Region "Telerik Grid Clients Editing & Deleting Events"

    Protected Sub clientslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles clientslist.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Clients" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('" & Resources.Resource.ClientsDeleteConfirmationMessage & "');")

            End If

        End If

    End Sub

    Protected Sub clientslist_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles clientslist.ItemCommand

        Select Case e.CommandName

            Case "cmdEdit"
                EditClient(e.CommandArgument)

            Case "cmdDelete"
                DeleteClient(e.CommandArgument)

        End Select

    End Sub

    Private Sub DeleteClient(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pMisClientes @cmd='3', @clienteId ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelClientRegistration.Visible = False

            clientslist.DataSource = GetClients()
            clientslist.DataBind()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Private Sub EditClient(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        'Try

        Dim cmd As New SqlCommand("EXEC pMisClientes @cmd='2', @clienteId='" & id & "'", conn)

        conn.Open()

        Dim rs As SqlDataReader
        rs = cmd.ExecuteReader()

        If rs.Read Then
            lblDescClienteDescuetoFamiliaValue.Text = rs("razonsocial")
            lblDescClienteDescuetoEspecialValue.Text = rs("razonsocial")
            txtSocialReason.Text = rs("razonsocial")
            txtDenominacionRaznScial.Text = rs("denominacion_razon_social")
            txtContact.Text = rs("contacto")
            txtContactEmail.Text = rs("email_contacto")
            txtContactPhone.Text = rs("telefono_contacto")
            txtStreet.Text = rs("fac_calle")
            txtExtNumber.Text = rs("fac_num_ext")
            txtIntNumber.Text = rs("fac_num_int")
            txtColony.Text = rs("fac_colonia")
            txtCountry.Text = rs("fac_pais")
            txtTownship.Text = rs("fac_municipio")
            txtZipCode.Text = rs("fac_cp")
            txtRFC.Text = rs("rfc")
            txtNumCtaPago.Text = rs("numctapago")
            chbaplicapogramalealtad.Checked = rs("afiliacionbit")
            txtnumeroafiliacion.Text = rs("NumeroAfiliacion")
            '
            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(condicionesid, "select id, nombre from tblCondiciones", rs("condicionesid"))
            ObjData.Catalogo(tipoContribuyenteid, "select id, nombre from tblTipoContribuyente", rs("tipoContribuyenteid"))
            ObjData.CatalogoString(formapagoid, "select id, nombre from tblFormaPago order by nombre", rs("formapagoid"))
            ObjData.Catalogo(estadoid, "select id, nombre from tblEstado order by nombre", rs("fac_estadoid"))
            cmbUsoCfd.SelectedValue = rs("usoCfdId")
            ObjData = Nothing

            Call SetCmbRegFiscal(rs("tipoContribuyenteid"), rs("regimenfiscalid"))

            panelClientRegistration.Visible = True

            InsertOrUpdate.Value = 1
            ClientsID.Value = id
            Call ObtenerDescuentosFamilia()
            Call ObtenerDescuentosEspeciales()
        End If

        'Catch ex As Exception


        'Finally

        conn.Close()
        conn.Dispose()

        'End Try

    End Sub

#End Region

#Region "Telerik Grid Clients Column Names (From Resource File)"

    'Protected Sub clientslist_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles clientslist.ItemCreated

    '    If TypeOf e.Item Is GridHeaderItem Then

    '        Dim header As GridHeaderItem = CType(e.Item, GridHeaderItem)

    '        If e.Item.OwnerTableView.Name = "Clients" Then

    '            header("razonsocial").Text = Resources.Resource.gridColumnNameSocialReason
    '            header("contacto").Text = Resources.Resource.gridColumnNameContact
    '            header("telefono_contacto").Text = Resources.Resource.gridColumnNameContactPhone
    '            header("rfc").Text = Resources.Resource.gridColumnNameRFC

    '        End If

    '    End If

    'End Sub

#End Region

#Region "Display Client Data Panel"

    Protected Sub btnAddClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddClient.Click

        InsertOrUpdate.Value = 0

        txtSocialReason.Text = ""
        txtContact.Text = ""
        txtContactEmail.Text = ""
        txtContactPhone.Text = ""
        txtStreet.Text = ""
        txtExtNumber.Text = ""
        txtIntNumber.Text = ""
        txtColony.Text = ""
        txtCountry.Text = ""
        txtTownship.Text = ""
        txtZipCode.Text = ""
        cmbUsoCfd.SelectedValue = 0
        txtDenominacionRaznScial.Text = ""
        regimenid.SelectedValue = 0
        txtRFC.Text = ""

        panelClientRegistration.Visible = True

    End Sub

#End Region

#Region "Save Client"

    Protected Sub btnSaveClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveClient.Click

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            If InsertOrUpdate.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pMisClientes @cmd=4, @clienteUnionId='" & Session("clienteid").ToString & "', @razonsocial='" & txtSocialReason.Text & "', @contacto='" & txtContact.Text & "', @email_contacto='" & txtContactEmail.Text & "', @telefono_contacto='" & txtContactPhone.Text & "', @fac_calle='" & txtStreet.Text & "', @fac_num_int='" & txtIntNumber.Text & "', @fac_num_ext='" & txtExtNumber.Text & "', @fac_colonia='" & txtColony.Text & "', @fac_pais='" & txtCountry.Text & "', @fac_municipio='" & txtTownship.Text & "', @fac_estadoid='" & estadoid.SelectedValue.ToString & "', @fac_cp='" & txtZipCode.Text & "', @fac_rfc='" & txtRFC.Text & "', @tipoprecioid='" & 0 & "', @condicionesid='" & condicionesid.SelectedValue.ToString & "', @tipocontribuyenteid='" & tipoContribuyenteid.SelectedValue.ToString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @usoCfdId='" & cmbUsoCfd.SelectedValue & "', @numctapago='" & txtNumCtaPago.Text & "', @regimenfiscalid='" & regimenid.SelectedValue & "', @denominacion_razon_social='" & txtDenominacionRaznScial.Text & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelClientRegistration.Visible = False

                clientslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
                clientslist.DataSource = GetClients()
                clientslist.DataBind()

                conn.Close()
                conn.Dispose()

            Else

                Dim cmd As New SqlCommand("EXEC pMisClientes @cmd=5, @clienteid='" & ClientsID.Value & "', @razonsocial='" & txtSocialReason.Text & "', @contacto='" & txtContact.Text & "', @email_contacto='" & txtContactEmail.Text & "', @telefono_contacto='" & txtContactPhone.Text & "', @fac_calle='" & txtStreet.Text & "', @fac_num_int='" & txtIntNumber.Text & "', @fac_num_ext='" & txtExtNumber.Text & "', @fac_colonia='" & txtColony.Text & "',  @fac_pais='" & txtCountry.Text & "', @fac_municipio='" & txtTownship.Text & "', @fac_estadoid='" & estadoid.SelectedValue.ToString & "', @fac_cp='" & txtZipCode.Text & "', @fac_rfc='" & txtRFC.Text & "', @tipoprecioid='" & 0 & "', @condicionesid='" & condicionesid.SelectedValue.ToString & "', @tipocontribuyenteid='" & tipoContribuyenteid.SelectedValue.ToString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @usoCfdId='" & cmbUsoCfd.SelectedValue & "', @numctapago='" & txtNumCtaPago.Text & "', @regimenfiscalid='" & regimenid.SelectedValue & "', @denominacion_razon_social='" & txtDenominacionRaznScial.Text & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelClientRegistration.Visible = False

                clientslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
                clientslist.DataSource = GetClients()
                clientslist.DataBind()

                conn.Close()
                conn.Dispose()

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

#End Region

#Region "Cancel Client (Save/Edit)"

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        InsertOrUpdate.Value = 0

        txtSocialReason.Text = ""
        txtContact.Text = ""
        txtContactEmail.Text = ""
        txtContactPhone.Text = ""
        txtStreet.Text = ""
        txtExtNumber.Text = ""
        txtIntNumber.Text = ""
        txtColony.Text = ""
        txtCountry.Text = ""
        txtTownship.Text = ""
        txtZipCode.Text = ""
        txtRFC.Text = ""
        formapagoid.SelectedValue = 0

        panelClientRegistration.Visible = False

    End Sub

#End Region

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        clientslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        clientslist.DataSource = GetClients()
        clientslist.DataBind()
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAll.Click
        txtSearch.Text = ""
        clientslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        clientslist.DataSource = GetClients()
        clientslist.DataBind()
    End Sub

    Private Sub btnAgregarDesuentoFamilia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarDesuentoFamilia.Click
        panelRegistroDescuentoFamilia.Visible = True
        familiaid.SelectedValue = 0
        subfamiliaid.SelectedValue = 0
        subfamiliaid.Enabled = False
        txtDescuentoFamilia.Text = "0"
        DescuentoFamiliaID.Value = 0
    End Sub

    Private Sub btnGuardarDescuentoFamilia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarDescuentoFamilia.Click
        Dim ObjData As New DataControl(1)
        If DescuentoFamiliaID.Value = 0 Then
            ObjData.RunSQLQuery("exec pClienteDescuentoFamilia @cmd=3, @clienteid='" & ClientsID.Value.ToString & "', @familiaid='" & familiaid.SelectedValue.ToString & "', @subfamiliaid='" & subfamiliaid.SelectedValue.ToString & "', @descuento='" & txtDescuentoFamilia.Text & "'")
        Else
            ObjData.RunSQLQuery("exec pClienteDescuentoFamilia @cmd=5, @id='" & DescuentoFamiliaID.Value.ToString & "', @clienteid='" & ClientsID.Value.ToString & "', @familiaid='" & familiaid.SelectedValue.ToString & "', @subfamiliaid='" & subfamiliaid.SelectedValue.ToString & "', @descuento='" & txtDescuentoFamilia.Text & "'")
        End If

        panelRegistroDescuentoFamilia.Visible = False
        Call ObtenerDescuentosFamilia()

        ObjData = Nothing
    End Sub

    Private Sub ObtenerDescuentosFamilia()
        Dim ObjData As New DataControl(1)
        descuentosfamilialist.DataSource = ObjData.FillDataSet("EXEC pClienteDescuentoFamilia @cmd=1, @clienteid='" & ClientsID.Value.ToString & "'")
        descuentosfamilialist.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        descuentosfamilialist.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub btnCancelarDescuentoFamilia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarDescuentoFamilia.Click
        panelRegistroDescuentoFamilia.Visible = False
        familiaid.SelectedValue = 0
        subfamiliaid.SelectedValue = 0
        subfamiliaid.Enabled = False
        txtDescuentoFamilia.Text = "0"
        DescuentoFamiliaID.Value = 0
    End Sub

    Private Sub familiaid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles familiaid.SelectedIndexChanged
        If familiaid.SelectedValue = 0 Then
            subfamiliaid.Enabled = False
            subfamiliaid.SelectedValue = 0
        Else
            subfamiliaid.Enabled = True
            Dim objCat As New DataControl(1)
            objCat.Catalogo(subfamiliaid, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 and familiaid='" & familiaid.SelectedValue & "' order by nombre", 0)
            objCat = Nothing
        End If
    End Sub

    Private Sub descuentosfamilialist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles descuentosfamilialist.ItemCommand
        Select Case e.CommandName

            Case "cmdEdit"
                EditaDescuentoFamilia(e.CommandArgument)
            Case "cmdDelete"
                EliminaDescuentoFamilia(e.CommandArgument)

        End Select
    End Sub

    Private Sub EditaDescuentoFamilia(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pClienteDescuentoFamilia @cmd=4, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Dim objCat As New DataControl(1)
                objCat.Catalogo(familiaid, "select id, nombre from tblFamilia where isnull(borradoBit,0)=0 order by nombre", rs("familiaid"))
                objCat.Catalogo(subfamiliaid, "select id, nombre from tblSubFamilia where isnull(borradoBit,0)=0 order by nombre", rs("subfamiliaid"))
                objCat = Nothing
                If familiaid.SelectedValue = 0 Then
                    subfamiliaid.Enabled = True
                Else
                    subfamiliaid.Enabled = True
                End If
                txtDescuentoFamilia.Text = rs("descuento")

                panelRegistroDescuentoFamilia.Visible = True
                DescuentoFamiliaID.Value = id

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub EliminaDescuentoFamilia(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pClienteDescuentoFamilia @cmd=2, @id ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistroDescuentoFamilia.Visible = False

            Call ObtenerDescuentosFamilia()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Private Sub descuentosfamilialist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles descuentosfamilialist.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
                btnDelete.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea eliminar este descuento de la base de datos?');")
        End Select
    End Sub

    Private Sub descuentosfamilialist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles descuentosfamilialist.NeedDataSource
        Dim ds As New DataSet()
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("EXEC pClienteDescuentoFamilia @cmd=1, @clienteid='" & ClientsID.Value.ToString & "'")
        descuentosfamilialist.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        descuentosfamilialist.DataSource = ds
        ObjData = Nothing
    End Sub

    Function GetProducts() As DataSet
        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pMisProductos @cmd=6, @txtSearch='" & txtProducto.Text & "'", conn)
        Dim ds As DataSet = New DataSet
        Try
            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        Return ds
    End Function

    Private Sub gridResults_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gridResults.ItemCommand
        Select Case e.CommandName
            Case "cmdAdd"
                Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","c})
                Dim productoid As Long = commandArgs(0)
                Dim presentacionid As Long = commandArgs(1)
                Call InsertItem(productoid, presentacionid, e.Item)
        End Select
    End Sub

    Private Sub InsertItem(ByVal productoid As Long, ByVal presentacionid As Long, ByVal item As GridItem)
        '
        '   Agrega descuento especial
        '
        Dim txtDescuentoEspecial As RadNumericTextBox = DirectCast(item.FindControl("txtDescuentoEspecial"), RadNumericTextBox)

        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("exec pClienteDescuentoEspecial @cmd=3, @clienteid='" & ClientsID.Value.ToString & "', @productoid='" & productoid.ToString & "', @presentacionid='" & presentacionid.ToString & "', @descuento='" & txtDescuentoEspecial.Text & "'")
        ObjData = Nothing
        '
        gridResults.Visible = False
        Call ObtenerDescuentosEspeciales()
        ''
    End Sub

    Protected Sub gridResults_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gridResults.NeedDataSource
        gridResults.Visible = True
        gridResults.DataSource = GetProducts()
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        gridResults.Visible = True
        gridResults.DataSource = GetProducts()
        gridResults.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        gridResults.DataBind()
    End Sub

    Private Sub descuentosespecialeslist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles descuentosespecialeslist.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                Call EliminaDescuentoEspecial(e.CommandArgument)
                Call ObtenerDescuentosEspeciales()
        End Select
    End Sub

    Private Sub EliminaDescuentoEspecial(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pClienteDescuentoEspecial @cmd=2, @id ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            Call ObtenerDescuentosEspeciales()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Private Sub ObtenerDescuentosEspeciales()
        Dim ObjData As New DataControl(1)
        descuentosespecialeslist.DataSource = ObjData.FillDataSet("EXEC pClienteDescuentoEspecial @cmd=1, @clienteid='" & ClientsID.Value.ToString & "'")
        descuentosespecialeslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        descuentosespecialeslist.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub descuentosespecialeslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles descuentosespecialeslist.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
                btnDelete.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea eliminar este descuento de la base de datos?');")
        End Select
    End Sub

    Private Sub descuentosespecialeslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles descuentosespecialeslist.NeedDataSource
        Dim ObjData As New DataControl(1)
        descuentosespecialeslist.DataSource = ObjData.FillDataSet("EXEC pClienteDescuentoEspecial @cmd=1, @clienteid='" & ClientsID.Value.ToString & "'")
        descuentosespecialeslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros para mostrar"
        ObjData = Nothing
    End Sub

    Private Sub btnguardarprogramalealtad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardarprogramalealtad.Click
        Dim ObjData As New DataControl(1)
        ObjData.RunSQLQuery("exec pCliente @cmd=5, @clienteid='" & ClientsID.Value.ToString & "', @programalealtadbitcliente='" & chbaplicapogramalealtad.Checked & "', @numeroafiliacionlealtad='" & txtnumeroafiliacion.Text & "'")
        ObjData = Nothing
    End Sub

    Private Sub SetCmbRegFiscal(Optional ByVal contribuyenteid As Integer = 0, Optional ByVal sel As Integer = 0)
        Dim ObjData As New DataControl(1)
        Dim sqlwhere As String
        Select Case tipoContribuyenteid.SelectedValue
            Case 1
                sqlwhere = "where fisica='Sí' "
            Case 2
                sqlwhere = "where moral='Sí' "
            Case Else
                sqlwhere = ""
        End Select
        ObjData.CatalogoString(regimenid, "select id, id + ' - ' + nombre as descripcion " & "from tblRegimenFiscal " & sqlwhere & " order by nombre ", sel)
        ObjData = Nothing
    End Sub

    Private Sub tipoContribuyenteid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tipoContribuyenteid.SelectedIndexChanged
        SetCmbRegFiscal(tipoContribuyenteid.SelectedValue)
    End Sub

End Class