﻿Imports ApskaitaObjects.Workers
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccDataBindingsWinForms.Printing

Friend Class F_ContractUpdate
    Implements ISupportsPrinting, IObjectEditForm, ISupportsChronologicValidator

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of ContractUpdate)
    Private _ContractUpdateToEdit As ContractUpdate = Nothing


    Public ReadOnly Property ObjectID() As Integer Implements IObjectEditForm.ObjectID
        Get
            If _FormManager Is Nothing OrElse _FormManager.DataSource Is Nothing Then
                If _ContractUpdateToEdit Is Nothing OrElse _ContractUpdateToEdit.IsNew Then
                    Return Integer.MinValue
                Else
                    Return _ContractUpdateToEdit.ID
                End If
            ElseIf _FormManager.DataSource.IsNew Then
                Return Integer.MinValue
            End If
            Return _FormManager.DataSource.ID
        End Get
    End Property

    Public ReadOnly Property ObjectType() As System.Type Implements IObjectEditForm.ObjectType
        Get
            Return GetType(ContractUpdate)
        End Get
    End Property


    Public Sub New(ByVal contractUpdateToEdit As ContractUpdate)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ContractUpdateToEdit = contractUpdateToEdit

    End Sub


    Private Sub F_LabourContractUpdate_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If _ContractUpdateToEdit Is Nothing Then
            MsgBox("Klaida. Nenurodytas darbo sutarties pakeitimas.", MsgBoxStyle.Exclamation, "Klaida")
            DisableAllControls(Me)
            Exit Sub
        End If

        If Not SetDataSources() Then Exit Sub

        Try
            _FormManager = New CslaActionExtenderEditForm(Of ContractUpdate)(Me, _
                ContractUpdateBindingSource, _ContractUpdateToEdit, Nothing, _
                IOkButton, IApplyButton, ICancelButton, Nothing, ProgressFiller1)
        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

        ConfigureButtons()

    End Sub

    Private Function SetDataSources() As Boolean

        Try

            SetupDefaultControls(Of ContractUpdate)(Me, _
                ContractUpdateBindingSource, _ContractUpdateToEdit)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Sub PositionChangedCheckBox_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles PositionChangedCheckBox.CheckedChanged, _
        WorkLoadChangedCheckBox.CheckedChanged, WageChangedCheckBox.CheckedChanged, _
        ExtraPayChangedCheckBox.CheckedChanged, NpdChangedCheckBox.CheckedChanged, _
        PnpdChangedCheckBox.CheckedChanged, AnnualHolidayChangedCheckBox.CheckedChanged, _
        HolidayCorrectionChangedCheckBox.CheckedChanged

        PositionTextBox.ReadOnly = Not PositionChangedCheckBox.Checked
        WorkLoadAccTextBox.ReadOnly = Not WorkLoadChangedCheckBox.Checked
        WageAccTextBox.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange OrElse Not WageChangedCheckBox.Checked
        HumanReadableWageTypeComboBox.Enabled = _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange AndAlso WageChangedCheckBox.Checked
        ExtraPayAccTextBox.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange OrElse Not ExtraPayChangedCheckBox.Checked
        NPDAccTextBox.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange OrElse Not NpdChangedCheckBox.Checked
        PNPDAccTextBox.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange OrElse Not PnpdChangedCheckBox.Checked
        AnnualHolidayAccTextBox.ReadOnly = _FormManager.DataSource.HolidayCompensationPayed OrElse Not AnnualHolidayChangedCheckBox.Checked
        HolidayCorrectionAccTextBox.ReadOnly = _FormManager.DataSource.HolidayCompensationPayed OrElse Not HolidayCorrectionChangedCheckBox.Checked

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
            PrintObject(_FormManager.DataSource, False, 0, "", Me, "")
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "", Me, "")
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function


    Public Function ChronologicContent() As String _
            Implements ISupportsChronologicValidator.ChronologicContent
        If _FormManager.DataSource Is Nothing Then Return ""
        Return _FormManager.DataSource.ChronologicValidator.LimitsExplanation
    End Function

    Public Function HasChronologicContent() As Boolean _
        Implements ISupportsChronologicValidator.HasChronologicContent

        Return Not _FormManager.DataSource Is Nothing AndAlso _
            Not StringIsNullOrEmpty(_FormManager.DataSource.ChronologicValidator.LimitsExplanation)

    End Function


    Private Sub _FormManager_DataSourceStateHasChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles _FormManager.DataSourceStateHasChanged
        ConfigureButtons()
    End Sub

    Private Sub ConfigureButtons()

        If _FormManager.DataSource Is Nothing Then Exit Sub

        WageChangedCheckBox.Enabled = _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        ExtraPayChangedCheckBox.Enabled = _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        NpdChangedCheckBox.Enabled = _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        PnpdChangedCheckBox.Enabled = _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange

        PositionTextBox.ReadOnly = Not PositionChangedCheckBox.Checked
        WorkLoadAccTextBox.ReadOnly = Not WorkLoadChangedCheckBox.Checked
        WageAccTextBox.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange OrElse Not WageChangedCheckBox.Checked
        HumanReadableWageTypeComboBox.Enabled = _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange AndAlso WageChangedCheckBox.Checked
        ExtraPayAccTextBox.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange OrElse Not ExtraPayChangedCheckBox.Checked
        NPDAccTextBox.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange OrElse Not NpdChangedCheckBox.Checked
        PNPDAccTextBox.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange OrElse Not PnpdChangedCheckBox.Checked
        AnnualHolidayAccTextBox.ReadOnly = Not AnnualHolidayChangedCheckBox.Checked
        HolidayCorrectionAccTextBox.ReadOnly = Not HolidayCorrectionChangedCheckBox.Checked

    End Sub

End Class