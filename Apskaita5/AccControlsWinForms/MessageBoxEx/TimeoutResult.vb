Imports System
Namespace MessageBoxExLib
    ''' <summary>
    ''' Enumerates the kind of results that can be returned when a
    ''' message box times out
    ''' (c) http://www.codeproject.com/Articles/9656/Dissecting-the-MessageBox
    ''' </summary>
    Public Enum TimeoutResult
        ''' <summary>
        ''' On timeout the value associated with the default button is set as the result.
        ''' This is the default action on timeout.
        ''' </summary>
        [Default]

        ''' <summary>
        ''' On timeout the value associated with the cancel button is set as the result. If
        ''' the messagebox does not have a cancel button then the value associated with 
        ''' the default button is set as the result.
        ''' </summary>
        Cancel

        ''' <summary>
        ''' On timeout MessageBoxExResult.Timeout is set as the result.
        ''' </summary>
        Timeout
    End Enum
End Namespace