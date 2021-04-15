Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="General.Account">ledger account</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table saskaitupl.</remarks>
    <Serializable()> _
    Public NotInheritable Class AccountInfo
        Inherits ReadOnlyBase(Of AccountInfo)
        Implements IValueObject, IComparable

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _ID As Long = 0
        Private _Name As String = ""
        Private _AssociatedReportItem As String = ""
        Private _Class As Byte = 0


        ''' <summary>
        ''' Whether an object is a place holder (does not represent a real account).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObject.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ID > 0
            End Get
        End Property

        ''' <summary>
        ''' Account ID - <see cref="Long">Long</see> number that identifies account.
        ''' Equals zero for a placeholder object.
        ''' </summary>
        ''' <value></value>
        ''' <remarks>Corresponds to <see cref="General.Account.ID">Account.ID</see> property.
        ''' Value is stored in the database field saskaitupl.Saskaitosnr.</remarks>
        Public ReadOnly Property ID() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Account name - a short description of an account.
        ''' Equals empty string for a placeholder.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.Account.Name">Account.Name</see> property.
        ''' Value is stored in the database field saskaitupl.Saskaita.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a full description of an account in format "{ID} {Name}".
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property FullName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _ID > 0 Then Return ""
                Return String.Format("{0} {1}", _ID.ToString, _Name.Trim)
            End Get
        End Property

        ''' <summary>
        ''' Associated <see cref="General.ConsolidatedReportItem">ConsolidatedReportItem</see>. 
        ''' </summary>
        ''' <value></value>
        ''' <remarks>Corresponds to <see cref="General.Account.AssociatedReportItem">Account.AssociatedReportItem</see> property.
        ''' Value is stored in the database field saskaitupl.Rusis.</remarks>
        Public ReadOnly Property AssociatedReportItem() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssociatedReportItem.Trim
            End Get
        End Property

        ''' <summary>
        ''' Class of account http://en.wikipedia.org/wiki/Debits_and_credits#Accounts_pertaining_to_the_five_accounting_elements. 
        ''' Maped by the first number ("prefix") in the <see cref="ID" /> and values of <see cref="General.Company.AccountClassPrefix11" />,
        ''' <see cref="General.Company.AccountClassPrefix12" />, <see cref="General.Company.AccountClassPrefix21" />, etc.
        ''' </summary>
        ''' <value></value>
        ''' <returns>Base class of account:
        ''' 0 - Invalid account, i.e. not <see cref="ID">ID</see> > 0.
        ''' 1 - Long term assets;
        ''' 2 - Short term assets;
        ''' 3 - Equity;
        ''' 4 - Liabilities;
        ''' 5 - Income/Revenue;
        ''' 6 - Expenses.</returns>
        ''' <remarks>Maping method - <see cref="General.Account.GetAccountClass" />.</remarks>
        Public ReadOnly Property [Class]() As Byte
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Class
            End Get
        End Property


        Public Shared Operator =(ByVal a As AccountInfo, ByVal b As AccountInfo) As Boolean

            Dim aId, bId As Long
            If a Is Nothing OrElse a.IsEmpty Then
                aId = 0
            Else
                aId = a.ID
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bId = 0
            Else
                bId = b.ID
            End If

            Return aId = bId

        End Operator

        Public Shared Operator <>(ByVal a As AccountInfo, ByVal b As AccountInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As AccountInfo, ByVal b As AccountInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString > bToString

        End Operator

        Public Shared Operator <(ByVal a As AccountInfo, ByVal b As AccountInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString < bToString

        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim tmp As AccountInfo = TryCast(obj, AccountInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return String.Format("{0} {1}", _ID.ToString, _Name.Trim)
        End Function

#End Region

#Region " Factory Methods "

        Private Shared _Empty As AccountInfo = Nothing

        ''' <summary>
        ''' Gets an empty account info (placeholder).
        ''' </summary>
        Public Shared Function Empty() As AccountInfo
            If _Empty Is Nothing Then
                _Empty = New AccountInfo
            End If
            Return _Empty
        End Function

        ''' <summary>
        ''' Gets an existing account info by a database query.
        ''' </summary>
        ''' <param name="dr">DataRow containing account data.</param>
        Friend Shared Function GetAccountInfo(ByVal dr As DataRow) As AccountInfo
            Return New AccountInfo(dr)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CLongSafe(dr.Item(0), 0)
            _Name = CStrSafe(dr.Item(1)).Trim
            _AssociatedReportItem = CStrSafe(dr.Item(4)).Trim
            _Class = General.Account.GetAccountClass(_ID)

        End Sub

#End Region

    End Class

End Namespace
