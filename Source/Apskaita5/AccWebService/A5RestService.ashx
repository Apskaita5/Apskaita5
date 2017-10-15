<%@ WebHandler Language="VB" Class="AccRestService" Debug="true" %>

Imports System
Imports System.Web
Imports System.xml
Imports ApskaitaObjects
Imports InvoiceInfo
Imports AccCommon

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
                Dim exceptionBytes As Byte() = Encoding.UTF8.GetBytes(FormatExceptionString(ex))
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
        
        Dim enc As New UTF8Encoding(False)
        Return enc.GetBytes(Factory.ToXmlString(New RestResponse(result, e)))
               
    End Function

End Class