<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.lbItems = New System.Windows.Forms.ListBox()
        Me.cmsItems = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NamethingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FontToolStripMenuItem = New System.Windows.Forms.ToolStripSeparator()
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.FontToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlignRightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlignLeftToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlignCenterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtBox1 = New System.Windows.Forms.TextBox()
        Me.txtOpenBackgroundFileName = New System.Windows.Forms.TextBox()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.txtBox3 = New System.Windows.Forms.TextBox()
        Me.plResize = New System.Windows.Forms.Panel()
        Me.nudResize = New System.Windows.Forms.NumericUpDown()
        Me.cmdResize = New System.Windows.Forms.Button()
        Me.tsMain = New System.Windows.Forms.ToolStrip()
        Me.tscmdShow = New System.Windows.Forms.ToolStripButton()
        Me.tslblBackground = New System.Windows.Forms.ToolStripDropDownButton()
        Me.CreateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ApplyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tscmdOpenBG = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsMenu = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ChangeColorAllItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeFontAllItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tscbFontFamilies = New System.Windows.Forms.ToolStripComboBox()
        Me.tscbFontSize = New System.Windows.Forms.ToolStripComboBox()
        Me.tscmdFontBold = New System.Windows.Forms.ToolStripButton()
        Me.tscmdFontItalic = New System.Windows.Forms.ToolStripButton()
        Me.tscmdFontUnderline = New System.Windows.Forms.ToolStripButton()
        Me.tscmdFontColor = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tscmdAddItem = New System.Windows.Forms.ToolStripButton()
        Me.tscbListItems = New System.Windows.Forms.ToolStripComboBox()
        Me.tscmdHide = New System.Windows.Forms.ToolStripButton()
        Me.tscmdClose = New System.Windows.Forms.ToolStripButton()
        Me.tscmdPreviewBG = New System.Windows.Forms.ToolStripButton()
        Me.tscmdSaveItems = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tscmdSettings = New System.Windows.Forms.ToolStripButton()
        Me.pbMainBackground = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ofdOpenBackground = New System.Windows.Forms.OpenFileDialog()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.sfdSaveColorBackground = New System.Windows.Forms.SaveFileDialog()
        Me.plSettings = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdOpenSourceBackground = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbCoordinatesMode = New System.Windows.Forms.ComboBox()
        Me.cmdSavedUserBackground = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSaveWallpaper = New System.Windows.Forms.TextBox()
        Me.lblScreenPos = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.pbBackground = New System.Windows.Forms.PictureBox()
        Me.lblLiveScreenPos = New System.Windows.Forms.Label()
        Me.cmsItems.SuspendLayout()
        Me.plResize.SuspendLayout()
        CType(Me.nudResize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tsMain.SuspendLayout()
        CType(Me.pbMainBackground, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.plSettings.SuspendLayout()
        CType(Me.pbBackground, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbItems
        '
        Me.lbItems.FormattingEnabled = True
        Me.lbItems.Location = New System.Drawing.Point(7, 6)
        Me.lbItems.Name = "lbItems"
        Me.lbItems.Size = New System.Drawing.Size(120, 95)
        Me.lbItems.TabIndex = 2
        Me.lbItems.Visible = False
        '
        'cmsItems
        '
        Me.cmsItems.DropShadowEnabled = False
        Me.cmsItems.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmsItems.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NamethingToolStripMenuItem, Me.FontToolStripMenuItem, Me.RemoveToolStripMenuItem, Me.ToolStripSeparator1, Me.FontToolStripMenuItem1, Me.ColorToolStripMenuItem, Me.SizeToolStripMenuItem, Me.AlignRightToolStripMenuItem, Me.AlignLeftToolStripMenuItem, Me.AlignCenterToolStripMenuItem})
        Me.cmsItems.Name = "ContextMenuStrip1"
        Me.cmsItems.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.cmsItems.Size = New System.Drawing.Size(141, 192)
        '
        'NamethingToolStripMenuItem
        '
        Me.NamethingToolStripMenuItem.Enabled = False
        Me.NamethingToolStripMenuItem.Name = "NamethingToolStripMenuItem"
        Me.NamethingToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.NamethingToolStripMenuItem.Text = "name-thing"
        '
        'FontToolStripMenuItem
        '
        Me.FontToolStripMenuItem.Name = "FontToolStripMenuItem"
        Me.FontToolStripMenuItem.Size = New System.Drawing.Size(137, 6)
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.RemoveToolStripMenuItem.Text = "Remove"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(137, 6)
        '
        'FontToolStripMenuItem1
        '
        Me.FontToolStripMenuItem1.Name = "FontToolStripMenuItem1"
        Me.FontToolStripMenuItem1.Size = New System.Drawing.Size(140, 22)
        Me.FontToolStripMenuItem1.Text = "Font"
        '
        'ColorToolStripMenuItem
        '
        Me.ColorToolStripMenuItem.Name = "ColorToolStripMenuItem"
        Me.ColorToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.ColorToolStripMenuItem.Text = "Color"
        '
        'SizeToolStripMenuItem
        '
        Me.SizeToolStripMenuItem.Name = "SizeToolStripMenuItem"
        Me.SizeToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.SizeToolStripMenuItem.Text = "Size"
        '
        'AlignRightToolStripMenuItem
        '
        Me.AlignRightToolStripMenuItem.Name = "AlignRightToolStripMenuItem"
        Me.AlignRightToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.AlignRightToolStripMenuItem.Text = "Align Right"
        '
        'AlignLeftToolStripMenuItem
        '
        Me.AlignLeftToolStripMenuItem.Name = "AlignLeftToolStripMenuItem"
        Me.AlignLeftToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.AlignLeftToolStripMenuItem.Text = "Align Left"
        '
        'AlignCenterToolStripMenuItem
        '
        Me.AlignCenterToolStripMenuItem.Name = "AlignCenterToolStripMenuItem"
        Me.AlignCenterToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.AlignCenterToolStripMenuItem.Text = "Align Center"
        '
        'txtBox1
        '
        Me.txtBox1.Location = New System.Drawing.Point(6, 252)
        Me.txtBox1.Name = "txtBox1"
        Me.txtBox1.Size = New System.Drawing.Size(146, 20)
        Me.txtBox1.TabIndex = 6
        '
        'txtOpenBackgroundFileName
        '
        Me.txtOpenBackgroundFileName.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtOpenBackgroundFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOpenBackgroundFileName.Location = New System.Drawing.Point(8, 22)
        Me.txtOpenBackgroundFileName.Name = "txtOpenBackgroundFileName"
        Me.txtOpenBackgroundFileName.ReadOnly = True
        Me.txtOpenBackgroundFileName.Size = New System.Drawing.Size(528, 20)
        Me.txtOpenBackgroundFileName.TabIndex = 7
        '
        'txtBox3
        '
        Me.txtBox3.Location = New System.Drawing.Point(7, 278)
        Me.txtBox3.Name = "txtBox3"
        Me.txtBox3.Size = New System.Drawing.Size(145, 20)
        Me.txtBox3.TabIndex = 8
        '
        'plResize
        '
        Me.plResize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plResize.Controls.Add(Me.nudResize)
        Me.plResize.Controls.Add(Me.cmdResize)
        Me.plResize.Cursor = System.Windows.Forms.Cursors.Default
        Me.plResize.Location = New System.Drawing.Point(593, 305)
        Me.plResize.Name = "plResize"
        Me.plResize.Size = New System.Drawing.Size(178, 34)
        Me.plResize.TabIndex = 12
        '
        'nudResize
        '
        Me.nudResize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudResize.Location = New System.Drawing.Point(4, 6)
        Me.nudResize.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudResize.Name = "nudResize"
        Me.nudResize.Size = New System.Drawing.Size(97, 20)
        Me.nudResize.TabIndex = 3
        Me.nudResize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudResize.ThousandsSeparator = True
        Me.nudResize.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'cmdResize
        '
        Me.cmdResize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdResize.Location = New System.Drawing.Point(107, 4)
        Me.cmdResize.Name = "cmdResize"
        Me.cmdResize.Size = New System.Drawing.Size(63, 23)
        Me.cmdResize.TabIndex = 0
        Me.cmdResize.Text = "Close"
        Me.cmdResize.UseVisualStyleBackColor = True
        '
        'tsMain
        '
        Me.tsMain.AutoSize = False
        Me.tsMain.Dock = System.Windows.Forms.DockStyle.None
        Me.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsMain.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tscmdShow, Me.tslblBackground, Me.tscmdOpenBG, Me.ToolStripSeparator4, Me.tsMenu, Me.tscbFontFamilies, Me.tscbFontSize, Me.tscmdFontBold, Me.tscmdFontItalic, Me.tscmdFontUnderline, Me.tscmdFontColor, Me.ToolStripSeparator3, Me.tscmdAddItem, Me.tscbListItems, Me.tscmdHide, Me.tscmdClose, Me.tscmdPreviewBG, Me.tscmdSaveItems, Me.ToolStripSeparator2, Me.ToolStripButton1, Me.tscmdSettings})
        Me.tsMain.Location = New System.Drawing.Point(0, 0)
        Me.tsMain.Name = "tsMain"
        Me.tsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.tsMain.Size = New System.Drawing.Size(1050, 25)
        Me.tsMain.TabIndex = 14
        Me.tsMain.Text = "ToolStrip1"
        '
        'tscmdShow
        '
        Me.tscmdShow.AutoToolTip = False
        Me.tscmdShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tscmdShow.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tscmdShow.Image = CType(resources.GetObject("tscmdShow.Image"), System.Drawing.Image)
        Me.tscmdShow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tscmdShow.Name = "tscmdShow"
        Me.tscmdShow.Size = New System.Drawing.Size(23, 22)
        Me.tscmdShow.Text = ">"
        '
        'tslblBackground
        '
        Me.tslblBackground.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tslblBackground.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateToolStripMenuItem, Me.SaveToolStripMenuItem, Me.ToolStripSeparator5, Me.ApplyToolStripMenuItem, Me.ToolStripSeparator6, Me.ExitToolStripMenuItem})
        Me.tslblBackground.Image = CType(resources.GetObject("tslblBackground.Image"), System.Drawing.Image)
        Me.tslblBackground.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tslblBackground.Name = "tslblBackground"
        Me.tslblBackground.Size = New System.Drawing.Size(87, 22)
        Me.tslblBackground.Text = "Background:"
        '
        'CreateToolStripMenuItem
        '
        Me.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem"
        Me.CreateToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.CreateToolStripMenuItem.Text = "Create Background"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.SaveToolStripMenuItem.Text = "Save Background"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(172, 6)
        '
        'ApplyToolStripMenuItem
        '
        Me.ApplyToolStripMenuItem.Enabled = False
        Me.ApplyToolStripMenuItem.Name = "ApplyToolStripMenuItem"
        Me.ApplyToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.ApplyToolStripMenuItem.Text = "Apply Background"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(172, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'tscmdOpenBG
        '
        Me.tscmdOpenBG.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tscmdOpenBG.Image = CType(resources.GetObject("tscmdOpenBG.Image"), System.Drawing.Image)
        Me.tscmdOpenBG.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tscmdOpenBG.Name = "tscmdOpenBG"
        Me.tscmdOpenBG.Size = New System.Drawing.Size(40, 22)
        Me.tscmdOpenBG.Text = "Open"
        Me.tscmdOpenBG.ToolTipText = "Open Background file"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'tsMenu
        '
        Me.tsMenu.AutoSize = False
        Me.tsMenu.AutoToolTip = False
        Me.tsMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChangeColorAllItemsToolStripMenuItem, Me.ChangeFontAllItemsToolStripMenuItem})
        Me.tsMenu.Name = "tsMenu"
        Me.tsMenu.Size = New System.Drawing.Size(52, 22)
        Me.tsMenu.Text = "Items:"
        Me.tsMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ChangeColorAllItemsToolStripMenuItem
        '
        Me.ChangeColorAllItemsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ChangeColorAllItemsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ChangeColorAllItemsToolStripMenuItem.Name = "ChangeColorAllItemsToolStripMenuItem"
        Me.ChangeColorAllItemsToolStripMenuItem.ShowShortcutKeys = False
        Me.ChangeColorAllItemsToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.ChangeColorAllItemsToolStripMenuItem.Text = "Color: Change all Items"
        '
        'ChangeFontAllItemsToolStripMenuItem
        '
        Me.ChangeFontAllItemsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ChangeFontAllItemsToolStripMenuItem.Name = "ChangeFontAllItemsToolStripMenuItem"
        Me.ChangeFontAllItemsToolStripMenuItem.ShowShortcutKeys = False
        Me.ChangeFontAllItemsToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.ChangeFontAllItemsToolStripMenuItem.Text = "Font: Change all Items"
        Me.ChangeFontAllItemsToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tscbFontFamilies
        '
        Me.tscbFontFamilies.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.tscbFontFamilies.AutoToolTip = True
        Me.tscbFontFamilies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tscbFontFamilies.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tscbFontFamilies.Name = "tscbFontFamilies"
        Me.tscbFontFamilies.Size = New System.Drawing.Size(150, 25)
        Me.tscbFontFamilies.ToolTipText = "Font Family"
        '
        'tscbFontSize
        '
        Me.tscbFontSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tscbFontSize.Name = "tscbFontSize"
        Me.tscbFontSize.Size = New System.Drawing.Size(75, 25)
        Me.tscbFontSize.ToolTipText = "Font Size"
        '
        'tscmdFontBold
        '
        Me.tscmdFontBold.BackColor = System.Drawing.SystemColors.Control
        Me.tscmdFontBold.CheckOnClick = True
        Me.tscmdFontBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tscmdFontBold.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tscmdFontBold.Image = CType(resources.GetObject("tscmdFontBold.Image"), System.Drawing.Image)
        Me.tscmdFontBold.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tscmdFontBold.Name = "tscmdFontBold"
        Me.tscmdFontBold.Size = New System.Drawing.Size(23, 22)
        Me.tscmdFontBold.Text = "B"
        Me.tscmdFontBold.ToolTipText = "Bold"
        '
        'tscmdFontItalic
        '
        Me.tscmdFontItalic.CheckOnClick = True
        Me.tscmdFontItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tscmdFontItalic.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tscmdFontItalic.Image = CType(resources.GetObject("tscmdFontItalic.Image"), System.Drawing.Image)
        Me.tscmdFontItalic.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tscmdFontItalic.Name = "tscmdFontItalic"
        Me.tscmdFontItalic.Size = New System.Drawing.Size(23, 22)
        Me.tscmdFontItalic.Text = "I"
        Me.tscmdFontItalic.ToolTipText = "Italic"
        '
        'tscmdFontUnderline
        '
        Me.tscmdFontUnderline.CheckOnClick = True
        Me.tscmdFontUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tscmdFontUnderline.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tscmdFontUnderline.Image = CType(resources.GetObject("tscmdFontUnderline.Image"), System.Drawing.Image)
        Me.tscmdFontUnderline.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tscmdFontUnderline.Name = "tscmdFontUnderline"
        Me.tscmdFontUnderline.Size = New System.Drawing.Size(23, 22)
        Me.tscmdFontUnderline.Text = "U"
        Me.tscmdFontUnderline.ToolTipText = "Underline"
        '
        'tscmdFontColor
        '
        Me.tscmdFontColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tscmdFontColor.Image = CType(resources.GetObject("tscmdFontColor.Image"), System.Drawing.Image)
        Me.tscmdFontColor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tscmdFontColor.Name = "tscmdFontColor"
        Me.tscmdFontColor.Size = New System.Drawing.Size(23, 22)
        Me.tscmdFontColor.Text = "C"
        Me.tscmdFontColor.ToolTipText = "Change all items to the selected color"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'tscmdAddItem
        '
        Me.tscmdAddItem.AutoToolTip = False
        Me.tscmdAddItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tscmdAddItem.Image = CType(resources.GetObject("tscmdAddItem.Image"), System.Drawing.Image)
        Me.tscmdAddItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tscmdAddItem.Name = "tscmdAddItem"
        Me.tscmdAddItem.Size = New System.Drawing.Size(33, 22)
        Me.tscmdAddItem.Text = "Add"
        '
        'tscbListItems
        '
        Me.tscbListItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.tscbListItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.tscbListItems.AutoSize = False
        Me.tscbListItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tscbListItems.Name = "tscbListItems"
        Me.tscbListItems.Size = New System.Drawing.Size(150, 23)
        Me.tscbListItems.Text = "%username%"
        '
        'tscmdHide
        '
        Me.tscmdHide.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tscmdHide.AutoToolTip = False
        Me.tscmdHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tscmdHide.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tscmdHide.Image = CType(resources.GetObject("tscmdHide.Image"), System.Drawing.Image)
        Me.tscmdHide.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tscmdHide.Name = "tscmdHide"
        Me.tscmdHide.Size = New System.Drawing.Size(23, 22)
        Me.tscmdHide.Text = "<"
        '
        'tscmdClose
        '
        Me.tscmdClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tscmdClose.AutoToolTip = False
        Me.tscmdClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tscmdClose.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tscmdClose.Image = CType(resources.GetObject("tscmdClose.Image"), System.Drawing.Image)
        Me.tscmdClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tscmdClose.Name = "tscmdClose"
        Me.tscmdClose.Size = New System.Drawing.Size(29, 22)
        Me.tscmdClose.Text = "Exit"
        '
        'tscmdPreviewBG
        '
        Me.tscmdPreviewBG.AutoToolTip = False
        Me.tscmdPreviewBG.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tscmdPreviewBG.Image = CType(resources.GetObject("tscmdPreviewBG.Image"), System.Drawing.Image)
        Me.tscmdPreviewBG.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tscmdPreviewBG.Name = "tscmdPreviewBG"
        Me.tscmdPreviewBG.Size = New System.Drawing.Size(52, 22)
        Me.tscmdPreviewBG.Text = "Preview"
        '
        'tscmdSaveItems
        '
        Me.tscmdSaveItems.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tscmdSaveItems.Image = CType(resources.GetObject("tscmdSaveItems.Image"), System.Drawing.Image)
        Me.tscmdSaveItems.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tscmdSaveItems.Name = "tscmdSaveItems"
        Me.tscmdSaveItems.Size = New System.Drawing.Size(35, 22)
        Me.tscmdSaveItems.Text = "Save"
        Me.tscmdSaveItems.ToolTipText = "Save items to the users registry"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(6, 25)
        '
        'tscmdSettings
        '
        Me.tscmdSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tscmdSettings.AutoToolTip = False
        Me.tscmdSettings.BackColor = System.Drawing.SystemColors.Control
        Me.tscmdSettings.CheckOnClick = True
        Me.tscmdSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tscmdSettings.Image = CType(resources.GetObject("tscmdSettings.Image"), System.Drawing.Image)
        Me.tscmdSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tscmdSettings.Name = "tscmdSettings"
        Me.tscmdSettings.Size = New System.Drawing.Size(53, 22)
        Me.tscmdSettings.Text = "Settings"
        '
        'pbMainBackground
        '
        Me.pbMainBackground.Location = New System.Drawing.Point(432, 258)
        Me.pbMainBackground.Name = "pbMainBackground"
        Me.pbMainBackground.Size = New System.Drawing.Size(100, 50)
        Me.pbMainBackground.TabIndex = 15
        Me.pbMainBackground.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbItems)
        Me.Panel1.Controls.Add(Me.txtBox1)
        Me.Panel1.Controls.Add(Me.txtBox3)
        Me.Panel1.Location = New System.Drawing.Point(48, 172)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(249, 310)
        Me.Panel1.TabIndex = 16
        '
        'ofdOpenBackground
        '
        Me.ofdOpenBackground.FileName = "*.*"
        Me.ofdOpenBackground.Title = "Open Background"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(13, 22)
        '
        'plSettings
        '
        Me.plSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plSettings.Controls.Add(Me.Label3)
        Me.plSettings.Controls.Add(Me.cmdOpenSourceBackground)
        Me.plSettings.Controls.Add(Me.txtOpenBackgroundFileName)
        Me.plSettings.Controls.Add(Me.Label2)
        Me.plSettings.Controls.Add(Me.cbCoordinatesMode)
        Me.plSettings.Controls.Add(Me.cmdSavedUserBackground)
        Me.plSettings.Controls.Add(Me.Label1)
        Me.plSettings.Controls.Add(Me.txtSaveWallpaper)
        Me.plSettings.Location = New System.Drawing.Point(460, 46)
        Me.plSettings.Name = "plSettings"
        Me.plSettings.Size = New System.Drawing.Size(544, 122)
        Me.plSettings.TabIndex = 17
        Me.plSettings.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(117, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Source backgound file:"
        '
        'cmdOpenSourceBackground
        '
        Me.cmdOpenSourceBackground.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdOpenSourceBackground.Location = New System.Drawing.Point(476, 21)
        Me.cmdOpenSourceBackground.Name = "cmdOpenSourceBackground"
        Me.cmdOpenSourceBackground.Size = New System.Drawing.Size(60, 22)
        Me.cmdOpenSourceBackground.TabIndex = 6
        Me.cmdOpenSourceBackground.Text = "Open..."
        Me.cmdOpenSourceBackground.UseVisualStyleBackColor = True
        Me.cmdOpenSourceBackground.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Text location on screen"
        '
        'cbCoordinatesMode
        '
        Me.cbCoordinatesMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCoordinatesMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbCoordinatesMode.Items.AddRange(New Object() {"Location", "Percent", "Invert"})
        Me.cbCoordinatesMode.Location = New System.Drawing.Point(130, 91)
        Me.cbCoordinatesMode.Name = "cbCoordinatesMode"
        Me.cbCoordinatesMode.Size = New System.Drawing.Size(106, 21)
        Me.cbCoordinatesMode.TabIndex = 3
        '
        'cmdSavedUserBackground
        '
        Me.cmdSavedUserBackground.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdSavedUserBackground.Location = New System.Drawing.Point(476, 63)
        Me.cmdSavedUserBackground.Name = "cmdSavedUserBackground"
        Me.cmdSavedUserBackground.Size = New System.Drawing.Size(60, 22)
        Me.cmdSavedUserBackground.TabIndex = 2
        Me.cmdSavedUserBackground.Text = "File..."
        Me.cmdSavedUserBackground.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "User background file"
        '
        'txtSaveWallpaper
        '
        Me.txtSaveWallpaper.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtSaveWallpaper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSaveWallpaper.Location = New System.Drawing.Point(8, 64)
        Me.txtSaveWallpaper.Name = "txtSaveWallpaper"
        Me.txtSaveWallpaper.ReadOnly = True
        Me.txtSaveWallpaper.Size = New System.Drawing.Size(466, 20)
        Me.txtSaveWallpaper.TabIndex = 0
        '
        'lblScreenPos
        '
        Me.lblScreenPos.AutoSize = True
        Me.lblScreenPos.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScreenPos.Location = New System.Drawing.Point(455, 207)
        Me.lblScreenPos.Name = "lblScreenPos"
        Me.lblScreenPos.Size = New System.Drawing.Size(144, 18)
        Me.lblScreenPos.TabIndex = 18
        Me.lblScreenPos.Text = "{ X=9999, Y=9999 }"
        Me.lblScreenPos.Visible = False
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(12, 34)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(97, 18)
        Me.lblVersion.TabIndex = 19
        Me.lblVersion.Text = "beta version:"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblVersion.Visible = False
        '
        'pbBackground
        '
        Me.pbBackground.Location = New System.Drawing.Point(432, 332)
        Me.pbBackground.Name = "pbBackground"
        Me.pbBackground.Size = New System.Drawing.Size(413, 166)
        Me.pbBackground.TabIndex = 20
        Me.pbBackground.TabStop = False
        Me.pbBackground.Visible = False
        '
        'lblLiveScreenPos
        '
        Me.lblLiveScreenPos.AutoSize = True
        Me.lblLiveScreenPos.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLiveScreenPos.Location = New System.Drawing.Point(632, 444)
        Me.lblLiveScreenPos.Name = "lblLiveScreenPos"
        Me.lblLiveScreenPos.Size = New System.Drawing.Size(56, 18)
        Me.lblLiveScreenPos.TabIndex = 23
        Me.lblLiveScreenPos.Text = "Label5"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1094, 520)
        Me.Controls.Add(Me.lblLiveScreenPos)
        Me.Controls.Add(Me.pbBackground)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblScreenPos)
        Me.Controls.Add(Me.plSettings)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pbMainBackground)
        Me.Controls.Add(Me.tsMain)
        Me.Controls.Add(Me.plResize)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmMain"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.cmsItems.ResumeLayout(False)
        Me.plResize.ResumeLayout(False)
        CType(Me.nudResize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tsMain.ResumeLayout(False)
        Me.tsMain.PerformLayout()
        CType(Me.pbMainBackground, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.plSettings.ResumeLayout(False)
        Me.plSettings.PerformLayout()
        CType(Me.pbBackground, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbItems As System.Windows.Forms.ListBox
    Friend WithEvents cmsItems As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NamethingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtBox1 As System.Windows.Forms.TextBox
    Friend WithEvents txtOpenBackgroundFileName As System.Windows.Forms.TextBox
    Friend WithEvents FontToolStripMenuItem As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FontToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents ColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents txtBox3 As System.Windows.Forms.TextBox
    Friend WithEvents SizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents plResize As System.Windows.Forms.Panel
    Friend WithEvents cmdResize As System.Windows.Forms.Button
    Friend WithEvents nudResize As System.Windows.Forms.NumericUpDown
    Friend WithEvents AlignRightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AlignLeftToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pbMainBackground As System.Windows.Forms.PictureBox
    Friend WithEvents tscbListItems As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents tscmdAddItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents tscmdSaveItems As System.Windows.Forms.ToolStripButton
    Friend WithEvents tscmdOpenBG As System.Windows.Forms.ToolStripButton
    Friend WithEvents tscmdPreviewBG As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ofdOpenBackground As System.Windows.Forms.OpenFileDialog
    Friend WithEvents tscmdHide As System.Windows.Forms.ToolStripButton
    Friend WithEvents tscmdShow As System.Windows.Forms.ToolStripButton
    Friend WithEvents AlignCenterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tscmdClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsMain As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents sfdSaveColorBackground As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tscmdFontColor As System.Windows.Forms.ToolStripButton
    Friend WithEvents tscbFontFamilies As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents tscbFontSize As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents tscmdFontBold As System.Windows.Forms.ToolStripButton
    Friend WithEvents tscmdFontItalic As System.Windows.Forms.ToolStripButton
    Friend WithEvents tscmdFontUnderline As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsMenu As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ChangeFontAllItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeColorAllItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tslblBackground As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents CreateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ApplyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tscmdSettings As System.Windows.Forms.ToolStripButton
    Friend WithEvents plSettings As System.Windows.Forms.Panel
    Friend WithEvents cmdSavedUserBackground As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSaveWallpaper As System.Windows.Forms.TextBox
    Friend WithEvents lblScreenPos As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbCoordinatesMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdOpenSourceBackground As System.Windows.Forms.Button
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents pbBackground As System.Windows.Forms.PictureBox
    Friend WithEvents lblLiveScreenPos As System.Windows.Forms.Label

End Class
