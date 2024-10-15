Imports System.Data
Imports System.Data.SqlClient

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            If Not CookieUtil.GetTripleDESEncryptedCookieValue("email") Is Nothing Then
                chkRemember.Checked = True
                email.Text = CookieUtil.GetTripleDESEncryptedCookieValue("email")
                contrasena.Attributes.Add("value", CookieUtil.GetTripleDESEncryptedCookieValue("contrasena"))
            End If
        End If

    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim ClienteValido As Boolean = False
        Try
            Dim ObjData As New DataControl(0)
            Dim ds As New DataSet
            Dim p As New ArrayList
            p.Add(New SqlParameter("@email", email.Text))
            p.Add(New SqlParameter("@contrasena", contrasena.Text))
            ds = ObjData.FillDataSet("pLogin", p)
            ObjData = Nothing

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    If row("error") = 1 Then
                        lblMensaje.Text = row("mensaje")
                        ClienteValido = False
                    ElseIf row("error") = 0 Then
                        Session("appkey") = row("appKey").ToString
                        Session("conexion") = "Data Source=localhost; Initial Catalog=erp_retail_" & row("appKey") & "; User ID=sa; Password=Jupiter10!; Max Pool Size=200"
                        'Session("conexion") = "Data Source=.\SQLEXPRESS; Initial Catalog=erp_retail_" & row("appKey") & "; Persist Security Info=True; Trusted_Connection=yes; Max Pool Size=200;"
                        'Session("conexion") = "Data Source=lk1.linkium.net,56881; Initial Catalog=erp_retail_" & row("appKey") & "; User ID=sa; Password=Jupiter10!; Max Pool Size=200"
                        Session("clienteid") = row("clienteid")
                        Session("userid") = row("usuarioid")
                        Session("usuario") = row("usuario")
                        Session("perfilid") = row("perfilid")

                        If Session("perfilid") <> 3 Then
                            Dim DataControl As New DataControl(1)
                            ds = New DataSet
                            p.Clear()
                            p.Add(New SqlParameter("@cmd", 1))
                            p.Add(New SqlParameter("@email", email.Text))
                            p.Add(New SqlParameter("@contrasena", contrasena.Text))
                            ds = DataControl.FillDataSet("pConfiguracion", p)

                            If ds.Tables(0).Rows.Count > 0 Then
                                For Each r As DataRow In ds.Tables(0).Rows

                                    ds = New DataSet
                                    DataControl = New DataControl(1)
                                    p.Clear()
                                    p.Add(New SqlParameter("@cmd", 2))
                                    p.Add(New SqlParameter("@userid", Session("userid")))
                                    ds = DataControl.FillDataSet("pConfiguracion", p)

                                    If ds.Tables(0).Rows.Count > 0 Then
                                        For Each reg As DataRow In ds.Tables(0).Rows
                                            Session("restriccionProductoBit") = reg("restriccionProductoBit")
                                        Next
                                    Else
                                        Session("restriccionProductoBit") = False
                                    End If

                                    ClienteValido = True
                                    Session("admin") = r("admin")
                                    Session("razonsocial") = r("razonsocial")
                                    Session("contacto") = r("contacto")
                                    Session("logo") = r("logo")
                                    Session("logo_formato") = r("logo_formato")
                                Next
                                If chkRemember.Checked = True Then
                                    CookieUtil.SetTripleDESEncryptedCookie("email", email.Text, Now.AddDays(30))
                                    CookieUtil.SetTripleDESEncryptedCookie("contrasena", contrasena.Text, Now.AddDays(30))
                                Else
                                    CookieUtil.SetTripleDESEncryptedCookie("email", "", Now.AddDays(-1))
                                    CookieUtil.SetTripleDESEncryptedCookie("contrasena", "", Now.AddDays(-1))
                                End If
                            End If
                            DataControl = Nothing
                        Else
                            lblMensaje.Text = "Acceso Denegado"
                        End If
                    Else
                        lblMensaje.Text = "Pongase en contacto con el administrador"
                        ClienteValido = False
                    End If
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.End()
        End Try

        If ClienteValido Then
            Response.Redirect("~/portalcfd/Home.aspx", False)
        End If

    End Sub

End Class