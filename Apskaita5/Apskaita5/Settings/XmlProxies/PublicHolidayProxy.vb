Namespace Settings.XmlProxies

    <Serializable()> _
    Public Class PublicHolidayProxy

        Private _PublicHolidayDate As Date = Today

        Public Property PublicHolidayDate() As Date
            Get
                Return _PublicHolidayDate
            End Get
            Set(ByVal value As Date)
                If _PublicHolidayDate.Date <> value.Date Then
                    _PublicHolidayDate = value
                End If
            End Set
        End Property

    End Class

End Namespace