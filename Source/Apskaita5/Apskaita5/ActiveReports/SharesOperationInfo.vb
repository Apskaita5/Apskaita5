Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of a <see cref="SharesOperationInfoList">SharesOperationInfoList</see> report.
    ''' Contains information about <see cref="General.SharesOperation">shares operations</see>.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="SharesOperationInfoList">SharesOperationInfoList</see>.</remarks>
    <Serializable()>
    Public NotInheritable Class SharesOperationInfo
        Inherits ReadOnlyBase(Of SharesOperationInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Date As Date = Today
        Private _DocumentDate As Date = Today
        Private _DocumentName As String = ""
        Private _DocumentNumber As String = ""
        Private _Remarks As String = ""
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now


        ''' <summary>
        ''' Gets an ID of the operation (assigned by DB AUTO_INCREMENT).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.SharesOperation.ID">SharesOperation.ID</see>.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a registration date of the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.SharesOperation.Date">SharesOperation.Date</see>.</remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a substantiating document date.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.SharesOperation.DocumentDate">SharesOperation.DocumentDate</see>.</remarks>
        Public ReadOnly Property DocumentDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DocumentDate
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the substantiating document.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.SharesOperation.DocumentName">SharesOperation.DocumentName</see>.</remarks>
        Public ReadOnly Property DocumentName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DocumentName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a number of the substantiating document.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.SharesOperation.DocumentNumber">SharesOperation.DocumentNumber</see>.</remarks>
        Public ReadOnly Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DocumentNumber.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets arbitrary remarks regarding the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.SharesOperation.Remarks">SharesOperation.Remarks</see>.</remarks>
        Public ReadOnly Property Remarks() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Remarks.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was inserted into the database.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.SharesOperation.InsertDate">SharesOperation.InsertDate</see>.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was last updated.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.SharesOperation.UpdateDate">SharesOperation.UpdateDate</see>.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _UpdateDate
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_SharesOperationInfo_ToString,
                _Date.ToString("yyyy-MM-dd"), _DocumentDate.ToString("yyyy-MM-dd"),
                _DocumentName, _DocumentNumber)

        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetSharesOperationInfo(ByVal dr As DataRow) As SharesOperationInfo
            Return New SharesOperationInfo(dr)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(0), 0)
            _Date = CDateSafe(dr.Item(1), Today)
            _DocumentDate = CDateSafe(dr.Item(2), Today)
            _DocumentName = CStrSafe(dr.Item(3)).Trim
            _DocumentNumber = CStrSafe(dr.Item(4)).Trim
            _Remarks = CStrSafe(dr.Item(5)).Trim
            _InsertDate = CTimeStampSafe(dr.Item(6))
            _UpdateDate = CTimeStampSafe(dr.Item(7))

        End Sub

#End Region

    End Class

End Namespace
