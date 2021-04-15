Namespace General

    ''' <summary>
    ''' Represents a list of <see cref="Person">person's</see> assignments to <see cref="PersonGroup">PersonGroups</see>. (many to many relation)
    ''' </summary>
    ''' <remarks>Only used as a child of a <see cref="Person">Person</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class PersonGroupAssignmentList
        Inherits BusinessListBase(Of PersonGroupAssignmentList, PersonGroupAssignment)

#Region " Business Methods "

        Public Shadows ReadOnly Property IsDirty() As Boolean
            Get
                For Each assignment As PersonGroupAssignment In Me
                    If assignment.IsDirtyAssignement Then Return True
                Next
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Whether the are any assignemnts to any group.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function IsAssignedToAnyGroup() As Boolean
            For Each i As PersonGroupAssignment In Me
                If i.IsAssigned Then Return True
            Next
            Return False
        End Function

        ''' <summary>
        ''' Refreshes PersonGroup data when <see cref="PersonGroupList">PersonGroupList</see> changes.
        ''' </summary>
        ''' <param name="RefreshedPersonGroupInfoList"></param>
        Public Sub RefreshPersonGroupInfoList(ByVal RefreshedPersonGroupInfoList As PersonGroupInfoList, _
            ByVal RaiseListChangedEvent As Boolean)

            RaiseListChangedEvents = False

            Dim ListHasChanged As Boolean = False

            Dim GroupExists As Boolean
            For Each RefreshedItem As PersonGroupInfo In RefreshedPersonGroupInfoList
                GroupExists = False
                For Each assignement As PersonGroupAssignment In Me
                    If assignement.GroupID = RefreshedItem.ID Then
                        If assignement.RefreshPersonGroupInfo(RefreshedItem) Then ListHasChanged = True
                        GroupExists = True
                        Exit For
                    End If
                Next
                If Not GroupExists Then
                    Add(PersonGroupAssignment.GetPersonGroupAssignment(RefreshedItem))
                    ListHasChanged = True
                End If
            Next

            RaiseListChangedEvents = True

            If ListHasChanged AndAlso RaiseListChangedEvent Then ResetBindings()

        End Sub


        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)

            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)

            Return result
        End Function

        Public Function HasWarnings() As Boolean
            For Each i As PersonGroupAssignment In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a PersonGroupAssignmentList instance for a person.
        ''' </summary>
        ''' <param name="parent"></param>
        ''' <remarks></remarks>
        Friend Shared Function GetPersonGroupAssignmentList(ByVal parent As Person) As PersonGroupAssignmentList
            Return New PersonGroupAssignmentList(parent)
        End Function

        ''' <summary>
        ''' Gets a new PersonGroupAssignmentList instance for a new person.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function NewPersonGroupAssignmentList() As PersonGroupAssignmentList
            Return New PersonGroupAssignmentList()
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
        End Sub

        Private Sub New(ByVal parent As Person)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            Fetch(parent)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal parent As Person)

            Dim myComm As New SQLCommand("FetchPersonGroupAssignmentList")
            myComm.AddParam("?PD", parent.ID)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                For Each dr As DataRow In myData.Rows
                    Add(PersonGroupAssignment.GetPersonGroupAssignment(dr))
                Next

                RaiseListChangedEvents = True

            End Using

        End Sub

        Friend Sub Update(ByVal parent As Person)

            RaiseListChangedEvents = False

            DeletedList.Clear()

            For Each item As PersonGroupAssignment In Me
                If item.IsNewAssignement AndAlso item.IsAssigned Then
                    item.Insert(parent)
                ElseIf item.IsDeletedAssignement Then
                    item.DeleteSelf(parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace