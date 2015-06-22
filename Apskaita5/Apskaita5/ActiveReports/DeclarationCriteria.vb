Imports Csla.Validation

Namespace ActiveReports

    <Serializable()> _
    Public Class DeclarationCriteria
        Inherits Csla.CriteriaBase

        Private _Date As Date = Today
        Private _DateFrom As Date = Today
        Private _DateTo As Date = Today
        Private _SodraDepartment As String = ""
        Private _SodraRate As Double = 0
        Private _SodraAccount As Long = 0
        Private _SodraAccount2 As Long = 0
        Private _Year As Integer = Today.Year
        Private _Quarter As Integer = Convert.ToInt32(Math.Ceiling(Today.Month / 3))
        Private _Month As Integer = Today.Month
        Private _MunicipalityCode As String = ""
        Private _DeclarationType As IDeclaration = Nothing


        Public Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                If _Date.Date <> value.Date Then
                    _Date = value.Date
                End If
            End Set
        End Property

        Public Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateFrom
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                If _DateFrom.Date <> value.Date Then
                    _DateFrom = value.Date
                End If
            End Set
        End Property

        Public Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateTo
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                If _DateTo.Date <> value.Date Then
                    _DateTo = value.Date
                End If
            End Set
        End Property

        <StringField(ValueRequiredLevel.Mandatory, 30)> _
        Public Property SodraDepartment() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SodraDepartment.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _SodraDepartment.Trim <> value.Trim Then
                    _SodraDepartment = value.Trim
                End If
            End Set
        End Property

        <DoubleField(ValueRequiredLevel.Recommended, False, 2, True, 0.0, 99.99, False)> _
        Public Property SodraRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SodraRate)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_SodraRate) <> CRound(value) Then
                    _SodraRate = CRound(value)
                End If
            End Set
        End Property

        <AccountField(ValueRequiredLevel.Mandatory, False, 4)> _
        Public Property SodraAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SodraAccount
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                If _SodraAccount <> value Then
                    _SodraAccount = value
                End If
            End Set
        End Property

        <AccountField(ValueRequiredLevel.Optional, False, 4)> _
        Public Property SodraAccount2() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SodraAccount2
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                If _SodraAccount2 <> value Then
                    _SodraAccount2 = value
                End If
            End Set
        End Property

        <IntegerField(ValueRequiredLevel.Mandatory, False, True, 1950, 2100, True)> _
        Public Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                If _Year <> value Then
                    _Year = value
                End If
            End Set
        End Property

        <IntegerField(ValueRequiredLevel.Mandatory, False, True, 1, 4, True)> _
        Public Property Quarter() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Quarter
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                If _Quarter <> value Then
                    _Quarter = value
                End If
            End Set
        End Property

        <IntegerField(ValueRequiredLevel.Mandatory, False, True, 1, 12, True)> _
        Public Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                If _Month <> value Then
                    _Month = value
                End If
            End Set
        End Property

        <StringField(ValueRequiredLevel.Mandatory, 30)> _
        Public Property MunicipalityCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MunicipalityCode.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _MunicipalityCode.Trim <> value.Trim Then
                    _MunicipalityCode = value.Trim
                End If
            End Set
        End Property

        Public Property DeclarationType() As IDeclaration
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DeclarationType
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As IDeclaration)
                If Not (_DeclarationType Is Nothing AndAlso value Is Nothing) _
                    AndAlso Not (Not _DeclarationType Is Nothing AndAlso Not value Is Nothing _
                    AndAlso _DeclarationType.Name = value.Name) Then
                    _DeclarationType = value
                End If
            End Set
        End Property


        Public Function IsValid() As Boolean
            If _DeclarationType Is Nothing Then Return False
            Return _DeclarationType.IsValid(Me)
        End Function

        Public Function HasWarnings() As Boolean
            If _DeclarationType Is Nothing Then Return False
            Return _DeclarationType.HasWarnings(Me)
        End Function

        Public Function GetAllErrors() As String
            If _DeclarationType Is Nothing Then Return My.Resources.ActiveReports_DeclarationCriteria_DeclarationTypeNull
            Return _DeclarationType.GetAllErrors(Me)
        End Function

        Public Function GetAllWarnings() As String
            If _DeclarationType Is Nothing Then Return ""
            Return _DeclarationType.GetAllWarnings(Me)
        End Function


        Friend Function TryValidateSodraDepartment(ByRef isWarning As Boolean, ByRef errorMessage As String) As Boolean

            Dim e As New Validation.RuleArgs("SodraDepartment")
            Dim result As Boolean = CommonValidation.StringFieldValidation(Me, e)
            errorMessage = e.Description
            isWarning = (e.Severity = RuleSeverity.Warning)
            Return result

        End Function

        Friend Function TryValidateMunicipalityCode(ByRef isWarning As Boolean, ByRef errorMessage As String) As Boolean

            Dim e As New Validation.RuleArgs("MunicipalityCode")
            Dim result As Boolean = CommonValidation.StringFieldValidation(Me, e)
            errorMessage = e.Description
            isWarning = (e.Severity = RuleSeverity.Warning)
            Return result

        End Function

        Friend Function TryValidateYear(ByRef isWarning As Boolean, ByRef errorMessage As String) As Boolean

            Dim e As New Validation.RuleArgs("Year")
            Dim result As Boolean = CommonValidation.IntegerFieldValidation(Me, e)
            errorMessage = e.Description
            isWarning = (e.Severity = RuleSeverity.Warning)
            Return result

        End Function

        Friend Function TryValidateQuarter(ByRef isWarning As Boolean, ByRef errorMessage As String) As Boolean

            Dim e As New Validation.RuleArgs("Quarter")
            Dim result As Boolean = CommonValidation.IntegerFieldValidation(Me, e)
            errorMessage = e.Description
            isWarning = (e.Severity = RuleSeverity.Warning)
            Return result

        End Function

        Friend Function TryValidateMonth(ByRef isWarning As Boolean, ByRef errorMessage As String) As Boolean

            Dim e As New Validation.RuleArgs("Month")
            Dim result As Boolean = CommonValidation.IntegerFieldValidation(Me, e)
            errorMessage = e.Description
            isWarning = (e.Severity = RuleSeverity.Warning)
            Return result

        End Function

        Friend Function TryValidateSodraAccount(ByRef isWarning As Boolean, ByRef errorMessage As String) As Boolean

            Dim e As New Validation.RuleArgs("SodraAccount")
            Dim result As Boolean = CommonValidation.AccountFieldValidation(Me, e)
            errorMessage = e.Description
            isWarning = (e.Severity = RuleSeverity.Warning)
            Return result

        End Function

        Friend Function TryValidateSodraAccount2(ByRef isWarning As Boolean, ByRef errorMessage As String) As Boolean

            Dim e As New Validation.RuleArgs("SodraAccount2")
            Dim result As Boolean = CommonValidation.AccountFieldValidation(Me, e)
            errorMessage = e.Description
            isWarning = (e.Severity = RuleSeverity.Warning)
            Return result

        End Function

        Friend Function TryValidateSodraRate(ByRef isWarning As Boolean, ByRef errorMessage As String) As Boolean

            Dim e As New Validation.RuleArgs("SodraRate")
            Dim result As Boolean = CommonValidation.DoubleFieldValidation(Me, e)
            errorMessage = e.Description
            isWarning = (e.Severity = RuleSeverity.Warning)
            Return result

        End Function


        Public Function NewDeclarationCriteria() As DeclarationCriteria
            Return New DeclarationCriteria()
        End Function

        Public Function NewDeclarationCriteria(ByVal nDate As Date, ByVal nDateFrom As Date, ByVal nDateTo As Date, _
            ByVal nDeclarationType As IDeclaration, ByVal nYear As Integer, ByVal nQuarter As Integer, _
            ByVal nMonth As Integer, ByVal nMunicipalityCode As String, ByVal nSodraDepartment As String, _
            ByVal nSodraAccount As Long, ByVal nSodraAccount2 As Long, ByVal nSodraRate As Double) As DeclarationCriteria
            Return New DeclarationCriteria(nDate, nDateFrom, nDateTo, nDeclarationType, nYear, nQuarter, _
                nMonth, nMunicipalityCode, nSodraDepartment, nSodraAccount, nSodraAccount2, nSodraRate)
        End Function


        Private Sub New()
            MyBase.New(GetType(Declaration))
        End Sub

        Private Sub New(ByVal nDate As Date, ByVal nDateFrom As Date, ByVal nDateTo As Date, _
            ByVal nDeclarationType As IDeclaration, ByVal nYear As Integer, ByVal nQuarter As Integer, _
            ByVal nMonth As Integer, ByVal nMunicipalityCode As String, ByVal nSodraDepartment As String, _
            ByVal nSodraAccount As Long, ByVal nSodraAccount2 As Long, ByVal nSodraRate As Double)

            MyBase.New(GetType(Declaration))

            _Date = nDate
            _DateFrom = nDateFrom
            _DateTo = nDateTo
            _DeclarationType = nDeclarationType
            _Month = nMonth
            _MunicipalityCode = nMunicipalityCode
            _Quarter = nQuarter
            _SodraAccount = nSodraAccount
            _SodraAccount2 = nSodraAccount2
            _SodraDepartment = nSodraDepartment
            _SodraRate = nSodraRate
            _Year = nYear

        End Sub

    End Class

End Namespace