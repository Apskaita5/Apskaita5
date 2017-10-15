Imports System.Data.SQLite
<SQLiteFunction(Name:="CONCAT", Arguments:=-1, FuncType:=FunctionType.Scalar)> _
Public Class SQLiteConcat
    Inherits SQLite.SQLiteFunction

    Public Overrides Function Invoke(ByVal args() As Object) As Object
        Return String.Concat(args)
    End Function

End Class