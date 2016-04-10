Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Globalization

''' <summary>
''' Custom DateTime picker that understands additional commands.
''' Ex: td, +, -
''' </summary>
<DesignerCategory("")> _
<ToolboxItem(True), ToolboxBitmap(GetType(DateTimePicker))> _
Public Class SmartDateTimePicker
    Inherits DateTimePicker

#Region "Private variables"

    Private Const checkWidth As Integer = 0
    Private Const buttonWidth As Integer = 16

    Private _myDateTextBox As TextBox
    Private _myDateValue As Date
    Private _customFormat As String = "d"

#End Region

#Region "Constructor and destructor"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="SmartDateTimePicker"/> class.
    ''' </summary>
    Public Sub New()
        ' This call is required by the Windows.Forms Form Designer.
        InitializeComponent()

        MyBase.TabStop = False

        _myDateTextBox.BorderStyle = BorderStyle.Fixed3D
        _myDateTextBox.MaxLength = 10

        'Initialise base.Format to Custom, we only need Custom Format
        SmartDateTimePicker_Resize(Me, Nothing)

    End Sub

#End Region

#Region "Component Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify 
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()

        _myDateTextBox = New System.Windows.Forms.TextBox()
        _myDateValue = Today
        SuspendLayout()
        ' 
        ' txtDateTime
        ' 
        _myDateTextBox.Location = New System.Drawing.Point(20, 49)
        _myDateTextBox.MaxLength = 50
        _myDateTextBox.Name = "myDateTextBox"
        _myDateTextBox.TabIndex = 0
        _myDateTextBox.Text = ""
        AddHandler _myDateTextBox.Leave, AddressOf MyTextBox_Leave
        AddHandler _myDateTextBox.Enter, AddressOf MyTextBox_Enter
        ' 
        ' DateTimePicker
        ' 
        Controls.Add(_myDateTextBox)

        ' setup events 
        AddHandler DropDown, AddressOf SmartDateTimePicker_DropDown
        AddHandler CloseUp, AddressOf SmartDateTimePicker_CloseUp
        AddHandler FontChanged, AddressOf SmartDateTimePicker_FontChanged
        AddHandler ForeColorChanged, AddressOf SmartDateTimePicker_ForeColorChanged
        AddHandler BackColorChanged, AddressOf SmartDateTimePicker_BackColorChanged
        AddHandler Resize, AddressOf SmartDateTimePicker_Resize
        AddHandler Enter, AddressOf SmartDateTimePicker_Enter
        AddHandler FormatChanged, AddressOf SmartDateTimePicker_FormatChanged
        AddHandler EnabledChanged, AddressOf SmartDateTimePicker_EnabledChanged

        ' set format 
        MyBase.Format = DateTimePickerFormat.[Short]
        _customFormat = "d"

        ResumeLayout(False)

    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets or sets the text associated with this control.
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    ''' A string that represents the text associated with this control.
    ''' </returns>
    ''' <PermissionSet>
    ''' 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    ''' 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    ''' 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
    ''' 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    ''' </PermissionSet>
    <Bindable(True)> _
    Public Shadows Property Text() As String
        Get
            Return _myDateValue.ToString(_customFormat)
        End Get
        Set(ByVal value As String)
            ' set the Text property of _mySmartDate
            SetMyValue(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the date/time value assigned to the control.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The <see cref="T:System.DateTime"/> value assign to the control.</returns>
    ''' <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than <see cref="P:System.Windows.Forms.DateTimePicker.MinDate"/> or more than <see cref="P:System.Windows.Forms.DateTimePicker.MaxDate"/>.</exception>
    ''' <PermissionSet>
    ''' 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    ''' 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    ''' 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
    ''' 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    ''' </PermissionSet>
    <Bindable(True)> _
    Public Shadows Property Value() As Date
        Get
            Return _myDateValue.Date
        End Get
        Set(ByVal value As Date)
            ' set the Date property of _mySmartDate
            SetMyValue(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the format of the date and time displayed in the control.
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    ''' One of the <see cref="T:System.Windows.Forms.DateTimePickerFormat"/> values. The default is <see cref="F:System.Windows.Forms.DateTimePickerFormat.Long"/>.
    ''' </returns>
    ''' <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
    ''' The value assigned is not one of the <see cref="T:System.Windows.Forms.DateTimePickerFormat"/> values.
    ''' </exception>
    ''' <PermissionSet>
    ''' 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    ''' 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    ''' 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
    ''' 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    ''' </PermissionSet>
    <Description("Constant - overridden and can only be DateTimePickerFormat.Custom."), DefaultValue(DateTimePickerFormat.[Custom])> _
    Public Shadows Property Format() As DateTimePickerFormat
        Get
            Return MyBase.Format
        End Get
        Set(ByVal value As DateTimePickerFormat)
            MyBase.Format = DateTimePickerFormat.Custom
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the custom date/time format string.
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    ''' A string that represents the custom date/time format. The default is null.
    ''' </returns>
    ''' <PermissionSet>
    ''' 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    ''' 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    ''' 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
    ''' 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
    ''' </PermissionSet>
    <Description("Sets the display format for use in DateTimePicker"), DefaultValue("d"), Browsable(True)> _
    Public Shadows Property CustomFormat() As String
        Get
            Return _customFormat
        End Get
        Set(ByVal value As String)
            _customFormat = value
            UpdateMyTextBox()
        End Set
    End Property

#End Region

#Region "DateTimePicker events"

    ''' <summary>
    ''' Handles the Resize event of the DateTimePicker control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub SmartDateTimePicker_Resize(ByVal sender As Object, ByVal e As EventArgs)
        _myDateTextBox.Location = New System.Drawing.Point(0, 0)
        '_myDateTextBox.Location = New System.Drawing.Point(-2 + checkWidth, -2)
        _myDateTextBox.Size = New System.Drawing.Size(Width - buttonWidth - checkWidth, Height)
    End Sub

    Private Sub SmartDateTimePicker_FontChanged(ByVal sender As Object, ByVal e As EventArgs)
        _myDateTextBox.Font = Font
    End Sub

    Private Sub SmartDateTimePicker_BackColorChanged(ByVal sender As Object, ByVal e As EventArgs)
        _myDateTextBox.BackColor = BackColor
    End Sub

    Private Sub SmartDateTimePicker_ForeColorChanged(ByVal sender As Object, ByVal e As EventArgs)
        _myDateTextBox.ForeColor = BackColor
    End Sub

    Private Sub SmartDateTimePicker_EnabledChanged(ByVal sender As Object, ByVal e As EventArgs)
        _myDateTextBox.ReadOnly = Not Enabled
    End Sub

    Private Sub SmartDateTimePicker_FormatChanged(ByVal sender As Object, ByVal e As EventArgs)
        UpdateMyTextBox()
    End Sub

    Private Sub DateTimePicker_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        If MyBase.Value.Date <> _myDateValue.Date Then
            _myDateValue = MyBase.Value
        End If
        UpdateMyTextBox()
    End Sub

    Private Sub MyTextBox_Enter(ByVal sender As Object, ByVal e As EventArgs)
        If _myDateTextBox.Text.Length <= 0 Then Exit Sub
        _myDateTextBox.SelectionStart = 0
        _myDateTextBox.SelectionLength = _myDateTextBox.Text.Length
    End Sub

    Private Sub MyTextBox_Leave(ByVal sender As Object, ByVal e As EventArgs)
        SetMyValue(_myDateTextBox.Text)
    End Sub

    Private Sub SmartDateTimePicker_Enter(ByVal sender As Object, ByVal e As EventArgs)
        _myDateTextBox.Focus()
    End Sub

    Private Sub SmartDateTimePicker_DropDown(ByVal sender As Object, ByVal e As EventArgs)
        ' hookup event for callback on selected value
        AddHandler MyBase.TextChanged, AddressOf DateTimePicker_TextChanged
    End Sub

    Private Sub SmartDateTimePicker_CloseUp(ByVal sender As Object, ByVal e As EventArgs)
        ' unhook event for callback on selected value and focus myTextBox
        RemoveHandler MyBase.TextChanged, AddressOf DateTimePicker_TextChanged
        _myDateTextBox.Focus()
    End Sub

    Private Sub SetMyValue(ByVal text As String)
        Dim tempdate As Date
        Dim currentFormat As String = _customFormat
        If currentFormat Is Nothing OrElse String.IsNullOrEmpty(currentFormat.Trim) _
            OrElse currentFormat.Trim.ToLower = "d" Then
            currentFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern
        End If
        If Date.TryParseExact(text, currentFormat, Globalization.CultureInfo.InvariantCulture, _
            DateTimeStyles.None, tempdate) Then
            If tempdate.Date <> _myDateValue.Date Then
                _myDateValue = tempdate
            End If
        ElseIf Date.TryParseExact(text, New String() {"yyyy-MM-dd", "yyyyMMdd", "yyMMdd"}, _
            Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, tempdate) Then
            If tempdate.Date <> _myDateValue.Date Then
                _myDateValue = tempdate
            End If
        End If
        UpdateMyTextBox()
        SetBaseValue()
    End Sub

    Private Sub SetMyValue(ByVal value As DateTime)
        If _myDateValue.Date <> value.Date Then
            _myDateValue = value
        End If
        UpdateMyTextBox()
        SetBaseValue()
    End Sub

    Private Sub SetBaseValue()
        MyBase.Value = _myDateValue.Date
    End Sub

    ''' <summary>
    ''' Formats the text box.
    ''' </summary>
    Private Sub UpdateMyTextBox()
        If _myDateValue.ToString(_customFormat) = _myDateTextBox.Text Then Exit Sub
        _myDateTextBox.Text = _myDateValue.ToString(_customFormat)
    End Sub

#End Region

End Class