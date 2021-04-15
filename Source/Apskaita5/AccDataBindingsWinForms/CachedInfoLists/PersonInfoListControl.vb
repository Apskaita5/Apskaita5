Imports AccControlsWinForms

Namespace CachedInfoLists

    Public Class PersonInfoListControl
        Inherits InfoListControl

        Public Overrides Sub AddNewItem()
            OpenNewForm(Of General.Person)()
        End Sub

    End Class
End Namespace
