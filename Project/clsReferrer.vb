Imports DataAccessLayer.DataAccessLayer

Public Class clsReferrer

    Private strReferrerUrl As String = ""
    Public Property ReferrerUrl() As String
        Set(value As String)
            strReferrerUrl = value
        End Set
        Get
            Return strReferrerUrl
        End Get
    End Property

    Private strFBReferrerUrl As String = ""
    Public Property FBReferrerUrl() As String
        Set(value As String)
            strFBReferrerUrl = value
        End Set
        Get
            Return strFBReferrerUrl
        End Get
    End Property


    Public Function GetReferrerData() As DataSet
        Dim ds As New DataSet
        Dim objDataAccess As New DALDataAccess()
        objDataAccess.AddCommand(CommandType.StoredProcedure, "prc_GetReferrerData")
        objDataAccess.AddParam("@ReferrerUrl", SqlDbType.VarChar, ReferrerUrl)
        ds = objDataAccess.GetDataset()
        Return ds
    End Function

End Class
