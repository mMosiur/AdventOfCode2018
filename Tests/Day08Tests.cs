using AdventOfCode.Year2018.Day08;
using Xunit;

namespace AdventOfCode.Year2018.Tests;

[Trait("Year", "2018")]
[Trait("Day", "08")]
[Trait("Day", "8")]
public class Day08Tests : BaseDayTests<Day08Solver, Day08SolverOptions>
{
	public override string DayInputsDirectory => "Day08";

	protected override Day08Solver CreateSolver(Day08SolverOptions options) => new(options);

	[Theory]
	[InlineData("example-input.txt", "138")]
	[InlineData("my-input.txt", "40984")]
	public override void TestPart1(string inputFilename, string expectedResult, Day08SolverOptions? options = null)
		=> base.TestPart1(inputFilename, expectedResult, options);
}
