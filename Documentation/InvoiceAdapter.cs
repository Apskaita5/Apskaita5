using ApskaitaObjects.HelperLists;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using InvoiceInfo;

namespace ApskaitaObjects.Extensibility
{
    [Serializable]
    public class InvoiceAdapter : IInvoiceAdapter
    {
        /// <inheritdoc cref="IInvoiceAdapter.ForInvoicesMade"/>
        public bool ForInvoicesMade => false;

        /// <inheritdoc cref="IInvoiceAdapter.IdPrefix"/>
        public string IdPrefix => "ivsk";

        /// <inheritdoc cref="IInvoiceAdapter.FileExtension"/>
        public string FileExtension => "xml";

        /// <inheritdoc cref="IInvoiceAdapter.RequiresDefaultLineContent"/>
        public bool RequiresDefaultLineContent => true;

        /// <inheritdoc cref="IInvoiceAdapter.RequiresDefaultAccount"/>
        public bool RequiresDefaultAccount => false;


        /// <inheritdoc cref="IInvoiceAdapter.LoadInvoices"/>
        public List<InvoiceInfo.InvoiceInfo> LoadInvoices(Stream dataStream, 
            string companyCode, string invoiceIdPrefix, long defaultAccount, 
            long defaultVatAccount, VatDeclarationSchemaInfo defaultVatSchema, 
            string defaultContent, string defaultLineContent, string defaultMeasureUnit,
            bool allowNullVatSchema, IList<AccountInfo> accountLookup, 
            IList<VatDeclarationSchemaInfo> vatSchemaLookup, IList<ServiceInfo> serviceLookup)
        {
            if (null == dataStream) throw new ArgumentNullException(nameof(dataStream));
            if (null == accountLookup) throw new ArgumentNullException(nameof(accountLookup));
            if (null == vatSchemaLookup) throw new ArgumentNullException(nameof(vatSchemaLookup));
            if (null == serviceLookup) throw new ArgumentNullException(nameof(serviceLookup));
            if (!allowNullVatSchema && null == defaultVatSchema) throw new ArgumentNullException(nameof(defaultVatSchema));
            if (IsNullOrWhitespace(companyCode)) throw new ArgumentNullException(nameof(companyCode));
            if (defaultVatAccount < 1) throw new ArgumentNullException(nameof(defaultVatAccount));
            if (IsNullOrWhitespace(defaultContent)) throw new ArgumentNullException(nameof(defaultContent));
            if (IsNullOrWhitespace(defaultLineContent)) throw new ArgumentNullException(nameof(defaultLineContent));
            if (IsNullOrWhitespace(defaultMeasureUnit)) throw new ArgumentNullException(nameof(defaultMeasureUnit));

            documents originalInvoices;

            var serializer = new XmlSerializer(typeof(documents));
            try
            {
                originalInvoices = (documents)serializer.Deserialize(dataStream);
            }
            catch (Exception e)
            {
                throw new FormatException("Data is not in valid format for ivesk.lt: "
                    + e.Message, e);
            }

            var result = new List<InvoiceInfo.InvoiceInfo>();

            if (null == originalInvoices.document || originalInvoices.document.Length < 1)
                return result;

            companyCode = companyCode.Trim();
            invoiceIdPrefix = invoiceIdPrefix?.Trim() ?? string.Empty;

            foreach (var originalInvoice in originalInvoices.document)
            {
                if (IsNullOrWhitespace(originalInvoice.id)) throw new Exception(
                    $"Id is not specified for invoice {originalInvoice.date:yyyy-MM-dd} " +
                    $"No {originalInvoice.docnum}, supplier {originalInvoice.sellername} " +
                    $"({originalInvoice.sellercode}).");

                if (originalInvoice.buyercode?.Trim() == companyCode)
                {
                    result.Add(new InvoiceInfo.InvoiceInfo
                    {
                        Content = defaultContent,
                        CurrencyCode = originalInvoice.currency ?? string.Empty,
                        Date = originalInvoice.date,
                        FullNumber = originalInvoice.docnum ?? string.Empty,
                        ID = invoiceIdPrefix + originalInvoice.id,
                        LanguageCode = "LT",
                        Payer = MapSupplier(originalInvoice),
                        CommentsInternal = MapInternalComments(originalInvoice),
                        InvoiceItems = MapInvoiceItems(originalInvoice, defaultAccount,
                            defaultVatSchema, defaultLineContent, defaultMeasureUnit,
                            allowNullVatSchema, accountLookup, vatSchemaLookup, serviceLookup),
                        Sum = decimal.ToDouble(originalInvoice.subtotal),
                        SumVat = decimal.ToDouble(originalInvoice.vat),
                        SumTotal = decimal.ToDouble(originalInvoice.total)
                    });
                }
            }

            return result;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "ivesk.lt gautų sąskaitų adapteris";
        }

