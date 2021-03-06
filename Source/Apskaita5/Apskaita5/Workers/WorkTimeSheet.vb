﻿Imports ApskaitaObjects.Attributes

Namespace Workers

    ''' <summary>
    ''' Represents work and rest time data for specific labour contracts for a specific month.
    ''' </summary>
    ''' <remarks>Values are stored in the database table worktimesheets.</remarks>
    <Serializable()> _
    Public NotInheritable Class WorkTimeSheet
        Inherits BusinessBase(Of WorkTimeSheet)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Date As Date = Today
        Private _Number As String = ""
        Private _SubDivision As String = ""
        Private _Year As Integer = Today.Year
        Private _Month As Integer = Today.Month
        Private _SignedByPosition As String = ""
        Private _SignedByName As String = ""
        Private _PreparedByPosition As String = ""
        Private _PreparedByName As String = ""
        Private _WorkersCount As Integer = 0
        Private _TotalWorkTime As Double = 0
        Private _DefaultRestTimeClass As WorkTimeClassInfo = Nothing
        Private _DefaultPublicHolidayTimeClass As WorkTimeClassInfo = Nothing
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private WithEvents _GeneralItemList As WorkTimeItemList
        Private WithEvents _SpecialItemList As SpecialWorkTimeItemList


        ''' <summary>
        ''' Gets an ID of the sheet that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimesheets.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the sheet was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.InsertDate.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the sheet was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.UpdateDate.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the default <see cref="HelperLists.WorkTimeClassInfo">rest time class</see>.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.RestTypeID.</remarks>
        Public ReadOnly Property DefaultRestTimeClass() As WorkTimeClassInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DefaultRestTimeClass
            End Get
        End Property

        ''' <summary>
        ''' Gets the default <see cref="HelperLists.WorkTimeClassInfo">public holiday time class</see>.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.PublicHolidaysTypeID.</remarks>
        Public ReadOnly Property DefaultPublicHolidayTimeClass() As WorkTimeClassInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DefaultPublicHolidayTimeClass
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the date of the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.SheetDate.</remarks>
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
        ''' Gets or sets the number of the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.Number.</remarks>
        <StringField(ValueRequiredLevel.Recommended, 100)> _
        Public Property Number() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Number.Trim <> value.Trim Then
                    _Number = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the subdivision of the company that the sheet is ment for.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.SubDivision.</remarks>
        <StringField(ValueRequiredLevel.Recommended, 255)> _
        Public Property SubDivision() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SubDivision.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _SubDivision.Trim <> value.Trim Then
                    _SubDivision = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets the year of the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.SheetYear.</remarks>
        Public ReadOnly Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
        End Property

        ''' <summary>
        ''' Gets the month of the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.SheetMonth.</remarks>
        Public ReadOnly Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the position of the worker who signed the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.SignedByPosition.</remarks>
        <StringField(ValueRequiredLevel.Recommended, 255)> _
        Public Property SignedByPosition() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SignedByPosition.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _SignedByPosition.Trim <> value.Trim Then
                    _SignedByPosition = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the name of the worker who signed the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.SignedByName.</remarks>
        <StringField(ValueRequiredLevel.Recommended, 255)> _
        Public Property SignedByName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SignedByName.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _SignedByName.Trim <> value.Trim Then
                    _SignedByName = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the position of the worker who prepared the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.PreparedByPosition.</remarks>
        <StringField(ValueRequiredLevel.Recommended, 255)> _
        Public Property PreparedByPosition() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PreparedByPosition.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _PreparedByPosition.Trim <> value.Trim Then
                    _PreparedByPosition = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the name of the worker who prepared the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field worktimesheets.PreparedByName.</remarks>
        <StringField(ValueRequiredLevel.Recommended, 255)> _
        Public Property PreparedByName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PreparedByName.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _PreparedByName.Trim <> value.Trim Then
                    _PreparedByName = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets the general work and (or) rest time items.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property GeneralItemList() As WorkTimeItemList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GeneralItemList
            End Get
        End Property

        ''' <summary>
        ''' Gets the non standard work and (or) rest time items.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property SpecialItemList() As SpecialWorkTimeItemList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SpecialItemList
            End Get
        End Property

        ''' <summary>
        ''' Gets the general work and (or) rest time items as a sortable collection.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public ReadOnly Property GeneralItemListSorted() As Csla.SortedBindingList(Of WorkTimeItem)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GeneralItemList.GetSortedList
            End Get
        End Property

        ''' <summary>
        ''' Gets the non standard work and (or) rest time items as a sortable collection.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public ReadOnly Property SpecialItemListSorted() As Csla.SortedBindingList(Of SpecialWorkTimeItem)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SpecialItemList.GetSortedList
            End Get
        End Property

        ''' <summary>
        ''' Gets the total workers count within the sheet.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Mandatory, False)> _
        Public ReadOnly Property WorkersCount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkersCount
            End Get
        End Property

        ''' <summary>
        ''' Gets the total work time within the sheet.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TotalWorkTime() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalWorkTime, ROUNDWORKHOURS)
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
                Return (Not String.IsNullOrEmpty(_Number.Trim) _
                    OrElse Not String.IsNullOrEmpty(_SubDivision.Trim) _
                    OrElse Not String.IsNullOrEmpty(_SignedByPosition.Trim) _
                    OrElse Not String.IsNullOrEmpty(_SignedByName.Trim) _
                    OrElse Not String.IsNullOrEmpty(_PreparedByPosition.Trim) _
                    OrElse Not String.IsNullOrEmpty(_PreparedByName.Trim) _
                    OrElse _SpecialItemList.Count > 0)
            End Get
        End Property


        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _GeneralItemList.IsDirty OrElse _SpecialItemList.IsDirty
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid AndAlso _GeneralItemList.IsValid AndAlso _SpecialItemList.IsValid
            End Get
        End Property



        Public Overrides Function Save() As WorkTimeSheet

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Return MyBase.Save

        End Function


        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            If Not _GeneralItemList.IsValid Then result = AddWithNewLine(result, _
                _GeneralItemList.GetAllBrokenRules, False)
            If Not _SpecialItemList.IsValid Then result = AddWithNewLine(result, _
                _SpecialItemList.GetAllBrokenRules, False)
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If MyBase.BrokenRulesCollection.WarningCount > 0 Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            If _GeneralItemList.HasWarning() Then result = _
                AddWithNewLine(result, _GeneralItemList.GetAllWarnings, False)
            If _SpecialItemList.HasWarnings() Then result = _
                AddWithNewLine(result, _SpecialItemList.GetAllWarnings, False)
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return Me.BrokenRulesCollection.WarningCount > 0 OrElse _
                _GeneralItemList.HasWarning() OrElse _SpecialItemList.HasWarnings()
        End Function


        Private Sub GeneralItemList_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _GeneralItemList.ListChanged
            RecalculateGeneralTotals(True)
        End Sub

        Private Sub RecalculateGeneralTotals(ByVal raisePropertyHasChanged As Boolean)

            _WorkersCount = 0
            _TotalWorkTime = 0

            For Each item As WorkTimeItem In _GeneralItemList
                If item.IsChecked Then
                    _WorkersCount += 1
                    _TotalWorkTime = CRound(_TotalWorkTime + item.TotalHours, ROUNDWORKHOURS)
                End If
            Next

            If raisePropertyHasChanged Then
                PropertyHasChanged("WorkersCount")
                PropertyHasChanged("TotalWorkTime")
            End If

        End Sub

        Private Sub SpecialItemList_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _SpecialItemList.ListChanged


        End Sub

        ''' <summary>
        ''' Helper method. Takes care of child lists loosing their handlers.
        ''' </summary>
        Protected Overrides Function GetClone() As Object
            Dim result As WorkTimeSheet = DirectCast(MyBase.GetClone(), WorkTimeSheet)
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
                RemoveHandler _GeneralItemList.ListChanged, AddressOf GeneralItemList_Changed
                RemoveHandler _SpecialItemList.ListChanged, AddressOf SpecialItemList_Changed
            Catch ex As Exception
            End Try
            AddHandler _GeneralItemList.ListChanged, AddressOf GeneralItemList_Changed
            AddHandler _SpecialItemList.ListChanged, AddressOf SpecialItemList_Changed
        End Sub


        ''' <summary>
        ''' Gets the total general work hours within the sheet.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetTotalHours() As Double

            Dim result As Double = 0

            For Each item As WorkTimeItem In _GeneralItemList
                If item.IsChecked Then result = CRound(result + item.TotalHours, ROUNDWORKHOURS)
            Next

            Return result

        End Function

        ''' <summary>
        ''' Gets the total general work days within the sheet.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetTotalDays() As Integer

            Dim result As Integer = 0

            For Each item As WorkTimeItem In _GeneralItemList
                If item.IsChecked Then result = result + item.TotalDays
            Next

            Return result

        End Function

        ''' <summary>
        ''' Gets the total non standard work hours within the sheet by a specified 
        ''' <see cref="WorkTimeType">general work and rest time type</see>.
        ''' </summary>
        ''' <param name="timeClass">A type of work and rest time.</param>
        ''' <remarks></remarks>
        Public Function GetTotalHoursByType(ByVal timeClass As WorkTimeType) As Double

            Dim result As Double = 0

            For Each item As SpecialWorkTimeItem In _SpecialItemList
                If item.Type.Type = timeClass Then result = CRound(result + item.TotalHours, ROUNDWORKHOURS)
            Next

            Return result

        End Function

        ''' <summary>
        ''' Gets the total non standard hours within the sheet when a worker was absent (did not work).
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetTotalAbsenceHours() As Double

            Dim result As Double = 0

            For Each item As SpecialWorkTimeItem In _SpecialItemList
                If item.Type.Type = WorkTimeType.AnnualHolidays OrElse _
                    item.Type.Type = WorkTimeType.DownTime OrElse _
                    item.Type.Type = WorkTimeType.OtherExcluded OrElse _
                    item.Type.Type = WorkTimeType.OtherHolidays OrElse _
                    item.Type.Type = WorkTimeType.SickDays OrElse _
                    item.Type.Type = WorkTimeType.Truancy Then result = _
                        CRound(result + item.TotalHours, ROUNDWORKHOURS)
            Next

            Return result

        End Function

        ''' <summary>
        ''' Gets the total general days within the sheet when a worker was absent (did not work).
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetTotalAbsenceDays() As Integer

            Dim result As Integer = 0

            For Each item As WorkTimeItem In _GeneralItemList
                If item.IsChecked Then result = result + item.GetTotalAbsenceDays(_DefaultRestTimeClass, _
                    _DefaultPublicHolidayTimeClass)
            Next

            Return result

        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Workers_WorkTimeSheet_ToString, _
                _Date.ToString("yyyy-MM-dd"), _Year.ToString(), _Month.ToString(), _ID.ToString())
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Number"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("SignedByPosition"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("SignedByName"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("PreparedByPosition"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("PreparedByName"))

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.IntegerFieldValidation, _
                New Csla.Validation.RuleArgs("WorkersCount"))

        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("Workers.WorkTimeSheet2")
        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.WorkTimeSheet1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.WorkTimeSheet2")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.WorkTimeSheet3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.WorkTimeSheet3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Creates a new instance of WorkTimeSheet for a specified year and month.
        ''' </summary>
        ''' <param name="forYear">Year of the new WorkTimeSheet.</param>
        ''' <param name="forMonth">Month of the new WorkTimeSheet.</param>
        ''' <param name="restDayInfo">Default work and rest time type to use for the rest days.</param>
        ''' <param name="publicHolidaysInfo">Default work and rest time type to use for the public holiday.</param>
        ''' <remarks></remarks>
        Public Shared Function NewWorkTimeSheet(ByVal forYear As Integer, _
            ByVal forMonth As Integer, ByVal restDayInfo As WorkTimeClassInfo, _
            ByVal publicHolidaysInfo As WorkTimeClassInfo) As WorkTimeSheet

            Dim result As WorkTimeSheet = DataPortal.Create(Of WorkTimeSheet) _
                (New CreateCriteria(forYear, forMonth, restDayInfo, publicHolidaysInfo))
            result.MarkNew()
            Return result

        End Function

        ''' <summary>
        ''' Gets an existing instance of WorkTimeSheet from a database.
        ''' </summary>
        ''' <param name="nID">An ID of the WorkTimeSheet to get.</param>
        ''' <remarks></remarks>
        Public Shared Function GetWorkTimeSheet(ByVal nID As Integer) As WorkTimeSheet
            Return DataPortal.Fetch(Of WorkTimeSheet)(New Criteria(nID))
        End Function

        ''' <summary>
        ''' Deletes an existing instance of WorkTimeSheet from a database.
        ''' </summary>
        ''' <param name="id">An ID of the WorkTimeSheet to delete.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteWorkTimeSheet(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ID As Integer
            Public ReadOnly Property ID() As Integer
                Get
                    Return _ID
                End Get
            End Property
            Public Sub New(ByVal nID As Integer)
                _ID = nID
            End Sub
        End Class

        <Serializable()> _
        Private Class CreateCriteria
            Private _Year As Integer
            Private _Month As Integer
            Private _RestDayInfo As WorkTimeClassInfo
            Private _PublicHolidaysInfo As WorkTimeClassInfo
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
            Public ReadOnly Property RestDayInfo() As WorkTimeClassInfo
                Get
                    Return _RestDayInfo
                End Get
            End Property
            Public ReadOnly Property PublicHolidaysInfo() As WorkTimeClassInfo
                Get
                    Return _PublicHolidaysInfo
                End Get
            End Property
            Public Sub New(ByVal nYear As Integer, ByVal nMonth As Integer, _
                ByVal nRestDayInfo As WorkTimeClassInfo, ByVal nPublicHolidaysInfo As WorkTimeClassInfo)
                _Year = nYear
                _Month = nMonth
                _RestDayInfo = nRestDayInfo
                _PublicHolidaysInfo = nPublicHolidaysInfo
            End Sub
        End Class


        Private Overloads Sub DataPortal_Create(ByVal criteria As CreateCriteria)

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            If Not criteria.Year > 0 Then Throw New Exception(My.Resources.Workers_WorkTimeSheet_YearNull)
            If criteria.Year > 2100 OrElse criteria.Year < 1950 Then Throw New Exception( _
                My.Resources.Workers_WorkTimeSheet_YearInvalid)
            If Not criteria.Month > 0 Then Throw New Exception(My.Resources.Workers_WorkTimeSheet_MonthNull)
            If criteria.Month > 12 Then Throw New Exception( _
                My.Resources.Workers_WorkTimeSheet_MonthInvalid)
            If criteria.RestDayInfo Is Nothing OrElse Not criteria.RestDayInfo.ID > 0 Then _
                Throw New Exception(My.Resources.Workers_WorkTimeSheet_RestTimeTypeNull)
            If criteria.PublicHolidaysInfo Is Nothing OrElse Not criteria.PublicHolidaysInfo.ID > 0 Then _
                Throw New Exception(My.Resources.Workers_WorkTimeSheet_PublicHolidayTimeTypeNull)

            _Year = criteria.Year
            _Month = criteria.Month
            _DefaultRestTimeClass = criteria.RestDayInfo
            _DefaultPublicHolidayTimeClass = criteria.PublicHolidaysInfo
            _Date = New Date(criteria.Year, criteria.Month, _
                Date.DaysInMonth(criteria.Year, criteria.Month))

            _GeneralItemList = WorkTimeItemList.NewWorkTimeItemList(Me, _
                criteria.RestDayInfo, criteria.PublicHolidaysInfo)
            _SpecialItemList = SpecialWorkTimeItemList.NewSpecialWorkTimeItemList(Me)

            MarkNew()

            RecalculateGeneralTotals(False)

            ValidationRules.CheckRules()

        End Sub


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchWorkTimeSheet")
            myComm.AddParam("?PD", criteria.ID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Workers_WorkTimeSheet_TypeName, _
                    criteria.ID.ToString()))

                Dim dr As DataRow = myData.Rows(0)

                _Date = CDateSafe(dr.Item(0), Today)
                _Number = CStrSafe(dr.Item(1)).Trim
                _SubDivision = CStrSafe(dr.Item(2)).Trim
                _Year = CIntSafe(dr.Item(3), 0)
                _Month = CIntSafe(dr.Item(4), 0)
                _SignedByPosition = CStrSafe(dr.Item(5)).Trim
                _SignedByName = CStrSafe(dr.Item(6)).Trim
                _PreparedByPosition = CStrSafe(dr.Item(7)).Trim
                _PreparedByName = CStrSafe(dr.Item(8)).Trim
                _InsertDate = CTimeStampSafe(dr.Item(9))
                _UpdateDate = CTimeStampSafe(dr.Item(10))
                _DefaultRestTimeClass = WorkTimeClassInfo.GetWorkTimeClassInfo(dr, 11)
                _DefaultPublicHolidayTimeClass = WorkTimeClassInfo.GetWorkTimeClassInfo(dr, 20)

                _ID = criteria.ID

            End Using

            _GeneralItemList = WorkTimeItemList.GetWorkTimeItemList(Me)
            _SpecialItemList = SpecialWorkTimeItemList.GetSpecialWorkTimeItemList(Me)

            RecalculateGeneralTotals(False)

            ValidationRules.CheckRules()

            MarkOld()

        End Sub


        Protected Overrides Sub DataPortal_Insert()

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Dim myComm As New SQLCommand("InsertWorkTimeSheet")
            AddWithParams(myComm)
            myComm.AddParam("?AD", _Year)
            myComm.AddParam("?AE", _Month)
            myComm.AddParam("?AK", _DefaultRestTimeClass.ID)
            myComm.AddParam("?AL", _DefaultPublicHolidayTimeClass.ID)

            Using transaction As New SqlTransaction

                Try

                    myComm.Execute()

                    _ID = Convert.ToInt32(myComm.LastInsertID)

                    GeneralItemList.Update(Me)
                    SpecialItemList.Update(Me)

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

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            CheckIfUpdateDateChanged()

            Dim myComm As New SQLCommand("UpdateWorkTimeSheet")
            AddWithParams(myComm)
            myComm.AddParam("?CD", _ID)

            Using transaction As New SqlTransaction

                Try

                    myComm.Execute()

                    GeneralItemList.Update(Me)
                    SpecialItemList.Update(Me)

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

            Dim myComm As New SQLCommand("DeleteDayWorkTimeForWorkTimeSheet")
            myComm.AddParam("?PD", DirectCast(criteria, Criteria).ID)

            Using transaction As New SqlTransaction

                Try

                    myComm.Execute()

                    myComm = New SQLCommand("DeleteWorkTimeItemList")
                    myComm.AddParam("?PD", DirectCast(criteria, Criteria).ID)

                    myComm.Execute()

                    myComm = New SQLCommand("DeleteSpecialWorkTimeItemList")
                    myComm.AddParam("?CD", DirectCast(criteria, Criteria).ID)

                    myComm.Execute()

                    myComm = New SQLCommand("DeleteWorkTimeSheet")
                    myComm.AddParam("?CD", DirectCast(criteria, Criteria).ID)

                    myComm.Execute()

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkNew()

        End Sub


        Private Sub AddWithParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AA", _Date.Date)
            myComm.AddParam("?AB", _Number.Trim)
            myComm.AddParam("?AC", _SubDivision.Trim)
            myComm.AddParam("?AF", _SignedByPosition.Trim)
            myComm.AddParam("?AG", _SignedByName.Trim)
            myComm.AddParam("?AH", _PreparedByPosition.Trim)
            myComm.AddParam("?AI", _PreparedByName.Trim)

            _UpdateDate = GetCurrentTimeStamp()
            If Me.IsNew Then _InsertDate = _UpdateDate
            myComm.AddParam("?AJ", _UpdateDate.ToUniversalTime)

        End Sub


        Private Sub CheckIfUpdateDateChanged()

            Dim myComm As New SQLCommand("CheckIfWorkTimeSheetUpdateDateChanged")
            myComm.AddParam("?CD", _ID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 OrElse CDateTimeSafe(myData.Rows(0).Item(0), _
                    Date.MinValue) = Date.MinValue Then

                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                        My.Resources.Workers_WorkTimeSheet_TypeName, _ID.ToString))

                End If

                If CTimeStampSafe(myData.Rows(0).Item(0)) <> _UpdateDate Then

                    Throw New Exception(My.Resources.Common_UpdateDateHasChanged)

                End If

            End Using

        End Sub

#End Region

    End Class

End Namespace