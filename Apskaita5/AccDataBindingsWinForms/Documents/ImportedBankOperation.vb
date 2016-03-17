''' <summary>
''' Helper object that encapsulates both an <see cref="ApskaitaObjects.Documents.BankOperationItem">
''' imported bank operation data</see> and a <see cref="ApskaitaObjects.Documents.BankOperationItemList.Account">
''' cash account that the operation data was imported for</see>.
''' </summary>
''' <remarks>Required for consistency in order to open any object by passing the object as a single
''' param for the <see cref="OpenObjectEditForm">OpenObjectEditForm</see> method.</remarks>
Public Class ImportedBankOperation

    ''' <summary>
    ''' Corresponds to a <see cref="ApskaitaObjects.Documents.BankOperationItem">BankOperationItem</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly OperationData As Documents.BankOperationItem

    ''' <summary>
    ''' Corresponds to a <see cref="ApskaitaObjects.Documents.BankOperationItemList.Account">BankOperationItemList.Account</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly OperationAccount As HelperLists.CashAccountInfo

    Public Sub New(ByVal operationItem As Documents.BankOperationItem, ByVal cashAccount As HelperLists.CashAccountInfo)
        OperationData = operationItem
        OperationAccount = cashAccount
    End Sub

End Class
