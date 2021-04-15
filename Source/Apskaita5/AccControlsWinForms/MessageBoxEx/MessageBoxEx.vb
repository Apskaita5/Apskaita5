Imports System
Imports System.Drawing
Imports System.Windows.Forms
Namespace MessageBoxExLib
    ''' <summary>
    ''' An extended MessageBox with lot of customizing capabilities.
    ''' (c) http://www.codeproject.com/Articles/9656/Dissecting-the-MessageBox
    ''' </summary>
    Public Class MessageBoxEx

#Region "Fields"
        Private _msgBox As New MessageBoxExForm()

        Private _useSavedResponse As Boolean = True
        Private _name As String = Nothing
#End Region

#Region "Properties"

        Friend Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        ''' <summary>
        ''' Sets the caption of the message box
        ''' </summary>
        Public WriteOnly Property Caption() As String
            Set(ByVal value As String)
                _msgBox.Caption = value
            End Set
        End Property

        ''' <summary>
        ''' Sets the text of the message box
        ''' </summary>
        Public WriteOnly Property Text() As String
            Set(ByVal value As String)
                _msgBox.Message = value
            End Set
        End Property

        ''' <summary>
        ''' Sets the icon to show in the message box
        ''' </summary>
        Public WriteOnly Property CustomIcon() As Icon
            Set(ByVal value As Icon)
                _msgBox.CustomIcon = value
            End Set
        End Property

        ''' <summary>
        ''' Sets the icon to show in the message box
        ''' </summary>
        Public WriteOnly Property Icon() As MessageBoxExIcon
            Set(ByVal value As MessageBoxExIcon)
                _msgBox.StandardIcon = DirectCast([Enum].Parse(GetType(MessageBoxIcon), value.ToString()), MessageBoxIcon)
            End Set
        End Property

        ''' <summary>
        ''' Sets the font for the text of the message box
        ''' </summary>
        Public WriteOnly Property Font() As Font
            Set(ByVal value As Font)
                _msgBox.Font = value
            End Set
        End Property

        ''' <summary>
        ''' Sets or Gets the ability of the user to save his/her response
        ''' </summary>
        Public Property AllowSaveResponse() As Boolean
            Get
                Return _msgBox.AllowSaveResponse
            End Get
            Set(ByVal value As Boolean)
                _msgBox.AllowSaveResponse = value
            End Set
        End Property

        ''' <summary>
        ''' Sets the text to show to the user when saving his/her response
        ''' </summary>
        Public WriteOnly Property SaveResponseText() As String
            Set(ByVal value As String)
                _msgBox.SaveResponseText = value
            End Set
        End Property

        ''' <summary>
        ''' Sets the exception to show to the user
        ''' </summary>
        Public WriteOnly Property [Exception]() As Exception
            Set(ByVal value As Exception)
                _msgBox.Exception = value
            End Set
        End Property

        ''' <summary>
        ''' Sets the base exception to show to the user
        ''' </summary>
        Public WriteOnly Property BaseException() As Exception
            Set(ByVal value As Exception)
                _msgBox.BaseException = value
            End Set
        End Property

        Public WriteOnly Property BusinessObject() As Object
            Set(ByVal value As Object)
                _msgBox.BusinessObject = value
            End Set
        End Property

        ''' <summary>
        ''' Sets or Gets wether the saved response if available should be used
        ''' </summary>
        Public Property UseSavedResponse() As Boolean
            Get
                Return _useSavedResponse
            End Get
            Set(ByVal value As Boolean)
                _useSavedResponse = value
            End Set
        End Property

        ''' <summary>
        ''' Sets or Gets wether an alert sound is played while showing the message box.
        ''' The sound played depends on the the Icon selected for the message box
        ''' </summary>
        Public Property PlayAlsertSound() As Boolean
            Get
                Return _msgBox.PlayAlertSound
            End Get
            Set(ByVal value As Boolean)
                _msgBox.PlayAlertSound = value
            End Set
        End Property

        ''' <summary>
        ''' Sets or Gets the time in milliseconds for which the message box is displayed.
        ''' </summary>
        Public Property Timeout() As Integer
            Get
                Return _msgBox.Timeout
            End Get
            Set(ByVal value As Integer)
                _msgBox.Timeout = value
            End Set
        End Property

        ''' <summary>
        ''' Controls the result that will be returned when the message box times out.
        ''' </summary>
        Public Property TimeoutResult() As TimeoutResult
            Get
                Return _msgBox.TimeoutResult
            End Get
            Set(ByVal value As TimeoutResult)
                _msgBox.TimeoutResult = value
            End Set
        End Property

#End Region

