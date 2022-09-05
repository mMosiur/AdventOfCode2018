namespace AdventOfCode.Year2018.Day14;

public class HotChocolateScoreboard
{
	private readonly IList<byte> _scores;
	private int _elf1Index;
	private int _elf2Index;

	public IReadOnlyList<byte> Scores => (IReadOnlyList<byte>)_scores;
	public byte Elf1CurrentScore => _scores[_elf1Index];
	public byte Elf2CurrentScore => _scores[_elf2Index];

	public HotChocolateScoreboard(int numberOfRecipesToSkip)
	{
		if (numberOfRecipesToSkip < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(numberOfRecipesToSkip), $"Number of recipes to skip must be greater than or equal to zero and lower than or equal to {int.MaxValue}.");
		}
		int capacity = Math.Min(numberOfRecipesToSkip + 10, int.MaxValue);
		_scores = new List<byte>(capacity) { 3, 7 };
		_elf1Index = 0;
		_elf2Index = 1;
	}

	public void GenerateNextScores()
	{
		byte firstScore = (byte)(Elf1CurrentScore + Elf2CurrentScore);
		byte? secondScore = null;
		if (firstScore >= 10)
		{
			secondScore = (byte)(firstScore % 10);
			firstScore /= 10;
		}
		_scores.Add(firstScore);
		if (secondScore.HasValue)
		{
			_scores.Add(secondScore.Value);
		}
		_elf1Index = (_elf1Index + Elf1CurrentScore + 1) % _scores.Count;
		_elf2Index = (_elf2Index + Elf2CurrentScore + 1) % _scores.Count;
	}
}
