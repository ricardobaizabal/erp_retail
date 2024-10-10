'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class FacturarPedido

    '''<summary>
    '''Control RadWindowManager2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RadWindowManager2 As Global.Telerik.Web.UI.RadWindowManager

    '''<summary>
    '''Control RadAjaxPanel1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RadAjaxPanel1 As Global.Telerik.Web.UI.RadAjaxPanel

    '''<summary>
    '''Control panelClients.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panelClients As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control imgPanel1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imgPanel1 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control lblClientsSelectionLegend.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblClientsSelectionLegend As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control cmbSucursal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbSucursal As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control cmbMoneda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbMoneda As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtTipoCambio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTipoCambio As Global.Telerik.Web.UI.RadNumericTextBox

    '''<summary>
    '''Control valSucursal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valSucursal As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valMoneda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valMoneda As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valTipoCambio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valTipoCambio As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control rblTipoCliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rblTipoCliente As Global.System.Web.UI.WebControls.RadioButtonList

    '''<summary>
    '''Control cmbCliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbCliente As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control cmbDocumento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbDocumento As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control cmbMetodoPago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbMetodoPago As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control valClienteID.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valClienteID As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valSerieId.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valSerieId As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valMetodoPago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valMetodoPago As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lblTipoRelacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTipoRelacion As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblUUID.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblUUID As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control cmbTipoRelacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbTipoRelacion As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtFolioFiscal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFolioFiscal As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control valTipoRelecion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valTipoRelecion As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valFolioFiscal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valFolioFiscal As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control panelSpecificClient.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panelSpecificClient As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control Image4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Image4 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control lblDatosCliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblDatosCliente As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblRazonSocial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblRazonSocial As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblRFC.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblRFC As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtRazonSocial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtRazonSocial As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control txtRFC.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtRFC As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control valRazonSocial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valRazonSocial As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valRFCRequerido.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valRFCRequerido As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valRFC.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valRFC As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblDenominacionRazonSocial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblDenominacionRazonSocial As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtDenominacionRazonSocial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDenominacionRazonSocial As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control valDenominacionRazonSocial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valDenominacionRazonSocial As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lblContacto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblContacto As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblEmailContacto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblEmailContacto As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblTelefonoContacto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTelefonoContacto As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtContacto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtContacto As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control txtEmailContacto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEmailContacto As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control txtTelefonoContacto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTelefonoContacto As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control valEmailContacto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valEmailContacto As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblCalle.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCalle As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblNoExt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblNoExt As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblNoInt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblNoInt As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblColonia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblColonia As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtCalle.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCalle As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control txtNumeroExt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNumeroExt As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control txtNumeroInt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNumeroInt As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control txtColonia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtColonia As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control valCalle.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valCalle As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valNumeroExt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valNumeroExt As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valColonia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valColonia As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lblPais.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPais As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblEstado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblEstado As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblMunicipio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblMunicipio As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtPais.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPais As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control cmbEstado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbEstado As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtMunicipio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMunicipio As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control valPais.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valPais As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valEstado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valEstado As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valMunicipio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valMunicipio As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lblCP.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCP As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblCondicionesPago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCondicionesPago As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtCP.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCP As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control cmbCondiciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbCondiciones As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control valCP.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valCP As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lblTipoContribuyente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTipoContribuyente As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblFormaPagoCliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFormaPagoCliente As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblNumCtaPago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblNumCtaPago As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control cmbTipoContribuyente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbTipoContribuyente As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control cmbFormaPago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbFormaPago As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtNumCtaPago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNumCtaPago As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control valTipoContribuyente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valTipoContribuyente As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valFormaPagoCliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valFormaPagoCliente As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lblRegimenFiscal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblRegimenFiscal As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control cmbRegimenFiscal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbRegimenFiscal As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control valRegimenFiscal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valRegimenFiscal As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lblUsoCFD.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblUsoCFD As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control cmbUsoCFD.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbUsoCFD As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control valUsoCFDCliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valUsoCFDCliente As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lblInstruccionesEspeciales.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblInstruccionesEspeciales As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control instrucciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents instrucciones As Global.Telerik.Web.UI.RadTextBox

    '''<summary>
    '''Control serie.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents serie As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control folio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents folio As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control tipoidF.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents tipoidF As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control panelFacturaGlobal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panelFacturaGlobal As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control Image6.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Image6 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control Label2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label2 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control valcmbPeriodicidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valcmbPeriodicidad As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valcmbMes.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valcmbMes As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control valtxtAnio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents valtxtAnio As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control cmbPeriodicidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbPeriodicidad As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control cmbMes.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbMes As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtAnio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtAnio As Global.Telerik.Web.UI.RadNumericTextBox

    '''<summary>
    '''Control panelItemsRegistration.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panelItemsRegistration As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control productoid.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents productoid As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control Image2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Image2 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control lblClientItems.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblClientItems As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control itemsList.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents itemsList As Global.Telerik.Web.UI.RadGrid

    '''<summary>
    '''Control panelResume.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panelResume As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control Image3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Image3 As Global.System.Web.UI.WebControls.Image

    '''<summary>
    '''Control lblResume.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblResume As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblSubTotal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblSubTotal As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblSubTotalValue.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblSubTotalValue As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblDescuento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblDescuento As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblDescuentoValue.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblDescuentoValue As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblIVA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblIVA As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblIVAValue.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblIVAValue As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblIEPS.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblIEPS As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblIEPSValue.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblIEPSValue As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblRetISR.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblRetISR As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblRetISRValue.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblRetISRValue As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblRetIVA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblRetIVA As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblRetIVAValue.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblRetIVAValue As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblTotal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTotal As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblTotalValue.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTotalValue As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control btnCreateInvoice.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnCreateInvoice As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btnCancelInvoice.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnCancelInvoice As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control RadWindow1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RadWindow1 As Global.Telerik.Web.UI.RadWindow

    '''<summary>
    '''Control txtErrores.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtErrores As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAceptar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAceptar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control RadAjaxLoadingPanel1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RadAjaxLoadingPanel1 As Global.Telerik.Web.UI.RadAjaxLoadingPanel
End Class
