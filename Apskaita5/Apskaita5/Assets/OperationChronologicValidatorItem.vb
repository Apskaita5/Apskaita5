Namespace Assets

    <Serializable()> _
    Public Class OperationChronologicValidatorItem
        Inherits ReadOnlyBase(Of OperationChronologicValidatorItem)

#Region " Business Methods "

        Private _OperationType As LtaOperationType = LtaOperationType.Discard
        Private _AccountChangeType As LtaAccountChangeType = LtaAccountChangeType.AcquisitionAccount
        Private _MaxDate As Date = Date.MaxValue
        Private _ChronologyType As OperationChronologyType = OperationChronologyType.Overall


        Public ReadOnly Property OperationType() As LtaOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationType
            End Get
        End Property

        Public ReadOnly Property AccountChangeType() As LtaAccountChangeType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountChangeType
            End Get
        End Property

        Public ReadOnly Property MaxDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MaxDate
            End Get
        End Property

        Public ReadOnly Property ChronologyType() As OperationChronologyType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologyType
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return ToString()
        End Function

        Public Overrides Function ToString() As String
            If _OperationType = LtaOperationType.AccountChange Then
                Return _ChronologyType.ToString & ":" & _OperationType.ToString & ":" & _AccountChangeType.ToString
            Else
                Return _ChronologyType.ToString & ":" & _OperationType.ToString
            End If
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetOperationChronologicValidatorItem(ByVal dr As DataRow) As OperationChronologicValidatorItem
            Return New OperationChronologicValidatorItem(dr)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _OperationType = ConvertEnumDatabaseStringCode(Of LtaOperationType)(CStrSafe(dr.Item(1)))
            If _OperationType = LtaOperationType.AccountChange Then
                Try
                    _AccountChangeType = ConvertEnumDatabaseStringCode(Of LtaAccountChangeType) _
                        (CStrSafe(dr.Item(2)))
                Catch ex As Exception
                End Try
            End If
            _ChronologyType = ConvertEnumDatabaseCode(Of OperationChronologyType)(CIntSafe(dr.Item(3), 0))
            _MaxDate = CDateSafe(dr.Item(4), Date.MaxValue)

        End Sub

#End Region

    End Class

End Namespace