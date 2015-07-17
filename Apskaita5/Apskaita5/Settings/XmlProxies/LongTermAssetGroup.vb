Namespace Settings.XmlProxies

    <Serializable()> _
    Public Class LongTermAssetGroup

        Private _Name As String = ""
        Private _MaxAmortizationPeriod As Integer = 3
        Private _ValidFrom As Date = Date.MinValue
        Private _ValidTo As Date = Date.MaxValue
        Private _IsObsolete As Boolean = False


        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _Name.Trim <> value.Trim Then
                    _Name = value.Trim
                End If
            End Set
        End Property

        Public Property MaxAmortizationPeriod() As Integer
            Get
                Return _MaxAmortizationPeriod
            End Get
            Set(ByVal value As Integer)
                If _MaxAmortizationPeriod <> value Then
                    _MaxAmortizationPeriod = value
                End If
            End Set
        End Property

        Public Property ValidFrom() As Date
            Get
                Return _ValidFrom
            End Get
            Set(ByVal value As Date)
                If value.Date <> _ValidFrom.Date Then
                    _ValidFrom = value.Date
                End If
            End Set
        End Property

        Public Property ValidTo() As Date
            Get
                Return _ValidTo
            End Get
            Set(ByVal value As Date)
                If value.Date <> _ValidTo.Date Then
                    _ValidTo = value.Date
                End If
            End Set
        End Property

        Public Property IsObsolete() As Boolean
            Get
                Return _IsObsolete
            End Get
            Set(ByVal value As Boolean)
                If _IsObsolete <> value Then
                    _IsObsolete = value
                End If
            End Set
        End Property

    End Class

End Namespace