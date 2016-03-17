Imports System
Imports System.IO
Imports System.Collections
Imports System.Resources
Imports System.Reflection
Namespace MessageBoxExLib

    ''' <summary>
    ''' Manages a collection of MessageBoxes. Basically manages the
    ''' saved response handling for messageBoxes.
    ''' (c) http://www.codeproject.com/Articles/9656/Dissecting-the-MessageBox
    ''' </summary>
    Public Class MessageBoxExManager

#Region "Fields"
        Private Shared _messageBoxes As New Hashtable()
        Private Shared _savedResponses As New Hashtable()

        Private Shared _standardButtonsText As New Hashtable()
#End Region

#Region "Static ctor"
        Shared Sub New()
            _standardButtonsText(MessageBoxExButtons.Ok.ToString()) = "Ok"
            _standardButtonsText(MessageBoxExButtons.Cancel.ToString()) = "Atšaukti"
            _standardButtonsText(MessageBoxExButtons.Yes.ToString()) = "Taip"
            _standardButtonsText(MessageBoxExButtons.No.ToString()) = "Ne"
            _standardButtonsText(MessageBoxExButtons.Abort.ToString()) = "Abort"
            _standardButtonsText(MessageBoxExButtons.Retry.ToString()) = "Retry"
            _standardButtonsText(MessageBoxExButtons.Ignore.ToString()) = "Ignore"
        End Sub
#End Region

#Region "Methods"

        ''' <summary>
        ''' Creates a new message box with the specified name. If null is specified
        ''' in the message name then the message box is not managed by the Manager and
        ''' will be disposed automatically after a call to Show()
        ''' </summary>
        ''' <param name="name">The name of the message box</param>
        ''' <returns>A new message box</returns>
        Public Shared Function CreateMessageBox(ByVal name As String) As MessageBoxEx
            If name IsNot Nothing AndAlso _messageBoxes.ContainsKey(name) Then
                Dim err As String = String.Format("A MessageBox with the name {0} already exists.", name)
                Throw New ArgumentException(err, "name")
            End If

            Dim msgBox As New MessageBoxEx()
            msgBox.Name = name
            If msgBox.Name IsNot Nothing Then
                _messageBoxes(name) = msgBox
            End If

            Return msgBox
        End Function

        ''' <summary>
        ''' Gets the message box with the specified name
        ''' </summary>
        ''' <param name="name">The name of the message box to retrieve</param>
        ''' <returns>The message box with the specified name or null if a message box
        ''' with that name does not exist</returns>
        Public Shared Function GetMessageBox(ByVal name As String) As MessageBoxEx
            If _messageBoxes.Contains(name) Then
                Return TryCast(_messageBoxes(name), MessageBoxEx)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Deletes the message box with the specified name
        ''' </summary>
        ''' <param name="name">The name of the message box to delete</param>
        Public Shared Sub DeleteMessageBox(ByVal name As String)
            If name Is Nothing Then
                Exit Sub
            End If

            If _messageBoxes.Contains(name) Then
                Dim msgBox As MessageBoxEx = TryCast(_messageBoxes(name), MessageBoxEx)
                msgBox.Dispose()
                _messageBoxes.Remove(name)
            End If
        End Sub

        Public Shared Sub WriteSavedResponses(ByVal stream As Stream)
            Throw New NotImplementedException("This feature has not yet been implemented")
        End Sub

        Public Shared Sub ReadSavedResponses(ByVal stream As Stream)
            Throw New NotImplementedException("This feature has not yet been implemented")
        End Sub

        ''' <summary>
        ''' Reset the saved response for the message box with the specified name.
        ''' </summary>
        ''' <param name="messageBoxName">The name of the message box whose response is to be reset.</param>
        Public Shared Sub ResetSavedResponse(ByVal messageBoxName As String)
            If messageBoxName Is Nothing Then
                Exit Sub
            End If

            If _savedResponses.ContainsKey(messageBoxName) Then
                _savedResponses.Remove(messageBoxName)
            End If
        End Sub

        ''' <summary>
        ''' Resets the saved responses for all message boxes that are managed by the manager.
        ''' </summary>
        Public Shared Sub ResetAllSavedResponses()
            _savedResponses.Clear()
        End Sub

#End Region

#Region "Internal Methods"

        ''' <summary>
        ''' Set the saved response for the specified message box
        ''' </summary>
        ''' <param name="msgBox">The message box whose response is to be set</param>
        ''' <param name="response">The response to save for the message box</param>
        Friend Shared Sub SetSavedResponse(ByVal msgBox As MessageBoxEx, ByVal response As String)
            If msgBox.Name Is Nothing Then
                Exit Sub
            End If

            _savedResponses(msgBox.Name) = response
        End Sub

        ''' <summary>
        ''' Gets the saved response for the specified message box
        ''' </summary>
        ''' <param name="msgBox">The message box whose saved response is to be retrieved</param>
        ''' <returns>The saved response if exists, null otherwise</returns>
        Friend Shared Function GetSavedResponse(ByVal msgBox As MessageBoxEx) As String
            Dim msgBoxName As String = msgBox.Name
            If msgBoxName Is Nothing Then
                Return Nothing
            End If

            If _savedResponses.ContainsKey(msgBoxName) Then
                Return _savedResponses(msgBox.Name).ToString()
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Returns the localized string for standard button texts like,
        ''' "Ok", "Cancel" etc.
        ''' </summary>
        ''' <param name="key"></param>
        ''' <returns></returns>
        Friend Shared Function GetLocalizedString(ByVal key As String) As String
            If _standardButtonsText.ContainsKey(key) Then
                Return DirectCast(_standardButtonsText(key), String)
            Else
                Return Nothing
            End If
        End Function

#End Region

    End Class

End Namespace