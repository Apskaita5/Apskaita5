Imports ApskaitaObjects.Documents

Namespace HelperLists

    ''' <summary>
    ''' Represents a helper object that holds dictionaries of localized names and prices.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class RegionalInfoDictionary
        Inherits ReadOnlyBase(Of RegionalInfoDictionary)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ContentDictionary As RegionalContentEntryList = Nothing
        Private _PriceDictionary As RegionalPriceEntryList = Nothing


        ''' <summary>
        ''' Gets a regional content info dictionary.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ContentDictionary() As RegionalContentEntryList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContentDictionary
            End Get
        End Property

        ''' <summary>
        ''' Gets a regional prices info dictionary.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PriceDictionary() As RegionalPriceEntryList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PriceDictionary
            End Get
        End Property


        ''' <summary>
        ''' Gets a purchase price per unit for a regionalizable object.
        ''' </summary>
        ''' <param name="objectType">Type of the regionalizable object.</param>
        ''' <param name="objectID">ID of the regionalizable object.</param>
        ''' <param name="currencyCode">Currency to look a price for.</param>
        ''' <param name="price">Returnes price per unit in <paramref name="currencyCode">selected currency</paramref>.</param>
        ''' <returns>True if the regional price is found.</returns>
        ''' <remarks></remarks>
        Public Function GetPurchasePrice(ByVal objectType As RegionalizedObjectType, _
            ByVal objectID As Integer, ByVal currencyCode As String, ByRef price As Double) As Boolean

            Dim result As RegionalPriceEntry = _PriceDictionary.GetRegionalPriceEntry( _
                objectType, objectID, currencyCode)

            price = result.ValuePerUnitPurchases

            Return (result.ObjectID > 0)

        End Function

        ''' <summary>
        ''' Gets a sale price per unit for a regionalizable object.
        ''' </summary>
        ''' <param name="objectType">Type of the regionalizable object.</param>
        ''' <param name="objectID">ID of the regionalizable object.</param>
        ''' <param name="currencyCode">Currency to look a price for.</param>
        ''' <param name="price">Returns price per unit in <paramref name="currencyCode">selected currency</paramref>.</param>
        ''' <returns>True if the regional price is found.</returns>
        ''' <remarks></remarks>
        Public Function GetSalePrice(ByVal objectType As RegionalizedObjectType, _
            ByVal objectID As Integer, ByVal currencyCode As String, ByRef price As Double) As Boolean

            Dim result As RegionalPriceEntry = _PriceDictionary.GetRegionalPriceEntry( _
                objectType, objectID, currencyCode)

            price = result.ValuePerUnitSales

            Return (result.ObjectID > 0)

        End Function

        ''' <summary>
        ''' Gets regional content info for a regionalizable object.
        ''' </summary>
        ''' <param name="objectType">Type of the regionalizable object.</param>
        ''' <param name="objectID">ID of the regionalizable object.</param>
        ''' <param name="languageCode">Language to look for.</param>
        ''' <param name="contentInvoice">Returns a regionalizable object description 
        ''' for an invoice in <paramref name="languageCode">selected language</paramref>.</param>
        ''' <param name="measureUnit">Returns a regionalizable object measure unit 
        ''' for an invoice in <paramref name="languageCode">selected language</paramref>.</param>
        ''' <param name="vatExempt">Returns a regionalizable object VAT exempt description 
        ''' for an invoice in <paramref name="languageCode">selected language</paramref>.</param>
        ''' <returns>True if the regional content info is found.</returns>
        ''' <remarks></remarks>
        Public Function GetRegionalContentInfo(ByVal objectType As RegionalizedObjectType, _
            ByVal objectID As Integer, ByVal languageCode As String, _
            ByRef contentInvoice As String, ByRef measureUnit As String, ByRef vatExempt As String) As Boolean

            Dim result As RegionalContentEntry = _ContentDictionary.GetRegionalContentEntry( _
                objectType, objectID, languageCode)

            contentInvoice = result.ContentInvoice
            measureUnit = result.MeasureUnit
            vatExempt = result.VatExempt

            Return (result.ObjectID > 0)

        End Function

        ''' <summary>
        ''' Gets VAT exempt info for a regionalizable object.
        ''' </summary>
        ''' <param name="objectType">Type of the regionalizable object.</param>
        ''' <param name="objectID">ID of the regionalizable object.</param>
        ''' <param name="languageCode">Language to look for.</param>
        ''' <param name="vatExemptLT">Returns a regionalizable object VAT exempt description 
        ''' for an invoice in base language.</param>
        ''' <param name="vatExempt">Returns a regionalizable object VAT exempt description 
        ''' for an invoice in <paramref name="languageCode">selected language</paramref>.</param>
        ''' <returns>True if any VAT exempt info is found.</returns>
        ''' <remarks></remarks>
        Public Function GetVatExempts(ByVal objectType As RegionalizedObjectType, _
            ByVal objectID As Integer, ByVal languageCode As String, _
            ByRef vatExemptLT As String, ByRef vatExempt As String) As Boolean

            Dim hasValues As Boolean = False

            Dim result As RegionalContentEntry = _ContentDictionary.GetRegionalContentEntry( _
                objectType, objectID, languageCode)

            hasValues = (result.ObjectID > 0 AndAlso Not StringIsNullOrEmpty(result.VatExempt))

            If LanguagesEquals(result.LanguageCode, LanguageCodeLith, LanguageCodeLith) Then

                vatExemptLT = result.VatExempt
                vatExempt = ""

            Else

                vatExempt = result.VatExempt

                result = _ContentDictionary.GetRegionalContentEntry(objectType, objectID, LanguageCodeLith)

                vatExemptLT = result.VatExempt

                If Not hasValues AndAlso result.ObjectID > 0 AndAlso Not StringIsNullOrEmpty(vatExemptLT) Then
                    hasValues = True
                End If

            End If

            Return hasValues

        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return Me.GetType().FullName
        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.RegionalInfoDictionary1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a current RegionalInfoDictionary from database.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetList() As RegionalInfoDictionary

            Dim result As RegionalInfoDictionary = CacheManager.GetItemFromCache(Of RegionalInfoDictionary)( _
                GetType(RegionalInfoDictionary), Nothing)

            If result Is Nothing Then
                result = DataPortal.Fetch(Of RegionalInfoDictionary)(New Criteria())
                CacheManager.AddCacheItem(GetType(RegionalInfoDictionary), result, Nothing)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a filtered view of the current RegionalInfoDictionary.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetCachedFilteredList() As Csla.FilteredBindingList(Of RegionalInfoDictionary)

            Dim pseudoList As New List(Of RegionalInfoDictionary)
            pseudoList.Add(RegionalInfoDictionary.GetList())

            Return New Csla.FilteredBindingList(Of RegionalInfoDictionary)(pseudoList, Nothing)

        End Function

        ''' <summary>
        ''' Invalidates the current RegionalInfoDictionary cache 
        ''' so that the next <see cref="GetList">GetList</see> call would hit the database.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Sub InvalidateCache()
            CacheManager.InvalidateCache(GetType(RegionalInfoDictionary))
        End Sub

        ''' <summary>
        ''' Returnes true if the cache does not contain a current RegionalInfoDictionary.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function CacheIsInvalidated() As Boolean
            Return CacheManager.CacheIsInvalidated(GetType(RegionalInfoDictionary))
        End Function

        ''' <summary>
        ''' Returns true if the collection is common across all the databases.
        ''' I.e. cache is not to be cleared on changing databases.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function IsApplicationWideCache() As Boolean
            Return False
        End Function

        ''' <summary>
        ''' Gets a current RegionalInfoDictionary from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be called server side.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function GetListOnServer() As RegionalInfoDictionary
            Dim result As New RegionalInfoDictionary
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function

        ''' <summary>
        ''' Gets a current RegionalInfoDictionary from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be invoked server side.</remarks>
        Friend Shared Function GetListChild() As RegionalInfoDictionary
            Dim result As New RegionalInfoDictionary
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Public Sub New()
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            _ContentDictionary = RegionalContentEntryList.GetRegionalContentEntryList()
            _PriceDictionary = RegionalPriceEntryList.GetRegionalPriceEntryList()

        End Sub

#End Region

    End Class

End Namespace
