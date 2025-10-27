Imports System

Module Program
    Function GetNumericalInput(Optional ByVal CanBeDecimal As Boolean = False, Optional ByVal minVal As Integer = -2147483648, Optional ByVal maxVal As Integer = 2147483647)
        Dim UserInput As String

        'Loop until valid input is received which breaks the loop
        While True
            UserInput = Console.ReadLine()

            'Check if input is numeric
            If IsNumeric(UserInput) Then
                UserInput = Convert.ToDecimal(UserInput) 'Convert to decimal for range checking

                If UserInput >= minVal And UserInput <= maxVal Then
                    'return input as decimal or integer based on CanBeDecimal
                    If CanBeDecimal Then
                        Return UserInput
                    Else
                        Return Int(UserInput)
                    End If


                    'All other scenarios, re-ask
                Else
                    Console.WriteLine("Please enter a valid number")
                End If
            Else
                Console.WriteLine("Please enter a valid number")
            End If

        End While

    End Function

    Function SelectProgram()
        Console.WriteLine("
Which program would you like to use
1.
2.
3.
4.
5.
6.
7.
8.
9. Quit
")
        Return GetNumericalInput(False, 1, 9)
    End Function

    Sub Main(args As String())
        Dim ProgramSelected As Integer = SelectProgram()

        If ProgramSelected = 1 Then
            Console.WriteLine("1")
        ElseIf ProgramSelected = 2 Then
            Console.WriteLine("2")
        ElseIf ProgramSelected = 3 Then
            Console.WriteLine("3")
        ElseIf ProgramSelected = 4 Then
            Console.WriteLine("4")
        ElseIf ProgramSelected = 5 Then
            Console.WriteLine("5")
        ElseIf ProgramSelected = 6 Then
            Console.WriteLine("6")
        ElseIf ProgramSelected = 7 Then
            Console.WriteLine("7")
        ElseIf ProgramSelected = 8 Then
            Console.WriteLine("8")
        ElseIf ProgramSelected = 9 Then
            Console.WriteLine("Goodbye, World!")
            Environment.Exit(0)
        End If




    End Sub


End Module
