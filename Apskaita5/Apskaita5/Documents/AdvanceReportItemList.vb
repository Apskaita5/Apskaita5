Namespace Documents

    ''' <summary>
    ''' Represents a collection of an advance report document items. Contains info about documents:  
    ''' (a) that were payed by the accountable person and needs to be reimbursed;
    ''' (b) under which the money was received by the accountable person and needs to be redeemed by the company.
    ''' </summary>
    ''' <remarks>Should only be used as a child of a <see cref="AdvanceReport">AdvanceReport</see>.
    ''' Values are stored in the database table apyskaitos.</remarks>
    <Serializable()> _
    Public Class AdvanceReportItemList
        Inherits BusinessListBase(Of AdvanceReportItemList, AdvanceReportItem)

#Region " Business Methods "

        Private _CurrencyRate As Double = 1
        Private _CurrencyCode As String = GetCurrentCompany.BaseCurrency

        ''' <summary>
        ''' Currency rate of the parent advance report.
        ''' </summary>
        ''' <remarks>Used to transfer parent data to items and avoid circular references.</remarks>
        Friend ReadOnly Property CurrencyRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrencyRate, 6)
            End Get
        End Property

        ''' <summary>
        ''' Currency code of the parent advance report.
        ''' </summary>
        ''' <remarks>Used to transfer parent data to items and avoid circular references.</remarks>
        Friend ReadOnly Property CurrencyCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrencyCode
            End Get
        End Property


        ''' <summary>
        ''' Updates item data when the parent currency data changes.
        ''' </summary>
        ''' <param name="nCurrencyCode">New currency code of the parent advance report.</param>
        ''' <param name="nCurrencyRate">New currency rate of the parent advance report.</param>
        ''' <remarks></remarks>
        Friend Sub UpdateCurrency(ByVal nCurrencyCode As String, ByVal nCurrencyRate As Double)

            RaiseListChangedEvents = False

            _CurrencyRate = nCurrencyRate
            _CurrencyCode = nCurrencyCode

            Dim baseCurrency As String = GetCurrentCompany.BaseCurrency

            For Each o As AdvanceReportItem In Me
                o.UpdateCurrency(_CurrencyRate, _CurrencyCode, baseCurrency)
            Next

            RaiseListChangedEvents = True

            Me.ResetBindings()

        End Sub

        ''' <summary>
        ''' Gets a maximum (latest) itam date within the list. Returnes Date.MinValue if no item in the list.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Function GetMaxDate() As Date
            If Me.Count < 1 Then Return Date.MinValue
            Dim result As Date = Date.MinValue
            For Each i As AdvanceReportItem In Me
                If i.Date.Date > result.Date Then result = i.Date
            Next
            Return result
        End Function


        Protected Overrides Function AddNewCore() As Object
            Dim newItem As AdvanceReportItem = AdvanceReportItem.NewAdvanceReportItem
            Me.Add(newItem)
            Return newItem
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

        Public Function HasWarnings() As Boolean
            For Each i As AdvanceReportItem In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewAdvanceReportItemList() As AdvanceReportItemList
            Return New AdvanceReportItemList
        End Function

        Friend Shared Function GetAdvanceReportItemList(ByVal myData As DataTable, _
            ByVal pCurrencyRate As Double, ByVal pCurrencyCode As String, _
            ByVal nFinancialDataCanChange As Boolean, ByRef fetchWarnings As String) As AdvanceReportItemList
            Return New AdvanceReportItemList(myData, pCurrencyRate, pCurrencyCode, nFinancialDataCanChange, fetchWarnings)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub


        Private Sub New(ByVal myData As DataTable, ByVal pCurrencyRate As Double, _
            ByVal pCurrencyCode As String, ByVal nFinancialDataCanChange As Boolean, ByRef fetchWarnings As String)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = nFinancialDataCanChange
            Me.AllowRemove = nFinancialDataCanChange
            Fetch(myData, pCurrencyRate, pCurrencyCode, nFinancialDataCanChange, fetchWarnings)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal pCurrencyRate As Double, _
            ByVal pCurrencyCode As String, ByVal nFinancialDataCanChange As Boolean, _
            ByRef fetchWarnings As String)

            RaiseListChangedEvents = False

            _CurrencyRate = pCurrencyRate
            _CurrencyCode = pCurrencyCode

            For Each dr As DataRow In myData.Rows
                Add(AdvanceReportItem.GetAdvanceReportItem(dr, pCurrencyRate, _
                    pCurrencyCode, nFinancialDataCanChange, fetchWarnings))
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