Imports System.Net
Imports System.Runtime.Serialization.Json
Imports BusinessAccessLayer.BusinessLayer
Imports System.IO
Imports Facebook
Imports System.Threading
Imports System.Net.Mail
Public Class ajax_sweepstak_contest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write("Name" & Request("nm"))
        'Response.Write("Email" & Request("em"))
        'Response.Write(Session("FacebookUserId"))
        'Response.Write(Session("AppUserId"))
        'Response.Write(Session("FBPID"))
        'SendExternalEmail("postmaster@techsturedevelopment.mailgun.org", Request("em"), Request("fbpagename") & "- Sweepstakes Entry", "Thank you for entering our sweepstakes on our Facebook Business page!  Our sweepstakes platform draws a winner the 15th of every month.  If you did not win, we automatically put you back in the sweepstakes for the next month.  We hope you win! <br><br>Please visit our community frequently and let us know how you are enjoying our product and/or service.<br><br><br>All the best,<br>" & Request("fbpagename"), "", "")
        If CInt(Request("swid")) = 3 Then
            SendExternalEmail("postmaster@no-reply.com", Request("em"), "Sweepstakes Entry", "<br> Thank you for entering our Vision Products Sweepstakes on our Facebook Business page! <br><br> Our sweepstakes platform draws a winner the 4th of December, 2012. <br><br>We hope you win!", "", "")
        Else
            SendExternalEmail("postmaster@no-reply.com", Request("em"), "Sweepstakes Entry", "Thank you for entering our sweepstakes on our Facebook Business page!  Our sweepstakes platform draws a winner the 15th of every month.<br>  If you did not win, we automatically put you back in the sweepstakes for the next month. <br><br> We hope you win! ", "", "")
        End If


        Dim objsweepstakecontest As New BALSweepStake
        objsweepstakecontest.Name = Request("nm")
        objsweepstakecontest.Email = Request("em")
        objsweepstakecontest.FBUserId = Request("fbuid")
        'objsweepstakecontest.TSMAUserId = Request("TSMAUserId")
        objsweepstakecontest.FBPageId = Request("fbpid")
        objsweepstakecontest.FBAppId = Request("appid")
        objsweepstakecontest.FBAppName = Request("appname")
        objsweepstakecontest.AddSweepStakeContest()

    End Sub
#Region "Send External Email"
    Sub SendExternalEmail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strMailBody As String, ByVal strcc As String, ByVal strbcc As String)

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


        ' Dim networkCredentials As New System.Net.NetworkCredential("techsture.devlopers@gmail.com", "chrdjbar19")
        'Dim smtpClient As New SmtpClient("smtp.gmail.com")
        Dim networkCredentials As New System.Net.NetworkCredential("postmaster@techsturedevelopment.mailgun.org", "testmailgun")
        Dim smtpClient As New SmtpClient("smtp.mailgun.org")
        smtpClient.UseDefaultCredentials = False

        smtpClient.Credentials = networkCredentials
        smtpClient.EnableSsl = True
        smtpClient.Port = 587 '25

        smtpClient.Send(mailMessage)
        ' Response.Write("Mail Successfully sent")

    End Sub
#End Region

End Class