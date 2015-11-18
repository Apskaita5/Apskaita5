Imports System.Reflection

''' <summary>
''' Object represents a collection of any business objects. 
''' Object is used to fetch several business objects at a time (for printing, emailing, etc.).
''' </summary>
''' <typeparam name="T">A type of business objects in the collection.</typeparam>
''' <remarks>Business objects must implement a public static method "CanGetObject"
''' and a nonpublic static method "GetObjectTypeNameOnServer" in order for 
''' the collection to be able to fetch them.</remarks>
<Serializable()> _
Public Class BusinessObjectCollection(Of T)
    Inherits BusinessBase(Of BusinessObjectCollection(Of T))

#Region " Business Methods "

    Private ReadOnly _Guid As Guid = Guid.NewGuid
    Private _Result As List(Of T) = Nothing
    Private _SkipedObjects As String = ""
    Private _SkipedObjectsCount As Integer = 0


    ''' <summary>
    ''' A resulting list of business objects.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property Result() As List(Of T)
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _Result
        End Get
    End Property

    ''' <summary>
    ''' A description of skiped (failed to fetch) objects.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property SkipedObjects() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _SkipedObjects
        End Get
    End Property

    ''' <summary>
    ''' A number of the requested objects that were skiped (failed to fetch).
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property SkipedObjectsCount() As Integer
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _SkipedObjectsCount
        End Get
    End Property


    ''' <summary>
    ''' Gets a BusinessObjectCollection (a value in the KeyValuePair) grouped by email (a key in the KeyValuePair).
    ''' </summary>
    ''' <param name="skipedDocuments">Returns a human readable description of 
    ''' the documents (business objects), that have no client assigned, 
    ''' and the clients, that have no email assigned.</param>
    ''' <remarks></remarks>
    Public Function GetListForEachClientEmail(ByRef skipedDocuments As String) As List(Of KeyValuePair(Of String, BusinessObjectCollection(Of T)))

        If Not GetType(IClientEmailProvider).IsAssignableFrom(GetType(T)) Then
            Throw New InvalidOperationException(My.Resources.BusinessObjectCollection_IClientEmailProviderNotImplemented)
        End If

        Dim groupedResult As New List(Of KeyValuePair(Of String, BusinessObjectCollection(Of T)))
        Dim clientsWithoutEmail As New List(Of String)
        Dim documentsWithoutClient As New List(Of String)

        For Each i As IClientEmailProvider In _Result

            If StringIsNullOrEmpty(i.GetClientName) Then

                documentsWithoutClient.Add(i.ToString)

            ElseIf StringIsNullOrEmpty(i.GetClientEmail) Then

                If Not clientsWithoutEmail.Contains(i.GetClientName.Trim) Then
                    clientsWithoutEmail.Add(i.GetClientName.Trim)
                End If

            Else

                Dim isFound As Boolean = False
                Dim currentEmail As String = i.GetClientEmail
                For Each k As KeyValuePair(Of String, BusinessObjectCollection(Of T)) In groupedResult
                    If k.Key.Trim.ToLower = currentEmail.Trim.ToLower Then
                        k.Value.Result.Add(DirectCast(i, T))
                        isFound = True
                        Exit For
                    End If
                Next

                If Not isFound Then

                    Dim newEntry As New KeyValuePair(Of String, BusinessObjectCollection(Of T)) _
                        (currentEmail.Trim.ToLower, Me.NewBusinessObjectCollection(DirectCast(i, T)))
                    groupedResult.Add(newEntry)

                End If

            End If

        Next

        skipedDocuments = ""
        If clientsWithoutEmail.Count > 0 Then
            skipedDocuments = AddWithNewLine(skipedDocuments, String.Format( _
                My.Resources.BusinessObjectCollection_ClientsWithoutEmail, _
                String.Join(", ", clientsWithoutEmail.ToArray)), False)
        End If
        If documentsWithoutClient.Count > 0 Then
            skipedDocuments = AddWithNewLine(skipedDocuments, String.Format( _
                My.Resources.BusinessObjectCollection_DocumentsWithoutClient, _
                String.Join(", ", documentsWithoutClient.ToArray)), False)
        End If

        Return groupedResult

    End Function

    ''' <summary>
    ''' Gets a BusinessObjectCollection (a value in the KeyValuePair) grouped by client (a person name; a key in the KeyValuePair).
    ''' </summary>
    ''' <param name="skipedDocuments">Returns a human readable description of 
    ''' the documents (business objects), that have no client assigned.</param>
    ''' <remarks></remarks>
    Public Function GetListForEachClient(ByRef skipedDocuments As String) As List(Of KeyValuePair(Of String, BusinessObjectCollection(Of T)))

        If Not GetType(IClientEmailProvider).IsAssignableFrom(GetType(T)) Then
            Throw New InvalidOperationException(My.Resources.BusinessObjectCollection_IClientEmailProviderNotImplemented)
        End If

        Dim groupedResult As New List(Of KeyValuePair(Of String, BusinessObjectCollection(Of T)))
        Dim documentsWithoutClient As New List(Of String)

        For Each i As IClientEmailProvider In _Result

            If StringIsNullOrEmpty(i.GetClientName) Then

                documentsWithoutClient.Add(i.ToString)

            Else

                Dim isFound As Boolean = False
                Dim currentName As String = i.GetClientName
                For Each k As KeyValuePair(Of String, BusinessObjectCollection(Of T)) In groupedResult
                    If k.Key.Trim.ToLower = currentName.Trim.ToLower Then
                        k.Value.Result.Add(DirectCast(i, T))
                        isFound = True
                        Exit For
                    End If
                Next

                If Not isFound Then

                    Dim newEntry As New KeyValuePair(Of String, BusinessObjectCollection(Of T)) _
                        (currentName.Trim, Me.NewBusinessObjectCollection(DirectCast(i, T)))
                    groupedResult.Add(newEntry)

                End If

            End If

        Next

        skipedDocuments = ""
        If documentsWithoutClient.Count > 0 Then
            skipedDocuments = AddWithNewLine(skipedDocuments, String.Format( _
                My.Resources.BusinessObjectCollection_DocumentsWithoutClient, _
                String.Join(", ", documentsWithoutClient.ToArray)), False)
        End If

        Return groupedResult

    End Function

    Private Function NewBusinessObjectCollection(ByVal firstItem As T) As BusinessObjectCollection(Of T)
        Dim newCollection As New BusinessObjectCollection(Of T)
        newCollection._Result = New List(Of T)
        newCollection._Result.Add(firstItem)
        Return newCollection
    End Function


    Protected Overrides Function GetIdValue() As Object
        Return _Guid
    End Function

    Public Overrides Function ToString() As String
        Return Me.GetType.FullName
    End Function

#End Region

#Region " Authorization Rules "

    Protected Overrides Sub AddAuthorizationRules()

    End Sub

    Public Function CanGetObject() As Boolean

        Try
            Dim MI As MethodInfo = GetType(T).GetMethod("CanGetObject", _
                BindingFlags.Public Or BindingFlags.Static)
            Return DirectCast(MI.Invoke(Nothing, Nothing), Boolean)
        Catch ex As Exception
            Throw New NotImplementedException(String.Format(My.Resources.Common_MethodNotImplementedForType, _
                GetType(T).FullName, "CanGetObject"), ex)
        End Try

    End Function

#End Region

#Region " Factory Methods "

    ''' <summary>
    ''' Gets a collection of business objects of type C by the provided <paramref name="ids">id's</paramref>.
    ''' </summary>
    ''' <param name="ids">ID's of the business objects to fetch.</param>
    ''' <remarks></remarks>
    Public Shared Function GetBusinessObjectCollection(ByVal ids As Integer()) As BusinessObjectCollection(Of T)
        Return DataPortal.Fetch(Of BusinessObjectCollection(Of T))(New Criteria(ids))
    End Function


    Private Sub New()
        ' require use of factory methods
    End Sub

