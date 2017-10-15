Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of a VAT declaration report. Contains information
    ''' about declaration field's values at the maximum detail level:
    ''' document (an invoice or an advance report) -> document item/line
    ''' -> declaration field.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="VatDeclarationItemList">VatDeclarationItemList</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class VatDeclarationItem
        Inherits ReadOnlyBase(Of VatDeclarationItem)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ID As Integer = 0
        Private _DocumentType As DocumentType = Nothing
        Private _Document As String = ""
        Private _Item As String = ""
        Private _ItemSum As Double = 0
        Private _ItemVatRate As Double = 0
        Private _ItemVatSum As Double = 0
        Private _FieldCode As String = ""
        Private _FieldSum As Double = 0


        ''' <summary>
        ''' Gets <see cref="General.JournalEntry.ID">an ID of the journal entry</see> 
        ''' that is created by the invoice or advance report.
        ''' </summary>
        ''' <remarks>Value is stored in the database field bz.Op_ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="ApskaitaObjects.DocumentType">DocumentType</see> 
        ''' of the document associated with the Journal Entry (as enum).
        ''' </summary>
        ''' <remarks>Value is stored in the database field bz.Op_dok_rusis.</remarks>
        Public ReadOnly Property DocumentType() As DocumentType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentType
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of the document (invoice or advance report).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Document() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Document.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of the document item (a line in an 
        ''' invoice or an advance report).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Item() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Item.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a sum (excluding VAT) in the document item (a line in an 
        ''' invoice or an advance report).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ItemSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ItemSum)
            End Get
        End Property

        ''' <summary>
        ''' Gets a VAT rate in the document item (a line in an 
        ''' invoice or an advance report).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property ItemVatRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ItemVatRate)
            End Get
        End Property

        ''' <summary>
        ''' Gets a VAT sum in the document item (a line in an 
        ''' invoice or an advance report).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ItemVatSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ItemVatSum)
            End Get
        End Property

        ''' <summary>
        ''' Gets a code of the VAT declaration field that the document 
        ''' (VAT) sum is added (or subtracted) to.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property FieldCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FieldCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a sum that is added (or subtracted) to the VAT declaration field.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property FieldSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_FieldSum)
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_VatDeclarationItem_ToString, _
                _Document, _Item, _FieldCode, DblParser(_FieldSum))
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetVatDeclarationItem(ByVal dr As DataRow) As VatDeclarationItem
            Return New VatDeclarationItem(dr)
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
            _DocumentType = ConvertDatabaseCharID(Of ApskaitaObjects.DocumentType) _
                (CStrSafe(dr.Item(4)).Trim)
            _Document = String.Format(My.Resources.ActiveReports_VatDeclarationItem_DocumentTemplate, _
                CDateSafe(dr.Item(1), Date.MinValue).ToString("yyyy-MM-dd"), _
                ConvertLocalizedName(_DocumentType), CStrSafe(dr.Item(2)), _ID.ToString())
            _Item = CStrSafe(dr.Item(5)).Trim
            _ItemSum = CDblSafe(dr.Item(6), 2, 0)
            _ItemVatRate = CDblSafe(dr.Item(7), 2, 0)
            _ItemVatSum = CDblSafe(dr.Item(8), 2, 0)
            _FieldCode = CStrSafe(dr.Item(9)).Trim.ToUpper()
            _FieldSum = CDblSafe(dr.Item(10), 2, 0)

        End Sub

#End Region

    End Class

End Namespace