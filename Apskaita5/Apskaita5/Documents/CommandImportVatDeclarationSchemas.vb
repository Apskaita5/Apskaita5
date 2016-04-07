
Imports ApskaitaObjects.My.Resources

Namespace Documents

    ''' <summary>
    ''' Represents a command that imports <see cref="VatDeclarationSchema">VatDeclarationSchema</see>
    ''' data using <see cref="VatDeclarationSchemaProxy">XML proxy data</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class CommandImportVatDeclarationSchemas
        Inherits CommandBase

#Region " Authorization Rules "

        Public Shared Function CanExecuteCommand() As Boolean
            Return VatDeclarationSchema.CanAddObject()
        End Function

#End Region

#Region " Client-side Code "

        Private _Result As String = ""
        Private _XmlProxies As VatDeclarationSchemaProxy()

        ''' <summary>
        ''' gets a human readable result of the command execution (success message or warnings (non critical exceptions))
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Result() As String
            Get
                Return _Result
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
        ''' Imports <see cref="VatDeclarationSchema">VatDeclarationSchema</see>
        ''' data using <see cref="VatDeclarationSchemaProxy">XML proxy data</see> provided
        ''' and returns a human readable result of the command execution (success message or 
        ''' warnings (non critical exceptions))
        ''' </summary>
        ''' <param name="xmlString">an XML string that contains a selialized list of 
        ''' <see cref="VatDeclarationSchemaProxy">VatDeclarationSchemaProxy</see></param>
        ''' <remarks></remarks>
        Public Shared Function TheCommand(ByVal xmlString As String) As String

            If String.IsNullOrEmpty(xmlString) Then
                Throw New ArgumentNullException("xmlString")
            End If

            Dim xmlProxies As List(Of VatDeclarationSchemaProxy) = Nothing
            Try
                xmlProxies = FromXmlString(Of List(Of VatDeclarationSchemaProxy))(xmlString)
            Catch ex As Exception
                Throw New Exception(String.Format(Documents_CommandImportVatDeclarationSchemas_FailedToDeserializeXmlData, ex.Message), ex)
            End Try

            If xmlProxies Is Nothing Then
                Throw New Exception(Documents_CommandImportVatDeclarationSchemas_FailedToDeserializeXmlDataForUnkownReason)
            End If

            Return TheCommand(xmlProxies.ToArray())

        End Function

        ''' <summary>
        ''' Imports <see cref="VatDeclarationSchema">VatDeclarationSchema</see>
        ''' data using <see cref="VatDeclarationSchemaProxy">XML proxy data</see> provided
        ''' and returns a human readable result of the command execution (success message or 
        ''' warnings (non critical exceptions))
        ''' </summary>
        ''' <param name="xmlProxies"><see cref="VatDeclarationSchemaProxy">XML proxy data</see></param>
        ''' <remarks></remarks>
        Public Shared Function TheCommand(ByVal xmlProxies As VatDeclarationSchemaProxy()) As String

            If xmlProxies Is Nothing OrElse xmlProxies.Length < 1 Then
                Throw New ArgumentNullException("xmlProxies")
            End If

            Dim cmd As New CommandImportVatDeclarationSchemas
            cmd._XmlProxies = xmlProxies

            cmd.BeforeServer()
            cmd = DataPortal.Execute(Of CommandImportVatDeclarationSchemas)(cmd)
            HelperLists.VatDeclarationSchemaInfoList.InvalidateCache()
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

            If _XmlProxies Is Nothing OrElse _XmlProxies.Length < 1 Then
                Throw New InvalidOperationException(Documents_CommandImportVatDeclarationSchemas_XmlProxiesNull)
            End If

            Dim successCount As Integer = 0

            Using transaction As New SqlTransaction
                Try

                    For Each proxy As VatDeclarationSchemaProxy In _XmlProxies

                        Dim schema As VatDeclarationSchema = VatDeclarationSchema.NewVatDeclarationSchemaChild(proxy)

                        If schema.IsValid Then
                            schema.SaveChild()
                            successCount += 1
                        Else
                            _Result = AddWithNewLine(_Result, String.Format("Praleista PVM deklaravimo schema ""{0}"", klaidos:{1}{2}", _
                                proxy.Name, vbCrLf, schema.GetAllBrokenRules()), False)
                        End If

                    Next

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try
            End Using

            If successCount = _XmlProxies.Length Then
                _Result = String.Format(Documents_CommandImportVatDeclarationSchemas_SuccessMessage, successCount.ToString)
            ElseIf successCount < 1 Then
                Throw New Exception(String.Format(Documents_CommandImportVatDeclarationSchemas_FailureMessage, _
                    vbCrLf, _Result))
            Else
                _Result = String.Format(Documents_CommandImportVatDeclarationSchemas_WarningMessage, _
                    successCount.ToString, _XmlProxies.Length.ToString, vbCrLf, _Result)
            End If

        End Sub

#End Region

    End Class

End Namespace