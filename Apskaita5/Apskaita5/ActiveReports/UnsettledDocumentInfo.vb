Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of <see cref="UnsettledPersonInfoList">UnsettledPersonInfoList</see> report.
    ''' Contains information about an unsettled (not payed) documents.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="UnsettledDocumentInfoList">UnsettledDocumentInfoList</see>.</remarks>
    <Serializable()> _
    Public Class UnsettledDocumentInfo
        Inherits ReadOnlyBase(Of UnsettledDocumentInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Date As Date = Today
        Private _DocNo As String = ""
        Private _DocType As DocumentType = DocumentType.None
        Private _DocTypeHumanReadable As String = ""
        Private _Content As String = ""
        Private _SumInDocument As Double = 0
        Private _Debt As Double = 0


        ''' <summary>
        ''' Gets an ID of the JournalEntry object (assigned by DB AUTO_INCREMENT).
        ''' </summary>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the Journal Entry.
        ''' </summary>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a number of the document associated with the Journal Entry.
        ''' </summary>
        Public ReadOnly Property DocNo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocNo.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a DocumentType of the document associated with the Journal Entry (as enum).
        ''' </summary>
        Public ReadOnly Property DocType() As DocumentType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocType
            End Get
        End Property

        ''' <summary>
        ''' Gets a DocumentType of the document associated with the Journal Entry (as human readable string).
        ''' </summary>
        Public ReadOnly Property DocTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocTypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a content/description of the the Journal Entry.
        ''' </summary>
        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the total sum of debt in the document.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SumInDocument() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumInDocument)
            End Get
        End Property

        ''' <summary>
        ''' Gets the unsettled part of debt in the document.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property Debt() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Debt)
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_UnsettledDocumentInfo_ToString, _
                _Date.ToString("yyyy-MM-dd"), _DocNo, GetLimitedLengthString(_Content, 100))
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets an UnsettledDocumentInfo item by a database query.
        ''' </summary>
        ''' <param name="dr">a database query result</param>
        ''' <remarks></remarks>
        Friend Shared Function GetUnsettledDocumentInfo(ByVal dr As DataRow) As UnsettledDocumentInfo
            Return New UnsettledDocumentInfo(dr)
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
            _Date = CDateSafe(dr.Item(1), Today)
            _DocNo = CStrSafe(dr.Item(2)).Trim
            _DocType = ConvertEnumDatabaseStringCode(Of DocumentType)(CStrSafe(dr.Item(3)))
            _DocTypeHumanReadable = ConvertEnumHumanReadable(_DocType)
            _Content = CStrSafe(dr.Item(4)).Trim
            _SumInDocument = CDblSafe(dr.Item(5), 2, 0)
            _Debt = _SumInDocument

        End Sub

        ''' <summary>
        ''' Ajusts the last document debt so that it does not exceed actual total debt.
        ''' </summary>
        ''' <param name="value">A value by which debt shall be reduced.</param>
        ''' <remarks></remarks>
        Friend Sub AdjustDebt(ByVal value As Double)
            _Debt = CRound(_Debt - value, 2)
        End Sub

#End Region

    End Class

End Namespace