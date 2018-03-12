Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of a <see cref="SharesAccountEntryList">SharesAccountEntryList</see> report.
    ''' Contains information about an operation of a particular class of shares for a particular person (shareholder).
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="SharesAccountEntryList">DebtInfoList</see>.</remarks>
    <Serializable()>
    Public NotInheritable Class SharesAccountEntry
        Inherits ReadOnlyBase(Of SharesAccountEntry)

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Date As Date = Today
        Private _Document As String = ""
        Private _AmountAcquired As Double = 0
        Private _AmountDiscarded As Double = 0
        Private _AmountAfterOperation As Double = 0
        Private _CorrespondingAccounts As String = ""
        Private _Remarks As String = ""


        ''' <summary>
        ''' Gets an ID of the shares operation.
        ''' </summary>
        ''' <returns>Corresponds to <see cref="General.SharesOperation.ID">General.SharesOperation.ID</see>.</returns>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a registration date of the shares operation.
        ''' </summary>
        ''' <returns>Corresponds to <see cref="General.SharesOperation.Date">General.SharesOperation.Date</see>.</returns>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a substantiating document of the shares operation.
        ''' </summary>
        ''' <returns>Corresponds to <see cref="General.SharesOperation.DocumentDate">SharesOperation.DocumentDate</see>,
        ''' <see cref="General.SharesOperation.DocumentName">SharesOperation.DocumentName</see>
        ''' and <see cref="General.SharesOperation.DocumentNumber">SharesOperation.DocumentNumber</see>.</returns>
        Public ReadOnly Property Document() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Document.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an amount of the shares acquired by the shares operation.
        ''' </summary>
        ''' <returns>Corresponds to <see cref="General.SharesAcquisition.Amount">SharesAcquisition.Amount</see>.</returns>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)>
        Public ReadOnly Property AmountAcquired() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_AmountAcquired)
            End Get
        End Property

        ''' <summary>
        ''' Gets an amount of the shares discarded by the shares operation.
        ''' </summary>
        ''' <returns>Corresponds to <see cref="General.SharesDiscard.Amount">SharesDiscard.Amount</see>.</returns>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)>
        Public ReadOnly Property AmountDiscarded() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_AmountDiscarded)
            End Get
        End Property

        ''' <summary>
        ''' Gets an amount of the shares owned after the shares operation.
        ''' </summary>
        ''' <returns>Equals initial amount + <see cref="AmountAcquired">AmountAcquired</see>
        ''' - <see cref="AmountDiscarded">AmountDiscarded</see>.</returns>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)>
        Public ReadOnly Property AmountAfterOperation() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_AmountAfterOperation)
            End Get
        End Property

        ''' <summary>
        ''' Gets a comma separated list of the corresponding accounts affected by the shares operations.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property CorrespondingAccounts() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _CorrespondingAccounts.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets arbitrary remarks regarding the shares operation details.
        ''' </summary>
        ''' <returns>Corresponds to <see cref="General.SharesAcquisition.Remarks">SharesAcquisition.Remarks</see> 
        ''' or <see cref="General.SharesDiscard.Remarks">SharesDiscard.Remarks</see>.</returns>
        Public ReadOnly Property Remarks() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Remarks.Trim
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_ShareHolderInfoList_ToString,
                _Date.ToString("yyyy-MM-dd"), _Document)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetSharesAccountEntry(ByVal dr As DataRow, ByRef initialAmount As Double) As SharesAccountEntry
            Return New SharesAccountEntry(dr, initialAmount)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(dr As DataRow, ByRef initialAmount As Double)
            Fetch(dr, initialAmount)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByRef initialAmount As Double)

            _ID = CIntSafe(dr.Item(0), 0)
            _Date = CDateSafe(dr.Item(1), Today)
            If String.IsNullOrEmpty(CStrSafe(dr.Item(4)).Trim) Then
                _Document = String.Format(My.Resources.ActiveReports_SharesAccountEntry_DocumentFormatWithoutNumber,
                    CDateSafe(dr.Item(2), _Date).ToString("yyyy-MM-dd"), CStrSafe(dr.Item(3)).Trim)
            Else
                _Document = String.Format(My.Resources.ActiveReports_SharesAccountEntry_DocumentFormat,
                    CDateSafe(dr.Item(2), _Date).ToString("yyyy-MM-dd"),
                    CStrSafe(dr.Item(3)).Trim, CStrSafe(dr.Item(4)).Trim)
            End If
            _Remarks = CStrSafe(dr.Item(5)).Trim
            Dim amount As Double = CDblSafe(dr.Item(6), 2, 0)
            If amount > 0 Then
                _AmountAcquired = amount
            Else
                _AmountDiscarded = -amount
            End If
            initialAmount = CRound(initialAmount + amount)
            _AmountAfterOperation = initialAmount
            _CorrespondingAccounts = CStrSafe(dr.Item(7)).Trim

        End Sub

#End Region

    End Class

End Namespace