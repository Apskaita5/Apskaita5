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
        Private Const ObjectType_InvoiceReceived As String = "InvoiceReceived"

        Private ReadOnly AllowedObjectTypes As String() = New String() _
            {ObjectType_Invoice.ToLowerInvariant(), ObjectType_InvoiceReceived.ToLowerInvariant()}


        Private _invoiceLock As New Object
        Private _invoiceReceivedLock As New Object


        Public Function ProcessRestRequest(ByVal request As HttpRequest) As String

            If request.HttpMethod.ToUpper.Trim() = "GET" OrElse request.HttpMethod.ToUpper.Trim() = "DELETE" Then

                Return ProcessGetRequest(request)

            Elseif request.HttpMethod.ToUpper.Trim() = "POST" Then

                Return ProcessPostRequest(request)

            Else 

                Throw New NotSupportedException(String.Format("Http method {0} is not supported.", _
                                                              request.HttpMethod.ToUpper.Trim()))

            End If

        End Function

        Private Function ProcessPostRequest(ByVal request As HttpRequest) As String

            RestCredentialsList.AuthorizeRest(GetApplicationName(request), GetApplicationToken(request))

            Try

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

                Dim objectType As String = GetObjectType(request)

                Dim result As String = ""

                If objectType.Equals(ObjectType_Invoice, StringComparison.OrdinalIgnoreCase) Then
                    result = SyncInvoice(content)
                ElseIf objectType.Equals(ObjectType_InvoiceReceived, StringComparison.OrdinalIgnoreCase) Then
                    result = SyncInvoiceReceived(content) 
                Else
                    Throw New NotImplementedException(String.Format("ObjectType value {0} is not implemented.", objectType))
                End If

                Return result

            Finally 
                AccDataAccessLayer.Security.AccPrincipal.Logout(New CustomCacheManager)
            End Try

        End Function

        Private Function ProcessGetRequest(ByVal request As HttpRequest) As String

            RestCredentialsList.AuthorizeRest(GetApplicationName(request), GetApplicationToken(request))

            Try

                Dim objectType As String = GetObjectType(request)

                Dim objectId As String = request.QueryString(UrlParamObjectId)
                If StringIsNullOrEmpty(objectId) Then
                    Throw New Exception("ObjectId is not specified in the query string.")
                End If

                Dim result As String = ""

                If objectType.Equals(ObjectType_Invoice, StringComparison.OrdinalIgnoreCase) Then
                    result = DeleteInvoice(objectId)
                ElseIf objectType.Equals(ObjectType_InvoiceReceived, StringComparison.OrdinalIgnoreCase) Then 
                    result = DeleteInvoiceReceived(objectId)
                Else
                    Throw New NotImplementedException(String.Format("ObjectType value {0} is not implemented.", objectType))
                End If

                Return result

            Finally
                AccDataAccessLayer.Security.AccPrincipal.Logout(New CustomCacheManager)
            End Try

        End Function


        Private Function SyncInvoice(ByVal content As String) As String

            Dim proxy As InvoiceInfo.InvoiceInfo = GetInvoiceInfo(content)

            SyncLock _invoiceLock
                SyncInvoice(proxy, content)
            End SyncLock

            Return String.Format("Invoice has been succesfully imported: {0} No. {1}", _
                proxy.Date.ToString("yyyy-MM-dd"), proxy.FullNumber)

        End Function

        Private Sub SyncInvoice(ByVal proxy As InvoiceInfo.InvoiceInfo, ByVal content As String)

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

            Dim newClientInfo As InvoiceInfo.ClientInfo = Nothing
            Dim result As KeyValuePair(of Documents.InvoiceMade, Person)

            Try 
                result = Documents.ImportInvoicesMadeCommand.GetOrCreateInvoice(proxy, _
                clientList, accountList, vatSchemaList, serviceList)
            Catch ex As Exception
                Throw New Exception(String.Format("Error while mapping invoice data:{0}{1}.{2}------InvoiceData------{3}{4}", _
                    vbCrLf, ex.Message, vbCrLf, vbCrLf, content))
            End Try

            Try

                Using transaction As New SqlTransaction()
                    Try                                                                
                        Documents.ImportInvoicesMadeCommand.SaveImportedInvoice(result)
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

        Private Function SyncInvoiceReceived(ByVal content As String) As String

            Dim proxy As InvoiceInfo.InvoiceInfo = GetInvoiceInfo(content)

            SyncLock _invoiceReceivedLock
                SyncInvoiceReceived(proxy, content)
            End SyncLock

            Return String.Format("Invoice has been successfully imported: {0} No. {1}", _
                proxy.Date.ToString("yyyy-MM-dd"), proxy.FullNumber)

        End Function

        Private Sub SyncInvoiceReceived(ByVal proxy As InvoiceInfo.InvoiceInfo, ByVal content As String)

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
            
            Dim result As KeyValuePair(of Documents.InvoiceReceived, Person)

            Try 
                result = Documents.ImportInvoicesReceivedCommand.GetOrCreateInvoice(proxy, _
                    clientList, accountList, vatSchemaList, serviceList)
            Catch ex As Exception
                Throw New Exception(String.Format("Error while mapping invoice data:{0}{1}.{2}------InvoiceData------{3}{4}", _
                    vbCrLf, ex.Message, vbCrLf, vbCrLf, content))
            End Try

            Try

                Using transaction As New SqlTransaction()
                    Try                                                                
                        Documents.ImportInvoicesReceivedCommand.SaveImportedInvoice(result)
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

        Private Function DeleteInvoiceReceived(ByVal objectId As String) As String

            Try
                SyncLock _invoiceReceivedLock
                    Documents.InvoiceReceived.DeleteInvoiceByExternalID(objectId)
                End SyncLock
            Catch ex As Exception
                Throw New Exception(String.Format("Error while deleting invoice No. {0}:{1}{2}", _
                    objectId, vbCrLf, ex.Message), ex)
            End Try

            Return String.Format("Invoice has been successfully deleted:{0}.", objectId)

        End Function


        Private Function GetObjectType(request As HttpRequest) As String

            Dim result As String = request.QueryString(UrlParamObjectType)

            If String.IsNullOrEmpty(result) Then Throw New Exception( _
                "ObjectType is not specified in the query string.")

            result = result.Trim()

            If Array.IndexOf(AllowedObjectTypes, result.ToLowerInvariant()) < 0 Then
                Throw New Exception(String.Format("ObjectType value {0} is invalid.", result))
            End If

            Return result

        End Function

        Private Function GetApplicationName(request As HttpRequest) As String

            Dim result As String = request.Params(UrlParamApplicationName)

            If StringIsNullOrEmpty(result) Then Throw New Exception( _
                "ApplicationName is not specified in the query string.")

            Return result.Trim()

        End Function

        Private Function GetApplicationToken(request As HttpRequest) As String

            Dim result As String = request.Params(UrlParamApplicationToken)

            If StringIsNullOrEmpty(result) Then

                Dim authHeader As string = request.Headers("Authorization")

                If (Not StringIsNullOrEmpty(authHeader) AndAlso _
                    authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) Then
                    result = authHeader.Substring(7)
                End If

            End If

            If StringIsNullOrEmpty(result) Then Throw New Exception( _
                "Application token is not specified neither in the query string (ApplicationToken) nor Authorization header (Bearer).")

            Return result.Trim()

        End Function

        Private Function GetInvoiceInfo(ByVal content As String) As InvoiceInfo.InvoiceInfo

            Dim result As InvoiceInfo.InvoiceInfo = Nothing

            Try

                result = InvoiceInfo.FromXmlString(Of InvoiceInfo.InvoiceInfo)(content)

            Catch ex As Exception
                Throw New Exception(String.Format("Invalid payload data for invoice: {0}{1}------InvoiceData------{2}{3}", _
                                                  ex.Message, vbCrLf, vbCrLf, content), ex)
            End Try

            ' --- Validation ---

            If StringIsNullOrEmpty(result.ID) Then
                Throw New Exception(String.Format("Invoice id is not specified.{0}------InvoiceData------{1}{2}", _
                                                  vbCrLf, vbCrLf, content))
            End If
            If StringIsNullOrEmpty(result.Content) Then
                result.Content = "Išrašyta sąskaita faktūra"
            End If
            If result.Payer Is Nothing OrElse StringIsNullOrEmpty(result.Payer.Code) Then
                Throw New Exception(String.Format("Invoice person (code) is not specified.{0}------InvoiceData------{1}{2}", _
                                                  vbCrLf, vbCrLf, content))
            End If
            If result.Payer Is Nothing OrElse StringIsNullOrEmpty(result.Payer.Name) Then
                Throw New Exception(String.Format("Invoice person (name) is not specified.{0}------InvoiceData------{1}{2}", _
                                                  vbCrLf, vbCrLf, content))
            End If
            If Not result.InvoiceItems.Count > 0 Then
                Throw New Exception(String.Format("Invoice items are not specified.{0}------InvoiceData------{1}{2}", _
                                                  vbCrLf, vbCrLf, content))
            End If

            Return result

        End Function

    End Module

End Namespace
