using AdventOfCode.Year2018.Day25;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "25")]
public class Day25Tests : BaseDayTests<Day25Solver, Day25SolverOptions>
{
	public override string DayInputsDirectory => "Day25";

	protected override Day25Solver CreateSolver(Day25SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input-1.txt", "2")]
	[InlineData("example-input-2.txt", "4")]
	[InlineData("example-input-3.txt", "3")]
	[InlineData("example-input-4.txt", "8")]
	[InlineData("my-input.txt", "386")]
	public override void TestPart1(string inputFilename, string expectedResult, Day25SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);

	// No test for part 2, as there is no part 2.
	// Merry Christmas!
}
