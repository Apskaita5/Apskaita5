Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="Goods.Warehouse">goods warehouse</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table warehouses.</remarks>
    <Serializable()> _
    Public Class WarehouseInfo
        Inherits ReadOnlyBase(Of WarehouseInfo)
        Implements IValueObjectIsEmpty, IComparable

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _IsProduction As Boolean = False
        Private _IsObsolete As Boolean = False
        Private _WarehouseAccount As Long = 0


        ''' <summary>
        ''' Gets whether an object is a place holder (does not represent a real warehouse).
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
        ''' Gets an ID of the warehouse that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Data is stored in database field warehouses.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the warehouse.
        ''' </summary>
        ''' <remarks>Data is stored in database field warehouses.Name.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Whether the warehouse is used in production for production components.
        ''' </summary>
        ''' <remarks>Data is stored in database field warehouses.IsProduction.</remarks>
        Public ReadOnly Property IsProduction() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsProduction
            End Get
        End Property

        ''' <summary>
        ''' Whether the warehouse data is only ment for historical purposes, 
        ''' not for use by the current operations.
        ''' </summary>
        ''' <remarks>Data is stored in database field warehouses.IsObsolete.</remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        ''' <summary>
        ''' Gets an inventory <see cref="General.Account.ID">account</see> of the warehouse.
        ''' </summary>
        ''' <remarks>Data is stored in database field warehouses.WarehouseAccount.</remarks>
        Public ReadOnly Property WarehouseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseAccount
            End Get
        End Property


        Public Shared Operator =(ByVal a As WarehouseInfo, ByVal b As WarehouseInfo) As Boolean
            If a Is Nothing AndAlso b Is Nothing Then Return True
            If a Is Nothing OrElse b Is Nothing Then Return False
            Return a.ID = b.ID
        End Operator

        Public Shared Operator <>(ByVal a As WarehouseInfo, ByVal b As WarehouseInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As WarehouseInfo, ByVal b As WarehouseInfo) As Boolean
            If a Is Nothing Then Return False
            If a IsNot Nothing And b Is Nothing Then Return True
            Return a.ToString > b.ToString
        End Operator

        Public Shared Operator <(ByVal a As WarehouseInfo, ByVal b As WarehouseInfo) As Boolean
            If a Is Nothing And b Is Nothing Then Return False
            If a Is Nothing Then Return True
            If b Is Nothing Then Return False
            Return a.ToString < b.ToString
        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer _
            Implements System.IComparable.CompareTo
            Dim tmp As WarehouseInfo = TryCast(obj, WarehouseInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            If _IsProduction Then
                Return String.Format(My.Resources.HelperLists_WarehouseInfo_ToStringProduction, _
                    _WarehouseAccount.ToString, _Name)
            End If
            Return String.Format(My.Resources.HelperLists_WarehouseInfo_ToString, _
                _WarehouseAccount.ToString, _Name)
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