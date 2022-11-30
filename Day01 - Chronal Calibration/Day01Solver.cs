using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day01;

public sealed class Day01Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 1;
	public override string Title => "Chronal Calibration";

	private readonly IReadOnlyList<int> _numbers;

	public Day01Solver(Day01SolverOptions options) : base(options)
	{
		_numbers = InputLines
			.Select(int.Parse)
			.ToList();
	}

	public Day01Solver(Action<Day01SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day01Solver() : this(new Day01SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		const int StartingFrequency = 0;
		FrequencyDriftAnalyzer analyzer = new(StartingFrequency);
		int result = analyzer.GetFrequencyAfterChanges(
			frequencyChanges: _numbers
		);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		const int StartingFrequency = 0;
		FrequencyDriftAnalyzer analyzer = new(StartingFrequency);
		int result = analyzer.GetFirstFrequencyReachedTwice(
			frequencyChanges: _numbers,
			loopFrequencyChanges: true
		);
		return result.ToString();
	}
}
