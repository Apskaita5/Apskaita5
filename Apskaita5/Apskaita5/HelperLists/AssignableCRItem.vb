﻿Namespace HelperLists

    ''' <summary>
    ''' Represents a value object containing a name and an ID of available 
    ''' <see cref="General.ConsolidatedReportItem">ConsolidatedReportItem</see>
    ''' that could be assigned to a <see cref="General.Account.AssociatedReportItem">Account.AssociatedReportItem</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class AssignableCRItem
        Inherits ReadOnlyBase(Of AssignableCRItem)
        Implements IValueObjectIsEmpty, IComparable

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""


        ''' <summary>
        ''' Gets an ID of a <see cref="General.ConsolidatedReportItem">ConsolidatedReportItem</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.ConsolidatedReportItem.ID">ConsolidatedReportItem.ID</see>.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of a <see cref="General.ConsolidatedReportItem">ConsolidatedReportItem</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.ConsolidatedReportItem.Name">ConsolidatedReportItem.Name</see>.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Whether an object is a place holder (does not represent a real consolidated report item).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObjectIsEmpty.IsEmpty
            Get
                Return (Not _ID > 0)
            End Get
        End Property


        Public Shared Operator =(ByVal a As AssignableCRItem, ByVal b As AssignableCRItem) As Boolean

            If a Is Nothing AndAlso b Is Nothing Then Return True
            If a Is Nothing OrElse b Is Nothing Then Return False

            Return a.ID = b.ID

        End Operator

        Public Shared Operator <>(ByVal a As AssignableCRItem, ByVal b As AssignableCRItem) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As AssignableCRItem, ByVal b As AssignableCRItem) As Boolean
            If a Is Nothing Then Return False
            If b Is Nothing Then Return True
            Return a.ToString > b.ToString
        End Operator

        Public Shared Operator <(ByVal a As AssignableCRItem, ByVal b As AssignableCRItem) As Boolean
            If a Is Nothing And b Is Nothing Then Return False
            If a Is Nothing Then Return True
            If b Is Nothing Then Return False
            Return a.ToString < b.ToString
        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim tmp As AssignableCRItem = TryCast(obj, AssignableCRItem)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return _Name
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewAssignableCRItem() As AssignableCRItem
            Return New AssignableCRItem()
        End Function

        Friend Shared Function GetAssignableCRItem(ByVal dr As DataRow, _
            ByVal offset As Integer) As AssignableCRItem
            Return New AssignableCRItem(dr, offset)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal offset As Integer)
            Fetch(dr, offset)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal offset As Integer)

            _ID = CIntSafe(dr.Item(0 + offset), 0)
            _Name = CStrSafe(dr.Item(1 + offset)).Trim

        End Sub

#End Region

    End Class

End Namespace