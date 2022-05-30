using AdventOfCode.Year2018.Day05;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "05")]
[Trait("Day", "5")]
public class Day05Tests : BaseDayTests<Day05Solver, Day05SolverOptions>
{
	public override string DayInputsDirectory => "Day05";

	protected override Day05Solver CreateSolver(Day05SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "10")]
	[InlineData("my-input.txt", "10584")]
	public override void TestPart1(string inputFilename, string expectedResult, Day05SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input.txt", "4")]
	[InlineData("my-input.txt", "6968")]
	public override void TestPart2(string inputFilename, string expectedResult, Day05SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
