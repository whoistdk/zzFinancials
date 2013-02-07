Public Class uiPanelTree
    Public Root As uiPT_Node
    Public Rgn As Rectangle

    Private pOffset As Point

    Public Spacing As Integer = 1
    Public Indent As Integer = 16

    Public Sub New()
        Root = New uiPT_Node("Root", "root")
        With Root
            .rfT = Me
        End With
    End Sub

    Public Sub Update()
        Try
            pOffset = Point.Empty
            Root.Update(pOffset)
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
    End Sub

End Class

Public Class uiPT_Node
    Public WithEvents rfPnl As Panel
    Public rfT As uiPanelTree
    Public rfP As uiPT_Node
    Public lNodes As New List(Of uiPT_Node)

    Public Name As String
    Public Tag As String
    Public Rgn As New zzDynamix.CoreClasses.dynRectangle2
    Public rCollapsed As Rectangle
    Public rExpanded As Rectangle


    Private Expandable As Boolean
    Private ExpState As Boolean = False
    Public ExpStateLock As Boolean

    Private nType As PanelType
    Private Icon As Image

    Public Sub New()

    End Sub
    Public Sub New(ByVal nName As String, Optional ByVal nTag As String = "")
        Name = nName
        Tag = nTag
    End Sub

    Public Sub LinkPanel(ByRef ObjPanel As Panel, ByVal CollapsedHeight As Integer, ByVal ExpandedHeight As Integer, ByVal nPanelType As PanelType, Optional ByVal nIcon As Image = Nothing)
        rfPnl = ObjPanel
        rCollapsed.Height = CollapsedHeight
        rExpanded.Height = ExpandedHeight
        nType = nPanelType
        Icon = nIcon
    End Sub

    Public Function AddNode(ByVal nNode As uiPT_Node) As uiPT_Node
        With nNode
            .rfT = rfT
            .rfP = Me
        End With
        lNodes.Add(nNode)
        Return nNode
    End Function



    Public Function Update(ByVal pOffset As Point) As Point
        Try

            If lNodes.Count > 0 Then
                Expandable = True
            End If

            rExpanded.X = pOffset.X : rExpanded.Y = pOffset.Y : rExpanded.Width = rfT.Rgn.Width - (rExpanded.X)
            rCollapsed.X = pOffset.X : rCollapsed.Y = pOffset.Y : rCollapsed.Width = rfT.Rgn.Width - (rCollapsed.X)

            If rfP IsNot Nothing Then
                If ExpStateLock Then
                    Rgn.Setup(New Rectangle(rCollapsed.X, rCollapsed.Y, rCollapsed.Width, 0))
                Else

                    If rfP.Expandable Then



                        If rfP.ExpState Then
                            Rgn.Setup(rExpanded)
                        Else
                            Rgn.Setup(rCollapsed)
                        End If
                    End If

                End If

            End If


            Rgn.Update()
            Rgn.C(1) = pOffset.Y

            If rfPnl IsNot Nothing Then
                If Rgn.GetCurrentF.Width > 0 And Rgn.GetCurrentF.Height > 0 Then
                    rfPnl.Visible = True
                    rfPnl.Location = Rgn.GetCurrent.Location
                    rfPnl.Size = Rgn.GetCurrent.Size
                Else
                    rfPnl.Visible = False
                End If
                If nType = PanelType.Catagory Then
                    'If ExpState Then

                    ' e.Graphics.DrawImage(My.Resources.folderframehl, New Rectangle(Rgn.GetCurrent.X, Rgn.GetCurrent.Y, 18, 18))

                    If Not rfPnl.BackgroundImage Is My.Resources.BarBG Then rfPnl.BackgroundImage = My.Resources.BarBG
                    rfPnl.BackgroundImageLayout = ImageLayout.Stretch

                Else
                    ' e.Graphics.DrawImage(My.Resources.folderframe, New Rectangle(Rgn.GetCurrent.X, Rgn.GetCurrent.Y, 18, 18))
                    If Not rfPnl.BackgroundImage Is Nothing Then rfPnl.BackgroundImage = Nothing
                    rfPnl.BackgroundImageLayout = ImageLayout.None

                    'End If
                End If

                rfPnl.Invalidate()
            End If


            If rfP Is Nothing Then
                pOffset.Y += Rgn.GetCurrentF.Height
            Else

                pOffset.Y += Rgn.GetCurrentF.Height + rfT.Spacing
            End If




            '  If Expandable Then
            ' If ExpState Then
            If rfP IsNot Nothing Then pOffset.X += rfT.Indent
            For Each N As uiPT_Node In lNodes
                pOffset = N.Update(pOffset)
            Next
            If rfP IsNot Nothing Then pOffset.X -= rfT.Indent
            ' End If
            ' End If
        Catch ex As Exception
            Log.AddErr(ex)
        End Try
        Return pOffset
    End Function

    Public Sub ChainEx(ByVal ExState As Boolean)
        ExpStateLock = ExState
        For Each N As uiPT_Node In lNodes
            N.ChainEx(ExState)
        Next
    End Sub


    Public Sub ePnl_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rfPnl.MouseUp
        If nType = PanelType.Catagory Then
            ExpState = Not ExpState
            If ExpState Then
                For Each N As uiPT_Node In lNodes
                    N.ChainEx(True)
                Next
            Else
                For Each N As uiPT_Node In lNodes
                    N.ChainEx(False)
                Next
            End If
            '  sender.invalidate()

        End If

        ' End If
    End Sub

    Private Sub rfPnl_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles rfPnl.Paint

        If nType = PanelType.Catagory Then
            If ExpState Then

                e.Graphics.DrawImage(Icon, New Rectangle(1, 1, 16, 16))

                'If Not rfPnl.BackgroundImage Is My.Resources.folderframehl Then rfPnl.BackgroundImage = My.Resources.folderframehl
                'rfPnl.BackgroundImageLayout = ImageLayout.None

            Else
                e.Graphics.DrawImage(Icon, New Rectangle(1, 1, 16, 16))
                'If Not rfPnl.BackgroundImage Is My.Resources.folderframe Then rfPnl.BackgroundImage = My.Resources.folderframe
                'rfPnl.BackgroundImageLayout = ImageLayout.None

            End If
            Dim f As New Font("Microsoft Sans Serif", 10, FontStyle.Bold, GraphicsUnit.Pixel)
            e.Graphics.DrawString(Name, f, Brushes.DimGray, New Point(20, 3))
            f.Dispose()
            'Else
            '    Dim f As New Font("Microsoft Sans Serif", 10, FontStyle.Bold, GraphicsUnit.Pixel)
            '    e.Graphics.DrawString(Name, f, Brushes.Silver, New Point(20, 3))
            '    f.Dispose()

        End If



    End Sub



End Class
Public Enum PanelType
    Catagory = 0
    Container = 1
End Enum