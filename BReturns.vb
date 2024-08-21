Imports System.Data.SqlClient

Public Class BReturns
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
    Private Sub DisplayReturnedBook()
        Con.Open()
        Dim query = "select * from ReturnTbl"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, Con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        ReturnedBooksDGV.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Private Sub RemoveFromIssue()
        Try
            Con.open()
            Dim query = " delete from IssueTbl where INum= " & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Issue Removed")
            Con.close()
            ' DisplayBook()
            ' Reset()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If StIdCb.Text = "" Or BIdCb.Text = "" Or StNameTb.Text = "" Or BookNameTb.Text = "" Or FineTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Try
                Con.open()
                Dim query = "insert into returnTbl values(" & StIdCb.Text & ",'" & StNameTb.Text & "'," & BIdCb.Text & ",'" & BookNameTb.Text & "','" & IssueDate.Value & "','" & ReturnDate.Value.Date & "'," & fine & ")"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Book Returned")
                Con.close()
                RemoveFromIssue()
                DisplayReturnedBook()
                DisplayBook()
                Reset()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub

    Private Sub BReturns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayBook()
        DisplayReturnedBook()
    End Sub
    Dim key = 0
    Private Sub IssueDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles IssueDGV.CellMouseClick
        Dim row As DataGridViewRow = IssueDGV.Rows(e.RowIndex)
        StIdCb.Text = row.Cells(1).Value.ToString
        StNameTb.Text = row.Cells(2).Value.ToString
        BIdCb.Text = row.Cells(3).Value.ToString
        BookNameTb.Text = row.Cells(4).Value.ToString
        IssueDate.Text = row.Cells(5).Value.ToString
        If StNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub
    Dim fine As Integer
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim Dif As TimeSpan

        Dif = ReturnDate.Value.Date - IssueDate.Value.Date
        Dim Days = Dif.Days
        If Days <= 5 Then
            Fine = 0
            FineTb.Text = "No Fine"
        Else
            fine = (Days - 5) * 2
            FineTb.Text = "Rs" + Convert.ToString(fine)
        End If

    End Sub
    Private Sub Reset()
        StIdCb.SelectedIndex = -1
        BIdCb.SelectedIndex = -1
        StNameTb.Text = ""
        BookNameTb.Text = ""
        FineTb.Text = ""
        key = 0
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Reset()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim obj = New MainForm
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub BIdCb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles BIdCb.SelectedIndexChanged

    End Sub
End Class