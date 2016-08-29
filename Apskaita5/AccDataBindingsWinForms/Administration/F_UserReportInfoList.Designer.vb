<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_UserReportInfoList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_UserReportInfoList))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.UploadUserReportButton = New System.Windows.Forms.Button
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.UserReportInfoListDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DownloadItem_MenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteItem_MenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UserReportInfoListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ProgressFiller2 = New AccControlsWinForms.ProgressFiller
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.Panel1.SuspendLayout()
        CType(Me.UserReportInfoListDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.UserReportInfoListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.UploadUserReportButton)
        Me.Panel1.Controls.Add(Me.RefreshButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(633, 38)
        Me.Panel1.TabIndex = 0
        '
        'UploadUserReportButton
        '
        Me.UploadUserReportButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UploadUserReportButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.folder_open_icon_24p
        Me.UploadUserReportButton.Location = New System.Drawing.Point(542, 3)
        Me.UploadUserReportButton.Name = "UploadUserReportButton"
        Me.UploadUserReportButton.Size = New System.Drawing.Size(32, 32)
        Me.UploadUserReportButton.TabIndex = 2
        Me.UploadUserReportButton.UseVisualStyleBackColor = True
        '
        'RefreshButton
        '
        Me.RefreshButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefreshButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_24p
        Me.RefreshButton.Location = New System.Drawing.Point(590, 3)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(32, 32)
        Me.RefreshButton.TabIndex = 1
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'UserReportInfoListDataListView
        '
        Me.UserReportInfoListDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.UserReportInfoListDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.UserReportInfoListDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.UserReportInfoListDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.UserReportInfoListDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.UserReportInfoListDataListView.AllowColumnReorder = True
        Me.UserReportInfoListDataListView.AutoGenerateColumns = False
        Me.UserReportInfoListDataListView.CausesValidation = False
        Me.UserReportInfoListDataListView.CellEditUseWholeCell = False
        Me.UserReportInfoListDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn4, Me.OlvColumn5})
        Me.UserReportInfoListDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.UserReportInfoListDataListView.DataSource = Me.UserReportInfoListBindingSource
        Me.UserReportInfoListDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserReportInfoListDataListView.FullRowSelect = True
        Me.UserReportInfoListDataListView.HasCollapsibleGroups = False
        Me.UserReportInfoListDataListView.HeaderWordWrap = True
        Me.UserReportInfoListDataListView.HideSelection = False
        Me.UserReportInfoListDataListView.HighlightBackgroundColor = System.Drawing.Color.PaleGreen
        Me.UserReportInfoListDataListView.HighlightForegroundColor = System.Drawing.Color.Black
        Me.UserReportInfoListDataListView.IncludeColumnHeadersInCopy = True
        Me.UserReportInfoListDataListView.Location = New System.Drawing.Point(0, 38)
        Me.UserReportInfoListDataListView.Name = "UserReportInfoListDataListView"
        Me.UserReportInfoListDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.UserReportInfoListDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.UserReportInfoListDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.UserReportInfoListDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.UserReportInfoListDataListView.ShowCommandMenuOnRightClick = True
        Me.UserReportInfoListDataListView.ShowGroups = False
        Me.UserReportInfoListDataListView.ShowImagesOnSubItems = True
        Me.UserReportInfoListDataListView.ShowItemCountOnGroups = True
        Me.UserReportInfoListDataListView.ShowItemToolTips = True
        Me.UserReportInfoListDataListView.Size = New System.Drawing.Size(633, 269)
        Me.UserReportInfoListDataListView.TabIndex = 4
        Me.UserReportInfoListDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.UserReportInfoListDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.UserReportInfoListDataListView.UseCellFormatEvents = True
        Me.UserReportInfoListDataListView.UseCompatibleStateImageBehavior = False
        Me.UserReportInfoListDataListView.UseFilterIndicator = True
        Me.UserReportInfoListDataListView.UseFiltering = True
        Me.UserReportInfoListDataListView.UseHotItem = True
        Me.UserReportInfoListDataListView.UseNotifyPropertyChanged = True
        Me.UserReportInfoListDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "Name"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsEditable = False
        Me.OlvColumn2.Text = "Ataskaitos Pavadinimas"
        Me.OlvColumn2.Width = 235
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "Author"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsEditable = False
        Me.OlvColumn3.Text = "Ataskaitos Autorius"
        Me.OlvColumn3.Width = 116
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "FileAdded"
        Me.OlvColumn4.AspectToStringFormat = "{0:yyyy-MM-dd HH:mm:ss}"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.IsEditable = False
        Me.OlvColumn4.Text = "Įkelta"
        Me.OlvColumn4.Width = 129
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "FileLastUpdated"
        Me.OlvColumn5.AspectToStringFormat = "{0:yyyy-MM-dd HH:mm:ss}"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsEditable = False
        Me.OlvColumn5.Text = "Pakeista"
        Me.OlvColumn5.Width = 141
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "FileName"
        Me.OlvColumn1.CellEditUseWholeCell = True
        Me.OlvColumn1.DisplayIndex = 1
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsEditable = False
        Me.OlvColumn1.IsVisible = False
        Me.OlvColumn1.Text = "Failas"
        Me.OlvColumn1.Width = 100
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DownloadItem_MenuItem, Me.DeleteItem_MenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(127, 48)
        '
        'DownloadItem_MenuItem
        '
        Me.DownloadItem_MenuItem.Name = "DownloadItem_MenuItem"
        Me.DownloadItem_MenuItem.Size = New System.Drawing.Size(126, 22)
        Me.DownloadItem_MenuItem.Text = "Parsisiųsti"
        '
        'DeleteItem_MenuItem
        '
        Me.DeleteItem_MenuItem.Name = "DeleteItem_MenuItem"
        Me.DeleteItem_MenuItem.Size = New System.Drawing.Size(126, 22)
        Me.DeleteItem_MenuItem.Text = "Ištrinti"
        '
        'UserReportInfoListBindingSource
        '
        Me.UserReportInfoListBindingSource.DataSource = GetType(ApskaitaObjects.HelperLists.UserReportInfo)
        '
        'ProgressFiller2
        '
        Me.ProgressFiller2.Location = New System.Drawing.Point(178, 131)
        Me.ProgressFiller2.Name = "ProgressFiller2"
        Me.ProgressFiller2.Size = New System.Drawing.Size(134, 40)
        Me.ProgressFiller2.TabIndex = 6
        Me.ProgressFiller2.Visible = False
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(33, 131)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(120, 37)
        Me.ProgressFiller1.TabIndex = 5
        Me.ProgressFiller1.Visible = False
        '
        'F_UserReportInfoList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(633, 307)
        Me.Controls.Add(Me.UserReportInfoListDataListView)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ProgressFiller2)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_UserReportInfoList"
        Me.ShowInTaskbar = False
        Me.Text = "Vartotojo ataskaitų sąrašas"
        Me.Panel1.ResumeLayout(False)
        CType(Me.UserReportInfoListDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.UserReportInfoListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents UploadUserReportButton As System.Windows.Forms.Button
    Friend WithEvents UserReportInfoListDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn5 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DownloadItem_MenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteItem_MenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserReportInfoListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ProgressFiller2 As AccControlsWinForms.ProgressFiller
End Class
