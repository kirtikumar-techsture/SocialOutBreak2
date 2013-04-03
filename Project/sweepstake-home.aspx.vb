Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System.Threading

Public Class sweepstake_home
    Inherits System.Web.UI.Page

#Region "Page Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim obj As New BAlsidebar
                Dim ds1 As New DataSet
                obj.Page = CStr(HttpContext.Current.Request.Url.AbsolutePath)

                ds1 = obj.GetVideoTutorial()

                If CInt(GetSetCookies.GetCookie("CompanyId")) = 12 Then
                    imgSweep.Src = "Content/images/LOBSweepstakes_Contest_cover.jpg"
                ElseIf CInt(GetSetCookies.GetCookie("CompanyId")) = 13 Then
                    imgSweep.Src = "Content/images/amerifirst_sw_cover.png"
                ElseIf CInt(GetSetCookies.GetCookie("CompanyId")) = 15 Then
                    imgSweep.Src = "Content/images/Boresha_Sweepstakes/home_boresha_after_like.png"
                Else
                    imgSweep.Src = "Content/images/Contest_cover.jpg"
                End If


                If HttpContext.Current.Request.Url.AbsolutePath = ds1.Tables(0).Rows(0).Item("vt_Page").ToString Then

                    strVideo1.HRef = ds1.Tables(0).Rows(0).Item("vt_Video").ToString & "?fs=1&autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer"
                    strVideo1.Title = ds1.Tables(0).Rows(0).Item("vt_Title").ToString
                    btnVideo.HRef = ds1.Tables(0).Rows(0).Item("vt_Video").ToString & "?fs=1&autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer"
                    btnVideo.Title = ds1.Tables(0).Rows(0).Item("vt_Title").ToString
                    imgsweepstake.Src = ds1.Tables(0).Rows(0).Item("vt_VideoThumbnail").ToString
                End If

            Else
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
            End If
        Catch ex As Exception
            lblmessage.Text = "Error: " & ex.Message
        End Try

        If Page.IsPostBack = False Then
            BindFanPages()
        End If

    End Sub
#End Region


#Region "Bind Fan Pages"
    Sub BindFanPages()
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
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
                 'Dim listPages As New List(Of FanPage.m_data)

                'For Each item As FanPage.m_data In fPage.data
                '    listPages.Add(item)
                'Next
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
                    dstFanPages.DataSource = Nothing
                    dstFanPages.DataBind()
                    plcData.Visible = False
                    plcNoData.Visible = True
                End If
            Else
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
            End If
        Catch ex As Exception
            'lblmessage.Text = "Error: " & ex.Message
			 plcData.Visible = False
            plcNoData.Visible = False
            plcError.Visible = True
        End Try
    End Sub
#End Region

    Private Sub btnUpload_ServerClick(sender As Object, e As System.EventArgs) Handles btnUpload.ServerClick

       
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim result As String = ""
                Dim AppID As String = ""
                If CInt(GetSetCookies.GetCookie("CompanyId")) = 12 Then
                    AppID = System.Configuration.ConfigurationManager.AppSettings("LexusSweepStakeFBAPI").ToString()
                ElseIf CInt(GetSetCookies.GetCookie("CompanyId")) = 13 Then
                    AppID = System.Configuration.ConfigurationManager.AppSettings("AmerifirstSweepStakeFBAPI").ToString()
                ElseIf CInt(GetSetCookies.GetCookie("CompanyId")) = 15 Then
                    AppID = System.Configuration.ConfigurationManager.AppSettings("BoreshaSweepStakeFBAPI").ToString()
                Else
                    AppID = System.Configuration.ConfigurationManager.AppSettings("SweepStakeFBAPI").ToString()
                End If

                Dim url As String = "https://graph.facebook.com/{0}/tabs/?app_id={2}&access_token={1}&method=post"
                For Each item As DataListItem In dstFanPages.Items
                    Dim myCheckBox As HtmlInputCheckBox
                    myCheckBox = CType(item.FindControl("chkPage"), HtmlInputCheckBox)
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
                            Dim objSweepStake As New BALSweepStake

                            objSweepStake.TSMAUserId = Session("SiteUserId")
                            objSweepStake.CompanyId = GetSetCookies.GetCookie("CompanyId") 'Session("CompanyId")
                            objSweepStake.IndustryId = GetSetCookies.GetCookie("IndustryId") 'Session("IndustryId")
                            objSweepStake.FBUserId = Session("FacebookUserId")
                            objSweepStake.FBPageId = ViewState("PageID")
                            objSweepStake.FBPageName = ViewState("PageName")
                            '  objSweepStake.Title = txtTabTitle.Value
                            objSweepStake.Content = divSidebarHtml.InnerHtml
                            objSweepStake.AddEditSweepStake()
                        End If
                    End If
                    'lblmessage.Text = ViewState("PageID") & ViewState("PageName")
                   
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

                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Sweepstake Uploaded Successfully');", True)
                    '  lblmessage.Text = "Sweepstakes Uploaded Successfully"
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Sweepstakes published successfully');", True)
            Else
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
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
End Class