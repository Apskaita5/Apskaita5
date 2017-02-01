Namespace ActiveReports

    ''' <summary>
    ''' Represents a collection of report information about an invoice amount 
    ''' and (VAT) tax for a particular invoice (grouped by tax code).
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="InvoiceInfoItem">InvoiceInfoItem</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class InvoiceSubtotalItemList
        Inherits ReadOnlyListBase(Of InvoiceSubtotalItemList, InvoiceSubtotalItem)

#Region " Business Methods "

        Public Overrides Function ToString() As String
            Dim result As New List(Of String)
            For Each i As InvoiceSubtotalItem In Me
                result.Add(i.ToString())
            Next
            Return String.Join(vbCrLf, result.ToArray())
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetInvoiceSubtotalItemList(ByVal invoiceID As Integer, _
            ByVal myData As DataTable) As InvoiceSubtotalItemList
            Return New InvoiceSubtotalItemList(invoiceID, myData)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal invoiceID As Integer, ByVal myData As DataTable)
            ' require use of factory methods
            Fetch(invoiceID, myData)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal invoiceID As Integer, ByVal myData As DataTable)

            If myData Is Nothing Then Exit Sub

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As DataRow In myData.Rows
                If CIntSafe(dr.Item(0), 0) = invoiceID Then
                    Add(InvoiceSubtotalItem.GetInvoiceSubtotalItem(dr))
                End If
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace