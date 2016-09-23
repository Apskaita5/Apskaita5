Imports ApskaitaObjects.Goods
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists

Public Class F_GoodsOperationRedeemFromBuyer
    Implements IObjectEditForm, ISupportsChronologicValidator

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.AccountInfoList), GetType(HelperLists.WarehouseInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of GoodsOperationRedeemFromBuyer)
    Private _QueryManager As CslaActionExtenderQueryObject

    Private _DocumentToEdit As GoodsOperationRedeemFromBuyer = Nothing
    Private _GoodsID As Integer = 0


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
            Return GetType(GoodsOperationRedeemFromBuyer)
        End Get
    End Property


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal documentToEdit As GoodsOperationRedeemFromBuyer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _DocumentToEdit = documentToEdit

    End Sub

    Public Sub New(ByVal goodsID As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _GoodsID = goodsID

    End Sub


    Private Sub F_GoodsOperationRedeemFromBuyer_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If _DocumentToEdit Is Nothing Then
            Using frm As New F_NewGoodsOperation(Of GoodsOperationRedeemFromBuyer)(_GoodsID)
                frm.ShowDialog()
                _DocumentToEdit = frm.Result
            End Using
        End If

        If _DocumentToEdit Is Nothing Then
            Me.BeginInvoke(New MethodInvoker(AddressOf Me.Close))
            Exit Sub
        End If

        If Not SetDataSources() Then Exit Sub

        Try

            _FormManager = New CslaActionExtenderEditForm(Of GoodsOperationRedeemFromBuyer) _
                (Me, GoodsOperationRedeemFromBuyerBindingSource, _DocumentToEdit, _
                _RequiredCachedLists, nOkButton, ApplyButton, nCancelButton, _
                Nothing, ProgressFiller1)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

        ConfigureButtons()

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, GetType(HelperLists.WarehouseInfoList), GetType(HelperLists.AccountInfoList)) Then Return False

        Try

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            SetupDefaultControls(Of GoodsOperationRedeemFromBuyer)(Me, _
                GoodsOperationRedeemFromBuyerBindingSource, _DocumentToEdit)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Sub RefreshJournalEntryInfoButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshJournalEntryInfoButton.Click

        If _FormManager.DataSource Is Nothing OrElse _FormManager.IsChild Then Exit Sub

        'ActiveReports.JournalEntryInfoList.GetList(_FormManager.DataSource.Date, _
        '    _FormManager.DataSource.Date, "", -1, -1, DocumentType.None, False, "", "")
        _QueryManager.InvokeQuery(Of ActiveReports.JournalEntryInfoList)(Nothing, "GetList", True, _
            AddressOf OnJournalEntryInfoListFetched, _FormManager.DataSource.Date, _
            _FormManager.DataSource.Date, "", -1, -1, DocumentType.None, False, "", "")

    End Sub

    Private Sub OnJournalEntryInfoListFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        JournalEntryInfoComboBox.DataSource = DirectCast(result, ActiveReports.JournalEntryInfoList)

    End Sub

    Private Sub AttachJournalEntryInfoButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AttachJournalEntryInfoButton.Click

        If _FormManager.DataSource Is Nothing OrElse _FormManager.IsChild Then Exit Sub

        If JournalEntryInfoComboBox.SelectedItem Is Nothing Then Exit Sub

        Try
            _FormManager.DataSource.LoadAssociatedJournalEntry(CType(JournalEntryInfoComboBox.SelectedItem,  _
                ActiveReports.JournalEntryInfo))
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

    End Sub

    Private Sub ViewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ViewJournalEntryButton.Click
        If _FormManager.DataSource Is Nothing OrElse Not _FormManager.DataSource.JournalEntryID > 0 Then Exit Sub
        OpenJournalEntryEditForm(_QueryManager, _FormManager.DataSource.JournalEntryID)
    End Sub


    Public Function ChronologicContent() As String _
            Implements ISupportsChronologicValidator.ChronologicContent
        If _FormManager.DataSource Is Nothing Then Return ""
        Return _FormManager.DataSource.OperationLimitations.LimitsExplanation
    End Function

    Public Function HasChronologicContent() As Boolean _
        Implements ISupportsChronologicValidator.HasChronologicContent

        Return Not _FormManager.DataSource Is Nothing AndAlso _
            Not StringIsNullOrEmpty(_FormManager.DataSource.OperationLimitations.LimitsExplanation)

    End Function


  Private Sub ConfigureButtons()

        WarehouseAccGridComboBox.Enabled = Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.DataSource.WarehouseIsReadOnly
        UnitCostAccTextBox.ReadOnly = _FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.UnitCostIsReadOnly
        AmountAccTextBox.ReadOnly = _FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.AmountIsReadOnly
        TotalCostAccTextBox.ReadOnly = _FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.TotalCostIsReadOnly
        DateDateTimePicker.Enabled = Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.DataSource.DateIsReadOnly
        DescriptionTextBox.ReadOnly = _FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.DescriptionIsReadOnly
        AmountInWarehouseAccTextBox.ReadOnly = _FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.AmountInWarehouseIsReadOnly
        RedeemCostsAccountAccGridComboBox.Enabled = Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.DataSource.RedeemCostsAccountIsReadOnly

        ApplyButton.Enabled = Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.IsChild
        nCancelButton.Enabled = Not _FormManager.DataSource Is Nothing AndAlso (_FormManager.IsChild _
            OrElse Not _FormManager.DataSource.IsNew)

        AttachJournalEntryInfoButton.Enabled = Not _FormManager.DataSource Is Nothing AndAlso _
            Not _FormManager.DataSource.AssociatedJournalEntryIsReadOnly
        JournalEntryInfoComboBox.Enabled = Not _FormManager.DataSource Is Nothing AndAlso _
            Not _FormManager.DataSource.AssociatedJournalEntryIsReadOnly
        RefreshJournalEntryInfoButton.Enabled = Not _FormManager.DataSource Is Nothing AndAlso _
            Not _FormManager.DataSource.AssociatedJournalEntryIsReadOnly

        ViewJournalEntryButton.Visible = Not _FormManager.DataSource Is Nothing

    End Sub

End Class