Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="Goods.ProductionCalculation">goods production 
    ''' template (calculation)</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table kalkuliac.</remarks>
    <Serializable()> _
    Public NotInheritable Class ProductionCalculationInfo
        Inherits ReadOnlyBase(Of ProductionCalculationInfo)
        Implements IValueObject, IComparable

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Date As Date = Date.MinValue
        Private _IsObsolete As Boolean = False
        Private _Description As String = ""
        Private _GoodsID As Integer = 0
        Private _GoodsName As String = ""


        ''' <summary>
        ''' Gets whether an object is a place holder (does not represent a real goods production calculation).
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
        ''' Gets an ID of the production template that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the production template.
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.K_data.</remarks>
        Public ReadOnly Property [Date]() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _Date = Date.MinValue Then Return ""
                Return _Date.ToString("yyyy-MM-dd")
            End Get
        End Property

        ''' <summary>
        ''' Whether the production template is obsolete (no longer in use).
        ''' </summary>
        ''' <remarks>Value is stored in the database field kalkuliac.IsObsolete.</remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of the production template.
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.Description.</remarks>
        Public ReadOnly Property Description() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Description.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="Goods.GoodsItem.ID">ID of the goods</see> 
        ''' that are produced using the production template.
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.P_ID.</remarks>
        Public ReadOnly Property GoodsID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsID
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="Goods.GoodsItem.Name">name of the goods</see> 
        ''' that are produced using the production template.
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.P_ID.</remarks>
        Public ReadOnly Property GoodsName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsName.Trim
            End Get
        End Property


        Public Shared Operator =(ByVal a As ProductionCalculationInfo, ByVal b As ProductionCalculationInfo) As Boolean

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

        Public Shared Operator <>(ByVal a As ProductionCalculationInfo, ByVal b As ProductionCalculationInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As ProductionCalculationInfo, ByVal b As ProductionCalculationInfo) As Boolean

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

        Public Shared Operator <(ByVal a As ProductionCalculationInfo, ByVal b As ProductionCalculationInfo) As Boolean

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
            Dim tmp As ProductionCalculationInfo = TryCast(obj, ProductionCalculationInfo)
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
            Return String.Format(My.Resources.HelperLists_ProductionCalculationInfo_ToString, _
                _Date.ToString("yyyy-MM-dd"), _GoodsName, _Description)
        End Function

#End Region

#Region " Factory Methods "

        Private Shared _Empty As ProductionCalculationInfo = Nothing

        ''' <summary>
        ''' Gets an empty ProductionCalculationInfo (placeholder).
        ''' </summary>
        Public Shared Function Empty() As ProductionCalculationInfo
            If _Empty Is Nothing Then
                _Empty = New ProductionCalculationInfo
            End If
            Return _Empty
        End Function

        Friend Shared Function GetProductionCalculationInfo(ByVal dr As DataRow) As ProductionCalculationInfo
            Return New ProductionCalculationInfo(dr)
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
            _Date = CDateSafe(dr.Item(1), Date.MinValue)
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(2), 0))
            _Description = CStrSafe(dr.Item(3)).Trim
            _GoodsID = CIntSafe(dr.Item(4), 0)
            _GoodsName = CStrSafe(dr.Item(5)).Trim

        End Sub

#End Region

    End Class

End Namespace