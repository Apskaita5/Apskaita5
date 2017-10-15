Imports AccControlsWinForms.MessageBoxExLib
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Text

Public Module CommonMethods

    ''' <summary>
    ''' A callback delegate that is invoked when an async operation finishes
    ''' execution.
    ''' </summary>
    ''' <param name="result">a result returned by the async method</param>
    ''' <param name="ex">an exception thrown by the async method (if any)</param>
    ''' <remarks></remarks>
    Public Delegate Sub AsyncOperationCallback(ByVal result As Object, ByVal ex As Exception)


    ''' <summary>
    ''' Creates a modal dialog, asks user a question and provides a list of possible answers as buttons.
    ''' </summary>
    ''' <param name="Question">The text of the question of the dialog.</param>
    ''' <param name="buttons">List of possible answers as button params (used as New ButtonStructure())</param>
    ''' <returns>The text on the button pressed by user. </returns>
    Public Function Ask(ByVal question As String, ByVal ParamArray buttons As ButtonStructure()) As String

        Dim msgBox As MessageBoxEx = MessageBoxExManager.CreateMessageBox(Nothing)

        msgBox.Caption = "Nepykite už durnus klausimus, bet..."
        msgBox.Text = question

        For Each s As ButtonStructure In buttons

            Dim button As New MessageBoxExButton

            button.Text = s.ButtonText
            button.HelpText = s.Buttoncomment
            button.Value = s.ButtonText

            msgBox.AddButton(button)

        Next

        msgBox.Icon = MessageBoxExIcon.Question

        Return msgBox.Show

    End Function

    ''' <summary>
    ''' Creates a modal dialog, asks user a question and provides an alternative Yes/No as buttons.
    ''' </summary>
    ''' <param name="Question">The text of the question (Yes or No) of the dialog.</param>
    ''' <returns>TRUE if user pressed "Taip" (i.e. "Yes") button, else - false. </returns>
    Public Function YesOrNo(ByVal Question As String) As Boolean

        Dim msgBox As MessageBoxEx = MessageBoxExManager.CreateMessageBox(Nothing)

        msgBox.Caption = "Nepykite už durnus klausimus, bet..."
        msgBox.Text = Question

        msgBox.AddButtons(Windows.Forms.MessageBoxButtons.YesNo)

        msgBox.Icon = MessageBoxExIcon.Question

        Dim result As String = msgBox.Show()

        Return (result = "Taip")

    End Function

    ''' <summary>
    ''' Provides a dialog with short/full info about the exception provided.
    ''' </summary>
    Public Sub ShowError(ByVal nException As Exception)

        Dim msgBox As MessageBoxEx = MessageBoxExManager.CreateMessageBox(Nothing)

        msgBox.Caption = "Ir atleiskite nusidėjėliams už jų kaltes :)"

        msgBox.BaseException = GetBaseException(nException)
        msgBox.Exception = nException

        msgBox.Show()

        msgBox.Dispose()

    End Sub

    Private Function GetBaseException(ByVal ex As Exception) As Exception

        If ex Is Nothing Then Return ex

        Dim securityEx As System.Security.SecurityException = Nothing
        Dim simpleEx As Exception = Nothing
        Dim lastEx As Exception = Nothing

        GetExceptionCategories(ex, securityEx, simpleEx, lastEx)

        If Not securityEx Is Nothing Then
            Return securityEx
        ElseIf Not simpleEx Is Nothing Then
            Return simpleEx
        Else
            Return lastEx
        End If

    End Function

    Private Sub GetExceptionCategories(ByVal ex As Exception, _
        ByRef securityEx As System.Security.SecurityException, ByRef simpleEx As Exception, _
        ByRef lastEx As Exception)

        If TypeOf ex Is System.Security.SecurityException Then
            securityEx = ex
        ElseIf simpleEx Is Nothing AndAlso ex.GetType.FullName = GetType(Exception).FullName Then
            simpleEx = ex
        End If

        If ex.InnerException Is Nothing Then
            lastEx = ex
        Else
            GetExceptionCategories(ex.InnerException, securityEx, simpleEx, lastEx)
        End If

    End Sub


    ''' <summary>
    ''' Disables all controls in the target form.
    ''' </summary>
    Public Sub DisableAllControls(ByRef targetForm As Control)
        Dim ignoreTypes As Type() = {GetType(Windows.Forms.GroupBox), GetType(Windows.Forms.Label), _
            GetType(Windows.Forms.LinkLabel), GetType(Windows.Forms.Panel), _
            GetType(Windows.Forms.SplitContainer), GetType(Windows.Forms.TabControl), _
            GetType(Windows.Forms.TableLayoutPanel), GetType(Windows.Forms.TabPage), _
            GetType(Windows.Forms.Timer), GetType(Windows.Forms.TableLayoutPanel), _
            GetType(Windows.Forms.FlowLayoutPanel), GetType(System.Windows.Forms.HScrollBar), _
            GetType(System.Windows.Forms.VScrollBar)}
        For Each ctrl As Control In targetForm.Controls
            If Array.IndexOf(ignoreTypes, ctrl.GetType) < 0 Then
                Try
                    If TypeOf ctrl Is DataGridView Then
                        DirectCast(ctrl, DataGridView).ReadOnly = True
                        DirectCast(ctrl, DataGridView).AllowUserToAddRows = False
                        DirectCast(ctrl, DataGridView).AllowUserToDeleteRows = False
                    Else
                        DirectCast(ctrl, Object).Readonly = True
                    End If
                Catch ex As Exception
                    ctrl.Enabled = False
                End Try
            End If
            If ctrl.Controls.Count > 0 Then DisableAllControls(ctrl)
        Next
    End Sub


    ''' <summary>
    ''' Returns path to the folder where the program (.exe) is executing.
    ''' </summary>
    Friend Function AppPath() As String
        Return System.IO.Path.GetDirectoryName(Reflection.Assembly _
            .GetEntryAssembly().Location)
    End Function

    ''' <summary>
    ''' Gets a rounded value of d using Asymmetric Arithmetic Rounding algorithm
    ''' </summary>
    Friend Function CRound(ByVal d As Double, ByVal r As Integer) As Double
        Dim i As Long = CLng(Math.Floor(d * Math.Pow(10, r)))
        If i + 0.5 > CType(d * Math.Pow(10, r), Decimal) Then
            Return i / Math.Pow(10, r)
        Else
            Return (i + 1) / Math.Pow(10, r)
        End If
    End Function

    ''' <summary>
    ''' Gets a substring from Tab (CHR9) delimited string.
    ''' </summary>
    ''' <param name="sourceString">Tab (CHR9) delimited string.</param>
    ''' <param name="index">Number (index) of substring to retrieve.</param>
    Friend Function GetElement(ByVal sourceString As String, ByVal index As Integer) As String
        Dim subStrings As String() = sourceString.Split(Chr(9))
        If subStrings.Length > index Then
            Return subStrings(index)
        Else
            Return ""
        End If
    End Function


    ''' <summary>
    ''' Invokes a custom method synchronosly and returns a result.
    ''' </summary>
    ''' <typeparam name="T">a class which method should be invoked</typeparam>
    ''' <param name="instance">an instance of the class to invoke the method on
    ''' (if the method is static, then null)</param>
    ''' <param name="methodName">a name of the method to invoke</param>
    ''' <param name="params">params for the method to invoke (if any)</param>
    ''' <remarks></remarks>
    Public Function InvokeMethod(Of T)(ByVal instance As T, ByVal methodName As String, _
        ByVal ParamArray params As Object()) As Object
        Dim invoker As New MethodInvoker(Of T)
        If params Is Nothing OrElse params.Length < 1 Then
            Return invoker.Invoke(instance, methodName)
        Else
            Return invoker.Invoke(instance, methodName, params)
        End If
    End Function


    Public Function KeyCodeToUnicode(ByVal key As Windows.Forms.Keys) As String

        Dim keyboardState(255) As Byte
        Dim keyboardStateStatus As Boolean = GetKeyboardState(keyboardState)
        If Not keyboardStateStatus Then Return ""

        Dim virtualKeyCode As UInt32 = CType(key, UInt32)
        Dim scanCode As UInt32 = MapVirtualKey(virtualKeyCode, 0)
        Dim inputLocaleIdentifier As IntPtr = GetKeyboardLayout(0)

        Dim result As New StringBuilder()
        Dim uint0 As UInteger = 0
        ToUnicodeEx(virtualKeyCode, scanCode, keyboardState, result, DirectCast(5, Int32), uint0, inputLocaleIdentifier)

        Return result.ToString()

    End Function

    <DllImport("User32.dll")> _
    Private Function GetKeyboardState(ByVal pbKeyState As Byte()) As Boolean
    End Function

    <DllImport("User32.dll")> _
    Private Function MapVirtualKey(ByVal uCode As UInt32, ByVal uMapType As UInt32) As UInt32
    End Function

    <DllImport("user32.dll")> _
    Private Function GetKeyboardLayout(ByVal idThread As Long) As IntPtr
    End Function

    '<DllImport("user32.dll")> _
    'static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

    <DllImport("user32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True, CallingConvention:=CallingConvention.Winapi)> _
    Private Function ToUnicodeEx(ByVal wVirtKey As UInteger, ByVal wScanCode As UInteger, ByVal lpKeyState As Byte(), _
        <Out(), MarshalAs(UnmanagedType.LPWStr)> _
        ByVal pwszBuff As StringBuilder, _
        ByVal cchBuff As Integer, ByVal wFlags As UInteger, ByVal dwhkl As IntPtr) As Integer
    End Function

End Module
