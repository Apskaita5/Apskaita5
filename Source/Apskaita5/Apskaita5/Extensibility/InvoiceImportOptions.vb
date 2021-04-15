Imports ApskaitaObjects.Documents
Imports ApskaitaObjects.General
Imports ApskaitaObjects.Settings

Namespace Extensibility

    ''' <summary>
    ''' A collection of invoice import options for <see cref="IInvoiceAdapter"/>.
    ''' </summary>
    <Serializable()>
Public Class InvoiceImportOptions
        Inherits BusinessBase(Of InvoiceImportOptions)
        Implements IValidationMessageProvider
        
        #Region " Business Methods "

        Private _forInvoicesMade As Boolean = false
        Private _adapter As IInvoiceAdapter = Nothing
        Private _invoiceIdPrefix As String = String.Empty
        Private _defaultAccount As Long = 0
        Private _defaultVatAccount As Long = 0
        Private _defaultVatSchema As VatDeclarationSchemaInfo = Nothing
        Private _defaultContent As String = String.Empty
        Private _defaultLineContent As String = String.Empty
        Private _defaultMeasureUnit As String = String.Empty


        Public Sub New()

            Dim company As CompanyInfo = GetCurrentCompany()
            _defaultVatAccount = company.Accounts.GetAccount(DefaultAccountType.VatReceivable)
            _defaultVatSchema = company.DeclarationSchemaPurchase
            _defaultContent = company.DefaultInvoiceReceivedContent
            _defaultMeasureUnit = company.MeasureUnitInvoiceReceived

            ValidationRules.CheckRules()

        End Sub


        ''' <summary>
        ''' a prefix to use for original invoice id's in order to differentiate
        ''' multiple integration endpoints
        ''' </summary>
        <StringField(ValueRequiredLevel.Mandatory, 10, True)> _
        Public Property InvoiceIdPrefix As String
            Get
                Return _invoiceIdPrefix
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set
                If String.IsNullOrEmpty(value) Then value = String.Empty
                _invoiceIdPrefix = value.Trim()
                PropertyHasChanged()
            End Set
        End Property

        ''' <summary>
        ''' whether to import data for invoices made (otherwise - for invoices received)
        ''' </summary>
        Public Property ForInvoicesMade As Boolean
            Get
                Return _forInvoicesMade
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set 
                If (_forInvoicesMade <> value) Then
                    _forInvoicesMade = value
                    PropertyHasChanged()
                   
                    Adapter = Nothing
                    DefaultAccount = 0
                    DefaultLineContent = String.Empty

                    Dim company As CompanyInfo = GetCurrentCompany()

                    If _forInvoicesMade Then
                        DefaultVatAccount = company.Accounts.GetAccount(DefaultAccountType.VatPayable)
                        DefaultVatSchema = company.DeclarationSchemaSales
                        DefaultContent = company.DefaultInvoiceMadeContent
                        DefaultMeasureUnit = company.MeasureUnitInvoiceMade
                    Else 
                        DefaultVatAccount = company.Accounts.GetAccount(DefaultAccountType.VatReceivable)
                        DefaultVatSchema = company.DeclarationSchemaPurchase
                        DefaultContent = company.DefaultInvoiceReceivedContent
                        DefaultMeasureUnit = company.MeasureUnitInvoiceReceived
                    End If    

                End If   
            End Set
        End Property

        ''' <summary>
        ''' an adapter to use for reading imported data stream (e.g. file)
        ''' </summary>
        Public Property Adapter As IInvoiceAdapter
            Get
                Return _adapter
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set 
                If (value Is Nothing AndAlso _adapter Is Nothing) Then Exit Property
                If (value Is Nothing OrElse _adapter Is Nothing OrElse _
                    Not value.GetType() Is _adapter.GetType())
                    
                    _adapter = value
                    PropertyHasChanged()

                    If Not _adapter Is Nothing AndAlso Not StringIsNullOrEmpty(_adapter.IdPrefix) Then _
                        InvoiceIdPrefix = _adapter.IdPrefix

                End If
            End Set
        End Property

        ''' <summary>
        ''' a default income/costs account to use (if it's not specified in the original invoice data)
        ''' </summary>
        <AccountField(ValueRequiredLevel.Mandatory, False,  5, 6)> _
        Public Property DefaultAccount As Long
            Get
                Return _defaultAccount
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set
                _defaultAccount = value
                PropertyHasChanged()
            End Set
        End Property

        ''' <summary>
        ''' a default VAT account to use (if it's not specified in the original invoice data)
        ''' </summary>
        <AccountField(ValueRequiredLevel.Mandatory, False, 2, 4, 6)> _
        Public Property DefaultVatAccount As Long
            Get
                Return _defaultVatAccount
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set
                _defaultVatAccount = value
                PropertyHasChanged()
            End Set
        End Property

        ''' <summary>
        ''' a default VAT schema to use (if it's not specified in the original invoice data)
        ''' </summary> 
        <VatDeclarationSchemaField(ValueRequiredLevel.Mandatory, Documents.TradedItemType.All)> _
        Public Property DefaultVatSchema As VatDeclarationSchemaInfo
            Get
                Return _defaultVatSchema
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set
                _defaultVatSchema = value
                PropertyHasChanged()
            End Set
        End Property

        ''' <summary>
        ''' a default invoice content (description) to use
        ''' (if it's not specified in the original invoice data)
        ''' </summary>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
        Public Property DefaultContent As String
            Get
                Return _defaultContent
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set
                If String.IsNullOrEmpty(value) Then value = String.Empty
                _defaultContent = value.Trim()
                PropertyHasChanged()
            End Set
        End Property

        ''' <summary>
        ''' a default invoice item (line) content to use
        ''' (if it's not specified in the original invoice data)
        ''' </summary>
        <StringField(ValueRequiredLevel.Optional, 255)> _ 
        Public Property DefaultLineContent As String
            Get
                Return _defaultLineContent
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set
                If String.IsNullOrEmpty(value) Then value = String.Empty
                _defaultLineContent = value.Trim()
                PropertyHasChanged()
            End Set
        End Property

        ''' <summary>
        ''' a default measure unit to use (if it's not specified in the original invoice data)
        ''' </summary>
        <StringField(ValueRequiredLevel.Mandatory, 10)> _
        Public Property DefaultMeasureUnit As String
            Get
                Return _defaultMeasureUnit
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set
                If String.IsNullOrEmpty(value) Then value = String.Empty
                _defaultMeasureUnit = value.Trim()
                PropertyHasChanged()
            End Set
        End Property


        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid 
            End Get
        End Property


        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            If Not MyBase.IsValid Then Return Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error)
            Return String.Empty
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            If MyBase.BrokenRulesCollection.WarningCount > 0 Then _
                Return Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning)
            Return String.Empty
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return (MyBase.BrokenRulesCollection.WarningCount > 0)
        End Function


        Public Function ReadInvoiceData(dataStream As IO.Stream) As List(Of InvoiceInfo.InvoiceInfo)

            If Not CanImport() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            ValidationRules.CheckRules()

            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, _
                    vbCrLf, GetAllBrokenRules()))
            End If

            Dim accountLookup As AccountInfoList
            Dim vatSchemaLookup As VatDeclarationSchemaInfoList
            Dim serviceLookup As ServiceInfoList

            Try
                accountLookup = AccountInfoList.GetList()
                vatSchemaLookup = VatDeclarationSchemaInfoList.GetList()
                serviceLookup = ServiceInfoList.GetList()
            Catch ex As Exception
                Throw New Exception("Failed to fetch lookup lists: " + ex.Message, ex)
            End Try

            Return _adapter.LoadInvoices(dataStream, GetCurrentCompany().Code, _
                _invoiceIdPrefix, _defaultAccount, _defaultVatAccount, _
                _defaultVatSchema, _defaultContent, _defaultLineContent, _
                _defaultMeasureUnit, Not GetCurrentCompany().UseVatDeclarationSchemas, _
                accountLookup, vatSchemaLookup, serviceLookup)

        End Function

        Public Function ImportInvoices(invoices As List(Of InvoiceInfo.InvoiceInfo)) As String

            If _forInvoicesMade Then
                Return Documents.ImportInvoicesMadeCommand.TheCommand(invoices)
            Else 
                Return Documents.ImportInvoicesReceivedCommand.TheCommand(invoices)
            End If

        End Function

        Public Overrides Function Save() As InvoiceImportOptions
             Throw new NotSupportedException()
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return 1
        End Function

#End Region
   
#Region " Authorization Rules "

        Public Shared Function CanUse() As Boolean
            Return Documents.ImportInvoicesMadeCommand.CanExecuteCommand() _
                OrElse Documents.ImportInvoicesReceivedCommand.CanExecuteCommand()
        End Function

        Public Function CanImport() As Boolean
            If _forInvoicesMade Then
                Return Documents.ImportInvoicesMadeCommand.CanExecuteCommand()
            Else 
                Return Documents.ImportInvoicesReceivedCommand.CanExecuteCommand()
            End If
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Validation.RuleArgs("InvoiceIdPrefix"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Validation.RuleArgs("DefaultContent"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Validation.RuleArgs("DefaultMeasureUnit"))

            ValidationRules.AddRule(AddressOf AdapterValidation, _
                New Validation.RuleArgs("Adapter"))
            ValidationRules.AddRule(AddressOf DefaultLineContentValidation, _
                New Validation.RuleArgs("DefaultLineContent"))
            ValidationRules.AddRule(AddressOf VatDeclarationSchemaInfoValidation, _
                New Validation.RuleArgs("DefaultVatSchema"))
            ValidationRules.AddRule(AddressOf DefaultAccountValidation, _
                New Validation.RuleArgs("DefaultAccount"))
            ValidationRules.AddRule(AddressOf DefaultVatAccountValidation, _
                New Validation.RuleArgs("DefaultVatAccount"))

            ValidationRules.AddDependantProperty("Adapter", "DefaultLineContent", False)
            ValidationRules.AddDependantProperty("Adapter", "DefaultAccount", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring that the value of property Adapter is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AdapterValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As InvoiceImportOptions = DirectCast(target, InvoiceImportOptions)

            If valObj._adapter Is Nothing Then
                e.Description = "Nepasirinktas sąskaitų formato adapteris."
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            If valObj._forInvoicesMade <> valObj._adapter.ForInvoicesMade Then
                e.Description = "Pasirinktas sąskaitų formato adapteris yra skirtas ne tam sąskaitų tipui."
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the value of property DefaultLineContent is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function DefaultLineContentValidation(ByVal target As Object, _
                                                  ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As InvoiceImportOptions = DirectCast(target, InvoiceImportOptions)

            If Not valObj._adapter Is Nothing AndAlso valObj._adapter.RequiresDefaultLineContent _
               AndAlso StringIsNullOrEmpty(valObj._defaultLineContent) Then
                e.Description = "Nenurodytas sąskaitos eilutės turinys pagal nutylėjimą (jei jo nėra originaliuose duomenyse)."
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            Return CommonValidation.StringFieldValidation(target, e)

        End Function

        ''' <summary>
        ''' Rule ensuring that the value of property VatDeclarationSchemaInfo is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function VatDeclarationSchemaInfoValidation(ByVal target As Object, _
                                                  ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As InvoiceImportOptions = DirectCast(target, InvoiceImportOptions)
            
            If valObj._defaultVatSchema = VatDeclarationSchemaInfo.Empty() _
                AndAlso GetCurrentCompany().UseVatDeclarationSchemas Then
                e.Description = "Nenurodyta PVM deklaravimo schema pagal nutylėjimą (kai ji nenurodyta pirminiuose duomenyse)."
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            If valObj._defaultVatSchema <> VatDeclarationSchemaInfo.Empty() _
                AndAlso valObj._defaultVatSchema.TradedType <> TradedItemType.All Then
                
                If valObj._forInvoicesMade AndAlso valObj._defaultVatSchema.TradedType _
                    <> TradedItemType.Sales Then
                    e.Description = "Pasirinkta PVM deklaravimo schema skirta įsigijimams, o ne pardavimams."
                    e.Severity = Validation.RuleSeverity.Error
                    Return False
                End If

                If Not valObj._forInvoicesMade AndAlso valObj._defaultVatSchema.TradedType _
                    <> TradedItemType.Purchases Then
                    e.Description = "Pasirinkta PVM deklaravimo schema skirta pardavimams, o ne įsigijimams."
                    e.Severity = Validation.RuleSeverity.Error
                    Return False
                End If

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the value of property DefaultAccount is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function DefaultAccountValidation(ByVal target As Object, _
                                                  ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As InvoiceImportOptions = DirectCast(target, InvoiceImportOptions)

            If Not valObj._adapter Is Nothing AndAlso valObj.Adapter.RequiresDefaultAccount _
               AndAlso Not valObj._defaultAccount > 0 Then
                If valObj._forInvoicesMade Then
                    e.Description = "Nepasirinkta pajamų sąskaita pagal nutylėjimą (jei ji nenurodoma pirminiuose duomenyse)."
                Else 
                    e.Description = "Nepasirinkta sąnaudų sąskaita pagal nutylėjimą (jei ji nenurodoma pirminiuose duomenyse)."
                End If
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            If Not valObj._defaultAccount > 0 Then Return True

            Dim accountClass As Integer = GetCurrentCompany().GetAccountClass(valObj._defaultAccount)

            If valObj._forInvoicesMade AndAlso accountClass <> 5 Then
                e.Description = "Pasirinkta sąskaita pagal nutylėjimą nėra pajamų sąskaita."
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            End If

            If Not valObj._forInvoicesMade AndAlso accountClass <> 6 Then
                e.Description = "Pasirinkta sąskaita pagal nutylėjimą nėra sąnaudų sąskaita."
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the value of property DefaultVatAccount is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function DefaultVatAccountValidation(ByVal target As Object, _
                                                  ByVal e As Validation.RuleArgs) As Boolean
            
            Dim valObj As InvoiceImportOptions = DirectCast(target, InvoiceImportOptions)

            If valObj._forInvoicesMade AndAlso StringIsNullOrEmpty(GetCurrentCompany().CodeVat) _
                Then Return True
            
            If Not valObj._defaultVatAccount > 0 Then
                e.Description = "Nenurodyta PVM sąskaita pagal nutylėjimą (kai ji nenurodyta pirminiuose duomenyse)."
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            Dim accountClass As Integer = GetCurrentCompany().GetAccountClass(valObj._defaultVatAccount)

            If valObj._forInvoicesMade AndAlso accountClass <> 4 Then
                e.Description = "Pasirinkta PVM sąskaita yra ne 4 klasės (mokėtinas PVM yra įsipareigojimas, t.y. 4 klasė)."
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            End If

            If Not valObj._forInvoicesMade AndAlso accountClass <> 2 AndAlso accountClass <> 6 Then
                e.Description = "Pasirinkta PVM sąskaita yra ne 2 arba 6 klasės."
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            End If

            Return True

        End Function
                
#End Region

End Class

End Namespace
