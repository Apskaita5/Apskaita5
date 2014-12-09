Namespace Documents

    <Serializable()> _
    Public Class OffsetItemList
        Inherits BusinessListBase(Of OffsetItemList, OffsetItem)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim NewItem As OffsetItem = OffsetItem.NewOffsetItem
            Me.Add(NewItem)
            Return NewItem
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

#End Region

#Region " Factory Methods "

        Friend Shared Function NewOffsetItemList() As OffsetItemList
            Return New OffsetItemList
        End Function

        Friend Shared Function GetOffsetItemList(ByVal parent As Offset, _
            ByRef BalanceOffsetItem As OffsetItem) As OffsetItemList
            Return New OffsetItemList(parent, BalanceOffsetItem)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal parent As Offset, ByRef BalanceOffsetItem As OffsetItem)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = parent.ChronologicValidator.FinancialDataCanChange
            Me.AllowRemove = parent.ChronologicValidator.FinancialDataCanChange
            Fetch(parent, BalanceOffsetItem)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal parent As Offset, ByRef BalanceOffsetItem As OffsetItem)

            Dim myComm As New SQLCommand("FetchOffsetItemList")
            myComm.AddParam("?BD", parent.ID)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                Dim CurrentChild As OffsetItem

                For Each dr As DataRow In myData.Rows
                    CurrentChild = OffsetItem.GetOffsetItem(dr, parent.ChronologicValidator.FinancialDataCanChange)
                    If (CurrentChild.Person Is Nothing OrElse Not CurrentChild.Person.ID > 0) _
                        AndAlso CurrentChild.CurrencyCode.Trim.ToUpper = GetCurrentCompany.BaseCurrency Then
                        BalanceOffsetItem = CurrentChild
                    Else
                        Add(CurrentChild)
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