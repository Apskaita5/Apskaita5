Imports ApskaitaObjects.ActiveReports
Imports AccControlsWinForms
Imports System.IO
Imports System.IO.Compression

Public Class F_CommandFetchAuditDataFileSAFT
    Implements ISingleInstanceForm

    Private _QueryManager As CslaActionExtenderQueryObject


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller1)
        Me.VersionComboBox.DataSource = GetAvailableIAuditFileSAFTList()
        Me.PeriodStartAccDatePicker.Value = New Date(Today.Year - 1, 1, 1)
        Me.PeriodEndAccDatePicker.Value = New Date(Today.Year - 1, 12, 31)

    End Sub


    Private Sub RefreshButton_Click(sender As Object, e As EventArgs) Handles RefreshButton.Click

        Dim version As IAuditFileSAFT = Nothing
        Try
            version = DirectCast(VersionComboBox.SelectedItem, IAuditFileSAFT)
        Catch ex As Exception
        End Try
        If version Is Nothing Then
            MsgBox("Klaida. Nepasirinkta SAF-T audito duomenų failo versija.", MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        If version.ValidTo < Today Then
            If Not YesOrNo(String.Format("Pasirinkta SAF-T duomenų versija nebegalioja. Ji galiojo nuo {0} iki {1}. {2} Ar tikrai norite išsaugoti duomenis šia versija?",
                version.ValidFrom.ToString("yyyy-MM-dd"), version.ValidTo.ToString("yyyy-MM-dd"), vbCrLf)) Then
                Exit Sub
            End If
        End If

        'CommandFetchAuditDataFileSAFT.TheCommand(version, PeriodStartAccDatePicker.Value,
        '    PeriodEndAccDatePicker.Value, MyCustomSettings.LastUpdateDate.ToString("yyyyMMdd"))
        _QueryManager.InvokeQuery(Of CommandFetchAuditDataFileSAFT)(Nothing, "TheCommand", True,
            AddressOf OnFileFetched, version, PeriodStartAccDatePicker.Value,
            PeriodEndAccDatePicker.Value, MyCustomSettings.LastUpdateDate.ToString("yyyyMMdd"))

    End Sub

    Private Sub OnFileFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        Dim xmlString As String = DirectCast(result, String)

        If StringIsNullOrEmpty(xmlString.Trim) Then
            MsgBox("Klaida. Dėl nežinomų priežasčių negautas duomenų failas.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        If ValidateXmlCheckBox.Checked Then

            Dim version As IAuditFileSAFT = DirectCast(VersionComboBox.SelectedItem, IAuditFileSAFT)

            Dim errors As String = String.Empty
            Dim warnings As String = String.Empty

            Try
                Using busy As New StatusBusy
                    Declarations.XmlValidationErrorBuilder.Validate(xmlString,
                        IO.Path.Combine(AppPath(), version.XsdFileName),
                        version.TargetNameSpace, errors, warnings)
                End Using
            Catch ex As Exception
                ShowError(ex, Nothing)
                Exit Sub
            End Try

            Dim message As String = String.Empty

            If Not errors Is Nothing AndAlso Not String.IsNullOrEmpty(errors.Trim) Then
                message = AddWithNewLine(message, "KLAIDOS:", False)
                message = AddWithNewLine(message, errors, False)
            End If

            If Not warnings Is Nothing AndAlso Not String.IsNullOrEmpty(warnings.Trim) Then
                If Not String.IsNullOrEmpty(message.Trim) Then
                    message = AddWithNewLine(message, "", True)
                    message = AddWithNewLine(message, "", True)
                End If
                message = AddWithNewLine(message, "PERSPĖJIMAI:", False)
                message = AddWithNewLine(message, warnings, False)
            End If

            If Not String.IsNullOrEmpty(message.Trim) Then
                message = String.Format("DĖMESIO. Duomenys neatitinka arba nepilnai atitinka SAF-T reikalavimus.{0}Ar norite išsaugoti duomenis su klaidomis ir/ar perspėjimais?{1}{2}{3}",
                    vbCrLf, vbCrLf, vbCrLf, message)
                If Not YesOrNo(message) Then Exit Sub
            End If

        End If

        Dim fileName As String = ""

        Using sfd As New SaveFileDialog
            sfd.Filter = "GZip failai|*.gz|XML failai|*.xml|Visi failai|*.*"
            sfd.CheckFileExists = False
            sfd.AddExtension = True
            sfd.DefaultExt = ".gz"
            If sfd.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
            fileName = sfd.FileName.Trim
        End Using

        If StringIsNullOrEmpty(fileName) Then Exit Sub

        Try

            Using busy As New StatusBusy

                If IO.Path.GetExtension(fileName).Trim.ToLower = ".xml" Then
                    IO.File.WriteAllText(fileName, result, New System.Text.UTF8Encoding(False))
                Else
                    fileName = fileName.Replace(IO.Path.GetExtension(fileName),
                        ".xml" & IO.Path.GetExtension(fileName))
                    IO.File.WriteAllBytes(fileName, Compress(result))
                End If
            End Using

        Catch ex As Exception
            ShowError(ex, Nothing)
            Exit Sub
        End Try

        If YesOrNo("Failas sėkmingai išsaugotas. Atidaryti?") Then
            System.Diagnostics.Process.Start(fileName)
        End If

    End Sub


    Private Function Compress(ByVal source As String) As Byte()

        Using ms As New MemoryStream
            Using gzip As New GZipStream(ms, CompressionMode.Compress, True)
                Dim encoding As New System.Text.UTF8Encoding(False)
                Dim sourceBytes As Byte() = encoding.GetBytes(source)
                gzip.Write(sourceBytes, 0, sourceBytes.Length)
            End Using
            Return ms.ToArray()
        End Using

    End Function

    Private Sub F_CommandFetchAuditDataFileSAFT_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

End Class
