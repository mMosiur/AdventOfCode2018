using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day08;

public sealed class Day08Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 8;
	public override string Title => "Memory Maneuver";

	private readonly int[] _numbers;

	private LicenseTree? _licenseTree;

	private LicenseTree LicenseTree => _licenseTree ??= GenerateLicenseTree();

	public Day08Solver(Day08SolverOptions options) : base(options)
	{
		_numbers = Input
			.Split(' ')
			.Select(int.Parse)
			.ToArray();
	}

	private LicenseTree GenerateLicenseTree()
		=> LicenseTree.BuildFromNumberStructure(_numbers);

	public Day08Solver(Action<Day08SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day08Solver() : this(new Day08SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		int result = LicenseTree.Root.CalculateMetadataEntriesSum();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		int result = LicenseTree.Root.CalculateValue();
		return result.ToString();
	}
}
