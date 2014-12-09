'	LumenWorks.Framework.IO.CSV.CsvReader
'	Copyright (c) 2005 Sébastien Lorion
'
'	MIT license (http://en.wikipedia.org/wiki/MIT_License)
'
'	Permission is hereby granted, free of charge, to any person obtaining a copy
'	of this software and associated documentation files (the "Software"), to deal
'	in the Software without restriction, including without limitation the rights 
'	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
'	of the Software, and to permit persons to whom the Software is furnished to do so, 
'	subject to the following conditions:
'
'	The above copyright notice and this permission notice shall be included in all 
'	copies or substantial portions of the Software.
'
'	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
'	INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
'	PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
'	FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
'	ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common
Imports Debug = System.Diagnostics.Debug
Imports System.Globalization
Imports System.IO

Namespace CsvReader

    ''' <summary>
    ''' Represents a reader that provides fast, non-cached, forward-only access to CSV data.  
    ''' </summary>
    Partial Public Class CsvReader
        Implements IDataReader
        Implements IEnumerable(Of String())
        Implements IDisposable

#Region "Constants"

        ''' <summary>
        ''' Defines the default buffer size.
        ''' </summary>
        Public Const DefaultBufferSize As Integer = &H1000

        ''' <summary>
        ''' Defines the default delimiter character separating each field.
        ''' </summary>
        Public Const DefaultDelimiter As Char = ","c

        ''' <summary>
        ''' Defines the default quote character wrapping every field.
        ''' </summary>
        Public Const DefaultQuote As Char = """"c

        ''' <summary>
        ''' Defines the default escape character letting insert quotation characters inside a quoted field.
        ''' </summary>
        Public Const DefaultEscape As Char = """"c

        ''' <summary>
        ''' Defines the default comment character indicating that a line is commented out.
        ''' </summary>
        Public Const DefaultComment As Char = "#"c

#End Region

#Region "Fields"

        ''' <summary>
        ''' Contains the field header comparer.
        ''' </summary>
        Private Shared ReadOnly _fieldHeaderComparer As StringComparer = StringComparer.CurrentCultureIgnoreCase

#Region "Settings"

        ''' <summary>
        ''' Contains the <see cref="T:TextReader"/> pointing to the CSV file.
        ''' </summary>
        Private _reader As TextReader

        ''' <summary>
        ''' Contains the buffer size.
        ''' </summary>
        Private _bufferSize As Integer

        ''' <summary>
        ''' Contains the comment character indicating that a line is commented out.
        ''' </summary>
        Private _comment As Char

        ''' <summary>
        ''' Contains the escape character letting insert quotation characters inside a quoted field.
        ''' </summary>
        Private _escape As Char

        ''' <summary>
        ''' Contains the delimiter character separating each field.
        ''' </summary>
        Private _delimiter As Char

        ''' <summary>
        ''' Contains the quotation character wrapping every field.
        ''' </summary>
        Private _quote As Char

        ''' <summary>
        ''' Determines which values should be trimmed.
        ''' </summary>
        Private _trimmingOptions As ValueTrimmingOptions

        ''' <summary>
        ''' Indicates if field names are located on the first non commented line.
        ''' </summary>
        Private _hasHeaders As Boolean

        ''' <summary>
        ''' Contains the default action to take when a parsing error has occured.
        ''' </summary>
        Private _defaultParseErrorAction As ParseErrorAction

        ''' <summary>
        ''' Contains the action to take when a field is missing.
        ''' </summary>
        Private _missingFieldAction As MissingFieldAction

        ''' <summary>
        ''' Indicates if the reader supports multiline.
        ''' </summary>
        Private _supportsMultiline As Boolean

        ''' <summary>
        ''' Indicates if the reader will skip empty lines.
        ''' </summary>
        Private _skipEmptyLines As Boolean

#End Region

#Region "State"

        ''' <summary>
        ''' Indicates if the class is initialized.
        ''' </summary>
        Private _initialized As Boolean

        ''' <summary>
        ''' Contains the field headers.
        ''' </summary>
        Private _fieldHeaders As String()

        ''' <summary>
        ''' Contains the dictionary of field indexes by header. The key is the field name and the value is its index.
        ''' </summary>
        Private _fieldHeaderIndexes As Dictionary(Of String, Integer)

        ''' <summary>
        ''' Contains the current record index in the CSV file.
        ''' A value of <see cref="M:Int32.MinValue"/> means that the reader has not been initialized yet.
        ''' Otherwise, a negative value means that no record has been read yet.
        ''' </summary>
        Private _currentRecordIndex As Long

        ''' <summary>
        ''' Contains the starting position of the next unread field.
        ''' </summary>
        Private _nextFieldStart As Integer

        ''' <summary>
        ''' Contains the index of the next unread field.
        ''' </summary>
        Private _nextFieldIndex As Integer

        ''' <summary>
        ''' Contains the array of the field values for the current record.
        ''' A null value indicates that the field have not been parsed.
        ''' </summary>
        Private _fields As String()

        ''' <summary>
        ''' Contains the maximum number of fields to retrieve for each record.
        ''' </summary>
        Private _fieldCount As Integer

        ''' <summary>
        ''' Contains the read buffer.
        ''' </summary>
        Private _buffer As Char()

        ''' <summary>
        ''' Contains the current read buffer length.
        ''' </summary>
        Private _bufferLength As Integer

        ''' <summary>
        ''' Indicates if the end of the reader has been reached.
        ''' </summary>
        Private _eof As Boolean

        ''' <summary>
        ''' Indicates if the last read operation reached an EOL character.
        ''' </summary>
        Private _eol As Boolean

        ''' <summary>
        ''' Indicates if the first record is in cache.
        ''' This can happen when initializing a reader with no headers
        ''' because one record must be read to get the field count automatically
        ''' </summary>
        Private _firstRecordInCache As Boolean

        ''' <summary>
        ''' Indicates if one or more field are missing for the current record.
        ''' Resets after each successful record read.
        ''' </summary>
        Private _missingFieldFlag As Boolean

        ''' <summary>
        ''' Indicates if a parse error occured for the current record.
        ''' Resets after each successful record read.
        ''' </summary>
        Private _parseErrorFlag As Boolean

