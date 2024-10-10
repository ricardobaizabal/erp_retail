Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://retail.linkium.mx")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class PosServices
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function leeusuarios(ByVal token As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pPosServices @cmd=1, @token='" & token.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leeproductos(ByVal token As String, ByVal con As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=2, @token='" & token.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leepresentacionesproductos(ByVal token As String, ByVal con As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=21, @token='" & token.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leemargenesdeutilidad(ByVal token As String, ByVal con As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=4, @token='" & token.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leeclientes(ByVal token As String, ByVal con As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=3, @token='" & token.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leecliente(ByVal token As String, ByVal clienteid As Long) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("EXEC pPosServices @cmd=19, @token='" & token.ToString & "', @clienteid='" & clienteid.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leedescuentosespecialescliente(ByVal token As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pPosServices @cmd=13, @token='" & token.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leedescuentosfamilia(ByVal token As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pPosServices @cmd=14, @token='" & token.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leeunidadesdemedida(ByVal token As String, ByVal con As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=12, @token='" & token.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
Public Function leedescuentos(ByVal token As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pPosServices @cmd=14, @token='" & token.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Sub reportaremision(ByVal token As String, ByVal tokenRemision As String, ByVal remisionid As Long, ByVal sucursalid As Integer, ByVal fecha As String, ByVal userid As Integer, ByVal clienteid As Long, ByVal promocionid As Long, ByVal descuentoid As Integer, ByVal descuento As String, ByVal cambio As String, ByVal empleadobit As Boolean, ByVal comentario As String, ByVal importe As Double, ByVal subtotal As Double, ByVal iva As Double, ByVal total As Double, ByVal vendedorid As Integer, ByVal descuentoid2 As Integer, ByVal descuento2 As String, ByVal creditoBit As Boolean, ByVal tokenCaja As String, ByVal pedidoid As Integer, ByVal tipopedidoid As Integer, ByVal direccionenvio As String, ByVal cnn As String)
        Dim empleadoInt As Integer
        Dim creditoInt As Integer

        If empleadobit = True Then
            empleadoInt = 1
        Else
            empleadoInt = 0
        End If

        If creditoBit = True Then
            creditoInt = 1
        Else
            creditoInt = 0
        End If

        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPosServices @cmd=5, @tokenRemision='" & tokenRemision.ToString & "', @remisionid='" & remisionid.ToString & "', @sucursalid='" & sucursalid.ToString & "', @fecha='" & fecha.ToString & "', @userid='" & userid.ToString & "', @clienteid='" & clienteid.ToString & "', @promocionid='" & promocionid.ToString & "', @descuentoid='" & descuentoid.ToString & "', @descuento='" & descuento.ToString & "', @cambio='" & cambio.ToString & "', @empleadobit='" & empleadoInt.ToString & "', @comentario='" & comentario.ToString & "', @importe='" & importe & "', @subtotal='" & subtotal & "', @iva='" & iva & "', @total='" & total & "', @vendedorid='" & vendedorid & "', @descuentoid2='" & descuentoid2.ToString & "', @descuento2='" & descuento2.ToString & "', @creditoBit='" & creditoInt.ToString & "', @tokenCaja='" & tokenCaja.ToString & "', @pedidoid='" & pedidoid.ToString & "', @tipopedidoid='" & tipopedidoid.ToString & "', @direccionenvio='" & direccionenvio.ToString & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Sub reportaremisiondetalle(ByVal token As String, ByVal remisiondetalleid As Long, ByVal remisionid As Long, ByVal sucursalid As Integer, ByVal productoid As Long, ByVal presentacionid As Long, ByVal factor As Decimal, ByVal cantidad As Decimal, ByVal unidad As String, ByVal descripcion As String, ByVal unitario As String, ByVal unitario2 As String, ByVal unitario3 As String, ByVal promocionid As Long, ByVal descuentoid As Integer, ByVal descuento As String, ByVal empleadoBit As Boolean, ByVal comentario As String, ByVal tokenCaja As String, ByVal costo_estandar As Decimal, ByVal cnn As String)
        Dim empleadoInt As Integer
        If empleadoBit = True Then
            empleadoInt = 1
        Else
            empleadoInt = 0
        End If
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPosServices @cmd=6, @remisiondetalleid='" & remisiondetalleid.ToString & "', @remisionid='" & remisionid.ToString & "', @sucursalid='" & sucursalid.ToString & "', @productoid='" & productoid.ToString & "', @presentacionid='" & presentacionid.ToString & "', @factor='" & factor.ToString & "', @cantidad='" & cantidad.ToString & "', @unidad='" & unidad.ToString & "', @descripcion='" & descripcion.ToString & "', @unitario='" & unitario.ToString & "', @unitario2='" & unitario2.ToString & "', @unitario3='" & unitario3.ToString & "', @promocionid='" & promocionid.ToString & "', @descuentoid='" & descuentoid.ToString & "', @descuento='" & descuento.ToString & "', @empleadobit='" & empleadoInt.ToString & "', @comentario='" & comentario.ToString & "', @tokenCaja='" & tokenCaja.ToString & "', @costo_estandar='" & costo_estandar.ToString & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Sub reportaremisionpago(ByVal token As String, ByVal remisionpagoid As Long, ByVal remisionid As Long, ByVal sucursalid As Integer, ByVal fecha As String, ByVal formapagoid As Integer, ByVal monto As String, ByVal autorizacion As String, ByVal userid As Integer, ByVal cajaid As Long, ByVal tokenCaja As String, ByVal cnn As String)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPosServices @cmd=7, @remisionpagoid='" & remisionpagoid.ToString & "', @remisionid='" & remisionid.ToString & "', @sucursalid='" & sucursalid.ToString & "', @fecha='" & fecha.ToString & "', @formapagoid='" & formapagoid.ToString & "', @monto='" & monto.ToString & "', @autorizacion='" & autorizacion.ToString & "', @userid='" & userid.ToString & "', @cajaid='" & cajaid.ToString & "', @tokenCaja='" & tokenCaja.ToString & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function agregarnuevocliente(ByVal token As String, ByVal vis_nombrecomercial As String, ByVal calle As String, ByVal numExt As String, ByVal numInt As String, ByVal colonia As String, ByVal zipcode As String, ByVal estadoid As Integer, ByVal estado As String, ByVal municipio As String, ByVal pais As String, ByVal rfc As String, ByVal clasificacionid As Integer, ByVal userid As Integer, ByVal email As String, ByVal telefono As String, ByVal celular As String, ByVal vendedorid As Integer) As Long
        Dim SQL As String = ""
        Dim clienteId As Long = 0
        Dim ObjData As New DataControl
        SQL = "exec pPosServices @cmd=11, @token='" & token.ToString & "', @vis_nombrecomercial='" & vis_nombrecomercial & "', @vis_calle='" & calle & "', @vis_num_ext='" & numExt & "', @vis_num_int='" & numInt & "', @vis_colonia='" & colonia & "', @vis_cp='" & zipcode & "', @vis_estadoid='" & estadoid.ToString() & "', @vis_estado='" & estado & "', @vis_municipio='" & municipio & "', @vis_pais='" & pais & "', @clasificacionid='" & clasificacionid.ToString() & "', @userid='" & userid.ToString() & "', @email_contacto='" & email & "', @telefono_contacto='" & telefono & "', @movil_contacto='" & celular & "', @vendedorid='" & vendedorid.ToString & "'"
        clienteId = ObjData.RunSQLScalarQuery(SQL)
        ObjData = Nothing
        Return clienteId
    End Function

    <WebMethod()> _
    Public Sub actualizarcliente(ByVal token As String, ByVal clienteid As Long, ByVal razonsocial As String, ByVal fac_rfc As String, ByVal contacto As String, ByVal email_contacto As String, ByVal telefono_contacto As String, ByVal fac_calle As String, ByVal fac_num_int As String, ByVal fac_num_ext As String, ByVal fac_colonia As String, ByVal fac_municipio As String, ByVal fac_pais As String, ByVal fac_estadoid As Integer, ByVal fac_cp As String, ByVal condicionesid As Integer, ByVal tipocontribuyenteid As Integer, ByVal formapagoid As Integer, ByVal numctapago As String)
        Dim SQL As String = ""
        Dim ObjData As New DataControl
        SQL = "exec pPosServices @cmd=9, @token='" & token.ToString & "', @clienteid='" & clienteid.ToString() & "', @razonsocial='" & razonsocial.ToString() & "', @fac_rfc='" & fac_rfc.ToString() & "', @contacto='" & contacto.ToString() & "', @email_contacto='" & email_contacto.ToString() & "', @telefono_contacto='" & telefono_contacto.ToString() & "', @fac_calle='" & fac_calle.ToString() & "', @fac_num_int='" & fac_num_int.ToString() & "', @fac_num_ext='" & fac_num_ext.ToString() & "', @fac_colonia='" & fac_colonia.ToString() & "', @fac_municipio='" & fac_municipio.ToString() & "', @fac_pais='" & fac_pais.ToString() & "', @fac_estadoid='" & fac_estadoid.ToString() & "', @fac_cp='" & fac_cp.ToString() & "', @condicionesid='" & condicionesid.ToString() & "', @tipocontribuyenteid='" & tipocontribuyenteid.ToString() & "', @formapagoid='" & formapagoid.ToString() & "', @numctapago='" & numctapago.ToString() & "'"
        ObjData.RunSQLQuery(SQL)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function leesucursales(ByVal token As String, ByVal con As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=15, @token='" & token.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leeformapago(ByVal token As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pPosServices @cmd=16, @token='" & token.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leemetododepago(ByVal token As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pPosServices @cmd=30, @token='" & token.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leeusodecfdi(ByVal token As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pPosServices @cmd=31, @token='" & token.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leecondicionespago(ByVal token As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pPosServices @cmd=17, @token='" & token.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leetiposcontribuyente(ByVal token As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pPosServices @cmd=18, @token='" & token.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leeestados(ByVal token As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pPosServices @cmd=20, @token='" & token.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function agregaconsignacion(ByVal token As String, ByVal remisionId As Long, ByVal clienteId As Long, ByVal sucursalId As Integer, ByVal userId As Integer, ByVal saldo As Decimal, ByVal creditobit As Boolean, ByVal tipo_consignacion As Integer) As Long
        Dim SQL As String = ""
        Dim consignacionId As Long = 0
        Dim creditoInt As Integer
        If creditobit = True Then
            creditoInt = 1
        Else
            creditoInt = 0
        End If
        Dim ObjData As New DataControl
        SQL = "exec pConsignaciones @cmd=1, @token='" & token.ToString & "', @remisionid='" & remisionId.ToString() & "', @clienteid='" & clienteId.ToString() & "', @sucursalid='" & sucursalId.ToString() & "', @userid='" & userId.ToString() & "', @saldo='" & saldo.ToString() & "', @creditobit='" & creditoInt & "', @tipo_consignacion='" & tipo_consignacion & "'"
        consignacionId = ObjData.RunSQLScalarQuery(SQL)
        ObjData = Nothing
        Return consignacionId
    End Function

    <WebMethod()> _
    Public Sub agregaproductoconsignacion(ByVal token As String, ByVal clienteid As Long, ByVal consignacionid As Long, ByVal productoid As Integer, ByVal cantidad As Decimal)
        Dim SQL As String = ""
        Dim ObjData As New DataControl
        SQL = "exec pConsignaciones @cmd=2, @token='" & token.ToString & "', @clienteid='" & clienteid.ToString() & "', @consignacionid='" & consignacionid.ToString() & "', @productoid='" & productoid.ToString() & "', @cantidad='" & cantidad.ToString() & "'"
        ObjData.RunSQLQuery(SQL)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function leeconsignaciones(ByVal sucursalid As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("EXEC pConsignaciones @cmd=5, @sucursalid='" & sucursalid.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leedetalleconsignacion(ByVal sucursalid As String, ByVal consignacionid As Integer) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("EXEC pConsignaciones @cmd=6, @sucursalid='" & sucursalid.ToString & "', @consignacionid='" & consignacionid.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Sub devolucionproductoconsignacion(ByVal sucursalid As Long, ByVal consignacionclienteid As Long, ByVal cantidad As Decimal)
        Dim SQL As String = ""
        Dim ObjData As New DataControl
        SQL = "exec pConsignaciones @cmd=7, @sucursalid='" & sucursalid.ToString & "', @consignacionclienteid='" & consignacionclienteid.ToString() & "', @cantidad='" & cantidad.ToString() & "'"
        ObjData.RunSQLQuery(SQL)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Sub pagoproductocredito(ByVal sucursalid As Long, ByVal consignacionclienteid As Long, ByVal cantidad As Decimal, ByVal pagoid As Long)
        Dim SQL As String = ""
        Dim ObjData As New DataControl
        SQL = "exec pConsignaciones @cmd=11, @sucursalid='" & sucursalid.ToString & "', @consignacionclienteid='" & consignacionclienteid.ToString() & "', @cantidad='" & cantidad.ToString() & "', @pagoid='" & pagoid.ToString() & "'"
        ObjData.RunSQLQuery(SQL)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function leesaldototalconsignacion(ByVal consignacionid) As Decimal
        Dim total As Decimal = 0
        Dim ObjData As New DataControl
        total = ObjData.RunSQLScalarQueryDecimal("exec pConsignaciones @cmd=8, @consignacionid='" & consignacionid.ToString & "'")
        ObjData = Nothing
        Return total
    End Function

    <WebMethod()> _
    Public Function leefechavencimientoconsignacion(ByVal sucursalid As Long, ByVal remisionid As Long) As String
        Dim fecha As String = ""
        Dim ObjData As New DataControl
        fecha = ObjData.RunSQLScalarQueryString("exec pConsignaciones @cmd=9, @sucursalid='" & sucursalid.ToString & "', @remisionid='" & remisionid.ToString() & "'")
        ObjData = Nothing
        Return fecha
    End Function

    <WebMethod()> _
    Public Sub agregapagoconsignacion(ByVal consignacionid As Long, ByVal pago As Decimal)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pConsignaciones @cmd=10, @consignacionid='" & consignacionid.ToString & "', @pago='" & pago.ToString() & "'")
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function leepartidaspago(ByVal pagoid As Long) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pConsignaciones @cmd=12, @pagoid='" & pagoid.ToString() & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Sub actualizaremision(ByVal cfdid As String, ByVal remisionid As String)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPosServices @cmd=22, @cfdid='" & cfdid.ToString & "', @remisionid='" & remisionid.ToString & "'")
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function leeremisionescanceladas(ByVal token As String, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("EXEC pPosServices @cmd=24, @token='" & token.ToString & "'", cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Sub reportacanceladas(ByVal token As String, ByVal remisionid As String, ByVal cnn As String)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("EXEC pPosServices @cmd=25, @token='" & token.ToString & "', @remisionid='" & remisionid.ToString & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Sub reportapagocredito(ByVal token As String, ByVal remisionid As String, ByVal cnn As String)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("EXEC pPosServices @cmd=27, @token='" & token.ToString & "', @remisionid='" & remisionid.ToString & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function leeremisionesfacturadascanceladas(ByVal token As String, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("EXEC pPosServices @cmd=28, @token='" & token.ToString & "'", cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function reportaremisionesfacturadascanceladas(ByVal token As String, ByVal remisionid As String, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("EXEC pPosServices @cmd=29, @token='" & token.ToString & "', @remisionid='" & remisionid.ToString & "'", cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leeusuariosretail(ByVal clienteid As String, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pUsuarios @cmd=1, @clienteid='" & clienteid & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leersucursalcaja(ByVal token As String, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pCaja @token=" & token.ToString, cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function obteneralumnos(ByVal sucursalid As Integer, ByVal matricula As Integer, ByVal nombre As String, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pAlumnos @cmd=7, @sucursalid=" & sucursalid & ", @matricula=" & matricula & ", @nombre=" & nombre, cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function obtenerpedidosporsucursal(ByVal sucursalid As Integer, ByVal estatus As Integer, ByVal nombre As String, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPedidos @cmd=18, @txtSearch='" & nombre & "', @estatusid='" & estatus & "', @sucursalid='" & sucursalid & "'", cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function obtenerestatus(ByVal query As String, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN(query, cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function IngresarAlumno(ByVal matricula As String, ByVal nombre As String, ByVal familia As String, ByVal sucursalid As Integer, ByVal contactopadre As String, ByVal telefonopadre As String, ByVal emailpadre As String, ByVal contactomadre As String, ByVal telefonomadre As String, ByVal emailmadre As String, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim sql As String = ""
        sql = "EXEC pAlumnos @cmd=8, @nombre='" & nombre & "', @matricula='" & matricula & "', @sucursalid='" & sucursalid & "', @familia='" & familia & "', @contacto_padre='" & contactopadre & "', @telefono_contacto_padre='" & telefonopadre & "', @email_contacto_padre='" & emailpadre & "', @contacto_madre='" & contactomadre & "', @telefono_contacto_madre='" & telefonomadre & "', @email_contacto_madre='" & emailmadre & "'"

        Dim ObjData As New DataControl()
        ds = ObjData.FillDataSetCNN(sql, cnn)
        ObjData = Nothing

        Return ds
    End Function

    <WebMethod()> _
    Public Sub ActualizarAlumno(ByVal matricula As String, ByVal nombre As String, ByVal familia As String, ByVal sucursalid As Integer, ByVal contactopadre As String, ByVal telefonopadre As String, ByVal emailpadre As String, ByVal contactomadre As String, ByVal telefonomadre As String, ByVal emailmadre As String, ByVal id As Integer, ByVal cnn As String)
        Dim sql As String = ""
        sql = "EXEC pAlumnos @cmd=9, @nombre='" & nombre & "', @matricula='" & matricula & "', @sucursalid='" & sucursalid & "', @familia='" & familia & "', @contacto_padre='" & contactopadre & "', @telefono_contacto_padre='" & telefonopadre & "', @email_contacto_padre='" & emailpadre & "', @contacto_madre='" & contactomadre & "', @telefono_contacto_madre='" & telefonomadre & "', @email_contacto_madre='" & emailmadre & "', @id='" & id & "'"

        Dim ObjData As New DataControl()
        ObjData.RunSQLQuery(sql, cnn)
        ObjData = Nothing

    End Sub

    <WebMethod()> _
    Public Function obteneralumnosactualizar(ByVal matricula As Integer, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pAlumnos @cmd=10,@matricula=" & matricula, cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function ingresarpedidoPOS(ByVal userid As Integer, ByVal alumnoid As Integer, ByVal comentarios As String, ByVal estatusid As Integer, ByVal sucursalid As Integer, ByVal cnn As String) As Long
        Dim sql As String = ""
        sql = "EXEC pPedidos @cmd=20, @userid='" & userid & "', @alumnoid='" & alumnoid & "', @comentarios='" & comentarios & "', @estatusid='" & estatusid & "', @sucursalid='" & sucursalid & "'"

        Dim pedidoidservice As Long
        Dim ObjData As New DataControl()
        pedidoidservice = ObjData.RunSQLQueryLong(sql, cnn)
        ObjData = Nothing

        Return pedidoidservice
    End Function

    <WebMethod()> _
    Public Sub ingresarpartidasPOS(ByVal pedidoid As Integer, ByVal codigo As String, ByVal cantidad As Integer, ByVal cnn As String)
        Dim ObjData As New DataControl()
        ObjData.RunSQLQuery("EXEC pPedidos @cmd=19, @pedidoid='" & pedidoid & "', @codigo='" & codigo & "', @cantidad='" & cantidad & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function obtenerpartidasPOS(ByVal pedidoid As Integer, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPedidos @cmd=21, @pedidoid=" & pedidoid, cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function obtenerpedidosactualizar(ByVal pedidoid As Integer, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPedidos @cmd=22, @pedidoid=" & pedidoid, cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Sub actualizapedidoPOS(ByVal comentarios As String, ByVal estatusid As Integer, ByVal pedidoid As Integer, ByVal cnn As String)
        Dim ObjData As New DataControl()
        ObjData.RunSQLQuery("EXEC pPedidos @cmd=23, @comentarios='" & comentarios & "', @estatusid='" & estatusid & "', @pedidoid='" & pedidoid & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Sub MarcarPartidaEntregada(ByVal folio As Integer, ByVal fechaentrega As String, ByVal userid As Integer, ByVal cnn As String)
        Dim ObjData As New DataControl()
        ObjData.RunSQLQuery("EXEC pEntregarPartidas @cmd=1, @userid='" & userid & "', @fechaentrega='" & fechaentrega & "', @id='" & folio & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Sub MarcarPedidoEntregado(ByVal folio As Integer, ByVal cnn As String)
        Dim ObjData As New DataControl()
        ObjData.RunSQLQuery("EXEC pEntregarPartidas @cmd=2, @id='" & folio & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Sub IngresarDescuentoPedidoPOS(ByVal pedidoid As Integer, ByVal descuento As Integer, ByVal cnn As String)
        Dim ObjData As New DataControl()
        ObjData.RunSQLQuery("EXEC pPedidos @cmd=24, @pedidoid='" & pedidoid & "', @descuento='" & descuento & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function obtenertotalpartidas(ByVal pedidoid As Integer, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPedidos @cmd=25,@pedidoid=" & pedidoid, cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function obtenerdescuentopedido(ByVal pedidoid As Integer, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPedidos @cmd=26,@pedidoid=" & pedidoid, cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function eliminarpartidaPOS(ByVal partidaid As Integer, ByVal cnn As String)
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPedidos @cmd=27, @partidaid=" & partidaid, cnn)
        ObjData = Nothing
    End Function

    <WebMethod()> _
    Public Function ActualizarMontoPedido(ByVal importe As Decimal, ByVal fechapago As String, ByVal pedidoid As Integer, ByVal cnn As String)
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPedidos @cmd=28,@importe=" & importe & ",@fechapago=" & fechapago & ",@pedidoid=" & pedidoid, cnn)
        ObjData = Nothing

    End Function

    <WebMethod()> _
    Public Function obtenerpartidasPOSVER(ByVal pedidoid As Integer, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPedidos @cmd=30,@pedidoid=" & pedidoid, cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leeproveedores(ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pMisProveedores @cmd=6", cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Sub reportaentradainventario(ByVal identradainventario As Integer, ByVal sucursalid As Integer, ByVal fecha As String, ByVal proveedorid As Integer, ByVal documento As String, ByVal userid As Integer, ByVal transferidobit As Boolean, ByVal cnn As String)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPosServices @cmd=33, @identradainventario='" & identradainventario & "', @sucursalid='" & sucursalid & "', @fechaentradainventario='" & fecha & "', @proveedorid='" & proveedorid & "', @documento='" & documento & "', @userid='" & userid & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Sub reportadetalleentradainventario(ByVal id As Integer, ByVal identradainventario As Integer, ByVal sucursalid As Integer, ByVal productoid As Integer, ByVal presentacionid As Integer, ByVal codigo As String, ByVal descripcion As String, ByVal unitario As Decimal, ByVal costo As Decimal, ByVal cantidad As Decimal, ByVal cnn As String)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPosServices @cmd=34, @iddetalleentradainv='" & id & "', @identradainventario='" & identradainventario & "', @sucursalid='" & sucursalid.ToString & "', @productoid='" & productoid & "', @presentacionid='" & presentacionid & "', @codigo='" & codigo & "', @descripcion='" & descripcion & "', @unitario='" & unitario & "', @costo='" & costo & "', @cantidad='" & cantidad & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function leefamilias(ByVal token As String, ByVal con As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=35, @token='" & token.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leesubfamilias(ByVal token As String, ByVal con As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=36, @token='" & token.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Sub reportaentradahistorialinventario(ByVal identradainventario As Integer, ByVal sucursalid As Integer, ByVal fecha As String, ByVal userid As Integer, ByVal cnn As String)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPosServices @cmd=37, @identradainventario='" & identradainventario & "', @userid='" & userid & "', @sucursalid='" & sucursalid & "', @fechaentradainventario='" & fecha & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Sub reportadetallehistorialentradainventario(ByVal id As Integer, ByVal identradainventario As Integer, ByVal sucursalid As Integer, ByVal productoid As Integer, ByVal codigo As String, ByVal categoria As String, ByVal subcategoria As String, ByVal descripcion As String, ByVal cantidad As Decimal, ByVal existencia As Decimal, ByVal diferencia As Decimal, ByVal unitario As Decimal, ByVal costo_estandar As Decimal, ByVal cnn As String)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPosServices @cmd=38, @iddetalleentradainv='" & id & "', @identradainventario='" & identradainventario & "', @sucursalid='" & sucursalid & "', @productoid='" & productoid & "', @codigo='" & codigo & "', @categoria='" & categoria & "', @subcategoria='" & subcategoria & "', @descripcion='" & descripcion & "', @cantidad='" & cantidad & "', @existencia='" & existencia & "', @diferencia='" & diferencia & "', @unitario='" & unitario & "', @costo_estandar='" & costo_estandar & "'", cnn)
        ObjData = Nothing
        'Dim valido As Boolean = False
        'Dim ObjData As New DataControl
        'Dim p As New ArrayList
        'p.Add(New SqlParameter("@cmd", 38))
    End Sub

    <WebMethod()> _
    Public Function actualizaTopeEfectivo(ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=39", cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function actualizaPrecioHorario(ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=40", cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function ActualizaDias(ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=41", cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function ActualizaProductoHorario(ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=42", cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function ActualizaProgramaLealtad(ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=43", cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leetasasimpuesto(ByVal token As String, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=43, @token='" & token.ToString & "'", cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function leetipopedido(ByVal token As String, ByVal cnn As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=44, @token='" & token.ToString & "'", cnn)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function logincontrolacceso(ByVal email As String, ByVal contrasena As String, ByVal clienteid As Long) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pControlAcceso @cmd=1, @email='" & email.ToString & "', @contrasena='" & contrasena.ToString & "', @clienteid='" & clienteid.ToString & "'")
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Sub actualizahuelladigital(ByVal userid As Integer, ByVal imgHuella As Byte())
        Dim ObjData As New DataControl
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 2))
        p.Add(New SqlParameter("@userid", userid))
        p.Add(New SqlParameter("@imgHuella", imgHuella))
        ObjData.ExecuteNonQueryWithParams("pControlAcceso", p)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Sub reportaasistencia(ByVal userid As Integer, ByVal token As String, ByVal con As String)
        Dim ObjData As New DataControl
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 1))
        p.Add(New SqlParameter("@userid", userid))
        p.Add(New SqlParameter("@token", token))
        ObjData.ExecuteNonQueryWithParamsCNN("pAsistencia", p, con)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function reportacortescaja(ByVal corteid As Integer, ByVal sucursalid As Integer, ByVal fechaini As String, ByVal fechafin As String, ByVal userid As Integer, ByVal estatusid As Integer, ByVal monto_inicial As Decimal, ByVal efectivo_billetes As Decimal, ByVal efectivo_monedas As Decimal, ByVal billetes20 As Decimal, ByVal billetes50 As Decimal, ByVal billetes100 As Decimal, ByVal billetes200 As Decimal, ByVal billetes500 As Decimal, ByVal billetes1000 As Decimal, ByVal monedas50c As Decimal, ByVal monedas1 As Decimal, ByVal monedas2 As Decimal, ByVal monedas5 As Decimal, ByVal monedas10 As Decimal, ByVal monedas20 As Decimal, ByVal efectivo As Decimal, ByVal vaciados As Decimal, ByVal tarjetas As Decimal, ByVal cheques As Decimal, ByVal credito As Decimal, ByVal monedero As Decimal, ByVal depositos As Decimal, ByVal transferencias As Decimal, ByVal web As Decimal, ByVal vales As Decimal, ByVal plataformasdigitales As Decimal, ByVal observaciones As String, ByVal procesada As Boolean, ByVal borradoBit As Boolean, ByVal entradas As Decimal, ByVal retiros As Decimal, ByVal tokenCaja As String, ByVal total_gastos As Decimal, ByVal promedio_venta As Decimal, ByVal total_transacciones As Integer, ByVal con As String) As Boolean
        Dim valido As Boolean = False
        Dim ObjData As New DataControl
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 1))
        p.Add(New SqlParameter("@corteid", corteid))
        p.Add(New SqlParameter("@sucursalid", sucursalid))
        p.Add(New SqlParameter("@fechaini", fechaini))
        p.Add(New SqlParameter("@fechafin", fechafin))
        p.Add(New SqlParameter("@userid", userid))
        p.Add(New SqlParameter("@estatusid", estatusid))
        p.Add(New SqlParameter("@monto_inicial", monto_inicial))
        p.Add(New SqlParameter("@efectivo_billetes", efectivo_billetes))
        p.Add(New SqlParameter("@efectivo_monedas", efectivo_monedas))
        p.Add(New SqlParameter("@billetes20", billetes20))
        p.Add(New SqlParameter("@billetes50", billetes50))
        p.Add(New SqlParameter("@billetes100", billetes100))
        p.Add(New SqlParameter("@billetes200", billetes200))
        p.Add(New SqlParameter("@billetes500", billetes500))
        p.Add(New SqlParameter("@billetes1000", billetes1000))
        p.Add(New SqlParameter("@monedas50c", monedas50c))
        p.Add(New SqlParameter("@monedas1", monedas1))
        p.Add(New SqlParameter("@monedas2", monedas2))
        p.Add(New SqlParameter("@monedas5", monedas5))
        p.Add(New SqlParameter("@monedas10", monedas10))
        p.Add(New SqlParameter("@monedas20", monedas20))
        p.Add(New SqlParameter("@efectivo", efectivo))
        p.Add(New SqlParameter("@vaciados", vaciados))
        p.Add(New SqlParameter("@tarjetas", tarjetas))
        p.Add(New SqlParameter("@cheques", cheques))
        p.Add(New SqlParameter("@credito", credito))
        p.Add(New SqlParameter("@monedero", monedero))
        p.Add(New SqlParameter("@depositos", depositos))
        p.Add(New SqlParameter("@transferencias", transferencias))
        p.Add(New SqlParameter("@web", web))
        p.Add(New SqlParameter("@vales", vales))
        p.Add(New SqlParameter("@plataformasdigitales", plataformasdigitales))
        p.Add(New SqlParameter("@observaciones", observaciones))
        p.Add(New SqlParameter("@procesada", procesada))
        p.Add(New SqlParameter("@borradoBit", borradoBit))
        p.Add(New SqlParameter("@entradas", entradas))
        p.Add(New SqlParameter("@retiros", retiros))
        p.Add(New SqlParameter("@tokenCaja", tokenCaja))
        p.Add(New SqlParameter("@total_gastos", total_gastos))
        p.Add(New SqlParameter("@promedio_venta", promedio_venta))
        p.Add(New SqlParameter("@total_transacciones", total_transacciones))
        valido = ObjData.ExecuteNonQueryTransactionCNN("pCortesCaja", p, con)
        ObjData = Nothing

        Return valido

    End Function

    <WebMethod()> _
    Public Function validaToken(ByVal token As String, ByVal con As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=45, @token='" & token.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function activaToken(ByVal token As String, ByVal sucursalid As Integer, ByVal con As String) As Integer
        Dim valido As Integer = 0
        Dim ObjData As New DataControl
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 46))
        p.Add(New SqlParameter("@token", token))
        p.Add(New SqlParameter("@sucursalid", sucursalid))
        valido = ObjData.SentenceScalarLong("pPosServices", 1, p, con)
        ObjData = Nothing

        Return valido

    End Function

    <WebMethod()> _
    Public Function leeproductosproveedor(ByVal token As String, ByVal con As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=47, @token='" & token.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function reportatransferencias(ByVal sucursalId As Integer, ByVal con As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pTransferencia @cmd=11, @sucursalid='" & sucursalId.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function reportadetalleferencias(ByVal sucursalId As Integer, ByVal con As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pTransferencia @cmd=12, @sucursalid='" & sucursalId.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function reportatransferidotransferencia(ByVal transferenciaid As Integer, ByVal con As String) As Boolean
        Dim valido As Boolean = False
        Dim ObjData As New DataControl
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 13))
        p.Add(New SqlParameter("@transferenciaid", transferenciaid))
        valido = ObjData.ExecuteNonQueryTransactionCNN("pTransferencia", p, con)
        ObjData = Nothing

        Return valido

    End Function

    <WebMethod()> _
    Public Function reportatransferidotransferenciadetalle(ByVal id As Integer, ByVal con As String) As Boolean
        Dim valido As Boolean = False
        Dim ObjData As New DataControl
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 14))
        p.Add(New SqlParameter("@id", id))
        valido = ObjData.ExecuteNonQueryTransactionCNN("pTransferencia", p, con)
        ObjData = Nothing

        Return valido

    End Function

    <WebMethod()> _
    Public Function actualizacantidadrecibidatransferencia(ByVal id As Integer, ByVal transferenciaid As Integer, ByVal recibido As Decimal, ByVal con As String) As Boolean
        Dim valido As Boolean = False
        Dim ObjData As New DataControl
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 15))
        p.Add(New SqlParameter("@id", id))
        p.Add(New SqlParameter("@transferenciaid", transferenciaid))
        p.Add(New SqlParameter("@recibido", recibido))
        valido = ObjData.ExecuteNonQueryTransactionCNN("pTransferencia", p, con)
        ObjData = Nothing

        Return valido

    End Function

    <WebMethod()> _
    Public Function actualizaestatustransferencia(ByVal transferenciaid As Integer, ByVal estatusid As Integer, ByVal con As String) As Boolean
        Dim valido As Boolean = False
        Dim ObjData As New DataControl
        Dim p As New ArrayList
        p.Add(New SqlParameter("@cmd", 16))
        p.Add(New SqlParameter("@transferenciaid", transferenciaid))
        p.Add(New SqlParameter("@estatusid", estatusid))
        valido = ObjData.ExecuteNonQueryTransactionCNN("pTransferencia", p, con)
        ObjData = Nothing

        Return valido

    End Function

    <WebMethod()> _
    Public Function leeactivosfijos(ByVal token As String, ByVal con As String, ByVal sucursalid As Integer, ByVal serach As String) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=48, @token='" & token.ToString & "', @sucursalid='" & sucursalid.ToString & "', @txtSearch='" & serach.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Sub reportavalescaja(ByVal token As String, ByVal id As Integer, ByVal userid As Integer, ByVal nombre As String, ByVal concepto As String, ByVal monto As Decimal, ByVal cajaid As Integer, ByVal fecha As String, ByVal sucursalid As Integer, ByVal cnn As String)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPosServices @cmd=49, @token='" & token.ToString & "', @id='" & id.ToString & "', @userid='" & userid.ToString & "', @nombre='" & nombre.ToString & "', @concepto='" & concepto.ToString & "', @monto='" & monto.ToString & "', @cajaid='" & cajaid.ToString & "', @fecha='" & fecha.ToString & "', @sucursalid='" & sucursalid.ToString & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Sub reportaretiroscaja(ByVal token As String, ByVal id As Integer, ByVal userid As Integer, ByVal nombre As String, ByVal concepto As String, ByVal monto As Decimal, ByVal cajaid As Integer, ByVal fecha As String, ByVal billetes20 As Integer, ByVal billetes50 As Integer, ByVal billetes100 As Integer, ByVal billetes200 As Integer, ByVal billetes500 As Integer, ByVal billetes1000 As Integer, ByVal monedas50c As Integer, ByVal monedas1 As Integer, ByVal monedas2 As Integer, ByVal monedas5 As Integer, ByVal monedas10 As Integer, ByVal monedas20 As Integer, ByVal sucursalid As Integer, ByVal cnn As String)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPosServices @cmd=50, @token='" & token.ToString & "', @id='" & id.ToString & "', @userid='" & userid.ToString & "', @nombre='" & nombre.ToString & "', @concepto='" & concepto.ToString & "', @monto='" & monto.ToString & "', @cajaid='" & cajaid.ToString & "', @fecha='" & fecha.ToString & "', @billetes20='" & billetes20.ToString & "', @billetes50='" & billetes50.ToString & "', @billetes100='" & billetes100.ToString & "', @billetes200='" & billetes200.ToString & "', @billetes500='" & billetes500.ToString & "', @billetes1000='" & billetes1000.ToString & "', @monedas50c='" & monedas50c.ToString & "', @monedas1='" & monedas1.ToString & "', @monedas2='" & monedas2.ToString & "', @monedas5='" & monedas5.ToString & "', @monedas10='" & monedas10.ToString & "', @monedas20='" & monedas20.ToString & "', @sucursalid='" & sucursalid.ToString & "'", cnn)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function leeconfiguracionventapresentaciones(ByVal token As String, ByVal con As String, ByVal sucursalid As Integer) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=51, @token='" & token.ToString & "', @sucursalid='" & sucursalid.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Sub actualizaconfiguracionventapresentaciones(ByVal token As String, ByVal con As String, ByVal sucursalid As Integer)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pPosServices @cmd=52, @token='" & token.ToString & "', @sucursalid='" & sucursalid.ToString & "'", con)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function leersucursalestransferencia(ByVal token As String, ByVal con As String, ByVal sucursalid As Integer) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=53, @token='" & token.ToString & "', @sucursalid='" & sucursalid.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function agregartransferencia(ByVal con As String, ByVal userid As Integer, ByVal origenid As Integer, ByVal destinoid As Integer, ByVal comentario As String) As Long
        Dim transferenciaid As New Long
        Dim ObjData As New DataControl
        transferenciaid = ObjData.RunSQLQueryLong("exec pTransferencia @cmd=1, @userid='" & userid.ToString & "', @origenid='" & origenid.ToString & "', @destinoid='" & destinoid.ToString & "', @comentario='" & comentario.ToString & "'", con)
        ObjData = Nothing
        Return transferenciaid
    End Function

    <WebMethod()> _
    Public Function consultardatostransferencia(ByVal con As String, ByVal transferenciaid As Long) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pTransferencia @cmd=2, @transferenciaid='" & transferenciaid.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Sub agregarconceptotransferencia(ByVal con As String, ByVal transferenciaid As Long, ByVal cantidad As Decimal, ByVal productoid As Integer)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pTransferencia @cmd=3, @transferenciaid='" & transferenciaid.ToString & "', @cantidad='" & cantidad.ToString & "', @productoid='" & productoid.ToString & "', @presentacionid=0, @factor=0", con)
        ObjData = Nothing
    End Sub

    <WebMethod()> _
    Public Function consultardetalletransferencia(ByVal con As String, ByVal transferenciaid As Long) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pTransferencia @cmd=6, @transferenciaid='" & transferenciaid.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Function consultaproductotransferencia(ByVal con As String, ByVal codigo As String, ByVal sucursalid As Integer) As DataSet
        Dim ds As New DataSet
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSetCNN("exec pPosServices @cmd=54, @codigo='" & codigo.ToString & "', @sucursalid='" & sucursalid.ToString & "'", con)
        ObjData = Nothing
        Return ds
    End Function

    <WebMethod()> _
    Public Sub finalizartransferencia(ByVal con As String, ByVal transferenciaid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pTransferencia @cmd=8, @transferenciaid='" & transferenciaid.ToString & "'", con)
        ObjData = Nothing
    End Sub

End Class