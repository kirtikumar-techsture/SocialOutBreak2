Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System.Threading
Public Class lead_tutorial
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim obj As New BAlsidebar
                Dim ds1 As New DataSet
                obj.Page = CStr(HttpContext.Current.Request.Url.AbsolutePath)

                ds1 = obj.GetVideoTutorial()

                If HttpContext.Current.Request.Url.AbsolutePath = ds1.Tables(0).Rows(0).Item("vt_Page").ToString Then

                    'strVideo1.HRef = ds1.Tables(0).Rows(0).Item("vt_Video").ToString & "?fs=1&autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer"
                    'strVideo1.Title = ds1.Tables(0).Rows(0).Item("vt_Title").ToString
                    'btnVideo.HRef = ds1.Tables(0).Rows(0).Item("vt_Video").ToString & "?fs=1&autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer"
                    'btnVideo.Title = ds1.Tables(0).Rows(0).Item("vt_Title").ToString
                    'imgsweepstake.Src = ds1.Tables(0).Rows(0).Item("vt_VideoThumbnail").ToString
                End If

            Else
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub pdfdownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pdfdownload.Click
        Response.AppendHeader("Content-disposition", "attachment; filename=Summa-Social-Dashboard.pdf")
        Response.ContentType = "application/pdf"
        Response.WriteFile(Server.MapPath("~") & "Summa-Social-Dashboard.pdf")
        Response.End()
    End Sub
End Class