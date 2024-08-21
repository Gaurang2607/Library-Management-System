Imports System.Data.SqlClient

Public Class Librarians
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Application.Exit()

    End Sub
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\Documents\LibraryVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub DisplayLib()
        Con.Open()
        Dim query = "select * from LibrarianTbl"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, Con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        LibrariansDGV.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Private Sub Reset()
        libNameTb.Text = ""
        PasswordTb.Text = ""
        PhoneTb.Text = ""
        ' key = 0
    End Sub
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If libNameTb.Text = "" Or PhoneTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Con.open()
            Dim query = "insert into LibrarianTbl values('" & libNameTb.Text & "','" & PhoneTb.Text & "','" & PasswordTb.Text & "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Librarian Saved")
            Con.close()
            DisplayLib()
            Reset()
        End If
    End Sub

    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub

    Private Sub Librarians_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayLib()
    End Sub
    Dim key = 0
    Private Sub LibrariansDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles LibrariansDGV.CellMouseClick
        Dim row As DataGridViewRow = LibrariansDGV.Rows(e.RowIndex)
        libNameTb.Text = row.Cells(1).Value.ToString
        PhoneTb.Text = row.Cells(2).Value.ToString
        PasswordTb.Text = row.Cells(3).Value.ToString

        If libNameTb.Text = "" Then
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
            Dim query = "delete from  LibrarianTbl where LibId=" & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Librarian Deleted")
            Con.close()
            DisplayLib()
            Reset()
        End If
    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If libNameTb.Text = "" Or PhoneTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Con.open()
            Dim query = "update LibrarianTbl set LibName='" & libNameTb.Text & "',LibPhone='" & PhoneTb.Text & "',LibPass='" & PasswordTb.Text & "' where LibId=" & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Librarian Edited")
            Con.close()
            DisplayLib()
            Reset()
        End If
    End Sub

    Private Sub BackBtn_Click(sender As Object, e As EventArgs) Handles BackBtn.Click
        Dim obj = New Login
        obj.Show()
        Me.Hide()
    End Sub
End Class