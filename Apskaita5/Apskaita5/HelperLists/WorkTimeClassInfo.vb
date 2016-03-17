Imports ApskaitaObjects.Workers
Namespace HelperLists

    ''' <summary>
    ''' Represents an immutable value object for work (or rest) time class (type).
    ''' </summary>
    ''' <remarks>Corresponds to <see cref="Workers.WorkTimeClass">Workers.WorkTimeClass</see>.
    ''' Values are stored in the database table worktimeclasss.</remarks>
    <Serializable()> _
    Public Class WorkTimeClassInfo
        Inherits ReadOnlyBase(Of WorkTimeClassInfo)
        Implements IComparable, IValueObjectIsEmpty

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Code As String = ""
        Private _Name As String = ""
        Private _Type As WorkTimeType = WorkTimeType.OtherExcluded
        Private _TypeHumanReadable As String = ""
        Private _InclusionPercentage As Double = 100
        Private _SpecialWageShemaApplicable As Boolean = False
        Private _SpecialWageShema As String = ""
        Private _WithoutWorkHours As Boolean = True
        Private _AlreadyIncludedInGeneralTime As Boolean = True


        ''' <summary>
        ''' Whether the instance is only a placeholder.
        ''' </summary>
        ''' <value></value>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObjectIsEmpty.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ID > 0
            End Get
        End Property

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
        ''' Gets a code of the work (or rest) time class item.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.Code.</remarks>
        Public ReadOnly Property Code() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Code.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the work (or rest) time class item.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.Name.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="WorkTimeType">general type</see> of the work (or rest) time class item.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.TypeID.</remarks>
        Public ReadOnly Property [Type]() As WorkTimeType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="WorkTimeType">general type</see> of the work (or rest) time class item as a human readable (localized) string.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.TypeID.</remarks>
        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a percentage of the time to include into general work time. 
        ''' E.g. beeing on watch at home is considered 50 percent of work time.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.InclusionPercentage.</remarks>
        Public ReadOnly Property InclusionPercentage() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InclusionPercentage)
            End Get
        End Property

        ''' <summary>
        ''' Gets whether a specific formula is used for calculating wage for this type of work time.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.SpecialWageShemaApplicable.</remarks>
        Public ReadOnly Property SpecialWageShemaApplicable() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SpecialWageShemaApplicable
            End Get
        End Property

        ''' <summary>
        ''' Gets a specific formula that is used for calculating wage for this type of work time.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.SpecialWageShema.</remarks>
        Public ReadOnly Property SpecialWageShema() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _SpecialWageShema.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets whether this type of work (rest) time is only measured in days, not in hours.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.WithoutWorkHours.</remarks>
        Public ReadOnly Property WithoutWorkHours() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WithoutWorkHours
            End Get
        End Property

        ''' <summary>
        ''' Gets whether this type of work time is automaticaly included in general work time.
        ''' </summary>
        ''' <remarks>Value is stored in the database table worktimeclasss.AlreadyIncludedInGeneralTime.</remarks>
        Public ReadOnly Property AlreadyIncludedInGeneralTime() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AlreadyIncludedInGeneralTime
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
            _Type = Utilities.ConvertDatabaseID(Of WorkTimeType)(CIntSafe(dr.Item(4 + offset), 0))
            _TypeHumanReadable = Utilities.ConvertLocalizedName(_Type)
            _SpecialWageShemaApplicable = ConvertDbBoolean(CIntSafe(dr.Item(5 + offset), 0))
            _SpecialWageShema = CStrSafe(dr.Item(6 + offset)).Trim
            _WithoutWorkHours = ConvertDbBoolean(CIntSafe(dr.Item(7 + offset), 0))
            _AlreadyIncludedInGeneralTime = ConvertDbBoolean(CIntSafe(dr.Item(8 + offset), 0))

        End Sub

#End Region

    End Class

End Namespace