#End Region

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the CsvReader class.
        ''' </summary>
        ''' <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
        ''' <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        ''' <exception cref="T:ArgumentNullException">
        '''		<paramref name="reader"/> is a <see langword="null"/>.
        ''' </exception>
        ''' <exception cref="T:ArgumentException">
        '''		Cannot read from <paramref name="reader"/>.
        ''' </exception>
        Public Sub New(ByVal reader As TextReader, ByVal hasHeaders As Boolean)
            Me.New(reader, hasHeaders, DefaultDelimiter, DefaultQuote, DefaultEscape, DefaultComment, _
             ValueTrimmingOptions.UnquotedOnly, DefaultBufferSize)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the CsvReader class.
        ''' </summary>
        ''' <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
        ''' <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        ''' <param name="bufferSize">The buffer size in bytes.</param>
        ''' <exception cref="T:ArgumentNullException">
        '''		<paramref name="reader"/> is a <see langword="null"/>.
        ''' </exception>
        ''' <exception cref="T:ArgumentException">
        '''		Cannot read from <paramref name="reader"/>.
        ''' </exception>
        Public Sub New(ByVal reader As TextReader, ByVal hasHeaders As Boolean, ByVal bufferSize As Integer)
            Me.New(reader, hasHeaders, DefaultDelimiter, DefaultQuote, DefaultEscape, DefaultComment, _
             ValueTrimmingOptions.UnquotedOnly, bufferSize)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the CsvReader class.
        ''' </summary>
        ''' <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
        ''' <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        ''' <param name="delimiter">The delimiter character separating each field (default is ',').</param>
        ''' <exception cref="T:ArgumentNullException">
        '''		<paramref name="reader"/> is a <see langword="null"/>.
        ''' </exception>
        ''' <exception cref="T:ArgumentException">
        '''		Cannot read from <paramref name="reader"/>.
        ''' </exception>
        Public Sub New(ByVal reader As TextReader, ByVal hasHeaders As Boolean, ByVal delimiter As Char)
            Me.New(reader, hasHeaders, delimiter, DefaultQuote, DefaultEscape, DefaultComment, _
             ValueTrimmingOptions.UnquotedOnly, DefaultBufferSize)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the CsvReader class.
        ''' </summary>
        ''' <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
        ''' <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        ''' <param name="delimiter">The delimiter character separating each field (default is ',').</param>
        ''' <param name="bufferSize">The buffer size in bytes.</param>
        ''' <exception cref="T:ArgumentNullException">
        '''		<paramref name="reader"/> is a <see langword="null"/>.
        ''' </exception>
        ''' <exception cref="T:ArgumentException">
        '''		Cannot read from <paramref name="reader"/>.
        ''' </exception>
        Public Sub New(ByVal reader As TextReader, ByVal hasHeaders As Boolean, ByVal delimiter As Char, ByVal bufferSize As Integer)
            Me.New(reader, hasHeaders, delimiter, DefaultQuote, DefaultEscape, DefaultComment, _
             ValueTrimmingOptions.UnquotedOnly, bufferSize)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the CsvReader class.
        ''' </summary>
        ''' <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
        ''' <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        ''' <param name="delimiter">The delimiter character separating each field (default is ',').</param>
        ''' <param name="quote">The quotation character wrapping every field (default is ''').</param>
        ''' <param name="escape">
        ''' The escape character letting insert quotation characters inside a quoted field (default is '\').
        ''' If no escape character, set to '\0' to gain some performance.
        ''' </param>
        ''' <param name="comment">The comment character indicating that a line is commented out (default is '#').</param>
        ''' <param name="trimmingOptions">Determines which values should be trimmed.</param>
        ''' <exception cref="T:ArgumentNullException">
        '''		<paramref name="reader"/> is a <see langword="null"/>.
        ''' </exception>
        ''' <exception cref="T:ArgumentException">
        '''		Cannot read from <paramref name="reader"/>.
        ''' </exception>
        Public Sub New(ByVal reader As TextReader, ByVal hasHeaders As Boolean, ByVal delimiter As Char, ByVal quote As Char, ByVal escape As Char, ByVal comment As Char, _
        ByVal trimmingOptions As ValueTrimmingOptions)
            Me.New(reader, hasHeaders, delimiter, quote, escape, comment, _
             trimmingOptions, DefaultBufferSize)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the CsvReader class.
        ''' </summary>
        ''' <param name="reader">A <see cref="T:TextReader"/> pointing to the CSV file.</param>
        ''' <param name="hasHeaders"><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</param>
        ''' <param name="delimiter">The delimiter character separating each field (default is ',').</param>
        ''' <param name="quote">The quotation character wrapping every field (default is ''').</param>
        ''' <param name="escape">
        ''' The escape character letting insert quotation characters inside a quoted field (default is '\').
        ''' If no escape character, set to '\0' to gain some performance.
        ''' </param>
        ''' <param name="comment">The comment character indicating that a line is commented out (default is '#').</param>
        ''' <param name="trimmingOptions">Determines which values should be trimmed.</param>
        ''' <param name="bufferSize">The buffer size in bytes.</param>
        ''' <exception cref="T:ArgumentNullException">
        '''		<paramref name="reader"/> is a <see langword="null"/>.
        ''' </exception>
        ''' <exception cref="ArgumentOutOfRangeException">
        '''		<paramref name="bufferSize"/> must be 1 or more.
        ''' </exception>
        Public Sub New(ByVal reader As TextReader, ByVal hasHeaders As Boolean, ByVal delimiter As Char, _
            ByVal quote As Char, ByVal escape As Char, ByVal comment As Char, _
            ByVal trimmingOptions As ValueTrimmingOptions, ByVal bufferSize As Integer)

#If DEBUG Then
			_allocStack = New System.Diagnostics.StackTrace()
#End If

            If reader Is Nothing Then Throw New ArgumentNullException("reader")

            If bufferSize <= 0 Then Throw New ArgumentOutOfRangeException("bufferSize", bufferSize, _
                "Buffer size must be 1 or more.")

            _bufferSize = bufferSize

            If TypeOf reader Is StreamReader Then
                Dim stream As Stream = DirectCast(reader, StreamReader).BaseStream
                ' Handle bad implementations returning 0 or less
                If stream.CanSeek AndAlso stream.Length > 0 Then _bufferSize = _
                    CInt(Math.Min(bufferSize, stream.Length))
            End If

            _reader = reader
            _delimiter = delimiter
            _quote = quote
            _escape = escape
            _comment = comment

            _hasHeaders = hasHeaders
            _trimmingOptions = trimmingOptions
            _supportsMultiline = True
            _skipEmptyLines = True
            Me.DefaultHeaderName = "Column"

            _currentRecordIndex = -1
            _defaultParseErrorAction = ParseErrorAction.[RaiseEvent]

        End Sub

#End Region

#Region "Events"

        ''' <summary>
        ''' Occurs when there is an error while parsing the CSV stream.
        ''' </summary>
        Public Event ParseError As EventHandler(Of ParseErrorEventArgs)

        ''' <summary>
        ''' Raises the <see cref="M:ParseError"/> event.
        ''' </summary>
        ''' <param name="e">The <see cref="ParseErrorEventArgs"/> that contains the event data.</param>
        Protected Overridable Sub OnParseError(ByVal e As ParseErrorEventArgs)
            RaiseEvent ParseError(Me, e)
        End Sub

#End Region

#Region "Properties"

#Region "Settings"

        ''' <summary>
        ''' Gets the comment character indicating that a line is commented out.
        ''' </summary>
        ''' <value>The comment character indicating that a line is commented out.</value>
        Public ReadOnly Property Comment() As Char
            Get
                Return _comment
            End Get
        End Property

        ''' <summary>
        ''' Gets the escape character letting insert quotation characters inside a quoted field.
        ''' </summary>
        ''' <value>The escape character letting insert quotation characters inside a quoted field.</value>
        Public ReadOnly Property Escape() As Char
            Get
                Return _escape
            End Get
        End Property

        ''' <summary>
        ''' Gets the delimiter character separating each field.
        ''' </summary>
        ''' <value>The delimiter character separating each field.</value>
        Public ReadOnly Property Delimiter() As Char
            Get
                Return _delimiter
            End Get
        End Property

        ''' <summary>
        ''' Gets the quotation character wrapping every field.
        ''' </summary>
        ''' <value>The quotation character wrapping every field.</value>
        Public ReadOnly Property Quote() As Char
            Get
                Return _quote
            End Get
        End Property

        ''' <summary>
        ''' Indicates if field names are located on the first non commented line.
        ''' </summary>
        ''' <value><see langword="true"/> if field names are located on the first non commented line, otherwise, <see langword="false"/>.</value>
        Public ReadOnly Property HasHeaders() As Boolean
            Get
                Return _hasHeaders
            End Get
        End Property

        ''' <summary>
        ''' Indicates if spaces at the start and end of a field are trimmed.
        ''' </summary>
        ''' <value><see langword="true"/> if spaces at the start and end of a field are trimmed, otherwise, <see langword="false"/>.</value>
        Public ReadOnly Property TrimmingOption() As ValueTrimmingOptions
            Get
                Return _trimmingOptions
            End Get
        End Property

        ''' <summary>
        ''' Gets the buffer size.
        ''' </summary>
        Public ReadOnly Property BufferSize() As Integer
            Get
                Return _bufferSize
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the default action to take when a parsing error has occured.
        ''' </summary>
        ''' <value>The default action to take when a parsing error has occured.</value>
        Public Property DefaultParseErrorAction() As ParseErrorAction
            Get
                Return _defaultParseErrorAction
            End Get
            Set(ByVal value As ParseErrorAction)
                _defaultParseErrorAction = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the action to take when a field is missing.
        ''' </summary>
        ''' <value>The action to take when a field is missing.</value>
        Public Property MissingFieldAction() As MissingFieldAction
            Get
                Return _missingFieldAction
            End Get
            Set(ByVal value As MissingFieldAction)
                _missingFieldAction = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating if the reader supports multiline fields.
        ''' </summary>
        ''' <value>A value indicating if the reader supports multiline field.</value>
        Public Property SupportsMultiline() As Boolean
            Get
                Return _supportsMultiline
            End Get
            Set(ByVal value As Boolean)
                _supportsMultiline = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating if the reader will skip empty lines.
        ''' </summary>
        ''' <value>A value indicating if the reader will skip empty lines.</value>
        Public Property SkipEmptyLines() As Boolean
            Get
                Return _skipEmptyLines
            End Get
            Set(ByVal value As Boolean)
                _skipEmptyLines = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the default header name when it is an empty string or only whitespaces.
        ''' The header index will be appended to the specified name.
        ''' </summary>
        ''' <value>The default header name when it is an empty string or only whitespaces.</value>
        Public Property DefaultHeaderName() As String
            Get
                Return m_DefaultHeaderName
            End Get
            Set(ByVal value As String)
                m_DefaultHeaderName = value
            End Set
        End Property
        Private m_DefaultHeaderName As String

#End Region

#Region "State"

        ''' <summary>
        ''' Gets the maximum number of fields to retrieve for each record.
        ''' </summary>
        ''' <value>The maximum number of fields to retrieve for each record.</value>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Public ReadOnly Property FieldCount() As Integer _
            Implements IDataReader.FieldCount
            Get
                EnsureInitialize()
                Return _fieldCount
            End Get
        End Property

        ''' <summary>
        ''' Gets a value that indicates whether the current stream position is at the end of the stream.
        ''' </summary>
        ''' <value><see langword="true"/> if the current stream position is at the end of the stream; otherwise <see langword="false"/>.</value>
        Public Overridable ReadOnly Property EndOfStream() As Boolean
            Get
                Return _eof
            End Get
        End Property

        ''' <summary>
        ''' Gets the field headers.
        ''' </summary>
        ''' <returns>The field headers or an empty array if headers are not supported.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Public Function GetFieldHeaders() As String()
            EnsureInitialize()
            Debug.Assert(_fieldHeaders IsNot Nothing, "Field headers must be non null.")

            Dim fieldHeaders As String() = New String(_fieldHeaders.Length - 1) {}

            For i As Integer = 0 To fieldHeaders.Length - 1
                fieldHeaders(i) = _fieldHeaders(i)
            Next

            Return fieldHeaders
        End Function

        ''' <summary>
        ''' Gets the current record index in the CSV file.
        ''' </summary>
        ''' <value>The current record index in the CSV file.</value>
        Public Overridable ReadOnly Property CurrentRecordIndex() As Long
            Get
                Return _currentRecordIndex
            End Get
        End Property

        ''' <summary>
        ''' Indicates if one or more field are missing for the current record.
        ''' Resets after each successful record read.
        ''' </summary>
        Public ReadOnly Property MissingFieldFlag() As Boolean
            Get
                Return _missingFieldFlag
            End Get
        End Property

        ''' <summary>
        ''' Indicates if a parse error occured for the current record.
        ''' Resets after each successful record read.
        ''' </summary>
        Public ReadOnly Property ParseErrorFlag() As Boolean
            Get
                Return _parseErrorFlag
            End Get
        End Property

#End Region

#End Region

#Region "Indexers"

        ''' <summary>
        ''' Gets the field with the specified name and record position. <see cref="M:hasHeaders"/> must be <see langword="true"/>.
        ''' </summary>
        ''' <value>
        ''' The field with the specified name and record position.
        ''' </value>
        ''' <exception cref="T:ArgumentNullException">
        '''		<paramref name="field"/> is <see langword="null"/> or an empty string.
        ''' </exception>
        ''' <exception cref="T:InvalidOperationException">
        '''	The CSV does not have headers (<see cref="M:HasHeaders"/> property is <see langword="false"/>).
        ''' </exception>
        ''' <exception cref="T:ArgumentException">
        '''		<paramref name="field"/> not found.
        ''' </exception>
        ''' <exception cref="T:ArgumentOutOfRangeException">
        '''		Record index must be > 0.
        ''' </exception>
        ''' <exception cref="T:InvalidOperationException">
        '''		Cannot move to a previous record in forward-only mode.
        ''' </exception>
        ''' <exception cref="T:EndOfStreamException">
        '''		Cannot read record at <paramref name="record"/>.
        '''	</exception>
        '''	<exception cref="T:MalformedCsvException">
        '''		The CSV appears to be corrupt at the current position.
        ''' </exception>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Default Public ReadOnly Property Item(ByVal record As Integer, ByVal field As String) As String
            Get
                If Not MoveTo(record) Then
                    Throw New InvalidOperationException(String.Format(CultureInfo.InvariantCulture, _
                        "Cannot read record at index '{0}'.", record))
                End If

                Return Me(field)
            End Get
        End Property

        ''' <summary>
        ''' Gets the field at the specified index and record position.
        ''' </summary>
        ''' <value>
        ''' The field at the specified index and record position.
        ''' A <see langword="null"/> is returned if the field cannot be found for the record.
        ''' </value>
        ''' <exception cref="T:ArgumentOutOfRangeException">
        '''		<paramref name="field"/> must be included in [0, <see cref="M:FieldCount"/>[.
        ''' </exception>
        ''' <exception cref="T:ArgumentOutOfRangeException">
        '''		Record index must be > 0.
        ''' </exception>
        ''' <exception cref="T:InvalidOperationException">
        '''		Cannot move to a previous record in forward-only mode.
        ''' </exception>
        ''' <exception cref="T:EndOfStreamException">
        '''		Cannot read record at <paramref name="record"/>.
        ''' </exception>
        ''' <exception cref="T:MalformedCsvException">
        '''		The CSV appears to be corrupt at the current position.
        ''' </exception>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Default Public ReadOnly Property Item(ByVal record As Integer, ByVal field As Integer) As String
            Get
                If Not MoveTo(record) Then
                    Throw New InvalidOperationException(String.Format(CultureInfo.InvariantCulture, _
                        "Cannot read record at index '{0}'.", record))
                End If

                Return Me(field)
            End Get
        End Property

        ''' <summary>
        ''' Gets the field with the specified name. <see cref="M:hasHeaders"/> must be <see langword="true"/>.
        ''' </summary>
        ''' <value>
        ''' The field with the specified name.
        ''' </value>
        ''' <exception cref="T:ArgumentNullException">
        '''		<paramref name="field"/> is <see langword="null"/> or an empty string.
        ''' </exception>
        ''' <exception cref="T:InvalidOperationException">
        '''	The CSV does not have headers (<see cref="M:HasHeaders"/> property is <see langword="false"/>).
        ''' </exception>
        ''' <exception cref="T:ArgumentException">
        '''		<paramref name="field"/> not found.
        ''' </exception>
        ''' <exception cref="T:MalformedCsvException">
        '''		The CSV appears to be corrupt at the current position.
        ''' </exception>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Default Public ReadOnly Property Item(ByVal field As String) As String
            Get

                If String.IsNullOrEmpty(field) Then Throw New ArgumentNullException("field")
                If Not _hasHeaders Then Throw New InvalidOperationException( _
                    "The CSV does not have headers (CsvReader.HasHeaders property is false).")

                Dim index As Integer = GetFieldIndex(field)

                If index < 0 Then Throw New ArgumentException(String.Format(CultureInfo.InvariantCulture, _
                    "'{0}' field header not found.", field), "field")

                Return Me(index)

            End Get
        End Property

        ''' <summary>
        ''' Gets the field at the specified index.
        ''' </summary>
        ''' <value>The field at the specified index.</value>
        ''' <exception cref="T:ArgumentOutOfRangeException">
        '''		<paramref name="field"/> must be included in [0, <see cref="M:FieldCount"/>[.
        ''' </exception>
        ''' <exception cref="T:InvalidOperationException">
        '''		No record read yet. Call ReadLine() first.
        ''' </exception>
        ''' <exception cref="T:MalformedCsvException">
        '''		The CSV appears to be corrupt at the current position.
        ''' </exception>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Default Public Overridable ReadOnly Property Item(ByVal field As Integer) As String
            Get
                Return ReadField(field, False, False)
            End Get
        End Property

#End Region

#Region "Methods"

#Region "EnsureInitialize"

        ''' <summary>
        ''' Ensures that the reader is initialized.
        ''' </summary>
        Private Sub EnsureInitialize()
            If Not _initialized Then Me.ReadNextRecord(True, False)
            Debug.Assert(_fieldHeaders IsNot Nothing)
            Debug.Assert(_fieldHeaders.Length > 0 OrElse (_fieldHeaders.Length = 0 _
                AndAlso _fieldHeaderIndexes Is Nothing))
        End Sub

#End Region

#Region "GetFieldIndex"

        ''' <summary>
        ''' Gets the field index for the provided header.
        ''' </summary>
        ''' <param name="header">The header to look for.</param>
        ''' <returns>The field index for the provided header. -1 if not found.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Public Function GetFieldIndex(ByVal header As String) As Integer
            EnsureInitialize()
            Dim index As Integer
            If _fieldHeaderIndexes IsNot Nothing AndAlso _fieldHeaderIndexes.TryGetValue(header, index) Then
                Return index
            Else
                Return -1
            End If
        End Function

#End Region

#Region "CopyCurrentRecordTo"

        ''' <summary>
        ''' Copies the field array of the current record to a one-dimensional array, starting at the beginning of the target array.
        ''' </summary>
        ''' <param name="array"> The one-dimensional <see cref="T:Array"/> that is the destination of the fields of the current record.</param>
        ''' <exception cref="T:ArgumentNullException">
        '''		<paramref name="array"/> is <see langword="null"/>.
        ''' </exception>
        ''' <exception cref="ArgumentException">
        '''		The number of fields in the record is greater than the available space from <paramref name="index"/> to the end of <paramref name="array"/>.
        ''' </exception>
        Public Sub CopyCurrentRecordTo(ByVal array As String())
            CopyCurrentRecordTo(array, 0)
        End Sub

        ''' <summary>
        ''' Copies the field array of the current record to a one-dimensional array, starting at the beginning of the target array.
        ''' </summary>
        ''' <param name="array"> The one-dimensional <see cref="T:Array"/> that is the destination of the fields of the current record.</param>
        ''' <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        ''' <exception cref="T:ArgumentNullException">
        '''		<paramref name="array"/> is <see langword="null"/>.
        ''' </exception>
        ''' <exception cref="T:ArgumentOutOfRangeException">
        '''		<paramref name="index"/> is les than zero or is equal to or greater than the length <paramref name="array"/>. 
        ''' </exception>
        ''' <exception cref="InvalidOperationException">
        '''	No current record.
        ''' </exception>
        ''' <exception cref="ArgumentException">
        '''		The number of fields in the record is greater than the available space from <paramref name="index"/> to the end of <paramref name="array"/>.
        ''' </exception>
        Public Sub CopyCurrentRecordTo(ByVal array As String(), ByVal index As Integer)

            If array Is Nothing Then Throw New ArgumentNullException("array")
            If index < 0 OrElse index >= array.Length Then Throw New ArgumentOutOfRangeException( _
                "index", index, String.Empty)
            If _currentRecordIndex < 0 OrElse Not _initialized Then Throw New InvalidOperationException( _
                "No current record.")
            If array.Length - index < _fieldCount Then Throw New ArgumentException( _
                "The number of fields in the record is greater than the available space from index to the end of the destination array.", "array")

            For i As Integer = 0 To _fieldCount - 1
                If _parseErrorFlag Then
                    array(index + i) = Nothing
                Else
                    array(index + i) = Me(i)
                End If
            Next

        End Sub

#End Region

#Region "GetCurrentRawData"

        ''' <summary>
        ''' Gets the current raw CSV data.
        ''' </summary>
        ''' <remarks>Used for exception handling purpose.</remarks>
        ''' <returns>The current raw CSV data.</returns>
        Public Function GetCurrentRawData() As String
            If _buffer IsNot Nothing AndAlso _bufferLength > 0 Then
                Return New String(_buffer, 0, _bufferLength)
            Else
                Return String.Empty
            End If
        End Function

#End Region

#Region "IsWhiteSpace"

        ''' <summary>
        ''' Indicates whether the specified Unicode character is categorized as white space.
        ''' </summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns><see langword="true"/> if <paramref name="c"/> is white space; otherwise, <see langword="false"/>.</returns>
        Private Function IsWhiteSpace(ByVal c As Char) As Boolean
            ' Handle cases where the delimiter is a whitespace (e.g. tab)
            If c = _delimiter Then
                Return False
            Else
                ' See char.IsLatin1(char c) in Reflector
                If c <= "ÿ"c Then
                    Return (c = " "c OrElse c = ControlChars.Tab)
                Else
                    Return (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) _
                        = System.Globalization.UnicodeCategory.SpaceSeparator)
                End If
            End If
        End Function

#End Region

#Region "MoveTo"

        ''' <summary>
        ''' Moves to the specified record index.
        ''' </summary>
        ''' <param name="record">The record index.</param>
        ''' <returns><c>true</c> if the operation was successful; otherwise, <c>false</c>.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Public Overridable Function MoveTo(ByVal record As Long) As Boolean

            If record < _currentRecordIndex Then Return False

            ' Get number of record to read
            Dim offset As Long = record - _currentRecordIndex

            While offset > 0
                If Not ReadNextRecord() Then Return False
                offset -= 1
            End While

            Return True

        End Function

#End Region

#Region "ParseNewLine"

        ''' <summary>
        ''' Parses a new line delimiter.
        ''' </summary>
        ''' <param name="pos">The starting position of the parsing. Will contain the resulting end position.</param>
        ''' <returns><see langword="true"/> if a new line delimiter was found; otherwise, <see langword="false"/>.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Private Function ParseNewLine(ByRef pos As Integer) As Boolean

            Debug.Assert(pos <= _bufferLength)

            ' Check if already at the end of the buffer
            If pos = _bufferLength Then
                pos = 0
                If Not ReadBuffer() Then Return False
            End If

            Dim c As Char = _buffer(pos)

            ' Treat \r as new line only if it's not the delimiter

            If c = ControlChars.Cr AndAlso _delimiter <> ControlChars.Cr Then

                pos += 1

                ' Skip following \n (if there is one)

                If pos < _bufferLength Then
                    If _buffer(pos) = ControlChars.Lf Then pos += 1
                Else
                    If ReadBuffer() Then
                        If _buffer(0) = ControlChars.Lf Then
                            pos = 1
                        Else
                            pos = 0
                        End If
                    End If
                End If

                If pos >= _bufferLength Then
                    ReadBuffer()
                    pos = 0
                End If

                Return True

            ElseIf c = ControlChars.Lf Then

                pos += 1

                If pos >= _bufferLength Then
                    ReadBuffer()
                    pos = 0
                End If

                Return True

            End If

            Return False

        End Function

        ''' <summary>
        ''' Determines whether the character at the specified position is a new line delimiter.
        ''' </summary>
        ''' <param name="pos">The position of the character to verify.</param>
        ''' <returns>
        ''' 	<see langword="true"/> if the character at the specified position is a new line delimiter; otherwise, <see langword="false"/>.
        ''' </returns>
        Private Function IsNewLine(ByVal pos As Integer) As Boolean

            Debug.Assert(pos < _bufferLength)

            Dim c As Char = _buffer(pos)

            If c = ControlChars.Lf Then
                Return True
            ElseIf c = ControlChars.Cr AndAlso _delimiter <> ControlChars.Cr Then
                Return True
            Else
                Return False
            End If

        End Function

#End Region

#Region "ReadBuffer"

        ''' <summary>
        ''' Fills the buffer with data from the reader.
        ''' </summary>
        ''' <returns><see langword="true"/> if data was successfully read; otherwise, <see langword="false"/>.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Private Function ReadBuffer() As Boolean

            If _eof Then Return False

            CheckDisposed()

            _bufferLength = _reader.Read(_buffer, 0, _bufferSize)

            If _bufferLength > 0 Then
                Return True
            Else
                _eof = True
                _buffer = Nothing
                Return False
            End If

        End Function

#End Region

#Region "ReadField"

        ''' <summary>
        ''' Reads the field at the specified index.
        ''' Any unread fields with an inferior index will also be read as part of the required parsing.
        ''' </summary>
        ''' <param name="field">The field index.</param>
        ''' <param name="initializing">Indicates if the reader is currently initializing.</param>
        ''' <param name="discardValue">Indicates if the value(s) are discarded.</param>
        ''' <returns>
        ''' The field at the specified index. 
        ''' A <see langword="null"/> indicates that an error occured or that the last field has been reached during initialization.
        ''' </returns>
        ''' <exception cref="ArgumentOutOfRangeException">
        '''		<paramref name="field"/> is out of range.
        ''' </exception>
        ''' <exception cref="InvalidOperationException">
        '''		There is no current record.
        ''' </exception>
        ''' <exception cref="MissingFieldCsvException">
        '''		The CSV data appears to be missing a field.
        ''' </exception>
        ''' <exception cref="MalformedCsvException">
        '''		The CSV data appears to be malformed.
        ''' </exception>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Private Function ReadField(ByVal field As Integer, ByVal initializing As Boolean, _
            ByVal discardValue As Boolean) As String

            If Not initializing Then

                If field < 0 OrElse field >= _fieldCount Then Throw New ArgumentOutOfRangeException( _
                    "field", field, String.Format(CultureInfo.InvariantCulture, _
                    "Field index must be included in [0, FieldCount[. Specified field index was : '{0}'.", field))
                If _currentRecordIndex < 0 Then Throw New InvalidOperationException( _
                    "No current record.")

                ' Directly return field if cached
                If _fields(field) IsNot Nothing Then
                    Return _fields(field)
                ElseIf _missingFieldFlag Then
                    Return HandleMissingField(Nothing, field, _nextFieldStart)
                End If

            End If

            CheckDisposed()

            Dim index As Integer = _nextFieldIndex

            While index < field + 1

                ' Handle case where stated start of field is past buffer
                ' This can occur because _nextFieldStart is simply 1 + last char position of previous field
                If _nextFieldStart = _bufferLength Then
                    _nextFieldStart = 0
                    ' Possible EOF will be handled later (see Handle_EOF1)
                    ReadBuffer()
                End If

                Dim value As String = Nothing

                If _missingFieldFlag Then
                    value = HandleMissingField(value, index, _nextFieldStart)
                ElseIf _nextFieldStart = _bufferLength Then
                    ' Handle_EOF1: Handle EOF here

                    ' If current field is the requested field, then the value of the field is "" as in "f1,f2,f3,(\s*)"
                    ' otherwise, the CSV is malformed

                    If index = field Then

                        If Not discardValue Then
                            value = String.Empty
                            _fields(index) = value
                        End If

                        _missingFieldFlag = True

                    Else
                        value = HandleMissingField(value, index, _nextFieldStart)
                    End If

                Else
                    ' Trim spaces at start
                    If (_trimmingOptions And ValueTrimmingOptions.UnquotedOnly) <> 0 Then
                        SkipWhiteSpaces(_nextFieldStart)
                    End If

                    If _eof Then

                        value = String.Empty
                        _fields(field) = value
                        If field < _fieldCount Then _missingFieldFlag = True

                    ElseIf _buffer(_nextFieldStart) <> _quote Then
                        ' Non-quoted field

                        Dim start As Integer = _nextFieldStart
                        Dim pos As Integer = _nextFieldStart

                        While True

                            While pos < _bufferLength

                                Dim c As Char = _buffer(pos)

                                If c = _delimiter Then
                                    _nextFieldStart = pos + 1
                                    Exit While

                                ElseIf c = ControlChars.Cr OrElse c = ControlChars.Lf Then
                                    _nextFieldStart = pos
                                    _eol = True
                                    Exit While

                                Else
                                    pos += 1

                                End If

                            End While

                            If pos < _bufferLength Then

                                Exit While

                            Else

                                If Not discardValue Then value += New String(_buffer, start, pos - start)
                                start = 0
                                pos = 0
                                _nextFieldStart = 0

                                If Not ReadBuffer() Then Exit While

                            End If

                        End While

                        If Not discardValue Then

                            If (_trimmingOptions And ValueTrimmingOptions.UnquotedOnly) = 0 Then

                                If Not _eof AndAlso pos > start Then
                                    value += New String(_buffer, start, pos - start)
                                End If

                            Else

                                If Not _eof AndAlso pos > start Then
                                    ' Do the trimming
                                    pos -= 1
                                    While pos > -1 AndAlso IsWhiteSpace(_buffer(pos))
                                        pos -= 1
                                    End While
                                    pos += 1

                                    If pos > 0 Then
                                        value += New String(_buffer, start, pos - start)
                                    End If

                                Else
                                    pos = -1
                                End If

                                ' If pos <= 0, that means the trimming went past buffer start,
                                ' and the concatenated value needs to be trimmed too.
                                If pos <= 0 Then

                                    If value Is Nothing Then
                                        pos = -1
                                    Else
                                        pos = value.Length - 1
                                    End If

                                    ' Do the trimming
                                    While pos > -1 AndAlso IsWhiteSpace(value(pos))
                                        pos -= 1
                                    End While

                                    pos += 1

                                    If pos > 0 AndAlso pos <> value.Length Then
                                        value = value.Substring(0, pos)
                                    End If

                                End If
                            End If

                            If value Is Nothing Then value = String.Empty

                        End If

                        If _eol OrElse _eof Then

                            _eol = ParseNewLine(_nextFieldStart)

                            ' Reaching a new line is ok as long as the parser is initializing or it is the last field
                            If Not initializing AndAlso index <> _fieldCount - 1 Then

                                If value IsNot Nothing AndAlso value.Length = 0 Then value = Nothing
                                value = HandleMissingField(value, index, _nextFieldStart)

                            End If

                        End If

                        If Not discardValue Then _fields(index) = value

                    Else
                        ' Quoted field

                        ' Skip quote
                        Dim start As Integer = _nextFieldStart + 1
                        Dim pos As Integer = start

                        Dim quoted As Boolean = True
                        Dim escaped As Boolean = False

                        If (_trimmingOptions And ValueTrimmingOptions.QuotedOnly) <> 0 Then
                            SkipWhiteSpaces(start)
                            pos = start
                        End If

                        While True

                            While pos < _bufferLength

                                Dim c As Char = _buffer(pos)

                                If escaped Then
                                    escaped = False
                                    start = pos
                                    ' IF current char is escape AND (escape and quote are different OR next char is a quote)
                                ElseIf c = _escape AndAlso (_escape <> _quote OrElse _
                                    (pos + 1 < _bufferLength AndAlso _buffer(pos + 1) = _quote) OrElse _
                                    (pos + 1 = _bufferLength AndAlso Convert.ToChar(_reader.Peek()) = _quote)) Then

                                    If Not discardValue Then
                                        value += New String(_buffer, start, pos - start)
                                    End If

                                    escaped = True

                                ElseIf c = _quote Then
                                    quoted = False
                                    Exit While
                                End If

                                pos += 1

                            End While

                            If Not quoted Then
                                Exit While
                            Else

                                If Not discardValue AndAlso Not escaped Then
                                    value += New String(_buffer, start, pos - start)
                                End If

                                start = 0
                                pos = 0
                                _nextFieldStart = 0

                                If Not ReadBuffer() Then
                                    HandleParseError(New MalformedCsvException(GetCurrentRawData(), _
                                        _nextFieldStart, Math.Max(0, _currentRecordIndex), index), _
                                        _nextFieldStart)
                                    Return Nothing
                                End If

                            End If

                        End While

                        If Not _eof Then

                            ' Append remaining parsed buffer content
                            If Not discardValue AndAlso pos > start Then
                                value += New String(_buffer, start, pos - start)
                            End If

                            If Not discardValue AndAlso value IsNot Nothing AndAlso _
                                (_trimmingOptions And ValueTrimmingOptions.QuotedOnly) <> 0 Then

                                Dim newLength As Integer = value.Length
                                While newLength > 0 AndAlso IsWhiteSpace(value(newLength - 1))
                                    newLength -= 1
                                End While

                                If newLength < value.Length Then value = value.Substring(0, newLength)

                            End If

                            ' Skip quote
                            _nextFieldStart = pos + 1

                            ' Skip whitespaces between the quote and the delimiter/eol
                            SkipWhiteSpaces(_nextFieldStart)

                            ' Skip delimiter
                            Dim delimiterSkipped As Boolean
                            If _nextFieldStart < _bufferLength AndAlso _buffer(_nextFieldStart) = _delimiter Then
                                _nextFieldStart += 1
                                delimiterSkipped = True
                            Else
                                delimiterSkipped = False
                            End If

                            ' Skip new line delimiter if initializing or last field
                            ' (if the next field is missing, it will be caught when parsed)
                            If Not _eof AndAlso Not delimiterSkipped AndAlso _
                                (initializing OrElse index = _fieldCount - 1) Then
                                _eol = ParseNewLine(_nextFieldStart)
                            End If

                            ' If no delimiter is present after the quoted field and it is not the last field, then it is a parsing error
                            If Not delimiterSkipped AndAlso Not _eof AndAlso _
                                Not (_eol OrElse IsNewLine(_nextFieldStart)) Then
                                HandleParseError(New MalformedCsvException(GetCurrentRawData(), _
                                    _nextFieldStart, Math.Max(0, _currentRecordIndex), index), _nextFieldStart)
                            End If
                        End If

                        If Not discardValue Then

                            If value Is Nothing Then
                                value = String.Empty
                            End If

                            _fields(index) = value
                        End If

                    End If

                End If

                _nextFieldIndex = Math.Max(index + 1, _nextFieldIndex)

                If index = field Then
                    ' If initializing, return null to signify the last field has been reached

                    If initializing Then
                        If _eol OrElse _eof Then
                            Return Nothing
                        Else
                            If String.IsNullOrEmpty(value) Then
                                Return String.Empty
                            Else
                                Return value
                            End If
                        End If
                    Else
                        Return value
                    End If

                End If

                index += 1
            End While

            ' Getting here is bad ...
            HandleParseError(New MalformedCsvException(GetCurrentRawData(), _nextFieldStart, _
                Math.Max(0, _currentRecordIndex), index), _nextFieldStart)

            Return Nothing

        End Function

#End Region

#Region "ReadNextRecord"

        ''' <summary>
        ''' Reads the next record.
        ''' </summary>
        ''' <returns><see langword="true"/> if a record has been successfully reads; otherwise, <see langword="false"/>.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Public Function ReadNextRecord() As Boolean
            Return ReadNextRecord(False, False)
        End Function

        ''' <summary>
        ''' Reads the next record.
        ''' </summary>
        ''' <param name="onlyReadHeaders">
        ''' Indicates if the reader will proceed to the next record after having read headers.
        ''' <see langword="true"/> if it stops after having read headers; otherwise, <see langword="false"/>.
        ''' </param>
        ''' <param name="skipToNextLine">
        ''' Indicates if the reader will skip directly to the next line without parsing the current one. 
        ''' To be used when an error occurs.
        ''' </param>
        ''' <returns><see langword="true"/> if a record has been successfully reads; otherwise, <see langword="false"/>.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Protected Overridable Function ReadNextRecord(ByVal onlyReadHeaders As Boolean, ByVal skipToNextLine__1 As Boolean) As Boolean

            If _eof Then
                If _firstRecordInCache Then
                    _firstRecordInCache = False
                    _currentRecordIndex += 1
                    Return True
                Else
                    Return False
                End If
            End If

            CheckDisposed()

            If Not _initialized Then

                _buffer = New Char(_bufferSize - 1) {}

                ' will be replaced if and when headers are read
                _fieldHeaders = New String(-1) {}

                If Not ReadBuffer() Then Return False

                If Not SkipEmptyAndCommentedLines(_nextFieldStart) Then Return False

                ' Keep growing _fields array until the last field has been found
                ' and then resize it to its final correct size

                _fieldCount = 0
                _fields = New String(15) {}

                While ReadField(_fieldCount, True, False) IsNot Nothing
                    If _parseErrorFlag Then
                        _fieldCount = 0
                        Array.Clear(_fields, 0, _fields.Length)
                        _parseErrorFlag = False
                        _nextFieldIndex = 0
                    Else
                        _fieldCount += 1

                        If _fieldCount = _fields.Length Then
                            Array.Resize(Of String)(_fields, (_fieldCount + 1) * 2)
                        End If
                    End If
                End While

                ' _fieldCount contains the last field index, but it must contains the field count,
                ' so increment by 1
                _fieldCount += 1

                If _fields.Length <> _fieldCount Then
                    Array.Resize(Of String)(_fields, _fieldCount)
                End If

                _initialized = True

                ' If headers are present, call ReadNextRecord again
                If _hasHeaders Then

                    ' Don't count first record as it was the headers
                    _currentRecordIndex = -1

                    _firstRecordInCache = False

                    _fieldHeaders = New String(_fieldCount - 1) {}
                    _fieldHeaderIndexes = New Dictionary(Of String, Integer)(_fieldCount, _fieldHeaderComparer)

                    For i As Integer = 0 To _fields.Length - 1

                        Dim headerName As String = _fields(i)
                        If String.IsNullOrEmpty(headerName) OrElse headerName.Trim().Length = 0 Then
                            headerName = Me.DefaultHeaderName & i.ToString()
                        End If

                        _fieldHeaders(i) = headerName
                        _fieldHeaderIndexes.Add(headerName, i)

                    Next

                    ' Proceed to first record
                    If Not onlyReadHeaders Then

                        ' Calling again ReadNextRecord() seems to be simpler, 
                        ' but in fact would probably cause many subtle bugs because a derived class does not expect a recursive behavior
                        ' so simply do what is needed here and no more.

                        If Not SkipEmptyAndCommentedLines(_nextFieldStart) Then Return False

                        Array.Clear(_fields, 0, _fields.Length)
                        _nextFieldIndex = 0
                        _eol = False

                        _currentRecordIndex += 1
                        Return True

                    End If
                Else
                    If onlyReadHeaders Then
                        _firstRecordInCache = True
                        _currentRecordIndex = -1
                    Else
                        _firstRecordInCache = False
                        _currentRecordIndex = 0
                    End If
                End If

            Else

                If skipToNextLine__1 Then
                    SkipToNextLine(_nextFieldStart)
                ElseIf _currentRecordIndex > -1 AndAlso Not _missingFieldFlag Then
                    ' If not already at end of record, move there
                    If Not _eol AndAlso Not _eof Then
                        If Not _supportsMultiline Then
                            SkipToNextLine(_nextFieldStart)
                        Else
                            ' a dirty trick to handle the case where extra fields are present
                            While ReadField(_nextFieldIndex, True, True) IsNot Nothing
                            End While
                        End If
                    End If
                End If

                If Not _firstRecordInCache AndAlso Not SkipEmptyAndCommentedLines(_nextFieldStart) Then Return False

                If _hasHeaders OrElse Not _firstRecordInCache Then _eol = False

                ' Check to see if the first record is in cache.
                ' This can happen when initializing a reader with no headers
                ' because one record must be read to get the field count automatically
                If _firstRecordInCache Then
                    _firstRecordInCache = False
                Else
                    Array.Clear(_fields, 0, _fields.Length)
                    _nextFieldIndex = 0
                End If

                _missingFieldFlag = False
                _parseErrorFlag = False
                _currentRecordIndex += 1

            End If

            Return True

        End Function

#End Region

#Region "SkipEmptyAndCommentedLines"

        ''' <summary>
        ''' Skips empty and commented lines.
        ''' If the end of the buffer is reached, its content be discarded and filled again from the reader.
        ''' </summary>
        ''' <param name="pos">
        ''' The position in the buffer where to start parsing. 
        ''' Will contains the resulting position after the operation.
        ''' </param>
        ''' <returns><see langword="true"/> if the end of the reader has not been reached; otherwise, <see langword="false"/>.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Private Function SkipEmptyAndCommentedLines(ByRef pos As Integer) As Boolean

            If pos < _bufferLength Then DoSkipEmptyAndCommentedLines(pos)

            While pos >= _bufferLength AndAlso Not _eof

                If ReadBuffer() Then
                    pos = 0
                    DoSkipEmptyAndCommentedLines(pos)
                Else
                    Return False
                End If

            End While

            Return Not _eof

        End Function

        ''' <summary>
        ''' <para>Worker method.</para>
        ''' <para>Skips empty and commented lines.</para>
        ''' </summary>
        ''' <param name="pos">
        ''' The position in the buffer where to start parsing. 
        ''' Will contains the resulting position after the operation.
        ''' </param>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Private Sub DoSkipEmptyAndCommentedLines(ByRef pos As Integer)

            While pos < _bufferLength

                If _buffer(pos) = _comment Then
                    pos += 1
                    SkipToNextLine(pos)
                ElseIf _skipEmptyLines AndAlso ParseNewLine(pos) Then
                    Continue While
                Else
                    Exit While
                End If

            End While

        End Sub

#End Region

#Region "SkipWhiteSpaces"

        ''' <summary>
        ''' Skips whitespace characters.
        ''' </summary>
        ''' <param name="pos">The starting position of the parsing. Will contain the resulting end position.</param>
        ''' <returns><see langword="true"/> if the end of the reader has not been reached; otherwise, <see langword="false"/>.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Private Function SkipWhiteSpaces(ByRef pos As Integer) As Boolean

            While True

                While pos < _bufferLength AndAlso IsWhiteSpace(_buffer(pos))
                    pos += 1
                End While

                If pos < _bufferLength Then
                    Exit While
                Else
                    pos = 0
                    If Not ReadBuffer() Then Return False
                End If

            End While

            Return True

        End Function

#End Region

#Region "SkipToNextLine"

        ''' <summary>
        ''' Skips ahead to the next NewLine character.
        ''' If the end of the buffer is reached, its content be discarded and filled again from the reader.
        ''' </summary>
        ''' <param name="pos">
        ''' The position in the buffer where to start parsing. 
        ''' Will contains the resulting position after the operation.
        ''' </param>
        ''' <returns><see langword="true"/> if the end of the reader has not been reached; otherwise, <see langword="false"/>.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Private Function SkipToNextLine(ByRef pos As Integer) As Boolean
            ' ((pos = 0) == 0) is a little trick to reset position inline
            While (pos < _bufferLength OrElse (ReadBuffer() AndAlso _
                ((InlineAssignHelper(pos, 0)) = 0))) AndAlso Not ParseNewLine(pos)
                pos += 1
            End While

            Return Not _eof

        End Function

#End Region

#Region "HandleParseError"

        ''' <summary>
        ''' Handles a parsing error.
        ''' </summary>
        ''' <param name="error">The parsing error that occured.</param>
        ''' <param name="pos">The current position in the buffer.</param>
        ''' <exception cref="ArgumentNullException">
        '''	<paramref name="error"/> is <see langword="null"/>.
        ''' </exception>
        Private Sub HandleParseError(ByVal [error] As MalformedCsvException, ByRef pos As Integer)

            If [error] Is Nothing Then Throw New ArgumentNullException("error")

            _parseErrorFlag = True

            Select Case _defaultParseErrorAction

                Case ParseErrorAction.ThrowException
                    Throw [error]

                Case ParseErrorAction.[RaiseEvent]

                    Dim e As New ParseErrorEventArgs([error], ParseErrorAction.ThrowException)
                    OnParseError(e)

                    Select Case e.Action
                        Case ParseErrorAction.ThrowException
                            Throw e.[Error]

                        Case ParseErrorAction.[RaiseEvent]
                            Throw New InvalidOperationException(String.Format(CultureInfo.InvariantCulture, _
                                "'{0}' is not a valid ParseErrorAction while inside a ParseError event.", e.Action), e.[Error])

                        Case ParseErrorAction.AdvanceToNextLine
                            ' already at EOL when fields are missing, so don't skip to next line in that case
                            If Not _missingFieldFlag AndAlso pos >= 0 Then
                                SkipToNextLine(pos)
                            End If
                            Exit Select
                        Case Else

                            Throw New NotSupportedException(String.Format(CultureInfo.InvariantCulture, _
                                "'{0}' is not a supported ParseErrorAction.", e.Action), e.[Error])

                    End Select

                    Exit Select

                Case ParseErrorAction.AdvanceToNextLine

                    ' already at EOL when fields are missing, so don't skip to next line in that case
                    If Not _missingFieldFlag AndAlso pos >= 0 Then SkipToNextLine(pos)
                    Exit Select

                Case Else

                    Throw New NotSupportedException(String.Format(CultureInfo.InvariantCulture, _
                        "'{0}' is not a supported ParseErrorAction.", _defaultParseErrorAction), [error])

            End Select

        End Sub

#End Region

#Region "HandleMissingField"

        ''' <summary>
        ''' Handles a missing field error.
        ''' </summary>
        ''' <param name="value">The partially parsed value, if available.</param>
        ''' <param name="fieldIndex">The missing field index.</param>
        ''' <param name="currentPosition">The current position in the raw data.</param>
        ''' <returns>
        ''' The resulting value according to <see cref="M:MissingFieldAction"/>.
        ''' If the action is set to <see cref="T:MissingFieldAction.TreatAsParseError"/>,
        ''' then the parse error will be handled according to <see cref="DefaultParseErrorAction"/>.
        ''' </returns>
        Private Function HandleMissingField(ByVal value As String, ByVal fieldIndex As Integer, ByRef currentPosition As Integer) As String

            If fieldIndex < 0 OrElse fieldIndex >= _fieldCount Then Throw New ArgumentOutOfRangeException( _
                "fieldIndex", fieldIndex, String.Format(CultureInfo.InvariantCulture, _
                "Field index must be included in [0, FieldCount[. Specified field index was : '{0}'.", fieldIndex))

            _missingFieldFlag = True

            For i As Integer = fieldIndex + 1 To _fieldCount - 1
                _fields(i) = Nothing
            Next

            If value IsNot Nothing Then
                Return value
            Else
                Select Case _missingFieldAction
                    Case MissingFieldAction.ParseError
                        HandleParseError(New MissingFieldCsvException(GetCurrentRawData(), _
                            currentPosition, Math.Max(0, _currentRecordIndex), fieldIndex), currentPosition)
                        Return value

                    Case MissingFieldAction.ReplaceByEmpty
                        Return String.Empty

                    Case MissingFieldAction.ReplaceByNull
                        Return Nothing

                    Case Else
                        Throw New NotSupportedException(String.Format(CultureInfo.InvariantCulture, _
                            "'{0}' is not a supported missing field action.", _missingFieldAction))

                End Select

            End If

        End Function

#End Region

#End Region

#Region "IDataReader support methods"

        ''' <summary>
        ''' Validates the state of the data reader.
        ''' </summary>
        ''' <param name="validations">The validations to accomplish.</param>
        ''' <exception cref="InvalidOperationException">
        '''	No current record.
        ''' </exception>
        ''' <exception cref="InvalidOperationException">
        '''	This operation is invalid when the reader is closed.
        ''' </exception>
        Private Sub ValidateDataReader(ByVal validations As DataReaderValidations)
            If (validations And DataReaderValidations.IsInitialized) <> 0 AndAlso Not _initialized Then _
                Throw New InvalidOperationException("No current record.")
            If (validations And DataReaderValidations.IsNotClosed) <> 0 AndAlso _isDisposed Then _
                Throw New InvalidOperationException("This operation is invalid when the reader is closed.")
        End Sub

        ''' <summary>
        ''' Copy the value of the specified field to an array.
        ''' </summary>
        ''' <param name="field">The index of the field.</param>
        ''' <param name="fieldOffset">The offset in the field value.</param>
        ''' <param name="destinationArray">The destination array where the field value will be copied.</param>
        ''' <param name="destinationOffset">The destination array offset.</param>
        ''' <param name="length">The number of characters to copy from the field value.</param>
        ''' <returns></returns>
        Private Function CopyFieldToArray(ByVal field As Integer, ByVal fieldOffset As Long, _
            ByVal destinationArray As Array, ByVal destinationOffset As Integer, ByVal length As Integer) As Long

            EnsureInitialize()

            If field < 0 OrElse field >= _fieldCount Then Throw New ArgumentOutOfRangeException( _
                "field", field, String.Format(CultureInfo.InvariantCulture, _
                "Field index must be included in [0, FieldCount[. Specified field index was : '{0}'.", field))
            If fieldOffset < 0 OrElse fieldOffset >= Integer.MaxValue Then Throw New ArgumentOutOfRangeException( _
                "fieldOffset")

            ' Array.Copy(...) will do the remaining argument checks

            If length = 0 Then Return 0

            Dim value As String = Me(field)

            If value Is Nothing Then value = String.Empty

            Debug.Assert(fieldOffset < Integer.MaxValue)

            Debug.Assert(destinationArray.GetType Is GetType(Char()) OrElse _
                destinationArray.GetType Is GetType(Byte()))

            If destinationArray.GetType Is GetType(Char()) Then
                Array.Copy(value.ToCharArray(CInt(fieldOffset), length), 0, destinationArray, _
                    destinationOffset, length)
            Else

                Dim chars As Char() = value.ToCharArray(CInt(fieldOffset), length)
                Dim source As Byte() = New Byte(chars.Length - 1) {}
                For i As Integer = 0 To chars.Length - 1
                    source(i) = Convert.ToByte(chars(i))
                Next

                Array.Copy(source, 0, destinationArray, destinationOffset, length)

            End If

            Return length

        End Function

#End Region

#Region "IDataReader Members"

        Private ReadOnly Property RecordsAffected() As Integer Implements IDataReader.RecordsAffected
            Get
                ' For SELECT statements, -1 must be returned.
                Return -1
            End Get
        End Property

        Private ReadOnly Property IsClosed() As Boolean Implements IDataReader.IsClosed
            Get
                Return _eof
            End Get
        End Property

        Private Function NextResult() As Boolean Implements IDataReader.NextResult
            ValidateDataReader(DataReaderValidations.IsNotClosed)
            Return False
        End Function

        Private Sub Close() Implements IDataReader.Close
            Dispose()
        End Sub

        Private Function Read() As Boolean Implements IDataReader.Read
            ValidateDataReader(DataReaderValidations.IsNotClosed)
            Return ReadNextRecord()
        End Function

        Private ReadOnly Property Depth() As Integer Implements IDataReader.Depth
            Get
                ValidateDataReader(DataReaderValidations.IsNotClosed)
                Return 0
            End Get
        End Property

        Private Function GetSchemaTable() As DataTable Implements IDataReader.GetSchemaTable

            EnsureInitialize()
            ValidateDataReader(DataReaderValidations.IsNotClosed)

            Dim schema As New DataTable("SchemaTable")
            schema.Locale = CultureInfo.InvariantCulture
            schema.MinimumCapacity = _fieldCount

            schema.Columns.Add(SchemaTableColumn.AllowDBNull, GetType(Boolean)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.BaseColumnName, GetType(String)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.BaseSchemaName, GetType(String)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.BaseTableName, GetType(String)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.ColumnName, GetType(String)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.ColumnOrdinal, GetType(Integer)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.ColumnSize, GetType(Integer)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.DataType, GetType(Object)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.IsAliased, GetType(Boolean)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.IsExpression, GetType(Boolean)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.IsKey, GetType(Boolean)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.IsLong, GetType(Boolean)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.IsUnique, GetType(Boolean)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.NumericPrecision, GetType(Short)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.NumericScale, GetType(Short)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableColumn.ProviderType, GetType(Integer)).[ReadOnly] = True

            schema.Columns.Add(SchemaTableOptionalColumn.BaseCatalogName, GetType(String)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableOptionalColumn.BaseServerName, GetType(String)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableOptionalColumn.IsAutoIncrement, GetType(Boolean)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableOptionalColumn.IsHidden, GetType(Boolean)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableOptionalColumn.IsReadOnly, GetType(Boolean)).[ReadOnly] = True
            schema.Columns.Add(SchemaTableOptionalColumn.IsRowVersion, GetType(Boolean)).[ReadOnly] = True

            Dim columnNames As String()

            If _hasHeaders Then
                columnNames = _fieldHeaders
            Else
                columnNames = New String(_fieldCount - 1) {}
                For i As Integer = 0 To _fieldCount - 1
                    columnNames(i) = "Column" + i.ToString(CultureInfo.InvariantCulture)
                Next
            End If

            ' null marks columns that will change for each row
            ' 00- AllowDBNull
            ' 01- BaseColumnName
            ' 02- BaseSchemaName
            ' 03- BaseTableName
            ' 04- ColumnName
            ' 05- ColumnOrdinal
            ' 06- ColumnSize
            ' 07- DataType
            ' 08- IsAliased
            ' 09- IsExpression
            ' 10- IsKey
            ' 11- IsLong
            ' 12- IsUnique
            ' 13- NumericPrecision
            ' 14- NumericScale
            ' 15- ProviderType
            ' 16- BaseCatalogName
            ' 17- BaseServerName
            ' 18- IsAutoIncrement
            ' 19- IsHidden
            ' 20- IsReadOnly
            ' 21- IsRowVersion
            Dim schemaRow As Object() = New Object() {True, Nothing, String.Empty, String.Empty, _
                Nothing, Nothing, Integer.MaxValue, GetType(String), False, False, False, False, _
                False, DBNull.Value, DBNull.Value, CInt(DbType.[String]), String.Empty, String.Empty, _
                False, False, True, False}

            For i As Integer = 0 To columnNames.Length - 1
                schemaRow(1) = columnNames(i)
                ' Base column name
                schemaRow(4) = columnNames(i)
                ' Column name
                schemaRow(5) = i
                ' Column ordinal
                schema.Rows.Add(schemaRow)
            Next

            Return schema

        End Function

#End Region

#Region "IDataRecord Members"

        Private Function GetInt32(ByVal i As Integer) As Integer Implements IDataRecord.GetInt32

            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)

            Dim value As String = Me(i)

            If value Is Nothing Then
                Return 0
            Else
                Return Int32.Parse(value, CultureInfo.CurrentCulture)
            End If

        End Function

        Private ReadOnly Property IDataRecord_Item(ByVal name As String) As Object _
            Implements IDataRecord.Item
            Get
                ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
                Return Me(name)
            End Get
        End Property

        Private ReadOnly Property IDataRecord_Item(ByVal i As Integer) As Object _
            Implements IDataRecord.Item
            Get
                ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
                Return Me(i)
            End Get
        End Property

        Private Function GetValue(ByVal i As Integer) As Object Implements IDataRecord.GetValue
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            If DirectCast(Me, IDataRecord).IsDBNull(i) Then
                Return DBNull.Value
            Else
                Return Me(i)
            End If
        End Function

        Private Function IsDBNull(ByVal i As Integer) As Boolean Implements IDataRecord.IsDBNull
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return (String.IsNullOrEmpty(Me(i)))
        End Function

        Private Function GetBytes(ByVal i As Integer, ByVal fieldOffset As Long, ByVal buffer As Byte(), ByVal bufferoffset As Integer, ByVal length As Integer) As Long Implements IDataRecord.GetBytes
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return CopyFieldToArray(i, fieldOffset, buffer, bufferoffset, length)
        End Function

        Private Function GetByte(ByVal i As Integer) As Byte Implements IDataRecord.GetByte
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return Byte.Parse(Me(i), CultureInfo.CurrentCulture)
        End Function

        Private Function GetFieldType(ByVal i As Integer) As Type Implements IDataRecord.GetFieldType

            EnsureInitialize()
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)

            If i < 0 OrElse i >= _fieldCount Then
                Throw New ArgumentOutOfRangeException("i", i, String.Format(CultureInfo.InvariantCulture, _
                    "Field index must be included in [0, FieldCount[. Specified field index was : '{0}'.", i))
            End If

            Return GetType(String)

        End Function

        Private Function GetDecimal(ByVal i As Integer) As Decimal Implements IDataRecord.GetDecimal
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return [Decimal].Parse(Me(i), CultureInfo.CurrentCulture)
        End Function

        Private Function GetValues(ByVal values As Object()) As Integer Implements IDataRecord.GetValues

            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)

            Dim record As IDataRecord = DirectCast(Me, IDataRecord)

            For i As Integer = 0 To _fieldCount - 1
                values(i) = record.GetValue(i)
            Next

            Return _fieldCount

        End Function

        Private Function GetName(ByVal i As Integer) As String Implements IDataRecord.GetName

            EnsureInitialize()
            ValidateDataReader(DataReaderValidations.IsNotClosed)

            If i < 0 OrElse i >= _fieldCount Then Throw New ArgumentOutOfRangeException( _
                "i", i, String.Format(CultureInfo.InvariantCulture, _
                "Field index must be included in [0, FieldCount[. Specified field index was : '{0}'.", i))

            If _hasHeaders Then
                Return _fieldHeaders(i)
            Else
                Return "Column" + i.ToString(CultureInfo.InvariantCulture)
            End If

        End Function

        Private Function GetInt64(ByVal i As Integer) As Long Implements IDataRecord.GetInt64
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return Int64.Parse(Me(i), CultureInfo.CurrentCulture)
        End Function

        Private Function GetDouble(ByVal i As Integer) As Double Implements IDataRecord.GetDouble
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return [Double].Parse(Me(i), CultureInfo.CurrentCulture)
        End Function

        Private Function GetBoolean(ByVal i As Integer) As Boolean Implements IDataRecord.GetBoolean

            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)

            Dim value As String = Me(i)

            Dim result As Integer

            If Int32.TryParse(value, result) Then
                Return (result <> 0)
            Else
                Return Boolean.Parse(value)
            End If

        End Function

        Private Function GetGuid(ByVal i As Integer) As Guid Implements IDataRecord.GetGuid
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return New Guid(Me(i))
        End Function

        Private Function GetDateTime(ByVal i As Integer) As DateTime Implements IDataRecord.GetDateTime
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return DateTime.Parse(Me(i), CultureInfo.CurrentCulture)
        End Function

        Private Function GetOrdinal(ByVal name As String) As Integer Implements IDataRecord.GetOrdinal

            EnsureInitialize()
            ValidateDataReader(DataReaderValidations.IsNotClosed)

            Dim index As Integer

            If Not _fieldHeaderIndexes.TryGetValue(name, index) Then Throw New ArgumentException( _
                String.Format(CultureInfo.InvariantCulture, _
                "'{0}' field header not found.", name), "name")

            Return index

        End Function

        Private Function GetDataTypeName(ByVal i As Integer) As String Implements IDataRecord.GetDataTypeName
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return GetType(String).FullName
        End Function

        Private Function GetFloat(ByVal i As Integer) As Single Implements IDataRecord.GetFloat
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return Single.Parse(Me(i), CultureInfo.CurrentCulture)
        End Function

        Private Function GetData(ByVal i As Integer) As IDataReader Implements IDataRecord.GetData
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            If i = 0 Then
                Return Me
            Else
                Return Nothing
            End If
        End Function

        Private Function GetChars(ByVal i As Integer, ByVal fieldoffset As Long, ByVal buffer As Char(), ByVal bufferoffset As Integer, ByVal length As Integer) As Long Implements IDataRecord.GetChars
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return CopyFieldToArray(i, fieldoffset, buffer, bufferoffset, length)
        End Function

        Private Function GetString(ByVal i As Integer) As String Implements IDataRecord.GetString
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return Me(i)
        End Function

        Private Function GetChar(ByVal i As Integer) As Char Implements IDataRecord.GetChar
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return Char.Parse(Me(i))
        End Function

        Private Function GetInt16(ByVal i As Integer) As Short Implements IDataRecord.GetInt16
            ValidateDataReader(DataReaderValidations.IsInitialized Or DataReaderValidations.IsNotClosed)
            Return Int16.Parse(Me(i), CultureInfo.CurrentCulture)
        End Function

#End Region

#Region "IEnumerable<string[]> Members"

        ''' <summary>
        ''' Returns an <see cref="T:RecordEnumerator"/>  that can iterate through CSV records.
        ''' </summary>
        ''' <returns>An <see cref="T:RecordEnumerator"/>  that can iterate through CSV records.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Public Function GetEnumerator() As CsvReader.RecordEnumerator
            Return New CsvReader.RecordEnumerator(Me)
        End Function

        ''' <summary>
        ''' Returns an <see cref="T:System.Collections.Generics.IEnumerator"/>  that can iterate through CSV records.
        ''' </summary>
        ''' <returns>An <see cref="T:System.Collections.Generics.IEnumerator"/>  that can iterate through CSV records.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Private Function IEnumerableStringArray_GetEnumerator() As IEnumerator(Of String()) _
            Implements IEnumerable(Of String()).GetEnumerator
            Return Me.GetEnumerator()
        End Function

#End Region

#Region "IEnumerable Members"

        ''' <summary>
        ''' Returns an <see cref="T:System.Collections.IEnumerator"/>  that can iterate through CSV records.
        ''' </summary>
        ''' <returns>An <see cref="T:System.Collections.IEnumerator"/>  that can iterate through CSV records.</returns>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        '''	The instance has been disposed of.
        ''' </exception>
        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

#End Region

#Region "IDisposable members"

#If DEBUG Then
		''' <summary>
		''' Contains the stack when the object was allocated.
		''' </summary>
		Private _allocStack As System.Diagnostics.StackTrace
#End If

        ''' <summary>
        ''' Contains the disposed status flag.
        ''' </summary>
        Private _isDisposed As Boolean = False

        ''' <summary>
        ''' Contains the locking object for multi-threading purpose.
        ''' </summary>
        Private ReadOnly _lock As New Object()

        ''' <summary>
        ''' Occurs when the instance is disposed of.
        ''' </summary>
        Public Event Disposed As EventHandler

        ''' <summary>
        ''' Gets a value indicating whether the instance has been disposed of.
        ''' </summary>
        ''' <value>
        ''' 	<see langword="true"/> if the instance has been disposed of; otherwise, <see langword="false"/>.
        ''' </value>
        <System.ComponentModel.Browsable(False)> _
        Public ReadOnly Property IsDisposed() As Boolean
            Get
                Return _isDisposed
            End Get
        End Property

        ''' <summary>
        ''' Raises the <see cref="M:Disposed"/> event.
        ''' </summary>
        ''' <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data.</param>
        Protected Overridable Sub OnDisposed(ByVal e As EventArgs)
            RaiseEvent Disposed(Me, e)
        End Sub

        ''' <summary>
        ''' Checks if the instance has been disposed of, and if it has, throws an <see cref="T:System.ComponentModel.ObjectDisposedException"/>; otherwise, does nothing.
        ''' </summary>
        ''' <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ''' 	The instance has been disposed of.
        ''' </exception>
        ''' <remarks>
        ''' 	Derived classes should call this method at the start of all methods and properties that should not be accessed after a call to <see cref="M:Dispose()"/>.
        ''' </remarks>
        Protected Sub CheckDisposed()
            If _isDisposed Then
                Throw New ObjectDisposedException(Me.[GetType]().FullName)
            End If
        End Sub

        ''' <summary>
        ''' Releases all resources used by the instance.
        ''' </summary>
        ''' <remarks>
        ''' 	Calls <see cref="M:Dispose(Boolean)"/> with the disposing parameter set to <see langword="true"/> to free unmanaged and managed resources.
        ''' </remarks>
        Public Sub Dispose() Implements System.IDisposable.Dispose
            If Not _isDisposed Then
                Dispose(True)
                GC.SuppressFinalize(Me)
            End If
        End Sub

        ''' <summary>
        ''' Releases the unmanaged resources used by this instance and optionally releases the managed resources.
        ''' </summary>
        ''' <param name="disposing">
        ''' 	<see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.
        ''' </param>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            ' Refer to http://www.bluebytesoftware.com/blog/PermaLink,guid,88e62cdf-5919-4ac7-bc33-20c06ae539ae.aspx
            ' Refer to http://www.gotdotnet.com/team/libraries/whitepapers/resourcemanagement/resourcemanagement.aspx

            ' No exception should ever be thrown except in critical scenarios.
            ' Unhandled exceptions during finalization will tear down the process.
            If Not _isDisposed Then
                Try
                    ' Dispose-time code should call Dispose() on all owned objects that implement the IDisposable interface. 
                    ' "owned" means objects whose lifetime is solely controlled by the container. 
                    ' In cases where ownership is not as straightforward, techniques such as HandleCollector can be used.  
                    ' Large managed object fields should be nulled out.

                    ' Dispose-time code should also set references of all owned objects to null, after disposing them. This will allow the referenced objects to be garbage collected even if not all references to the "parent" are released. It may be a significant memory consumption win if the referenced objects are large, such as big arrays, collections, etc. 
                    If disposing Then
                        ' Acquire a lock on the object while disposing.

                        If _reader IsNot Nothing Then
                            SyncLock _lock
                                If _reader IsNot Nothing Then
                                    _reader.Dispose()

                                    _reader = Nothing
                                    _buffer = Nothing
                                    _eof = True
                                End If
                            End SyncLock
                        End If
                    End If
                Finally
                    ' Ensure that the flag is set
                    _isDisposed = True

                    ' Catch any issues about firing an event on an already disposed object.
                    Try
                        OnDisposed(EventArgs.Empty)
                    Catch
                    End Try
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Releases unmanaged resources and performs other cleanup operations before the instance is reclaimed by garbage collection.
        ''' </summary>
        Protected Overrides Sub Finalize()

            Try

#If DEBUG Then
				Debug.WriteLine("FinalizableObject was not disposed" + _allocStack.ToString())
#End If

                Dispose(False)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
            target = value
            Return value
        End Function

#End Region

    End Class

End Namespace