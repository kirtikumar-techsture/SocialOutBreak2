Imports System
Imports System.Web
Imports DataAccessLayer.DataAccessLayer
Imports System.Data
Namespace BusinessLayer
    Public Class videos
        Inherits IRecord
        Private _VideoId As Integer = 0
        Private _CatId As Integer = 0
        Private _CompanyId As Integer = 0
        Private _IndustryId As Integer = 0
        Private _AllCompanyIndustryId As Integer = 1
        Private _VideoTitle As String = ""
        Private _VideoCode As String = ""
        Private _CompanySelectedID As String = ""
        Private _IndustrySelectedID As String = ""
        Private _Status As Integer
        Private _Result As Integer = 0
        Private _VideoImage As String = ""
        Private _VideoType As Int16 = 1 '1-Active 0-Inactive'
        Private _VideoEmbedCode As String = ""

        Public Property VideoId() As Integer
            Get
                Return _VideoId
            End Get
            Set(ByVal value As Integer)
                _VideoId = value
            End Set
        End Property


        Public Property CatId() As Integer
            Get
                Return _CatId
            End Get
            Set(ByVal value As Integer)
                _CatId = value
            End Set
        End Property

        Public Property VideoTitle() As String
            Get
                Return _VideoTitle
            End Get
            Set(ByVal value As String)
                _VideoTitle = value
            End Set
        End Property

        Public Property CompanySelectedID() As String
            Get
                Return _CompanySelectedID
            End Get
            Set(ByVal value As String)
                _CompanySelectedID = value
            End Set
        End Property

        Public Property IndustrySelectedID() As String
            Get
                Return _IndustrySelectedID
            End Get
            Set(ByVal value As String)
                _IndustrySelectedID = value
            End Set
        End Property

        Public Property VideoCode() As String
            Get
                Return _VideoCode
            End Get
            Set(ByVal value As String)
                _VideoCode = value
            End Set
        End Property

        Public Property Status() As Integer
            Get
                Return _Status
            End Get
            Set(ByVal value As Integer)
                _Status = value
            End Set
        End Property

        Public Property CompanyId() As Integer
            Get
                Return _CompanyId
            End Get
            Set(ByVal value As Integer)
                _CompanyId = value
            End Set
        End Property

        Public Property IndustryId() As Integer
            Get
                Return _IndustryId
            End Get
            Set(ByVal value As Integer)
                _IndustryId = value
            End Set
        End Property

        Public Property AllCompanyIndustryId() As Integer
            Get
                Return _AllCompanyIndustryId
            End Get
            Set(ByVal value As Integer)
                _AllCompanyIndustryId = value
            End Set
        End Property


        Public Property Result() As Integer
            Get
                Return _Result
            End Get
            Set(ByVal value As Integer)
                _Result = value
            End Set
        End Property

        Public Property VideoType() As Integer
            Get
                Return _VideoType
            End Get
            Set(ByVal value As Integer)
                _VideoType = value
            End Set
        End Property
        Public Property VideoImage() As String
            Get
                Return _VideoImage
            End Get
            Set(ByVal value As String)
                _VideoImage = value
            End Set
        End Property
        Public Property VideoEmbededCode() As String
            Get
                Return _VideoEmbedCode
            End Get
            Set(ByVal value As String)
                _VideoEmbedCode = value
            End Set
        End Property


        'Public Property Library() As String
        '    Get
        '        Return _Library
        '    End Get
        '    Set(ByVal value As String)
        '        _Library = value
        '    End Set
        'End Property
        'Private _Library As String = String.Empty

        Public Function GetTrainingVideoByCategoryId() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_BindAllVideoCategoriesContainingVideo")
            dataAccess.AddParam("@vd_CompanyId", SqlDbType.Int, ParamString(Me.CompanyId))
            dataAccess.AddParam("@vd_IndustryId", SqlDbType.Int, ParamString(Me.IndustryId))
            dataAccess.AddParam("@vd_CatagoryId", SqlDbType.VarChar, ParamString(Me.CatId))

            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            Return ds
        End Function

        Public Function GetTrainingVideo() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetVideoById")
            dataAccess.AddParam("@vd_CompanyId", SqlDbType.Int, ParamString(Me.CompanyId))
            dataAccess.AddParam("@vd_IndustryId", SqlDbType.Int, ParamString(Me.IndustryId))
            dataAccess.AddParam("@vd_Id", SqlDbType.VarChar, Me.VideoId)
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            'If ds.Tables(0).Rows.Count > 0 Then
            '    With ds.Tables(0).Rows(0)
            '        Me.VideoTitle = .Item("vd_Title").ToString
            '        Me.VideoId = .Item("vd_Catagory").ToString
            '        Me.VideoType = .Item("vd_Type").ToString
            '        Me.VideoCode = .Item("vd_Video").ToString
            '        Me.VideoEmbededCode = .Item("vd_EmbedCode").ToString
            '        'Me.Status = .Item("vd_isActive")
            '    End With
            Return ds
        End Function
        Public Function GetVideoDetailsById() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetVideoForUpdate")
            dataAccess.AddParam("@VideoId", SqlDbType.Int, Me.VideoId)
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            If ds.Tables(0).Rows.Count > 0 Then
                With ds.Tables(0).Rows(0)
                    Me.CompanyId = .Item("vd_CompanyId")
                    Me.IndustryId = .Item("vd_IndustryId")
                    Me.AllCompanyIndustryId = .Item("vd_AllCompanyIndustryId")
                    Me.VideoTitle = .Item("vd_Title").ToString
                    Me.VideoEmbededCode = .Item("vd_EmbededCode").ToString
                    Me.VideoId = .Item("vd_id")
                    Me.CatId = .Item("vd_Categories")
                    Me.Status = .Item("vd_isActive")
                End With
            End If
            Return ds
        End Function

        Public Function AddEditVideo() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_AddEditVideo")
            dataAccess.AddParam("@vd_Id", SqlDbType.Int, Me.VideoId)
            dataAccess.AddParam("@vd_Title", SqlDbType.VarChar, (Me.VideoTitle))
            dataAccess.AddParam("@vd_Catagory", SqlDbType.Int, (Me.CatId))
            dataAccess.AddParam("@ComSelectedID", SqlDbType.VarChar, (Me.CompanySelectedID))
            dataAccess.AddParam("@IndSelectedID", SqlDbType.VarChar, (Me.IndustrySelectedID))
            dataAccess.AddParam("@vd_Video", SqlDbType.VarChar, (Me.VideoEmbededCode))
            'dataAccess.AddParam("@vd_VideoT", SqlDbType.VarChar, (Me.VideoImage))
            dataAccess.AddParam("@vd_Status", SqlDbType.Int, (Me.Status))
            dataAccess.AddParam("@vd_Type", SqlDbType.Int, (Me.VideoType))
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            If ds.Tables(0).Rows.Count > 0 Then
                With ds.Tables(0).Rows(0)
                    Me.Result = .Item("Msg").ToString
                    'Me.VideoId = .Item("VideoId")
                End With
            End If
        End Function

        Public Function GetDefaultVideo() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddParam("@vd_CompanyId", SqlDbType.Int, ParamString(Me.CompanyId))
            dataAccess.AddParam("@vd_IndustryId", SqlDbType.Int, ParamString(Me.IndustryId))
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetDefaultTrainingVideo")
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            Return ds
        End Function
        Public Function BindVideosCatagory() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_BindVideoCategories")
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            Return ds
        End Function
        Public Function GetVideos() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetVideos")
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            If ds.Tables(0).Rows.Count > 0 Then
                With ds.Tables(0).Rows(0)
                    Me.VideoTitle = .Item("vd_Title").ToString
                    Me.VideoEmbededCode = .Item("vd_EmbededCode").ToString
                    Me.VideoId = .Item("vd_id")
                    Me.CatId = .Item("vd_Categories")
                    Me.Status = .Item("vd_isActive")
                End With
            End If
            Return ds
        End Function
        Public Function DeleteVideo() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_DeleteVideo")
            dataAccess.AddParam("@VideoId", SqlDbType.Int, (Me.VideoId))
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            Return ds
        End Function
        Public Function UpdateVideo() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_UpdateVideo")
            dataAccess.AddParam("@vd_Id", SqlDbType.Int, (Me.VideoId))
            dataAccess.AddParam("@vd_Title", SqlDbType.VarChar, (Me.VideoTitle))
            dataAccess.AddParam("@vd_cat", SqlDbType.Int, (Me.CatId))
            dataAccess.AddParam("@vd_code", SqlDbType.VarChar, (Me.VideoEmbededCode))
            dataAccess.AddParam("@vd_isActive", SqlDbType.Int, (Me.Status))

            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            Return ds
        End Function


        Public Function ChangeVideoStatus() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_ChangeVideoStatus")
            dataAccess.AddParam("@VideoId", SqlDbType.Int, (Me.VideoId))
            dataAccess.AddParam("@Status", SqlDbType.Int, (Me.Status))
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            Return ds
        End Function

        Public Function BindALlCategories() As DataSet
            Dim ds As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetAllActiveVideosByCategoryId")
            dataAccess.AddParam("@CatId", SqlDbType.VarChar, CatId)
            dataAccess.AddParam("@vd_CompanyId", SqlDbType.Int, ParamString(Me.CompanyId))
            dataAccess.AddParam("@vd_IndustryId", SqlDbType.Int, ParamString(Me.IndustryId))
            ds = dataAccess.GetDataset()
            Return ds
            'objVideo.ApplicationPath = ConfigurationManager.AppSettings("ApplicationPath") & ConfigurationManager.AppSettings("ProductVideoPath") & "thumbnails/"
            'objVideo.VideoCatagoryId = CatId
            'ds = objVideo.GetAllActiveVideosByCategoryId()
            'If RowId = 1 Then
            '    If ds.Tables(0).Rows.Count > 0 Then
            '        With ds.Tables(0).Rows(0)
            '            If (.Item("vd_Type") = 1) Then
            '                phVideo.Visible = True
            '                phVideoEmbedCode.Visible = False
            '                strFileName = ConfigurationManager.AppSettings("ApplicationPath") & ConfigurationManager.AppSettings("ProductVideoPath") & .Item("vd_Video").ToString()
            '                strImageName = .Item("ThumbImage").ToString()
            '            Else
            '                phVideo.Visible = False
            '                phVideoEmbedCode.Visible = True
            '                spnVideoEmbed.InnerHtml = .Item("vd_EmbedCode").ToString()
            '            End If
            '            spanTitle.InnerHtml = .Item("vd_Title").ToString
            '        End With
            '    End If
            'End If
            'Return ds

        End Function
    End Class
End Namespace
