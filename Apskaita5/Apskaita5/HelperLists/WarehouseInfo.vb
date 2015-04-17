Namespace HelperLists

    <Serializable()> _
    Public Class WarehouseInfo
        Inherits ReadOnlyBase(Of WarehouseInfo)
        Implements IValueObjectIsEmpty

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _IsProduction As Boolean = False
        Private _IsObsolete As Boolean = False
        Private _WarehouseAccount As Long = 0


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

        Public ReadOnly Property IsProduction() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsProduction
            End Get
        End Property

        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        Public ReadOnly Property WarehouseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseAccount
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            If _IsProduction Then Return _Name & " : " & _WarehouseAccount.ToString & "(gamyba)"
            Return _Name & " : " & _WarehouseAccount.ToString
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewWarehouseInfo() As WarehouseInfo
            Return New WarehouseInfo
        End Function

        Friend Shared Function GetWarehouseInfo(ByVal dr As DataRow, ByVal offset As Integer) As WarehouseInfo
            Return New WarehouseInfo(dr, offset)
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
            If Not _ID > 0 Then Exit Sub
            _Name = CStrSafe(dr.Item(1 + offset)).Trim
            _IsProduction = ConvertDbBoolean(CIntSafe(dr.Item(2 + offset), 0))
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(3 + offset), 0))
            _WarehouseAccount = CLongSafe(dr.Item(4 + offset), 0)

        End Sub

#End Region

    End Class

End Namespace