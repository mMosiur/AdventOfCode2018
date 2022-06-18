namespace AdventOfCode.Year2018.Day09;

public class MarbleGame
{
	private readonly PriorityQueue<Marble, int> _marblesLeft;
	private readonly long[] _playerScores;

	public IReadOnlyCollection<Marble> MarblesLeft => (IReadOnlyCollection<Marble>)_marblesLeft;
	public IReadOnlyList<long> PlayerScores => _playerScores;
	public MarbleCircle Circle { get; }
	public int PlayerCount => _playerScores.Length;

	public MarbleGame(IEnumerable<Marble> marbles, int playerCount)
	{
		_marblesLeft = new(marbles.Select(marble => (marble, marble.Number)));
		_playerScores = new long[playerCount];
		Circle = new(_marblesLeft.Dequeue());
	}

	public (int WinningPlayerIndex, long WinningPlayerScore) Play()
	{
		int currentPlayerIndex = -1;
		while (_marblesLeft.Count > 0)
		{
			currentPlayerIndex = (currentPlayerIndex + 1) % PlayerCount;
			Marble pickedMarble = _marblesLeft.Dequeue();
			if (pickedMarble.Number % 23 == 0)
			{
				_playerScores[currentPlayerIndex] += pickedMarble.Number;
				Circle.MoveCounterClockwise(6);
				Marble removedMarble = Circle.RemoveMarbleCounterClockwise();
				_playerScores[currentPlayerIndex] += removedMarble.Number;
				continue;
			}
			Circle.MoveClockwise(1);
			Circle.InsertMarbleClockwise(pickedMarble);
		}
		return _playerScores
			.Select((score, index) => (index, score))
			.MaxBy(player => player.score);
	}
}
