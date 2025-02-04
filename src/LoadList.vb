Imports System.IO
Imports System.Runtime.CompilerServices

Module List


    'Load word list from path
    'Tries to fill 43% of 7x4 grid
    'Picks only words having 3 or more lenght
    '+ Picks seed (e.g rot), split in to 2 parts i.e ("ro" , "ot")
    'Pick word containing "r" and "o"
    'Pick another word containing "o" and "t"
    'Do same again from "+" line
    Public Sub Load(FPath As String, Optional Min As Integer = 0, Optional Max As Integer = 10, Optional TotalWords As Integer = 4, Optional CapilizedPer As Integer = 20)
        Dim TotalArea = ROWS * COLS
        Dim AllowedArea = TotalArea * 0.43 '43%
        If File.Exists(FPath) Then
            _words.Clear()
            Dim Arr = File.ReadAllLines(FPath).Where(Function(L) L.Length >= 3 AndAlso L.Distinct().Count() >= Max).Distinct().ToList
            Dim Seed = Arr(rand.Next(Arr.Count))
            _words.Add(Seed)
            Arr.Remove(Seed)
            While _words.Count < TotalWords
                Dim D = Seed.Distinct().ToArray()
                Dim Now = D(0) & D(1)
                Dim Nxt = D(1) & D(1 + 1)
                For Each S In New String() {Now, Nxt}
                    Dim MatchArr = Arr.Where(Function(A) MatchCount(A, S) >= 2).ToArray()
                    'Dim MatchArr = Arr.Where(Function(A) S.StartsWith(S) OrElse S.EndsWith(S)).ToArray()
                    If MatchArr.Length = 0 Then
                        Seed = Arr(rand.Next(Arr.Count))
                        Arr.Remove(Seed)
                        Continue While
                    End If
                    Dim MatchSeed = MatchArr(rand.Next(MatchArr.Count))
                    Arr.Remove(MatchSeed)
                    _words.Add(MatchSeed)
                    Seed = MatchSeed
                Next
                'Seed = MatchSeed
            End While
            While _words.Count > TotalWords
                _words.Remove(_words.Last())
            End While

        End If
    End Sub

    'Take unique letters of A and B
    'Tells how many matches found ?
    'Suppose A = "moon" and B = "sun"
    'Distinct/Unique letters in A = "mon" and B = "sun"
    'Matches = 1, only "n" matches in both
    Function MatchCount(A As String, B As String) As Integer
        Dim Count = 0
        Dim Am = A.Distinct().ToArray()
        Dim Bm = B.Distinct().ToArray()
        For Each Ac In Am
            If Bm.Contains(Ac) Then Count += 1
        Next
        Return Count
    End Function

    'Pick random string from array of strings
    <Extension>
    Function Rnd(Arr As String()) As String
        If Arr.Length = 0 Then Return String.Empty
        Return Arr(rand.Next(Arr.Length))
    End Function

    'Pick random string from array of strings
    <Extension>
    Function Rnd(Arr As IOrderedEnumerable(Of String)) As String
        Return Arr(rand.Next(Arr.Count()))
    End Function

    'Random generator
    Public ReadOnly rand As New Random()

    'Loaded words are storred here
    Public _words As New List(Of String)()
End Module
