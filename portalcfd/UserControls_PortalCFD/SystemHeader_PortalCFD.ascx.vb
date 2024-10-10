
Partial Class portalcfd_UserControls_PortalCFD_SystemHeader_PortalCFD
    Inherits System.Web.UI.UserControl

#Region "Show Date, User Name & User IP On The Master Page Header"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        imgLogo.ImageUrl = "~/clientes/" & Session("appkey").ToString & "/logos/" & Session("logo").ToString
    End Sub

#End Region

End Class