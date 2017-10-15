Imports Csla.Validation

Namespace Documents

    ''' <summary>
    ''' Represents an invoice item (VAT) sum maping to a VAT declaration field,
    ''' defines a declaration field that the invoice (VAT) sum should be added to.
    ''' </summary>
    ''' <remarks>Values are persisted in the database table VatDeclarationEntrys.
    ''' Should only be used as a child of <see cref="VatDeclarationEntryList">VatDeclarationEntryList</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class VatDeclarationEntry
        Inherits BusinessBase(Of VatDeclarationEntry)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ID As Integer = 0
        Private _FieldCode As String = ""
        Private _InclusionPercentage As Double = 100
        Private _MinusValue As Boolean = False
        Private _IsVatField As Boolean = False
        Private _Remarks As String = ""


        ''' <summary>
        ''' Gets an ID of the item that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationEntrys.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a code of the VAT declaration field that the invoice (VAT) sum should be added to.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationEntrys.FieldCode.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
        Public Property FieldCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FieldCode.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _FieldCode.Trim <> value.Trim Then
                    _FieldCode = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets how an invoice item/line value is displayed in a VAT declaration field.
        ''' </summary>
        ''' <remarks>A proxy complex property to the <see cref="MinusValue">MinusValue</see>
        ''' and <see cref="IsVatField">IsVatField</see> values.</remarks>
        Public Property [Type]() As VatDeclarationEntryType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _IsVatField Then
                    If _MinusValue Then
                        Return VatDeclarationEntryType.SubtractVat
                    Else
                        Return VatDeclarationEntryType.AddVat
                    End If
                Else
                    If _MinusValue Then
                        Return VatDeclarationEntryType.SubtractPrice
                    Else
                        Return VatDeclarationEntryType.AddPrice
                    End If
                End If
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As VatDeclarationEntryType)
                CanWriteProperty(True)
                If value <> Me.Type Then
                    _MinusValue = (value = VatDeclarationEntryType.SubtractPrice OrElse _
                        value = VatDeclarationEntryType.SubtractVat)
                    _IsVatField = (value = VatDeclarationEntryType.AddVat OrElse _
                        value = VatDeclarationEntryType.SubtractVat)
                    PropertyHasChanged()
                    PropertyHasChanged("MinusValue")
                    PropertyHasChanged("IsVatField")
                    PropertyHasChanged("TypeHumanReadable")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets how an invoice item/line value is displayed in a VAT declaration field 
        ''' as a human readable string.
        ''' </summary>
        ''' <remarks>A proxy complex property to the <see cref="MinusValue">MinusValue</see>
        ''' and <see cref="IsVatField">IsVatField</see> values.</remarks>
        <LocalizedEnumField(GetType(VatDeclarationEntryType), False, "")> _
        Public Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return ConvertLocalizedName(Me.Type)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                Dim enumValue As VatDeclarationEntryType = VatDeclarationEntryType.AddPrice
                If Not StringIsNullOrEmpty(value) Then
                    enumValue = ConvertLocalizedName(Of VatDeclarationEntryType)(value)
                End If
                If enumValue <> Me.Type Then
                    Me.Type = enumValue
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a persent of the invoice (VAT) sum that should be added to the declaration field.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationEntrys.InclusionPercentage.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2, True, 0.1, 100, False)> _
        Public Property InclusionPercentage() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InclusionPercentage)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_InclusionPercentage) <> CRound(value) Then
                    _InclusionPercentage = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether to add the invoice VAT sum (as oposed to the invoice sum).
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationEntrys.IsVatField.</remarks>
        Public Property IsVatField() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsVatField
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _IsVatField <> value Then
                    _IsVatField = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether to subtract the invoice (VAT) sum from the declaration field instead of adding.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationEntrys.MinusValue.</remarks>
        Public Property MinusValue() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MinusValue
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _MinusValue <> value Then
                    _MinusValue = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets user remarks regarding the item.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationEntrys.Remarks.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255)> _
        Public Property Remarks() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Remarks.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Remarks.Trim <> value.Trim Then
                    _Remarks = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property



        Public Function GetErrorString() As String _
             Implements IGetErrorForListItem.GetErrorString
            If IsValid Then Return ""
            Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error))
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If BrokenRulesCollection.WarningCount < 1 Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning))
        End Function


        ''' <summary>
        ''' Gets an XML proxy for the VatDeclarationEntry instance.
        ''' </summary>
        ''' <remarks>Used for serializing data to/from XML string.</remarks>
        Friend Function GetXmlProxy() As VatDeclarationEntryProxy

            Dim result As New VatDeclarationEntryProxy

            result.FieldCode = _FieldCode
            result.InclusionPercentage = _InclusionPercentage
            result.IsVatField = _IsVatField
            result.MinusValue = _MinusValue
            result.Remarks = _Remarks

            Return result

        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return _FieldCode
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
                New RuleArgs("FieldCode"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
                New RuleArgs("FieldCode"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New RuleArgs("Remarks"))

        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function NewVatDeclarationEntry() As VatDeclarationEntry
            Dim result As New VatDeclarationEntry
            result.ValidationRules.CheckRules()
            Return result
        End Function

        Friend Shared Function NewVatDeclarationEntry(ByVal xmlProxy As VatDeclarationEntryProxy) As VatDeclarationEntry
            Return New VatDeclarationEntry(xmlProxy)
        End Function

        Friend Shared Function GetVatDeclarationEntry(ByVal dr As DataRow) As VatDeclarationEntry
            Return New VatDeclarationEntry(dr)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal xmlProxy As VatDeclarationEntryProxy)
            MarkAsChild()
            Create(xmlProxy)
        End Sub

        Private Sub New(ByVal dr As DataRow)
            MarkAsChild()
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal xmlProxy As VatDeclarationEntryProxy)

            If xmlProxy Is Nothing Then Throw New ArgumentNullException("xmlProxy")

            _FieldCode = xmlProxy.FieldCode
            _InclusionPercentage = xmlProxy.InclusionPercentage
            _IsVatField = xmlProxy.IsVatField
            _MinusValue = xmlProxy.MinusValue
            _Remarks = xmlProxy.Remarks

            ValidationRules.CheckRules()

        End Sub

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(0), 0)
            _FieldCode = CStrSafe(dr.Item(1)).Trim
            _InclusionPercentage = CDblSafe(dr.Item(2), 2, 0)
            _IsVatField = ConvertDbBoolean(CIntSafe(dr.Item(3), 0))
            _MinusValue = ConvertDbBoolean(CIntSafe(dr.Item(4), 0))
            _Remarks = CStrSafe(dr.Item(5)).Trim

            MarkOld()

        End Sub

        Friend Sub Insert(ByVal parent As VatDeclarationSchema)

            Dim myComm As New SQLCommand("InsertVatDeclarationEntry")
            AddWithParams(myComm)
            myComm.AddParam("?CD", parent.ID)

            myComm.Execute()

            _ID = Convert.ToInt32(myComm.LastInsertID)

            MarkOld()

        End Sub

        Friend Sub Update(ByVal parent As VatDeclarationSchema)

            Dim myComm As New SQLCommand("UpdateVatDeclarationEntry")
            myComm.AddParam("?CD", _ID)
            AddWithParams(myComm)

            myComm.Execute()

            MarkOld()

        End Sub

        Friend Sub DeleteSelf()

            Dim myComm As New SQLCommand("DeleteVatDeclarationEntry")
            myComm.AddParam("?CD", _ID)

            myComm.Execute()

            MarkNew()

        End Sub

        Private Sub AddWithParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AA", _FieldCode.Trim)
            myComm.AddParam("?AB", CRound(_InclusionPercentage))
            myComm.AddParam("?AC", ConvertDbBoolean(_IsVatField))
            myComm.AddParam("?AD", _Remarks.Trim)
            myComm.AddParam("?AE", ConvertDbBoolean(_MinusValue))

        End Sub

#End Region

    End Class

End Namespace