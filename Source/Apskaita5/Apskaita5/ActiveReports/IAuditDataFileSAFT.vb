Namespace ActiveReports

    ''' <summary>
    ''' Represents an interface required for a SAF-T audit data file generation.
    ''' An object, that implements this interface, represents a SAF-T audit data file in XMl format (SAF-T)
    ''' (report to a state institution).
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IAuditFileSAFT

        ''' <summary>
        ''' Gets a name of the particular SAF-T audit data file version.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property Name() As String

        ''' <summary>
        ''' Gets a start of the period that the SAF-T audit data file format is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidFrom() As Date

        ''' <summary>
        ''' Gets an end of the period that the SAF-T audit data file is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidTo() As Date

        ''' <summary>
        ''' Gets a name of the xsd file that defines the SAF-T audit data file xml requirements.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property XsdFileName() As String

        ''' <summary>
        ''' Gets a name of the xsd target namespace that is used to validate the generated xml.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property TargetNameSpace() As String

        ''' <summary>
        ''' Gets an XML representation of the report.
        ''' </summary>
        ''' <param name="softwareVersion">a current version of the application</param>
        ''' <param name="periodStart">a starting date of the report data period</param>
        ''' <param name="periodEnd">an ending date of the report data period</param>
        ''' <remarks></remarks>
        Function GetXmlString(ByVal softwareVersion As String, ByVal periodStart As Date,
            ByVal periodEnd As Date) As String

    End Interface

End Namespace