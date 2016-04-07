Namespace Documents

    ''' <summary>
    ''' Represents a list of invoice item (VAT) sum mapings to VAT declaration fields,
    ''' defines declaration fields that the invoice (VAT) sum should be added (or subtracted) to.
    ''' </summary>
    ''' <remarks>Values are persisted in the database table VatDeclarationEntrys.
    ''' Should only be used as a child of <see cref="VatDeclarationSchema">VatDeclarationSchema</see>.</remarks>
    <Serializable()> _
    Public Class VatDeclarationEntryList
        Inherits BusinessListBase(Of VatDeclarationEntryList, VatDeclarationEntry)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim newItem As VatDeclarationEntry = VatDeclarationEntry.NewVatDeclarationEntry
            Me.Add(newItem)
            Return newItem
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
            For Each i As VatDeclarationEntry In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewVatDeclarationEntryList() As VatDeclarationEntryList
            Return New VatDeclarationEntryList
        End Function

        Friend Shared Function GetVatDeclarationEntryList(ByVal parentID As Integer) As VatDeclarationEntryList
            Return New VatDeclarationEntryList(parentID)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal parentID As Integer)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            Fetch(parentID)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal parentID As Integer)

            Dim myComm As New SQLCommand("FetchVatDeclarationEntryList")
            myComm.AddParam("?CD", parentID)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                For Each dr As DataRow In myData.Rows
                    Add(VatDeclarationEntry.GetVatDeclarationEntry(dr))
                Next

                RaiseListChangedEvents = True

            End Using

        End Sub

        Friend Sub Update(ByVal parent As VatDeclarationSchema)

            RaiseListChangedEvents = False

            For Each item As VatDeclarationEntry In DeletedList
                If Not item.IsNew Then item.DeleteSelf()
            Next
            DeletedList.Clear()

            For Each item As VatDeclarationEntry In Me
                If item.IsNew Then
                    item.Insert(parent)
                ElseIf item.IsDirty Then
                    item.Update(parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace