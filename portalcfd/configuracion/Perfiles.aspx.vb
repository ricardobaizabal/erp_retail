Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Public Class Perfiles
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblTitulo.Text = "Listado de Perfiles"
            lblEditarTitle.Text = "Agregar/Editar Perfil"

        End If

    End Sub

End Class