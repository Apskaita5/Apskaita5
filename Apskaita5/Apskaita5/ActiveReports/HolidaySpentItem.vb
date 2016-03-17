Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents info about holiday used by a worker.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class HolidaySpentItem
        Inherits ReadOnlyBase(Of HolidaySpentItem)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Type As HolidaySpentItemType = HolidaySpentItemType.Correction
        Private _TypeHumanReadable As String = ""
        Private _Spent As Integer = 0
        Private _Compensated As Double = 0
        Private _Correction As Double = 0
        Private _Total As Double = 0
        Private _DocumentID As Integer = 0
        Private _DocumentDate As Date = Today
        Private _DocumentNumber As String = ""
        Private _DocumentContent As String = ""


        ''' <summary>
        ''' Type of the item.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As HolidaySpentItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Human readable description of the type of the item.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Amount of holiday days that were granted to a worker.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Spent() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Spent
            End Get
        End Property

        ''' <summary>
        ''' Amount of holiday days that were compensated for when terminating a labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDACCUMULATEDHOLIDAY)> _
        Public ReadOnly Property Compensated() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Compensated, ROUNDACCUMULATEDHOLIDAY)
            End Get
        End Property

        ''' <summary>
        ''' Amount of technical (manual) correction holiday days.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDACCUMULATEDHOLIDAY)> _
        Public ReadOnly Property Correction() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Correction, ROUNDACCUMULATEDHOLIDAY)
            End Get
        End Property

        ''' <summary>
        ''' Amount of holiday days applicable.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDACCUMULATEDHOLIDAY)> _
        Public ReadOnly Property Total() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Total, ROUNDACCUMULATEDHOLIDAY)
            End Get
        End Property

        ''' <summary>
        ''' ID of the document.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DocumentID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentID
            End Get
        End Property

        ''' <summary>
        ''' Date of the document.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DocumentDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentDate
            End Get
        End Property

        ''' <summary>
        ''' Number of the document.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentNumber.Trim
            End Get
        End Property

        ''' <summary>
        ''' Description of the document.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DocumentContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentContent.Trim
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_HolidaySpentItem_ToString, _
                _DocumentDate.ToString("yyyy-MM-dd"), _TypeHumanReadable, _
                DblParser(_Total, ROUNDACCUMULATEDHOLIDAY))
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetHolidaySpentItem(ByVal dr As DataRow) As HolidaySpentItem
            Return New HolidaySpentItem(dr)
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

            _Type = Utilities.ConvertDatabaseID(Of HolidaySpentItemType)(CIntSafe(dr.Item(0), 0))
            _TypeHumanReadable = Utilities.ConvertLocalizedName(_Type)
            _Spent = CIntSafe(dr.Item(1), 0)
            _Compensated = CDblSafe(dr.Item(2), ROUNDACCUMULATEDHOLIDAY, 0)
            _Correction = CDblSafe(dr.Item(3), ROUNDACCUMULATEDHOLIDAY, 0)
            _DocumentID = CIntSafe(dr.Item(4), 0)
            _DocumentDate = CDateSafe(dr.Item(5), Today)
            _DocumentNumber = CStrSafe(dr.Item(6)).Trim
            _DocumentContent = CStrSafe(dr.Item(7)).Trim

            If _Type = HolidaySpentItemType.Spent Then
                _Total = CRound(Convert.ToDouble(_Spent), ROUNDACCUMULATEDHOLIDAY)
            ElseIf _Type = HolidaySpentItemType.Compensated Then
                _Total = _Compensated
            Else
                _Total = _Correction
            End If

        End Sub

#End Region

    End Class

End Namespace