Imports ApskaitaObjects.My.Resources

Namespace ActiveReports

    ''' <summary>
    ''' Represents a VAT declaration with extended data to allow
    ''' an accountant to inspect VAT declaration details.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class VatDeclaration
        Inherits ReadOnlyBase(Of VatDeclaration)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Date As Date = Today
        Private _Year As Integer = Today.AddMonths(-1).Year
        Private _Month As Integer = Today.AddMonths(-1).Month
        Private _Items As VatDeclarationItemList = Nothing
        Private _Subtotals As VatDeclarationSubtotalList = Nothing
        Private _CustomPeriod As Boolean = False
        Private _DateFrom As Date = New Date(Today.AddMonths(-1).Year, _
            Today.AddMonths(-1).Month, 1)
        Private _DateTo As Date = New Date(Today.AddMonths(-1).Year, _
            Today.AddMonths(-1).Month, Date.DaysInMonth(Today.AddMonths(-1).Year, _
            Today.AddMonths(-1).Month))


        ''' <summary>
        ''' Gets a date of the VAT declaration.
        ''' </summary>
        '''  <remarks></remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a year that the VAT declaration data is fetched for.
        ''' </summary>
        '''  <remarks></remarks>
        Public ReadOnly Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
        End Property

        ''' <summary>
        ''' Gets a month that the VAT declaration data is fetched for.
        ''' </summary>
        '''  <remarks></remarks>
        Public ReadOnly Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the VAT declaration is fetched for a custom period
        ''' (not a month).
        ''' </summary>
        '''  <remarks></remarks>
        Public ReadOnly Property CustomPeriod() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CustomPeriod
            End Get
        End Property

        ''' <summary>
        ''' Gets a starting date of the VAT declaration period.
        ''' </summary>
        '''  <remarks></remarks>
        Public ReadOnly Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateFrom
            End Get
        End Property

        ''' <summary>
        ''' Gets an ending date of the VAT declaration period.
        ''' </summary>
        '''  <remarks></remarks>
        Public ReadOnly Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateTo
            End Get
        End Property

        ''' <summary>
        ''' Gets a declaration items - a list of the declaration field entries
        ''' (sums) for each appropriate document and field code.
        ''' </summary>
        '''  <remarks></remarks>
        Public ReadOnly Property Items() As VatDeclarationItemList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Items
            End Get
        End Property

        ''' <summary>
        ''' Gets a declaration items subtotals - a list of values (sums)
        ''' for each of the the declaration field.
        ''' </summary>
        '''  <remarks></remarks>
        Public ReadOnly Property Subtotals() As VatDeclarationSubtotalList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Subtotals
            End Get
        End Property


        ''' <summary>
        ''' Exports the declaration to a ffdata file.
        ''' </summary>
        ''' <param name="fileName">a name and path of the file to create</param>
        ''' <param name="declarationForm">an implementation of a concrete 
        ''' declaration version to use</param>
        ''' <param name="warnings">out param - returns non critical errors 
        ''' encountered when exporting data</param>
        ''' <remarks></remarks>
        Public Sub SaveToFfData(ByVal fileName As String, _
            ByVal declarationForm As IVatDeclaration, ByRef warnings As String)

            warnings = ""

            If _Items.Count < 1 Then
                Throw New Exception(ActiveReports_VatDeclaration_ListEmpty)
            End If

            If StringIsNullOrEmpty(fileName) Then
                Throw New Exception(ActiveReports_VatDeclaration_FileNull)
            End If

            If declarationForm Is Nothing Then
                Throw New Exception(ActiveReports_VatDeclaration_DeclarationFormNull)
            End If

            If _DateFrom.Date < declarationForm.ValidFrom.Date OrElse _
                _DateTo.Date > declarationForm.ValidTo.Date Then
                warnings = String.Format(ActiveReports_VatDeclaration_PeriodInvalid, _
                    declarationForm.ValidFrom.ToString("yyyy-MM-dd"), _
                    declarationForm.ValidTo.ToString("yyyy-MM-dd"), _
                    _DateFrom.ToString("yyyy-MM-dd"), _DateTo.ToString("yyyy-MM-dd"))
            End If

            ' Set culture params that were used when parsing declaration's
            ' numbers and dates to string
            Dim oldCulture As Globalization.CultureInfo = _
                DirectCast(Threading.Thread.CurrentThread.CurrentCulture.Clone, Globalization.CultureInfo)

            Threading.Thread.CurrentThread.CurrentCulture = _
                New Globalization.CultureInfo("lt-LT", False)

            Dim ffDataFormatDataSet As DataSet = Nothing

            Try

                ffDataFormatDataSet = declarationForm.GetFfDataDataSet(Me)

                If ffDataFormatDataSet Is Nothing Then
                    Throw New Exception(ActiveReports_VatDeclaration_UnknownErrorWhileExporting)
                End If

            Catch ex As Exception

                If Not ffDataFormatDataSet Is Nothing Then
                    ffDataFormatDataSet.Dispose()
                End If

                Threading.Thread.CurrentThread.CurrentCulture = oldCulture

                Throw

            End Try

            Try

                Using ffDataFileStream As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.Create)
                    ffDataFormatDataSet.WriteXml(ffDataFileStream)
                    ffDataFileStream.Close()
                End Using

            Catch ex As Exception

                If Not ffDataFormatDataSet Is Nothing Then
                    ffDataFormatDataSet.Dispose()
                End If

                Threading.Thread.CurrentThread.CurrentCulture = oldCulture

                Throw

            End Try

            ffDataFormatDataSet.Dispose()

            Threading.Thread.CurrentThread.CurrentCulture = oldCulture

        End Sub


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(ActiveReports_VatDeclaration_ToString, _
                Year.ToString(), _Month.ToString())
        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Documents.VatDeclaration1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Fetches a new instance of VatDeclaration for a month.
        ''' </summary>
        ''' <param name="declarationDate">a date of the declaration</param>
        ''' <param name="year">a year that the VAT declaration data is fetched for</param>
        ''' <param name="month">a month that the VAT declaration data is fetched for</param>
        ''' <remarks></remarks>
        Public Shared Function GetVatDeclaration(ByVal declarationDate As Date, _
            ByVal year As Integer, ByVal month As Integer) As VatDeclaration
            Return DataPortal.Fetch(Of VatDeclaration)(New Criteria( _
                declarationDate, year, month))
        End Function

        ''' <summary>
        ''' Fetches a new instance of VatDeclaration for a custom period.
        ''' </summary>
        ''' <param name="declarationDate">a date of the declaration</param>
        ''' <param name="periodStart">a starting date of the VAT declaration custom period</param>
        ''' <param name="periodEnd">an ending date of the VAT declaration custom period</param>
        ''' <remarks></remarks>
        Public Shared Function GetVatDeclaration(ByVal declarationDate As Date, _
            ByVal periodStart As Date, ByVal periodEnd As Date) As VatDeclaration
            Return DataPortal.Fetch(Of VatDeclaration)(New Criteria( _
                declarationDate, periodStart, periodEnd))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _Date As Date = Today
            Private _Year As Integer = Today.AddMonths(-1).Year
            Private _Month As Integer = Today.AddMonths(-1).Month
            Private _CustomPeriod As Boolean = False
            Private _DateFrom As Date = New Date(Today.AddMonths(-1).Year, _
                Today.AddMonths(-1).Month, 1)
            Private _DateTo As Date = New Date(Today.AddMonths(-1).Year, _
                Today.AddMonths(-1).Month, Date.DaysInMonth(Today.AddMonths(-1).Year, _
                Today.AddMonths(-1).Month))
            Public ReadOnly Property [Date]() As Date
                Get
                    Return _Date
                End Get
            End Property
            Public ReadOnly Property Year() As Integer
                Get
                    Return _Year
                End Get
            End Property
            Public ReadOnly Property Month() As Integer
                Get
                    Return _Month
                End Get
            End Property
            Public ReadOnly Property CustomPeriod() As Boolean
                Get
                    Return _CustomPeriod
                End Get
            End Property
            Public ReadOnly Property DateFrom() As Date
                Get
                    Return _DateFrom
                End Get
            End Property
            Public ReadOnly Property DateTo() As Date
                Get
                    Return _DateTo
                End Get
            End Property
            Public Sub New(ByVal nDate As Date, ByVal nYear As Integer, _
                ByVal nMonth As Integer)
                _Date = nDate
                _Year = nYear
                _Month = nMonth
            End Sub
            Public Sub New(ByVal nDate As Date, ByVal nDateFrom As Date, _
                ByVal nDateTo As Date)
                _Date = nDate
                _Year = nDateFrom.Year
                _Month = 0
                _CustomPeriod = True
                _DateFrom = nDateFrom
                _DateTo = nDateTo
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
               My.Resources.Common_SecuritySelectDenied)

            If criteria.Year < 1970 OrElse criteria.Year > 2100 Then
                Throw New Exception(ActiveReports_VatDeclaration_YearInvalid)
            End If

            If Not criteria.CustomPeriod Then
                If criteria.Month < 1 OrElse criteria.Month > 12 Then
                    Throw New Exception(ActiveReports_VatDeclaration_MonthInvalid)
                ElseIf criteria.Date.Date < New Date(criteria.Year, criteria.Month, 1) Then
                    Throw New Exception(ActiveReports_VatDeclaration_DateInvalid)
                End If
            End If

            _Year = criteria.Year
            _Date = criteria.Date
            If criteria.CustomPeriod Then
                _DateFrom = criteria.DateFrom
                _DateTo = criteria.DateTo
                _Month = 0
            Else
                _DateFrom = New Date(criteria.Year, criteria.Month, 1)
                _DateTo = New Date(criteria.Year, criteria.Month, _
                    Date.DaysInMonth(criteria.Year, criteria.Month))
                _Month = criteria.Month
            End If

            Dim myComm As New SQLCommand("FetchVatDeclaration")
            myComm.AddParam("?DF", _DateFrom.Date)
            myComm.AddParam("?DT", _DateTo.Date)
            myComm.AddParam("?IV", ActiveReports_VatDeclaration_IndirectVatName)

            Using myData As DataTable = myComm.Fetch
                _Items = VatDeclarationItemList.GetVatDeclarationItemList(myData)
            End Using

            _Subtotals = VatDeclarationSubtotalList.GetVatDeclarationSubtotalList(_Items)

        End Sub

#End Region

    End Class

End Namespace
