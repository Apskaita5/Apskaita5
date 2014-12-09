Namespace Workers

    <Serializable()> _
    Public Class WagePayOutItemList
        Inherits BusinessListBase(Of WagePayOutItemList, WagePayOutItem)

#Region " Business Methods "

        Private _Date As Date

        Friend ReadOnly Property [Date]() As Date
            Get
                Return _Date
            End Get
        End Property


        Public Function GetIfAnyItemIsChecked() As Boolean
            For Each i As WagePayOutItem In Me
                If i.IsChecked Then Return True
            Next
            Return False
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

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of WagePayOutItem) = Nothing

        Friend Shared Function GetWagePayOutItemList(ByVal myData As DataTable, _
            ByVal DocumentDate As Date) As WagePayOutItemList
            Dim result As WagePayOutItemList = New WagePayOutItemList(myData, DocumentDate)
            Return result
        End Function

        Public Function GetSortedList() As Csla.SortedBindingList(Of WagePayOutItem)
            If _SortedList Is Nothing Then _SortedList = New Csla.SortedBindingList(Of WagePayOutItem)(Me)
            Return _SortedList
        End Function

        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
        End Sub

        Private Sub New(ByVal myData As DataTable, ByVal DocumentDate As Date)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            Fetch(myData, DocumentDate)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal DocumentDate As Date)

            RaiseListChangedEvents = False

            For Each dr As DataRow In myData.Rows
                Add(WagePayOutItem.GetWagePayOutItem(dr))
            Next

            _Date = DocumentDate.Date

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As WagePayOutDocument)

            RaiseListChangedEvents = False

            DeletedList.Clear()

            For Each item As WagePayOutItem In Me
                If (item.IsNew AndAlso item.IsChecked) OrElse _
                    (Not item.IsNew AndAlso item.IsDirty) Then item.Update(parent)
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace