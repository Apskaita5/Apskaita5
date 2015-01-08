Partial Class AccGridComboBox

    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then components.Dispose()
            If DisposeToolStripDataGridView Then
                If Not myDropDown Is Nothing AndAlso Not myDropDown.IsDisposed Then myDropDown.Dispose()
                If Not myDataGridView Is Nothing AndAlso _
                    Not myDataGridView.DataGridViewControl Is Nothing AndAlso _
                    Not myDataGridView.DataGridViewControl.IsDisposed Then _
                    myDataGridView.DataGridViewControl.Dispose()
                If Not myDataGridView Is Nothing AndAlso Not myDataGridView.IsDisposed Then _
                    myDataGridView.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Component Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify 
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.ResumeLayout(False)
    End Sub

#End Region
End Class
