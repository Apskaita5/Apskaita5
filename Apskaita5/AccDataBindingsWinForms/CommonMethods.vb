Imports System.Windows.Forms
Imports AccControlsWinForms
Imports System.Reflection
Imports ApskaitaObjects.Attributes
Imports ApskaitaObjects.HelperLists
Imports AccDataBindingsWinForms.CachedInfoLists

Public Module CommonMethods

    Private _CurrentMdiParent As Form = Nothing
    Private ReadOnly _InstanceGuid As Guid = Guid.NewGuid()


    ''' <summary>
    ''' Gets or sets the current mdi parent form.
    ''' </summary>
    ''' <remarks>Needs to be set at startup.</remarks>
    Public Property CurrentMdiParent() As Form
        Get
            Return _CurrentMdiParent
        End Get
        Set(ByVal value As Form)
            _CurrentMdiParent = value
        End Set
    End Property

    ''' <summary>
    ''' Gets a Guid of the current application instance.
    ''' </summary>
    ''' <remarks>Needs to be set at startup.</remarks>
    Public ReadOnly Property InstanceGuid() As Guid
        Get
            Return _InstanceGuid
        End Get
    End Property


#Region " Form Management "

    Private _BusinessObjectsViews As Dictionary(Of Type, Type)


    Private Sub InitializeBusinessObjectsViews()

        _BusinessObjectsViews = New Dictionary(Of Type, Type)

        _BusinessObjectsViews.Add(GetType(General.AccountList), GetType(F_AccountList))
        _BusinessObjectsViews.Add(GetType(General.ClosingEntriesCommand), GetType(F_ClosingEntriesCommand))
        _BusinessObjectsViews.Add(GetType(General.Company), GetType(F_Company))
        _BusinessObjectsViews.Add(GetType(General.CompanyRegionalData), GetType(F_CompanyRegionalData))
        _BusinessObjectsViews.Add(GetType(General.ImportedPersonList), GetType(F_ImportedPersonList))
        _BusinessObjectsViews.Add(GetType(General.JournalEntry), GetType(F_JournalEntry))
        _BusinessObjectsViews.Add(GetType(General.Person), GetType(F_Person))
        _BusinessObjectsViews.Add(GetType(General.PersonGroupList), GetType(F_PersonGroupList))
        _BusinessObjectsViews.Add(GetType(General.TemplateJournalEntry), GetType(F_TemplateJournalEntry))
        _BusinessObjectsViews.Add(GetType(General.TransferOfBalance), GetType(F_TransferOfBalance))
        _BusinessObjectsViews.Add(GetType(ActiveReports.BookEntryInfoListParent), GetType(F_BookEntryInfoListParent))
        _BusinessObjectsViews.Add(GetType(ActiveReports.DebtInfoList), GetType(F_DebtInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.FinancialStatementsInfo), GetType(F_FinancialStatementsInfo))
        _BusinessObjectsViews.Add(GetType(ActiveReports.JournalEntryInfoList), GetType(F_JournalEntryInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.PersonInfoItemList), GetType(F_PersonInfoItemList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.UnsettledPersonInfoList), GetType(F_UnsettledPersonInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.Declaration), GetType(F_Declaration))
        _BusinessObjectsViews.Add(GetType(ActiveReports.ImprestSheetInfoList), GetType(F_ImprestSheetInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.ContractInfoList), GetType(F_ContractInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.PayOutNaturalPersonInfoList), GetType(F_PayOutNaturalPersonInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.WageSheetInfoList), GetType(F_WageSheetInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.WorkerHolidayInfo), GetType(F_WorkerHolidayInfo))
        _BusinessObjectsViews.Add(GetType(ActiveReports.WorkerWageInfoReport), GetType(F_WorkerWageInfoReport))
        _BusinessObjectsViews.Add(GetType(ActiveReports.WorkersVDUInfo), GetType(F_WorkersVDUInfo))
        _BusinessObjectsViews.Add(GetType(ActiveReports.WorkTimeSheetInfoList), GetType(F_WorkTimeSheetInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.AdvanceReportInfoList), GetType(F_AdvanceReportInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.CashOperationInfoList), GetType(F_CashOperationInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.InvoiceInfoItemList), GetType(F_InvoiceInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.GoodsOperationInfoListParent), GetType(F_GoodsOperationInfoListParent))
        _BusinessObjectsViews.Add(GetType(ActiveReports.ProductionCalculationItemList), GetType(F_ProductionCalculationItemList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.GoodsTurnoverInfoList), GetType(F_GoodsTurnoverInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.ServiceTurnoverInfoList), GetType(F_ServiceTurnoverInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.LongTermAssetComplexDocumentInfoList), GetType(F_LongTermAssetComplexDocumentInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.LongTermAssetOperationInfoListParent), GetType(F_LongTermAssetOperationInfoListParent))
        _BusinessObjectsViews.Add(GetType(ActiveReports.LongTermAssetInfoList), GetType(F_LongTermAssetInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.VatDeclaration), GetType(F_VatDeclaration))
        _BusinessObjectsViews.Add(GetType(HelperLists.IndirectRelationInfoList), GetType(F_IndirectRelationInfoList))
        _BusinessObjectsViews.Add(GetType(HelperLists.TemplateJournalEntryInfoList), GetType(F_TemplateJournalEntryInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.ServiceInfoItemList), GetType(F_ServiceInfoList))
        _BusinessObjectsViews.Add(GetType(ActiveReports.VatDeclarationSchemaInfoItemList), GetType(F_VatDeclarationSchemaInfoItemList))
        _BusinessObjectsViews.Add(GetType(ApskaitaObjects.Settings.DocumentSerialList), GetType(F_DocumentSerialList))
        _BusinessObjectsViews.Add(GetType(ApskaitaObjects.Settings.CommonSettings), GetType(F_GeneralSettings))
        _BusinessObjectsViews.Add(GetType(Workers.ImprestSheet), GetType(F_ImprestSheet))
        _BusinessObjectsViews.Add(GetType(Workers.HolidayPayReserve), GetType(F_HolidayPayReserve))
        _BusinessObjectsViews.Add(GetType(Workers.Contract), GetType(F_Contract))
        _BusinessObjectsViews.Add(GetType(Workers.ContractUpdate), GetType(F_ContractUpdate))
        _BusinessObjectsViews.Add(GetType(Workers.PayOutNaturalPerson), GetType(F_PayOutNaturalPerson))
        _BusinessObjectsViews.Add(GetType(Workers.WageSheet), GetType(F_WageSheet))
        _BusinessObjectsViews.Add(GetType(Workers.WorkTimeClassList), GetType(F_WorkTimeClassList))
        _BusinessObjectsViews.Add(GetType(Workers.WorkTimeSheet), GetType(F_WorkTimeSheet))
        _BusinessObjectsViews.Add(GetType(Documents.AccumulativeCosts), GetType(F_AccumulativeCosts))
        _BusinessObjectsViews.Add(GetType(Documents.AdvanceReport), GetType(F_AdvanceReport))
        _BusinessObjectsViews.Add(GetType(Documents.BankOperation), GetType(F_BankOperation))
        _BusinessObjectsViews.Add(GetType(Documents.BankOperationItemList), GetType(F_BankOperationItemList))
        _BusinessObjectsViews.Add(GetType(ImportedBankOperation), GetType(F_BankOperation))
        _BusinessObjectsViews.Add(GetType(Documents.CashAccountList), GetType(F_CashAccountList))
        _BusinessObjectsViews.Add(GetType(Documents.InvoiceMade), GetType(F_InvoiceMade))
        _BusinessObjectsViews.Add(GetType(Documents.InvoiceReceived), GetType(F_InvoiceReceived))
        _BusinessObjectsViews.Add(GetType(Documents.Offset), GetType(F_Offset))
        _BusinessObjectsViews.Add(GetType(Documents.Service), GetType(F_Service))
        _BusinessObjectsViews.Add(GetType(Documents.TillIncomeOrder), GetType(F_TillIncomeOrder))
        _BusinessObjectsViews.Add(GetType(Documents.TillSpendingsOrder), GetType(F_TillSpendingsOrder))
        _BusinessObjectsViews.Add(GetType(Documents.VatDeclarationSchema), GetType(F_VatDeclarationSchema))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsComplexOperationDiscard), GetType(F_GoodsComplexOperationDiscard))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsComplexOperationInternalTransfer), GetType(F_GoodsComplexOperationInternalTransfer))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsComplexOperationInventorization), GetType(F_GoodsComplexOperationInventorization))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsComplexOperationPriceCut), GetType(F_GoodsComplexOperationPriceCut))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsComplexOperationProduction), GetType(F_GoodsComplexOperationProduction))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsComplexOperationTransferOfBalance), GetType(F_GoodsComplexOperationTransferOfBalance))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsGroupList), GetType(F_GoodsGroupList))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsItem), GetType(F_GoodsItem))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsOperationAccountChange), GetType(F_GoodsOperationAccountChange))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsOperationAcquisition), GetType(F_GoodsOperationAcquisition))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsOperationAdditionalCosts), GetType(F_GoodsOperationAdditionalCosts))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsOperationDiscard), GetType(F_GoodsOperationDiscard))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsOperationDiscount), GetType(F_GoodsOperationDiscount))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsOperationPriceCut), GetType(F_GoodsOperationPriceCut))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsOperationRedeemFromBuyer), GetType(F_GoodsOperationRedeemFromBuyer))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsOperationTransfer), GetType(F_GoodsOperationTransfer))
        _BusinessObjectsViews.Add(GetType(Goods.GoodsOperationValuationMethod), GetType(F_GoodsOperationValuationMethod))
        _BusinessObjectsViews.Add(GetType(Goods.ImportedGoodsItemList), GetType(F_ImportedGoodsItemList))
        _BusinessObjectsViews.Add(GetType(Goods.ProductionCalculation), GetType(F_ProductionCalculation))
        _BusinessObjectsViews.Add(GetType(Goods.WarehouseList), GetType(F_WarehouseList))
        _BusinessObjectsViews.Add(GetType(Assets.LongTermAsset), GetType(F_LongTermAsset))
        _BusinessObjectsViews.Add(GetType(Assets.LongTermAssetCustomGroupList), GetType(F_LongTermAssetCustomGroupList))
        _BusinessObjectsViews.Add(GetType(Assets.OperationAmortization), GetType(F_OperationAmortization))
        _BusinessObjectsViews.Add(GetType(Assets.OperationAccountChange), GetType(F_OperationAccountChange))
        _BusinessObjectsViews.Add(GetType(Assets.OperationAcquisitionValueIncrease), GetType(F_OperationAcquisitionValueIncrease))
        _BusinessObjectsViews.Add(GetType(Assets.OperationAmortizationPeriodChange), GetType(F_OperationAmortizationPeriodChange))
        _BusinessObjectsViews.Add(GetType(Assets.OperationDiscard), GetType(F_OperationDiscard))
        _BusinessObjectsViews.Add(GetType(Assets.OperationOperationalStatusChange), GetType(F_OperationOperationalStatusChange))
        _BusinessObjectsViews.Add(GetType(Assets.OperationTransfer), GetType(F_OperationTransfer))
        _BusinessObjectsViews.Add(GetType(Assets.OperationValueChange), GetType(F_OperationValueChange))
        _BusinessObjectsViews.Add(GetType(Assets.ComplexOperationAmortization), GetType(F_ComplexOperationAmortization))
        _BusinessObjectsViews.Add(GetType(Assets.ComplexOperationDiscard), GetType(F_ComplexOperationDiscard))
        _BusinessObjectsViews.Add(GetType(Assets.ComplexOperationOperationalStatusChange), GetType(F_ComplexOperationOperationalStatusChange))
        _BusinessObjectsViews.Add(GetType(Assets.ComplexOperationValueChange), GetType(F_ComplexOperationValueChange))
        _BusinessObjectsViews.Add(GetType(Assets.LongTermAssetsTransferOfBalance), GetType(F_LongTermAssetsTransferOfBalance))
        _BusinessObjectsViews.Add(GetType(AccDataBindingsWinForms.Settings.LocalSettings), GetType(F_SettingsLocal))
        _BusinessObjectsViews.Add(GetType(InvoiceInfo.ClientInfo), GetType(F_Person))

    End Sub

    Friend Function GetFormForBusinessType(ByVal businessType As Type, _
        ByVal ParamArray params As Object()) As Form

        If businessType Is Nothing Then
            Throw New ArgumentNullException("businessType")
        End If

        If _BusinessObjectsViews Is Nothing Then
            InitializeBusinessObjectsViews()
        End If

        If Not _BusinessObjectsViews.ContainsKey(businessType) Then
            Throw New ArgumentException(String.Format("Klaida. Nežinoma kokia forma reikėtų naudoti tipui {0}.", _
                businessType.FullName), "businessType")
        End If

        If params Is Nothing OrElse params.Length < 1 Then
            Return DirectCast(Activator.CreateInstance(_BusinessObjectsViews(businessType)), Form)
        Else
            Return DirectCast(Activator.CreateInstance(_BusinessObjectsViews(businessType), _
                params), Form)
        End If

    End Function


    ''' <summary>
    ''' Opens a new form that manages business objects (editable or report) of type T.
    ''' </summary>
    ''' <typeparam name="T">a type of business objects (editable or report)
    ''' to open a form for</typeparam>
    ''' <remarks></remarks>
    Public Sub OpenNewForm(Of T)()
        OpenNewForm(GetType(T))
    End Sub

    ''' <summary>
    ''' Opens a new form that manages business objects (editable or report) of type T.
    ''' </summary>
    ''' <param name="businessType">a type of business objects (editable or report)
    ''' to open a form for</param>
    ''' <remarks></remarks>
    Public Sub OpenNewForm(ByVal businessType As Type)
        If businessType Is Nothing Then
            Throw New ArgumentNullException("businessType")
        End If
        Dim frm As Form = GetFormForBusinessType(businessType)
        OpenForm(frm)
    End Sub


    ''' <summary>
    ''' Opens an editable business object in appropriate form.
    ''' </summary>
    ''' <param name="obj">a business object to edit</param>
    ''' <remarks></remarks>
    Public Sub OpenObjectEditForm(ByVal obj As Object)

        If obj Is Nothing Then
            Throw New ArgumentNullException("obj")
        End If

        Dim frm As Form = GetFormForBusinessType(obj.GetType(), obj)
        OpenForm(frm)

    End Sub

    ''' <summary>
    ''' An overload for a CslaActionExtenderQueryObject when a fetched object
    ''' should be immediately opened in it's editing form.
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="exceptionHandled"></param>
    ''' <remarks></remarks>
    Friend Sub OpenObjectEditForm(ByVal obj As Object, ByVal exceptionHandled As Boolean)
        If exceptionHandled OrElse obj Is Nothing Then Exit Sub
        OpenObjectEditForm(obj)
    End Sub

    ''' <summary>
    ''' Fetches a business object by it's (encapsulated) journal entry type 
    ''' and opens it in appropriate form.
    ''' </summary>
    ''' <param name="queryManager">a CslaActionExtenderQueryObject instance
    ''' to use for progress visualization</param>
    ''' <param name="journalEntryID">an <see cref="General.JournalEntry.ID">ID
    ''' of the JournalEntry</see> to fetch</param>
    ''' <param name="docType">a type of the JournalEntry</param>
    ''' <remarks></remarks>
    Public Sub OpenObjectEditForm(ByVal queryManager As CslaActionExtenderQueryObject, _
        ByVal journalEntryID As Integer, ByVal docType As DocumentType)

        If docType = DocumentType.None Then
            'General.JournalEntry.GetJournalEntry(journalEntryID)
            queryManager.InvokeQuery(Of General.JournalEntry)(Nothing, "GetJournalEntry", True, _
                AddressOf OpenObjectEditForm, journalEntryID)
        ElseIf docType = DocumentType.TransferOfBalance Then
            OpenNewForm(Of General.TransferOfBalance)()
        ElseIf docType = DocumentType.AdvanceReport Then
            ' Documents.AdvanceReport.GetAdvanceReport(journalEntryID)
            queryManager.InvokeQuery(Of Documents.AdvanceReport)(Nothing, "GetAdvanceReport", True, _
                AddressOf OpenObjectEditForm, journalEntryID)
        ElseIf docType = DocumentType.AccumulatedCosts Then
            ' Documents.AccumulativeCosts.GetAccumulativeCosts(journalEntryID)
            queryManager.InvokeQuery(Of Documents.AccumulativeCosts)(Nothing, "GetAccumulativeCosts", True, _
                AddressOf OpenObjectEditForm, journalEntryID)
        ElseIf docType = DocumentType.BankOperation Then
            ' Documents.BankOperation.GetBankOperation(journalEntryID)
            queryManager.InvokeQuery(Of Documents.BankOperation)(Nothing, "GetBankOperation", True, _
                AddressOf OpenObjectEditForm, journalEntryID)
        ElseIf docType = DocumentType.ClosingEntries Then
            MsgBox("Klaida. Sąskaitų uždarymo operacija gali būti tik pašalinama " & _
                "ir įvedama iš naujo, o ne redaguojama.", MsgBoxStyle.Exclamation, "Klaida")
        ElseIf docType = DocumentType.GoodsAccountChange OrElse docType = DocumentType.GoodsInternalTransfer _
            OrElse docType = DocumentType.GoodsInventorization OrElse docType = DocumentType.GoodsProduction _
            OrElse docType = DocumentType.GoodsRevalue OrElse docType = DocumentType.GoodsWriteOff Then
            ' GoodsOperationManager.GetGoodsOperation(journalEntryID, docType)
            queryManager.InvokeQuery(Of GoodsOperationManager)(Nothing, "GetGoodsOperation", True, _
                AddressOf OpenObjectEditForm, journalEntryID, docType)
        ElseIf docType = DocumentType.HolidayReserve Then
            ' Workers.HolidayPayReserve.GetHolidayPayReserve(journalEntryID)
            queryManager.InvokeQuery(Of Workers.HolidayPayReserve)(Nothing, "GetHolidayPayReserve", True, _
                AddressOf OpenObjectEditForm, journalEntryID)
        ElseIf docType = DocumentType.ImprestSheet Then
            'Workers.ImprestSheet.GetImprestSheet(journalEntryID)
            queryManager.InvokeQuery(Of Workers.ImprestSheet)(Nothing, "GetImprestSheet", True, _
                AddressOf OpenObjectEditForm, journalEntryID)
        ElseIf docType = DocumentType.InvoiceMade Then
            'Documents.InvoiceMade.GetInvoiceMade(journalEntryID)
            queryManager.InvokeQuery(Of Documents.InvoiceMade)(Nothing, "GetInvoiceMade", True, _
                AddressOf OpenObjectEditForm, journalEntryID)
        ElseIf docType = DocumentType.InvoiceReceived Then
            'Documents.InvoiceReceived.GetInvoiceReceived(journalEntryID)
            queryManager.InvokeQuery(Of Documents.InvoiceReceived)(Nothing, "GetInvoiceReceived", True, _
                AddressOf OpenObjectEditForm, journalEntryID)
        ElseIf docType = DocumentType.Offset Then
            'Documents.Offset.GetOffset(journalEntryID)
            queryManager.InvokeQuery(Of Documents.Offset)(Nothing, "GetOffset", True, _
                AddressOf OpenObjectEditForm, journalEntryID)
        ElseIf docType = DocumentType.TillIncomeOrder Then
            'Documents.TillIncomeOrder.GetTillIncomeOrder(journalEntryID)
            queryManager.InvokeQuery(Of Documents.TillIncomeOrder)(Nothing, "GetTillIncomeOrder", True, _
                AddressOf OpenObjectEditForm, journalEntryID)
        ElseIf docType = DocumentType.TillSpendingOrder Then
            'Documents.TillSpendingsOrder.GetTillSpendingsOrder(journalEntryID)
            queryManager.InvokeQuery(Of Documents.TillSpendingsOrder)(Nothing, "GetTillSpendingsOrder", True, _
                AddressOf OpenObjectEditForm, journalEntryID)
        ElseIf docType = DocumentType.WageSheet Then
            'Workers.WageSheet.GetWageSheet(journalEntryID)
            queryManager.InvokeQuery(Of Workers.WageSheet)(Nothing, "GetWageSheet", True, _
                AddressOf OpenObjectEditForm, journalEntryID)
        ElseIf docType = DocumentType.LongTermAssetAccountChange OrElse docType = DocumentType.Amortization _
            OrElse docType = DocumentType.LongTermAssetDiscard Then
            'AssetOperationManager.GetAssetOperation(journalEntryID, docType)
            queryManager.InvokeQuery(Of AssetOperationManager)(Nothing, "GetAssetOperation", True, _
                AddressOf OpenObjectEditForm, journalEntryID, docType)

        Else
            Throw New NotImplementedException(String.Format("Klaida. Dokumento tipas '{0}' neimplementuotas metode OpenObjectEditForm.", _
                docType.ToString))

        End If

    End Sub


    ''' <summary>
    ''' Checks if a business object of type T with the given id is currently
    ''' open in any edit form.
    ''' </summary>
    ''' <typeparam name="T">a type of the business object</typeparam>
    ''' <param name="id">an ID of the business object</param>
    ''' <param name="showcannotDeleteMessage">whether to show a 'cannot delete' message
    ''' if there is an open edit form for the business object</param>
    ''' <param name="activateForm">whether to activate the open edit form 
    ''' for the business object if found</param>
    ''' <remarks></remarks>
    Public Function CheckIfObjectEditFormOpen(Of T)(ByVal id As Integer, _
        ByVal showcannotDeleteMessage As Boolean, ByVal activateForm As Boolean) As Boolean

        If CurrentMdiParent Is Nothing Then Return False

        For Each frm As Form In CurrentMdiParent.MdiChildren
            If TypeOf frm Is IObjectEditForm AndAlso DirectCast(frm, IObjectEditForm).ObjectID = id _
                AndAlso DirectCast(frm, IObjectEditForm).ObjectType Is GetType(T) Then

                If showcannotDeleteMessage Then
                    MsgBox("Negalima pašalinti duomenų, kol jie yra redaguojami. Uždarykite redagavimo formą.", _
                        MsgBoxStyle.Exclamation, "Klaida")
                End If
                If activateForm Then
                    frm.Activate()
                End If

                Return True

            End If
        Next

        Return False

    End Function


    ''' <summary>
    ''' Shows the form specified.
    ''' If the form is ISingleInstanceForm, then first checks if the form of the 
    ''' same type is open (activates the form if already open).
    ''' If the form is IObjectEditForm, then first checks if the form containing
    ''' the same business object is open (activates the form if already open).
    ''' </summary>
    ''' <param name="frm">a form to show</param>
    ''' <remarks></remarks>
    Public Sub OpenForm(ByVal frm As Form)

        If frm Is Nothing Then
            Throw New ArgumentNullException("frm")
        End If

        If Not CurrentMdiParent Is Nothing Then

            Dim singleInstance As Boolean = (Not Array.IndexOf(frm.GetType.GetInterfaces(), _
                GetType(ISingleInstanceForm)) < 0)
            Dim editForm As Boolean = (Not Array.IndexOf(frm.GetType.GetInterfaces(), _
                GetType(IObjectEditForm)) < 0)
            If editForm AndAlso DirectCast(frm, IObjectEditForm).ObjectID = Integer.MinValue Then
                editForm = False
            End If

            If singleInstance OrElse editForm Then

                For Each f As Form In CurrentMdiParent.MdiChildren

                    If f.GetType() Is frm.GetType() Then

                        If singleInstance OrElse DirectCast(frm, IObjectEditForm).ObjectID _
                            = DirectCast(f, IObjectEditForm).ObjectID Then
                            f.Activate()
                            frm.Dispose()
                            Exit Sub
                        End If

                    End If

                Next

            End If

        End If

        frm.MdiParent = CurrentMdiParent

        frm.Show()

    End Sub

#End Region

#Region " Setup Default Controls For BindingSource "

    ''' <summary>
    ''' Initializes default control bindings and formats:
    ''' - sets AccTextBox, NumericUpDown, TextBox formats;
    ''' - loads AccountInfoList, CashAccountInfoList, PersonGroupInfoList
    ''' and WarehouseInfoList to AccListBox controls that are bound to the
    ''' properties of appropriate types (marked with AccountFieldAttribute 
    ''' in case of AccountInfoList);
    ''' - sets controls readonly/enabled status depending on whether the property is readonly.
    ''' </summary>
    ''' <typeparam name="T">a type of the object that the controls are bound to</typeparam>
    ''' <param name="targetForm">a form that contains the controls</param>
    ''' <param name="datasource">a datasource (BindingSource) that the controls are bound to</param>
    ''' <remarks></remarks>
    Public Sub SetupDefaultControls(Of T)(ByVal targetForm As Control, _
        ByVal datasource As Object)
        If Not targetForm.Controls Is Nothing Then
            For Each ctrl As Control In targetForm.Controls
                SetupDefaultControl(Of T)(ctrl, datasource)
                If ctrl.Controls.Count > 0 Then SetupDefaultControls(Of T)(ctrl, datasource)
            Next
        End If
    End Sub

    Private Sub SetupDefaultControl(Of T)(ByVal ctrl As Control, ByVal datasource As Object)

        Dim propInfo As PropertyInfo = GetBindingProperty(Of T)(ctrl, datasource)

        If propInfo Is Nothing Then Exit Sub

        If TypeOf ctrl Is AccTextBox Then

            DirectCast(ctrl, AccTextBox).ReadOnly = Not propInfo.CanWrite
            DirectCast(ctrl, AccTextBox).TextAlign = HorizontalAlignment.Center
            DirectCast(ctrl, AccTextBox).KeepBackColorWhenReadOnly = False

            If Not GetAttribute(Of DoubleFieldAttribute)(propInfo) Is Nothing Then

                Dim attribute As DoubleFieldAttribute = GetAttribute(Of DoubleFieldAttribute)(propInfo)

                DirectCast(ctrl, AccTextBox).NegativeValue = attribute.AllowNegative
                DirectCast(ctrl, AccTextBox).DecimalLength = attribute.Round

            ElseIf Not GetAttribute(Of IntegerFieldAttribute)(propInfo) Is Nothing Then

                Dim attribute As IntegerFieldAttribute = GetAttribute(Of IntegerFieldAttribute)(propInfo)

                DirectCast(ctrl, AccTextBox).NegativeValue = attribute.AllowNegative
                DirectCast(ctrl, AccTextBox).DecimalLength = 0

            End If

        ElseIf TypeOf ctrl Is TextBox Then

            DirectCast(ctrl, TextBox).ReadOnly = Not propInfo.CanWrite

            If Not GetAttribute(Of StringFieldAttribute)(propInfo) Is Nothing Then

                Dim attribute As StringFieldAttribute = GetAttribute(Of StringFieldAttribute)(propInfo)

                DirectCast(ctrl, TextBox).MaxLength = attribute.MaxLength

            ElseIf propInfo.PropertyType Is GetType(Integer) OrElse _
                propInfo.PropertyType Is GetType(Long) OrElse _
                propInfo.PropertyType Is GetType(Byte) OrElse _
                propInfo.PropertyType Is GetType(Date) OrElse _
                propInfo.PropertyType Is GetType(DateTime) Then

                DirectCast(ctrl, TextBox).TextAlign = HorizontalAlignment.Center

            End If

        ElseIf TypeOf ctrl Is NumericUpDown Then

            If propInfo.CanWrite Then
                DirectCast(ctrl, NumericUpDown).ReadOnly = False
                DirectCast(ctrl, NumericUpDown).Increment = 1
            Else
                DirectCast(ctrl, NumericUpDown).ReadOnly = True
                DirectCast(ctrl, NumericUpDown).Increment = 0
            End If

            DirectCast(ctrl, NumericUpDown).DecimalPlaces = 0
            DirectCast(ctrl, NumericUpDown).TextAlign = HorizontalAlignment.Center

            If Not GetAttribute(Of IntegerFieldAttribute)(propInfo) Is Nothing Then

                Dim attribute As IntegerFieldAttribute = GetAttribute(Of IntegerFieldAttribute)(propInfo)

                If attribute.WithinRange Then
                    DirectCast(ctrl, NumericUpDown).Maximum = attribute.MaxValue
                    DirectCast(ctrl, NumericUpDown).Minimum = attribute.MinValue
                ElseIf attribute.AllowNegative Then
                    DirectCast(ctrl, NumericUpDown).Maximum = 1000000
                    DirectCast(ctrl, NumericUpDown).Minimum = -1000000
                Else
                    DirectCast(ctrl, NumericUpDown).Maximum = 1000000
                    DirectCast(ctrl, NumericUpDown).Minimum = 0
                End If

            Else

                DirectCast(ctrl, NumericUpDown).Maximum = 1000000
                DirectCast(ctrl, NumericUpDown).Minimum = -1000000

            End If

        ElseIf TypeOf ctrl Is AccListComboBox Then

            DirectCast(ctrl, AccListComboBox).Enabled = propInfo.CanWrite

            If Not GetAttribute(Of AccountFieldAttribute)(propInfo) Is Nothing Then

                Dim attribute As AccountFieldAttribute = GetAttribute(Of AccountFieldAttribute)(propInfo)

                LoadAccountInfoListToListCombo(DirectCast(ctrl, AccListComboBox), _
                    Not attribute.ValueRequired, attribute.AcceptedClasses)

            ElseIf propInfo.PropertyType Is GetType(CashAccountInfo) Then

                LoadCashAccountInfoListToListCombo(DirectCast(ctrl, AccListComboBox), True)

            ElseIf propInfo.PropertyType Is GetType(PersonGroupInfo) Then

                LoadPersonGroupInfoListToListCombo(DirectCast(ctrl, AccListComboBox))

            ElseIf propInfo.PropertyType Is GetType(WarehouseInfo) Then

                LoadWarehouseInfoListToListCombo(DirectCast(ctrl, AccListComboBox), True)

            End If

        Else

            Try
                DirectCast(ctrl, Object).IsReadOnly = Not propInfo.CanWrite
            Catch ex As Exception
                ctrl.Enabled = propInfo.CanWrite
            End Try

        End If

    End Sub

    Private Function GetBindingProperty(Of T)(ByVal ctrl As Control, ByVal datasource As Object) As PropertyInfo

        For Each binding As Binding In ctrl.DataBindings

            If binding.DataSource Is datasource Then

                If (TypeOf ctrl Is TextBox AndAlso binding.PropertyName.ToLower = "text") _
                    OrElse (TypeOf ctrl Is AccTextBox AndAlso binding.PropertyName.ToLower = "decimalvalue") _
                    OrElse (TypeOf ctrl Is AccListComboBox AndAlso binding.PropertyName.ToLower = "selectedvalue") _
                    OrElse (TypeOf ctrl Is DateTimePicker AndAlso binding.PropertyName.ToLower = "value") _
                    OrElse (TypeOf ctrl Is NumericUpDown AndAlso binding.PropertyName.ToLower = "value") _
                    OrElse (TypeOf ctrl Is ComboBox AndAlso binding.PropertyName.ToLower = "text") Then

                    Return GetBindingProperty(Of T)(binding.BindingMemberInfo.BindingMember)

                End If

            End If

        Next

        Return Nothing

    End Function

