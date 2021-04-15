Imports ApskaitaObjects.Documents.InvoiceAdapters
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.Attributes
Imports ApskaitaObjects.Documents

Friend Class F_NewInvoiceAdapterForServiceOperation

    Private _ForInvoiceReceived As Boolean = False
    Private _ParentChronologyValidator As IChronologicValidator = Nothing
    Private _QueryBrowser As CslaActionExtenderQueryObject
    Private _Value As ServiceInvoiceAdapter = Nothing


    Public ReadOnly Property Value() As ServiceInvoiceAdapter
        Get
            Return _Value
        End Get
    End Property


    Public Sub New(ByVal forInvoiceReceived As Boolean, ByVal parentChronologyValidator As IChronologicValidator)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _ForInvoiceReceived = forInvoiceReceived
        _ParentChronologyValidator = parentChronologyValidator

    End Sub

    Private Sub InvoiceAdapterForServiceOperation_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not PrepareCache(Me, GetType(HelperLists.ServiceInfoList)) Then Exit Sub

        Try

            PrepareControl(ServicesAccGridComboBox, New ServiceFieldAttribute( _
                ValueRequiredLevel.Optional, TradedItemType.All))

            _QueryBrowser = New CslaActionExtenderQueryObject(Me, ProgressFiller1)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
        End Try

    End Sub


    Private Sub IOkButton_Click(ByVal sender As System.Object, _
       ByVal e As System.EventArgs) Handles IOkButton.Click

        Dim selectedService As HelperLists.ServiceInfo = Nothing
        Try
            selectedService = DirectCast(ServicesAccGridComboBox.SelectedValue, HelperLists.ServiceInfo)
        Catch ex As Exception
        End Try
        If selectedService Is Nothing OrElse selectedService.IsEmpty Then
            MsgBox("Klaida. Nepasirinkta paslauga.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Try

            'ServiceInvoiceAdapter.NewServiceInvoiceAdapter(selectedService, _ParentChronologyValidator, _
            '    Not _ForInvoiceReceived)
            _QueryBrowser.InvokeQuery(Of ServiceInvoiceAdapter)(Nothing, "NewServiceInvoiceAdapter", _
                True, AddressOf OnValueFetched, selectedService, _ParentChronologyValidator, _
                Not _ForInvoiceReceived)

        Catch ex As Exception
            ShowError(ex, Nothing)
        End Try

    End Sub

    Private Sub OnValueFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        _Value = DirectCast(result, ServiceInvoiceAdapter)

        Me.Close()

    End Sub

    Private Sub ICancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ICancelButton.Click

        Me.Close()

    End Sub

End Class
