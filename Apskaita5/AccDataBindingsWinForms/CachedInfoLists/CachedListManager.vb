Imports AccControlsWinForms
Imports AccDataAccessLayer
Imports ApskaitaObjects.HelperLists
Imports System.Windows.Forms
Imports ApskaitaObjects.Settings

Namespace CachedInfoLists

    Public Module CachedListManager

#Region "*** CACHE BINDINGS MANAGER METHODS ***"

        Private _BindingSourceList As List(Of BindingSourceItem)

        Friend Function GetBindingSourceForCachedList(Of T)( _
            ByVal ParamArray filterCriteria As Object()) As BindingSource

            Dim result As New BindingSourceItem(GetType(T), filterCriteria)

            If _BindingSourceList Is Nothing Then
                _BindingSourceList = New List(Of BindingSourceItem)
            End If
            _BindingSourceList.Add(result)

            AddHandler result.BindingSourceInstance.Disposed, AddressOf BindingSource_Disposed

            Return result.BindingSourceInstance

        End Function

        Public Sub CachedListChanged(ByVal e As CacheChangedEventArgs)
            If _BindingSourceList Is Nothing Then Exit Sub
            For Each bs As BindingSourceItem In _BindingSourceList
                If bs.BaseType Is e.Type Then bs.UpdateDataSource()
            Next
        End Sub

        Private Sub BindingSource_Disposed(ByVal sender As Object, ByVal e As System.EventArgs)

            If _BindingSourceList Is Nothing Then Exit Sub

            For i As Integer = _BindingSourceList.Count To 1 Step -1
                If _BindingSourceList(i - 1).BindingSourceInstance Is DirectCast(sender, BindingSource) Then
                    _BindingSourceList.RemoveAt(i - 1)
                    RemoveHandler DirectCast(sender, BindingSource).Disposed, AddressOf BindingSource_Disposed
                    Exit For
                End If
            Next

        End Sub

        Public Function PrepareCache(ByVal callingForm As Form, ByVal ParamArray baseTypes As Type()) As Boolean

            Try
                Using busy As New StatusBusy()
                    CacheObjectList.GetList(baseTypes)
                End Using
            Catch ex As Exception
                ShowError(ex)
                If Not callingForm Is Nothing Then DisableAllControls(CType(callingForm, Control))
                Return False
            End Try

            Return True

        End Function

