Imports System.Web
Imports System.IO
Public Class support
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        If CInt(GetSetCookies.GetCookie("CompanyId")) = 14 Then
            tdVisalus.Visible = True
            tdSupport.Visible = False
        Else
            tdVisalus.Visible = False
            tdSupport.Visible = True
        End If
        If Session("FacebookAccessToken") = Nothing AndAlso Session("FacebookAccessToken") = "" Then
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
           

        End If
        'Catch ex As Exception
        '    'lblmessage.Text = "Error: " & ex.Message
        'End Try
    End Sub

End Class