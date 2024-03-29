﻿Imports System.Web.SessionState
Imports System.Web.Routing
Imports System.Runtime.Hosting
Imports System.Runtime

Public Class Global_asax
    Inherits System.Web.HttpApplication
    Private Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.Add(New Route("{resource}.axd/{*pathInfo}", New StopRoutingHandler()))
        '' Add an unnamed handler for simple requests     
        routes.Add("Home", New Route("home", New WPageHandler("~/default.aspx")))
        'routes.Add("custom-tab", New Route("custom-tab", New WPageHandler("~/custom-tab.aspx")))
        routes.Add("handshake", New Route("handshake", New WPageHandler("~/handshake.aspx")))
        routes.Add("SiteUserLogin", New Route("siteuserlogin", New WPageHandler("~/siteuserlogin1.aspx")))
        routes.Add("SiteUserLoginLocal", New Route("siteuserloginlocal", New WPageHandler("~/siteuserlogin.aspx")))
        routes.Add("SiteUserLogout", New Route("siteuserlogout", New WPageHandler("~/SiteUserLogout.aspx")))
        routes.Add("Friendlist", New Route("friendlist", New WPageHandler("~/friendlist.aspx")))
        routes.Add("Fanpages", New Route("fanpages", New WPageHandler("~/fanpages.aspx")))
        routes.Add("Quickpost", New Route("quickpost", New WPageHandler("~/quickpost.aspx")))
        routes.Add("SchedulerEdit", New Route("scheduler-main/{sm_Id}", New WPageHandler("~/scheduler-new.aspx")))
        routes.Add("AutoPostEdit", New Route("scheduler/{apm_Id}", New WPageHandler("~/scheduler.aspx")))
        routes.Add("Create Sidebar", New Route("create-sidebar", New WPageHandler("~/sidebar.aspx")))
        routes.Add("Upload Sidebar", New Route("upload-sidebar", New WPageHandler("~/upload-sidebar.aspx")))
        routes.Add("Sidebar Templates", New Route("sidebar-templates", New WPageHandler("~/sidebar-templates.aspx")))
        routes.Add("SchedulerHome", New Route("scheduler", New WPageHandler("~/scheduler-home.aspx")))
        routes.Add("Scheduler", New Route("scheduler-main", New WPageHandler("~/scheduler-new.aspx")))
        routes.Add("Drafts", New Route("drafts", New WPageHandler("~/drafts.aspx")))
        routes.Add("Templates", New Route("templates", New WPageHandler("~/templates.aspx")))
        routes.Add("Pending Messages", New Route("pending-messages", New WPageHandler("~/pending-messages.aspx")))
        routes.Add("Sent Messages", New Route("sent-messages", New WPageHandler("~/sent-messages.aspx")))
        routes.Add("admin", New Route("admin", New WPageHandler("~/admin/login.aspx")))
        routes.Add("Edit Drafts", New Route("editdrafts/{df_Id}/{df_PageId}", New WPageHandler("~/edit-drafts.aspx")))
        routes.Add("setup-page", New Route("setup-page", New WPageHandler("~/setup-page.aspx")))
        routes.Add("cover-photo", New Route("cover-photo", New WPageHandler("~/cover-photos-home.aspx")))
        routes.Add("sidebar", New Route("sidebar", New WPageHandler("~/sidebar-home.aspx")))
        routes.Add("custom-tab", New Route("custom-tab", New WPageHandler("~/custom-tab-home.aspx")))
        routes.Add("create-custom-tab", New Route("create-custom-tab", New WPageHandler("~/custom-tabs.aspx")))
        routes.Add("create-cover-photo", New Route("create-cover-photo", New WPageHandler("~/cover-photos.aspx")))
        routes.Add("sweepstakes", New Route("sweepstakes", New WPageHandler("~/sweepstake-home.aspx")))
        routes.Add("create-sweepstake", New Route("create-sweepstake", New WPageHandler("~/sweepstake.aspx")))
        routes.Add("Training", New Route("training", New WPageHandler("~/training.aspx")))
        routes.Add("Support", New Route("support", New WPageHandler("~/support.aspx")))
        routes.Add("express-sidebar", New Route("express-sidebar", New WPageHandler("~/express-sidebar.aspx")))
        routes.Add("express-customtab", New Route("express-customtab", New WPageHandler("~/express-customtab.aspx")))
        routes.Add("express-blank", New Route("express-blank", New WPageHandler("~/express-blank.aspx")))
        routes.Add("local-user", New Route("local-user", New WPageHandler("~/local-user.aspx")))
        routes.Add("view-all-leads", New Route("view-all-leads", New WPageHandler("~/view-all-leads.aspx")))
        routes.Add("dashboard", New Route("dashboard", New WPageHandler("~/dashboard.aspx")))
        routes.Add("view-autopost", New Route("view-autopost", New WPageHandler("~/view-autopost.aspx")))
        routes.Add("configure-autopost", New Route("configure-autopost", New WPageHandler("~/configure-autopost.aspx")))
        routes.Add("sweepstake-main", New Route("sweepstake-main", New WPageHandler("~/sweepstake-main.aspx")))
        routes.Add("customize-sweepstake", New Route("customize-sweepstake", New WPageHandler("~/customize-sweepstake.aspx")))
        routes.Add("selected-sweepstake", New Route("selected-sweepstake", New WPageHandler("~/selected-sweepstake.aspx")))
        routes.Add("sweepstake-list", New Route("sweepstake-list", New WPageHandler("~/sweepstake-list.aspx")))
        routes.Add("custom-tabs-leads-admin", New Route("custom-tabs-leads-admin", New WPageHandler("~/custom-tabs-leads-admin.aspx")))
        routes.Add("custom-tabs-leads", New Route("custom-tabs-leads", New WPageHandler("~/custom-tabs-leads.aspx")))
        routes.Add("sweepstakes-leads-admin", New Route("sweepstakes-leads-admin", New WPageHandler("~/sweepstakes-leads-admin.aspx")))
        routes.Add("sweepstakes-leads", New Route("sweepstakes-leads", New WPageHandler("~/sweepstakes-leads.aspx")))
        routes.Add("lead-tutorial", New Route("lead-tutorial", New WPageHandler("~/lead-tutorial.aspx")))
        routes.Add("engage-tutorial", New Route("engage-tutorial", New WPageHandler("~/engage-tutorial.aspx")))

    End Sub
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        RegisterRoutes(RouteTable.Routes)
        runConsole()
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub
    Private Sub runConsole()

    End Sub
End Class