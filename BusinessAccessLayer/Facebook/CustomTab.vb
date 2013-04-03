Imports System
Imports DataAccessLayer.DataAccessLayer
Imports System.Data

Namespace BusinessLayer

    Public Class BALCustomTab
        Inherits IRecord

#Region "Variables"
        Private intPageIndex As Integer = 1
        Private intCustomTabId As Integer = 0
        Private strCustomTabName As String = String.Empty
        Private strCustomTabContent As String = String.Empty
        Private strUnlikeSwContent As String = String.Empty
        Private strUserId As String = String.Empty
        Private strFBUserId As String = String.Empty
        Private strFBUserEmail As String = String.Empty
        Private strFBPageId As String = String.Empty
        Private strFBPageName As String = String.Empty
        Private strFBPageAccessToken As String = String.Empty
        Private strFBSweepstakeAppId As String = String.Empty
        Private intCompanyId As Integer = 0
        Private intIndustryId As Integer = 0
        Private intCategoryId As Integer = 0
        Private intFreeCustomTabId As Integer = 0
        Private intCid As Integer = 0
        Private intPageId As Integer = 0
        Private intswid As Integer = 0
        Dim intDirection As Integer = 0
        Private strShareText As String = String.Empty
        Private strShareMessage As String = String.Empty
        Private strShareImg As String = String.Empty
        Private strShareEmail As String = String.Empty
        Private strEmailText As String = String.Empty
        Private strPDF As String = String.Empty
        Private strPage As String = ""
        Private strCustomTabImage As String = ""
        Private intCustomise As Integer = 0
        Private sw_type As Integer = 0
        Private intLikeUnlike As Integer = 0
#End Region

#Region "Properties"
        Public Property PageId() As Integer
            Get
                Return intPageId
            End Get
            Set(ByVal value As Integer)
                intPageId = value
            End Set
        End Property

        Public Property LikeUnlike() As Integer
            Get
                Return intLikeUnlike
            End Get
            Set(ByVal value As Integer)
                intLikeUnlike = value
            End Set
        End Property
        Public Property PageIndex() As Integer
            Get
                Return intPageIndex
            End Get
            Set(value As Integer)
                intPageIndex = value
            End Set
        End Property
        Public Property CustomTabName() As String
            Get
                Return strCustomTabName
            End Get
            Set(value As String)
                strCustomTabName = value
            End Set
        End Property
        Public Property CustomTabId() As Integer
            Get
                Return intCustomTabId
            End Get
            Set(value As Integer)
                intCustomTabId = value
            End Set
        End Property
        Public Property CompanyId() As Integer
            Get
                Return intCompanyId
            End Get
            Set(value As Integer)
                intCompanyId = value
            End Set
        End Property
        Public Property IndustryId() As Integer
            Get
                Return intIndustryId
            End Get
            Set(value As Integer)
                intIndustryId = value
            End Set
        End Property
        Public Property CategoryId() As Integer
            Get
                Return intCategoryId
            End Get
            Set(ByVal value As Integer)
                intCategoryId = value
            End Set
        End Property

        Public Property UserId() As String
            Get
                Return strUserId
            End Get
            Set(value As String)
                strUserId = value
            End Set
        End Property
        Public Property FBUserId() As String
            Get
                Return strFBUserId
            End Get
            Set(value As String)
                strFBUserId = value
            End Set
        End Property

        Public Property FBUserEmail() As String
            Get
                Return strFBUserEmail
            End Get
            Set(value As String)
                strFBUserEmail = value
            End Set
        End Property
        Public Property FBPageId() As String
            Get
                Return strFBPageId
            End Get
            Set(value As String)
                strFBPageId = value
            End Set
        End Property

        Public Property FBSweepstakeAppId() As String
            Get
                Return strFBSweepstakeAppId
            End Get
            Set(value As String)
                strFBSweepstakeAppId = value
            End Set
        End Property
        Public Property FBPageName() As String
            Get
                Return strFBPageName
            End Get
            Set(value As String)
                strFBPageName = value
            End Set
        End Property
        Public Property FBPageAccessToken() As String
            Get
                Return strFBPageAccessToken
            End Get
            Set(value As String)
                strFBPageAccessToken = value
            End Set
        End Property
        Public Property CustomTabContent() As String
            Get
                Return strCustomTabContent
            End Get
            Set(value As String)
                strCustomTabContent = value
            End Set
        End Property
        Public Property UnlikeSwContent() As String
            Get
                Return strUnlikeSwContent
            End Get
            Set(ByVal value As String)
                strUnlikeSwContent = value
            End Set
        End Property

        Public Property FreeCustomTabId() As Integer
            Get
                Return intFreeCustomTabId
            End Get
            Set(value As Integer)
                intFreeCustomTabId = value
            End Set
        End Property

        Public Property Page() As String
            Get
                Return strPage
            End Get
            Set(value As String)
                strPage = value
            End Set
        End Property
        Public Property CustomTabImage() As String
            Get
                Return strCustomTabImage
            End Get
            Set(value As String)
                strCustomTabImage = value
            End Set
        End Property
        Public Property Cid() As Integer
            Get
                Return intCid
            End Get
            Set(value As Integer)
                intCid = value
            End Set
        End Property
        Public Property Direction() As Integer
            Get
                Return intDirection
            End Get
            Set(value As Integer)
                intDirection = value
            End Set
        End Property
        Public Property swid() As Integer
            Get
                Return intswid
            End Get
            Set(ByVal value As Integer)
                intswid = value
            End Set
        End Property
        Public Property ShareText() As String
            Get
                Return strShareText
            End Get
            Set(ByVal value As String)
                strShareText = value
            End Set
        End Property
        Public Property ShareMessage() As String
            Get
                Return strShareMessage
            End Get
            Set(ByVal value As String)
                strShareMessage = value
            End Set
        End Property
        Public Property ShareImage() As String
            Get
                Return strShareImg
            End Get
            Set(ByVal value As String)
                strShareImg = value
            End Set
        End Property
        Public Property ShareEmailAddress() As String
            Get
                Return strEmailText
            End Get
            Set(ByVal value As String)
                strEmailText = value
            End Set
        End Property
        Public Property ShareEmailText() As String
            Get
                Return strEmailText
            End Get
            Set(ByVal value As String)
                strEmailText = value
            End Set
        End Property
        Public Property SharePDF() As String
            Get
                Return strPDF
            End Get
            Set(ByVal value As String)
                strPDF = value
            End Set
        End Property
        Public Property Customise() As Integer
            Get
                Return intCustomise
            End Get
            Set(ByVal value As Integer)
                intCustomise = value
            End Set
        End Property
        Public Property getSwType() As Integer
            Get
                Return sw_type
            End Get
            Set(ByVal value As Integer)
                sw_type = value
            End Set
        End Property
