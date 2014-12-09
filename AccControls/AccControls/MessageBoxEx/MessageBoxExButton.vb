Imports System
Namespace MessageBoxExLib
    ''' <summary>
    ''' Internal DataStructure used to represent a button
    ''' (c) http://www.codeproject.com/Articles/9656/Dissecting-the-MessageBox
    ''' </summary>
    Public Class MessageBoxExButton
        Private _text As String = Nothing
        ''' <summary>
        ''' Gets or Sets the text of the button
        ''' </summary>
        Public Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal value As String)
                _text = value
            End Set
        End Property

        Private _value As String = Nothing
        ''' <summary>
        ''' Gets or Sets the return value when this button is clicked
        ''' </summary>
        Public Property Value() As String
            Get
                Return _value
            End Get
            Set(ByVal value As String)
                _value = value
            End Set
        End Property

        Private _helpText As String = Nothing
        ''' <summary>
        ''' Gets or Sets the tooltip that is displayed for this button
        ''' </summary>
        Public Property HelpText() As String
            Get
                Return _helpText
            End Get
            Set(ByVal value As String)
                _helpText = value
            End Set
        End Property

        Private _isCancelButton As Boolean = False
        ''' <summary>
        ''' Gets or Sets wether this button is a cancel button. i.e. the button
        ''' that will be assumed to have been clicked if the user closes the message box
        ''' without pressing any button.
        ''' </summary>
        Public Property IsCancelButton() As Boolean
            Get
                Return _isCancelButton
            End Get
            Set(ByVal value As Boolean)
                _isCancelButton = value
            End Set
        End Property
    End Class
End Namespace