Namespace Documents

    ''' <summary>
    ''' Represents a collection of offset items (accounting entries that needs to be canceled 
    ''' with an equal but opposite entries).
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="Offset">Offset</see>.
    ''' Values are stored in the database table offsetitems.</remarks>
    <Serializable()> _
    Public NotInheritable Class OffsetItemList
        Inherits BusinessListBase(Of OffsetItemList, OffsetItem)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim newItem As OffsetItem = OffsetItem.NewOffsetItem
            Me.Add(newItem)
            Return newItem
        End Function


        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)

            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

        Public Function HasWarnings() As Boolean
            For Each i As OffsetItem In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function


        Friend Function GetBookEntryList() As BookEntryInternalList

            Dim result As BookEntryInternalList = BookEntryInternalList. _
                NewBookEntryInternalList(BookEntryType.Debetas)

            For Each i As OffsetItem In Me
                result.AddRange(i.GetBookEntryList())
            Next

            Return result

        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewOffsetItemList() As OffsetItemList
            Return New OffsetItemList
        End Function

        Friend Shared Function GetOffsetItemList(ByVal parent As Offset, _
            ByRef balanceOffsetItem As OffsetItem) As OffsetItemList
            Return New OffsetItemList(parent, balanceOffsetItem)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal parent As Offset, ByRef balanceOffsetItem As OffsetItem)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = parent.ChronologicValidator.FinancialDataCanChange
            Me.AllowRemove = parent.ChronologicValidator.FinancialDataCanChange
            Fetch(parent, balanceOffsetItem)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal parent As Offset, ByRef balanceOffsetItem As OffsetItem)

            Dim myComm As New SQLCommand("FetchOffsetItemList")
            myComm.AddParam("?BD", parent.ID)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                Dim currentChild As OffsetItem

                For Each dr As DataRow In myData.Rows

                    currentChild = OffsetItem.GetOffsetItem(dr, parent.ChronologicValidator.FinancialDataCanChange)

                    If (currentChild.Person Is Nothing OrElse currentChild.Person.IsEmpty) _
                        AndAlso IsBaseCurrency(currentChild.CurrencyCode, GetCurrentCompany.BaseCurrency) Then
                        balanceOffsetItem = currentChild
                    Else
                        Me.Add(currentChild)
                    End If

                Next

                RaiseListChangedEvents = True

            End Using

        End Sub

        Friend Sub Update(ByVal parent As Offset)

            RaiseListChangedEvents = False

            If parent.ChronologicValidator.FinancialDataCanChange Then
                For Each item As OffsetItem In DeletedList
                    If Not item.IsNew Then item.DeleteSelf()
                Next
            End If
            DeletedList.Clear()

            For Each item As OffsetItem In Me
                If item.IsNew AndAlso parent.ChronologicValidator.FinancialDataCanChange Then
                    item.Insert(parent)
                ElseIf Not item.IsNew AndAlso item.IsDirty Then
                    item.Update(parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace
