Imports System.ComponentModel
Public Class DataGridViewAccComboBoxColumn
    Inherits System.Windows.Forms.DataGridViewColumn

    Public Sub New()
        MyBase.CellTemplate = New DataGridViewAccComboBoxCell
    End Sub

    Private _DataSource As Object = Nothing
    Public Property DataSource() As Object
        Get
            Return _DataSource
        End Get
        Set(ByVal value As Object)
            _DataSource = value

            If Me.DataGridView Is Nothing Then Exit Property

            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewAccComboBoxCell). _
                    AccComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewAccComboBoxCell). _
                        AccComboBoxEditingControl.DataSource = Nothing
                    CType(row.Cells(Me.Index), DataGridViewAccComboBoxCell). _
                        AccComboBoxEditingControl.DataSource = value

                End If
            Next
        End Set
    End Property

    Private _MaxDropDownItems As Integer = 9
    Public Property MaxDropDownItems() As Integer
        Get
            Return _MaxDropDownItems
        End Get
        Set(ByVal value As Integer)
            _MaxDropDownItems = value

            If Me.DataGridView Is Nothing Then Exit Property

            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewAccComboBoxCell). _
                    AccComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewAccComboBoxCell). _
                        AccComboBoxEditingControl.MaxDropDownItems = value

                End If
            Next

        End Set
    End Property

    Private _DropDownWidth As Integer = 0
    Public Property DropDownWidth() As Integer
        Get
            Return _DropDownWidth
        End Get
        Set(ByVal value As Integer)
            _DropDownWidth = value

            If Me.DataGridView Is Nothing Then Exit Property

            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewAccComboBoxCell). _
                    AccComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewAccComboBoxCell). _
                        AccComboBoxEditingControl.DropDownWidth = value

                End If
            Next

        End Set
    End Property

    Private _FlatStyle As FlatStyle = FlatStyle.System
    Public Property FlatStyle() As FlatStyle
        Get
            Return _FlatStyle
        End Get
        Set(ByVal value As FlatStyle)
            _FlatStyle = value

            If Me.DataGridView Is Nothing Then Exit Property

            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewAccComboBoxCell). _
                    AccComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewAccComboBoxCell). _
                        AccComboBoxEditingControl.FlatStyle = value

                End If
            Next
        End Set
    End Property

End Class
