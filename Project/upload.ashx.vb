Imports System
Imports System.Web
Imports System.Web.Services
Imports System.IO
Imports System.Threading
Imports System.Net

Public Class test
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim oFile As HttpPostedFile = context.Request.Files("Filedata")
        Dim strfolder = HttpContext.Current.Request("folder")
        Dim sDirectory As String = HttpContext.Current.Server.MapPath("/uploads/") & strfolder.Replace("undefined", "") & "/" '& strfolder & "/"
        If Not Directory.Exists(sDirectory) Then
            Directory.CreateDirectory(sDirectory)
        End If
        Dim strFileName As String = HttpContext.Current.Request("filename").Replace(" ", "-")
        Dim strPhoto As String
        Dim strDate12 As Date = "1/1/1900"
        strPhoto = "picture-" & CStr(DateDiff(DateInterval.Second, strDate12, Now())) & "-"
        oFile.SaveAs(sDirectory & strPhoto & strFileName)
        'If 
        context.Response.ContentType = "text/plain"
        context.Response.Write(ConfigurationManager.AppSettings("AppPath") & "resize.ashx?P=/uploads/" & strfolder.Replace("undefined", "") & "/" & strPhoto & strFileName & "&D=150x180")

        'Old Code 14 June 2012
        'Dim oFile As HttpPostedFile = context.Request.Files("Filedata")
        '    Dim sDirectory As String = HttpContext.Current.Server.MapPath("/uploads/")
        '    Dim strFileName As String = HttpContext.Current.Request("filename")
        '    oFile.SaveAs(sDirectory & strFileName)
        '    If Not Directory.Exists(sDirectory) Then
        '        Directory.CreateDirectory(sDirectory)
        '    End If
        'context.Response.ContentType = "text/plain"
        'context.Response.Write(ConfigurationManager.AppSettings("AppPath") & "resize.ashx?P=/uploads/" & strFileName & "&D=150x180")
        ''''context.Response.Write(ConfigurationManager.AppSettings("AppPath") & "resize-tabs.ashx?P=/uploads/" & strFileName & "&D=150x180")
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class