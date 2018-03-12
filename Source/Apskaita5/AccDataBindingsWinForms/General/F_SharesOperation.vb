Imports AccControlsWinForms
Imports AccDataBindingsWinForms.Printing
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.General

Public Class F_SharesOperation
    Implements ISupportsPrinting, IObjectEditForm

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.PersonInfoList), GetType(HelperLists.SharesClassInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of SharesOperation)
    Private _ListViewManagerAcquisitions As DataListViewEditControlManager(Of SharesAcquisition)
    Private _ListViewManagerDiscards As DataListViewEditControlManager(Of SharesDiscard)

    Private _OperationToEdit As SharesOperation


    Public Sub New(ByVal operationToEdit As SharesOperation)
        InitializeComponent()
        _OperationToEdit = operationToEdit
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub


    Public ReadOnly Property ObjectID() As Integer Implements IObjectEditForm.ObjectID
        Get
            If _FormManager Is Nothing OrElse _FormManager.DataSource Is Nothing Then
                If _OperationToEdit Is Nothing OrElse _OperationToEdit.IsNew Then
                    Return Integer.MinValue
                Else
                    Return _OperationToEdit.ID
                End If
            ElseIf _FormManager.DataSource.IsNew Then
                Return Integer.MinValue
            End If
            Return _FormManager.DataSource.ID
        End Get
    End Property

    Public ReadOnly Property ObjectType() As System.Type Implements IObjectEditForm.ObjectType
        Get
            Return GetType(SharesOperation)
        End Get
    End Property


    Private Sub F_SharesOperation_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If _OperationToEdit Is Nothing Then
            _OperationToEdit = SharesOperation.NewSharesOperation
        End If

        If Not SetDataSources() Then Exit Sub

        Try

            _FormManager = New CslaActionExtenderEditForm(Of SharesOperation) _
                (Me, SharesOperationBindingSource, _OperationToEdit,
                 _RequiredCachedLists, nOkButton, ApplyButton, nCancelButton,
                 Nothing, ProgressFiller1)

            _FormManager.ManageDataListViewStates(Me.AcquisitionsDataListView, Me.DiscardsDataListView)

        Catch ex As Exception
            ShowError(New Exception("Klaida. Nepavyko gauti akcijų operacijos duomenų.", ex))
            DisableAllControls(Me)
            Exit Sub
        End Try

        If Not _OperationToEdit.IsNew AndAlso Not SharesOperation.CanEditObject Then

            DisableAllControls(Me)
            MsgBox("Klaida. Jūsų teisių nepakanka informacijai redaguoti.",
                MsgBoxStyle.Exclamation, "Klaida")

        Else
            ConfigureButtons()
        End If

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _ListViewManagerAcquisitions = New DataListViewEditControlManager(Of SharesAcquisition) _
                (AcquisitionsDataListView, Nothing, AddressOf OnItemsDeleteAcquisition,
                 AddressOf OnItemAddAcquisition, Nothing, _OperationToEdit)

            _ListViewManagerDiscards = New DataListViewEditControlManager(Of SharesDiscard) _
                (DiscardsDataListView, Nothing, AddressOf OnItemsDeleteDiscard,
                 AddressOf OnItemAddDiscard, Nothing, _OperationToEdit)

            SetupDefaultControls(Of SharesOperation)(Me,
                SharesOperationBindingSource, _OperationToEdit)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Sub OnItemsDeleteAcquisition(ByVal items As SharesAcquisition())
        If items Is Nothing OrElse items.Length < 1 OrElse _FormManager.DataSource Is Nothing Then Exit Sub
        For Each item As SharesAcquisition In items
            _FormManager.DataSource.Acquisitions.Remove(item)
        Next
    End Sub

    Private Sub OnItemsDeleteDiscard(ByVal items As SharesDiscard())
        If items Is Nothing OrElse items.Length < 1 OrElse _FormManager.DataSource Is Nothing Then Exit Sub
        For Each item As SharesDiscard In items
            _FormManager.DataSource.Discards.Remove(item)
        Next
    End Sub

    Private Sub OnItemAddAcquisition()
        If _FormManager.DataSource Is Nothing Then Exit Sub
        _FormManager.DataSource.Acquisitions.AddNew()
    End Sub

    Private Sub OnItemAddDiscard()
        If _FormManager.DataSource Is Nothing Then Exit Sub
        _FormManager.DataSource.Discards.AddNew()
    End Sub


    Public Function GetMailDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetMailDropDownItems
        Return Nothing
    End Function

    Public Function GetPrintDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintDropDownItems
        Return Nothing
    End Function

    Public Function GetPrintPreviewDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintPreviewDropDownItems
        Return Nothing
    End Function

    Public Sub OnMailClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnMailClick
        If _FormManager.DataSource Is Nothing Then Exit Sub

        Using frm As New F_SendObjToEmail(_FormManager.DataSource, 0)
            frm.ShowDialog()
        End Using

    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, False, 0, "AkcijuOperacija", Me, "")
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "AkcijuOperacija", Me, "")
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function


    Private Sub ConfigureButtons() Handles _FormManager.DataSourceStateHasChanged

        If _FormManager.DataSource Is Nothing Then Exit Sub

        nCancelButton.Enabled = Not _FormManager.DataSource.IsNew

    End Sub

End Class