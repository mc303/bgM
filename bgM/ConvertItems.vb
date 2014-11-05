Imports System.Text.RegularExpressions
Imports System.DirectoryServices.AccountManagement

Public Class ConvertItems
    Private Const _findHASH = "#(.*?)#"
    Private Const _findPERCENT = "%(.*?)%"
    '  ^\%.*\%$
    Public Shared Function itemToEnviromentVar(ByVal _env As String) As String
        Dim _found As Match = Regex.Match(_env, _findPERCENT)
        'Dim _txt_found As String = _found.ToString
        If _found.Success Then
            _env = _env.Replace(_found.Value, Environment.GetEnvironmentVariable(_found.Value.Replace("%", "")))
            Return _env
        End If
        _found = Regex.Match(_env, _findHASH)
        If _found.Success Then
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
                Return _env.Replace("#OSNAME#", My.Computer.Info.OSFullName)

            Case _env.Contains("#OSVERSION#")
                Return _env.Replace("#OSVERSION#", My.Computer.Info.OSVersion)

            Case _env.Contains("#DOMAIN#")
                Return _env.Replace("#DOMAIN#", Environment.UserDomainName())

                'Case _env.Contains("#STARTTIME#")
                '    Return Environment.TickCount.ToString

            Case _env.Contains("#DISPLAYNAMEN#")
                Return _env.Replace("#DISPLAYNAMEN#", UserPrincipal.Current.DisplayName)

            Case _env.Contains("#UPN#")
                Return _env.Replace("#UPN#", UserPrincipal.Current.UserPrincipalName)

            Case _env.Contains("#NIC")
                Dim _found As Match = Regex.Match(_env, _findHASH)
                If _found.Success Then
                    Try
                        Return _env.Replace(_found.Value, _networkinfo.Item(_found.Value))
                    Catch ex As Exception
                        Return ""
                    End Try
                End If

        End Select
        Return _env
    End Function

   
End Class
