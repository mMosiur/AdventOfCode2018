using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day05;

public class Day05Solver : DaySolver
{
	private readonly IEnumerable<char> _polymerUnits;

	public Day05Solver(string inputFilePath) : base(inputFilePath)
	{
		_polymerUnits = Input.Trim();
	}

	public override string SolvePart1()
	{
		Polymer polymer = new(_polymerUnits);
		Polymer result = polymer.GetReactionResult();
		int resultLength = result.Length;
		return resultLength.ToString();
	}

	public override string SolvePart2()
	{
		int shortestPolymerLength = int.MaxValue;
		foreach ((char lower, char upper) in Helpers.GetAsciiAlphabetCasePair())
		{
			Polymer polumer = new(_polymerUnits.Where(c => c != lower && c != upper));
			Polymer result = polumer.GetReactionResult();
			int resultLength = result.Length;
			if (resultLength < shortestPolymerLength)
			{
				shortestPolymerLength = resultLength;
			}
		}
		return shortestPolymerLength.ToString();
	}
}
