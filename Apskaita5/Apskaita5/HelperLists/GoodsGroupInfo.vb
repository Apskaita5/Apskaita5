Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="Goods.GoodsGroup">custom goods group</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table prekes_gr.</remarks>
    <Serializable()> _
Public Class GoodsGroupInfo
        Inherits ReadOnlyBase(Of GoodsGroupInfo)
        Implements IValueObjectIsEmpty, IComparable

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""


        ''' <summary>
        ''' Gets whether an object is a place holder (does not represent a real goods group).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObjectIsEmpty.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ID > 0
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the custom goods group that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Data is stored in database field prekes_gr.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the custom goods group.
        ''' </summary>
        ''' <remarks>Data is stored in database field prekes_gr.Name.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property


        Public Overloads Shared Operator =(ByVal Obj1 As GoodsGroupInfo, _
            ByVal Obj2 As GoodsGroupInfo) As Boolean
            Return Obj1.ID = Obj2.ID
        End Operator

        Public Overloads Shared Operator <>(ByVal Obj1 As GoodsGroupInfo, _
            ByVal Obj2 As GoodsGroupInfo) As Boolean
            Return Obj1.ID <> Obj2.ID
        End Operator

        Public Shared Operator >(ByVal a As GoodsGroupInfo, ByVal b As GoodsGroupInfo) As Boolean
            If a Is Nothing Then Return False
            If a IsNot Nothing And b Is Nothing Then Return True
            Return a.ToString > b.ToString
        End Operator

        Public Shared Operator <(ByVal a As GoodsGroupInfo, ByVal b As GoodsGroupInfo) As Boolean
            If a Is Nothing And b Is Nothing Then Return False
            If a Is Nothing Then Return True
            If b Is Nothing Then Return False
            Return a.ToString < b.ToString
        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer _
            Implements System.IComparable.CompareTo
            Dim tmp As GoodsGroupInfo = TryCast(obj, GoodsGroupInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return _Name.Trim
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function EmptyGoodsGroupInfo() As GoodsGroupInfo
            Return New GoodsGroupInfo()
        End Function

        Friend Shared Function GetGoodsGroupInfo(ByVal dr As DataRow, _
            Optional ByVal offset As Integer = 0) As GoodsGroupInfo
            Return New GoodsGroupInfo(dr, offset)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal Offset As Integer)
            Fetch(dr, Offset)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal offset As Integer)
            _ID = CIntSafe(dr.Item(offset), 0)
            _Name = CStrSafe(dr.Item(offset + 1)).Trim
        End Sub

#End Region

    End Class

End Namespace