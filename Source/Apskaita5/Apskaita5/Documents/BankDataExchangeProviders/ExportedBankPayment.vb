
Imports ApskaitaObjects.My.Resources

Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Represents a payment data to be exported to some e-bank readable file (ISO20022 or other).
    ''' </summary>
<Serializable()> _
Public Class ExportedBankPayment
    Inherits BusinessBase(Of ExportedBankPayment)
        Implements IGetErrorForListItem

#Region " Business Methods "

    Private _UniqueID As String = Guid.NewGuid().ToString("N")  
    Private _Receiver As PersonInfo = Nothing 
    Private _CustomBankAccount As String = string.Empty
    Private _Amount As Double = 0    
    Private _Description As String = "" 
    Private _PurposeCode As String = "" 
    

    ''' <summary>
    ''' Gets a unique ID of the exported bank payment (Guid.NewGuid().ToString("N")).
    ''' </summary>
    ''' <returns>a unique ID of the exported bank payment (Guid.NewGuid().ToString("N"))</returns>
    Public ReadOnly Property UniqueID() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _UniqueID
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets a <see cref="General.Person">person</see> whom the money shall be transferred.
    ''' </summary>
    ''' <remarks>Use <see cref="HelperLists.PersonInfoList">PersonInfoList</see> as a datasource.</remarks>
    <PersonField(ValueRequiredLevel.Mandatory)> _
    Public Property Receiver() As PersonInfo 
    <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> 
    Get
        Return _Receiver
    End Get
    <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
    Set(ByVal value As PersonInfo)
        If _Receiver <> value Then
            _Receiver = value 
            PropertyHasChanged()
            PropertyHasChanged("ReceiverBankAccount")
        End If
    End Set
    End Property

    ''' <summary>
    ''' Gets a default bank account of the selected receiver (person) if its profile contains one.
    ''' </summary>
    ''' <returns>a default bank account of the selected receiver (person) if its profile contains one</returns>
    Public ReadOnly Property ReceiverBankAccount() As String
    <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
    Get
        If _Receiver <> PersonInfo.Empty() Then Return _Receiver.BankAccount
        Return String.Empty
    End Get
    End Property

    ''' <summary>
    ''' Gets or sets a custom receiver bank account if the payment shall be made to a non default account
    ''' or a default account is not specified in the person profile.
    ''' </summary>
    <StringField(ValueRequiredLevel.Optional, 100)> _
    Public Property CustomBankAccount() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _CustomBankAccount.Trim
        End Get
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Set(ByVal value As String)
            If value is nothing Then value=""
            If _CustomBankAccount.Trim <> value.Trim Then
                _CustomBankAccount = value.Trim
                PropertyHasChanged()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an amount to pay.
    ''' </summary>
    <DoubleField(ValueRequiredLevel.Mandatory, False, ROUNDCURRENCYRATE)> _
    Public Property Amount() As Double
    <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
    Get
        Return CRound(_Amount)
    End Get
    <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
    Set(ByVal value As Double)
        If CRound(_Amount) <> CRound(value) Then
            _Amount = CRound(value)
            PropertyHasChanged()
        End If
    End Set
    End Property

    ''' <summary>
    ''' Gets or sets the (purpose) description of the payment.
    ''' </summary>
    <StringField(ValueRequiredLevel.Optional, 140)> _
    Public Property Description() As String
    <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
    Get
        Return _Description.Trim
    End Get
    <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
    Set(ByVal value As String)
        If value is nothing Then value=""
        If _Description.Trim <> value.Trim Then
            _Description = value.Trim
            PropertyHasChanged()
        End If
    End Set
    End Property

    ''' <summary>
    ''' Gets or sets the purpose code of the payment.
    ''' </summary>
    <StringField(ValueRequiredLevel.Optional, 16)> _
    Public Property PurposeCode() As String
    <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
    Get
        Return _PurposeCode.Trim
    End Get
    <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
    Set(ByVal value As String)
        If value is nothing Then value=""
        If _PurposeCode.Trim <> value.Trim Then
            _PurposeCode = value.Trim
            PropertyHasChanged()
        End If
    End Set
    End Property


    Public Function GetErrorString() As String _
        Implements IGetErrorForListItem.GetErrorString
        If IsValid Then Return ""
        Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString, _
                             vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error))
    End Function

    Public Function GetWarningString() As String _
        Implements IGetErrorForListItem.GetWarningString
        If BrokenRulesCollection.WarningCount < 1 Then Return ""
        Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                             vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning))
    End Function


    Protected Overrides Function GetIdValue() As Object
        Return _UniqueID
    End Function

    Public Overrides Function ToString() As String
        Dim personDescription = Documents_BankDataExchangeProviders_ExportedBankPayment_NullPersonToString
        If _Receiver <> PersonInfo.Empty() Then personDescription = _Receiver.ToString()
        Dim purposeDescription = Documents_BankDataExchangeProviders_ExportedBankPayment_NullPurposeToString
        If Not StringIsNullOrEmpty(_PurposeCode) Then purposeDescription = string.Format(Documents_BankDataExchangeProviders_ExportedBankPayment_PurposeCodeToString, _PurposeCode.Trim())
        If Not StringIsNullOrEmpty(_Description) Then purposeDescription = string.Format(Documents_BankDataExchangeProviders_ExportedBankPayment_PurposeDescriptionToString, _Description.Trim())
        Return String.Format(Documents_BankDataExchangeProviders_ExportedBankPayment_ToString, _Amount, GetCurrentCompany().BaseCurrency.Trim().ToUpper(), 
                             personDescription, purposeDescription)
    End Function

