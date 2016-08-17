Namespace ActiveReports

    ''' <summary>
    ''' Represents a collection of subtotal items in a VAT declaration report. 
    ''' Contains information about declaration field's subtotal values
    ''' for each declaration field.
    ''' </summary>
    ''' <remarks>Should only be used as part of <see cref="VatDeclaration">VatDeclaration</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class VatDeclarationSubtotalList
        Inherits ReadOnlyListBase(Of VatDeclarationSubtotalList, VatDeclarationSubtotal)

#Region " Business Methods "

#End Region

#Region " Factory Methods "

        Friend Shared Function GetVatDeclarationSubtotalList( _
            ByVal itemList As VatDeclarationItemList) As VatDeclarationSubtotalList
            Return New VatDeclarationSubtotalList(itemList)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal itemList As VatDeclarationItemList)
            ' require use of factory methods
            Fetch(itemList)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal itemList As VatDeclarationItemList)

            If itemList Is Nothing OrElse itemList.Count < 1 Then Exit Sub

            RaiseListChangedEvents = False
            IsReadOnly = False

            Dim isFound As Boolean

            For Each item As VatDeclarationItem In itemList

                isFound = False

                For Each subtotal As VatDeclarationSubtotal In Me
                    If subtotal.Code.Trim.ToUpper = item.FieldCode.Trim.ToUpper Then
                        subtotal.AddVatDeclarationSubtotal(item)
                        isFound = True
                        Exit For
                    End If
                Next

                If Not isFound Then
                    Add(VatDeclarationSubtotal.GetVatDeclarationSubtotal(item))
                End If

            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace