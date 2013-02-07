Public Class guiHyperLogo
    Public Rgn As Rectangle
    Public TitleText() As String = {"zz", "Financials", "Welcome <UserName>"}
    Public ImgIcon As Image


    Public rTitleOut() As Rectangle
    Public rTitleInA() As Rectangle
    Public rTitleInB() As Rectangle
    Public TitleRgns() As zzDynamix.CoreClasses.dynRectangle2
    Public TitleAlpha() As zzDynamix.CoreClasses.dynSingle

    Public Sub Init()
        ReDim TitleRgns(4)
        ReDim TitleAlpha(4)
        ReDim rTitleOut(4)
        ReDim rTitleInA(4)
        ReDim rTitleInB(4)

        Dim idx As Integer = 0
        rTitleOut(idx) = New Rectangle(Rgn.X - 100, Rgn.Y + 20, 100, 14)
        rTitleInA(idx) = New Rectangle(Rgn.X + 100, Rgn.Y + 20, 100, 14)
        rTitleInB(idx) = New Rectangle(Rgn.X + 200, Rgn.Y + 20, 100, 14)
        TitleRgns(idx) = New zzDynamix.CoreClasses.dynRectangle2
        TitleAlpha(idx) = New zzDynamix.CoreClasses.dynSingle
        TitleRgns(idx).Setup(rTitleOut(idx), rTitleOut(idx), 1 / 16, 1 / 16, 1)

        idx = 1
        rTitleOut(idx) = New Rectangle(Rgn.X + 120, Rgn.Y - 20, 200, 14)
        rTitleInA(idx) = New Rectangle(Rgn.X + 120, Rgn.Y + 15, 200, 14)
        rTitleInB(idx) = New Rectangle(Rgn.X + 120, Rgn.Y + 25, 200, 14)
        TitleRgns(idx) = New zzDynamix.CoreClasses.dynRectangle2
        TitleAlpha(idx) = New zzDynamix.CoreClasses.dynSingle
        TitleRgns(idx).Setup(rTitleOut(idx), rTitleOut(idx), 1 / 16, 1 / 16, 1)

        idx = 2
        rTitleOut(idx) = New Rectangle(Rgn.X + Rgn.Width + 100, Rgn.Y + 20, 100, 14)
        rTitleInA(idx) = New Rectangle(Rgn.X + 100, Rgn.Y + 20, 100, 14)
        rTitleInB(idx) = New Rectangle(Rgn.X + 200, Rgn.Y + 20, 100, 14)
        TitleRgns(idx) = New zzDynamix.CoreClasses.dynRectangle2
        TitleRgns(idx).Setup(rTitleOut(idx), rTitleOut(idx), 1 / 16, 1 / 16, 1)

    End Sub

End Class
Public Enum LogoStages
    Invisible = 0
    Show = 1
    AnimationIn = 2
    AnimationStatic = 3
    AnimationOut = 4
    SwitchNext = 5
End Enum