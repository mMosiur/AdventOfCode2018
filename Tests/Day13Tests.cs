using AdventOfCode.Year2018.Day13;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "13")]
public class Day13Tests : BaseDayTests<Day13Solver, Day13SolverOptions>
{
	public override string DayInputsDirectory => "Day13";

	protected override Day13Solver CreateSolver(Day13SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "0,3")]
	[InlineData("example-input-2.txt", "7,3")]
	[InlineData("my-input.txt", "136,36")]
	public override void TestPart1(string inputFilename, string expectedResult, Day13SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input-3.txt", "6,4")]
	[InlineData("my-input.txt", "53,111")]
	public override void TestPart2(string inputFilename, string expectedResult, Day13SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
