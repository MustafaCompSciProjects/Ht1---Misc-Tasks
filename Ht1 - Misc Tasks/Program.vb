Imports System
Imports System.Diagnostics.CodeAnalysis
Imports System.IO
Imports System.Numerics

Module Program
    Function GetNumericalInput(Optional ByVal CanBeDecimal As Boolean = False, Optional ByVal minVal As Integer = -2147483648, Optional ByVal maxVal As Integer = 2147483647) 'Min Inclusive, Max Exclusive
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
                        If UserInput = Int(UserInput) Then
                            Return UserInput
                        Else

                            'All other scenarios, re-ask
                            Console.WriteLine("Please enter a valid number")
                        End If
                    End If
                Else
                    Console.WriteLine("Please enter a valid number")
                End If
            Else
                Console.WriteLine("Please enter a valid number")
            End If

        End While
        End
    End Function
    Function SelectProgram()
        Console.WriteLine("
Which program would you like to use
1. Factorial Finder
2. Speed Tracker
3. Thief
4. Classification
5. Fruit Machine
6. Unit Converter
7. Arithmetic Test
8. Happy Numbers
9. Quit
")
        Return GetNumericalInput(False, 1, 9)
    End Function
    Sub Main(args As String())
        Dim ProgramSelected As Integer


        While True
            ProgramSelected = SelectProgram()
            If ProgramSelected = 1 Then
                Console.Clear()
                FactorialFinder()
            ElseIf ProgramSelected = 2 Then
                Console.Clear()
                SpeedTracker()
            ElseIf ProgramSelected = 3 Then
                Console.Clear()
                Thief()
            ElseIf ProgramSelected = 4 Then
                Console.Clear()
                Classification()
            ElseIf ProgramSelected = 5 Then
                Console.Clear()
                FruitMachine()
            ElseIf ProgramSelected = 6 Then
                Console.Clear()
                UnitConverter()
            ElseIf ProgramSelected = 7 Then
                Console.Clear()
                ArithmeticTest()
            ElseIf ProgramSelected = 8 Then
                Console.Clear()
                HappyNumbers()
            ElseIf ProgramSelected = 9 Then
                Console.WriteLine("Goodbye, World!")
                Environment.Exit(0)
            End If

        End While


    End Sub
    Sub FactorialFinder()
        Console.WriteLine("Please enter the number you want to find the factorial of")

        Dim FactorialNum As BigInteger = BigInteger.Parse(GetNumericalInput())

        For i As Integer = 1 To FactorialNum - 1
            FactorialNum *= i
        Next

        If FactorialNum = 0 Then
            FactorialNum = 1
        End If
        Console.WriteLine(FactorialNum)
        Console.ReadLine()
    End Sub
    Function GetTimeInput()
        Dim UserInput As String
        While True
            UserInput = Console.ReadLine
            Try
                Return TimeOnly.Parse(UserInput)
            Catch ex As Exception
                Console.WriteLine("Please enter a valid time in format HH:MM:SS")
            End Try
        End While
        End
    End Function
    Function GetLicensePlate()
        'https://assets.publishing.service.gov.uk/media/6694e379fc8e12ac3edafc60/inf104-vehicle-registration-numbers-and-number-plates.pdf
        Dim Valid As Boolean = False
        Dim UserInput As String
        Dim ValidLetters As String = "ABCDEFGHIJKLMNPRSTUVWXYZ" 'All letters except O, Q
        While Not Valid
            UserInput = Console.ReadLine
            UserInput = UserInput.Replace(" ", "")

            If Len(UserInput) = 7 Then 'Verify Length
                Valid = True
                For Each i As Char In UserInput
                    If (ValidLetters.Contains(i) And Char.IsUpper(i)) Or IsNumeric(i) Then

                    Else
                        Valid = False
                    End If
                Next
            Else

            End If
            If Not Valid Then
                Console.WriteLine("Please enter a valid UK License plate")
            Else
                Return UserInput
            End If
        End While
        End
    End Function
    Function GetRandomPlateNumber()
        Dim Rnd As New Random
        Dim Alphabet As String = "ABCDEFGHIJKLMNPQRSTUVWXYZ"
        Dim PlateNumber As String = ""

        'Generate plate number using current standard of AB12 CDE
        PlateNumber = PlateNumber & Alphabet(Rnd.Next(0, 25))
        PlateNumber = PlateNumber & Alphabet(Rnd.Next(0, 25))
        PlateNumber = PlateNumber & Rnd.Next(0, 9)
        PlateNumber = PlateNumber & Rnd.Next(0, 9)
        PlateNumber = PlateNumber & " "
        PlateNumber = PlateNumber & Alphabet(Rnd.Next(0, 25))
        PlateNumber = PlateNumber & Alphabet(Rnd.Next(0, 25))
        PlateNumber = PlateNumber & Alphabet(Rnd.Next(0, 25))
        Return PlateNumber

    End Function
    Sub SpeedTracker()
        'Add a few random plates to the list of ticketed vehicles
        Dim rnd As New Random
        For i As Integer = 0 To rnd.Next(4, 6)
            File.AppendAllText("TicketsToGive.txt", GetRandomPlateNumber() & vbCrLf)
        Next

        'Get the times from both cameras
        Console.WriteLine("Please enter the time at speed camera 1")
        Dim Time1 As TimeOnly = GetTimeInput()

        Console.WriteLine("Please enter the time at speed camera 2")
        Dim Time2 As TimeOnly = GetTimeInput()

        Console.WriteLine("Please enter the license plate of the vehicle")
        Dim PlateNumber As String = GetLicensePlate()


        'Calculate the time between the cameras
        Dim TimeDelta As TimeSpan
        TimeDelta = Time2.ToTimeSpan - Time1.ToTimeSpan

        'Convert the time delta to hours
        Dim TimeInHours As Double
        TimeInHours += TimeDelta.Hours
        TimeInHours += TimeDelta.Minutes / 60
        TimeInHours += TimeDelta.Seconds / 60 / 60

        'Calculate speed
        Dim MPH As Double = 1 / TimeInHours

        'If speed is over 70mph, add to the list of tickets to give
        If MPH > 70 Then
            File.AppendAllText("TicketsToGive.txt", PlateNumber & vbCrLf)
            Console.WriteLine(Double.Round(MPH, 2) & "Mph, This person has been ticketed")
        Else
            Console.WriteLine(Double.Round(MPH, 2) & "Mph")
        End If
        Console.ReadLine()
    End Sub
    Function GetMultiDigitInput(Digits)
        While True
            Dim UserInput As String = Console.ReadLine()
            If IsNumeric(UserInput) And Len(UserInput) = Digits Then
                Return UserInput
            Else
                Console.WriteLine("Please enter a valid " & Digits & " digit number")
            End If
        End While
        End
    End Function
    Sub Thief()
        Console.WriteLine("Please enter your 4 known digits")
        Dim Digits As String = GetMultiDigitInput(4)
        Console.WriteLine("The possible combinations are as follows:")

        Dim AllCombos As New List(Of String)
        Dim UniqueCombos As New List(Of String)

        For i As Integer = 0 To 3
            For ii As Integer = 0 To 3
                For iii As Integer = 0 To 3
                    For iiii As Integer = 0 To 3

                        AllCombos.Add((Digits(i) & Digits(ii) & Digits(iii) & Digits(iiii)))

                    Next
                Next
            Next
        Next
        'Remove duplicates

        For Each Combo As String In AllCombos 'Check all combinations

            If (Not UniqueCombos.Contains(Combo)) AndAlso (Combo.Contains(Digits(0)) And Combo.Contains(Digits(1)) And Combo.Contains(Digits(2)) And Combo.Contains(Digits(3))) Then

                UniqueCombos.Add(Combo)
                Console.WriteLine(Combo)

            End If
        Next

        Console.ReadLine()
    End Sub
    Function GetYNInput() As String
        While True
            Dim UserInput As String = Console.ReadLine
            If UserInput.ToLower = "n" Then
                Return "0"
            ElseIf UserInput.ToLower = "y" Then
                Return "1"
            Else
                Console.WriteLine("Please answer with a Y or N")
            End If
        End While
        End
    End Function
    Sub Classification()
        'Declare known distros and pair them with their solutions
        Dim DistroDict As New Dictionary(Of String, String) From {
    {"1111", "SparkyLinux (KDE edition)"},
    {"1110", "Vanilla OS"},
    {"1101", "Manjaro KDE"},
    {"1100", "Manjaro (GNOME edition)"},
    {"1011", "Kubuntu"},
    {"1010", "Ubuntu"},
    {"1001", "Fedora KDE Spin"},
    {"1000", "Fedora Workstation"},
    {"0111", "SolydXK"},
    {"0110", "Kali Linux"},
    {"0101", "Garuda Linux (KDE Dr460nized)"},
    {"0100", "Arch Linux"},
    {"0011", "KDE Neon"},
    {"0010", "Debian"},
    {"0001", "openSUSE Leap (KDE)"},
    {"0000", "Slackware"}
}

        'Get the code of their answers
        Dim UserCode As String = ""

        Console.WriteLine("Are you new to Linux? [Y/N]")
        UserCode = UserCode & GetYNInput()
        Console.WriteLine("Do you prefer constant updates over a long term, stable release? [Y/N]")
        UserCode = UserCode & GetYNInput()
        Console.WriteLine("Do you want a Debian based distro? (Reccomended for beginners) [Y/N]")
        UserCode = UserCode & GetYNInput()
        Console.WriteLine("Do you want it to look Windows-Like? [Y/N]")
        UserCode = UserCode & GetYNInput()

        'Match their options to the list of distros
        Dim SuggestedDistro As String
        Try
            SuggestedDistro = DistroDict.Item(UserCode)
            Console.WriteLine(SuggestedDistro)
        Catch ex As Exception
            Console.WriteLine("We don't have a distro for you in our database, maybe have a google, I'm sure the perfect one for you is out there!")
        End Try
        Console.ReadLine()
    End Sub
    Sub FruitMachine()
        Dim PlayerCredit As Decimal = 0.99D + 0.01D 'Here to work around a VB quirk that keeps decimal places from a calculation
        Dim PossibleEmojis() As String = {"[Cherry]", "[Bell]", "[Lemon]", "[Orange]", "[Star]", "[Skull]"} 'GOD DAM YOU WINDOWS WITH YOUR SHITTY UNICODE IMPLEMENTATION!!!
        Dim rnd As New Random


        Console.WriteLine("Welcome to Fruit Machine!")
        While True
            Console.WriteLine("
Would you like to:
1. Play a round of slots
2. See your balance
3. Stop Playing
")
            Dim UserSelection As Integer = GetNumericalInput(False, 1, 4)

            If UserSelection = 1 Then

                If PlayerCredit < 0.2D Then
                    Console.WriteLine("You do not have enough money to do this")
                Else
                    PlayerCredit += -0.2D
                    Dim Icon1 As String = PossibleEmojis(rnd.Next(0, 6))
                    Dim Icon2 As String = PossibleEmojis(rnd.Next(0, 6))
                    Dim Icon3 As String = PossibleEmojis(rnd.Next(0, 6))

                    Console.WriteLine(Icon1 & Icon2 & Icon3)
                    If Icon1 = Icon2 And Icon2 = Icon3 Then '3 of the same check

                        If Icon1 = PossibleEmojis(1) Then 'bell check
                            Console.WriteLine("3 Bells!! 5GBP has been added to your account")
                            PlayerCredit += 5D

                        ElseIf Icon1 = PossibleEmojis(5) Then 'skull check
                            Console.WriteLine("3 Skulls, you lose everything")
                            PlayerCredit = 0D

                        Else 'any other 3 combo
                            Console.WriteLine("3 in a row! Nice. 1GBP has been added to your account")
                            PlayerCredit += 1D

                        End If

                    ElseIf Icon1 = Icon2 Or Icon2 = Icon3 Or Icon1 = Icon3 Then

                        If Icon1 = PossibleEmojis(5) Or Icon2 = PossibleEmojis(5) Then '2 skulls check
                            Console.WriteLine("2 skulls, you lose 1GBP")
                            PlayerCredit += -1D
                            If PlayerCredit < 0 Then
                                PlayerCredit = 0
                                Console.WriteLine("You're now broke..")
                            End If

                        Else
                            Console.WriteLine("2 of the same, you win 50p")
                            PlayerCredit += 0.5D
                        End If

                    End If
                End If

            ElseIf UserSelection = 2 Then
                Console.WriteLine("You currently have " & PlayerCredit & "GBP in your account")

            ElseIf UserSelection = 3 Then
                Console.Clear()
                Exit While

            Else
                Console.WriteLine("A critical error has occured. Please restart the program")
            End If
        End While
    End Sub
    Sub UnitConverter()
        While True
            Console.WriteLine("
1. Convert C to F
2. Convert F to C
3. Quit
")
            Dim UserSelection As Integer = GetNumericalInput(False, 1, 3)

            If UserSelection = 1 Then
                Console.WriteLine("Enter the value you want to convert")
                Console.WriteLine("Thats " & CInt(GetNumericalInput(True) * 1.8 + 32) & "°F")

            ElseIf UserSelection = 2 Then
                Console.WriteLine("Enter the value you want to convert")
                Console.WriteLine("Thats " & CInt((GetNumericalInput(True) - 32) * 5 / 9) & "°C")

            ElseIf UserSelection = 3 Then
                Console.Clear()
                Exit While

            Else
                Console.WriteLine("A critical error has occured. Please inform the dev to go cry in a corner and fix it")

            End If
        End While
    End Sub
    Sub ArithmeticTest()
        Dim QuestionSheet As New Dictionary(Of String, Decimal)
        Dim rnd As New Random

        Do 'Create a random set of questions every time
            Dim Symbol As Integer = rnd.Next(0, 4)
            Dim Value1 As Integer = rnd.Next(1, 13)
            Dim Value2 As Integer = rnd.Next(1, 13)
            Dim ListOfDivisors() As Integer = {1, 2, 4, 5, 10} 'Here to prevent infinite decimals
            Dim Equation As String
            Dim Solution As Decimal
            If Symbol = 0 Then
                Equation = Value1 & " + " & Value2
                Solution = Value1 + Value2

            ElseIf Symbol = 1 Then
                Equation = Value1 & " - " & Value2
                Solution = Value1 - Value2

            ElseIf Symbol = 2 Then
                Equation = Value1 & " * " & Value2
                Solution = Value1 * Value2

            Else
                Value2 = ListOfDivisors(rnd.Next(0, 5))
                Equation = Value1 & " / " & Value2
                Solution = Value1 / Value2

            End If

            Try
                QuestionSheet.Add(Equation, Solution)
            Catch

            End Try
        Loop Until QuestionSheet.Count = 10 'Create the question sheet

        Console.WriteLine("Welcome to the math test. Please enter your name")
        Dim PlayerName As String = Console.ReadLine()


        While True
            Console.WriteLine("Please select what you'd like to do")
            Console.WriteLine("
1. Take the Test
2. View your latest scores
3. Exit
")
            Dim UserSelection As Integer = GetNumericalInput(False, 1, 3)
            If UserSelection = 1 Then


                Dim PlayerScore As Integer = 0
                For Each i In QuestionSheet
                    Dim iKey As String = i.Key
                    Dim iVal As Decimal = i.Value
                    Console.WriteLine("Please solve " & iKey)
                    If GetNumericalInput(True) = iVal Then
                        Console.WriteLine("Correct!")
                        PlayerScore += 1
                    Else
                        Console.WriteLine("Wrong, have a go at this one instead")
                    End If
                Next

                Console.WriteLine("Your total score was " & PlayerScore)
                File.AppendAllText(PlayerName.ToUpper & ".save", PlayerScore & vbCrLf)

            ElseIf UserSelection = 2 Then


                Try
                    Dim FileContents As String = File.ReadAllText(PlayerName.ToUpper & ".save")

                    Dim Scores() As String
                    Scores = FileContents.Split(vbCrLf)
                    Dim LatestScore As String = 0
                    Dim SecondLatestScore As String = 0
                    Dim ThirdLatestScore As String = 0

                    For i = 0 To Scores.Length - 1
                        If i = Scores.Length - 2 Then
                            LatestScore = Scores(i)
                        ElseIf i = Scores.Length - 3 Then
                            SecondLatestScore = Scores(i)
                        ElseIf i = Scores.Length - 4 Then
                            ThirdLatestScore = Scores(i)
                        End If
                    Next

                    Console.WriteLine("
Your latest scores are as follows" & vbCrLf & "1." &
LatestScore & vbCrLf & "2." &
SecondLatestScore & vbCrLf & "3." &
ThirdLatestScore
)
                Catch ex As Exception
                    Console.WriteLine("Sorry, we don't have your latest scores. Try taking the quiz and trying again")
                End Try
            Else
                Exit While
            End If
        End While
    End Sub
    Function CheckIfHappy(Input As Integer)
        Dim SeenValues As New List(Of Integer)
        Do
            SeenValues.Add(Input)
            Dim Sum As Integer = 0
            For Each Character In Input.ToString
                Sum += Val(Character) ^ 2
            Next

            Input = Sum
        Loop Until SeenValues.Contains(Input) Or Input = 1

        If Input = 1 Then
            Return True
        ElseIf SeenValues.Contains(Input) Then
            Return False
        Else
            Return "PANIC"
        End If

    End Function
    Sub HappyNumbers()
        Dim HappyNums As New List(Of Integer)
        Dim ValToTry As Integer = 1
        While HappyNums.Count < 8
            If CheckIfHappy(ValToTry) Then
                HappyNums.Add(ValToTry)
            End If
            ValToTry += 1
        End While

        Console.WriteLine("The first 8 happy numbers are as follows:")
        For Each i In HappyNums
            Console.WriteLine(i)
        Next
        Console.ReadLine
    End Sub
End Module