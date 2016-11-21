'***************************************************************************
'* Class Name   : ErrorWarnInfoProvider.cs
'* Author       : Kenneth J. Koteles
'* Created      : 10/04/2007 2:14 PM
'* C# Version   : .NET 2.0
'* Description  : This code is designed to create a new provider object to
'*           work specifically with CSLA BusinessBase objects.  In
'*           addition to providing the red error icon for items in the
'*           BrokenRulesCollection with Csla.Rules.RuleSeverity.Error,
'*           this object also provides a yellow warning icon for items
'*           with Csla.Rules.RuleSeverity.Warning and a blue
'*           information icon for items with
'*           Csla.Rules.RuleSeverity.Information.  Since warnings
'*           and information type items do not need to be fixed /
'*           corrected prior to the object being saved, the tooltip
'*           displayed when hovering over the respective icon contains
'*           all the control's associated (by severity) broken rules.
'* Revised      : 11/20/2007 8:32 AM
'*     Change   : Warning and information icons were not being updated for
'*           dependant properties (controls without the focus) when
'*           changes were being made to a related property (control with
'*           the focus).  Added a list of controls to be recursed
'*           through each time a change was made to any control.  This
'*           obviously could result in performance issues; however,
'*           there is no consistent way to question the BusinessObject
'*           in order to get a list of dependant properties based on a
'*           property name.  It can be exposed to the UI (using
'*           ValidationRules.GetRuleDescriptions()); however, it is up
'*           to each developer to implement their own public method on
'*           on the Business Object to do so.  To make this generic for
'*           all CSLA Business Objects, I cannot assume the developer
'*           always exposes the dependant properties (nor do I know what
'*                they'll call the method); therefore, this is the best I can
'*           do right now.
'* Revised      : 11/23/2007 9:02 AM
'*     Change   : Added new property ProcessDependantProperties to allow for
'*           controlling when all controls are recursed through (for
'*           dependant properties or not).  Default value is 'false'.
'*           This allows the developer to ba able to choose whether or
'*           not to use the control in this manner (which could have
'*           performance implications).
'* Revised      : 10/05/2009, Jonny Bekkum
'*     Change: Added initialization of controls list (controls attached to BindingSource) 
'*           and will update errors on all controls. Optimized retrieval of error, warn, info 
'*           messages and setting these on the controls. 
'***************************************************************************


Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

''' <summary>
''' WindowsForms extender control that automatically
''' displays error, warning, or information icons and
''' text for the form controls based on the
''' BrokenRulesCollection of a CSLA .NET business object.
''' </summary>
<DesignerCategory("")> _
<ToolboxItem(True), ToolboxBitmap(GetType(ErrorWarnInfoProvider), "Cascade.ico")> _
Public Class ErrorWarnInfoProvider
    Inherits ErrorProvider
    Implements IExtenderProvider
    Implements ISupportInitialize

#Region "private variables"

    Private ReadOnly components As IContainer
    Private ReadOnly _errorProviderInfo As ErrorProvider
    Private ReadOnly _errorProviderWarn As ErrorProvider
    Private ReadOnly _controls As New List(Of Control)()
    Private Shared ReadOnly DefaultIconInformation As Icon
    Private Shared ReadOnly DefaultIconWarning As Icon
    Private _offsetInformation As Integer = 32
    Private _offsetWarning As Integer = 16
    Private _showInformation As Boolean = True
    Private _showWarning As Boolean = True
    Private _showMostSevereOnly As Boolean = True
    Private ReadOnly _errorList As New Dictionary(Of String, String)()
    Private ReadOnly _warningList As New Dictionary(Of String, String)()
    Private ReadOnly _infoList As New Dictionary(Of String, String)()
    Private _isInitializing As Boolean
    Private _childProperties As New List(Of String)

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Initializes the <see cref="ErrorWarnInfoProvider"/> class.
    ''' </summary>
    Shared Sub New()
        DefaultIconInformation = My.Resources.InformationIco16
        DefaultIconWarning = My.Resources.WarningIco16
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ErrorWarnInfoProvider"/> class.
    ''' </summary>
    Public Sub New()
        Me.components = New System.ComponentModel.Container()
        Me._errorProviderInfo = New System.Windows.Forms.ErrorProvider(Me.components)
        Me._errorProviderWarn = New System.Windows.Forms.ErrorProvider(Me.components)
        BlinkRate = 0

        _errorProviderInfo.BlinkRate = 0
        _errorProviderInfo.Icon = DefaultIconInformation

        _errorProviderWarn.BlinkRate = 0
        _errorProviderWarn.Icon = DefaultIconWarning
    End Sub

    ''' <summary>
    ''' Creates an instance of the object.
    ''' </summary>
    ''' <param name="container">The container of the control.</param>
    Public Sub New(ByVal container As IContainer)
        Me.New()
        container.Add(Me)
    End Sub

    ''' <summary>
    ''' Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"></see> and optionally releases the managed resources.
    ''' </summary>
    ''' <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

