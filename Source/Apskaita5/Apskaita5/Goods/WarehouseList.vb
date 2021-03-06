﻿Namespace Goods

    ''' <summary>
    ''' Represents a goods warehouse list for the company.
    ''' </summary>
    ''' <remarks>Exists a single instance per company.
    ''' Values are stored in the database table warehouses.</remarks>
    <Serializable()> _
    Public NotInheritable Class WarehouseList
        Inherits BusinessListBase(Of WarehouseList, Warehouse)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid
            End Get
        End Property

        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Me.IsDirty
            End Get
        End Property


        Protected Overrides Function AddNewCore() As Object
            Dim newItem As Warehouse = Warehouse.NewWarehouse
            Me.Add(newItem)
            Return newItem
        End Function


        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = GetAllBrokenRulesForList(Me)
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = GetAllWarningsForList(Me)
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            For Each i As Warehouse In Me
                If i.HasWarnings() Then Return True
            Next
            Return False
        End Function


        Public Overrides Function Save() As WarehouseList

            Dim result As WarehouseList = MyBase.Save
            HelperLists.WarehouseInfoList.InvalidateCache()
            Return result

        End Function


        Protected Overrides Sub ClearItems()
            For Each w As Warehouse In Me
                If Not w.IsNew AndAlso w.ContainsGoods Then Throw New Exception(String.Format( _
                    My.Resources.Goods_WarehouseList_InvalidDelete, w.ToString))
            Next
            MyBase.ClearItems()
        End Sub

        Protected Overrides Sub RemoveItem(ByVal index As Integer)
            If Item(index).ContainsGoods Then Throw New Exception(String.Format( _
                My.Resources.Goods_WarehouseList_InvalidDelete, Item(index).ToString))
            MyBase.RemoveItem(index)
        End Sub

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.WarehouseList1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return False
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.WarehouseList3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return False
        End Function

#End Region

#Region " Factory Methods "

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of Warehouse) = Nothing

        ''' <summary>
        ''' Gets a current warehouse list from a database.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function GetWarehouseList() As WarehouseList
            Return DataPortal.Fetch(Of WarehouseList)(New Criteria())
        End Function

        ''' <summary>
        ''' Gets a sortable view of the warehouse list.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public Function GetSortedList() As Csla.SortedBindingList(Of Warehouse)
            If _SortedList Is Nothing Then
                _SortedList = New Csla.SortedBindingList(Of Warehouse)(Me)
            End If
            Return _SortedList
        End Function


        Private Sub New()
            ' require use of factory methods
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Public Sub New()
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchWarehouseList")

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                For Each dr As DataRow In myData.Rows
                    Add(Warehouse.GetWarehouse(dr))
                Next

                RaiseListChangedEvents = True

            End Using

        End Sub

        Protected Overrides Sub DataPortal_Update()

            If Not CanEditObject() Then
                Throw New System.Security.SecurityException(My.Resources.Common_SecurityUpdateDenied)
            End If

            Dim deletedCount As Integer = 0
            For Each w As Warehouse In Me.DeletedList
                If Not w.IsNew Then deletedCount += 1
            Next

            If Not Me.Count > 0 AndAlso Not deletedCount > 0 Then
                Throw New Exception(My.Resources.Goods_WarehouseList_ListEmpty)
            End If

            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            ReloadUsageData()

            For Each w As Warehouse In DeletedList
                If w.ContainsGoods Then Throw New Exception(String.Format( _
                    My.Resources.Goods_WarehouseList_InvalidDelete, w.ToString))
            Next

            Using transaction As New SqlTransaction

                Try

                    RaiseListChangedEvents = False

                    For Each w As Warehouse In DeletedList
                        If Not w.IsNew Then w.DeleteSelf()
                    Next
                    DeletedList.Clear()

                    For Each w As Warehouse In Me
                        If w.IsNew Then
                            w.Insert(Me)
                        ElseIf w.IsDirty Then
                            w.Update(Me)
                        End If
                    Next

                    RaiseListChangedEvents = True

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

        End Sub

        Private Sub ReloadUsageData()

            Dim myComm As New SQLCommand("FetchUsedWarehouses")

            Dim currentID As Integer
            Dim currentLastDate As Date

            Using myData As DataTable = myComm.Fetch

                For Each dr As DataRow In myData.Rows

                    currentID = CIntSafe(dr.Item(0), 0)
                    currentLastDate = CDateSafe(dr.Item(0), Date.MinValue)

                    For Each w As Warehouse In Me
                        w.ReloadUsageData(currentID, currentLastDate)
                    Next

                    For Each w As Warehouse In Me.DeletedList
                        w.ReloadUsageData(currentID, currentLastDate)
                    Next

                Next

            End Using

        End Sub

#End Region

    End Class

End Namespace