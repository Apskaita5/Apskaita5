Imports ApskaitaObjects.ActiveReports
Imports ApskaitaObjects.Documents.InvoiceAdapters
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists

Friend Class F_NewInvoiceAdapterForAssetOperation(Of T)

    Private _ForInvoiceReceived As Boolean = False
    Private _ParentChronologyValidator As IChronologicValidator = Nothing
    Private _QueryBrowser As CslaActionExtenderQueryObject
    Private _Value As T = Nothing


    Public ReadOnly Property Value() As T
        Get
            Return _Value
        End Get
    End Property


    Public Sub New(ByVal forInvoiceReceived As Boolean, ByVal parentChronologyValidator As IChronologicValidator)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _ForInvoiceReceived = forInvoiceReceived
        _ParentChronologyValidator = parentChronologyValidator

    End Sub


    Private Sub InvoiceAdapterForAssetOperation_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Try


            _QueryBrowser = New CslaActionExtenderQueryObject(Me, ProgressFiller1)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
        End Try

        If GetType(T) Is GetType(AssetAcquisitionValueIncreaseInvoiceAdapter) Then
            Me.Text = "Nauja ilgalaikio turto savikainos padidinimo operacija"
        ElseIf GetType(T) Is GetType(AssetSaleInvoiceAdapter) Then
            Me.Text = "Nauja ilgalaikio turto pardavimo operacija"
        Else
            MsgBox(String.Format("Klaida. Tipas {0} neimplementuotas formoje {1}.", _
                GetType(T).FullName, Me.GetType.Name), MsgBoxStyle.Exclamation, "Klaida")
            DisableAllControls(Me)
            Exit Sub
        End If

        'LongTermAssetInfoList.GetLongTermAssetInfoList(Today, Today.AddYears(50), Nothing)
        _QueryBrowser.InvokeQuery(Of LongTermAssetInfoList)(Nothing, "", _
            True, AddressOf OnAssetListFetched, Today, Today.AddYears(50), Nothing)

    End Sub

    Private Sub OnAssetListFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then
            DisableAllControls(Me)
            Exit Sub
        End If

        Try

            Dim assetList As LongTermAssetInfoList = DirectCast(result, LongTermAssetInfoList)

            Dim listView As New LongTermAssetInfoListControl

            listView.DataSource = assetList.GetFilteredList(False)
            listView.AcceptSingleClick = True

            Me.LongTermAssetAccGridComboBox.AddDataListView(listView)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

    End Sub


    Private Sub IOkButton_Click(ByVal sender As System.Object, _
       ByVal e As System.EventArgs) Handles IOkButton.Click

        Dim selectedAsset As LongTermAssetInfo = Nothing
        Try
            selectedAsset = DirectCast(LongTermAssetAccGridComboBox.SelectedValue, LongTermAssetInfo)
        Catch ex As Exception
        End Try
        If selectedAsset Is Nothing Then
            MsgBox("Klaida. Nepasirinktas ilgalaikis turtas.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Try

            If GetType(T) Is GetType(AssetAcquisitionValueIncreaseInvoiceAdapter) Then
                'AssetAcquisitionValueIncreaseInvoiceAdapter.NewAssetAcquisitionValueIncreaseInvoiceAdapter( _
                '    selectedAsset.ID, _ParentChronologyValidator, Not _ForInvoiceReceived)
                _QueryBrowser.InvokeQuery(Of AssetAcquisitionValueIncreaseInvoiceAdapter)(Nothing, _
                    "NewAssetAcquisitionValueIncreaseInvoiceAdapter", True, AddressOf OnValueFetched, _
                    selectedAsset.ID, _ParentChronologyValidator, Not _ForInvoiceReceived)
            ElseIf GetType(T) Is GetType(AssetSaleInvoiceAdapter) Then
                'AssetSaleInvoiceAdapter.NewAssetSaleInvoiceAdapter(selectedAsset.ID, _
                '    _ParentChronologyValidator, Not _ForInvoiceReceived)
                _QueryBrowser.InvokeQuery(Of AssetSaleInvoiceAdapter)(Nothing, "NewAssetSaleInvoiceAdapter", _
                    True, AddressOf OnValueFetched, selectedAsset.ID, _
                    _ParentChronologyValidator, Not _ForInvoiceReceived)
            Else
                MsgBox(String.Format("Klaida. Tipas {0} neimplementuotas formoje {1}.", _
                    GetType(T).FullName, Me.GetType.Name), MsgBoxStyle.Exclamation, "Klaida")
                DisableAllControls(Me)
                Exit Sub
            End If

        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Private Sub OnValueFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        _Value = DirectCast(result, T)

        Me.Hide()
        Me.Close()

    End Sub

    Private Sub ICancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ICancelButton.Click

        Me.Hide()
        Me.Close()

    End Sub

End Class