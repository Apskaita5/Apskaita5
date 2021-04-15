Namespace General

    ''' <summary>
    ''' Represents a class of shares that is issued by the company (e.g. simple, priviledged, etc.).
    ''' </summary>
    ''' <remarks>Values are stored in the database table SharesClasses.</remarks>
    <Serializable()>
    Public NotInheritable Class SharesClass
        Inherits BusinessBase(Of SharesClass)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private _UniqueID As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _ValuePerUnit As Double = 0.0
        Private _Description As String = ""
        Private _SaftCode As CodeInfo
        Private _Account As Long = 0
        Private _IsInUse As Boolean = False


        ''' <summary>
        ''' Gets an ID of the shares class that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database table SharesClasses.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a name of the shares class. 
        ''' </summary>
        ''' <remarks>Value is stored in the database table SharesClasses.Name.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)>
        Public Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Name.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
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
        ''' Gets or sets a value of the shares class per unit.
        ''' </summary>
        ''' <remarks>Value is stored in the database table SharesClasses.ValuePerUnit.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)>
        Public Property ValuePerUnit As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_ValuePerUnit)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ValuePerUnit) <> CRound(value) Then
                    _ValuePerUnit = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a description of the shares class. 
        ''' </summary>
        ''' <remarks>Value is stored in the database table SharesClasses.Description.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255)>
        Public Property Description() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Description.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
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
        ''' Gets or sets an state approved code (SAF-T) of the shares class. 
        ''' </summary>
        ''' <remarks>Value is stored in the database table SharesClasses.SaftCode and
        ''' SharesClasses.SaftDescription.</remarks>
        <CodeAndNameField(ValueRequiredLevel.Recommended, Settings.CodeType.SaftSharesType)>
        Public Property SaftCode As CodeInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _SaftCode
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As CodeInfo)
                CanWriteProperty(True)
                If _SaftCode <> value Then
                    _SaftCode = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets an account that the shares capital is accounted in.
        ''' </summary>
        ''' <remarks>Value is stored in the database table SharesClasses.Account.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, False, 3)>
        Public Property Account As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Account
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If _Account <> value Then
                    _Account = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether the shares class is in use, i.e. there are shares operations using the class.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsInUse() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _IsInUse
            End Get
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
            Return _UniqueID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.General_SharesClass_ToString, _Name,
                DblParser(_ValuePerUnit), GetCurrentCompany.BaseCurrency)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation,
              New Csla.Validation.RuleArgs("ValuePerUnit"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation,
              New Csla.Validation.RuleArgs("Name"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation,
              New Csla.Validation.RuleArgs("Description"))
            ValidationRules.AddRule(AddressOf CommonValidation.CodeAndNameFieldValidation,
              New Csla.Validation.RuleArgs("SaftCode"))
            ValidationRules.AddRule(AddressOf CommonValidation.AccountFieldValidation,
              New Csla.Validation.RuleArgs("Account"))
        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function NewSharesClass() As SharesClass
            Dim result As New SharesClass
            result.ValidationRules.CheckRules()
            Return result
        End Function

        Friend Shared Function GetSharesClass(ByVal dr As DataRow) As SharesClass
            Return New SharesClass(dr)
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

            _ID = CIntSafe(dr.Item(0), 0)
            _Name = CStrSafe(dr.Item(1)).Trim
            _ValuePerUnit = CDblSafe(dr.Item(2), 2, 0.0)
            _Description = CStrSafe(dr.Item(3)).Trim
            Dim code As String = CStrSafe(dr.Item(4))
            If Not String.IsNullOrEmpty(code.Trim) Then
                _SaftCode = CodeInfo.GetCodeInfo(code, CStrSafe(dr.Item(5)), Settings.CodeType.SaftSharesType)
            End If
            _Account = CLongSafe(dr.Item(6), 0)
            _IsInUse = ConvertDbBoolean(CIntSafe(dr.Item(7), 0))

            MarkOld()

            ValidationRules.CheckRules()

        End Sub

        Friend Sub Insert(ByVal parent As SharesClassList)

            Dim myComm As New SQLCommand("InsertSharesClass")
            AddWithParams(myComm)

            myComm.Execute()

            _ID = Convert.ToInt32(myComm.LastInsertID)

            MarkOld()

        End Sub

        Friend Sub Update(ByVal parent As SharesClassList)

            Dim myComm As New SQLCommand("UpdateSharesClass")
            myComm.AddParam("?CD", _ID)
            AddWithParams(myComm)

            myComm.Execute()

            MarkOld()

        End Sub

        Friend Sub DeleteSelf()

            Dim myComm As New SQLCommand("DeleteSharesClass")
            myComm.AddParam("?CD", _ID)

            myComm.Execute()

            MarkNew()

        End Sub


        Private Sub AddWithParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AA", _Name.Trim)
            myComm.AddParam("?AB", CRound(_ValuePerUnit))
            myComm.AddParam("?AC", _Description.Trim)
            If _SaftCode = CodeInfo.Empty Then
                myComm.AddParam("?AD", "")
                myComm.AddParam("?AE", "")
            Else
                myComm.AddParam("?AD", _SaftCode.Code)
                myComm.AddParam("?AE", _SaftCode.Name)
            End If
            myComm.AddParam("?AF", _Account)

        End Sub


        Friend Sub CheckIfInUse()

            If IsNew Then Exit Sub

            Dim myComm As New SQLCommand("CheckIfSharesClassUsed")
            myComm.AddParam("?CD", _ID)

            Using myData As DataTable = myComm.Fetch
                If myData.Rows.Count < 1 Then
                    _IsInUse = False
                Else
                    _IsInUse = ConvertDbBoolean(CIntSafe(myData.Rows(0).Item(0), 0))
                End If
            End Using

        End Sub

#End Region

    End Class

End Namespace

'FetchSharesClass	SELECT S.ID, S.Name, S.Description FROM SharesClasss S	General.SharesClass
'InsertSharesClass	INSERT INTO SharesClasss(Name, Description) VALUES(?AA, ?AB);	General.SharesClass
'UpdateSharesClass	UPDATE SharesClasss SET Name=?AA, Description=?AB WHERE ID=?CD ;	General.SharesClass
'DeleteSharesClass	DELETE FROM SharesClass WHERE SharesClass.ID=?CD ;	General.SharesClass
