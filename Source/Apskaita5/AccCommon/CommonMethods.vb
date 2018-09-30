Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization


Public Module CommonMethods

    ''' <summary>
    ''' Gets a rounded value of d using Asymmetric Arithmetic Rounding algorithm
    ''' </summary>
    Public Function CRound(ByVal d As Double, ByVal r As Integer) As Double
        Dim i As Long = CLng(Math.Floor(d * Math.Pow(10, r)))
        If i + 0.5 > CType(d * Math.Pow(10, r), Decimal) Then
            Return i / Math.Pow(10, r)
        Else
            Return (i + 1) / Math.Pow(10, r)
        End If
    End Function
    Public Function CRound(ByVal d As Double) As Double
        Return CRound(d, 2)
    End Function


    ''' <summary>
    ''' Gets Easter date for the year.
    ''' </summary>
    ''' <param name="year">A year to find Easter date to.</param>
    ''' <returns>Easter date</returns>
    ''' <remarks></remarks>
    Public Function GetEasterDateByYear(ByVal year As Integer) As Date
        Dim c, n, k, i, j, l, m, d As Integer
        c = Convert.ToInt32(Math.Floor(year / 100))
        n = year - (19 * Convert.ToInt32(Math.Floor(year / 19)))
        k = Convert.ToInt32(Math.Floor((c - 17) / 25))
        i = c - Convert.ToInt32(Math.Floor(c / 4)) - Convert.ToInt32(Math.Floor((c - k) / 3)) + 19 * n + 15
        i = i - Convert.ToInt32(30 * Math.Floor(i / 30))
        i = i - Convert.ToInt32(Math.Floor(i / 28) * (1 - Math.Floor(i / 28) * Math.Floor(29 / (i + 1)) _
                * Math.Floor((21 - n) / 11)))
        j = year + Convert.ToInt32(Math.Floor(year / 4)) + i + 2 - c + Convert.ToInt32(Math.Floor(c / 4))
        j = j - Convert.ToInt32(7 * Math.Floor(j / 7))
        l = i - j
        m = 3 + Convert.ToInt32(Math.Floor((l + 40) / 44))
        d = l + 28 - Convert.ToInt32(31 * Math.Floor(m / 4))
        Return New Date(year, m, d)
    End Function


    ''' <summary>
    ''' Converts byte array encoded by <see cref="System.Drawing.Imaging.ImageFormat.Jpeg"/> 
    ''' to <see cref="System.Drawing.Image"/>.
    ''' </summary>
    ''' <param name="source">Byte array that contains image.</param>
    ''' <returns><see cref="System.Drawing.Image"/></returns>
    ''' <remarks></remarks>
    Public Function ByteArrayToImage(ByVal source As Byte()) As System.Drawing.Image

        If source Is Nothing OrElse source.Length < 10 Then Return Nothing

        Dim result As System.Drawing.Image = Nothing

        Using MS As New IO.MemoryStream(source)
            Try
                result = System.Drawing.Image.FromStream(MS)
            Catch ex As Exception
            End Try
        End Using

        Return result

    End Function

    ''' <summary>
    ''' Converts <see cref="System.Drawing.Image" /> to byte array encoded by 
    ''' <see cref="System.Drawing.Imaging.ImageFormat.Jpeg" />.
    ''' </summary>
    ''' <param name="source">Image to serialize to byte array.</param>
    ''' <returns>Byte array that the <paramref name="source"/> was serialized to.</returns>
    ''' <remarks></remarks>
    Public Function ImageToByteArray(ByVal source As System.Drawing.Image) As Byte()

        If source Is Nothing Then Return Nothing

        Dim result As Byte() = Nothing

        Dim ImageToSave As System.Drawing.Bitmap = New System.Drawing.Bitmap( _
            source.Width, source.Height, source.PixelFormat)
        Using gr As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(ImageToSave)
            gr.DrawImage(source, New System.Drawing.PointF(0, 0))
        End Using

        Using ms As New IO.MemoryStream
            Try
                ImageToSave.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                result = ms.ToArray
            Catch ex As Exception
            End Try
        End Using

        ImageToSave.Dispose()
        GC.Collect()

        Return result

    End Function

    ''' <summary>
    ''' Determines <see cref="System.Text.Encoding"/> of a text file.
    ''' </summary>
    ''' <param name="FilePath">Path to the text file.</param>
    ''' <param name="ThrowOnFail">Whether to throw an exception if the encoding is not determined.</param>
    ''' <returns><see cref="System.Text.Encoding"/> or the file.</returns>
    ''' <remarks></remarks>
    Public Function GetFileEncoding(ByVal FilePath As String, _
        ByVal ThrowOnFail As Boolean) As System.Text.Encoding

        Using afile As System.IO.FileStream = New System.IO.FileStream(FilePath, _
            IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)

            If afile.CanSeek Then

                Dim bom As Byte() = New Byte(3) {} ' Get the byte-order mark, if there is one

                afile.Read(bom, 0, 4)

                afile.Close()

                If (bom(0) = &HEF AndAlso bom(1) = &HBB AndAlso bom(2) = &HBF) OrElse _
                    (bom(0) = &HFF AndAlso bom(1) = &HFE) OrElse (bom(0) = &HFE AndAlso _
                    bom(1) = &HFF) OrElse (bom(0) = 0 AndAlso bom(1) = 0 AndAlso _
                    bom(2) = &HFE AndAlso bom(3) = &HFF) Then ' ucs-4

                    Return System.Text.Encoding.Unicode

                Else

                    Return System.Text.Encoding.Default

                End If

            Else

                afile.Close()

                If ThrowOnFail Then Throw New NotImplementedException( _
                    "Failed to determine file encoding.")

                Return System.Text.Encoding.Default

            End If

        End Using

    End Function

    ''' <summary>
    ''' Checks if a string value is nothing or contains only white spaces.
    ''' </summary>
    ''' <param name="s">String value to check.</param>
    Public Function StringIsNullOrEmpty(ByVal s As String) As Boolean
        Return s Is Nothing OrElse String.IsNullOrEmpty(s.Trim)
    End Function

    ''' <summary>
    ''' Gets a DateTime containing DateTime.Now timestamp with a second precision.
    ''' </summary>
    ''' <remarks>Required by most SQL engines.</remarks>
    Public Function GetCurrentTimeStamp() As DateTime
        Dim result As DateTime = DateTime.Now
        result = New DateTime(Convert.ToInt64(Math.Floor(result.Ticks / TimeSpan.TicksPerSecond) _
            * TimeSpan.TicksPerSecond))
        Return result
    End Function

    ''' <summary>
    ''' Clones an object using binary serialization.
    ''' </summary>
    ''' <typeparam name="T">Type of the object to clone.</typeparam>
    ''' <param name="source">Object to clone.</param>
    ''' <remarks></remarks>
    Public Function Clone(Of T)(ByVal source As T) As T

        If Not GetType(T).IsSerializable Then
            Throw New ArgumentException(My.Resources.CommonMethods_TheTypeMustBeSerializable, "source")
        End If

        ' Don't serialize a null object, simply return the default for that object
        If [Object].ReferenceEquals(source, Nothing) Then
            Return Nothing
        End If

        Dim formatter As IFormatter = New BinaryFormatter()
        Using stream As Stream = New MemoryStream()
            formatter.Serialize(stream, source)
            stream.Seek(0, SeekOrigin.Begin)
            Return DirectCast(formatter.Deserialize(stream), T)
        End Using

    End Function

    ''' <summary>
    ''' Serializes object to an xml string.
    ''' </summary>
    ''' <typeparam name="T">Type of the object to serialize.</typeparam>
    ''' <param name="obj">The object to serialize.</param>
    ''' <param name="enc">Text encoding to use for serialization. 
    ''' The default is <see cref="Encoding.Unicode">Unicode</see>.</param>
    ''' <remarks></remarks>
    Public Function ToXmlString(Of T)(ByVal obj As T, _
        Optional ByVal enc As Encoding = Nothing) As String

        If enc Is Nothing Then enc = Encoding.Unicode

        Dim serializer As New XmlSerializer(GetType(T))
        Dim settings As New XmlWriterSettings

        settings.Indent = True
        settings.IndentChars = " "
        settings.Encoding = enc

        Using ms As New IO.MemoryStream
            Using writer As XmlWriter = XmlWriter.Create(ms, settings)
                serializer.Serialize(writer, obj)
                Return enc.GetString(ms.ToArray())
            End Using
        End Using

    End Function

    ''' <summary>
    ''' Deserializes object from an xml string.
    ''' </summary>
    ''' <typeparam name="T">Type of the object to deserialize.</typeparam>
    ''' <param name="xmlString">An xml string containing serialized object data.</param>
    ''' <param name="enc">Text encoding to use for serialization. 
    ''' The default is <see cref="Encoding.Unicode">Unicode</see>.</param>
    ''' <remarks></remarks>
    Public Function FromXmlString(Of T)(ByVal xmlString As String, _
        Optional ByVal enc As Encoding = Nothing) As T

        If enc Is Nothing Then enc = Encoding.Unicode

        Dim serializer As New XmlSerializer(GetType(T))

        Using ms As New IO.MemoryStream(enc.GetBytes(xmlString))
            Return DirectCast(serializer.Deserialize(ms), T)
        End Using

    End Function

    ''' <summary>
    ''' Gets an xml string that is stored in the XmlDocument.
    ''' </summary>
    ''' <param name="document">XmlDocument to save to xml string</param>
    ''' <returns></returns>
    Public Function ToXmlString(document As XmlDocument) As String

        Using sw As New StringWriter()
            Using xw As New XmlTextWriter(sw)
                document.WriteTo(xw)
                Return sw.ToString
            End Using
        End Using

    End Function

#Region " String manipulation methods "

    ''' <summary>
    ''' Gets a substring from Tab (CHR9) delimited string.
    ''' </summary>
    ''' <param name="SourceString">Tab (CHR9) delimited string.</param>
    ''' <param name="index">Number (index) of substring to retrieve.</param>
    Public Function GetElement(ByVal SourceString As String, ByVal index As Integer) As String
        Dim subStrings As String() = SourceString.Split(Chr(9))
        If subStrings.Length > index Then
            Return subStrings(index)
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' Gets a Tab (CHR9) delimited string consisting of the elements provided.
    ''' </summary>
    ''' <param name="list">List of the elements.</param>
    <Obsolete()> _
    Public Function ElementsToString(ByVal list As List(Of String)) As String
        If list Is Nothing OrElse list.Count < 1 Then Return ""
        If list.Count = 1 Then Return list(0)
        Return String.Join(Chr(9), list.ToArray)
    End Function

    ''' <summary>
    ''' Gets a string which length does not exceeds maximum length provided.
    ''' </summary>
    ''' <param name="MaxLength">Maximum allowed length of the string.</param>
    ''' <param name="s">String to be checked.</param>
    Public Function GetLimitedLengthString(ByVal s As String, ByVal MaxLength As Integer) As String
        If s Is Nothing Then Return ""
        If s.Trim.Length <= MaxLength Then Return s.Trim
        Return s.Trim.Substring(0, MaxLength)
    End Function

    ''' <summary>
    ''' Gets a string which length is at least minimum length provided.
    ''' </summary>
    ''' <param name="s">Base string.</param>
    ''' <param name="MinLength">Minimum length of the string to be returned.</param>
    ''' <param name="CharToAdd">Char to be used when adding to the minimum lenght.</param>
    ''' <param name="AddAtTheBegining">If set to TRUE, chars are added at the begining of the string (if it's too short).</param>
    Public Function GetMinLengthString(ByVal s As String, ByVal MinLength As Integer, _
        ByVal CharToAdd As Char, Optional ByVal AddAtTheBegining As Boolean = True) As String

        If s Is Nothing OrElse String.IsNullOrEmpty(s.Trim) Then
            Return String.Empty.PadLeft(MinLength, CharToAdd)
        End If

        If s.Trim.Length >= MinLength Then
            Return s.Trim
        End If

        If AddAtTheBegining Then
            Return s.Trim.PadLeft(MinLength, CharToAdd)
        Else
            Return s.Trim.PadRight(MinLength, CharToAdd)
        End If

    End Function

    ''' <summary>
    ''' Gets a numeric part (consisting of numbers) of the string.
    ''' </summary>
    ''' <param name="s">String which the numeric part is to be extracted from.</param>
    Public Function GetNumericPart(ByVal s As String) As String

        If s Is Nothing OrElse String.IsNullOrEmpty(s.Trim) Then
            Return ""
        End If

        Dim trimedString As String = s.Trim

        Dim result As String = ""

        For i As Integer = trimedString.Length To 1 Step -1

            If Not Char.IsNumber(trimedString.Chars(i - 1)) Then
                Return result
            End If

            result = trimedString.Chars(i - 1) & result

        Next

        Return result

    End Function

    ''' <summary>
    ''' Adds a new string (line) separated by VbCrLf to the TargetString.
    ''' </summary>
    ''' <param name="TargetString">Cumulative string, containing lines separated by VbCrLf.</param>
    ''' <param name="NewLineString">String containing the new line to add.</param>
    ''' <param name="AllowAddEmptyLine">Add the new line even if it is empty.</param>
    ''' <returns>TargetString added with the NewLineString.</returns>
    Public Function AddWithNewLine(ByVal targetString As String, _
        ByVal newLineString As String, ByVal allowAddEmptyLine As Boolean) As String

        Dim validatedNewLineString As String = newLineString

        If validatedNewLineString Is Nothing Then
            validatedNewLineString = ""
        End If

        validatedNewLineString = validatedNewLineString.Trim

        Dim validatedTargetString As String = targetString

        If validatedTargetString Is Nothing Then
            validatedTargetString = ""
        End If

        If allowAddEmptyLine OrElse Not String.IsNullOrEmpty(validatedNewLineString) Then

            If String.IsNullOrEmpty(validatedTargetString.Trim) Then
                Return validatedNewLineString
            Else
                Return validatedTargetString.Trim & vbCrLf & validatedNewLineString.Trim
            End If

        Else

            Return validatedTargetString

        End If

    End Function

    ''' <summary>
    ''' Gets a string (column) value separated by delimiter from the value.
    ''' If value is nothing or empty, returns empty string.
    ''' If value field count is less then the index, returns empty string.
    ''' </summary>
    ''' <param name="value">String, containing fields separated by delimiter.</param>
    ''' <param name="delimiter">String that is used to delimit fields.</param>
    ''' <param name="index">Index of the field to return.</param>
    Public Function GetField(ByVal value As String, ByVal delimiter As String, _
        ByVal index As Integer) As String

        If index < 0 Then
            Throw New ArgumentOutOfRangeException("Parameter index cannot be less then 0 in method Utilities.GetField.")
        End If

        If delimiter Is Nothing Then
            Throw New ArgumentNullException("Parameter delimiter cannot be null in method Utilities.GetField.")
        End If

        If StringIsNullOrEmpty(value) Then
            Return ""
        End If

        Dim values As String() = value.Split(New String() {delimiter}, StringSplitOptions.None)

        If values.Length < index + 1 Then
            Return ""
        End If

        Return values(index).Trim

    End Function

#End Region

End Module
