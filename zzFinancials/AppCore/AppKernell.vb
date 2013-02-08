Imports zLib_DynVar
Imports System.Text

''' <summary>
''' System's core.
''' </summary>
Public Module SysMain
    Public Kernell As New AppKernell
    Public Log As New EzLog
    Public bRealTimeSearch As Boolean = False

    ''' <summary>
    ''' Application entrypoint
    ''' </summary>
    Public Sub Main()
        Windows.Forms.Application.EnableVisualStyles()

        Dim c As ConsoleColor
        Log.Add("Initializing ^2zz^1Financials^0...", 0)

        Kernell.Initialize()
        Kernell.InitAcc("Test")

        Log.Add("Initialization ^3OK^0, welcome to ^2zz^1Financials^0...", , 0)


        Windows.Forms.Application.Run()
    End Sub

End Module

''' <summary>
''' System's core application manager.
''' </summary>
Public Class AppKernell
    Public guiDataview As wDataview

    Public ActiveLogbook As String = ""
    Public WithEvents DM As zzDataMgr.DataManager
    Public Const DM_Header As String = "#created|datetime|mutationdate|date|amount|currency|description|text|label|text"
    'Public Const DM_Header2 As String = "#timestamp|time|amount|currency|description|text|label|text"

    Public WithEvents DMCAT As zzDataMgr.DataManager
    Public Const DMCAT_Header As String = "#name|text|color|text"

    Public WithEvents DMLOG As zzDataMgr.DataManager
    Public Const DMLOG_Header As String = "#created|datetime|message|text|type|text"
    Public bExceptionState As Boolean = False

    Public DP As DataPresenter
    Public LBM As LogBookMgr
    Public DV As DV_Mgr
    ' Public MsgQue As New guiMsgQue

    Public LogBooks As New List(Of String)

    Public pthBase As String = Application.StartupPath & "\"
    Public pthBaseAcc As String = ""
    Public AccountName As String = "Dev"

    Public Function AddLog(sMsg As String)
        DMLOG.TryParse(Now.ToShortDateString & " " & Now.ToShortTimeString & "|" & sMsg & "|informative")
    End Function

    Public Function AddLog(Ex As Exception)
        DMLOG.TryParse(Now.ToShortDateString & " " & Now.ToShortTimeString & "|" & Ex.ToString & "|exception")
        bExceptionState = True
    End Function

    Public Function GetSet(Path As String, Key As String, DefaultValue As String) As String
        Return GetSetting("zzFinancials", Path, Key, DefaultValue)
    End Function
    Public Sub SetSet(Path As String, Key As String, Value As String)
        SaveSetting("zzFinancials", Path, Key, Value)
    End Sub

    Public Sub Initialize()
        Try
            'dbMain = New BaseDB
            'dbMain.InitTest()
            pthBase = Application.StartupPath & "\"

            DMLOG = New zzDataMgr.DataManager
            DMLOG.TryParse(DMLOG_Header)

            DMCAT = New zzDataMgr.DataManager
            DMCAT.TryParse(DMCAT_Header)

            DM = New zzDataMgr.DataManager
            DM.TryParse(DM_Header)

            DP = New DataPresenter
            DP.LinkDM(DM)
            DP.SetRGN(New Rectangle(0, 100, 600, 300))
            '600, 400
            LBM = New LogBookMgr

            'SF = New zzSuperForms
            'SF.load()
            ' InitAcc("Dev")
        Catch ex As Exception
            'Console.WriteLine(ex.ToString)
        End Try

    End Sub

    Public Sub Updates()
        ' MsgQue.Update()
        ' A code change
        ' MsgBox("lol")
    End Sub

    Public Function Rebuild_LocalTree() As String
        Dim treeScript As New StringBuilder
        With treeScript
            .AppendLine("Actions|actions||root|actionc")
            .AppendLine("Quick Register...|quickreg||root/actions|moneyn")
            .AppendLine("New Logbook...|newlb||root/actions|databasen")
            .AppendLine("New Filter...|newfilter||root/actions|filtern")
            .AppendLine("Export Data|export||root/actions|exportc")
            .AppendLine("Export as Text...|export:txt||root/actions/export|export")
            .AppendLine("Export as XML...|export:xml||root/actions/export|export")
            .AppendLine("Export as HTML...|export:html||root/actions/export|export")
            .AppendLine("Export as CSV...|export:csv||root/actions/export|export")

            .AppendLine("Developer Tools|tools||root/actions|actionc")
            .AppendLine("Data Generator|tool:datagen||root/actions/tools|action")
            .AppendLine("Memory Graph|tool:memgraph||root/actions/tools|action")
            .AppendLine("Test Data Graph|tool:TestGraphData||root/actions/tools|action")

            .AppendLine("LogBooks|logbooks||root|databasec")
            LogBooks = Enlist_Logbooks(pthBase, ".lbk")
            For Each LB As String In LogBooks
                Dim xpth As String = ExtrBasePath(LB, pthBase)
                If InStr(xpth, "\") > 0 Then
                    Dim apth() As String = xpth.Split("\")
                    .AppendLine(apth(0) & "|lbdir:" & apth(0) & "||root/logbooks|databased")
                    .AppendLine(ExtrFileName(LB) & "|logbook:" & LB & "||root/logbooks/lbdir:" & apth(0) & "|database")
                End If
            Next
            For Each LB As String In LogBooks
                Dim xpth As String = ExtrBasePath(LB, pthBase)
                If Not InStr(xpth, "\") > 0 Then
                    .AppendLine(ExtrFileName(LB) & "|logbook:" & LB & "||root/logbooks|database")
                End If
            Next
            .AppendLine("New Logbook...|newlb||root/logbooks|databasen")

            .AppendLine("Filters|filters||root|filterc")
            .AppendLine("Time and Date|timedate||root/filters|filterc")
            .AppendLine("Current month|filter:cmonth||root/filters/timedate|filter")
            .AppendLine("Last month|filter:lmonth||root/filters/timedate|filter")
            .AppendLine("Labels|labels||root/filters|filterc")
            .AppendLine("Some labelA|filter:lbla||root/filters/labels|filter")
            .AppendLine("Some labelB|filter:lblb||root/filters/labels|filter")
            .AppendLine("New Filter...|newfilter||root/filters|filtern")






        End With

        Return treeScript.ToString
    End Function


    Public Sub ResultsToGraph()
        Try
            Dim total As Integer = 0
            For rp = 0 To DP.Results.Count - 1
                For r = 0 To DP.Results(rp).Results.Count - 1
                    total += 1
                Next
            Next

            If total > 100 Then
                Dim hop As Integer = total / 100
                For rp = 0 To DP.Results.Count - 1
                    For r = 0 To DP.Results(rp).Results.Count - 1 Step hop
                        guiDataview.CustGraph.AddNode(0, DP.Results(rp).Results(r).Data(2))
                    Next
                Next
            Else
                For rp = 0 To DP.Results.Count - 1
                    For r = 0 To DP.Results(rp).Results.Count - 1
                        guiDataview.CustGraph.AddNode(0, DP.Results(rp).Results(r).Data(2))
                    Next
                Next
            End If


        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub


    Public Sub InitAcc(ByVal nwAccountName As String)
        AccountName = nwAccountName
        pthBaseAcc = pthBase & AccountName & "\"
        CheckPath(pthBaseAcc)



        ' SF.SwitchInst("Main", True)
        ' Kernell.MsgQue.Que.Add(New MsgQueItm("Loading catagories..."))
        DMCAT.FromFile(pthBaseAcc & "catagory.db")
        DMCAT.ExecQuery("")



        ' Kernell.MsgQue.Que.Add(New MsgQueItm("Loading logbook..."))
        ' DM.FromFile(pthBaseAcc & "Logbook001.lbk")
        'DM.FromFile("DevLogbook.lbk")
        DM.ExecQuery("")

        DP.ShowResults()

        'Dim f As New fMain
        'f.Show()

        guiDataview = New wDataview
        guiDataview.Location = New Point(GetSet(AccountName, "dataview_win_x", (Screen.PrimaryScreen.Bounds.Width / 2) - (300)), GetSet(AccountName, "dataview_win_y", (Screen.PrimaryScreen.Bounds.Height / 2) - (200)))
        guiDataview.Size = New Size(GetSet(AccountName, "dataview_win_w", 600), GetSet(AccountName, "dataview_win_h", 400))
        guiDataview.Show()
        guiDataview.UpdOnResize()
    End Sub

    Public Sub ListCatagories(ByRef ObjListView As ListView)
        Try
            ObjListView.Items.Clear()
            DMCAT.ExecQuery("sort name 1")
            For Each DR As zzDataMgr.DataRow In DMCAT.Results
                Dim nwI As New ListViewItem
                Dim cl As Color
                Dim acl() As String = DR.Data(1).Split(",")
                cl = Color.FromArgb(acl(0), acl(1), acl(2))
                With nwI
                    .Text = DR.Data(0)
                    .ForeColor = cl
                End With
                ObjListView.Items.Add(nwI)
            Next

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub


    'Private Sub DM_OnFileLoaded() Handles DM.OnFileLoaded

    '    Kernell.MsgQue.Que.Add(New MsgQueItm("Logbook loaded."))
    'End Sub

    'Private Sub DMCAT_OnFileLoaded() Handles DMCAT.OnFileLoaded
    '    Kernell.MsgQue.Que.Add(New MsgQueItm("Catagories loaded."))
    'End Sub

    Private Sub DM_OnFileLoaded(ByVal Result As Integer) Handles DM.OnFileLoaded
        'If Result = 0 Then
        '    Kernell.MsgQue.Que.Add(New MsgQueItm("Logbook loaded successfully."))
        'Else
        '    Kernell.MsgQue.Que.Add(New MsgQueItm("Failed to load Logbook !"))
        'End If
    End Sub

    Private Sub DMCAT_OnFileLoaded(ByVal Result As Integer) Handles DMCAT.OnFileLoaded
        'If Result = 0 Then
        '    Kernell.MsgQue.Que.Add(New MsgQueItm("Catagories loaded successfully."))
        'Else
        '    Kernell.MsgQue.Que.Add(New MsgQueItm("Failed to load catagories !"))
        'End If
    End Sub




    





End Class

''' <summary>
''' System's Shared Functions and resources.
''' </summary>
Public Module SysShared




#Region "Graphics and math Stuff"
    Public Rnd As New Random
    Public sysfnt As New Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel)
    Public sysfntbrs As New SolidBrush(Color.FromArgb(0, 0, 0)) 'Shadowbrush for function DrawText
    Public Const RAD As Double = Math.PI / 180                   'Radians multiplier

    Private cData() As String = {"a", "b", "c", "d", "e", "f", "g", _
                                 "h", "i", "j", "k", "l", "m", "n", "o", "p", _
                                 "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", " "}

    Public Function RndName(ByVal MinSize As Integer, ByVal MaxSize As Integer) As String
        Dim nxLen As Integer = MinSize + (rnd.Next(1, MaxSize - MinSize))
        Dim sb As New System.Text.StringBuilder
        For c = 1 To nxLen
            sb.Append(cData(rnd.Next(0, UBound(cData))))
        Next
        Return sb.ToString
    End Function

    'Public Sub DrawText(ByRef G As Graphics, ByVal Text As String, ByRef Fnt As Font, ByRef Br As SolidBrush, ByVal Align As StringAlignment, ByVal LineAlign As StringAlignment, ByVal Rgn As Rectangle, Optional ByVal UseShadow As Boolean = True)
    '    Try
    '        Dim sf As New StringFormat
    '        sf.Alignment = Align
    '        sf.LineAlignment = LineAlign
    '        sf.Trimming = StringTrimming.Character
    '        If Rgn.Width <> 0 And Rgn.Height <> 0 Then
    '            If UseShadow Then G.DrawString(Text, Fnt, sysfntbrs, New Rectangle(Rgn.X + 1, Rgn.Y + 1, Rgn.Width, Rgn.Height), sf)
    '            G.DrawString(Text, Fnt, Br, Rgn, sf)
    '        End If
    '    Catch ex As Exception
    '        Console.WriteLine(ex.ToString)
    '    End Try
    'End Sub

    Public Sub DrawText(ByRef G As Graphics, ByVal Text As String, ByRef Fnt As Font, ByRef Br As SolidBrush, ByVal AlignHorizontal As StringAlignment, ByVal AlignVertical As StringAlignment, ByVal Rgn As RectangleF, Optional ByVal UseShadow As Boolean = True)
        Try
            If Rgn.Width <> 0 And Rgn.Height <> 0 Then
                Using sf As New StringFormat
                    sf.Alignment = AlignHorizontal
                    sf.LineAlignment = AlignVertical
                    If UseShadow Then
                        Dim trgAlpha As Single = Br.Color.A
                        trgAlpha -= 128
                        If trgAlpha < 0 Then trgAlpha = 0
                        Using SBR As New SolidBrush(Color.FromArgb(trgAlpha, 0, 0, 0))
                            G.DrawString(Text, Fnt, SBR, New Rectangle(Rgn.X + 1, Rgn.Y + 1, Rgn.Width, Rgn.Height), sf)
                        End Using
                    End If
                    G.DrawString(Text, Fnt, Br, Rgn, sf)
                End Using
            End If
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

