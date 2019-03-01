Imports ApskaitaObjects.Assets
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.Printing
Imports AccDataBindingsWinForms.CachedInfoLists

Friend Class F_OperationOperationalStatusChange
    Implements ISupportsPrinting, IObjectEditForm, ISupportsChronologicValidator

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of OperationOperationalStatusChange)
    Private _QueryManager As CslaActionExtenderQueryObject

    Private _DocumentToEdit As OperationOperationalStatusChange = Nothing
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
            Return GetType(OperationOperationalStatusChange)
        End Get
    End Property


    Public Sub New(ByVal documentToEdit As OperationOperationalStatusChange)

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


    Private Sub F_OperationOperationalStatusChange_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Dim beginOperationalPeriod As Boolean

        If _DocumentToEdit Is Nothing AndAlso Not _AssetID > 0 Then
            MsgBox("Klaida. Nenurodytas ilgalaikis turtas.", MsgBoxStyle.Exclamation, "Klaida")
            Me.BeginInvoke(New MethodInvoker(AddressOf Me.Close))
            Exit Sub
        ElseIf _DocumentToEdit Is Nothing Then
            Dim answ As String = Ask("Įvedimas į eksploataciją ar išvedimas iš eksploatacijos?", _
                New ButtonStructure("Įvedimas", "Naujas ilgalaikio turto įvedimo į eksploataciją aktas."), _
                New ButtonStructure("Išvedimas", "Naujas ilgalaikio turto išvedimo iš eksploatacijos aktas."), _
                New ButtonStructure("Atšaukti", "Nieko nedaryti."))
            If answ = "Įvedimas" OrElse answ = "Išvedimas" Then
                beginOperationalPeriod = (answ = "Įvedimas")
            Else
                Me.BeginInvoke(New MethodInvoker(AddressOf Me.Close))
                Exit Sub
            End If
        End If

        If Not SetDataSources() Then Exit Sub

        If _DocumentToEdit Is Nothing Then
            'OperationOperationalStatusChange.NewOperationOperationalStatusChange(_AssetID, beginOperationalPeriod)
            _QueryManager.InvokeQuery(Of OperationOperationalStatusChange)(Nothing, _
                "NewOperationOperationalStatusChange", True, AddressOf OnNewOperationLoaded, _
                _AssetID, beginOperationalPeriod)
        Else
            InitializeFormManager()
        End If

    End Sub

    Private Function SetDataSources() As Boolean

        Try

            CType(BackgroundInfoPanel1.GetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            BackgroundInfoPanel1.GetBindingSource.DataMember = "Background"
            BackgroundInfoPanel1.GetBindingSource.DataSource = OperationOperationalStatusChangeBindingSource
            CType(BackgroundInfoPanel1.GetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            SetupDefaultControls(Of OperationOperationalStatusChange) _
                (Me, OperationOperationalStatusChangeBindingSource, _DocumentToEdit)

            SetupDefaultControls(Of OperationBackground) _
                (Me, BackgroundInfoPanel1.GetBindingSource(), _DocumentToEdit)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function

    Private Sub InitializeFormManager()

        Try
            _FormManager = New CslaActionExtenderEditForm(Of OperationOperationalStatusChange) _
                (Me, OperationOperationalStatusChangeBindingSource, _DocumentToEdit, _
                Nothing, nOkButton, ApplyButton, nCancelButton, Nothing, ProgressFiller1)
        Catch ex As Exception
            ShowError(ex, Nothing)
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

        _DocumentToEdit = DirectCast(result, OperationOperationalStatusChange)

        InitializeFormManager()

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
            PrintObject(_FormManager.DataSource, False, 0, "EksploatacijosAktas", Me, "")
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try

    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Try
            PrintObject(_FormManager.DataSource, True, 0, "EksploatacijosAktas", Me, "")
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

        DateAccDatePicker.ReadOnly = (_FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.DateIsReadOnly)
        DocumentNumberTextBox.ReadOnly = (_FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.DocumentNumberIsReadOnly)
        ContentTextBox.ReadOnly = (_FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.ContentIsReadOnly)

        nOkButton.Enabled = (Not _FormManager.DataSource Is Nothing)
        ApplyButton.Enabled = (Not _FormManager.DataSource Is Nothing)
        nCancelButton.Enabled = (Not _FormManager.DataSource Is Nothing AndAlso Not _FormManager.DataSource.IsNew)

    End Sub

End Class