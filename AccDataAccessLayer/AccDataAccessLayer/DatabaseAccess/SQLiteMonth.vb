Imports System.Data.SQLite
<SQLiteFunction(Name:="MONTH", Arguments:=1, FuncType:=FunctionType.Scalar)> _
Public Class SQLiteMonth
    Inherits SQLite.SQLiteFunction

    Public Overrides Function Invoke(ByVal args As Object()) As Object
        Dim result As Date = Date.MinValue
        If args Is Nothing OrElse Not args.Length = 1 OrElse _
            Not Date.TryParse(args(0).ToString, result) Then Return 0
        Return result.Month
    End Function

End Class
