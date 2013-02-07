Public Class guiAniDB

    Public Rgn As Rectangle
    Private lRgn As Rectangle

    Public Percentage As Integer = 0
    Private lPercentage As Integer = -1
    Public NumSections As Integer = 5
    Private Rdy As Boolean = False
    Private Bld As Boolean = False
    Private BldRdy As Boolean = False

    Public Alpha As Single
    Public Sections() As AniDBSection
    'Public Section2 As New AniDBSection

    Public FillIDX() As Integer

    Public AniSpeedMs As Integer = 1
    Public AniPos() As Integer
    Public AniPosL As Integer
    Public AniSize As Integer = 40
    Public AniDetail As Integer = 50
    Public AnimationID As Integer = 0
    Private iOffs As Integer = Environment.TickCount


    Public Sub New()
        Init()
    End Sub
    Public Sub Init()
        Rdy = False
        ReDim Sections(NumSections - 1)



        ReDim AniPos(NumSections - 1)
        For c = 0 To NumSections - 1
            Sections(c) = New AniDBSection(Me)
            AniPos(c) = -1
        Next
        '  FillIDX()
        Rdy = True
    End Sub
    Public Sub Reloc(nwRgn As Rectangle)
        Rgn = nwRgn
        If Not lRgn = Rgn Then
            Bld = True
            BldRdy = False
            lRgn = Rgn
        End If
    End Sub
    Public Sub SetHlColor(nwColorA As Color, nwColorB As Color)
        For c = 0 To NumSections - 1
            Sections(c).HlColorA = nwColorA
            Sections(c).HlColorB = nwColorB
        Next
    End Sub

    Public Sub Update()

        If Rdy Then
            If Bld Then
                Dim pOffs As New Point(Rgn.X, Rgn.Y)
                For c = 0 To NumSections - 1
                    Sections(c).Rgn = New Rectangle(pOffs.X, pOffs.Y, Rgn.Width, Rgn.Height)
                    '  Sections(c).AniPos = AniDetail - (c * 2)
                    pOffs.Y += Rgn.Height
                Next
                BldRdy = True
                Bld = False
            End If

            If BldRdy Then
                Dim HLPos As Integer = CInt((100 / Sections.Length) * Percentage)
                For c = 0 To NumSections - 1
                    If HLPos <= c Then
                        Sections(c).HLMode = 1
                    Else
                        Sections(c).HLMode = 0
                    End If
                    Sections(c).Alpha = Alpha
                    Sections(c).Update()
                Next
            End If

            If lPercentage <> Percentage Then
                If Percentage > 0 And Percentage < 100 Then
                    AnimationID = 1
                    For c = 0 To NumSections - 1
                        AniPos(c) = -1
                    Next
                    AniPosL = 0
                Else
                    AnimationID = 0
                    For c = 0 To NumSections - 1
                        AniPos(c) = -1
                    Next
                    AniPosL = 0
                End If
                lPercentage = Percentage
            End If


            Select Case AnimationID
                Case 0
                    If Environment.TickCount - iOffs >= AniSpeedMs Then
                        AniPos(AniPosL) += 1
                        If AniPos(AniPosL) > AniDetail * 2 - 1 Then
                            'AniPos(AniPosL) = 0
                            Sections(AniPosL).AniPos = AniPos(AniPosL)
                            AniPosL += 1
                            If AniPosL > NumSections - 1 Then
                                For c = 0 To NumSections - 1
                                    AniPos(c) = -1
                                Next
                                AniPosL = 0
                            End If
                        End If
                        For c = 0 To NumSections - 1
                            Sections(c).AniPos = AniPos(c)
                        Next

                        iOffs = Environment.TickCount
                    End If
                Case 1
                    'Dim pBlock As Integer = (100 / (NumSections * AniDetail)) * Percentage
                    'Dim lID As Integer = (pBlock / NumSections) - 1

                    If Percentage = 0 Then
                        For c = 0 To NumSections - 1
                            Sections(c).AniPos = -1
                            For x = 0 To AniDetail - 1
                                Sections(c).aDC(x).Setup(Color.Transparent)
                            Next
                        Next
                    Else



                        Dim remID As Integer = ((NumSections * AniDetail) / 100) * Percentage
                        For c = 0 To NumSections - 1

                            Sections(c).AniPos = remID
                            If remID >= AniDetail Then
                                Sections(c).AniPos = AniDetail
                                remID -= AniDetail
                            ElseIf remID > 0 Then
                                Sections(c).AniPos = remID
                                remID -= remID
                            Else
                                Sections(c).AniPos = -1
                            End If


                            'If c < lID Then
                            '    Sections(c).AniPos = AniDetail
                            'ElseIf c = lID Then
                            '    Sections(c).AniPos = remID
                            'Else
                            '    Sections(c).AniPos = -1
                            'End If

                        Next
                    End If
            End Select



        End If
    End Sub

    Public Sub Render(ByRef G As Graphics)
        If BldRdy Then
            For c = NumSections - 1 To 0 Step -1
                Sections(c).Render(G)
            Next
        End If
    End Sub