#Region "Methods"

        ''' <summary>
        ''' Shows the message box
        ''' </summary>
        ''' <returns></returns>
        Public Function Show() As String
            Return Show(Nothing)
        End Function

        ''' <summary>
        ''' Shows the messsage box with the specified owner
        ''' </summary>
        ''' <param name="owner"></param>
        ''' <returns></returns>
        Public Function Show(ByVal owner As IWin32Window) As String
            If _useSavedResponse AndAlso Me.Name IsNot Nothing Then
                Dim savedResponse As String = MessageBoxExManager.GetSavedResponse(Me)
                If savedResponse IsNot Nothing Then
                    Return savedResponse
                End If
            End If

            If owner Is Nothing Then
                _msgBox.ShowDialog()
            Else
                _msgBox.ShowDialog(owner)
            End If

            If Me.Name IsNot Nothing Then
                If _msgBox.AllowSaveResponse AndAlso _msgBox.SaveResponse Then
                    MessageBoxExManager.SetSavedResponse(Me, _msgBox.Result)
                Else
                    MessageBoxExManager.ResetSavedResponse(Me.Name)
                End If
            Else
                Dispose()
            End If

            Return _msgBox.Result
        End Function

        ''' <summary>
        ''' Add a custom button to the message box
        ''' </summary>
        ''' <param name="button">The button to add</param>
        Public Sub AddButton(ByVal button As MessageBoxExButton)
            If button Is Nothing Then
                Throw New ArgumentNullException("button", "A null button cannot be added")
            End If

            _msgBox.Buttons.Add(button)

            If button.IsCancelButton Then
                _msgBox.CustomCancelButton = button
            End If
        End Sub

        ''' <summary>
        ''' Add a custom button to the message box
        ''' </summary>
        ''' <param name="text">The text of the button</param>
        ''' <param name="val">The return value in case this button is clicked</param>
        Public Sub AddButton(ByVal text As String, ByVal val As String)
            If text Is Nothing Then
                Throw New ArgumentNullException("text", "Text of a button cannot be null")
            End If

            If val Is Nothing Then
                Throw New ArgumentNullException("val", "Value of a button cannot be null")
            End If

            Dim button As New MessageBoxExButton()
            button.Text = text
            button.Value = val

            AddButton(button)
        End Sub

        ''' <summary>
        ''' Add a standard button to the message box
        ''' </summary>
        ''' <param name="button">The standard button to add</param>
        Public Sub AddButton(ByVal button As MessageBoxExButtons)
            Dim buttonText As String
            Select Case button
                Case MessageBoxExButtons.Abort
                    buttonText = "Abort"
                Case MessageBoxExButtons.Cancel
                    buttonText = "Ataukti"
                Case MessageBoxExButtons.Ignore
                    buttonText = "Ignore"
                Case MessageBoxExButtons.Yes
                    buttonText = "Taip"
                Case MessageBoxExButtons.No
                    buttonText = "Ne"
                Case MessageBoxExButtons.Ok
                    buttonText = "Ok"
                Case Else
                    buttonText = "Retry"
            End Select

            If buttonText Is Nothing Then
                buttonText = button.ToString()
            End If

            Dim buttonVal As String = buttonText

            Dim btn As New MessageBoxExButton()
            btn.Text = buttonText
            btn.Value = buttonVal

            If button = MessageBoxExButtons.Cancel Then
                btn.IsCancelButton = True
            End If

            AddButton(btn)
        End Sub

        ''' <summary>
        ''' Add standard buttons to the message box.
        ''' </summary>
        ''' <param name="buttons">The standard buttons to add</param>
        Public Sub AddButtons(ByVal buttons As MessageBoxButtons)
            Select Case buttons
                Case MessageBoxButtons.OK
                    AddButton(MessageBoxExButtons.Ok)
                    Exit Select

                Case MessageBoxButtons.AbortRetryIgnore
                    AddButton(MessageBoxExButtons.Abort)
                    AddButton(MessageBoxExButtons.Retry)
                    AddButton(MessageBoxExButtons.Ignore)
                    Exit Select

                Case MessageBoxButtons.OKCancel
                    AddButton(MessageBoxExButtons.Ok)
                    AddButton(MessageBoxExButtons.Cancel)
                    Exit Select

                Case MessageBoxButtons.RetryCancel
                    AddButton(MessageBoxExButtons.Retry)
                    AddButton(MessageBoxExButtons.Cancel)
                    Exit Select

                Case MessageBoxButtons.YesNo
                    AddButton(MessageBoxExButtons.Yes)
                    AddButton(MessageBoxExButtons.No)
                    Exit Select

                Case MessageBoxButtons.YesNoCancel
                    AddButton(MessageBoxExButtons.Yes)
                    AddButton(MessageBoxExButtons.No)
                    AddButton(MessageBoxExButtons.Cancel)
                    Exit Select
            End Select
        End Sub

#End Region

#Region "Ctor"

        ''' <summary>
        ''' Ctor is internal because this can only be created by MBManager
        ''' </summary>
        Friend Sub New()
        End Sub

        ''' <summary>
        ''' Called by the manager when it is disposed
        ''' </summary>
        Friend Sub Dispose()
            If _msgBox IsNot Nothing Then
                _msgBox.Dispose()
            End If
        End Sub

#End Region

    End Class

End Namespace
