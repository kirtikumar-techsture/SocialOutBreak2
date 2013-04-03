Imports System
Imports System.Web
Imports DataAccessLayer.DataAccessLayer
Imports System.Data

Namespace BusinessLayer
    <RecordAttribute("tbl_AdminUsers,tbl_Users", "prc_adminLogin,prc_UserLogin", New [String](0) {"AdminLogin"})> _
    Public Class BALlogin
        Inherits IRecord

#Region "Variables"
        Private _strUserName As String = String.Empty
        Private _strFirstName As String = String.Empty
        Private _strLastName As String = String.Empty
        Private _strFBUserEmail As String = String.Empty
        Private _strPassword As String = String.Empty
        Private _intUserId As Integer = 0
        Private _strFBId As String = String.Empty
        Private _strFBUserName As String = String.Empty
        Private _strCompanyID As Integer = 0
        Private _strIndustryID As Integer = 0
#End Region

#Region "Properties"
        ''' <summary>
        ''' Gets and sets the <c>Username</c> Password
        ''' </summary>
        ''' <value>The <c>Username</c> Password</value>

        Public Property UserName() As String
            Get
                Return _strUserName
            End Get
            Set(value As String)
                _strUserName = value
            End Set
        End Property

        Public Property password() As String
            Get
                Return _strPassword
            End Get
            Set(value As String)
                _strPassword = value
            End Set
        End Property

        Public Property FirstName() As String
            Get
                Return _strFirstName
            End Get
            Set(value As String)
                _strFirstName = value
            End Set
        End Property

        Public Property LastName() As String
            Get
                Return _strLastName
            End Get
            Set(value As String)
                _strLastName = value
            End Set
        End Property

        Public ReadOnly Property UserId() As String
            Get
                Return _intUserId
            End Get
        End Property

        Public Property FBId() As String
            Get
                Return _strFBId
            End Get
            Set(value As String)
                _strFBId = value
            End Set
        End Property
        Public Property FBUserName() As String
            Get
                Return _strFBUserName
            End Get
            Set(value As String)
                _strFBUserName = value
            End Set
        End Property

        Public Property FBUserEmail() As String
            Get
                Return _strFBUserEmail
            End Get
            Set(value As String)
                _strFBUserEmail = value
            End Set
        End Property

        Public Property CompanyID() As Integer
            Get
                Return _strCompanyID
            End Get
            Set(value As Integer)
                _strCompanyID = value
            End Set
        End Property
        Public Property IndustryID() As Integer
            Get
                Return _strIndustryID
            End Get
            Set(value As Integer)
                _strIndustryID = value
            End Set
        End Property
        'Public Property isError() As Boolean
        '    Get
        '        Return _isError
        '    End Get
        'End Property


        'Public Property Message() As String
        '    Get
        '        Return strMessage
        '    End Get
        'End Property
#End Region

#Region "CONSTRUCTORS"
        Public Sub New()
        End Sub
#End Region

#Region "Check Site User Login Data"
        Public Function CheckLogin() As DataSet
            Try
                'Dim objEncDec As New Utility
                Dim dataAccess As New DALDataAccess()
                Dim ds As New DataSet
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_UserLogin")
                dataAccess.AddParam("@user_UserName", SqlDbType.NVarChar, ParamString(Me.UserName))
                dataAccess.AddParam("@user_Password", SqlDbType.NVarChar, ParamString(Utility.Encryption(Me.password)))
                ds = dataAccess.GetDataset()
                Return ds
                'If ds.Tables(0).Rows.Count > 0 Then

                '    'If ds.Tables(0).Rows(0).Item("u_Username") = _strUserName And ds.Tables(0).Rows(0).Item("u_Password") = _strPassword Then
                '    '    _intUserId = ds.Tables(0).Rows(0).Item("u_Id")
                '    'End If
                'End If
                'ds = Nothing
            Catch ex As Exception
                Utility.LogError(ex, "User Login", True)
                Throw
            Finally
            End Try
            'Return _intUserId
        End Function
#End Region

