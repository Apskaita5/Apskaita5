Namespace DGVColumnSelector
    Partial Class UserControlMenu
        ''' <summary> 
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing
        
        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
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
            '
            'UserControlMenu
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoScroll = True
            Me.AutoScrollMargin = New System.Drawing.Size(15, 0)
            Me.AutoScrollMinSize = New System.Drawing.Size(0, 350)
            Me.AutoSize = True
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.MinimumSize = New System.Drawing.Size(150, 349)
            Me.Name = "UserControlMenu"
            Me.Size = New System.Drawing.Size(133, 349)
            Me.ResumeLayout(False)

        End Sub
        
        #End Region
        
    End Class
End Namespace