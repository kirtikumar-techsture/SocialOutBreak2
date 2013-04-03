Imports System.Configuration
Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Runtime.Serialization.Json
Imports Facebook
Imports BusinessAccessLayer.BusinessLayer
Public Class uleft
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        Dim ds As New DataSet
        Dim objBAL As New BALSchedulePost
        objBAL.FBUserId = Session("FacebookUserId")
        ds = objBAL.GetAllDrafts
        If ds.Tables(1).Rows.Count > 0 Then
            ltrdrafts.Text = "(" & ds.Tables(1).Rows(0).Item("DraftCount").ToString & ")"

        Else
            ltrdrafts.Text = "(0)"
        End If
     

    End Sub

End Class