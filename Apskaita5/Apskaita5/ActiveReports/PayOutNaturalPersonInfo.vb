Namespace ActiveReports

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


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
        End Property

        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        Public ReadOnly Property DocNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocNumber.Trim
            End Get
        End Property

        Public ReadOnly Property PersonInfo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonInfo.Trim
            End Get
        End Property

        Public ReadOnly Property PersonCodeSODRA() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonCodeSODRA.Trim
            End Get
        End Property

        Public ReadOnly Property SumNeto() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumNeto)
            End Get
        End Property

        Public ReadOnly Property SumBruto() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumBruto)
            End Get
        End Property

        Public ReadOnly Property RateGPM() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateGPM)
            End Get
        End Property

        Public ReadOnly Property RatePSDForPerson() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RatePSDForPerson)
            End Get
        End Property

        Public ReadOnly Property RatePSDForCompany() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RatePSDForCompany)
            End Get
        End Property

        Public ReadOnly Property SODRABase() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SODRABase
            End Get
        End Property

        Public ReadOnly Property RateSODRAForPerson() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateSODRAForPerson)
            End Get
        End Property

        Public ReadOnly Property RateSODRAForCompany() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateSODRAForCompany)
            End Get
        End Property

        Public ReadOnly Property CodeVMI() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CodeVMI
            End Get
        End Property

        Public ReadOnly Property CodeSODRA() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CodeSODRA
            End Get
        End Property

        Public ReadOnly Property DeductionGPM() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionGPM)
            End Get
        End Property

        Public ReadOnly Property DeductionPSD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionPSD)
            End Get
        End Property

        Public ReadOnly Property DeductionSODRA() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionSODRA)
            End Get
        End Property

        Public ReadOnly Property ContributionPSD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ContributionPSD)
            End Get
        End Property

        Public ReadOnly Property ContributionSODRA() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ContributionSODRA)
            End Get
        End Property

        Public ReadOnly Property TotalPSD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalPSD)
            End Get
        End Property

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
            If Not _ID > 0 Then Return ""
            Return _Content
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
            _CodeVMI = CIntSafe(dr.Item(15), 0)
            _CodeSODRA = CIntSafe(dr.Item(16), 0)
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