Namespace ActiveReports

    <Serializable()> _
    Public Class LabourContractUpdateInfoList
        Inherits ReadOnlyListBase(Of LabourContractUpdateInfoList, LabourContractUpdateInfo)

#Region " Business Methods "

#End Region

#Region " Factory Methods "

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of LabourContractUpdateInfo) = Nothing

        Friend Shared Function GetLabourContractUpdateInfoList(ByVal myData As DataTable, _
            ByVal ContractSerial As String, ByVal ContractNumber As Integer) As LabourContractUpdateInfoList
            Dim result As LabourContractUpdateInfoList = New LabourContractUpdateInfoList( _
                myData, ContractSerial, ContractNumber)
            Return result
        End Function

        Public Function GetSortedList() As Csla.SortedBindingList(Of LabourContractUpdateInfo)
            If _SortedList Is Nothing Then _SortedList = _
                New Csla.SortedBindingList(Of LabourContractUpdateInfo)(Me)
            Return _SortedList
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal myData As DataTable, ByVal ContractSerial As String, _
            ByVal ContractNumber As Integer)
            ' require use of factory methods
            Fetch(myData, ContractSerial, ContractNumber)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal ContractSerial As String, _
            ByVal ContractNumber As Integer)

            If Not ContractNumber > 0 OrElse myData Is Nothing OrElse Not myData.Rows.Count > 0 Then Exit Sub

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As DataRow In myData.Rows
                If CStrSafe(dr.Item(1)).Trim.ToUpper = ContractSerial.Trim.ToUpper _
                    AndAlso CIntSafe(dr.Item(2), 0) = ContractNumber Then _
                    Add(LabourContractUpdateInfo.GetLabourContractUpdateInfo(dr))
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace