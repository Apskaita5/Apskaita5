Imports System.Security

Namespace Assets

    ''' <summary>
    ''' Represents a helper object that could be used to fetch a long term asset
    ''' operation general data by a journal entry ID, operation ID or a
    ''' complex operation ID.
    ''' </summary>
    ''' <remarks>Used to provide long term asset operation access from other
    ''' program modules, e.g. to open operation form from a general ledger.</remarks>
    <Serializable()> _
    Public NotInheritable Class OperationInfo
        Inherits ReadOnlyBase(Of OperationInfo)

#Region " Business Methods "

        ''' <summary>
        ''' Represents possible id types that could be used to fetch
        ''' a long term asset operation data,
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum OperationInfoFetchType
            ''' <summary>
            ''' Corresponds to an <see cref="General.JournalEntry.ID">ID of the journal entry</see>.
            ''' </summary>
            ''' <remarks></remarks>
            JournalEntryID
            ''' <summary>
            ''' Corresponds to a long term asset operation ID, 
            ''' e.g. <see cref="OperationAmortization.ID">OperationAmortization.ID</see>.
            ''' </summary>
            ''' <remarks></remarks>
            OperationID
            ''' <summary>
            ''' Corresponds to a long term asset complex operation ID, 
            ''' e.g. <see cref="ComplexOperationAmortization.ID">ComplexOperationAmortization.ID</see>.
            ''' </summary>
            ''' <remarks></remarks>
            ComplexOperationID
        End Enum

        Private _ID As Integer = 0
        Private _ComplexOperationID As Integer = 0
        Private _JournalEntryID As Integer = 0
        Private _AssetID As Integer = 0
        Private _Type As LtaOperationType = LtaOperationType.Discard
        Private _AccountType As LtaAccountChangeType = LtaAccountChangeType.AcquisitionAccount
        Private _JournalEntryType As DocumentType = DocumentType.None
        Private _Date As Date = Today
        Private _Content As String = ""
        Private _JournalEntryContent As String = ""


        ''' <summary>
        ''' Gets an ID of the operation that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the long term asset complex operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.IsComplexAct.</remarks>
        Public ReadOnly Property ComplexOperationID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexOperationID
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.JournalEntry.ID">ID of the journal entry</see> 
        ''' that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.JE_ID.</remarks>
        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="LongTermAsset.ID">an ID of the long term asset</see> that is affected by the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.T_ID.</remarks>
        Public ReadOnly Property AssetID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetID
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.OperationType.</remarks>
        Public ReadOnly Property [Type]() As LtaOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the long term asset account change operation.
        ''' </summary>
        ''' <remarks>Only relevant when the <see cref="Type">Type</see>
        ''' is set to <see cref="LtaOperationType.AccountChange">LtaOperationType.AccountChange</see>.
        ''' Value is stored in the database field turtas_op.AccountOperationType.</remarks>
        Public ReadOnly Property AccountType() As LtaAccountChangeType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountType
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the document that owns the journal entry 
        ''' that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.DocType">JournalEntry.DocType</see>.</remarks>
        Public ReadOnly Property JournalEntryType() As DocumentType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryType
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.OperationDate.</remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a content (description) of the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.Content.</remarks>
        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a content of the journal entry that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Content">JournalEntry.Content</see>.</remarks>
        Public ReadOnly Property JournalEntryContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryContent.Trim
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            If ComplexOperationID > 0 Then Return ComplexOperationID
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            If _ComplexOperationID > 0 Then
                Return String.Format(My.Resources.Assets_OperationInfo_ToStringComplex, _
                    _Date.ToString("yyyy-MM-dd"), Utilities.ConvertLocalizedName( _
                    _Type), _ComplexOperationID.ToString())
            Else
                Return String.Format(My.Resources.Assets_OperationInfo_ToString, _
                    _Date.ToString("yyyy-MM-dd"), Utilities.ConvertLocalizedName( _
                    _Type), _ID.ToString())
            End If
        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAssetOperation1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new OperationInfo instance, i.e. readonly long term asset operation data.
        ''' </summary>
        ''' <param name="id">A lookup ID as defined by fetchType.</param>
        ''' <param name="fetchType">A type of the lookup ID.</param>
        ''' <remarks></remarks>
        Public Shared Function GetOperationInfo(ByVal id As Integer, _
            ByVal fetchType As OperationInfoFetchType) As OperationInfo
            Return DataPortal.Fetch(Of OperationInfo)(New Criteria(id, fetchType))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ID As Integer
            Private _FetchType As OperationInfoFetchType
            Public ReadOnly Property ID() As Integer
                Get
                    Return _ID
                End Get
            End Property
            Public ReadOnly Property FetchType() As OperationInfoFetchType
                Get
                    Return _FetchType
                End Get
            End Property
            Public Sub New(ByVal nID As Integer, ByVal nFetchType As OperationInfoFetchType)
                _ID = nID
                _FetchType = nFetchType
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim persistence As OperationPersistenceObject
            If criteria.FetchType = OperationInfoFetchType.JournalEntryID Then

                Dim operationID As Integer = OperationPersistenceObject. _
                    GetOperationIdByJournalEntry(criteria.ID)
                persistence = OperationPersistenceObject.GetOperationPersistenceObject( _
                    operationID, LtaOperationType.Discard, False)

            ElseIf criteria.FetchType = OperationInfoFetchType.ComplexOperationID Then

                Dim list As List(Of OperationPersistenceObject) = _
                    OperationPersistenceObject.GetOperationPersistenceObjectList( _
                    criteria.ID, LtaOperationType.Discard, False)
                persistence = list(0)

            Else

                persistence = OperationPersistenceObject.GetOperationPersistenceObject( _
                    criteria.ID, LtaOperationType.Discard, False)

            End If

            _ID = persistence.ID
            _ComplexOperationID = persistence.ComplexActID
            _JournalEntryID = persistence.JournalEntryID
            _Date = persistence.OperationDate
            _Content = persistence.Content
            _JournalEntryContent = persistence.JournalEntryContent
            _AssetID = persistence.AssetID
            _Type = persistence.OperationType
            _AccountType = persistence.AccountOperationType
            _JournalEntryType = persistence.JournalEntryDocumentType

        End Sub

#End Region

    End Class

End Namespace