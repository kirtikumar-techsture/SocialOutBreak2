Imports System.Net
Imports System.Runtime.Serialization.Json
Imports BusinessAccessLayer.BusinessLayer
Imports System.IO
Imports Facebook
Imports System.Threading
Public Class configure_autopost
    Inherits RoutablePage
    Implements IRequiresSessionState
    Dim objLib As New Library
    Public strSelectionType As String = ""
    Public intAutoPostOnOff As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoginCheck.LoginSessionCheck()
        Dim obj As New BAlsidebar
        Try
            If Not Page.IsPostBack Then
                'Response.Write(Session("strPageId"))
                If GetPageId() <> "" Then
                    hdnSetTab.Value = "1"
                    drpFanPages.SelectedValue = GetPageId()
                End If
                Dim ds1 As New DataSet
                obj.Page = CStr(HttpContext.Current.Request.Url.AbsolutePath)
                ds1 = obj.GetVideoTutorial()
                If HttpContext.Current.Request.Url.AbsolutePath = ds1.Tables(0).Rows(0).Item("vt_Page").ToString Then
                    btnVideo.HRef = ds1.Tables(0).Rows(0).Item("vt_Video").ToString & "?fs=1&autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer"
                    btnVideo.Title = ds1.Tables(0).Rows(0).Item("vt_Title").ToString
                End If
                ViewState("CurrentSentAutoPostPageIndex") = 1
                ViewState("CurrentPageIndex") = 1
                ViewState("CurrentAutoPostPageIndex") = 1
                ViewState("CurrentPageIndexSent") = 1
                ViewState("CurrentPageIndexScheduled") = 1
                If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                    BindAllAutoPostFanPages()
                    BindAutoPostFanPages()
                    BindAllAutoPost()
                    BindAllSentAutoPost()
                    SelectFanPage()

                    objLib.FBUserId = Session("FacebookUserId")
                    strSelectionType = objLib.GetUSerSelectionType
                    BindLibraryCategories()
                Else
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
                End If
               
                'Else
                '    GenerateAutoPostControls()
                '    GenerateSentAutoPostControls()
            End If
          

        Catch ex As Exception
            If ex.Message.Contains("The remote server returned an error: (400)") Then
                Dim strUrl As String = "Login.aspx?i=" & Session("Industry") & "&c=" & Session("company") & "&u=2&at=" & Session("hdnToken") & ""
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, ";ShowHide11;", ";openNewWin('" & strUrl & "');", True)
            Else
                lblMessage.Text = "Error: " & ex.Message
            End If
            'plcData.Visible = False
            'plcNoData.Visible = False
            'plcError.Visible = True
        End Try
    End Sub

    Function PageIDsToConfigure() As String
        Return IIf(Session("PageIds") <> Nothing, Session("PageIds"), "")
    End Function

    Function GetPageId() As String
        Return IIf(Request("pid") <> Nothing AndAlso Request("pid") <> "", Request("pid"), "")
    End Function
    Function GetConfigurePageId() As String
        Return IIf(Request("confid") <> Nothing AndAlso Request("confid") <> "", Request("confid"), "")
    End Function

    Sub SelectFanPage()
        For Each item As DataListItem In dstAutoPostFanPages.Items
            Dim chkPage As HtmlInputCheckBox
            chkPage = CType(item.FindControl("autopostchkPage"), HtmlInputCheckBox)
            Dim arrPageId As String() = GetConfigurePageId().Split(",")
            If arrPageId.Contains(chkPage.Attributes("autopostpageid").ToString) Then
                chkPage.Checked = True
            End If
        Next
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), ";SelectedAutoPostPagesName;", ";SelectedAutoPostPagesName();", True)
    End Sub

    'Sub SelectConfiguredPage()
    '    For Each item As DataListItem In dstAutoPostFanPages.Items
    '        Dim chkPage As HtmlInputCheckBox
    '        chkPage = CType(item.FindControl("autopostchkPage"), HtmlInputCheckBox)
    '        Dim arrPageId As String() = PageIDsToConfigure().Split(",")
    '        If arrPageId.Contains(chkPage.Attributes("autopostpageid").ToString) Then
    '            chkPage.Checked = True
    '        End If
    '    Next
    '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), ";SelectedAutoPostPagesName;", ";SelectedAutoPostPagesName();", True)
    'End Sub

#Region "Library"
    Sub BindLibraryCategories()
        Dim ds As DataSet
        objLib.UserId = Session("SiteUserId")
        objLib.FBUserId = Session("FacebookUserId")
        objLib.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 
        objLib.IndustryId = GetSetCookies.GetCookie("IndustryId") ' 1 
        ds = objLib.GetLibraryCategories()

        rptAdminLibCat.DataSource = ds.Tables(0)
        rptAdminLibCat.DataBind()

        rptAdminLibCatTitle.DataSource = ds.Tables(0)
        rptAdminLibCatTitle.DataBind()

        rptUserLibCat.DataSource = ds.Tables(1)
        rptUserLibCat.DataBind()

        rptUserLibCatTitle.DataSource = ds.Tables(1)
        rptUserLibCatTitle.DataBind()

        'rptLibUserCatList.DataSource = ds.Tables(1)
        'rptLibUserCatList.DataBind()
    End Sub
    Function BindAdminLibraries(ByVal intCatID As Integer) As DataSet
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Dim ds As DataSet
            objLib.LibraryCategory = intCatID
            objLib.UserId = -1
            objLib.FBUserId = ""
            objLib.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 
            objLib.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 
            ds = objLib.GetLIbraries()
            Return ds
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Function
    Function BindUserLibraries(ByVal intCatID As Integer) As DataSet
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Dim ds As DataSet
            objLib.LibraryCategory = intCatID
            objLib.UserId = Session("SiteUserId")
            objLib.FBUserId = Session("FacebookUserId")
            objLib.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 
            objLib.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 
            ds = objLib.GetLIbraries()
            Return ds
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Function

    

    Sub DeleteMyLib(ByVal sender As Object, ByVal e As CommandEventArgs)
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            objLib.LibraryId = e.CommandArgument
            objLib.DeleteMyLibrary()
            BindLibraryCategories()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, ";ShowHide;", ";ShowUserLib(" & e.CommandName & ");SaveAlert('Library post deleted successfully');", True)

        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub

    Sub DeleteMyLibCat(ByVal sender As Object, ByVal e As CommandEventArgs)
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            objLib.LibraryCatId = e.CommandArgument
            objLib.DeleteMyLibraryCategory()
            BindLibraryCategories()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, ";ShowHide;", ";ShowUserLib(" & e.CommandName & ");SaveAlert('Library post deleted successfully');", True)
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub

