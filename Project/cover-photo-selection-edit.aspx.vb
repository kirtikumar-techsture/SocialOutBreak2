Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports System.Drawing.Image
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System.Threading

Public Class cover_photo_selection_edit
    Inherits System.Web.UI.Page
    Public strFBUserId As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                    strFBUserId = Session("FacebookUserId").ToString
                    Dim obj As New BALCoverPhoto
                    Dim ds1 As New DataSet
                    obj.Page = "/cover-photo"
                    ds1 = obj.GetVideoTutorial()
                    Dim videostring As String
                    videostring = Replace(ds1.Tables(0).Rows(0).Item("vt_Video").ToString, "watch?v=", "v/")
                    spnVideo.InnerHtml = "<object style='height: 335px; width: 555px'><param name='movie' value='" & videostring & "'><param name='allowFullScreen' value='true'><param name='allowScriptAccess' value='always'><param name='wmode' value='transparent'><embed src='" & videostring & "' type='application/x-shockwave-flash' allowfullscreen='true' allowScriptAccess='always' width='555' height='335'></object>"

                    If Request.QueryString("cpId") <> "" And IsNumeric(Request.QueryString("cpId")) Then

                        BindSavedCoverPhoto()
                        ' BindCoverPhoto()

                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
                End If
            Catch ex As Exception
                lblMessage.Text = "Error: " & ex.Message
            End Try
        End If
    End Sub


    Public Function BindSavedCoverPhoto()
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim ds As New DataSet
                Dim objBAL As New BALCoverPhoto
                objBAL.CoverPhotoId = CInt(Request.QueryString("cpId"))
                objBAL.UserId = CInt(Request.QueryString("userId"))
                objBAL.FBUserId = Request.QueryString("FbuserId")
                objBAL.FBUserEmail = Session("FacebookUserEmail")
                objBAL.FBPageId = Session("strPageId")
                objBAL.CompanyId = CInt(Request.QueryString("CId"))
                objBAL.IndustryId = CInt(Request.QueryString("IId"))
                ds = objBAL.GetCoverPhotoMasterByID
                If ds.Tables(0).Rows.Count > 0 Then
                    txtname.Text = ds.Tables(1).Rows(0).Item("CoverPhotoName")
                    txtCoverPhotoName.Value = ds.Tables(1).Rows(0).Item("CoverPhotoName")
                    txtUserEmail.Value = ds.Tables(0).Rows(0).Item("FBUserEmail")
                    lnkFanPageName.InnerHtml = ds.Tables(0).Rows(0).Item("cp_FBPageName")
                    divCoverPhotoHtml.InnerHtml = ds.Tables(0).Rows(0).Item("CoverPhotoContent").Replace("{%domain%}/", ConfigurationManager.AppSettings("AppPath"))
                    txtUserEmail.Value = ds.Tables(0).Rows(0).Item("FBUserEmail")
                    Session("CoverPhotoID") = ds.Tables(0).Rows(0).Item("CoverPhotoId")
                Else
                    Response.Redirect("/create-cover-photo")
                End If
                If ds.Tables(2).Rows(0).Item("mainadmin") = 1 Then
                    pnlEdit.Visible = True
                    pnlMessage.Visible = False
                Else
                    pnlEdit.Visible = False
                    pnlMessage.Visible = True
                    lnkFanPageName.InnerHtml = ""
                    spnAssigned.InnerHtml = "A member of Total Social Media has shared this Cover Photo with you for viewing purposes only. To edit, please create a copy in '<b><a href='cover-photo' title='My Saved Cover Photos'>My Saved Cover Photos</a></b>' and edit that copy"
					'spnAssigned.InnerHtml = "An administrator of business page <b>" & ds.Tables(0).Rows(0).Item("FBPageName") & "</b> has shared this Cover Photo with you for viewing purposes only. To edit, please create a copy in '<b><a href='cover-photo' title='My Saved Cover Photos'>My Saved Cover Photos</a></b>' and edit that copy"
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
                divCoverPhotoHtml.InnerHtml = ""
                Dim ds As New DataSet
                Dim objBAL As New BALCoverPhoto
                objBAL.CoverPhotoId = CInt(Request.QueryString("cpId"))
                objBAL.UserId = CInt(Request.QueryString("userId"))
                objBAL.FBUserId = Request.QueryString("FbuserId")
                objBAL.CompanyId = CInt(Request.QueryString("CId"))
                objBAL.IndustryId = CInt(Request.QueryString("IId"))
                ds = objBAL.GetCoverPhotoMasterByID
                If ds.Tables(0).Rows.Count > 0 Then
                    divCoverPhotoHtml.InnerHtml = ds.Tables(0).Rows(0).Item("CoverPhotoContent").Replace("{%domain%}/", ConfigurationManager.AppSettings("AppPath"))
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

                Dim objBAL As New BALCoverPhoto
                objBAL.CoverPhotoId = CInt(Request.QueryString("cpId"))
                objBAL.UserId = CInt(Request.QueryString("userId"))
                objBAL.FBUserId = Request.QueryString("FbuserId")
                objBAL.FBUserEmail = txtUserEmail.Value
                objBAL.CompanyId = CInt(Request.QueryString("CId"))
                objBAL.CoverPhotoName = If(txtCoverPhotoName.Value <> "", txtCoverPhotoName.Value, "")
                objBAL.IndustryId = CInt(Request.QueryString("IId"))
                objBAL.CoverPhotoContent = hdnCoverPhotoContent.Value

                ViewState("CoverPhotoID") = CInt(Request.QueryString("cpId"))
                ViewState("FBUserId") = Request.QueryString("FbuserId")

                ' objBAL.CoverPhotoImage = Session("ImageName")
                objBAL.UpdateCoverPhotoContent()
                BindSavedCoverPhoto()

                Dim siteUrl As String = ConfigurationManager.AppSettings("AppPath") & "generate-cover-image.aspx?id=" & Session("CoverPhotoID")
                Dim browserWidth As Integer = Convert.ToInt32(800)
                Dim browserHeight As Integer = Convert.ToInt32(600)
                Dim thumbnailWidth As Integer = Convert.ToInt32(800)
                Dim thumbnailHeight As Integer = Convert.ToInt32(600)
                Dim relativeImagePath As String = ConfigurationManager.AppSettings("uploadpath")
                Dim fullPath As String = Server.MapPath(relativeImagePath)
                Dim strExt As String = ""
                Dim strPhoto As String = ""
                Dim strDate As Date = "1/1/1900"
                strPhoto = "CoverPhoto-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & ".jpg"
                Session("ImageName") = strPhoto
                Dim strD As [String] = strPhoto
                If Not fullPath.EndsWith("\") Then
                    fullPath += "\"
                End If
                Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteUrl, hdnWidth.Value.Replace("px", ""), hdnHeight.Value.Replace("px", ""), hdnWidth.Value.Replace("px", ""), hdnHeight.Value.Replace("px", ""), fullPath, strD), System.Drawing.Image)

                objBAL.CoverPhotoId = ViewState("CoverPhotoID")
                objBAL.FBUserId = ViewState("FBUserId")
                objBAL.CoverPhotoImage = Session("ImageName")
                objBAL.UpdateImageName()
                hdnPublished.Value = "0"


                'lblMessage.Text = "Saved Successfully!"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Your work has been saved');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try


    End Sub

    Private Sub lnkPublish_ServerClick(sender As Object, e As System.EventArgs) Handles lnkPublish.ServerClick
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                If hdnPublished.Value = "1" Then
                    ViewState("CoverPhotoID") = CInt(Request.QueryString("cpId"))
                    ViewState("FBUserId") = Request.QueryString("FbuserId")
                    Dim objBAL As New BALCoverPhoto
                    objBAL.CoverPhotoId = CInt(Request.QueryString("cpId"))
                    objBAL.UserId = CInt(Request.QueryString("userId"))
                    objBAL.FBUserId = Request.QueryString("FbuserId")
                    objBAL.FBUserEmail = txtUserEmail.Value
                    objBAL.CoverPhotoName = If(txtCoverPhotoName.Value <> "", txtCoverPhotoName.Value, "")
                    objBAL.CompanyId = CInt(Request.QueryString("CId"))
                    objBAL.IndustryId = CInt(Request.QueryString("IId"))
                    objBAL.CoverPhotoContent = hdnCoverPhotoContent.Value

                    objBAL.UpdateCoverPhotoContent()

                    Dim ds As New DataSet
                    objBAL.CoverPhotoId = CInt(Request.QueryString("cpId"))
                    objBAL.UserId = CInt(Request.QueryString("userId"))
                    objBAL.FBUserId = Request.QueryString("FbuserId")
                    objBAL.CompanyId = CInt(Request.QueryString("CId"))
                    objBAL.IndustryId = CInt(Request.QueryString("IId"))
                    ds = objBAL.GetCoverPhotoMaster

                    If ds.Tables(0).Rows.Count > 0 Then
                        divCoverPhotoHtml.InnerHtml = ds.Tables(0).Rows(0).Item("CoverPhotoContent").Replace("{%domain%}/", ConfigurationManager.AppSettings("AppPath"))
                        Session("CoverPhotoID") = ds.Tables(0).Rows(0).Item("CoverPhotoId")
                    End If

                    Dim siteUrl As String = ConfigurationManager.AppSettings("AppPath") & "generate-cover-image.aspx?id=" & Session("CoverPhotoID")
                    Dim browserWidth As Integer = Convert.ToInt32(800)
                    Dim browserHeight As Integer = Convert.ToInt32(600)
                    Dim thumbnailWidth As Integer = Convert.ToInt32(800)
                    Dim thumbnailHeight As Integer = Convert.ToInt32(600)
                    Dim relativeImagePath As String = ConfigurationManager.AppSettings("uploadpath")
                    Dim fullPath As String = Server.MapPath(relativeImagePath)
                    Dim strExt As String = ""
                    Dim strPhoto As String = ""
                    Dim strDate As Date = "1/1/1900"
                    strPhoto = "CoverPhoto-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & ".jpg"
                    Session("ImageName") = strPhoto
                    Dim strD As [String] = strPhoto
                    If Not fullPath.EndsWith("\") Then
                        fullPath += "\"
                    End If
                    Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteUrl, hdnWidth.Value.Replace("px", ""), hdnHeight.Value.Replace("px", ""), hdnWidth.Value.Replace("px", ""), hdnHeight.Value.Replace("px", ""), fullPath, strD), System.Drawing.Image)

                    objBAL.CoverPhotoId = ViewState("CoverPhotoID")
                    objBAL.FBUserId = ViewState("FBUserId")
                    objBAL.CoverPhotoImage = Session("ImageName")
                    objBAL.UpdateImageName()


                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenProgressbar", "ShowProgress();", True)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseProgressbar", "HideProgress();", True)
                End If
                Response.Redirect("publish-cover-photo-edit.aspx?id=" & Session("CoverPhotoID"))
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub lnkDownload_ServerClick(sender As Object, e As System.EventArgs) Handles lnkDownload.ServerClick
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GetWidthHeight", "GetWidthHeight();", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "DownloadAlert", "DownloadAlert();", True)
                ViewState("CoverPhotoID") = CInt(Request.QueryString("cpId"))
                ViewState("FBUserId") = Request.QueryString("FbuserId")

                Dim objBAL As New BALCoverPhoto

                objBAL.CoverPhotoId = CInt(Request.QueryString("cpId"))
                objBAL.UserId = CInt(Request.QueryString("userId"))
                objBAL.FBUserId = Request.QueryString("FbuserId")
                objBAL.CoverPhotoName = If(txtCoverPhotoName.Value <> "", txtCoverPhotoName.Value, "")
                objBAL.CompanyId = CInt(Request.QueryString("CId"))
                objBAL.IndustryId = CInt(Request.QueryString("IId"))
                objBAL.CoverPhotoContent = hdnCoverPhotoContent.Value
                objBAL.UpdateCoverPhotoContent()
                BindSavedCoverPhoto()

                Dim ds As New DataSet
                objBAL.CoverPhotoId = CInt(Request.QueryString("cpId"))
                objBAL.UserId = CInt(Request.QueryString("userId"))
                objBAL.FBUserId = Request.QueryString("FbuserId")
                objBAL.CompanyId = CInt(Request.QueryString("CId"))
                objBAL.IndustryId = CInt(Request.QueryString("IId"))
                ds = objBAL.GetCoverPhotoMaster

                If ds.Tables(0).Rows.Count > 0 Then
                    divCoverPhotoHtml.InnerHtml = ds.Tables(0).Rows(0).Item("CoverPhotoContent").Replace("{%domain%}/", ConfigurationManager.AppSettings("AppPath"))
                    Session("CoverPhotoID") = ds.Tables(0).Rows(0).Item("CoverPhotoId")
                End If
                Dim siteUrl As String = ConfigurationManager.AppSettings("AppPath") & "generate-cover-image.aspx?id=" & Session("CoverPhotoID")
                Dim browserWidth As Integer = Convert.ToInt32(800)
                Dim browserHeight As Integer = Convert.ToInt32(600)
                Dim thumbnailWidth As Integer = Convert.ToInt32(800)
                Dim thumbnailHeight As Integer = Convert.ToInt32(600)
                Dim relativeImagePath As String = ConfigurationManager.AppSettings("uploadpath")
                Dim fullPath As String = Server.MapPath(relativeImagePath)
                Dim strExt As String = ""
                Dim strPhoto As String = ""
                Dim strDate As Date = "1/1/1900"
                strPhoto = "CoverPhoto-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & ".jpg"
                Session("ImageName") = strPhoto
                Dim strD As [String] = strPhoto
                If Not fullPath.EndsWith("\") Then
                    fullPath += "\"
                End If
                Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteUrl, hdnWidth.Value.Replace("px", ""), hdnHeight.Value.Replace("px", ""), hdnWidth.Value.Replace("px", ""), hdnHeight.Value.Replace("px", ""), fullPath, strD), System.Drawing.Image)

                objBAL.CoverPhotoId = ViewState("CoverPhotoID")
                objBAL.FBUserId = ViewState("FBUserId")
                objBAL.CoverPhotoImage = Session("ImageName")
                objBAL.UpdateImageName()

                Response.Redirect("download.aspx?Image=" & Session("ImageName"))
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub lnkShareUserEmail_ServerClick(sender As Object, e As System.EventArgs) Handles lnkShareUserEmail.ServerClick
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                If hdnPublished.Value = "1" Then
                    ViewState("CoverPhotoID") = CInt(Request.QueryString("cpId"))
                    ViewState("FBUserId") = Request.QueryString("FbuserId")
                    Dim objBAL As New BALCoverPhoto
                    objBAL.CoverPhotoId = CInt(Request.QueryString("cpId"))
                    objBAL.UserId = CInt(Request.QueryString("userId"))
                    objBAL.FBUserId = Request.QueryString("FbuserId")
                    objBAL.CoverPhotoName = If(txtCoverPhotoName.Value <> "", txtCoverPhotoName.Value, "")
                    objBAL.FBUserEmail = txtUserEmail.Value
                    objBAL.CompanyId = CInt(Request.QueryString("CId"))
                    objBAL.IndustryId = CInt(Request.QueryString("IId"))
                    objBAL.CoverPhotoContent = hdnCoverPhotoContent.Value

                    objBAL.UpdateCoverPhotoContent()

                    BindSavedCoverPhoto()

                    'Dim ds As New DataSet
                    'objBAL.CoverPhotoId = CInt(Request.QueryString("cpId"))
                    'objBAL.UserId = CInt(Request.QueryString("userId"))
                    'objBAL.FBUserId = Request.QueryString("FbuserId")
                    'objBAL.CompanyId = CInt(Request.QueryString("CId"))
                    'objBAL.IndustryId = CInt(Request.QueryString("IId"))
                    'ds = objBAL.GetCoverPhotoMaster

                    'If ds.Tables(0).Rows.Count > 0 Then
                    '    divCoverPhotoHtml.InnerHtml = ds.Tables(0).Rows(0).Item("CoverPhotoContent").Replace("{%domain%}/", ConfigurationManager.AppSettings("AppPath"))
                    '    Session("CoverPhotoID") = ds.Tables(0).Rows(0).Item("CoverPhotoId")
                    'End If

                    Dim siteUrl As String = ConfigurationManager.AppSettings("AppPath") & "generate-cover-image.aspx?id=" & Session("CoverPhotoID")
                    Dim browserWidth As Integer = Convert.ToInt32(800)
                    Dim browserHeight As Integer = Convert.ToInt32(600)
                    Dim thumbnailWidth As Integer = Convert.ToInt32(800)
                    Dim thumbnailHeight As Integer = Convert.ToInt32(600)
                    Dim relativeImagePath As String = ConfigurationManager.AppSettings("uploadpath")
                    Dim fullPath As String = Server.MapPath(relativeImagePath)
                    Dim strExt As String = ""
                    Dim strPhoto As String = ""
                    Dim strDate As Date = "1/1/1900"
                    strPhoto = "CoverPhoto-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & ".jpg"
                    Session("ImageName") = strPhoto
                    Dim strD As [String] = strPhoto
                    If Not fullPath.EndsWith("\") Then
                        fullPath += "\"
                    End If
                    Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteUrl, hdnWidth.Value.Replace("px", ""), hdnHeight.Value.Replace("px", ""), hdnWidth.Value.Replace("px", ""), hdnHeight.Value.Replace("px", ""), fullPath, strD), System.Drawing.Image)

                    objBAL.CoverPhotoId = ViewState("CoverPhotoID")
                    objBAL.FBUserId = ViewState("FBUserId")
                    objBAL.CoverPhotoImage = Session("ImageName")
                    objBAL.UpdateImageName()

                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert('Cover Photo Shared successfully');", True)
                'Response.Redirect("publish-cover-photo-edit.aspx?id=" & Session("CoverPhotoID"))
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

End Class