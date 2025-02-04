Imports System.IO

Public Class Main
    Dim _board As Crossword = New Crossword(ROWS, COLS)
    Private _order As New List(Of String)
    Private HLines As New List(Of String)
    Private VLines As New List(Of String)
    Private Unused As New List(Of String)
    Dim FPath As String = "wordlistnew.txt"

    Private Sub Main_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ClearBoard()
    End Sub

    'When generate button clicks
    'Keep loading word untill getting successfull puzzle
    Private Sub GenerateBtn_Click(sender As Object, e As EventArgs) Handles GenerateBtn.Click
        GenerateBtn.Enabled = False
        While Not LoadWords()
        End While
        GenerateBtn.Enabled = True
    End Sub

    'Load words from file
    'return yes if words loaded and also created puzzle fine
    Function LoadWords() As Boolean
        List.Load(FPath, MinLen.Value, MaxLen.Value, TotalWords.Value, CapitalPer.Value)
        _words = _words.OrderBy(Function(x) x).ToList()
        _words.Reverse()
        _order = _words
        WordList.Lines = List._words.ToArray
        Return GenCrossword()
    End Function

    'Clear puzzle from grid
    Sub ClearBoard()
        Grid.RowTemplate.Height = 40
        Grid.Columns.Clear()
        Grid.Rows.Clear()
        For C = 0 To _board.M - 1
            Grid.Columns.Add(New DataGridViewTextBoxColumn With {.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill})
        Next
        For R = 0 To _board.N - 1
            Grid.Rows.Add()
        Next
        Grid.Width = Grid.RowTemplate.Height * _board.M
        Grid.Height = Grid.RowTemplate.Height * (_board.N + 1)
        Grid.ClearSelection()
    End Sub



    'Generates puzzle and shows on grid
    'Tries 1000 combination and chooses best one with respect how much filled puzzle is and how much optimized
    Function GenCrossword() As Boolean
        ClearBoard()
        Dim Dict As New List(Of Puzzle) 'KeyValuePair(Of Crossword, Integer))
        For index = 1 To 1000
            _board = New Crossword(ROWS, COLS)
            _board.Reset()
            HLines.Clear()
            VLines.Clear()
            Unused.Clear()
            For Each word In _order
                Capitlize(word)
                Select Case _board.AddWord(word)
                    Case 0
                        HLines.Add(word)
                    Case 1
                        VLines.Add(word)
                    Case Else
                        Unused.Add(word)
                End Select
            Next

            Dim matrix = _board.GetBoard
            Dim topMost As Integer = 0
            Dim leftMost As Integer = 0
            Dim rightMost As Integer = 6
            Dim bottomMost As Integer = 3
            Dim TotalChar = 0
            For i As Integer = 0 To matrix.GetLength(0) - 1
                For j As Integer = 0 To matrix.GetLength(1) - 1
                    If matrix(i, j) <> " "c AndAlso matrix(i, j) <> "*"c Then
                        If i < topMost Then topMost = i
                        If i > bottomMost Then bottomMost = i
                        If j < leftMost Then leftMost = j
                        If j > rightMost Then rightMost = j
                        TotalChar += 1
                    End If
                Next
            Next

            Dim Sides = Math.Max(bottomMost - topMost, rightMost - leftMost) + 1
            Dim Area = (Sides * Sides) + TotalChar

            If Unused.Count = 0 AndAlso IsOnlyOneSnake(_board.GetBoard) Then
                Dim P = FillPossible(matrix, bottomMost, topMost, rightMost, leftMost)
                Dim Puzzle As New Puzzle() With {.Cross = _board, .Fills = P, .ValidChars = ValidCharCount(_board.GetBoard)}
                Dict.Add(Puzzle) 'New KeyValuePair(Of Crossword, Integer)(_board, Area - P))
            End If
            'If Unused.Count = 0 Then Exit For
        Next
        If Dict.Count > 0 Then
            If Not String.IsNullOrEmpty(GapLbl.Text) Then GapLbl.Text = String.Empty
            Dim Chars = Dict.OrderByDescending(Function(D) D.Fills).ThenBy(Function(D) D.ValidChars).First
            Dim Board = Chars.Cross
            _board = Board
            ShowPuzzle()
            Return True
        Else
            GapLbl.Text = "Can't create combination"
        End If
        Return False
    End Function

    Class Puzzle
        Public Cross As Crossword 'class containing puzzle data
        Public ValidChars As Integer 'how many letters in puzzle ?
        Public Fills As Integer 'how many new words were inserted/filled ?
    End Class

    'How many letters are there in a puzzle ?
    Function ValidCharCount(ByRef matrix As Char(,)) As Integer
        Dim V = 0
        For r As Integer = 0 To matrix.GetLength(0) - 1
            For c As Integer = 0 To matrix.GetLength(1) - 1
                If IsNotEmpty(matrix(r, c)) Then
                    V += 1
                End If
            Next
        Next
        Return V
    End Function

    'Fill empty spaces with possible words
    Function FillPossible(ByRef matrix As Char(,), bottomMost As Integer, topMost As Integer, rightMost As Integer, leftMost As Integer) As Integer
        Dim Fills = 0
        Dim PossMat(ROWS, COLS) As Integer
        Dim P = Possibilities(PossMat, matrix, bottomMost, topMost, rightMost, leftMost)
        While P > 1
            Dim WordLen = P + 1
            Dim Arr = File.ReadAllLines(FPath).Where(Function(L) L.Length = WordLen AndAlso Not _words.Contains(L)).Distinct().ToList
            Dim IsChanged = False
            For c = 0 To COLS - 1
                If IsChanged Then Exit For
                For r = 0 To ROWS - 1
                    If PossMat(r, c) = P Then
                        Dim Chr = matrix(r, c).ToString().ToLower()
                        Dim Seed = Arr.Where(Function(A) A(r) = Chr).ToArray().Rnd().ToArray()
                        If Not String.IsNullOrEmpty(Seed) Then
                            Dim S1 = Seed.Take(r).ToArray()
                            Dim S2 = Seed.Skip(r + 1).ToArray()
                            For c1 = 0 To S1.Length - 1
                                matrix(r - S1.Length + c1, c) = S1(c1)
                            Next
                            For c2 = 0 To S2.Length - 1
                                matrix(r + c2 + 1, c) = S2(c2)
                            Next
                            Fills += 1
                            IsChanged = True
                            Exit For
                        End If
                    End If
                Next
            Next
            P = Possibilities(PossMat, matrix, bottomMost, topMost, rightMost, leftMost)
        End While
        Return Fills
    End Function

    'How many letters can be placed vertically ?
    Function PossibleVertic(ByRef matrix As Char(,), r As Integer, c As Integer, bottomMost As Integer, topMost As Integer, rightMost As Integer, leftMost As Integer) As Integer
        Dim LetterCount = 0
        Dim Prev = False
        For r1 = topMost To Math.Min(bottomMost, r - 1)
            If Prev Then
                LetterCount += 1
                Prev = False
            End If
            Dim T = IsHorizonEmpty(matrix, r1, c, bottomMost, topMost, rightMost, leftMost)
            If T AndAlso Not IsNotEmpty(matrix(r1, c)) Then
                Prev = True
            Else
                Exit For
            End If
        Next
        If Prev Then LetterCount += 1
        Prev = False
        For r2 = Math.Max(topMost, r + 1) To bottomMost
            If Prev Then
                LetterCount += 1
                Prev = False
            End If
            Dim T = IsHorizonEmpty(matrix, r2, c, bottomMost, topMost, rightMost, leftMost)
            If T AndAlso Not IsNotEmpty(matrix(r2, c)) Then
                Prev = True
            Else
                Exit For
            End If
        Next
        If Prev Then LetterCount += 1
        Return LetterCount
    End Function

    'How many letters are possible to be placed more ? 
    Function Possibilities(ByRef PossMat As Integer(,), ByRef matrix As Char(,), bottomMost As Integer, topMost As Integer, rightMost As Integer, leftMost As Integer) As Integer
        Dim Possibles = 0
        For r As Integer = 0 To PossMat.GetLength(0) - 1
            For c As Integer = 0 To PossMat.GetLength(1) - 1
                PossMat(r, c) = 0
            Next
        Next
        For r As Integer = 0 To matrix.GetLength(0) - 1
            For c As Integer = 0 To matrix.GetLength(1) - 1
                If IsNotEmpty(matrix(r, c)) Then
                    If IsVerticEmpty(matrix, r, c, bottomMost, topMost, rightMost, leftMost) Then
                        Dim T = IsHorizonEmpty(matrix, r + 1, c, bottomMost, topMost, rightMost, leftMost)
                        Dim B = IsHorizonEmpty(matrix, r - 1, c, bottomMost, topMost, rightMost, leftMost)
                        If T AndAlso B Then
                            Dim Pos = PossibleVertic(matrix, r, c, bottomMost, topMost, rightMost, leftMost)
                            Possibles = Math.Max(Pos, Possibles)
                            PossMat(r, c) = Pos
                        End If
                    End If
                End If
            Next
        Next
        Return Possibles
    End Function

    'Check right and left box of [r,c] box if is empty or not
    Function IsHorizonEmpty(ByRef matrix As Char(,), r As Integer, c As Integer, bottomMost As Integer, topMost As Integer, rightMost As Integer, leftMost As Integer)
        If r < topMost OrElse r > bottomMost Then Return True

        Dim Left = c - 1
        Dim Rigt = c + 1
        If Left < leftMost Then Left = -1
        If Rigt > rightMost Then Rigt = -1

        Dim leftEmpty = Left < 0 OrElse Not IsNotEmpty(matrix(r, Left))
        Dim RigtEmpty = Rigt < 0 OrElse Not IsNotEmpty(matrix(r, Rigt))
        Return leftEmpty AndAlso RigtEmpty
    End Function

    'Check top and bottom box of [r,c] box if is empty or not
    Function IsVerticEmpty(ByRef matrix As Char(,), r As Integer, c As Integer, bottomMost As Integer, topMost As Integer, rightMost As Integer, leftMost As Integer)
        If c < leftMost OrElse c > rightMost Then Return True

        Dim Top = r - 1
        Dim Bot = r + 1
        If Top < topMost Then Top = -1
        If Bot > bottomMost Then Bot = -1

        Dim TopEmpty = Top < 0 OrElse Not IsNotEmpty(matrix(Top, c))
        Dim BotEmpty = Bot < 0 OrElse Not IsNotEmpty(matrix(Bot, c))
        Return TopEmpty AndAlso BotEmpty
    End Function

    'Check if word isn't empty box
    Function IsNotEmpty(W As Char)
        Return W <> " "c AndAlso W <> "*"c
    End Function

    'Make some letters as capital as indication of fixed letters
    Sub Capitlize(ByRef _words As String)
        Dim Chars = _words.ToArray()
        Dim CapWords = Chars.Length * (CapitalPer.Text / 100)
        Dim CapList As New List(Of Integer)
        For index = 0 To CapWords - 1
            Dim Cell = rand.Next(Chars.Length)
            While CapList.Contains(Cell)
                Cell = rand.Next(Chars.Length)
            End While
            CapList.Add(Cell)
            Chars(Cell) = Chars(Cell).ToString().ToUpper()
        Next
        _words = String.Join(CType(String.Empty, String), CType(Chars, String))
    End Sub

    'Show puzzle data on grid
    Private Sub ShowPuzzle()
        Dim count = _board.N * _board.M
        Dim board = _board.GetBoard
        Dim p = 0

        For i = 0 To _board.N - 1
            For j = 0 To _board.M - 1
                Dim letter = If(board(i, j) = "*"c, " "c, board(i, j))
                If letter <> " "c Then count -= 1
                Dim isCapital = letter.ToString() = letter.ToString().ToUpper()
                Dim B = CType(Grid.Rows(i).Cells(j), DataGridViewTextBoxCell)

                Dim Clr = Color.White
                If Not (letter = " "c) Then
                    Clr = If(isCapital, Color.Green, Color.Lime)
                End If
                B.Value = letter.ToString().ToLower()
                B.Style.BackColor = Clr
                '(CType(grid1.Children(p), Button)).Background = If(letter <> " "c, _buttons(4).Background, _buttons(0).Background)
                p += 1
            Next
        Next
        'blackSquaresLabel.Content = count.ToString()
    End Sub

    'When visulize button clicked, generate random combination
    Private Sub Randomize_Click(sender As Object, e As EventArgs) Handles Randomize.Click
        _words = WordList.Lines.Distinct().Where(Function(L) Not String.IsNullOrEmpty(L)).ToList()
        _words.Reverse()
        _order = _words
        GenCrossword()
    End Sub
End Class
