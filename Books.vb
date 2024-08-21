Imports System.Data.SqlClient
Public Class Books
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ACER\Documents\LibraryVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub DisplayBook()
        Con.Open()
        Dim query = "select * from BookTbl"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, Con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        BooksDGV.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If BNameTb.Text = "" Or BAuthorTb.Text = "" Or BPublisherTb.Text = "" Or QtyTb.Text = "" Or BPriceTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Con.open()
            Dim query = "insert into BookTbl values('" & BNameTb.Text & "','" & BAuthorTb.Text & "','" & BPublisherTb.Text & "'," & BPriceTb.Text & "," & QtyTb.Text & ")"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Book Saved")
            Con.close()
            DisplayBook()
            Reset()
        End If
    End Sub

    Private Sub Books_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayBook()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub
    Private Sub Reset()
        BNameTb.Text() = ""
        BAuthorTb.Text() = ""
        BPriceTb.Text() = ""
        QtyTb.Text() = ""
        BPublisherTb.Text() = ""
        key = 0
    End Sub
    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub
    Dim key = 0
    Private Sub BooksDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles BooksDGV.CellMouseClick
        Dim row As DataGridViewRow = BooksDGV.Rows(e.RowIndex)
        BNameTb.Text = row.Cells(1).Value.ToString
        BAuthorTb.Text = row.Cells(2).Value.ToString
        BPublisherTb.Text = row.Cells(3).Value.ToString
        BPriceTb.Text = row.Cells(4).Value.ToString
        QtyTb.Text = row.Cells(5).Value.ToString
        If BNameTb.Text = "" Then
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
            Dim query = "delete from  BookTbl where BID=" & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Book Deleted")
            Con.close()
            DisplayBook()
            Reset()
        End If
    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If BNameTb.Text = "" Or BAuthorTb.Text = "" Or BPublisherTb.Text = "" Or QtyTb.Text = "" Or BPriceTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Con.open()
            Dim query = "Update BookTbl set BName='" & BNameTb.Text & "',BAuthor='" & BAuthorTb.Text & "',BPublisher='" & BPublisherTb.Text & "',BPrice=" & BPriceTb.Text & ",BQty=" & QtyTb.Text & " where BId =" & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Book Edited")
            Con.close()
            DisplayBook()
            Reset()
        End If
    End Sub

    Private Sub BackBtn_Click(sender As Object, e As EventArgs) Handles BackBtn.Click
        Dim obj = New MainForm
        obj.Show()
        Me.Hide()
    End Sub
End Class