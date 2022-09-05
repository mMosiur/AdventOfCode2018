using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day14;

public class Day14Solver : DaySolver
{
	private readonly int _numberOfRecipesToSkip;

	public Day14Solver(Day14SolverOptions options) : base(options)
	{
		try
		{
			_numberOfRecipesToSkip = options.InputNumber ?? int.Parse(Input);
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

	public Day14Solver(Action<Day14SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		HotChocolateScoreboard scoreboard = new(_numberOfRecipesToSkip);
		while (scoreboard.Scores.Count < _numberOfRecipesToSkip + 10)
		{
			scoreboard.GenerateNextScores();
		}
		return string.Join(string.Empty, scoreboard.Scores.Skip((int)_numberOfRecipesToSkip).Take(10));
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
