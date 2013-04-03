Imports DataAccessLayer.DataAccessLayer
Namespace BusinessLayer
    Public Class BALSweepStake
        Inherits IRecord
        Public Property Title() As String
            Get
                Return _Title
            End Get
            Set(value As String)
                _Title = value
            End Set
        End Property
        Private _Title As String = String.Empty

        Public Property swid() As Integer
            Get
                Return _swid
            End Get
            Set(value As Integer)
                _swid = value
            End Set
        End Property
        Private _swid As Integer = 0

        Public Property Content() As String
            Get
                Return _Content
            End Get
            Set(value As String)
                _Content = value
            End Set
        End Property
        Private _Content As String = String.Empty

        Public Property FBUserId() As String
            Get
                Return _FBUserId
            End Get
            Set(value As String)
                _FBUserId = value
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

        Public Property FBAppId() As String
            Get
                Return _FBAppId
            End Get
            Set(value As String)
                _FBAppId = value
            End Set
        End Property
        Private _FBAppId As String = String.Empty

        Public Property FBAppName() As String
            Get
                Return _FBAppName
            End Get
            Set(value As String)
                _FBAppName = value
            End Set
        End Property
        Private _FBAppName As String = String.Empty

        Public Property FBPageName() As String
            Get
                Return _FBPageName
            End Get
            Set(value As String)
                _FBPageName = value
            End Set
        End Property
        Private _FBPageName As String = String.Empty

        Public Property TSMAUserId() As Integer
            Get
                Return _TSMAUserId
            End Get
            Set(value As Integer)
                _TSMAUserId = value
            End Set
        End Property
        Private _TSMAUserId As Integer = 0

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

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(value As String)
                _Name = value
            End Set
        End Property
        Private _Name As String = String.Empty

        Public Property Email() As String
            Get
                Return _Email
            End Get
            Set(value As String)
                _Email = value
            End Set
        End Property
        Private _Email As String = String.Empty

        Public Sub AddEditSweepStake()
            Try
                Dim dataAccess As New DALDataAccess()
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_InsertUpdateSweepStake")
                dataAccess.AddParam("@TSMAUserId", SqlDbType.Int, ParamString(Me.TSMAUserId))
                dataAccess.AddParam("@CompanyId", SqlDbType.Int, ParamString(Me.CompanyId))
                dataAccess.AddParam("@IndustryId", SqlDbType.Int, ParamString(Me.IndustryId))
                dataAccess.AddParam("@FBUserId", SqlDbType.VarChar, ParamString(Me.FBUserId))
                dataAccess.AddParam("@FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
                dataAccess.AddParam("@FBPageName", SqlDbType.VarChar, ParamString(Me.FBPageName))
                dataAccess.AddParam("@Title", SqlDbType.VarChar, ParamString(Me.Title))
                dataAccess.AddParam("@Content", SqlDbType.VarChar, ParamString(Me.Content))
                dataAccess.AddParam("@IsPublished", SqlDbType.Int, 1)
                dataAccess.ExecuteNonQuery()
            Catch ex As Exception
                Utility.LogError(ex, "Sweep Stake", True)
                Throw
            Finally
            End Try
        End Sub

        Public Function GetSweepStake() As DataSet
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetSweepStake")
            dataAccess.AddParam("@FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            Return ds
            'If ds.Tables(0).Rows.Count > 0 Then
            '    Me.Title = ds.Tables(0).Rows(0).Item("Title").ToString
            '    Me.Content = ds.Tables(0).Rows(0).Item("Content").ToString
            '    Me.FBUserId = ds.Tables(0).Rows(0).Item("FBUserId").ToString
            '    Me.FBPageId = ds.Tables(0).Rows(0).Item("FBPageId").ToString
            'End If
        End Function

        Public Sub AddSweepStakeContest()
            Try
                Dim dataAccess As New DALDataAccess()
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_AddSweepStakeContest")
                dataAccess.AddParam("@Name", SqlDbType.VarChar, ParamString(Me.Name))
                dataAccess.AddParam("@Email", SqlDbType.VarChar, ParamString(Me.Email))
                dataAccess.AddParam("@FBUserId", SqlDbType.VarChar, ParamString(Me.FBUserId))
                dataAccess.AddParam("@TSMAUserId", SqlDbType.VarChar, ParamString(Me.TSMAUserId))
                dataAccess.AddParam("@FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
                dataAccess.AddParam("@FBAppId", SqlDbType.VarChar, ParamString(Me.FBAppId))
                dataAccess.AddParam("@FBAppName", SqlDbType.VarChar, ParamString(Me.FBAppName))

                dataAccess.ExecuteNonQuery()
            Catch ex As Exception
                Utility.LogError(ex, "Sweep Stake Contest", True)
                Throw
            Finally
            End Try
        End Sub
#Region "Copy My Saved Sweepstakes"
        Public Function CopyMySavedSweepstake() As DataSet
            Dim dsCopy As New DataSet
            Dim objDataAccess As New DALDataAccess()
            objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_CopyMySavedSweepstake")
            objDataAccess.AddParam("@cs_swid", SqlDbType.Int, Me.swid)
            objDataAccess.AddParam("@cs_FBUserid", SqlDbType.VarChar, Me.FBUserId)
            objDataAccess.AddParam("@cs_TSMAUserId", SqlDbType.Int, Me.TSMAUserId)
            dsCopy = objDataAccess.GetDataset()
            Return dsCopy
        End Function
#End Region
    End Class
End Namespace