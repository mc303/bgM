Imports System.Text.RegularExpressions
Imports System.Net

Public Class ConvertItems

    '  ^\%.*\%$
    Public Shared Function itemToEnviromentVar(ByVal _env As String) As String
        Dim expression As Regex
        expression = New Regex("^\%.*\%$")
        If expression.Match(_env).Success Then
            Return Environment.GetEnvironmentVariable(_env.Replace("%", ""))
        End If
        expression = New Regex("^\#.*\#$")
        If expression.Match(_env).Success Then
            Return hashToValue(_env)
        End If

        Return _env
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

    Private Shared Function hashToValue(ByVal _env As String) As String
        _env = _env.Replace("#", "")

        Select Case _env
            Case "OSNAME"
                Return My.Computer.Info.OSFullName

            Case "OSVERSION"
                Return My.Computer.Info.OSVersion

            Case "IP0"
                Try
                    Return GetHostEntryIPv4.AddressList(0).ToString
                Catch ex As Exception
                    Return "0.0.0.0"
                End Try

            Case "IP1"
                Try
                    Return GetHostEntryIPv4.AddressList(1).ToString
                Catch ex As Exception
                    Return "0.0.0.0"
                End Try

            Case "IP2"
                Try
                    Return GetHostEntryIPv4.AddressList(2).ToString
                Catch ex As Exception
                    Return "0.0.0.0"
                End Try

            Case "IP3"
                Try
                    Return GetHostEntryIPv4.AddressList(3).ToString
                Catch ex As Exception
                    Return "0.0.0.0"
                End Try
        End Select
        Return _env
    End Function
End Class
