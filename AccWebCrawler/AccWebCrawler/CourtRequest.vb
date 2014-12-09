Public Class CourtRequest

    Public ReadOnly FolderPath As String
    Public ReadOnly DateFrom As Date
    Public ReadOnly DateTo As Date
    Public ReadOnly ForCourtType As CourtType

    Public Sub New(ByVal nForCourtType As CourtType, ByVal nFolderPath As String, _
        ByVal nDateFrom As Date, ByVal nDateTo As Date)
        If nFolderPath Is Nothing Then nFolderPath = ""
        ForCourtType = nForCourtType
        FolderPath = nFolderPath
        DateFrom = nDateFrom
        DateTo = nDateTo
    End Sub

End Class
