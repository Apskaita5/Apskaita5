Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.Goods
Imports ApskaitaObjects.Documents
Imports ApskaitaObjects.Attributes

Public Class F_NewGoodsOperation(Of T)

    Private ReadOnly _TypesThatRequireGoods As Type() = New Type() {GetType(GoodsOperationAcquisition), _
        GetType(GoodsOperationAdditionalCosts), GetType(GoodsOperationDiscard), _
        GetType(GoodsOperationDiscount), GetType(GoodsOperationPriceCut), _
        GetType(GoodsOperationRedeemFromBuyer), GetType(GoodsOperationTransfer), GetType(GoodsOperationValuationMethod)}
    Private ReadOnly _TypesThatRequireWarehouseFrom As Type() = New Type() { _
        GetType(GoodsComplexOperationDiscard), GetType(GoodsComplexOperationInternalTransfer), _
        GetType(GoodsComplexOperationInventorization), GetType(GoodsOperationAdditionalCosts), _
        GetType(GoodsOperationDiscard), GetType(GoodsOperationDiscount), GetType(GoodsOperationTransfer)}
    Private ReadOnly _TypesThatRequireWarehouseTo As Type() = New Type() { _
        GetType(GoodsComplexOperationInternalTransfer), GetType(GoodsOperationAcquisition), _
        GetType(GoodsOperationRedeemFromBuyer)}

    Private _Result As T = Nothing
    Private _QueryManager As CslaActionExtenderQueryObject
    Private _GoodsID As Integer = 0


    Public ReadOnly Property Result() As T
        Get
            Return _Result
        End Get
    End Property


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal goodsID As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _GoodsID = goodsID

    End Sub


    Private Sub F_NewGoodsOperation_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Dim servicedTypes As New List(Of Type)
        For Each t As Type In _TypesThatRequireGoods
            If Not servicedTypes.Contains(t) Then servicedTypes.Add(t)
        Next
        For Each t As Type In _TypesThatRequireWarehouseFrom
            If Not servicedTypes.Contains(t) Then servicedTypes.Add(t)
        Next
        For Each t As Type In _TypesThatRequireWarehouseTo
            If Not servicedTypes.Contains(t) Then servicedTypes.Add(t)
        Next

        If Not servicedTypes.Contains(GetType(T)) Then
            Throw New NotImplementedException(String.Format("Klaida. Prekių operacijos tipas {0} neimplementuotas klasėje {1}.", _
                GetType(T).FullName, Me.GetType.FullName))
        End If

        If Not PrepareCache(Me, GetType(HelperLists.GoodsInfoList), GetType(HelperLists.WarehouseInfoList)) Then Exit Sub

        Try

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller1)

            PrepareControl(GoodsInfoListAccListComboBox, _
                New GoodsFieldAttribute(ValueRequiredLevel.Optional))
            PrepareControl(WarehouseFromInfoListAccListComboBox, _
                New WarehouseFieldAttribute(ValueRequiredLevel.Optional))
            PrepareControl(WarehouseToInfoListAccListComboBox, _
                New WarehouseFieldAttribute(ValueRequiredLevel.Optional))

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

        OperationDateLabel.Visible = (GetType(T) Is GetType(Goods.GoodsComplexOperationInventorization))
        OperationDateAccDatePicker.Visible = (GetType(T) Is GetType(Goods.GoodsComplexOperationInventorization))
        GoodsLabel.Visible = Not Array.IndexOf(_TypesThatRequireGoods, GetType(T)) < 0
        GoodsInfoListAccListComboBox.Visible = Not Array.IndexOf(_TypesThatRequireGoods, GetType(T)) < 0
        WarehouseFromLabel.Visible = Not Array.IndexOf(_TypesThatRequireWarehouseFrom, GetType(T)) < 0
        WarehouseFromInfoListAccListComboBox.Visible = Not Array.IndexOf(_TypesThatRequireWarehouseFrom, GetType(T)) < 0
        WarehouseToLabel.Visible = Not Array.IndexOf(_TypesThatRequireWarehouseTo, GetType(T)) < 0
        WarehouseToInfoListAccListComboBox.Visible = Not Array.IndexOf(_TypesThatRequireWarehouseTo, GetType(T)) < 0

        If GetType(T) Is GetType(GoodsComplexOperationDiscard) Then
            Me.Text = "Nauja kompleksinė prekių nurašymo operacija"

        ElseIf GetType(T) Is GetType(GoodsComplexOperationInternalTransfer) Then
            Me.Text = "Nauja kompleksinė prekių vidinio judėjimo operacija"

        ElseIf GetType(T) Is GetType(GoodsComplexOperationInventorization) Then
            Me.Text = "Nauja kompleksinė prekių vidinio judėjimo operacija"

        ElseIf GetType(T) Is GetType(GoodsOperationAcquisition) Then
            Me.Text = "Nauja prekių įsigijimo operacija"

        ElseIf GetType(T) Is GetType(GoodsOperationAdditionalCosts) Then
            Me.Text = "Nauja prekių įsigijimo savikainos padidinimo operacija"

        ElseIf GetType(T) Is GetType(GoodsOperationDiscard) Then
            Me.Text = "Nauja prekių nurašymo operacija"

        ElseIf GetType(T) Is GetType(GoodsOperationDiscount) Then
            Me.Text = "Nauja prekėms gautos nuolaidos operacija"

        ElseIf GetType(T) Is GetType(GoodsOperationPriceCut) Then
            Me.Text = "Nauja prekių nukainojimo operacija"

        ElseIf GetType(T) Is GetType(GoodsOperationRedeemFromBuyer) Then
            Me.Text = "Nauja prekių susigrąžinimo iš pirkėjo operacija"

        ElseIf GetType(T) Is GetType(GoodsOperationTransfer) Then
            Me.Text = "Nauja prekių perleidimo operacija"

        ElseIf GetType(T) Is GetType(GoodsOperationValuationMethod) Then
            Me.Text = "Nauja prekių vertinimo metodo pakeitimo operacija"

        Else
            Throw New NotImplementedException(String.Format("Klaida. Prekių operacijos tipas {0} neimplementuotas klasėje {1}.", _
                GetType(T).FullName, Me.GetType.FullName))

        End If

        If _GoodsID > 0 Then

            Dim list As HelperLists.GoodsInfoList = HelperLists.GoodsInfoList.GetList()
            Dim selectedItem As HelperLists.GoodsInfo = list.GetItem(_GoodsID)
            If Not selectedItem Is Nothing AndAlso Not selectedItem.IsEmpty Then
                GoodsInfoListAccListComboBox.SelectedValue = selectedItem
                If Array.IndexOf(_TypesThatRequireWarehouseFrom, GetType(T)) < 0 _
                    AndAlso Array.IndexOf(_TypesThatRequireWarehouseTo, GetType(T)) < 0 Then
                    nOkButton.PerformClick()
                End If
            End If

        End If

    End Sub


    Private Sub nOkButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nOkButton.Click

        Dim goodsInfo As HelperLists.GoodsInfo = Nothing
        Dim warehouseFrom As HelperLists.WarehouseInfo = Nothing
        Dim warehouseTo As HelperLists.WarehouseInfo = Nothing

        Try
            goodsInfo = DirectCast(GoodsInfoListAccListComboBox.SelectedValue, HelperLists.GoodsInfo)
        Catch ex As Exception
        End Try
        Try
            warehouseFrom = DirectCast(WarehouseFromInfoListAccListComboBox.SelectedValue, HelperLists.WarehouseInfo)
        Catch ex As Exception
        End Try
        Try
            warehouseTo = DirectCast(WarehouseToInfoListAccListComboBox.SelectedValue, HelperLists.WarehouseInfo)
        Catch ex As Exception
        End Try

        If Not Array.IndexOf(_TypesThatRequireGoods, GetType(T)) < 0 AndAlso (goodsInfo Is Nothing _
            OrElse goodsInfo.IsEmpty) Then
            MsgBox("Klaida. Nenurodytos prekės.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        ElseIf Not Array.IndexOf(_TypesThatRequireWarehouseFrom, GetType(T)) < 0 AndAlso (warehouseFrom Is Nothing _
            OrElse warehouseFrom.IsEmpty) Then
            MsgBox("Klaida. Nenurodytas sandėlis.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        ElseIf Not Array.IndexOf(_TypesThatRequireWarehouseTo, GetType(T)) < 0 AndAlso (warehouseTo Is Nothing _
            OrElse warehouseTo.IsEmpty) Then
            MsgBox("Klaida. Nenurodytas sandėlis, į kurį prekės perduodamos.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If


        If GetType(T) Is GetType(GoodsComplexOperationDiscard) Then
            'GoodsComplexOperationDiscard.NewGoodsComplexOperationDiscard(warehouseFrom.ID)
            _QueryManager.InvokeQuery(Of GoodsComplexOperationDiscard)(Nothing, _
                "NewGoodsComplexOperationDiscard", True, AddressOf OnNewOperationFetched, warehouseFrom.ID)

        ElseIf GetType(T) Is GetType(GoodsComplexOperationInternalTransfer) Then
            'GoodsComplexOperationInternalTransfer.NewGoodsComplexOperationInternalTransfer( _
            '    warehouseFrom.ID, warehouseTo.ID)
            _QueryManager.InvokeQuery(Of GoodsComplexOperationInternalTransfer)(Nothing, _
                "NewGoodsComplexOperationInternalTransfer", True, AddressOf OnNewOperationFetched, _
                warehouseFrom.ID, warehouseTo.ID)

        ElseIf GetType(T) Is GetType(GoodsComplexOperationInventorization) Then
            'GoodsComplexOperationInventorization.NewGoodsComplexOperationInventorization( _
            '    OperationDateDateTimePicker.Value, warehouseFrom.ID)
            _QueryManager.InvokeQuery(Of GoodsComplexOperationInventorization)(Nothing, _
                "NewGoodsComplexOperationInventorization", True, AddressOf OnNewOperationFetched, _
                OperationDateAccDatePicker.Value, warehouseFrom.ID)

        ElseIf GetType(T) Is GetType(GoodsOperationAcquisition) Then
            'GoodsOperationAcquisition.NewGoodsOperationAcquisition(goodsInfo.ID, warehouseTo.ID)
            _QueryManager.InvokeQuery(Of GoodsOperationAcquisition)(Nothing, _
                "NewGoodsOperationAcquisition", True, AddressOf OnNewOperationFetched, _
                goodsInfo.ID, warehouseTo.ID)

        ElseIf GetType(T) Is GetType(GoodsOperationAdditionalCosts) Then
            'GoodsOperationAdditionalCosts.NewGoodsOperationAdditionalCosts(goodsInfo.ID, warehouseFrom.ID)
            _QueryManager.InvokeQuery(Of GoodsOperationAdditionalCosts)(Nothing, _
                "NewGoodsOperationAdditionalCosts", True, AddressOf OnNewOperationFetched, _
                goodsInfo.ID, warehouseFrom.ID)

        ElseIf GetType(T) Is GetType(GoodsOperationDiscard) Then
            'GoodsOperationDiscard.NewGoodsOperationDiscard(goodsInfo.ID, warehouseFrom.ID)
            _QueryManager.InvokeQuery(Of GoodsOperationDiscard)(Nothing, _
                "NewGoodsOperationDiscard", True, AddressOf OnNewOperationFetched, _
                goodsInfo.ID, warehouseFrom.ID)

        ElseIf GetType(T) Is GetType(GoodsOperationDiscount) Then
            'GoodsOperationDiscount.NewGoodsOperationDiscount(goodsInfo.ID, warehouseFrom.ID)
            _QueryManager.InvokeQuery(Of GoodsOperationDiscount)(Nothing, _
                "NewGoodsOperationDiscount", True, AddressOf OnNewOperationFetched, _
                goodsInfo.ID, warehouseFrom.ID)

        ElseIf GetType(T) Is GetType(GoodsOperationPriceCut) Then
            'GoodsOperationPriceCut.NewGoodsOperationPriceCut(goodsInfo.ID)
            _QueryManager.InvokeQuery(Of GoodsOperationPriceCut)(Nothing, _
                "NewGoodsOperationPriceCut", True, AddressOf OnNewOperationFetched, goodsInfo.ID)

        ElseIf GetType(T) Is GetType(GoodsOperationRedeemFromBuyer) Then
            'GoodsOperationRedeemFromBuyer.NewGoodsOperationRedeemFromBuyer(goodsInfo.ID, warehouseTo.ID)
            _QueryManager.InvokeQuery(Of GoodsOperationRedeemFromBuyer)(Nothing, _
                "NewGoodsOperationRedeemFromBuyer", True, AddressOf OnNewOperationFetched, _
                goodsInfo.ID, warehouseTo.ID)

        ElseIf GetType(T) Is GetType(GoodsOperationTransfer) Then
            'GoodsOperationTransfer.NewGoodsOperationTransfer(goodsInfo.ID, warehouseFrom.ID)
            _QueryManager.InvokeQuery(Of GoodsOperationTransfer)(Nothing, _
                "NewGoodsOperationTransfer", True, AddressOf OnNewOperationFetched, _
                goodsInfo.ID, warehouseFrom.ID)

        ElseIf GetType(T) Is GetType(GoodsOperationValuationMethod) Then
            'GoodsOperationValuationMethod.NewGoodsOperationValuationMethod(goodsInfo.ID)
            _QueryManager.InvokeQuery(Of GoodsOperationValuationMethod)(Nothing, _
                "NewGoodsOperationValuationMethod", True, AddressOf OnNewOperationFetched, goodsInfo.ID)

        Else
            Throw New NotImplementedException(String.Format("Klaida. Prekių operacijos tipas {0} neimplementuotas klasėje {1}.", _
                GetType(T).FullName, Me.GetType.FullName))

        End If


    End Sub

    Private Sub OnNewOperationFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        _Result = DirectCast(result, T)

        Me.Close()

    End Sub

    Private Sub nCancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        Me.Close()
    End Sub

End Class