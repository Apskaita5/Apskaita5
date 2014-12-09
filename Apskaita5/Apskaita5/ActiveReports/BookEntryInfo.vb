Imports ApskaitaObjects.General
Namespace ActiveReports

    <Serializable()> _
    Public Class BookEntryInfo
        Inherits ReadOnlyBase(Of BookEntryInfo)

#Region " Business Methods "

        Private _ID As Guid = Guid.NewGuid
        Private _JournalEntryID As Integer
        Private _JournalEntryDate As Date
        Private _DocType As DocumentType
        Private _DocTypeHumanReadable As String
        Private _DocNumber As String
        Private _Content As String
        Private _DebetTurnover As Double
        Private _CreditTurnover As Double
        Private _Person As HelperLists.PersonInfo
        Private _BookEntriesString As String

        ''' <summary>
        ''' Gets a GUID only for technical purposes (databinding needs to define unique row).
        ''' </summary>
        Public ReadOnly Property Id() As Guid
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a JuornalEntry ID (DB assigned by AUTO_INCREMENT).
        ''' </summary>
        Public ReadOnly Property JournalEntryID() As Integer
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the JournalEntry.
        ''' </summary>
        Public ReadOnly Property JournalEntryDate() As Date
            Get
                Return _JournalEntryDate
            End Get
        End Property

        ''' <summary>
        ''' Gets an associated document type as enumeration.
        ''' </summary>
        Public ReadOnly Property DocType() As DocumentType
            Get
                Return _DocType
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable name of an associated document type.
        ''' </summary>
        Public ReadOnly Property DocTypeHumanReadable() As String
            Get
                Return _DocTypeHumanReadable
            End Get
        End Property

        ''' <summary>
        ''' Gets a number of the document that substantiates the JournalEntry.
        ''' </summary>
        Public ReadOnly Property DocNumber() As String
            Get
                Return _DocNumber
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of the JournalEntry.
        ''' </summary>
        Public ReadOnly Property Content() As String
            Get
                Return _Content
            End Get
        End Property

        ''' <summary>
        ''' Gets a debet side turnover of the book entry (if any).
        ''' </summary>
        Public ReadOnly Property DebetTurnover() As Double
            Get
                Return _DebetTurnover
            End Get
        End Property

        ''' <summary>
        ''' Gets a credit side turnover of the book entry (if any).
        ''' </summary>
        Public ReadOnly Property CreditTurnover() As Double
            Get
                Return _CreditTurnover
            End Get
        End Property

        ''' <summary>
        ''' Gets a person that is associated with the JournalEntry or the BookEntry.
        ''' JournalEntry level person association has priority over BookEntry level associatiation.
        ''' </summary>
        Public ReadOnly Property Person() As HelperLists.PersonInfo
            Get
                Return _Person
            End Get
        End Property

        ''' <summary>
        ''' Gets a list of all the book entries in the JournalEntry formated as e.g. "D 271, K 443".
        ''' </summary>
        Public ReadOnly Property BookEntriesString() As String
            Get
                Return _BookEntriesString
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return _JournalEntryDate.ToShortDateString & " " & _DocNumber & " " & _Content
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetBookEntryInfo(ByVal dr As DataRow) As BookEntryInfo
            Return New BookEntryInfo(dr)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)
            _JournalEntryID = CInt(dr.Item(0))
            _JournalEntryDate = CDate(dr.Item(1))
            _DocNumber = dr.Item(2).ToString
            _Content = dr.Item(3).ToString

            If IsDBNull(dr.Item(4)) Then dr.Item(4) = ""
            _DocType = ConvertEnumDatabaseStringCode(Of DocumentType)(CStrSafe(dr.Item(4)))
            _DocTypeHumanReadable = ConvertEnumHumanReadable(_DocType)

            If IsDBNull(dr.Item(5)) OrElse String.IsNullOrEmpty(dr.Item(5).ToString) Then dr.Item(5) = "0"
            If IsDBNull(dr.Item(11)) OrElse String.IsNullOrEmpty(dr.Item(11).ToString) Then dr.Item(11) = "0"
            If Not CInt(dr.Item(5)) > 0 AndAlso Not CInt(dr.Item(11)) > 0 Then
                _Person = HelperLists.PersonInfo.GetEmptyPersonInfo
            ElseIf CInt(dr.Item(5)) > 0 Then
                _Person = HelperLists.PersonInfo.GetPersonInfo(CInt(dr.Item(5)), dr.Item(6).ToString, _
                    dr.Item(7).ToString, dr.Item(8).ToString, "", "", "", "", "", 0, 0)
            Else
                _Person = HelperLists.PersonInfo.GetPersonInfo(CInt(dr.Item(11)), dr.Item(12).ToString, _
                    dr.Item(13).ToString, dr.Item(14).ToString, "", "", "", "", "", 0, 0)
            End If

            If dr.Item(9).ToString.Trim.ToLower = "debetas" Then
                _DebetTurnover = CRound(CDbl(dr.Item(10)))
                _CreditTurnover = 0
            Else
                _DebetTurnover = 0
                _CreditTurnover = CRound(CDbl(dr.Item(10)))
            End If

            _BookEntriesString = dr.Item(15).ToString

        End Sub

#End Region

    End Class

End Namespace