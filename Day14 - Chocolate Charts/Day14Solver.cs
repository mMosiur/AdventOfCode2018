using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day14;

public sealed class Day14Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 14;
	public override string Title => "Chocolate Charts";

	private readonly int _inputNumber;
	private readonly byte[] _inputSequence;

	public Day14Solver(Day14SolverOptions options) : base(options)
	{
		try
		{
			_inputNumber = int.Parse(Input);
			_inputSequence = Input.Trim().Select(c => Convert.ToByte(char.GetNumericValue(c))).ToArray();
		}
		catch (Exception e) when (e is FormatException || e is ArgumentNullException)
		{
			throw new ApplicationException("Input was not a number.");
		}
		catch (Exception e) when (e is OverflowException)
		{
			throw new ApplicationException("Input number was too large.");
		}
	}

	public Day14Solver(Action<Day14SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day14Solver() : this(Day14SolverOptions.Default)
	{
	}

	public override string SolvePart1()
	{
		const int ScoresToFindCount = 10;

		HotChocolateScoreboard scoreboard = new();
		int scoresToSkipCount = _inputNumber;
		scoreboard.EnsureCapacity(scoresToSkipCount + ScoresToFindCount + 1);
		int neededScoreCount = scoresToSkipCount + ScoresToFindCount;
		while (scoreboard.Scores.Count < neededScoreCount)
		{
			_ = scoreboard.GenerateNextScores();
		}
		IEnumerable<byte> resultScores = scoreboard.Scores.Skip(scoresToSkipCount).Take(ScoresToFindCount);
		return string.Concat(resultScores);
	}

	public override string SolvePart2()
	{
		const int BatchSize = 1000;
		const int MaxSearchRange = int.MaxValue;

		HotChocolateScoreboard scoreboard = new();
		int scoresToTheLeftCount = 0;
		// Buffer for new scores batch generation
		List<byte> newScores = new(BatchSize + _inputSequence.Length + 1);
		// Buffer for searching for the input sequence
		byte[] sequence = new byte[_inputSequence.Length];
		while (scoreboard.Scores.Count < MaxSearchRange)
		{
			newScores.Clear();
			newScores.AddRange(scoreboard.Scores.TakeLast(_inputSequence.Length - 1));

			int maxSize = BatchSize + newScores.Count;
			while (newScores.Count < maxSize)
			{
				(byte FirstNewScore, byte? SecondNewScore) = scoreboard.GenerateNextScores();
				newScores.Add(FirstNewScore);
				if (SecondNewScore.HasValue)
				{
					newScores.Add(SecondNewScore.Value);
				}
			}
			newScores.CopyTo(0, sequence, 0, sequence.Length);
			for (int i = 0; i < newScores.Count - _inputSequence.Length; i++)
			{
				if (sequence.SequenceEqual(_inputSequence))
				{
					scoresToTheLeftCount += i;
					return scoresToTheLeftCount.ToString();
				}
				sequence.AsSpan()[1..].CopyTo(sequence);
				sequence[^1] = newScores[i + _inputSequence.Length];
			}
			scoresToTheLeftCount += newScores.Count - _inputSequence.Length;
			if (sequence.SequenceEqual(_inputSequence))
			{
				return scoresToTheLeftCount.ToString();
			}
			scoresToTheLeftCount += 1; // Move for next batch search
		}
		throw new ApplicationException($"Input sequence not found to upper range of {MaxSearchRange}.");
	}
}
