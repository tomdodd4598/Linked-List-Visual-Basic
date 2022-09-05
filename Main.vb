Imports System.Numerics
Imports System.Text.RegularExpressions

Module Main
	ReadOnly ValidRegex = New Regex("^(0|-?[1-9][0-9]*|[A-Za-z][0-9A-Z_a-z]*)$", RegexOptions.Compiled)

	Function IsValidString(str As String) As Boolean
		Return ValidRegex.IsMatch(str)
	End Function

	Function InsertBefore(val As String, oth As Item(Of String)) As Boolean
		Dim x = Nothing, y = Nothing
		If BigInteger.TryParse(val, x) AndAlso BigInteger.TryParse(oth.value, y) Then
			Return x <= y
		Else
			Return val <= oth.value
		End If
	End Function

	Function ValueEquals(item As Item(Of String), val As String) As Boolean
		Return item.value.Equals(val)
	End Function

	Sub Main()
		Dim start As Item(Of String) = Nothing
		Dim begin = True
		Dim input As String

		While True
			If Not begin Then
				Console.WriteLine()
			Else
				begin = False
			End If

			Console.WriteLine("Awaiting input...")
			input = Console.ReadLine()

			If input.Length = 0 Then
				Console.WriteLine(vbLf & "Program terminated!")
				Helpers.RemoveAll(start)
				Return
			ElseIf input.StartsWith("~"c) Then
				If input.Length = 1 Then
					Console.WriteLine(vbLf & "Deleting list...")
					Helpers.RemoveAll(start)
				Else
					input = input.Substring(1)
					If IsValidString(input) Then
						Console.WriteLine(vbLf & "Removing item...")
						Helpers.RemoveItem(start, input, AddressOf ValueEquals)
					Else
						Console.WriteLine(vbLf & "Could not parse input!")
					End If
				End If
			ElseIf input.Equals("l") Then
				Console.WriteLine(vbLf & "Loop print...")
				Helpers.PrintLoop(start)
			ElseIf input.Equals("i") Then
				Console.WriteLine(vbLf & "Iterator print...")
				Helpers.PrintIterator(start)
			ElseIf input.Equals("a") Then
				Console.WriteLine(vbLf & "Array print...")
				Helpers.PrintArray(start)
			ElseIf input.Equals("r") Then
				Console.WriteLine(vbLf & "Recursive print...")
				Helpers.PrintRecursive(start)
			ElseIf input.Equals("f") Then
				Console.WriteLine(vbLf & "Fold print...")
				Helpers.PrintFold(start)
			ElseIf input.Equals("b") Then
				Console.WriteLine(vbLf & "Foldback print...")
				Helpers.PrintFoldback(start)
			ElseIf IsValidString(input) Then
				Console.WriteLine(vbLf & "Inserting item...")
				Helpers.InsertItem(start, input, AddressOf InsertBefore)
			Else
				Console.WriteLine(vbLf & "Could not parse input!")
			End If
		End While
	End Sub

End Module
