﻿Imports ApskaitaObjects.My.Resources

Namespace Goods

    ''' <summary>
    ''' Represents a goods consignment (an amount of goods within a single goods purchase).
    ''' </summary>
    ''' <remarks>Consignments are created:
    ''' a) for each <see cref="GoodsOperationAcquisition">simple acquisition operation</see>;
    ''' b) for each operation that modifies a consignment, e.g. <see cref="GoodsOperationAdditionalCosts">
    ''' additional costs operation</see> discards an ""old"" consignment (the one 
    ''' that should increase in value) and inserts a new modified consignment (with 
    ''' an incresed value).
    ''' Values are stored in the database table consignments.</remarks>
    <Serializable()>
    Friend NotInheritable Class ConsignmentPersistenceObject
        Inherits BusinessBase(Of ConsignmentPersistenceObject)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _ParentID As Integer = 0
        Private _AcquisitionID As Integer = 0
        Private _WarehouseID As Integer = 0
        Private _Amount As Double = 0
        Private _UnitValue As Double = 0
        Private _TotalValue As Double = 0
        Private _AmountWithdrawn As Double = 0
        Private _TotalValueWithdrawn As Double = 0
        Private _AcquisitionDate As Date = Today


        ''' <summary>
        ''' Gets an ID of the consignment that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field consignments.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the parent goods operation.
        ''' </summary>
        ''' <remarks>Could be an <see cref="GoodsOperationAcquisition">acquisition operation</see>
        ''' or operation that modifies an existing consignment.
        ''' Value is stored in the database field consignments.ParentID.</remarks>
        Public ReadOnly Property ParentID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ParentID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the parent <see cref="GoodsOperationAcquisition">acquisition operation</see>.
        ''' </summary>
        ''' <remarks>Value is stored in the database field consignments.AcquisitionID.</remarks>
        Public ReadOnly Property AcquisitionID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _AcquisitionID
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="Warehouse.ID">ID of the warehouse</see>
        ''' that the consignment was acquired to.
        ''' </summary>
        ''' <remarks>Value is stored in the database field consignments.AcquisitionID.</remarks>
        Public ReadOnly Property WarehouseID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _WarehouseID
            End Get
        End Property


        Public Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_Amount, ROUNDAMOUNTGOODS)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_Amount, ROUNDAMOUNTGOODS) <> CRound(value, ROUNDAMOUNTGOODS) Then
                    _Amount = CRound(value, ROUNDAMOUNTGOODS)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a unit value of the goods in the consignment.
        ''' </summary>
        ''' <remarks>Value is stored in the database field consignments.UnitValue.</remarks>
        Public Property UnitValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_UnitValue, ROUNDUNITGOODS)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_UnitValue, ROUNDUNITGOODS) <> CRound(value, ROUNDUNITGOODS) Then
                    _UnitValue = CRound(value, ROUNDUNITGOODS)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a total value of the goods in the consignment.
        ''' </summary>
        ''' <remarks>Value is stored in the database field consignments.TotalValue.</remarks>
        Public Property TotalValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_TotalValue)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_TotalValue) <> CRound(value) Then
                    _TotalValue = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets an amount of goods that was withdrawn from the consignment
        ''' (by some other goods operations).
        ''' </summary>
        ''' <remarks>Value is fetched by a subquery.</remarks>
        Public ReadOnly Property AmountWithdrawn() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_AmountWithdrawn, ROUNDAMOUNTGOODS)
            End Get
        End Property

        ''' <summary>
        ''' Gets a remaining amount of the goods in the consignment.
        ''' (<see cref="Amount">Amount</see> minus <see cref="AmountWithdrawn">AmountWithdrawn</see>).
        ''' </summary>
        ''' <remarks>Value is calculated.</remarks>
        Public ReadOnly Property AmountLeft() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_Amount - _AmountWithdrawn, ROUNDAMOUNTGOODS)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total value of goods that were withdrawn from the consignment
        ''' (by some other goods operations).
        ''' </summary>
        ''' <remarks>Value is fetched by a subquery.</remarks>
        Public ReadOnly Property TotalValueWithdrawn() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_TotalValueWithdrawn)
            End Get
        End Property

        ''' <summary>
        ''' Gets a remaining total value of the goods in the consignment.
        ''' (<see cref="TotalValue">TotalValue</see> minus <see cref="TotalValueWithdrawn">TotalValueWithdrawn</see>).
        ''' </summary>
        ''' <remarks>Value is calculated.</remarks>
        Public ReadOnly Property TotalValueLeft() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_TotalValue - _TotalValueWithdrawn)
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the of the parent <see cref="GoodsOperationAcquisition">acquisition operation</see>.
        ''' </summary>
        ''' <remarks>Value is fetched by a subquery.</remarks>
        Public ReadOnly Property AcquisitionDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _AcquisitionDate
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(Goods_ConsignmentPersistenceObject_ToString,
                _ParentID.ToString, DblParser(_Amount, ROUNDAMOUNTGOODS),
                DblParser(_UnitValue, ROUNDUNITGOODS), DblParser(_TotalValue, 2),
                (_AcquisitionID > 0 AndAlso _ParentID <> _AcquisitionID).ToString,
                _ID.ToString)
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new ConsignmentPersistenceObject instance.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function NewConsignmentPersistenceObject() As ConsignmentPersistenceObject
            Return New ConsignmentPersistenceObject
        End Function

        Friend Shared Function NewConsignmentPersistenceObject(
            ByVal acquisitionConsignment As ConsignmentPersistenceObject,
            ByRef amountToWithdraw As Double) As ConsignmentPersistenceObject

            If Not CRound(amountToWithdraw, ROUNDAMOUNTGOODS) >= 0 Then
                Throw New ArgumentException(Goods_ConsignmentPersistenceObject_InvalidAmountToWithdraw)
            End If
            If acquisitionConsignment Is Nothing OrElse Not acquisitionConsignment._ID > 0 _
               OrElse Not acquisitionConsignment._ParentID > 0 Then
                Throw New ArgumentNullException(Goods_ConsignmentPersistenceObject_InvalidParentAcquisitionConsignment)
            End If

            Dim result As New ConsignmentPersistenceObject

            If acquisitionConsignment._AcquisitionID > 0 Then
                result._AcquisitionID = acquisitionConsignment._AcquisitionID
            Else
                result._AcquisitionID = acquisitionConsignment._ID
            End If

            result._UnitValue = acquisitionConsignment.UnitValue

            If CRound(amountToWithdraw, ROUNDAMOUNTGOODS) >= acquisitionConsignment.AmountLeft Then
                result._Amount = acquisitionConsignment.AmountLeft
                result._TotalValue = acquisitionConsignment.TotalValueLeft
                amountToWithdraw = CRound(amountToWithdraw - acquisitionConsignment.AmountLeft, ROUNDAMOUNTGOODS)
            Else
                result._Amount = CRound(amountToWithdraw, ROUNDAMOUNTGOODS)
                result._TotalValue = CRound(CRound(amountToWithdraw, ROUNDAMOUNTGOODS) _
                    * acquisitionConsignment.UnitValue, 2)
                amountToWithdraw = 0
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a new ConsignmentPersistenceObject instance that is
        ''' going to be a modified consignment (inserted instead a replaceable concidnment).
        ''' </summary>
        ''' <param name="consignmentModification">a consignment modification operation</param>
        ''' <remarks>E.g. <see cref="GoodsOperationAdditionalCosts">
        ''' additional costs operation</see> uses a<see cref="ConsignmentItem">
        ''' consignment modification operations</see> to discard ""old"" consignments 
        ''' (the ones that should increase in value) and to insert new modified consignments 
        ''' (with an incresed value).</remarks>
        Friend Shared Function NewConsignmentPersistenceObject(
            ByVal consignmentModification As ConsignmentItem) As ConsignmentPersistenceObject

            If consignmentModification Is Nothing OrElse Not consignmentModification.ID > 0 _
                OrElse Not consignmentModification.ParentID > 0 Then
                Throw New ArgumentNullException(Goods_ConsignmentPersistenceObject_InvalidParentAcquisitionConsignment)
            End If

            Dim result As New ConsignmentPersistenceObject

            If consignmentModification.AcquisitionID > 0 Then
                result._AcquisitionID = consignmentModification.AcquisitionID
            Else
                result._AcquisitionID = consignmentModification.ID
            End If
            result._Amount = consignmentModification.AmountLeft
            result._WarehouseID = consignmentModification.WarehouseID

            Return result

        End Function

        Friend Shared Function GetConsignmentPersistenceObject(ByVal dr As DataRow,
            ByVal isForUpdate As Boolean) As ConsignmentPersistenceObject
            Return New ConsignmentPersistenceObject(dr, isForUpdate)
        End Function

        ''' <summary>
        ''' Gets an existing ConsignmentPersistenceObject instance that is
        ''' a modified consignment (inserted instead a replaceable concidnment).
        ''' </summary>
        ''' <param name="consignmentModification">a consignment modification operation</param>
        ''' <param name="parentID">an <see cref="OperationPersistenceObject.ID">
        ''' ID of the parent goods operation</see> (an operation that is a parent
        ''' of the consignment modification operation)</param>
        ''' <remarks>E.g. <see cref="GoodsOperationAdditionalCosts">
        ''' additional costs operation</see> uses a<see cref="ConsignmentItem">
        ''' consignment modification operations</see> to discard ""old"" consignments 
        ''' (the ones that should increase in value) and to insert new modified consignments 
        ''' (with an incresed value).</remarks>
        Friend Shared Function GetConsignmentPersistenceObject(
            ByVal consignmentModification As ConsignmentItem, ByVal parentID As Integer) As ConsignmentPersistenceObject

            Dim result As New ConsignmentPersistenceObject

            If consignmentModification.AcquisitionID > 0 Then
                result._AcquisitionID = consignmentModification.AcquisitionID
            Else
                result._AcquisitionID = consignmentModification.ID
            End If
            result._Amount = consignmentModification.AmountLeft
            result._ID = consignmentModification.UpdatedConsignmentID
            result._ParentID = parentID
            result._WarehouseID = consignmentModification.WarehouseID

            Return result

        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal isForUpdate As Boolean)
            MarkAsChild()
            Fetch(dr, isForUpdate)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal isForUpdate As Boolean)

            _ID = CIntSafe(dr.Item(0), 0)
            _ParentID = CIntSafe(dr.Item(1), 0)
            _AcquisitionID = CIntSafe(dr.Item(2), 0)
            _WarehouseID = CIntSafe(dr.Item(3), 0)
            _Amount = CDblSafe(dr.Item(4), ROUNDAMOUNTGOODS, 0)
            _UnitValue = CDblSafe(dr.Item(5), ROUNDUNITGOODS, 0)
            _TotalValue = CDblSafe(dr.Item(6), 2, 0)
            _AmountWithdrawn = CDblSafe(dr.Item(7), ROUNDAMOUNTGOODS, 0)
            _TotalValueWithdrawn = CDblSafe(dr.Item(8), ROUNDAMOUNTGOODS, 0)
            _AcquisitionDate = CDateSafe(dr.Item(9), Today)

            If isForUpdate Then MarkOld()

        End Sub

        Friend Sub Insert(ByVal nparentID As Integer, ByVal parentWarehouseID As Integer)

            _ParentID = nparentID
            _WarehouseID = parentWarehouseID
            If Not _AcquisitionID > 0 Then _AcquisitionID = nparentID

            Dim myComm As New SQLCommand("InsertConsignmentPersistenceObject")
            AddWithParams(myComm)
            myComm.AddParam("?AA", _ParentID)
            myComm.AddParam("?AB", _AcquisitionID)

            myComm.Execute()

            _ID = Convert.ToInt32(myComm.LastInsertID)

            MarkOld()

        End Sub

        Friend Sub Update(ByVal parentWarehouseID As Integer)

            If Not IsNew AndAlso Not IsDirty Then Exit Sub

            _WarehouseID = parentWarehouseID

            Dim myComm As New SQLCommand("UpdateConsignmentPersistenceObject")
            myComm.AddParam("?CD", _ID)
            AddWithParams(myComm)

            myComm.Execute()

            MarkOld()

        End Sub

        Friend Sub DeleteSelf()
            DeleteSelf(_ID)
            MarkNew()
        End Sub

        Friend Shared Sub DeleteSelf(ByVal consignemntID As Integer)

            Dim myComm As New SQLCommand("DeleteConsignmentPersistenceObject")
            myComm.AddParam("?CD", consignemntID)

            myComm.Execute()

        End Sub

        Private Sub AddWithParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AC", _WarehouseID)
            myComm.AddParam("?AD", CRound(_Amount, ROUNDAMOUNTGOODS))
            myComm.AddParam("?AE", CRound(_UnitValue, ROUNDUNITGOODS))
            myComm.AddParam("?AF", CRound(_TotalValue))

        End Sub

#End Region

    End Class

End Namespace