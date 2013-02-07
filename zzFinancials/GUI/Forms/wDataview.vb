Public Class wDataview
    Private lstQuery As String = ""
    Private QueryInfo As String = "Search..."
    Private WithEvents TreeMenu As TreeMenu
    Private CMover As New ControlMover
    Private MemGraph As New GraphEx

    Private QRInf_Amount As String = "Type an Amount"
    Private QRUsr_Amount As String
    Private QRInf_Desc As String = "Type a Description (Not required)"
    Private QRUsr_Desc As String
    Private QRInf_Label As String = "Select or type a Label"
    Private QRUsr_Label As String

    Private LBInf_Name As String = "Logbook Name"
    Private LBUsr_Name As String
    Private LBInf_Folder As String = "Sub Folder (Not required)"
    Private LBUsr_Folder As String



    Private Sub wDataview_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        TreeMenu = New TreeMenu("zzFinancials")
        TreeMenu.TryParse(Kernell.Rebuild_LocalTree())

        UpdOnResize()
        Kernell.DP.SetRGN(pnlView.ClientRectangle)

        CMover.AddControl(pnlView, "Viewer")
        CMover.AddControl(pnlGraph, "Graph")
        CMover.AddControl(pnlLogbook, "Logbooks")
        CMover.AddControl(pnlQReg, "QuickReg")
        'CMover.AddControl(PictureboxEx2, "Menu")
        CMover.SwitchControl("Viewer")

        MemGraph.SetView(pnlGraph.ClientRectangle)
        MemGraph.V.Title = "Memory Usage"
        MemGraph.V.UnitTitle = "in Megabytes"
        MemGraph.H.Title = "Time Elapsed"
        MemGraph.H.UnitTitle = "per 100 ms"

        Dim clrtbl = New DarkColorTable
        ToolStripManager.Renderer = New ToolStripProfessionalRenderer(clrtbl)
        '    StatusStrip1.Renderer = New ToolStripProfessionalRenderer(clrtbl)
        txtAmount.Text = QRInf_Amount
        txtDesc.Text = QRInf_Desc
        cbLabel.Text = QRInf_Label

    End Sub

    Private Sub tUpdate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tUpdate.Tick
        CMover.Update()
        MemGraph.Update()

        TreeMenu.Rgn = New Rectangle(PictureboxEx2.ClientRectangle.X, PictureboxEx2.ClientRectangle.Y, PictureboxEx2.ClientRectangle.Width - 16, PictureboxEx2.ClientRectangle.Height - 16)
        TreeMenu.Update()
        Kernell.DP.Update()
        If Not tslStatus.Text = Kernell.DP.sStatus Then tslStatus.Text = Kernell.DP.sStatus

        MemGraph.SetView(pnlGraph.ClientRectangle)
        pnlView.Invalidate()
        pnlGraph.Invalidate()
        PictureboxEx2.Invalidate()
    End Sub

    Private Sub PictureboxEx1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlView.Click

    End Sub

    Private Sub PictureboxEx1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlView.MouseDown
        Kernell.DP.Inf_Ms_Down(e.Location)
    End Sub

    Private Sub PictureboxEx1_MouseLeave(sender As Object, e As EventArgs) Handles pnlView.MouseLeave
        Kernell.DP.Inf_Ms_Move(New Point(-1, -1))
        Kernell.DP.Inf_Ms_Up(New Point(-1, -1))
    End Sub

    Private Sub PictureboxEx1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlView.MouseMove
        Kernell.DP.Inf_Ms_Move(e.Location)
    End Sub

    Private Sub PictureboxEx1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlView.MouseUp
        Kernell.DP.Inf_Ms_Up(e.Location)
    End Sub

    Private Sub PictureboxEx1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlView.MouseWheel
        Kernell.DP.Inf_Ms_Whl(e.Delta)
    End Sub

    Private Sub PictureboxEx1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlView.Paint
        Kernell.DP.Render(e.Graphics)
        '   TestAni.Render(e.Graphics)
    End Sub

    Private Sub ToolStripTextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripTextBox1.Click

    End Sub

    Private Sub ToolStripTextBox1_GotFocus(sender As Object, e As EventArgs) Handles ToolStripTextBox1.GotFocus
        If ToolStripTextBox1.Text = QueryInfo Then
            ToolStripTextBox1.Text = lstQuery
        End If
        ToolStripTextBox1.BackColor = Color.FromArgb(255, 0, 0, 0)
        ToolStripTextBox1.ForeColor = Color.FromArgb(255, 200, 200, 200)
    End Sub

    Private Sub ToolStripTextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ToolStripTextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            With Kernell.DP
                .SearchField = "*"
                'If ToolStripTextBox1.Text <> SearchBoxInfo Then
                .SearchText = ToolStripTextBox1.Text
                'Else
                '    .SearchText = ""
                'End If
                .PostQuery()
            End With
        End If
    End Sub

    Private Sub wDataview_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Kernell.SetSet(Kernell.AccountName, "dataview_win_x", Me.Location.X)
        Kernell.SetSet(Kernell.AccountName, "dataview_win_y", Me.Location.Y)
        Kernell.SetSet(Kernell.AccountName, "dataview_win_w", Me.Size.Width)
        Kernell.SetSet(Kernell.AccountName, "dataview_win_h", Me.Size.Height)


    End Sub



    Public Sub UpdOnResize()
        Kernell.DP.SetRGN(pnlView.ClientRectangle)
        Kernell.DP.OnResizeEnd()

        CMover.RgnHidden = New Rectangle(Me.ClientRectangle.X + Me.ClientRectangle.Width, Me.ClientRectangle.Y + ToolStrip1.Height, Me.ClientRectangle.Width - PictureboxEx2.Width, Me.ClientRectangle.Height - ToolStrip1.Height)
        CMover.RgnHideSEQ = New Rectangle(Me.ClientRectangle.X + Me.ClientRectangle.Width, Me.ClientRectangle.Y + ToolStrip1.Height, Me.ClientRectangle.Width - PictureboxEx2.Width, Me.ClientRectangle.Height - ToolStrip1.Height)
        CMover.RgnShown = New Rectangle(Me.ClientRectangle.X + PictureboxEx2.Width, Me.ClientRectangle.Y + ToolStrip1.Height, Me.ClientRectangle.Width - PictureboxEx2.Width, Me.ClientRectangle.Height - ToolStrip1.Height)
        CMover.RgnShowSEQ = New Rectangle(Me.ClientRectangle.X + Me.ClientRectangle.Width, Me.ClientRectangle.Y + ToolStrip1.Height, Me.ClientRectangle.Width - PictureboxEx2.Width, Me.ClientRectangle.Height - ToolStrip1.Height)
        'CMover.RgnShowSEQ = New Rectangle(Me.ClientRectangle.X + PictureboxEx2.Width, Me.ClientRectangle.Y + ToolStrip1.Height, Me.ClientRectangle.Width - PictureboxEx2.Width, Me.ClientRectangle.Height - ToolStrip1.Height)
        MemGraph.SetView(pnlGraph.ClientRectangle)
    End Sub


    Private Sub wDataview_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        UpdOnResize()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Dim f As New frmQuickReg
        f.ShowDialog()
    End Sub

    Private Sub ToolStripTextBox1_LostFocus(sender As Object, e As EventArgs) Handles ToolStripTextBox1.LostFocus
        lstQuery = ToolStripTextBox1.Text
        If lstQuery = "" Then
            ToolStripTextBox1.Text = QueryInfo

        End If
        ToolStripTextBox1.BackColor = Color.FromArgb(255, 32, 32, 32)
        ToolStripTextBox1.ForeColor = Color.FromArgb(255, 92, 92, 92)
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        With Kernell.DP
            .SearchField = "*"
            'If ToolStripTextBox1.Text <> SearchBoxInfo Then
            .SearchText = lstQuery
            'Else
            '    .SearchText = ""
            'End If
            .PostQuery()
        End With
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        lstQuery = ""
        ToolStripTextBox1.Text = ""
        ToolStripTextBox1.Text = QueryInfo
        With Kernell.DP
            .SearchField = "*"
            'If ToolStripTextBox1.Text <> SearchBoxInfo Then
            .SearchText = lstQuery
            'Else
            '    .SearchText = ""
            'End If
            .PostQuery()
        End With
    End Sub

    Private Sub PictureboxEx2_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureboxEx2.MouseDown
        TreeMenu.Inf_Ms_Down(e.Location)
    End Sub

    Private Sub PictureboxEx2_Paint(sender As Object, e As PaintEventArgs) Handles PictureboxEx2.Paint
        TreeMenu.RenderR(e.Graphics)
    End Sub

    Private Sub PictureboxEx2_Click(sender As Object, e As EventArgs) Handles PictureboxEx2.Click

    End Sub

    Private Sub PictureboxEx2_MouseEnter(sender As Object, e As EventArgs) Handles PictureboxEx2.MouseEnter

    End Sub

    Private Sub PictureboxEx2_MouseLeave(sender As Object, e As EventArgs) Handles PictureboxEx2.MouseLeave

    End Sub

    Private Sub PictureboxEx2_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureboxEx2.MouseUp
        TreeMenu.Inf_Ms_Up(e.Location)
    End Sub

    Private Sub PictureboxEx2_MouseWheel(sender As Object, e As MouseEventArgs) Handles PictureboxEx2.MouseWheel
        TreeMenu.Inf_Ms_Whl(e.Delta)
    End Sub

    Private Sub PictureboxEx2_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureboxEx2.MouseMove
        TreeMenu.Inf_Ms_Move(e.Location)
    End Sub

    Private Sub GraphToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GraphToolStripMenuItem.Click
        'CMover.SwitchControl("Logbooks")
        'Dim AppDiag As New AppDiag
        'AppDiag.Update()
    End Sub

    Private Sub pnlLogbook_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlLogbook.MouseUp
        CMover.SwitchControl("Viewer")
    End Sub

    Private Sub pnlLogbook_Paint(sender As Object, e As PaintEventArgs) Handles pnlLogbook.Paint

    End Sub

    Private Sub wDataview_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' UpdOnResize()
    End Sub

    Private Sub PictureboxEx1_SizeChanged(sender As Object, e As EventArgs) Handles pnlView.SizeChanged
        UpdOnResize()
    End Sub

    Private Sub TreeMenu_ItemClicked(Item As TreeMenuItem) Handles TreeMenu.ItemClicked
        Select Case Item.Tag
            Case "quickreg"
                CMover.SwitchControl("QuickReg")
            Case "newlb"
                CMover.SwitchControl("Logbooks")
            Case "tool:memgraph"
                CMover.SwitchControl("Graph")
            Case "tool:datagen"
                Dim f As New fDevTools
                f.Show()
            Case Else
                If Mid(Item.Tag, 1, 8) = "logbook:" Then
                    'Kernell.DM.PurgeData()
                    'Kernell.DP.PurgeData()

                    Kernell.DM.Dispose()
                    Kernell.DM = Nothing
                    Kernell.DM = New zzDataMgr.DataManager
                    Kernell.DP.rfDM = Kernell.DM


                    Kernell.DM.TryParse(AppKernell.DM_Header)
                    Kernell.DM.FromFile(Mid(Item.Tag, 9, Item.Tag.Length - 8))

                    '  MsgBox("Open logbook")
                End If


                'MsgBox(Item.Tag)
        End Select

    End Sub

    Private Sub pnlGraph_Paint(sender As Object, e As PaintEventArgs) Handles pnlGraph.Paint
        MemGraph.Render(e.Graphics)
    End Sub
    Dim gID As Integer = 0
    Private Sub pnlGraph_Click(sender As Object, e As EventArgs) Handles pnlGraph.Click

    End Sub

    Private Sub tFeed_Tick(sender As Object, e As EventArgs) Handles tFeed.Tick
        MemGraph.AddNode(gID, AppDiag.GetMem())

        If gID > 10000 Then
            MemGraph.Nodes.Clear()
            gID = 0
        End If

        gID += 1
    End Sub

    Private Sub txtAmount_GotFocus(sender As Object, e As EventArgs) Handles txtAmount.GotFocus
        If txtAmount.Text = QRInf_Amount Then
            txtAmount.Text = QRUsr_Amount
        End If
    End Sub

    Private Sub txtAmount_LostFocus(sender As Object, e As EventArgs) Handles txtAmount.LostFocus
        If txtAmount.Text <> QRInf_Amount Then
            QRUsr_Amount = txtAmount.Text
        End If
        If txtAmount.Text = "" Then
            txtAmount.Text = QRInf_Amount
        End If
    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged

    End Sub

    Private Sub pnlQReg_Paint(sender As Object, e As PaintEventArgs) Handles pnlQReg.Paint

    End Sub

    Private Sub txtDesc_GotFocus(sender As Object, e As EventArgs) Handles txtDesc.GotFocus
        If txtDesc.Text = QRInf_Desc Then
            txtDesc.Text = QRUsr_Desc
        End If
    End Sub

    Private Sub txtDesc_LostFocus(sender As Object, e As EventArgs) Handles txtDesc.LostFocus
        If txtDesc.Text <> QRInf_Desc Then
            QRUsr_Desc = txtDesc.Text
        End If
        If txtDesc.Text = "" Then
            txtDesc.Text = QRInf_Desc
        End If
    End Sub

    Private Sub cbLabel_GotFocus(sender As Object, e As EventArgs) Handles cbLabel.GotFocus
        If cbLabel.Text = QRInf_Label Then
            cbLabel.Text = QRUsr_Label
        End If
    End Sub

    Private Sub cbLabel_LostFocus(sender As Object, e As EventArgs) Handles cbLabel.LostFocus
        If cbLabel.Text <> QRInf_Label Then
            QRUsr_Label = cbLabel.Text
        End If
        If cbLabel.Text = "" Then
            cbLabel.Text = QRInf_Label
        End If
    End Sub
End Class