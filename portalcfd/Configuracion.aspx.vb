Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Class portalcfd_Configuracion
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblPrivateKeyLegend.Text = Resources.Resource.lblPrivateKeyLegend
            lblCertificateLegend.Text = Resources.Resource.lblCertificateLegend
            lblCertificatesListLegend.Text = Resources.Resource.lblCertificatesListLegend

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblPrivateKey.Text = Resources.Resource.lblPrivateKey
            lblPrivateKeyDownload.Text = Resources.Resource.lblPrivateKeyDownload
            lblPasword.Text = Resources.Resource.lblPassword
            lblCertificate.Text = Resources.Resource.lblCertificate
            lblActivate.Text = Resources.Resource.lblActivate

            '''''''''''''''''''
            'Validators Titles'
            '''''''''''''''''''
            RequiredFieldValidator1.Text = Resources.Resource.validatorMessage
            CustomValidator.Text = Resources.Resource.validatorMessage
            ValidateExtensions.Text = Resources.Resource.InvalidExtension

            CustomValidator2.Text = Resources.Resource.validatorMessage
            ValidateExtensions2.Text = Resources.Resource.InvalidExtension

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnSavePrivateKey.Text = Resources.Resource.btnSave
            btnSaveCertificate.Text = Resources.Resource.btnSave

            '''''''''''''''''''''''''''''''
            'Private Key & Password Values'
            '''''''''''''''''''''''''''''''

            DisplayPrivateKeyAndPassword()

            '''''''''''''''''''''''''''
            'Telerik Grid Data Binding'
            '''''''''''''''''''''''''''

            DisplayCertificates()

        End If

    End Sub

#End Region

#Region "Private Key & Password Loading"

    Protected Sub DisplayPrivateKeyAndPassword()

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 2))
        ds = ObjData.FillDataSet("pCliente", p)
        ObjData = Nothing

        If ds.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                txtPassword.Text = row("contrasena_llave_privada")

                If (row("archivo_llave_privada").ToString.Length > 4) Then

                    fileName.Value = row("archivo_llave_privada")
                    lnkDownloadPrivateKey.Text = row("archivo_llave_privada")

                    lblPrivateKeyDownload.Visible = True
                    fileIcon.Visible = True
                    lnkDownloadPrivateKey.Visible = True

                End If

            Next
        Else
            lblPrivateKeyDownload.Visible = False
            fileIcon.Visible = False
            lnkDownloadPrivateKey.Visible = False
        End If

    End Sub

#End Region

#Region "Telerik Grid Certificates Binding"

    Private Sub DisplayCertificates()

        Dim ObjData As New DataControl(1)

        Try

            certificatesList.MasterTableView.NoMasterRecordsText = Resources.Resource.certificatesEmptyGridMessage
            certificatesList.DataSource = ObjData.FillDataSet("EXEC pCertificados @cmd=5")
            certificatesList.DataBind()

        Catch ex As Exception

        Finally

            ObjData = Nothing

        End Try

    End Sub

    Protected Sub certificatesList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles certificatesList.NeedDataSource

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 5))
        ds = ObjData.FillDataSet("pCertificados", p)
        ObjData = Nothing

        certificatesList.MasterTableView.NoMasterRecordsText = Resources.Resource.certificatesEmptyGridMessage
        certificatesList.DataSource = ds
        certificatesList.DataBind()

    End Sub

#End Region

#Region "Telerik Grid Active Image & Certificate Events (Download & Delete)"

    Protected Sub certificatesList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles certificatesList.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Certificates" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('" & Resources.Resource.CertificateDeleteConfirmationMessage & "');")
                Dim btnDownload As ImageButton = CType(e.Item.FindControl("btnDownload"), ImageButton)

                Dim imgDownload As Image = CType(dataItem("activo").FindControl("imgActive"), Image)

                If e.Item.DataItem("activo") <> "False" Then

                    imgDownload.ImageUrl = "~/images/icons/arrow.gif"

                Else

                    imgDownload.ImageUrl = "~/images/icons/close.gif"

                End If

            End If

        End If

    End Sub

    Protected Sub certificatesList_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles certificatesList.ItemCommand

        Select Case e.CommandName

            Case "cmdDownloadCertificate"
                DownloadCertificate(e.CommandArgument)

            Case "cmdDeleteCertificate"
                DeleteCertificateFromFolder(e.CommandArgument)
                DeleteCertificateFromDB(e.CommandArgument)
                DisplayCertificates()

        End Select

    End Sub

    Private Sub DownloadCertificate(ByVal id As Integer)

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 6))
        p.Add(New SqlParameter("@certificadoId", id))
        ds = ObjData.FillDataSet("pCertificados", p)
        ObjData = Nothing

        If ds.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                Dim FilePath As String = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/certificados/") & row("certificado")
                If File.Exists(FilePath) Then
                    Dim FileName As String = Path.GetFileName(FilePath)
                    Response.Clear()
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
                    Response.Flush()
                    Response.WriteFile(FilePath)
                End If
            Next
        End If

    End Sub

    Private Sub DeleteCertificateFromFolder(ByVal id As Integer)

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 6))
        p.Add(New SqlParameter("@certificadoId", id))
        ds = ObjData.FillDataSet("pCertificados", p)
        ObjData = Nothing

        If ds.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                Dim FilePath As String = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/certificados/") & row("certificado")
                If File.Exists(FilePath) Then
                    File.Delete(FilePath)
                End If
            Next
        End If

    End Sub

    Private Sub DeleteCertificateFromDB(ByVal id As Integer)

        Dim ObjData As New DataControl(1)
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 4))
        p.Add(New SqlParameter("@certificadoId", id))
        ObjData.ExecuteSP("pCertificados", 1, p)
        ObjData = Nothing

    End Sub

