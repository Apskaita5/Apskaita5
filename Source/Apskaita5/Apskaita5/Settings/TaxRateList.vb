Imports ApskaitaObjects.Settings.XmlProxies

Namespace Settings

    ''' <summary>
    ''' Represents a collection of tax rates that could be used in business objects.
    ''' </summary>
    ''' <remarks>Exists a single instance accross all of the databases.
    ''' Should only be used as a child of <see cref="CommonSettings">CommonSettings</see>
    ''' Persisted using xml proxies as a part of <see cref="CommonSettings">CommonSettings</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class TaxRateList
        Inherits BusinessListBase(Of TaxRateList, TaxRate)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim newItem As TaxRate = TaxRate.NewTaxRate()
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
            For Each h As TaxRate In Me
                If h.HasWarnings Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetTaxRateList(ByVal proxyList As List(Of TaxRateProxy)) As TaxRateList
            Return New TaxRateList(proxyList)
        End Function


        Private Sub New()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            MarkAsChild()
        End Sub

        Private Sub New(ByVal proxyList As List(Of TaxRateProxy))
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            MarkAsChild()
            Fetch(proxyList)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal proxyList As List(Of TaxRateProxy))

            RaiseListChangedEvents = False

            For Each proxy As TaxRateProxy In proxyList
                Add(TaxRate.GetTaxRate(proxy))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Function GetProxyList(ByVal markItemsOld As Boolean) As List(Of TaxRateProxy)

            Dim result As New List(Of TaxRateProxy)

            RaiseListChangedEvents = False

            If markItemsOld Then DeletedList.Clear()

            For Each n As TaxRate In Me
                result.Add(n.GetProxy(markItemsOld))
            Next

            RaiseListChangedEvents = True

            Return result

        End Function

#End Region

    End Class

End Namespace