#End Region

#Region "Get Video Tutorial"
        Public Function GetVideoTutorial() As DataSet
            ' Try
            'Dim objEncDec As New Utility
            Dim dataAccess As New DALDataAccess()
            Dim ds As New DataSet
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetVideoTutorial")
            dataAccess.AddParam("@vt_Page", SqlDbType.VarChar, Me.Page)


            ds = dataAccess.GetDataset()

            Return ds

        End Function
#End Region

#Region "Get Application ID By Category"

        Public Function GetApplicationIdByCategory() As DataSet
            ' Try
            'Dim objEncDec As New Utility
            Dim dataAccess As New DALDataAccess()
            Dim ds As New DataSet
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetApplicationIdByCategory")
            dataAccess.AddParam("@ctm_CTID", SqlDbType.VarChar, Me.CustomTabId)
            ds = dataAccess.GetDataset()
            Return ds
        End Function

#End Region


#Region "Get Video Tutorial By Custom Tab ID"
        Public Function GetVideoTutorialByID() As DataSet
            ' Try
            'Dim objEncDec As New Utility
            Dim dataAccess As New DALDataAccess()
            Dim ds As New DataSet
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetVideoTutorialByID")
            dataAccess.AddParam("@ctm_CTID", SqlDbType.VarChar, Me.CustomTabId)


            ds = dataAccess.GetDataset()

            Return ds

        End Function
        Public Function GetVideoTutorialByIDNew() As DataSet
            ' Try
            'Dim objEncDec As New Utility
            Dim dataAccess As New DALDataAccess()
            Dim ds As New DataSet
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetVideoTutorialByCatID")
            dataAccess.AddParam("@ctm_CTID", SqlDbType.VarChar, Me.CustomTabId)


            ds = dataAccess.GetDataset()

            Return ds

        End Function

