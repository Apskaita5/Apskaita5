Namespace Documents

    <Serializable()> _
    Public Class AdvanceReportItemList
        Inherits BusinessListBase(Of AdvanceReportItemList, AdvanceReportItem)

#Region " Business Methods "

        Private _CurrencyRate As Double = 1
        Private _CurrencyCode As String = GetCurrentCompany.BaseCurrency

        Friend ReadOnly Property CurrencyRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrencyRate, 6)
            End Get
        End Property

        Friend ReadOnly Property CurrencyCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrencyCode
            End Get
        End Property


        Friend Sub UpdateCurrencyRate(ByVal nCurrencyRate As Double)

            RaiseListChangedEvents = False

            _CurrencyRate = nCurrencyRate
            For Each o As AdvanceReportItem In Me
                o.UpdateCurrencyRate(nCurrencyRate)
            Next

            RaiseListChangedEvents = True

            Me.ResetBindings()

        End Sub

        Friend Sub UpdateCurrencyCode(ByVal nCurrencyCode As String)

            RaiseListChangedEvents = False

            _CurrencyCode = nCurrencyCode
            For Each o As AdvanceReportItem In Me
                If o.InvoiceID > 0 Then o.UpdateCurrencyCode(nCurrencyCode)
            Next

            RaiseListChangedEvents = True

            Me.ResetBindings()

        End Sub

        Friend Function GetMaxDate() As Date
            If Me.Count < 1 Then Return Date.MinValue
            Dim result As Date = Date.MinValue
            For Each i As AdvanceReportItem In Me
                If i.Date.Date > result.Date Then result = i.Date
            Next
            Return result
        End Function


        Protected Overrides Function AddNewCore() As Object
            Dim NewItem As AdvanceReportItem = AdvanceReportItem.NewAdvanceReportItem
            Me.Add(NewItem)
            Return NewItem
        End Function


        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)

            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewAdvanceReportItemList() As AdvanceReportItemList
            Return New AdvanceReportItemList
        End Function

        Friend Shared Function GetAdvanceReportItemList(ByVal myData As DataTable, _
            ByVal pCurrencyRate As Double, ByVal pCurrencyCode As String, _
            ByVal nFinancialDataCanChange As Boolean) As AdvanceReportItemList
            Return New AdvanceReportItemList(myData, pCurrencyRate, pCurrencyCode, nFinancialDataCanChange)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub


        Private Sub New(ByVal myData As DataTable, ByVal pCurrencyRate As Double, _
            ByVal pCurrencyCode As String, ByVal nFinancialDataCanChange As Boolean)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = nFinancialDataCanChange
            Me.AllowRemove = nFinancialDataCanChange
            Fetch(myData, pCurrencyRate, pCurrencyCode, nFinancialDataCanChange)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal pCurrencyRate As Double, _
            ByVal pCurrencyCode As String, ByVal nFinancialDataCanChange As Boolean)

            RaiseListChangedEvents = False

            _CurrencyRate = pCurrencyRate
            _CurrencyCode = pCurrencyCode

            For Each dr As DataRow In myData.Rows
                Add(AdvanceReportItem.GetAdvanceReportItem(dr, pCurrencyRate, _
                    pCurrencyCode, nFinancialDataCanChange))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As AdvanceReport)

            RaiseListChangedEvents = False

            If parent.ChronologicValidator.FinancialDataCanChange Then
                For Each item As AdvanceReportItem In DeletedList
                    If Not item.IsNew Then item.DeleteSelf()
                Next
            End If
            DeletedList.Clear()

            For Each item As AdvanceReportItem In Me
                If item.IsNew AndAlso parent.ChronologicValidator.FinancialDataCanChange Then
                    item.Insert(parent)
                ElseIf Not item.IsNew AndAlso item.IsDirty Then
                    item.Update(parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace