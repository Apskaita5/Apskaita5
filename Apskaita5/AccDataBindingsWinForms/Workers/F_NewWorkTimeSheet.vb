Imports ApskaitaObjects.Workers
Imports ApskaitaObjects.HelperLists
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.Attributes

Friend Class F_NewWorkTimeSheet

    Private _Result As WorkTimeSheet = Nothing
    Private _QueryManager As CslaActionExtenderQueryObject = Nothing


    Public ReadOnly Property Result() As WorkTimeSheet
        Get
            Return _Result
        End Get
    End Property


    Private Sub F_NewWorkTimeSheet_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not PrepareCache(Me, GetType(HelperLists.WorkTimeClassInfoList)) Then Exit Sub

        YearComboBox.Items.Clear()
        For i As Integer = 1 To 10
            YearComboBox.Items.Add((Today.Year - i + 1).ToString)
        Next

        PrepareControl(RestDayClassAccListComboBox, New WorkTimeClassFieldAttribute( _
            ValueRequiredLevel.Optional, False, True))
        PrepareControl(PublicHolidayClassAccListComboBox, New WorkTimeClassFieldAttribute( _
            ValueRequiredLevel.Optional, False, True))

        _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller1)

        MonthComboBox.SelectedIndex = Today.Month - 1
        YearComboBox.SelectedIndex = 0

        Try
            Dim list As WorkTimeClassInfoList = WorkTimeClassInfoList.GetList
            RestDayClassAccListComboBox.SelectedValue = list.GetItemByCode("P")
            PublicHolidayClassAccListComboBox.SelectedValue = list.GetItemByCode("S")
        Catch ex As Exception
        End Try

    End Sub


    Private Sub nOkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nOkButton.Click

        Dim year As Integer = 0
        Dim month As Integer = 0

        Try
            year = CInt(YearComboBox.SelectedItem.ToString)
        Catch ex As Exception
        End Try
        month = MonthComboBox.SelectedIndex + 1

        If year < 1970 OrElse year > 2099 OrElse month < 1 OrElse month > 12 Then
            MsgBox("Klaida. Nenurodyta naujo žiniaraščio metai ir (ar) mėnuo.", _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Dim restInfo As WorkTimeClassInfo = Nothing
        Try
            restInfo = DirectCast(RestDayClassAccListComboBox.SelectedValue, WorkTimeClassInfo)
        Catch ex As Exception
        End Try
        Dim publicHolidaysInfo As WorkTimeClassInfo = Nothing
        Try
            publicHolidaysInfo = DirectCast(PublicHolidayClassAccListComboBox.SelectedValue, WorkTimeClassInfo)
        Catch ex As Exception
        End Try

        ' WorkTimeSheet.NewWorkTimeSheet(year, month, RestInfo, PublicHolidaysInfo)
        _QueryManager.InvokeQuery(Of WorkTimeSheet)(Nothing, "NewWorkTimeSheet", _
            True, AddressOf OnSheetFetched, year, month, restInfo, publicHolidaysInfo)

    End Sub

    Private Sub OnSheetFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If exceptionHandled Then
            Exit Sub
        ElseIf result Is Nothing Then
            MsgBox("Klaida. Nepavyko gauti naujo žiniaraščio.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        _Result = DirectCast(result, WorkTimeSheet)

        Me.Hide()
        Me.Close()

    End Sub


    Private Sub nCancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        Me.Hide()
        Me.Close()
    End Sub

End Class