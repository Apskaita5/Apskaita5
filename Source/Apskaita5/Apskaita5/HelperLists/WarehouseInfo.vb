Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="Goods.Warehouse">goods warehouse</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table warehouses.</remarks>
    <Serializable()> _
    Public NotInheritable Class WarehouseInfo
        Inherits ReadOnlyBase(Of WarehouseInfo)
        Implements IValueObject, IComparable

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
            Implements IValueObject.IsEmpty
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

        Public Shared Operator <>(ByVal a As WarehouseInfo, ByVal b As WarehouseInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As WarehouseInfo, ByVal b As WarehouseInfo) As Boolean

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

        Public Shared Operator <(ByVal a As WarehouseInfo, ByVal b As WarehouseInfo) As Boolean

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
            Dim tmp As WarehouseInfo = TryCast(obj, WarehouseInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Friend Function GetValueObjectIdString() As String
            If Me.IsEmpty Then Return ""
            Return _ID.ToString(Globalization.CultureInfo.InvariantCulture)
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

        Private Shared _Empty As WarehouseInfo = Nothing

        ''' <summary>
        ''' Gets an empty WarehouseInfo (placeholder).
        ''' </summary>
        Public Shared Function Empty() As WarehouseInfo
            If _Empty Is Nothing Then
                _Empty = New WarehouseInfo
            End If
            Return _Empty
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
