Namespace Workers

    <Serializable()> _
    Public Class ImprestItemList
        Inherits BusinessListBase(Of ImprestItemList, ImprestItem)

#Region " Business Methods "

        Friend Function GetTotalSum() As Double
            Dim result As Double = 0
            For Each i As ImprestItem In Me
                If i.IsChecked Then result = CRound(result + i.PayOffSumTotal)
            Next
            Return result
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


        Friend Function GetIsDirtyEnough(ByVal parent As ImprestSheet) As Boolean
            If Not parent.IsNew Then Return IsDirty
            For Each i As ImprestItem In Me
                If i.PayOffSumTotal > 0 Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetImprestItemList(ByVal myData As DataTable, _
            ByVal nFinancialDataCanChange As Boolean) As ImprestItemList
            Dim result As ImprestItemList = New ImprestItemList(myData, nFinancialDataCanChange)
            Return result
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
        End Sub


        Private Sub New(ByVal myData As DataTable, ByVal nFinancialDataCanChange As Boolean)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = nFinancialDataCanChange
            Me.AllowNew = False
            Me.AllowRemove = False
            Fetch(myData, nFinancialDataCanChange)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal nFinancialDataCanChange As Boolean)

            RaiseListChangedEvents = False

            For Each dr As DataRow In myData.Rows
                Add(ImprestItem.GetImprestItem(dr, nFinancialDataCanChange))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As ImprestSheet)

            RaiseListChangedEvents = False
            DeletedList.Clear()

            For Each item As ImprestItem In Me
                If item.IsNew AndAlso item.IsChecked Then
                    item.Insert(parent)
                ElseIf Not item.IsNew AndAlso Not item.IsChecked Then
                    item.DeleteSelf()
                ElseIf Not item.IsNew AndAlso item.IsDirty Then
                    item.Update(parent)
                End If
            Next

            Me.AllowRemove = True
            For i As Integer = Me.Count To 1 Step -1
                If Not Item(i - 1).IsChecked Then Me.RemoveAt(i - 1)
            Next
            Me.AllowRemove = False
            DeletedList.Clear()

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace