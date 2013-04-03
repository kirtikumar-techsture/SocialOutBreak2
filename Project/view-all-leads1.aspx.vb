
Imports System.Net
Imports System.Runtime.Serialization.Json
Imports BusinessAccessLayer.BusinessLayer
Imports System.IO
Imports Facebook
Imports System.Threading

Public Class leads
    Inherits System.Web.UI.Page
    Dim objLeads1 As New BusinessAccessLayer.BusinessLayer.Leads
    Dim objPaging1 As Paging = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ltrMsg.Text = ""
        objPaging1 = New Paging("bp_", ViewState, phPagingTop, phPagingBottom, _
                                               "<img src='content/images/left_arrow.gif' border='0' />", _
                                               "<img src='content/images/right_arrow.gif' border='0' />", _
                                                AddressOf BindLeads, "currentpage")
        objPaging1.PageLinkClass = "paginglink"
        If Not Page.IsPostBack Then
            objPaging1.CurrentPage = 1
            BindAllAutoPostFanPages()
            BindLeads()
        Else
            objPaging1.GenerateControls()
        End If
    End Sub

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
                drpFanPages.DataSource = dv
                drpFanPages.DataValueField = "id"
                drpFanPages.DataTextField = "name"
                drpFanPages.DataBind()
            Else
                drpFanPages.DataSource = Nothing
                drpFanPages.DataBind()

            End If
        Catch ex As Exception
            ltrMsg.Text = "Error" & ex.Message.ToString
        End Try

    End Sub
#End Region

    Sub BindLeads()

        objLeads1.PageIndex = objPaging1.CurrentPage
        objLeads1.FBUserId = drpFanPages.SelectedValue
        'objLeads1.FBUserId = IIf(Session("FacebookUserId") <> Nothing, Session("FacebookUserId"), "")
        objLeads1.SortBy = hdnSortexp.Value
        Dim ds As DataSet = objLeads1.GetAllLeads
        If ds.Tables(0).Rows.Count > 0 Then
            dgrLeads.DataSource = ds.Tables(0)
            dgrLeads.DataBind()
        Else
            dgrLeads.DataSource = Nothing
            dgrLeads.DataBind()
            ltrMsg.Text = "No Data Found"
        End If
        If ds.Tables(1).Rows.Count > 0 Then
            objPaging1.TotalPages = ds.Tables(1).Rows(0).Item("TotalPage").ToString
            objPaging1.TotalRecords = ds.Tables(1).Rows(0).Item("TotalRec").ToString
        End If
        objPaging1.ResetControls()
        objPaging1.GenerateControls()
    End Sub

    Private Sub dgrLeads_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles dgrLeads.Sorting
        If e.SortExpression = "Name" Then
            If dgrLeads.Columns(0).HeaderText = "Name <img src='content/images/asc.gif' id='imgsort' border='0' align='absmiddle'/>" Then
                hdnSortexp.Value = "Name DESC"
                dgrLeads.Columns(0).HeaderText = "Name <img src='content/images/desc.gif' id='imgsort' border='0' align='absmiddle'/>"
            Else
                hdnSortexp.Value = "Name ASC"
                dgrLeads.Columns(0).HeaderText = "Name <img src='content/images/asc.gif' id='imgsort' border='0' align='absmiddle'/>"
            End If
            dgrLeads.Columns(1).HeaderText = "Email <img src=content/images/i-sort.gif id=imgsort border=0 align='absmiddle'/>"
            dgrLeads.Columns(2).HeaderText = "Phone <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            dgrLeads.Columns(4).HeaderText = "Lead From <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            dgrLeads.Columns(5).HeaderText = "Date <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            BindLeads()
        End If
        If e.SortExpression = "Email" Then
            If dgrLeads.Columns(1).HeaderText = "Email <img src='content/images/asc.gif' id='imgsort' border='0' align='absmiddle'/>" Then
                hdnSortexp.Value = "Email DESC"
                dgrLeads.Columns(1).HeaderText = "Email <img src='content/images/desc.gif' id='imgsort' border='0' align='absmiddle'/>"
            Else
                hdnSortexp.Value = "Email ASC"
                dgrLeads.Columns(1).HeaderText = "Email <img src='content/images/asc.gif' id='imgsort' border='0' align='absmiddle'/>"
            End If
            dgrLeads.Columns(0).HeaderText = "Name <img src=content/images/i-sort.gif id=imgsort border=0 align='absmiddle'/>"
            dgrLeads.Columns(2).HeaderText = "Phone <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            dgrLeads.Columns(4).HeaderText = "Lead From <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            dgrLeads.Columns(5).HeaderText = "Date <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            BindLeads()
        End If
        If e.SortExpression = "Phone" Then
            If dgrLeads.Columns(2).HeaderText = "Phone <img src='content/images/asc.gif' id='imgsort' border='0' align='absmiddle'/>" Then
                hdnSortexp.Value = "Phone DESC"
                dgrLeads.Columns(2).HeaderText = "Phone <img src='content/images/desc.gif' id='imgsort' border='0' align='absmiddle'/>"
            Else
                hdnSortexp.Value = "Phone ASC"
                dgrLeads.Columns(2).HeaderText = "Phone <img src='content/images/asc.gif' id='imgsort' border='0' align='absmiddle'/>"
            End If
            dgrLeads.Columns(0).HeaderText = "Name <img src=content/images/i-sort.gif id=imgsort border=0 align='absmiddle'/>"
            dgrLeads.Columns(1).HeaderText = "Email <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            dgrLeads.Columns(4).HeaderText = "Lead From <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            dgrLeads.Columns(5).HeaderText = "Date <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            BindLeads()
        End If
        If e.SortExpression = "LeadFrom" Then
            If dgrLeads.Columns(4).HeaderText = "LeadFrom <img src='content/images/asc.gif' id='imgsort' border='0' align='absmiddle'/>" Then
                hdnSortexp.Value = "LeadFrom DESC"
                dgrLeads.Columns(4).HeaderText = "LeadFrom <img src='content/images/desc.gif' id='imgsort' border='0' align='absmiddle'/>"
            Else
                hdnSortexp.Value = "LeadFrom ASC"
                dgrLeads.Columns(4).HeaderText = "LeadFrom <img src='content/images/asc.gif' id='imgsort' border='0' align='absmiddle'/>"
            End If
            dgrLeads.Columns(0).HeaderText = "Name <img src=content/images/i-sort.gif id=imgsort border=0 align='absmiddle'/>"
            dgrLeads.Columns(1).HeaderText = "Email <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            dgrLeads.Columns(2).HeaderText = "Phone <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            dgrLeads.Columns(5).HeaderText = "Date <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            BindLeads()
        End If
        If e.SortExpression = "Date" Then
            If dgrLeads.Columns(5).HeaderText = "Date <img src='content/images/asc.gif' id='imgsort' border='0' align='absmiddle'/>" Then
                hdnSortexp.Value = "Date DESC"
                dgrLeads.Columns(5).HeaderText = "Date <img src='content/images/desc.gif' id='imgsort' border='0' align='absmiddle'/>"
            Else
                hdnSortexp.Value = "Date ASC"
                dgrLeads.Columns(5).HeaderText = "Date <img src='content/images/asc.gif' id='imgsort' border='0' align='absmiddle'/>"
            End If
            dgrLeads.Columns(0).HeaderText = "Name <img src=content/images/i-sort.gif id=imgsort border=0 align='absmiddle'/>"
            dgrLeads.Columns(1).HeaderText = "Email <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            dgrLeads.Columns(2).HeaderText = "Phone <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            dgrLeads.Columns(4).HeaderText = "LeadFrom <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"
            BindLeads()
        End If
    End Sub

