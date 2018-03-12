Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="General.TemplateJournalEntry">journal entry template</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database tables tipines_op (general data) and tipines_data (details).</remarks>
    <Serializable()> _
    Public NotInheritable Class TemplateJournalEntryInfo
        Inherits ReadOnlyBase(Of TemplateJournalEntryInfo)
        Implements IValueObject, IComparable

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _Content As String = ""
        Private _DebetList As Long() = Nothing
        Private _CreditList As Long() = Nothing
        Private _DebetListString As String = ""
        Private _CreditListString As String = ""
        Private _CorespondationListString As String = ""


        ''' <summary>
        ''' Whether an object is a place holder (does not represent a real template).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObject.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ID > 0
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the TemplateJournalEntry object (assigned by DB AUTO_INCREMENT).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.TemplateJournalEntry.ID">TemplateJournalEntry.ID</see> property.
        ''' Value is stored in the database field tipines_op.T_ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the name of the template. Usualy it is shown in drop down controls.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.TemplateJournalEntry.Name">TemplateJournalEntry.Name</see> property.
        ''' Value is stored in the database field tipines_op.Turinys.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the template (default text) for the JournalEntry property <see cref="General.JournalEntry.Content">Content</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.TemplateJournalEntry.Content">TemplateJournalEntry.Content</see> property.
        ''' Value is stored in the database field tipines_op.Pavadinimas.</remarks>
        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a list of debited accounts <see cref="General.Account.ID">ID</see>.
        ''' </summary>
        ''' <remarks>Values are stored in the database table tipines_data.</remarks>
        Public ReadOnly Property DebetList() As Long()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DebetList
            End Get
        End Property

        ''' <summary>
        ''' Gets a list of credited accounts <see cref="General.Account.ID">ID</see>.
        ''' </summary>
        ''' <remarks>Values are stored in the database table tipines_data.</remarks>
        Public ReadOnly Property CreditList() As Long()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CreditList
            End Get
        End Property

        ''' <summary>
        ''' Gets a comma delimited list of debited accounts <see cref="General.Account.ID">ID</see>.
        ''' </summary>
        ''' <remarks>Values are stored in the database table tipines_data.</remarks>
        Public ReadOnly Property DebetListString() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DebetListString.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a comma delimited list of credited accounts <see cref="General.Account.ID">ID</see>.
        ''' </summary>
        ''' <remarks>Values are stored in the database table tipines_data.</remarks>
        Public ReadOnly Property CreditListString() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CreditListString.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a desciption of template book entries within the template.
        ''' </summary>
        ''' <remarks>Values are stored in the database table tipines_data.</remarks>
        Public ReadOnly Property CorespondationListString() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CorespondationListString.Trim
            End Get
        End Property


        Public Shared Operator =(ByVal a As TemplateJournalEntryInfo, ByVal b As TemplateJournalEntryInfo) As Boolean

            Dim aId, bId As Integer
            If a Is Nothing OrElse a.IsEmpty Then
                aId = 0
            Else
                aId = a.ID
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bId = 0
            Else
                bId = b.ID
            End If

            Return aId = bId

        End Operator

        Public Shared Operator <>(ByVal a As TemplateJournalEntryInfo, ByVal b As TemplateJournalEntryInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As TemplateJournalEntryInfo, ByVal b As TemplateJournalEntryInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString > bToString

        End Operator

        Public Shared Operator <(ByVal a As TemplateJournalEntryInfo, ByVal b As TemplateJournalEntryInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString < bToString

        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim tmp As TemplateJournalEntryInfo = TryCast(obj, TemplateJournalEntryInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Name.Trim
        End Function

#End Region

#Region " Factory Methods "

        Private Shared _Empty As TemplateJournalEntryInfo = Nothing

        ''' <summary>
        ''' Gets an empty TemplateJournalEntryInfo (placeholder).
        ''' </summary>
        Public Shared Function Empty() As TemplateJournalEntryInfo
            If _Empty Is Nothing Then
                _Empty = New TemplateJournalEntryInfo
            End If
            Return _Empty
        End Function

        ''' <summary>
        ''' Gets an existing template info by a database query.
        ''' </summary>
        ''' <param name="dr">DataRow containing general template data</param>
        ''' <param name="detailsData">Datatable containing template booke entries' data</param>
        Friend Shared Function GetTemplateJournalEntryInfo(ByVal dr As DataRow, _
            ByVal detailsData As DataTable) As TemplateJournalEntryInfo
            Return New TemplateJournalEntryInfo(dr, detailsData)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal detailsData As DataTable)
            Fetch(dr, detailsData)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal detailsData As DataTable)

            _ID = CIntSafe(dr.Item(0), 0)
            _Name = CStrSafe(dr.Item(1)).Trim
            _Content = CStrSafe(dr.Item(2)).Trim

            Dim DebetListStr As New List(Of String)
            Dim CreditListStr As New List(Of String)
            Dim DebetListLng As New List(Of Long)
            Dim CreditListLng As New List(Of Long)
            Dim currentAccount As Long

            For Each entry As DataRow In detailsData.Rows
                If CIntSafe(entry.Item(0), 0) = _ID Then
                    currentAccount = CLongSafe(entry.Item(2), 0)
                    If ConvertDatabaseCharID(Of BookEntryType)(CStrSafe(entry.Item(1))) = BookEntryType.Debetas Then
                        DebetListStr.Add(currentAccount.ToString())
                        DebetListLng.Add(currentAccount)
                    Else
                        CreditListStr.Add(currentAccount.ToString())
                        CreditListLng.Add(currentAccount)
                    End If
                End If
            Next

            _DebetList = DebetListLng.ToArray
            _CreditList = CreditListLng.ToArray
            _DebetListString = String.Join(",", DebetListStr.ToArray)
            _CreditListString = String.Join(",", CreditListStr.ToArray)

            For i As Integer = 1 To DebetListStr.Count
                DebetListStr(i - 1) = My.Resources.General_BookEntryList_CharForDebit & DebetListStr(i - 1)
            Next
            For i As Integer = 1 To CreditListStr.Count
                CreditListStr(i - 1) = My.Resources.General_BookEntryList_CharForCredit & CreditListStr(i - 1)
            Next

            DebetListStr.AddRange(CreditListStr)

            _CorespondationListString = String.Join(", ", DebetListStr.ToArray)

        End Sub

#End Region

    End Class

End Namespace