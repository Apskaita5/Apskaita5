Imports ApskaitaObjects.Assets
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccDataBindingsWinForms.Printing

Friend Class F_ComplexOperationValueChange
    Implements ISupportsPrinting, IObjectEditForm, ISupportsChronologicValidator

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of ComplexOperationValueChange)
    Private _ListViewManager As DataListViewEditControlManager(Of OperationValueChange)
    Private _QueryManager As CslaActionExtenderQueryObject

    Private _DocumentToEdit As ComplexOperationValueChange = Nothing


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
            Return GetType(ComplexOperationValueChange)
        End Get
    End Property


    Public Sub New(ByVal documentToEdit As ComplexOperationValueChange)

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


    Private Sub F_ComplexOperationValueChange_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If _DocumentToEdit Is Nothing Then
            _DocumentToEdit = ComplexOperationValueChange.NewComplexOperationValueChange()
        End If

        If Not SetDataSources() Then Exit Sub

        Try

            _FormManager = New CslaActionExtenderEditForm(Of ComplexOperationValueChange) _
                (Me, ComplexOperationValueChangeBindingSource, _DocumentToEdit, _
                Nothing, nOkButton, ApplyButton, nCancelButton, Nothing, ProgressFiller1)

            _FormManager.ManageDataListViewStates(ItemsDataListView)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

        ConfigureButtons()

    End Sub

    Private Function SetDataSources() As Boolean

        Try

            _ListViewManager = New DataListViewEditControlManager(Of OperationValueChange) _
                (ItemsDataListView, Nothing, AddressOf OnItemsDelete, _
                 Nothing, Nothing, _DocumentToEdit)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            SetupDefaultControls(Of ComplexOperationValueChange) _
                (Me, ComplexOperationValueChangeBindingSource, _DocumentToEdit)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Sub OnItemsDelete(ByVal items As OperationValueChange())
        If items Is Nothing OrElse items.Length < 1 OrElse _FormManager.DataSource Is Nothing Then Exit Sub
        For Each item As OperationValueChange In items
            If Not item.ChronologyValidator.FinancialDataCanChange Then
                MsgBox(String.Format("Klaida. Ilgalaikio turto {0} balansinės vertės pakeitimo (pervertinimo) operacijos pašalinti iš dokumento negalima:{1}{2}", _
                    item.AssetName, vbCrLf, item.ChronologyValidator.FinancialDataCanChangeExplanation))
                Exit Sub
            ElseIf Not item.IsNew AndAlso Not _FormManager.DataSource.ChronologyValidator.FinancialDataCanChange Then
                MsgBox(String.Format("Klaida. Ilgalaikio turto balansinės vertės pakeitimo (pervertinimo) operacijų pašalinti iš dokumento negalima:{0}{1}", _
                    vbCrLf, _FormManager.DataSource.ChronologyValidator.FinancialDataCanChangeExplanation))
                Exit Sub
            End If
        Next
        For Each item As OperationValueChange In items
            _FormManager.DataSource.Items.Remove(item)
        Next
    End Sub

    Private Sub ViewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ViewJournalEntryButton.Click
        If _FormManager.DataSource Is Nothing OrElse Not _FormManager.DataSource.JournalEntryID > 0 Then Exit Sub
        OpenJournalEntryEditForm(_QueryManager, _FormManager.DataSource.JournalEntryID)
    End Sub

    Private Sub CreateNewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles CreateNewJournalEntryButton.Click

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Dim result As General.JournalEntry = Nothing

        Try
            result = _FormManager.DataSource.NewJournalEntry()
        Catch ex As Exception
            ShowError(ex, Nothing)
        End Try

        If Not result Is Nothing Then
            OpenObjectEditForm(result)
        End If

    End Sub

    Private Sub AttachNewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AttachNewJournalEntryButton.Click

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Using dlg As New F_JournalEntryInfoList(_FormManager.DataSource.Date.AddMonths(-1), _
            _FormManager.DataSource.Date.AddMonths(1), True)

            If dlg.ShowDialog() <> DialogResult.OK OrElse dlg.SelectedEntries Is Nothing _
                OrElse dlg.SelectedEntries.Count < 1 Then Exit Sub

            Try
                _FormManager.DataSource.LoadAssociatedJournalEntry(dlg.SelectedEntries(0))
            Catch ex As Exception
                ShowError(ex, New Object() {_FormManager.DataSource, dlg.SelectedEntries})
            End Try

        End Using

    End Sub

    Private Sub AddItemsButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AddItemsButton.Click

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Dim ids As Integer() = AssetOperationManager.RequestUserToChooseAssets()

        If ids Is Nothing OrElse ids.Length < 1 Then Exit Sub

        ' OperationValueChangeList.NewOperationValueChangeList(ids, _FormManager.DataSource.ChronologyValidator)
        _QueryManager.InvokeQuery(Of OperationValueChangeList)(Nothing, "NewOperationValueChangeList", True, _
            AddressOf OnNewItemsFetched, ids, _FormManager.DataSource.ChronologyValidator)

    End Sub

    Private Sub OnNewItemsFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        Try
            _FormManager.DataSource.AddRange(DirectCast(result, OperationValueChangeList))
        Catch ex As Exception
            ShowError(ex, New Object() {_FormManager.DataSource, result})
            Exit Sub
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
            PrintObject(_FormManager.DataSource, False, 0, "ITVertinimas", Me, "")
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "ITVertinimas", Me, "")
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
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

        DateAccDatePicker.ReadOnly = _FormManager.DataSource Is Nothing
        ContentTextBox.ReadOnly = _FormManager.DataSource Is Nothing

        AttachNewJournalEntryButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso _
            Not _FormManager.DataSource.AssociatedJournalEntryIsReadOnly)
        CreateNewJournalEntryButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso _
            Not _FormManager.DataSource.AssociatedJournalEntryIsReadOnly)

        _ListViewManager.SetColumnReadOnly("ValueChangeTotal", (_FormManager.DataSource Is Nothing OrElse _
            Not _FormManager.DataSource.ChronologyValidator.FinancialDataCanChange))

        nOkButton.Enabled = (Not _FormManager.DataSource Is Nothing)
        ApplyButton.Enabled = (Not _FormManager.DataSource Is Nothing)
        nCancelButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.DataSource.IsNew)

    End Sub

End Class
