Imports System.IO

Module mainMod
   
    Public screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Public screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height


    Public Sub loadPictureToMsToPicturebox()
        Dim _bgBox As PictureBox = frmMain.pbMainBackground
        Dim _bg As String = _reg.getRegWallpaper
        Dim _imgToMemoryStream As New MemoryStream()
        Dim _bmImage As Bitmap = New System.Drawing.Bitmap(_bg)

        _bmImage.Save(_imgToMemoryStream, System.Drawing.Imaging.ImageFormat.Png)
        _bmImage.Dispose()

        _bgBox.Image = Nothing
        _bgBox.BackgroundImage = Image.FromStream(_imgToMemoryStream)
        _bgBox.BackgroundImageLayout = ImageLayout.Stretch
        _imgToMemoryStream.Dispose()

        _imgToMemoryStream = Nothing
        _bgBox = Nothing
        _bg = Nothing

    End Sub

    Public Sub saveLocation(_lbl As Control)

        Dim _pX As Integer = _lbl.Left / (screenWidth / 100)
        Dim _pY As Integer = (_lbl.Top + 25) / (screenHeight / 100)
        ' frmMain.TextBox3.Text = frmMain.MousePosition.X & ", " & frmMain.MousePosition.Y & " - " & _pX & ", " & _pY

        Call _reg.setRegX(_pX)
        Call _reg.setRegY(_pY)
    End Sub

    

End Module
