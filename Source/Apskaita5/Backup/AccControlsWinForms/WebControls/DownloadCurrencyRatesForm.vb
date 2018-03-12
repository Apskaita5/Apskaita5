Imports System.Windows.Forms

Namespace WebControls

    Friend Class DownloadCurrencyRatesForm

        Private _Factory As CurrencyRateFactoryBase = Nothing
        Private _Params As List(Of CurrencyRate.CurrencyRateParam) = Nothing
        Private _Result As List(Of CurrencyRate) = Nothing
        Private _DownloadException As Exception = Nothing
        Private _WebClient As System.Net.WebClient = Nothing
        Private _Canceled As Boolean = False
        Private _DistinctDates As List(Of Date) = Nothing
        Private _CurrentIndex As Integer = 0


        Public ReadOnly Property Result() As List(Of CurrencyRate)
            Get
                Return _Result
            End Get
        End Property

        Public ReadOnly Property DownloadException() As Exception
            Get
                Return _DownloadException
            End Get
        End Property


        Friend Sub New(ByVal factory As CurrencyRateFactoryBase, ByVal params As List(Of CurrencyRate.CurrencyRateParam))

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _Factory = factory
            _Params = params

        End Sub


        Private Sub DownloadCurrencyRatesForm_Load(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                _DistinctDates = CurrencyRate.GetDistinctDates(_Params)

                _WebClient = New System.Net.WebClient
                AddHandler _WebClient.DownloadDataCompleted, AddressOf OnDownloadCompleted
                Me.ProgressBar1.Style = ProgressBarStyle.Continuous
                Me.ProgressBar1.Maximum = _DistinctDates.Count
                Me.ProgressBar1.Value = 0

                Me.Text = String.Format("Atsisiunčiami valiutų kursai {0} datai...", _
                    _DistinctDates(0).ToString("yyyy-MM-dd"))

                Dim updateUrl As New Uri(_Factory.GetDownloadUrl(_DistinctDates(0)))

                _WebClient.DownloadDataAsync(updateUrl)

            Catch ex As Exception

                _DownloadException = ex

                CleanUp()

                Me.DialogResult = DialogResult.Retry
                Me.Close()

            End Try

        End Sub


        Private Sub CancelUpdateButton_Click(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles CancelDownloadButton.Click

            _WebClient.CancelAsync()

            _Canceled = True

            _Result = Nothing

            CleanUp()

            Me.DialogResult = DialogResult.Cancel
            Me.Close()

        End Sub

        Private Sub OnDownloadCompleted(ByVal sender As Object, ByVal e As System.Net.DownloadDataCompletedEventArgs)

            If _Canceled OrElse e.Cancelled Then Exit Sub

            If Not e.Error Is Nothing Then
                _DownloadException = e.Error
            ElseIf e.Result Is Nothing OrElse e.Result.Length < 1 Then
                _DownloadException = New Exception("Web request returned null.")
            Else

                If _Result Is Nothing Then _Result = New List(Of CurrencyRate)()

                Try
                    _Result.AddRange(_Factory.RawDataToCurrencyRateList(e.Result, _
                        _DistinctDates(_CurrentIndex), _Params))
                Catch ex As Exception
                    _DownloadException = New Exception(String.Format("Failed to parse downloaded data: {0}", ex.Message), ex)
                End Try

            End If

            _CurrentIndex += 1

            Me.ProgressBar1.Value = _CurrentIndex

            If Not _DownloadException Is Nothing OrElse _CurrentIndex >= _DistinctDates.Count Then

                CleanUp()

                If _DownloadException Is Nothing Then
                    Me.DialogResult = DialogResult.OK
                Else
                    Me.DialogResult = DialogResult.Retry
                End If

                Me.Close()

                Exit Sub

            End If

            Me.Text = String.Format("Atsisiunčiami valiutų kursai {0} datai...", _
                _DistinctDates(_CurrentIndex).ToString("yyyy-MM-dd"))

            Try

                Dim updateUrl As New Uri(_Factory.GetDownloadUrl(_DistinctDates(0)))

                _WebClient.DownloadDataAsync(updateUrl)

            Catch ex As Exception

                _DownloadException = ex

                CleanUp()

                Me.DialogResult = DialogResult.Retry
                Me.Close()

            End Try

        End Sub


        Private Sub CleanUp()

            If Not _WebClient Is Nothing Then
                Try
                    RemoveHandler _WebClient.DownloadDataCompleted, AddressOf OnDownloadCompleted
                Catch eg As Exception
                End Try
                Try
                    _WebClient.Dispose()
                Catch eg As Exception
                End Try
            End If

        End Sub

    End Class

End Namespace