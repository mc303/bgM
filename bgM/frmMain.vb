Imports Microsoft.Win32
Imports System.Text.RegularExpressions
Imports System.Drawing.Imaging
Imports System.IO

Public Class frmMain
    Dim _i As Integer = 0
    Dim _l As Integer = 0
    Dim _nameN As Integer = 0
    ' Dim cmb As New ComboBox()
    Dim txt As New TextBox()

    Public _notSaved As Boolean = False
    ' Public _

    Public Const _locCorrect = 0

    Public screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Public screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

    Private _bRubberBandingOn As Boolean = False '-- State to control if we are drawing the rubber banding object
    Private _pClickStart As New Point '-- The place where the mouse button went 'down'.
    Private _pClickStop As New Point '-- The place where the mouse button went 'up'.
    Private _pNow As New Point

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Call Me.saveComboboxToReg()
    End Sub

    Private Sub frmMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Select Case e.KeyCode
            Case Keys.F5
                Call frmPreview.Show()
            Case Keys.Escape
                Call Me.saveMessageBeforeExit()
        End Select
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        Dim _image As Bitmap

        'check registry
        Call _reg.createRootKey()

        Dim _fontstyle As FontStyle = _reg.getFontStyle
        Dim _font As Font = New Font(_reg.getFontFamily, Convert.ToSingle(_reg.getFontSize), _fontstyle)
       

        With Me.Panel1
            .Visible = False
        End With

        With Me.tsMain
            .Top = 25
            .Left = 8
        End With


        plResize.Visible = False

        With Me.nudResize
            .Maximum = 500
            .Minimum = 50
        End With

        Me.KeyPreview = True

        If _reg.getRegWallpaper = "" Then
            Dim _regKey As RegistryKey
            _regKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", False)
            If _regKey.GetValue("Wallpaper") = "" Then
                _regKey = Registry.CurrentUser.OpenSubKey("Control Panel\Colors", False)
                Dim _color As String() = _regKey.GetValue("Background").ToString.Split(" ")
                Me.pbMainBackground.BackColor = ColorTranslator.FromOle(RGB(_color(0), _color(1), _color(2)))
            Else
                _image = ResizeImage.Image(_regKey.GetValue("Wallpaper"), New Size(screenWidth, screenHeight), True)
            End If
            _regKey = Nothing
        Else
            _image = ResizeImage.Image(_reg.getRegWallpaper, New Size(screenWidth, screenHeight), True)           
        End If

        With Me.pbMainBackground
            .Dock = DockStyle.Fill
            .BorderStyle = BorderStyle.None
            '.BackgroundImageLayout = ImageLayout.Center
            .BackgroundImage = _image
            .Visible = True
            .SendToBack()
        End With

        With Me.tscmdShow
            .Visible = False
        End With

        Call Me.loadItemsFromRegistryAddToForm()

        With Me.tscbListItems
            .Text = "%USERNAME%"
            .Items.Add("%ALLUSERSPROFILE%")
            .Items.Add("%APPDATA%")
            .Items.Add("%CommonProgramFiles%")
            .Items.Add("%COMMONPROGRAMFILES(x86)%")
            .Items.Add("%COMPUTERNAME%")
            .Items.Add("%COMSPEC%")
            .Items.Add("%HOMEDRIVE%")
            .Items.Add("%HOMEPATH%")
            .Items.Add("%LOCALAPPDATA%")
            .Items.Add("%LOGONSERVER%")
            .Items.Add("%NUMBER_OF_PROCESSORS%")
            .Items.Add("%OS%")
            .Items.Add("%HO%")
            .Items.Add("%ProgramData%")
            .Items.Add("%ProgramFiles%")
            .Items.Add("%ProgramFiles(x86)%")
            .Items.Add("%PSModulePath%")
            .Items.Add("%Public%")
            .Items.Add("%SessionName%")
            .Items.Add("%SYSTEMDRIVE%")
            .Items.Add("%SYSTEMROOT%")
            .Items.Add("%TEMP%")
            .Items.Add("%UserDnsDomain%")
            .Items.Add("%USERDOMAIN%")
            .Items.Add("%USERNAME%")
            .Items.Add("%USERPROFILE%")
            .Items.Add("%WINDIR%")
        End With

        For Each Family As FontFamily In FontFamily.Families
            tscbFontFamilies.Items.Add(Family.Name)
        Next
        tscbFontFamilies.Text = _reg.getFontFamily

        With Me.tscbFontSize
            .Items.Add("8")
            .Items.Add("9")
            .Items.Add("10")
            .Items.Add("11")
            .Items.Add("12")
            .Items.Add("14")
            .Items.Add("16")
            .Items.Add("18")
            .Items.Add("20")
            .Items.Add("22")
            .Items.Add("24")
            .Items.Add("26")
            .Items.Add("28")
            .Items.Add("36")
            .Items.Add("48")
            .Items.Add("72")
        End With
        tscbFontSize.Text = _reg.getFontSize

        Select Case _fontstyle
            Case FontStyle.Bold
                tscmdFontBold.Checked = True

            Case FontStyle.Italic
                tscmdFontItalic.Checked = True

            Case FontStyle.Underline
                tscmdFontUnderline.Checked = True

            Case FontStyle.Bold Or FontStyle.Italic
                tscmdFontBold.Checked = True
                tscmdFontItalic.Checked = True

            Case FontStyle.Bold Or FontStyle.Underline
                tscmdFontBold.Checked = True
                tscmdFontUnderline.Checked = True

            Case FontStyle.Italic Or FontStyle.Underline
                tscmdFontItalic.Checked = True
                tscmdFontUnderline.Checked = True

            Case FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline
                tscmdFontBold.Checked = True
                tscmdFontItalic.Checked = True
                tscmdFontUnderline.Checked = True
        End Select


        Me.FontDialog1.Font = _font
        Me.ColorDialog1.Color = ColorTranslator.FromWin32(_reg.getFontColor)
        Me.tscmdFontColor.BackColor = ColorTranslator.FromWin32(_reg.getFontColor)



    End Sub

    Public Function getFontStyle() As FontStyle
        Dim _fontstyle As FontStyle = FontStyle.Regular

        If tscmdFontBold.Checked Then _fontstyle = _fontstyle Or FontStyle.Bold
        If tscmdFontItalic.Checked Then _fontstyle = _fontstyle Or FontStyle.Italic
        If tscmdFontUnderline.Checked Then _fontstyle = _fontstyle Or FontStyle.Underline

        Return _fontstyle
    End Function

    Public Sub addItemToForm(ByVal _txtText As String)
        txt = New TextBox
        Dim _font As Font

        ' AddHandler cmb.Click, AddressOf btnDelete
        AddHandler txt.MouseDown, AddressOf Me.txt_MouseDown
        AddHandler txt.MouseMove, AddressOf Me.txt_MouseMove
        AddHandler txt.MouseUp, AddressOf Me.txt_MouseUp
        AddHandler txt.KeyDown, AddressOf Me.txt_KeyDown
        AddHandler txt.GotFocus, AddressOf Me.txt_GotFocus

        _font = New Font(tscbFontFamilies.Text, Convert.ToSingle(tscbFontSize.Text), getFontStyle)

        With txt
            .Text = _txtText
            .Location = New Point((screenWidth / 2), (screenHeight / 2))
            .Font = _font 'Me.FontDialog1.Font
            .Name = "TextBox" & _nameN
            .ContextMenuStrip = cmsItems
            .ForeColor = tscmdFontColor.BackColor
            .Size() = New System.Drawing.Size(220, 21)
            .BorderStyle = BorderStyle.FixedSingle
        End With

        Me.Controls.Add(txt)
        Me.txt.BringToFront()
        lbItems.Items.Add("TextBox" & _nameN)
        _nameN = _nameN + 1
        _font = Nothing
    End Sub


    Private Sub loadItemsFromRegistryAddToForm()
        Dim _inputFields As Integer = _reg.getInputFields()
        Me.txtOpenBackgroundFileName.Text = _reg.getRegWallpaper()

        If Not _inputFields = 0 Then
            For i As Integer = 0 To _inputFields - 1
                txt = New TextBox

                AddHandler txt.MouseDown, AddressOf Me.txt_MouseDown
                AddHandler txt.MouseMove, AddressOf Me.txt_MouseMove
                AddHandler txt.MouseUp, AddressOf Me.txt_MouseUp
                AddHandler txt.KeyDown, AddressOf Me.txt_KeyDown
                AddHandler txt.GotFocus, AddressOf Me.txt_GotFocus

                With txt
                    .Text = _reg.getItemText(i.ToString("D2"))
                    .Name = "TextBox" & _nameN
                    .ContextMenuStrip = cmsItems
                    .ForeColor = ColorTranslator.FromWin32(_reg.getItemColor(i.ToString("D2")))
                    .Size() = New System.Drawing.Size(_reg.getItemWidth(i.ToString("D2")), 21)
                    .Font = _Convert.ToFontFromString(_reg.getItemFont(i.ToString("D2")))
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

    Public Sub saveItemsToRegistry()
        Dim _txt As TextBox = txt
        Dim i As Integer = 0
        Dim _screenPos As Point

        'Remove
        Call Me.removeRegText()

        'txtBox3.Text = ScreenPos.ToString
        lblScreenPos.Text = _screenPos.ToString
        _reg.setInputFields(Me.Controls.OfType(Of TextBox).Count)
        _reg.setRegWallpaper(Me.txtOpenBackgroundFileName.Text)
        '_reg.setFont(FontDialog1.Font.ToString)

        Call _reg.setFontFamily(tscbFontFamilies.Text)
        Call _reg.setFontSize(tscbFontSize.Text)
        Call _reg.setFontStyle(getFontStyle)

        For Each _item As String In lbItems.Items
            _txt = CType(Me.Controls(_item), TextBox)
            _screenPos = _txt.PointToScreen(New Point(0, 0))
            _reg.setItemText(i.ToString("D2"), _txt.Text)
            _reg.setItemFont(i.ToString("D2"), _txt.Font.ToString)
            _reg.setItemColor(i.ToString("D2"), ColorTranslator.ToWin32(_txt.ForeColor))
            _reg.setItemLocation(i.ToString("D2"), _screenPos.ToString)
            _reg.setItemWidth(i.ToString("D2"), _txt.Width)
            _reg.setItemAlign(i.ToString("D2"), _txt.TextAlign)
            i = i + 1
        Next

        _notSaved = False
        _txt = Nothing
    End Sub
    Private Sub removeRegText()
        Dim _inputFields As Integer = _reg.getInputFields()
        If Not _inputFields = 0 Then
            For i As Integer = 0 To _inputFields - 1
                Try
                    _reg.delItemText(i.ToString("D2"))
                    _reg.delItemFont(i.ToString("D2"))
                    _reg.delItemColor(ToString("D2"))
                    _reg.delItemLocation(i.ToString("D2"))
                    _reg.delItemWidth(i.ToString("D2"))
                    _reg.delItemAlign(i.ToString("D2"))
                Catch ex As Exception

                End Try
            Next
        End If
        _reg.setInputFields(0)

        _inputFields = Nothing
    End Sub

    Sub saveItemsToBackground(ByVal _bg As String)
        Dim _txt As TextBox
        Dim i As Integer = 0
        Dim _screenPos As Point
        'Dim _imgToMemoryStream As New MemoryStream()
        Dim _color As Brush

        Dim stringFormat As New StringFormat()

        'Load the Image to be written on.
        Dim bitMapImage As Bitmap = pbMainBackground.BackgroundImage
        'Dim bitMapImage As Bitmap = Image.FromFile(_bg)
        Dim graphicImage As Graphics = Graphics.FromImage(bitMapImage)

        'Smooth graphics is nice.
        'graphicImage.SmoothingMode = SmoothingMode.AntiAlias
        graphicImage.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        graphicImage.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
        graphicImage.CompositingQuality = Drawing2D.CompositingQuality.HighQuality


        For Each _item As String In Me.lbItems.Items
            _txt = CType(Me.Controls(_item), TextBox)
            '_screenPos = _txt.PointToScreen(New Point(-21, -10))
            _screenPos = _txt.PointToScreen(New Point(-5, 0))
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

            'TextRenderer.DrawText()
            graphicImage.DrawString(_txt.Text, _txt.Font, _color, _screenPos, stringFormat)

        Next

        'I am drawing a oval around my text.
        ' graphicImage.DrawArc(New Pen(Color.Red, 3), 90, 235, 150, 50, 0, 360)

        'Save the new image to the response output stream.
        bitMapImage.Save(_bg, System.Drawing.Imaging.ImageFormat.Png)
        'Clean house.
        graphicImage.Dispose()
        bitMapImage.Dispose()

        _txt = Nothing
        stringFormat = Nothing
        graphicImage = Nothing
        bitMapImage = Nothing
        'Me.pbBackground.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Sub saveMessageBeforeExit()
        If _notSaved Then
            Dim result As Integer = MessageBox.Show("Do you want to save changes?", "Message before Exit", MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Cancel Then
                '
            ElseIf result = DialogResult.No Then
                Me.Dispose()
            ElseIf result = DialogResult.Yes Then
                Me.saveItemsToRegistry()
                Me.Dispose()
            End If
        Else
            Me.Dispose()
        End If
    End Sub

    Private Sub btnDelete(sender As Object, e As EventArgs)
        Dim _txt As TextBox = DirectCast(sender, TextBox)
        ''Me.Controls.Remove(_txt)
    End Sub

    Private Sub txt_MouseDown(sender As Object, e As MouseEventArgs)
        Dim _controlMove As TextBox = DirectCast(sender, TextBox)
        _controlMove.BringToFront()
        _controlMove.Tag = New DragInfo(Form.MousePosition, _controlMove.Location)
        _controlMove = Nothing
    End Sub

    Private Sub txt_MouseMove(sender As Object, e As MouseEventArgs)
        Dim _controlMove As TextBox = DirectCast(sender, TextBox)

        If _controlMove.Tag IsNot Nothing Then
            Dim info As DragInfo = CType(_controlMove.Tag, DragInfo)
            Dim newLoc As Point = info.NewLocation(Form.MousePosition)

            If Me.ClientRectangle.Contains(New Rectangle(newLoc, _controlMove.Size)) Then
                _controlMove.Location = newLoc
                'txtBox1.Text = newLoc.ToString
                Dim _screenPos As Point = _controlMove.PointToScreen(New Point(0, 0))
                'txtBox3.Text = ScreenPos.ToString

                If Not lblScreenPos.Text = _screenPos.ToString Then
                    lblScreenPos.Text = _screenPos.ToString
                    _notSaved = True
                End If
            End If
        End If
        _controlMove = Nothing
    End Sub

    Private Sub txt_MouseUp(sender As Object, e As MouseEventArgs)
        Dim _controlMove As TextBox = DirectCast(sender, TextBox)
        _controlMove.Tag = Nothing
        _controlMove = Nothing
    End Sub

    Private Sub txt_KeyDown(sender As Object, e As KeyEventArgs)
        Dim _controlMove As TextBox = DirectCast(sender, TextBox)
        'txtBox2.Text = e.KeyData
        Select Case e.KeyData
            Case (Keys.Control + Keys.Left)
                _controlMove.Location = New Point(_controlMove.Left - 1, _controlMove.Top)
                _notSaved = True
            Case (Keys.Control + Keys.Right)
                _controlMove.Location = New Point(_controlMove.Left + 1, _controlMove.Top)
                _notSaved = True
            Case (Keys.Control + Keys.Up)
                _controlMove.Location = New Point(_controlMove.Left, _controlMove.Top - 1)
                _notSaved = True
            Case (Keys.Control + Keys.Down)
                _controlMove.Location = New Point(_controlMove.Left, _controlMove.Top + 1)
                _notSaved = True
        End Select

        Dim _screenPos As Point = _controlMove.PointToScreen(New Point(0, 0))
        'txtBox3.Text = _screenPos.ToString
        'txtBox1.Text = _controlMove.Location.ToString
        lblScreenPos.Text = _screenPos.ToString

        _controlMove.Enabled = True

        _controlMove = Nothing
        _screenPos = Nothing
    End Sub

    Private Sub txt_GotFocus(sender As Object, e As EventArgs)
        Dim _controlMove As TextBox = DirectCast(sender, TextBox)
        Dim _screenPos As Point = _controlMove.PointToScreen(New Point(0, 0))
        'txtBox3.Text = _screenPos.ToString
        'txtBox1.Text = _controlMove.Location.ToString
        lblScreenPos.Text = _screenPos.ToString
        _controlMove = Nothing
        _screenPos = Nothing
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        Dim _clickedMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim _cms As ContextMenuStrip = CType(_clickedMenu.Owner, ContextMenuStrip)

        ' MessageBox.Show(_cms.SourceControl.Name)
        Me.Controls.Remove(_cms.SourceControl)
        For Each _item As String In lbItems.Items
            If _item = _cms.SourceControl.Name Then
                lbItems.Items.Remove(_item)
                Exit For
            End If
        Next
        _cms = Nothing
        _clickedMenu = Nothing
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsItems.Opening
        Dim _cms As ContextMenuStrip = CType(sender, ContextMenuStrip)
        Dim _txt As TextBox = _cms.SourceControl
        NamethingToolStripMenuItem.Text = _txt.Name

        Select Case _txt.TextAlign
            Case HorizontalAlignment.Left
                AlignRightToolStripMenuItem.Enabled = True
                AlignLeftToolStripMenuItem.Enabled = False
            Case HorizontalAlignment.Right
                AlignRightToolStripMenuItem.Enabled = False
                AlignLeftToolStripMenuItem.Enabled = True
            Case HorizontalAlignment.Center
                AlignRightToolStripMenuItem.Enabled = True
                AlignLeftToolStripMenuItem.Enabled = True

        End Select
        _cms = Nothing
    End Sub

    Private Sub FontToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FontToolStripMenuItem1.Click
        Dim _clickedMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim _cms As ContextMenuStrip = CType(_clickedMenu.Owner, ContextMenuStrip)
        FontDialog1.Font = _cms.SourceControl.Font
        If (FontDialog1.ShowDialog() = DialogResult.OK) Then
            _cms.SourceControl.Font = FontDialog1.Font
        End If
        _cms = Nothing
        _clickedMenu = Nothing
    End Sub

    Private Sub ColorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ColorToolStripMenuItem.Click
        Dim _clickedMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim _cms As ContextMenuStrip = CType(_clickedMenu.Owner, ContextMenuStrip)
        ColorDialog1.Color = _cms.SourceControl.ForeColor
        If (ColorDialog1.ShowDialog() = DialogResult.OK) Then
            _cms.SourceControl.ForeColor = ColorDialog1.Color
        End If
        _cms = Nothing
        _clickedMenu = Nothing
    End Sub

    Private Sub SizeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SizeToolStripMenuItem.Click
        Dim _clickedMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim _cms As ContextMenuStrip = CType(_clickedMenu.Owner, ContextMenuStrip)

        With Me.plResize
            .Location = New Point(_cms.SourceControl.Left, _cms.SourceControl.Top + (_cms.SourceControl.Height + 1))
            .Visible = True
            .BringToFront()
        End With

        Me.nudResize.Text = _cms.SourceControl.Width
        Me.nudResize.Tag = _cms.SourceControl
        _cms = Nothing
        _clickedMenu = Nothing
    End Sub

    Private Sub cmdResize_Click(sender As Object, e As EventArgs) Handles cmdResize.Click
        Dim _txt As TextBox = Me.nudResize.Tag
        _txt.Width = Integer.Parse(nudResize.Text)
        plResize.Visible = False
        _txt = Nothing
        Me.nudResize.Tag = Nothing
    End Sub


    Private Sub AlignRightToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlignRightToolStripMenuItem.Click
        Dim _clickedMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim _cms As ContextMenuStrip = CType(_clickedMenu.Owner, ContextMenuStrip)
        Dim _txt As TextBox = _cms.SourceControl

        _txt.TextAlign = HorizontalAlignment.Right

        _cms = Nothing
        _txt = Nothing
        _clickedMenu = Nothing
    End Sub

    Private Sub tscmdAddItem_Click(sender As Object, e As EventArgs) Handles tscmdAddItem.Click
        _notSaved = True
        Call Me.addItemToForm(tscbListItems.Text)
    End Sub

    Private Sub tscmdPreviewBG_Click(sender As Object, e As EventArgs) Handles tscmdPreviewBG.Click
        Call frmPreview.Show()
    End Sub

    Private Sub tscmdOpenBG_Click(sender As Object, e As EventArgs) Handles tscmdOpenBG.Click
        With Me.ofdOpenBackground
            .Filter = "PNG Files|*.png|JPEG Files|*.jpg"
            .DefaultExt = "png"
        End With

        If (ofdOpenBackground.ShowDialog() = DialogResult.OK) Then
            'Call _reg.setRegWallpaper(ofdOpenBG.FileName)
            Me.txtOpenBackgroundFileName.Text = ofdOpenBackground.FileName
            Me.pbMainBackground.BackgroundImage = ResizeImage.Image(ofdOpenBackground.FileName, New Size(screenWidth, screenHeight), True)
            _notSaved = True
        End If
    End Sub

    Private Sub tscmdSaveItems_Click(sender As Object, e As EventArgs) Handles tscmdSaveItems.Click
        Call Me.saveItemsToRegistry()

    End Sub

    Private Sub AlignLeftToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlignLeftToolStripMenuItem.Click
        Dim _clickedMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim _cms As ContextMenuStrip = CType(_clickedMenu.Owner, ContextMenuStrip)
        Dim _txt As TextBox = _cms.SourceControl

        _txt.TextAlign = HorizontalAlignment.Left

        _cms = Nothing
        _txt = Nothing
        _clickedMenu = Nothing
    End Sub

    Private Sub tscmdHideShow_Click(sender As Object, e As EventArgs) Handles tscmdHide.Click
        tsMain.Size = New Size(41, 25)
        tscmdShow.Visible = True
        tscmdHide.Visible = False
    End Sub

    Private Sub tscmdShow_Click(sender As Object, e As EventArgs) Handles tscmdShow.Click
        tsMain.Size = New Size(942, 25)
        tscmdShow.Visible = False
        tscmdHide.Visible = True
    End Sub

    Private Sub AlignCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlignCenterToolStripMenuItem.Click
        Dim _clickedMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim _cms As ContextMenuStrip = CType(_clickedMenu.Owner, ContextMenuStrip)
        Dim _txt As TextBox = _cms.SourceControl

        _txt.TextAlign = HorizontalAlignment.Center

        _cms = Nothing
        _txt = Nothing
        _clickedMenu = Nothing
    End Sub

    Private Sub tscmdClose_Click(sender As Object, e As EventArgs) Handles tscmdClose.Click
        Call Me.saveMessageBeforeExit()
    End Sub

    Private Sub tscmdCreateBackground_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub tscmdSaveBackground_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtOpenBackgroundFileName_TextChanged(sender As Object, e As EventArgs) Handles txtOpenBackgroundFileName.TextChanged
        Dim _txt As TextBox = DirectCast(sender, TextBox)

        If Not _txt.Text = _reg.getRegWallpaper Then
            _notSaved = True
        End If
    End Sub

    Private Sub ChangeColorAllItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeColorAllItemsToolStripMenuItem.Click
        Dim _txt As TextBox
        ColorDialog1.Color = tscmdFontColor.BackColor

        If (ColorDialog1.ShowDialog() = DialogResult.OK) Then
            For Each _item As String In lbItems.Items
                _txt = CType(Me.Controls(_item), TextBox)
                _txt.ForeColor = ColorDialog1.Color
                _notSaved = True
                tscmdFontColor.BackColor = ColorDialog1.Color
            Next
        End If

        _txt = Nothing
    End Sub

    Private Sub ChangeFontAllItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeFontAllItemsToolStripMenuItem.Click
        Dim _txt As TextBox
        FontDialog1.Font = New Font(tscbFontFamilies.Text, Convert.ToSingle(tscbFontSize.Text), getFontStyle)

        If (FontDialog1.ShowDialog() = DialogResult.OK) Then
            For Each _item As String In lbItems.Items
                _txt = CType(Me.Controls(_item), TextBox)
                _txt.Font = FontDialog1.Font
                _notSaved = True
            Next

            tscbFontFamilies.Text = FontDialog1.Font.Name.ToString
            tscbFontSize.Text = FontDialog1.Font.Size.ToString

            Select Case FontDialog1.Font.Style
                Case FontStyle.Bold
                    tscmdFontBold.Checked = True

                Case FontStyle.Italic
                    tscmdFontItalic.Checked = True

                Case FontStyle.Underline
                    tscmdFontUnderline.Checked = True

                Case FontStyle.Bold Or FontStyle.Italic
                    tscmdFontBold.Checked = True
                    tscmdFontItalic.Checked = True

                Case FontStyle.Bold Or FontStyle.Underline
                    tscmdFontBold.Checked = True
                    tscmdFontUnderline.Checked = True

                Case FontStyle.Italic Or FontStyle.Underline
                    tscmdFontItalic.Checked = True
                    tscmdFontUnderline.Checked = True

                Case FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline
                    tscmdFontBold.Checked = True
                    tscmdFontItalic.Checked = True
                    tscmdFontUnderline.Checked = True
            End Select
        End If

        _txt = Nothing
    End Sub

    Private Sub tscmdColor_Click(sender As Object, e As EventArgs) Handles tscmdFontColor.Click
        If (ColorDialog1.ShowDialog() = DialogResult.OK) Then
            tscmdFontColor.BackColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Call Me.saveMessageBeforeExit()
    End Sub

    Private Sub CreateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateToolStripMenuItem.Click
        Dim newBitmap As Bitmap
        Dim g As Graphics

        With Me.sfdSaveColorBackground
            .Filter = "PNG Files | *.png"
            .DefaultExt = "png"
        End With

        If (sfdSaveColorBackground.ShowDialog() = DialogResult.OK) Then
            '_bg = sfdSaveColorBackground.FileName
            txtOpenBackgroundFileName.Text = sfdSaveColorBackground.FileName
        End If

        If (ColorDialog1.ShowDialog() = DialogResult.OK) Then
            ' Create bitmap from color
            newBitmap = New Bitmap(screenWidth, screenHeight, PixelFormat.Format32bppArgb)
            g = Graphics.FromImage(newBitmap)

            g.FillRectangle(New SolidBrush(Me.ColorDialog1.Color), New Rectangle(0, 0, screenWidth, screenHeight))

            ' I needed to save to a PNG..
            newBitmap.Save(sfdSaveColorBackground.FileName, ImageFormat.Png)
            Call loadPictureToMsToPicturebox()
            _notSaved = True
            newBitmap.Dispose()
            g.Dispose()
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        With Me.sfdSaveColorBackground
            .Filter = "PNG Files | *.png"
            .DefaultExt = "png"
        End With

        If (sfdSaveColorBackground.ShowDialog() = DialogResult.OK) Then
            'txtOpenBackgroundFileName.Text = sfdSaveColorBackground.FileName
            '_bg = sfdSaveColorBackground.FileName
            Call Me.saveItemsToBackground(sfdSaveColorBackground.FileName)
        End If
    End Sub
End Class