#End Region

#Region "Disk and Files Stuff"

    Public Sub CheckPath(ByVal sPath As String)
        Dim aPth() As String = sPath.Split("\")
        Dim pth As String
        For c = 0 To UBound(aPth)
            pth = BuildPath(aPth, c)
            If Not IO.Directory.Exists(pth) Then
                Try
                    MkDir(pth)
                    Log.Add("Created directory " & pth)
                Catch ex As Exception
                    Log.AddErr(ex)
                End Try

            End If
        Next
    End Sub

    Public Function BuildPath(ByVal aPath() As String, ByVal ToIDX As Integer) As String
        Dim r As New System.Text.StringBuilder
        r.Append(aPath(0) & "\")
        For c = 1 To ToIDX
            r.Append(aPath(c) & "\")
        Next
        Return r.ToString
    End Function

    Public Function ExtrFileName(ByVal sFullPath As String) As String
        Try
            Dim aPth() As String = sFullPath.Split("\")

            Dim File As String = aPth(UBound(aPth))
            If InStr(File, ".") > 0 Then
                File = Mid(File, 1, InStr(File, ".") - 1)
            End If
            Return File
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return sFullPath
        End Try
    End Function

    Public Function ExtrBasePath(ByVal sFullPath As String, ByVal sBasePath As String) As String
        Try
            If sFullPath.Length > sBasePath.Length Then

                Return Mid(sFullPath, sBasePath.Length + 1, sFullPath.Length - sBasePath.Length)
            Else
                Return sFullPath
            End If



        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return sFullPath
        End Try
    End Function

    Public Function Enlist_Logbooks(BasePath As String, ExtPattern As String) As List(Of String)
        Dim lQ As New List(Of String)
        Dim lR As New List(Of String)
        Dim lD(), lF() As String
        lQ.Add(BasePath)
        Dim cPth As String
        Do
            cPth = lQ(0)
            lQ.RemoveAt(0)

            Try
                lD = IO.Directory.GetDirectories(cPth)
                If lD IsNot Nothing Then
                    If lD.Length > 0 Then
                        For c = 0 To UBound(lD)
                            lQ.Add(lD(c))
                        Next
                    End If
                End If
            Catch ex As Exception
                Console.WriteLine(ex.ToString)
            End Try
            Try
                lF = IO.Directory.GetFiles(cPth)
                If lF IsNot Nothing Then
                    If lF.Length > 0 Then
                        For c = 0 To UBound(lF)
                            If LCase(Mid(lF(c), lF(c).Length - ExtPattern.Length + 1, ExtPattern.Length)) = LCase(ExtPattern) Then
                                lR.Add(lF(c))
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                Console.WriteLine(ex.ToString)
            End Try


        Loop Until lQ.Count = 0
        Return lR
    End Function

#End Region





End Module