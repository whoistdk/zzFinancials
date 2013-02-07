Public Class GraphEx

    Public RgnView As Rectangle = New Rectangle(0, 0, 64, 64)
    Public RgnGraph As Rectangle = New Rectangle(0, 0, 32, 32)

    Public Title As String

    Public H As New GraphEx_Axis
    Public V As New GraphEx_Axis

    Public Nodes As New List(Of GraphEx_DataNode)
    Public MaxNodes As Integer = 100

    Private flgReCalc As Boolean


    Public ClrRelation As Color = Color.FromArgb(128, Color.SlateGray)
    Public ClrNode As Color = Color.FromArgb(128, 255, 255, 255)

    Public Sub SetView(ByVal nRgnView As Rectangle)
        If nRgnView.Width > 33 And nRgnView.Height > 33 Then
            RgnView = nRgnView
            flgReCalc = True
        End If

    End Sub

    Public Sub InitTest()
        Try
            With H
                .Title = "Date"
                .UnitTitle = "Entry"
            End With
            With V
                .Title = "Amount"
                .UnitTitle = "Euro"
            End With
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Public Sub Update()
        Try
            If flgReCalc Then
                CalcMinMax()
                CalcNodePositions()
                flgReCalc = False
            End If

            For n = 0 To Nodes.Count - 1
                Nodes(n).Pos.Update()
            Next

            If Nodes.Count > MaxNodes Then
                Nodes.RemoveAt(0)
            End If

            For n = 0 To Nodes.Count - 1
                Nodes(n).H.Value = n
            Next

        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub


    Private Sub CalcNodePositions()
        Try
            For n = 0 To Nodes.Count - 1
                With Nodes(n)
                    .Pos.Setup(New PointF(H.Offset + (H.Unit * .H.Value), V.Offset - (V.Unit * .V.Value)))
                End With
            Next
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Private Sub CalcMinMax()
        Try
            RgnGraph = New Rectangle(RgnView.X + 18, RgnView.Y + 18, RgnView.Width - (18 * 2), RgnView.Height - (18 * 2))


            Dim chMax As Single = 0
            Dim cvMax As Single = 0

            For n = 0 To Nodes.Count - 1
                With Nodes(n)
                    If .H.Value > chMax Then chMax = .H.Value
                    If .V.Value > cvMax Then cvMax = .V.Value
                End With
            Next
            Dim chMin As Single = chMax
            Dim cvMin As Single = cvMax
            For n = 0 To Nodes.Count - 1
                With Nodes(n)
                    If .H.Value < chMin Then chMin = .H.Value
                    If .V.Value < cvMin Then cvMin = .V.Value
                End With
            Next

            If Nodes.Count > 0 Then
                With H
                    .Min = chMin
                    .Max = chMax
                    .Offset = RgnGraph.X
                    .Len = Nodes.Count
                    .Unit = RgnGraph.Width / .Len
                End With
                With V
                    .Min = 0
                    .Max = cvMax
                    .Offset = RgnGraph.Y + RgnGraph.Height
                    If .Max >= .Min Then
                        .Len = .Max - .Min
                    Else
                        .Len = .Min - .Max
                    End If
                    .Unit = RgnGraph.Height / .Len
                End With
            Else
                With H
                    .Min = chMin
                    .Max = chMax
                    .Offset = RgnGraph.X
                    .Len = 1
                    .Unit = RgnGraph.Width / .Len
                End With
                With V
                    .Min = cvMin
                    .Max = cvMax
                    .Offset = RgnGraph.Y + RgnGraph.Height
                    .Unit = 1
                End With
            End If
            'If cvMin < 0 Then cvMin = 0

        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Public Sub AddNode(ByVal ValueH As Single, ByVal ValueV As Single)
        Try
            Dim nwN As New GraphEx_DataNode
            With nwN
                .H.Value = ValueH
                .V.Value = ValueV

                If Nodes.Count > 0 Then
                    .Pos.Setup(Nodes(Nodes.Count - 1).Pos.GetCurrentF, Nodes(Nodes.Count - 1).Pos.GetCurrentF, 1 / 32, 1 / 32, 1.0)
                    .R = Nodes(Nodes.Count - 1)

                Else
                    .Pos.Setup(Point.Empty, Point.Empty, 1 / 32, 1 / 32, 1.0)
                End If
            End With
            Nodes.Add(nwN)
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Private lvH As Integer = 0
    Public Sub AddRndNode()
        Try
            Dim nwN As New GraphEx_DataNode
            With nwN
                .H.Value = lvH
                If Nodes.Count = 0 Then
                    .V.Value = rnd.Next(1, 100)
                Else
                    ' If Nodes(lvH - 1).V.Value < 50 Then
                    '  Nodes(lvH - 1).V.Value += rnd.Next(-1, 1)
                    .V.Value = rnd.Next(0, 100)
                    ' Else
                    ' Nodes(lvH - 1).V.Value -= rnd.Next(0, 10)
                End If



                If Nodes.Count > 0 Then
                    .Pos.Setup(Nodes(Nodes.Count - 1).Pos.GetCurrentF, Nodes(Nodes.Count - 1).Pos.GetCurrentF, 1 / 32, 1 / 32, 1.0)
                    .R = Nodes(Nodes.Count - 1)

                Else
                    .Pos.Setup(Point.Empty, Point.Empty, 1 / 32, 1 / 32, 1.0)
                End If
            End With
            Nodes.Add(nwN)
            lvH += 1

            flgReCalc = True
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Public Sub Render(ByVal G As Graphics)
        Try
            G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            DrawGrid(G, Color.FromArgb(92, 92, 92))
            For Each N As GraphEx_DataNode In Nodes
                If N.R IsNot Nothing Then Draw_Relation(G, N, ClrRelation)
            Next
            For Each N As GraphEx_DataNode In Nodes
                Draw_Node(G, N, ClrNode)
            Next

            'DrawText(G, "Time Elapsed per 100 miliseconds", sysfnt, Brushes.Silver, StringAlignment.Far, StringAlignment.Center, New Rectangle(RgnGraph.X + RgnGraph.Width - 1000, RgnGraph.Y + RgnGraph.Height, 1000, 16))
            'DrawText(G, "Memory Usage in Megabytes (Max: " & V.Max & ")", sysfnt, Brushes.Silver, StringAlignment.Near, StringAlignment.Center, New Rectangle(RgnGraph.X, RgnGraph.Y - 16, 1000, 16))


            DrawText(G, H.Title & " " & H.UnitTitle, sysfnt, Brushes.Silver, StringAlignment.Far, StringAlignment.Center, New Rectangle(RgnGraph.X + RgnGraph.Width - 1000, RgnGraph.Y + RgnGraph.Height + 3, 1000, 16))
            DrawText(G, V.Title & " " & V.UnitTitle & " (Max: " & V.Max & ")", sysfnt, Brushes.Silver, StringAlignment.Near, StringAlignment.Center, New Rectangle(RgnGraph.X, RgnGraph.Y - 19, 1000, 16))



        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Private Sub Draw_Node(ByRef G As Graphics, ByRef DataNode As GraphEx_DataNode, ByVal nColor As Color)
        Dim p(2) As PointF
        Try
            'If DataNode.Pos.GetCurrent.Y = 0 Then Exit Sub
            'If DataNode.Pos.GetCurrent.X = 0 Then Exit Sub

            Dim a As Single = 0
            Dim r As Single = 3
            a = 90 : p(0) = New PointF(DataNode.Pos.GetCurrent.X + (r * Math.Cos(a * RAD)), DataNode.Pos.GetCurrent.Y + (r * Math.Sin(a * RAD)))
            a = 210 : p(1) = New PointF(DataNode.Pos.GetCurrent.X + (r * Math.Cos(a * RAD)), DataNode.Pos.GetCurrent.Y + (r * Math.Sin(a * RAD)))
            a = 330 : p(2) = New PointF(DataNode.Pos.GetCurrent.X + (r * Math.Cos(a * RAD)), DataNode.Pos.GetCurrent.Y + (r * Math.Sin(a * RAD)))
            Dim br As New SolidBrush(nColor)
            G.FillPolygon(br, p)
            br.Dispose()
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Private Sub Draw_Relation(ByRef G As Graphics, ByRef DataNode As GraphEx_DataNode, ByVal nColor As Color)
        Try
            Dim pn As New Pen(nColor, 2)
            G.DrawLine(pn, DataNode.Pos.GetCurrentF, DataNode.R.Pos.GetCurrentF)
            pn.Dispose()
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Private Sub DrawGrid(ByVal G As Graphics, ByVal BaseColor As Color)
        Dim pnGrd As New Pen(Color.FromArgb(64, BaseColor), 1)
        Dim pnBrd As New Pen(Color.FromArgb(200, BaseColor), 2)
        'For x = 1 To Nodes.Count
        '    G.DrawLine(pnGrd, New Point(Nodes(x - 1).Pos.GetCurrent.X, RgnGraph.Y), New Point(Nodes(x - 1).Pos.GetCurrent.X, RgnGraph.Height))
        'Next
        For y = H.Offset To V.Offset + (V.Len) Step 10
            G.DrawLine(pnGrd, New Point(RgnGraph.X, y), New Point(RgnGraph.X + RgnGraph.Width, y))
        Next
        G.DrawRectangle(pnBrd, RgnGraph)
        pnGrd.Dispose()
        pnBrd.Dispose()
    End Sub

End Class

Public Class GraphEx_Axis
    Public Title As String
    Public UnitTitle As String
    Public Offset As Single
    Public Len As Single
    Public Unit As Single
    Public Min As Single
    Public Max As Single
End Class
Public Class GraphEx_NodeAxis
    Public Value As Single
End Class
Public Class GraphEx_DataNode
    Public H As New GraphEx_NodeAxis
    Public V As New GraphEx_NodeAxis
    Public Pos As New zzDynamix.CoreClasses.dynPoint
    Public R As GraphEx_DataNode = Nothing 'Relative
End Class