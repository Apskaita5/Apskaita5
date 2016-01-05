﻿Imports ApskaitaObjects.Assets
Public Class F_ComplexOperationDiscard
    Implements ISupportsPrinting, IObjectEditForm

    Private Obj As ComplexOperationDiscard
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
            Return GetType(ComplexOperationDiscard)
        End Get
    End Property


    Public Sub New(ByVal operationID As Integer, ByVal fetchByJournalEntry As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _OperationID = operationID
        _FetchByJournalEntry = fetchByJournalEntry

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub F_ComplexOperationDiscard_Activated(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.Activated

        If Me.WindowState = FormWindowState.Maximized AndAlso MyCustomSettings.AutoSizeForm Then _
            Me.WindowState = FormWindowState.Normal

        If Loading Then
            Loading = False
            Exit Sub
        End If

        If Not PrepareCache(Me, GetType(HelperLists.AccountInfoList)) Then Exit Sub

    End Sub

    Private Sub F_ComplexOperationDiscard_FormClosing(ByVal sender As Object, _
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

        GetDataGridViewLayOut(ItemsDataGridView)
        GetFormLayout(Me)

    End Sub

    Private Sub F_ComplexOperationDiscard_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        Try
            If _OperationID > 0 Then
                Obj = LoadObject(Of ComplexOperationDiscard)(Nothing, _
                    "GetComplexOperationDiscard", False, _OperationID, _
                    _FetchByJournalEntry)
            Else
                Obj = ComplexOperationDiscard.NewComplexOperationDiscard()
            End If
        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

        ComplexOperationDiscardBindingSource.DataSource = Obj

        AddDGVColumnSelector(ItemsDataGridView)

        ConfigureButtons()

        SetDataGridViewLayOut(ItemsDataGridView)
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

    Private Sub AddItemsButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AddItemsButton.Click

        If Obj Is Nothing Then Exit Sub

        Dim ids As Integer() = Nothing

        Using frm As New F_AssetSelectionList
            frm.ShowDialog()
            ids = frm.Result
        End Using

        If ids Is Nothing OrElse ids.Length < 1 Then Exit Sub

        Try

            Dim newList As OperationDiscardList = LoadObject(Of OperationDiscardList) _
                (Nothing, "NewOperationDiscardList", True, ids, Obj.ChronologyValidator.BaseValidator)
            Obj.AddRange(newList)

        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Private Sub ApplyCommonCostsAccountButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ApplyCommonCostsAccountButton.Click

        If Obj Is Nothing OrElse Obj.Items.Count < 1 Then Exit Sub

        Dim selectedAccount As Long = 0
        Try
            selectedAccount = DirectCast(CostsAccountAccGridComboBox.SelectedValue, Long)
        Catch ex As Exception
        End Try

        If Not selectedAccount > 0 Then Exit Sub

        Try
            Obj.SetCommonAccountCosts(selectedAccount)
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub


    Private Sub ItemsDataGridView_CellBeginEdit(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) _
        Handles ItemsDataGridView.CellBeginEdit

        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 OrElse Obj Is Nothing Then Exit Sub

        If Not Obj.ChronologyValidator.FinancialDataCanChange Then
            e.Cancel = True
            Exit Sub
        End If

        Dim current As OperationDiscard = Nothing
        Try
            current = DirectCast(ItemsDataGridView.Rows(e.RowIndex).DataBoundItem, OperationDiscard)
        Catch ex As Exception
        End Try
        If current Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If

        If (ItemsDataGridView.Columns(e.ColumnIndex) Is AccountCostsDataGridViewColumn _
            AndAlso current.AccountCostsIsReadOnly) OrElse _
            (ItemsDataGridView.Columns(e.ColumnIndex) Is AmountToDiscardDataGridViewColumn _
            AndAlso current.AmountToDiscardIsReadOnly) Then

            e.Cancel = True

        End If

    End Sub

    Private Sub ItemsDataGridView_UserDeletingRow(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) _
        Handles ItemsDataGridView.UserDeletingRow

        If e.Row Is Nothing OrElse Obj Is Nothing Then Exit Sub

        Dim current As OperationDiscard = Nothing
        Try
            current = DirectCast(e.Row.DataBoundItem, OperationDiscard)
        Catch ex As Exception
        End Try
        If current Is Nothing Then Exit Sub

        If Not current.ChronologyValidator.FinancialDataCanChange Then
            MsgBox(String.Format("Klaida. Ilgalaikio turto {0} nurašymo operacijos pašalinti iš dokumento negalima:{1}{2}", _
                current.AssetName, vbCrLf, current.ChronologyValidator.FinancialDataCanChangeExplanation))
            e.Cancel = True
        ElseIf Not current.IsNew AndAlso Not Obj.ChronologyValidator.FinancialDataCanChange Then
            MsgBox(String.Format("Klaida. Ilgalaikio turto nurašymo operacijų pašalinti iš dokumento negalima:{0}{1}", _
                vbCrLf, Obj.ChronologyValidator.FinancialDataCanChangeExplanation))
            e.Cancel = True
        End If

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

        Using bm As New BindingsManager(ComplexOperationDiscardBindingSource, _
            ItemsSortedBindingSource, Nothing, True, False)

            Try
                Obj = LoadObject(Of ComplexOperationDiscard)(Obj, "Save", False)
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
        Using bm As New BindingsManager(ComplexOperationDiscardBindingSource, _
            ItemsSortedBindingSource, Nothing, True, True)
        End Using
    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, GetType(HelperLists.AccountInfoList)) Then Return False

        Try

            LoadAccountInfoListToGridCombo(CostsAccountAccGridComboBox, True, 6)
            LoadAccountInfoListToGridCombo(AccountCostsDataGridViewColumn, True, 6)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function

    Private Sub ConfigureButtons()

        DateDateTimePicker.Enabled = Not Obj Is Nothing
        DocumentNumberTextBox.ReadOnly = Obj Is Nothing
        ContentTextBox.ReadOnly = Obj Is Nothing

        ApplyCommonCostsAccountButton.Enabled = (Not Obj Is Nothing AndAlso _
            Obj.ChronologyValidator.FinancialDataCanChange AndAlso _
            Obj.ChronologyValidator.ChildrenFinancialDataCanChange)
        CostsAccountAccGridComboBox.Enabled = (Not Obj Is Nothing AndAlso _
            Obj.ChronologyValidator.FinancialDataCanChange AndAlso _
            Obj.ChronologyValidator.ChildrenFinancialDataCanChange)

        AccountCostsDataGridViewColumn.ReadOnly = (Obj Is Nothing OrElse _
            Not Obj.ChronologyValidator.FinancialDataCanChange)
        AmountToDiscardDataGridViewColumn.ReadOnly = (Obj Is Nothing OrElse _
            Not Obj.ChronologyValidator.FinancialDataCanChange)

        nOkButton.Enabled = (Not Obj Is Nothing)
        ApplyButton.Enabled = (Not Obj Is Nothing)
        nCancelButton.Enabled = (Not Obj Is Nothing AndAlso Not Obj.IsNew)

        LimitationsButton.Visible = (Not Obj Is Nothing AndAlso _
            Not StringIsNullOrEmpty(Obj.ChronologyValidator.LimitsExplanation))

    End Sub

End Class