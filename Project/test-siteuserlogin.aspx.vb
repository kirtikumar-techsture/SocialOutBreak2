Imports System.Configuration
Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Runtime.Serialization.Json
Imports Facebook
Imports BusinessAccessLayer.BusinessLayer
Public Class test_siteuserlogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsPostBack Then
        'GenerateImage()
        'pnlmsg.Visible = False
        'spnEror.InnerText = ""
        'End If
    End Sub

    Private Sub imgLogin1_ServerClick(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgLogin1.ServerClick
        Response.Write("test")

    End Sub
End Class