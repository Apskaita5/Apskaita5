Namespace Assets

    ''' <summary>
    ''' Represents a helper object that contains information about a changes
    ''' in long term asset financial state (accounts, accounts balancies, amortization period)
    ''' and provides functionality to calculate state for a given date.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="OperationBackground">OperationBackground</see>.</remarks>
    <Serializable()> _
    Public Class OperationDeltaList
        Inherits ReadOnlyListBase(Of OperationDeltaList, OperationDelta)

#Region " Business Methods "

        Public Function GetDateMax(ByVal operationTypes As LtaOperationType()) As Date

            Dim result As Date = Date.MinValue

            If operationTypes Is Nothing OrElse operationTypes.Length < 1 Then

                For Each i As OperationDelta In Me
                    If i.Date.Date > result.Date Then result = i.Date
                Next

            Else

                For Each i As OperationDelta In Me
                    If Not Array.IndexOf(operationTypes, i.OperationType) < 0 _
                        AndAlso i.Date.Date > result.Date Then result = i.Date
                Next

            End If

            Return result

        End Function

        Public Function GetDateMin(ByVal operationTypes As LtaOperationType()) As Date

            Dim result As Date = Date.MaxValue

            If operationTypes Is Nothing OrElse operationTypes.Length < 1 Then

                For Each i As OperationDelta In Me
                    If i.Date.Date < result.Date Then result = i.Date
                Next

            Else

                For Each i As OperationDelta In Me
                    If Not Array.IndexOf(operationTypes, i.OperationType) < 0 _
                        AndAlso i.Date.Date < result.Date Then result = i.Date
                Next

            End If

            Return result

        End Function

        Public Function GetDateLastBefore(ByVal operationDate As Date, _
            ByVal operationTypes As LtaOperationType()) As Date

            Dim result As Date = Date.MinValue

            If operationTypes Is Nothing OrElse operationTypes.Length < 1 Then

                For Each i As OperationDelta In Me
                    If i.Date.Date <= operationDate.Date AndAlso _
                        i.Date.Date > result.Date Then result = i.Date
                Next

            Else

                For Each i As OperationDelta In Me
                    If i.Date.Date <= operationDate.Date AndAlso _
                        Not Array.IndexOf(operationTypes, i.OperationType) < 0 _
                        AndAlso i.Date.Date > result.Date Then result = i.Date
                Next

            End If

            Return result

        End Function

        Public Function GetDateFirstAfter(ByVal operationDate As Date, _
            ByVal operationTypes As LtaOperationType()) As Date

            Dim result As Date = Date.MaxValue

            If operationTypes Is Nothing OrElse operationTypes.Length < 1 Then

                For Each i As OperationDelta In Me
                    If i.Date.Date > operationDate.Date AndAlso _
                        i.Date.Date < result.Date Then result = i.Date
                Next

            Else

                For Each i As OperationDelta In Me
                    If i.Date.Date > operationDate.Date AndAlso _
                        Not Array.IndexOf(operationTypes, i.OperationType) < 0 _
                        AndAlso i.Date.Date < result.Date Then result = i.Date
                Next

            End If

            Return result

        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetList(ByVal assetID As Integer, _
            ByVal currentOperationID As Integer, ByVal myData As DataTable) As OperationDeltaList
            Return New OperationDeltaList(assetID, currentOperationID, myData)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal assetID As Integer, ByVal currentOperationID As Integer, _
            ByVal myData As DataTable)
            Fetch(assetID, currentOperationID, myData)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal assetID As Integer, ByVal currentOperationID As Integer)

            If Not assetID > 0 Then
                Throw New ArgumentNullException("assetID")
            End If

            Dim myComm As New SQLCommand("FetchOperationBackgroundDeltas")
            myComm.AddParam("?AD", assetID)

            Using myData As DataTable = myComm.Fetch()
                Fetch(assetID, currentOperationID, myData)
            End Using

        End Sub

        Private Sub Fetch(ByVal assetID As Integer, ByVal currentOperationID As Integer, _
            ByVal myData As DataTable)

            If myData Is Nothing Then
                Fetch(assetID, currentOperationID)
                Exit Sub
            End If

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As DataRow In myData.Rows
                If CIntSafe(dr.Item(0), 0) = assetID AndAlso CIntSafe(dr.Item(1), 0) _
                    <> currentOperationID Then
                    Add(OperationDelta.GetOperationDelta(dr))
                End If
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace