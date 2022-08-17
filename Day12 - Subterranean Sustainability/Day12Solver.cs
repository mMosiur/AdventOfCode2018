using System.Text.RegularExpressions;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day12;

public class Day12Solver : DaySolver
{
	private readonly PotTransformNotes _notes;
	private readonly IEnumerable<PotState> _initialPots;

	public Day12Solver(Day12SolverOptions options) : base(options)
	{
		Regex regex = new(@"initial state: (?<initialState>[#\.]+)\n+(?<notes>(?>[#\.]+\s*=>\s*[#\.](?:\n|$))+)");
		Match match = regex.Match(Input);
		IEnumerable<PotTransformNote> rawNotes = match.Groups["notes"].Value
			.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			.Select(PotTransformNote.Parse);
		_notes = new(rawNotes, options.AssumeMissingNotesProduceEmpty ? PotState.Empty : null);
		string initialState = match.Groups["initialState"].Value;
		PotState[] initialPots = new PotState[initialState.Length];
		for (int i = 0; i < initialState.Length; i++)
		{
			initialPots[i] = initialState[i] switch
			{
				'#' => PotState.Plant,
				'.' => PotState.Empty,
				_ => throw new ApplicationException($"Invalid pot state: {initialState[i]}")
			};
		}
		_initialPots = initialPots;
	}

	public Day12Solver(Action<Day12SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		PotRow row = new(_initialPots);
		for(int i = 0; i < 20; i++)
		{
			row.NextGeneration(_notes);
		}
		int sum = Enumerable.Range(row.FirstNonEmptyIndex, row.LastNonEmptyIndex - row.FirstNonEmptyIndex + 1)
			.Where(i => row[i] is PotState.Plant)
			.Sum();
		return sum.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
