﻿Namespace Settings.XmlProxies

    <Serializable()> _
    Public Class TaxRate

        Private _TaxType As TaxTarifType = TaxTarifType.Vat
        Private _TaxRate As Double = 0
        Private _IsObsolete As Boolean = False

        Public Property TaxType() As TaxTarifType
            Get
                Return _TaxType
            End Get
            Set(ByVal value As TaxTarifType)
                If _TaxType <> value Then
                    _TaxType = value
                End If
            End Set
        End Property

        Public Property TaxRate() As Double
            Get
                Return _TaxRate
            End Get
            Set(ByVal value As Double)
                If CRound(_TaxRate) <> CRound(value) Then
                    _TaxRate = CRound(value)
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