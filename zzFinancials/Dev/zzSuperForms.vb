'Imports System.Runtime.InteropServices

'Public Class zzSuperForms

'    Public Instances As New List(Of SFInstance)

'    Private WithEvents UpdTimer As Timer
'    Private UpdThr As Threading.Thread
'    Private UpdThrAlive As Boolean
'    Private UpdState As UpdateStates

'    Public fTypes(99) As Object
'    Public fNames(99) As String
'    Public rID As Integer = 0

'    Public Const RgnsIDX As Integer = 7 'How much precalculated reigions there are in the array (See: Enum RgnIDs)
'    Public WinSize As Size = New Size(300, 400)
'    Public SpawnSize As Size = New Size(2, 2)
'    Public IcoSize As Size = New Size(56, 56)
'    Public rWindow As Rectangle = Centerize(WinSize)
'    Public rSpawn As Rectangle = Centerize(SpawnSize)

'    Public Anim_Speed_Alpha As Integer = 2
'    Public Anim_Speed_Rgn As Integer = 2

'    Public Sub New()
'        rID = 0
'        ReDim fTypes(rID)
'        ReDim fNames(rID)

'        ' AddHandler System.Windows.Forms.Application.Idle, New EventHandler(AddressOf AppIdle)
'        UpdTimer = New Timer
'        UpdTimer.Interval = 1
'        UpdTimer.Start()

'        UpdThr = New Threading.Thread(AddressOf UpdProc)
'        UpdThr.IsBackground = False
'        UpdThr.Priority = Threading.ThreadPriority.Highest
'        UpdThr.Start()
'    End Sub

'    Public Function Centerize(ByVal FromSize As Size) As Rectangle
'        Return New Rectangle((Screen.PrimaryScreen.Bounds.Width / 2) - (FromSize.Width / 2), (Screen.PrimaryScreen.Bounds.Height / 2) - (FromSize.Height / 2), FromSize.Width, FromSize.Height)
'    End Function

'    Public Sub UpdProc()
'        UpdThrAlive = True
'        Do While UpdThrAlive
'            If UpdState = UpdateStates.Begin_UpdThr Then
'                UpdateMT()
'                UpdState = UpdateStates.Begin_Upd
'            Else
'                'Threading.Thread.Sleep(1)
'            End If
'        Loop

'    End Sub

'    'Public Sub AppIdle()
'    '    Do While _AppIsIdle()
'    '        Update()
'    '        '  Threading.Thread.Sleep(100)
'    '    Loop
'    'End Sub

'    Public Sub load()
'        'Register used forms
'        Reg("acc", New fAccount)
'        Reg("main", New frmMain)
'        Reg("quick", New frmQuickReg)
'        Reg("cat", New fCatagories)
'        Reg("view", New fViewer)
'        Reg("load", New fLoader)
'        Reg("filter", New fFilter)
'        Reg("logbook", New fLogBooks)
'        Reg("rapport", New fRapportED)
'        Reg("graph", New fGraph)
'        Reg("dev", New fDevTools)

'        'Create instances
'        MkInstance("Acc", "acc")
'        MkInstance("Main", "main")
'        MkInstance("Quick", "quick")
'        MkInstance("Cat", "cat")
'        MkInstance("View", "view")
'        MkInstance("Load", "load")
'        MkInstance("FilterView", "filter")
'        MkInstance("LogBooks", "logbook")
'        MkInstance("Rapports", "rapport")
'        MkInstance("Graph", "graph")
'        MkInstance("Dev", "dev")

'        GetInst("View").rCustWin = Centerize(New Size(600, 400))
'        GetInst("View").flgCustWin = True
'        GetInst("FilterView").rCustWin = Centerize(New Size(600, 400))
'        GetInst("FilterView").flgCustWin = True
'        GetInst("Graph").rCustWin = Centerize(New Size(600, 400))
'        GetInst("Graph").flgCustWin = True

'        GetInst("Acc").State = SF_States.Active
'    End Sub

'    Public Sub MkInstance(ByVal Name As String, ByVal FormID As String)
'        Dim nwI As New SFInstance

