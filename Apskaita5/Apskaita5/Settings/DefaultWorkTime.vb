Imports Csla.Validation
Imports ApskaitaObjects.Settings.XmlProxies

Namespace Settings

    ''' <summary>
    ''' Represents information about gauge work time amounts for a certain year
    ''' and a certain month.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="DefaultWorkTimeList">DefaultWorkTimeList</see>.
    ''' Persisted using xml proxies as a part of <see cref="CommonSettings">CommonSettings</see>.</remarks>
    <Serializable()> _
    Public Class DefaultWorkTime
        Inherits BusinessBase(Of DefaultWorkTime)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Year As Integer = 0
        Private _Month As Integer = 0
        Private _WorkDaysFor5WorkDayWeek As Integer = 0
        Private _WorkHoursFor5WorkDayWeek As Double = 0
        Private _WorkDaysFor6WorkDayWeek As Integer = 0
        Private _WorkHoursFor6WorkDayWeek As Double = 0


        ''' <summary>
        ''' Gets or sets a year of the data.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Mandatory, False, True, 1972, 2100, True)> _
        Public Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If _Year <> value Then
                    _Year = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a month of the data.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Mandatory, False, True, 1, 12, True)> _
        Public Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If _Month <> value Then
                    _Month = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a gauge amount of work days in month as applicable for 5 days work week.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Mandatory, False, True, 1, 31, True)> _
        Public Property WorkDaysFor5WorkDayWeek() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkDaysFor5WorkDayWeek
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If _WorkDaysFor5WorkDayWeek <> value Then
                    _WorkDaysFor5WorkDayWeek = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a gauge amount of work hours in month as applicable for 5 days work week.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, ROUNDWORKTIME, True, 0.01, 744.0, True)> _
        Public Property WorkHoursFor5WorkDayWeek() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_WorkHoursFor5WorkDayWeek, ROUNDWORKTIME)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_WorkHoursFor5WorkDayWeek, ROUNDWORKTIME) <> CRound(value, ROUNDWORKTIME) Then
                    _WorkHoursFor5WorkDayWeek = CRound(value, ROUNDWORKTIME)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a gauge amount of work days in month as applicable for 6 days work week.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Mandatory, False, True, 1, 31, True)> _
        Public Property WorkDaysFor6WorkDayWeek() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkDaysFor6WorkDayWeek
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If _WorkDaysFor6WorkDayWeek <> value Then
                    _WorkDaysFor6WorkDayWeek = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a gauge amount of work hours in month as applicable for 6 days work week.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, ROUNDWORKTIME, True, 0.01, 744.0, True)> _
        Public Property WorkHoursFor6WorkDayWeek() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_WorkHoursFor6WorkDayWeek, ROUNDWORKTIME)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_WorkHoursFor6WorkDayWeek, ROUNDWORKTIME) <> CRound(value, ROUNDWORKTIME) Then
                    _WorkHoursFor6WorkDayWeek = CRound(value, ROUNDWORKTIME)
                    PropertyHasChanged()
                End If
            End Set
        End Property



        Public Function HasWarnings() As Boolean
            Return (Me.BrokenRulesCollection.WarningCount > 0)
        End Function

        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            If IsValid Then Return ""
            Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(RuleSeverity.Error))
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If BrokenRulesCollection.WarningCount < 1 Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(RuleSeverity.Warning))
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Settings_DefaultWorkTime_ToString, _
                _Year.ToString, _Month.ToString, _WorkDaysFor5WorkDayWeek.ToString, _
                DblParser(_WorkHoursFor5WorkDayWeek, ROUNDWORKTIME), _
                _WorkDaysFor6WorkDayWeek.ToString, _
                DblParser(_WorkHoursFor6WorkDayWeek, ROUNDWORKTIME))
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.IntegerFieldValidation, _
                New RuleArgs("Year"))
            ValidationRules.AddRule(AddressOf CommonValidation.IntegerFieldValidation, _
                New RuleArgs("Month"))
            ValidationRules.AddRule(AddressOf CommonValidation.IntegerFieldValidation, _
                New RuleArgs("WorkDaysFor5WorkDayWeek"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New RuleArgs("WorkHoursFor5WorkDayWeek"))
            ValidationRules.AddRule(AddressOf CommonValidation.IntegerFieldValidation, _
                New RuleArgs("WorkDaysFor6WorkDayWeek"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New RuleArgs("WorkHoursFor6WorkDayWeek"))
        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function NewDefaultWorkTime() As DefaultWorkTime
            Dim result As New DefaultWorkTime
            result.ValidationRules.CheckRules()
            Return result
        End Function

        Friend Shared Function GetDefaultWorkTime(ByVal proxy As DefaultWorkTimeProxy) As DefaultWorkTime
            Return New DefaultWorkTime(proxy)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal proxy As DefaultWorkTimeProxy)
            MarkAsChild()
            Fetch(proxy)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal proxy As DefaultWorkTimeProxy)

            _Year = proxy.Year
            _Month = proxy.Month
            _WorkDaysFor5WorkDayWeek = proxy.WorkDaysFor5WorkDayWeek
            _WorkHoursFor5WorkDayWeek = proxy.WorkHoursFor5WorkDayWeek
            _WorkDaysFor6WorkDayWeek = proxy.WorkDaysFor6WorkDayWeek
            _WorkHoursFor6WorkDayWeek = proxy.WorkHoursFor6WorkDayWeek

            MarkOld()

        End Sub

        Friend Function GetProxy(ByVal markItemAsOld As Boolean) As DefaultWorkTimeProxy

            Dim result As New DefaultWorkTimeProxy

            result.Year = _Year
            result.Month = _Month
            result.WorkDaysFor5WorkDayWeek = _WorkDaysFor5WorkDayWeek
            result.WorkHoursFor5WorkDayWeek = _WorkHoursFor5WorkDayWeek
            result.WorkDaysFor6WorkDayWeek = _WorkDaysFor6WorkDayWeek
            result.WorkHoursFor6WorkDayWeek = _WorkHoursFor6WorkDayWeek

            If markItemAsOld Then MarkOld()

            Return result

        End Function

#End Region

    End Class

End Namespace