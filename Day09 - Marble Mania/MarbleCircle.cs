namespace AdventOfCode.Year2018.Day09;

class MarbleCircle
{
	private readonly LinkedList<Marble> _marbles = new();
	private LinkedListNode<Marble> _currentMarble;

	public int MarbleCount => _marbles.Count;
	public Marble CurrentMarble => _currentMarble.Value;

	public MarbleCircle(Marble firstMarbleValue)
	{
		_currentMarble = _marbles.AddFirst(firstMarbleValue);
	}

	public void InsertMarbleClockwise(Marble marble)
	{
		_currentMarble = _marbles.AddAfter(_currentMarble, marble);
	}

	public void InsertMarbleCounterClockwise(Marble marble)
	{
		_currentMarble = _marbles.AddBefore(_currentMarble, marble);
	}

	public Marble RemoveMarbleClockwise()
	{
		LinkedListNode<Marble> marbleNode = _currentMarble.Next ?? _marbles.First
			?? throw new InvalidOperationException("MarbleCircle is empty.");
		_marbles.Remove(marbleNode);
		return marbleNode.Value;
	}

	public Marble RemoveMarbleCounterClockwise()
	{
		LinkedListNode<Marble> marbleNode = _currentMarble.Previous ?? _marbles.Last
			?? throw new InvalidOperationException("MarbleCircle is empty.");
		_marbles.Remove(marbleNode);
		return marbleNode.Value;
	}

	public void MoveClockwise(int steps = 1)
	{
		if (steps < 0)
		{
			MoveCounterClockwise(-steps);
			return;
		}
		for (int i = 0; i < steps; i++)
		{
			_currentMarble = _currentMarble.Next ?? _marbles.First
				?? throw new InvalidOperationException("Marble circle is empty.");
		}
	}

	public void MoveCounterClockwise(int steps = 1)
	{
		if (steps < 0)
		{
			MoveClockwise(-steps);
			return;
		}
		for (int i = 0; i < steps; i++)
		{
			_currentMarble = _currentMarble.Previous ?? _marbles.Last
				?? throw new InvalidOperationException("Marble circle is empty.");
		}
	}
}
