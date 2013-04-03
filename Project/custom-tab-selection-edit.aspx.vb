﻿Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System.Threading

Public Class custom_tab_selection_edit
    Inherits System.Web.UI.Page
    Public strPageId As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            If Not Page.IsPostBack Then
                Dim obj As New BALCustomTab
                Dim ds1 As New DataSet
                'obj.Page = "/custom-tab"
                'ds1 = obj.GetVideoTutorial()
                obj.CustomTabId = Request.QueryString("ctId")
                ds1 = obj.GetVideoTutorialByMasterID()
                Dim videostring As String
                If ds1.Tables(0).Rows.Count > 0 Then
                    videostring = Replace(ds1.Tables(0).Rows(0).Item("ctm_Video").ToString, "watch?v=", "v/")
                    spnVideo.InnerHtml = "<object style='height: 335px; width: 555px'><param name='movie' value='" & videostring & "'><param name='allowFullScreen' value='true'><param name='allowScriptAccess' value='always'><param name='wmode' value='transparent'><embed src='" & videostring & "' type='application/x-shockwave-flash' allowfullscreen='true' allowScriptAccess='always' width='555' height='335'></object>"
                End If
                If Request.QueryString("ctId") <> "" And IsNumeric(Request.QueryString("ctId")) Then
                    BindFanPages()
                    BindSavedCustomTab()
                End If
            End If
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
        End If
        'Catch ex As Exception
        '    lblMessage.Text = "Error: " & ex.Message
        'End Try
    End Sub

    'Sub bindTotalFanPage()
    '    Dim accessToken As String = Session("FacebookAccessToken")
    '    Dim clientId As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
    '    Dim clientSecret As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey").ToString()
    '    Dim url As String = "https://graph.facebook.com/me/accounts?fields=id,name,picture,category,access_token&return_ssl_resources=true&access_token={0}"
    '    Dim request As WebRequest = WebRequest.Create(String.Format(url, accessToken))
    '    Dim response1 As WebResponse = request.GetResponse()
    '    Dim stream As Stream = response1.GetResponseStream()
    '    Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(FanPage))

    '    Dim fPage As New FanPage()
    '    fPage = TryCast(dataContractJsonSerializer.ReadObject(stream), FanPage)
    '    Dim listPages As New List(Of FanPage.m_data)

    '    For Each item1 As FanPage.m_data In fPage.data
    '        listPages.Add(item1)
    '    Next

    '    Dim cnt As Integer = listPages.Count

    '    Dim aid As String = ""
    '    For i As Integer = 0 To cnt - 1
    '        strPageId = strPageId + listPages.Item(i).id & ","
    '        Session("strPageId") = strPageId
    '    Next
    'End Sub

