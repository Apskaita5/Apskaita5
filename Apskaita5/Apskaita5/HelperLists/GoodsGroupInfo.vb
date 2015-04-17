Namespace HelperLists

    <Serializable()> _
Public Class GoodsGroupInfo
        Inherits ReadOnlyBase(Of GoodsGroupInfo)
        Implements IValueObjectIsEmpty

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""


        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObjectIsEmpty.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ID > 0
            End Get
        End Property

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


        Public Overloads Shared Operator =(ByVal Obj1 As GoodsGroupInfo, _
            ByVal Obj2 As GoodsGroupInfo) As Boolean
            Return Obj1.ID = Obj2.ID
        End Operator

        Public Overloads Shared Operator <>(ByVal Obj1 As GoodsGroupInfo, _
            ByVal Obj2 As GoodsGroupInfo) As Boolean
            Return Obj1.ID <> Obj2.ID
        End Operator

        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return _Name.Trim
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetGoodsGroupInfo(ByVal dr As DataRow, _
            Optional ByVal Offset As Integer = 0) As GoodsGroupInfo
            Return New GoodsGroupInfo(dr, Offset)
        End Function

        Friend Shared Function GetEmptyGoodsGroupInfo() As GoodsGroupInfo
            Return New GoodsGroupInfo()
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal Offset As Integer)
            Fetch(dr, Offset)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal Offset As Integer)
            _ID = CIntSafe(dr.Item(Offset), 0)
            _Name = CStrSafe(dr.Item(Offset + 1)).Trim
        End Sub

#End Region

    End Class

End Namespace