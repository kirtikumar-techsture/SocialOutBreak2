﻿Imports System.Configuration
Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Runtime.Serialization.Json
Imports Facebook
Imports BusinessAccessLayer.BusinessLayer
Public Class manage_users
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                BindGrid()
            Catch ex As Exception
                ltrMsg.Text = "Error: " & ex.Message.ToString
            End Try
        End If

    End Sub


    Sub BindGrid()
        Try
            Dim objViewUsers As New BALSiteUser
            Dim ds As DataSet = objViewUsers.BindUsers()
            If ds.Tables(0).Rows.Count > 0 Then
            dgrSystemUsers.DataSource = ds.Tables(0)
            dgrSystemUsers.DataBind()
            trNote.Visible = True
            ltrMsg.Text = ""
            Else
            dgrSystemUsers.DataSource = Nothing
            dgrSystemUsers.DataBind()
            ltrMsg.Text = "No record found."
            trNote.Visible = False
            End If
        Catch ex As Exception
            ltrMsg.Text = "Error: " & ex.Message.ToString
        End Try
    End Sub

    Sub ChangeStatus(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If sender.commandName <> "" And sender.commandArgument <> "" Then
                Dim dt As New DataSet
                Dim objStatusUsers As New BALSiteUser
                objStatusUsers.UserId = CInt(sender.commandName)
                objStatusUsers.Status = CInt(sender.commandArgument)
                dt = objStatusUsers.ChangeSiteUserStatus()
                If CInt(sender.CommandArgument) < 2 Then
                    BindGrid()
                    ltrMsg.Text = "SiteUser Status Changed Successfully."
                Else
                    BindGrid()
                    ltrMsg.Text = "SiteUser Deleted Successfully."
                End If
            End If
        Catch ex As Exception
            ltrMsg.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub dgrSystemUsers_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgrSystemUsers.PageIndexChanged
        dgrSystemUsers.CurrentPageIndex = e.NewPageIndex
        'dgrSystemUsers.SelectedIndex = -1
        BindGrid()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim objViewUsers As New BALSiteUser
            objViewUsers.Search = txtSearch.Value
            Dim ds As DataSet = objViewUsers.SearchUsers()
            If ds.Tables(0).Rows.Count > 0 Then
                dgrSystemUsers.DataSource = ds.Tables(0)
                dgrSystemUsers.DataBind()
                ltrMsg.Text = ""
            Else
                dgrSystemUsers.DataSource = Nothing
                dgrSystemUsers.DataBind()
                ltrMsg.Text = "No Record Found......"
            End If
        Catch ex As Exception
            ltrMsg.Text = "Error: " & ex.Message
        End Try
    End Sub
End Class