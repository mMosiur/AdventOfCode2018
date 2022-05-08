using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day01;

public class Day01Solver : DaySolver
{
	private readonly IReadOnlyList<int> _numbers;

	public Day01Solver(string inputFilePath) : base(inputFilePath)
	{
		_numbers = InputLines
			.Select(int.Parse)
			.ToList();
	}

	public override string SolvePart1()
	{
		const int StartingFrequency = 0;
		var analyzer = new FrequencyDriftAnalyzer(StartingFrequency);
		int result = analyzer.GetFrequencyAfterChanges(
			frequencyChanges: _numbers
		);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		const int StartingFrequency = 0;
		var analyzer = new FrequencyDriftAnalyzer(StartingFrequency);
		int result = analyzer.GetFirstFrequencyReachedTwice(
			frequencyChanges: _numbers,
			loopFrequencyChanges: true
		);
		return result.ToString();
	}
}
