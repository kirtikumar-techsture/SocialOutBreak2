
   Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System.Threading
Public Class rajni_generateimage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BindCustomTab(Request("id"))
    End Sub

    Public Sub BindCustomTab(ByVal intId As Integer)
        'Try
        '    If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
        Dim ds As New DataSet
        Dim objBAL As New BALCustomTab
        objBAL.CustomTabId = intId
        ds = objBAL.GetPublishedCustomTabByIdtemp
        If ds.Tables(0).Rows.Count > 0 Then
            divSidebarHtml.InnerHtml = ds.Tables(0).Rows(0).Item("ct_Content")
        End If
    End Sub


End Class