Imports AccDataAccessLayer.DatabaseAccess.DatabaseStructure
Imports System.ComponentModel
Public Class F_Migrate

    Private WithEvents worker As New BackgroundWorker
    Private _credentials As MigrationMethods.MySqlToSQLiteCredentials
    Private _migrateToSQLite As Boolean
    Private InternalClose As Boolean = False


    Public Sub New(ByVal credentials As MigrationMethods.MySqlToSQLiteCredentials, ByVal migrateToSQLite As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _credentials = credentials
        _migrateToSQLite = migrateToSQLite

    End Sub


    Private Sub F_Migrate_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not InternalClose Then e.Cancel = True
    End Sub

    Private Sub F_Migrate_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        worker.WorkerReportsProgress = True
        worker.WorkerSupportsCancellation = True

        AddHandler worker.ProgressChanged, AddressOf worker_ProgressChanged
        AddHandler worker.RunWorkerCompleted, AddressOf worker_RunWorkerCompleted

        If _migrateToSQLite Then
            AddHandler worker.DoWork, AddressOf MigrationMethods.ConvertMySqlToSQLite
        Else
            AddHandler worker.DoWork, AddressOf MigrationMethods.ConvertSQLiteToMySql
        End If

        worker.RunWorkerAsync(_credentials)

    End Sub

    Private Sub worker_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)

        ProgressBar1.Value = e.ProgressPercentage
        Label1.Text = e.UserState.ToString

    End Sub

    Private Sub worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)

        If Not e.Error Is Nothing Then
            MsgBox(e.Error.Message, MsgBoxStyle.Exclamation, "")
        Else
            MsgBox("Migration has completed succesfuly.", MsgBoxStyle.Information, "")
        End If

        InternalClose = True
        Me.Close()

    End Sub

    Private Sub ICancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ICancelButton.Click
        worker.CancelAsync()
    End Sub

End Class