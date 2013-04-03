Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Web
Imports System.Web.Services

Public Class resize
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        ' Try
        With context.Request
            Dim strPath As String = .QueryString("P")
            Dim strData As String()
            strData = .QueryString("D").ToLower.Split("x")
            Dim strWidth As String = strData(0)
            Dim strHeight As String = strData(1)

            Dim intResizeType As String = "1"
            If strData.Length > 2 Then
                intResizeType = strData(2)
            End If
            Dim strFilePath As String = context.Server.MapPath("~\" & strPath)

            context.Response.Clear()
            context.Response.ContentType = "image/jpeg"
            ResizeImageFile(strFilePath, CInt(strHeight), CInt(strWidth), Convert.ToSingle(intResizeType)).WriteTo(context.Response.OutputStream)
            context.Response.End()
        End With
        'Catch ex As Exception

        'End Try
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

#Region "Old"
    Private Function ResizeImageFile(ByVal strFileName As String, ByVal height As Integer, ByVal width As Integer, ByVal intResizeType As Single) As MemoryStream
        'Using imgPhoto As Image = System.Drawing.Image.FromFile(strFileName)
        '    Dim sourceWidth As Integer = imgPhoto.Width
        '    Dim sourceHeight As Integer = imgPhoto.Height
        '    Dim sourceX As Integer = 0
        '    Dim sourceY As Integer = 0
        '    Dim destX As Integer = 0
        '    Dim destY As Integer = 0

        '    Dim nPercent As Single = 0
        '    Dim nPercentW As Single = 0
        '    Dim nPercentH As Single = 0

        '    nPercentW = (CSng(width) / CSng(sourceWidth))
        '    nPercentH = (CSng(height) / CSng(sourceHeight))
        '    If nPercentH < nPercentW Then
        '        nPercent = nPercentH
        '        destX = System.Convert.ToInt16((width - (sourceWidth * nPercent)) / 2)
        '    Else
        '        nPercent = nPercentW
        '        destY = System.Convert.ToInt16((height - (sourceHeight * nPercent)) / 2)
        '    End If

        '    Dim destWidth As Integer = CInt(Math.Truncate(sourceWidth * nPercent))
        '    Dim destHeight As Integer = CInt(Math.Truncate(sourceHeight * nPercent))

        '    Dim bmPhoto As New Bitmap(width, height, PixelFormat.Format32bppArgb)
        '    bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)

        '    Dim grPhoto As Graphics = Graphics.FromImage(bmPhoto)
        '    'change the color of the image padding
        '    'grPhoto.Clear(Color.White);
        '    grPhoto.Clear(Color.Transparent)

        '    grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic

        '    grPhoto.DrawImage(imgPhoto, New Rectangle(destX, destY, destWidth, destHeight), New Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel)
        '    Dim m As New MemoryStream()
        '    bmPhoto.Save(m, System.Drawing.Imaging.ImageFormat.Bmp)

        '    'grPhoto.Dispose()
        '    Return m


        'End Using
        Using oldImage As Image = System.Drawing.Image.FromFile(strFileName)
            Dim oldwidth As Single = oldImage.Width
            Dim oldheight As Single = oldImage.Height

            Dim thumbHeight As Integer = height
            Dim thumbWidth As Integer = width

            If intResizeType = 1 Then
                If width > height Then
                    Dim def As Double = width
                    Dim imgwidth As Double = Convert.ToDouble(oldwidth)
                    Dim diff As Double = (def / imgwidth)
                    thumbWidth = Convert.ToInt16(Math.Round(diff * oldwidth))
                    thumbHeight = Convert.ToInt16(Math.Round(diff * oldheight))
                    If thumbHeight < height Then
                        Dim def1 As Double = height
                        Dim imgheight As Double = Convert.ToDouble(oldheight)
                        Dim diff1 As Double = (def1 / imgheight)
                        thumbWidth = Convert.ToInt16(Math.Round(diff1 * oldwidth))
                        thumbHeight = Convert.ToInt16(Math.Round(diff1 * oldheight))
                    End If
                ElseIf height > width Then
                    Dim def As Double = height
                    Dim imgheight As Double = Convert.ToDouble(oldheight)
                    Dim diff As Double = (def / imgheight)
                    thumbWidth = Convert.ToInt16(Math.Round(diff * oldwidth))
                    thumbHeight = Convert.ToInt16(Math.Round(diff * oldheight))
                    If thumbWidth < width Then
                        Dim def1 As Double = width
                        Dim imgwidth As Double = Convert.ToDouble(oldwidth)
                        Dim diff1 As Double = (def1 / imgwidth)
                        thumbWidth = Convert.ToInt16(Math.Round(diff1 * oldwidth))
                        thumbHeight = Convert.ToInt16(Math.Round(diff1 * oldheight))
                    End If
                Else
                    Dim def As Double = width
                    Dim imgwidth As Double = Convert.ToDouble(oldwidth)
                    Dim diff As Double = (def / imgwidth)
                    thumbWidth = Convert.ToInt16(Math.Round(diff * oldwidth))
                    thumbHeight = Convert.ToInt16(Math.Round(diff * oldheight))
                    If thumbHeight < height Then
                        Dim def1 As Double = height
                        Dim imgheight As Double = Convert.ToDouble(oldheight)
                        Dim diff1 As Double = (def1 / imgheight)
                        thumbWidth = Convert.ToInt16(Math.Round(diff1 * oldwidth))
                        thumbHeight = Convert.ToInt16(Math.Round(diff1 * oldheight))
                    End If
                End If
            Else
                thumbWidth = oldwidth
                thumbHeight = oldheight
            End If

            Dim newSize As New Size(thumbWidth + 3, thumbHeight + 3)
            Using newImage As New Bitmap(newSize.Width - 3, newSize.Height - 3, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                Using canvas As Graphics = Graphics.FromImage(newImage)
                    canvas.Clear(Color.Transparent)
                    canvas.SmoothingMode = SmoothingMode.AntiAlias
                    canvas.InterpolationMode = InterpolationMode.HighQualityBicubic
                    canvas.PixelOffsetMode = PixelOffsetMode.HighQuality
                    canvas.DrawImage(oldImage, New Rectangle(New Point(-1, -1), newSize))
                    Dim m As New MemoryStream()
                    newImage.Save(m, System.Drawing.Imaging.ImageFormat.Png)
                    Return m
                End Using
            End Using
        End Using
    End Function
#End Region

#Region "New"
    Private Function ResizeImageFile_new(ByVal strFileName As String, ByVal Height As Integer, ByVal Width As Integer, ByVal intResizeType As Single) As MemoryStream
        Using oldImage As Image = System.Drawing.Image.FromFile(strFileName)
            Dim oldwidth As Single = oldImage.Width
            Dim oldheight As Single = oldImage.Height

            Dim thumbHeight As Integer = height
            Dim thumbWidth As Integer = width

            If (intResizeType = 1) Then
                If (oldwidth > width) Then
                    Dim def As Double = width
                    Dim imgwidth As Double = Convert.ToDouble(oldwidth)
                    Dim diff As Double = (def / imgwidth)
                    thumbWidth = Convert.ToInt16(Math.Round(diff * oldwidth))
                    thumbHeight = Convert.ToInt16(Math.Round(diff * oldheight))
                Else
                    thumbWidth = oldwidth
                    thumbHeight = oldheight
                End If

                If (thumbHeight > height) Then
                    Dim def As Double = height
                    Dim imgheight As Double = Convert.ToDouble(thumbHeight)
                    Dim diff As Double = (def / imgheight)
                    thumbWidth = Convert.ToInt16(Math.Round(diff * thumbWidth))
                    thumbHeight = height
                End If
            Else
                thumbWidth = oldwidth
                thumbHeight = oldheight
            End If

            Dim newSize As New Size(thumbWidth + 3, thumbHeight + 3)
            Using newImage As New Bitmap(newSize.Width - 3, newSize.Height - 3, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                Using canvas As Graphics = Graphics.FromImage(newImage)
                    canvas.Clear(Color.Transparent)
                    canvas.SmoothingMode = SmoothingMode.AntiAlias
                    canvas.InterpolationMode = InterpolationMode.HighQualityBicubic
                    canvas.PixelOffsetMode = PixelOffsetMode.HighQuality
                    canvas.DrawImage(oldImage, New Rectangle(New Point(-1, -1), newSize))
                    Dim m As New MemoryStream()
                    newImage.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Return m
                End Using
            End Using
        End Using
    End Function
#End Region

   
End Class