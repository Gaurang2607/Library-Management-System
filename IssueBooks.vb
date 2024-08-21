Imports System.Data.SqlClient

Public Class IssueBooks
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\Documents\LibraryVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub DisplayBook()
        Con.Open()
        Dim query = "select * from IssueTbl"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, Con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        IssueDGV.DataSource = ds.Tables(0)
        Con.Close()
    End Sub

    Private Sub Fillstudent()
        Con.open()
        Dim query = "select * from StudentTbl"
        Dim cmd As New SqlCommand(query, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tbl As New DataTable()
        adapter.Fill(Tbl)
        StIdCb.DataSource = Tbl
        StIdCb.DisplayMember = "StId"
        StIdCb.ValueMember = "StId"
        Con.close()
    End Sub

    Private Sub GetStudentName()
        Con.open()
        Dim query = "select * from StudentTbl where StId=" & StIdCb.SelectedValue.ToString() & ""
        Dim cmd = New SqlCommand(query, Con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read
            StNameTb.Text = "" + reader(1).ToString()
        End While
        Con.close()
    End Sub
    Private Sub GetBookName()
        Con.open()
        Dim query = "select * from BookTbl where BId=" & BIdCb.SelectedValue.ToString() & ""
        Dim cmd = New SqlCommand(query, Con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read
            BookNameTb.Text = "" + reader(1).ToString()
        End While
        Con.close()
    End Sub
    Private Sub FillBook()
        Con.open()
        Dim query = "select * from BookTbl"
        Dim cmd As New SqlCommand(query, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tbl As New DataTable()
        adapter.Fill(Tbl)
        BIdCb.DataSource = Tbl
        BIdCb.DisplayMember = "BId"
        BIdCb.ValueMember = "BId"
        Con.close()

    End Sub
    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If StNameTb.Text = "" Or BookNameTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Con.open()
            Dim query = "Update IssueTbl set StId=" & StIdCb.SelectedValue.ToString() & ",StName='" & StNameTb.Text & "',BookId=" & BIdCb.SelectedValue.ToString() & ",BookName='" & BookNameTb.Text & "',IssueDate='" & IssueDate.Value.Date & "' where INum =" & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Issue Edited")
            Con.close()
            DisplayBook()
            reset()
        End If
    End Sub

    Private Sub IssueBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayBook()
        Fillstudent()
        FillBook()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub

    Private Sub StIdCb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles StIdCb.SelectionChangeCommitted
        GetStudentName()
    End Sub

    Private Sub BIdCb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles BIdCb.SelectionChangeCommitted
        GetBookName()
    End Sub
    Dim Num = 0
    Private Sub CountBook()
        Try
            Con.Open()
            Dim query = "select count(*) from IssueTbl where StId =" & StIdCb.SelectedValue.ToString() & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            Num = cmd.ExecuteScalar
            Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub
    Private Sub IssueBtn_Click(sender As Object, e As EventArgs) Handles IssueBtn.Click
        CountBook()
        If StNameTb.Text = "" Or BookNameTb.Text = "" Or StIdCb.SelectedIndex = -1 Or BIdCb.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        ElseIf Num = 2 Then
            MsgBox("No More than 2 Books Issued")
        Else
            Try
                Con.open()
                Dim query = "insert into IssueTbl values(" & StIdCb.SelectedValue.ToString() & ",'" & StNameTb.Text & "'," & BIdCb.SelectedValue.ToString() & ",'" & BookNameTb.Text & "','" & IssueDate.Value & "')"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Book Issued")
                Con.close()
                DisplayBook()
                reset()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub reset()
        BookNameTb.Text = ""
        StNameTb.Text = ""
        StIdCb.SelectedIndex = -1
        BIdCb.SelectedIndex = -1
    End Sub
    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        reset()
    End Sub

    Private Sub BackBtn_Click(sender As Object, e As EventArgs) Handles BackBtn.Click
        Dim obj = New MainForm()
        obj.Show()
        Me.Hide()

    End Sub
    Dim key = 0
    Private Sub IssueDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles IssueDGV.CellMouseClick
        Dim row As DataGridViewRow = IssueDGV.Rows(e.RowIndex)
        StIdCb.SelectedValue = row.Cells(1).Value.ToString
        StNameTb.Text = row.Cells(2).Value.ToString
        BIdCb.SelectedValue = row.Cells(3).Value.ToString
        BookNameTb.Text = row.Cells(4).Value.ToString
        IssueDate.Text = row.Cells(5).Value.ToString
        If StNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Con.Open()
        Dim query = "select BName,BAuthor from BookTbl where BAuthor = '" & SearchTb.Text & "' "
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, Con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        SearchDGV.DataSource = ds.Tables(0)
        Con.Close()
    End Sub

    Private Sub IssueDGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles IssueDGV.CellContentClick

    End Sub
End Class