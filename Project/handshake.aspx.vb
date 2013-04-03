Imports System.Configuration
Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Runtime.Serialization.Json
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook

Public Class handshake
    Inherits System.Web.UI.Page
    Dim objFBUserPagesDetails As New FacebookUserAndPagesDetails
    Public strPageId As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        hdnId.Value = Session("OpenWindow")
        Dim clientId As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId")
        Dim clientSecret As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey")
        Dim url As String = "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}"
        Dim redirectUri As String = System.Configuration.ConfigurationManager.AppSettings("AppPath") & "handshake.aspx"
        Dim request1 As WebRequest = WebRequest.Create(String.Format(url, clientId, redirectUri, clientSecret, Request.QueryString("code")))

        Dim response1 As WebResponse = request1.GetResponse()
        Dim stream As Stream = response1.GetResponseStream()
        Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
        Dim streamReader As New StreamReader(stream, encode)
        Dim accessToken As String = streamReader.ReadToEnd().Replace("access_token=", "")
        streamReader.Close()
        'response.Close()
        'ViewData("Token") = accessToken
        Session("FacebookAccessToken") = accessToken
        'lblAccessToken.Text = Session("FacebookAccessToken")
        url = "https://graph.facebook.com/me?access_token={0}"
        request1 = WebRequest.Create(String.Format(url, accessToken))
        response1 = request1.GetResponse()
        stream = response1.GetResponseStream()
        Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(FacebookUser))

        Dim user As New FacebookUser()
        user = TryCast(dataContractJsonSerializer.ReadObject(stream), FacebookUser)
        'response.Close()
        Session("FacebookUserId") = user.id
        Session("FacebookUserName") = user.name
        Session("FacebookUserEmail") = user.email

        If user.outherror IsNot Nothing Then
            If user.outherror.type = "OAuthException" Then
                objFBUserPagesDetails.ErrorUploadedData = "Error At Login In Application"
                objFBUserPagesDetails.OuthErrorDetails = user.outherror.message
                objFBUserPagesDetails.OuthErrorCode = user.outherror.code
                objFBUserPagesDetails.OuthErrorType = user.outherror.type
                objFBUserPagesDetails.AddOuthErrorDetails()

                Dim Url1 As String = "https://graph.facebook.com/oauth/authorize?type=web_server&client_id={0}&redirect_uri={1}&scope=offline_access,manage_pages,publish_stream,user_photos,user_photo_video_tags,photo_upload,user_videos,read_stream,email&display=popup"
                Response.Redirect(String.Format(Url1, clientId, redirectUri))
            Else
                objFBUserPagesDetails.ErrorUploadedData = "Error At Login In Application"
                objFBUserPagesDetails.OuthErrorDetails = "Other Error Occurs"
                objFBUserPagesDetails.OuthErrorCode = ""
                objFBUserPagesDetails.OuthErrorType = "Other Error"
                objFBUserPagesDetails.AddOuthErrorDetails()

                Dim Url1 As String = "https://graph.facebook.com/oauth/authorize?type=web_server&client_id={0}&redirect_uri={1}&scope=offline_access,manage_pages,publish_stream,user_photos,user_photo_video_tags,photo_upload,user_videos,read_stream,email&display=popup"
                Response.Redirect(String.Format(Url1, clientId, redirectUri))
            End If
        Else
            objFBUserPagesDetails.FBUSerId = user.id
            objFBUserPagesDetails.TSMAUserId = Session("SiteUserId")
            objFBUserPagesDetails.FBUSerFirstName = user.first_name
            objFBUserPagesDetails.FBUSerLastName = user.last_name
            objFBUserPagesDetails.FBUSerName = user.username
            objFBUserPagesDetails.FBUSerToken = Session("FacebookAccessToken")
            objFBUserPagesDetails.FBUserEmail = user.email
            objFBUserPagesDetails.FBUserTokenExpirationDate = "Never"
            objFBUserPagesDetails.AddUpdateFBUserDetails()
            'Response.Write("AccessToken:  " & Session("FacebookAccessToken") & "<br/>")
            'Response.Write("Name: " & Session("FacebookUserName") & "Id" & Session("FacebookUserId") & " Email: " & Session("FacebookUserEmail"))
            'Response.End()

            Dim strUserId As String = user.id.ToString
            Dim objBAL As New BALCompanyIndusty
            '   Response.Write(Session("FacebookUserId") & "<br/>")
            '            Response.Write(Session("Industry") & "<br/>")
            '            Response.Write(Session("Company") & "<br/>")
            '            Response.End()
            'Response.End()
            If Session("Industry") <> Nothing AndAlso IsNumeric(Session("Industry")) AndAlso Session("Industry") <> -1 Then
                Dim ds As New DataSet
                objBAL.FBUserId = Session("FacebookUserId")
                objBAL.CompanyId = Session("Industry")
                objBAL.GetIndustryUserDetails()

            End If
            If Session("Company") <> Nothing AndAlso IsNumeric(Session("Company")) AndAlso Session("Company") <> -1 Then
                Dim ds As New DataSet
                'Dim objBAL As New BALCompanyIndusty
                objBAL.FBUserId = Session("FacebookUserId")
                objBAL.CompanyId = Session("Company")
                objBAL.GetCompnayUserDetails()
            End If
            Session("Company") = 0
            Session("Industry") = 0

            Dim dsCompInd As New DataSet
            Dim objBALCompInd As New BALCompanyIndusty
            objBALCompInd.FBUserId = Session("FacebookUserId")
            dsCompInd = objBALCompInd.GetCompnayIndustryName
            'Dim strName As String = ""

            'For Each strResult As String In dsCompInd.Tables(0).Rows(0).Item("Name").ToString()
            '    strName = strResult.ToString
            '    Response.Write(strName)
            '    Exit For
            'Next
            'Response.Write(dsCompInd.Tables(0).Rows(0).Item("Name").ToString())

            If dsCompInd.Tables(0).Rows.Count > 0 Then
                'If dsCompInd.Tables(0).Rows(0).Item("Name") <> "" Then
                Dim intIndustryId = dsCompInd.Tables(0).Rows(0).Item("IndustryId") ' 1
                Dim intCompanyId = dsCompInd.Tables(0).Rows(0).Item("CompanyId") ' 0
                'Session("IndustryId") = dsCompInd.Tables(0).Rows(0).Item("IndustryId")
                'Session("CompanyId") = dsCompInd.Tables(0).Rows(0).Item("CompanyId")
                Dim strName As String = dsCompInd.Tables(0).Rows(0).Item("Name").ToString() 'Session("FacebookUserName") 
                GetSetCookies.SetCookie("SelectedName", strName, 1)
                GetSetCookies.SetCookie("IndustryId", intIndustryId, 1)
                GetSetCookies.SetCookie("CompanyId", intCompanyId, 1)
                'End If
            End If

            BindAllFanPages()
        End If
        'Utils.SetCookie("SelectedName", strName, 1)
        'Session("FacebookUserId") = "100003192937027"
        'Session("FacebookAccessToken") = "AAAD2ZCc3HZAxABAG41OqIjnjvtea6SkNxR9qnxeIQka8IFH2xQZBi4XZB42YZBItWK2U9qqrR8THDYK2dU8V9Kv7zvuI2oykL3AjzMZCR1VgZDZD"
        'Catch
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Error", "$('#diverr').css('display','block');", True)
        'End Try

    End Sub

    Sub BindAllFanPages()
       Try
            Dim accessToken1 As String = Session("FacebookAccessToken")
            Dim clientId1 As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
            Dim clientSecret1 As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey").ToString()
            Dim url1 As String = "https://graph.facebook.com/me/accounts?fields=id,name,link,picture,category,access_token&return_ssl_resources=true&access_token={0}"
            Dim fbRequest1 As WebRequest = WebRequest.Create(String.Format(url1, accessToken1))
            Dim fbResponse1 As WebResponse = fbRequest1.GetResponse()
            Dim stream1 As Stream = fbResponse1.GetResponseStream()
            Dim dataContractJsonSerializer1 As New DataContractJsonSerializer(GetType(FanPage))

            Dim fPage1 As New FanPage()
            fPage1 = TryCast(dataContractJsonSerializer1.ReadObject(stream1), FanPage)


            Dim url2 As String = "https://graph.facebook.com/me?fields=id,name,link,picture&return_ssl_resources=true&access_token={0}"
            Dim request1 As WebRequest = WebRequest.Create(String.Format(url2, accessToken1))
            Dim response2 As WebResponse = request1.GetResponse()
            Dim stream2 As Stream = response2.GetResponseStream()
            Dim dataContractJsonSerializer2 As New DataContractJsonSerializer(GetType(FacebookUser))

            Dim fUser1 As New FacebookUser()
            fUser1 = TryCast(dataContractJsonSerializer2.ReadObject(stream2), FacebookUser)


            Dim dtTable As New DataTable
            dtTable.Columns.Add(New DataColumn("name", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("id", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("picture", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("link", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("access_token", Type.GetType("System.String")))

            Dim dtRow As DataRow = dtTable.NewRow


            dtRow("name") = fUser1.name
            dtRow("id") = fUser1.id
            dtRow("picture") = fUser1.picture.data.url
            dtRow("link") = fUser1.link
            dtRow("access_token") = Session("FacebookAccessToken")

            dtTable.Rows.Add(dtRow)


            For Each item As FanPage.m_data In fPage1.data
                If Not item.access_token Is Nothing Then
                    If Not item.link Is Nothing Then
                        If Not item.picture Is Nothing Then
                            Dim dtRow1 As DataRow = dtTable.NewRow
                            dtRow1("name") = item.name.ToString
                            dtRow1("id") = item.id.ToString
                            dtRow1("picture") = item.picture.data.url.ToString
                            dtRow1("link") = item.link.ToString
                            dtRow1("access_token") = item.access_token.ToString
                            dtTable.Rows.Add(dtRow1)
                        End If
                    End If
                End If
            Next
            dtTable.TableName = "fanpages"
            Dim dv As DataView = New DataView(dtTable)
            dv.Sort = "name"

            If dv.Count > 0 Then
                Dim myros As DataRowView
                For Each myros In dv
                    'For i = 0 To dv.Table.Columns.Count - 1
                    'Response.Write(myros(i) & " <Br/>" & vbTab)
                    'Response.Write(myros("name") & " <Br/>" & vbTab)
                    'Response.Write(myros("picture") & " <Br/>" & vbTab)
                    'Response.Write(myros("id") & " <Br/>" & vbTab)
                    'Response.Write(myros("access_token") & " <Br/> <br/>" & vbTab)

                    'Next
                    Dim objPagesDetails As New FacebookUserAndPagesDetails
                    objPagesDetails.FBUSerId = Session("FacebookUserId")
                    objPagesDetails.TSMAUserId = Session("SiteUserId")
                    objPagesDetails.FBPageId = myros("id")
                    objPagesDetails.FBPageName = myros("name")
                    objPagesDetails.FBPageURl = myros("link")
                    objPagesDetails.FBPageToken = myros("access_token")
                    objPagesDetails.FBPageTokenExpirationDate = "Never"
                    objPagesDetails.AddUpdateFBUserPageDetails()



                    UpdateAccessTokenOnLogin(myros("id"), myros("access_token"), myros("picture"))


                    strPageId = strPageId + myros("id") & ","
                    Session("strPageId") = strPageId
                Next
             Else
                Session("strPageId") = ""
            End If

        Catch ex As Exception
            'ltrAutoPostError.Text = "Facebook is experiencing problems at this time. Please save your work and try to publish later."
        End Try
    End Sub

    Sub UpdateAccessTokenOnLogin(ByVal strPageId As String, ByVal strToken As String, ByVal strImage As String)
        Dim objLogin As New BALSchedulePost
        objLogin.FBUserId = Session("FacebookUserId")
        objLogin.FBPageId = strPageId
        objLogin.FBPageAccessToken = strToken
        objLogin.FBPageImage = strImage
        objLogin.UpdateAccessTokenOnLogin()
    End Sub
End Class