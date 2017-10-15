Imports System.Windows.Forms
Imports ApskaitaObjects.HelperLists
Imports System.Drawing
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccControlsWinForms
Imports ApskaitaObjects.Documents
Imports ApskaitaObjects.Attributes

Public Class F_UserReportParamsDialog

    Private ReadOnly _Info As UserReportInfo
    Private ReadOnly _Controls As New List(Of KeyValuePair(Of UserReportParamInfo, Control))
    Private _Params As List(Of KeyValuePair(Of String, Object)) = Nothing


    Public ReadOnly Property Params() As List(Of KeyValuePair(Of String, Object))
        Get
            Return _Params
        End Get
    End Property


    Public Sub New(ByVal info As UserReportInfo)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        If info Is Nothing Then Throw New ArgumentNullException("info")

        If info.Params.Count < 1 Then
            Throw New ArgumentException("Klaida. Pasirinkta ataskaita neturi parametrų.", "info")
        End If

        _Info = info

    End Sub


    Private Sub F_UserReportParamsDialog_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = _Info.ToString()

        Dim requiredCachedLists As Type() = GetRequiredCachedLists()

        If Not requiredCachedLists Is Nothing AndAlso requiredCachedLists.Length > 0 Then
            If Not PrepareCache(Me, requiredCachedLists) Then Exit Sub
        End If

        Dim headerHeight As Integer = Me.Height - Me.TableLayoutPanel2.Margin.Top _
            - Me.TableLayoutPanel2.Margin.Bottom - Me.TableLayoutPanel2.Height _
            - Me.TableLayoutPanel1.Margin.Bottom - Me.TableLayoutPanel1.Height _
            - Me.TableLayoutPanel1.Margin.Top

        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()

        Me.TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.AutoSize))

        Dim height As Integer = 0

        Dim i As Integer = 0
        Dim radioButtons As New List(Of RadioButton)

        For Each param As UserReportParamInfo In _Info.Params

            Dim cntr As Control = GetControl(param)

            If Not cntr Is Nothing Then

                _Controls.Add(New KeyValuePair(Of UserReportParamInfo, Control) _
                    (param, cntr))

                If TypeOf cntr Is RadioButton Then

                    radioButtons.Add(cntr)

                Else

                    Me.TableLayoutPanel2.RowCount = i + 1

                    Dim paramLabel As New Label()
                    paramLabel.Font = New Font(Me.Font, FontStyle.Bold)
                    If param.AllowNull Then
                        paramLabel.Text = param.Prompt
                    Else
                        paramLabel.Text = param.Prompt & "*"
                    End If
                    paramLabel.Padding = New Padding(0, 5, 0, 0)
                    paramLabel.TextAlign = ContentAlignment.TopRight
                    Me.TableLayoutPanel2.Controls.Add(paramLabel, 0, i)
                    paramLabel.Dock = DockStyle.Fill

                    Me.TableLayoutPanel2.Controls.Add(cntr, 1, i)
                    cntr.Dock = DockStyle.Fill

                    height += cntr.GetPreferredSize(New Size(100, 100)).Height _
                        + cntr.Margin.Top + cntr.Margin.Bottom

                End If

            End If

            i += 1

        Next

        If radioButtons.Count > 0 Then

            Dim flowPanel As New FlowLayoutPanel
            flowPanel.AutoSize = True
            flowPanel.FlowDirection = FlowDirection.LeftToRight
            For Each rbutton As RadioButton In radioButtons
                flowPanel.Controls.Add(rbutton)
            Next

            Me.TableLayoutPanel2.RowCount = Me.TableLayoutPanel2.RowCount + 1

            Me.TableLayoutPanel2.Controls.Add(flowPanel, 0, Me.TableLayoutPanel2.RowCount + 1)
            Me.TableLayoutPanel2.SetRowSpan(flowPanel, 2)
            flowPanel.Dock = DockStyle.Fill

            height += flowPanel.GetPreferredSize(New Size(100, 100)).Height

        End If

        Me.TableLayoutPanel2.ResumeLayout(True)
        Me.ResumeLayout(True)

        height += Me.TableLayoutPanel2.Margin.Top + Me.TableLayoutPanel2.Margin.Bottom

        height += Me.TableLayoutPanel1.Size.Height + Me.TableLayoutPanel1.Margin.Top _
            + Me.TableLayoutPanel1.Margin.Bottom

        Me.Height = height + headerHeight

    End Sub


    Private Sub OK_Button_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles OK_Button.Click
        _Params = GetParams()
        If _Params Is Nothing Then Exit Sub
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    Private Function GetRequiredCachedLists() As Type()

        If _Info.Params Is Nothing OrElse _Info.Params.Count < 1 Then Return Nothing

        Dim result As New List(Of Type)

        For Each param As UserReportParamInfo In _Info.Params

            If param.Name.Trim.ToLower.EndsWith("AccountInfo".ToLower) Then

                If Not result.Contains(GetType(AccountInfoList)) Then
                    result.Add(GetType(AccountInfoList))
                End If

            ElseIf param.Name.Trim.ToLower.EndsWith("AssignableCRItem".ToLower) Then

                If Not result.Contains(GetType(AssignableCRItemList)) Then
                    result.Add(GetType(AssignableCRItemList))
                End If

            ElseIf param.Name.Trim.ToLower.EndsWith("CashAccountInfo".ToLower) Then

                If Not result.Contains(GetType(CashAccountInfoList)) Then
                    result.Add(GetType(CashAccountInfoList))
                End If

            ElseIf param.Name.Trim.ToLower.EndsWith("GoodsGroupInfo".ToLower) Then

                If Not result.Contains(GetType(GoodsGroupInfoList)) Then
                    result.Add(GetType(GoodsGroupInfoList))
                End If

            ElseIf param.Name.Trim.ToLower.EndsWith("GoodsInfo".ToLower) Then

                If Not result.Contains(GetType(GoodsInfoList)) Then
                    result.Add(GetType(GoodsInfoList))
                End If

            ElseIf param.Name.Trim.ToLower.EndsWith("LongTermAssetCustomGroupInfo".ToLower) Then

                If Not result.Contains(GetType(LongTermAssetCustomGroupInfoList)) Then
                    result.Add(GetType(LongTermAssetCustomGroupInfoList))
                End If

            ElseIf param.Name.Trim.ToLower.EndsWith("PersonGroupInfo".ToLower) Then

                If Not result.Contains(GetType(PersonGroupInfoList)) Then
                    result.Add(GetType(PersonGroupInfoList))
                End If

            ElseIf param.Name.Trim.ToLower.EndsWith("PersonInfo".ToLower) Then

                If Not result.Contains(GetType(PersonInfoList)) Then
                    result.Add(GetType(PersonInfoList))
                End If

            ElseIf param.Name.Trim.ToLower.EndsWith("ServiceInfo".ToLower) Then

                If Not result.Contains(GetType(ServiceInfoList)) Then
                    result.Add(GetType(ServiceInfoList))
                End If

            ElseIf param.Name.Trim.ToLower.EndsWith("LabourContractSerial".ToLower) Then

                ' TODO: implement labour contracts
                Return Nothing

            ElseIf param.Name.Trim.ToLower.EndsWith("VatDeclarationSchemaInfo".ToLower) Then

                If Not result.Contains(GetType(VatDeclarationSchemaInfoList)) Then
                    result.Add(GetType(VatDeclarationSchemaInfoList))
                End If

            ElseIf param.Name.Trim.ToLower.EndsWith("WarehouseInfo".ToLower) Then

                If Not result.Contains(GetType(WarehouseInfoList)) Then
                    result.Add(GetType(WarehouseInfoList))
                End If

            End If

        Next

        Return result.ToArray()

    End Function

    Private Function GetControl(ByVal param As UserReportParamInfo) As Control

        If param.Name.Trim.ToLower.EndsWith("Date".ToLower) Then

            Dim result As New DateTimePicker
            result.Format = DateTimePickerFormat.Short
            result.Tag = GetType(DateTime)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("Year".ToLower) Then

            Dim result As New ComboBox
            For i As Integer = 1 To 20
                result.Items.Add(Today.Year - i + 1)
            Next
            result.Tag = GetType(Integer)
            result.SelectedItem = Today.AddMonths(-1).Year
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("Month".ToLower) Then

            Dim result As New ComboBox
            For i As Integer = 1 To 12
                result.Items.Add(i)
            Next
            result.Tag = GetType(Integer)
            result.SelectedItem = Today.AddMonths(-1).Month
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("ComboBox".ToLower) Then

            Dim result As New ComboBox
            result.DataSource = param.ParamValues
            result.Tag = GetType(UserReportParamValueInfo)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("CheckBox".ToLower) Then

            Dim result As New CheckBox
            result.Text = ""
            result.AutoSize = True
            result.Tag = GetType(Byte)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("RadioButton".ToLower) Then

            Dim result As New RadioButton
            result.Text = param.Prompt
            result.Font = New Font(Me.Font, FontStyle.Bold)
            result.AutoSize = True
            result.Tag = GetType(Byte)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("Number".ToLower) Then

            Dim result As New AccControlsWinForms.AccTextBox
            result.DecimalLength = 0
            result.NegativeValue = True
            result.Tag = GetType(Integer)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("Decimal".ToLower) Then

            Dim result As New AccTextBox
            result.DecimalLength = 2
            result.NegativeValue = True
            result.Tag = GetType(Double)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("AccountInfo".ToLower) Then

            Dim result As New AccListComboBox
            PrepareControl(result, New AccountFieldAttribute( _
                ValueRequiredLevel.Optional, False))
            result.Tag = GetType(AccountInfo)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("AssignableCRItem".ToLower) Then

            Dim result As New AccListComboBox
            PrepareControl(result, New AssignableCRItemFieldAttribute( _
                ValueRequiredLevel.Optional))
            result.Tag = GetType(AssignableCRItem)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("CashAccountInfo".ToLower) Then

            Dim result As New AccListComboBox
            PrepareControl(result, New CashAccountFieldAttribute( _
                ValueRequiredLevel.Optional, False))
            result.Tag = GetType(CashAccountInfo)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("GoodsGroupInfo".ToLower) Then

            Dim result As New AccListComboBox
            PrepareControl(result, New GoodsGroupFieldAttribute( _
                ValueRequiredLevel.Optional))
            result.Tag = GetType(GoodsGroupInfo)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("GoodsInfo".ToLower) Then

            Dim result As New AccListComboBox
            PrepareControl(result, New GoodsFieldAttribute( _
                ValueRequiredLevel.Optional, TradedItemType.All))
            result.Tag = GetType(GoodsInfo)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("LongTermAssetCustomGroupInfo".ToLower) Then

            Dim result As New AccListComboBox
            PrepareControl(result, New LongTermAssetCustomGroupFieldAttribute( _
                ValueRequiredLevel.Optional))
            result.Tag = GetType(LongTermAssetCustomGroupInfo)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("PersonGroupInfo".ToLower) Then

            Dim result As New AccListComboBox
            PrepareControl(result, New PersonGroupFieldAttribute( _
                ValueRequiredLevel.Optional))
            result.Tag = GetType(PersonGroupInfo)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("PersonInfo".ToLower) Then

            Dim result As New AccListComboBox
            PrepareControl(result, New PersonFieldAttribute( _
                ValueRequiredLevel.Optional, True, True, True))
            result.Tag = GetType(PersonInfo)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("ServiceInfo".ToLower) Then

            Dim result As New AccListComboBox
            PrepareControl(result, New ServiceFieldAttribute( _
                ValueRequiredLevel.Optional))
            result.Tag = GetType(ServiceInfo)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("LabourContractSerial".ToLower) Then

            Dim result As New AccListComboBox
            PrepareControl(result, New ShortLabourContractFieldAttribute( _
                ValueRequiredLevel.Optional))
            result.Tag = GetType(ShortLabourContract)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("LabourContractNumber".ToLower) Then

            ' because it is handled by LabourContractSerial
            Return Nothing

        ElseIf param.Name.Trim.ToLower.EndsWith("VatDeclarationSchemaInfo".ToLower) Then

            Dim result As New AccListComboBox
            PrepareControl(result, New VatDeclarationSchemaFieldAttribute( _
                ValueRequiredLevel.Optional))
            result.Tag = GetType(VatDeclarationSchemaInfo)
            Return result

        ElseIf param.Name.Trim.ToLower.EndsWith("WarehouseInfo".ToLower) Then

            Dim result As New AccListComboBox
            PrepareControl(result, New WarehouseFieldAttribute( _
                ValueRequiredLevel.Optional))
            result.Tag = GetType(WarehouseInfo)
            Return result

        Else

            Dim result As New TextBox
            result.MaxLength = 255
            result.Tag = GetType(String)
            Return result

        End If

    End Function

    Private Function GetParams() As List(Of KeyValuePair(Of String, Object))

        Dim result As New List(Of KeyValuePair(Of String, Object))

        Dim missingValues As String = ""

        For Each param As KeyValuePair(Of UserReportParamInfo, Control) In _Controls
            Try
                Dim value As Object = GetControlValue(param.Value, param.Key)
                result.Add(New KeyValuePair(Of String, Object) _
                    (param.Key.Name, value))
                If TypeOf param.Value Is AccListComboBox AndAlso _
                    DirectCast(param.Value.Tag, Type) Is GetType(ShortLabourContract) Then
                    Try
                        result.Add(New KeyValuePair(Of String, Object) _
                            (param.Key.Name.Replace("LabourContractSerial", _
                            "LabourContractNumber"), DirectCast(DirectCast(param.Value,  _
                             AccListComboBox).SelectedValue, ShortLabourContract).Number))
                    Catch ex As Exception
                        result.Add(New KeyValuePair(Of String, Object) _
                            (param.Key.Name.Replace("LabourContractSerial", _
                            "LabourContractNumber"), 0))
                    End Try
                End If
            Catch ex As Exception
                missingValues = AddWithNewLine(missingValues, ex.Message, False)
            End Try
        Next

        If Not StringIsNullOrEmpty(missingValues) Then
            MsgBox(String.Format("Klaida. Nenurodyti visi reikalingi ataskaitos parametrai:{0}{1}", _
                vbCrLf, missingValues), MsgBoxStyle.Exclamation, "Klaida")
            Return Nothing
        Else
            Return result
        End If

    End Function

    Private Function GetControlValue(ByVal cntr As Control, _
        ByVal param As UserReportParamInfo) As Object

        If TypeOf cntr Is TextBox Then

            If Not param.AllowNull AndAlso StringIsNullOrEmpty(DirectCast(cntr, TextBox).Text) Then
                Throw New Exception(String.Format("Nenurodyta {0}.", param.Prompt))
            End If

            Return DirectCast(cntr, TextBox).Text

        ElseIf TypeOf cntr Is AccTextBox Then

            If DirectCast(cntr.Tag, Type) Is GetType(Integer) Then

                If Not param.AllowNull AndAlso Convert.ToInt32(DirectCast(cntr, AccTextBox).DecimalValue) = 0 Then
                    Throw New Exception(String.Format("Nenurodyta {0}.", param.Prompt))
                End If

                Return Convert.ToInt32(DirectCast(cntr, AccTextBox).DecimalValue)

            Else

                If Not param.AllowNull AndAlso DirectCast(cntr, AccTextBox).DecimalValue = 0.0 Then
                    Throw New Exception(String.Format("Nenurodyta {0}.", param.Prompt))
                End If

                Return DirectCast(cntr, AccTextBox).DecimalValue

            End If

        ElseIf TypeOf cntr Is CheckBox Then

            If DirectCast(cntr, CheckBox).Checked Then
                Return 1
            Else
                Return 0
            End If

        ElseIf TypeOf cntr Is RadioButton Then

            If DirectCast(cntr, RadioButton).Checked Then
                Return 1
            Else
                Return 0
            End If

        ElseIf TypeOf cntr Is DateTimePicker Then

            Return DirectCast(cntr, DateTimePicker).Value.Date

        ElseIf TypeOf cntr Is AccListComboBox Then

            Dim underlyingType As Type = DirectCast(cntr.Tag, Type)

            If Not param.AllowNull AndAlso GetType(IValueObject).IsAssignableFrom(underlyingType) Then
                Dim valueObject As IValueObject = Nothing
                Try
                    valueObject = DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, IValueObject)
                Catch ex As Exception
                End Try
                If valueObject Is Nothing OrElse valueObject.IsEmpty Then
                    Throw New Exception(String.Format("Nenurodyta {0}.", param.Prompt))
                End If
            End If

            If underlyingType Is GetType(HelperLists.AccountInfo) Then

                Try
                    Return DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, AccountInfo).ID
                Catch ex As Exception
                    Return 0
                End Try

            ElseIf underlyingType Is GetType(HelperLists.AssignableCRItem) Then

                Try
                    Return DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, AssignableCRItem).ID
                Catch ex As Exception
                    Return 0
                End Try

            ElseIf underlyingType Is GetType(HelperLists.CashAccountInfo) Then

                Try
                    Return DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, CashAccountInfo).ID
                Catch ex As Exception
                    Return 0
                End Try

            ElseIf underlyingType Is GetType(HelperLists.GoodsGroupInfo) Then

                Try
                    Return DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, GoodsGroupInfo).ID
                Catch ex As Exception
                    Return 0
                End Try

            ElseIf underlyingType Is GetType(HelperLists.GoodsInfo) Then

                Try
                    Return DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, GoodsInfo).ID
                Catch ex As Exception
                    Return 0
                End Try

            ElseIf underlyingType Is GetType(HelperLists.LongTermAssetCustomGroupInfo) Then

                Try
                    Return DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, LongTermAssetCustomGroupInfo).ID
                Catch ex As Exception
                    Return 0
                End Try

            ElseIf underlyingType Is GetType(HelperLists.PersonGroupInfo) Then

                Try
                    Return DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, PersonGroupInfo).ID
                Catch ex As Exception
                    Return 0
                End Try

            ElseIf underlyingType Is GetType(HelperLists.PersonInfo) Then

                Try
                    Return DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, PersonInfo).ID
                Catch ex As Exception
                    Return 0
                End Try

            ElseIf underlyingType Is GetType(HelperLists.ServiceInfo) Then

                Try
                    Return DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, ServiceInfo).ID
                Catch ex As Exception
                    Return 0
                End Try

            ElseIf underlyingType Is GetType(HelperLists.ShortLabourContract) Then

                Try
                    Return DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, ShortLabourContract).Serial
                Catch ex As Exception
                    Return ""
                End Try

            ElseIf underlyingType Is GetType(HelperLists.VatDeclarationSchemaInfo) Then

                Try
                    Return DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, VatDeclarationSchemaInfo).ID
                Catch ex As Exception
                    Return 0
                End Try

            ElseIf underlyingType Is GetType(HelperLists.WarehouseInfo) Then

                Try
                    Return DirectCast(DirectCast(cntr, AccListComboBox). _
                        SelectedValue, WarehouseInfo).ID
                Catch ex As Exception
                    Return 0
                End Try

            Else
                Throw New NotImplementedException(String.Format( _
                    "Tipas {0} neimplementuotas metode GetControlValue", _
                    underlyingType.FullName))
            End If

        ElseIf TypeOf cntr Is ComboBox Then

            If param.Name.Trim.ToLower.EndsWith("Year".ToLower) Then

                Dim result As Integer = 0
                Try
                    Try
                        result = CInt(DirectCast(cntr, ComboBox).SelectedItem.ToString)
                    Catch ex As Exception
                    End Try
                Catch ex As Exception
                End Try
                If result < 1970 OrElse result > 2100 Then
                    Throw New Exception(String.Format("Nenurodyta {0}.", param.Prompt))
                End If
                Return result

            ElseIf param.Name.Trim.ToLower.EndsWith("Month".ToLower) Then

                Dim result As Integer = 0
                result = DirectCast(cntr, ComboBox).SelectedIndex + 1
                If result < 1 OrElse result > 12 Then
                    Throw New Exception(String.Format("Nenurodyta {0}.", param.Prompt))
                End If
                Return result

            Else

                Dim result As UserReportParamValueInfo = Nothing
                Try
                    result = DirectCast(DirectCast(cntr, ComboBox).SelectedValue,  _
                        UserReportParamValueInfo)
                Catch ex As Exception
                End Try
                If result Is Nothing Then
                    If Not param.AllowNull Then
                        Throw New Exception(String.Format("Nenurodyta {0}.", param.Prompt))
                    End If
                    Return ""
                Else
                    Return result.Value
                End If

            End If

        Else

            Throw New NotImplementedException(String.Format( _
                "Control'o tipas {0} neimplementuotas metode GetControlValue", _
                cntr.GetType.FullName))

        End If


    End Function

End Class
