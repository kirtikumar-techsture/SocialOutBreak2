Imports DataAccessLayer.DataAccessLayer
Imports BusinessAccessLayer.BusinessLayer
Public Class cls_CompanyPasswordValidation
    Private _isError As Boolean = False
    ReadOnly Property isError() As Boolean
        Get
            Return _isError
        End Get
    End Property

    Private strMessage As String = ""
    Public Property Message() As String
        Set(value As String)
            strMessage = value
        End Set
        Get
            Return strMessage
        End Get
    End Property

    Public Sub PasswordValidation(Password As String, CompanyID As Integer)

        Dim ds As New DataSet
        Dim objDataAccess As New DALDataAccess()
        objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetComapnyPasswod")
        objDataAccess.AddParam("@CompanyId", SqlDbType.Int, CompanyID)
        objDataAccess.AddParam("@Password", SqlDbType.VarChar, Password)
        ds = objDataAccess.GetDataset()

        If ds.Tables(0).Rows.Count > 0 Then
            If Password = ds.Tables(0).Rows(0).Item("c_Password") Then
                strMessage = ""
                _isError = False
            End If
        Else
            'strMessage = "Incorrect Password   " & CompanyID & "  pwd  " & Password
            strMessage = "Password does not match."
            _isError = True
        End If
        'If Password = "legacy" Then
        '    strMessage = ""
        '    _isError = False
        'Else
        '    strMessage = "Password does not match."
        '    _isError = True
        'End If

    End Sub
End Class