#End Region
#Region "Get Video Tutorial By Custom Tab ID"
        Public Function GetVideoTutorialByMasterID() As DataSet
            ' Try
            'Dim objEncDec As New Utility
            Dim dataAccess As New DALDataAccess()
            Dim ds As New DataSet
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetVideoTutorialByMasterID")
            dataAccess.AddParam("@ctm_CTID", SqlDbType.VarChar, Me.CustomTabId)


            ds = dataAccess.GetDataset()

            Return ds

        End Function

#End Region

#Region "Get CustomTab According to the User Id, Company Id or Industry Id"
        Public Function GetCustomTabMaster() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetCustomTabMaster")
            objDataAccess.AddParam("@ctm_Id", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function

        Public Function GetCustomTabMasterByID() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetCustomTabMasterByID")
            objDataAccess.AddParam("@ct_Id", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function
        
        Public Function GetCustomTabMasterByIDNew() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetCustomTabMasterByIDNew2")
            objDataAccess.AddParam("@ct_Id", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function

        Public Function GetCustomTabMasterTemplates() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetCustomTabMasterTemplates")
            objDataAccess.AddParam("@ctm_Id", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function

        Public Function GetCustomTabMasterTemplatesNew() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetCustomTabMasterTemplatesNew")
            objDataAccess.AddParam("@ctm_Id", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function
        Public Function GetSweepstakeTemplate() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetSweepstakeTemplates")
            'objDataAccess.AddParam("@ctm_Id", SqlDbType.Int, Me.CustomTabId)
            'objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            'objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            'objDataAccess.AddParam("@swid", SqlDbType.Int, Me.swid)
            objDataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@sw_id", SqlDbType.Int, Me.swid)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function
#End Region

#Region "Get All CustomTab According to the Company Id or Industry Id"
        Public Function GetCustomTabMasterByCompOrIndustry() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetCustomTabMasterByCompOrIndustry")
            objDataAccess.AddParam("@pageindex", SqlDbType.Int, Me.PageIndex)
            objDataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@PageId", SqlDbType.Int, Me.intPageId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function
        Public Function GetCustomTabMasterByCompOrIndustryNew() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetCustomTabMasterByCompOrIndustryNew")
            objDataAccess.AddParam("@pageindex", SqlDbType.Int, Me.PageIndex)
            objDataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@catId", SqlDbType.Int, Me.CategoryId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function
#End Region

#Region "Update CustomTab"
        Public Sub UpdateCustomTabContent()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_UpdateCustomTab")
            objDataAccess.AddParam("@ct_Id", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@ct_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@ct_Name", SqlDbType.VarChar, Me.CustomTabName)
            'objDataAccess.AddParam("@ct_FBPageAccessToken", SqlDbType.VarChar, Me.strFBPageAccessToken)
            objDataAccess.AddParam("@ct_Content", SqlDbType.VarChar, Me.CustomTabContent)
            'objDataAccess.AddParam("@ct_Image", SqlDbType.VarChar, Me.CustomTabImage)
            objDataAccess.ExecuteNonQuery()
        End Sub
        Public Sub UpdateCustomTabContentNew()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_UpdateCustomTabNew")
            objDataAccess.AddParam("@ct_Id", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@ct_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@ct_Name", SqlDbType.VarChar, Me.CustomTabName)
            'objDataAccess.AddParam("@ct_FBPageAccessToken", SqlDbType.VarChar, Me.strFBPageAccessToken)
            objDataAccess.AddParam("@ct_Content", SqlDbType.VarChar, Me.CustomTabContent)
            'objDataAccess.AddParam("@ct_Image", SqlDbType.VarChar, Me.CustomTabImage)
            objDataAccess.ExecuteNonQuery()
        End Sub

        Public Sub UpdateImageName()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_UpdateCustomTabImageName")
            objDataAccess.AddParam("@ct_Id", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_Image", SqlDbType.VarChar, Me.CustomTabImage)
            objDataAccess.ExecuteNonQuery()
        End Sub

        Public Sub UpdateIsPublishedCustomTab()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_UpdateIsPublishedCustomTab")
            objDataAccess.AddParam("@ct_Id", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@ct_FBPageName", SqlDbType.VarChar, Me.FBPageName)
            objDataAccess.AddParam("@ct_FBPageAccessToken", SqlDbType.VarChar, Me.FBPageAccessToken)
            objDataAccess.ExecuteNonQuery()
        End Sub

        Public Function getApplicationIdByTabId() As String
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_getFacebookApplicationId")
            objDataAccess.AddParam("@ct_Id", SqlDbType.Int, Me.CustomTabId)
            getApplicationIdByTabId = objDataAccess.ExecuteScalar
        End Function
#End Region

#Region "Save New CustomTab"
        Public Function AddNewCustomTabContent() As DataSet
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_AddNewCustomTab")
            objDataAccess.AddParam("@ct_ctmId", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@ct_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@ct_Name", SqlDbType.VarChar, Me.CustomTabName)
            objDataAccess.AddParam("@ct_FBPageAccessToken", SqlDbType.VarChar, Me.strFBPageAccessToken)
            objDataAccess.AddParam("@ct_Content", SqlDbType.VarChar, Me.CustomTabContent)
            objDataAccess.AddParam("@ct_Image", SqlDbType.VarChar, Me.CustomTabImage)
            ds = objDataAccess.GetDataset()
            Return ds
        End Function
        Public Function AddNewCustomTabContentNew() As Integer
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_AddNewCustomTabNew")
            objDataAccess.AddParam("@ct_ctmId", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@ct_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@ct_Name", SqlDbType.VarChar, Me.CustomTabName)
            objDataAccess.AddParam("@ct_FBPageAccessToken", SqlDbType.VarChar, Me.strFBPageAccessToken)
            objDataAccess.AddParam("@ct_Content", SqlDbType.VarChar, Me.CustomTabContent)
            objDataAccess.AddParam("@ct_Image", SqlDbType.VarChar, Me.CustomTabImage)
            objDataAccess.AddParam("@ct_IsCustomize", SqlDbType.VarChar, Me.Customise)
            AddNewCustomTabContentNew = objDataAccess.ExecuteScalar()
        End Function
       
#End Region

#Region "Get Published CustomTab Content Id"
        Public Function GetPublishedCustomTabById() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetPublishedCustomTabById")
            objDataAccess.AddParam("@CustomTabId", SqlDbType.Int, Me.CustomTabId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function
        Public Function GetPublishedSweepstakeById() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetPublishedSweepstakeById")
            objDataAccess.AddParam("@swid", SqlDbType.Int, Me.swid)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function
        Public Function GetPublishedCustomTabByIdtemp() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetPublishedCustomTabByIdtemp")
            objDataAccess.AddParam("@CustomTabId", SqlDbType.Int, Me.CustomTabId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function
#End Region

#Region "Get My CustomTabs"
        Public Function GetMyCustomTabs() As DataSet
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            'objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetMyCustomTabMaster")
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetMyCustomTabMasterByFanPageId")
            objDataAccess.AddParam("@ct_userid", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_fbuserid", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_fbuseremail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@ct_cid", SqlDbType.Int, Me.Cid)
            objDataAccess.AddParam("@ct_direction", SqlDbType.Int, Me.Direction)
            objDataAccess.AddParam("@ct_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@ct_IndustryId", SqlDbType.Int, Me.IndustryId)
            ds = objDataAccess.GetDataset()
            Return ds
        End Function

        Public Function GetMyCustomTabsNew() As DataSet
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            'objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetMyCustomTabMaster")
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetMyCustomTabMasterByFanPageIdNew")
            objDataAccess.AddParam("@ct_userid", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_fbuserid", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_fbuseremail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@ct_cid", SqlDbType.Int, Me.Cid)
            objDataAccess.AddParam("@ct_direction", SqlDbType.Int, Me.Direction)
            objDataAccess.AddParam("@ct_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@ct_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@catagoryid", SqlDbType.Int, Me.CategoryId)
            ds = objDataAccess.GetDataset()
            Return ds
        End Function
#End Region

#Region "Delete My CustomTabs"
        Public Sub DeleteMyCustomTabs()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_DeleteMyCustomTabMaster")
            objDataAccess.AddParam("@ct_userid", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_cid", SqlDbType.Int, Me.Cid)
            objDataAccess.ExecuteNonQuery()
        End Sub
#End Region

#Region "Get CutomTabs Fan Page Data"
        Public Function GetCustomTabFanPageData() As DataSet
            Dim objDataAccess As New DALDataAccess()
            Dim ds As New DataSet
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetCustomTabFanPageData")
            objDataAccess.AddParam("@ct_userid", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_cid", SqlDbType.Int, Me.Cid)
            ds = objDataAccess.GetDataset()
            Return ds
        End Function
#End Region
#Region "Get Express Left Menus"
        Public Function GetExpressMenus() As DataSet
            Try
                Dim dataAccess As New DALDataAccess()
                Dim ds As New DataSet
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetExpressMenus")
                dataAccess.AddParam("@CompanyID", SqlDbType.NVarChar, ParamString(Me.CompanyID))
                dataAccess.AddParam("@IndustryID", SqlDbType.NVarChar, ParamString(Me.IndustryID))


                ds = dataAccess.GetDataset()
                'If ds.Tables(0).Rows.Count > 0 Then
                Return ds
                ' End If
            Catch ex As Exception
                Utility.LogError(ex, "Express Menu", True)
                Throw
            Finally
            End Try
        End Function
#End Region

#Region "Get Express Blank Tab in Left Menus"
        Public Function GetExpressBlankMenus() As DataSet
            Try
                Dim dataAccess As New DALDataAccess()
                Dim ds As New DataSet
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetExpressBlankMenus")
                dataAccess.AddParam("@CompanyID", SqlDbType.NVarChar, ParamString(Me.CompanyId))
                dataAccess.AddParam("@IndustryID", SqlDbType.NVarChar, ParamString(Me.IndustryId))


                ds = dataAccess.GetDataset()
                'If ds.Tables(0).Rows.Count > 0 Then
                Return ds
                ' End If
            Catch ex As Exception
                Utility.LogError(ex, "Express Menu", True)
                Throw
            Finally
            End Try
        End Function
#End Region

#Region "Copy My Custom Tabs"
        Public Function CopyMyCustomTabs() As DataSet
            Dim dsCopy As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_CopyMyCustomTabMaster")
            objDataAccess.AddParam("@ct_userid", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_FBUserid", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_cid", SqlDbType.Int, Me.Cid)
            objDataAccess.AddParam("@ct_isCustomize", SqlDbType.Int, Me.Customise)
            dsCopy = objDataAccess.GetDataset()
            Return dsCopy
        End Function
#End Region

#Region "save customise user sweepstake content"

        Public Function SaveSweepstakeContent() As Integer
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_SaveSweepstakeContent")
            objDataAccess.AddParam("@cs_id", SqlDbType.Int, Me.Customise)
            objDataAccess.AddParam("@cs_swid", SqlDbType.Int, Me.swid)
            objDataAccess.AddParam("@cs_LikeUnlike", SqlDbType.Int, Me.LikeUnlike)
            objDataAccess.AddParam("@cs_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@cs_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@cs_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@cs_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@cs_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@cs_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@cs_FBPageName", SqlDbType.VarChar, Me.FBPageName)
            objDataAccess.AddParam("@cs_Name", SqlDbType.VarChar, Me.CustomTabName)
            objDataAccess.AddParam("@cs_FBPageAccessToken", SqlDbType.VarChar, Me.FBPageAccessToken)
            objDataAccess.AddParam("@cs_Content", SqlDbType.VarChar, Me.CustomTabContent)
            objDataAccess.AddParam("@cs_UnLikeSwContent", SqlDbType.VarChar, Me.UnlikeSwContent)
            objDataAccess.AddParam("@cs_ShareText", SqlDbType.VarChar, Me.ShareText)
            objDataAccess.AddParam("@cs_ShareMessage", SqlDbType.VarChar, Me.ShareMessage)
            objDataAccess.AddParam("@cs_Image", SqlDbType.VarChar, Me.ShareImage)
            objDataAccess.AddParam("@cs_type", SqlDbType.Int, Me.sw_type)
            'objDataAccess.AddParam("@cs_ShareEmail", SqlDbType.VarChar, Me.ShareEmailAddress)
            objDataAccess.AddParam("@cs_ShareEmailText", SqlDbType.VarChar, Me.ShareEmailText)

            objDataAccess.AddParam("@cs_PDFLink", SqlDbType.VarChar, Me.SharePDF)
            SaveSweepstakeContent = objDataAccess.ExecuteScalar()
        End Function
        Public Function SaveCustomiseSweepstakeContent() As Integer
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_SaveCustomiseSweepstakeContent")
            objDataAccess.AddParam("@cs_id", SqlDbType.Int, Me.Customise)
            objDataAccess.AddParam("@cs_swid", SqlDbType.Int, Me.swid)
            'objDataAccess.AddParam("@cs_LikeUnlike", SqlDbType.Int, Me.LikeUnlike)
            objDataAccess.AddParam("@cs_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@cs_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@cs_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@cs_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@cs_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@cs_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@cs_FBPageName", SqlDbType.VarChar, Me.FBPageName)
            objDataAccess.AddParam("@cs_Name", SqlDbType.VarChar, Me.CustomTabName)
            objDataAccess.AddParam("@cs_FBPageAccessToken", SqlDbType.VarChar, Me.FBPageAccessToken)
            objDataAccess.AddParam("@cs_Content", SqlDbType.VarChar, Me.CustomTabContent)
            'objDataAccess.AddParam("@cs_UnLikeSwContent", SqlDbType.VarChar, Me.UnlikeSwContent)
            objDataAccess.AddParam("@cs_ShareText", SqlDbType.VarChar, Me.ShareText)
            objDataAccess.AddParam("@cs_ShareMessage", SqlDbType.VarChar, Me.ShareMessage)
            objDataAccess.AddParam("@cs_type", SqlDbType.Int, Me.sw_type)
            objDataAccess.AddParam("@cs_Image", SqlDbType.VarChar, Me.ShareImage)

            'objDataAccess.AddParam("@cs_ShareEmail", SqlDbType.VarChar, Me.ShareEmailAddress)
            objDataAccess.AddParam("@cs_ShareEmailText", SqlDbType.VarChar, Me.ShareEmailText)

            objDataAccess.AddParam("@cs_PDFLink", SqlDbType.VarChar, Me.SharePDF)
            SaveCustomiseSweepstakeContent = objDataAccess.ExecuteScalar()
        End Function

        Public Sub UpdateSweepstakeContent()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_UpdateSweepstakeContent")
            objDataAccess.AddParam("@cs_swid", SqlDbType.Int, Me.swid)
            objDataAccess.AddParam("@cs_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@cs_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@cs_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@cs_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@cs_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@cs_FBPageName", SqlDbType.VarChar, Me.FBPageName)
            objDataAccess.AddParam("@cs_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@cs_Name", SqlDbType.VarChar, Me.CustomTabName)
            objDataAccess.AddParam("@cs_FBPageAccessToken", SqlDbType.VarChar, Me.FBPageAccessToken)
            objDataAccess.AddParam("@cs_Content", SqlDbType.VarChar, Me.CustomTabContent)
            objDataAccess.AddParam("@cs_ShareText", SqlDbType.VarChar, Me.ShareText)
            objDataAccess.AddParam("@cs_ShareMessage", SqlDbType.VarChar, Me.ShareMessage)
            objDataAccess.AddParam("@cs_Image", SqlDbType.VarChar, Me.ShareImage)
            'objDataAccess.AddParam("@cs_ShareEmail", SqlDbType.VarChar, Me.ShareEmailAddress)
            objDataAccess.AddParam("@cs_ShareEmailText", SqlDbType.VarChar, Me.ShareEmailText)
            objDataAccess.AddParam("@cs_PDFLink", SqlDbType.VarChar, Me.SharePDF)
            objDataAccess.ExecuteNonQuery()
        End Sub
        Public Sub UpdateSweepstakeCustomiseContent()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_UpdateCustomiseSweepstakeContent")
            objDataAccess.AddParam("@cs_swid", SqlDbType.Int, Me.swid)
            objDataAccess.AddParam("@cs_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@cs_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@cs_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@cs_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@cs_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@cs_FBPageName", SqlDbType.VarChar, Me.FBPageName)
            objDataAccess.AddParam("@cs_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@cs_Name", SqlDbType.VarChar, Me.CustomTabName)
            objDataAccess.AddParam("@cs_FBPageAccessToken", SqlDbType.VarChar, Me.FBPageAccessToken)
            objDataAccess.AddParam("@cs_Content", SqlDbType.VarChar, Me.CustomTabContent)
            'objDataAccess.AddParam("@cs_UnLikeSwContent", SqlDbType.VarChar, Me.UnlikeSwContent)
            objDataAccess.AddParam("@cs_ShareText", SqlDbType.VarChar, Me.ShareText)
            objDataAccess.AddParam("@cs_ShareMessage", SqlDbType.VarChar, Me.ShareMessage)
            objDataAccess.AddParam("@cs_Image", SqlDbType.VarChar, Me.ShareImage)
            'objDataAccess.AddParam("@cs_ShareEmail", SqlDbType.VarChar, Me.ShareEmailAddress)
            objDataAccess.AddParam("@cs_ShareEmailText", SqlDbType.VarChar, Me.ShareEmailText)
            objDataAccess.AddParam("@cs_PDFLink", SqlDbType.VarChar, Me.SharePDF)
            objDataAccess.ExecuteNonQuery()
        End Sub

        Public Function SaveCustomiseUserShareTextandEmailText() As DataSet
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_SaveCustomiseUserShareTextandEmailText")
            objDataAccess.AddParam("@ct_ctmId", SqlDbType.Int, Me.swid)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@ct_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@ct_Name", SqlDbType.VarChar, Me.CustomTabName)
            objDataAccess.AddParam("@ct_FBPageAccessToken", SqlDbType.VarChar, Me.strFBPageAccessToken)
            objDataAccess.AddParam("@ct_Content", SqlDbType.VarChar, Me.CustomTabContent)
            objDataAccess.AddParam("@ct_Image", SqlDbType.VarChar, Me.CustomTabImage)
            ds = objDataAccess.GetDataset()
            Return ds
        End Function

        Public Sub UpdateIsPublishedSweepstake()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_UpdateIsPublishedSweepstake")
            objDataAccess.AddParam("@cs_Id", SqlDbType.Int, Me.swid)
            objDataAccess.AddParam("@cs_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@cs_FBPageName", SqlDbType.VarChar, Me.FBPageName)
            objDataAccess.AddParam("@cs_FBPageAccessToken", SqlDbType.VarChar, Me.FBPageAccessToken)
            objDataAccess.ExecuteNonQuery()
        End Sub

        Public Function GetSweepstakeByCompOrIndustry() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_SweeptakeTabMasterByCompOrIndustry")
            objDataAccess.AddParam("@pageindex", SqlDbType.Int, Me.PageIndex)
            objDataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            'objDataAccess.AddParam("@PageId", SqlDbType.Int, Me.intPageId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function

        Public Function GetMySweepstakeTabsNew() As DataSet
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            'objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetMyCustomTabMaster")
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetMySweepstakeTabMasterByFanPage")
            objDataAccess.AddParam("@ct_userid", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_fbuserid", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_fbuseremail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@ct_cid", SqlDbType.Int, Me.Cid)
            objDataAccess.AddParam("@ct_direction", SqlDbType.Int, Me.Direction)
            objDataAccess.AddParam("@ct_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@ct_IndustryId", SqlDbType.Int, Me.IndustryId)
            'objDataAccess.AddParam("@catagoryid", SqlDbType.Int, Me.CategoryId)
            ds = objDataAccess.GetDataset()
            Return ds
        End Function


        Public Sub UpdateSweeptakeImageName()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_UpdateSweepstakeImageName")
            objDataAccess.AddParam("@ct_Id", SqlDbType.Int, Me.swid)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_Image", SqlDbType.VarChar, Me.CustomTabImage)
            objDataAccess.ExecuteNonQuery()
        End Sub


        Public Function GetSweepstakeById() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetSweepstakeTabById")
            objDataAccess.AddParam("@cs_id", SqlDbType.Int, Me.swid)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function

        Public Function GetSaveSweepstakeByID() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetSweepstakeByID")
            objDataAccess.AddParam("@cs_Id", SqlDbType.Int, Me.swid)
            objDataAccess.AddParam("@cs_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@cs_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@cs_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@cs_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function

        Public Function GetSweepstakListByCompOrIndustry() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetSweepstakListByCompOrIndustry")
            objDataAccess.AddParam("@pageindex", SqlDbType.Int, Me.PageIndex)
            objDataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            'objDataAccess.AddParam("@catId", SqlDbType.Int, Me.CategoryId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function

        Public Sub AddEditPublishedSweepstakeData()
            Try
                Dim dataAccess As New DALDataAccess()
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_InsertUpdatePublishedSweepstakeData")
                dataAccess.AddParam("@FBUserId", SqlDbType.VarChar, ParamString(Me.FBUserId))
                dataAccess.AddParam("@FBApplicationId", SqlDbType.VarChar, ParamString(Me.FBSweepstakeAppId))
                dataAccess.AddParam("@FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
                dataAccess.AddParam("@FBPageName", SqlDbType.VarChar, ParamString(Me.FBPageName))
                dataAccess.AddParam("@FBPageAccessToken", SqlDbType.VarChar, ParamString(Me.FBPageAccessToken))
                dataAccess.AddParam("@FBUserEmail", SqlDbType.VarChar, ParamString(Me.FBUserEmail))
                dataAccess.AddParam("@ShareText", SqlDbType.VarChar, ParamString(Me.ShareText))
                dataAccess.AddParam("@ShareMessage", SqlDbType.VarChar, ParamString(Me.ShareMessage))
                dataAccess.AddParam("@SWEmailText", SqlDbType.VarChar, ParamString(Me.ShareEmailText))
                dataAccess.AddParam("@ShareImage", SqlDbType.VarChar, ParamString(Me.ShareImage))
                dataAccess.AddParam("@SharePDF", SqlDbType.VarChar, ParamString(Me.SharePDF))
                dataAccess.AddParam("@Content", SqlDbType.VarChar, ParamString(Me.CustomTabContent))
                'dataAccess.AddParam("@UnLikeContent", SqlDbType.VarChar, ParamString(Me.UnlikeSwContent))
                'dataAccess.AddParam("@sw_Type", SqlDbType.Int, ParamInt(Me.getSwType))
                dataAccess.AddParam("@sw_id", SqlDbType.Int, ParamInt(Me.swid))

                dataAccess.ExecuteNonQuery()
            Catch ex As Exception
                Utility.LogError(ex, "Custom Tab", True)
                Throw
            Finally
            End Try
        End Sub

#End Region


#Region "Customise User Custom Tab"

        Public Function SaveCustomiseUserCustomTab() As Integer
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_SaveCustomiseUserCustomTab")
            objDataAccess.AddParam("@ct_ctmId", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@ct_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_name", SqlDbType.VarChar, Me.CustomTabName)
            objDataAccess.AddParam("@ct_Content", SqlDbType.VarChar, Me.CustomTabContent)
            SaveCustomiseUserCustomTab = objDataAccess.ExecuteScalar()
        End Function

        Public Sub DeleteMySweepstakes()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_DeleteMySweepstakeMaster")
            objDataAccess.AddParam("@cs_userid", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@cs_cid", SqlDbType.Int, Me.swid)
            objDataAccess.ExecuteNonQuery()
        End Sub

        Public Function GetCustomiseSaveCustomTabById() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetCustomiseSaveCustomTabById")
            objDataAccess.AddParam("@ct_Id", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            'objDataAccess.AddParam("@ct_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            'objDataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function
        Public Function AddUpdateCustomiseTab() As Integer
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_AddUpdateCustomiseCustomTab")
            objDataAccess.AddParam("@ct_ctmId", SqlDbType.Int, Me.CustomTabId)
            objDataAccess.AddParam("@ct_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@ct_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@ct_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@ct_FBUserEmail", SqlDbType.VarChar, Me.FBUserEmail)
            objDataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@ct_Name", SqlDbType.VarChar, Me.CustomTabName)
            objDataAccess.AddParam("@ct_FBPageAccessToken", SqlDbType.VarChar, Me.strFBPageAccessToken)
            objDataAccess.AddParam("@ct_Content", SqlDbType.VarChar, Me.CustomTabContent)
            objDataAccess.AddParam("@ct_Image", SqlDbType.VarChar, Me.CustomTabImage)
            objDataAccess.AddParam("@ct_isCustomise", SqlDbType.VarChar, Me.Customise)
            AddUpdateCustomiseTab = objDataAccess.ExecuteScalar()
        End Function

#End Region


    End Class
End Namespace

