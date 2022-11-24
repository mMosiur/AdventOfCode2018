using System.Text.RegularExpressions;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day09;

public sealed class Day09Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 9;
	public override string Title => "Marble Mania";

	private int PlayerCount { get; }
	private int LastMarbleValue { get; }

	public Day09Solver(Day09SolverOptions options) : base(options)
	{
		try
		{
			Match match = Regex.Match(Input, @"^\s*(\d+)\s*players;\s*last\s*marble\s*is\s*worth\s*(\d+)\s*points\s*$", RegexOptions.IgnoreCase);
			if (!match.Success) throw new FormatException("Input was not in the expected format.");
			PlayerCount = int.Parse(match.Groups[1].ValueSpan);
			LastMarbleValue = int.Parse(match.Groups[2].ValueSpan);
		}
		catch (FormatException e)
		{
			throw new InputException("Could not parse input.", e);
		}
	}

	public Day09Solver(Action<Day09SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day09Solver() : this(new Day09SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int lastMarbleValue = LastMarbleValue;
		IEnumerable<Marble> marbles = Enumerable
			.Range(0, lastMarbleValue + 1)
			.Select(i => new Marble(i));
		MarbleGame game = new(marbles, PlayerCount);
		(int winningPlayerIndex, long winningPlayerScore) = game.Play();
		return winningPlayerScore.ToString();
	}

	public override string SolvePart2()
	{
		int lastMarbleValue = LastMarbleValue * 100;
		IEnumerable<Marble> marbles = Enumerable
			.Range(0, lastMarbleValue + 1)
			.Select(i => new Marble(i));
		MarbleGame game = new(marbles, PlayerCount);
		(int winningPlayerIndex, long winningPlayerScore) = game.Play();
		return winningPlayerScore.ToString();
	}
}
