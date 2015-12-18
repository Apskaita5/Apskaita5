Namespace Assets

    ''' <summary>
    ''' Represents a long term asset balance transfer operation.
    ''' Registers assets acquired before the transfer of balance.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class LongTermAssetsTransferOfBalance
        Inherits BusinessBase(Of LongTermAssetsTransferOfBalance)
        Implements IIsDirtyEnough

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _ChronologyValidator As ComplexChronologicValidator
        Private _Date As Date = Today
        Private WithEvents _Items As LongTermAssetList

        ' used to implement automatic sort in datagridview
        <NotUndoable()> _
        <NonSerialized()> _
        Private _ItemsSortedList As Csla.SortedBindingList(Of LongTermAsset) = Nothing


        ''' <summary>
        ''' Gets an <see cref="General.TransferOfBalance.id">ID of the transfer of balance 
        ''' operation journal entry</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was inserted into the database.
        ''' </summary>
        ''' <remarks>Returns a date and time when the first long term asset 
        ''' was inserted.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was last updated.
        ''' </summary>
        ''' <remarks>Returns a date and time when the last long term asset 
        ''' was modified.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object 
        ''' that contains business restraints on updating the document.
        ''' </summary>
        ''' <remarks>A <see cref="ComplexChronologicValidator">ComplexChronologicValidator</see> 
        ''' is used to validate a long term assets transfer of balance document 
        ''' chronological business rules.</remarks>
        Public ReadOnly Property ChronologyValidator() As ComplexChronologicValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologyValidator
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.TransferOfBalance.Date">date of the transfer of balance 
        ''' operation journal entry</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a collection of long term asset data that is beeing transfered
        ''' together with balance.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Items() As LongTermAssetList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Items
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable view of the collection of long term asset data 
        ''' that is beeing transfered together with balance.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public ReadOnly Property ItemsSorted() As Csla.SortedBindingList(Of LongTermAsset)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _ItemsSortedList Is Nothing Then
                    _ItemsSortedList = New Csla.SortedBindingList(Of LongTermAsset)(_Items)
                End If
                Return _ItemsSortedList
            End Get
        End Property


        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            Get
                Return IsDirty
            End Get
        End Property


        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _Items.IsDirty
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso _Items.IsValid
            End Get
        End Property



        Public Function GetAllBrokenRules() As String
            Dim result As String = ""
            If Not MyBase.IsValid Then
                result = AddWithNewLine(result, _
                    Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            End If
            If Not _Items.IsValid Then
                result = AddWithNewLine(result, _Items.GetAllBrokenRules(), False)
            End If
            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = ""
            If Not MyBase.BrokenRulesCollection.WarningCount > 0 Then
                result = AddWithNewLine(result, _
                    Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            End If
            If _Items.HasWarnings() Then
                result = AddWithNewLine(result, _Items.GetAllWarnings(), False)
            End If
            Return result
        End Function

        Public Function HasWarnings() As Boolean
            Return (MyBase.BrokenRulesCollection.WarningCount > 0 OrElse _Items.HasWarnings())
        End Function


        ''' <summary>
        ''' Imports long term asset using string (clipboard) data.
        ''' </summary>
        ''' <param name="source">Paste string, lines delimited by CrLf, fields - by Tab.</param>
        ''' <remarks></remarks>
        Public Sub ImportRange(ByVal source As String)

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            If StringIsNullOrEmpty(source) Then _
                Throw New Exception(My.Resources.Assets_LongTermAssetsTransferOfBalance_PasteStringEmpty)

            _Items.ImportRange(source.Split(New String() {vbCrLf}, _
                StringSplitOptions.RemoveEmptyEntries), vbTab, _
                _ChronologyValidator.BaseValidator)

        End Sub


        Private Sub Items_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _Items.ListChanged

            If e.ListChangedType = ComponentModel.ListChangedType.ItemAdded Then

                Try
                    _ChronologyValidator.MergeNewValidationItem(_Items(e.NewIndex).ChronologyValidator)
                    PropertyHasChanged("ChronologyValidator")
                Catch ex As Exception
                End Try

            ElseIf e.ListChangedType = ComponentModel.ListChangedType.ItemDeleted Then

                _ChronologyValidator = ComplexChronologicValidator.GetComplexChronologicValidator( _
                    _ChronologyValidator, _Items.GetChronologyValidators())

                PropertyHasChanged("ChronologyValidator")

            End If

        End Sub

        ''' <summary>
        ''' Helper method. Takes care of child lists loosing their handlers.
        ''' </summary>
        Protected Overrides Function GetClone() As Object
            Dim result As LongTermAssetsTransferOfBalance = DirectCast(MyBase.GetClone(), LongTermAssetsTransferOfBalance)
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
        ''' Helper method. Takes care of ReportItems loosing its handler. See GetClone method.
        ''' </summary>
        Friend Sub RestoreChildListsHandles()
            Try
                RemoveHandler _Items.ListChanged, AddressOf Items_Changed
            Catch ex As Exception
            End Try
            AddHandler _Items.ListChanged, AddressOf Items_Changed
        End Sub


        Public Overrides Function Save() As LongTermAssetsTransferOfBalance
            Return MyBase.Save
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return My.Resources.Assets_LongTermAssetsTransferOfBalance_ToString
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf CommonValidation.ChronologyValidation, _
                New CommonValidation.ChronologyRuleArgs("Date", "ChronologyValidator"))
            ValidationRules.AddDependantProperty("ChronologyValidator", "Date", False)
        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAsset1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return False
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAsset3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return False
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets an existing (unique and single per company) LongTermAssetsTransferOfBalance 
        ''' instance from a database.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function GetLongTermAssetsTransferOfBalance() As LongTermAssetsTransferOfBalance
            Return DataPortal.Fetch(Of LongTermAssetsTransferOfBalance)(New Criteria())
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Public Sub New()
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            Dim myComm As New SQLCommand("FetchTransferOfBalanceData")
            
            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception( _
                    My.Resources.Assets_LongTermAssetsTransferOfBalance_TransferOfBalanceDoesNotExist)

                Dim dr As DataRow = myData.Rows(0)

                _ID = CIntSafe(dr.Item(0), 0)
                _Date = CDateSafe(dr.Item(1), Today)
                
            End Using

            Dim baseValidator As SimpleChronologicValidator = _
                SimpleChronologicValidator.GetSimpleChronologicValidator( _
                _ID, Nothing)

            _Items = LongTermAssetList.GetLongTermAssetList(baseValidator)

            _InsertDate = _Items.GetInsertDate()
            _UpdateDate = _Items.GetUpdateDate()

            _ChronologyValidator = ComplexChronologicValidator.GetComplexChronologicValidator( _
                _ID, _Date, My.Resources.Assets_LongTermAssetsTransferOfBalance_TypeName, _
                baseValidator, Nothing, _Items.GetChronologyValidators())

            MarkOld()

            ValidationRules.CheckRules()

        End Sub

        Protected Overrides Sub DataPortal_Update()

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            _Items.Update(Me)

            MarkOld()

        End Sub

#End Region

    End Class

End Namespace