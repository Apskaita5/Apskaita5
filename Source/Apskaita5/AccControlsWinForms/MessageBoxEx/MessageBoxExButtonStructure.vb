''' <summary>
''' Describes a button to be used in Ask dialog (module AccControls).
''' (c) http://www.codeproject.com/Articles/9656/Dissecting-the-MessageBox
''' </summary>
Public Structure ButtonStructure

    Public ButtonText As String
    Public Buttoncomment As String

    ''' <summary>
    ''' Creates a new instance of a structure.
    ''' </summary>
    ''' <param name="nButtonText">Text to be displayed on the button.</param>
    ''' <param name="nButtonComment">Comment to be displayed when user hovers mouse over the button.</param>
    Public Sub New(ByVal nButtonText As String, Optional ByVal nButtonComment As String = "")
        ButtonText = nButtonText
        Buttoncomment = nButtonComment
    End Sub

End Structure
