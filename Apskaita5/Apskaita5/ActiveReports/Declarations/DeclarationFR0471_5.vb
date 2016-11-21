Imports ApskaitaObjects.Workers

Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of an <see cref="IPayOutsNaturalPersonsWithoutTaxesDeclaration">IPayOutsNaturalPersonsWithoutTaxesDeclaration</see>
    ''' for a state tax inspectorate (VMI) report No. FR0471 version 5.
    ''' </summary>
    ''' <remarks>Object is responsible for transforming payments to natural persons data to ffdata format 
    ''' (required by the FormFiller application).</remarks>
    <Serializable()> _
    Public Class DeclarationFR0471_5
        Implements IPayOutsNaturalPersonsWithoutTaxesDeclaration

        Private Const DECLARATION_NAME As String = "FR0471 v. 5"
        Private Const FILENAMEMXFD As String = "MXFD\FR0471(5).mxfd"
        Private Const FILENAMEFFDATA As String = "FFData\FR0471(5).ffdata"
        Private ReadOnly VALID_FROM As Date = New Date(2014, 1, 1)


        ''' <summary>
        ''' Gets a name of the payouts declaration.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String _
            Implements IPayOutsNaturalPersonsWithoutTaxesDeclaration.Name
            Get
                Return DECLARATION_NAME
            End Get
        End Property

        ''' <summary>
        ''' Gets a start of the period that the payouts declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidFrom() As Date _
            Implements IPayOutsNaturalPersonsWithoutTaxesDeclaration.ValidFrom
            Get
                Return VALID_FROM
            End Get
        End Property

        ''' <summary>
        ''' Gets an end of the period that the payouts declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidTo() As Date _
            Implements IPayOutsNaturalPersonsWithoutTaxesDeclaration.ValidTo
            Get
                Return Date.MaxValue
            End Get
        End Property


        ''' <summary>
        ''' Gets a ffdata format data as an XML string.
        ''' </summary>
        ''' <param name="payouts">a list of the payouts data to be exported</param>
        ''' <remarks></remarks>
        Public Function GetFfDataString(ByVal payouts As PayOutNaturalPersonWithoutTaxesList) As String _
            Implements IPayOutsNaturalPersonsWithoutTaxesDeclaration.GetFfDataString

            Dim declarationFilePath As String = IO.Path.Combine(AppPath(), FILENAMEFFDATA)

            Dim myDoc As New Xml.XmlDocument
            myDoc.Load(declarationFilePath)

            Dim subtotals As List(Of Subtotal) = Subtotal.GetSubtotals(payouts)
            Dim payoutsPages As List(Of List(Of Subtotal)) = GetPages(subtotals)
            Dim declarationPages As Xml.XmlNodeList = GetPages(myDoc, payoutsPages.Count)

            myDoc.ChildNodes(1).Attributes(2).Value = GetCurrentIdentity.Name
            myDoc.ChildNodes(1).Attributes(3).Value = GetDateInFFDataFormat(Today)
            myDoc.ChildNodes(1).ChildNodes(0).Attributes(1).Value = IO.Path.Combine(AppPath(), FILENAMEMXFD)

            Dim amountTotal As Double = 0
            Dim lineNo As Integer = 0

            For i As Integer = 1 To payoutsPages.Count

                Dim amountInPage As Double = 0

                For j As Integer = 1 To payoutsPages(i - 1).Count

                    amountInPage = CRound(amountInPage + payoutsPages(i - 1)(j - 1).Amount)
                    lineNo += 1

                    For Each node As Xml.XmlNode In declarationPages(i - 1).ChildNodes(0).ChildNodes

                        If node.Attributes(0).Value = "P11-" & j.ToString OrElse _
                            node.Attributes(0).Value = "E11-" & j.ToString Then
                            node.InnerText = lineNo.ToString()
                        ElseIf node.Attributes(0).Value = "P12-" & j.ToString OrElse _
                            node.Attributes(0).Value = "E12-" & j.ToString Then
                            If payoutsPages(i - 1)(j - 1).Receiver <> PersonInfo.Empty Then
                                node.InnerText = payoutsPages(i - 1)(j - 1).Receiver.Code
                            End If
                        ElseIf node.Attributes(0).Value = "P12_1-" & j.ToString OrElse _
                            node.Attributes(0).Value = "E12_1-" & j.ToString Then
                            node.InnerText = "1"
                        ElseIf node.Attributes(0).Value = "P13-" & j.ToString OrElse _
                            node.Attributes(0).Value = "E13-" & j.ToString Then
                            If payoutsPages(i - 1)(j - 1).Receiver <> PersonInfo.Empty Then
                                node.InnerText = payoutsPages(i - 1)(j - 1).Receiver.Name
                            End If
                        ElseIf node.Attributes(0).Value = "P14-" & j.ToString OrElse _
                            node.Attributes(0).Value = "E14-" & j.ToString Then
                            node.InnerText = payoutsPages(i - 1)(j - 1).Code.ToString("00")
                        ElseIf node.Attributes(0).Value = "P15-" & j.ToString OrElse _
                            node.Attributes(0).Value = "E15-" & j.ToString Then
                            node.InnerText = GetNumberInFFDataFormat(payoutsPages(i - 1)(j - 1).Amount)
                        End If

                    Next

                Next

                For Each node As Xml.XmlNode In declarationPages(i - 1).ChildNodes(0).ChildNodes
                    If node.Attributes(0).Value = "P19" OrElse node.Attributes(0).Value = "E19" Then
                        node.InnerText = GetNumberInFFDataFormat(amountInPage)
                    ElseIf node.Attributes(0).Value = "B_MM_ID" Then
                        node.InnerText = GetCurrentCompany.Code
                    ElseIf node.Attributes(0).Value = "LapoNr" Then
                        node.InnerText = (i - 1).ToString()
                    ElseIf node.Attributes(0).Value = "B_ML_Metai" Then
                        node.InnerText = payouts.DateFrom.Year.ToString()
                    ElseIf node.Attributes(0).Value = "B_FormNr" Then

                    ElseIf node.Attributes(0).Value = "B_FormVerNr" Then

                    End If
                Next

                amountTotal = CRound(amountInPage + amountTotal)

            Next

            For Each node As Xml.XmlNode In declarationPages(0).ChildNodes(0).ChildNodes
                If node.Attributes(0).Value = "B_MM_Pavad" Then
                    node.InnerText = GetLimitedLengthString(GetCurrentCompany.Name, 45)
                ElseIf node.Attributes(0).Value = "B_MM_Adresas" Then
                    node.InnerText = GetLimitedLengthString(GetCurrentCompany.Address, 45)
                ElseIf node.Attributes(0).Value = "B_MM_Epastas" Then
                    node.InnerText = GetLimitedLengthString(GetCurrentCompany.Email, 32)
                ElseIf node.Attributes(0).Value = "E17" Then
                    If payoutsPages.Count > 1 Then
                        node.InnerText = (payoutsPages.Count - 1).ToString()
                    Else
                        node.InnerText = ""
                    End If
                ElseIf node.Attributes(0).Value = "E20" Then
                    node.InnerText = GetNumberInFFDataFormat(amountTotal)
                End If
            Next

            Return myDoc.OuterXml

        End Function

        Private Function GetPages(ByVal payouts As List(Of Subtotal)) As List(Of List(Of Subtotal))

            Dim result As New List(Of List(Of Subtotal))

            Dim firstPage As New List(Of Subtotal)

            For i As Integer = 1 To Math.Min(2, payouts.Count)
                firstPage.Add(payouts(i - 1))
            Next

            result.Add(firstPage)

            For i As Integer = 1 To Math.Ceiling((payouts.Count - 2) / 5)

                Dim page As New List(Of Subtotal)

                For j As Integer = 1 To Math.Min(5, payouts.Count - 2 - ((i - 1) * 5))
                    page.Add(payouts(1 + ((i - 1) * 5) + j))
                Next

                result.Add(page)

            Next

            Return result

        End Function

        Private Function GetPages(ByVal doc As Xml.XmlDocument, ByVal count As Integer) As Xml.XmlNodeList

            If count = 1 Then

                doc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).RemoveChild( _
                    doc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).ChildNodes(1))
                doc.ChildNodes(1).ChildNodes(0).ChildNodes(1).RemoveChild( _
                    doc.ChildNodes(1).ChildNodes(0).ChildNodes(1).ChildNodes(1))

            ElseIf count > 2 Then

                For i As Integer = 3 To count
                    Dim addPg1 As Xml.XmlElement = DirectCast(doc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(0).ChildNodes(0).ChildNodes(1).Clone, Xml.XmlElement)
                    doc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(addPg1)
                    Dim addPg2 As Xml.XmlElement = DirectCast(doc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(1).ChildNodes(1).Clone, Xml.XmlElement)
                    addPg2.Attributes(1).Value = i.ToString
                    doc.ChildNodes(1).ChildNodes(0).ChildNodes(1).AppendChild(addPg2)
                Next

            End If

            doc.ChildNodes(1).ChildNodes(0).ChildNodes(1).Attributes(0).Value = count.ToString

            Return doc.GetElementsByTagName("Page")

        End Function


        Private Class Subtotal

            Private _Receiver As PersonInfo = Nothing
            Private _Amount As Double = 0
            Private _Code As Integer = 0


            Public ReadOnly Property Receiver() As PersonInfo
                Get
                    Return _Receiver
                End Get
            End Property

            Public ReadOnly Property Amount() As Double
                Get
                    Return CRound(_Amount)
                End Get
            End Property

            Public ReadOnly Property Code() As Integer
                Get
                    Return _Code
                End Get
            End Property


            Private Sub New(ByVal item As PayOutNaturalPersonWithoutTaxes)
                _Receiver = item.PaymentReceiver
                _Code = item.TaxCode
                _Amount = item.PaymentAmount
            End Sub


            Private Sub Add(ByVal item As Subtotal)
                _Amount = CRound(_Amount + item.Amount)
            End Sub

            Private Function GetDictionaryKey() As String
                Return _Receiver.ID.ToString() & "-" & _Code.ToString()
            End Function


            Public Shared Function GetSubtotals(ByVal items As PayOutNaturalPersonWithoutTaxesList) As List(Of Subtotal)

                Dim dict As New Dictionary(Of String, Subtotal)

                For Each item As PayOutNaturalPersonWithoutTaxes In items

                    If item.PaymentReceiver <> PersonInfo.Empty Then

                        Dim newEntry As New Subtotal(item)

                        If dict.ContainsKey(newEntry.GetDictionaryKey()) Then
                            dict(newEntry.GetDictionaryKey()).Add(newEntry)
                        Else
                            dict.Add(newEntry.GetDictionaryKey(), newEntry)
                        End If

                    End If

                Next

                Dim result As New List(Of Subtotal)

                For Each entry As KeyValuePair(Of String, Subtotal) In dict
                    result.Add(entry.Value)
                Next

                Return result

            End Function

        End Class

    End Class

End Namespace