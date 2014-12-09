Imports System.ComponentModel
Public Class DataGridViewAccBoxColumn
    Inherits System.Windows.Forms.DataGridViewColumn

    Private _RoundDegree As Integer = 2
    Private _UseSeparator As AccBox.Separator = AccBox.Separator.OnValidate
    Private _InputType As AccBox.Tip = AccBox.Tip.Skaicius
    Private _AddZeros As Boolean = True

    <Browsable(True), _
    EditorBrowsable(EditorBrowsableState.Always), DefaultValue(GetType(Integer), "2"), _
    DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public Property RoundDegree() As Integer
        Get
            Return _RoundDegree
        End Get
        Set(ByVal value As Integer)
            _RoundDegree = value
        End Set
    End Property

    <Browsable(True), _
    EditorBrowsable(EditorBrowsableState.Always), DefaultValue(GetType(AccBox.Separator), "OnValidate"), _
    DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public Property UseSeparator() As AccBox.Separator
        Get
            Return _UseSeparator
        End Get
        Set(ByVal value As AccBox.Separator)
            _UseSeparator = value
        End Set
    End Property

    <Browsable(True), _
    EditorBrowsable(EditorBrowsableState.Always), DefaultValue(GetType(AccBox.Tip), "Skaicius"), _
    DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public Property InputType() As AccBox.Tip
        Get
            Return _InputType
        End Get
        Set(ByVal value As AccBox.Tip)
            _InputType = value
        End Set
    End Property

    <Browsable(True), _
    EditorBrowsable(EditorBrowsableState.Always), DefaultValue(GetType(Boolean), "True"), _
    DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public Property AddZeros() As Boolean
        Get
            Return _AddZeros
        End Get
        Set(ByVal value As Boolean)
            _AddZeros = value
        End Set
    End Property

    Public Sub New()
        MyBase.CellTemplate = New DataGridViewAccBoxCell
    End Sub

End Class
