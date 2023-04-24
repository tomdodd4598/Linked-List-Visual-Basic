Public Class Item(Of T)
	Public ReadOnly value As T
	Public next_ As Item(Of T)

	Public Sub New(value As T, next_ As Item(Of T))
		Console.WriteLine($"Creating item: {value}")
		MyClass.value = value
		MyClass.next_ = next_
	End Sub

	Default Public ReadOnly Property Item(n As Integer) As Item(Of T)
		Get
			Dim current = Me
			For i = 0 To n - 1
				current = current?.next_
			Next
			Return current
		End Get
	End Property

	Public Function PrintGetNext() As Item(Of T)
		Console.Write(String.Format("{0}{1}", value, If(next_ Is Nothing, vbLf, ", ")))
		Return next_
	End Function

	Public Iterator Function GetIterator() As IEnumerable(Of Item(Of T))
		Dim item = Me
		While item IsNot Nothing
			Yield item
			item = item.next_
		End While
	End Function

	Public Shared Function Fold(Of A, R)(fSome As Func(Of Item(Of T), Item(Of T), A, A), fLast As Func(Of Item(Of T), A, R), fEmpty As Func(Of A, R), accumulator As A, item As Item(Of T)) As R
		If item IsNot Nothing Then
			Dim next_ As Item(Of T) = item.next_
			If next_ IsNot Nothing Then
				Return Fold(fSome, fLast, fEmpty, fSome(item, next_, accumulator), next_)
			Else
				Return fLast(item, accumulator)
			End If
		Else
			Return fEmpty(accumulator)
		End If
	End Function

	Public Shared Function Foldback(Of A, R)(fSome As Func(Of Item(Of T), Item(Of T), A, A), fLast As Func(Of Item(Of T), A), fEmpty As Func(Of A), generator As Func(Of A, R), item As Item(Of T)) As R
		If item IsNot Nothing Then
			Dim next_ As Item(Of T) = item.next_
			If next_ IsNot Nothing Then
				Return Foldback(fSome, fLast, fEmpty, Function(innerVal) generator(fSome(item, next_, innerVal)), next_)
			Else
				Return generator(fLast(item))
			End If
		Else
			Return generator(fEmpty())
		End If
	End Function
End Class
