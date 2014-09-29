Imports System.Text.RegularExpressions

Public Class _Convert

    Public Shared Function ToPointFromString(_loc As String) As Point
        If _loc IsNot Nothing Then
            Dim g = Regex.Replace(_loc, "[\{\}a-zA-Z=]", "").Split(","c)
            Dim pointResult As New Point(Integer.Parse(g(0)), Integer.Parse(g(1)))

            Return pointResult
        End If
    End Function

    Public Shared Function ToFontFromString(_font As String) As Font
        If _font IsNot Nothing Then
            ' [Font: Name=Manga SILCharis, Size=15.75, Units=3, GdiCharSet=0, GdiVerticalFont=False]
            Dim fontName As String
            Dim fontSize As String

            fontName = regexReplace(_font, "(.+Name=)(.+?)(, Size.+?])", "$2")
            fontSize = regexReplace(_font, "(.+Size=)(.+?)(, Units.+?])", "$2")
            Return New Font(fontName, fontSize)
        Else
            Return New Font("Arial", "10")
        End If

    End Function
    Private Shared Function regexReplace(ByVal sInput As String, ByVal sFind As String, ByVal sReplace As String)
        Try
            ' nul input allowed
            Dim expression As Regex
            expression = New Regex(sFind)
            Return expression.Replace(sInput, sReplace)
        Catch ex As Exception
            Return sInput
        End Try
    End Function


End Class
