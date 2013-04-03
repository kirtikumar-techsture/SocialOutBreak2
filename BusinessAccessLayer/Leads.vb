Imports DataAccessLayer.DataAccessLayer
Namespace BusinessLayer
    Public Class Leads
        Inherits IRecord
        Public Property PageIndex() As Integer
            Get
                Return _PageIndex
            End Get
            Set(value As Integer)
                _PageIndex = value
            End Set
        End Property
        Private _PageIndex As Integer = 1

        Public Property CompanyId() As Integer
            Get
                Return _CompanyId
            End Get
            Set(value As Integer)
                _CompanyId = value
            End Set
        End Property
        Private _CompanyId As Integer = 0

        Public Property IndustryId() As Integer
            Get
                Return _IndustryId
            End Get
            Set(value As Integer)
                _IndustryId = value
            End Set
        End Property
        Private _IndustryId As Integer = 0

        Public Property FBUserId() As String
            Get
                Return _FBUserId
            End Get
            Set(value As String)
                _FBUserId = value
            End Set
        End Property
        Private _Status As String = "0"

        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(value As String)
                _Status = value
            End Set
        End Property
        Private _FBUserId As String = String.Empty
        Public Property FBPageId() As String
            Get
                Return _FBPageId
            End Get
            Set(value As String)
                _FBPageId = value
            End Set
        End Property
        Private _FBPageId As String = String.Empty
        Public Property SWEmail() As String
            Get
                Return _SWEmail
            End Get
            Set(value As String)
                _SWEmail = value
            End Set
        End Property
        Private _SWEmail As String = String.Empty
        Public Property SortBy() As String
            Get
                Return _SortBy
            End Get
            Set(value As String)
                _SortBy = value
            End Set
        End Property
        Private _SortBy As String = "Date desc"

        
#Region "Get All Leads of Both Sweepstakes and Custom Tabs"
        Public Function GetAllLeads() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetAllLeads")
            dataAccess.AddParam("@PageIndex", SqlDbType.Int, Me.PageIndex)
            dataAccess.AddParam("@FBUserID", SqlDbType.VarChar, Me.FBUserId)
            dataAccess.AddParam("@SortBy", SqlDbType.VarChar, Me.SortBy)
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            Return ds
        End Function
#End Region

#Region "LEAD SECTION FOR SWEEPSTAKES"
#Region "Update Notification On Off For Sweepstakes"
        Public Function GetAllLeadsForSW() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetAllLeadsForSW")
            dataAccess.AddParam("@PageIndex", SqlDbType.Int, Me.PageIndex)
            dataAccess.AddParam("@FBUserID", SqlDbType.VarChar, Me.FBUserId)
            dataAccess.AddParam("@SortBy", SqlDbType.VarChar, Me.SortBy)
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            Return ds
        End Function

        Public Function GetAdminLeadsForSW()
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetAdminLeadsForSW")
            dataAccess.AddParam("@PageIndex", SqlDbType.Int, Me.PageIndex)
            dataAccess.AddParam("@FBUserID", SqlDbType.VarChar, Me.FBUserId)
            dataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            dataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            Return ds
        End Function

        Public Function NotificationTurnOnOffForSW() As DataSet
            Try
                Dim dataAccess As New DALDataAccess()
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_NotificationTurnOnOffForSW")
                dataAccess.AddParam("@sw_FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
                dataAccess.AddParam("@sw_FBUserId", SqlDbType.VarChar, ParamString(Me.FBUserId))
                'Dim ds As New DataSet
                'ds = dataAccess.GetDataset()
                dataAccess.ExecuteNonQuery()
                'Return ds
            Catch ex As Exception
                Utility.LogError(ex, "Auto Post", True)
                Throw
            Finally
            End Try
        End Function
#End Region

#Region "Update Notification Email For Sweepstakes"
        Public Function NotificationEmailForSW() As DataSet
            Try
                Dim dataAccess As New DALDataAccess()
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_NotificationEmailForSW")
                dataAccess.AddParam("@sw_FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
                dataAccess.AddParam("@sw_FBUserId", SqlDbType.VarChar, ParamString(Me.FBUserId))
                dataAccess.AddParam("@sw_Email", SqlDbType.VarChar, Me.SWEmail)
                'dataAccess.AddParam("@sw_Status", SqlDbType.VarChar, Me.Status)
                'Dim ds As New DataSet
                'ds = dataAccess.GetDataset()
                dataAccess.ExecuteNonQuery()
                ' ds
            Catch ex As Exception
                Utility.LogError(ex, "Auto Post", True)
                Throw
            Finally
            End Try
        End Function
#End Region
#End Region

#Region "LEAD SECTION FOR CUSTOM TABS"
        Public Function GetAllLeadsForCT() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetAllLeadsForCT")
            dataAccess.AddParam("@PageIndex", SqlDbType.Int, Me.PageIndex)
            dataAccess.AddParam("@FBUserID", SqlDbType.VarChar, Me.FBUserId)
            dataAccess.AddParam("@SortBy", SqlDbType.VarChar, Me.SortBy)
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            Return ds
        End Function

        Public Function GetAdminLeadsForCT()
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetAdminLeadsForCT")
            dataAccess.AddParam("@PageIndex", SqlDbType.Int, Me.PageIndex)
            dataAccess.AddParam("@FBUserID", SqlDbType.VarChar, Me.FBUserId)
            dataAccess.AddParam("@CompanyId", SqlDbType.Int, Me.CompanyId)
            dataAccess.AddParam("@IndustryId", SqlDbType.Int, Me.IndustryId)
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            Return ds
        End Function
#Region "Update Notification On Off For Custom Tabs"
        Public Function NotificationTurnOnOffForCT() As DataSet
            Try
                Dim dataAccess As New DALDataAccess()
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_NotificationTurnOnOffForCT")
                dataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
                dataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, ParamString(Me.FBUserId))
                'Dim ds As New DataSet
                'ds = dataAccess.GetDataset()
                dataAccess.ExecuteNonQuery()
                'Return ds
            Catch ex As Exception
                Utility.LogError(ex, "Auto Post", True)
                Throw
            Finally
            End Try
        End Function
#End Region

#Region "Update Notification Email For Sweepstakes"
        Public Function NotificationEmailForCT() As DataSet
            Try
                Dim dataAccess As New DALDataAccess()
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_NotificationEmailForCT")
                dataAccess.AddParam("@ct_FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
                dataAccess.AddParam("@ct_FBUserId", SqlDbType.VarChar, ParamString(Me.FBUserId))
                dataAccess.AddParam("@ct_Email", SqlDbType.VarChar, Me.SWEmail)
                'dataAccess.AddParam("@ct_Status", SqlDbType.VarChar, Me.Status)
                'Dim ds As New DataSet
                'ds = dataAccess.GetDataset()
                dataAccess.ExecuteNonQuery()
                ' ds
            Catch ex As Exception
                Utility.LogError(ex, "Auto Post", True)
                Throw
            Finally
            End Try
        End Function
#End Region
#End Region

    End Class
End Namespace