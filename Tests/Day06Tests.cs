using AdventOfCode.Year2018.Day06;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "06")]
[Trait("Day", "6")]
public class Day06Tests : BaseDayTests<Day06Solver, Day06SolverOptions>
{
	public override string DayInputsDirectory => "Day06";

	protected override Day06Solver CreateSolver(Day06SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "17")]
	[InlineData("my-input.txt", "2917")]
	public override void TestPart1(string inputFilename, string expectedResult, Day06SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("my-input.txt", "44202")]
	public override void TestPart2(string inputFilename, string expectedResult, Day06SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", 31, "16")]
	[InlineData("my-input.txt", 9999, "44202")]
	public void TestPart2WithCustomOptions(string inputFilename, int maxTotalDistance, string expectedResult)
		=> base.TestPart2(inputFilename, expectedResult, new Day06SolverOptions()
		{
			MaxTotalDistance = maxTotalDistance
		});
}
