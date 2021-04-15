Imports ApskaitaObjects.Workers
Imports AccControlsWinForms

Friend Class F_NewSheet(Of T)

    Private _Result As T = Nothing
    Private _QueryManager As CslaActionExtenderQueryObject = Nothing


    Public ReadOnly Property Result() As T
        Get
            Return _Result
        End Get
    End Property


    Private Sub F_NewWageSheet_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not GetType(T) Is GetType(Workers.WageSheet) AndAlso _
            Not GetType(T) Is GetType(Workers.ImprestSheet) Then
            MsgBox(String.Format("Forma F_NewSheet nemoka gauti naujo {0}.", _
                GetType(T).FullName), MsgBoxStyle.Exclamation, "Klaida")
            DisableAllControls(Me)
            nCancelButton.Enabled = True
            Exit Sub
        End If

        If GetType(T) Is GetType(Workers.WageSheet) Then
            Me.Text = "Naujas darbo užmokesčio žiniaraštis"
        Else
            Me.Text = "Naujas avanso žiniaraštis"
        End If

        YearComboBox.Items.Clear()
        For i As Integer = 1 To 10
            YearComboBox.Items.Add((Today.Year - i + 1).ToString)
        Next

        MonthComboBox.Items.Clear()
        For i As Integer = 1 To 12
            MonthComboBox.Items.Add(i.ToString)
        Next

        Dim msg As String = ""

        If Not GetCurrentCompany.IsSettingsReadyForWageSheet(msg) Then

            MsgBox("Klaida. Nenustatyti visi reikiami nustatymai:" & vbCrLf & msg, _
                MsgBoxStyle.Exclamation, "Klaida.")

            Dim frm As New F_Company
            frm.ShowDialog()

            If Not GetCurrentCompany.IsSettingsReadyForWageSheet(msg) Then
                MsgBox("Klaida. Nenustatyti visi reikiami nustatymai:" & vbCrLf & msg, _
                    MsgBoxStyle.Exclamation, "Klaida.")
                DisableAllControls(Me)
                nCancelButton.Enabled = True
                Exit Sub
            End If

        End If

        _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller1)

        MonthComboBox.SelectedIndex = Today.Month - 1
        YearComboBox.SelectedIndex = 0

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

        If GetType(T) Is GetType(WageSheet) Then
            ' WageSheet.NewWageSheet(year, month)
            _QueryManager.InvokeQuery(Of WageSheet)(Nothing, "NewWageSheet", _
                True, AddressOf OnSheetFetched, year, month)
        Else
            ' ImprestSheet.NewImprestSheet(year, month)
            _QueryManager.InvokeQuery(Of ImprestSheet)(Nothing, "NewImprestSheet", _
                True, AddressOf OnSheetFetched, year, month)
        End If

    End Sub

    Private Sub OnSheetFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If exceptionHandled Then
            Exit Sub
        ElseIf result Is Nothing Then
            MsgBox("Klaida. Nepavyko gauti naujo žiniaraščio.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        _Result = DirectCast(result, T)

        Me.Close()

    End Sub


    Private Sub nCancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        Me.Close()
    End Sub

End Class
