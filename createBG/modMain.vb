Imports Microsoft.Win32
Imports System.Drawing
Imports System.IO

Module modMain
    Const _locCorrect = 23

    Public screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Public screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height


    Sub Main()


        createPreviewFromBackground()

    End Sub

    Private Sub loadItemsFromRegistryAddToForm()
        Dim _inputFields As Integer = _reg.getInputFields()
        _wallpaper = _reg.getRegWallpaper()

        If Not _inputFields = 0 Then
            For i As Integer = 0 To _inputFields - 1

                With txt
                    .Text = _reg.getItemText(i.ToString("D2"))
                    .Name = "TextBox" & _nameN
                    .ContextMenuStrip = cmsItems
                    .ForeColor = ColorTranslator.FromWin32(_reg.getItemColor(i.ToString("D2")))
                    .Size() = New System.Drawing.Size(_reg.getItemWidth(i.ToString("D2")), 21)
                    .Font = _reg.getItemFont(i.ToString("D2"))
                    .TextAlign = _reg.getItemAlign(i.ToString("D2"))
                    '.Location = Convert.ToPointFromString(_reg.getItemLocation(i.ToString("D2")))
                    .Location = PointToClient(_Convert.ToPointFromString(_reg.getItemLocation(i.ToString("D2"))))
                    .BorderStyle = BorderStyle.FixedSingle
                End With
                Me.Controls.Add(txt)
                Me.txt.BringToFront()
                lbItems.Items.Add("TextBox" & _nameN)
                _nameN = _nameN + 1
            Next
        End If

        _inputFields = Nothing
    End Sub

    Sub createPreviewFromBackground()
        Dim _txt As String
        Dim i As Integer = 0
        Dim _screenPos As Point
        Dim _imgToMemoryStream As New MemoryStream()
        Dim _color As Brush
        Dim _envText As String
        Dim stringFormat As New StringFormat()
        Dim _inputFields As Integer = _reg.getInputFields()
        Dim _wallpaper As String = _reg.getRegWallpaper()

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
                _screenPos = _txt.PointToScreen(New Point(-5, 0))
                'ScreenPos = _txt.Location
                _color = New SolidBrush()
                'Write your text.
                Select Case _txt.TextAlign
                    Case 0 'Left
                        stringFormat.Alignment = StringAlignment.Near
                        ' stringFormat.LineAlignment = StringAlignment.Near
                    Case 1 'Right
                        stringFormat.Alignment = StringAlignment.Far
                        ' stringFormat.LineAlignment = StringAlignment.Far
                        _screenPos = New Point(_screenPos.X + _txt.Width, _screenPos.Y)
                    Case 2 'Center
                        stringFormat.Alignment = StringAlignment.Center
                        'stringFormat.LineAlignment = StringAlignment.Center
                        _screenPos = New Point(_screenPos.X + (_txt.Width / 2), _screenPos.Y)
                End Select

                'TextRenderer.DrawText(graphicImage, _txt.Text, _txt.Font, New Point(ScreenPos), _txt.ForeColor, Color.Transparent, TextFormatFlags.HorizontalCenter)
                _envText = ConvertItems.itemToEnviromentVar(_txt.Text)
                'TextRenderer.DrawText()
                graphicImage.DrawString(_envText, _txt.Font, _color, _screenPos, stringFormat)

            Next

            'I am drawing a oval around my text.
            ' graphicImage.DrawArc(New Pen(Color.Red, 3), 90, 235, 150, 50, 0, 360)

            'Save the new image to the response output stream.
            bitMapImage.Save(_imgToMemoryStream, System.Drawing.Imaging.ImageFormat.Png)
            'Clean house.
            graphicImage.Dispose()
            bitMapImage.Dispose()

            Me.pbBackground.BackgroundImage = Image.FromStream(_imgToMemoryStream)
            'Me.pbBackground.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub
End Module
