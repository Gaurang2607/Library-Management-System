Public Class AdminLogin
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If PasswordTb.Text = "" Then
            MsgBox("Enter the Admin Password")
        ElseIf PasswordTb.Text = "Admin" Then
            Dim obj = New Librarians
            obj.Show()
            Me.Hide()
        Else
            MsgBox("Wrong Password")
            PasswordTb.Text = ""
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim obj = New Login
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub AdminLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class