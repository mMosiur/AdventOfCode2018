using AdventOfCode.Year2018.Day23;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "23")]
public class Day23Tests : BaseDayTests<Day23Solver, Day23SolverOptions>
{
	public override string DayInputsDirectory => "Day23";

	protected override Day23Solver CreateSolver(Day23SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "7")]
	[InlineData("my-input.txt", "616")]
	public override void TestPart1(string inputFilename, string expectedResult, Day23SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	[Theory]
	[InlineData("example-input-2.txt", "36")]
	[InlineData("my-input.txt", "93750870")]
	public override void TestPart2(string inputFilename, string expectedResult, Day23SolverOptions? options = null)
		=> base.TestPart2(inputFilename, expectedResult, options);
}
