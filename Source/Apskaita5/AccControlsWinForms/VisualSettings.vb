Imports System.Windows.Forms
Imports BrightIdeasSoftware

''' <summary>
''' Manages forms and ObjectListView's visual states (saves and restores).
''' </summary>
''' <remarks></remarks>
Public Module VisualSettings

    Private _formPropertiesDictionary As Dictionary(Of String, FormProperties)
    Private _listViewDictionary As Dictionary(Of String, Byte())


    Public Sub SetFormProperties(ByVal nForm As Form, ByVal autoResizeForm As Boolean, _
        ByVal settingsStringCollection As System.Collections.Specialized.StringCollection)

        If autoResizeForm Then
            If nForm.WindowState <> FormWindowState.Normal Then _
                nForm.WindowState = FormWindowState.Normal
            Exit Sub
        End If

        If _formPropertiesDictionary Is Nothing Then
            GetFormPropertiesDictionary(settingsStringCollection)
        End If

        If _formPropertiesDictionary.ContainsKey(GetFullFormName(nForm)) Then

            Dim formPropertiesToApply As FormProperties = _
                _formPropertiesDictionary(GetFullFormName(nForm))

            If formPropertiesToApply.Maximized Then
                nForm.WindowState = FormWindowState.Maximized
                Exit Sub
            End If

            nForm.Location = New System.Drawing.Point(formPropertiesToApply.LocationX, _
                formPropertiesToApply.LocationY)
            nForm.Size = New System.Drawing.Size(formPropertiesToApply.Width, _
                formPropertiesToApply.Height)

        End If

    End Sub

    Public Sub GetFormProperties(ByVal nForm As Form, ByVal autoResizeForm As Boolean, _
        ByVal settingsStringCollection As System.Collections.Specialized.StringCollection)

        If autoResizeForm Then Exit Sub

        If _formPropertiesDictionary Is Nothing Then
            GetFormPropertiesDictionary(settingsStringCollection)
        End If

        Dim currentFormProperties As New FormProperties(nForm)

        If _formPropertiesDictionary.ContainsKey(GetFullFormName(nForm)) Then
            _formPropertiesDictionary.Item(GetFullFormName(nForm)) = currentFormProperties
        Else
            _formPropertiesDictionary.Add(GetFullFormName(nForm), currentFormProperties)
        End If

    End Sub

    Public Sub SetListViewProperties(ByVal listView As ObjectListView, _
        ByVal autoResizeListView As Boolean, ByVal settingsStringCollection _
        As System.Collections.Specialized.StringCollection)

        If _listViewDictionary Is Nothing Then
            GetListViewDictionary(settingsStringCollection)
        End If

        If _listViewDictionary.ContainsKey(GetFullListViewName(listView)) Then
            listView.RestoreState(_listViewDictionary(GetFullListViewName(listView)))
        End If

    End Sub

    Public Sub GetListViewProperties(ByVal listView As ObjectListView, _
        ByVal autoResizeListView As Boolean, ByVal settingsStringCollection _
        As System.Collections.Specialized.StringCollection)

        If _listViewDictionary Is Nothing Then
            GetListViewDictionary(settingsStringCollection)
        End If

        If _listViewDictionary.ContainsKey(GetFullListViewName(listView)) Then
            _listViewDictionary.Item(GetFullListViewName(listView)) = listView.SaveState()
        Else
            _listViewDictionary.Add(GetFullListViewName(listView), listView.SaveState())
        End If

    End Sub


    Private Function GetFullFormName(ByVal nForm As Form) As String
        Return nForm.GetType.Namespace.Trim.ToLower & nForm.GetType.Name.Trim.ToLower
    End Function

    Private Function GetFullListViewName(ByVal listView As ObjectListView) As String
        If Not listView.Parent Is Nothing AndAlso TypeOf listView.Parent Is InfoListControl Then
            Return "InfoListControl_" & listView.Parent.GetType.Name
        End If
        Return GetFullFormName(listView.FindForm) & "_" & listView.Name.Trim.ToLower
    End Function


    Public Sub GetFormPropertiesDictionary(ByVal settingsStringCollection _
        As System.Collections.Specialized.StringCollection)

        _formPropertiesDictionary = New Dictionary(Of String, FormProperties)

        If settingsStringCollection Is Nothing Then Exit Sub

        For Each settingsString As String In settingsStringCollection
            _formPropertiesDictionary.Add(GetElement(settingsString, 0).Trim, _
                New FormProperties(settingsString))
        Next

    End Sub

    Public Sub GetListViewDictionary(ByVal settingsStringCollection _
        As System.Collections.Specialized.StringCollection)

        _listViewDictionary = New Dictionary(Of String, Byte())

        If settingsStringCollection Is Nothing Then Exit Sub

        Dim keyValuePairString As String()

        For Each settingsString As String In settingsStringCollection
            keyValuePairString = settingsString.Split(New Char() {Chr(9)})
            _listViewDictionary.Add(keyValuePairString(0), _
                Convert.FromBase64String(keyValuePairString(1)))
        Next

    End Sub

    Public Function GetFormPropertiesStringCollection(ByVal settingsStringCollection _
        As System.Collections.Specialized.StringCollection) _
        As System.Collections.Specialized.StringCollection

        Dim result As New System.Collections.Specialized.StringCollection

        If _formPropertiesDictionary Is Nothing Then
            GetFormPropertiesDictionary(settingsStringCollection)
        End If

        For Each p As KeyValuePair(Of String, FormProperties) In _formPropertiesDictionary
            result.Add(p.Key.Trim & p.Value.ToString)
        Next

        Return result

    End Function

    Public Function GetListViewPropertiesStringCollection(ByVal settingsStringCollection _
        As System.Collections.Specialized.StringCollection) _
        As System.Collections.Specialized.StringCollection

        Dim result As New System.Collections.Specialized.StringCollection

        If _listViewDictionary Is Nothing Then
            GetListViewDictionary(settingsStringCollection)
        End If

        For Each p As KeyValuePair(Of String, Byte()) In _listViewDictionary
            result.Add(p.Key.Trim & Chr(9) & Convert.ToBase64String(p.Value))
        Next

        Return result

    End Function

End Module
