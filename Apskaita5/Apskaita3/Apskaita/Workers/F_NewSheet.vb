Public Class F_NewSheet

    Private _Year As Integer = 0
    Private _Month As Integer = 0

    Public ReadOnly Property Year() As Integer
        Get
            Return _Year
        End Get
    End Property

    Public ReadOnly Property Month() As Integer
        Get
            Return _Month
        End Get
    End Property

    Public ReadOnly Property Result() As Boolean
        Get
            Return (_Year > 1900 AndAlso _Year < 2100 AndAlso _Month > 0 AndAlso _Month < 13)
        End Get
    End Property


    Public Sub New(ByVal caption As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = caption

    End Sub


    Private Sub F_NewWageSheet_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load


        YearComboBox.Items.Clear()
        For i As Integer = 1 To 10
            YearComboBox.Items.Add((Today.Year - i + 1).ToString)
        Next

        MonthComboBox.Items.Clear()
        For i As Integer = 1 To 12
            MonthComboBox.Items.Add(i.ToString)
        Next

        Dim msg As String = ""

        If Not GetCurrentCompany.IsSettingsReadyForWageSheet(msg) Then

            MsgBox("Klaida. Nenustatyti visi reikiami nustatymai:" & vbCrLf & msg, _
                MsgBoxStyle.Exclamation, "Klaida.")

            MDIParent1.LaunchForm(GetType(F_Company), True, True, 0)

            If Not GetCurrentCompany.IsSettingsReadyForWageSheet(msg) Then
                MsgBox("Klaida. Nenustatyti visi reikiami nustatymai:" & vbCrLf & msg, _
                    MsgBoxStyle.Exclamation, "Klaida.")
                DisableAllControls(Me)
                nCancelButton.Enabled = True
                Exit Sub
            End If

        End If

        MonthComboBox.SelectedIndex = Today.Month - 1
        YearComboBox.SelectedIndex = 0

    End Sub

    Private Sub nOkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nOkButton.Click

        Try
            _Year = CInt(YearComboBox.SelectedItem.ToString)
        Catch ex As Exception
        End Try
        _Month = MonthComboBox.SelectedIndex + 1

        If Not Result Then
            MsgBox("Klaida. Nenurodyta naujo žiniaraščio metai ir (ar) mėnuo.", _
                MsgBoxStyle.Exclamation, "Klaida")
            _Year = 0
            _Month = 0
            Exit Sub
        End If

        Me.Hide()
        Me.Close()

    End Sub

    Private Sub nCancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        Me.Hide()
        Me.Close()
    End Sub

End Class