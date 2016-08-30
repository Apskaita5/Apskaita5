Imports ApskaitaObjects.My.Resources

Namespace ActiveReports

    <Serializable()> _
    Public Class UserReport
        Inherits ReadOnlyBase(Of UserReport)

#Region " Business Methods "

        Private ReadOnly ParamKeys As String() = New String() {"?AA", "?AB", _
            "?AC", "?AD", "?AE", "?AF", "?AG", "?AH", "?AI", "?AJ", "?AK", "?AL", _
            "?AM", "?AN", "?AO", "?AP", "?AQ", "?AR", "?AT", "?AW", "?AZ", "?BA", _
            "?BC", "?BD", "?BE", "?BF", "?BG", "?BH", "?BI", "?BJ", "?BK", "?BL", _
            "?BM", "?BN", "?BO", "?BP", "?BQ", "?BR", "?BS", "?BT", "?BW", "?BZ", _
            "?CA", "?CB", "?CC", "?CD", "?CE", "?CF", "?CG", "?CH"}

        Private Const EMPTY_TABLE_MARKER As String = "%#DELETE#"

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ReportInfo As UserReportInfo = Nothing
        Private _ReportParams As List(Of KeyValuePair(Of String, Object)) = Nothing
        Private _ReportRdlContent As String = ""

        ' manual (de)serializacion of dataset (to string) is used because 
        ' current dataset serialization by MS does not work with web services (at least)
        <NonSerialized()> _
        Private _ReportDataSet As DataSet = Nothing
        Private _ReportDataSetString As String = ""


        ''' <summary>
        ''' Gets general information about the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ReportInfo() As UserReportInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ReportInfo
            End Get
        End Property

        ''' <summary>
        ''' Gets the report data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ReportDataSet() As DataSet
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _ReportDataSet Is Nothing AndAlso StringIsNullOrEmpty(_ReportDataSetString) Then Return Nothing

                If _ReportDataSet Is Nothing Then

                    Using reader As IO.StringReader = New IO.StringReader(_ReportDataSetString)
                        _ReportDataSet = New DataSet("ReportDataSet")
                        _ReportDataSet.ReadXml(reader)
                    End Using

                    For Each table As DataTable In _ReportDataSet.Tables
                        If table.Rows.Count = 1 AndAlso Not table.Rows(0).Item(0) Is Nothing _
                           AndAlso table.Rows(0).Item(0).ToString.Trim.ToUpper() = EMPTY_TABLE_MARKER Then
                            table.Rows.RemoveAt(0)
                        End If
                    Next

                End If

                Return _ReportDataSet
            End Get
        End Property

        ''' <summary>
        ''' Gets the report parameters.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ReportParams() As List(Of KeyValuePair(Of String, Object))
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ReportParams
            End Get
        End Property

        ''' <summary>
        ''' Gets the report file (*.rdl) content.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ReportRdlContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ReportRdlContent.Trim
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return _ReportInfo.ToString()
        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.UserReportInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new instance of the specified user report.
        ''' </summary>
        ''' <param name="reportInfo">a report to fetch</param>
        ''' <param name="params">params required by the report</param>
        ''' <remarks></remarks>
        Public Shared Function GetUserReport(ByVal reportInfo As UserReportInfo, _
                ByVal params As List(Of KeyValuePair(Of String, Object))) As UserReport
            Return DataPortal.Fetch(Of UserReport)(New Criteria(reportInfo, params))
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ReportInfo As UserReportInfo
            Private _Params As List(Of KeyValuePair(Of String, Object)) = Nothing
            Public ReadOnly Property ReportInfo() As UserReportInfo
                Get
                    Return _ReportInfo
                End Get
            End Property
            Public ReadOnly Property Params() As List(Of KeyValuePair(Of String, Object))
                Get
                    Return _Params
                End Get
            End Property
            Public Sub New(ByVal nReportInfo As UserReportInfo, _
                ByVal nParams As List(Of KeyValuePair(Of String, Object)))
                _ReportInfo = nReportInfo
                _Params = nParams
            End Sub
        End Class


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            _ReportInfo = criteria.ReportInfo
            _ReportParams = criteria.Params

            _ReportRdlContent = IO.File.ReadAllText(IO.Path.Combine(IO.Path.Combine( _
                AppPath(), USERREPORTSFOLDER), criteria.ReportInfo.FileName), _
                Text.Encoding.UTF8)

            Dim data As New Xml.XmlDocument()
            data.LoadXml(_ReportRdlContent)

            Dim queries As Xml.XmlNodeList = data.GetElementsByTagName("DataSets")

            If queries Is Nothing OrElse queries.Count < 1 OrElse _
                queries(0).ChildNodes.Count < 1 Then
                Throw New Exception(ActiveReports_UserReport_FailedToParseQueries)
            End If

            Dim result As New DataSet("ReportDataSet")

            For Each query As Xml.XmlNode In queries(0).ChildNodes
                result.Tables.Add(FetchDataTableForQuery(query))
            Next

            _ReportDataSetString = WriteDataSetToString(result)

        End Sub

        Private Function FetchDataTableForQuery(ByVal query As Xml.XmlNode) As DataTable

            Dim nameAttribute As Xml.XmlAttribute = query.Attributes("Name")
            If nameAttribute Is Nothing OrElse StringIsNullOrEmpty(nameAttribute.InnerText) Then
                Throw New Exception(ActiveReports_UserReport_FailedToParseQueryName)
            End If

            Dim queryString As String = ""

            For Each node As Xml.XmlNode In query.ChildNodes
                If node.Name.Trim.ToLower = "query" Then
                    For Each childNode As Xml.XmlNode In node.ChildNodes
                        If childNode.Name.Trim.ToLower = "commandtext" Then
                            queryString = childNode.InnerText
                            Exit For
                        End If
                    Next
                    Exit For
                End If
            Next

            If StringIsNullOrEmpty(queryString) Then
                Throw New Exception(ActiveReports_UserReport_FailedToParseQueryContent)
            End If

            Dim requiredParams As New List(Of KeyValuePair(Of String, Object))

            If Not _ReportParams Is Nothing Then

                Dim i As Integer = 0

                For Each param As KeyValuePair(Of String, Object) In _ReportParams

                    If queryString.Contains(String.Format("@{0}", param.Key)) Then

                        queryString = queryString.Replace(String.Format("@{0}", param.Key), _
                            ParamKeys(i))
                        requiredParams.Add(New KeyValuePair(Of String, Object) _
                            (ParamKeys(i), param.Value))
                        i += 1

                    End If

                Next

            End If

            Dim myComm As New SQLCommand("RawSQL", queryString)
            For Each param As KeyValuePair(Of String, Object) In requiredParams
                myComm.AddParam(param.Key, param.Value)
            Next

            Dim result As DataTable = myComm.Fetch()

            result.TableName = nameAttribute.InnerText

            Return result

        End Function

        Private Shared Function WriteDataSetToString(ByVal dataSetToSerialize As DataSet) As String

            ' this method does not adds datatables with 0 rows
            ' thus we need to create an empty row and mark it for deletion when deserializing

            For Each table As DataTable In dataSetToSerialize.Tables
                If table.Rows.Count < 1 AndAlso table.Columns.Count > 0 Then
                    table.Rows.Add()
                    table.Rows(0).Item(0) = EMPTY_TABLE_MARKER
                End If
            Next

            Dim result As String

            Dim objStream As New IO.MemoryStream()

            Try
                dataSetToSerialize.WriteXml(objStream)
                Dim objXmlWriter As New Xml.XmlTextWriter(objStream, Text.Encoding.UTF8)
                objStream = DirectCast(objXmlWriter.BaseStream, IO.MemoryStream)
                result = Text.Encoding.UTF8.GetString(objStream.ToArray())
            Catch ex As Exception
                objStream.Dispose()
                Throw
            End Try

            objStream.Dispose()

            Return result

        End Function

#End Region

    End Class

End Namespace