Imports ApskaitaObjects.My.Resources

Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Represents a collection of payment data for a particular bank account to be exported to some e-bank readable file (ISO20022 or other).
    ''' </summary>
Public Class ExportedBankPayments
        Inherits BusinessBase(Of ExportedBankPayments)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _UniqueID As String = Guid.NewGuid().ToString("N")  
        Private _Account As CashAccountInfo = Nothing
        Private WithEvents _Payments As ExportedBankPaymentList = ExportedBankPaymentList.NewExportedBankPaymentList()
        Private _Subtotal As Double = 0.0


        ''' <summary>
        ''' Gets a unique ID of the exported bank payments set (Guid.NewGuid().ToString("N")).
        ''' </summary>
        ''' <returns>a unique ID of the exported bank payments set (Guid.NewGuid().ToString("N"))</returns>
        Public ReadOnly Property UniqueID() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UniqueID
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a <see cref="Documents.CashAccount">bank account</see> from which the money shall be transferred.
        ''' </summary>
        ''' <remarks>Use <see cref="HelperLists.CashAccountInfoList">CashAccountInfoList</see> as a datasource.</remarks>
        <CashAccountField(ValueRequiredLevel.Mandatory, true, CashAccountType.BankAccount)> _
        Public Property Account() As CashAccountInfo 
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> 
            Get
                Return _Account
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As CashAccountInfo)
                If _Account <> value Then
                    _Account = value 
                    PropertyHasChanged()
                    PropertyHasChanged("ReceiverBankAccount")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets a list of <see cref="ExportedBankPayment">exported (individual) payments</see>. 
        ''' </summary>
        Public ReadOnly Property Payments() As ExportedBankPaymentList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Payments
            End Get
        End Property

        ''' <summary>
        ''' Gets the total amount of payments.
        ''' </summary>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public ReadOnly Property Subtotal() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Subtotal)
            End Get
        End Property



        ''' <summary>
        ''' Returnes TRUE if the object contains some user provided data 
        ''' </summary>
        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            Get
                Return _Payments.IsDirtyEnough
            End Get
        End Property


        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _Payments.IsDirty
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid AndAlso _Payments.IsValid
            End Get
        End Property



        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then result = Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error)
            If Not _Payments.IsValid Then result = AddWithNewLine(result, _
                                                                  _Payments.GetAllBrokenRules, False)
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If MyBase.BrokenRulesCollection.WarningCount > 0 Then
                result = Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning)
            End If
            If _Payments.HasWarnings() Then
                result = AddWithNewLine(result, _Payments.GetAllWarnings, False)
            End If
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return (MyBase.BrokenRulesCollection.WarningCount > 0 OrElse _Payments.HasWarnings())
        End Function


        ''' <summary>
        ''' Merges a new exported payment data list into the export set.
        ''' </summary>
        ''' <param name="list">a list of exported payment data to merge into the set</param>
        public sub Merge(list As ExportedBankPaymentList)
            If list Is Nothing OrElse list.Count < 1 Then Throw new ArgumentNullException("list")
            _Payments.Merge(list)
        End sub


        Private Sub BookEntryItems_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _Payments.ListChanged   
            _Subtotal = _Payments.GetSubtotal()
            PropertyHasChanged("Subtotal")
        End Sub

        ''' <summary>
        ''' Helper method. Takes care of child lists loosing their handlers.
        ''' </summary>
        Protected Overrides Function GetClone() As Object
            Dim result As ExportedBankPayments = DirectCast(MyBase.GetClone(), ExportedBankPayments)
            result.RestoreChildListsHandles()
            Return result
        End Function

        Protected Overrides Sub OnDeserialized(ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.OnDeserialized(context)
            RestoreChildListsHandles()
        End Sub

        Protected Overrides Sub UndoChangesComplete()
            MyBase.UndoChangesComplete()
            RestoreChildListsHandles()
        End Sub

        ''' <summary>
        ''' Helper method. Takes care of child lists loosing its handler. See GetClone method.
        ''' </summary>
        Friend Sub RestoreChildListsHandles()
            Try
                RemoveHandler _Payments.ListChanged, AddressOf BookEntryItems_Changed
            Catch ex As Exception
            End Try
            AddHandler _Payments.ListChanged, AddressOf BookEntryItems_Changed
        End Sub


        Public Overrides Function Save() As ExportedBankPayments
            Throw new NotSupportedException("Exported payment set can only be exported to external storage, not stored in local database.")
        End Function



        Protected Overrides Function GetIdValue() As Object
            Return _UniqueID
        End Function

        Public Overrides Function ToString() As String
            Return Documents_BankDataExchangeProviders_ExportedBankPayments_ToString
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()   
            ValidationRules.AddRule(AddressOf AccountValidation, New Validation.RuleArgs("Account"))
            ValidationRules.AddRule(AddressOf DoubleFieldValidation, New Validation.RuleArgs("Subtotal"))
        End Sub

        ''' <summary>
        ''' Rule ensuring that a valid bank account is specified.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AccountValidation(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As ExportedBankPayments = DirectCast(target, ExportedBankPayments)

            If Not CashAccountFieldValidation(target, e) Then return False

            If Not IsBaseCurrency(valObj._Account.CurrencyCode, GetCurrentCompany().BaseCurrency) Then
                e.Description = Documents_BankDataExchangeProviders_ExportedBankPayments_InvalidAccount 
                e.Severity = Validation.RuleSeverity.Error 
                Return False
            End If

            Return True

        End Function
     
#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new instance of ExportedBankPayments.
        ''' </summary>
        Public Shared Function NewExportedBankPayments() As ExportedBankPayments
            Dim result as New ExportedBankPayments()
            result.ValidationRules.CheckRules()
            Return result
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

End Class

End Namespace