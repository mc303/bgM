<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreview
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
        Me.pbBackground = New System.Windows.Forms.PictureBox()
        Me.lblScreenPos = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        CType(Me.pbBackground, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbBackground
        '
        Me.pbBackground.Location = New System.Drawing.Point(87, 95)
        Me.pbBackground.Name = "pbBackground"
        Me.pbBackground.Size = New System.Drawing.Size(100, 50)
        Me.pbBackground.TabIndex = 0
        Me.pbBackground.TabStop = False
        '
        'lblScreenPos
        '
        Me.lblScreenPos.AutoSize = True
        Me.lblScreenPos.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScreenPos.Location = New System.Drawing.Point(25, 24)
        Me.lblScreenPos.Name = "lblScreenPos"
        Me.lblScreenPos.Size = New System.Drawing.Size(56, 18)
        Me.lblScreenPos.TabIndex = 1
        Me.lblScreenPos.Text = "Label1"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(34, 74)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(101, 18)
        Me.lblVersion.TabIndex = 20
        Me.lblVersion.Text = " beta version:"
        Me.lblVersion.Visible = False
        '
        'frmPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblScreenPos)
        Me.Controls.Add(Me.pbBackground)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPreview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Background Preview"
        Me.TopMost = True
        CType(Me.pbBackground, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbBackground As System.Windows.Forms.PictureBox
    Friend WithEvents lblScreenPos As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
End Class
