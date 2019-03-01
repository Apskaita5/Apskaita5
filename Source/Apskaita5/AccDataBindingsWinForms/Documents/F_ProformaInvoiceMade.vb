Imports ApskaitaObjects.Documents
Imports ApskaitaObjects.Settings
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.Printing
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.Attributes
Imports ApskaitaObjects.HelperLists

Public Class F_ProformaInvoiceMade

    Implements ISupportsPrinting, IObjectEditForm

    Private ReadOnly _RequiredCachedLists As Type() = New Type() {GetType(DocumentSerialInfoList), _
        GetType(PersonInfoList), GetType(CompanyRegionalInfoList), GetType(TaxRateInfoList), _
        GetType(AccDataAccessLayer.Security.UserProfile), GetType(RegionalInfoDictionary)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of ProformaInvoiceMade)
    Private _ListViewManager As DataListViewEditControlManager(Of ProformaInvoiceMadeItem)
    Private _QueryManager As CslaActionExtenderQueryObject

    Private _DocumentToEdit As ProformaInvoiceMade = Nothing

    Private _PrintDropDown As Windows.Forms.ToolStripDropDown = Nothing
    Private _PrintPreviewDropDown As Windows.Forms.ToolStripDropDown = Nothing
    Private _EmailDropDown As Windows.Forms.ToolStripDropDown = Nothing


    Public ReadOnly Property ObjectID() As Integer _
        Implements IObjectEditForm.ObjectID
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

    Public ReadOnly Property ObjectType() As System.Type _
        Implements IObjectEditForm.ObjectType
        Get
            Return GetType(ProformaInvoiceMade)
        End Get
    End Property


    Public Sub New(ByVal documentToEdit As ProformaInvoiceMade)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _DocumentToEdit = documentToEdit

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()


    End Sub


    Private Sub F_ProformaInvoiceMade_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        If _DocumentToEdit Is Nothing Then
            _DocumentToEdit = ProformaInvoiceMade.NewProformaInvoiceMade()
        End If

        Try

            _FormManager = New CslaActionExtenderEditForm(Of ProformaInvoiceMade) _
                (Me, ProformaInvoiceMadeBindingSource, _DocumentToEdit, _
                _RequiredCachedLists, IOkButton, IApplyButton, ICancelButton, _
                Nothing, ProgressFiller1)

            _FormManager.AddNewDataSourceButton(NewButton, "NewInvoiceMade")

            _FormManager.ManageDataListViewStates(ItemsDataListView)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

        ConfigureButtons()

        Try
            If _FormManager.DataSource.IsNew AndAlso StringIsNullOrEmpty(_FormManager.DataSource.Serial) Then
                _FormManager.DataSource.Serial = HelperLists.DocumentSerialInfoList. _
                    GetCachedFilteredList(False, DocumentSerialType.ProformaInvoice)(0).Serial
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of ProformaInvoiceMadeItem) _
                (ItemsDataListView, Nothing, AddressOf OnItemsDelete, _
                 AddressOf OnItemAdd, Nothing, _DocumentToEdit)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            SetupDefaultControls(Of ProformaInvoiceMade)(Me, ProformaInvoiceMadeBindingSource, _DocumentToEdit)

            PrepareControl(Me.ServicesToAddAccListComboBox, New ServiceFieldAttribute( _
                ValueRequiredLevel.Optional, TradedItemType.Sales))
            PrepareControl(Me.GoodsToAddAccListComboBox, New GoodsFieldAttribute( _
                ValueRequiredLevel.Optional, TradedItemType.Sales))

            If MyCustomSettings.DefaultInvoiceMadeItemIsGoods Then
                Me.AddGoodsRadioButton.Checked = True
            Else
                Me.AddServicesRadioButton.Checked = True
            End If

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Sub OnItemsDelete(ByVal items As ProformaInvoiceMadeItem())
        If items Is Nothing OrElse items.Length < 1 OrElse _FormManager.DataSource Is Nothing Then Exit Sub
        For Each item As ProformaInvoiceMadeItem In items
            _FormManager.DataSource.InvoiceItems.Remove(item)
        Next
    End Sub

    Private Sub OnItemAdd()
        _FormManager.DataSource.InvoiceItemsSorted.AddNew()
    End Sub

    Private Sub AddTradeItemButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AddTradeItemButton.Click

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Dim itemToAdd As ITradedItem = Nothing
        If AddGoodsRadioButton.Checked Then
            Try
                itemToAdd = DirectCast(GoodsToAddAccListComboBox.SelectedValue, ITradedItem)
            Catch ex As Exception
            End Try
        Else
            Try
                itemToAdd = DirectCast(ServicesToAddAccListComboBox.SelectedValue, ITradedItem)
            Catch ex As Exception
            End Try
        End If
        If itemToAdd Is Nothing Then Exit Sub

        Dim regionalDictionary As RegionalInfoDictionary
        Try
            regionalDictionary = RegionalInfoDictionary.GetList()
        Catch ex As Exception
            ShowError(ex, Nothing)
            Exit Sub
        End Try

        Try
            Using busy As New StatusBusy
                _FormManager.DataSource.AddNewTradedItem(itemToAdd, regionalDictionary)
            End Using
        Catch ex As Exception
            ShowError(ex, New Object() {_FormManager.DataSource, itemToAdd, regionalDictionary})
            Exit Sub
        End Try

    End Sub

    Private Sub RefreshNumberButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshNumberButton.Click

        If _FormManager.DataSource Is Nothing Then Exit Sub

        If Not _FormManager.DataSource.IsNew Then
            If Not YesOrNo("DĖMESIO. Jūs redaguojate jau įtrauktą į duomenų bazę dokumentą. " _
                & "Ar tikrai norite suteikti jam naują numerį?") Then Exit Sub
        End If

        'CommandLastDocumentNumber.TheCommand(DocumentSerialType.ProformaInvoice, _
        '    _FormManager.DataSource.Serial.Trim, _FormManager.DataSource.Date, _
        '    _FormManager.DataSource.AddDateToNumberOptionWasUsed)
        _QueryManager.InvokeQuery(Of CommandLastDocumentNumber)(Nothing, "TheCommand", True, _
            AddressOf OnInvoiceNumberFetched, DocumentSerialType.ProformaInvoice, _
            _FormManager.DataSource.Serial.Trim, _FormManager.DataSource.Date, _
            _FormManager.DataSource.AddDateToNumberOptionWasUsed)

    End Sub

    Private Sub OnInvoiceNumberFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        _FormManager.DataSource.Number = DirectCast(result, Integer) + 1

    End Sub

    Private Sub CopyInvoiceButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles CopyInvoiceButton.Click

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Dim info As InvoiceInfo.InvoiceInfo = Nothing
        Try
            Using busy As New StatusBusy
                info = _FormManager.DataSource.GetInvoiceInfo(InstanceGuid.ToString)
            End Using
        Catch ex As Exception
            ShowError(New Exception(String.Format("Klaida. Nepavyko generuoti InvoiceInfo objekto:{0}{1}",
                vbCrLf, ex.Message), ex), _FormManager.DataSource)
            Exit Sub
        End Try

        If info Is Nothing Then
            MsgBox("Klaida. Dėl nežinomų priežasčių nepavyko generuoti InvoiceInfo objekto.", _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Try
            Using busy As New StatusBusy
                System.Windows.Forms.Clipboard.SetText(InvoiceInfo.Factory. _
                    ToXmlString(Of InvoiceInfo.InvoiceInfo)(info), TextDataFormat.UnicodeText)
            End Using
        Catch ex As Exception
            ShowError(New Exception(String.Format("Klaida. Nepavyko serializuoti InvoiceInfo objekto:{0}{1}",
                vbCrLf, ex.Message), ex), Nothing)
            Exit Sub
        End Try

        MsgBox("Sąskaita sėkmingai nukopijuota į ClipBoard'ą.", MsgBoxStyle.Information, "Info")

    End Sub

    Private Sub PasteInvoiceButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles PasteInvoiceButton.Click

        Dim clipboardText As String = System.Windows.Forms.Clipboard.GetText(TextDataFormat.UnicodeText)

        If StringIsNullOrEmpty(clipboardText) Then

            MsgBox("Klaida. ClipBoard'as tuščias, t.y. nebuvo nukopijuota jokia sąskaita.", _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub

        End If

        Dim info As InvoiceInfo.InvoiceInfo = Nothing
        Try
            Using busy As New StatusBusy
                info = InvoiceInfo.Factory.FromXmlString(Of InvoiceInfo.InvoiceInfo)(clipboardText)
            End Using
        Catch ex As Exception
            ShowError(New Exception(String.Format("Klaida. Nepavyko atkurti sąskaitos objekto. " _
                & "Teigtina, kad prieš tai į ClipBoard'ą buvo nukopijuota ne sąskaita, " _
                & "o šiaip kažkoks tekstas.{0}Klaidos tekstas:{1}", vbCrLf, ex.Message), ex), clipboardText)
            Exit Sub
        End Try

        If info Is Nothing Then
            MsgBox("Klaida. Dėl nežinomų priežasčių nepavyko atkurti sąskaitos objekto.", _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Dim useExternalID As Boolean = False

        If InstanceGuid.ToString.Trim <> info.SystemGuid.Trim Then

            Dim answer As String

            If info.ExternalID Is Nothing OrElse String.IsNullOrEmpty(info.ExternalID.Trim) Then
                If Not MyCustomSettings.AlwaysUseExternalIdInvoicesMade Then
                    answer = Ask("Sąskaita yra kopijuojama iš išorinės sistemos. " _
                        & "Ką priskirti išoriniam ID?", New ButtonStructure("Nieko", _
                        "Nepriskirti jokio išorinio ID."), New ButtonStructure( _
                        "ID", "Sąskaitos ID laikyti išoriniu ID."), New ButtonStructure( _
                        "Atšaukti", "Atšaukti kopijavimą."))
                Else
                    answer = "ID"
                End If
            Else
                answer = Ask("Sąskaita yra kopijuojama iš išorinės sistemos. " _
                    & "Ką priskirti išoriniam ID?", New ButtonStructure("Nieko", _
                    "Nepriskirti jokio išorinio ID."), New ButtonStructure( _
                    "ID", "Sąskaitos ID laikyti išoriniu ID."), New ButtonStructure( _
                    "Išorinį ID", "Sąskaitos išorinį ID laikyti išoriniu ID."), _
                    New ButtonStructure("Atšaukti", "Atšaukti kopijavimą."))
            End If

            If answer = "Nieko" Then
                info.SystemGuid = InstanceGuid.ToString.Trim
            ElseIf answer = "Atšaukti" Then
                Exit Sub
            ElseIf answer = "Išorinį ID" Then
                useExternalID = True
            End If

        End If

        Dim newObj As ProformaInvoiceMade = Nothing
        Dim newPerson As InvoiceInfo.ClientInfo = Nothing
        Try
            Using busy As New StatusBusy
                Dim personList As HelperLists.PersonInfoList = HelperLists.PersonInfoList.GetList()
                newObj = ProformaInvoiceMade.NewProformaInvoiceMade(info, InstanceGuid.ToString, _
                    useExternalID, personList, newPerson)
            End Using
        Catch ex As Exception
            ShowError(New Exception(String.Format("Klaida. Nepavyko įkrauti kopijuojamos sąskaitos duomenų:{0}{1}",
                vbCrLf, ex.Message), ex), info)
            Exit Sub
        End Try

        If newObj Is Nothing Then
            MsgBox("Klaida. Dėl nežinomų priežasčių nepavyko įkrauti kopijuojamos sąskaitos duomenų.", _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        If Not StringIsNullOrEmpty(newObj.ExternalID) Then

            Dim existingInvoiceID As Integer = 0

            Try
                Using busy As New StatusBusy
                    existingInvoiceID = CommandDocumentIdByExternalId.TheCommand(Of ProformaInvoiceMade)( _
                        newObj.ExternalID)
                End Using
            Catch ex As Exception
                ShowError(ex, Nothing)
                Exit Sub
            End Try

            If existingInvoiceID > 0 Then
                If YesOrNo(String.Format("DĖMESIO. Sąskaita su tokiu išoriniu ID jau egzistuoja. " _
                    & "Įvesti sąskaitą iš naujo nebus leidžiama.{0}{1}Atidaryti egzistuojančią sąskaitą?", _
                    vbCrLf, vbCrLf)) Then
                    'ProformaInvoiceMade.GetProformaInvoiceMade(existingInvoiceID)
                    _QueryManager.InvokeQuery(Of ProformaInvoiceMade)(Nothing, "GetProformaInvoiceMade", True, _
                        AddressOf OpenObjectEditForm, existingInvoiceID)
                End If
                Exit Sub
            End If

        End If

        _FormManager.AddNewDataSource(newObj)

        If Not newPerson Is Nothing Then
            OpenObjectEditForm(newPerson)
        End If

    End Sub

    Private Sub AddServicesRadioButton_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AddServicesRadioButton.CheckedChanged, _
        AddGoodsRadioButton.CheckedChanged
        GoodsToAddAccListComboBox.Enabled = AddGoodsRadioButton.Checked
        ServicesToAddAccListComboBox.Enabled = AddServicesRadioButton.Checked
        If Not GoodsToAddAccListComboBox.Enabled Then GoodsToAddAccListComboBox.SelectedValue = Nothing
        If Not ServicesToAddAccListComboBox.Enabled Then ServicesToAddAccListComboBox.SelectedValue = Nothing
    End Sub


    Public Function GetMailDropDownItems() As System.Windows.Forms.ToolStripDropDown _
       Implements ISupportsPrinting.GetMailDropDownItems

        If _EmailDropDown Is Nothing Then
            _EmailDropDown = New ToolStripDropDown
            _EmailDropDown.Items.Add("Lietuvių klb.", Nothing, AddressOf OnMailClick)
        End If

        Return _EmailDropDown

    End Function

    Public Function GetPrintDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintDropDownItems

        If _PrintDropDown Is Nothing Then
            _PrintDropDown = New ToolStripDropDown
            _PrintDropDown.Items.Add("Lietuvių klb.", Nothing, AddressOf OnPrintClick)

        End If

        Return _PrintDropDown

    End Function

    Public Function GetPrintPreviewDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintPreviewDropDownItems

        If _PrintPreviewDropDown Is Nothing Then
            _PrintPreviewDropDown = New ToolStripDropDown
            _PrintPreviewDropDown.Items.Add("Lietuvių klb.", Nothing, AddressOf OnPrintPreviewClick)

        End If

        Return _PrintPreviewDropDown

    End Function

    Public Sub OnMailClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnMailClick
        If _FormManager.DataSource Is Nothing Then Exit Sub

        Using frm As New F_SendObjToEmail(_FormManager.DataSource, Convert.ToInt32(IIf(GetSenderText(sender).ToLower.Contains("lietuvių"), 1, 0)))
            frm.ShowDialog()
        End Using

    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, False, Convert.ToInt32(IIf(GetSenderText(sender).ToLower.Contains("lietuvių"), 1, 0)), _
                _FormManager.DataSource.GetFileName(), Me, "")
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Dim r As Dictionary(Of Type, List(Of String)) = GetContainedValueObjectLists(_FormManager.DataSource)
        Try
            PrintObject(_FormManager.DataSource, True, Convert.ToInt32(IIf(GetSenderText(sender).ToLower.Contains("lietuvių"), 1, 0)), _
                _FormManager.DataSource.GetFileName(), Me, "")
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
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

        If _FormManager.DataSource Is Nothing Then Exit Sub

        ICancelButton.Enabled = Not _FormManager.DataSource.IsNew

    End Sub

End Class