Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.Goods
Imports ApskaitaObjects.Documents
Imports ApskaitaObjects.Attributes

Public Class F_NewGoodsOperationAccountChange

    Private _Result As GoodsOperationAccountChange = Nothing
    Private _QueryManager As CslaActionExtenderQueryObject
    Private _GoodsID As Integer = 0


    Public ReadOnly Property Result() As GoodsOperationAccountChange
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


    Private Sub F_NewGoodsOperationAccountChange_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not PrepareCache(Me, GetType(HelperLists.GoodsInfoList)) Then Exit Sub

        Try

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller1)

            PrepareControl(GoodsInfoListAccListComboBox, _
                New GoodsFieldAttribute(ValueRequiredLevel.Optional))
            PrepareControl(AccountTypeComboBox, New LocalizedEnumFieldAttribute( _
                GetType(GoodsOperationType), False, "", _
                GoodsOperationType.AccountDiscountsChange, _
                GoodsOperationType.AccountPurchasesChange, _
                GoodsOperationType.AccountSalesNetCostsChange, _
                GoodsOperationType.AccountValueReductionChange))

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

        Dim goodsInfo As HelperLists.GoodsInfo = Nothing
        Dim accountType As GoodsOperationType = GoodsOperationType.Acquisition

        Try
            accountType = ConvertLocalizedName(Of GoodsOperationType)(AccountTypeComboBox.SelectedItem.ToString)
        Catch ex As Exception
        End Try
        Try
            goodsInfo = DirectCast(GoodsInfoListAccListComboBox.SelectedValue, HelperLists.GoodsInfo)
        Catch ex As Exception
        End Try

        If goodsInfo Is Nothing OrElse goodsInfo.IsEmpty Then
            MsgBox("Klaida. Nenurodytos gaminamos prekės.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        ElseIf accountType = GoodsOperationType.Acquisition Then
            MsgBox("Klaida. Nenurodytas keičiamos sąskaitos tipas.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        'GoodsOperationAccountChange.NewGoodsOperationAccountChange(goodsInfo.ID, accountType)
        _QueryManager.InvokeQuery(Of GoodsOperationAccountChange)(Nothing, _
            "NewGoodsOperationAccountChange", True, _
            AddressOf OnNewOperationFetched, goodsInfo.ID, accountType)

    End Sub

    Private Sub OnNewOperationFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        _Result = DirectCast(result, GoodsOperationAccountChange)

        Me.Close()

    End Sub

    Private Sub nCancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        Me.Close()
    End Sub

End Class