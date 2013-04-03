Imports System.Data
Imports System.Net.Mail
Imports DataAccessLayer.DataAccessLayer
Imports System.Text

Public Class SendLeadEmail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SendSWLeads()
        SendCTLeads()
    End Sub

#Region "Sweepstakes Leads Data For mail"
    Sub SendSWLeads()
        Dim dalayers As New DALDataAccess
        Dim dsSw As New DataSet
        dalayers.AddCommand(CommandType.StoredProcedure, "prc_GetEmailForSWCTLeads")
        dsSw = dalayers.GetDataset

        If dsSw.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To dsSw.Tables(0).Rows.Count - 1
                Try
                    Dim strDate1 As DateTime
                    Dim strdate As String
                    If dsSw.Tables(0).Rows(i).Item("sc_Date").ToString <> "--" Then
                        strDate1 = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dsSw.Tables(0).Rows(i).Item("sc_Date").ToString, ConfigurationManager.AppSettings("ServerTimeZone"), "Pacific Standard Time").ToString
                        strdate = strDate1
                    Else
                        strdate = "--"
                    End If

                    Dim strMailBody As New StringBuilder
                    strMailBody.Append("Hi there,<br/><br/>")
                    strMailBody.Append("A Facebook Fan has asked you to contact him/her. Below are the details entered from your Facebook Business Page:<br/><br/>")
                    strMailBody.Append("<strong>Name:</strong>&nbsp;" & dsSw.Tables(0).Rows(i).Item("sc_Name").ToString & "<br/>")
                    strMailBody.Append("<strong>Email:</strong>&nbsp;" & dsSw.Tables(0).Rows(i).Item("sc_Email").ToString & "<br/>")
                    strMailBody.Append("<strong>Phone:</strong>&nbsp;<br/>")
                    strMailBody.Append("<strong>Message:</strong>&nbsp;<br/>")
                    strMailBody.Append("<strong>Lead From:</strong>&nbsp; Sweepstakes<br/>")
                    strMailBody.Append("<strong>Date + (PST):</strong>&nbsp;" & strdate & "<br/><br/>")
                    If ConfigurationManager.AppSettings("AppPath") = "https://so.tsmaapplication.com/" Then
                        strMailBody.Append("Also, all of your leads can be found within your Social Outbreak Dashboard. {Access https://so.tsmaapplication.com and login; then see Convert > Leads where you can export all your leads as a CSV file.} Follow-up by sending specific information or by just saying: THANK YOU FOR YOUR INTEREST ... here's how I can help you.")
                    Else
                        strMailBody.Append("Also, all of your leads can be found within your Summa Social Dashboard. {Access https://www.summasocialapp.com and login; then see Convert > Leads where you can export all your leads as a CSV file.} Follow-up by sending specific information or by just saying: THANK YOU FOR YOUR INTEREST ... here's how I can help you.")
                    End If


                    Dim dalayers1 As New DALDataAccess
                    Dim dsSwpages As New DataSet
                    dalayers1.AddCommand(CommandType.StoredProcedure, "prc_GetEmailForSWLeadsPages")
                    dalayers1.AddParam("@swpageid", SqlDbType.VarChar, dsSw.Tables(0).Rows(i).Item("sc_FBPageId").ToString)
                    dsSwpages = dalayers1.GetDataset

                    SendExternalEmail("postmaster@no-reply.com", dsSwpages.Tables(0).Rows(0).Item("scp_NotificationMail").ToString, "Sweepstakes Entry", strMailBody.ToString, "", "")

                    Dim dalUpdate As New DALDataAccess
                    dalUpdate.AddCommand(CommandType.StoredProcedure, "prc_UpdateSWLeadsData")
                    dalUpdate.AddParam("@swleadid", SqlDbType.Int, dsSw.Tables(0).Rows(i).Item("sc_id"))
                    dalUpdate.AddParam("@swpageid", SqlDbType.VarChar, dsSw.Tables(0).Rows(i).Item("sc_FBPageId"))
                    dalUpdate.ExecuteNonQuery()
                Catch ex As Exception
                    InsertErrorLogSWCT(dsSw.Tables(0).Rows(i).Item("sc_id"), 0, ex.Message.ToString)
                End Try
            Next
        End If

    End Sub
#End Region

#Region "Sweepstakes Leads Data For mail"
    Sub SendCTLeads()
        Dim dalayers As New DALDataAccess
        Dim dsCT As New DataSet
        dalayers.AddCommand(CommandType.StoredProcedure, "prc_GetEmailForSWCTLeads")
        dsCT = dalayers.GetDataset

        If dsCT.Tables(1).Rows.Count > 0 Then
            For i As Integer = 0 To dsCT.Tables(1).Rows.Count - 1
                Try
                    Dim strDate1 As DateTime
                    Dim strdate As String
                    If dsCT.Tables(1).Rows(i).Item("cti_Date").ToString <> "--" Then
                        strDate1 = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dsCT.Tables(1).Rows(i).Item("cti_Date").ToString, ConfigurationManager.AppSettings("ServerTimeZone"), "Pacific Standard Time").ToString
                        strdate = strDate1
                    Else
                        strdate = "--"
                    End If

                    Dim strMailBody As New StringBuilder
                    strMailBody.Append("Hi there,<br/><br/>")
                    strMailBody.Append("A Facebook Fan has asked you to contact him/her. Below are the details entered from your Facebook Business Page:<br/><br/>")
                    strMailBody.Append("<strong>Name:</strong>&nbsp;" & dsCT.Tables(1).Rows(i).Item("cti_Name").ToString & "<br/>")
                    strMailBody.Append("<strong>Email:</strong>&nbsp;" & dsCT.Tables(1).Rows(i).Item("cti_Email").ToString & "<br/>")
                    strMailBody.Append("<strong>Phone:</strong>&nbsp;" & dsCT.Tables(1).Rows(i).Item("cti_Phone").ToString & "<br/>")
                    strMailBody.Append("<strong>Message:</strong>&nbsp;" & dsCT.Tables(1).Rows(i).Item("cti_Message").ToString.Replace("<br/>", Chr(10)) & "<br/>")
                    strMailBody.Append("<strong>Lead From:</strong>&nbsp; Custom Tab<br/>")
                    strMailBody.Append("<strong>Date + (PST):</strong>&nbsp;" & strdate & "<br/><br/>")
                    If ConfigurationManager.AppSettings("AppPath") = "https://so.tsmaapplication.com/" Then
                        strMailBody.Append("Also, all of your leads can be found within your Social Outbreak Dashboard. {Access https://so.tsmaapplication.com and login; then see Convert > Leads where you can export all your leads as a CSV file.} Follow-up by sending specific information or by just saying: THANK YOU FOR YOUR INTEREST ... here's how I can help you.")
                    Else
                        strMailBody.Append("Also, all of your leads can be found within your Summa Social Dashboard. {Access https://www.summasocialapp.com and login; then see Convert > Leads where you can export all your leads as a CSV file.} Follow-up by sending specific information or by just saying: THANK YOU FOR YOUR INTEREST ... here's how I can help you.")
                    End If

                    Dim dalayers2 As New DALDataAccess
                    Dim dsctpages As New DataSet
                    dalayers2.AddCommand(CommandType.StoredProcedure, "prc_GetEmailForCTLeadsPages")
                    dalayers2.AddParam("@ctpageid", SqlDbType.VarChar, dsCT.Tables(1).Rows(i).Item("cti_FBPageId").ToString)
                    dsctpages = dalayers2.GetDataset

                    SendExternalEmail("postmaster@no-reply.com", dsctpages.Tables(0).Rows(0).Item("ctip_NotificationMail").ToString, "Custom Tab Form Data", strMailBody.ToString, "", "")

                    Dim dalUpdate As New DALDataAccess
                    dalUpdate.AddCommand(CommandType.StoredProcedure, "prc_UpdateCTLeadsData")
                    dalUpdate.AddParam("@ctleadid", SqlDbType.Int, dsCT.Tables(1).Rows(i).Item("cti_id"))
                    dalUpdate.AddParam("@ctpageid", SqlDbType.VarChar, dsCT.Tables(1).Rows(i).Item("cti_FBPageId"))
                    dalUpdate.ExecuteNonQuery()
                Catch ex As Exception
                    InsertErrorLogSWCT(0, dsCT.Tables(1).Rows(i).Item("cti_id"), ex.Message.ToString)
                End Try
            Next
        End If

    End Sub
#End Region
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


        'Dim networkCredentials As New System.Net.NetworkCredential("techsture.devlopers@gmail.com", "chrdjbar19")
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
#Region "Error Log"
    Sub InsertErrorLogSWCT(ByVal swid As Integer, ByVal ctid As Integer, ByVal ErrorDetails As String)
        Try
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_ErrorLogSendLeadNotification")
            dataAccess.AddParam("@el_swid", SqlDbType.Int, swid)
            dataAccess.AddParam("@el_ctid", SqlDbType.Int, ctid)
            dataAccess.AddParam("@el_errormsg", SqlDbType.VarChar, ErrorDetails)
            dataAccess.ExecuteNonQuery()
        Catch ex As Exception
        End Try
    End Sub
#End Region
End Class