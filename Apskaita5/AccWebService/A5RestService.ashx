<%@ WebHandler Language="VB" Class="AccRestService" Debug="true" %>

Imports System
Imports System.Web
Imports System.xml
Imports ApskaitaObjects
Imports InvoiceInfo

Public Class AccRestService : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
        Dim result As String = ""
        Dim e As Exception = Nothing
        
        Try
            
            result = ApskaitaObjects.Extensibility.ProcessRestRequest(context.Request)
                        
        Catch ex As Exception
            
            If TypeOf ex Is System.Security.SecurityException OrElse IsSqlException(ex) Then
                If TypeOf ex Is System.Security.SecurityException Then
                    context.Response.StatusCode = 403
                    context.Response.StatusDescription = "Forbidden"
                ElseIf IsSqlException(ex) Then
                    context.Response.StatusCode = 404
                    context.Response.StatusDescription = "Not Found"
                End If
                context.Response.ContentEncoding = Encoding.UTF8
                Dim exceptionBytes As Byte() = Encoding.UTF8.GetBytes(ex.Message)
                context.Response.ContentType = "text/plain"
                context.Response.Charset = Encoding.UTF8.WebName
                context.Response.OutputStream.Write(exceptionBytes, 0, exceptionBytes.Length)
                Exit Sub
            End If
            
            e = ex
            
        End Try
        
        Dim responseBytes As Byte() = GetResponseBytes(result, e)
        
        context.Response.ContentEncoding = Encoding.UTF8
        context.Response.StatusCode = 200
        context.Response.StatusDescription = "OK"
        context.Response.ContentType = "text/xml"
        context.Response.Charset = Encoding.UTF8.WebName
        context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length)
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property
    
    Private Function GetResponseBytes(ByVal result As String, ByVal e As Exception) As Byte()
        
        Dim response As New RestResponse(result, e)
        Dim serializer As New Xml.Serialization.XmlSerializer(GetType(RestResponse))
        Dim settings As New XmlWriterSettings

        settings.Indent = True
        settings.IndentChars = " "
        settings.Encoding = New System.Text.UnicodeEncoding()

        Using ms As New IO.MemoryStream
            Using writer As XmlWriter = XmlWriter.Create(ms, settings)
                serializer.Serialize(writer, response)
                Return ms.ToArray()
            End Using
        End Using
               
    End Function

End Class