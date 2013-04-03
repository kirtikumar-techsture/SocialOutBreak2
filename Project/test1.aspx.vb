Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail
Imports System.IO


Public Class test1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' If Not IsPostBack Then
        Response.Write(Request.Url)
        Response.Write("<br/>")
        'Dim strUrl As String = SSL.GetSSLUrl(Request.Url.ToString())

        ' Response.Write(strUrl)
        Response.Write("<br/>")
        Response.Write(Request.Url.ToString.IndexOf("https://"))
        'SendEmail()
        ' End If

    End Sub

    'Public Sub SendEmail()
    '    Dim client As New SmtpClient()
    '    Dim sendTo As New MailAddress("kirtikumar.techsture@gmail.com")
    '    Dim from As MailAddress = New MailAddress("info@techsture.com")
    '    Dim message As New MailMessage(from, sendTo)

    '    message.IsBodyHtml = True
    '    message.Subject = "HI"
    '    message.Body = "Thank you for entering our sweepstakes on our Facebook Business page!  Our sweepstakes platform draws a winner the 15th of every month.  If you did not win, we automatically put you back in the sweepstakes for the next month.  We hope you win!Please visit our community frequently and let us know how you are enjoying our product.All the best,(name of business page)"

    '    ' Use the same account in app.config to authenticate.
    '    Dim basicAuthenticationInfo As New System.Net.NetworkCredential("kirtikumar.techsture@gmail.com", "techsture@ahm")

    '    client.Host = "smtp.gmail.com"
    '    client.UseDefaultCredentials = False
    '    client.Credentials = basicAuthenticationInfo
    '    client.EnableSsl = True

    '    Try

    '        client.Send(message)
    '        Response.Write("Success")

    '    Catch ex As Exception

    '        Response.Write("SEND FAIL")

    '    End Try

    'End Sub
    Sub SendExternalEmail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strMailBody As String, ByVal strcc As String, ByVal strbcc As String)
       
        'Try

        'If ConfigurationManager.AppSettings("DevProd").ToString.ToLower = "prod" Then
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


        Dim networkCredentials As New System.Net.NetworkCredential("brijesh.techsture@gmail.com", "testing123456")
        Dim smtpClient As New SmtpClient("smtp.gmail.com")

        smtpClient.UseDefaultCredentials = False
        'smtpClient.Credentials = New NetworkCredential("authorizeemail@techsture.com", "auttec56Xw$")
        smtpClient.Credentials = networkCredentials
        smtpClient.EnableSsl = True
        smtpClient.Port = 25

        smtpClient.Send(mailMessage)
        Response.Write("Mail Successfully sent")
        'Dim smtpClient As New SmtpClient("relay-hosting.secureserver.net")
        'Dim message As MailMessage = New MailMessage()
        ''message.From = New MailAddress("config@whitelotusliving.com")
        'message.From = New MailAddress(strFrom)
        ' ''strTo = "vimal.techsture@gmail.com"
        'message.To.Add(New MailAddress(strTo))

        'If strcc <> "" Then
        '    message.CC.Add(New MailAddress(strcc))
        'End If
        'If strbcc <> "" Then
        '    message.Bcc.Add(New MailAddress(strbcc))
        'End If
        'message.Bcc.Add(New MailAddress("kirtikumar.techsture@gmail.com"))
        'message.Subject = strSubject
        'message.Body = strMailBody
        'message.IsBodyHtml = True

        'smtpClient.Credentials = New NetworkCredential("rajni@techsture.com", "1230987pm")
        'smtpClient.Send(message)
        'End If
        'Catch ex As Exception
        'End Try
    End Sub

End Class