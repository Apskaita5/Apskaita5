Namespace HelperLists

    <Serializable()> _
    Public Class DocumentSerialInfo
        Inherits ReadOnlyBase(Of DocumentSerialInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Serial As String = ""
        Private _DocumentType As Settings.DocumentSerialType = Settings.DocumentSerialType.Invoice


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Serial() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Serial.Trim
            End Get
        End Property

        Public ReadOnly Property DocumentType() As Settings.DocumentSerialType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentType
            End Get
        End Property

        Public ReadOnly Property DocumentTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return ConvertEnumHumanReadable(_DocumentType)
            End Get
        End Property

        Public ReadOnly Property GetMe() As DocumentSerialInfo
            Get
                Return Me
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Serial
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetDocumentSerialInfo(ByVal dr As DataRow) As DocumentSerialInfo
            Return New DocumentSerialInfo(dr)
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

            _ID = CIntSafe(dr.Item(0), 0)
            _DocumentType = ConvertEnumDatabaseStringCode(Of Settings.DocumentSerialType) _
                (CStrSafe(dr.Item(1)))
            _Serial = CStrSafe(dr.Item(2)).Trim

        End Sub

#End Region

    End Class

End Namespace