#Region "Bind Fan Pages"
    Sub BindFanPages()
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim accessToken As String = Session("FacebookAccessToken")
                Dim clientId As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
                Dim clientSecret As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey").ToString()
                Dim url As String = "https://graph.facebook.com/me/accounts?fields=id,name,picture,category,access_token&return_ssl_resources=true&access_token={0}"
                Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, accessToken))
                Dim fbResponse As WebResponse = fbRequest.GetResponse()
                Dim stream As Stream = fbResponse.GetResponseStream()
                Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(FanPage))

                Dim fPage As New FanPage()
                fPage = TryCast(dataContractJsonSerializer.ReadObject(stream), FanPage)
                'Dim listPages As New List(Of FanPage.m_data)

                'For Each item As FanPage.m_data In fPage.data
                '    listPages.Add(item)
                'Next
                'If listPages.Count > 0 Then
                '    dstFanPages.DataSource = listPages
                '    dstFanPages.DataBind()
                '    plcData.Visible = True
                '    plcNoData.Visible = False
                'Else
                '    dstFanPages.DataSource = Nothing
                '    dstFanPages.DataBind()
                '    plcData.Visible = False
                '    plcNoData.Visible = True
                'End If
                Dim dtTable As New DataTable
                dtTable.Columns.Add(New DataColumn("name", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("id", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("picture", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("access_token", Type.GetType("System.String")))

                For Each item As FanPage.m_data In fPage.data
                    If Not item.access_token Is Nothing Then
                        If Not item.picture Is Nothing Then
                            Dim dtRow1 As DataRow = dtTable.NewRow
                            dtRow1("name") = item.name.ToString
                            dtRow1("id") = item.id.ToString
                            dtRow1("picture") = item.picture.data.url.ToString
                            dtRow1("access_token") = item.access_token.ToString
                            dtTable.Rows.Add(dtRow1)
                        End If
                    End If
                Next
                dtTable.TableName = "fanpages"
                Dim dv As DataView = New DataView(dtTable)
                dv.Sort = "name"

                If dv.Count > 0 Then
                    dstFanPages.DataSource = dv
                    dstFanPages.DataBind()
                    plcData.Visible = True
                    plcNoData.Visible = False
                Else
                    'Dim strUrl As String = "Login.aspx?i=" & Session("Industry") & "&c=" & Session("company") & "&u=1&at=" & Session("hdnToken") & ""
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, ";ShowHide11;", ";openNewWin('" & strUrl & "');", True)
                    dstFanPages.DataSource = Nothing
                    dstFanPages.DataBind()
                    plcData.Visible = False
                    plcNoData.Visible = True
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub
#End Region

#Region "Bind Fan Pages"
    Sub BindSelectedFanPages(ByVal strFanPageId As String)
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim accessToken As String = Session("FacebookAccessToken")
                Dim clientId As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
                Dim clientSecret As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey").ToString()
                Dim url As String = "https://graph.facebook.com/me/accounts?fields=id,name,picture,category,access_token&return_ssl_resources=true&access_token={0}"
                Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, accessToken))
                Dim fbResponse As WebResponse = fbRequest.GetResponse()
                Dim stream As Stream = fbResponse.GetResponseStream()
                Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(FanPage))

                Dim fPage As New FanPage()
                fPage = TryCast(dataContractJsonSerializer.ReadObject(stream), FanPage)
                'Dim listPages As New List(Of FanPage.m_data)

                'For Each item As FanPage.m_data In fPage.data
                '    listPages.Add(item)
                'Next
                'If listPages.Count > 0 Then
                '    dstFanPages.DataSource = listPages
                '    dstFanPages.DataBind()
                '    plcData.Visible = True
                '    plcNoData.Visible = False
                'Else
                '    dstFanPages.DataSource = Nothing
                '    dstFanPages.DataBind()
                '    plcData.Visible = False
                '    plcNoData.Visible = True
                'End If
                Dim dtTable As New DataTable
                dtTable.Columns.Add(New DataColumn("name", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("id", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("picture", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("access_token", Type.GetType("System.String")))

                For Each item As FanPage.m_data In fPage.data
                    If Not item.access_token Is Nothing Then
                        If Not item.picture Is Nothing Then
                            Dim dtRow1 As DataRow = dtTable.NewRow
                            dtRow1("name") = item.name.ToString
                            dtRow1("id") = item.id.ToString
                            dtRow1("picture") = item.picture.data.url.ToString
                            dtRow1("access_token") = item.access_token.ToString
                            dtTable.Rows.Add(dtRow1)
                        End If
                    End If
                Next
                dtTable.TableName = "fanpages"
                Dim dv As DataView = New DataView(dtTable)
                dv.Sort = "name"

                If dv.Count > 0 Then
                    dstFanPages.DataSource = dv
                    dstFanPages.DataBind()
                    plcData.Visible = True
                    plcNoData.Visible = False
                Else
                    dstFanPages.DataSource = Nothing
                    dstFanPages.DataBind()
                    plcData.Visible = False
                    plcNoData.Visible = True
                End If

                For Each item As DataListItem In dstFanPages.Items
                    Dim myCheckBox As HtmlInputRadioButton
                    Dim HiddenID As New HtmlInputHidden
                    myCheckBox = CType(item.FindControl("chkPage"), HtmlInputRadioButton)
                    HiddenID = CType(item.FindControl("hdnPageId"), HtmlInputHidden)
                    Dim str As String = CStr(HiddenID.Value)
                    If str = strFanPageId Then
                        myCheckBox.Checked = True
                    Else
                        myCheckBox.Disabled = True
                    End If
                Next
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub
#End Region
    Public Function BindSavedCustomTab()
        ' Try
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            'Response.Write(Request.QueryString("ctId") & "<br/> userid :" & Request.QueryString("userId") & "<br>fbuserid :" & Request.QueryString("FbuserId") & "<br> CompanyId :" & Request.QueryString("CId") & "<br> industryid :" & Request.QueryString("IId"))


            Dim ds As New DataSet
            Dim objBAL As New BALCustomTab
            objBAL.CustomTabId = CInt(Request.QueryString("ctId"))
            objBAL.UserId = CInt(Request.QueryString("userId"))
            objBAL.FBUserId = If(Session("FacebookUserId") <> Nothing, Session("FacebookUserId"), "")
            objBAL.FBPageId = If(Session("strPageId") <> Nothing, Session("strPageId"), "")
            objBAL.FBUserEmail = If(Session("FacebookUserEmail") <> Nothing, Session("FacebookUserEmail"), "")
            objBAL.CompanyId = CInt(Request.QueryString("CId"))
            objBAL.IndustryId = CInt(Request.QueryString("IId"))
            ds = objBAL.GetCustomTabMasterByID
            If ds.Tables(0).Rows.Count > 0 Then
                txtname.Text = ds.Tables(1).Rows(0).Item("CustomTabName")
                If ds.Tables(0).Rows(0).Item("ct_type") = "2" Then
                    dvTabMainDiv.Style.Add("width", "1115px")
                    dvTabSubDiv.Style.Add("width", "820px")
                Else
                    dvTabMainDiv.Style.Add("width", "825px")
                    dvTabSubDiv.Style.Add("width", "530px")
                End If
                hdnType.Value = ds.Tables(0).Rows(0).Item("ct_type")
                txtCustomTabName.Value = ds.Tables(1).Rows(0).Item("CustomTabName")
                txtUserEmail.Value = ds.Tables(0).Rows(0).Item("ct_FBUserEmail")
                divSidebarHtml.InnerHtml = ds.Tables(0).Rows(0).Item("CustomTabContent")
                Session("CustomTabID") = ds.Tables(0).Rows(0).Item("CustomTabId")
                hdnCTID.Value = Session("CustomTabID")
                Session("CustomTabID") = ds.Tables(0).Rows(0).Item("CustomTabId")
                Session("FanPageSelected") = ds.Tables(0).Rows(0).Item("ct_FBPageId").ToString
                Session("FanPageName") = ds.Tables(0).Rows(0).Item("ct_FBPageName").ToString
                hdnCTID.Value = Session("CustomTabID")
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ct_IsPublished")) AndAlso ds.Tables(0).Rows(0).Item("ct_IsPublished") = 1 Then
                    BindSelectedFanPages(Session("FanPageSelected"))
                ElseIf Session("FanPageSelected") <> "" AndAlso Not IsDBNull(Session("FanPageSelected")) Then
                    For Each item As DataListItem In dstFanPages.Items
                        Dim myCheckBox As HtmlInputRadioButton
                        Dim HiddenID As New HtmlInputHidden
                        myCheckBox = CType(item.FindControl("chkPage"), HtmlInputRadioButton)
                        HiddenID = CType(item.FindControl("hdnPageId"), HtmlInputHidden)
                        Dim str As String = CStr(HiddenID.Value)
                        ' Response.Write(str & " ---- ")
                        If str = ds.Tables(0).Rows(0).Item("ct_FBPageId").ToString Then
                            myCheckBox.Checked = True
                        Else
                            myCheckBox.Disabled = True
                        End If
                    Next
                End If
            End If
            If ds.Tables(2).Rows(0).Item("mainadmin") = 1 Then
                pnlEdit.Visible = True
                pnlMessage.Visible = False
            Else
                pnlEdit.Visible = False
                pnlMessage.Visible = True
                spnAssigned.InnerHtml = "An administrator of business page <b>" & ds.Tables(0).Rows(0).Item("ct_FBPageName") & "</b> has shared this Custom Tab with you for viewing purposes only. To edit, please create a copy in '<b><a href='custom-tab' title='My Saved Cover Photos'>My Saved Custom tabs</a></b>' and edit that copy"
            End If
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
        End If
        'Catch ex As Exception
        '    lblMessage.Text = "Error: " & ex.Message
        'End Try
    End Function

    Private Sub lnkReset_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReset.ServerClick
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                divSidebarHtml.InnerHtml = ""
                Dim ds As New DataSet
                Dim objBAL As New BALCustomTab
                objBAL.CustomTabId = CInt(Request.QueryString("ctId"))
                objBAL.UserId = CInt(Request.QueryString("userId"))
                objBAL.FBUserId = Request.QueryString("FbuserId")
                objBAL.CompanyId = CInt(Request.QueryString("CId"))
                objBAL.IndustryId = CInt(Request.QueryString("IId"))
                ds = objBAL.GetCustomTabMaster
                If ds.Tables(0).Rows.Count > 0 Then
                    divSidebarHtml.InnerHtml = ds.Tables(0).Rows(0).Item("CustomTabContent")
                End If

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub lnkSave_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.ServerClick

        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                'Response.Write(Request.QueryString("ctId") & "<br/> userid :" & Request.QueryString("userId") & "<br>fbuserid :" & Request.QueryString("FbuserId") & "<br> CompanyId :" & Request.QueryString("CId") & "<br> industryid :" & Request.QueryString("IId"))
                Dim objBAL As New BALCustomTab
                objBAL.CustomTabId = CInt(Request.QueryString("ctId"))
                objBAL.UserId = CInt(Request.QueryString("userId"))
                objBAL.FBUserId = Request.QueryString("FbuserId")
                objBAL.CustomTabName = If(txtCustomTabName.Value <> "", txtCustomTabName.Value, "")
                objBAL.FBUserEmail = txtUserEmail.Value
                objBAL.CompanyId = CInt(Request.QueryString("CId"))
                objBAL.IndustryId = CInt(Request.QueryString("IId"))
                objBAL.CustomTabContent = hdnSidebarContent.Value

                ViewState("CustomTabID") = CInt(Request.QueryString("ctId"))
                ViewState("FBUserId") = Request.QueryString("FbuserId")

                objBAL.UpdateCustomTabContent()

                BindSavedCustomTab()

                Dim siteUrl As String = ConfigurationManager.AppSettings("AppPath") & "generate-ct-image.aspx?id=" & Session("CustomTabID")
                Dim browserWidth As Integer = Convert.ToInt32(800)
                Dim browserHeight As Integer = Convert.ToInt32(600)
                Dim thumbnailWidth As Integer = Convert.ToInt32(800)
                Dim thumbnailHeight As Integer = Convert.ToInt32(600)
                Dim relativeImagePath As String = ConfigurationManager.AppSettings("uploadpath")
                Dim fullPath As String = Server.MapPath(relativeImagePath)
                Dim strExt As String = ""
                Dim strPhoto As String = ""
                Dim strDate As Date = "1/1/1900"
                strPhoto = "customtab-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & ".jpg"
                Session("ImageName") = strPhoto

                Dim strD As [String] = strPhoto
                If Not fullPath.EndsWith("\") Then
                    fullPath += "\"
                End If

                Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteUrl, 520, 1000, 320, 740, fullPath, strD), System.Drawing.Image)

                objBAL.CustomTabId = Session("CustomTabID")
                objBAL.FBUserId = ViewState("FBUserId")
                objBAL.CustomTabImage = Session("ImageName")
                objBAL.UpdateImageName()
                hdnPublished.Value = "0"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Your work has been saved');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try

    End Sub


    Private Sub btnUpload_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.ServerClick
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim result As String = ""
                Dim strPageId As String = ""
                Dim AppID As String = ""
                If hdnType.Value = "2" Then
                    AppID = System.Configuration.ConfigurationManager.AppSettings("FBAppIdFor810Tab").ToString()

                    Dim url As String = "https://graph.facebook.com/{0}/tabs/?app_id={2}&access_token={1}&method=post"
                    For Each item As DataListItem In dstFanPages.Items
                        Dim myCheckBox As HtmlInputRadioButton
                        myCheckBox = CType(item.FindControl("chkPage"), HtmlInputRadioButton)
                        If myCheckBox.Checked = True Then
                            strPageId = CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
                            Dim strPageAccessToken As String = CType(item.FindControl("hdnAccessToken"), HtmlInputHidden).Value
                            ViewState("PageName") = CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value
                            If strPageId <> "" AndAlso strPageAccessToken <> "" Then
                                ViewState("PageID") = strPageId
                                ViewState("PageAccessToken") = strPageAccessToken
                                Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, strPageId, strPageAccessToken, AppID))
                                Dim fbResponse As WebResponse = fbRequest.GetResponse()
                                Dim stream As Stream = fbResponse.GetResponseStream()
                                Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                                Dim streamReader As New StreamReader(stream, encode)
                                result = streamReader.ReadToEnd()
                                streamReader.Close()
                            End If
                            Dim objCustomTab As New CustomTabContent
                            objCustomTab.FBPageId = ViewState("PageID")
                            objCustomTab.FBApplicationId = ConfigurationManager.AppSettings("FBAppIdFor810Tab").ToString()
                            objCustomTab.FBPageName = ViewState("PageName")
                            objCustomTab.FBUserId = Session("FacebookUserId")
                            objCustomTab.Content = divSidebarHtml.InnerHtml
                            objCustomTab.AddEditCustomTab()

                            Dim obj As New BALCustomTab
                            obj.CustomTabId = Session("CustomTabID")
                            obj.FBPageId = ViewState("PageID") 'strPageId 'CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
                            obj.FBPageName = ViewState("PageName") 'CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value
                            obj.FBPageAccessToken = ViewState("PageAccessToken")
                            obj.UpdateIsPublishedCustomTab()
                        End If
                    Next
                    If result.ToLower = "true" Then
                        url = "https://graph.facebook.com/{0}/tabs?app_id={3}&access_token={1}&method=post&custom_name={2}"
                        Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, ViewState("PageID"), ViewState("PageAccessToken"), "", AppID))
                        Dim fbResponse As WebResponse = fbRequest.GetResponse()
                        Dim stream As Stream = fbResponse.GetResponseStream()
                        Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                        Dim streamReader As New StreamReader(stream, encode)
                        result = streamReader.ReadToEnd()
                        streamReader.Close()


                        ''''''''''' Delete Code for 520px Tab 

                        'Dim url1 As String = "https://graph.facebook.com/{0}/tabs/{2}?access_token={1}"
                        'Dim fbRequest1 As WebRequest = WebRequest.Create(String.Format(url1, ViewState("PageID"), ViewState("PageAccessToken"), System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()))
                        'Dim fbResponse1 As WebResponse = fbRequest1.GetResponse()
                        'Dim stream1 As Stream = fbResponse1.GetResponseStream()
                        'Dim dataContractJsonSerializer1 As New DataContractJsonSerializer(GetType(checkApp))
                        'Dim fPage As New checkApp()
                        'fPage = TryCast(dataContractJsonSerializer1.ReadObject(stream1), checkApp)
                        'Dim listAlbums As New List(Of checkApp.m_data)

                        'For Each item1 As checkApp.m_data In fPage.data
                        '    listAlbums.Add(item1)
                        'Next

                        'If listAlbums.Count > 0 Then
                        '    Dim strPageId12 As String = ViewState("PageID") '"103823149672939"
                        '    Dim strPageAccessToken12 As String = ViewState("PageAccessToken") '"AAAD2ZCc3HZAxABAIDxzyAwOqZCgzSRFVkLHqiLl88muU7QIRYTE1ZBEnPM7GTL7yZCbs7REZA0cVul1DsSWs2ntOKZB4boRTDPDZB8htHZBcn3C5Jlnlnqw4R"

                        '    Dim url2 As String = "https://graph.facebook.com/{0}/tabs?app_id={3}&access_token={1}&method=delete&custom_name={2}"
                        '    Dim fbRequest2 As WebRequest = WebRequest.Create(String.Format(url2, strPageId12, strPageAccessToken12, "", System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()))
                        '    Dim fbResponse2 As WebResponse = fbRequest2.GetResponse()
                        '    Dim stream2 As Stream = fbResponse2.GetResponseStream()
                        '    Dim encode2 As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                        '    Dim streamReader2 As New StreamReader(stream2, encode2)
                        '    result = streamReader2.ReadToEnd()
                        '    streamReader2.Close()
                        'End If
                    End If
                Else
                    AppID = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()

                    Dim url As String = "https://graph.facebook.com/{0}/tabs/?app_id={2}&access_token={1}&method=post"
                    For Each item As DataListItem In dstFanPages.Items
                        Dim myCheckBox As HtmlInputRadioButton
                        myCheckBox = CType(item.FindControl("chkPage"), HtmlInputRadioButton)
                        If myCheckBox.Checked = True Then
                            strPageId = CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
                            Dim strPageAccessToken As String = CType(item.FindControl("hdnAccessToken"), HtmlInputHidden).Value
                            ViewState("PageName") = CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value
                            If strPageId <> "" AndAlso strPageAccessToken <> "" Then
                                ViewState("PageID") = strPageId
                                ViewState("PageAccessToken") = strPageAccessToken
                                Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, strPageId, strPageAccessToken, AppID))
                                Dim fbResponse As WebResponse = fbRequest.GetResponse()
                                Dim stream As Stream = fbResponse.GetResponseStream()
                                Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                                Dim streamReader As New StreamReader(stream, encode)
                                result = streamReader.ReadToEnd()
                                streamReader.Close()
                            End If
                            Dim objCustomTab As New CustomTabContent
                            objCustomTab.FBPageId = ViewState("PageID")
                            objCustomTab.FBApplicationId = ConfigurationManager.AppSettings("FBAppId").ToString()
                            objCustomTab.FBPageName = ViewState("PageName")
                            objCustomTab.FBUserId = Session("FacebookUserId")
                            objCustomTab.Content = divSidebarHtml.InnerHtml
                            objCustomTab.AddEditCustomTab()

                            Dim obj As New BALCustomTab
                            obj.CustomTabId = Session("CustomTabID")
                            obj.FBPageId = ViewState("PageID") 'strPageId 'CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
                            obj.FBPageName = ViewState("PageName") 'CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value
                            obj.FBPageAccessToken = ViewState("PageAccessToken")
                            obj.UpdateIsPublishedCustomTab()
                        End If
                    Next
                    If result.ToLower = "true" Then
                        url = "https://graph.facebook.com/{0}/tabs?app_id={3}&access_token={1}&method=post&custom_name={2}"
                        Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, ViewState("PageID"), ViewState("PageAccessToken"), "", AppID))
                        Dim fbResponse As WebResponse = fbRequest.GetResponse()
                        Dim stream As Stream = fbResponse.GetResponseStream()
                        Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                        Dim streamReader As New StreamReader(stream, encode)
                        result = streamReader.ReadToEnd()
                        streamReader.Close()

                        ''''''''''' Delete Code for 520px Tab 

                        'Dim url1 As String = "https://graph.facebook.com/{0}/tabs/{2}?access_token={1}"
                        'Dim fbRequest1 As WebRequest = WebRequest.Create(String.Format(url1, ViewState("PageID"), ViewState("PageAccessToken"), System.Configuration.ConfigurationManager.AppSettings("FBAppIdFor810Tab").ToString()))
                        'Dim fbResponse1 As WebResponse = fbRequest1.GetResponse()
                        'Dim stream1 As Stream = fbResponse1.GetResponseStream()
                        'Dim dataContractJsonSerializer1 As New DataContractJsonSerializer(GetType(checkApp))
                        'Dim fPage As New checkApp()
                        'fPage = TryCast(dataContractJsonSerializer1.ReadObject(stream1), checkApp)
                        'Dim listAlbums As New List(Of checkApp.m_data)

                        'For Each item1 As checkApp.m_data In fPage.data
                        '    listAlbums.Add(item1)
                        'Next

                        'If listAlbums.Count > 0 Then
                        '    Dim strPageId12 As String = ViewState("PageID") '"103823149672939"
                        '    Dim strPageAccessToken12 As String = ViewState("PageAccessToken") '"AAAD2ZCc3HZAxABAIDxzyAwOqZCgzSRFVkLHqiLl88muU7QIRYTE1ZBEnPM7GTL7yZCbs7REZA0cVul1DsSWs2ntOKZB4boRTDPDZB8htHZBcn3C5Jlnlnqw4R"

                        '    Dim url2 As String = "https://graph.facebook.com/{0}/tabs?app_id={3}&access_token={1}&method=delete&custom_name={2}"
                        '    Dim fbRequest2 As WebRequest = WebRequest.Create(String.Format(url2, strPageId12, strPageAccessToken12, "", System.Configuration.ConfigurationManager.AppSettings("FBAppIdFor810Tab").ToString()))
                        '    Dim fbResponse2 As WebResponse = fbRequest2.GetResponse()
                        '    Dim stream2 As Stream = fbResponse2.GetResponseStream()
                        '    Dim encode2 As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                        '    Dim streamReader2 As New StreamReader(stream2, encode2)
                        '    result = streamReader2.ReadToEnd()
                        '    streamReader2.Close()
                        'End If
                    End If
                End If
                'AppID = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
                

                ClearData()
                BindSelectedFanPages(ViewState("PageID"))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Custom Tab published to your Page(s) successfully');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub btnSave_ServerClick(sender As Object, e As System.EventArgs) Handles btnSave.ServerClick
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                'If hdnPublished.Value = "1" Then
                Dim objBAL As New BALCustomTab
                objBAL.CustomTabId = CInt(Request.QueryString("ctId"))
                objBAL.UserId = CInt(Request.QueryString("userId"))
                objBAL.FBUserId = Request.QueryString("FbuserId")
                objBAL.CustomTabName = If(txtCustomTabName.Value <> "", txtCustomTabName.Value, "")
                objBAL.FBUserEmail = txtUserEmail.Value
                objBAL.CompanyId = CInt(Request.QueryString("CId"))
                objBAL.IndustryId = CInt(Request.QueryString("IId"))
                objBAL.CustomTabContent = hdnSidebarContent.Value

                ViewState("CustomTabID") = CInt(Request.QueryString("ctId"))
                ViewState("FBUserId") = Request.QueryString("FbuserId")

                objBAL.UpdateCustomTabContent()

                'For Each item As DataListItem In dstFanPages.Items
                '    Dim myCheckBox As HtmlInputRadioButton
                '    myCheckBox = CType(item.FindControl("chkPage"), HtmlInputRadioButton)
                '    If myCheckBox.Checked = True Then
                '        strPageId = CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
                '        Dim strPageAccessToken As String = CType(item.FindControl("hdnAccessToken"), HtmlInputHidden).Value
                '        ViewState("PageName") = CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value
                '        Dim objCustomTab As New CustomTabContent
                '        objCustomTab.FBPageId = ViewState("PageID")
                '        objCustomTab.FBPageName = ViewState("PageName")
                '        objCustomTab.FBUserId = Session("FacebookUserId")
                '        objCustomTab.Content = hdnSidebarContent.Value
                '        objCustomTab.AddEditCustomTab()
                '    End If
                'Next

                BindSavedCustomTab()

                Dim siteUrl As String = ConfigurationManager.AppSettings("AppPath") & "generate-ct-image.aspx?id=" & Session("CustomTabID")
                Dim browserWidth As Integer = Convert.ToInt32(800)
                Dim browserHeight As Integer = Convert.ToInt32(600)
                Dim thumbnailWidth As Integer = Convert.ToInt32(800)
                Dim thumbnailHeight As Integer = Convert.ToInt32(600)
                Dim relativeImagePath As String = ConfigurationManager.AppSettings("uploadpath")
                Dim fullPath As String = Server.MapPath(relativeImagePath)
                Dim strExt As String = ""
                Dim strPhoto As String = ""
                Dim strDate As Date = "1/1/1900"
                strPhoto = "customtab-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & ".jpg"
                Session("ImageName") = strPhoto

                Dim strD As [String] = strPhoto
                If Not fullPath.EndsWith("\") Then
                    fullPath += "\"
                End If

                'Response.Write(siteUrl & "<br/>" & fullPath & "<br/>" & strD)
                'Response.End()
                Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteUrl, 520, 1000, 320, 740, fullPath, strD), System.Drawing.Image)

                objBAL.CustomTabId = Session("CustomTabID")
                objBAL.FBUserId = ViewState("FBUserId")
                objBAL.CustomTabImage = Session("ImageName")
                objBAL.UpdateImageName()
                'End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "PublishAlert();", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

    Sub ClearData()
        For Each item As DataListItem In dstFanPages.Items
            Dim myCheckBox As HtmlInputRadioButton
            Dim HiddenID As HtmlInputHidden
            myCheckBox = CType(item.FindControl("chkPage"), HtmlInputRadioButton)
            HiddenID = CType(item.FindControl("hdnPageId"), HtmlInputHidden)
            myCheckBox.Checked = False
        Next
    End Sub

    Private Sub lnkShareUserEmail_ServerClick(sender As Object, e As System.EventArgs) Handles lnkShareUserEmail.ServerClick
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                'Response.Write(Request.QueryString("ctId") & "<br/> userid :" & Request.QueryString("userId") & "<br>fbuserid :" & Request.QueryString("FbuserId") & "<br> CompanyId :" & Request.QueryString("CId") & "<br> industryid :" & Request.QueryString("IId"))
                Dim objBAL As New BALCustomTab
                objBAL.CustomTabId = CInt(Request.QueryString("ctId"))
                objBAL.UserId = CInt(Request.QueryString("userId"))
                objBAL.FBUserId = Request.QueryString("FbuserId")
                objBAL.CustomTabName = If(txtCustomTabName.Value <> "", txtCustomTabName.Value, "")
                objBAL.FBUserEmail = txtUserEmail.Value
                objBAL.CompanyId = CInt(Request.QueryString("CId"))
                objBAL.IndustryId = CInt(Request.QueryString("IId"))
                objBAL.CustomTabContent = hdnSidebarContent.Value

                ViewState("CustomTabID") = CInt(Request.QueryString("ctId"))
                ViewState("FBUserId") = Request.QueryString("FbuserId")

                objBAL.UpdateCustomTabContent()

                BindSavedCustomTab()

                Dim siteUrl As String = ConfigurationManager.AppSettings("AppPath") & "generate-ct-image.aspx?id=" & Session("CustomTabID")
                Dim browserWidth As Integer = Convert.ToInt32(800)
                Dim browserHeight As Integer = Convert.ToInt32(600)
                Dim thumbnailWidth As Integer = Convert.ToInt32(800)
                Dim thumbnailHeight As Integer = Convert.ToInt32(600)
                Dim relativeImagePath As String = ConfigurationManager.AppSettings("uploadpath")
                Dim fullPath As String = Server.MapPath(relativeImagePath)
                Dim strExt As String = ""
                Dim strPhoto As String = ""
                Dim strDate As Date = "1/1/1900"
                strPhoto = "customtab-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & ".jpg"
                Session("ImageName") = strPhoto

                Dim strD As [String] = strPhoto
                If Not fullPath.EndsWith("\") Then
                    fullPath += "\"
                End If

                Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteUrl, 520, 1000, 320, 740, fullPath, strD), System.Drawing.Image)

                objBAL.CustomTabId = Session("CustomTabID")
                objBAL.FBUserId = ViewState("FBUserId")
                objBAL.CustomTabImage = Session("ImageName")
                objBAL.UpdateImageName()
                hdnPublished.Value = "0"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Custom tab shared successfully');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub
End Class