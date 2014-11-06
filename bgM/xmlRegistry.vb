Imports System.Threading
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports System.Diagnostics
Imports System.Text
Imports System.Collections
Imports System.Windows.Forms

Namespace Cll
    ''' <summary>
    ''' xmlRegistry is the class that saves and load registry trees to/from xml files.
    ''' It needs to directly access the API because Win32 classes do not allow good control over key type being created
    ''' </summary>
    Public Class xmlRegistry
        Public Structure InternalKeyLoad
            ' used in LoadAsXml
            Public strKeyname As String
            Public reg As RegistryKey
        End Structure

        Private Const ERROR_SUCCESS As Integer = 0

        Private Const HKEY_CLASSES_ROOT As UInteger = &H80000000UI
        Private Const HKEY_CURRENT_USER As UInteger = &H80000001UI
        Private Const HKEY_LOCAL_MACHINE As UInteger = &H80000002UI
        Private Const HKEY_USERS As UInteger = &H80000003UI
        Private Const HKEY_PERFORMANCE_DATA As UInteger = &H80000004UI
        Private Const HKEY_CURRENT_CONFIG As UInteger = &H80000005UI
        Private Const HKEY_DYN_DATA As UInteger = &H80000006UI

        Private Const REG_NONE As Integer = 0
        ' No value type
        Private Const REG_SZ As Integer = 1
        ' Unicode nul terminated string
        Private Const REG_EXPAND_SZ As Integer = 2
        ' Unicode nul terminated string
        Private Const REG_BINARY As Integer = 3
        ' Free form binary
        Private Const REG_DWORD As Integer = 4
        ' 32-bit number
        Private Const REG_DWORD_LITTLE_ENDIAN As Integer = 4
        ' 32-bit number =same as REG_DWORD)
        Private Const REG_DWORD_BIG_ENDIAN As Integer = 5
        ' 32-bit number
        Private Const REG_LINK As Integer = 6
        ' Symbolic Link = unicode
        Private Const REG_MULTI_SZ As Integer = 7
        ' Multiple Unicode strings
        Private Const REG_RESOURCE_LIST As Integer = 8
        ' Resource list in the resource map
        Private Const REG_FULL_RESOURCE_DESCRIPTOR As Integer = 9
        ' Resource list in the hardware description
        Private Const REG_RESOURCE_REQUIREMENTS_LIST As Integer = 10

        Private Const FAKEDITEM As String = "faked item"
        Private Const IDS_DEFAULTVALUENAME As String = "(Default)"
        Private Const IDS_DEFAULTVALUEVALUE As String = "(value not set)"

        ' standard XML
        Public Const XML_ROOT As String = "registry"
        Public Const XML_KEY As String = "k"
        Public Const XML_NAME As String = "name"
        Public Const XML_VALUE As String = "v"
        Public Const XML_VALUE2 As String = "value"
        Public Const XML_TYPE As String = "type"

        Private _nSaveCounter As Integer = 0

        <DllImport("advapi32.dll", EntryPoint:="RegOpenKey")> _
        Public Shared Function RegOpenKeyA(hKey As Integer, lpSubKey As String, ByRef phkResult As Integer) As Integer
        End Function
        <DllImport("advapi32.dll")> _
        Public Shared Function RegCloseKey(hKey As Integer) As Integer
        End Function
        <DllImport("advapi32.dll", EntryPoint:="RegQueryInfoKey")> _
        Public Shared Function RegQueryInfoKeyA(hKey As Integer, lpClass As String, ByRef lpcbClass As Integer, lpReserved As Integer, ByRef lpcSubKeys As Integer, ByRef lpcbMaxSubKeyLen As Integer, _
            ByRef lpcbMaxClassLen As Integer, ByRef lpcValues As Integer, ByRef lpcbMaxValueNameLen As Integer, ByRef lpcbMaxValueLen As Integer, ByRef lpcbSecurityDescriptor As Integer, ByRef lpftLastWriteTime As FILETIME) As Integer
        End Function
        <DllImport("advapi32.dll", EntryPoint:="RegEnumValue")> _
        Public Shared Function RegEnumValueA(hKey As Integer, dwIndex As Integer, ByRef lpValueName As Byte, ByRef lpcbValueName As Integer, lpReserved As Integer, ByRef lpType As Integer, _
            ByRef lpData As Byte, ByRef lpcbData As Integer) As Integer
        End Function
        <DllImport("advapi32.dll", EntryPoint:="RegEnumKeyEx")> _
        Public Shared Function RegEnumKeyExA(hKey As Integer, dwIndex As Integer, ByRef lpName As Byte, ByRef lpcbName As Integer, lpReserved As Integer, lpClass As String, _
            ByRef lpcbClass As Integer, ByRef lpftLastWriteTime As FILETIME) As Integer
        End Function
        <DllImport("advapi32.dll", EntryPoint:="RegSetValueEx")> _
        Public Shared Function RegSetValueExA(hKey As Integer, lpSubKey As String, reserved As Integer, dwType As Integer, ByRef lpData As Byte, cbData As Integer) As Integer
        End Function

        Public Sub New()
        End Sub

        ''' <summary>
        ''' Gets the Win32 handle for a given RegistryKey.
        ''' </summary>
        ''' <param name="registryKey">registry key you want the handle for</param>
        ''' <returns>Desired handle</returns>
        Public Shared Function getRegistryHandle(registryKey As RegistryKey) As Integer
            Dim type As Type = registryKey.[GetType]()
            Dim fieldInfo As FieldInfo = type.GetField("hkey", BindingFlags.Instance Or BindingFlags.NonPublic)
            Return CType(fieldInfo.GetValue(registryKey), IntPtr).ToInt32()
        End Function

        Public Shared Function keyExists(strInPath As String) As Boolean
            Dim strPath As String = strInPath

            If strPath.Length = 0 Then
                Return False
            End If

            Dim strMainKeyname As String = strPath

            Dim nSlash As Integer = strPath.IndexOf("\")
            If nSlash > -1 Then
                strMainKeyname = strPath.Substring(0, nSlash)
                strPath = strPath.Substring(nSlash + 1)
            Else
                strPath = ""
            End If

            Return keyExists(strMainKeyname, strPath)
        End Function

        Public Shared Function keyExists(strMainKey As String, strPath As String) As Boolean
            Dim bKeyExists As Boolean = False
            Dim reg As RegistryKey = openKey(strMainKey, strPath)

            If reg IsNot Nothing Then
                bKeyExists = True
                reg.Close()
            End If

            Return bKeyExists
        End Function

        Public Shared Function openKey(strMainKey As String, strPath As String) As RegistryKey
            Dim reg As RegistryKey = Nothing

            Try
                If strMainKey.ToUpper().CompareTo("HKEY_CLASSES_ROOT") = 0 Then
                    reg = Registry.ClassesRoot.OpenSubKey(strPath, True)
                ElseIf strMainKey.ToUpper().CompareTo("HKEY_CURRENT_USER") = 0 Then
                    reg = Registry.CurrentUser.OpenSubKey(strPath, True)
                ElseIf strMainKey.ToUpper().CompareTo("HKEY_LOCAL_MACHINE") = 0 Then
                    reg = Registry.LocalMachine.OpenSubKey(strPath, True)
                ElseIf strMainKey.ToUpper().CompareTo("HKEY_USERS") = 0 Then
                    reg = Registry.Users.OpenSubKey(strPath, True)
                ElseIf strMainKey.ToUpper().CompareTo("HKEY_CURRENT_CONFIG") = 0 Then
                    reg = Registry.CurrentConfig.OpenSubKey(strPath, True)
                Else
                    Return Nothing
                    ' break here
                End If
            Catch generatedExceptionName As Exception
            End Try

            Return reg
        End Function

        Private Function addRegistryValues(hKey As Integer) As ArrayList
            Dim arrValues As New ArrayList()

            Dim classLength As Integer = 0
            Dim cSubKeys As Integer = 0
            ' number of subkeys 
            Dim cbMaxSubKey As Integer = 0
            ' longest subkey size 
            Dim cchMaxClass As Integer = 0
            ' longest class string 
            Dim cValues As Integer = 0
            ' number of values for key 
            Dim cchMaxValue As Integer = 0
            ' longest value name 
            Dim cbMaxValueData As Integer = 0
            ' longest value data 
            Dim cbSecurityDescriptor As Integer = 0
            ' size of security descriptor 
            Dim ftLastWriteTime As New FILETIME()
            ' last write time 
            Dim j As Integer
            Dim retValue As Integer

            ' Get the class name and the value count. 
            ' key handle 
            ' buffer for class name 
            ' length of class string 
            ' reserved 
            ' number of subkeys 
            ' longest subkey size 
            ' longest class string 
            ' number of values for this key 
            ' longest value name 
            ' longest value data 
            ' security descriptor 
            retValue = RegQueryInfoKeyA(hKey, Nothing, classLength, 0, cSubKeys, cbMaxSubKey, _
                cchMaxClass, cValues, cchMaxValue, cbMaxValueData, cbSecurityDescriptor, ftLastWriteTime)
            ' last write time 
            ' Enumerate the child keys, until RegEnumKeyEx fails.
            Dim achValueName As Byte() = New Byte(cchMaxValue) {}
            Dim achValueData As Byte() = New Byte(cbMaxValueData) {}

            Dim strDefaultName As String = IDS_DEFAULTVALUENAME
            ' (Default)
            Dim strDefaultValue As String = IDS_DEFAULTVALUEVALUE
            ' (value not set)
            ' Enumerate the key values. 
            If cValues <> 0 AndAlso cValues <> -1 AndAlso retValue = ERROR_SUCCESS Then
                j = 0
                retValue = ERROR_SUCCESS
                While j < cValues
                    Dim cchValueName As Integer = cchMaxValue + 1
                    Dim cchValueData As Integer = cbMaxValueData + 1
                    Dim dwType As Integer = 0

                    achValueName(0) = 0
                    achValueData(0) = 0

                    Dim sValueName As String = ""

                    retValue = RegEnumValueA(hKey, j, achValueName(0), cchValueName, 0, dwType, _
                        achValueData(0), cchValueData)

                    If retValue = ERROR_SUCCESS Then
                        Dim p As New keyValue()

                        If cchValueName = 0 AndAlso sValueName.Length = 0 Then
                            If cchValueData = 0 AndAlso achValueData(0) = 0 Then
                                p.setKeyValue(strDefaultName, strDefaultValue, dwType)
                            Else
                                p.setKeyValue(strDefaultName, convertToString(REG_SZ, achValueData, cchValueData), dwType)
                            End If
                        Else
                            Dim ascii As Encoding = Encoding.ASCII

                            ' Convert the new byte[] into a char[] and then into a string.
                            Dim asciiChars As Char() = New Char(ascii.GetCharCount(achValueName, 0, cchValueName) - 1) {}
                            ascii.GetChars(achValueName, 0, cchValueName, asciiChars, 0)
                            Dim asciiString As New String(asciiChars)

                            sValueName = asciiString

                            p.setKeyValue(sValueName, convertToString(dwType, achValueData, cchValueData), dwType)
                        End If

                        arrValues.Add(p)
                    End If
                    j += 1
                End While
            End If
            ' end if (cValues && cValues!=-1)
            ' now sort all this
            Dim nNbItems As Integer = arrValues.Count

            ' make sure we have at least a default value to play with
            Dim bFound As Boolean = False
            Dim i As Integer = 0

            While Not bFound AndAlso i < nNbItems
                Dim p As keyValue = DirectCast(arrValues(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)), keyValue)
                bFound = (p.getName().CompareTo(strDefaultName) = 0)
            End While

            If Not bFound Then
                ' this is a fake add, just to visually match what's shown in regedit.exe
                Dim p As New keyValue()
                p.setKeyValue(strDefaultName, strDefaultValue, REG_SZ)

                arrValues.Insert(0, p)
            End If

            Return arrValues
        End Function

        Private Function getEscapedXmlString(strInput As String) As String
            Dim s As String = ""
            If strInput.Length = 0 Then
                Return s
            End If

            Dim nLength As Integer = strInput.Length

            For i As Integer = 0 To nLength - 1
                Dim c As Char = strInput(i)

                If c = "&"c Then
                    s += "&amp;"
                ElseIf c = """"c Then
                    s += "&quot;"
                ElseIf c = "<"c Then
                    s += "&lt;"
                Else
                    s += c
                End If
            Next

            Return UTF8Conversion(s)
        End Function

        Private Function UTF8Conversion(s As String) As String
            ' local charset ==> UTF8
            ' Create two different encodings.
            Dim ascii As Encoding = Encoding.ASCII
            Dim utf8 As Encoding = Encoding.UTF8

            ' Convert the string into a byte[].
            Dim asciiBytes As Byte() = ascii.GetBytes(s)

            ' Perform the conversion from one encoding to the other.
            Dim utf8Bytes As Byte() = Encoding.Convert(ascii, utf8, asciiBytes)

            ' Convert the new byte[] into a char[] and then into a string.
            Dim utf8Chars As Char() = New Char(utf8.GetCharCount(utf8Bytes, 0, utf8Bytes.Length) - 1) {}
            utf8.GetChars(utf8Bytes, 0, utf8Bytes.Length, utf8Chars, 0)
            Dim utf8String As New String(utf8Chars)

            Return utf8String
        End Function

        Private Function getUnescapedXmlString(strInput As String) As String
            ' Xml escape chars (&quot; ==> ", &lt; ==> <, &amp; ==> &);
            Dim s As String = ""
            If strInput.Length = 0 Then
                Return s
            End If

            s = fromUTF8Conversion(strInput)

            Dim nLt As Integer
            While (InlineAssignHelper(nLt, s.IndexOf("&lt;", 0))) > -1
                s = s.Substring(0, nLt) & "<" & s.Substring(nLt + 4)
            End While

            Dim nQuot As Integer
            While (InlineAssignHelper(nQuot, s.IndexOf("&quot;", 0))) > -1
                s = s.Substring(0, nQuot) & """" & s.Substring(nQuot + 6)
            End While

            Dim nAmp As Integer
            While (InlineAssignHelper(nAmp, s.IndexOf("&amp;", 0))) > -1
                s = s.Substring(0, nAmp) & "&" & s.Substring(nAmp + 5)
            End While

            Return s
        End Function

        Private Function fromUTF8Conversion(s As String) As String
            ' UTF8 ==> local charset
            ' Create two different encodings.
            Dim ascii As Encoding = Encoding.ASCII
            Dim utf8 As Encoding = Encoding.UTF8

            ' Convert the string into a byte[].
            Dim utf8Bytes As Byte() = utf8.GetBytes(s)

            ' Perform the conversion from one encoding to the other.
            Dim asciiBytes As Byte() = Encoding.Convert(utf8, ascii, utf8Bytes)

            ' Convert the new byte[] into a char[] and then into a string.
            Dim asciiChars As Char() = New Char(ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length) - 1) {}
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0)
            Dim asciiString As New String(asciiChars)

            Return asciiString
        End Function

        Private Function convertToString(dwType As Integer, bRawBuffer As Byte(), nLen As Integer) As String
            Dim s As String = ""

            ' conversion from number to string
            If (dwType >= REG_BINARY AndAlso dwType <= REG_DWORD_BIG_ENDIAN) OrElse dwType = 11 OrElse dwType = REG_RESOURCE_LIST OrElse dwType = REG_RESOURCE_REQUIREMENTS_LIST Then
                Select Case dwType
                    Case REG_BINARY, REG_RESOURCE_LIST, REG_RESOURCE_REQUIREMENTS_LIST
                        If True Then
                            Dim [sByte] As String = ""
                            For i As Integer = 0 To nLen - 1
                                Dim c As Byte = bRawBuffer(i)
                                [sByte] = String.Format("{0:x2}", c)
                                If s.Length > 0 Then
                                    s += " "
                                End If
                                s += [sByte]
                            Next
                        End If
                        Exit Select
                    Case REG_DWORD
                        ' == REG_DWORD_LITTLE_ENDIAN
                        If True Then
                            Dim a As Byte = bRawBuffer(3)
                            Dim b As Byte = bRawBuffer(2)
                            Dim c As Byte = bRawBuffer(1)
                            Dim d As Byte = bRawBuffer(0)
                            s = String.Format("0x{0:x2}{1:x2}{2:x2}{3:x2}", a, b, c, d)
                            Dim n As UInteger = CUInt((a << 24) Or (b << 16) Or (c << 8) Or d)
                            Dim sDword As String = ""
                            sDword = String.Format(" {0}", n)
                            s += sDword
                        End If
                        Exit Select
                    Case REG_DWORD_BIG_ENDIAN
                        If True Then
                            Dim a As Byte = bRawBuffer(0)
                            Dim b As Byte = bRawBuffer(1)
                            Dim c As Byte = bRawBuffer(2)
                            Dim d As Byte = bRawBuffer(3)
                            s = String.Format("0x{0:x2}{1:x2}{2:x2}{3:x2}", a, b, c, d)
                            Dim n As UInteger = CUInt((a << 24) Or (b << 16) Or (c << 8) Or d)
                            Dim sDword As String
                            sDword = String.Format(" {0}", n)
                            s += sDword
                        End If
                        Exit Select
                    Case 11
                        ' QWORD, QWORD_LITTLE_ENDIAN (64-bit integer)
                        If True Then
                            Dim a As Byte = bRawBuffer(7)
                            Dim b As Byte = bRawBuffer(6)
                            Dim c As Byte = bRawBuffer(5)
                            Dim d As Byte = bRawBuffer(4)
                            Dim e As Byte = bRawBuffer(3)
                            Dim f As Byte = bRawBuffer(2)
                            Dim g As Byte = bRawBuffer(1)
                            Dim h As Byte = bRawBuffer(0)
                            s = String.Format("0x{0:x2}{1:x2}{2:x2}{3:x2}{4:x2}{5:x2}{6:x2}{7:x2}", a, b, c, d, e, _
                                f, g, h)
                        End If
                        Exit Select
                End Select
            Else
                If dwType = REG_LINK Then
                    ' convert the Unicode string to local charset string
                    ' Create two different encodings.
                    Dim ascii As Encoding = Encoding.ASCII
                    Dim unicode As Encoding = Encoding.Unicode

                    ' Perform the conversion from one encoding to the other.
                    Dim asciiBytes As Byte() = Encoding.Convert(unicode, ascii, bRawBuffer)

                    ' Convert the new byte[] into a char[] and then into a string.
                    Dim asciiChars As Char() = New Char(ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length) - 1) {}
                    ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0)
                    Dim asciiString As New String(asciiChars)

                    s = asciiString
                ElseIf dwType = REG_MULTI_SZ Then
                    ' a MULTI_str value is a set of strings separated by a 0 char, and
                    ' finishes with a double 0
                    For i As Integer = 0 To nLen - 3
                        ' nLen-1 instead of nLen, because we don't care the second 0 of the double 0
                        If bRawBuffer(i) = 0 Then
                            s += vbCr & vbLf
                        Else
                            s += ChrW(bRawBuffer(i))
                        End If
                    Next
                Else
                    Dim ascii As Encoding = Encoding.ASCII

                    ' Convert the new byte[] into a char[] and then into a string.
                    Dim asciiChars As Char() = New Char(ascii.GetCharCount(bRawBuffer, 0, nLen) - 1) {}
                    ascii.GetChars(bRawBuffer, 0, nLen, asciiChars, 0)
                    Dim asciiString As New String(asciiChars)

                    s = asciiString
                End If
            End If

            Return s
        End Function

        ' this function is the dual of convertToString
        Private Function convertFromString(strValue As String, dwType As Integer) As Byte()
            Dim nLen As Integer = 0
            Dim bRawBuffer As Byte() = Nothing

            ' real conversion starts here
            If dwType = REG_SZ OrElse dwType = REG_EXPAND_SZ Then
                Dim ascii As Encoding = Encoding.ASCII

                ' Convert the string into a byte[].
                bRawBuffer = ascii.GetBytes(strValue.TrimEnd() & " ")
                bRawBuffer(bRawBuffer.Length - 1) = 0
            ElseIf dwType = REG_BINARY OrElse dwType = REG_RESOURCE_LIST OrElse dwType = REG_RESOURCE_REQUIREMENTS_LIST Then
                If strValue.Length = 0 Then
                    ' stupid case handling
                    bRawBuffer = New Byte(0) {}
                    bRawBuffer(0) = 0
                Else
                    Dim n As Integer = (strValue.Length + 1) \ 3
                    Dim buffer As Byte() = New Byte(n - 1) {}
                    If buffer IsNot Nothing Then
                        nLen = n
                        Dim ncur As Integer = 0

                        For i As Integer = 0 To n - 1
                            buffer(i) = convertToDecimal(strValue(ncur), strValue(ncur + 1))
                            ncur += 3
                        Next

                        bRawBuffer = buffer
                    End If
                End If
            ElseIf dwType = REG_DWORD Then
                If strValue.Length = 0 Then
                    ' stupid case handling
                    Dim buffer As Byte() = New Byte(3) {}
                    If buffer IsNot Nothing Then
                        buffer(0) = 0
                        buffer(1) = 0
                        buffer(2) = 0
                        buffer(3) = 0
                        nLen = 4
                        bRawBuffer = buffer
                    End If
                Else
                    Dim n As Integer = 4
                    Dim buffer As Byte() = New Byte(n - 1) {}
                    If buffer IsNot Nothing Then
                        nLen = n
                        Dim ncur As Integer = 2
                        ' starts of offset 2 since we have 0x
                        For i As Integer = 0 To n - 1
                            buffer(n - 1 - i) = convertToDecimal(strValue(ncur), strValue(ncur + 1))
                            ncur += 2
                        Next

                        bRawBuffer = buffer
                    End If
                End If
            ElseIf dwType = 11 Then
                If strValue.Length = 0 Then
                    ' stupid case handling
                    Dim buffer As Byte() = New Byte(7) {}
                    If buffer IsNot Nothing Then
                        buffer(0) = 0
                        buffer(1) = 0
                        buffer(2) = 0
                        buffer(3) = 0
                        buffer(4) = 0
                        buffer(5) = 0
                        buffer(6) = 0
                        buffer(7) = 0
                        nLen = 8
                        bRawBuffer = buffer
                    End If
                Else
                    Dim n As Integer = 8
                    Dim buffer As Byte() = New Byte(n - 1) {}
                    If buffer IsNot Nothing Then
                        nLen = n
                        Dim ncur As Integer = 2
                        ' starts of offset 2 since we have 0x
                        For i As Integer = 0 To n - 1
                            buffer(n - 1 - i) = convertToDecimal(strValue(ncur), strValue(ncur + 1))
                            ncur += 2
                        Next

                        bRawBuffer = buffer
                    End If
                End If
            ElseIf dwType = REG_LINK Then
                ' convert the local charset string to Unicode string
                ' Create two different encodings.
                Dim ascii As Encoding = Encoding.ASCII
                Dim unicode As Encoding = Encoding.Unicode

                ' Perform the conversion from one encoding to the other.
                bRawBuffer = Encoding.Convert(ascii, unicode, bRawBuffer)
            ElseIf dwType = REG_MULTI_SZ Then
                ' strValue : the MULTI_str value is strings separated by \r\n (0x0A0x0D)

                ' buffer : a MULTI_str value is a set of strings separated by a 0 char, and
                ' finishes with a double 0

                ' strValue has a greater length than buffer since \r\n is 2 chars, while a 0-EOL is only one
                ' buf buffer has also a trailing EOL
                Dim buffer As Byte() = New Byte(strValue.Length + 1) {}
                If buffer IsNot Nothing Then
                    Dim n As Integer = 0

                    Dim nstrvaluelen As Integer = strValue.Length
                    For i As Integer = 0 To nstrvaluelen - 1
                        Dim c As Char = strValue(i)
                        If c <> ControlChars.Cr AndAlso c <> ControlChars.Lf Then
                            buffer(System.Math.Max(System.Threading.Interlocked.Increment(n), n - 1)) = CByte(AscW(c))
                        ElseIf c = ControlChars.Lf Then
                            buffer(System.Math.Max(System.Threading.Interlocked.Increment(n), n - 1)) = 0
                        End If
                    Next

                    buffer(System.Math.Max(System.Threading.Interlocked.Increment(n), n - 1)) = 0
                    buffer(System.Math.Max(System.Threading.Interlocked.Increment(n), n - 1)) = 0

                    If nstrvaluelen = 0 Then
                        n = 1
                    End If

                    nLen = n
                    ' -SD, may need to account for actual buffer length in nLen
                    bRawBuffer = buffer
                End If
            End If

            Return bRawBuffer
        End Function

        Private Function convertToDecimal(c1 As Char, c2 As Char) As Byte
            Dim n1 As Byte = 0, n2 As Byte = 0

            If c1 >= "0"c AndAlso c1 <= "9"c Then
                n1 = CByte(AscW(c1 - "0"c))
            ElseIf c1 >= "A"c AndAlso c1 <= "F"c Then
                n1 = CByte(c1 - "A"c + 10)
            ElseIf c1 >= "a"c AndAlso c1 <= "f"c Then
                n1 = CByte(c1 - "a"c + 10)
            End If

            If c2 >= "0"c AndAlso c2 <= "9"c Then
                n2 = CByte(AscW(c2 - "0"c))
            ElseIf c2 >= "A"c AndAlso c2 <= "F"c Then
                n2 = CByte(c2 - "A"c + 10)
            ElseIf c2 >= "a"c AndAlso c2 <= "f"c Then
                n2 = CByte(c2 - "a"c + 10)
            End If

            Return CByte(n1 * 16 + n2)
        End Function

        Private Function stringFromValueType(nType As Integer) As String
            Dim s As String = ""

            Select Case nType
                ' see winnt.h
                Case REG_BINARY
                    s = "REG_BINARY"
                    Exit Select
                Case REG_DWORD
                    s = "REG_DWORD"
                    Exit Select
                Case REG_DWORD_BIG_ENDIAN
                    s = "REG_DWORD"
                    Exit Select
                Case REG_EXPAND_SZ
                    s = "REG_EXPAND_SZ"
                    Exit Select
                Case REG_LINK
                    s = "REG_SZ"
                    Exit Select
                Case REG_MULTI_SZ
                    s = "REG_MULTI_SZ"
                    Exit Select
                Case REG_NONE
                    s = "REG_SZ"
                    Exit Select
                Case 11
                    s = "REG_QWORD"
                    Exit Select
                    ' QWORD (64-bit integer)
                Case REG_RESOURCE_LIST
                    s = "REG_RESOURCE_LIST"
                    Exit Select
                Case REG_RESOURCE_REQUIREMENTS_LIST
                    s = "REG_RESOURCE_REQUIREMENTS_LIST"
                    Exit Select
                Case REG_SZ
                    s = "REG_SZ"
                    Exit Select
            End Select

            Return s
        End Function

        ' this function is the dual of StringFromValueType
        Private Function typeFromString(strValueType As String) As Integer
            Dim nType As Integer = REG_SZ
            ' default type
            If strValueType.ToUpper().CompareTo("REG_BINARY") = 0 Then
                nType = REG_BINARY
            ElseIf strValueType.ToUpper().CompareTo("REG_DWORD") = 0 Then
                nType = REG_DWORD
            ElseIf strValueType.ToUpper().CompareTo("REG_SZ") = 0 Then
                nType = REG_SZ
            ElseIf strValueType.ToUpper().CompareTo("REG_EXPAND_SZ") = 0 Then
                nType = REG_EXPAND_SZ
            ElseIf strValueType.ToUpper().CompareTo("REG_MULTI_SZ") = 0 Then
                nType = REG_MULTI_SZ
            ElseIf strValueType.ToUpper().CompareTo("REG_QWORD") = 0 Then
                nType = 11
                ' QWORD (64-bit integer)
            ElseIf strValueType.ToUpper().CompareTo("REG_RESOURCE_LIST") = 0 Then
                nType = REG_RESOURCE_LIST
            ElseIf strValueType.ToUpper().CompareTo("REG_RESOURCE_REQUIREMENTS_LIST") = 0 Then
                nType = REG_RESOURCE_REQUIREMENTS_LIST
            End If

            Return nType
        End Function

        Public Function saveAsXml(w As xmlWriter, bFakedXml As Boolean, strInPath As String, strLimitValue As String) As Boolean
            Dim strPath As String = strInPath

            If strPath.Length = 0 Then
                Return False
            End If

            Dim strMainKeyname As String = strPath

            Dim nSlash As Integer = strPath.IndexOf("\")
            If nSlash > -1 Then
                strMainKeyname = strPath.Substring(0, nSlash)
                strPath = strPath.Substring(nSlash + 1)
            Else
                strPath = ""
            End If

            ' open the key now
            Dim reg As RegistryKey = openKey(strMainKeyname, strPath)

            If reg IsNot Nothing Then
                ' it's ok
                ' write the main key here
                Dim arKeyPath As New ArrayList()

                Dim nIndexSlash As Integer = 0, i As Integer
                Dim strTmpPath As String = strInPath

                Do
                    nIndexSlash = strTmpPath.IndexOf("\", 0)
                    If nIndexSlash > -1 Then
                        arKeyPath.Add(strTmpPath.Substring(0, System.Math.Max(System.Threading.Interlocked.Increment(nIndexSlash), nIndexSlash - 1)))
                        strTmpPath = strTmpPath.Substring(nIndexSlash)
                    Else
                        arKeyPath.Add(strTmpPath)
                    End If
                Loop While nIndexSlash > -1

                Dim nSize As Integer = arKeyPath.Count
                Dim bResult As Boolean = True

                If Not bFakedXml Then
                    For i = 0 To nSize - 1
                        Dim wkey As New xmlElement(XML_KEY)
                        wkey.addAttrib(getEscapedXmlString(XML_NAME), getEscapedXmlString(DirectCast(arKeyPath(i), String)))
                        wkey.write(w, 1, False, True)
                    Next

                    bResult = saveAsXml(w, bFakedXml, "", getRegistryHandle(reg), strLimitValue)

                    For i = 0 To nSize - 1
                        Dim wkey As New xmlElement(XML_KEY)
                        wkey.writeClosingTag(w, -1, False, True)
                    Next
                Else

                    nSize -= 1

                    For i = 0 To nSize - 1
                        Dim wkey As New xmlElement(DirectCast(arKeyPath(i), String))
                        wkey.write(w, 1, False, True)
                    Next

                    bResult = saveAsXml(w, bFakedXml, DirectCast(arKeyPath(nSize), String), getRegistryHandle(reg), strLimitValue)

                    For i = 0 To nSize - 1
                        Dim wkey As New xmlElement(DirectCast(arKeyPath(nSize - 1 - i), String))
                        wkey.writeClosingTag(w, -1, False, True)
                    Next
                End If

                reg.Close()

                Return bResult
            End If

            Return False
        End Function

        Private Function saveAsXml(w As xmlWriter, bFakedXml As Boolean, strKeyname As String, hKey As Integer, strLimitValue As String) As Boolean
            ' write key name
            Dim classLength As Integer = 0
            Dim cSubKeys As Integer = 0
            ' number of subkeys 
            Dim cbMaxSubKey As Integer = 0
            ' longest subkey size 
            Dim cchMaxClass As Integer = 0
            ' longest class string 
            Dim cValues As Integer = 0
            ' number of values for key 
            Dim cchMaxValue As Integer = 0
            ' longest value name 
            Dim cbMaxValueData As Integer = 0
            ' longest value data 
            Dim cbSecurityDescriptor As Integer = 0
            ' size of security descriptor 
            Dim ftLastWriteTime As New FILETIME()
            ' last write time 
            Dim i As Integer
            Dim retCode As Integer


            ' Get the class name and the value count. 
            ' key handle 
            ' buffer for class name 
            ' length of class string 
            ' reserved 
            ' number of subkeys 
            ' longest subkey size 
            ' longest class string 
            ' number of values for this key 
            ' longest value name 
            ' longest value data 
            ' security descriptor 
            retCode = RegQueryInfoKeyA(hKey, Nothing, classLength, 0, cSubKeys, cbMaxSubKey, _
                cchMaxClass, cValues, cchMaxValue, cbMaxValueData, cbSecurityDescriptor, ftLastWriteTime)
            ' last write time 

            If retCode <> ERROR_SUCCESS Then
                Return False
            End If

            ' Enumerate the child keys, until RegEnumKeyEx fails. 
            Dim achKey As Byte() = New Byte(cbMaxSubKey) {}

            Dim wkey As New xmlElement(XML_KEY)

            If strKeyname.Length > 0 Then
                If Not bFakedXml Then
                    ' standard xml
                    wkey.addAttrib(getEscapedXmlString(XML_NAME), getEscapedXmlString(strKeyname))

                    If cSubKeys > 0 OrElse cValues > 0 Then
                        wkey.write(w, 1, False, True)
                    Else
                        wkey.writeEmpty(w, 1, False, True)
                    End If
                Else
                    ' faked xml
                    wkey.setName(getEscapedXmlString(strKeyname))
                End If
            End If


            ' each 50 values, we pump a window message, to check out whether the user hit ESCAPE
            ' this could be done....
            If (System.Math.Max(System.Threading.Interlocked.Increment(_nSaveCounter), _nSaveCounter - 1) Mod 50) = 0 Then
            End If

            ' save values
            Dim arrValues As ArrayList = addRegistryValues(hKey)

            Dim nbItems As Integer = arrValues.Count
            For j As Integer = 0 To nbItems - 1
                Dim p As keyValue = DirectCast(arrValues(j), keyValue)

                If strLimitValue.Length <> 0 AndAlso p.getName().CompareTo(strLimitValue) <> 0 Then
                    Continue For
                End If

                If p.getName().CompareTo(IDS_DEFAULTVALUENAME) <> 0 OrElse p.getValue().CompareTo(IDS_DEFAULTVALUEVALUE) <> 0 Then
                    Dim dwType As Integer = p.[getType]()

                    If Not bFakedXml Then
                        ' standard xml
                        Dim wvalue As New xmlElement(XML_VALUE)

                        wvalue.addAttrib(XML_NAME, getEscapedXmlString(p.getName()))
                        wvalue.addAttrib(XML_VALUE2, getEscapedXmlString(p.getValue()))

                        If dwType <> REG_SZ AndAlso dwType <> REG_NONE Then
                            wvalue.addAttrib(XML_TYPE, getEscapedXmlString(stringFromValueType(dwType)))
                        End If

                        wvalue.writeEmpty(w, 1, False, True)
                    Else
                        ' faked xml
                        wkey.addAttrib(getEscapedXmlString(p.getName()), getEscapedXmlString(p.getValue()))

                    End If
                End If
            Next

            If strKeyname.Length <> 0 Then
                If bFakedXml Then
                    If cSubKeys > 0 Then
                        wkey.write(w, 1, False, True)
                    Else
                        wkey.writeEmpty(w, 1, False, True)
                    End If
                End If
            End If

            i = 0
            retCode = ERROR_SUCCESS
            While retCode = ERROR_SUCCESS
                Dim achKeyMaxLength As Integer = cbMaxSubKey + 1

                retCode = RegEnumKeyExA(hKey, i, achKey(0), achKeyMaxLength, 0, Nothing, _
                    cchMaxClass, ftLastWriteTime)

                If retCode = ERROR_SUCCESS AndAlso achKeyMaxLength > 0 Then
                    achKey(achKeyMaxLength) = 0
                    ' force EOL
                    Dim ascii As Encoding = Encoding.ASCII

                    ' Convert the new byte[] into a char[] and then into a string.
                    Dim asciiChars As Char() = New Char(ascii.GetCharCount(achKey, 0, achKeyMaxLength) - 1) {}
                    ascii.GetChars(achKey, 0, achKeyMaxLength, asciiChars, 0)
                    Dim sKeyName As New String(asciiChars)

                    ' open sub keys
                    Dim hSubkey As Integer = 0
                    If RegOpenKeyA(hKey, sKeyName, hSubkey) = ERROR_SUCCESS Then
                        If Not saveAsXml(w, bFakedXml, sKeyName, hSubkey, strLimitValue) Then
                            Return False
                        End If

                        RegCloseKey(hSubkey)
                    End If
                End If
                i += 1
            End While
            ' end for
            If strKeyname.Length <> 0 Then
                If Not bFakedXml Then
                    If cSubKeys > 0 OrElse cValues > 0 Then
                        wkey.writeClosingTag(w, -1, False, True)
                    End If
                Else
                    ' with faked xml, we only need to actually close the tag when there
                    ' are keys under it, otherwise, we did a WriteEmpty above.
                    If cSubKeys > 0 Then
                        wkey.writeClosingTag(w, -1, False, True)
                    End If
                End If
            End If

            Return True
        End Function

        Private Function LoadAsXml_OpenKey(arrInternalKeyLoads As ArrayList) As RegistryKey
            Dim reg As RegistryKey = Nothing

            If arrInternalKeyLoads.Count = 0 Then
                Return Nothing
            End If
            ' come on!
            ' build the real path from the array of keynames
            ' open the key now
            Dim strMainKey As String = "", strPath As String = ""
            Dim nSize As Integer = arrInternalKeyLoads.Count

            For i As Integer = 0 To nSize - 1
                Dim p As InternalKeyLoad = CType(arrInternalKeyLoads(i), InternalKeyLoad)
                If i = 0 Then
                    strMainKey = p.strKeyname
                Else
                    If strPath.Length > 0 Then
                        strPath += "\"
                    End If

                    strPath += p.strKeyname
                End If
            Next
            ' end for

            Try
                ' open the key now
                reg = openKey(strMainKey, strPath)

                If reg IsNot Nothing Then
                    ' it's ok
                    Return reg
                End If

                ' key does not exist, so let's create it !
                If strMainKey.ToUpper().CompareTo("HKEY_CLASSES_ROOT") = 0 Then
                    reg = Registry.ClassesRoot.CreateSubKey(strPath)
                ElseIf strMainKey.ToUpper().CompareTo("HKEY_CURRENT_USER") = 0 Then
                    reg = Registry.CurrentUser.CreateSubKey(strPath)
                ElseIf strMainKey.ToUpper().CompareTo("HKEY_LOCAL_MACHINE") = 0 Then
                    reg = Registry.LocalMachine.CreateSubKey(strPath)
                ElseIf strMainKey.ToUpper().CompareTo("HKEY_USERS") = 0 Then
                    reg = Registry.Users.CreateSubKey(strPath)
                ElseIf strMainKey.ToUpper().CompareTo("HKEY_CURRENT_CONFIG") = 0 Then
                    reg = Registry.CurrentConfig.CreateSubKey(strPath)
                Else
                    Return Nothing
                    ' break here
                End If
            Catch generatedExceptionName As Exception
            End Try

            If reg IsNot Nothing Then
                ' it's ok
                Return reg
            End If

            MessageBox.Show(String.Format("couldn't create key {0}\{1}", strMainKey, strPath), "Office Recovery Manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Return Nothing
        End Function

        Private Sub LoadAsXml_SetValue(hKey As Integer, strName As String, strValue As String, strType As String)
            If hKey = 0 Then
                Return
            End If

            ' handle the (Default) value
            strName = If(strName.ToUpper().CompareTo(IDS_DEFAULTVALUENAME) = 0, "", strName)

            Dim nType As Integer = typeFromString(strType)
            Dim buffer As Byte() = Nothing
            Dim hr As Integer = 0

            buffer = convertFromString(strValue, nType)

            If buffer IsNot Nothing AndAlso buffer.Length > 0 Then
                ' handle to key
                ' value name
                ' reserved
                ' value type
                ' value data
                ' size of value data
                hr = RegSetValueExA(hKey, strName, 0, nType, buffer(0), buffer.Length)

                If hr = ERROR_SUCCESS Then
                    ' it's ok
                    buffer = Nothing
                    Return
                End If
            End If

            MessageBox.Show(String.Format("couldn't set value {0}", strName), "Office Recovery Manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Sub

        Public Function loadAsXml(r As xmlReader, strFilename As String) As Boolean
            Dim bReturn As Boolean = True
            Dim arrInternalKeyLoads As New ArrayList()
            Dim strValueName As String = "", strValueValue As String = "", strValueType As String = ""

            r.showMsgBoxOnError(True)
            r.open(strFilename)

            While r.readString() AndAlso bReturn
                Select Case r.getNodeType()
                    Case NODETYPE.NODETYPE_BEGINELEMENT
                        If True Then
                            Dim strElement As String = r.getNodeName()
                            strElement = getUnescapedXmlString(strElement)
                        End If
                        Exit Select
                    Case NODETYPE.NODETYPE_ENDELEMENT
                        If True Then
                            Dim strElement As String = r.getNodeName()
                            strElement = getUnescapedXmlString(strElement)

                            ' close the latest key now
                            If strElement.CompareTo(XML_KEY) = 0 AndAlso arrInternalKeyLoads.Count > 0 Then
                                ' retrieve latest key
                                Dim p As InternalKeyLoad = CType(arrInternalKeyLoads(arrInternalKeyLoads.Count - 1), InternalKeyLoad)
                                p.reg.Close()
                                arrInternalKeyLoads.RemoveAt(arrInternalKeyLoads.Count - 1)
                            ElseIf strElement.CompareTo(XML_VALUE) = 0 AndAlso arrInternalKeyLoads.Count > 0 Then
                                ' <v .../>
                                ' now set/create the value
                                Dim p As InternalKeyLoad = CType(arrInternalKeyLoads(arrInternalKeyLoads.Count - 1), InternalKeyLoad)
                                LoadAsXml_SetValue(getRegistryHandle(p.reg), strValueName, strValueValue, strValueType)

                                strValueName = ""
                                strValueValue = ""
                                strValueType = ""
                            End If
                        End If
                        Exit Select
                    Case NODETYPE.NODETYPE_ATTRIB
                        If True Then
                            Dim strName As String, strValue As String

                            strName = r.getAttribName()
                            strValue = r.getAttribValue()

                            strName = getUnescapedXmlString(strName)
                            strValue = getUnescapedXmlString(strValue)

                            Dim strCurrentElement As String = r.getNodeName()

                            ' <k name="..."> ?
                            If strName.CompareTo(XML_NAME) = 0 AndAlso strCurrentElement.CompareTo(XML_KEY) = 0 Then
                                Dim p As New InternalKeyLoad()
                                p.strKeyname = strValue

                                arrInternalKeyLoads.Add(p)

                                ' open the key, and store the handle
                                p.reg = LoadAsXml_OpenKey(arrInternalKeyLoads)

                                arrInternalKeyLoads.RemoveAt(arrInternalKeyLoads.Count - 1)
                                arrInternalKeyLoads.Add(p)

                                If p.reg Is Nothing Then
                                    bReturn = False
                                    ' abort the process
                                End If
                            ElseIf strCurrentElement.CompareTo(XML_VALUE) = 0 Then
                                ' <v name="..." value="..." type="..." />
                                If strName.CompareTo(XML_NAME) = 0 Then
                                    ' name="..."
                                    strValueName = strValue
                                    strValueValue = ""
                                    strValueType = ""
                                ElseIf strName.CompareTo(XML_VALUE2) = 0 Then
                                    ' value="..."
                                    strValueValue = strValue
                                    strValueType = ""
                                ElseIf strName.CompareTo(XML_TYPE) = 0 Then
                                    ' type="..."
                                    strValueType = strValue
                                End If
                            End If
                        End If
                        Exit Select
                        ' other nodetypes : we don't care
                End Select
            End While
            ' end read lines one by one
            r.close()

            Return bReturn
        End Function
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace