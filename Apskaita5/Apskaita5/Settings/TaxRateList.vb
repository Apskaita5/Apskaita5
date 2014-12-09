Namespace Settings

    <Serializable()> _
    Public Class TaxRateList
        Inherits BusinessListBase(Of TaxRateList, TaxRate)
        Implements ICustomXmlSerialized

#Region " Business Methods "

        ''' <summary>
        ''' Gets a list of possible tax tokens.
        ''' </summary>
        Public Shared Function GetAllTaxTokens(Optional ByVal AddEmptyToken As Boolean = True) As List(Of String)
            Return GetEnumValuesHumanReadableList(GetType(TaxTarifType), AddEmptyToken)
        End Function

        Protected Overrides Function AddNewCore() As Object
            Dim NewItem As TaxRate = TaxRate.NewTaxRate(True)
            Me.Add(NewItem)
            Return NewItem
        End Function

        Friend Sub ForceCheckRules()
            For Each t As TaxRate In Me
                t.ForceCheckRules()
            Next
        End Sub

        Friend Function GetAllBrokenRules() As String
            If IsValid Then Return ""

            Dim result As String = ""

            For Each gitem As TaxRate In Me
                If Not gitem.IsValid Then
                    If String.IsNullOrEmpty(result.Trim) Then
                        result = gitem.BrokenRulesCollection.ToString
                    Else
                        result = result & vbCrLf & gitem.BrokenRulesCollection.ToString
                    End If
                End If
            Next

            Return result
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewTaxRateList() As TaxRateList
            Return New TaxRateList()
        End Function

        Private Sub New()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            MarkAsChild()
        End Sub

#End Region

#Region " Data Access "

        Friend Sub FetchTaxesInUse()

            If Not GetCurrentIdentity.IsAuthenticatedWithDB Then Exit Sub

            Dim myComm As New SQLCommand("FetchTaxRateListInUse")

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                For Each dr As DataRow In myData.Rows
                    If Not ListContainsDataRow(dr) Then Me.Add(TaxRate.NewTaxRate(dr))
                Next

                RaiseListChangedEvents = True

            End Using


        End Sub

        Private Function ListContainsDataRow(ByVal dr As DataRow) As Boolean
            For Each i As TaxRate In Me
                If i.EqualsDataRow(dr) Then Return True
            Next
            Return False
        End Function


        Private Sub DeSerialize(ByVal node As System.Xml.XmlNode) _
            Implements ICustomXmlSerialized.DeSerialize

            RaiseListChangedEvents = False

            For Each n As System.Xml.XmlNode In CustomXmlSerialization.GetCollectionNode(node)
                Dim newItem As TaxRate = TaxRate.NewTaxRate(False)
                CustomXmlSerialization.SetValues(newItem, n)
                Add(newItem)
            Next

            RaiseListChangedEvents = True

        End Sub

        Public Function IsSerializedCollection() As Boolean _
            Implements ICustomXmlSerialized.IsSerializedCollection
            Return True
        End Function

        Private Sub Serialize(ByRef node As System.Xml.XmlNode) _
            Implements ICustomXmlSerialized.Serialize

            RaiseListChangedEvents = False

            Dim CollectionNode As Xml.XmlNode = CustomXmlSerialization.GetCollectionNode(node)
            For Each tr As TaxRate In Me
                CustomXmlSerialization.AddChildNode(CollectionNode, "TaxRateItem", tr)
            Next

            DeletedList.Clear()

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace