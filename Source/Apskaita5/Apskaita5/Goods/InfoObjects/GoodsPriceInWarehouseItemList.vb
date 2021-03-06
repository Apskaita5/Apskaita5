﻿Imports ApskaitaObjects.My.Resources

Namespace Goods

    ''' <summary>
    ''' Represents a helper query object for a <see cref="GoodsComplexOperationPriceCut">
    ''' complex goods price cut operation</see>. Contains information about goods 
    ''' total amounts and their balance value in <see cref="Warehouse.WarehouseAccount">
    ''' warehouses</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class GoodsPriceInWarehouseItemList
        Inherits ReadOnlyListBase(Of GoodsPriceInWarehouseItemList, GoodsPriceInWarehouseItem)

#Region " Business Methods "

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.GoodsPriceInWarehouseItemList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new GoodsPriceInWarehouseItemList instance for the params specified.
        ''' </summary>
        ''' <param name="nDate">a date that the data should be fetched for, 
        ''' i.e. excluding goods transactions after the date</param>
        ''' <param name="params">parameters of the data (goods ID's and parent operation ID's)</param>
        ''' <remarks></remarks>
        Public Shared Function GetGoodsPriceInWarehouseItemList(ByVal nDate As Date, _
            ByVal params As GoodsPriceInWarehouseParam()) As GoodsPriceInWarehouseItemList
            Return DataPortal.Fetch(Of GoodsPriceInWarehouseItemList)(New Criteria(nDate, params))
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _Params As GoodsPriceInWarehouseParam()
            Private _Date As Date
            Public ReadOnly Property Params() As GoodsPriceInWarehouseParam()
                Get
                    Return _Params
                End Get
            End Property
            Public ReadOnly Property [Date]() As Date
                Get
                    Return _Date
                End Get
            End Property
            Public Sub New(ByVal nDate As Date, ByVal nParams As GoodsPriceInWarehouseParam())
                _Date = nDate
                _Params = nParams
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            If criteria.Params Is Nothing OrElse criteria.Params.Length < 1 Then
                Throw New ArgumentException(Goods_GoodsPriceInWarehouseItemList_ParamsNull)
            End If

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each param As GoodsPriceInWarehouseParam In criteria.Params
                Add(GoodsPriceInWarehouseItem.GetGoodsPriceInWarehouseItemChild( _
                    criteria.Date, param))
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace