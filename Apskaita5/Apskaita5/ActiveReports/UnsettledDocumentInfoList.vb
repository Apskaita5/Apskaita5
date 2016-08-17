Namespace ActiveReports

    ''' <summary>
    ''' Represents a list of the unsettled documents for <see cref="UnsettledPersonInfo">UnsettledPersonInfo</see>
    ''' as part of <see cref="UnsettledPersonInfoList">UnsettledPersonInfoList</see> report.
    ''' </summary>
    ''' <remarks>Should onle be used as a child of <see cref="UnsettledPersonInfo">UnsettledPersonInfo</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class UnsettledDocumentInfoList
        Inherits ReadOnlyListBase(Of UnsettledDocumentInfoList, UnsettledDocumentInfo)

#Region " Business Methods "

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets unsettled documents info by a database query.
        ''' </summary>
        ''' <param name="myData">a database query result</param>
        ''' <param name="personID">ID of the person to get the info about.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetUnsettledDocumentInfoList(ByVal myData As DataTable, _
            ByVal personID As Integer) As UnsettledDocumentInfoList
            Return New UnsettledDocumentInfoList(myData, personID)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal myData As DataTable, ByVal personID As Integer)
            ' require use of factory methods
            Fetch(myData, personID)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal personID As Integer)

            RaiseListChangedEvents = False
            IsReadOnly = False

            Dim actualDebt As Double = 0
            Dim totalDebt As Double = 0

            For Each dr As DataRow In myData.Rows
                If CIntSafe(dr.Item(6), 0) = personID Then
                    If Not actualDebt > 0 Then actualDebt = CDblSafe(dr.Item(14), 2, 0)
                    Add(UnsettledDocumentInfo.GetUnsettledDocumentInfo(dr))
                    totalDebt = CRound(totalDebt + Me.Item(Me.Count - 1).SumInDocument)
                End If
            Next

            AdjustLastSumInDocument(actualDebt, totalDebt)

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

        Private Sub AdjustLastSumInDocument(ByVal actualDebt As Double, ByVal totalDebt As Double)

            If Me.Count < 1 Then Exit Sub

            If CRound(totalDebt - actualDebt, 2) = Me.Item(Me.Count - 1).SumInDocument Then
                Me.RemoveAt(Me.Count - 1)
            ElseIf CRound(totalDebt - actualDebt, 2) > 0 Then
                Me.Item(Me.Count - 1).AdjustDebt(CRound(totalDebt - actualDebt, 2))
            End If

        End Sub

#End Region

    End Class

End Namespace