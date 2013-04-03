Imports System.Configuration
Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Runtime.Serialization.Json
Imports Facebook
Imports BusinessAccessLayer.BusinessLayer

Public Class siteuserlogin
    Inherits System.Web.UI.Page

    'Dim objBAL As New BALlogin
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GenerateImage()
            pnlmsg.Visible = False
            spnEror.InnerText = ""
        End If
    End Sub

    Private Sub imgLogin_ServerClick(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgLogin.ServerClick
        'Try
        If txtcode1.Value = Utility.Decryption(hdncode1.Value) Then
            Dim ds, dsTet As New DataSet
            Dim objBAL As New BALlogin
            Dim objEncry As New Utility
            objBAL.UserName = txtUid.Value.Trim()
            objBAL.password = txtPwd.Value.ToString() 'Session("FacebookUserId")
            ds = objBAL.CheckLogin
            ' dsTet = objBAL.AdminLoginDetails

            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("u_Username").Trim() = txtUid.Value.Trim() AndAlso Utility.Decryption(ds.Tables(0).Rows(0).Item("u_Password").ToString()) = txtPwd.Value.ToString() Then
                    Session("SiteUserId") = ds.Tables(0).Rows(0).Item("u_Id")
                    Session("SiteUserName") = ds.Tables(0).Rows(0).Item("u_FirstName")
                    If ConfigurationManager.AppSettings("AppPath") = "https://www.summasocialapp.com/" Or ConfigurationManager.AppSettings("AppPath") = "http://www.summasocialapp.com/" Then
                        Response.Redirect(ConfigurationManager.AppSettings("AppPath") & "dashboard")
                    Else
                        Response.Redirect(ConfigurationManager.AppSettings("AppPath") & "scheduler")
                    End If

                    'AddSOUser()
                Else
                    spnEror.InnerText = "Invalid Username or Password"
                End If
            Else
                spnEror.InnerText = "Invalid Username or Password"
            End If
        Else
            spnEror.Visible = True
            spnEror.InnerText = "Please enter correct security code!"
        End If
        GenerateImage()
        'Catch ex As Exception
        '    spnEror.InnerText = "Error :" & ex.Message()
        'End Try
    End Sub

#Region "Send External Email"
    Shared Sub SendExternalEmail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strMailBody As String, ByVal strcc As String, ByVal strbcc As String)
        'Try
        'If ConfigurationManager.AppSettings("SendMail").ToLower = "true" Then
        Dim objMail As Object
        objMail = System.Web.HttpContext.Current.Server.CreateObject("CDO.Message")
        objMail.To = "kirtikumar.techsture@gmail.com"
        objMail.To = CStr(strTo)
        objMail.From = CStr(strFrom)
        objMail.cc = strcc
        objMail.bcc = strbcc
        ' objMail.MailFormat = 0
        ' objMail.BodyFormat = 0
        objMail.Subject = CStr(strSubject)
        objMail.HtmlBody = strMailBody
        'objMail.Body = strMailBody
        objMail.send()
        objMail = Nothing
        ' End If
        'Catch ex As Exception
        'End Try
    End Sub

#End Region

#Region "Generate Image"
    Public Sub GenerateImage()
        Try
            Dim ds As New DataSet
            Dim objBAL As New BALlogin
            Dim imgFile As String
            ds = objBAL.GenereateImage
            If ds.Tables(0).Rows.Count > 0 Then
                imgFile = ds.Tables(0).Rows(0).Item("im_name").ToString
                hdncode1.Value = Utility.Encryption(imgFile.ToString())
                imgcode1.Src = "../Content/textimages/" & imgFile & ".gif"
                If ConfigurationManager.AppSettings("AppPath") = "http://192.168.19.28:5069/" Then
                    txtcode1.Value = imgFile
                    txtUid.Value = "techdev"
                    txtPwd.Value = "techdev123"
                End If
                '
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

    Private Sub imgPassword_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgPassword.Click
        Dim objBAL As New BALSchedulePost
        Dim ds As New DataSet
        objBAL.UserEmailForgotpwd = txtForget.Value
        ds = objBAL.GetEmailForgotPassword
        If ds.Tables(0).Rows.Count > 0 Then
            SendExternalEmail("info@techsturedevelopment.com", txtForget.Value, "Forget Password", "Your Password is: " & ds.Tables(0).Rows(0).Item("u_Password").ToString, "", "")
            spnEror.InnerText = "We Will send Your Password Soon"
        Else
            spnEror.InnerText = "Please enter valid email address."
        End If
    End Sub

    Sub AddSOUser()
        Dim obj As New BALlogin
        Dim strFirstName As String = ""
        Dim strLastName As String = ""
        Dim name As String = Session("FacebookUserName")
        If name <> "" Then
            If (name.IndexOf(" ") = -1) Then
                strFirstName = name
                strLastName = "NA"

            Else
                strFirstName = name.Substring(0, name.IndexOf(" ")) '"kirtikumar" '
                strLastName = name.Substring(name.IndexOf(" ") + 1)
            End If
        Else
            strFirstName = "NA"
            strLastName = "NA"
        End If
        Dim ds As New DataSet
        obj.FBId = Session("FacebookUserId")
        obj.FBUserName = Session("FacebookUserName")
        obj.FirstName = strFirstName
        obj.LastName = strLastName
        obj.FBUserEmail = Session("FacebookUserEmail")
        obj.UserName = txtUid.Value
        obj.password = txtPwd.Value
        obj.CompanyID = GetSetCookies.GetCookie("CompanyId")
        obj.IndustryID = GetSetCookies.GetCookie("IndustryId")
        ds = obj.AddSOUser()
        If ds.Tables(0).Rows.Count > 0 Then
            Session("SiteUserId") = ds.Tables(0).Rows(0).Item("u_Id")
            Session("SiteUserName") = ds.Tables(0).Rows(0).Item("u_UserName")
            If ConfigurationManager.AppSettings("AppPath") = "https://www.summasocialapp.com/" Or ConfigurationManager.AppSettings("AppPath") = "http://www.summasocialapp.com/" Then
                Response.Redirect(ConfigurationManager.AppSettings("AppPath") & "dashboard")
            Else
                Response.Redirect(ConfigurationManager.AppSettings("AppPath") & "scheduler")
            End If
        End If
    End Sub
End Class