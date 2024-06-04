using System;
using System.Collections.Generic;

namespace LimitedSizeStack;

public class LimitedSizeStack<T>
{
	private int _capacity;
	private LinkedList<T> list;
	public LimitedSizeStack(int undoLimit)
	{
		list = new LinkedList<T> { };
		_capacity = undoLimit;
	}

	public void Push(T item)
	{
		list.AddFirst(item);
		if (list.Count > _capacity)
			list.RemoveLast();
	}

	public T Pop()
	{
		if (list.Count == 0)
			throw new InvalidOperationException();
		var outPut = list.First.Value;
		list.RemoveFirst();
		return outPut;
	}

	public int Count {get {return list.Count;}}
}