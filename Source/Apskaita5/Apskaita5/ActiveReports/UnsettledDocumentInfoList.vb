Imports ApskaitaObjects.My.Resources

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

        Friend Function GetDescriptionForExportedPayment() As String
            Dim docs As New List(Of String)
            For Each doc As UnsettledDocumentInfo In Me
                docs.Add(doc.GetDescriptionForPayment())
            Next
            Return string.Format(ActiveReports_UnsettledDocumentInfoList_DescriptionForExportedPayment, _
                                 String.Join(", ", docs.ToArray()))
        End Function

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

                    Dim document As UnsettledDocumentInfo = UnsettledDocumentInfo.GetUnsettledDocumentInfo(dr)

                    Add(document)

                    totalDebt = CRound(totalDebt + document.SumInDocument)

                    If totalDebt >= actualDebt Then
                        document.AdjustDebt(CRound(totalDebt - actualDebt, 2))
                        Exit For
                    End If

                End If
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace