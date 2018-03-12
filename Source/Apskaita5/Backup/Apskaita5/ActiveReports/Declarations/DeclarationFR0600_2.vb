Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of a <see cref="Declaration">Declaration</see>
    ''' for a state tax inspectorate (VMI) report No. FR0572 version 2.
    ''' </summary>
    ''' <remarks>Object is responsible for fetching the report data to a dataset
    ''' and transforming the dataset to ffdata format (required by the FormFiller application).</remarks>
    <Serializable()> _
    Public Class DeclarationFR0600_2
        Implements IVatDeclaration

        Private Const DECLARATION_NAME As String = "FR0600 v.2"
        Private Const FILENAMEMXFDFR0600_2 As String = "MXFD\FR0600(2).mxfd"
        Private Const FILENAMEFFDATAFR0600_2 As String = "FFData\FR0600(2).ffdata"


        ''' <summary>
        ''' Gets a name of the declaration.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String Implements IVatDeclaration.Name
            Get
                Return DECLARATION_NAME
            End Get
        End Property

        ''' <summary>
        ''' Gets a start of the period that the declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidFrom() As Date Implements IVatDeclaration.ValidFrom
            Get
                Return Date.MinValue
            End Get
        End Property

        ''' <summary>
        ''' Gets an end of the period that the declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidTo() As Date Implements IVatDeclaration.ValidTo
            Get
                Return Date.MaxValue
            End Get
        End Property


        ''' <summary>
        ''' Gets a ffdata format dataset.
        ''' </summary>
        ''' <param name="declaration">a declaration data to be rendered into state tax inspectorate format</param>
        ''' <remarks></remarks>
        Public Function GetFfDataDataSet(ByVal declaration As VatDeclaration) As DataSet _
            Implements IVatDeclaration.GetFfDataDataSet

            If declaration Is Nothing Then
                Throw New ArgumentNullException("declaration")
            End If

            ' read ffdata xml structure to dataset
            Dim formDataSet As DataSet
            Using formFileStream As IO.FileStream = New IO.FileStream( _
                IO.Path.Combine(AppPath(), FILENAMEFFDATAFR0600_2), IO.FileMode.Open)
                formDataSet = New DataSet
                formDataSet.ReadXml(formFileStream)
                formFileStream.Close()
            End Using

            formDataSet.Tables(0).Rows(0).Item(3) = GetCurrentIdentity.Name
            formDataSet.Tables(0).Rows(0).Item(4) = GetDateInFFDataFormat(Today)
            formDataSet.Tables(1).Rows(0).Item(2) = IO.Path.Combine(AppPath(), FILENAMEMXFDFR0600_2)

            For i As Integer = 1 To formDataSet.Tables(8).Rows.Count
                If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString.Trim.ToUpper = "B_MM_Pavad".ToUpper Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(GetCurrentCompany.Name, 30).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString.Trim.ToUpper = "B_MM_ID".ToUpper Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetCurrentCompany.Code
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString.Trim.ToUpper = "B_MM_PVM".ToUpper Then
                    If GetCurrentCompany.CodeVat.Trim.Length > 2 Then
                        formDataSet.Tables(8).Rows(i - 1).Item(1) = GetCurrentCompany.CodeVat.Trim.Substring(2)
                    Else
                        formDataSet.Tables(8).Rows(i - 1).Item(1) = ""
                    End If
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString.Trim.ToUpper = "B_MM_Adresas".ToUpper Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(GetCurrentCompany.Address, 30).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString.Trim.ToUpper = "B_MM_Epastas".ToUpper Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(GetCurrentCompany.Email, 30).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString.Trim.ToUpper = "B_UzpildData".ToUpper Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat(declaration.Date.Date)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString.Trim.ToUpper = "B_ML_DataNuo".ToUpper Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat(declaration.DateFrom)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString.Trim.ToUpper = "B_ML_DataIki".ToUpper Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat(declaration.DateTo)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString.Trim.ToUpper = "E10".ToUpper Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetCurrentCompany.MainEconomicActivityCode
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString.Trim.ToUpper = "E28".ToUpper Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = CInt(GetCurrentCompany.VatDeductionPercentage)
                Else

                    For Each subtotal As VatDeclarationSubtotal In declaration.Subtotals
                        If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString.Trim.ToUpper = subtotal.Code.Trim.ToUpper Then
                            formDataSet.Tables(8).Rows(i - 1).Item(1) = CInt(subtotal.Sum)
                            Exit For
                        End If
                    Next

                End If
            Next

            Return formDataSet

        End Function


        Public Overrides Function ToString() As String
            Return DECLARATION_NAME
        End Function

    End Class

End Namespace