#Region "IExtenderProvider Members"

    ''' <summary>
    ''' Gets a value indicating whether the extender control
    ''' can extend the specified control.
    ''' </summary>
    ''' <param name="extendee">The control to be extended.</param>
    ''' <remarks>
    ''' Any control implementing either a ReadOnly property or
    ''' Enabled property can be extended.
    ''' </remarks>
    Private Function IExtenderProvider_CanExtend(ByVal extendee As Object) As Boolean Implements IExtenderProvider.CanExtend
        'if (extendee is ErrorProvider)
        '{
        '    return true;
        '}
        If (TypeOf extendee Is Control AndAlso Not (TypeOf extendee Is Form)) Then
            Return Not (TypeOf extendee Is ToolBar)
        Else
            Return False
        End If
    End Function

#End Region

#Region "Public properties"

    ''' <summary>
    ''' Gets or sets the rate at which the error icon flashes.
    ''' </summary>
    ''' <value>The rate, in milliseconds, at which the error icon should flash. The default is 250 milliseconds.</value>
    ''' <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero. </exception>
    <Category("Behavior")> _
    <DefaultValue(0)> _
    <Description("The rate in milliseconds at which the error icon blinks.")> _
    Public Shadows Property BlinkRate() As Integer
        Get
            Return MyBase.BlinkRate
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then
                Throw New ArgumentOutOfRangeException("BlinkRate", value, "Blink rate must be zero or more")
            End If

            MyBase.BlinkRate = value
            If value = 0 Then
                BlinkStyle = ErrorBlinkStyle.NeverBlink
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the rate at which the information icon flashes.
    ''' </summary>
    ''' <value>The rate, in milliseconds, at which the information icon should flash. The default is 250 milliseconds.</value>
    ''' <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero. </exception>
    <Category("Behavior")> _
    <DefaultValue(0)> _
    <Description("The rate in milliseconds at which the information icon blinks.")> _
    Public Property BlinkRateInformation() As Integer
        Get
            Return _errorProviderInfo.BlinkRate
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then
                Throw New ArgumentOutOfRangeException("BlinkRateInformation", value, "Blink rate must be zero or more")
            End If

            _errorProviderInfo.BlinkRate = value

            If value = 0 Then
                _errorProviderInfo.BlinkStyle = ErrorBlinkStyle.NeverBlink
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the rate at which the warning icon flashes.
    ''' </summary>
    ''' <value>The rate, in milliseconds, at which the warning icon should flash.
    ''' The default is 250 milliseconds.</value>
    ''' <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero. </exception>
    <Category("Behavior")> _
    <DefaultValue(0)> _
    <Description("The rate in milliseconds at which the warning icon blinks.")> _
    Public Property BlinkRateWarning() As Integer
        Get
            Return _errorProviderWarn.BlinkRate
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then
                Throw New ArgumentOutOfRangeException("BlinkRateWarning", value, "Blink rate must be zero or more")
            End If

            _errorProviderWarn.BlinkRate = value

            If value = 0 Then
                _errorProviderWarn.BlinkStyle = ErrorBlinkStyle.NeverBlink
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating when the error icon flashes.
    ''' </summary>
    ''' <value>One of the <see cref="T:System.Windows.Forms.ErrorBlinkStyle"/> values.
    ''' The default is <see cref="F:System.Windows.Forms.ErrorBlinkStyle.BlinkIfDifferentError"/>.</value>
    ''' <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ErrorBlinkStyle"/> values. </exception>
    <Category("Behavior")> _
    <DefaultValue(ErrorBlinkStyle.NeverBlink)> _
    <Description("Controls whether the error icon blinks when an error is set.")> _
    Public Shadows Property BlinkStyle() As ErrorBlinkStyle
        Get
            Return MyBase.BlinkStyle
        End Get
        Set(ByVal value As ErrorBlinkStyle)
            MyBase.BlinkStyle = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating when the information icon flashes.
    ''' </summary>
    ''' <value>One of the <see cref="T:System.Windows.Forms.ErrorBlinkStyle"/> values.
    ''' The default is <see cref="F:System.Windows.Forms.ErrorBlinkStyle.BlinkIfDifferentError"/>.</value>
    ''' <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ErrorBlinkStyle"/> values. </exception>
    <Category("Behavior")> _
    <DefaultValue(ErrorBlinkStyle.NeverBlink)> _
    <Description("Controls whether the information icon blinks when information is set.")> _
    Public Property BlinkStyleInformation() As ErrorBlinkStyle
        Get
            Return _errorProviderInfo.BlinkStyle
        End Get
        Set(ByVal value As ErrorBlinkStyle)
            _errorProviderWarn.BlinkStyle = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating when the warning icon flashes.
    ''' </summary>
    ''' <value>One of the <see cref="T:System.Windows.Forms.ErrorBlinkStyle"/> values. The default is <see cref="F:System.Windows.Forms.ErrorBlinkStyle.BlinkIfDifferentError"/>.</value>
    ''' <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ErrorBlinkStyle"/> values. </exception>
    <Category("Behavior")> _
    <DefaultValue(ErrorBlinkStyle.NeverBlink)> _
    <Description("Controls whether the warning icon blinks when a warning is set.")> _
    Public Property BlinkStyleWarning() As ErrorBlinkStyle
        Get
            Return _errorProviderWarn.BlinkStyle
        End Get
        Set(ByVal value As ErrorBlinkStyle)
            _errorProviderWarn.BlinkStyle = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the data source that the <see cref="T:System.Windows.Forms.ErrorProvider"></see> monitors.
    ''' </summary>
    ''' <value>A data source based on the <see cref="T:System.Collections.IList"></see> interface to be monitored for errors. Typically, this is a <see cref="T:System.Data.DataSet"></see> to be monitored for errors.</value>
    ''' <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    <DefaultValue(DirectCast(Nothing, String))> _
    Public Shadows Property DataSource() As Object
        Get
            Return MyBase.DataSource
        End Get
        Set(ByVal value As Object)
            If Not MyBase.DataSource Is value Then
                Dim bs1 = TryCast(MyBase.DataSource, BindingSource)
                If bs1 IsNot Nothing Then
                    RemoveHandler bs1.CurrentItemChanged, AddressOf DataSource_CurrentItemChanged
                End If
            End If

            MyBase.DataSource = value

            Dim bs = TryCast(value, BindingSource)
            If bs IsNot Nothing Then
                AddHandler bs.CurrentItemChanged, AddressOf DataSource_CurrentItemChanged
            End If
        End Set
    End Property

    Private Sub UpdateBindingsAndProcessAllControls()
        If ContainerControl IsNot Nothing Then
            InitializeAllControls(ContainerControl.Controls)
        End If
        ProcessAllControls()
    End Sub

    ''' <summary>
    ''' Gets or sets the icon information.
    ''' </summary>
    ''' <value>The icon information.</value>
    <Category("Behavior")> _
    <Description("The icon used to indicate information.")> _
    Public Property IconInformation() As Icon
        Get
            Return _errorProviderInfo.Icon
        End Get
        Set(ByVal value As Icon)
            If value Is Nothing Then
                value = DefaultIconInformation
            End If

            _errorProviderInfo.Icon = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the icon warning.
    ''' </summary>
    ''' <value>The icon warning.</value>
    <Category("Behavior")> _
    <Description("The icon used to indicate a warning.")> _
    Public Property IconWarning() As Icon
        Get
            Return _errorProviderWarn.Icon
        End Get
        Set(ByVal value As Icon)
            If value Is Nothing Then
                value = DefaultIconWarning
            End If

            _errorProviderWarn.Icon = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the offset information.
    ''' </summary>
    ''' <value>The offset information.</value>
    <Category("Behavior")> _
    <DefaultValue(32), Description("The number of pixels the information icon will be offset from the error icon.")> _
    Public Property OffsetInformation() As Integer
        Get
            Return _offsetInformation
        End Get
        Set(ByVal value As Integer)
            _offsetInformation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the offset warning.
    ''' </summary>
    ''' <value>The offset warning.</value>
    <Category("Behavior")> _
    <DefaultValue(16), Description("The number of pixels the warning icon will be offset from the error icon.")> _
    Public Property OffsetWarning() As Integer
        Get
            Return _offsetWarning
        End Get
        Set(ByVal value As Integer)
            _offsetWarning = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether broken rules with severity information should be visible.
    ''' </summary>
    ''' <value><c>true</c> if information is visible; otherwise, <c>false</c>.</value>
    <Category("Behavior")> _
    <DefaultValue(True), Description("Determines if the information icon should be displayed when information exists.")> _
    Public Property ShowInformation() As Boolean
        Get
            Return _showInformation
        End Get
        Set(ByVal value As Boolean)
            _showInformation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether broken rules with severity Warning should be visible.
    ''' </summary>
    ''' <value><c>true</c> if Warning is visible; otherwise, <c>false</c>.</value>
    <Category("Behavior")> _
    <DefaultValue(True), Description("Determines if the warning icon should be displayed when warnings exist.")> _
    Public Property ShowWarning() As Boolean
        Get
            Return _showWarning
        End Get
        Set(ByVal value As Boolean)
            _showWarning = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether show only most severe broken rules message.
    ''' </summary>
    ''' <value><c>true</c> if show only most severe; otherwise, <c>false</c>.</value>
    <Category("Behavior")> _
    <DefaultValue(True), Description("Determines if the broken rules are show by severity - if true only most severe level is shown.")> _
    Public Property ShowOnlyMostSevere() As Boolean
        Get
            Return _showMostSevereOnly
        End Get
        Set(ByVal value As Boolean)
            If _showMostSevereOnly <> value Then
                _showMostSevereOnly = value
                'Refresh controls
                ProcessAllControls()
            End If
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Clears all errors associated with this component.
    ''' </summary>
    ''' <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    Public Shadows Sub Clear()
        MyBase.Clear()
        _errorProviderInfo.Clear()
        _errorProviderWarn.Clear()
    End Sub

    ''' <summary>
    ''' Returns the current information description string for the specified control.
    ''' </summary>
    ''' <param name="control">The item to get the error description string for.</param>
    ''' <returns>The information description string for the specified control.</returns>
    Public Function GetInformation(ByVal control As Control) As String
        Return _errorProviderInfo.GetError(control)
    End Function

    ''' <summary>
    ''' Returns the current warning description string for the specified control.
    ''' </summary>
    ''' <param name="control">The item to get the error description string for.</param>
    ''' <returns>The warning description string for the specified control.</returns>
    Public Function GetWarning(ByVal control As Control) As String
        Return _errorProviderWarn.GetError(control)
    End Function

    Private Sub InitializeAllControls(ByVal controls As Control.ControlCollection)
        ' clear internal
        _controls.Clear()
        _childProperties.Clear()

        ' run recursive initialize of controls
        Initialize(controls)
    End Sub

    Private Sub Initialize(ByVal controls As Control.ControlCollection)
        'We don't provide an extended property, so if the control is
        ' not a Label then 'hook' the validating event here!
        For Each control As Control In controls
            If TypeOf control Is Label Then
                Continue For
            End If
            ' Initialize bindings
            For Each binding As Binding In control.DataBindings
                ' get the Binding if appropriate
                If binding.DataSource Is DataSource Then
                    _controls.Add(control)
                    If binding.BindingMemberInfo.BindingMember.Contains(".") Then
                        Dim childProperty As String = binding.BindingMemberInfo.BindingMember.Split( _
                            New Char() {"."c}, StringSplitOptions.RemoveEmptyEntries)(0)
                        If Not _childProperties.Contains(childProperty) Then
                            _childProperties.Add(childProperty)
                        End If
                    End If
                End If
            Next
            ' Initialize any subcontrols
            If control.Controls.Count > 0 Then
                Initialize(control.Controls)
            End If
        Next
    End Sub

    Private Sub DataSource_CurrentItemChanged(ByVal sender As Object, ByVal e As EventArgs)
        Debug.Print("ErrorWarnInfo: CurrentItemChanged, {0}", DateTime.Now.Ticks)
        ProcessAllControls()
    End Sub

    Private Sub ProcessAllControls()
        If _isInitializing Then
            Return
        End If

        ' get error/warn/info list from business object
        GetWarnInfoList()
        ' process controls in window
        ProcessControls()
    End Sub

    Private Sub GetWarnInfoList()
        _infoList.Clear()
        _warningList.Clear()
        _errorList.Clear()

        Dim bs As BindingSource = DirectCast(DataSource, BindingSource)
        If bs Is Nothing Then
            Return
        End If
        If bs.Position = -1 Then
            Return
        End If

        ' we can only deal with CSLA BusinessBase objects
        If TypeOf bs.Current Is Csla.Core.BusinessBase Then
            ' get the BusinessBase object
            Dim bb As Csla.Core.BusinessBase = TryCast(bs.Current, Csla.Core.BusinessBase)

            If bb IsNot Nothing Then

                GetBrokenRules(bb, "")

                For Each childProperty As String In _childProperties
                    Dim child As Csla.Core.BusinessBase = Nothing
                    Try
                        child = DirectCast(bb.GetType.GetProperty(childProperty).GetValue(bb, Nothing), Csla.Core.BusinessBase)
                    Catch ex As Exception
                    End Try
                    If Not child Is Nothing Then
                        GetBrokenRules(child, childProperty & ".")
                    End If
                Next

            End If
        End If
    End Sub

    Private Sub GetBrokenRules(ByVal bb As Csla.Core.BusinessBase, ByVal prefix As String)

        For Each br As Csla.Validation.BrokenRule In bb.BrokenRulesCollection
            ' we do not want to import result of object level broken rules 
            If br.[Property] Is Nothing OrElse String.IsNullOrEmpty(br.[Property].Trim) Then
                Continue For
            End If

            Dim key As String = prefix & br.[Property]

            Select Case br.Severity
                Case Csla.Validation.RuleSeverity.[Error]
                    If _errorList.ContainsKey(key) Then
                        _errorList(key) = [String].Concat(_errorList(key), Environment.NewLine, br.Description)
                    Else
                        _errorList.Add(key, br.Description)
                    End If
                    Exit Select
                Case Csla.Validation.RuleSeverity.Warning
                    If _warningList.ContainsKey(key) Then
                        _warningList(key) = [String].Concat(_warningList(key), Environment.NewLine, br.Description)
                    Else
                        _warningList.Add(key, br.Description)
                    End If
                    Exit Select
                Case Else
                    ' consider it an Info
                    If _infoList.ContainsKey(key) Then
                        _infoList(key) = [String].Concat(_infoList(key), Environment.NewLine, br.Description)
                    Else
                        _infoList.Add(key, br.Description)
                    End If
                    Exit Select
            End Select

        Next

    End Sub

    Private Sub ProcessControls()
        For Each control As Control In _controls
            ProcessControl(control)
        Next
    End Sub

    ''' <summary>
    ''' Processes the control.
    ''' </summary>
    ''' <param name="control">The control.</param>
    Private Sub ProcessControl(ByVal control As Control)
        If control Is Nothing Then
            Throw New ArgumentNullException("control")
        End If

        Dim hasWarning As Boolean = False
        Dim hasInfo As Boolean = False
        Dim isChildProperty = False

        Dim sbError = New StringBuilder()
        Dim sbWarn = New StringBuilder()
        Dim sbInfo = New StringBuilder()

        For Each binding As Binding In control.DataBindings
            ' get the Binding if appropriate
            If binding.DataSource Is DataSource Then
                Dim propertyName As String = binding.BindingMemberInfo.BindingMember

                If _errorList.ContainsKey(propertyName) Then
                    sbError.AppendLine(_errorList(propertyName))
                End If
                If _warningList.ContainsKey(propertyName) Then
                    sbWarn.AppendLine(_warningList(propertyName))
                End If
                If _infoList.ContainsKey(propertyName) Then
                    sbInfo.AppendLine(propertyName)
                End If
                If propertyName.Contains(".") Then isChildProperty = True
            End If
        Next

        Dim bError As Boolean = sbError.Length > 0
        Dim bWarn As Boolean = sbWarn.Length > 0
        Dim bInfo As Boolean = sbInfo.Length > 0

        ' ErrorProvider does not handle child objects
        If isChildProperty AndAlso bError Then
            MyBase.SetError(control, sbError.ToString())
        ElseIf isChildProperty Then
            MyBase.SetError(control, String.Empty)
        End If

        ' set flags to indicat if Warning or Info is highest severity; else false
        If _showMostSevereOnly Then
            bInfo = bInfo AndAlso Not bWarn AndAlso Not bError
            bWarn = bWarn AndAlso Not bError
        End If

        Dim offsetInformation As Integer = _offsetInformation
        Dim offsetWarning As Integer = _offsetWarning

        ' Set / fix offsets
        ' by default the setting are correct for Error (0), Warning and Info
        If Not bError Then
            If bWarn Then
                ' warning and possibly info, no error
                offsetInformation = _offsetInformation - _offsetWarning
                offsetWarning = 0
            Else
                ' Info only
                offsetInformation = 0
            End If
        ElseIf Not bWarn Then
            offsetInformation = _offsetInformation - _offsetWarning
        End If

        ' should warning be visible
        If _showWarning AndAlso bWarn Then
            _errorProviderWarn.SetError(control, sbWarn.ToString())
            _errorProviderWarn.SetIconPadding(control, MyBase.GetIconPadding(control) + offsetWarning)
            _errorProviderWarn.SetIconAlignment(control, MyBase.GetIconAlignment(control))
            hasWarning = True
        End If

        ' should info be shown
        If _showInformation AndAlso bInfo Then
            _errorProviderInfo.SetError(control, sbInfo.ToString())
            _errorProviderInfo.SetIconPadding(control, MyBase.GetIconPadding(control) + offsetInformation)
            _errorProviderInfo.SetIconAlignment(control, MyBase.GetIconAlignment(control))

            hasInfo = True
        End If

        If Not hasWarning Then
            _errorProviderWarn.SetError(DirectCast(control, Control), String.Empty)
        End If
        If Not hasInfo Then
            _errorProviderInfo.SetError(DirectCast(control, Control), String.Empty)
        End If
    End Sub

    Private Sub ResetBlinkStyleInformation()
        BlinkStyleInformation = ErrorBlinkStyle.BlinkIfDifferentError
    End Sub

    Private Sub ResetBlinkStyleWarning()
        BlinkStyleWarning = ErrorBlinkStyle.BlinkIfDifferentError
    End Sub

    Private Sub ResetIconInformation()
        IconInformation = DefaultIconInformation
    End Sub

    Private Sub ResetIconWarning()
        IconWarning = DefaultIconWarning
    End Sub

    ''' <summary>
    ''' Sets the information description string for the specified control.
    ''' </summary>
    ''' <param name="control">The control to set the information description string for.</param>
    ''' <param name="value">The information description string, or null or System.String.Empty to remove the information description.</param>
    Public Sub SetInformation(ByVal control As Control, ByVal value As String)
        _errorProviderInfo.SetError(control, value)
    End Sub

    ''' <summary>
    ''' Sets the warning description string for the specified control.
    ''' </summary>
    ''' <param name="control">The control to set the warning description string for.</param>
    ''' <param name="value">The warning description string, or null or System.String.Empty to remove the warning description.</param>
    Public Sub SetWarning(ByVal control As Control, ByVal value As String)
        _errorProviderWarn.SetError(control, value)
    End Sub

    Private Function ShouldSerializeIconInformation() As Boolean
        Return (Not IconInformation Is DefaultIconInformation)
    End Function

    Private Function ShouldSerializeIconWarning() As Boolean
        Return (Not IconWarning Is DefaultIconWarning)
    End Function

    Private Function ShouldSerializeBlinkStyleInformation() As Boolean
        Return (BlinkStyleInformation <> ErrorBlinkStyle.BlinkIfDifferentError)
    End Function

    Private Function ShouldSerializeBlinkStyleWarning() As Boolean
        Return (BlinkStyleWarning <> ErrorBlinkStyle.BlinkIfDifferentError)
    End Function

    ''' <summary>
    ''' Provides a method to update the bindings of the <see cref="P:System.Windows.Forms.ErrorProvider.DataSource"></see>, <see cref="P:System.Windows.Forms.ErrorProvider.DataMember"></see>, and the error text.
    ''' </summary>
    ''' <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    Public Shadows Sub UpdateBinding()
        MyBase.UpdateBinding()
        _errorProviderInfo.UpdateBinding()
        _errorProviderWarn.UpdateBinding()
    End Sub

#End Region

#Region "ISupportInitialize Members"

    Private Sub ISupportInitialize_BeginInit() Implements ISupportInitialize.BeginInit
        _isInitializing = True
    End Sub

    Private Sub ISupportInitialize_EndInit() Implements ISupportInitialize.EndInit
        _isInitializing = False
        If Me.ContainerControl IsNot Nothing Then
            InitializeAllControls(Me.ContainerControl.Controls)
        End If
    End Sub

#End Region

End Class