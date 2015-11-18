Namespace Documents.InvoiceAdapters

    Module InvoiceAdapterFactory

        ''' <summary>
        ''' Gets an existing IInvoiceAdapter instance from a database.
        ''' </summary>
        ''' <param name="adapterType">a <see cref="IInvoiceAdapter.Type">type</see> of the existing adapter</param>
        ''' <param name="operationID">an <see cref="IInvoiceAdapter.Id">ID</see> of the existing adapter</param>
        ''' <param name="parentValidator">chronology validator of the parent invoice</param>
        ''' <param name="forInvoiceMade">whether the adapter is ment for an InvoiceMade 
        ''' (InvoiceReceived otherwise)</param>
        ''' <remarks></remarks>
        Friend Function GetInvoiceAdapter(ByVal adapterType As InvoiceAdapterType, _
            ByVal operationID As Integer, ByVal parentValidator As IChronologicValidator, _
            ByVal forInvoiceMade As Boolean) As IInvoiceAdapter

            Select Case adapterType
                Case InvoiceAdapterType.GoodsAcquisition
                    Return GoodsAcquisitionInvoiceAdapter.GetGoodsAcquisitionInvoiceAdapter( _
                        operationID, parentValidator, forInvoiceMade)
                Case InvoiceAdapterType.GoodsConsignmentAdditionalCosts
                    Return GoodsAddedCostsInvoiceAdapter.GetGoodsAddedCostsInvoiceAdapter( _
                        operationID, parentValidator, forInvoiceMade)
                Case InvoiceAdapterType.GoodsConsignmentDiscount
                    Return GoodsDiscountInvoiceAdapter.GetGoodsDiscountInvoiceAdapter( _
                        operationID, parentValidator, forInvoiceMade)
                Case InvoiceAdapterType.GoodsTransfer
                    Return GoodsSaleInvoiceAdapter.GetGoodsSaleInvoiceAdapter( _
                        operationID, parentValidator, forInvoiceMade)
                Case InvoiceAdapterType.LongTermAssetAcquisitionValueChange
                    Return AssetAcquisitionValueIncreaseInvoiceAdapter. _
                        GetAssetAcquisitionValueIncreaseInvoiceAdapter( _
                        operationID, parentValidator, forInvoiceMade)
                Case InvoiceAdapterType.LongTermAssetPurchase
                    Return AssetAcquisitionInvoiceAdapter.GetAssetAcquisitionInvoiceAdapter( _
                        operationID, parentValidator, forInvoiceMade)
                Case InvoiceAdapterType.LongTermAssetSale
                    Return AssetSaleInvoiceAdapter.GetAssetSaleInvoiceAdapter( _
                        operationID, parentValidator, forInvoiceMade)
                Case InvoiceAdapterType.Service
                    Return ServiceInvoiceAdapter.GetServiceInvoiceAdapter( _
                        operationID, parentValidator, forInvoiceMade)
                Case Else
                    Throw New NotImplementedException(String.Format( _
                        My.Resources.Documents_InvoiceAdapters_InvoiceAdapterFactory_OperationTypeNotImplemented, _
                        adapterType.ToString()))
            End Select

        End Function

        ''' <summary>
        ''' Checks if two IInvoiceAdapter instances are compatible with each other.
        ''' </summary>
        ''' <param name="adapter1">the first adapter to check</param>
        ''' <param name="adapter2">the second adapter to check</param>
        ''' <param name="explanation">our parameter, returns explanation of incompatibility</param>
        ''' <param name="throwOnNotCompatible">whether to throw an exception 
        ''' if the adapters are incompatible</param>
        ''' <remarks>Compatibility is checked against each of the adapters to
        ''' allow new IInvoiceAdapter implementations without changing the existing ones.</remarks>
        Friend Function AdaptersCompatible(ByVal adapter1 As IInvoiceAdapter, _
            ByVal adapter2 As IInvoiceAdapter, ByRef explanation As String, _
            ByVal throwOnNotCompatible As Boolean) As Boolean

            explanation = ""

            If Not adapter1 Is Nothing AndAlso Not adapter1.IsCompatibleWithAdapter(adapter2, explanation) Then

                If throwOnNotCompatible Then
                    Throw New Exception(String.Format(My.Resources.Documents_InvoiceAdapters_InvoiceAdapterFactory_AdaptersIncompatibleException, _
                        explanation))
                Else
                    Return False
                End If

            End If

            If Not adapter2 Is Nothing AndAlso Not adapter2.IsCompatibleWithAdapter(adapter1, explanation) Then

                If throwOnNotCompatible Then
                    Throw New Exception(String.Format(My.Resources.Documents_InvoiceAdapters_InvoiceAdapterFactory_AdaptersIncompatibleException, _
                        explanation))
                Else
                    Return False
                End If

            End If

            Return True

        End Function

    End Module

End Namespace