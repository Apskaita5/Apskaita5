Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.Goods
Imports ApskaitaObjects.Documents
Imports ApskaitaObjects.Attributes

Public Class F_NewGoodsProductionOperation

    Private _Result As GoodsComplexOperationProduction = Nothing
    Private _QueryManager As CslaActionExtenderQueryObject
    Private _GoodsID As Integer = 0


    Public ReadOnly Property Result() As GoodsComplexOperationProduction
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


    Private Sub F_NewGoodsProductionOperation_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not PrepareCache(Me, GetType(HelperLists.GoodsInfoList), GetType(HelperLists.WarehouseInfoList), _
            GetType(HelperLists.ProductionCalculationInfoList)) Then Exit Sub

        Try

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller1)

            PrepareControl(GoodsInfoListAccListComboBox, _
                New GoodsFieldAttribute(ValueRequiredLevel.Optional))
            PrepareControl(WarehouseFromInfoListAccListComboBox, _
                New WarehouseFieldAttribute(ValueRequiredLevel.Optional))
            PrepareControl(WarehouseToInfoListAccListComboBox, _
                New WarehouseFieldAttribute(ValueRequiredLevel.Optional))
            PrepareControl(ProductionCalculationInfoListAccListComboBox, _
                New ProductionCalculationFieldAttribute(ValueRequiredLevel.Optional))

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

        If _GoodsID > 0 Then

            Dim list As HelperLists.GoodsInfoList = HelperLists.GoodsInfoList.GetList()
            Dim selectedItem As HelperLists.GoodsInfo = list.GetItem(_GoodsID)
            If Not selectedItem Is Nothing AndAlso Not selectedItem.IsEmpty Then
                GoodsInfoListAccListComboBox.SelectedValue = selectedItem
            End If

        End If

    End Sub


    Private Sub nOkButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nOkButton.Click

        Dim calculationInfo As HelperLists.ProductionCalculationInfo = Nothing
        Dim goodsInfo As HelperLists.GoodsInfo = Nothing
        Dim warehouseFrom As HelperLists.WarehouseInfo = Nothing
        Dim warehouseTo As HelperLists.WarehouseInfo = Nothing

        Try
            calculationInfo = DirectCast(ProductionCalculationInfoListAccListComboBox.SelectedValue, HelperLists.ProductionCalculationInfo)
        Catch ex As Exception
        End Try
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

        If Not UseCalculationCheckBox.Checked AndAlso (goodsInfo Is Nothing OrElse goodsInfo.IsEmpty) Then
            MsgBox("Klaida. Nenurodytos gaminamos prekės.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        ElseIf warehouseFrom Is Nothing OrElse warehouseFrom.IsEmpty Then
            MsgBox("Klaida. Nenurodytas žaliavų sandėlis.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        ElseIf warehouseTo Is Nothing OrElse warehouseTo.IsEmpty Then
            MsgBox("Klaida. Nenurodytas gaminių sandėlis.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        ElseIf UseCalculationCheckBox.Checked AndAlso (calculationInfo Is Nothing OrElse calculationInfo.IsEmpty) Then
            MsgBox("Klaida. Nenurodyta gamybos kalkuliacija.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        If UseCalculationCheckBox.Checked Then
            'GoodsComplexOperationProduction.NewGoodsComplexOperationProductionByCalculation( _
            '    calculationInfo.ID, warehouseTo.ID, warehouseFrom.ID)
            _QueryManager.InvokeQuery(Of GoodsComplexOperationProduction)(Nothing, _
                "NewGoodsComplexOperationProductionByCalculation", True, _
                AddressOf OnNewOperationFetched, calculationInfo.ID, warehouseTo.ID, warehouseFrom.ID)
        Else
            'GoodsComplexOperationProduction.NewGoodsComplexOperationProduction( _
            '    goodsInfo.ID, warehouseTo.ID, warehouseFrom.ID)
            _QueryManager.InvokeQuery(Of GoodsComplexOperationProduction)(Nothing, _
                "NewGoodsComplexOperationProduction", True, _
                AddressOf OnNewOperationFetched, goodsInfo.ID, warehouseTo.ID, warehouseFrom.ID)
        End If

    End Sub

    Private Sub OnNewOperationFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        _Result = DirectCast(result, GoodsComplexOperationProduction)

        Me.Hide()
        Me.Close()

    End Sub

    Private Sub nCancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        Me.Hide()
        Me.Close()
    End Sub


    Private Sub UseCalculationCheckBox_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles UseCalculationCheckBox.CheckedChanged

        ProductionCalculationInfoListAccListComboBox.Enabled = UseCalculationCheckBox.Checked
        GoodsInfoListAccListComboBox.Enabled = Not UseCalculationCheckBox.Checked

    End Sub

    Private Sub ProductionCalculationInfoListAccListComboBox_SelectedValueChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles ProductionCalculationInfoListAccListComboBox.SelectedValueChanged

        If ProductionCalculationInfoListAccListComboBox.SelectedValue Is Nothing Then Exit Sub

        Dim current As HelperLists.ProductionCalculationInfo = DirectCast( _
            ProductionCalculationInfoListAccListComboBox.SelectedValue, HelperLists.ProductionCalculationInfo)

        If current.IsEmpty Then Exit Sub

        Dim list As HelperLists.GoodsInfoList = HelperLists.GoodsInfoList.GetList()
        Dim selectedItem As HelperLists.GoodsInfo = list.GetItem(current.GoodsID)
        If Not selectedItem Is Nothing AndAlso Not selectedItem.IsEmpty Then
            GoodsInfoListAccListComboBox.SelectedValue = selectedItem
        End If

    End Sub

End Class