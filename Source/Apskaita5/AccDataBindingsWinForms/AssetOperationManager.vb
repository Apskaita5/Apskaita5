Imports ApskaitaObjects.Assets

''' <summary>
''' A helper class containing methods to access long term assets operations in a uniform way. 
''' </summary>
''' <remarks></remarks>
Public Class AssetOperationManager

    Private Shared _SimpleOperationTypes As List(Of TypeItem)
    Private Shared _ComplexOperationTypes As List(Of TypeItem)


    ''' <summary>
    ''' Gets a list of the available simple asset operations (that acts on a single asset).
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function GetSimpleOperationTypes() As List(Of TypeItem)
        If _SimpleOperationTypes Is Nothing Then InitializeSimpleOperationTypes()
        Return _SimpleOperationTypes
    End Function

    Private Shared Sub InitializeSimpleOperationTypes()

        _SimpleOperationTypes = New List(Of TypeItem)

        _SimpleOperationTypes.Add(New TypeItem(GetType(Assets.OperationAccountChange), _
            CommonValidation.GetResourcesTypeName(GetType(Assets.OperationAccountChange))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Assets.OperationAcquisitionValueIncrease), _
            CommonValidation.GetResourcesTypeName(GetType(Assets.OperationAcquisitionValueIncrease))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Assets.OperationAmortization), _
            CommonValidation.GetResourcesTypeName(GetType(Assets.OperationAmortization))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Assets.OperationAmortizationPeriodChange), _
            CommonValidation.GetResourcesTypeName(GetType(Assets.OperationAmortizationPeriodChange))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Assets.OperationDiscard), _
            CommonValidation.GetResourcesTypeName(GetType(Assets.OperationDiscard))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Assets.OperationOperationalStatusChange), _
            CommonValidation.GetResourcesTypeName(GetType(Assets.OperationOperationalStatusChange))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Assets.OperationTransfer), _
            CommonValidation.GetResourcesTypeName(GetType(Assets.OperationTransfer))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Assets.OperationValueChange), _
            CommonValidation.GetResourcesTypeName(GetType(Assets.OperationValueChange))))

    End Sub

    ''' <summary>
    ''' Gets a list of the available complex asset operations (that acts on multiple assets).
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function GetComplexOperationTypes() As List(Of TypeItem)
        If _ComplexOperationTypes Is Nothing Then InitializeComplexOperationTypes()
        Return _ComplexOperationTypes
    End Function

    Private Shared Sub InitializeComplexOperationTypes()

        _ComplexOperationTypes = New List(Of TypeItem)

        _ComplexOperationTypes.Add(New TypeItem(GetType(Assets.ComplexOperationAmortization), _
            CommonValidation.GetResourcesTypeName(GetType(Assets.ComplexOperationAmortization))))
        _ComplexOperationTypes.Add(New TypeItem(GetType(Assets.ComplexOperationDiscard), _
            CommonValidation.GetResourcesTypeName(GetType(Assets.ComplexOperationDiscard))))
        _ComplexOperationTypes.Add(New TypeItem(GetType(Assets.ComplexOperationOperationalStatusChange), _
            CommonValidation.GetResourcesTypeName(GetType(Assets.ComplexOperationOperationalStatusChange))))
        _ComplexOperationTypes.Add(New TypeItem(GetType(Assets.ComplexOperationValueChange), _
            CommonValidation.GetResourcesTypeName(GetType(Assets.ComplexOperationValueChange))))

    End Sub


    ''' <summary>
    ''' Creates a new asset operation and shows it in edit form.
    ''' </summary>
    ''' <param name="assetID">an <see cref="Assets.LongTermAsset.ID">ID of the assets</see>
    ''' that the new operation is for</param>
    ''' <param name="operationType">a type of the business object that represents an asset operation
    ''' (an asset operation edit form shall implement a constructor that accepts assetID)</param>
    ''' <remarks>an asset operation edit form shall implement a constructor that accepts assetID</remarks>
    Public Shared Sub StartNewAssetOperation(ByVal assetID As Integer, ByVal operationType As Type)
        Dim frm As Form = GetFormForBusinessType(operationType, assetID)
        OpenForm(frm)
    End Sub


    ''' <summary>
    ''' Fetches an asset operation by the encapsulated journal entry ID (should only be invoked by a 
    ''' <see cref="CslaActionExtenderQueryObject">query browser</see>).
    ''' </summary>
    ''' <param name="journalEntryId">an ID of the jousrnal entry to look the asset operation by</param>
    ''' <param name="journalEntryType">a type of the jousrnal entry to look the asset operation by</param>
    ''' <remarks></remarks>
    Public Shared Function GetAssetOperation(ByVal journalEntryId As Integer, ByVal journalEntryType As DocumentType) As Object
        Dim idInfo As OperationInfo = OperationInfo.GetOperationInfo(journalEntryId, OperationInfo.OperationInfoFetchType.JournalEntryID)
        Return GetAssetOperation(idInfo)
    End Function

    ''' <summary>
    ''' Fetches an asset operation using a helper info object (should only be invoked by a 
    ''' <see cref="CslaActionExtenderQueryObject">query browser</see>).
    ''' </summary>
    ''' <param name="operationInfo">a helper info object containing info about an asset operation</param>
    ''' <remarks></remarks>
    Public Shared Function GetAssetOperation(ByVal operationInfo As OperationInfo) As Object
        If operationInfo.ComplexOperationID > 0 Then
            Return GetAssetOperation(operationInfo.ComplexOperationID, operationInfo.Type, True, _
                operationInfo.JournalEntryID, operationInfo.JournalEntryType)
        Else
            Return GetAssetOperation(operationInfo.ID, operationInfo.Type, False, _
                operationInfo.JournalEntryID, operationInfo.JournalEntryType)
        End If
    End Function

    ''' <summary>
    ''' Fetches an asset operation using an assets operations report item (should only be invoked by a 
    ''' <see cref="CslaActionExtenderQueryObject">query browser</see>).
    ''' </summary>
    ''' <param name="operationInfo">an assets operations report item containing info about an asset operation</param>
    ''' <remarks></remarks>
    Public Shared Function GetAssetOperation(ByVal operationInfo As ActiveReports.LongTermAssetOperationInfo) As Object

        If operationInfo.ComplexActID > 0 Then
            Return GetAssetOperation(operationInfo.ComplexActID, operationInfo.Type, True, _
                operationInfo.AttachedJournalEntryID, DocumentType.None)
        Else
            Return GetAssetOperation(operationInfo.ID, operationInfo.Type, False, _
                operationInfo.AttachedJournalEntryID, DocumentType.None)
        End If

    End Function

    ''' <summary>
    ''' Fetches an asset operation using a given id params (should only be invoked by a 
    ''' <see cref="CslaActionExtenderQueryObject">query browser</see>).
    ''' </summary>
    ''' <param name="operationId">an ID of the simple asset operation (an ID of the complex asset operation
    ''' if operationIsComplex)</param>
    ''' <param name="operationType">a type of the simple asset operation</param>
    ''' <param name="operationIsComplex">whether the operation is complex</param>
    ''' <param name="journalEntryId">an ID of the encapsulated journal entry</param>
    ''' <param name="journalEntryType">a type of the encapsulated journal entry</param>
    ''' <remarks></remarks>
    Public Shared Function GetAssetOperation(ByVal operationId As Integer, ByVal operationType As LtaOperationType, _
        ByVal operationIsComplex As Boolean, ByVal journalEntryId As Integer, ByVal journalEntryType As DocumentType) As Object

        If Not operationId > 0 Then
            Throw New ArgumentNullException("operationId")
        End If

        If operationIsComplex Then

            Select Case operationType
                Case LtaOperationType.Amortization
                    Return ComplexOperationAmortization.GetComplexOperationAmortization(operationId, False)
                Case LtaOperationType.Discard
                    Return ComplexOperationDiscard.GetComplexOperationDiscard(operationId, False)
                Case LtaOperationType.UsingEnd, LtaOperationType.UsingStart
                    Return ComplexOperationOperationalStatusChange.GetComplexOperationOperationalStatusChange(operationId)
                Case LtaOperationType.ValueChange
                    Return ComplexOperationValueChange.GetComplexOperationValueChange(operationId, False)
                Case Else
                    Throw New NotImplementedException(String.Format("Klaida. Ilgalaikio turto kompleksinės operacijos tipas {0} neimplementuotas klasėje {1}.", _
                        operationType.ToString, GetType(GoodsOperationManager).FullName))
            End Select

        Else

            Dim invoiceChildTypes As LtaOperationType() = New LtaOperationType() _
                {LtaOperationType.AcquisitionValueIncrease, LtaOperationType.Transfer}

            If Not Array.IndexOf(invoiceChildTypes, operationType) < 0 AndAlso journalEntryId > 0 Then
                If journalEntryType = DocumentType.InvoiceMade Then
                    Return Documents.InvoiceMade.GetInvoiceMade(journalEntryId)
                ElseIf journalEntryType = DocumentType.InvoiceReceived Then
                    Return Documents.InvoiceReceived.GetInvoiceReceived(journalEntryId)
                End If
            End If

            Select Case operationType
                Case LtaOperationType.AccountChange
                    Return OperationAccountChange.GetOperationAccountChange(operationId, False)
                Case LtaOperationType.AcquisitionValueIncrease
                    Return OperationAcquisitionValueIncrease.GetOperationAcquisitionValueIncrease(operationId, False)
                Case LtaOperationType.Amortization
                    Return OperationAmortization.GetOperationAmortization(operationId, False)
                Case LtaOperationType.AmortizationPeriod
                    Return OperationAmortizationPeriodChange.GetOperationAmortizationPeriodChange(operationId)
                Case LtaOperationType.Discard
                    Return OperationDiscard.GetOperationDiscard(operationId, False)
                Case LtaOperationType.Transfer
                    Return OperationTransfer.GetOperationTransfer(operationId, False)
                Case LtaOperationType.UsingEnd, LtaOperationType.UsingStart
                    Return OperationOperationalStatusChange.GetOperationOperationalStatusChange(operationId)
                Case LtaOperationType.ValueChange
                    Return OperationValueChange.GetOperationValueChange(operationId, False)

                Case Else
                    Throw New NotImplementedException(String.Format("Klaida. Ilgalaikio turto operacijos tipas {0} neimplementuotas klasėje {1}.", _
                        operationType.ToString, GetType(GoodsOperationManager).FullName))
            End Select

        End If

    End Function


    ''' <summary>
    ''' Checks if the operation with the given id is currently open in any edit form.
    ''' </summary>
    ''' <param name="operationId">an ID of the simple asset operation (an ID of the complex asset operation
    ''' if operationIsComplex)</param>
    ''' <param name="operationType">a type of the simple asset operation</param>
    ''' <param name="operationIsComplex">whether the operation is complex</param>
    ''' <param name="showCannotDeleteMessage">whether to show a 'cannot delete' message
    ''' if there is an open edit form for the business object</param>
    ''' <param name="activateForm">whether to activate the open edit form 
    ''' for the business object if found</param>
    ''' <remarks></remarks>
    Public Shared Function CheckIfAssetOperationEditFormOpen(ByVal operationId As Integer, _
        ByVal operationType As LtaOperationType, ByVal operationIsComplex As Boolean, _
        ByVal showCannotDeleteMessage As Boolean, ByVal activateForm As Boolean) As Boolean

        If Not operationId > 0 Then
            Throw New ArgumentNullException("operationId")
        End If

        If operationIsComplex Then

            Select Case operationType
                Case LtaOperationType.Amortization
                    CheckIfObjectEditFormOpen(Of ComplexOperationAmortization) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case LtaOperationType.Discard
                    CheckIfObjectEditFormOpen(Of ComplexOperationDiscard) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case LtaOperationType.UsingEnd, LtaOperationType.UsingStart
                    CheckIfObjectEditFormOpen(Of ComplexOperationOperationalStatusChange) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case LtaOperationType.ValueChange
                    CheckIfObjectEditFormOpen(Of ComplexOperationValueChange) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case Else
                    Throw New NotImplementedException(String.Format("Klaida. Ilgalaikio turto kompleksinės operacijos tipas {0} neimplementuotas klasėje {1}.", _
                        operationType.ToString, GetType(GoodsOperationManager).FullName))
            End Select

        Else

            Select Case operationType
                Case LtaOperationType.AccountChange
                    CheckIfObjectEditFormOpen(Of OperationAccountChange) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case LtaOperationType.AcquisitionValueIncrease
                    CheckIfObjectEditFormOpen(Of OperationAcquisitionValueIncrease) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case LtaOperationType.Amortization
                    CheckIfObjectEditFormOpen(Of OperationAmortization) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case LtaOperationType.AmortizationPeriod
                    CheckIfObjectEditFormOpen(Of OperationAmortizationPeriodChange) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case LtaOperationType.Discard
                    CheckIfObjectEditFormOpen(Of OperationDiscard) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case LtaOperationType.Transfer
                    CheckIfObjectEditFormOpen(Of OperationTransfer) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case LtaOperationType.UsingEnd, LtaOperationType.UsingStart
                    CheckIfObjectEditFormOpen(Of OperationOperationalStatusChange) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case LtaOperationType.ValueChange
                    CheckIfObjectEditFormOpen(Of OperationValueChange) _
                        (operationId, showCannotDeleteMessage, activateForm)

                Case Else
                    Throw New NotImplementedException(String.Format("Klaida. Ilgalaikio turto operacijos tipas {0} neimplementuotas klasėje {1}.", _
                        operationType.ToString, GetType(GoodsOperationManager).FullName))
            End Select

        End If

    End Function


    ''' <summary>
    ''' Deletes an asset operation using an assets operations report item (should only be invoked by a 
    ''' <see cref="CslaActionExtenderQueryObject">query browser</see>).
    ''' </summary>
    ''' <param name="operationInfo">an assets operations report item containing info about an asset operation</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteAssetOperation(ByVal operationInfo As ActiveReports.LongTermAssetOperationInfo)
        If operationInfo.ComplexActID > 0 Then
            DeleteAssetOperation(operationInfo.ComplexActID, operationInfo.Type, True)
        Else
            DeleteAssetOperation(operationInfo.ID, operationInfo.Type, False)
        End If
    End Sub

    ''' <summary>
    ''' Deletes an asset operation using a given id params (should only be invoked by a 
    ''' <see cref="CslaActionExtenderQueryObject">query browser</see>).
    ''' </summary>
    ''' <param name="operationId">an ID of the simple asset operation (an ID of the complex asset operation
    ''' if operationIsComplex)</param>
    ''' <param name="operationType">a type of the simple asset operation</param>
    ''' <param name="operationIsComplex">whether the operation is complex</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteAssetOperation(ByVal operationId As Integer, ByVal operationType As LtaOperationType, _
        ByVal operationIsComplex As Boolean)

        If Not operationId > 0 Then
            Throw New ArgumentNullException("operationId")
        End If

        If operationIsComplex Then

            Select Case operationType
                Case LtaOperationType.Amortization
                    ComplexOperationAmortization.DeleteComplexOperationAmortization(operationId)
                Case LtaOperationType.Discard
                    ComplexOperationDiscard.DeleteComplexOperationDiscard(operationId)
                Case LtaOperationType.UsingEnd, LtaOperationType.UsingStart
                    ComplexOperationOperationalStatusChange.DeleteComplexOperationOperationalStatusChange(operationId)
                Case LtaOperationType.ValueChange
                    ComplexOperationValueChange.DeleteComplexOperationValueChange(operationId)
                Case Else
                    Throw New NotImplementedException(String.Format("Klaida. Ilgalaikio turto kompleksinės operacijos tipas {0} neimplementuotas klasėje {1}.", _
                        operationType.ToString, GetType(GoodsOperationManager).FullName))
            End Select

        Else

            Select Case operationType
                Case LtaOperationType.AccountChange
                    OperationAccountChange.DeleteOperationAccountChange(operationId)
                Case LtaOperationType.AcquisitionValueIncrease
                    OperationAcquisitionValueIncrease.DeleteOperationAcquisitionValueIncrease(operationId)
                Case LtaOperationType.Amortization
                    OperationAmortization.DeleteOperationAmortization(operationId)
                Case LtaOperationType.AmortizationPeriod
                    OperationAmortizationPeriodChange.DeleteOperationAmortizationPeriodChange(operationId)
                Case LtaOperationType.Discard
                    OperationDiscard.DeleteOperationDiscard(operationId)
                Case LtaOperationType.Transfer
                    OperationTransfer.DeleteOperationTransfer(operationId)
                Case LtaOperationType.UsingEnd, LtaOperationType.UsingStart
                    OperationOperationalStatusChange.DeleteOperationOperationalStatusChange(operationId)
                Case LtaOperationType.ValueChange
                    OperationValueChange.DeleteOperationValueChange(operationId)

                Case Else
                    Throw New NotImplementedException(String.Format("Klaida. Ilgalaikio turto operacijos tipas {0} neimplementuotas klasėje {1}.", _
                        operationType.ToString, GetType(GoodsOperationManager).FullName))
            End Select

        End If

    End Sub


    ''' <summary>
    ''' Shows a form for the user to choose assets and returns ID's of the chosen items (if any).
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function RequestUserToChooseAssets() As Integer()
        Using frm As New F_AssetSelectionList
            frm.ShowDialog()
            Return frm.Result
        End Using
    End Function

    ''' <summary>
    ''' Shows an <see cref="ActiveReports.LongTermAssetOperationInfoListParent">asset operation info report</see>
    ''' for the specified asset.
    ''' </summary>
    ''' <param name="assetID">an <see cref="LongTermAsset">ID of the asset</see> to show the operations for</param>
    ''' <remarks></remarks>
    Public Shared Sub ShowAssetOperationInfoList(ByVal assetID As Integer)
        Dim frm As New F_LongTermAssetOperationInfoListParent(assetID)
        OpenForm(frm)
    End Sub

End Class
