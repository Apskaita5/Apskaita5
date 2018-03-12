''' <summary>
''' Atsiskaitymo lapelio printinimui.
''' </summary>
''' <remarks></remarks>
Public Class WageSheetItem

    Public ReadOnly Sheet As Workers.WageSheet
    Public ReadOnly Item As Workers.WageItem

    Public Sub New(ByVal currenSheet As Workers.WageSheet, ByVal currentItem As Workers.WageItem)
        Sheet = currenSheet
        Item = currentItem
    End Sub

End Class
