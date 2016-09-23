Imports ApskaitaObjects.HelperLists
Imports ApskaitaObjects.Workers
Imports AccControlsWinForms
Imports ApskaitaObjects.Settings
Imports AccDataBindingsWinForms.CachedInfoLists

Friend Class F_PayOutNaturalPerson
    Implements IObjectEditForm

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(TaxRateInfoList), GetType(CodeInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of PayOutNaturalPerson)
    Private _QueryManager As CslaActionExtenderQueryObject
    Private _PayOutToEdit As PayOutNaturalPerson = Nothing


    Public ReadOnly Property ObjectID() As Integer _
        Implements IObjectEditForm.ObjectID
        Get
            If _FormManager Is Nothing OrElse _FormManager.DataSource Is Nothing Then
                If _PayOutToEdit Is Nothing OrElse _PayOutToEdit.IsNew Then
                    Return Integer.MinValue
                Else
                    Return _PayOutToEdit.ID
                End If
            ElseIf _FormManager.DataSource.IsNew Then
                Return Integer.MinValue
            End If
            Return _FormManager.DataSource.ID
        End Get
    End Property

    Public ReadOnly Property ObjectType() As System.Type _
        Implements IObjectEditForm.ObjectType
        Get
            Return GetType(PayOutNaturalPerson)
        End Get
    End Property


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal payOutToEdit As PayOutNaturalPerson)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _PayOutToEdit = payOutToEdit

    End Sub


    Private Sub F_PayOutNaturalPerson_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        Try

            If _PayOutToEdit Is Nothing Then
                _PayOutToEdit = PayOutNaturalPerson.NewPayOutNaturalPerson()
            End If

            _FormManager = New CslaActionExtenderEditForm(Of PayOutNaturalPerson)(Me, _
                PayOutNaturalPersonBindingSource, _PayOutToEdit, _RequiredCachedLists, _
                IOkButton, IApplyButton, ICancelButton, Nothing, ProgressFiller1)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

        IOkButton.Enabled = ((_PayOutToEdit.IsNew AndAlso PayOutNaturalPerson.CanAddObject) _
            OrElse (Not _PayOutToEdit.IsNew AndAlso PayOutNaturalPerson.CanEditObject))
        IApplyButton.Enabled = ((_PayOutToEdit.IsNew AndAlso PayOutNaturalPerson.CanAddObject) _
            OrElse (Not _PayOutToEdit.IsNew AndAlso PayOutNaturalPerson.CanEditObject))
        RefreshJournalEntryListButton.Enabled = ((_PayOutToEdit.IsNew AndAlso PayOutNaturalPerson.CanAddObject) _
            OrElse (Not _PayOutToEdit.IsNew AndAlso PayOutNaturalPerson.CanEditObject))
        LoadJournalEntryButton.Enabled = ((_PayOutToEdit.IsNew AndAlso PayOutNaturalPerson.CanAddObject) _
            OrElse (Not _PayOutToEdit.IsNew AndAlso PayOutNaturalPerson.CanEditObject))

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            SetupDefaultControls(Of PayOutNaturalPerson)(Me, _
                PayOutNaturalPersonBindingSource, _PayOutToEdit)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Sub RefreshJournalEntryListButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshJournalEntryListButton.Click

        'ActiveReports.JournalEntryInfoList.GetList(JournalEntryListDateDateTimePicker.Value, _
        '    JournalEntryListDateDateTimePicker.Value, "", -1, -1, DocumentType.None, False, "", "")
        _QueryManager.InvokeQuery(Of ActiveReports.JournalEntryInfoList)(Nothing, "GetList", True, _
            AddressOf OnJournalEntryInfoListRefreshed, JournalEntryListDateDateTimePicker.Value, _
            JournalEntryListDateDateTimePicker.Value, "", -1, -1, DocumentType.None, False, "", "")

    End Sub

    Private Sub OnJournalEntryInfoListRefreshed(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        Dim datasource As New Csla.FilteredBindingList(Of ActiveReports.JournalEntryInfo) _
            (DirectCast(result, ActiveReports.JournalEntryInfoList), AddressOf DocumentTypeFilter)

        Dim allowedDocTypes(2) As DocumentType
        allowedDocTypes(0) = DocumentType.BankOperation
        allowedDocTypes(1) = DocumentType.None
        allowedDocTypes(2) = DocumentType.TillSpendingOrder
        datasource.ApplyFilter("DocType", allowedDocTypes)

        JournalEntryListComboBox.DataSource = Nothing
        JournalEntryListComboBox.DataSource = datasource
        JournalEntryListComboBox.SelectedIndex = -1

    End Sub

    Private Sub LoadJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles LoadJournalEntryButton.Click

        If JournalEntryListComboBox.SelectedItem Is Nothing Then
            MsgBox("Klaida. Nepasirinkta BŽ operacija.", MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        Try
            Using busy As New StatusBusy
                _FormManager.DataSource.LoadAssociatedJournalEntry(DirectCast(JournalEntryListComboBox.SelectedItem,  _
                    ActiveReports.JournalEntryInfo))
            End Using
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub


    Private Sub ViewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ViewJournalEntryButton.Click

        If _FormManager.DataSource Is Nothing Then Exit Sub

        If _FormManager.DataSource.JournalEntryID > 0 Then

            OpenJournalEntryEditForm(_QueryManager, _FormManager.DataSource.JournalEntryID)

        Else

            OpenObjectEditForm(_FormManager.DataSource.GetNewJournalEntry())

        End If

    End Sub


    Public Function DocumentTypeFilter(ByVal item As Object, ByVal filterValue As Object) As Boolean

        If filterValue Is Nothing OrElse Not TypeOf filterValue Is DocumentType() _
            OrElse Not CType(filterValue, DocumentType()).Length > 0 Then Return True

        Dim flt() As DocumentType = CType(filterValue, DocumentType())

        If Not Array.IndexOf(flt, CType(item, DocumentType)) < 0 Then Return True

        Return False

    End Function

End Class