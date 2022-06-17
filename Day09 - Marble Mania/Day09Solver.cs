using System.Text.RegularExpressions;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day09;

public class Day09Solver : DaySolver
{
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
			throw new ApplicationException("Could not parse input.", e);
		}
	}

	public Day09Solver(Action<Day09SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		PriorityQueue<Marble, int> marbles = new();
		marbles.EnqueueRange(Enumerable.Range(0, LastMarbleValue + 1).Select(i => (Element: new Marble(i), Priority: i)));
		MarbleCircle circle = new(marbles.Dequeue());
		int currentPlayerIndex = -1;
		int[] playerScores = new int[PlayerCount];
		while (marbles.Count > 0)
		{
			currentPlayerIndex = (currentPlayerIndex + 1) % PlayerCount;
			var marble = marbles.Dequeue();
			if (marble.Number % 23 == 0)
			{
				playerScores[currentPlayerIndex] += marble.Number;
				circle.MoveCounterClockwise(6);
				Marble removedMarble = circle.RemoveMarbleCounterClockwise();
				playerScores[currentPlayerIndex] += removedMarble.Number;
				continue;
			}
			circle.MoveClockwise(1);
			circle.InsertMarbleClockwise(marble);
		}
		return playerScores.Max().ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
