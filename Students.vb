Imports System.Data.SqlClient

Public Class Students
    Private Sub Students_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayStudents()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\Documents\LibraryVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub DisplayStudents()
        Con.Open()
        Dim query = "select * from StudentTbl"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, Con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        StudentsDGV.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Private Sub Reset()
        StNameTb.Text = ""
        phoneTb.Text = ""
        SemCb.SelectedIndex = 0
        DepartmentTb.Text = ""
        key = 0
    End Sub
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If StNameTb.Text = "" Or DepartmentTb.Text = "" Or SemCb.SelectedIndex = -1 Or phoneTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Con.open()
            Dim query = "insert into StudentTbl values('" & StNameTb.Text & "','" & DepartmentTb.Text & "'," & SemCb.SelectedItem.ToString() & ",'" & phoneTb.Text & "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Student Saved")
            Con.close()
            DisplayStudents()
            Reset()
        End If
    End Sub

    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub
    Dim key = 0
    Private Sub StudentsDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles StudentsDGV.CellMouseClick
        Dim row As DataGridViewRow = StudentsDGV.Rows(e.RowIndex)
        StNameTb.Text = row.Cells(1).Value.ToString
        DepartmentTb.Text = row.Cells(2).Value.ToString
        SemCb.SelectedItem = row.Cells(3).Value.ToString
        phoneTb.Text = row.Cells(4).Value.ToString

        If StNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Missing Information")
        Else
            Con.open()
            Dim query = "delete from  StudentTbl where StId=" & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Student Deleted")
            Con.close()
            DisplayStudents()
            Reset()
        End If
    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If StNameTb.Text = "" Or DepartmentTb.Text = "" Or SemCb.SelectedIndex = -1 Or phoneTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Con.open()
            Dim query = "Update StudentTbl set StName='" & StNameTb.Text & "',StDep='" & DepartmentTb.Text & "',StSem=" & SemCb.SelectedItem.ToString & ",StPhone='" & phoneTb.Text & "' where StId=" & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Student Edited")
            Con.close()
            DisplayStudents()
            Reset()
        End If
    End Sub

    Private Sub BackBtn_Click(sender As Object, e As EventArgs) Handles BackBtn.Click
        Dim obj = New MainForm()
        obj.Show()
        Me.Hide()
    End Sub
End Class