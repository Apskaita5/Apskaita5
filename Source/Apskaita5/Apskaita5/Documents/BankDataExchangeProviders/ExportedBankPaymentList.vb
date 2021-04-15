Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Represents a list of payment data to be exported to some e-bank readable file (ISO20022 or other).
    ''' </summary>
    <Serializable()> _
    Public Class ExportedBankPaymentList
        Inherits BusinessListBase(Of ExportedBankPaymentList, ExportedBankPayment)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim NewItem As ExportedBankPayment = ExportedBankPayment.NewExportedBankPayment()
            Me.Add(NewItem)
            Return NewItem
        End Function

        ''' <summary>
        ''' Returnes TRUE if the object contains some user provided data.
        ''' </summary>
        Public ReadOnly Property IsDirtyEnough() As Boolean Implements IIsDirtyEnough.IsDirtyEnough
            Get
                Return Me.Count > 0
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid
            End Get
        End Property

        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = GetAllBrokenRulesForList(Me)
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = GetAllWarningsForList(Me)
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            For Each i As ExportedBankPayment In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function

        Friend sub Merge(list As ExportedBankPaymentList)
            If list Is Nothing OrElse list.Count < 1 Then Throw new ArgumentNullException("list")
            Me.RaiseListChangedEvents = False
            For Each item As ExportedBankPayment In list
                Me.Add(item)
            Next
            Me.RaiseListChangedEvents = true
            Me.ResetBindings()
        End sub

        Friend Function GetSubtotal() As Double
            Dim result As Double = 0.0
            For Each item As ExportedBankPayment In me
                result = CRound(result + item.Amount)
            Next
            Return result
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Creates a new (empty) list of the payment data to be exported to some e-bank readable format. 
        ''' </summary>
        ''' <returns>a new (empty) list of the payment data to be exported to some e-bank readable format</returns>
        Public Shared Function NewExportedBankPaymentList() As ExportedBankPaymentList
            Return New ExportedBankPaymentList()
        End Function

        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

#End Region

    End Class

End Namespace
