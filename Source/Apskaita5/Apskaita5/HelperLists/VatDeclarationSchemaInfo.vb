﻿Imports ApskaitaObjects.Documents

Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="Documents.VatDeclarationSchema">VAT declaration schema</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table VatDeclarationSchemas.</remarks>
    <Serializable()> _
    Public NotInheritable Class VatDeclarationSchemaInfo
        Inherits ReadOnlyBase(Of VatDeclarationSchemaInfo)
        Implements IComparable, IValueObject

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _Description As String = ""
        Private _VatRate As Double = 0.0
        Private _IsObsolete As Boolean = False
        Private _TradedType As TradedItemType = Nothing
        Private _TradedTypeHumanReadable As String = ""
        Private _ExternalCode As String = ""
        Private _TaxCode As String = ""
        Private _VatIsVirtual As Boolean = False


        ''' <summary>
        ''' Gets whether an object is a place holder (does not represent a real VAT declaration schema).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObject.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ID > 0
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the VAT declaration schema that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the VAT declaration schema (as used in dropboxes).
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.Name.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of the VAT declaration schema.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.Description.</remarks>
        Public ReadOnly Property Description() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Description.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a VAT rate for the declaration schema.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.VatRate.</remarks>
        <DoubleField(ValueRequiredLevel.Recommended, False, 2)> _
        Public ReadOnly Property VatRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_VatRate)
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating whether the VAT is virtual (indirect).
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.VatIsVirtual.</remarks>
        Public ReadOnly Property VatIsVirtual() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VatIsVirtual
            End Get
        End Property

        ''' <summary>
        ''' Gets a code of the VAT declaration schema as defined by the tax inspectorate.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.ExternalCode.</remarks>
        <CodeField(ValueRequiredLevel.Mandatory, ApskaitaObjects.Settings.CodeType.VmiVatType)> _
        Public ReadOnly Property TaxCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TaxCode.Trim
            End Get
        End Property


        ''' <summary>
        ''' Gets whether the declaration schema is obsolete, no longer in use.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.IsObsolete.</remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        ''' <summary>
        ''' Gets how the daclaration schema is used in trade operations (sale, purchase, etc.).
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.TradedType.</remarks>
        Public ReadOnly Property TradedType() As TradedItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradedType
            End Get
        End Property

        ''' <summary>
        ''' Gets how the daclaration schema is used in trade operations (sale, purchase, etc.)
        ''' as a human readable string.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.TradedType.</remarks>
        Public ReadOnly Property TradedTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradedTypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a code of the VAT declaration schema that is used for integration with external systems.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.ExternalCode.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255)> _
        Public ReadOnly Property ExternalCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ExternalCode.Trim
            End Get
        End Property


        Public Shared Operator =(ByVal a As VatDeclarationSchemaInfo, ByVal b As VatDeclarationSchemaInfo) As Boolean

            Dim aId, bId As Integer
            If a Is Nothing OrElse a.IsEmpty Then
                aId = 0
            Else
                aId = a.ID
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bId = 0
            Else
                bId = b.ID
            End If

            Return aId = bId

        End Operator

        Public Shared Operator <>(ByVal a As VatDeclarationSchemaInfo, ByVal b As VatDeclarationSchemaInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As VatDeclarationSchemaInfo, ByVal b As VatDeclarationSchemaInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString > bToString

        End Operator

        Public Shared Operator <(ByVal a As VatDeclarationSchemaInfo, ByVal b As VatDeclarationSchemaInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString < bToString

        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim tmp As VatDeclarationSchemaInfo = TryCast(obj, VatDeclarationSchemaInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Friend Function GetValueObjectIdString() As String
            If Me.IsEmpty Then Return ""
            Return _ID.ToString(Globalization.CultureInfo.InvariantCulture)
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return String.Format(My.Resources.HelperLists_VatDeclarationSchemaInfo_ToString, _
                DblParser(_VatRate, 2), _Name)
        End Function

#End Region

#Region " Factory Methods "

        Private Shared _Empty As VatDeclarationSchemaInfo = Nothing

        ''' <summary>
        ''' Gets an empty VatDeclarationSchemaInfo (placeholder).
        ''' </summary>
        Public Shared Function Empty() As VatDeclarationSchemaInfo
            If _Empty Is Nothing Then
                _Empty = New VatDeclarationSchemaInfo
            End If
            Return _Empty
        End Function

        Friend Shared Function GetVatDeclarationSchemaInfo(ByVal dr As DataRow, ByVal offset As Integer) As VatDeclarationSchemaInfo
            Return New VatDeclarationSchemaInfo(dr, offset)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal offset As Integer)
            Fetch(dr, offset)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal offset As Integer)

            _ID = CIntSafe(dr.Item(0 + offset), 0)
            _Name = CStrSafe(dr.Item(1 + offset)).Trim
            _Description = CStrSafe(dr.Item(2 + offset)).Trim
            _VatRate = CDblSafe(dr.Item(3 + offset), 2, 0)
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(4 + offset), 0))
            _TradedType = ConvertDatabaseID(Of TradedItemType)(CIntSafe(dr.Item(5 + offset), 0))
            _TradedTypeHumanReadable = ConvertLocalizedName(_TradedType)
            _ExternalCode = CStrSafe(dr.Item(6 + offset)).Trim
            _TaxCode = CStrSafe(dr.Item(7 + offset)).Trim
            _VatIsVirtual = ConvertDbBoolean(CIntSafe(dr.Item(8 + offset), 0))

        End Sub

#End Region

    End Class

End Namespace