Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System.Threading

Public Class publish_cover_photo_edit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lnkDownload.HRef = "download.aspx?Image=" & Session("ImageName")
            BindCoverPhoto(Request("id"))
            'BindFanPages()
            ' CreateAlbum(Session("FacebookAccessToken"))
        End If
    End Sub


#Region "Bind Fan Pages"
    Sub BindSelectedFanPages(ByVal intFanPageId As String)
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim accessToken As String = Session("FacebookAccessToken")
                Dim clientId As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
                Dim clientSecret As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey").ToString()
                Dim url As String = "https://graph.facebook.com/me/accounts?fields=id,name,link,picture,category,access_token&return_ssl_resources=true&access_token={0}"
                Dim fbRequest As WebRequest = WebRequest.Create(String.Format(url, accessToken))
                Dim fbResponse As WebResponse = fbRequest.GetResponse()
                Dim stream As Stream = fbResponse.GetResponseStream()
                Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(FanPage))

                Dim fPage As New FanPage()
                fPage = TryCast(dataContractJsonSerializer.ReadObject(stream), FanPage)
                'Dim listPages As New List(Of FanPage.m_data)

                'For Each item As FanPage.m_data In fPage.data
                '    listPages.Add(item)
                '    'listPages.Sort(sortByName)
                'Next
                'If listPages.Count > 0 Then
                '    dstFanPages.DataSource = listPages
                '    dstFanPages.DataBind()
                '    'plcData.Visible = True
                '    'plcNoData.Visible = False
                'Else
                '    dstFanPages.DataSource = Nothing
                '    dstFanPages.DataBind()
                '    'plcData.Visible = False
                '    'plcNoData.Visible = True
                'End If

                Dim dtTable As New DataTable
                dtTable.Columns.Add(New DataColumn("name", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("id", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("link", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("picture", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("access_token", Type.GetType("System.String")))

                For Each item As FanPage.m_data In fPage.data
                    If Not item.access_token Is Nothing Then
                        If Not item.link Is Nothing Then
                            If Not item.picture Is Nothing Then
                                Dim dtRow1 As DataRow = dtTable.NewRow
                                dtRow1("name") = item.name.ToString
                                dtRow1("id") = item.id.ToString
                                dtRow1("picture") = item.picture.data.url.ToString
                                dtRow1("access_token") = item.access_token.ToString

                                dtTable.Rows.Add(dtRow1)
                            End If
                        End If
                    End If
                Next
                dtTable.TableName = "fanpages"
                Dim dv As DataView = New DataView(dtTable)
                dv.Sort = "name"

                If dv.Count > 0 Then
                    dstFanPages.DataSource = dv
                    dstFanPages.DataBind()
                    'plcData.Visible = True
                    'plcNoData.Visible = False
                Else
                    dstFanPages.DataSource = Nothing
                    dstFanPages.DataBind()
                    'plcData.Visible = False
                    'plcNoData.Visible = True
                End If

                For Each item123 As DataListItem In dstFanPages.Items
                    Dim myCheckBox As HtmlInputRadioButton
                    Dim HiddenID As New HtmlInputHidden
                    myCheckBox = CType(item123.FindControl("chkPage"), HtmlInputRadioButton)
                    HiddenID = CType(item123.FindControl("hdnPageId"), HtmlInputHidden)
                    'Response.Write("Hidden Id : " & HiddenID.Value & "<br/>")
                    Dim str As String = CStr(HiddenID.Value)
                    'Response.Write("intFanPageId : " & intFanPageId & "<br/>")
                    If str = intFanPageId Then
                        myCheckBox.Checked = True
                    Else
                        myCheckBox.Disabled = True
                    End If
                Next
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Facebook is experiencing problems at this time. Please save your work and try to publish later."
        End Try
    End Sub


    Sub BindFanPages()
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                Dim accessToken1 As String = Session("FacebookAccessToken")
                Dim clientId1 As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
                Dim clientSecret1 As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey").ToString()
                Dim url As String = "https://graph.facebook.com/me/accounts?fields=id,name,link,picture,category,access_token&return_ssl_resources=true&access_token={0}"
                Dim fbRequest1 As WebRequest = WebRequest.Create(String.Format(url, accessToken1))
                Dim fbResponse1 As WebResponse = fbRequest1.GetResponse()
                Dim stream1 As Stream = fbResponse1.GetResponseStream()
                Dim dataContractJsonSerializer1 As New DataContractJsonSerializer(GetType(FanPage))

                Dim fPage1 As New FanPage()
                fPage1 = TryCast(dataContractJsonSerializer1.ReadObject(stream1), FanPage)
                'Dim listPages1 As New List(Of FanPage.m_data)

                'For Each item As FanPage.m_data In fPage1.data
                '    listPages1.Add(item)
                '    'listPages.Sort(sortByName)
                'Next
                'If listPages1.Count > 0 Then
                '    dstFanPages.DataSource = listPages1
                '    dstFanPages.DataBind()
                '    'plcData.Visible = True
                '    'plcNoData.Visible = False
                'Else
                '    dstFanPages.DataSource = Nothing
                '    dstFanPages.DataBind()
                '    'plcData.Visible = False
                '    'plcNoData.Visible = True
                'End If
                Dim dtTable As New DataTable
                dtTable.Columns.Add(New DataColumn("name", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("id", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("link", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("picture", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("access_token", Type.GetType("System.String")))

                For Each item As FanPage.m_data In fPage1.data
                    If Not item.access_token Is Nothing Then
                        If Not item.link Is Nothing Then
                            If Not item.picture Is Nothing Then
                                Dim dtRow1 As DataRow = dtTable.NewRow
                                dtRow1("name") = item.name.ToString
                                dtRow1("id") = item.id.ToString
                                dtRow1("picture") = item.picture.data.url.ToString
                                dtRow1("access_token") = item.access_token.ToString
                                dtTable.Rows.Add(dtRow1)
                            End If
                        End If
                    End If
                Next
                dtTable.TableName = "fanpages"
                Dim dv As DataView = New DataView(dtTable)
                dv.Sort = "name"

                If dv.Count > 0 Then
                    dstFanPages.DataSource = dv
                    dstFanPages.DataBind()
                    plcData.Visible = True
                    plcNoData.Visible = False
                Else
                    dstFanPages.DataSource = Nothing
                    dstFanPages.DataBind()
                    plcData.Visible = False
                    plcNoData.Visible = True
                End If

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If
        Catch ex As Exception
            lblMessage.Text = "Facebook is experiencing problems at this time. Please save your work and try to publish later."
        End Try
    End Sub
#End Region

    Sub BindCoverPhoto(ByVal intId As Integer)
        'Try
        If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
            Dim dstest As New DataSet
            'Dim str As String = ""
            Dim objBAL As New BALCoverPhoto
            objBAL.CoverPhotoId = intId
            dstest = objBAL.GetPublishedCoverPhotoById
            If dstest.Tables(0).Rows.Count > 0 Then
                divCoverPhotoHtml.InnerHtml = dstest.Tables(0).Rows(0).Item("cp_Content").Replace("{%domain%}/", ConfigurationManager.AppSettings("AppPath"))
                Session("FanPageSelected") = dstest.Tables(0).Rows(0).Item("cp_FBPageId").ToString
                Session("FanPageName") = dstest.Tables(0).Rows(0).Item("cp_FBPageName").ToString
                'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                'Response.Write("Database Page ID : " & dstest.Tables(0).Rows(0).Item("cp_FBPageId").ToString)
                If dstest.Tables(0).Rows(0).Item("cp_IsPublished") = 1 Then
                    BindSelectedFanPages(Session("FanPageSelected"))
                Else
                    BindFanPages()
                End If
                'Response.Write(str)
                'Response.Write("Fan page selected :  " & Session("FanPageSelected"))
                'Next
            End If

        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
        End If
        'Catch ex As Exception
        '    lblMessage.Text = "Error: " & ex.Message
        'End Try
    End Sub

    'Public Function CreateAlbum(accessToken As String) As JsonObject
    '    Dim facebookClient As New FacebookClient(accessToken)
    '    Dim albumParameters As New Dictionary(Of String, Object)()
    '    'albumParameters.Add("message", "My Album message")
    '    albumParameters.Add("name", "Custom CoverPhotos")
    '    Dim resul As JsonObject = TryCast(facebookClient.Post("/me/albums", albumParameters), JsonObject)
    '    Return resul
    'End Function


    Private Sub btnUpload_ServerClick(sender As Object, e As System.EventArgs) Handles btnUpload.ServerClick
        Try
            If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenProgressbar", "ShowProgress();", True)
                Dim AccessToken As String = Session("FacebookAccessToken")
                'Dim photopath As String = ConfigurationManager.AppSettings("AppPath") & ConfigurationManager.AppSettings("uploadpath") & Session("ImageName")
                Dim photopath As String = Server.MapPath("~/" & ConfigurationManager.AppSettings("uploadpath") & Session("ImageName"))
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenProgressbar", "ShowProgress();", True)
                'Response.Write(photopath)
                'Response.End()
                'Dim str As String = ""
                Dim objBAL As New BALCoverPhoto
                objBAL.DeleteCoverPhotoFanPages(Request("id"))
                For Each item As DataListItem In dstFanPages.Items
                    Dim myCheckBox1 As New HtmlInputRadioButton
                    myCheckBox1 = CType(item.FindControl("chkPage"), HtmlInputRadioButton)
                    If myCheckBox1.Checked = True Then
                        Dim path1 As String = CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value & "/albums" '"225274570816748/photos"
                        Dim Albumpath As String = "https://graph.facebook.com/" & CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value & "/albums?fields=id,name&access_token={0}"
                        Dim fbapp1 = New FacebookClient(CType(item.FindControl("hdnAccessToken"), HtmlInputHidden).Value)
                        Dim upload1 As IDictionary(Of String, Object) = New Dictionary(Of String, Object)()
                        Dim get1 As IDictionary(Of String, Object) = New Dictionary(Of String, Object)()
                        Dim request1 As WebRequest = WebRequest.Create(String.Format(Albumpath, CType(item.FindControl("hdnAccessToken"), HtmlInputHidden).Value))
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
                        For i As Integer = 0 To cnt - 1
                            If listAlbums.Item(i).name = "Custom Cover Photos" Then
                                aid = listAlbums.Item(i).id
                                flag = 0
                                Exit For
                            Else
                                flag = 1
                            End If
                        Next

                        If flag = 1 Then
                            upload1.Add("name", "Custom Cover Photos")

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

                            upload.Add("image", mediaObject)
                            Dim fbapp = New FacebookClient(CType(item.FindControl("hdnAccessToken"), HtmlInputHidden).Value)
                            fbapp.Post(path, upload)
                        Else
                            'Response.Write(aid)
                            'Response.End()
                            Dim path As String = aid & "/photos" '"225274570816748/photos"
                            Dim mediaObject As New FacebookMediaObject() With { _
                                .FileName = photopath, _
                                .ContentType = "image/jpg" _
                            }
                            Dim fileBytes As Byte() = File.ReadAllBytes(mediaObject.FileName)
                            mediaObject.SetValue(fileBytes)
                            Dim upload As IDictionary(Of String, Object) = New Dictionary(Of String, Object)()

                            upload.Add("image", mediaObject)
                            Dim fbapp = New FacebookClient(CType(item.FindControl("hdnAccessToken"), HtmlInputHidden).Value)
                            fbapp.Post(path, upload)
                        End If

                        objBAL.UserId = Session("SiteUserId")
                        objBAL.FBUserId = Session("FacebookUserId")
                        objBAL.CoverPhotoId = Request("id")
                        objBAL.FBPageId = CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
                        objBAL.FBPageName = CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value
                        objBAL.FBPageImage = CType(item.FindControl("hdnImage"), HtmlInputHidden).Value
                        objBAL.FBPageAccessToken = CType(item.FindControl("hdnAccessToken"), HtmlInputHidden).Value
                        objBAL.AddCoverPhotoFanPages()

                        Dim obj As New BALCoverPhoto
                        obj.CoverPhotoId = Request("id")
                        obj.FBPageId = CType(item.FindControl("hdnPageId"), HtmlInputHidden).Value
                        obj.FBPageName = CType(item.FindControl("hdnPageName"), HtmlInputHidden).Value
                        obj.UpdateIsPublishedCoverPhoto()
                    End If
                Next

                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseProgressbar", "HideProgress();", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SaveAlert", "SaveAlert();", True)
                'lblMessage.Text = "Photo Uploaded Successfully"
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "GotoHome", "RedirectToHome();", True)
            End If

        Catch ex As Exception
            'lblMessage.Text = "Error: " & ex.Message
            lblMessage.Text = "Facebook is experiencing problems at this time. Please save your work and try to publish later."
        End Try
    End Sub
End Class