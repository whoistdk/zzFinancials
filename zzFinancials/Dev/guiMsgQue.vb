Public Class guiMsgQue
    Public RgnView As Rectangle
    Public RgnOut As New zzDynamix.CoreClasses.dynRectangle2
    Public Que As New List(Of MsgQueItm)
    Public ItemHeight As Integer = 14

    Public Sub Update()
        Dim hReq As Integer = 0
        Dim Offs As Point = RgnView.Location
        If Que.Count > 0 Then
            For q = 0 To Que.Count - 1
                With Que(q)

                    Select Case .State
                        Case MsgState.Init
                            .Alpha.Setup(0, 0)
                            .RgnOut.Setup(New Rectangle(RgnView.X, RgnView.Y + RgnOut.GetCurrent.Height, RgnView.Width, ItemHeight), New Rectangle(RgnView.X, RgnView.Y + RgnOut.GetCurrent.Height, RgnView.Width, ItemHeight))
                            .State = MsgState.FadeIn
                        Case MsgState.FadeIn
                            .Alpha.Setup(1000)
                            .RgnOut.Setup(New Rectangle(Offs.X, Offs.Y, RgnView.Width, ItemHeight))
                            If .Alpha.GetCurrent = 1000 Then .State = MsgState.AwaitTimeOut
                        Case MsgState.AwaitTimeOut
                            .RgnOut.Setup(New Rectangle(Offs.X, Offs.Y, RgnView.Width, ItemHeight))
                            If Environment.TickCount - .iTime >= .Timeout Then
                                .State = MsgState.FadeOut
                            End If
                        Case MsgState.FadeOut
                            .RgnOut.Setup(New Rectangle(Offs.X, Offs.Y, RgnView.Width, ItemHeight))
                            .Alpha.Setup(0)
                            If .Alpha.GetCurrent = 0 Then .State = MsgState.Remove
                    End Select
                    .RgnOut.Update()
                    .Alpha.Update()

                    If Not .State = MsgState.Remove And Not .State = MsgState.FadeOut Then
                        hReq += ItemHeight + 0
                        Offs.Y += ItemHeight + 0
                    End If
                End With

 
            Next
        End If
        RgnOut.Setup(New Rectangle(RgnView.X, RgnView.Y, RgnView.Width, hReq))
        RgnOut.Update()

    End Sub

    Private sysfnt As New Font("Ariel", 9, FontStyle.Regular, GraphicsUnit.Pixel)
    ' Private sysfntbr As New SolidBrush(Color.FromArgb(200, 200, 200))
    Public Sub Render(ByRef G As Graphics)
        G.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit
        If Que.Count > 0 Then
            For q = 0 To Que.Count - 1
                With Que(q)
                    If .Alpha.GetCurrent > 0 Then
                        If .RgnOut.GetCurrent.Width > 0 And .RgnOut.GetCurrent.Height > 0 Then
                            Dim br As New SolidBrush(Color.FromArgb((255 / 1000) * .Alpha.GetCurrent, Color.Black))
                            Dim brF As New SolidBrush(Color.FromArgb((255 / 1000) * .Alpha.GetCurrent, Color.Silver))
                            G.FillRectangle(br, .RgnOut.GetCurrent)
                            DrawText(G, .Msg, sysfnt, brF, StringAlignment.Near, StringAlignment.Center, .RgnOut.GetCurrent, False)
                            br.Dispose()
                            brF.Dispose()
                        End If
                    End If
                End With
            Next
        End If
    End Sub
End Class


Public Class MsgQueItm
    Public RgnOut As New zzDynamix.CoreClasses.dynRectangle2
    Public Alpha As New zzDynamix.CoreClasses.dynSingle
    Public Msg As String
    Public Tag As String
    Public Clr As Color = Color.Silver
    Public Timeout As Integer
    Public iTime As Integer
    Public State As MsgState = MsgState.Init
    Public Sub New(ByVal nMsg As String, Optional ByVal nTag As String = "", Optional ByVal nTimeout As Integer = 5000)
        RgnOut.Setup(Rectangle.Empty, Rectangle.Empty, 1 / 32, 1 / 32, 1)
        Msg = nMsg
        Tag = nTag
        Timeout = nTimeout
        iTime = Environment.TickCount
    End Sub

End Class


Public Enum MsgState
    Init = 0
    FadeIn = 1
    AwaitTimeOut = 2
    FadeOut = 3
    Remove = 4
End Enum