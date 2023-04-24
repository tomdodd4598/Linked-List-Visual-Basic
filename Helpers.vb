Module Helpers
	Public Sub InsertItem(Of T)(ByRef start As Item(Of T), val As T, insertBefore As Func(Of T, Item(Of T), Boolean))
		Dim current = start, previous As Item(Of T) = Nothing

		While current IsNot Nothing AndAlso Not insertBefore(val, current)
			previous = current
			current = current.next_
		End While
		Dim item = New Item(Of T)(val, current)

		If previous Is Nothing Then
			start = item
		Else
			previous.next_ = item
		End If
	End Sub

	Public Sub RemoveItem(Of T)(ByRef start As Item(Of T), val As T, valueEquals As Func(Of Item(Of T), T, Boolean))
		Dim current = start, previous As Item(Of T) = Nothing

		While current IsNot Nothing AndAlso Not valueEquals(current, val)
			previous = current
			current = current.next_
		End While

		If current Is Nothing Then
			Console.WriteLine($"Item {val} does not exist!")
		Else
			If previous Is Nothing Then
				start = current.next_
			Else
				previous.next_ = current.next_
			End If
			Console.WriteLine($"Removed item: {val}")
		End If
	End Sub

	Public Sub RemoveAll(Of T)(ByRef start As Item(Of T))
		start = Nothing
	End Sub

	Public Sub PrintLoop(Of T)(start As Item(Of T))
		While start IsNot Nothing
			start = start.PrintGetNext()
		End While
	End Sub

	Public Sub PrintIterator(Of T)(start As Item(Of T))
		If start IsNot Nothing Then
			For Each item In start.GetIterator()
				item.PrintGetNext()
			Next
		End If
	End Sub

	Public Sub PrintArray(Of T)(start As Item(Of T))
		Dim item = start
		Dim i = 0
		While item IsNot Nothing
			item = start(i).PrintGetNext()
			i += 1
		End While
	End Sub

	Public Sub PrintRecursive(Of T)(start As Item(Of T))
		If start IsNot Nothing Then
			PrintRecursive(start.PrintGetNext())
		End If
	End Sub

	Public Sub PrintFold(Of T)(ByVal start As Item(Of T))
		Dim fSome = Function(current, next_, accumulator) $"{accumulator}{current.value}, "
		Dim fLast = Function(current, accumulator) $"{accumulator}{current.value}" & vbLf
		Dim fEmpty = Function(accumulator) accumulator
		Console.Write(Item(Of T).Fold(fSome, fLast, fEmpty, "", start))
	End Sub

	Public Sub PrintFoldback(Of T)(ByVal start As Item(Of T))
		Dim fSome = Function(current, next_, innerVal) $"{current.value}, {innerVal}"
		Dim fLast = Function(current) current.value & vbLf
		Dim fEmpty = Function() ""
		Console.Write(Item(Of T).Foldback(fSome, fLast, fEmpty, Function(x) x, start))
	End Sub
End Module
