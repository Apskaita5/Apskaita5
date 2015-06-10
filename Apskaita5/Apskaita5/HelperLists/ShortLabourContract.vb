Namespace HelperLists

    ''' <summary>
    ''' A value object that provides serial, number and date of an existing labour contract.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="ShortLabourContractList">ShortLabourContractList</see>.</remarks>
    <Serializable()> _
    Public Structure ShortLabourContract

        ''' <summary>
        ''' Number of a labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Number As Integer
        ''' <summary>
        ''' Serial of a labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Serial As String
        ''' <summary>
        ''' Date of a labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly [Date] As Date

        Friend Sub New(ByVal nNumber As Integer, ByVal nSerial As String, ByVal nDate As Date)
            Serial = nSerial
            Number = nNumber
            [Date] = nDate.Date
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.HelperLists_ShortLabourContract_ToString, _
                [Date].ToString("yyyy-MM-dd"), Serial, Number.ToString)
        End Function

    End Structure

End Namespace

