Imports ApskaitaObjects.Workers
Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents a bonus info data item for calculating VDU (average wage).
    ''' </summary>
    ''' <remarks>Should only be used as a child of BonusVDUInfoList.</remarks>
    <Serializable()> _
    Public NotInheritable Class BonusVDUInfo
        Inherits ReadOnlyBase(Of BonusVDUInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Year As Integer = 0
        Private _Month As Integer = 0
        Private _BonusType As BonusType = BonusType.k
        Private _BonusTypeHumanReadable As String = ""
        Private _BonusAmount As Double = 0


        ''' <summary>
        ''' Year which the bonus is calculated in.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
        End Property

        ''' <summary>
        ''' Month which the bonus is calculated in.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
        End Property

        ''' <summary>
        ''' Type of the bonus.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property BonusType() As BonusType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BonusType
            End Get
        End Property

        ''' <summary>
        ''' Type of the bonus as humanreadable string.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property BonusTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BonusTypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Amount of the bonus.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property BonusAmount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BonusAmount)
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_BonusVDUInfo_ToString, _
                _Year.ToString, _Month.ToString, _BonusTypeHumanReadable, _
                DblParser(_BonusAmount), GetCurrentCompany().BaseCurrency)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetBonusVDUInfo(ByVal dr As DataRow) As BonusVDUInfo
            Return New BonusVDUInfo(dr)
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

            _Year = CIntSafe(dr.Item(0), 0)
            _Month = CIntSafe(dr.Item(1), 0)
            _BonusType = Utilities.ConvertDatabaseCharID(Of BonusType)(CStrSafe(dr.Item(2)))
            _BonusTypeHumanReadable = Utilities.ConvertLocalizedName(_BonusType)
            _BonusAmount = CDblSafe(dr.Item(3), 2, 0)

        End Sub

#End Region

    End Class

End Namespace