'        With nwI
'            .Name = Name
'            .Link(Me, FormID)
'            .fRgn.Setup(rSpawn, rSpawn, CSng(1 / Anim_Speed_Rgn), CSng(1 / (Anim_Speed_Rgn / 2)), 1)
'            .fAlpha.Setup(0, 0, 1 / Anim_Speed_Alpha, 1 / Anim_Speed_Alpha, 1)
'            .Show()

'            ReDim .Rgns(RgnsIDX)
'        End With
'        Instances.Add(nwI)
'    End Sub

'    Public Function SpawnForm(ByVal Name As String) As Object
'        For c = 0 To fNames.Length - 1
'            If fNames(c) = Name Then
'                Dim rInst As Object = Activator.CreateInstance(fTypes(c))
'                PrepForm(rInst)
'                'Log.Add("Created new instance of formtype ^1" & fTypes(rID).FullName & "^0.")
'                Return rInst
'            End If
'        Next
'        Return Nothing
'    End Function

'    Public Sub Reg(ByVal Name As String, ByRef FormInst As Object)
'        Try
'            ReDim Preserve fTypes(rID)
'            ReDim Preserve fNames(rID)

'            fTypes(rID) = FormInst.GetType
'            fNames(rID) = Name

'            '  Log.Add("Registered formtype ^1" & fTypes(rID).FullName & "^0 under id ^3" & fNames(rID) & "^0.")
'            rID += 1

'        Catch ex As Exception
'            ' Log.Add(ex.ToString)
'        End Try

'        'FormInst.close()
'        'FormInst.dispose()
'        'FormInst = Nothing
'    End Sub

'    Public Sub PrepForm(ByRef ObjForm As Object)
'        If ObjForm IsNot Nothing Then
'            With ObjForm
'                .FormBorderStyle = FormBorderStyle.None
'                .BackColor = Color.FromArgb(64, 64, 64)
'                .ShowInTaskbar = False
'                .ShowIcon = False
'                .ControlBox = False
'                .Opacity = 0
'            End With
'        End If
'    End Sub

'    Public lInst As String = ""
'    Public lID As Integer = -1
'    Public Sub SwitchInst(ByVal nwInst As String, Optional ByVal KillOld As Boolean = False)
'        Dim id As Integer = 0
'        For Each I As SFInstance In Instances
'            If I.Name = nwInst Then
'                If I.State = SF_States.Hidden Then
'                    I.Rgns(RgnIDs.rSpawn) = rSpawn
'                    I.fRgn.Setup(I.Rgns(RgnIDs.rSpawn), I.Rgns(RgnIDs.rSpawn))
'                End If
'                I.State = SF_States.Active
'                I.ObjFrm.Focus()
'                lID = id
'                lInst = nwInst
'            Else
'                If KillOld And Not I.Name = "Load" Then
'                    If id < lID Then
'                        If I.Name = lInst Then
'                            I.State = SF_States.Hidden
'                        Else
'                            If Not I.State = SF_States.Hidden And Not I.State = SF_States.Iconized And Not I.State = SF_States.StatusBar And Not I.State = SF_States.StatusBarEx And Not I.State = SF_States.VerticalBar Then I.State = SF_States.ToIconized
'                        End If
'                    Else
'                        I.State = SF_States.Hidden
'                    End If

'                Else
'                    If Not I.State = SF_States.Hidden And Not I.State = SF_States.Iconized And Not I.State = SF_States.StatusBar And Not I.State = SF_States.StatusBarEx And Not I.State = SF_States.VerticalBar Then I.State = SF_States.ToIconized
'                End If

'            End If
'            id += 1
'        Next

'    End Sub

'    Public Sub ShowLoader()
'        For Each I As SFInstance In Instances
'            If I.Name = "Load" Then
'                If I.State <> SF_States.Iconized Then
'                    I.State = SF_States.Iconized
'                End If
'            End If
'        Next
'    End Sub
'    Public Sub HideLoader()
'        For Each I As SFInstance In Instances
'            If I.Name = "Load" Then
'                If I.State <> SF_States.Hidden Then
'                    I.State = SF_States.Hidden
'                End If
'            End If
'        Next
'    End Sub

'    Public Sub Update()
'        For Each I As SFInstance In Instances
'            I.Update()
'        Next
'    End Sub

'    Public Sub UpdateMT()

'        Dim nwWindow As Rectangle = Centerize(WinSize)

'        Dim flgSet As Boolean = False
'        If lInst <> "" Then
'            If GetInst(lInst) IsNot Nothing Then
'                If GetInst(lInst).flgCustWin Then
'                    nwWindow = GetInst(lInst).rCustWin
'                    flgSet = True
'                End If
'            End If
'        End If
'        rWindow = nwWindow


'        ' Dim pIcon As New Point(rWindow.X + CountIconWidth(), rWindow.Y - IcoSize.Height)
'        Dim pIcon As New Point(rWindow.X - IcoSize.Height, rWindow.Y)

'        For Each I As SFInstance In Instances

'            With I
'                I.Rgns(RgnIDs.rVisible) = rWindow
'                I.UpdateMT()

'                Select Case I.State
'                    Case SF_States.Active
'                        ' I.rIcon = New Rectangle(pIcon.X, pIcon.Y, IcoSize.Width, IcoSize.Height)
'                        ' I.rToIcon = New Rectangle(pIcon.X, pIcon.Y + IcoSize.Height, IcoSize.Width, rWindow.Height)

'                        .Rgns(RgnIDs.rIcon) = New Rectangle(pIcon.X, pIcon.Y, IcoSize.Width, IcoSize.Height)
'                        .Rgns(RgnIDs.rToIcon) = New Rectangle(pIcon.X, pIcon.Y + IcoSize.Height, IcoSize.Width, rWindow.Height)
'                        ' rSpawn = rWindow

'                        'pIcon.X += IcoSize.Width
'                    Case SF_States.Hidden
'                        'I.rIcon = New Rectangle(pIcon.X, pIcon.Y, 1, 1)
'                        ' I.rToIcon = New Rectangle(pIcon.X, pIcon.Y + IcoSize.Height, IcoSize.Width, rWindow.Height)
'                        .Rgns(RgnIDs.rIcon) = New Rectangle(pIcon.X, pIcon.Y, 1, 1)
'                        .Rgns(RgnIDs.rToIcon) = New Rectangle(pIcon.X, pIcon.Y + IcoSize.Height, IcoSize.Width, rWindow.Height)
'                        .Rgns(RgnIDs.rHidden) = New Rectangle(.Rgns(RgnIDs.rVisible).X + .Rgns(RgnIDs.rVisible).Width, .Rgns(RgnIDs.rVisible).Y, 1, .Rgns(RgnIDs.rVisible).Height)
'                    Case SF_States.Iconized
'                        ' I.rIcon = New Rectangle(pIcon.X, pIcon.Y, IcoSize.Width, IcoSize.Height)
'                        'I.rToIcon = New Rectangle(pIcon.X, pIcon.Y + IcoSize.Height, IcoSize.Width, rWindow.Height)
'                        .Rgns(RgnIDs.rIcon) = New Rectangle(pIcon.X, pIcon.Y, IcoSize.Width, IcoSize.Height)
'                        .Rgns(RgnIDs.rToIcon) = New Rectangle(pIcon.X, pIcon.Y + IcoSize.Height, IcoSize.Width, rWindow.Height)

'                        pIcon.Y += IcoSize.Width

'                    Case SF_States.StatusBar
'                        ' I.rStatus = New Rectangle(rWindow.X, rWindow.Y + rWindow.Height, rWindow.Width, 18)
'                        .Rgns(RgnIDs.rStatus) = New Rectangle(rWindow.X, rWindow.Y + rWindow.Height, rWindow.Width, 1)
'                    Case SF_States.StatusBarEx
'                        'I.rStatusEx = New Rectangle(rWindow.X, rWindow.Y + rWindow.Height, rWindow.Width, 54)
'                        .Rgns(RgnIDs.rStatusEx) = New Rectangle(rWindow.X, rWindow.Y + rWindow.Height, rWindow.Width, 54)
'                    Case SF_States.VerticalBar
'                        'I.rVertical = New Rectangle(rWindow.X - 16, rWindow.Y, 16, rWindow.Height)
'                        .Rgns(RgnIDs.rVertical) = New Rectangle(rWindow.X - 16, rWindow.Y, 16, rWindow.Height)
'                End Select
'            End With

