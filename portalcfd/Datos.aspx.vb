Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Class portalcfd_Datos
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

            lblDataLegend.Text = Resources.Resource.lblDataLegend

            '''''''''''''''''''''''''''''''''
            'Combobox Values & Empty Message'
            '''''''''''''''''''''''''''''''''

            Dim TelerikRadComboBox As New FillRadComboBox(1)
            TelerikRadComboBox.FillRadComboBox(cmbStates, "EXEC pCatalogos @cmd=1")

            cmbStates.Text = Resources.Resource.cmbEmptyMessage

            Dim ObjData As New DataControl(1)
            ObjData.Catalogo(plantillaid, "select id, nombre from tblPlantilla order by nombre", 0)
            ObjData.Catalogo(regimenid, "select id, id + ' - ' + nombre from tblRegimenFiscal order by nombre", 0)
            ObjData = Nothing

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblSocialReason.Text = Resources.Resource.lblSocialReason
            lblEmailContact.Text = "Email de acceso"
            lblPassword.Text = Resources.Resource.lblPassword
            lblStreet.Text = Resources.Resource.lblStreet
            lblExtNumber.Text = Resources.Resource.lblExtNumber
            lblIntNumber.Text = Resources.Resource.lblIntNumber
            lblColony.Text = Resources.Resource.lblColony
            lblCountry.Text = Resources.Resource.lblCountry
            lblState.Text = Resources.Resource.lblState
            lblTownship.Text = Resources.Resource.lblTownship
            lblZipCode.Text = Resources.Resource.lblZipCode
            lblRFC.Text = Resources.Resource.lblRFC
            lblLogo.Text = Resources.Resource.lblLogo
            lblRegimen.Text = Resources.Resource.lblRegimen

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
            valRegimen.Text = Resources.Resource.validatorMessage
            ValidateExtensions.Text = Resources.Resource.InvalidExtension

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnSaveData.Text = Resources.Resource.btnSave

            '''''''''''''
            'Data Values'
            '''''''''''''

            DisplayData()

        End If

        'If Session("admin") = 1 Then
        '    RadUpload1.Enabled = True
        '    RadUpload2.Enabled = True
        '    plantillaid.Enabled = True
        'Else
        '    RadUpload1.Enabled = False
        '    RadUpload2.Enabled = False
        '    plantillaid.Enabled = False
        'End If

    End Sub

#End Region

#Region "Display Data"

    Private Sub DisplayData()

        Try
            Dim ObjData As New DataControl(1)
            Dim ds As New DataSet
            Dim p As New ArrayList
            p.Add(New SqlParameter("@cmd", 3))
            ds = ObjData.FillDataSet("pCliente", p)

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    txtSocialReason.Text = row("razonsocial")
                    txtEmailContact.Text = row("email_contacto")
                    txtPassword.Text = row("contrasena")
                    txtStreet.Text = row("fac_calle")
                    txtExtNumber.Text = row("fac_num_ext")
                    If row("fac_num_int") = "." Then
                        txtIntNumber.Text = ""
                    Else
                        txtIntNumber.Text = row("fac_num_int")
                    End If
                    txtColony.Text = row("fac_colonia")
                    txtCountry.Text = row("fac_pais")
                    cmbStates.SelectedValue = row("fac_estadoid")
                    txtTownship.Text = row("fac_municipio")
                    txtZipCode.Text = row("fac_cp")
                    txtRFC.Text = row("fac_rfc")
                    lblLogoName.Text = row("logo")
                    lblLogoName2.Text = row("logo_formato")
                    txtExpedidoLinea1.Text = row("expedicionLinea1")
                    txtExpedidoLinea2.Text = row("expedicionLinea2")
                    txtExpedidoLinea3.Text = row("expedicionLinea3")
                    email_from.Text = row("email_from")
                    email_smtp_server.Text = row("email_smtp_server")
                    email_smtp_username.Text = row("email_smtp_username")
                    email_smtp_port.Text = row("email_smtp_port")
                    email_smtp_password.Text = row("email_smtp_password")
                    If row("porcentaje") > 0 Then
                        porcentaje.Text = row("porcentaje")
                    End If
                    ObjData.Catalogo(plantillaid, "select id, nombre from tblPlantilla order by nombre", row("plantillaid"))
                    ObjData.Catalogo(regimenid, "select id, id + ' - ' + nombre from tblRegimenFiscal order by nombre", row("regimenid"))
                    ObjData = Nothing
                Next
            End If

        Catch ex As Exception
            rwAlerta.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        End Try

    End Sub

#End Region

#Region "Save Data"

    Protected Sub btnSaveData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveData.Click

        Dim intNumber As String = txtIntNumber.Text
        Dim newFileName As String = ""
        Dim newFileName2 As String = ""

        If intNumber = "" Then

            intNumber = "."

        End If

        If ((RadUpload1.UploadedFiles.Count = 0) And (lblLogoName.Text <> "")) Then

            newFileName = lblLogoName.Text

        Else

            For Each validFile As UploadedFile In RadUpload1.UploadedFiles

                newFileName = validFile.GetName()

                validFile.SaveAs(Server.MapPath("~/clientes/" & Session("appkey").ToString & "/logos/") & newFileName.ToString)

            Next

        End If

        If ((RadUpload2.UploadedFiles.Count = 0) And (lblLogoName2.Text <> "")) Then

            newFileName2 = lblLogoName2.Text

        Else

            For Each validFile As UploadedFile In RadUpload2.UploadedFiles

                newFileName2 = validFile.GetName()

                validFile.SaveAs(Server.MapPath("~/clientes/" & Session("appkey").ToString & "/logos/") & newFileName2.ToString)

            Next

        End If

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pCliente @cmd=4, @razonsocial='" & txtSocialReason.Text & "', @fac_calle='" & txtStreet.Text & "', @fac_num_int='" & intNumber & "', @fac_num_ext='" & txtExtNumber.Text & "', @fac_colonia='" & txtColony.Text & "',  @fac_pais='" & txtCountry.Text & "', @fac_municipio='" & txtTownship.Text & "', @fac_estadoid='" & cmbStates.SelectedValue & "', @fac_cp='" & txtZipCode.Text & "', @fac_rfc='" & txtRFC.Text & "', @email_contacto='" & txtEmailContact.Text & "', @contrasena='" & txtPassword.Text & "', @logo='" & newFileName & "', @logo_formato='" & newFileName2 & "', @expedicionLinea1='" & txtExpedidoLinea1.Text & "', @expedicionLinea2='" & txtExpedidoLinea2.Text & "', @expedicionLinea3='" & txtExpedidoLinea3.Text & "', @email_from='" & email_from.Text & "', @email_smtp_server='" & email_smtp_server.Text & "', @email_smtp_username='" & email_smtp_username.Text & "', @email_smtp_password='" & email_smtp_password.Text & "', @email_smtp_port='" & email_smtp_port.Text & "', @porcentaje='" & porcentaje.Text & "', @plantillaid='" & plantillaid.SelectedValue.ToString & "', @regimenid='" & regimenid.SelectedValue.ToString & "'", conn)

            conn.Open()

            cmd.ExecuteReader()

            conn.Close()
            conn.Dispose()

            Response.Redirect("~/portalcfd/Datos.aspx")

        Catch ex As Exception
            rwAlerta.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

#End Region

End Class