Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="Goods.GoodsGroup">custom goods group</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table prekes_gr.</remarks>
    <Serializable()> _
Public NotInheritable Class GoodsGroupInfo
        Inherits ReadOnlyBase(Of GoodsGroupInfo)
        Implements IValueObject, IComparable

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""


        ''' <summary>
        ''' Gets whether an object is a place holder (does not represent a real goods group).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObject.IsEmpty
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


        Public Shared Operator =(ByVal a As GoodsGroupInfo, ByVal b As GoodsGroupInfo) As Boolean

            Dim aId, bId As Integer
            If a Is Nothing OrElse a.IsEmpty Then
                aId = 0
            Else
                aId = a.ID
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bId = 0
            Else
                bId = b.ID
            End If

            Return aId = bId

        End Operator

        Public Shared Operator <>(ByVal a As GoodsGroupInfo, ByVal b As GoodsGroupInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As GoodsGroupInfo, ByVal b As GoodsGroupInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString > bToString

        End Operator

        Public Shared Operator <(ByVal a As GoodsGroupInfo, ByVal b As GoodsGroupInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString < bToString

        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
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

        Private Shared _Empty As GoodsGroupInfo = Nothing

        ''' <summary>
        ''' Gets an empty GoodsGroupInfo (placeholder).
        ''' </summary>
        Public Shared Function Empty() As GoodsGroupInfo
            If _Empty Is Nothing Then
                _Empty = New GoodsGroupInfo
            End If
            Return _Empty
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
