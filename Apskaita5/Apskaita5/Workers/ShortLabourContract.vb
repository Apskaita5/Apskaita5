Namespace Workers
    <Serializable()> _
    Public Structure ShortLabourContract
        Public Number As Integer
        Public Serial As String
        Public [Date] As Date
        Public Sub New(ByVal nNumber As Integer, ByVal nSerial As String, ByVal nDate As Date)
            Serial = nSerial
            Number = nNumber
            [Date] = nDate.Date
        End Sub
        Public Overrides Function ToString() As String
            Return Serial.Trim & Number.ToString
        End Function
    End Structure
End Namespace

