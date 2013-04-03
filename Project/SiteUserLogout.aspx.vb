Public Class logout1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("SiteUserId") = 0
        Session("SiteUserName") = ""
        Session("FacebookAccessToken") = ""
        Session("FacebookUserId") = ""
        Session("CompanyId") = 0
        Session("IndustryId") = 0
        Session.Clear()
        Session.Abandon()
        Response.Redirect(ConfigurationManager.AppSettings("AppPath"))
    End Sub

End Class