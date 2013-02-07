Public Class fFilter
    Public DisableInput As Boolean = False
    Private lv1Sel As ListViewItem
    'Private lv2Sel As ListViewItem

    Private Sub fFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
    End Sub



    Public Sub Reset()
        ' Kernell.DM.ToLv_Fields(ListView1)
    End Sub

    Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If lv1Sel IsNot Nothing Then
            '    Dim i As ListViewItem = lv1Sel
            '    ListView1.Items.Remove(lv1Sel)
            '    ListView2.Items.Add(i)
            If lv1Sel.ImageKey = "active" Then
                lv1Sel.ImageKey = "inactive"
            Else
                lv1Sel.ImageKey = "active"
            End If
        End If
    End Sub

    Private Sub ListView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        lv1Sel = ListView1.GetItemAt(e.X, e.Y)
    End Sub

    Private Sub ListView2_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'If lv2Sel IsNot Nothing Then
        '    Dim i As ListViewItem = lv2Sel
        '    ListView2.Items.Remove(lv2Sel)
        '    ListView1.Items.Add(i)
        'End If
    End Sub

    Private Sub ListView2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        lv1Sel = ListView1.GetItemAt(e.X, e.Y)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SortLVI(ListView1, lv1Sel, -1)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SortLVI(ListView1, lv1Sel, 1)
    End Sub

    Public Function SortLVI(ByRef objLV As ListView, ByRef ObjI As ListViewItem, ByVal SortMode As Integer)

        Dim idOld As Integer = -1
        Dim idNew As Integer = -1
        For c = 0 To objLV.Items.Count - 1
            If objLV.Items(c) Is ObjI Then
                idOld = c
                Exit For
            End If
        Next


        Select Case SortMode
            Case 0
            Case 1
                idNew = idOld + 1
                If idNew > objLV.Items.Count - 1 Then idNew = objLV.Items.Count - 1
            Case -1
                idNew = idOld - 1
                If idNew < 0 Then idNew = 0
        End Select

        Try
            Dim itm As ListViewItem
            itm = objLV.Items(idOld)
            objLV.Items.RemoveAt(idOld)
            objLV.Items.Insert(idNew, itm)

        Catch ex As Exception

        End Try


    End Function


    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lv1Sel.SubItems(1).Text = "Ascending"
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lv1Sel.SubItems(1).Text = "Descending"
    End Sub
End Class