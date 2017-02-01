<Serializable()> <Xml.Serialization.XmlRoot("RestResponse")> _
Public Class RestResponse

    Private _Status As String = ""
    Private _Message As String = ""
    Private _Exception As String = ""

    Public Property Status() As String
        Get
            Return _Status.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _Status.Trim <> value.Trim Then
                _Status = value.Trim
            End If
        End Set
    End Property

    Public Property Exception() As String
        Get
            Return _Exception.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _Exception.Trim <> value.Trim Then
                _Exception = value.Trim
            End If
        End Set
    End Property

    Public Property Message() As String
        Get
            Return _Message.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _Message.Trim <> value.Trim Then
                _Message = value.Trim
            End If
        End Set
    End Property


    Public Sub New()

    End Sub

    Public Sub New(ByVal msg As String, ByVal ex As Exception)
        If ex Is Nothing Then
            _Status = "OK"
            _Exception = ""
        Else
            _Status = "Failed"
            _Exception = FormatExceptionString(ex, False)
        End If
        _Message = msg
    End Sub


    Private Function FormatExceptionString(ByVal ex As Exception, ByVal Internal As Boolean) As String

        Dim result As String = ""
        If Internal Then
            result = "Internal exception data: " & vbCrLf
        Else
            result = "Exception data: " & vbCrLf
        End If

        result = result & "Exception message: " & vbCrLf & ex.Message & vbCrLf

        If ex.Source IsNot Nothing Then _
            result = result & "Exception Source: " & vbCrLf & ex.Source & vbCrLf

        If ex.TargetSite IsNot Nothing Then _
                result = result & "Exception TargetSite: " & vbCrLf & ex.TargetSite.Name & vbCrLf

        If ex.StackTrace IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.StackTrace) Then _
            result = result & "Exception StackTrace: " & vbCrLf & ex.StackTrace & vbCrLf

        If ex.InnerException IsNot Nothing Then
            result = result & vbCrLf & "----------------------------" & vbCrLf
            result = result & FormatExceptionString(ex.InnerException, True)
        End If

        Return result

    End Function

End Class