#Region "Add SO User"
        Public Function AddSOUser() As DataSet
            Try
                'Dim objEncDec As New Utility
                Dim dataAccess As New DALDataAccess()
                Dim ds As New DataSet
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_AddSOUser")
                dataAccess.AddParam("@FBId", SqlDbType.NVarChar, ParamString(Me.FBId))
                dataAccess.AddParam("@FBUserName", SqlDbType.NVarChar, ParamString(Me.FBUserName))
                dataAccess.AddParam("@FBUserFirstName", SqlDbType.NVarChar, ParamString(Me.FirstName))
                dataAccess.AddParam("@FBUserLastName", SqlDbType.NVarChar, ParamString(Me.LastName))
                dataAccess.AddParam("@FBUserEmail", SqlDbType.NVarChar, ParamString(Me.FBUserEmail))
                dataAccess.AddParam("@UserName", SqlDbType.NVarChar, ParamString(Me.UserName))
                dataAccess.AddParam("@Password", SqlDbType.NVarChar, ParamString(Me.password))
                dataAccess.AddParam("@CompanyID", SqlDbType.NVarChar, ParamString(Me.CompanyID))
                dataAccess.AddParam("@IndustryID", SqlDbType.NVarChar, ParamString(Me.IndustryID))

                ds = dataAccess.GetDataset()
                Return ds
                'If ds.Tables(0).Rows.Count > 0 Then

                '    'If ds.Tables(0).Rows(0).Item("u_Username") = _strUserName And ds.Tables(0).Rows(0).Item("u_Password") = _strPassword Then
                '    '    _intUserId = ds.Tables(0).Rows(0).Item("u_Id")
                '    'End If
                'End If
                'ds = Nothing
            Catch ex As Exception
                Utility.LogError(ex, "User Login", True)
                Throw
            Finally
            End Try
            'Return _intUserId
        End Function
#End Region

#Region "Check SO User"
        Public Function CheckSOUser() As DataSet
            Try
                'Dim objEncDec As New Utility
                Dim dataAccess As New DALDataAccess()
                Dim ds As New DataSet
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_CheckSOUser")
                dataAccess.AddParam("@FBId", SqlDbType.NVarChar, ParamString(Me.FBId))
                '   dataAccess.AddParam("@FBUserName", SqlDbType.NVarChar, ParamString(Me.FBUserName))
                ' dataAccess.AddParam("@CompanyID", SqlDbType.NVarChar, ParamString(Me.CompanyID))
                'dataAccess.AddParam("@IndustryID", SqlDbType.NVarChar, ParamString(Me.IndustryID))

                ds = dataAccess.GetDataset()
                Return ds
                'If ds.Tables(0).Rows.Count > 0 Then

                '    'If ds.Tables(0).Rows(0).Item("u_Username") = _strUserName And ds.Tables(0).Rows(0).Item("u_Password") = _strPassword Then
                '    '    _intUserId = ds.Tables(0).Rows(0).Item("u_Id")
                '    'End If
                'End If
                'ds = Nothing
            Catch ex As Exception
                Utility.LogError(ex, "User Login", True)
                Throw
            Finally
            End Try
            'Return _intUserId
        End Function
#End Region

#Region "Check Admin User Login Data"
        Public Function AdminLoginDetails() As DataSet
            Try
                'Dim objEncDec As New Utility
                Dim dataAccess As New DALDataAccess()
                Dim ds As New DataSet
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_AdminLogin")
                dataAccess.AddParam("@aus_UserName", SqlDbType.NVarChar, ParamString(Me.UserName))
                dataAccess.AddParam("@aus_Password", SqlDbType.NVarChar, Utility.Encryption(ParamString(Me.password)))
                ds = dataAccess.GetDataset()
                Return ds
                'If ds.Tables(0).Rows.Count > 0 Then

                'Else
                '    ds = Nothing
                'End If
            Catch ex As Exception
                Utility.LogError(ex, "Admin Login", True)
                Throw
            Finally
            End Try
        End Function
#End Region

#Region "Generate Image"
        Public Function GenereateImage() As DataSet
            Try
                Dim dataAccess As New DALDataAccess()
                Dim ds As New DataSet
                dataAccess.AddCommand(CommandType.StoredProcedure, "prc_getImageCode")
                'dataAccess.AddParam("@pnl_FbUserId", SqlDbType.NVarChar, ParamString(Me._strFBUserId))
                ds = dataAccess.GetDataset()
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds
                End If
            Catch ex As Exception
                Utility.LogError(ex, "Generate Images", True)
                Throw
            Finally
            End Try
        End Function
#End Region
    End Class
End Namespace


