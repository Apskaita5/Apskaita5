Namespace Assets

    <Serializable()> _
Public Class LongTermAssetOperationChildList
        Inherits BusinessListBase(Of LongTermAssetOperationChildList, LongTermAssetOperationChild)

#Region " Business Methods "

        Friend Sub UpdateType(ByVal nType As LtaOperationType)
            Me.RaiseListChangedEvents = False
            For Each item As LongTermAssetOperationChild In Me
                item.Type = nType
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        Friend Sub UpdateDate(ByVal nDate As Date)
            Me.RaiseListChangedEvents = False
            For Each item As LongTermAssetOperationChild In Me
                item.Date = nDate
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        Friend Sub UpdateContent(ByVal nContent As String)
            Me.RaiseListChangedEvents = False
            For Each item As LongTermAssetOperationChild In Me
                item.Content = nContent
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        Friend Sub UpdateAccountCorresponding(ByVal nAccountCorresponding As Integer)
            Me.RaiseListChangedEvents = False
            For Each item As LongTermAssetOperationChild In Me
                item.AccountCorresponding = nAccountCorresponding
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        Friend Sub UpdateActNumber(ByVal nActNumber As Integer)
            Me.RaiseListChangedEvents = False
            For Each item As LongTermAssetOperationChild In Me
                item.ActNumber = nActNumber
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        Friend Sub UpdateJournalEntry(ByVal nJournalEntryID As Integer, _
            ByVal nJournalEntryContent As String, ByVal nJournalEntryType As DocumentType, _
            ByVal nJournalEntryDocNumber As String)
            Me.RaiseListChangedEvents = False
            For Each item As LongTermAssetOperationChild In Me
                item.SetAttachedJournalEntry(nJournalEntryID, nJournalEntryContent, _
                    nJournalEntryType, nJournalEntryDocNumber)
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        Friend Function GetChronologyValidators() As IChronologicValidator()
            Dim result As New List(Of IChronologicValidator)
            For Each i As LongTermAssetOperationChild In Me
                If i.IsChecked OrElse Not i.IsNew Then result.Add(i.ChronologyValidator)
            Next
            Return result.ToArray
        End Function


        Friend Function GetAllBrokenRules() As String

            If Me.IsValid Then Return ""

            Dim result As String = ""
            For Each item As LongTermAssetOperationChild In Me
                If Not item.IsValid Then
                    If String.IsNullOrEmpty(result) Then
                        result = item.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error)
                    Else
                        result = result & vbCrLf & item.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error)
                    End If
                End If
            Next
            Return result

        End Function

        Friend Function GetAllWarnings() As String

            Dim result As String = ""
            For Each item As LongTermAssetOperationChild In Me
                If item.BrokenRulesCollection.WarningCount > 0 Then
                    If String.IsNullOrEmpty(result) Then
                        result = item.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning)
                    Else
                        result = result & vbCrLf & item.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning)
                    End If
                End If
            Next
            Return result

        End Function

        Friend Sub ForceCheckRules()
            For Each item As LongTermAssetOperationChild In Me
                item.ForceCheckRules()
            Next
        End Sub


        Friend Function IsAnyItemChecked() As Boolean
            If Me.Count < 1 Then Return False
            For Each item As LongTermAssetOperationChild In Me
                If item.IsChecked Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _ItemsSortable As Csla.SortedBindingList(Of LongTermAssetOperationChild) = Nothing

        Friend Shared Function NewLongTermAssetOperationChildList(ByVal BackGroundInfoTable As DataTable, _
            ByVal ChronologyDataSource As DataTable, ByVal parentValidator As IChronologicValidator) As LongTermAssetOperationChildList
            Return New LongTermAssetOperationChildList(BackGroundInfoTable, ChronologyDataSource, parentValidator)
        End Function

        Friend Shared Function GetLongTermAssetOperationChildList(ByVal dt As DataTable, _
            ByVal BackgroundData As DataTable, ByVal ChronologyDataSource As DataTable, _
            ByVal parentValidator As IChronologicValidator) As LongTermAssetOperationChildList
            Return New LongTermAssetOperationChildList(dt, BackgroundData, ChronologyDataSource, parentValidator)
        End Function

        Public Function GetSortedList() As Csla.SortedBindingList(Of LongTermAssetOperationChild)
            If _ItemsSortable Is Nothing Then _
                _ItemsSortable = New Csla.SortedBindingList(Of LongTermAssetOperationChild)(Me)
            Return _ItemsSortable
        End Function


        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal BackGroundInfoTable As DataTable, _
            ByVal ChronologyDataSource As DataTable, ByVal parentValidator As IChronologicValidator)
            MarkAsChild()
            Create(BackGroundInfoTable, ChronologyDataSource, parentValidator)
        End Sub

        Private Sub New(ByVal dt As DataTable, ByVal BackgroundData As DataTable, _
            ByVal ChronologyDataSource As DataTable, ByVal parentValidator As IChronologicValidator)
            MarkAsChild()
            Fetch(dt, BackgroundData, ChronologyDataSource, parentValidator)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal BackGroundInfoTable As DataTable, _
            ByVal ChronologyDataSource As DataTable, ByVal parentValidator As IChronologicValidator)

            RaiseListChangedEvents = False

            For Each dr As DataRow In BackGroundInfoTable.Rows
                Add(LongTermAssetOperationChild.NewLongTermAssetOperationChild( _
                    dr, ChronologyDataSource, parentValidator))
            Next

            RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal dt As DataTable, ByVal BackGroundInfoTable As DataTable, _
            ByVal ChronologyDataSource As DataTable, ByVal parentValidator As IChronologicValidator)

            RaiseListChangedEvents = False

            Dim BackGroundIsFound As Boolean

            For Each dr As DataRow In dt.Rows

                BackGroundIsFound = False
                For Each dk As DataRow In BackGroundInfoTable.Rows
                    If CIntSafe(dr.Item(0), 0) = 0 Then

                        Throw New Exception("Klaida. Nenustatytas IT ID operacijai, kurios ID=" _
                            & CIntSafe(dr.Item(20), 0).ToString & ".")

                    ElseIf CIntSafe(dr.Item(0), 0) = CIntSafe(dk.Item(0), 0) Then

                        Add(LongTermAssetOperationChild.GetLongTermAssetOperationChild( _
                            dr, dk, ChronologyDataSource, parentValidator))
                        BackGroundIsFound = True
                        Exit For

                    End If
                Next
                If Not BackGroundIsFound Then Throw New Exception("Klaida. Nerasta bendra info " _
                    & "apie IT, kurio ID=" & CIntSafe(dr.Item(0), 0).ToString & ".")

            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As LongTermAssetComplexDocument)

            DeletedList.Clear()

            RaiseListChangedEvents = False

            For Each item As LongTermAssetOperationChild In Me
                If item.IsNew AndAlso item.IsChecked Then
                    item.Insert(parent)
                ElseIf Not item.IsNew AndAlso item.IsChecked AndAlso item.IsDirty Then
                    item.Update(parent)
                ElseIf Not item.IsNew AndAlso Not item.IsChecked Then
                    item.DeleteSelf()
                End If
            Next

            For i As Integer = Me.Count To 1 Step -1
                If Not Me(i - 1).IsChecked Then Me.RemoveAt(i - 1)
            Next
            DeletedList.Clear()

            RaiseListChangedEvents = True

        End Sub

        Friend Sub CheckAllRules(ByVal parent As LongTermAssetComplexDocument, _
            ByVal ChronologyDataSource As DataTable)

            For Each item As LongTermAssetOperationChild In Me
                If item.IsChecked OrElse Not item.IsNew Then item.ReloadChronologyValidation( _
                    parent, ChronologyDataSource)
                If Not item.IsNew AndAlso Not item.IsChecked Then _
                    item.CheckIfCanDelete(parent.JournalEntryID)
                If item.IsChecked AndAlso (item.IsNew OrElse item.IsDirty) Then _
                    item.CheckAllRules()
            Next

        End Sub

#End Region

    End Class

End Namespace