Public Class fGraph
    Public DisableInput As Boolean = False
    Public Graph As New GraphEx
    Private Sub fGraph_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Graph.InitTest()
        Graph.SetView(Me.ClientRectangle)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Graph.Update()
        Me.Invalidate()
    End Sub

    Private Sub fGraph_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        Graph.AddRndNode()
    End Sub

    Private Sub fGraph_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Graph.Render(e.Graphics)
    End Sub

 
    Private Sub fGraph_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Graph.SetView(Me.ClientRectangle)
    End Sub
End Class