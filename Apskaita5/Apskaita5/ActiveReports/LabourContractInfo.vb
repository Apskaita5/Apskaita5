Imports ApskaitaObjects.Workers
Namespace ActiveReports

    <Serializable()> _
    Public Class LabourContractInfo
        Inherits ReadOnlyBase(Of LabourContractInfo)

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _Code As String = ""
        Private _CodeSD As String = ""
        Private _ContractNumber As String = ""
        Private _HasContract As Boolean = False
        Private _ContractDate As Date = Date.MinValue
        Private _Position As String = ""
        Private _Wage As Double = 0
        Private _WageType As String = ""
        Private _HolidayNorm As Integer = 0
        Private _NPD As Double = 0
        Private _PNPD As Double = 0
        Private _ExtraPay As Double = 0
        Private _WorkLoad As Double = 0


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        Public ReadOnly Property Code() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Code.Trim
            End Get
        End Property

        Public ReadOnly Property CodeSD() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CodeSD.Trim
            End Get
        End Property

        Public ReadOnly Property ContractNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContractNumber.Trim
            End Get
        End Property

        Public ReadOnly Property HasContract() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HasContract
            End Get
        End Property

        Public ReadOnly Property ContractDate() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _ContractDate = Date.MinValue Then Return ""
                Return _ContractDate.ToShortDateString
            End Get
        End Property

        Public ReadOnly Property Position() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Position.Trim
            End Get
        End Property

        Public ReadOnly Property Wage() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Wage)
            End Get
        End Property

        Public ReadOnly Property WageType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WageType.Trim
            End Get
        End Property

        Public ReadOnly Property HolidayNorm() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayNorm
            End Get
        End Property

        Public ReadOnly Property NPD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_NPD)
            End Get
        End Property

        Public ReadOnly Property PNPD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PNPD)
            End Get
        End Property

        Public ReadOnly Property ExtraPay() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ExtraPay)
            End Get
        End Property

        Public ReadOnly Property WorkLoad() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_WorkLoad, 4)
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            If String.IsNullOrEmpty(_Position.Trim) Then
                Return _Name & "(" & _Code & ")"
            Else
                Return _Position & " " & _Name & "(" & _Code & ")"
            End If
        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function GetLabourContractInfo(ByVal dr As DataRow) As LabourContractInfo
            Return New LabourContractInfo(dr)
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
            _Name = CStrSafe(dr.Item(1)).Trim
            _Code = CStrSafe(dr.Item(2)).Trim
            _CodeSD = CStrSafe(dr.Item(3)).Trim
            If CIntSafe(dr.Item(5), 0) > 0 Then
                _ContractNumber = CStrSafe(dr.Item(4)).Trim & CStrSafe(dr.Item(5)).Trim
                _HasContract = True
                _ContractDate = CStrSafe(dr.Item(6)).Trim
                _Position = CStrSafe(dr.Item(7)).Trim
                _Wage = CDblSafe(dr.Item(8), 2, 0)
                _WageType = ConvertEnumHumanReadable(ConvertEnumDatabaseStringCode(Of WageType) _
                    (CStrSafe(dr.Item(9)).Trim))
                _HolidayNorm = CIntSafe(dr.Item(10), 0)
                _NPD = CDblSafe(dr.Item(11), 2, 0)
                _PNPD = CDblSafe(dr.Item(12), 2, 0)
                _ExtraPay = CDblSafe(dr.Item(13), 2, 0)
                _WorkLoad = CDblSafe(dr.Item(14), 2, 0)
            End If

        End Sub

#End Region

    End Class

End Namespace