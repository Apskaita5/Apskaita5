Imports ApskaitaObjects.Settings.XmlProxies

Namespace Settings

    ''' <summary>
    ''' Represents a collection of various codes with names/descriptions 
    ''' that are used by business objects, e.g. income type for various tax declarations.
    ''' </summary>
    ''' <remarks>Exists a single instance accross all of the databases.
    ''' Should only be used as a child of <see cref="CommonSettings">CommonSettings</see>
    ''' Persisted using xml proxies as a part of <see cref="CommonSettings">CommonSettings</see>.</remarks>
    <Serializable()> _
    Public Class CodeList
        Inherits BusinessListBase(Of CodeList, Code)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim newItem As Code = Code.NewCode
            Me.Add(newItem)
            Return newItem
        End Function

        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)
            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            Return result
        End Function

        Public Function HasWarnings() As Boolean
            For Each h As Code In Me
                If h.HasWarnings Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetCodeList(ByVal proxyList As List(Of CodeProxy)) As CodeList
            Dim result As CodeList = New CodeList(proxyList)
            Return result
        End Function

        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal proxyList As List(Of CodeProxy))
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            Fetch(proxyList)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal proxyList As List(Of CodeProxy))

            RaiseListChangedEvents = False

            For Each proxy As CodeProxy In proxyList
                Add(Code.GetCode(proxy))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Function GetProxyList(ByVal markItemsOld As Boolean) As List(Of CodeProxy)

            RaiseListChangedEvents = False

            Dim result As New List(Of CodeProxy)

            If markItemsOld Then Me.DeletedList.Clear()

            For Each c As Code In Me
                result.Add(c.GetProxy(markItemsOld))
            Next

            RaiseListChangedEvents = True

            Return result

        End Function

#End Region

    End Class

End Namespace