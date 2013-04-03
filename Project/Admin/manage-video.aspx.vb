Imports BusinessAccessLayer.BusinessLayer
Public Class manage_video
    Inherits System.Web.UI.Page
    Dim objVideo As New videos

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindCompanies()
            BindIndustries()
            ViewState("video") = ""
            BindVideoCatagory()
            BindGrid()
            If Me.GetVideoId() > 0 Then
                LoadVideo()
            End If
        End If
    End Sub

    Sub BindCompanies()
        Dim objcomp As New BALAdminLibrary
        Dim ds As New DataSet
        ds = objcomp.BindCompanies
        If ds.Tables(0).Rows.Count > 0 Then
            chkCompany.DataSource = ds.Tables(0)
            chkCompany.DataBind()
        Else
            chkCompany.DataSource = Nothing
            chkCompany.DataBind()
        End If
        'drpCompany.Items.Insert(0, New ListItem("-- Select Company --", "0"))
    End Sub

    Sub BindIndustries()
        Dim objcomp As New BALAdminLibrary

        Dim ds As New DataSet
        ds = objcomp.BindIndustries()
        If ds.Tables(0).Rows.Count > 0 Then
            chkIndustry.DataSource = ds.Tables(0)
            chkIndustry.DataBind()
        Else
            chkIndustry.DataSource = Nothing
            chkIndustry.DataBind()
        End If
        'drpIndustry.Items.Insert(0, New ListItem("-- Select Industry --", "0"))
    End Sub

#Region "Bind Video Catagory"
    Sub BindVideoCatagory()
        Try
            Dim ds As New DataSet
            ds = objVideo.BindVideosCatagory()
            If ds.Tables.Count > 0 Then
                If (ds.Tables(0).Rows.Count > 0) Then
                    cmbCatagory.DataSource = ds.Tables(0)
                    cmbCatagory.DataTextField = "vcat_Name"
                    cmbCatagory.DataValueField = "vcat_Id"
                    cmbCatagory.DataBind()
                Else
                    cmbCatagory.DataSource = Nothing
                    cmbCatagory.DataBind()
                End If
                cmbCatagory.Items.Insert("0", New ListItem("Select Category", "0"))
            End If
            ds = Nothing
        Catch ex As Exception
            ltrMsg.Text = "Error: " & ex.Message
        End Try
    End Sub