#End Region


        '------------------------------------
        'ComboBox'es
        '------------------------------------


        Public Sub LoadCurrencyCodeListToComboBox(ByVal comboControl As ComboBox, ByVal addEmptyItem As Boolean)
            comboControl.DataSource = AccCommon.CurrencyCodes
        End Sub

        Public Sub LoadDocumentSerialInfoListToCombo(ByVal comboObject As ComboBox, _
                                                     ByVal docType As DocumentSerialType, ByVal addEmptyItem As Boolean, ByVal reload As Boolean)

            If Not reload Then

                comboObject.ValueMember = "Serial"
                comboObject.DisplayMember = "Serial"

                comboObject.DataSource = GetBindingSourceForCachedList(Of DocumentSerialInfoList) _
                    (addEmptyItem, docType)

                AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

            End If

        End Sub

        Public Sub LoadEnumLocalizedListToComboBox(ByVal comboControl As ComboBox, _
                                                   ByVal enumType As Type, ByVal addEmptyItem As Boolean, _
                                                   ByVal ParamArray enumValues As [Enum]())
            comboControl.DataSource = GetEnumLocalizedList(enumType, addEmptyItem, enumValues)
        End Sub

        Private Function GetEnumLocalizedList(ByVal enumType As Type, ByVal addEmptyItem As Boolean, _
                                              ByVal enumValues As [Enum]()) As List(Of String)

            Dim result As List(Of String)

            If enumValues Is Nothing OrElse enumValues.Length < 1 Then

                result = ApskaitaObjects.Utilities.GetLocalizedNameList(enumType)

            Else

                result = New List(Of String)
                For Each enumValue As [Enum] In enumValues
                    result.Add(ApskaitaObjects.Utilities.ConvertLocalizedName(enumValue))
                Next

            End If

            If addEmptyItem Then result.Insert(0, "")

            Return result

        End Function

        Public Sub LoadLanguageListToComboBox(ByVal comboObject As ComboBox, _
                                              ByVal addEmptyItem As Boolean)

            If Not comboObject.DataSource Is Nothing Then Exit Sub

            comboObject.DataSource = GetBindingSourceForCachedList(Of CompanyRegionalInfoList) _
                (addEmptyItem)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadNameInfoListToCombo(ByVal comboObject As ComboBox, _
                                           ByVal ofType As ApskaitaObjects.Settings.NameType, ByVal addEmptyItem As Boolean)

            If Not comboObject.DataSource Is Nothing Then Exit Sub

            comboObject.DataSource = GetBindingSourceForCachedList(Of NameInfoList)( _
                ofType, addEmptyItem)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Function LoadTaxRateListToCombo(ByVal comboObject As ComboBox, _
                                               ByVal taxType As TaxRateType) As Boolean

            If Not comboObject.DataSource Is Nothing Then Exit Function

            comboObject.DataSource = GetBindingSourceForCachedList(Of TaxRateInfoList)(taxType, True)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

            Return True

        End Function


        '------------------------------------
        'AccListComboBox'es
        '------------------------------------


        Public Sub LoadAccountInfoListToListCombo(ByVal comboObject As AccListComboBox, _
                                                  ByVal addEmptyItem As Boolean, ByVal ParamArray classFilter() As Integer)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New AccountInfoListControl

            result.ValueMember = "ID"
            result.DataSource = GetBindingSourceForCachedList(Of AccountInfoList) _
                (addEmptyItem, classFilter)
            result.AcceptSingleClick = True

            comboObject.AddDataListView(result)
            comboObject.EmptyValueString = "0"

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadAssignableCRItemListToListCombo(ByVal comboObject As AccListComboBox, _
                                                       ByVal includeEmpty As Boolean)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New AssignableCRItemListControl

            result.DataSource = GetBindingSourceForCachedList(Of AssignableCRItemList) _
                (includeEmpty)
            result.AcceptSingleClick = True

            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadCashAccountInfoListToListCombo(ByRef comboObject As AccListComboBox, _
                                                      ByVal addEmptyItem As Boolean)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New CashAccountInfoListControl

            result.DataSource = GetBindingSourceForCachedList(Of CashAccountInfoList) _
                (True, addEmptyItem)
            result.AcceptSingleClick = True

            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadCodeInfoListToListCombo(ByVal comboObject As AccListComboBox, _
                                               ByVal ofType As CodeType, ByVal includeEmpty As Boolean, ByVal includeObsolete As Boolean)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New CodeInfoListControl

            result.ValueMember = "Code"
            result.DataSource = GetBindingSourceForCachedList(Of CodeInfoList) _
                (ofType, includeEmpty, includeObsolete)
            result.AcceptSingleClick = True

            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadGoodsGroupInfoListToListCombo(ByVal comboObject As AccListComboBox, _
                                                     ByVal addEmptyItem As Boolean)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New GoodsGroupInfoListControl

            result.DataSource = GetBindingSourceForCachedList(Of GoodsGroupInfoList) _
                (addEmptyItem)
            result.AcceptSingleClick = True

            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadGoodsInfoListToListCombo(ByVal comboObject As AccListComboBox, _
                                                ByVal addEmptyItem As Boolean, ByVal tradedType As ApskaitaObjects.Documents.TradedItemType)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New GoodsInfoListControl

            result.DataSource = GetBindingSourceForCachedList(Of GoodsInfoList) _
                (True, addEmptyItem, tradedType)
            result.AcceptSingleClick = True

            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadLocalUserListToGridCombo(ByVal comboObject As AccListComboBox, _
                                                ByVal list As AccDataAccessLayer.Security.LocalUserList)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New LocalUserListControl

            Dim listBindingSource As BindingSource = New BindingSource
            listBindingSource.DataSource = list

            result.DataSource = listBindingSource
            result.AcceptSingleClick = True

            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadLongTermAssetCustomGroupInfoToListCombo(ByVal comboObject As AccListComboBox, _
                                                               ByVal addEmptyItem As Boolean)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New LongTermAssetCustomGroupInfoListControl

            result.DataSource = GetBindingSourceForCachedList(Of LongTermAssetCustomGroupInfoList) _
                (addEmptyItem)
            result.AcceptSingleClick = True

            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadPersonGroupInfoListToListCombo(ByVal comboObject As AccListComboBox)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New PersonGroupInfoListControl

            result.DataSource = GetBindingSourceForCachedList(Of PersonGroupInfoList)(True)
            result.AcceptSingleClick = True
            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadPersonInfoListToListCombo(ByVal comboObject As AccListComboBox, _
                                                 ByVal addEmptyItem As Boolean, ByVal showClients As Boolean, _
                                                 ByVal showSuppliers As Boolean, ByVal showWorkers As Boolean)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New PersonInfoListControl

            result.DataSource = GetBindingSourceForCachedList(Of PersonInfoList) _
                (addEmptyItem, showClients, showSuppliers, showWorkers)
            result.AcceptSingleClick = True
            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadProductionCalculationInfoListToListCombo(ByVal comboObject As AccListComboBox, _
                                                                ByVal addEmptyItem As Boolean, ByVal showObsolete As Boolean)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New ProductionCalculationInfoListControl

            result.DataSource = GetBindingSourceForCachedList(Of ProductionCalculationInfoList) _
                (addEmptyItem, showObsolete)
            result.AcceptSingleClick = True
            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadServiceInfoListToListCombo(ByVal comboObject As AccListComboBox, _
                                                  ByVal addEmptyItem As Boolean, ByVal showSales As Boolean, _
                                                  ByVal showPurchases As Boolean)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New ServiceInfoListControl

            result.DataSource = GetBindingSourceForCachedList(Of ServiceInfoList) _
                (addEmptyItem, showSales, showPurchases, True)
            result.AcceptSingleClick = True
            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadVatDeclarationSchemaInfoListToListCombo(ByVal comboObject As AccListComboBox, _
            ByVal addEmptyItem As Boolean, ByVal showSales As Boolean, _
            ByVal showPurchases As Boolean)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New VatDeclarationSchemaInfoListControl

            result.DataSource = GetBindingSourceForCachedList(Of VatDeclarationSchemaInfoList) _
                (addEmptyItem, showSales, showPurchases, True)
            result.AcceptSingleClick = True
            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadWarehouseInfoListToListCombo(ByVal comboObject As AccListComboBox, _
                                                    ByVal addEmptyItem As Boolean)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New WarehouseInfoListControl

            result.DataSource = GetBindingSourceForCachedList(Of WarehouseInfoList) _
                (addEmptyItem, True)
            result.AcceptSingleClick = True
            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub

        Public Sub LoadWorkTimeClassInfoListToListCombo(ByVal comboObject As AccListComboBox, _
                                                        ByVal addEmptyItem As Boolean, ByVal showWithoutHours As Boolean, _
                                                        ByVal showWithHours As Boolean)

            If comboObject.HasAttachedInfoList Then Exit Sub

            Dim result As New WorkTimeClassInfoListControl

            result.DataSource = GetBindingSourceForCachedList(Of WorkTimeClassInfoList) _
                (addEmptyItem, showWithoutHours, showWithHours)
            result.AcceptSingleClick = True
            comboObject.AddDataListView(result)

            AddHandler comboObject.Disposed, AddressOf ComboControl_Disposed

        End Sub


        Private Sub ComboControl_Disposed(ByVal sender As Object, ByVal e As System.EventArgs)
            If TypeOf sender Is ComboBox Then

                Try
                    RemoveHandler DirectCast(sender, ComboBox).Disposed, _
                        AddressOf ComboControl_Disposed
                Catch ex As Exception
                End Try

                If Not DirectCast(sender, ComboBox).DataSource Is Nothing _
                   AndAlso TypeOf DirectCast(sender, ComboBox).DataSource Is BindingSource Then
                    Try
                        DirectCast(DirectCast(sender, ComboBox).DataSource, BindingSource).Dispose()
                    Catch ex As Exception
                    End Try
                End If

            ElseIf TypeOf sender Is AccListComboBox Then

                Try
                    RemoveHandler DirectCast(sender, AccListComboBox).Disposed, _
                        AddressOf ComboControl_Disposed
                Catch ex As Exception
                End Try

                If Not DirectCast(sender, AccListComboBox).InfoListControlDataSource Is Nothing _
                   AndAlso TypeOf DirectCast(sender, AccListComboBox).InfoListControlDataSource Is BindingSource Then
                    Try
                        DirectCast(DirectCast(sender, AccListComboBox).InfoListControlDataSource,  _
                                   BindingSource).Dispose()
                    Catch ex As Exception
                    End Try
                End If

            End If

        End Sub

    End Module

End Namespace