#End Region

#Region " Invoice Adapters "

    Private _InvoiceAdapterTypes As List(Of TypeItem)

    ''' <summary>
    ''' Gets a list of the available invoice adapters (operations that could be attached to an invoice).
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetInvoiceAdapterTypes() As List(Of TypeItem)
        If _InvoiceAdapterTypes Is Nothing Then InitializeInvoiceAdapterTypes()
        Return _InvoiceAdapterTypes
    End Function

    Private Sub InitializeInvoiceAdapterTypes()

        _InvoiceAdapterTypes = New List(Of TypeItem)

        _InvoiceAdapterTypes.Add(New TypeItem(GetType(Documents.InvoiceAdapters.AssetAcquisitionInvoiceAdapter), _
            CommonValidation.GetResourcesTypeName(GetType(Documents.InvoiceAdapters.AssetAcquisitionInvoiceAdapter))))
        _InvoiceAdapterTypes.Add(New TypeItem(GetType(Documents.InvoiceAdapters.AssetSaleInvoiceAdapter), _
            CommonValidation.GetResourcesTypeName(GetType(Documents.InvoiceAdapters.AssetSaleInvoiceAdapter))))
        _InvoiceAdapterTypes.Add(New TypeItem(GetType(Documents.InvoiceAdapters.AssetAcquisitionValueIncreaseInvoiceAdapter), _
            CommonValidation.GetResourcesTypeName(GetType(Documents.InvoiceAdapters.AssetAcquisitionValueIncreaseInvoiceAdapter))))
        _InvoiceAdapterTypes.Add(New TypeItem(GetType(Documents.InvoiceAdapters.ServiceInvoiceAdapter), _
            CommonValidation.GetResourcesTypeName(GetType(Documents.InvoiceAdapters.ServiceInvoiceAdapter))))
        _InvoiceAdapterTypes.Add(New TypeItem(GetType(Documents.InvoiceAdapters.GoodsAcquisitionInvoiceAdapter), _
            CommonValidation.GetResourcesTypeName(GetType(Documents.InvoiceAdapters.GoodsAcquisitionInvoiceAdapter))))
        _InvoiceAdapterTypes.Add(New TypeItem(GetType(Documents.InvoiceAdapters.GoodsSaleInvoiceAdapter), _
            CommonValidation.GetResourcesTypeName(GetType(Documents.InvoiceAdapters.GoodsSaleInvoiceAdapter))))
        _InvoiceAdapterTypes.Add(New TypeItem(GetType(Documents.InvoiceAdapters.GoodsAddedCostsInvoiceAdapter), _
            CommonValidation.GetResourcesTypeName(GetType(Documents.InvoiceAdapters.GoodsAddedCostsInvoiceAdapter))))
        _InvoiceAdapterTypes.Add(New TypeItem(GetType(Documents.InvoiceAdapters.GoodsDiscountInvoiceAdapter), _
            CommonValidation.GetResourcesTypeName(GetType(Documents.InvoiceAdapters.GoodsDiscountInvoiceAdapter))))
        _InvoiceAdapterTypes.Add(New TypeItem(GetType(Documents.InvoiceAdapters.GoodsRedeemFromBuyerInvoiceAdapter), _
            CommonValidation.GetResourcesTypeName(GetType(Documents.InvoiceAdapters.GoodsRedeemFromBuyerInvoiceAdapter))))

    End Sub

    ''' <summary>
    ''' Gets a new invoice adapter of the requested type by requesting the user params required
    ''' </summary>
    ''' <param name="adapterType">a type of the adapter to create (must implement IInvoiceAdapter interface)</param>
    ''' <param name="forInvoiceReceived">whether the adapter should be created for an InvoiceReceived
    ''' (otherwise - for InvoiceMade)</param>
    ''' <param name="parentChronologyValidator">a chronologic validator of the invoice that the
    ''' new adapter is ment for</param>
    ''' <remarks></remarks>
    Public Function GetNewInvoiceAdapter(ByVal adapterType As Type, ByVal forInvoiceReceived As Boolean, _
        ByVal parentChronologyValidator As IChronologicValidator) As Documents.InvoiceAdapters.IInvoiceAdapter

        If adapterType Is Nothing Then
            Throw New ArgumentNullException("adapterType")
        ElseIf Array.IndexOf(adapterType.GetInterfaces(), GetType(Documents.InvoiceAdapters.IInvoiceAdapter)) < 0 Then
            Throw New ArgumentException(String.Format("Klaida. Tipas {0} neimplementuoja IInvoiceAdpter interfeiso.", _
                adapterType.FullName), "adapterType")
        End If

        If adapterType Is GetType(Documents.InvoiceAdapters.ServiceInvoiceAdapter) Then

            Using frm As New F_NewInvoiceAdapterForServiceOperation(forInvoiceReceived, parentChronologyValidator)
                frm.ShowDialog()
                Return frm.Value
            End Using

        ElseIf adapterType Is GetType(Documents.InvoiceAdapters.AssetAcquisitionInvoiceAdapter) Then

            Return Documents.InvoiceAdapters.AssetAcquisitionInvoiceAdapter.NewAssetAcquisitionInvoiceAdapter( _
                parentChronologyValidator, Not forInvoiceReceived)

        ElseIf adapterType Is GetType(Documents.InvoiceAdapters.AssetAcquisitionValueIncreaseInvoiceAdapter) Then

            Using frm As New F_NewInvoiceAdapterForAssetOperation(Of Documents.InvoiceAdapters.AssetAcquisitionValueIncreaseInvoiceAdapter) _
                (forInvoiceReceived, parentChronologyValidator)
                frm.ShowDialog()
                Return frm.Value
            End Using

        ElseIf adapterType Is GetType(Documents.InvoiceAdapters.AssetSaleInvoiceAdapter) Then

            Using frm As New F_NewInvoiceAdapterForAssetOperation(Of Documents.InvoiceAdapters.AssetSaleInvoiceAdapter) _
                (forInvoiceReceived, parentChronologyValidator)
                frm.ShowDialog()
                Return frm.Value
            End Using

        ElseIf adapterType Is GetType(Documents.InvoiceAdapters.GoodsAcquisitionInvoiceAdapter) Then

            Using frm As New F_NewInvoiceAdapterForGoodsOperation(Of Documents.InvoiceAdapters.GoodsAcquisitionInvoiceAdapter) _
                (forInvoiceReceived, parentChronologyValidator)
                frm.ShowDialog()
                Return frm.Value
            End Using

        ElseIf adapterType Is GetType(Documents.InvoiceAdapters.GoodsAddedCostsInvoiceAdapter) Then

            Using frm As New F_NewInvoiceAdapterForGoodsOperation(Of Documents.InvoiceAdapters.GoodsAddedCostsInvoiceAdapter) _
                (forInvoiceReceived, parentChronologyValidator)
                frm.ShowDialog()
                Return frm.Value
            End Using

        ElseIf adapterType Is GetType(Documents.InvoiceAdapters.GoodsDiscountInvoiceAdapter) Then

            Using frm As New F_NewInvoiceAdapterForGoodsOperation(Of Documents.InvoiceAdapters.GoodsDiscountInvoiceAdapter) _
                (forInvoiceReceived, parentChronologyValidator)
                frm.ShowDialog()
                Return frm.Value
            End Using

        ElseIf adapterType Is GetType(Documents.InvoiceAdapters.GoodsRedeemFromBuyerInvoiceAdapter) Then

            Using frm As New F_NewInvoiceAdapterForGoodsOperation(Of Documents.InvoiceAdapters.GoodsRedeemFromBuyerInvoiceAdapter) _
                (forInvoiceReceived, parentChronologyValidator)
                frm.ShowDialog()
                Return frm.Value
            End Using

        ElseIf adapterType Is GetType(Documents.InvoiceAdapters.GoodsSaleInvoiceAdapter) Then

            Using frm As New F_NewInvoiceAdapterForGoodsOperation(Of Documents.InvoiceAdapters.GoodsSaleInvoiceAdapter) _
                (forInvoiceReceived, parentChronologyValidator)
                frm.ShowDialog()
                Return frm.Value
            End Using

        Else

            Throw New NotImplementedException(String.Format("Klaida. Adapterio tipas {0} neimplementuotas metode GetNewInvoiceAdapter.", _
                adapterType.FullName))

        End If

    End Function

#End Region

#Region " Declarations "

    Private _AvailableIDeclarationList As List(Of ActiveReports.IDeclaration)

    ''' <summary>
    ''' Gets a list of available <see cref="ApskaitaObjects.ActiveReports.Declarations">
    ''' (tax, social insurance, etc.) declarations (reports)</see>, i.e.
    ''' objects that implement IDeclarationInterface.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAvailableIDeclarationList() As List(Of ActiveReports.IDeclaration)

        If _AvailableIDeclarationList Is Nothing Then
            InitializeAvailableIDeclarationList()
        End If

        Return _AvailableIDeclarationList

    End Function

    Private Sub InitializeAvailableIDeclarationList()

        _AvailableIDeclarationList = New List(Of ActiveReports.IDeclaration)

        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationFR0572_2)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationFR0572_3)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationFR0572_4)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationFR0573_2)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationFR0573_3)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationFR0573_4)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationSam1)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationSAM_2)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationSAM_3)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationSAM_4)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationSAM_5)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationSamAut1)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationSD13_1)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationSD13_2)
        _AvailableIDeclarationList.Add(New ActiveReports.Declarations.DeclarationSD13_5)

    End Sub

#End Region

#Region " Miscellaneous "

    ''' <summary>
    ''' Opens a new instance of currency rate change impact calculator that could be used to
    ''' calculate currency rate change impact on multiple documents.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowCurrencyRateChangeImpactCalculator()
        Dim frm As New F_CurrencyRateChangeImpactCalculator
        OpenForm(frm)
    End Sub

    ''' <summary>
    ''' Opens a <see cref="General.JournalEntry">JournalEntry</see> in edit form.
    ''' </summary>
    ''' <param name="queryManager">a CslaActionExtenderQueryObject instance
    ''' to use for progress visualization</param>
    ''' <param name="journalEntryID">an <see cref="General.JournalEntry.ID">ID
    ''' of the JournalEntry</see> to fetch</param>
    ''' <remarks></remarks>
    Public Sub OpenJournalEntryEditForm(ByVal queryManager As CslaActionExtenderQueryObject, _
        ByVal journalEntryID As Integer)

        If Not journalEntryID > 0 Then Exit Sub

        queryManager.InvokeQuery(Of General.JournalEntry)(Nothing, "GetJournalEntry", _
            True, AddressOf OpenObjectEditForm, journalEntryID)

    End Sub

    ''' <summary>
    ''' Shows <see cref="ActiveReports.BookEntryInfoListParent">an account turnover report</see> 
    ''' form for the given params.
    ''' </summary>
    ''' <param name="accountNumber">an <see cref="General.Account.ID">ID of the account</see></param>
    ''' <param name="dateFrom">a starting date of the report</param>
    ''' <param name="dateTo">an ending date of the report</param>
    ''' <param name="personID">an <see cref="General.Person.ID">ID of the person</see>
    ''' to filter the report by</param>
    ''' <remarks></remarks>
    Public Sub ShowAccountTurnover(ByVal accountNumber As Long, ByVal dateFrom As Date, _
        ByVal dateTo As Date, Optional ByVal personID As Integer = 0)
        ' Dim frm As Form = New F_BookEntryInfoListParent(accountNumber, dateFrom, dateTo, personID)
        Dim frm As Form = GetFormForBusinessType(GetType(ActiveReports.BookEntryInfoListParent), _
            accountNumber, dateFrom, dateTo, personID)
        OpenForm(frm)
    End Sub

