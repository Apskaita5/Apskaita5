Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents a general ledger report item.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class JournalEntryInfo
        Inherits ReadOnlyBase(Of JournalEntryInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Date As Date = Today
        Private _DocNumber As String = ""
        Private _Content As String = ""
        Private _PersonID As Integer = 0
        Private _Person As String = ""
        Private _Ammount As Double = 0
        Private _BookEntries As String = ""
        Private _DocTypeHumanReadable As String = ""
        Private _DocType As DocumentType = DocumentType.None
        Private _PersonCodeSODRA As String = ""


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
        ''' Gets an ID of the person associated with the Journal Entry.
        ''' </summary>
        Public ReadOnly Property PersonID() As Integer
            Get
                Return _PersonID
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
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
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
            Return String.Format(My.Resources.ActiveReports_JournalEntryInfo_ToString, _
                _Date.ToString("yyyy-MM-dd"), _DocNumber, GetLimitedLengthString(_Content, 50))
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a journal entry info item by a database query result.
        ''' </summary>
        ''' <param name="dr">a database query result</param>
        ''' <remarks></remarks>
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
            _ID = CIntSafe(dr.Item(0), 0)
            _Date = CDateSafe(dr.Item(1), Date.MinValue)
            _DocNumber = CStrSafe(dr.Item(2))
            _Content = CStrSafe(dr.Item(3))
            If CIntSafe(dr.Item(4), 0) > 0 Then
                _PersonID = CIntSafe(dr.Item(4), 0)
                _Person = String.Format("{0} ({1})", CStrSafe(dr.Item(5)), CStrSafe(dr.Item(6)))
                _PersonCodeSODRA = dr.Item(10).ToString
            End If
            If Not StringIsNullOrEmpty(CStrSafe(dr.Item(7))) Then
                _DocType = Utilities.ConvertDatabaseCharID(Of DocumentType)(CStrSafe(dr.Item(7)))
            End If
            _DocTypeHumanReadable = Utilities.ConvertLocalizedName(_DocType)
            _Ammount = CDblSafe(dr.Item(8), 2, 0)
            _BookEntries = CStrSafe(dr.Item(9))
        End Sub

#End Region

    End Class

End Namespace
