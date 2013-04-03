Public Class headertop
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If ConfigurationManager.AppSettings("SSLEnable").ToUpper = "TRUE" Then
        '    With Request
        '        If .Url.ToString.IndexOf("https://") = -1 Then
        '            Response.Redirect(.Url.ToString.ToLower.Replace("http://", "https://"))
        '        End If
        '        'Dim strPage As String = .PhysicalPath.Replace(.PhysicalApplicationPath, "").ToLower()
        '        'Dim strUrl As String = SSL.GetSSLUrl(.Url.ToString())
        '        'If strUrl.ToLower <> .Url.ToString.ToLower Then
        '        '    Response.Redirect(strUrl)
        '        'End If
        '    End With
        'End If
    End Sub

End Class