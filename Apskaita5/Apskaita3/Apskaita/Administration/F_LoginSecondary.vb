Imports AccDataAccessLayer.Security
Public Class F_LoginSecondary

    Private _Database As DatabaseInfo
    Private _IsLogonSuccesfull As Boolean = False

    Public ReadOnly Property IsLogonSuccesfull() As Boolean
        Get
            Return _IsLogonSuccesfull
        End Get
    End Property

    Public Sub New(ByVal cDatabase As DatabaseInfo)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _Database = cDatabase

    End Sub

    Private Sub OkButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nOkButton.Click

        Try
            If AccPrincipal.Login(_Database.DatabaseName, New CustomCacheManager, PasswordTextBox.Text) Then
                _IsLogonSuccesfull = True
                Me.Close()
            End If
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        Me.Close()
    End Sub

End Class