#End Region

    Sub SaveVideo()
        'Try

        Dim intStatus As Integer = 1, intType As Integer = 1
        If rdoInactive.Checked = True Then
            intStatus = 0
        End If

        Dim strExt As String = "", strVideoThumb As String = "", strVideo As String = ""
        Dim strVideoFileNameWithoutExtension As String = ""
        'If rdoEmbed.Checked = True Then

        '    intType = 2
        '    strVideo = Replace(Trim(txtEmbedCode.Value), "'", "''")
        'Else
        '    If flVideo.PostedFile.ContentLength > 0 Then
        '        strExt = IO.Path.GetExtension(flVideo.PostedFile.FileName).ToLower
        '        If strExt = ".rm" Or strExt = ".wma" Or strExt = ".mp3" Or strExt = ".mov" Or strExt = ".wav" Or strExt = ".wmv" Or strExt = ".mpg" Or strExt = ".avi" Or strExt = ".flv" Then
        '            strVideoFileNameWithoutExtension = "V-" & String.Format("{0:MMddyyyyhhmmsstt}", Now)
        '            If strExt <> ".flv" Then
        '                flVideo.PostedFile.SaveAs(Server.MapPath("~" & ConfigurationManager.AppSettings("ProductVideoPath") & "temp") & "\" & strVideoFileNameWithoutExtension & strExt)
        '                Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process
        '                myprocess.StartInfo.UseShellExecute = True
        '                myprocess.StartInfo.FileName = Server.MapPath("~\VideoBlog.bat")
        '                myprocess.StartInfo.WorkingDirectory = Server.MapPath("~\")
        '                myprocess.StartInfo.Arguments = " " & strVideoFileNameWithoutExtension & strExt & " -s -m"
        '                myprocess.Start()
        '            Else
        '                flVideo.PostedFile.SaveAs(Server.MapPath("~" & ConfigurationManager.AppSettings("ProductVideoPath")) & strVideoFileNameWithoutExtension & ".flv")
        '                Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process
        '                myprocess.StartInfo.UseShellExecute = True
        '                myprocess.StartInfo.FileName = Server.MapPath("~\VideoBlog-flv.bat")
        '                myprocess.StartInfo.WorkingDirectory = Server.MapPath("~\")
        '                myprocess.StartInfo.Arguments = " " & strVideoFileNameWithoutExtension & ".flv" & " -s -m"
        '                myprocess.Start()
        '            End If
        '            strVideoThumb = strVideoFileNameWithoutExtension & ".jpg"
        '            strVideo = strVideoFileNameWithoutExtension & ".flv"
        '            deleteVideo(Server.MapPath(ViewState("video").ToString.Replace(ConfigurationManager.AppSettings("AppPath"), "")))
        '            deleteVideo(Server.MapPath(ViewState("video").ToString.Replace("/videos/", "/videos/thumbnails/").Replace(".flv", ".jpg").Replace(ConfigurationManager.AppSettings("AppPath"), "")))
        '            ltrMsg.Text = ViewState("video")
        '        Else
        '            ltrMsg.Text = "File must be a video file!"
        '            Exit Sub
        '        End If
        '    Else
        '        If (rdoUpload.Checked = True) Then
        '            If (ViewState("video") <> "") Then
        '                strVideo = ViewState("video").ToString
        '                strVideoThumb = ViewState("video").ToString.Replace(".flv", ".jpg")
        '            End If
        '        End If
        '    End If
        'End If

        objVideo.VideoId = Me.GetVideoId()
        objVideo.VideoTitle = txtTitle.Value.Trim()
        objVideo.CatId = cmbCatagory.SelectedIndex
        'objVideo.VideoCode = strVideoob
        objVideo.CompanySelectedID = If(GetCompanyString() <> "", GetCompanyString(), "")
        objVideo.IndustrySelectedID = If(GetIndustryString() <> "", GetIndustryString(), "")
        objVideo.VideoEmbededCode = txtEmbedCode.Value
        objVideo.Status = intStatus
        objVideo.VideoImage = strVideoThumb
        objVideo.VideoType = intType
        objVideo.AddEditVideo()
        BindGrid()
        If objVideo.Result = "1" Then
            ltrMsg.Text = "Video Added Successfully."
            ClearData()
        ElseIf objVideo.Result = "2" Then
            ltrMsg.Text = "Video Edited Successfully."
            ClearData()
        End If
    End Sub
    
    Sub ClearData()
        txtTitle.Value = ""
        txtEmbedCode.Value = ""
        'rdoUpload.Checked = True
        'trUploadVideo.Style.Add("display", "")
        'trEmbedVideo.Style.Add("display", "none")
        'rdoEmbed.Checked = False
        'rdoActive.Checked = True
        'rdoInactive.Checked = False
        cmbCatagory.Value = 0 '
    End Sub
    Protected Sub DeleteVideoById(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            objVideo.VideoId = e.CommandArgument
            'ltrMsg.Text = e.CommandArgument
            objVideo.DeleteVideo()
            BindGrid()
            ltrMsg.Text = "Video deleted successfully."
        Catch ex As Exception
            ltrMsg.Text = "Error! " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ChangeStatus(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            objVideo.Status = e.CommandArgument
            objVideo.VideoId = e.CommandName
            'ltrMsg.Text = objVideo.VideoId
            objVideo.ChangeVideoStatus()
            BindGrid()
        Catch ex As Exception
            ltrMsg.Text = "Error! " & ex.Message.ToString
        End Try
    End Sub
    

#Region "BindGrid"
    Sub BindGrid()
        Try
            Dim ds As DataSet
            Dim intStatus As Integer
            'objVideo.PageIndex = objPaging.CurrentPage
            ds = objVideo.GetVideos()
            If ds.Tables(0).Rows.Count > 0 Then
                dgrVideo.DataSource = ds.Tables(0)
                'spnVideoEmbed.InnerHtml = objVideo.VideoEmbededCode
                dgrVideo.DataBind()
                pnldata.Visible = True
                intStatus = objVideo.Status
                If (objVideo.Status = 1) Then
                    rdoActive.Checked = True
                    rdoInactive.Checked = False
                End If
                If (objVideo.Status = 0) Then
                    rdoActive.Checked = False
                    rdoInactive.Checked = True
                End If
            Else
                dgrVideo.DataSource = Nothing
                dgrVideo.DataBind()
                pnldata.Visible = False
                ltrMsg.Text = "No Data Found"
            End If

            'If ds.Tables(1).Rows.Count > 0 Then
            '    objPaging.TotalPages = ds.Tables(1).Rows(0).Item("TotalPage").ToString
            '    objPaging.TotalRecords = ds.Tables(1).Rows(0).Item("TotalRec").ToString
            'End If
            'objPaging.ResetControls()
            'objPaging.GenerateControls()
            ds = Nothing
        Catch ex As Exception
            ltrMsg.Text = "Error! " & ex.Message.ToString
        End Try
    End Sub
#End Region

    Function GetCompanyString() As String
        'Dim strString As String = ""
        Dim strSelected As String = ""
        If rdoComp.Checked = True Then
            'Dim i As Integer
            'Dim chkbx As CheckBoxList
            'chkbx = CType(frm.FindControl("chkCompany"), CheckBoxList)
            'Dim sb As StringBuilder = New StringBuilder()
            'For i = 0 To chkbx.Items.Count - 1
            '    If chkbx.Items(i).Selected Then
            '        sb.Append(chkbx.Items(i).Value & ",")
            '        strString = sb.ToString()
            '    End If
            'Next


            Dim strAll As String = ""
            Dim chlSelected As HtmlInputCheckBox
            For Each item As DataListItem In chkCompany.Items
                chlSelected = CType(item.FindControl("chkSelected"), HtmlInputCheckBox)
                strAll = strAll & chlSelected.Attributes("comid") & ","
                If chlSelected.Checked = True Then
                    strSelected = strSelected & chlSelected.Attributes("comid") & ","
                End If
            Next
            If strAll.Length > 0 Then
                strAll = Left(strAll, strAll.Length - 1)
            End If
            If strSelected.Length > 0 Then
                strSelected = Left(strSelected, strSelected.Length - 1)
            End If

        End If

        Return strSelected
    End Function

    Function GetIndustryString() As String
        Dim strSelected As String = ""
        If rdoInd.Checked = True Then

            Dim strAll As String = ""
            Dim chlSelected As HtmlInputCheckBox
            For Each item As DataListItem In chkIndustry.Items
                chlSelected = CType(item.FindControl("chkSelected"), HtmlInputCheckBox)
                strAll = strAll & chlSelected.Attributes("indid").ToString & ","
                If chlSelected.Checked = True Then
                    strSelected = strSelected & chlSelected.Attributes("indid").ToString & ","
                End If
            Next
            If strAll.Length > 0 Then
                strAll = Left(strAll, strAll.Length - 1)
            End If
            If strSelected.Length > 0 Then
                strSelected = Left(strSelected, strSelected.Length - 1)
            End If

            'Dim i As Integer
            'Dim chkbx As CheckBoxList
            'chkbx = CType(frm.FindControl("chkIndustry"), CheckBoxList)
            'Dim sb As StringBuilder = New StringBuilder()
            'For i = 0 To chkbx.Items.Count - 1
            '    If chkbx.Items(i).Selected Then
            '        sb.Append(chkbx.Items(i).Value & ",")
            '        strString = sb.ToString()
            '    End If
            'Next
        End If
        Return strSelected
    End Function


    #Region "Load Video"
    Sub LoadVideo()
        Dim intStatus As Integer
        Try
            objVideo.VideoId = Me.GetVideoId()
            objVideo.GetVideoDetailsById()
            txtTitle.Value = objVideo.VideoTitle
            txtEmbedCode.Value = objVideo.VideoEmbededCode
            cmbCatagory.Value = objVideo.CatId
            intStatus = objVideo.Status
            If (objVideo.CompanyId <> 0) Then
                rdoComp.Checked = True
                rdoInd.Checked = False
                'rdoAll.Checked = False
                trcomp.Visible = True
                trind.Visible = False
                ' drpCompany.SelectedIndex = objVideo.CompanyId
                'ElseIf (objVideo.IndustryId <> 0) Then
                '    rdoInd.Checked = True
                '    rdoAll.Checked = False
                '    rdoComp.Checked = False
                '    drpIndustry.SelectedIndex = objVideo.IndustryId
                'Else
                '    rdoAll.Checked = True
                '    rdoInd.Checked = False
                '    rdoComp.Checked = False
            End If
            If (objVideo.IndustryId <> 0) Then
                rdoInd.Checked = True
                trcomp.Visible = False
                trind.Visible = True
                'rdoAll.Checked = False
                rdoComp.Checked = False
                'drpIndustry.SelectedIndex = objVideo.IndustryId
            End If
            If (objVideo.AllCompanyIndustryId <> 0) Then
                'rdoAll.Checked = True
                trcomp.Visible = False
                trind.Visible = False
                rdoInd.Checked = False
                rdoComp.Checked = False
            End If
            If (objVideo.Status = 1) Then
                rdoActive.Checked = True
                rdoInactive.Checked = False
            End If
            If (objVideo.Status = 0) Then
                rdoActive.Checked = False
                rdoInactive.Checked = True
            End If
            'If intStatus = 0 Then
            '    rdoInactive.Checked = True
            '    If intStatus = 1 Then
            'rdoActive.Checked = True
            '    End If
            'End If

            'objVideo.VideoId = Me.GetVideoId()
            'objVideo.VideoTitle = txtTitle.Value
            'objVideo.VideoEmbededCode = txtEmbedCode.Value
            'objVideo.CatId = cmbCatagory.Value

            'If rdoActive.Checked = True Then
            '    intStatus = 1
            'Else
            '    intStatus = 0
            '    objVideo.Status = intStatus
            'End If
            'objVideo.UpdateVideo()
        Catch ex As Exception
            ltrMsg.Text = "Error: " & ex.Message.ToString
        End Try
    End Sub
    #End Region


    'Sub SetVideoType()
    '    If (rdoUpload.Checked = True) Then
    '        trEmbedVideo.Style.Add("display", "none")
    '        trUploadVideo.Style.Add("display", "")
    '    Else
    '        trEmbedVideo.Style.Add("display", "")
    '        trUploadVideo.Style.Add("display", "none")
    '    End If
    'End Sub


#Region "Get Video ID"
    Function GetVideoId() As Integer
        Return IIf(IsNumeric(Request("id")), Request("id"), 0)
    End Function
#End Region

#Region "deleteVideo"
    Sub deleteVideo(ByVal path As String)
        If path <> "" Then
            If System.IO.File.Exists(path) Then
                System.IO.File.Delete(path)
            End If
        End If
    End Sub
#End Region

    Private Sub btnSave_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.ServerClick
        SaveVideo()
        BindGrid()
    End Sub

End Class