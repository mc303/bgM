Imports System.IO
Imports System.Windows.Forms

Public Enum NODETYPE
    NODETYPE_NA = 0
    NODETYPE_BEGINELEMENT = 1
    NODETYPE_ENDELEMENT = 2
    NODETYPE_ATTRIB = 3
    NODETYPE_CONTENT = 4
    NODETYPE_PI = 5
    NODETYPE_COMMENT = 6
    NODETYPE_CDATA = 7
End Enum

Namespace Cll
    ''' <summary>
    ''' xmlReader reads and parses an XML file
    ''' </summary>
    Public Class xmlReader
        Public Const IDS_EMPTYELEMENTNAME As String = "Empty element name"
        Public Const IDS_BADBEGINNODESYMBOL As String = "Bad '<' symbol"
        Public Const IDS_NOBEGINNODESYMBOLINEOL As String = "'<' symbol not allowed at the end of a line"
        Public Const IDS_GENERICSYNTAXERROR As String = "Syntax error"
        Public Const IDS_MISSINGATTRIBNAME As String = "Missing attribute name before '='"
        Public Const IDS_MISSINGEQUALSYMBOL As String = "Missing '=' after attribute name"
        Public Const IDS_NOEQUALSYMBOLINEOL As String = "There should not be a '=' symbol at the end of a line"
        Public Const IDS_BADATTRIBUTEVALUESYNTAX As String = "There should not be a quote char at the end of a line"

        Private _sr As StreamReader
        ' general file members
        Private _bFileOpen As Boolean
        Private _strFilename As String
        Private _bShowMsgBoxOnError As Boolean
        ' true if message boxes are allowed to display while parsing
        Private _strLastError As String
        ' filled with error description if ParseContent() returns false
        '
        Private _strContent As String
        ' internal use : parser buffer
        Private _nCursor As Integer, _nbLines As Integer
        ' internal cursors (horizontal and vertical directions)
        Private _nCurNodeType As NODETYPE
        ' returns where is the parser on at the moment
        Private _strCurNodeName As String, _strCurNodeContent As String
        ' returns the current node value, and the current node content value
        Private _strCurAttribName As String, _strCurAttribValue As String
        ' returns the current attrib name/value pair
        Private _strCurPInstruction As String
        ' returns the current PInstruction (for instance ?xml, !DOCTYPE, ...)
        Private _bCurInsideComment As Boolean
        ' true if the parser is inside a comment ( <!-- ... -->)
        Private _bCurInsideCDATA As Boolean
        ' true if hte parser is inside a CDATA secrtion ( <![CDATA[[ ... ]]> )
        Private _bCurInsideNode As Boolean
        ' true if the parser is inside a node begin tag
        Private _bCurInsideAttrib As Boolean
        ' true if _strCurAttribName is valid and _strCurAttribValue is pending
        Private _bCurInsideContent As Boolean
        ' true if the parser is inside content
        Public Sub New()
            init()
        End Sub

        Private Sub init()
            _bFileOpen = False
            showMsgBoxOnError(False)
        End Sub

        Public Sub showMsgBoxOnError(bShow As Boolean)
            _bShowMsgBoxOnError = bShow
        End Sub

        Public Function open(strFilename As String) As Boolean
            If _bFileOpen Then
                Return True
            End If

            _strFilename = strFilename

            Return True
        End Function

        Public Function readString() As Boolean
            If Not _bFileOpen Then
                ' open file for reading
                Try
                    _sr = File.OpenText(_strFilename)
                    _bFileOpen = _sr IsNot Nothing
                    _nCursor = -1
                    _nbLines = 0
                    _bCurInsideNode = InlineAssignHelper(_bCurInsideComment, InlineAssignHelper(_bCurInsideAttrib, InlineAssignHelper(_bCurInsideContent, InlineAssignHelper(_bCurInsideCDATA, False))))
                    _strLastError = ""
                    _strCurNodeName = ""
                    _strCurNodeContent = ""
                    _strCurAttribName = ""
                    _strCurAttribValue = ""
                    _strCurPInstruction = ""
                Catch generatedExceptionName As Exception
                    _bFileOpen = False
                End Try
            End If

            If Not _bFileOpen Then
                Return False
            End If

            Dim bResult As Boolean = True

            If _nCursor = -1 Then
                _strContent = _sr.ReadLine()
                _nCursor = 0
                _nbLines += 1
            End If

            If _strContent Is Nothing Then
                bResult = False
            End If

            If _strContent IsNot Nothing AndAlso Not parseContent() Then
                Dim s As String
                s = "Parse error in line " & _nbLines & " : " & _strLastError
                _strLastError = s

                If _bShowMsgBoxOnError Then
                    MessageBox.Show(s, "Office Recovery Manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

                bResult = False
            End If

            Return bResult
        End Function

        Public Function close() As Boolean
            If _bFileOpen Then
                _sr.Close()
            End If

            init()

            Return True
        End Function

        Public Function getNodeType() As NODETYPE
            Return _nCurNodeType
        End Function

        Public Function getNodeName() As String
            Return _strCurNodeName
        End Function

        Public Function getAttribName() As String
            Return _strCurAttribName
        End Function

        Public Function getAttribValue() As String
            Return _strCurAttribValue
        End Function

        Private Function getNodeContent() As String
            Return _strCurNodeContent
        End Function


        Private Function getPInstruction() As String
            Return _strCurPInstruction
        End Function

        Public Function getCurrentLine() As Integer
            Return _nbLines
        End Function


        Public Function getLastError() As String
            ' if any
            Return _strLastError
        End Function

        Public Function parseContent() As Boolean
            _nCurNodeType = NODETYPE.NODETYPE_NA

            Dim i As Integer = CInt(_nCursor)
            Dim imax As Integer = _strContent.Length - 1

            If i > imax Then
                _nCursor = -1
                ' force next string to be read from file
                Return True
            End If

            Dim strTemp As String = _strContent & i

            ' pass spaces if we are inside a <...> and not yet processing an attribute value
            While (i <= imax) AndAlso (_bCurInsideNode AndAlso Not _bCurInsideAttrib) AndAlso (_strContent(i) = " "c OrElse _strContent(i) = &HA OrElse _strContent(i) = &HD)
                i += 1
            End While

            If i > imax Then
                _nCursor = -1
                ' force next string to be read from file
                Return True
            End If

            ' are we inside a comment ?
            If _bCurInsideComment Then
                While (i <= imax - 2) AndAlso Not (_strContent(i) = "-"c AndAlso _strContent(i + 1) = "-"c AndAlso _strContent(i + 2) = ">"c)
                    i += 1
                End While

                If i <= imax - 2 Then
                    ' found an end-comment
                    _nCurNodeType = NODETYPE.NODETYPE_NA
                    ' tell user we have nothing to provide him with
                    _nCursor = i + 2 + 1

                    ' after '-->' we are automatically within a content
                    _bCurInsideNode = InlineAssignHelper(_bCurInsideAttrib, InlineAssignHelper(_bCurInsideComment, InlineAssignHelper(_bCurInsideCDATA, False)))
                    _bCurInsideContent = True
                    _strCurNodeContent = ""

                    Return True
                Else
                    ' we still are inside an comment
                    _nCurNodeType = NODETYPE.NODETYPE_COMMENT
                    _nCursor = imax + 1
                    ' force next string to be read
                    Return True
                End If
            End If


            ' are we inside a CDATA section ?
            If _bCurInsideCDATA Then
                While (i <= imax - 2) AndAlso Not (_strContent(i) = "]"c AndAlso _strContent(i + 1) = "]"c AndAlso _strContent(i + 2) = ">"c)
                    i += 1
                End While

                If i <= imax - 2 Then
                    ' found an end-comment
                    _nCurNodeType = NODETYPE.NODETYPE_NA
                    ' tell user we have nothing to provide him with
                    _nCursor = i + 2 + 1

                    ' after ']]>' we are automatically within a content
                    _bCurInsideNode = InlineAssignHelper(_bCurInsideAttrib, InlineAssignHelper(_bCurInsideComment, InlineAssignHelper(_bCurInsideCDATA, False)))
                    _bCurInsideContent = True
                    _strCurNodeContent = ""

                    Return True
                Else
                    ' we still are inside an CDATA section
                    _nCurNodeType = NODETYPE.NODETYPE_CDATA
                    _nCursor = imax + 1
                    ' force next string to be read
                    Return True
                End If
            End If


            If _bCurInsideAttrib Then
                ' extracting the attrib value, possibly in multiple passes
                If _strCurAttribValue.Length = 0 Then
                    ' pass EOL
                    While (i <= imax) AndAlso (_strContent(i) = " "c OrElse _strContent(i) = &HA OrElse _strContent(i) = &HD)
                        i += 1
                    End While

                    If i > imax Then
                        _nCurNodeType = NODETYPE.NODETYPE_NA
                        _nCursor = i
                        Return True
                    End If

                    Dim quotechar As Char = _strContent(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1))

                    _strCurAttribValue += quotechar
                    ' start with something whatsoever!
                    ' in fact, we don't check the quotechar is an actual quotechar, ie " or '
                    _nCurNodeType = NODETYPE.NODETYPE_NA
                    _nCursor = i
                    Return True
                Else
                    Dim ibegin As Long = i

                    ' pass until we find spaces or EOL or >
                    While (i <= imax) AndAlso _strContent(i) <> """"c AndAlso _strContent(i) <> "'"c AndAlso _strContent(i) <> &HA AndAlso _strContent(i) <> &HD AndAlso _strContent(i) <> ">"c
                        i += 1
                    End While

                    ' TODO : properly manage the case of a multiple-line attrib-value
                    ' (we should in this case return a N/A nodetype as long as we haven't
                    ' encountered the ending quotechar, while buffering all the chars in
                    ' the strAttribValue member).

                    Dim iend As Long = i

                    _strCurAttribValue += _strContent.Substring(CInt(ibegin), CInt(iend - ibegin))

                    If i > imax Then
                        ' don't forget to add the EOL as well
                        _strCurAttribValue += vbCr & vbLf

                        _nCurNodeType = NODETYPE.NODETYPE_NA
                        _nCursor = i
                        Return True
                    End If

                    ' and remove the prefixed quote char
                    While _strCurAttribValue.Length <> 0 AndAlso (_strCurAttribValue(0) = """"c OrElse _strCurAttribValue(0) = "'"c)
                        _strCurAttribValue = _strCurAttribValue.Substring(1)
                    End While

                    _nCurNodeType = NODETYPE.NODETYPE_ATTRIB
                    _bCurInsideAttrib = False

                    If _strContent(i) <> ">"c Then
                        i += 1
                    End If
                    ' pass ending quote char
                    _nCursor = i
                    Return True

                End If
            End If
            ' end if _bCurInsideAttrib==true

            If _bCurInsideContent Then
                Dim ibegin As Long = i

                ' pass until we find spaces or EOL or >
                While (i <= imax) AndAlso _strContent(i) <> &HA AndAlso _strContent(i) <> &HD AndAlso _strContent(i) <> "<"c
                    i += 1
                End While

                Dim iend As Long = i

                If (i <= imax) AndAlso _strContent(i) = "<"c Then
                    _bCurInsideContent = False
                End If

                _strCurNodeContent = _strContent.Substring(CInt(ibegin), CInt(iend - ibegin))
                If _strCurNodeContent.Length = 0 Then
                    _nCurNodeType = NODETYPE.NODETYPE_NA
                Else
                    _nCurNodeType = NODETYPE.NODETYPE_CONTENT
                End If

                _nCursor = i
                Return True
            End If
            ' end if (_bCurInsideContent)
            '
            Dim c As Char = _strContent(i)

            ' a node ?
            If c = "<"c Then
                If _bCurInsideNode Then
                    ' error, we were already inside one
                    _strLastError = IDS_BADBEGINNODESYMBOL
                    Return False
                End If

                _bCurInsideNode = True
                _bCurInsideAttrib = InlineAssignHelper(_bCurInsideContent, InlineAssignHelper(_bCurInsideComment, InlineAssignHelper(_bCurInsideCDATA, False)))

                i += 1

                ' pass spaces
                While _strContent(i) = " "c OrElse _strContent(i) = &HA OrElse _strContent(i) = &HD
                    i += 1
                End While

                If i > imax Then
                    _strLastError = IDS_NOBEGINNODESYMBOLINEOL
                    Return False
                End If

                ' here we have either a node name, a PI, or a begin comment
                If imax - i >= 2 Then
                    ' is it a begin comment ? ( <!-- )
                    If _strContent(i + 0) = "!"c AndAlso _strContent(i + 1) = "-"c AndAlso _strContent(i + 2) = "-"c Then
                        _nCurNodeType = NODETYPE.NODETYPE_COMMENT
                        _bCurInsideComment = True

                        i += 3
                        ' go to actual comment content
                        _nCursor = i
                        Return True
                    End If
                End If

                If imax - i >= 7 Then
                    ' is it a begin cdatasection ? ( <![CDATA[ )
                    If _strContent(i + 0) = "!"c AndAlso _strContent(i + 1) = "["c AndAlso _strContent(i + 2) = "C"c AndAlso _strContent(i + 3) = "D"c AndAlso _strContent(i + 4) = "A"c AndAlso _strContent(i + 5) = "T"c AndAlso _strContent(i + 6) = "A"c AndAlso _strContent(i + 7) = "["c Then
                        _nCurNodeType = NODETYPE.NODETYPE_CDATA
                        _bCurInsideCDATA = True

                        i += 8
                        ' go to actual cdata section content
                        _nCursor = i
                        Return True
                    End If
                End If


                ' the node name begins at position i
                Dim ibegin As Long = i

                ' pass until we find spaces or EOL or >
                ' don't forget empty elements (for instance <br/>)
                While (i <= imax) AndAlso _strContent(i) <> " "c AndAlso _strContent(i) <> &HA AndAlso _strContent(i) <> &HD AndAlso (_strContent(i) <> "/"c OrElse (i = ibegin)) AndAlso _strContent(i) <> ">"c
                    i += 1
                End While

                Dim iend As Long = i

                _strCurNodeName = _strContent.Substring(CInt(ibegin), CInt(iend - ibegin))
                If _strCurNodeName.Length = 0 Then
                    _strLastError = IDS_EMPTYELEMENTNAME
                    Return False
                End If


                If _strCurNodeName(0) = "?"c OrElse _strCurNodeName(0) = "!"c Then
                    _nCurNodeType = NODETYPE.NODETYPE_PI
                    _strCurPInstruction = _strCurNodeName
                    ' erase the PI instruction so it does not appear as a node name
                    _strCurNodeName = ""
                ElseIf _strCurNodeName(0) = "/"c Then
                    _nCurNodeType = NODETYPE.NODETYPE_ENDELEMENT
                    ' remove /
                    _strCurNodeName = _strCurNodeName.Substring(1)
                Else
                    _nCurNodeType = NODETYPE.NODETYPE_BEGINELEMENT
                End If

                _nCursor = i
                Return True
            Else
                ' >, or ?, or content or attribute
                If c = "?"c Then
                    _nCurNodeType = NODETYPE.NODETYPE_NA
                    _nCursor = i + 1
                    Return True
                ElseIf c = "/"c Then
                    i += 1

                    ' pass node name
                    Dim ibegin As Long = i

                    ' pass until we find spaces or EOL or >
                    While (i <= imax) AndAlso _strContent(i) <> " "c AndAlso _strContent(i) <> &HA AndAlso _strContent(i) <> &HD AndAlso _strContent(i) <> ">"c
                        i += 1
                    End While

                    Dim iend As Long = i

                    _nCurNodeType = NODETYPE.NODETYPE_ENDELEMENT

                    _nCursor = i
                    Return True
                ElseIf c = ">"c Then
                    _bCurInsideNode = InlineAssignHelper(_bCurInsideAttrib, False)
                    _bCurInsideContent = True
                    _strCurNodeContent = ""
                    _nCurNodeType = NODETYPE.NODETYPE_NA
                    _nCursor = i + 1
                    Return True
                End If

                If _bCurInsideNode Then
                    ' attributes

                    If Not _bCurInsideAttrib Then
                        If c = "="c Then
                            _nCurNodeType = NODETYPE.NODETYPE_NA
                            _bCurInsideAttrib = True
                            ' enable extraction of the associated attribute value
                            i += 1
                            ' pass '=' symbol
                            _nCursor = i
                            Return True
                        End If

                        ' get attribute name
                        Dim ibegin As Long = i

                        ' pass until we find spaces or EOL or >
                        While (i <= imax) AndAlso _strContent(i) <> " "c AndAlso _strContent(i) <> &HA AndAlso _strContent(i) <> &HD AndAlso _strContent(i) <> "="c AndAlso _strContent(i) <> ">"c
                            ' check against > is just for safety
                            i += 1
                        End While

                        Dim iend As Long = i

                        _strCurAttribName = _strContent.Substring(CInt(ibegin), CInt(iend - ibegin))
                        If _strCurAttribName.Length = 0 Then
                            _strLastError = IDS_MISSINGATTRIBNAME
                            Return False
                        End If

                        _strCurAttribValue = ""
                        ' make sure the attrib value is empty for the moment
                        _nCurNodeType = NODETYPE.NODETYPE_NA

                        _nCursor = i
                        Return True
                    End If
                End If
            End If

            ' this code never executes
            _strLastError = IDS_GENERICSYNTAXERROR
            Return False
        End Function
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace