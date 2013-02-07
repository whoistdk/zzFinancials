
Public Class FloatMenuMgr

    Public Menus As New List(Of FloatMenu)

    Public RgnHidden As Rectangle
    Public RgnShowSEQ As Rectangle
    Public RgnShown As Rectangle
    Public RgnHideSEQ As Rectangle

    Public lMenu As String = ""

    Public Sub AddMenu(ByRef RefMenuForm As Form, ByVal MenuName As String, ByVal rSrc As Rectangle)
        Dim nwFM As New FloatMenu(Me, RefMenuForm, MenuName, rSrc)
        Menus.Add(nwFM)
    End Sub

    Public Sub SwitchMenu(ByVal MenuName As String)
        For Each FM As FloatMenu In Menus
            If FM.Name = MenuName Then
                FM.State = MenuStates.Show
            Else
                FM.State = MenuStates.Hide
            End If
        Next
        lMenu = MenuName
    End Sub

    Public Function GetMenu(ByVal MenuName As String) As FloatMenu
        For Each FM As FloatMenu In Menus
            If FM.Name = MenuName Then
                Return FM
            End If
        Next
        Return Nothing
    End Function

    Public Sub Update()
        For Each FM As FloatMenu In Menus
            FM.Update()
        Next
    End Sub

End Class

Public Class FloatMenu
    Public rfMgr As FloatMenuMgr
    Public rfMenu As form
    Public Name As String
    Public State As MenuStates = MenuStates.Hidden
    Public Rgn As New zzDynamix.CoreClasses.dynRectangle
    Public Alpha As New zzDynamix.CoreClasses.dynSingle

    '  Public ObjLink As Object
    Public rSrc As Rectangle
    Public rReal_Hidden1 As Rectangle
    Public rReal_Hidden2 As Rectangle
    Public rReal_Visible1 As Rectangle
    Public rReal_Visible2 As Rectangle

    Public Sub New(ByRef RefMgr As FloatMenuMgr, ByRef RefMenu As Form, ByVal NenuName As String, ByVal rSource As Rectangle)
        rfMgr = RefMgr
        rfMenu = RefMenu
        Name = NenuName
        rSource = rSource
        Rgn.Setup(rfMgr.RgnHidden, rfMgr.RgnHidden, 1 / 4, 1 / 8, 1)
        State = MenuStates.Hidden
    End Sub

    Public Sub Update()
        Dim pS As Point = rSrc.Location
        rReal_Hidden1 = rSrc
        rReal_Hidden2 = rSrc
        rReal_Visible1 = rSrc
        rReal_Visible2 = rSrc


        Select Case State
            Case MenuStates.Hidden
                ' Rgn.Setup(rReal_Hidden1)
                Alpha.Setup(0)
            Case MenuStates.Show
                State = MenuStates.Show_Seq1
                rfMenu.Show()
            Case MenuStates.Show_Seq1
                Rgn.Setup(rReal_Visible2)
                Alpha.Setup(800)
                If Rgn.GetCurrent = rReal_Visible2 Then State = MenuStates.Show_Seq2
            Case MenuStates.Show_Seq2
                Rgn.Setup(rReal_Visible1)
                Alpha.Setup(800)
                If Rgn.GetCurrent = rReal_Visible1 Then State = MenuStates.Shown
            Case MenuStates.Shown
                Rgn.Setup(rReal_Visible1)
                Alpha.Setup(800)
            Case MenuStates.Hide
                State = MenuStates.Hide_Seq1
            Case MenuStates.Hide_Seq1
                Rgn.Setup(rReal_Hidden2)
                Alpha.Setup(0)
                If Rgn.GetCurrent = rReal_Hidden2 Then State = MenuStates.Hide_Seq2
            Case MenuStates.Hide_Seq2
                Rgn.Setup(rReal_Hidden1)
                Alpha.Setup(0)
                If Rgn.GetCurrent = rReal_Hidden1 Then
                    State = MenuStates.Hidden
                    rfMenu.Hide()
                End If

        End Select
        Rgn.Update()
        Alpha.Update()

        If rfMenu IsNot Nothing Then
            rfMenu.Location = Rgn.GetCurrent.Location
            rfMenu.Size = Rgn.GetCurrent.Size
            rfMenu.Opacity = Alpha.GetCurrent / 1000
        End If
    End Sub

End Class

Public Enum MenuStates
    Hidden = 0
    Show = 1
    Show_Seq1 = 2
    Show_Seq2 = 3
    Shown = 4
    Hide = 5
    Hide_Seq1 = 6
    Hide_Seq2 = 7
End Enum