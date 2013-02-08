Public Class TreeMenu
    Public R As TreeMenuItem
    Public Rgn As Rectangle

    Public Event ItemClicked(Item As TreeMenuItem)

    Private lRgn As Rectangle
    Private sysfnt As New Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel)
    Private Rdy As Boolean = False
    Public ilIcons As New ImageList

    Private bCamMove As Boolean = False
    Private pLastMs As Point
    Private pCam As Point
    Private pLastCam As Point

    Private ErrStateAlpha As New zzDynamix.CoreClasses.dynSingle
    Private bErrState As Boolean

    Public sMorph As New StringMorpher

    Public Sub New(nwRootText As String)
        R = New TreeMenuItem
        With R
            .Text = nwRootText
            .Tag = "root"
            .IconKey = "money"
            .Exp = True
        End With

        ilIcons.ImageSize = New Size(16, 16)
        ilIcons.ColorDepth = ColorDepth.Depth32Bit

        ilIcons.Images.Add("money", My.Resources.euro_dark)
        ilIcons.Images.Add("moneyn", My.Resources.euro_green)

        ilIcons.Images.Add("actionc", My.Resources.lightning_darkergray)

        ilIcons.Images.Add("export", My.Resources.cd_dark)
        ilIcons.Images.Add("exportc", My.Resources.cd_darker)

        ilIcons.Images.Add("filter", My.Resources.filterA)
        ilIcons.Images.Add("filterc", My.Resources.filter_darker)
        ilIcons.Images.Add("filtern", My.Resources.filter_green)

        ilIcons.Images.Add("database", My.Resources.database_Dark)
        ilIcons.Images.Add("databasec", My.Resources.database_darker)
        ilIcons.Images.Add("databased", My.Resources.folder_darkergray)
        ilIcons.Images.Add("databasen", My.Resources.database_green)


        sMorph.MasterAlpha = 1000
        sMorph.SetString("Wazzap !", Color.Lime)

    End Sub

    Public Sub SetErrState(nErrState As Boolean)
        bErrState = nErrState
        If bErrState Then
            ErrStateAlpha.Setup(1000)
        Else
            ErrStateAlpha.Setup(0)
        End If
    End Sub

    Public Sub Update()
        If bErrState Then
            If ErrStateAlpha.GetCurrent = 1000 Then
                ErrStateAlpha.Setup(200)
            ElseIf ErrStateAlpha.GetCurrent = 200 Then
                ErrStateAlpha.Setup(1000)
            End If
        Else
            ErrStateAlpha.Setup(0)
        End If
        ErrStateAlpha.Update()


        If Not lRgn = Rgn Or Not pLastCam = pCam Then
            Reloc(New Point(pCam.X + 8, pCam.Y + 8))
            lRgn = Rgn
            pLastCam = pCam
            Rdy = True
        End If

        UpdNodes()
        sMorph.Update()
    End Sub

    Public Sub UpdNodes(Optional AtNode As TreeMenuItem = Nothing)
        If AtNode Is Nothing Then AtNode = R

        If AtNode.Rgn.Contains(pLastMs) Then
            AtNode.Hl = True
        Else
            AtNode.Hl = False
        End If

        If AtNode.Exp Then
            For Each N As TreeMenuItem In AtNode.C
                UpdNodes(N)
            Next
        End If

    End Sub

    Public Sub Render(G As Graphics)
        Try
            G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            G.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            G.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit
            If Rdy Then RenderR(G)


            If ErrStateAlpha.GetCurrent > 0 Then
                Dim rgnBox As New Rectangle(Rgn.X + 16, Rgn.Y + Rgn.Height - 24, 24, 24)
                Using sb As New SolidBrush(Color.FromArgb((155 / 1000) * ErrStateAlpha.GetCurrent, 255, 0, 0))
                    Using pn As New Pen(Color.FromArgb((255 / 1000) * ErrStateAlpha.GetCurrent, 255, 0, 0), 1)
                        G.FillRectangle(sb, rgnBox)
                        G.DrawRectangle(pn, rgnBox)
                        G.DrawImage(My.Resources._error, New Rectangle(rgnBox.X + 4, rgnBox.Y + 4, 16, 16))
                    End Using
                End Using
            End If

            sMorph.Render(G, sysfnt, Rgn, StringAlignment.Center, StringAlignment.Center, False)

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub
    Public Sub RenderR(G As Graphics, Optional AtNode As TreeMenuItem = Nothing)
        Try

            If AtNode Is Nothing Then AtNode = R
            Dim brText As New SolidBrush(Color.FromArgb(148, 148, 148))
            Dim brTextC As New SolidBrush(Color.FromArgb(200, 200, 200))
            If AtNode.Rgn.Width > 0 And AtNode.Rgn.Height > 0 Then

                If AtNode.C.Count > 0 Then
                    Dim brFill As New Drawing2D.LinearGradientBrush(New Rectangle(AtNode.Rgn.X - 4, AtNode.Rgn.Y - 4, AtNode.Rgn.Width + 8, AtNode.Rgn.Height + 8), Color.FromArgb(92, 92, 92), Color.FromArgb(64, 64, 64), 0)
                    Dim cb As New Drawing2D.ColorBlend
                    cb.Positions = {0.0, 0.5, 1.0}

                    If AtNode.Hl Then
                        cb.Colors = {Color.SlateGray, Color.FromArgb(92, 92, 92), Color.FromArgb(64, 64, 64)}
                    Else
                        cb.Colors = {Color.FromArgb(128, 128, 128), Color.FromArgb(92, 92, 92), Color.FromArgb(64, 64, 64)}
                    End If


                    brFill.InterpolationColors = cb
                    G.FillPolygon(brFill, AtNode.Ply)
                    brFill.Dispose()


                    Dim pnBrd As New Pen(Color.FromArgb(255, 64, 64, 64), 1)
                    G.DrawPolygon(pnBrd, AtNode.Ply)
                    pnBrd.Dispose()

                    DrawText(G, AtNode.Text, sysfnt, brTextC, StringAlignment.Near, StringAlignment.Center, New Rectangle(AtNode.Rgn.X + 16, AtNode.Rgn.Y, AtNode.Rgn.Width - 16, AtNode.Rgn.Height), True)

                    If ilIcons.Images(AtNode.IconKey) IsNot Nothing Then
                        G.DrawImage(ilIcons.Images(AtNode.IconKey), New Rectangle(AtNode.Rgn.X + 1, AtNode.Rgn.Y + 1, 16, 16))
                    End If
                Else

                    If AtNode.Hl Then
                        Dim brFillx As New Drawing2D.LinearGradientBrush(AtNode.Rgn, Color.FromArgb(92, 92, 92), Color.FromArgb(64, 64, 64), 0)
                        Dim cbx As New Drawing2D.ColorBlend
                        cbx.Positions = {0.0, 0.5, 1.0}

                        'If AtNode.Hl Then
                        cbx.Colors = {Color.FromArgb(64, 64, 64), Color.FromArgb(92, 92, 92), Color.FromArgb(64, 64, 64)}
                        brFillx.InterpolationColors = cbx
                        'Else
                        '    cb.Colors = {Color.FromArgb(128, 128, 128), Color.FromArgb(92, 92, 92), Color.FromArgb(64, 64, 64)}
                        'End If
                        G.FillRectangle(brFillx, AtNode.Rgn)
                        brFillx.Dispose()
                        DrawText(G, AtNode.Text, sysfnt, brTextC, StringAlignment.Near, StringAlignment.Center, New Rectangle(AtNode.Rgn.X + 16, AtNode.Rgn.Y, AtNode.Rgn.Width - 16, AtNode.Rgn.Height), True)
                    Else
                        DrawText(G, AtNode.Text, sysfnt, brText, StringAlignment.Near, StringAlignment.Center, New Rectangle(AtNode.Rgn.X + 16, AtNode.Rgn.Y, AtNode.Rgn.Width - 16, AtNode.Rgn.Height), True)
                    End If

                    If ilIcons.Images(AtNode.IconKey) IsNot Nothing Then
                        G.DrawImage(ilIcons.Images(AtNode.IconKey), New Rectangle(AtNode.Rgn.X + 1, AtNode.Rgn.Y + 0, 16, 16))
                    End If
                End If







            End If
            brText.Dispose()
            brTextC.Dispose()
            If AtNode.Exp Then
                For Each N As TreeMenuItem In AtNode.C
                    RenderR(G, N)
                Next
            End If

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub



    Public Sub Reloc(ByRef pOffs As Point, Optional AtNode As TreeMenuItem = Nothing)
        Try
            If AtNode Is Nothing Then AtNode = R
            AtNode.Rgn = New Rectangle(pOffs.X, pOffs.Y, Rgn.Width - (pOffs.X * 1), 16)
            AtNode.Ply = BuildPly(AtNode)

            'If AtNode.C.Count > 0 Then
            pOffs.Y += AtNode.Rgn.Height + 9
            'Else
            '    pOffs.Y += AtNode.Rgn.Height + 3
            'End If

            If AtNode.Exp Then
                For Each N As TreeMenuItem In AtNode.C
                    pOffs.X += 14
                    Reloc(pOffs, N)
                    pOffs.X -= 14
                Next
            End If

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Public Function BuildPly(Optional AtNode As TreeMenuItem = Nothing) As PointF()
        Try
            Dim p(9999) As PointF
            Dim pID As Integer = 0
            Dim ang As Single = 315
            Dim pC As New PointF(AtNode.Rgn.X + 8, AtNode.Rgn.Y + 8)

            p(pID) = New PointF(AtNode.Rgn.X + AtNode.Rgn.Width, AtNode.Rgn.Y) : pID += 1
            For c = 0 To 270 Step 3
                p(pID) = New PointF(pC.X + (12 * Math.Cos(ang * RAD)), pC.Y + (12 * Math.Sin(ang * RAD))) : pID += 1
                ang -= 3
                If ang < 0 Then ang += 360
                If ang > 360 Then ang -= 360

            Next
            p(pID) = New PointF(AtNode.Rgn.X + AtNode.Rgn.Width, AtNode.Rgn.Y + AtNode.Rgn.Height) : pID += 1

            ReDim Preserve p(pID - 1)
            Return p
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Function

    Public Sub TryParse(Src As String)
        Try
            R.C.Clear()
            Dim aSrc() As String = Src.Split(vbCrLf)
            For c = 0 To UBound(aSrc)


                TryParseLn(aSrc(c))

            Next

            Reloc(New Point(pCam.X + 8, pCam.Y + 8))
            UpdNodes()
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub
    Private Sub TryParseLn(Src As String)
        Try
            Src = Src.Replace(vbCr, "")
            Src = Src.Replace(vbLf, "")
            If Src <> "" Then
                Dim aSrc() As String = Src.Split("|")

                'Title|Tag|Cmd|Path|IconKey
                Dim nTrg As TreeMenuItem
                Dim nwN As New TreeMenuItem
                nwN.Text = aSrc(0)
                nwN.Tag = aSrc(1)
                nwN.Cmd = aSrc(2)
                nTrg = GetNode(aSrc(3), 0)
                nwN.IconKey = aSrc(4)

                If GetNode(aSrc(3) & "/" & aSrc(1), 0) Is Nothing Then
                    If nTrg Is Nothing Then
                        R.C.Add(nwN)
                    Else
                        nTrg.C.Add(nwN)
                    End If
                End If
            End If




        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Public Function GetNode(ByPath As String, PathID As Integer, Optional AtNode As TreeMenuItem = Nothing) As TreeMenuItem
        Try
            Dim rN As TreeMenuItem = Nothing
            If AtNode Is Nothing Then AtNode = R
            Dim aPth() As String = ByPath.Split("/")

            If AtNode.Tag = aPth(PathID) Then
                If PathID = UBound(aPth) Then
                    rN = AtNode
                    Return rN
                End If
            End If

            If Not PathID + 1 > UBound(aPth) Then
                For Each N As TreeMenuItem In AtNode.C
                    rN = GetNode(ByPath, PathID + 1, N)
                Next
            End If


            Return rN
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try

    End Function
    Public Function GetNodeAt(Pos As Point, Optional AtNode As TreeMenuItem = Nothing) As TreeMenuItem
        Dim ret As TreeMenuItem = Nothing
        If AtNode Is Nothing Then AtNode = R
        Try
            If AtNode.Rgn.Contains(Pos) Then
                Return AtNode
            End If
            If AtNode.Exp Then
                For Each N As TreeMenuItem In AtNode.C
                    ret = GetNodeAt(Pos, N)
                    If ret IsNot Nothing Then Return ret
                Next
            End If

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
        Return ret
    End Function

    Public Sub Inf_Ms_Down(ByVal pMs As Point)
        Try
            bCamMove = True
            pLastMs = pMs
            'bHasMoved = False
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Public Sub Inf_Ms_Up(ByVal pMs As Point)
        Try
            bCamMove = False
            pLastMs = pMs

            Dim n As TreeMenuItem = GetNodeAt(pMs)
            If n IsNot Nothing Then
                n.Exp = Not n.Exp
                RaiseEvent ItemClicked(n)
                Reloc(New Point(pCam.X + 8, pCam.Y + 8))
                UpdNodes()
            End If

            'bHasMoved = False
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Public Sub Inf_Ms_Move(ByVal pMs As Point)
        Try
            If bCamMove Then
                pCam.Y += (pLastMs.Y - pMs.Y) * 1
                If pCam.Y >= 0 Then pCam.Y = 0 'UpperBound

                ' RaiseEvent UpdMovement()
            End If
            pLastMs = pMs
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Public Sub Inf_Ms_Whl(ByVal Delta As Single)
        Try
            If Delta < 0 Then
                pCam.Y -= 10
                ' RaiseEvent UpdMovement()
            Else
                pCam.Y += 10
                '   RaiseEvent UpdMovement()
            End If

        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

End Class
Public Class TreeMenuItem
    Public T As TreeMenuItem
    Public P As TreeMenuItem
    Public C As New List(Of TreeMenuItem)

    Public Rgn As Rectangle
    Public Text As String
    Public Tag As String
    Public Cmd As String
    Public IconKey As String
    Public Exp As Boolean
    Public Hl As Boolean

    Public Ply() As PointF

End Class
