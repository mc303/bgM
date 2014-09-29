Imports System.Text.RegularExpressions


Public Class ConvertItems

    '  ^\%.*\%$
    Public Shared Function itemToEnviromentVar(ByVal _env As String) As String
        Dim expression As Regex
        expression = New Regex("^\%.*\%$")
        If expression.Match(_env).Success Then
            Return Environment.GetEnvironmentVariable(_env.Replace("%", ""))
        Else
            Return _env
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
