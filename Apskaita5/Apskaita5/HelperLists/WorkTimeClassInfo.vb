Imports ApskaitaObjects.Workers
Namespace HelperLists

    <Serializable()> _
    Public Class WorkTimeClassInfo
        Inherits ReadOnlyBase(Of WorkTimeClassInfo)
        Implements IComparable

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Code As String = ""
        Private _Name As String = ""
        Private _Type As WorkTimeType = WorkTimeType.OtherIncluded
        Private _TypeHumanReadable As String = ""
        Private _InclusionPercentage As Double = 100
        Private _SpecialWageShemaApplicable As Boolean = False
        Private _SpecialWageShema As String = ""
        Private _WithoutWorkHours As Boolean = False
        Private _AlreadyIncludedInGeneralTime As Boolean = True


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Code() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Code.Trim
            End Get
        End Property

        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        Public ReadOnly Property [Type]() As WorkTimeType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeHumanReadable.Trim
            End Get
        End Property

        Public ReadOnly Property InclusionPercentage() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InclusionPercentage)
            End Get
        End Property

        Public ReadOnly Property SpecialWageShemaApplicable() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SpecialWageShemaApplicable
            End Get
        End Property

        Public ReadOnly Property SpecialWageShema() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SpecialWageShema.Trim
            End Get
        End Property

        Public ReadOnly Property WithoutWorkHours() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WithoutWorkHours
            End Get
        End Property

        Public ReadOnly Property AlreadyIncludedInGeneralTime() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AlreadyIncludedInGeneralTime
            End Get
        End Property

        Public ReadOnly Property GetMe() As WorkTimeClassInfo
            Get
                Return Me
            End Get
        End Property



        Public Shared Operator =(ByVal a As WorkTimeClassInfo, ByVal b As WorkTimeClassInfo) As Boolean
            If a Is Nothing AndAlso b Is Nothing Then Return True
            If a Is Nothing OrElse b Is Nothing Then Return False
            Return a.ID = b.ID
        End Operator

        Public Shared Operator <>(ByVal a As WorkTimeClassInfo, ByVal b As WorkTimeClassInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As WorkTimeClassInfo, ByVal b As WorkTimeClassInfo) As Boolean
            If a Is Nothing Then Return False
            If a IsNot Nothing And b Is Nothing Then Return True
            Return a.ToString > b.ToString
        End Operator

        Public Shared Operator <(ByVal a As WorkTimeClassInfo, ByVal b As WorkTimeClassInfo) As Boolean
            If a Is Nothing And b Is Nothing Then Return False
            If a Is Nothing Then Return True
            If b Is Nothing Then Return False
            Return a.ToString < b.ToString
        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer _
            Implements System.IComparable.CompareTo
            Dim tmp As WorkTimeClassInfo = TryCast(obj, WorkTimeClassInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Code
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewWorkTimeClassInfo() As WorkTimeClassInfo
            Return New WorkTimeClassInfo()
        End Function

        Friend Shared Function GetWorkTimeClassInfo(ByVal dr As DataRow, ByVal offset As Integer) As WorkTimeClassInfo
            Return New WorkTimeClassInfo(dr, offset)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal offset As Integer)
            Fetch(dr, offset)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal offset As Integer)

            _ID = CIntSafe(dr.Item(0 + offset), 0)
            _Code = CStrSafe(dr.Item(1 + offset)).Trim
            _Name = CStrSafe(dr.Item(2 + offset)).Trim
            _InclusionPercentage = CDblSafe(dr.Item(3 + offset), 2, 0)
            _Type = ConvertEnumDatabaseCode(Of WorkTimeType)(CIntSafe(dr.Item(4 + offset), 0))
            _TypeHumanReadable = ConvertEnumHumanReadable(_Type)
            _SpecialWageShemaApplicable = ConvertDbBoolean(CIntSafe(dr.Item(5 + offset), 0))
            _SpecialWageShema = CStrSafe(dr.Item(6 + offset)).Trim
            _WithoutWorkHours = ConvertDbBoolean(CIntSafe(dr.Item(7 + offset), 0))
            _AlreadyIncludedInGeneralTime = ConvertDbBoolean(CIntSafe(dr.Item(8 + offset), 0))

        End Sub

#End Region

    End Class

End Namespace