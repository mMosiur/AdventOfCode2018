using AdventOfCode.Year2018.Day20;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "20")]
public class Day20Tests : BaseDayTests<Day20Solver, Day20SolverOptions>
{
	public override string DayInputsDirectory => "Day20";

	protected override Day20Solver CreateSolver(Day20SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "3")]
	[InlineData("example-input-2.txt", "10")]
	[InlineData("example-input-3.txt", "18")]
	[InlineData("example-input-4.txt", "23")]
	[InlineData("example-input-5.txt", "31")]
	[InlineData("my-input.txt", "3672")]
	public override void TestPart1(string inputFilename, string expectedResult, Day20SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);
}