End Class




Public Class AniDBSection
    Public rfMgr As guiAniDB

    Private plyTop() As PointF
    Private plyFront() As PointF
    Public Rgn As Rectangle

    Public HLMode As Integer = 0
    Public HlColorA As Color = Color.Orange
    Public HlColorB As Color = Color.SlateGray
    Public Alpha As Single

    Private lRgn As Rectangle
    Private iOffs As Integer

    Private Rdy As Boolean = False
    Private Bld As Boolean = False

    'Private AniSpeedMs As Integer = 10
    Public AniPos As Integer = -1
    'Public AniSize As Integer = 40
    'Public AniDetail As Integer = 50
    'Public AnimationID As Integer = 0
    Private aPos() As Single
    Private aClr() As Color
    Public aDC() As zzDynamix.CoreClasses.dynColor

    Public Sub New(ByRef refMgr As guiAniDB)
        rfMgr = refMgr
        Init()
    End Sub
    Public Sub Init()
        ReDim aPos(rfMgr.AniDetail)
        ReDim aClr(rfMgr.AniDetail)
        ReDim aDC(rfMgr.AniDetail)
        Dim pU As Single = 1 / rfMgr.AniDetail
        For c = 0 To rfMgr.AniDetail
            aDC(c) = New zzDynamix.CoreClasses.dynColor
            aDC(c).Setup(Color.Transparent, Color.Transparent, 1 / 8, 1 / 8, 1)
            aPos(c) = pU * c
            aClr(c) = Color.Transparent
        Next
        iOffs = Environment.TickCount
    End Sub
    Public Sub BuildPolygons()
        Rdy = False
        Bld = False
        ReDim plyTop(9999)
        ReDim plyFront(9999)
        Dim ang As Single
        Dim angU As Single = 3
        Dim p As Integer = 0
        Dim DiskCenter As Point = New Point(Rgn.X + (Rgn.Width / 2), Rgn.Y + (Rgn.Height / 2))
        Dim DiskRadiusX As Integer = Rgn.Width / 2
        Dim DiskRadiusY As Integer = Rgn.Height / 2

        ang = 0
        For a = 0 To 360 Step angU
            plyTop(p) = New Point(DiskCenter.X + (DiskRadiusX * Math.Sin(ang * RAD)), DiskCenter.Y + (DiskRadiusY * Math.Cos(ang * RAD)))
            p += 1

            ang += angU
            If ang < 0 Then ang += 360
            If ang > 360 Then ang -= 360
        Next
        ReDim Preserve plyTop(p - 1)

        p = 0
        ang = 90
        DiskCenter.Y += 0 ' DiskRadiusY / 2
        For a = 0 To 180 Step angU
            plyFront(p) = New Point(DiskCenter.X + (DiskRadiusX * Math.Sin(ang * RAD)), DiskCenter.Y + (DiskRadiusY * Math.Cos(ang * RAD)))
            p += 1

            ang -= angU
            If ang < 0 Then ang += 360
            If ang > 360 Then ang -= 360
        Next
        ' ang = 90
        DiskCenter.Y += DiskRadiusY
        For a = 0 To 180 Step angU
            plyFront(p) = New Point(DiskCenter.X + (DiskRadiusX * Math.Sin(ang * RAD)), DiskCenter.Y + (DiskRadiusY * Math.Cos(ang * RAD)))
            p += 1

            ang += angU
            If ang < 0 Then ang += 360
            If ang > 360 Then ang -= 360
        Next
        ReDim Preserve plyFront(p - 1)
        Rdy = True
    End Sub

    Public Sub Update()
        Try

 
        If Not lRgn = Rgn Then
            Bld = True
            lRgn = Rgn
        End If

        If Bld Then
            BuildPolygons()
        End If

        If Environment.TickCount - iOffs >= rfMgr.AniSpeedMs Then
            UpdateAni()
            iOffs = Environment.TickCount
        End If

        If Rdy Then
            For c = 0 To rfMgr.AniDetail - 1
                aDC(c).Setup(Color.Transparent)
                Next

                Select Case rfMgr.AnimationID
                    Case 0
                        For s = AniPos To AniPos - rfMgr.AniSize Step -1
                            If s > -1 And s <= rfMgr.AniDetail Then
                                aDC(s).Setup(Color.FromArgb(Alpha - ((Alpha / rfMgr.AniDetail) * s), HlColorB))
                            End If
                        Next
                        ' If HLMode = 0 Then
                        'If AniPos > -1 And AniPos <= rfMgr.AniDetail Then
                        '    aDC(SwitchPos(AniPos, -0)).Setup(Color.FromArgb(Alpha - ((Alpha / rfMgr.AniDetail) * 1), HlColorB))
                        'End If

                        'Else
                        '    aDC(SwitchPos(AniPos, -s)).Setup(Color.FromArgb(Alpha - ((Alpha / rfMgr.AniSize) * s), HlColorA))
                        'End If

                        '  Next
                    Case 1
                        For x = 0 To AniPos
                            If x > -1 And x <= rfMgr.AniDetail Then
                                aDC(x).Setup(Color.FromArgb(Alpha - ((Alpha / rfMgr.AniDetail) * x), HlColorB))
                            End If
                        Next
                End Select




                For c = 0 To rfMgr.AniDetail - 1
                    aDC(c).Update()
                    aClr(c) = aDC(c).GetCurrent
                Next
            End If
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub
    Public Function SwitchPos(Src As Integer, vMod As Integer) As Integer
        Src += vMod
        If Src > rfMgr.AniDetail Then Src -= rfMgr.AniDetail
        If Src < 0 Then Src += rfMgr.AniDetail
        Return Src
    End Function
    Public Sub UpdateAni()

        'AniPos += 1
        'If AniPos > rfMgr.AniDetail Then AniPos = 0
    End Sub

    Public Sub Render(ByRef G As Graphics)
        If Rdy Then
            Try
                Dim lgbr As New Drawing2D.LinearGradientBrush(Rgn, Color.Black, Color.Black, 0)
                Dim cb As New Drawing2D.ColorBlend
                cb.Positions = {0.0, 1.0}
                cb.Colors = {Color.FromArgb(Alpha, 32, 32, 32), Color.FromArgb(Alpha, 92, 92, 92)}
                lgbr.InterpolationColors = cb


                Dim lgbrLyr As New Drawing2D.LinearGradientBrush(Rgn, Color.Transparent, Color.Transparent, 0)
                Dim cb2 As New Drawing2D.ColorBlend
                cb2.Positions = aPos
                cb2.Colors = aClr
                lgbrLyr.InterpolationColors = cb2

                Dim pthbr As New Drawing2D.PathGradientBrush(plyTop)
                ' Fill an ellipse setting CenterColor and SurroundColors.
                Dim ellipse_path As New Drawing2D.GraphicsPath
                ellipse_path.AddEllipse(Rgn.X, Rgn.Y, Rgn.Width, CInt(Rgn.Height))
                pthbr = New Drawing2D.PathGradientBrush(ellipse_path)
                pthbr.CenterColor = Color.FromArgb(Alpha, 32, 32, 32)
                pthbr.SurroundColors = New Color() {Color.FromArgb(Alpha, 64, 64, 64)}

                Dim pnBrd As New Pen(Color.FromArgb(ModAlpha(Alpha, -55, 255), 92, 92, 92), 1)
                Dim gSM As Drawing2D.SmoothingMode = G.SmoothingMode
                G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

                G.FillPolygon(pthbr, plyTop)
                G.DrawPolygon(pnBrd, plyTop)
                G.FillPolygon(lgbr, plyFront)
                G.FillPolygon(lgbrLyr, plyFront)
                G.DrawPolygon(pnBrd, plyFront)

                G.SmoothingMode = gSM
                pthbr.Dispose()
                pnBrd.Dispose()
                lgbr.Dispose()
                lgbrLyr.Dispose()


                '  DrawText(G, AniPos, sysfnt, Brushes.Red, StringAlignment.Center, StringAlignment.Center, Rgn)
            Catch ex As Exception
                Console.WriteLine(ex.ToString)
            End Try

        End If



    End Sub

    Private Function ModAlpha(Src As Single, vMod As Single, vMax As Single) As Single
        Src += vMod
        If Src < 0 Then Src = 0
        If Src > vMax Then Src = vMax
        Return Src
    End Function
End Class
