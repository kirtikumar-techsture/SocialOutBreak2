Imports System.Configuration
Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Runtime.Serialization.Json
Imports Facebook
Imports BusinessAccessLayer.BusinessLayer
Public Class headerleft
    Inherits System.Web.UI.UserControl
    Dim objBAL As New BALSchedulePost
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CInt(GetSetCookies.GetCookie("CompanyId")) = 12 Then
            trLegacy.Visible = True
            trAmerifirst1.Visible = False
            trAmerifirst2.Visible = False
        ElseIf CInt(GetSetCookies.GetCookie("CompanyId")) = 13 Then
            trAmerifirst1.Visible = True
            trAmerifirst2.Visible = True
            trLegacy.Visible = False
        ElseIf CInt(GetSetCookies.GetCookie("CompanyId")) = 15 Then
            trBoresha.Visible = True
        Else
            bind_menu()
            trLegacy.Visible = False
            trAmerifirst1.Visible = False
            trAmerifirst2.Visible = False
        End If



        ' If Not IsPostBack Then
        ' GetExpressMenu()
        'Dim dsLeftMenu As New DataSet
        'Dim objLeftMenu As New BALLeftMenu

        'objLeftMenu.UserID = Session("SiteUserId")
        'objLeftMenu.CompanyID = GetSetCookies.GetCookie("CompanyId")
        'objLeftMenu.IndustryID = GetSetCookies.GetCookie("IndustryId")
        'dsLeftMenu = objLeftMenu.GetLeftMenuItem()
        'rptLeftMenu.DataSource = dsLeftMenu.Tables(0)
        'rptLeftMenu.DataBind()



        ''Access Rights 
        'Dim dsAllMenu As New DataSet
        'Dim objAllLeftMenu As New BALLeftMenu

        'objAllLeftMenu.UserID = Session("SiteUserId")
        'objAllLeftMenu.CompanyID = GetSetCookies.GetCookie("CompanyId")
        'objAllLeftMenu.IndustryID = GetSetCookies.GetCookie("IndustryId")
        'dsAllMenu = objAllLeftMenu.GetAllLeftMenu()
        'Dim flag As Integer
        'If dsAllMenu.Tables(0).Rows.Count > 0 Then
        '    'For i As Integer = 0 To dsAllMenu.Tables(0).Rows.Count - 1
        '    '    If CStr((HttpContext.Current.Request.Url.AbsolutePath).Replace("/", "")) = dsAllMenu.Tables(0).Rows(i).Item("mnu_Link").ToString() Then
        '    '        flag = 0
        '    '        Exit For
        '    '    Else
        '    '        flag = 1
        '    '    End If

        '    'Next

        'End If
        'If flag = 1 Then
        '    ' Response.Redirect("Default.aspx")
        '    'Else
        '    '    Response.Redirect("scheduler")
        'End If



        'Dim dsSubMenu As New DataSet
        'Dim objsubmenu As New BALLeftMenu
        'objsubmenu.UserID = Session("SiteUserId")
        'dsSubMenu = objsubmenu.GetLeftSubMenuItem()
        'If dsSubMenu.Tables(0).Rows.Count > 0 Then
        '    rptLeftSubMenu.DataSource = dsSubMenu.Tables(0)
        '    rptLeftSubMenu.DataBind()
        'Else

        'End If
        'Dim ds, DsLeftMenu As New DataSet

        'objBAL.FBUserId = Session("FacebookUserId")
        ''Dim dsdrafts As New DataSet
        ''dsdrafts = objBAL.GetDrafts
        ''If dsdrafts.Tables(0).Rows.Count > 0 Then
        ''    ltrdrafts.Text = "(" & dsdrafts.Tables(0).Rows.Count & ")"

        ''Else
        ''    ltrdrafts.Text = "(0)"
        ''End If

        ''Dim dstemp As New DataSet


        ''dstemp = objBAL.GetTemplates
        ''If dstemp.Tables(0).Rows.Count > 0 Then
        ''    ltrtemplates.Text = "(" & dstemp.Tables(0).Rows.Count & ")"

        ''Else
        ''    ltrtemplates.Text = "(0)"
        ''End If

        ''Dim dspending As New DataSet


        ''dspending = objBAL.GetPendingMess
        ''If dspending.Tables(0).Rows.Count > 0 Then
        ''    ltrpending.Text = "(" & dspending.Tables(0).Rows.Count & ")"

        ''Else
        ''    ltrpending.Text = "(0)"
        ''End If


        'objBAL.FBUserId = Session("FacebookUserId")
        'DsLeftMenu = objBAL.GetLeftMenuItem
        'rptLeftMenu.DataSource = DsLeftMenu.Tables(0)
        'rptLeftMenu.DataBind()

        'ds = objBAL.GetLeftSubMenuItem
        'If ds.Tables(0).Rows.Count > 0 Then
        '    rptLeftSubMenu.DataSource = ds.Tables(0)
        '    rptLeftSubMenu.DataBind()
        'Else

        'End If
        '  End If
    End Sub

    'Function GetSubMenu(ByVal menuid As Integer) As DataTable

    '    Dim dsSubMenu As New DataSet
    '    Dim objsubmenu As New BALLeftMenu
    '    objsubmenu.UserID = Session("SiteUserId")
    '    objsubmenu.CompanyID = GetSetCookies.GetCookie("CompanyId")
    '    objsubmenu.IndustryID = GetSetCookies.GetCookie("IndustryId")
    '    objsubmenu.UserID = Session("SiteUserId")
    '    objsubmenu.MenuID = menuid
    '    dsSubMenu = objsubmenu.GetLeftSubMenuItem()
    '    'If dsSubMenu.Tables(0).Rows.Count > 0 Then
    '    '    rptLeftSubMenu.DataSource = dsSubMenu.Tables(0)
    '    '    rptLeftSubMenu.DataBind()
    '    'Else

    '    'End If
    '    Return dsSubMenu.Tables(0)
    'End Function




    'Sub GetExpressMenu()
    '    Dim obj As New BALLeftMenu
    '    Dim ds As New DataSet
    '    obj.UserID = Session("SiteUserId")
    '    obj.CompanyID = GetSetCookies.GetCookie("CompanyId")
    '    obj.IndustryID = GetSetCookies.GetCookie("IndustryId")
    '    obj.FBUserId = Session("FacebookUserId")
    '    ds = obj.GetExpressMenus()

    '    If ds.Tables(0).Rows.Count > 0 Then
    '        SBURL.HRef = "express-sidebar.aspx?id=" & ds.Tables(0).Rows(0).Item("sdm_Id").ToString '& "&userId=" & Session("SiteUserId") & "&FbuserId=" & Session("FacebookUserId") & "&CId=" & GetSetCookies.GetCookie("CompanyId") & "&IId=" & GetSetCookies.GetCookie("IndustryId")
    '    End If
    '    If ds.Tables(1).Rows.Count > 0 Then
    '        CTURL.HRef = "express-customtab.aspx?id=" & ds.Tables(1).Rows(0).Item("ctm_Id").ToString '& "&userId=" & Session("SiteUserId") & "&FbuserId=" & Session("FacebookUserId") & "&CId=" & GetSetCookies.GetCookie("CompanyId") & "&IId=" & GetSetCookies.GetCookie("IndustryId")
    '    End If

    'End Sub
    Private Sub bind_menu()
        Dim obj As New BALLeftMenu
        obj.CompanyID = GetSetCookies.GetCookie("CompanyId")
        obj.IndustryID = GetSetCookies.GetCookie("IndustryId")
        ltrMenu.Text = obj.getLeftmenu
    End Sub
End Class