#End Region

#Region " Data Access "

    <Serializable()> _
    Private Class Criteria
        Private _IDs As Integer()
        Public ReadOnly Property IDs() As Integer()
            Get
                Return _IDs
            End Get
        End Property
        Public Sub New(ByVal idArray As Integer())
            _IDs = idArray
        End Sub
    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

        If Not CanGetObject() Then Throw New System.Security.SecurityException( _
            My.Resources.Common_SecuritySelectDenied)

        If criteria.IDs Is Nothing OrElse criteria.IDs.Length < 1 Then
            Throw New ArgumentException(My.Resources.BusinessObjectCollection_CriteriaNull)
        End If

        Dim MI As MethodInfo = GetFactoryMethod()

        _Result = New List(Of T)
        _SkipedObjects = ""
        _SkipedObjectsCount = 0

        For Each id As Integer In criteria.IDs

            Try
                _Result.Add(DirectCast(MI.Invoke(Nothing, New Object() {id}), T))
            Catch ex As Exception
                _SkipedObjects = AddWithNewLine(_SkipedObjects, String.Format( _
                    My.Resources.BusinessObjectCollection_FailedToFetchObject, _
                    id.ToString, ex.Message), False)
                _SkipedObjectsCount += 1
            End Try

        Next

    End Sub


    Private Function GetFactoryMethod() As MethodInfo

        Dim methodName As String = "Get" & GetType(T).Name & "OnServer"

        Dim MI As MethodInfo = Nothing

        Try
            MI = GetType(T).GetMethod(methodName, BindingFlags.NonPublic Or BindingFlags.Static)
        Catch ex As Exception
        End Try
        If MI Is Nothing Then
            Throw New NotImplementedException(String.Format(My.Resources.Common_MethodNotImplementedForType, _
                GetType(T).FullName, methodName))
        End If

        Return MI

    End Function

#End Region

End Class