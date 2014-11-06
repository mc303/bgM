Imports System.Collections

Namespace Cll
    '''	<summary>
    '''	xmlElement is used by xmlReader and xmlWriter for processing.
    '''	</summary>
    Public Class xmlElement
        Public _strName As String
        Private _arrayAttribNames As ArrayList, _arrayAttribValues As ArrayList

        Public Sub New()
            _arrayAttribNames = New ArrayList()
            _arrayAttribValues = New ArrayList()
        End Sub

        Public Sub New(strName As String)
            _arrayAttribNames = New ArrayList()
            _arrayAttribValues = New ArrayList()

            setName(strName)
        End Sub

        Private Function helperBuildIndent(nLevel As Integer) As String
            Dim strSpaces As String = ""

            For i As Integer = 0 To nLevel - 1
                strSpaces += " "
            Next

            Return strSpaces
        End Function

        Public Sub setName(strName As String)
            _strName = strName
        End Sub

        Private Function getName() As String
            Return _strName
        End Function

        Private Function getAttribName(nIndex As Integer) As String
            Dim strAttribName As String = ""
            If nIndex > -1 AndAlso nIndex < getAttribCount() Then
                strAttribName = DirectCast(_arrayAttribNames(nIndex), String)
            End If

            Return strAttribName
        End Function

        Private Function getAttribValue(nIndex As Integer) As String
            Dim strAttribValue As String = ""
            If nIndex > -1 AndAlso nIndex < _arrayAttribValues.Count Then
                strAttribValue = DirectCast(_arrayAttribValues(nIndex), String)
            End If

            Return strAttribValue
        End Function

        Private Function getAttribCount() As Integer
            Return CInt(_arrayAttribNames.Count)
        End Function

        Private Function findAttrib(strAttribName As String) As Boolean
            Dim bFound As Boolean = False
            Dim i As Integer = 0
            Dim nSize As Integer = CInt(_arrayAttribNames.Count)

            While i < nSize AndAlso Not bFound
                bFound = (DirectCast(_arrayAttribNames(i), String) = strAttribName)
                i += 1
            End While

            Return bFound
        End Function

        Public Sub addAttrib(strAttribName As String, strAttribValue As String)
            Dim bFound As Boolean = False
            Dim i As Integer = 0
            Dim nSize As Integer = CInt(_arrayAttribNames.Count)

            While i < nSize AndAlso Not bFound
                bFound = (DirectCast(_arrayAttribNames(i), String) = strAttribName)
                i += 1
            End While

            If bFound Then
                ' already known
                i -= 1
                _arrayAttribValues(i) = strAttribValue
            Else
                _arrayAttribNames.Add(strAttribName)
                _arrayAttribValues.Add(strAttribValue)
            End If
        End Sub

        Public Sub write(writer As xmlWriter, nDeltaLevel As Integer, bIndent As Boolean, bEOL As Boolean)
            ' for any kind	of open	tag
            writer.setIndentLevel(writer.getIndentLevel() + nDeltaLevel)

            Dim s As String = ""
            If bIndent Then
                s = helperBuildIndent(writer.getIndentLevel())
            End If

            s += "<"
            s += _strName
            Dim i As Integer
            Dim nCount As Integer = getAttribCount()

            For i = 0 To nCount - 1
                s += " "
                ' separator between attribute pairs
                s += _arrayAttribNames(i)
                s += "="""
                s += _arrayAttribValues(i)
                s += """"
            Next

            s += ">"
            If bEOL Then
                s += vbCr & vbLf
            End If
            '	ENDL
            writer.writeString(s)
        End Sub

        Public Sub writeEmpty(writer As xmlWriter, nDeltaLevel As Integer, bIndent As Boolean, bEOL As Boolean)
            writer.setIndentLevel(writer.getIndentLevel() + nDeltaLevel)

            Dim s As String = ""
            If bIndent Then
                helperBuildIndent(writer.getIndentLevel())
            End If

            s += "<"
            s += _strName
            Dim i As Integer
            Dim nCount As Integer = getAttribCount()

            For i = 0 To nCount - 1
                s += " "
                ' separator between attribute pairs
                s += _arrayAttribNames(i)
                s += "="""
                s += _arrayAttribValues(i)
                s += """"
            Next

            s += "></"
            s += _strName
            s += ">"

            If bEOL Then
                s += vbCr & vbLf
            End If
            '	ENDL
            writer.writeString(s)

            writer.setIndentLevel(writer.getIndentLevel() - nDeltaLevel)
        End Sub

        Public Sub writePInstruction(writer As xmlWriter, nDeltaLevel As Integer)
            writer.setIndentLevel(writer.getIndentLevel() + nDeltaLevel)

            Dim s As String = ""

            s = helperBuildIndent(writer.getIndentLevel())

            s += "<?"
            s += _strName

            Dim i As Integer
            Dim nCount As Integer = getAttribCount()

            For i = 0 To nCount - 1
                s += " "
                ' separator between attribute pairs
                s += _arrayAttribNames(i)
                s += "="""
                s += _arrayAttribValues(i)
                s += """"
            Next

            s += "?>"
            s += vbCr & vbLf
            '	ENDL
            writer.writeString(s)
        End Sub

        Public Sub writeClosingTag(writer As xmlWriter, nDeltaLevel As Integer, bIndent As Boolean, bEOL As Boolean)
            Dim s As String = ""

            If bIndent Then
                s = helperBuildIndent(writer.getIndentLevel())
            End If

            s += "</"
            s += _strName
            s += ">"
            If bEOL Then
                s += vbCr & vbLf
            End If
            '	ENDL
            writer.writeString(s)

            writer.setIndentLevel(writer.getIndentLevel() + nDeltaLevel)
        End Sub
    End Class
End Namespace