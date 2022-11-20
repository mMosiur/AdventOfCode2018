using AdventOfCode.Year2018.Day24;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "24")]
public class Day24Tests : BaseDayTests<Day24Solver, Day24SolverOptions>
{
	public override string DayInputsDirectory => "Day24";

	protected override Day24Solver CreateSolver(Day24SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "5216")]
	[InlineData("my-input.txt", "9878")]
	public override void TestPart1(string inputFilename, string expectedResult, Day24SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);
}
