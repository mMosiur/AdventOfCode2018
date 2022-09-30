using AdventOfCode.Year2018.Day18;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "18")]
public class Day18Tests : BaseDayTests<Day18Solver, Day18SolverOptions>
{
	public override string DayInputsDirectory => "Day18";

	protected override Day18Solver CreateSolver(Day18SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "1147")]
	[InlineData("my-input.txt", "638400")]
	public override void TestPart1(string inputFilename, string expectedResult, Day18SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);
}
