Public Class frmQuickReg
    Public DisableInput As Boolean = False
    Public Sub SetLabel(ByVal nLabel As String, ByVal nColor As Color)
        txtLabel.Text = nLabel
        txtLabel.ForeColor = nColor
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bLabel.Click
        '    Kernell.SF.SwitchInst("Cat")
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frmQuickReg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ClearForm()
    End Sub
    Public Sub ClearForm()
        txtStamp.Text = Date.Now.ToString()

    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Kernell.SF.SwitchInst("Quick", True)
    End Sub

    Private Sub bCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bCreate.Click

        'DM.AddData({txtStamp.Text, txtAmount.Text, txtDesc.Text, txtLabel.Text, "0"})


        Kernell.DM.TryParse(txtStamp.Text & "|" & txtAmount.Text & "|" & txtDesc.Text & "|" & txtLabel.Text & "|" & "0")
        Kernell.DM.ToFile(Kernell.pthBaseAcc & "Logbook001.lbk")

        Me.Close()
        ' Kernell.SF.SwitchInst("Main", True)

    End Sub

    Private Sub bCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bCancel.Click
        'Kernell.SF.SwitchInst("Main", True)
    End Sub
End Class