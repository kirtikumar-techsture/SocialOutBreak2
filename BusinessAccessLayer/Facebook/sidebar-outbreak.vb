Imports System
Imports DataAccessLayer.DataAccessLayer
Imports System.Data

Namespace BusinessLayer
    <RecordAttribute("tbl_SidebarMaster,tbl_sidebar", "prc_AddSidebar,prc_getSidebar", New [String](0) {"Sidebar"})>
    Public Class BAlsidebar1
        Inherits IRecord

#Region "Variables"
        Private intPageIndex As Integer = 1
        Private intSidebarId As Integer = 0
        Private strSidebarName As String = String.Empty
        Private strSidebarContent As String = String.Empty
        Private strUserId As String = String.Empty
        Private strFBUserId As String = String.Empty
        Private strFBPageId As String = String.Empty
        Private strFBPageAccessToken As String = String.Empty
        Private intCompanyId As Integer = 0
        Private intIndustryId As Integer = 0
        Private intFreeSidebarId As Integer = 0
        Private intCid As Integer = 0
        Dim intDirection As Integer = 0

        Private strPage As String = ""
        Private strSidebarImage As String = ""
#End Region

#Region "Properties"
        Public Property PageIndex() As Integer
            Get
                Return intPageIndex
            End Get
            Set(value As Integer)
                intPageIndex = value
            End Set
        End Property
        Public Property SidebarId() As Integer
            Get
                Return intSidebarId
            End Get
            Set(value As Integer)
                intSidebarId = value
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
        Public Property SideBarImage() As String
            Get
                Return strSidebarImage
            End Get
            Set(value As String)
                strSidebarImage = value
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
        Public Property FBPageAccessToken() As String
            Get
                Return strFBPageAccessToken
            End Get
            Set(value As String)
                strFBPageAccessToken = value
            End Set
        End Property
        Public Property SidebarContent() As String
            Get
                Return strSidebarContent
            End Get
            Set(value As String)
                strSidebarContent = value
            End Set
        End Property
        Public Property FreeSidebarId() As Integer
            Get
                Return intFreeSidebarId
            End Get
            Set(value As Integer)
                intFreeSidebarId = value
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
        Public Property Cid() As String
            Get
                Return intCid
            End Get
            Set(value As String)
                intCid = value
            End Set
        End Property
        Public Property Direction() As String
            Get
                Return intDirection
            End Get
            Set(value As String)
                intDirection = value
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

#Region "Get Sidebar According to the User Id, Company Id or Industry Id"
        Public Function GetSidebarMaster() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetSidebarMaster")
            objDataAccess.AddParam("@sdm_Id", SqlDbType.Int, Me.SidebarId)
            objDataAccess.AddParam("@sd_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@sd_FBUserId", SqlDbType.VarChar, Me.FBUserId)
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

        Public Function GetSidebarMasterByID() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetSidebarMasterByID")
            objDataAccess.AddParam("@sd_Id", SqlDbType.Int, Me.SidebarId)
            objDataAccess.AddParam("@sd_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@sd_FBUserId", SqlDbType.VarChar, Me.FBUserId)
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

        Public Function GetSidebarMasterTemplates() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetSidebarMasterTemplates")
            objDataAccess.AddParam("@sdm_Id", SqlDbType.Int, Me.SidebarId)
            objDataAccess.AddParam("@sd_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@sd_FBUserId", SqlDbType.VarChar, Me.FBUserId)
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
#End Region

#Region "Get All Sidebar According to the Company Id or Industry Id"
        Public Function GetSidebarMasterByCompOrIndustry() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetSidebarMasterByCompOrIndustry")
            objDataAccess.AddParam("@pageindex", SqlDbType.Int, Me.PageIndex)
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
#End Region

#Region "Update Sidebar"
        Public Sub UpdateSidebarContent()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_UpdateSidebar")
            objDataAccess.AddParam("@sd_Id", SqlDbType.Int, Me.SidebarId)
            objDataAccess.AddParam("@sd_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@sd_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@sd_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@sd_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@sd_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@sd_FBPageAccessToken", SqlDbType.VarChar, Me.strFBPageAccessToken)
            objDataAccess.AddParam("@sd_Content", SqlDbType.VarChar, Me.SidebarContent)
            objDataAccess.AddParam("@sd_Image", SqlDbType.VarChar, Me.SideBarImage)
            objDataAccess.ExecuteNonQuery()
        End Sub

        Public Sub UpdateImageName()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_UpdateSidebarImageName")
            objDataAccess.AddParam("@sd_Id", SqlDbType.Int, Me.SidebarId)
            objDataAccess.AddParam("@sd_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@sd_Image", SqlDbType.VarChar, Me.SideBarImage)
            objDataAccess.ExecuteNonQuery()
        End Sub

        Public Sub UpdateIsPublishedSidebar()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_UpdateIsPublishedSidebar")
            objDataAccess.AddParam("@sd_Id", SqlDbType.Int, Me.SidebarId)
            objDataAccess.ExecuteNonQuery()
        End Sub
#End Region

#Region "New Save Sidebar"
        Public Function AddNewSidebarContent() As DataSet
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_AddNewSidebar")
            objDataAccess.AddParam("@sd_sdmId", SqlDbType.Int, Me.SidebarId)
            objDataAccess.AddParam("@sd_UserId", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@sd_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@sd_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@sd_FBUserId", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@sd_FBPageId", SqlDbType.VarChar, Me.FBPageId)
            objDataAccess.AddParam("@sd_FBPageAccessToken", SqlDbType.VarChar, Me.strFBPageAccessToken)
            objDataAccess.AddParam("@sd_Content", SqlDbType.VarChar, Me.SidebarContent)
            objDataAccess.AddParam("@sd_Image", SqlDbType.VarChar, Me.SideBarImage)
            ds = objDataAccess.GetDataset()
            Return ds
        End Function


#End Region

#Region "Get Publiched Sidebar Content Id"
        Public Function GetPublishedSidebarById() As DataSet
            'Try
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetPublishedSidebarById")
            objDataAccess.AddParam("@SidebarId", SqlDbType.Int, Me.SidebarId)
            ds = objDataAccess.GetDataset()
            Return ds
            '  Catch ex As Exception
            '    Utility.LogError(ex, "Drafts", True)
            '    Throw
            'Finally
            'End Try
        End Function
#End Region

#Region "Get My Sidebars"
        Public Function GetMySidebars() As DataSet
            Dim ds As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetMySidebarMaster")
            objDataAccess.AddParam("@sd_userid", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@sd_fbuserid", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@sd_CompanyId", SqlDbType.Int, Me.CompanyId)
            objDataAccess.AddParam("@sd_IndustryId", SqlDbType.Int, Me.IndustryId)
            objDataAccess.AddParam("@sd_cid", SqlDbType.VarChar, Me.Cid)
            objDataAccess.AddParam("@sd_direction", SqlDbType.VarChar, Me.Direction)
            ds = objDataAccess.GetDataset()
            Return ds
        End Function
#End Region

#Region "Delete My Sidebars"
        Public Sub DeleteMySidebars()
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_DeleteMySidebarMaster")
            objDataAccess.AddParam("@sd_userid", SqlDbType.Int, Me.UserId)
            objDataAccess.AddParam("@sd_cid", SqlDbType.VarChar, Me.Cid)
            objDataAccess.ExecuteNonQuery()
        End Sub
#End Region


    End Class
End Namespace

