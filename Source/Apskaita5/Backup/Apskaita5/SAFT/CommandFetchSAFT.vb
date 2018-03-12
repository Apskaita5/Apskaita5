Namespace SAFT

    ''' <summary>
    ''' Represents a command that returns a SAF-T accounting data.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class CommandFetchSAFT
        Inherits CommandBase

#Region " Authorization Rules "

        Public Shared Function CanExecuteCommand() As Boolean
            Return ApplicationContext.User.IsInRole("SAFT.CommandFetchSAFT1")
        End Function

#End Region

#Region " Client-side Code "

        Private _Result As String = String.Empty
        Private _Version As SAFTversion = SAFTversion.v1_1
        Private _DateFrom As Date = Today
        Private _DateTo As Date = Today

        ''' <summary>
        ''' SAF-T (XML) accounting data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Result() As String
            Get
                Return _Result
            End Get
        End Property

        ''' <summary>
        ''' Gets a version of the XSD schema to use.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Version() As SAFTversion
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Version
            End Get
        End Property

        ''' <summary>
        ''' Gets a starting date of the period that the accounting data is fetched for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateFrom
            End Get
        End Property

        ''' <summary>
        ''' Gets an ending date of the period that the accounting data is fetched for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateTo
            End Get
        End Property


        Private Sub BeforeServer()
            ' implement code to run on client
            ' before server is called
        End Sub

        Private Sub AfterServer()
            ' implement code to run on client
            ' after server is called
        End Sub

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets SAF-T accounting (XML) data for the period requested.
        ''' </summary>
        ''' <param name="version">a version of the XSD schema to use</param>
        ''' <param name="dateFrom">a starting date of the period that the accounting data is fetched for</param>
        ''' <param name="dateTo">an ending date of the period that the accounting data is fetched for</param>
        ''' <remarks></remarks>
        Public Shared Function TheCommand(ByVal version As SAFTversion, ByVal dateFrom As Date, _
            ByVal dateTo As Date) As String

            Dim cmd As New CommandFetchSAFT
            cmd._Version = version
            cmd._DateFrom = dateFrom
            cmd._DateTo = dateTo

            cmd.BeforeServer()
            cmd = DataPortal.Execute(Of CommandFetchSAFT)(cmd)
            cmd.AfterServer()

            Return cmd.Result

        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Server-side Code "

        Protected Overrides Sub DataPortal_Execute()

            If Not CanExecuteCommand() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Select Case _Version
                Case SAFTversion.v1_1
                    _Result = FetchSAFTversion2_0()
                Case Else
                    Throw New NotImplementedException(String.Format(My.Resources.Common_DocumentTypeNotImplemented, _
                    _Version.ToString, "CommandFetchSAFT", "DataPortal_Execute."))
            End Select

        End Sub

        Private Function FetchSAFTversion2_0() As String

            Dim result As New SAFTv1.AuditFile()
            result.Header = New SAFTv1.AuditFileHeader
            result.Header.AuditFileCountry = StateCodeLith
            result.Header.AuditFileDateCreated = Today
            result.Header.AuditFileVersion = "1.00"
            result.Header.DefaultCurrencyCode = GetCurrentCompany.BaseCurrency
            result.Header.SoftwareCompanyName = "Marius Dagys"
            result.Header.SoftwareID = "Apskaita5"
            result.Header.SoftwareVersion = "20180115"
            result.Header.TaxAccountingBasis = SAFTv1.AuditFileHeaderTaxAccountingBasis.K
            result.Header.SelectionCriteria = New SAFTv1.SelectionCriteriaStructure
            result.Header.SelectionCriteria.PeriodStart = _DateFrom.ToString("yyyy-MM-dd")
            result.Header.SelectionCriteria.PeriodEnd = _DateTo.ToString("yyyy-MM-dd")
            result.Header.SelectionCriteria.PeriodStartYear = _DateFrom.Year.ToString
            result.Header.SelectionCriteria.PeriodEndYear = _DateTo.Year.ToString
            result.Header.SelectionCriteria.SelectionStartDate = _DateFrom
            result.Header.SelectionCriteria.SelectionEndDate = _DateTo
            result.Header.Company = New SAFTv1.CompanyHeaderStructure
            result.Header.Company.Address = New SAFTv1.AddressStructure() {New SAFTv1.AddressStructure}
            result.Header.Company.Address(0).AddressType = SAFTv1.AddressStructureAddressType.Buveinėsadresas
            result.Header.Company.Address(0).FullAddress = GetCurrentCompany.Address
            result.Header.Company.BankAccount = New SAFTv1.BankAccountStructure() {New SAFTv1.BankAccountStructure}
            result.Header.Company.BankAccount(0).ItemElementName = SAFTv1.ItemChoiceType.IBANNumber
            result.Header.Company.BankAccount(0).Item = GetCurrentCompany.BankAccount
            result.Header.Company.Name = GetCurrentCompany.Name
            result.Header.Company.RegistrationNumber = GetCurrentCompany.Code
            If Not String.IsNullOrEmpty(GetCurrentCompany.CodeVat.Trim) Then
                result.Header.Company.TaxRegistration = New SAFTv1.TaxIDStructure() _
                    {New SAFTv1.TaxIDStructure, New SAFTv1.TaxIDStructure}
                result.Header.Company.TaxRegistration(0).TaxRegistrationNumber = GetCurrentCompany.Code
                result.Header.Company.TaxRegistration(0).TaxType = SAFTv1.TaxIDStructureTaxType.MMR
                result.Header.Company.TaxRegistration(1).TaxRegistrationNumber = GetCurrentCompany.CodeVat
                result.Header.Company.TaxRegistration(1).TaxType = SAFTv1.TaxIDStructureTaxType.PVM
            Else
                result.Header.Company.TaxRegistration = New SAFTv1.TaxIDStructure() _
                    {New SAFTv1.TaxIDStructure}
                result.Header.Company.TaxRegistration(0).TaxRegistrationNumber = GetCurrentCompany.Code
                result.Header.Company.TaxRegistration(0).TaxType = SAFTv1.TaxIDStructureTaxType.MMR
            End If




        End Function

#End Region

    End Class

End Namespace
