Imports ApskaitaObjects
Imports ApskaitaObjects.HelperLists
Imports ApskaitaObjects.Settings
Imports System.Windows.Forms

Namespace CachedInfoLists

    Public Class BindingSourceItem

        Private WithEvents _BindingSourceInstance As BindingSource
        Private _BaseType As Type
        Private _FilterCriteria As Object()


        Public ReadOnly Property BindingSourceInstance() As BindingSource
            Get
                Return _BindingSourceInstance
            End Get
        End Property

        Public ReadOnly Property BaseType() As Type
            Get
                Return _BaseType
            End Get
        End Property

        Public ReadOnly Property FilterCriteria() As Object()
            Get
                Return _FilterCriteria
            End Get
        End Property


        Public Sub New(ByVal cachedItemBaseType As Type, ByVal nFilterCriteria As Object())

            Dim dataSource As Object = GetDataSource(cachedItemBaseType, nFilterCriteria)

            _BindingSourceInstance = New BindingSource
            _BindingSourceInstance.DataSource = dataSource

            _BaseType = CachedItemBaseType
            _FilterCriteria = nFilterCriteria

        End Sub


        Public Sub UpdateDataSource()

            Dim dataSource As Object = GetDataSource(_BaseType, _FilterCriteria)

            Dim currentObject As Object = Nothing
            If Not _BindingSourceInstance.Current Is Nothing Then
                Try
                    currentObject = Clone(_BindingSourceInstance.Current)
                Catch ex As Exception
                End Try
            End If

            _BindingSourceInstance.SuspendBinding()

            _BindingSourceInstance.DataSource = Nothing
            _BindingSourceInstance.DataSource = dataSource

            _BindingSourceInstance.ResumeBinding()

            If Not currentObject Is Nothing AndAlso Not _BindingSourceInstance.IndexOf(currentObject) < 0 Then
                _BindingSourceInstance.Position = _BindingSourceInstance.IndexOf(currentObject)
            Else
                _BindingSourceInstance.Position = -1
            End If

            _BindingSourceInstance.ResetBindings(False)

        End Sub

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return (obj.GetType Is GetType(BindingSourceItem) AndAlso _
                    DirectCast(obj, BindingSourceItem).BindingSourceInstance Is Me._BindingSourceInstance)
        End Function

        Private Function GetDataSource(ByVal cachedItemBaseType As Type, _
                                       ByRef nFilterCriteria As Object()) As Object

            Dim result As Object = Nothing

            If CachedItemBaseType Is GetType(AccountInfoList) Then

                If nFilterCriteria.Length > 1 AndAlso Not nFilterCriteria(1) Is Nothing Then
                    result = AccountInfoList.GetCachedFilteredList( _
                        DirectCast(nFilterCriteria(0), Boolean), _
                        DirectCast(nFilterCriteria(1), Integer()))
                Else
                    result = AccountInfoList.GetCachedFilteredList( _
                        DirectCast(nFilterCriteria(0), Boolean))
                End If

            ElseIf CachedItemBaseType Is GetType(CompanyRegionalInfoList) Then

                If nFilterCriteria Is Nothing OrElse nFilterCriteria.Length < 1 Then
                    result = CompanyRegionalInfoList.GetList
                Else
                    result = CompanyRegionalInfoList.GetCachedFilteredList(DirectCast(nFilterCriteria(0), Boolean))
                End If

            ElseIf CachedItemBaseType Is GetType(PersonGroupInfoList) Then

                result = PersonGroupInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), Boolean))

            ElseIf CachedItemBaseType Is GetType(PersonInfoList) Then

                result = PersonInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), Boolean), _
                    DirectCast(nFilterCriteria(1), Boolean), _
                    DirectCast(nFilterCriteria(2), Boolean), _
                    DirectCast(nFilterCriteria(3), Boolean))

            ElseIf CachedItemBaseType Is GetType(AssignableCRItemList) Then

                result = AssignableCRItemList.GetCachedFilteredList(True)

            ElseIf cachedItemBaseType Is GetType(CodeInfoList) Then

                result = CodeInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), ApskaitaObjects.Settings.CodeType), _
                    DirectCast(nFilterCriteria(1), Boolean), _
                    DirectCast(nFilterCriteria(2), Boolean))

            ElseIf cachedItemBaseType Is GetType(DefaultWorkTimeInfoList) Then

                result = DefaultWorkTimeInfoList.GetList

            ElseIf cachedItemBaseType Is GetType(NameInfoList) Then

                result = NameInfoList.GetList.GetStringList( _
                    DirectCast(nFilterCriteria(0), ApskaitaObjects.Settings.NameType), _
                    DirectCast(nFilterCriteria(1), Boolean))

            ElseIf cachedItemBaseType Is GetType(TaxRateInfoList) Then

                result = TaxRateInfoList.GetList.GetRateList( _
                    DirectCast(nFilterCriteria(0), TaxRateType), _
                    DirectCast(nFilterCriteria(1), Boolean))

            ElseIf CachedItemBaseType Is GetType(LongTermAssetCustomGroupInfoList) Then

                result = LongTermAssetCustomGroupInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), Boolean))

            ElseIf CachedItemBaseType Is GetType(CashAccountInfoList) Then

                result = CashAccountInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), Boolean), _
                    DirectCast(nFilterCriteria(1), Boolean))

            ElseIf CachedItemBaseType Is GetType(ServiceInfoList) Then

                result = ServiceInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), Boolean), _
                    DirectCast(nFilterCriteria(1), Boolean), _
                    DirectCast(nFilterCriteria(2), Boolean), _
                    DirectCast(nFilterCriteria(3), Boolean))

            ElseIf CachedItemBaseType Is GetType(DocumentSerialInfoList) Then

                result = DocumentSerialInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), Boolean), _
                    DirectCast(nFilterCriteria(1), DocumentSerialType))

            ElseIf CachedItemBaseType Is GetType(TemplateJournalEntryInfoList) Then

                result = TemplateJournalEntryInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), Boolean))

            ElseIf CachedItemBaseType Is GetType(WorkTimeClassInfoList) Then

                result = WorkTimeClassInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), Boolean), _
                    DirectCast(nFilterCriteria(1), Boolean), _
                    DirectCast(nFilterCriteria(2), Boolean))

            ElseIf CachedItemBaseType Is GetType(GoodsInfoList) Then

                result = GoodsInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), Boolean), _
                    DirectCast(nFilterCriteria(1), Boolean), _
                    DirectCast(nFilterCriteria(2), Documents.TradedItemType))

            ElseIf CachedItemBaseType Is GetType(WarehouseInfoList) Then

                result = WarehouseInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), Boolean), _
                    DirectCast(nFilterCriteria(1), Boolean))

            ElseIf CachedItemBaseType Is GetType(ProductionCalculationInfoList) Then

                result = ProductionCalculationInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), Boolean), _
                    DirectCast(nFilterCriteria(1), Boolean))

            ElseIf CachedItemBaseType Is GetType(GoodsGroupInfoList) Then

                result = GoodsGroupInfoList.GetCachedFilteredList( _
                    DirectCast(nFilterCriteria(0), Boolean))




            Else
                Throw New InvalidOperationException("Klaida. CacheBindingsManager neaptarnauja kešo tipui '" _
                                                    & CachedItemBaseType.FullName & "'.")
            End If

            Return result

        End Function

        <Obsolete()> _
        Private Function Clone(ByVal obj As Object) As Object

            Using buffer As New IO.MemoryStream()
                Dim formatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()

                formatter.Serialize(buffer, obj)
                buffer.Position = 0
                Dim temp As Object = formatter.Deserialize(buffer)
                Return temp

            End Using

        End Function

    End Class

End Namespace