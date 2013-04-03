Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System.Threading
Public Class selected_sweepstake
    Inherits System.Web.UI.Page

    Public strPageId As String = ""
    Protected strPageReturnCatId As String = ""
    Public strFBUserId As String
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoginCheck.LoginSessionCheck()
        strFBUserId = Session("FacebookUserId").ToString
        If Not Page.IsPostBack Then
            Dim obj As New BALCustomTab
            Dim ds1 As New DataSet
            obj.swid = Request.QueryString("swid")
            ds1 = obj.GetVideoTutorialByID()
            Dim videostring As String
            If ds1.Tables(0).Rows.Count > 0 Then
                videostring = Replace(ds1.Tables(0).Rows(0).Item("ctm_Video").ToString, "watch?v=", "v/")
                spnVideo.InnerHtml = "<object style='height: 335px; width: 555px'><param name='movie' value='" & videostring & "'><param name='allowFullScreen' value='true'><param name='allowScriptAccess' value='always'><param name='wmode' value='transparent'><embed src='" & videostring & "' type='application/x-shockwave-flash' allowfullscreen='true' allowScriptAccess='always' width='555' height='335'></object>"
            End If
            If Request.QueryString("swid") <> "" And IsNumeric(Request.QueryString("swid")) Then
                BindFanPages()
                BindSaveSweepstakeByID()
            End If
            'hdnSaveMenu.Value = Request.QueryString("sid")
        End If
        If CInt(hdnSaveMenu.Value) = 2 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ChangeMenuStep", "SaveLikeUnlike(2);", True)
        ElseIf CInt(hdnSaveMenu.Value) = 3 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ChangeMenuStep", "SaveLikeUnlike(3);", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ChangeMenuStep", "SaveLikeUnlike(1);", True)
        End If
        
    End Sub



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
                'Dim strUrl As String = "Login.aspx?i=" & Session("Industry") & "&c=" & Session("company") & "&u=1&at=" & Session("hdnToken") & ""
                'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, ";ShowHide13;", ";openNewWin('" & strUrl & "');", True)
                dstFanPages.DataSource = Nothing
                dstFanPages.DataBind()
                plcData.Visible = False
                plcNoData.Visible = True
            End If
        Catch ex As Exception
            lblMessage.Text = "Facebook is experiencing problems at this time. Please save your work and try to publish later."
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
                'Dim strUrl As String = "Login.aspx?i=" & Session("Industry") & "&c=" & Session("company") & "&u=1&at=" & Session("hdnToken") & ""
                'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, ";ShowHide12;", ";openNewWin('" & strUrl & "');", True)
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
        Catch ex As Exception
            lblMessage.Text = "Facebook is experiencing problems at this time. Please save your work and try to publish later."
        End Try
    End Sub
#End Region

