Imports System.Net
Imports System.Runtime.Serialization.Json
Imports BusinessAccessLayer.BusinessLayer
Imports System.IO
Imports Facebook
Imports System.Threading
Imports AjaxControlToolkit

Public Class rajni_post2
    Inherits RoutablePage
    Implements IRequiresSessionState

#Region "Variable Declaration"
    Dim objLib As New Library
    Public strSelectionType As String = ""
    Public intAutoPostOnOff As Integer
    Dim strFileName As String = ""
#End Region

#Region "Page Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoginCheck.LoginSessionCheck()
        hdnFBUserId.Value = Convert.ToString(Session("FacebookUserId"))
        strFileName = Convert.ToString(Session("FacebookUserId"))
        If Not Page.IsPostBack Then
            BindQuickPostFanPages()
            'BindUsersLibraries()
        End If
    End Sub
#End Region

#Region "Bind Quick Post Fanpages"
    Private Sub BindQuickPostFanPages()
        Dim accessToken As String = Session("FacebookAccessToken")
        Dim clientId As String = System.Configuration.ConfigurationManager.AppSettings("FBAppId").ToString()
        Dim clientSecret As String = System.Configuration.ConfigurationManager.AppSettings("FBSecretKey").ToString()
        'Fan Pages List Retrieval
        Dim url As String = "https://graph.facebook.com/me/accounts?fields=id,name,picture,category,access_token&return_ssl_resources=true&access_token={0}"
        Dim request As WebRequest = WebRequest.Create(String.Format(url, accessToken))
        Dim response1 As WebResponse = request.GetResponse()
        Dim stream As Stream = response1.GetResponseStream()
        Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(FanPage))
        Dim fPage As New FanPage()
        fPage = TryCast(dataContractJsonSerializer.ReadObject(stream), FanPage)

        'User's Profile Retrieval
        Dim url1 As String = "https://graph.facebook.com/me?fields=id,name,picture&return_ssl_resources=true&access_token={0}"
        Dim request1 As WebRequest = WebRequest.Create(String.Format(url1, accessToken))
        Dim response2 As WebResponse = request1.GetResponse()
        Dim stream1 As Stream = response2.GetResponseStream()
        Dim dataContractJsonSerializer2 As New DataContractJsonSerializer(GetType(FacebookUser))
        Dim fUser As New FacebookUser()
        fUser = TryCast(dataContractJsonSerializer2.ReadObject(stream1), FacebookUser)

        Dim dtTable As New DataTable
        dtTable.Columns.Add(New DataColumn("name", Type.GetType("System.String")))
        dtTable.Columns.Add(New DataColumn("id", Type.GetType("System.String")))
        dtTable.Columns.Add(New DataColumn("picture", Type.GetType("System.String")))
        dtTable.Columns.Add(New DataColumn("access_token", Type.GetType("System.String")))
        Dim dtRow As DataRow = dtTable.NewRow
        dtRow("name") = fUser.name
        dtRow("id") = fUser.id
        dtRow("picture") = fUser.picture
        dtRow("access_token") = Session("FacebookAccessToken")
        dtTable.Rows.Add(dtRow)


        For Each item As FanPage.m_data In fPage.data
            If Not item.access_token Is Nothing Then
                If Not item.picture Is Nothing Then
                    Dim dtRow1 As DataRow = dtTable.NewRow
                    dtRow1("name") = item.name.ToString
                    dtRow1("id") = item.id.ToString
                    dtRow1("picture") = item.picture.data.url.ToString
                    dtRow1("access_token") = item.access_token.ToString
                    dtTable.Rows.Add(dtRow1)
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
    End Sub
#End Region

#Region "File Upload"
    Private Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload1.UploadedComplete
        Dim savePath As String
        savePath = Server.MapPath("/content/uploads/images/") & Convert.ToString(Session("FacebookUserId")) & "-" & Path.GetFileName(e.FileName)
        AsyncFileUpload1.SaveAs(savePath)
    End Sub

    Private Sub AsyncFileUpload1_UploadedFileError(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload1.UploadedFileError
        'ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "size", "alert('Uploaded size: " & AsyncFileUpload1.FileBytes.Length.ToString() & "');", True)
    End Sub
#End Region

#Region "Library"
    Sub BindUsersLibraries()
        Dim ds As DataSet
        objLib.UserId = Session("SiteUserId")
        objLib.FBUserId = Session("FacebookUserId")
        objLib.CompanyId = GetSetCookies.GetCookie("CompanyId") '0 
        objLib.IndustryId = GetSetCookies.GetCookie("IndustryId") ' 1 
        ds = objLib.GetUsersLibraries()
        rptLibUserCatList.DataSource = ds.Tables(0)
        rptLibUserCatList.DataBind()
    End Sub
#End Region

End Class