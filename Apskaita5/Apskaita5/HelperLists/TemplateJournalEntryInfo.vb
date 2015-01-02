Namespace HelperLists

    <Serializable()> _
    Public Class TemplateJournalEntryInfo
        Inherits ReadOnlyBase(Of TemplateJournalEntryInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _Content As String = ""
        Private _DebetListString As String = ""
        Private _CreditListString As String = ""
        Private _CorespondationListString As String = ""


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        Public ReadOnly Property DebetListString() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DebetListString.Trim
            End Get
        End Property

        Public ReadOnly Property CreditListString() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CreditListString.Trim
            End Get
        End Property

        Public ReadOnly Property CorespondationListString() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CorespondationListString.Trim
            End Get
        End Property

        Public ReadOnly Property GetMe() As TemplateJournalEntryInfo
            Get
                Return Me
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Name.Trim
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetTemplateJournalEntryInfo(ByVal dr As DataRow) As TemplateJournalEntryInfo
            Return New TemplateJournalEntryInfo(dr)
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

            _ID = CIntSafe(dr.item(0), 0)
            _Name = CStrSafe(dr.Item(1)).Trim
            _Content = CStrSafe(dr.Item(2)).Trim

            Dim Corespondations As String() = CStrSafe(dr.Item(3)).Trim.Split( _
                New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)

            Dim DebetList As New List(Of String)
            Dim CreditList As New List(Of String)

            For Each s As String In Corespondations
                If s.Trim.ToLower.StartsWith(ConvertEnumDatabaseStringCode(BookEntryType.Debetas).ToLower) Then
                    DebetList.Add(s.Trim.ToLower.Replace(ConvertEnumDatabaseStringCode( _
                        BookEntryType.Debetas).ToLower, ""))
                ElseIf s.Trim.ToLower.StartsWith(ConvertEnumDatabaseStringCode(BookEntryType.Kreditas).ToLower) Then
                    CreditList.Add(s.Trim.ToLower.Replace(ConvertEnumDatabaseStringCode( _
                        BookEntryType.Kreditas).ToLower, ""))
                End If
            Next

            For i As Integer = DebetList.Count To 1 Step -1
                If Not Long.TryParse(DebetList(i - 1), New Long) Then DebetList.RemoveAt(i - 1)
            Next
            For i As Integer = CreditList.Count To 1 Step -1
                If Not Long.TryParse(CreditList(i - 1), New Long) Then CreditList.RemoveAt(i - 1)
            Next

            _DebetListString = String.Join(",", DebetList.ToArray)
            _CreditListString = String.Join(",", CreditList.ToArray)

            For i As Integer = DebetList.Count To 1 Step -1
                DebetList(i - 1) = "D" & DebetList(i - 1)
            Next
            For i As Integer = CreditList.Count To 1 Step -1
                CreditList(i - 1) = "K" & CreditList(i - 1)
            Next

            DebetList.AddRange(CreditList)

            _CorespondationListString = String.Join(", ", DebetList.ToArray)

        End Sub

#End Region

    End Class

End Namespace