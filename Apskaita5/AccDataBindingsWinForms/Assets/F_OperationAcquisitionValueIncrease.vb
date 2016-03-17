﻿Imports ApskaitaObjects.Assets
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccDataBindingsWinForms.Printing

Friend Class F_OperationAcquisitionValueIncrease
    Implements ISupportsPrinting, IObjectEditForm

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.AccountInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of OperationAcquisitionValueIncrease)
    Private _QueryManager As CslaActionExtenderQueryObject

    Private _DocumentToEdit As OperationAcquisitionValueIncrease = Nothing
    Private _AssetID As Integer = 0


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
            Return GetType(OperationAcquisitionValueIncrease)
        End Get
    End Property


    Public Sub New(ByVal documentToEdit As OperationAcquisitionValueIncrease)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _DocumentToEdit = documentToEdit

    End Sub

    Public Sub New(ByVal assetID As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _AssetID = assetID

    End Sub


    Private Sub F_OperationAcquisitionValueIncrease_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If _DocumentToEdit Is Nothing AndAlso Not _AssetID > 0 Then
            MsgBox("Klaida. Nenurodytas ilgalaikis turtas.", MsgBoxStyle.Exclamation, "Klaida")
            Me.BeginInvoke(New MethodInvoker(AddressOf Me.Close))
            Exit Sub
        End If

        If Not SetDataSources() Then Exit Sub

        If _DocumentToEdit Is Nothing Then
            'OperationAcquisitionValueIncrease.NewOperationAcquisitionValueIncrease(_AssetID)
            _QueryManager.InvokeQuery(Of OperationAcquisitionValueIncrease)(Nothing, _
                "NewOperationAcquisitionValueIncrease", True, AddressOf OnNewOperationLoaded, _AssetID)
        Else
            InitializeFormManager()
        End If

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            CType(BackgroundInfoPanel1.GetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            BackgroundInfoPanel1.GetBindingSource.DataMember = "Background"
            BackgroundInfoPanel1.GetBindingSource.DataSource = OperationAcquisitionValueIncreaseBindingSource
            CType(BackgroundInfoPanel1.GetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            SetupDefaultControls(Of OperationAcquisitionValueIncrease)(Me, OperationAcquisitionValueIncreaseBindingSource)

            SetupDefaultControls(Of OperationBackground)(Me, BackgroundInfoPanel1.GetBindingSource())

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function

    Private Sub InitializeFormManager()

        Try
            _FormManager = New CslaActionExtenderEditForm(Of OperationAcquisitionValueIncrease) _
                (Me, OperationAcquisitionValueIncreaseBindingSource, _DocumentToEdit, _
                _RequiredCachedLists, nOkButton, ApplyButton, nCancelButton, _
                LimitationsButton, ProgressFiller1)
        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

        ConfigureButtons()

    End Sub

    Private Sub OnNewOperationLoaded(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If exceptionHandled Then
            DisableAllControls(Me)
            Exit Sub
        ElseIf result Is Nothing Then
            MsgBox("Klaida. Dėl nežinomų priežasčių nepavyko sukurti naujos operacijos.", _
                MsgBoxStyle.Exclamation, "Klaida")
            DisableAllControls(Me)
            Exit Sub
        End If

        _DocumentToEdit = DirectCast(result, OperationAcquisitionValueIncrease)

        InitializeFormManager()

    End Sub


    Private Sub ViewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ViewJournalEntryButton.Click
        If _FormManager.DataSource Is Nothing OrElse Not _FormManager.DataSource.JournalEntryID > 0 Then Exit Sub
        OpenJournalEntryEditForm(_QueryManager, _FormManager.DataSource.JournalEntryID)
    End Sub

    Private Sub RefreshJournalEntryInfoListButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshJournalEntryInfoListButton.Click

        If _FormManager.DataSource Is Nothing OrElse _FormManager.IsChild Then Exit Sub

        'ActiveReports.JournalEntryInfoList.GetList(_FormManager.DataSource.Date, _
        '    _FormManager.DataSource.Date, "", -1, -1, DocumentType.None, False, "", "")
        _QueryManager.InvokeQuery(Of ActiveReports.JournalEntryInfoList)(Nothing, "GetList", True, _
            AddressOf OnJournalEntryInfoListFetched, _FormManager.DataSource.Date, _
            _FormManager.DataSource.Date, "", -1, -1, DocumentType.None, False, "", "")

    End Sub

    Private Sub OnJournalEntryInfoListFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        JournalEntryInfoListComboBox.DataSource = DirectCast(result, ActiveReports.JournalEntryInfoList)

    End Sub

    Private Sub AttachNewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AttachNewJournalEntryButton.Click

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Dim selectedEntry As ActiveReports.JournalEntryInfo = Nothing

        Try
            selectedEntry = DirectCast(JournalEntryInfoListComboBox.SelectedItem,  _
                ActiveReports.JournalEntryInfo)
        Catch ex As Exception
        End Try

        If selectedEntry Is Nothing Then
            MsgBox("Klaida. Nepasirinktas dokumentas (bendrojo žurnalo operacija).", _
                MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        Try
            _FormManager.DataSource.LoadAssociatedJournalEntry(selectedEntry)
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
            PrintObject(_FormManager.DataSource, False, 0, "ITSavikainosPadidinimas", Me, "")
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Try
            PrintObject(_FormManager.DataSource, True, 0, "ITSavikainosPadidinimas", Me, "")
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function


    Private Sub _FormManager_DataSourceStateHasChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles _FormManager.DataSourceStateHasChanged
        ConfigureButtons()
    End Sub

    Private Sub ConfigureButtons()

        DateDateTimePicker.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.DataSource.DateIsReadOnly)
        ContentTextBox.ReadOnly = (_FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.ContentIsReadOnly)
        ValueIncreaseAccTextBox.ReadOnly = (_FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.ValueIncreaseIsReadOnly)

        RefreshJournalEntryInfoListButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.DataSource.AssociatedJournalEntryIsReadOnly)
        AttachNewJournalEntryButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.DataSource.AssociatedJournalEntryIsReadOnly)

        nOkButton.Enabled = (Not _FormManager.DataSource Is Nothing)
        ApplyButton.Enabled = (Not _FormManager.DataSource Is Nothing)
        nCancelButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.DataSource.IsNew)

    End Sub

End Class