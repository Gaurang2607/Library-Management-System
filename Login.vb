Imports System.Data.SqlClient

Public Class Login
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim obj = New AdminLogin
        obj.Show()
        Me.Hide()
    End Sub
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\Documents\LibraryVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If UNameTb.Text = "" Then
            MsgBox("Enter Librarian Name")
        ElseIf PasswordTb.Text = "" Then
            MsgBox("Enter the Password")
        Else
            Con.open()
            Dim query = "select * from LibrarianTbl where LibName ='" & UNameTb.Text & "' and LibPass ='" & PasswordTb.Text & "'"
            Dim cmd = New SqlCommand(query, Con)
            Dim da = New SqlDataAdapter(cmd)
            Dim ds = New DataSet()
            da.Fill(ds)
            Dim a As Integer
            a = ds.Tables(0).Rows.Count
            If a = 0 Then
                MsgBox("Wrong UserName Or Password")
            Else
                Dim obj = New MainForm
                obj.Show()
                Me.Hide()
            End If

            Con.close()
        End If
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class