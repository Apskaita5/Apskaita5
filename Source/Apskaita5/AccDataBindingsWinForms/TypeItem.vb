''' <summary>
''' Represents a wrapper for a type in order to provide human readable type name.
''' </summary>
''' <remarks></remarks>
Public Class TypeItem

    ''' <summary>
    ''' An undelying type of the item.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly [Type] As Type

    ''' <summary>
    ''' A human readable localized name of the type.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Name As String = ""


    Public Sub New(ByVal ofType As Type, ByVal typeName As String)
        If ofType Is Nothing Then
            Throw New ArgumentNullException("ofType")
        End If
        [Type] = ofType
        If StringIsNullOrEmpty(typeName) Then typeName = ofType.FullName
        Name = typeName
    End Sub


    Public Overrides Function ToString() As String
        Return Name
    End Function

End Class
