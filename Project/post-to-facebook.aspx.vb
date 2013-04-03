Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System.Net
Imports System.IO
Imports System.Runtime.Serialization.Json

Public Class post_to_facebook
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If LoginCheck.LoginSessionCheckOnly() And (Not Request("msg") Is Nothing And Convert.ToString(Request("msg")) <> "") Then
            If Convert.ToString(Request("pt")) = "1" Then 'Save Draft
                Dim strResult As String
                Dim stringSeparators As String() = New String() {"%@$"}

                Dim strFBPageId() As String
                strFBPageId = Convert.ToString(Request("fpId")).Split(stringSeparators, StringSplitOptions.None)

                Dim strFBPageName() As String
                strFBPageName = Convert.ToString(Request("fpName")).Split(stringSeparators, StringSplitOptions.None)

                Dim strFBPageImage() As String
                strFBPageImage = Convert.ToString(Request("fpImage")).Split(stringSeparators, StringSplitOptions.None)

                Dim strFBPageAToken() As String
                strFBPageAToken = Convert.ToString(Request("fpAccessToken")).Split(stringSeparators, StringSplitOptions.None)


                Dim objBAL As New BALSchedulePost
                objBAL.TSMAUserId = Convert.ToString(Session("SiteUserId"))
                objBAL.FBUserId = Convert.ToString(Session("FacebookUserId"))
                objBAL.CompanyId = Convert.ToString(GetSetCookies.GetCookie("CompanyId"))
                objBAL.IndustryId = Convert.ToString(GetSetCookies.GetCookie("IndustryId"))
                objBAL.FBApplicationAccessToken = Convert.ToString(Session("FacebookAccessToken"))
                objBAL.Message = CStr(Replace(Request("msg"), Chr(10), "<br>"))
                objBAL.Image = Convert.ToString(Request("lnkP"))
                objBAL.VideoLink = Convert.ToString(Request("lnkY"))
                objBAL.VideoId = Convert.ToString(Request("lnkYId"))
                objBAL.VideoImage = Convert.ToString(Request("lnkYImage"))
                objBAL.ScheduleDate = Date.Now
                objBAL.ScheduleHour = "0"
                objBAL.ScheduleMinute = "0"
                objBAL.ScheduleAMPM = "AM"
                objBAL.ScheduleTimeZone = ""
                objBAL.IsPosted = 0
                objBAL.PostType = 2
                objBAL.CreatedDate = Date.Now
                objBAL.UpdatedDate = Date.Now
                strResult = objBAL.SavetoDraft()
                If Convert.ToString(strResult) <> "" Then
                    Dim obj As New BALSchedulePost
                    Dim i As Integer = 0
                    While (i < strFBPageId.Length And Convert.ToString(strFBPageId(i) <> ""))
                        obj.ScheduleId = Convert.ToInt32(strResult)
                        obj.FBPageId = strFBPageId(i)
                        obj.FBPageName = strFBPageName(i)
                        obj.FBPageImage = strFBPageImage(i)
                        obj.FBPageAccessToken = strFBPageAToken(i)
                        obj.AddQuickPost()
                        i = i + 1
                    End While
                End If
                Response.Write("Success")
            ElseIf Convert.ToString(Request("pt")) = "3" Then 'Save Lib
                Dim objLib As New Library
                Dim strResult As String = ""
                objLib.LibraryCategory = Convert.ToString(Request("lcat"))
                objLib.UserId = Convert.ToString(Session("SiteUserId"))
                objLib.FBUserId = Convert.ToString(Session("FacebookUserId"))
                objLib.LibCatTitle = Convert.ToString(Request("catTitle"))
                objLib.Library = CStr(Replace(Request("msg"), Chr(10), "<br>"))
                objLib.LibImage = Convert.ToString(Request("lnkP"))
                objLib.LibVideo = Convert.ToString(Request("lnkY"))
                objLib.LibVideoId = Convert.ToString(Request("lnkYId"))
                objLib.LibVideoImage = Convert.ToString(Request("lnkYImage"))
                objLib.CompanyId = Convert.ToString(GetSetCookies.GetCookie("CompanyId"))
                objLib.IndustryId = Convert.ToString(GetSetCookies.GetCookie("IndustryId"))
                objLib.SavetoMyLibrary2()
                Response.Write(Convert.ToString(Request("lcat")) & Convert.ToString(Request("catTitle")) & Request("msg"))
            ElseIf Convert.ToString(Request("pt")) = "4" Then 'Save Draft
                quickpost()
                Response.Write("Success")
            End If


        Else
            If Convert.ToString(Request("pt")) <> "2" Then
                Response.Write("Failure")
            End If
        End If
        'Get Libraries
        If Convert.ToString(Request("pt")) = "2" Then 'Get Lib
            Dim strResult As String = ""
            Dim objLib As New Library
            objLib.UserId = Convert.ToString(Session("SiteUserId"))
            objLib.FBUserId = Convert.ToString(Session("FacebookUserId"))
            objLib.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 
            objLib.IndustryId = GetSetCookies.GetCookie("IndustryId") ' 1 
            strResult = objLib.GetUsersLibraries2()
            Response.Write(strResult)
        End If
        Response.End()
    End Sub

