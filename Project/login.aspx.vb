﻿Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      
        Try
            hdnIdOfUser.Value = Request.QueryString("u")
            Session("OpenWindow") = hdnIdOfUser.Value
            If Request.QueryString("i") <> "" AndAlso IsNumeric(Request.QueryString("i")) Then
                Session("Industry") = Request.QueryString("i")

            End If
            If Request.QueryString("c") <> "" AndAlso IsNumeric(Request.QueryString("c")) Then
                Session("company") = Request.QueryString("c")
            End If

           
            Dim clientId As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId")
            Dim clientSecret As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey")
            Dim redirectUri As String = System.Configuration.ConfigurationManager.AppSettings("AppPath") & "handshake.aspx"
            'Dim Url As String = "https://graph.facebook.com/oauth/authorize?type=web_server&client_id={0}&redirect_uri={1}&scope=manage_pages,publish_stream,user_photos,user_photo_video_tags,photo_upload,user_videos,video_upload&display=popup"
            'Dim Url As String = "https://graph.facebook.com/oauth/authorize?type=web_server&client_id={0}&redirect_uri={1}&scope=manage_pages,publish_stream,offline_access,photo_upload,video_upload,email&display=popup"
            Dim Url As String = "https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&scope=manage_pages,publish_stream,offline_access,photo_upload,video_upload,email&display=popup"
            Response.Redirect(String.Format(Url, clientId, redirectUri))
            'Else
            'Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
            'End If
        Catch
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Error", "$('#diverr').css('display','block');", True)
        End Try
    End Sub

End Class