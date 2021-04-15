Namespace DatabaseAccess

    ''' <summary>
    ''' ReadOnly object to get CharSetDirectory, which is needed to 
    ''' restore database from backup file. Possibly MySQL specific.
    ''' </summary>
    <Serializable()> _
    Public Class CharSetDir
        Inherits ReadOnlyBase(Of CharSetDir)

#Region " Business Methods "

        Private mId As Guid = New Guid
        Private _CharSetDir As String = ""
        Private _BaseDir As String = ""

        Public ReadOnly Property CharSetDir() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CharSetDir
            End Get
        End Property

        Public ReadOnly Property BaseDir() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BaseDir
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object

            Return mId

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

            ' TODO: add authorization rules
            'AuthorizationRules.AllowRead("", "")

        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Admin")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetCharSetDir() As CharSetDir
            Return DataPortal.Fetch(Of CharSetDir)(New Criteria())
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria

            Public Sub New()

            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            Dim myComm As New SQLCommand("ShowCharSetDir")
            Using myData As DataTable = myComm.Fetch
                Try
                    _CharSetDir = myData.Rows(0).Item(1).ToString.Substring(0, _
                        myData.Rows(0).Item(1).ToString.Length - 1)
                Catch ex As Exception
                End Try
            End Using

            myComm = New SQLCommand("ShowBaseDir")
            Using myData As DataTable = myComm.Fetch
                Try
                    _BaseDir = myData.Rows(0).Item(1).ToString.Substring(0, _
                        myData.Rows(0).Item(1).ToString.Length - 1)
                Catch ex As Exception
                End Try
            End Using

        End Sub

#End Region

    End Class

End Namespace
