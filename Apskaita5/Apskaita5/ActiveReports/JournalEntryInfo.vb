Namespace ActiveReports

    <Serializable()> _
    Public Class JournalEntryInfo
        Inherits ReadOnlyBase(Of JournalEntryInfo)

#Region " Business Methods "

        Private _ID As Integer
        Private _Date As Date
        Private _DocNumber As String
        Private _Content As String
        Private _Person As String
        Private _Ammount As Double
        Private _BookEntries As String
        Private _DocTypeHumanReadable As String
        Private _DocType As DocumentType
        Private _PersonCodeSODRA As String

        ''' <summary>
        ''' Gets an ID of the JournalEntry object (assigned by DB AUTO_INCREMENT).
        ''' </summary>
        Public ReadOnly Property Id() As Integer
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the Journal Entry.
        ''' </summary>
        Public ReadOnly Property [Date]() As Date
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a number of the document associated with the Journal Entry.
        ''' </summary>
        Public ReadOnly Property DocNumber() As String
            Get
                Return _DocNumber
            End Get
        End Property

        ''' <summary>
        ''' Gets a content/description of the the Journal Entry.
        ''' </summary>
        Public ReadOnly Property Content() As String
            Get
                Return _Content
            End Get
        End Property

        ''' <summary>
        ''' Gets a person associated with the Journal Entry.
        ''' </summary>
        Public ReadOnly Property Person() As String
            Get
                Return _Person
            End Get
        End Property

        ''' <summary>
        ''' Gets a SODRA code of the person associated with the Journal Entry.
        ''' </summary>
        Public ReadOnly Property PersonCodeSODRA() As String
            Get
                Return _PersonCodeSODRA
            End Get
        End Property

        ''' <summary>
        ''' Gets a total sum/value of the Journal Entry, usually turnover/2.
        ''' </summary>
        Public ReadOnly Property Ammount() As Double
            Get
                Return _Ammount
            End Get
        End Property

        ''' <summary>
        ''' Gets human readable description of book entries made by the Journal Entry.
        ''' </summary>
        Public ReadOnly Property BookEntries() As String
            Get
                Return _BookEntries
            End Get
        End Property

        ''' <summary>
        ''' Gets a DocumentType of the document associated with the Journal Entry (as human readable string).
        ''' </summary>
        Public ReadOnly Property DocTypeHumanReadable() As String
            Get
                Return _DocTypeHumanReadable
            End Get
        End Property

        ''' <summary>
        ''' Gets a DocumentType of the document associated with the Journal Entry (as enum).
        ''' </summary>
        Public ReadOnly Property DocType() As DocumentType
            Get
                Return _DocType
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Date.ToShortDateString & " " & _DocNumber & " " & GetLimitedLengthString(_Content, 50)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetJournalEntryInfo(ByVal dr As DataRow) As JournalEntryInfo
            Return New JournalEntryInfo(dr)
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
            _ID = CInt(dr.Item(0))
            _Date = CDate(dr.Item(1))
            _DocNumber = dr.Item(2).ToString
            _Content = dr.Item(3).ToString
            If Not IsDBNull(dr.Item(4)) AndAlso Not String.IsNullOrEmpty(dr.Item(4).ToString.Trim) _
                AndAlso Integer.TryParse(dr.Item(4).ToString, New Integer) _
                AndAlso CInt(dr.Item(4).ToString) > 0 Then
                _Person = dr.Item(5).ToString & " (" & dr.Item(6).ToString & ")"
                _PersonCodeSODRA = dr.Item(10).ToString
            Else
                _Person = "Nepriskirtas"
                _PersonCodeSODRA = ""
            End If
            If Not IsDBNull(dr.Item(7)) Then
                _DocType = ConvertEnumDatabaseStringCode(Of DocumentType)(CStrSafe(dr.Item(7)))
                _DocTypeHumanReadable = ConvertEnumHumanReadable(_DocType)
            Else
                _DocType = DocumentType.None
                _DocTypeHumanReadable = ConvertEnumHumanReadable(DocumentType.None)
            End If
            _Ammount = CRound(CDbl(dr.Item(8)))
            _BookEntries = dr.Item(9).ToString
        End Sub

#End Region

    End Class

End Namespace
