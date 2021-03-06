﻿Imports ApskaitaObjects.Attributes
Imports Csla.Validation
Imports ApskaitaObjects.My.Resources

Namespace Goods

    ''' <summary>
    ''' Represents a goods account prospective change: 
    ''' <see cref="GoodsItem.AccountDiscounts">AccountDiscounts</see> 
    ''' (for periodic accounting method only);
    ''' <see cref="GoodsItem.AccountPurchases">AccountPurchases</see> 
    ''' (for periodic accounting method only);
    ''' <see cref="GoodsItem.AccountSalesNetCosts">AccountSalesNetCosts</see> 
    ''' (for periodic accounting method only) or
    ''' <see cref="GoodsItem.AccountValueReduction">AccountValueReduction</see>.
    ''' </summary>
    ''' <remarks>Changes an account used by subsequent goods operations and moves 
    ''' the current balance to a new account.
    ''' Values are stored in the database table goodsaccounts.</remarks>
    <Serializable()> _
    Public NotInheritable Class GoodsOperationAccountChange
        Inherits BusinessBase(Of GoodsOperationAccountChange)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private ReadOnly _AllowedTypes As GoodsOperationType() = _
            New GoodsOperationType() {GoodsOperationType.AccountDiscountsChange, _
            GoodsOperationType.AccountPurchasesChange, _
            GoodsOperationType.AccountSalesNetCostsChange, _
            GoodsOperationType.AccountValueReductionChange}

        Private _ID As Integer = 0
        Private _Type As GoodsOperationType = GoodsOperationType.AccountDiscountsChange
        Private _TypeHumanReadable As String = Utilities.ConvertLocalizedName(GoodsOperationType.AccountDiscountsChange)
        Private _GoodsInfo As GoodsSummary
        Private _OperationLimitations As OperationalLimitList
        Private _JournalEntryID As Integer = 0
        Private _DocumentNumber As String = ""
        Private _Date As Date = Today
        Private _Description As String = ""
        Private _PreviousAccount As Long = 0
        Private _NewAccount As Long = 0
        Private _CorrespondationValue As Double = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now


        ''' <summary>
        ''' Gets an ID of the operation that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field goodsaccounts.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation data was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goodsaccounts.InsertDate.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation data was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goodsaccounts.UpdateDate.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object 
        ''' that contains business restraints on updating the operation data.
        ''' </summary>
        ''' <remarks>A <see cref="OperationalLimitList">OperationalLimitList</see> 
        ''' is used to validate a goods operation chronological business rules.</remarks>
        Public ReadOnly Property OperationLimitations() As OperationalLimitList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationLimitations
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the account that is changed by the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goodsaccounts.AccountType.</remarks>
        Public ReadOnly Property [Type]() As GoodsOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the account that is changed by the operation as 
        ''' a localized human readable string.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goodsaccounts.AccountType.</remarks>
        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeHumanReadable
            End Get
        End Property

        ''' <summary>
        ''' Gets a general data of the goods which account is changed.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goodsaccounts.GoodsID.</remarks>
        Public ReadOnly Property GoodsInfo() As GoodsSummary
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsInfo
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.JournalEntry.ID">ID of the encapsulated
        ''' journal entry</see> (if any).
        ''' </summary>
        ''' <remarks>A journal entry is only made if the current account balance is not null.
        ''' Value is stored in the database field goodsaccounts.JournalEntryID.</remarks>
        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="General.Account.ID">the current account</see> 
        ''' that is changed by the operation.
        ''' </summary>
        ''' <remarks>Value is fetched by a subquery.</remarks>
        Public ReadOnly Property PreviousAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PreviousAccount
            End Get
        End Property

        ''' <summary>
        ''' Gets the current balance of the account that is changed by the operation.
        ''' </summary>
        ''' <remarks>Value is fetched by a subquery.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CorrespondationValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CorrespondationValue)
            End Get
        End Property


        ''' <summary>
        ''' Gets or sets a date of the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goodsaccounts.ChangeDate.</remarks>
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
        ''' Gets or sets a document number of the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goodsaccounts.DocumentNumber.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 30)> _
        Public Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentNumber.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _DocumentNumber.Trim <> value.Trim Then
                    _DocumentNumber = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a description of the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goodsaccounts.Content.</remarks>
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
        ''' Gets or sets a <see cref="General.Account.ID">new account</see>.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goodsaccounts.AccountValue.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, False, 2, 6)> _
        Public Property NewAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NewAccount
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If NewAccountIsReadOnly Then Exit Property
                If _NewAccount <> value Then
                    _NewAccount = value
                    PropertyHasChanged()
                End If
            End Set
        End Property


        ''' <summary>
        ''' Whether the <see cref="NewAccount">NewAccount</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property NewAccountIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _OperationLimitations.FinancialDataCanChange
            End Get
        End Property

        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            Get
                If Not IsNew Then Return IsDirty
                Return (Not StringIsNullOrEmpty(_DocumentNumber) _
                    OrElse Not StringIsNullOrEmpty(_Description) OrElse _NewAccount > 0)
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid
            End Get
        End Property



        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If MyBase.BrokenRulesCollection.WarningCount > 0 Then
                result = AddWithNewLine(result, _
                    Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            End If
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return MyBase.BrokenRulesCollection.WarningCount > 0
        End Function


        Public Overrides Function Save() As GoodsOperationAccountChange
            Return MyBase.Save
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Goods_GoodsOperationAccountChange_ToString, _
                _Date.ToString("yyyy-MM-dd"), _TypeHumanReadable, _DocumentNumber, _Description)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("DocumentNumber"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Description"))

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.ChronologyValidation, _
                New CommonValidation.CommonValidation.ChronologyRuleArgs("Date", "OperationLimitations"))

            ValidationRules.AddRule(AddressOf NewAccountValidation, _
                New Validation.RuleArgs("NewAccount"))

        End Sub

        ''' <summary>
        ''' Rule ensuring that the value of property NewAccount is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function NewAccountValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As GoodsOperationAccountChange = DirectCast(target, GoodsOperationAccountChange)

            If valObj._NewAccount > 0 AndAlso valObj._NewAccount = valObj._PreviousAccount Then
                e.Description = Goods_GoodsOperationAccountChange_NewAccountInvalid
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            Return CommonValidation.CommonValidation.AccountFieldValidation(target, e)

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("Goods.GoodsOperationAccountChange2")
        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsOperationAccountChange1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsOperationAccountChange2")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsOperationAccountChange3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsOperationAccountChange3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new GoodsOperationAccountChange instance.
        ''' </summary>
        ''' <param name="goodsID">an <see cref="GoodsItem.ID">ID of the goods</see>
        ''' which account is changed by the new operation.</param>
        ''' <param name="accountType">a type of the account to change (allowed
        ''' values: <see cref="GoodsOperationType.AccountDiscountsChange">AccountDiscountsChange</see>,
        ''' <see cref="GoodsOperationType.AccountPurchasesChange">AccountPurchasesChange</see>,
        ''' <see cref="GoodsOperationType.AccountSalesNetCostsChange">AccountSalesNetCostsChange</see>
        ''' and <see cref="GoodsOperationType.AccountValueReductionChange">AccountValueReductionChange</see>)</param>
        ''' <remarks></remarks>
        Public Shared Function NewGoodsOperationAccountChange(ByVal goodsID As Integer, _
            ByVal accountType As GoodsOperationType) As GoodsOperationAccountChange
            Return DataPortal.Create(Of GoodsOperationAccountChange) _
                (New Criteria(goodsID, accountType))
        End Function

        ''' <summary>
        ''' Gets an existing GoodsOperationAccountChange instance from a database.
        ''' </summary>
        ''' <param name="id">an <see cref="ID">ID</see> of the operation to fetch</param>
        ''' <remarks></remarks>
        Public Shared Function GetGoodsOperationAccountChange(ByVal id As Integer) As GoodsOperationAccountChange
            Return DataPortal.Fetch(Of GoodsOperationAccountChange)(New Criteria(id))
        End Function

        ''' <summary>
        ''' Deletes an existing GoodsOperationAccountChange instance from a database.
        ''' </summary>
        ''' <param name="id">an <see cref="ID">ID</see> of the operation to delete</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteGoodsOperationAccountChange(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private mId As Integer
            Private mType As GoodsOperationType
            Public ReadOnly Property Id() As Integer
                Get
                    Return mId
                End Get
            End Property
            Public ReadOnly Property [Type]() As GoodsOperationType
                Get
                    Return mType
                End Get
            End Property
            Public Sub New(ByVal id As Integer, ByVal nType As GoodsOperationType)
                mId = id
                mType = nType
            End Sub
            Public Sub New(ByVal id As Integer)
                mId = id
                mType = GoodsOperationType.AccountDiscountsChange
            End Sub
        End Class


        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            If Array.IndexOf(_AllowedTypes, criteria.Type) < 0 Then
                Throw New Exception(String.Format( _
                    Goods_GoodsOperationAccountChange_TypeInvalid, _
                    Utilities.ConvertLocalizedName(criteria.Type)))
            End If

            If Not criteria.Id > 0 Then
                Throw New ArgumentException(Goods_GoodsOperationAccountChange_GoodsIdNull)
            End If

            _Type = criteria.Type
            _TypeHumanReadable = ConvertLocalizedName(_Type)

            _GoodsInfo = GoodsSummary.NewGoodsSummary(criteria.Id)

            _OperationLimitations = OperationalLimitList.NewOperationalLimitList( _
                _GoodsInfo, criteria.Type, 0, Nothing)

            If _GoodsInfo.AccountingMethod <> GoodsAccountingMethod.Periodic _
                AndAlso criteria.Type <> GoodsOperationType.AccountValueReductionChange Then
                Throw New Exception(Goods_GoodsOperationAccountChange_TypeInvalidForPersistentMethod)
            End If

            Select Case _Type
                Case GoodsOperationType.AccountDiscountsChange
                    _PreviousAccount = _GoodsInfo.AccountDiscounts
                Case GoodsOperationType.AccountPurchasesChange
                    _PreviousAccount = _GoodsInfo.AccountPurchases
                Case GoodsOperationType.AccountSalesNetCostsChange
                    _PreviousAccount = _GoodsInfo.AccountSalesNetCosts
                Case GoodsOperationType.AccountValueReductionChange
                    _PreviousAccount = _GoodsInfo.AccountValueReduction
                Case Else
                    Throw New Exception(String.Format( _
                        Goods_GoodsOperationAccountChange_TypeInvalid, _
                        Utilities.ConvertLocalizedName(criteria.Type)))
            End Select

            LoadCorrespondationValue()

            MarkNew()

            ValidationRules.CheckRules()

        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchGoodsOperationAccountChange")
            myComm.AddParam("?OD", criteria.Id)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Goods_GoodsOperationAccountChange_TypeName, _
                    criteria.Id.ToString()))

                Dim dr As DataRow = myData.Rows(0)

                _ID = CIntSafe(dr.Item(0), 0)
                _Type = ConvertDatabaseID(Of GoodsOperationType) _
                    (CIntSafe(dr.Item(1), 0))
                _TypeHumanReadable = Utilities.ConvertLocalizedName(_Type)
                _Date = CDateSafe(dr.Item(2), Today)
                _NewAccount = CLongSafe(dr.Item(3), 0)
                _DocumentNumber = CStrSafe(dr.Item(5)).Trim
                _Description = CStrSafe(dr.Item(6)).Trim
                _JournalEntryID = CIntSafe(dr.Item(7), 0)
                _CorrespondationValue = CDblSafe(dr.Item(8), 2, 0)
                _InsertDate = CTimeStampSafe(dr.Item(9))
                _UpdateDate = CTimeStampSafe(dr.Item(10))

                _GoodsInfo = GoodsSummary.GetGoodsSummary(dr, 11)

                If Not _PreviousAccount > 0 Then
                    Select Case _Type
                        Case GoodsOperationType.AccountDiscountsChange
                            _PreviousAccount = _GoodsInfo.AccountDiscounts
                        Case GoodsOperationType.AccountPurchasesChange
                            _PreviousAccount = _GoodsInfo.AccountPurchases
                        Case GoodsOperationType.AccountSalesNetCostsChange
                            _PreviousAccount = _GoodsInfo.AccountSalesNetCosts
                        Case GoodsOperationType.AccountValueReductionChange
                            _PreviousAccount = _GoodsInfo.AccountValueReduction
                    End Select
                End If

                _OperationLimitations = OperationalLimitList.GetOperationalLimitList( _
                    _GoodsInfo, _Type, _ID, _Date, 0, Nothing, Nothing)

            End Using

            MarkOld()

        End Sub


        Protected Overrides Sub DataPortal_Insert()

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            _OperationLimitations = OperationalLimitList.GetUpdatedOperationalLimitList( _
                _OperationLimitations, Nothing, Nothing)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    Me.BrokenRulesCollection.ToString(RuleSeverity.Error)))
            End If

            LoadCorrespondationValue()

            Dim entry As General.JournalEntry = GetJournalEntryForDocument()

            Dim myComm As New SQLCommand("InsertGoodsOperationAccountChange")
            myComm.AddParam("?AA", Utilities.ConvertDatabaseID(_Type))
            myComm.AddParam("?AB", _GoodsInfo.ID)
            AddWithParamsGeneral(myComm)
            AddWithParamsFinancial(myComm)

            Using transaction As New SqlTransaction

                Try

                    If Not entry Is Nothing Then
                        entry = entry.SaveChild
                        _JournalEntryID = entry.ID
                        myComm.AddParam("?AE", _JournalEntryID)
                    Else
                        myComm.AddParam("?AE", 0)
                    End If

                    myComm.Execute()

                    _ID = Convert.ToInt32(myComm.LastInsertID)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            _OperationLimitations = OperationalLimitList.GetOperationalLimitList( _
                _GoodsInfo, _Type, _ID, _Date, 0, Nothing, Nothing)

            MarkOld()

        End Sub

        Protected Overrides Sub DataPortal_Update()

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            _OperationLimitations = OperationalLimitList.GetUpdatedOperationalLimitList( _
                _OperationLimitations, Nothing, Nothing)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    Me.BrokenRulesCollection.ToString(RuleSeverity.Error)))
            End If

            Dim entry As General.JournalEntry = GetJournalEntryForDocument()

            CheckIfUpdateDateChanged()

            Dim myComm As SQLCommand
            If _OperationLimitations.FinancialDataCanChange Then
                myComm = New SQLCommand("UpdateGoodsOperationAccountChangeFull")
                AddWithParamsFinancial(myComm)
            Else
                myComm = New SQLCommand("UpdateGoodsOperationAccountChangeLimited")
            End If
            AddWithParamsGeneral(myComm)

            myComm.AddParam("?CD", _ID)

            Using transaction As New SqlTransaction

                Try

                    If Not entry Is Nothing Then
                        entry = entry.SaveChild()
                        _JournalEntryID = entry.ID
                    ElseIf _JournalEntryID > 0 Then
                        IndirectRelationInfoList.CheckIfJournalEntryCanBeDeleted( _
                            _JournalEntryID, DocumentType.GoodsAccountChange)
                        General.JournalEntry.DeleteJournalEntryChild(_JournalEntryID)
                        _JournalEntryID = 0
                    Else
                        _JournalEntryID = 0
                    End If
                    If _OperationLimitations.FinancialDataCanChange Then
                        myComm.AddParam("?AE", _JournalEntryID)
                    End If

                    myComm.Execute()

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            _OperationLimitations = OperationalLimitList.GetOperationalLimitList( _
                _GoodsInfo, _Type, _ID, _Date, 0, Nothing, Nothing)

            MarkOld()

        End Sub

        Private Function GetJournalEntryForDocument() As General.JournalEntry

            If CRound(_CorrespondationValue) = 0 Then Return Nothing

            Dim result As General.JournalEntry = Nothing

            If IsNew OrElse Not _JournalEntryID > 0 Then
                result = General.JournalEntry.NewJournalEntryChild(DocumentType.GoodsAccountChange)
            Else
                result = General.JournalEntry.GetJournalEntryChild(_JournalEntryID, _
                    DocumentType.GoodsAccountChange)
            End If

            If Not _OperationLimitations.FinancialDataCanChange AndAlso result.IsNew Then
                Throw New Exception(String.Format(Goods_GoodsOperationAccountChange_InvalidJournalEntryUpdate, _
                    vbCrLf, _OperationLimitations.FinancialDataCanChangeExplanation))
            End If

            result.Date = _Date.Date
            result.Person = Nothing
            result.Content = String.Format("{0} ({1})", _Description, _TypeHumanReadable)
            result.DocNumber = _DocumentNumber

            If _OperationLimitations.FinancialDataCanChange Then

                Dim commonBookEntryList As BookEntryInternalList = _
                BookEntryInternalList.NewBookEntryInternalList(BookEntryType.Debetas)

                If CRound(_CorrespondationValue) > 0 Then

                    commonBookEntryList.Add(BookEntryInternal.NewBookEntryInternal( _
                        BookEntryType.Kreditas, _PreviousAccount, _
                        CRound(_CorrespondationValue), Nothing))

                    commonBookEntryList.Add(BookEntryInternal.NewBookEntryInternal( _
                        BookEntryType.Debetas, _NewAccount, _
                        CRound(_CorrespondationValue), Nothing))

                Else

                    commonBookEntryList.Add(BookEntryInternal.NewBookEntryInternal( _
                        BookEntryType.Debetas, _PreviousAccount, _
                        CRound(-_CorrespondationValue), Nothing))

                    commonBookEntryList.Add(BookEntryInternal.NewBookEntryInternal( _
                        BookEntryType.Kreditas, _NewAccount, _
                        CRound(-_CorrespondationValue), Nothing))

                End If

                result.DebetList.LoadBookEntryListFromInternalList(commonBookEntryList, False, False)
                result.CreditList.LoadBookEntryListFromInternalList(commonBookEntryList, False, False)

            End If

            If Not result.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_FailedToCreateJournalEntry, _
                    vbCrLf, result.ToString, vbCrLf, result.GetAllBrokenRules))
            End If

            Return result

        End Function


        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(_ID))
        End Sub

        Protected Overrides Sub DataPortal_Delete(ByVal criteria As Object)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Dim operationToDelete As GoodsOperationAccountChange = New GoodsOperationAccountChange
            operationToDelete.DataPortal_Fetch(DirectCast(criteria, Criteria))

            If Not operationToDelete._OperationLimitations.FinancialDataCanChange Then
                Throw New Exception(String.Format(Goods_GoodsOperationAccountChange_DeleteInvalid, _
                    vbCrLf, operationToDelete._OperationLimitations.FinancialDataCanChangeExplanation))
            End If

            If operationToDelete.JournalEntryID > 0 Then
                IndirectRelationInfoList.CheckIfJournalEntryCanBeDeleted( _
                    operationToDelete.JournalEntryID, DocumentType.GoodsAccountChange)
            End If

            Dim myComm As New SQLCommand("DeleteGoodsOperationAccountChange")
            myComm.AddParam("?CD", DirectCast(criteria, Criteria).Id)

            Using transaction As New SqlTransaction

                Try

                    If operationToDelete.JournalEntryID > 0 Then
                        General.JournalEntry.DeleteJournalEntryChild(operationToDelete.JournalEntryID)
                    End If

                    myComm.Execute()

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkNew()

        End Sub


        Private Sub AddWithParamsGeneral(ByRef myComm As SQLCommand)

            myComm.AddParam("?AD", _Date.Date)
            myComm.AddParam("?AF", _DocumentNumber.Trim)
            myComm.AddParam("?AG", _Description.Trim)

            _UpdateDate = GetCurrentTimeStamp()
            If Me.IsNew Then _InsertDate = _UpdateDate
            myComm.AddParam("?AH", _UpdateDate.ToUniversalTime)

        End Sub

        Private Sub AddWithParamsFinancial(ByRef myComm As SQLCommand)
            myComm.AddParam("?AC", _NewAccount)
        End Sub


        Private Sub LoadCorrespondationValue()

            Dim myComm As SQLCommand

            Select Case _Type
                Case GoodsOperationType.AccountDiscountsChange
                    myComm = New SQLCommand("CreateGoodsOperationAccountChangeDiscounts")
                Case GoodsOperationType.AccountPurchasesChange
                    myComm = New SQLCommand("CreateGoodsOperationAccountChangePurchases")
                Case GoodsOperationType.AccountSalesNetCostsChange
                    myComm = New SQLCommand("CreateGoodsOperationAccountChangeSalesNetCosts")
                Case GoodsOperationType.AccountValueReductionChange
                    myComm = New SQLCommand("CreateGoodsOperationAccountChangePriceCut")
                Case Else
                    Throw New Exception(String.Format( _
                        Goods_GoodsOperationAccountChange_TypeInvalid, _
                        Utilities.ConvertLocalizedName(_Type)))
            End Select

            myComm.AddParam("?GD", _GoodsInfo.ID)

            Using myData As DataTable = myComm.Fetch
                If myData.Rows.Count > 0 Then
                    _CorrespondationValue = CDblSafe(myData.Rows(0).Item(0), 2, 0)
                End If
            End Using

        End Sub

        Private Sub CheckIfUpdateDateChanged()

            Dim myComm As New SQLCommand("CheckIfAccountChangeUpdateDateChanged")
            myComm.AddParam("?CD", _ID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 OrElse CDateTimeSafe(myData.Rows(0).Item(0), _
                    Date.MinValue) = Date.MinValue Then

                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                        My.Resources.Goods_GoodsOperationAccountChange_TypeName, _ID.ToString))

                End If

                If CTimeStampSafe(myData.Rows(0).Item(0)) <> _UpdateDate Then

                    Throw New Exception(My.Resources.Common_UpdateDateHasChanged)

                End If

            End Using

        End Sub

#End Region

    End Class

End Namespace