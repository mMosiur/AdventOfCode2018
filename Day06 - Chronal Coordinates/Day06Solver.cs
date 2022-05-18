using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day06;

public class Day06Solver : DaySolver
{
	private const int DefaultMaxTotalDistance = 10_000 - 1;
	private readonly int _maxTotalDistance = DefaultMaxTotalDistance;
	private readonly Point[] _points;

	public Day06Solver(string inputFilePath) : base(inputFilePath)
	{
		_points = InputLines.Select(Point.Parse).ToArray();
	}

	public Day06Solver(string inputFilePath, int maxTotalDistance) : this(inputFilePath)
	{
		_maxTotalDistance = maxTotalDistance;
	}

	public override string SolvePart1()
	{
		ClosestPointCoverageAnalyzer analyzer = new(_points);
		Dictionary<Point, int?> areas = analyzer.GetAreasCovered();
		(Point p, int? count) = areas.Where(kvp => kvp.Value is not null)
			.MaxBy(kvp => kvp.Value);
		int result = count!.Value;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		TotalDistanceCoverageAnalyzer analyzer = new(_points);
		int result = analyzer.GetAreaWithTotalDistanceAtLeast(_maxTotalDistance);
		return result.ToString();
	}
}
