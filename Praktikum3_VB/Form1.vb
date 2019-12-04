Imports System.Data.Odbc
Public Class Form1

    Public SaldoSekarang As String

    Sub TampilGrid()
        bukakoneksi()
        DA = New OdbcDataAdapter("Select * from  tbl_tampilan", CONN)
        DS = New DataSet
        DA.Fill(DS, "tbl_tampilan")
        DataGridView1.DataSource = DS.Tables("tbl_tampilan")
        tutupkoneksi()

    End Sub

    Sub getSaldoUtama()
        bukakoneksi()

        DA = New OdbcDataAdapter("SELECT * from tbl_tampilan order by id desc limit 1", CONN)
        DS = New DataSet
        DA.Fill(DS, "tbl_tampilan")
        Label8.Text = DS.Tables(0).Rows(0).Item(4)
        SaldoSekarang = DS.Tables(0).Rows(0).Item(4)

        tutupkoneksi()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TampilGrid()
        getSaldoUtama()
    End Sub
    Sub KosongkanData()
        textBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If textBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Silahkan Isi Semua From")

        Else
            If ComboBox1.Text = "Masuk" Then
                Dim saldoBaru As Integer
                Dim saldoMasuk As Integer = CInt(TextBox2.Text)
                Dim saldoTerakhir As Integer = CInt(SaldoSekarang)
                saldoBaru = saldoMasuk + saldoTerakhir

                bukakoneksi()
                Dim simpan1 As String = "insert into tbl_tampilan (tanggal,jenis,jumlah,saldo_sekarang,keterangan) values('" & textBox1.Text & "','" & ComboBox1.Text & "','" & TextBox2.Text & "','" & saldoBaru & "','" & TextBox3.Text & "')"
                CMD = New OdbcCommand(simpan1, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Input data Berhasil")
                TampilGrid()
                getSaldoUtama()
                KosongkanData()
                tutupkoneksi()

            ElseIf ComboBox1.Text = "Keluar" Then
                Dim saldoBaru As Integer
                Dim saldoKeluar As Integer = CInt(TextBox2.Text)
                Dim saldoTerakhir As Integer = CInt(SaldoSekarang)
                saldoBaru = saldoTerakhir - saldoKeluar

                bukakoneksi()
                Dim simpan1 As String = "insert into tbl_tampilan (tanggal,jenis,jumlah,saldo_sekarang,keterangan) values('" & textBox1.Text & "','" & ComboBox1.Text & "','" & TextBox2.Text & "','" & saldoBaru & "','" & TextBox3.Text & "')"
                CMD = New OdbcCommand(simpan1, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Input data Berhasil")
                TampilGrid()
                getSaldoUtama()
                KosongkanData()
                tutupkoneksi()

            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        textBox1.MaxLength = 6
        If e.KeyChar = Chr(13) Then
            bukakoneksi()
            CMD = New OdbcCommand("Select * from tbl_tampilan where id ='" & TextBox4.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                MsgBox("No Tidak Ada, Silahkan coba lagi!")
                textBox1.Focus()
            Else
                textBox1.Text = RD.Item("Tanggal")
                ComboBox1.Text = RD.Item("Jenis")
                TextBox2.Text = RD.Item("Jumlah")
                TextBox3.Text = RD.Item("Keterangan")
                TextBox4.Focus()
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByValsender As Object, ByVal e As EventArgs) Handles Button2.Click
        bukakoneksi()
        Dim edit As String = "update tbl_tampilan set
        Tanggal='" & textBox1.Text & "',
        Jenis='" & ComboBox1.Text & "',
        Jumlah='" & TextBox2.Text & "',
        Keterangan='" & TextBox3.Text & "'
        where id ='" & TextBox4.Text & "'"

        CMD = New OdbcCommand(edit, CONN)
        CMD.ExecuteNonQuery()
        MsgBox("Data Berhasil diUpdate")
        TampilGrid()
        KosongkanData()
        tutupkoneksi()
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        If TextBox4.Text = "" Then
            MsgBox("Silahkan Pilih Data yag akan dihapus dengan Masukan id dan Enter")
        Else
            If MessageBox.Show("Yakin akan dihapus..? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) Then
                bukakoneksi()
                Dim hapus As String = "delete from tbl_tampilan where id='" & TextBox4.Text & "'"
                CMD = New OdbcCommand(hapus, CONN)
                CMD.ExecuteNonQuery()
                TampilGrid()
                KosongkanData()
                tutupkoneksi()
            End If
        End If
    End Sub
End Class
