Namespace HelperLists

    <Serializable()> _
    Public Class AccountInfo
        Inherits ReadOnlyBase(Of AccountInfo)

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _ID As Long = 0
        Private _Name As String = ""
        Private _AssociatedReportItem As String = ""
        Private _Class As Byte = 0


        Public ReadOnly Property ID() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        Public ReadOnly Property FullName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID.ToString & " " & _Name.Trim
            End Get
        End Property

        Public ReadOnly Property AssociatedReportItem() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssociatedReportItem.Trim
            End Get
        End Property

        Public ReadOnly Property [Class]() As Byte
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Class
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _ID.ToString & " " & _Name
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetAccountInfo(ByVal dr As DataRow) As AccountInfo
            Return New AccountInfo(dr)
        End Function

        Friend Shared Function GetEmptyAccountInfo() As AccountInfo
            Return New AccountInfo()
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

            _ID = CLongSafe(dr.Item(0), 0)
            _Name = CStrSafe(dr.Item(1)).Trim
            _AssociatedReportItem = CStrSafe(dr.Item(2)).Trim
            _Class = General.Account.GetAccountClass(_ID)

        End Sub

#End Region

    End Class

End Namespace