Imports AccControlsWinForms
Imports AccDataAccessLayer
Imports ApskaitaObjects.Attributes
Imports ApskaitaObjects.HelperLists
Imports Csla.Core
Imports System.Reflection

Namespace CachedInfoLists

    Public Module BindingSourceManager

#Region " Cached Value Object Lists BindingSource Methods "

        Private _BindingSourceList As Dictionary(Of Type, List(Of DataBindings))

        Friend Function GetBindingSource(ByVal dataSourceProvider As IDataSourceProvider, _
            ByVal usedValueObjectIds As Dictionary(Of Type, List(Of String))) As BindingSource

            If dataSourceProvider Is Nothing Then
                Throw New ArgumentNullException("dataSourceProvider")
            End If

            If _BindingSourceList Is Nothing Then
                _BindingSourceList = New Dictionary(Of Type, List(Of DataBindings))
            End If

            Dim result As New DataBindings(dataSourceProvider, usedValueObjectIds)

            If Not _BindingSourceList.ContainsKey(dataSourceProvider.DataSourceBaseType) Then
                _BindingSourceList.Add(dataSourceProvider.DataSourceBaseType, _
                    New List(Of DataBindings))
            End If

            _BindingSourceList(dataSourceProvider.DataSourceBaseType).Add(result)

            AddHandler result.BindingSource.Disposed, AddressOf BindingSource_Disposed

            Return result.BindingSource

        End Function

        Public Sub CachedItemChanged(ByVal e As CacheChangedEventArgs)
            If _BindingSourceList Is Nothing OrElse _
                Not _BindingSourceList.ContainsKey(e.Type) Then Exit Sub
            For Each binding As DataBindings In _BindingSourceList(e.Type)
                binding.UpdateDataSource()
            Next
        End Sub

        Private Sub BindingSource_Disposed(ByVal sender As Object, ByVal e As System.EventArgs)

            If _BindingSourceList Is Nothing Then Exit Sub

            For Each entry As KeyValuePair(Of Type, List(Of DataBindings)) In _BindingSourceList
                For Each binding As DataBindings In entry.Value
                    If binding.BindingSource Is DirectCast(sender, BindingSource) Then

                        RemoveHandler DirectCast(sender, BindingSource).Disposed, _
                            AddressOf BindingSource_Disposed
                        entry.Value.Remove(binding)
                        Exit Sub

                    End If
                Next
            Next

        End Sub

        Private Class DataBindings

            Private _ValueObjectIds As List(Of String) = Nothing
            Private _DataSourceProvider As IDataSourceProvider
            Private _BindingSource As BindingSource


            Public ReadOnly Property ValueObjectIds() As List(Of String)
                Get
                    Return _ValueObjectIds
                End Get
            End Property

            Public ReadOnly Property DataSourceProvider() As IDataSourceProvider
                Get
                    Return _DataSourceProvider
                End Get
            End Property

            Public ReadOnly Property BindingSource() As BindingSource
                Get
                    Return _BindingSource
                End Get
            End Property


            Public Sub New(ByVal dataSourceProvider As IDataSourceProvider, _
                ByVal usedValueObjectIds As Dictionary(Of Type, List(Of String)))

                If dataSourceProvider Is Nothing Then
                    Throw New ArgumentNullException("dataSourceProvider")
                End If

                _DataSourceProvider = dataSourceProvider

                If Not usedValueObjectIds Is Nothing AndAlso _
                    usedValueObjectIds.ContainsKey(_DataSourceProvider.DataSourceBaseType) Then
                    _ValueObjectIds = usedValueObjectIds( _
                        _DataSourceProvider.DataSourceBaseType)
                End If

                Dim dataSource As IList = _DataSourceProvider.GetDataSource( _
                    _ValueObjectIds)

                _BindingSource = New BindingSource()
                _BindingSource.DataSource = dataSource

            End Sub


            Public Sub UpdateDataSource()

                Dim dataSource As IList = _DataSourceProvider.GetDataSource(_ValueObjectIds)

                Dim currentObject As Object = Nothing
                If Not _BindingSource.Current Is Nothing Then
                    Try
                        currentObject = Clone(_BindingSource.Current)
                    Catch ex As Exception
                    End Try
                End If

                _BindingSource.SuspendBinding()

                _BindingSource.DataSource = Nothing
                _BindingSource.DataSource = dataSource

                _BindingSource.ResumeBinding()

                If Not currentObject Is Nothing AndAlso Not _BindingSource.IndexOf(currentObject) < 0 Then
                    _BindingSource.Position = _BindingSource.IndexOf(currentObject)
                Else
                    _BindingSource.Position = -1
                End If

                _BindingSource.ResetBindings(False)

            End Sub

        End Class

        Public Function PrepareCache(ByVal callingForm As Form, ByVal ParamArray baseTypes As Type()) As Boolean

            Try
                Using busy As New StatusBusy()
                    CacheObjectList.GetList(baseTypes)
                End Using
            Catch ex As Exception
                ShowError(ex)
                If Not callingForm Is Nothing Then
                    DisableAllControls(CType(callingForm, Control))
                End If
                Return False
            End Try

            Return True

        End Function