#Region "Create CSV"
    Sub Create_Csv()
        Try
            objLeads1.PageIndex = 0
            objLeads1.FBUserId = drpFanPages.SelectedValue
            'objLeads1.FBUserId = IIf(Session("FacebookUserId") <> Nothing, Session("FacebookUserId"), "")
            objLeads1.SortBy = hdnSortexp.Value
            Dim ds As DataSet = objLeads1.GetAllLeads

            Dim strPageName As String = drpFanPages.SelectedItem.Text.ToString.Replace(" ", "-")
            Dim strPath As String = strPageName & "-Leads-" & CStr(Day(Now)) & CStr(Month(Now)) & CStr(Year(Now)) & CStr(Hour(Now)) & CStr(Minute(Now)) & CStr(Second(Now)) & ".csv"
            Response.Expires = 0
            Response.Clear()
            Response.AddHeader("Content-Disposition", "inline;filename=" & strPath & "")
            Response.AddHeader("Content-type", "text/csv")

            'Response.Write(""" UserId "",")
            'Response.Write(""" PageId "",")
            'Response.Write(""" PageName "",")
            Response.Write(""" Name "",")
            ' Response.Write(""" Content "",")
            Response.Write(""" Email "",")
            Response.Write(""" Phone "",")
            Response.Write(""" Message "",")
            Response.Write(""" Lead From "",")
            Response.Write(""" Date "",")
            Response.Write(vbNewLine)
            Dim i As Integer
            For i = 0 To ds.Tables(0).Rows.Count - 1
                With ds.Tables(0).Rows(i)
                    'Response.Write("""" & .Item("FBUserId").ToString() & """,")
                    'Response.Write("""" & .Item("FBPageId").ToString() & """,")
                    'Response.Write("""" & drpFanPages.SelectedItem.Text & """,")
                    Response.Write("""" & .Item("Name").ToString() & """,")
                    Response.Write("""" & .Item("Email").ToString() & """,")
                    Response.Write("""" & .Item("Phone").ToString() & """,")
                    Response.Write("""" & .Item("Message").ToString() & """,")
                    Response.Write("""" & .Item("LeadFrom").ToString() & """,")
                    Response.Write("""" & .Item("Date").ToString() & """,")
                    Response.Write(vbNewLine)
                End With
            Next
            ds.Dispose()
            Response.Flush()
            'Response.Close()
            Response.End()
        Catch ex As Exception
            ltrMsg.Text = "Error: " & ex.Message
        End Try
    End Sub
#End Region

    Private Sub drpFanPages_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpFanPages.SelectedIndexChanged
        BindLeads()
    End Sub

    Private Sub btnExport_ServerClick(sender As Object, e As System.EventArgs) Handles btnExport.ServerClick
        Create_Csv()
    End Sub
End Class