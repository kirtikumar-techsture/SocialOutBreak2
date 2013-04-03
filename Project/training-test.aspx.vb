Imports BusinessAccessLayer.BusinessLayer
Public Class training_test
    Inherits System.Web.UI.Page
    Dim objVideo As New videos
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BindVideosCategories(0)
        BindDefaultVideos()
    End Sub
    Function BindVideosCategories(ByVal CatId As Integer)
        Dim ds As DataSet
        objVideo.VideoId = CatId
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
        ds = objVideo.BindALlCategories()
        Return ds
    End Function

    Sub imgVideoThumb_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim ds As DataSet
        objVideo.VideoId = e.CommandArgument
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
