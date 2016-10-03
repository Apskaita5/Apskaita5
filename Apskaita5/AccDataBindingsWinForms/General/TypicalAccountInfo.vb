Imports System.ComponentModel

''' <summary>
''' A wraper object around <see cref="My.Resources.TypicalAccountsGeneral">Resources.TypicalAccountsGeneral</see>.
''' </summary>
''' <remarks></remarks>
Public Class TypicalAccountInfo

    Private _AccountNo As Long = 0
    Private _AccountName As String = ""


    Public ReadOnly Property AccountNo() As Long
        Get
            Return _AccountNo
        End Get
    End Property

    Public ReadOnly Property AccountName() As String
        Get
            Return _AccountName
        End Get
    End Property


    Private Sub New(ByVal source As String)
        Try
            _AccountNo = Long.Parse(GetElement(source, 0))
        Catch ex As Exception
        End Try
        _AccountName = GetElement(source, 1)
    End Sub


    Private Function IsEmpty() As Boolean
        Return Not _AccountNo > 0 OrElse StringIsNullOrEmpty(_AccountName)
    End Function


    Public Shared Function GetTypicalAccountList() As BindingList(Of TypicalAccountInfo)

        Dim result As New BindingList(Of TypicalAccountInfo)

        For Each source As String In My.Resources.TypicalAccountsGeneral.Split( _
            New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
            Dim newItem As New TypicalAccountInfo(source)
            If Not newItem.IsEmpty() Then result.Add(newItem)
        Next

        Return result

    End Function

End Class
