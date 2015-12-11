﻿Namespace Assets

    ''' <summary>
    ''' Represents a collection of long term asset discard operations
    ''' that belong to a complex discard document.
    ''' </summary>
    ''' <remarks>Values are stored in the database table turtas_op.
    ''' Should only be used as a child of <see cref="ComplexOperationDiscard">ComplexOperationDiscard</see>.</remarks>
    <Serializable()> _
    Public Class OperationDiscardList
        Inherits BusinessListBase(Of OperationDiscardList, OperationDiscard)

#Region " Business Methods "

        Private _FinancialDataCanChange As Boolean = True
        Private _FinancialDataCanChangeExplanation As String = ""
        Private _ParentDate As Date = Today


        Friend ReadOnly Property FinancialDataCanChange() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FinancialDataCanChange
            End Get
        End Property

        Friend ReadOnly Property FinancialDataCanChangeExplanation() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FinancialDataCanChangeExplanation
            End Get
        End Property

        Friend ReadOnly Property ParentDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ParentDate
            End Get
        End Property


        Friend Sub SetParentDate(ByVal newDate As Date)
            Me.RaiseListChangedEvents = False
            For Each i As OperationDiscard In Me
                i.SetParentDate(newDate)
            Next
            _ParentDate = newDate
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        Friend Sub SetCommonAccountCosts(ByVal accountCosts As Long)
            Me.RaiseListChangedEvents = False
            For Each i As OperationDiscard In Me
                i.AccountCosts = accountCosts
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        Friend Function GetChronologyValidators() As IChronologicValidator()
            Dim result As New List(Of IChronologicValidator)
            For Each i As OperationDiscard In Me
                result.Add(i.ChronologyValidator)
            Next
            Return result.ToArray
        End Function

        Friend Function GetTotalCosts() As Double
            Dim result As Double = 0
            For Each i As OperationDiscard In Me
                result = CRound(result + i.Background.ChangeAssetValue)
            Next
            Return result
        End Function

        ''' <summary>
        ''' Adds items in the list to the current collection.
        ''' </summary>
        ''' <param name="list"></param>
        ''' <remarks>Should only be called by a parent complex discard document
        ''' because it does not handle CanChangeFinancialData and does not 
        ''' automaticaly initiate reloading of IChronologyValidator of the parent document.
        ''' (ListChanged event fired with type <see cref="ComponentModel.ListChangedType.Reset">ComponentModel.ListChangedType.Reset</see>,
        ''' not ItemAdded or ItemDeleted.</remarks>
        Friend Sub AddRange(ByVal list As OperationDiscardList)
            CheckNewListForConcurrentItems(list)
            Me.RaiseListChangedEvents = False
            For Each o As OperationDiscard In list
                Add(o.Clone())
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        ''' <summary>
        ''' Checks a new items list for concurrent items that already exists
        ''' in the document or were deleted from the document. Throws exception
        ''' if concurrent items are found or the new item list is null or empty. 
        ''' </summary>
        ''' <param name="list"></param>
        ''' <remarks></remarks>
        Private Sub CheckNewListForConcurrentItems(ByVal list As OperationDiscardList)

            If list Is Nothing OrElse list.Count < 1 Then
                Throw New Exception(My.Resources.Assets_OperationDiscardList_NewItemsListNull)
            End If

            Dim message As String = ""

            For Each newItem As OperationDiscard In list
                For Each existingItem As OperationDiscard In Me
                    If existingItem.AssetID = newItem.AssetID Then
                        message = AddWithNewLine(message, String.Format("{0} (ID={1})", _
                            existingItem.AssetName, existingItem.AssetID), False)
                        Exit For
                    End If
                Next
            Next

            For Each newItem As OperationDiscard In list
                For Each deletedItem As OperationDiscard In Me
                    If Not deletedItem.IsNew AndAlso deletedItem.AssetID = newItem.AssetID Then
                        message = AddWithNewLine(message, String.Format("{0} (ID={1})", _
                            deletedItem.AssetName, deletedItem.AssetID), False)
                        Exit For
                    End If
                Next
            Next

            If Not String.IsNullOrEmpty(message) Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationDiscardList_NewItemsListInvalid, _
                    vbCrLf, message))
            End If

        End Sub

        Friend Function GetTotalBookEntryList() As BookEntryInternalList

            Dim result As BookEntryInternalList = BookEntryInternalList.NewBookEntryInternalList(BookEntryType.Debetas)

            For Each o As OperationDiscard In Me
                result.AddRange(o.GetTotalBookEntryList())
            Next

            Return result

        End Function

        Friend Function GetInsertDate() As DateTime
            Dim result As DateTime = DateTime.MaxValue
            For Each o As OperationDiscard In Me
                If o.InsertDate < result Then result = o.InsertDate
            Next
            Return result
        End Function

        Friend Function GetUpdateDate() As DateTime
            Dim result As DateTime = DateTime.MinValue
            For Each o As OperationDiscard In Me
                If o.UpdateDate > result Then result = o.UpdateDate
            Next
            Return result
        End Function


        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)
            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            Return result
        End Function

        Public Function HasWarnings() As Boolean
            For Each i As OperationDiscard In Me
                If i.HasWarnings() Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a collection of new discard operations created for 
        ''' asset ID's specified. Use <see cref="ComplexOperationDiscard.AddRange">ComplexOperationDiscard.AddRange</see> 
        ''' method to add them to a complex discard document.
        ''' </summary>
        ''' <param name="assetIDs"></param>
        ''' <remarks></remarks>
        Public Shared Function NewOperationDiscardList(ByVal assetIDs As Integer()) As OperationDiscardList
            Return DataPortal.Fetch(Of OperationDiscardList)(New Criteria(assetIDs))
        End Function

        ''' <summary>
        ''' Gets a new empty OperationDiscardList instance to be added
        ''' to a new complex discard document.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function NewOperationDiscardList() As OperationDiscardList
            Return New OperationDiscardList
        End Function

        ''' <summary>
        ''' Gets an existing OperationDiscardList instance for
        ''' an old complex discard document.
        ''' </summary>
        ''' <param name="persistanceList">A list of <see cref="OperationPersistenceObject">OperationPersistenceObject</see>
        ''' that contains the operations data.</param>
        ''' <param name="generalData">A general asset data datasource for the 
        ''' <see cref="OperationBackground">OperationBackground</see> (could be null).</param>
        ''' <param name="deltaData">An asset delta data datasource for the 
        ''' <see cref="OperationBackground">OperationBackground</see> (could be null).</param>
        ''' <param name="parentValidator">A parent document's IChronologyValidator.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetOperationDiscardList( _
            ByVal persistanceList As List(Of OperationPersistenceObject), _
            ByVal generalData As DataTable, ByVal deltaData As DataTable, _
            ByVal parentValidator As SimpleChronologicValidator) As OperationDiscardList
            Return New OperationDiscardList(persistanceList, generalData, _
                deltaData, parentValidator)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal persistanceList As List(Of OperationPersistenceObject), _
            ByVal generalData As DataTable, ByVal deltaData As DataTable, _
            ByVal parentValidator As SimpleChronologicValidator)
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = True
            Fetch(persistanceList, generalData, deltaData, parentValidator)
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _IDs As Integer()
            Public ReadOnly Property IDs() As Integer()
                Get
                    Return _IDs
                End Get
            End Property
            Public Sub New(ByVal nIDs As Integer())
                _IDs = nIDs
            End Sub
        End Class


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not ComplexOperationDiscard.CanAddObject() Then
                Throw New System.Security.SecurityException( _
                    My.Resources.Common_SecuritySelectDenied)
            End If

            If criteria.IDs Is Nothing OrElse criteria.IDs.Length < 1 Then
                Throw New Exception(My.Resources.Assets_OperationDiscardList_AssetIDsNull)
            End If

            RaiseListChangedEvents = False

            For Each assetID As Integer In criteria.IDs
                MyBase.Add(OperationDiscard.NewOperationDiscard(assetID))
            Next

            RaiseListChangedEvents = True

        End Sub


        Private Sub Fetch(ByVal persistanceList As List(Of OperationPersistenceObject), _
            ByVal generalData As DataTable, ByVal deltaData As DataTable, _
            ByVal parentValidator As SimpleChronologicValidator)

            RaiseListChangedEvents = False

            _FinancialDataCanChange = parentValidator.FinancialDataCanChange
            _FinancialDataCanChangeExplanation = parentValidator.FinancialDataCanChangeExplanation
            _ParentDate = persistanceList(0).OperationDate

            For Each p As OperationPersistenceObject In persistanceList

                MyBase.Add(OperationDiscard.GetOperationDiscardChild( _
                    p, parentValidator, generalData, deltaData))

            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As ComplexOperationDiscard)

            RaiseListChangedEvents = False

            For Each o As OperationDiscard In Me.DeletedList
                If Not o.IsNew Then o.DeleteOperationDiscardChild()
            Next
            DeletedList.Clear()

            For Each i As OperationDiscard In Me
                i.SaveChild(parent.ID, parent.JournalEntryID, False)
            Next

            RaiseListChangedEvents = True

        End Sub


        Friend Sub CheckIfCanSave(ByVal parent As ComplexOperationDiscard)

            For Each o As OperationDiscard In Me.DeletedList
                If Not o.IsNew Then
                    o.CheckIfCanDeleteChild(parent.ChronologyValidator)
                End If
            Next

            For Each o As OperationDiscard In Me

                o.SetParentProperties(parent.DocumentNumber, parent.Content)

                If o.IsNew OrElse o.IsDirty Then
                    o.CheckIfCanSaveChild(parent.ChronologyValidator)
                End If

            Next

        End Sub

        Friend Sub CheckIfCanDelete(ByVal parentValidator As IChronologicValidator)

            For Each o As OperationDiscard In Me
                If Not o.ChronologyValidator.FinancialDataCanChange Then
                    Throw New Exception(String.Format( _
                        My.Resources.Assets_OperationDiscard_InvalidDelete, _
                        o.AssetName, vbCrLf, o.ChronologyValidator.FinancialDataCanChangeExplanation))
                End If
            Next

        End Sub

        Friend Sub DeleteChildren()

            For Each o As OperationDiscard In Me
                o.DeleteOperationDiscardChild()
            Next

        End Sub

#End Region

    End Class

End Namespace