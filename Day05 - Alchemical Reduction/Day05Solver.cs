using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day05;

public class Day05Solver : DaySolver
{
	private readonly string _polymerUnits;

	public Day05Solver(Day05SolverOptions options) : base(options)
	{
		_polymerUnits = Input.Trim();
	}

	public Day05Solver(Action<Day05SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		Polymer polymer = new(_polymerUnits);
		Polymer result = polymer.GetReactionResult();
		int resultLength = result.Count;
		return resultLength.ToString();
	}

	public override string SolvePart2()
	{
		int shortestPolymerLength = int.MaxValue;
		int originalSize = _polymerUnits.Length;
		foreach ((char lower, char upper) in Helpers.GetAsciiAlphabetCasePairs())
		{
			Polymer polymer = new(_polymerUnits.Where(c => c != lower && c != upper));
			if (polymer.Count == originalSize)
			{
				// No polymer units were removed.
				continue;
			}
			Polymer result = polymer.GetReactionResult();
			int resultLength = result.Count;
			if (resultLength < shortestPolymerLength)
			{
				shortestPolymerLength = resultLength;
			}
		}
		return shortestPolymerLength.ToString();
	}
}