#End Region

#Region " Controls Init (Configuration) Methods "

        Private _InfoListControlDictionary As Dictionary(Of Type, Type)

        Private Sub InitInfoListControlDictionary()

            _InfoListControlDictionary = New Dictionary(Of Type, Type)

            _InfoListControlDictionary.Add(GetType(AccountInfoList), _
                GetType(AccountInfoListControl))
            _InfoListControlDictionary.Add(GetType(AssignableCRItemList), _
                GetType(AssignableCRItemListControl))
            _InfoListControlDictionary.Add(GetType(CashAccountInfoList), _
                GetType(CashAccountInfoListControl))
            _InfoListControlDictionary.Add(GetType(CodeInfoList), _
                GetType(CodeInfoListControl))
            _InfoListControlDictionary.Add(GetType(GoodsGroupInfoList), _
                GetType(GoodsGroupInfoListControl))
            _InfoListControlDictionary.Add(GetType(GoodsInfoList), _
                GetType(GoodsInfoListControl))
            _InfoListControlDictionary.Add(GetType(LongTermAssetCustomGroupInfoList), _
                GetType(LongTermAssetCustomGroupInfoListControl))
            _InfoListControlDictionary.Add(GetType(ActiveReports.LongTermAssetInfoList), _
                GetType(LongTermAssetInfoListControl))
            _InfoListControlDictionary.Add(GetType(NameInfoList), _
                GetType(NameInfoListControl))
            _InfoListControlDictionary.Add(GetType(PersonGroupInfoList), _
                GetType(PersonGroupInfoListControl))
            _InfoListControlDictionary.Add(GetType(PersonInfoList), _
                GetType(PersonInfoListControl))
            _InfoListControlDictionary.Add(GetType(ProductionCalculationInfoList), _
                GetType(ProductionCalculationInfoListControl))
            _InfoListControlDictionary.Add(GetType(ServiceInfoList), _
                GetType(ServiceInfoListControl))
            _InfoListControlDictionary.Add(GetType(ShortLabourContractList), _
                GetType(ShortLabourContractListControl))
            _InfoListControlDictionary.Add(GetType(TaxRateInfoList), _
                GetType(TaxRateInfoListControl))
            _InfoListControlDictionary.Add(GetType(TemplateJournalEntryInfoList), _
                GetType(TemplateJournalEntryInfoListControl))
            _InfoListControlDictionary.Add(GetType(UserReportInfoList), _
                GetType(UserReportInfoListControl))
            _InfoListControlDictionary.Add(GetType(VatDeclarationSchemaInfoList), _
                GetType(VatDeclarationSchemaInfoListControl))
            _InfoListControlDictionary.Add(GetType(WarehouseInfoList), _
                GetType(WarehouseInfoListControl))
            _InfoListControlDictionary.Add(GetType(WorkTimeClassInfoList), _
                GetType(WorkTimeClassInfoListControl))
            _InfoListControlDictionary.Add(GetType(Security.LocalUserList), _
                GetType(LocalUserListControl))

        End Sub


        ''' <summary>
        ''' Adds a datasource to a ComboBox control and configures it's
        ''' binding properties (ValueMember, DisplayMember).
        ''' </summary>
        ''' <param name="cntr">a ComboBox control to prepare</param>
        ''' <param name="dataSourceProvider">an IDataSourceProvider
        ''' instance that provides a datasource for the ComboBox control</param>
        ''' <param name="usedValueObjectIds">a dictionary of the value objects
        ''' ids' by value object type that are used in an edited business object
        ''' (in order to include obsolete value objects that are used in
        ''' an old business object)</param>
        ''' <remarks></remarks>
        Public Sub PrepareControl(ByVal cntr As ComboBox, _
            ByVal dataSourceProvider As IDataSourceProvider, _
            Optional ByVal usedValueObjectIds As Dictionary(Of Type, List(Of String)) = Nothing)

            If cntr Is Nothing Then
                Throw New ArgumentNullException("cntr")
            ElseIf dataSourceProvider Is Nothing Then
                Throw New ArgumentNullException("dataSourceProvider")
            End If

            cntr.ValueMember = dataSourceProvider.DataSourceValueMember
            cntr.DisplayMember = dataSourceProvider.DataSourceValueMember

            cntr.DataSource = GetBindingSource(dataSourceProvider, usedValueObjectIds)

            AddHandler cntr.Disposed, AddressOf ComboBox_Disposed

            If TypeOf dataSourceProvider Is DocumentSerialFieldAttribute Then
                AddHandler cntr.KeyDown, AddressOf OnAddDocumentSerial
            ElseIf TypeOf dataSourceProvider Is LanguageCodeFieldAttribute OrElse _
                TypeOf dataSourceProvider Is LanguageNameFieldAttribute Then
                AddHandler cntr.KeyDown, AddressOf OnAddRegion
            ElseIf TypeOf dataSourceProvider Is NameFieldAttribute OrElse _
                TypeOf dataSourceProvider Is TaxRateFieldAttribute Then
                AddHandler cntr.KeyDown, AddressOf OnAddCommonSettings
            End If

        End Sub

        Private Sub OnAddDocumentSerial(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
            If e.Control AndAlso (e.KeyCode = Keys.Insert OrElse e.KeyCode = Keys.Add) Then
                OpenNewForm(Of ApskaitaObjects.Settings.DocumentSerialList)()
                e.Handled = True
            End If
        End Sub

        Private Sub OnAddRegion(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
            If e.Control AndAlso (e.KeyCode = Keys.Insert OrElse e.KeyCode = Keys.Add) Then
                OpenNewForm(Of General.CompanyRegionalData)()
                e.Handled = True
            End If
        End Sub

        Private Sub OnAddCommonSettings(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
            If e.Control AndAlso (e.KeyCode = Keys.Insert OrElse e.KeyCode = Keys.Add) Then
                OpenNewForm(Of ApskaitaObjects.Settings.CommonSettings)()
                e.Handled = True
            End If
        End Sub

        ''' <summary>
        ''' Adds all of the enumeration values to a ComboBox control Items collection.
        ''' </summary>
        ''' <param name="cntr">a ComboBox control to prepare</param>
        ''' <param name="enumType">a type of the enumeration to add the values
        ''' to the ComboBox control</param>
        ''' <remarks></remarks>
        Public Sub PrepareControl(ByVal cntr As ComboBox, ByVal enumType As Type)

            If cntr Is Nothing Then
                Throw New ArgumentNullException("cntr")
            ElseIf enumType Is Nothing Then
                Throw New ArgumentNullException("enumType")
            ElseIf Not enumType.IsEnum Then
                Throw New ArgumentException(String.Format("PrepareControl method cannot handle non Enum type {0}.", _
                    enumType.FullName), "enumType")
            End If

            For Each value As [Enum] In [Enum].GetValues(enumType)
                cntr.Items.Add(value)
            Next

        End Sub

        ''' <summary>
        ''' Adds provided enumeration values to a ComboBox control Items collection.
        ''' </summary>
        ''' <param name="cntr">a ComboBox control to prepare</param>
        ''' <param name="enumValues">enumaretion values to add 
        ''' to the ComboBox control</param>
        ''' <remarks></remarks>
        Public Sub PrepareControl(ByVal cntr As ComboBox, _
            ByVal ParamArray enumValues() As [Enum])

            If cntr Is Nothing Then
                Throw New ArgumentNullException("cntr")
            ElseIf enumValues Is Nothing OrElse enumValues.Length < 1 Then
                Throw New ArgumentNullException("enumValues")
            End If

            For Each value As [Enum] In enumValues
                cntr.Items.Add(value)
            Next

        End Sub

        ''' <summary>
        ''' Adds a datasource to a ComboBox control and configures it's
        ''' binding properties (ValueMember, DisplayMember).
        ''' </summary>
        ''' <param name="cntr">a ComboBox control to prepare</param>
        ''' <param name="propInfo">a property of a business object that has
        ''' an attribute of type IDataSourceProvider or is of Enum type</param>
        ''' <param name="usedValueObjectIds">a dictionary of the value objects
        ''' ids' by value object type that are used in an edited business object
        ''' (in order to include obsolete value objects that are used in
        ''' an old business object)</param>
        ''' <remarks></remarks>
        Public Sub PrepareControl(ByVal cntr As ComboBox, ByVal propInfo As PropertyInfo, _
            Optional ByVal usedValueObjectIds As Dictionary(Of Type, List(Of String)) = Nothing)

            Dim dataSourceProvider As IDataSourceProvider = _
                GetDataSourceProvider(propInfo)

            If dataSourceProvider Is Nothing Then

                If propInfo.PropertyType Is GetType([Enum]) Then
                    PrepareControl(cntr, propInfo.PropertyType)
                End If

                Exit Sub

            End If

            PrepareControl(cntr, dataSourceProvider, usedValueObjectIds)

        End Sub

        ''' <summary>
        ''' Adds a datasource to an AccListComboBox control and configures it's
        ''' binding properties (ValueMember, DataListView, EmptyValueString).
        ''' </summary>
        ''' <param name="cntr">an AccListComboBox control to prepare</param>
        ''' <param name="dataSourceProvider">an IDataSourceProvider
        ''' instance that provides a datasource for the AccListComboBox control</param>
        ''' <param name="usedValueObjectIds">a dictionary of the value objects
        ''' ids' by value object type that are used in an edited business object
        ''' (in order to include obsolete value objects that are used in
        ''' an old business object)</param>
        ''' <remarks></remarks>
        Public Sub PrepareControl(ByVal cntr As AccListComboBox, _
            ByVal dataSourceProvider As IDataSourceProvider, _
            Optional ByVal usedValueObjectIds As Dictionary(Of Type, List(Of String)) = Nothing)

            If cntr Is Nothing Then
                Throw New ArgumentNullException("cntr")
            ElseIf dataSourceProvider Is Nothing Then
                Throw New ArgumentNullException("dataSourceProvider")
            End If

            If cntr.HasAttachedInfoList Then Exit Sub

            If _InfoListControlDictionary Is Nothing Then
                InitInfoListControlDictionary()
            End If

            If Not _InfoListControlDictionary.ContainsKey(dataSourceProvider.DataSourceBaseType) Then
                Throw New ArgumentException(String.Format( _
                    "InfoListControl subtype for datasource type {0} is unknown.", _
                    dataSourceProvider.DataSourceBaseType.FullName))
            End If

            Dim dataSource As IList = GetBindingSource(dataSourceProvider, usedValueObjectIds)

            Dim result As InfoListControl = DirectCast(Activator.CreateInstance( _
                _InfoListControlDictionary(dataSourceProvider.DataSourceBaseType)),  _
                InfoListControl)

            result.ValueMember = dataSourceProvider.DataSourceValueMember
            result.DataSource = dataSource
            result.AcceptSingleClick = True

            cntr.AddDataListView(result)
            cntr.EmptyValueString = dataSourceProvider.DataSourceEmptyValueString

            AddHandler cntr.Disposed, AddressOf AccListComboBox_Disposed

        End Sub

        ''' <summary>
        ''' Adds a datasource to an AccListComboBox control and configures it's
        ''' binding properties (ValueMember, DataListView, EmptyValueString).
        ''' </summary>
        ''' <param name="cntr">an AccListComboBox control to prepare</param>
        ''' <param name="propInfo">a property of a business object that has
        ''' an attribute of type IDataSourceProvider</param>
        ''' <param name="usedValueObjectIds">a dictionary of the value objects
        ''' ids' by value object type that are used in an edited business object
        ''' (in order to include obsolete value objects that are used in
        ''' an old business object)</param>
        ''' <remarks></remarks>
        Public Sub PrepareControl(ByVal cntr As AccListComboBox, _
            ByVal propInfo As PropertyInfo, Optional ByVal usedValueObjectIds _
            As Dictionary(Of Type, List(Of String)) = Nothing)

            Dim dataSourceProvider As IDataSourceProvider = _
                GetDataSourceProvider(propInfo)

            If dataSourceProvider Is Nothing Then Exit Sub

            PrepareControl(cntr, dataSourceProvider, usedValueObjectIds)

        End Sub

        ''' <summary>
        ''' Adds a provided datasource to an AccListComboBox control 
        ''' and configures it's binding properties (ValueMember, DataListView, _
        ''' EmptyValueString). Method is used when the datasource is not cached.
        ''' </summary>
        ''' <param name="cntr">an AccListComboBox control to prepare</param>
        ''' <param name="dataSource">a datasource for the AccListComboBox control</param>
        ''' <param name="baseType">a base datasource type when an intermediate
        ''' list is used (FilteredList(of T))</param>
        ''' <remarks></remarks>
        Public Sub PrepareControlWithAdHocDataSource(Of T)(ByVal cntr As AccListComboBox, _
            ByVal dataSource As T, Optional ByVal baseType As Type = Nothing)

            If cntr Is Nothing Then
                Throw New ArgumentNullException("cntr")
            ElseIf dataSource Is Nothing Then
                Throw New ArgumentNullException("dataSource")
            End If

            If cntr.HasAttachedInfoList Then Exit Sub

            If _InfoListControlDictionary Is Nothing Then
                InitInfoListControlDictionary()
            End If

            Dim dataSourceType As Type = GetType(T)
            If Not baseType Is Nothing Then
                dataSourceType = baseType
            End If

            If Not _InfoListControlDictionary.ContainsKey(dataSourceType) Then
                Throw New ArgumentException(String.Format( _
                    "InfoListControl subtype for datasource type {0} is unknown.", _
                    dataSourceType.FullName))
            End If

            Dim result As InfoListControl = DirectCast(Activator.CreateInstance( _
                _InfoListControlDictionary(dataSourceType)), InfoListControl)

            result.DataSource = dataSource
            result.AcceptSingleClick = True

            cntr.AddDataListView(result)

            AddHandler cntr.Disposed, AddressOf AccListComboBox_Disposed

        End Sub

        ''' <summary>
        ''' Configures visual properties of a TextBox control (ReadOnly, 
        ''' MaxLength, TextAlign) using binded property info.
        ''' </summary>
        ''' <param name="cntr">a TextBox control to prepare</param>
        ''' <param name="propInfo">a property of a business object that is
        ''' binded to the TextBox control</param>
        ''' <remarks></remarks>
        Public Sub PrepareControl(ByVal cntr As TextBox, ByVal propInfo As PropertyInfo)

            cntr.ReadOnly = Not propInfo.CanWrite

            If Not GetAttribute(Of StringFieldAttribute)(propInfo) Is Nothing Then

                Dim attribute As StringFieldAttribute = GetAttribute(Of StringFieldAttribute)(propInfo)

                cntr.MaxLength = attribute.MaxLength

            ElseIf propInfo.PropertyType Is GetType(Integer) OrElse _
                propInfo.PropertyType Is GetType(Long) OrElse _
                propInfo.PropertyType Is GetType(Byte) OrElse _
                propInfo.PropertyType Is GetType(Date) Then

                cntr.TextAlign = HorizontalAlignment.Center

            End If

        End Sub

        ''' <summary>
        ''' Configures visual properties of an AccTextBox control (ReadOnly, 
        ''' TextAlign, KeepBackColorWhenReadOnly, NegativeValue, DecimalLength) 
        ''' using binded property info.
        ''' </summary>
        ''' <param name="cntr">an AccTextBox control to prepare</param>
        ''' <param name="propInfo">a property of a business object that is
        ''' binded to the AccTextBox control</param>
        ''' <remarks></remarks>
        Public Sub PrepareControl(ByVal cntr As AccTextBox, ByVal propInfo As PropertyInfo)

            cntr.ReadOnly = Not propInfo.CanWrite
            cntr.TextAlign = HorizontalAlignment.Center
            cntr.KeepBackColorWhenReadOnly = False

            If Not GetAttribute(Of DoubleFieldAttribute)(propInfo) Is Nothing Then

                Dim attribute As DoubleFieldAttribute = GetAttribute(Of DoubleFieldAttribute)(propInfo)

                cntr.NegativeValue = attribute.AllowNegative
                cntr.DecimalLength = attribute.Round

            ElseIf Not GetAttribute(Of IntegerFieldAttribute)(propInfo) Is Nothing Then

                Dim attribute As IntegerFieldAttribute = GetAttribute(Of IntegerFieldAttribute)(propInfo)

                cntr.NegativeValue = attribute.AllowNegative
                cntr.DecimalLength = 0

            End If

        End Sub

        ''' <summary>
        ''' Configures visual properties of a NumericUpDown control (ReadOnly, 
        ''' Increment, DecimalPlaces, TextAlign, Maximum, Minimum) using 
        ''' binded property info.
        ''' </summary>
        ''' <param name="cntr">a NumericUpDown control to prepare</param>
        ''' <param name="propInfo">a property of a business object that is
        ''' binded to the NumericUpDown control</param>
        ''' <remarks></remarks>
        Public Sub PrepareControl(ByVal cntr As NumericUpDown, ByVal propInfo As PropertyInfo)

            If propInfo.CanWrite Then
                cntr.ReadOnly = False
                cntr.Increment = 1
            Else
                cntr.ReadOnly = True
                cntr.Increment = 0
            End If

            cntr.DecimalPlaces = 0
            cntr.TextAlign = HorizontalAlignment.Center

            If Not GetAttribute(Of IntegerFieldAttribute)(propInfo) Is Nothing Then

                Dim attribute As IntegerFieldAttribute = GetAttribute(Of IntegerFieldAttribute)(propInfo)

                If attribute.WithinRange Then
                    cntr.Maximum = attribute.MaxValue
                    cntr.Minimum = attribute.MinValue
                ElseIf attribute.AllowNegative Then
                    cntr.Maximum = 1000000
                    cntr.Minimum = -1000000
                Else
                    cntr.Maximum = 1000000
                    cntr.Minimum = 0
                End If

            Else

                cntr.Maximum = 1000000
                cntr.Minimum = -1000000

            End If

        End Sub


        Private Sub ComboBox_Disposed(ByVal sender As Object, ByVal e As EventArgs)

            If sender Is Nothing Then Exit Sub

            If Not TypeOf sender Is ComboBox Then
                Throw New ArgumentException(String.Format( _
                    "ComboBox_Disposed method cannot handle sender type {0}.", _
                    sender.GetType.FullName))
            End If

            Dim control As ComboBox = DirectCast(sender, ComboBox)

            Try
                RemoveHandler control.KeyDown, AddressOf OnAddDocumentSerial
            Catch ex As Exception
            End Try

            Try
                RemoveHandler control.KeyDown, AddressOf OnAddRegion
            Catch ex As Exception
            End Try

            Try
                RemoveHandler control.KeyDown, AddressOf OnAddCommonSettings
            Catch ex As Exception
            End Try

            Try
                RemoveHandler control.Disposed, AddressOf ComboBox_Disposed
            Catch ex As Exception
            End Try

            If Not control.DataSource Is Nothing _
               AndAlso TypeOf control.DataSource Is BindingSource Then
                Try
                    DirectCast(control.DataSource, BindingSource).Dispose()
                Catch ex As Exception
                End Try
            End If

        End Sub

        Private Sub AccListComboBox_Disposed(ByVal sender As Object, ByVal e As EventArgs)

            If sender Is Nothing Then Exit Sub

            If Not TypeOf sender Is AccListComboBox Then
                Throw New ArgumentException(String.Format( _
                    "AccListComboBox_Disposed method cannot handle sender type {0}.", _
                    sender.GetType.FullName))
            End If

            Dim control As AccListComboBox = DirectCast(sender, AccListComboBox)

            Try
                RemoveHandler control.Disposed, AddressOf AccListComboBox_Disposed
            Catch ex As Exception
            End Try

            If Not control.InfoListControlDataSource Is Nothing _
               AndAlso TypeOf control.InfoListControlDataSource Is BindingSource Then
                Try
                    DirectCast(control.InfoListControlDataSource, BindingSource).Dispose()
                Catch ex As Exception
                End Try
            End If

        End Sub

        ''' <summary>
        ''' a helper method to get an IDataSourceProvider attribute of a property
        ''' </summary>
        ''' <param name="propInfo">a PropertyInfo to look the attribute in</param>
        ''' <remarks></remarks>
        Friend Function GetDataSourceProvider(ByVal propInfo As PropertyInfo) As IDataSourceProvider

            If propInfo Is Nothing Then Return Nothing

            For Each attr As Attribute In Attribute.GetCustomAttributes(propInfo)
                If GetType(IDataSourceProvider).IsAssignableFrom(attr.GetType) Then
                    Return CType(attr, IDataSourceProvider)
                End If
            Next

            Return Nothing

        End Function

#End Region

#Region " Setup Default Controls For BindingSource "

        ''' <summary>
        ''' Initializes default control bindings and formats:
        ''' - sets AccTextBox, NumericUpDown, TextBox formats;
        ''' - loads AccountInfoList, CashAccountInfoList, PersonGroupInfoList
        ''' and WarehouseInfoList to AccListBox controls that are bound to the
        ''' properties of appropriate types (marked with AccountFieldAttribute 
        ''' in case of AccountInfoList);
        ''' - sets controls readonly/enabled status depending on whether the property is readonly.
        ''' </summary>
        ''' <typeparam name="T">a type of the object that the controls are bound to</typeparam>
        ''' <param name="targetForm">a form that contains the controls</param>
        ''' <param name="datasource">a datasource (BindingSource) that the controls are bound to</param>
        ''' <param name="businessObject">an old business object that is about 
        ''' to being bound (contains potentialy obsolete value objects)</param>
        ''' <remarks></remarks>
        Public Sub SetupDefaultControls(Of T)(ByVal targetForm As Control, _
            ByVal datasource As Object, ByVal businessObject As Object)

            Dim usedValueObjectIds As Dictionary(Of Type, List(Of String)) = Nothing
            If Not businessObject Is Nothing Then
                usedValueObjectIds = GetContainedValueObjectLists(businessObject)
            End If

            SetupDefaultControls(Of T)(targetForm, datasource, usedValueObjectIds)

        End Sub

        Private Sub SetupDefaultControls(Of T)(ByVal targetForm As Control, _
            ByVal datasource As Object, ByVal usedValueObjectIds As Dictionary(Of Type, List(Of String)))
            If Not targetForm.Controls Is Nothing Then
                For Each ctrl As Control In targetForm.Controls
                    SetupDefaultControl(Of T)(ctrl, datasource, usedValueObjectIds)
                    If ctrl.Controls.Count > 0 Then
                        SetupDefaultControls(Of T)(ctrl, datasource, usedValueObjectIds)
                    End If
                Next
            End If
        End Sub

        Private Sub SetupDefaultControl(Of T)(ByVal ctrl As Control, _
            ByVal datasource As Object, ByVal usedValueObjectIds As Dictionary(Of Type, List(Of String)))

            Dim propInfo As PropertyInfo = GetBindingProperty(Of T)(ctrl, datasource)

            If propInfo Is Nothing Then Exit Sub

            If TypeOf ctrl Is AccTextBox Then

                PrepareControl(DirectCast(ctrl, AccTextBox), propInfo)

            ElseIf TypeOf ctrl Is TextBox Then

                PrepareControl(DirectCast(ctrl, TextBox), propInfo)

            ElseIf TypeOf ctrl Is NumericUpDown Then

                PrepareControl(DirectCast(ctrl, NumericUpDown), propInfo)

            ElseIf TypeOf ctrl Is AccListComboBox Then

                PrepareControl(DirectCast(ctrl, AccListComboBox), propInfo, _
                    usedValueObjectIds)

            ElseIf TypeOf ctrl Is ComboBox Then

                PrepareControl(DirectCast(ctrl, ComboBox), propInfo, _
                    usedValueObjectIds)

            Else

                Try
                    DirectCast(ctrl, Object).IsReadOnly = Not propInfo.CanWrite
                Catch ex As Exception
                    ctrl.Enabled = propInfo.CanWrite
                End Try

            End If

        End Sub

        Private Function GetBindingProperty(Of T)(ByVal ctrl As Control, ByVal datasource As Object) As PropertyInfo

            For Each binding As Binding In ctrl.DataBindings

                If binding.DataSource Is datasource Then

                    If (TypeOf ctrl Is TextBox AndAlso binding.PropertyName.ToLower = "text") _
                        OrElse (TypeOf ctrl Is AccTextBox AndAlso binding.PropertyName.ToLower = "decimalvalue") _
                        OrElse (TypeOf ctrl Is AccListComboBox AndAlso binding.PropertyName.ToLower = "selectedvalue") _
                        OrElse (TypeOf ctrl Is DateTimePicker AndAlso binding.PropertyName.ToLower = "value") _
                        OrElse (TypeOf ctrl Is NumericUpDown AndAlso binding.PropertyName.ToLower = "value") _
                        OrElse (TypeOf ctrl Is ComboBox AndAlso binding.PropertyName.ToLower = "text") Then

                        Return GetPropertyInfo(Of T)(binding.BindingMemberInfo.BindingMember)

                    End If

                End If

            Next

            Return Nothing

        End Function

#End Region

#Region " Value Objects (obsolete) Ids' collection methods "

        ''' <summary>
        ''' Gets a dictionary of value objects' id's (by value object type)
        ''' that are contained within a business object. (for use in value objects'
        ''' filters in order to always include used value objects in selection lists)
        ''' </summary>
        ''' <param name="businessObject">a business object that (potentialy)
        ''' contains value objects</param>
        ''' <remarks></remarks>
        Public Function GetContainedValueObjectLists(ByVal businessObject As Object) As Dictionary(Of Type, List(Of String))

            Dim result As New Dictionary(Of Type, List(Of String))

            If businessObject Is Nothing Then Return result

            If IsBusinessBaseList(businessObject) Then

                For Each obj As BusinessBase In DirectCast(businessObject, IList)
                    MergeIdDictionary(result, GetContainedValueObjectLists(obj))
                Next

            Else

                For Each prop As PropertyInfo In businessObject.GetType.GetProperties( _
                    BindingFlags.Instance Or BindingFlags.Public)

                    Dim idProvider As IValueObjectIdProvider = GetIValueObjectProvider(prop)

                    If idProvider Is Nothing Then

                        Dim value As Object = Nothing
                        Try
                            value = prop.GetValue(businessObject, Nothing)
                        Catch ex As Exception
                        End Try

                        If Not value Is Nothing Then

                            If GetType(BusinessBase).IsAssignableFrom(value.GetType) Then
                                MergeIdDictionary(result, GetContainedValueObjectLists(value))
                            ElseIf IsBusinessBaseList(value) Then
                                For Each obj As BusinessBase In DirectCast(prop.GetValue(businessObject, Nothing), IList)
                                    MergeIdDictionary(result, GetContainedValueObjectLists(obj))
                                Next
                            End If

                        End If

                    ElseIf prop.CanWrite Then

                        Dim id As String = idProvider.GetValueObjectId(businessObject, prop)

                        If Not StringIsNullOrEmpty(id) Then

                            If Not result.ContainsKey(idProvider.GetValueObjectType()) Then
                                result.Add(idProvider.GetValueObjectType(), New List(Of String))
                            End If

                            If Not result(idProvider.GetValueObjectType()).Contains(id) Then
                                result(idProvider.GetValueObjectType()).Add(id)
                            End If

                        End If

                    End If

                Next

            End If

            Return result

        End Function

        Private Sub MergeIdDictionary(ByVal target As Dictionary(Of Type, List(Of String)), _
            ByVal source As Dictionary(Of Type, List(Of String)))

            For Each pair As KeyValuePair(Of Type, List(Of String)) In source

                If Not target.ContainsKey(pair.Key) Then

                    target.Add(pair.Key, pair.Value)

                Else

                    For Each id As String In pair.Value
                        If Not target(pair.Key).Contains(id) Then
                            target(pair.Key).Add(id)
                        End If
                    Next

                End If

            Next

        End Sub

        Private Function IsBusinessBaseList(ByVal obj As Object) As Boolean

            If obj Is Nothing OrElse Not GetType(IList).IsAssignableFrom(obj.GetType) _
               OrElse DirectCast(obj, IList).Count < 1 OrElse Not _
                GetType(BusinessBase).IsAssignableFrom(DirectCast(obj, IList)(0).GetType) Then
                Return False
            End If

            Return True

        End Function

        Private Function GetIValueObjectProvider(ByVal prop As PropertyInfo) As IValueObjectIdProvider

            For Each attr As Attribute In prop.GetCustomAttributes(True)
                If GetType(IValueObjectIdProvider).IsAssignableFrom(attr.GetType) Then
                    Return DirectCast(attr, IValueObjectIdProvider)
                End If
            Next

            Return Nothing

        End Function

#End Region

    End Module

End Namespace