Imports DataAccessLayer.DataAccessLayer
Namespace BusinessLayer
    Public Class CustomTabContent
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

        Public Property Content() As String
            Get
                Return _Content
            End Get
            Set(value As String)
                _Content = value
            End Set
        End Property
        Private _Content As String = String.Empty
        Public Property UnlikeContent() As String
            Get
                Return _UnLikeContent
            End Get
            Set(ByVal value As String)
                _UnLikeContent = value
            End Set
        End Property
        Private _UnLikeContent As String = String.Empty


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

        Public Property SubCatId() As Integer
            Get
                Return _SubCatId
            End Get
            Set(ByVal value As Integer)
                _SubCatId = value
            End Set
        End Property
        Private _SubCatId As Integer = 0

        Public Property CustomTabId() As String
            Get
                Return _CustomTabId
            End Get
            Set(ByVal value As String)
                _CustomTabId = value
            End Set
        End Property
        Private _CustomTabId As Integer = 0
        Public Property FBPageName() As String
            Get
                Return _FBPageName
            End Get
            Set(ByVal value As String)
                _FBPageName = value
            End Set
        End Property
        Private _FBPageName As String = String.Empty

        Public Property TSMAUserId() As String
            Get
                Return _TSMAUserId
            End Get
            Set(value As String)
                _TSMAUserId = value
            End Set
        End Property
        Private _TSMAUserId As String = String.Empty

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
        Public Property Phone() As String
            Get
                Return _Phone
            End Get
            Set(value As String)
                _Phone = value
            End Set
        End Property
        Private _Phone As String = String.Empty
        Public Property Message() As String
            Get
                Return _Message
            End Get
            Set(value As String)
                _Message = value
            End Set
        End Property
        Private _Message As String = String.Empty

        Public Property FBApplicationId() As String
            Get
                Return _FBApplicationId
            End Get
            Set(value As String)
                _FBApplicationId = value
            End Set
        End Property
       
        Private _FBApplicationId As String = String.Empty


        Public Property FBApplicationName() As String
            Get
                Return _FBApplicationName
            End Get
            Set(value As String)
                _FBApplicationName = value
            End Set
        End Property

        Private _FBApplicationName As String = String.Empty

        Public Property ShareText() As String
            Get
                Return _ShareText
            End Get
            Set(ByVal value As String)
                _ShareText = value
            End Set
        End Property
        Private _ShareText As String = String.Empty
        Public Property ShareMessage() As String
            Get
                Return _ShareMessage
            End Get
            Set(ByVal value As String)
                _ShareMessage = value
            End Set
        End Property
        Private _ShareMessage As String = String.Empty
        Public Property ShareImage() As String
            Get
                Return _ShareImage
            End Get
            Set(ByVal value As String)
                _ShareImage = value
            End Set
        End Property
        Private _ShareImage As String = String.Empty
        Public Property EmailText() As String
            Get
                Return _EmailText
            End Get
            Set(ByVal value As String)
                _EmailText = value
            End Set
        End Property
        Private _EmailText As String = String.Empty
        Public Property PdfLink() As String
            Get
                Return _PdfLink
            End Get
            Set(ByVal value As String)
                _PdfLink = value
            End Set
        End Property
        Private _PdfLink As String = String.Empty

        Public Sub AddEditCustomTab()
            Try
                Dim dataAccess As New DALDataAccess()
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_InsertUpdateCustomTab")
                dataAccess.AddParam("@FBUserId", SqlDbType.VarChar, ParamString(Me.FBUserId))
                dataAccess.AddParam("@FBApplicationId", SqlDbType.VarChar, ParamString(Me.FBApplicationId))
                dataAccess.AddParam("@FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
                dataAccess.AddParam("@FBPageName", SqlDbType.VarChar, ParamString(Me.FBPageName))
                dataAccess.AddParam("@Title", SqlDbType.VarChar, ParamString(Me.Title))
                dataAccess.AddParam("@Content", SqlDbType.VarChar, ParamString(Me.Content))
                dataAccess.ExecuteNonQuery()
            Catch ex As Exception
                Utility.LogError(ex, "Custom Tab", True)
                Throw
            Finally
            End Try
        End Sub

        Public Sub AddEditCustomTabNew()
            Try
                Dim dataAccess As New DALDataAccess()
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_InsertUpdateCustomTabNew")
                dataAccess.AddParam("@FBUserId", SqlDbType.VarChar, ParamString(Me.FBUserId))
                dataAccess.AddParam("@FBApplicationId", SqlDbType.VarChar, ParamString(Me.FBApplicationId))
                dataAccess.AddParam("@FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
                dataAccess.AddParam("@FBPageName", SqlDbType.VarChar, ParamString(Me.FBPageName))
                dataAccess.AddParam("@Title", SqlDbType.VarChar, ParamString(Me.Title))
                dataAccess.AddParam("@Content", SqlDbType.VarChar, ParamString(Me.Content))
                dataAccess.AddParam("@ct_id", SqlDbType.Int, ParamInt(Me.CustomTabId))

                dataAccess.ExecuteNonQuery()
            Catch ex As Exception
                Utility.LogError(ex, "Custom Tab", True)
                Throw
            Finally
            End Try
        End Sub

        Public Function getFacebookAppIdbyPage() As dataset
            Dim dataAccess As New DALDataAccess()
            Dim ds As New DataSet
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_getFacebookApplicationIdForPage")
            dataAccess.AddParam("@ctsc_id", SqlDbType.Int, Me.SubCatId)
            ds = dataAccess.GetDataset
            Return ds
        End Function

        'Public Function getFacebookAppIdbyPageString() As String
        '    Dim dataAccess As New DALDataAccess()
        '    dataAccess.AddCommand(CommandType.StoredProcedure, "prc_getFacebookApplicationIdForPage")
        '    dataAccess.AddParam("@ctsc_id", SqlDbType.Int, Me.SubCatId)
        '    Dim strAppId As String = dataAccess.ExecuteScalar()
        'End Function

        Public Sub GetCustomTabContent()
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetCustomTabContent")
            dataAccess.AddParam("@FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
            dataAccess.AddParam("@FBApplicationId", SqlDbType.VarChar, ParamString(Me.FBApplicationId))
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            If ds.Tables(0).Rows.Count > 0 Then
                Me.Title = ds.Tables(0).Rows(0).Item("Title")
                Me.Content = ds.Tables(0).Rows(0).Item("Content")
                Me.FBUserId = ds.Tables(0).Rows(0).Item("FBUserId")
                Me.FBPageId = ds.Tables(0).Rows(0).Item("FBPageId")
                Me.FBPageName = ds.Tables(0).Rows(0).Item("FBPageName")
            End If
        End Sub
        Public Sub GetSweepstakeContent()
            Dim dataAccess As New DALDataAccess()
            dataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetSweepstakeContent")
            dataAccess.AddParam("@FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
            dataAccess.AddParam("@FBApplicationId", SqlDbType.VarChar, ParamString(Me.FBApplicationId))
            Dim ds As New DataSet
            ds = dataAccess.GetDataset()
            If ds.Tables(0).Rows.Count > 0 Then
                Me.Title = ds.Tables(0).Rows(0).Item("Title")
                Me.Content = ds.Tables(0).Rows(0).Item("Content")
                Me.FBUserId = ds.Tables(0).Rows(0).Item("FBUserId")
                Me.FBPageId = ds.Tables(0).Rows(0).Item("FBPageId")
                Me.FBPageName = ds.Tables(0).Rows(0).Item("FBPageName")
                Me.ShareText = ds.Tables(0).Rows(0).Item("ShareText")
                Me.ShareMessage = ds.Tables(0).Rows(0).Item("ShareMessage")
                Me.ShareImage = ds.Tables(0).Rows(0).Item("ShareImage")
                Me.EmailText = ds.Tables(0).Rows(0).Item("EmailText")
                Me.PdfLink = ds.Tables(0).Rows(0).Item("pdflink")
                Me.UnlikeContent = ds.Tables(0).Rows(0).Item("Content")
            End If
        End Sub

        Public Sub AddCustomTabInfo()
            Try
                Dim dataAccess As New DALDataAccess()
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_AddCustomTabInfo")
                dataAccess.AddParam("@Name", SqlDbType.VarChar, ParamString(Me.Name))
                dataAccess.AddParam("@Email", SqlDbType.VarChar, ParamString(Me.Email))
                dataAccess.AddParam("@Phone", SqlDbType.VarChar, ParamString(Me.Phone))
                dataAccess.AddParam("@Message", SqlDbType.VarChar, ParamString(Me.Message))
                dataAccess.AddParam("@FBUserId", SqlDbType.VarChar, ParamString(Me.FBUserId))
                dataAccess.AddParam("@TSMAUserId", SqlDbType.VarChar, ParamString(Me.TSMAUserId))
                dataAccess.AddParam("@FBPageId", SqlDbType.VarChar, ParamString(Me.FBPageId))
                dataAccess.AddParam("@FBAppId", SqlDbType.VarChar, ParamString(Me.FBApplicationId))
                dataAccess.AddParam("@FBAppName", SqlDbType.VarChar, ParamString(Me.FBApplicationName))

                dataAccess.ExecuteNonQuery()
            Catch ex As Exception
                Utility.LogError(ex, "Custom Tab Information", True)
                Throw
            Finally
            End Try
        End Sub
    End Class
End Namespace