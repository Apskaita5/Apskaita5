Imports ApskaitaObjects.Assets
Public Class F_OperationValueChange
    Implements ISupportsPrinting, IObjectEditForm

    Private Obj As OperationValueChange
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
            Return GetType(OperationValueChange)
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


    Private Sub F_OperationValueChange_Activated(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.Activated

        If Me.WindowState = FormWindowState.Maximized AndAlso MyCustomSettings.AutoSizeForm Then _
            Me.WindowState = FormWindowState.Normal

        If Loading Then
            Loading = False
            Exit Sub
        End If

        ' If Not PrepareCache(Me, GetType(HelperLists.AccountInfoList)) Then Exit Sub

    End Sub

    Private Sub F_OperationValueChange_FormClosing(ByVal sender As Object, _
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

    Private Sub F_OperationValueChange_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        Try
            If _OperationID > 0 Then
                Obj = LoadObject(Of OperationValueChange)(Nothing, _
                    "GetOperationValueChange", True, _OperationID, _FetchByJournalEntry)
            ElseIf _AssetID > 0 Then
                Obj = LoadObject(Of OperationValueChange)(Nothing, _
                    "NewOperationValueChange", True, _AssetID)
            Else
                Throw New Exception("Nenurodyta nei turto, nei operacijos ID.")
            End If
        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

        OperationValueChangeBindingSource.DataSource = Obj
        BackgroundInfoPanel1.GetBindingSource.DataSource = Obj.Background

        ConfigureButtons()

        SetFormLayout(Me)

    End Sub


    Private Sub OkButton_Click(ByVal sender As System.Object, _
           ByVal e As System.EventArgs) Handles nOkButton.Click

        If Obj Is Nothing Then Exit Sub

        If Not SaveObj() Then Exit Sub

        Me.Hide()
        Me.Close()

    End Sub

    Private Sub ApplyButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ApplyButton.Click
        If Obj Is Nothing Then Exit Sub
        If SaveObj() Then ConfigureButtons()
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        If Obj Is Nothing Then Exit Sub
        CancelObj()
    End Sub


    Private Sub LimitationsButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles LimitationsButton.Click
        If Obj Is Nothing OrElse StringIsNullOrEmpty(Obj.ChronologyValidator.LimitsExplanation) Then Exit Sub
        MsgBox(Obj.ChronologyValidator.LimitsExplanation, MsgBoxStyle.MsgBoxHelp, "Taikomi Ribojimai")
    End Sub

    Private Sub ViewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ViewJournalEntryButton.Click
        If Obj Is Nothing OrElse Not Obj.JournalEntryID > 0 Then Exit Sub
        MDIParent1.LaunchForm(GetType(F_GeneralLedgerEntry), False, False, _
            Obj.JournalEntryID, Obj.JournalEntryID)
    End Sub

    Private Sub RefreshJournalEntryInfoListButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshJournalEntryInfoListButton.Click

        If Obj Is Nothing Then Exit Sub

        Try

            Dim list As ActiveReports.JournalEntryInfoList

            list = LoadObject(Of ActiveReports.JournalEntryInfoList)(Nothing, "GetList", _
                True, Obj.Date, Obj.Date, "", -1, -1, DocumentType.None, False, "", "")

            JournalEntryInfoListComboBox.DataSource = list

        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Private Sub CreateNewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles CreateNewJournalEntryButton.Click

        If Obj Is Nothing Then Exit Sub

        Dim result As General.JournalEntry = Nothing

        Try
            result = Obj.NewJournalEntry()
        Catch ex As Exception
            ShowError(ex)
        End Try

        If Not result Is Nothing Then
            MDIParent1.LaunchForm(GetType(F_GeneralLedgerEntry), False, False, 0, result)
        End If

    End Sub

    Private Sub AttachNewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AttachNewJournalEntryButton.Click

        If Obj Is Nothing Then Exit Sub

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
            Obj.LoadAssociatedJournalEntry(selectedEntry)
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



        Using cm As New BindingsManager(BackgroundInfoPanel1.GetBindingSource, _
            Nothing, Nothing, True, False)

            Using bm As New BindingsManager(OperationValueChangeBindingSource, _
                Nothing, Nothing, True, False)

                Try
                    Obj = LoadObject(Of OperationValueChange)(Obj, "Save", False)
                Catch ex As Exception
                    ShowError(ex)
                    Return False
                End Try

                bm.SetNewDataSource(Obj)
                cm.SetNewDataSource(Obj.Background)

            End Using

        End Using

        MsgBox(Answer, MsgBoxStyle.Information, "Info")

        Return True

    End Function

    Private Sub CancelObj()
        If Obj Is Nothing OrElse Obj.IsNew OrElse Not Obj.IsDirty Then Exit Sub
        Using bm As New BindingsManager(OperationValueChangeBindingSource, _
            BackgroundInfoPanel1.GetBindingSource, Nothing, True, True)
        End Using
    End Sub

    Private Function SetDataSources() As Boolean

        ' If Not PrepareCache(Me, GetType(HelperLists.AccountInfoList)) Then Return False

        Try



        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function

    Private Sub ConfigureButtons()

        DateDateTimePicker.Enabled = (Not Obj Is Nothing AndAlso Not Obj.DateIsReadOnly)
        ContentTextBox.ReadOnly = (Obj Is Nothing OrElse Obj.ContentIsReadOnly)
        ValueChangeTotalAccTextBox.ReadOnly = (Obj Is Nothing OrElse Obj.ValueChangeTotalIsReadOnly)

        RefreshJournalEntryInfoListButton.Enabled = (Not Obj Is Nothing AndAlso Not Obj.AssociatedJournalEntryIsReadOnly)
        CreateNewJournalEntryButton.Enabled = (Not Obj Is Nothing AndAlso Not Obj.AssociatedJournalEntryIsReadOnly)
        AttachNewJournalEntryButton.Enabled = (Not Obj Is Nothing AndAlso Not Obj.AssociatedJournalEntryIsReadOnly)

        nOkButton.Enabled = (Not Obj Is Nothing)
        ApplyButton.Enabled = (Not Obj Is Nothing)
        nCancelButton.Enabled = (Not Obj Is Nothing AndAlso Not Obj.IsNew)

        LimitationsButton.Visible = (Not Obj Is Nothing AndAlso _
            Not StringIsNullOrEmpty(Obj.ChronologyValidator.LimitsExplanation))

    End Sub

End Class