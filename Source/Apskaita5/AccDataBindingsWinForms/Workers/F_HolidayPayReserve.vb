Imports ApskaitaObjects.Workers
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccDataBindingsWinForms.Printing

Friend Class F_HolidayPayReserve
    Implements ISupportsPrinting, IObjectEditForm, ISupportsChronologicValidator

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.AccountInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of HolidayPayReserve)
    Private _ListViewManager As DataListViewEditControlManager(Of HolidayPayReserveItem)
    Private _QueryManager As CslaActionExtenderQueryObject

    Private _DocumentToEdit As HolidayPayReserve = Nothing


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
            Return GetType(HolidayPayReserve)
        End Get
    End Property


    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal documentToEdit As HolidayPayReserve)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _DocumentToEdit = documentToEdit

    End Sub


    Private Sub F_HolidayPayReserve_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        If _DocumentToEdit Is Nothing Then

            _QueryManager.InvokeQuery(Of HolidayPayReserve)(Nothing, _
                "NewHolidayPayReserve", False, AddressOf OnDataSourceFetched, Today)

        Else

            InitializeFormManager()

        End If

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then
            DisableAllControls(Me)
            Return False
        End If

        Try

            _ListViewManager = New DataListViewEditControlManager(Of HolidayPayReserveItem) _
                (ItemsDataListView, Nothing, AddressOf OnItemsDelete, _
                 Nothing, Nothing, _DocumentToEdit)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            SetupDefaultControls(Of HolidayPayReserve)(Me, _
                HolidayPayReserveBindingSource, _DocumentToEdit)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function

    Private Sub InitializeFormManager()

        Try

            _FormManager = New CslaActionExtenderEditForm(Of HolidayPayReserve) _
                (Me, HolidayPayReserveBindingSource, _DocumentToEdit, _
                 _RequiredCachedLists, IOkButton, IApplyButton, ICancelButton, _
                 Nothing, ProgressFiller1)

            _FormManager.ManageDataListViewStates(ItemsDataListView)

        Catch ex As Exception
            ShowError(New Exception("Klaida. Nepavyko gauti atostogų rezervo pažymos duomenų.", ex), Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

        ConfigureButtons()

    End Sub

    Private Sub OnDataSourceFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If exceptionHandled Then
            DisableAllControls(Me)
            Exit Sub
        ElseIf result Is Nothing Then
            ShowError(New Exception("Klaida. Nepavyko gauti naujos atostogų rezervo pažymos duomenų."), Nothing)
            DisableAllControls(Me)
            Exit Sub
        End If

        _DocumentToEdit = DirectCast(result, HolidayPayReserve)

        InitializeFormManager()

    End Sub


    Private Sub OnItemsDelete(ByVal items As HolidayPayReserveItem())

        If _FormManager.DataSource Is Nothing OrElse items Is Nothing _
            OrElse items.Length < 1 Then Exit Sub

        If Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange Then
            MsgBox(String.Format("Klaida. Pašalinti eilučių neleidžiama:{0}{1}", _
                vbCrLf, _FormManager.DataSource.ChronologicValidator.FinancialDataCanChangeExplanation), _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        For Each item As HolidayPayReserveItem In items
            _FormManager.DataSource.Items.Remove(item)
        Next

    End Sub


    Private Sub NewButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles NewButton.Click

        _QueryManager.InvokeQuery(Of HolidayPayReserve)(Nothing, _
            "NewHolidayPayReserve", False, AddressOf OnNewDocumentFetched, _
            NewObjectDateAccDatePicker.Value)

    End Sub

    Private Sub OnNewDocumentFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        _FormManager.AddNewDataSource(DirectCast(result, HolidayPayReserve))

    End Sub


    Private Sub ViewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ViewJournalEntryButton.Click
        If _FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.IsNew Then Exit Sub
        OpenJournalEntryEditForm(_QueryManager, _FormManager.DataSource.ID)
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
            PrintObject(_FormManager.DataSource, False, 0, "AtostoguRezervoPazyma", Me, _
                _ListViewManager.GetCurrentFilterDescription(), _
                _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "AtostoguRezervoPazyma", Me, _
                _ListViewManager.GetCurrentFilterDescription(), _
                _ListViewManager.GetDisplayOrderIndexes())
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
        Return _FormManager.DataSource.ChronologicValidator.LimitsExplanation
    End Function

    Public Function HasChronologicContent() As Boolean _
        Implements ISupportsChronologicValidator.HasChronologicContent

        Return Not _FormManager.DataSource Is Nothing AndAlso _
            Not StringIsNullOrEmpty(_FormManager.DataSource.ChronologicValidator.LimitsExplanation)

    End Function


    Private Sub _FormManager_DataSourceStateHasChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles _FormManager.DataSourceStateHasChanged
        ConfigureButtons()
    End Sub

    Private Sub ConfigureButtons()

        If _FormManager.DataSource Is Nothing Then Exit Sub

        ICancelButton.Enabled = Not _FormManager.DataSource.IsNew

        NewButton.Enabled = HolidayPayReserve.CanAddObject
        NewObjectDateAccDatePicker.ReadOnly = Not HolidayPayReserve.CanAddObject

        _ListViewManager.SetColumnReadOnly("ApplicableVDUDaily", _
            Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange)
        _ListViewManager.SetColumnReadOnly("ApplicableUnusedHolidayDays", _
            Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange)
        _ListViewManager.SetColumnReadOnly("ApplicableWorkDaysRatio", _
            Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange)

        AccountCostsAccGridComboBox.Enabled = Not _FormManager.DataSource.AccountCostsIsReadOnly
        TaxRateAccTextBox.ReadOnly = _FormManager.DataSource.TaxRateIsReadOnly

    End Sub

End Class