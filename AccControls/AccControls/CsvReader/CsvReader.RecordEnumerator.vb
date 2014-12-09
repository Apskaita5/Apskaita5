'	LumenWorks.Framework.IO.CSV.CsvReader.RecordEnumerator
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

Namespace CsvReader

    Partial Public Class CsvReader

        ''' <summary>
        ''' Supports a simple iteration over the records of a <see cref="T:CsvReader"/>.
        ''' </summary>
        Public Structure RecordEnumerator
            Implements IEnumerator(Of String())
            Implements IEnumerator

#Region "Fields"

            ''' <summary>
            ''' Contains the enumerated <see cref="T:CsvReader"/>.
            ''' </summary>
            Private _reader As CsvReader

            ''' <summary>
            ''' Contains the current record.
            ''' </summary>
            Private _current As String()

            ''' <summary>
            ''' Contains the current record index.
            ''' </summary>
            Private _currentRecordIndex As Long

#End Region

#Region "Constructors"

            ''' <summary>
            ''' Initializes a new instance of the <see cref="T:RecordEnumerator"/> class.
            ''' </summary>
            ''' <param name="reader">The <see cref="T:CsvReader"/> to iterate over.</param>
            ''' <exception cref="T:ArgumentNullException">
            '''		<paramref name="reader"/> is a <see langword="null"/>.
            ''' </exception>
            Public Sub New(ByVal reader As CsvReader)
                If reader Is Nothing Then Throw New ArgumentNullException("reader")

                _reader = reader
                _current = Nothing

                _currentRecordIndex = reader._currentRecordIndex

            End Sub

#End Region

#Region "IEnumerator<string[]> Members"

            ''' <summary>
            ''' Gets the current record.
            ''' </summary>
            Public ReadOnly Property Current() As String() _
                Implements IEnumerator(Of String()).Current
                Get
                    Return _current
                End Get
            End Property

            ''' <summary>
            ''' Advances the enumerator to the next record of the CSV.
            ''' </summary>
            ''' <returns><see langword="true"/> if the enumerator was successfully advanced to the next record, <see langword="false"/> if the enumerator has passed the end of the CSV.</returns>
            Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
                If _reader._currentRecordIndex <> _currentRecordIndex Then _
                    Throw New InvalidOperationException("Collection was modified; enumeration operation may not execute.")

                If _reader.ReadNextRecord() Then

                    _current = New String(_reader._fieldCount - 1) {}

                    _reader.CopyCurrentRecordTo(_current)
                    _currentRecordIndex = _reader._currentRecordIndex

                    Return True

                Else

                    _current = Nothing
                    _currentRecordIndex = _reader._currentRecordIndex

                    Return False

                End If

            End Function

#End Region

#Region "IEnumerator Members"

            ''' <summary>
            ''' Sets the enumerator to its initial position, which is before the first record in the CSV.
            ''' </summary>
            Public Sub Reset() Implements IEnumerator.Reset

                If _reader._currentRecordIndex <> _currentRecordIndex Then _
                    Throw New InvalidOperationException("Collection was modified; enumeration operation may not execute.")

                _reader.MoveTo(-1)

                _current = Nothing
                _currentRecordIndex = _reader._currentRecordIndex

            End Sub

            ''' <summary>
            ''' Gets the current record.
            ''' </summary>
            Private ReadOnly Property IEnumerator_Current() As Object _
                Implements IEnumerator.Current
                Get
                    If _reader._currentRecordIndex <> _currentRecordIndex Then
                        Throw New InvalidOperationException("Collection was modified; enumeration operation may not execute.")
                    End If

                    Return Me.Current
                End Get
            End Property

#End Region

#Region "IDisposable Members"

            ''' <summary>
            ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            ''' </summary>
            Public Sub Dispose() Implements IEnumerator(Of String()).Dispose
                _reader = Nothing
                _current = Nothing
            End Sub

#End Region

        End Structure

    End Class

End Namespace