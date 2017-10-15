Imports ApskaitaObjects.Attributes

''' <summary>
''' Represents a type of chronologic relation between operations.
''' </summary>
''' <remarks></remarks>
Public Enum OperationChronologyType

    ''' <summary>
    ''' A chronology without taking into account the current operation date (first or latest).
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(0)> _
    Overall

    ''' <summary>
    ''' The latest operation before the current operation.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(1)> _
    LastBefore

    ''' <summary>
    ''' The first operation after the current operation.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(2)> _
    FirstAfter

End Enum
