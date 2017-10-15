Imports ApskaitaObjects.Goods

''' <summary>
''' A helper class containing methods to access goods operations in a uniform way. 
''' </summary>
''' <remarks></remarks>
Public Class GoodsOperationManager

    Private Shared _SimpleOperationTypes As List(Of TypeItem)
    Private Shared _ComplexOperationTypes As List(Of TypeItem)


    ''' <summary>
    ''' Gets a list of the available simple goods operations.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function GetSimpleOperationTypes() As List(Of TypeItem)
        If _SimpleOperationTypes Is Nothing Then InitializeSimpleOperationTypes()
        Return _SimpleOperationTypes
    End Function

    Private Shared Sub InitializeSimpleOperationTypes()

        _SimpleOperationTypes = New List(Of TypeItem)

        _SimpleOperationTypes.Add(New TypeItem(GetType(Goods.GoodsOperationAccountChange), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsOperationAccountChange))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Goods.GoodsOperationAcquisition), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsOperationAcquisition))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Goods.GoodsOperationAdditionalCosts), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsOperationAdditionalCosts))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Goods.GoodsOperationDiscard), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsOperationDiscard))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Goods.GoodsOperationDiscount), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsOperationDiscount))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Goods.GoodsOperationPriceCut), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsOperationPriceCut))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Goods.GoodsOperationRedeemFromBuyer), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsOperationRedeemFromBuyer))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Goods.GoodsOperationTransfer), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsOperationTransfer))))
        _SimpleOperationTypes.Add(New TypeItem(GetType(Goods.GoodsOperationValuationMethod), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsOperationValuationMethod))))

    End Sub

    ''' <summary>
    ''' Gets a list of the available complex goods operations.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function GetComplexOperationTypes() As List(Of TypeItem)
        If _ComplexOperationTypes Is Nothing Then InitializeComplexOperationTypes()
        Return _ComplexOperationTypes
    End Function

    Private Shared Sub InitializeComplexOperationTypes()

        _ComplexOperationTypes = New List(Of TypeItem)

        _ComplexOperationTypes.Add(New TypeItem(GetType(Goods.GoodsComplexOperationDiscard), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsComplexOperationDiscard))))
        _ComplexOperationTypes.Add(New TypeItem(GetType(Goods.GoodsComplexOperationInternalTransfer), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsComplexOperationInternalTransfer))))
        _ComplexOperationTypes.Add(New TypeItem(GetType(Goods.GoodsComplexOperationInventorization), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsComplexOperationInventorization))))
        _ComplexOperationTypes.Add(New TypeItem(GetType(Goods.GoodsComplexOperationPriceCut), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsComplexOperationPriceCut))))
        _ComplexOperationTypes.Add(New TypeItem(GetType(Goods.GoodsComplexOperationProduction), _
            CommonValidation.GetResourcesTypeName(GetType(Goods.GoodsComplexOperationProduction))))

    End Sub


    ''' <summary>
    ''' Creates a new goods operation and shows it in edit form.
    ''' </summary>
    ''' <param name="goodsID">an <see cref="Goods.GoodsItem.ID">ID of the goods</see>
    ''' that the new operation is for</param>
    ''' <param name="operationType">a type of the business object that represents a goods operation
    ''' (a goods operation edit form shall implement a constructor that accepts goodsID)</param>
    ''' <remarks>a goods operation edit form shall implement a constructor that accepts goodsID</remarks>
    Public Shared Sub StartNewGoodsOperation(ByVal goodsID As Integer, ByVal operationType As Type)
        Dim frm As Form = GetFormForBusinessType(operationType, goodsID)
        OpenForm(frm)
    End Sub

    ''' <summary>
    ''' Creates a new goods operation and shows it in edit form.
    ''' </summary>
    ''' <param name="operationType">a type of the business object that represents a goods operation
    ''' (a goods operation edit form shall implement an empty constructor)</param>
    ''' <remarks>a goods operation edit form shall implement empty constructor</remarks>
    Public Shared Sub StartNewGoodsOperation(ByVal operationType As Type)
        Dim frm As Form = GetFormForBusinessType(operationType)
        OpenForm(frm)
    End Sub


    ''' <summary>
    ''' Fetches a goods operation by the encapsulated journal entry ID (should only be invoked by a 
    ''' <see cref="CslaActionExtenderQueryObject">query browser</see>).
    ''' </summary>
    ''' <param name="journalEntryId">an ID of the jousrnal entry to look the goods operation by</param>
    ''' <param name="journalEntryType">a type of the jousrnal entry to look the goods operation by</param>
    ''' <remarks></remarks>
    Public Shared Function GetGoodsOperation(ByVal journalEntryId As Integer, ByVal journalEntryType As DocumentType) As Object
        Return GetGoodsOperation(GoodsOperationIdInfo.GetGoodsOperationIdInfo( _
            journalEntryId, journalEntryType))
    End Function

    ''' <summary>
    ''' Fetches a goods operation using a helper info object (should only be invoked by a 
    ''' <see cref="CslaActionExtenderQueryObject">query browser</see>).
    ''' </summary>
    ''' <param name="operationInfo">a helper info object containing info about a goods operation</param>
    ''' <remarks></remarks>
    Public Shared Function GetGoodsOperation(ByVal operationInfo As GoodsOperationIdInfo) As Object
        Return GetGoodsOperation(operationInfo.ID, operationInfo.Type, operationInfo.IsComplex, _
            operationInfo.ComplexType, operationInfo.JournalEntryID, operationInfo.DocumentType)
    End Function

    ''' <summary>
    ''' Fetches a goods operation using a goods operations report item (should only be invoked by a 
    ''' <see cref="CslaActionExtenderQueryObject">query browser</see>).
    ''' </summary>
    ''' <param name="operationInfo">a goods operations report item containing info about a goods operation</param>
    ''' <remarks></remarks>
    Public Shared Function GetGoodsOperation(ByVal operationInfo As ActiveReports.GoodsOperationInfo) As Object
        If operationInfo.ComplexOperationID > 0 Then
            Return GetGoodsOperation(operationInfo.ComplexOperationID, _
                ConvertLocalizedName(Of GoodsOperationType)(operationInfo.Type), True, _
                ConvertLocalizedName(Of GoodsComplexOperationType)(operationInfo.ComplexType), _
                operationInfo.JournalEntryID, ConvertLocalizedName(Of DocumentType)(operationInfo.JournalEntryType))
        Else
            Return GetGoodsOperation(operationInfo.ID, _
                ConvertLocalizedName(Of GoodsOperationType)(operationInfo.Type), False, _
                GoodsComplexOperationType.BulkDiscard, operationInfo.JournalEntryID, _
                ConvertLocalizedName(Of DocumentType)(operationInfo.JournalEntryType))
        End If
    End Function

    ''' <summary>
    ''' Fetches a goods operation using a given id params (should only be invoked by a 
    ''' <see cref="CslaActionExtenderQueryObject">query browser</see>).
    ''' </summary>
    ''' <param name="operationId">an ID of the simple goods operation (an ID of the complex goods operation
    ''' if operationIsComplex)</param>
    ''' <param name="operationType">a type of the simple goods operation</param>
    ''' <param name="operationIsComplex">whether the operation is complex</param>
    ''' <param name="complexType">a type of the complex goods operation</param>
    ''' <param name="journalEntryId">an ID of the encapsulated journal entry</param>
    ''' <param name="journalEntryType">a type of the encapsulated journal entry</param>
    ''' <remarks></remarks>
    Public Shared Function GetGoodsOperation(ByVal operationId As Integer, ByVal operationType As GoodsOperationType, _
        ByVal operationIsComplex As Boolean, ByVal complexType As GoodsComplexOperationType, _
        ByVal journalEntryId As Integer, ByVal journalEntryType As DocumentType) As Object

        If Not operationId > 0 Then
            Throw New ArgumentNullException("operationId")
        End If

        If operationIsComplex Then

            Select Case complexType
                Case GoodsComplexOperationType.BulkDiscard
                    Return GoodsComplexOperationDiscard.GetGoodsComplexOperationDiscard(operationId)
                Case GoodsComplexOperationType.BulkPriceCut
                    Return GoodsComplexOperationPriceCut.GetGoodsComplexOperationPriceCut(operationId)
                Case GoodsComplexOperationType.InternalTransfer
                    Return GoodsComplexOperationInternalTransfer.GetGoodsComplexOperationInternalTransfer(operationId)
                Case GoodsComplexOperationType.Inventorization
                    Return GoodsComplexOperationInventorization.GetGoodsComplexOperationInventorization(operationId)
                Case GoodsComplexOperationType.Production
                    Return GoodsComplexOperationProduction.GetGoodsComplexOperationProduction(operationId)
                Case GoodsComplexOperationType.TransferOfBalance
                    Return GoodsComplexOperationTransferOfBalance.GetGoodsComplexOperationTransferOfBalance()
                Case Else
                    Throw New NotImplementedException(String.Format("Klaida. Prekių operacijos tipas {0} neimplementuotas klasėje {1}.", _
                        complexType.ToString, GetType(GoodsOperationManager).FullName))
            End Select

        Else

            Dim invoiceChildTypes As GoodsOperationType() = New GoodsOperationType() _
                {GoodsOperationType.Acquisition, GoodsOperationType.ConsignmentAdditionalCosts, _
                 GoodsOperationType.ConsignmentDiscount, GoodsOperationType.RedeemFromBuyer, _
                 GoodsOperationType.Transfer}

            If Not Array.IndexOf(invoiceChildTypes, operationType) < 0 AndAlso journalEntryId > 0 Then
                If journalEntryType = DocumentType.InvoiceMade Then
                    Return Documents.InvoiceMade.GetInvoiceMade(journalEntryId)
                ElseIf journalEntryType = DocumentType.InvoiceReceived Then
                    Return Documents.InvoiceReceived.GetInvoiceReceived(journalEntryId)
                End If
            End If

            Select Case operationType
                Case GoodsOperationType.AccountDiscountsChange, GoodsOperationType.AccountPurchasesChange, _
                    GoodsOperationType.AccountSalesNetCostsChange, GoodsOperationType.AccountValueReductionChange
                    Return GoodsOperationAccountChange.GetGoodsOperationAccountChange(operationId)
                Case GoodsOperationType.Acquisition
                    Return GoodsOperationAcquisition.GetGoodsOperationAcquisition(operationId)
                Case GoodsOperationType.ConsignmentAdditionalCosts
                    Return GoodsOperationAdditionalCosts.GetGoodsOperationAdditionalCosts(operationId)
                Case GoodsOperationType.ConsignmentDiscount
                    Return GoodsOperationDiscount.GetGoodsOperationDiscount(operationId)
                Case GoodsOperationType.Discard
                    Return GoodsOperationDiscard.GetGoodsOperationDiscard(operationId)
                Case GoodsOperationType.PriceCut
                    Return GoodsOperationPriceCut.GetGoodsOperationPriceCut(operationId)
                Case GoodsOperationType.RedeemFromBuyer
                    Return GoodsOperationRedeemFromBuyer.GetGoodsOperationRedeemFromBuyer(operationId)
                Case GoodsOperationType.Transfer
                    Return GoodsOperationTransfer.GetGoodsOperationTransfer(operationId)
                Case GoodsOperationType.ValuationMethodChange
                    Return GoodsOperationValuationMethod.GetGoodsOperationValuationMethod(operationId)
                Case Else
                    Throw New NotImplementedException(String.Format("Klaida. Prekių operacijos tipas {0} neimplementuotas klasėje {1}.", _
                        operationType.ToString, GetType(GoodsOperationManager).FullName))
            End Select

        End If

    End Function


    ''' <summary>
    ''' Checks if the operation with the given id is currently open in any edit form.
    ''' </summary>
    ''' <param name="operationId">an ID of the simple goods operation (an ID of the complex goods operation
    ''' if operationIsComplex)</param>
    ''' <param name="operationType">a type of the simple goods operation</param>
    ''' <param name="operationIsComplex">whether the operation is complex</param>
    ''' <param name="complexType">a type of the complex goods operation</param>
    ''' <param name="showCannotDeleteMessage">whether to show a 'cannot delete' message
    ''' if there is an open edit form for the business object</param>
    ''' <param name="activateForm">whether to activate the open edit form 
    ''' for the business object if found</param>
    ''' <remarks></remarks>
    Public Shared Function CheckIfGoodsOperationEditFormOpen(ByVal operationId As Integer, _
        ByVal operationType As GoodsOperationType, ByVal operationIsComplex As Boolean, _
        ByVal complexType As GoodsComplexOperationType, ByVal showCannotDeleteMessage As Boolean, _
        ByVal activateForm As Boolean) As Boolean

        If Not operationId > 0 Then
            Throw New ArgumentNullException("operationId")
        End If

        If operationIsComplex Then

            Select Case complexType
                Case GoodsComplexOperationType.BulkDiscard
                    Return CheckIfObjectEditFormOpen(Of GoodsComplexOperationDiscard) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsComplexOperationType.BulkPriceCut
                    Return CheckIfObjectEditFormOpen(Of GoodsComplexOperationPriceCut) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsComplexOperationType.InternalTransfer
                    Return CheckIfObjectEditFormOpen(Of GoodsComplexOperationInternalTransfer) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsComplexOperationType.Inventorization
                    Return CheckIfObjectEditFormOpen(Of GoodsComplexOperationInventorization) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsComplexOperationType.Production
                    Return CheckIfObjectEditFormOpen(Of GoodsComplexOperationProduction) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsComplexOperationType.TransferOfBalance
                    Return CheckIfObjectEditFormOpen(Of GoodsComplexOperationTransferOfBalance) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case Else
                    Throw New NotImplementedException(String.Format("Klaida. Prekių operacijos tipas {0} neimplementuotas klasėje {1}.", _
                        complexType.ToString, GetType(GoodsOperationManager).FullName))
            End Select

        Else

            Select Case operationType
                Case GoodsOperationType.AccountDiscountsChange, GoodsOperationType.AccountPurchasesChange, _
                    GoodsOperationType.AccountSalesNetCostsChange, GoodsOperationType.AccountValueReductionChange
                    Return CheckIfObjectEditFormOpen(Of GoodsOperationAccountChange) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsOperationType.Acquisition
                    Return CheckIfObjectEditFormOpen(Of GoodsOperationAcquisition) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsOperationType.ConsignmentAdditionalCosts
                    Return CheckIfObjectEditFormOpen(Of GoodsOperationAdditionalCosts) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsOperationType.ConsignmentDiscount
                    Return CheckIfObjectEditFormOpen(Of GoodsOperationDiscount) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsOperationType.Discard
                    Return CheckIfObjectEditFormOpen(Of GoodsOperationDiscard) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsOperationType.PriceCut
                    Return CheckIfObjectEditFormOpen(Of GoodsOperationPriceCut) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsOperationType.RedeemFromBuyer
                    Return CheckIfObjectEditFormOpen(Of GoodsOperationRedeemFromBuyer) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsOperationType.Transfer
                    Return CheckIfObjectEditFormOpen(Of GoodsOperationTransfer) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case GoodsOperationType.ValuationMethodChange
                    Return CheckIfObjectEditFormOpen(Of GoodsOperationValuationMethod) _
                        (operationId, showCannotDeleteMessage, activateForm)
                Case Else
                    Throw New NotImplementedException(String.Format("Klaida. Prekių operacijos tipas {0} neimplementuotas klasėje {1}.", _
                        operationType.ToString, GetType(GoodsOperationManager).FullName))
            End Select

        End If

    End Function


    ''' <summary>
    ''' Deletes a goods operation using a goods operations report item (should only be invoked by a 
    ''' <see cref="CslaActionExtenderQueryObject">query browser</see>).
    ''' </summary>
    ''' <param name="operationInfo">a goods operations report item containing info about a goods operation</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteGoodsOperation(ByVal operationInfo As ActiveReports.GoodsOperationInfo)
        If operationInfo.ComplexOperationID > 0 Then
            DeleteGoodsOperation(operationInfo.ComplexOperationID, _
                ConvertLocalizedName(Of GoodsOperationType)(operationInfo.Type), True, _
                ConvertLocalizedName(Of GoodsComplexOperationType)(operationInfo.ComplexType), _
                operationInfo.JournalEntryID, ConvertLocalizedName(Of DocumentType)(operationInfo.JournalEntryType))
        Else
            DeleteGoodsOperation(operationInfo.ID, _
                ConvertLocalizedName(Of GoodsOperationType)(operationInfo.Type), False, _
                GoodsComplexOperationType.BulkDiscard, operationInfo.JournalEntryID, _
                ConvertLocalizedName(Of DocumentType)(operationInfo.JournalEntryType))
        End If
    End Sub

    ''' <summary>
    ''' Deletes a goods operation using a given id params (should only be invoked by a 
    ''' <see cref="CslaActionExtenderQueryObject">query browser</see>).
    ''' </summary>
    ''' <param name="operationId">an ID of the simple goods operation (an ID of the complex goods operation
    ''' if operationIsComplex) to delete</param>
    ''' <param name="operationType">a type of the simple goods operation</param>
    ''' <param name="operationIsComplex">whether the operation is complex</param>
    ''' <param name="complexType">a type of the complex goods operation</param>
    ''' <param name="journalEntryId">an ID of the encapsulated journal entry</param>
    ''' <param name="journalEntryType">a type of the encapsulated journal entry</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteGoodsOperation(ByVal operationId As Integer, ByVal operationType As GoodsOperationType, _
        ByVal operationIsComplex As Boolean, ByVal complexType As GoodsComplexOperationType, _
        ByVal journalEntryId As Integer, ByVal journalEntryType As DocumentType)

        If Not operationId > 0 Then
            Throw New ArgumentNullException("operationId")
        End If

        If operationIsComplex Then

            Select Case complexType
                Case GoodsComplexOperationType.BulkDiscard
                    GoodsComplexOperationDiscard.DeleteGoodsComplexOperationDiscard(operationId)
                Case GoodsComplexOperationType.BulkPriceCut
                    GoodsComplexOperationPriceCut.DeleteGoodsComplexOperationPriceCut(operationId)
                Case GoodsComplexOperationType.InternalTransfer
                    GoodsComplexOperationInternalTransfer.DeleteGoodsComplexOperationInternalTransfer(operationId)
                Case GoodsComplexOperationType.Inventorization
                    GoodsComplexOperationInventorization.DeleteGoodsComplexOperationInventorization(operationId)
                Case GoodsComplexOperationType.Production
                    GoodsComplexOperationProduction.DeleteGoodsComplexOperationProduction(operationId)
                Case GoodsComplexOperationType.TransferOfBalance
                    GoodsComplexOperationTransferOfBalance.DeleteGoodsComplexOperationTransferOfBalance()
                Case Else
                    Throw New NotImplementedException(String.Format("Klaida. Prekių operacijos tipas {0} neimplementuotas klasėje {1}.", _
                        complexType.ToString, GetType(GoodsOperationManager).FullName))
            End Select

        Else

            Dim invoiceChildTypes As GoodsOperationType() = New GoodsOperationType() _
                {GoodsOperationType.Acquisition, GoodsOperationType.ConsignmentAdditionalCosts, _
                 GoodsOperationType.ConsignmentDiscount, GoodsOperationType.RedeemFromBuyer, _
                 GoodsOperationType.Transfer}

            If Not Array.IndexOf(invoiceChildTypes, operationType) < 0 Then
                If journalEntryType = DocumentType.InvoiceMade Then
                    Throw New Exception("Klaida. Ši operacija buvo sukurta registruojant išrašytą sąskaitą faktūra ir gali būti ištrinta tik redaguojant ar ištrinant atitinkamą sąskaitą faktūrą.")
                ElseIf journalEntryType = DocumentType.InvoiceReceived Then
                    Throw New Exception("Klaida. Ši operacija buvo sukurta registruojant gautą sąskaitą faktūra ir gali būti ištrinta tik redaguojant ar ištrinant atitinkamą sąskaitą faktūrą.")
                End If
            End If

            Select Case operationType
                Case GoodsOperationType.AccountDiscountsChange, GoodsOperationType.AccountPurchasesChange, _
                    GoodsOperationType.AccountSalesNetCostsChange, GoodsOperationType.AccountValueReductionChange
                    GoodsOperationAccountChange.DeleteGoodsOperationAccountChange(operationId)
                Case GoodsOperationType.Acquisition
                    GoodsOperationAcquisition.DeleteGoodsOperationAcquisition(operationId)
                Case GoodsOperationType.ConsignmentAdditionalCosts
                    GoodsOperationAdditionalCosts.DeleteGoodsOperationAdditionalCosts(operationId)
                Case GoodsOperationType.ConsignmentDiscount
                    GoodsOperationDiscount.DeleteGoodsOperationDiscount(operationId)
                Case GoodsOperationType.Discard
                    GoodsOperationDiscard.DeleteGoodsOperationDiscard(operationId)
                Case GoodsOperationType.PriceCut
                    GoodsOperationPriceCut.DeleteGoodsOperationPriceCut(operationId)
                Case GoodsOperationType.RedeemFromBuyer
                    GoodsOperationRedeemFromBuyer.DeleteGoodsOperationRedeemFromBuyer(operationId)
                Case GoodsOperationType.Transfer
                    GoodsOperationTransfer.DeleteGoodsOperationTransfer(operationId)
                Case GoodsOperationType.ValuationMethodChange
                    GoodsOperationValuationMethod.DeleteGoodsOperationValuationMethod(operationId)
                Case Else
                    Throw New NotImplementedException(String.Format("Klaida. Prekių operacijos tipas {0} neimplementuotas klasėje {1}.", _
                        operationType.ToString, GetType(GoodsOperationManager).FullName))
            End Select

        End If

    End Sub


    ''' <summary>
    ''' Shows a form for the user to choose goods and returns ID's of the chosen items (if any).
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function RequestUserToChooseGoods() As Integer()
        Using frm As New F_GoodsSelectionList
            frm.ShowDialog()
            Return frm.Result
        End Using
    End Function

    ''' <summary>
    ''' Shows a <see cref="ActiveReports.GoodsOperationInfoListParent">goods operation info report</see>
    ''' for the specified report params.
    ''' </summary>
    ''' <param name="dateFrom">starting date of the report</param>
    ''' <param name="dateTo">ending date of the report</param>
    ''' <param name="goodsID">an <see cref="Goods.GoodsItem.ID">ID of the goods</see> to show the operations for</param>
    ''' <param name="warehouseID">an <see cref="Goods.Warehouse.ID">ID of the warehouse</see> to filter the operations by</param>
    ''' <remarks></remarks>
    Public Shared Sub ShowGoodsOperationInfoList(ByVal dateFrom As Date, ByVal dateTo As Date, _
        ByVal goodsID As Integer, ByVal warehouseID As Integer)
        Dim frm As New F_GoodsOperationInfoListParent(dateFrom, dateTo, goodsID, warehouseID)
        OpenForm(frm)
    End Sub

End Class
