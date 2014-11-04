Imports Microsoft.Win32
Imports System.Drawing

Public Class _reg
    Public Shared Sub createRootKey()
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.CreateSubKey("Software\bgM", RegistryKeyPermissionCheck.Default)
        _regKey = Nothing
    End Sub

    Public Shared Sub setSourceWallpaper(_bg As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("SourceWallpaper", _bg, RegistryValueKind.String)
        _regKey = Nothing
    End Sub

    Public Shared Function getSourceWallpaper() As String
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("SourceWallpaper", "").ToString

        _regKey = Nothing
    End Function

    Public Shared Sub setWallpaper(_bg As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("Wallpaper", _bg, RegistryValueKind.String)
        _regKey = Nothing
    End Sub

    Public Shared Function getWallpaper() As String
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("Wallpaper", "").ToString

        _regKey = Nothing
    End Function

    Public Shared Function getCoordinatesMode() As String
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("CoordinatesMode", "Location").ToString

        _regKey = Nothing
    End Function

    Public Shared Sub setCoordinatesMode(_cm As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("CoordinatesMode", _cm, RegistryValueKind.String)

        _regKey = Nothing
    End Sub

    Public Shared Sub setRegFont(_font As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("Font", _font, RegistryValueKind.String)

        _regKey = Nothing
    End Sub

    Public Shared Function getRegFont() As String
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("Font")

        _regKey = Nothing
    End Function

    Public Shared Function getInputFields() As Integer
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", False)
        Return _regKey.GetValue("InputFields")

        _regKey = Nothing
    End Function

    Public Shared Sub setInputFields(_n As Integer)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("InputFields", _n, RegistryValueKind.DWord)

        _regKey = Nothing
    End Sub

    Public Shared Function getItemText(_n As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("text" & _n)

        _regKey = Nothing
    End Function

    Public Shared Sub setItemText(_n As String, _txt As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("text" & _n, _txt, RegistryValueKind.String)
        _regKey = Nothing
    End Sub


    Public Shared Sub delItemText(_n As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.DeleteValue("text" & _n, False)
        _regKey = Nothing
    End Sub

    Public Shared Function getItemFont(_n As String) As Font
        Dim _font As Font
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Dim _fontstyle As FontStyle = _regKey.GetValue("text" & _n & "FontStyle", 0)
        _font = New Font(_regKey.GetValue("text" & _n & "FontFamily", "Arial").ToString, Convert.ToSingle(_regKey.GetValue("text" & _n & "FontSize", "16")), _fontstyle)
        Return _font

        _regKey = Nothing
    End Function

    Public Shared Sub setItemFont(_n As String, _font As Font)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("text" & _n & "FontStyle", _font.Style, RegistryValueKind.DWord)
        _regKey.SetValue("text" & _n & "FontFamily", _font.FontFamily.Name, RegistryValueKind.String)
        _regKey.SetValue("text" & _n & "FontSize", _font.Size, RegistryValueKind.DWord)
        _regKey = Nothing
    End Sub

    Public Shared Sub delItemFont(_n As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.DeleteValue("text" & _n & "FontStyle", False)
        _regKey.DeleteValue("text" & _n & "FontFamily", False)
        _regKey.DeleteValue("text" & _n & "FontSize", False)
        _regKey = Nothing
    End Sub

    Public Shared Function getItemLocation(_n As String) As String
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("text" & _n & "Location")

        _regKey = Nothing
    End Function

    Public Shared Sub setItemLocation(_n As String, _txtLocation As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("text" & _n & "Location", _txtLocation, RegistryValueKind.String)
        _regKey = Nothing
    End Sub

    Public Shared Sub delItemLocation(_n As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.DeleteValue("text" & _n & "Location", False)
        _regKey = Nothing
    End Sub

    Public Shared Function getItemLocationInvert(_n As String) As String
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("text" & _n & "LocationInvert")

        _regKey = Nothing
    End Function

    Public Shared Sub setItemLocationInvert(_n As String, _txtLocation As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("text" & _n & "LocationInvert", _txtLocation, RegistryValueKind.String)
        _regKey = Nothing
    End Sub

    Public Shared Sub delItemLocationInvert(_n As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.DeleteValue("text" & _n & "LocationInvert", False)
        _regKey = Nothing
    End Sub

    Public Shared Function getItemLocationPercent(_n As String) As String
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("text" & _n & "LocationPercent")

        _regKey = Nothing
    End Function

    Public Shared Sub setItemLocationPercent(_n As String, _txtLocation As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("text" & _n & "LocationPercent", _txtLocation, RegistryValueKind.String)
        _regKey = Nothing
    End Sub

    Public Shared Sub delItemLocationPercent(_n As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.DeleteValue("text" & _n & "LocationPercent", False)
        _regKey = Nothing
    End Sub

    Public Shared Function getItemColor(_n As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("text" & _n & "Color")

        _regKey = Nothing
    End Function

    Public Shared Sub setItemColor(_n As String, _intColor As Integer)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("text" & _n & "Color", _intColor, RegistryValueKind.DWord)
        _regKey = Nothing
    End Sub

    Public Shared Sub delItemColor(_n As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.DeleteValue("text" & _n & "Color", False)
        _regKey = Nothing
    End Sub

    Public Shared Function getItemWidth(_n As String) As Integer
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("text" & _n & "Width")

        _regKey = Nothing
    End Function

    Public Shared Sub setItemWidth(_n As String, _intWidth As Integer)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("text" & _n & "Width", _intWidth, RegistryValueKind.DWord)
        _regKey = Nothing
    End Sub

    Public Shared Sub delItemWidth(_n As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.DeleteValue("text" & _n & "Width", False)
        _regKey = Nothing
    End Sub

    Public Shared Function getItemAlign(_n As String) As Integer
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("text" & _n & "Align", 0)

        _regKey = Nothing
    End Function

    Public Shared Sub setItemAlign(_n As String, intAlign As Integer)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("text" & _n & "Align", intAlign, RegistryValueKind.DWord)
        _regKey = Nothing
    End Sub

    Public Shared Sub delItemAlign(_n As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.DeleteValue("text" & _n & "Align", False)
        _regKey = Nothing
    End Sub

    Public Shared Function getFontFamily() As String
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", False)
        Return _regKey.GetValue("FontFamily", "Arial")

        _regKey = Nothing
    End Function

    Public Shared Sub setFontFamily(ByVal _font As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("FontFamily", _font, RegistryValueKind.String)
        _regKey = Nothing
    End Sub

    Public Shared Function getFontSize() As String
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("FontSize", "10")

        _regKey = Nothing
    End Function

    Public Shared Sub setFontSize(ByVal _size As String)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("FontSize", _size, RegistryValueKind.String)
        _regKey = Nothing
    End Sub

    Public Shared Function getFontStyle() As FontStyle
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("FontStyle", FontStyle.Regular)

        _regKey = Nothing
    End Function

    Public Shared Sub setFontStyle(ByVal _style As Integer)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("FontStyle", _style, RegistryValueKind.DWord)
        _regKey = Nothing
    End Sub

    Public Shared Function getFontColor() As Integer
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("FontColor", 0)

        _regKey = Nothing
    End Function

    Public Shared Sub setFontColor(ByVal _color As Integer)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("FontColor", _color, RegistryValueKind.DWord)
        _regKey = Nothing
    End Sub

    Public Shared Function getWait() As Integer
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        Return _regKey.GetValue("Wait", 0)

        _regKey = Nothing
    End Function

    Public Shared Sub setWait(ByVal _wait As Integer)
        Dim _regKey As RegistryKey
        _regKey = Registry.CurrentUser.OpenSubKey("Software\bgM", True)
        _regKey.SetValue("Wait", _wait, RegistryValueKind.DWord)
        _regKey = Nothing
    End Sub
End Class