'This algorithum is mod of following:
'https://www.codeproject.com/Articles/530853/Creating-a-Crossword-Generator
Public Class Crossword
    Const Letters As String = "abcdefghijklmnopqrstuvwxyz"
    ReadOnly _dirX As Integer() = {0, 1}
    ReadOnly _dirY As Integer() = {1, 0}
    Private _board As Char(,)
    ReadOnly _hWords As Integer(,)
    ReadOnly _vWords As Integer(,)
    ReadOnly _n As Integer
    ReadOnly _m As Integer
    Private _hCount, _vCount As Integer
    Shared _rand As Random
    Private Shared _wordsToInsert As IList(Of String)
    Private Shared _tempBoard As Char(,)
    Private Shared _bestSol As Integer
    Private initialTime As DateTime

    Public Sub New(ByVal xDimen As Integer, ByVal yDimen As Integer)
        _board = New Char(xDimen - 1, yDimen - 1) {}
        _hWords = New Integer(xDimen - 1, yDimen - 1) {}
        _vWords = New Integer(xDimen - 1, yDimen - 1) {}
        _n = xDimen
        _m = yDimen
        _rand = New Random()

        For i = 0 To _n - 1
            For j = 0 To _m - 1
                _board(i, j) = " "c
            Next
        Next
    End Sub

    Public Overrides Function ToString() As String
        Dim result As String = ""

        For i As Integer = 0 To _n - 1

            For j As Integer = 0 To _m - 1
                result += If(Letters.Contains(_board(i, j).ToString()), _board(i, j), " "c)
            Next

            If i < _n - 1 Then result += vbLf
        Next

        Return result
    End Function

    Default Public Property Item(ByVal i As Integer, ByVal j As Integer) As Char
        Get
            Return _board(i, j)
        End Get
        Set(ByVal value As Char)
            _board(i, j) = value
        End Set
    End Property

    Public ReadOnly Property N As Integer
        Get
            Return _n
        End Get
    End Property

    Public ReadOnly Property M As Integer
        Get
            Return _m
        End Get
    End Property

    Public Property inRTL As Boolean = False

    Private Function IsValidPosition(ByVal x As Integer, ByVal y As Integer) As Boolean
        Return x >= 0 AndAlso y >= 0 AndAlso x < _n AndAlso y < _m
    End Function

    Private Function CanBePlaced(ByVal word As String, ByVal x As Integer, ByVal y As Integer, ByVal dir As Integer) As Integer
        Dim result = 0

        If dir = 0 Then

            For j = 0 To word.Length - 1
                Dim x1 As Integer = x, y1 As Integer = y + j
                If Not (IsValidPosition(x1, y1) AndAlso (_board(x1, y1) = " "c OrElse _board(x1, y1) = word(j))) Then Return -1

                If IsValidPosition(x1 - 1, y1) Then
                    If _hWords(x1 - 1, y1) > 0 Then Return -1
                End If

                If IsValidPosition(x1 + 1, y1) Then
                    If _hWords(x1 + 1, y1) > 0 Then Return -1
                End If

                If _board(x1, y1) = word(j) Then result += 1
            Next
        Else

            For j = 0 To word.Length - 1
                Dim x1 As Integer = x + j, y1 As Integer = y
                If Not (IsValidPosition(x1, y1) AndAlso (_board(x1, y1) = " "c OrElse _board(x1, y1) = word(j))) Then Return -1

                If IsValidPosition(x1, y1 - 1) Then
                    If _vWords(x1, y1 - 1) > 0 Then Return -1
                End If

                If IsValidPosition(x1, y1 + 1) Then
                    If _vWords(x1, y1 + 1) > 0 Then Return -1
                End If

                If _board(x1, y1) = word(j) Then result += 1
            Next
        End If

        Dim xStar As Integer = x - _dirX(dir), yStar As Integer = y - _dirY(dir)

        If IsValidPosition(xStar, yStar) Then
            If Not (_board(xStar, yStar) = " "c OrElse _board(xStar, yStar) = "*"c) Then Return -1
        End If

        xStar = x + _dirX(dir) * word.Length
        yStar = y + _dirY(dir) * word.Length

        If IsValidPosition(xStar, yStar) Then
            If Not (_board(xStar, yStar) = " "c OrElse _board(xStar, yStar) = "*"c) Then Return -1
        End If

        Return If(result = word.Length, -1, result)
    End Function

    Private Sub PutWord(ByVal word As String, ByVal x As Integer, ByVal y As Integer, ByVal dir As Integer, ByVal value As Integer)
        Dim mat = If(dir = 0, _hWords, _vWords)

        For i = 0 To word.Length - 1
            Dim x1 As Integer = x + _dirX(dir) * i, y1 As Integer = y + _dirY(dir) * i
            _board(x1, y1) = word(i)
            mat(x1, y1) = value
        Next

        Dim xStar As Integer = x - _dirX(dir), yStar As Integer = y - _dirY(dir)
        If IsValidPosition(xStar, yStar) Then _board(xStar, yStar) = "*"c
        xStar = x + _dirX(dir) * word.Length
        yStar = y + _dirY(dir) * word.Length
        If IsValidPosition(xStar, yStar) Then _board(xStar, yStar) = "*"c
    End Sub

    Public Function AddWord(ByVal word As String) As Integer
        Dim wordToInsert = word
        Dim info = BestPosition(wordToInsert)
        If info IsNot Nothing Then
            If info.Item3 = 0 Then
                _hCount += 1
                If inRTL Then wordToInsert = word.Aggregate("", Function(x, y) y + x)
            Else
                _vCount += 1
            End If
            Dim value = If(info.Item3 = 0, _hCount, _vCount)
            PutWord(wordToInsert, info.Item1, info.Item2, info.Item3, value)
            Return info.Item3
        End If
        Return -1
    End Function

    Private Function FindPositions(ByVal word As String) As List(Of Tuple(Of Integer, Integer, Integer))
        Dim max = 0
        Dim positions = New List(Of Tuple(Of Integer, Integer, Integer))()

        For x = 0 To _n - 1

            For y = 0 To _m - 1

                For i = 0 To _dirX.Length - 1
                    Dim dir = i
                    Dim wordToInsert = If(i = 0 AndAlso inRTL, word.Aggregate("", Function(a, b) b + a), word)
                    Dim count = CanBePlaced(wordToInsert, x, y, dir)
                    If count < max Then Continue For
                    If count > max Then positions.Clear()
                    max = count
                    positions.Add(New Tuple(Of Integer, Integer, Integer)(x, y, dir))
                Next
            Next
        Next

        Return positions
    End Function

    Private Function BestPosition(ByVal word As String) As Tuple(Of Integer, Integer, Integer)
        Dim positions = FindPositions(word)
        If positions.Count > 0 Then
            Dim index = _rand.[Next](positions.Count)
            Return positions(index)
        End If

        Return Nothing
    End Function

    Public Function IsLetter(ByVal a As Char) As Boolean
        Return Letters.Contains(a.ToString())
    End Function

    Public Property GetBoard As Char(,)
        Get
            Return _board
        End Get
        Set
            _board = Value
        End Set
    End Property

    Public Sub Reset()
        For i = 0 To _n - 1

            For j = 0 To _m - 1
                _board(i, j) = " "c
                _vWords(i, j) = 0
                _hWords(i, j) = 0
                _hCount = CSharpImpl.__Assign(_vCount, 0)
            Next
        Next
    End Sub

    Public Sub AddWords(ByVal words As IList(Of String), Index As Integer)
        _wordsToInsert = words
        _bestSol = N * M
        initialTime = DateTime.Now
        Gen(0)
        _board = _tempBoard
    End Sub

    Private Function FreeSpaces() As Integer
        Dim count = 0

        For i = 0 To N - 1

            For j = 0 To M - 1
                If _board(i, j) = " "c OrElse _board(i, j) = "*"c Then count += 1
            Next
        Next

        Return count
    End Function

    Private Sub Gen(ByVal pos As Integer)
        If pos >= _wordsToInsert.Count OrElse (DateTime.Now - initialTime).Minutes > 1 Then Return

        For i As Integer = pos To _wordsToInsert.Count - 1
            Dim posi = BestPosition(_wordsToInsert(i))

            If posi IsNot Nothing Then
                Dim word = _wordsToInsert(i)
                If posi.Item3 = 0 AndAlso inRTL Then word = word.Aggregate("", Function(x, y) y + x)
                Dim value = If(posi.Item3 = 0, _hCount, _vCount)
                PutWord(word, posi.Item1, posi.Item2, posi.Item3, value)
                Gen(pos + 1)
                RemoveWord(word, posi.Item1, posi.Item2, posi.Item3)
            Else
                Gen(pos + 1)
            End If
        Next

        Dim c = FreeSpaces()
        If c >= _bestSol Then Return
        _bestSol = c
        _tempBoard = TryCast(_board.Clone(), Char(,))
    End Sub

    Private Sub RemoveWord(ByVal word As String, ByVal x As Integer, ByVal y As Integer, ByVal dir As Integer)
        Dim mat = If(dir = 0, _hWords, _vWords)
        Dim mat1 = If(dir = 0, _vWords, _hWords)

        For i = 0 To word.Length - 1
            Dim x1 As Integer = x + _dirX(dir) * i, y1 As Integer = y + _dirY(dir) * i
            If mat1(x1, y1) = 0 Then _board(x1, y1) = " "c
            mat(x1, y1) = 0
        Next

        Dim xStar As Integer = x - _dirX(dir), yStar As Integer = y - _dirY(dir)
        If IsValidPosition(xStar, yStar) AndAlso HasFactibleValueAround(xStar, yStar) Then _board(xStar, yStar) = " "c
        xStar = x + _dirX(dir) * word.Length
        yStar = y + _dirY(dir) * word.Length
        If IsValidPosition(xStar, yStar) AndAlso HasFactibleValueAround(xStar, yStar) Then _board(xStar, yStar) = " "c
    End Sub

    Private Function HasFactibleValueAround(ByVal x As Integer, ByVal y As Integer) As Boolean
        For i = 0 To _dirX.Length - 1
            Dim x1 As Integer = x + _dirX(i), y1 As Integer = y + _dirY(i)
            If IsValidPosition(x1, y1) AndAlso (_board(x1, y1) <> " "c OrElse _board(x1, y1) = "*"c) Then Return True
            x1 = x - _dirX(i)
            y1 = y - _dirY(i)
            If IsValidPosition(x1, y1) AndAlso (_board(x1, y1) <> " "c OrElse _board(x1, y1) = "*"c) Then Return True
        Next

        Return False
    End Function

    Private Class CSharpImpl
        <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
        Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Class
