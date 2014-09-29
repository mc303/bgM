﻿Imports Microsoft.Win32
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.Timers

Module modMain
    Const _locCorrect = 23
    Private _timer As System.Timers.Timer
    Public screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Public screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

    Sub Main()
        If _reg.getWait = 0 Then
            Call tick()
        Else
            _timer = New System.Timers.Timer
            _timer.Interval = _reg.getWait * 1000
            '  _timer.Enabled = True
            _timer.AutoReset = False
            ' _timer.Stop()
            _timer.Start()
            AddHandler _timer.Elapsed, AddressOf tick

            While _timer.Enabled = True

            End While
        End If
    End Sub

    Private Sub tick()
        Call createPreviewFromBackground()
        Call Wallpaper.Apply(_reg.getWallpaper)
        If Not _reg.getWait = 0 Then
            _timer.Stop()
        End If
        Application.Exit()
    End Sub

    Sub createPreviewFromBackground()
        Dim _txt As String
        Dim _screenPos As Point
        Dim _imgToMemoryStream As New MemoryStream()
        Dim _color As Brush
        Dim _envText As String
        Dim stringFormat As New StringFormat()
        Dim _inputFields As Integer = _reg.getInputFields()
        Dim _wallpaper As String = _reg.getRegSourceWallpaper
        'Load the Image to be written on.
        Dim bitMapImage As Bitmap = Bitmap.FromFile(_wallpaper)
        'Dim bitMapImage As Bitmap = Image.FromFile(_bg)
        Dim graphicImage As Graphics = Graphics.FromImage(bitMapImage)

        'Smooth graphics is nice.
        'graphicImage.SmoothingMode = SmoothingMode.AntiAlias
        graphicImage.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        graphicImage.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
        graphicImage.CompositingQuality = Drawing2D.CompositingQuality.HighQuality


        If Not _inputFields = 0 Then
            For i As Integer = 0 To _inputFields - 1
                _txt = _reg.getItemText(i.ToString("D2"))
                '_screenPos = _txt.PointToScreen(New Point(-21, -10))
                _screenPos = _Convert.ToPointFromString(_reg.getItemLocation(i.ToString("D2")))
                'ScreenPos = _txt.Location
                _color = New SolidBrush(ColorTranslator.FromWin32(_reg.getItemColor(i.ToString("D2"))))
                'Write your text.
                Select Case _reg.getItemAlign(i.ToString("D2"))
                    Case 0 'Left
                        stringFormat.Alignment = StringAlignment.Near
                        ' stringFormat.LineAlignment = StringAlignment.Near
                    Case 1 'Right
                        stringFormat.Alignment = StringAlignment.Far
                        ' stringFormat.LineAlignment = StringAlignment.Far
                        _screenPos = New Point(_screenPos.X + _reg.getItemWidth(i.ToString("D2")), _screenPos.Y)
                    Case 2 'Center
                        stringFormat.Alignment = StringAlignment.Center
                        'stringFormat.LineAlignment = StringAlignment.Center
                        _screenPos = New Point(_screenPos.X + (_reg.getItemWidth(i.ToString("D2")) / 2), _screenPos.Y)
                End Select

                _envText = ConvertItems.itemToEnviromentVar(_txt)

                'TextRenderer.DrawText()
                graphicImage.DrawString(_envText, _reg.getItemFont(i.ToString("D2")), _color, _screenPos, stringFormat)
            Next

            'Save the new image to the response output stream.
            bitMapImage.Save(_reg.getWallpaper, System.Drawing.Imaging.ImageFormat.Png)
            'Clean house.
            graphicImage.Dispose()
            bitMapImage.Dispose()
            'Me.pbBackground.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub
End Module
