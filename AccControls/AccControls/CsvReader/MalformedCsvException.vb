'	LumenWorks.Framework.IO.Csv.MalformedCsvException
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

Imports System.Globalization
Imports System.Runtime.Serialization
Imports System.Security.Permissions

Namespace CsvReader

    ''' <summary>
    ''' Represents the exception that is thrown when a CSV file is malformed.
    ''' </summary>
    <Serializable()> _
    Public Class MalformedCsvException
        Inherits Exception

#Region "Fields"

        Private Const NullString As String = Nothing

        ''' <summary>
        ''' Contains the message that describes the error.
        ''' </summary>
        Private _message As String

        ''' <summary>
        ''' Contains the raw data when the error occured.
        ''' </summary>
        Private _rawData As String

        ''' <summary>
        ''' Contains the current field index.
        ''' </summary>
        Private _currentFieldIndex As Integer

        ''' <summary>
        ''' Contains the current record index.
        ''' </summary>
        Private _currentRecordIndex As Long

        ''' <summary>
        ''' Contains the current position in the raw data.
        ''' </summary>
        Private _currentPosition As Integer

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the MalformedCsvException class.
        ''' </summary>
        Public Sub New()
            Me.New(NullString, Nothing)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MalformedCsvException class.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        Public Sub New(ByVal message As String)
            Me.New(message, Nothing)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MalformedCsvException class.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception.</param>
        Public Sub New(ByVal message As String, ByVal innerException As Exception)

            MyBase.New(String.Empty, innerException)

            _message = IIf(message Is Nothing, String.Empty, message)

            _rawData = String.Empty
            _currentPosition = -1
            _currentRecordIndex = -1
            _currentFieldIndex = -1

        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MalformedCsvException class.
        ''' </summary>
        ''' <param name="rawData">The raw data when the error occured.</param>
        ''' <param name="currentPosition">The current position in the raw data.</param>
        ''' <param name="currentRecordIndex">The current record index.</param>
        ''' <param name="currentFieldIndex">The current field index.</param>
        Public Sub New(ByVal rawData As String, ByVal currentPosition As Integer, _
            ByVal currentRecordIndex As Long, ByVal currentFieldIndex As Integer)
            Me.New(rawData, currentPosition, currentRecordIndex, currentFieldIndex, Nothing)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MalformedCsvException class.
        ''' </summary>
        ''' <param name="rawData">The raw data when the error occured.</param>
        ''' <param name="currentPosition">The current position in the raw data.</param>
        ''' <param name="currentRecordIndex">The current record index.</param>
        ''' <param name="currentFieldIndex">The current field index.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception.</param>
        Public Sub New(ByVal rawData As String, ByVal currentPosition As Integer, _
            ByVal currentRecordIndex As Long, ByVal currentFieldIndex As Integer, _
            ByVal innerException As Exception)

            MyBase.New([String].Empty, innerException)

            _rawData = IIf(rawData Is Nothing, String.Empty, rawData)
            _currentPosition = currentPosition
            _currentRecordIndex = currentRecordIndex
            _currentFieldIndex = currentFieldIndex

            _message = [String].Format(CultureInfo.InvariantCulture, _
                "The CSV appears to be corrupt near record '{0}' field '{1} at position '{2}'. Current raw data : '{3}'.", _currentRecordIndex, _
                _currentFieldIndex, _currentPosition, _rawData)

        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MalformedCsvException class with serialized data.
        ''' </summary>
        ''' <param name="info">The <see cref="T:SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        ''' <param name="context">The <see cref="T:StreamingContext"/> that contains contextual information about the source or destination.</param>
        Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)

            MyBase.New(info, context)

            _message = info.GetString("MyMessage")

            _rawData = info.GetString("RawData")
            _currentPosition = info.GetInt32("CurrentPosition")
            _currentRecordIndex = info.GetInt64("CurrentRecordIndex")
            _currentFieldIndex = info.GetInt32("CurrentFieldIndex")

        End Sub

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gets the raw data when the error occured.
        ''' </summary>
        ''' <value>The raw data when the error occured.</value>
        Public ReadOnly Property RawData() As String
            Get
                Return _rawData
            End Get
        End Property

        ''' <summary>
        ''' Gets the current position in the raw data.
        ''' </summary>
        ''' <value>The current position in the raw data.</value>
        Public ReadOnly Property CurrentPosition() As Integer
            Get
                Return _currentPosition
            End Get
        End Property

        ''' <summary>
        ''' Gets the current record index.
        ''' </summary>
        ''' <value>The current record index.</value>
        Public ReadOnly Property CurrentRecordIndex() As Long
            Get
                Return _currentRecordIndex
            End Get
        End Property

        ''' <summary>
        ''' Gets the current field index.
        ''' </summary>
        ''' <value>The current record index.</value>
        Public ReadOnly Property CurrentFieldIndex() As Integer
            Get
                Return _currentFieldIndex
            End Get
        End Property

#End Region

#Region "Overrides"

        ''' <summary>
        ''' Gets a message that describes the current exception.
        ''' </summary>
        ''' <value>A message that describes the current exception.</value>
        Public Overrides ReadOnly Property Message() As String
            Get
                Return _message
            End Get
        End Property

        ''' <summary>
        ''' When overridden in a derived class, sets the <see cref="T:SerializationInfo"/> with information about the exception.
        ''' </summary>
        ''' <param name="info">The <see cref="T:SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        ''' <param name="context">The <see cref="T:StreamingContext"/> that contains contextual information about the source or destination.</param>
        Public Overrides Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)

            info.AddValue("MyMessage", _message)

            info.AddValue("RawData", _rawData)
            info.AddValue("CurrentPosition", _currentPosition)
            info.AddValue("CurrentRecordIndex", _currentRecordIndex)
            info.AddValue("CurrentFieldIndex", _currentFieldIndex)
        End Sub

#End Region

    End Class

End Namespace