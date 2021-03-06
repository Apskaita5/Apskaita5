﻿Imports ApskaitaObjects.Attributes

Namespace Workers

    ''' <summary>
    ''' Represents a work (or rest) time class (type).
    ''' </summary>
    ''' <remarks>Should only be used as a child of a <see cref="WorkTimeClassList">WorkTimeClassList</see>.
    ''' Values are stored in the database table worktimeclasss.</remarks>
    <Serializable()> _
    Public NotInheritable Class WorkTimeClass
        Inherits BusinessBase(Of WorkTimeClass)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Code As String = ""
        Private _Name As String = ""
        Private _Type As WorkTimeType = WorkTimeType.OtherIncluded
        Private _TypeHumanReadable As String = Utilities.ConvertLocalizedName(WorkTimeType.OtherIncluded)
        Private _InclusionPercentage As Double = 100
        Private _SpecialWageShemaApplicable As Boolean = False
        Private _SpecialWageShema As String = ""
        Private _WithoutWorkHours As Boolean = False
        Private _AlreadyIncludedInGeneralTime As Boolean = True
        Private _IsInUse As Boolean = False


        ''' <summary>
        ''' Gets an ID of the work (or rest) time class item that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a code of the work (or rest) time class item.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.Code.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 50)> _
        Public Property Code() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Code.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Code.Trim <> value.Trim Then
                    _Code = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a name of the work (or rest) time class item.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.Name.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
        Public Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Name.Trim <> value.Trim Then
                    _Name = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a <see cref="WorkTimeType">general type</see> of the work (or rest) time class item.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.TypeID.</remarks>
        Public Property [Type]() As WorkTimeType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As WorkTimeType)
                CanWriteProperty(True)
                If _Type <> value AndAlso Not _IsInUse Then
                    _Type = value
                    _TypeHumanReadable = Utilities.ConvertLocalizedName(_Type)
                    PropertyHasChanged()
                    PropertyHasChanged("TypeHumanReadable")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a <see cref="WorkTimeType">general type</see> of the work (or rest) time class item as a human readable (localized) string.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.TypeID.</remarks>
        <LocalizedEnumField(GetType(WorkTimeType), False, "")> _
        Public Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeHumanReadable.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                Dim enumValue As WorkTimeType = Utilities.ConvertLocalizedName(Of WorkTimeType)(value)
                If _Type <> enumValue AndAlso Not _IsInUse Then
                    _Type = enumValue
                    _TypeHumanReadable = Utilities.ConvertLocalizedName(_Type)
                    PropertyHasChanged()
                    PropertyHasChanged("Type")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a percentage of the time to include into general work time. 
        ''' E.g. beeing on watch at home is considered 50 percent of work time.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.InclusionPercentage.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2, True, 0.0, 100.0, True)> _
        Public Property InclusionPercentage() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InclusionPercentage)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_InclusionPercentage) <> CRound(value) AndAlso Not _IsInUse Then
                    _InclusionPercentage = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether a specific formula is used for calculating wage for this type of work time.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.SpecialWageShemaApplicable.</remarks>
        Public Property SpecialWageShemaApplicable() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SpecialWageShemaApplicable
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _SpecialWageShemaApplicable <> value Then
                    _SpecialWageShemaApplicable = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a specific formula that is used for calculating wage for this type of work time.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.SpecialWageShema.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
        Public Property SpecialWageShema() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SpecialWageShema.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _SpecialWageShema.Trim <> value.Trim Then
                    _SpecialWageShema = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether this type of work (rest) time is only measured in days, not in hours.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.WithoutWorkHours.</remarks>
        Public Property WithoutWorkHours() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WithoutWorkHours
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _WithoutWorkHours <> value Then
                    _WithoutWorkHours = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether this type of work time is automaticaly included in general work time.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.AlreadyIncludedInGeneralTime.</remarks>
        Public Property AlreadyIncludedInGeneralTime() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AlreadyIncludedInGeneralTime
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _AlreadyIncludedInGeneralTime <> value Then
                    _AlreadyIncludedInGeneralTime = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Whether this work time type is in use by a <see cref="WorkTimeSheet">WorkTimeSheet</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsInUse() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsInUse
            End Get
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
            Return String.Format(My.Resources.Workers_WorkTimeClass_ToString, _Code, _Name)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Code"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Name"))

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringValueUniqueInCollectionValidation, _
                New Csla.Validation.RuleArgs("Code"))

            ValidationRules.AddRule(AddressOf InclusionPercentageValidation, _
                New Validation.RuleArgs("InclusionPercentage"))
            ValidationRules.AddRule(AddressOf SpecialWageShemaValidation, _
                New Validation.RuleArgs("SpecialWageShema"))
            ValidationRules.AddRule(AddressOf WithoutWorkHoursValidation, _
                New Validation.RuleArgs("WithoutWorkHours"))
            ValidationRules.AddRule(AddressOf SpecialWageShemaApplicableValidation, _
                New Validation.RuleArgs("SpecialWageShemaApplicable"))

            ValidationRules.AddDependantProperty("Type", "InclusionPercentage", False)
            ValidationRules.AddDependantProperty("Type", "SpecialWageShemaApplicable", False)
            ValidationRules.AddDependantProperty("SpecialWageShemaApplicable", "SpecialWageShema", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring that the value of property InclusionPercentage is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function InclusionPercentageValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As WorkTimeClass = DirectCast(target, WorkTimeClass)

            If CRound(valObj._InclusionPercentage) > 100.0 OrElse CRound(valObj._InclusionPercentage) < 0.0 Then
                e.Description = My.Resources.Workers_WorkTimeClass_InclusionPercentageOutOfRange
                e.Severity = Validation.RuleSeverity.Error
                Return False
            ElseIf valObj._Type <> WorkTimeType.OtherIncluded AndAlso CRound(valObj._InclusionPercentage) <> 0.0 _
                AndAlso CRound(valObj._InclusionPercentage) <> 100.0 Then
                e.Description = My.Resources.Workers_WorkTimeClass_InclusionPercentageMustBeNull
                e.Severity = Validation.RuleSeverity.Error
                Return False
            ElseIf (valObj._Type = WorkTimeType.NightWork OrElse _
                valObj._Type = WorkTimeType.OvertimeWork OrElse _
                valObj._Type = WorkTimeType.PublicHolidaysAndRestDayWork OrElse _
                valObj._Type = WorkTimeType.UnusualWork OrElse _
                valObj._Type = WorkTimeType.DownTime) AndAlso _
                CRound(valObj._InclusionPercentage) <> 100.0 Then
                e.Description = String.Format(My.Resources.Workers_WorkTimeClass_InclusionPercentageConstant, (100).ToString)
                e.Severity = Validation.RuleSeverity.Error
                Return False
            ElseIf (valObj._Type = WorkTimeType.OtherExcluded OrElse _
                valObj._Type = WorkTimeType.Truancy OrElse _
                valObj._Type = WorkTimeType.AnnualHolidays OrElse _
                valObj._Type = WorkTimeType.OtherHolidays OrElse _
                valObj._Type = WorkTimeType.SickDays) AndAlso _
                CRound(valObj._InclusionPercentage) <> 0.0 Then
                e.Description = String.Format(My.Resources.Workers_WorkTimeClass_InclusionPercentageConstant, (0).ToString)
                e.Severity = Validation.RuleSeverity.Error
                Return False
            ElseIf valObj._Type = WorkTimeType.OtherIncluded AndAlso _
                Not CRound(valObj._InclusionPercentage) > 0.0 Then
                e.Description = My.Resources.Workers_WorkTimeClass_InclusionPercentageNull
                e.Severity = Validation.RuleSeverity.Error
                Return False
            ElseIf valObj._Type = WorkTimeType.OtherIncluded AndAlso _
                CRound(valObj._InclusionPercentage) > 100 Then
                e.Description = My.Resources.Workers_WorkTimeClass_InclusionPercentageOutOfRange
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the value of property SpecialWageShemaApplicable is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function SpecialWageShemaApplicableValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As WorkTimeClass = DirectCast(target, WorkTimeClass)

            If valObj._SpecialWageShemaApplicable AndAlso _
                (valObj._Type = WorkTimeType.AnnualHolidays OrElse _
                valObj._Type = WorkTimeType.NightWork OrElse _
                valObj._Type = WorkTimeType.OvertimeWork OrElse _
                valObj._Type = WorkTimeType.PublicHolidaysAndRestDayWork OrElse _
                valObj._Type = WorkTimeType.SickDays OrElse _
                valObj._Type = WorkTimeType.Truancy OrElse _
                valObj._Type = WorkTimeType.UnusualWork) Then

                e.Description = My.Resources.Workers_WorkTimeClass_SpecialWageShemaApplicableMustBeNull
                e.Severity = Validation.RuleSeverity.Error
                Return False

            ElseIf Not valObj._SpecialWageShemaApplicable AndAlso _
                valObj._Type = WorkTimeType.DownTime Then

                e.Description = My.Resources.Workers_WorkTimeClass_SpecialWageShemaApplicableNull
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the value of property SpecialWageShema is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function SpecialWageShemaValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As WorkTimeClass = DirectCast(target, WorkTimeClass)

            If valObj._SpecialWageShemaApplicable AndAlso StringIsNullOrEmpty(valObj._SpecialWageShema) Then

                e.Description = My.Resources.Workers_WorkTimeClass_SpecialWageShemaNull
                e.Severity = Validation.RuleSeverity.Error
                Return False

            ElseIf valObj._SpecialWageShemaApplicable Then

                Dim solver As New FormulaSolver
                solver.Param("VAL") = 8
                solver.Param("DIN") = 1
                solver.Param("DUV") = 6
                solver.Param("DUD") = 32
                solver.Param("VDD") = 32.32
                solver.Param("VDV") = 5.99
                If Not solver.Solved(valObj._SpecialWageShema.Trim, New Double) Then
                    e.Description = My.Resources.Workers_WorkTimeClass_SpecialWageShemaInvalid
                    e.Severity = Validation.RuleSeverity.Error
                    Return False
                End If

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the value of property WithoutWorkHours is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function WithoutWorkHoursValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As WorkTimeClass = DirectCast(target, WorkTimeClass)

            If valObj._Type <> WorkTimeType.AnnualHolidays AndAlso _
                valObj._Type <> WorkTimeType.OtherExcluded AndAlso _
                valObj._Type <> WorkTimeType.OtherHolidays AndAlso _
                valObj._Type <> WorkTimeType.SickDays AndAlso valObj._WithoutWorkHours Then

                e.Description = My.Resources.Workers_WorkTimeClass_WithoutWorkHoursNull
                e.Severity = Validation.RuleSeverity.Error
                Return False

            ElseIf (valObj._Type = WorkTimeType.AnnualHolidays OrElse _
                valObj._Type = WorkTimeType.OtherHolidays OrElse _
                valObj._Type = WorkTimeType.SickDays) AndAlso Not valObj._WithoutWorkHours Then

                e.Description = My.Resources.Workers_WorkTimeClass_WithoutWorkHoursMustBeNull
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

        Friend Shared Function NewWorkTimeClass() As WorkTimeClass
            Dim result As New WorkTimeClass
            result.ValidationRules.CheckRules()
            Return result
        End Function

        Friend Shared Function NewWorkTimeClass(ByVal line As String, ByVal delimiter As String) As WorkTimeClass
            Return New WorkTimeClass(line, delimiter)
        End Function

        Friend Shared Function GetWorkTimeClass(ByVal dr As DataRow) As WorkTimeClass
            Return New WorkTimeClass(dr)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal dr As DataRow)
            MarkAsChild()
            Fetch(dr)
        End Sub

        Private Sub New(ByVal line As String, ByVal delimiter As String)
            MarkAsChild()
            FetchDelimitedString(line, delimiter)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(0), 0)
            _Code = CStrSafe(dr.Item(1)).Trim
            _Name = CStrSafe(dr.Item(2)).Trim
            _InclusionPercentage = CDblSafe(dr.Item(3), 2, 0)
            _Type = Utilities.ConvertDatabaseID(Of WorkTimeType)(CIntSafe(dr.Item(4), 0))
            _TypeHumanReadable = Utilities.ConvertLocalizedName(_Type)
            _SpecialWageShemaApplicable = ConvertDbBoolean(CIntSafe(dr.Item(5), 0))
            _SpecialWageShema = CStrSafe(dr.Item(6)).Trim
            _WithoutWorkHours = ConvertDbBoolean(CIntSafe(dr.Item(7), 0))
            _AlreadyIncludedInGeneralTime = ConvertDbBoolean(CIntSafe(dr.Item(8), 0))
            _IsInUse = ConvertDbBoolean(CIntSafe(dr.Item(9), 0))

            MarkOld()

        End Sub

        Private Sub FetchDelimitedString(ByVal source As String, ByVal delimiter As String)

            If StringIsNullOrEmpty(source) Then
                Throw New ArgumentNullException("source")
            End If

            Dim line As String() = source.Split(New String() {delimiter}, StringSplitOptions.None)

            _Name = CStrSafe(line(0)).Trim
            _Code = CStrSafe(line(1)).Trim
            _Type = Utilities.ConvertDatabaseID(Of WorkTimeType)(CIntSafe(line(2), 0))
            _TypeHumanReadable = Utilities.ConvertLocalizedName(_Type)
            Try
                _InclusionPercentage = Double.Parse(line(3), System.Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
            End Try
            _WithoutWorkHours = ConvertDbBoolean(CIntSafe(line(4), 0))
            _AlreadyIncludedInGeneralTime = ConvertDbBoolean(CIntSafe(line(5), 0))
            _SpecialWageShemaApplicable = ConvertDbBoolean(CIntSafe(line(6), 0))
            If line.Length > 7 Then _SpecialWageShema = CStrSafe(line(7)).Trim

            ValidationRules.CheckRules()

        End Sub

        Friend Function GetDelimitedString(ByVal delimiter As String) As String

            Dim result As New List(Of String)
            result.Add(_Name.Trim)
            result.Add(_Code.Trim)
            result.Add(Utilities.ConvertDatabaseID(_Type).ToString)
            result.Add(_InclusionPercentage.ToString(System.Globalization.CultureInfo.InvariantCulture))
            result.Add(ConvertDbBoolean(_WithoutWorkHours).ToString)
            result.Add(ConvertDbBoolean(_AlreadyIncludedInGeneralTime).ToString)
            result.Add(ConvertDbBoolean(_SpecialWageShemaApplicable).ToString)
            result.Add(_SpecialWageShema.Trim)

            Return String.Join(delimiter, result.ToArray())

        End Function


        Friend Sub Insert(ByVal parent As WorkTimeClassList)

            Dim myComm As New SQLCommand("InsertWorkTimeClass")
            AddWithParams(myComm)

            myComm.Execute()

            _ID = Convert.ToInt32(myComm.LastInsertID)

            MarkOld()

        End Sub

        Friend Sub Update(ByVal parent As WorkTimeClassList)

            Dim myComm As SQLCommand
            If _IsInUse Then
                myComm = New SQLCommand("UpdateWorkTimeClassLimited")
            Else
                myComm = New SQLCommand("UpdateWorkTimeClass")
            End If
            myComm.AddParam("?CD", _ID)
            AddWithParams(myComm)

            myComm.Execute()

            MarkOld()

        End Sub

        Friend Sub DeleteSelf()

            Dim myComm As New SQLCommand("DeleteWorkTimeClass")
            myComm.AddParam("?CD", _ID)

            myComm.Execute()

            MarkNew()

        End Sub


        Private Sub AddWithParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AA", _Code.Trim)
            myComm.AddParam("?AB", _Name.Trim)
            If Not _IsInUse Then
                myComm.AddParam("?AC", CRound(_InclusionPercentage))
                myComm.AddParam("?AD", Utilities.ConvertDatabaseID(_Type))
                myComm.AddParam("?AE", ConvertDbBoolean(_SpecialWageShemaApplicable))
                myComm.AddParam("?AG", ConvertDbBoolean(_WithoutWorkHours))
                myComm.AddParam("?AH", ConvertDbBoolean(_AlreadyIncludedInGeneralTime))
            End If
            myComm.AddParam("?AF", _SpecialWageShema.Trim)

        End Sub

        Friend Sub CheckIfInUse(ByVal throwOnInUse As Boolean)

            If IsNew Then Exit Sub

            Dim myComm As New SQLCommand("CheckIfWorkTimeClassIsInUse")
            myComm.AddParam("?TD", _ID)

            Using myData As DataTable = myComm.Fetch
                _IsInUse = (myData.Rows.Count > 0 AndAlso ConvertDbBoolean(CIntSafe(myData.Rows(0).Item(0), 0)))
            End Using

            If throwOnInUse AndAlso _IsInUse Then Throw New Exception( _
                String.Format(My.Resources.Workers_WorkTimeClass_InvalidDelete, myComm.ToString()))

        End Sub

#End Region

    End Class

End Namespace