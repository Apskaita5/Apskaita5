Imports System.ComponentModel

Public Class BackgroundInfoPanel

    '<System.ComponentModel.ReadOnly(True), Browsable(False)> _
    'Public Property DataSource() As Assets.OperationBackground
    '    Get
    '        If OperationBackgroundBindingSource.DataSource Is Nothing Then Return Nothing
    '        If OperationBackgroundBindingSource.DataSource.GetType().FullName = "System.RuntimeType" Then Return Nothing
    '        Return DirectCast(OperationBackgroundBindingSource.DataSource, Assets.OperationBackground)
    '    End Get
    '    Set(ByVal value As Assets.OperationBackground)
    '        OperationBackgroundBindingSource.DataSource = value
    '    End Set
    'End Property

    'Public ReadOnly Property BindingSource() As BindingSource
    '    Get
    '        Return OperationBackgroundBindingSource
    '    End Get
    'End Property

    Public Function GetBindingSource() As BindingSource
        Return OperationBackgroundBindingSource
    End Function

End Class
