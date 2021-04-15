Imports ApskaitaObjects.General

Namespace Documents

    ''' <summary>
    ''' Represents a command that imports invoices received from external systems (integration end point).
    ''' </summary>
    <Serializable()> _
Public NotInheritable Class ImportInvoicesReceivedCommand
        Inherits CommandBase
                         
#Region " Authorization Rules "

        Public Shared Function CanExecuteCommand() As Boolean
            Return ApplicationContext.User.IsInRole("Documents.InvoiceReceived3")
        End Function

#End Region

#Region " Client-side Code "

        Private _Result As String
        Private _Invoices As List(Of InvoiceInfo.InvoiceInfo)


        ''' <summary>
        ''' A human readable description of imported invoices (how many created/updated etc.).
        ''' </summary>
        Public ReadOnly Property Result() As String
            Get
                Return _Result
            End Get
        End Property

        ''' <summary>
        ''' Invoices data to import.
        ''' </summary>
        Public ReadOnly Property Invoices() As List(Of InvoiceInfo.InvoiceInfo)
            Get
                Return _Invoices
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
        ''' Imports invoices received from an external system (integration end point).
        ''' </summary>
        ''' <param name="cInvoices">Invoices data to import.</param>
        ''' <remarks>A human readable description of imported invoices (how many created/updated etc.).</remarks>
        Public Shared Function TheCommand(ByVal cInvoices As List(Of InvoiceInfo.InvoiceInfo)) As String

            if (cInvoices Is Nothing OrElse cInvoices.Count < 1) Then _
                Throw New Exception("Nenurodyta nė viena importuotina gauta sąskaita faktūra.")

            Dim cmd As New ImportInvoicesReceivedCommand
            cmd._Invoices = cInvoices

            cmd.BeforeServer()
            cmd = DataPortal.Execute(Of ImportInvoicesReceivedCommand)(cmd)
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
                My.Resources.Common_SecurityInsertDenied)

            if (Invoices Is Nothing OrElse Invoices.Count < 1) Then _
                Throw New Exception("Nenurodyta nė viena importuotina gauta sąskaita faktūra.")

            Dim clientList As PersonInfoList
            Dim accountList As AccountInfoList
            Dim vatSchemaList As VatDeclarationSchemaInfoList
            Dim serviceList As ServiceInfoList

            Try
                clientList = PersonInfoList.GetListChild()
                accountList = AccountInfoList.GetListChild()
                vatSchemaList = VatDeclarationSchemaInfoList.GetListChild()
                serviceList = ServiceInfoList.GetListChild()
            Catch ex As Exception
                Throw New Exception(String.Format("Error while fetching lookup values:{0}", _
                    ex.Message), ex)
            End Try

            Dim invoicesToSave As new List(Of KeyValuePair(of InvoiceReceived, Person))
            For Each invoice As InvoiceInfo.InvoiceInfo In Invoices
                invoicesToSave.Add(GetOrCreateInvoice(invoice, clientList, _
                    accountList, vatSchemaList, serviceList))
            Next

            Dim newPersons As New List(Of Person)
            Dim newPersonCodes As New List(Of String)
            For Each invoice As KeyValuePair(Of InvoiceReceived,Person) In invoicesToSave
                If (Not invoice.Value Is Nothing AndAlso Not newPersonCodes.Contains(invoice.Value.Code.Trim))
                    newPersons.Add(invoice.Value)
                End If
            Next    

            Dim newInvoicesCount As Integer = 0
            Dim updatedInvoicesCount As Integer = 0

            Dim current as InvoiceReceived = Nothing
            Dim currentPerson As Person = Nothing

            Try

                Using transaction As New SqlTransaction()
                    Try

                        Dim savedPersons As New List(Of Person)
                        For Each newPerson As Person In newPersons
                            currentPerson = newPerson
                            savedPersons.Add(newPerson.SaveChild())
                        Next

                        For Each invoice As KeyValuePair(Of InvoiceReceived,Person) In invoicesToSave
                            
                            current = invoice.Key

                            If Not invoice.Value Is Nothing Then
                                invoice.Key.SetImportedSupplier(PersonInfo.GetPersonInfo( _
                                    GetNewPerson(invoice.Value.Code, savedPersons)))
                            End If                                         

                            If invoice.Key.IsNew Then
                                newInvoicesCount += 1
                            Else 
                                updatedInvoicesCount += 1
                            End If

                            invoice.Key.SaveChild()

                        Next

                        transaction.Commit()

                    Catch ex As Exception
                        transaction.SetNonSqlException(ex)
                        Throw
                    End Try
                End Using

            Catch ex As Exception
                if current Is Nothing Then Throw New Exception(String.Format( _
                    "Nepavyko išsaugoti naujo kontrahento {0}: {1}", _
                    currentPerson.ToString(), ex.Message), ex)
                Throw New Exception(String.Format("Nepavyko išsaugoti sąskaitos faktūros {0}: {1}", _
                    current.ToString(), ex.Message), ex)
            End Try

            _result = "Sąskaitos faktūros sėkmingai importuotos."
            if newInvoicesCount > 0 Then _result = AddWithNewLine(_result, _
                "Įtraukta naujų sąskaitų - " + newInvoicesCount.ToString(), false)
            if updatedInvoicesCount > 0 Then _result = AddWithNewLine(_result, _
                "Pakeista sąskaitų - " + updatedInvoicesCount.ToString(), false)
            if newPersons.Count > 0 Then _result = AddWithNewLine(_result, _
                "Įtraukta naujų kontrahentų - " + newPersons.Count.ToString(), false)

        End Sub

        Friend Shared Function GetOrCreateInvoice(invoice As InvoiceInfo.InvoiceInfo, _
            clientList As PersonInfoList, accountList As AccountInfoList, _
            vatSchemaList As VatDeclarationSchemaInfoList, serviceList As ServiceInfoList) As KeyValuePair(of InvoiceReceived, Person)

            ' --- Validation ---

            If StringIsNullOrEmpty(invoice.ID) Then
                Throw New Exception(String.Format("Invoice id is not specified.{0}------InvoiceData------{1}{2}", _
                    vbCrLf, vbCrLf, InvoiceInfo.ToXmlString(invoice)))
            End If
            If StringIsNullOrEmpty(invoice.Content) Then
                invoice.Content = "Gauta sąskaita faktūra"
            End If
            If invoice.Payer Is Nothing OrElse StringIsNullOrEmpty(invoice.Payer.Code) Then
                Throw New Exception(String.Format("Nenurodytas tiekėjo kodas.{0}------InvoiceData------{1}{2}", _
                    vbCrLf, vbCrLf, InvoiceInfo.ToXmlString(invoice)))
            End If
            If invoice.Payer Is Nothing OrElse StringIsNullOrEmpty(invoice.Payer.Name) Then
                Throw New Exception(String.Format("Nenurodytas tiekėjo pavadinimas.{0}------InvoiceData------{1}{2}", _
                    vbCrLf, vbCrLf, InvoiceInfo.ToXmlString(invoice)))
            End If
            If invoice.InvoiceItems Is Nothing OrElse Not invoice.InvoiceItems.Count > 0 Then
                Throw New Exception(String.Format("Sąskaitoje faktūroje nėra nei vienos eilutės.{0}------InvoiceData------{1}{2}", _
                    vbCrLf, vbCrLf, InvoiceInfo.ToXmlString(invoice)))
            End If

            Dim newClientInfo As InvoiceInfo.ClientInfo = Nothing
            Dim result As InvoiceReceived

            Try
                result = InvoiceReceived.GetOrCreateInvoiceReceivedChild(invoice, clientList, _
                    accountList, vatSchemaList, serviceList, newClientInfo)  
            Catch ex As Exception
                Throw New Exception(String.Format("Nepavyko sukurti ar pakeisti sąskaitos faktūros:{0}{1}.{2}------InvoiceData------{3}{4}", _
                    vbCrLf, ex.Message, vbCrLf, vbCrLf, InvoiceInfo.ToXmlString(invoice)))
            End Try

            ' --- Supplier Data ---
            Dim newSupplier As Person = Nothing
            If result.Supplier = PersonInfo.Empty Then

                invoice.Payer.IsClient = False
                invoice.Payer.IsSupplier = True
                invoice.Payer.IsWorker = False

                newSupplier = Person.NewPersonChild(invoice.Payer)

                If Not newSupplier.IsValid Then Throw New Exception(String.Format( _
                    "Nepakanka duomenų naujo tiekėjo įtraukimui:{0}{1}{2}------InvoiceData------{3}{4}", _
                    vbCrLf, newSupplier.GetAllBrokenRules(), vbCrLf, vbCrLf, InvoiceInfo.ToXmlString(invoice)))

            End If

            Return new KeyValuePair(Of InvoiceReceived,Person)(result, newSupplier)

        End Function

        Friend Shared Sub SaveImportedInvoice(invoice As KeyValuePair(of InvoiceReceived, Person))

            If Not invoice.Value Is Nothing Then
                Dim newClient = invoice.Value.SaveChild()
                invoice.Key.SetImportedSupplier(PersonInfo.GetPersonInfo(newClient))
            End If

            invoice.Key.SaveChild()

        End Sub

        Private Function GetNewPerson(code As String, newPersons As List(Of Person)) As Person

            For Each newPerson As Person In newPersons
                 If (newPerson.Code.Trim = code.Trim) Then Return newPerson 
            Next

            Throw new Exception("Failed to match a new person for code " + code)

        End Function

#End Region
   
End Class

End Namespace
