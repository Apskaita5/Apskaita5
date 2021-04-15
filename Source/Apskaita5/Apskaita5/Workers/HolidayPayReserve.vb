Imports ApskaitaObjects.General
Imports ApskaitaObjects.Attributes

Namespace Workers

    ''' <summary>
    ''' Represents an evaluation of future costs of the current unused holiday amount.
    ''' </summary>
    ''' <remarks>Encapsulates a <see cref="General.JournalEntry">JournalEntry</see> of type <see cref="DocumentType.ImprestSheet">DocumentType.ImprestSheet</see>.
    ''' Values are stored in the database table d_avansai.</remarks>
    <Serializable()> _
    Public NotInheritable Class HolidayPayReserve
        Inherits BusinessBase(Of HolidayPayReserve)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _ChronologicValidator As SimpleChronologicValidator
        Private _Number As String = ""
        Private _Date As Date = Today
        Private _Content As String = ""
        Private _AccountCosts As Long = 0
        Private _AccountReserve As Long = 0
        Private _Comments As String = ""
        Private _TaxRate As Double = 0
        Private _TotalSumEvaluatedBeforeTaxes As Double = 0
        Private _TotalSumEvaluated As Double = 0
        Private _TotalSumCurrent As Double = 0
        Private _TotalSumChange As Double = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private WithEvents _Items As HolidayPayReserveItemList

        ' used to implement automatic sort in datagridview
        <NotUndoable()> _
        <NonSerialized()> _
        Private _ItemsSortedList As Csla.SortedBindingList(Of HolidayPayReserveItem) = Nothing


        ''' <summary>
        ''' Gets <see cref="General.JournalEntry.ID">an ID of the journal entry</see> that is created by the holiday pay reserve evaluation.
        ''' </summary>
        ''' <remarks>Value is stored in the database table d_avansai.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object that contains business restraints on updating the sheet.
        ''' </summary>
        ''' <remarks>A <see cref="SimpleChronologicValidator">SimpleChronologicValidator</see> is used to validate an imprest sheet chronological business rules.</remarks>
        Public ReadOnly Property ChronologicValidator() As IChronologicValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologicValidator
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the document was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored by the encapsulated <see cref="General.JournalEntry.InsertDate">JournalEntry.InsertDate</see>.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the document was last updated.
        ''' </summary>
        ''' <remarks>Value is stored by the encapsulated <see cref="General.JournalEntry.UpdateDate">JournalEntry.UpdateDate</see>.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the number of the document.
        ''' </summary>
        ''' <remarks>Value is stored by the encapsulated <see cref="General.JournalEntry.DocNumber">JournalEntry.DocNumber</see>.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 30, False)> _
        Public Property Number() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Number.Trim <> value.Trim Then
                    _Number = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets the date of the document.
        ''' </summary>
        ''' <remarks>Value is stored by the encapsulated <see cref="General.JournalEntry.Date">JournalEntry.Date</see>.</remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the content (description) of the operation.
        ''' </summary>
        ''' <remarks>Value is stored by the encapsulated <see cref="General.JournalEntry.Content">JournalEntry.Content</see>.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255, False)> _
        Public Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Content.Trim <> value.Trim Then
                    _Content = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the (costs or revenues) <see cref="General.Account.ID">account number</see>.
        ''' </summary>
        ''' <remarks>Value is stored in the database table accumulativecosts.AccountCosts.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, True, 5, 6)> _
        Public Property AccountCosts() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountCosts
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If AccountCostsIsReadOnly Then Exit Property
                If _AccountCosts <> value Then
                    _AccountCosts = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <see cref="General.Account.ID">account number</see> 
        ''' in which the holidy pay reserve is stored.
        ''' </summary>
        ''' <remarks>Value is stored in the database table accumulativecosts.AccountAccumulatedCosts.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, True, 3)> _
        Public ReadOnly Property AccountReserve() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountReserve
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the user comments for the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database table accumulativecosts.Comments.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255, False)> _
        Public Property Comments() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Comments.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Comments.Trim <> value.Trim Then
                    _Comments = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets a collection of the holiday pay reserve evaluation items.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Items() As HolidayPayReserveItemList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Items
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable view of the collection of the holiday pay reserve evaluation items.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in datagridview.</remarks>
        Public ReadOnly Property ItemsSorted() As Csla.SortedBindingList(Of HolidayPayReserveItem)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _ItemsSortedList Is Nothing Then _ItemsSortedList = _
                    New Csla.SortedBindingList(Of HolidayPayReserveItem)(_Items)
                Return _ItemsSortedList
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the rate of taxes payed by an employer in percents.
        ''' </summary>
        ''' <remarks>Value is stored in the database table accumulativecosts.Comments.</remarks>
        <DoubleField(ValueRequiredLevel.Recommended, False, 2, True, 0.0, 100.0)> _
        Public Property TaxRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TaxRate, 2)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If TaxRateIsReadOnly Then Exit Property
                If CRound(_TaxRate, 2) <> CRound(value, 2) Then
                    _TaxRate = CRound(value, 2)
                    PropertyHasChanged()
                    RecalculateSubtotals(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets the total sum of the evaluated holiday pay reserve before employer taxes.
        ''' (<see cref="HolidayPayReserveItem.HolidayPayReserveValue">HolidayPayReserveItem.HolidayPayReserveValue</see>).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Recommended, False, 2)> _
        Public ReadOnly Property TotalSumEvaluatedBeforeTaxes() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumEvaluatedBeforeTaxes)
            End Get
        End Property

        ''' <summary>
        ''' Gets the total sum of the evaluated holiday pay reserve.
        ''' (<see cref="ImprestItem.PayOffSumTotal">ImprestItem.PayOffSumTotal</see>).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Recommended, False, 2)> _
        Public ReadOnly Property TotalSumEvaluated() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumEvaluated)
            End Get
        End Property

        ''' <summary>
        ''' Gets the total sum of the current holiday pay reserve
        ''' balance in the <see cref="AccountReserve">AccountReserve</see>).
        ''' </summary>
        ''' <remarks>Positive value is for credit balance (debit balance is not possible).</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalSumCurrent() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumCurrent)
            End Get
        End Property

        ''' <summary>
        ''' Gets the total sum of the current holiday pay reserve
        ''' balance change in the <see cref="AccountReserve">AccountReserve</see>).
        ''' </summary>
        ''' <remarks>Positive value is for credit change.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, True, 2)> _
        Public ReadOnly Property TotalSumChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumChange)
            End Get
        End Property

        ''' <summary>
        ''' Whether the property <see cref="AccountCosts">AccountCosts</see> 
        ''' is readonly due to the business rules defined by the 
        ''' <see cref="ChronologicValidator">ChronologicValidator</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AccountCostsIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ChronologicValidator Is Nothing AndAlso _
                    Not _ChronologicValidator.FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Whether the property <see cref="TaxRate">TaxRate</see> is readonly 
        ''' due to the business rules defined by the 
        ''' <see cref="ChronologicValidator">ChronologicValidator</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TaxRateIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ChronologicValidator Is Nothing AndAlso _
                    Not _ChronologicValidator.FinancialDataCanChange
            End Get
        End Property


        ''' <summary>
        ''' Returnes TRUE if the object is new and contains some user provided data 
        ''' OR
        ''' object is not new and was changed by the user.
        ''' </summary>
        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            Get
                If Not IsNew Then Return IsDirty
                Return (Not StringIsNullOrEmpty(_Content) OrElse _
                    Not Not StringIsNullOrEmpty(_Number) OrElse _
                    Not Not StringIsNullOrEmpty(_Comments) OrElse _
                    _AccountCosts > 0)
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _Items.IsDirty
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid AndAlso _Items.IsValid
            End Get
        End Property


        Public Overrides Function Save() As HolidayPayReserve

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Return MyBase.Save

        End Function


        Private Sub Items_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _Items.ListChanged
            RecalculateSubtotals(True)
        End Sub

        Private Sub RecalculateSubtotals(ByVal raisePropertyHasChanged As Boolean)

            _TotalSumEvaluatedBeforeTaxes = _Items.GetTotalSum
            _TotalSumEvaluated = CRound(_Items.GetTotalSum * (1 + (_TaxRate / 100)), 2)
            _TotalSumChange = CRound(_TotalSumEvaluated - _TotalSumCurrent, 2)

            If raisePropertyHasChanged Then
                PropertyHasChanged("TotalSumEvaluatedBeforeTaxes")
                PropertyHasChanged("TotalSumEvaluated")
                PropertyHasChanged("TotalSumChange")
            End If

        End Sub

        ''' <summary>
        ''' Helper method. Takes care of child lists loosing their handlers.
        ''' </summary>
        Protected Overrides Function GetClone() As Object
            Dim result As HolidayPayReserve = DirectCast(MyBase.GetClone(), HolidayPayReserve)
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
        ''' Helper method. Takes care of item collection loosing its handler. See GetClone method.
        ''' </summary>
        Friend Sub RestoreChildListsHandles()
            Try
                RemoveHandler _Items.ListChanged, AddressOf Items_Changed
            Catch ex As Exception
            End Try
            AddHandler _Items.ListChanged, AddressOf Items_Changed
        End Sub


        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then
                result = AddWithNewLine(result, _
                    Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            End If
            If Not _Items.IsValid Then
                result = AddWithNewLine(result, _Items.GetAllBrokenRules, False)
            End If
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If Not MyBase.BrokenRulesCollection.WarningCount > 0 Then
                result = AddWithNewLine(result, _
                    Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            End If
            If _Items.HasWarnings Then
                result = AddWithNewLine(result, _Items.GetAllWarnings, False)
            End If
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return (MyBase.BrokenRulesCollection.WarningCount > 0 OrElse _Items.HasWarnings)
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Workers_HolidayPayReserve_ToString, _
                _Date.ToString("yyyy-MM-dd"), _Number.ToString(), _ID.ToString())
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Number"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Content"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Comments"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.AccountFieldValidation, _
                New Csla.Validation.RuleArgs("AccountCosts"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("TaxRate"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("TotalSumEvaluatedBeforeTaxes"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("TotalSumEvaluated"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("TotalSumChange"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.ChronologyValidation, _
                New CommonValidation.CommonValidation.ChronologyRuleArgs("Date", "ChronologicValidator"))

        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("Workers.HolidayPayReserve2")
        End Sub

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.HolidayPayReserve2")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.HolidayPayReserve1")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.HolidayPayReserve3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.HolidayPayReserve3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new holiday pay reserve evaluation.
        ''' </summary>
        ''' <param name="nDate">A date which the evaluation is done at.</param>
        ''' <remarks></remarks>
        Public Shared Function NewHolidayPayReserve(ByVal nDate As Date) As HolidayPayReserve
            Return DataPortal.Create(Of HolidayPayReserve)(New Criteria(nDate))
        End Function

        ''' <summary>
        ''' Gets an existing imprest sheet from the database.
        ''' </summary>
        ''' <param name="nID">ID of the holiday pay reserve evaluation to get.</param>
        ''' <remarks></remarks>
        Public Shared Function GetHolidayPayReserve(ByVal nID As Integer) As HolidayPayReserve
            Return DataPortal.Fetch(Of HolidayPayReserve)(New Criteria(nID))
        End Function

        ''' <summary>
        ''' Deletes an existing holiday pay reserve evaluation from the database.
        ''' </summary>
        ''' <param name="id">ID of the holiday pay reserve evaluation to delete.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteHolidayPayReserve(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private ReadOnly _ID As Integer
            Private ReadOnly _Date As Date
            Public ReadOnly Property Id() As Integer
                Get
                    Return _ID
                End Get
            End Property
            Public ReadOnly Property [Date]() As Date
                Get
                    Return _Date
                End Get
            End Property
            Public Sub New(ByVal id As Integer)
                _ID = id
                _Date = Date.MinValue
            End Sub
            Public Sub New(ByVal nDate As Date)
                _ID = 0
                _Date = nDate
            End Sub
        End Class


        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            _Date = criteria.Date
            _ChronologicValidator = SimpleChronologicValidator.NewSimpleChronologicValidator( _
                My.Resources.Workers_HolidayPayReserve_TypeName, Nothing)
            _AccountReserve = GetCurrentCompany().GetDefaultAccount( _
                DefaultAccountType.HolidayReserve)

            If Not _AccountReserve > 0 Then
                Throw New Exception(My.Resources.Workers_HolidayPayReserve_AccountReserveNull)
            End If

            _Items = HolidayPayReserveItemList.NewHolidayPayReserveItemList(criteria.Date)

            Dim myComm As New SQLCommand("FetchHolidayPayReserveBalance")
            myComm.AddParam("?DT", criteria.Date)
            myComm.AddParam("?AC", _AccountReserve)

            Using myData As DataTable = myComm.Fetch()
                If myData.Rows.Count > 0 Then
                    _TotalSumCurrent = CDblSafe(myData.Rows(0).Item(0), 2, 0)
                End If
            End Using

            _TaxRate = CRound(GetCurrentCompany().GetDefaultRate(DefaultRateType.GuaranteeFund) _
                + GetCurrentCompany().GetDefaultRate(DefaultRateType.PsdEmployer) _
                + GetCurrentCompany().GetDefaultRate(DefaultRateType.SodraEmployer))

            RecalculateSubtotals(False)

            MarkNew()

            ValidationRules.CheckRules()

        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchHolidayPayReserve")
            myComm.AddParam("?CD", criteria.Id)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Workers_HolidayPayReserve_TypeName, _
                    criteria.Id.ToString()))

                Dim dr As DataRow = myData.Rows(0)

                _ID = criteria.Id


                _Date = CDateSafe(dr.Item(0), Today)
                _Number = CStrSafe(dr.Item(1))
                _Content = CStrSafe(dr.Item(2))
                _InsertDate = CTimeStampSafe(dr.Item(3))
                _UpdateDate = CTimeStampSafe(dr.Item(4))
                _AccountCosts = CLongSafe(dr.Item(5), 0)
                _AccountReserve = CLongSafe(dr.Item(6), 0)
                _Comments = CStrSafe(dr.Item(7))
                _TaxRate = CDblSafe(dr.Item(8), 2, 0)
                _TotalSumCurrent = CDblSafe(dr.Item(9), 2, 0)

            End Using

            _ChronologicValidator = SimpleChronologicValidator.GetSimpleChronologicValidator( _
                _ID, _Date, My.Resources.Workers_HolidayPayReserve_TypeName, Nothing)

            _Items = HolidayPayReserveItemList.GetHolidayPayReserveItemList( _
                _ID, _Date, _ChronologicValidator.FinancialDataCanChange)

            RecalculateSubtotals(False)

            MarkOld()

            ValidationRules.CheckRules()

        End Sub


        Protected Overrides Sub DataPortal_Insert()

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Dim entry As General.JournalEntry = GetJournalEntry()

            Using transaction As New SqlTransaction

                Try

                    entry = entry.SaveChild()

                    _ID = entry.ID
                    _InsertDate = entry.InsertDate
                    _UpdateDate = entry.UpdateDate

                    Dim myComm As New SQLCommand("InsertHolidayPayReserve")
                    myComm.AddParam("?AA", _AccountCosts)
                    myComm.AddParam("?AB", _AccountReserve)
                    myComm.AddParam("?AC", _Comments.Trim)
                    myComm.AddParam("?AD", CRound(_TaxRate, 2))
                    myComm.AddParam("?AE", _ID)

                    myComm.Execute()

                    _Items.Update(Me)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkOld()

        End Sub

        Protected Overrides Sub DataPortal_Update()

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            _ChronologicValidator = SimpleChronologicValidator.GetSimpleChronologicValidator( _
                _ID, _Date, My.Resources.Workers_HolidayPayReserve_TypeName, Nothing)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Dim entry As General.JournalEntry = GetJournalEntry()

            Using transaction As New SqlTransaction

                Try

                    entry = entry.SaveChild()

                    _UpdateDate = entry.UpdateDate

                    Dim myComm As SQLCommand
                    If _ChronologicValidator.FinancialDataCanChange Then
                        myComm = New SQLCommand("UpdateHolidayPayReserve")
                        myComm.AddParam("?AA", _AccountCosts)
                        myComm.AddParam("?AB", _AccountReserve)
                        myComm.AddParam("?AD", CRound(_TaxRate, 2))
                    Else
                        myComm = New SQLCommand("UpdateHolidayPayReserveNonFinancial")
                    End If
                    myComm.AddParam("?AC", _Comments.Trim)

                    myComm.Execute()

                    If _ChronologicValidator.FinancialDataCanChange Then
                        _Items.Update(Me)
                    End If

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkOld()

        End Sub


        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(_ID))
        End Sub

        Protected Overrides Sub DataPortal_Delete(ByVal criteria As Object)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            IndirectRelationInfoList.CheckIfJournalEntryCanBeDeleted( _
                DirectCast(criteria, Criteria).Id, DocumentType.HolidayReserve)

            Using transaction As New SqlTransaction

                Try

                    General.JournalEntry.DeleteJournalEntryChild(DirectCast(criteria, Criteria).Id)

                    Dim myComm As New SQLCommand("DeleteHolidayPayReserve")
                    myComm.AddParam("?CD", DirectCast(criteria, Criteria).Id)
                    myComm.Execute()

                    myComm = New SQLCommand("DeleteHolidayPayReserveItemList")
                    myComm.AddParam("?CD", DirectCast(criteria, Criteria).Id)
                    myComm.Execute()

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkNew()

        End Sub


        Private Function GetJournalEntry() As General.JournalEntry

            Dim result As General.JournalEntry

            If IsNew Then
                result = General.JournalEntry.NewJournalEntryChild(DocumentType.HolidayReserve)
            Else
                result = General.JournalEntry.GetJournalEntryChild(_ID, DocumentType.HolidayReserve)
                If result.UpdateDate <> _UpdateDate Then Throw New Exception( _
                    My.Resources.Common_UpdateDateHasChanged)
            End If

            result.Content = _Content
            result.Date = _Date.Date
            result.DocNumber = _Number

            RecalculateSubtotals(False) ' just in case

            If IsNew Then
                Dim debetEntry As General.BookEntry = General.BookEntry.NewBookEntry()
                debetEntry.Amount = Math.Abs(_TotalSumChange)
                If _TotalSumChange > 0 Then
                    debetEntry.Account = _AccountCosts
                Else
                    debetEntry.Account = _AccountReserve
                End If
                Dim creditEntry As General.BookEntry = General.BookEntry.NewBookEntry()
                creditEntry.Amount = Math.Abs(_TotalSumChange)
                If _TotalSumChange > 0 Then
                    creditEntry.Account = _AccountReserve
                Else
                    creditEntry.Account = _AccountCosts
                End If
                result.CreditList.Add(creditEntry)
                result.DebetList.Add(debetEntry)
            ElseIf _ChronologicValidator.FinancialDataCanChange Then
                result.DebetList(0).Amount = Math.Abs(_TotalSumChange)
                result.CreditList(0).Amount = Math.Abs(_TotalSumChange)
                If _TotalSumChange > 0 Then
                    result.DebetList(0).Account = _AccountCosts
                    result.CreditList(0).Account = _AccountReserve
                Else
                    result.DebetList(0).Account = _AccountReserve
                    result.CreditList(0).Account = _AccountCosts
                End If
            End If

            If Not result.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_FailedToCreateJournalEntry, _
                    vbCrLf, result.ToString, vbCrLf, result.GetAllBrokenRules))
            End If

            Return result

        End Function

#End Region

    End Class

End Namespace
