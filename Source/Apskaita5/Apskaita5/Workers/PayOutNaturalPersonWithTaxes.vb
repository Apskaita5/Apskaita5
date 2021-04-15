Imports ApskaitaObjects.My.Resources
Imports Csla.Validation

Namespace Workers

    ''' <summary>
    ''' Represent a payment to a natural person when some taxes are deducted or payed by the company. 
    ''' Used in tax declarations.
    ''' </summary>
    ''' <remarks>Provides additional info on top of a <see cref="General.JournalEntry">JournalEntry</see>.
    ''' Values are stored in the database table d_kitos.</remarks>
    <Serializable()>
    Public NotInheritable Class PayOutNaturalPersonWithTaxes
        Inherits BusinessBase(Of PayOutNaturalPersonWithTaxes)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _JournalEntryID As Integer = 0
        Private _Date As Date = Today
        Private _Content As String = ""
        Private _DocNumber As String = ""
        Private _PersonInfo As String = ""
        Private _PersonCodeSODRA As String = ""
        Private _SumJournalEntry As Double = 0
        Private _SumNeto As Double = 0
        Private _SumBruto As Double = 0
        Private _RateGPM As Double = 0
        Private _RatePSDForPerson As Double = 0
        Private _RatePSDForCompany As Double = 0
        Private _SODRABase As Integer = 100
        Private _RateSODRAForPerson As Double = 0
        Private _RateSODRAForCompany As Double = 0
        Private _CodeVMI As Integer = 0
        Private _CodeSODRA As Integer = 0
        Private _DeductionGPM As Double = 0
        Private _DeductionPSD As Double = 0
        Private _DeductionSODRA As Double = 0
        Private _ContributionPSD As Double = 0
        Private _ContributionSODRA As Double = 0
        Private _TotalPSD As Double = 0
        Private _TotalSODRA As Double = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now


        ''' <summary>
        ''' Gets an ID of the payment info that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the payment info was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.InsertDate.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the payment info was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.UpdateDate.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the <see cref="General.JournalEntry.ID">general ledger entry ID</see> for the payment info.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.BZ_ID.</remarks>
        <IntegerField(ValueRequiredLevel.Mandatory, False)>
        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date of the payment.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Date">JournalEntry.Date</see>.</remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets the description of the payment.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Content">JournalEntry.Content</see>.</remarks>
        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Content.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the document number of the payment.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.DocNumber">JournalEntry.DocNumber</see>.</remarks>
        Public ReadOnly Property DocNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DocNumber.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the payment receiver name.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ActiveReports.JournalEntryInfo.Person">JournalEntryInfo.Person</see>.</remarks>
        Public ReadOnly Property PersonInfo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _PersonInfo.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the payment receiver social security (SODRA) code.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.KodasSDA.</remarks>
        <StringField(ValueRequiredLevel.Optional, 15, False)>
        Public Property PersonCodeSODRA() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _PersonCodeSODRA.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _PersonCodeSODRA.Trim <> value.Trim Then
                    _PersonCodeSODRA = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets the total sum of the journal entry.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)>
        Public ReadOnly Property SumJournalEntry() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_SumJournalEntry)
            End Get
        End Property

        ''' <summary>
        ''' Gets the total sum that was actualy payed to the receiver (i.e. excluding all the taxes).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.Suma.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)>
        Public Property SumNeto() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_SumNeto)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_SumNeto) <> CRound(value) Then
                    _SumNeto = CRound(value)
                    PropertyHasChanged()
                    RecalculateAllTaxes(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the contractual sum of the payment (i.e. <see cref="SumNeto">SumNeto</see> plus the deducted taxes excluding taxes that are payed at the company's cost).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.SumaBruto.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)>
        Public ReadOnly Property SumBruto() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_SumBruto)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the personal income tax (GPM) rate.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.Tar.</remarks>
        <TaxRateField(ValueRequiredLevel.Recommended, ApskaitaObjects.Settings.TaxRateType.GPM)>
        Public Property RateGPM() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_RateGPM)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_RateGPM) <> CRound(value) Then
                    _RateGPM = CRound(value)
                    PropertyHasChanged()
                    RecalculateAllTaxes(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the health insurance contribution (PSD) rate for the receiver (deducted from the contractual sum).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.TarPSDW.</remarks>
        <TaxRateField(ValueRequiredLevel.Recommended, ApskaitaObjects.Settings.TaxRateType.PSDForPerson)>
        Public Property RatePSDForPerson() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_RatePSDForPerson)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_RatePSDForPerson) <> CRound(value) Then
                    _RatePSDForPerson = CRound(value)
                    PropertyHasChanged()
                    RecalculateAllTaxes(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the health insurance contribution (PSD) rate for the company (at the company's expense additionaly to the contractual sum).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.TarPSDE.</remarks>
        <TaxRateField(ValueRequiredLevel.Recommended, ApskaitaObjects.Settings.TaxRateType.PSDForCompany)>
        Public Property RatePSDForCompany() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_RatePSDForCompany)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_RatePSDForCompany) <> CRound(value) Then
                    _RatePSDForCompany = CRound(value)
                    PropertyHasChanged()
                    _ContributionPSD = CRound(_SumBruto * _SODRABase * _RatePSDForCompany / 100 / 100)
                    _TotalPSD = CRound(_DeductionPSD + _ContributionPSD)
                    PropertyHasChanged("ContributionPSD")
                    PropertyHasChanged("TotalPSD")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the percentage of the contractual sum that is taxable by the social security contributions (SODRA) rates.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.BaseSODRA.</remarks>
        <IntegerField(ValueRequiredLevel.Recommended, False, True, 1, 100, False)>
        Public Property SODRABase() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _SODRABase
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If _SODRABase <> value Then
                    _SODRABase = value
                    PropertyHasChanged()
                    RecalculateAllTaxes(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the social security contribution (SODRA) rate for the receiver (deducted from the contractual sum).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.TarSDW.</remarks>
        <TaxRateField(ValueRequiredLevel.Recommended, ApskaitaObjects.Settings.TaxRateType.SodraForPerson)>
        Public Property RateSODRAForPerson() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_RateSODRAForPerson)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_RateSODRAForPerson) <> CRound(value) Then
                    _RateSODRAForPerson = CRound(value)
                    PropertyHasChanged()
                    RecalculateAllTaxes(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the social security contribution (SODRA) rate for the company (at the company's expense additionaly to the contractual sum).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.TarSDE.</remarks>
        <TaxRateField(ValueRequiredLevel.Recommended, ApskaitaObjects.Settings.TaxRateType.SodraForCompany)>
        Public Property RateSODRAForCompany() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_RateSODRAForCompany)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_RateSODRAForCompany) <> CRound(value) Then
                    _RateSODRAForCompany = CRound(value)
                    PropertyHasChanged()
                    _ContributionSODRA = CRound(_SumBruto * _SODRABase * _RateSODRAForCompany / 100 / 100)
                    _TotalSODRA = CRound(_DeductionSODRA + _ContributionSODRA)
                    PropertyHasChanged("ContributionSODRA")
                    PropertyHasChanged("TotalSODRA")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the code of the payment according to the state tax inspectorate (VMI) clasification.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.Kodas.</remarks>
        <CodeField(ValueRequiredLevel.Recommended, ApskaitaObjects.Settings.CodeType.GpmDeclaration)>
        Public Property CodeVMI() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _CodeVMI
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If _CodeVMI <> value Then
                    _CodeVMI = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the code of the payment according to the social security (SODRA) clasification.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.KodasSD.</remarks>
        <CodeField(ValueRequiredLevel.Recommended, ApskaitaObjects.Settings.CodeType.SodraDeclaration)>
        Public Property CodeSODRA() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _CodeSODRA
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If _CodeSODRA <> value Then
                    _CodeSODRA = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets total amount of personal income tax (GPM) deducted from the contractual sum.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)>
        Public ReadOnly Property DeductionGPM() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_DeductionGPM)
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of health insurance contributions deducted from the contractual sum.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)>
        Public ReadOnly Property DeductionPSD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_DeductionPSD)
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of social security contributions (SODRA) deducted from the contractual sum.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)>
        Public ReadOnly Property DeductionSODRA() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_DeductionSODRA)
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of health insurance contributions at the company's expense additionaly to the contractual sum.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)>
        Public ReadOnly Property ContributionPSD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_ContributionPSD)
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of social security contributions (SODRA) at the company's expense additionaly to the contractual sum.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)>
        Public ReadOnly Property ContributionSODRA() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_ContributionSODRA)
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of health insurance contributions.
        ''' </summary>
        ''' <remarks><see cref="DeductionPSD">DeductionPSD</see> plus <see cref="ContributionPSD">ContributionPSD</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)>
        Public ReadOnly Property TotalPSD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_TotalPSD)
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of social security contributions (SODRA) deducted from the contractual sum.
        ''' </summary>
        ''' <remarks><see cref="DeductionSODRA">DeductionSODRA</see> plus <see cref="ContributionSODRA">ContributionSODRA</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)>
        Public ReadOnly Property TotalSODRA() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_TotalSODRA)
            End Get
        End Property



        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            If IsValid Then Return ""
            Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString,
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error))
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If BrokenRulesCollection.WarningCount < 1 Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString,
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning))
        End Function



        Private Sub RecalculateAllTaxes(ByVal raisePropertyHasChanged As Boolean)

            _SumBruto = CRound(CRound(_SumNeto) / CRound(1 - CRound(_RateGPM / 100, 4) _
                - CRound(_RatePSDForPerson * _SODRABase / 10000, 4) _
                - CRound(_RateSODRAForPerson * _SODRABase / 10000, 4), 4))

            RecalculateAllTaxesByBrutoValue(raisePropertyHasChanged)

            If raisePropertyHasChanged Then
                PropertyHasChanged("SumBruto")
            End If

        End Sub

        Private Sub RecalculateAllTaxesByBrutoValue(ByVal raisePropertyHasChanged As Boolean)

            _DeductionGPM = CRound(_SumBruto * _RateGPM / 100)
            _DeductionPSD = CRound(_SumBruto * _SODRABase * _RatePSDForPerson / 100 / 100)
            _DeductionSODRA = CRound(_SumBruto * _SODRABase * _RateSODRAForPerson / 100 / 100)

            ' Add rounding error to either SODRA, GPM or PSD deductions
            Dim diff As Double = CRound(_SumBruto - _SumNeto - _DeductionGPM - _DeductionPSD - _DeductionSODRA)

            If Not CRound(_DeductionGPM) > 0 AndAlso Not CRound(_DeductionPSD) > 0 AndAlso Not CRound(_DeductionSODRA) > 0 Then
                _SumBruto = _SumNeto
            ElseIf CRound(_DeductionSODRA) > 0 Then
                _DeductionSODRA = CRound(_DeductionSODRA + diff)
            ElseIf CRound(_DeductionGPM) > 0 Then
                _DeductionGPM = CRound(_DeductionGPM + diff)
            Else
                _DeductionPSD = CRound(_DeductionPSD + diff)
            End If

            _ContributionPSD = CRound(_SumBruto * _SODRABase * _RatePSDForCompany / 100 / 100)
            _ContributionSODRA = CRound(_SumBruto * _SODRABase * _RateSODRAForCompany / 100 / 100)

            _TotalPSD = CRound(_DeductionPSD + _ContributionPSD)
            _TotalSODRA = CRound(_DeductionSODRA + _ContributionSODRA)

            If raisePropertyHasChanged Then
                PropertyHasChanged("DeductionGPM")
                PropertyHasChanged("DeductionPSD")
                PropertyHasChanged("DeductionSODRA")
                PropertyHasChanged("ContributionPSD")
                PropertyHasChanged("ContributionSODRA")
                PropertyHasChanged("TotalPSD")
                PropertyHasChanged("TotalSODRA")
            End If

        End Sub


        ''' <summary>
        ''' Generates a new <see cref="General.JournalEntry">JournalEntry</see> that is used to account for tax obligations.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetNewJournalEntry() As General.JournalEntry

            Dim result As General.JournalEntry = General.JournalEntry.NewJournalEntry

            result.Content = My.Resources.Workers_PayOutNaturalPerson_NewJournalEntryContent
            result.Date = Today
            result.DocNumber = My.Resources.Workers_PayOutNaturalPerson_NewJournalEntryDocNumber
            result.Person = Nothing

            Dim costsEntry As General.BookEntry = General.BookEntry.NewBookEntry
            costsEntry.Amount = CRound(_SumBruto + _ContributionPSD + _ContributionSODRA)

            result.DebetList.Add(costsEntry)

            Dim cc As Settings.CompanyInfo = GetCurrentCompany()

            Dim netoEntry As General.BookEntry = General.BookEntry.NewBookEntry
            netoEntry.Account = cc.GetDefaultAccount(General.DefaultAccountType.Suppliers)
            netoEntry.Amount = CRound(_SumBruto - _DeductionPSD - _DeductionSODRA - _DeductionGPM)

            result.CreditList.Add(netoEntry)

            Dim gpmEntry As General.BookEntry = General.BookEntry.NewBookEntry
            gpmEntry.Account = cc.GetDefaultAccount(General.DefaultAccountType.OtherGpmPayable)
            gpmEntry.Amount = CRound(_DeductionGPM)

            result.CreditList.Add(gpmEntry)

            Dim sodraEntry As General.BookEntry = General.BookEntry.NewBookEntry
            sodraEntry.Account = cc.GetDefaultAccount(General.DefaultAccountType.OtherSodraPayable)
            sodraEntry.Amount = CRound(_TotalSODRA)

            result.CreditList.Add(sodraEntry)

            Dim psdEntry As General.BookEntry = General.BookEntry.NewBookEntry
            psdEntry.Account = cc.GetDefaultAccount(General.DefaultAccountType.OtherPsdPayable)
            psdEntry.Amount = CRound(_TotalPSD)

            result.CreditList.Add(psdEntry)

            Return result

        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Workers_PayOutNaturalPersonWithTaxes_ToString,
                _Date.ToString("yyyy-MM-dd"), _PersonInfo, _Content)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf DoubleFieldValidation, New RuleArgs("SumNeto"))
            ValidationRules.AddRule(AddressOf DoubleFieldValidation, New RuleArgs("SumBruto"))
            ValidationRules.AddRule(AddressOf DoubleFieldValidation, New RuleArgs("RateGPM"))
            ValidationRules.AddRule(AddressOf DoubleFieldValidation, New RuleArgs("RatePSDForPerson"))
            ValidationRules.AddRule(AddressOf DoubleFieldValidation, New RuleArgs("RatePSDForCompany"))
            ValidationRules.AddRule(AddressOf DoubleFieldValidation, New RuleArgs("RateSODRAForPerson"))
            ValidationRules.AddRule(AddressOf DoubleFieldValidation, New RuleArgs("RateSODRAForCompany"))

            ValidationRules.AddRule(AddressOf IntegerFieldValidation, New RuleArgs("SODRABase"))
            ValidationRules.AddRule(AddressOf CodeFieldValidation, New RuleArgs("CodeVMI"))
            ValidationRules.AddRule(AddressOf CodeFieldValidation, New RuleArgs("CodeSODRA"))
            ValidationRules.AddRule(AddressOf IntegerFieldValidation, New RuleArgs("JournalEntryID"))

            ValidationRules.AddRule(AddressOf PersonSODRACodeValidation, "PersonCodeSODRA")

            ValidationRules.AddDependantProperty("CodeSODRA", "PersonCodeSODRA", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring persons SODRA code is entered if payout code for SODRA is set.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")>
        Private Shared Function PersonSODRACodeValidation(ByVal target As Object,
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As PayOutNaturalPersonWithTaxes = DirectCast(target, PayOutNaturalPersonWithTaxes)

            If valObj._CodeSODRA > 0 AndAlso String.IsNullOrEmpty(valObj._PersonCodeSODRA.Trim) Then
                e.Description = My.Resources.Workers_PayOutNaturalPersonWithTaxes_CodeSODRANull
                e.Severity = Validation.RuleSeverity.Error
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

        Friend Shared Function NewPayOutNaturalPersonWithTaxes(ByVal journalEntryID As Integer) As PayOutNaturalPersonWithTaxes

            Dim myComm As New SQLCommand("CreatePayOutNaturalPersonWithTaxes")
            myComm.AddParam("?ND", journalEntryID)

            Using myData As DataTable = myComm.Fetch()
                If myData.Rows.Count < 1 Then
                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound,
                        My.Resources.General_JournalEntry_TypeName, journalEntryID.ToString))
                End If
                Return New PayOutNaturalPersonWithTaxes(myData.Rows(0))
            End Using

        End Function

        Friend Shared Function GetPayOutNaturalPersonWithTaxes(ByVal dr As DataRow) As PayOutNaturalPersonWithTaxes
            Return New PayOutNaturalPersonWithTaxes(dr)
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
            _Date = CDateSafe(dr.Item(2), Today)
            _DocNumber = CStrSafe(dr.Item(3)).Trim
            _Content = CStrSafe(dr.Item(4)).Trim
            _PersonInfo = String.Format("{0} ({1})", CStrSafe(dr.Item(5)).Trim, CStrSafe(dr.Item(6)).Trim)
            _PersonCodeSODRA = CStrSafe(dr.Item(7)).Trim
            _SumJournalEntry = CDblSafe(dr.Item(20), 2, 0)
            _SumBruto = _SumJournalEntry
            _SumNeto = _SumJournalEntry

            MarkNew()
            ValidationRules.CheckRules()
        End Sub

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(0), 0)
            _JournalEntryID = CIntSafe(dr.Item(1), 0)
            _Date = CDateSafe(dr.Item(2), Today)
            _DocNumber = CStrSafe(dr.Item(3)).Trim
            _Content = CStrSafe(dr.Item(4)).Trim
            _PersonInfo = CStrSafe(dr.Item(5)).Trim & " (" & CStrSafe(dr.Item(6)).Trim & ")"
            _PersonCodeSODRA = CStrSafe(dr.Item(7)).Trim
            _SumNeto = CDblSafe(dr.Item(8), 2, 0)
            _RateGPM = CDblSafe(dr.Item(9), 2, 0)
            _RatePSDForPerson = CDblSafe(dr.Item(10), 2, 0)
            _RatePSDForCompany = CDblSafe(dr.Item(11), 2, 0)
            _RateSODRAForPerson = CDblSafe(dr.Item(12), 2, 0)
            _RateSODRAForCompany = CDblSafe(dr.Item(13), 2, 0)
            _SumBruto = CDblSafe(dr.Item(14), 2, 0)
            _CodeVMI = CIntSafe(dr.Item(15), 0)
            _CodeSODRA = CIntSafe(dr.Item(16), 0)
            _SODRABase = 100 - CIntSafe(dr.Item(17), 0)
            _InsertDate = CTimeStampSafe(dr.Item(18))
            _UpdateDate = CTimeStampSafe(dr.Item(19))
            _SumJournalEntry = CDblSafe(dr.Item(20), 2, 0)

            ' To support old version
            If Not CRound(_SumBruto) > 0 Then
                _SumBruto = CRound(100 * _SumJournalEntry / (100 - _RateGPM))
            End If

            RecalculateAllTaxes(False)

            MarkOld()
            ValidationRules.CheckRules()
        End Sub

        Friend Sub Insert()

            Dim myComm As New SQLCommand("InsertPayOutNaturalPerson")
            myComm.AddParam("?BD", _JournalEntryID)
            AddWithParams(myComm)

            myComm.Execute()

            _ID = Convert.ToInt32(myComm.LastInsertID)

            MarkOld()

        End Sub

        Friend Sub Update()

            Dim myComm As New SQLCommand("UpdatePayOutNaturalPerson")
            myComm.AddParam("?PD", _ID)
            AddWithParams(myComm)

            myComm.Execute()

            MarkOld()

        End Sub

        Friend Sub DeleteSelf()

            Dim myComm As New SQLCommand("DeletePayOutNaturalPerson")
            myComm.AddParam("?PD", _ID)

            myComm.Execute()

            MarkNew()

        End Sub


        Private Sub AddWithParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?TG", CRound(_RateGPM))
            myComm.AddParam("?CV", _CodeVMI)
            myComm.AddParam("?SN", CRound(_SumNeto))
            myComm.AddParam("?SB", CRound(_SumBruto))
            myComm.AddParam("?TPW", CRound(_RatePSDForPerson))
            myComm.AddParam("?TPE", CRound(_RatePSDForCompany))
            myComm.AddParam("?TSW", CRound(_RateSODRAForPerson))
            myComm.AddParam("?TSE", CRound(_RateSODRAForCompany))
            myComm.AddParam("?CS", _CodeSODRA)
            myComm.AddParam("?PS", _PersonCodeSODRA.Trim)
            myComm.AddParam("?BS", 100 - _SODRABase)

            _UpdateDate = GetCurrentTimeStamp()
            If Me.IsNew Then _InsertDate = _UpdateDate
            myComm.AddParam("?UD", _UpdateDate.ToUniversalTime)

        End Sub

#End Region

    End Class

End Namespace
