Public Class rajni_gimages
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.ServerClick
        'Dim siteUrl As String = ConfigurationManager.AppSettings("AppPath") & "rajni-generateimage.aspx?id=1"
        'Dim browserWidth As Integer = Convert.ToInt32(800)
        'Dim browserHeight As Integer = Convert.ToInt32(600)
        'Dim thumbnailWidth As Integer = Convert.ToInt32(800)
        'Dim thumbnailHeight As Integer = Convert.ToInt32(600)
        'Dim relativeImagePath As String = ConfigurationManager.AppSettings("uploadpath")
        'Dim fullPath As String = Server.MapPath(relativeImagePath)
        'Response.Write(fullPath)
        'Response.End()
        'Dim strExt As String = ""
        Dim strPhoto As String = ""
        Dim strDate As Date = "1/1/1900"
        strPhoto = "old-customtab-" & CStr(DateDiff(DateInterval.Second, strDate, Now())) & ".jpg"

        'Dim strD As String = strPhoto
        'If Not fullPath.EndsWith("\") Then
        '    fullPath += "\"
        'End If

        ''Try
        Dim img As System.Drawing.Image = DirectCast(PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail("http://192.168.19.28:5069/rajni-generateimage.aspx?id=1", 520, 1000, 320, 740, "D:\Projects\tsmaOutBreak\Project\uploads\", strPhoto), System.Drawing.Image)

    End Sub
End Class