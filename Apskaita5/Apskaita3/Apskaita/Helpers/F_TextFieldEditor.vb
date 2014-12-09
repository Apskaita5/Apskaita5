Public Class F_TextFieldEditor
    Private _FormHeaderText As String
    Private _TextField As String
    Private _AcceptChanges As Boolean = False
    Private _IsReadOnly As Boolean

    Public Sub New(ByVal nFormHeaderText As String, ByVal nTextField As String, _
        ByVal nIsReadOnly As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _FormHeaderText = nFormHeaderText
        _TextField = nTextField
        _IsReadOnly = nIsReadOnly

    End Sub

    Public ReadOnly Property TextField() As String
        Get
            Return _TextField
        End Get
    End Property

    Public ReadOnly Property AcceptChanges() As Boolean
        Get
            Return _AcceptChanges
        End Get
    End Property



    Private Sub F_TextFieldEditor_FormClosed(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        If Me.DialogResult <> Windows.Forms.DialogResult.OK Then _
            Me.DialogResult = Windows.Forms.DialogResult.Cancel

    End Sub

    Private Sub F_TextFieldEditor_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = _FormHeaderText
        TextFieldTextBox.Text = _TextField
        TextFieldTextBox.ReadOnly = _IsReadOnly

    End Sub

    Private Sub OkButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles OkButton.Click

        If _IsReadOnly Then
            _AcceptChanges = False
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Else
            _AcceptChanges = True
            _TextField = TextFieldTextBox.Text
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
        
        Me.Hide()
        Me.Close()

    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles CancelButton.Click

        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Hide()
        Me.Close()

    End Sub

End Class