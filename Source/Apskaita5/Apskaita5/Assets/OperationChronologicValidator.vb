﻿Namespace Assets

    ''' <summary>
    ''' Represents an <see cref="IChronologicValidator">IChronologicValidator</see> instance 
    ''' that is used to validate long term asset operation chronologic restrains.
    ''' </summary>
    ''' <remarks>Should only be used as a child of a long term asset operation, 
    ''' e.g. <see cref="OperationAmortization">OperationAmortization</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class OperationChronologicValidator
        Inherits ReadOnlyBase(Of OperationChronologicValidator)
        Implements IChronologicValidator

#Region " Business Methods "

        Private Shared _syncRoot As New Object
        Private Shared _IsAffectedByDictionary As Dictionary(Of LtaOperationType, LtaOperationType())
        Private Shared _IsLockedByDictionary As Dictionary(Of LtaOperationType, LtaOperationType())

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _BackgroundInfo As OperationBackground = Nothing
        Private _CurrentOperationID As Integer = 0
        Private _CurrentOperationDate As Date = Today
        Private _CurrentOperationName As String = ""
        Private _FinancialDataCanChange As Boolean = True
        Private _MinDateApplicable As Boolean = False
        Private _MaxDateApplicable As Boolean = False
        Private _MinDate As Date = Date.MinValue
        Private _MaxDate As Date = Date.MaxValue
        Private _FinancialDataCanChangeExplanation As String = ""
        Private _MinDateExplanation As String = ""
        Private _MaxDateExplanation As String = ""
        Private _LimitsExplanation As String = ""
        Private _BackgroundExplanation As String = ""
        Private _TypeOperation As LtaOperationType = LtaOperationType.Discard
        Private _TypeAccount As LtaAccountChangeType = LtaAccountChangeType.AcquisitionAccount
        Private _AssetAcquisitionDate As Date = Date.MinValue
        Private _ParentFinancialDataCanChange As Boolean = True
        Private _ParentFinancialDataCanChangeExplanation As String = ""


        ''' <summary>
        ''' Gets a dictionary of <see cref="LtaOperationType">long term asset operation types</see>
        ''' that affect the current operation type, i.e. the current operation type
        ''' date and financial data is limited.
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared ReadOnly Property IsAffectedByDictionary() As Dictionary(Of LtaOperationType, LtaOperationType())
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                InitializeDescriptors()
                Return _IsAffectedByDictionary
            End Get
        End Property

        ''' <summary>
        ''' Gets a dictionary of <see cref="LtaOperationType">long term asset operation types</see>
        ''' that lock the current operation type, i.e. a subsequent operation
        ''' locks the current type operation's date and financial data.
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared ReadOnly Property IsLockedByDictionary() As Dictionary(Of LtaOperationType, LtaOperationType())
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                InitializeDescriptors()
                Return _IsLockedByDictionary
            End Get
        End Property


        ''' <summary>
        ''' Gets an ID of the validated (parent) long term asset operation object,
        ''' e.g. <see cref="OperationAmortization.ID">OperationAmortization.ID</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentOperationID() As Integer _
            Implements IChronologicValidator.CurrentOperationID
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentOperationID
            End Get
        End Property

        ''' <summary>
        ''' Gets the current date of the validated (parent) long term asset operation 
        ''' (<see cref="Today">Today</see> for a new object). 
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentOperationDate() As Date _
            Implements IChronologicValidator.CurrentOperationDate
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentOperationDate
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable name of the validated (parent) long term asset operation. 
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentOperationName() As String _
            Implements IChronologicValidator.CurrentOperationName
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentOperationName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Wheather the financial data of the validated (parent) long term asset operation 
        ''' is allowed to be changed.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property FinancialDataCanChange() As Boolean _
            Implements IChronologicValidator.FinancialDataCanChange
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Wheather there is a minimum allowed date for the validated (parent) operation.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property MinDateApplicable() As Boolean _
            Implements IChronologicValidator.MinDateApplicable
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MinDateApplicable
            End Get
        End Property

        ''' <summary>
        ''' Wheather there is a maximum allowed date for the validated (parent) operation.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property MaxDateApplicable() As Boolean _
            Implements IChronologicValidator.MaxDateApplicable
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MaxDateApplicable
            End Get
        End Property

        ''' <summary>
        ''' Gets a minimum allowed date for the validated (parent) operation.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property MinDate() As Date _
            Implements IChronologicValidator.MinDate
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MinDate
            End Get
        End Property

        ''' <summary>
        ''' Gets a maximum allowed date for the validated (parent) operation.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property MaxDate() As Date _
            Implements IChronologicValidator.MaxDate
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MaxDate
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable explanation of why the financial data 
        ''' of the validated (parent) operation is NOT allowed to be changed.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property FinancialDataCanChangeExplanation() As String _
            Implements IChronologicValidator.FinancialDataCanChangeExplanation
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FinancialDataCanChangeExplanation.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable explanation of why there is a minimum allowed date 
        ''' for the validated (parent) operation.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property MinDateExplanation() As String _
            Implements IChronologicValidator.MinDateExplanation
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MinDateExplanation.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable explanation of why there is a maximum allowed date 
        ''' for the validated (parent) operation.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property MaxDateExplanation() As String _
            Implements IChronologicValidator.MaxDateExplanation
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MaxDateExplanation.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable explanation of all the applicable business rules restrains.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property LimitsExplanation() As String _
            Implements IChronologicValidator.LimitsExplanation
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LimitsExplanation.Trim
            End Get
        End Property

        ''' <summary>
        ''' A human readable explanation of the applicable business rules restrains.
        ''' </summary>
        ''' <remarks>More exhaustive than <see cref="LimitsExplanation">LimitsExplanation</see>.</remarks>
        Public ReadOnly Property BackgroundExplanation() As String _
            Implements IChronologicValidator.BackgroundExplanation
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BackgroundExplanation.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="LtaOperationType">type</see> of the validated (parent) 
        ''' long term asset operation.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TypeOperation() As LtaOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeOperation
            End Get
        End Property

        ''' <summary>
        ''' Gets the <see cref="LongTermAsset.AcquisitionDate">acquisition date
        ''' of the long term asset</see> that the validated (parent) operation operates on.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property AssetAcquisitionDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetAcquisitionDate
            End Get
        End Property

        ''' <summary>
        ''' Wheather the financial data of the validated (parent) long term asset operation 
        ''' is allowed to be changed by the operation's parent document.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ParentFinancialDataCanChange() As Boolean _
        Implements IChronologicValidator.ParentFinancialDataCanChange
            Get
                Return _ParentFinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable explanation of why the financial data 
        ''' of the validated (parent) operation is NOT allowed to be changed
        ''' by the operation's parent document.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ParentFinancialDataCanChangeExplanation() As String _
            Implements IChronologicValidator.ParentFinancialDataCanChangeExplanation
            Get
                Return _ParentFinancialDataCanChangeExplanation
            End Get
        End Property


        Private Shared Sub InitializeDescriptors()

            If _IsAffectedByDictionary Is Nothing OrElse _IsLockedByDictionary Is Nothing Then

                SyncLock _syncRoot

                    If _IsAffectedByDictionary Is Nothing OrElse _IsLockedByDictionary Is Nothing Then

                        Dim list As List(Of OperationChronologicDescriptor) _
                            = GetOperationChronologicDescriptorList()

                        _IsAffectedByDictionary = GetIsAffectedByDictionary(list)
                        _IsLockedByDictionary = GetIsLockedByDictionary(list)

                    End If

                End SyncLock

            End If

        End Sub

        Private Shared Function GetOperationChronologicDescriptorList() As List(Of OperationChronologicDescriptor)

            Dim result As New List(Of OperationChronologicDescriptor)

            result.Add(OperationChronologicDescriptor.GetOperationChronologicDescriptor( _
                LtaOperationType.AccountChange, _
                New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.Discard, _
                LtaOperationType.Transfer, LtaOperationType.ValueChange}, _
                Nothing, New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.Discard, _
                LtaOperationType.Transfer, LtaOperationType.ValueChange}, Nothing))

            result.Add(OperationChronologicDescriptor.GetOperationChronologicDescriptor( _
                LtaOperationType.AcquisitionValueIncrease, _
                New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.ValueChange, _
                LtaOperationType.Discard, LtaOperationType.Transfer}, Nothing, _
                New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.ValueChange, _
                LtaOperationType.Discard, LtaOperationType.Transfer}, _
                New LtaOperationType() {LtaOperationType.Amortization}))

            result.Add(OperationChronologicDescriptor.GetOperationChronologicDescriptor( _
                LtaOperationType.Amortization, _
                New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.ValueChange, _
                LtaOperationType.Discard, LtaOperationType.Transfer}, _
                New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.ValueChange, _
                LtaOperationType.Discard, LtaOperationType.Transfer, _
                LtaOperationType.AmortizationPeriod, LtaOperationType.UsingEnd, _
                LtaOperationType.UsingStart}, _
                New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.ValueChange, _
                LtaOperationType.Discard, LtaOperationType.Transfer, _
                LtaOperationType.AmortizationPeriod, LtaOperationType.UsingEnd, _
                LtaOperationType.UsingStart}, _
                New LtaOperationType() {LtaOperationType.Amortization}))

            result.Add(OperationChronologicDescriptor.GetOperationChronologicDescriptor( _
                LtaOperationType.AmortizationPeriod, _
                New LtaOperationType() {LtaOperationType.Amortization}, Nothing, _
                New LtaOperationType() {LtaOperationType.Amortization}, _
                New LtaOperationType() {LtaOperationType.Amortization}))

            result.Add(OperationChronologicDescriptor.GetOperationChronologicDescriptor( _
                LtaOperationType.Discard, _
                New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.ValueChange, _
                LtaOperationType.Discard, LtaOperationType.Transfer}, Nothing, _
                New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.ValueChange, _
                LtaOperationType.Discard, LtaOperationType.Transfer}, _
                New LtaOperationType() {LtaOperationType.Amortization}))

            result.Add(OperationChronologicDescriptor.GetOperationChronologicDescriptor( _
                LtaOperationType.Transfer, _
                New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.ValueChange, _
                LtaOperationType.Discard, LtaOperationType.Transfer}, Nothing, _
                New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.ValueChange, _
                LtaOperationType.Discard, LtaOperationType.Transfer}, _
                New LtaOperationType() {LtaOperationType.Amortization}))

            result.Add(OperationChronologicDescriptor.GetOperationChronologicDescriptor( _
                LtaOperationType.UsingEnd, _
                New LtaOperationType() {LtaOperationType.Amortization, _
                LtaOperationType.UsingEnd, LtaOperationType.UsingStart}, Nothing, _
                New LtaOperationType() {LtaOperationType.Amortization, _
                LtaOperationType.UsingEnd, LtaOperationType.UsingStart}, _
                New LtaOperationType() {LtaOperationType.Amortization}))

            result.Add(OperationChronologicDescriptor.GetOperationChronologicDescriptor( _
                LtaOperationType.UsingStart, _
                New LtaOperationType() {LtaOperationType.Amortization, _
                LtaOperationType.UsingEnd, LtaOperationType.UsingStart}, Nothing, _
                New LtaOperationType() {LtaOperationType.Amortization, _
                LtaOperationType.UsingEnd, LtaOperationType.UsingStart}, _
                New LtaOperationType() {LtaOperationType.Amortization}))

            result.Add(OperationChronologicDescriptor.GetOperationChronologicDescriptor( _
                LtaOperationType.ValueChange, _
                New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.ValueChange, _
                LtaOperationType.Discard, LtaOperationType.Transfer}, Nothing, _
                New LtaOperationType() {LtaOperationType.AccountChange, _
                LtaOperationType.AcquisitionValueIncrease, _
                LtaOperationType.Amortization, LtaOperationType.ValueChange, _
                LtaOperationType.Discard, LtaOperationType.Transfer}, _
                New LtaOperationType() {LtaOperationType.Amortization}))

            Return result

        End Function

        Private Shared Function GetIsAffectedByDictionary( _
            ByVal list As List(Of OperationChronologicDescriptor)) _
            As Dictionary(Of LtaOperationType, LtaOperationType())

            Dim result As New Dictionary(Of LtaOperationType, LtaOperationType())

            For Each desc As OperationChronologicDescriptor In list
                result.Add(desc.OperationType, OperationChronologicDescriptor. _
                    GetAffectingTypes(list, desc.OperationType))
            Next

            Return result

        End Function

        Private Shared Function GetIsLockedByDictionary( _
            ByVal list As List(Of OperationChronologicDescriptor)) _
            As Dictionary(Of LtaOperationType, LtaOperationType())

            Dim result As New Dictionary(Of LtaOperationType, LtaOperationType())

            For Each desc As OperationChronologicDescriptor In list
                result.Add(desc.OperationType, OperationChronologicDescriptor. _
                    GetLockingTypes(list, desc.OperationType))
            Next

            Return result

        End Function


        ''' <summary>
        ''' Validates a long term asset operation date against chronological
        ''' rules contained within the object. Returns TRUE if the date is valid, 
        ''' otherwise returns FALSE.
        ''' </summary>
        ''' <param name="operationDate">A date of a long term asset operation to validate.</param>
        ''' <param name="errorMessage">Output parameter that is set to error message.</param>
        ''' <param name="errorSeverity">Output parameter that is set to error severity level.</param>
        ''' <returns>Returns TRUE if the operation date is valid, otherwise returns FALSE.</returns>
        ''' <remarks></remarks>
        Friend Function ValidateOperationDate(ByVal operationDate As Date, _
            ByRef errorMessage As String, ByRef errorSeverity As Validation.RuleSeverity) As Boolean _
            Implements IChronologicValidator.ValidateOperationDate

            If _MinDateApplicable AndAlso operationDate.Date < _MinDate.Date Then
                errorMessage = _MinDateExplanation
                errorSeverity = Validation.RuleSeverity.Error
                Return False
            ElseIf _MaxDateApplicable AndAlso operationDate.Date > _MaxDate.Date Then
                errorMessage = _MaxDateExplanation
                errorSeverity = Validation.RuleSeverity.Error
                Return False
            End If

            Return True

        End Function


        Private Shared Function GetOperationName(ByVal operationType As LtaOperationType, _
            ByVal accountType As LtaAccountChangeType) As String

            If operationType = LtaOperationType.AccountChange Then
                Return String.Format("{0} - {1}", Utilities.ConvertLocalizedName(operationType), _
                    Utilities.ConvertLocalizedName(accountType))
            Else
                Return Utilities.ConvertLocalizedName(operationType)
            End If

        End Function


        Friend Sub MarkOld(ByVal newID As Integer, ByVal newDate As Date)
            _CurrentOperationID = newID
            _CurrentOperationDate = newDate
        End Sub


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Assets_OperationChronologicValidator_ToString, _
                _CurrentOperationName)
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets an OperationChronologicValidator instance for a new long term asset operation.
        ''' </summary>
        ''' <param name="backgroundInfo">A background info for the long term asset
        ''' that the operation operates on.</param>
        ''' <param name="operationType">A type of the operation.</param>
        ''' <param name="accountType">A type of the account change (only releveant
        ''' if the <paramref name="operationType">operationType</paramref>
        ''' is set to <see cref="LtaOperationType.AccountChange">LtaOperationType.AccountChange</see>, 
        ''' ignored otherwise)</param>
        ''' <param name="parentValidator">The operation's parent document 
        ''' IChronologicValidator if any.</param>
        ''' <remarks></remarks>
        Friend Shared Function NewOperationChronologicValidator( _
            ByVal backgroundInfo As OperationBackground, ByVal operationType As LtaOperationType, _
            ByVal accountType As LtaAccountChangeType, ByVal parentValidator As IChronologicValidator) _
            As OperationChronologicValidator
            Return New OperationChronologicValidator(backgroundInfo, operationType, _
                accountType, parentValidator)
        End Function

        ''' <summary>
        ''' Gets an OperationChronologicValidator instance for a new long term asset operation.
        ''' </summary>
        ''' <param name="backgroundInfo">A background info for the long term asset
        ''' that the operation operates on.</param>
        ''' <param name="operationType">A type of the operation.</param>
        ''' <param name="parentValidator">The operation's parent document 
        ''' IChronologicValidator if any.</param>
        ''' <remarks></remarks>
        Friend Shared Function NewOperationChronologicValidator( _
            ByVal backgroundInfo As OperationBackground, ByVal operationType As LtaOperationType, _
            ByVal parentValidator As IChronologicValidator) As OperationChronologicValidator
            Return New OperationChronologicValidator(backgroundInfo, operationType, _
                LtaAccountChangeType.AcquisitionAccount, parentValidator)
        End Function


        ''' <summary>
        ''' Gets an OperationChronologicValidator instance for an existing 
        ''' long term asset operation.
        ''' </summary>
        ''' <param name="backgroundInfo">A background info for the long term asset
        ''' that the operation operates on.</param>
        ''' <param name="operationType">A type of the operation.</param>
        ''' <param name="accountType">A type of the account change (only releveant
        ''' if the <paramref name="operationType">operationType</paramref>
        ''' is set to <see cref="LtaOperationType.AccountChange">LtaOperationType.AccountChange</see>, 
        ''' ignored otherwise)</param>
        ''' <param name="operationID">An ID of the long term asset operation.</param>
        ''' <param name="operationDate">A date of the long term asset operation.</param>
        ''' <param name="parentValidator">The operation's parent document 
        ''' IChronologicValidator if any.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetOperationChronologicValidator( _
            ByVal backgroundInfo As OperationBackground, ByVal operationType As LtaOperationType, _
            ByVal accountType As LtaAccountChangeType, ByVal operationID As Integer, _
            ByVal operationDate As Date, ByVal parentValidator As IChronologicValidator) As OperationChronologicValidator
            Return New OperationChronologicValidator(backgroundInfo, operationType, _
                accountType, operationID, operationDate, parentValidator)
        End Function

        ''' <summary>
        ''' Gets an OperationChronologicValidator instance for an existing 
        ''' long term asset operation.
        ''' </summary>
        ''' <param name="backgroundInfo">A background info for the long term asset
        ''' that the operation operates on.</param>
        ''' <param name="operationType">A type of the operation.</param>
        ''' <param name="operationID">An ID of the long term asset operation.</param>
        ''' <param name="operationDate">A date of the long term asset operation.</param>
        ''' <param name="parentValidator">The operation's parent document 
        ''' IChronologicValidator if any.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetOperationChronologicValidator( _
            ByVal backgroundInfo As OperationBackground, ByVal operationType As LtaOperationType, _
            ByVal operationID As Integer, ByVal operationDate As Date, _
            ByVal parentValidator As IChronologicValidator) As OperationChronologicValidator
            Return New OperationChronologicValidator(backgroundInfo, operationType, _
                LtaAccountChangeType.AcquisitionAccount, operationID, operationDate, parentValidator)
        End Function



        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal nBackgroundInfo As OperationBackground, ByVal operationType As LtaOperationType, _
            ByVal accountType As LtaAccountChangeType, ByVal parentValidator As IChronologicValidator)
            ' require use of factory methods
            Create(nBackgroundInfo, operationType, accountType, parentValidator)
        End Sub

        Private Sub New(ByVal nBackgroundInfo As OperationBackground, ByVal operationType As LtaOperationType, _
            ByVal accountType As LtaAccountChangeType, ByVal operationID As Integer, _
            ByVal operationDate As Date, ByVal parentValidator As IChronologicValidator)
            ' require use of factory methods
            Fetch(nBackgroundInfo, operationType, accountType, operationID, _
                operationDate, parentValidator)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal nBackgroundInfo As OperationBackground, ByVal operationType As LtaOperationType, _
            ByVal accountType As LtaAccountChangeType, ByVal parentValidator As IChronologicValidator)

            If nBackgroundInfo Is Nothing Then
                Throw New ArgumentNullException("nBackgroundInfo")
            End If

            _BackgroundInfo = nBackgroundInfo
            _TypeOperation = operationType
            _TypeAccount = accountType
            _CurrentOperationName = GetOperationName(operationType, accountType)
            _AssetAcquisitionDate = nBackgroundInfo.AssetDateAcquired

            FetchLimitations(parentValidator)

        End Sub


        Private Sub Fetch(ByVal nBackgroundInfo As OperationBackground, ByVal operationType As LtaOperationType, _
            ByVal accountType As LtaAccountChangeType, ByVal operationID As Integer, _
            ByVal operationDate As Date, ByVal parentValidator As IChronologicValidator)

            If nBackgroundInfo Is Nothing Then
                Throw New ArgumentNullException("nBackgroundInfo")
            End If

            _BackgroundInfo = nBackgroundInfo
            _TypeOperation = operationType
            _TypeAccount = accountType
            _CurrentOperationName = GetOperationName(operationType, accountType)
            _CurrentOperationID = operationID
            _CurrentOperationDate = operationDate
            _AssetAcquisitionDate = nBackgroundInfo.AssetDateAcquired

            FetchLimitations(parentValidator)

        End Sub


        Private Sub FetchLimitations(ByVal parentValidator As IChronologicValidator)

            SetDefaults()

            SetMinDateApplicable(_AssetAcquisitionDate, String.Format( _
                My.Resources.Assets_OperationChronologicValidator_MinDateByAcquisitionDate, _
                _AssetAcquisitionDate.ToString("yyyy-MM-dd")), False)

            If _CurrentOperationID > 0 Then
                FetchLimitationsForOldOperation()
            Else
                FetchLimitationsForNewOperation()
            End If

            If Not parentValidator Is Nothing AndAlso Not parentValidator.FinancialDataCanChange _
                AndAlso (_TypeOperation = LtaOperationType.AccountChange _
                OrElse _TypeOperation = LtaOperationType.Amortization _
                OrElse _TypeOperation = LtaOperationType.Discard) Then

                _ParentFinancialDataCanChange = False
                _ParentFinancialDataCanChangeExplanation = AddWithNewLine( _
                    _ParentFinancialDataCanChangeExplanation, _
                    parentValidator.FinancialDataCanChangeExplanation, False)

            End If

            SetLimitsExplanation()

            GetBackgroundDescription()

        End Sub

        Private Sub SetDefaults()

            _FinancialDataCanChange = True
            _FinancialDataCanChangeExplanation = ""
            _ParentFinancialDataCanChange = True
            _ParentFinancialDataCanChangeExplanation = ""

            _MaxDateApplicable = False
            _MaxDate = Date.MaxValue
            _MaxDateExplanation = ""

            _MinDateApplicable = False
            _MinDate = Date.MinValue
            _MinDateExplanation = ""

            _LimitsExplanation = ""
            _BackgroundExplanation = ""

        End Sub

        Private Sub SetMaxDateApplicable(ByVal nDate As Date, ByVal nExplanation As String, _
            ByVal addOneDay As Boolean)

            If nDate = Date.MaxValue OrElse nDate = Date.MinValue Then Exit Sub

            If addOneDay Then
                nDate = nDate.AddDays(-1)
            End If

            If Not nDate.Date < _MaxDate.Date Then Exit Sub

            _MaxDateApplicable = True
            _MaxDate = nDate.Date
            _MaxDateExplanation = nExplanation

        End Sub

        Private Sub SetMinDateApplicable(ByVal nDate As Date, ByVal nExplanation As String, _
            ByVal addOneDay As Boolean)

            If nDate = Date.MaxValue OrElse nDate = Date.MinValue Then Exit Sub

            If addOneDay Then
                nDate = nDate.AddDays(1)
            End If

            If Not nDate.Date > _MinDate.Date Then Exit Sub

            _MinDateApplicable = True
            _MinDate = nDate.Date
            _MinDateExplanation = nExplanation

        End Sub

        Private Sub SetFinancialDataCanChange(ByVal nDate As Date, ByVal nExplanation As String, _
            ByVal nDateExplanation As String, ByVal addOneDay As Boolean)

            If nDate = Date.MaxValue OrElse nDate = Date.MinValue OrElse _
                StringIsNullOrEmpty(nExplanation) Then Exit Sub

            _FinancialDataCanChange = False
            _FinancialDataCanChangeExplanation = AddWithNewLine(_FinancialDataCanChangeExplanation, _
                nExplanation, False)

            SetMaxDateApplicable(nDate, nDateExplanation, addOneDay)

        End Sub

        Private Sub LockDateAndFinancialData(ByVal nExplanation As String, _
            ByVal nDateExplanation As String)

            _FinancialDataCanChange = False
            _FinancialDataCanChangeExplanation = AddWithNewLine(_FinancialDataCanChangeExplanation, _
                nExplanation, False)

            SetMinDateApplicable(_CurrentOperationDate, nDateExplanation, False)
            SetMaxDateApplicable(_CurrentOperationDate, nDateExplanation, False)

        End Sub

        Private Sub SetLimitsExplanation()

            _LimitsExplanation = ""

            If Not _FinancialDataCanChange Then
                _LimitsExplanation = _FinancialDataCanChangeExplanation
            End If

            If _MinDateApplicable AndAlso _MaxDateApplicable Then

                If _MinDateExplanation.Trim = _MaxDateExplanation.Trim Then
                    _LimitsExplanation = AddWithNewLine(_LimitsExplanation, _MinDateExplanation, False)
                Else
                    _LimitsExplanation = AddWithNewLine(_LimitsExplanation, _MinDateExplanation, False)
                    _LimitsExplanation = AddWithNewLine(_LimitsExplanation, _MaxDateExplanation, False)
                End If

            Else

                If _MinDateApplicable Then
                    _LimitsExplanation = AddWithNewLine(_LimitsExplanation, _MinDateExplanation, False)
                End If
                If _MaxDateApplicable Then
                    _LimitsExplanation = AddWithNewLine(_LimitsExplanation, _MaxDateExplanation, False)
                End If

            End If

        End Sub

        Private Function GetBackgroundDescription() As String

            Dim result As String = _LimitsExplanation

            If String.IsNullOrEmpty(_LimitsExplanation) Then

                result = My.Resources.Assets_OperationChronologicValidator_DescriptionLimitsNull

            Else

                result = String.Format(My.Resources.Assets_OperationChronologicValidator_DescriptionLimits, _
                    vbCrLf, _LimitsExplanation)

            End If

            Return result

        End Function

        Private Function GetOperationListString(ByVal operations As LtaOperationType()) As String

            If operations Is Nothing OrElse operations.Length < 1 Then Return ""

            Dim result As New List(Of String)

            For Each operation As LtaOperationType In operations
                result.Add(Utilities.ConvertLocalizedName(operation))
            Next

            Return String.Join(", ", result.ToArray())

        End Function


        Private Sub FetchLimitationsForNewOperation()

            Dim affectingTypes As LtaOperationType() = IsAffectedByDictionary(_TypeOperation)

            If Not affectingTypes Is Nothing AndAlso affectingTypes.Length > 0 Then

                Dim lastSignificantDate As Date = _BackgroundInfo.GetDateMax(affectingTypes)

                SetMinDateApplicable(lastSignificantDate, String.Format( _
                    My.Resources.Assets_OperationChronologicValidator_MinDateExplanation, _
                    lastSignificantDate.ToString("yyyy-MM-dd")), False)

            End If

        End Sub

        Private Sub FetchLimitationsForOldOperation()

            Dim lockingTypes As LtaOperationType() = IsLockedByDictionary(_TypeOperation)

            If Not lockingTypes Is Nothing AndAlso lockingTypes.Length > 0 Then

                If _BackgroundInfo.GetDateFirstAfter(_CurrentOperationDate, _
                    _CurrentOperationID, lockingTypes) <> Date.MaxValue Then

                    LockDateAndFinancialData(My.Resources.Assets_OperationChronologicValidator_FinancialDataCanChangeExplanation, _
                        String.Format(My.Resources.Assets_OperationChronologicValidator_DateIsLockedExplanation, _
                        GetOperationListString(lockingTypes)))

                    Exit Sub

                End If

            End If

            Dim affectingTypes As LtaOperationType() = IsAffectedByDictionary(_TypeOperation)

            If Not affectingTypes Is Nothing AndAlso affectingTypes.Length > 0 Then

                Dim firstSignificantDate As Date = _
                    _BackgroundInfo.GetDateFirstAfter(_CurrentOperationDate, _
                    _CurrentOperationID, affectingTypes)

                SetFinancialDataCanChange(firstSignificantDate, _
                    My.Resources.Assets_OperationChronologicValidator_FinancialDataCanChangeExplanation, _
                    String.Format(My.Resources.Assets_OperationChronologicValidator_MaxDateExplanation, _
                    firstSignificantDate.ToString("yyyy-MM-dd")), False)

                Dim lastSignificantDate As Date = _
                    _BackgroundInfo.GetDateLastBefore(_CurrentOperationDate, _
                    _CurrentOperationID, affectingTypes)

                SetMinDateApplicable(lastSignificantDate, String.Format( _
                    My.Resources.Assets_OperationChronologicValidator_MinDateExplanation, _
                    lastSignificantDate.ToString("yyyy-MM-dd")), False)

            End If

        End Sub


        'Private Sub FetchLimitationsForNewAccountChange()

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.Overall, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.Amortization, LtaOperationType.ValueChange, LtaOperationType.Discard, _
        '        LtaOperationType.Transfer)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra registruota operacijų su turtu.", True)

        'End Sub

        'Private Sub FetchLimitationsForOldAccountChange()

        '    Dim firstSignificantDate As Date = GetMinDate(OperationChronologyType.FirstAfter, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.Amortization, LtaOperationType.ValueChange, LtaOperationType.Discard, _
        '        LtaOperationType.Transfer)

        '    SetFinancialDataCanChange(firstSignificantDate, "Finansinių operacijos duomenų keisti " _
        '        & "negalima, nes vėlesne data yra registruota operacijų su turtu.", _
        '        "Maksimali leidžiama operacijos data yra " & DatePlaceHolder & ", nes šia data " _
        '        & "yra registruota operacijų su turtu.", False)

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.LastBefore, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.Amortization, LtaOperationType.ValueChange, LtaOperationType.Discard, _
        '        LtaOperationType.Transfer)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra registruota operacijų su turtu.", True)

        'End Sub

        'Private Sub FetchLimitationsForNewValueIncrease()

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.Overall, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.Amortization, LtaOperationType.ValueChange, LtaOperationType.Discard, _
        '        LtaOperationType.Transfer)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra registruota operacijų su turtu.", True)

        'End Sub

        'Private Sub FetchLimitationsForOldValueIncrease()

        '    If GetMaxDate(OperationChronologyType.FirstAfter, LtaOperationType.Amortization) _
        '        <> Date.MinValue Then

        '        LockDateAndFinancialData("Finansinių operacijos duomenų keisti " _
        '            & "negalima, nes nes po šios operacijos buvo skaičiuota amortizacija.", _
        '            "Operacijos data negali keistis, nes po šios operacijos " _
        '            & "buvo skaičiuota amortizacija.")

        '        Exit Sub

        '    End If

        '    Dim firstSignificantDate As Date = GetMinDate(OperationChronologyType.FirstAfter, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.ValueChange, LtaOperationType.Discard, LtaOperationType.Transfer)

        '    SetFinancialDataCanChange(firstSignificantDate, "Finansinių operacijos duomenų keisti " _
        '        & "negalima, nes vėlesne data yra registruota operacijų su turtu.", _
        '        "Maksimali leidžiama operacijos data yra " & DatePlaceHolder & ", nes šia data " _
        '        & "yra registruota operacijų su turtu.", False)

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.LastBefore, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.Amortization, LtaOperationType.ValueChange, LtaOperationType.Discard, _
        '        LtaOperationType.Transfer)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra registruota operacijų su turtu.", True)

        'End Sub

        'Private Sub FetchLimitationsForNewAmortization()

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.Overall, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.Amortization, LtaOperationType.ValueChange, LtaOperationType.Discard, _
        '        LtaOperationType.Transfer)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra registruota operacijų su turtu.", True)

        'End Sub

        'Private Sub FetchLimitationsForOldAmortization()

        '    If GetMaxDate(OperationChronologyType.FirstAfter, LtaOperationType.Amortization) _
        '        <> Date.MinValue Then

        '        LockDateAndFinancialData("Finansinių operacijos duomenų keisti " _
        '            & "negalima, nes nes po šios operacijos buvo skaičiuota amortizacija.", _
        '            "Operacijos data negali keistis, nes po šios operacijos " _
        '            & "buvo skaičiuota amortizacija.")

        '        Exit Sub

        '    End If

        '    Dim firstSignificantDate As Date = GetMinDate(OperationChronologyType.FirstAfter, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.ValueChange, LtaOperationType.Discard, LtaOperationType.Transfer)

        '    SetFinancialDataCanChange(firstSignificantDate, "Finansinių operacijos duomenų keisti " _
        '        & "negalima, nes vėlesne data yra registruota operacijų su turtu.", _
        '        "Maksimali leidžiama operacijos data yra " & DatePlaceHolder & ", nes šia data " _
        '        & "yra registruota operacijų su turtu.", False)

        '    Dim LastSignificantDate As Date = GetMaxDate(OperationChronologyType.LastBefore, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.ValueChange, LtaOperationType.Discard, LtaOperationType.Transfer)

        '    SetMinDateApplicable(LastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra registruota operacijų su turtu.", True)

        'End Sub

        'Private Sub FetchLimitationsForNewAmortizationPeriod()

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.Overall, _
        '        LtaOperationType.Amortization)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra skaičiuota amortizacija.", True)

        'End Sub

        'Private Sub FetchLimitationsForOldAmortizationPeriod()

        '    If GetMaxDate(OperationChronologyType.FirstAfter, LtaOperationType.Amortization) _
        '        <> Date.MinValue Then

        '        LockDateAndFinancialData("Finansinių operacijos duomenų keisti " _
        '            & "negalima, nes nes po šios operacijos buvo skaičiuota amortizacija.", _
        '            "Operacijos data negali keistis, nes po šios operacijos " _
        '            & "buvo skaičiuota amortizacija.")

        '    Else

        '        Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.Overall, _
        '            LtaOperationType.Amortization)

        '        SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '            & DatePlaceHolder & ", nes prieš tai yra skaičiuota amortizacija.", True)

        '    End If

        'End Sub

        'Private Sub FetchLimitationsForNewDiscard()

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.Overall, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.Amortization, LtaOperationType.ValueChange, LtaOperationType.Discard, _
        '        LtaOperationType.Transfer)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra registruota operacijų su turtu.", True)

        'End Sub

        'Private Sub FetchLimitationsForOldDiscard()

        '    If GetMaxDate(OperationChronologyType.FirstAfter, LtaOperationType.Amortization) _
        '        <> Date.MinValue Then

        '        LockDateAndFinancialData("Finansinių operacijos duomenų keisti " _
        '            & "negalima, nes nes po šios operacijos buvo skaičiuota amortizacija.", _
        '            "Operacijos data negali keistis, nes po šios operacijos " _
        '            & "buvo skaičiuota amortizacija.")

        '        Exit Sub

        '    End If

        '    Dim firstSignificantDate As Date = GetMinDate(OperationChronologyType.FirstAfter, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.ValueChange, LtaOperationType.Transfer)

        '    SetFinancialDataCanChange(firstSignificantDate, "Finansinių operacijos duomenų keisti " _
        '        & "negalima, nes vėlesne data yra registruota operacijų su turtu.", _
        '        "Maksimali leidžiama operacijos data yra " & DatePlaceHolder & ", nes šia data " _
        '        & "yra registruota operacijų su turtu.", False)

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.LastBefore, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.ValueChange, LtaOperationType.Discard, LtaOperationType.Transfer)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra registruota operacijų su turtu.", True)

        'End Sub

        'Private Sub FetchLimitationsForNewTransfer()

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.Overall, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.Amortization, LtaOperationType.ValueChange, LtaOperationType.Discard, _
        '        LtaOperationType.Transfer)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra registruota operacijų su turtu.", True)

        'End Sub

        'Private Sub FetchLimitationsForOldTransfer()

        '    If GetMaxDate(OperationChronologyType.FirstAfter, LtaOperationType.Amortization) _
        '        <> Date.MinValue Then

        '        LockDateAndFinancialData("Finansinių operacijos duomenų keisti " _
        '            & "negalima, nes nes po šios operacijos buvo skaičiuota amortizacija.", _
        '            "Operacijos data negali keistis, nes po šios operacijos " _
        '            & "buvo skaičiuota amortizacija.")

        '        Exit Sub

        '    End If

        '    Dim firstSignificantDate As Date = GetMinDate(OperationChronologyType.FirstAfter, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.ValueChange, LtaOperationType.Transfer)

        '    SetFinancialDataCanChange(firstSignificantDate, "Finansinių operacijos duomenų keisti " _
        '        & "negalima, nes vėlesne data yra registruota operacijų su turtu.", _
        '        "Maksimali leidžiama operacijos data yra " & DatePlaceHolder & ", nes šia data " _
        '        & "yra registruota operacijų su turtu.", False)

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.LastBefore, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.ValueChange, LtaOperationType.Discard, LtaOperationType.Transfer)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra registruota operacijų su turtu.", True)

        'End Sub

        'Private Sub FetchLimitationsForNewUsage()

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.Overall, _
        '        LtaOperationType.Amortization, LtaOperationType.UsingStart, LtaOperationType.UsingEnd)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra skaičiuota amortizacija ir (ar) keistas " _
        '        & "eksploatacijos statusas.", True)

        'End Sub

        'Private Sub FetchLimitationsForOldUsage()

        '    If GetMaxDate(OperationChronologyType.FirstAfter, LtaOperationType.Amortization) _
        '        <> Date.MinValue Then

        '        LockDateAndFinancialData("Finansinių operacijos duomenų keisti " _
        '            & "negalima, nes nes po šios operacijos buvo skaičiuota amortizacija.", _
        '            "Operacijos data negali keistis, nes po šios operacijos " _
        '            & "buvo skaičiuota amortizacija.")

        '        Exit Sub

        '    End If

        '    Dim firstSignificantDate As Date = GetMinDate(OperationChronologyType.FirstAfter, _
        '        LtaOperationType.UsingEnd, LtaOperationType.UsingStart)

        '    SetFinancialDataCanChange(firstSignificantDate, "Finansinių operacijos duomenų keisti " _
        '        & "negalima, nes vėlesne data yra registruota eksploatacijos statuso keitimas.", _
        '        "Maksimali leidžiama operacijos data yra " & DatePlaceHolder & ", nes šia data " _
        '        & "yra registruotas eksploatacijos statuso keitimas.", False)

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.LastBefore, _
        '        LtaOperationType.Amortization, LtaOperationType.UsingStart, LtaOperationType.UsingEnd)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra skaičiuota amortizacija ir (ar) keistas " _
        '        & "eksploatacijos statusas.", True)

        'End Sub

        'Private Sub FetchLimitationsForNewValueChange()

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.Overall, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.Amortization, LtaOperationType.ValueChange, LtaOperationType.Discard, _
        '        LtaOperationType.Transfer)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra registruota operacijų su turtu.", True)

        'End Sub

        'Private Sub FetchLimitationsForOldValueChange()

        '    If GetMaxDate(OperationChronologyType.FirstAfter, LtaOperationType.Amortization) _
        '        <> Date.MinValue Then

        '        LockDateAndFinancialData("Finansinių operacijos duomenų keisti " _
        '            & "negalima, nes nes po šios operacijos buvo skaičiuota amortizacija.", _
        '            "Operacijos data negali keistis, nes po šios operacijos " _
        '            & "buvo skaičiuota amortizacija.")

        '        Exit Sub

        '    End If

        '    Dim firstSignificantDate As Date = GetMinDate(OperationChronologyType.FirstAfter, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.ValueChange, LtaOperationType.Discard, LtaOperationType.Transfer)

        '    SetFinancialDataCanChange(firstSignificantDate, "Finansinių operacijos duomenų keisti " _
        '        & "negalima, nes vėlesne data yra registruota operacijų su turtu.", _
        '        "Maksimali leidžiama operacijos data yra " & DatePlaceHolder & ", nes šia data " _
        '        & "yra registruota operacijų su turtu.", False)

        '    Dim lastSignificantDate As Date = GetMaxDate(OperationChronologyType.LastBefore, _
        '        LtaOperationType.AccountChange, LtaOperationType.AcquisitionValueIncrease, _
        '        LtaOperationType.Amortization, LtaOperationType.ValueChange, LtaOperationType.Discard, _
        '        LtaOperationType.Transfer)

        '    SetMinDateApplicable(lastSignificantDate, "Minimali leidžiama operacijos data yra " _
        '        & DatePlaceHolder & ", nes prieš tai yra registruota operacijų su turtu.", True)

        'End Sub

#End Region

    End Class

End Namespace