#Region "Quick Post"
    Private Sub QuickPost()
        Dim strResult As String = ""
        Dim stringSeparators As String() = New String() {"%@$"}

        Dim strFBPageId() As String
        strFBPageId = Convert.ToString(Request("fpId")).Split(stringSeparators, StringSplitOptions.None)

        Dim strFBPageName() As String
        strFBPageName = Convert.ToString(Request("fpName")).Split(stringSeparators, StringSplitOptions.None)

        Dim strFBPageImage() As String
        strFBPageImage = Convert.ToString(Request("fpImage")).Split(stringSeparators, StringSplitOptions.None)

        Dim strFBPageAToken() As String
        strFBPageAToken = Convert.ToString(Request("fpAccessToken")).Split(stringSeparators, StringSplitOptions.None)

        Dim photopath As String = ""
        If Convert.ToString(Request("lnkP")) <> "" Then
            photopath = Server.MapPath("~/" & "Content/uploads/images/" & Convert.ToString(Request("lnkP")))
        End If
        'Photo Post
        If Convert.ToString(Request("lnkP")) <> "" Then
            Dim i As Integer = 0
            While (i < strFBPageId.Length And Convert.ToString(strFBPageId(i) <> ""))
                Dim path1 As String = strFBPageId(i) & "/albums"
                Dim Albumpath As String = "https://graph.facebook.com/" & strFBPageId(i) & "/albums?fields=id,name&access_token={0}"
                Dim fbapp1 = New FacebookClient(strFBPageAToken(i))
                Dim upload1 As IDictionary(Of String, Object) = New Dictionary(Of String, Object)()
                Dim get1 As IDictionary(Of String, Object) = New Dictionary(Of String, Object)()
                Dim request1 As WebRequest = WebRequest.Create(String.Format(Albumpath, strFBPageAToken(i)))
                Dim response1 As WebResponse = request1.GetResponse()
                Dim stream As Stream = response1.GetResponseStream()
                Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(Albums))
                Dim fAlbums As New Albums()
                fAlbums = TryCast(dataContractJsonSerializer.ReadObject(stream), Albums)
                Dim listAlbums As New List(Of Albums.m_data)

                For Each item1 As Albums.m_data In fAlbums.data
                    listAlbums.Add(item1)
                Next
                Dim cnt As Integer = listAlbums.Count
                Dim flag As Integer
                Dim aid As String = ""
                For k As Integer = 0 To cnt - 1
                    If listAlbums.Item(k).name = ConfigurationManager.AppSettings("FBAlbumName") Then
                        aid = listAlbums.Item(k).id
                        flag = 0
                        Exit For
                    Else
                        flag = 1
                    End If
                Next
                If flag = 1 Then
                    upload1.Add("name", ConfigurationManager.AppSettings("FBAlbumName"))

                    Dim album As JsonObject = fbapp1.Post(path1, upload1)

                    Dim albumID As String = album("id")

                    Dim path As String = albumID & "/photos" '"225274570816748/photos"
                    Dim mediaObject As New FacebookMediaObject() With { _
                        .FileName = photopath, _
                        .ContentType = "image/jpg" _
                    }
                    Dim fileBytes As Byte() = File.ReadAllBytes(mediaObject.FileName)
                    mediaObject.SetValue(fileBytes)
                    Dim upload As IDictionary(Of String, Object) = New Dictionary(Of String, Object)()
                    upload.Add("message", CStr(Replace(Request("msg"), Chr(10), "<br>")))
                    upload.Add("image", mediaObject)
                    Dim fbapp = New FacebookClient(strFBPageAToken(i))
                    fbapp.Post(path, upload)
                Else
                    Dim path As String = aid & "/photos" '"225274570816748/photos"
                    Dim mediaObject As New FacebookMediaObject() With { _
                        .FileName = photopath, _
                        .ContentType = "image/jpg" _
                    }
                    Dim fileBytes As Byte() = File.ReadAllBytes(mediaObject.FileName)
                    mediaObject.SetValue(fileBytes)
                    Dim upload As IDictionary(Of String, Object) = New Dictionary(Of String, Object)()
                    upload.Add("message", CStr(Replace(Request("msg"), Chr(10), "<br>")))
                    upload.Add("image", mediaObject)
                    Dim fbapp = New FacebookClient(strFBPageAToken(i))
                    fbapp.Post(path, upload)
                End If
                i = i + 1
            End While
        ElseIf Convert.ToString(Request("lnkY")) = "" Then
            Dim args = New Dictionary(Of String, Object)()
            args("message") = CStr(Replace(Request("msg"), Chr(10), "<br>"))
            Dim regex As New Regex(",")
            Dim str As String = ""
            Dim i As Integer = 0
            While (i < strFBPageId.Length And Convert.ToString(strFBPageId(i) <> ""))
                Dim path As String = strFBPageId(i) & "/feed"
                Dim fbapp = New FacebookClient(strFBPageAToken(i))
                fbapp.Post(path, args)
            End While
        End If

        If Convert.ToString(Request("lnkY")) <> "" Then
            Dim args = New Dictionary(Of String, Object)()
            args("message") = CStr(Replace(Request("msg"), Chr(10), "<br>"))
            args("link") = Convert.ToString(Request("lnkY"))
            Dim regex As New Regex(",")
            Dim str As String = ""
            Dim i As Integer = 0
            While (i < strFBPageId.Length And Convert.ToString(strFBPageId(i) <> ""))
                Dim path As String = strFBPageId(i) & "/feed"
                Dim fbapp = New FacebookClient(strFBPageAToken(i))
                fbapp.Post(path, args)
            End While
        End If
        Dim intDId As Integer = 0
        Dim objBAL As New BALSchedulePost
        objBAL.DraftId = intDId
        objBAL.TSMAUserId = Convert.ToString(Session("SiteUserId"))
        objBAL.FBUserId = Convert.ToString(Session("FacebookUserId"))
        objBAL.CompanyId = Convert.ToString(GetSetCookies.GetCookie("CompanyId"))
        objBAL.IndustryId = Convert.ToString(GetSetCookies.GetCookie("IndustryId"))
        objBAL.FBApplicationAccessToken = Convert.ToString(Session("FacebookAccessToken"))
        objBAL.Message = CStr(Replace(Request("msg"), Chr(10), "<br>"))
        objBAL.Image = Convert.ToString(Request("lnkP"))
        objBAL.Video = Convert.ToString(Request("lnkY"))
        objBAL.VideoLink = Convert.ToString(Request("lnkY"))
        objBAL.VideoId = Convert.ToString(Request("lnkYId"))
        objBAL.VideoImage = Convert.ToString(Request("lnkYImage"))
        objBAL.ScheduleDate = Date.Now
        objBAL.ScheduleHour = "0"
        objBAL.ScheduleMinute = "0"
        objBAL.ScheduleAMPM = "AM"
        objBAL.ScheduleTimeZone = ""
        objBAL.IsPosted = 1
        objBAL.PostType = 0
        objBAL.UpdatedDate = Date.Now
        objBAL.UpdateDrafts()
        objBAL.DeleteFanPages(intDId)
        Dim l As Integer = 0
        While (l < strFBPageId.Length And Convert.ToString(strFBPageId(l) <> ""))
            objBAL.ScheduleId = Convert.ToInt32(strResult)
            objBAL.FBPageId = strFBPageId(l)
            objBAL.FBPageName = strFBPageName(l)
            objBAL.FBPageImage = strFBPageImage(l)
            objBAL.FBPageAccessToken = strFBPageAToken(l)
            objBAL.AddQuickPost()
            l = l + 1
        End While
    End Sub
#End Region
End Class