Namespace Assets

    ''' <summary>
    ''' Represents a collection of long term asset data that is 
    ''' <see cref="General.TransferOfBalance">transfered with the balance</see>.
    ''' </summary>
    ''' <remarks>Values are stored in the database table turtas.
    ''' Should only be used as a child of <see cref="ComplexOperationValueChange">ComplexOperationValueChange</see>.</remarks>
    <Serializable()> _
    Public Class LongTermAssetList
        Inherits BusinessListBase(Of LongTermAssetList, LongTermAsset)

#Region " Business Methods "

        Friend Function GetChronologyValidators() As IChronologicValidator()
            Dim result As New List(Of IChronologicValidator)
            For Each i As LongTermAsset In Me
                result.Add(i.ChronologyValidator)
            Next
            Return result.ToArray
        End Function

        Friend Sub SetParentDate(ByVal parentDate As Date)
            Me.RaiseListChangedEvents = False
            For Each a As LongTermAsset In Me
                a.AcquisitionDate = parentDate
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        ''' <summary>
        ''' Adds items to the current collection using imported data.
        ''' </summary>
        ''' <param name="lines">imported data as one string row per asset</param>
        ''' <param name="columnSeparator">a separator used to separate asset 
        ''' data columns within a line string</param>
        ''' <remarks></remarks>
        Friend Sub ImportRange(ByVal lines As String(), ByVal columnSeparator As String, _
            ByVal parentValidator As IChronologicValidator)
            Dim accounList As AccountInfoList = AccountInfoList.GetList()
            Me.RaiseListChangedEvents = False
            For Each line As String In lines
                Add(LongTermAsset.NewLongTermAssetChild(line.Split(New String() _
                    {columnSeparator}, StringSplitOptions.None), parentValidator, accounList))
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub

        Friend Function GetInsertDate() As DateTime
            Dim result As DateTime = DateTime.MaxValue
            For Each o As LongTermAsset In Me
                If o.InsertDate < result Then result = o.InsertDate
            Next
            Return result
        End Function

        Friend Function GetUpdateDate() As DateTime
            Dim result As DateTime = DateTime.MinValue
            For Each o As LongTermAsset In Me
                If o.UpdateDate > result Then result = o.UpdateDate
            Next
            Return result
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
            For Each i As LongTermAsset In Me
                If i.HasWarnings() Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets an existing LongTermAssetList instance.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function GetLongTermAssetList(ByVal parentValidator As SimpleChronologicValidator) As LongTermAssetList
            Return New LongTermAssetList(parentValidator)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal parentValidator As SimpleChronologicValidator)
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = True
            Fetch(parentValidator)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal parentValidator As SimpleChronologicValidator)

            Dim myComm As New SQLCommand("FetchLongTermAssetList")

            Using myData As DataTable = myComm.Fetch()

                RaiseListChangedEvents = False

                For Each dr As DataRow In myData.Rows
                    MyBase.Add(LongTermAsset.GetLongTermAssetChild(dr, parentValidator))
                Next

                RaiseListChangedEvents = True

            End Using

        End Sub

        Friend Sub Update(ByVal parent As LongTermAssetsTransferOfBalance)

            Dim emptyCounter As Integer = 0

            For Each i As LongTermAsset In DeletedList
                If Not i.IsNew Then
                    i.CheckIfCanDelete()
                    emptyCounter += 1
                End If
            Next

            For Each i As LongTermAsset In Me
                If i.IsDirty Then
                    i.CheckIfCanUpdate()
                End If
                emptyCounter += 1
            Next

            If emptyCounter < 1 Then
                Throw New Exception(My.Resources.Assets_LongTermAssetList_ListEmpty)
            End If

            RaiseListChangedEvents = False

            Using transaction As New SqlTransaction

                Try

                    For Each i As LongTermAsset In DeletedList
                        If Not i.IsNew Then LongTermAsset.DeleteChild(i.ID)
                    Next
                    DeletedList.Clear()

                    For Each i As LongTermAsset In Me
                        i.SaveChild(parent.ID, Not parent.ChronologyValidator.FinancialDataCanChange)
                    Next

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace