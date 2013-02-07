Imports System.Text

Public Class EzLog
    Public Level As LogLevel = LogLevel.DeveloperEx
    Public Hist(99) As Point
    Private lPos As Point
    Private cprf() As ConsoleColor = {ConsoleColor.Gray, ConsoleColor.White, ConsoleColor.DarkGray, ConsoleColor.Green, ConsoleColor.Red}
    Public Sub Add(ByVal sMsg As String, Optional ByVal Remember As Integer = -1, Optional ByVal Replace As Integer = -1)
        Try


            If Not Remember = -1 Then
                Hist(Remember) = New Point(Console.CursorLeft, Console.CursorTop)
            End If
            If Not Replace = -1 Then
                lPos = New Point(Console.CursorLeft, Console.CursorTop)
                Console.CursorLeft = Hist(Replace).X
                Console.CursorTop = Hist(Replace).Y
                Console.WriteLine("                                                      ")
                Console.CursorLeft = Hist(Replace).X
                Console.CursorTop = Hist(Replace).Y
            End If
            CCSP(sMsg)
            If Not Replace = -1 Then
                Console.CursorLeft = lPos.X
                Console.CursorTop = lPos.Y
            End If
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub


    Public Sub CCSP(ByVal sMsg As String)
        Try

   
            Dim clrdef As ConsoleColor = cprf(0)
        Dim iPos As Integer = 1
        Dim lPos As Integer = 1
        Dim sb As New StringBuilder

        sb.Clear()

        Do
            If Mid(sMsg, iPos, 1) = "^" Then
                sb.Append(Mid(sMsg, lPos, iPos - lPos))
                Console.Write(sb.ToString)
                sb.Clear()
                Dim clr As Integer = Mid(sMsg, iPos + 1, 1)
                    Console.ForegroundColor = cprf(clr)
                iPos += 2
                lPos = iPos
            Else
                iPos += 1
            End If
        Loop Until iPos >= sMsg.Length


            If lPos < iPos + 1 Then
                sb.Append(Mid(sMsg, lPos, sMsg.Length - (lPos - 1)))
                Console.WriteLine(sb.ToString)
            End If

            Console.ForegroundColor = clrdef
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Public Sub AddErr(ByVal ExInfo As Exception)
        Select Case Level
            Case LogLevel.User

            Case Else
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.WriteLine(ExInfo.Message.ToString)
                Console.ForegroundColor = ConsoleColor.DarkGray
                Console.WriteLine(ExInfo.StackTrace.ToString.Replace(vbCr, " ").Replace(vbLf, " "))
                Console.ForegroundColor = ConsoleColor.Gray
        End Select
    End Sub

    Public Function GetLineNr(ByVal sStackTrace As String) As Integer
        Dim idx As Integer = InStr(sStackTrace, "line")
        If idx > 0 Then
            idx += 5
            Try
                Return Mid(sStackTrace, idx, (sStackTrace.Length - idx) + 1)
            Catch ex As Exception
                Return sStackTrace
            End Try
        Else
            Return sStackTrace
        End If
    End Function

End Class

Public Enum LogLevel
    User = 0
    Developer = 1
    DeveloperEx = 2
End Enum