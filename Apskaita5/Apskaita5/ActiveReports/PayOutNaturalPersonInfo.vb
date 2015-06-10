Namespace ActiveReports

    ''' <summary>
    ''' Represents readonly information about a <see cref="Workers.PayOutNaturalPerson">payment to a natural person</see>. Used in tax declarations.
    ''' </summary>
    ''' <remarks>Provides additional info on top of a <see cref="General.JournalEntry">JournalEntry</see>.
    ''' Values are stored in the database table d_kitos.</remarks>
    <Serializable()> _
    Public Class PayOutNaturalPersonInfo
        Inherits ReadOnlyBase(Of PayOutNaturalPersonInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _JournalEntryID As Integer = 0
        Private _Date As Date = Today
        Private _Content As String = ""
        Private _DocNumber As String = ""
        Private _PersonInfo As String = ""
        Private _PersonCodeSODRA As String = ""
        Private _SumNeto As Double = 0
        Private _SumBruto As Double = 0
        Private _RateGPM As Double = 0
        Private _RatePSDForPerson As Double = 0
        Private _RatePSDForCompany As Double = 0
        Private _SODRABase As Integer = 0
        Private _RateSODRAForPerson As Double = 0
        Private _RateSODRAForCompany As Double = 0
        Private _CodeVMI As String = ""
        Private _CodeSODRA As String = ""
        Private _DeductionGPM As Double = 0
        Private _DeductionPSD As Double = 0
        Private _DeductionSODRA As Double = 0
        Private _ContributionPSD As Double = 0
        Private _ContributionSODRA As Double = 0
        Private _TotalPSD As Double = 0
        Private _TotalSODRA As Double = 0


        ''' <summary>
        ''' Gets an ID of the payment info that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the <see cref="General.JournalEntry.ID">general ledger entry ID</see> for the payment info.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.BZ_ID.</remarks>
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
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets the description of the payment.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Content">JournalEntry.Content</see>.</remarks>
        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the document number of the payment.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.DocNumber">JournalEntry.DocNumber</see>.</remarks>
        Public ReadOnly Property DocNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocNumber.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the payment receiver name.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ActiveReports.JournalEntryInfo.Person">JournalEntryInfo.Person</see>.</remarks>
        Public ReadOnly Property PersonInfo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonInfo.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the payment receiver social security (SODRA) code.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.KodasSDA.</remarks>
        Public ReadOnly Property PersonCodeSODRA() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonCodeSODRA.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the total sum that was actualy payed to the receiver (i.e. excluding all the taxes).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.Suma.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property SumNeto() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumNeto)
            End Get
        End Property

        ''' <summary>
        ''' Gets the contractual sum of the payment (i.e. <see cref="SumNeto">SumNeto</see> plus the deducted taxes excluding taxes that are payed at the company's cost).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.SumaBruto.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property SumBruto() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumBruto)
            End Get
        End Property

        ''' <summary>
        ''' Gets the personal income tax (GPM) rate.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.Tar.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RateGPM() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateGPM)
            End Get
        End Property

        ''' <summary>
        ''' Gets the health insurance contribution (PSD) rate for the receiver (deducted from the contractual sum).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.TarPSDW.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RatePSDForPerson() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RatePSDForPerson)
            End Get
        End Property

        ''' <summary>
        ''' Gets the health insurance contribution (PSD) rate for the company (at the company's expense additionaly to the contractual sum).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.TarPSDE.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RatePSDForCompany() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RatePSDForCompany)
            End Get
        End Property

        ''' <summary>
        ''' Gets the percentage of the contractual sum that is taxable by the social security contributions (SODRA) rates.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.BaseSODRA.</remarks>
        Public ReadOnly Property SODRABase() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SODRABase
            End Get
        End Property

        ''' <summary>
        ''' Gets the social security contribution (SODRA) rate for the receiver (deducted from the contractual sum).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.TarSDW.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RateSODRAForPerson() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateSODRAForPerson)
            End Get
        End Property

        ''' <summary>
        ''' Gets the social security contribution (SODRA) rate for the company (at the company's expense additionaly to the contractual sum).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.TarSDE.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RateSODRAForCompany() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateSODRAForCompany)
            End Get
        End Property

        ''' <summary>
        ''' Gets the code of the payment according to the state tax inspectorate (VMI) clasification.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.Kodas.</remarks>
        Public ReadOnly Property CodeVMI() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CodeVMI
            End Get
        End Property

        ''' <summary>
        ''' Gets the code of the payment according to the social security (SODRA) clasification.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_kitos.KodasSD.</remarks>
        Public ReadOnly Property CodeSODRA() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CodeSODRA
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of personal income tax (GPM) deducted from the contractual sum.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DeductionGPM() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionGPM)
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of health insurance contributions deducted from the contractual sum.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DeductionPSD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionPSD)
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of social security contributions (SODRA) deducted from the contractual sum.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DeductionSODRA() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionSODRA)
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of health insurance contributions at the company's expense additionaly to the contractual sum.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property ContributionPSD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ContributionPSD)
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of social security contributions (SODRA) at the company's expense additionaly to the contractual sum.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property ContributionSODRA() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ContributionSODRA)
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of health insurance contributions.
        ''' </summary>
        ''' <remarks><see cref="DeductionPSD">DeductionPSD</see> plus <see cref="ContributionPSD">ContributionPSD</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalPSD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalPSD)
            End Get
        End Property

        ''' <summary>
        ''' Gets total amount of social security contributions (SODRA) deducted from the contractual sum.
        ''' </summary>
        ''' <remarks><see cref="DeductionSODRA">DeductionSODRA</see> plus <see cref="ContributionSODRA">ContributionSODRA</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalSODRA() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSODRA)
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Workers_PayOutNaturalPerson_ToString, _
                _Date.ToString("yyyy-MM-dd"), _PersonInfo, _Content)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetPayOutNaturalPersonInfo(ByVal dr As DataRow) As PayOutNaturalPersonInfo
            Return New PayOutNaturalPersonInfo(dr)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

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
            _CodeVMI = CStrSafe(dr.Item(15))
            _CodeSODRA = CStrSafe(dr.Item(16))
            _SODRABase = 100 - CIntSafe(dr.Item(17), 0)

            ' To support old version
            If Not CRound(_SumBruto) > 0 Then _SumBruto = CRound(100 * _SumNeto / (100 - _RateGPM))

            _DeductionGPM = CRound(_SumBruto * _RateGPM / 100)
            _DeductionPSD = CRound(_SumBruto * _SODRABase * _RatePSDForPerson / 100 / 100)
            _DeductionSODRA = CRound(_SumBruto * _SODRABase * _RateSODRAForPerson / 100 / 100)
            _ContributionPSD = CRound(_SumBruto * _SODRABase * _RatePSDForCompany / 100 / 100)
            _ContributionSODRA = CRound(_SumBruto * _SODRABase * _RateSODRAForCompany / 100 / 100)
            _TotalPSD = CRound(_DeductionPSD + _ContributionPSD)
            _TotalSODRA = CRound(_DeductionSODRA + _ContributionSODRA)

        End Sub

#End Region

    End Class

End Namespace