'        Next
'    End Sub
'    Public Function CountIconWidth()
'        Dim r As Integer = -IcoSize.Width
'        For Each I As SFInstance In Instances
'            If I.State = SF_States.Iconized Then
'                r += I.Rgns(RgnIDs.rIcon).Width
'            End If
'        Next
'        Return r
'    End Function
'    Public Function GetInst(ByVal ByName As String) As SFInstance
'        For Each I As SFInstance In Instances
'            If I.Name = ByName Then
'                Return I
'            End If

'        Next
'        Return Instances(0)
'    End Function

'    Public Shared PM_NOREMOVE As UInteger = &H0
'    <System.Security.SuppressUnmanagedCodeSecurity()> _
'    Private Declare Function PeekMessage Lib "User32.dll" Alias "PeekMessageA" ( _
'        ByRef msg As MessageStruct, _
'        ByVal hWnd As IntPtr, _
'        ByVal messageFilterMin As Integer, _
'        ByVal messageFilterMax As Integer, _
'        ByVal flags As Integer _
'     ) As Integer

'   Protected Function _AppIsIdle() As Boolean
'        Dim msg As MessageStruct
'        Return (PeekMessage(msg, IntPtr.Zero, 0, 0, 0) = 0)
'    End Function


'    Private Sub UpdTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdTimer.Tick
'        If UpdState = UpdateStates.Begin_Upd Then
'            Update()
'            UpdState = UpdateStates.Begin_UpdThr
'        End If
'    End Sub
'End Class

'Public Enum UpdateStates
'    Begin_UpdThr = 0
'    Begin_Upd = 1
'End Enum

'Public Class SFInstance
'    Public rfMgr As zzSuperForms = Nothing
'    Public Name As String
'    Public FormID As String

'    Public ObjFrm As Object = Nothing
'    Public State As SF_States = SF_States.Hidden

'    Public zzD As New zzDynamix.SuperClasses.dynObject
'    Public fRgn As New zzDynamix.CoreClasses.dynRectangle2
'    Public fAlpha As New zzDynamix.CoreClasses.dynSingle

'    Public Rgns() As Rectangle
'    'Public rSpawn As Rectangle
'    'Public rVisible As Rectangle
'    'Public rIcon As Rectangle
'    'Public rToIcon As Rectangle
'    'Public rStatus As Rectangle
'    'Public rStatusEx As Rectangle
'    'Public rVertical As Rectangle

'    Public rCustWin As Rectangle
'    Public flgCustWin As Boolean

'    Public Sub New()
'        zzD.Initialize(True, False, True)
'    End Sub

'    Public Sub Link(ByRef RefMgr As zzSuperForms, ByRef nFormID As String)
'        FormID = nFormID
'        rfMgr = RefMgr
'    End Sub

'    Public Sub Show()
'        If rfMgr IsNot Nothing And ObjFrm Is Nothing Then
'            ObjFrm = rfMgr.SpawnForm(FormID)
'            ObjFrm.Show()
'        End If
'    End Sub

'    Public Sub Hide()
'        If rfMgr IsNot Nothing And ObjFrm IsNot Nothing Then
'            ObjFrm.Close()
'            ObjFrm.Dispose()
'            ObjFrm = Nothing
'        End If
'    End Sub

'    Public Sub Update()

'        If rfMgr IsNot Nothing And ObjFrm IsNot Nothing Then
'            Try
'                ObjFrm.suspendlayout()




'                ObjFrm.Location = zzD.dRgn.GetCurrent.Location
'                ObjFrm.Size = zzD.dRgn.GetCurrent.Size

'                If zzD.dAlp.GetCurrent > 0 Then
'                    ObjFrm.Opacity = zzD.dAlp.GetCurrent / 1000
'                Else
'                    ObjFrm.Opacity = 0
'                End If



'                ObjFrm.resumelayout()
'            Catch ex As Exception
'                Console.WriteLine(ex.ToString)
'            End Try


'        End If

