Public Class DataPresenter
    Inherits DP_Shared
    Public WithEvents rfDM As zzDataMgr.DataManager
    Public WithEvents Cam As New DP_Cam
    Public Stage As PresentStage = PresentStage.FadeIN
    Public RgnView As Rectangle
    Public pLstMs As Point
    Public sStatus As String = ""

    Public Results As New List(Of DP_ResPage)
    Private ResultsB As New List(Of DP_ResPage)
    Private DrwData() As DP_DrwDat

    Private IsSearching As Boolean = False
    Private Ready As Boolean = False
    Private ReLocate As Boolean = False

    Private lColumns As New List(Of DP_Column)

    Public HeaderHeight As Integer = 18
    Public ItemHeight As Integer = 16

    Public LoadDBAlpha As New zzDynamix.CoreClasses.dynSingle
    Public LoadViewAlpha As New zzDynamix.CoreClasses.dynSingle
    Public SrchDBAlpha As New zzDynamix.CoreClasses.dynSingle
    Public AnimAlpha As New zzDynamix.CoreClasses.dynSingle

    Private prefix As String = "€ "
    Private sysfnt As New Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel)
    Private sysfntb As New Font("Microsoft Sans Serif", 10, FontStyle.Bold, GraphicsUnit.Pixel)
    Private sysfntbr As New SolidBrush(Color.FromArgb(200, 200, 200))
    Private clmfntbr As New SolidBrush(Color.FromArgb(128, 128, 128))
    Private sysfntbrs As New SolidBrush(Color.FromArgb(0, 0, 0))

    Public SearchText As String = ""
    Public SearchField As String = ""
    Public SortField As String = "mutationdate"
    Public SortOrder As Integer = 1

    '  Private Indicator As New ProIndicator
    Private Indicator As New guiAniDB

    Public Sub New()
        'LoadAlpha.SetSpeed(1 / 4)
        'Alpha.SetSpeed(1 / 4)
        AnimAlpha.Setup(0, 0, 1 / 64, 1 / 64, 1)
        Alpha.Setup(0, 0, 1 / 8, 1 / 8, 1)
        LoadDBAlpha.Setup(0, 0, 1 / 8, 1 / 8, 1)
        LoadViewAlpha.Setup(0, 0, 1 / 8, 1 / 8, 1)
        SrchDBAlpha.Setup(0, 0, 1 / 8, 1 / 8, 1)
    End Sub
    Public Sub LinkDM(ByRef RefDM As zzDataMgr.DataManager)
        rfDM = RefDM
        InitColumns()
    End Sub
    Public Sub SetRGN(ByVal nwRgn As Rectangle)
        Rgn = New Rectangle(nwRgn.X, nwRgn.Y, nwRgn.Width, nwRgn.Height)
        OnResize()
    End Sub
    Public Sub PurgeData()
        Ready = False
        For rp = 0 To Results.Count - 1
            Results(rp).Results.Clear()
        Next
        Results.Clear()
    End Sub
    Public Sub PostQuery()
        Try
            sStatus = "Executing query..."
            IsSearching = True
            Dim Script As String = ""
            If SearchText <> "" Then
                Script &= "filter " & SearchField & " Contains " & SearchText
            End If
            If SortField <> "" Then
                If Script <> "" Then Script &= vbCrLf
                Script &= "sort " & Kernell.DP.SortField & " " & SortOrder
            End If
            Kernell.DM.ExecQuery(Script)



        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Public Function BuildResults() As List(Of DP_ResPage)
        Try
            Results.Clear()
            Dim nwL As New List(Of DP_ResPage)
            Dim lstPage As DP_ResPage
            Dim PageID As Integer = 0
            Dim PageIDX As Integer = 0
            Dim ResIDX As Integer = 0
            lstPage = New DP_ResPage
            nwL.Add(lstPage)

            ' Dim MinDatSize(rfDM.Fields.Count - 1) As Single

            sStatus = "Building result list..."

            For c = 0 To rfDM.Results.Count - 1
                Dim res As New DP_Res(rfDM.Results(c))
                With res
                    .IDX = ResIDX

                End With
                lstPage.Results.Add(res)
                ResIDX += 1

                PageIDX += 1
                If PageIDX >= 1000 Then
                    PageID += 1
                    PageIDX = 0
                    lstPage = New DP_ResPage
                    nwL.Add(lstPage)
                End If
            Next

            DynSizeColumns()

            If ResIDX = 0 Then
                sStatus = "The search yielded no results"
            ElseIf ResIDX = 1 Then
                sStatus = "1 Item found"
            Else
                sStatus = ResIDX & " Items found"
            End If


            Return nwL
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Function

    Public Sub DynSizeColumns()
        ' sStatus = "Calculating column sizes..."
        Try
            Dim MaxDatSize(rfDM.Fields.Count - 1) As Single
            Dim DatSize As New SizeF
            Dim B As New Bitmap(1, 1)
            Dim G As Graphics = Graphics.FromImage(B)
            Dim id As Integer
            For rp = 0 To Results.Count - 1
                For r = 0 To Results(rp).Results.Count - 1
                    For x = 0 To UBound(Results(rp).Results(r).Data)
                        Try
                            DatSize = G.MeasureString(Results(rp).Results(r).Data(x), sysfnt)
                            If DatSize.Width > MaxDatSize(x) Then MaxDatSize(x) = DatSize.Width
                        Catch ex As Exception

                        End Try

                    Next
                    id += 1
                    If id > 1000 Then Exit For
                Next
                If id > 1000 Then Exit For
            Next

            G.Dispose()
            B.Dispose()

            Dim wRem As Integer = RgnView.Width
            For x = 0 To lColumns.Count - 1
                lColumns(x).Rgn.Width = MaxDatSize(x)
                wRem -= MaxDatSize(x)
            Next

            If wRem > 0 Then
                Dim wU As Integer = wRem / lColumns.Count
                For x = 0 To lColumns.Count - 1
                    lColumns(x).Rgn.Width += wU
                Next
            End If

            For x = 1 To lColumns.Count - 1
                lColumns(x).Rgn.X = lColumns(x - 1).Rgn.X + lColumns(x - 1).Rgn.Width
            Next

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Public Function TestPage(ByRef PageRGN As Rectangle)
        If Rgn.Y >= PageRGN.Y And Rgn.Y + Rgn.Height <= PageRGN.Y + PageRGN.Height Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub RelocResults()
        Try
            Dim pOffs As Point = Cam.Pos
            Dim pOffsX As New Point()
            For c = 0 To Results.Count - 1
                Results(c).Rgn = New Rectangle(pOffs.X, pOffs.Y, RgnView.Width, ItemHeight * Results(c).Results.Count)
                Results(c).Vis = RgnView.IntersectsWith(Results(c).Rgn)
                ' If TestPage(Results(c).Rgn) Then
                If Results(c).Vis Then
                    pOffsX = pOffs
                    For x = 0 To Results(c).Results.Count - 1
                        Results(c).Results(x).Rgn = New Rectangle(pOffsX.X, pOffsX.Y, RgnView.Width, ItemHeight)
                        Results(c).Results(x).Vis = RgnView.IntersectsWith(Results(c).Results(x).Rgn)
                        'If Results(c).Results(x).Rgn.Contains(pLstMs) Then
                        '    Results(c).Results(x).hl = True
                        'Else
                        '    Results(c).Results(x).hl = False
                        'End If
                        pOffsX.Y += ItemHeight
                    Next
                End If
                pOffs.Y += ItemHeight * Results(c).Results.Count
            Next
                    Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub
    Public Sub ShowResults()
        ResultsB = BuildResults()
        Stage = PresentStage.StartSwitch
    End Sub

    Public Sub InitColumns()
        Try
            lColumns.Clear()
            For c = 0 To rfDM.Fields.Count - 1
                Dim nwC As New DP_Column
                With nwC
                    .Title = rfDM.Fields(c).Name
                    .fType = rfDM.Fields(c).fType
                End With
                lColumns.Add(nwC)
            Next
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Public Sub Update()
        Try


            If rfDM.IsLoading Then
                AnimAlpha.Setup(255)
                LoadDBAlpha.Setup(1000)
                sStatus = "Loading database..."
            Else
                LoadDBAlpha.Setup(0)
            End If

            If IsSearching Then
                sStatus = "Searching through database..."
                SrchDBAlpha.Setup(1000)
                AnimAlpha.Setup(255)
            Else
                SrchDBAlpha.Setup(0)
            End If
            'If ReLocate Then
            '    RelocResults()
            '    ReLocate = False
            'End If


            Select Case Stage
                Case PresentStage.DoSwitch
                    Results = ResultsB

                    ResetCam()
                    RelocResults()
                    SetRGN(Rgn)
                    OnResizeEnd()
                    'DynSizeColumns()

                    Stage = PresentStage.FadeIN
                Case PresentStage.StartSwitch
                    Stage = PresentStage.FadeOut
                Case PresentStage.FadeIN
                    Alpha.Setup(1000)
                    LoadViewAlpha.Setup(0)
                    If Alpha.GetCurrent = 1000 Then Stage = PresentStage.Visible
                Case PresentStage.Visible

                Case PresentStage.FadeOut
                    Alpha.Setup(0)
                    AnimAlpha.Setup(0)
                    LoadViewAlpha.Setup(1000)
                    If Alpha.GetCurrent = 0 Then Stage = PresentStage.DoSwitch
            End Select
            Alpha.Update()
            LoadViewAlpha.Update()
            SrchDBAlpha.Update()
            LoadDBAlpha.Update()

            AnimAlpha.Update()
            Indicator.Alpha = AnimAlpha.GetCurrent
            Indicator.Update()

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    'Public Sub BuildDrwData()
    '    If Results.Count > 0 Then
    '        Dim pOffs As New Point

    '        ReDim DrwData(Results.Count - 1)
    '        For r = 0 To Results.Count - 1
    '            DrwData(r) = New DP_DrwDat
    '            With DrwData(r)
    '                .Rgn = New Rectangle(pOffs.X, pOffs.Y, RgnView.Width, ItemHeight)
    '                ReDim .RgnC(lColumns.Count - 1)
    '                For c = 0 To lColumns.Count - 1
    '                    .RgnC(c) = New Rectangle(lColumns(c).Rgn.X, pOffs.Y, lColumns(c).Rgn.Width, ItemHeight)
    '                Next
    '            End With

    '            pOffs.Y += ItemHeight
    '        Next
    '    End If
    'End Sub
    'Public Sub TestDrwData()
    '    For r = 0 To Results.Count - 1
    '        For c = 0 To lColumns.Count - 1
    '            If RgnView.Contains(DrwData(r).Rgn) Then
    '                DrwData(r).VIS = True
    '            Else
    '                DrwData(r).VIS = True
    '            End If
    '        Next
    '    Next
    'End Sub
    Public Sub OnResize()
        Try
            Dim cPos As PointF = Rgn.Location
            Dim uWidth As Single = Rgn.Width / lColumns.Count
            For c = 0 To lColumns.Count - 1
                lColumns(c).Rgn = New Rectangle(cPos.X, cPos.Y, uWidth, HeaderHeight)
                cPos.X += uWidth
            Next
            RgnView = New Rectangle(Rgn.X, Rgn.Y + HeaderHeight, Rgn.Width, Rgn.Height - HeaderHeight)

            ReLocate = True
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Public Sub OnResizeEnd()
        DynSizeColumns()
    End Sub

    Public Sub Render(ByVal G As Graphics)
        Try
            G.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit

            Dim pnGrid As New Pen(Color.FromArgb((255 / 1000) * Alpha.GetCurrent, Color.FromArgb(92, 92, 92)))
            Dim brData As New SolidBrush(Color.FromArgb((255 / 1000) * Alpha.GetCurrent, Color.Silver))
            Dim brDataPos As New SolidBrush(Color.FromArgb((255 / 1000) * Alpha.GetCurrent, Color.Lime))
            Dim brDataNeg As New SolidBrush(Color.FromArgb((255 / 1000) * Alpha.GetCurrent, Color.Red))
            'Dim iid As Integer

            For c = 0 To Results.Count - 1
                If Results(c).Vis Then

                    For x = 0 To Results(c).Results.Count - 1
                        If Results(c).Results(x).Vis Then
                            If Results(c).Results(x).HL Then
                                Dim brTrans As New SolidBrush(Color.FromArgb(64, Color.SlateGray))
                                G.FillRectangle(brTrans, New Rectangle(Results(c).Results(x).Rgn.X + 1, Results(c).Results(x).Rgn.Y + 1, Results(c).Results(x).Rgn.Width - 2, Results(c).Results(x).Rgn.Height - 1))
                                brTrans.Dispose()
                            End If
                            DrawFadedLine(G, New Rectangle(Results(c).Results(x).Rgn.X, Results(c).Results(x).Rgn.Y + Results(c).Results(x).Rgn.Height - 1, RgnView.Width, 1), Color.FromArgb(8, 255, 255, 255), 0)

                            ' DrawText(G, Results(c).Results(x).IDX.ToString, sysfnt, brData, StringAlignment.Near, StringAlignment.Center, Results(c).Results(x).Rgn, False)
                            For v = 0 To lColumns.Count - 1
                                Dim nxR As New Rectangle(lColumns(v).Rgn.X, Results(c).Results(x).Rgn.Y, lColumns(v).Rgn.Width, ItemHeight)


                                If Not nxR = RectangleF.Empty Then
                                    '   Dim nxRL As RectangleF = Cam.Reloc(nxR)
                                    '  If Rgn.Contains(nxRL) Then
                                    '  If Not Results(c).Results(x).HL Then
                                    ' G.DrawRectangle(pnGrid, nxR)
                                    'End If
                                    Select Case lColumns(v).fType
                                        Case "currency"
                                            Dim iTest As Single = Results(c).Results(x).Data(v)
                                            If iTest < 0 Then
                                                DrawText(G, prefix & Format(iTest, "0.00"), sysfnt, brDataNeg, StringAlignment.Far, StringAlignment.Center, nxR, False)
                                            Else
                                                DrawText(G, prefix & Format(iTest, "0.00"), sysfnt, brDataPos, StringAlignment.Far, StringAlignment.Center, nxR, False)
                                            End If

                                        Case Else
                                            DrawText(G, Results(c).Results(x).Data(v), sysfnt, brData, StringAlignment.Near, StringAlignment.Center, nxR, False)
                                    End Select

                                    'End If
                                End If
                            Next

                            'G.DrawRectangles(pnGrid, {Cam.Reloc(nxR)})
                            'DrawText(G, Results(r).Data(c), sysfnt, brData, StringAlignment.Near, StringAlignment.Center, Cam.Reloc(nxR), False)


                        End If
                    Next

                End If
            Next



            'For r = 0 To Results.Count - 1
            '    For c = 0 To lColumns.Count - 1
            '        Dim nxR As New RectangleF(lColumns(c).Rgn.X, r * ItemHeight, lColumns(c).Rgn.Width, ItemHeight)


            '        If Not nxR = RectangleF.Empty Then
            '            Dim nxRL As RectangleF = Cam.Reloc(nxR)
            '            If Rgn.Contains(nxRL) Then
            '                G.DrawRectangles(pnGrid, {Cam.Reloc(nxR)})
            '                DrawText(G, Results(r).Data(c), sysfnt, brData, StringAlignment.Near, StringAlignment.Center, Cam.Reloc(nxR), False)
            '            End If
            '        End If
            '    Next
            'Next
            brData.Dispose()
            pnGrid.Dispose()



            Dim rFadeA As New Rectangle(Rgn.X, Rgn.Y, Rgn.Width, 16 + HeaderHeight)
            Dim rFadeB As New Rectangle(Rgn.X, Rgn.Y + Rgn.Height - 16, Rgn.Width, 16)
            Dim bFadeA As New Drawing2D.LinearGradientBrush(rFadeA, Color.Transparent, Color.FromArgb(64, 64, 64), 90)
            Dim bFadeB As New Drawing2D.LinearGradientBrush(rFadeB, Color.Transparent, Color.FromArgb(64, 64, 64), 90)
            Dim cbA As New Drawing2D.ColorBlend
            cbA.Colors = {Color.FromArgb(64, 64, 64), Color.Transparent}
            cbA.Positions = {0.0, 1.0}
            bFadeA.InterpolationColors = cbA
            G.FillRectangle(bFadeA, rFadeA)
            G.FillRectangle(bFadeB, rFadeB)
            bFadeA.Dispose()
            bFadeB.Dispose()

            For c = 0 To lColumns.Count - 1
                If Not lColumns(c).Rgn = RectangleF.Empty Then
                    ' G.DrawRectangles(Pens.Red, {lColumns(c).Rgn})
                    DrawFadedLine(G, New Rectangle(lColumns(c).Rgn.X + lColumns(c).Rgn.Width, RgnView.Y, 1, RgnView.Height), Color.FromArgb(128, 255, 255, 255), 90)


                    Dim bCol As New Drawing2D.LinearGradientBrush(lColumns(c).Rgn, Color.FromArgb(92, 92, 92), Color.FromArgb(64, 64, 64), 90)
                    G.FillRectangle(bCol, lColumns(c).Rgn)
                    bCol.Dispose()

                    If lColumns(c).HL Then
                        Dim brTrans As New SolidBrush(Color.FromArgb(64, Color.SlateGray))
                        ' Dim pnTrans As New Pen(Color.FromArgb(128, Color.SlateGray))
                        G.FillRectangle(brTrans, New Rectangle(lColumns(c).Rgn.X, lColumns(c).Rgn.Y, lColumns(c).Rgn.Width, RgnView.Height + (RgnView.Y - lColumns(c).Rgn.Y)))
                        ' G.DrawRectangle(pnTrans, New Rectangle(lColumns(c).Rgn.X, lColumns(c).Rgn.Y, lColumns(c).Rgn.Width, RgnView.Height))
                        brTrans.Dispose()
                        ' pnTrans.Dispose()
                    End If

                    If SortField = lColumns(c).Title Then
                        DrawText(G, lColumns(c).Title & " *", sysfnt, clmfntbr, StringAlignment.Center, StringAlignment.Center, lColumns(c).Rgn, False)

                    Else
                        DrawText(G, lColumns(c).Title, sysfnt, clmfntbr, StringAlignment.Center, StringAlignment.Center, lColumns(c).Rgn, False)
                    End If

                End If
            Next


            If LoadDBAlpha.GetCurrent > 0 Then
                DrawStatus(G, LoadDBAlpha.GetCurrent, Color.Orange, Color.SlateGray, rfDM.sStatus, rfDM.Percentage)
            End If
            If SrchDBAlpha.GetCurrent > 0 Then
                DrawStatus(G, SrchDBAlpha.GetCurrent, Color.SlateGray, Color.Lime, "Executing query...", rfDM.Percentage)

                'Dim txtR As New Rectangle(RgnView.X + (RgnView.Width / 2) - (100), RgnView.Y + (RgnView.Height / 2) - (24), 200, 48)
                'Dim brTrans2 As New SolidBrush(Color.FromArgb((155 / 1000) * SrchDBAlpha.GetCurrent, Color.FromArgb(0, 0, 0)))
                'Dim pnTrans2 As New Pen(Color.FromArgb((255 / 1000) * SrchDBAlpha.GetCurrent, Color.FromArgb(0, 0, 0)))
                'Dim brTrans As New SolidBrush(Color.FromArgb((255 / 1000) * SrchDBAlpha.GetCurrent, Color.Silver))
                'G.FillRectangle(brTrans2, txtR)
                'G.DrawRectangle(pnTrans2, txtR)
                'Indicator.Reloc(New Rectangle(txtR.X - 16, txtR.Y - 8, 48, 14))
                'Indicator.Update()
                ''    Indicator.AlphaMax = SrchDBAlpha.GetCurrent
                'Indicator.SetHlColor(Color.Orange)
                'Indicator.Render(G)

                'DrawText(G, "Searching Database...", sysfnt, brTrans, StringAlignment.Center, StringAlignment.Center, txtR, False)
                'brTrans2.Dispose()
                'brTrans.Dispose()
                'pnTrans2.Dispose()
                ''  Indicator.Alpha = SrchDBAlpha.GetCurrent
            End If
            If LoadViewAlpha.GetCurrent > 0 Then
                DrawStatus(G, LoadViewAlpha.GetCurrent, Color.Lime, Color.White, "Prepairing View...", rfDM.Percentage)

                'Dim txtR As New Rectangle(RgnView.X + (RgnView.Width / 2) - (100), RgnView.Y + (RgnView.Height / 2) - (24), 200, 48)
                'Dim brTrans2 As New SolidBrush(Color.FromArgb((155 / 1000) * LoadViewAlpha.GetCurrent, Color.FromArgb(0, 0, 0)))
                'Dim pnTrans2 As New Pen(Color.FromArgb((255 / 1000) * LoadViewAlpha.GetCurrent, Color.FromArgb(0, 0, 0)))
                'Dim brTrans As New SolidBrush(Color.FromArgb((255 / 1000) * LoadViewAlpha.GetCurrent, Color.Silver))
                'G.FillRectangle(brTrans2, txtR)
                'G.DrawRectangle(pnTrans2, txtR)
                'Indicator.Reloc(New Rectangle(txtR.X - 16, txtR.Y - 8, 48, 14))
                'Indicator.Update()
                ''  Indicator.AlphaMax = LoadViewAlpha.GetCurrent
                'Indicator.SetHlColor(Color.Lime)
                'Indicator.Render(G)

                'DrawText(G, "Prepairing View...", sysfnt, brTrans, StringAlignment.Center, StringAlignment.Center, txtR, False)
                'brTrans2.Dispose()
                'brTrans.Dispose()
                'pnTrans2.Dispose()
                'Indicator.Alpha = LoadViewAlpha.GetCurrent
            End If
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Public Sub DrawStatus(G As Graphics, cAlpha As Single, HlColorA As Color, HlColorB As Color, Text As String, percentage As Integer)
        Try
            Dim txtR As New Rectangle(RgnView.X + (RgnView.Width / 2) - (150), RgnView.Y + (RgnView.Height / 2) - (24), 300, 48)
            Dim brTrans2 As New SolidBrush(Color.FromArgb((155 / 1000) * cAlpha, Color.FromArgb(0, 0, 0)))
            Dim pnTrans2 As New Pen(Color.FromArgb((255 / 1000) * cAlpha, Color.FromArgb(0, 0, 0)))
            Dim brTrans As New SolidBrush(Color.FromArgb((255 / 1000) * cAlpha, Color.Silver))
            G.FillRectangle(brTrans2, txtR)
            G.DrawRectangle(pnTrans2, txtR)
            Indicator.Reloc(New Rectangle(txtR.X - 24, txtR.Y - 16, 56, 14))

            Indicator.Update()
            '   Indicator.AlphaMax = LoadDBAlpha.GetCurrent
            Indicator.Percentage = percentage
            Indicator.SetHlColor(HlColorA, HlColorB)
            Indicator.Render(G)
            DrawText(G, Text, sysfnt, brTrans, StringAlignment.Center, StringAlignment.Center, txtR, False)
            brTrans2.Dispose()
            brTrans.Dispose()
            pnTrans2.Dispose()
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Public Sub DrawFadedLine(ByRef G As Graphics, R As Rectangle, Clr As Color, Angle As Single)
        Dim brf As New Drawing2D.LinearGradientBrush(R, Clr, Clr, Angle)
        Dim bl As New Drawing2D.ColorBlend
        bl.Colors = {Color.Transparent, Clr, Clr, Color.Transparent}
        bl.Positions = {0.0, 0.1, 0.9, 1.0}
        brf.InterpolationColors = bl
        G.FillRectangle(brf, R)
        brf.Dispose()
    End Sub

    Public Sub ResetCam()
        Cam.Pos.X = 0 : Cam.Pos.Y = lColumns(0).Rgn.Y + lColumns(0).Rgn.Height
    End Sub

    Public Sub Inf_Ms_Down(ByVal pMs As Point)
        Try
            ReLocate = True
            Cam.Inf_Ms_Down(pMs)

        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Public Sub Inf_Ms_Up(ByVal pMs As Point)
        Try
            ReLocate = True
            For c = 0 To lColumns.Count - 1
                If lColumns(c).Rgn.Contains(pMs) Then
                    SortField = lColumns(c).Title
                    PostQuery()
                    Exit For
                End If
            Next

            Cam.Inf_Ms_Up(pMs)

        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Public lSelItm As DP_Res
    Public Sub Inf_Ms_Move(ByVal pMs As Point)
        Try
            ReLocate = True

            Dim SelItm As DP_Res = GetItemAt(pMs)
            If lSelItm IsNot SelItm Then
                If lSelItm IsNot Nothing Then
                    lSelItm.HL = False
                End If
                If SelItm IsNot Nothing Then
                    SelItm.HL = True
                End If
                lSelItm = SelItm
            End If

            For c = 0 To lColumns.Count - 1
                If lColumns(c).Rgn.Contains(pMs) Then
                    lColumns(c).HL = True
                Else
                    lColumns(c).HL = False
                End If
            Next

            Cam.Inf_Ms_Move(pMs)

        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Public Sub Inf_Ms_Whl(ByVal Delta As Single)
        Try
            ReLocate = True
            Cam.Inf_Ms_Whl(Delta)

        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Private Sub Cam_UpdMovement() Handles Cam.UpdMovement
        RelocResults()
    End Sub

    Public Function GetItemAt(ByVal pLoc As Point) As DP_Res
        For c = 0 To Results.Count - 1
            '  If Results(c).Vis Then

            For x = 0 To Results(c).Results.Count - 1
                If Results(c).Results(x).Rgn.Contains(pLoc) Then
                    Return Results(c).Results(x)
                End If

                '  If Results(c).Results(x).Vis Then
                '  DrawText(G, Results(c).Results(x).IDX.ToString, sysfnt, brData, StringAlignment.Near, StringAlignment.Center, Results(c).Results(x).Rgn, False)
                'For v = 1 To lColumns.Count - 1
                '    Dim nxR As New Rectangle(lColumns(v).Rgn.X, Results(c).Results(x).Rgn.Y, lColumns(v).Rgn.Width, ItemHeight)
                'Next
            Next
        Next
        Return Nothing
    End Function

    Private Sub rfDM_OnFileLoaded(Result As Integer) Handles rfDM.OnFileLoaded
        If Result = 0 Then
            PostQuery()
            OnResizeEnd()
        End If
    End Sub

    Private Sub rfDM_OnFileSaved(Result As Integer) Handles rfDM.OnFileSaved
        'ernell.DP.PostQuery()
        If Result = 0 Then
            PostQuery()
            OnResizeEnd()
        End If
    End Sub

    Private Sub rfDM_ServeResults() Handles rfDM.ServeResults
        ShowResults()

        IsSearching = False
    End Sub
End Class

Public Class DP_Cam
    Public Pos As Point
    Public Event UpdMovement()

    Public Zoom As Single

    Private ZoomScale As Single = 1.0
    Private MouseMultiplier As Single = 10.0

    Private bCamMove As Boolean
    Private pLastMS As Point

    Public Function Reloc(ByVal pSrc As PointF) As PointF
        Return New PointF(Pos.X + (pSrc.X * ZoomScale), Pos.Y + (pSrc.Y * ZoomScale))
    End Function

    Public Function Reloc(ByVal rSrc As RectangleF) As RectangleF
        Return New RectangleF(Pos.X + (rSrc.X * ZoomScale), Pos.Y + (rSrc.Y * ZoomScale), rSrc.Width * ZoomScale, rSrc.Height * ZoomScale)
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
            'bHasMoved = False
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Public Sub Inf_Ms_Move(ByVal pMs As Point)
        Try
            If bCamMove Then
                Pos.Y += (pLastMS.Y - pMs.Y) * MouseMultiplier
                If Pos.Y >= 100 Then Pos.Y = 100 'UpperBound
                pLastMS = pMs
                RaiseEvent UpdMovement()
            End If
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

    Public Sub Inf_Ms_Whl(ByVal Delta As Single)
        Try
            If Delta < 0 Then
                Pos.Y -= 1
                RaiseEvent UpdMovement()
            Else
                Pos.Y += 1
                RaiseEvent UpdMovement()
            End If

        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub


