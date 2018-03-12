Namespace DatabaseAccess.DatabaseStructure

    ''' <summary>
    ''' Interface for creating a custom DB error monitoring class.
    ''' </summary>
    Public Interface IDatabaseStructureErrorManager

        Sub FetchCustomErrors(ByRef StructureErrorList As DatabaseStructureErrorList, _
            ByVal DeFactoDatabaseStructure As DatabaseStructure, ByVal DatabaseName As String)

        Sub RepairCustomError(ByRef StructureErrorList As DatabaseStructureErrorList, _
            ByVal CustomError As DatabaseStructureError)

    End Interface

End Namespace
