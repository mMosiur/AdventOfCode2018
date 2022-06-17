using AdventOfCode.Year2018.Day09;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2019")]
[Trait("Day", "09")]
[Trait("Day", "9")]
public class Day09Tests : BaseDayTests<Day09Solver, Day09SolverOptions>
{
	public override string DayInputsDirectory => "Day09";

	protected override Day09Solver CreateSolver(Day09SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "32")]
	[InlineData("example-input-2.txt", "8317")]
	[InlineData("example-input-3.txt", "146373")]
	[InlineData("example-input-4.txt", "2764")]
	[InlineData("example-input-5.txt", "54718")]
	[InlineData("example-input-6.txt", "37305")]
	[InlineData("my-input.txt", "398242")]
	public override void TestPart1(string inputFilename, string expectedResult, Day09SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);
}
