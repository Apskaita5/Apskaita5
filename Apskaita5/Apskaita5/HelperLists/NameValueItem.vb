Namespace HelperLists

    <Serializable()> _
    Public Class NameValueItem
        Inherits ReadOnlyBase(Of NameValueItem)

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _Name As String = ""
        Private _Value As String = ""


        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        Public ReadOnly Property Value() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Value.Trim
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return _Value & " " & _Name
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetNameValueItem(ByVal s As String) As NameValueItem
            Return New NameValueItem(s)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal s As String)
            Fetch(s)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal s As String)

            _Name = GetElement(s, 1)
            _Value = GetElement(s, 0)

        End Sub

#End Region

    End Class

End Namespace