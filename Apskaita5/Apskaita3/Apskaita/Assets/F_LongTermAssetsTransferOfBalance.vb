Imports ApskaitaObjects.Assets
Imports ApskaitaObjects.HelperLists

Public Class F_LongTermAssetsTransferOfBalance
    Implements IObjectEditForm

    Private Obj As LongTermAssetsTransferOfBalance
    Private Loading As Boolean = True


    Public ReadOnly Property ObjectID() As Integer Implements IObjectEditForm.ObjectID
        Get
            If Not Obj Is Nothing AndAlso Not Obj.IsNew Then Return Obj.ID
            Return 0
        End Get
    End Property

    Public ReadOnly Property ObjectType() As System.Type Implements IObjectEditForm.ObjectType
        Get
            Return GetType(LongTermAssetsTransferOfBalance)
        End Get
    End Property


    Private Sub F_LongTermAssetsTransferOfBalance_Activated(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.Activated

        If Me.WindowState = FormWindowState.Maximized AndAlso MyCustomSettings.AutoSizeForm Then _
            Me.WindowState = FormWindowState.Normal

        If Loading Then
            Loading = False
            Exit Sub
        End If

        If Not PrepareCache(Me, GetType(HelperLists.AccountInfoList), _
            GetType(LongTermAssetCustomGroupInfoList)) Then Exit Sub

    End Sub

    Private Sub F_LongTermAssetsTransferOfBalance_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If Not Obj Is Nothing AndAlso TypeOf Obj Is IIsDirtyEnough AndAlso _
            DirectCast(Obj, IIsDirtyEnough).IsDirtyEnough Then
            Dim answ As String = Ask("Išsaugoti duomenis?", New ButtonStructure("Taip"), _
                New ButtonStructure("Ne"), New ButtonStructure("Atšaukti"))
            If answ <> "Taip" AndAlso answ <> "Ne" Then
                e.Cancel = True
                Exit Sub
            End If
            If answ = "Taip" Then
                If Not SaveObj() Then
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If

        If Not Obj Is Nothing AndAlso Obj.IsDirty Then CancelObj()

        GetFormLayout(Me)
        GetDataGridViewLayOut(ItemsDataGridView)

    End Sub

    Private Sub F_LongTermAssetsTransferOfBalance_Load(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.Load

        If Not SetDataSources() Then Exit Sub

        Try
            Obj = LoadObject(Of LongTermAssetsTransferOfBalance)(Nothing, _
                "GetLongTermAssetsTransferOfBalance", False)
        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

        LongTermAssetsTransferOfBalanceBindingSource.DataSource = Obj

        Dim cm As New ContextMenu()
        Dim cmItem As New MenuItem("Informacija apie formatą", AddressOf PasteAccButton_Click)
        cm.MenuItems.Add(cmItem)
        PasteAccButton.ContextMenu = cm

        ConfigureButtons()

        AddDGVColumnSelector(ItemsDataGridView)

        SetDataGridViewLayOut(ItemsDataGridView)
        SetFormLayout(Me)

    End Sub


    Private Sub OkButton_Click(ByVal sender As System.Object, _
           ByVal e As System.EventArgs) Handles nOkButton.Click

        If Not SaveObj() Then Exit Sub
        Me.Hide()
        Me.Close()

    End Sub

    Private Sub ApplyButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ApplyButton.Click
        If SaveObj() Then ConfigureButtons()
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        CancelObj()
    End Sub


    Private Sub LimitationsButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles LimitationsButton.Click
        If Obj Is Nothing OrElse StringIsNullOrEmpty(Obj.ChronologyValidator.LimitsExplanation) Then Exit Sub
        MsgBox(Obj.ChronologyValidator.LimitsExplanation, MsgBoxStyle.MsgBoxHelp, "Taikomi Ribojimai")
    End Sub

    Private Sub ViewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ViewJournalEntryButton.Click
        If Obj Is Nothing OrElse Not Obj.ID > 0 Then Exit Sub
        MDIParent1.LaunchForm(GetType(F_TransferOfBalance), True, False, 0)
    End Sub

    Private Sub PasteAccButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles PasteAccButton.Click

        If Obj Is Nothing OrElse Not LongTermAssetsTransferOfBalance.CanEditObject Then Exit Sub

        If GetSenderText(sender).Trim.ToLower.Contains("informacija") Then
            MsgBox(LongTermAsset.GetPasteStringColumnsDescription, _
                MsgBoxStyle.Information, "Info")
            Clipboard.SetText(String.Join(vbTab, LongTermAsset.GetPasteStringColumns))
            Exit Sub
        End If

        Try
            Using busy As New StatusBusy
                Obj.ImportRange(Clipboard.GetText())
            End Using
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub


    Private Sub ItemsDataGridView_CellBeginEdit(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) _
        Handles ItemsDataGridView.CellBeginEdit

        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 OrElse Obj Is Nothing Then Exit Sub

        Dim current As LongTermAsset = Nothing
        Try
            current = DirectCast(ItemsDataGridView.Rows(e.RowIndex).DataBoundItem, LongTermAsset)
        Catch ex As Exception
        End Try
        If current Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If

        If current.FinancialDataIsReadOnly Then

            If ItemsDataGridView.Columns(e.ColumnIndex) Is AccountAcquisitionDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is AccountValueDecreaseDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is AccountAccumulatedAmortizationDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is AccountValueIncreaseDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is AccountRevaluedPortionAmmortizationDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is AcquisitionAccountValueCorrectionDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is AmortizationAccountValueCorrectionDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is ValueDecreaseAccountValueCorrectionDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is ValueIncreaseAccountValueCorrectionDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is ValueIncreaseAmmortizationAccountValueCorrectionDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is AmmountDataGridViewColumn Then

                e.Cancel = True
                Exit Sub

            End If

        End If

        If current.AmortizationDataIsReadOnly Then

            If ItemsDataGridView.Columns(e.ColumnIndex) Is AmortizationCalculatedForMonthsDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is DefaultAmortizationPeriodDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is LiquidationUnitValueDataGridViewColumn _
                OrElse ItemsDataGridView.Columns(e.ColumnIndex) Is ContinuedUsageDataGridViewColumn Then

                e.Cancel = True

            End If

        End If

    End Sub

    Private Sub ItemsDataGridView_DataError(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) _
        Handles ItemsDataGridView.DataError
        e.Cancel = True
        e.ThrowException = False
    End Sub

    Private Sub ItemsDataGridView_UserDeletingRow(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) _
        Handles ItemsDataGridView.UserDeletingRow

        If e.Row Is Nothing OrElse Obj Is Nothing Then Exit Sub

        Dim current As LongTermAsset = Nothing
        Try
            current = DirectCast(e.Row.DataBoundItem, LongTermAsset)
        Catch ex As Exception
        End Try
        If current Is Nothing Then Exit Sub

        If Not current.ChronologyValidator.FinancialDataCanChange OrElse _
            current.ChronologyValidator.MaxDateApplicable Then
            MsgBox(String.Format("Klaida. Ilgalaikio turto {0} duomenų pašalinti iš dokumento negalima, nes apskaitoje yra registruota operacijų su juo.", _
                current.Name))
            e.Cancel = True
        End If

    End Sub


    Private Function SaveObj() As Boolean

        If Not Obj.IsDirty Then Return True

        If Not Obj.IsValid Then
            MsgBox("Formoje yra klaidų:" & vbCrLf & Obj.GetAllBrokenRules, MsgBoxStyle.Exclamation, "Klaida.")
            Return False
        End If

        Dim Question, Answer As String
        If Obj.HasWarnings() Then
            Question = "DĖMESIO. Duomenyse gali būti klaidų: " & vbCrLf & Obj.GetAllWarnings & vbCrLf
        Else
            Question = ""
        End If
        If Obj.IsNew Then
            Question = Question & "Ar tikrai norite įtraukti naujus duomenis?"
            Answer = "Nauji duomenys sėkmingai įtraukti."
        Else
            Question = Question & "Ar tikrai norite pakeisti duomenis?"
            Answer = "Duomenys sėkmingai pakeisti."
        End If

        If Not YesOrNo(Question) Then Return False

        Using bm As New BindingsManager(LongTermAssetsTransferOfBalanceBindingSource, _
            ItemsSortedBindingSource, Nothing, True, False)

            Try
                Obj = LoadObject(Of LongTermAssetsTransferOfBalance)(Obj, "Save", False)
            Catch ex As Exception
                ShowError(ex)
                Return False
            End Try

            bm.SetNewDataSource(Obj)

        End Using

        MsgBox(Answer, MsgBoxStyle.Information, "Info")

        Return True

    End Function

    Private Sub CancelObj()
        If Obj Is Nothing OrElse Obj.IsNew OrElse Not Obj.IsDirty Then Exit Sub
        Using bm As New BindingsManager(LongTermAssetsTransferOfBalanceBindingSource, _
            ItemsSortedBindingSource, Nothing, True, True)
        End Using
    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, GetType(HelperLists.AccountInfoList)) Then Return False

        Try

            LoadAccountInfoListToGridCombo(AccountAcquisitionDataGridViewColumn, True, 1, 2)
            LoadAccountInfoListToGridCombo(AccountAccumulatedAmortizationDataGridViewColumn, True, 1, 2)
            LoadAccountInfoListToGridCombo(AccountValueDecreaseDataGridViewColumn, True, 1, 2)
            LoadAccountInfoListToGridCombo(AccountValueIncreaseDataGridViewColumn, True, 1, 2)
            LoadAccountInfoListToGridCombo(AccountRevaluedPortionAmmortizationDataGridViewColumn, True, 1, 2)
            LoadNameInfoListToCombo(LegalGroupDataGridViewColumn, _
                ApskaitaObjects.Settings.NameType.LongTermAssetLegalGroup, True)
            LoadLongTermAssetCustomGroupInfoToGridCombo(CustomGroupInfoDataGridViewColumn, True)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function

    Private Sub ConfigureButtons()

        PasteAccButton.Enabled = (Not Obj Is Nothing AndAlso _
            Obj.ChronologyValidator.FinancialDataCanChange)

        nOkButton.Enabled = (Not Obj Is Nothing)
        ApplyButton.Enabled = (Not Obj Is Nothing)
        nCancelButton.Enabled = (Not Obj Is Nothing AndAlso Not Obj.IsNew)

        LimitationsButton.Visible = (Not Obj Is Nothing AndAlso _
            Not StringIsNullOrEmpty(Obj.ChronologyValidator.LimitsExplanation))

    End Sub

End Class