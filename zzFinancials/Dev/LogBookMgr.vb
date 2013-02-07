Public Class LogBookMgr
    Public AccountName As String = "DevUser"
    Public LogBookName As String = "DevLogBook"

    Public Root As lbNode

    Public Sub New()
        Root = New lbNode
        With Root
            .nType = lbNodeType.LogBook
            .Title = LogBookName
            .Path = Application.StartupPath & "\" & "DevUser" & "\" & .Title & "\"
            CheckPath(.Path)
        End With
        Scan()
    End Sub



    Public Sub Scan(Optional ByRef Parent As lbNode = Nothing)
        Dim nwY As lbNode

        If Parent Is Nothing Then
            Parent = Root
        End If
        Log.Add("Scanning: ^0" & ExtrBase(Parent.Path).Replace("\", "^0\^2") & "^0...")
        Dim lDirs() As String = IO.Directory.GetDirectories(Parent.Path)
        For Each d As String In lDirs
            nwY = New lbNode
            With nwY
                .nType = Parent.nType + 1
                .Title = ExtrName(d)
                .Path = Parent.Path & .Title & "\"
                '  CheckPath(.Path)
            End With
            Parent.Nodes.Add(nwY)
            Scan(nwY)
        Next


    End Sub

    Private Function ExtrName(ByVal sPath As String) As String
        Dim r As String = sPath
        If Mid(sPath, sPath.Length, 1) = "\" Then r = Mid(sPath, 1, sPath.Length - 1)
        Dim id As Integer = InStrRev(r, "\")
        Return Mid(r, id + 1, r.Length - (id))
    End Function
    Private Function ExtrBase(ByVal sPath As String) As String
        Return sPath.Replace(Application.StartupPath, "...")
    End Function
End Class




Public Class lbBook
    Public Name As String
    Public sysPath As String

    Public Years As New List(Of lbBookYear)

    Public Sub Check()
        Try
            If Not IO.Directory.Exists(sysPath) Then
                MkDir(sysPath)
            End If
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

End Class

Public Class lbBookYear 'The Year
    Public Name As String
    Public sysPath As String

    Public Months As New List(Of lbBookMonth)
End Class

Public Class lbBookMonth 'The Month
    Public Name As String
    Public sysPath As String

    Public Days As New List(Of lbBookDay)
End Class

Public Class lbBookDay 'The Day
    Public Name As String
    Public sysPath As String

    Public Entries As New List(Of lbBookEntry)
End Class

Public Class lbBookEntry 'The Entry
    Public Name As String
    Public sysPath As String
End Class

Public Class lbNode
    Public ID As String
    Public Title As String
    Public Path As String

    Public nType As lbNodeType
    Public Nodes As New List(Of lbNode)


    Public Sub Init()
        CheckPath(Path)
    End Sub

End Class




Public Enum lbNodeType
    LogBook = 0
    Year = 1
    Month = 2
    Day = 3
    Entry = 4
End Enum