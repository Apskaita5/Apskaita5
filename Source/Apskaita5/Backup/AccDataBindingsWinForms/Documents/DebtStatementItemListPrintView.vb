Imports ApskaitaObjects.ActiveReports
Imports AccDataBindingsWinForms.Printing
Imports AccControlsWinForms

Public Class DebtStatementItemListPrintView

    Private ReadOnly _Source As DebtStatementItemList
    Private ReadOnly _SelectedPersonsIds As List(Of Integer)
    Private ReadOnly _StatementDate As Date = Today
    Private ReadOnly _SignWithFacsimile As Boolean = False


    Public ReadOnly Property Source() As DebtStatementItemList
        Get
            Return _Source
        End Get
    End Property

    Public ReadOnly Property SelectedPersonsIds() As List(Of Integer)
        Get
            Return _SelectedPersonsIds
        End Get
    End Property

    Public ReadOnly Property StatementDate() As Date
        Get
            Return _StatementDate
        End Get
    End Property

    Public ReadOnly Property SignWithFacsimile() As Boolean
        Get
            Return _SignWithFacsimile
        End Get
    End Property


    Private Sub New(ByVal source As DebtStatementItemList, ByVal selectedIds As List(Of Integer), _
        ByVal statementDate As Date, ByVal signWithFacsimile As Boolean)
        _Source = source
        _SelectedPersonsIds = selectedIds
        _StatementDate = statementDate
        _SignWithFacsimile = signWithFacsimile
    End Sub


    Public Function GetStatementNumber(ByVal personId As Integer) As String
        Return String.Format("{0:yyyyMMdd}-{1}", _StatementDate, personId.ToString())
    End Function


    Public Shared Sub Print(ByVal source As DebtStatementItemList, ByVal statementDate As Date, _
        ByVal signWithFacsimile As Boolean, ByVal parentForm As Form, ByVal preview As Boolean)

        If source Is Nothing Then Exit Sub

        Using dlg As New F_DebtStatementPersonDialog(source, False)

            If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub
            If dlg.SelectedEntries Is Nothing OrElse dlg.SelectedEntries.Count < 1 Then Exit Sub

            Dim ids As List(Of Integer) = (From p In dlg.SelectedEntries Select p.PersonId).ToList()

            Try
                PrintObject(New DebtStatementItemListPrintView(source, ids, statementDate, _
                    signWithFacsimile), preview, 0, "SkoluSuderinimoAktai", parentForm, "", New List(Of Integer)())
            Catch ex As Exception
                ShowError(ex)
            End Try

        End Using

    End Sub

    Public Shared Sub Email(ByVal source As DebtStatementItemList, ByVal statementDate As Date, _
        ByVal signWithFacsimile As Boolean)

        If source Is Nothing Then Exit Sub

        Dim result As DebtStatementItemListPrintView

        Using dlg As New F_DebtStatementPersonDialog(source, False)

            If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub
            If dlg.SelectedEntries Is Nothing OrElse dlg.SelectedEntries.Count < 1 Then Exit Sub

            Dim ids As List(Of Integer) = (From p In dlg.SelectedEntries Select p.PersonId).ToList()

            result = New DebtStatementItemListPrintView(source, ids, statementDate, signWithFacsimile)

        End Using

        Using frm As New F_SendObjToEmail(result, 0)
            frm.ShowDialog()
        End Using

    End Sub

    Public Shared Sub EmailToPersons(ByVal source As DebtStatementItemList, ByVal statementDate As Date, _
        ByVal signWithFacsimile As Boolean)

        If source Is Nothing Then Exit Sub

        Dim ids As List(Of Integer)

        Using dlg As New F_DebtStatementPersonDialog(source, True)

            If dlg.ShowDialog() <> DialogResult.OK Then Exit Sub
            If dlg.SelectedEntries Is Nothing OrElse dlg.SelectedEntries.Count < 1 Then Exit Sub

            ids = (From p In dlg.SelectedEntries Select p.PersonId).ToList()

        End Using

        Try

            Using busy As New StatusBusy

                Dim mailSubject, fileName, mail As String

                For Each id As Integer In ids

                    Dim idParam As New List(Of Integer)
                    idParam.Add(id)
                    Dim item As New DebtStatementItemListPrintView(source, idParam, statementDate, signWithFacsimile)

                    mailSubject = String.Format("Suderinimo aktas {0:yyyy-MM-dd} Nr. {1}", _
                        statementDate, item.GetStatementNumber(id))
                    fileName = String.Format("{0:yyyyMMdd}SuderinimoAktas{1}", _
                        statementDate, item.GetStatementNumber(id))
                    mail = source.GetEmail(id)

                    SendObjectToEmail(item, mail, mailSubject, MyCustomSettings.EmailMessageText, 0, fileName, "", Nothing)

                Next

            End Using
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

    End Sub

End Class
