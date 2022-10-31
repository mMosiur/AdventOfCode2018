using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day02;

public sealed class Day02Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 2;
	public override string Title => "XD";

	public Day02Solver(Day02SolverOptions options) : base(options)
	{
	}

	public Day02Solver(Action<Day02SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day02Solver() : this(Day02SolverOptions.Default)
	{
	}

	public override string SolvePart1()
	{
		int twos = 0;
		int threes = 0;
		var analyzer = new CharAnalyzer();
		foreach (string line in InputLines)
		{
			analyzer.Chars = line;
			if (analyzer.GetLettersThatAppearNTimes(2).Any())
			{
				twos++;
			}
			if (analyzer.GetLettersThatAppearNTimes(3).Any())
			{
				threes++;
			}
		}
		int checksum = twos * threes;
		return checksum.ToString();
	}

	private (string First, string Second, int DifferencePosition) GetFirstPairThatDiffersByOne()
	{
		var lines = InputLines.ToList();
		var analyzer = new CharAnalyzer();
		for (int i = 0; i < lines.Count; i++)
		{
			string line = lines[i];
			analyzer.Chars = line;
			for (int j = i + 1; j < lines.Count; j++)
			{
				string other = lines[j];
				var positions = analyzer.GetPositionsOfDifferences(other);
				int differences = positions.Count();
				if (differences == 1)
				{
					return (line, other, positions.First());
				}
			}
		}
		throw new ApplicationException("No pair that differs by 1 found");
	}

	public override string SolvePart2()
	{
		var pair = GetFirstPairThatDiffersByOne();
		string result =
			pair.First.Substring(0, pair.DifferencePosition)
			+ pair.First.Substring(pair.DifferencePosition + 1);
		return result;
	}
}
