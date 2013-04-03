Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.Collections.ObjectModel
Imports System.IO
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System
'Imports System.Globalization
'Imports System.Data
'Imports System.Configuration
'Imports System.Web
'Imports System.Web.Security
'Imports System.Web.UI
'Imports System.Web.UI.WebControls
'Imports System.Web.UI.WebControls.WebParts
'Imports System.Web.UI.HtmlControls
'Imports System.Text.RegularExpressions
'Imports System.Threading
'Imports System.Collections.ObjectModel

'----------------------------------------------------------------
' Converted from C# to VB .NET using CSharpToVBConverter(1.2).
' Developed by: Kamal Patel (http://www.KamalPatel.net) 
'----------------------------------------------------------------


Public Class test_image_generate
    Inherits RoutablePage
    Implements IRequiresSessionState

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblLocalTime.Text = DateTime.Now.ToString()
            lblLocalTime1.Text = DateTime.Now.ToString()
			Dim tzi As ReadOnlyCollection(Of TimeZoneInfo)
			tzi = TimeZoneInfo.GetSystemTimeZones()
			For Each timeZone As TimeZoneInfo In tzi
                'ddlTimeZone.Items.Add(New ListItem(timeZone.DisplayName, timeZone.Id))
                ddlTimeZone1.Items.Add(New ListItem(timeZone.DisplayName, timeZone.Id))
            Next
            BindAllAutoPostFanPages()
            BinFanPages()
		End If
		'ViewState("PageID") = ""
        'ViewState("PageAccessToken") = ""
        'ViewState("PageName") = ""
        'BindFanPages()
        'lblLocalTime.Text = txtActivationDate.Value & " " & selActivationHour.Value & ":" & selActivationMinutes.Value & ":00 " & selAMPM.Value  'Date.Now.ToString()
        'Dim tzi As ReadOnlyCollection(Of TimeZoneInfo)
        'tzi = TimeZoneInfo.GetSystemTimeZones()
        'For Each timeZone As TimeZoneInfo In tzi
        '    ddlTimeZone.Items.Add(New ListItem(timeZone.DisplayName, timeZone.Id))
        'Next timeZone


        'For Each timeZone As TimeZoneInfo In tzi
        '    ddlTimeZone.Items.Add(New ListItem(timeZone.DisplayName, timeZone.Id))
        'Next timeZone
        'Dim timeZoneInfo As TimeZoneInfo
        'Dim datetime, datetime1, localDateTime As DateTime
        ''Set the time zone information to US Mountain Standard Time 
        'timeZoneInfo = timeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time")
        ''Get date and time in US Mountain Standard Time 
        'datetime = timeZoneInfo.ConvertTime(Date.Now, timeZoneInfo)
        'localDateTime = Date.Parse(Date.Now)
        'datetime1 = localDateTime.ToUniversalTime
        ''Print out the date and time
        'lblMessage.Text = datetime.ToString("yyyy-MM-dd HH-mm-ss")

        'Dim en As CultureInfo = New CultureInfo("en-US")
        'Thread.CurrentThread.CurrentCulture = en

        '' Creates a DateTime for the local time.
        'Dim dt As DateTime = New DateTime()
        'dt = Date.Now()
        '' Converts the local DateTime to the UTC time.
        'Dim utcdt As DateTime = dt.ToUniversalTime()

        '' Defines a custom string format to display the DateTime value.
        '' zzzz specifies the full time zone offset.
        'Dim format As String = "MM/dd/yyyy hh:mm:sszzz"

        '' Converts the local DateTime to a string 
        '' using the custom format string and display.
        'Dim str As String = dt.ToString(format)
        'Response.Write(str & "<br/>")

        '' Converts the UTC DateTime to a string 
        '' using the custom format string and display.
        'Dim utcstr As String = utcdt.ToString(format)
        'Response.Write(utcstr & "<br/>")

        '' Converts the string back to a local DateTime and displays it.
        'Dim parsedBack As DateTime = Date.ParseExact(str, format, en.DateTimeFormat)
        ''datetime(parsedBack = datetime.ParseExact(str, format, en.DateTimeFormat))
        'Response.Write(parsedBack & "<br/>")

        '' Converts the string back to a UTC DateTime and displays it.
        '' If you do not use the DateTime.ParseExact method that takes
        '' a DateTimeStyles.AdjustToUniversal value, the parsed DateTime
        '' object will not include the time zone information.
        'Dim parsedBackUTC As Date = Date.ParseExact(str, format, en.DateTimeFormat, DateTimeStyles.AdjustToUniversal)
        ''datetime(parsedBackUTC = datetime.ParseExact(str, format, _
        ''en.DateTimeFormat, DateTimeStyles.AdjustToUniversal))
        'Response.Write(parsedBackUTC & "<br/>")

        'Resp(); 
    End Sub
    'Protected Sub ddlTimeZone_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    If ddlTimeZone.SelectedIndex > 0 Then
    '        Response.Write("TimeZone Value : " & ddlTimeZone.SelectedValue & "<br/>")
    '        Dim dt As Date = Date.Now
    '        lblTimeZone.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Local.Id, ddlTimeZone.SelectedValue).ToString()
    '        Dim strDateFormate As DateTime = lblTimeZone.Text

    '        getDetailsOfDateTime(strDateFormate)
    '    End If
    'End Sub
