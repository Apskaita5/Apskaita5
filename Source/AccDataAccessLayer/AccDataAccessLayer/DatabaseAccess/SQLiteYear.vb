Imports System.Data.SQLite
<SQLiteFunction(Name:="YEAR", Arguments:=1, FuncType:=FunctionType.Scalar)> _
Public Class SQLiteYear
    Inherits SQLite.SQLiteFunction

    Public Overrides Function Invoke(ByVal args As Object()) As Object
        Dim result As Date = Date.MinValue
        If args Is Nothing OrElse Not args.Length = 1 OrElse _
            Not Date.TryParse(args(0).ToString, result) Then Return 0
        Return result.Year
    End Function

End Class
