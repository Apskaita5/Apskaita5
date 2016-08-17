Imports ApskaitaObjects.Assets
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccDataBindingsWinForms.Printing

Friend Class F_ComplexOperationDiscard
    Implements ISupportsPrinting, IObjectEditForm, ISupportsChronologicValidator

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.AccountInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of ComplexOperationDiscard)
    Private _ListViewManager As DataListViewEditControlManager(Of OperationDiscard)
    Private _QueryManager As CslaActionExtenderQueryObject

    Private _DocumentToEdit As ComplexOperationDiscard = Nothing


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
            Return GetType(ComplexOperationDiscard)
        End Get
    End Property


    Public Sub New(ByVal documentToEdit As ComplexOperationDiscard)

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


    Private Sub F_ComplexOperationDiscard_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If _DocumentToEdit Is Nothing Then
            _DocumentToEdit = ComplexOperationDiscard.NewComplexOperationDiscard()
        End If

        If Not SetDataSources() Then Exit Sub

        Try

            _FormManager = New CslaActionExtenderEditForm(Of ComplexOperationDiscard) _
                (Me, ComplexOperationDiscardBindingSource, _DocumentToEdit, _
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

            _ListViewManager = New DataListViewEditControlManager(Of OperationDiscard) _
                (ItemsDataListView, Nothing, AddressOf OnItemsDelete, Nothing, Nothing)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            SetupDefaultControls(Of ComplexOperationDiscard)(Me, ComplexOperationDiscardBindingSource)

            LoadAccountInfoListToListCombo(CostsAccountAccGridComboBox, True, 6)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Sub OnItemsDelete(ByVal items As OperationDiscard())
        If items Is Nothing OrElse items.Length < 1 OrElse _FormManager.DataSource Is Nothing Then Exit Sub
        For Each item As OperationDiscard In items
            If Not item.ChronologyValidator.FinancialDataCanChange Then
                MsgBox(String.Format("Klaida. Ilgalaikio turto {0} nurašymo pašalinti iš dokumento negalima:{1}{2}", _
                    item.AssetName, vbCrLf, item.ChronologyValidator.FinancialDataCanChangeExplanation))
                Exit Sub
            ElseIf Not item.IsNew AndAlso Not _FormManager.DataSource.ChronologyValidator.FinancialDataCanChange Then
                MsgBox(String.Format("Klaida. Ilgalaikio turto nurašymo pašalinti iš dokumento negalima:{0}{1}", _
                    vbCrLf, _FormManager.DataSource.ChronologyValidator.FinancialDataCanChangeExplanation))
                Exit Sub
            End If
        Next
        For Each item As OperationDiscard In items
            _FormManager.DataSource.Items.Remove(item)
        Next
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

        ' OperationDiscardList.NewOperationDiscardList(ids, _FormManager.DataSource.ChronologyValidator)
        _QueryManager.InvokeQuery(Of OperationDiscardList)(Nothing, "NewOperationDiscardList", True, _
            AddressOf OnNewItemsFetched, ids, _FormManager.DataSource.ChronologyValidator)

    End Sub

    Private Sub OnNewItemsFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        Try
            _FormManager.DataSource.AddRange(DirectCast(result, OperationDiscardList))
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

    End Sub

    Private Sub ApplyCommonCostsAccountButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ApplyCommonCostsAccountButton.Click

        If _FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.Items.Count < 1 Then Exit Sub

        Dim selectedAccount As Long = 0
        Try
            selectedAccount = DirectCast(CostsAccountAccGridComboBox.SelectedValue, Long)
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
            PrintObject(_FormManager.DataSource, False, 0, "ITNurasymas", Me, "")
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "ITNurasymas", Me, "")
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

        ApplyCommonCostsAccountButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso _
            _FormManager.DataSource.ChronologyValidator.FinancialDataCanChange AndAlso _
            _FormManager.DataSource.ChronologyValidator.ChildrenFinancialDataCanChange)
        CostsAccountAccGridComboBox.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso _
            _FormManager.DataSource.ChronologyValidator.FinancialDataCanChange AndAlso _
            _FormManager.DataSource.ChronologyValidator.ChildrenFinancialDataCanChange)

        _ListViewManager.SetColumnReadOnly("AmountToDiscard", (_FormManager.DataSource Is Nothing OrElse _
            Not _FormManager.DataSource.ChronologyValidator.FinancialDataCanChange))
        _ListViewManager.SetColumnReadOnly("AccountCosts", (_FormManager.DataSource Is Nothing OrElse _
            Not _FormManager.DataSource.ChronologyValidator.FinancialDataCanChange))

        nOkButton.Enabled = (Not _FormManager.DataSource Is Nothing)
        ApplyButton.Enabled = (Not _FormManager.DataSource Is Nothing)
        nCancelButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.DataSource.IsNew)

    End Sub

End Class