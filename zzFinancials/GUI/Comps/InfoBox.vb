Public Class InfoBox

End Class
Public Class InfoBoxLine
    Public Title As String
    Public Info As String
    Public Clr As Color
End Class



Public Class StringMorpher
    Private sA As String
    Private sB As String

    Private cA As Color
    Private cB As Color

    Private sNext As String
    Private sLast As String
    Private cNext As Color

    Private State As Integer = -1

    Public MasterAlpha As Single

    Private AlphaA As New zzDynamix.CoreClasses.dynSingle
    Private AlphaB As New zzDynamix.CoreClasses.dynSingle

    Public Sub New()

    End Sub

    Public Sub SetString(nString As String, nColor As Color)
        sNext = nString
        cNext = nColor
    End Sub

    Public Sub Update()

        Select Case State
            Case -1 'Wait for text change
                AlphaA.Setup(0)
                AlphaB.Setup(0)
                If Not sNext = sLast Then
                    sA = sNext
                    cA = cNext
                    sLast = sNext
                    State = 0
                End If
            Case 0 'Fade in A
                AlphaA.Setup(MasterAlpha)
                If AlphaA.GetCurrent = MasterAlpha Then State = 1
            Case 1 'Static A
                AlphaA.Setup(MasterAlpha)
                If Not sNext = sLast Then
                    sB = sNext
                    cB = cNext
                    sLast = sNext
                    State = 2
                End If
            Case 2 'Fade Out A and Fade In B
                AlphaA.Setup(0)
                AlphaB.Setup(MasterAlpha)
                If AlphaA.GetCurrent = 0 And AlphaB.GetCurrent = MasterAlpha Then State = 3
            Case 3 'Static B
                AlphaB.Setup(MasterAlpha)
                If Not sNext = sLast Then
                    sA = sNext
                    cA = cNext
                    sLast = sNext
                    State = 4
                End If
            Case 4 'Fade Out B and Fade In A
                AlphaA.Setup(MasterAlpha)
                AlphaB.Setup(0)
                If AlphaA.GetCurrent = MasterAlpha And AlphaB.GetCurrent = 0 Then State = 1
        End Select

        AlphaA.Update()
        AlphaB.Update()

    End Sub

    Public Sub Render(G As Graphics, Fnt As Font, Rgn As Rectangle, AlignHorizontal As StringAlignment, AlignVertical As StringAlignment, Optional UseShadow As Boolean = True)

        If AlphaA.GetCurrent > 0 Then
            Using BrF As New SolidBrush(Color.FromArgb((255 / 1000) * AlphaA.GetCurrent, cA))
                DrawText(G, sA, Fnt, BrF, AlignHorizontal, AlignVertical, Rgn, UseShadow)
            End Using
        End If
        If AlphaB.GetCurrent > 0 Then
            Using BrF As New SolidBrush(Color.FromArgb((255 / 1000) * AlphaB.GetCurrent, cB))
                DrawText(G, sB, Fnt, BrF, AlignHorizontal, AlignVertical, Rgn, UseShadow)
            End Using
        End If
    End Sub


End Class