#Region "Bind Fan Pages"
    Sub BindAllAutoPostFanPages()
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Dim accessToken As String = Session("FacebookAccessToken")
            Dim clientId As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
            Dim clientSecret As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey").ToString()
            Dim url As String = "https://graph.facebook.com/me/accounts?fields=id,name,picture,category,access_token&return_ssl_resources=true&access_token={0}"
            Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, accessToken))
            Dim fbResponse As WebResponse = fbRequest.GetResponse()
            Dim stream As Stream = fbResponse.GetResponseStream()
            Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(FanPage))

            Dim fPage1 As New FanPage()
            fPage1 = TryCast(dataContractJsonSerializer.ReadObject(stream), FanPage)
            Dim listPages As New List(Of FanPage.m_data)

            For Each item As FanPage.m_data In fPage1.data
                listPages.Add(item)
            Next
            If listPages.Count > 0 Then
                dstAutoPostFanPages.DataSource = listPages
                dstAutoPostFanPages.DataBind()
            Else
                dstAutoPostFanPages.DataSource = Nothing
                dstAutoPostFanPages.DataBind()
            End If
        Else
            ' Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
        End If
    End Sub
#End Region

#Region "Bind Fan Pages"
    Sub BinFanPages()
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
            Dim listPages1 As New List(Of FanPage.m_data)

            For Each item As FanPage.m_data In fPage.data
                listPages1.Add(item)
            Next
            dstFanPages.DataSource = listPages1
            dstFanPages.DataBind()

        Else
            ' Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath"))
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
        End If
    End Sub
