Imports System.Configuration
Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Runtime.Serialization.Json
Imports Facebook
Imports BusinessAccessLayer.BusinessLayer
Imports System.Security.Cryptography
Imports tsma.SOServiceReference
Public Class siteuserlogin1
     Inherits System.Web.UI.Page

    'Dim objBAL As New BALlogin
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            spnEror.InnerText = ""
            Dim ds As New DataSet
            Dim objBAL As New BALlogin
            objBAL.FBId = Session("FacebookUserId")
            ds = objBAL.CheckSOUser()
            If ds.Tables(0).Rows(0).Item("Res") = "1" Then
                If ds.Tables(1).Rows.Count > 0 Then
                    Session("SiteUserName") = ds.Tables(1).Rows(0).Item("u_UserName")
                    Session("SiteUserId") = ds.Tables(1).Rows(0).Item("u_Id")
                    Dim uname As String = ds.Tables(1).Rows(0).Item("u_UserName").ToString()
                    Dim password As String = ds.Tables(1).Rows(0).Item("u_Password").ToString()
                    Dim client As New distributorWSSoapClient("distributorWSSoap")
                    Dim authHeader As New AuthHeader()
                    authHeader.AuthorizationKey = "404B6EA0-89C1-4CE3-B4A3-EAB79AA3B01F"
                    authHeader.Domain = "http://www.prodigix.com/"

                    'get a response ready
                    Dim response1 As WSResponse

                    'hash your password using SHA1
                    'testing testaccount1
                    '  Dim passwordEncode As String = "testaccount1"
                    Dim passwordEncode As String = password
                    Dim UE As New ASCIIEncoding
                    Dim HashValue As Byte(), MessageBytes As Byte() = UE.GetBytes(passwordEncode)
                    Dim SHhash As New SHA1Managed
                    Dim strHex As String = ""

                    HashValue = SHhash.ComputeHash(MessageBytes)
                    For Each b As Byte In HashValue
                        strHex += String.Format("{0:x2}", b)
                    Next


                    'authorize the user and get the distributor id back as well as the type of subscription
                    response1 = client.VerifyUser(authHeader, uname, strHex)
                    ' response1 = client.VerifyDistIDUser(authHeader, 111781)
                    If response1.Success = True Then
                        Response.Redirect("scheduler")
                    Else
                        spnEror.InnerText = response1.Message
                    End If
                End If

            End If
            GenerateImage()
            pnlmsg.Visible = False

        End If
    End Sub

    Private Sub imgLogin_ServerClick(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgLogin.ServerClick
        Try
            Dim client As New distributorWSSoapClient("distributorWSSoap")
            Dim authHeader As New AuthHeader()
            authHeader.AuthorizationKey = "404B6EA0-89C1-4CE3-B4A3-EAB79AA3B01F"
            authHeader.Domain = "http://www.prodigix.com/"

            'get a response ready
            Dim response1 As WSResponse



            'hash your password using SHA1
            'testing testaccount1
            '  Dim passwordEncode As String = "testaccount1"
            Dim passwordEncode As String = txtPwd.Value
            Dim UE As New ASCIIEncoding
            Dim HashValue As Byte(), MessageBytes As Byte() = UE.GetBytes(passwordEncode)
            Dim SHhash As New SHA1Managed
            Dim strHex As String = ""

            HashValue = SHhash.ComputeHash(MessageBytes)
            For Each b As Byte In HashValue
                strHex += String.Format("{0:x2}", b)
            Next
            

            'authorize the user and get the distributor id back as well as the type of subscription
            response1 = client.VerifyUser(authHeader, txtUid.Value, strHex)
            ' response1 = client.VerifyDistIDUser(authHeader, 111781)

            'Response.Write(response1.Success)
            'Response.End()
            If response1.Success = True Then
                AddSOUser()
                Response.Redirect("scheduler")
            Else
                spnEror.InnerText = "Invalid Username or Password"
            End If
            'Response.Write("Message:" & response1.Message & " Success:" & response1.Success & " uname:" & txtUid.Value & " PWD:" & strHex)
            'Response.End()
            'If txtcode1.Value = Utility.Decryption(hdncode1.Value) Then
            '    Dim ds, dsTet As New DataSet
            '    Dim objBAL As New BALlogin
            '    Dim objEncry As New Utility
            '    objBAL.UserName = txtUid.Value
            '    objBAL.password = txtPwd.Value.ToString() 'Session("FacebookUserId")
            '    ds = objBAL.CheckLogin
            '    dsTet = objBAL.AdminLoginDetails

            '    If ds.Tables(0).Rows.Count > 0 Then
            '        If ds.Tables(0).Rows(0).Item("u_Username") = txtUid.Value AndAlso Utility.Decryption(ds.Tables(0).Rows(0).Item("u_Password").ToString()) = txtPwd.Value.ToString() Then
            '            Session("SiteUserId") = ds.Tables(0).Rows(0).Item("u_Id")
            '            Session("SiteUserName") = ds.Tables(0).Rows(0).Item("u_FirstName")
            '            Response.Redirect(ConfigurationManager.AppSettings("AppPath") & "scheduler")
            '        Else
            '            spnEror.InnerText = "Invalid Username or Password"
            '        End If
            '    Else
            '        spnEror.InnerText = "Invalid Username or Password"
            '    End If
            'Else
            '    spnEror.Visible = True
            '    spnEror.InnerText = "Please enter correct security code!"
            'End If
            'GenerateImage()
        Catch ex As Exception
            spnEror.InnerText = "Error :" & ex.Message()
        End Try
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
        End If
    End Sub

#Region "Send External Email"
    Shared Sub SendExternalEmail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strMailBody As String, ByVal strcc As String, ByVal strbcc As String)
        'Try
        'If ConfigurationManager.AppSettings("SendMail").ToLower = "true" Then
        Dim objMail As Object
        objMail = System.Web.HttpContext.Current.Server.CreateObject("CDO.Message")
        'objMail.To = "kirtikumar.techsture@gmail.com"
        'objMail.To = "kapil.6988@gmail.com"
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
    Public Function GenerateImage()
        Try
            Dim ds As New DataSet
            Dim objBAL As New BALlogin
            Dim imgFile As String
            ds = objBAL.GenereateImage
            If ds.Tables(0).Rows.Count > 0 Then
                imgFile = ds.Tables(0).Rows(0).Item("im_name").ToString
                hdncode1.Value = Utility.Encryption(imgFile.ToString())
                imgcode1.Src = "../Content/textimages/" & imgFile & ".gif"
            End If
        Catch ex As Exception
        End Try
    End Function

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
End Class