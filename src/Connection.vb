Module Connection
    'Grid Size 7x4
    Public Const COLS As Integer = 7
    Public Const ROWS As Integer = 4

    'IsOnlyOneSnake
    'To make sure all words are interlinked
    'Considering each letter as star, then comparing whether all connected to each other
    'Helpful to exclude puzzles where some words are outside of link
    'Mostly written by chatgpt, but tested properly to insure stability
    Function IsOnlyOneSnake(pattern(,) As Char) As Boolean
        Dim visited(ROWS, COLS) As Boolean
        Dim connected As Boolean = True
        For i As Integer = 0 To ROWS - 1
            For j As Integer = 0 To COLS - 1
                If Not (pattern(i, j) = " "c OrElse pattern(i, j) = "*"c) AndAlso Not visited(i, j) Then
                    DFS(pattern, visited, i, j)
                    If Not AllStarsConnected(visited, pattern) Then
                        connected = False
                        Exit For
                    End If
                End If
            Next
            If Not connected Then
                Exit For
            End If
        Next
        Return connected
    End Function

    Sub DFS(ByVal pattern(,) As Char, ByRef visited(,) As Boolean, ByVal row As Integer, ByVal col As Integer)
        Dim rowMoves() As Integer = {-1, 1, 0, 0}
        Dim colMoves() As Integer = {0, 0, -1, 1}
        visited(row, col) = True
        For i As Integer = 0 To 3
            Dim newRow As Integer = row + rowMoves(i)
            Dim newCol As Integer = col + colMoves(i)
            If newRow >= 0 AndAlso newRow < ROWS AndAlso newCol >= 0 AndAlso newCol < COLS AndAlso Not (pattern(newRow, newCol) = " "c OrElse pattern(newRow, newCol) = "*"c) AndAlso Not visited(newRow, newCol) Then
                DFS(pattern, visited, newRow, newCol)
            End If
        Next
    End Sub
    Function AllStarsConnected(ByVal visited(,) As Boolean, ByVal pattern(,) As Char) As Boolean
        For i As Integer = 0 To ROWS - 1
            For j As Integer = 0 To COLS - 1
                If visited(i, j) = False AndAlso Not (pattern(i, j) = " "c OrElse pattern(i, j) = "*"c) Then
                    Return False
                End If
            Next
        Next
        Return True
    End Function
End Module
