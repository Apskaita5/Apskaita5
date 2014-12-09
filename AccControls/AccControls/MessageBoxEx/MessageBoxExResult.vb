Imports System
Imports System.IO
Imports System.Collections
Namespace MessageBoxExLib
    ''' <summary>
    ''' Standard MessageBoxEx results
    ''' (c) http://www.codeproject.com/Articles/9656/Dissecting-the-MessageBox
    ''' </summary>
    Public Structure MessageBoxExResult
        Public Const Ok As String = "Ok"
        Public Const Cancel As String = "Cancel"
        Public Const Yes As String = "Yes"
        Public Const No As String = "No"
        Public Const Abort As String = "Abort"
        Public Const Retry As String = "Retry"
        Public Const Ignore As String = "Ignore"
        Public Const Timeout As String = "Timeout"
        Private GatesWishes As String
    End Structure
End Namespace