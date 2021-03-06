﻿Imports ApskaitaObjects.Attributes

Namespace Goods

    ''' <summary>
    ''' Represents a production template (""recipe"").
    ''' </summary>
    ''' <remarks>Values are stored in the database table kalkuliac.</remarks>
    <Serializable()> _
    Public NotInheritable Class ProductionCalculation
        Inherits BusinessBase(Of ProductionCalculation)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _Goods As GoodsInfo = Nothing
        Private _Amount As Double = 1
        Private _Date As Date = Today
        Private _IsObsolete As Boolean = False
        Private _Description As String = ""
        Private WithEvents _ComponentList As ProductionComponentItemList
        Private WithEvents _CostList As ProductionCostItemList


        ' used to implement automatic sort in datagridview
        <NotUndoable()> _
        <NonSerialized()> _
        Private _ComponentListSortedList As Csla.SortedBindingList(Of ProductionComponentItem) = Nothing
        ' used to implement automatic sort in datagridview
        <NotUndoable()> _
        <NonSerialized()> _
        Private _CostListSortedList As Csla.SortedBindingList(Of ProductionCostItem) = Nothing

        ''' <summary>
        ''' Gets an ID of the production template that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the production template was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in the database field kalkuliac.InsertDate.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the production template was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in the database field kalkuliac.UpdateDate.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets goods that are produced using the production template.
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.P_ID.</remarks>
        <GoodsField(ValueRequiredLevel.Mandatory)> _
        Public Property Goods() As GoodsInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Goods
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As GoodsInfo)
                CanWriteProperty(True)
                If Not (_Goods Is Nothing AndAlso value Is Nothing) _
                    AndAlso Not (Not _Goods Is Nothing AndAlso Not value Is Nothing _
                    AndAlso _Goods.ID = value.ID) Then
                    _Goods = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a standard amount of the goods produced 
        ''' (components and costs amounts are set for this amount of the goods produced)
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.Vnt_sk.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, ROUNDAMOUNTGOODS)> _
        Public Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Amount, ROUNDAMOUNTGOODS)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_Amount, ROUNDAMOUNTGOODS) <> CRound(value, ROUNDAMOUNTGOODS) Then
                    _Amount = CRound(value, ROUNDAMOUNTGOODS)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a date of the production template.
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.K_data.</remarks>
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
        ''' Gets or sets whether the production template is obsolete (no longer in use).
        ''' </summary>
        ''' <remarks>Value is stored in the database field kalkuliac.IsObsolete.</remarks>
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
        ''' Gets or sets a description of the production template.
        ''' </summary>
        ''' <remarks>Data is stored in database field kalkuliac.Description.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255)> _
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
        ''' Gets a list of production template component items 
        ''' (goods (components, stock) consumed in production).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ComponentList() As ProductionComponentItemList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComponentList
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable view of the component items list  
        ''' (goods (components, stock) consumed in production).
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public ReadOnly Property ComponentListSorted() As Csla.SortedBindingList(Of ProductionComponentItem)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _ComponentListSortedList Is Nothing Then
                    _ComponentListSortedList = New Csla.SortedBindingList(Of ProductionComponentItem)(_ComponentList)
                End If
                Return _ComponentListSortedList
            End Get
        End Property

        ''' <summary>
        ''' Gets a list of production template costs items (costs incured by the production).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CostList() As ProductionCostItemList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CostList
            End Get
        End Property

        ''' <summary>
        ''' Gets sortable view of the costs items list (costs incured by the production).
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public ReadOnly Property CostListSorted() As Csla.SortedBindingList(Of ProductionCostItem)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _CostListSortedList Is Nothing Then
                    _CostListSortedList = New Csla.SortedBindingList(Of ProductionCostItem)(_CostList)
                End If
                Return _CostListSortedList
            End Get
        End Property


        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            Get
                If Not IsNew Then Return IsDirty
                Return _ComponentList.Count > 0 OrElse _CostList.Count > 0 _
                    OrElse Not StringIsNullOrEmpty(_Description) _
                    OrElse CRound(_Amount, ROUNDAMOUNTGOODS) > 0
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _ComponentList.IsDirty OrElse _CostList.IsDirty
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid AndAlso _ComponentList.IsValid AndAlso _CostList.IsValid
            End Get
        End Property



        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then
                result = Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error)
            End If
            If Not _CostList.IsValid Then
                result = AddWithNewLine(result, _CostList.GetAllBrokenRules, False)
            End If
            If Not _ComponentList.IsValid Then
                result = AddWithNewLine(result, _ComponentList.GetAllBrokenRules, False)
            End If
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If BrokenRulesCollection.WarningCount > 0 Then
                result = Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning)
            End If
            If _CostList.HasWarnings Then
                result = AddWithNewLine(result, _CostList.GetAllWarnings(), False)
            End If
            If Not _ComponentList.HasWarnings Then
                result = AddWithNewLine(result, _ComponentList.GetAllWarnings(), False)
            End If
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return BrokenRulesCollection.WarningCount > 0 OrElse _CostList.HasWarnings _
                OrElse _ComponentList.HasWarnings
        End Function


        Public Overrides Function Save() As ProductionCalculation
            Dim result As ProductionCalculation = MyBase.Save
            HelperLists.ProductionCalculationInfoList.InvalidateCache()
            Return result
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If _Goods Is Nothing OrElse _Goods.IsEmpty Then
                Return String.Format(My.Resources.Goods_ProductionCalculation_ToString, _
                    _Date.ToString("yyyy-MM-dd"), "")
            Else
                Return String.Format(My.Resources.Goods_ProductionCalculation_ToString, _
                    _Date.ToString("yyyy-MM-dd"), _Goods.Name)
            End If
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.ValueObjectFieldValidation, _
                New Csla.Validation.RuleArgs("Goods"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("Amount"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Description"))

        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("Goods.ProductionCalculation2")
        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.ProductionCalculation1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.ProductionCalculation2")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.ProductionCalculation3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.ProductionCalculation3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new ProductionCalculation instance.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function NewProductionCalculation() As ProductionCalculation
            Dim result As New ProductionCalculation
            result.Create()
            Return result
        End Function

        ''' <summary>
        ''' Gets an existing ProductionCalculation instance from a database.
        ''' </summary>
        ''' <param name="id">an <see cref="ID">ID</see> of the template to fetch</param>
        ''' <remarks></remarks>
        Public Shared Function GetProductionCalculation(ByVal id As Integer) As ProductionCalculation
            Return DataPortal.Fetch(Of ProductionCalculation)(New Criteria(id))
        End Function

        ''' <summary>
        ''' Gets an existing ProductionCalculation instance from a database
        ''' bypassing the dataportal.
        ''' </summary>
        ''' <param name="id">an <see cref="ID">ID</see> of the template to fetch</param>
        ''' <remarks>Should only be invoked on server side.</remarks>
        Friend Shared Function GetProductionCalculationChild(ByVal id As Integer) As ProductionCalculation
            Return New ProductionCalculation(id)
        End Function

        ''' <summary>
        ''' Deletes an existing ProductionCalculation instance from a database.
        ''' </summary>
        ''' <param name="id">an <see cref="ID">ID</see> of the template to delete</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteProductionCalculation(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
            HelperLists.ProductionCalculationInfoList.InvalidateCache()
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal nID As Integer)
            DataPortal_Fetch(New Criteria(nID))
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
            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)
            _ComponentList = ProductionComponentItemList.NewProductionComponentItemList
            _CostList = ProductionCostItemList.NewProductionCostItemList
            ValidationRules.CheckRules()
        End Sub


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchProductionCalculation")
            myComm.AddParam("?CD", criteria.ID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Goods_ProductionCalculation_TypeName, _
                    criteria.ID.ToString()))

                Dim dr As DataRow = myData.Rows(0)

                _ID = CIntSafe(dr.Item(0), 0)
                _Amount = CDblSafe(dr.Item(1), ROUNDAMOUNTGOODS, 0)
                _Date = CDateSafe(dr.Item(2), Today)
                _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(3), 0))
                _Description = CStrSafe(dr.Item(4))
                _InsertDate = CTimeStampSafe(dr.Item(5))
                _UpdateDate = CTimeStampSafe(dr.Item(6))
                _Goods = GoodsInfo.GetGoodsInfo(dr, 7)

            End Using

            myComm = New SQLCommand("FetchProductionItemList")
            myComm.AddParam("?CD", criteria.ID)

            Using myData As DataTable = myComm.Fetch

                _ComponentList = ProductionComponentItemList.GetProductionComponentItemList(myData)
                _CostList = ProductionCostItemList.GetProductionCostItemList(myData)

            End Using

            MarkOld()

        End Sub


        Protected Overrides Sub DataPortal_Insert()

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            If Not _CostList.Count > 0 AndAlso Not _ComponentList.Count > 0 Then _
                Throw New Exception(My.Resources.Goods_ProductionCalculation_ListsEmpty)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Dim myComm As New SQLCommand("InsertProductionCalculation")
            AddWithParams(myComm)

            Using transaction As New SqlTransaction

                Try

                    myComm.Execute()

                    _ID = Convert.ToInt32(myComm.LastInsertID)

                    ComponentList.Update(Me)
                    CostList.Update(Me)

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

            If Not _CostList.Count > 0 AndAlso Not _ComponentList.Count > 0 Then _
                Throw New Exception(My.Resources.Goods_ProductionCalculation_ListsEmpty)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            CheckIfUpdateDateChanged()

            Dim myComm As New SQLCommand("UpdateProductionCalculation")
            AddWithParams(myComm)
            myComm.AddParam("?CD", _ID)

            Using transaction As New SqlTransaction

                Try

                    myComm.Execute()

                    ComponentList.Update(Me)
                    CostList.Update(Me)

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

            Dim myComm As New SQLCommand("DeleteProductionCalculationItems")
            myComm.AddParam("?CD", DirectCast(criteria, Criteria).ID)

            Using transaction As New SqlTransaction

                Try

                    myComm.Execute()

                    myComm = New SQLCommand("DeleteProductionCalculation")
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

            myComm.AddParam("?AA", CRound(_Amount, ROUNDAMOUNTGOODS))
            myComm.AddParam("?AB", _Date.Date)
            myComm.AddParam("?AC", ConvertDbBoolean(_IsObsolete))
            myComm.AddParam("?AD", _Goods.ID)
            myComm.AddParam("?AE", _Description.Trim)

            _UpdateDate = DateTime.Now
            _UpdateDate = New DateTime(Convert.ToInt64(Math.Floor(_UpdateDate.Ticks / TimeSpan.TicksPerSecond) _
                * TimeSpan.TicksPerSecond))
            If Not _ID > 0 Then _InsertDate = _UpdateDate

            myComm.AddParam("?AF", _UpdateDate.ToUniversalTime)

        End Sub

        Private Sub CheckIfUpdateDateChanged()

            Dim myComm As New SQLCommand("CheckIfProductionCalculationUpdateDateChanged")
            myComm.AddParam("?CD", _ID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 OrElse CDateTimeSafe(myData.Rows(0).Item(0), _
                    Date.MinValue) = Date.MinValue Then

                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                        My.Resources.Goods_ProductionCalculation_TypeName, _ID.ToString))

                End If

                If CTimeStampSafe(myData.Rows(0).Item(0)) <> _UpdateDate Then

                    Throw New Exception(My.Resources.Common_UpdateDateHasChanged)

                End If

            End Using

        End Sub

#End Region

    End Class

End Namespace