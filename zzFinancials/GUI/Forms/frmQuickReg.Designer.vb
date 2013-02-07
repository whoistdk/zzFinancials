<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuickReg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQuickReg))
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.txtStamp = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtLabel = New System.Windows.Forms.TextBox()
        Me.bCreate = New System.Windows.Forms.Button()
        Me.bCancel = New System.Windows.Forms.Button()
        Me.bLabel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtDesc
        '
        Me.txtDesc.BackColor = System.Drawing.Color.Black
        Me.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc.ForeColor = System.Drawing.Color.White
        Me.txtDesc.Location = New System.Drawing.Point(91, 24)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(185, 20)
        Me.txtDesc.TabIndex = 0
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.Black
        Me.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount.ForeColor = System.Drawing.Color.White
        Me.txtAmount.Location = New System.Drawing.Point(90, 50)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(133, 20)
        Me.txtAmount.TabIndex = 1
        '
        'txtStamp
        '
        Me.txtStamp.BackColor = System.Drawing.Color.Black
        Me.txtStamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStamp.ForeColor = System.Drawing.Color.White
        Me.txtStamp.Location = New System.Drawing.Point(90, 102)
        Me.txtStamp.Name = "txtStamp"
        Me.txtStamp.Size = New System.Drawing.Size(186, 20)
        Me.txtStamp.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Silver
        Me.Label4.Location = New System.Drawing.Point(54, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Title"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Silver
        Me.Label5.Location = New System.Drawing.Point(68, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "€"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.Silver
        Me.Label6.Location = New System.Drawing.Point(229, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Euro"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.ForeColor = System.Drawing.Color.Silver
        Me.Label7.Location = New System.Drawing.Point(23, 104)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Timestamp"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.ForeColor = System.Drawing.Color.Silver
        Me.Label8.Location = New System.Drawing.Point(48, 78)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Label"
        '
        'txtLabel
        '
        Me.txtLabel.BackColor = System.Drawing.Color.Black
        Me.txtLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLabel.ForeColor = System.Drawing.Color.White
        Me.txtLabel.Location = New System.Drawing.Point(90, 76)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(133, 20)
        Me.txtLabel.TabIndex = 2
        '
        'bCreate
        '
        Me.bCreate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.bCreate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.bCreate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.bCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bCreate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.bCreate.Image = CType(resources.GetObject("bCreate.Image"), System.Drawing.Image)
        Me.bCreate.Location = New System.Drawing.Point(259, 171)
        Me.bCreate.Name = "bCreate"
        Me.bCreate.Size = New System.Drawing.Size(29, 29)
        Me.bCreate.TabIndex = 5
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
        Me.bCancel.Location = New System.Drawing.Point(224, 171)
        Me.bCancel.Name = "bCancel"
        Me.bCancel.Size = New System.Drawing.Size(29, 29)
        Me.bCancel.TabIndex = 6
        Me.bCancel.UseVisualStyleBackColor = True
        '
        'bLabel
        '
        Me.bLabel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.bLabel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.bLabel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.bLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.bLabel.Image = CType(resources.GetObject("bLabel.Image"), System.Drawing.Image)
        Me.bLabel.Location = New System.Drawing.Point(232, 70)
        Me.bLabel.Name = "bLabel"
        Me.bLabel.Size = New System.Drawing.Size(29, 29)
        Me.bLabel.TabIndex = 3
        Me.bLabel.UseVisualStyleBackColor = True
        '
        'frmQuickReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(296, 205)
        Me.Controls.Add(Me.bCreate)
        Me.Controls.Add(Me.bCancel)
        Me.Controls.Add(Me.txtLabel)
        Me.Controls.Add(Me.txtStamp)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.bLabel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmQuickReg"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Quick Register"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtStamp As System.Windows.Forms.TextBox
    Friend WithEvents bLabel As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Friend WithEvents bCancel As System.Windows.Forms.Button
    Friend WithEvents bCreate As System.Windows.Forms.Button
End Class
