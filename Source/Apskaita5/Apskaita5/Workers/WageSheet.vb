﻿Imports ApskaitaObjects.Attributes
Imports ApskaitaObjects.Documents.BankDataExchangeProviders
Imports ApskaitaObjects.My.Resources

Namespace Workers

    ''' <summary>
    ''' Represents wage calculations for particular labour contracts for a particular month.
    ''' </summary>
    ''' <remarks>Values are stored in the database table du_ziniarastis.</remarks>
    <Serializable()> _
    Public NotInheritable Class WageSheet
        Inherits BusinessBase(Of WageSheet)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private Enum WageSheetFetchMode
            NewWageSheet
            OldWageSheet
            Info
        End Enum

        Private _ID As Integer = 0
        Private _ChronologicValidator As SheetChronologicValidator
        Private _Number As Integer = 0
        Private _IsNonClosing As Boolean = False
        Private _CostAccount As Long = 0
        Private _Remarks As String = ""
        Private _Year As Integer = 0
        Private _Month As Integer = 0
        Private _Date As Date = Today
        Private WithEvents _Items As WageItemList
        Private _TotalSum As Double = 0
        Private _TotalSumAfterDeductions As Double = 0
        Private WithEvents _WageRates As CompanyWageRates
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now

        Private _SuspendChildListChangedEvents As Boolean = False
        ' used to implement automatic sort in datagridview
        <NotUndoable()> _
        <NonSerialized()> _
        Private _ItemsSortedList As Csla.SortedBindingList(Of WageItem) = Nothing


        ''' <summary>
        ''' Gets <see cref="General.JournalEntry.ID">an ID of the journal entry</see> that is created by the wage sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.ID.</remarks>
        <IntegerField(ValueRequiredLevel.Optional, True)> _
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object that contains business restraints on updating the sheet.
        ''' </summary>
        ''' <remarks>Underlying type is <see cref="SheetChronologicValidator">SheetChronologicValidator</see>.</remarks>
        Public ReadOnly Property ChronologicValidator() As SheetChronologicValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologicValidator
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the sheet was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.InsertDate.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the sheet was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.UpdateDate.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the number of the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.Nr.</remarks>
        <IntegerField(ValueRequiredLevel.Mandatory, False)> _
        Public Property Number() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If _Number <> value Then
                    _Number = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a boolean values indicating whether the sheet is final for the current month,
        ''' i.e. labour contracts within the sheet will not appear on other sheets for the same month.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.Dalin.</remarks>
        Public Property IsNonClosing() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsNonClosing
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _IsNonClosing <> value Then
                    _IsNonClosing = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets <see cref="General.Account.ID">the account</see> for the total wage costs.
        ''' </summary>
        ''' <remarks>Use <see cref="HelperLists.AccountInfoList">AccountInfoList</see> for the datasource.
        ''' Value is stored in the database field du_ziniarastis.Saskaita.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, False, 1, 2, 6)> _
        Public Property CostAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CostAccount
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If Not _ChronologicValidator.FinancialDataCanChange Then Exit Property
                If _CostAccount <> value Then
                    _CostAccount = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets remarks about the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.Remark.</remarks>
        <StringField(ValueRequiredLevel.Optional, 6000, False)> _
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

        ''' <summary>
        ''' Gets the year of the wage calculations within the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.Metai.</remarks>
        Public ReadOnly Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
        End Property

        ''' <summary>
        ''' Gets the month of the wage calculations within the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.Men.</remarks>
        Public ReadOnly Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the date of the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.Z_data.</remarks>
        Public Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If _Date.Date <> value.Date Then
                    _Date = value.Date
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets a collection of the calculation items.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Items() As WageItemList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Items
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable collection of the calculation items.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in datagridview.</remarks>
        Public ReadOnly Property ItemsSorted() As Csla.SortedBindingList(Of WageItem)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _ItemsSortedList Is Nothing Then _ItemsSortedList = _
                    New Csla.SortedBindingList(Of WageItem)(_Items)
                Return _ItemsSortedList
            End Get
        End Property

        ''' <summary>
        ''' Gets the total sum of the calculated payments before deductions (tax, imprest, other)
        ''' (<see cref="WageItem.PayOutTotal">WageItem.PayOutTotal</see>).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public ReadOnly Property TotalSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSum)
            End Get
        End Property

        ''' <summary>
        ''' Gets the total netto wage minus imprest (part of wage already payed in advance)
        ''' (<see cref="WageItem.PayOutTotal">WageItem.PayOutTotalAfterDeductions</see>).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalSumAfterDeductions() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumAfterDeductions)
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="WageRates">the wage rates</see> applicable for the sheet.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WageRates() As CompanyWageRates
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WageRates
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
                Return (Not String.IsNullOrEmpty(_Remarks.Trim) OrElse _WageRates.IsDirty)
            End Get
        End Property


        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _Items.IsDirty OrElse _WageRates.IsDirty
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid AndAlso _Items.IsValid AndAlso _WageRates.IsValid
            End Get
        End Property



        Public Overrides Function Save() As WageSheet

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Return MyBase.Save

        End Function


        ''' <summary>
        ''' Updates tax rates (GPM, SODRA, etc.) with default values as specified by
        ''' <see cref="ApskaitaObjects.Settings.CompanyInfo.GetDefaultRate">CompanyInfo.GetDefaultRate</see> method.
        ''' </summary>
        Public Sub UpdateTaxRates()

            If Not _ChronologicValidator.FinancialDataCanChange Then _
                Throw New Exception("Klaida. Taisyti finansinių žiniaraščio duomenų neleidžiama:" _
                & vbCrLf & _ChronologicValidator.FinancialDataCanChangeExplanation)

            _SuspendChildListChangedEvents = True

            _WageRates.UpdateTaxRates()
            PropertyHasChanged("RateSODRAEmployee")
            PropertyHasChanged("RateSODRAEmployer")
            PropertyHasChanged("RatePSDEmployee")
            PropertyHasChanged("RatePSDEmployer")
            PropertyHasChanged("RateGuaranteeFund")
            PropertyHasChanged("RateGPM")
            PropertyHasChanged("NPDFormula")

            _SuspendChildListChangedEvents = False

            _Items.UpdateTaxRates(_WageRates, True)

        End Sub

        ''' <summary>
        ''' Updates wage rates (overtime, night work, etc.) with default values as specified by
        ''' <see cref="ApskaitaObjects.Settings.CompanyInfo.GetDefaultRate">CompanyInfo.GetDefaultRate</see> method.
        ''' </summary>
        Public Sub UpdateWageRates()

            If Not _ChronologicValidator.FinancialDataCanChange Then _
                Throw New Exception("Klaida. Taisyti finansinių žiniaraščio duomenų neleidžiama:" _
                & vbCrLf & _ChronologicValidator.FinancialDataCanChangeExplanation)

            _SuspendChildListChangedEvents = True

            _WageRates.UpdateWageRates()
            PropertyHasChanged("RateHR")
            PropertyHasChanged("RateSC")
            PropertyHasChanged("RateON")
            PropertyHasChanged("RateSickLeave")

            _SuspendChildListChangedEvents = False

            _Items.UpdateWageRates(_WageRates, True)

        End Sub

        ''' <summary>
        ''' Calculates <see cref="WageItem.ApplicableNPD">NPD</see> by the formula as provided by <see cref="WageRates">WageRates.NpdFormula</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub CalculateNPD()
            If Not _ChronologicValidator.FinancialDataCanChange Then _
                Throw New Exception("Klaida. Taisyti finansinių žiniaraščio duomenų neleidžiama:" _
                & vbCrLf & _ChronologicValidator.FinancialDataCanChangeExplanation)
            _Items.CalculateNPD(True)
        End Sub

        ''' <summary>
        ''' Updates <see cref="WageItem.ApplicableVDUDaily">ApplicableVDUDaily</see> and <see cref="WageItem.ApplicableVDUHourly">ApplicableVDUHourly</see>.
        ''' </summary>
        ''' <param name="workersVDUList">An object containing VDU info.</param>
        ''' <remarks></remarks>
        Public Sub UpdateWorkersVDUInfo(ByVal workersVDUList As ActiveReports.WorkersVDUInfoList)
            If Not _ChronologicValidator.FinancialDataCanChange Then _
                Throw New Exception(String.Format(My.Resources.Workers_WageSheet_FinancialDataCannotChange, _
                vbCrLf, _ChronologicValidator.FinancialDataCanChangeExplanation))
            _Items.UpdateWorkersVDUInfo(workersVDUList, True)
        End Sub

        Public Function GetWorkersVDUInfoArray() As ActiveReports.WorkersVDUInfo()
            Dim resultList As New List(Of ActiveReports.WorkersVDUInfo)
            For Each item As WageItem In _Items
                If item.IsChecked Then resultList.Add(ActiveReports.WorkersVDUInfo.NewWorkersVDUInfo(Me, item))
            Next
            Return resultList.ToArray
        End Function


        Public Function ExportBankPayments() As ExportedBankPaymentList

            Dim personLookup As PersonInfoList = PersonInfoList.GetList()

            Dim result As ExportedBankPaymentList = ExportedBankPaymentList.NewExportedBankPaymentList()
            For Each item As WageItem In _Items
                If item.IsChecked AndAlso Not item.IsPayedOut Then
                    result.Add(ExportedBankPayment.NewExportedBankPayment(item.PersonID, _
                        item.PayOutTotalAfterDeductions, String.Format(Workers_WageSheet_DescriptionForExportedBankPayment,
                        _Year, _Month), personLookup))
                End If
            Next

            If result.Count < 1 Then Throw New Exception(Workers_WageSheet_NoWageToPay)

            Return result

        End Function


        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            If Not _Items.IsValid Then result = AddWithNewLine(result, _
                _Items.GetAllBrokenRules, False)
            If Not _WageRates.IsValid Then result = AddWithNewLine(result, _
                _WageRates.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If MyBase.BrokenRulesCollection.WarningCount > 0 Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            If _Items.HasWarnings() Then result = AddWithNewLine(result, _Items.GetAllWarnings, False)
            If _WageRates.BrokenRulesCollection.WarningCount > 0 Then _
                result = AddWithNewLine(result, _WageRates.BrokenRulesCollection.ToString( _
                    Validation.RuleSeverity.Warning), False)
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return Me.BrokenRulesCollection.WarningCount > 0 OrElse _
                _WageRates.BrokenRulesCollection.WarningCount > 0 OrElse _
                _Items.HasWarnings()
        End Function


        Private Sub Items_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _Items.ListChanged

            If _SuspendChildListChangedEvents Then Exit Sub

            _TotalSum = _Items.GetTotalSum
            _TotalSumAfterDeductions = _Items.GetTotalSumAfterDeductions
            PropertyHasChanged("TotalSum")
            PropertyHasChanged("TotalSumAfterDeductions")

        End Sub

        Private Sub WageRates_PropertyChanged(ByVal sender As Object, _
            ByVal e As System.ComponentModel.PropertyChangedEventArgs) _
            Handles _WageRates.PropertyChanged

            If _SuspendChildListChangedEvents Then Exit Sub

            Dim wageRatesHasChanged As Boolean = True
            Dim taxRatesHasChanged As Boolean = True

            If Not StringIsNullOrEmpty(e.PropertyName) Then

                Select Case e.PropertyName.Trim
                    Case "RateHR"
                        taxRatesHasChanged = False
                    Case "RateSC"
                        taxRatesHasChanged = False
                    Case "RateON"
                        taxRatesHasChanged = False
                    Case "RateSickLeave"
                        taxRatesHasChanged = False
                    Case "RateSODRAEmployee"
                        wageRatesHasChanged = False
                    Case "RateSODRAEmployer"
                        wageRatesHasChanged = False
                    Case "RatePSDEmployee"
                        wageRatesHasChanged = False
                    Case "RatePSDEmployer"
                        wageRatesHasChanged = False
                    Case "RateGuaranteeFund"
                        wageRatesHasChanged = False
                    Case "RateGPM"
                        wageRatesHasChanged = False
                    Case "NPDFormula"
                        wageRatesHasChanged = False
                End Select

            End If

            If wageRatesHasChanged Then
                _Items.UpdateWageRates(_WageRates, True)
            ElseIf taxRatesHasChanged Then
                _Items.UpdateTaxRates(_WageRates, True)
            End If

        End Sub

        ''' <summary>
        ''' Helper method. Takes care of child lists loosing their handlers.
        ''' </summary>
        Protected Overrides Function GetClone() As Object
            Dim result As WageSheet = DirectCast(MyBase.GetClone(), WageSheet)
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
        ''' Helper method. Takes care of TaskTimeSpans loosing its handler. See GetClone method.
        ''' </summary>
        Friend Sub RestoreChildListsHandles()
            Try
                RemoveHandler _Items.ListChanged, AddressOf Items_Changed
            Catch ex As Exception
            End Try
            Try
                RemoveHandler _WageRates.PropertyChanged, AddressOf WageRates_PropertyChanged
            Catch ex As Exception
            End Try
            AddHandler _Items.ListChanged, AddressOf Items_Changed
            AddHandler _WageRates.PropertyChanged, AddressOf WageRates_PropertyChanged
        End Sub


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Workers_WageSheet_ToString, _
                _Date.ToString("yyyy-MM-dd"), _Number.ToString(), _Year.ToString(), _
                _Month.ToString(), _ID.ToString())
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.IntegerFieldValidation, _
                New Csla.Validation.RuleArgs("Number"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.AccountFieldValidation, _
                New Csla.Validation.RuleArgs("CostAccount"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("TotalSum"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("TotalSumAfterDeductions"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Remarks"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.ChronologyValidation, _
                New CommonValidation.CommonValidation.ChronologyRuleArgs("Date", "ChronologicValidator"))

        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("Workers.WageSheet2")
        End Sub

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.WageSheet2")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.WageSheet1")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.WageSheet3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.WageSheet3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new wage sheet.
        ''' </summary>
        ''' <param name="nYear">A year of the wage sheet.</param>
        ''' <param name="nMonth">A month of the wage sheet.</param>
        ''' <remarks></remarks>
        Public Shared Function NewWageSheet(ByVal nYear As Integer, ByVal nMonth As Integer) As WageSheet

            Dim result As WageSheet = DataPortal.Create(Of WageSheet)(New Criteria(nYear, nMonth))
            result.MarkNew()
            Return result

        End Function

        ''' <summary>
        ''' Gets an existing wage sheet from the database.
        ''' </summary>
        ''' <param name="nID">ID of the wage sheet to get.</param>
        ''' <remarks></remarks>
        Public Shared Function GetWageSheet(ByVal nID As Integer) As WageSheet
            Return DataPortal.Fetch(Of WageSheet)(New Criteria(nID))
        End Function

        ''' <summary>
        ''' Deletes an existing wage sheet from the database.
        ''' </summary>
        ''' <param name="id">ID of the wage sheet to delete.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteWageSheet(ByVal id As Integer)
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
            Private ReadOnly _Year As Integer
            Private ReadOnly _Month As Integer
            Public ReadOnly Property Id() As Integer
                Get
                    Return _ID
                End Get
            End Property
            Public ReadOnly Property Year() As Integer
                Get
                    Return _Year
                End Get
            End Property
            Public ReadOnly Property Month() As Integer
                Get
                    Return _Month
                End Get
            End Property
            Public Sub New(ByVal id As Integer)
                _ID = id
                _Year = 0
                _Month = 0
            End Sub
            Public Sub New(ByVal nYear As Integer, ByVal nMonth As Integer)
                _ID = 0
                _Year = nYear
                _Month = nMonth
            End Sub
        End Class


        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            If Not criteria.Year > 0 OrElse Not criteria.Month > 0 Then Throw New Exception( _
                My.Resources.Workers_WageSheet_YearOrMonthNull)

            _WageRates = CompanyWageRates.NewCompanyWageRates
            _Year = criteria.Year
            _Month = criteria.Month
            _Date = New Date(criteria.Year, criteria.Month, _
                Date.DaysInMonth(criteria.Year, criteria.Month))

            Dim myComm As New SQLCommand("FetchNewWageSheet")
            myComm.AddParam("?DT", New Date(_Year, _Month, Date.DaysInMonth(_Year, _Month)))
            myComm.AddParam("?DA", New Date(_Year, _Month, 1))
            myComm.AddParam("?YR", _Year)
            myComm.AddParam("?MN", _Month)

            Using myData As DataTable = myComm.Fetch

                Dim wtl As DefaultWorkTimeInfoList = DefaultWorkTimeInfoList.GetListChild()
                Dim wt As DefaultWorkTimeInfo = wtl.GetDefaultWorkTimeInfo(_Year, _Month)

                _Items = WageItemList.NewWageItemList(myData, _WageRates, _Year, _Month, wt)

            End Using

            _ChronologicValidator = SheetChronologicValidator.NewSheetChronologicValidator( _
                DocumentType.WageSheet, _Year, _Month)

            MarkNew()

            ValidationRules.CheckRules()

        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchWageGeneralData")
            myComm.AddParam("?SD", criteria.Id)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Workers_WageSheet_TypeName, _
                    criteria.Id.ToString()))

                Dim dr As DataRow = myData.Rows(0)

                _ID = criteria.Id
                _Number = CIntSafe(dr.Item(0), 0)
                _Date = CDateSafe(dr.Item(1), Today)
                _Year = CIntSafe(dr.Item(2), 0)
                _Month = CIntSafe(dr.Item(3), 0)
                _IsNonClosing = (CStrSafe(dr.Item(4)).Trim.ToLower() <> "n")
                _CostAccount = CLongSafe(dr.Item(5), 0)
                _Remarks = CStrSafe(dr.Item(6)).Trim

                _ChronologicValidator = SheetChronologicValidator.GetSheetChronologicValidator( _
                    DocumentType.WageSheet, _ID, _Date, _Year, _Month, Nothing)

                _WageRates = CompanyWageRates.GetCompanyWageRates(dr, _ChronologicValidator.FinancialDataCanChange)

                _InsertDate = CTimeStampSafe(dr.Item(19))
                _UpdateDate = CTimeStampSafe(dr.Item(20))

            End Using

            myComm = New SQLCommand("FetchWageDetails")
            myComm.AddParam("?SD", criteria.Id)
            myComm.AddParam("?DT", New Date(_Year, _Month, Date.DaysInMonth(_Year, _Month)))
            myComm.AddParam("?YR", _Year)
            myComm.AddParam("?MN", _Month)

            Using myData As DataTable = myComm.Fetch
                _Items = WageItemList.GetWageItemList(myData, _WageRates, _Year, _Month, _
                    _ChronologicValidator.FinancialDataCanChange)
            End Using

            _TotalSum = _Items.GetTotalSum
            _TotalSumAfterDeductions = _Items.GetTotalSumAfterDeductions

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

                    Dim myComm As New SQLCommand("InsertWageSheet")
                    AddWithParamsGeneral(myComm)
                    AddWithParamsFinancial(myComm)
                    myComm.AddParam("?ME", _Year)
                    myComm.AddParam("?MN", _Month)
                    myComm.Execute()

                    Items.Update(Me)

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

            _ChronologicValidator = SheetChronologicValidator.GetSheetChronologicValidator( _
                DocumentType.WageSheet, _ID, _ChronologicValidator.CurrentOperationDate, _
                _Year, _Month, Nothing)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Dim entry As General.JournalEntry = GetJournalEntry()

            CheckIfUpdateDateChanged()

            Dim myComm As SQLCommand
            If _ChronologicValidator.FinancialDataCanChange Then
                myComm = New SQLCommand("UpdateWageSheet")
                AddWithParamsFinancial(myComm)
            Else
                myComm = New SQLCommand("UpdateWageSheetNonFinancial")
            End If
            AddWithParamsGeneral(myComm)

            Using transaction As New SqlTransaction

                Try

                    entry = entry.SaveChild()

                    myComm.Execute()

                    If _ChronologicValidator.FinancialDataCanChange Then Items.Update(Me)

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

            SheetChronologicValidator.CheckIfCanDelete(DocumentType.WageSheet, _
                DirectCast(criteria, Criteria).Id)

            IndirectRelationInfoList.CheckIfJournalEntryCanBeDeleted( _
                DirectCast(criteria, Criteria).Id, DocumentType.WageSheet)

            Dim myComm As New SQLCommand("DeleteAllWageItems")
            myComm.AddParam("?SD", DirectCast(criteria, Criteria).Id)

            Using transaction As New SqlTransaction

                Try

                    General.JournalEntry.DeleteJournalEntryChild(DirectCast(criteria, Criteria).Id)

                    myComm.Execute()

                    myComm = New SQLCommand("DeleteWageSheet")
                    myComm.AddParam("?SD", DirectCast(criteria, Criteria).Id)

                    myComm.Execute()

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkNew()

        End Sub


        Private Sub AddWithParamsGeneral(ByRef myComm As SQLCommand)

            myComm.AddParam("?BD", _ID)
            myComm.AddParam("?ZD", _Date.Date)
            myComm.AddParam("?NR", _Number)
            If _IsNonClosing Then
                myComm.AddParam("?DA", "t")
            Else
                myComm.AddParam("?DA", "n")
            End If
            myComm.AddParam("?RM", _Remarks)

            _UpdateDate = GetCurrentTimeStamp()
            If Me.IsNew Then _InsertDate = _UpdateDate
            myComm.AddParam("?UD", _UpdateDate.ToUniversalTime)

        End Sub

        Private Sub AddWithParamsFinancial(ByRef myComm As SQLCommand)

            myComm.AddParam("?PH", _WageRates.RateHR)
            myComm.AddParam("?NV", _WageRates.RateON)
            myComm.AddParam("?YS", _WageRates.RateSC)
            myComm.AddParam("?NE", _WageRates.RateSickLeave)
            myComm.AddParam("?GP", _WageRates.RateGPM)
            myComm.AddParam("?GL", _WageRates.RateGPMSickLeave)
            myComm.AddParam("?NL", ConvertDbBoolean(_WageRates.ApplyNpdToSickLeave))
            myComm.AddParam("?GA", _WageRates.RateGuaranteeFund)
            myComm.AddParam("?SD", _WageRates.RateSODRAEmployee)
            myComm.AddParam("?SV", _WageRates.RateSODRAEmployer)
            myComm.AddParam("?PW", _WageRates.RatePSDEmployee)
            myComm.AddParam("?PE", _WageRates.RatePSDEmployer)
            myComm.AddParam("?NF", _WageRates.NPDFormula.Trim)
            myComm.AddParam("?SA", _CostAccount)

        End Sub

        Private Function GetJournalEntry() As General.JournalEntry

            Dim result As General.JournalEntry

            If IsNew Then
                result = General.JournalEntry.NewJournalEntryChild(DocumentType.WageSheet)
            Else
                result = General.JournalEntry.GetJournalEntryChild(_ID, DocumentType.WageSheet)
            End If

            result.Content = String.Format(My.Resources.Workers_WageSheet_JournalEntryContent, _
                _Year.ToString, _Month.ToString)
            result.Date = _Date.Date
            result.DocNumber = String.Format(My.Resources.Workers_WageSheet_JournalEntryDocNumber, _
                _Number.ToString)

            If _ChronologicValidator.FinancialDataCanChange Then

                result.CreditList.Clear()
                result.DebetList.Clear()

                Dim nTotalSODRA As Double = _Items.GetTotalSODRAPayments
                Dim nTotalPSD As Double = _Items.GetTotalPSDPaymentsForSODRA
                Dim nTotalPSDForVmi As Double = _Items.GetTotalPSDPaymentsForVMI
                Dim nTotalGPM As Double = _Items.GetTotalGPMDeductions
                Dim nTotalGf As Double = _Items.GetTotalGuaranteeFundContributions
                Dim nTotalWageForPayOut As Double = _Items.GetTotalSumAfterDeductions
                Dim nTotalImprestDeductions As Double = _Items.GetTotalImprestDeductions
                Dim nTotalOtherDeductions As Double = _Items.GetTotalOtherDeductions
                Dim nTotalCosts As Double = _Items.GetTotalCosts

                If Not CRound(nTotalCosts) > 0 Then
                    Throw New Exception(My.Resources.Workers_WageSheet_CostsNull)
                Else
                    Dim debetEntry As General.BookEntry = General.BookEntry.NewBookEntry()
                    debetEntry.Amount = CRound(nTotalCosts)
                    debetEntry.Account = _CostAccount
                    result.DebetList.Add(debetEntry)
                End If

                If CRound(nTotalWageForPayOut) > 0 Then
                    Dim creditEntry As General.BookEntry = General.BookEntry.NewBookEntry()
                    creditEntry.Amount = CRound(nTotalWageForPayOut)
                    creditEntry.Account = GetCurrentCompany.Accounts.GetAccount( _
                        General.DefaultAccountType.WagePayable)
                    result.CreditList.Add(creditEntry)
                End If

                If CRound(nTotalGf) > 0 Then
                    Dim creditEntry As General.BookEntry = General.BookEntry.NewBookEntry()
                    creditEntry.Amount = CRound(nTotalGf)
                    creditEntry.Account = GetCurrentCompany.Accounts.GetAccount( _
                        General.DefaultAccountType.WageGuaranteeFundPayable)
                    result.CreditList.Add(creditEntry)
                End If

                If CRound(nTotalOtherDeductions) > 0 Then
                    Dim creditEntry As General.BookEntry = General.BookEntry.NewBookEntry()
                    creditEntry.Amount = CRound(nTotalOtherDeductions)
                    creditEntry.Account = GetCurrentCompany.Accounts.GetAccount( _
                        General.DefaultAccountType.WageWithdraw)
                    result.CreditList.Add(creditEntry)
                End If

                If CRound(nTotalImprestDeductions) > 0 Then
                    Dim creditEntry As General.BookEntry = General.BookEntry.NewBookEntry()
                    creditEntry.Amount = CRound(nTotalImprestDeductions)
                    creditEntry.Account = GetCurrentCompany.Accounts.GetAccount( _
                        General.DefaultAccountType.WageImprestPayable)
                    result.CreditList.Add(creditEntry)
                End If

                If CRound(nTotalGPM) > 0 Then
                    Dim creditEntry As General.BookEntry = General.BookEntry.NewBookEntry()
                    creditEntry.Amount = CRound(nTotalGPM)
                    creditEntry.Account = GetCurrentCompany.Accounts.GetAccount( _
                        General.DefaultAccountType.WageGpmPayable)
                    result.CreditList.Add(creditEntry)
                End If

                If CRound(nTotalSODRA) > 0 OrElse CRound(nTotalPSD) > 0 Then

                    If GetCurrentCompany.Accounts.GetAccount(General.DefaultAccountType.WagePsdPayable) _
                        = GetCurrentCompany.Accounts.GetAccount( _
                        General.DefaultAccountType.WageSodraPayable) Then

                        Dim creditEntry As General.BookEntry = General.BookEntry.NewBookEntry()
                        creditEntry.Amount = CRound(CRound(nTotalSODRA) + CRound(nTotalPSD))
                        creditEntry.Account = GetCurrentCompany.Accounts.GetAccount( _
                            General.DefaultAccountType.WageSodraPayable)
                        result.CreditList.Add(creditEntry)

                    Else

                        If CRound(nTotalSODRA) > 0 Then
                            Dim creditEntry As General.BookEntry = General.BookEntry.NewBookEntry()
                            creditEntry.Amount = CRound(nTotalSODRA)
                            creditEntry.Account = GetCurrentCompany.Accounts.GetAccount( _
                                General.DefaultAccountType.WageSodraPayable)
                            result.CreditList.Add(creditEntry)
                        End If
                        If CRound(nTotalPSD) > 0 Then
                            Dim creditEntry As General.BookEntry = General.BookEntry.NewBookEntry()
                            creditEntry.Amount = CRound(nTotalPSD)
                            creditEntry.Account = GetCurrentCompany.Accounts.GetAccount( _
                                General.DefaultAccountType.WagePsdPayable)
                            result.CreditList.Add(creditEntry)
                        End If

                    End If

                End If

                If CRound(nTotalPSDForVmi) > 0 Then
                    Dim creditEntry As General.BookEntry = General.BookEntry.NewBookEntry()
                    creditEntry.Amount = CRound(nTotalPSDForVmi)
                    creditEntry.Account = GetCurrentCompany.Accounts.GetAccount( _
                        General.DefaultAccountType.WagePsdPayableToVMI)
                    result.CreditList.Add(creditEntry)
                End If

            End If

            If Not result.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_FailedToCreateJournalEntry, _
                    vbCrLf, result.ToString, vbCrLf, result.GetAllBrokenRules))
            End If

            Return result

        End Function

        Private Sub CheckIfUpdateDateChanged()

            Dim myComm As New SQLCommand("CheckIfWageSheetUpdateDateChanged")
            myComm.AddParam("?CD", _ID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 OrElse CDateTimeSafe(myData.Rows(0).Item(0), _
                    Date.MinValue) = Date.MinValue Then

                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                        My.Resources.Workers_WageSheet_TypeName, _ID.ToString))

                End If

                If CTimeStampSafe(myData.Rows(0).Item(0)) <> _UpdateDate Then

                    Throw New Exception(My.Resources.Common_UpdateDateHasChanged)

                End If

            End Using

        End Sub

#End Region

    End Class

End Namespace