#End Region

#Region " Utilities "

    ''' <summary>
    ''' Returns path to the folder where the program (.exe) is executing.
    ''' </summary>
    Friend Function AppPath() As String
        Return System.IO.Path.GetDirectoryName(Reflection.Assembly _
            .GetEntryAssembly().Location)
    End Function

    Friend Function FetchCurrencyRate(ByVal currencyCode As String, _
        ByVal atDate As Date) As AccWebCrawler.CurrencyRateList

        Dim paramList As New AccWebCrawler.CurrencyRateList
        paramList.Add(atDate, currencyCode)

        Return FetchCurrencyRate(paramList)

    End Function

    Friend Function FetchCurrencyRate(ByVal currencyList As AccWebCrawler.CurrencyRateList) _
        As AccWebCrawler.CurrencyRateList

        Using frm As New AccWebCrawler.F_LaunchWebCrawler(currencyList, GetCurrentCompany.BaseCurrency)
            If frm.ShowDialog <> DialogResult.OK OrElse frm.result Is Nothing _
                OrElse Not TypeOf frm.result Is AccWebCrawler.CurrencyRateList _
                OrElse DirectCast(frm.result, AccWebCrawler.CurrencyRateList).Count < 1 Then Return Nothing
            Return DirectCast(frm.result, AccWebCrawler.CurrencyRateList)
        End Using

    End Function

    ''' <summary>
    ''' a helper method to get an attribute of a property
    ''' </summary>
    ''' <typeparam name="TC">a type of attribute to look for</typeparam>
    ''' <param name="propInfo">a PropertyInfo to look the attribute in</param>
    ''' <remarks></remarks>
    Friend Function GetAttribute(Of TC As Attribute)(ByVal propInfo As PropertyInfo) As TC

        If propInfo Is Nothing Then Return Nothing

        For Each attr As Attribute In Attribute.GetCustomAttributes(propInfo)
            If TypeOf attr Is TC Then
                Return CType(attr, TC)
            End If
        Next

        Return Nothing

    End Function

    ''' <summary>
    ''' Helper method that tries to get an object's Text property.
    ''' </summary>
    ''' <param name="sender">an object that is supposed to have a Text property</param>
    ''' <remarks></remarks>
    Public Function GetSenderText(ByVal sender As Object) As String
        If sender Is Nothing Then Return ""
        Try
            Return sender.Text
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Gets a property info for a binded object by aspect name.
    ''' </summary>
    ''' <typeparam name="T">a type of the binded object</typeparam>
    ''' <param name="aspectName">a name of the binded property (could be a property of a child object
    ''' delimited by a .)</param>
    ''' <remarks></remarks>
    Friend Function GetBindingProperty(Of T)(ByVal aspectName As String) As PropertyInfo

        If StringIsNullOrEmpty(aspectName) Then Return Nothing

        Try
            If aspectName.Contains(".") Then
                Return GetType(T).GetProperty(aspectName.Split(New Char() {"."c}, StringSplitOptions.RemoveEmptyEntries)(0)). _
                    PropertyType.GetProperty(aspectName.Split(New Char() {"."c}, StringSplitOptions.RemoveEmptyEntries)(1))
            Else
                Return GetType(T).GetProperty(aspectName)
            End If
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region


    Public Function DoTest()

        Dim f As New F_ImportedGoodsItemList()
        'f.Show()
        Dim counter As Integer = 1

        'Clipboard.SetText(GenerateDataListViewColumns(f.ImportedGoodsItemListDataGridView, "ImportedGoodsItemListDataListView", counter), TextDataFormat.UnicodeText)
        'Clipboard.SetText(GenerateDataListViewColumns(f.CodesDataGridView, "CodesDataListView", counter) _
        '& vbCrLf & vbCrLf & vbCrLf & vbCrLf & _
        'GenerateDataListViewColumns(f.DefaultWorkTimesDataGridView, "DefaultWorkTimesDataListView", counter) _
        '& vbCrLf & vbCrLf & vbCrLf & vbCrLf & _
        'GenerateDataListViewColumns(f.NamesDataGridView, "NamesDataListView", counter) _
        '& vbCrLf & vbCrLf & vbCrLf & vbCrLf & _
        'GenerateDataListViewColumns(f.PublicHolidaysDataGridView, "PublicHolidaysDataListView", counter) _
        '& vbCrLf & vbCrLf & vbCrLf & vbCrLf & _
        'GenerateDataListViewColumns(f.TaxRatesDataGridView, "TaxRatesDataListView", counter), TextDataFormat.UnicodeText)

    End Function

    Private Function GenerateDataListViewColumns(ByVal grid As DataGridView, _
        ByVal listViewName As String, ByRef counter As Integer) As String

        Dim constructors As String = ""
        Dim addColumns As String = ""
        Dim columns As String = ""
        Dim declarations As String = ""

        For i As Integer = 1 To grid.Columns.Count

            constructors = AddWithNewLine(constructors, String.Format( _
                "Me.OlvColumn{0} = New BrightIdeasSoftware.OLVColumn", counter.ToString), False)

            addColumns = AddWithNewLine(addColumns, String.Format( _
                "Me.{0}.AllColumns.Add(Me.OlvColumn{1})", listViewName, counter.ToString), False)

            declarations = AddWithNewLine(declarations, String.Format( _
                "Friend WithEvents OlvColumn{0} As BrightIdeasSoftware.OLVColumn", counter.ToString), False)

            columns = AddWithNewLine(columns, "'", False)
            columns = AddWithNewLine(columns, String.Format( _
                "'OlvColumn{0}", counter.ToString), False)
            columns = AddWithNewLine(columns, String.Format( _
                "'", counter.ToString), False)
            columns = AddWithNewLine(columns, String.Format( _
                "Me.OlvColumn{0}.AspectName = ""{1}""", counter.ToString, _
                grid.Columns(i - 1).DataPropertyName), False)
            If Not StringIsNullOrEmpty(grid.Columns(i - 1).DefaultCellStyle.Format) Then
                columns = AddWithNewLine(columns, String.Format( _
                    "Me.OlvColumn{0}.AspectToStringFormat = ""{{0:{1}}}""", counter.ToString, _
                    grid.Columns(i - 1).DefaultCellStyle.Format), False)
            End If
            columns = AddWithNewLine(columns, String.Format( _
                "Me.OlvColumn{0}.CellEditUseWholeCell = True", counter.ToString), False)
            columns = AddWithNewLine(columns, String.Format( _
                "Me.OlvColumn{0}.DisplayIndex = {1}", counter.ToString, _
                grid.Columns(i - 1).DisplayIndex.ToString), False)
            columns = AddWithNewLine(columns, String.Format( _
                "Me.OlvColumn{0}.HeaderFont = New System.Drawing.Font(""Microsoft Sans Serif"", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))", counter.ToString), False)
            columns = AddWithNewLine(columns, String.Format( _
                "Me.OlvColumn{0}.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center", counter.ToString), False)
            If grid.ReadOnly OrElse grid.Columns(i - 1).ReadOnly Then
                columns = AddWithNewLine(columns, String.Format( _
                    "Me.OlvColumn{0}.IsEditable = False", counter.ToString), False)
            Else
                columns = AddWithNewLine(columns, String.Format( _
                    "Me.OlvColumn{0}.IsEditable = True", counter.ToString), False)
            End If
            If grid.Columns(i - 1).Visible Then
                columns = AddWithNewLine(columns, String.Format( _
                    "Me.OlvColumn{0}.IsVisible = True", counter.ToString), False)
            Else
                columns = AddWithNewLine(columns, String.Format( _
                    "Me.OlvColumn{0}.IsVisible = False", counter.ToString), False)
            End If
            columns = AddWithNewLine(columns, String.Format( _
                "Me.OlvColumn{0}.Text = ""{1}""", counter.ToString, _
                grid.Columns(i - 1).HeaderText), False)
            columns = AddWithNewLine(columns, String.Format( _
                "Me.OlvColumn{0}.TextAlign = System.Windows.Forms.HorizontalAlignment.Left", _
                counter.ToString), False)
            columns = AddWithNewLine(columns, String.Format( _
                "Me.OlvColumn{0}.ToolTipText = ""{1}""", counter.ToString, _
                grid.Columns(i - 1).ToolTipText), False)
            columns = AddWithNewLine(columns, String.Format( _
                "Me.OlvColumn{0}.Width = {1}", counter.ToString, _
                grid.Columns(i - 1).Width.ToString), False)

            counter += 1

        Next

        Return constructors & vbCrLf & vbCrLf & vbCrLf _
            & addColumns & vbCrLf & vbCrLf & vbCrLf _
            & columns & vbCrLf & vbCrLf & vbCrLf _
            & declarations

    End Function

End Module
