Public Class AppDiag
    Public Shared Function GetMem() As Long
        Dim processes() As Process = Process.GetProcessesByName("zzFinancials.vshost")
        Return processes(0).WorkingSet64 / 1024 / 1024
    End Function
End Class