#End Region

#Region "Telerik Grid Items Column Names (From Resource File)"

    Protected Sub certificatesList_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles certificatesList.ItemCreated

        If TypeOf e.Item Is GridHeaderItem Then

            Dim header As GridHeaderItem = CType(e.Item, GridHeaderItem)

            If e.Item.OwnerTableView.Name = "Certificates" Then

                header("download").Text = Resources.Resource.gridColumnNameCertificate
                header("certificado").Text = Resources.Resource.gridColumnNameCertificate
                header("activo").Text = Resources.Resource.gridColumnNameActive
                header("delete").Text = Resources.Resource.gridColumnNameDelete

            End If

        End If

    End Sub

#End Region

#Region "Private Key Download Events"

    Protected Sub fileIcon_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles fileIcon.Click

        Dim FilePath As String = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/llaves/") & fileName.Value.ToString

        Trace.Write(FilePath)

        If File.Exists(FilePath) Then

            Dim PrivateKeyFile As String = Path.GetFileName(FilePath)

            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & PrivateKeyFile & """")
            Response.Flush()
            Response.WriteFile(FilePath)

        End If

    End Sub

    Protected Sub lnkDownloadPrivateKey_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDownloadPrivateKey.Click

        Dim FilePath As String = Server.MapPath("~/clientes/" + Session("appkey").ToString + "/llaves/") & fileName.Value.ToString

        Trace.Write(FilePath)

        If File.Exists(FilePath) Then

            Dim PrivateKeyFile As String = Path.GetFileName(FilePath)

            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & PrivateKeyFile & """")
            Response.Flush()
            Response.WriteFile(FilePath)

        End If

    End Sub

#End Region

#Region "Save Private Key"

    Protected Sub btnSavePrivateKey_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSavePrivateKey.Click

        Try

            If ((RadUpload1.UploadedFiles.Count = 0) And (lnkDownloadPrivateKey.Text <> "")) Then

                Dim newFileName As String = lnkDownloadPrivateKey.Text

                Dim ObjData As New DataControl(1)
                Dim p As New ArrayList
                p.Add(New SqlParameter("@cmd", 1))
                p.Add(New SqlParameter("@archivo_llave_privada", newFileName))
                p.Add(New SqlParameter("@contrasena_llave_privada", txtPassword.Text))
                ObjData.ExecuteSP("pCliente", 1, p)
                ObjData = Nothing

                Call DisplayPrivateKeyAndPassword()

            Else

                Dim newFileName As String = ""

                For Each validFile As UploadedFile In RadUpload1.UploadedFiles

                    newFileName = validFile.GetName()

                    validFile.SaveAs(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/certificados/") & newFileName)

                Next

                Dim ObjData As New DataControl(1)
                Dim p As New ArrayList
                p.Add(New SqlParameter("@cmd", 1))
                p.Add(New SqlParameter("@archivo_llave_privada", newFileName))
                p.Add(New SqlParameter("@contrasena_llave_privada", txtPassword.Text))
                ObjData.ExecuteSP("pCliente", 1, p)
                ObjData = Nothing

                Call DisplayPrivateKeyAndPassword()

            End If

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Save Certificate"

    Protected Sub btnSaveCertificate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveCertificate.Click

        Try

            Dim newFileName As String = ""

            For Each validFile As UploadedFile In RadUpload2.UploadedFiles

                newFileName = validFile.GetName()

                validFile.SaveAs(Server.MapPath("~/clientes/" + Session("appkey").ToString + "/certificados/") & newFileName)

            Next

            Dim ObjData As New DataControl(1)
            Dim p As New ArrayList
            p.Add(New SqlParameter("@cmd", 1))
            p.Add(New SqlParameter("@archivoCertificado", newFileName))
            p.Add(New SqlParameter("@activo", chckActivate.Checked))
            ObjData.ExecuteSP("pCertificados", 1, p)
            ObjData = Nothing

            Call DisplayCertificates()

        Catch ex As Exception

        End Try

    End Sub

#End Region

End Class