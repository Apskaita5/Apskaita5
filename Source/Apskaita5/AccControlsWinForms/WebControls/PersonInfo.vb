﻿Imports System.Net
Imports System.Web
Imports System.Xml

Namespace WebControls

    ''' <summary>
    ''' Represents a data about a person that was fetched from web service.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PersonInfo

        Private _Code As String = ""
        Private _Name As String = ""
        Private _Address As String = ""
        Private _VatCode As String = ""
        Private _Message As String = ""


        ''' <summary>
        ''' Gets an official code of the person.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Code() As String
            Get
                Return _Code.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an official name of the person.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property Name() As String
            Get
                Return _Name.Trim
            End Get
            Friend Set(ByVal value As String)
                If value Is Nothing Then value = ""
                _Name = value.Trim
            End Set
        End Property

        ''' <summary>
        ''' Gets an address of the person.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property Address() As String
            Get
                Return _Address.Trim
            End Get
            Friend Set(ByVal value As String)
                If value Is Nothing Then value = ""
                _Address = value.Trim
            End Set
        End Property

        ''' <summary>
        ''' Gets a VAT code of the person.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property VatCode() As String
            Get
                Return _VatCode.Trim
            End Get
            Friend Set(ByVal value As String)
                If value Is Nothing Then value = ""
                _VatCode = value.Trim
            End Set
        End Property

        Public Property Message() As String
            Get
                Return _Message.Trim
            End Get
            Friend Set(ByVal value As String)
                If value Is Nothing Then value = ""
                _Message = value.Trim
            End Set
        End Property


        Private Sub New(ByVal nCode As String)
            If nCode Is Nothing Then nCode = ""
            _Code = nCode.Trim
        End Sub


        ''' <summary>
        ''' Fetches person data by it's official code using Rekvizitai.lt web service.
        ''' </summary>
        ''' <param name="personCode">an official code of the person to the data for</param>
        '''  <remarks></remarks>
        Public Shared Function GetPersonInfoRekvizitai(ByVal personCode As String) As PersonInfo

            If personCode Is Nothing OrElse String.IsNullOrEmpty(personCode.Trim) Then _
                Throw New ArgumentNullException("personCode", "Klaida. Nenurodytas įmonės kodas.")

            Dim result As New PersonInfo(personCode)

            Dim xmlResponse As String = Nothing

            Try
                Using client As New WebClient
                    Dim data As Byte() = client.DownloadData("http://www.rekvizitai.lt/api-xml/?apiKey=fa8cb6d2d671c4f71815b64e63525bff&clientId=1&method=companyDetails&code=" & personCode)
                    xmlResponse = System.Text.Encoding.UTF8.GetString(data)
                End Using
            Catch ex As Exception
                Throw New Exception(String.Format("Nepavyko prisijungti prie Rekvizitai.lt duomenų bazės: {0}", ex.Message), ex)
            End Try

            result = ParseResultForRekvizitai(xmlResponse, result)

            Return result

        End Function

        ''' <summary>
        ''' Fetches a list of person data by it's name fragment using Rekvizitai.lt web service.
        ''' </summary>
        ''' <param name="personNameFragment">a fragment of the person's name to look the data for</param>
        '''  <remarks></remarks>
        Public Shared Function GetPersonInfoListRekvizitai(ByVal personNameFragment As String) As PersonInfo()

            Dim xmlResponse As String = Nothing

            Try
                Using client As New WebClient
                    Dim data As Byte() = client.DownloadData("http://www.rekvizitai.lt/api-xml/?apiKey=fa8cb6d2d671c4f71815b64e63525bff&clientId=1&method=search&query=" _
                                                             & HttpUtility.UrlEncode(personNameFragment))
                    xmlResponse = System.Text.Encoding.UTF8.GetString(data)
                End Using
            Catch ex As Exception
                Throw New Exception("Nepavyko prisijungti prie Rekvizitai.lt duomenų bazės: " & ex.Message, ex)
            End Try

            Dim d As New Xml.XmlDocument()
            d.LoadXml(xmlResponse)

            Dim result As New List(Of PersonInfo)
            For Each node As Xml.XmlNode In d.GetElementsByTagName("code")
                If Not node.InnerText Is Nothing AndAlso Not String.IsNullOrEmpty(node.InnerText.Trim) Then
                    result.Add(GetPersonInfoRekvizitai(node.InnerText))
                End If
            Next

            Return result.ToArray()

        End Function


        Private Shared Function ParseResultForRekvizitai(ByVal webResponseString As String, _
            ByVal currentPersonInfo As PersonInfo) As PersonInfo

            Dim d As New Xml.XmlDocument()
            d.LoadXml(webResponseString.Trim)

            Try

                If d.SelectSingleNode("/data/companies") Is Nothing OrElse _
                   d.SelectSingleNode("/data/companies").FirstChild Is Nothing Then
                    Throw New Exception(String.Format("Rekvizitai.lt duomenų bazėje nėra duomenų apie asmenį, kurio kodas {0}.", _
                                                      currentPersonInfo.Code))
                End If

                currentPersonInfo.Address = GetValueAsString(d.SelectSingleNode("/data/companies").FirstChild, "./address", True)
                currentPersonInfo.Name = GetValueAsString(d.SelectSingleNode("/data/companies").FirstChild, "./title", True)
                currentPersonInfo.VatCode = GetValueAsString(d.SelectSingleNode("/data/companies").FirstChild, "./pvmCode", False)

            Catch ex As Exception
                Throw New Exception("Klaida. Nepavyko iššifruoti iš Rekvizitai.lt gautų duomenų: " & ex.Message, ex)
            End Try

            Return currentPersonInfo

        End Function

        Private Shared Function GetValueAsString(ByVal node As System.Xml.XmlNode, _
            ByVal xpath As String, ByVal throwOnNotFound As Boolean) As String

            Try
                Return node.SelectSingleNode(xpath).InnerText
            Catch ex As Exception
                If throwOnNotFound Then
                    Throw New Exception(String.Format("Klaida gautuose duomenyse, nerastas elementas {0}.", xpath), ex)
                End If
            End Try

            Return ""

        End Function

    End Class

End Namespace