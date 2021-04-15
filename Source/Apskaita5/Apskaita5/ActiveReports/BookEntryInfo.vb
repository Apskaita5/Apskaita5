Imports ApskaitaObjects.General
Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represent an item of a account book entry report.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class BookEntryInfo
        Inherits ReadOnlyBase(Of BookEntryInfo)

#Region " Business Methods "

        Private ReadOnly _ID As Guid = Guid.NewGuid
        Private _JournalEntryID As Integer = 0
        Private _JournalEntryDate As Date = Today
        Private _DocType As DocumentType = DocumentType.None
        Private _DocTypeHumanReadable As String = ""
        Private _DocNumber As String = ""
        Private _Content As String = ""
        Private _DebetTurnover As Double = 0
        Private _CreditTurnover As Double = 0
        Private _PersonID As Integer = 0
        Private _PersonName As String = ""
        Private _PersonCode As String = ""
        Private _PersonAddress As String = ""

        Private _BookEntriesString As String = ""

        ''' <summary>
        ''' Gets a GUID only for technical purposes.
        ''' </summary>
        ''' <remarks>Databinding needs to define unique row. 
        ''' There might be more then one book entry for the same account for the same journal entry, 
        ''' thus journal entry ID is not sufficient.</remarks>
        Public ReadOnly Property Id() As Guid
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a JuornalEntry ID (DB assigned by AUTO_INCREMENT).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.ID">JournalEntry.ID</see> property.</remarks>
        Public ReadOnly Property JournalEntryID() As Integer
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the JournalEntry.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.[Date]">JournalEntry.Date</see> property.</remarks>
        Public ReadOnly Property JournalEntryDate() As Date
            Get
                Return _JournalEntryDate
            End Get
        End Property

        ''' <summary>
        ''' Gets an associated document type as enumeration.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.DocType">JournalEntry.DocType</see> property.</remarks>
        Public ReadOnly Property DocType() As DocumentType
            Get
                Return _DocType
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable name of an associated document type.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.DocTypeHumanReadable">JournalEntry.DocTypeHumanReadable</see> property.</remarks>
        Public ReadOnly Property DocTypeHumanReadable() As String
            Get
                Return _DocTypeHumanReadable
            End Get
        End Property

        ''' <summary>
        ''' Gets a number of the document that substantiates the JournalEntry.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.DocNumber">JournalEntry.DocNumber</see> property.</remarks>
        Public ReadOnly Property DocNumber() As String
            Get
                Return _DocNumber
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of the JournalEntry.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Content">JournalEntry.Content</see> property.</remarks>
        Public ReadOnly Property Content() As String
            Get
                Return _Content
            End Get
        End Property

        ''' <summary>
        ''' Gets a debet side turnover of the book entry (if any).
        ''' </summary>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DebetTurnover() As Double
            Get
                Return _DebetTurnover
            End Get
        End Property

        ''' <summary>
        ''' Gets a credit side turnover of the book entry (if any).
        ''' </summary>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property CreditTurnover() As Double
            Get
                Return _CreditTurnover
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the person that is associated with the JournalEntry or the BookEntry.
        ''' JournalEntry level person association has priority over BookEntry level associatiation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Person">JournalEntry.Person</see> property.</remarks>
        Public ReadOnly Property PersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonID
            End Get
        End Property

        ''' <summary>
        ''' Gets a Name of the person that is associated with the JournalEntry or the BookEntry.
        ''' JournalEntry level person association has priority over BookEntry level associatiation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Person">JournalEntry.Person</see> property.</remarks>
        Public ReadOnly Property PersonName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a code of the person that is associated with the JournalEntry or the BookEntry.
        ''' JournalEntry level person association has priority over BookEntry level associatiation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Person">JournalEntry.Person</see> property.</remarks>
        Public ReadOnly Property PersonCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an address of the person that is associated with the JournalEntry or the BookEntry.
        ''' JournalEntry level person association has priority over BookEntry level associatiation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Person">JournalEntry.Person</see> property.</remarks>
        Public ReadOnly Property PersonAddress() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonAddress.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of the person that is associated with the JournalEntry or the BookEntry.
        ''' JournalEntry level person association has priority over BookEntry level associatiation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.JournalEntry.Person">JournalEntry.Person</see> property.</remarks>
        Public ReadOnly Property Person() As String
            Get
                If _PersonID > 0 Then
                    Return String.Format("{0} ({1})", _PersonName, _PersonCode)
                Else
                    Return ""
                End If
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
            Return String.Format(My.Resources.ActiveReports_BookEntryInfo_ToString, _
                _JournalEntryDate.ToString("yyyy-MM-dd"), _DocNumber, _Content)
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets book entry info by a database query result.
        ''' </summary>
        ''' <param name="dr">a database query result</param>
        ''' <remarks></remarks>
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
            _JournalEntryID = CIntSafe(dr.Item(0), 0)
            _JournalEntryDate = CDateSafe(dr.Item(1), Date.MinValue)
            _DocNumber = CStrSafe(dr.Item(2))
            _Content = CStrSafe(dr.Item(3))

            _DocType = ConvertDatabaseCharID(Of DocumentType)(CStrSafe(dr.Item(4)))
            _DocTypeHumanReadable = ConvertLocalizedName(_DocType)

            _PersonID = CIntSafe(dr.Item(5), 0)
            If _PersonID > 0 Then
                _PersonName = CStrSafe(dr.Item(6)).Trim
                _PersonCode = CStrSafe(dr.Item(7)).Trim
                _PersonAddress = CStrSafe(dr.Item(8)).Trim
            End If

            If ConvertDatabaseCharID(Of BookEntryType)(CStrSafe(dr.Item(9))) = BookEntryType.Debetas Then
                _DebetTurnover = CDblSafe(dr.Item(10), 2, 0)
                _CreditTurnover = 0
            Else
                _DebetTurnover = 0
                _CreditTurnover = CDblSafe(dr.Item(10), 2, 0)
            End If

            _BookEntriesString = CStrSafe(dr.Item(11))

        End Sub

#End Region

    End Class

End Namespace
