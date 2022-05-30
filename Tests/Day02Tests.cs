using AdventOfCode.Year2018.Day02;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "02")]
[Trait("Day", "2")]
public class Day02Tests : BaseDayTests<Day02Solver, Day02SolverOptions>
{
	public override string DayInputsDirectory => "Day02";

	protected override Day02Solver CreateSolver(Day02SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "12")]
	[InlineData("my-input.txt", "6474")]
	public override void TestPart1(string inputFilename, string expectedResult, Day02SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input-2.txt", "fgij")]
	[InlineData("my-input.txt", "mxhwoglxgeauywfkztndcvjqr")]
	public override void TestPart2(string inputFilename, string expectedResult, Day02SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
