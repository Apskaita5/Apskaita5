Namespace HelperLists

    <Serializable()> _
    Public Class RegionalPriceInfo
        Inherits ReadOnlyBase(Of RegionalPriceInfo)
        Implements IComparable

#Region " Business Methods "

        Private _GUID As Guid = Guid.NewGuid
        Private _CurrencyCode As String = ""
        Private _ValuePerUnitSales As Double = 0
        Private _ValuePerUnitPurchases As Double = 0


        Public ReadOnly Property CurrencyCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrencyCode.Trim
            End Get
        End Property

        Public ReadOnly Property ValuePerUnitSales() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValuePerUnitSales, 4)
            End Get
        End Property

        Public ReadOnly Property ValuePerUnitPurchases() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValuePerUnitPurchases, 4)
            End Get
        End Property


        Public ReadOnly Property GetMe() As RegionalPriceInfo
            Get
                Return Me
            End Get
        End Property



        Public Shared Operator =(ByVal a As RegionalPriceInfo, ByVal b As RegionalPriceInfo) As Boolean
            If a Is Nothing AndAlso b Is Nothing Then Return True
            If a Is Nothing OrElse b Is Nothing Then Return False
            Return a._CurrencyCode.Trim.ToUpper = b._CurrencyCode.Trim.ToUpper
        End Operator

        Public Shared Operator <>(ByVal a As RegionalPriceInfo, ByVal b As RegionalPriceInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As RegionalPriceInfo, ByVal b As RegionalPriceInfo) As Boolean
            If a Is Nothing Then Return False
            If a IsNot Nothing And b Is Nothing Then Return True
            Return a._CurrencyCode.Trim.ToUpper > b._CurrencyCode.Trim.ToUpper
        End Operator

        Public Shared Operator <(ByVal a As RegionalPriceInfo, ByVal b As RegionalPriceInfo) As Boolean
            If a Is Nothing And b Is Nothing Then Return False
            If a Is Nothing Then Return True
            If b Is Nothing Then Return False
            Return a._CurrencyCode.Trim.ToUpper < b._CurrencyCode.Trim.ToUpper
        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer _
            Implements System.IComparable.CompareTo
            Dim tmp As RegionalPriceInfo = TryCast(obj, RegionalPriceInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function

        Protected Overrides Function GetIdValue() As Object
            Return _GUID
        End Function

        Public Overrides Function ToString() As String
            If String.IsNullOrEmpty(_CurrencyCode.Trim) Then Return ""
            Return _CurrencyCode & " = " & _ValuePerUnitSales.ToString & "/" _
                & _ValuePerUnitPurchases.ToString
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetRegionalPriceInfo(ByVal dr As String) As RegionalPriceInfo
            Return New RegionalPriceInfo(dr)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As String)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As String)

            Dim DataArray As String() = dr.Split(New String() {"*-*-"}, StringSplitOptions.RemoveEmptyEntries)

            _CurrencyCode = DataArray(1).Trim
            _ValuePerUnitSales = CRound(CLongSafe(DataArray(2).Trim, 0) / 10000, 4)
            _ValuePerUnitPurchases = CRound(CLongSafe(DataArray(3).Trim, 0) / 10000, 4)

        End Sub

#End Region

    End Class

End Namespace