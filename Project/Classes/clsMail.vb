Imports System.Net.Mail
Public Class clsMail
    Public Function SendExternalEmail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strMailBody As String, ByVal strcc As String, ByVal strbcc As String)
        Dim mailMessage As MailMessage = New MailMessage()
        mailMessage.From = New MailAddress(strFrom)
        mailMessage.To.Add(strTo)

        If strcc <> "" Then
            mailMessage.CC.Add(New MailAddress(strcc))
        End If

        If strbcc <> "" Then
            mailMessage.Bcc.Add(New MailAddress(strbcc))
        End If
        mailMessage.Subject = strSubject
        mailMessage.Body = strMailBody
        mailMessage.IsBodyHtml = True
        'Dim networkCredentials As New System.Net.NetworkCredential("techsture.devlopers@gmail.com", "chrdjbar19")
        'Dim smtpClient As New SmtpClient("smtp.gmail.com")
        Dim networkCredentials As New System.Net.NetworkCredential("postmaster@techsturedevelopment.mailgun.org", "testmailgun")
        Dim smtpClient As New SmtpClient("smtp.mailgun.org")
        smtpClient.UseDefaultCredentials = False
        smtpClient.Credentials = networkCredentials
        smtpClient.EnableSsl = True
        smtpClient.Port = 587 '25
        smtpClient.Send(mailMessage)
    End Function
End Class
