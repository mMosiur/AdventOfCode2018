using AdventOfCode.Year2018.Day17;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "17")]
public class Day17Tests : BaseDayTests<Day17Solver, Day17SolverOptions>
{
	public override string DayInputsDirectory => "Day17";

	protected override Day17Solver CreateSolver(Day17SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "57")]
	[InlineData("my-input.txt", "37649")]
	public override void TestPart1(string inputFilename, string expectedResult, Day17SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);
}
