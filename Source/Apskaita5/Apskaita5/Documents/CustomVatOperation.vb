Imports Csla.Validation
Imports ApskaitaObjects.My.Resources

Namespace Documents

    ''' <summary>
    ''' Represents a custom VAT operation (not invoice based operation that affects VAT obligations).
    ''' </summary>
    ''' <remarks>Provides additional info on top of a <see cref="General.JournalEntry">JournalEntry</see>,
    ''' but could also be a standalone operation.
    ''' Values are stored in the database table CustomVatOperations.</remarks>
    <Serializable()> _
    Public Class CustomVatOperation
        Inherits BusinessBase(Of CustomVatOperation)
        Implements IGetErrorForListItem

#Region " Business Methods "

        ''' <summary>
        ''' Types of the (journal entry) documents that could be associated with
        ''' the operation.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ReadOnly AllowedJournalEntryTypes As DocumentType() _
            = New DocumentType() {DocumentType.AccumulatedCosts, _
            DocumentType.Amortization, DocumentType.BankOperation, _
            DocumentType.ClosingEntries, DocumentType.Custom, _
            DocumentType.GoodsInternalTransfer, DocumentType.GoodsInventorization, _
            DocumentType.GoodsProduction, DocumentType.GoodsRevalue, _
            DocumentType.GoodsWriteOff, DocumentType.LongTermAssetDiscard, _
            DocumentType.None, DocumentType.Offset, _
            DocumentType.TillIncomeOrder, DocumentType.TillSpendingOrder, _
            DocumentType.TransferOfBalance}


        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ID As Integer = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _Date As Date = Today
        Private _Description As String = ""
        Private _VatSchema As VatDeclarationSchemaInfo = Nothing
        Private _ValueBase As Double = 0
        Private _ValueVat As Double = 0
        Private _JournalEntryId As Integer = 0
        Private _JournalEntryDate As Date = Today
        Private _JournalEntryContent As String = ""
        Private _JournalEntryCorrespondence As String = ""
        Private _JournalEntryRelatedPerson As String = ""
        Private _JournalEntryType As DocumentType = DocumentType.None
        Private _JournalEntryTypeHumanReadable As String = ""
        Private _JournalEntryDocNo As String = ""


        ''' <summary>
        ''' Gets an ID of the custom VAT operation assigned by the database (autoincrement).
        ''' </summary>
        ''' <remarks>Value is stored in the database field customvatoperations.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in the database field customvatoperations.InsertDate.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in the database field customvatoperations.UpdateDate.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the date of the operation. In general it should match the attached
        ''' journal entry date (if any), but it might differ for tax reporting purpose.
        ''' </summary>
        ''' <remarks>Value is stored in the database field customvatoperations.OperationDate.</remarks>
        Public Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If _Date.Date <> value.Date Then
                    _Date = value.Date
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a description of the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field customvatoperations.Description.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
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
        ''' Gets or sets the applicable VAT declaration schema for the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database table customvatoperations.VatSchemaID.</remarks>
        <VatDeclarationSchemaFieldAttribute(ValueRequiredLevel.Mandatory, TradedItemType.All)> _
        Public Property VatSchema() As VatDeclarationSchemaInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VatSchema
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As VatDeclarationSchemaInfo)
                CanWriteProperty(True)
                If _VatSchema <> value Then
                    _VatSchema = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the base value of the operation, i.e. value excluding VAT.
        ''' </summary>
        ''' <remarks>Value is stored in the database table customvatoperations.ValueBase.</remarks>
        <DoubleField(ValueRequiredLevel.Recommended, True, 2)> _
        Public Property ValueBase() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueBase)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ValueBase) <> CRound(value) Then
                    _ValueBase = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the VAT value of the operation. In general should be equal to
        ''' <see cref="ValueBase">ValueBase</see> * <see cref="VatDeclarationSchemaInfo.VatRate">VatSchema.VatRate</see>,
        ''' but it might differ for tax reporting purpose. In any case both <see cref="ValueBase">ValueBase</see>
        ''' and <see cref="ValueVat">ValueVat</see> cannot be null.
        ''' </summary>
        ''' <remarks>Value is stored in the database table customvatoperations.ValueVat.</remarks>
        <DoubleField(ValueRequiredLevel.Recommended, True, 2)> _
        Public Property ValueVat() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueVat)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ValueVat) <> CRound(value) Then
                    _ValueVat = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.JournalEntry.ID">ID of the journal entry</see>
        ''' that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke the appropriate operation factory method to associate a journal entry with the operation.
        ''' Value is stored in the database table customvatoperations.JournalEntryId.</remarks>
        Public ReadOnly Property JournalEntryId() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryId
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.[Date]">date of the journal entry</see>
        ''' that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke the appropriate operation factory method to associate a journal entry with the operation.
        ''' Value is not persisted in the database (info value).</remarks>
        Public ReadOnly Property JournalEntryDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDate
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.Content">content of the journal entry</see>
        ''' that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke the appropriate operation factory method to associate a journal entry with the operation.
        ''' Value is not persisted in the database (info value).</remarks>
        Public ReadOnly Property JournalEntryContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryContent.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="ActiveReports.JournalEntryInfo.BookEntries">
        ''' description of the book entries of the journal entry</see>
        ''' that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke the appropriate operation factory method to associate a journal entry with the operation.
        ''' Value is not persisted in the database (info value).</remarks>
        Public ReadOnly Property JournalEntryCorrespondence() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryCorrespondence.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.Person">person of the journal entry</see>
        ''' that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke the appropriate operation factory method to associate a journal entry with the operation.
        ''' Value is not persisted in the database (info value).</remarks>
        Public ReadOnly Property JournalEntryRelatedPerson() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryRelatedPerson.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.DocType">type of the journal entry</see>
        ''' that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke the appropriate operation factory method to associate a journal entry with the operation.
        ''' Value is not persisted in the database (info value).</remarks>
        Public ReadOnly Property JournalEntryType() As DocumentType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryType
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.DocType">type of the journal entry</see>,
        ''' that is associated with the operation, as a localized human readable string.
        ''' </summary>
        ''' <remarks>Invoke the appropriate operation factory method to associate a journal entry with the operation.
        ''' Value is not persisted in the database (info value).</remarks>
        Public ReadOnly Property JournalEntryTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryTypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.DocNumber">document number 
        ''' of the journal entry</see> that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke the appropriate operation factory method to associate a journal entry with the operation.
        ''' Value is not persisted in the database (info value).</remarks>
        Public ReadOnly Property JournalEntryDocNo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDocNo.Trim
            End Get
        End Property



        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            If IsValid Then Return ""
            Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString, vbCrLf, _
                Me.BrokenRulesCollection.ToString(RuleSeverity.Error))
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If BrokenRulesCollection.WarningCount < 1 Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, vbCrLf, _
                Me.BrokenRulesCollection.ToString(RuleSeverity.Warning))
        End Function

        Public Function HasWarnings() As Boolean
            Return BrokenRulesCollection.WarningCount > 0
        End Function


        ''' <summary>
        ''' Calculates <see cref="ValueVat">VAT value</see> by multiplying 
        ''' <see cref="ValueBase">base value</see> and <see cref="VatDeclarationSchemaInfo.VatRate">vat rate</see>
        ''' of the <see cref="VatSchema">VAT schema assigned</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub CalculateVat()
            Dim rate As Double = 0
            If _VatSchema <> VatDeclarationSchemaInfo.Empty() Then
                rate = _VatSchema.VatRate
            End If
            ValueVat = _ValueBase * rate / 100
        End Sub


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Dim schema As String = Documents_CustomVatOperation_NullSchemaName
            If _VatSchema <> VatDeclarationSchemaInfo.Empty() Then schema = _VatSchema.ToString()
            Return String.Format(Documents_CustomVatOperation_ToString, _
                _Date, _Description, schema)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
                New Validation.RuleArgs("Description"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Validation.RuleArgs("ValueBase"))
            ValidationRules.AddRule(AddressOf CommonValidation.VatDeclarationSchemaFieldValidation, _
                New CommonValidation.VatDeclarationSchemaFieldRuleArgs("VatSchema", _
                ""))

            ValidationRules.AddRule(AddressOf ValueVatValidation, New RuleArgs("ValueVat"))

            ValidationRules.AddDependantProperty("ValueBase", "ValueVat", False)
            ValidationRules.AddDependantProperty("VatSchema", "ValueVat", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring that the value of property ValueVat is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function ValueVatValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As CustomVatOperation = DirectCast(target, CustomVatOperation)

            If valObj.ValueBase = 0 AndAlso valObj.ValueVat = 0 Then
                e.Description = "Operacijos suma ir PVM suma negali būti abi lygios nuliui."
                e.Severity = Validation.RuleSeverity.Error
                Return False
            ElseIf valObj._VatSchema <> VatDeclarationSchemaInfo.Empty() AndAlso _
                valObj.ValueVat <> CRound(valObj._ValueBase * valObj._VatSchema.VatRate / 100) Then
                e.Description = "Operacijos PVM suma nesutampa su operacijos sumos ir PVM deklaravimo schemos PVM tarifo sandauga."
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            End If

            Return True

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Creates a new CustomVatOperation instance without a journal entry attached.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function NewCustomVatOperation() As CustomVatOperation
            Dim result As New CustomVatOperation()
            result.ValidationRules.CheckRules()
            Return result
        End Function

        ''' <summary>
        ''' Creates a new CustomVatOperation instance with a journal entry attached.
        ''' </summary>
        ''' <param name="entryInfo">a journal entry info object to attach</param>
        ''' <remarks></remarks>
        Friend Shared Function NewCustomVatOperation(ByVal journalEntryID As Integer) As CustomVatOperation
            Return New CustomVatOperation(journalEntryID)
        End Function

        Friend Shared Function GetCustomVatOperation(ByVal dr As DataRow) As CustomVatOperation
            Return New CustomVatOperation(dr)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal journalEntryID As Integer)
            ' require use of factory methods
            Create(journalEntryID)
            MarkAsChild()
        End Sub

        Private Sub New(ByVal dr As DataRow)
            MarkAsChild()
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal journalEntryID As Integer)

            If Not journalEntryID > 0 Then
                Throw New Exception(Documents_CustomVatOperation_JournalEntryNull)
            End If

            Dim myComm As New SQLCommand("CreateCustomVatOperation")
            myComm.AddParam("?CD", journalEntryID)

            Using myData As DataTable = myComm.Fetch()

                If Not myData.Rows.Count > 0 Then
                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                        My.Resources.General_JournalEntry_TypeName, journalEntryID.ToString))
                End If

                DoFetch(myData.Rows(0))

            End Using

            If _ID > 0 Then
                MarkOld()
            Else
                _Date = _JournalEntryDate
                If Array.IndexOf(AllowedJournalEntryTypes, _JournalEntryType) < 0 Then
                    Throw New Exception(String.Format(Documents_CustomVatOperation_JournalEntryTypeInvalid, _
                        _JournalEntryTypeHumanReadable))
                End If
            End If

            ValidationRules.CheckRules()

        End Sub

        Private Sub Fetch(ByVal dr As DataRow)

            DoFetch(dr)

            MarkOld()

            ValidationRules.CheckRules()

        End Sub

        Private Sub DoFetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(0), 0)
            _Date = CDateSafe(dr.Item(1), Today)
            _Description = CStrSafe(dr.Item(2)).Trim
            _ValueBase = CDblSafe(dr.Item(3), 2, 0)
            _ValueVat = CDblSafe(dr.Item(4), 2, 0)
            _InsertDate = CTimeStampSafe(dr.Item(5))
            _UpdateDate = CTimeStampSafe(dr.Item(6))
            _JournalEntryId = CIntSafe(dr.Item(7), 0)
            _JournalEntryDate = CDateSafe(dr.Item(8), Today)
            _JournalEntryContent = CStrSafe(dr.Item(9)).Trim
            _JournalEntryType = ConvertDatabaseCharID(Of DocumentType)(CStrSafe(dr.Item(10)))
            _JournalEntryTypeHumanReadable = ConvertLocalizedName(_JournalEntryType)
            _JournalEntryDocNo = CStrSafe(dr.Item(11)).Trim
            _JournalEntryCorrespondence = CStrSafe(dr.Item(12)).Trim
            _JournalEntryRelatedPerson = CStrSafe(dr.Item(13)).Trim

            _VatSchema = VatDeclarationSchemaInfo.GetVatDeclarationSchemaInfo(dr, 14)

        End Sub

        Friend Sub Insert()

            Dim myComm As New SQLCommand("InsertCustomVatOperation")
            AddWithParams(myComm)
            myComm.AddParam("?AG", _JournalEntryId)

            myComm.Execute()

            _ID = Convert.ToInt32(myComm.LastInsertID)

            MarkOld()

        End Sub

        Friend Sub Update()

            Dim myComm As New SQLCommand("UpdateCustomVatOperation")
            myComm.AddParam("?CD", _ID)
            AddWithParams(myComm)

            myComm.Execute()

            MarkOld()

        End Sub

        Friend Sub DeleteSelf()

            Dim myComm As New SQLCommand("DeleteCustomVatOperation")
            myComm.AddParam("?CD", _ID)

            myComm.Execute()

            MarkNew()

        End Sub


        Private Sub AddWithParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AA", _Date.Date)
            myComm.AddParam("?AB", _Description.Trim)
            myComm.AddParam("?AC", CRound(_ValueBase))
            myComm.AddParam("?AD", CRound(_ValueVat))
            myComm.AddParam("?AE", _VatSchema.ID)

            _UpdateDate = GetCurrentTimeStamp()
            If Me.IsNew Then _InsertDate = _UpdateDate

            myComm.AddParam("?AF", _UpdateDate.ToUniversalTime)

        End Sub


#End Region

    End Class

End Namespace