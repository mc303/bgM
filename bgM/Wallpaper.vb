Public Class Wallpaper
    Private Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Integer

    Private Const SETDESKWALLPAPER = 20
    Private Const UPDATEINIFILE = &H1

    Public Shared Sub Apply(_bg As String)
        SystemParametersInfo(SETDESKWALLPAPER, 0, _bg, UPDATEINIFILE)
    End Sub

End Class
