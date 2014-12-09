Namespace HelperLists

    <Serializable()> _
    Public Class ProductionCalculationInfo
        Inherits ReadOnlyBase(Of ProductionCalculationInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Date As Date = Date.MinValue
        Private _IsObsolete As Boolean = False
        Private _Description As String = ""
        Private _GoodsID As Integer = 0
        Private _GoodsName As String = ""


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property [Date]() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _Date = Date.MinValue Then Return ""
                Return _Date.ToString("yyyy-MM-dd")
            End Get
        End Property

        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        Public ReadOnly Property Description() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Description.Trim
            End Get
        End Property

        Public ReadOnly Property GoodsID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsID
            End Get
        End Property

        Public ReadOnly Property GoodsName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsName.Trim
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Date.ToString("yyyy-MM-dd") & " " & _GoodsName & " -> " & _Description
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewProductionCalculationInfo() As ProductionCalculationInfo
            Return New ProductionCalculationInfo()
        End Function

        Friend Shared Function GetProductionCalculationInfo(ByVal dr As DataRow) As ProductionCalculationInfo
            Return New ProductionCalculationInfo(dr)
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

            _ID = CIntSafe(dr.item(0), 0)
            _Date = CDateSafe(dr.Item(1), Date.MinValue)
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(2), 0))
            _Description = CStrSafe(dr.Item(3)).Trim
            _GoodsID = CIntSafe(dr.Item(4), 0)
            _GoodsName = CStrSafe(dr.Item(5)).Trim

        End Sub

#End Region

    End Class

End Namespace