#End Region
#Region "Bind Auto Post Fan Pages"
    Sub BindAllAutoPostFanPages()
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
            dtTable.Columns.Add(New DataColumn("link", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("picture", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("access_token", Type.GetType("System.String")))

            Dim dtRow As DataRow = dtTable.NewRow


            dtRow("name") = fUser1.name
            dtRow("id") = fUser1.id
            dtRow("link") = fUser1.link
            dtRow("picture") = fUser1.picture.data.url
            dtRow("access_token") = Session("FacebookAccessToken")

            dtTable.Rows.Add(dtRow)


            For Each item As FanPage.m_data In fPage1.data
                If Not item.access_token Is Nothing Then
                    If Not item.link Is Nothing Then
                        If Not item.picture Is Nothing Then
                            Dim dtRow1 As DataRow = dtTable.NewRow
                            dtRow1("name") = item.name.ToString
                            dtRow1("id") = item.id.ToString
                            dtRow1("picture") =  item.picture.data.url.ToString
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
                dstAutoPostFanPages.DataSource = dv
                dstAutoPostFanPages.DataBind()
                drpFanPages.DataSource = dv
                drpFanPages.DataValueField = "id"
                drpFanPages.DataTextField = "name"
                drpFanPages.DataBind()
                plcAutoData.Visible = True
                plcNoAutoData.Visible = False
            Else
                dstAutoPostFanPages.DataSource = Nothing
                dstAutoPostFanPages.DataBind()
                drpFanPages.DataSource = Nothing
                drpFanPages.DataBind()
                plcAutoData.Visible = False
                plcNoAutoData.Visible = True
            End If
        Catch ex As Exception
            ltrAutoPostError.Text = "Facebook is experiencing problems at this time. Please save your work and try to publish later."
        End Try

    End Sub
#End Region

#Region "Custom Paging AutoPost"

    Sub GenerateAutoPostControls()

        Dim objlink As LinkButton
        Dim objlabel As Label
        Dim intStartRecord, intEndRecord As Integer
        If (ViewState("TotalPageAutoPost") > 1) Then
            Dim i As Integer
            ' phPaging.Controls.Add(New LiteralControl("Pages:&nbsp;&nbsp;"))
            phPagingAutoPost.Controls.Add(New LiteralControl("Pages:&nbsp;&nbsp;"))
            If (ViewState("TotalPageAutoPost") > 1 And ViewState("CurrentAutoPostPageIndex") <> 1) Then
                ''''Generating previous button''
                objlink = New LinkButton()
                objlink.ID = "PrevAutoPost"
                objlink.Visible = True
                objlink.Text = "<img src=""../Content/images/previous-arrow.gif""  hspace=""5"" align=""absmiddle"" border=""0"">"
                AddHandler objlink.Click, AddressOf lnkPageAutoPost_Previous
                'phPaging.Controls.Add(objlink)
                'phPaging.Controls.Add(New LiteralControl("&nbsp;&nbsp;"))

                ''''''------------------''''''
                objlink = New LinkButton()
                objlink.ID = "Prev1AutoPost"
                objlink.Text = "<img src=""../Content/images/previous-arrow.gif""  hspace=""5"" align=""absmiddle"" border=""0"">"
                AddHandler objlink.Click, AddressOf lnkPageAutoPost_Previous
                phPagingAutoPost.Controls.Add(objlink)
                phPagingAutoPost.Controls.Add(New LiteralControl("&nbsp;&nbsp;"))
                ''''-----'''''''
            End If
            If (ViewState("CurrentAutoPostPageIndex") <= 7) Then
                intStartRecord = 1
                intEndRecord = 8
                If (ViewState("TotalPageAutoPost") > 8) Then
                    intEndRecord = 8
                Else
                    intEndRecord = ViewState("TotalPageAutoPost")
                End If
            Else
                If ((ViewState("CurrentAutoPostPageIndex") / 8).ToString.IndexOf(".") > 0) Then
                    intStartRecord = (ViewState("CurrentAutoPostPageIndex") / 8).ToString.Substring(0, (ViewState("CurrentAutoPostPageIndex") / 8).ToString.IndexOf(".")) * 8
                Else
                    intStartRecord = (8 * CInt((ViewState("CurrentAutoPostPageIndex") / 8)))
                End If
                If (intStartRecord > ViewState("TotalPageAutoPost")) Then
                    intStartRecord = (8 * (CInt((ViewState("CurrentAutoPostPageIndex") / 8)) - 1))
                End If
                If (intStartRecord + 8 < ViewState("TotalPageAutoPost")) Then
                    intEndRecord = intStartRecord + 8
                Else
                    intEndRecord = ViewState("TotalPageAutoPost")
                End If
            End If

            'For i = intStartRecord To intEndRecord
            '    If (ViewState("CurrentAutoPostPageIndex") <> i) Then
            '        objlink = New LinkButton()
            '        objlink.ID = i
            '        objlink.Text = i
            '        AddHandler objlink.Click, AddressOf lnkPageAutoPost_click
            '        'phPaging.Controls.Add(objlink)
            '        'phPaging.Controls.Add(New LiteralControl("&nbsp;&nbsp;|&nbsp;&nbsp;"))
            '        ''''''------------------''''''
            '        'objlink = New LinkButton()
            '        'objlink.ID = i + ViewState("TotalPageAutoPost")
            '        'objlink.Text = i
            '        'AddHandler objlink.Click, AddressOf lnkPageAutoPost_click
            '        phPagingAutoPost.Controls.Add(objlink)
            '        phPagingAutoPost.Controls.Add(New LiteralControl("&nbsp;&nbsp;|&nbsp;&nbsp;"))
            '    Else
            '        objlabel = New Label()
            '        objlabel.ID = i
            '        objlabel.Text = i
            '        'phPaging.Controls.Add(objlabel)
            '        'phPaging.Controls.Add(New LiteralControl("&nbsp;&nbsp;|&nbsp;&nbsp;"))

            '        ''''''------------------''''''
            '        'objlabel = New Label()
            '        'objlabel.ID = i + ViewState("TotalPageAutoPost")
            '        'objlabel.Text = i
            '        objlabel.CssClass = "curpage"
            '        phPagingAutoPost.Controls.Add(objlabel)
            '        phPagingAutoPost.Controls.Add(New LiteralControl("&nbsp;&nbsp;|&nbsp;&nbsp;"))
            '    End If
            'Next

            If (ViewState("TotalPageAutoPost") > 1 And ViewState("CurrentAutoPostPageIndex") < ViewState("TotalPageAutoPost")) Then
                ''''Generating next button '
                objlink = New LinkButton()
                objlink.ID = "NextAutoPost"
                objlink.Visible = True
                objlink.Text = "<img src=""../Content/images/Next-arrow.gif""  hspace=""5"" align=""absmiddle"" border=""0"">"
                AddHandler objlink.Click, AddressOf lnkPageAutoPost_Next
                'phPaging.Controls.Add(objlink)
                'phPaging.Controls.Add(New LiteralControl("&nbsp;&nbsp;"))
                ''''''------------------''''''
                objlink = New LinkButton()
                objlink.ID = "Next1AutoPost"
                objlink.Text = "<img src=""../Content/images/Next-arrow.gif""  hspace=""5"" align=""absmiddle"" border=""0"">"
                AddHandler objlink.Click, AddressOf lnkPageAutoPost_Next
                phPagingAutoPost.Controls.Add(objlink)
                phPagingAutoPost.Controls.Add(New LiteralControl("&nbsp;&nbsp;"))
                ''''-----'''''''

            End If
        End If
        showHideAutoPostPrevNext()
    End Sub
    Private Sub lnkPageAutoPost_click(ByVal sender As Object, ByVal e As System.EventArgs)
        If (CInt(CType(sender, LinkButton).ID) > ViewState("TotalRecordsAutoPost")) Then
            ViewState("CurrentAutoPostPageIndex") = CType(sender, LinkButton).ID - ViewState("TotalRecordsAutoPost")
        Else
            ViewState("CurrentAutoPostPageIndex") = CType(sender, LinkButton).ID
        End If
        BindAllAutoPost()

    End Sub
    Private Sub lnkPageAutoPost_Previous(ByVal sender As Object, ByVal e As System.EventArgs)
        If (ViewState("CurrentAutoPostPageIndex") <> 1) Then
            ViewState("CurrentAutoPostPageIndex") = ViewState("CurrentAutoPostPageIndex") - 1
        End If
        BindAllAutoPost()

    End Sub
    Private Sub lnkPageAutoPost_Next(ByVal sender As Object, ByVal e As System.EventArgs)
        If (ViewState("CurrentAutoPostPageIndex") < ViewState("TotalPageAutoPost")) Then
            ViewState("CurrentAutoPostPageIndex") = ViewState("CurrentAutoPostPageIndex") + 1
        Else
            ViewState("CurrentAutoPostPageIndex") = ViewState("CurrentAutoPostPageIndex")
        End If
        BindAllAutoPost()

    End Sub

    Sub showHideAutoPostPrevNext()
        If (ViewState("CurrentAutoPostPageIndex") = ViewState("TotalPageAutoPost")) Then
            If (Not IsNothing(form1.FindControl("NextAutoPost"))) Then
                form1.FindControl("NextAutoPost").Visible = False
                form1.FindControl("Next1AutoPost").Visible = False
            End If
        Else
            If (Not IsNothing(form1.FindControl("NextAutoPost"))) Then
                form1.FindControl("NextAutoPost").Visible = True
                form1.FindControl("Next1AutoPost").Visible = True
            End If
        End If
    End Sub
#End Region

#Region "Custom Paging Sent AutoPost"

    Sub GenerateSentAutoPostControls()

        Dim objlink As LinkButton
        Dim objlabel As Label
        Dim intStartRecord, intEndRecord As Integer
        If (ViewState("TotalPageSentAutoPost") > 1) Then
            Dim i As Integer
            ' phPaging.Controls.Add(New LiteralControl("Pages:&nbsp;&nbsp;"))
            phPagingSentAutoPost.Controls.Add(New LiteralControl("Pages:&nbsp;&nbsp;"))
            If (ViewState("TotalPageSentAutoPost") > 1 And ViewState("CurrentSentAutoPostPageIndex") <> 1) Then
                ''''Generating previous button''
                objlink = New LinkButton()
                objlink.ID = "PrevSentAutoPost"
                objlink.Visible = True
                objlink.Text = "<img src=""../Content/images/previous-arrow.gif""  hspace=""5"" align=""absmiddle"" border=""0"">"
                AddHandler objlink.Click, AddressOf lnkPageSentAutoPost_Previous
                'phPaging.Controls.Add(objlink)
                'phPaging.Controls.Add(New LiteralControl("&nbsp;&nbsp;"))

                ''''''------------------''''''
                objlink = New LinkButton()
                objlink.ID = "Prev1SentAutoPost"
                objlink.Text = "<img src=""../Content/images/previous-arrow.gif""  hspace=""5"" align=""absmiddle"" border=""0"">"
                AddHandler objlink.Click, AddressOf lnkPageSentAutoPost_Previous
                phPagingSentAutoPost.Controls.Add(objlink)
                phPagingSentAutoPost.Controls.Add(New LiteralControl("&nbsp;&nbsp;"))
                ''''-----'''''''
            End If
            If (ViewState("CurrentSentAutoPostPageIndex") <= 7) Then
                intStartRecord = 1
                intEndRecord = 8
                If (ViewState("TotalPageSentAutoPost") > 8) Then
                    intEndRecord = 8
                Else
                    intEndRecord = ViewState("TotalPageSentAutoPost")
                End If
            Else
                If ((ViewState("CurrentSentAutoPostPageIndex") / 8).ToString.IndexOf(".") > 0) Then
                    intStartRecord = (ViewState("CurrentSentAutoPostPageIndex") / 8).ToString.Substring(0, (ViewState("CurrentSentAutoPostPageIndex") / 8).ToString.IndexOf(".")) * 8
                Else
                    intStartRecord = (8 * CInt((ViewState("CurrentSentAutoPostPageIndex") / 8)))
                End If
                If (intStartRecord > ViewState("TotalPageSentAutoPost")) Then
                    intStartRecord = (8 * (CInt((ViewState("CurrentSentAutoPostPageIndex") / 8)) - 1))
                End If
                If (intStartRecord + 8 < ViewState("TotalPageSentAutoPost")) Then
                    intEndRecord = intStartRecord + 8
                Else
                    intEndRecord = ViewState("TotalPageSentAutoPost")
                End If
            End If

            'For i = intStartRecord To intEndRecord
            '    If (ViewState("CurrentSentAutoPostPageIndex") <> i) Then
            '        objlink = New LinkButton()
            '        objlink.ID = "sent" & i
            '        objlink.Text = i
            '        AddHandler objlink.Click, AddressOf lnkPageSentAutoPost_click
            '        'phPaging.Controls.Add(objlink)
            '        'phPaging.Controls.Add(New LiteralControl("&nbsp;&nbsp;|&nbsp;&nbsp;"))
            '        ''''''------------------''''''
            '        'objlink = New LinkButton()
            '        'objlink.ID = "sent" & i + ViewState("TotalPageSentAutoPost")
            '        'objlink.Text = i
            '        'AddHandler objlink.Click, AddressOf lnkPageSentAutoPost_click
            '        phPagingSentAutoPost.Controls.Add(objlink)
            '        phPagingSentAutoPost.Controls.Add(New LiteralControl("&nbsp;&nbsp;|&nbsp;&nbsp;"))
            '    Else
            '        objlabel = New Label()
            '        objlabel.ID = "sent" & i
            '        objlabel.Text = i
            '        'phPaging.Controls.Add(objlabel)
            '        'phPaging.Controls.Add(New LiteralControl("&nbsp;&nbsp;|&nbsp;&nbsp;"))

            '        ''''''------------------''''''
            '        'objlabel = New Label()
            '        'objlabel.ID = "sent" & i + ViewState("TotalPageSentAutoPost")
            '        'objlabel.Text = i
            '        objlabel.CssClass = "curpage"
            '        phPagingSentAutoPost.Controls.Add(objlabel)
            '        phPagingSentAutoPost.Controls.Add(New LiteralControl("&nbsp;&nbsp;|&nbsp;&nbsp;"))
            '    End If
            'Next

            If (ViewState("TotalPageSentAutoPost") > 1 And ViewState("CurrentSentAutoPostPageIndex") < ViewState("TotalPageSentAutoPost")) Then
                ''''Generating next button '
                objlink = New LinkButton()
                objlink.ID = "NextSentAutoPost"
                objlink.Visible = True
                objlink.Text = "<img src=""../Content/images/Next-arrow.gif""  hspace=""5"" align=""absmiddle"" border=""0"">"
                AddHandler objlink.Click, AddressOf lnkPageSentAutoPost_Next
                'phPaging.Controls.Add(objlink)
                'phPaging.Controls.Add(New LiteralControl("&nbsp;&nbsp;"))
                ''''''------------------''''''
                objlink = New LinkButton()
                objlink.ID = "Next1SentAutoPost"
                objlink.Text = "<img src=""../Content/images/Next-arrow.gif""  hspace=""5"" align=""absmiddle"" border=""0"">"
                AddHandler objlink.Click, AddressOf lnkPageSentAutoPost_Next
                phPagingSentAutoPost.Controls.Add(objlink)
                phPagingSentAutoPost.Controls.Add(New LiteralControl("&nbsp;&nbsp;"))
                ''''-----'''''''

            End If
        End If
        showHideSentAutoPostPrevNext()
    End Sub
    Private Sub lnkPageSentAutoPost_click(ByVal sender As Object, ByVal e As System.EventArgs)
        'If (CInt(CType(sender, LinkButton).ID.Replace("sent", "")) > ViewState("TotalRecordsSentAutoPost")) Then
        '    ViewState("CurrentSentAutoPostPageIndex") = CType(sender, LinkButton).ID.Replace("sent", "") - ViewState("TotalRecordsSentAutoPost")
        'Else
        '    ViewState("CurrentSentAutoPostPageIndex") = CType(sender, LinkButton).ID.Replace("sent", "")
        'End If
        If (CInt(CType(sender, LinkButton).ID) > ViewState("TotalRecordsSentAutoPost")) Then
            ViewState("CurrentSentAutoPostPageIndex") = CType(sender, LinkButton).ID - ViewState("TotalRecordsSentAutoPost")
        Else
            ViewState("CurrentSentAutoPostPageIndex") = CType(sender, LinkButton).ID
        End If
        BindAllSentAutoPost()

    End Sub
    Private Sub lnkPageSentAutoPost_Previous(ByVal sender As Object, ByVal e As System.EventArgs)
        If (ViewState("CurrentSentAutoPostPageIndex") <> 1) Then
            ViewState("CurrentSentAutoPostPageIndex") = ViewState("CurrentSentAutoPostPageIndex") - 1
        End If
        BindAllSentAutoPost()

    End Sub
    Private Sub lnkPageSentAutoPost_Next(ByVal sender As Object, ByVal e As System.EventArgs)
        If (ViewState("CurrentSentAutoPostPageIndex") < ViewState("TotalPageSentAutoPost")) Then
            ViewState("CurrentSentAutoPostPageIndex") = ViewState("CurrentSentAutoPostPageIndex") + 1
        Else
            ViewState("CurrentSentAutoPostPageIndex") = ViewState("CurrentSentAutoPostPageIndex")
        End If
        BindAllSentAutoPost()

    End Sub

    Sub showHideSentAutoPostPrevNext()
        If (ViewState("CurrentSentAutoPostPageIndex") = ViewState("TotalPageSentAutoPost")) Then
            If (Not IsNothing(form1.FindControl("NextSentAutoPost"))) Then
                form1.FindControl("NextSentAutoPost").Visible = False
                form1.FindControl("Next1SentAutoPost").Visible = False
            End If
        Else
            If (Not IsNothing(form1.FindControl("NextSentAutoPost"))) Then
                form1.FindControl("NextSentAutoPost").Visible = True
                form1.FindControl("Next1SentAutoPost").Visible = True
            End If
        End If
    End Sub
#End Region



#Region "Daily Auto Post Functions"
    Sub AddLibToAutoPost(ByVal sender As Object, ByVal e As CommandEventArgs)
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Dim objBAL As New BALSchedulePost
            Dim ds As DataSet
            objBAL.LibCatId = e.CommandArgument
            ds = objBAL.GetLibDetailsForAutoPost()

            Dim objGetPageDetails As New BALSchedulePost
            Dim dsPages As DataSet
            objGetPageDetails.FBPageId = If(Session("strPageId") <> Nothing, Session("strPageId"), "")
            objGetPageDetails.CompanyId = GetSetCookies.GetCookie("CompanyId")
            objGetPageDetails.IndustryId = GetSetCookies.GetCookie("IndustryId")
            dsPages = objGetPageDetails.GetAutoPostFanPagesDataforlibrary()

            If dsPages.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To dsPages.Tables(0).Rows.Count - 1
                    objBAL.TSMAUserId = Session("SiteUserId")
                    objBAL.FBUserId = Session("FacebookUserId")
                    objBAL.FBApplicationAccessToken = Session("FacebookAccessToken")
                    objBAL.FBPageId = dsPages.Tables(0).Rows(i).Item("PageId").ToString
                    objBAL.LibCatId = e.CommandArgument
                    objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 '
                    objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 '
                    objBAL.Message = ds.Tables(0).Rows(0).Item("lib_Template").ToString.Replace("<br>", Chr(10))
                    objBAL.Image = ds.Tables(0).Rows(0).Item("lib_Image").ToString
                    objBAL.Video = ds.Tables(0).Rows(0).Item("lib_Link").ToString
                    objBAL.VideoLink = ds.Tables(0).Rows(0).Item("lib_Video").ToString
                    objBAL.VideoId = ds.Tables(0).Rows(0).Item("lib_VideoId").ToString
                    objBAL.VideoImage = ds.Tables(0).Rows(0).Item("lib_VideoImage").ToString
                    objBAL.ScheduleDate = Convert.ToString(Date.Now)
                    objBAL.ScheduleHour = Date.Now.Hour 'If(selAutoPostActivationHour.Value <> "", CInt(selAutoPostActivationHour.Value), 0)
                    objBAL.ScheduleMinute = Date.Now.Minute 'If(selAutoPostActivationHour.Value <> "", CInt(selAutoPostActivationHour.Value), 0)
                    objBAL.ScheduleAMPM = Date.Now.ToString("tt") 'If(selAutoPostActivationHour.Value <> "", CInt(selAutoPostActivationHour.Value), 0)
                    objBAL.ScheduleTimeZone = If(ddlAutoPostTimeZone.SelectedValue <> "0", ddlAutoPostTimeZone.SelectedValue, "")
                    objBAL.AutoPostOnOff = 0 'If(hdnAutoPostOnOff.Value <> "", hdnAutoPostOnOff.Value, "0")
                    objBAL.AutoPostShuffle = 1
                    objBAL.AutoPostStatus = 1
                    objBAL.IsPosted = 0
                    objBAL.PostType = 1
                    objBAL.CreatedDate = Date.Now
                    objBAL.UpdatedDate = Date.Now
                    objBAL.AddAutoPostMaster()
                Next
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Message added in daily Autopost successfully ');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowAutoPostOnOff111", "ShowAutoPostOnOff('Please set <strong>Auto Post Data</strong> before Adding Library Data');", True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert123", "SaveAlert('Please Set Autopost before Adding Library Data');", True)
            End If
            BindAllAutoPost()

        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub

    Sub BindAllAutoPost()
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                ltrAutoPostError.Text = ""
                Dim ds As New DataSet
                Dim objBAL As New BALSchedulePost
                objBAL.PageIndex = CInt(ViewState("CurrentAutoPostPageIndex"))
                objBAL.FBUserId = Session("FacebookUserId")
                objBAL.TSMAUserId = Session("SiteUserId")
                objBAL.FBPageId = drpFanPages.SelectedValue 'Session("strPageId")
                objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 '
                objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 '
                ds = objBAL.GetAutoPostData()
                If ds.Tables(0).Rows.Count > 0 Then
                    ltrAutoPostRpt.Text = ""
                    dtlAutoPost.DataSource() = ds.Tables(0)
                    dtlAutoPost.DataBind()
                    pnlClearAll.Visible = True
                Else
                    dtlAutoPost.DataSource() = Nothing
                    dtlAutoPost.DataBind()
                    ltrAutoPostRpt.Text = "No Auto Post Found"
                    'pnlClearAll.Visible = False
                End If
                If ds.Tables(1).Rows.Count > 0 Then
                    ViewState("TotalPageAutoPost") = ds.Tables(1).Rows(0).Item("TotalPage").ToString
                    ViewState("TotalRecordsAutoPost") = ds.Tables(1).Rows(0).Item("TotalRec").ToString
                End If

                'If ds.Tables(0).Rows.Count > 0 Then
                '    ' Dim strimgsrc = ConfigurationManager.AppSettings("AppPath") & ds.Tables(0).Rows(0).Item("ImageOnOff").ToString
                '    ' imgOnOff.Src = ""
                '    ' ltrOnOffText.Text = ""
                '    ltrAutoPostTotal.Text = " (" & ds.Tables(1).Rows(0).Item("TotalRec").ToString & ")"
                'Else

                '    ltrAutoPostTotal.Text = " (0)"
                '    ' imgOnOff.Src = ConfigurationManager.AppSettings("AppPath") & "Content/images/Off_bullate.png"
                '    'ltrOnOffText.Text = "Off"
                'End If
                'phPagingAutoPost.Controls.Clear()
                'GenerateAutoPostControls()
            Else
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
            End If

        Catch ex As Exception
            ltrAutoPostError.Text = "Error :" & ex.Message()
        End Try
    End Sub

    Sub BindAllSentAutoPost()
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                ltrSentAutoPostRpt.Text = ""
                Dim ds As New DataSet
                Dim objBAL As New BALSchedulePost
                objBAL.PageIndex = CInt(ViewState("CurrentSentAutoPostPageIndex"))
                objBAL.FBUserId = Session("FacebookUserId")
                objBAL.TSMAUserId = Session("SiteUserId")
                objBAL.FBPageId = drpFanPages.SelectedValue
                objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 '
                objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 '
                ds = objBAL.GetSentAutoPostData()
                If ds.Tables(0).Rows.Count > 0 Then
                    ltrSentAutoPostRpt.Text = ""
                    dtlSentAutoPost.DataSource() = ds.Tables(0)
                    dtlSentAutoPost.DataBind()
                Else
                    dtlSentAutoPost.DataSource() = Nothing
                    dtlSentAutoPost.DataBind()
                    ltrSentAutoPostRpt.Text = "No Sent Auto Post Found"
                End If
                If ds.Tables(1).Rows.Count > 0 Then
                    ViewState("TotalPageSentAutoPost") = ds.Tables(1).Rows(0).Item("TotalPage").ToString
                    ViewState("TotalRecordsSentAutoPost") = ds.Tables(1).Rows(0).Item("TotalRec").ToString
                End If
                ' phPagingSentAutoPost.Controls.Clear()
                'GenerateSentAutoPostControls()
            Else
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
            End If

        Catch ex As Exception
            ltrAutoPostError.Text = "Error :" & ex.Message()
        End Try
    End Sub

    Private Sub lnkAutoPost_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAutoPost.ServerClick
        Dim objBAL As New BALSchedulePost
        'Dim dtAutoPostDate As DateTime
        'Dim strDate1 As DateTime
        'Dim strShortDate As String
        'Dim strActivationHours As String
        'Dim strActivationMinutes As String
        'Dim strAMPM As String
        'Dim strDate As String = ""
        'dtAutoPostDate = Date.Now.ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()

        'If ddlAutoPostTimeZone.SelectedIndex > 0 Then
        '    If ddlAutoPostTimeZone.SelectedValue <> ConfigurationManager.AppSettings("ServerTimeZone") Then
        '        strDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dtAutoPostDate, ddlAutoPostTimeZone.SelectedValue, ConfigurationManager.AppSettings("ServerTimeZone")).ToString()
        '    Else
        '        strDate = dtAutoPostDate
        '    End If
        'End If

        'If strDate < Date.Now Then
        '    strDate = Date.Now.AddDays(1).ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()
        'Else
        '    strDate = Date.Now.ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()
        'End If


        'If ddlAutoPostTimeZone.SelectedIndex > 0 Then
        '    If ddlAutoPostTimeZone.SelectedValue <> ConfigurationManager.AppSettings("ServerTimeZone") Then
        '        strDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(strDate, ddlAutoPostTimeZone.SelectedValue, ConfigurationManager.AppSettings("ServerTimeZone")).ToString()
        '    Else
        '        strDate = dtAutoPostDate
        '    End If
        'End If

        'strDate1 = If((strDate <> ""), Convert.ToDateTime(strDate), Date.Now)
        Dim dtAutoPostDate As DateTime
        Dim strDate1 As DateTime
        Dim strConvertToOriginalDate As DateTime
        Dim strDateWithTimezoneSelected As DateTime
        Dim strShortDate As String
        Dim strActivationHours As String
        Dim strActivationMinutes As String
        Dim strAMPM As String
        Dim strpost7day As String = ""
        Dim strpost5day As String = ""
        Dim strpost1day As String = ""

        'If (rd7day.Checked = True) Then
        '    strpost7day = 1
        'Else
        '    strpost7day = 0
        'End If
        'If (rd5day.Checked = True) Then
        '    strpost5day = 1
        'Else
        '    strpost5day = 0
        'End If
        'If (autopostday1.Checked = True) Then
        '    strpost1day = String.Empty
        '    strpost1day = strpost1day + "1"
        'Else
        '    strpost1day = strpost1day + "0"
        'End If
        'If (autopostday2.Checked = True) Then
        '    strpost1day = strpost1day + "1"
        'Else
        '    strpost1day = strpost1day + "0"
        'End If
        'If (autopostday3.Checked = True) Then
        '    strpost1day = strpost1day + "1"
        'Else
        '    strpost1day = strpost1day + "0"
        'End If
        'If (autopostday4.Checked = True) Then
        '    strpost1day = strpost1day + "1"
        'Else
        '    strpost1day = strpost1day + "0"
        'End If
        'If (autopostday5.Checked = True) Then
        '    strpost1day = strpost1day + "1"
        'Else
        '    strpost1day = strpost1day + "0"
        'End If
        'If (autopostday6.Checked = True) Then
        '    strpost1day = strpost1day + "1"
        'Else
        '    strpost1day = strpost1day + "0"
        'End If
        'If (autopostday7.Checked = True) Then
        '    strpost1day = strpost1day + "1"
        'Else
        '    strpost1day = strpost1day + "0"
        'End If
        Dim strDate As DateTime
        dtAutoPostDate = Date.Now.ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString() Old Code for getdatetime
        'dtAutoPostDate = Date.Now.ToString("MM/dd/yyyy")

        'Response.Write("Original Date : " & dtAutoPostDate & "<br/>")

        If ddlAutoPostTimeZone.SelectedIndex > 0 Then
            If ddlAutoPostTimeZone.SelectedValue <> ConfigurationManager.AppSettings("ServerTimeZone") Then
                strDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dtAutoPostDate, ddlAutoPostTimeZone.SelectedValue, ConfigurationManager.AppSettings("ServerTimeZone")).ToString()
            Else
                strDate = dtAutoPostDate
            End If
        End If

        'If strDate < Date.Now Then
        '    strDate = Date.Now.AddDays(1).ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()
        'Else
        '    strDate = Date.Now.ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()
        'End If

        If strDate.Date = Date.Now.Date Then
            Dim startTime As DateTime = DateTime.Now
            Dim endTime As DateTime = strDate
            Dim span As TimeSpan = endTime.Subtract(startTime)
            If span.Minutes <= 5 Then
                strDate = Date.Now.AddDays(1).ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()
            Else
                strDate = dtAutoPostDate
            End If
        Else
            If Convert.ToDateTime(strDate) < Date.Now Then
                strDate = Date.Now.AddDays(1).ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()
            Else
                strDate = dtAutoPostDate
            End If
        End If
        'strDateWithTimezoneSelected = Convert.ToDateTime(strDate).ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value


        'Response.Write("Date and time After Converted   : " & strDateWithTimezoneSelected & "<br/>")

        If ddlAutoPostTimeZone.SelectedIndex > 0 Then
            If ddlAutoPostTimeZone.SelectedValue <> ConfigurationManager.AppSettings("ServerTimeZone") Then
                strConvertToOriginalDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(strDate, ddlAutoPostTimeZone.SelectedValue, ConfigurationManager.AppSettings("ServerTimeZone")).ToString()
            Else
                strConvertToOriginalDate = dtAutoPostDate
            End If
        End If

        'dtAutoPostDate = Date.Now.ToString("MM/dd/yyyy")
        '
        '        'Response.Write("Original Date : " & dtAutoPostDate)
        '
        '        If ddlAutoPostTimeZone.SelectedIndex > 0 Then
        '            If ddlAutoPostTimeZone.SelectedValue <> ConfigurationManager.AppSettings("ServerTimeZone") Then
        '                'strDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dtAutoPostDate, ddlAutoPostTimeZone.SelectedValue, ConfigurationManager.AppSettings("ServerTimeZone")).ToString()
        '                strDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dtAutoPostDate, ConfigurationManager.AppSettings("ServerTimeZone"), ddlAutoPostTimeZone.SelectedValue).ToString()
        '            Else
        '                strDate = dtAutoPostDate
        '            End If
        '        End If
        '
        '        'Response.Write("Date After Converted   : " & Convert.ToDateTime(strDate).ToString("MM/dd/yyyy"))
        '
        '        strDateWithTimezoneSelected = Convert.ToDateTime(strDate).ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value
        '
        '
        '        'Response.Write("Date and time After Converted   : " & strDateWithTimezoneSelected)
        '
        '        If ddlAutoPostTimeZone.SelectedIndex > 0 Then
        '            If ddlAutoPostTimeZone.SelectedValue <> ConfigurationManager.AppSettings("ServerTimeZone") Then
        '                strConvertToOriginalDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(strDateWithTimezoneSelected, ddlAutoPostTimeZone.SelectedValue, ConfigurationManager.AppSettings("ServerTimeZone"))
        '            Else
        '                strConvertToOriginalDate = dtAutoPostDate
        '            End If
        '        End If
        '
        '
        '        If strConvertToOriginalDate < Date.Now Then
        '            strConvertToOriginalDate = strConvertToOriginalDate.AddDays(1) 'Date.Now.AddDays(1).ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()
        '        Else
        '            strConvertToOriginalDate = strConvertToOriginalDate 'Date.Now.ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()
        '        End If



        'Response.Write("Set AutoPost   : " & strConvertToOriginalDate)

        'Response.End()
        strDate1 = strConvertToOriginalDate

        strShortDate = strDate1.Date
        strActivationHours = Integer.Parse(strDate1.ToString("hh")).ToString()
        strActivationMinutes = strDate1.Minute.ToString
        strAMPM = strDate1.ToString("tt")


        'objBAL.TSMAUserId = Session("SiteUserId")
        'objBAL.FBUserId = Session("FacebookUserId")
        'objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 '
        'objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 '
        'objBAL.ScheduleDate = strShortDate
        'objBAL.ScheduleHour = If(strActivationHours <> "", CInt(strActivationHours), 0)
        'objBAL.ScheduleMinute = If(strActivationMinutes <> "", CInt(strActivationMinutes), 0)
        'objBAL.ScheduleAMPM = If(strAMPM <> "", CStr(strAMPM), "0")
        'objBAL.ScheduleTimeZone = If(ddlAutoPostTimeZone.SelectedValue <> "0", ddlAutoPostTimeZone.SelectedValue, "")
        'objBAL.AutoPostOnOff = 0
        'objBAL.AddAutoPostScheduleData()

        'Dim objBALDel As New BALSchedulePost
        'objBALDel.FBUserId = Session("FacebookUserId")
        'objBALDel.TSMAUserId = Session("SiteUserId")
        'objBALDel.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 '
        'objBALDel.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 '
        'objBALDel.DeleteAutoPostFanPages()
        'For Each AutoPostitem As DataListItem In dstAutoPostFanPages.Items
        '    Dim AutoPostCheckBox As HtmlInputCheckBox
        '    AutoPostCheckBox = CType(AutoPostitem.FindControl("autopostchkPage"), HtmlInputCheckBox)

        '    If AutoPostCheckBox.Checked = True Then
        '        objBAL.TSMAUserId = Session("SiteUserId")
        '        objBAL.FBUserId = Session("FacebookUserId")
        '        objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 '
        '        objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 '
        '        objBAL.FBPageId = CType(AutoPostitem.FindControl("hdnAutoPostPageId"), HtmlInputHidden).Value
        '        objBAL.FBPageName = CType(AutoPostitem.FindControl("hdnAutoPostPageName"), HtmlInputHidden).Value
        '        objBAL.FBPageImage = CType(AutoPostitem.FindControl("hdnAutoPostImage"), HtmlInputHidden).Value
        '        objBAL.FBPageAccessToken = CType(AutoPostitem.FindControl("hdnAutoPostAccessToken"), HtmlInputHidden).Value
        '        objBAL.AddAutoPostPages()
        '    End If
        'Next

        For Each AutoPostitem As DataListItem In dstAutoPostFanPages.Items
            Dim AutoPostCheckBox As HtmlInputCheckBox
            AutoPostCheckBox = CType(AutoPostitem.FindControl("autopostchkPage"), HtmlInputCheckBox)
            If AutoPostCheckBox.Checked = True Then

                objBAL.TSMAUserId = Session("SiteUserId")
                objBAL.FBUserId = Session("FacebookUserId")
                objBAL.FBPageId = CType(AutoPostitem.FindControl("hdnAutoPostPageId"), HtmlInputHidden).Value
                objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId")
                objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId")
                objBAL.ScheduleDate = strShortDate
                objBAL.ScheduleHour = If(strActivationHours <> "", CInt(strActivationHours), 0)
                objBAL.ScheduleMinute = If(strActivationMinutes <> "", CInt(strActivationMinutes), 0)
                objBAL.ScheduleAMPM = If(strAMPM <> "", CStr(strAMPM), "0")
                objBAL.ScheduleTimeZone = If(ddlAutoPostTimeZone.SelectedValue <> "0", ddlAutoPostTimeZone.SelectedValue, "")
                'objBAL.AutoPostOnOff = 0 old code in which by default it is off
                objBAL.AutoPostOnOff = 1 'new code in which by default it is on
                'objBAL.post7DayWeek = strpost7day
                'objBAL.post5DayWeek = strpost5day
                'objBAL.post1DayWeek = strpost1day
                objBAL.AddAutoPostScheduleData()

                objBAL.TSMAUserId = Session("SiteUserId")
                objBAL.FBUserId = Session("FacebookUserId")
                objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId")
                objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId")
                objBAL.FBPageId = CType(AutoPostitem.FindControl("hdnAutoPostPageId"), HtmlInputHidden).Value
                objBAL.FBPageName = CType(AutoPostitem.FindControl("hdnAutoPostPageName"), HtmlInputHidden).Value
                objBAL.FBPageImage = CType(AutoPostitem.FindControl("hdnAutoPostImage"), HtmlInputHidden).Value
                objBAL.FBPageAccessToken = CType(AutoPostitem.FindControl("hdnAutoPostAccessToken"), HtmlInputHidden).Value

                objBAL.AddAutoPostPages()

                Dim dsShuffle As New DataSet
                objBAL.FBUserId = Session("FacebookUserId")
                objBAL.TSMAUserId = Session("SiteUserId")
                objBAL.FBPageId = CType(AutoPostitem.FindControl("hdnAutoPostPageId"), HtmlInputHidden).Value
                objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId")
                objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId")
                'objBAL.AutoPostOnOff = 0
                dsShuffle = objBAL.ShuffleAutoPostData()

            End If
        Next

        BindAllAutoPost()
        'BindAutoPostFanPages()
        'BindAutoPostSchedule()
        BindAllSentAutoPost()
        ClearAutoPostData()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Autopost scheduled successfully');", True)
        'Else
        'ltrAutoPostError.Text = "Current date/time is not valid"

    End Sub

    Sub BindAutoPostFanPages()
        Try
            ltrAutoPostError.Text = ""
            Dim ds As New DataSet
            Dim objBAL As New BALSchedulePost
            objBAL.PageIndex = CInt(ViewState("CurrentAutoPostPageIndex"))
            objBAL.FBUserId = If(Session("FacebookUserId") <> Nothing, Session("FacebookUserId"), "")
            objBAL.TSMAUserId = Session("SiteUserId")
            objBAL.FBPageId = If(Session("strPageId") <> Nothing, Session("strPageId"), "")
            objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 '
            objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 '
            ds = objBAL.GetAutoPostFanPages()
            If ds.Tables(0).Rows.Count > 0 Then
                ltrAutoPostPage.Text = ""
                rptAutoPostfanpages.DataSource() = ds.Tables(0)
                rptAutoPostfanpages.DataBind()
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    For Each item As DataListItem In dstAutoPostFanPages.Items
                        Dim myCheckBox As HtmlInputCheckBox
                        Dim HiddenID As HtmlInputHidden
                        myCheckBox = CType(item.FindControl("autopostchkPage"), HtmlInputCheckBox)
                        HiddenID = CType(item.FindControl("hdnAutoPostPageId"), HtmlInputHidden)
                        Dim str As String = CStr(HiddenID.Value)
                        If str = ds.Tables(0).Rows(i).Item("ap_FBPageId").ToString Then
                            'myCheckBox.Checked = True
                            Session("PageIds") = Session("PageIds") & ds.Tables(0).Rows(i).Item("ap_FBPageId").ToString & ","
                        End If

                    Next
                Next

            Else
                rptAutoPostfanpages.DataSource() = Nothing
                rptAutoPostfanpages.DataBind()
                ltrAutoPostPage.Text = "No Business Pages Found"
            End If
        Catch ex As Exception
            ltrAutoPostError.Text = "Error :" & ex.Message()
            'ltrAutoPostError.Text = "Facebook is experiencing problems at this time. Please save your work and try to publish later."
        End Try
    End Sub

    Sub BindAutoPostSchedule()
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                ltrAutoPostError.Text = ""
                Dim ds As DataSet
                Dim objBAL As New BALSchedulePost
                objBAL.FBUserId = Session("FacebookUserId")
                objBAL.TSMAUserId = Session("SiteUserId")
                objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 ' 
                objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 '
                ds = objBAL.GetAutoPostSchedule()
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("apsm_IsAutoPostSet") <> 0 Then
                        pnlAutoPostSet.Visible = False
                        pnlAutoPostUpdate.Visible = True
                        Dim strDate As DateTime
                        Dim strDateOriginal As DateTime = ds.Tables(0).Rows(0).Item("apsm_ScheduleDate") & " " & ds.Tables(0).Rows(0).Item("apsm_ScheduleHour") & ":" & ds.Tables(0).Rows(0).Item("apsm_ScheduleMinute") & " " & ds.Tables(0).Rows(0).Item("apsm_ScheduleAMPM")
                        If ds.Tables(0).Rows(0).Item("apsm_ScheduleTimeZone") <> ConfigurationManager.AppSettings("ServerTimeZone") Then
                            Dim strConvertedDate As DateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(strDateOriginal, ConfigurationManager.AppSettings("ServerTimeZone"), ds.Tables(0).Rows(0).Item("apsm_ScheduleTimeZone")).ToString()
                            strDate = String.Format("{0:dd MMM yyyy hh:mm tt}", strConvertedDate)
                        Else
                            strDate = String.Format("{0:dd MMM yyyy hh:mm tt}", strDateOriginal)
                        End If
                        selAutoPostActivationHour.Value = Integer.Parse(strDate.ToString("hh")).ToString() 'ds.Tables(0).Rows(0).Item("sm_ScheduleHour")
                        selAutoPostActivationMinute.Value = strDate.Minute 'ds.Tables(0).Rows(0).Item("sm_ScheduleMinute")
                        selAutoPostAMPM.Value = strDate.ToString("tt") 'ds.Tables(0).Rows(0).Item("sm_ScheduleAMPM")
                        ddlAutoPostTimeZone.SelectedValue = ds.Tables(0).Rows(0).Item("apsm_ScheduleTimeZone")
                    Else
                        pnlAutoPostSet.Visible = True
                        pnlAutoPostUpdate.Visible = False
                    End If
                    'If ds.Tables(0).Rows(0).Item("apsm_OnOff") = 0 Then
                    '    pnlAutoPostOn.Visible = True
                    '    pnlAutoPostOff.Visible = False
                    '    pnlOn.Visible = False
                    '    pnlOff.Visible = True
                    'Else
                    '    pnlAutoPostOff.Visible = True
                    '    pnlAutoPostOn.Visible = False
                    '    pnlOn.Visible = True
                    '    pnlOff.Visible = False
                    'End If
                End If
            Else
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
            End If
        Catch ex As Exception
            ltrAutoPostError.Text = "Error :" & ex.Message()
        End Try
    End Sub

    Private Sub lnkAutoPostOnOff_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAutoPostOnOff.ServerClick
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Try
                Dim ds As DataSet
                Dim objBAL As New BALSchedulePost
                objBAL.FBUserId = Session("FacebookUserId")
                objBAL.TSMAUserId = Session("SiteUserId")
                objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId")
                objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId")
                ds = objBAL.GetAutoPostSchedule()
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("apsm_IsAutoPostSet") = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowAutoPostOnOff", "ShowAutoPostOnOff('Please set <strong>Auto Post Schedule Time</strong> before <strong>Turn on Auto Post</strong>');", True)
                        Exit Sub
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowAutoPostOnOff", "ShowAutoPostOnOff('Please set <strong>Auto Post Schedule Time</strong> before <strong>Turn on Auto Post</strong>');", True)
                    Exit Sub
                End If


                'Dim dtAutoPostDate As DateTime
                'Dim strDate1 As DateTime
                'Dim strShortDate As String
                'Dim strActivationHours As String
                'Dim strActivationMinutes As String
                'Dim strAMPM As String
                'Dim strDate As String = ""
                'dtAutoPostDate = Date.Now.ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()

                'If ddlAutoPostTimeZone.SelectedIndex > 0 Then
                '    If ddlAutoPostTimeZone.SelectedValue <> ConfigurationManager.AppSettings("ServerTimeZone") Then
                '        strDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dtAutoPostDate, ddlAutoPostTimeZone.SelectedValue, ConfigurationManager.AppSettings("ServerTimeZone")).ToString()
                '    Else
                '        strDate = dtAutoPostDate
                '    End If
                'End If

                'If strDate < Date.Now Then
                '    strDate = Date.Now.AddDays(1).ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()
                'Else
                '    strDate = Date.Now.ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()
                'End If

                'If ddlAutoPostTimeZone.SelectedIndex > 0 Then
                '    If ddlAutoPostTimeZone.SelectedValue <> ConfigurationManager.AppSettings("ServerTimeZone") Then
                '        strDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(strDate, ddlAutoPostTimeZone.SelectedValue, ConfigurationManager.AppSettings("ServerTimeZone")).ToString()
                '    Else
                '        strDate = dtAutoPostDate
                '    End If
                'End If

                'strDate1 = If((strDate <> ""), Convert.ToDateTime(strDate), Date.Now)
                Dim dtAutoPostDate As DateTime
                Dim strDate1 As DateTime
                Dim strConvertToOriginalDate As DateTime
                Dim strDateWithTimezoneSelected As DateTime
                Dim strShortDate As String
                Dim strActivationHours As String
                Dim strActivationMinutes As String
                Dim strAMPM As String
                Dim strDate As String = ""
                dtAutoPostDate = Date.Now.ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString() Old Code for getdatetime
                'dtAutoPostDate = Date.Now.ToString("MM/dd/yyyy")

                'Response.Write("Original Date : " & dtAutoPostDate & "<br/>")

                If ddlAutoPostTimeZone.SelectedIndex > 0 Then
                    If ddlAutoPostTimeZone.SelectedValue <> ConfigurationManager.AppSettings("ServerTimeZone") Then
                        strDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dtAutoPostDate, ddlAutoPostTimeZone.SelectedValue, ConfigurationManager.AppSettings("ServerTimeZone")).ToString()
                    Else
                        strDate = dtAutoPostDate
                    End If
                End If

                If strDate < Date.Now Then
                    strDate = Date.Now.AddDays(1).ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()
                Else
                    strDate = Date.Now.ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value  'Date.Now.ToString()
                End If

                'strDateWithTimezoneSelected = Convert.ToDateTime(strDate).ToString("MM/dd/yyyy") & " " & selAutoPostActivationHour.Value & ":" & selAutoPostActivationMinute.Value & ":00 " & selAutoPostAMPM.Value


                'Response.Write("Date and time After Converted   : " & strDateWithTimezoneSelected & "<br/>")

                If ddlAutoPostTimeZone.SelectedIndex > 0 Then
                    If ddlAutoPostTimeZone.SelectedValue <> ConfigurationManager.AppSettings("ServerTimeZone") Then
                        strConvertToOriginalDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(strDate, ddlAutoPostTimeZone.SelectedValue, ConfigurationManager.AppSettings("ServerTimeZone")).ToString()
                    Else
                        strConvertToOriginalDate = dtAutoPostDate
                    End If
                End If

                strDate1 = strConvertToOriginalDate
                strShortDate = strDate1.Date
                strActivationHours = Integer.Parse(strDate1.ToString("hh")).ToString()
                strActivationMinutes = strDate1.Minute.ToString
                strAMPM = strDate1.ToString("tt")

                objBAL.TSMAUserId = Session("SiteUserId")
                objBAL.FBUserId = Session("FacebookUserId")
                objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId")
                objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId")
                objBAL.ScheduleDate = strShortDate
                objBAL.ScheduleHour = If(strActivationHours <> "", CInt(strActivationHours), 0)
                objBAL.ScheduleMinute = If(strActivationMinutes <> "", CInt(strActivationMinutes), 0)
                objBAL.ScheduleAMPM = If(strAMPM <> "", CStr(strAMPM), "0")
                objBAL.ScheduleTimeZone = If(ddlAutoPostTimeZone.SelectedValue <> "0", ddlAutoPostTimeZone.SelectedValue, "")
                objBAL.AutoPostOnOff = 1
                objBAL.AddAutoPostScheduleData()
                'ltrAutoPostError.Text = ""
                BindAllAutoPost()
                BindAutoPostFanPages()
                BindAutoPostSchedule()
                BindAllSentAutoPost()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Daily Autopost turned on successfully ');", True)
                'pnlAutoPostOn.Visible = False
                'pnlAutoPostOff.Visible = True

                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Daily Autopost status changed successfully');", True)
            Catch ex As Exception
                ltrAutoPostError.Text = "Error :" & ex.Message()
            End Try
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub
    Private Sub lnkAutoPostShuffle_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAutoPostShuffle.ServerClick
        ltrAutoPostError.Text = ""
        Dim objBAL As New BALSchedulePost
        Dim dsShuffle As New DataSet
        objBAL.FBUserId = Session("FacebookUserId")
        objBAL.TSMAUserId = Session("SiteUserId")
        objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 ' 
        objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 '
        dsShuffle = objBAL.ShuffleAutoPostData()

        If dsShuffle.Tables(0).Rows(0).Item("Added") = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('All available messages in your company library have already been posted');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('All library posts imported successfully');", True)
        End If
        BindAllAutoPost()
        BindAllSentAutoPost()
        BindAutoPostSchedule()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('data is shuffled successfully in Daily Autopost');", True)
    End Sub
    Sub DeleteMyAutoPostoLD(ByVal sender As Object, ByVal e As CommandEventArgs)
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Dim objBAlDel As New BALSchedulePost
            objBAlDel.ScheduleId = e.CommandArgument
            objBAlDel.DeleteMyAutoPost()
            BindAllAutoPost()
            BindAutoPostSchedule()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, ";ShowHide;", ";ShowUserLib(" & e.CommandName & ");", True)
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub
    Sub DeleteMyAutoPost(ByVal sender As Object, ByVal e As CommandEventArgs)
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            'Response.Write(Session("SiteUserId"))

            Dim objBAlDel As New BALSchedulePost
            objBAlDel.ScheduleId = e.CommandArgument
            objBAlDel.TSMAUserId = CInt(Session("SiteUserId"))
            objBAlDel.FBUserId = Session("FacebookUserId")
            objBAlDel.FBPageId = e.CommandName
            objBAlDel.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 ' 
            objBAlDel.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 '
            objBAlDel.DeleteMyAutoPostNew()
            BindAllAutoPost()
            BindAutoPostSchedule()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, ";ShowHide;", ";ShowUserLib(" & e.CommandName & ");", True)
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub
    Private Sub dtlAutoPost_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dtlAutoPost.ItemCommand
        If e.CommandName = "Up" Or e.CommandName = "Down" Then
            Try
                Dim objBAl As New BALSchedulePost
                objBAl.AutoPostId = e.CommandArgument
                objBAl.strAutoPostOrder = e.CommandName
                objBAl.UpdateAutoPostOrder()
                ltrAutoPostError.Text = "Order Changed Succefully"
                BindAllAutoPost()
                BindAutoPostSchedule()
            Catch ex As Exception
                ltrAutoPostError.Text = "Error! " & ex.Message
            End Try
        End If
    End Sub

    Private Sub dtlAutoPost_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dtlAutoPost.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If CType(e.Item.DataItem, DataRowView)("apm_Image") <> "" Then
                CType(e.Item.FindControl("divAutoPostImage"), HtmlGenericControl).Style.Add("display", "")
                CType(e.Item.FindControl("imgAutoPostPhoto"), HtmlImage).Src = ConfigurationManager.AppSettings("AppPath") & "resize-tabs.ashx?P=/Content/uploads/images/" & CType(e.Item.DataItem, DataRowView)("apm_Image") & "&D=150x100"
                'CType(e.Item.FindControl("imgScheduleLargePhoto"), HtmlImage).Src = ConfigurationManager.AppSettings("AppPath") & "Content/uploads/images/" & CType(e.Item.DataItem, DataRowView)("sm_Image")
            Else
                CType(e.Item.FindControl("divAutoPostImage"), HtmlGenericControl).Style.Add("display", "none")
            End If

            Dim strDateOriginal As DateTime = CType(e.Item.DataItem, DataRowView)("apm_ScheduleDate") & " " & CType(e.Item.DataItem, DataRowView)("apm_ScheduleHour") & ":" & CType(e.Item.DataItem, DataRowView)("apm_ScheduleMinute") & " " & CType(e.Item.DataItem, DataRowView)("apm_ScheduleAMPM")
            If CType(e.Item.DataItem, DataRowView)("apm_ScheduleTimeZone") <> "" Then
                If CType(e.Item.DataItem, DataRowView)("apm_ScheduleTimeZone") <> ConfigurationManager.AppSettings("ServerTimeZone") Then
                    Dim strConvertedDate As DateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(strDateOriginal, ConfigurationManager.AppSettings("ServerTimeZone"), CType(e.Item.DataItem, DataRowView)("apm_ScheduleTimeZone")).ToString()
                    'Dim strConvertedDate As DateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(strDateOriginal, ConfigurationManager.AppSettings("ServerTimeZone"), CType(e.Item.DataItem, DataRowView)("sm_ScheduleTimeZone")).ToString()
                    CType(e.Item.FindControl("lblAutoPostDate"), HtmlGenericControl).InnerText = String.Format("{0:dd MMM yyyy hh:mm tt}", strConvertedDate)
                Else
                    CType(e.Item.FindControl("lblAutoPostDate"), HtmlGenericControl).InnerText = String.Format("{0:dd MMM yyyy hh:mm tt}", strDateOriginal)
                End If
            End If
        End If
    End Sub
#End Region



#Region "Sent Auto Post Item Bound"
    Private Sub dtlSentAutoPost_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dtlSentAutoPost.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If CType(e.Item.DataItem, DataRowView)("apm_Image") <> "" Then
                CType(e.Item.FindControl("divSentAutoPostImage"), HtmlGenericControl).Style.Add("display", "")
                CType(e.Item.FindControl("imgSentAutoPostPhoto"), HtmlImage).Src = ConfigurationManager.AppSettings("AppPath") & "resize-tabs.ashx?P=/Content/uploads/images/" & CType(e.Item.DataItem, DataRowView)("apm_Image") & "&D=150x100"
                'CType(e.Item.FindControl("imgScheduleLargePhoto"), HtmlImage).Src = ConfigurationManager.AppSettings("AppPath") & "Content/uploads/images/" & CType(e.Item.DataItem, DataRowView)("sm_Image")
            Else
                CType(e.Item.FindControl("divSentAutoPostImage"), HtmlGenericControl).Style.Add("display", "none")
            End If

            Dim strDateOriginal As DateTime = CType(e.Item.DataItem, DataRowView)("apm_ScheduleDate") & " " & CType(e.Item.DataItem, DataRowView)("apm_ScheduleHour") & ":" & CType(e.Item.DataItem, DataRowView)("apm_ScheduleMinute") & " " & CType(e.Item.DataItem, DataRowView)("apm_ScheduleAMPM")
            If CType(e.Item.DataItem, DataRowView)("apm_ScheduleTimeZone") <> "" Then
                If CType(e.Item.DataItem, DataRowView)("apm_ScheduleTimeZone") <> ConfigurationManager.AppSettings("ServerTimeZone") Then
                    Dim strConvertedDate As DateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(strDateOriginal, ConfigurationManager.AppSettings("ServerTimeZone"), CType(e.Item.DataItem, DataRowView)("apm_ScheduleTimeZone")).ToString()
                    'Dim strConvertedDate As DateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(strDateOriginal, ConfigurationManager.AppSettings("ServerTimeZone"), CType(e.Item.DataItem, DataRowView)("sm_ScheduleTimeZone")).ToString()
                    CType(e.Item.FindControl("lblSentAutoPostDate"), HtmlGenericControl).InnerText = String.Format("{0:dd MMM yyyy hh:mm tt}", strConvertedDate)
                Else
                    CType(e.Item.FindControl("lblSentAutoPostDate"), HtmlGenericControl).InnerText = String.Format("{0:dd MMM yyyy hh:mm tt}", strDateOriginal)
                End If
            End If

        End If
    End Sub
#End Region

#Region "Clear All"
    Private Sub lnkClearAll_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkClearAll.ServerClick
        ltrAutoPostError.Text = ""
        Dim objBAL As New BALSchedulePost
        objBAL.FBUserId = Session("FacebookUserId")
        objBAL.TSMAUserId = Session("SiteUserId")
        objBAL.FBPageId = If(Session("strPageId") <> Nothing, Session("strPageId"), "")
        objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 '
        objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 '
        objBAL.ClearAllAutoPostData()
        BindAllAutoPost()
        BindAutoPostFanPages()
        BindAutoPostSchedule()
        ClearAutoPostData()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('All Auto Posts deleted successfully');", True)
    End Sub
#End Region

#Region "Clear Auto Posts Data"
    Sub ClearAutoPostData()
        selAutoPostActivationHour.Value = "0"
        selAutoPostActivationMinute.Value = "Minute"
        selAutoPostAMPM.Value = "0"
        ddlAutoPostTimeZone.SelectedValue = "0"
        'pnlOn.Visible = False
        'pnlOff.Visible = True
        'pnlAutoPostOn.Visible = True
        'pnlAutoPostSet.Visible = True
        'pnlAutoPostUpdate.Visible = False
        'pnlAutoPostOff.Visible = False
        For Each item As DataListItem In dstAutoPostFanPages.Items
            Dim myCheckBox As HtmlInputCheckBox
            Dim HiddenID As HtmlInputHidden
            myCheckBox = CType(item.FindControl("autopostchkPage"), HtmlInputCheckBox)
            HiddenID = CType(item.FindControl("hdnPageId"), HtmlInputHidden)
            myCheckBox.Checked = False
        Next
    End Sub
#End Region


    Private Sub lnkAutoPostOff_ServerClick(sender As Object, e As System.EventArgs) Handles lnkAutoPostOff.ServerClick
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Try
                ltrAutoPostError.Text = ""
                Dim ds As DataSet
                Dim objBAL As New BALSchedulePost
                objBAL.FBUserId = If(Session("FacebookUserId") <> Nothing, Session("FacebookUserId"), "")
                objBAL.TSMAUserId = Session("SiteUserId")
                objBAL.FBPageId = If(Session("strPageId") <> Nothing, Session("strPageId"), "")
                objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 '
                objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 '
                ds = objBAL.GetAutoPostSchedule()
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("apsm_IsAutoPostSet") = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowAutoPostOnOff", "ShowAutoPostOnOff('Please set <strong>Auto Post Schedule Time</strong> before <strong>Turn on Auto Post</strong>');", True)
                        Exit Sub
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowAutoPostOnOff", "ShowAutoPostOnOff('Please set <strong>Auto Post Schedule Time</strong> before <strong>Turn on Auto Post</strong>');", True)
                    Exit Sub
                End If


                For Each AutoPostitem As DataListItem In dstAutoPostFanPages.Items
                    Dim AutoPostCheckBox As HtmlInputCheckBox
                    AutoPostCheckBox = CType(AutoPostitem.FindControl("autopostchkPage"), HtmlInputCheckBox)
                    If AutoPostCheckBox.Checked = True Then
                        Dim dsAutoPostOnOf As New DataSet
                        objBAL.FBUserId = Session("FacebookUserId")
                        objBAL.TSMAUserId = Session("SiteUserId")
                        objBAL.FBPageId = CType(AutoPostitem.FindControl("hdnAutoPostPageId"), HtmlInputHidden).Value
                        objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId")
                        objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId")
                        dsAutoPostOnOf = objBAL.AutoPostTurnOnOff()
                    End If
                    'If dsShuffle.Tables(0).Rows(0).Item("Added") = 0 Then
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveScheduleAlert('All available messages in your company library have already been posted');", True)
                    '    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('All available messages in your company library have already been posted');", True)
                    'Else
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveScheduleAlert('All library posts imported successfully');", True)
                    '    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('All library posts imported successfully');", True)
                    'End If
                Next

                'Dim dsAutoPostOnOf As New DataSet
                'objBAL.FBUserId = Session("FacebookUserId")
                'objBAL.TSMAUserId = Session("SiteUserId")
                'objBAL.FBPageId = CType(AutoPostitem.FindControl("hdnAutoPostPageId"), HtmlInputHidden).Value
                'objBAL.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 '
                'objBAL.IndustryId = GetSetCookies.GetCookie("IndustryId") '1 ' 
                'dsAutoPostOnOf = objBAL.AutoPostTurnOnOff()
                'If dsAutoPostOnOf.Tables(0).Rows(0).Item("apsm_OnOff") = 1 Then
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Daily Autopost turned on successfully ');", True)
                'Else
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Daily Autopost turned off successfully ');", True)
                'End If
                'pnlAutoPostOn.Visible = True
                'pnlAutoPostOff.Visible = False
                BindAllAutoPost()
                'BindAutoPostFanPages()
                'BindAutoPostSchedule()
                BindAllSentAutoPost()
                ClearAutoPostData()
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Daily Autopost status changed successfully');", True)
            Catch ex As Exception
                ltrAutoPostError.Text = "Error :" & ex.Message()
            End Try
        Else
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
        End If
    End Sub


    Private Sub drpFanPages_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpFanPages.SelectedIndexChanged
        'BindAutoPostFanPages()
        hdnSetTab.Value = "1"
        BindAllAutoPost()
        'BindAutoPostSchedule()
        BindAllSentAutoPost()
    End Sub
End Class