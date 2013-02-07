Public Class fDevTools
    Public DisableInput As Boolean = False
    Private Sub fDevTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim num As Integer = InputBox("Number of items:", "", "1000000")
        Dim DMG As New zzDataMgr.DataManager
        DMG.TryParse(AppKernell.DM_Header)
        For c = 0 To num
            DMG.TryParse(GetCreated() & "|" & GetMutDate() & "|" & Rnd.Next(1, 1000000) & "|" & RndName(30, 50) & "|" & RndName(10, 20))
        Next
        DMG.ToFile("DevLogbook.lbk")
    End Sub

    Public Function GetCreated() As String
        Return Now.Day & "/" & Now.Month & "/" & Now.Year & " " & Now.ToShortTimeString
    End Function
    Public Function GetMutDate() As String
        Return Now.Day & "/" & Now.Month & "/" & Now.Year
    End Function

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


End Class