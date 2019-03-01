Imports ApskaitaObjects.Documents
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists

Friend Class F_VatDeclarationSchema
    Implements IObjectEditForm

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.TaxRateInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of VatDeclarationSchema)
    Private _ListViewManager As DataListViewEditControlManager(Of VatDeclarationEntry)

    Private _DocumentToEdit As VatDeclarationSchema = Nothing

    Public ReadOnly Property ObjectID() As Integer Implements IObjectEditForm.ObjectID
        Get
            If _FormManager Is Nothing OrElse _FormManager.DataSource Is Nothing Then
                If _DocumentToEdit Is Nothing OrElse _DocumentToEdit.IsNew Then
                    Return Integer.MinValue
                Else
                    Return _DocumentToEdit.ID
                End If
            End If
            Return _FormManager.DataSource.ID
        End Get
    End Property

    Public ReadOnly Property ObjectType() As System.Type Implements IObjectEditForm.ObjectType
        Get
            Return GetType(VatDeclarationSchema)
        End Get
    End Property


    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal documentToEdit As VatDeclarationSchema)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _DocumentToEdit = documentToEdit

    End Sub


    Private Sub F_VatDeclarationSchema_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If _DocumentToEdit Is Nothing Then
            _DocumentToEdit = VatDeclarationSchema.NewVatDeclarationSchema()
        End If

        If Not SetDataSources() Then Exit Sub

        Try

            _FormManager = New CslaActionExtenderEditForm(Of VatDeclarationSchema) _
                (Me, VatDeclarationSchemaBindingSource, _DocumentToEdit, _
                _RequiredCachedLists, IOkButton, IApplyButton, ICancelButton, _
                Nothing, ProgressFiller1)

            _FormManager.ManageDataListViewStates(DeclarationEntriesDataListView)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of VatDeclarationEntry) _
                (DeclarationEntriesDataListView, Nothing, AddressOf OnItemsDelete, _
                 AddressOf OnItemAdd, Nothing, _DocumentToEdit)

            SetupDefaultControls(Of VatDeclarationSchema)(Me, _
                VatDeclarationSchemaBindingSource, _DocumentToEdit)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Sub OnItemsDelete(ByVal items As VatDeclarationEntry())
        If items Is Nothing OrElse items.Length < 1 OrElse _FormManager.DataSource Is Nothing Then Exit Sub
        For Each item As VatDeclarationEntry In items
            _FormManager.DataSource.DeclarationEntries.Remove(item)
        Next
    End Sub

    Private Sub OnItemAdd()
        If _FormManager.DataSource Is Nothing Then Exit Sub
        _FormManager.DataSource.DeclarationEntries.AddNew()
    End Sub

End Class