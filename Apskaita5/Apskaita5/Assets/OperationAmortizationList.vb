Namespace Assets

    ''' <summary>
    ''' Represents a collection of long term asset amortization (depreciation) operations
    ''' that belong to a complex amortization (depreciation) document.
    ''' </summary>
    ''' <remarks>Values are stored in the database table turtas_op.
    ''' Should only be used as a child of <see cref="ComplexOperationAmortization">ComplexOperationAmortization</see>.</remarks>
    <Serializable()> _
    Public Class OperationAmortizationList
        Inherits BusinessListBase(Of OperationAmortizationList, OperationAmortization)

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
            For Each i As OperationAmortization In Me
                i.SetParentDate(newDate)
            Next
            _ParentDate = newDate
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        Friend Sub SetCommonAccountCosts(ByVal accountCosts As Long)
            Me.RaiseListChangedEvents = False
            For Each i As OperationAmortization In Me
                If Not i.AccountCostsIsReadOnly Then i.AccountCosts = accountCosts
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        Friend Sub SetCalculations(ByVal calculationList As LongTermAssetAmortizationCalculationList, _
            ByRef warnings As String)

            If calculationList Is Nothing OrElse calculationList.Count < 1 Then
                Throw New Exception(My.Resources.Assets_OperationAmortizationList_CalculationsNull)
            End If

            warnings = ""

            If calculationList.Count <> Me.Count Then
                warnings = AddWithNewLine(warnings, String.Format( _
                    My.Resources.Assets_OperationAmortizationList_CalculationsCountMismatch, _
                    Me.Count.ToString(), calculationList.Count.ToString()), False)
            End If

            Dim successCount As Integer = 0
            Dim isFound As Boolean

            Me.RaiseListChangedEvents = False

            For Each calculationItem As LongTermAssetAmortizationCalculation In calculationList

                isFound = False

                For Each i As OperationAmortization In Me

                    If calculationItem.AssetID = i.AssetID Then
                        Try
                            i.SetAmortizationCalculation(calculationItem)
                            successCount += 1
                        Catch ex As Exception
                            warnings = AddWithNewLine(warnings, ex.Message, False)
                        End Try
                        isFound = True
                        Exit For
                    End If

                Next

                If Not isFound Then
                    warnings = AddWithNewLine(warnings, String.Format( _
                        My.Resources.Assets_OperationAmortizationList_AssetNotFoundForCalculation, _
                        calculationItem.AssetID.ToString()), False)
                End If

            Next

            Me.RaiseListChangedEvents = True
            Me.ResetBindings()

            If Not StringIsNullOrEmpty(warnings) Then
                If successCount > 0 Then
                    warnings = String.Format(My.Resources.Assets_OperationAmortizationList_CalculationImportWarning, _
                        successCount.ToString(), calculationList.Count.ToString(), vbCrLf, warnings)
                Else
                    Throw New Exception(String.Format( _
                        My.Resources.Assets_OperationAmortizationList_CalculationImportException, _
                        vbCrLf, warnings))
                End If
            End If

        End Sub

        Friend Sub SetCalculations(ByVal calculation As LongTermAssetAmortizationCalculation)

            Dim isFound As Boolean = False

            For Each i As OperationAmortization In Me

                If calculation.AssetID = i.AssetID Then
                    i.SetAmortizationCalculation(calculation)
                    isFound = True
                    Exit For
                End If

            Next

            If Not isFound Then
                Throw New Exception(String.Format( _
                    My.Resources.Assets_OperationAmortizationList_AssetNotFoundForCalculation, _
                    calculation.AssetID.ToString()))
            End If

        End Sub

        Friend Function GetChronologyValidators() As IChronologicValidator()
            Dim result As New List(Of IChronologicValidator)
            For Each i As OperationAmortization In Me
                result.Add(i.ChronologyValidator)
            Next
            Return result.ToArray
        End Function

        Friend Function GetTotalCosts() As Double
            Dim result As Double = 0
            For Each i As OperationAmortization In Me
                result = CRound(result + i.TotalValueChange)
            Next
            Return result
        End Function

        ''' <summary>
        ''' Adds items in the list to the current collection.
        ''' </summary>
        ''' <param name="list"></param>
        ''' <remarks>Should only be called by a parent complex amortization document
        ''' because it does not handle CanChangeFinancialData and does not 
        ''' automaticaly initiate reloading of IChronologyValidator of the parent document.
        ''' (ListChanged event fired with type <see cref="ComponentModel.ListChangedType.Reset">ComponentModel.ListChangedType.Reset</see>,
        ''' not ItemAdded or ItemDeleted.</remarks>
        Friend Sub AddRange(ByVal list As OperationAmortizationList)
            CheckNewListForConcurrentItems(list)
            Me.RaiseListChangedEvents = False
            For Each o As OperationAmortization In list
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
        Private Sub CheckNewListForConcurrentItems(ByVal list As OperationAmortizationList)

            If list Is Nothing OrElse list.Count < 1 Then
                Throw New Exception(My.Resources.Assets_OperationAmortizationList_NewItemsListNull)
            End If

            Dim message As String = ""

            For Each newItem As OperationAmortization In list
                For Each existingItem As OperationAmortization In Me
                    If existingItem.AssetID = newItem.AssetID Then
                        message = AddWithNewLine(message, String.Format("{0} (ID={1})", _
                            existingItem.AssetName, existingItem.AssetID), False)
                        Exit For
                    End If
                Next
            Next

            For Each newItem As OperationAmortization In list
                For Each deletedItem As OperationAmortization In Me
                    If Not deletedItem.IsNew AndAlso deletedItem.AssetID = newItem.AssetID Then
                        message = AddWithNewLine(message, String.Format("{0} (ID={1})", _
                            deletedItem.AssetName, deletedItem.AssetID), False)
                        Exit For
                    End If
                Next
            Next

            If Not String.IsNullOrEmpty(message) Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationAmortizationList_NewItemsListInvalid, _
                    vbCrLf, message))
            End If

        End Sub

        Friend Function GetTotalBookEntryList() As BookEntryInternalList

            Dim result As BookEntryInternalList = BookEntryInternalList.NewBookEntryInternalList(BookEntryType.Debetas)

            For Each o As OperationAmortization In Me
                result.AddRange(o.GetTotalBookEntryList())
            Next

            Return result

        End Function

        Friend Function GetInsertDate() As DateTime
            Dim result As DateTime = DateTime.MaxValue
            For Each o As OperationAmortization In Me
                If o.InsertDate < result Then result = o.InsertDate
            Next
            Return result
        End Function

        Friend Function GetUpdateDate() As DateTime
            Dim result As DateTime = DateTime.MinValue
            For Each o As OperationAmortization In Me
                If o.UpdateDate > result Then result = o.UpdateDate
            Next
            Return result
        End Function


        Public Shadows Sub Clear()

            If Not _FinancialDataCanChange Then
                Throw New Exception(String.Format( _
                    "Klaida. Koreguoti dokumento finansinius duomenis, įskaitant eilučių pridėjimą ar ištrynimą, neleidžiama:{0}{1}", _
                    vbCrLf, _FinancialDataCanChangeExplanation))
            End If

            For Each i As OperationAmortization In Me
                If Not i.ChronologyValidator.FinancialDataCanChange Then
                    Throw New Exception(String.Format( _
                        "Klaida. Ilgalaikio turto '{0}' amortizacijos paskaičiavimo operacijos pašalinti neleidžiama.", _
                        i.AssetName))
                End If
            Next

            MyBase.Clear()

        End Sub


        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)
            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            Return result
        End Function

        Public Function HasWarnings() As Boolean
            For Each i As OperationAmortization In Me
                If i.HasWarnings() Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a collection of new amortization operations created for 
        ''' asset ID's specified. Use <see cref="ComplexOperationAmortization.AddRange">
        ''' ComplexOperationAmortization.AddRange</see> method to add them 
        ''' to a complex amortization document.
        ''' </summary>
        ''' <param name="assetIDs"><see cref="LongTermAsset.ID">an array of long term asset ID's
        ''' that the operation list should be fetched for</see></param>
        ''' <param name="parentValidator">a chronologic validator of a parent document
        ''' that the operation list should be fetched for</param>
        ''' <remarks></remarks>
        Public Shared Function NewOperationAmortizationList(ByVal assetIDs As Integer(), _
            ByVal parentValidator As IChronologicValidator) As OperationAmortizationList
            Return DataPortal.Fetch(Of OperationAmortizationList) _
                (New Criteria(assetIDs, parentValidator))
        End Function

        ''' <summary>
        ''' Gets a new empty OperationAmortizationList instance to be added
        ''' to a new complex amortization document.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function NewOperationAmortizationList() As OperationAmortizationList
            Return New OperationAmortizationList
        End Function

        ''' <summary>
        ''' Gets an existing OperationAmortizationList instance for
        ''' an old complex amortization document.
        ''' </summary>
        ''' <param name="persistanceList">A list of <see cref="OperationPersistenceObject">OperationPersistenceObject</see>
        ''' that contains the operations data.</param>
        ''' <param name="generalData">A general asset data datasource for the 
        ''' <see cref="OperationBackground">OperationBackground</see> (could be null).</param>
        ''' <param name="deltaData">An asset delta data datasource for the 
        ''' <see cref="OperationBackground">OperationBackground</see> (could be null).</param>
        ''' <param name="parentValidator">A parent document's IChronologyValidator.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetOperationAmortizationList( _
            ByVal persistanceList As List(Of OperationPersistenceObject), _
            ByVal generalData As DataTable, ByVal deltaData As DataTable, _
            ByVal parentValidator As SimpleChronologicValidator) As OperationAmortizationList
            Return New OperationAmortizationList(persistanceList, generalData, _
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
            Private _ParentValidator As IChronologicValidator
            Public ReadOnly Property IDs() As Integer()
                Get
                    Return _IDs
                End Get
            End Property
            Public ReadOnly Property ParentValidator() As IChronologicValidator
                Get
                    Return _ParentValidator
                End Get
            End Property
            Public Sub New(ByVal nIDs As Integer(), ByVal nParentValidator As IChronologicValidator)
                _IDs = nIDs
                _ParentValidator = nParentValidator
            End Sub
        End Class


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not ComplexOperationAmortization.CanAddObject() Then
                Throw New System.Security.SecurityException( _
                    My.Resources.Common_SecuritySelectDenied)
            End If

            If criteria.IDs Is Nothing OrElse criteria.IDs.Length < 1 Then
                Throw New Exception(My.Resources.Assets_OperationAmortizationList_AssetIDsNull)
            End If

            RaiseListChangedEvents = False

            For Each assetID As Integer In criteria.IDs
                Add(OperationAmortization.NewOperationAmortizationChild(assetID, _
                    criteria.ParentValidator, True))
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

                MyBase.Add(OperationAmortization.GetOperationAmortizationChild( _
                    p, parentValidator, generalData, deltaData))

            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As ComplexOperationAmortization)

            RaiseListChangedEvents = False

            For Each o As OperationAmortization In Me.DeletedList
                If Not o.IsNew Then o.DeleteOperationAmortizationChild()
            Next
            DeletedList.Clear()

            For Each i As OperationAmortization In Me
                i.SaveChild(parent.ID, parent.JournalEntryID, False)
            Next

            RaiseListChangedEvents = True

        End Sub


        Friend Sub CheckIfCanSave(ByVal parent As ComplexOperationAmortization)

            For Each o As OperationAmortization In Me.DeletedList
                If Not o.IsNew Then
                    o.CheckIfCanDeleteChild(parent.ChronologyValidator)
                End If
            Next

            For Each o As OperationAmortization In Me

                o.SetParentProperties(parent.DocumentNumber, parent.Content)

                If o.IsNew OrElse o.IsDirty Then
                    o.CheckIfCanSaveChild(parent.ChronologyValidator)
                End If

            Next

        End Sub

        Friend Sub CheckIfCanDelete(ByVal parentValidator As IChronologicValidator)

            For Each o As OperationAmortization In Me
                If Not o.ChronologyValidator.FinancialDataCanChange Then
                    Throw New Exception(String.Format( _
                        My.Resources.Assets_OperationAmortization_InvalidDelete, _
                        o.AssetName, vbCrLf, o.ChronologyValidator.FinancialDataCanChangeExplanation))
                End If
            Next

        End Sub

        Friend Sub DeleteChildren()

            For Each o As OperationAmortization In Me
                o.DeleteOperationAmortizationChild()
            Next

        End Sub

#End Region

    End Class

End Namespace