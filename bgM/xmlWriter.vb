Imports System.IO
Imports System.Windows.Forms

Namespace Cll
    ''' <summary>
    ''' Summary description for xmlWriter.
    ''' </summary>
    Public Class xmlWriter
        Private _sw As StreamWriter = Nothing
        Private _bFileOpen As Boolean = False, _bPrototypeWritten As Boolean
        Private _strFilename As String
        Private _nLevel As Integer

        Public Sub New()
            init()
        End Sub

        Public Sub setIndentLevel(n As Integer)
            _nLevel = n
        End Sub

        Public Function getIndentLevel() As Integer
            Return _nLevel
        End Function

        Public Function getFilename() As String
            Return _strFilename
        End Function

        Public Function mustBeClosed() As Boolean
            Return _bFileOpen
        End Function

        Private Sub init()
            setIndentLevel(0)
            _bFileOpen = False
            _bPrototypeWritten = False
        End Sub

        Public Function open(strFilename As String) As Boolean
            If _bFileOpen Then
                Return True
            End If

            _strFilename = strFilename

            Return True
        End Function

        Public Function writeString(strData As String) As Boolean
            If Not _bFileOpen Then
                ' open file
                Try
                    _sw = File.CreateText(_strFilename)
                    _bFileOpen = True
                Catch e As Exception
                    MessageBox.Show(e.ToString(), "Office Recovery Manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    _bFileOpen = False
                End Try
            End If

            If Not _bFileOpen Then
                Return False
            End If

            If Not _bPrototypeWritten Then
                _bPrototypeWritten = True
                ' make sure to set this flag to true before we call a reentrant method such like .WritePInstruction
                ' write XML prototype, once and only once
                '
                Dim nOldIndent As Integer = getIndentLevel()
                setIndentLevel(0)

                Dim xml As New xmlElement("xml")
                xml.addAttrib("version", "1.0")
                xml.addAttrib("encoding", "UTF-8")
                xml.writePInstruction(Me, 0)

                setIndentLevel(nOldIndent)
            End If

            ' actual write
            _sw.Write(strData)

            Return True
        End Function

        Public Function close() As Boolean
            If _sw IsNot Nothing AndAlso _bFileOpen Then
                _sw.Close()
            End If

            init()

            Return True
        End Function
    End Class
End Namespace