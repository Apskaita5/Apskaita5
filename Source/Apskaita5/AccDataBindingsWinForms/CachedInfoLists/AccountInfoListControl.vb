Imports AccControlsWinForms

Namespace CachedInfoLists

    Public Class AccountInfoListControl
        Inherits InfoListControl

        Public Overrides Sub AddNewItem()
            OpenNewForm(Of General.AccountList)()
        End Sub

    End Class

End Namespace
