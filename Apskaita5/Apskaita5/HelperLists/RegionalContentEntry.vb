Imports ApskaitaObjects.Documents

Namespace HelperLists

    ''' <summary>
    ''' Represents value object containing a description info for a particular localized object for a particular language.
    ''' </summary>
    ''' <remarks>Should be only used as a child of <see cref="RegionalContentEntryList">RegionalContentEntryList</see>.
    ''' Used with <see cref="IRegionalDataObject">localized objects</see> in order to provide localization in runtime.
    ''' Values are stored in the database table regionalcontents.</remarks>
    <Serializable()> _
    Public Class RegionalContentEntry
        Inherits ReadOnlyBase(Of RegionalContentEntry)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ObjectType As RegionalizedObjectType = RegionalizedObjectType.Service
        Private _ObjectID As Integer = 0
        Private _LanguageCode As String = ""
        Private _ContentInvoice As String = ""
        Private _MeasureUnit As String = ""
        Private _VatExempt As String = ""


        ''' <summary>
        ''' Type of the regionalized object for which the description info is provided.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.ParentType.</remarks>
        Public ReadOnly Property ObjectType() As RegionalizedObjectType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ObjectType
            End Get
        End Property

        ''' <summary>
        ''' ID of the object for which the description info is provided.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.ParentID.</remarks>
        Public ReadOnly Property ObjectID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ObjectID
            End Get
        End Property

        ''' <summary>
        ''' Language of the description info.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.LanguageCode.</remarks>
        Public ReadOnly Property LanguageCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LanguageCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Description of the object within an invoice.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.ContentInvoice.</remarks>
        Public ReadOnly Property ContentInvoice() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContentInvoice.Trim
            End Get
        End Property

        ''' <summary>
        ''' Description of the object measure unit within an invoice.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.MeasureUnit.</remarks>
        Public ReadOnly Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit.Trim
            End Get
        End Property

        ''' <summary>
        ''' Description of the VAT exempt that is applicable to the object within an invoice.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.VatExempt.</remarks>
        Public ReadOnly Property VatExempt() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VatExempt.Trim
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("{0}: {1}, {2}, {3}.", _LanguageCode, _ContentInvoice, _MeasureUnit, _VatExempt)
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets an empty (default) RegionalContentEntry.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function NewRegionalContentEntry() As RegionalContentEntry
            Return New RegionalContentEntry()
        End Function

        ''' <summary>
        ''' Gets a RegionalContentEntry by a database query.
        ''' </summary>
        ''' <param name="dr">Database query result.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetRegionalContentEntry(ByVal dr As DataRow) As RegionalContentEntry
            Return New RegionalContentEntry(dr)
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

            _ObjectID = CIntSafe(dr.Item(0), 0)
            _ObjectType = EnumValueAttribute.ConvertDatabaseID(Of RegionalizedObjectType) _
                (CIntSafe(dr.Item(1), 0))
            _LanguageCode = CStrSafe(dr.Item(2)).Trim
            _ContentInvoice = CStrSafe(dr.Item(3)).Trim
            _MeasureUnit = CStrSafe(dr.Item(4)).Trim
            _VatExempt = CStrSafe(dr.Item(5)).Trim

        End Sub

#End Region

    End Class

End Namespace