#Region "Bind Saved Custom Tab By Id"
    Public Sub BindSaveSweepstakeByID()
        Try
            Dim ds As New DataSet
            Dim objBAL As New BALCustomTab
            objBAL.swid = Request.QueryString("swid")
            objBAL.UserId = CInt(Session("SiteUserId"))
            objBAL.FBUserId = Session("FacebookUserId")
            objBAL.FBPageId = If(Session("strPageId") <> Nothing, Session("strPageId"), "")
            objBAL.FBUserEmail = If(Session("FacebookUserEmail") <> Nothing, Session("FacebookUserEmail"), "")
            objBAL.CompanyId = CInt(GetSetCookies.GetCookie("CompanyId"))
            objBAL.IndustryId = CInt(GetSetCookies.GetCookie("IndustryId"))
            ds = objBAL.GetSaveSweepstakeByID
            If ds.Tables(0).Rows.Count > 0 Then
                txtname.Text = ds.Tables(0).Rows(0).Item("name")
                txtCustomTabName.Value = ds.Tables(0).Rows(0).Item("name")
                divSweepstakeHtml.InnerHtml = ds.Tables(0).Rows(0).Item("cs_content")
                'divLikeHtml.InnerHtml = ds.Tables(0).Rows(0).Item("cs_content")
                'divUnLikeHtml.InnerHtml = ds.Tables(0).Rows(0).Item("cs_UnLikecontent")
                hdnCTID.Value = ds.Tables(0).Rows(0).Item("cs_id")
                ViewState("sw_type") = ds.Tables(0).Rows(0).Item("sw_type")
                If ds.Tables(0).Rows(0).Item("sw_type") = "1" Then
                    dvTabSubDiv.Style.Add("padding-left", "140px")
                    'dvTabMainDiv.Style.Add("width", "1115px")
                    'dvTabSubDiv.Style.Add("width", "820px")
                Else
                    'dvTabMainDiv.Style.Add("width", "825px")
                    'dvTabSubDiv.Style.Add("padding-left", "140px")
                    'End If
                    hdnCTID.Value = ds.Tables(0).Rows(0).Item("cs_id")
                End If

                txtShareEmailText.InnerHtml = ds.Tables(0).Rows(0).Item("cs_EmailText")
                txtShareMessage.Text = ds.Tables(0).Rows(0).Item("cs_ShareMessage")
                txtShareText.Text = ds.Tables(0).Rows(0).Item("cs_ShareText")
                ViewState("strImageName") = ds.Tables(0).Rows(0).Item("cs_ShareImage")
                ViewState("strPDFName") = ds.Tables(0).Rows(0).Item("cs_PDFLink")
                lnkShareImage.HRef = ConfigurationManager.AppSettings("AppPath") & "Content/uploads/images/" & ds.Tables(0).Rows(0).Item("cs_ShareImage")
                lblShareImage.Text = ds.Tables(0).Rows(0).Item("cs_ShareImage")
                lnkSharePdf.HRef = ConfigurationManager.AppSettings("AppPath") & "Content/uploads/pdf/" & ds.Tables(0).Rows(0).Item("cs_PDFLink")
                lblSharePDF.Text = ds.Tables(0).Rows(0).Item("cs_PDFLink")
                Session("FanPageSelected") = ds.Tables(0).Rows(0).Item("cs_FBPageId").ToString
                Session("FanPageName") = ds.Tables(0).Rows(0).Item("cs_FBPageName").ToString
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("cs_IsPublished")) AndAlso ds.Tables(0).Rows(0).Item("cs_IsPublished") = 1 Then
                    BindSelectedFanPages(Session("FanPageSelected"))
                ElseIf Session("FanPageSelected") <> "" AndAlso Not IsDBNull(Session("FanPageSelected")) Then
                    For Each item As DataListItem In dstFanPages.Items
                        Dim myCheckBox As HtmlInputRadioButton
                        Dim HiddenID As New HtmlInputHidden
                        myCheckBox = CType(item.FindControl("chkPage"), HtmlInputRadioButton)
                        HiddenID = CType(item.FindControl("hdnPageId"), HtmlInputHidden)
                        Dim str As String = CStr(HiddenID.Value)
                        If Trim(str) <> "" Then
                            If Trim(str) = Trim(Convert.ToString(ds.Tables(0).Rows(0).Item("cs_FBPageId"))) Then
                                myCheckBox.Checked = True
                            Else
                                myCheckBox.Disabled = True
                            End If
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
                spnAssigned.InnerHtml = "A member of Total Social Media has shared this Sweepstake with you for viewing purposes only. To edit, please create a copy from '<b>My Saved Sweepstakes</b>' and edit that copy"
                divUnLikeHtml.Visible = False
                'spnAssigned.InnerHtml = "An administrator of business page <b>" & ds.Tables(0).Rows(0).Item("ct_FBPageName") & "</b> has shared this Custom Tab with you for viewing purposes only. To edit, please create a copy in '<b><a href='custom-tab' title='My Saved Cover Photos'>My Saved Custom tabs</a></b>' and edit that copy"
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub
#End Region


