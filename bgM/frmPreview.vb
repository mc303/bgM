Imports Microsoft.Win32
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class frmPreview
    Public lbl As New Label()

    Const _locCorrect = 23

    Public screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Public screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

    Private Sub frmPreview_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub frmPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim _txt As TextBox = frmMain.txtOpenBackgroundFileName
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized


        With Me.pbBackground
            .Visible = True
            .Dock = DockStyle.Fill
            '.Top = 8
            '.Left = 8
            '.Width = screenWidth
            '.Height = screenHeight
            .BackColor = Color.Red
            '.BackgroundImage = Bitmap.FromFile(_txt.Text)
            .BackgroundImage = ResizeImage.Image(_txt.Text, New Size(screenWidth, screenHeight), False)

            '.BackgroundImageLayout = ImageLayout.Stretch
            .SendToBack()
        End With

        ' ImageAutoSize.Image(_reg.getRegWallpaper, Me.pbBackground)

        Call Me.createPreviewFromBackground()
        'Call Me.loadPreviewView()
       
        _txt = Nothing
    End Sub

    Sub createPreviewFromBackground()
        Dim _txt As TextBox
        Dim i As Integer = 0
        Dim _screenPos As Point
        Dim _imgToMemoryStream As New MemoryStream()
        Dim _color As Brush
        Dim _envText As String
        Dim stringFormat As New StringFormat()

       
        'Load the Image to be written on.
        Dim bitMapImage As Bitmap = pbBackground.BackgroundImage
        'Dim bitMapImage As Bitmap = Image.FromFile(_bg)
        Dim graphicImage As Graphics = Graphics.FromImage(bitMapImage)

        'Smooth graphics is nice.
        'graphicImage.SmoothingMode = SmoothingMode.AntiAlias
        graphicImage.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        graphicImage.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
        graphicImage.CompositingQuality = Drawing2D.CompositingQuality.HighQuality


        For Each _item As String In frmMain.lbItems.Items
            _txt = CType(frmMain.Controls(_item), TextBox)
            '_screenPos = _txt.PointToScreen(New Point(-21, -10))
            ' _screenPos = _txt.PointToScreen(New Point(-5, 0))
            _screenPos = _txt.PointToScreen(New Point(0, 5))
            'ScreenPos = _txt.Location
            _color = New SolidBrush(_txt.ForeColor)
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
    End Sub


 

    Private Sub pbBackground_MouseMove(sender As Object, e As MouseEventArgs) Handles pbBackground.MouseMove
        Dim screenPos As Point = MousePosition()
        lblScreenPos.Text = screenPos.ToString
    End Sub
End Class
