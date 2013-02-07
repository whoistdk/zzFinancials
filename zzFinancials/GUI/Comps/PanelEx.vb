Public Class PanelEx
    Inherits Panel

    Public Sub New()
        Me.DoubleBuffered = True
        Me.SetStyle(
ControlStyles.AllPaintingInWmPaint Or _
ControlStyles.UserPaint Or _
ControlStyles.DoubleBuffer, True)
        InitializeComponent()
    End Sub


End Class