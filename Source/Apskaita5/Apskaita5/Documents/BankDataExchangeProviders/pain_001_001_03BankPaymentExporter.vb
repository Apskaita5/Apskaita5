Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization
Imports ApskaitaObjects.Documents.BankDataExchangeProviders.pain_001_001_03
Imports ApskaitaObjects.Settings

Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Exports payments to ISO20022 Pain.001.001.03 data format
    ''' </summary>
    Public NotInheritable Class Pain_001_001_03BankPaymentExporter
        Implements IBankPaymentExporter

        Public ReadOnly Property Description As String Implements IBankPaymentExporter.Description
            Get
                Return "ISO20022 Pain.001.001.03"
            End Get
        End Property

        Public ReadOnly Property FileFormatDescription As String Implements IBankPaymentExporter.FileFormatDescription
            Get
                Return "ISO20022 File (*.xml)"
            End Get
        End Property

        Public ReadOnly Property FileExtension As String Implements IBankPaymentExporter.FileExtension
            Get
                Return "xml"
            End Get
        End Property


        Public Sub Export(payments As ExportedBankPayments, targetStream As Stream) Implements IBankPaymentExporter.Export
            If payments Is Nothing Then Throw New ArgumentNullException("payments")
            If targetStream Is Nothing Then Throw New ArgumentNullException("targetStream")

            If Not payments.IsValid Then Throw New Exception(payments.GetAllBrokenRules)

            Dim currentCompany As CompanyInfo = GetCurrentCompany()

            Dim result As New Document With {
                .CstmrCdtTrfInitn = New CustomerCreditTransferInitiationV03 With {
                    .GrpHdr = New GroupHeader32 With {
                        .MsgId = payments.UniqueID,
                        .CreDtTm = DateTime.Now,
                        .NbOfTxs = payments.Payments.Count.ToString(),
                        .CtrlSum = Convert.ToDecimal(payments.Subtotal),
                        .CtrlSumSpecified = True,
                        .InitgPty = New PartyIdentification32 With {
                            .Nm = currentCompany.Name
                        }
                    },
                    .PmtInf = New PaymentInstructionInformation3() {New PaymentInstructionInformation3()}
                }
            }

            result.CstmrCdtTrfInitn.PmtInf(0).PmtInfId = payments.UniqueID
            result.CstmrCdtTrfInitn.PmtInf(0).PmtMtd = PaymentMethod3Code.TRF
            result.CstmrCdtTrfInitn.PmtInf(0).NbOfTxs = payments.Payments.Count.ToString()
            result.CstmrCdtTrfInitn.PmtInf(0).CtrlSum = Convert.ToDecimal(payments.Subtotal)
            result.CstmrCdtTrfInitn.PmtInf(0).CtrlSumSpecified = True
            result.CstmrCdtTrfInitn.PmtInf(0).ReqdExctnDt = DateTime.Today.Date
            result.CstmrCdtTrfInitn.PmtInf(0).Dbtr = New PartyIdentification32 With {
                .Nm = currentCompany.Name
            }
            result.CstmrCdtTrfInitn.PmtInf(0).DbtrAcct = New CashAccount16 With {
                .Id = New AccountIdentification4Choice With {
                    .Item = payments.Account.BankAccountNumber
                }
            }
            result.CstmrCdtTrfInitn.PmtInf(0).DbtrAgt = New BranchAndFinancialInstitutionIdentification4 With
            {
                .FinInstnId = New FinancialInstitutionIdentification7()
            }
            result.CstmrCdtTrfInitn.PmtInf(0).ChrgBr = ChargeBearerType1Code.SLEV

            Dim exportedPayments As New List(Of CreditTransferTransactionInformation10)
            For Each payment As ExportedBankPayment In payments.Payments
                exportedPayments.Add(GetPayment(payment, currentCompany.BaseCurrency.Trim().ToUpper()))
            Next

            result.CstmrCdtTrfInitn.PmtInf(0).CdtTrfTxInf = exportedPayments.ToArray()

            Dim serializer As New XmlSerializer(GetType(Document))
            Dim settings As New XmlWriterSettings With {
                    .Indent = True,
                    .IndentChars = " ",
                    .Encoding = New UTF8Encoding(False)
                    }

            Using writer As XmlWriter = XmlWriter.Create(targetStream, settings)
                serializer.Serialize(writer, result)
            End Using

        End Sub


        Private Function GetPayment(payment As ExportedBankPayment, currency As String) As CreditTransferTransactionInformation10

            Dim result As New CreditTransferTransactionInformation10 With {
              .PmtId = New PaymentIdentification1 With {
                    .EndToEndId = payment.UniqueID,
                    .InstrId = payment.UniqueID
                    },
              .Amt = New AmountType3Choice With {
                    .Item = New ActiveOrHistoricCurrencyAndAmount With {
                        .Ccy = currency,
                        .Value = Convert.ToDecimal(payment.Amount)
                    }
                    },
              .Cdtr = New PartyIdentification32 With {
                    .Nm = payment.Receiver.Name
                    },
              .CdtrAcct = New CashAccount16 With {
                    .Id = New AccountIdentification4Choice With {
                        .Item = If(StringIsNullOrEmpty(payment.CustomBankAccount), _
                                   payment.ReceiverBankAccount, payment.CustomBankAccount)
                    }
                    },
              .RmtInf = New RemittanceInformation5()
            }

            If StringIsNullOrEmpty(payment.Description) Then
                result.RmtInf.Strd = New StructuredRemittanceInformation7() {New StructuredRemittanceInformation7()}
                result.RmtInf.Strd(0).CdtrRefInf = New CreditorReferenceInformation2 With
                    {
                    .Tp = New CreditorReferenceType2 With {
                        .CdOrPrtry = New CreditorReferenceType1Choice With {
                            .Item = DocumentType3Code.SCOR
                            }
                        },
                    .Ref = payment.PurposeCode
                    }
            Else 
                result.RmtInf.Ustrd = new String(){ payment.Description }
            End If  

            Return result

        End Function


        Public Overrides Function ToString() As String
            Return Description
        End Function
    
    End Class

End Namespace