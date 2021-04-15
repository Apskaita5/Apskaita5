Imports AccControlsWinForms
Imports ApskaitaObjects.General
Imports BrightIdeasSoftware

Public Class F_ConsolidatedReport
    Implements IObjectEditForm

    Private _DataSource As ConsolidatedReport = Nothing
    Private _ListViewManager As DataListViewEditControlManager(Of ConsolidatedReportItem)


    Public ReadOnly Property ObjectID() As Integer Implements IObjectEditForm.ObjectID
        Get
            If _DataSource Is Nothing OrElse _DataSource.IsNew Then
                Return Integer.MinValue
            Else
                Return 1
            End If
        End Get
    End Property

    Public ReadOnly Property ObjectType() As System.Type Implements IObjectEditForm.ObjectType
        Get
            Return GetType(ConsolidatedReport)
        End Get
    End Property


    Private Sub F_ConsolidatedReport_Activated(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Activated
        If Me.WindowState = FormWindowState.Maximized AndAlso MyCustomSettings.AutoSizeForm Then _
            Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub F_ConsolidatedReportsStructure_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing



        MyCustomSettings.GetFormLayout(Me)
        MyCustomSettings.GetListViewLayOut(Me.ConsolidatedReportTreeListView)

    End Sub

    Private Sub F_ConsolidatedReport_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            _ListViewManager = New DataListViewEditControlManager(Of ConsolidatedReportItem) _
                (Me.ConsolidatedReportTreeListView, Nothing, AddressOf DeleteItem, _
                AddressOf AddItem, Nothing, Nothing)
        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

        SaveInDatabaseButton.Enabled = ConsolidatedReport.CanEditObject
        FetchFromDatabaseButton.Enabled = ConsolidatedReport.CanGetObject

        MyCustomSettings.SetFormLayout(Me)
        MyCustomSettings.SetListViewLayOut(Me.ConsolidatedReportTreeListView)

        DirectCast(Me.ConsolidatedReportTreeListView.DropSink, SimpleDropSink).AcceptExternal = False
        DirectCast(Me.ConsolidatedReportTreeListView.DropSink, SimpleDropSink).CanDropOnBackground = True
        DirectCast(Me.ConsolidatedReportTreeListView.DropSink, SimpleDropSink).CanDropOnItem = True
        DirectCast(Me.ConsolidatedReportTreeListView.DropSink, SimpleDropSink).CanDropOnSubItem = True
        DirectCast(Me.ConsolidatedReportTreeListView.DropSink, SimpleDropSink).EnableFeedback = True
        DirectCast(Me.ConsolidatedReportTreeListView.DropSink, SimpleDropSink).CanDropBetween = True

        Me.ConsolidatedReportTreeListView.CanExpandGetter = AddressOf CanExpand
        Me.ConsolidatedReportTreeListView.ChildrenGetter = AddressOf ChildrenGetter
        
    End Sub


    Private Function CanExpand(ByVal item As Object) As Boolean
        If item Is Nothing Then Return False
        If TypeOf item Is ConsolidatedReportItem Then
            Return (DirectCast(item, ConsolidatedReportItem).Children.Count > 0)
        Else
            Return False
        End If
    End Function

    Private Function ChildrenGetter(ByVal item As Object) As ConsolidatedReportItemList
        If item Is Nothing OrElse Not TypeOf item Is ConsolidatedReportItem Then Return Nothing
        Return DirectCast(item, ConsolidatedReportItem).Children
    End Function


    Private Sub DeleteItem(ByVal items As ConsolidatedReportItem())

        If _DataSource Is Nothing Then Exit Sub

        If items Is Nothing OrElse items.Length < 1 Then
            MsgBox("Klaida. Nepasirinkta eilutė, kurią reikėtų ištrinti.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        If items.Length > 1 Then
            MsgBox("Klaida. Vienu kartu leidžiama ištrinti tik vieną eilutę, pasirinkite tik vieną eilutę.", _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        DeleteItem(items(0))

        Me.ConsolidatedReportTreeListView.RefreshObjects(_DataSource.Children.Children)

    End Sub

    Private Sub DeleteItem(ByVal item As ConsolidatedReportItem)

        Dim message As String = ""
        If Not _DataSource.CanRemoveItem(item, message) Then
            MsgBox(message, MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        If _DataSource.WarnBeforeRemoveItem(item, message) Then

            If Not YesOrNo(message & vbCrLf & "Ar tikrai norite pašalinti pasirinktą ataskaitos elementą?") Then Exit Sub

        Else

            If Not YesOrNo("Ar tikrai norite pašalinti pasirinktą ataskaitos elementą?") Then Exit Sub

        End If

        Try
            _DataSource.RemoveItem(item)
        Catch ex As Exception
            ShowError(ex, New Object() {_DataSource, item})
            Exit Sub
        End Try

    End Sub

    Private Sub AddItem()

        If _DataSource Is Nothing Then Exit Sub

        If Me.ConsolidatedReportTreeListView.SelectedObject Is Nothing Then
            MsgBox("Klaida. Nepasirinkta eilutė, kuriai reikėtų pridėti dukterinę eilutę.", _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Dim current As ConsolidatedReportItem = Nothing
        Try
            current = DirectCast(Me.ConsolidatedReportTreeListView.SelectedObject, ConsolidatedReportItem)
        Catch ex As Exception
        End Try
        If current Is Nothing Then Exit Sub

        Dim message As String = ""
        If Not current.CanAddChild(message) Then
            MsgBox(message, MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        If current.WarnBeforeAddingItem(message) Then
            If Not YesOrNo(message & vbCrLf & "Ar tikrai norite tęsti?") Then Exit Sub
        End If

        Dim newChild As ConsolidatedReportItem
        Try
            newChild = current.AddChild()
        Catch ex As Exception
            ShowError(ex, New Object() {_DataSource, current})
            Exit Sub
        End Try

        If newChild Is Nothing Then
            MsgBox("Klaida. Dėl neaiškių priežasčių nepavyko pridėti naujos eilutės.", _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Me.ConsolidatedReportTreeListView.RefreshObjects(_DataSource.Children.Children)

        Me.ConsolidatedReportTreeListView.Expand(current)
        Me.ConsolidatedReportTreeListView.Reveal(newChild, True)
        Me.ConsolidatedReportTreeListView.EnsureModelVisible(newChild)

    End Sub


    Private Sub FetchFromDatabaseButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles FetchFromDatabaseButton.Click

        Try
            Using busy As New StatusBusy
                _DataSource = ConsolidatedReport.GetConsolidatedReport()
            End Using
        Catch ex As Exception
            ShowError(ex, Nothing)
            Exit Sub
        End Try

        Me.ConsolidatedReportTreeListView.SetObjects(_DataSource.Children.Children)

        Me.ConsolidatedReportTreeListView.ExpandAll()
        Me.ConsolidatedReportTreeListView.Select()

    End Sub

    Private Sub GetNewFormButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles GetNewFormButton.Click

        _DataSource = ConsolidatedReport.NewConsolidatedReport

        Me.ConsolidatedReportTreeListView.SetObjects(_DataSource.Children.Children)

        Me.ConsolidatedReportTreeListView.ExpandAll()
        Me.ConsolidatedReportTreeListView.Select()

    End Sub

    Private Sub OpenFileButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles OpenFileButton.Click

        Dim fileName As String = ""

        Using openFile As New OpenFileDialog
            openFile.InitialDirectory = AppPath()
            openFile.Filter = "Ataskaitos forma|*.str|Visi failai|*.*"
            openFile.Multiselect = False
            If openFile.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
            fileName = openFile.FileName
        End Using

        If StringIsNullOrEmpty(fileName) Then Exit Sub

        If Not IO.File.Exists(fileName) Then
            MsgBox(String.Format("Klaida. Failas {0} neegzistuoja.", fileName), MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Try
            Using busy As New StatusBusy
                _DataSource = ConsolidatedReport.NewConsolidatedReport(fileName, Nothing)
            End Using
        Catch ex As Exception
            ShowError(ex, Nothing)
            Exit Sub
        End Try

        Me.ConsolidatedReportTreeListView.SetObjects(_DataSource.Children.Children)

        Me.ConsolidatedReportTreeListView.ExpandAll()
        Me.ConsolidatedReportTreeListView.Select()

    End Sub

    Private Sub SaveAsFileButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles SaveAsFileButton.Click

        If _DataSource Is Nothing Then Exit Sub

        _DataSource.CheckRules()
        If Not _DataSource.IsValid Then
            If Not YesOrNo(String.Format("Ataskaitos formoje yra klaidų:{0}{1}{2}{3}Ar tikrai norite išsaugoti ataskaitos formą su klaidomis?", _
                vbCrLf, _DataSource.GetAllBrokenRules, vbCrLf, vbCrLf)) Then Exit Sub
        End If

        Dim fileName As String

        Using saveFile As New SaveFileDialog
            saveFile.InitialDirectory = AppPath()
            saveFile.Filter = "Ataskaitos forma|*.str|Visi failai|*.*"
            saveFile.AddExtension = True
            saveFile.DefaultExt = ".str"
            If saveFile.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
            fileName = saveFile.FileName
        End Using

        If StringIsNullOrEmpty(fileName) Then Exit Sub

        Try
            Using busy As New StatusBusy
                _DataSource.SaveToFile(fileName)
            End Using
        Catch ex As Exception
            ShowError(ex, _DataSource)
            Exit Sub
        End Try

        MsgBox("Ataskaitos formos duomenys sėkmingai išsaugoti faile.", _
            MsgBoxStyle.Information, "Info")

        Me.ConsolidatedReportTreeListView.Select()

    End Sub

    Private Sub SaveInDatabaseButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles SaveInDatabaseButton.Click

        If _DataSource Is Nothing OrElse Not _DataSource.IsDirty Then Exit Sub

        If Not _DataSource.IsValid Then
            MsgBox("Formoje yra klaidų: " & _DataSource.GetAllBrokenRules, MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        If _DataSource.IsNew Then
            If Not YesOrNo("DĖMESIO. Jūs ketinate įkrauti visiškai naują finansinių ataskaitų " _
                & "rinkinio struktūrą. Po įkrovimo visos sąskaitos įmonės sąskaitų plane praras " _
                & "susiejimą su finansinių ataskaitų eilutėmis ir šį priskyrimą reikės iš naujo " _
                & "nustatyti sąskaitų plano formoje. Ar tikrai norite tęsti?") Then Exit Sub
        Else
            If Not YesOrNo("Ar tikrai norite išsaugoti ataskaitų formas įmonės duomenų bazėje?") Then Exit Sub
        End If

        Try
            Using busy As New StatusBusy
                _DataSource = _DataSource.Clone.Save
            End Using
        Catch ex As Exception
            ShowError(ex, _DataSource)
            Exit Sub
        End Try

        MsgBox("Ataskaitų formos duomenys sėkmingai išsaugoti duomenų bazėje.", _
            MsgBoxStyle.Information, "Info")

        Me.ConsolidatedReportTreeListView.Select()

    End Sub


    Private Sub ConsolidatedReportTreeListView_ModelCanDrop(ByVal sender As Object, _
        ByVal e As BrightIdeasSoftware.ModelDropEventArgs) Handles ConsolidatedReportTreeListView.ModelCanDrop

        e.Handled = True
        e.Effect = DragDropEffects.None

        If _DataSource Is Nothing OrElse e.SourceModels Is Nothing OrElse e.SourceModels.Count < 1 Then Exit Sub

        If e.SourceModels.Count > 1 Then
            e.InfoMessage = "Galima kilnoti tik po vieną eilutę."
            Exit Sub
        End If

        Dim moveType As MoveItemType = MoveItemType.ToItem
        If e.DropTargetLocation = DropTargetLocation.AboveItem Then
            moveType = MoveItemType.AboveItem
        ElseIf e.DropTargetLocation = DropTargetLocation.BelowItem Then
            moveType = MoveItemType.BelowItem
        ElseIf e.DropTargetLocation = DropTargetLocation.Item OrElse _
            e.DropTargetLocation = DropTargetLocation.SubItem Then
            moveType = MoveItemType.ToItem
        ElseIf e.DropTargetLocation = DropTargetLocation.Background Then
            If DirectCast(e.SourceModels(0), ConsolidatedReportItem).IsRootItem Then
                e.InfoMessage = "Klaida. Antraštinių eilučių pašalinti negalima."
                Exit Sub
            End If
            e.Effect = DragDropEffects.Move
            Exit Sub
        Else
            Exit Sub
        End If

        Dim message As String = ""
        If _DataSource.CanMoveItem(DirectCast(e.SourceModels(0), ConsolidatedReportItem), _
            DirectCast(e.TargetModel, ConsolidatedReportItem), moveType, message) Then
            e.Effect = DragDropEffects.Move
        Else
            e.InfoMessage = message
        End If

    End Sub

    Private Sub ConsolidatedReportTreeListView_ModelDropped(ByVal sender As Object, _
        ByVal e As BrightIdeasSoftware.ModelDropEventArgs) Handles ConsolidatedReportTreeListView.ModelDropped

        If _DataSource Is Nothing OrElse e.SourceModels Is Nothing OrElse e.SourceModels.Count < 1 Then Exit Sub

        If e.SourceModels.Count > 1 Then
            e.InfoMessage = "Galima kilnoti tik po vieną eilutę."
            Exit Sub
        End If

        Dim moveType As MoveItemType = MoveItemType.ToItem
        If e.DropTargetLocation = DropTargetLocation.AboveItem Then
            moveType = MoveItemType.AboveItem
        ElseIf e.DropTargetLocation = DropTargetLocation.BelowItem Then
            moveType = MoveItemType.BelowItem
        ElseIf e.DropTargetLocation = DropTargetLocation.Item OrElse _
            e.DropTargetLocation = DropTargetLocation.SubItem Then
            moveType = MoveItemType.ToItem
        ElseIf e.DropTargetLocation = DropTargetLocation.Background Then

            DeleteItem(DirectCast(e.SourceModels(0), ConsolidatedReportItem))
            e.RefreshObjects()
            Exit Sub

        Else
            Exit Sub
        End If

        Dim message As String = ""
        If _DataSource.CanMoveItem(DirectCast(e.SourceModels(0), ConsolidatedReportItem), _
            DirectCast(e.TargetModel, ConsolidatedReportItem), moveType, message) Then
            e.Effect = DragDropEffects.Move
        Else
            e.InfoMessage = message
            Exit Sub
        End If

        Try
            _DataSource.MoveItem(DirectCast(e.SourceModels(0), ConsolidatedReportItem), _
                DirectCast(e.TargetModel, ConsolidatedReportItem), moveType)
        Catch ex As Exception
            ShowError(ex, New Object() {_DataSource, e.SourceModels, e.TargetModel})
            Exit Sub
        End Try

        e.RefreshObjects()

    End Sub


    Private Sub DoPaste()

        Dim current As ConsolidatedReportItem = Nothing
        Try
            current = DirectCast(Me.ConsolidatedReportTreeListView.SelectedObject, ConsolidatedReportItem)
        Catch ex As Exception
        End Try
        If current Is Nothing Then Exit Sub

        For Each line As String In Clipboard.GetText(TextDataFormat.UnicodeText).Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
            Dim newChild As ConsolidatedReportItem = Nothing
            Try
                newChild = current.AddChild()
                newChild.DisplayedNumber = GetField(line, vbTab, 0)
                newChild.Name = GetField(line, vbTab, 1)
            Catch ex As Exception
                ShowError(ex, New Object() {_DataSource, current, newChild})
                Exit Sub
            End Try
        Next

        Me.ConsolidatedReportTreeListView.RefreshObjects(_DataSource.Children.Children)

    End Sub

End Class