        #region Mapping Methods

        private ClientInfo MapSupplier(documentsDocument originalInvoice)
        {
            return new ClientInfo
            {
                CurrencyCode = originalInvoice.currency ?? string.Empty,
                Address = originalInvoice.selleraddress ?? string.Empty,
                Code = originalInvoice.sellercode ?? throw new Exception(
                    $"Supplier code is not specified for invoice {originalInvoice.date:yyyy-MM-dd} No {originalInvoice.docnum}, supplier name - {originalInvoice.sellername}."),
                CodeVAT = originalInvoice.sellervat ?? string.Empty,
                IsCodeLocal = false, // ??
                IsNaturalPerson = originalInvoice.sellerisperson,
                IsSupplier = true,
                Name = originalInvoice.sellername ?? throw new Exception(
                    $"Supplier name is not specified for invoice {originalInvoice.date:yyyy-MM-dd} No {originalInvoice.docnum}, supplier code - {originalInvoice.sellercode}."),
                BankAccount = originalInvoice.selleriban ?? string.Empty,
                CountryCode = originalInvoice.sellercountry ?? string.Empty,
                LanguageCode = "LT"
            };
        }

        private string MapInternalComments(documentsDocument originalInvoice)
        {
            var internalComments = new List<string>();
            if (originalInvoice.duedateSpecified)
                internalComments.Add($"Apmokėjimo terminas - {originalInvoice.duedate:yyyy-MM-dd}.");
            if (!IsNullOrWhitespace(originalInvoice.ordernum))
                internalComments.Add($"Užsakymo Nr. {originalInvoice.ordernum}.");
            if (!originalInvoice.report2isaf)
                internalComments.Add("Neteikiama ISAF.");
            if (!originalInvoice.separatevat)
                internalComments.Add("PVM įtrauktas į bendrą sumą.");
            if (!IsNullOrWhitespace(originalInvoice.url))
                internalComments.Add($"Sąskaitos failas intenete: {originalInvoice.url}.");

            return string.Join(" ", internalComments.ToArray());
        }

        private List<InvoiceItemInfo> MapInvoiceItems(documentsDocument originalInvoice,
            long defaultVatAccount, VatDeclarationSchemaInfo defaultVatSchema,
            string defaultLineContent, string defaultMeasureUnit, bool allowNullVatSchema, 
            IList<AccountInfo> accountLookup, IList<VatDeclarationSchemaInfo> vatSchemaLookup, 
            IList<ServiceInfo> serviceLookup)
        {
            if (null == originalInvoice.line || originalInvoice.line.Length < 1)
                throw new Exception($"No lines for invoice {originalInvoice.date:yyyy-MM-dd} " +
                    $"No {originalInvoice.docnum}, supplier {originalInvoice.sellername} " +
                    $"({originalInvoice.sellercode}).");

            var result = new List<InvoiceItemInfo>();
            foreach (var originalLine in originalInvoice.line)
            {
                if (IsNullOrWhitespace(originalLine.code)) throw new Exception(
                $"Costs (account or service) code is not specified for invoice {originalInvoice.date:yyyy-MM-dd} " +
                      $"No {originalInvoice.docnum}, supplier {originalInvoice.sellername} " +
                      $"({originalInvoice.sellercode}).");

                var service = MapServiceInfo(originalLine.code, serviceLookup);
                long account;
                string vatSchemaId;
                if (null == service)
                {
                    if (!long.TryParse(originalLine.code, NumberStyles.Any,
                        CultureInfo.InvariantCulture, out account) || !IsValidAccount(account, accountLookup))
                        throw new Exception($"Invalid account code {originalLine.code} for invoice " +
                            $"{originalInvoice.date:yyyy-MM-dd} No {originalInvoice.docnum}, " +
                            $"supplier {originalInvoice.sellername} ({originalInvoice.sellercode}).");
                    vatSchemaId = MapVatSchema(originalLine.vatclass, originalInvoice,
                        defaultVatSchema, vatSchemaLookup);
                    if (!allowNullVatSchema && IsNullOrWhitespace(vatSchemaId))
                        throw new Exception($"Invalid VAT schema id {originalLine.vatclass} for invoice {originalInvoice.date:yyyy-MM-dd} " +
                            $"No {originalInvoice.docnum}, supplier {originalInvoice.sellername} " +
                            $"({originalInvoice.sellercode}).");
                }
                else
                {
                    account = service.AccountPurchase;
                    if (account < 1) throw new Exception(
                        $"Account for purchases is not specified for service" +
                            $"{service.NameShort} referenced by the invoice {originalInvoice.date:yyyy-MM-dd} " +
                            $"No {originalInvoice.docnum}, supplier {originalInvoice.sellername} " +
                            $"({originalInvoice.sellercode}).");
                    vatSchemaId = service.DeclarationSchemaPurchase?.ExternalCode ?? string.Empty;
                    if (!allowNullVatSchema && IsNullOrWhitespace(vatSchemaId))
                        throw new Exception($"VAT schema for purchases is not specified for service" +
                            $"{service.NameShort} referenced by the invoice {originalInvoice.date:yyyy-MM-dd} " +
                            $"No {originalInvoice.docnum}, supplier {originalInvoice.sellername} " +
                            $"({originalInvoice.sellercode}).");
                }

                result.Add(new InvoiceItemInfo
                {
                    AccountIncome = account,
                    AccountVat = service?.AccountVatPurchase ?? defaultVatAccount,
                    Ammount = 1.0,
                    MeasureUnit = service?.MeasureUnit ?? defaultMeasureUnit,
                    NameInvoice = service?.NameInvoice ?? defaultLineContent,
                    Sum = decimal.ToDouble(originalLine.subtotal),
                    SumTotal = decimal.ToDouble(originalLine.total),
                    SumVat = decimal.ToDouble(originalLine.vat),
                    UnitValue = decimal.ToDouble(originalLine.subtotal),
                    VatRate = decimal.ToDouble(originalLine.vatpercent),
                    VatDDeclarationSchemaID = vatSchemaId,
                    ServiceCode = service?.Code ?? string.Empty
                });
            }

            return result;
        }

        private string MapVatSchema(string originalVatCode, documentsDocument originalInvoice,
            VatDeclarationSchemaInfo defaultVatSchema, IList<VatDeclarationSchemaInfo> vatSchemaLookup)
        {
            if (IsNullOrWhitespace(originalVatCode)) return defaultVatSchema?.ExternalCode ?? string.Empty;

            foreach (var vatSchema in vatSchemaLookup)
            {
                if (!IsNullOrWhitespace(vatSchema.ExternalCode) && vatSchema.TaxCode.Equals(
                    originalVatCode.Trim(), StringComparison.OrdinalIgnoreCase))
                    return vatSchema.ExternalCode;
            }

            return string.Empty;
        }

        private ServiceInfo MapServiceInfo(string serviceCode, IList<ServiceInfo> serviceLookup)
        {
            foreach (var service in serviceLookup)
            {
                if (serviceCode.Equals(service.Code, StringComparison.OrdinalIgnoreCase))
                    return service;
            }

            return null;
        }

        #endregion

        #region Helper Methods

        private static bool IsNullOrWhitespace(string value)
        {
            return null == value || string.IsNullOrEmpty(value.Trim());
        }
                   
        private static bool IsValidAccount(long account, IList<AccountInfo> accountLookup)
        {
            foreach (var accountInfo in accountLookup)
            {
                if (accountInfo.ID == account) return true;
            }

            return false;
        }

        #endregion

        #region Autogenerated Code For XML schema

       //------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class documents
    {

        private documentsDocument[] documentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("document", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public documentsDocument[] document
        {
            get
            {
                return this.documentField;
            }
            set
            {
                this.documentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class documentsDocument
    {

        private string idField;

        private System.DateTime dateField;

        private System.DateTime operationdateField;

        private bool operationdateFieldSpecified;

        private System.DateTime duedateField;

        private bool duedateFieldSpecified;

        private string paymenttermField;

        private string docnumField;

        private string ordernumField;

        private decimal subtotalField;

        private decimal vatField;

        private decimal totalField;

        private string currencyField;

        private string urlField;

        private string filenameField;

        private bool report2isafField;

        private bool separatevatField;

        private string selleridField;

        private string sellercodeField;

        private string sellervatField;

        private string sellernameField;

        private string selleraddressField;

        private bool sellerispersonField;

        private string sellercountryField;

        private string selleribanField;

        private string buyeridField;

        private string buyercodeField;

        private string buyervatField;

        private string buyernameField;

        private string buyeraddressField;

        private bool buyerispersonField;

        private string buyercountryField;

        private bool hasreceiptField;

        private documentsDocumentLine[] lineField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime operationdate
        {
            get
            {
                return this.operationdateField;
            }
            set
            {
                this.operationdateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool operationdateSpecified
        {
            get
            {
                return this.operationdateFieldSpecified;
            }
            set
            {
                this.operationdateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime duedate
        {
            get
            {
                return this.duedateField;
            }
            set
            {
                this.duedateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool duedateSpecified
        {
            get
            {
                return this.duedateFieldSpecified;
            }
            set
            {
                this.duedateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string paymentterm
        {
            get
            {
                return this.paymenttermField;
            }
            set
            {
                this.paymenttermField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string docnum
        {
            get
            {
                return this.docnumField;
            }
            set
            {
                this.docnumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ordernum
        {
            get
            {
                return this.ordernumField;
            }
            set
            {
                this.ordernumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal subtotal
        {
            get
            {
                return this.subtotalField;
            }
            set
            {
                this.subtotalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal vat
        {
            get
            {
                return this.vatField;
            }
            set
            {
                this.vatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string currency
        {
            get
            {
                return this.currencyField;
            }
            set
            {
                this.currencyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string filename
        {
            get
            {
                return this.filenameField;
            }
            set
            {
                this.filenameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool report2isaf
        {
            get
            {
                return this.report2isafField;
            }
            set
            {
                this.report2isafField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool separatevat
        {
            get
            {
                return this.separatevatField;
            }
            set
            {
                this.separatevatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string sellerid
        {
            get
            {
                return this.selleridField;
            }
            set
            {
                this.selleridField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string sellercode
        {
            get
            {
                return this.sellercodeField;
            }
            set
            {
                this.sellercodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string sellervat
        {
            get
            {
                return this.sellervatField;
            }
            set
            {
                this.sellervatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string sellername
        {
            get
            {
                return this.sellernameField;
            }
            set
            {
                this.sellernameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string selleraddress
        {
            get
            {
                return this.selleraddressField;
            }
            set
            {
                this.selleraddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool sellerisperson
        {
            get
            {
                return this.sellerispersonField;
            }
            set
            {
                this.sellerispersonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string sellercountry
        {
            get
            {
                return this.sellercountryField;
            }
            set
            {
                this.sellercountryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string selleriban
        {
            get
            {
                return this.selleribanField;
            }
            set
            {
                this.selleribanField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string buyerid
        {
            get
            {
                return this.buyeridField;
            }
            set
            {
                this.buyeridField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string buyercode
        {
            get
            {
                return this.buyercodeField;
            }
            set
            {
                this.buyercodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string buyervat
        {
            get
            {
                return this.buyervatField;
            }
            set
            {
                this.buyervatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string buyername
        {
            get
            {
                return this.buyernameField;
            }
            set
            {
                this.buyernameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string buyeraddress
        {
            get
            {
                return this.buyeraddressField;
            }
            set
            {
                this.buyeraddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool buyerisperson
        {
            get
            {
                return this.buyerispersonField;
            }
            set
            {
                this.buyerispersonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string buyercountry
        {
            get
            {
                return this.buyercountryField;
            }
            set
            {
                this.buyercountryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool hasreceipt
        {
            get
            {
                return this.hasreceiptField;
            }
            set
            {
                this.hasreceiptField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("line", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public documentsDocumentLine[] line
        {
            get
            {
                return this.lineField;
            }
            set
            {
                this.lineField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class documentsDocumentLine
    {

        private decimal subtotalField;

        private decimal vatField;

        private decimal vatpercentField;

        private decimal totalField;

        private string codeField;

        private string nameField;

        private string vatclassField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal subtotal
        {
            get
            {
                return this.subtotalField;
            }
            set
            {
                this.subtotalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal vat
        {
            get
            {
                return this.vatField;
            }
            set
            {
                this.vatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal vatpercent
        {
            get
            {
                return this.vatpercentField;
            }
            set
            {
                this.vatpercentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string vatclass
        {
            get
            {
                return this.vatclassField;
            }
            set
            {
                this.vatclassField = value;
            }
        }
    }

    #endregion
}
}
