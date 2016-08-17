Namespace ActiveReports

    ''' <summary>
    ''' Represents a collection of items in a VAT declaration report. 
    ''' Contains information  about declaration field's values 
    ''' at the maximum detail level: document (an invoice or an advance report) 
    ''' -> document item/line -> declaration field.
    ''' </summary>
    ''' <remarks>Should only be used as part of <see cref="VatDeclaration">VatDeclaration</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class VatDeclarationItemList
        Inherits ReadOnlyListBase(Of VatDeclarationItemList, VatDeclarationItem)

#Region " Business Methods "



#End Region

#Region " Factory Methods "

        Friend Shared Function GetVatDeclarationItemList(ByVal myData As DataTable) As VatDeclarationItemList
            Return New VatDeclarationItemList(myData)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal myData As DataTable)
            ' require use of factory methods
            Fetch(myData)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable)

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As DataRow In myData.Rows
                Add(VatDeclarationItem.GetVatDeclarationItem(dr))
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace