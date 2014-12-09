Imports System.Data.SQLite
<SQLiteFunction(Name:="CROUND", Arguments:=2, FuncType:=FunctionType.Scalar)> _
Public Class SQLiteRound
    Inherits SQLite.SQLiteFunction

    Public Overrides Function Invoke(ByVal args() As Object) As Object
        Dim argument As Double = 0
        If Not IsDBNull(args(0)) Then
            Try
                argument = Convert.ToDouble(args(0))
            Catch ex As Exception
                Return 0
            End Try
        Else
            Return 0
        End If
        Return CRound(argument, Convert.ToInt32(args(1)))
    End Function

    Private Function CRound(ByVal d As Double, ByVal r As Integer) As Double
        Dim i As Long = CLng(Math.Floor(d * Math.Pow(10, r)))
        If i + 0.5 > CType(d * Math.Pow(10, r), Decimal) Then
            Return i / Math.Pow(10, r)
        Else
            Return (i + 1) / Math.Pow(10, r)
        End If
    End Function

End Class