#Region "Bind Saved Custom Tab By Id"
    'Public Sub BindSaveSweepstakeByID(ByVal id As Integer)
    '    Try
    '        Dim ds As New DataSet
    '        Dim objBAL As New BALCustomTab
    '        objBAL.swid = id
    '        objBAL.UserId = CInt(Session("SiteUserId"))
    '        objBAL.FBUserId = Session("FacebookUserId")
    '        objBAL.CompanyId = CInt(GetSetCookies.GetCookie("CompanyId"))
    '        objBAL.IndustryId = CInt(GetSetCookies.GetCookie("IndustryId"))
    '        ds = objBAL.GetSaveSweepstakeByID
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            txtname.Text = ds.Tables(1).Rows(0).Item("name")
    '            txtCustomTabName.Value = ds.Tables(1).Rows(0).Item("name")
    '            divSidebarHtml.InnerHtml = ds.Tables(0).Rows(0).Item("sw_content")
    '            hdnCTID.Value = ds.Tables(0).Rows(0).Item("sw_id")
    '        End If
    '    Catch ex As Exception
    '        lblMessage.Text = "Error: " & ex.Message
    '    End Try
    'End Sub
#End Region

#Region "Reset"
    Private Sub lnkReset_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReset.ServerClick
        'BindSaveSweepstakeById(ob)
    End Sub
#End Region


