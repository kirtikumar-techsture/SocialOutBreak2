﻿Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports BusinessAccessLayer.BusinessLayer
Imports Facebook
Imports System.Threading
Public Class generate_sw_image
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BindCustomTab(Request("id"))
    End Sub

    Public Function BindCustomTab(ByVal intId As Integer)
        'Try
        '    If Session("FacebookAccessToken") <> Nothing AndAlso Session("FacebookAccessToken") <> "" Then
        Dim ds As New DataSet
        Dim objBAL As New BALCustomTab
        objBAL.swid = intId
        'objBAL.UserId = CInt(Request.QueryString("userId"))
        'objBAL.FBUserId = Request.QueryString("FbuserId")
        'objBAL.CompanyId = CInt(Request.QueryString("CId"))
        'objBAL.IndustryId = CInt(Request.QueryString("IId"))
        ds = objBAL.GetPublishedSweepstakeById
        If ds.Tables(0).Rows.Count > 0 Then
            divSidebarHtml.InnerHtml = ds.Tables(0).Rows(0).Item("cs_content").ToString()
        End If
        'Dim obj As New BAlsidebar
        'Dim ds1 As New DataSet
        'obj.Page = CStr(HttpContext.Current.Request.Url.AbsolutePath)
        'ds1 = obj.GetVideoTutorial()
        'If HttpContext.Current.Request.Url.AbsolutePath = ds1.Tables(0).Rows(0).Item("vt_Page").ToString Then
        '    strVideo1.HRef = ds1.Tables(0).Rows(0).Item("vt_Video").ToString
        'End If
        '    Else
        'Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("AppPath") & "login.aspx")
        '    End If
        'Catch ex As Exception
        '    lblMessage.Text = "Error: " & ex.Message
        'End Try
    End Function


End Class