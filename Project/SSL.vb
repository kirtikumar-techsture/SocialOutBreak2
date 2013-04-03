Public Class SSL
    Shared Function GetSSLUrl(ByVal strUrl As String)
        If strUrl.IndexOf("http://") <> -1 Then
            Return strUrl.Replace("http://", "https://")
        ElseIf strUrl.IndexOf("https://") <> -1 Then
            Return strUrl
        ElseIf strUrl.IndexOf("http://") = -1 AndAlso strUrl.IndexOf("https://") = -1 Then
            Return "https://" & strUrl
        End If
        Return strUrl
    End Function
End Class
