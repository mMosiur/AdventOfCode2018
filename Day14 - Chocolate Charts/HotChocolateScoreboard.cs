namespace AdventOfCode.Year2018.Day14;

public class HotChocolateScoreboard
{
	private readonly List<byte> _scores;
	private int _elf1Index;
	private int _elf2Index;

	public IReadOnlyList<byte> Scores => _scores;
	public byte Elf1CurrentScore => _scores[_elf1Index];
	public byte Elf2CurrentScore => _scores[_elf2Index];

	public HotChocolateScoreboard()
	{
		_scores = new List<byte> { 3, 7 };
		_elf1Index = 0;
		_elf2Index = 1;
	}

	public void EnsureCapacity(int capacity)
	{
		_scores.EnsureCapacity(capacity);
	}

	public (byte FirstNewScore, byte? SecondNewScore) GenerateNextScores()
	{
		byte firstScore = (byte)(Elf1CurrentScore + Elf2CurrentScore);
		byte? secondScore = null;
		if (firstScore >= 10)
		{
			secondScore = (byte)(firstScore % 10);
			firstScore = 1; // Scores are always single digits, so their sum is <= 18
		}
		_scores.Add(firstScore);
		if (secondScore.HasValue)
		{
			_scores.Add(secondScore.Value);
		}
		_elf1Index = (_elf1Index + Elf1CurrentScore + 1) % _scores.Count;
		_elf2Index = (_elf2Index + Elf2CurrentScore + 1) % _scores.Count;
		return (firstScore, secondScore);
	}
}
