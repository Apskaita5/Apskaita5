Namespace DatabaseAccess.DatabaseStructure

    <Serializable()> _
Public Class DatabaseStructureError
        Inherits BusinessBase(Of DatabaseStructureError)

#Region " Business Methods "

        Private _GID As Guid = Guid.NewGuid
        Private _IsChecked As Boolean = True
        Private _Description As String = ""
        Private _Table As String
        Private _Field As String
        Private _SqlStatementsToCorrect As String = ""
        Private _CanBeFixedAutomatically As Boolean = True
        Private _IsComplexError As Boolean = False
        Private _ComplexErrorCode As String = ""
        Private _IsFixed As Boolean = False
        Private _ErrorType As DatabaseStructureErrorType = DatabaseStructureErrorType.TableMissing


        Public Property IsChecked() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsChecked
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _IsChecked <> value Then
                    If _CanBeFixedAutomatically Then
                        _IsChecked = value
                    Else
                        _IsChecked = False
                    End If
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property Description() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Description.Trim
            End Get
        End Property

        Public ReadOnly Property Table() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Table.Trim
            End Get
        End Property

        Public ReadOnly Property Field() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Field.Trim
            End Get
        End Property

        Public ReadOnly Property SqlStatementsToCorrect() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SqlStatementsToCorrect.Trim
            End Get
        End Property

        Public ReadOnly Property CanBeFixedAutomatically() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CanBeFixedAutomatically
            End Get
        End Property

        Public ReadOnly Property IsComplexError() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsComplexError
            End Get
        End Property

        Public ReadOnly Property ComplexErrorCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexErrorCode.Trim
            End Get
        End Property

        Public ReadOnly Property IsFixed() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsFixed
            End Get
        End Property

        Friend ReadOnly Property ErrorType() As DatabaseStructureErrorType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ErrorType
            End Get
        End Property



        Friend Sub MarkFixed()
            _IsFixed = True
        End Sub

        Private Function GetSqlStatementList() As List(Of String)
            Dim result As New List(Of String)
            If _ErrorType <> DatabaseStructureErrorType.ProcedureDefinitionObsolete _
                AndAlso _ErrorType <> DatabaseStructureErrorType.ProcedureMissing Then
                For Each s As String In _SqlStatementsToCorrect.Trim.Split(New Char() {";"c}, _
                StringSplitOptions.RemoveEmptyEntries)
                    result.Add(s & ";")
                Next
            Else
                result.Add(_SqlStatementsToCorrect.Trim)
            End If
            Return result
        End Function

        Protected Overrides Function GetIdValue() As Object
            Return _GID
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetDatabaseStructureError(ByVal nDescription As String, _
            ByVal nTable As String, ByVal nField As String, ByVal nSqlStatementsToCorrect As String, _
            ByVal nCanBeFixedAutomatically As Boolean, ByVal nErrorCode As String) As DatabaseStructureError

            Return New DatabaseStructureError(nDescription, nTable, nField, _
                nSqlStatementsToCorrect, nCanBeFixedAutomatically, True, nErrorCode, _
                DatabaseStructureErrorType.Custom)

        End Function

        Friend Shared Function GetDatabaseStructureError(ByVal nDescription As String, _
            ByVal nTable As String, ByVal nField As String, _
            ByVal nSqlStatementsToCorrect As String, ByVal nCanBeFixedAutomatically As Boolean, _
            ByVal nErrorType As DatabaseStructureErrorType) As DatabaseStructureError

            Return New DatabaseStructureError(nDescription, nTable, nField, _
                nSqlStatementsToCorrect, nCanBeFixedAutomatically, False, "", nErrorType)

        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal nDescription As String, ByVal nTable As String, _
            ByVal nField As String, ByVal nSqlStatementsToCorrect As String, _
            ByVal nCanBeFixedAutomatically As Boolean, ByVal nIsComplexError As Boolean, _
            ByVal nComplexErrorCode As String, ByVal nErrorType As DatabaseStructureErrorType)
            MarkAsChild()
            Fetch(nDescription, nTable, nField, nSqlStatementsToCorrect, _
                nCanBeFixedAutomatically, nIsComplexError, nComplexErrorCode, nErrorType)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal nDescription As String, ByVal nTable As String, _
            ByVal nField As String, ByVal nSqlStatementsToCorrect As String, _
            ByVal nCanBeFixedAutomatically As Boolean, ByVal nIsComplexError As Boolean, _
            ByVal nComplexErrorCode As String, ByVal nErrorType As DatabaseStructureErrorType)

            _Description = nDescription
            _Table = nTable
            _Field = nField
            _SqlStatementsToCorrect = nSqlStatementsToCorrect
            _CanBeFixedAutomatically = nCanBeFixedAutomatically
            _IsComplexError = nIsComplexError
            _ComplexErrorCode = nComplexErrorCode
            _ErrorType = nErrorType

            _IsChecked = _CanBeFixedAutomatically

            MarkOld()
            MarkDirty()

        End Sub

        Public Sub Update(ByRef parent As DatabaseStructureErrorList)

            If Not _IsChecked OrElse Not _CanBeFixedAutomatically OrElse _IsFixed Then Exit Sub

            If _IsComplexError Then
                parent.CustomErrorManager.RepairCustomError(parent, Me)
                MarkFixed()
                Exit Sub
            End If

            Dim myComm As SQLCommand
            For Each statement As String In Me.GetSqlStatementList
                myComm = New SQLCommand("RawSQL", statement)
                myComm.Execute()
            Next

            MarkFixed()

        End Sub

#End Region

    End Class

End Namespace