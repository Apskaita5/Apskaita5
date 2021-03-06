﻿Imports ApskaitaObjects.My.Resources
Imports ApskaitaObjects.Attributes

Namespace Goods

    ''' <summary>
    ''' Represents a calculation of total goods value to be discarded for a given amount.
    ''' (calculates value depending on the <see cref="GoodsItem.DefaultValuationMethod">
    ''' goods valuation method</see>.)
    ''' </summary>
    ''' <remarks>A query object that interacts with <see cref="ConsignmentPersistenceObjectList">ConsignmentPersistenceObjectList</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class GoodsCostItem
        Inherits ReadOnlyBase(Of GoodsCostItem)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _GoodsID As Integer = 0
        Private _WarehouseID As Integer = 0
        Private _Amount As Double = 0
        Private _UnitCosts As Double = 0
        Private _TotalCosts As Double = 0
        Private _ValuationMethod As GoodsValuationMethod = GoodsValuationMethod.FIFO
        Private _NotEnoughInStock As Boolean = False
        Private _CalculationDate As Date = Date.MaxValue


        ''' <summary>
        ''' Gets an <see cref="GoodsItem.ID">ID of the goods</see> that the calculation is for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property GoodsID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsID
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="Warehouse.ID">ID of the warehouse</see> that the calculation is for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WarehouseID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseID
            End Get
        End Property

        ''' <summary>
        ''' Gets an amount of the goods that the calculation is for.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Amount, ROUNDAMOUNTGOODS)
            End Get
        End Property

        ''' <summary>
        ''' Gets a calculated goods value per unit.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITGOODS)> _
        Public ReadOnly Property UnitCosts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitCosts, ROUNDUNITGOODS)
            End Get
        End Property

        ''' <summary>
        ''' Gets a calculated total goods value.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalCosts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalCosts)
            End Get
        End Property

        ''' <summary>
        ''' Gets a goods valuation method that the calculation is made by.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValuationMethod() As GoodsValuationMethod
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ValuationMethod
            End Get
        End Property

        ''' <summary>
        ''' Whether there is not enough goods in the warehouse.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property NotEnoughInStock() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NotEnoughInStock
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the calculation.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CalculationDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CalculationDate
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(Goods_GoodsCostItem_ToString, _
                _CalculationDate.ToString("yyyy-MM-dd"), _GoodsID.ToString, _
                _WarehouseID.ToString, Utilities.ConvertLocalizedName( _
                _ValuationMethod), DblParser(_Amount, ROUNDAMOUNTGOODS), _
                DblParser(_UnitCosts, ROUNDUNITGOODS), DblParser(_TotalCosts, 2))
        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.GoodsCostItem1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new GoodsCostItem instance (calculates goods value for the given parameters).
        ''' </summary>
        ''' <param name="param">parameters for the calculation</param>
        ''' <param name="throwOnNotEnoughInStock">whether to throw if there
        ''' is not enough goods in the warehouse</param>
        ''' <remarks></remarks>
        Public Shared Function GetGoodsCostItem(ByVal param As GoodsCostParam, _
            ByVal throwOnNotEnoughInStock As Boolean) As GoodsCostItem

            Dim result As GoodsCostItem = DataPortal.Fetch(Of GoodsCostItem) _
                (New Criteria(param))

            If throwOnNotEnoughInStock AndAlso result.NotEnoughInStock Then
                Throw New Exception(Goods_GoodsCostItem_NotEnoughInStockException)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a new GoodsCostItem instance (calculates goods value for the given parameters)
        ''' bypassing the dataportal.
        ''' </summary>
        ''' <param name="param">parameters for the calculation</param>
        ''' <remarks>Should only be invoked on server side.</remarks>
        Friend Shared Function GetGoodsCostItemChild(ByVal param As GoodsCostParam) As GoodsCostItem
            Return New GoodsCostItem(param)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal param As GoodsCostParam)
            Fetch(param)
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _Param As GoodsCostParam
            Public ReadOnly Property Param() As GoodsCostParam
                Get
                    Return _Param
                End Get
            End Property
            Public Sub New(ByVal nParam As GoodsCostParam)
                _Param = nParam
            End Sub
        End Class


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Fetch(criteria.Param)

        End Sub

        Private Sub Fetch(ByVal param As GoodsCostParam)

            If param Is Nothing Then
                Throw New ArgumentNullException("param", Goods_GoodsCostItem_ParamNull)
            End If
            If Not param.GoodsID > 0 Then
                Throw New ArgumentException(Goods_GoodsCostItem_GoodsIdNull)
            End If
            If Not param.WarehouseID > 0 Then
                Throw New ArgumentException(Goods_GoodsCostItem_WarehouseIdNull)
            End If

            _GoodsID = param.GoodsID
            _WarehouseID = param.WarehouseID
            _Amount = param.Amount
            _ValuationMethod = param.ValuationMethod
            _CalculationDate = param.CalculationDate

            If Not param.Amount > 0 Then Exit Sub

            If param.ValuationMethod = GoodsValuationMethod.Average Then

                Dim myComm As New SQLCommand("FetchGoodsCostItemAverage")
                myComm.AddParam("?DT", param.CalculationDate.Date)
                myComm.AddParam("?GD", param.GoodsID)
                myComm.AddParam("?WD", param.WarehouseID)
                If param.ConsignmentParentID > 0 Then
                    myComm.AddParam("?CD", param.ConsignmentParentID)
                Else
                    myComm.AddParam("?CD", -1)
                End If
                If param.DiscardParentID > 0 Then
                    myComm.AddParam("?OD", param.DiscardParentID)
                Else
                    myComm.AddParam("?OD", -1)
                End If

                Using myData As DataTable = myComm.Fetch

                    If myData.Rows.Count < 1 Then
                        _NotEnoughInStock = True
                        Exit Sub
                    End If

                    Dim dr As DataRow = myData.Rows(0)

                    If CDblSafe(dr.Item(0), ROUNDAMOUNTGOODS, 0) < param.Amount Then
                        _NotEnoughInStock = True
                        Exit Sub
                    End If

                    ' just in case
                    If CDblSafe(dr.Item(0), ROUNDAMOUNTGOODS, 0) > 0 Then
                        _UnitCosts = CRound(CDblSafe(dr.Item(1), 2, 0) / _
                            CDblSafe(dr.Item(0), ROUNDAMOUNTGOODS, 0), ROUNDUNITGOODS)
                    Else
                        _UnitCosts = 0
                    End If
                    _TotalCosts = CRound(_UnitCosts * param.Amount, 2)

                End Using

            Else

                Dim consignments As ConsignmentPersistenceObjectList = _
                    ConsignmentPersistenceObjectList.NewConsignmentPersistenceObjectList( _
                    param.GoodsID, param.WarehouseID, param.DiscardParentID, _
                    param.ConsignmentParentID, (param.ValuationMethod = GoodsValuationMethod.LIFO))

                consignments.RemoveLateEntries(param.CalculationDate)

                If consignments.GetTotalAmountLeft() < param.Amount Then
                    _NotEnoughInStock = True
                    Exit Sub
                End If

                Dim discards As ConsignmentDiscardPersistenceObjectList = _
                    ConsignmentDiscardPersistenceObjectList.NewConsignmentDiscardPersistenceObjectList( _
                    consignments, _Amount, "")

                _TotalCosts = discards.GetTotalValue
                _UnitCosts = CRound(_TotalCosts / _Amount, ROUNDUNITGOODS)

            End If

        End Sub

#End Region

    End Class

End Namespace