#End Region
    Public Function getDetailsOfDateTime(ByVal strDate As String)
        'Dim strDate1 As DateTime = strDate
        ''strDate1.ToString("MM/dd/yyyy hh:mm tt")
        'Dim strDateMain As String = strDate1.Date
        'Dim strHour As String = Integer.Parse(strDate1.ToString("hh")).ToString()
        'Dim strMin As String = strDate1.Minute
        'Dim strAMPM As String = strDate1.ToString("tt")
        'Response.Write("Date:" & strDateMain & "<br/>")
        'Response.Write("Hour:" & strHour & "<br/>")
        'Response.Write("Minute:" & strMin & "<br/>")
        'Response.Write("AM/PM: " & strAMPM & "<br/>")
    End Function
    'Private Sub lnkPublish_ServerClick(sender As Object, e As System.EventArgs) Handles lnkPublish.ServerClick
    '    Dim siteUrl As String = ConfigurationManager.AppSettings("AppPath") & "generate_image.aspx?id=1" '& Session("SidebarID")
    '    Dim browserWidth As Integer = Convert.ToInt32(800)
    '    Dim browserHeight As Integer = Convert.ToInt32(600)
    '    Dim thumbnailWidth As Integer = Convert.ToInt32(800)
    '    Dim thumbnailHeight As Integer = Convert.ToInt32(600)
    '    Dim relativeImagePath As String = ConfigurationManager.AppSettings("uploadpath")
    '    Dim fullPath As String = Server.MapPath(relativeImagePath)
    '    Dim strExt As String = ""
    '    Dim strPhoto As String = ""
    '    Dim strDate As Date = "1/1/1900"
    '    strPhoto = "sidebar-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & ".jpg"
    '    Session("ImageName") = strPhoto
    '    Dim strD As [String] = strPhoto
    '    If Not fullPath.EndsWith("\") Then
    '        fullPath += "\"
    '    End If
    '    Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteUrl, 180, 540, 220, 640, fullPath, strD), System.Drawing.Image)
    '    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenProgressbar", "ShowProgress();", True)
    '    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseProgressbar", "HideProgress();", True)
    '    'Response.Redirect("publish-sidebar.aspx?id=" & Session("SidebarID"))
    'End Sub

    Protected Sub ddlTimeZone1_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlTimeZone1.SelectedIndex > 0 Then
            Dim dt As DateTime = DateTime.Now
            lblTimeZone1.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Local.Id, ddlTimeZone1.SelectedValue).ToString()
        End If
        '        hdnMakeDateString.Value = txtActivationDate.Value & " " & selActivationHour.Value & ":" & selActivationMinute.Value & selAMPM.Value  'Date.Now.ToString()
        '        If ddlTimeZone.SelectedIndex > 0 Then
        '            If ddlTimeZone.SelectedValue <> ConfigurationManager.AppSettings("ServerTimeZone") Then
        '                lblTimeZone.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(hdnMakeDateString.Value, ConfigurationManager.AppSettings("ServerTimeZone"), ddlTimeZone.SelectedValue).ToString()
        '            Else
        '                lblTimeZone.Text = hdnMakeDateString.Value
        '            End If
        '        End If
    End Sub


    '#Region "Bind Fan Pages"
    '    Sub BindFanPages()
    '        Try
    '            'If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
    '            Dim accessToken As String = Session("FacebookAccessToken")
    '            Dim clientId As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
    '            Dim clientSecret As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey").ToString()
    '            Dim url As String = "https://graph.facebook.com/me/accounts?fields=id,name,picture,category,access_token&return_ssl_resources=true&access_token={0}"
    '            Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, accessToken))
    '            Dim fbResponse As WebResponse = fbRequest.GetResponse()
    '            Dim stream As Stream = fbResponse.GetResponseStream()
    '            Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(FanPage))

    '            Dim fPage As New FanPage()
    '            fPage = TryCast(dataContractJsonSerializer.ReadObject(stream), FanPage)
    '            Dim listPages As New List(Of FanPage.m_data)

    '            For Each item As FanPage.m_data In fPage.data
    '                listPages.Add(item)
    '            Next
    '            If listPages.Count > 0 Then
    '                dstFanPages.DataSource = listPages
    '                dstFanPages.DataBind()
    '                'plcData.Visible = True
    '                'plcNoData.Visible = False
    '            Else
    '                dstFanPages.DataSource = Nothing
    '                dstFanPages.DataBind()
    '                'plcData.Visible = False
    '                'plcNoData.Visible = True
    '            End If
    '            'Else
    '            'Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath") & "login.aspx")
    '            'End If
    '        Catch ex As Exception
    '            lblMessage.Text = "Error: " & ex.Message
    '        End Try
    '    End Sub
    '#End Region

    '#Region "Upload Sidebar"
    '    Private Sub lnkUploadPhoto_ServerClick(sender As Object, e As System.EventArgs) Handles lnkUploadPhoto.ServerClick
    '        Try
    '            'If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
    '            Dim AccessToken As String = Session("FacebookAccessToken")
    '            Dim strActivationHours As String = Now.Hour
    '            Dim strActivationMinutes As String = Now.Minute
    '            Dim pageAccessToken = "AAAD2ZCc3HZAxABAPtaMKzCQKocvsiH0aZCyAFP6gEsmyG1CF0RwpKNUW7JAZBiH7ylNRZCmaGDp4v2IxSNVSdyXXJVbRGigjwXcTJpejsAJppODRG7ykE"
    '            'Dim strmsg As String = txtMessage.Value
    '            Dim strDate As String = Now.Date
    '            Dim strPageId As String = hdnselectedPages.Value
    '            Dim strPageName As String = hdnSelectedPagesName.Value
    '            Dim strPageImage As String = hdnSelectedPagesImage.Value
    '            Dim strPageAccessToken As String = hdnselectedPagesAccessToken.Value
    '            Dim strExt As String = ""
    '            Dim strPhoto As String = ""

    '            If photo.PostedFile.FileName <> "" Then
    '                strExt = IO.Path.GetExtension(photo.PostedFile.FileName).ToLower
    '                If Not (strExt = ".jpg" Or strExt = ".gif" Or strExt = ".bmp" Or strExt = ".jpeg" Or strExt = ".png") Then
    '                    lblMessage.Text = "File must be .jpg or .gif or .png or .jpeg or .tif or .bmp"
    '                    Exit Sub
    '                End If
    '                Dim strDate1 As Date = "1/1/1900"
    '                strPhoto = "picture-" & CStr(DateDiff(DateInterval.Second, strDate1, Now())) & strExt
    '                photo.PostedFile.SaveAs(Server.MapPath("~/" & "Content\uploads\" & strPhoto))

    '            End If

    '            Dim photopath As String = Server.MapPath("~/" & "Content/uploads/" & strPhoto)

    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenProgressbar", "ShowProgress();", True)
    '            Dim str As String = ""
    '            For Each item As DataListItem In dstFanPages.Items
    '                Dim myCheckBox As HtmlInputCheckBox
    '                myCheckBox = CType(item.FindControl("chkPage"), HtmlInputCheckBox)
    '                If myCheckBox.Checked = True Then
    '                    Dim path As String = "195470630536254/photos"
    '                    Dim mediaObject As New FacebookMediaObject() With { _
    '                        .FileName = photopath, _
    '                        .ContentType = "image/jpg" _
    '                }
    '                    Dim fileBytes As Byte() = File.ReadAllBytes(mediaObject.FileName)
    '                    mediaObject.SetValue(fileBytes)
    '                    Dim upload As IDictionary(Of String, Object) = New Dictionary(Of String, Object)()
    '                    'upload.Add("message", strmsg)
    '                    upload.Add("image", mediaObject)
    '                    Dim fbapp = New FacebookClient("")
    '                    fbapp.Post(path, upload)
    '                End If
    '            Next


    '            lblMessage.Text = "Photo Uploaded Successfully"
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseProgressbar", "HideProgress();", True)
    '            'Else
    '            'Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath") & "login.aspx")
    '            'End If

    '        Catch ex As Exception
    '            lblMessage.Text = "Error: " & ex.Message
    '        End Try
    '    End Sub
    '#End Region


    'Private Sub lnkScheduledPost_ServerClick(sender As Object, e As System.EventArgs) Handles lnkScheduledPost.ServerClick
    '    lblLocalTime.Text = txtActivationDate.Value & " " & selActivationHour.Value & ":" & selActivationMinutes.Value & ":00 " & selAMPM.Value  'Date.Now.ToString()
    '    'Dim dt As Date = Date.Now
    '    If ddlTimeZone.SelectedIndex > 0 Then
    '        lblTimeZone.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(lblLocalTime.Text, TimeZoneInfo.Local.Id, ddlTimeZone.SelectedValue).ToString()
    '        getDetailsOfDateTime(lblTimeZone.Text)
    '    End If
    'End Sub

    Private Sub lnkScheduledPost_ServerClick(sender As Object, e As System.EventArgs) Handles lnkScheduledPost.ServerClick
        Response.Write(TimeZoneInfo.Local.Id)
        Dim UserDateTime As DateTime = txtActivationDate.Value & " " & selActivationHour.Value & ":" & selActivationMinute.Value & ":00 " & selAMPM.Value  'Date.Now.ToString()
        Response.Write(UserDateTime)
        'hdnMakeDateString.Value = txtActivationDate.Value & " " & selActivationHour.Value & ":" & selActivationMinute.Value & ":00 " & selAMPM.Value  'Date.Now.ToString()
        If ddlTimeZone.SelectedIndex > 0 Then
            If ddlTimeZone.SelectedValue <> ConfigurationManager.AppSettings("ServerTimeZone") Then
                lblTimeZone.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(UserDateTime, ddlTimeZone.SelectedValue, TimeZoneInfo.Local.Id).ToString()
                lblReOrgTimeZone.Text = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(lblTimeZone.Text, TimeZoneInfo.Local.Id, ddlTimeZone.SelectedValue).ToString()
            Else
                lblTimeZone.Text = UserDateTime
            End If
        End If

    End Sub

    Private Sub lnkAutoPost_ServerClick(sender As Object, e As System.EventArgs) Handles lnkAutoPost.ServerClick
        'If Not Page.IsPostBack Then
        Dim objBAL As New BALSchedulePost
        For Each AutoPostitem As DataListItem In dstAutoPostFanPages.Items
            Dim AutoPostCheckBox As HtmlInputCheckBox
            AutoPostCheckBox = CType(AutoPostitem.FindControl("AutoPostchkPage"), HtmlInputCheckBox)
            Response.Write(CType(AutoPostitem.FindControl("hdnAutoPostPageId"), HtmlInputHidden).Value & "<br/>")
            Response.Write(CType(AutoPostitem.FindControl("hdnAutoPostPageName"), HtmlInputHidden).Value & "<br/>")
            Response.Write(CType(AutoPostitem.FindControl("hdnAutoPostImage"), HtmlInputHidden).Value & "<br/>")
            Response.Write(CType(AutoPostitem.FindControl("hdnAutoPostAccessToken"), HtmlInputHidden).Value & "<br/>")
            Response.Write(AutoPostCheckBox.Checked)
            If AutoPostCheckBox.Checked = True Then
                Response.Write("test")
                objBAL.TSMAUserId = Session("SiteUserId")
                objBAL.FBUserId = Session("FacebookUserId")
                objBAL.CompanyId = 0 'GetSetCookies.GetCookie("CompanyId") 
                objBAL.IndustryId = 1 'GetSetCookies.GetCookie("IndustryId") 
                objBAL.FBPageId = CType(AutoPostitem.FindControl("hdnAutoPostPageId"), HtmlInputHidden).Value
                objBAL.FBPageName = CType(AutoPostitem.FindControl("hdnAutoPostPageName"), HtmlInputHidden).Value
                objBAL.FBPageImage = CType(AutoPostitem.FindControl("hdnAutoPostImage"), HtmlInputHidden).Value
                objBAL.FBPageAccessToken = CType(AutoPostitem.FindControl("hdnAutoPostAccessToken"), HtmlInputHidden).Value
                objBAL.AddAutoPostPages()
            End If
        Next
        Response.End()
        'End If
    End Sub

    Private Sub A1_ServerClick(sender As Object, e As System.EventArgs) Handles A1.ServerClick
        Dim objBAL As New BALSchedulePost
        For Each item As DataListItem In dstFanPages.Items
            Dim myCheckBox As HtmlInputCheckBox
            myCheckBox = CType(item.FindControl("chkPage"), HtmlInputCheckBox)

            Response.Write(CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value & "<br/>")
            Response.Write(CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value & "<br/>")
            Response.Write(CType(item.FindControl("hdnImage"), HtmlInputHidden).Value & "<br/>")
            Response.Write(CType(item.FindControl("hdnAccessToken"), HtmlInputHidden).Value & "<br/>")
            Response.Write(myCheckBox.Checked)
            If myCheckBox.Checked = True Then
                Response.Write("Test" & "<br/>")
                'objBAL.ScheduleId = ID
                objBAL.FBPageId = CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
                objBAL.FBPageName = CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value
                objBAL.FBPageImage = CType(item.FindControl("hdnImage"), HtmlInputHidden).Value
                objBAL.FBPageAccessToken = CType(item.FindControl("hdnAccessToken"), HtmlInputHidden).Value
                'objBAL.AddQuickPost()
            End If
        Next
        Response.End()
    End Sub
End Class