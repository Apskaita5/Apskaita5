Imports ApskaitaObjects.Assets

Namespace ActiveReports

    ''' <summary>
    ''' Represents a collection of <see cref="LongTermAssetOperationInfo">LongTermAssetOperationInfo</see> 
    ''' as part of the <see cref="LongTermAssetOperationInfoListParent">LongTermAssetOperationInfoListParent</see> report.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="LongTermAssetOperationInfoListParent">LongTermAssetOperationInfoListParent</see>.
    ''' Values are stored in the database table turtas_op.</remarks>
    <Serializable()> _
Public NotInheritable Class LongTermAssetOperationInfoList
        Inherits ReadOnlyListBase(Of LongTermAssetOperationInfoList, LongTermAssetOperationInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAssetOperationInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetList(ByVal parent As LongTermAssetOperationInfoListParent, _
            ByVal dt As DataTable) As LongTermAssetOperationInfoList
            Return New LongTermAssetOperationInfoList(parent, dt)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal parent As LongTermAssetOperationInfoListParent, ByVal dt As DataTable)
            Fetch(parent, dt)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal parent As LongTermAssetOperationInfoListParent, _
            ByVal dt As DataTable)

            Dim nBeforeOperationAcquisitionAccountValue As Double = _
                parent.AcquisitionAccountValue
            Dim nBeforeOperationAcquisitionAccountValuePerUnit As Double = _
                parent.AcquisitionAccountValuePerUnit
            Dim nBeforeOperationAmortizationAccountValue As Double = _
                parent.AmortizationAccountValue
            Dim nBeforeOperationAmortizationAccountValuePerUnit As Double = _
                parent.AmortizationAccountValuePerUnit
            Dim nBeforeOperationValueDecreaseAccountValue As Double = _
                parent.ValueDecreaseAccountValue
            Dim nBeforeOperationValueDecreaseAccountValuePerUnit As Double = _
                parent.ValueDecreaseAccountValuePerUnit
            Dim nBeforeOperationValueIncreaseAccountValue As Double = _
                parent.ValueIncreaseAccountValue
            Dim nBeforeOperationValueIncreaseAccountValuePerUnit As Double = _
                parent.ValueIncreaseAccountValuePerUnit
            Dim nBeforeOperationValueIncreaseAmmortizationAccountValue As Double = _
                parent.ValueIncreaseAmmortizationAccountValue
            Dim nBeforeOperationValueIncreaseAmmortizationAccountValuePerUnit As Double = _
                parent.ValueIncreaseAmmortizationAccountValuePerUnit
            Dim nBeforeOperationValue As Double = parent.Value
            Dim nBeforeOperationValuePerUnit As Double = parent.ValuePerUnit
            Dim nBeforeOperationAmmount As Integer = parent.Ammount

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As DataRow In dt.Rows
                Add(LongTermAssetOperationInfo.GetLongTermAssetOperationInfo(dr, _
                    nBeforeOperationAcquisitionAccountValue, _
                    nBeforeOperationAcquisitionAccountValuePerUnit, _
                    nBeforeOperationAmortizationAccountValue, _
                    nBeforeOperationAmortizationAccountValuePerUnit, _
                    nBeforeOperationValueDecreaseAccountValue, _
                    nBeforeOperationValueDecreaseAccountValuePerUnit, _
                    nBeforeOperationValueIncreaseAccountValue, _
                    nBeforeOperationValueIncreaseAccountValuePerUnit, _
                    nBeforeOperationValueIncreaseAmmortizationAccountValue, _
                    nBeforeOperationValueIncreaseAmmortizationAccountValuePerUnit, _
                    nBeforeOperationValue, nBeforeOperationValuePerUnit, nBeforeOperationAmmount))
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace