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
    ''' Represents the exception that is thrown when a there is a missing field in a record of the CSV file.
    ''' </summary>
    ''' <remarks>
    ''' MissingFieldException would have been a better name, but there is already a <see cref="T:System.MissingFieldException"/>.
    ''' </remarks>
    <Serializable()> _
    Public Class MissingFieldCsvException
        Inherits MalformedCsvException

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the MissingFieldCsvException class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MissingFieldCsvException class.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MissingFieldCsvException class.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception.</param>
        Public Sub New(ByVal message As String, ByVal innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MissingFieldCsvException class.
        ''' </summary>
        ''' <param name="rawData">The raw data when the error occured.</param>
        ''' <param name="currentPosition">The current position in the raw data.</param>
        ''' <param name="currentRecordIndex">The current record index.</param>
        ''' <param name="currentFieldIndex">The current field index.</param>
        Public Sub New(ByVal rawData As String, ByVal currentPosition As Integer, ByVal currentRecordIndex As Long, ByVal currentFieldIndex As Integer)
            MyBase.New(rawData, currentPosition, currentRecordIndex, currentFieldIndex)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MissingFieldCsvException class.
        ''' </summary>
        ''' <param name="rawData">The raw data when the error occured.</param>
        ''' <param name="currentPosition">The current position in the raw data.</param>
        ''' <param name="currentRecordIndex">The current record index.</param>
        ''' <param name="currentFieldIndex">The current field index.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception.</param>
        Public Sub New(ByVal rawData As String, ByVal currentPosition As Integer, _
            ByVal currentRecordIndex As Long, ByVal currentFieldIndex As Integer, _
            ByVal innerException As Exception)
            MyBase.New(rawData, currentPosition, currentRecordIndex, currentFieldIndex, innerException)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MissingFieldCsvException class with serialized data.
        ''' </summary>
        ''' <param name="info">The <see cref="T:SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        ''' <param name="context">The <see cref="T:StreamingContext"/> that contains contextual information about the source or destination.</param>
        Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.New(info, context)
        End Sub

#End Region

    End Class

End Namespace