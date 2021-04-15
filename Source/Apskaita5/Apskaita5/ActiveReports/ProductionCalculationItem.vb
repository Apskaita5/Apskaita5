Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents a goods production calculation report item. 
    ''' Contains information about a <see cref="Goods.ProductionCalculation">ProductionCalculation</see>.
    ''' </summary>
    ''' <remarks>Values are stored in the database tables kalkuliac and kalkuliac_d.</remarks>
    <Serializable()> _
    Public NotInheritable Class ProductionCalculationItem
        Inherits ReadOnlyBase(Of ProductionCalculationItem)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Amount As Double = 0
        Private _Date As Date = Today
        Private _IsObsolete As Boolean = False
        Private _Description As String = ""
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _GoodsID As Integer = 0
        Private _GoodsName As String = ""
        Private _GoodsMeasureUnit As String = ""
        Private _GoodsCode As String = ""
        Private _ComponentCount As Integer = 0
        Private _CostsSum As Double = 0


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
        ''' Gets a standard amount of the goods produced (components and costs 
        ''' amounts are set for this amount of the goods produced)
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.Vnt_sk.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Amount, ROUNDAMOUNTGOODS)
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the production template.
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.K_data.</remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
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
        ''' Gets the date and time when the production template was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in the database field kalkuliac.InsertDate.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the production template was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in the database field kalkuliac.UpdateDate.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
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

        ''' <summary>
        ''' Gets a <see cref="Goods.GoodsItem.MeasureUnit">measure unit of the goods</see> 
        ''' that are produced using the production template.
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.P_ID.</remarks>
        Public ReadOnly Property GoodsMeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsMeasureUnit.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="Goods.GoodsItem.InternalCode">code of the goods</see> 
        ''' that are produced using the production template.
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.P_ID.</remarks>
        Public ReadOnly Property GoodsCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a number of different components in the production template.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ComponentCount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComponentCount
            End Get
        End Property

        ''' <summary>
        ''' Gets a total production costs per <see cref="Amount">standard production 
        ''' amount</see> in the production template.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CostsSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CostsSum)
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_ProductionCalculationItem_ToString, _
                _Date.ToString("yyyy-MM-dd"), _GoodsName, _Description)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetProductionCalculationItem(ByVal dr As DataRow) As ProductionCalculationItem
            Return New ProductionCalculationItem(dr)
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
            _Amount = CDblSafe(dr.Item(1), ROUNDAMOUNTGOODS, 0)
            _Date = CDateSafe(dr.Item(2), Today)
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(3), 0))
            _Description = CStrSafe(dr.Item(4)).Trim
            _InsertDate = CTimeStampSafe(dr.Item(5))
            _UpdateDate = CTimeStampSafe(dr.Item(6))
            _GoodsID = CIntSafe(dr.Item(7), 0)
            _GoodsName = CStrSafe(dr.Item(8)).Trim
            _GoodsMeasureUnit = CStrSafe(dr.Item(9)).Trim
            _GoodsCode = CStrSafe(dr.Item(10)).Trim
            _ComponentCount = CIntSafe(dr.Item(11), 0)
            _CostsSum = CDblSafe(dr.Item(12), 2, 0)

        End Sub

#End Region

    End Class

End Namespace
