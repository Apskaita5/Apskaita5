<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_TypicalAccountInfoDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.CheckAllCheckBox = New System.Windows.Forms.CheckBox
        Me.ReportDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.TypicalAccountInfoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ReportDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TypicalAccountInfoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(288, 462)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'CheckAllCheckBox
        '
        Me.CheckAllCheckBox.AutoSize = True
        Me.CheckAllCheckBox.Location = New System.Drawing.Point(3, 3)
        Me.CheckAllCheckBox.Name = "CheckAllCheckBox"
        Me.CheckAllCheckBox.Size = New System.Drawing.Size(15, 14)
        Me.CheckAllCheckBox.TabIndex = 1
        Me.CheckAllCheckBox.UseVisualStyleBackColor = True
        '
        'ReportDataListView
        '
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.ReportDataListView.AllowColumnReorder = True
        Me.ReportDataListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReportDataListView.AutoGenerateColumns = False
        Me.ReportDataListView.CausesValidation = False
        Me.ReportDataListView.CellEditUseWholeCell = False
        Me.ReportDataListView.CheckBoxes = True
        Me.ReportDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn1, Me.OlvColumn2})
        Me.ReportDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.ReportDataListView.DataSource = Me.TypicalAccountInfoBindingSource
        Me.ReportDataListView.FullRowSelect = True
        Me.ReportDataListView.HasCollapsibleGroups = False
        Me.ReportDataListView.HeaderWordWrap = True
        Me.ReportDataListView.HideSelection = False
        Me.ReportDataListView.IncludeColumnHeadersInCopy = True
        Me.ReportDataListView.Location = New System.Drawing.Point(3, 23)
        Me.ReportDataListView.Name = "ReportDataListView"
        Me.ReportDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.ReportDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.ReportDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.ReportDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.ReportDataListView.ShowCommandMenuOnRightClick = True
        Me.ReportDataListView.ShowGroups = False
        Me.ReportDataListView.ShowImagesOnSubItems = True
        Me.ReportDataListView.ShowItemCountOnGroups = True
        Me.ReportDataListView.ShowItemToolTips = True
        Me.ReportDataListView.Size = New System.Drawing.Size(431, 433)
        Me.ReportDataListView.TabIndex = 4
        Me.ReportDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.ReportDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.ReportDataListView.UseCellFormatEvents = True
        Me.ReportDataListView.UseCompatibleStateImageBehavior = False
        Me.ReportDataListView.UseFilterIndicator = True
        Me.ReportDataListView.UseFiltering = True
        Me.ReportDataListView.UseHotItem = True
        Me.ReportDataListView.UseNotifyPropertyChanged = True
        Me.ReportDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "AccountNo"
        Me.OlvColumn1.CellEditUseWholeCell = True
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsEditable = False
        Me.OlvColumn1.Text = "Nr."
        Me.OlvColumn1.ToolTipText = ""
        Me.OlvColumn1.Width = 102
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "AccountName"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsEditable = False
        Me.OlvColumn2.Text = "Pavadinimas"
        Me.OlvColumn2.ToolTipText = ""
        Me.OlvColumn2.Width = 314
        '
        'TypicalAccountInfoBindingSource
        '
        Me.TypicalAccountInfoBindingSource.DataSource = GetType(AccDataBindingsWinForms.TypicalAccountInfo)
        '
        'F_TypicalAccountInfoDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(446, 503)
        Me.Controls.Add(Me.ReportDataListView)
        Me.Controls.Add(Me.CheckAllCheckBox)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "F_TypicalAccountInfoDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Pasirinkti iš Tipinio Sąskaitų Plano"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.ReportDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TypicalAccountInfoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents CheckAllCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ReportDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents TypicalAccountInfoBindingSource As System.Windows.Forms.BindingSource

End Class
