Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of <see cref="UnsettledPersonInfoList">UnsettledPersonInfoList</see> report.
    ''' Contains information about a person and total debt.
    ''' </summary>
    ''' <remarks>Should onle be used as a child of <see cref="UnsettledPersonInfoList">UnsettledPersonInfoList</see>.</remarks>
    <Serializable()> _
    Public Class UnsettledPersonInfo
        Inherits ReadOnlyBase(Of UnsettledPersonInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _Code As String = ""
        Private _Address As String = ""
        Private _CodeVat As String = ""
        Private _Email As String = ""
        Private _CodeInternal As String = ""
        Private _ContactInfo As String = ""
        Private _Debt As Double = 0
        Private _Items As UnsettledDocumentInfoList = Nothing

        <NotUndoable(), NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of UnsettledDocumentInfo) = Nothing


        ''' <summary>
        ''' Gets an ID of the person.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the person.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a code of the person.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Code() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Code.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an address of the person.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Address() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Address.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a VAT code of the person.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CodeVat() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CodeVat.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an email of the person.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Email() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Email.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an internal code of the person.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CodeInternal() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CodeInternal.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets contact info for the person.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ContactInfo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContactInfo.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the total debt of (to) the person.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property Debt() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Debt)
            End Get
        End Property


        ''' <summary>
        ''' Gets a list of unsettled documents. 
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Items() As UnsettledDocumentInfoList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Items
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable view of the list of unsettled documents. Used to implement auto sort in a grid.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ItemsSorted() As Csla.SortedBindingList(Of UnsettledDocumentInfo)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _SortedList Is Nothing Then
                    _SortedList = New Csla.SortedBindingList(Of UnsettledDocumentInfo)(_Items)
                End If
                Return _SortedList
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_UnsettledPersonInfo_ToString, _
                _Name, _Code, _Debt.ToString)
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets person info by a database query.
        ''' </summary>
        ''' <param name="myData">a database query result</param>
        ''' <param name="personID">ID of the person to get the info about.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetUnsettledPersonInfo(ByVal myData As DataTable, _
            ByVal personID As Integer) As UnsettledPersonInfo
            Return New UnsettledPersonInfo(myData, personID)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal myData As DataTable, ByVal personID As Integer)
            Fetch(myData, personID)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal personID As Integer)

            For Each dr As DataRow In myData.Rows

                If CIntSafe(dr.Item(6), 0) = personID Then

                    _ID = CIntSafe(dr.Item(6), 0)
                    _Name = CStrSafe(dr.Item(7)).Trim
                    _Code = CStrSafe(dr.Item(8)).Trim
                    _Address = CStrSafe(dr.Item(9)).Trim
                    _CodeVat = CStrSafe(dr.Item(10)).Trim
                    _Email = CStrSafe(dr.Item(11)).Trim
                    _CodeInternal = CStrSafe(dr.Item(12)).Trim
                    _ContactInfo = CStrSafe(dr.Item(13)).Trim
                    _Debt = CDblSafe(dr.Item(14), 2, 0)

                    Exit For

                End If

            Next

            _Items = UnsettledDocumentInfoList.GetUnsettledDocumentInfoList(myData, personID)

        End Sub

#End Region

    End Class

End Namespace