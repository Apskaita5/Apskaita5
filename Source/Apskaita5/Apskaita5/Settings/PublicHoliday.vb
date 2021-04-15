Imports Csla.Validation
Imports ApskaitaObjects.Settings.XmlProxies

Namespace Settings

    ''' <summary>
    ''' Represents a date of public holidays.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="PublicHolidayList">PublicHolidayList</see>.
    ''' Persisted using xml proxies as a part of <see cref="CommonSettings">CommonSettings</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class PublicHoliday
        Inherits BusinessBase(Of PublicHoliday)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _PublicHolidayDate As Date = Today


        ''' <summary>
        ''' Gets or sets a public holiday date. 
        ''' </summary>
        ''' <remarks></remarks>
        Public Property PublicHolidayDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PublicHolidayDate
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If _PublicHolidayDate.Date <> value.Date Then
                    _PublicHolidayDate = value.Date
                    PropertyHasChanged()
                End If
            End Set
        End Property



        Public Function HasWarnings() As Boolean
            Return (Me.BrokenRulesCollection.WarningCount > 0)
        End Function

        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            If IsValid Then Return ""
            Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(RuleSeverity.Error))
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If BrokenRulesCollection.WarningCount < 1 Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(RuleSeverity.Warning))
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return _PublicHolidayDate.ToString("yyyy-MM-dd")
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function NewPublicHoliday() As PublicHoliday
            Dim result As New PublicHoliday
            result.ValidationRules.CheckRules()
            Return result
        End Function

        Friend Shared Function GetPublicHoliday(ByVal proxy As PublicHolidayProxy) As PublicHoliday
            Return New PublicHoliday(proxy)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal proxy As PublicHolidayProxy)
            MarkAsChild()
            Fetch(proxy)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal proxy As PublicHolidayProxy)

            _PublicHolidayDate = proxy.PublicHolidayDate

            MarkOld()

        End Sub

        Friend Function GetProxy(ByVal markItemAsOld As Boolean) As PublicHolidayProxy

            Dim result As New PublicHolidayProxy

            result.PublicHolidayDate = _PublicHolidayDate

            If markItemAsOld Then MarkOld()

            Return result

        End Function

#End Region

    End Class

End Namespace
