using AdventOfCode.Abstractions;
using System;

namespace AdventOfCode.Year2018.Day02;

public sealed class Day02Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 2;
	public override string Title => "Inventory Management System";

	public Day02Solver(Day02SolverOptions options) : base(options)
	{
	}

	public Day02Solver(Action<Day02SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day02Solver() : this(new Day02SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int twos = 0;
		int threes = 0;
		CharAnalyzer analyzer = new();
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
		List<string> lines = InputLines.ToList();
		CharAnalyzer analyzer = new();
		for (int i = 0; i < lines.Count; i++)
		{
			string line = lines[i];
			analyzer.Chars = line;
			for (int j = i + 1; j < lines.Count; j++)
			{
				string other = lines[j];
				IEnumerable<int> positions = analyzer.GetPositionsOfDifferences(other);
				int differences = positions.Count();
				if (differences == 1)
				{
					return (line, other, positions.First());
				}
			}
		}
		throw new DaySolverException("No pair that differs by 1 found");
	}

	public override string SolvePart2()
	{
		(string first, _, int differencePosition) = GetFirstPairThatDiffersByOne();
		string result = string.Concat(first.AsSpan(0, differencePosition), first.AsSpan(differencePosition + 1));
		return result;
	}
}
