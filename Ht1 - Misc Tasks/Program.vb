Imports System
Imports System.IO
Imports System.Numerics

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
                        If UserInput = Int(UserInput) Then
                            Return UserInput
                        Else
                            Console.WriteLine("Please enter a valid number")
                        End If

                    End If


                    'All other scenarios, re-ask
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
End Module
