using AdventOfCode.Year2018.Day15;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "15")]
public class Day15Tests : BaseDayTests<Day15Solver, Day15SolverOptions>
{
	public override string DayInputsDirectory => "Day15";

	protected override Day15Solver CreateSolver(Day15SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "27730")]
	[InlineData("example-input-2.txt", "36334")]
	[InlineData("example-input-3.txt", "39514")]
	[InlineData("example-input-4.txt", "27755")]
	[InlineData("example-input-5.txt", "28944")]
	[InlineData("example-input-6.txt", "18740")]
	[InlineData("my-input.txt", "189000")]
	public override void TestPart1(string inputFilename, string expectedResult, Day15SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input-1.txt", "4988")]
	[InlineData("example-input-3.txt", "31284")]
	[InlineData("example-input-4.txt", "3478")]
	[InlineData("example-input-5.txt", "6474")]
	[InlineData("example-input-6.txt", "1140")]
	[InlineData("my-input.txt", "38512")]
	public override void TestPart2(string inputFilename, string expectedResult, Day15SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
