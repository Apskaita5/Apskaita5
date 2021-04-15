Imports ApskaitaObjects.General
Imports AccControlsWinForms

Public Class F_SharesClassList
    Implements ISingleInstanceForm

    Private _FormManager As CslaActionExtenderEditForm(Of SharesClassList)
    Private _ListViewManager As DataListViewEditControlManager(Of SharesClass)
    Private _QueryManager As CslaActionExtenderQueryObject


    Private Sub F_SharesClassList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        Try

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            ' SharesClassList.GetSharesClassList()
            _QueryManager.InvokeQuery(Of SharesClassList)(Nothing, "GetSharesClassList",
                True, AddressOf OnDataSourceLoaded)

        Catch ex As Exception
            ShowError(New Exception("Klaida. Nepavyko gauti akcijų klasių duomenų.", ex), Nothing)
            DisableAllControls(Me)
        End Try

    End Sub

    Private Function SetDataSources() As Boolean

        Try

            _ListViewManager = New DataListViewEditControlManager(Of SharesClass) _
                (ItemsDataListView, Nothing, AddressOf DeleteItems,
                 AddressOf AddNewItem, Nothing, Nothing)

        Catch ex As Exception
            ShowError(New Exception("Klaida. Nepavyko gauti akcijų klasių duomenų.", ex), Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        nOkButton.Enabled = SharesClassList.CanEditObject
        Me.ApplyButton.Enabled = SharesClassList.CanEditObject

        Return True

    End Function

    Private Sub OnDataSourceLoaded(ByVal source As Object, ByVal exceptionHandled As Boolean)

        If exceptionHandled Then

            DisableAllControls(Me)
            Exit Sub

        ElseIf source Is Nothing Then

            ShowError(New Exception("Klaida. Nepavyko gauti akcijų klasių duomenų."), Nothing)
            DisableAllControls(Me)
            Exit Sub

        End If

        Try

            _FormManager = New CslaActionExtenderEditForm(Of SharesClassList) _
                (Me, Me.SharesClassListBindingSource, DirectCast(source, SharesClassList),
                 Nothing, nOkButton, ApplyButton, nCancelButton, Nothing, ProgressFiller1)

            _FormManager.ManageDataListViewStates(ItemsDataListView)

        Catch ex As Exception
            ShowError(New Exception("Klaida. Nepavyko gauti akcijų klasių duomenų.", ex), Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

    End Sub


    Private Sub DeleteItems(ByVal items As SharesClass())
        If items Is Nothing OrElse items.Length < 1 Then Exit Sub

        For Each item As SharesClass In items
            If item.IsInUse Then
                MsgBox(String.Format("Klaida. Akcijų klasė {0} buvo naudojama registruojant operacijas. Jos pašalinti neleidžiama.",
                    item.Name), MsgBoxStyle.Exclamation, "Klaida")
                Exit Sub
            End If
        Next

        For Each item As SharesClass In items
            _FormManager.DataSource.Remove(item)
        Next

    End Sub

    Private Sub AddNewItem()
        _FormManager.DataSource.AddNew()
    End Sub

End Class
