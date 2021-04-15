Imports System.IO
Imports ApskaitaObjects.Documents.BankDataExchangeProviders
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccControlsWinForms

Public Class F_ExportedBankPayments

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.PersonInfoList), GetType(HelperLists.CashAccountInfoList)}

    Private _ListViewManager As DataListViewEditControlManager(Of ExportedBankPayment)
    Private _InitialList As ExportedBankPaymentList
    Private _Payments As ExportedBankPayments = ExportedBankPayments.NewExportedBankPayments()


    Public Sub New(ByVal initialList As ExportedBankPaymentList)

        If initialList Is Nothing OrElse initialList.Count < 1 Then Throw new ArgumentNullException("initialList")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _InitialList = initialList

    End Sub


    Private Sub F_ExportedBankPayments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not SetDataSources() Then Exit Sub

        _Payments.Merge(_InitialList)

        ExportedBankPaymentsBindingSource.DataSource = _Payments
    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return false

        Try

            _ListViewManager = New DataListViewEditControlManager(Of ExportedBankPayment) _
                (ItemsDataListView, Nothing, AddressOf OnItemsDelete, _
                 AddressOf OnItemAdd, Nothing, _Payments)

            SetupDefaultControls(Of ExportedBankPayments)(Me, ExportedBankPaymentsBindingSource, _Payments)

            Dim adapters = new List(Of IBankPaymentExporter)({ New Pain_001_001_03BankPaymentExporter() })
            AdaptersComboBox.DataSource = adapters
            AdaptersComboBox.SelectedIndex = 0

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Sub OnItemsDelete(ByVal items As ExportedBankPayment())
        If items Is Nothing OrElse items.Length < 1 Then Exit Sub
        For Each item As ExportedBankPayment In items
            _Payments.Payments.Remove(item)
        Next
    End Sub

    Private Sub OnItemAdd()
        _Payments.Payments.AddNew()
    End Sub


    public sub Merge(list As ExportedBankPaymentList)
        _Payments.Merge(list)
    End sub

    Private Sub ExportButton_Click(sender As Object, e As EventArgs) Handles ExportButton.Click

        Dim adapter As IBankPaymentExporter = TryCast(AdaptersComboBox.SelectedItem, IBankPaymentExporter)

        If adapter Is Nothing Then 
            MsgBox("Nepasirinktas eksporto formatas", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        If Not _Payments.IsValid Then
            MsgBox(String.Format("Formoje yra klaidų:{0}{1}", vbCrLf, _
                _Payments.GetAllBrokenRules), MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        If _Payments.HasWarnings() Then
            If Not YesOrNo(String.Format("DĖMESIO. Duomenyse gali būti klaidų:{0}{1}{2}Ar tikrai norite eksportuoti mokėjimų duomenis?", _
                vbCrLf, _Payments.GetAllWarnings, vbCrLf)) Then Exit Sub
        End If

        Dim fileName As String = ""
        Using sfd As New SaveFileDialog
            sfd.Filter = String.Format("{0}|*.{1}|All Files (*.*)|*.*", _
                adapter.FileFormatDescription, adapter.FileExtension)
            If sfd.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then Exit Sub
            fileName = sfd.FileName
        End Using
        If StringIsNullOrEmpty(fileName) Then Exit Sub

        Using fileStream As Stream = File.Create(fileName)
            Try
                adapter.Export(_Payments, fileStream)
                fileStream.Flush()
            Catch ex As Exception
                ShowError(ex)
                Exit Sub
            End Try
        End Using

        If YesOrNo("Duomenys sėkmingai išsaugoti. Uždaryti formą?") Then
            Me.Hide()
            Me.Close()
        End If

    End Sub

End Class
