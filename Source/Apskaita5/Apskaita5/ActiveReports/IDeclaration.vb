Namespace ActiveReports

    ''' <summary>
    ''' Represents an interface required for a <see cref="Declaration">Declaration</see>.
    ''' An object that implements this interface represents a certain declaration (report to a state institution)
    ''' and holds methods that extracts declaration data from a database and maps it to a rdlc format.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IDeclaration

        ''' <summary>
        ''' Gets a name of the declaration.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property Name() As String

        ''' <summary>
        ''' Gets a start of the period that the declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidFrom() As Date

        ''' <summary>
        ''' Gets an end of the period that the declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidTo() As Date

        ''' <summary>
        ''' Gets a number of details tables within the declaration.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property DetailsTableCount() As Integer

        ''' <summary>
        ''' Gets a name of the rdlc file that should be used to print the declaration.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property RdlcFileName() As String

        ''' <summary>
        ''' Gets a ffdata format dataset.
        ''' </summary>
        ''' <param name="declarationDataSet">a declaration dataset fetched by the <see cref="GetBaseDataSet">GetBaseDataSet</see> method.</param>
        ''' <param name="preparatorName">a name of the person who prepared the declaration.</param>
        ''' <remarks></remarks>
        Function GetFfDataDataSet(ByVal declarationDataSet As DataSet, ByVal preparatorName As String) As DataSet

        ''' <summary>
        ''' Gets a declaration data from a database in a form of a dataset.
        ''' </summary>
        ''' <param name="criteria">criteria of the declaration that holds data required to fetch the declaration data</param>
        ''' <param name="warnings">output parameter containg warnings that were issued during the fetch procedure
        ''' (indicates some discrepancies in data, that are not critical for the data fetched)</param>
        ''' <remarks></remarks>
        Function GetBaseDataSet(ByVal criteria As DeclarationCriteria, ByRef warnings As String) As DataSet

        ''' <summary>
        ''' whether the criteria data is valid, i.e. good enough to proceed with fetch.
        ''' </summary>
        ''' <param name="criteria">criteria of the declaration that holds data required to fetch the declaration data</param>
        ''' <remarks></remarks>
        Function IsValid(ByVal criteria As DeclarationCriteria) As Boolean

        ''' <summary>
        ''' whether the criteria data has some discrepancies, i.e. good enough to proceed with fetch, but might hold invalid data.
        ''' </summary>
        ''' <param name="criteria">criteria of the declaration that holds data required to fetch the declaration data</param>
        ''' <remarks></remarks>
        Function HasWarnings(ByVal criteria As DeclarationCriteria) As Boolean

        ''' <summary>
        ''' Gets all the errors in the criteria data.
        ''' </summary>
        ''' <param name="criteria">criteria of the declaration that holds data required to fetch the declaration data</param>
        ''' <remarks></remarks>
        Function GetAllErrors(ByVal criteria As DeclarationCriteria) As String

        ''' <summary>
        ''' Gets all the warnings for the criteria data.
        ''' </summary>
        ''' <param name="criteria">criteria of the declaration that holds data required to fetch the declaration data</param>
        ''' <remarks></remarks>
        Function GetAllWarnings(ByVal criteria As DeclarationCriteria) As String

    End Interface

    ''' <summary>
    ''' Represents an interface required for a <see cref="Declaration">Declaration</see>.
    ''' An object that implements this interface represents a certain declaration (report to a state institution)
    ''' and holds methods that extracts declaration data from a database and maps it to a rdlc format.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface CopyOfIDeclaration

        ''' <summary>
        ''' Gets a name of the declaration.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property Name() As String

        ''' <summary>
        ''' Gets a start of the period that the declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidFrom() As Date

        ''' <summary>
        ''' Gets an end of the period that the declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidTo() As Date

        ''' <summary>
        ''' Gets a number of details tables within the declaration.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property DetailsTableCount() As Integer

        ''' <summary>
        ''' Gets a name of the rdlc file that should be used to print the declaration.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property RdlcFileName() As String

        ''' <summary>
        ''' Gets a ffdata format dataset.
        ''' </summary>
        ''' <param name="declarationDataSet">a declaration dataset fetched by the <see cref="GetBaseDataSet">GetBaseDataSet</see> method.</param>
        ''' <param name="preparatorName">a name of the person who prepared the declaration.</param>
        ''' <remarks></remarks>
        Function GetFfDataDataSet(ByVal declarationDataSet As DataSet, ByVal preparatorName As String) As DataSet

        ''' <summary>
        ''' Gets a declaration data from a database in a form of a dataset.
        ''' </summary>
        ''' <param name="criteria">criteria of the declaration that holds data required to fetch the declaration data</param>
        ''' <param name="warnings">output parameter containg warnings that were issued during the fetch procedure
        ''' (indicates some discrepancies in data, that are not critical for the data fetched)</param>
        ''' <remarks></remarks>
        Function GetBaseDataSet(ByVal criteria As DeclarationCriteria, ByRef warnings As String) As DataSet

        ''' <summary>
        ''' whether the criteria data is valid, i.e. good enough to proceed with fetch.
        ''' </summary>
        ''' <param name="criteria">criteria of the declaration that holds data required to fetch the declaration data</param>
        ''' <remarks></remarks>
        Function IsValid(ByVal criteria As DeclarationCriteria) As Boolean

        ''' <summary>
        ''' whether the criteria data has some discrepancies, i.e. good enough to proceed with fetch, but might hold invalid data.
        ''' </summary>
        ''' <param name="criteria">criteria of the declaration that holds data required to fetch the declaration data</param>
        ''' <remarks></remarks>
        Function HasWarnings(ByVal criteria As DeclarationCriteria) As Boolean

        ''' <summary>
        ''' Gets all the errors in the criteria data.
        ''' </summary>
        ''' <param name="criteria">criteria of the declaration that holds data required to fetch the declaration data</param>
        ''' <remarks></remarks>
        Function GetAllErrors(ByVal criteria As DeclarationCriteria) As String

        ''' <summary>
        ''' Gets all the warnings for the criteria data.
        ''' </summary>
        ''' <param name="criteria">criteria of the declaration that holds data required to fetch the declaration data</param>
        ''' <remarks></remarks>
        Function GetAllWarnings(ByVal criteria As DeclarationCriteria) As String

    End Interface

End Namespace