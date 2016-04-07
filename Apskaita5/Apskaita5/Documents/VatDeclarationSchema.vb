Imports ApskaitaObjects.My.Resources
Imports Csla.Validation

Namespace Documents

    ''' <summary>
    ''' Represents a VAT declaration schema. Defines how an invoice items' (VAT) sums are
    ''' maped to a VAT declaration.
    ''' </summary>
    ''' <remarks>Values are stored in the database table VatDeclarationSchemas.</remarks>
    <Serializable()> _
    Public Class VatDeclarationSchema
        Inherits BusinessBase(Of VatDeclarationSchema)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _Name As String = ""
        Private _Description As String = ""
        Private _VatRate As Double = 0
        Private _IsObsolete As Boolean = False
        Private _TradedType As TradedItemType = TradedItemType.All
        Private _TradedTypeHumanReadable As String = ConvertLocalizedName(TradedItemType.All)
        Private _ExternalCode As String = ""
        Private _DeclarationEntries As VatDeclarationEntryList


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
        ''' Gets the date and time when the schema was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.InsertDate.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the schema was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.UpdateDate.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a name of the VAT declaration schema (as used in dropboxes).
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.Name.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
        Public Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Name.Trim <> value.Trim Then
                    _Name = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a description of the VAT declaration schema.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.Description.</remarks>
        <StringField(ValueRequiredLevel.Optional, 1000)> _
        Public Property Description() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Description.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Description.Trim <> value.Trim Then
                    _Description = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a VAT rate for the declaration schema.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.VatRate.</remarks>
        <DoubleField(ValueRequiredLevel.Recommended, False, 2, True, 0, 99)> _
        Public Property VatRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_VatRate)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_VatRate) <> CRound(value) Then
                    _VatRate = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether the declaration schema is obsolete, no longer in use.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.IsObsolete.</remarks>
        Public Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _IsObsolete <> value Then
                    _IsObsolete = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets how the daclaration schema is used in trade operations (sale, purchase, etc.).
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.TradedType.</remarks>
        Public Property TradedType() As TradedItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradedType
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As TradedItemType)
                CanWriteProperty(True)
                If _TradedType <> value Then
                    _TradedType = value
                    PropertyHasChanged()
                    _TradedTypeHumanReadable = ConvertLocalizedName(value)
                    PropertyHasChanged("TradedTypeHumanReadable")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets how the daclaration schema is used in trade operations (sale, purchase, etc.)
        ''' as a human readable string.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.TradedType.</remarks>
        Public Property TradedTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradedTypeHumanReadable.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                Dim enumValue As TradedItemType = ConvertLocalizedName(Of TradedItemType)(value)
                If enumValue <> _TradedType Then
                    _TradedType = enumValue
                    _TradedTypeHumanReadable = ConvertLocalizedName(enumValue)
                    PropertyHasChanged()
                    PropertyHasChanged("TradedType")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a code of the VAT declaration schema that is used for integration with external systems.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.ExternalCode.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255)> _
        Public Property ExternalCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ExternalCode.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _ExternalCode.Trim <> value.Trim Then
                    _ExternalCode = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets a list of invoice item (VAT) sum mapings to VAT declaration fields
        ''' (defines declaration fields that the invoice (VAT) sum should be added (or subtracted) to).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DeclarationEntries() As VatDeclarationEntryList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DeclarationEntries
            End Get
        End Property


        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            Get
                If Not IsNew Then Return IsDirty
                Return (Not String.IsNullOrEmpty(_Name.Trim) _
                    OrElse Not String.IsNullOrEmpty(_Description.Trim) _
                    OrElse _DeclarationEntries.Count > 0)
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _DeclarationEntries.IsDirty
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid AndAlso _DeclarationEntries.IsValid
            End Get
        End Property



        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then
                result = Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error)
            End If
            If Not _DeclarationEntries.IsValid Then
                result = AddWithNewLine(result, _DeclarationEntries.GetAllBrokenRules, False)
            End If
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If MyBase.BrokenRulesCollection.WarningCount > 0 Then _
                result = Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning)
            If _DeclarationEntries.HasWarnings() Then _
                result = AddWithNewLine(result, _DeclarationEntries.GetAllWarnings, False)
            If _DeclarationEntries.Count < 1 Then result = AddWithNewLine(result, _
                Documents_VatDeclarationSchema_EntriesEmpty, False)
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return (MyBase.BrokenRulesCollection.WarningCount > 0 OrElse _
                _DeclarationEntries.Count < 1 OrElse _DeclarationEntries.HasWarnings())
        End Function


        ''' <summary>
        ''' Gets an XML proxy for the VatDeclarationSchema instance.
        ''' </summary>
        ''' <remarks>Used for serializing data to/from XML string.</remarks>
        Friend Function GetXmlProxy() As VatDeclarationSchemaProxy

            Dim result As New VatDeclarationSchemaProxy

            result.Description = _Description
            result.ExternalCode = _ExternalCode
            result.IsObsolete = _IsObsolete
            result.Name = _Name
            result.TradedType = _TradedType
            result.VatRate = _VatRate

            For Each entry As VatDeclarationEntry In _DeclarationEntries
                result.DeclarationEntries.Add(entry.GetXmlProxy())
            Next

            Return result

        End Function

        ''' <summary>
        ''' Gets an XML serilized data of the VatDeclarationSchema instance.
        ''' </summary>
        ''' <remarks>Used for serializing data to/from XML string.</remarks>
        Public Function GetXmlString() As String
            Return ToXmlString(Of VatDeclarationSchemaProxy)(GetXmlProxy())
        End Function

        ''' <summary>
        ''' Gets an XML serilized data of the VatDeclarationSchema list.
        ''' </summary>
        ''' <remarks>Used for serializing data to/from XML string.</remarks>
        Public Shared Function GetXmlString(ByVal list As BusinessObjectCollection(Of VatDeclarationSchema)) As String
            Dim result As New List(Of VatDeclarationSchemaProxy)
            For Each schema As VatDeclarationSchema In list.Result
                result.Add(schema.GetXmlProxy())
            Next
            Return ToXmlString(Of List(Of VatDeclarationSchemaProxy))(result)
        End Function

        Public Overrides Function Save() As VatDeclarationSchema
            Dim result As VatDeclarationSchema = MyBase.Save
            HelperLists.VatDeclarationSchemaInfoList.InvalidateCache()
            Return result
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Documents_VatDeclarationSchema_ToString, _Name)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
                New RuleArgs("Name"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
                New RuleArgs("Description"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
                New RuleArgs("ExternalCode"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New RuleArgs("VatRate"))

        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Documents.VatDeclarationSchema1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Documents.VatDeclarationSchema2")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Documents.VatDeclarationSchema3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Documents.VatDeclarationSchema3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new VatDeclarationSchema instance.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function NewVatDeclarationSchema() As VatDeclarationSchema
            Return New VatDeclarationSchema(False)
        End Function

        ''' <summary>
        ''' Gets a new VatDeclarationSchema instance using data in the XML proxy.
        ''' </summary>
        ''' <remarks>Used for serializing data to/from XML string.</remarks>
        Public Shared Function NewVatDeclarationSchema(ByVal xmlProxy As VatDeclarationSchemaProxy) As VatDeclarationSchema
            Return New VatDeclarationSchema(xmlProxy, False)
        End Function

        ''' <summary>
        ''' Gets a new VatDeclarationSchema instance as a child of some other object.
        ''' </summary>
        ''' <remarks>Should only be invoked on server side.</remarks>
        Friend Shared Function NewVatDeclarationSchemaChild() As VatDeclarationSchema
            Return New VatDeclarationSchema(True)
        End Function

        ''' <summary>
        ''' Gets a new VatDeclarationSchema instance using data in the XML proxy as a child of some other object.
        ''' </summary>
        ''' <remarks>Used for serializing data to/from XML string.
        ''' Should only be invoked on server side.</remarks>
        Friend Shared Function NewVatDeclarationSchemaChild(ByVal xmlProxy As VatDeclarationSchemaProxy) As VatDeclarationSchema
            Return New VatDeclarationSchema(xmlProxy, True)
        End Function

        ''' <summary>
        ''' Gets an existing VatDeclarationSchema instance from a database.
        ''' </summary>
        ''' <param name="id">An ID of the schema to get.</param>
        ''' <remarks></remarks>
        Public Shared Function GetVatDeclarationSchema(ByVal id As Integer) As VatDeclarationSchema
            Return DataPortal.Fetch(Of VatDeclarationSchema)(New Criteria(id))
        End Function

        ''' <summary>
        ''' Gets an existing VatDeclarationSchema instance from a database bypassing DataPortal.
        ''' </summary>
        ''' <param name="id">an ID of the schema to get</param>
        ''' <remarks>Should only be invoked on server side.
        ''' Required by the <see cref="BusinessObjectCollection(of T)">BusinessObjectCollection</see>
        ''' in order to fetch multiple schemas by a single request.</remarks>
        Friend Shared Function GetVatDeclarationSchemaChild(ByVal id As Integer) As VatDeclarationSchema
            Return New VatDeclarationSchema(id)
        End Function

        ''' <summary>
        ''' Deletes an existing VatDeclarationSchema instance from a database.
        ''' </summary>
        ''' <param name="id">An ID of the schema to delete.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteVatDeclarationSchema(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
            HelperLists.VatDeclarationSchemaInfoList.InvalidateCache()
        End Sub

        ''' <summary>
        ''' Deletes the VatDeclarationSchema instance as child of some other object from a database.
        ''' </summary>
        ''' <remarks>Should only be invoked on server side.</remarks>
        Friend Sub DeleteVatDeclarationSchemaChild()
            DoDelete()
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal createAsChild As Boolean)
            If createAsChild Then MarkAsChild()
            Create()
        End Sub

        Private Sub New(ByVal xmlProxy As VatDeclarationSchemaProxy, ByVal createAsChild As Boolean)
            If createAsChild Then MarkAsChild()
            Create(xmlProxy)
        End Sub

        Private Sub New(ByVal nID As Integer)
            MarkAsChild()
            DoFetch(nID)
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ID As Integer
            Public ReadOnly Property ID() As Integer
                Get
                    Return _ID
                End Get
            End Property
            Public Sub New(ByVal nID As Integer)
                _ID = nID
            End Sub
        End Class


        Private Sub Create()
            _DeclarationEntries = VatDeclarationEntryList.NewVatDeclarationEntryList()
            ValidationRules.CheckRules()
        End Sub

        Private Sub Create(ByVal xmlProxy As VatDeclarationSchemaProxy)

            _Description = xmlProxy.Description
            _ExternalCode = xmlProxy.ExternalCode
            _IsObsolete = xmlProxy.IsObsolete
            _Name = xmlProxy.Name
            _TradedType = xmlProxy.TradedType
            _TradedTypeHumanReadable = ConvertLocalizedName(_TradedType)
            _VatRate = xmlProxy.VatRate

            _DeclarationEntries = VatDeclarationEntryList.NewVatDeclarationEntryList()
            If Not xmlProxy.DeclarationEntries Is Nothing Then
                For Each entryProxy As VatDeclarationEntryProxy In xmlProxy.DeclarationEntries
                    _DeclarationEntries.Add(VatDeclarationEntry.NewVatDeclarationEntry(entryProxy))
                Next
            End If

            ValidationRules.CheckRules()

        End Sub


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            DoFetch(criteria.ID)

        End Sub

        Private Sub DoFetch(ByVal nID As Integer)

            Dim myComm As New SQLCommand("FetchVatDeclarationSchema")
            myComm.AddParam("?CD", nID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Documents_VatDeclarationSchema_TypeName, _
                    nID.ToString()))

                Dim dr As DataRow = myData.Rows(0)

                _ID = CIntSafe(dr.Item(0), 0)
                _Name = CStrSafe(dr.Item(1)).Trim
                _Description = CStrSafe(dr.Item(2)).Trim
                _VatRate = CDblSafe(dr.Item(3), 2, 0)
                _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(4), 0))
                _TradedType = ConvertDatabaseID(Of TradedItemType)(CIntSafe(dr.Item(5), 0))
                _TradedTypeHumanReadable = ConvertLocalizedName(_TradedType)
                _ExternalCode = CStrSafe(dr.Item(6)).Trim
                _InsertDate = CTimeStampSafe(dr.Item(7))
                _UpdateDate = CTimeStampSafe(dr.Item(8))

                _DeclarationEntries = VatDeclarationEntryList.GetVatDeclarationEntryList(_ID)

            End Using

            MarkOld()

        End Sub


        ''' <summary>
        ''' Saves the object as a child of some other object bypassing DataPortal and returns the saved instance.
        ''' </summary>
        ''' <remarks>Should only be invoked on server side.
        ''' Should only be invoked on child objects that were created or fetched using child factory methods.</remarks>
        Friend Function SaveChild() As VatDeclarationSchema

            If Not IsChild Then Throw New Exception(Common_InvalidSaveChild)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Dim result As VatDeclarationSchema = Me.Clone

            If IsNew Then
                result.DoInsert()
            Else
                result.DoUpdate()
            End If

            Return result

        End Function

        Protected Overrides Sub DataPortal_Insert()

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            DoInsert()

        End Sub

        Private Sub DoInsert()

            Dim myComm As New SQLCommand("InsertVatDeclarationSchema")
            AddWithParams(myComm)

            Using transaction As New SqlTransaction
                Try

                    myComm.Execute()

                    _ID = Convert.ToInt32(myComm.LastInsertID)

                    DeclarationEntries.Update(Me)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try
            End Using

            MarkOld()

        End Sub

        Protected Overrides Sub DataPortal_Update()

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            CheckIfUpdateDateChanged()

            DoUpdate()

        End Sub

        Private Sub DoUpdate()

            Dim myComm As New SQLCommand("UpdateVatDeclarationSchema")
            AddWithParams(myComm)
            myComm.AddParam("?CD", _ID)

            Using transaction As New SqlTransaction
                Try

                    myComm.Execute()

                    DeclarationEntries.Update(Me)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try
            End Using

            MarkOld()

        End Sub


        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(_ID))
        End Sub

        Protected Overrides Sub DataPortal_Delete(ByVal criteria As Object)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Dim result As New VatDeclarationSchema
            result._ID = DirectCast(criteria, Criteria).ID
            result.DoDelete()

            MarkNew()

        End Sub

        Private Sub DoDelete()

            CheckIfWasUsed(_ID)

            Dim myComm As New SQLCommand("DeleteVatDeclarationSchema")
            myComm.AddParam("?CD", _ID)

            Using transaction As New SqlTransaction
                Try

                    myComm.Execute()

                    myComm = New SQLCommand("DeleteVatDeclarationEntryList")
                    myComm.AddParam("?CD", _ID)
                    myComm.Execute()

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try
            End Using

            MarkNew()

        End Sub


        Private Sub AddWithParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AA", _Name.Trim)
            myComm.AddParam("?AB", _Description.Trim)
            myComm.AddParam("?AC", CRound(_VatRate))
            myComm.AddParam("?AD", ConvertDbBoolean(_IsObsolete))
            myComm.AddParam("?AE", ConvertDatabaseID(_TradedType))
            myComm.AddParam("?AF", _ExternalCode.Trim)

            _UpdateDate = GetCurrentTimeStamp()
            If Me.IsNew Then _InsertDate = _UpdateDate
            myComm.AddParam("?AG", _UpdateDate.ToUniversalTime)

        End Sub

        Private Sub CheckIfUpdateDateChanged()

            Dim myComm As New SQLCommand("CheckIfVatDeclarationSchemaUpdateDateChanged")
            myComm.AddParam("?CD", _ID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 OrElse CDateTimeSafe(myData.Rows(0).Item(0), _
                    Date.MinValue) = Date.MinValue Then

                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                        My.Resources.Documents_VatDeclarationSchema_TypeName, _ID.ToString))

                End If

                If CTimeStampSafe(myData.Rows(0).Item(0)) <> _UpdateDate Then

                    Throw New Exception(My.Resources.Common_UpdateDateHasChanged)

                End If

            End Using

        End Sub

        Private Shared Sub CheckIfWasUsed(ByVal schemaID As Integer)

            Dim myComm As New SQLCommand("CheckIfVatDeclarationSchemaWasUsed")
            myComm.AddParam("?CD", schemaID)

            Using myData As DataTable = myComm.Fetch
                If myData.Rows.Count > 0 Then
                    Throw New Exception(String.Format(My.Resources.Documents_VatDeclarationSchema_CannotDelete, _
                        CDateSafe(myData.Rows(0).Item(0), Date.MinValue), CStrSafe(myData.Rows(0).Item(1))))
                End If
            End Using

        End Sub

#End Region

    End Class

End Namespace