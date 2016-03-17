
Imports System.Windows.Forms
Imports AccControlsWinForms
Imports ApskaitaObjects
Imports ApskaitaObjects.Attributes
Imports BrightIdeasSoftware
Imports System.Reflection
Imports AccCommon
Imports ApskaitaObjects.HelperLists
Imports AccDataBindingsWinForms.CachedInfoLists

''' <summary>
''' Extender control providing automation around
''' data binding to CSLA .NET editable business objects.
''' </summary>
''' <remarks>An edit form should be loaded in the following sequence:
''' - Invoke PrepareCache for the cached value object lists required;
''' - Invoke SetupDefaultControls to configure default form controls;
''' - Set DataListViewEditControlManager's for the editable DataListView's;
''' - Initialize custom controls for the form and DataListViewEditControlManager's,
''' e.g. for PersonInfoList;
''' - Initialize CslaActionExtenderEditForm;
''' - Invoke AddNewDataSourceButton for a new object button (if exists);
''' - Configure controls that are dependant on object state;
''' - Handle DataSourceStateHasChanged event on CslaActionExtenderEditForm
''' to configure controls that are dependant on object state every time 
''' the state changes.</remarks>
Public Class CslaActionExtenderEditForm(Of T)

    ''' <summary>
    ''' Event is raised after the the datasource object is saved or canceled.
    ''' </summary>
    ''' <remarks>use the event to configure controls that are dependant on object state</remarks>
    Public Event DataSourceStateHasChanged As EventHandler

    Public Delegate Function CustomValidation() As Boolean

    Private _DataSource As T = Nothing
    Private _WarnIfCloseOnDirty As Boolean = True
    Private _DirtyWarningMessage As String = "Išsaugoti duomenis?"
    Private _WarnOnCancel As Boolean = False
    Private _WarnOnCancelMessage As String = "Ar tikrai norite atšaukti pataisymus ir grįžti prie pirminių duomenų?"
    Private _ParentForm As Form = Nothing
    Private _CachedListsTypes As Type()
    Private _BindingSourceTree As BindingSourceNode = Nothing
    Private _OkButton As Button = Nothing
    Private _ApplyButton As Button = Nothing
    Private _CancelButton As Button = Nothing
    Private _NewButton As Button = Nothing
    Private _LimitationsButton As Button = Nothing
    Private _ProgressControl As ProgressFiller = Nothing
    Private _IsLoading As Boolean = True
    Private _CloseFormAfterSave As Boolean = False
    Private _ManagedStateDataListViews As DataListView() = Nothing
    Private _NewDataSourceMethodName As String = ""
    Private _NewDataSourceMethodParams As Object() = Nothing
    Private _ProceedToNewDataSource As Boolean = False
    Private _NewDataSource As T = Nothing
    Private _FetchingNewDataSource As Boolean = False
    Private _CustomValidation As CustomValidation = Nothing


    ''' <summary>
    ''' Gets or sets a reference to the data source object.
    ''' </summary>
    Public Property DataSource() As T
        Get
            Return _DataSource
        End Get
        Set(ByVal value As T)
            _DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether to warn the user on close 
    ''' when the object is dirty.
    ''' </summary>
    Public Property WarnIfCloseOnDirty() As Boolean
        Get
            Return _WarnIfCloseOnDirty
        End Get
        Set(ByVal value As Boolean)
            _WarnIfCloseOnDirty = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the message shown to the user in a close on dirty warning.
    ''' </summary>
    Public Property DirtyWarningMessage() As String
        Get
            Return _DirtyWarningMessage
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _DirtyWarningMessage = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether to warn the user on cancel.
    ''' </summary>
    Public Property WarnOnCancel() As Boolean
        Get
            Return _WarnOnCancel
        End Get
        Set(ByVal value As Boolean)
            _WarnOnCancel = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the message shown to the user in a warn on cancel.
    ''' </summary>
    Public Property WarnOnCancelMessage() As String
        Get
            Return _WarnOnCancelMessage
        End Get
        Set(ByVal value As String)
            _WarnOnCancelMessage = value
        End Set
    End Property

    ''' <summary>
    ''' Gets whether the current business object is a child object of some other document.
    ''' </summary>
    ''' <remarks>Tries to invoke <see cref="Csla.BusinessBase(Of T).IsChild">non public IsChild method</see>.</remarks>
    Public ReadOnly Property IsChild() As Boolean
        Get
            Return DataSourceIsChild()
        End Get
    End Property


    ''' <summary>
    ''' Creates a new CslaActionExtenderEditForm instance.
    ''' </summary>
    ''' <param name="parentForm">a parent form that is managed by the instance</param>
    ''' <param name="parentBindingSource">a root binding source for the business object</param>
    ''' <param name="dataSource">a business object to edit</param>
    ''' <param name="cachedListsTypes">types of the cached value object lists
    ''' that are required by the business object</param>
    ''' <param name="okButton">an Ok button</param>
    ''' <param name="applyButton">an apply button (if any)</param>
    ''' <param name="cancelButton">a cancel button (if any)</param>
    ''' <param name="limitationsButton">a button that displays chronologic limitations (if any)</param>
    ''' <param name="progressControl">a progress control for saving the business object async</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal parentForm As Form, ByVal parentBindingSource As BindingSource, _
        ByVal dataSource As T, ByVal cachedListsTypes As Type(), _
        ByVal okButton As Button, ByVal applyButton As Button, ByVal cancelButton As Button, _
        ByVal limitationsButton As Button, ByVal progressControl As ProgressFiller)

        If parentForm Is Nothing Then
            Throw New ArgumentNullException("parentForm")
        ElseIf parentBindingSource Is Nothing Then
            Throw New ArgumentNullException("parentBindingSource")
        ElseIf okButton Is Nothing Then
            Throw New ArgumentNullException("okButton")
        ElseIf progressControl Is Nothing Then
            Throw New ArgumentNullException("progressControl")
        End If

        _BindingSourceTree = BindingSourceNode.GetBindingSourceTree(parentForm, parentBindingSource)
        _ParentForm = parentForm
        _CachedListsTypes = cachedListsTypes
        _OkButton = okButton
        _ApplyButton = applyButton
        _CancelButton = cancelButton
        _ProgressControl = progressControl
        _LimitationsButton = limitationsButton
        _DataSource = dataSource

        AddHandler parentForm.Activated, AddressOf Form_Activated
        AddHandler parentForm.FormClosing, AddressOf Form_FormClosing

        AddHandler okButton.Click, AddressOf OkButton_Click
        If Not cancelButton Is Nothing Then
            AddHandler cancelButton.Click, AddressOf CancelButton_Click
        End If
        If Not applyButton Is Nothing Then
            AddHandler applyButton.Click, AddressOf ApplyButton_Click
        End If
        If Not limitationsButton Is Nothing Then
            AddHandler limitationsButton.Click, AddressOf LimitationsButton_Click
        End If
        AddHandler progressControl.AsyncOperationCompleted, AddressOf AsyncOperationCompleted

        Try
            If Not dataSource Is Nothing Then DirectCast(dataSource, Object).BeginEdit()
        Catch ex As Exception
        End Try

        parentBindingSource.DataSource = dataSource

        MyCustomSettings.SetFormLayout(parentForm)

        If Not _LimitationsButton Is Nothing Then
            _LimitationsButton.Visible = Not String.IsNullOrEmpty(GetLimitationsString().Trim)
        End If

    End Sub

    Private Sub New()
        ' do not allow creation of an empty instance
    End Sub


    ''' <summary>
    ''' Sets DataListView's which visual state should be restored/saved
    ''' using <see cref="MyCustomSettings">MyCustomSettings</see>.
    ''' </summary>
    ''' <param name="listViews">DataListView's which visual state should 
    ''' be restored/saved using <see cref="MyCustomSettings">MyCustomSettings</see></param>
    ''' <remarks></remarks>
    Public Sub ManageDataListViewStates(ByVal ParamArray listViews As DataListView())

        _ManagedStateDataListViews = listViews

        If Not listViews Is Nothing AndAlso listViews.Length > 0 Then
            For Each listView As DataListView In listViews
                MyCustomSettings.SetListViewLayOut(listView)
            Next
        End If

    End Sub

    ''' <summary>
    ''' Adds a new object/operation button to handled buttons.
    ''' </summary>
    ''' <param name="newButton">a new object/operation button</param>
    ''' <param name="newDataSourceMethodName">a name of the static factory method
    ''' that creates a new object/operation</param>
    ''' <param name="newDataSourceMethodParams">params for the static factory method (if any)</param>
    ''' <remarks></remarks>
    Public Sub AddNewDataSourceButton(ByVal newButton As Button, _
        ByVal newDataSourceMethodName As String, _
        ByVal ParamArray newDataSourceMethodParams As Object())

        If newButton Is Nothing Then
            Throw New ArgumentNullException("newButton")
        ElseIf newDataSourceMethodName Is Nothing OrElse _
            String.IsNullOrEmpty(newDataSourceMethodName.Trim) Then
            Throw New ArgumentNullException("newDataSourceMethodName")
        End If

        _NewButton = newButton
        _NewDataSourceMethodName = newDataSourceMethodName
        _NewDataSourceMethodParams = newDataSourceMethodParams

        AddHandler newButton.Click, AddressOf NewButton_Click

    End Sub

    ''' <summary>
    ''' Sets a new datasource for the form after asking the user if he wants to 
    ''' save the current datasource.
    ''' </summary>
    ''' <param name="newDataSource">a new datasource to set</param>
    ''' <remarks></remarks>
    Public Sub AddNewDataSource(ByVal newDataSource As T)

        If DataSourceIsChild() Then Exit Sub

        If Not _DataSource Is Nothing AndAlso TypeOf _DataSource Is IIsDirtyEnough _
            AndAlso DirectCast(_DataSource, IIsDirtyEnough).IsDirtyEnough Then

            Dim answ As String = Ask(_DirtyWarningMessage, New ButtonStructure("Taip"), _
                New ButtonStructure("Ne"), New ButtonStructure("Atšaukti"))

            If answ <> "Taip" AndAlso answ <> "Ne" Then Exit Sub

            If answ = "Taip" Then

                _NewDataSource = newDataSource
                Save(False)

                Exit Sub

            End If

        End If

        _BindingSourceTree.Apply()

        _DataSource = newDataSource

        _BindingSourceTree.Bind(_DataSource)

        If Not _LimitationsButton Is Nothing Then
            _LimitationsButton.Visible = Not String.IsNullOrEmpty(GetLimitationsString().Trim)
        End If

        RaiseEvent DataSourceStateHasChanged(Me, New EventArgs)

    End Sub

    ''' <summary>
    ''' Adds a custom validation method that is invoked before the datasource is saved.
    ''' </summary>
    ''' <param name="customValidationMethod"></param>
    ''' <remarks></remarks>
    Public Sub AddCustomValidation(ByVal customValidationMethod As CustomValidation)
        _CustomValidation = customValidationMethod
    End Sub


    Private Sub Form_Activated(ByVal sender As Object, ByVal e As System.EventArgs)

        If _ParentForm.WindowState = FormWindowState.Maximized AndAlso MyCustomSettings.AutoSizeForm Then
            _ParentForm.WindowState = FormWindowState.Normal
        End If

        If _IsLoading Then
            _IsLoading = False
            Exit Sub
        End If

        If Not _CachedListsTypes Is Nothing AndAlso _CachedListsTypes.Length > 0 Then
            CachedInfoLists.PrepareCache(_ParentForm, _CachedListsTypes)
        End If

    End Sub

    Private Sub Form_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)

        If Not _CloseFormAfterSave AndAlso Not _DataSource Is Nothing _
            AndAlso TypeOf _DataSource Is IIsDirtyEnough AndAlso _
            DirectCast(_DataSource, IIsDirtyEnough).IsDirtyEnough Then

            Dim answ As String = Ask(_DirtyWarningMessage, New ButtonStructure("Taip"), _
                New ButtonStructure("Ne"), New ButtonStructure("Atšaukti"))

            If answ <> "Taip" AndAlso answ <> "Ne" Then
                e.Cancel = True
                Exit Sub
            End If

            If answ = "Taip" Then
                Save(True)
                e.Cancel = True
                Exit Sub
            End If

        End If

        If Not _ManagedStateDataListViews Is Nothing AndAlso _ManagedStateDataListViews.Length > 0 Then
            For Each listView As DataListView In _ManagedStateDataListViews
                MyCustomSettings.GetListViewLayOut(listView)
            Next
        End If
        MyCustomSettings.GetFormLayout(_ParentForm)

        If Not DataSourceIsChild() Then
            _BindingSourceTree.Close()
        End If

        Try
            RemoveHandler _ParentForm.FormClosing, AddressOf Form_FormClosing
        Catch ex As Exception
        End Try
        Try
            RemoveHandler _ParentForm.Activated, AddressOf Form_Activated
        Catch ex As Exception
        End Try
        Try
            RemoveHandler _OkButton.Click, AddressOf OkButton_Click
        Catch ex As Exception
        End Try
        Try
            If Not _CancelButton Is Nothing Then
                RemoveHandler _CancelButton.Click, AddressOf CancelButton_Click
            End If
        Catch ex As Exception
        End Try
        Try
            If Not _ApplyButton Is Nothing Then
                RemoveHandler _ApplyButton.Click, AddressOf ApplyButton_Click
            End If
        Catch ex As Exception
        End Try
        Try
            If Not _LimitationsButton Is Nothing Then
                RemoveHandler _LimitationsButton.Click, AddressOf LimitationsButton_Click
            End If
        Catch ex As Exception

        End Try
        Try
            RemoveHandler _ProgressControl.AsyncOperationCompleted, AddressOf AsyncOperationCompleted
        Catch ex As Exception
        End Try
        Try
            If Not _NewButton Is Nothing Then
                RemoveHandler _NewButton.Click, AddressOf NewButton_Click
            End If
        Catch ex As Exception

        End Try

        _ManagedStateDataListViews = Nothing
        _DataSource = Nothing
        _ParentForm = Nothing
        _CachedListsTypes = Nothing
        _BindingSourceTree = Nothing
        _OkButton = Nothing
        _ApplyButton = Nothing
        _CancelButton = Nothing
        _ProgressControl = Nothing
        _LimitationsButton = Nothing

    End Sub


    Private Sub OkButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Save(True)
    End Sub

    Private Sub ApplyButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If _DataSource Is Nothing OrElse DataSourceIsChild() Then Exit Sub
        Save(False)
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Cancel()
    End Sub

    Private Sub NewButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If DataSourceIsChild() Then Exit Sub

        If Not _DataSource Is Nothing AndAlso TypeOf _DataSource Is IIsDirtyEnough _
            AndAlso DirectCast(_DataSource, IIsDirtyEnough).IsDirtyEnough Then

            Dim answ As String = Ask(_DirtyWarningMessage, New ButtonStructure("Taip"), _
                New ButtonStructure("Ne"), New ButtonStructure("Atšaukti"))

            If answ <> "Taip" AndAlso answ <> "Ne" Then Exit Sub

            If answ = "Taip" Then

                _ProceedToNewDataSource = True
                Save(False)

                Exit Sub

            End If

        End If

        FetchNewDataSource()

    End Sub

    Private Sub LimitationsButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim result As String = GetLimitationsString()

        If String.IsNullOrEmpty(result.Trim) Then
            MsgBox("Nėra jokių taikytinų chronologinių apribojimų.")
        Else
            MsgBox(String.Format("Taikomi chronologiniai apribojimai:{0}{1}", _
                vbCrLf, result), MsgBoxStyle.Information, "Info")
        End If

    End Sub


    Private Sub AsyncOperationCompleted(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim success As Boolean = False

        If Not _ProgressControl.Exception Is Nothing Then

            ShowError(_ProgressControl.Exception)

        ElseIf Not _ProgressControl.Result Is Nothing Then

            If _FetchingNewDataSource Then

                BindNewDataSource(DirectCast(_ProgressControl.Result, T))

            ElseIf Not _NewDataSource Is Nothing Then

                _DataSource = _NewDataSource
                _NewDataSource = Nothing

            Else

                Try
                    _DataSource = DirectCast(_ProgressControl.Result, T)
                    success = True
                Catch ex As Exception
                    ShowError(ex)
                End Try

            End If


        Else

            MsgBox("ProgressControl negrąžino nei klaidos, nei rezultato.", _
                MsgBoxStyle.Exclamation, "Klaida")

        End If

        If Not _LimitationsButton Is Nothing Then
            _LimitationsButton.Visible = Not String.IsNullOrEmpty(GetLimitationsString().Trim)
        End If

        If _FetchingNewDataSource Then
            _FetchingNewDataSource = False
            RaiseEvent DataSourceStateHasChanged(Me, New EventArgs)
            Exit Sub
        End If

        _BindingSourceTree.Bind(_DataSource)

        If success Then

            MsgBox("Duomenys sėkmingai išsaugoti.", _
                MsgBoxStyle.Information, "Info")

            If _CloseFormAfterSave Then
                _ParentForm.Hide()
                _ParentForm.Close()
                Exit Sub
            End If

            RaiseEvent DataSourceStateHasChanged(Me, New EventArgs)

            If _ProceedToNewDataSource Then
                _ProceedToNewDataSource = False
                FetchNewDataSource()
            End If

        Else

            _ProceedToNewDataSource = False

        End If

    End Sub


    Private Sub Save(ByVal closeFormAfterSave As Boolean)

        If _DataSource Is Nothing Then

            If closeFormAfterSave Then
                _CloseFormAfterSave = True
                _ParentForm.Hide()
                _ParentForm.Close()
            ElseIf _ProceedToNewDataSource Then
                _ProceedToNewDataSource = False
                FetchNewDataSource()
            End If

            Exit Sub

        End If

        If Not DirectCast(_DataSource, Object).IsDirty AndAlso Not DataSourceIsChild() Then

            If closeFormAfterSave Then
                _CloseFormAfterSave = True
                _ParentForm.Hide()
                _ParentForm.Close()
            ElseIf _ProceedToNewDataSource Then
                _ProceedToNewDataSource = False
                FetchNewDataSource()
            End If

            Exit Sub

        End If

        If Not _CustomValidation Is Nothing Then
            If Not _CustomValidation.Invoke() Then Exit Sub
        End If

        Dim validationProvider As IValidationMessageProvider = Nothing
        Try
            validationProvider = DirectCast(_DataSource, IValidationMessageProvider)
        Catch ex As Exception
        End Try

        If Not validationProvider Is Nothing Then

            If Not validationProvider.IsValid Then
                MsgBox(String.Format("Formoje yra klaidų:{0}{1}", vbCrLf, _
                    validationProvider.GetAllBrokenRules), MsgBoxStyle.Exclamation, "Klaida")
                _ProceedToNewDataSource = False
                Exit Sub
            End If

            Dim question As String = ""
            If validationProvider.HasWarnings() Then
                question = String.Format("DĖMESIO. Duomenyse gali būti klaidų:{0}{1}{2}", _
                    vbCrLf, validationProvider.GetAllWarnings, vbCrLf)
            Else
                question = ""
            End If
            Try
                If DirectCast(_DataSource, Object).IsNew Then
                    question = question & "Ar tikrai norite įtraukti naujus duomenis?"
                Else
                    question = question & "Ar tikrai norite pakeisti duomenis?"
                End If
            Catch ex As Exception
                question = question & "Ar tikrai norite pakeisti duomenis?"
            End Try

            If Not YesOrNo(question) Then
                _ProceedToNewDataSource = False
                _NewDataSource = Nothing
                Exit Sub
            End If

        End If

        _BindingSourceTree.Apply()

        If DataSourceIsChild() Then

            _CloseFormAfterSave = True
            _ParentForm.Hide()
            _ParentForm.Close()

        End If

        If MyCustomSettings.UseThreadingForDataTransfer Then

            _CloseFormAfterSave = closeFormAfterSave
            _ProgressControl.RunOperationAsync(Of T)(_DataSource, "Save", False)
            Exit Sub

        End If

        Try
            Using busy As New StatusBusy()
                _DataSource = DirectCast(DirectCast(_DataSource, Object).Save, T)
            End Using
        Catch ex As Exception
            _BindingSourceTree.Bind(_DataSource)
            ShowError(ex)
            _ProceedToNewDataSource = False
            Exit Sub
        End Try

        If Not _NewDataSource Is Nothing Then
            _DataSource = _NewDataSource
            _NewDataSource = Nothing
        End If

        _BindingSourceTree.Bind(_DataSource)

        MsgBox("Duomenys sėkmingai išsaugoti.", MsgBoxStyle.Information, "Info")

        If closeFormAfterSave Then

            _ProceedToNewDataSource = False
            _CloseFormAfterSave = True
            _ParentForm.Hide()
            _ParentForm.Close()

        Else

            If Not _LimitationsButton Is Nothing Then
                _LimitationsButton.Visible = Not String.IsNullOrEmpty(GetLimitationsString().Trim)
            End If

            RaiseEvent DataSourceStateHasChanged(Me, New EventArgs)

            If _ProceedToNewDataSource Then
                _ProceedToNewDataSource = False
                FetchNewDataSource()
            End If

        End If

    End Sub

    Private Sub Cancel()

        If _DataSource Is Nothing Then Exit Sub

        If DataSourceIsChild() Then

            _BindingSourceTree.CancelChild()

            _CloseFormAfterSave = True

            _ParentForm.Hide()
            _ParentForm.Close()

        Else

            If _WarnOnCancel AndAlso Not YesOrNo(_WarnOnCancelMessage) Then Exit Sub

            _BindingSourceTree.Cancel(_DataSource)

            RaiseEvent DataSourceStateHasChanged(Me, New EventArgs)

        End If

    End Sub

    Private Sub FetchNewDataSource()

        If MyCustomSettings.UseThreadingForDataTransfer Then

            _FetchingNewDataSource = True
            If _NewDataSourceMethodParams Is Nothing Then
                _ProgressControl.RunOperationAsync(Of T)(Nothing, _NewDataSourceMethodName, False)
            Else
                _ProgressControl.RunOperationAsync(Of T)(Nothing, _NewDataSourceMethodName, _
                    False, _NewDataSourceMethodParams)
            End If
            
        Else

            Dim newDataSource As T = Nothing

            Try
                Using busy As New StatusBusy()
                    newDataSource = DirectCast(InvokeMethod(Of T) _
                        (Nothing, _NewDataSourceMethodName, _NewDataSourceMethodParams), T)
                End Using
            Catch ex As Exception
                ShowError(ex)
                Exit Sub
            End Try

            BindNewDataSource(newDataSource)

        End If

    End Sub

    Private Sub BindNewDataSource(ByVal newDataSource As T)

        _BindingSourceTree.CancelChild()

        _DataSource = newDataSource

        _BindingSourceTree.Bind(_DataSource)

    End Sub


    Private Function DataSourceIsChild() As Boolean

        If _DataSource Is Nothing Then Return False

        Dim result As Boolean = False

        Try
            result = DirectCast(_DataSource, Object).IsChild
        Catch ex As Exception
            Try
                result = DirectCast(GetType(T).GetProperty("IsChild", _
                    BindingFlags.Instance Or BindingFlags.NonPublic). _
                    GetValue(_DataSource, Nothing), Boolean)
            Catch e As Exception
            End Try
        End Try

        Return result

    End Function

    Private Function GetLimitationsString() As String

        If _DataSource Is Nothing OrElse _LimitationsButton Is Nothing Then Return ""

        Dim result As String = ""

        For Each propInfo As PropertyInfo In GetType(T).GetProperties

            If propInfo.PropertyType Is GetType(IChronologicValidator) _
                OrElse Not Array.IndexOf(propInfo.PropertyType.GetInterfaces, _
                GetType(IChronologicValidator)) < 0 Then

                Try
                    result = AddWithNewLine(result, DirectCast(propInfo.GetValue( _
                        _DataSource, Nothing), IChronologicValidator).LimitsExplanation, False)
                Catch ex As Exception
                End Try

            End If

        Next

        Return result

    End Function

End Class