'    End Sub
'    Public Sub UpdateMT()

'        Select Case State
'            Case SF_States.Active

'                zzD.dRgn.Setup(Rgns(RgnIDs.rVisible))
'                zzD.dAlp.Setup(1000)

'                'fRgn.Setup(rVisible)
'                'fAlpha.Setup(1000)

'                If zzD.dRgn.GetCurrent = Rgns(RgnIDs.rVisible) And zzD.dAlp.GetCurrent = 1000 Then
'                    ObjFrm.disableinput = False
'                Else
'                    ObjFrm.disableinput = True
'                End If
'            Case SF_States.ToIconized
'                zzD.dRgn.Setup(Rgns(RgnIDs.rToIcon))
'                zzD.dAlp.Setup(100)

'                'fRgn.Setup(rToIcon)
'                'fAlpha.Setup(100)
'                If zzD.dRgn.GetCurrent = Rgns(RgnIDs.rToIcon) Then State = SF_States.Iconized
'                ObjFrm.disableinput = True
'            Case SF_States.Iconized
'                zzD.dRgn.Setup(Rgns(RgnIDs.rIcon))
'                zzD.dAlp.Setup(900)

'                'fRgn.Setup(rIcon)
'                'fAlpha.Setup(900)
'                ObjFrm.disableinput = True
'            Case SF_States.Hidden
'                zzD.dRgn.Setup(Rgns(RgnIDs.rHidden))
'                zzD.dAlp.Setup(0)

'                'fRgn.Setup(rIcon)
'                'fAlpha.Setup(0)
'                ObjFrm.disableinput = True
'            Case SF_States.StatusBar
'                zzD.dRgn.Setup(Rgns(RgnIDs.rStatus))
'                zzD.dAlp.Setup(0)

'                'fRgn.Setup(rStatus)
'                'fAlpha.Setup(900)
'                ObjFrm.disableinput = True
'            Case SF_States.StatusBarEx
'                zzD.dRgn.Setup(Rgns(RgnIDs.rStatusEx))
'                zzD.dAlp.Setup(900)

'                'fRgn.Setup(rStatusEx)
'                'fAlpha.Setup(900)
'                ObjFrm.disableinput = True
'            Case SF_States.VerticalBar
'                zzD.dRgn.Setup(Rgns(RgnIDs.rVertical))
'                zzD.dAlp.Setup(900)

'                'fRgn.Setup(rVertical)
'                'fAlpha.Setup(900)
'                ObjFrm.disableinput = True
'        End Select


'        'fRgn.Update()
'        'fAlpha.Update()
'        zzD.Update()

'        '' ''If rfMgr IsNot Nothing And ObjFrm IsNot Nothing Then
'        '' ''    Try
'        '' ''        ObjFrm.Location = zzD.dRgn.GetCurrent.Location
'        '' ''        ObjFrm.Size = zzD.dRgn.GetCurrent.Size

'        '' ''        If zzD.dAlp.GetCurrent > 0 Then
'        '' ''            ObjFrm.Opacity = zzD.dAlp.GetCurrent / 1000
'        '' ''        Else
'        '' ''            ObjFrm.Opacity = 0
'        '' ''        End If
'        '' ''    Catch ex As Exception

'        '' ''    End Try


'        '' ''End If

'    End Sub
'End Class

'Public Enum SF_States
'    Hidden = 0
'    Iconized = 1
'    Active = 2
'    ToIconized = 3
'    StatusBar = 4
'    StatusBarEx = 5
'    VerticalBar = 6
'End Enum

'Public Enum RgnIDs
'    rSpawn = 0
'    rVisible = 1

'    rIcon = 2
'    rToIcon = 3

'    rStatus = 4
'    rStatusEx = 5

'    rVertical = 6
'    rHidden = 7
'    'rForward_StartA = 7
'    'rForward_TOA = 7
'End Enum

' <StructLayout(LayoutKind.Sequential)> _
'Public Structure MessageStruct
'    Public HWnd As IntPtr
'    Public Msg As Message
'    Public WParam As IntPtr
'    Public LParam As IntPtr
'    Public Time As Integer
'    Public P As System.Drawing.Point
'End Structure
