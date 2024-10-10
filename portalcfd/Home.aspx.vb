Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Partial Class portalcfd_Home
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = Resources.Resource.WindowsTitle
        '
        '   Define estados alternos para iconos
        '
        lnk1.Attributes.Add("onmouseover", "this.src='../images/inicio/clientes2.jpg'")
        lnk1.Attributes.Add("onmouseout", "this.src='../images/inicio/clientes.jpg'")
        '
        lnk2.Attributes.Add("onmouseover", "this.src='../images/inicio/productos2.jpg'")
        lnk2.Attributes.Add("onmouseout", "this.src='../images/inicio/productos.jpg'")
        '
        lnk3.Attributes.Add("onmouseover", "this.src='../images/inicio/Facturacion-03.jpg'")
        lnk3.Attributes.Add("onmouseout", "this.src='../images/inicio/Facturacion-02.jpg'")
        '
        lnk5.Attributes.Add("onmouseover", "this.src='../images/inicio/reportes2.jpg'")
        lnk5.Attributes.Add("onmouseout", "this.src='../images/inicio/reportes.jpg'")
        '
        lnk8.Attributes.Add("onmouseover", "this.src='../images/inicio/inventario2.jpg'")
        lnk8.Attributes.Add("onmouseout", "this.src='../images/inicio/inventario.jpg'")
        '
        lnk9.Attributes.Add("onmouseover", "this.src='../images/inicio/proveedores2.jpg'")
        lnk9.Attributes.Add("onmouseout", "this.src='../images/inicio/proveedores.jpg'")
        '
        lnk10.Attributes.Add("onmouseover", "this.src='../images/inicio/mis-datos-02.jpg'")
        lnk10.Attributes.Add("onmouseout", "this.src='../images/inicio/mis-datos-01.jpg'")
        '
        lnk11.Attributes.Add("onmouseover", "this.src='../images/inicio/salir2.jpg'")
        lnk11.Attributes.Add("onmouseout", "this.src='../images/inicio/salir.jpg'")

        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet()
        Dim p As New ArrayList
        p.Add(New SqlParameter("@IdPerfil", Session("perfilid")))
        ds = ObjData.FillDataSet("pPermisos_Read", p)
        ObjData = Nothing

        If ds.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows

                'Clientes
                If CStr(row("UrlMenu")).ToString = "~/portalcfd/Clientes.aspx" Then
                    lnk1.Enabled = CBool(row("Lectura"))
                End If

                'Productos
                If CStr(row("UrlMenu")).ToString = "~/portalcfd/almacen/Productos.aspx" Then
                    lnk2.Enabled = CBool(row("Lectura"))
                End If

                'Facturacion
                If CStr(row("UrlMenu")).ToString = "~/portalcfd/CFD.aspx" Then
                    lnk3.Enabled = CBool(row("Lectura"))
                End If

                'Datos
                If CStr(row("UrlMenu")).ToString = "~/portalcfd/datos.aspx" Then
                    lnk10.Enabled = CBool(row("Lectura"))
                End If

                'Proveedores
                If CStr(row("UrlMenu")).ToString = "~/portalcfd/proveedores/proveedores.aspx" Then
                    lnk9.Enabled = CBool(row("Lectura"))
                End If

                'Inventarios
                If CStr(row("UrlMenu")).ToString = "~/portalcfd/almacen/abastecimiento.aspx" Then
                    lnk8.Enabled = CBool(row("Lectura"))
                End If

                'Reportes
                If CStr(row("Descripcion")).ToString = "Reportes" Then
                    lnk5.Enabled = CBool(row("Lectura"))
                End If

            Next
        End If
    End Sub

#End Region

End Class
