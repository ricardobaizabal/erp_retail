Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Class portalcfd_usercontrols_portalcfd_Menu_PortalCFD
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Session("perfilid") Is Nothing Then
                Session.Abandon()
                Response.Redirect("~/Default.aspx")
            End If

            lblSocialReason.Text = "Cliente: " & Session("razonsocial").ToString
            lblContact.Text = "Usuario en sesión: " & Session("usuario").ToString

            RadMenu1.DataTextField = "Descripcion"
            RadMenu1.DataNavigateUrlField = "UrlMenu"
            RadMenu1.DataFieldID = "IdMenu"
            RadMenu1.DataFieldParentID = "IdMenuPadre"
            RadMenu1.DataSource = ListMenu()
            RadMenu1.DataBind()

            Select Case Path.GetFileName(Request.PhysicalPath.ToLower)
                Case "home.aspx"
                    RadMenu1.Items(0).Selected = True
                Case "clientes.aspx"
                    RadMenu1.Items(1).Selected = True
                Case "proveedores.aspx", "cuentas_banco.aspx", "ordenes_compra.aspx", "recepcion_facturas.aspx", "facturas_recibidas.aspx", "gastos.aspx", "cuentas_por_pagar.aspx", "egresos.aspx"
                    RadMenu1.Items(2).Selected = True
                Case "alumnos.aspx", "pedidos.aspx"
                    RadMenu1.Items(3).Selected = True
                Case "productos.aspx", "categorias.aspx", "subfamilias.aspx", "atributos.aspx", "valores.aspx", "abastecimiento.aspx", "entradas.aspx", "ajustes.aspx", "kardex.aspx", "transferencias.aspx", "activos.aspx"
                    RadMenu1.Items(4).Selected = True
                Case "remisiones.aspx"
                    RadMenu1.Items(5).Selected = True
                Case "cfd.aspx", "facturar33.aspx", "folios.aspx"
                    RadMenu1.Items(6).Selected = True
                Case "empleados.aspx", "asistencia.aspx"
                    RadMenu1.Items(6).Selected = True
                Case "reportes.aspx", "asistencia.aspx", "inventarios.aspx", "cortes.aspx", "conteodiario.aspx", "entradas.aspx", "detallepedido.aspx", "reportedetalladoventas.aspx", "detalleventa.aspx", "acumulado.aspx", "detalleventatipopago.aspx", "tarjetas.aspx", "ventas_hielo.aspx"
                    RadMenu1.Items(7).Selected = True
                Case "general.aspx", "programalealtad.aspx", "datos.aspx", "configuracion.aspx", "unidad.aspx", "claveproducto.aspx", "ciclosescolares.aspx", "gradosescolares.aspx", "gruposescolares.aspx", "sucursales.aspx", "pedidosplazo.aspx", "pos.aspx", "perfilpermisos.aspx"
                    RadMenu1.Items(8).Selected = True
            End Select

            Dim ObjData As New DataControl(0)
            Dim ds As New DataSet
            Dim p As New ArrayList
            p.Add(New SqlParameter("@cmd", 1))
            p.Add(New SqlParameter("@clienteid", Session("clienteid")))
            ds = ObjData.FillDataSet("pCliente", p)
            ObjData = Nothing

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    Session("moduloescolar") = CBool(row("ModuloEscolarBit"))
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        End Try
    End Sub
    Private Function ListMenu() As DataTable
        Dim ds As New DataSet
        Dim p As New ArrayList

        Dim ObjData As New DataControl(1)
        p.Add(New SqlParameter("@IdPerfil", Session("perfilid")))
        ds = ObjData.FillDataSet("pMenu_Read", p)
        ObjData = Nothing

        Return ds.Tables(0)

    End Function
    Private Sub RadMenu1_ItemDataBound(sender As Object, e As RadMenuEventArgs) Handles RadMenu1.ItemDataBound
        Dim row As DataRowView = DirectCast(e.Item.DataItem, DataRowView)
        e.Item.Visible = [Boolean].Parse(row("tienepermiso").ToString())
    End Sub

End Class