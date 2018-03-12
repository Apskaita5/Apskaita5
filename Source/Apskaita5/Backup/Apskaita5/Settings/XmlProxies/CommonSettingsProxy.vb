Namespace Settings.XmlProxies

    <Serializable()> _
    Public Class CommonSettingsProxy

        Private _CodeWageGPM As String = ""
        Private _Codes As List(Of CodeProxy) = Nothing
        Private _DefaultWorkTimes As List(Of DefaultWorkTimeProxy) = Nothing
        Private _PublicHolidays As List(Of PublicHolidayProxy) = Nothing
        Private _TaxRates As List(Of TaxRateProxy) = Nothing
        Private _Names As List(Of NameProxy) = Nothing


        Public Property CodeWageGPM() As String
            Get
                Return _CodeWageGPM
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _CodeWageGPM.Trim.ToLower <> value.Trim.ToLower Then
                    _CodeWageGPM = value.Trim
                End If
            End Set
        End Property

        Public Property Codes() As List(Of CodeProxy)
            Get
                If _Codes Is Nothing Then
                    _Codes = New List(Of CodeProxy)
                End If
                Return _Codes
            End Get
            Set(ByVal value As List(Of CodeProxy))
                _Codes = value
            End Set
        End Property

        Public Property DefaultWorkTimes() As List(Of DefaultWorkTimeProxy)
            Get
                If _DefaultWorkTimes Is Nothing Then
                    _DefaultWorkTimes = New List(Of DefaultWorkTimeProxy)
                End If
                Return _DefaultWorkTimes
            End Get
            Set(ByVal value As List(Of DefaultWorkTimeProxy))
                _DefaultWorkTimes = value
            End Set
        End Property

        Public Property PublicHolidays() As List(Of PublicHolidayProxy)
            Get
                If _PublicHolidays Is Nothing Then
                    _PublicHolidays = New List(Of PublicHolidayProxy)
                End If
                Return _PublicHolidays
            End Get
            Set(ByVal value As List(Of PublicHolidayProxy))
                _PublicHolidays = value
            End Set
        End Property

        Public Property TaxRates() As List(Of TaxRateProxy)
            Get
                If _TaxRates Is Nothing Then
                    _TaxRates = New List(Of TaxRateProxy)
                End If
                Return _TaxRates
            End Get
            Set(ByVal value As List(Of TaxRateProxy))
                _TaxRates = value
            End Set
        End Property

        Public Property Names() As List(Of NameProxy)
            Get
                If _Names Is Nothing Then
                    _Names = New List(Of NameProxy)
                End If
                Return _Names
            End Get
            Set(ByVal value As List(Of NameProxy))
                _Names = value
            End Set
        End Property

    End Class

End Namespace