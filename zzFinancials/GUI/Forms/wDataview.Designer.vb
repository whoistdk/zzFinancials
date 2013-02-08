<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wDataview
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wDataview))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.tslStatus = New System.Windows.Forms.ToolStripLabel()
        Me.tUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.pnlLogbook = New System.Windows.Forms.Panel()
        Me.cbLBFolder = New System.Windows.Forms.ComboBox()
        Me.bCreate = New System.Windows.Forms.Button()
        Me.bCancel = New System.Windows.Forms.Button()
        Me.txtLBName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlQReg = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbLabel = New System.Windows.Forms.ComboBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.txtTimeDate = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tFeed = New System.Windows.Forms.Timer(Me.components)
        Me.pnlGraphCust = New zzFinancials.PictureboxEx(Me.components)
        Me.pnlGraph = New zzFinancials.PictureboxEx(Me.components)
        Me.pnlView = New zzFinancials.PictureboxEx(Me.components)
        Me.PictureboxEx2 = New zzFinancials.PictureboxEx(Me.components)
        Me.ToolStrip1.SuspendLayout()
        Me.pnlLogbook.SuspendLayout()
        Me.pnlQReg.SuspendLayout()
        CType(Me.pnlGraphCust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureboxEx2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton3, Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripTextBox1, Me.tslStatus})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(906, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "Quick Register"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton3.Text = "Quick Register"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "Clear searchbox"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "Start Search"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.ToolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolStripTextBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(92, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(100, 25)
        Me.ToolStripTextBox1.Text = "Search..."
        '
        'tslStatus
        '
        Me.tslStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tslStatus.ForeColor = System.Drawing.Color.Gray
        Me.tslStatus.Name = "tslStatus"
        Me.tslStatus.Size = New System.Drawing.Size(26, 22)
        Me.tslStatus.Text = "Idle"
        Me.tslStatus.ToolTipText = "Idle"
        '
        'tUpdate
        '
        Me.tUpdate.Enabled = True
        Me.tUpdate.Interval = 1
        '
        'pnlLogbook
        '
        Me.pnlLogbook.Controls.Add(Me.cbLBFolder)
        Me.pnlLogbook.Controls.Add(Me.bCreate)
        Me.pnlLogbook.Controls.Add(Me.bCancel)
        Me.pnlLogbook.Controls.Add(Me.txtLBName)
        Me.pnlLogbook.Controls.Add(Me.Label3)
        Me.pnlLogbook.Controls.Add(Me.Label2)
        Me.pnlLogbook.Controls.Add(Me.Label1)
        Me.pnlLogbook.ForeColor = System.Drawing.Color.Silver
        Me.pnlLogbook.Location = New System.Drawing.Point(209, 163)
        Me.pnlLogbook.Name = "pnlLogbook"
        Me.pnlLogbook.Size = New System.Drawing.Size(283, 174)
        Me.pnlLogbook.TabIndex = 4
        '
        'cbLBFolder
        '
        Me.cbLBFolder.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.cbLBFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbLBFolder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cbLBFolder.FormattingEnabled = True
        Me.cbLBFolder.Location = New System.Drawing.Point(86, 85)
        Me.cbLBFolder.Name = "cbLBFolder"
        Me.cbLBFolder.Size = New System.Drawing.Size(181, 21)
        Me.cbLBFolder.TabIndex = 9
        '
        'bCreate
        '
        Me.bCreate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.bCreate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.bCreate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.bCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bCreate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.bCreate.Image = CType(resources.GetObject("bCreate.Image"), System.Drawing.Image)
        Me.bCreate.Location = New System.Drawing.Point(191, 124)
        Me.bCreate.Name = "bCreate"
        Me.bCreate.Size = New System.Drawing.Size(29, 29)
        Me.bCreate.TabIndex = 7
        Me.bCreate.UseVisualStyleBackColor = True
        '
        'bCancel
        '
        Me.bCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.bCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.bCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.bCancel.Image = CType(resources.GetObject("bCancel.Image"), System.Drawing.Image)
        Me.bCancel.Location = New System.Drawing.Point(156, 124)
        Me.bCancel.Name = "bCancel"
        Me.bCancel.Size = New System.Drawing.Size(29, 29)
        Me.bCancel.TabIndex = 8
        Me.bCancel.UseVisualStyleBackColor = True
        '
        'txtLBName
        '
        Me.txtLBName.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.txtLBName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtLBName.Location = New System.Drawing.Point(86, 52)
        Me.txtLBName.Name = "txtLBName"
        Me.txtLBName.Size = New System.Drawing.Size(181, 20)
        Me.txtLBName.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Gray
        Me.Label3.Location = New System.Drawing.Point(44, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Folder"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Gray
        Me.Label2.Location = New System.Drawing.Point(45, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(13, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "New Logbook"
        '
        'pnlQReg
        '
        Me.pnlQReg.Controls.Add(Me.Label9)
        Me.pnlQReg.Controls.Add(Me.cbLabel)
        Me.pnlQReg.Controls.Add(Me.Button4)
        Me.pnlQReg.Controls.Add(Me.Button3)
        Me.pnlQReg.Controls.Add(Me.txtTimeDate)
        Me.pnlQReg.Controls.Add(Me.Button1)
        Me.pnlQReg.Controls.Add(Me.Button2)
        Me.pnlQReg.Controls.Add(Me.txtDesc)
        Me.pnlQReg.Controls.Add(Me.txtAmount)
        Me.pnlQReg.Controls.Add(Me.Label6)
        Me.pnlQReg.ForeColor = System.Drawing.Color.Silver
        Me.pnlQReg.Location = New System.Drawing.Point(498, 163)
        Me.pnlQReg.Name = "pnlQReg"
        Me.pnlQReg.Size = New System.Drawing.Size(375, 280)
        Me.pnlQReg.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.ForeColor = System.Drawing.Color.Silver
        Me.Label9.Location = New System.Drawing.Point(251, 51)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(13, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "€"
        '
        'cbLabel
        '
        Me.cbLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.cbLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cbLabel.FormattingEnabled = True
        Me.cbLabel.Location = New System.Drawing.Point(33, 113)
        Me.cbLabel.Name = "cbLabel"
        Me.cbLabel.Size = New System.Drawing.Size(215, 21)
        Me.cbLabel.TabIndex = 15
        '
        'Button4
        '
        Me.Button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.ForeColor = System.Drawing.Color.Silver
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.Location = New System.Drawing.Point(254, 145)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(21, 21)
        Me.Button4.TabIndex = 14
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.ForeColor = System.Drawing.Color.Silver
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.Location = New System.Drawing.Point(254, 113)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(21, 21)
        Me.Button3.TabIndex = 13
        Me.Button3.UseVisualStyleBackColor = True
        '
        'txtTimeDate
        '
        Me.txtTimeDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.txtTimeDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTimeDate.Location = New System.Drawing.Point(33, 146)
        Me.txtTimeDate.Name = "txtTimeDate"
        Me.txtTimeDate.Size = New System.Drawing.Size(215, 20)
        Me.txtTimeDate.TabIndex = 11
        '
        'Button1
        '
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(189, 196)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(29, 29)
        Me.Button1.TabIndex = 7
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(154, 196)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(29, 29)
        Me.Button2.TabIndex = 8
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtDesc
        '
        Me.txtDesc.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.txtDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtDesc.Location = New System.Drawing.Point(33, 80)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(215, 20)
        Me.txtDesc.TabIndex = 4
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.txtAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtAmount.Location = New System.Drawing.Point(33, 48)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(215, 20)
        Me.txtAmount.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Gray
        Me.Label6.Location = New System.Drawing.Point(13, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 16)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Quick Register"
        '
        'tFeed
        '
        Me.tFeed.Enabled = True
        '
        'pnlGraphCust
        '
        Me.pnlGraphCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pnlGraphCust.Location = New System.Drawing.Point(209, 394)
        Me.pnlGraphCust.Name = "pnlGraphCust"
        Me.pnlGraphCust.Size = New System.Drawing.Size(283, 45)
        Me.pnlGraphCust.TabIndex = 7
        Me.pnlGraphCust.TabStop = False
        '
        'pnlGraph
        '
        Me.pnlGraph.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pnlGraph.Location = New System.Drawing.Point(209, 343)
        Me.pnlGraph.Name = "pnlGraph"
        Me.pnlGraph.Size = New System.Drawing.Size(283, 45)
        Me.pnlGraph.TabIndex = 6
        Me.pnlGraph.TabStop = False
        '
        'pnlView
        '
        Me.pnlView.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pnlView.Location = New System.Drawing.Point(209, 43)
        Me.pnlView.Name = "pnlView"
        Me.pnlView.Size = New System.Drawing.Size(387, 100)
        Me.pnlView.TabIndex = 1
        Me.pnlView.TabStop = False
        '
        'PictureboxEx2
        '
        Me.PictureboxEx2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.PictureboxEx2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureboxEx2.Location = New System.Drawing.Point(0, 25)
        Me.PictureboxEx2.Name = "PictureboxEx2"
        Me.PictureboxEx2.Size = New System.Drawing.Size(185, 451)
        Me.PictureboxEx2.TabIndex = 2
        Me.PictureboxEx2.TabStop = False
        '
        'wDataview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(906, 476)
        Me.Controls.Add(Me.pnlGraphCust)
        Me.Controls.Add(Me.pnlGraph)
        Me.Controls.Add(Me.pnlQReg)
        Me.Controls.Add(Me.pnlLogbook)
        Me.Controls.Add(Me.pnlView)
        Me.Controls.Add(Me.PictureboxEx2)
        Me.Controls.Add(Me.ToolStrip1)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Name = "wDataview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "zzFinancials - Dataview"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlLogbook.ResumeLayout(False)
        Me.pnlLogbook.PerformLayout()
        Me.pnlQReg.ResumeLayout(False)
        Me.pnlQReg.PerformLayout()
        CType(Me.pnlGraphCust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlGraph, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureboxEx2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents pnlView As zzFinancials.PictureboxEx
    Friend WithEvents tUpdate As System.Windows.Forms.Timer
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents PictureboxEx2 As zzFinancials.PictureboxEx
    Friend WithEvents pnlLogbook As System.Windows.Forms.Panel
    Friend WithEvents txtLBName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents bCreate As System.Windows.Forms.Button
    Friend WithEvents bCancel As System.Windows.Forms.Button
    Friend WithEvents pnlQReg As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbLBFolder As System.Windows.Forms.ComboBox
    Friend WithEvents pnlGraph As zzFinancials.PictureboxEx
    Friend WithEvents tslStatus As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tFeed As System.Windows.Forms.Timer
    Friend WithEvents cbLabel As System.Windows.Forms.ComboBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents txtTimeDate As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pnlGraphCust As zzFinancials.PictureboxEx
End Class
