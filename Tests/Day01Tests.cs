using AdventOfCode.Year2018.Day01;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "01")]
[Trait("Day", "1")]
public class Day01Tests : BaseDayTests<Day01Solver, Day01SolverOptions>
{
	public override string DayInputsDirectory => "Day01";

	protected override Day01Solver CreateSolver(Day01SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "3")]
	[InlineData("example-input-2.txt", "3")]
	[InlineData("example-input-3.txt", "0")]
	[InlineData("example-input-4.txt", "-6")]
	[InlineData("my-input.txt", "439")]
	public override void TestPart1(string inputFilename, string expectedResult, Day01SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input-1.txt", "2")]
	[InlineData("example-input-5.txt", "0")]
	[InlineData("example-input-6.txt", "10")]
	[InlineData("example-input-7.txt", "5")]
	[InlineData("example-input-8.txt", "14")]
	[InlineData("my-input.txt", "124645")]
	public override void TestPart2(string inputFilename, string expectedResult, Day01SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
