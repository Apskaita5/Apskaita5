﻿Imports ApskaitaObjects.My.Resources
Imports ApskaitaObjects.Attributes

Namespace Goods

    ''' <summary>
    ''' Represents a helper query object for a <see cref="GoodsOperationPriceCut">
    ''' goods price cut operation</see>. Contains information about a goods item
    ''' total amount and it's balance value in <see cref="Warehouse.WarehouseAccount">
    ''' warehouses</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class GoodsPriceInWarehouseItem
        Inherits ReadOnlyBase(Of GoodsPriceInWarehouseItem)

#Region " Business Methods "

        Private _GoodsID As Integer = 0
        Private _Date As Date = Today
        Private _AmountInWarehouseAccounts As Double = 0
        Private _TotalValueInWarehouseAccounts As Double = 0
        Private _TotalValueCurrentPriceCut As Double = 0


        ''' <summary>
        ''' Gets an <see cref="GoodsItem.ID">ID of the goods</see> that the 
        ''' data is provided for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property GoodsID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsID
            End Get
        End Property

        ''' <summary>
        ''' Gets a date that the data is provided for, i.e. excluding goods transactions
        ''' after the date.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a total amount of the goods in all the <see cref="Warehouse.WarehouseAccount">
        ''' warehouses</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountInWarehouseAccounts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountInWarehouseAccounts, ROUNDAMOUNTGOODS)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total balance value of the goods in all the <see cref="Warehouse.WarehouseAccount">
        ''' warehouses</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalValueInWarehouseAccounts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValueInWarehouseAccounts)
            End Get
        End Property

        ''' <summary>
        ''' Gets a current balance of the <see cref="GoodsItem.AccountValueReduction">
        ''' value reduction account</see> for the goods.
        ''' </summary>
        ''' <remarks>Positive value is for a credit balance, negative value (invalid) 
        ''' is for a debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalValueCurrentPriceCut() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValueCurrentPriceCut)
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _GoodsID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Goods_GoodsPriceInWarehouseItem_ToString, _
                _GoodsID.ToString, _Date.ToString("yyyy-MM-dd"), _
                DblParser(_AmountInWarehouseAccounts, ROUNDAMOUNTGOODS), _
                DblParser(_TotalValueInWarehouseAccounts, 2), _
                DblParser(_TotalValueCurrentPriceCut, 2))
        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.GoodsPriceInWarehouseItem1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new GoodsPriceInWarehouseItem instance for the params specified.
        ''' </summary>
        ''' <param name="nDate">a date that the data should be fetched for, 
        ''' i.e. excluding goods transactions after the date</param>
        ''' <param name="param">parameters of the data (goods ID and parent operation ID)</param>
        ''' <remarks></remarks>
        Public Shared Function GetGoodsPriceInWarehouseItem(ByVal nDate As Date, _
            ByVal param As GoodsPriceInWarehouseParam) As GoodsPriceInWarehouseItem
            Return DataPortal.Fetch(Of GoodsPriceInWarehouseItem)(New Criteria(nDate, param))
        End Function

        ''' <summary>
        ''' Gets a new GoodsPriceInWarehouseItem instance for the params specified
        ''' bypassing dataportal.
        ''' </summary>
        ''' <param name="nDate">a date that the data should be fetched for, 
        ''' i.e. excluding goods transactions after the date</param>
        ''' <param name="param">parameters of the data (goods ID and parent operation ID)</param>
        ''' <remarks>Should only be invoked on server side.</remarks>
        Friend Shared Function GetGoodsPriceInWarehouseItemChild(ByVal nDate As Date, _
            ByVal param As GoodsPriceInWarehouseParam) As GoodsPriceInWarehouseItem
            Return New GoodsPriceInWarehouseItem(nDate, param)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal nDate As Date, ByVal param As GoodsPriceInWarehouseParam)
            ' require use of factory methods
            DoFetch(nDate, param)
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _Param As GoodsPriceInWarehouseParam
            Private _Date As Date
            Public ReadOnly Property [Date]() As Date
                Get
                    Return _Date
                End Get
            End Property
            Public ReadOnly Property Param() As GoodsPriceInWarehouseParam
                Get
                    Return _Param
                End Get
            End Property
            Public Sub New(ByVal nDate As Date, ByVal nParam As GoodsPriceInWarehouseParam)
                _Param = nParam
                _Date = nDate
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)
            DoFetch(criteria.Date, criteria.Param)
        End Sub

        Private Sub DoFetch(ByVal nDate As Date, ByVal param As GoodsPriceInWarehouseParam)

            If param Is Nothing Then
                Throw New ArgumentNullException("param")
            End If

            If Not param.GoodsID > 0 Then
                Throw New ArgumentException(Goods_GoodsPriceInWarehouseItem_GoodsIdNull)
            End If

            Dim myComm As New SQLCommand("FetchGoodsPriceInWarehouseItem")
            myComm.AddParam("?GD", param.GoodsID)
            myComm.AddParam("?DT", nDate.Date)
            myComm.AddParam("?OD", param.ParentOperationID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Goods_GoodsItem_TypeName, _
                    param.GoodsID.ToString()))

                Dim dr As DataRow = myData.Rows(0)

                _GoodsID = param.GoodsID
                _Date = nDate

                _AmountInWarehouseAccounts = CDblSafe(dr.Item(0), ROUNDAMOUNTGOODS, 0)
                _TotalValueInWarehouseAccounts = CDblSafe(dr.Item(1), 2, 0)
                _TotalValueCurrentPriceCut = CDblSafe(dr.Item(2), 2, 0)

            End Using

        End Sub

#End Region

    End Class

End Namespace