'Public Class ControlMover

'    Public ObjControl As Control
'    Public State As Integer = 0

'    Public rInvisible As Rectangle
'    Public rVisible As Rectangle

'    Public Rgn As New zzDynamix.CoreClasses.dynRectangle

'    Public Sub Update()
'        Select Case State
'            Case 0
'                Rgn.Setup(rInvisible)
'            Case 1
'                Rgn.Setup(rVisible)
'        End Select
'        Rgn.Update()

'        If ObjControl IsNot Nothing Then
'            ObjControl.Location = Rgn.GetCurrent.Location
'            ObjControl.Size = Rgn.GetCurrent.Size
'        End If
'    End Sub


'End Class


Public Class ControlMover

    Public Controls As New List(Of ControlInst)

    Public RgnHidden As Rectangle
    Public RgnShowSEQ As Rectangle
    Public RgnShown As Rectangle
    Public RgnHideSEQ As Rectangle

    Public lControl As String = ""

    Public Sub AddControl(ByRef RefControl As Object, ByVal ControlName As String)
        Dim nwPI As New ControlInst(Me, RefControl, ControlName)
        Controls.Add(nwPI)
        nwPI.State = ControlStates.Hide
    End Sub

    Public Sub SwitchControl(ByVal ControlName As String)
        For Each pnlI As ControlInst In Controls
            If pnlI.Name = ControlName Then
                pnlI.State = ControlStates.Show
            Else
                pnlI.State = ControlStates.Hide
            End If
        Next
        lControl = ControlName
    End Sub

    Public Sub Update()
        For Each pnlI As ControlInst In Controls
            pnlI.Update()
        Next
    End Sub

End Class

Public Class ControlInst
    Public rfMover As ControlMover
    Public rfControl As Object
    Public Name As String
    Public State As ControlStates = ControlStates.Hidden
    Public Rgn As New zzDynamix.CoreClasses.dynRectangle

    Public Sub New(ByRef RefMover As ControlMover, ByRef RefControl As Control, ByVal ControlName As String)
        rfMover = RefMover
        rfControl = RefControl
        Name = ControlName
        Rgn.Setup(rfMover.RgnHidden, rfMover.RgnHidden, 1 / 8, 1 / 8, 1)
        State = ControlStates.Hidden
    End Sub

    Public Sub Update()
        Select Case State
            Case ControlStates.Hidden
                Rgn.Setup(rfMover.RgnHidden)
            Case ControlStates.Show
                State = ControlStates.Show_Seq1
            Case ControlStates.Show_Seq1
                Rgn.Setup(rfMover.RgnShowSEQ)
                If Rgn.GetCurrent = rfMover.RgnShowSEQ Then State = ControlStates.Show_Seq2
            Case ControlStates.Show_Seq2
                Rgn.Setup(rfMover.RgnShown)
                If Rgn.GetCurrent = rfMover.RgnShown Then State = ControlStates.Shown
            Case ControlStates.Shown
                Rgn.Setup(rfMover.RgnShown)
            Case ControlStates.Hide
                State = ControlStates.Hide_Seq1
            Case ControlStates.Hide_Seq1
                Rgn.Setup(rfMover.RgnHideSEQ)
                If Rgn.GetCurrent = rfMover.RgnHideSEQ Then State = ControlStates.Hide_Seq2
            Case ControlStates.Hide_Seq2
                Rgn.Setup(rfMover.RgnHidden)
                If Rgn.GetCurrent = rfMover.RgnHidden Then State = ControlStates.Hidden
        End Select
        Rgn.Update()

        If rfControl IsNot Nothing Then
            rfControl.Location = Rgn.GetCurrent.Location
            rfControl.Size = Rgn.GetCurrent.Size
        End If
    End Sub

End Class

Public Enum ControlStates
    Hidden = 0
    Show = 1
    Show_Seq1 = 2
    Show_Seq2 = 3
    Shown = 4
    Hide = 5
    Hide_Seq1 = 6
    Hide_Seq2 = 7
End Enum