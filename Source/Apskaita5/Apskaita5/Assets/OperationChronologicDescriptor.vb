Namespace Assets

    ''' <summary>
    ''' Represents chronologic limitations that a long term asset operation imposes
    ''' on other long term asset operations.
    ''' </summary>
    ''' <remarks>A helper object for <see cref="OperationChronologicValidator">OperationChronologicValidator</see>.
    ''' Acts as a common data container to make changing it's content or datasource easyer.
    ''' Possibly could be made generic for goods also???</remarks>
    <Serializable()> _
    Public NotInheritable Class OperationChronologicDescriptor

#Region " Business Methods "

        Private ReadOnly _OperationType As LtaOperationType = LtaOperationType.Transfer
        Private ReadOnly _AffectsOperations As LtaOperationType() = Nothing
        Private ReadOnly _LocksOperations As LtaOperationType() = Nothing
        Private ReadOnly _IsAffectedByOperations As LtaOperationType() = Nothing
        Private ReadOnly _IsLockedByOperations As LtaOperationType() = Nothing

        ''' <summary>
        ''' Gets a <see cref="LtaOperationType">current long term asset operation type</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property OperationType() As LtaOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationType
            End Get
        End Property

        ''' <summary>
        ''' Gets an array of <see cref="LtaOperationType">long term asset operation types</see>
        ''' that are affected by the current operation type, i.e. the affected operations'
        ''' dates and financial data is limited by the current operation date.
        ''' </summary>
        ''' <remarks>If the value is set to nothing or an empty array,
        ''' then the current operation does not affect any operations.</remarks>
        Public ReadOnly Property AffectsOperations() As LtaOperationType()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AffectsOperations
            End Get
        End Property

        ''' <summary>
        ''' Gets an array of <see cref="LtaOperationType">long term asset operation types</see>
        ''' that are locked by the current operation type, i.e. the affected operations'
        ''' dates and financial data are rendered readonly 
        ''' by a subsequent operation of current type.
        ''' </summary>
        ''' <remarks>If the value is set to nothing or an empty array,
        ''' then the current operation does not lock any operations.</remarks>
        Public ReadOnly Property LocksOperations() As LtaOperationType()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LocksOperations
            End Get
        End Property

        ''' <summary>
        ''' Gets an array of <see cref="LtaOperationType">long term asset operation types</see>
        ''' that affect the current operation type, i.e. the current operation type
        ''' date and financial data is limited.
        ''' </summary>
        ''' <remarks>If the value is set to nothing or an empty array,
        ''' then the current operation does not affect any operations.
        ''' Is required (AffectsOperations is not enough) to provide 
        ''' full chronological information without modifying other descriptors.</remarks>
        Public ReadOnly Property IsAffectedByOperations() As LtaOperationType()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsAffectedByOperations
            End Get
        End Property

        ''' <summary>
        ''' Gets an array of <see cref="LtaOperationType">long term asset operation types</see>
        ''' that lock the current operation type, i.e. a subsequent operation
        ''' locks the current type operation's date and financial data.
        ''' </summary>
        ''' <remarks>If the value is set to nothing or an empty array,
        ''' then no operation can lock the current operation type.
        ''' Is required (LocksOperations is not enough) to provide 
        ''' full chronological information without modifying other descriptors.</remarks>
        Public ReadOnly Property IsLockedByOperations() As LtaOperationType()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsLockedByOperations
            End Get
        End Property


        ''' <summary>
        ''' Gets an array of <see cref="LtaOperationType">long term asset operation types</see>
        ''' that affect the <paramref name="forType">requested operation type</paramref>, 
        ''' i.e. the requested operation type date and financial data is limited.
        ''' </summary>
        ''' <param name="list">A full list of available OperationChronologicDescriptor's.</param>
        ''' <param name="forType">A type for which the affecting types are returned.</param>
        ''' <remarks>Aggregates <see cref="AffectsOperations">AffectsOperations</see>
        ''' and <see cref="IsAffectedByOperations">IsAffectedByOperations</see>
        ''' arrays for the specified type.
        ''' Throws a <see cref="NotImplementedException">NotImplementedException</see>
        ''' if the requested type does not have a descriptor.</remarks>
        Public Shared Function GetAffectingTypes(ByVal list As List(Of OperationChronologicDescriptor), _
            ByVal forType As LtaOperationType) As LtaOperationType()

            If list Is Nothing Then
                Throw New ArgumentNullException("list")
            End If

            Dim result As New List(Of LtaOperationType)

            Dim isFound As Boolean = False

            For Each desc As OperationChronologicDescriptor In list

                If desc._OperationType = forType Then

                    If Not desc._IsAffectedByOperations Is Nothing Then
                        For Each t As LtaOperationType In desc._IsAffectedByOperations
                            If Not result.Contains(t) Then result.Add(t)
                        Next
                    End If

                    isFound = True

                ElseIf Not desc._AffectsOperations Is Nothing Then

                    If Not Array.IndexOf(desc._AffectsOperations, forType) < 0 _
                        AndAlso Not result.Contains(desc._OperationType) Then
                        result.Add(desc._OperationType)
                    End If

                End If

            Next

            If Not isFound Then
                Throw New NotImplementedException(String.Format( _
                    My.Resources.Assets_OperationChronologicDescriptor_TypeNotImplemented, _
                    forType.ToString()))
            End If

            Return result.ToArray()

        End Function

        ''' <summary>
        ''' Gets an array of <see cref="LtaOperationType">long term asset operation types</see>
        ''' that lock the <paramref name="forType">requested operation type</paramref>, 
        ''' i.e. a subsequent operation locks the requested type operation's date 
        ''' and financial data.
        ''' </summary>
        ''' <param name="list">A full list of available OperationChronologicDescriptor's.</param>
        ''' <param name="forType">A type for which the locking types are returned.</param>
        ''' <remarks>Aggregates <see cref="LocksOperations">LocksOperations</see>
        ''' and <see cref="IsLockedByOperations">IsLockedByOperations</see>
        ''' arrays for the specified type.
        ''' Throws a <see cref="NotImplementedException">NotImplementedException</see>
        ''' if the requested type does not have a descriptor.</remarks>
        Public Shared Function GetLockingTypes(ByVal list As List(Of OperationChronologicDescriptor), _
            ByVal forType As LtaOperationType) As LtaOperationType()

            If list Is Nothing Then
                Throw New ArgumentNullException("list")
            End If

            Dim result As New List(Of LtaOperationType)

            Dim isFound As Boolean = False

            For Each desc As OperationChronologicDescriptor In list

                If desc._OperationType = forType Then

                    If Not desc._IsLockedByOperations Is Nothing Then
                        For Each t As LtaOperationType In desc._IsLockedByOperations
                            If Not result.Contains(t) Then result.Add(t)
                        Next
                    End If

                    isFound = True

                ElseIf Not desc._LocksOperations Is Nothing Then

                    If Not Array.IndexOf(desc._LocksOperations, forType) < 0 _
                        AndAlso Not result.Contains(desc._OperationType) Then
                        result.Add(desc._OperationType)
                    End If

                End If

            Next

            If Not isFound Then
                Throw New NotImplementedException(String.Format( _
                    My.Resources.Assets_OperationChronologicDescriptor_TypeNotImplemented, _
                    forType.ToString()))
            End If

            Return result.ToArray()

        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new OperationChronologicDescriptor instance.
        ''' </summary>
        ''' <param name="nOperationType">A type of the long term asset operation
        ''' that is described by the instance.</param>
        ''' <param name="nAffectsOperations">An array of the long term asset operation
        ''' types that are affected by an operation of described type (use null for none).</param>
        ''' <param name="nLocksOperations">An array of the long term asset operation
        ''' types that are locked by a subsequent operation of described type (use null for none).</param>
        ''' <param name="nIsAffectedByOperations">An array of the long term asset operation
        ''' types that affect an operation of described type (use null for none).</param>
        ''' <param name="nIsLockedByOperations">An array of the long term asset operation
        ''' types that lock an operation of described type (use null for none).</param>
        ''' <remarks></remarks>
        Public Shared Function GetOperationChronologicDescriptor( _
            ByVal nOperationType As LtaOperationType, _
            ByVal nAffectsOperations As LtaOperationType(), _
            ByVal nLocksOperations As LtaOperationType(), _
            ByVal nIsAffectedByOperations As LtaOperationType(), _
            ByVal nIsLockedByOperations As LtaOperationType()) As OperationChronologicDescriptor
            Return New OperationChronologicDescriptor(nOperationType, _
                nAffectsOperations, nLocksOperations, nIsAffectedByOperations, _
                nIsLockedByOperations)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal nOperationType As LtaOperationType, _
            ByVal nAffectsOperations As LtaOperationType(), _
            ByVal nLocksOperations As LtaOperationType(), _
            ByVal nIsAffectedByOperations As LtaOperationType(), _
            ByVal nIsLockedByOperations As LtaOperationType())

            _OperationType = nOperationType
            _AffectsOperations = nAffectsOperations
            _LocksOperations = nLocksOperations
            _IsAffectedByOperations = nIsAffectedByOperations
            _IsLockedByOperations = nIsLockedByOperations

        End Sub

#End Region

    End Class

End Namespace
