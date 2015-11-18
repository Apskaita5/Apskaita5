''' <summary>
''' Represents an attribute that is used to mark business objects' properties holding a reference
''' to the <see cref="General.Account.ID">Account.ID</see> property and to store basic business rules
''' (mandatory, allowed values, etc.).
''' </summary>
''' <remarks>Used for validation purposes in <see cref="AccountFieldValidation">AccountFieldValidation</see>
''' method.
''' Could be used by GUI to initialize appropriate controls.</remarks>
<Serializable()> _
<AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
Public Class AccountFieldAttribute
    Inherits System.Attribute

    ''' <summary>
    ''' A list of all the possible account base class values as provided by 
    ''' <see cref="General.Account.GetAccountClass">Account.GetAccountClass</see> method.
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly AllAccountClasses As Integer() = New Integer() {1, 2, 3, 4, 5, 6}

    Private _ValueRequired As ValueRequiredLevel = ValueRequiredLevel.Optional
    Private _AcceptedClasses As Integer() = AllAccountClasses
    Private _ErrorOnClassMismatch As Boolean = False

    ''' <summary>
    ''' Whether the property value is mandatory.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property ValueRequired() As ValueRequiredLevel
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _ValueRequired
        End Get
    End Property

    ''' <summary>
    ''' A list of the allowed account base class values for the property as provided by 
    ''' <see cref="General.Account.GetAccountClass">Account.GetAccountClass</see> method.
    ''' </summary>
    ''' <remarks>Defaults to <see cref="AllAccountClasses">all the possible account base class values.</see></remarks>
    Public ReadOnly Property AcceptedClasses() As Integer()
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            If _AcceptedClasses Is Nothing OrElse _AcceptedClasses.Length < 1 Then
                _AcceptedClasses = AllAccountClasses
            End If
            Return _AcceptedClasses
        End Get
    End Property

    ''' <summary>
    ''' Whether to treat account value with an invalid base class as an error (not warning).
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property ErrorOnClassMismatch() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _ErrorOnClassMismatch
        End Get
    End Property

    ''' <summary>
    ''' Gets <see cref="AcceptedClasses">a list of AcceptedClasses</see> formated as comma separated string.
    ''' </summary>
    Public Function GetAcceptedClassesString() As String

        If _AcceptedClasses Is Nothing OrElse _AcceptedClasses.Length < 1 Then
            _AcceptedClasses = AllAccountClasses
        End If

        Dim result As New List(Of String)

        For Each n As Integer In _AcceptedClasses
            result.Add(n.ToString)
        Next

        Return String.Join(", ", result.ToArray)

    End Function


    ''' <summary>
    ''' Creates a new instance of an AccountFieldAttribute class.
    ''' </summary>
    ''' <param name="valueValueRequired">Whether the property value is mandatory.</param>
    ''' <param name="valueErrorOnClassMismatch">Whether to treat account value with an invalid base class as an error (not warning).</param>
    ''' <param name="valueAcceptedClasses">A list of the allowed account base class values for the property as provided by 
    ''' <see cref="General.Account.GetAccountClass">Account.GetAccountClass</see> method.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueErrorOnClassMismatch As Boolean, _
        ByVal ParamArray valueAcceptedClasses As Integer())

        _ValueRequired = valueValueRequired
        _ErrorOnClassMismatch = valueErrorOnClassMismatch
        _AcceptedClasses = valueAcceptedClasses

        If _AcceptedClasses Is Nothing OrElse _AcceptedClasses.Length < 1 Then
            _AcceptedClasses = AllAccountClasses
        End If

    End Sub

End Class
