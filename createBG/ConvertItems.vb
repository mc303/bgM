Imports System.Text.RegularExpressions
Imports System.DirectoryServices.AccountManagement

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
        'Dim __env As String = _env.Replace("#", "")
        'System.DirectoryServices.AccountManagement.UserPrincipal.FindByIdentity()

        Select Case True
            Case _env.Contains("#OSNAME#")
                Return My.Computer.Info.OSFullName

            Case _env.Contains("#OSVERSION#")
                Return My.Computer.Info.OSVersion

            Case _env.Contains("#DOMAIN#")
                Return Environment.UserDomainName()

            Case _env.Contains("#STARTTIME#")
                Return Environment.TickCount.ToString

            Case _env.Contains("#DISPLAYNAMEN#")
                Return UserPrincipal.Current.DisplayName

            Case _env.Contains("#UPN#")
                Return UserPrincipal.Current.UserPrincipalName

            Case _env.Contains("#NIC")
                Try
                    Return _networkinfo.Item(_env)
                Catch ex As Exception
                    Return ""
                End Try
        End Select
        Return _env
    End Function

   
End Class
