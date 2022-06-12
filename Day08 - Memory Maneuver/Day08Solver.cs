using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day08;

public class Day08Solver : DaySolver
{
	private readonly int[] _numbers;

	public Day08Solver(Day08SolverOptions options) : base(options)
	{
		_numbers = Input
			.Split(' ')
			.Select(int.Parse)
			.ToArray();
	}

	public Day08Solver(Action<Day08SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		LicenseTree tree = LicenseTree.BuildFromNumberStructure(_numbers);
		int result = tree.Root.CalculateMetadataEntriesSum();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
