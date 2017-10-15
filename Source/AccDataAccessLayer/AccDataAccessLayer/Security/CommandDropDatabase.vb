Imports AccDataAccessLayer.DatabaseAccess
Namespace Security

    <Serializable()> _
Public Class CommandDropDatabase
        Inherits CommandBase

#Region " Authorization Rules "

        Public Shared Function CanExecuteCommand() As Boolean
            Return (GetRootName().Trim.ToLower _
                = GetCurrentIdentity.Name.Trim.ToLower)
        End Function

#End Region

#Region " Client-side Code "

        Private mResult As Boolean
        Private _DatabaseName As String = ""

        Public ReadOnly Property Result() As Boolean
            Get
                Return mResult
            End Get
        End Property

        Private Sub BeforeServer()
            ' implement code to run on client
            ' before server is called
        End Sub

        Private Sub AfterServer()
            ' implement code to run on client
            ' after server is called
        End Sub

#End Region

#Region " Factory Methods "

        Public Shared Function TheCommand(ByVal cDatabaseName As String) As Boolean

            Dim cmd As New CommandDropDatabase
            cmd._DatabaseName = cDatabaseName
            cmd.BeforeServer()
            cmd = DataPortal.Execute(Of CommandDropDatabase)(cmd)
            cmd.AfterServer()
            Return cmd.Result

        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Server-side Code "

        Protected Overrides Sub DataPortal_Execute()

            Dim SqlCommandManager As SqlServerSpecificMethods.ISqlCommandManager = GetSqlCommandManager()

            mResult = False

            SqlCommandManager.DropDatabase(_DatabaseName)

            mResult = True

        End Sub

#End Region

    End Class

End Namespace
