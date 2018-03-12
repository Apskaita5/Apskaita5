Namespace General

    ''' <summary>
    ''' Represents a shares discard (sub)operation (a person disposes of some of the company shares).
    ''' </summary>
    ''' <remarks>Can only be used as a child object for <see cref="General.SharesDiscardList">SharesDiscardList</see>
    ''' Data is stored in database table SharesOperationDetails.</remarks>
    <Serializable()>
    Public NotInheritable Class SharesDiscard
        Inherits BusinessBase(Of SharesDiscard)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _ShareHolder As PersonInfo = Nothing
        Private _Class As SharesClassInfo = Nothing
        Private _Amount As Double = 0
        Private _IsCancellation As Boolean = False
        Private _IsCompanyShares As Boolean = False
        Private _Remarks As String = ""


        ''' <summary>
        ''' Gets an ID of the shares (sub)operation that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Data is stored in database field SharesOperationDetails.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' |Gets or sets the person that disposes of the shares.
        ''' </summary>
        ''' <remarks>Use <see cref="HelperLists.PersonInfoList">PersonInfoList</see> for the datasource.
        ''' Data is stored in database field SharesOperationDetails.PersonID.</remarks>
        <PersonField(ValueRequiredLevel.Mandatory)>
        Public Property ShareHolder() As PersonInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ShareHolder
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As PersonInfo)
                CanWriteProperty(True)
                If _ShareHolder <> value Then
                    _ShareHolder = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the class of the shares discarded.
        ''' </summary>
        ''' <remarks>Use <see cref="HelperLists.SharesClassInfoList">SharesClassInfoList</see> for the datasource.
        ''' Data is stored in database field SharesOperationDetails.ClassID.</remarks>
        <SharesClassField(ValueRequiredLevel.Mandatory)>
        Public Property [Class]() As SharesClassInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Class
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As SharesClassInfo)
                CanWriteProperty(True)
                If _Class <> value Then
                    _Class = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of the shares discarded.
        ''' </summary>
        ''' <remarks>Data is stored in database field SharesOperationDetails.Amount.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)>
        Public Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_Amount)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_Amount) <> CRound(value) Then
                    _Amount = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether the shares are canceled, i.e. cease to exists, not transfered to someone.
        ''' </summary>
        ''' <remarks>Data is stored in database field SharesOperationDetails.IsInternal.</remarks>
        Public Property IsCancellation() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _IsCancellation
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _IsCancellation <> value Then
                    _IsCancellation = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether the person that disposes of the shares is the company itself.
        ''' </summary>
        ''' <remarks>Data is derived from the database field SharesOperationDetails.PersonID.
        ''' If the PersonID field is not <see cref="Person.ID">an actual person ID</see> (usualy equals to 0)
        ''' then the share holder is the company itself.</remarks>
        Public Property IsCompanyShares() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _IsCompanyShares
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _IsCompanyShares <> value Then
                    _IsCompanyShares = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets some arbitrary regards regarding the shares discarded.
        ''' </summary>
        ''' <remarks>Data is stored in database field SharesOperationDetails.Remarks.</remarks>
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


        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            If IsValid Then Return ""
            Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString,
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error))
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If BrokenRulesCollection.WarningCount < 1 Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString,
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning))
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            If _IsCompanyShares OrElse _ShareHolder = PersonInfo.Empty Then
                If _Class = SharesClassInfo.Empty Then
                    Return String.Format(My.Resources.General_SharesDiscard_ToString,
                        GetCurrentCompany.Name, GetCurrentCompany.Code, "", DblParser(0.0),
                        GetCurrentCompany.BaseCurrency, DblParser(_Amount))
                Else
                    Return String.Format(My.Resources.General_SharesDiscard_ToString,
                        GetCurrentCompany.Name, GetCurrentCompany.Code, _Class.Name,
                        DblParser(_Class.ValuePerUnit), GetCurrentCompany.BaseCurrency, DblParser(_Amount))
                End If
            Else
                If _Class = SharesClassInfo.Empty Then
                    Return String.Format(My.Resources.General_SharesDiscard_ToString,
                        _ShareHolder.Name, _ShareHolder.Code, "", DblParser(0.0),
                        GetCurrentCompany.BaseCurrency, DblParser(_Amount))
                Else
                    Return String.Format(My.Resources.General_SharesDiscard_ToString,
                        _ShareHolder.Name, _ShareHolder.Code, _Class.Name,
                        DblParser(_Class.ValuePerUnit), GetCurrentCompany.BaseCurrency, DblParser(_Amount))
                End If
            End If
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation,
                New Validation.RuleArgs("Remarks"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation,
                New Validation.RuleArgs("Amount"))
            ValidationRules.AddRule(AddressOf CommonValidation.ValueObjectFieldValidation,
                New Validation.RuleArgs("Class"))

            ValidationRules.AddRule(AddressOf ShareHolderValidation, New Validation.RuleArgs("ShareHolder"))
            ValidationRules.AddDependantProperty("IsCompanyShares", "ShareHolder", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring that a ShareHolder is set unless the shares are acquired by the company itself.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")>
        Private Shared Function ShareHolderValidation(ByVal target As Object,
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As SharesDiscard = DirectCast(target, SharesDiscard)

            If Not valObj._IsCompanyShares Then

                Return CommonValidation.PersonFieldValidation(target, e)

            ElseIf valObj._IsCompanyShares AndAlso valObj._ShareHolder <> PersonInfo.Empty Then

                e.Description = My.Resources.General_SharesDiscard_ShareHolderInvalid
                e.Severity = Validation.RuleSeverity.Error
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

        Friend Shared Function NewSharesDiscard() As SharesDiscard
            Dim result As New SharesDiscard
            result.ValidationRules.CheckRules()
            Return result
        End Function

        Friend Shared Function GetSharesDiscard(ByVal dr As DataRow) As SharesDiscard
            Return New SharesDiscard(dr)
        End Function

        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(dr As DataRow)
            MarkAsChild()
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(1), 0)
            _Amount = -CDblSafe(dr.Item(2), 2, 0)
            _IsCancellation = ConvertDbBoolean(CIntSafe(dr.Item(3), 0))
            _Remarks = CStrSafe(dr.Item(4)).Trim
            _Class = SharesClassInfo.GetSharesClassInfo(dr, 5)
            _ShareHolder = PersonInfo.GetPersonInfo(dr, 9)

            _IsCompanyShares = (_ShareHolder = PersonInfo.Empty)

            MarkOld()

        End Sub

        Friend Sub Insert(ByVal parent As SharesOperation)

            Dim myComm As New SQLCommand("InsertSharesOperationItem")
            myComm.AddParam("?AA", ConvertDatabaseID(SharesOperationType.Discard))
            myComm.AddParam("?AB", parent.ID)
            AddWithParams(myComm)

            myComm.Execute()

            _ID = Convert.ToInt32(myComm.LastInsertID)

            MarkOld()

        End Sub

        Friend Sub Update(ByVal parent As SharesOperation)

            Dim myComm As New SQLCommand("UpdateSharesOperationItem")
            myComm.AddParam("?CD", _ID)
            AddWithParams(myComm)

            myComm.Execute()

            MarkOld()
        End Sub

        Friend Sub DeleteSelf()

            Dim myComm As New SQLCommand("DeleteSharesOperationItem")
            myComm.AddParam("?CD", _ID)

            myComm.Execute()

            MarkNew()

        End Sub

        Private Sub AddWithParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AC", -CRound(_Amount))
            myComm.AddParam("?AD", ConvertDbBoolean(_IsCancellation))
            myComm.AddParam("?AE", _Remarks.Trim)
            If _Class = SharesClassInfo.Empty Then
                myComm.AddParam("?AF", 0)
            Else
                myComm.AddParam("?AF", _Class.ID)
            End If
            If _IsCompanyShares OrElse _ShareHolder = PersonInfo.Empty Then
                myComm.AddParam("?AG", 0)
            Else
                myComm.AddParam("?AG", _ShareHolder.ID)
            End If

        End Sub

#End Region

    End Class

End Namespace