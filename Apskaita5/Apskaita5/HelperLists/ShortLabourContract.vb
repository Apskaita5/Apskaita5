Namespace HelperLists

    ''' <summary>
    ''' A value object that provides serial, number and date of an existing labour contract.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="ShortLabourContractList">ShortLabourContractList</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class ShortLabourContract
        Inherits ReadOnlyBase(Of ServiceInfo)
        Implements IValueObject, IComparable

        Private ReadOnly _Guid As Guid = Guid.NewGuid()


        ''' <summary>
        ''' Gets whether an object is a place holder (does not represent a real labour contract).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObject.IsEmpty
            Get
                Return Not Number > 0
            End Get
        End Property

        ''' <summary>
        ''' Number of a labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Number As Integer = 0

        ''' <summary>
        ''' Serial of a labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Serial As String = ""

        ''' <summary>
        ''' Date of a labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly [Date] As Date = DateTime.MinValue


        Friend Sub New(ByVal nNumber As Integer, ByVal nSerial As String, ByVal nDate As Date)
            Serial = nSerial
            Number = nNumber
            [Date] = nDate.Date
        End Sub

        Private Sub New()

        End Sub


        Public Shared Operator =(ByVal a As ShortLabourContract, ByVal b As ShortLabourContract) As Boolean

            Dim aId, bId As String
            If a Is Nothing OrElse a.IsEmpty Then
                aId = ""
            Else
                aId = a.Serial.Trim.ToUpper & a.Number.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bId = ""
            Else
                bId = b.Serial.Trim.ToUpper & b.Number.ToString
            End If

            Return aId = bId

        End Operator

        Public Shared Operator <>(ByVal a As ShortLabourContract, ByVal b As ShortLabourContract) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As ShortLabourContract, ByVal b As ShortLabourContract) As Boolean

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

        Public Shared Operator <(ByVal a As ShortLabourContract, ByVal b As ShortLabourContract) As Boolean

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
            Dim tmp As ShortLabourContract = TryCast(obj, ShortLabourContract)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Private Shared _Empty As ShortLabourContract = Nothing

        ''' <summary>
        ''' Gets an empty ShortLabourContract (placeholder).
        ''' </summary>
        Public Shared Function Empty() As ShortLabourContract
            If _Empty Is Nothing Then
                _Empty = New ShortLabourContract
            End If
            Return _Empty
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            If Not Number > 0 Then Return ""
            Return String.Format(My.Resources.HelperLists_ShortLabourContract_ToString, _
                [Date].ToString("yyyy-MM-dd"), Serial, Number.ToString)
        End Function

    End Class

End Namespace

