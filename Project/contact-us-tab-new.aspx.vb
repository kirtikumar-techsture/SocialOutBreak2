Imports BusinessAccessLayer.BusinessLayer
Imports System.Runtime.Serialization.Json
Imports System.IO
Public Class contact_us_tab_new
    Inherits System.Web.UI.Page
    Dim objCustomTab As New CustomTabContent
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim obj As New CustomTabContent
            obj.SubCatId = 8
            Dim ds As New DataSet
            ds = obj.getFacebookAppIdbyPage()
            Dim strAppId As String = ds.Tables(0).Rows(0).Item("FBAppId")
            Dim strAppName As String = ds.Tables(0).Rows(0).Item("FBAppName")

            If Request.QueryString("fbpageid") <> "" AndAlso Not IsNothing(Request.QueryString("fbpageid")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, ";SetBodyStyle;", "SetBodyStyle();", True)
                objCustomTab.FBPageId = Request.QueryString("fbpageid")
            Else
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
            End If

            objCustomTab.FBApplicationId = Trim(strAppId)
            objCustomTab.GetCustomTabContent()
            divContent.InnerHtml = objCustomTab.Content.Replace("<br/>", "").Replace("notextedit", "").Replace("setLocation", "")
            hdnFBUserId.Value = objCustomTab.FBUserId
            hdnFBPageName.Value = objCustomTab.FBPageName
            hdnFBPageId.Value = objCustomTab.FBPageId
            hdnAppId.Value = Trim(strAppId)
            hdnAppName.Value = Trim(strAppName)
        Catch ex As Exception
        End Try
    End Sub

End Class