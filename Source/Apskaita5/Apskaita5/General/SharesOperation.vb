Namespace General

    ''' <summary>
    ''' Represents a single company'sshares operation that encompasses one or more 
    ''' shares acquisition and/ar discards.
    ''' </summary>
    ''' <remarks>Values are stored in the database table SharesOperations.</remarks>
    <Serializable()>
    Public NotInheritable Class SharesOperation
        Inherits BusinessBase(Of SharesOperation)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Date As Date = Today
        Private _DocumentDate As Date = Today
        Private _DocumentNumber As String = ""
        Private _DocumentName As String = ""
        Private _Remarks As String = ""
        Private WithEvents _Discards As SharesDiscardList
        Private WithEvents _Acquisitions As SharesAcquisitionList
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now


        ''' <summary>
        ''' Gets an ID of the operation (assigned by DB AUTO_INCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field SharesOperations.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a registration date of the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field SharesOperations.RegistrationDate.</remarks>
        Public Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Date
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If _Date.Date <> value.Date Then
                    _Date = value.Date
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a substantiating document date.
        ''' </summary>
        ''' <remarks>Value is stored in the database field SharesOperations.DocumentDate.</remarks>
        Public Property DocumentDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DocumentDate
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If _DocumentDate.Date <> value.Date Then
                    _DocumentDate = value.Date
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a number of the substantiating document (if any).
        ''' </summary>
        ''' <remarks>Value is stored in the database field SharesOperations.DocumentNo.</remarks>
        <StringField(ValueRequiredLevel.Optional, 50, False)>
        Public Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DocumentNumber.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
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
        ''' Gets or sets a name (title) of the substantiating document, e.g. sales contract, 
        ''' minutes of the shareholders meeting, etc.
        ''' </summary>
        ''' <remarks>Value is stored in the database field SharesOperations.DocumentName.</remarks>
        <StringField(ValueRequiredLevel.Recommended, 50, False)>
        Public Property DocumentName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DocumentName.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _DocumentName.Trim <> value.Trim Then
                    _DocumentName = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets arbitrary remarks regarding the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field SharesOperations.Remarks.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255, False)>
        Public Property Remarks() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Remarks.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Remarks.Trim <> value.Trim Then
                    _Remarks = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets a list of shares discard (sub) operations of the shares operation.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Discards() As SharesDiscardList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Discards
            End Get
        End Property

        ''' <summary>
        ''' Gets a list of shares acquisition (sub) operations of the shares operation.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Acquisitions() As SharesAcquisitionList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Acquisitions
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in the database field SharesOperations.InsertDate.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in the database field SharesOperations.UpdateDate.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Returnes TRUE if the operation is new and contains some user provided data 
        ''' OR
        ''' object is not new and was changed by the user.
        ''' </summary>
        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            Get
                If Not IsNew Then Return IsDirty
                Return (Not String.IsNullOrEmpty(_DocumentNumber.Trim) _
                    OrElse Not String.IsNullOrEmpty(_DocumentName.Trim) _
                    OrElse Not String.IsNullOrEmpty(_Remarks.Trim) _
                    OrElse _Acquisitions.Count > 0 _
                    OrElse _Discards.Count > 0)
            End Get
        End Property


        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _Discards.IsDirty OrElse _Acquisitions.IsDirty
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid AndAlso _Discards.IsValid AndAlso _Acquisitions.IsValid
            End Get
        End Property



        Public Overrides Function Save() As SharesOperation

            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf,
                    GetAllBrokenRules()))
            End If

            Return MyBase.Save

        End Function


        Private Sub Discards_Changed(ByVal sender As Object,
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _Discards.ListChanged


        End Sub

        Private Sub Acquisitions_Changed(ByVal sender As Object,
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _Acquisitions.ListChanged


        End Sub

        ''' <summary>
        ''' Helper method. Takes care of child lists loosing their handlers.
        ''' </summary>
        Protected Overrides Function GetClone() As Object
            Dim result As SharesOperation = DirectCast(MyBase.GetClone(), SharesOperation)
            result.RestoreChildListsHandles()
            Return result
        End Function

        Protected Overrides Sub OnDeserialized(ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.OnDeserialized(context)
            RestoreChildListsHandles()
        End Sub

        Protected Overrides Sub UndoChangesComplete()
            MyBase.UndoChangesComplete()
            RestoreChildListsHandles()
        End Sub

        ''' <summary>
        ''' Helper method. Takes care of TaskTimeSpans loosing its handler. See GetClone method.
        ''' </summary>
        Friend Sub RestoreChildListsHandles()
            Try
                RemoveHandler _Discards.ListChanged, AddressOf Discards_Changed
                RemoveHandler _Acquisitions.ListChanged, AddressOf Acquisitions_Changed
            Catch ex As Exception
            End Try
            AddHandler _Discards.ListChanged, AddressOf Discards_Changed
            AddHandler _Acquisitions.ListChanged, AddressOf Acquisitions_Changed
        End Sub


        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then result = AddWithNewLine(result,
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            If Not _Discards.IsValid Then result = AddWithNewLine(result,
                _Discards.GetAllBrokenRules, False)
            If Not _Acquisitions.IsValid Then result = AddWithNewLine(result,
                _Acquisitions.GetAllBrokenRules, False)
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If Me.BrokenRulesCollection.WarningCount > 0 Then
                result = AddWithNewLine(result, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            End If
            If _Discards.HasWarnings Then
                result = AddWithNewLine(result, _Discards.GetAllWarnings, False)
            End If
            If _Acquisitions.HasWarnings Then
                result = AddWithNewLine(result, _Acquisitions.GetAllWarnings, False)
            End If
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return Me.BrokenRulesCollection.WarningCount > 0 OrElse _Discards.HasWarnings _
                OrElse _Acquisitions.HasWarnings
        End Function


        Protected Overrides Function GetIdValue() As Object
            If IsNew Then
                Return _Guid
            Else
                Return _ID
            End If
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.General_SharesOperation_ToString,
                _Date.ToString("yyyy-MM-dd"), _DocumentDate.ToString("yyyy-MM-dd"),
                _DocumentName, _DocumentNumber)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation,
                New Csla.Validation.RuleArgs("DocumentNumber"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation,
                New Csla.Validation.RuleArgs("DocumentName"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation,
                New Csla.Validation.RuleArgs("Remarks"))


        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("General.SharesOperation2")
        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.SharesOperation1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.SharesOperation2")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.SharesOperation3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.SharesOperation3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets as new shares operation.
        ''' </summary>
        Public Shared Function NewSharesOperation() As SharesOperation
            If Not CanAddObject() Then Throw New System.Security.SecurityException(
                My.Resources.Common_SecurityInsertDenied)
            Dim result As New SharesOperation
            result._Acquisitions = SharesAcquisitionList.NewSharesAcquisitionList
            result._Discards = SharesDiscardList.NewSharesDiscardList
            result.ValidationRules.CheckRules()
            Return result
        End Function

        ''' <summary>
        ''' Gets an existing shares operation by it's ID.
        ''' </summary>
        ''' <param name="operationID">an ID of the operation to fetch</param>
        ''' <returns></returns>
        Public Shared Function GetSharesOperation(ByVal operationID As Integer) As SharesOperation
            Return DataPortal.Fetch(Of SharesOperation)(New Criteria(operationID))
        End Function

        ''' <summary>
        ''' Deletes an existing shares operation by it's ID.
        ''' </summary>
        ''' <param name="operationID">n ID of the operation to delete</param>
        Public Shared Sub DeleteSharesOperation(ByVal operationID As Integer)
            DataPortal.Delete(New Criteria(operationID))
        End Sub

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()>
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

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException(
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchSharesOperation")
            myComm.AddParam("?CD", criteria.ID)

            Using myData As DataTable = myComm.Fetch

                If Not myData.Rows.Count > 0 Then
                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound,
                        My.Resources.General_SharesOperation_TypeName, criteria.ID.ToString))
                End If

                Dim dr As DataRow = myData.Rows(0)

                _ID = CIntSafe(dr.Item(0), 0)
                _Date = CDateSafe(dr.Item(1), Today)
                _DocumentDate = CDateSafe(dr.Item(2), Today)
                _DocumentName = CStrSafe(dr.Item(3)).Trim
                _DocumentNumber = CStrSafe(dr.Item(4)).Trim
                _Remarks = CStrSafe(dr.Item(5)).Trim
                _InsertDate = CTimeStampSafe(dr.Item(6))
                _UpdateDate = CTimeStampSafe(dr.Item(7))

                myComm = New SQLCommand("FetchSharesOperationItems")
                myComm.AddParam("?CD", criteria.ID)
                Using details As DataTable = myComm.Fetch
                    _Discards = SharesDiscardList.GetSharesDiscardList(details)
                    _Acquisitions = SharesAcquisitionList.GetSharesAcquisitionList(details)
                End Using

            End Using

            MarkOld()

            ValidationRules.CheckRules()

        End Sub

        Protected Overrides Sub DataPortal_Insert()

            If Not CanAddObject() Then Throw New System.Security.SecurityException(
                My.Resources.Common_SecurityInsertDenied)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf,
                    GetAllBrokenRules()))
            End If

            Using transaction As New SqlTransaction

                Try

                    Dim myComm As New SQLCommand("InsertSharesOperation")
                    AddWithParams(myComm)

                    myComm.Execute()

                    _ID = Convert.ToInt32(myComm.LastInsertID)

                    Discards.Update(Me)
                    Acquisitions.Update(Me)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkOld()

        End Sub

        Protected Overrides Sub DataPortal_Update()

            If Not CanEditObject() Then Throw New System.Security.SecurityException(
                My.Resources.Common_SecurityUpdateDenied)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf,
                    GetAllBrokenRules()))
            End If

            CheckIfUpdateDateChanged()

            Using transaction As New SqlTransaction

                Try

                    Dim myComm As New SQLCommand("UpdateSharesOperation")
                    AddWithParams(myComm)
                    myComm.AddParam("?CD", _ID)

                    myComm.Execute()

                    Discards.Update(Me)
                    Acquisitions.Update(Me)

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

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException(
                My.Resources.Common_SecurityUpdateDenied)

            Using transaction As New SqlTransaction

                Try

                    Dim myComm As New SQLCommand("DeleteSharesOperation")
                    myComm.AddParam("?CD", DirectCast(criteria, Criteria).ID)
                    myComm.Execute()

                    myComm = New SQLCommand("DeleteSharesOperationItems")
                    myComm.AddParam("?CD", DirectCast(criteria, Criteria).ID)
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

            myComm.AddParam("?AA", _Date.Date)
            myComm.AddParam("?AB", _DocumentDate.Date)
            myComm.AddParam("?AC", _DocumentName.Trim)
            myComm.AddParam("?AD", _DocumentNumber.Trim)
            myComm.AddParam("?AE", _Remarks.Trim)

            _UpdateDate = GetCurrentTimeStamp()
            If Me.IsNew Then _InsertDate = _UpdateDate

            myComm.AddParam("?AF", _UpdateDate.ToUniversalTime)

        End Sub

        Private Sub CheckIfUpdateDateChanged()

            Dim myComm As New SQLCommand("CheckIfSharesOperationUpdateDateChanged")
            myComm.AddParam("?CD", _ID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 OrElse CDateTimeSafe(myData.Rows(0).Item(0),
                    Date.MinValue) = Date.MinValue Then

                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound,
                        My.Resources.General_SharesOperation_TypeName, _ID.ToString))

                End If

                If CTimeStampSafe(myData.Rows(0).Item(0)) <> _UpdateDate Then

                    Throw New Exception(My.Resources.Common_UpdateDateHasChanged)

                End If

            End Using

        End Sub

#End Region

    End Class

End Namespace
