Imports ApskaitaObjects.Assets
Public Class F_OperationAmortization
    Implements ISupportsPrinting, IObjectEditForm

    Private Obj As OperationAmortization
    Private _AssetID As Integer = 0
    Private _OperationID As Integer = 0
    Private _FetchByJournalEntry As Boolean = False
    Private Loading As Boolean = True


    Public ReadOnly Property ObjectID() As Integer Implements IObjectEditForm.ObjectID
        Get
            If Not Obj Is Nothing AndAlso Not Obj.IsNew Then Return Obj.ID
            Return 0
        End Get
    End Property

    Public ReadOnly Property ObjectType() As System.Type Implements IObjectEditForm.ObjectType
        Get
            Return GetType(OperationAmortization)
        End Get
    End Property


    Public Sub New(ByVal operationID As Integer, ByVal fetchByJournalEntry As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _OperationID = operationID
        _FetchByJournalEntry = fetchByJournalEntry

    End Sub

    Public Sub New(ByVal assetID As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _AssetID = assetID

    End Sub


    Private Sub F_OperationAmortization_Activated(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.Activated

        If Me.WindowState = FormWindowState.Maximized AndAlso MyCustomSettings.AutoSizeForm Then _
            Me.WindowState = FormWindowState.Normal

        If Loading Then
            Loading = False
            Exit Sub
        End If

        If Not PrepareCache(Me, GetType(HelperLists.AccountInfoList)) Then Exit Sub

    End Sub

    Private Sub F_OperationAmortization_FormClosing(ByVal sender As Object, _
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

        GetFormLayout(Me)

    End Sub

    Private Sub F_OperationAmortization_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        Try
            If _OperationID > 0 Then
                Obj = LoadObject(Of OperationAmortization)(Nothing, _
                    "GetOperationAmortization", False, _OperationID, _FetchByJournalEntry)
            ElseIf _AssetID > 0 Then
                Obj = LoadObject(Of OperationAmortization)(Nothing, _
                    "NewOperationAmortization", False, _AssetID)
            Else
                Throw New Exception("Nenurodyta nei turto, nei operacijos ID.")
            End If

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

        BackgroundInfoPanel1.BindingSource.DataSource = OperationAmortizationBindingSource
        BackgroundInfoPanel1.BindingSource.DataMember = "Background"
        OperationAmortizationBindingSource.DataSource = Obj

        ConfigureButtons()

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


    Private Sub CalculateAmortizationButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles CalculateAmortizationButton.Click

        If Obj Is Nothing Then Exit Sub

        If Not YesOrNo("Gauti amortizacijos paskaičiavimus?") Then Exit Sub

        Try

            Dim calculation As LongTermAssetAmortizationCalculation _
                = LoadObject(Of LongTermAssetAmortizationCalculation)(Nothing, _
                "GetLongTermAssetAmortizationCalculation", True, _
                Obj.AssetID, Obj.ID, Obj.Date)
            Obj.SetAmortizationCalculation(calculation)

            MsgBox("Amortizacijos paskaičiavimai sėkmingai gauti.", _
                MsgBoxStyle.Information, "Info")

        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Private Sub LimitationsButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles LimitationsButton.Click
        If Obj Is Nothing OrElse StringIsNullOrEmpty(Obj.ChronologyValidator.LimitsExplanation) Then Exit Sub
        MsgBox(Obj.ChronologyValidator.LimitsExplanation, MsgBoxStyle.MsgBoxHelp, "Taikomi Ribojimai")
    End Sub

    Private Sub ViewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ViewJournalEntryButton.Click
        If Obj Is Nothing OrElse Not Obj.ID > 0 Then Exit Sub
        MDIParent1.LaunchForm(GetType(F_GeneralLedgerEntry), False, False, _
            Obj.JournalEntryID, Obj.JournalEntryID)
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
        If Obj Is Nothing Then Exit Sub

        Using frm As New F_SendObjToEmail(Obj, 0)
            frm.ShowDialog()
        End Using
    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick

        If Obj Is Nothing Then Exit Sub

        Try
            PrintObject(Obj, False, 0)
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick

        If Obj Is Nothing Then Exit Sub

        Try
            PrintObject(Obj, True, 0)
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function



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

        Using bm As New BindingsManager(OperationAmortizationBindingSource, _
            BackgroundInfoPanel1.BindingSource, Nothing, True, False)

            Try
                Obj = LoadObject(Of OperationAmortization)(Obj, "Save", False)
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
        Using bm As New BindingsManager(OperationAmortizationBindingSource, _
            BackgroundInfoPanel1.BindingSource, Nothing, True, True)
        End Using
    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, GetType(HelperLists.AccountInfoList)) Then Return False

        Try

            LoadAccountInfoListToGridCombo(AccountCostsAccGridComboBox, True, 6)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function

    Private Sub ConfigureButtons()

        DateDateTimePicker.Enabled = (Not Obj Is Nothing AndAlso Not Obj.DateIsReadOnly)
        DocumentNumberTextBox.ReadOnly = (Obj Is Nothing OrElse Obj.DocumentNumberIsReadOnly)
        AccountCostsAccGridComboBox.Enabled = (Not Obj Is Nothing AndAlso Not Obj.AccountCostsIsReadOnly)
        ContentTextBox.ReadOnly = (Obj Is Nothing OrElse Obj.ContentIsReadOnly)
        TotalValueChangeAccTextBox.ReadOnly = (Obj Is Nothing _
            OrElse Obj.TotalValueChangeIsReadOnly)
        UnitValueChangeAccTextBox.ReadOnly = (Obj Is Nothing _
            OrElse Obj.UnitValueChangeIsReadOnly)
        AmortizationCalculatedForMonthsAccTextBox.ReadOnly = (Obj Is Nothing _
            OrElse Obj.AmortizationCalculatedForMonthsIsReadOnly)
        RevaluedPortionTotalValueChangeAccTextBox.ReadOnly = (Obj Is Nothing _
            OrElse Obj.RevaluedPortionTotalValueChangeIsReadOnly)
        RevaluedPortionUnitValueChangeAccTextBox.ReadOnly = (Obj Is Nothing _
            OrElse Obj.RevaluedPortionUnitValueChangeIsReadOnly)
        CalculateAmortizationButton.Enabled = (Not Obj Is Nothing AndAlso _
            Not Obj.UnitValueChangeIsReadOnly)
        AmortizationCalculationsTextBox.ReadOnly = (Obj Is Nothing _
            OrElse Obj.AmortizationCalculationsIsReadOnly)

        nOkButton.Enabled = (Not Obj Is Nothing)
        ApplyButton.Enabled = (Not Obj Is Nothing)
        nCancelButton.Enabled = (Not Obj Is Nothing AndAlso Not Obj.IsNew)

        LimitationsButton.Visible = (Not Obj Is Nothing AndAlso _
            Not StringIsNullOrEmpty(Obj.ChronologyValidator.LimitsExplanation))

    End Sub

End Class