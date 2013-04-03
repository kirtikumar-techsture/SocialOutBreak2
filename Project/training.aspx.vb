Imports BusinessAccessLayer.BusinessLayer
Public Class training
    Inherits System.Web.UI.Page
    Dim objVideo As New videos
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write(Request.IsSecureConnection)
        Dim obj As New BAlsidebar
        Dim ds1 As New DataSet
        obj.Page = CStr(HttpContext.Current.Request.Url.AbsolutePath)
        If Not Page.IsPostBack Then
            '    If Request.IsSecureConnection Then
            '        Dim urlb = New UriBuilder(Request.Url)
            '        urlb.Scheme = Uri.UriSchemeHttp
            '        urlb.Port = -1
            '        ' use default port for scheme
            '        Response.Redirect(urlb.Uri.ToString(), True)
            '        Return
            '    Else
            '        Response.Write("SdafsdfDS")
            BindVideosCategories(0)
            BindDefaultVideos()
            ds1 = obj.GetVideoTutorial()
            If HttpContext.Current.Request.Url.AbsolutePath = ds1.Tables(0).Rows(0).Item("vt_Page").ToString Then
                btnVideo.HRef = ds1.Tables(0).Rows(0).Item("vt_Video").ToString & "?fs=1&autoplay=1&rel=0&enablejsapi=1&playerapiid=ytplayer"
                btnVideo.Title = ds1.Tables(0).Rows(0).Item("vt_Title").ToString
            End If
        End If
    End Sub
    Function BindVideosCategories(ByVal CatId As Integer)
        Dim ds As DataSet
        objVideo.VideoId = CatId
        objVideo.CompanyId = GetSetCookies.GetCookie("CompanyId")
        objVideo.IndustryId = GetSetCookies.GetCookie("IndustryId")
        ds = objVideo.GetTrainingVideoByCategoryId()
        'If RowId = 1 Then
        If ds.Tables(0).Rows.Count > 0 Then
            rptCategory.DataSource = ds.Tables(0)
            rptCategory.DataBind()
        Else
            rptCategory.DataSource = Nothing
            rptCategory.DataBind()
        End If
        'End If
        'Return ds

    End Function

    Public Function BindAllVideos(ByVal intCatID As Integer) As DataSet
        Dim ds As DataSet
        objVideo.CatId = intCatID
        objVideo.CompanyId = GetSetCookies.GetCookie("CompanyId")
        objVideo.IndustryId = GetSetCookies.GetCookie("IndustryId")
        ds = objVideo.BindALlCategories()
        Return ds
    End Function

    Sub imgVideoThumb_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim ds As DataSet
        objVideo.VideoId = e.CommandArgument
        objVideo.CompanyId = GetSetCookies.GetCookie("CompanyId")
        objVideo.IndustryId = GetSetCookies.GetCookie("IndustryId")
        ds = objVideo.GetTrainingVideo()
        If ds.Tables(0).Rows.Count > 0 Then
            With ds.Tables(0).Rows(0)
                spnVideoEmbed.InnerHtml = .Item("vd_EmbededCode").ToString()
                spanTitle.InnerHtml = .Item("vd_Title").ToString
            End With
        End If
        ds = Nothing
    End Sub

    Public Function BindDefaultVideos()
        Dim ds As DataSet
        objVideo.CompanyId = GetSetCookies.GetCookie("CompanyId")
        objVideo.IndustryId = GetSetCookies.GetCookie("IndustryId")
        ds = objVideo.GetDefaultVideo()
        If ds.Tables(0).Rows.Count > 0 Then
            With ds.Tables(0).Rows(0)
                spnVideoEmbed.InnerHtml = .Item("vd_EmbededCode").ToString()
                spanTitle.InnerHtml = .Item("vd_Title").ToString
            End With
        End If
        ds = Nothing
    End Function

End Class
