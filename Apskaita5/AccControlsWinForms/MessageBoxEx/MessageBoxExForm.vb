Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Namespace MessageBoxExLib
    ''' <summary>
    ''' An advanced MessageBox that supports customizations like Font, Icon,
    ''' Buttons and Saved Responses
    ''' (c) http://www.codeproject.com/Articles/9656/Dissecting-the-MessageBox
    ''' </summary>
    Friend Class MessageBoxExForm
        Inherits System.Windows.Forms.Form

#Region "Constants"
        Private Const LEFT_PADDING As Integer = 12
        Private Const RIGHT_PADDING As Integer = 12
        Private Const TOP_PADDING As Integer = 12
        Private Const BOTTOM_PADDING As Integer = 12

        Private Const BUTTON_LEFT_PADDING As Integer = 4
        Private Const BUTTON_RIGHT_PADDING As Integer = 4
        Private Const BUTTON_TOP_PADDING As Integer = 4
        Private Const BUTTON_BOTTOM_PADDING As Integer = 4

        Private Const MIN_BUTTON_HEIGHT As Integer = 23
        Private Const MIN_BUTTON_WIDTH As Integer = 74

        Private Const ITEM_PADDING As Integer = 10
        Private Const ICON_MESSAGE_PADDING As Integer = 15

        Private Const BUTTON_PADDING As Integer = 5

        Private Const CHECKBOX_WIDTH As Integer = 20

        Private Const IMAGE_INDEX_EXCLAMATION As Integer = 0
        Private Const IMAGE_INDEX_QUESTION As Integer = 1
        Private Const IMAGE_INDEX_STOP As Integer = 2
        Private Const IMAGE_INDEX_INFORMATION As Integer = 3
#End Region

#Region "Fields"

        Private components As System.ComponentModel.IContainer
        Private chbSaveResponse As System.Windows.Forms.CheckBox
        Private imageListIcons As System.Windows.Forms.ImageList
        Private buttonToolTip As System.Windows.Forms.ToolTip

        Private _buttons As New ArrayList()
        Private _allowSaveResponse As Boolean
        Private _playAlert As Boolean = True
        Private _cancelButton As MessageBoxExButton = Nothing
        Private _defaultButtonControl As Button = Nothing

        Private _maxLayoutWidth As Integer
        Private _maxLayoutHeight As Integer

        Private _maxWidth As Integer
        Private _maxHeight As Integer

        Private _allowCancel As Boolean = True
        Private _result As String = Nothing
        Private _Exception As Exception = Nothing
        Private _BaseException As Exception = Nothing

        ''' <summary>
        ''' Used to determine the alert sound to play
        ''' </summary>
        Private _standardIcon As MessageBoxIcon = MessageBoxIcon.None
        Private _iconImage As Icon = Nothing

        Private timerTimeout As Timer = Nothing
        Private _timeout As Integer = 0
        Private _timeoutResult As TimeoutResult = TimeoutResult.[Default]
        Private panelIcon As System.Windows.Forms.Panel
        Private rtbMessage As System.Windows.Forms.RichTextBox

        ''' <summary>
        ''' Maps MessageBoxEx buttons to Button controls
        ''' </summary>
        Private _buttonControlsTable As New Hashtable()
#End Region

#Region "Properties"

        Public WriteOnly Property Message() As String
            Set(ByVal value As String)
                rtbMessage.Text = value
            End Set
        End Property

        Public WriteOnly Property Caption() As String
            Set(ByVal value As String)
                Me.Text = value
            End Set
        End Property

        Public WriteOnly Property CustomFont() As Font
            Set(ByVal value As Font)
                Me.Font = value
            End Set
        End Property

        Public ReadOnly Property Buttons() As ArrayList
            Get
                Return _buttons
            End Get
        End Property

        Public Property AllowSaveResponse() As Boolean
            Get
                Return _allowSaveResponse
            End Get
            Set(ByVal value As Boolean)
                _allowSaveResponse = value
            End Set
        End Property

        Public ReadOnly Property SaveResponse() As Boolean
            Get
                Return chbSaveResponse.Checked
            End Get
        End Property

        Public WriteOnly Property SaveResponseText() As String
            Set(ByVal value As String)
                chbSaveResponse.Text = value
            End Set
        End Property

        Public WriteOnly Property StandardIcon() As MessageBoxIcon
            Set(ByVal value As MessageBoxIcon)
                SetStandardIcon(value)
            End Set
        End Property

        Public WriteOnly Property CustomIcon() As Icon
            Set(ByVal value As Icon)
                _standardIcon = MessageBoxIcon.None
                _iconImage = value
            End Set
        End Property

        Public WriteOnly Property CustomCancelButton() As MessageBoxExButton
            Set(ByVal value As MessageBoxExButton)
                _cancelButton = value
            End Set
        End Property

        Public ReadOnly Property Result() As String
            Get
                Return _result
            End Get
        End Property

        Public Property PlayAlertSound() As Boolean
            Get
                Return _playAlert
            End Get
            Set(ByVal value As Boolean)
                _playAlert = value
            End Set
        End Property

        Public Property Timeout() As Integer
            Get
                Return _timeout
            End Get
            Set(ByVal value As Integer)
                _timeout = value
            End Set
        End Property

        Public Property TimeoutResult() As TimeoutResult
            Get
                Return _timeoutResult
            End Get
            Set(ByVal value As TimeoutResult)
                _timeoutResult = value
            End Set
        End Property

        Public WriteOnly Property [Exception]() As Exception
            Set(ByVal value As Exception)
                _Exception = value
            End Set
        End Property

        Public WriteOnly Property BaseException() As Exception
            Set(ByVal value As Exception)
                _BaseException = value
            End Set
        End Property

#End Region

#Region "Ctor/Dtor"

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            _maxWidth = CInt((SystemInformation.WorkingArea.Width * 0.6R))
            _maxHeight = CInt((SystemInformation.WorkingArea.Height * 0.9R))
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#End Region

#Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            ' Dim resources As New System.Resources.ResourceManager(GetType(MessageBoxExForm))
            Me.panelIcon = New System.Windows.Forms.Panel()
            Me.chbSaveResponse = New System.Windows.Forms.CheckBox()
            Me.imageListIcons = New System.Windows.Forms.ImageList(Me.components)
            Me.buttonToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.rtbMessage = New System.Windows.Forms.RichTextBox()
            Me.SuspendLayout()
            ' 
            ' panelIcon
            ' 
            Me.panelIcon.BackColor = System.Drawing.Color.Transparent
            Me.panelIcon.Location = New System.Drawing.Point(8, 8)
            Me.panelIcon.Name = "panelIcon"
            Me.panelIcon.Size = New System.Drawing.Size(32, 32)
            Me.panelIcon.TabIndex = 3
            Me.panelIcon.Visible = False
            ' 
            ' chbSaveResponse
            ' 
            Me.chbSaveResponse.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.chbSaveResponse.Location = New System.Drawing.Point(56, 56)
            Me.chbSaveResponse.Name = "chbSaveResponse"
            Me.chbSaveResponse.Size = New System.Drawing.Size(104, 16)
            Me.chbSaveResponse.TabIndex = 0
            ' 
            ' imageListIcons
            ' 
            Me.imageListIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
            Me.imageListIcons.ImageSize = New System.Drawing.Size(32, 32)
            Me.imageListIcons.ImageStream = DirectCast(My.Resources.imageListIconsImageStream, System.Windows.Forms.ImageListStreamer)
            Me.imageListIcons.TransparentColor = System.Drawing.Color.Transparent
            ' 
            ' rtbMessage
            ' 
            Me.rtbMessage.BackColor = System.Drawing.SystemColors.Control
            Me.rtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.rtbMessage.Location = New System.Drawing.Point(200, 8)
            Me.rtbMessage.Name = "rtbMessage"
            Me.rtbMessage.[ReadOnly] = True
            Me.rtbMessage.Size = New System.Drawing.Size(100, 48)
            Me.rtbMessage.TabIndex = 4
            Me.rtbMessage.Text = ""
            Me.rtbMessage.Visible = False
            ' 
            ' MessageBoxExForm
            ' 
            Me.AutoScale = False
            Me.AutoScaleMode = Windows.Forms.AutoScaleMode.None
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
            Me.ClientSize = New System.Drawing.Size(322, 224)
            Me.Controls.Add(Me.rtbMessage)
            Me.Controls.Add(Me.chbSaveResponse)
            Me.Controls.Add(Me.panelIcon)
            Me.Font = New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte((0)))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.ShowInTaskbar = False
            Me.Name = "MessageBoxExForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual

            Me.ResumeLayout(False)
        End Sub

#End Region

#Region "Overrides"

        ''' <summary>
        ''' This will get called everytime we call ShowDialog on the form
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overloads Overrides Sub OnLoad(ByVal e As EventArgs)
            'Reset result
            _result = Nothing

            Me.Size = New Size(_maxWidth, _maxHeight)

            'This is the rectangle in which all items will be layed out
            _maxLayoutWidth = Me.ClientSize.Width - LEFT_PADDING - RIGHT_PADDING
            _maxLayoutHeight = Me.ClientSize.Height - TOP_PADDING - BOTTOM_PADDING

            AddOkButtonIfNoButtonsPresent()
            DisableCloseIfMultipleButtonsAndNoCancelButton()

            SetIconSizeAndVisibility()
            SetMessageSizeAndVisibility()
            SetCheckboxSizeAndVisibility()

            SetOptimumSize()

            LayoutControls()

            CenterForm()

            PlayAlert()

            SelectDefaultButton()

            StartTimerIfTimeoutGreaterThanZero()

            MyBase.OnLoad(e)
        End Sub


        Protected Overloads Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
            If CInt(keyData) = CInt((Keys.Alt Or Keys.F4)) AndAlso Not _allowCancel Then
                Return True
            End If

            Return MyBase.ProcessCmdKey(msg, keyData)
        End Function


        Protected Overloads Overrides Sub OnClosing(ByVal e As CancelEventArgs)
            If _result Is Nothing Then
                If _allowCancel Then
                    _result = _cancelButton.Value
                Else
                    e.Cancel = True
                    Exit Sub
                End If
            End If

            If timerTimeout IsNot Nothing Then
                timerTimeout.[Stop]()
            End If

            MyBase.OnClosing(e)
        End Sub

        Protected Overloads Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)

            If _iconImage IsNot Nothing Then
                e.Graphics.DrawIcon(_iconImage, New Rectangle(panelIcon.Location, New Size(32, 32)))
            End If
        End Sub

#End Region

#Region "Methods"

        ''' <summary>
        ''' Measures a string using the Graphics object for this form with
        ''' the specified font
        ''' </summary>
        ''' <param name="str">The string to measure</param>
        ''' <param name="maxWidth">The maximum width available to display the string</param>
        ''' <param name="font">The font with which to measure the string</param>
        ''' <returns></returns>
        Private Function MeasureString(ByVal str As String, ByVal maxWidth As Integer, ByVal font As Font) As Size
            Dim g As Graphics = Me.CreateGraphics()
            Dim strRectSizeF As SizeF = g.MeasureString(str, font, maxWidth)
            g.Dispose()

            Return New Size(CInt(Math.Ceiling(strRectSizeF.Width)), CInt(Math.Ceiling(strRectSizeF.Height)))
        End Function

        ''' <summary>
        ''' Measures a string using the Graphics object for this form and the
        ''' font of this form
        ''' </summary>
        ''' <param name="str"></param>
        ''' <param name="maxWidth"></param>
        ''' <returns></returns>
        Private Function MeasureString(ByVal str As String, ByVal maxWidth As Integer) As Size
            Return MeasureString(str, maxWidth, Me.Font)
        End Function

        ''' <summary>
        ''' Gets the longest button text
        ''' </summary>
        ''' <returns></returns>
        Private Function GetLongestButtonText() As String
            Dim maxLen As Integer = 0
            Dim maxStr As String = Nothing
            For Each button As MessageBoxExButton In _buttons
                If button.Text IsNot Nothing AndAlso button.Text.Length > maxLen Then
                    maxLen = button.Text.Length
                    maxStr = button.Text
                End If
            Next

            Return maxStr
        End Function

        ''' <summary>
        ''' Sets the size and visibility of the Message
        ''' </summary>
        Private Sub SetMessageSizeAndVisibility()
            If rtbMessage.Text Is Nothing OrElse rtbMessage.Text.Trim().Length = 0 Then
                rtbMessage.Size = Drawing.Size.Empty
                rtbMessage.Visible = False
            Else
                Dim maxWidth As Integer = _maxLayoutWidth
                If panelIcon.Size.Width <> 0 Then
                    maxWidth = maxWidth - (panelIcon.Size.Width + ICON_MESSAGE_PADDING)
                End If

                'We need to account for scroll bar width and height, otherwise for certains
                'kinds of text the scroll bar shows up unnecessarily
                maxWidth = maxWidth - SystemInformation.VerticalScrollBarWidth
                Dim messageRectSize As Size = MeasureString(rtbMessage.Text, maxWidth)

                messageRectSize.Width += SystemInformation.VerticalScrollBarWidth
                messageRectSize.Height = Math.Max(panelIcon.Height, messageRectSize.Height) + SystemInformation.HorizontalScrollBarHeight

                rtbMessage.Size = messageRectSize
                rtbMessage.Visible = True
            End If
        End Sub

        ''' <summary>
        ''' Sets the size and visibility of the Icon
        ''' </summary>
        Private Sub SetIconSizeAndVisibility()
            If _iconImage Is Nothing Then
                panelIcon.Visible = False
                panelIcon.Size = Drawing.Size.Empty
            Else
                panelIcon.Size = New Size(32, 32)
                panelIcon.Visible = True
            End If
        End Sub

        ''' <summary>
        ''' Sets the size and visibility of the save response checkbox
        ''' </summary>
        Private Sub SetCheckboxSizeAndVisibility()
            If Not AllowSaveResponse Then
                chbSaveResponse.Visible = False
                chbSaveResponse.Size = Drawing.Size.Empty
            Else
                Dim saveResponseTextSize As Size = MeasureString(chbSaveResponse.Text, _maxLayoutWidth)
                saveResponseTextSize.Width += CHECKBOX_WIDTH
                chbSaveResponse.Size = saveResponseTextSize
                chbSaveResponse.Visible = True
            End If
        End Sub

        ''' <summary>
        ''' Calculates the button size based on the text of the longest
        ''' button text
        ''' </summary>
        ''' <returns></returns>
        Private Function GetButtonSize() As Size
            Dim longestButtonText As String = GetLongestButtonText()
            If longestButtonText Is Nothing Then
                'TODO:Handle this case
            End If

            Dim buttonTextSize As Size = MeasureString(longestButtonText, _maxLayoutWidth)
            Dim buttonSize As New Size(buttonTextSize.Width + BUTTON_LEFT_PADDING + BUTTON_RIGHT_PADDING, buttonTextSize.Height + BUTTON_TOP_PADDING + BUTTON_BOTTOM_PADDING)

            If buttonSize.Width < MIN_BUTTON_WIDTH Then
                buttonSize.Width = MIN_BUTTON_WIDTH
            End If
            If buttonSize.Height < MIN_BUTTON_HEIGHT Then
                buttonSize.Height = MIN_BUTTON_HEIGHT
            End If

            Return buttonSize
        End Function

        ''' <summary>
        ''' Set the icon
        ''' </summary>
        ''' <param name="icon"></param>
        Private Sub SetStandardIcon(ByVal icon As MessageBoxIcon)
            _standardIcon = icon

            Select Case icon
                Case MessageBoxIcon.Asterisk
                    _iconImage = SystemIcons.Asterisk
                    Exit Select
                Case MessageBoxIcon.[Error]
                    _iconImage = SystemIcons.[Error]
                    Exit Select
                Case MessageBoxIcon.Exclamation
                    _iconImage = SystemIcons.Exclamation
                    Exit Select
                Case MessageBoxIcon.Hand
                    _iconImage = SystemIcons.Hand
                    Exit Select
                Case MessageBoxIcon.Information
                    _iconImage = SystemIcons.Information
                    Exit Select
                Case MessageBoxIcon.Question
                    _iconImage = SystemIcons.Question
                    Exit Select
                Case MessageBoxIcon.Warning
                    _iconImage = SystemIcons.Warning
                    Exit Select
                Case MessageBoxIcon.None
                    _iconImage = Nothing
                    Exit Select
            End Select
        End Sub

        Private Sub AddOkButtonIfNoButtonsPresent()
            If _buttons.Count = 0 Then

                Dim okButton As New MessageBoxExButton()
                okButton.Text = MessageBoxExButtons.Ok.ToString()
                okButton.Value = MessageBoxExButtons.Ok.ToString()

                _buttons.Add(okButton)

                If _Exception IsNot Nothing Then

                    Dim detailsButton As New MessageBoxExButton()
                    detailsButton.Text = "Detaliau"
                    detailsButton.Value = "Detaliau"
                    _buttons.Add(detailsButton)

                    If _BaseException Is Nothing Then
                        rtbMessage.Text = _Exception.Message
                        _iconImage = My.Resources.Flameia_Xrabbit_Tools_Activity_Monitor
                    Else
                        rtbMessage.Text = _BaseException.Message
                        If TypeOf _BaseException Is System.Security.SecurityException Then
                            _iconImage = My.Resources.Flameia_Xrabbit_General_Private
                        ElseIf _BaseException.GetType.FullName = GetType(Exception).FullName Then
                            _iconImage = My.Resources.Flameia_Xrabbit_Device_Network
                        Else
                            _iconImage = My.Resources.Flameia_Xrabbit_Tools_Activity_Monitor
                        End If
                    End If

                    Me.Text = "Klaida."

                End If
            End If
        End Sub

        ''' <summary>
        ''' Centers the form on the screen
        ''' </summary>
        Private Sub CenterForm()
            Dim x As Integer = (SystemInformation.WorkingArea.Width - Me.Width) / 2
            Dim y As Integer = (SystemInformation.WorkingArea.Height - Me.Height) / 2

            Me.Location = New Point(x, y)
        End Sub

        ''' <summary>
        ''' Sets the optimum size for the form based on the controls that
        ''' need to be displayed
        ''' </summary>
        Private Sub SetOptimumSize()
            Dim ncWidth As Integer = Me.Width - Me.ClientSize.Width
            Dim ncHeight As Integer = Me.Height - Me.ClientSize.Height

            Dim iconAndMessageRowWidth As Integer = rtbMessage.Width + ICON_MESSAGE_PADDING + panelIcon.Width
            Dim saveResponseRowWidth As Integer = chbSaveResponse.Width + CInt((panelIcon.Width / 2))
            Dim buttonsRowWidth As Integer = GetWidthOfAllButtons()
            Dim captionWidth As Integer = GetCaptionSize().Width

            Dim maxItemWidth As Integer = Math.Max(saveResponseRowWidth, Math.Max(iconAndMessageRowWidth, buttonsRowWidth))

            Dim requiredWidth As Integer = LEFT_PADDING + maxItemWidth + RIGHT_PADDING + ncWidth
            'Since Caption width is not client width, we do the check here
            If requiredWidth < captionWidth Then
                requiredWidth = captionWidth
            End If

            Dim requiredHeight As Integer = TOP_PADDING + Math.Max(rtbMessage.Height, panelIcon.Height) + ITEM_PADDING + chbSaveResponse.Height + ITEM_PADDING + GetButtonSize().Height + BOTTOM_PADDING + ncHeight

            'Fix the bug where if the message text is huge then the buttons are overwritten.
            'Incase the required height is more than the max height then adjust that in the
            'message height
            If requiredHeight > _maxHeight Then
                rtbMessage.Height -= requiredHeight - _maxHeight
            End If

            Dim height As Integer = Math.Min(requiredHeight, _maxHeight)
            Dim width As Integer = Math.Min(requiredWidth, _maxWidth)
            Me.Size = New Size(width, height)
        End Sub

        ''' <summary>
        ''' Returns the width that will be occupied by all buttons including
        ''' the inter-button padding
        ''' </summary>
        Private Function GetWidthOfAllButtons() As Integer
            Dim buttonSize As Size = GetButtonSize()
            Dim allButtonsWidth As Integer = buttonSize.Width * _buttons.Count + BUTTON_PADDING * (_buttons.Count - 1)

            Return allButtonsWidth
        End Function

        ''' <summary>
        ''' Gets the width of the caption
        ''' </summary>
        Private Function GetCaptionSize() As Size
            Dim captionFont As Font = GetCaptionFont()
            If captionFont Is Nothing Then
                'some error occured while determining system font
                captionFont = New Font("Tahoma", 11)
            End If

            Dim availableWidth As Integer = _maxWidth - SystemInformation.CaptionButtonSize.Width - SystemInformation.Border3DSize.Width * 2
            Dim captionSize As Size = MeasureString(Me.Text, availableWidth, captionFont)

            captionSize.Width += SystemInformation.CaptionButtonSize.Width + SystemInformation.Border3DSize.Width * 2
            Return captionSize
        End Function

        ''' <summary>
        ''' Layout all the controls 
        ''' </summary>
        Private Sub LayoutControls()
            panelIcon.Location = New Point(LEFT_PADDING, TOP_PADDING)
            rtbMessage.Location = New Point(LEFT_PADDING + panelIcon.Width + ICON_MESSAGE_PADDING * (IIf(panelIcon.Width = 0, 0, 1)), TOP_PADDING)

            chbSaveResponse.Location = New Point(LEFT_PADDING + CInt((panelIcon.Width / 2)), TOP_PADDING + Math.Max(panelIcon.Height, rtbMessage.Height) + ITEM_PADDING)

            Dim buttonSize As Size = GetButtonSize()
            Dim allButtonsWidth As Integer = GetWidthOfAllButtons()

            Dim firstButtonX As Integer = (CInt((Me.ClientSize.Width - allButtonsWidth)) / 2)
            Dim firstButtonY As Integer = Me.ClientSize.Height - BOTTOM_PADDING - buttonSize.Height
            Dim nextButtonLocation As New Point(firstButtonX, firstButtonY)

            Dim foundDefaultButton As Boolean = False
            For Each button As MessageBoxExButton In _buttons
                Dim buttonCtrl As Button = GetButton(button, buttonSize, nextButtonLocation)

                If Not foundDefaultButton Then
                    _defaultButtonControl = buttonCtrl
                    foundDefaultButton = True
                End If

                nextButtonLocation.X += buttonSize.Width + BUTTON_PADDING
            Next
        End Sub

        ''' <summary>
        ''' Gets the button control for the specified MessageBoxExButton, if the
        ''' control has not been created this method creates the control
        ''' </summary>
        ''' <param name="button"></param>
        ''' <param name="size"></param>
        ''' <param name="location"></param>
        ''' <returns></returns>
        Private Function GetButton(ByVal button As MessageBoxExButton, ByVal size As Size, ByVal location As Point) As Button
            Dim buttonCtrl As Button = Nothing
            If _buttonControlsTable.ContainsKey(button) Then
                buttonCtrl = TryCast(_buttonControlsTable(button), Button)
                buttonCtrl.Size = size
                buttonCtrl.Location = location
            Else
                buttonCtrl = CreateButton(button, size, location)
                _buttonControlsTable(button) = buttonCtrl
                Me.Controls.Add(buttonCtrl)
            End If

            Return buttonCtrl
        End Function

        ''' <summary>
        ''' Creates a button control based on info from MessageBoxExButton
        ''' </summary>
        ''' <param name="button"></param>
        ''' <param name="size"></param>
        ''' <param name="location"></param>
        ''' <returns></returns>
        Private Function CreateButton(ByVal button As MessageBoxExButton, ByVal size As Size, ByVal location As Point) As Button
            Dim buttonCtrl As New Button()
            buttonCtrl.Size = size
            buttonCtrl.Text = button.Text
            buttonCtrl.TextAlign = ContentAlignment.MiddleCenter
            buttonCtrl.FlatStyle = FlatStyle.System
            If button.HelpText IsNot Nothing AndAlso button.HelpText.Trim().Length <> 0 Then
                buttonToolTip.SetToolTip(buttonCtrl, button.HelpText)
            End If
            buttonCtrl.Location = location
            AddHandler buttonCtrl.Click, AddressOf OnButtonClicked
            buttonCtrl.Tag = button.Value

            Return buttonCtrl
        End Function

        Private Sub DisableCloseIfMultipleButtonsAndNoCancelButton()
            If _buttons.Count > 1 Then
                If _cancelButton IsNot Nothing Then
                    Exit Sub
                End If

                'See if standard cancel button is present
                For Each button As MessageBoxExButton In _buttons
                    If button.Text = MessageBoxExButtons.Cancel.ToString() AndAlso button.Value = MessageBoxExButtons.Cancel.ToString() Then
                        _cancelButton = button
                        Exit Sub
                    End If
                Next

                'Standard cancel button is not present, Disable
                'close button
                DisableCloseButton(Me)

                _allowCancel = False
            ElseIf _buttons.Count = 1 Then
                _cancelButton = TryCast(_buttons(0), MessageBoxExButton)
            Else
                'This condition should never get called
                _allowCancel = False
            End If
        End Sub

        ''' <summary>
        ''' Plays the alert sound based on the icon set for the message box
        ''' </summary>
        Private Sub PlayAlert()
            If _playAlert Then
                If _standardIcon <> MessageBoxIcon.None Then
                    MessageBeep(CUInt(_standardIcon))
                Else
                    'MB_OK
                    MessageBeep(0)
                End If
            End If
        End Sub

        Private Sub SelectDefaultButton()
            If _defaultButtonControl IsNot Nothing Then
                _defaultButtonControl.[Select]()
            End If
        End Sub

        Private Sub StartTimerIfTimeoutGreaterThanZero()
            If _timeout > 0 Then
                If timerTimeout Is Nothing Then
                    timerTimeout = New Timer(Me.components)
                    AddHandler timerTimeout.Tick, AddressOf timerTimeout_Tick
                End If

                If Not timerTimeout.Enabled Then
                    timerTimeout.Interval = _timeout
                    timerTimeout.Start()
                End If
            End If
        End Sub

        Private Sub SetResultAndClose(ByVal result As String)
            _result = result
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End Sub

#End Region

#Region "Event Handlers"

        Private Sub OnButtonClicked(ByVal sender As Object, ByVal e As EventArgs)
            Dim btn As Button = TryCast(sender, Button)
            If btn Is Nothing OrElse btn.Tag Is Nothing Then
                Exit Sub
            End If

            Dim result As String = TryCast(btn.Tag, String)

            If _Exception IsNot Nothing AndAlso result = "Detaliau" Then
                Dim frm As New MessageBoxExDetails
                frm._Exception = _Exception
                frm.ShowDialog()
                Exit Sub
            End If

            SetResultAndClose(result)
        End Sub

        Private Sub timerTimeout_Tick(ByVal sender As Object, ByVal e As EventArgs)
            timerTimeout.[Stop]()

            Select Case _timeoutResult
                Case TimeoutResult.[Default]
                    _defaultButtonControl.PerformClick()
                    Exit Select

                Case TimeoutResult.Cancel
                    If _cancelButton IsNot Nothing Then
                        SetResultAndClose(_cancelButton.Value)
                    Else
                        _defaultButtonControl.PerformClick()
                    End If
                    Exit Select

                Case TimeoutResult.Timeout
                    SetResultAndClose(MessageBoxExResult.Timeout)
                    Exit Select
            End Select
        End Sub

#End Region

#Region "P/Invoke - SystemParametersInfo, GetSystemMenu, EnableMenuItem, MessageBeep"

        Private Function GetCaptionFont() As Font

            Dim ncm As New NONCLIENTMETRICS()
            ncm.cbSize = Marshal.SizeOf(GetType(NONCLIENTMETRICS))
            Try
                Dim result As Boolean = SystemParametersInfo(SPI_GETNONCLIENTMETRICS, ncm.cbSize, ncm, 0)

                If result Then

                    Return Drawing.Font.FromLogFont(ncm.lfCaptionFont)
                Else
                    Dim lastError As Integer = Marshal.GetLastWin32Error()
                    Return Nothing
                End If
            Catch generatedExceptionName As Exception
                'ex
                'System.Console.WriteLine(ex.Message);
            End Try

            Return Nothing
        End Function

        Private Const SPI_GETNONCLIENTMETRICS As Integer = 41
        Private Const LF_FACESIZE As Integer = 32

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
        Private Structure LOGFONT
            Public lfHeight As Integer
            Public lfWidth As Integer
            Public lfEscapement As Integer
            Public lfOrientation As Integer
            Public lfWeight As Integer
            Public lfItalic As Byte
            Public lfUnderline As Byte
            Public lfStrikeOut As Byte
            Public lfCharSet As Byte
            Public lfOutPrecision As Byte
            Public lfClipPrecision As Byte
            Public lfQuality As Byte
            Public lfPitchAndFamily As Byte
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
            Public lfFaceSize As String
        End Structure

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
        Private Structure NONCLIENTMETRICS
            Public cbSize As Integer
            Public iBorderWidth As Integer
            Public iScrollWidth As Integer
            Public iScrollHeight As Integer
            Public iCaptionWidth As Integer
            Public iCaptionHeight As Integer
            Public lfCaptionFont As LOGFONT
            Public iSmCaptionWidth As Integer
            Public iSmCaptionHeight As Integer
            Public lfSmCaptionFont As LOGFONT
            Public iMenuWidth As Integer
            Public iMenuHeight As Integer
            Public lfMenuFont As LOGFONT
            Public lfStatusFont As LOGFONT
            Public lfMessageFont As LOGFONT
        End Structure

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Private Shared Function SystemParametersInfo(ByVal uiAction As Integer, ByVal uiParam As Integer, ByRef ncMetrics As NONCLIENTMETRICS, ByVal fWinIni As Integer) As Boolean
        End Function


        <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
        Private Shared Function GetSystemMenu(ByVal hWnd As IntPtr, ByVal bRevert As Boolean) As IntPtr
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
        Private Shared Function EnableMenuItem(ByVal hMenu As IntPtr, ByVal uIDEnableItem As UInteger, ByVal uEnable As UInteger) As Boolean
        End Function

        Private Const SC_CLOSE As Integer = &HF060
        Private Const MF_BYCOMMAND As Integer = &H0
        Private Const MF_GRAYED As Integer = &H1
        Private Const MF_ENABLED As Integer = &H0

        Private Sub DisableCloseButton(ByVal form As Form)
            Try
                EnableMenuItem(GetSystemMenu(form.Handle, False), SC_CLOSE, MF_BYCOMMAND Or MF_GRAYED)
            Catch generatedExceptionName As Exception
                'ex
                'System.Console.WriteLine(ex.Message);
            End Try
        End Sub

        <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
        Private Shared Function MessageBeep(ByVal type As UInteger) As Boolean
        End Function

#End Region

    End Class

End Namespace