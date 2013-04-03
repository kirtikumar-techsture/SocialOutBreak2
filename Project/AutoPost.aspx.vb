Imports DataAccessLayer.DataAccessLayer
Imports BusinessAccessLayer.BusinessLayer
Imports System.IO
Imports System.Net
Imports Facebook
Imports System.Runtime.Serialization.Json

Public Class AutoPost
    Inherits System.Web.UI.Page


    Dim objBAL As New BALSchedulePost
    Dim objLog As New Log

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SchedulePost()
        'Dim intTotal As Integer = 0
        'Dim intSuccessFull As Integer = 0
        'Dim intFailed As Integer = 0
        'objLog.WritePlainLog(FormatDateTime(Now.Date, DateFormat.ShortDate))
        'objLog.WritePlainLog("==========================================")
        'objLog.WriteLog("START")
        'AutoPost(intTotal, intSuccessFull, intFailed)
        'objLog.WriteLog("END  ")
        'objLog.WritePlainLog("------------------------------------------")
        'objLog.WritePlainLog("Total Records : " & intTotal)
        'objLog.WritePlainLog("Successfull   : " & intSuccessFull)
        'objLog.WritePlainLog("Failed        : " & intFailed)
        'objLog.WritePlainLog("------------------------------------------")
        'objLog.WritePlainLog("")

    End Sub

    '#Region "Auto Post"
    '    Sub AutoPost(ByRef intTotal As Integer, ByRef intSuccessFull As Integer, ByRef intFailed As Integer)
    '        Dim dataAccess As New DALDataAccess()
    '        Dim ds As New DataSet
    '        dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetRecordsForAutoPost")
    '        ds = dataAccess.GetDataset()
    '        intTotal = ds.Tables(0).Rows.Count
    '        For Each dtRow As DataRow In ds.Tables(0).Rows
    '            If dtRow("AccessToken") <> "" Then
    '                Try
    '                    Dim path As String = ""
    '                    Dim fbApp = New FacebookClient(dtRow("AccessToken").ToString)
    '                    Dim postData = New Dictionary(Of String, Object)()

    '                    If dtRow("Image") <> "" Then
    '                        Dim imagePath As String = Server.MapPath("~/" & "Content/uploads/images/" & dtRow("Image").ToString)
    '                        Dim mediaObject As New FacebookMediaObject() With { _
    '                            .FileName = imagePath, _
    '                            .ContentType = "image/jpg" _
    '                        }
    '                        Dim fileBytes As Byte() = File.ReadAllBytes(mediaObject.FileName)
    '                        mediaObject.SetValue(fileBytes)

    '                        postData.Clear()
    '                        postData.Add("message", dtRow("Message").ToString.Replace("<br>", Chr(10)))
    '                        postData.Add("image", mediaObject)
    '                        path = dtRow("FBPageId").ToString & "/photos"
    '                        fbApp.Post(path, postData)

    '                        'objBAL.AutoPostId = dtRow("AutoPostId")
    '                        'objBAL.FBUserId = dtRow("FBUserID")
    '                        'objBAL.FBPageId = dtRow("FBPageId")
    '                        'objBAL.FBPageAccessToken = dtRow("AccessToken")
    '                        'objBAL.TSMAUserId = dtRow("TSMAUserId")
    '                        'objBAL.AddLogOfSuccessfullAutopost()
    '                    Else
    '                        postData.Add("message", dtRow("Message").ToString.Replace("<br>", Chr(10)))
    '                        If dtRow("Link") <> "" Then
    '                            postData.Add("link", dtRow("Link").ToString)
    '                        End If
    '                        path = dtRow("FBPageId").ToString & "/feed"
    '                        fbApp.Post(path, postData)

    '                        'objBAL.AutoPostId = dtRow("AutoPostId")
    '                        'objBAL.FBUserId = dtRow("FBUserID")
    '                        'objBAL.FBPageId = dtRow("FBPageId")
    '                        'objBAL.FBPageAccessToken = dtRow("AccessToken")
    '                        'objBAL.TSMAUserId = dtRow("TSMAUserId")
    '                        'objBAL.AddLogOfSuccessfullAutopost()
    '                    End If
    '                    objBAL.AutoPostId = dtRow("AutoPostId")
    '                    objBAL.UpdateAutoPostMaster()

    '                    intSuccessFull = intSuccessFull + 1
    '                Catch ex As FacebookOAuthException
    '                    InsertErrorLog(dtRow("AutoPostId"), dtRow("TSMAUserId"), dtRow("FBUserID"), dtRow("FBPageId"), dtRow("AccessToken"), ex.Message.ToString())
    '                    intFailed = intFailed + 1
    '                End Try
    '            Else
    '                InsertErrorLog(dtRow("AutoPostId"), dtRow("TSMAUserId"), dtRow("FBUserID"), dtRow("FBPageId"), dtRow("AccessToken"), "Access token does not exists!")
    '                intFailed = intFailed + 1
    '            End If
    '        Next
    '    End Sub
    '#End Region

    '#Region "Error Log"
    '    Sub InsertErrorLog(AutoPostId As Integer, TSMAUserId As Integer, FBUserID As String, FBPageId As String, AccessToken As String, ErrorDetails As String)
    '        Try
    '            Dim dataAccess As New DALDataAccess()
    '            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_AddAutoPostErrorDetails")
    '            dataAccess.AddParam("@er_AutoPostId", SqlDbType.Int, AutoPostId)
    '            dataAccess.AddParam("@er_TSMAUserId", SqlDbType.Int, TSMAUserId)
    '            dataAccess.AddParam("@er_FBUserId", SqlDbType.VarChar, FBUserID)
    '            dataAccess.AddParam("@er_FBPageId", SqlDbType.VarChar, FBPageId)
    '            dataAccess.AddParam("@er_AcessToken", SqlDbType.VarChar, AccessToken)
    '            dataAccess.AddParam("@er_Error", SqlDbType.VarChar, ErrorDetails)
    '            dataAccess.ExecuteNonQuery()
    '        Catch ex As Exception
    '        End Try
    '    End Sub
    '#End Region

    Private Sub SchedulePost()
        Try
            Dim ds As New DataSet
            Dim objBAL As New BALSchedulePost
            ' objBAL.TSMAUserId = Session("SiteUserId")
            ' objBAL.FBUserId = Session("FacebookUserId")
            ds = objBAL.GetScheduledData()

            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim strid As Integer = ds.Tables(0).Rows(i).Item("sm_id")
                    Dim strTSMAUserId As Integer = ds.Tables(0).Rows(i).Item("sm_TSMAUserId")
                    Dim strmessage1 As String = ds.Tables(0).Rows(i).Item("sm_Message").ToString.Replace("<br>", Chr(10))
                    Dim strDate As String = ds.Tables(0).Rows(i).Item("sm_ScheduleDate").ToString
                    Dim strHour As String = ds.Tables(0).Rows(i).Item("sm_ScheduleHour").ToString
                    Dim strMinute As String = ds.Tables(0).Rows(i).Item("sm_ScheduleMinute").ToString
                    Dim strAMPM As String = ds.Tables(0).Rows(i).Item("sm_ScheduleAMPM").ToString
                    Dim strTimeZone As String = ds.Tables(0).Rows(i).Item("sm_ScheduleTimeZone").ToString

                    If ds.Tables(0).Rows(i).Item("sm_Image").ToString <> "" Then
                        Dim photopath As String = Server.MapPath("~/" & "Content/uploads/images/" & ds.Tables(0).Rows(i).Item("sm_Image").ToString)
                        objBAL.ScheduleId = CInt(strid)
                        Dim dsFanPages1 As New DataSet
                        dsFanPages1 = objBAL.GetScheduledDataFanPages()

                        If dsFanPages1.Tables(0).Rows.Count > 0 Then
                            For j As Integer = 0 To dsFanPages1.Tables(0).Rows.Count - 1
                                Try
                                    Dim path1 As String = dsFanPages1.Tables(0).Rows(j).Item("sp_FBPageId").ToString & "/albums" '"225274570816748/photos"
                                    Dim Albumpath As String = "https://graph.facebook.com/" & dsFanPages1.Tables(0).Rows(j).Item("sp_FBPageId").ToString & "/albums?fields=id,name&access_token={0}"
                                    Dim fbapp1 = New FacebookClient(dsFanPages1.Tables(0).Rows(j).Item("sp_FBPageAccessToken").ToString)
                                    Dim upload1 As IDictionary(Of String, Object) = New Dictionary(Of String, Object)()
                                    Dim get1 As IDictionary(Of String, Object) = New Dictionary(Of String, Object)()
                                    Dim request1 As WebRequest = WebRequest.Create(String.Format(Albumpath, dsFanPages1.Tables(0).Rows(j).Item("sp_FBPageAccessToken").ToString))
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
                                        upload.Add("message", ds.Tables(0).Rows(i).Item("sm_Message").ToString.Replace("<br>", Chr(10)))
                                        upload.Add("image", mediaObject)
                                        Dim fbapp = New FacebookClient(dsFanPages1.Tables(0).Rows(j).Item("sp_FBPageAccessToken").ToString)
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
                                        upload.Add("message", ds.Tables(0).Rows(i).Item("sm_Message").ToString.Replace("<br>", Chr(10)))
                                        upload.Add("image", mediaObject)
                                        Dim fbapp = New FacebookClient(dsFanPages1.Tables(0).Rows(j).Item("sp_FBPageAccessToken").ToString)
                                        fbapp.Post(path, upload)
                                    End If

                                    '    Dim path As String = dsFanPages1.Tables(0).Rows(j).Item("sp_FBPageId").ToString & "/photos"
                                    '    Dim mediaObject As New FacebookMediaObject() With { _
                                    '        .FileName = photopath, _
                                    '        .ContentType = "image/jpg" _
                                    '}
                                    '    Dim fileBytes As Byte() = File.ReadAllBytes(mediaObject.FileName)
                                    '    mediaObject.SetValue(fileBytes)
                                    '    Dim upload As IDictionary(Of String, Object) = New Dictionary(Of String, Object)()
                                    '    upload.Add("message", ds.Tables(0).Rows(i).Item("sm_Message").ToString.Replace("<br>", Chr(10)))
                                    '    upload.Add("image", mediaObject)
                                    '    Dim fbapp = New FacebookClient(dsFanPages1.Tables(0).Rows(j).Item("sp_FBPageAccessToken").ToString)
                                    '    fbapp.Post(path, upload)

                                    objBAL.DraftId = CInt(strid)
                                    objBAL.TSMAUserId = ds.Tables(0).Rows(i).Item("sm_TSMAUserId").ToString 'Session("SiteUserId")
                                    objBAL.FBUserId = ds.Tables(0).Rows(i).Item("sm_FBUserId").ToString 'Session("FacebookUserId")
                                    objBAL.UpdateSchdulePostData()
                                Catch ex As Exception
                                    InsertErrorLogSchedule(strid, strTSMAUserId, dsFanPages1.Tables(0).Rows(j).Item("sp_FBUserId").ToString, dsFanPages1.Tables(0).Rows(j).Item("sp_FBPageId").ToString, dsFanPages1.Tables(0).Rows(j).Item("sp_FBPageAccessToken").ToString, ex.Message.ToString())
                                End Try
                            Next
                        End If
                    Else

                        Dim args1 = New Dictionary(Of String, Object)()
                        args1.Add("message", strmessage1.ToString.Replace("<br>", Chr(10)))
                        If ds.Tables(0).Rows(i).Item("sm_VideoLink").ToString <> "" Then
                            args1.Add("link", ds.Tables(0).Rows(i).Item("sm_VideoLink").ToString)
                        End If

                        objBAL.ScheduleId = CInt(strid)
                        Dim dsFanPages As New DataSet
                        dsFanPages = objBAL.GetScheduledDataFanPages()
                        If dsFanPages.Tables(0).Rows.Count > 0 Then
                            For j As Integer = 0 To dsFanPages.Tables(0).Rows.Count - 1
                                Try
                                    ' Response.Write("fanpages Id :" & dsFanPages.Tables(0).Rows(j).Item("sp_FBPageId").ToString & ",<br/>")
                                    '  Response.Write("AccessToken :" & dsFanPages.Tables(0).Rows(j).Item("sp_FBPageAccessToken").ToString & ",")
                                    'Response.End()
                                    Dim AccessToken As String = dsFanPages.Tables(0).Rows(j).Item("sp_FBPageAccessToken").ToString
                                    Dim path As String = dsFanPages.Tables(0).Rows(j).Item("sp_FBPageId").ToString & "/feed" '"151638698269605/feed" '
                                    Dim fbapp = New FacebookClient(AccessToken)
                                    fbapp.Post(path, args1)

                                    objBAL.DraftId = CInt(strid)
                                    objBAL.TSMAUserId = ds.Tables(0).Rows(i).Item("sm_TSMAUserId").ToString 'Session("SiteUserId")
                                    objBAL.FBUserId = ds.Tables(0).Rows(i).Item("sm_FBUserId").ToString 'Session("FacebookUserId")
                                    objBAL.UpdateSchdulePostData()
                                Catch ex As Exception
                                    InsertErrorLogSchedule(strid, strTSMAUserId, dsFanPages.Tables(0).Rows(j).Item("sp_FBUserId").ToString, dsFanPages.Tables(0).Rows(j).Item("sp_FBPageId").ToString, dsFanPages.Tables(0).Rows(j).Item("sp_FBPageAccessToken").ToString, ex.Message.ToString())
                                End Try

                            Next
                            'Response.Write(dsFanPages.Tables(0).Rows(0).Item("sp_FBPageId").ToString)
                        End If

                    End If

                    'If ds.Tables(0).Rows(i).Item("sm_VideoLink").ToString <> "" Then
                    '    Dim args1 = New Dictionary(Of String, Object)()
                    '    args1("message") = strmessage1
                    '    args1("link") = ds.Tables(0).Rows(i).Item("sm_VideoLink").ToString
                    '    objBAL.ScheduleId = CInt(strid)
                    '    Dim dsFanPages As New DataSet
                    '    dsFanPages = objBAL.GetScheduledDataFanPages()
                    '    If dsFanPages.Tables(0).Rows.Count > 0 Then
                    '        For j As Integer = 0 To dsFanPages.Tables(0).Rows.Count - 1
                    '            Try
                    '                ' Response.Write("fanpages Id :" & dsFanPages.Tables(0).Rows(j).Item("sp_FBPageId").ToString & ",<br/>")
                    '                '  Response.Write("AccessToken :" & dsFanPages.Tables(0).Rows(j).Item("sp_FBPageAccessToken").ToString & ",")
                    '                'Response.End()
                    '                Dim AccessToken As String = dsFanPages.Tables(0).Rows(j).Item("sp_FBPageAccessToken").ToString
                    '                Dim path As String = dsFanPages.Tables(0).Rows(j).Item("sp_FBPageId").ToString & "/feed" '"151638698269605/feed" '
                    '                Dim fbapp = New FacebookClient(AccessToken)
                    '                fbapp.Post(path, args1)

                    '                objBAL.DraftId = CInt(strid)
                    '                objBAL.TSMAUserId = ds.Tables(0).Rows(i).Item("sm_TSMAUserId").ToString 'Session("SiteUserId")
                    '                objBAL.FBUserId = ds.Tables(0).Rows(i).Item("sm_FBUserId").ToString 'Session("FacebookUserId")
                    '                objBAL.UpdateSchdulePostData()
                    '            Catch ex As Exception
                    '                InsertErrorLogSchedule(strid, strTSMAUserId, dsFanPages.Tables(0).Rows(j).Item("sp_FBUserId").ToString, dsFanPages.Tables(0).Rows(j).Item("sp_FBPageId").ToString, dsFanPages.Tables(0).Rows(j).Item("sp_FBPageAccessToken").ToString, ex.Message.ToString())
                    '            End Try

                    '        Next
                    '        'Response.Write(dsFanPages.Tables(0).Rows(0).Item("sp_FBPageId").ToString)
                    '    End If
                    '    'Response.Redirect(ConfigurationManager.AppSettings("AppPath"))
                    'End If


                    'objBAL.DraftId = CInt(strid)
                    'objBAL.TSMAUserId = ds.Tables(0).Rows(i).Item("sm_TSMAUserId").ToString 'Session("SiteUserId")
                    'objBAL.FBUserId = ds.Tables(0).Rows(i).Item("sm_FBUserId").ToString 'Session("FacebookUserId")
                    'objBAL.CompanyId = ds.Tables(0).Rows(i).Item("sm_CompanyId").ToString
                    'objBAL.IndustryId = ds.Tables(0).Rows(i).Item("sm_IndustryId").ToString
                    'objBAL.FBApplicationAccessToken = ds.Tables(0).Rows(i).Item("sm_FBApplicationAccessToken").ToString 'Session("FacebookAccessToken")
                    'objBAL.Message = ds.Tables(0).Rows(i).Item("sm_Message").ToString
                    'objBAL.Image = ds.Tables(0).Rows(i).Item("sm_image").ToString
                    'objBAL.Video = ds.Tables(0).Rows(i).Item("sm_Video").ToString
                    'objBAL.VideoLink = ds.Tables(0).Rows(i).Item("sm_VideoLink").ToString
                    'objBAL.VideoId = ds.Tables(0).Rows(i).Item("sm_VideoId").ToString
                    'objBAL.VideoImage = ds.Tables(0).Rows(i).Item("sm_VideoImage").ToString
                    'objBAL.ScheduleDate = strDate
                    'objBAL.ScheduleHour = strHour
                    'objBAL.ScheduleMinute = strMinute
                    'objBAL.ScheduleAMPM = strAMPM
                    'objBAL.ScheduleTimeZone = strTimeZone
                    'objBAL.IsPosted = 1
                    'objBAL.PostType = 0
                    'objBAL.UpdatedDate = Date.Now
                    'objBAL.UpdateDrafts()
                    '   lblMessage.Text = "Message Sent"

                Next
            End If
        Catch ex As Exception
            InsertErrorLogSchedule(0, 0, "", "", "", "Error at starting of schedule post : " & ex.Message.ToString())
        End Try
    End Sub
#Region "Error Log"
    Sub InsertErrorLogSchedule(ByVal AutoPostId As Integer, ByVal TSMAUserId As Integer, ByVal FBUserID As String, ByVal FBPageId As String, ByVal AccessToken As String, ByVal ErrorDetails As String)
        Try
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_AddSchedulePostErrorDetails")
            dataAccess.AddParam("@sper_AutoPostId", SqlDbType.Int, AutoPostId)
            dataAccess.AddParam("@sper_TSMAUserId", SqlDbType.Int, TSMAUserId)
            dataAccess.AddParam("@sper_FBUserId", SqlDbType.VarChar, FBUserID)
            dataAccess.AddParam("@sper_FBPageId", SqlDbType.VarChar, FBPageId)
            dataAccess.AddParam("@sper_AcessToken", SqlDbType.VarChar, AccessToken)
            dataAccess.AddParam("@sper_Error", SqlDbType.VarChar, ErrorDetails)
            dataAccess.ExecuteNonQuery()
        Catch ex As Exception
        End Try
    End Sub
#End Region

End Class