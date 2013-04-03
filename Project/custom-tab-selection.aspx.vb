Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System.Threading

Public Class custom_tab_selection
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim obj As New BALCustomTab
                Dim ds1 As New DataSet
                'obj.Page = "/custom-tab"
                'ds1 = obj.GetVideoTutorial()
                obj.CustomTabId = Request.QueryString("ctId")
                ds1 = obj.GetVideoTutorialByID()
                Dim videostring As String
                If ds1.Tables(0).Rows.Count > 0 Then
                    videostring = Replace(ds1.Tables(0).Rows(0).Item("ctm_Video").ToString, "watch?v=", "v/")
                    spnVideo.InnerHtml = "<object style='height: 335px; width: 555px'><param name='movie' value='" & videostring & "'><param name='allowFullScreen' value='true'><param name='allowScriptAccess' value='always'><param name='wmode' value='transparent'><embed src='" & videostring & "' type='application/x-shockwave-flash' allowfullscreen='true' allowScriptAccess='always' width='555' height='335'></object>"
                End If
                If Request.QueryString("ctId") <> "" And IsNumeric(Request.QueryString("ctId")) Then

                    BindFanPages()
                    BindSavedCustomTab()
                    BindCustomTab()

                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        End If
    End Sub

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
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, ";ShowHide13;", ";openNewWin('" & strUrl & "');", True)
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

    Public Function BindCustomTab()
        Try
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Dim ds As New DataSet
            Dim objBAL As New BALCustomTab
            objBAL.CustomTabId = CInt(Request.QueryString("ctId"))
            objBAL.UserId = CInt(Request.QueryString("userId"))
            objBAL.FBUserId = Request.QueryString("FbuserId")
            objBAL.CompanyId = CInt(Request.QueryString("CId"))
            objBAL.IndustryId = CInt(Request.QueryString("IId"))
            ds = objBAL.GetCustomTabMasterTemplates
            If ds.Tables(0).Rows.Count > 0 Then
                txtname.Text = ds.Tables(0).Rows(0).Item("CustomTabName")
                divSidebarHtml.InnerHtml = ds.Tables(0).Rows(0).Item("CustomTabContent")
                    Session("CustomTabID") = ds.Tables(0).Rows(0).Item("CustomTabId")

                hdnCTID.Value = Session("CustomTabID")
            End If
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
        End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Function

    Public Function BindSavedCustomTab()
        Try
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Dim ds As New DataSet
            Dim objBAL As New BALCustomTab
            objBAL.CustomTabId = CInt(Request.QueryString("ctId"))
            objBAL.UserId = CInt(Request.QueryString("userId"))
            objBAL.FBUserId = Request.QueryString("FbuserId")
            objBAL.CompanyId = CInt(Request.QueryString("CId"))
            objBAL.IndustryId = CInt(Request.QueryString("IId"))
            ds = objBAL.GetCustomTabMaster
                If ds.Tables(0).Rows.Count > 0 Then
                    hdnType.Value = ds.Tables(0).Rows(0).Item("ctm_Type")
                    If ds.Tables(0).Rows(0).Item("ctm_Type") = "2" Then
                        dvTabMainDiv.Style.Add("width", "1115px")
                        dvTabSubDiv.Style.Add("width", "820px")
                    Else
                        dvTabMainDiv.Style.Add("width", "825px")
                        dvTabSubDiv.Style.Add("width", "530px")
                    End If
                    txtname.Text = ds.Tables(0).Rows(0).Item("CustomTabName")
                    divSidebarHtml.InnerHtml = ds.Tables(0).Rows(0).Item("CustomTabContent")
                    Session("CustomTabID") = ds.Tables(0).Rows(0).Item("CustomTabId")
                    hdnCTID.Value = Session("CustomTabID")
                End If
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
        End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Function

    Public Function BindSavedCustomTabByID(ByVal id As Integer)
        Try
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Dim ds As New DataSet
            Dim objBAL As New BALCustomTab
            objBAL.CustomTabId = id
            objBAL.UserId = CInt(Request.QueryString("userId"))
            objBAL.FBUserId = Request.QueryString("FbuserId")
            objBAL.CompanyId = CInt(Request.QueryString("CId"))
            objBAL.IndustryId = CInt(Request.QueryString("IId"))
            ds = objBAL.GetCustomTabMasterByID
            If ds.Tables(0).Rows.Count > 0 Then
                    txtname.Text = ds.Tables(1).Rows(0).Item("CustomTabName")
                    txtCustomTabName.Value = ds.Tables(1).Rows(0).Item("CustomTabName")
                divSidebarHtml.InnerHtml = ds.Tables(0).Rows(0).Item("CustomTabContent")
                Session("CustomTabID") = ds.Tables(0).Rows(0).Item("CustomTabId")
                hdnCTID.Value = Session("CustomTabID")
            End If
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
        End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Function

    Private Sub lnkReset_ServerClick(sender As Object, e As System.EventArgs) Handles lnkReset.ServerClick
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
            ds = objBAL.GetCustomTabMasterTemplates
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

    Private Sub lnkSave_ServerClick(sender As Object, e As System.EventArgs) Handles lnkSave.ServerClick
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim dsId As New DataSet
                Dim objBAL As New BALCustomTab
                objBAL.CustomTabId = CInt(Request.QueryString("ctId"))
                objBAL.UserId = CInt(Request.QueryString("userId"))
                objBAL.FBUserId = Request.QueryString("FbuserId")
                objBAL.CustomTabName = If(txtCustomTabName.Value <> "", txtCustomTabName.Value, "")
                objBAL.CompanyId = CInt(Request.QueryString("CId"))
                objBAL.IndustryId = CInt(Request.QueryString("IId"))
                objBAL.CustomTabContent = hdnSidebarContent.Value

                ViewState("CustomTabID") = CInt(Request.QueryString("ctId"))
                ViewState("FBUserId") = Request.QueryString("FbuserId")

                dsId = objBAL.AddNewCustomTabContent()
                BindSavedCustomTabByID(CInt(dsId.Tables(0).Rows(0).Item("IDNew").ToString))


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
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert();", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

   Private Sub btnUpload_ServerClick(sender As Object, e As System.EventArgs) Handles btnUpload.ServerClick
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim result As String = ""
                Dim strPageId As String = ""
                Dim AppID As String = ""
                If hdnType.Value = "2" Then
                    AppID = ConfigurationManager.AppSettings("FBAppIdFor810Tab").ToString()

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
                            obj.CustomTabId = Session("CustomTabID") 'ViewState("CustomTabID")
                            obj.FBPageId = ViewState("PageID") 'CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
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
                            obj.CustomTabId = Session("CustomTabID") 'ViewState("CustomTabID")
                            obj.FBPageId = ViewState("PageID") 'CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
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
               
                'ClearData()
                BindSelectedFanPages(ViewState("PageID"))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PublishSaveAlert", "PublishSaveAlert();", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub btnSave_ServerClick(sender As Object, e As System.EventArgs) Handles btnSave.ServerClick
        'If hdnPublished.Value = "1" Then
        Dim dsId As New DataSet
        Dim objBAL As New BALCustomTab
        objBAL.CustomTabId = CInt(Request.QueryString("ctId"))
        objBAL.UserId = CInt(Request.QueryString("userId"))
        objBAL.FBUserId = Request.QueryString("FbuserId")
        objBAL.CustomTabName = If(txtCustomTabName.Value <> "", txtCustomTabName.Value, "")
        objBAL.CompanyId = CInt(Request.QueryString("CId"))
        objBAL.IndustryId = CInt(Request.QueryString("IId"))
        objBAL.CustomTabContent = hdnSidebarContent.Value

        ViewState("CustomTabID") = CInt(Request.QueryString("ctId"))
        ViewState("FBUserId") = Request.QueryString("FbuserId")

        dsId = objBAL.AddNewCustomTabContent()
        BindSavedCustomTabByID(CInt(dsId.Tables(0).Rows(0).Item("IDNew").ToString))


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
        'End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PublishAlert12", "PublishAlert();", True)
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
    Private Sub lnkRedirect_ServerClick(sender As Object, e As System.EventArgs) Handles lnkRedirect.ServerClick
        Response.Redirect("custom-tab-selection-edit.aspx?ctId=" & Session("CustomTabID") & "&userId=" & CInt(Request.QueryString("userId")) & "&FbuserId=" & Request.QueryString("FbuserId") & "&CId=" & Request.QueryString("CId") & "&IId=" & Request.QueryString("IId"))
    End Sub

    Private Sub lnkRedirectPublish_ServerClick(sender As Object, e As System.EventArgs) Handles lnkRedirectPublish.ServerClick
        Response.Redirect("custom-tab-selection-edit.aspx?ctId=" & Session("CustomTabID") & "&userId=" & CInt(Request.QueryString("userId")) & "&FbuserId=" & Request.QueryString("FbuserId") & "&CId=" & Request.QueryString("CId") & "&IId=" & Request.QueryString("IId"))
    End Sub

    Private Sub lnkRedirectShare_ServerClick(sender As Object, e As System.EventArgs) Handles lnkRedirectShare.ServerClick
        Response.Redirect("custom-tab-selection-edit.aspx?ctId=" & Session("CustomTabID") & "&userId=" & CInt(Request.QueryString("userId")) & "&FbuserId=" & Request.QueryString("FbuserId") & "&CId=" & Request.QueryString("CId") & "&IId=" & Request.QueryString("IId"))
    End Sub

    Private Sub lnkShareUserEmail_ServerClick(sender As Object, e As System.EventArgs) Handles lnkShareUserEmail.ServerClick
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim dsId As New DataSet
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

                dsId = objBAL.AddNewCustomTabContent()
                BindSavedCustomTabByID(CInt(dsId.Tables(0).Rows(0).Item("IDNew").ToString))


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
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShareAlert", "ShareAlert();", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub
End Class