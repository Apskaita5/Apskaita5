Imports System.Security

Namespace ActiveReports

    ''' <summary>
    ''' Represents a VAT declaration schemas report, contains information about all the
    ''' <see cref="Documents.VatDeclarationSchema">VAT declaration schemas</see> used in the company.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class VatDeclarationSchemaInfoItemList
        Inherits ReadOnlyListBase(Of VatDeclarationSchemaInfoItemList, VatDeclarationSchemaInfoItem)

#Region " Business Methods "

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("ActiveReports.VatDeclarationSchemaInfoItemList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new instance of VAT declaration schemas report from the database.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function GetVatDeclarationSchemaInfoItemList() As VatDeclarationSchemaInfoItemList
            Return DataPortal.Fetch(Of VatDeclarationSchemaInfoItemList)(New Criteria())
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

            If Not CanGetObject() Then Throw New SecurityException(My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchVatDeclarationSchemaInfoItemList")

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Add(VatDeclarationSchemaInfoItem.GetVatDeclarationSchemaInfoItem(dr))
                Next

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace