Public Class Form1
    Private stopwatch As New Stopwatch
    Dim Jogadas(11) As Integer
    Dim Matriz(11) As Integer
    Dim Quadros() As PictureBox
    Dim Anterior, Passo As Integer

    Sub Imagens(quadro)
        Dim fig As New PictureBox
        Dim i = Int(Rnd() * 6)
        Select Case Matriz(quadro)
            Case 0 : fig.BackgroundImage = My.Resources.caveira
            Case 1 : fig.BackgroundImage = My.Resources.jager
            Case 2 : fig.BackgroundImage = My.Resources.jackal
            Case 3 : fig.BackgroundImage = My.Resources.rook
            Case 4 : fig.BackgroundImage = My.Resources.ela
            Case 5 : fig.BackgroundImage = My.Resources.zofia
        End Select
        Quadros(quadro).BackgroundImage = fig.BackgroundImage
        Refresh()
        Threading.Thread.Sleep(500)
        Jogadas(quadro) = 1
        If Passo = 1 Then : Anterior = quadro
        ElseIf Matriz(quadro) <> Matriz(Anterior) Then
            Jogadas(quadro) = 0
            Jogadas(Anterior) = 0
            Quadros(quadro).BackgroundImage = My.Resources.fundo
            Quadros(Anterior).BackgroundImage = My.Resources.fundo
        End If
    End Sub

    Sub inicializa()
        Dim i As Integer
        For i = 0 To 11
            Quadros(i).BackgroundImage = My.Resources.fundo
            Matriz(i) = 0
            Jogadas(i) = 0
        Next

        For par = 0 To 5
            For x = 0 To 1
                Do : i = Int(Rnd() * 12)
                Loop While Matriz(i)
                Matriz(i) = par
            Next
        Next
        Passo = 1
    End Sub


    Private Sub Clicar(sender As Object, e As EventArgs) Handles P1.Click, P9.Click, P8.Click, P7.Click, P6.Click, P5.Click, P4.Click, P3.Click, P2.Click, P12.Click, P11.Click, P10.Click
        Dim quadro As Integer
        Dim i = Int(Rnd() * 6)
        Select Case sender.name
            Case "P1" : quadro = 0
            Case "P2" : quadro = 1
            Case "P3" : quadro = 2
            Case "P4" : quadro = 3
            Case "P5" : quadro = 4
            Case "P6" : quadro = 5
            Case "P7" : quadro = 6
            Case "P8" : quadro = 7
            Case "P9" : quadro = 8
            Case "P10" : quadro = 9
            Case "P11" : quadro = 10
            Case "P12" : quadro = 11
        End Select
        If Jogadas(quadro) <> 0 Then Return
        Call Imagens(quadro)
        If Passo = 1 Then : Passo = 2
        Else : Passo = 1
        End If
        Dim ganhou = True
        For i = 0 To 11
            If Jogadas(i) = 0 Then ganhou = False
        Next
        If ganhou Then
            Beep()
            MsgBox("Parabens!! Ganhaste! Jogar Novamente ?",, "Fim de Jogo")
            inicializa()
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Randomize()
        Quadros = {P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12}
        Call inicializa()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MsgBox("Se carregaste neste butão é porque precisas de ajuda ou não sabes como jogar!
É muito simples so precisas de carregar nos butões de forma a por a mesma imagem ao mesmo tempo de forma a formar um ponto ate acabar o quadro
Gonçalo Gomes 11º4",, "Help")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim elapsed As TimeSpan = stopwatch.Elapsed
        Label2.Text = String.Format("{0: 00}:{1:00}:{2:00}:{3:00}",
                                    Math.Floor(elapsed.TotalHours),
                                    elapsed.Minutes, elapsed.Seconds, elapsed.Milliseconds)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Start" Then
            Timer1.Start()
            stopwatch.Start()
            Button3.Text = "Stop"
            Button5.Enabled = False
        Else
            Timer1.Stop()
            stopwatch.Stop()
            Button3.Text = "Start"
            Button5.Enabled = True
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ListBox1.Items.Add(ListBox1.Items.Count + 1 & ". " & Label2.Text)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        stopwatch.Reset()
        Label2.Text = "00:00:00:00"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Beep()
        Dim resposta = MsgBox("Tem a Certeza", vbYesNo, "Novo Jogo")
        If resposta = vbNo Then Return
        inicializa()
    End Sub
End Class
