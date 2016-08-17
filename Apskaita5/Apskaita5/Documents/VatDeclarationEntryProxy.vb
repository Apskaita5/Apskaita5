Namespace Documents

    ''' <summary>
    ''' Represents a <see cref="VatDeclarationEntry">VatDeclarationEntry</see> XML proxy, 
    ''' used to XML serialize VatDeclarationEntry data.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class VatDeclarationEntryProxy

        Private _FieldCode As String = ""
        Private _InclusionPercentage As Double = 100
        Private _MinusValue As Boolean = False
        Private _IsVatField As Boolean = False
        Private _Remarks As String = ""


        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationEntry.FieldCode">VatDeclarationEntry.FieldCode</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property FieldCode() As String
            Get
                Return _FieldCode.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _FieldCode.Trim <> value.Trim Then
                    _FieldCode = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationEntry.InclusionPercentage">VatDeclarationEntry.InclusionPercentage</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property InclusionPercentage() As Double
            Get
                Return CRound(_InclusionPercentage)
            End Get
            Set(ByVal value As Double)
                If CRound(_InclusionPercentage) <> CRound(value) Then
                    _InclusionPercentage = CRound(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationEntry.IsVatField">VatDeclarationEntry.IsVatField</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property IsVatField() As Boolean
            Get
                Return _IsVatField
            End Get
            Set(ByVal value As Boolean)
                If _IsVatField <> value Then
                    _IsVatField = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationEntry.MinusValue">VatDeclarationEntry.MinusValue</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property MinusValue() As Boolean
            Get
                Return _MinusValue
            End Get
            Set(ByVal value As Boolean)
                If _MinusValue <> value Then
                    _MinusValue = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationEntry.Remarks">VatDeclarationEntry.Remarks</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property Remarks() As String
            Get
                Return _Remarks.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _Remarks.Trim <> value.Trim Then
                    _Remarks = value.Trim
                End If
            End Set
        End Property

    End Class

End Namespace