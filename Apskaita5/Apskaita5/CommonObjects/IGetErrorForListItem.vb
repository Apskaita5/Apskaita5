''' <summary>
''' An interface used by <see cref="Utilities.GetAllBrokenRulesForList">GetAllBrokenRulesForList</see> 
''' and <see cref="Utilities.GetAllWarningsForList">GetAllWarningsForList</see> methods 
''' to genericaly aggregate all the error/warning messages in <see cref="Csla.BusinessListBase(Of T, C)">BusinessListBase(Of T, C)</see>.
''' </summary>
''' <remarks></remarks>
Public Interface IGetErrorForListItem

    ''' <summary>
    ''' Gets an error message for child that includes child description and broken rules description.
    ''' </summary>
    ''' <remarks></remarks>
    Function GetErrorString() As String

    ''' <summary>
    ''' Gets a warning message for child that includes child description and broken rules description.
    ''' </summary>
    ''' <remarks></remarks>
    Function GetWarningString() As String

End Interface
