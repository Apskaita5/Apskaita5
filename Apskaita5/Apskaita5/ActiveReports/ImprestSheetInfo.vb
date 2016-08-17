Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of imprest sheet report. Contains information about an imprest sheet.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="ImprestSheetInfoList">ImprestSheetInfoList</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class ImprestSheetInfo
        Inherits ReadOnlyBase(Of ImprestSheetInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Date As Date = Today
        Private _Number As Integer = 0
        Private _Year As Integer = 0
        Private _Month As Integer = 0
        Private _TotalSum As Double = 0
        Private _TotalSumPayedOut As Double = 0
        Private _WorkersCount As Integer = 0
        Private _IsPayedOut As Boolean = False


        ''' <summary>
        ''' Gets <see cref="General.JournalEntry.ID">an ID of the journal entry</see> that is created by the imprest sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database table d_avansai.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date of the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_avansai.Z_data.</remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets the number of the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_avansai.Nr.</remarks>
        Public ReadOnly Property Number() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number
            End Get
        End Property

        ''' <summary>
        ''' Gets the year of the imprest calculations within the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_avansai.Met.</remarks>
        Public ReadOnly Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
        End Property

        ''' <summary>
        ''' Gets the month of the imprest calculations within the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_avansai.Men.</remarks>
        Public ReadOnly Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
        End Property

        ''' <summary>
        ''' Gets the total sum of the calculated imprest.
        ''' (<see cref="workers.ImprestSheet.TotalSum">ImprestSheet.TotalSum</see>).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSum)
            End Get
        End Property

        ''' <summary>
        ''' Gets the total sum of the calculated imprest that was actualy payed to the workers.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalSumPayedOut() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumPayedOut)
            End Get
        End Property

        ''' <summary>
        ''' Gets the workers count within the sheet.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WorkersCount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkersCount
            End Get
        End Property

        ''' <summary>
        ''' Whether all the imprest within the sheet was payed to the workers.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsPayedOut() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsPayedOut
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Workers_ImprestSheet_ToString, _
                _Date.ToString("yyyy-MM-dd"), _Number.ToString(), _Year.ToString(), _
                _Month.ToString(), _ID.ToString())
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetImprestSheetInfo(ByVal dr As DataRow) As ImprestSheetInfo
            Return New ImprestSheetInfo(dr)
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
            _Number = CIntSafe(dr.Item(1), 0)
            _Date = CDateSafe(dr.Item(2), Today)
            _Year = CIntSafe(dr.Item(3), 0)
            _Month = CIntSafe(dr.Item(4), 0)
            _WorkersCount = CIntSafe(dr.Item(5), 0)
            _TotalSum = CDblSafe(dr.Item(6), 2, 0)
            _TotalSumPayedOut = CDblSafe(dr.Item(7), 2, 0)
            _IsPayedOut = (CRound(_TotalSum) = CRound(_TotalSumPayedOut))

        End Sub

#End Region

    End Class

End Namespace
