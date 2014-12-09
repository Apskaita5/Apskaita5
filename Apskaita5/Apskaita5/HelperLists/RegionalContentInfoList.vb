Namespace HelperLists

    <Serializable()> _
    Public Class RegionalContentInfoList
        Inherits ReadOnlyListBase(Of RegionalContentInfoList, RegionalContentInfo)

#Region " Business Methods "

        Public Function GetRegionalContent(ByVal LanguageCode As String) As String
            For Each c As RegionalContentInfo In Me
                If c.LanguageCode.Trim.ToUpper = LanguageCode.Trim.ToUpper Then Return c.ContentInvoice
            Next
            Return ""
        End Function

        Public Function GetRegionalMeasureUnit(ByVal LanguageCode As String) As String
            For Each c As RegionalContentInfo In Me
                If c.LanguageCode.Trim.ToUpper = LanguageCode.Trim.ToUpper Then Return c.MeasureUnit
            Next
            Return ""
        End Function

        Public Function GetRegionalVatExempt(ByVal LanguageCode As String) As String
            For Each c As RegionalContentInfo In Me
                If c.LanguageCode.Trim.ToUpper = LanguageCode.Trim.ToUpper Then Return c.VatExempt
            Next
            Return ""
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewRegionalContentInfoList() As RegionalContentInfoList
            Dim result As RegionalContentInfoList = New RegionalContentInfoList
            Return result
        End Function

        Friend Shared Function GetRegionalContentInfoList(ByVal ConcetanatedString As String) As RegionalContentInfoList
            Dim result As RegionalContentInfoList = New RegionalContentInfoList(ConcetanatedString)
            Return result
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal ConcetanatedString As String)
            ' require use of factory methods
            Fetch(ConcetanatedString)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal ConcetanatedString As String)

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As String In ConcetanatedString.Split(New String() {"@*#@"}, _
                StringSplitOptions.RemoveEmptyEntries)
                If Not dr Is Nothing AndAlso Not String.IsNullOrEmpty(dr.Trim) Then _
                    Add(RegionalContentInfo.GetRegionalContentInfo(dr.Trim))
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace