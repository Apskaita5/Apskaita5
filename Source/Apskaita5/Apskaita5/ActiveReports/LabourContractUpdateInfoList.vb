Namespace ActiveReports

    ''' <summary>
    ''' Represents a list of <see cref="Workers.Contract">labour contract's updates</see>.
    ''' </summary>
    ''' <remarks>Should only be used as a child of a <see cref="ContractInfo">ContractInfo</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class LabourContractUpdateInfoList
        Inherits ReadOnlyListBase(Of LabourContractUpdateInfoList, LabourContractUpdateInfo)

#Region " Business Methods "

#End Region

#Region " Factory Methods "

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of LabourContractUpdateInfo) = Nothing

        Friend Shared Function GetLabourContractUpdateInfoList(ByVal myData As DataTable, _
            ByVal contractSerial As String, ByVal contractNumber As Integer) As LabourContractUpdateInfoList
            Dim result As LabourContractUpdateInfoList = New LabourContractUpdateInfoList( _
                myData, contractSerial, contractNumber)
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

        Private Sub New(ByVal myData As DataTable, ByVal contractSerial As String, _
            ByVal contractNumber As Integer)
            ' require use of factory methods
            Fetch(myData, contractSerial, contractNumber)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal contractSerial As String, _
            ByVal contractNumber As Integer)

            If Not contractNumber > 0 OrElse myData Is Nothing OrElse Not myData.Rows.Count > 0 Then Exit Sub

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As DataRow In myData.Rows
                If CStrSafe(dr.Item(1)).Trim.ToUpper = contractSerial.Trim.ToUpper _
                    AndAlso CIntSafe(dr.Item(2), 0) = contractNumber Then _
                    Add(LabourContractUpdateInfo.GetLabourContractUpdateInfo(dr))
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace