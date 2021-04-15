Imports Csla.Validation
Imports ApskaitaObjects.My.Resources

Namespace Workers

    ''' <summary>
    ''' Represents a payment to a natural person when no taxes are deducted or payed by the company. 
    ''' Used in tax declarations.
    ''' </summary>
    ''' <remarks>Provides additional info on top of a <see cref="General.JournalEntry">JournalEntry</see>.
    ''' Values are stored in the database table PayOutNaturalPersonWithoutTaxes.</remarks>
    <Serializable()> _
    Public NotInheritable Class PayOutNaturalPersonWithoutTaxes
        Inherits BusinessBase(Of PayOutNaturalPersonWithoutTaxes)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ID As Integer = 0
        Private _JournalEntryID As Integer = 0
        Private _JournalEntryDate As Date = Today
        Private _JournalEntryDocNo As String = ""
        Private _JournalEntryDocType As DocumentType = DocumentType.None
        Private _JournalEntryDocTypeHumanReadable As String = ""
        Private _JournalEntryContent As String = ""
        Private _JournalEntryPersonID As Integer = 0
        Private _JournalEntryPersonName As String = ""
        Private _JournalEntryPersonCode As String = ""
        Private _BookEntries As String = ""
        Private _JournalEntryAmount As Double = 0
        Private _PaymentAmount As Double = 0
        Private _PaymentReceiver As PersonInfo = Nothing
        Private _TaxCode As Integer = 0
        Private _TaxPayedOutDate As SmartDate = New SmartDate(False)
        Private _TaxDeductedAndPayed As Double = 0
        Private _TaxPayedByCompany As Double = 0
        Private _Remarks As String = ""


        ''' <summary>
        ''' Gets an ID of the payment info that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field PayOutNaturalPersonWithoutTaxes.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the <see cref="General.JournalEntry.ID">general ledger entry ID</see> for the payment info.
        ''' </summary>
        ''' <remarks>Value is stored in the database field PayOutNaturalPersonWithoutTaxes.JournalEntryID.</remarks>
        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date of the payment.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Date">JournalEntry.Date</see>.</remarks>
        Public ReadOnly Property JournalEntryDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the document number of the payment.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.DocNumber">JournalEntry.DocNumber</see>.</remarks>
        Public ReadOnly Property JournalEntryDocNo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDocNo.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="DocumentType">DocumentType</see> of the document associated with the Journal Entry (as enum).
        ''' </summary>
        ''' <remarks>Value is stored in the database field bz.Op_dok_rusis.</remarks>
        Public ReadOnly Property JournalEntryDocType() As DocumentType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDocType
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="DocumentType">DocumentType</see> of the document associated with the Journal Entry 
        ''' (as a human readable string).
        ''' </summary>
        ''' <remarks>Value is stored in the database field bz.Op_dok_rusis.</remarks>
        Public ReadOnly Property JournalEntryDocTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDocTypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the description of the payment.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Content">JournalEntry.Content</see>.</remarks>
        Public ReadOnly Property JournalEntryContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryContent.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the payment receiver ID.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Person">JournalEntry.Person.ID</see>.</remarks>
        Public ReadOnly Property JournalEntryPersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryPersonID
            End Get
        End Property

        ''' <summary>
        ''' Gets the payment receiver name.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Person">JournalEntry.Person.Name</see>.</remarks>
        Public ReadOnly Property JournalEntryPersonName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryPersonName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the payment receiver code.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Person">JournalEntry.Person.Code</see>.</remarks>
        Public ReadOnly Property JournalEntryPersonCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryPersonCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets human readable description of book entries made by the Journal Entry.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ActiveReports.JournalEntryInfo.BookEntries">JournalEntryInfo.BookEntries</see>.</remarks>
        Public ReadOnly Property BookEntries() As String
            Get
                Return _BookEntries
            End Get
        End Property

        ''' <summary>
        ''' Gets the total sum of the journal entry.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ActiveReports.JournalEntryInfo.Ammount">JournalEntryInfo.Ammount</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property JournalEntryAmount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_JournalEntryAmount)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a total payment amount. Could be different from the journal entry amount
        ''' due to the currency exchange rate effects.
        ''' </summary>
        ''' <remarks>Value is stored in the database field PayOutNaturalPersonWithoutTaxes.PaymentAmount.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public Property PaymentAmount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PaymentAmount)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_PaymentAmount) <> CRound(value) Then
                    _PaymentAmount = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a payment receiver. Could be different from the journal entry person.
        ''' </summary>
        ''' <remarks>Value is stored in the database field PayOutNaturalPersonWithoutTaxes.PersonID.</remarks>
        <PersonField(ValueRequiredLevel.Mandatory, False, True, True)> _
        Public Property PaymentReceiver() As PersonInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PaymentReceiver
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As PersonInfo)
                CanWriteProperty(True)
                If value <> _PaymentReceiver Then
                    _PaymentReceiver = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the code of the payment according to the state tax inspectorate (VMI) clasification.
        ''' </summary>
        ''' <remarks>Value is stored in the database field PayOutNaturalPersonWithoutTaxes.TaxCode.</remarks>
        <CodeField(ValueRequiredLevel.Recommended, ApskaitaObjects.Settings.CodeType.GpmDeclaration)> _
        Public Property TaxCode() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TaxCode
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If _TaxCode <> value Then
                    _TaxCode = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a date that either a TaxDeductedAndPayed or TaxPayedByCompany was payed 
        ''' to the state. Null if no tax was payed by the company.
        ''' </summary>
        ''' <remarks>Value is stored in the database field PayOutNaturalPersonWithoutTaxes.TaxPayedOutDate.</remarks>
        <StringField(ValueRequiredLevel.Optional, 20)>
        Public Property TaxPayedOutDate As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _TaxPayedOutDate.ToString("yyyy-MM-dd")
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                Dim dateValue As SmartDate = New Csla.SmartDate(value.Trim, False)
                If _TaxPayedOutDate <> dateValue Then
                    _TaxPayedOutDate = dateValue
                    PropertyHasChanged()
                    PropertyHasChanged("TaxDeductedAndPayed")
                    PropertyHasChanged("TaxPayedByCompany")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets an amount of taxes deducted from the payment due and payed by the company.
        ''' due to the currency exchange rate effects.
        ''' </summary>
        ''' <remarks>Value is stored in the database field PayOutNaturalPersonWithoutTaxes.TaxDeductedAndPayed.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)>
        Public Property TaxDeductedAndPayed As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_TaxDeductedAndPayed)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_TaxDeductedAndPayed) <> CRound(value) Then
                    _TaxDeductedAndPayed = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets an amount of taxes payed by the company on it's own account.
        ''' </summary>
        ''' <remarks>Value is stored in the database field PayOutNaturalPersonWithoutTaxes.TaxPayedByCompany.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)>
        Public Property TaxPayedByCompany As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_TaxPayedByCompany)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_TaxPayedByCompany) <> CRound(value) Then
                    _TaxPayedByCompany = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets accountant remarks regarding the payment.
        ''' </summary>
        ''' <remarks>Value is stored in the database field PayOutNaturalPersonWithoutTaxes.Remarks.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255, False)> _
        Public Property Remarks() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Remarks.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Remarks.Trim <> value.Trim Then
                    _Remarks = value.Trim
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
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(Workers_PayOutNaturalPersonWithoutTaxes_ToString, _
                _JournalEntryDate.ToString("yyyy-MM-dd"), _JournalEntryPersonName)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CodeFieldValidation, New RuleArgs("TaxCode"))
            ValidationRules.AddRule(AddressOf StringFieldValidation, New RuleArgs("Remarks"))

            ValidationRules.AddRule(AddressOf PaymentAmountValidation, New RuleArgs("PaymentAmount"))
            ValidationRules.AddRule(AddressOf PaymentReceiverValidation, New RuleArgs("PaymentReceiver"))
            ValidationRules.AddRule(AddressOf TaxDeductedAndPayedValidation, New RuleArgs("TaxDeductedAndPayed"))
            ValidationRules.AddRule(AddressOf TaxPayedByCompanyValidation, New RuleArgs("TaxPayedByCompany"))

            ValidationRules.AddDependantProperty("TaxPayedOutDate", "TaxDeductedAndPayed", False)
            ValidationRules.AddDependantProperty("TaxPayedOutDate", "TaxPayedByCompany", False)
            ValidationRules.AddDependantProperty("TaxDeductedAndPayed", "TaxPayedByCompany", True)

        End Sub

        ''' <summary>
        ''' Rule ensuring that the payment amount is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function PaymentAmountValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            If Not DoubleFieldValidation(target, e) Then Return False

            Dim valObj As PayOutNaturalPersonWithoutTaxes = DirectCast(target, PayOutNaturalPersonWithoutTaxes)

            If valObj.JournalEntryAmount <> valObj.PaymentAmount Then
                e.Description = Workers_PayOutNaturalPersonWithoutTaxes_PaymentAmountMismatch
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the payment receiver (person) is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")>
        Private Shared Function PaymentReceiverValidation(ByVal target As Object,
            ByVal e As Validation.RuleArgs) As Boolean

            If Not PersonFieldValidation(target, e) Then Return False

            Dim valObj As PayOutNaturalPersonWithoutTaxes = DirectCast(target, PayOutNaturalPersonWithoutTaxes)

            If valObj._JournalEntryPersonID > 0 AndAlso valObj._JournalEntryPersonID <> valObj._PaymentReceiver.ID Then
                e.Description = Workers_PayOutNaturalPersonWithoutTaxes_PaymentReceiverMismatch
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the TaxDeductedAndPayed is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")>
        Private Shared Function TaxDeductedAndPayedValidation(ByVal target As Object,
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As PayOutNaturalPersonWithoutTaxes = DirectCast(target, PayOutNaturalPersonWithoutTaxes)

            If valObj._TaxPayedOutDate.IsEmpty AndAlso valObj._TaxDeductedAndPayed > 0 Then
                e.Description = Workers_PayOutNaturalPersonWithoutTaxes_TaxPayedAmountInvalid
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            ElseIf Not valObj._TaxPayedOutDate.IsEmpty AndAlso Not valObj._TaxDeductedAndPayed > 0.0 _
                AndAlso Not valObj._TaxPayedByCompany > 0.0 Then
                e.Description = Workers_PayOutNaturalPersonWithoutTaxes_TaxPayedAmountNull
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the TaxPayedByCompany is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")>
        Private Shared Function TaxPayedByCompanyValidation(ByVal target As Object,
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As PayOutNaturalPersonWithoutTaxes = DirectCast(target, PayOutNaturalPersonWithoutTaxes)

            If valObj._TaxPayedOutDate.IsEmpty AndAlso valObj._TaxPayedByCompany > 0 Then
                e.Description = Workers_PayOutNaturalPersonWithoutTaxes_TaxPayedAmountInvalid
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            ElseIf Not valObj._TaxPayedOutDate.IsEmpty AndAlso Not valObj._TaxDeductedAndPayed > 0.0 _
                AndAlso Not valObj._TaxPayedByCompany > 0.0 Then
                e.Description = Workers_PayOutNaturalPersonWithoutTaxes_TaxPayedAmountNull
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            End If

            Return True

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function NewPayOutNaturalPersonWithoutTaxes(ByVal journalEntryID As Integer) As PayOutNaturalPersonWithoutTaxes

            Dim myComm As New SQLCommand("CreatePayOutNaturalPersonWithoutTaxes")
            myComm.AddParam("?CD", journalEntryID)

            Using myData As DataTable = myComm.Fetch()
                If myData.Rows.Count < 1 Then
                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                        My.Resources.General_JournalEntry_TypeName, journalEntryID.ToString))
                End If
                Return New PayOutNaturalPersonWithoutTaxes(myData.Rows(0))
            End Using

        End Function

        Friend Shared Function GetPayOutNaturalPersonWithoutTaxes(ByVal dr As DataRow) As PayOutNaturalPersonWithoutTaxes
            Return New PayOutNaturalPersonWithoutTaxes(dr)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal dr As DataRow)
            MarkAsChild()
            If CIntSafe(dr.Item(0), 0) > 0 Then
                Fetch(dr)
            Else
                Create(dr)
            End If
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal dr As DataRow)

            _JournalEntryID = CIntSafe(dr.Item(1), 0)
            _JournalEntryDate = CDateSafe(dr.Item(2), Today)
            _JournalEntryDocNo = CStrSafe(dr.Item(3)).Trim
            _JournalEntryContent = CStrSafe(dr.Item(4)).Trim
            _JournalEntryDocType = ConvertDatabaseCharID(Of DocumentType)(CStrSafe(dr.Item(5)).Trim)
            _JournalEntryDocTypeHumanReadable = ConvertLocalizedName(_JournalEntryDocType)
            _BookEntries = CStrSafe(dr.Item(6)).Trim
            _PaymentAmount = CDblSafe(dr.Item(7), 2, 0)
            _TaxCode = CIntSafe(dr.Item(8), 0)
            If CDateSafe(dr.Item(9), Date.MaxValue) <> Date.MaxValue Then
                _TaxPayedOutDate = New Csla.SmartDate(CDateSafe(dr.Item(9), Date.MinValue), False)
            End If
            _TaxDeductedAndPayed = CDblSafe(dr.Item(10), 2, 0)
            _TaxPayedByCompany = CDblSafe(dr.Item(11), 2, 0)
            _Remarks = CStrSafe(dr.Item(12)).Trim
            _JournalEntryAmount = CDblSafe(dr.Item(13), 2, 0)

            Dim journalEntryPerson As PersonInfo = PersonInfo.GetPersonInfo(dr, 14)
            If Not journalEntryPerson.IsEmpty Then
                _JournalEntryPersonID = journalEntryPerson.ID
                _JournalEntryPersonName = journalEntryPerson.Name
                _JournalEntryPersonCode = JournalEntryPersonCode
                _PaymentReceiver = journalEntryPerson
            End If

            _PaymentAmount = _JournalEntryAmount

            MarkNew()

            ValidationRules.CheckRules()

        End Sub

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(0), 0)
            _JournalEntryID = CIntSafe(dr.Item(1), 0)
            _JournalEntryDate = CDateSafe(dr.Item(2), Today)
            _JournalEntryDocNo = CStrSafe(dr.Item(3)).Trim
            _JournalEntryContent = CStrSafe(dr.Item(4)).Trim
            _JournalEntryDocType = ConvertDatabaseCharID(Of DocumentType)(CStrSafe(dr.Item(5)).Trim)
            _JournalEntryDocTypeHumanReadable = ConvertLocalizedName(_JournalEntryDocType)
            _BookEntries = CStrSafe(dr.Item(6)).Trim
            _PaymentAmount = CDblSafe(dr.Item(7), 2, 0)
            _TaxCode = CIntSafe(dr.Item(8), 0)
            If CDateSafe(dr.Item(9), Date.MaxValue) <> Date.MaxValue Then
                _TaxPayedOutDate = New Csla.SmartDate(CDateSafe(dr.Item(9), Date.MinValue), False)
            End If
            _TaxDeductedAndPayed = CDblSafe(dr.Item(10), 2, 0)
            _TaxPayedByCompany = CDblSafe(dr.Item(11), 2, 0)
            _Remarks = CStrSafe(dr.Item(12)).Trim
            _JournalEntryAmount = CDblSafe(dr.Item(13), 2, 0)

            Dim journalEntryPerson As PersonInfo = PersonInfo.GetPersonInfo(dr, 14)
            If Not journalEntryPerson.IsEmpty Then
                _JournalEntryPersonID = journalEntryPerson.ID
                _JournalEntryPersonName = journalEntryPerson.Name
                _JournalEntryPersonCode = JournalEntryPersonCode
            End If

            _PaymentReceiver = PersonInfo.GetPersonInfo(dr, 34)

            MarkOld()

            ValidationRules.CheckRules()

        End Sub

        Friend Sub Insert()

            Dim myComm As New SQLCommand("InsertPayOutNaturalPersonWithoutTaxes")
            AddWithParams(myComm)
            myComm.AddParam("?AA", _JournalEntryID)

            myComm.Execute()

            _ID = Convert.ToInt32(myComm.LastInsertID)

            MarkOld()

        End Sub

        Friend Sub Update()

            Dim myComm As New SQLCommand("UpdatePayOutNaturalPersonWithoutTaxes")
            myComm.AddParam("?CD", _ID)
            AddWithParams(myComm)

            myComm.Execute()

            MarkOld()

        End Sub

        Friend Sub DeleteSelf()

            Dim myComm As New SQLCommand("DeletePayOutNaturalPersonWithoutTaxes")
            myComm.AddParam("?CD", _ID)

            myComm.Execute()

            MarkNew()

        End Sub

        Private Sub AddWithParams(ByRef myComm As SQLCommand)

            If _PaymentReceiver.IsEmpty Then
                myComm.AddParam("?AB", 0)
            Else
                myComm.AddParam("?AB", _PaymentReceiver.ID)
            End If

            myComm.AddParam("?AC", CRound(_PaymentAmount))
            myComm.AddParam("?AD", _TaxCode)
            myComm.AddParam("?AE", _Remarks.Trim)

            If _TaxPayedOutDate.IsEmpty Then
                myComm.AddParam("?AF", Nothing, GetType(Date))
                myComm.AddParam("?AG", 0.0)
                myComm.AddParam("?AH", 0.0)
            Else
                myComm.AddParam("?AF", _TaxPayedOutDate.Date)
                myComm.AddParam("?AG", CRound(_TaxDeductedAndPayed))
                myComm.AddParam("?AH", CRound(_TaxPayedByCompany))
            End If

        End Sub

#End Region

    End Class

End Namespace
