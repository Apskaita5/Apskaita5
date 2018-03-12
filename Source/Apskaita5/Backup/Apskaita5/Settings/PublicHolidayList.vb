Imports ApskaitaObjects.Settings.XmlProxies

Namespace Settings

    ''' <summary>
    ''' Represents a collection of public holidays dates.
    ''' </summary>
    ''' <remarks>Exists a single instance accross all of the databases.
    ''' Should only be used as a child of <see cref="CommonSettings">CommonSettings</see>
    ''' Persisted using xml proxies as a part of <see cref="CommonSettings">CommonSettings</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class PublicHolidayList
        Inherits BusinessListBase(Of PublicHolidayList, PublicHoliday)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim newItem As PublicHoliday = PublicHoliday.NewPublicHoliday
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
            For Each h As PublicHoliday In Me
                If h.HasWarnings Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetPublicHolidayList(ByVal proxyList As List(Of PublicHolidayProxy)) As PublicHolidayList
            Return New PublicHolidayList(proxyList)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal proxyList As List(Of PublicHolidayProxy))
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            Fetch(proxyList)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal proxyList As List(Of PublicHolidayProxy))

            RaiseListChangedEvents = False

            For Each proxy As PublicHolidayProxy In proxyList
                Add(PublicHoliday.GetPublicHoliday(proxy))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Function GetProxyList(ByVal markItemsOld As Boolean) As List(Of PublicHolidayProxy)

            Dim result As New List(Of PublicHolidayProxy)

            RaiseListChangedEvents = False

            If markItemsOld Then DeletedList.Clear()

            For Each n As PublicHoliday In Me
                result.Add(n.GetProxy(markItemsOld))
            Next

            RaiseListChangedEvents = True

            Return result

        End Function

#End Region

    End Class

End Namespace