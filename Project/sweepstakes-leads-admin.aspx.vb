Imports BusinessAccessLayer.BusinessLayer

Public Class admin_view_lead
    Inherits System.Web.UI.Page
    Dim objLeads As New BusinessAccessLayer.BusinessLayer.Leads
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoginCheck.LoginSessionCheck()
        Dim obj As New BAlsidebar
        If Not Page.IsPostBack Then
            ViewState("CurrentPageIndex") = 0
            GetSweepstakesLeads()
            Dim ds1 As New DataSet
            obj.Page = CStr(HttpContext.Current.Request.Url.AbsolutePath)
            ds1 = obj.GetVideoTutorial()
            If HttpContext.Current.Request.Url.AbsolutePath = ds1.Tables(0).Rows(0).Item("vt_Page").ToString Then
                btnVideo.HRef = ds1.Tables(0).Rows(0).Item("vt_Video").ToString & "?fs=1&autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer"
                btnVideo.Title = ds1.Tables(0).Rows(0).Item("vt_Title").ToString
            End If
        End If

    End Sub

    Sub GetSweepstakesLeads()
        objLeads.PageIndex = ViewState("CurrentPageIndex")
        objLeads.FBUserId = FBUserID()
        objLeads.CompanyId = GetSetCookies.GetCookie("CompanyId")
        objLeads.IndustryId = GetSetCookies.GetCookie("IndustryId")
        Dim ds As DataSet
        ds = objLeads.GetAdminLeadsForSW
        If ds.Tables(0).Rows.Count > 0 Then


            'If Session("FinalFanPages") IsNot Nothing Then
            'Dim forrentfanpages As New List(Of String)
            'forrentfanpages = Session("FinalFanPages")
            Dim dtTable As New DataTable
            dtTable.Columns.Add(New DataColumn("id", Type.GetType("System.Int32")))
            dtTable.Columns.Add(New DataColumn("PageId", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("PageName", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("PageUrl", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("PublishedDate", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("PublishedBy", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("IsNotification", Type.GetType("System.Int32")))
            dtTable.Columns.Add(New DataColumn("NotificationEmail", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("Leads", Type.GetType("System.String")))
            dtTable.Columns.Add(New DataColumn("UpdatedDate", Type.GetType("System.String")))

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                'For j As Integer = 0 To forrentfanpages.Count - 1
                '    If ds.Tables(0).Rows(i).Item("PageUrl").ToString().ToLower = forrentfanpages.Item(j).ToString.ToLower Then
                '        Dim dtRow1 As DataRow = dtTable.NewRow
                '        dtRow1("id") = ds.Tables(0).Rows(i).Item("id")
                '        dtRow1("PageId") = ds.Tables(0).Rows(i).Item("PageId").ToString
                '        dtRow1("PageName") = ds.Tables(0).Rows(i).Item("PageName").ToString
                '        dtRow1("PageUrl") = ds.Tables(0).Rows(i).Item("PageUrl").ToString
                '        dtRow1("PublishedDate") = ds.Tables(0).Rows(i).Item("PublishedDate").ToString
                '        dtRow1("PublishedBy") = ds.Tables(0).Rows(i).Item("PublishedBy")
                '        dtRow1("IsNotification") = ds.Tables(0).Rows(i).Item("IsNotification")
                '        dtRow1("NotificationEmail") = ds.Tables(0).Rows(i).Item("NotificationEmail").ToString
                '        dtRow1("Leads") = ds.Tables(0).Rows(i).Item("Leads").ToString
                '        dtRow1("UpdatedDate") = ds.Tables(0).Rows(i).Item("UpdatedDate").ToString
                '        dtTable.Rows.Add(dtRow1)
                '        'Exit For
                '    ElseIf ds.Tables(0).Rows(i).Item("PageId").ToString().ToLower = forrentfanpages.Item(j).ToString.ToLower Then
                Dim strDate1 As DateTime
                Dim strDate As String
                If ds.Tables(0).Rows(i).Item("PublishedDate").ToString <> "--" Then
                    strDate1 = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(ds.Tables(0).Rows(i).Item("PublishedDate").ToString, ConfigurationManager.AppSettings("ServerTimeZone"), "Pacific Standard Time")
                    'Else
                    'lblTimeZone.Text = hdnMakeDateString.Value
                    'End If
                    'End If
                    strDate = strDate1.Date
                Else
                    strDate = "--"
                End If

                Dim dtRow1 As DataRow = dtTable.NewRow
                dtRow1("id") = ds.Tables(0).Rows(i).Item("id")
                dtRow1("PageId") = ds.Tables(0).Rows(i).Item("PageId").ToString
                dtRow1("PageName") = ds.Tables(0).Rows(i).Item("PageName").ToString
                dtRow1("PageUrl") = ds.Tables(0).Rows(i).Item("PageUrl").ToString
                dtRow1("PublishedDate") = strDate 'ds.Tables(0).Rows(i).Item("PublishedDate").ToString
                dtRow1("PublishedBy") = ds.Tables(0).Rows(i).Item("PublishedBy")
                dtRow1("IsNotification") = ds.Tables(0).Rows(i).Item("IsNotification")
                dtRow1("NotificationEmail") = ds.Tables(0).Rows(i).Item("NotificationEmail").ToString
                dtRow1("Leads") = ds.Tables(0).Rows(i).Item("Leads").ToString
                dtRow1("UpdatedDate") = ds.Tables(0).Rows(i).Item("UpdatedDate").ToString
                dtTable.Rows.Add(dtRow1)
                ' End If
                'Next
            Next


            'Response.End()
            dtTable.TableName = "fanpages"
            Dim dv As DataView = New DataView(dtTable)
            dv.Sort = "PageName"

            If dv.Count > 0 Then
                rptPages.DataSource = dv
                rptPages.DataBind()
                plcNoData.Visible = False
                plcData.Visible = True
                'Else
                '    Dim strUrl As String = "Login.aspx?i=" & Session("Industry") & "&c=" & Session("company") & "&u=2&at=" & Session("hdnToken") & ""
                '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, ";ShowHide13;", ";openNewWin('" & strUrl & "');", True)
            Else
                rptPages.DataSource = Nothing
                rptPages.DataBind()
                'ltrMsg.Text = "No business Pages Found!"
                plcData.Visible = False
                plcNoData.Visible = True
            End If
            'Else
            '    rptPages.DataSource = ds.Tables(0)
            '    rptPages.DataBind()
            'End If
        Else
        rptPages.DataSource = Nothing
        rptPages.DataBind()
        plcData.Visible = False
        plcNoData.Visible = True
        End If
    End Sub

    Function FBUserID() As String
        Return IIf(Session("FacebookUserId") <> Nothing, Session("FacebookUserId"), "")
    End Function

    Sub TurnOnnOffAutoPost(sender As Object, e As CommandEventArgs)

        Dim dsAutoPostOnOf As New DataSet
        objLeads.FBUserId = Session("FacebookUserId")
        objLeads.FBPageId = e.CommandName
        'dsAutoPostOnOf = objLeads.NotificationTurnOnOffForSW()
        objLeads.NotificationTurnOnOffForSW()
        ltrMsg.Text = "Auto Post Status Changed Successfully."
        GetSweepstakesLeads()
    End Sub

    Sub SaveNotificationEmail(sender As Object, e As CommandEventArgs)
        For Each item As RepeaterItem In rptPages.Items
            If e.CommandName = CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value Then
                Dim objLeads1 As New BusinessAccessLayer.BusinessLayer.Leads
                Dim dsAutoPostOnOf1 As New DataSet
                objLeads1.FBUserId = Session("FacebookUserId")
                objLeads1.FBPageId = CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
                objLeads1.SWEmail = CType(item.FindControl("txtEmail"), HtmlInputText).Value
                'objLeads1.Status = CType(item.FindControl("hdnStatus"), HtmlInputHidden).Value
                'dsAutoPostOnOf1 = objLeads1.NotificationEmailForSW()
                objLeads1.NotificationEmailForSW()
            End If
            ltrMsg.Text = "Notification Email Saved Successfully."
        Next
        GetSweepstakesLeads()
    End Sub

    Sub ConfigureAutoPost(sender As Object, e As CommandEventArgs)
        Session("PageIds") = e.CommandName
        'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, ";TransferPage;", ";OpenWindow();", True)
        Response.Redirect("configure-autopost")
    End Sub

    Private Sub rptPages_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptPages.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            CType(e.Item.FindControl("hdnPageId"), HtmlInputHidden).Value = CType(e.Item.DataItem, DataRowView)("PageId")
            If CType(e.Item.DataItem, DataRowView)("NotificationEmail") <> "" AndAlso CType(e.Item.DataItem, DataRowView)("NotificationEmail") <> "--" Then
                CType(e.Item.FindControl("txtEmail"), HtmlInputText).Value = CType(e.Item.DataItem, DataRowView)("NotificationEmail")
            Else
                CType(e.Item.FindControl("txtEmail"), HtmlInputText).Value = ""
            End If
        End If
    End Sub

   
End Class