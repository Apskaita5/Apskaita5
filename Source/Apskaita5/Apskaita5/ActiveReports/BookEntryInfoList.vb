﻿Imports ApskaitaObjects.General
Namespace ActiveReports

    ''' <summary>
    ''' Represents a list of <see cref="General.BookEntry">book entry</see> items for <see cref="BookEntryInfoListParent">BookEntryInfoListParent</see>.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="BookEntryInfoListParent">BookEntryInfoListParent</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class BookEntryInfoList
        Inherits ReadOnlyListBase(Of BookEntryInfoList, BookEntryInfo)

#Region " Business Methods "

        ''' <summary>
        ''' Gets a sum of book entry values in the list by type.
        ''' </summary>
        ''' <param name="entryType">Type of the book entries to sum.</param>
        ''' <remarks></remarks>
        Friend Function GetTurnover(ByVal entryType As BookEntryType) As Double

            Dim result As Double = 0

            If entryType = BookEntryType.Debetas Then
                For Each i As BookEntryInfo In Me
                    result = CRound(result + i.DebetTurnover)
                Next
            Else
                For Each i As BookEntryInfo In Me
                    result = CRound(result + i.CreditTurnover)
                Next
            End If

            Return result

        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.GeneralLedger1")
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetList(ByVal myData As DataTable) As BookEntryInfoList

            Return New BookEntryInfoList(myData)

        End Function

        Private Sub New(ByVal myData As DataTable)
            Fetch(myData)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable)

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As DataRow In myData.Rows
                Add(BookEntryInfo.GetBookEntryInfo(dr))
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace