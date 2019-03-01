Imports ApskaitaObjects.Assets
Imports ApskaitaObjects.HelperLists
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists

Friend Class F_LongTermAssetsTransferOfBalance
    Implements ISingleInstanceForm, ISupportsChronologicValidator

    Private Shared ReadOnly _FinancialAspects As String() = New String() {"AccountAcquisition", "AccountAccumulatedAmortization", _
        "AccountRevaluedPortionAmmortization", "AccountValueDecrease", "AccountValueIncrease", _
        "AcquisitionAccountValueCorrection", "AmortizationAccountValueCorrection", _
        "ValueDecreaseAccountValueCorrection", "ValueIncreaseAccountValueCorrection", _
        "ValueIncreaseAmmortizationAccountValueCorrection", "Ammount", "AcquisitionAccountValuePerUnit", _
        "AmortizationAccountValuePerUnit", "ValueDecreaseAccountValuePerUnit", _
        "ValueIncreaseAccountValuePerUnit", "ValueIncreaseAmmortizationAccountValuePerUnit"}

    Private Shared ReadOnly _AmortizationAspects As String() = New String() {"AmortizationAccountValue", "ContinuedUsage", _
        "DefaultAmortizationPeriod", "LiquidationUnitValue"}

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.AccountInfoList), GetType(LongTermAssetCustomGroupInfoList), _
         GetType(HelperLists.NameInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of LongTermAssetsTransferOfBalance)
    Private _ListViewManager As DataListViewEditControlManager(Of LongTermAsset)
    Private _QueryManager As CslaActionExtenderQueryObject


    Private Sub F_LongTermAssetsTransferOfBalance_Load(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.Load

        Try

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            ' LongTermAssetsTransferOfBalance.GetLongTermAssetsTransferOfBalance()
            _QueryManager.InvokeQuery(Of LongTermAssetsTransferOfBalance)(Nothing, _
                "GetLongTermAssetsTransferOfBalance", True, AddressOf OnDataSourceLoaded)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

    End Sub

    Private Function SetDataSources(ByVal currentSource As LongTermAssetsTransferOfBalance) As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of LongTermAsset) _
                (ItemsDataListView, Nothing, AddressOf OnItemsDelete, _
                 Nothing, Nothing, currentSource)

            SetupDefaultControls(Of LongTermAssetsTransferOfBalance) _
                (Me, LongTermAssetsTransferOfBalanceBindingSource, currentSource)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function

    Private Sub OnDataSourceLoaded(ByVal source As Object, ByVal exceptionHandled As Boolean)

        If exceptionHandled Then

            DisableAllControls(Me)
            Exit Sub

        ElseIf source Is Nothing Then

            ShowError(New Exception("Klaida. Nepavyko gauti ilgalaikio turto likučių perkėlimo duomenų."), Nothing)
            DisableAllControls(Me)
            Exit Sub

        End If

        If Not SetDataSources(DirectCast(source, LongTermAssetsTransferOfBalance)) Then
            Exit Sub
        End If

        Try

            _FormManager = New CslaActionExtenderEditForm(Of LongTermAssetsTransferOfBalance) _
                (Me, LongTermAssetsTransferOfBalanceBindingSource, _
                 DirectCast(source, LongTermAssetsTransferOfBalance), _
                 _RequiredCachedLists, nOkButton, ApplyButton, nCancelButton, Nothing, ProgressFiller1)

            _FormManager.ManageDataListViewStates(ItemsDataListView)

        Catch ex As Exception
            ShowError(New Exception("Klaida. Nepavyko gauti ilgalaikio turto likučių perkėlimo duomenų: " & ex.Message, ex), Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

        ConfigureButtons()

    End Sub


    Private Sub OnItemsDelete(ByVal items As LongTermAsset())
        If items Is Nothing OrElse items.Length < 1 OrElse _FormManager.DataSource Is Nothing Then Exit Sub
        For Each item As LongTermAsset In items
            If Not item.ChronologyValidator.FinancialDataCanChange OrElse _
                item.ChronologyValidator.MaxDateApplicable Then
                MsgBox(String.Format("Klaida. Ilgalaikio turto {0} duomenų pašalinti iš dokumento negalima, nes apskaitoje yra registruota operacijų su juo:{1}{2}{3}{4}", _
                    item.Name, vbCrLf, item.ChronologyValidator.FinancialDataCanChangeExplanation, _
                    vbCrLf, item.ChronologyValidator.MaxDateExplanation))
                Exit Sub
            End If
        Next
        For Each item As LongTermAsset In items
            _FormManager.DataSource.Items.Remove(item)
        Next
    End Sub

    Private Sub ViewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ViewJournalEntryButton.Click
        If _FormManager.DataSource Is Nothing OrElse Not _FormManager.DataSource.ID > 0 Then Exit Sub
        OpenNewForm(Of General.TransferOfBalance)()
    End Sub

    Private Sub PasteAccButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles PasteAccButton.Click

        If _FormManager.DataSource Is Nothing OrElse Not LongTermAssetsTransferOfBalance.CanEditObject Then Exit Sub

        Dim data As DataTable = F_DataImport.GetImportedData(LongTermAsset.GetDataTableSpecification)
        If data Is Nothing Then Exit Sub

        Try
            Using busy As New StatusBusy
                _FormManager.DataSource.ImportRange(data)
            End Using
        Catch ex As Exception
            ShowError(ex, New Object() {_FormManager.DataSource, data})
        End Try

    End Sub

    Private Sub ItemsDataListView_CellEditStarting(ByVal sender As Object, _
        ByVal e As BrightIdeasSoftware.CellEditEventArgs) Handles ItemsDataListView.CellEditStarting

        If e.RowObject Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If

        Dim item As LongTermAsset = DirectCast(e.RowObject, LongTermAsset)

        If (item.FinancialDataIsReadOnly AndAlso Not Array.IndexOf(_FinancialAspects, e.Column.AspectName.Trim) < 0) _
            OrElse (item.AmortizationDataIsReadOnly AndAlso Not Array.IndexOf(_AmortizationAspects, e.Column.AspectName.Trim) < 0) Then
            e.Cancel = True
            Exit Sub
        End If

    End Sub


    Public Function ChronologicContent() As String _
        Implements ISupportsChronologicValidator.ChronologicContent
        If _FormManager.DataSource Is Nothing Then Return ""
        Return _FormManager.DataSource.ChronologyValidator.LimitsExplanation
    End Function

    Public Function HasChronologicContent() As Boolean _
        Implements ISupportsChronologicValidator.HasChronologicContent

        Return Not _FormManager.DataSource Is Nothing AndAlso _
            Not StringIsNullOrEmpty(_FormManager.DataSource.ChronologyValidator.LimitsExplanation)

    End Function


    Private Sub _FormManager_DataSourceStateHasChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles _FormManager.DataSourceStateHasChanged
        ConfigureButtons()
    End Sub

    Private Sub ConfigureButtons()

        PasteAccButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso _
            _FormManager.DataSource.ChronologyValidator.FinancialDataCanChange)

        nOkButton.Enabled = (Not _FormManager.DataSource Is Nothing)
        ApplyButton.Enabled = (Not _FormManager.DataSource Is Nothing)
        nCancelButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.DataSource.IsNew)

    End Sub

End Class