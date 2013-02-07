Public Class fCatagories
    'Private pMov As New PanelMover
    'Public DisableInput As Boolean = False
    'Private Sub fCatagories_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '    Kernell.Catagories.ToFile()
    'End Sub
    'Private Sub fCatagories_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Me.Size = New Size(285, 298)
    '    pMov.ObjPanel = Panel1
    '    pMov.rInvisible = New Rectangle(0, -Me.ClientRectangle.Height, Me.ClientRectangle.Width, Me.ClientRectangle.Height)
    '    pMov.rVisible = New Rectangle(0, 56, Me.ClientRectangle.Width, Me.ClientRectangle.Height - 56)
    '    pMov.Rgn.Setup(pMov.rInvisible, pMov.rInvisible, 1 / 8, 1 / 8, 1)

    '    Kernell.ListCatagories(ListView1)
    'End Sub

    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    pMov.Update()
    'End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    pMov.State = 1
    '    TextBox1.Focus()
    'End Sub

    'Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
    '    pMov.State = 0
    '    Dim nwC As New BaseCatagory
    '    nwC.Name = TextBox1.Text
    '    nwC.Color = PictureBox1.BackColor
    '    Kernell.Catagories.Catagories.Add(nwC)
    '    Kernell.Catagories.ToLV(ListView1)


    '    Kernell.DMCAT.TryParse(TextBox1.Text & "|" & PictureBox1.BackColor.R & "," & PictureBox1.BackColor.G & "," & PictureBox1.BackColor.B)
    '    Kernell.DMCAT.ToFile(Kernell.pthBaseAcc & "catagory.db")
    '    Kernell.ListCatagories(ListView1)

    'End Sub

    'Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    '    pMov.State = 0
    'End Sub

    'Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
    '    ColorDialog1.ShowDialog()
    '    PictureBox1.BackColor = ColorDialog1.Color
    'End Sub

    'Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click

    'End Sub


    'Public flgDlg As Boolean = True
    'Public lItem As ListViewItem

    'Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDoubleClick
    '    If lItem IsNot Nothing Then
    '        Try
    '            Dim oFrm As Object = Kernell.SF.GetInst("Quick").ObjFrm
    '            oFrm.setlabel(lItem.Text, lItem.ForeColor)
    '        Catch ex As Exception
    '            Console.WriteLine(ex.ToString)
    '        End Try
    '        Kernell.SF.SwitchInst("Quick", True)
    '    End If
    'End Sub

    'Private Sub ListView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseUp
    '    lItem = ListView1.GetItemAt(e.X, e.Y)
    'End Sub

    'Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    'End Sub
End Class