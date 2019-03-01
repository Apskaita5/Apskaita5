Imports ApskaitaObjects.ActiveReports
Imports ApskaitaObjects.Documents
Imports AccControlsWinForms

Public Class F_VatDeclarationSchemaInfoItemList

    Private _FormManager As CslaActionExtenderReportForm(Of VatDeclarationSchemaInfoItemList)
    Private _ListViewManager As DataListViewEditControlManager(Of VatDeclarationSchemaInfoItem)
    Private _QueryManager As CslaActionExtenderQueryObject


    Private Sub F_VatDeclarationSchemaInfoItemList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load
        If Not SetDataSources() Then Exit Sub
    End Sub

    Private Function SetDataSources() As Boolean

        Try

            _ListViewManager = New DataListViewEditControlManager(Of VatDeclarationSchemaInfoItem) _
                (VatDeclarationSchemaInfoItemListDataListView, _
                 ContextMenuStrip1, Nothing, Nothing, Nothing, Nothing)

            _ListViewManager.AddCancelButton = True
            _ListViewManager.AddButtonHandler("Keisti", "Keisti deklaravimo schemos duomenis.", _
                AddressOf ChangeItem)
            _ListViewManager.AddButtonHandler("Ištrinti", "Pašalinti deklaravimo schemos duomenis iš duomenų bazės.", _
                AddressOf DeleteItem)

            _ListViewManager.AddMenuItemHandler(ChangeItem_MenuItem, AddressOf ChangeItem)
            _ListViewManager.AddMenuItemHandler(DeleteItem_MenuItem, AddressOf DeleteItem)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            ' VatDeclarationSchemaInfoItemList.GetVatDeclarationSchemaInfoItemList()
            _FormManager = New CslaActionExtenderReportForm(Of VatDeclarationSchemaInfoItemList) _
                (Me, VatDeclarationSchemaInfoItemListBindingSource, Nothing, Nothing, RefreshButton, _
                 ProgressFiller1, "GetVatDeclarationSchemaInfoItemList", AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(VatDeclarationSchemaInfoItemListDataListView)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Function GetReportParams() As Object()
        Return New Object() {}
    End Function

    Private Sub ChangeItem(ByVal item As VatDeclarationSchemaInfoItem)
        If item Is Nothing Then Exit Sub
        ' VatDeclarationSchema.GetVatDeclarationSchema(item.ID)
        _QueryManager.InvokeQuery(Of VatDeclarationSchema)(Nothing, "GetVatDeclarationSchema", True, _
            AddressOf OpenObjectEditForm, item.ID)
    End Sub

    Private Sub DeleteItem(ByVal item As VatDeclarationSchemaInfoItem)

        If item Is Nothing Then Exit Sub

        If CheckIfObjectEditFormOpen(Of VatDeclarationSchema)(item.ID, True, True) Then Exit Sub

        If Not YesOrNo("Ar tikrai norite pašalinti schemos duomenis iš duomenų bazės?") Then Exit Sub

        ' VatDeclarationSchema.DeleteVatDeclarationSchema(item.ID)
        _QueryManager.InvokeQuery(Of VatDeclarationSchema)(Nothing, "DeleteVatDeclarationSchema", False, _
            AddressOf OnItemDeleted, item.ID)

    End Sub

    Private Sub OnItemDeleted(ByVal result As Object, ByVal exceptionHandled As Boolean)
        If exceptionHandled Then Exit Sub
        If Not YesOrNo("Schemos duomenys sėkmingai pašalinti iš įmonės duomenų bazės. Atnaujinti sąrašą?") Then Exit Sub
        RefreshButton.PerformClick()
    End Sub

    Private Sub NewObjectButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles NewObjectButton.Click
        OpenNewForm(Of VatDeclarationSchema)()
    End Sub

    Private Sub OpenFileButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles OpenFileButton.Click

        Dim filePath As String

        Using openFile As New OpenFileDialog
            openFile.InitialDirectory = AppPath()
            openFile.Filter = "PVM deklaravimo schemų duomenys (*.xml)|*.xml|Visi failai|*.*"
            openFile.Multiselect = False
            If openFile.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
            filePath = openFile.FileName
        End Using

        If StringIsNullOrEmpty(filePath) Then Exit Sub

        If Not IO.File.Exists(filePath) Then
            MsgBox(String.Format("Klaida. Failas '{0}' neegzistuoja.", filePath), _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Dim xmlString As String = Nothing
        Try
            Using busy As New StatusBusy
                xmlString = IO.File.ReadAllText(filePath, System.Text.Encoding.Unicode)
            End Using
        Catch ex As Exception
            ShowError(ex, Nothing)
            Exit Sub
        End Try

        If StringIsNullOrEmpty(xmlString) Then
            MsgBox("Klaida. Pasirinktas failas yra tuščias", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        If Not YesOrNo("Importuoti PVM deklaravimo schemų duomenis iš failo?") Then Exit Sub

        Try
            ' CommandImportVatDeclarationSchemas.TheCommand(xmlString)
            _QueryManager.InvokeQuery(Of CommandImportVatDeclarationSchemas)(Nothing, _
                "TheCommand", True, AddressOf OnSchemasImportComplete, xmlString)
        Catch ex As Exception
            ShowError(ex, Nothing)
        End Try

    End Sub

    Private Sub OnSchemasImportComplete(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        If StringIsNullOrEmpty(result.ToString) Then Exit Sub

        MsgBox(result.ToString, MsgBoxStyle.Information, "Info")

    End Sub

    Private Sub SaveFileButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles SaveFileButton.Click

        If _FormManager.DataSource Is Nothing OrElse VatDeclarationSchemaInfoItemListDataListView.CheckedObjects Is Nothing Then Exit Sub

        Dim ids As New List(Of Integer)

        For Each item As VatDeclarationSchemaInfoItem In VatDeclarationSchemaInfoItemListDataListView.CheckedObjects
            ids.Add(item.ID)
        Next

        If ids.Count < 1 Then
            MsgBox("Klaida. Nepasirinkta nė viena schema.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Try

            _QueryManager.InvokeQuery(Of BusinessObjectCollection(Of VatDeclarationSchema))(Nothing, _
                "GetBusinessObjectCollection", True, AddressOf OnSchemasFetched, ids.ToArray())

        Catch ex As Exception
            ShowError(ex, Nothing)
            Exit Sub
        End Try

    End Sub

    Private Sub OnSchemasFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        Dim list As BusinessObjectCollection(Of VatDeclarationSchema) = _
            DirectCast(result, BusinessObjectCollection(Of VatDeclarationSchema))

        Dim filePath As String

        Using saveFile As New SaveFileDialog
            saveFile.Filter = "PVM deklaravimo schemų duomenys (*.xml)|*.xml|Visi failai|*.*"
            saveFile.AddExtension = True
            saveFile.DefaultExt = ".xml"
            If saveFile.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
            filePath = saveFile.FileName
        End Using

        If StringIsNullOrEmpty(filePath) Then Exit Sub

        Try
            Using busy As New StatusBusy
                IO.File.WriteAllText(filePath, VatDeclarationSchema.GetXmlString(list), System.Text.Encoding.Unicode)
            End Using
        Catch ex As Exception
            ShowError(ex, Nothing)
            Exit Sub
        End Try

        MsgBox("Pasirinktų schemų duomenys sėkmingai išsaugoti faile.", MsgBoxStyle.Information, "Info")

    End Sub

End Class