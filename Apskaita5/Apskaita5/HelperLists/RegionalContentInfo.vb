Namespace HelperLists

    <Serializable()> _
    Public Class RegionalContentInfo
        Inherits ReadOnlyBase(Of RegionalContentInfo)

#Region " Business Methods "

        Private _GUID As Guid = Guid.NewGuid
        Private _LanguageCode As String = ""
        Private _ContentInvoice As String = ""
        Private _MeasureUnit As String = ""
        Private _VatExempt As String = ""


        Public ReadOnly Property LanguageCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LanguageCode.Trim
            End Get
        End Property

        Public ReadOnly Property ContentInvoice() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContentInvoice.Trim
            End Get
        End Property

        Public ReadOnly Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit.Trim
            End Get
        End Property

        Public ReadOnly Property VatExempt() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VatExempt.Trim
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _GUID
        End Function

        Public Overrides Function ToString() As String
            If String.IsNullOrEmpty(_LanguageCode.Trim) Then Return ""
            Return _LanguageCode & " -> " & _ContentInvoice
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetRegionalContentInfo(ByVal dr As DataRow) As RegionalContentInfo
            Return New RegionalContentInfo(dr)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _LanguageCode = CStrSafe(dr.Item(2))
            _ContentInvoice = CStrSafe(dr.Item(3))
            _MeasureUnit = CStrSafe(dr.Item(4))
            _VatExempt = CStrSafe(dr.Item(5))

        End Sub

#End Region

    End Class

End Namespace