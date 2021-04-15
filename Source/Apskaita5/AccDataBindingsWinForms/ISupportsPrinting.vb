Public Interface ISupportsPrinting

    Function GetPrintDropDownItems() As Windows.Forms.ToolStripDropDown
    Function GetMailDropDownItems() As Windows.Forms.ToolStripDropDown
    Function GetPrintPreviewDropDownItems() As Windows.Forms.ToolStripDropDown
    Function SupportsEmailing() As Boolean
    Sub OnPrintClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Sub OnPrintPreviewClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Sub OnMailClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

End Interface