#End Region

#Region " Validation Rules "

    Protected Overrides Sub AddBusinessRules()  
        ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, New Validation.RuleArgs("Amount"))
        ValidationRules.AddRule(AddressOf PersonFieldValidation, New Validation.RuleArgs("Receiver"))
        ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, New Validation.RuleArgs("PurposeCode"))

        ValidationRules.AddRule(AddressOf DescriptionOrCodeValidation, New Validation.RuleArgs("Description"))
        ValidationRules.AddRule(AddressOf CustomBankAccountValidation, New Validation.RuleArgs("CustomBankAccount")) 

        ValidationRules.AddDependantProperty("Description", "PurposeCode", True)
        ValidationRules.AddDependantProperty("ReceiverBankAccount", "CustomBankAccount", False)  
    End Sub

    ''' <summary>
    ''' Rule ensuring that either a purpose description or code is specified yet not both.
    ''' </summary>
    ''' <param name="target">Object containing the data to validate</param>
    ''' <param name="e">Arguments parameter specifying the name of the string
    ''' property to validate</param>
    ''' <returns><see langword="false" /> if the rule is broken</returns>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    Private Shared Function DescriptionOrCodeValidation(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean

        Dim valObj As ExportedBankPayment = DirectCast(target, ExportedBankPayment)

        If StringIsNullOrEmpty(valObj._Description) AndAlso StringIsNullOrEmpty(valObj._PurposeCode) Then
            e.Description = Documents_BankDataExchangeProviders_ExportedBankPayment_PurposeDescriptionAndCodeNull
            e.Severity = Validation.RuleSeverity.Error
            Return False 
        elseIf Not StringIsNullOrEmpty(valObj._Description) AndAlso Not StringIsNullOrEmpty(valObj._PurposeCode) Then
            e.Description = Documents_BankDataExchangeProviders_ExportedBankPayment_PurposeDescriptionAndCodeNotNull
            e.Severity = Validation.RuleSeverity.Error 
            Return False
        End If

        Return CommonValidation.StringFieldValidation(target, e)

    End Function

    ''' <summary>
    ''' Rule ensuring that either a person profile contains a default bank account, or a custom bank account is specified.
    ''' </summary>
    ''' <param name="target">Object containing the data to validate</param>
    ''' <param name="e">Arguments parameter specifying the name of the string
    ''' property to validate</param>
    ''' <returns><see langword="false" /> if the rule is broken</returns>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    Private Shared Function CustomBankAccountValidation(ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean

        Dim valObj As ExportedBankPayment = DirectCast(target, ExportedBankPayment)

        If valObj._Receiver = PersonInfo.Empty() Then Return StringFieldValidation(target, e)

        If StringIsNullOrEmpty(valObj.ReceiverBankAccount) AndAlso StringIsNullOrEmpty(valObj._CustomBankAccount) Then
            e.Description = Documents_BankDataExchangeProviders_ExportedBankPayment_CustomBankAccountRequired 
            e.Severity = Validation.RuleSeverity.Error
            Return False 
        elseIf Not StringIsNullOrEmpty(valObj.ReceiverBankAccount) AndAlso Not StringIsNullOrEmpty(valObj._CustomBankAccount) _
            AndAlso Not valObj.ReceiverBankAccount.Trim().Equals(valObj._CustomBankAccount.Trim(), StringComparison.OrdinalIgnoreCase) Then
            e.Description = Documents_BankDataExchangeProviders_ExportedBankPayment_CustomBankAccountSpecifiedWarning
            e.Severity = Validation.RuleSeverity.Warning
            Return False
        End If

        Return CommonValidation.StringFieldValidation(target, e)

    End Function

#End Region
        
#Region " Factory Methods "

    ''' <summary>
    ''' Creates a new exported payment without any data.
    ''' </summary>
    ''' <returns>a new (empty) exported payment</returns>
    Public Shared Function NewExportedBankPayment() As ExportedBankPayment
        Dim result As New ExportedBankPayment()
        result.ValidationRules.CheckRules()
        Return result
    End Function   

    ''' <summary>
    ''' Creates a new exported payment with the data specified.
    ''' </summary>
    ''' <param name="personId"> an <see cref="General.Person.Id">Id of the person</see> to make the payment to.</param>
    ''' <param name="amount">an amount of the payment</param>
    ''' <param name="purposeDescription">a purpose (description) of the payment</param>
    ''' <param name="personLookup">a lookup list to use when resolving a person by its id</param>
    ''' <returns>a new exported payment</returns>
    Public Shared Function NewExportedBankPayment(personId As Integer, amount As Double, purposeDescription As String, 
        personLookup As PersonInfoList) As ExportedBankPayment
        If personLookup Is Nothing Then Throw new ArgumentNullException("personLookup")
        Return new ExportedBankPayment(personId, amount, purposeDescription, personLookup)
    End Function

    ''' <summary>
    ''' Creates a new exported payment with the data specified.
    ''' </summary>
    ''' <param name="person"> a person to make the payment to</param>
    ''' <param name="amount">an amount of the payment</param>
    ''' <param name="purposeDescription">a purpose (description) of the payment</param>
    ''' <returns>a new exported payment</returns>
    Public Shared Function NewExportedBankPayment(person As PersonInfo, amount As Double, purposeDescription As String) As ExportedBankPayment
        Return new ExportedBankPayment(person, amount, purposeDescription)
    End Function


    Private Sub New()
    ' require use of factory methods
    MarkAsChild()
    End Sub

    Private Sub New(personId As Integer, amount As Double, purposeDescription As String, personLookup As PersonInfoList)
        Create(personId, amount, purposeDescription, personLookup)
        MarkAsChild()
    End Sub 

    Private Sub New(person As PersonInfo, amount As Double, purposeDescription As String)
        Create(person, amount, purposeDescription)
        MarkAsChild()
    End Sub


    Private sub Create(personId As Integer, amount As Double, purposeDescription As String, personLookup As PersonInfoList)
        _Amount = CRound(amount)
        _Description = purposeDescription
        If personId > 0
            _Receiver = personLookup.GetPersonInfo(personId)
        End If
        ValidationRules.CheckRules()
    End sub

    Private sub Create(person As PersonInfo, amount As Double, purposeDescription As String)
        _Amount = CRound(amount)
        _Description = purposeDescription
        _Receiver = person
        ValidationRules.CheckRules()
    End sub

#End Region  

End Class

End Namespace
