Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Public Class rotate_image
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub btnUpload_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.ServerClick
        Dim strExt As String = ""
        Dim strPhoto As String = ""
        If flImage.PostedFile.ContentLength > 0 Then
            Dim uploadContent = flImage.PostedFile.ContentLength / 1000
            strExt = IO.Path.GetExtension(flImage.PostedFile.FileName).ToLower
            If strExt = ".jpg" Or strExt = ".gif" Or strExt = ".bmp" Or strExt = ".jpeg" Or strExt = ".png" Then
                Dim strdate As Date = "1/1/1900"
                strPhoto = "picture-" & CStr(DateDiff(DateInterval.Second, strdate, Now())) & strExt
                ViewState("strPhoto") = strPhoto
                flImage.PostedFile.SaveAs(Server.MapPath("~/" & "Content/uploads/resize/" & strPhoto))

                imgRotateImg.Src = "Content/uploads/resize/" & strPhoto
            Else
                lblMessage.Visible = True
                lblMessage.Text = "File must be .jpg or .gif or .png or .jpeg or .tif or .bmp"
            End If
        End If
        'photopath = Server.MapPath("~/" & "Content/uploads/images/" & strPhoto)

        'imgPhoto.Src = photopath
    End Sub
    Private Sub btnRotateImg_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRotateImg.ServerClick
        Dim RotatedImage As String = Server.MapPath(("~/" & "Content/uploads/resize/" & ViewState("strPhoto")))
        ' creating image from the image url
        Dim i As System.Drawing.Image = System.Drawing.Image.FromFile(RotatedImage)
        ' rotate Image 90' Degree
        i.RotateFlip(RotateFlipType.Rotate90FlipXY)
        ' save it to its actual path
        i.Save(RotatedImage)
        lnkDownload.HRef = "download.aspx?Image=" & ViewState("strPhoto")
        ' release Image File
        i.Dispose()
    End Sub
End Class