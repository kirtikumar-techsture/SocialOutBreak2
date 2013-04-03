Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System.Threading
Public Class selected_tab
    Inherits System.Web.UI.Page
    Public strPageId As String = ""
    Protected strPageReturnCatId As String = ""
    Public strFBUserId As String
    Public strAppId As String = ""
    Public strurl As String = ""


#Region "Page Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoginCheck.LoginSessionCheck()
        strFBUserId = Session("FacebookUserId").ToString
        Try
            If Not Page.IsPostBack Then
                Dim obj As New BALCustomTab
                Dim ds1 As New DataSet
                Dim ds2 As New DataSet
                obj.CustomTabId = Request.QueryString("ctId")
                ds1 = obj.GetVideoTutorialByMasterID()

                Dim videostring As String
                If ds1.Tables(0).Rows.Count > 0 Then
                    If ds1.Tables(0).Rows(0).Item("ctm_Video").ToString <> "" Then
                        videostring = Replace(ds1.Tables(0).Rows(0).Item("ctm_Video").ToString, "watch?v=", "v/")
                        spnVideo.InnerHtml = "<object style='height: 335px; width: 555px'><param name='movie' value='" & videostring & "'><param name='allowFullScreen' value='true'><param name='allowScriptAccess' value='always'><param name='wmode' value='transparent'><embed src='" & videostring & "' type='application/x-shockwave-flash' allowfullscreen='true' allowScriptAccess='always' width='555' height='335'></object>"
                    Else
                        spnVideo.InnerHtml = ""
                    End If
                End If
                ds2 = obj.GetApplicationIdByCategory()
                If ds2.Tables(0).Rows.Count > 0 Then
                    strurl = ds2.Tables(0).Rows(0).Item("fburl").ToString
                    strAppId = ds2.Tables(0).Rows(0).Item("appid").ToString
                    hdnApplicationID.Value = strAppId.ToString.Trim()
                    hdnPageURL.Value = strurl
                End If

                If Request.QueryString("isCustomise") <> "" And IsNumeric(Request.QueryString("isCustomise")) Then
                    If CInt(Request.QueryString("isCustomise")) = 1 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PublishSaveAlert", "ShowCustomiseTabEditMenu();", True)
                    End If
                End If
                If Request.QueryString("ctId") <> "" And IsNumeric(Request.QueryString("ctId")) Then
                    BindFanPages()
                    BindSavedCustomTab()
                End If
            End If



        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub
#End Region

