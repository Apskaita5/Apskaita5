Imports ApskaitaObjects.Assets
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccDataBindingsWinForms.Printing

Friend Class F_ComplexOperationAmortization
    Implements ISupportsPrinting, IObjectEditForm, ISupportsChronologicValidator

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.AccountInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of ComplexOperationAmortization)
    Private _ListViewManager As DataListViewEditControlManager(Of OperationAmortization)
    Private _QueryManager As CslaActionExtenderQueryObject

    Private _DocumentToEdit As ComplexOperationAmortization = Nothing


    Public ReadOnly Property ObjectID() As Integer Implements IObjectEditForm.ObjectID
        Get
            If _FormManager Is Nothing OrElse _FormManager.DataSource Is Nothing Then
                If _DocumentToEdit Is Nothing OrElse _DocumentToEdit.IsNew Then
                    Return Integer.MinValue
                Else
                    Return _DocumentToEdit.ID
                End If
            End If
            Return _FormManager.DataSource.ID
        End Get
    End Property

    Public ReadOnly Property ObjectType() As System.Type Implements IObjectEditForm.ObjectType
        Get
            Return GetType(ComplexOperationAmortization)
        End Get
    End Property


    Public Sub New(ByVal documentToEdit As ComplexOperationAmortization)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _DocumentToEdit = documentToEdit

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub F_OperationAmortization_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If _DocumentToEdit Is Nothing Then
            _DocumentToEdit = ComplexOperationAmortization.NewComplexOperationAmortization()
        End If

        If Not SetDataSources() Then Exit Sub

        Try

            _FormManager = New CslaActionExtenderEditForm(Of ComplexOperationAmortization) _
                (Me, ComplexOperationAmortizationBindingSource, _DocumentToEdit, _
                _RequiredCachedLists, nOkButton, ApplyButton, nCancelButton, _
                Nothing, ProgressFiller1)

            _FormManager.ManageDataListViewStates(ItemsDataListView)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

        ConfigureButtons()

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of OperationAmortization) _
                (ItemsDataListView, Nothing, AddressOf OnItemsDelete, Nothing, Nothing)

            _ListViewManager.AddCancelButton = False
            _ListViewManager.AddButtonHandler("Paskaičiuoti", _
                "Gauti amortizacijos paskaičiavimus", AddressOf OnItemClicked)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            SetupDefaultControls(Of ComplexOperationAmortization)(Me, ComplexOperationAmortizationBindingSource)

            LoadAccountInfoListToListCombo(CommonCostsAccountAccGridComboBox, True, 2, 3, 6)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Sub OnItemsDelete(ByVal items As OperationAmortization())
        If items Is Nothing OrElse items.Length < 1 OrElse _FormManager.DataSource Is Nothing Then Exit Sub
        For Each item As OperationAmortization In items
            If Not item.ChronologyValidator.FinancialDataCanChange Then
                MsgBox(String.Format("Klaida. Ilgalaikio turto {0} amortizacijos paskaičiavimo pašalinti iš dokumento negalima:{1}{2}", _
                    item.AssetName, vbCrLf, item.ChronologyValidator.FinancialDataCanChangeExplanation))
                Exit Sub
            ElseIf Not item.IsNew AndAlso Not _FormManager.DataSource.ChronologyValidator.FinancialDataCanChange Then
                MsgBox(String.Format("Klaida. Ilgalaikio turto amortizacijos paskaičiavimų pašalinti iš dokumento negalima:{0}{1}", _
                    vbCrLf, _FormManager.DataSource.ChronologyValidator.FinancialDataCanChangeExplanation))
                Exit Sub
            End If
        Next
        For Each item As OperationAmortization In items
            _FormManager.DataSource.Items.Remove(item)
        Next
    End Sub

    Private Sub OnItemClicked(ByVal item As OperationAmortization)

        If item Is Nothing Then Exit Sub

        If Not YesOrNo(String.Format("Gauti amortizacijos paskaičiavimą turtui ""{0}""?", _
            item.AssetName)) Then Exit Sub

        'LongTermAssetAmortizationCalculation.GetLongTermAssetAmortizationCalculation( _
        '    item.AssetID, item.ID, _FormManager.DataSource.Date)
        _QueryManager.InvokeQuery(Of LongTermAssetAmortizationCalculation)(Nothing, _
            "GetLongTermAssetAmortizationCalculation", True, AddressOf OnCalculationsFetched, _
            item.AssetID, item.ID, _FormManager.DataSource.Date)

    End Sub

    Private Sub CalculateAmortizationButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles GetCalculationsButton.Click

        If _FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.Items.Count < 1 Then Exit Sub

        If Not YesOrNo("Gauti amortizacijos paskaičiavimus visoms eilutėms?") Then Exit Sub

        '_FormManager.DataSource.GetCalculations()
        _QueryManager.InvokeQuery(Of ComplexOperationAmortization)(_FormManager.DataSource, _
            "GetCalculations", True, AddressOf OnCalculationsFetched)

    End Sub

    Private Sub OnCalculationsFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        Dim warnings As String = ""

        Try
            If TypeOf result Is LongTermAssetAmortizationCalculation Then
                _FormManager.DataSource.SetCalculations(DirectCast(result, LongTermAssetAmortizationCalculation))
            Else
                _FormManager.DataSource.SetCalculations(DirectCast(result, LongTermAssetAmortizationCalculationList), warnings)
            End If
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

        If StringIsNullOrEmpty(warnings) Then
            MsgBox("Amortizacijos paskaičiavimai sėkmingai gauti.", _
                MsgBoxStyle.Information, "Info")
        Else
            MsgBox("Amortizacijos paskaičiavimai gauti su klaidomis:" & vbCrLf & warnings, _
                MsgBoxStyle.Exclamation, "Įspėjimas")
        End If

    End Sub

    Private Sub ViewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ViewJournalEntryButton.Click
        If _FormManager.DataSource Is Nothing OrElse Not _FormManager.DataSource.JournalEntryID > 0 Then Exit Sub
        OpenJournalEntryEditForm(_QueryManager, _FormManager.DataSource.JournalEntryID)
    End Sub

    Private Sub AddItemsButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AddItemsButton.Click

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Dim ids As Integer() = AssetOperationManager.RequestUserToChooseAssets()

        If ids Is Nothing OrElse ids.Length < 1 Then Exit Sub

        'OperationAmortizationList.NewOperationAmortizationList(ids, _FormManager.DataSource.ChronologyValidator)
        _QueryManager.InvokeQuery(Of OperationAmortizationList)(Nothing, "NewOperationAmortizationList", True, _
            AddressOf OnNewItemsFetched, ids, _FormManager.DataSource.ChronologyValidator)

    End Sub

    Private Sub OnNewItemsFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        Try
            _FormManager.DataSource.AddRange(DirectCast(result, OperationAmortizationList))
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

    End Sub

    Private Sub SetCommonAccountCostsButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles SetCommonAccountCostsButton.Click

        If _FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.Items.Count < 1 Then Exit Sub

        Dim selectedAccount As Long = 0
        Try
            selectedAccount = DirectCast(CommonCostsAccountAccGridComboBox.SelectedValue, Long)
        Catch ex As Exception
        End Try

        If Not selectedAccount > 0 Then Exit Sub

        Try
            _FormManager.DataSource.SetCommonAccountCosts(selectedAccount)
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub


    Public Function GetMailDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetMailDropDownItems
        Return Nothing
    End Function

    Public Function GetPrintDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintDropDownItems
        Return Nothing
    End Function

    Public Function GetPrintPreviewDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintPreviewDropDownItems
        Return Nothing
    End Function

    Public Sub OnMailClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnMailClick
        If _FormManager.DataSource Is Nothing Then Exit Sub

        Using frm As New F_SendObjToEmail(_FormManager.DataSource, 0)
            frm.ShowDialog()
        End Using

    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, False, 0, "ITAmortizacija", Me, "")
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "ITAmortizacija", Me, "")
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function


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

        DateDateTimePicker.Enabled = Not _FormManager.DataSource Is Nothing
        DocumentNumberTextBox.ReadOnly = _FormManager.DataSource Is Nothing
        ContentTextBox.ReadOnly = _FormManager.DataSource Is Nothing

        GetCalculationsButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso _
            _FormManager.DataSource.ChronologyValidator.FinancialDataCanChange AndAlso _
            _FormManager.DataSource.ChronologyValidator.ParentFinancialDataCanChange)

        nOkButton.Enabled = (Not _FormManager.DataSource Is Nothing)
        ApplyButton.Enabled = (Not _FormManager.DataSource Is Nothing)
        nCancelButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.DataSource.IsNew)

    End Sub

End Class