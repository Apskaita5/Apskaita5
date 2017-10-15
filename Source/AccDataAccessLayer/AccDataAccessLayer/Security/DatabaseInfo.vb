Namespace Security

    <Serializable()> _
    Public Class DatabaseInfo
        Inherits Csla.ReadOnlyBase(Of DatabaseInfo)

        Private _Id As String = ""
        ''' <summary>
        ''' Gets a official registration code of the company.
        ''' </summary>
        Public ReadOnly Property Id() As String
            Get
                Return _Id
            End Get
        End Property

        Private _DatabaseName As String = ""
        ''' <summary>
        ''' Gets a name of the database, where the company's data is stored (DatabaseNameConvention%).
        ''' </summary>
        Public ReadOnly Property DatabaseName() As String
            Get
                Return _DatabaseName
            End Get
        End Property

        Private _CompanyName As String = ""
        ''' <summary>
        ''' Gets a human readable name of the company.
        ''' </summary>
        Public ReadOnly Property CompanyName() As String
            Get
                Return _CompanyName
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return _DatabaseName.Trim
        End Function

        Public Overrides Function ToString() As String
            If String.IsNullOrEmpty(_CompanyName.Trim) Then Return _DatabaseName
            Return _CompanyName
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Friend Sub New(ByVal nDatabaseName As String, ByRef nCompanyName As String, _
            ByRef nCompanyID As String)
            _DatabaseName = nDatabaseName
            _CompanyName = nCompanyName
            _Id = nCompanyID
        End Sub

    End Class

End Namespace