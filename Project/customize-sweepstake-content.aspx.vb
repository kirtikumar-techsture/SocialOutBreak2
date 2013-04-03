Imports BusinessAccessLayer.BusinessLayer
Imports System.Runtime.Serialization.Json
Public Class customize_sweepstake_content
    Inherits System.Web.UI.Page
    Dim objCustomTab As New CustomTabContent
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strContent As String = ""
            Dim payload As String = Request.Form("signed_request").Split("."c)(1)
            Dim encoding = New UTF8Encoding()
            Dim decodedJson = payload.Replace("=", String.Empty).Replace("-"c, "+"c).Replace("_"c, "/"c)

            Dim base64JsonArray = Convert.FromBase64String(decodedJson.PadRight(decodedJson.Length + (4 - decodedJson.Length Mod 4) Mod 4, "="c))
            Dim json = encoding.GetString(base64JsonArray)
            Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(json))
            Dim dataContractJsonSerializer As New DataContractJsonSerializer(GetType(CustomTab))
            Dim cTab As New CustomTab()
            cTab = TryCast(dataContractJsonSerializer.ReadObject(stream), CustomTab)
            objCustomTab.FBPageId = cTab.page.id
            Dim liked = cTab.page.liked
            objCustomTab.FBApplicationId = ConfigurationManager.AppSettings("CustomizeSweepStakeFBAPI520").ToString()
            FBApplicationId.Value = ConfigurationManager.AppSettings("CustomizeSweepStakeFBAPI520").ToString()
            objCustomTab.GetSweepstakeContent()
            divLiked1.InnerHtml = objCustomTab.Content.Replace("<br/>", "").Replace("notextedit", "").Replace("setLocation", "")
            'divUnliked1.InnerHtml = objCustomTab.UnlikeContent.Replace("<br/>", "").Replace("notextedit", "").Replace("setLocation", "")
            hdnFBUserId.Value = objCustomTab.FBUserId
            hdnFBPageName.Value = objCustomTab.FBPageName
            hdnFBPageId.Value = objCustomTab.FBPageId
            hdnShareMessage.Value = objCustomTab.ShareMessage
            hdnShareText.Value = objCustomTab.ShareText
            hdnShareImage.Value = objCustomTab.ShareImage
            hdnPDFlink.Value = objCustomTab.PdfLink
            hdnEmailText.Value = objCustomTab.EmailText
            Dim strlink As String
            Dim FBAPPID As String
            FBAPPID = System.Configuration.ConfigurationManager.AppSettings("CustomizeSweepStakeFBAPI520").ToString
            Dim fbappname As String = System.Configuration.ConfigurationManager.AppSettings("CustomizeSweepStakeFBAPIName").ToString
            strlink = "http://www.facebook.com/" & hdnFBPageId.Value
            'strlink = "http://www.facebook.com/pages/" + hdnFBPageName.Value + "/" + hdnFBPageId.Value + "?sk=app_" + FBAPPID
            linkpage.HRef = strlink
            hdnURL.Value = strlink
            hdnSweepStakeAppId.Value = FBAPPID
            hdnSweepStakeAppName.value = fbappname
            If liked.ToString().Trim().ToLower() = "true" Then
                ViewState("likeunlike") = "1"
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "LikeAlert", "LikeAlert();", True)
                'divLiked1.Visible = True
                'divUnliked1.Visible = False
            Else
                ViewState("likeunlike") = "0"
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UnLikeAlert", "UnLikeAlert();", True)
                'divLiked1.Visible = False
                'divUnliked1.Visible = True
            End If
            hdnlikeunlike.Value = ViewState("likeunlike")
        Catch ex As Exception
        End Try
    End Sub

End Class