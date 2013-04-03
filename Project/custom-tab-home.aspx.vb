Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System.Threading

Public Class custom_tab_home
    Inherits System.Web.UI.Page
	
	Public strPageId As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim obj As New BALCustomTab
                Dim ds1 As New DataSet
                obj.Page = CStr(HttpContext.Current.Request.Url.AbsolutePath)

                ds1 = obj.GetVideoTutorial()

                Dim dsimage As New DataSet
                Dim objBALimage As New BALCustomTab
                objBALimage.PageIndex = 0 'ViewState("currentpageindex")
                objBALimage.CompanyId = GetSetCookies.GetCookie("CompanyId") 'Session("CompanyId")
                objBALimage.IndustryId = GetSetCookies.GetCookie("IndustryId") 'Session("IndustryId")
                dsimage = objBALimage.GetCustomTabMasterByCompOrIndustry
                If dsimage.Tables(1).Rows.Count > 0 Then
                    imgtop.Src = "Content/images/customtab/" & dsimage.Tables(1).Rows(0).Item("ctl_image").ToString()
                Else
                    lblNoSidebar1.Visible = True
                    pnlSidebar.Visible = False
                End If

                If HttpContext.Current.Request.Url.AbsolutePath = ds1.Tables(0).Rows(0).Item("vt_Page").ToString Then

                    strVideo1.HRef = ds1.Tables(0).Rows(0).Item("vt_Video").ToString & "?fs=1&autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer"
                    strVideo1.Title = ds1.Tables(0).Rows(0).Item("vt_Title").ToString
                    btnVideo.HRef = ds1.Tables(0).Rows(0).Item("vt_Video").ToString & "?fs=1&autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer"
                    btnVideo.Title = ds1.Tables(0).Rows(0).Item("vt_Title").ToString
                    imgcustomtab.Src = ds1.Tables(0).Rows(0).Item("vt_VideoThumbnail").ToString
                End If
                If Page.IsPostBack = False Then
                    bindTotalFanPage()
                    bind_myCustomTabs(0)
                End If

            Else
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
            End If
        Catch ex As Exception
            '  lblMessage.Text = "Error: " & ex.Message
        End Try
        'imgsidebar.Src = "Content/images/video_img.jpg"

        'strVideo1.HRef = "http://www.youtube.com/v/pp_CLpsmjo0"
        'strVideo1.Title = "Watch Sidebar Videos"
        'imgcreatepage.Src = "Content/images/video_img.jpg"

        'strVideo1.HRef = "http://www.youtube.com/v/yz-bJQtSAx0?fs=1&autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer"
        'strVideo1.Title = "Setup A New Page"

    End Sub


    Sub bindTotalFanPage()
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Dim accessToken As String = Session("FacebookAccessToken")
            Dim clientId As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
            Dim clientSecret As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey").ToString()
            Dim url As String = "https://graph.facebook.com/me/accounts?fields=id,name,picture,category,access_token&return_ssl_resources=true&access_token={0}"
            Dim request As WebRequest = WebRequest.Create(String.Format(url, accessToken))
            Dim response1 As WebResponse = request.GetResponse()
            Dim stream As Stream = response1.GetResponseStream()
            Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(FanPage))

            Dim fPage As New FanPage()
            fPage = TryCast(dataContractJsonSerializer.ReadObject(stream), FanPage)
            Dim listPages As New List(Of FanPage.m_data)

            For Each item1 As FanPage.m_data In fPage.data
                listPages.Add(item1)
            Next

            Dim cnt As Integer = listPages.Count

            Dim aid As String = ""
            For i As Integer = 0 To cnt - 1
                strPageId = strPageId + listPages.Item(i).id & ","
                Session("strPageId") = strPageId
            Next
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub
	
    Private Sub bind_myCustomTabs(ByVal direction As Integer)
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            If Not Session("SiteUserId") Is Nothing Then
                Dim ds As New DataSet
                Dim obj As New BALCustomTab
                obj.UserId = Session("SiteUserId")
                obj.FBUserId = If(Session("FacebookUserId") <> Nothing, Session("FacebookUserId"), "")
                obj.FBPageId = If(Session("strPageId") <> Nothing, Session("strPageId"), "")
                obj.FBUserEmail = If(Session("FacebookUserEmail") <> Nothing, Session("FacebookUserEmail"), "")
                obj.Cid = hdnCustomTab.Value
                obj.Direction = direction
                obj.CompanyId = GetSetCookies.GetCookie("CompanyId") 'Session("CompanyId")
                obj.IndustryId = GetSetCookies.GetCookie("IndustryId") 'Session("IndustryId")
                ds = obj.GetMyCustomTabs()
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("ct_Image") = "" Then
                        imgMyCustomTab.Src = "/content/images/nomysidebar.jpg"
                    Else
                        ltrName.Text = ds.Tables(0).Rows(0).Item("ct_Name").ToString
                        imgMyCustomTab.Src = "/uploads/" & ds.Tables(0).Rows(0).Item("ct_Image")
                        hdnImageName.Value = ds.Tables(0).Rows(0).Item("ct_Image")
                        ViewState("ct_FBPageId") = ds.Tables(0).Rows(0).Item("ct_FBPageId")
                        ViewState("ct_FBPageAccessToken") = ds.Tables(0).Rows(0).Item("ct_FBPageAccessToken")
                    End If
                    hdnCustomTab.Value = ds.Tables(0).Rows(0).Item("ct_Id")
                    hdnMasterId.Value = ds.Tables(0).Rows(0).Item("ct_Id")
                Else
                    imgMyCustomTab.Style.Add("display", "none")
                    lblNoSidebar.Visible = True
                    ibtnNext.Visible = False
                    ibtnPre.Visible = False
                    ltrName.Text = ""
                End If
            End If
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub

    Private Sub ibtnNext_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnNext.Click
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            bind_myCustomTabs(1)
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub

    Private Sub ibtnPre_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtnPre.Click
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            bind_myCustomTabs(0)
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub

    'Private Sub lnlDelete_Click(sender As Object, e As System.EventArgs) Handles lnlDelete.Click
    '    If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
    '        Dim ds As New DataSet
    '        Dim obj As New BALCustomTab
    '        obj.UserId = 1 'Session("SiteUserId")
    '        obj.Cid = hdnCustomTab.Value
    '        obj.DeleteMyCustomTabs()
    '        hdnCustomTab.Value = 0
    '        bind_myCustomTabs(1)
    '    Else
    '        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
    '    End If
    'End Sub

    Private Sub lnlPublish_Click(sender As Object, e As System.EventArgs) Handles lnlPublish.Click
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            If hdnImageName.Value <> "0" Then
                Response.Redirect("custom-tabs.aspx?id=-" & hdnCustomTab.Value)
            End If
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub

    Private Sub lnkdelete1_ServerClick(sender As Object, e As System.EventArgs) Handles lnkdelete1.ServerClick
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Dim obj As New BALCustomTab
            If Not IsDBNull(ViewState("ct_IsPublished")) AndAlso ViewState("ct_IsPublished") = 1 Then
                If ViewState("ct_FBPageId") <> "" And ViewState("ct_FBPageAccessToken") <> "" Then
                    Dim result As String = ""
                    Dim result1 As String = ""
                    Dim strPageId As String = ""
                    Dim AppID = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
                    Dim url1 As String = "https://graph.facebook.com/{0}/tabs/{2}?access_token={1}"
                    Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url1, ViewState("ct_FBPageId"), ViewState("ct_FBPageAccessToken"), AppID))
                    Dim fbResponse As WebResponse = fbRequest.GetResponse()
                    Dim stream As Stream = fbResponse.GetResponseStream()
                    Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(checkApp))
                    Dim fPage As New checkApp()
                    fPage = TryCast(dataContractJsonSerializer.ReadObject(stream), checkApp)
                    Dim listAlbums As New List(Of checkApp.m_data)

                    For Each item1 As checkApp.m_data In fPage.data
                        listAlbums.Add(item1)
                    Next
                    'Response.Write(listAlbums.Count & "   teste")
                    'Response.End()

                    If listAlbums.Count > 0 Then
                        strPageId = ViewState("ct_FBPageId") '"103823149672939"
                        Dim strPageAccessToken As String = ViewState("ct_FBPageAccessToken") '"AAAD2ZCc3HZAxABAIDxzyAwOqZCgzSRFVkLHqiLl88muU7QIRYTE1ZBEnPM7GTL7yZCbs7REZA0cVul1DsSWs2ntOKZB4boRTDPDZB8htHZBcn3C5Jlnlnqw4R"

                        Dim url As String = "https://graph.facebook.com/{0}/tabs?app_id={3}&access_token={1}&method=delete&custom_name={2}"
                        Dim fbRequest1 As WebRequest = WebRequest.Create(String.Format(url, strPageId, strPageAccessToken, "", AppID))
                        Dim fbResponse1 As WebResponse = fbRequest1.GetResponse()
                        Dim stream1 As Stream = fbResponse1.GetResponseStream()
                        Dim encode1 As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                        Dim streamReader1 As New StreamReader(stream1, encode1)
                        result = streamReader1.ReadToEnd()
                        streamReader1.Close()
                    End If

                End If
            End If
            'End If
            Dim ds As New DataSet
            obj.UserId = Session("SiteUserId")
            obj.Cid = hdnCustomTab.Value
            obj.DeleteMyCustomTabs()

            hdnCustomTab.Value = 0
            bind_myCustomTabs(1)
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub

    Private Sub lnkCopy_ServerClick(sender As Object, e As System.EventArgs) Handles lnkCopy.ServerClick
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Dim ds As New DataSet
            Dim obj As New BALCustomTab
            obj.UserId = Session("SiteUserId")
            obj.FBUserId = Session("FacebookUserId")
            obj.Cid = hdnCustomTab.Value
            ds = obj.CopyMyCustomTabs()
            If ds.Tables(0).Rows.Count > 0 Then
                Response.Redirect("custom-tabs.aspx?id=" & ds.Tables(0).Rows(0).Item("CopyId"))
            End If
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub

End Class