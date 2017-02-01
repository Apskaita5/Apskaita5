
Imports System.Windows.Forms
Imports AccControlsWinForms
Imports BrightIdeasSoftware

''' <summary>
''' Extender control providing automation around
''' data binding to CSLA .NET readonly business objects (active reports).
''' </summary>
''' <remarks>An edit form should be loaded in the following sequence:
''' - Invoke PrepareCache for the cached value object lists required;
''' - Invoke SetupDefaultControls to configure default form controls;
''' - Set DataListViewEditControlManager's for the editable DataListView's;
''' - Initialize custom controls for the form and DataListViewEditControlManager's,
''' e.g. for PersonInfoList;
''' - Initialize CslaActionExtenderReportForm;
''' - Configure controls that are dependant on security state.</remarks>
Public Class CslaActionExtenderReportForm(Of T)

    ''' <summary>
    ''' a method that the parent form shall implement in order to provide
    ''' the current request (refresh) parameters
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Delegate Function GetReportParams() As Object()

    ''' <summary>
    ''' a method signature that the parent form shall implement in order 
    ''' to implement some before and after fetch processing
    ''' </summary>
    ''' <remarks></remarks>
    Public Delegate Sub OnProgressChange()


    Private _DataSource As T = Nothing
    Private _ParentForm As Form = Nothing
    Private _CachedListsTypes As Type()
    Private _BindingSourceTree As BindingSourceNode = Nothing
    Private _RefreshButton As Button = Nothing
    Private _ProgressControl As ProgressFiller = Nothing
    Private _IsLoading As Boolean = True
    Private _ManagedStateDataListViews As ObjectListView() = Nothing
    Private _GetReportParamsDelegate As GetReportParams = Nothing
    Private _RefreshMethodName As String = ""
    Private _BeforeFetchHandler As OnProgressChange = Nothing
    Private _AfterFetchHandler As OnProgressChange = Nothing


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
    ''' Gets or sets a name of the method that is used to fets a new report instance.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property RefreshMethodName() As String
        Get
            Return _RefreshMethodName
        End Get
        Set(ByVal value As String)
            If value Is Nothing OrElse String.IsNullOrEmpty(value.Trim) Then
                Throw New ArgumentNullException("value")
            End If
            _RefreshMethodName = value
        End Set
    End Property


    ''' <summary>
    ''' Creates a new CslaActionExtenderReportForm instance.
    ''' </summary>
    ''' <param name="parentForm">a parent form that is managed by the instance</param>
    ''' <param name="parentBindingSource">a root binding source for the report</param>
    ''' <param name="dataSource">a report object instance to start with (if any)</param>
    ''' <param name="cachedListsTypes">types of the cached value object lists
    ''' that are required by the report</param>
    ''' <param name="refreshButton">a refresh button</param>
    ''' <param name="progressControl">a progress control for fetching a report async</param>
    ''' <param name="nRefreshMethodName">a name of the method 
    ''' that is used to fets a new report instance</param>
    ''' <param name="getReportParamsDelegate">a method to provide the current 
    ''' request (refresh) parameters</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal parentForm As Form, ByVal parentBindingSource As BindingSource, _
        ByVal dataSource As T, ByVal cachedListsTypes As Type(), _
        ByVal refreshButton As Button, ByVal progressControl As ProgressFiller, _
        ByVal nRefreshMethodName As String, ByVal getReportParamsDelegate As GetReportParams)

        If parentForm Is Nothing Then
            Throw New ArgumentNullException("parentForm")
        ElseIf parentBindingSource Is Nothing Then
            Throw New ArgumentNullException("parentBindingSource")
        ElseIf refreshButton Is Nothing Then
            Throw New ArgumentNullException("refreshButton")
        ElseIf progressControl Is Nothing Then
            Throw New ArgumentNullException("progressControl")
        ElseIf getReportParamsDelegate Is Nothing Then
            Throw New ArgumentNullException("getReportParamsDelegate")
        ElseIf nRefreshMethodName Is Nothing OrElse String.IsNullOrEmpty(nRefreshMethodName.Trim) Then
            Throw New ArgumentNullException("nRefreshMethodName")
        End If

        _BindingSourceTree = BindingSourceNode.GetBindingSourceTree(parentForm, parentBindingSource)
        _ParentForm = parentForm
        _CachedListsTypes = cachedListsTypes
        _RefreshButton = refreshButton
        _ProgressControl = progressControl
        _GetReportParamsDelegate = getReportParamsDelegate
        _RefreshMethodName = nRefreshMethodName

        AddHandler parentForm.Activated, AddressOf Form_Activated
        AddHandler parentForm.FormClosing, AddressOf Form_FormClosing

        AddHandler refreshButton.Click, AddressOf RefreshButton_Click
        AddHandler progressControl.AsyncOperationCompleted, AddressOf AsyncOperationCompleted

        If Not dataSource Is Nothing Then
            parentBindingSource.DataSource = dataSource
            _DataSource = dataSource
        End If

        MyCustomSettings.SetFormLayout(parentForm)

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
    Public Sub ManageDataListViewStates(ByVal ParamArray listViews As ObjectListView())

        _ManagedStateDataListViews = listViews

        If Not listViews Is Nothing AndAlso listViews.Length > 0 Then
            For Each listView As ObjectListView In listViews
                MyCustomSettings.SetListViewLayOut(listView)
            Next
        End If

    End Sub

    ''' <summary>
    ''' Sets a method that will be invoked before the fetch operation.
    ''' </summary>
    ''' <param name="beforeFetchHandler">a method to invoke before the fetch operation</param>
    ''' <remarks></remarks>
    Public Sub SetBeforeFetchHandler(ByVal beforeFetchHandler As OnProgressChange)
        _BeforeFetchHandler = beforeFetchHandler
    End Sub

    ''' <summary>
    ''' Sets a method that will be invoked after the fetch operation.
    ''' </summary>
    ''' <param name="afterFetchHandler">a method to invoke after the fetch operation</param>
    ''' <remarks></remarks>
    Public Sub SetAfterFetchHandler(ByVal afterFetchHandler As OnProgressChange)
        _AfterFetchHandler = afterFetchHandler
    End Sub


    Private Sub Form_Activated(ByVal sender As Object, ByVal e As System.EventArgs)

        If _ParentForm.WindowState = FormWindowState.Maximized AndAlso (_ParentForm.Modal _
            OrElse MyCustomSettings.AutoSizeForm) Then
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

        If Not _ProgressControl Is Nothing AndAlso Not _ProgressControl.IsDisposed _
            AndAlso _ProgressControl.IsRunning Then
            _ProgressControl.CancelProgress()
        End If

        If Not _ManagedStateDataListViews Is Nothing AndAlso _ManagedStateDataListViews.Length > 0 Then
            For Each listView As ObjectListView In _ManagedStateDataListViews
                MyCustomSettings.GetListViewLayOut(listView)
            Next
        End If
        If Not _ParentForm.Modal Then MyCustomSettings.GetFormLayout(_ParentForm)

        _BindingSourceTree.Close()

        Try
            RemoveHandler _ParentForm.FormClosing, AddressOf Form_FormClosing
        Catch ex As Exception
        End Try
        Try
            RemoveHandler _ParentForm.Activated, AddressOf Form_Activated
        Catch ex As Exception
        End Try
        Try
            RemoveHandler _RefreshButton.Click, AddressOf RefreshButton_Click
        Catch ex As Exception
        End Try
        Try
            RemoveHandler _ProgressControl.AsyncOperationCompleted, AddressOf AsyncOperationCompleted
        Catch ex As Exception
        End Try

        _ManagedStateDataListViews = Nothing
        _DataSource = Nothing
        _ParentForm = Nothing
        _CachedListsTypes = Nothing
        _BindingSourceTree = Nothing
        _RefreshButton = Nothing
        _ProgressControl = Nothing

    End Sub


    Private Sub RefreshButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If Not _BeforeFetchHandler Is Nothing Then _BeforeFetchHandler.Invoke()

        Dim params As Object() = Nothing

        Try
            params = _GetReportParamsDelegate.Invoke()
        Catch ex As Exception
        End Try

        If MyCustomSettings.UseThreadingForDataTransfer Then

            If params Is Nothing OrElse params.Length < 1 Then
                _ProgressControl.RunOperationAsync(Of T)(Nothing, _RefreshMethodName, True)
            Else
                _ProgressControl.RunOperationAsync(Of T)(Nothing, _RefreshMethodName, True, params)
            End If

        Else

            Dim result As T = Nothing

            Try
                Using busy As New StatusBusy()
                    result = DirectCast(InvokeMethod(Of T)(Nothing, _RefreshMethodName, params), T)
                End Using
            Catch ex As Exception
                ShowError(ex)
                Exit Sub
            End Try

            SetNewDataSource(result)

        End If

    End Sub

    Private Sub AsyncOperationCompleted(ByVal sender As Object, ByVal e As System.EventArgs)

        If Not _ProgressControl.Exception Is Nothing Then

            ShowError(_ProgressControl.Exception)

        ElseIf Not _ProgressControl.Result Is Nothing Then

            Try
                SetNewDataSource(DirectCast(_ProgressControl.Result, T))
            Catch ex As Exception
                ShowError(ex)
            End Try

        Else

            MsgBox("ProgressControl negrąžino nei klaidos, nei rezultato.", _
                MsgBoxStyle.Exclamation, "Klaida")

        End If

    End Sub

    Private Sub SetNewDataSource(ByVal result As T)
        If result Is Nothing Then Exit Sub
        _DataSource = result
        _BindingSourceTree.Cancel(result)
        If Not _ManagedStateDataListViews Is Nothing AndAlso _
            _ManagedStateDataListViews.Length > 0 Then
            _ManagedStateDataListViews(0).Focus()
        End If
        If Not _AfterFetchHandler Is Nothing Then _AfterFetchHandler.Invoke()
    End Sub

End Class