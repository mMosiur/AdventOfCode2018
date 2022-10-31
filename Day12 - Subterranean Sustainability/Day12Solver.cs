using System.Text.RegularExpressions;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day12;

public sealed class Day12Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 12;
	public override string Title => "Subterranean Sustainability";

	private const int NOF_REPETITIONS_TO_ASSUME_STABLE_DIFF = 100;
	private readonly PotTransformNotes _notes;
	private readonly IEnumerable<PotState> _initialPots;

	public Day12Solver(Day12SolverOptions options) : base(options)
	{
		Regex regex = new(@"initial state: (?<initialState>[#\.]+)(?>\r?\n)+(?<notes>(?>[#\.]+\s*=>\s*[#\.](?:(?>\r?\n)|$))+)");
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

	public Day12Solver(Action<Day12SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day12Solver() : this(Day12SolverOptions.Default)
	{
	}

	private static int CalculatePlantIndexSum(PotRow row)
	{
		return Enumerable.Range(row.FirstNonEmptyIndex, row.LastNonEmptyIndex - row.FirstNonEmptyIndex + 1)
			.Where(i => row[i] is PotState.Plant)
			.Sum();
	}

	public override string SolvePart1()
	{
		const long GENERATIONS_TO_SIMULATE = 20;
		PotRow row = new(_initialPots);
		for (int i = 0; i < GENERATIONS_TO_SIMULATE; i++)
		{
			row.NextGeneration(_notes);
		}
		int sum = CalculatePlantIndexSum(row);
		return sum.ToString();
	}

	public override string SolvePart2()
	{
		const long GENERATIONS_TO_SIMULATE = 50_000_000_000;
		PotRow row = new(_initialPots);
		int lastSum = CalculatePlantIndexSum(row);
		int lastSumDiff = 0;
		int diffRepeats = 0;
		long generationPassed = 0;
		while (generationPassed < GENERATIONS_TO_SIMULATE)
		{
			row.NextGeneration(_notes);
			generationPassed++;
			int sum = CalculatePlantIndexSum(row);
			int sumDiff = sum - lastSum;
			lastSum = sum;
			if (lastSumDiff == sumDiff)
			{
				diffRepeats++;
				if (diffRepeats >= NOF_REPETITIONS_TO_ASSUME_STABLE_DIFF)
				{
					break;
				}
			}
			else
			{
				diffRepeats = 1;
			}
			lastSumDiff = sumDiff;
		}
		long generationPassedToRepeat = GENERATIONS_TO_SIMULATE - generationPassed;
		checked
		{
			long result = lastSum + generationPassedToRepeat * lastSumDiff;
			return result.ToString();
		}
	}
}
