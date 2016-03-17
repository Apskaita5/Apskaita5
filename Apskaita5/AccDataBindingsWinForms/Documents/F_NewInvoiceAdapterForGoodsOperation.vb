Imports ApskaitaObjects.Documents.InvoiceAdapters
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.Documents

Friend Class F_NewInvoiceAdapterForGoodsOperation(Of T)

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


    Private Sub InvoiceAdapterForGoodsOperation_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not PrepareCache(Me, GetType(HelperLists.GoodsInfoList), GetType(HelperLists.WarehouseInfoList)) Then Exit Sub

        Try
            LoadGoodsInfoListToListCombo(GoodsInfoListAccGridComboBox, True, TradedItemType.All)
            LoadWarehouseInfoListToListCombo(WarehouseInfoListAccGridComboBox, True)

            _QueryBrowser = New CslaActionExtenderQueryObject(Me, ProgressFiller1)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
        End Try

        If GetType(T) Is GetType(GoodsAcquisitionInvoiceAdapter) Then
            Me.Text = "Nauja prekių įsigijimo operacija"
        ElseIf GetType(T) Is GetType(GoodsAddedCostsInvoiceAdapter) Then
            Me.Text = "Nauja prekių savikainos padidinimo operacija"
        ElseIf GetType(T) Is GetType(GoodsDiscountInvoiceAdapter) Then
            Me.Text = "Nauja gautos nuolaidos prekėms operacija"
        ElseIf GetType(T) Is GetType(GoodsRedeemFromBuyerInvoiceAdapter) Then
            Me.Text = "Nauja prekių susigrąžinimo iš pirkėjo operacija"
        ElseIf GetType(T) Is GetType(GoodsSaleInvoiceAdapter) Then
            Me.Text = "Nauja prekių pardavimo operacija"
        Else
            MsgBox(String.Format("Klaida. Tipas {0} neimplementuotas formoje {1}.", _
                GetType(T).FullName, Me.GetType.Name), MsgBoxStyle.Exclamation, "Klaida")
            DisableAllControls(Me)
            Exit Sub
        End If

        BarCodeTextBox.Focus()

    End Sub


    Private Sub IOkButton_Click(ByVal sender As System.Object, _
       ByVal e As System.EventArgs) Handles IOkButton.Click

        Dim selectedGoods As HelperLists.GoodsInfo = Nothing
        Try
            selectedGoods = DirectCast(GoodsInfoListAccGridComboBox.SelectedValue, HelperLists.GoodsInfo)
        Catch ex As Exception
        End Try
        If selectedGoods Is Nothing OrElse selectedGoods.IsEmpty Then
            MsgBox("Klaida. Nepasirinkta prekė.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Dim selectedWarehouse As HelperLists.WarehouseInfo = Nothing
        Try
            selectedWarehouse = DirectCast(WarehouseInfoListAccGridComboBox.SelectedValue, HelperLists.WarehouseInfo)
        Catch ex As Exception
        End Try
        If selectedWarehouse Is Nothing OrElse selectedWarehouse.IsEmpty Then
            MsgBox("Klaida. Nepasirinktas sandėlys.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Try

            If GetType(T) Is GetType(GoodsAcquisitionInvoiceAdapter) Then
                'GoodsAcquisitionInvoiceAdapter.NewGoodsAcquisitionInvoiceAdapter(selectedGoods.ID, _
                '    _ParentChronologyValidator, Not _ForInvoiceReceived)
                _QueryBrowser.InvokeQuery(Of GoodsAcquisitionInvoiceAdapter)(Nothing, "NewGoodsAcquisitionInvoiceAdapter", _
                    True, AddressOf OnValueFetched, selectedGoods.ID, _
                    _ParentChronologyValidator, Not _ForInvoiceReceived)
            ElseIf GetType(T) Is GetType(GoodsAddedCostsInvoiceAdapter) Then
                'GoodsAddedCostsInvoiceAdapter.NewGoodsAddedCostsInvoiceAdapter(selectedGoods.ID, _
                '    selectedWarehouse.ID, _ParentChronologyValidator, Not _ForInvoiceReceived)
                _QueryBrowser.InvokeQuery(Of GoodsAddedCostsInvoiceAdapter)(Nothing, "NewGoodsAddedCostsInvoiceAdapter", _
                    True, AddressOf OnValueFetched, selectedGoods.ID, _
                    selectedWarehouse.ID, _ParentChronologyValidator, Not _ForInvoiceReceived)
            ElseIf GetType(T) Is GetType(GoodsDiscountInvoiceAdapter) Then
                'GoodsDiscountInvoiceAdapter.NewGoodsDiscountInvoiceAdapter(selectedGoods.ID, _
                '    selectedWarehouse.ID, _ParentChronologyValidator, Not _ForInvoiceReceived)
                _QueryBrowser.InvokeQuery(Of GoodsDiscountInvoiceAdapter)(Nothing, "NewGoodsDiscountInvoiceAdapter", _
                    True, AddressOf OnValueFetched, selectedGoods.ID, _
                    selectedWarehouse.ID, _ParentChronologyValidator, Not _ForInvoiceReceived)
            ElseIf GetType(T) Is GetType(GoodsRedeemFromBuyerInvoiceAdapter) Then
                'GoodsRedeemFromBuyerInvoiceAdapter.NewGoodsRedeemFromBuyerInvoiceAdapter(selectedGoods.ID, _
                '    _ParentChronologyValidator, Not _ForInvoiceReceived)
                _QueryBrowser.InvokeQuery(Of GoodsRedeemFromBuyerInvoiceAdapter)(Nothing, "NewGoodsRedeemFromBuyerInvoiceAdapter", _
                    True, AddressOf OnValueFetched, selectedGoods.ID, _
                    _ParentChronologyValidator, Not _ForInvoiceReceived)
            ElseIf GetType(T) Is GetType(GoodsSaleInvoiceAdapter) Then
                'GoodsSaleInvoiceAdapter.NewGoodsSaleInvoiceAdapter(selectedGoods.ID, _
                '    selectedWarehouse.ID, _ParentChronologyValidator, Not _ForInvoiceReceived)
                _QueryBrowser.InvokeQuery(Of GoodsSaleInvoiceAdapter)(Nothing, "NewGoodsSaleInvoiceAdapter", _
                    True, AddressOf OnValueFetched, selectedGoods.ID, _
                    selectedWarehouse.ID, _ParentChronologyValidator, Not _ForInvoiceReceived)
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


    Private Sub SelectGoodsInfoButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles SelectGoodsInfoButton.Click
        GoodsInfoListAccGridComboBox.SelectedValue = HelperLists.GoodsInfoList.GetList. _
            GetItemByBarCode(Me.BarCodeTextBox.Text.Trim)
    End Sub

    Private Sub BarCodeTextBox_TextChanged(ByVal sender As System.Object, _
        ByVal e As System.Windows.Forms.KeyEventArgs) Handles BarCodeTextBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            SelectGoodsInfoButton_Click(sender, New EventArgs)
        End If
    End Sub

End Class