Imports ApskaitaObjects.Attributes

Namespace Settings

    ''' <summary>
    ''' Represents a type of names (predefined single string values) used in business objects.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum NameType

        ''' <summary>
        ''' A name of social security (SODRA) administrative branch. 
        ''' Used in various SODRA declarations.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        SodraBranch

        ''' <summary>
        ''' A name of a long term asset group (class) defined by Corporative income tax law.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        LongTermAssetLegalGroup

    End Enum

End Namespace