End Class

Public Class DP_Column
    Inherits DP_Shared

    Public Title As String
    Public fType As String
End Class
Public Class DP_Row
    Inherits DP_Shared

End Class
Public Class DP_DrwDat
    Public Rgn As Rectangle
    Public RgnC() As Rectangle
    Public HL As Boolean
    Public Vis As Boolean
End Class

Public Class DP_Shared
    Public Alpha As New zzDynamix.CoreClasses.dynSingle
    Public HL As Boolean
    Public Rgn As Rectangle
End Class

Public Class DP_ResPage
    Public Rgn As Rectangle
    Public Vis As Boolean
    Public Results As New List(Of DP_Res)
End Class
Public Class DP_Res
    Public Rgn As Rectangle
    Public Vis As Boolean
    Public Data() As String
    Public IDX As Integer
    Public HL As Boolean
    Public Sub New(ByRef rfData As zzDataMgr.DataRow)
        Data = rfData.Data

    End Sub
End Class

Public Enum PresentStage
    FadeIN = 0
    Visible = 1
    StartSwitch = 2
    FadeOut = 3
    DoSwitch = 4
End Enum

Public Class ProIndicator
    Public Rgn As Rectangle
    Public pCenter As Point

    Public Sgm(19) As ProSegment
    Public pAng As Single
    Public rAng As Single = 8
    Public rAng2 As Single = 12
    Public rDir As Boolean
    Public rCur As Single

    Public AlphaMax As Integer = 0
    Public BaseClr As Color = Color.White



    Public Sub New()
        For c = 0 To UBound(Sgm)
            Sgm(c) = New ProSegment
        Next
    End Sub

    Public Sub Update()
        pCenter = New Point(Rgn.X + (Rgn.Width / 2), Rgn.Y + (Rgn.Height / 2))
        'Array.Copy(pArr, 0, pArr, 1, pArr.Length - 1)
        'Dim pN As New PointF(pCenter.X + (rAng * Math.Sin(pAng * RAD)), pCenter.Y + (rAng * Math.Cos(pAng * RAD)))
        'pArr(0) = pN
        Dim nxAng As Single = pAng
        Dim nxAng2 As Single = pAng - 5
        Dim alp As Single = AlphaMax
        Dim alpU As Single = 1000 / Sgm.Length
        Dim nxalp As Single = 0
        For c = 0 To UBound(Sgm)
            'If c > 0 Then
            '    If Not Sgm(c).Radius = Sgm(c - 1).Radius Then
            '        If Sgm(c).Radius < Sgm(c - 1).Radius Then
            '            Sgm(c).Radius += 0.5
            '        ElseIf Sgm(c).Radius > Sgm(c - 1).Radius Then
            '            Sgm(c).Radius -= 0.5
            '        End If
            '    End If
            'End If
            Sgm(c).Radius = rAng
            Sgm(c).Poly(0) = New PointF(pCenter.X + (Sgm(c).Radius * Math.Sin(nxAng * RAD)), pCenter.Y + (Sgm(c).Radius * Math.Cos(nxAng * RAD)))
            Sgm(c).Poly(1) = New PointF(pCenter.X + (Sgm(c).Radius * Math.Sin(nxAng2 * RAD)), pCenter.Y + (Sgm(c).Radius * Math.Cos(nxAng2 * RAD)))
            Sgm(c).Poly(2) = New PointF(pCenter.X + ((Sgm(c).Radius + 5) * Math.Sin(nxAng2 * RAD)), pCenter.Y + ((Sgm(c).Radius + 5) * Math.Cos(nxAng2 * RAD)))
            Sgm(c).Poly(3) = New PointF(pCenter.X + ((Sgm(c).Radius + 5) * Math.Sin(nxAng * RAD)), pCenter.Y + ((Sgm(c).Radius + 5) * Math.Cos(nxAng * RAD)))


            If alp <= 0 Then
                Sgm(c).Clr = Color.Transparent
            Else
                Sgm(c).Clr = Color.FromArgb((255 / 1000) * alp, BaseClr)
            End If

            alp -= alpU
            nxAng -= 5
            nxAng2 = nxAng - 5
            If nxAng > 360 Then nxAng -= 360
            If nxAng < 0 Then nxAng += 360
        Next


        pAng += 2
        If pAng > 360 Then pAng -= 360


        'If rDir Then
        '    rCur += 1
        '    If rCur > rAng Then rDir = False
        'Else
        '    rCur -= 1
        '    If rCur <= 0 Then rDir = True
        'End If

    End Sub

    Public Sub Render(ByRef G As Graphics)
        Dim tsm As Drawing2D.SmoothingMode = G.SmoothingMode
        G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        For c = 0 To UBound(Sgm)
            Sgm(c).Render(G)
        Next
        G.SmoothingMode = tsm
    End Sub

End Class
Public Class ProSegment
    Public Poly(3) As PointF
    Public Clr As Color
    Public Radius As Single
    Public Sub Render(ByRef G As Graphics)
        '  If Poly IsNot Nothing Then
        'If UBound(Poly) > 0 Then
        Dim nwB As New SolidBrush(Clr)
        G.FillPolygon(nwB, Poly)
        nwB.Dispose()
        ' End If
        ' End If
    End Sub
End Class