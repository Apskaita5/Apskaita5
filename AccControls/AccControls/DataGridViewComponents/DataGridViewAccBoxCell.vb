Public Class DataGridViewAccBoxCell
    Inherits DataGridViewTextBoxCell

    Private _AccBoxEditingControl As AccBoxEditingControl

    Public Overloads Property Value()
        Get

            If Not MyBase.Value Is Nothing AndAlso Not System.Convert.IsDBNull(MyBase.Value) _
                AndAlso Double.TryParse(MyBase.Value.ToString.Replace(System.Threading.Thread. _
                CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator, ""), New Double) Then

                Return CRound(CDbl(MyBase.Value), CType(Me.OwningColumn, DataGridViewAccBoxColumn).RoundDegree)

            Else

                Return 0

            End If
        End Get
        Set(ByVal value)
            If Not value Is Nothing AndAlso Not System.Convert.IsDBNull(value) _
                AndAlso Double.TryParse(value.ToString.Replace(System.Threading.Thread. _
                CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator, ""), New Double) Then

                MyBase.Value = CRound(CDbl(value.ToString.Replace(System.Threading.Thread. _
                    CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator, "")), _
                    CType(Me.OwningColumn, DataGridViewAccBoxColumn).RoundDegree)

            Else

                MyBase.Value = 0

            End If
        End Set
    End Property

    Public Overrides ReadOnly Property EditType() As Type
        Get
            ' Return the type of the editing contol that Cell uses.
            Return GetType(AccBoxEditingControl)
        End Get
    End Property

    Public Overrides ReadOnly Property ValueType() As Type
        Get
            ' Return the type of the value that Cell contains.
            Return GetType(Double)
        End Get
    End Property

    Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, _
      ByVal initialFormattedValue As Object, _
      ByVal dataGridViewCellStyle As DataGridViewCellStyle)

        ' Set the value of the editing control to the current cell value.
        MyBase.InitializeEditingControl(rowIndex, _
          initialFormattedValue, dataGridViewCellStyle)

        _AccBoxEditingControl = CType(DataGridView.EditingControl, AccBoxEditingControl)

        If Me.Value Is Nothing OrElse System.Convert.IsDBNull(Me.Value) OrElse _
            Not Double.TryParse(Me.Value.ToString.Trim.Replace(System.Threading.Thread. _
            CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator, ""), New Double) _
            Then Me.Value = 0

        If _AccBoxEditingControl Is Nothing Then Exit Sub

        _AccBoxEditingControl.AddZeros = CType(Me.OwningColumn, DataGridViewAccBoxColumn).AddZeros
        _AccBoxEditingControl.InputType = CType(Me.OwningColumn, DataGridViewAccBoxColumn).InputType
        _AccBoxEditingControl.Apvalinimas = CType(Me.OwningColumn, DataGridViewAccBoxColumn).RoundDegree
        _AccBoxEditingControl.UseSeparator = CType(Me.OwningColumn, DataGridViewAccBoxColumn).UseSeparator
        _AccBoxEditingControl.DecimalValue = Me.Value

    End Sub

    Public Overrides ReadOnly Property DefaultNewRowValue() As Object
        Get
            Return 0
        End Get
    End Property

    Public Sub New()
        MyBase.New()
    End Sub


End Class
