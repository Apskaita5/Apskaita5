Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="ApskaitaObjects.Settings.DocumentSerial">document serial</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table serijos.</remarks>
    <Serializable()> _
    Public NotInheritable Class DocumentSerialInfo
        Inherits ReadOnlyBase(Of DocumentSerialInfo)
        Implements IValueObject, IComparable

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Serial As String = ""
        Private _DocumentType As Settings.DocumentSerialType = Settings.DocumentSerialType.Invoice
        Private _DocumentTypeHumanReadable As String = ""


        ''' <summary>
        ''' Whether an object is a place holder (does not represent a real document serial).
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
        ''' Gets an ID of the serial that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database table serijos.Serijos_ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a serial.
        ''' </summary>
        ''' <remarks>Value is stored in the database table serijos.Serija.</remarks>
        Public ReadOnly Property Serial() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Serial.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a document type that the serial is ment for.
        ''' </summary>
        ''' <remarks>Value is stored in the database table serijos.Serijos_dok.</remarks>
        Public ReadOnly Property DocumentType() As Settings.DocumentSerialType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentType
            End Get
        End Property

        ''' <summary>
        ''' Gets a document type that the serial is ment for as a localized human readable string.
        ''' </summary>
        ''' <remarks>Value is stored in the database table serijos.Serijos_dok.</remarks>
        Public ReadOnly Property DocumentTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentTypeHumanReadable
            End Get
        End Property


        Public Shared Operator =(ByVal a As DocumentSerialInfo, ByVal b As DocumentSerialInfo) As Boolean

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

        Public Shared Operator <>(ByVal a As DocumentSerialInfo, ByVal b As DocumentSerialInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As DocumentSerialInfo, ByVal b As DocumentSerialInfo) As Boolean

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

        Public Shared Operator <(ByVal a As DocumentSerialInfo, ByVal b As DocumentSerialInfo) As Boolean

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
            Dim tmp As DocumentSerialInfo = TryCast(obj, DocumentSerialInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Serial
        End Function

#End Region

#Region " Factory Methods "

        Private Shared _Empty As DocumentSerialInfo = Nothing

        ''' <summary>
        ''' Gets an empty DocumentSerialInfo (placeholder).
        ''' </summary>
        Public Shared Function Empty() As DocumentSerialInfo
            If _Empty Is Nothing Then
                _Empty = New DocumentSerialInfo
            End If
            Return _Empty
        End Function

        Friend Shared Function GetDocumentSerialInfo(ByVal dr As DataRow) As DocumentSerialInfo
            Return New DocumentSerialInfo(dr)
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
            _DocumentType = Utilities.ConvertDatabaseCharID(Of Settings.DocumentSerialType) _
                (CStrSafe(dr.Item(1)))
            _DocumentTypeHumanReadable = Utilities.ConvertLocalizedName(_DocumentType)
            _Serial = CStrSafe(dr.Item(2)).Trim

        End Sub

#End Region

    End Class

End Namespace