#Region "Upload/Publish"
    Private Sub btnUpload_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.ServerClick

        
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim result As String = ""
                Dim AppID As String = ""
                If CInt(GetSetCookies.GetCookie("CompanyId")) = 12 Then
                    AppID = System.Configuration.ConfigurationManager.AppSettings("CustomizeLexusSweepStakeFBAPI").ToString()
                ElseIf CInt(GetSetCookies.GetCookie("CompanyId")) = 13 Then
                    AppID = System.Configuration.ConfigurationManager.AppSettings("CustomizeAmerifirstSweepStakeFBAPI").ToString()
                Else
                    If ViewState("sw_type") = 1 Then

                        AppID = System.Configuration.ConfigurationManager.AppSettings("CustomizeSweepStakeFBAPI520").ToString()
                    Else

                        AppID = System.Configuration.ConfigurationManager.AppSettings("CustomizeSweepStakeFBAPI810").ToString()
                    End If
                End If
                Dim url As String = "https://graph.facebook.com/{0}/tabs/?app_id={2}&access_token={1}&method=post"
                For Each item As DataListItem In dstFanPages.Items
                    Dim myCheckBox As HtmlInputRadioButton
                    myCheckBox = CType(item.FindControl("chkPage"), HtmlInputRadioButton)
                    If myCheckBox.Checked = True Then
                        Dim strPageId As String = CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
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


                            Dim obj As New BALCustomTab
                            'obj.CustomTabId = CInt(Request.QueryString("swId"))
                            'obj.FBPageId = strPageId
                            'obj.FBPageName = CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value
                            'obj.FBPageAccessToken = strPageAccessToken
                            'obj.UpdateIsPublishedSweepstake()

                            'Dim objCustomTab As New CustomTabContent
                            obj.FBPageId = ViewState("PageID")
                            obj.FBSweepstakeAppId = AppID
                            obj.FBPageName = ViewState("PageName")
                            obj.FBUserId = Session("FacebookUserId")
                            obj.FBUserEmail = Session("FacebookUserEmail")
                            'obj.CustomTabContent = divSidebarHtml.InnerHtml
                            obj.swid = CInt(Request.QueryString("swId"))
                            obj.ShareText = txtShareText.Text
                            obj.ShareMessage = txtShareMessage.Text
                            obj.ShareEmailText = txtShareEmailText.InnerHtml
                            obj.ShareImage = ViewState("strImageName")
                            obj.SharePDF = ViewState("strPDFName")
                            obj.CustomTabContent = hdnLike.Value 'hdnSidebarContent.Value
                            'obj.UnlikeSwContent = hdnUnLike.Value
                            'If (rdLike.Checked = True) Then
                            '    obj.getSwType = 1
                            'Else
                            '    obj.getSwType = 2
                            'End If
                            obj.AddEditPublishedSweepstakeData()
                            'obj.UpdateIsPublishedSweepstake()
                        End If
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
                End If
                'BindSaveSweepstakeByID(ViewState("SavedTabId"))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "PublishSaveAlert('Sweepstakes published successfully');", True)
            Else
                'Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
            End If
        Catch ex As Exception
            lblmessage.Text = "Error: " & ex.Message
        End Try

        ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseProgressbar", "SaveAlert('Sweepstake Uploaded Successfully');", True)


        'Dim strMsg As String = ""
        'Try
        '    Dim result As String = ""
        '    Dim AppID = System.Configuration.ConfigurationManager.AppSettings("SweepStakeFBAPI").ToString()
        '    Dim url As String = "https://graph.facebook.com/{0}/tabs/?app_id={2}&access_token={1}&method=post"
        '    Dim strPageId As String = Request("pid")
        '    Dim strPageAccessToken As String = Request("pat")
        '    ViewState("PageName") = Request("pnm")
        '    If strPageId <> "" AndAlso strPageAccessToken <> "" Then
        '        ViewState("PageID") = strPageId
        '        ViewState("PageAccessToken") = strPageAccessToken
        '        Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, strPageId, strPageAccessToken, AppID))
        '        Dim fbResponse As WebResponse = fbRequest.GetResponse()
        '        Dim stream As Stream = fbResponse.GetResponseStream()
        '        Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
        '        Dim streamReader As New StreamReader(stream, encode)
        '        result = streamReader.ReadToEnd()
        '        streamReader.Close()
        '    End If

        '    If result.ToLower = "true" Then
        '        url = "https://graph.facebook.com/{0}/tabs?app_id={3}&access_token={1}&method=post&custom_name={2}"
        '        Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, ViewState("PageID"), ViewState("PageAccessToken"), "Sweepstake", AppID))
        '        Dim fbResponse As WebResponse = fbRequest.GetResponse()
        '        Dim stream As Stream = fbResponse.GetResponseStream()
        '        Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
        '        Dim streamReader As New StreamReader(stream, encode)
        '        result = streamReader.ReadToEnd()
        '        streamReader.Close()
        '        'lnkEditTab.HRef = "http://www.facebook.com/pages/" & ViewState("PageName").ToString.Replace(" ", "-") & "/" & ViewState("PageID") & "/?sk=app_" & AppID
        '    End If
        'Catch ex As Exception
        '    strMsg = "Error: " & ex.Message
        'End Try
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


    Private Sub SaveTab(ByVal intSaveType As Int16)

        'If (intSaveType = 4) Then
        '    Try
        '        Dim ds As New DataSet
        '        Dim intSavedTabId As Integer = 0
        '        Dim objBAL As New BALCustomTab
        '        objBAL.CustomTabId = CInt(Request.QueryString("swid"))
        '        objBAL.swid = CInt(Request.QueryString("swid"))
        '        objBAL.UserId = CInt(Session("SiteUserId"))
        '        objBAL.CustomTabName = txtCustomTabName.Value
        '        objBAL.CustomTabContent = hdnSidebarContent.Value
        '        objBAL.FBUserId = Session("FacebookUserId")
        '        objBAL.FBUserEmail = txtUserEmail.Value
        '        objBAL.FBPageAccessToken = Session("FacebookAccessToken")
        '        objBAL.CompanyId = CInt(GetSetCookies.GetCookie("CompanyId"))
        '        objBAL.IndustryId = CInt(GetSetCookies.GetCookie("IndustryId"))
        '        objBAL.ShareText = txtShareText.Text
        '        objBAL.ShareMessage = txtShareMessage.Text
        '        ' objBAL.ShareEmailAddress = txtShareEmailadd.Text
        '        objBAL.ShareEmailText = txtShareEmailText.InnerHtml

        '        If ViewState("ImageName") Is Nothing Then
        '            ViewState("ImageName") = "default-share-image.png"
        '        End If

        '        If ViewState("PDFName") Is Nothing Then
        '            ViewState("PDFName") = "default-terms.pdf"
        '        End If


        '        objBAL.ShareImage = ViewState("ImageName")
        '        objBAL.SharePDF = ViewState("PDFName")

        '        objBAL.UpdateSweepstakeContent()

        '        BindSaveSweepstakeByID()
        '        'BindSaveSweepstakeByID(temp)
        '        Dim siteUrl As String = ConfigurationManager.AppSettings("AppPath") & "generate-sw-image.aspx?id=" & CInt(ViewState("SavedTabId"))
        '        Dim browserWidth As Integer = Convert.ToInt32(800)
        '        Dim browserHeight As Integer = Convert.ToInt32(600)
        '        Dim thumbnailWidth As Integer = Convert.ToInt32(800)
        '        Dim thumbnailHeight As Integer = Convert.ToInt32(600)
        '        Dim relativeImagePath As String = System.Configuration.ConfigurationManager.AppSettings("uploadpath")
        '        Dim fullPath As String = Server.MapPath(relativeImagePath)
        '        Dim strExt As String = ""
        '        Dim strPhoto As String = ""
        '        Dim strDate As Date = "1/1/1900"
        '        strPhoto = "customtab-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & ".jpg"

        '        Dim strD As String = strPhoto
        '        If Not fullPath.EndsWith("\") Then
        '            fullPath += "\"
        '        End If

        '        Try
        '            Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteUrl, 520, 700, 320, 700, fullPath, strD), System.Drawing.Image)
        '        Catch ex As Exception
        '        End Try

        '        objBAL.swid = CInt(ViewState("SavedTabId"))
        '        objBAL.FBUserId = Session("FacebookUserId")
        '        objBAL.CustomTabImage = strPhoto
        '        objBAL.UpdateSweeptakeImageName()

        '        If intSaveType = 1 Then
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert();", True)
        '        ElseIf intSaveType = 2 Then
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PublishAlert12", "PublishAlert();", True)
        '        Else
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShareAlert", "ShareAlert();", True)
        '        End If
        '    Catch ex As Exception
        '        lblMessage.Text = "Error: " & ex.Message
        '    End Try
        'Else
        lblMessage.Text = ""
        Dim ds As New DataSet
        Dim intSavedTabId As Integer = 0
        Dim strDate As Date
        Dim objBAL As New BALCustomTab
        Dim strExt As String
        Dim strShareImage As String = ""
        Dim strPDFName As String = ""
        Dim strPhoto As String = ""
        strDate = "1/1/1900"
        If Not (ShareImg.PostedFile Is Nothing) And (CType(ShareImg.PostedFile.FileName, String) <> "") Then
            Dim file_img_s As HttpPostedFile = ShareImg.PostedFile
            Dim file_name_s As String = Path.GetFileName(file_img_s.FileName)
            Dim file_len_s As Integer = file_img_s.ContentLength
            Dim file_typ_s As String = Path.GetExtension(file_img_s.FileName).ToLower
            If Not (file_typ_s = ".jpg" Or file_typ_s = ".gif" Or file_typ_s = ".bmp" Or file_typ_s = ".jpeg" Or file_typ_s = ".png") Then
                lblMessage.Text = "Share Image name must be .jpg or .gif or .png or .jpeg or .tif or .bmp"
                Exit Sub
            End If
            'If file_len_s > 8000000 Then
            '    lblMessage.Text = "Image - File Upload Size exceed maximum (2000000 Bytes)"
            '    Exit Sub
            'End If

            strExt = IO.Path.GetExtension(ShareImg.PostedFile.FileName).ToLower
            strShareImage = "cs-share-img-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & strExt

            If Not File.Exists(Server.MapPath("~\Content\uploads\images\" & strShareImage)) Then
                file_img_s.SaveAs(Server.MapPath("~\Content\uploads\images\" & strShareImage))
            End If

            ViewState("strImageName") = strShareImage
        End If

        If Not (PDffile.PostedFile Is Nothing) And (CType(PDffile.PostedFile.FileName, String) <> "") Then
            Dim file_pdf As HttpPostedFile = PDffile.PostedFile

            Dim file_name_pdf As String = Path.GetFileName(file_pdf.FileName)
            strExt = IO.Path.GetExtension(PDffile.PostedFile.FileName).ToLower
            Dim file_len_pdf As Integer = file_pdf.ContentLength
            Dim file_typ_pdf As String = Path.GetExtension(file_pdf.FileName).ToLower
            If Not (file_typ_pdf = ".pdf") Then
                'lblMessage.Text = "Please upload .pdf file as PDF in Share and Email Settings "
                lblMessage.Text = "We only accept .pdf files"
                Exit Sub
            End If

            strPDFName = "cs-pdf-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & strExt
            If Not File.Exists(Server.MapPath("~\content\uploads\pdf\" & strPDFName)) Then
                file_pdf.SaveAs(Server.MapPath("~\content\uploads\pdf\" & strPDFName))
            End If

            ViewState("strPDFName") = strPDFName

        End If

        If ViewState("strImageName") = "" Then
            ViewState("strImageName") = "default-share-image.png"
        End If
        If ViewState("strPDFName") = "" Then
            ViewState("strPDFName") = "default-terms.pdf"
        End If



        Try
            objBAL.CustomTabId = CInt(Request.QueryString("swid"))
            objBAL.swid = CInt(Request.QueryString("swid"))
            objBAL.UserId = CInt(Session("SiteUserId"))
            objBAL.CustomTabName = txtCustomTabName.Value
            objBAL.CustomTabContent = hdnLike.Value 'hdnSidebarContent.Value
            'objBAL.UnlikeSwContent = hdnUnLike.Value
            objBAL.FBUserId = Session("FacebookUserId")
            objBAL.FBUserEmail = txtUserEmail.Value
            objBAL.FBPageAccessToken = Session("FacebookAccessToken")
            objBAL.CompanyId = CInt(GetSetCookies.GetCookie("CompanyId"))
            objBAL.IndustryId = CInt(GetSetCookies.GetCookie("IndustryId"))
            objBAL.ShareText = txtShareText.Text
            objBAL.ShareMessage = txtShareMessage.Text
            'objBAL.ShareEmailAddress = txtShareEmailadd.Text
            objBAL.ShareEmailText = txtShareEmailText.InnerHtml

            'If ViewState("ImageName") Is Nothing Then
            '    ViewState("ImageName") = "default-share-image.png"
            'End If

            'If ViewState("PDFName") Is Nothing Then
            '    ViewState("PDFName") = "default-terms.pdf"
            'End If


            objBAL.ShareImage = ViewState("strImageName")
            objBAL.SharePDF = ViewState("strPDFName")

            objBAL.UpdateSweepstakeCustomiseContent()

            BindSaveSweepstakeByID()
            'BindSaveSweepstakeByID(temp)
            Dim siteUrl As String = ConfigurationManager.AppSettings("AppPath") & "generate-sw-image.aspx?id=" & CInt(Request.QueryString("swid"))
            Dim browserWidth As Integer = Convert.ToInt32(800)
            Dim browserHeight As Integer = Convert.ToInt32(600)
            Dim thumbnailWidth As Integer = Convert.ToInt32(800)
            Dim thumbnailHeight As Integer = Convert.ToInt32(600)
            Dim relativeImagePath As String = System.Configuration.ConfigurationManager.AppSettings("uploadpath")
            Dim fullPath As String = Server.MapPath(relativeImagePath)

            strPhoto = "customize-sweepstake-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & ".jpg"

            Dim strD As String = strPhoto
            If Not fullPath.EndsWith("\") Then
                fullPath += "\"
            End If

            Try
                Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteUrl, 520, 700, 320, 700, fullPath, strD), System.Drawing.Image)
            Catch ex As Exception
                'lblMessage.Text = "Error: " & ex.Message
            End Try

            objBAL.swid = CInt(Request.QueryString("swid")) 'CInt(ViewState("SavedTabId"))
            objBAL.FBUserId = Session("FacebookUserId")
            objBAL.CustomTabImage = strPhoto
            objBAL.UpdateSweeptakeImageName()

            If intSaveType = 1 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert();", True)
            ElseIf intSaveType = 2 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PublishAlert12", "PublishAlert();", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShareAlert", "ShareAlert();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
        'End If
    End Sub

    Private Sub csswdetails_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles csswdetails.ServerClick
        Dim objBAL As New BALCustomTab

        If Not (ShareImg.PostedFile Is Nothing) And (CType(ShareImg.PostedFile.FileName, String) <> "") Then
            Dim file_img_s As HttpPostedFile = ShareImg.PostedFile
            Dim file_name_s As String = Path.GetFileName(file_img_s.FileName)
            Dim file_len_s As Integer = file_img_s.ContentLength
            Dim file_typ_s As String = Path.GetExtension(file_img_s.FileName).ToLower
            If Not (file_typ_s = ".jpg" Or file_typ_s = ".gif" Or file_typ_s = ".bmp" Or file_typ_s = ".jpeg" Or file_typ_s = ".png") Then
                'ltrMsg.Text = "File must be .jpg or .gif or .png or .jpeg or .tif or .bmp"
                Exit Sub
            End If
            'If file_len_s > 8000000 Then
            '    lblMessage.Text = "Image - File Upload Size exceed maximum (2000000 Bytes)"
            '    Exit Sub
            'End If
            Dim strExt As String
            Dim strLogo As String = ""
            strExt = IO.Path.GetExtension(ShareImg.PostedFile.FileName).ToLower
            Dim strDate As Date = "1/1/1900"
            strLogo = "icon-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & strExt

            If Not File.Exists(Server.MapPath("~\Content\uploads\images\" & file_name_s)) Then
                file_img_s.SaveAs(Server.MapPath("~\Content\uploads\images\" & file_name_s))
            End If

            Dim file_pdf As HttpPostedFile = PDffile.PostedFile
            Dim file_name_pdf As String = Path.GetFileName(file_pdf.FileName)


            Dim file_len_pdf As Integer = file_pdf.ContentLength
            Dim file_typ_pdf As String = Path.GetExtension(file_pdf.FileName).ToLower
            If Not (file_typ_pdf = ".pdf") Then
                'ltrMsg.Text = "File must be .pdf "
                Exit Sub
            End If

            If Not File.Exists(Server.MapPath("~\content\uploads\pdf\" & file_name_pdf)) Then
                file_pdf.SaveAs(Server.MapPath("~\content\uploads\pdf\" & file_name_pdf))
            End If


            objBAL.ShareText = txtShareText.Text
            objBAL.ShareMessage = txtShareMessage.Text
            ' objBAL.ShareEmailAddress = txtShareEmailadd.Text
            objBAL.ShareEmailText = txtShareEmailText.InnerHtml
            ViewState("ImageName") = file_name_s
            If ViewState("ImageName") Is Nothing Then
                ViewState("ImageName") = "general-image.pdf"
            End If
            ViewState("PDFName") = file_name_pdf
            If ViewState("PDFName") Is Nothing Then
                ViewState("PDFName") = "default-terms.pdf"
            End If
        End If
        SaveTab(4)
    End Sub
#End Region

#Region "Redirect"
    'Private Sub lnkRedirect_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRedirect.ServerClick
    '    Redirect()
    'End Sub

    'Private Sub lnkRedirectPublish_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRedirectPublish.ServerClick
    '    Redirect()
    'End Sub

    'Private Sub lnkRedirectShare_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRedirectShare.ServerClick
    '    Redirect()
    'End Sub
    Private Sub Redirect()
        If Convert.ToString(Request.QueryString("swid")) <> "" Then
            Response.Redirect("selected-sweepstake.aspx?swid=" & CInt(Request.QueryString("swid")) & "&sid=" & hdnSaveMenu.Value)
        Else
            Response.Redirect("sweepstake-list.aspx")
        End If
    End Sub
    Private Sub lnkdownloadrulepdf_ServerClick(sender As Object, e As System.EventArgs) Handles lnkdownloadrulepdf.ServerClick
        Response.AppendHeader("Content-disposition", "attachment; filename=Sample-Privacy-Policy-and-Rules")
        Response.ContentType = "application/doc"
        Response.WriteFile(Server.MapPath("~") & "/Content/uploads/pdf/Sample-Privacy-Policy-and-Rules.doc")
        Response.End()
    End Sub
#End Region


End Class