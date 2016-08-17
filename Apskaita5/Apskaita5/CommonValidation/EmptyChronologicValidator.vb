''' <summary>
''' Represents an <see cref="IChronologicValidator">IChronologicValidator</see> instance that is used 
''' as a placeholder, i.e. does not contain any business restrictions.
''' </summary>
''' <remarks></remarks>
<Serializable()> _
Public NotInheritable Class EmptyChronologicValidator
    Inherits ReadOnlyBase(Of EmptyChronologicValidator)
    Implements IChronologicValidator

#Region " Business Methods "

    Private _CurrentOperationID As Integer = 0
    Private _CurrentOperationDate As Date = Today
    Private _CurrentOperationName As String = ""
    Private _FinancialDataCanChange As Boolean = True
    Private _MinDateApplicable As Boolean = False
    Private _MaxDateApplicable As Boolean = False
    Private _MinDate As Date = Date.MinValue
    Private _MaxDate As Date = Date.MaxValue
    Private _FinancialDataCanChangeExplanation As String = ""
    Private _MinDateExplanation As String = ""
    Private _MaxDateExplanation As String = ""
    Private _LimitsExplanation As String = ""
    Private _BackgroundExplanation As String = ""
    Private _ParentFinancialDataCanChange As Boolean = True
    Private _ParentFinancialDataCanChangeExplanation As String = ""


    ''' <summary>
    ''' Gets an ID of the validated (parent) object. 
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentOperationID() As Integer _
        Implements IChronologicValidator.CurrentOperationID
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _CurrentOperationID
        End Get
    End Property

    ''' <summary>
    ''' Gets the current date of the validated (parent) object (<see cref="Today">Today</see> for a new object). 
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentOperationDate() As Date _
        Implements IChronologicValidator.CurrentOperationDate
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _CurrentOperationDate
        End Get
    End Property

    ''' <summary>
    ''' Gets the human readable name of the validated (parent) object. 
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentOperationName() As String _
        Implements IChronologicValidator.CurrentOperationName
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _CurrentOperationName.Trim
        End Get
    End Property

    ''' <summary>
    ''' Wheather the financial data of the validated (parent) object is allowed to be changed.
    ''' </summary>
    ''' <returns>Always TRUE.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FinancialDataCanChange() As Boolean _
        Implements IChronologicValidator.FinancialDataCanChange
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _FinancialDataCanChange
        End Get
    End Property

    ''' <summary>
    ''' Wheather there is a minimum allowed date for the validated (parent) object.
    ''' </summary>
    ''' <returns>Always FALSE.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MinDateApplicable() As Boolean _
        Implements IChronologicValidator.MinDateApplicable
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _MinDateApplicable
        End Get
    End Property

    ''' <summary>
    ''' Wheather there is a maximum allowed date for the validated (parent) object.
    ''' </summary>
    ''' <returns>Always FALSE.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MaxDateApplicable() As Boolean _
        Implements IChronologicValidator.MaxDateApplicable
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _MaxDateApplicable
        End Get
    End Property

    ''' <summary>
    ''' Gets a minimum allowed date for the validated (parent) object.
    ''' </summary>
    ''' <returns>Always Date.MinValue.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MinDate() As Date _
        Implements IChronologicValidator.MinDate
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _MinDate
        End Get
    End Property

    ''' <summary>
    ''' Gets a maximum allowed date for the validated (parent) object.
    ''' </summary>
    ''' <returns>Always Date.MaxValue.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MaxDate() As Date _
        Implements IChronologicValidator.MaxDate
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _MaxDate
        End Get
    End Property

    ''' <summary>
    ''' Gets a human readable explanation of why the financial data of the validated (parent) object 
    ''' is NOT allowed to be changed.
    ''' </summary>
    ''' <returns>Always String.Empty.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FinancialDataCanChangeExplanation() As String _
        Implements IChronologicValidator.FinancialDataCanChangeExplanation
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _FinancialDataCanChangeExplanation.Trim
        End Get
    End Property

    ''' <summary>
    ''' Gets a human readable explanation of why there is a minimum allowed date 
    ''' for the validated (parent) object.
    ''' </summary>
    ''' <returns>Always String.Empty.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MinDateExplanation() As String _
        Implements IChronologicValidator.MinDateExplanation
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _MinDateExplanation.Trim
        End Get
    End Property

    ''' <summary>
    ''' Gets a human readable explanation of why there is a maximum allowed date 
    ''' for the validated (parent) object.
    ''' </summary>
    ''' <returns>Always String.Empty.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MaxDateExplanation() As String _
        Implements IChronologicValidator.MaxDateExplanation
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _MaxDateExplanation.Trim
        End Get
    End Property

    ''' <summary>
    ''' A human readable explanation of all the applicable business rules restrains.
    ''' </summary>
    ''' <returns>Always String.Empty.</returns>
    Public ReadOnly Property LimitsExplanation() As String _
        Implements IChronologicValidator.LimitsExplanation
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _LimitsExplanation.Trim
        End Get
    End Property

    ''' <summary>
    ''' A human readable explanation of the applicable business rules restrains.
    ''' </summary>
    ''' <returns>Always String.Empty.</returns>
    ''' <remarks>More exhaustive than <see cref="LimitsExplanation">LimitsExplanation</see>.</remarks>
    Public ReadOnly Property BackgroundExplanation() As String _
        Implements IChronologicValidator.BackgroundExplanation
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _BackgroundExplanation.Trim
        End Get
    End Property

    ''' <summary>
    ''' Wheather the financial data of the validated (parent) object is allowed 
    ''' to be changed by it's parent document.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property ParentFinancialDataCanChange() As Boolean _
        Implements IChronologicValidator.ParentFinancialDataCanChange
        Get
            Return _ParentFinancialDataCanChange
        End Get
    End Property

    ''' <summary>
    ''' Gets a human readable explanation of why the financial data of the validated object 
    ''' is NOT allowed to be changed by it's parent document.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property ParentFinancialDataCanChangeExplanation() As String _
        Implements IChronologicValidator.ParentFinancialDataCanChangeExplanation
        Get
            Return _ParentFinancialDataCanChangeExplanation
        End Get
    End Property


    Public Function ValidateOperationDate(ByVal operationDate As Date, ByRef errorMessage As String, _
        ByRef errorSeverity As Csla.Validation.RuleSeverity) As Boolean _
        Implements IChronologicValidator.ValidateOperationDate

        Return True

    End Function


    Protected Overrides Function GetIdValue() As Object
        Return _CurrentOperationID
    End Function

    Public Overrides Function ToString() As String
        Return _CurrentOperationName
    End Function

#End Region

#Region " Factory Methods "

    ''' <summary>
    ''' Gets a new EmptyChronologicValidator instance for a new or an existing operation (object).
    ''' </summary>
    ''' <param name="operationName">A name of the validated operation (object).</param>
    ''' <remarks></remarks>
    Friend Shared Function NewEmptyChronologicValidator(ByVal operationName As String, _
        ByVal parentValidator As IChronologicValidator) As EmptyChronologicValidator
        Return New EmptyChronologicValidator(operationName, parentValidator)
    End Function


    Private Sub New()
        ' require use of factory methods
    End Sub

    Private Sub New(ByVal nOperationName As String, ByVal parentValidator As IChronologicValidator)
        Create(nOperationName, parentValidator)
    End Sub

#End Region

#Region " Data Access "

    Private Sub Create(ByVal nOperationName As String, ByVal parentValidator As IChronologicValidator)

        _CurrentOperationName = nOperationName

        If Not parentValidator Is Nothing AndAlso Not parentValidator.FinancialDataCanChange Then

            _ParentFinancialDataCanChange = False
            _ParentFinancialDataCanChangeExplanation = parentValidator.FinancialDataCanChangeExplanation

        End If

    End Sub

#End Region

End Class