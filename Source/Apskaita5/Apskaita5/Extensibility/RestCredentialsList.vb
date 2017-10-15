Imports System.Xml.Serialization
Imports System.Xml
Imports System.Text
Imports System.IO

Namespace Extensibility

    <Serializable()> _
    Public Class RestCredentialsList

        Private Const RestCredentialsFile As String = "A5RestCredentials.xml"

        Private _RestCredentialsList As New List(Of RestCredentials)


        Public Property RestCredentialsList() As List(Of RestCredentials)
            Get
                Return _RestCredentialsList
            End Get
            Set(ByVal value As List(Of RestCredentials))
                _RestCredentialsList = value
            End Set
        End Property


        Public Sub New()
        End Sub


        Public Sub Save(ByVal fileName As String)

            Dim serializer As New XmlSerializer(GetType(RestCredentialsList))
            Dim settings As New XmlWriterSettings
            settings.Encoding = New UnicodeEncoding(False, False) ' no BOM in a .NET string
            settings.Indent = True
            settings.OmitXmlDeclaration = False

            Using textWriter As New StringWriter
                Using xmlWriter As XmlWriter = System.Xml.XmlWriter.Create(textWriter, settings)
                    serializer.Serialize(xmlWriter, Me)
                End Using
                IO.File.WriteAllText(fileName, textWriter.ToString, New UnicodeEncoding(False, False))
            End Using

        End Sub


        Public Shared Function GetCredentialsList() As RestCredentialsList

            Dim serializer As New XmlSerializer(GetType(RestCredentialsList))
            Dim path As String = IO.Path.Combine(System.Web.HttpContext.Current. _
                Server.MapPath("App_Data"), RestCredentialsFile)
            'Dim path As String = IO.Path.Combine("D:\My Documents\My Projects\LindenLawAccounting\llaWebService\App_Data", _
            '    RestCredentialsFile) ' debug location
            Using reader As New IO.StreamReader(path, New UnicodeEncoding(False, False))
                Return DirectCast(serializer.Deserialize(reader), RestCredentialsList)
            End Using

        End Function

        Public Shared Sub AuthorizeRest(ByVal name As String, ByVal token As String)

            If name Is Nothing OrElse String.IsNullOrEmpty(name.Trim) Then _
                Throw New System.Security.SecurityException("User is not identified.")
            If token Is Nothing OrElse String.IsNullOrEmpty(token.Trim) Then _
                Throw New System.Security.SecurityException("Security token is not provided.")

            Dim credentials As RestCredentialsList = GetCredentialsList()
            For Each c As RestCredentials In credentials._RestCredentialsList
                If c.Name.Trim = Name.Trim AndAlso c.Token.Trim = Token.Trim Then
                    AccDataAccessLayer.Security.AccPrincipal.LoginAsServer(name, c.Database, _
                        New CustomCacheManager)
                    Exit Sub
                End If
            Next

            Throw New System.Security.SecurityException("User identified by " & name.Trim _
                & ", token " & token & " is not authorized to use Apskaita5 REST services.")

        End Sub


        Public Shared Sub SaveTestCredentials()

            Dim credentials As New RestCredentialsList

            Dim restCredentials As New RestCredentials
            restCredentials.Database = "apskaita04"
            restCredentials.Name = "test"
            restCredentials.Token = "testtoken"

            credentials.RestCredentialsList.Add(restCredentials)

            credentials.Save("C:\" & RestCredentialsFile)

        End Sub

    End Class

End Namespace