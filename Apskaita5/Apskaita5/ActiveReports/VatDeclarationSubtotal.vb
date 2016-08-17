Namespace ActiveReports

    ''' <summary>
    ''' Represents a subtotal item of a VAT declaration report. 
    ''' Contains information about a declaration field's subtotal value
    ''' for a declaration field.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="VatDeclarationSubtotalList">VatDeclarationSubtotalList</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class VatDeclarationSubtotal
        Inherits ReadOnlyBase(Of VatDeclarationSubtotal)

#Region " Business Methods "

        Private _Code As String = ""
        Private _Sum As Double = 0


        ''' <summary>
        ''' Gets a code of the VAT declaration field.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Code() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Code.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a total sum in the VAT declaration field.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property Sum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Sum)
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _Code
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_VatDeclarationSubtotal_ToString, _
                _Code, DblParser(_Sum))
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetVatDeclarationSubtotal(ByVal item As VatDeclarationItem) As VatDeclarationSubtotal
            Return New VatDeclarationSubtotal(item)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal item As VatDeclarationItem)
            Fetch(item)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal item As VatDeclarationItem)

            _Code = item.FieldCode
            _Sum = item.FieldSum

        End Sub

        Friend Sub AddVatDeclarationSubtotal(ByVal item As VatDeclarationItem)
            _Sum = CRound(_Sum + item.FieldSum, 2)
        End Sub

#End Region

    End Class

End Namespace