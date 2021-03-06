﻿Imports ApskaitaObjects.Attributes

Namespace Documents

    ''' <summary>
    ''' Represents a description info for a particular regionalized object for a particular language.
    ''' </summary>
    ''' <remarks>Should be only used as a child of <see cref="RegionalContentList">RegionalContentList</see>.
    ''' Values are stored in the database table regionalcontents.</remarks>
    <Serializable()> _
    Public NotInheritable Class RegionalContent
        Inherits BusinessBase(Of RegionalContent)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _LanguageCode As String = LanguageCodeLith
        Private _LanguageName As String = GetLanguageName(LanguageCodeLith)
        Private _ContentInvoice As String = ""
        Private _MeasureUnit As String = ""
        Private _VatExempt As String = ""


        ''' <summary>
        ''' Gets an ID of the item that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a code of the language of the description info.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.LanguageCode.</remarks>
        <LanguageCodeField(ValueRequiredLevel.Mandatory, True)> _
        Public Property LanguageCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LanguageCode.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _LanguageCode.Trim <> value.Trim Then
                    _LanguageName = GetLanguageName(value.Trim, False)
                    If String.IsNullOrEmpty(_LanguageName.Trim) Then
                        _LanguageCode = ""
                    Else
                        _LanguageCode = value.Trim
                    End If
                    PropertyHasChanged()
                    PropertyHasChanged("LanguageName")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a language of the description info (as localized human readable string).
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.LanguageCode.</remarks>
        <LanguageNameField(ValueRequiredLevel.Mandatory, True)> _
        Public Property LanguageName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LanguageName.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _LanguageCode.Trim.ToUpper <> GetLanguageCode(value.Trim, False).Trim.ToUpper Then
                    _LanguageCode = GetLanguageCode(value.Trim, False)
                    _LanguageName = value.Trim
                    PropertyHasChanged()
                    PropertyHasChanged("LanguageCode")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a description of the object within an invoice.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.ContentInvoice.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
        Public Property ContentInvoice() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContentInvoice.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _ContentInvoice.Trim <> value.Trim Then
                    _ContentInvoice = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets an object's measure unit within an invoice.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.MeasureUnit.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 50)> _
        Public Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _MeasureUnit.Trim <> value.Trim Then
                    _MeasureUnit = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a description of the VAT exempt that is applicable to the object within an invoice.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.VatExempt.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255)> _
        Public Property VatExempt() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VatExempt.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _VatExempt.Trim <> value.Trim Then
                    _VatExempt = value.Trim
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


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Documents_RegionalContent_ToString, _
                _LanguageName, _ContentInvoice, _MeasureUnit)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.LanguageNameValidation, _
               New Csla.Validation.RuleArgs("LanguageName"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
               New Csla.Validation.RuleArgs("ContentInvoice"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
               New Csla.Validation.RuleArgs("MeasureUnit"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
               New Csla.Validation.RuleArgs("VatExempt"))

            ValidationRules.AddRule(AddressOf CommonValidation.StringValueUniqueInCollectionValidation, _
                New Csla.Validation.RuleArgs("LanguageName"))

        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function NewRegionalContent() As RegionalContent
            Dim result As New RegionalContent
            result.ValidationRules.CheckRules()
            Return result
        End Function

        Friend Shared Function GetRegionalContent(ByVal dr As DataRow) As RegionalContent
            Return New RegionalContent(dr)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal dr As DataRow)
            MarkAsChild()
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(1), 0)
            _LanguageCode = CStrSafe(dr.Item(2))
            _LanguageName = GetLanguageName(_LanguageCode, False)
            _ContentInvoice = CStrSafe(dr.Item(3))
            _MeasureUnit = CStrSafe(dr.Item(4))
            _VatExempt = CStrSafe(dr.Item(5))

            MarkOld()

        End Sub

        Friend Sub Insert(ByVal parent As IRegionalDataObject)

            Dim myComm As New SQLCommand("InsertRegionalContent")
            AddWithParams(myComm)
            myComm.AddParam("?AA", parent.RegionalObjectID)
            myComm.AddParam("?AB", Utilities.ConvertDatabaseID(parent.RegionalObjectType))

            myComm.Execute()

            _ID = Convert.ToInt32(myComm.LastInsertID)

            MarkOld()

        End Sub

        Friend Sub Update(ByVal parent As IRegionalDataObject)

            Dim myComm As New SQLCommand("UpdateRegionalContent")
            myComm.AddParam("?CD", _ID)
            AddWithParams(myComm)

            myComm.Execute()

            MarkOld()

        End Sub

        Friend Sub DeleteSelf()

            Dim myComm As New SQLCommand("DeleteRegionalContent")
            myComm.AddParam("?CD", _ID)

            myComm.Execute()

            MarkNew()

        End Sub


        Private Sub AddWithParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AC", _LanguageCode.Trim)
            myComm.AddParam("?AD", _ContentInvoice.Trim)
            myComm.AddParam("?AE", _MeasureUnit.Trim)
            myComm.AddParam("?AF", _VatExempt.Trim)

        End Sub

#End Region

    End Class

End Namespace