#Region "Bind Fan Pages"
    Sub BindFanPages()
        Try
            Dim accessToken As String = Session("FacebookAccessToken")
            Dim clientId As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
            Dim clientSecret As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey").ToString()
            Dim url As String = "https://graph.facebook.com/me/accounts?fields=id,name,link,picture,category,access_token&return_ssl_resources=true&access_token={0}"
            Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, accessToken))
            Dim fbResponse As WebResponse = fbRequest.GetResponse()
            Dim stream As Stream = fbResponse.GetResponseStream()
            Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(FanPage))

            Dim fPage As New FanPage()
            fPage = TryCast(dataContractJsonSerializer.ReadObject(stream), FanPage)

            Dim dtTable As New DataTable
            dtTable.Columns.Add(New DataColumn("name", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("id", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("link", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("picture", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("access_token", Type.GetType("System.String")))

            For Each item As FanPage.m_data In fPage.data
                If Not item.access_token Is Nothing Then
                    If Not item.link Is Nothing Then
                        If Not item.picture Is Nothing Then
                            Dim dtRow1 As DataRow = dtTable.NewRow
                            dtRow1("name") = item.name.ToString
                            dtRow1("id") = item.id.ToString
                            dtRow1("link") = item.link.ToString
                            dtRow1("picture") = item.picture.data.url.ToString
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
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub
#End Region

#Region "Bind Fan Pages"
    Sub BindSelectedFanPages(ByVal strFanPageId As String)
        Try
            Dim accessToken As String = Session("FacebookAccessToken")
            Dim clientId As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
            Dim clientSecret As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey").ToString()
            Dim url As String = "https://graph.facebook.com/me/accounts?fields=id,name,link,picture,category,access_token&return_ssl_resources=true&access_token={0}"
            Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, accessToken))
            Dim fbResponse As WebResponse = fbRequest.GetResponse()
            Dim stream As Stream = fbResponse.GetResponseStream()
            Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(FanPage))

            Dim fPage As New FanPage()
            fPage = TryCast(dataContractJsonSerializer.ReadObject(stream), FanPage)

            Dim dtTable As New DataTable
            dtTable.Columns.Add(New DataColumn("name", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("id", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("link", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("picture", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("access_token", Type.GetType("System.String")))

            For Each item As FanPage.m_data In fPage.data
                If Not item.access_token Is Nothing Then
                    If Not item.link Is Nothing Then
                        If Not item.picture Is Nothing Then
                            Dim dtRow1 As DataRow = dtTable.NewRow
                            dtRow1("name") = item.name.ToString
                            dtRow1("id") = item.id.ToString
                            dtRow1("link") = item.link.ToString
                            dtRow1("picture") = item.picture.data.url.ToString
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
                If Trim(str) <> "" Then
                    If Trim(str) = Trim(strFanPageId) Then
                        myCheckBox.Checked = True
                    Else
                        myCheckBox.Disabled = True
                    End If
                End If
            Next
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub
#End Region

#Region "Bind Saved Tab"
    Private Sub BindSavedCustomTab()
        Try
            lblMessage.Text = ""
            Dim ds As New DataSet
            Dim objBAL As New BALCustomTab
            objBAL.CustomTabId = CInt(Request.QueryString("ctId"))
            objBAL.UserId = CInt(Session("SiteUserId"))
            objBAL.FBUserId = If(Session("FacebookUserId") <> Nothing, Session("FacebookUserId"), "")
            objBAL.FBPageId = If(Session("strPageId") <> Nothing, Session("strPageId"), "")
            objBAL.FBUserEmail = If(Session("FacebookUserEmail") <> Nothing, Session("FacebookUserEmail"), "")
            objBAL.CompanyId = CInt(Request.QueryString("CId"))
            objBAL.IndustryId = CInt(Request.QueryString("IId"))
            ds = objBAL.GetCustomTabMasterByIDNew
            If ds.Tables(0).Rows.Count > 0 Then
                txtname.Text = ds.Tables(1).Rows(0).Item("CustomTabName")
                If ds.Tables(0).Rows(0).Item("ct_Type") = "2" Then
                    'dvTabMainDiv.Style.Add("width", "1115px")
                    'dvTabSubDiv.Style.Add("width", "820px")
                Else
                    'dvTabMainDiv.Style.Add("width", "825px")
                    dvTabSubDiv.Style.Add("padding-left", "140px")
                End If
                ViewState("ExpressTabId") = ds.Tables(0).Rows(0).Item("ctl_libId")
                ViewState("MasterTabId") = ds.Tables(0).Rows(0).Item("ct_MasterTable")
                hdnType.Value = ds.Tables(0).Rows(0).Item("ct_type")
                txtCustomTabName.Value = ds.Tables(1).Rows(0).Item("CustomTabName")
                txtUserEmail.Value = ds.Tables(0).Rows(0).Item("ct_FBUserEmail")
                divSidebarHtml.InnerHtml = ds.Tables(0).Rows(0).Item("CustomTabContent")
                If CInt(Request.QueryString("isCustomise")) = 1 Then
                    txtCustomCode.Value = ds.Tables(0).Rows(0).Item("CustomTabContent")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PublishSaveAlert", "ShowCustomiseTabEditMenu();", True)
                Else
                    txtCustomCode.Value = ""
                End If
                hdnCTID.Value = ds.Tables(0).Rows(0).Item("CustomTabId")
                Session("FanPageSelected") = ds.Tables(0).Rows(0).Item("ct_FBPageId").ToString
                Session("FanPageName") = ds.Tables(0).Rows(0).Item("ct_FBPageName").ToString
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ct_IsPublished")) AndAlso ds.Tables(0).Rows(0).Item("ct_IsPublished") = 1 Then
                    BindSelectedFanPages(Session("FanPageSelected"))
                ElseIf Session("FanPageSelected") <> "" AndAlso Not IsDBNull(Session("FanPageSelected")) Then
                    For Each item As DataListItem In dstFanPages.Items
                        Dim myCheckBox As HtmlInputRadioButton
                        Dim HiddenID As New HtmlInputHidden
                        myCheckBox = CType(item.FindControl("chkPage"), HtmlInputRadioButton)
                        HiddenID = CType(item.FindControl("hdnPageId"), HtmlInputHidden)
                        Dim str As String = CStr(HiddenID.Value)
                        If Trim(str) <> "" Then
                            If Trim(str) = Trim(Convert.ToString(ds.Tables(0).Rows(0).Item("ct_FBPageId"))) Then
                                myCheckBox.Checked = True
                            Else
                                myCheckBox.Disabled = True
                            End If
                        End If
                    Next
                End If
            Else
                Response.Redirect("tabs-list.aspx?cid=" & ds.Tables(2).Rows(0).Item("CatId"))
            End If
            If ds.Tables(2).Rows.Count > 0 Then
                If ds.Tables(2).Rows(0).Item("CatId") = "0" Then
                    strPageReturnCatId = "tabs-list.aspx?cid=2"
                Else
                    strPageReturnCatId = "tabs-list.aspx?cid=" & ds.Tables(2).Rows(0).Item("CatId")
                End If
            End If
            If ds.Tables(2).Rows(0).Item("mainadmin") = 1 Then
                pnlEdit.Visible = True
                pnlMessage.Visible = False
            Else
                pnlEdit.Visible = False
                pnlMessage.Visible = True
                spnAssigned.InnerHtml = "A member of Total Social Media has shared this Custom Tab with you for viewing purposes only. To edit, please create a copy from '<b>My Saved Custom tabs</b>' and edit that copy"
                'spnAssigned.InnerHtml = "An administrator of business page <b>" & ds.Tables(0).Rows(0).Item("ct_FBPageName") & "</b> has shared this Custom Tab with you for viewing purposes only. To edit, please create a copy in '<b><a href='custom-tab' title='My Saved Cover Photos'>My Saved Custom tabs</a></b>' and edit that copy"
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub
#End Region

#Region "Reset"
    Private Sub lnkReset_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReset.ServerClick
        BindSavedCustomTab()
    End Sub
#End Region

#Region "Save"
    Private Sub lnkSave_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSave.ServerClick
        SaveTab(1)
    End Sub
    Private Sub btnSave_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.ServerClick
        SaveTab(2)
    End Sub
    Private Sub lnkShareUserEmail_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkShareUserEmail.ServerClick
        SaveTab(3)
    End Sub
    Private Sub SaveTab(ByVal intSaveType As Integer)
        Try
            Dim objBAL As New BALCustomTab
            objBAL.CustomTabId = CInt(Request.QueryString("ctId"))
            objBAL.UserId = CInt(Session("SiteUserId"))
            objBAL.FBUserId = Session("FacebookUserId")
            objBAL.CustomTabName = If(txtCustomTabName.Value <> "", txtCustomTabName.Value, "")
            objBAL.FBUserEmail = txtUserEmail.Value
            objBAL.CompanyId = CInt(GetSetCookies.GetCookie("CompanyId"))
            objBAL.IndustryId = CInt(GetSetCookies.GetCookie("IndustryId"))
            If CInt(Request.QueryString("isCustomise")) = 1 Then
                objBAL.CustomTabContent = txtCustomCode.Value
            Else
                objBAL.CustomTabContent = hdnSidebarContent.Value.ToString() 'ViewState("SavedTabContent")
            End If

            'objBAL.CustomTabContent = If(txtCustomCode.Value <> "", txtCustomCode.Value, hdnSidebarContent.Value.ToString()) 'divSidebarHtml.InnerHtml

            objBAL.UpdateCustomTabContentNew()
            'Response.Write(ViewState("SavedTabContent"))
            'Response.End()
            BindSavedCustomTab()

            Dim siteUrl As String = ConfigurationManager.AppSettings("AppPath") & "generate-ct-image.aspx?id=" & Request.QueryString("ctId")
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

            Dim strD As [String] = strPhoto
            If Not fullPath.EndsWith("\") Then
                fullPath += "\"
            End If
            Try
                If ConfigurationManager.AppSettings("AppPath") <> "http://so2.techsturedevelopment.com/" Then
                    If ViewState("MasterTabId").ToString <> "3" Then
                        If ViewState("ExpressTabId") = 10 Then
                            Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteUrl, 520, 670, 320, 670, fullPath, strD), System.Drawing.Image)
                        Else
                            Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteUrl, 520, 1000, 320, 1000, fullPath, strD), System.Drawing.Image)
                        End If
                    Else
                        strPhoto = "sidebar_banner_my_old_save.jpg"
                    End If
                End If
            Catch ex As Exception
            End Try
            objBAL.CustomTabId = Request.QueryString("ctId")
            objBAL.FBUserId = Session("FacebookUserId")
            objBAL.CustomTabImage = strPhoto
            objBAL.UpdateImageName()
            hdnPublished.Value = "0"
            If intSaveType = 1 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Your work has been saved');", True)
            ElseIf intSaveType = 2 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "PublishAlert();", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Custom tab shared successfully');", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error : " & ex.Message
        End Try
    End Sub
#End Region

#Region "Publish"
    Private Sub btnUpload_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.ServerClick
        'Try
        Dim result As String = ""
        Dim strPageId As String = ""
        Dim AppID As String = ""
        Dim strPageAccessToken As String = ""
        Dim objAppId As New BALCustomTab
        objAppId.CustomTabId = CInt(Request.QueryString("ctId"))
        AppID = objAppId.getApplicationIdByTabId()
        If AppID = "" Then
            If hdnType.Value = "2" Then
                AppID = System.Configuration.ConfigurationManager.AppSettings("FBAppIdFor810Tab").ToString()
            Else
                AppID = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
            End If
        End If
        AppID = Trim(AppID)
        Dim url As String = "https://graph.facebook.com/{0}/tabs/?app_id={2}&access_token={1}&method=post"
        For Each item As DataListItem In dstFanPages.Items
            Dim myCheckBox As HtmlInputRadioButton
            myCheckBox = CType(item.FindControl("chkPage"), HtmlInputRadioButton)
            If myCheckBox.Checked = True Then
                strPageId = CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
                strPageAccessToken = CType(item.FindControl("hdnAccessToken"), HtmlInputHidden).Value
                ViewState("PageName") = CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value
                If strPageId <> "" AndAlso strPageAccessToken <> "" Then
                    'ViewState("PageID") = strPageId
                    'ViewState("PageAccessToken") = strPageAccessToken
                    Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, strPageId, strPageAccessToken, AppID))
                    Dim fbResponse As WebResponse = fbRequest.GetResponse()
                    Dim stream As Stream = fbResponse.GetResponseStream()
                    Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                    Dim streamReader As New StreamReader(stream, encode)
                    result = streamReader.ReadToEnd()
                    streamReader.Close()
                End If

                Dim obj As New BALCustomTab
                obj.CustomTabId = CInt(Request.QueryString("ctId"))
                obj.FBPageId = strPageId
                obj.FBPageName = CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value
                obj.FBPageAccessToken = strPageAccessToken
                obj.UpdateIsPublishedCustomTab()

                Dim objCustomTab As New CustomTabContent
                objCustomTab.FBPageId = strPageId
                objCustomTab.FBApplicationId = AppID
                objCustomTab.FBPageName = CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value
                objCustomTab.FBUserId = Session("FacebookUserId")
                objCustomTab.Content = divSidebarHtml.InnerHtml
                objCustomTab.CustomTabId = CInt(Request.QueryString("ctId"))
                objCustomTab.AddEditCustomTabNew()

            End If
        Next
        If result.ToLower = "true" Then
            url = "https://graph.facebook.com/{0}/tabs?app_id={3}&access_token={1}&method=post&custom_name={2}"
            Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, strPageId, strPageAccessToken, "", AppID))
            Dim fbResponse As WebResponse = fbRequest.GetResponse()
            Dim stream As Stream = fbResponse.GetResponseStream()
            Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim streamReader As New StreamReader(stream, encode)
            result = streamReader.ReadToEnd()
            streamReader.Close()
        End If
        ClearData()
        BindSelectedFanPages(strPageId)
        BindSavedCustomTab()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Custom Tab published to your Page(s) successfully');", True)
        'Catch ex As Exception
        '    'lblMessage.Text = "Error: " & ex.Message
        '    lblMessage.Text = "Facebook is experiencing problems at this time. Please save your work and try to publish later."
        'End Try
    End Sub
    Private Sub SaveTabImages_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveTabImages.ServerClick

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowImageUploads", "ShowImageUploadPopup();", True)

        If Not (flimg1.PostedFile Is Nothing) And (CType(flimg1.PostedFile.FileName, String) <> "") Then

            Dim uploadContent1 = flimg1.PostedFile.ContentLength / 1000
            If uploadContent1 > 1000 Then
                lblimg1.Visible = True
                lblimg1.Text = "Please Upload Image Of Size Less Than 1 MB"
                lblimg1.Style.Add("color", "red")
            Else
                Dim file_img_s1 As HttpPostedFile = flimg1.PostedFile
                Dim file_name_s1 As String = Path.GetFileName(file_img_s1.FileName)
                Dim file_len_s1 As Integer = file_img_s1.ContentLength
                Dim file_typ_s1 As String = Path.GetExtension(file_img_s1.FileName).ToLower
                Dim strExt1 As String
                Dim strLogo1 As String = ""
                strExt1 = IO.Path.GetExtension(flimg1.PostedFile.FileName).ToLower
                Dim strDate1 As Date = "1/1/1900"
                strLogo1 = "icon1-" & CStr(DateDiff(DateInterval.Second, strDate1, Now())) & strExt1
                lblimg1.Visible = True
                lblimg1.Text = "https://so2.techsturedevelopment.com/Content/uploads/images/" & strLogo1
                If Not File.Exists(Server.MapPath("~\Content\uploads\images\" & strLogo1)) Then
                    file_img_s1.SaveAs(Server.MapPath("~\Content\uploads\images\" & strLogo1))
                End If
            End If
        End If

        If Not (flimg2.PostedFile Is Nothing) And (CType(flimg2.PostedFile.FileName, String) <> "") Then

            Dim uploadContent2 = flimg2.PostedFile.ContentLength / 1000
            If uploadContent2 > 1000 Then
                lblimg2.Visible = True
                lblimg2.Text = "Please Upload Image Of Size Less Than 1 MB"
                lblimg2.Style.Add("color", "red")
            Else
                Dim file_img_s2 As HttpPostedFile = flimg2.PostedFile
                Dim file_name_s2 As String = Path.GetFileName(file_img_s2.FileName)
                Dim file_len_s2 As Integer = file_img_s2.ContentLength
                Dim file_typ_s2 As String = Path.GetExtension(file_img_s2.FileName).ToLower
                Dim strExt2 As String
                Dim strLogo2 As String = ""
                strExt2 = IO.Path.GetExtension(flimg2.PostedFile.FileName).ToLower
                Dim strDate2 As Date = "2/2/2900"
                strLogo2 = "icon2-" & CStr(DateDiff(DateInterval.Second, strDate2, Now())) & strExt2
                lblimg2.Visible = True
                lblimg2.Text = "https://so2.techsturedevelopment.com/Content/uploads/images/" & strLogo2
                If Not File.Exists(Server.MapPath("~\Content\uploads\images\" & strLogo2)) Then
                    file_img_s2.SaveAs(Server.MapPath("~\Content\uploads\images\" & strLogo2))
                End If
            End If
        End If


        If Not (flimg3.PostedFile Is Nothing) And (CType(flimg3.PostedFile.FileName, String) <> "") Then

            Dim uploadContent3 = flimg3.PostedFile.ContentLength / 1000
            If uploadContent3 > 1000 Then
                lblimg3.Visible = True
                lblimg3.Text = "Please Upload Image Of Size Less Than 1 MB"
                lblimg3.Style.Add("color", "red")
            Else
                Dim file_img_s3 As HttpPostedFile = flimg3.PostedFile
                Dim file_name_s3 As String = Path.GetFileName(file_img_s3.FileName)
                Dim file_len_s3 As Integer = file_img_s3.ContentLength
                Dim file_typ_s3 As String = Path.GetExtension(file_img_s3.FileName).ToLower
                Dim strExt3 As String
                Dim strLogo3 As String = ""
                strExt3 = IO.Path.GetExtension(flimg3.PostedFile.FileName).ToLower
                Dim strDate3 As Date = "3/3/3900"
                strLogo3 = "icon3-" & CStr(DateDiff(DateInterval.Second, strDate3, Now())) & strExt3
                lblimg3.Visible = True
                lblimg3.Text = "https://so2.techsturedevelopment.com/Content/uploads/images/" & strLogo3
                If Not File.Exists(Server.MapPath("~\Content\uploads\images\" & strLogo3)) Then
                    file_img_s3.SaveAs(Server.MapPath("~\Content\uploads\images\" & strLogo3))
                End If
            End If
        End If

        If Not (flimg4.PostedFile Is Nothing) And (CType(flimg4.PostedFile.FileName, String) <> "") Then

            Dim uploadContent4 = flimg4.PostedFile.ContentLength / 1000
            If uploadContent4 > 1000 Then
                lblimg4.Visible = True
                lblimg4.Text = "Please Upload Image Of Size Less Than 1 MB"
                lblimg4.Style.Add("color", "red")
            Else
                Dim file_img_s4 As HttpPostedFile = flimg4.PostedFile
                Dim file_name_s4 As String = Path.GetFileName(file_img_s4.FileName)
                Dim file_len_s4 As Integer = file_img_s4.ContentLength
                Dim file_typ_s4 As String = Path.GetExtension(file_img_s4.FileName).ToLower
                Dim strExt4 As String
                Dim strLogo4 As String = ""
                strExt4 = IO.Path.GetExtension(flimg4.PostedFile.FileName).ToLower
                Dim strDate4 As Date = "4/4/4900"
                strLogo4 = "icon4-" & CStr(DateDiff(DateInterval.Second, strDate4, Now())) & strExt4
                lblimg4.Visible = True
                lblimg4.Text = "https://so2.techsturedevelopment.com/Content/uploads/images/" & strLogo4
                If Not File.Exists(Server.MapPath("~\Content\uploads\images\" & strLogo4)) Then
                    file_img_s4.SaveAs(Server.MapPath("~\Content\uploads\images\" & strLogo4))
                End If
            End If
        End If

        If Not (flimg5.PostedFile Is Nothing) And (CType(flimg5.PostedFile.FileName, String) <> "") Then

            Dim uploadContent5 = flimg5.PostedFile.ContentLength / 1000
            If uploadContent5 > 1000 Then
                lblimg5.Visible = True
                lblimg5.Text = "Please Upload Image Of Size Less Than 1 MB"
                lblimg5.Style.Add("color", "red")
            Else
                Dim file_img_s5 As HttpPostedFile = flimg5.PostedFile
                Dim file_name_s5 As String = Path.GetFileName(file_img_s5.FileName)
                Dim file_len_s5 As Integer = file_img_s5.ContentLength
                Dim file_typ_s5 As String = Path.GetExtension(file_img_s5.FileName).ToLower
                Dim strExt5 As String
                Dim strLogo5 As String = ""
                strExt5 = IO.Path.GetExtension(flimg5.PostedFile.FileName).ToLower
                Dim strDate5 As Date = "5/5/5900"
                strLogo5 = "icon5-" & CStr(DateDiff(DateInterval.Second, strDate5, Now())) & strExt5
                lblimg5.Visible = True
                lblimg5.Text = "https://so2.techsturedevelopment.com/Content/uploads/images/" & strLogo5
                If Not File.Exists(Server.MapPath("~\Content\uploads\images\" & strLogo5)) Then
                    file_img_s5.SaveAs(Server.MapPath("~\Content\uploads\images\" & strLogo5))
                End If
            End If
        End If

    End Sub

    'Private Sub SaveCode_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveCode.ServerClick

    '    dvTabSubDiv.Style.Add("padding-left", "80px")
    '    divSidebarHtml.InnerHtml = txtCustomCode.Value
    '    hdnSidebarContent.Value = txtCustomCode.Value
    '    hdnType.Value = "2"

    'End Sub
#End Region

#Region "Clear Data"
    Sub ClearData()
        For Each item As DataListItem In dstFanPages.Items
            Dim myCheckBox As HtmlInputRadioButton
            Dim HiddenID As HtmlInputHidden
            myCheckBox = CType(item.FindControl("chkPage"), HtmlInputRadioButton)
            HiddenID = CType(item.FindControl("hdnPageId"), HtmlInputHidden)
            myCheckBox.Checked = False
        Next
    End Sub
#End Region

    'Private Sub lknDownloadBasicCode_ServerClick(sender As Object, e As System.EventArgs) Handles lknDownloadBasicCode.ServerClick
    '    Response.AppendHeader("Content-disposition", "attachment; filename=custom-html-basic-code.txt")
    '    Response.ContentType = "text/csv"
    '    Response.WriteFile(Server.MapPath("~") & "/custom-html-basic-code.txt")
    '    Response.End()
    'End Sub
End Class