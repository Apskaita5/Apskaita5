Imports System.Web
Imports System.IO
Imports ApskaitaObjects.General

Namespace Extensibility

    Public Module RestHandle

        Private Const UrlParamApplicationName As String = "ApplicationName"
        Private Const UrlParamApplicationToken As String = "ApplicationToken"
        Private Const UrlParamObjectType As String = "ObjectType"
        Private Const UrlParamObjectId As String = "ObjectId"
        Private Const ObjectType_Invoice As String = "Invoice"

        Private ReadOnly AllowedObjectTypes As String() = New String() {ObjectType_Invoice.ToLower}


        Private _invoiceLock As New Object


        Public Function ProcessRestRequest(ByVal request As HttpRequest) As String

            If request.HttpMethod.ToUpper.Trim() = "GET" Then

                Return ProcessGetRequest(request)

            Else

                Return ProcessPostRequest(request)

            End If

        End Function

        Private Function ProcessPostRequest(ByVal request As HttpRequest) As String

            ' get xml document from request body
            Dim content As String = ""

            Try

                Using stream As New StreamReader(request.InputStream, System.Text.Encoding.Unicode)
                    content = stream.ReadToEnd()
                End Using

                content = HttpUtility.UrlDecode(content)

            Catch ex As Exception
                Throw New Exception(String.Format("Invalid payload data: {0}{1}------Content------{2}{3}", _
                    ex.Message, vbCrLf, vbCrLf, content), ex)
            End Try

            Dim objectType As String = request.QueryString(UrlParamObjectType)
            If objectType Is Nothing OrElse String.IsNullOrEmpty(objectType.Trim) Then
                Throw New Exception("ObjectType is not specified in the query string.")
            ElseIf Array.IndexOf(AllowedObjectTypes, objectType.Trim.ToLower) < 0 Then
                Throw New Exception(String.Format("ObjectType value {0} is invalid.", objectType))
            End If

            RestCredentialsList.AuthorizeRest(request.Params(UrlParamApplicationName), _
                request.Params(UrlParamApplicationToken))

            Dim result As String = ""

            Try
                If objectType.Trim.ToLower = ObjectType_Invoice.Trim.ToLower Then
                    result = SyncInvoice(content)
                Else
                    Throw New NotImplementedException(String.Format("ObjectType value {0} is not implemented.", objectType))
                End If
            Finally
                AccDataAccessLayer.Security.AccPrincipal.Logout(New CustomCacheManager)
            End Try

            Return result

        End Function

        Private Function ProcessGetRequest(ByVal request As HttpRequest) As String

            Dim objectType As String = request.QueryString(UrlParamObjectType)
            If StringIsNullOrEmpty(objectType) Then
                Throw New Exception("ObjectType is not specified in the query string.")
            ElseIf Array.IndexOf(AllowedObjectTypes, objectType.Trim.ToLower) < 0 Then
                Throw New Exception(String.Format("ObjectType value {0} is invalid.", objectType))
            End If

            Dim objectId As String = request.QueryString(UrlParamObjectId)
            If StringIsNullOrEmpty(objectId) Then
                Throw New Exception("ObjectId is not specified in the query string.")
            End If

            RestCredentialsList.AuthorizeRest(request.Params(UrlParamApplicationName), _
                request.Params(UrlParamApplicationToken))

            Dim result As String = ""

            Try
                If objectType.Trim.ToLower = ObjectType_Invoice.Trim.ToLower Then
                    result = DeleteInvoice(objectId)
                Else
                    Throw New NotImplementedException(String.Format("ObjectType value {0} is not implemented.", objectType))
                End If
            Finally
                AccDataAccessLayer.Security.AccPrincipal.Logout(New CustomCacheManager)
            End Try

            Return result

        End Function


        Private Function SyncInvoice(ByVal content As String) As String

            Dim proxy As InvoiceInfo.InvoiceInfo = Nothing

            Try

                proxy = InvoiceInfo.FromXmlString(Of InvoiceInfo.InvoiceInfo)(content)

            Catch ex As Exception
                Throw New Exception(String.Format("Invalid payload data for invoice: {0}{1}------InvoiceData------{2}{3}", _
                    ex.Message, vbCrLf, vbCrLf, content), ex)
            End Try

            ' --- Validation ---

            If StringIsNullOrEmpty(proxy.ID) Then
                Throw New Exception(String.Format("Invoice id is not specified.{0}------InvoiceData------{1}{2}", _
                    vbCrLf, vbCrLf, content))
            End If
            If StringIsNullOrEmpty(proxy.Content) Then
                proxy.Content = "Išrašyta sąskaita faktūra"
            End If
            If proxy.Payer Is Nothing OrElse StringIsNullOrEmpty(proxy.Payer.Code) Then
                Throw New Exception(String.Format("Invoice client (code) is not specified.{0}------InvoiceData------{1}{2}", _
                    vbCrLf, vbCrLf, content))
            End If
            If proxy.Payer Is Nothing OrElse StringIsNullOrEmpty(proxy.Payer.Name) Then
                Throw New Exception(String.Format("Invoice client (name) is not specified.{0}------InvoiceData------{1}{2}", _
                    vbCrLf, vbCrLf, content))
            End If
            If Not proxy.InvoiceItems.Count > 0 Then
                Throw New Exception(String.Format("Invoice items are not specified.{0}------InvoiceData------{1}{2}", _
                    vbCrLf, vbCrLf, content))
            End If

            SyncLock _invoiceLock
                SyncInvoice(proxy, content)
            End SyncLock

            Return String.Format("Invoice has been succesfully imported: {0} No. {1}", _
                proxy.Date.ToString("yyyy-MM-dd"), proxy.FullNumber)

        End Function

        Private Sub SyncInvoice(ByVal proxy As InvoiceInfo.InvoiceInfo, ByVal content As String)

            Dim newClientInfo As InvoiceInfo.ClientInfo = Nothing
            Dim result As Documents.InvoiceMade

            Try
                Dim clientList As PersonInfoList = PersonInfoList.GetListChild()
                Dim accountList As AccountInfoList = AccountInfoList.GetListChild()
                Dim vatSchemaList As VatDeclarationSchemaInfoList = VatDeclarationSchemaInfoList.GetListChild()
                result = Documents.InvoiceMade.GetOrCreateInvoiceMadeChild(proxy, False, _
                    clientList, accountList, vatSchemaList, newClientInfo)
            Catch ex As Exception
                Throw New Exception(String.Format("Error while mapping invoice data:{0}{1}.{2}------InvoiceData------{3}{4}", _
                    vbCrLf, ex.Message, vbCrLf, vbCrLf, content))
            End Try

            ' --- Client Data ---
            Dim newClient As Person = Nothing
            If result.Payer = PersonInfo.Empty Then

                newClient = Person.NewPersonChild()

                newClient.AccountAgainstBankBuyer = GetCurrentCompany.GetDefaultAccount(DefaultAccountType.Buyers)
                newClient.AccountAgainstBankSupplyer = GetCurrentCompany.GetDefaultAccount(DefaultAccountType.Suppliers)
                newClient.Address = proxy.Payer.Address
                If StringIsNullOrEmpty(newClient.Address) Then
                    newClient.Address = "Nenurodytas"
                End If
                newClient.Code = proxy.Payer.Code
                newClient.CodeIsNotReal = proxy.Payer.IsCodeLocal
                newClient.CodeVAT = proxy.Payer.CodeVAT
                newClient.ContactInfo = proxy.Payer.Contacts
                newClient.CurrencyCode = proxy.Payer.CurrencyCode
                If StringIsNullOrEmpty(newClient.CurrencyCode) Then
                    newClient.CurrencyCode = "EUR"
                End If
                newClient.Email = proxy.Payer.Email
                newClient.InternalCode = proxy.Payer.BreedCode
                newClient.IsClient = proxy.Payer.IsClient
                newClient.IsNaturalPerson = proxy.Payer.IsNaturalPerson
                newClient.IsSupplier = proxy.Payer.IsSupplier
                newClient.IsWorker = proxy.Payer.IsWorker
                newClient.LanguageCode = proxy.Payer.LanguageCode
                If StringIsNullOrEmpty(newClient.LanguageCode) Then
                    newClient.LanguageCode = "LT"
                End If
                newClient.Name = proxy.Payer.Name

                If Not newClient.IsValid Then
                    Throw New Exception(String.Format("Failed to create a new client:{0}{1}{2}------InvoiceData------{3}{4}", _
                        vbCrLf, newClient.GetAllBrokenRules(), vbCrLf, vbCrLf, content))
                End If

            End If

            Try

                Using transaction As New SqlTransaction()
                    Try

                        If Not newClient Is Nothing Then
                            newClient = newClient.SaveChild()
                            result.SetImportedPayer(PersonInfo.GetPersonInfo(newClient))
                        End If

                        result.SaveChild()

                        transaction.Commit()

                    Catch ex As Exception
                        transaction.SetNonSqlException(ex)
                        Throw
                    End Try
                End Using

            Catch ex As Exception
                Throw New Exception(String.Format("Failed to save an invoice:{0}{1}{2}------InvoiceData------{3}{4}", _
                    vbCrLf, ex.Message, vbCrLf, vbCrLf, content), ex)
            End Try

        End Sub

        Private Function DeleteInvoice(ByVal objectId As String) As String

            Try
                SyncLock _invoiceLock
                    Documents.InvoiceMade.DeleteInvoiceByExternalID(objectId)
                End SyncLock
            Catch ex As Exception
                Throw New Exception(String.Format("Error while deleting invoice No. {0}:{1}{2}", _
                    objectId, vbCrLf, ex.Message), ex)
            End Try

            Return String.Format("Invoice has been succesfully deleted:{0}.", objectId)

        End Function

    End Module

End Namespace